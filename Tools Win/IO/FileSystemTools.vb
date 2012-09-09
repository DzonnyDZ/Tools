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

        ''' <summary>Creates a symbolic link</summary>
        ''' <param name="linkTarget">A file or directory to create symbolic link to</param>
        ''' <param name="linkPath">Path where to create symbolic link</param>
        ''' <param name="isDirectory">
        ''' True if <paramref name="linkTarget"/> represents directory.
        ''' False if <paramref name="linkTarget"/> represents file.
        ''' Null to detect (in this case <paramref name="linkTarget"/> must exist).
        ''' </param>
        ''' <remarks>This operation may require administrator privilegies.</remarks>
        ''' <exception cref="IO.FileNotFoundException"><paramref name="isDirectory"/> is null and <paramref name="linkTarget"/> does not exist.</exception>
        ''' <exception cref="IO.IOException">Failed to create symbolic link</exception>
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="linkTarget"/> or <paramref name="linkPath"/> is null. -or-
        ''' <paramref name="linkTarget"/> is root-relative (\something) or current-working-directoty-relative (X:something) and <paramref name="isDirectory"/> is null. 
        ''' </exception>
        ''' <exception cref="ArgumentException"><paramref name="linkTarget"/> or <paramref name="linkPath"/> is an empty string.</exception>
        ''' <version version="1.5.4">This method is new in version 1.5.4</version>
        Public Sub CreateSymbolicLink(linkTarget$, linkPath$, Optional isDirectory As Boolean? = Nothing)
            If linkTarget Is Nothing Then Throw New ArgumentNullException("linkTarget")
            If linkPath Is Nothing Then Throw New ArgumentNullException("linkPath")
            If linkTarget = "" Then Throw New ArgumentException(ResourcesT.Exceptions.ValueCannotBeEmptyString, "linkTarget")
            If linkPath = "" Then Throw New ArgumentException(ResourcesT.Exceptions.ValueCannotBeEmptyString, "linkPath")

            If isDirectory Is Nothing Then
                Dim check As String
                If linkTarget.StartsWith("\") AndAlso Not linkTarget.StartsWith("\\") Then
                    Throw New ArgumentException("When linkTarget is root-relative (\something) isDirectory cannot be null")
                ElseIf linkTarget Like "[A-Za-z]:" AndAlso linkTarget.Length >= 3 AndAlso linkTarget(2) <> IO.Path.DirectorySeparatorChar AndAlso linkTarget(2) <> IO.Path.AltDirectorySeparatorChar Then
                    Throw New ArgumentException("When linkTarget is working-directory-relative (X:something) isDirectory cannot be null")
                ElseIf IO.Path.IsPathRooted(linkTarget) Then
                    check = linkTarget
                Else
                    check = IO.Path.Combine(IO.Path.GetDirectoryName(linkPath), linkTarget)
                End If
                If IO.File.Exists(check) Then
                    isDirectory = False
                ElseIf IO.Directory.Exists(check) Then
                    isDirectory = True
                Else
                    Throw New IO.FileNotFoundException(ResourcesT.Exceptions.FileNotFound, check)
                End If
            End If
            If Not API.CreateSymbolicLink(linkPath, linkTarget, If(isDirectory, SYMBOLIC_LINK_FLAG.SYMBOLIC_LINK_FLAG_DIRECTORY, SYMBOLIC_LINK_FLAG.File)) Then
                Dim last = Win32APIException.GetLastWin32Exception()
                If TypeOf last Is IO.IOException Then Throw last
                Throw New IO.IOException(If(last Is Nothing, Nothing, last.Message), last)
            End If
        End Sub

        ''' <summary>Creates a hard-link</summary>
        ''' <param name="existingFile">File to point hardlink to</param>
        ''' <param name="newFile">Path to store hardlink into</param>
        ''' <remarks>This operation may require administrator privilegies.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="existingFile"/> or <paramref name="newFile"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="existingFile"/> or <paramref name="newFile"/> is an empty string</exception>
        ''' <exception cref="IO.IOException">Hard link creation failed.</exception>
        ''' <version version="1.5.4">This method is new in version 1.5.4</version>
        Public Sub CreateHardLink(existingFile$, newFile$)
            If existingFile$ Is Nothing Then Throw New ArgumentNullException("existingFile")
            If newFile$ Is Nothing Then Throw New ArgumentNullException("newFile")
            If existingFile$ = "" Then Throw New ArgumentException(ResourcesT.Exceptions.ValueCannotBeEmptyString, "existingFile")
            If newFile$ = "" Then Throw New ArgumentException(ResourcesT.Exceptions.ValueCannotBeEmptyString, "newFile")

            If Not API.CreateHardLink(newFile, existingFile) Then
                Dim last = Win32APIException.GetLastWin32Exception()
                If TypeOf last Is IO.IOException Then Throw last
                Throw New IO.IOException(If(last Is Nothing, Nothing, last.Message), last)
            End If
        End Sub

        ''' <summary>Attempts to get target path of symbolic link</summary>
        ''' <param name="symbolicLink">Path of symbolic link</param>
        ''' <returns>Path symbolic link points to. Null if ptah cannot be obtained.</returns>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Public Function ResolveSymbolicLink(symbolicLink As String) As String
            'Adapted from http://www.codeproject.com/Articles/21202/Reparse-Points-in-Vista
            If symbolicLink Is Nothing Then Throw New ArgumentNullException("symbolicLink")
            If symbolicLink = "" Then Throw New ArgumentException(ResourcesT.Exceptions.ValueCannotBeEmptyString, "symbolicLink")

            Dim success As Boolean
            Dim lastError%
            ' Apparently we need to have backup privileges
            Dim token As IntPtr
            Dim tokenPrivileges = New TOKEN_PRIVILEGES()
            ReDim tokenPrivileges.Privileges(0 To 1)
            success = API.OpenProcessToken(API.GetCurrentProcess(), API.AccessRightsForAccessTokenObjects.TOKEN_ADJUST_PRIVILEGES, token)
            lastError = Marshal.GetLastWin32Error()
            If success Then
                Try
                    success = API.LookupPrivilegeValue(Nothing, API.SE_BACKUP_NAME, tokenPrivileges.Privileges(0).Luid) 'null for local system
                    lastError = Marshal.GetLastWin32Error()
                    If success Then
                        tokenPrivileges.PrivilegeCount = 1
                        tokenPrivileges.Privileges(0).Attributes = API.PrivilegeAttributes.SE_PRIVILEGE_ENABLED
                        success = API.AdjustTokenPrivileges(token, False, tokenPrivileges, Marshal.SizeOf(tokenPrivileges), IntPtr.Zero, IntPtr.Zero)
                        lastError = Marshal.GetLastWin32Error()
                    End If
                Finally
                    CloseHandle(token)
                End Try
            End If

            If success Then
                'Open the file and get its handle
                Using handle = CreateFile(symbolicLink, GenericFileAccess.GENERIC_READ, ShareModes.None, IntPtr.Zero, FileCreateDisposition.OPEN_EXISTING, FileFlagsAndAttributes.FILE_FLAG_OPEN_REPARSE_POINT Or FileFlagsAndAttributes.FILE_FLAG_BACKUP_SEMANTICS, IntPtr.Zero)
                    lastError = Marshal.GetLastWin32Error()
                    If Not handle.IsInvalid Then
                        Dim buffer As REPARSE_DATA_BUFFER = New REPARSE_DATA_BUFFER()
                        ' Make up the control code - see CTL_CODE on ntddk.h
                        Dim controlCode = (API.FileDeviceTypes.FILE_DEVICE_FILE_SYSTEM << 16) Or (API.IOFileAccess.FILE_ANY_ACCESS << 14) Or (API.FSCTL_GET_REPARSE_POINT << 2) Or API.IOMethod.METHOD_BUFFERED
                        Dim bytesReturned As UInt32
                        success = API.DeviceIoControl(handle.DangerousGetHandle, controlCode, IntPtr.Zero, 0, buffer, API.MAXIMUM_REPARSE_DATA_BUFFER_SIZE, bytesReturned, IntPtr.Zero)
                        lastError = Marshal.GetLastWin32Error()
                        If success Then
                            Dim subsString = ""
                            Dim printString = ""
                            'Note that according to http://wesnerm.blogs.com/net_undocumented/2006/10/symbolic_links_.html
                            'Symbolic links store relative paths, while junctions use absolute paths
                            'however, they can in fact be either, and may or may not have a leading \.
                            Debug.Assert(buffer.ReparseTag = API.ReparseTags.IO_REPARSE_TAG_SYMLINK OrElse buffer.ReparseTag = API.ReparseTags.IO_REPARSE_TAG_MOUNT_POINT, "Unrecognised reparse tag")                     'We only recognise these two
                            Dim tag As TagType
                            If (buffer.ReparseTag = API.ReparseTags.IO_REPARSE_TAG_SYMLINK) Then
                                'for some reason symlinks seem to have an extra two characters on the front
                                subsString = New String(buffer.ReparseTarget, (buffer.SubsNameOffset / 2 + 2), buffer.SubsNameLength / 2)
                                printString = New String(buffer.ReparseTarget, (buffer.PrintNameOffset / 2 + 2), buffer.PrintNameLength / 2)
                                tag = TagType.SymbolicLink
                            ElseIf (buffer.ReparseTag = API.ReparseTags.IO_REPARSE_TAG_MOUNT_POINT) Then
                                'This could be a junction or a mounted drive - a mounted drive starts with "\\??\\Volume"
                                subsString = New String(buffer.ReparseTarget, buffer.SubsNameOffset / 2, buffer.SubsNameLength / 2)
                                printString = New String(buffer.ReparseTarget, buffer.PrintNameOffset / 2, buffer.PrintNameLength / 2)
                                tag = If(subsString.StartsWith("\??\Volume"), TagType.MountPoint, TagType.JunctionPoint)
                            End If

                            'the printstring should give us what we want
                            Dim normalisedTarget$
                            If (Not String.IsNullOrEmpty(printString)) Then
                                normalisedTarget = printString
                            Else
                                ' if not we can use the substring with a bit of tweaking
                                normalisedTarget = subsString
                                Debug.Assert(normalisedTarget.Length > 2, "Target string too short")
                                Debug.Assert(
                                    (normalisedTarget.StartsWith("\??\") AndAlso (normalisedTarget(5) = ":"c OrElse normalisedTarget.StartsWith("\??\Volume")) OrElse
                                    (Not normalisedTarget.StartsWith("\??\") AndAlso normalisedTarget(1) <> ":"c)),
                                    "Malformed subsString")
                                'Junction points must be absolute
                                Debug.Assert(buffer.ReparseTag = API.ReparseTags.IO_REPARSE_TAG_SYMLINK OrElse normalisedTarget.StartsWith("\??\Volume") OrElse normalisedTarget(1) = ":"c, "Relative junction point")
                                If (normalisedTarget.StartsWith("\??\")) Then
                                    normalisedTarget = normalisedTarget.Substring(4)
                                End If
                            End If
                            Dim actualTarget = normalisedTarget
                            Return actualTarget
                        End If
                    End If
                End Using
            End If
            Return Nothing
        End Function

        ''' <summary>Enumerates symbolic link types</summary>
        Private Enum TagType
            ''' <summary>Not detected</summary>
            None = 0
            ''' <summary>Volume mount point</summary>
            MountPoint = 1
            ''' <summary>Symbolic link (file link)</summary>
            SymbolicLink = 2
            ''' <summary>Junction (folder link)</summary>
            JunctionPoint = 3
        End Enum
    End Module

End Namespace
#End If