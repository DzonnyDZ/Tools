Imports Tools.COM.ShellLink
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.ComponentModel

#If True
Namespace IOt
    ''' <summary>Represents a *.LNK file (also called shortcut or link)</summary>
    ''' <remarks>
    ''' Implementation based on Mattias Sjögren's (© 2001÷2002) example http://www.msjogren.net/dotnet/, mattias@mvps.org.
    ''' <para>This is COM-based implementation. For managed implemetation look at <a href="http://sourceforge.net/projects/shellify/">Shellify</a>.</para>
    ''' </remarks>
    Public Class ShellLink
        Implements IDisposable, IPathProvider
        ''' <summary>The <see cref="IShellLinkW"/> object this class is wrapper to</summary>
        Private link As IShellLinkW
        ''' <summary>Path of the *.LNK file this link is stored in</summary>
        Private path$
        ''' <summary>Creates new instance of <see cref="ShellLink"/> from <see cref="IShellLinkW"/></summary>
        ''' <param name="link">A <see cref="IShellLinkW"/> object</param>
        ''' <param name="Path">Path where the link will be saved</param>
        Private Sub New(ByVal link As IShellLinkW, ByVal Path As String)
            Me.link = link
            Me.path = Path
        End Sub
        ''' <summary>Gets COM object that represents the link</summary>
        ''' <returns>The object which implements <see cref="IPersistFile"/> and <see cref="IShellLinkW"/> interfaces</returns>
        Protected ReadOnly Property IShellLinkObject() As Object
            Get
                Return link
            End Get
        End Property
        ''' <summary>Creates new instance of the <see cref="ShellLink"/> class</summary>
        ''' <param name="existingLink">Path of existing *.LNK file</param>
        ''' <exception cref="IO.FileNotFoundException">File <paramref name="ExistingLink"/> does not exist</exception>
        ''' <exception cref="ArgumentException">Link cannot be opened</exception>
        ''' <version version="1.5.4">Parameter <c>ExistingLink</c> renamed co <c>existingLink</c></version>
        Public Sub New(ByVal existingLink As String)
            If Not IO.File.Exists(existingLink) Then Throw New IO.FileNotFoundException(String.Format(ResourcesT.Exceptions.File0DoesNotExist, existingLink))
            Dim pf As IPersistFile
            Try
                link = CType(New COM.ShellLink.ShellLink(), IShellLinkW)
                path = existingLink
                pf = CType(link, IPersistFile)
                pf.Load(existingLink, 0)
            Catch ex As COMException
                Throw New ArgumentException(String.Format(ResourcesT.ExceptionsWin.CannotCreateLinkFromFile0, existingLink), "ExistingLink", ex)
            End Try
        End Sub

#Region "Properties"
        ''' <summary>The path to the shortcut's executable.</summary>
        ''' <remarks>This property is for the shortcut's target path only. Any arguments to the shortcut must be placed in the <see cref="Arguments"/> property.</remarks>
        ''' <value>The path to the shortcut's executable.</value>
        ''' <returns>The path to the shortcut's executable.</returns>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        ''' <exception cref="ArgumentNullException">Value being set is nulll</exception>
        ''' <exception cref="ArgumentException">Value being set is an empty string.</exception>
        ''' <version version="1.5.4"><see cref="ArgumentNullException"/> and <see cref="ArgumentException"/> can be thrown.</version>
        Public Property TargetPath() As String
            Get
                Return GetTargetPath(SLGP_FLAGS.SLGP_UNCPRIORITY)
            End Get
            Set(ByVal value As String)
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                If value Is Nothing Then Throw New ArgumentNullException("value")
                If value = "" Then Throw New ArgumentException(ResourcesT.Exceptions.ValueCannotBeEmptyString, "value")
                'If IO.Path.IsPathRooted(value) Then
                link.SetPath(value)
                'Else
                'link.SetRelativePath(value, 0)
                'End If
            End Set
        End Property

        ''' <summary>Gets raw target path of the link</summary>
        ''' <remarks>Raw path may not exist or it may contain environmental variables that need to be resolved</remarks>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        ''' <version version="1.5.4">This property is new in version 1.5.4</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property RawTargetPath As String
            Get
                Return GetTargetPath(SLGP_FLAGS.SLGP_RAWPATH)
            End Get
        End Property

        ''' <summary>Gets target path of the link in 8.3 format</summary>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        ''' <version version="1.5.4">This property is new in version 1.5.4</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property ShortTargetPath As String
            Get
                Return GetTargetPath(SLGP_FLAGS.SLGP_SHORTPATH)
            End Get
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
                Dim nWS As ShellLinkWindowStyle
                Select Case value
                    Case ProcessWindowStyle.Normal
                        nWS = ShellLinkWindowStyle.SW_SHOWNORMAL
                    Case ProcessWindowStyle.Minimized
                        nWS = ShellLinkWindowStyle.SW_SHOWMINNOACTIVE
                    Case ProcessWindowStyle.Maximized
                        nWS = ShellLinkWindowStyle.SW_SHOWMAXIMIZED
                    Case ProcessWindowStyle.Hidden
                        Throw New ArgumentException(String.Format(ResourcesT.Exceptions.Unsupported0Value, "ProcessWindowStyle"))
                    Case Else
                        Throw New InvalidEnumArgumentException("value", value, value.GetType)
                End Select
                link.SetShowCmd(nWS)
            End Set
        End Property
        ''' <summary>Maximum length of certain COM strings</summary>
        Private Const INFOTIPSIZE As Integer = 1024

        ''' <summary>Link argumens</summary>
        ''' <returns>Arguments</returns>
        ''' <value>Arguments</value>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        ''' <version version="1.5.4">Docuimentation fix: Removed remarks. Returns now reads Arguments (previously in remarks).</version>
        Public Property Arguments() As String
            Get
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                Dim sb As StringBuilder = New StringBuilder(INFOTIPSIZE)
                link.GetArguments(sb, sb.Capacity)
                Return sb.ToString()
            End Get
            Set(ByVal value As String)
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                link.SetArguments(value)
            End Set
        End Property

        ''' <summary>Gets or sets the hotkey for the shortcut.</summary>
        Public Property Hotkey() As Keys
            Get
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
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
                    Throw New ArgumentException(ResourcesT.ExceptionsWin.HotkeyMustIncludeAModifierKey)
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
        ''' <value>A string that locates the icon. The string should contain a fully qualified path associated with the icon.</value>
        ''' <returns>Identifies the icon assigned to a shortcut</returns>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        ''' <seelaso cref="IconIndex"/>
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

        ''' <remarks>Gets or sets 0-based index of icon within file <see cref="IconPath"/></remarks>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        ''' <seelaso cref="IconPath"/>
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
                Dim sb As StringBuilder = New StringBuilder(INFOTIPSIZE)
                link.GetDescription(sb, sb.Capacity)
                Return sb.ToString()
            End Get
            Set(ByVal value As String)
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                link.SetDescription(value)
            End Set
        End Property

        ''' <summary>Assign a working directory to a shortcut, or identifies the working directory used by a shortcut.</summary>
        ''' <value>String. Directory in which the shortcut starts.</value>
        ''' <returns>String. Directory in which the shortcut starts.</returns>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        Public Property WorkingDirectory() As String
            Get
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                Dim sb As StringBuilder = New StringBuilder(API.FileSystem.MAX_PATH)
                link.GetWorkingDirectory(sb, sb.Capacity)
                Return sb.ToString()
            End Get
            Set(ByVal value As String)
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                link.SetWorkingDirectory(value)
            End Set
        End Property

        ''' <summary>Returns the fully qualified path of the shortcut object's target.</summary>
        ''' <returns>The <see cref="FullName"/> property contains a read-only string value indicating the fully qualified path to the shortcut's target.</returns>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        Public ReadOnly Property FullName() As String Implements IPathProvider.Path
            Get
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                Return path
            End Get
        End Property

        ''' <summary>Returns value indicating if link properties has been changed since it was loaded from file</summary>
        ''' <returns>
        ''' True if a property of this object has benn changed since it was loaded from a *.lnk file.
        ''' Always returns true if this instance has not been loaded from file (i.e. a new link is being created).
        ''' </returns>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        ''' <version version="1.5.4">This property is new in version 1.5.4</version>
        Public ReadOnly Property IsDirty As Boolean
            Get
                If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                Dim l = TryCast(link, IPersistFile)
                If l IsNot Nothing Then Return l.IsDirty
                Return True
            End Get
        End Property
#End Region

#Region "Methods"
        ''' <summary>Gets link path of specified ty</summary>
        ''' <param name="flags">Specifies which type of path to retrieve</param>
        ''' <returns>Target path of the link</returns>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        Private Function GetTargetPath(flags As SLGP_FLAGS) As String
            If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
            Dim wfd As New WIN32_FIND_DATAW
            Dim sb As StringBuilder = New StringBuilder(API.FileSystem.MAX_PATH)
            link.GetPath(sb, sb.Capacity, wfd, flags)
            Return sb.ToString()
        End Function

        ''' <summary>Saves a shortcut object to disk.</summary>
        ''' <remarks>You must use this method to confir changes made to shortcut. The Save method uses the information in the shortcut object's FullName property to determine where to save the shortcut object on a disk. You can only create shortcuts to system objects. This includes files, directories, and drives (but does not include printer links or scheduled tasks).</remarks>
        ''' <exception cref="ObjectDisposedException">The <see cref="Disposed"/> property is true</exception>
        ''' <exception cref="IO.FileNotFoundException">File <see cref="FullName"/> was not found</exception>
        ''' <exception cref="IO.IOException">Exception occurred while writing to file <see cref="FullName"/></exception>
        Public Sub Save()
            If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
            Dim pf As IPersistFile = CType(link, IPersistFile)
            Try
                pf.Save(path, True)
            Catch ex As IO.FileNotFoundException
                Throw New IO.FileNotFoundException(String.Format(ResourcesT.Exceptions.TheFile0CannotBeFound, path), path, ex)
            Catch ex As COMException
                Throw New IO.IOException(String.Format(ResourcesT.Exceptions.ThereWasAnErrorSavingFile0, path), ex)
            End Try
        End Sub

        ''' <summary>Saves a shortcut object to disk on different place then where it is saved now</summary>
        ''' <param name="Path">Path to be saved</param>
        ''' <remarks>Invoking this method changes value of the <see cref="FullName"/> property</remarks>
        ''' <exception cref="IO.FileNotFoundException">File <paramref name="Path"/> was not found</exception>
        ''' <exception cref="IO.IOException">Exception occurred while writing to file <paramref name="Path"/></exception>
        ''' <version version="1.5.4">Parameter <c>Path</c> renamed to <c>path</c></version>
        Public Sub SaveAs(ByVal path As String)
            If Disposed Then Throw New ObjectDisposedException(Me.GetType.Name)
            Dim pf As IPersistFile = CType(link, IPersistFile)
            Try
                pf.Save(Path, True)
            Catch ex As IO.FileNotFoundException
                Throw New IO.FileNotFoundException(String.Format(ResourcesT.Exceptions.TheFile0CannotBeFound, Path), Path, ex)
            Catch ex As COMException
                Throw New IO.IOException(String.Format(ResourcesT.Exceptions.ThereWasAnErrorSavingFile0, Path), ex)
            End Try
            Me.path = Path
        End Sub
        ''' <summary>Sets the relative path to the Shell link object.</summary>
        ''' <param name="relativePath">String contains the new relative path. It should be a file name, not a folder name.</param>
        ''' <remarks>
        ''' Clients commonly define a relative link when it may be moved along with its target, causing the absolute path to become invalid. The SetRelativePath method can be used to help the link resolution process find its target based on a common path prefix between the target and the relative path. To assist in the resolution process, clients should set the relative path as part of the link creation process.
        ''' </remarks>
        ''' <version version="1.5.4">Parameter name <c>RelativePath</c> changed to <c>relativePath</c></version>
        Public Sub SetRelativePath(ByVal relativePath$)
            link.SetRelativePath(relativePath, 0)
        End Sub

        ''' <summary>Specifies defauult flags to be added to flags of every <see cref="IShellLinkW.Resolve"/> call</summary>
        Private Const combineOptionsWith As SLR_FLAGS = SLR_FLAGS.SLR_UPDATE

        ''' <summary>Attempts to find the target of a Shell link, even if it has been moved or renamed.</summary>
        ''' <param name="options">Indicates option for link resolve</param>
        ''' <param name="owner">Parent window to show any dialogs in context of</param>
        ''' <remarks>
        ''' This methods attempts to resolve the link.
        ''' If <see cref="TargetPath"/> is already valid trhis method does nothing.
        ''' If <see cref="TargetPath"/> is not valid the method attempts to resolve the link according to <paramref name="options"/>.
        ''' If it succeeds it sets <see cref="IsDirty"/> to true.
        ''' <note>If <see cref="IsDirty"/> is true before this method is called it is never changed to false. This method is typically called on instances with <see cref="IsDirty"/> false (i.e. links just read from a file).</note>
        ''' <para>There several possible workflows with <see cref="Resolve"/>, e.g.:</para>
        ''' <list type="bullet">
        ''' <item>
        ''' Optimistic:
        ''' <list type="number">
        ''' <item>Check if <see cref="TargetPath"/> is valid. If it is use it.</item>
        ''' <item>If it is not call <see cref="Resolve"/>.</item>
        ''' <item>If <see cref="IsDirty"/> is true check if <see cref="TargetPath"/> is valid. If it is use it. If it is not error.</item>
        ''' </list>
        ''' </item>
        ''' <item>
        ''' Pesimistic:
        ''' <list type="number">
        ''' <item>Call <see cref="Resolve"/></item>
        ''' <item>Check if <see cref="TargetPath"/> is valid. If it is use it. If it is not error.</item>
        ''' </list>
        ''' </item>
        ''' <item>
        ''' Two-level:
        ''' <list type="number">
        ''' <item>
        ''' Call <see cref="Resolve"/> with <paramref name="options"/> containing <see cref="LinkResolveOptions.NoUi"/>.
        ''' <note>You can use overload which allows to specify timeout in this case.</note>
        ''' </item>
        ''' <item>Check if <see cref="TargetPath"/> is valid. If it is use it.</item>
        ''' <item>If it is not call <see cref="Resolve"/> without <paramref name="options"/> containing <see cref="LinkResolveOptions.NoUi"/>.</item>
        ''' <item>Check if <see cref="IsDirty"/> is true. If it is chekc if <see cref="TargetPath"/> is valid. If it is use it. If it is not error.</item>
        ''' </list>
        ''' </item>
        ''' </list>
        ''' <para>When <paramref name="options"/> contains <see cref="LinkResolveOptions.NoUi"/> default timeout of 3 seconds is used.</para>
        ''' <para>If this method changes <see cref="IsDirty"/> from false to true consider calling <see cref="Save"/> to save the changes method has done to the link.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This method is new in version 1.5.4</version>
        Public Sub Resolve(options As LinkResolveOptions, Optional owner As IWin32Window = Nothing)
            link.Resolve(If(owner Is Nothing, IntPtr.Zero, owner.Handle), options Or combineOptionsWith)
        End Sub

        ''' <summary>Attempts to find the target of a Shell link, even if it has been moved or renamed.</summary>
        ''' <param name="timeout">Timeout (in millliseconds) to try to resolve link for. If zero default timeout of 3 seconds (3000 milliseconds) is used.</param>
        ''' <param name="options">Indicates option for link resolve. <see cref="LinkResolveOptions.NoUi"/> is always added (OR-ed) to this value.</param>
        ''' <param name="owner">Parent window to show any dialogs in context of</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="timeout"/> is less than zero.</exception>
        ''' <remarks>
        ''' For details how this method works see overload <see cref="M:Tools.IOt.ShellLink.Resolve(Tools.IOt.LinkResolveOptions,System.Windows.Forms.IWin32Window)"/>.
        ''' <para>If this method changes <see cref="IsDirty"/> from false to true consider calling <see cref="Save"/> to save the changes method has done to the link.</para>
        ''' </remarks>
        ''' <version version="1.5.4">This method is new in version 1.5.4</version>
        Public Sub Resolve(timeout As Short, Optional options As LinkResolveOptions = LinkResolveOptions.NoUi, Optional owner As IWin32Window = Nothing)
            If timeout < 0 Then Throw New ArgumentOutOfRangeException("timeout", "Value must be greater than or equal to zero")
            link.Resolve(If(owner Is Nothing, IntPtr.Zero, owner.Handle), options Or LinkResolveOptions.NoUi Or (CInt(timeout) << 16) Or combineOptionsWith)
        End Sub
#End Region

#Region "Sahred"
        ''' <summary>Creates new *.LNK link</summary>
        ''' <param name="Target">Link target</param>
        ''' <param name="LinkLocation">Full path where the link will be stored (including the *.lnk extension)</param>
        ''' <param name="Arguments">Optional. Arguments for launching link target</param>
        ''' <returns>Instance of link that was created. The link is saved.</returns>
        ''' <remarks>Link extension must be *.lnk!</remarks>
        ''' <exception cref="ArgumentException">
        ''' File or directory with path <paramref name="LinkLocation"/> already exists
        ''' -or-
        ''' Link cannot be created.
        ''' -or-
        ''' <paramref name="target"/> or <paramref name="linkLocation"/> is an empty string.
        ''' </exception>
        ''' <exception cref="ArgumentNullException"><paramref name="target"/> or <paramref name="linkLocation"/> is null</exception>
        ''' <version version="1.5.4">Parameter names changed to camelCase</version>
        ''' <version version="1.5.4"><see cref="ArgumentNullException"/> can be thrown.</version>
        ''' <version stage="1.5.4"><see cref="ArgumentException"/> is also thrown when <paramref name="target"/> or <paramref name="linkLocation"/> is an empty string.</version>
        Public Shared Function CreateLink(ByVal target$, ByVal linkLocation$, Optional ByVal arguments$ = Nothing) As ShellLink
            If linkLocation Is Nothing Then Throw New ArgumentNullException("linkLocation")
            If linkLocation = "" Then Throw New ArgumentException(ResourcesT.Exceptions.ValueCannotBeEmptyString, "linkLocation")
            If IO.File.Exists(linkLocation) Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.File0AlreadyExists, linkLocation), "linkLocation")
            If IO.Directory.Exists(linkLocation) Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.ThereIsAlreadyDirectoryNamed0, linkLocation), "linkLocation")
            Dim objShortcut As IShellLinkW
            objShortcut = CType(New COM.ShellLink.ShellLink(), IShellLinkW)
            Dim link = New ShellLink(objShortcut, linkLocation)
            link.TargetPath = target
            If arguments <> "" Then _
                link.Arguments = arguments
            link.Save()
            Return link
        End Function
        ''' <summary>Resolves link target</summary>
        ''' <param name="link">Address of *.LNK file</param>
        ''' <returns>Address of link target. Returns null when link cannot be resolved (i.e. <paramref name="link"/> is not valid *.LNK file)</returns>
        ''' <remarks>This method does not issue any dialog to user. This method does not attempt to resolve invalid links. To resolve invalid link use <see cref="Resolve"/>.</remarks>
        ''' <exception cref="IO.FileNotFoundException">File <paramref name="link"/> does not exist</exception>
        Public Shared Function ResolveLink(ByVal link As String) As String
            Try
                Dim l As New ShellLink(link)
                Return l.TargetPath
            Catch ex As IO.FileNotFoundException
                Throw
            Catch ex As ArgumentException
                Return Nothing
            End Try
        End Function
        ''' <summary>Resolves link target</summary>
        ''' <param name="link">Address of *.LNK file</param>
        ''' <returns>Address of link target. Returns null when link cannot be resolved (i.e. <paramref name="link"/> is not valid *.LNK file)</returns>
        ''' <remarks>This method does not issue any dialog to user. This method does not attempt to resolve invalid links. To resolve invalid link use <see cref="Resolve"/>.</remarks>
        ''' <exception cref="IO.FileNotFoundException">File <paramref name="link"/> does not exist</exception>
        Public Shared Function ResolveLink(ByVal link As Path) As Path
            Dim ret = ResolveLink(link.Path)
            If ret = "" Then Return Nothing Else Return ret
        End Function

#End Region

#Region " IDisposable Support "
        ''' <summary>To detect redundant calls</summary>
        Private disposedValue As Boolean = False
        ''' <summary>Gets value idicationg if object was disposed</summary>
        ''' <remarks>If object was disposed it is not valid to perform actions on it</remarks>
        <Browsable(False)> _
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

    ''' <summary>This enumeration indicates options used by <see cref="ShellLink.Resolve"/> method to indicate how to resolve *.lnk target</summary>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    <Flags>
    Public Enum LinkResolveOptions As Short
        ''' <summary>No special options are set</summary>
        none = 0
        ''' <summary>Do not display a dialog box if the link cannot be resolved.</summary>
        NoUi = SLR_FLAGS.SLR_NO_UI
        ''' <summary>Do not execute the search heuristics.</summary>
        NoSearch = SLR_FLAGS.SLR_NOSEARCH
        ''' <summary>Do not use distributed link tracking.</summary>
        NoDistributedTracking = SLR_FLAGS.SLR_NOTRACK
        ''' <summary>Do not use distributed link tracking and do not track links to UNC paths whose drive letter has changed.</summary>
        NoTrackingAtAll = SLR_FLAGS.SLR_NOLINKINFO
        ''' <summary>Call the Windows Installer.</summary>
        InvokeMsi = SLR_FLAGS.SLR_INVOKE_MSI
        ''' <summary>(Only Windows 7 and later) Offer the option to delete the shortcut when this method is unable to resolve it, even if the shortcut is not a shortcut to a file.</summary>
        OfferDelete = SLR_FLAGS.SLR_OFFER_DELETE_WITHOUT_FILE
        ''' <summary>(Only Windows 7 and later) Set <see cref="ShellLink.IsDirty"/> to true if link target is a known folder and the known folder was redirected.</summary>
        ''' <remarks>This only works if the original target path was a file system path or ID list and not an aliased known folder ID list.</remarks>
        KnownFolder = SLR_FLAGS.SLR_KNOWNFOLDER
    End Enum

End Namespace
#End If
