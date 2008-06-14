Imports Tools.COM.ShellLink
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.ComponentModel

#If Config <= Nightly Then 'Stage Nightly
Namespace IOt
    ''' <summary>Represents a *.LNK file (also called shortcut or link)</summary>
    Public Class ShellLink
        Implements IDisposable
        ''' <summary>The <see cref="IShellLinkW"/> object this class is wrapper to</summary>
        Private link As IShellLinkW
        Private path$
        ''' <summary>Creates new instance of <see cref="ShellLink"/> from <see cref="IShellLinkW"/></summary>
        ''' <param name="link">A <see cref="IShellLinkW"/> object</param>
        ''' <param name="Path">Path where the link will be saved</param>
        Private Sub New(ByVal link As IShellLinkW, ByVal Path As String)
            Me.link = link
            Me.path = Path
        End Sub
        ''' <summary>Creates new instace of the <see cref="ShellLink"/> class</summary>
        ''' <param name="ExistingLink">Path of existing *.LNK file</param>
        ''' <exception cref="IO.FileNotFoundException">File <paramref name="ExistingLink"/> does not exist</exception>
        ''' <exception cref="TypeMismatchException">File <paramref name="ExistingLink"/> does not represent *.LNK file, it is another kind of link - such as URL.</exception>
        ''' <exception cref="ArgumentException">Link cannot be opened</exception>
        Public Sub New(ByVal ExistingLink As String)
            If Not IO.File.Exists(ExistingLink) Then Throw New IO.FileNotFoundException(String.Format("File {0} does not exist.", ExistingLink))
            'Dim objShell = New WshShell
            'Dim shCreated As Object
            'Try
            '    shCreated = objShell.CreateShortcut(ExistingLink)
            'Catch ex As COMException
            '    Throw New ArgumentException(String.Format("Cannot create link from file {0}.", ExistingLink), "ExistingLink", ex)
            'End Try
            'If Not TypeOf shCreated Is IWshShortcut Then Throw New TypeMismatchException("ExistingLink", shCreated, GetType(IWshShortcut), String.Format("File {0} does not represent *.LNK shortcut.", ExistingLink))
            'Dim objShortcut As IWshShortcut = shCreated
            'link = New ShellLink(objShortcut)
            Dim pf As IPersistFile
            Try
                link = CType(New COM.ShellLink.ShellLink(), IShellLinkW)
                path = ExistingLink
                pf = CType(link, IPersistFile)
                pf.Load(ExistingLink, 0)
            Catch ex As COMException
                Throw New ArgumentException(String.Format("Cannot create link from file {0}.", ExistingLink), "ExistingLink", ex)
            End Try
        End Sub
#Region "Properties"
        ''' <summary>The path to the shortcut's executable.</summary>
        ''' <remarks>This property is for the shortcut's target path only. Any arguments to the shortcut must be placed in the Argument's property.</remarks>
        ''' <value>The path to the shortcut's executable.</value>
        ''' <returns>The path to the shortcut's executable.</returns>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        Public Property TargetPath() As String
            Get
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                'Return link.TargetPath
                Dim wfd As New WIN32_FIND_DATAW
                Dim sb As StringBuilder = New StringBuilder(API.FileSystem.MAX_PATH)
                link.GetPath(sb, sb.Capacity, wfd, SLGP_FLAGS.SLGP_UNCPRIORITY)
                Return sb.ToString()
            End Get
            Set(ByVal value As String)
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                'link.TargetPath = value
                link.SetPath(value)
            End Set
        End Property
        ''' <summary>Assigns a window style to a shortcut, or identifies the type of window style used by a shortcut.</summary>
        ''' <value>Sets the window style for the program being run.</value>
        ''' <returns>The WindowStyle property returns an integer.</returns>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        ''' <exception cref="ArgumentException">Value being se is <see cref="ProcessWindowStyle.Hidden"/></exception>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="ProcessWindowStyle"/></exception>
        Public Property WindowStyle() As ProcessWindowStyle
            Get
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                'Return link.WindowStyle
                Dim nWS As Integer
                link.GetShowCmd(nWS)
                Select Case nWS
                    Case ShellLinkWindowStyle.SW_SHOWMINIMIZED, ShellLinkWindowStyle.SW_SHOWMINNOACTIVE
                        Return ProcessWindowStyle.Minimized
                    Case ShellLinkWindowStyle.SW_SHOWMAXIMIZED
                        Return ProcessWindowStyle.Maximized
                    Case Else
                        Return ProcessWindowStyle.Normal
                End Select
            End Get
            Set(ByVal value As ProcessWindowStyle)
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                'link.WindowStyle = value
                Dim nWS As ShellLinkWindowStyle
                Select Case value
                    Case ProcessWindowStyle.Normal
                        nWS = ShellLinkWindowStyle.SW_SHOWNORMAL
                    Case ProcessWindowStyle.Minimized
                        nWS = ShellLinkWindowStyle.SW_SHOWMINNOACTIVE
                    Case ProcessWindowStyle.Maximized
                        nWS = ShellLinkWindowStyle.SW_SHOWMAXIMIZED
                    Case ProcessWindowStyle.Hidden
                        Throw New ArgumentException("Unsupported ProcessWindowStyle value.")
                    Case Else
                        Throw New InvalidEnumArgumentException("value", value, value.GetType)
                End Select
                link.SetShowCmd(nWS)
            End Set
        End Property
        'ASAP:Comment
        Private Const INFOTIPSIZE As Integer = 1024
       
        ''' <summary>Link argumens</summary>
        ''' <remarks>Argumens</remarks>
        ''' <value>Arguments</value>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        Public Property Arguments() As String
            Get
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                'Return link.Arguments
                Dim sb As StringBuilder = New StringBuilder(INFOTIPSIZE)
                link.GetArguments(sb, sb.Capacity)
                Return sb.ToString()
            End Get
            Set(ByVal value As String)
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                'link.Arguments = value
                link.SetArguments(value)
            End Set
        End Property
        ''' <summary>Assigns a key-combination to a shortcut, or identifies the key-combination assigned to a shortcut.</summary>
        ''' <value>The syntax of hotkey is:<c>[KeyModifier]KeyName</c> where
        ''' <list type="table">
        ''' <item><term><c>KeyModifier</c></term><description>Can be any one of the following: ALT+, CTRL+, SHIFT+, EXT+.<para>EXT+ means "Extended key." — it appears here in case a new type of SHIFT-key is added to the character set in the future.</para></description></item>
        ''' <item><term>KeyName </term><description>a ... z, 0 ... 9, F1 F12, ... The KeyName is not case-sensitive.</description></item>
        ''' </list>
        ''' </value>
        ''' <remarks>In Windows 2000, valid Hotkeys always begin with CTRL + ALT.</remarks>
        ''' <returns>A string representing the key-combination to assign to the shortcut.</returns>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        ''' <exception cref="ArgumentException">Value being set is not valid shortcut</exception>
        Public Property Hotkey() As Keys
            Get
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                'Return link.Hotkey
                Dim wHotkey As Short
                Dim dwHotkey As Integer
                link.GetHotkey(wHotkey)
                ' Convert from IShellLink 16-bit format to Keys enumeration 32-bit value
                ' IShellLink: &HMMVK
                ' Keys:  &H00MM00VK        
                '   MM = Modifier (Alt, Control, Shift)
                '   VK = Virtual key code
                '   
                dwHotkey = (wHotkey And &HFF00I) * &H100I Or (wHotkey And &HFFI)
                Return CType(dwHotkey, Keys)
            End Get
            Set(ByVal value As Keys)
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                Dim wHotkey As Short
                If (value And Keys.Modifiers) = 0 Then
                    Throw New ArgumentException("Hotkey must include a modifier key.")
                End If
                '
                ' Convert from Keys enumeration 32-bit value to IShellLink 16-bit format
                ' IShellLink: &HMMVK
                ' Keys:  &H00MM00VK        
                '   MM = Modifier (Alt, Control, Shift)
                '   VK = Virtual key code
                '   
                wHotkey = CShort(CInt(value And Keys.Modifiers) \ &H100) Or CShort(value And Keys.KeyCode)
                link.SetHotkey(wHotkey)
            End Set
        End Property
        ''' <summary>Assigns an icon to a shortcut, or identifies the icon assigned to a shortcut.</summary>
        ''' <value>A string that locates the icon. The string should contain a fully qualified path and an index associated with the icon. Index is appendex after comma (,) and space.</value>
        ''' <returns>Identifies the icon assigned to a shortcut</returns>
        ''' <remarks>Example value: <c>notepad.exe, 0</c></remarks>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        Public Property IconPath() As String
            Get
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                'Return link.IconLocation
                Dim sb As StringBuilder = New StringBuilder(API.FileSystem.MAX_PATH)
                Dim nIconIdx As Integer
                link.GetIconLocation(sb, sb.Capacity, nIconIdx)
                Return sb.ToString()
            End Get
            Set(ByVal value As String)
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                'link.IconLocation = value
                link.SetIconLocation(value, IconIndex)
            End Set
        End Property
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        Public Property IconIndex() As Integer
            Get
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                Dim sb As StringBuilder = New StringBuilder(API.MAX_PATH)
                Dim nIconIdx As Integer
                link.GetIconLocation(sb, sb.Capacity, nIconIdx)
                Return nIconIdx
            End Get
            Set(ByVal Value As Integer)
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                link.SetIconLocation(IconPath, Value)
            End Set
        End Property
        ''' <summary>Shortcut description</summary>
        ''' <value>The Description property contains a string value describing a shortcut.</value>
        ''' <remarks>Returns a shortcut's description.</remarks>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        Public Property Description() As String
            Get
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                'Return link.Description
                Dim sb As StringBuilder = New StringBuilder(INFOTIPSIZE)
                link.GetDescription(sb, sb.Capacity)
                Return sb.ToString()
            End Get
            Set(ByVal value As String)
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                'link.Description = value
                link.SetDescription(value)
            End Set
        End Property
        '''' <summary>Assigns a relative path to a shortcut.</summary>
        '''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        'Public WriteOnly Property RelativePath$() Implements IWshShortcut.RelativePath
        '    Set(ByVal value$)
        '        If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
        '        link.RelativePath = value
        '    End Set
        'End Property
        ''' <summary>Assign a working directory to a shortcut, or identifies the working directory used by a shortcut.</summary>
        ''' <value>String. Directory in which the shortcut starts.</value>
        ''' <returns>String. Directory in which the shortcut starts.</returns>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        Public Property WorkingDirectory() As String
            Get
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                'Return link.WorkingDirectory
                Dim sb As StringBuilder = New StringBuilder(API.FileSystem.MAX_PATH)
                link.GetWorkingDirectory(sb, sb.Capacity)
                Return sb.ToString()
            End Get
            Set(ByVal value As String)
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                'link.WorkingDirectory = value
                link.SetWorkingDirectory(value)
            End Set
        End Property
        ''' <summary>Returns the fully qualified path of the shortcut object's target.</summary>
        ''' <returns>The FullName property contains a read-only string value indicating the fully qualified path to the shortcut's target.</returns>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        Public ReadOnly Property FullName() As String
            Get
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                Return path
            End Get
        End Property
#End Region
#Region "Methods"
        ''' <summary>Saves a shortcut object to disk.</summary>
        ''' <remarks>You must use this method to confir changes made to shortcut. The Save method uses the information in the shortcut object's FullName property to determine where to save the shortcut object on a disk. You can only create shortcuts to system objects. This includes files, directories, and drives (but does not include printer links or scheduled tasks).</remarks>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        Public Sub Save()
            If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
            'link.Save()
            Dim pf As IPersistFile = CType(link, IPersistFile)
            pf.Save(path, True)
        End Sub
        '''' <summary>Load shortcut from given path</summary>
        '''' <param name="PathLink">Path of *.lnk file</param>
        '''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        'Private Sub IWshShortcut_Load(ByVal PathLink As String) Implements IWshRuntimeLibrary.IWshShortcut.Load
        '    If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
        '    link.Load(PathLink)
        'End Sub
#End Region
#Region "Sahred"
        ''' <summary>Creates new *.LNK link</summary>
        ''' <param name="Target">Link target</param>
        ''' <param name="LinkLocation">Full path where the link will be stored (including the *.lnk extension)</param>
        ''' <param name="Arguments">Optional. Arguments for launching link target</param>
        ''' <returns>Instance of link that was created. The link is saved.</returns>
        ''' <remarks>Link extension must be *.lnk!</remarks>
        ''' <exception cref="ArgumentException">File or directory with path <paramref name="LinkLocation"/> already exists =or= Link cannot be created.</exception>
        Public Shared Function CreateLink(ByVal Target$, ByVal LinkLocation$, Optional ByVal Arguments$ = Nothing) As ShellLink
            If IO.File.Exists(LinkLocation) Then Throw New ArgumentException(String.Format("File {0} already exists.", LinkLocation), "LinkLocation")
            If IO.Directory.Exists(LinkLocation) Then Throw New ArgumentException(String.Format("There is already directory named {0}.", LinkLocation), "LinkLocation")
            'Dim objShell = New WshShell
            'Dim objShortcut As IWshShortcut
            Dim objShortcut As IShellLinkW
            'Try
            'objShortcut = objShell.CreateShortcut(LinkLocation)
            objShortcut = CType(New COM.ShellLink.ShellLink(), IShellLinkW)
            'Catch ex As COMException
            'Throw New ArgumentException(String.Format("Cannot create shortcut {0}. Ensure that you can write there and that shortcut extension is *.lnk.", LinkLocation), "LinkLocation", ex)
            'End Try
            Dim link = New ShellLink(objShortcut, LinkLocation)
            link.TargetPath = Target
            If Arguments <> "" Then _
                link.Arguments = Arguments
            'If IconFile <> "" Then _
            '    link.IconPath = IconFile
            link.Save()
            Return link
        End Function
#End Region
#Region " IDisposable Support "
        ''' <summary>To detect redundant calls</summary>
        Private disposedValue As Boolean = False
        ''' <summary>Gets value idicationg if object was disposed</summary>
        ''' <remarks>If object was disposed it is not valid to perform actions on it</remarks>
        Public ReadOnly Property Disposed() As Boolean
            Get
                Return disposedValue
            End Get
        End Property
        ''' <summary><see cref="IDisposable"/></summary>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    'Free other state (managed objects).
                End If
                If link Is Nothing Then Exit Sub
                Marshal.ReleaseComObject(link)
                link = Nothing
            End If
            Me.disposedValue = True
        End Sub



        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        ''' <remarks>This code added by Visual Basic to correctly implement the disposable pattern.</remarks>
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

End Namespace
#End If