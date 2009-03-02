Imports Tools.ExtensionsT
''' <summary>Sample Total Commander file system plugin (just works over local file system)</summary>
<TotalCommanderPlugin("wfxSample")> _
Public Class SampleFileSystemPlugin
    Inherits FileSystemPlugin

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
            Dim ContentEnumerator = (From drv In IO.DriveInfo.GetDrives).GetEnumerator
            If ContentEnumerator.MoveNext Then
                FindData = GetFindData(ContentEnumerator.Current.Name)
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
    ''' <summary>Gets real local path from path reported by Total Commander</summary>
    ''' <param name="Path">UNC path corresponding to geiven Total Commander plugin rooted (\-starting) path</param>
    Private Function GetRealPath(ByVal Path$) As String
        If Path = "\" Then Return ""
        If Path = "" Then Return ""
        Dim ret = Path.Substring(1)
        If ret.Length = 2 AndAlso ret.EndsWith(":") Then ret &= "\"
        Return ret
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
    ''' <param name="FindData">A <see cref="FindData"/> struct (mimics WIN32_FIND_DATA as defined in the Windows SDK) to be pupulated with the file or directory details. Use the <see cref="FindData.Attributes"/> field set to <see2 cref2="F:Tools.TotalCommanderT.FileAttributes.Directory"/> to distinguish files from directories. On Unix systems, you can | (or) the <see cref="FindData.Attributes"/> field with 0x80000000 and set the <see cref="FindData.ReparsePointTag"/> parameter to the Unix file mode (permissions).</param>
    ''' <returns>Return false if there are no more files, and true otherwise. SetLastError() does not need to be called.</returns>
    ''' <exception cref="IO.DirectoryNotFoundException">Directory does not exists</exception>
    ''' <exception cref="UnauthorizedAccessException">The user does not have access to the directory</exception>
    ''' <exception cref="Security.SecurityException">Security error detected</exception>
    ''' <exception cref="IO.IOException">Another error occured</exception>
    ''' <remarks><note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
    Public Overrides Function FindNext(ByVal Status As Object, ByRef FindData As FindData) As Boolean
        If TypeOf Status Is IEnumerator(Of IO.DriveInfo) Then
            Dim en As IEnumerator(Of IO.DriveInfo) = Status
            If en.MoveNext Then
                FindData = GetFindData(en.Current.Name)
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
            Throw New InvalidOperationException 'Should be enver thrown cause nothing else should be passed here
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
    ''' <param name="Path">Name of the directory to be created, with full path. The name always starts with a backslash, then the names returned by <see cref="FsFindFirst"/>/<see cref="FsFindNext"/> separated by backslashes.</param>
    ''' <returns>Return true if the directory could be created, false if not.</returns>
    ''' <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
    ''' <exception cref="Security.SecurityException">Security error detected</exception>
    ''' <exception cref="IO.IOException">An IO error occured</exception>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
    ''' <remarks>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.
    ''' <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
    Public Overrides Function MkDir(ByVal Path As String) As Boolean
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
    ''' <returns>Return <see2 cref2="F:Tools.TotalCommanderT.ExecExitCode.Yourself"/> if Total Commander should download the file and execute it locally, <see2 cref2="F:Tools.TotalCommanderT.ExecExitCode.OK"/> if the command was executed successfully in the plugin (or if the command isn't applicable and no further action is needed), <see2 cref2="F:Tools.TotalCommanderT.ExecExitCode.Error"/> if execution failed, or <see2 cref2="F:Tools.TotalCommanderT.ExecExitCode.Symlink"/> if this was a (symbolic) link or .lnk file pointing to a different directory.</returns>
    ''' <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
    ''' <exception cref="Security.SecurityException">Security error detected</exception>
    ''' <exception cref="IO.IOException">An IO error occured</exception>
    ''' <exception cref="InvalidOperationException">Excution cannot be done from other reason</exception>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
    ''' <remarks>Meaning of verbs:
    ''' <list type="table"><listheader><term>verb</term><description>meaning</description></listheader>
    ''' <item><term>open</term><description>This is called when the user presses ENTER on a file. There are three ways to handle it:
    ''' <list type="bulet">
    ''' <item>For internal commands like "Add new connection", execute it in the plugin and return <see2 cref2="F:Tools.TotalCommanderT.ExecExitCode.OK"/> or <see2 cref2="F:Tools.TotalCommanderT.ExecExitCode.Error"/></item>
    ''' <item>Let Total Commander download the file and execute it locally: return <see2 cref2="F:Tools.TotalCommanderT.ExecExitCode.Yourself"/></item>
    ''' <item>If the file is a (symbolic) link, set <paramref name="RemoteName"/> to the location to which the link points (including the full plugin path), and return <see2 cref2="F:Tools.TotalCommanderT.ExecExitCode.Symlink"/>. Total Commander will then switch to that directory. You can also switch to a directory on the local harddisk! To do this, return a path starting either with a drive letter, or an UNC location (\\server\share). The maximum allowed length of such a path is <see cref="FindData.MaxPath"/>-1 (= 259) characters!</item>
    ''' </list></description></item>
    ''' <item><term>properties</term><description>Show a property sheet for the file (optional). Currently not handled by internal Totalcmd functions if <see2 cref2="F:Tools.TotalCommanderT.ExecExitCode.Yourself"/> is returned, so the plugin needs to do it internally.</description></item>
    ''' <item><term>chmod xxx</term><description>The xxx stands for the new Unix mode (attributes) to be applied to the file <paramref name="RemoteName"/>. This verb is only used when returning Unix attributes through <see cref="FindFirst"/>/<see cref="FindNext"/></description></item>
    ''' <item><term>quote commandline</term><description>Execute the command line entered by the user in the directory <paramref name="RemoteName"/> . This is called when the user enters a command in Totalcmd's command line, and presses ENTER. This is optional, and allows to send plugin-specific commands. It's up to the plugin writer what to support here. If the user entered e.g. a cd directory command, you can return the new path in <paramref name="RemoteName"/> (max 259 characters), and give <see2 cref2="F:Tools.TotalCommanderT.ExecExitCode.Symlink"/> as return value. Return <see2 cref2="F:Tools.TotalCommanderT.ExecExitCode.OK"/> to cause a refresh (re-read) of the active panel.</description></item>
    ''' </list>
    ''' <para>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.</para>
    ''' <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
    Public Overrides Function ExecuteFile(ByVal hMainWin As System.IntPtr, ByRef RemoteName As String, ByVal Verb As String) As ExecExitCode
        Return MyBase.ExecuteFile(hMainWin, RemoteName, Verb)  'This enables other functions in this #Region
    End Function
    ''' <summary>Opens or executes given file.</summary>
    ''' <param name="hMainWin">Handle to Total Commander window.</param>
    ''' <param name="RemoteName">Full path of file to be opened or executed. In case the file is link (like *.lnk files in Windows) method should assignt link target path to this argument and return <see2 cref2="F:Tools.TotalCommanderT.ExecExitCode.Symlink"/>. It will make Total Commander navigate to a new path.
    ''' <para>Do not assign string longer than <see cref="FindData.MaxPath"/>-1 or uncatchable <see cref="IO.PathTooLongException"/> will be thrown.</para></param>
    ''' <returns>Return <see2 cref2="F:Tools.TotalCommanderT.ExecExitCode.Yourself"/> if Total Commander should download the file and execute it locally, <see2 cref2="F:Tools.TotalCommanderT.ExecExitCode.OK"/> if the command was executed successfully in the plugin (or if the command isn't applicable and no further action is needed), <see2 cref2="F:Tools.TotalCommanderT.ExecExitCode.Error"/> if execution failed, or <see2 cref2="F:Tools.TotalCommanderT.ExecExitCode.Symlink"/> if this was a (symbolic) link or .lnk file pointing to a different directory.</returns>
    ''' <remarks><note type="inheritinfo">This method is called only when plugin implements <see cref="ExecuteFile"/> function and thah function calls base class method.</note></remarks>
    ''' <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
    ''' <exception cref="Security.SecurityException">Security error detected</exception>
    ''' <exception cref="IO.IOException">An IO error occured</exception>
    ''' <exception cref="InvalidOperationException">Excution cannot be done from other reason</exception>
    ''' <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method and all most derived implementations of following methods are marked with <see cref="MethodNotSupportedAttribute"/> as well: <see cref="FtpModeAdvertisement"/>, <see cref="OpenFile"/>, <see cref="ShowFileInfo"/>, <see cref="ExecuteCommand"/></exception>
    Protected Overrides Function OpenFile(ByVal hMainWin As System.IntPtr, ByRef RemoteName As String) As ExecExitCode
        Dim Path = GetRealPath(RemoteName)
        If IO.Path.GetExtension(Path).ToLower = ".lnk" Then 'Follow link
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
ExecFile:   Dim p As New Process
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
                If Not IO.Directory.Exists(newPath) Then Return ExecExitCode.Error
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
        Dim Path = GetRealPath(RemoteName)
        Try
            IOt.FileSystemTools.ShowProperties(Path, New WindowsT.NativeT.Win32Window(hMainWin))
        Catch ex As API.Win32APIException
            Return ExecExitCode.Error
        End Try
        Return ExecExitCode.OK
    End Function
#End Region
End Class
