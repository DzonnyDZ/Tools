<TotalCommanderPlugin("wfxSample")> _
Public Class SampleFileSystemPlugin
    Inherits FileSystemPlugin

    ''' <summary>When overriden in derived class retrieves the first file in a directory of the plugin's file system.</summary>
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
        If Path = "\" Then
            Dim ContentEnumerator = (From drv In IO.DriveInfo.GetDrives).GetEnumerator
            If ContentEnumerator.MoveNext Then
                FindData = GetFindData(ContentEnumerator.Current.Name)
                Return ContentEnumerator
            Else
                Return Nothing
            End If
        Else
            Dim RealPath = GetRealPath(Path)
            Dim ContentEnumerator = (From itm In IO.Directory.GetFileSystemEntries(RealPath)).GetEnumerator
            If ContentEnumerator.MoveNext Then
                FindData = GetFindData(ContentEnumerator.Current)
                Return ContentEnumerator
            Else
                Return Nothing
            End If
        End If
    End Function

    Private Function GetRealPath(ByVal Path$) As String
        If Path = "\" Then Return ""
        Dim ret = Path.Substring(1)
        If ret.Length = 2 AndAlso ret.EndsWith(":") Then ret &= "\"
        Return ret
    End Function

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
    ''' <summary>When overriden in derived class retrieves the next file in a directory of the plugin's file system</summary>
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
    ''' <summary>When overriden in derived class performs custom clenup at end of a <see cref="FindFirst"/>/<see cref="FindNext"/> loop, either after retrieving all files, or when the user aborts it.</summary>
    ''' <param name="Status">The object returned by <see cref="FindFirst"/>; null when Total Commander supplied handle that is  not in <see cref="HandleDictionary"/>. When this function exists, <paramref name="Status"/> automatically removed from <see cref="HandleDictionary"/></param>
    Public Overrides Sub FindClose(ByVal Status As Object)
        MyBase.FindClose(Status)
        Dim disp = TryCast(Status, IDisposable)
        If disp IsNot Nothing Then disp.Dispose()
    End Sub

    Public Overrides ReadOnly Property Name() As String
        Get
            Return "WFX Sample"
        End Get
    End Property
End Class
