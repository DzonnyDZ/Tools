Imports System.ComponentModel
Imports System.Globalization.CultureInfo
Imports System.Runtime.InteropServices
Imports Microsoft.Win32.SafeHandles
Imports System.IO.MemoryMappedFiles
Imports System.Text

Namespace InteropT
    ''' <summary>Represents an umnanaged (i.e. Win32) module (i.e. DLL or EXE tec.)</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public NotInheritable Class UnmanagedModule
        Implements IDisposable

        Private hModule As IntPtr

        ''' <summary>Gets handle of the module</summary>
        Public ReadOnly Property Handle As IntPtr
            Get
                Return hModule
            End Get
        End Property

        ''' <summary>Loads an unmanaged module (DLL, EXE, MUI etc.) as datafile for further processing</summary>
        ''' <param name="path">Path to a file to load. It can be any file that is in format of unmanaged executable such as DLL, EXE, MUI, EFX and much other extensions.</param>
        ''' <returns>An instance of <see cref="UnmanagedModule"/> class that represents a library loaded as data file.</returns>
        ''' <remarks>Some operations are not available when library is loaded as data file</remarks>
        ''' <exception cref="API.Win32APIException">Failed to load the library</exception>
        Public Shared Function LoadLibraryAsDataFile(path As String) As UnmanagedModule
            Dim handle = API.Common.LoadLibraryEx(path, IntPtr.Zero, API.LoadLibraryMode.LoadLibraryAsDatafile)
            If handle = IntPtr.Zero Then Throw New API.Win32APIException
            Return New UnmanagedModule(handle, True)
        End Function

        ''' <summary>Load an unmanaged module (DLL, EXE, etc.) for further processing or execution</summary>
        ''' <param name="path">Path to a file to load. It can be any file that is in format of unmanaged executable such as DLL, EXE, MUI, EFX and much other extensions.</param>
        ''' <returns>An instance of <see cref="UnmanagedModule"/> class that represents a library loaded as data file.</returns>
        ''' <exception cref="API.Win32APIException">Failed to load the library</exception>
        Public Shared Function LoadLibrary(path As String) As UnmanagedModule
            Dim handle = API.Common.LoadLibraryEx(path, IntPtr.Zero, 0)
            If handle = IntPtr.Zero Then Throw New API.Win32APIException
            Return New UnmanagedModule(handle, False)
        End Function

        ''' <summary>Well-known identifiers for versioning informations</summary>
        Private Shared ReadOnly versionInfoStringNames As String() = {
            "Comments", "InternalName", "ProductName",
            "CompanyName", "LegalCopyright", "ProductVersion",
            "FileDescription", "LegalTrademarks", "PrivateBuild",
            "FileVersion", "OriginalFilename", "SpecialBuild"
        }

        ''' <summary>Gets versioning information from managed or umnanaged module</summary>
        ''' <param name="path">Path of module to get information from</param>
        ''' <returns>Versioning information</returns>
        Public Shared Function GetVersionInformation(path As String) As ModuleVersionInformation
            Dim size = API.GetFileVersionInfoSize(path, 0)
            If size = 0 Then Throw API.Win32APIException.GetLastWin32Exception
            Dim buffPoint = Marshal.AllocHGlobal(size)
            Try
                If Not API.GetFileVersionInfo(path, 0, size, buffPoint) Then Throw API.Win32APIException.GetLastWin32Exception

                Dim ptr As IntPtr = IntPtr.Zero
                Dim busize As UInteger = 0

                If Not API.VerQueryValue(buffPoint, "\", ptr, busize) Then Throw API.Win32APIException.GetLastWin32Exception
                Dim fixedInfo As API.VS_FIXEDFILEINFO = Marshal.PtrToStructure(ptr, GetType(API.VS_FIXEDFILEINFO))

                If Not API.VerQueryValue(buffPoint, "\VarFileInfo\Translation", ptr, busize) Then Throw API.Win32APIException.GetLastWin32Exception
                Dim langAndCodes(0 To busize / 4 - 1) As Integer
                Marshal.Copy(ptr, langAndCodes, 0, langAndCodes.Length)

                Dim locData As New Dictionary(Of String, IDictionary(Of String, IDictionary(Of String, String)))

                For i = 0 To langAndCodes.Length - 1 Step 2
                    For Each viName In versionInfoStringNames
                        If Not API.VerQueryValue(buffPoint, String.Format("\StringFileInfo\{0:x}-{1:x}\{2}", langAndCodes(i), langAndCodes(i + 1), viName), ptr, busize) Then Throw API.Win32APIException.GetLastWin32Exception
                        Dim value$ = Marshal.PtrToStringAuto(ptr, busize)
                        Dim lang1 As New StringBuilder(1024)
                        Dim lang2 As New StringBuilder(1024)
                        API.VerLanguageName(langAndCodes(i), lang1, lang1.Length)
                        API.VerLanguageName(langAndCodes(i + 1), lang2, lang2.Length)
                        Dim l1 = lang1.ToString
                        Dim l2 = lang2.ToString

                        If Not locData.ContainsKey(l1) Then locData.Add(l1, New Dictionary(Of String, IDictionary(Of String, String)))
                        If Not locData(l1).ContainsKey(l2) Then locData(l1).Add(l2, New Dictionary(Of String, String))
                        locData(l1)(l2)(viName) = value
                    Next
                Next

                Return New ModuleVersionInformation(fixedInfo, locData)

            Finally
                Marshal.FreeHGlobal(buffPoint)
            End Try

        End Function

#Region "IDisposable Support"
        ''' <summary>To detect redundant calls</summary>
        Private disposedValue As Boolean

        ''' <summary>Implements <see cref="IDisposable.Dispose"/></summary>
        Protected Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    'Dispose managed state (managed objects).
                End If

                'Free unmanaged resources (unmanaged objects) and override Finalize() below.
                API.FreeLibrary(hModule)
                hModule = IntPtr.Zero
            End If
            Me.disposedValue = True
        End Sub


        ''' <summary>Allows an <see cref="T:System.Object" /> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Object" /> is reclaimed by garbage collection.</summary>
        Protected Overrides Sub Finalize()
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(False)
            MyBase.Finalize()
        End Sub


        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        ''' <filterpriority>2</filterpriority>
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

        ''' <summary>Gets a pointer to native method (or variable) in the module</summary>
        ''' <param name="procedureName">Name of the method to get pointer to</param>
        ''' <returns>Pointer to native method with given name</returns>
        ''' <exception cref="API.Win32APIException">Failed to obtain method address</exception>
        ''' <remarks>The spelling and case of a function name pointed to by <paramref name="procedureName"/> must be identical to that in the EXPORTS statement of the source DLL's module-definition (.def) file.</remarks>
        Public Function GetProcedureAddress(procedureName$) As IntPtr
            Dim ret = API.GetProcAddress(Handle, procedureName)
            If ret = IntPtr.Zero Then Throw New API.Win32APIException
            Return ret
        End Function

        ''' <summary>Attempts to gets a pointer to native method (or variable) in the module</summary>
        ''' <param name="procedureName">Name of the method to get pointer to</param>
        ''' <returns>Pointer to native method with given name, <see cref="IntPtr.Zero"/> if the method cannot be found or operation failed.</returns>
        ''' <remarks>The spelling and case of a function name pointed to by <paramref name="procedureName"/> must be identical to that in the EXPORTS statement of the source DLL's module-definition (.def) file.</remarks>
        Public Function TryGetProcedureAddress(procedureName$) As IntPtr
            Dim ret = API.GetProcAddress(Handle, procedureName)
            Return ret
        End Function

        ''' <summary>Gets list of types of resources in this module</summary>
        ''' <returns>List of all types of resources in this module</returns>
        ''' <exception cref="API.Win32APIException">Failed to obtain resource list</exception>
        Public Function GetResourceTypes() As IList(Of ResourceTypeIdentifier)
            Dim ret As New List(Of ResourceTypeIdentifier)
            If Not API.Common.EnumResourceTypes(hModule, Function(hModule, lpszType, lParam)
                                                             ret.Add(New ResourceTypeIdentifier(lpszType))
                                                             Return True
                                                         End Function, IntPtr.Zero
                                               ) Then Throw New API.Win32APIException
            Return ret
        End Function

        ''' <summary>Gets list of all resources of given typ in this module</summary>
        ''' <param name="type">Name of type. To get list of available resource types use <see cref="GetResourceTypes"/>.</param>
        ''' <returns>List of names of all resource is this module.</returns>
        Public Function GetResourceNames(type As ResourceTypeIdentifier) As IList(Of ResourceIdentifier)
            If type = ResourceTypeIdentifier.Empty Then Throw New ArgumentException("Value cannot be empty", "type")
            Dim ret As New List(Of ResourceIdentifier)
            Dim callback = Function(hModule As IntPtr, lpszType As IntPtr, lpszName As IntPtr, lParam As IntPtr)
                               ret.Add(New ResourceIdentifier(lpszName))
                               Return True
                           End Function
            Dim result As Boolean
            If type.Name Is Nothing Then
                result = API.Common.EnumResourceNames(hModule, CType(type.Id, IntPtr), callback, IntPtr.Zero)
            Else
                result = API.Common.EnumResourceNames(hModule, type.Name, callback, IntPtr.Zero)
            End If
            If Not result Then Throw New API.Win32APIException
            Return ret
        End Function

#Region "GetResourceStream"
        ''' <summary>Gets stream of unmanaged resources of given type and name</summary>
        ''' <param name="type">Type of the resource. To get available types of resources call <see cref="GetResourceTypes"/>.</param>
        ''' <param name="identifier">Name of resource. To get available names of resources for particular type use <see cref="GetResourceNames"/>.</param>
        ''' <returns>A stream that can be used to read resource data.</returns>
        ''' <exception cref="API.Win32APIException">Failed to get resource data</exception>
        Public Function GetResourceStream(type As ResourceTypeIdentifier, identifier As ResourceIdentifier) As IO.Stream
            If type = ResourceTypeIdentifier.Empty Then Throw New ArgumentException("Value cannot be empty", "type")
            If identifier = ResourceIdentifier.Empty Then Throw New ArgumentException("Value cannot be empty", "name")
            Dim rph As IntPtr
            If type.Name IsNot Nothing AndAlso identifier.Name IsNot Nothing Then
                rph = API.FindResource(hModule, identifier.Name, type.Name)
            ElseIf type.Name IsNot Nothing AndAlso identifier.Id IsNot Nothing Then
                rph = API.FindResource(hModule, CType(identifier.Id, IntPtr), type.Name)
            ElseIf type.Id IsNot Nothing AndAlso identifier.Id IsNot Nothing Then
                rph = API.FindResource(hModule, CType(identifier.Id, IntPtr), CType(type.Id, IntPtr))
            ElseIf type.Id IsNot Nothing AndAlso identifier.Name IsNot Nothing Then
                rph = API.FindResource(hModule, identifier.Name, CType(type.Id, IntPtr))
            Else
                Throw New ApplicationException("Invalid combination of type and name") 'Should not happen
            End If
            If rph = IntPtr.Zero Then Throw New API.Win32APIException
            Return GetResourceStream(rph)
        End Function


        '<EditorBrowsable(EditorBrowsableState.Advanced)>
        'Public Function GetResourceStream(type$, id%) As IO.Stream
        '    Return GetResourceStream(type, "#" & id.ToString(InvariantCulture))
        'End Function

        '<EditorBrowsable(EditorBrowsableState.Advanced)>
        'Public Function GetResourceStream(typeId%, id%) As IO.Stream
        '    Return GetResourceStream("#" & typeId.ToString(InvariantCulture), "#" & id.ToString(InvariantCulture))
        'End Function

        ''' <summary>Gets stream of unmanaged resources of given type and name</summary>
        ''' <param name="hRes">Handle to resource</param>
        ''' <returns>A stream that can be used to read resource data.</returns>
        ''' <exception cref="API.Win32APIException">Failed to obtain resource data</exception>
        Private Function GetResourceStream(hRes As IntPtr) As IO.Stream
            Dim size = API.Common.SizeofResource(Handle, hRes)
            Dim pt = API.Common.LoadResource(hModule, hRes)
            If pt = IntPtr.Zero Then Throw New API.Win32APIException()
            Return New IOt.UnmanagedMemoryStream(pt, size)
        End Function
#End Region
        ''' <summary>CTor - creates anew instance of the <see cref="UnmanagedModule"/> class</summary>
        ''' <param name="hModule">Handle of the module</param>
        ''' <param name="loadedAsDataFile">Indicates if library was loaded as data file</param>
        Private Sub New(hModule As IntPtr, loadedAsDataFile As Boolean)
            Me.hModule = hModule
            Me._loadedAsDataFile = loadedAsDataFile
        End Sub

        Private ReadOnly _loadedAsDataFile As Boolean
        ''' <summary>Gets value indicating if the library was loaded as data file or loaded for execution</summary>
        ''' <returns>True if the library was loaded as data file, false if it was loaded for execution</returns>
        Public ReadOnly Property LoadedAsDataFile As Boolean
            Get
                Return _loadedAsDataFile
            End Get
        End Property

        ''' <summary>Loads a string from module identified by resource ID</summary>
        ''' <param name="id">ID of string to load</param>
        ''' <returns>The string for given id</returns>
        ''' <remarks>This overload is not CLS-compliant. CLS-compliant overload is provided.</remarks>
        ''' <exception cref="API.Win32APIException">Failed to load string from module</exception>
        <CLSCompliant(False)>
        Public Function LoadString(id As UInteger) As String
            'When passing 0 to nBufferMax LoadString writes pointer to the buffer to memory pointed by ptr
            Dim bsize% = 2048
            Dim ret%
            Dim b As StringBuilder
            Do
                bsize *= 2
                b = New StringBuilder(bsize)
                ret = API.LoadString(Handle, id, b, bsize)
                If ret < 0 OrElse (ret = 0 AndAlso Marshal.GetLastWin32Error <> API.Common.ERROR_SUCCESS) Then Throw New API.Win32APIException
            Loop While ret = bsize 'This seems to be the only relaible way to load string longer than initial bufer size
            Return b.ToString
        End Function

        ''' <summary>Loads a string from module identified by resource ID</summary>
        ''' <param name="id">ID of string to load</param>
        ''' <returns>The string for given id</returns>
        ''' <remarks>This overload is provided only for CLS-compliance</remarks>
        ''' <exception cref="API.Win32APIException">Failed to load string from module</exception>
        ''' <exception cref="OverflowException"><paramref name="id"/> is greater than <see cref="UInteger.MaxValue"/> or less than zero</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Function LoadString(id As Long) As String
            Return LoadString(CUInt(id))
        End Function
    End Class

    ''' <summary>Unmanaged resource identificator</summary>
    ''' <remarks>Win32 resources are identified either using <see cref="UShort"/> ID or <see cref="String"/> name</remarks>
    ''' <version version="1.5.4">This structure is new in version 1.5.4</version>
    Public Structure ResourceIdentifier
        Private ReadOnly _id As UShort?
        Private ReadOnly _name As String
        ''' <summary>CTor - creates a new instance of the <see cref="ResourceIdentifier"/> structure from resource ID</summary>
        ''' <param name="id">Resource ID</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is out of range ot type <see cref="UShort"/>.</exception>
        Public Sub New(id As Integer)
            If id < 0 OrElse id > UShort.MaxValue Then Throw New ArgumentOutOfRangeException("id")
            _id = id
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="ResourceIdentifier"/> form a pointer that represents either ID of the resource or name of the resource.</summary>
        ''' <param name="lpszName">Pointer. Value less than or equal to <see cref="UShort.MaxValue"/> represents ID otherwise pointer to name.</param>
        Public Sub New(lpszName As IntPtr)
            If API.Common.IsIntRresource(lpszName) Then
                _id = lpszName.ToInt32
            Else
                _name = Marshal.PtrToStringAuto(lpszName)
            End If
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="ResourceIdentifier"/> structure from resource name</summary>
        ''' <param name="name">resource name</param>
        ''' <exception cref="ArgumentNullException"><paramref name="name"/> is null</exception>
        Public Sub New(name$)
            If name Is Nothing Then Throw New ArgumentNullException("name")
            _name = name
        End Sub
        ''' <summary>Gets ID of resource</summary>
        ''' <returns>Resource ID or null</returns>
        Public ReadOnly Property Id As Integer?
            Get
                Return _id
            End Get
        End Property

        ''' <summary>Gets name of resource</summary>
        ''' <returns>Resource name or null</returns>
        Public ReadOnly Property Name$
            Get
                Return _name
            End Get
        End Property

        ''' <summary>Gets resource identifier as object</summary>
        ''' <returns>Either <see cref="Int32"/> or <see cref="String"/></returns>
        Public ReadOnly Property Identifier As Object
            Get
                Return If(CObj(Id), Name)
            End Get
        End Property

        ''' <summary>Always gets resource name</summary>
        ''' <returns>Either <see cref="Name"/> or <see cref="Id"/> in form #<see cref="Id"/>.</returns>
        Public Overrides Function ToString() As String
            If Name Is Nothing AndAlso Id Is Nothing Then Return ""
            Return If(Name, "#" & Id.Value.ToString(InvariantCulture))
        End Function

        ''' <summary>Gets an empty instance of <see cref="ResourceIdentifier"/></summary>
        Public Shared ReadOnly Property Empty As ResourceIdentifier
            Get
                Return New ResourceIdentifier
            End Get
        End Property

        ''' <summary>Converts <see cref="String"/> to <see cref="ResourceIdentifier"/></summary>
        ''' <param name="name">Name of resource</param>
        ''' <returns>A new insdtance of the <see cref="ResourceIdentifier"/> structure with <see cref="Name"/> set to <paramref name="name"/>. <see cref="Empty"/> if <paramref name="name"/> is null.</returns>
        Public Shared Widening Operator CType(name$) As ResourceIdentifier
            If name Is Nothing Then Return Empty
            Return New ResourceIdentifier(name)
        End Operator

        ''' <summary>Compares two instances of <see cref="ResourceIdentifier"/> for equality</summary>
        ''' <param name="a">A <see cref="ResourceIdentifier"/></param>
        ''' <param name="b">A <see cref="ResourceIdentifier"/></param>
        ''' <returns>True if the two instances are equal, false otherwise</returns>
        Public Shared Operator =(a As ResourceIdentifier, b As ResourceIdentifier) As Boolean
            If (a.Name Is Nothing) <> (b.Name Is Nothing) OrElse (a.Id Is Nothing) <> (b.Id Is Nothing) Then Return False
            If a.Name Is Nothing AndAlso a.Id Is Nothing Then Return True
            If a.Name IsNot Nothing Then Return a.Name = b.Name
            Return a.Id = b.Id
        End Operator

        ''' <summary>Compares two instances of <see cref="ResourceIdentifier"/> for inequality</summary>
        ''' <param name="a">A <see cref="ResourceIdentifier"/></param>
        ''' <param name="b">A <see cref="ResourceIdentifier"/></param>
        ''' <returns>False if the two instances are equal, tue otherwise</returns>
        Public Shared Operator <>(a As ResourceIdentifier, b As ResourceIdentifier) As Boolean
            Return Not (a = b)
        End Operator

        ''' <summary>Indicates whether this instance and a specified object are equal.</summary>
        ''' <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.</returns>
        ''' <param name="obj">Another object to compare to. </param>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is ResourceIdentifier Then Return Me = DirectCast(obj, ResourceIdentifier)
            Return False
        End Function

        ''' <summary>Converts <see cref="ResourceTypeIdentifier"/> to <see cref="ResourceIdentifier"/></summary>
        ''' <param name="a">A <see cref="ResourceTypeIdentifier"/></param>
        ''' <returns>A new instance of the <see cref="ResourceIdentifier"/> initialized to same values as <paramref name="a"/></returns>
        Public Shared Widening Operator CType(a As ResourceTypeIdentifier) As ResourceIdentifier
            If a = ResourceTypeIdentifier.Empty Then Return Empty
            If a.Name IsNot Nothing Then Return New ResourceIdentifier(a.Name)
            Return New ResourceIdentifier(CInt(a.Id.Value))
        End Operator
    End Structure

    ''' <summary>Predefined unmanaged resource types</summary>
    ''' <version version="1.5.4">This enumeration is new in verison 1.5.4</version>
    Public Enum ResourceTypeId
        ''' <summary>Accelerator table.</summary>
        Accelerator = 9
        ''' <summary>Animated cursor.</summary>
        AniCursor = 21
        ''' <summary>Animated icon.</summary>
        AniIcon = 22
        ''' <summary>Bitmap</summary>
        Bitmap = 2
        ''' <summary>Hardware-dependent cursor resource.</summary>
        Cursor = 1
        ''' <summary>Dialog box.</summary>
        Dialog = 5
        ''' <summary>Allows a resource editing tool to associate a string with an .rc file. Typically, the string is the name of the header file that provides symbolic names. The resource compiler parses the string but otherwise ignores the value.</summary>
        DlgInclude = 17
        ''' <summary>Font resource.</summary>
        Font = 8
        ''' <summary>Font directory resource.</summary>
        FontDir = 7
        ''' <summary>Hardware-independent cursor resource.</summary>
        GroupCursor = Cursor + 11
        ''' <summary>Hardware-independent icon resource</summary>
        IconGroup = Icon + 11
        ''' <summary>HTML resource.</summary>
        Html = 23
        ''' <summary>Hardware-dependent icon resource.</summary>
        Icon = 3
        ''' <summary>Side-by-Side Assembly Manifest.</summary>
        Manifest = 24
        ''' <summary>Menu resource.</summary>
        Menu = 4
        ''' <summary>Message-table entry.</summary>
        MessageTable = 11
        ''' <summary>Plug and Play resource.</summary>
        PlugAndPlay = 19
        ''' <summary>Application-defined resource (raw data).</summary>
        Data = 10
        ''' <summary>String-table entry.</summary>
        [String] = 6
        ''' <summary>Version resource.</summary>
        Version = 16
        ''' <summary>VXD</summary>
        Vxd = 20
    End Enum

    ''' <summary>Identificator of unmanaged resource type</summary>
    ''' <remarks>Win32 resources are identified either using <see cref="UShort"/> ID or <see cref="String"/> name</remarks>
    ''' <version version="1.5.4">This structure is new in version 1.5.4</version>
    Public Structure ResourceTypeIdentifier
        Private ReadOnly _id As ResourceTypeId?
        Private ReadOnly _name As String
        ''' <summary>CTor - creates a new instance of the <see cref="ResourceIdentifier"/> structure from resource type ID</summary>
        ''' <param name="id">Resource type ID (can be one of predefined values)</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is out of range ot type <see cref="UShort"/>.</exception>
        Public Sub New(id As ResourceTypeId)
            If id < 0 OrElse id > UShort.MaxValue Then Throw New ArgumentOutOfRangeException("id")
            _id = id
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="ResourceIdentifier"/> form a pointer that represents either type ID or name of the type.</summary>
        ''' <param name="lpszName">Pointer. Value less than or equal to <see cref="UShort.MaxValue"/> represents ID otherwise pointer to name.</param>
        Public Sub New(lpszName As IntPtr)
            If API.Common.IsIntRresource(lpszName) Then
                _id = lpszName.ToInt32
            Else
                _name = Marshal.PtrToStringAnsi(lpszName)
            End If
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="ResourceIdentifier"/> structure from resource type name</summary>
        ''' <param name="name">resource type name</param>
        ''' <exception cref="ArgumentNullException"><paramref name="name"/> is null</exception>
        Public Sub New(name$)
            If name Is Nothing Then Throw New ArgumentNullException("name")
            _name = name
        End Sub
        ''' <summary>Gets ID of resource type</summary>
        ''' <returns>Resource ID or null</returns>
        Public ReadOnly Property Id As ResourceTypeId?
            Get
                Return _id
            End Get
        End Property

        ''' <summary>Gets name of resource type</summary>
        ''' <returns>Resource name or null</returns>
        Public ReadOnly Property Name$
            Get
                Return _name
            End Get
        End Property

        ''' <summary>Gets resource identifier as object</summary>
        ''' <returns>Either <see cref="ResourceTypeId"/> or <see cref="String"/></returns>
        Public ReadOnly Property Identifier As Object
            Get
                Return If(CObj(Id), Name)
            End Get
        End Property

        ''' <summary>Always gets resource name</summary>
        ''' <returns>Either <see cref="Name"/> or <see cref="Id"/> in form #<see cref="Id"/>.</returns>
        Public Overrides Function ToString() As String
            If Name Is Nothing AndAlso Id Is Nothing Then Return ""
            Return If(Name, "#" & CInt(Id.Value).ToString(InvariantCulture))
        End Function

        ''' <summary>Gets an empty instance of <see cref="ResourceTypeIdentifier"/></summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Shared ReadOnly Property Empty As ResourceTypeIdentifier
            Get
                Return New ResourceTypeIdentifier
            End Get
        End Property

        ''' <summary>Converts string to <see cref="ResourceTypeIdentifier"/></summary>
        ''' <param name="name">Resource type name</param>
        ''' <returns>A new insnatce of <see cref="ResourceTypeIdentifier"/> with <see cref="Name"/> set to <paramref name="name"/>. <see cref="Empty"/> if <paramref name="name"/> is null.</returns>
        Public Shared Widening Operator CType(name$) As ResourceTypeIdentifier
            If name Is Nothing Then Return Empty
            Return New ResourceTypeIdentifier(name)
        End Operator

        ''' <summary>Converts <see cref="ResourceTypeId"/> to <see cref="ResourceTypeIdentifier"/></summary>
        ''' <param name="Id">Identifies resource type</param>
        ''' <returns>A new insnatce of <see cref="ResourceTypeIdentifier"/> with <see cref="Id"/> set to <paramref name="id"/>.</returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="id"/> is out of range ot type <see cref="UShort"/>.</exception>
        Public Shared Widening Operator CType(id As ResourceTypeId) As ResourceTypeIdentifier
            Return New ResourceTypeIdentifier(id)
        End Operator

        ''' <summary>Compares two instance of <see cref="ResourceTypeIdentifier"/> for equality</summary>
        ''' <param name="a">A <see cref="ResourceTypeIdentifier"/></param>
        ''' <param name="b">A <see cref="ResourceTypeIdentifier"/></param>
        ''' <returns>True if the two instances are equal, false otherwise</returns>
        Public Shared Operator =(a As ResourceTypeIdentifier, b As ResourceTypeIdentifier) As Boolean
            If (a.Name Is Nothing) <> (b.Name Is Nothing) OrElse (a.Id Is Nothing) <> (b.Id Is Nothing) Then Return False
            If a.Name Is Nothing AndAlso a.Id Is Nothing Then Return True
            If a.Name IsNot Nothing Then Return a.Name = b.Name
            Return a.Id = b.Id
        End Operator

        ''' <summary>Compares two instance of <see cref="ResourceTypeIdentifier"/> for inequality</summary>
        ''' <param name="a">A <see cref="ResourceTypeIdentifier"/></param>
        ''' <param name="b">A <see cref="ResourceTypeIdentifier"/></param>
        ''' <returns>False if the two instances are equal, true otherwise</returns>
        Public Shared Operator <>(a As ResourceTypeIdentifier, b As ResourceTypeIdentifier) As Boolean
            Return Not (a = b)
        End Operator

        ''' <summary>Indicates whether this instance and a specified object are equal.</summary>
        ''' <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.</returns>
        ''' <param name="obj">Another object to compare to. </param>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is ResourceTypeIdentifier Then Return Me = DirectCast(obj, ResourceTypeIdentifier)
            Return False
        End Function

        ''' <summary>Converts <see cref="ResourceIdentifier"/> to <see cref="ResourceTypeIdentifier"/></summary>
        ''' <param name="a">A <see cref="ResourceIdentifier"/></param>
        ''' <returns><see cref="ResourceTypeIdentifier"/> with same values as <paramref name="a"/></returns>
        Public Shared Widening Operator CType(a As ResourceIdentifier) As ResourceTypeIdentifier
            If a = ResourceIdentifier.Empty Then Return Empty
            If a.Name IsNot Nothing Then Return New ResourceTypeIdentifier(a.Name)
            Return New ResourceTypeIdentifier(CType(a.Id.Value, ResourceTypeId))
        End Operator
    End Structure

End Namespace
