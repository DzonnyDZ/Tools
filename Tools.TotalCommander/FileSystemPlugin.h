#pragma once
#include "fsplugin.h"
#include "Common.h"
#include "ContentPluginBase.h"

namespace Tools{namespace TotalCommanderT{
    using namespace System;
    using namespace System::ComponentModel;

    /// <summary>Converts <see cref="FILETIME"/> to <see cref="DateTime"/></summary>
    /// <param name="value">A <see cref="FILETIME"/></param>
    /// <returns>Corresponding <see cref="DateTime"/></returns>
    DateTime FileTimeToDateTime(FILETIME value);
    /// <summary>Converts <see cref="DateTime"/> to <see cref="FILETIME"/></summary>
    /// <param name="value">A <see cref="DateTime"/></param>
    /// <returns>Corresponding <see cref="FILETIME"/></returns>
    FILETIME DateTimeToFileTime(DateTime value);
   
    /// <summary>Result of file system operation</summary>
    public enum class FileSystemExitCode{
        /// <summary>The file was copied/moved OK</summary>
        OK = FS_FILE_OK,
        /// <summary>The target file already exists</summary>
        FileExists = FS_FILE_EXISTS,
        /// <summary>The source file couldn't be found or opened.</summary>
        FileNotFound = FS_FILE_NOTFOUND,
        /// <summary>There was an error reading from the source file</summary>
        ReadError = FS_FILE_READERROR,
        /// <summary>There was an error writing to the target file, e.g. disk full</summary>
        WriteError = FS_FILE_WRITEERROR,
        /// <summary>Copying was aborted by the user (through ProgressProc)</summary>
        UserAbort = FS_FILE_USERABORT,
        /// <summary>The operation is not supported (e.g. resume)</summary>
        NotSupported = FS_FILE_NOTSUPPORTED,
        /// <summary>The local file already exists, and resume is supported (used when Gettin/Putting file not for pure-remote copy/move)</summary>
        ExistsResumeAllowed = FS_FILE_EXISTSRESUMEALLOWED
    };
 
    /// <summary>Identifies result of file system exec operation</summary>
    public enum class ExecExitCode{
        /// <summary>The command was executed successfully in the plugin (or if the command isn't applicable and no further action is needed)</summary>
        OK = FS_EXEC_OK,
        /// <summary>Execution failed</summary>
        Error = FS_EXEC_ERROR,
        /// <summary>Total Commander should download the file and execute it locally</summary>
        Yourself = FS_EXEC_YOURSELF,
        /// <summary>This was a (symbolic) link or .lnk file pointing to a different directory</summary>
        Symlink = FS_EXEC_SYMLINK
    };
    
    /// <summary>Cfalg used when copying or moving file or directory</summary>
    [FlagsAttribute()] 
    public enum class CopyFlags{
        /// <summary>Overwrite target if exists</summary>
        Overwrite = FS_COPYFLAGS_OVERWRITE,
        /// <summary>Resume interrupted operation</summary>
        Resume = FS_COPYFLAGS_RESUME,
        /// <summary>Move (delete source after copy)</summary>
        Move = FS_COPYFLAGS_MOVE,
        /// <summary>Same casing</summary>
        SameCase = FS_COPYFLAGS_EXISTS_SAMECASE,
        /// <summary>Different casing</summary>
        DiferentCase = FS_COPYFLAGS_EXISTS_DIFFERENTCASE
    };
    /// <summary>File attributes</summary>
    [FlagsAttribute]
    [CLSCompliantAttribute(false)]
    public enum class FileAttributes : DWORD{
        /// <summary>The file or directory is an archive file or directory. Applications use this attribute to mark files for backup or removal.</summary>
        Archive = FILE_ATTRIBUTE_ARCHIVE,
        /// <summary>The file or directory is compressed. For a file, this means that all of the data in the file is compressed. For a directory, this means that compression is the default for newly created files and subdirectories.</summary>
        Compressed = FILE_ATTRIBUTE_COMPRESSED,
        /// <summary>Reserved; do not use.</summary>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        Device = FILE_ATTRIBUTE_DEVICE,                         
        /// <summary>The handle identifies a directory.</summary>
        Directory = FILE_ATTRIBUTE_DIRECTORY,                
        /// <summary>The file or directory is encrypted. For a file, this means that all data in the file is encrypted. For a directory, this means that encryption is the default for newly created files and subdirectories.</summary>
        Encrypted = FILE_ATTRIBUTE_ENCRYPTED,
        /// <summary>The file or directory is hidden. It is not included in an ordinary directory listing.</summary>
        Hidden = FILE_ATTRIBUTE_HIDDEN,
        /// <summary>The file or directory does not have other attributes set. This attribute is valid only when used alone.</summary>
        Normal = FILE_ATTRIBUTE_NORMAL,
        /// <summary>The file is not to be indexed by the content indexing service.</summary>
        NotIndexed = FILE_ATTRIBUTE_NOT_CONTENT_INDEXED,
        /// <summary>The file data is not available immediately. This attribute indicates that the file data is physically moved to offline storage. This attribute is used by Remote Storage, which is the hierarchical storage management software.</summary>
        /// <remarks>Note  Applications should not arbitrarily change this attribute.</remarks>
        Offline = FILE_ATTRIBUTE_OFFLINE,
        /// <summary>The file or directory is read-only. For a file, applications can read the file, but cannot write to it or delete it. For a directory, applications cannot delete it.</summary>
        ReadOnly = FILE_ATTRIBUTE_READONLY,
        /// <summary>The file or directory has an associated reparse point.</summary>
        ReparsePoint = FILE_ATTRIBUTE_REPARSE_POINT,
        /// <summary>The file is a sparse file.</summary>
        SparseFile = FILE_ATTRIBUTE_SPARSE_FILE,
        /// <summary>The file or directory is part of the operating system, or the operating system uses the file or directory exclusively.</summary>
        System = FILE_ATTRIBUTE_SYSTEM,
        /// <summary>The file is being used for temporary storage. File systems attempt to keep all of the data in memory for quick access, rather than flushing it back to mass storage. An application should delete a temporary file as soon as it is not needed.</summary>
        Temporary = FILE_ATTRIBUTE_TEMPORARY,
        /// <summary>The file is a virtual file.</summary>
        Virtual = FILE_ATTRIBUTE_VIRTUAL
    };

    /// <summary>Standars subset of file attributes</summary>
    /// <seelso cref="FileAttributes"/>
    [FlagsAttribute]
    public enum class StandardFileAttributes{
        /// <summary>The file or directory is read-only. For a file, applications can read the file, but cannot write to it or delete it. For a directory, applications cannot delete it.</summary>
        ReadOnly = FILE_ATTRIBUTE_READONLY,
        /// <summary>The file or directory is hidden. It is not included in an ordinary directory listing.</summary>
        Hidden = FILE_ATTRIBUTE_HIDDEN,
        /// <summary>The file or directory is part of the operating system, or the operating system uses the file or directory exclusively.</summary>
        System = FILE_ATTRIBUTE_SYSTEM,
        /// <summary>The file or directory is an archive file or directory. Applications use this attribute to mark files for backup or removal.</summary>
        Archive = FILE_ATTRIBUTE_ARCHIVE
    };

    /// <summary>When you set a reparse point, you must tag the data to be placed in the reparse point. After the reparse point has been established, a new set operation fails if the tag for the new data does not match the tag for the existing data. If the tags match, the set operation overwrites the existing reparse point.</summary>
    [FlagsAttribute]
    [CLSCompliantAttribute(false)]
    public enum class ReparsePointTags : DWORD{
        /// <summary>Microsoft-defined reparse tag</summary>
        DFS = IO_REPARSE_TAG_DFS,
        /// <summary>Microsoft-defined reparse tag</summary>
	    DFSR = IO_REPARSE_TAG_DFSR,
        /// <summary>Microsoft-defined reparse tag</summary>
	    HSM = IO_REPARSE_TAG_HSM,
        /// <summary>Microsoft-defined reparse tag</summary>
	    HSM2 = IO_REPARSE_TAG_HSM2,
        /// <summary>Microsoft-defined reparse tag - NTFS mout point</summary>
	    MountPoint = IO_REPARSE_TAG_MOUNT_POINT,
        /// <summary>Microsoft-defined reparse tag</summary>
	    SIS = IO_REPARSE_TAG_SIS,
        /// <summary>Microsoft-defined reparse tag - symbolic link</summary>
	    Symlink = IO_REPARSE_TAG_SYMLINK
    };
   
    /// <summary>Contains information about the file that is found by the <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.FindFirst(System.String,Tools.TotalCommanderT.FindData@)"/>, or <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.FindNext(System.Object,Tools.TotalCommanderT.FindData@)"/> function.</summary>
    public value class FindData {
    private:
        /// <summary>Contains value of the <see cref="FileAttributes"/> property</summary>
        FileAttributes dwFileAttributes;
        /// <summary>Contains value of the <see cref="CreationTime"/> property</summary>
        DateTime ftCreationTime;
        /// <summary>Contains value of the <see cref="AccessTime"/> property</summary>
        DateTime ftLastAccessTime;
        /// <summary>Contains value of the <see cref="WriteTime"/> property</summary>
        DateTime ftLastWriteTime;
        /// <summary>Contains value of the high-order part of the <see cref="FileSize"/> property</summary>
        /// <remarks>This value is zero (0) unless the file size is greater than MAXDWORD.
        /// <para>The size of the file is equal to (<see cref="nFileSizeHigh"/> * (MAXDWORD+1)) + <see cref="nFileSizeLow"/>.</para></remarks>
        DWORD nFileSizeHigh;
        /// <summary>Contains value of the low-order part of the <see cref="FileSize"/> property</summary>
        DWORD nFileSizeLow;
        /// <summary>Contains value of the <see cref="ReparsePointTag"/> property</summary>
        ReparsePointTags dwReserved0;
        /// <summary>Contains value of the <see cref="Reserved1"/> property</summary>
        DWORD dwReserved1;
        /// <summary>Contains value of the <see cref="FileName"/> property</summary>
        String^ cFileName;
        /// <summary>Contains value of the <see cref="AlternateFileName"/> property</summary>
        String^ cAlternateFileName;
    internal:
        /// <summary>Ceates new instance of <see cref="FindData"/> from <see cref="WIN32_FIND_DATA"/></summary>
        /// <param name="Original"><see cref="WIN32_FIND_DATA"/> to initialize new instnce with</param>
        FindData(WIN32_FIND_DATA& Original);
        /// <summary>Converts <see cref="FindData"/> to <see cref="WIN32_FIND_DATA"/></summary>
        /// <returns><see cref="WIN32_FIND_DATA"/> created from this instance</returns>
        WIN32_FIND_DATA ToFindData();
        /// <summary>Populates given <see cref="WIN32_FIND_DATA"/> with data stored in current instance</summary>
        /// <param name="target">A <see cref="WIN32_FIND_DATA"/> to be populated</param>
        void Populate(WIN32_FIND_DATA& target);
    public:
        /// <summary>The file attributes of a file.</summary>
        /// <remarks>This property is not CLS-copliant. CLS-copliant alternatives are <see cref="GetAttributes"/> and <see cref="SetAttributes"/>.</remarks>
        [CLSCompliantAttribute(false)]
        property FileAttributes Attributes{FileAttributes get();void set(FileAttributes);}
        /// <summary>Specifies when a file or directory was created.</summary>
        /// <remarks>If the underlying file system does not support creation time, this member is zero.</remarks>
        property DateTime CreationTime{DateTime get(); void set(DateTime);}
        /// <summary>For a file, the structure specifies when the file was last read from, written to, or for executable files, run.</summary>
        /// <remarks>For a directory, the structure specifies when the directory is created. If the underlying file system does not support last access time, this member is zero.
        /// <para>On the FAT file system, the specified date for both files and directories is correct, but the time of day is always set to midnight.</para></remarks>
        property DateTime AccessTime{DateTime get(); void set(DateTime);}
        /// <summary>For a file, the structure specifies when the file was last written to, truncated, or overwritten. The date and time are not updated when file attributes or security descriptors are changed.</summary>
        /// <remarks>For a directory, the structure specifies when the directory is created. If the underlying file system does not support last write time, this member is zero.</remarks>
        property DateTime WriteTime{DateTime get(); void set(DateTime);}
        /// <summary>Size of file in bytes</summary>
        /// <remarks>This property is not CLS-compliant. CLS-cmplant alternative is to use some of following functions: <see cref="SetFileSize"/>, <see cref="GetFileSize"/>, <see cref="SetFileSizeLow"/>, <see cref="SetFileSizeHigh"/>, <see cref="GetFileSizeLow"/>, <see cref="GetFileSizeHigh"/>.</remarks>
        [CLSCompliantAttribute(false)]
        property QWORD FileSize{QWORD get(); void set(QWORD);}
        /// <summary>If the <see cref="Attributes"/> member includes the <see2 cref2="F:Tools.TotaCommanderT.FileAttributes.ReparsePoint"/> attribute, this member specifies the reparse point tag. Otherwise, this value is undefined and should not be used.</summary>
        /// <remarks>This property is not CLS-copliant. CLS-copliant alternatives are <see cref="GetReparsePointTag"/> and <see cref="SetReparsePointTag"/>.</remarks>
        [CLSCompliantAttribute(false)]
        property ReparsePointTags ReparsePointTag{ReparsePointTags get(); void set(ReparsePointTags);}
        /// <summary>Reserved for future use.</summary>
        /// <remarks>This property is not CLS-copliant. CLS-copliant alternatives are <see cref="GetReserved1"/> and <see cref="SetReserved1"/>.</remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        property DWORD Reserved1{DWORD get(); void set(DWORD);}
        /// <summary>The name of the file.</summary>
        /// <exception cref="ArgumentException">Value being set is longer than <see cref="MaxPath"/> characters</exception>
        property String^ FileName{String^ get(); void set(String^);}
        /// <summary>An alternative name for the file.</summary>
        /// <value>This name is in the classic 8.3 (filename.ext) file name format.</value>
        /// <exception cref="ArgumentException">Value being set is longer than 14 characters</exception>
        property String^ AlternateFileName{String^ get(); void set(String^);}
        /// <summary>Maximal length of path in characters</summary>
        static const int MaxPath = MAX_PATH;
#pragma region "CLS-compliance"
        /// <summary>Gets bitwise same value as <see cref="ReparsePointTag"/> as CLS-compliant type</summary>
        /// <returns>Bitwise same value as <see cref="ReparsePointTag"/></returns>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        Int32 GetReparsePointTag();
        /// <summary>Sets value of <see cref="ReparsePointTag"/> as CLS-compliant type</summary>
        /// <param name="value">New value of the <see cref="ReparsePointTag"/> property. The property will be set to bitwise same value.</param>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetReparsePointTag(Int32 value);
         
        /// <summary>Gets bitwise same value as <see cref="Reserved1"/> as CLS-compliant type</summary>
        /// <returns>Bitwise same value as <see cref="Reserved1"/></returns>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        Int32 GetReserved1();
        /// <summary>Sets value of <see cref="Reserved1"/> as CLS-compliant type</summary>
        /// <param name="value">New value of the <see cref="Reserved1"/> property. The property will be set to bitwise same value.</param>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        void SetReserved1(Int32 value);
 
        /// <summary>Gets bitwise same value as <see cref="Attributes"/> as CLS-compliant type</summary>
        /// <returns>Bitwise same value as <see cref="Attributes"/></returns>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        Int32 GetAttributes();
        /// <summary>Sets value of <see cref="Attributes"/> as CLS-compliant type</summary>
        /// <param name="value">New value of the <see cref="Attributes"/> property. The property will be set to bitwise same value.</param>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetAttributes(Int32 value);
    
        /// <summary>Sets <see cref="FileSize"/> as CLS-compliant <see cref="__int64"/></summary>
        /// <param name="value">New value of the <see cref="FileSize"/> property</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative</exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetFileSize(__int64 value);
        /// <summary>Gets <see cref="FileSize"/> as CLS-compliant <see cref="__int64"/></summary>
        /// <returns><see cref="FileSize"/></returns>
        /// <exception cref="InvalidOperationException"><see cref="FileSize"/> is greater than <see cref="__int64::MaxValue"/></exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        __int64 GetFileSize();
        /// <summary>Sets low word <see cref="FileSize"/> as CLS-compliant <see cref="__int64"/></summary>
        /// <param name="value">New value of the <see cref="FileSize"/> property's low word</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative or greater than <see cref="__int32::MaxValue"/></exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetFileSizeLow(__int64 value);
        /// <summary>Gets <see cref="FileSizeLow"/> as CLS-compliant <see cref="__int64"/></summary>
        /// <returns>Low word of <see cref="FileSize"/></returns>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        __int64 GetFileSizeLow();
        /// <summary>Sets high word <see cref="FileSize"/> as CLS-compliant <see cref="__int64"/></summary>
        /// <param name="value">New value of the <see cref="FileSize"/> property's high word</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative or greater than <see cref="__int32::MaxValue"/></exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetFileSizeHigh(__int64 value);
        /// <summary>Gets high word <see cref="FileSize"/> as CLS-compliant <see cref="__int64"/></summary>
        /// <returns>High word of <see cref="FileSize"/></returns>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        __int64 GetFileSizeHigh();
#pragma endregion
    };

   
    /// <summary>When user input is requested by plugin, one of those values may be used</summary>
    public enum class InputRequestKind{
        /// <summary>The requested string is none of the default types</summary>
        Other = RT_Other,
        /// <summary>Ask for the user name, e.g. for a connection</summary>
        UserName = RT_UserName,
        /// <summary>Ask for a password, e.g. for a connection (shows ***)</summary>
        Pasword = RT_Password,
        /// <summary>Ask for an account (needed for some FTP servers)</summary>
        Account = RT_Account,
        /// <summary>User name for a firewall</summary>
        UserNameFirewall = RT_UserNameFirewall,
        /// <summary>Password for a firewall</summary>
        PasswordFirewall = RT_PasswordFirewall,
        /// <summary>Asks for a local directory (with browse button)</summary>
        TargetDir = RT_TargetDir,
        /// <summary>Asks for an URL</summary>
        URL = RT_URL,
        /// <summary>Shows MessageBox with OK button</summary>
        MsgOK = RT_MsgOK,
        /// <summary>Shows MessageBox with Yes/No buttons</summary>
        MsgYesNo = RT_MsgYesNo,
        /// <summary>Shows MessageBox with OK/Cancel buttons</summary>
        MsgOKCancel = RT_MsgOKCancel
    };

    /// <summary>When log is issued its kind is denoted with one of following flags</summary>
    /// <remarks>Total Commander supports logging to files. While one log file will store all messages, the other will only store important errors, connects, disconnects and complete operations/transfers, but not messages of type <see2 cref2="F:Tools.TotalCommanderT.LogKind.Details"/>.</remarks>
    public enum class LogKind{
         /// <summary>Connect to a file system requiring disconnect</summary>
         Connect = MSGTYPE_CONNECT,
         /// <summary>Disconnected successfully</summary>
         Disconnect = MSGTYPE_DISCONNECT,
         /// <summary>Not so important messages like directory changing</summary>
         Details = MSGTYPE_DETAILS,
         /// <summary>A file transfer was completed successfully</summary>
         TransferComplete = MSGTYPE_TRANSFERCOMPLETE,
         /// <summary>unused</summary>
         ConnectComplete = MSGTYPE_CONNECTCOMPLETE,
         /// <summary>An important error has occured</summary>
         ImportantError = MSGTYPE_IMPORTANTERROR,
         /// <summary>An operation other than a file transfer has completed</summary>
         OperationComplete = MSGTYPE_OPERATIONCOMPLETE
    };

    /// <summary>Operation status</summary>
    public enum class OperationStatus{
        /// <summary>Operation starts (allocate buffers if needed)</summary>
        Start = FS_STATUS_START,
        /// <summary>Operation has ended (free buffers, flush cache etc)</summary>
        End = FS_STATUS_END
    };
    /// <summary>Kinds of operations</summary>
    public enum class OperationKind{
        /// <summary>Retrieve a directory listing</summary>
        List = FS_STATUS_OP_LIST,
        /// <summary>Get a single file from the plugin file system</summary>
        GetSingle = FS_STATUS_OP_GET_SINGLE,
        /// <summary>Get multiple files, may include subdirs</summary>
        GetMulti = FS_STATUS_OP_GET_MULTI,
        /// <summary>Put a single file to the plugin file system</summary>
        PuSingle = FS_STATUS_OP_PUT_SINGLE,
        /// <summary>Put multiple files, may include subdirs</summary>
        PuMulti = FS_STATUS_OP_PUT_MULTI,
        /// <summary>Rename/Move/Remote copy a single file</summary>
        RenMovSingle = FS_STATUS_OP_RENMOV_SINGLE,
        /// <summary>RenMov multiple files, may include subdirs</summary>
        RenMovMulti = FS_STATUS_OP_RENMOV_MULTI,
        /// <summary>Delete multiple files, may include subdirs</summary>
        Delete = FS_STATUS_OP_DELETE,
        /// <summary>Change attributes/times, may include subdirs</summary>
        Attrib = FS_STATUS_OP_ATTRIB,
        /// <summary>Create a single directory</summary>
        MkDir = FS_STATUS_OP_MKDIR,
        /// <summary>Start a single remote item, or a command line</summary>
        Exec = FS_STATUS_OP_EXEC,
        /// <summary>Calculating size of subdir (user pressed SPACE)</summary>
        CalcSize = FS_STATUS_OP_CALCSIZE,
        /// <summary>Searching for file names only (using <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.FindFirst(System.String,Tools.TotalCommanderT.FindData@)"/>/<see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.FindNext(System.Object,Tools.TotalCommanderT.FindData@)"/>/<see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.FindNext(System.Object"/>)</summary>
        Search = FS_STATUS_OP_SEARCH,	
        /// <summary>Searching for file contents (using also <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.GetFile(System.String,System.String@,Tools.TotalCommanderT.CopyFlags,Tools.TotalCommanderT.RemoteInfo)"/> calls)</summary>
        SerachText = FS_STATUS_OP_SEARCH_TEXT,	
        /// <summary>Synchronize dirs searches subdirs for info</summary>
        SyncSearch = FS_STATUS_OP_SYNC_SEARCH,	
        /// <summary>Synchronize: Downloading files from plugin</summary>
        SyncGet = FS_STATUS_OP_SYNC_GET,	
        /// <summary>Synchronize: Uploading files to plugin</summary>
        SyncPut = FS_STATUS_OP_SYNC_PUT,	
        /// <summary>Synchronize: Deleting files from plugin</summary>
        SyncDelete = FS_STATUS_OP_SYNC_DELETE	
    };

    /// <summary>Arguments of operation status notifications</summary>
    public ref class OperationEventArgs : EventArgs{
    private:
        /// <summary>Contains value of the <see cref="RemoteDir"/> property</summary>
        String^ remoteDir;
        /// <summary>Contains value of the <see cref="Kind"/> property</summary>
        OperationKind kind;
        /// <summary>Contains value of the <see cref="Status"/> property</summary>
        OperationStatus status;
    public:
        /// <summary>CTor - Creates new instance of the <see cref="OperationEventArgs"/> class</summary>
        /// <param name="remoteDir">This is the current source directory when the operation starts. May be used to find out which part of the file system is affected.</param>
        /// <param name="status">Information whether the operation starts or ends</param>
        /// <param name="kind">Information of which operaration starts/ends</param>
        /// <exception cref="ArgumentNullException"><paramref name="remoteDir"/> is null</exception>
        OperationEventArgs(String^ remoteDir, OperationKind kind, OperationStatus status);
        /// <summary>This is the current source directory when the operation starts. May be used to find out which part of the file system is affected.</summary>
        property String^ RemoteDir{String^ get();}
        /// <summary>Information of which operaration starts/ends.</summary>
        property OperationKind Kind{OperationKind get();}
        /// <summary>Information whether the operation starts or ends.</summary>
        property OperationStatus Status{OperationStatus get();}
    };

    /// <summary>Contains details about the remote file being copied.</summary>
    public value class RemoteInfo{
    public:
        /// <summary>Low DWORD of remote file size. Useful for a progress indicator.</summary>
        /// <remarks>This property is not CLS-compliat. CLS-compliant alternative is to use <see cref="SetSizeLow"/> and <see cref="GetSizeLow"/>.</remarks>
        [CLSCompliantAttribute(false)]
        property DWORD SizeLow;
        /// <summary>High DWORD of remote file size. Useful for a progress indicator.</summary>
        /// <remarks>This property is not CLS-compliat. CLS-compliant alternative is to use <see cref="SetSizeHigh"/> and <see cref="GetSizeHigh"/>.</remarks>
        [CLSCompliantAttribute(false)]
        property DWORD SizeHigh;
        /// <summary>Time stamp of the remote file - should be copied with the file.</summary>
        property DateTime LastWriteTime;
        /// <summary>Attributes of the remote file - should be copied with the file.</summary>
        property FileAttributes Attr;
        /// <summary>Remote file size. Useful for a progress indicator.</summary>
        /// <remarks>This property is not CLS-compliant. CLS-compliant alternative is to use <see cref="SetSize"/> and <see cref="GetSize"/>.</remarks>
        [CLSCompliantAttribute(false)]
        property QWORD Size{QWORD get();void set(QWORD);}
#pragma region "CLS-compliance"
        /// <summary>Sets <see cref="Size"/> as CLS-compliant <see cref="__int64"/></summary>
        /// <param name="value">New value of the <see cref="Size"/> property</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative</exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetSize(__int64 value);
        /// <summary>Gets <see cref="Size"/> as CLS-compliant <see cref="__int64"/></summary>
        /// <returns><see cref="Size"/></returns>
        /// <exception cref="InvalidOperationException"><see cref="Size"/> is greater than <see cref="__int64::MaxValue"/></exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        __int64 GetSize();
        /// <summary>Sets <see cref="SizeLow"/> as CLS-compliant <see cref="__int64"/></summary>
        /// <param name="value">New value of the <see cref="SizeLow"/> property</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative or greater than <see cref="__int32::MaxValue"/></exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetSizeLow(__int64 value);
        /// <summary>Gets <see cref="SizeLow"/> as CLS-compliant <see cref="__int64"/></summary>
        /// <returns><see cref="SizeLow"/></returns>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        __int64 GetSizeLow();
        /// <summary>Sets <see cref="SizeHigh"/> as CLS-compliant <see cref="__int64"/></summary>
        /// <param name="value">New value of the <see cref="SizeHigh"/> property</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative or greater than <see cref="__int32::MaxValue"/></exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetSizeHigh(__int64 value);
        /// <summary>Gets <see cref="SizeHigh"/> as CLS-compliant <see cref="__int64"/></summary>
        /// <returns><see cref="SizeHigh"/></returns>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        __int64 GetSizeHigh();
#pragma endregion
    internal:
        /// <summary>CTor from <see cref="RemoteInfoStruct"/></summary>
        /// <param name="ri"><see cref="RemoteInfoStruct"/> to initialize new instance with</param>
        RemoteInfo(const RemoteInfoStruct& ri);
    };

    
    /// <summary>Abstract base class for Total Commander file-system plugins (wfx)</summary>
    public ref class FileSystemPlugin abstract : ContentPluginBase {
    protected:
        FileSystemPlugin();
#pragma region "Delegates"
    public:
        /// <summary>Callback function, which the plugin can call to show copy progress.</summary>
        /// <param name="SourceName">Name of the source file being copied. Depending on the direction of the operation (Get, Put), this may be a local file name of a name in the plugin file system.</param>
        /// <param name="TargetName">Name to which the file is copied.</param>
        /// <param name="PercentDone">Percentage of THIS file being copied. Total Commander automatically shows a second percent bar if possible when multiple files are copied.</param>
        /// <returns>Total Commander returns <c>true</c> if the user wants to abort copying, and <c>false</c> if the operation can continue.</returns>
        /// <remarks>You should call this function at least twice in the copy functions <see cref="GetFile"/>, <see cref="PutFile"/> and <see cref="RenMovFile"/>, at the beginning and at the end. If you can't determine the progress, call it with 0% at the beginning and 100% at the end.
        /// <para>During the <see cref="FindFirst"/>/<see cref="FindNext"/>/<see cref="FindClose"/> loop, the plugin may now call the <see cref="ProgressProc"/> to make a progess dialog appear. This is useful for very slow connections. Don't call <see cref="ProgressProc"/> for fast connections! The progress dialog will only be shown for normal dir changes, not for compound operations like get/put. The calls to <see cref="ProgressProc"/> will also be ignored during the first 5 seconds, so the user isn't bothered with a progress dialog on every dir change.</para></remarks>
        delegate bool ProgressCallback(FileSystemPlugin^ sender, String^ SourceName, String^ TargetName,int PercentDone);
        /// <summary>Callback function, which the plugin can call to show the FTP connections toolbar, and to pass log messages to it. Totalcmd can show these messages in the log window (ftp toolbar) and write them to a log file.</summary>
        /// <param name="MsgType">Can be one of the <see cref="LogKind"/> flags</param>
        /// <param name="LogString">String which should be logged.
        /// <para>When <paramref name="MsgType"/>is <see2 cref2="F:Tools.TotaCommanderT.LogKind.Connect"/>, the string MUST have a specific format:</para>
        /// <para><c>"CONNECT"</c> followed by a single whitespace, then the root of the file system which was connected, without trailing backslash. Example: <c>CONNECT \Filesystem</c></para>
        /// <para>When <paramref name="MsgType"/> is <see2 cref2="F:Tools.TotaCommanderT.LogKind.TransferComplete"/>, this parameter should contain both the source and target names, separated by an arrow <c>" -> "</c>, e.g. <c>Download complete: \Filesystem\dir1\file1.txt -> c:\localdir\file1.txt</c></para></param>
        /// <remarks>Do NOT call <see cref="LogProc"/> with <see2 cref2="F:Tools.TotaCommanderT.LogKind.Connect"/> if your plugin does not require connect/disconnect! If you call it with <paramref name="MsgType"/> <see2 cref2="F:Tools.TotaCommanderT.LogKind.Connect"/>, the function <see cref="Disconnect"/> will be called (if defined) when the user presses the Disconnect button.</remarks>
        delegate void LogCallback(FileSystemPlugin^ sender, LogKind MsgType,String^ LogString);
        /// <summary>callback function, which the plugin can call to request input from the user. When using one of the standard parameters, the request will be in the selected language.</summary>
        /// <param name="RequestType">Can be one of the <see cref="InputRequestKind"/> flags</param>
        /// <param name="CustomTitle">Custom title for the dialog box. If NULL or empty, it will be "Total Commander"</param>
        /// <param name="CustomText">Override the text defined with <paramref name="RequestType"/>. Set this to NULL or an empty string to use the default text. The default text will be translated to the language set in the calling program.</param>
        /// <param name="DefaultText">This string contains the default text presented to the user. Set <paramref name="DefaultText"/>[0]=0 to have no default text.</param>
        /// <param name="maxlen">Maximum length allowed for returned text.</param>
        /// <returns>User-entered text if user clicked Yes or OK. Null otherwise</returns>
        /// <remarks>Leave <paramref name="CustomText"/> empty if you want to use the (translated) default strings!</remarks>
        /// <exception cref="ArgumentException"><paramref name="DefaultText"/> is longer than <paramref name="maxlen"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxlen"/> is less than 1</exception>
        delegate String^ RequestCallback(FileSystemPlugin^ sender, InputRequestKind RequestType,String^ CustomTitle, String^ CustomText, String^ DefaultText, int maxlen);
#pragma endregion
#pragma region "TC functions"
    private:
        /// <summary>Pointer to progress procedure</summary>
        tProgressProc pProgressProc;
        /// <summary>Pointer to log procedure</summary>
        tLogProc pLogProc;
        /// <summary>Pointer to request procedure</summary>
        tRequestProc pRequestProc;
        /// <summary>When plugin is used outside of Total Commander, used instead of <see cref="pProgressProc"/></summary>
        ProgressCallback^ dProgressProc;
        /// <summary>When plugin is used outside of Total Commander, used instead of <see cref="pLogProc"/></summary>
        LogCallback^ dLogProc;
        /// <summary>When plugin is used outside of Total Commander, used instead of <see cref="pRequestProc"/></summary>
        RequestCallback^ dRequestProc;
    public:
        /// <summary>Called when loading the plugin. The passed values should be stored in the plugin for later use.</summary>
        /// <param name="PluginNr">Internal number this plugin was given in Total Commander. Has to be passed as the first parameter in all callback functions so Totalcmd knows which plugin has sent the request.</param>
        /// <param name="pProgressProc">Pointer to the progress callback function.</param>
        /// <param name="pLogProc">Pointer to the logging function</param>
        /// <param name="pRequestProc">Pointer to the request text proc</param>
        /// <returns>The return value is currently unused. You should return 0 when successful.</returns>
        /// <remarks><see cref="FsInit"/> is NOT called when the user initially installs the plugin. Only <se cref="FsGetDefRootName"/>.is called in this case, and then the plugin DLL is unloaded again. The plugin DLL is loaded when the user enters the plugin root in Network Neighborhood.
        /// <para>This function is called by Total Commander and is not intended for direct use. If you need use plugin outside of Total Commander use <see cref="InitializePlugin"/> instead.</para></remarks>
        /// <exception cref="InvalidOperationException"><see cref="Initialized"/> is true</exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        int FsInit(int PluginNr,tProgressProc pProgressProc, tLogProc pLogProc,tRequestProc pRequestProc);
        /// <summary>Called when loading the plugin outside of Total Comander environment instead of <see cref="FsInit"/>. The passed values should be stored in the plugin for later use.</summary>
        /// <param name="PluginNr">Internal number this plugin was given in Total Commander. Has to be passed as the first parameter in all callback functions so Totalcmd knows which plugin has sent the request.</param>
        /// <param name="progress">Delegate to the progress callback function.</param>
        /// <param name="log">Delegate to the logging function</param>
        /// <param name="request">Delegate to the request text proc</param>
        /// <exception cref="InvalidOperationException"><see cref="Initialized"/> is true</exception>
        /// <exception cref="ArgumentNullException"><paramref name="progress"/>, <paramref name="log"/> or <paramref name="request"/> is null</exception>
        /// <remarks>Use this function to initialize the plugin when used outside of Total Commander</remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void InitializePlugin(int PluginNr, ProgressCallback^ progress, LogCallback^ log, RequestCallback^ request);
        /// <summary>When plugin is initialized, gets value indicating if it was initialiuzed by Total Commander or .NET application</summary>
        /// <returns>True if plugin was initialized by Total Commander; false when it was initialized by .NET application</returns>
        /// <exception cref="InvalidOperationException"><see cref="Initialized"/> is false</exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        property bool IsInTotalCommander{bool get();}
        /// <summary>Called to retrieve the first file in a directory of the plugin's file system.</summary>
        /// <param name="Path">Full path to the directory for which the directory listing has to be retrieved. Important: no wildcards are passed to the plugin! All separators will be backslashes, so you will need to convert them to forward slashes if your file system uses them!
        /// <para>As root, a single backslash is passed to the plugin. The root items appear in the plugin base directory retrieved by <see cref="FsGetDefRootName"/> at installation time. This default root name is NOT part of the path passed to the plugin!</para>
        /// <para>All subdirs are built from the directory names the plugin returns through <see cref="FsFindFirst"/> and <see cref="FsFindNext"/>, separated by single backslashes, e.g. \Some server\c:\subdir</para></param>
        /// <param name="FindData">A standard <see cref="WIN32_FIND_DATA"/> struct as defined in the Windows SDK, which contains the file or directory details. Use the dwFileAttributes field set to <see2 cref2="F:Tools.TotaCommanderT.FileAttributes.Directory"/> to distinguish files from directories. On Unix systems, you can | (or) the dwFileAttributes field with 0x80000000 and set the dwReserved0 parameter to the Unix file mode (permissions).</param>
        /// <returns>Return INVALID_HANDLE_VALUE (==-1, not zero!) if an error occurs, or a number of your choice if not. It is recommended to pass a pointer to an internal structure as this handle, which stores the current state of the search. This will allow recursive directory searches needed for copying whole trees. This handle will be passed to <see cref="FsFindNext"/> by the calling program.
        /// <para>When an error occurs, call <see cref="SetLastError"/> to set the reason of the error. Total Commander checks for the following two errors:</para>
        /// <list type="numbered"><item>ERROR_NO_MORE_FILES: The directory exists, but it's empty (Totalcmd can open it, e.g. to copy files to it)</item>
        /// <item>Any other error: The directory does not exist, and Total Commander will not try to open it.</item></list></returns>
        /// <remarks><see cref="FsFindFirst"/> may be called directly with a subdirectory of the plugin! You cannot rely on it being called with the root \ after it is loaded. Reason: Users may have saved a subdirectory to the plugin in the Ctrl+D directory hotlist in a previous session with the plugin.
        /// <para>This function is called by Total Commander and is not intended for direct use</para></remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        HANDLE FsFindFirst(char* Path,WIN32_FIND_DATA *FindData);
        /// <summary>Called to retrieve the next file in a directory of the plugin's file system</summary>
        /// <param name="Hdl">The find handle returned by <see cref="FsFindFirst"/>.</param>
        /// <param name="FindData">A standard <see cref="WIN32_FIND_DATA"/> struct as defined in the Windows SDK, which contains the file or directory details. Use the dwFileAttributes field set to <see2 cref2="F:Tools.TotaCommanderT.FileAttributes.Directory"/> to distinguish files from directories. On Unix systems, you can | (or) the dwFileAttributes field with 0x80000000 and set the dwReserved0 parameter to the Unix file mode (permissions).</param>
        /// <returns>Return FALSE if an error occurs or if there are no more files, and TRUE otherwise. <see cref="SetLastError"/>() does not need to be called.</returns>
        /// <remarks><para>This function is called by Total Commander and is not intended for direct use</para></remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        BOOL FsFindNext(HANDLE Hdl,WIN32_FIND_DATA *FindData);
        /// <summary>Called to end a <see cref="FsFindFirst"/>/<see cref="FsFindNext"/> loop, either after retrieving all files, or when the user aborts it</summary>
        /// <param name="Hdl">The find handle returned by <see cref="FsFindFirst"/>.</param>
        /// <returns>Currently unused, should return 0.</returns>
        /// <remarks><para>This function is called by Total Commander and is not intended for direct use</para></remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        int FsFindClose(HANDLE Hdl);
#pragma endregion
#pragma region ".NET Functions"
    private:
        /// <summary>Contains value of the <see cref="Initialized"/> property</summary>
        bool initialized;
        /// <summary>Contains value of the <see cref="PluginNr"/> property</summary>
        int pluginNr;
    protected:
        /// <summary>Gets plugin number this plugin instance is recognized by Total Commender under</summary>
        /// <exception cref="InvalidOperationException"><see cref="Initialized"/> is false</exception>
        property int PluginNr{int get();}
    public:
        /// <summary>Gets value indicating if this plugin instance was initialized or not</summary>
        property bool Initialized{bool get();}
    protected:
        /// <summary>When overriden in derived class provides custom code invoked when plugin is initialized.</summary>
        /// <remarks>When this method is called the <see cref="Initialized"/> property has value true and <see cref="PluginNr"/> is already set.
        /// <para>Default implementation of this method does nothing</para></remarks>
        virtual void OnInit();
#pragma region "Callbacks"
        /// <summary>Callback function, which the plugin can call to show copy progress.</summary>
        /// <param name="SourceName">Name of the source file being copied. Depending on the direction of the operation (Get, Put), this may be a local file name of a name in the plugin file system.</param>
        /// <param name="TargetName">Name to which the file is copied.</param>
        /// <param name="PercentDone">Percentage of THIS file being copied. Total Commander automatically shows a second percent bar if possible when multiple files are copied.</param>
        /// <returns>Total Commander returns <c>true</c> if the user wants to abort copying, and <c>false</c> if the operation can continue.</returns>
        /// <remarks>You should call this function at least twice in the copy functions <see cref="GetFile"/>, <see cref="PutFile"/> and <see cref="RenMovFile"/>, at the beginning and at the end. If you can't determine the progress, call it with 0% at the beginning and 100% at the end.
        /// <para>During the <see cref="FindFirst"/>/<see cref="FindNext"/>/<see cref="FindClose"/> loop, the plugin may now call the <see cref="ProgressProc"/> to make a progess dialog appear. This is useful for very slow connections. Don't call <see cref="ProgressProc"/> for fast connections! The progress dialog will only be shown for normal dir changes, not for compound operations like get/put. The calls to <see cref="ProgressProc"/> will also be ignored during the first 5 seconds, so the user isn't bothered with a progress dialog on every dir change.</para></remarks>
        /// <exception cref="InvalidOperationException"><see cref="Initialized"/> is false</exception>
        bool ProgressProc(String^ SourceName, String^ TargetName,int PercentDone);
        /// <summary>Callback function, which the plugin can call to show the FTP connections toolbar, and to pass log messages to it. Totalcmd can show these messages in the log window (ftp toolbar) and write them to a log file.</summary>
        /// <param name="MsgType">Can be one of the <see cref="LogKind"/> flags</param>
        /// <param name="LogString">String which should be logged.
        /// <para>When <paramref name="MsgType"/>is <see2 cref2="F:Tools.TotaCommanderT.LogKind.Connect"/>, the string MUST have a specific format:</para>
        /// <para><c>"CONNECT"</c> followed by a single whitespace, then the root of the file system which was connected, without trailing backslash. Example: <c>CONNECT \Filesystem</c></para>
        /// <para>When <paramref name="MsgType"/> is <see2 cref2="F:Tools.TotaCommanderT.LogKind.TransferComplete"/>, this parameter should contain both the source and target names, separated by an arrow <c>" -> "</c>, e.g. <c>Download complete: \Filesystem\dir1\file1.txt -> c:\localdir\file1.txt</c></para></param>
        /// <remarks>Do NOT call <see cref="LogProc"/> with <see2 cref2="F:Tools.TotaCommanderT.LogKind.Connect"/> if your plugin does not require connect/disconnect! If you call it with <paramref name="MsgType"/> <see2 cref2="F:Tools.TotaCommanderT.LogKind.Connect"/>, the function <see cref="Disconnect"/> will be called (if defined) when the user presses the Disconnect button.</remarks>
        /// <exception cref="InvalidOperationException"><see cref="Initialized"/> is false</exception>
        void LogProc(LogKind MsgType,String^ LogString);
        /// <summary>callback function, which the plugin can call to request input from the user. When using one of the standard parameters, the request will be in the selected language.</summary>
        /// <param name="RequestType">Can be one of the <see cref="InputRequestKind"/> flags</param>
        /// <param name="CustomTitle">Custom title for the dialog box. If NULL or empty, it will be "Total Commander"</param>
        /// <param name="CustomText">Override the text defined with <paramref name="RequestType"/>. Set this to NULL or an empty string to use the default text. The default text will be translated to the language set in the calling program.</param>
        /// <param name="DefaultText">This string contains the default text presented to the user. Set <paramref name="DefaultText"/>[0]=0 to have no default text.</param>
        /// <param name="maxlen">Maximum length allowed for returned text.</param>
        /// <returns>User-entered text if user clicked Yes or OK. Null otherwise</returns>
        /// <remarks>Leave <paramref name="CustomText"/> empty if you want to use the (translated) default strings!</remarks>
        /// <exception cref="InvalidOperationException"><see cref="Initialized"/> is false</exception>
        /// <exception cref="ArgumentException"><paramref name="DefaultText"/> is longer than <paramref name="maxlen"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxlen"/> is less than 1</exception>
        String^ RequestProc(InputRequestKind RequestType,String^ CustomTitle, String^ CustomText, String^ DefaultText, int maxlen);
#pragma endregion
    private:
        /// <summary>Contains handle-object dictionary for objects returned by <see cref="FindFirst"/> and <see cref="FindNext"/></summary>
        Collections::Generic::Dictionary<int,Object^>^ handleDictionary ;
        /// <summary>Contains maximum key in <see cref="HandleDictionary"/></summary>
        int MaxHandle;
        /// <summary>Used to synchronize access to <see cref="HandleDictionary"/></summary>
        Object^ HandleSyncObj;
    protected:
        /// <summary>Gets dictionary containing objects referenced by Total Commander by handles</summary>
        /// <remarks>Do not add/remove items form this collection directly. Use dedicated Handle* functions instead (they are thread-safe).
        /// <para>In rare occasions, your plugin is not utilized by Total Commander but by another .NET-based application, you cannot rely on objects being passed to <see cref="FindNext"/>/<see cref="FindClose"/> being present in this dictionary and object returned by <se cref="FindFirst"/> being added to this dictionary. It's because .NET application doesn't need to rely on integer handles - it can store objects itself.</para></remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        property Collections::Generic::Dictionary<int,Object^>^ HandleDictionary{Collections::Generic::Dictionary<int,Object^>^ get();}
        /// <summary>Gets next free handle for <see cref="HandleDictionary"/></summary>
        /// <returns>Always returns value greater than zero</returns>
        /// <threadsafety>This function is tread-safe</threadsafety>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        int GetNextHandle();
        /// <summary>Adds object to <see cref="HandleDictionary"/></summary>
        /// <param name="object">Object to add and obtain handle for</param>
        /// <returns>Handle assigned to object</returns>
        /// <remarks>You can assign multiple handles to same object.</remarks>
        /// <threadsafety>This function is tread-safe</threadsafety>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        int HandleAdd(Object^ object);
        /// <summary>Removes object from <see cref="HandleDictionary"/></summary>
        /// <param name="object">Object to be removed</param>
        /// <returns>True if object was present in <see cref="HandleDictionary"/> and it was removed; false if it was not present</returns>
        /// <remarks>If object has multiple handles assigned only the first handle is destroyed</remarks>
        /// <threadsafety>This function is tread-safe</threadsafety>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        bool HandleRemove(Object^ object);
        /// <summary>Removes object from <see cref="HandleDictionary"/> identified by integral handle</summary>
        /// <param name="handle">Handle of object to be removed</param>
        /// <returns>Ture if <paramref name="handle"/> was defined and it was removed; false if <paramref name="handle"/> was not defined (i.e. it was previously destroyed or never created)</returns>
        /// <threadsafety>This function is tread-safe</threadsafety>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        bool HandleRemove(int handle);
        /// <summary>Gets object identified by handle</summary>
        /// <param name="handle">Handle to get object for</param>
        /// <returns>The object stored in <see cref="HandleDictionary"/> under key <paramref name="handle"/>; null where <paramref name="handle"/> is nod defined.</returns>
        /// <remarks>In case you store null objects in <see cref="HandleDictionary"/> this function returns null either when <paramref name="handle"/> is invalid or objects stored under the handle is null. So, do not store null objects in <ses cref="HandleDictionary"/>.</remarks>
        /// <threadsafety>This function is tread-safe</threadsafety>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        Object^ HandleGet(int handle);
        /// <summary>Gets handle of object in <see cref="HandleDictionary"/></summary>
        /// <param name="object">Object to get handle of</param>
        /// <returns>Handle of <paramref name="object"/>; of -1 if <paramref name="object"/> is not present in <see cref="HandleDictionary"/></returns>
        /// <threadsafety>This function is tread-safe</threadsafety>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        int HandleGetHandle(Object^ object);
        /// <summary>Raplaces object in <see cref="HandleDictionary"/> with another one.</summary>
        /// <param name="handle">Handle to replace object for</param>
        /// <param name="object">New object to store with handle <paramref name="handle"/></param>
        /// <exception cref="System::Collections::Generic::KeyNotFoundException"><paramref name="handle"/> is nod defined</exception>
        /// <threadsafety>This function is tread-safe</threadsafety>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void HandleReplace(int handle, Object^ object);
    public:
        /// <summary>When overriden in derived class retrieves the first file in a directory of the plugin's file system.</summary>
        /// <param name="Path">Full path to the directory for which the directory listing has to be retrieved. Important: no wildcards are passed to the plugin! All separators will be backslashes, so you will need to convert them to forward slashes if your file system uses them!
        /// <para>As root, a single backslash is passed to the plugin. The root items appear in the plugin base directory retrieved by <see cref="FsGetDefRootName"/> at installation time. This default root name is NOT part of the path passed to the plugin!</para>
        /// <para>All subdirs are built from the directory names the plugin returns through <see cref="FindFirst"/> and <see cref="FindNext"/>, separated by single backslashes, e.g. \Some server\c:\subdir</para></param>
        /// <param name="FindData">A <see cref="FindData"/> struct (mimics WIN32_FIND_DATA as defined in the Windows SDK) to be pupulated with the file or directory details. Use the <see cref="FindData::Attributes"/> field set to <see2 cref2="F:Tools.TotaCommanderT.FileAttributes.Directory"/> to distinguish files from directories. On Unix systems, you can | (or) the <see cref="FindData::Attributes"/> field with 0x80000000 and set the <see cref="FindData::ReparsePointTag"/> parameter to the Unix file mode (permissions).</param>
        /// <returns>Any object. It is recommended to return object that represents current state of the search. This will allow recursive directory searches needed for copying whole trees. This object will be passed to <see cref="FindNext"/> by the calling program.
        /// Returned object is added to <see cref="HandleDictionary"/>
        /// <para>Null if there are no more files.</para></returns>
        /// <exception cref="IO:DirectoryNotFoundException">Directory does not exists</exception>
        /// <exception cref="UnauthorizedAccessException">The user does not have access to the directory</exception>
        /// <exception cref="Security::SecurityException">Security error detected</exception>
        /// <exception cref="IO:IOException">Another error occured</exception>
        /// <remarks><see cref="FsFindFirst"/> may be called directly with a subdirectory of the plugin! You cannot rely on it being called with the root \ after it is loaded. Reason: Users may have saved a subdirectory to the plugin in the Ctrl+D directory hotlist in a previous session with the plugin.
        /// <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
        virtual Object^ FindFirst(String^ Path,FindData% FindData) abstract;
        /// <summary>When overriden in derived class retrieves the next file in a directory of the plugin's file system</summary>
        /// <param name="Status">The object returned by <see cref="FindFirst"/>; null when Total Commander supplied handle that is  not in <see cref="HandleDictionary"/></param>
        /// <param name="FindData">A <see cref="FindData"/> struct (mimics WIN32_FIND_DATA as defined in the Windows SDK) to be pupulated with the file or directory details. Use the <see cref="FindData::Attributes"/> field set to <see2 cref2="F:Tools.TotaCommanderT.FileAttributes.Directory"/> to distinguish files from directories. On Unix systems, you can | (or) the <see cref="FindData::Attributes"/> field with 0x80000000 and set the <see cref="FindData::ReparsePointTag"/> parameter to the Unix file mode (permissions).</param>
        /// <returns>Return false if there are no more files, and true otherwise. <see cref="SetLastError"/>() does not need to be called.</returns>
        /// <exception cref="IO:DirectoryNotFoundException">Directory does not exists</exception>
        /// <exception cref="UnauthorizedAccessException">The user does not have access to the directory</exception>
        /// <exception cref="Security::SecurityException">Security error detected</exception>
        /// <exception cref="IO:IOException">Another error occured</exception>
        /// <remarks><note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
        virtual bool FindNext(Object^ Status, FindData% FindData) abstract;
        /// <summary>When overriden in derived class performs custom clenup at end of a <see cref="FindFirst"/>/<see cref="FindNext"/> loop, either after retrieving all files, or when the user aborts it.</summary>
        /// <param name="Status">The object returned by <see cref="FindFirst"/>; null when Total Commander supplied handle that is  not in <see cref="HandleDictionary"/>. When this function exists, <paramref name="Status"/> automatically removed from <see cref="HandleDictionary"/></param>
        virtual void FindClose(Object^ Status);
#pragma endregion
#pragma region "Optional methods"
    public:
        /// <summary>Create a directory on the plugin's file system.</summary>
        /// <param name="Path">Name of the directory to be created, with full path. The name always starts with a backslash, then the names returned by <see cref="FsFindFirst"/>/<see cref="FsFindNext"/> separated by backslashes.</param>
        /// <returns>Return TRUE if the directory could be created, FALSE if not.</returns>
        /// <remarks><para>This function is called by Total Commander and is not intended for direct use</para></remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        BOOL FsMkDir(char* Path);
    public:
        /// <summary>When overriden in derived class creates a directory on the plugin's file system.</summary>
        /// <param name="Path">Name of the directory to be created, with full path. The name always starts with a backslash, then the names returned by <see cref="FsFindFirst"/>/<see cref="FsFindNext"/> separated by backslashes.</param>
        /// <returns>Return true if the directory could be created, false if not.</returns>
        /// <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
        /// <exception cref="Security::SecurityException">Security error detected</exception>
        /// <exception cref="IO:IOException">An IO error occured</exception>
        /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
        /// <remarks>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.
        /// <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
        [MethodNotSupportedAttribute]
        virtual bool MkDir(String^ Path);
    public:
        /// <summary>Called to execute a file on the plugin's file system, or show its property sheet. It is also called to show a plugin configuration dialog when the user right clicks on the plugin root and chooses 'properties'. The plugin is then called with <paramref name="RemoteName"/>="\" and <paramref name="Verb"/>="properties" (requires TC>=5.51).</summary>
        /// <param name="MainWin">Parent window which can be used for showing a property sheet.</param>
        /// <param name="RemoteName">Name of the file to be executed, with full path.</param>
        /// <param name="Verb">This can be either "<c>open</c>", "<c>properties</c>", "<c>chmod</c>" or "<c>quote</c>" (case-insensitive).</param>
        /// <returns>Return <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.Yourself"/> if Total Commander should download the file and execute it locally, <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.OK"/> if the command was executed successfully in the plugin (or if the command isn't applicable and no further action is needed), <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.Error"/> if execution failed, or <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.Symlink"/> if this was a (symbolic) link or .lnk file pointing to a different directory.</returns>
        /// <remarks>Meaning of verbs:
        /// <list type="table"><listheader><term>verb</term><description>meaning</description></listheader>
        /// <item><term>open</term><description>This is called when the user presses ENTER on a file. There are three ways to handle it:
        /// <list type="bulet">
        /// <item>For internal commands like "Add new connection", execute it in the plugin and return <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.OK"/> or <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.Error"/></item>
        /// <item>Let Total Commander download the file and execute it locally: return <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.Yourself"/></item>
        /// <item>If the file is a (symbolic) link, set <paramref name="RemoteName"/> to the location to which the link points (including the full plugin path), and return <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.Symlink"/>. Total Commander will then switch to that directory. You can also switch to a directory on the local harddisk! To do this, return a path starting either with a drive letter, or an UNC location (\\server\share). The maximum allowed length of such a path is MAX_PATH-1 = 259 characters!</item>
        /// </list></description></item>
        /// <item><term>properties</term><description>Show a property sheet for the file (optional). Currently not handled by internal Totalcmd functions if <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.Yourself"/> is returned, so the plugin needs to do it internally.</description></item>
        /// <item><term>chmod xxx</term><description>The xxx stands for the new Unix mode (attributes) to be applied to the file <paramref name="RemoteName"/>. This verb is only used when returning Unix attributes through <see cref="FsFindFirst"/>/<see cref="FsFindNext"/></description></item>
        /// <item><term>quote commandline</term><description>Execute the command line entered by the user in the directory <paramref name="RemoteName"/> . This is called when the user enters a command in Totalcmd's command line, and presses ENTER. This is optional, and allows to send plugin-specific commands. It's up to the plugin writer what to support here. If the user entered e.g. a cd directory command, you can return the new path in RemoteName (max 259 characters), and give <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.Symlink"/> as return value. Return <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.OK"/> to cause a refresh (re-read) of the active panel.</description></item>
        /// </list>
        /// <para>This function is called by Total Commander and is not intended for direct use</para></remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        int FsExecuteFile(HWND MainWin,char* RemoteName,char* Verb);
    public:
        /// <summary>When overiden in derived class called to execute a file on the plugin's file system, or show its property sheet. It is also called to show a plugin configuration dialog when the user right clicks on the plugin root and chooses 'properties'. The plugin is then called with <paramref name="RemoteName"/>="\" and <paramref name="Verb"/>="properties" (requires TC>=5.51).</summary>
        /// <param name="hMainWin">Handle to parent window which can be used for showing a property sheet.</param>
        /// <param name="RemoteName">Name of the file to be executed, with full path.</param>
        /// <param name="Verb">This can be either "<c>open</c>", "<c>properties</c>", "<c>chmod</c>" or "<c>quote</c>" (case-insensitive).</param>
        /// <returns>Return <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.Yourself"/> if Total Commander should download the file and execute it locally, <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.OK"/> if the command was executed successfully in the plugin (or if the command isn't applicable and no further action is needed), <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.Error"/> if execution failed, or <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.Symlink"/> if this was a (symbolic) link or .lnk file pointing to a different directory.</returns>
        /// <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
        /// <exception cref="Security::SecurityException">Security error detected</exception>
        /// <exception cref="IO:IOException">An IO error occured</exception>
        /// <exception cref="InvalidOperationException">Excution cannot be done from other reason</exception>
        /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
        /// <remarks>Meaning of verbs:
        /// <list type="table"><listheader><term>verb</term><description>meaning</description></listheader>
        /// <item><term>open</term><description>This is called when the user presses ENTER on a file. There are three ways to handle it:
        /// <list type="bulet">
        /// <item>For internal commands like "Add new connection", execute it in the plugin and return <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.OK"/> or <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.Error"/></item>
        /// <item>Let Total Commander download the file and execute it locally: return <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.Yourself"/></item>
        /// <item>If the file is a (symbolic) link, set <paramref name="RemoteName"/> to the location to which the link points (including the full plugin path), and return <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.Symlink"/>. Total Commander will then switch to that directory. You can also switch to a directory on the local harddisk! To do this, return a path starting either with a drive letter, or an UNC location (\\server\share). The maximum allowed length of such a path is MAX_PATH-1 = 259 characters!</item>
        /// </list></description></item>
        /// <item><term>properties</term><description>Show a property sheet for the file (optional). Currently not handled by internal Totalcmd functions if <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.Yourself"/> is returned, so the plugin needs to do it internally.</description></item>
        /// <item><term>chmod xxx</term><description>The xxx stands for the new Unix mode (attributes) to be applied to the file <paramref name="RemoteName"/>. This verb is only used when returning Unix attributes through <see cref="FindFirst"/>/<see cref="FindNext"/></description></item>
        /// <item><term>quote commandline</term><description>Execute the command line entered by the user in the directory <paramref name="RemoteName"/> . This is called when the user enters a command in Totalcmd's command line, and presses ENTER. This is optional, and allows to send plugin-specific commands. It's up to the plugin writer what to support here. If the user entered e.g. a cd directory command, you can return the new path in RemoteName (max 259 characters), and give <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.Symlink"/> as return value. Return <see2 cref2="F:Tools.TotaCommanderT.ExecExitCode.OK"/> to cause a refresh (re-read) of the active panel.</description></item>
        /// </list>
        /// <para>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.</para>
        /// <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
        [MethodNotSupportedAttribute]
        virtual ExecExitCode ExecuteFile(IntPtr hMainWin, String^% RemoteName, String^ Verb);
    public:
        /// <summary>Called to transfer (copy or move) a file within the plugin's file system.</summary>
        /// <param name="OldName">Name of the remote source file, with full path. The name always starts with a backslash, then the names returned by <see cref="FsFindFirst"/>/<see cref="FsFindNext"/> separated by backslashes.</param>
        /// <param name="NewName">Name of the remote destination file, with full path. The name always starts with a backslash, then the names returned by <see cref="FsFindFirst"/>/<see cref="FsFindNext"/> separated by backslashes.</param>
        /// <param name="Move">If true, the file needs to be moved to the new location and name. Many file systems allow to rename/move a file without actually moving any of its data, only the pointer to it.</param>
        /// <param name="OverWrite">Tells the function whether it should overwrite the target file or not. See notes below on how this parameter is used.</param>
        /// <param name="ri">A structure of type <see cref="RemoteInfoStruct"/> which contains the parameters of the file being renamed/moved (not of the target file!). In TC 5.51, the fields are set as follows for directories: <see cref="RemoteInfoStruct::SizeLow"/>=0, <see cref="RemoteInfoStruct::SizeHigh"/>=0xFFFFFFFF</param>
        /// <returns>One of the <see cref="FileSystemExitCode"/> values</returns> 
        /// <remarks>Total Commander usually calls this function twice:
        /// <list tpe="bullet"><item>once with <paramref name="OverWrite"/>==false. If the remote file exists, return <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.FileExists"/>. If it doesn't exist, try to copy the file, and return an appropriate error code.</item>
        /// <item>a second time with <paramref name="OverWrite"/>==true, if the user chose to overwrite the file.</item></list>
        /// <para>While copying the file, but at least at the beginning and the end, call <see cref="ProgressProc"/> to show the copy progress and allow the user to abort the operation.</para>
        /// <para>This function is called by Total Commander and is not intended for direct use</para></remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        int FsRenMovFile(char* OldName,char* NewName,BOOL Move, BOOL OverWrite,RemoteInfoStruct* ri);
    public:
        /// <summary>When overriden in derived class called to transfer (copy or move) a file within the plugin's file system.</summary>
        /// <param name="OldName">Name of the remote source file, with full path. The name always starts with a backslash, then the names returned by <see cref="FindFirst"/>/<see cref="FindNext"/> separated by backslashes.</param>
        /// <param name="NewName">Name of the remote destination file, with full path. The name always starts with a backslash, then the names returned by <see cref="FindFirst"/>/<see cref="FindNext"/> separated by backslashes.</param>
        /// <param name="Move">If true, the file needs to be moved to the new location and name. Many file systems allow to rename/move a file without actually moving any of its data, only the pointer to it.</param>
        /// <param name="OverWrite">Tells the function whether it should overwrite the target file or not. See notes below on how this parameter is used.</param>
        /// <param name="info">A structure of type <see cref="RemoteInfo"/> which contains the parameters of the file being renamed/moved (not of the target file!). In TC 5.51, the fields are set as follows for directories: <see cref="RemoteInfo::SizeLow"/>=0, <see cref="RemoteInfo::SizeHigh"/>=0xFFFFFFFF</param>
        /// <returns>One of the <see cref="FileSystemExitCode"/> values</returns> 
        /// <remarks>Total Commander usually calls this function twice:
        /// <list tpe="bullet"><item>once with <paramref name="OverWrite"/>==false. If the remote file exists, return <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.FileExists"/>. If it doesn't exist, try to copy the file, and return an appropriate error code.</item>
        /// <item>a second time with <paramref name="OverWrite"/>==true, if the user chose to overwrite the file.</item></list>
        /// <para>While copying the file, but at least at the beginning and the end, call <see cref="ProgressProc"/> to show the copy progress and allow the user to abort the operation.</para>
        /// <para>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.</para>
        /// <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note>
        /// </remarks>
        /// <exception cref="UnauthorizedAccessException">The user does not have required access.  ame effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.ReadError"/>.</exception>
        /// <exception cref="Security::SecurityException">Security error detected. Same effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.ReadError"/>.</exception>
        /// <exception cref="IO:IOException">An IO error occured. Same effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.ReadError"/>.</exception>
        /// <exception cref="IO:FileNotFoundException">Source file was not found. Same effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.FileNotFound"/>.</exception>
        /// <exception cref="IO:DirectoryNotFoundException">Cannot locate parent directory of target file. Same effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.WriteError"/>.</exception>
        /// <exception cref="InvalidOperationException">Requested operation is not supported (e.g. resume). Same effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.NotSupported"/>.</exception>
        /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method. Do not confuse with returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.NotSupported"/> - it has completelly different effect.</exception>
        [MethodNotSupportedAttribute]
        virtual FileSystemExitCode RenMovFile(String^ OldName, String^ NewName, bool Move, bool OverWrite, RemoteInfo info);
    public:
        /// <summary>Called to transfer a file from the plugin's file system to the normal file system (drive letters or UNC).</summary>
        /// <param name="RemoteName">Name of the file to be retrieved, with full path. The name always starts with a backslash, then the names returned by <see cref="FsFindFirst"/>/<see cref="FsFindNext"/> separated by backslashes.</param>
        /// <param name="LocalName">Local file name with full path, either with a drive letter or UNC path (\\Server\Share\filename). The plugin may change the NAME/EXTENSION of the file (e.g. when file conversion is done), but not the path!</param>
        /// <param name="CopyFlags">Can be combination of the <see cref="CopyFlags"/> values</param>
        /// <param name="ri">This parameter contains information about the remote file which was previously retrieved via <see cref="FsFindFirst"/>/<see cref="FsFindNext"/>: The size, date/time, and attributes of the remote file. May be useful to copy the attributes with the file, and for displaying a progress dialog.</param>
        /// <returns>One of the <see cref="FileSystemExitCode"/> values</returns> 
        /// <remarks>Total Commander usually calls this function twice:
        /// <list type="bullet">
        /// <item>once with <paramref name="CopyFlags"/>==0 or <paramref name="CopyFlags"/>==<see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Move"/>. If the local file exists and resume is supported, return <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.ExistsResumeAllowed"/>. If resume isn't allowed, return <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.FileExists"/></item>
        /// <item>a second time with <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Resume"/> or <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Overwrite"/>, depending on the user's choice. The resume option is only offered to the user if <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.ExistsResumeAllowed"/> was returned by the first call.</item>
        /// <item><see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.SameCase"/> and <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.DifferentCase"/> are NEVER passed to this function, because the plugin can easily determine whether a local file exists or not.</item>
        /// <item><see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Move"/> is set, the plugin needs to delete the remote file after a successful download.</item>
        /// </list>
        /// <para>While copying the file, but at least at the beginning and the end, call <see cref="ProgressProc"/> to show the copy progress and allow the user to abort the operation.</para>
        /// <para>This function is called by Total Commander and is not intended for direct use</para></remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        int FsGetFile(char* RemoteName,char* LocalName,int CopyFlags, RemoteInfoStruct* ri);
    public:
        /// <summary>When overriden in derived class transfers a file from the plugin's file system to the normal file system (drive letters or UNC).</summary>
        /// <param name="RemoteName">Name of the file to be retrieved, with full path. The name always starts with a backslash, then the names returned by <see cref="FindFirst"/>/<see cref="FindNext"/> separated by backslashes.</param>
        /// <param name="LocalName">Local file name with full path, either with a drive letter or UNC path (\\Server\Share\filename). The plugin may change the NAME/EXTENSION of the file (e.g. when file conversion is done), but not the path!</param>
        /// <param name="CopyFlags">Can be combination of the <see cref="CopyFlags"/> values</param>
        /// <param name="info">This parameter contains information about the remote file which was previously retrieved via <see cref="FindFirst"/>/<see cref="FindNext"/>: The size, date/time, and attributes of the remote file. May be useful to copy the attributes with the file, and for displaying a progress dialog.</param>
        /// <returns>One of the <see cref="FileSystemExitCode"/> values</returns> 
        /// <remarks>Total Commander usually calls this function twice:
        /// <list type="bullet">
        /// <item>once with <paramref name="CopyFlags"/>==0 or <paramref name="CopyFlags"/>==<see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Move"/>. If the local file exists and resume is supported, return <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.ExistsResumeAllowed"/>. If resume isn't allowed, return <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.FileExists"/></item>
        /// <item>a second time with <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Resume"/> or <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Overwrite"/>, depending on the user's choice. The resume option is only offered to the user if <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.ExistsResumeAllowed"/> was returned by the first call.</item>
        /// <item><see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.SameCase"/> and <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.DifferentCase"/> are NEVER passed to this function, because the plugin can easily determine whether a local file exists or not.</item>
        /// <item><see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Move"/> is set, the plugin needs to delete the remote file after a successful download.</item>
        /// </list>
        /// <para>While copying the file, but at least at the beginning and the end, call <see cref="ProgressProc"/> to show the copy progress and allow the user to abort the operation.</para>
        /// <para>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.</para>
        /// <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note>
        /// </remarks>
        /// <exception cref="UnauthorizedAccessException">The user does not have required access.  ame effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.ReadError"/>.</exception>
        /// <exception cref="Security::SecurityException">Security error detected. Same effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.ReadError"/>.</exception>
        /// <exception cref="IO:IOException">An IO error occured. Same effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.ReadError"/>.</exception>
        /// <exception cref="IO:FileNotFoundException">Source file was not found. Same effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.FileNotFound"/>.</exception>
        /// <exception cref="IO:DirectoryNotFoundException">Cannot locate parent directory of target file. Same effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.WriteError"/>.</exception>
        /// <exception cref="InvalidOperationException">Requested operation is not supported (e.g. resume). Same effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.NotSupported"/>.</exception>
        /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method. Do not confuse with returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.NotSupported"/> - it has completelly different effect.</exception>
        [MethodNotSupportedAttribute]
        virtual FileSystemExitCode GetFile(String^ RemoteName, String^% LocalName, CopyFlags CopyFlags, RemoteInfo info);
    public:
        /// <summary>Called to transfer a file from the normal file system (drive letters or UNC) to the plugin's file system.</summary>
        /// <param name="LocalName">Local file name with full path, either with a drive letter or UNC path (\\Server\Share\filename). This file needs to be uploaded to the plugin's file system.</param>
        /// <param name="RemoteName">Name of the remote file, with full path. The name always starts with a backslash, then the names returned by <see cref="FsFindFirst"/>/<see cref="FsFindNext"/> separated by backslashes. The plugin may change the NAME/EXTENSION of the file (e.g. when file conversion is done), but not the path!</param>
        /// <param name="CopyFlags">Can be combination of the <see cref="CopyFlags"/> values.</param>
        /// <returns>One of the <see cref="FileSystemExitCode"/> values</returns>
        /// <remarks>Total Commander usually calls this function twice, with the following parameters in <paramref name="CopyFlags"/>:
        /// <list type="bullet">
        /// <item>once with neither <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Resume"/> nor <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Overwrite"/> set. If the remote file exists and resume is supported, return <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.ExistsResumeAllowed"/>. If resume isn't allowed, return <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.FileExists"/></item>
        /// <item>a second time with <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Resume"/> or <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Overwrite"/>, depending on the user's choice. The resume option is only offered to the user if <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.ExistsResumeAllowed"/> was returned by the first call.</item>
        /// <item>The flags <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.SameCase"/> or <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.DifferentCase"/> are added to CopyFlags when the remote file exists and needs to be overwritten. This is a hint to the plugin to allow optimizations: Depending on the plugin type, it may be very slow to check the server for every single file when uploading.</item>
        /// <item>If the flag <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Move"/> is set, the plugin needs to delete the local file after a successful upload.</item>
        /// </list>
        /// <para>While copying the file, but at least at the beginning and the end, call ProgressProc to show the copy progress and allow the user to abort the operation.</para>
        /// <para>This function is called by Total Commander and is not intended for direct use</para></remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        int FsPutFile(char* LocalName,char* RemoteName,int CopyFlags);
    public:
        /// <summary>When overriden in derived class transfers a file from the normal file system (drive letters or UNC) to the plugin's file system.</summary>
        /// <param name="LocalName">Local file name with full path, either with a drive letter or UNC path (\\Server\Share\filename). This file needs to be uploaded to the plugin's file system.</param>
        /// <param name="RemoteName">Name of the remote file, with full path. The name always starts with a backslash, then the names returned by <see cref="FindFirst"/>/<see cref="FindNext"/> separated by backslashes. The plugin may change the NAME/EXTENSION of the file (e.g. when file conversion is done), but not the path!</param>
        /// <param name="CopyFlags">Can be combination of the <see cref="CopyFlags"/> values.</param>
        /// <returns>One of the <see cref="FileSystemExitCode"/> values</returns>
        /// <remarks>Total Commander usually calls this function twice, with the following parameters in <paramref name="CopyFlags"/>:
        /// <list type="bullet">
        /// <item>once with neither <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Resume"/> nor <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Overwrite"/> set. If the remote file exists and resume is supported, return <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.ExistsResumeAllowed"/>. If resume isn't allowed, return <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.FileExists"/></item>
        /// <item>a second time with <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Resume"/> or <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Overwrite"/>, depending on the user's choice. The resume option is only offered to the user if <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.ExistsResumeAllowed"/> was returned by the first call.</item>
        /// <item>The flags <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.SameCase"/> or <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.DifferentCase"/> are added to CopyFlags when the remote file exists and needs to be overwritten. This is a hint to the plugin to allow optimizations: Depending on the plugin type, it may be very slow to check the server for every single file when uploading.</item>
        /// <item>If the flag <see2 cref2="F:Tools.TotaCommanderT.CopyFlags.ExitCode.Move"/> is set, the plugin needs to delete the local file after a successful upload.</item>
        /// </list>
        /// <para>While copying the file, but at least at the beginning and the end, call ProgressProc to show the copy progress and allow the user to abort the operation.</para>
        /// <para>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.</para>
        /// <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note>
        /// </remarks>
        /// <exception cref="UnauthorizedAccessException">The user does not have required access.  ame effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.ReadError"/>.</exception>
        /// <exception cref="Security::SecurityException">Security error detected. Same effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.ReadError"/>.</exception>
        /// <exception cref="IO:IOException">An IO error occured. Same effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.ReadError"/>.</exception>
        /// <exception cref="IO:FileNotFoundException">Source file was not found. Same effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.FileNotFound"/>.</exception>
        /// <exception cref="IO:DirectoryNotFoundException">Cannot locate parent directory of target file. Same effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.WriteError"/>.</exception>
        /// <exception cref="InvalidOperationException">Requested operation is not supported (e.g. resume). Same effect as returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.NotSupported"/>.</exception>
        /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method. Do not confuse with returning <see2 cref2="F:Tools.TotaCommanderT.FileSystem.ExitCode.NotSupported"/> - it has completelly different effect.</exception>
        [MethodNotSupportedAttribute]
        virtual FileSystemExitCode PutFile(String^ LocalName, String^% RemoteName, CopyFlags CopyFlags);
    public:
        /// <summary>Called to delete a file from the plugin's file system</summary>
        /// <param name="RemoteName">Name of the file to be deleted, with full path. The name always starts with a backslash, then the names returned by <see cref="FsFindFirst"/>/<see cref="FsFindNext"/> separated by backslashes</param>
        /// <returns>Return TRUE if the file could be deleted, FALSE if not.</returns>
        /// <remarks><para>This function is called by Total Commander and is not intended for direct use</para></remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        BOOL FsDeleteFile(char* RemoteName);
    public:
        /// <summary>When overriden in derived class deletes a file from the plugin's file system</summary>
        /// <param name="RemoteName">Name of the file to be deleted, with full path. The name always starts with a backslash, then the names returned by <see cref="FindFirst"/>/<see cref="FindNext"/> separated by backslashes</param>
        /// <returns>Return true if the file could be deleted, false if not.</returns>
        /// <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
        /// <exception cref="Security::SecurityException">Security error detected</exception>
        /// <exception cref="IO:IOException">An IO error occured</exception>
        /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
        /// <remarks>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.
        /// <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
        [MethodNotSupportedAttribute]
        virtual bool DeleteFile(String^ RemoteName);
    public:
        /// <summary>Called to remove a directory from the plugin's file system.</summary>
        /// <param name="RemoteName">Name of the directory to be removed, with full path. The name always starts with a backslash, then the names returned by <see cref="FsFindFirst"/>/<see cref="FsFindNext"/> separated by backslashes.</param>
        /// <returns>Return TRUE if the directory could be removed, FALSE if not.</returns>
        /// <remarks><para>This function is called by Total Commander and is not intended for direct use</para></remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        BOOL FsRemoveDir(char* RemoteName);
    public:
        /// <summary>When overriden in derived class removes a directory from the plugin's file system.</summary>
        /// <param name="RemoteName">Name of the directory to be removed, with full path. The name always starts with a backslash, then the names returned by <see cref="FsFindFirst"/>/<see cref="FsFindNext"/> separated by backslashes.</param>
        /// <returns>Return true if the directory could be removed, false if not.</returns>
        /// <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
        /// <exception cref="Security::SecurityException">Security error detected</exception>
        /// <exception cref="IO:IOException">An IO error occured</exception>
        /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
        /// <remarks>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.
        /// <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
        [MethodNotSupportedAttribute]
        virtual bool RemoveDir(String^ RemoteName);
    public:
        /// <summary>Called when the user presses the Disconnect button in the FTP connections toolbar. This toolbar is only shown if <see2 cref2="F:Tools.TotalCommanderT.LogKind.Connect"/> is passed to <see cref="LogProc"/>.</summary>
        /// <param name="DisconnectRoot">This is the root dir which was passed to <see cref="LogProc"/> when connecting. It allows the plugin to have serveral open connections to different file systems (e.g. ftp servers). Should be either \ (for a single possible connection) or \Servername (e.g. when having multiple open connections).</param>
        /// <returns>Return TRUE if the connection was closed (or never open), FALSE if it couldn't be closed.</returns>
        /// <remarks>To get calls to this function, the plugin MUST call <see cref="LogProc"/> with the parameter <see2 cref2="F:Tools.TotalCommanderT.LogKind.Connect"/>. The parameter LogString MUST start with "<c>CONNECT</c>", followed by one whitespace and the root of the file system which has been connected. This file system root will be passed to <see cref="FsDisconnect"/> when the user presses the Disconnect button, so the plugin knows which connection to close.
        /// Do NOT call <see cref="LogProc"/> with <see2 cref2="F:Tools.TotalCommanderT.LogKind.Connect"/> if your plugin does not require connect/disconnect!
        /// <list><listheader>Examples</listheader>
        /// <item>FTP requires connect/disconnect. Connect can be done automatically when the user enters a subdir, disconnect when the user clicks the Disconnect button.</item>
        /// <item>Access to local file systems (e.g. Linux EXT2) does not require connect/disconnect, so don't call <see cref="LogProc"/> with the parameter <see2 cref2="F:Tools.TotalCommanderT.LogKind.Connect"/>.</item></list>
        /// <para>This function is called by Total Commander and is not intended for direct use</para></remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        BOOL FsDisconnect(char* DisconnectRoot);
    public:
        /// <summary>When overridden in derived class, called when the user presses the Disconnect button in the FTP connections toolbar. This toolbar is only shown if <see2 cref2="F:Tools.TotalCommanderT.LogKind.Connect"/> is passed to <see cref="LogProc"/>.</summary>
        /// <param name="DisconnectRoot">This is the root dir which was passed to <see cref="LogProc"/> when connecting. It allows the plugin to have serveral open connections to different file systems (e.g. ftp servers). Should be either \ (for a single possible connection) or \Servername (e.g. when having multiple open connections).</param>
        /// <returns>Return true if the connection was closed (or never open), false if it couldn't be closed.</returns>
        /// <remarks>To get calls to this function, the plugin MUST call <see cref="LogProc"/> with the parameter <see2 cref2="F:Tools.TotalCommanderT.LogKind.Connect"/>. The parameter LogString MUST start with "<c>CONNECT</c>", followed by one whitespace and the root of the file system which has been connected. This file system root will be passed to <see cref="Disconnect"/> when the user presses the Disconnect button, so the plugin knows which connection to close.
        /// Do NOT call <see cref="LogProc"/> with <see2 cref2="F:Tools.TotalCommanderT.LogKind.Connect"/> if your plugin does not require connect/disconnect!
        /// <list><listheader>Examples</listheader>
        /// <item>FTP requires connect/disconnect. Connect can be done automatically when the user enters a subdir, disconnect when the user clicks the Disconnect button.</item>
        /// <item>Access to local file systems (e.g. Linux EXT2) does not require connect/disconnect, so don't call <see cref="LogProc"/> with the parameter <see2 cref2="F:Tools.TotalCommanderT.LogKind.Connect"/>.</item></list>
        /// </remarks>
        /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
        /// <remarks>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.
        /// <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
        [MethodNotSupportedAttribute]
        virtual bool Disconnect(String^ DisconnectRoot);
    public:
        /// <summary>Called to set the (Windows-Style) file attributes of a file/dir. <see cref="FsExecuteFile"/> is called for Unix-style attributes.</summary>
        /// <param name="RemoteName">Name of the file/directory whose attributes have to be set</param>
        /// <param name="NewAttr">New file attributes</param>
        /// <returns>Return TRUE if successful, FALSE if the function failed.</returns>
        /// <remarks><para>This function is called by Total Commander and is not intended for direct use</para></remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        BOOL FsSetAttr(char* RemoteName,int NewAttr);
    public:
        /// <summary>When overriden in derived class sets the (Windows-Style) file attributes of a file/dir. <see cref="ExecuteFile"/> is called for Unix-style attributes.</summary>
        /// <param name="RemoteName">Name of the file/directory whose attributes have to be set</param>
        /// <param name="NewAttr">New file attributes</param>
        /// <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
        /// <exception cref="Security::SecurityException">Security error detected</exception>
        /// <exception cref="IO:IOException">An IO error occured</exception>
        /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
        /// <remarks>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.
        /// <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
        [MethodNotSupportedAttribute]
        virtual void SetAttr(String^ RemoteName, StandardFileAttributes NewAttr);
    public:
        /// <summary>Called to set the (Windows-Style) file times of a file/dir.</summary>
        /// <param name="RemoteName">Name of the file/directory whose attributes have to be set</param>
        /// <param name="CreationTime">Creation time of the file. May be NULL to leave it unchanged.</param>
        /// <param name="LastAccessTime">Last access time of the file. May be NULL to leave it unchanged.</param>
        /// <param name="LastWriteTime">Last write time of the file. May be NULL to leave it unchanged. If your file system only supports one time, use this parameter!</param>
        /// <returns>Return TRUE if successful, FALSE if the function failed.</returns>
        /// <remarks><para>This function is called by Total Commander and is not intended for direct use</para></remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        BOOL FsSetTime(char* RemoteName,FILETIME *CreationTime, FILETIME *LastAccessTime,FILETIME *LastWriteTime);
    public:
        /// <summary>When overriden in derived class sets the (Windows-Style) file times of a file/dir.</summary>
        /// <param name="RemoteName">Name of the file/directory whose attributes have to be set</param>
        /// <param name="CreationTime">Creation time of the file. May be NULL to leave it unchanged.</param>
        /// <param name="LastAccessTime">Last access time of the file. May be NULL to leave it unchanged.</param>
        /// <param name="LastWriteTime">Last write time of the file. May be NULL to leave it unchanged. If your file system only supports one time, use this parameter!</param>
        /// <returns>Return TRUE if successful, FALSE if the function failed.</returns>
        /// <exception cref="UnauthorizedAccessException">The user does not have required access</exception>
        /// <exception cref="Security::SecurityException">Security error detected</exception>
        /// <exception cref="IO:IOException">An IO error occured</exception>
        /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
        /// <remarks>When most-derived method implementation is marked with <see cref="MethodNotSupportedAttribute"/>, it means that the most derived plugin implementation does not support operation provided by the method.
        /// <note type="inheritinfo">Do not thow any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note></remarks>
        [MethodNotSupportedAttribute]
        virtual void SetTime(String^ RemoteName, Nullable<DateTime> CreationTime, Nullable<DateTime> LastAccessTime, Nullable<DateTime> LastWriteTime);
    public:
        /// <summary>called just as an information to the plugin that a certain operation starts or ends. It can be used to allocate/free buffers, and/or to flush data from a cache. There is no need to implement this function if the plugin doesn't require it.</summary>
        /// <param name="RemoteDir">This is the current source directory when the operation starts. May be used to find out which part of the file system is affected.</param>
        /// <param name="InfoStartEnd">Information whether the operation starts or ends</param>
        /// <param name="InfoOperation">Information of which operaration starts/ends</param>
        /// <remarks>Please note that future versions of the framework may send additional values!
        /// <para>This function has been added for the convenience of plugin writers. All calls to plugin functions will be enclosed in a pair of <see cref="FsStatusInfo"/> calls: At the start, <see cref="FsStatusInfo"/>(...,FS_STATUS_START,...) and when the operation is done FsStatusInfo(...,FS_STATUS_END,...). Multiple plugin calls can be between these two calls. For example, a download may contain multiple calls to <see cref="FsGetFile"/>, and <see cref="FsFindFirst"/>, <see cref="FsFindNext"/>, <see cref="FsFindClose"/> (for copying subdirs).</para>
        /// <para>This function is called by Total Commander and is not intended for direct use.</para></remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        void FsStatusInfo(char* RemoteDir,int InfoStartEnd,int InfoOperation);
    public:
        /// <summary>Called instead of <see cref="FsStatusInfo"/> when plugin is used outside of Total Commander.</summary>
        /// <param name="RemoteDir">This is the current source directory when the operation starts. May be used to find out which part of the file system is affected.</param>
        /// <param name="InfoStartEnd">Information whether the operation starts or ends</param>
        /// <param name="InfoOperation">Information of which operaration starts/ends</param>
        /// <remarks>Please note that future versions of the framework may send additional values!</remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void StatusInfo(String^ RemoteDir,OperationStatus InfoStartEnd,OperationKind InfoOperation);
        /// <summary>When overiden in derived class handles operation status change reported by Total Commaner</summary>
        /// <param name="e">Event arguments</param>
        /// <remarks>Do not call this method from your code. It is called by Total Commander. In case you use plugin outside of Total Commander call <see cref="StatusInfo"/>.</remarks>
        virtual void OnOperationStatusChanged(OperationEventArgs^ e);
        /// <summary>When overriden in derived class handles start of operation reported by Total Commander</summary>
        /// <remarks>This method is called before operation sarts. Call of this method is always fololowed by call of <see cref="OnOperationStatusChanged"/>.
        /// <para>Do not call this method from your code. It is called by Total Commander. In case you use plugin outside of Total Commander call <see cref="StatusInfo"/>.</para></remarks>
        /// <param name="e">Event arguments</param>
        virtual void OnOperationStarting(OperationEventArgs^ e);
        /// <summary>When overriden in derived class handles end of operation reported by Total Commander</summary>
        /// <remarks>This method is called after operation finishes.  Call of this method is always fololowed by call of <see cref="OnOperationStatusChanged"/>.
        /// <para>Do not call this method from your code. It is called by Total Commander. In case you use plugin outside of Total Commander call <see cref="StatusInfo"/>.</para></remarks>
        /// <param name="e">Event arguments</param>
        virtual void OnOperationFinished(OperationEventArgs^ e);
    public:
        /// <summary>Called only when the plugin is installed. It asks the plugin for the default root name which should appear in the Network Neighborhood. This root name is NOT part of the path passed to the plugin when Totalcmd accesses the plugin file system! The root will always be "\", and all subpaths will be built from the directory names returned by the plugin.</summary>
        /// <param name="DefRootName">Pointer to a buffer (allocated by the calling program) which can receive the root name.</param>
        /// <param name="maxlen">Maximum number of characters (including the final 0) which fit in the buffer."</param>
        /// <remarks>Example: The root name may be "Linux file system" for a plugin which accesses Linux drives. If this function isn't implemented, Totalcmd will suggest the name of the DLL (without extension .DLL) as the plugin root. This function is called directly after loading the plugin (when the user installs it), <see cref="FsInit"/> is NOT called when installing the plugin.
        /// <para>This function is called by Total Commander and is not intended for direct use</para></remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        void FsGetDefRootName(char* DefRootName,int maxlen);
    public:
        //TODO:
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        int FsExtractCustomIcon(char* RemoteName,int ExtractFlags,HICON* TheIcon);
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        void FsSetDefaultParams(FsDefaultParamStruct* dps);
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        int FsGetPreviewBitmap(char* RemoteName,int width,int height, HBITMAP* ReturnedBitmap);
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        BOOL FsLinksToLocalFiles(void);
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        BOOL FsGetLocalName(char* RemoteName,int maxlen);
        //TODO: custom columns
#pragma endregion
    };

    
   
}}