Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports Microsoft.Win32.SafeHandles
Imports System.Text

#If Config <= Nightly Then 'Stage:Nightly
Namespace API
    ''' <summary>Contains API decalarations related to file system</summary>
    Module FileSystem
#Region "Structures"
        ''' <summary>Contains information about a file object.</summary>
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure SHFILEINFO
            ''' <summary>A handle to the icon that represents the file. You are responsible for destroying this handle with DestroyIcon when you no longer need it.</summary>
            Public hIcon As IntPtr
            ''' <summary>The index of the icon image within the system image list.</summary>
            Public iIcon As IntPtr
            ''' <summary>An array of values that indicates the attributes of the file object.</summary>
            Public dwAttributes As FileFlags
            ''' <summary>A string that contains the name of the file as it appears in the Microsoft Windows Shell, or the path and file name of the file that contains the icon representing the file.</summary>
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_PATH)> _
            Public szDisplayName As String
            ''' <summary>A string that describes the type of file.</summary>
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> _
            Public szTypeName As String
        End Structure
#End Region
#Region "Constants"
        ''' <summary>Maximum length of path for the <see cref="SHGetFileInfo"/> function</summary>
        Public Const MAX_PATH As Int32 = 260
#End Region
#Region "Functions"
        ''' <summary>Retrieves information about an object in the file system, such as a file, folder, directory, or drive root.</summary>
        ''' <param name="pszPath">[in] A pointer to a null-terminated string of maximum length <see cref="MAX_PATH"/> that contains the path and file name. Both absolute and relative paths are valid.
        ''' <para>If the <paramref name="uFlags"/> parameter includes the <see cref="FileInformationFlags.SHGFI_PIDL"/> flag, this parameter must be the address of an ITEMIDLIST (PIDL) structure that contains the list of item identifiers that uniquely identifies the file within the Shell's namespace. The pointer to an item identifier list (PIDL) must be a fully qualified PIDL. Relative PIDLs are not allowed.</para>
        ''' <para>If the uFlags parameter includes the <see cref="FileInformationFlags.SHGFI_USEFILEATTRIBUTES"/> flag, this parameter does not have to be a valid file name. The function will proceed as if the file exists with the specified name and with the file attributes passed in the dwFileAttributes parameter. This allows you to obtain information about a file type by passing just the extension for pszPath and passing <see cref="fileattributes.FILE_ATTRIBUTE_NORMAL"/> in dwFileAttributes.</para>
        ''' <para>This string can use either short (the 8.3 form) or long file names.</para>
        ''' </param>
        ''' <param name="dwFileAttributes">[in] A combination of one or more file attribute flags (<see cref="FileAttributes">FILE_ATTRIBUTE_</see> values as defined in Winnt.h). If uFlags does not include the <see cref="FileInformationFlags.shgfi_USEFILEATTRIBUTES"/> flag, this parameter is ignored.</param>
        ''' <param name="psfi">[out] The address of a <see cref="SHFILEINFO"/> structure to receive the file information.</param>
        ''' <param name="cbFileInfo">[in] The size, in bytes, of the <see cref="SHFILEINFO"/> structure pointed to by the psfi parameter.</param>
        ''' <param name="uFlags">[in] The flags that specify the file information to retrieve. This parameter can be a combination of the <see cref="FileInformationFlags">SHGFI_</see>  values.</param>
        ''' <returns>Returns a value whose meaning depends on the uFlags parameter.<para>If uFlags does not contain SHGFI_EXETYPE or SHGFI_SYSICONINDEX, the return value is nonzero if successful, or zero otherwise.</para><para>If uFlags contains the SHGFI_EXETYPE flag, the return value specifies the type of the executable file.</para></returns>
        Public Declare Auto Function SHGetFileInfo Lib "shell32.dll" ( _
            ByVal pszPath As String, _
            ByVal dwFileAttributes As FileAttributes, _
            ByRef psfi As SHFILEINFO, _
            ByVal cbFileInfo As UInt32, _
            ByVal uFlags As FileInformationFlags) As IntPtr
        ''' <summary>Creates or opens a file or I/O device. The most commonly used I/O devices are as follows: file, file stream, directory, physical disk, volume, console buffer, tape drive, communications resource, mailslot, and pipe. The function returns a handle that can be used to access the file or device for various types of I/O depending on the file or device and the flags and attributes specified.</summary>
        ''' <param name="lpFileName">The name of the file or device to be created or opened. </param>
        ''' <param name="dwDesiredAccess">The requested access to the file or device, which can be summarized as read, write, both or neither (zero).
        ''' <para>If this parameter is zero, the application can query certain metadata such as file, directory, or device attributes without accessing that file or device, even if <see cref="GenericFileAccess.GENERIC_READ"/> access would have been denied.</para>
        ''' <para>You cannot request an access mode that conflicts with the sharing mode that is specified by the <paramref name="dwShareMode"/> parameter in an open request that already has an open handle.</para></param>
        ''' <param name="dwShareMode">The requested sharing mode of the file or device, which can be read, write, both, delete, all of these, or none (refer to the following table). Access requests to attributes or extended attributes are not affected by this flag.
        ''' <para>If this parameter is zero and <see cref="CreateFile"/> succeeds, the file or device cannot be shared and cannot be opened again until the handle to the file or device is closed. For more information, see the Remarks section.</para>
        ''' <para>You cannot request a sharing mode that conflicts with the access mode that is specified in an existing request that has an open handle.</para>
        ''' <para>To enable a process to share a file or device while another process has the file or device open, use a compatible combination of one or more of the <see cref="ShareModes"/> values.</para></param>
        ''' <param name="lpSecurityAttributes"><para>A pointer to a <see cref="SECURITY_ATTRIBUTES"/> structure that contains two separate but related data members: an optional security descriptor, and a Boolean value that determines whether the returned handle can be inherited by child processes.</para>
        ''' <para>This parameter can be NULL.</para>
        ''' <para>If this parameter is NULL, the handle returned by CreateFile cannot be inherited by any child processes the application may create and the file or device associated with the returned handle gets a default security descriptor.</para></param>
        ''' <param name="dwCreationDisposition"> <para>An action to take on a file or device that exists or does not exist.</para>
        ''' <para>For devices other than files, this parameter is usually set to <see cref="FileCreateDisposition.OPEN_EXISTING"/>.</para></param>
        ''' <param name="dwFlagsAndAttributes"><para>The file or device attributes and flags, <see cref="FileFlagsAndAttributes.FILE_ATTRIBUTE_NORMAL"/> being the most common default value for files.</para>
        ''' <para>This parameter can include any combination of the available file attributes (FILE_ATTRIBUTE_*). All other file attributes override <see cref="FileFlagsAndAttributes.FILE_ATTRIBUTE_NORMAL"/>.</para>
        ''' <para>This parameter can also contain combinations of flags (FILE_FLAG_*) for control of file or device caching behavior, access modes, and other special-purpose flags. These combine with any FILE_ATTRIBUTE_* values.</para>
        ''' <para>This parameter can also contain Security Quality of Service information by specifying the SECURITY_SQOS_PRESENT flag. Additional SQOS-related flags information is presented in the table following the attributes and flags tables.</para></param>
        ''' <param name="hTemplateFile"> <para>A valid handle to a template file with the GENERIC_READ access right. The template file supplies file attributes and extended attributes for the file that is being created.</para>
        ''' <para>This parameter can be NULL.</para>
        ''' <para>When opening an existing file, CreateFile ignores this parameter.</para>
        ''' <para>When opening a new encrypted file, the file inherits the discretionary access control list from its parent directory. For additional information, see File Encryption</para></param>
        ''' <returns><para>If the function succeeds, the return value is an open handle to the specified file, device, named pipe, or mail slot.</para>
        ''' <para>If the function fails, the return value is <see cref="Common.Errors.INVALID_HANDLE_VALUE"/>.</para></returns>
        Public Declare Auto Function CreateFile Lib "kernel32.dll" ( _
                ByVal lpFileName As String, _
                ByVal dwDesiredAccess As GenericFileAccess, _
                ByVal dwShareMode As ShareModes, _
                ByVal lpSecurityAttributes As IntPtr, _
                Optional ByVal dwCreationDisposition As FileCreateDisposition = FileCreateDisposition.OPEN_EXISTING, _
                Optional ByVal dwFlagsAndAttributes As FileFlagsAndAttributes = FileFlagsAndAttributes.FILE_ATTRIBUTE_NORMAL, _
                Optional ByVal hTemplateFile As Int32 = NULL _
                ) As SafeFileHandle
        ''' <summary>Generic file access modes</summary>
        <Flags()> _
        Public Enum GenericFileAccess As Integer
            ''' <summary>Read, write, and execute access</summary>
            GENERIC_ALL = &H10000000
            ''' <summary>	Read access</summary>
            GENERIC_READ = &H80000000
            ''' <summary>	Write access</summary>
            GENERIC_WRITE = &H40000000
            ''' <summary>	Execute access</summary>
            GENERIC_EXECUTE = &H20000000
            ''' <summary>No access. Allows query some metadata.</summary>
            None = 0
        End Enum
        <Flags()> _
        Public Enum ShareModes As Integer
            ''' <summary><para>Enables subsequent open operations on a file or device to request delete access.</para>
            ''' <para>Otherwise, other processes cannot open the file or device if they request delete access.</para>
            ''' <para>If this flag is not specified, but the file or device has been opened for delete access, the function fails.</para>
            ''' <para>Note  Delete access allows both delete and rename operations.</para></summary>
            FILE_SHARE_DELETE = &H4
            ''' <summary><para>Enables subsequent open operations on a file or device to request read access.</para>
            ''' <para>Otherwise, other processes cannot open the file or device if they request read access.</para>
            ''' <para>If this flag is not specified, but the file or device has been opened for read access, the function fails.</para></summary>
            FILE_SHARE_READ = &H1
            ''' <summary><para>Enables subsequent open operations on a file or device to request write access.</para>
            ''' <para>Otherwise, other processes cannot open the file or device if they request write access.</para>
            ''' <para>If this flag is not specified, but the file or device has been opened for write access or has a file mapping with write access, the function fails.</para></summary>
            FILE_SHARE_WRITE = &H2
            ''' <summary>Prevents other processes from opening a file or device if they request delete, read, or write access.</summary>
            None = 0

        End Enum
        ''' <summary>contains the security descriptor for an object and specifies whether the handle retrieved by specifying this structure is inheritable.</summary>
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure SECURITY_ATTRIBUTES
            ''' <summary>The size, in bytes, of this structure. Set this value to the size of the <see cref="SECURITY_ATTRIBUTES"/> structure.</summary>
            Public nLength As Int32
            ''' <summary>A pointer to a security descriptor for the object that controls the sharing of it. If NULL is specified for this member, the object is assigned the default security descriptor of the calling process. This is not the same as granting access to everyone by assigning a NULL discretionary access control list (DACL). The default security descriptor is based on the default DACL of the access token belonging to the calling process. By default, the default DACL in the access token of a process allows access only to the user represented by the access token. If other users must access the object, you can either create a security descriptor with the appropriate access, or add ACEs to the DACL that grants access to a group of users.</summary>
            Public lpSecurityDescriptor As Int32
            ''' <summary>A Boolean value that specifies whether the returned handle is inherited when a new process is created. If this member is TRUE, the new process inherits the handle.</summary>
            <MarshalAs(UnmanagedType.I4)> Public bInheritHandle As Boolean
        End Structure

        ''' <summary>Actions to take on a file or device that exists or does not exist. </summary>
        Public Enum FileCreateDisposition As Integer
            ''' <summary><para>Creates a new file, always.</para>
            ''' <para>If the specified file exists and is writable, the function overwrites the file, the function succeeds, and last-error code is set to <see cref="Common.Errors.ERROR_ALREADY_EXISTS"/> (183).</para>
            ''' <para>If the specified file does not exist and is a valid path, a new file is created, the function succeeds, and the last-error code is set to zero.</para></summary>
            CREATE_ALWAYS = 2
            ''' <summary><para>Creates a new file, only if it does not already exist.</para>
            ''' <para>If the specified file exists, the function fails and the last-error code is set to <see cref="Common.Errors.ERROR_FILE_EXISTS"/> (80).</para>
            ''' <para>If the specified file does not exist and is a valid path to a writable location, a new file is created.</para></summary>
            CREATE_NEW = 1
            ''' <summary><para>Opens a file, always.</para>
            ''' <para>If the specified file exists, the function succeeds and the last-error code is set to <see cref="Common.Errors.ERROR_ALREADY_EXISTS"/> (183).</para>
            ''' <para>If the specified file does not exist and is a valid path to a writable location, the function creates a file and the last-error code is set to zero.</para></summary>
            OPEN_ALWAYS = 4
            ''' <summary><para>Opens a file or device, only if it exists.</para>
            ''' <para>If the specified file or device does not exist, the function fails and the last-error code is set to <see cref="Common.Errors.ERROR_FILE_NOT_FOUND"/> (2).</para></summary>
            OPEN_EXISTING = 3
            ''' <summary><para>Opens a file and truncates it so that its size is zero bytes, only if it exists.</para>
            ''' <para>If the specified file does not exist, the function fails and the last-error code is set to <see cref="Common.Errors.ERROR_FILE_NOT_FOUND"/> (2).</para>
            ''' <para>The calling process must open the file with the <see cref="GenericFileAccess.GENERIC_WRITE"/> bit set as part of the dwDesiredAccess parameter.</para></summary>
            TRUNCATE_EXISTING = 5
        End Enum

        ''' <summary>Values for the dwFlagsAndAttributes parameter of the <see cref="CreateFile"/> function</summary>
        <Flags()> _
        Public Enum FileFlagsAndAttributes
            ''' <summary>The file should be archived. Applications use this attribute to mark files for backup or removal.</summary>
            FILE_ATTRIBUTE_ARCHIVE = 32
            ''' <summary><para>The file or directory is encrypted. For a file, this means that all data in the file is encrypted. For a directory, this means that encryption is the default for newly created files and subdirectories. For more information, see File Encryption.</para>
            ''' <para>This flag has no effect if <see cref="FILE_ATTRIBUTE_SYSTEM"/> is also specified.</para></summary>
            FILE_ATTRIBUTE_ENCRYPTED = 16384
            ''' <summary>The file is hidden. Do not include it in an ordinary directory listing.</summary>
            FILE_ATTRIBUTE_HIDDEN = 2
            ''' <summary>The file does not have other attributes set. This attribute is valid only if used alone.</summary>
            FILE_ATTRIBUTE_NORMAL = 128
            ''' <summary>The data of a file is not immediately available. This attribute indicates that file data is physically moved to offline storage. This attribute is used by Remote Storage, the hierarchical storage management software. Applications should not arbitrarily change this attribute.</summary>
            FILE_ATTRIBUTE_OFFLINE = 4096
            ''' <summary>The file is read only. Applications can read the file, but cannot write to or delete it.</summary>
            FILE_ATTRIBUTE_READONLY = 1
            ''' <summary>The file is part of or used exclusively by an operating system.</summary>
            FILE_ATTRIBUTE_SYSTEM = 4
            ''' <summary>The file is being used for temporary storage.</summary>
            FILE_ATTRIBUTE_TEMPORARY = 256
            ''' <summary><para>The file is being opened or created for a backup or restore operation. The system ensures that the calling process overrides file security checks when the process has SE_BACKUP_NAME and SE_RESTORE_NAME privileges. For more information, see Changing Privileges in a Token.</para>
            ''' <para>You must set this flag to obtain a handle to a directory. A directory handle can be passed to some functions instead of a file handle. For more information, see the Remarks section.</para></summary>
            FILE_FLAG_BACKUP_SEMANTICS = &H2000000
            ''' <summary><para>The file is to be deleted immediately after all of its handles are closed, which includes the specified handle and any other open or duplicated handles.</para>
            ''' <para>If there are existing open handles to a file, the call fails unless they were all opened with the <see cref="ShareModes.FILE_SHARE_DELETE"/> share mode.</para>
            ''' <para>Subsequent open requests for the file fail, unless the <see cref="ShareModes.FILE_SHARE_DELETE"/> share mode is specified.</para></summary>
            FILE_FLAG_DELETE_ON_CLOSE = &H4000000
            ''' <summary><para>The file or device is being opened with no system caching for data reads and writes. This flag does not affect hard disk caching or memory mapped files.</para>
            ''' <para>There are strict requirements for successfully working with files opened with CreateFile using the FILE_FLAG_NO_BUFFERING flag, for details see File Buffering.</para></summary>
            FILE_FLAG_NO_BUFFERING = &H20000000
            ''' <summary>The file data is requested, but it should continue to be located in remote storage. It should not be transported back to local storage. This flag is for use by remote storage systems.</summary>
            FILE_FLAG_OPEN_NO_RECALL = &H100000
            ''' <summary><para>Normal reparse point processing will not occur; CreateFile will attempt to open the reparse point. When a file is opened, a file handle is returned, whether or not the filter that controls the reparse point is operational.</para>
            ''' <para>This flag cannot be used with the <see cref="FileCreateDisposition.CREATE_ALWAYS"/> flag.</para>
            ''' <para>If the file is not a reparse point, then this flag is ignored.</para>
            ''' <para>For more information, see the Remarks section.</para></summary>
            FILE_FLAG_OPEN_REPARSE_POINT = &H200000
            ''' <summary><para>The file or device is being opened or created for asynchronous I/O.</para>
            ''' <para>When subsequent I/O operations are completed on this handle, the event specified in the OVERLAPPED structure will be set to the signaled state.</para>
            ''' <para>If this flag is specified, the file can be used for simultaneous read and write operations.</para>
            ''' <para>If this flag is not specified, then I/O operations are serialized, even if the calls to the read and write functions specify an OVERLAPPED structure.</para></summary>
            FILE_FLAG_OVERLAPPED = &H40000000
            ''' <summary>Access will occur according to POSIX rules. This includes allowing multiple files with names, differing only in case, for file systems that support that naming. Use care when using this option, because files created with this flag may not be accessible by applications that are written for MS-DOS or 16-bit Windows.</summary>
            FILE_FLAG_POSIX_SEMANTICS = &H100000
            ''' <summary><para>Access is intended to be random. The system can use this as a hint to optimize file caching.</para>
            ''' <para>This flag has no effect if the file system does not support cached I/O and <see cref="FILE_FLAG_NO_BUFFERING"/>.</para></summary>
            FILE_FLAG_RANDOM_ACCESS = &H10000000
            ''' <summary><para>Access is intended to be sequential from beginning to end. The system can use this as a hint to optimize file caching.</para>
            ''' <para>This flag should not be used if read-behind (that is, backwards scans) will be used.</para>
            ''' <para>This flag has no effect if the file system does not support cached I/O and <see cref="FILE_FLAG_NO_BUFFERING"/>.</para></summary>
            FILE_FLAG_SEQUENTIAL_SCAN = &H8000000
            ''' <summary>Write operations will not go through any intermediate cache, they will go directly to disk.</summary>
            FILE_FLAG_WRITE_THROUGH = &H80000000
            ''' <summary>Impersonates a client at the Anonymous impersonation level.</summary>
            SECURITY_ANONYMOUS = SECURITY_IMPERSONATION_LEVEL.SecurityAnonymous << 16
            ''' <summary>The security tracking mode is dynamic. If this flag is not specified, the security tracking mode is static.</summary>
            SECURITY_CONTEXT_TRACKING = &H40000
            ''' <summary>Impersonates a client at the Delegation impersonation level.</summary>
            SECURITY_DELEGATION = SECURITY_IMPERSONATION_LEVEL.SecurityDelegation << 16
            ''' <summary><para>Only the enabled aspects of the client's security context are available to the server. If you do not specify this flag, all aspects of the client's security context are available.</para>
            ''' <para>This allows the client to limit the groups and privileges that a server can use while impersonating the client.</para></summary>
            SECURITY_EFFECTIVE_ONLY = &H80000
            ''' <summary>Impersonates a client at the Identification impersonation level.</summary>
            SECURITY_IDENTIFICATION = SECURITY_IMPERSONATION_LEVEL.SecurityIdentification << 16
            ''' <summary>Impersonate a client at the impersonation level. This is the default behavior if no other flags are specified along with the SECURITY_SQOS_PRESENT flag.</summary>
            SECURITY_IMPERSONATION = SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation << 16
        End Enum
        ''' <summary>contains values that specify security impersonation levels. Security impersonation levels govern the degree to which a server process can act on behalf of a client process.</summary>
        Public Enum SECURITY_IMPERSONATION_LEVEL As Integer
            ''' <summary>The server process cannot obtain identification information about the client, and it cannot impersonate the client. It is defined with no value given, and thus, by ANSI C rules, defaults to a value of zero.</summary>
            SecurityAnonymous
            ''' <summary>The server process can obtain information about the client, such as security identifiers and privileges, but it cannot impersonate the client. This is useful for servers that export their own objects, for example, database products that export tables and views. Using the retrieved client-security information, the server can make access-validation decisions without being able to use other services that are using the client's security context.</summary>
            SecurityIdentification
            ''' <summary>The server process can impersonate the client's security context on its local system. The server cannot impersonate the client on remote systems.</summary>
            SecurityImpersonation
            ''' <summary>The server process can impersonate the client's security context on remote systems. </summary>
            SecurityDelegation
        End Enum
        ''' <summary>Retrieves the localized name of a file in a Shell folder.</summary>
        ''' <param name="pszPath">[in] A pointer to a string that specifies the fully qualified path of the file.</param>
        ''' <param name="pszResModule">[out] When this function returns, contains a pointer to a string resource that specifies the localized version of the file name.</param>
        ''' <param name="cch">[out] When this function returns, contains the size of the string, in WCHARs, at <paramref name="pszResModule"/>.</param>
        ''' <param name="pidsRes">When this function returns, contains a pointer to the ID of the localized file name in the resource file.</param>
        ''' <returns>N/A</returns>
        <DllImport("shell32.dll", CallingConvention:=CallingConvention.Winapi, CharSet:=CharSet.Unicode, EntryPoint:="SHGetLocalizedName", ExactSpelling:=True)> _
        Public Function SHGetLocalizedName(ByVal pszPath As String, ByVal pszResModule As StringBuilder, ByRef cch%, <Out()> ByRef pidsRes As Integer) As Integer
        End Function
        Public Delegate Function dSHGetLocalizedName(ByVal pszPath As String, ByVal pszResModule As StringBuilder, ByRef cch%, ByRef pidsRes As Integer) As Integer

#End Region
#Region "Enumerations"
        ''' <summary>The flags that specify the file information to retrieve. USed by <see cref="SHGetFileInfo"/>.</summary>
        <Flags()> _
        Public Enum FileInformationFlags As UInt32
            <EditorBrowsable(EditorBrowsableState.Advanced)> zeor = 0UI
            ''' <summary>Version 5.0. Apply the appropriate overlays to the file's icon. The <see cref="SHGFI_ICON"/> flag must also be set.</summary>
            SHGFI_ADDOVERLAYS = &H20UI
            ''' <summary>Modify <see cref="SHGFI_ATTRIBUTES"/> to indicate that the dwAttributes member of the <see cref="SHFILEINFO"/> structure at psfi contains the specific attributes that are desired.</summary>
            SHGFI_ATTR_SPECIFIED = &H20000UI
            ''' <summary>Retrieve the item attributes. The attributes are copied to the dwAttributes member of the structure specified in the psfi parameter. These are the same attributes that are obtained from IShellFolder::GetAttributesOf.</summary>
            SHGFI_ATTRIBUTES = &H800UI
            ''' <summary>Retrieve the display name for the file. The name is copied to the szDisplayName member of the structure specified in psfi. The returned display name uses the long file name, if there is one, rather than the 8.3 form of the file name.</summary>
            SHGFI_DISPLAYNAME = &H200UI
            ''' <summary>Retrieve the type of the executable file if pszPath identifies an executable file. The information is packed into the return value. This flag cannot be specified with any other flags.</summary>
            SHGFI_EXETYPE = &H2000UI
            ''' <summary>Retrieve the handle to the icon that represents the file and the index of the icon within the system image list. The handle is copied to the hIcon member of the structure specified by psfi, and the index is copied to the iIcon member.</summary>
            SHGFI_ICON = &H100UI
            ''' <summary>Retrieve the name of the file that contains the icon representing the file specified by pszPath, as returned by the IExtractIcon::GetIconLocation method of the file's icon handler. Also retrieve the icon index within that file. The name of the file containing the icon is copied to the szDisplayName member of the structure specified by psfi. The icon's index is copied to that structure's iIcon member.</summary>
            SHGFI_ICONLOCATION = &H1000UI
            ''' <summary>Modify SHGFI_ICON, causing the function to retrieve the file's large icon. The <see cref="SHGFI_ICON"/> flag must also be set.</summary>
            SHGFI_LARGEICON = &H0UI
            ''' <summary>Modify <see cref="SHGFI_ICON"/>, causing the function to add the link overlay to the file's icon. The <see cref="SHGFI_ICON"/> flag must also be set.</summary>
            SHGFI_LINKOVERLAY = &H8000UI
            ''' <summary>Modify <see cref="SHGFI_ICON"/>, causing the function to retrieve the file's open icon. Also used to modify <see cref="SHGFI_SYSICONINDEX"/>, causing the function to return the handle to the system image list that contains the file's small open icon. A container object displays an open icon to indicate that the container is open. The <see cref="SHGFI_ICON"/> and/or <see cref="SHGFI_SYSICONINDEX"/> flag must also be set.</summary>
            SHGFI_OPENICON = &H2UI
            ''' <summary>Version 5.0. Return the index of the overlay icon. The value of the overlay index is returned in the upper eight bits of the iIcon member of the structure specified by psfi. This flag requires that the <see cref="SHGFI_ICON"/> be set as well.</summary>
            SHGFI_OVERLAYINDEX = &H40UI
            ''' <summary>Indicate that pszPath is the address of an ITEMIDLIST structure rather than a path name.</summary>
            SHGFI_PIDL = &H8UI
            ''' <summary>Modify <see cref="SHGFI_ICON"/>, causing the function to blend the file's icon with the system highlight color. The <see cref="SHGFI_ICON"/> flag must also be set.</summary>
            SHGFI_SELECTED = &H10000UI
            ''' <summary>Modify <see cref="SHGFI_ICON"/>, causing the function to retrieve a Shell-sized icon. If this flag is not specified the function sizes the icon according to the system metric values. The <see cref="SHGFI_ICON"/> flag must also be set.</summary>
            SHGFI_SHELLICONSIZE = &H4UI
            ''' <summary>Modify <see cref="SHGFI_ICON"/>, causing the function to retrieve the file's small icon. Also used to modify <see cref="SHGFI_SYSICONINDEX"/>, causing the function to return the handle to the system image list that contains small icon images. The <see cref="SHGFI_ICON"/> and/or <see cref="SHGFI_SYSICONINDEX"/> flag must also be set.</summary>
            SHGFI_SMALLICON = &H1UI
            ''' <summary>Retrieve the index of a system image list icon. If successful, the index is copied to the iIcon member of psfi. The return value is a handle to the system image list. Only those images whose indices are successfully copied to iIcon are valid. Attempting to access other images in the system image list will result in undefined behavior.</summary>
            SHGFI_SYSICONINDEX = &H4000UI
            ''' <summary>Retrieve the string that describes the file's type. The string is copied to the szTypeName member of the structure specified in psfi.</summary>
            SHGFI_TYPENAME = &H400UI
            ''' <summary>Indicates that the function should not attempt to access the file specified by pszPath. Rather, it should act as if the file specified by pszPath exists with the file attributes passed in dwFileAttributes. This flag cannot be combined with the <see cref="SHGFI_ATTRIBUTES"/>, <see cref="SHGFI_EXETYPE"/>, or <see cref="SHGFI_PIDL"/> flags.</summary>
            SHGFI_USEFILEATTRIBUTES = &H10UI
        End Enum

        ''' <summary>File attributes</summary>
        <Flags()> _
        Public Enum FileAttributes As UInt32
            ''' <summary>A file or directory that is an archive file or directory. Applications use this attribute to mark files for backup or removal.</summary>
            FILE_ATTRIBUTE_ARCHIVE = &H20
            ''' <summary>A file or directory that is compressed. For a file, all of the data in the file is compressed. For a directory, compression is the default for newly created files and subdirectories.</summary>
            FILE_ATTRIBUTE_COMPRESSED = &H800
            ''' <summary>Reserved; do not use.</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> FILE_ATTRIBUTE_DEVICE = &H40
            ''' <summary>The handle that identifies a directory.</summary>
            FILE_ATTRIBUTE_DIRECTORY = &H10
            ''' <summary>A file or directory that is encrypted. For a file, all data streams in the file are encrypted. For a directory, encryption is the default for newly created files and subdirectories.</summary>
            FILE_ATTRIBUTE_ENCRYPTED = &H4000
            ''' <summary>The file or directory is hidden. It is not included in an ordinary directory listing.</summary>
            FILE_ATTRIBUTE_HIDDEN = &H2
            ''' <summary>A file or directory that does not have other attributes set. This attribute is valid only when used alone.</summary>
            FILE_ATTRIBUTE_NORMAL = &H80
            ''' <summary>The file is not to be indexed by the content indexing service.</summary>
            FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = &H2000
            ''' <summary>The data of a file is not available immediately. This attribute indicates that the file data is physically moved to offline storage. This attribute is used by Remote Storage, which is the hierarchical storage management software. Applications should not arbitrarily change this attribute.</summary>
            FILE_ATTRIBUTE_OFFLINE = &H1000
            ''' <summary>A file or directory that is read-only. For a file, applications can read the file, but cannot write to it or delete it. For a directory, applications cannot delete it.</summary>
            FILE_ATTRIBUTE_READONLY = &H1
            ''' <summary>A file or directory that has an associated reparse point, or a file that is a symbolic link.</summary>
            FILE_ATTRIBUTE_REPARSE_POINT = &H400
            ''' <summary>A file that is a sparse file.</summary>
            FILE_ATTRIBUTE_SPARSE_FILE = &H200
            ''' <summary>A file or directory that the operating system uses a part of, or uses exclusively.</summary>
            FILE_ATTRIBUTE_SYSTEM = &H4
            ''' <summary>A file that is being used for temporary storage. File systems avoid writing data back to mass storage if sufficient cache memory is available, because typically, an application deletes a temporary file after the handle is closed. In that scenario, the system can entirely avoid writing the data. Otherwise, the data is written after the handle is closed.</summary>
            FILE_ATTRIBUTE_TEMPORARY = &H100
            ''' <summary>A file is a virtual file.</summary>
            FILE_ATTRIBUTE_VIRTUAL = &H10000
        End Enum

        ''' <summary>Valid drop-effect values are the result of applying the OR operation to the values contained in the <see cref="DropEfects">DROPEFFECT</see> enumeration</summary>
        <Flags()> _
        Public Enum DropEfects As UInt32
            ''' <summary>Drop results in a copy. The original data is untouched by the drag source.</summary>
            DROPEFFECT_COPY = 1UI
            ''' <summary>Drag source should create a link to the original data.</summary>
            DROPEFFECT_LINK = 4UI
            ''' <summary>Drag source should remove the data.</summary>
            DROPEFFECT_MOVE = 2UI
            ''' <summary>Drop target cannot accept the data.</summary>
            DROPEFFECT_NONE = 0UI
            ''' <summary>Scrolling is about to start or is currently occurring in the target. This value is used in addition to the other values.</summary>
            DROPEFFECT_SCROLL = &H80000000UI
        End Enum
        ''' <summary>File flags</summary>
        <Flags()> _
        Public Enum FileFlags As UInt32
            ''' <summary>The specified items can be browsed in place. This implies that the client can bind to this object as shown in a general form here.</summary>
            SFGAO_BROWSABLE = &H8000000UI
            ''' <summary>The specified items can be copied (same value as the <see cref="DropEfects.DROPEFFECT_COPY"/>) flag).</summary>
            SFGAO_CANCOPY = DropEfects.DROPEFFECT_COPY
            ''' <summary>The specified items can be deleted by selecting Delete from their context menus.</summary>
            SFGAO_CANDELETE = &H20UI
            ''' <summary>Shortcuts can be created for the specified items. This flag has the same value as <see cref="DropEfects.DROPEFFECT_LINK"/>. The normal use of this flag is to add a Create Shortcut item to the shortcut menu that is displayed during drag-and-drop operations. However, SFGAO_CANLINK also adds a Create Shortcut item to the Microsoft Windows Explorer's File menu and to normal shortcut menus. If this item is selected, your application's IContextMenu::InvokeCommand is invoked with the lpVerb member of the CMINVOKECOMMANDINFO structure set to "link." Your application is responsible for creating the link.</summary>
            SFGAO_CANLINK = DropEfects.DROPEFFECT_LINK
            ''' <summary>It is possible to create monikers for the specified items.</summary>
            SFGAO_CANMONIKER = &H400000UI
            ''' <summary>The specified items can be moved (same value as the <see cref="DropEfects.DROPEFFECT_MOVE"/> flag).</summary>
            SFGAO_CANMOVE = DropEfects.DROPEFFECT_MOVE
            ''' <summary>The specified items can be renamed. Note that this flag is essentially a suggestion. It does not guarantee that a namespace client will rename the file or folder object.</summary>
            SFGAO_CANRENAME = &H10UI
            ''' <summary>Do not use.</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> SFGAO_CAPABILITYMASK = &H177UI
            ''' <summary>The specified items are compressed.</summary>
            SFGAO_COMPRESSED = &H4000000UI
            ''' <summary>Do not use.</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> SFGAO_CONTENTSMASK = &H80000000UI
            ''' <summary>Do not use.</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> SFGAO_DISPLAYATTRMASK = &HF0000UI
            ''' <summary>The specified items are drop targets.</summary>
            SFGAO_DROPTARGET = &H100UI
            ''' <summary>The item is encrypted and may require special presentation.</summary>
            SFGAO_ENCRYPTED = &H2000UI
            ''' <summary>The specified folder objects are either file system folders or have at least one descendant (child, grandchild, or later) that is a file system folder.</summary>
            SFGAO_FILESYSANCESTOR = &H10000000UI
            ''' <summary>The specified items are part of the file system (that is, they are files, directories, or root directories). The parsed names of the items can be assumed to be valid Win32 file system paths. These paths can be either Universal Naming Convention (UNC) or drive-letter based.</summary>
            SFGAO_FILESYSTEM = &H40000000UI
            ''' <summary>The specified items are folders. Some items can be flagged with both <see cref="SFGAO_STREAM"/> and <see cref="SFGAO_FOLDER"/>, such as a .zip file. Some applications may include this flag when testing for items that are both files and containers.</summary>
            SFGAO_FOLDER = &H20000000UI
            ''' <summary>The specified items should be displayed using a ghosted icon.</summary>
            SFGAO_GHOSTED = &H80000UI
            ''' <summary>The specified items have property sheets.</summary>
            SFGAO_HASPROPSHEET = &H40UI
            ''' <summary>Not supported.</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> SFGAO_HASSTORAGE = &H400000UI
            ''' <summary>The specified folder objects may have subfolders and are, therefore, expandable in the left pane of Windows Explorer.</summary>
            ''' <remarks>Note  The SFGAO_HASSUBFOLDER attribute is only advisory and may be returned by Shell folder implementations even if they do not contain subfolders. Note, however, that the converse—failing to return SFGAO_HASSUBFOLDER—definitively states that the folder objects do not have subfolders.<para>Returning SFGAO_HASSUBFOLDER is recommended whenever a significant amount of time is required to determine whether or not any subfolders exist. For example, the Shell always returns SFGAO_HASSUBFOLDER when a folder is located on a network drive.</para></remarks>
            SFGAO_HASSUBFOLDER = &H80000000UI
            ''' <summary>The item is hidden and should not be displayed unless the Show hidden files and folders option is enabled in Folder Settings.</summary>
            SFGAO_HIDDEN = &H80000UI
            ''' <summary>Indicates that accessing the object (through IStream or other storage interfaces) is a slow operation. Applications should avoid accessing items flagged with <see cref="SFGAO_ISSLOW"/>.</summary>
            SFGAO_ISSLOW = &H4000UI
            ''' <summary>The specified items are shortcuts.</summary>
            SFGAO_LINK = &H10000UI
            ''' <summary>The specified items contain new content.</summary>
            SFGAO_NEWCONTENT = &H200000UI
            ''' <summary>The specified items are nonenumerated items. That is, they are not returned by the enumerator created by the IShellFolder::EnumObjects method.</summary>
            SFGAO_NONENUMERATED = &H100000UI
            ''' <summary>Mask for PKEY_SFGAOFlags attributes such as SFGAO_VALIDATE, SFGAO_ISSLOW, and SFGAO_HASSUBFOLDER. They are considered to cause slow calculations or lack context.</summary>
            SFGAO_PKEYSFGAOMASK = &H81044000UI
            ''' <summary>The specified items are read-only. In the case of folders, this means that new items cannot be created in those folders. </summary>
            [SFGAO_READONLY] = &H40000UI
            ''' <summary>The specified items are on removable media or are themselves removable devices.</summary>
            SFGAO_REMOVABLE = &H2000000UI
            ''' <summary>The specified folder objects are shared.</summary>
            SFGAO_SHARE = &H20000UI
            ''' <summary>The item can be bound to an IStorage interface through IShellFolder::BindToObject.</summary>
            SFGAO_STORAGEANCESTOR = &H10000000UI
            ''' <summary>This flag is a mask for the storage capability attributes.</summary>
            SFGAO_STORAGECAPMASK = &H70C50008UI
            ''' <summary>Indicates that the item has a stream associated with it that can be accessed by a call to IShellFolder::BindToObject with IID_IStream in the riid parameter. The pbc parameter in that same call provides the IBindCtx interface that specifies the access mode, such as read-only or read-write. Some items can be flagged with both SFGAO_STREAM and SFGAO_FOLDER, such as a .zip file. Some applications may include this flag when testing for items that are both files and containers.</summary>
            SFGAO_STREAM = &H400000UI
            ''' <summary>When specified as input, SFGAO_VALIDATE instructs the folder to validate that the items pointed to by the contents of apidl exist. If one or more of those items do not exist, IShellFolder::GetAttributesOf returns a failure code. When used with the file system folder, SFGAO_VALIDATE instructs the folder to discard cached properties retrieved by clients of IShellFolder2::GetDetailsEx that may have accumulated for the specified items.</summary>
            SFGAO_VALIDATE = &H1000000UI
        End Enum
#End Region


        'ASAP:MSDN, Sort to regions
        ''' <summary>Performs an operation on a specified file.</summary>
        ''' <param name="lpExecInfo">The address of a <see cref="SHELLEXECUTEINFO"/> structure that contains and receives information about the application being executed. </param>
        ''' <returns>Returns TRUE if successful, or FALSE otherwise. Call GetLastError for error information. </returns>
        Public Declare Auto Function ShellExecuteEx Lib "shell32.dll" (ByRef lpExecInfo As SHELLEXECUTEINFO) As Integer
        ''' <summary>Contains information used by <see cref="ShellExecuteEx"/>.</summary>
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure SHELLEXECUTEINFO
            ''' <summary>The size of the structure, in bytes.</summary>
            Dim cbSize As Integer
            ''' <summary>An array of flags that indicate the content and validity of the other structure members.</summary>
            Dim fMask As ShellExecuteInfoFlags
            ''' <summary>A window handle to any message boxes that the system might produce while executing this function.</summary>
            Dim hwnd As Integer
            ''' <summary>A string, referred to as a verb, that specifies the action to be performed. The set of available verbs depends on the particular file or folder. Generally, the actions available from an object's shortcut menu are available verbs. If you set this parameter to NULL:
            ''' <list type="bullet"><item>For systems prior to Windows 2000, the default verb is used if it is valid and available in the registry. If not, the "open" verb is used.</item>
            ''' <item>For Windows 2000 and later systems, the default verb is used if available. If not, the "open" verb is used. If neither verb is available, the system uses the first verb listed in the registry.</item></list></summary>
            ''' <remarks>The following verbs are commonly used.
            ''' <list type="table"><item><term>edit</term><description>Launches an editor and opens the document for editing. If lpFile is not a document file, the function will fail.</description></item>
            ''' <item><term>explore</term><description>Explores the folder specified by lpFile.</description></item>
            ''' <item><term>find</term><description>Initiates a search starting from the specified directory.</description></item>
            ''' <item><term>open</term><description>Opens the file specified by the lpFile parameter. The file can be an executable file, a document file, or a folder.</description></item>
            ''' <item><term>print</term><description>Prints the document file specified by lpFile. If lpFile is not a document file, the function will fail.</description></item>
            ''' <item><term>properties</term><description>Displays the file or folder's properties.</description></item></list></remarks>
            <MarshalAs(UnmanagedType.LPTStr)> _
            Dim lpVerb As String
            ''' <summary>The address of a null-terminated string that specifies the name of the file or object on which <see cref="ShellExecuteEx"/> will perform the action specified by the lpVerb parameter. The system registry verbs that are supported by the <see cref="ShellExecuteEx"/> function include "open" for executable files and document files and "print" for document files for which a print handler has been registered. Other applications might have added Shell verbs through the system registry, such as "play" for .avi and .wav files. To specify a Shell namespace object, pass the fully qualified parse name and set the <see cref="ShellExecuteInfoFlags.SEE_MASK_INVOKEIDLIST"/> flag in the <see cref="fMask"/> parameter.</summary>
            ''' <remarks><note>Note If the <see cref="ShellExecuteInfoFlags.SEE_MASK_INVOKEIDLIST"/> flag is set, you can use either lpFile or lpIDList to identify the item by its file system path or its PIDL respectively.</note><note>Note If the path is not included with the name, the current directory is assumed.</note></remarks>
            <MarshalAs(UnmanagedType.LPTStr)> _
            Dim lpFile As String
            ''' <summary>The address of a null-terminated string that contains the application parameters. The parameters must be separated by spaces. If the <see cref="lpFile"/> member specifies a document file, <see cref="lpParameters"/> should be NULL.</summary>
            <MarshalAs(UnmanagedType.LPTStr)> _
            Dim lpParameters As String
            ''' <summary>The address of a null-terminated string that specifies the name of the working directory. If this member is not specified, the current directory is used as the working directory.</summary>
            <MarshalAs(UnmanagedType.LPTStr)> _
            Dim lpDirectory As String
            ''' <summary>Flags that specify how an application is to be shown when it is opened. It can be one of the SW_ values listed for the ShellExecute function. If lpFile specifies a document file, the flag is simply passed to the associated application. It is up to the application to decide how to handle it.</summary>
            Dim nShow As Integer
            ''' <summary>If the function succeeds, it sets this member to a value greater than 32. If the function fails, it is set to an <see cref="ShellExecuteErrors"/> error value that indicates the cause of the failure. Although <see cref="hInstApp"/> is declared as an HINSTANCE for compatibility with 16-bit Windows applications, it is not a true HINSTANCE. It can be cast only to an int and compared to either 32 or the <see cref="ShellExecuteErrors"/>.</summary>
            Dim hInstApp As ShellExecuteErrors
            ''' <summary>The address of an ITEMIDLIST structure to contain an item identifier list uniquely identifying the file to execute. This member is ignored if the <see cref="fMask"/> member does not include <see cref="ShellExecuteInfoFlags.SEE_MASK_IDLIST"/> or <see cref="ShellExecuteInfoFlags.SEE_MASK_INVOKEIDLIST"/>.</summary>
            Dim lpIDList As Integer
            ''' <summary>The address of a null-terminated string that specifies the name of a file class or a GUID. This member is ignored if <see cref="fMask"/> does not include <see cref="ShellExecuteInfoFlags.SEE_MASK_CLASSNAME"/>.</summary>
            <MarshalAs(UnmanagedType.LPTStr)> _
            Dim lpClass As String
            ''' <summary>A handle to the registry key for the file class. This member is ignored if <see cref="fMask"/> does not include <see cref="ShellExecuteInfoFlags.SEE_MASK_CLASSKEY"/>.</summary>
            Dim hkeyClass As Integer
            ''' <summary>A keyboard shortcut to associate with the application. The low-order word is the virtual key code, and the high-order word is a modifier flag (HOTKEYF_). For a list of modifier flags, see the description of the <see cref="Messages.WindowMessages.WM_SETHOTKEY"/> message. This member is ignored if <see cref="fMask"/> does not include <see cref="ShellExecuteInfoFlags.SEE_MASK_HOTKEY"/>.</summary>
            Dim dwHotKey As Integer
            ''' <summary>A handle to the icon for the file class. This member is ignored if <see cref="fMask"/> does not include <see cref="ShellExecuteInfoFlags.SEE_MASK_ICON"/>.</summary>
            ''' <remarks>This value is same as <see cref="hMonitor"/></remarks>
            Dim hIcon As Integer
            ''' <summary>A handle to the monitor upon which the document is to be displayed. This member is ignored if <see cref="fMask"/> does not include <see cref="ShellExecuteInfoFlags.SEE_MASK_HMONITOR"/>.</summary>
            ''' <remarks>This value is same as <see cref="hIcon"/></remarks>
            Public Property hMonitor() As Integer
                Get
                    Return hIcon
                End Get
                Set(ByVal value As Integer)
                    hIcon = value
                End Set
            End Property
            ''' <summary>A handle to the newly started application. This member is set on return and is always NULL unless <see cref="fMask"/> is set to <see cref="ShellExecuteInfoFlags.SEE_MASK_NOCLOSEPROCESS"/>. Even if <see cref="fMask"/> is set to <see cref="ShellExecuteInfoFlags.SEE_MASK_NOCLOSEPROCESS"/>, <see cref="hProcess"/> will be NULL if no process was launched. For example, if a document to be launched is a URL and an instance of Internet Explorer is already running, it will display the document. No new process is launched, and <see cref="hProcess"/> will be NULL.</summary>
            ''' <remarks><note><see cref="ShellExecuteEx"/> does not always return an <see cref="hProcess"/>, even if a process is launched as the result of the call. For example, an <see cref="hProcess"/> does not return when you use <see cref="ShellExecuteInfoFlags.SEE_MASK_INVOKEIDLIST"/> to invoke IContextMenu.</note></remarks>
            Dim hProcess As Integer
        End Structure
        ''' <summary><see cref="SHELLEXECUTEINFO"/> errors</summary>
        Public Enum ShellExecuteErrors As Integer
            ''' <summary>File not found.</summary>
            SE_ERR_FNF = 2
            ''' <summary>Path not found.</summary>
            SE_ERR_PNF = 3
            ''' <summary>Access denied.</summary>
            SE_ERR_ACCESSDENIED = 5
            ''' <summary>Out of memory.</summary>
            SE_ERR_OOM = 8
            ''' <summary>Dynamic-link library not found.</summary>
            SE_ERR_DLLNOTFOUND = 32
            ''' <summary>Cannot share an open file.</summary>
            SE_ERR_SHARE = 26
            ''' <summary>File association information not complete.</summary>
            SE_ERR_ASSOCINCOMPLETE = 27
            ''' <summary>DDE operation timed out.</summary>
            SE_ERR_DDETIMEOUT = 28
            ''' <summary>DDE operation failed.</summary>
            SE_ERR_DDEFAIL = 29
            ''' <summary>DDE operation is busy.</summary>
            SE_ERR_DDEBUSY = 30
            ''' <summary>File association not available.</summary>
            SE_ERR_NOASSOC = 31
        End Enum
        ''' <summary><see cref="SHELLEXECUTEINFO"/> flags</summary>
        Public Enum ShellExecuteInfoFlags As Integer
            ''' <summary>Use the class name given by the lpClass member. If both <see cref="SEE_MASK_CLASSKEY"/> and <see cref="SEE_MASK_CLASSNAME"/> are set, the class key is used.</summary>
            SEE_MASK_CLASSNAME = &H1
            ''' <summary>Use the class key given by the hkeyClass member. If both <see cref="SEE_MASK_CLASSKEY"/> and <see cref="SEE_MASK_CLASSNAME"/> are set, the class key is used.</summary>
            SEE_MASK_CLASSKEY = &H3
            ''' <summary>Use the item identifier list given by the <see cref="SHELLEXECUTEINFO.lpIDList"/> member. The <see cref="SHELLEXECUTEINFO.lpIDList"/> member must point to an ITEMIDLIST structure.</summary>
            SEE_MASK_IDLIST = &H4
            ''' <summary>Use the IContextMenu interface of the selected item's shortcut menu handler. Use either <see cref="SHELLEXECUTEINFO.lpFile"/> to identify the item by its file system path or lpIDList to identify the item by its pointer to an item identifier list (PIDL). This flag allows applications to use <see cref="ShellExecuteEx"/> to invoke verbs from shortcut menu extensions instead of the static verbs listed in the registry.</summary>
            ''' <remarks><note><see cref="SEE_MASK_INVOKEIDLIST"/> overrides <see cref="SEE_MASK_IDLIST"/>.</note></remarks>
            SEE_MASK_INVOKEIDLIST = &HC
            ''' <summary>Use the icon given by the <see cref="SHELLEXECUTEINFO.hIcon"/> member. This flag cannot be combined with <see cref="SEE_MASK_HMONITOR"/>.</summary>
            ''' <remarks><note>Note  This flag is available only in Microsoft Windows XP and earlier. It is not available in Windows Vista and later versions of Windows.</note></remarks>
            SEE_MASK_ICON = &H10
            ''' <summary>Use the keyboard shortcut given by the <see cref="SHELLEXECUTEINFO.dwHotKey"/> member.</summary>
            SEE_MASK_HOTKEY = &H20
            ''' <summary>Use to indicate that the <see cref="SHELLEXECUTEINFO.hProcess"/> member receives the process handle. This handle is typically used to allow an application to find out when a process created with <see cref="ShellExecuteEx"/> terminates. In some cases, such as when execution is satisfied through a Dynamic Data Exchange (DDE) conversation, no handle will be returned. The calling application is responsible for closing the handle when it is no longer needed.</summary>
            SEE_MASK_NOCLOSEPROCESS = &H40
            ''' <summary>Validate the share and connect to a drive letter. The <see cref="SHELLEXECUTEINFO.lpFile"/> member is a Universal Naming Convention (UNC) path of a file on a network.</summary>
            SEE_MASK_CONNECTNETDRV = &H80
            ''' <summary>Wait for the execute operation to complete before returning. This flag should be used by callers that are using ShellExecute forms that might result in an async activation, for example DDE, and create a process that might be run on a background thread. (Note: <see cref="ShellExecuteEx"/> runs on a background thread by default if the caller's threading model is not Apartment.) Calls to <see cref="ShellExecuteEx"/> from processes already running on background threads should always pass this flag. Also, applications that exit immediately after calling <see cref="ShellExecuteEx"/> should specify this flag.</summary>
            ''' <remarks>If the execute operation is performed on a background thread and the caller did not specify the <see cref="SEE_MASK_ASYNCOK"/> flag, then the calling thread waits until the new process has started before returning. This typically means that either CreateProcess has been called, the DDE communication has completed, or that the custom execution delegate has notified <see cref="ShellExecuteEx"/> that it is done. If the <see cref="SEE_MASK_WAITFORINPUTIDLE"/> flag is specified, then <see cref="ShellExecuteEx"/> calls WaitForInputIdle and waits for the new process to idle before returning, with a maximum timeout of 1 minute.</remarks>
            SEE_MASK_NOASYNC = &H100
            ''' <summary>Do not use; use <see cref="SEE_MASK_NOASYNC"/> instead.</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            SEE_MASK_FLAG_DDEWAIT = &H100
            ''' <summary>Expand any environment variables specified in the string given by the <see cref="SHELLEXECUTEINFO.lpDirectory"/> or <see cref="SHELLEXECUTEINFO.lpFile"/> member.</summary>
            SEE_MASK_DOENVSUBST = &H200
            ''' <summary> Do not display an error message box if an error occurs.</summary>
            SEE_MASK_FLAG_NO_UI = &H400
            ''' <summary>Use this flag to indicate a Unicode application.</summary>
            SEE_MASK_UNICODE = &H4000
            ''' <summary>Use to create a console for the new process instead of having it inherit the parent's console. It is equivalent to using a CREATE_NEW_CONSOLE flag with CreateProcess.</summary>
            SEE_MASK_NO_CONSOLE = &H8000
            ''' <summary>Microsoft Windows NT 4.0Service Pack 6 (SP6), Windows 2000 Service Pack 3 (SP3) and later. The execution can be performed on a background thread and the call should return immediately without waiting for the background thread to finish. Note that in certain cases <see cref="ShellExecuteEx"/> ignores this flag and waits for the process to finish before returning.</summary>
            SEE_MASK_ASYNCOK = &H100000
            ''' <summary>Windows Internet Explorer 5.0 and later. Not used.</summary>
            SEE_MASK_NOQUERYCLASSSTORE = &H1000000
            ''' <summary>Use this flag when specifying a monitor on multi-monitor systems. The monitor is specified in the hMonitor member. This flag cannot be combined with <see cref="SEE_MASK_ICON"/>.</summary>
            SEE_MASK_HMONITOR = &H200000
            ''' <summary>Windows XP Service Pack 1 (SP1) and later. Do not perform a zone check. This flag allows <see cref="ShellExecuteEx"/> to bypass zone checking put into place by IAttachmentExecute.</summary>
            SEE_MASK_NOZONECHECKS = &H800000
            ''' <summary>Internet Explorer 5.0 and later. After the new process is created, wait for the process to become idle before returning, with a one minute timeout. See WaitForInputIdle for more details.</summary>
            SEE_MASK_WAITFORINPUTIDLE = &H2000000
            ''' <summary>Windows XP and later. Keep track of the number of times this application has been launched. Applications with sufficiently high counts appear in the Start Menu's list of most frequently used programs.</summary>
            SEE_MASK_FLAG_LOG_USAGE = &H4000000
        End Enum
        ''' <summary>retrieves information about each shared resource on a server.</summary>
        ''' <param name="servername">Pointer to a string that specifies the DNS or NetBIOS name of the remote server on which the function is to execute. If this parameter is NULL, the local computer is used.</param>
        ''' <param name="level">Specifies the information level of the data.</param>
        ''' <param name="bufptr">Pointer to the buffer that receives the data. The format of this data depends on the value of the <paramref name="level"/> parameter. 
        ''' <para>This buffer is allocated by the system and must be freed using the <see cref="NetApiBufferFree"/> function. Note that you must free the buffer even if the function fails with ERROR_MORE_DATA.</para></param>
        ''' <param name="prefmaxlen">Specifies the preferred maximum length of returned data, in bytes. If you specify <see cref="MAX_PREFERRED_LENGTH"/>, the function allocates the amount of memory required for the data. If you specify another value in this parameter, it can restrict the number of bytes that the function returns. If the buffer size is insufficient to hold all entries, the function returns ERROR_MORE_DATA. For more information, see Network Management Function Buffers and Network Management Function Buffer Lengths.</param>
        ''' <param name="entriesread">Pointer to a value that receives the count of elements actually enumerated.</param>
        ''' <param name="totalentries">Pointer to a value that receives the total number of entries that could have been enumerated. Note that applications should consider this value only as a hint.</param>
        ''' <param name="resume_handle">Pointer to a value that contains a resume handle which is used to continue an existing share search. The handle should be zero on the first call and left unchanged for subsequent calls. If resume_handle is NULL, then no resume handle is stored.</param>
        ''' <returns>If the function succeeds, the return value is NERR_Success. If the function fails, the return value is a system error code.</returns>
        Public Declare Unicode Function NetShareEnum Lib "netapi32.dll" ( _
            <MarshalAs(UnmanagedType.LPWStr)> ByVal servername As String, _
            ByVal level As NetShareLevel, _
            ByRef bufptr As IntPtr, _
            ByVal prefmaxlen As Integer, _
            ByRef entriesread As Integer, _
            ByRef totalentries As Integer, _
            ByRef resume_handle As Integer _
            ) As Integer
        ''' <summary>Maximum value for prefferred data length of the <see cref="NetShareEnum"/> function</summary>
        Public Const MAX_PREFERRED_LENGTH% = &HFFFFFFFF
        ''' <summary>Levels for the <see cref="NetShareEnum"/> function</summary>
        Public Enum NetShareLevel As Integer
            ''' <summary>Return share names. The bufptr parameter points to an array of SHARE_INFO_0 structures.</summary>
            Names = 0
            ''' <summary>Return information about shared resources, including the name and type of the resource, and a comment associated with the resource. The bufptr parameter points to an array of SHARE_INFO_1 structures. </summary>
            Resources = 1
            ''' <summary>Return information about shared resources, including name of the resource, type and permissions, password, and number of connections. The bufptr parameter points to an array of SHARE_INFO_2 structures.</summary>
            ResourecesEx = 502
            ''' <summary>Return information about shared resources, including name of the resource, type and permissions, number of connections, and other pertinent information. The bufptr parameter points to an array of SHARE_INFO_502 structures. Shares from different scopes are not returned. For more information about scoping, see the Remarks section of the documentation for the NetServerTransportAddEx function.</summary>
            ResourecesSingleScope = 503
            ''' <summary>Return information about shared resources, including the name of the resource, type and permissions, number of connections, and other pertinent information. The bufptr parameter points to an array of SHARE_INFO_503 structures. Shares from all scopes are returned. If the shi503_servername member of this structure is "*", there is no configured server name and the NetShareEnum function enumerates shares for all the unscoped names.</summary>
            ''' <remarks>Windows Server 2003, Windows XP, Windows 2000 Server, and Windows 2000 Professional:  This information level is not supported.</remarks>
            ResourcesAllScopes
        End Enum
        ''' <summary>contains the name of the shared resource</summary>
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure SHARE_INFO_0
            ''' <summary>Pointer to a Unicode string specifying the share name of a resource.</summary>
            <MarshalAs(UnmanagedType.LPWStr)> _
            Public shi0_netname As String
        End Structure
    End Module
End Namespace
#End If