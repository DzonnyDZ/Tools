Imports Tools.ExtensionsT, Tools.DrawingT
Imports Tools.IOt, System.Xml.Linq
Imports <xmlns:ws="http://dzonny.cz/xml/schemas/WfxSampleSettings.xsd">

''' <summary>Sample Total Commander file system plugin (just works over local file system)</summary>
<TotalCommanderPlugin("wfxSample")> _
<ResourcePluginIcon(GetType(SampleFileSystemPlugin), "Tools.TotalCommanderT.WfxSample.VisualBasic.ico")> _
Public Class SampleFileSystemPlugin
    Inherits FileSystemPlugin
    ''' <summary>Represents file system favorite item</summary>
    Private Class FavoriteItem
        ''' <summary>Contains value of the <see cref="Name"/> property</summary>
        Private _Name As String
        ''' <summary>Contains value of the <see cref="Target"/> property</summary>
        Private _Target As String
        ''' <summary>Gets or sets name of item</summary>
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property
        ''' <summary>Gets or sets target path of item</summary>
        Public Property Target() As String
            Get
                Return _Target
            End Get
            Set(ByVal value As String)
                _Target = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="CreateTime"/> property</summary>
        Private _CreateTime As Date
        ''' <summary>Date and time when item was created</summary>
        Public Property CreateTime() As Date
            Get
                Return _CreateTime
            End Get
            Set(ByVal value As Date)
                _CreateTime = value
            End Set
        End Property
        ''' <summary>CTor</summary>
        ''' <param name="Name">Item name</param>
        ''' <param name="Target">Item target path</param>
        ''' <param name="Created">Item creation date and time</param>
        Public Sub New(ByVal Name$, ByVal Target$, ByVal Created As Date)
            Me.Name = Name
            Me.Target = Target
            Me.CreateTime = Created
        End Sub
    End Class
    ''' <summary>Favorite items</summary>
    Private FavoriteItems As New List(Of FavoriteItem)

    ''' <summary>Retrieves the first file in a directory of the plugin's file system.</summary>
    ''' <param name="Path">Full path to the directory for which the directory listing has to be retrieved. Important: no wildcards are passed to the plugin! All separators will be backslashes, so you will need to convert them to forward slashes if your file system uses them!
    ''' <para>As root, a single backslash is passed to the plugin. The root items appear in the plugin base directory retrieved by <see cref="Name"/> at installation time. This default root name is NOT part of the path passed to the plugin!</para>
    ''' <para>All subdirs are built from the directory names the plugin returns through <see cref="FindFirst"/> and <see cref="FindNext"/>, separated by single backslashes, e.g. \Some server\c:\subdir</para></param>
    ''' <param name="FindData">A <see cref="FindData"/> struct (mimics WIN32_FIND_DATA as defined in the Windows SDK) to be pupulated with the file or directory details. Use the <see cref="FindData.Attributes"/> field set to <see cref="Tools.TotalCommanderT.FileAttributes.Directory"/> to distinguish files from directories. On Unix systems, you can | (or) the <see cref="FindData.Attributes"/> field with 0x80000000 and set the <see cref="FindData.ReparsePointTag"/> parameter to the Unix file mode (permissions).</param>
    ''' <returns>Any object. It is recommended to return object that represents current state of the search. This will allow recursive directory searches needed for copying whole trees. This object will be passed to <see cref="FindNext"/> by the calling program.
    ''' Returned object is added to <see cref="HandleDictionary"/>
    ''' <para>Null if there are no more files.</para></returns>
    ''' <exception cref="IO.DirectoryNotFoundException">Directory does not exists</exception>
    ''' <exception cref="UnauthorizedAccessException">The user does not have access to the directory</exception>
    ''' <exception cref="Security.SecurityException">Security error detected</exception>
    ''' <exception cref="IO.IOException">Another error occured</exception>
    ''' <remarks><see cref="FindFirst"/> may be called directly with a subdirectory of the plugin! You cannot rely on it being called with the root \ after it is loaded. Reason: Users may have saved a subdirectory to the plugin in the Ctrl+D directory hotlist in a previous session with the plugin.
    ''' <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
    Public Overrides Function FindFirst(ByVal Path As String, ByRef FindData As FindData) As Object
        Dim RealPath = GetRealPath(Path)
        If Path = "\" Then
            Dim ContentEnumerator = (From drv In IO.DriveInfo.GetDrives Select CObj(drv)) _
                .Union(New Object() {My.Resources.AddNewFavoriteItem}) _
                .Union(From fi In FavoriteItems Select CObj(fi)) _
            .GetEnumerator()
            If ContentEnumerator.MoveNext Then
                FindData = GetObjItemInfo(ContentEnumerator.Current)
                Return ContentEnumerator
            Else
                Return Nothing
            End If
        ElseIf RealPath.StartsWith("\\") AndAlso RealPath.IndexOf("\", 2) < 0 Then
            Dim NetFolders As String()
            Try
                NetFolders = IOt.SharedFolders.GetSharedFolders(RealPath.Substring(2))
            Catch ex As API.Win32APIException
                Throw New IO.IOException(ex.Message, ex)
            End Try
            Dim en = (From itm In NetFolders Select RealPath & "\" & itm).GetEnumerator
            If en.MoveNext Then
                FindData = GetFindData(en.Current)
                Return en
            Else
                Return Nothing
            End If
        Else
            Dim ContentEnumerator As IEnumerator(Of String)
            Try
                ContentEnumerator = (From itm In IO.Directory.GetFileSystemEntries(RealPath)).GetEnumerator
            Catch ex As Exception When TypeOf ex Is IO.IOException OrElse TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is Security.SecurityException
                Throw
            Catch ex As Exception
                Throw New IO.IOException(ex.Message, ex)
            End Try
            If ContentEnumerator.MoveNext Then
                FindData = GetFindData(ContentEnumerator.Current)
                Return ContentEnumerator
            Else
                Return Nothing
            End If
        End If
    End Function
    ''' <summary>Gets <see cref="FindData"/> for top-level item</summary>
    ''' <param name="objItem">Top-level item - either <see cref="IO.DriveInfo"/>, <see cref="String"/> or <see cref="FavoriteItem"/></param>
    Private Function GetObjItemInfo(ByVal objItem As Object) As FindData
        If TypeOf objItem Is IO.DriveInfo Then
            Return GetFindData(DirectCast(objItem, IO.DriveInfo).Name)
        ElseIf TypeOf objItem Is String Then
            Dim ret As New FindData
            ret.Attributes = FileAttributes.ReadOnly Or FileAttributes.Virtual
            ret.FileName = My.Resources.AddNewFavoriteItem
            Return ret
        ElseIf TypeOf objItem Is FavoriteItem Then
            Dim ret As New FindData
            Dim Item As FavoriteItem = objItem
            If Item.Target.StartsWith("\\") AndAlso Item.Target.IndexOf("\"c, 2) < 0 Then
                ret.Attributes = FileAttributes.Normal Or FileAttributes.Virtual
                ret.FileName = Item.Name
                ret.CreationTime = Item.CreateTime
            ElseIf IO.File.Exists(Item.Target) OrElse IO.Directory.Exists(Item.Target) Then
                ret.Attributes = FileAttributes.Normal Or FileAttributes.Virtual
                ret.FileName = Item.Name
                ret.CreationTime = Item.CreateTime
                If IO.File.Exists(Item.Target) Then ret.FileSize = New IO.FileInfo(Item.Target).Length
            Else
                ret.Attributes = FileAttributes.Normal Or FileAttributes.Virtual
                ret.FileName = Item.Name
            End If
            Return ret
        Else 'Never happens
            Throw New ApplicationException
        End If
    End Function
    ''' <summary>Gets real local path from path reported by Total Commander</summary>
    ''' <param name="Path">UNC path corresponding to geiven Total Commander plugin rooted (\-starting) path</param>
    Private Function GetRealPath(ByVal Path$) As String
        If Path = "\" Then Return ""
        If Path = "" Then Return ""
        If IsFavoriteAdd(Path) Then Return Path
        If IsFavorite(Path) Then
            Dim fi = GetFavoriteItem(Path.Substring(1))
            If fi IsNot Nothing Then Return fi.Target
            Return Path
        End If
        Dim ret = Path.Substring(1)
        If ret.Length = 2 AndAlso ret.EndsWith(":") Then ret &= "\"
        Return ret
    End Function
    ''' <summary>Gets value indicating if path of an item is add favorite</summary>
    ''' <param name="Path">Path to examine (in TC plugin format - with leading \)</param>
    ''' <returns>True if path represents add favorite item; false othervise</returns>
    Private Function IsFavoriteAdd(ByVal Path$) As Boolean
        Return Path = "\" & My.Resources.AddNewFavoriteItem
    End Function
    ''' <summary>Gets value indicating if path of an item is path of favorite item</summary>
    ''' <param name="Path">Path to examine (in TC plugin format - with leading \)</param>
    ''' <returns>True if path represents path of favorite item; false othervise</returns>
    Private Function IsFavorite(ByVal Path As String) As Boolean
        Return Path.StartsWith("\"c) AndAlso Path.IndexOf("\"c, 1) < 0 AndAlso (Path.Length < 3 OrElse Path(2) <> ":"c)
    End Function
    ''' <summary>Gets <see cref="FindData"/> for given file or directory</summary>
    ''' <param name="Path">Path to get data for</param>
    ''' <returns><see cref="FindData"/> filled by information relevant for <paramref name="Path"/></returns>
    Private Function GetFindData(ByVal Path As String) As FindData
        Dim ret As New FindData
        Dim oinfo As IO.FileSystemInfo
        If IO.Directory.Exists(Path) Then
            Dim info As New IO.DirectoryInfo(Path)
            If Path.Length = 3 AndAlso Path.EndsWith(":\") Then
                Dim dinfo As New IO.DriveInfo(Path(0))
                ret.FileSize = dinfo.TotalSize
                ret.FileName = Path.Substring(0, 2)
            End If
            oinfo = info
        ElseIf IO.File.Exists(Path) Then
            Dim info = New IO.FileInfo(Path)
            ret.FileSize = info.Length
            oinfo = info
        ElseIf Path.Length = 3 AndAlso Path.EndsWith(":\") Then 'I.E. empty CD-ROM drive
            Dim info As New IO.DriveInfo(Path(0))
            ret.FileName = Path.Substring(0, 2)
            ret.Attributes = FileAttributes.Directory Or FileAttributes.Normal
            Return ret
        ElseIf Path.StartsWith("\\") AndAlso Path.Count(Function(a) a = "\"c) = 3 Then
            ret.FileName = IO.Path.GetFileName(Path)
            ret.Attributes = FileAttributes.Directory Or FileAttributes.Normal
            Return ret
        Else
            Throw New IO.FileNotFoundException("File not found", Path)
        End If
        ret.CreationTime = oinfo.CreationTime
        ret.WriteTime = oinfo.LastWriteTime
        ret.AccessTime = oinfo.LastAccessTime
        ret.Attributes = oinfo.Attributes
        If Path.Length = 3 AndAlso Path.EndsWith(":\") Then
        Else
            ret.FileName = IO.Path.GetFileName(Path)
        End If
        Return ret
    End Function
    ''' <summary>Retrieves the next file in a directory of the plugin's file system</summary>
    ''' <param name="Status">The object returned by <see cref="FindFirst"/>; null when Total Commander supplied handle that is  not in <see cref="HandleDictionary"/></param>
    ''' <param name="FindData">A <see cref="FindData"/> struct (mimics WIN32_FIND_DATA as defined in the Windows SDK) to be pupulated with the file or directory details. Use the <see cref="FindData.Attributes"/> field set to <see cref="Tools.TotalCommanderT.FileAttributes.Directory"/> to distinguish files from directories. On Unix systems, you can | (or) the <see cref="FindData.Attributes"/> field with 0x80000000 and set the <see cref="FindData.ReparsePointTag"/> parameter to the Unix file mode (permissions).</param>
    ''' <returns>Return false if there are no more files, and true otherwise. SetLastError() does not need to be called.</returns>
    ''' <exception cref="IO.DirectoryNotFoundException">Directory does not exists</exception>
    ''' <exception cref="UnauthorizedAccessException">The user does not have access to the directory</exception>
    ''' <exception cref="Security.SecurityException">Security error detected</exception>
    ''' <exception cref="IO.IOException">Another error occured</exception>
    ''' <remarks><note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
    Public Overrides Function FindNext(ByVal Status As Object, ByRef FindData As FindData) As Boolean
        If TypeOf Status Is IEnumerator(Of Object) Then
            Dim en As IEnumerator(Of Object) = Status
            If en.MoveNext Then
                FindData = GetObjItemInfo(en.Current)
                Return True
            Else
                Return False
            End If
        ElseIf TypeOf Status Is IEnumerable(Of String) Then
            Dim en As IEnumerator(Of String) = Status
            If en.MoveNext Then
                FindData = GetFindData(en.Current)
                Return True
            Else
                Return False
            End If
        Else
            Throw New InvalidOperationException 'Should be never thrown cause nothing else should be passed here
        End If
    End Function
    ''' <summary>Performs custom clenup at end of a <see cref="FindFirst"/>/<see cref="FindNext"/> loop, either after retrieving all files, or when the user aborts it.</summary>
    ''' <param name="Status">The object returned by <see cref="FindFirst"/>; null when Total Commander supplied handle that is  not in <see cref="HandleDictionary"/>. When this function exists, <paramref name="Status"/> automatically removed from <see cref="HandleDictionary"/></param>
    Public Overrides Sub FindClose(ByVal Status As Object)
        MyBase.FindClose(Status)
        Dim disp = TryCast(Status, IDisposable)
        If disp IsNot Nothing Then disp.Dispose()
    End Sub
    ''' <summary>Gets name of the plugin</summary>
    Public Overrides ReadOnly Property Name() As String
        Get
            Return "WFX Sample"
        End Get
    End Property
    ''' <summary>Deletes a file from the plugin's file system</summary>
    ''' <param name="RemoteName">Name of the file to be deleted, with full path. The name always starts with a backslash, then the names returned by <see cref="FindFirst"/>/<see cref="FindNext"/> separated by backslashes</param>
    ''' <returns>Return true if the file could be deleted, false if not.</returns>
    ''' <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
    ''' <exception cref="Security.SecurityException">Security error detected</exception>
    ''' <exception cref="IO.IOException">An IO error occured</exception>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
    ''' <remarks>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.
    ''' <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
    Public Overrides Function DeleteFile(ByVal RemoteName As String) As Boolean
        If IsFavoriteAdd(RemoteName) Then Return False
        If IsFavorite(RemoteName) Then
            Dim fi = GetFavoriteItem(RemoteName.Substring(1))
            If fi IsNot Nothing Then FavoriteItems.Remove(fi) : SaveSettings()
            Return True
        End If
        Try
            IO.File.Delete(GetRealPath(RemoteName))
            Return True
        Catch ex As Exception When TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is Security.SecurityException OrElse TypeOf ex Is IO.IOException
            Throw
        Catch ex As Exception
            Return False
        End Try
    End Function
    ''' <summary>Removes a directory from the plugin's file system.</summary>
    ''' <param name="RemoteName">Name of the directory to be removed, with full path. The name always starts with a backslash, then the names returned by <see cref="FindFirst"/>/<see cref="FindNext"/> separated by backslashes.</param>
    ''' <returns>Return true if the directory could be removed, false if not.</returns>
    ''' <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
    ''' <exception cref="Security.SecurityException">Security error detected</exception>
    ''' <exception cref="IO.IOException">An IO error occured</exception>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
    ''' <remarks>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.
    ''' <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
    Public Overrides Function RemoveDir(ByVal RemoteName As String) As Boolean
        If IsFavoriteAdd(RemoteName) OrElse IsFavorite(RemoteName) Then Return False
        Try
            IO.Directory.Delete(GetRealPath(RemoteName), True)
        Catch ex As Exception When TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is Security.SecurityException OrElse TypeOf ex Is IO.IOException
            Throw
        Catch
            Return False
        End Try
        Return True
    End Function
    ''' <summary>Creates a directory on the plugin's file system.</summary>
    ''' <param name="Path">Name of the directory to be created, with full path. The name always starts with a backslash, then the names returned by <see cref="FindFirst"/>/<see cref="FindNext"/> separated by backslashes.</param>
    ''' <returns>Return true if the directory could be created, false if not.</returns>
    ''' <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
    ''' <exception cref="Security.SecurityException">Security error detected</exception>
    ''' <exception cref="IO.IOException">An IO error occured</exception>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
    ''' <remarks>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.
    ''' <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
    Public Overrides Function MkDir(ByVal Path As String) As Boolean
        If Path.IndexOf("\", 1) < 0 Then Return False
        Try
            IO.Directory.CreateDirectory(GetRealPath(Path))
        Catch ex As Exception When TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is Security.SecurityException OrElse TypeOf ex Is IO.IOException
            Throw
        Catch
            Return False
        End Try
        Return True
    End Function


#Region "Exec"
    ''' <summary>When overiden in derived class called to execute a file on the plugin's file system, or show its property sheet. It is also called to show a plugin configuration dialog when the user right clicks on the plugin root and chooses 'properties'. The plugin is then called with <paramref name="RemoteName"/>="\" and <paramref name="Verb"/>="properties" (requires TC>=5.51).</summary>
    ''' <param name="hMainWin">Handle to parent window which can be used for showing a property sheet.</param>
    ''' <param name="RemoteName">Name of the file to be executed, with full path. Do not assign string longer than <see cref="FindData.MaxPath"/>-1 or uncatchable <see cref="IO.PathTooLongException"/> will be thrown.</param>
    ''' <param name="Verb">This can be either "<c>open</c>", "<c>properties</c>", "<c>chmod</c>" or "<c>quote</c>" commandline (case-insensitive).</param>
    ''' <returns>Return <see cref="Tools.TotalCommanderT.ExecExitCode.Yourself"/> if Total Commander should download the file and execute it locally, <see cref="Tools.TotalCommanderT.ExecExitCode.OK"/> if the command was executed successfully in the plugin (or if the command isn't applicable and no further action is needed), <see cref="Tools.TotalCommanderT.ExecExitCode.Error"/> if execution failed, or <see cref="Tools.TotalCommanderT.ExecExitCode.Symlink"/> if this was a (symbolic) link or .lnk file pointing to a different directory.</returns>
    ''' <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
    ''' <exception cref="Security.SecurityException">Security error detected</exception>
    ''' <exception cref="IO.IOException">An IO error occured</exception>
    ''' <exception cref="InvalidOperationException">Excution cannot be done from other reason</exception>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
    ''' <remarks>Meaning of verbs:
    ''' <list type="table"><listheader><term>verb</term><description>meaning</description></listheader>
    ''' <item><term>open</term><description>This is called when the user presses ENTER on a file. There are three ways to handle it:
    ''' <list type="bulet">
    ''' <item>For internal commands like "Add new connection", execute it in the plugin and return <see cref="Tools.TotalCommanderT.ExecExitCode.OK"/> or <see cref="Tools.TotalCommanderT.ExecExitCode.Error"/></item>
    ''' <item>Let Total Commander download the file and execute it locally: return <see cref="Tools.TotalCommanderT.ExecExitCode.Yourself"/></item>
    ''' <item>If the file is a (symbolic) link, set <paramref name="RemoteName"/> to the location to which the link points (including the full plugin path), and return <see cref="Tools.TotalCommanderT.ExecExitCode.Symlink"/>. Total Commander will then switch to that directory. You can also switch to a directory on the local harddisk! To do this, return a path starting either with a drive letter, or an UNC location (\\server\share). The maximum allowed length of such a path is <see cref="FindData.MaxPath"/>-1 (= 259) characters!</item>
    ''' </list></description></item>
    ''' <item><term>properties</term><description>Show a property sheet for the file (optional). Currently not handled by internal Totalcmd functions if <see cref="Tools.TotalCommanderT.ExecExitCode.Yourself"/> is returned, so the plugin needs to do it internally.</description></item>
    ''' <item><term>chmod xxx</term><description>The xxx stands for the new Unix mode (attributes) to be applied to the file <paramref name="RemoteName"/>. This verb is only used when returning Unix attributes through <see cref="FindFirst"/>/<see cref="FindNext"/></description></item>
    ''' <item><term>quote commandline</term><description>Execute the command line entered by the user in the directory <paramref name="RemoteName"/> . This is called when the user enters a command in Totalcmd's command line, and presses ENTER. This is optional, and allows to send plugin-specific commands. It's up to the plugin writer what to support here. If the user entered e.g. a cd directory command, you can return the new path in <paramref name="RemoteName"/> (max 259 characters), and give <see cref="Tools.TotalCommanderT.ExecExitCode.Symlink"/> as return value. Return <see cref="Tools.TotalCommanderT.ExecExitCode.OK"/> to cause a refresh (re-read) of the active panel.</description></item>
    ''' </list>
    ''' <para>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.</para>
    ''' <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
    Public Overrides Function ExecuteFile(ByVal hMainWin As System.IntPtr, ByRef RemoteName As String, ByVal Verb As String) As ExecExitCode
        Return MyBase.ExecuteFile(hMainWin, RemoteName, Verb)  'This enables other functions in this #Region
    End Function
    ''' <summary>Opens or executes given file.</summary>
    ''' <param name="hMainWin">Handle to Total Commander window.</param>
    ''' <param name="RemoteName">Full path of file to be opened or executed. In case the file is link (like *.lnk files in Windows) method should assignt link target path to this argument and return <see cref="Tools.TotalCommanderT.ExecExitCode.Symlink"/>. It will make Total Commander navigate to a new path.
    ''' <para>Do not assign string longer than <see cref="FindData.MaxPath"/>-1 or uncatchable <see cref="IO.PathTooLongException"/> will be thrown.</para></param>
    ''' <returns>Return <see cref="Tools.TotalCommanderT.ExecExitCode.Yourself"/> if Total Commander should download the file and execute it locally, <see cref="Tools.TotalCommanderT.ExecExitCode.OK"/> if the command was executed successfully in the plugin (or if the command isn't applicable and no further action is needed), <see cref="Tools.TotalCommanderT.ExecExitCode.Error"/> if execution failed, or <see cref="Tools.TotalCommanderT.ExecExitCode.Symlink"/> if this was a (symbolic) link or .lnk file pointing to a different directory.</returns>
    ''' <remarks><note type="inheritinfo">This method is called only when plugin implements <see cref="ExecuteFile"/> function and thah function calls base class method.</note></remarks>
    ''' <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
    ''' <exception cref="Security.SecurityException">Security error detected</exception>
    ''' <exception cref="IO.IOException">An IO error occured</exception>
    ''' <exception cref="InvalidOperationException">Excution cannot be done from other reason</exception>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method and all most derived implementations of following methods are marked with <see cref="MethodNotSupportedAttribute"/> as well: <see cref="FtpModeAdvertisement"/>, <see cref="OpenFile"/>, <see cref="ShowFileInfo"/>, <see cref="ExecuteCommand"/></exception>
    Protected Overrides Function OpenFile(ByVal hMainWin As System.IntPtr, ByRef RemoteName As String) As ExecExitCode
        Dim Path = GetRealPath(RemoteName)
        If IsFavoriteAdd(RemoteName) Then
            Dim dlg As New AddFavoriteItemDialog
            If dlg.ShowDialog(New WindowsT.NativeT.Win32Window(hMainWin)) = Windows.Forms.DialogResult.OK Then
                Dim fi = GetFavoriteItem(dlg.Name)
                If fi IsNot Nothing OrElse dlg.Name = My.Resources.AddNewFavoriteItem Then
                    WindowsT.IndependentT.MessageBox.Modal_PTWBIO(My.Resources.ItemAlreadyExists, My.Resources.Error_, New WindowsT.NativeT.Win32Window(hMainWin), , Tools.WindowsT.IndependentT.MessageBox.GetIcon(WindowsT.IndependentT.MessageBox.MessageBoxIcons.Error))
                    Return ExecExitCode.OK
                End If
                FavoriteItems.Add(New FavoriteItem(dlg.ItemName, dlg.Target, Now))
                SaveSettings()
            End If
            Return ExecExitCode.OK
        ElseIf IsFavorite(RemoteName) Then
            Dim fi = GetFavoriteItem(RemoteName.Substring(1))
            If fi IsNot Nothing Then
                If (fi.Target.StartsWith("\\") AndAlso fi.Target.IndexOf("\"c, 2) < 0) Then
                    RemoteName = "\" & fi.Target
                    Return ExecExitCode.Symlink
                ElseIf IO.File.Exists(fi.Target) Then
                    Path = fi.Target
                    GoTo Normal
                ElseIf IO.Directory.Exists(fi.Target) Then
                    RemoteName = "\" & fi.Target
                    Return ExecExitCode.Symlink
                Else
                    Return ExecExitCode.Error
                End If
            End If
                Return ExecExitCode.Error
            End If
Normal:     If IO.Path.GetExtension(Path).ToLower = ".lnk" Then 'Follow link
                Try
                    Dim lnk As IOt.ShellLink
                    Try : lnk = New IOt.ShellLink(Path)
                    Catch : GoTo ExecFile : End Try
                    Dim newPath = lnk.TargetPath
                    If Not IO.Directory.Exists(newPath) Then GoTo ExecFile
                    RemoteName = "\" & newPath
                    Return ExecExitCode.Symlink
                Catch ex As Exception When TypeOf ex Is Security.SecurityException OrElse TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is IO.IOException OrElse TypeOf ex Is InvalidOperationException
                    Throw
                Catch
                    Return ExecExitCode.Error
                End Try
            Else 'Open/execute file
ExecFile:       Dim p As New Process
                p.StartInfo.FileName = Path
                p.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(Path)
                Try
                    p.Start()
                Catch ex As Exception When TypeOf ex Is Security.SecurityException OrElse TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is IO.IOException OrElse TypeOf ex Is InvalidOperationException
                    Throw
                Catch
                    Return ExecExitCode.Error
                End Try
                Return ExecExitCode.OK
            End If
    End Function
    ''' <summary>Executes command in plugin space</summary>
    ''' <param name="hMainWin">Handle to Total Commander window.</param>
    ''' <param name="RemoteName">Full path of currently show directory in Total Commander. Includes trailing \. If command changes current directory (like cd in Total Commander) asingn full path of new directory to this parameter. Total COmmander will navigate there.
    ''' <para>Do not assign string longer than <see cref="FindData.MaxPath"/>-1 or uncatchable <see cref="IO.PathTooLongException"/> will be thrown.</para></param>
    ''' <param name="command">Text of command to be executed. It's up to plugin authow which commads to support, but cd is very common.</param>
    ''' <remarks><note type="inheritinfo">This method is called only when plugin implements <see cref="ExecuteFile"/> function and thah function calls base class method.</note></remarks>
    ''' <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
    ''' <exception cref="Security.SecurityException">Security error detected</exception>
    ''' <exception cref="IO.IOException">An IO error occured</exception>
    ''' <exception cref="InvalidOperationException">Excution cannot be done from other reason</exception>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method and all most derived implementations of following methods are marked with <see cref="MethodNotSupportedAttribute"/> as well: <see cref="FtpModeAdvertisement"/>, <see cref="OpenFile"/>, <see cref="ShowFileInfo"/>, <see cref="ExecuteCommand"/></exception>
    Protected Overrides Function ExecuteCommand(ByVal hMainWin As System.IntPtr, ByRef RemoteName As String, ByVal command As String) As ExecExitCode
        Dim Path = GetRealPath(RemoteName)
        If command.ToLower.StartsWith("cd") Then 'CD
            If command.Length > 3 Then
                Dim CDPath = command.Substring(3)
                Dim newPath$
                If IO.Path.IsPathRooted(CDPath) Then
                    newPath = CDPath
                Else
                    Dim dirpath = If(Path.Length = 3 AndAlso Path.EndsWith(":\"), Path, IO.Path.GetDirectoryName(Path))
                    newPath = IO.Path.GetFullPath(IO.Path.Combine(dirpath, CDPath))
                End If
                If Not (newPath.StartsWith("\\") AndAlso newPath.IndexOf("\", 2) = -1) AndAlso Not IO.Directory.Exists(newPath) Then Return ExecExitCode.Error
                RemoteName = "\" & newPath
                Return ExecExitCode.Symlink
            Else
                Return ExecExitCode.Error
            End If
        Else 'Start process
            Dim Program$ : Dim Args$ = ""
            If command.StartsWith("""") Then
                If command.IndexOf(""""c, 1) < 0 Then Return ExecExitCode.Error
                Program = command.Substring(1, command.IndexOf(""""c, 1) - 1)
                If command.IndexOf(""""c, 1) < command.Length - 1 Then Args = command.Substring(command.IndexOf(""""c, 1) + 1)
            ElseIf command.Contains(" "c) Then
                Program = command.Substring(0, command.IndexOf(" "c))
                Args = command.Substring(command.IndexOf(" "c) + 1)
            Else
                Program = command
            End If
            Dim p As New Process
            p.StartInfo.FileName = Program
            p.StartInfo.Arguments = Args
            p.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(Path)
            Try
                p.Start()
            Catch ex As Exception When TypeOf ex Is Security.SecurityException OrElse TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is IO.IOException OrElse TypeOf ex Is InvalidOperationException
                Throw
            Catch
                Return ExecExitCode.Error
            End Try
            Return ExecExitCode.OK
        End If
    End Function
    ''' <summary>Shows file properties for given file or directory.</summary>
    ''' <param name="hMainWin">Handle to parent window which can be used for showing a property sheet.</param>
    ''' <param name="RemoteName">Full path of file or directory to show properties of</param>
    ''' <returns>One of <see cref="ExecExitCode"/> values.</returns>
    ''' <remarks><note type="inheritinfo">This method is called only when plugin implements <see cref="ExecuteFile"/> function and thah function calls base class method.</note></remarks>
    ''' <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
    ''' <exception cref="Security.SecurityException">Security error detected</exception>
    ''' <exception cref="IO.IOException">An IO error occured</exception>
    ''' <exception cref="InvalidOperationException">Excution cannot be done from other reason</exception>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method and all most derived implementations of following methods are marked with <see cref="MethodNotSupportedAttribute"/> as well: <see cref="FtpModeAdvertisement"/>, <see cref="OpenFile"/>, <see cref="ShowFileInfo"/>, <see cref="ExecuteCommand"/></exception>
    Protected Overrides Function ShowFileInfo(ByVal hMainWin As System.IntPtr, ByVal RemoteName As String) As ExecExitCode
        If IsFavoriteAdd(RemoteName) OrElse IsFavorite(RemoteName) Then Return ExecExitCode.Error
        Dim Path = GetRealPath(RemoteName)
        Try
            IOt.FileSystemTools.ShowProperties(Path, New WindowsT.NativeT.Win32Window(hMainWin))
        Catch ex As API.Win32APIException
            Return ExecExitCode.Error
        End Try
        Return ExecExitCode.OK
    End Function
#End Region
#Region "Copy/move"
    ''' <summary>Called to transfer (copy or move) a file within the plugin's file system.</summary>
    ''' <param name="OldName">Name of the remote source file, with full path. The name always starts with a backslash, then the names returned by <see cref="FindFirst"/>/<see cref="FindNext"/> separated by backslashes.</param>
    ''' <param name="NewName">Name of the remote destination file, with full path. The name always starts with a backslash, then the names returned by <see cref="FindFirst"/>/<see cref="FindNext"/> separated by backslashes.</param>
    ''' <param name="Move">If true, the file needs to be moved to the new location and name. Many file systems allow to rename/move a file without actually moving any of its data, only the pointer to it.</param>
    ''' <param name="OverWrite">Tells the function whether it should overwrite the target file or not. See notes below on how this parameter is used.</param>
    ''' <param name="info">A structure of type <see cref="RemoteInfo"/> which contains the parameters of the file being renamed/moved (not of the target file!). In TC 5.51, the fields are set as follows for directories: <see cref="RemoteInfo.SizeLow"/>=0, <see cref="RemoteInfo.SizeHigh"/>=0xFFFFFFFF</param>
    ''' <returns>One of the <see cref="FileSystemExitCode"/> values</returns> 
    ''' <remarks>Total Commander usually calls this function twice:
    ''' <list tpe="bullet"><item>once with <paramref name="OverWrite"/>==false. If the remote file exists, return <see cref="Tools.TotalCommanderT.FileSystemExitCode.FileExists"/>. If it doesn't exist, try to copy the file, and return an appropriate error code.</item>
    ''' <item>a second time with <paramref name="OverWrite"/>==true, if the user chose to overwrite the file.</item></list>
    ''' <para>While copying the file, but at least at the beginning and the end, call <see cref="ProgressProc"/> to show the copy progress and allow the user to abort the operation.</para>
    ''' <para>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.</para>
    ''' <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note>
    ''' </remarks>
    ''' <exception cref="UnauthorizedAccessException">The user does not have required access.  ame effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.ReadError"/>.</exception>
    ''' <exception cref="Security.SecurityException">Security error detected. Same effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.ReadError"/>.</exception>
    ''' <exception cref="IO.IOException">An IO error occured. Same effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.ReadError"/>.</exception>
    ''' <exception cref="IO.FileNotFoundException">Source file was not found. Same effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.FileNotFound"/>.</exception>
    ''' <exception cref="IO.DirectoryNotFoundException">Cannot locate parent directory of target file. Same effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.WriteError"/>.</exception>
    ''' <exception cref="InvalidOperationException">Requested operation is not supported (e.g. resume). Same effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.NotSupported"/>.</exception>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method. Do not confuse with returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.NotSupported"/> - it has completelly different effect.</exception>
    Public Overrides Function RenMovFile(ByVal OldName As String, ByVal NewName As String, ByVal Move As Boolean, ByVal OverWrite As Boolean, ByVal info As RemoteInfo) As FileSystemExitCode
        If IsFavoriteAdd(OldName) Then Return FileSystemExitCode.ReadError
        If NewName.IndexOf("\"c, 1) < 0 Then Return FileSystemExitCode.WriteError
        Dim SourceName = GetRealPath(OldName)
        Dim TargetName = GetRealPath(NewName)
        If Not OverWrite AndAlso IO.File.Exists(TargetName) OrElse IO.Directory.Exists(TargetName) Then Return FileSystemExitCode.FileExists
        If Not IO.File.Exists(SourceName) Then Return FileSystemExitCode.FileNotFound
        If Not IO.Directory.Exists(IO.Path.GetDirectoryName(TargetName)) Then Return FileSystemExitCode.WriteError
        Dim KnownError As FileSystemExitCode?
        Try
            IOt.Copy(SourceName, TargetName, _
                    Function(SourceFileName, TotalSize, BytesCopyed, TargetFileName) _
                        If(Me.ProgressProc(OldName, NewName, BytesCopyed / TotalSize * 100), PathCopyCallbackResult.Abort, PathCopyCallbackResult.Ignore), _
                    Function(SourceFileName, TargetFileName, Stage, Exception) _
                        If(Stage = PathCopyStages.CheckTagretFileExists AndAlso TypeOf Exception Is FileAlreadyExistsException AndAlso OverWrite, _
                           PathCopyCallbackResult.Retry, _
                        If( _
                            If(Stage = PathCopyStages.Read OrElse Stage = PathCopyStages.OpenSourceFile, _
                                Write(Of FileSystemExitCode?)(FileSystemExitCode.ReadError, KnownError), _
                            If(Stage = PathCopyStages.Write OrElse Stage = PathCopyStages.OpenTargetFile, _
                               Write(Of FileSystemExitCode?)(FileSystemExitCode.WriteError, KnownError), _
                               FileSystemExitCode.OK)) = FileSystemExitCode.OK, _
                            PathCopyCallbackResult.Abort, PathCopyCallbackResult.Abort)), _
                     Move)
        Catch ex As OperationCanceledException
            Return FileSystemExitCode.UserAbort
        Catch ex As Exception When TypeOf ex Is IO.IOException OrElse TypeOf ex Is Security.SecurityException OrElse TypeOf ex Is UnauthorizedAccessException
            If KnownError.HasValue Then Return KnownError
            Throw
        Catch ex As Exception
            If KnownError.HasValue Then Return KnownError
            Throw New IO.IOException(ex.Message, ex)
        End Try
        If Move Then
            Try
                IO.File.Delete(SourceName)
            Catch ex As Exception When TypeOf ex Is IO.IOException OrElse TypeOf ex Is Security.SecurityException OrElse TypeOf ex Is UnauthorizedAccessException
                Throw
            Catch ex As Exception
                Throw New IO.IOException(ex.Message, ex)
            End Try
        End If
        Return FileSystemExitCode.OK
    End Function
    ''' <summary>Writes value to target and returns it</summary>
    ''' <param name="Value">Value to be written</param>
    ''' <param name="Target">Target to write <paramref name="Value"/> to</param>
    ''' <typeparam name="T">Type of value</typeparam>
    ''' <returns><paramref name="Value"/></returns>
    ''' <remarks><note>This is helper function</note></remarks>
    Private Function Write(Of T)(ByVal Value As T, ByRef Target As T) As T
        Target = Value
        Return Value
    End Function

    ''' <summary>Transfers a file from the plugin's file system to the normal file system (drive letters or UNC).</summary>
    ''' <param name="RemoteName">Name of the file to be retrieved, with full path. The name always starts with a backslash, then the names returned by <see cref="FindFirst"/>/<see cref="FindNext"/> separated by backslashes.</param>
    ''' <param name="LocalName">Local file name with full path, either with a drive letter or UNC path (\\Server\Share\filename). The plugin may change the NAME/EXTENSION of the file (e.g. when file conversion is done), but not the path! Do not assign strings longer than <see cref="FindData.MaxPath"/> or uncatchable <see cref="IO.PathTooLongException"/> will be thrown.</param>
    ''' <param name="CopyFlags">Can be combination of the <see cref="CopyFlags"/> values</param>
    ''' <param name="info">This parameter contains information about the remote file which was previously retrieved via <see cref="FindFirst"/>/<see cref="FindNext"/>: The size, date/time, and attributes of the remote file. May be useful to copy the attributes with the file, and for displaying a progress dialog.</param>
    ''' <returns>One of the <see cref="FileSystemExitCode"/> values</returns> 
    ''' <remarks>Total Commander usually calls this function twice:
    ''' <list type="bullet">
    ''' <item>once with <paramref name="CopyFlags"/>==0 or <paramref name="CopyFlags"/>==<see cref="Tools.TotalCommanderT.CopyFlags.Move"/>. If the local file exists and resume is supported, return <see cref="Tools.TotalCommanderT.FileSystemExitCode.ExistsResumeAllowed"/>. If resume isn't allowed, return <see cref="Tools.TotalCommanderT.FileSystemExitCode.FileExists"/></item>
    ''' <item>a second time with <see cref="Tools.TotalCommanderT.CopyFlags.[Resume]"/> or <see cref="Tools.TotalCommanderT.CopyFlags.Overwrite"/>, depending on the user's choice. The resume option is only offered to the user if <see cref="Tools.TotalCommanderT.FileSystemExitCode.ExistsResumeAllowed"/> was returned by the first call.</item>
    ''' <item><see cref="Tools.TotalCommanderT.CopyFlags.SameCase"/> and <see cref="Tools.TotalCommanderT.CopyFlags.DifferentCase"/> are NEVER passed to this function, because the plugin can easily determine whether a local file exists or not.</item>
    ''' <item><see cref="Tools.TotalCommanderT.CopyFlags.Move"/> is set, the plugin needs to delete the remote file after a successful download.</item>
    ''' </list>
    ''' <para>While copying the file, but at least at the beginning and the end, call <see cref="ProgressProc"/> to show the copy progress and allow the user to abort the operation.</para>
    ''' <para>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.</para>
    ''' <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note>
    ''' </remarks>
    ''' <exception cref="UnauthorizedAccessException">The user does not have required access.  ame effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.ReadError"/>.</exception>
    ''' <exception cref="Security.SecurityException">Security error detected. Same effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.ReadError"/>.</exception>
    ''' <exception cref="IO.IOException">An IO error occured. Same effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.ReadError"/>.</exception>
    ''' <exception cref="IO.FileNotFoundException">Source file was not found. Same effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.FileNotFound"/>.</exception>
    ''' <exception cref="IO.DirectoryNotFoundException">Cannot locate parent directory of target file. Same effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.WriteError"/>.</exception>
    ''' <exception cref="InvalidOperationException">Requested operation is not supported (e.g. resume). Same effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.NotSupported"/>.</exception>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method. Do not confuse with returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.NotSupported"/> - it has completelly different effect.</exception>
    Public Overrides Function GetFile(ByVal RemoteName As String, ByRef LocalName As String, ByVal CopyFlags As CopyFlags, ByVal info As RemoteInfo) As FileSystemExitCode
        If IsFavoriteAdd(RemoteName) Then Return FileSystemExitCode.ReadError
        Dim SourcePath = GetRealPath(RemoteName)
        Try
            Return FileOperation(LocalName, SourcePath, LocalName, RemoteName, CopyFlags)
        Catch ex As Exception When Not TypeOf ex Is UnauthorizedAccessException AndAlso Not TypeOf ex Is IO.IOException AndAlso Not TypeOf ex Is Security.SecurityException
            Throw New IO.IOException(ex.Message, ex)
        End Try
    End Function
    ''' <summary>Copies file with possible resume</summary>
    ''' <param name="TargetPath">Copy file here</param>
    ''' <param name="SourcePath">Copy this file</param>
    ''' <param name="TargetPathDisplay">Display this as target</param>
    ''' <param name="SourcePathDisplay">Display this as stource</param>
    ''' <param name="CopyFlags">Operation configuration</param>
    ''' <returns>Operation result</returns>
    Private Function FileOperation(ByVal TargetPath As String, ByVal SourcePath As String, ByRef TargetPathDisplay As String, ByVal SourcePathDisplay As String, ByVal CopyFlags As CopyFlags) As FileSystemExitCode
        If Not (CopyFlags And TotalCommanderT.CopyFlags.Overwrite) = TotalCommanderT.CopyFlags.Overwrite AndAlso Not (CopyFlags And TotalCommanderT.CopyFlags.Resume) = TotalCommanderT.CopyFlags.Resume AndAlso IO.File.Exists(TargetPath) Then Return FileSystemExitCode.ExistsResumeAllowed
        If IO.File.Exists(TargetPath) AndAlso (CopyFlags And TotalCommanderT.CopyFlags.Resume) = TotalCommanderT.CopyFlags.Resume Then
            Using TargetFile = IO.File.Open(TargetPath, IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.Read), _
                  SourceFile = IO.File.Open(SourcePath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
                If SourceFile.Length <= TargetFile.Length Then Return FileSystemExitCode.ReadError
                SourceFile.Position = TargetFile.Length
                Dim buff(1024) As Byte
                Dim BRead As Integer
                Dim Total As Long = TargetFile.Length
                Do
                    Try : BRead = SourceFile.Read(buff, 0, buff.Length)
                    Catch : Return FileSystemExitCode.ReadError : End Try
                    If BRead > 0 Then
                        Try : TargetFile.Write(buff, 0, BRead)
                        Catch : Return FileSystemExitCode.WriteError : End Try
                        Total += BRead
                        If Me.ProgressProc(SourcePathDisplay, TargetPathDisplay, Total / SourceFile.Length * 100) Then Return FileSystemExitCode.UserAbort
                    End If
                Loop While BRead <> 0
            End Using
            If CopyFlags And TotalCommanderT.CopyFlags.Move Then IO.File.Delete(SourcePath)
        Else
            Using TargetFile = IO.File.Open(TargetPath, If(CopyFlags And TotalCommanderT.CopyFlags.Overwrite, IO.FileMode.Create, IO.FileMode.CreateNew), IO.FileAccess.Write, IO.FileShare.Read), _
                  SourceFile = IO.File.Open(SourcePath, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
                Dim buff(1024) As Byte
                Dim BRead As Integer
                Dim Total As Long = 0
                Do
                    Try : BRead = SourceFile.Read(buff, 0, buff.Length)
                    Catch : Return FileSystemExitCode.ReadError : End Try
                    If BRead > 0 Then
                        Try : TargetFile.Write(buff, 0, BRead)
                        Catch : Return FileSystemExitCode.WriteError : End Try
                        Total += BRead
                        If Me.ProgressProc(SourcePathDisplay, TargetPathDisplay, Total / SourceFile.Length * 100) Then Return FileSystemExitCode.UserAbort
                    End If
                Loop While BRead <> 0
            End Using
            If CopyFlags And TotalCommanderT.CopyFlags.Move Then IO.File.Delete(SourcePath)
        End If
        Return Nothing
    End Function
    ''' <summary>Transfers a file from the normal file system (drive letters or UNC) to the plugin's file system.</summary>
    ''' <param name="LocalName">Local file name with full path, either with a drive letter or UNC path (\\Server\Share\filename). This file needs to be uploaded to the plugin's file system.</param>
    ''' <param name="RemoteName">Name of the remote file, with full path. The name always starts with a backslash, then the names returned by <see cref="FindFirst"/>/<see cref="FindNext"/> separated by backslashes. The plugin may change the NAME/EXTENSION of the file (e.g. when file conversion is done), but not the path! Do not assign string longer than <see cref="FindData.MaxPath"/> to this parameter or uncatchable <see cref="IO.PathTooLongException"/> will be thrown.</param>
    ''' <param name="CopyFlags">Can be combination of the <see cref="CopyFlags"/> values.</param>
    ''' <returns>One of the <see cref="FileSystemExitCode"/> values</returns>
    ''' <remarks>Total Commander usually calls this function twice, with the following parameters in <paramref name="CopyFlags"/>:
    ''' <list type="bullet">
    ''' <item>once with neither <see cref="Tools.TotalCommanderT.CopyFlags.Resume"/> nor <see cref="Tools.TotalCommanderT.CopyFlags.Overwrite"/> set. If the remote file exists and resume is supported, return <see cref="Tools.TotalCommanderT.FileSystemExitCode.ExistsResumeAllowed"/>. If resume isn't allowed, return <see cref="Tools.TotalCommanderT.FileSystemExitCode.FileExists"/></item>
    ''' <item>a second time with <see cref="Tools.TotalCommanderT.CopyFlags.Resume"/> or <see cref="Tools.TotalCommanderT.CopyFlags.Overwrite"/>, depending on the user's choice. The resume option is only offered to the user if <see cref="Tools.TotalCommanderT.FileSystemExitCode.ExistsResumeAllowed"/> was returned by the first call.</item>
    ''' <item>The flags <see cref="Tools.TotalCommanderT.CopyFlags.SameCase"/> or <see cref="Tools.TotalCommanderT.CopyFlags.DifferentCase"/> are added to CopyFlags when the remote file exists and needs to be overwritten. This is a hint to the plugin to allow optimizations: Depending on the plugin type, it may be very slow to check the server for every single file when uploading.</item>
    ''' <item>If the flag <see cref="Tools.TotalCommanderT.CopyFlags.Move"/> is set, the plugin needs to delete the local file after a successful upload.</item>
    ''' </list>
    ''' <para>While copying the file, but at least at the beginning and the end, call ProgressProc to show the copy progress and allow the user to abort the operation.</para>
    ''' <para>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.</para>
    ''' <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note>
    ''' </remarks>
    ''' <exception cref="UnauthorizedAccessException">The user does not have required access.  ame effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.ReadError"/>.</exception>
    ''' <exception cref="Security.SecurityException">Security error detected. Same effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.ReadError"/>.</exception>
    ''' <exception cref="IO.IOException">An IO error occured. Same effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.ReadError"/>.</exception>
    ''' <exception cref="IO.FileNotFoundException">Source file was not found. Same effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.FileNotFound"/>.</exception>
    ''' <exception cref="IO.DirectoryNotFoundException">Cannot locate parent directory of target file. Same effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.WriteError"/>.</exception>
    ''' <exception cref="InvalidOperationException">Requested operation is not supported (e.g. resume). Same effect as returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.NotSupported"/>.</exception>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method. Do not confuse with returning <see cref="Tools.TotalCommanderT.FileSystemExitCode.NotSupported"/> - it has completelly different effect.</exception>
    Public Overrides Function PutFile(ByVal LocalName As String, ByRef RemoteName As String, ByVal CopyFlags As CopyFlags) As FileSystemExitCode
        If RemoteName.IndexOf("\"c, 1) < 0 Then Return FileSystemExitCode.WriteError
        Dim TargetPath = GetRealPath(RemoteName)
        Try
            Return FileOperation(TargetPath, LocalName, RemoteName, LocalName, CopyFlags)
        Catch ex As Exception When Not TypeOf ex Is UnauthorizedAccessException AndAlso Not TypeOf ex Is IO.IOException AndAlso Not TypeOf ex Is Security.SecurityException
            Throw New IO.IOException(ex.Message, ex)
        End Try
    End Function
#End Region
    ''' <summary>Sets the (Windows-Style) file times of a file/dir.</summary>
    ''' <param name="RemoteName">Name of the file/directory whose attributes have to be set</param>
    ''' <param name="CreationTime">Creation time of the file. May be NULL to leave it unchanged.</param>
    ''' <param name="LastAccessTime">Last access time of the file. May be NULL to leave it unchanged.</param>
    ''' <param name="LastWriteTime">Last write time of the file. May be NULL to leave it unchanged. If your file system only supports one time, use this parameter!</param>
    ''' <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
    ''' <exception cref="Security.SecurityException">Security error detected</exception>
    ''' <exception cref="IO.IOException">An IO error occured</exception>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
    ''' <remarks>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.
    ''' <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
    Public Overrides Sub SetTime(ByVal RemoteName As String, ByVal CreationTime As Date?, ByVal LastAccessTime As Date?, ByVal LastWriteTime As Date?)
        Dim path = GetRealPath(RemoteName)
        If IO.File.Exists(path) Then
            If CreationTime.HasValue Then IO.File.SetCreationTime(path, CreationTime)
            If LastAccessTime.HasValue Then IO.File.SetLastAccessTime(path, LastAccessTime)
            If LastWriteTime.HasValue Then IO.File.SetLastWriteTime(path, LastWriteTime)
        Else
            If CreationTime.HasValue Then IO.Directory.SetCreationTime(path, CreationTime)
            If LastAccessTime.HasValue Then IO.Directory.SetLastAccessTime(path, LastAccessTime)
            If LastWriteTime.HasValue Then IO.Directory.SetLastWriteTime(path, LastWriteTime)
        End If
    End Sub
    ''' <summary>Sets the (Windows-Style) file attributes of a file/dir. <see cref="ExecuteFile"/> is called for Unix-style attributes.</summary>
    ''' <param name="RemoteName">Name of the file/directory whose attributes have to be set</param>
    ''' <param name="NewAttr">New file attributes</param>
    ''' <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
    ''' <exception cref="Security.SecurityException">Security error detected</exception>
    ''' <exception cref="IO.IOException">An IO error occured</exception>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
    ''' <remarks>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.
    ''' <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
    Public Overrides Sub SetAttr(ByVal RemoteName As String, ByVal NewAttr As StandardFileAttributes)
        Dim path = GetRealPath(RemoteName)
        If IO.File.Exists(path) Then
            IO.File.SetAttributes(path, NewAttr)
        Else
            Dim di As New IO.DirectoryInfo(path)
            di.Attributes = NewAttr
        End If
    End Sub
    ''' <summary>Gets favorite item by name</summary>
    ''' <param name="Name">Name of titem to get</param>
    ''' <returns>Favorite item with <see cref="FavoriteItem.Name"/> <paramref name="Name"/> or null</returns>
    Private Function GetFavoriteItem(ByVal Name As String) As FavoriteItem
        Return (From fi In FavoriteItems Select fi Where fi.Name = Name).FirstOrDefault
    End Function

    ''' <summary>Called when a file/directory is displayed in the file list. It can be used to specify a custom icon for that file/directory.</summary>
    ''' <param name="RemoteName">This is the full path to the file or directory whose icon is to be retrieved. When extracting an icon, you can return an icon name here - this ensures that the icon is only cached once in the calling program. The returned icon name must not be longer than <see cref="FindData.MaxPath"/> - 1 characters (otherwise uncatchable <see cref="IO.PathTooLongException"/> will be thrown by <see cref="M:Tools.TotalCommanderT.FileSystemPlugin.FsExctractCustomIcon(System.SByte*,System.Int32,HICON__**)"/>). The icon itself must still be returned in <paramref name="TheIcon"/>!</param>
    ''' <param name="ExtractFlags">Flags for the extract operation. A combination of <see cref="IconExtractFlags"/>.</param>
    ''' <param name="TheIcon">Here you need to return the icon, unless return value is <see cref="Tools.TotalCommanderT.IconExtractResult.Delayed"/> or <see cref="Tools.TotalCommanderT.IconExtractResult.UseDefault"/></param>
    ''' <returns>One of the <see cref="IconExtractResult"/> values</returns> 
    ''' <remarks>If you return <see cref="Tools.TotalCommanderT.IconExtractResult.Delayed"/>, <see cref="ExctractCustomIcon"/> will be called again from a background thread at a later time. A critical section is used by the calling app to ensure that <see cref="ExctractCustomIcon"/> is never entered twice at the same time. This return value should be used for icons which take a while to extract, e.g. EXE icons. If the user turns off background loading of icons, the function will be called in the foreground with the <see cref="Tools.TotalCommanderT.IconExtractFlags.BackgroundThread"/> flag.
    ''' <para>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.</para>
    ''' <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
    Public Overrides Function ExctractCustomIcon(ByRef RemoteName As String, ByVal ExtractFlags As IconExtractFlags, ByRef TheIcon As System.Drawing.Icon) As IconExtractResult
        If RemoteName.EndsWith("\..\") Then Return IconExtractResult.UseDefault
        If IsFavoriteAdd(RemoteName) Then
            TheIcon = My.Resources.Favorites
            Return IconExtractResult.ExtractedDestroy
        End If
        'If (RemoteName.Length = 2 OrElse (RemoteName.Length > 2 And RemoteName(2) <> "C"c)) AndAlso RemoteName.IndexOf("\", 1) < 0 Then
        '    'UNC computer
        '    TheIcon = My.Resources.computer
        '    Return IconExtractResult.ExtractedDestroy
        'End If
        Static OnStack As Boolean
        If OnStack Then Return IconExtractResult.UseDefault
        OnStack = True
        Try
            Dim Path As Path = GetRealPath(RemoteName)
            Try
                If Not Path.IsUNC AndAlso Not Path.ExistsPath(True) Then Return IconExtractResult.UseDefault
                TheIcon = Path.GetIcon((ExtractFlags And IconExtractFlags.SmallIcon) <> IconExtractFlags.SmallIcon)
            Catch
                Return IconExtractResult.UseDefault
            End Try
            Return IconExtractResult.ExtractedDestroy
        Finally
            OnStack = False
        End Try
    End Function
    ''' <summary>Called when a file/directory is displayed in thumbnail view. It can be used to return a custom bitmap for that file/directory.</summary>
    ''' <param name="width">The maximum dimensions of the preview bitmap. If your image is smaller, or has a different side ratio, then you need to return an image which is smaller than these dimensions!</param>
    ''' <param name="height">The maximum dimensions of the preview bitmap. If your image is smaller, or has a different side ratio, then you need to return an image which is smaller than these dimensions!</param>
    ''' <returns>The <see cref="BitmapResult"/> indicating where to obtain the bitmap or the bitmap itself; null when default image shuld be used.</returns>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
    ''' <remarks>
    ''' <para>Inportant notes</para>
    ''' <list type="numbered">
    ''' <item>This function is only called in Total Commander 7.0 and later. The reported plugin version will be >= 1.4.</item>
    ''' <item>The bitmap handle goes into possession of Total Commander, which will delete it after using it. The plugin must not delete the bitmap handle! (when <see2 cref2="F:Tools.TotalCommanderT.BitmapResult.Bitmap"/> is set.</item>
    ''' <item>Make sure you scale your image correctly to the desired maximum width+height! Do not fill the rest of the bitmap - instead, create a bitmap which is SMALLER than requested! This way, Total Commander can center your image and fill the rest with the default background color.</item>
    ''' </list>
    ''' <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
    Public Overrides Function GetPreviewBitmap(ByVal RemoteName As String, ByVal width As Integer, ByVal height As Integer) As BitmapResult
        If IsFavoriteAdd(RemoteName) Then Return Nothing
        Dim Path = GetRealPath(RemoteName)
        Dim ext = IO.Path.GetExtension(Path)
        Try
            Select Case ext.ToLowerInvariant
                Case ".jpg", ".jpeg", ".bmp", ".dib", ".tiff", ".tif", ".gif", ".png"
                    Dim bmp = New Drawing.Bitmap(Path)
                    Return New BitmapResult(bmp.GetThumbnail(New Drawing.Size(width, height)))
                Case ".wmf", ".emf"
                    Dim metafile = New Drawing.Imaging.Metafile(Path)
                    Return New BitmapResult(metafile.GetThumbnail(New Drawing.Size(width, height)))
                Case ".ico", ".cur"
                    Dim icon = New Drawing.Icon(Path)
                    Return New BitmapResult(icon.ToBitmap.GetThumbnail(New Drawing.Size(width, height)))
                Case Else
                    Return New BitmapResult(Path, False)
            End Select
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    ''' <summary>Called immediately after <see cref="OnInit"/>.</summary>
    ''' <param name="dps">This structure curently contains version number of the Total Commander plugin interface (not this managed interface) and suggested location of settings file. It is recommended to store any plugin-specific information either directly in that file or in that directory under a different name.</param>
    ''' <remarks>Make sure to use a unique header when storing data in this file, because it is shared by other file system plugins! If your plugin needs more than 1kbyte of data, you should use your own ini file because ini files are limited to 64k.
    ''' <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note>
    ''' <note type="inheritinfo">Always call base class method. When base class method is not called, the <see cref="PluginParams"/> property does not have valid value.</note></remarks>
    ''' <exception cref="InvalidOperationException">This method is called when it was already called. This method can be called only once on each instance.</exception>
    Public Overrides Sub SetDefaultParams(ByVal dps As DefaultParams)
        MyBase.SetDefaultParams(dps)
        If IO.File.Exists(ConfigPath) Then
            Dim doc = XDocument.Load(ConfigPath)
            FavoriteItems = New List(Of FavoriteItem)( _
                From favel In doc.<ws:settings>.<ws:fav> Select New FavoriteItem(favel.@name, favel.@target, CType(favel.@created, Date)))
        End If
    End Sub
    ''' <summary>Saves settings</summary>
    Private Sub SaveSettings()
        Dim doc = <?xml version="1.0" encoding="utf-8"?>
                  <ws:settings>
                      <%= From fi In FavoriteItems Select <ws:fav name=<%= fi.Name %> target=<%= fi.Target %> created=<%= fi.CreateTime %>/> %>
                  </ws:settings>
        doc.Save(ConfigPath)
    End Sub

    ''' <summary>Gets path of configuration file</summary>
    ''' <exception cref="InvalidOperationException">Value is being got before <see cref="FileSystemPlugin.SetDefaultParams"/> was called</exception>
    Private ReadOnly Property ConfigPath() As String
        Get
            Return IO.Path.Combine(IO.Path.GetDirectoryName(PluginParams.DefaultIniName), "wfxSample.config")
        End Get
    End Property
End Class
