Imports System.Runtime.InteropServices, Tools.ExtensionsT
Imports System.ComponentModel
Imports Microsoft.Win32.SafeHandles
Imports System.Text

#If True
Namespace API
    ''' <summary>Contains API declarations related to file system</summary>
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
        ''' <summary>contains the name of the shared resource</summary>
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure SHARE_INFO_0
            ''' <summary>Pointer to a Unicode string specifying the share name of a resource.</summary>
            <MarshalAs(UnmanagedType.LPWStr)> _
            Public shi0_netname As String
        End Structure

        ''' <summary>Contains information about a set of privileges for an access token.</summary>
        Public Structure TOKEN_PRIVILEGES
            ''' <summary>This must be set to the number of entries in the <see cref="Privileges"/> array.</summary>
            Public PrivilegeCount As UInteger
            ' !! think we only need one
            ''' <summary>Specifies an array of <see cref="LUID_AND_ATTRIBUTES"/> structures.</summary>
            <MarshalAs(UnmanagedType.ByValArray, SizeConst:=1)>
            Public Privileges As LUID_AND_ATTRIBUTES()
        End Structure
        ''' <summary>Represents a locally unique identifier (<see cref="LUID"/>) and its attributes.</summary>
        <StructLayout(LayoutKind.Sequential)>
        Public Structure LUID_AND_ATTRIBUTES
            ''' <summary>Specifies an <see cref="LUID"/> value.</summary>
            Public Luid As LUID
            ''' <summary>Specifies attributes of the <see cref="LUID"/>.</summary>
            ''' <remarks>This value contains up to 32 one-bit flags. Its meaning is dependent on the definition and use of the <see cref="LUID"/>.</remarks>
            Public Attributes As PrivilegeAttributes
        End Structure
        ''' <summary>A 64-bit value guaranteed to be unique only on the system on which it was generated.</summary>
        ''' <remarks>The uniqueness of a locally unique identifier (<see cref="LUID"/>) is guaranteed only until the system is restarted.</remarks>
        <StructLayout(LayoutKind.Sequential)>
        Public Structure LUID
            ''' <summary>Low-order bits.</summary>
            Public LowPart As UInt32
            ''' <summary>High-order bits.</summary>
            Public HighPart As Int32
        End Structure

        ''' <summary>Contains reparse point data for a Microsoft reparse point.</summary>
        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
        Public Structure REPARSE_DATA_BUFFER
            ''' <summary>Reparse point tag. Must be a Microsoft reparse point tag.</summary>
            Public ReparseTag As UInteger
            ''' <summary>Size, in bytes, of the reparse data in the DataBuffer member.</summary>
            Public ReparseDataLength As Short
            ''' <summary>Length, in bytes, of the unparsed portion of the file name pointed to by the FileName member of the associated file object.</summary>
            Public Reserved As Short
            ''' <summary>Offset, in bytes, of the substitute name string in the PathBuffer array. Note that this offset must be divided by sizeof(WCHAR) to get the array index.</summary>
            Public SubsNameOffset As Short
            ''' <summary>Length, in bytes, of the substitute name string. If this string is NULL-terminated, SubstituteNameLength does not include space for the UNICODE_NULL character.</summary>
            Public SubsNameLength As Short
            ''' <summary>Offset, in bytes, of the print name string in the PathBuffer array. Note that this offset must be divided by sizeof(WCHAR) to get the array index.</summary>
            Public PrintNameOffset As Short
            ''' <summary>Length, in bytes, of the print name string. If this string is NULL-terminated, PrintNameLength does not include space for the UNICODE_NULL character.</summary>
            Public PrintNameLength As Short
            <MarshalAs(UnmanagedType.ByValArray, SizeConst:=MAXIMUM_REPARSE_DATA_BUFFER_SIZE)>
            Public ReparseTarget As Char()
        End Structure
#End Region

#Region "Constants"
        ''' <summary>Maximum length of path for the <see cref="SHGetFileInfo"/> function</summary>
        Public Const MAX_PATH As Int32 = 260
        ''' <summary>Maximum value for prefferred data length of the <see cref="NetShareEnum"/> function</summary>
        Public Const MAX_PREFERRED_LENGTH% = &HFFFFFFFF
        ''' <summary>name of backup privilege</summary>
        Public Const SE_BACKUP_NAME$ = "SeBackupPrivilege"
        ''' <summary>Retrieves the reparse point data associated with the file or directory identified by the specified handle.</summary>
        Public Const FSCTL_GET_REPARSE_POINT = 42UI
        ''' <summary>Maximum size of reparse buffer data</summary>
        Public Const MAXIMUM_REPARSE_DATA_BUFFER_SIZE% = 16 * 1024
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
            ''' <summary>Access will occur according to POSIX rules. This includes allowing multiple files with names, differing only in case, for file systems that support that naming. Use care when using this option, because files created with this flag may not be accessible by applications that are written for MS-DOS or 16-bit System.Windows.</summary>
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
        ''' <summary>Performs an operation on a specified file.</summary>
        ''' <param name="lpExecInfo">The address of a <see cref="SHELLEXECUTEINFO"/> structure that contains and receives information about the application being executed. </param>
        ''' <returns>Returns TRUE if successful, or FALSE otherwise. Call GetLastError for error information. </returns>
        Public Declare Auto Function ShellExecuteEx Lib "shell32.dll" (ByRef lpExecInfo As SHELLEXECUTEINFO) As Integer


        ''' <summary>retrieves information about each shared resource on a server.</summary>
        ''' <param name="servername">Pointer to a string that specifies the DNS or NetBIOS name of the remote server on which the function is to execute. If this parameter is NULL, the local computer is used.</param>
        ''' <param name="level">Specifies the information level of the data.</param>
        ''' <param name="bufptr">Pointer to the buffer that receives the data. The format of this data depends on the value of the <paramref name="level"/> parameter. 
        ''' <para>This buffer is allocated by the system and must be freed using the NetApiBufferFree function. Note that you must free the buffer even if the function fails with ERROR_MORE_DATA.</para></param>
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

        ''' <summary>Retrieves file system attributes for a specified file or directory.</summary>
        ''' <param name="lpFileName">The name of the file or directory.</param>
        ''' <returns>If the function succeeds, the return value contains the attributes of the specified file or directory. For a list of attribute values and their descriptions, see <see cref="FileAttributes"/>.
        ''' If the function fails, the return value is <see cref="INVALID_FILE_ATTRIBUTES"/>.</returns>
        Public Declare Auto Function GetFileAttributes Lib "Kernel32.dll" (ByVal lpFileName As String) As FileAttributes
        ''' <summary>Value returned by <see cref="GetFileAttributes"/> in case of error</summary>
        Public Const INVALID_FILE_ATTRIBUTES As UInteger = &HFFFFFFFFUI

        ''' <summary>Creates a symbolic link.</summary>
        ''' <param name="lpSymlinkFileName">The symbolic link to be created.</param>
        ''' <param name="lpTargetFileName">
        ''' The name of the target for the symbolic link to be created.
        ''' <para>If <paramref name="lpTargetFileName"/> has a device name associated with it, the link is treated as an absolute link; otherwise, the link is treated as a relative link.</para>
        ''' </param>
        ''' <param name="dwFlags">Indicates whether the link target, <paramref name="lpTargetFileName"/>, is a directory.</param>
        ''' <returns>If the function succeeds, the return value true.</returns>
        ''' <remarks>Symbolic links can either be absolute or relative links. Absolute links are links that specify each portion of the path name; relative links are determined relative to where relative–link specifiers are in a specified path.</remarks>
        <DllImport("kernel32.dll")> _
        Public Function CreateSymbolicLink(ByVal lpSymlinkFileName As String, ByVal lpTargetFileName As String, ByVal dwFlags As SYMBOLIC_LINK_FLAG) As Boolean
        End Function
        ''' <summary>Enumeration indicates if symbolic link is file or directory</summary>
        Public Enum SYMBOLIC_LINK_FLAG As Integer
            ''' <summary>The link target is a file.</summary>
            File = 0
            ''' <summary>The link target is a directory.</summary>
            SYMBOLIC_LINK_FLAG_DIRECTORY = 1
        End Enum
        ''' <summary>Establishes a hard link between an existing file and a new file.</summary>
        ''' <param name="lpFileName">The name of the new file. This parameter cannot specify the name of a directory.</param>
        ''' <param name="lpExistingFileName">The name of the existing file. This parameter cannot specify the name of a directory.</param>
        ''' <param name="lpSecurityAttributes">Reserved. Must be <see cref="IntPtr.Zero"/></param>
        ''' <returns>True on success</returns>
        ''' <remarks>This function is only supported on the NTFS file system, and only for files, not directories.</remarks>
        <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
        Public Function CreateHardLink(ByVal lpFileName As String, ByVal lpExistingFileName As String, Optional ByVal lpSecurityAttributes As IntPtr = Nothing) As Boolean
        End Function

        ''' <summary>Opens the access token associated with a process.</summary>
        ''' <param name="ProcessHandle">A handle to the process whose access token is opened.</param>
        ''' <param name="DesiredAccessas">Specifies an access mask that specifies the requested types of access to the access token. These requested access types are compared with the discretionary access control list (DACL) of the token to determine which accesses are granted or denied.</param>
        ''' <param name="TokenHandle">A pointer to a handle that identifies the newly opened access token when the function returns.</param>
        ''' <returns>True on success</returns>
        <DllImport("advapi32.dll", SetLastError:=True)>
        Public Function OpenProcessToken(ProcessHandle As IntPtr, DesiredAccessas As AccessRightsForAccessTokenObjects, <Out> ByRef TokenHandle As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ''' <summary>Retrieves a pseudo handle for the current process.</summary>
        ''' <returns>The return value is a pseudo handle to the current process.</returns>
        <DllImport("kernel32.dll")>
        Public Function GetCurrentProcess() As IntPtr
        End Function

        ''' <summary>Retrieves the locally unique identifier (LUID) used on a specified system to locally represent the specified privilege name.</summary>
        ''' <param name="lpSystemName">A pointer to a null-terminated string that specifies the name of the system on which the privilege name is retrieved. If a null string is specified, the function attempts to find the privilege name on the local system.</param>
        ''' <param name="lpName">A pointer to a null-terminated string that specifies the name of the privilege, as defined in the Winnt.h header file. For example, this parameter could specify the constant, SE_SECURITY_NAME, or its corresponding string, "SeSecurityPrivilege".</param>
        ''' <param name="lpLuid">A pointer to a variable that receives the LUID by which the privilege is known on the system specified by the lpSystemName parameter.</param>
        ''' <returns>True on success</returns>
        <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
        Public Function LookupPrivilegeValue(lpSystemName$, lpName$, <Out> ByRef lpLuid As LUID) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ''' <summary>Enables or disables privileges in the specified access token. </summary>
        ''' <param name="TokenHandle">A handle to the access token that contains the privileges to be modified.</param>
        ''' <param name="DisableAllPrivileges">Specifies whether the function disables all of the token's privileges. If this value is TRUE, the function disables all privileges and ignores the NewState parameter. If it is FALSE, the function modifies privileges based on the information pointed to by the NewState parameter.</param>
        ''' <param name="NewState">A pointer to a TOKEN_PRIVILEGES structure that specifies an array of privileges and their attributes. If the DisableAllPrivileges parameter is FALSE, the AdjustTokenPrivileges function enables, disables, or removes these privileges for the token. The following table describes the action taken by the AdjustTokenPrivileges function, based on the privilege attribute. If DisableAllPrivileges is TRUE, the function ignores this parameter.</param>
        ''' <param name="BufferLength">Specifies the size, in bytes, of the buffer pointed to by the PreviousState parameter. This parameter can be zero if the PreviousState parameter is NULL.</param>
        ''' <param name="PreviousState">A pointer to a buffer that the function fills with a TOKEN_PRIVILEGES structure that contains the previous state of any privileges that the function modifies. </param>
        ''' <param name="ReturnLength">A pointer to a variable that receives the required size, in bytes, of the buffer pointed to by the PreviousState parameter. This parameter can be NULL if PreviousState is NULL.</param>
        <DllImport("advapi32.dll", SetLastError:=True)>
        Public Function AdjustTokenPrivileges(TokenHandle As IntPtr,
            <MarshalAs(UnmanagedType.Bool)> DisableAllPrivileges As Boolean,
            ByRef NewState As TOKEN_PRIVILEGES,
            BufferLength As Int32,
            PreviousState As IntPtr,
            ReturnLength As IntPtr
        ) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ''' <summary>Sends a control code directly to a specified device driver, causing the corresponding device to perform the corresponding operation.</summary>
        ''' <param name="hDevice">A handle to the device on which the operation is to be performed. The device is typically a volume, directory, file, or stream. To retrieve a device handle, use the CreateFile function. </param>
        ''' <param name="dwIoControlCode">The control code for the operation. This value identifies the specific operation to be performed and the type of device on which to perform it.</param>
        ''' <param name="lpInBuffer">A pointer to the input buffer that contains the data required to perform the operation. The format of this data depends on the value of the dwIoControlCode parameter.</param>
        ''' <param name="nInBufferSize">The size of the input buffer, in bytes.</param>
        ''' <param name="outBuffer">A pointer to the output buffer that is to receive the data returned by the operation. The format of this data depends on the value of the dwIoControlCode parameter.</param>
        ''' <param name="nOutBufferSize">The size of the output buffer, in bytes.</param>
        ''' <param name="lpBytesReturned">A pointer to a variable that receives the size of the data stored in the output buffer, in bytes.</param>
        ''' <param name="lpOverlapped">A pointer to an OVERLAPPED structure.</param>
        ''' <returns>True on success</returns>
        <DllImport("kernel32.dll", ExactSpelling:=True, SetLastError:=True, CharSet:=CharSet.Auto)>
        Public Function DeviceIoControl(
             hDevice As IntPtr,
            dwIoControlCode As UInteger,
            lpInBuffer As IntPtr,
            nInBufferSize As UInteger,
            <Out> ByRef outBuffer As REPARSE_DATA_BUFFER,
            nOutBufferSize As UInteger,
            <Out> ByRef lpBytesReturned As UInteger,
            lpOverlapped As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function
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

        ''' <summary>Levels for the <see cref="NetShareEnum"/> function</summary>
        Public Enum NetShareLevel As Integer
            ''' <summary>Return share names. The bufptr parameter points to an array of SHARE_INFO_0 structures.</summary>
            Names = 0
            ''' <summary>Return information about shared resources, including the name and type of the resource, and a comment associated with the resource. The bufptr parameter points to an array of SHARE_INFO_1 structures. </summary>
            Resources = 1
            ''' <summary>Return information about shared resources, including name of the resource, type and permissions, password, and number of connections. The bufptr parameter points to an array of SHARE_INFO_2 structures.</summary>
            ResourecesEx = 2
            ''' <summary>Return information about shared resources, including name of the resource, type and permissions, number of connections, and other pertinent information. The bufptr parameter points to an array of SHARE_INFO_502 structures. Shares from different scopes are not returned. For more information about scoping, see the Remarks section of the documentation for the NetServerTransportAddEx function.</summary>
            ResourecesSingleScope = 502
            ''' <summary>Return information about shared resources, including the name of the resource, type and permissions, number of connections, and other pertinent information. The bufptr parameter points to an array of SHARE_INFO_503 structures. Shares from all scopes are returned. If the shi503_servername member of this structure is "*", there is no configured server name and the NetShareEnum function enumerates shares for all the unscoped names.</summary>
            ''' <remarks>Windows Server 2003, Windows XP, Windows 2000 Server, and Windows 2000 Professional:  This information level is not supported.</remarks>
            ResourcesAllScopes = 503
        End Enum

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
        <Flags()> _
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
            ''' <remarks><note>Note  This flag is available only in Microsoft Windows XP and earlier. It is not available in Windows Vista and later versions of System.Windows.</note></remarks>
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

        ''' <summary>Access flags for acccess to tokens</summary>
        <Flags>
        Public Enum AccessRightsForAccessTokenObjects As UInteger
            ''' <summary>The right to delete the object.</summary>
            DELETE = &H10000
            ''' <summary>The right to read the information in the object's security descriptor, not including the information in the system access control list (SACL).</summary>
            READ_CONTROL = &H20000
            ''' <summary>The right to modify the discretionary access control list (DACL) in the object's security descriptor.</summary>
            WRITE_DAC = &H40000
            ''' <summary>The right to change the owner in the object's security descriptor.</summary>
            WRITE_OWNER = &H80000
            ''' <summary>The right to use the object for synchronization. This enables a thread to wait until the object is in the signaled state. Some object types do not support this access right.</summary>
            SYNCHRONIZE = &H100000

            ''' <summary>Combines <see cref="DELETE"/>, <see cref="READ_CONTROL"/>, <see cref="WRITE_DAC"/>, and <see cref="WRITE_OWNER"/> access.</summary>
            STANDARD_RIGHTS_REQUIRED = &HF0000
            ''' <summary>Currently defined to equal <see cref="READ_CONTROL"/>.</summary>
            STANDARD_RIGHTS_READ = READ_CONTROL
            ''' <summary>Currently defined to equal <see cref="READ_CONTROL"/>.</summary>
            STANDARD_RIGHTS_WRITE = READ_CONTROL
            ''' <summary>Currently defined to equal <see cref="READ_CONTROL"/>.</summary>
            STANDARD_RIGHTS_EXECUTE = READ_CONTROL
            ''' <summary>Combines <see cref="DELETE"/>, <see cref="READ_CONTROL"/>, <see cref="WRITE_DAC"/>, <see cref="WRITE_OWNER"/>, and <see cref="SYNCHRONIZE"/> access.</summary>
            STANDARD_RIGHTS_ALL = &H1F0000
            ''' <summary>All specific rights.</summary>
            SPECIFIC_RIGHTS_ALL = &HFFFF

            ''' <summary>controls the ability to get or set the SACL in an object's security descriptor. The system grants this access right only if the SE_SECURITY_NAME privilege is enabled in the access token of the requesting thread.</summary>
            ACCESS_SYSTEM_SECURITY = &H1000000

            ''' <summary>Required to change the default owner, primary group, or DACL of an access token.</summary>
            TOKEN_ADJUST_DEFAULT = &H80
            ''' <summary>Required to adjust the attributes of the groups in an access token.</summary>
            TOKEN_ADJUST_GROUPS = &H40
            ''' <summary>Required to enable or disable the privileges in an access token.</summary>
            TOKEN_ADJUST_PRIVILEGES = &H20
            ''' <summary>Required to adjust the session ID of an access token. The SE_TCB_NAME privilege is required.</summary>
            TOKEN_ADJUST_SESSIONID = &H100
            ''' <summary>Required to attach a primary token to a process. The SE_ASSIGNPRIMARYTOKEN_NAME privilege is also required to accomplish this task.</summary>
            TOKEN_ASSIGN_PRIMARY = 1
            ''' <summary>Required to duplicate an access token.</summary>
            TOKEN_DUPLICATE = 2
            ''' <summary>Combines <see cref="STANDARD_RIGHTS_EXECUTE"/> and <see cref="TOKEN_IMPERSONATE"/>.</summary>
            TOKEN_EXECUTE
            ''' <summary>Required to attach an impersonation access token to a process.</summary>
            TOKEN_IMPERSONATE = 4
            ''' <summary>Required to query an access token.</summary>
            TOKEN_QUERY = 8
            ''' <summary>Required to query the source of an access token.</summary>
            TOKEN_QUERY_SOURCE = &H10
            ''' <summary>Combines <see cref="STANDARD_RIGHTS_READ"/> and <see cref="TOKEN_QUERY"/>.</summary>
            TOKEN_READ = STANDARD_RIGHTS_READ Or TOKEN_QUERY
            ''' <summary>Combines <see cref="STANDARD_RIGHTS_WRITE"/>, <see cref="TOKEN_ADJUST_PRIVILEGES"/>, <see cref="TOKEN_ADJUST_GROUPS"/>, and <see cref="TOKEN_ADJUST_DEFAULT"/>.</summary>
            TOKEN_WRITE = STANDARD_RIGHTS_WRITE Or TOKEN_ADJUST_PRIVILEGES Or TOKEN_ADJUST_GROUPS Or TOKEN_ADJUST_DEFAULT
            ''' <summary>Combines all possible access rights for a token.</summary>
            TOKEN_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED Or TOKEN_ASSIGN_PRIMARY Or TOKEN_DUPLICATE Or TOKEN_IMPERSONATE Or TOKEN_QUERY Or TOKEN_QUERY_SOURCE Or
                               TOKEN_ADJUST_PRIVILEGES Or TOKEN_ADJUST_GROUPS Or TOKEN_ADJUST_DEFAULT Or TOKEN_ADJUST_SESSIONID
        End Enum

        ''' <summary>Privilege attributes</summary>
        <Flags>
        Public Enum PrivilegeAttributes As UInt32
            ''' <summary>The privilege is enabled.</summary>
            SE_PRIVILEGE_ENABLED = 2
            ''' <summary>Used to remove a privilege. For details, see <see cref="AdjustTokenPrivileges"/>.</summary>
            SE_PRIVILEGE_REMOVED = 4
            ''' <summary>The privilege is enabled by default.</summary>
            SE_PRIVILEGE_ENABLED_BY_DEFAULT = 1
            ''' <summary>The privilege was used to gain access to an object or service. This flag is used to identify the relevant privileges in a set passed by a client application that may contain unnecessary privileges.</summary>
            SE_PRIVILEGE_USED_FOR_ACCESS = 8
        End Enum

        ''' <summary>Device types that are availbable for file-like operations</summary>
        Public Enum FileDeviceTypes As UInteger
            ''' <summary>8042 pport</summary>
            FILE_DEVICE_8042_PORT = &H27
            ''' <summary>ACPI device</summary>
            FILE_DEVICE_ACPI = &H32
            ''' <summary>Battery</summary>
            FILE_DEVICE_BATTERY = &H29
            ''' <summary>Beep</summary>
            FILE_DEVICE_BEEP = &H1
            ''' <summary>Bus extender</summary>
            FILE_DEVICE_BUS_EXTENDER = &H2A
            ''' <summary>CR-ROM</summary>
            FILE_DEVICE_CD_ROM = &H2
            ''' <summary>CD-ROM file system</summary>
            FILE_DEVICE_CD_ROM_FILE_SYSTEM = &H3
            ''' <summary>CHanged</summary>
            FILE_DEVICE_CHANGER = &H30
            ''' <summary>Controller</summary>
            FILE_DEVICE_CONTROLLER = &H4
            ''' <summary>Data-link</summary>
            FILE_DEVICE_DATALINK = &H5
            ''' <summary>DFS</summary>
            FILE_DEVICE_DFS = &H6
            ''' <summary>DFS file system</summary>
            FILE_DEVICE_DFS_FILE_SYSTEM = &H35
            ''' <summary>DFS volume</summary>
            FILE_DEVICE_DFS_VOLUME = &H36
            ''' <summary>Disk</summary>
            FILE_DEVICE_DISK = &H7
            ''' <summary>Disk file system</summary>
            FILE_DEVICE_DISK_FILE_SYSTEM = &H8
            ''' <summary>DVD</summary>
            FILE_DEVICE_DVD = &H33
            ''' <summary>File system</summary>
            FILE_DEVICE_FILE_SYSTEM = &H9
            ''' <summary>FIPS</summary>
            FILE_DEVICE_FIPS = &H3A
            ''' <summary>Full-screen video</summary>
            FILE_DEVICE_FULLSCREEN_VIDEO = &H34
            ''' <summary>Input port</summary>
            FILE_DEVICE_INPORT_PORT = &HA
            ''' <summary>Keyboard</summary>
            FILE_DEVICE_KEYBOARD = &HB
            ''' <summary>KS</summary>
            FILE_DEVICE_KS = &H2F
            ''' <summary>KSEC</summary>
            FILE_DEVICE_KSEC = &H39
            ''' <summary>MAil slot</summary>
            FILE_DEVICE_MAILSLOT = &HC
            ''' <summary>Mass Storage device</summary>
            FILE_DEVICE_MASS_STORAGE = &H2D
            ''' <summary>MIDI input</summary>
            FILE_DEVICE_MIDI_IN = &HD
            ''' <summary>MIDI output</summary>
            FILE_DEVICE_MIDI_OUT = &HE
            ''' <summary>Modem</summary>
            FILE_DEVICE_MODEM = &H2B
            ''' <summary>Mouse</summary>
            FILE_DEVICE_MOUSE = &HF
            ''' <summary>Multy UNC provider</summary>
            FILE_DEVICE_MULTI_UNC_PROVIDER = &H10
            ''' <summary>Named pipe</summary>
            FILE_DEVICE_NAMED_PIPE = &H11
            ''' <summary>Network</summary>
            FILE_DEVICE_NETWORK = &H12
            ''' <summary>Network browser</summary>
            FILE_DEVICE_NETWORK_BROWSER = &H13
            ''' <summary>Network file system</summary>
            FILE_DEVICE_NETWORK_FILE_SYSTEM = &H14
            ''' <summary>Network redirector</summary>
            FILE_DEVICE_NETWORK_REDIRECTOR = &H28
            ''' <summary>NULL device</summary>
            FILE_DEVICE_NULL = &H15
            ''' <summary>LPT parallel port</summary>
            FILE_DEVICE_PARALLEL_PORT = &H16
            ''' <summary>Physical net card</summary>
            FILE_DEVICE_PHYSICAL_NETCARD = &H17
            ''' <summary>Printer</summary>
            FILE_DEVICE_PRINTER = &H18
            ''' <summary>Scanner</summary>
            FILE_DEVICE_SCANNER = &H19
            ''' <summary>Screen</summary>
            FILE_DEVICE_SCREEN = &H1C
            ''' <summary>Serial enumerator</summary>
            FILE_DEVICE_SERENUM = &H37
            ''' <summary>Serial mouse port</summary>
            FILE_DEVICE_SERIAL_MOUSE_PORT = &H1A
            ''' <summary>Serial port</summary>
            FILE_DEVICE_SERIAL_PORT = &H1B
            ''' <summary>Smart card</summary>
            FILE_DEVICE_SMARTCARD = &H31
            ''' <summary>SMB</summary>
            FILE_DEVICE_SMB = &H2E
            ''' <summary>Sound device</summary>
            FILE_DEVICE_SOUND = &H1D
            ''' <summary>Stream</summary>
            FILE_DEVICE_STREAMS = &H1E
            ''' <summary>Tape</summary>
            FILE_DEVICE_TAPE = &H1F
            ''' <summary>Tape file system</summary>
            FILE_DEVICE_TAPE_FILE_SYSTEM = &H20
            ''' <summary>Terminal server</summary>
            FILE_DEVICE_TERMSRV = &H38
            ''' <summary>Transport device</summary>
            FILE_DEVICE_TRANSPORT = &H21
            ''' <summary>Unknown device</summary>
            FILE_DEVICE_UNKNOWN = &H22
            ''' <summary>VDM</summary>
            FILE_DEVICE_VDM = &H2C
            ''' <summary>Video</summary>
            FILE_DEVICE_VIDEO = &H23
            ''' <summary>Virtual disk</summary>
            FILE_DEVICE_VIRTUAL_DISK = &H24
            ''' <summary>Wave input</summary>
            FILE_DEVICE_WAVE_IN = &H25
            ''' <summary>Wave output</summary>
            FILE_DEVICE_WAVE_OUT = &H26
        End Enum

        ''' <summary>Specifies IO methods</summary>
        Public Enum IOMethod As UInteger
            ''' <summary>Specifies the buffered I/O method, which is typically used for transferring small amounts of data per request. </summary>
            METHOD_BUFFERED = 0
            ''' <summary>Specifies the direct I/O method, which is typically used for reading or writing large amounts of data. Use if caller will pass data to the driver.</summary>
            METHOD_IN_DIRECT = 1
            ''' <summary>Specifies the direct I/O method, which is typically used for reading or writing large amounts of data. Use if caller will receive data from the driver.</summary>
            METHOD_OUT_DIRECT = 2
            ''' <summary>Specifies neither buffered nor direct I/O. The I/O manager does not provide any system buffers or MDLs.</summary>
            METHOD_NEITHER = 3
        End Enum

        ''' <summary>Specifies IO fidle access methods</summary>
        Public Enum IOFileAccess As UInteger
            ''' <summary>The I/O manager sends the IRP for any caller that has a handle to the file object that represents the target device object.</summary>
            FILE_ANY_ACCESS = 0
            ''' <summary>The I/O manager sends the IRP only for a caller with read access rights, allowing the underlying device driver to transfer data from the device to system memory.</summary>
            FILE_READ_ACCESS = 1
            ''' <summary>The I/O manager sends the IRP only for a caller with write access rights, allowing the underlying device driver to transfer data from system memory to its device.</summary>
            FILE_WRITE_ACCESS = 2
        End Enum

        ''' <summary>Reparse tags values</summary>
        Public Enum ReparseTags As UInteger
            ''' <summary>Reserved reparse tag value.</summary>
            IO_REPARSE_TAG_RESERVED_ZERO =&h00000000
            ''' <summary>Reserved reparse tag value.</summary>
            IO_REPARSE_TAG_RESERVED_ONE = &H1
            ''' <summary>Used for mount point support, specified in section 2.1.2.5.</summary>
            IO_REPARSE_TAG_MOUNT_POINT = &HA0000003UI
            ''' <summary>Obsolete. Used by legacy Hierarchical Storage Manager Product.</summary>
            IO_REPARSE_TAG_HSM = &HC0000004UI
            ''' <summary>Obsolete. Used by legacy Hierarchical Storage Manager Product.</summary>
            IO_REPARSE_TAG_HSM2 = &H80000006UI
            ''' <summary>Home server drive extender.<3></summary>
            IO_REPARSE_TAG_DRIVER_EXTENDER = &H80000005UI
            ''' <summary>Used by single-instance storage (SIS) filter driver. Server-side interpretation only, not meaningful over the wire.</summary>
            IO_REPARSE_TAG_SIS = &H80000007UI
            ''' <summary>Used by the DFS filter. The DFS is described in the Distributed File System (DFS): Referral Protocol Specification [MS-DFSC]. Server-side interpretation only, not meaningful over the wire.</summary>
            IO_REPARSE_TAG_DFS = &H8000000AUI
            ''' <summary>Used by the DFS filter. The DFS is described in [MS-DFSC]. Server-side interpretation only, not meaningful over the wire.</summary>
            IO_REPARSE_TAG_DFSR = &H80000012UI
            ''' <summary>Used by filter manager test harness.<4></summary>
            IO_REPARSE_TAG_FILTER_MANAGER = &H8000000BUI
            ''' <summary>Used for symbolic link support. See section 2.1.2.</summary>
            IO_REPARSE_TAG_SYMLINK = &HA000000CUI
        End Enum
#End Region

        '        ''' <summary>Copies, moves, renames, or deletes a file system object.</summary>
        '        ''' <param name="lpFileOp">[in] A pointer to an <see cref="SHFILEOPSTRUCT"/> structure that contains information this function needs to carry out the specified operation. This parameter must contain a valid value that is not NULL. You are responsible for validating the value. If you do not validate it, you will experience unexpected results.</param>
        '        ''' <returns><para>Returns zero if successful; otherwise nonzero. Applications normally should simply check for zero or nonzero.</para>
        '        ''' <para>It is good practice to examine the value of the <see cref="SHFILEOPSTRUCT.fAnyOperationsAborted"/> member of the <see cref="SHFILEOPSTRUCT"/>. <see cref="SHFileOperation"/> can return 0 for success if the user cancels the operation. If you do not check <see cref="SHFILEOPSTRUCT.fAnyOperationsAborted"/> as well as the return value, you cannot know that the function accomplished the full task you asked of it and you might proceed under incorrect assumptions.</para>
        '        ''' <para>Do not use GetLastError with the return values of this function.</para></returns>
        '        Public Declare Auto Function SHFileOperation Lib "shell32.dll" (ByRef lpFileOp As SHFILEOPSTRUCT) As Int32
        '        ''' <summary></summary>
        '        <StructLayout(LayoutKind.Sequential)> _
        '        Public Structure SHFILEOPSTRUCT
        '            ''' <summary>A window handle to the dialog box to display information about the status of the file operation.</summary>
        '            Public hWnd As Int32
        '            ''' <summary>A value that indicates which operation to perform.</summary>
        '            Public wFunc As FileSystemOperation
        '            ''' <summary>Contains value of the <see cref="pFrom"/> property</summary>
        '            ''' <remarks>Although this member is declared as a single null-terminated string, it is actually a buffer that can hold multiple null-delimited file names. Each file name is terminated by a single NULL character. The last file name is terminated with a double NULL character ("\0\0") to indicate the end of the buffer.</remarks>
        '            <MarshalAs(UnmanagedType.LPTStr)> _
        '            Private _pFrom As String
        '            ''' <summary>A pointer to one or more source file names. These names should be fully-qualified paths to prevent unexpected results.</summary>
        '            ''' <remarks><note>Note  This string must be double-null terminated.</note>
        '            ''' <para>Standard Microsoft MS-DOS wildcard characters, such as "*", are permitted only in the file-name position. Using a wildcard character elsewhere in the string will lead to unpredictable results.</para></remarks>
        '            Public Property pFrom() As String()
        '                Get
        '                    Return _pFrom.TrimEnd(ChrW(0)).Split(ChrW(0))
        '                End Get
        '                Set(ByVal value As String())
        '                    _pFrom = value.Join(ChrW(0)) & ChrW(0) & ChrW(0)
        '                End Set
        '            End Property
        '            ''' <summary>Contains value of the <see cref="pTo"/> property</summary>
        '            ''' <remarks><note>Note  This string must be double-null terminated.</note>
        '            ''' <para>Although this member is declared as a single null-terminated string, it is actually a buffer that can hold multiple null-delimited file names. Each file name is terminated by a single NULL character. The last file name is terminated with a double NULL character ("\0\0") to indicate the end of the buffer.</para></remarks>
        '            Private _pTo As String
        '            ''' <summary>A pointer to one or more source file names. These names should be fully-qualified paths to prevent unexpected results.</summary>
        '            ''' <remarks>Standard Microsoft MS-DOS wildcard characters, such as "*", are permitted only in the file-name position. Using a wildcard character elsewhere in the string will lead to unpredictable results.</remarks>
        '            Public Property pTo() As String()
        '                Get
        '                    Return _pTo.TrimEnd(ChrW(0)).Split(ChrW(0))
        '                End Get
        '                Set(ByVal value As String())
        '                    _pTo = value.Join(ChrW(0)) & ChrW(0) & ChrW(0)
        '                End Set
        '            End Property
        '            ''' <summary>Flags that control the file operation. </summary>
        '            Public fFlags As FileOperationFlags
        '            ''' <summary>When the function returns, this member contains TRUE if any file operations were aborted before they were completed; otherwise, FALSE. An operation can be manually aborted by the user through UI or it can be silently aborted by the system if the <see cref="FileOperationFlags.FOF_NOERRORUI"/> or <see cref="FileOperationFlags.FOF_NOCONFIRMATION"/> flags were set.</summary>
        '            Public fAnyOperationsAborted As Int32
        '            ''' <summary>When the function returns, this member contains a handle to a name mapping object that contains the old and new names of the renamed files. This member is used only if the <see cref="fFlags"/> member includes the <see cref="FileOperationFlags.FOF_WANTMAPPINGHANDLE"/> flag.</summary>
        '            Public hNameMaps As IntPtr
        '            ''' <summary>A pointer to the title of a progress dialog box. This is a null-terminated string. This member is used only if <see cref="fFlags"/> includes the <see cref="FileOperationFlags.FOF_SIMPLEPROGRESS"/> flag.</summary>
        '            Public sProgress As String
        '#Region "Enums"
        '            ''' <summary>File system operations</summary>
        '            Public Enum FileSystemOperation As Integer
        '                ''' <summary>Copy the files specified in the <see cref="pFrom"/> member to the location specified in the <see cref="pTo"/> member.</summary>
        '                FO_COPY = &H2
        '                ''' <summary>Delete the files specified in <see cref="pFrom"/>.</summary>
        '                FO_DELETE = &H3
        '                ''' <summary>Move the files specified in <see cref="pFrom"/> to the location specified in <see cref="pTo"/>.</summary>
        '                FO_MOVE = &H1
        '                ''' <summary>Rename the file specified in <see cref="pFrom"/>. You cannot use this flag to rename multiple files with a single function call. Use <see cref="FO_MOVE"/> instead.</summary>
        '                FO_RENAME = &H4
        '            End Enum
        '            ''' <summary>File operation flags</summary>
        '            <Flags()> _
        '            Public Enum FileOperationFlags As UInt16
        '                ''' <summary>Preserve undo information, if possible. Prior to Windows Vista, operations could be undone only from the same process that performed the original operation. In Windows Vista and later systems, the scope of the undo is a user session. Any process running in the user session can undo another operation. The undo state is held in the Explorer.exe process, and as long as that process is running, it can coordinate the undo functions. If the source file parameter does not contain fully qualified path and file names, this flag is ignored.</summary>
        '                FOF_ALLOWUNDO = &H40
        '                ''' <summary>Not used.</summary>
        '                <EditorBrowsable(EditorBrowsableState.Never)> _
        '                FOF_CONFIRMMOUSE = &H2
        '                ''' <summary>Perform the operation only on files (not on folders) if a wildcard file name (*.*) is specified.</summary>
        '                FOF_FILESONLY = &H80
        '                ''' <summary>The pTo member specifies multiple destination files (one for each source file in pFrom) rather than one directory where all source files are to be deposited.</summary>
        '                FOF_MULTIDESTFILES = &H1
        '                ''' <summary>Respond with Yes to All for any dialog box that is displayed.</summary>
        '                FOF_NOCONFIRMATION = &H10
        '                ''' <summary>Do not ask the user to confirm the creation of a new directory if the operation requires one to be created.</summary>
        '                FOF_NOCONFIRMMKDIR = &H200
        '                ''' <summary>Version 5.0. Do not move connected files as a group. Only move the specified files.</summary>
        '                FOF_NO_CONNECTED_ELEMENTS = &H2000
        '                ''' <summary>Version 4.71. Do not copy the security attributes of the file. The destination file receives the security attributes of its new folder.</summary>
        '                FOF_NOCOPYSECURITYATTRIBS = &H800
        '                ''' <summary>Do not display a dialog to the user if an error occurs.</summary>
        '                FOF_NOERRORUI = &H400
        '                ''' <summary>Not used.</summary>
        '                FOF_NORECURSEREPARSE = &H8000
        '                ''' <summary>Only perform the operation in the local directory. Don't operate recursively into subdirectories, which is the default behavior.</summary>
        '                FOF_NORECURSION = &H1000
        '                ''' <summary>Version 6.0.6060 (Windows Vista). Perform the operation silently, presenting no user interface (UI) to the user. This is equivalent to FOF_SILENT | FOF_NOCONFIRMATION | FOF_NOERRORUI | FOF_NOCONFIRMMKDIR.</summary>
        '                FOF_NO_UI
        '                ''' <summary>Give the file being operated on a new name in a move, copy, or rename operation if a file with the target name already exists at the destination.</summary>
        '                FOF_RENAMEONCOLLISION = &H8
        '                ''' <summary>Do not display a progress dialog box.</summary>
        '                FOF_SILENT = &H4
        '                ''' <summary>Display a progress dialog box but do not show individual file names as they are operated on.</summary>
        '                FOF_SIMPLEPROGRESS = &H100
        '                ''' <summary>If FOF_RENAMEONCOLLISION is specified and any files were renamed, assign a name mapping object that contains their old and new names to the hNameMappings member. This object must be freed using SHFreeNameMappings when it is no longer needed.</summary>
        '                FOF_WANTMAPPINGHANDLE = &H20
        '                ''' <summary>Version 5.0. Send a warning if a file is being permanently destroyed during a delete operation rather than recycled. This flag partially overrides FOF_NOCONFIRMATION.</summary>
        '                FOF_WANTNUKEWARNING = &H4000
        '            End Enum
        '#End Region
        '        End Structure
    End Module
End Namespace
#End If