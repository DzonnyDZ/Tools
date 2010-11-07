Imports Tools.IOt, Tools.API, System.Drawing, Tools.ExtensionsT
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text, System.Linq

#If Config <= Nightly Then 'Stage:Nightly
Namespace IOt
    ''' <summary>Contains file system-related methods and extension methods</summary>
    ''' <version version="1.5.2">Renamed from FileTystemTools to FileSystemTools</version>
    Public Module FileSystemTools
        ''' <summary>Gets icon for given file or folder (including drive and UNC share or server)</summary>
        ''' <param name="PathP">Provides path to get icon for</param>
        ''' <param name="Large">True to get large icon (false to get small icon)</param>
        ''' <returns>Icon that represents given file or folder in operating system. Null if icon obtainment failed.</returns>
        ''' <param name="Overlays">True to add all applicable overlay icons</param>
        ''' <seelaso cref="Drawing.Icon.ExtractAssociatedIcon"/>
        ''' <exception cref="IO.FileNotFoundException">File or folder <paramref name="Path"/> does not exists.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Path"/> is null</exception>
        ''' <version version="1.5.2">Can get icon for empty removable drive (like CD-ROM without CD in it) and for UNC computer in format \\servername)</version>
        <Extension()> _
        Public Function GetIcon(ByVal PathP As IPathProvider, Optional ByVal Large As Boolean = False, Optional ByVal Overlays As Boolean = True) As Drawing.Icon
            If PathP Is Nothing Then Throw New ArgumentNullException("PathP")
            Dim Path = New Path(PathP.Path.TrimEnd("\"))
            If Not (PathP.Path.StartsWith("\\") AndAlso PathP.Path.Length > 2 AndAlso PathP.Path.IndexOf("\"c, 2) < 0 AndAlso (From ch In PathP.Path.Substring(2) Where IO.Path.GetInvalidFileNameChars.Contains(ch)).Count = 0) AndAlso _
                Not Path.Exists AndAlso (From di In My.Computer.FileSystem.Drives Where di.Name = PathP.Path Select di.Name).FirstOrDefault Is Nothing _
                Then Throw New IO.FileNotFoundException(String.Format(ResourcesT.Exceptions.Path0DoesNotExist, Path))
            Dim shInfo As New SHFILEINFO
            Dim ret = SHGetFileInfo(Path, 0, shInfo, Marshal.SizeOf(shInfo), _
                FileInformationFlags.SHGFI_ICON Or If(Large, FileInformationFlags.SHGFI_LARGEICON, FileInformationFlags.SHGFI_SMALLICON) Or If(Overlays, FileInformationFlags.SHGFI_ADDOVERLAYS, CType(0, FileInformationFlags)))
            If shInfo.hIcon = IntPtr.Zero Then Return Nothing
            Dim icon = Drawing.Icon.FromHandle(shInfo.hIcon)
            Return icon
        End Function
        ''' <summary>Gets icon for given file or folder</summary>
        ''' <param name="Path">Path to get icon for</param>
        ''' <param name="Large">True to get large icon (false to get small icon)</param>
        ''' <returns>Icon that represents given file or folder in operating system</returns>
        ''' <param name="Overlays">True to add all applicable overlay icons</param>
        ''' <seelaso cref="Drawing.Icon.ExtractAssociatedIcon"/>
        ''' <exception cref="IO.FileNotFoundException">File <paramref name="Path"/> does not exists.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Path"/> is null</exception>
        Public Function GetIcon(ByVal Path As String, Optional ByVal Large As Boolean = False, Optional ByVal Overlays As Boolean = True) As Drawing.Icon
            If Path Is Nothing Then Throw New ArgumentNullException("Path")
            Return GetIcon(DirectCast(New Path(Path), IPathProvider), Large)
        End Function
        '''' <summary>Gets localized name for file or folder</summary>
        '''' <param name="Path">Path to get loclized name of</param>
        '''' <returns>Localized name of given path; or file name part of path if localized name is not available</returns>
        '''' <remarks>Localized name is available at Windows Vista and later. On systems before Vista <see cref="Path.FileName"/> is always returned.</remarks>
        '''' <exception cref="ArgumentNullException"><paramref name="Path"/> is null</exception>
        '<Extension()> _
        'Public Function GetLocalizedName(ByVal Path As Path) As String
        '    If Path Is Nothing Then Throw New ArgumentNullException("Path")
        '    Dim dExists As New API.FileSystem.dSHGetLocalizedName(AddressOf API.FileSystem.SHGetLocalizedName)
        '    If Not API.Helpers.IsFunctionExported(dExists) Then _
        '        Return Path.FileName
        '    Dim pszResModule As New StringBuilder(1024), cch%
        '    Dim pidsRes As IntPtr
        '    Dim result = API.FileSystem.SHGetLocalizedName(Path.Path, pszResModule, cch, pidsRes)
        '    If result <> 0 Then Throw New API.Win32APIException
        '    'Dim Name = Marshal.PtrToStringBSTR(pszResModule)
        '    'Marshal.FreeBSTR(pszResModule)
        '    Return pszResModule.ToString
        'End Function
        'TODO: GetLocalizedName (reliable!)
        ''' <summary>Shows system file properties dialog for given file or directory</summary>
        ''' <param name="Path">Path to file or directory to show dialog for</param>
        ''' <param name="Owner">Owning window. May be null.</param>
        ''' <param name="WaitForClose">Wait for property dialog to be closed. Note: DIalog is not displayed modally.</param>
        ''' <exception cref="IO.FileNotFoundException">File or directory specified by <paramref name="Path"/> cannot be found</exception>
        ''' <exception cref="IO.DirectoryNotFoundException">Part of path of file or directory specified by <paramref name="Path"/> cannot be found</exception>
        ''' <exception cref="UnauthorizedAccessException">Assecc to file or directory specified by <paramref name="Path"/> is denied</exception>
        ''' <exception cref="API.Win32APIException">Another Win32 error occured</exception>
        ''' <version version="1.5.2">Method added</version>
        <Extension()> _
        Public Sub ShowProperties(ByVal Path As Path, Optional ByVal Owner As IWin32Window = Nothing, Optional ByVal WaitForClose As Boolean = False)
            Dim SEI As New SHELLEXECUTEINFO
            With SEI
                .cbSize = Marshal.SizeOf(SEI)
                .fMask = ShellExecuteInfoFlags.SEE_MASK_INVOKEIDLIST Or ShellExecuteInfoFlags.SEE_MASK_FLAG_NO_UI Or ShellExecuteInfoFlags.SEE_MASK_UNICODE
                If WaitForClose Then .fMask = .fMask Or ShellExecuteInfoFlags.SEE_MASK_NOCLOSEPROCESS Or ShellExecuteInfoFlags.SEE_MASK_NOASYNC
                .lpVerb = "properties"
                .lpFile = Path.Path
                .lpParameters = vbNullString
                .lpDirectory = vbNullString
                .nShow = 0
                .hInstApp = 0
                .lpIDList = 0
                If Owner IsNot Nothing Then .hwnd = Owner.Handle.ToInt32
            End With
            If ShellExecuteEx(SEI) = 0 Then
                Dim LastWin32 = New Win32APIException
                Select Case CType(Marshal.GetLastWin32Error, API.Errors)
                    Case Errors.ERROR_FILE_NOT_FOUND : Throw New IO.FileNotFoundException(LastWin32.Message, LastWin32)
                    Case Errors.ERROR_PATH_NOT_FOUND : Throw New IO.DirectoryNotFoundException(LastWin32.Message, LastWin32)
                    Case Errors.ERROR_ACCESS_DENIED : Throw New UnauthorizedAccessException(LastWin32.Message, LastWin32)
                    Case Else : Throw LastWin32
                End Select
            End If
        End Sub

        ''' <summary>Creates or opens a file or I/O device. The most commonly used I/O devices are as follows: file, file stream, directory, physical disk, volume, console buffer, tape drive, communications resource, mailslot, and pipe.</summary>
        ''' <param name="path">The file to open.</param>
        ''' <param name="mode">A <see cref="System.IO.FileMode" /> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten.</param>
        ''' <param name="access">A <see cref="System.IO.FileAccess" /> value that specifies the operations that can be performed on the file.</param>
        ''' <param name="share">A <see cref="System.IO.FileShare" /> value specifying the type of access other threads have to the file.</param>
        ''' <returns>A <see cref="System.IO.FileStream" /> on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.</returns>
        ''' <exception cref="System.ArgumentException">path is a zero-length string.</exception>
        ''' <exception cref="System.ArgumentNullException">path is null.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurred while opening the file.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">path specified a file that is read-only and access is not Read.-or- path specified a directory.-or- The caller does not have the required permission. -or-mode is <see cref="System.IO.FileMode.Create" /> and the specified file is a hidden file.</exception>
        ''' <exception cref="System.ArgumentOutOfRangeException">mode, access, or share specified an invalid value.</exception>
        ''' <exception cref="System.NotSupportedException">path is in an invalid format.</exception>
        ''' <remarks>Use this method instead of <see cref="IO.File.Open"/> when you need file-like access to something different than file (e.g. NTFS alternate stream or port)</remarks>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Function OpenFile(ByVal path As String, ByVal mode As IO.FileMode, ByVal access As IO.FileAccess, Optional ByVal share As IO.FileShare = IO.FileShare.Read) As IO.FileStream
            If path Is Nothing Then Throw New ArgumentException("path")
            If path = "" Then Throw New ArgumentNullException(ResourcesT.Exceptions.CannotBeAnEmptyString.f(ResourcesT.Exceptions.Path), "path")
            Dim apiAccess As FileSystem.GenericFileAccess = If(access.HasFlag(IO.FileAccess.Read), GenericFileAccess.GENERIC_READ, GenericFileAccess.None) Or
                                                            If(access.HasFlag(IO.FileAccess.Write), GenericFileAccess.GENERIC_WRITE, GenericFileAccess.None)
            Dim h = API.CreateFile(path, apiAccess, share, IntPtr.Zero, If(mode = IO.FileMode.Append, FileCreateDisposition.OPEN_ALWAYS, CInt(mode)))
            If h.IsInvalid Then
                Dim ex = API.Win32APIException.GetLastWin32Exception()
                If TypeOf ex Is IO.IOException Then Throw ex
                If TypeOf ex Is NotSupportedException Then Throw ex
                If TypeOf ex Is UnauthorizedAccessException Then Throw ex
                If TypeOf ex Is ArgumentException Then Throw ex
                If TypeOf ex Is Security.SecurityException Then Throw ex
                Throw New IO.IOException(ex.Message, ex)
            End If
            Dim ret = New IO.FileStream(h, access)
            If mode.HasFlag(IO.FileMode.Append) Then ret.Seek(ret.Length, IO.SeekOrigin.Begin)
            Return ret
        End Function
        ''' <summary>Tests if file, stream, device or something other file-like exists</summary>
        ''' <param name="path">Path to file, device etc. to test</param>
        ''' <returns>True if given device or file exists, false otherwise</returns>
        ''' <remarks>Use this instead of <see cref="IO.File.Exists"/> when you are working with something different than file (e.g. NTFS alternate stream or port)</remarks>
        Public Function TestFileExists(ByVal path$) As Boolean
            Dim attrs = API.GetFileAttributes(path)
            If attrs = INVALID_FILE_ATTRIBUTES Then Return False
            Return True
        End Function
    End Module

End Namespace
#End If