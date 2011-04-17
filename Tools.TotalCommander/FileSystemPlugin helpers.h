#pragma once

#include "Plugin\fsplugin.h"
#include "Common.h"
#include "ContentPluginBase.h"

namespace Tools{namespace TotalCommanderT{
    using namespace System;
    using namespace System::ComponentModel;
   
#pragma region "Enums"
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
        /// <summary>Copying was aborted by the user (through <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.ProgressProc(System.String,System.String,System.Int32)"/>)</summary>
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

    /// <summary>Defines a result of cryptography operation</summary>
    /// <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    public enum class CryptResult{
        /// <summary>The operation succeeded</summary>
        OK = FS_FILE_OK,
        /// <summary>Encrypt/Decrypt failed</summary>
        Fail = FS_FILE_NOTSUPPORTED,
        /// <summary>Could not write password to password store</summary>
        WriteError = FS_FILE_WRITEERROR,
        /// <summary>Password not found in password store</summary>
        ReadError = FS_FILE_READERROR,
        /// <summary>No master password entered yet</summary>
        NoMasterPassword = FS_FILE_NOTFOUND
    };

    /// <summary>Indicates currrent status of Total Commander secure password store</summary>
    /// <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    [Flags]
    public enum class CryptFlags{
        /// <summary>The user already has a master password defined</summary>
        MasterPasswordSet = FS_CRYPTOPT_MASTERPASS_SET
    };

    /// <summary>Modes of crypto operations</summary>
    /// <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    public enum class CryptMode{
        /// <summary>Save password to password store</summary>
        SavePassword = FS_CRYPT_SAVE_PASSWORD,
        /// <summary>Load password from password store</summary>
        LoadPassword = FS_CRYPT_LOAD_PASSWORD,
        /// <summary>Load password only if master password has already been entered</summary>
	    LoadPasswordNoUI = FS_CRYPT_LOAD_PASSWORD_NO_UI,
        /// <summary>Copy password to new connection. Here the second string parameter <c>Password</c> is not a password, but the name of the target connection</summary>
        CopyPassword = FS_CRYPT_COPY_PASSWORD,
        /// <summary>Copy password to new connection and delete source password. Here the second string parameter <c>Password</c> is not a password, but the name of the target connection</summary>
	    MovePassword = FS_CRYPT_MOVE_PASSWORD,
        /// <summary>Delete the password of the given connection</summary>
        DeletePassword = FS_CRYPT_DELETE_PASSWORD
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
        DifferentCase = FS_COPYFLAGS_EXISTS_DIFFERENTCASE
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
#pragma endregion

    /// <summary>Contains information about the file that is found by the <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.FindFirst(System.String,Tools.TotalCommanderT.FindData@)"/>, or <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.FindNext(System.Object,Tools.TotalCommanderT.FindData@)"/> function.</summary>
    public value class FindData {
    private:
        /// <summary>Contains value of the <see cref="FileAttributes"/> property</summary>
        FileAttributes dwFileAttributes;
        /// <summary>Contains value of the <see cref="CreationTime"/> property</summary>
        Nullable<DateTime> ftCreationTime;
        /// <summary>Contains value of the <see cref="AccessTime"/> property</summary>
        Nullable<DateTime> ftLastAccessTime;
        /// <summary>Contains value of the <see cref="WriteTime"/> property</summary>
        Nullable<DateTime> ftLastWriteTime;
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
        property Nullable<DateTime> CreationTime{Nullable<DateTime> get(); void set(Nullable<DateTime>);}
        /// <summary>For a file, the structure specifies when the file was last read from, written to, or for executable files, run.</summary>
        /// <remarks>For a directory, the structure specifies when the directory is created. If the underlying file system does not support last access time, this member is zero.
        /// <para>On the FAT file system, the specified date for both files and directories is correct, but the time of day is always set to midnight.</para></remarks>
        property Nullable<DateTime> AccessTime{Nullable<DateTime> get(); void set(Nullable<DateTime>);}
        /// <summary>For a file, the structure specifies when the file was last written to, truncated, or overwritten. The date and time are not updated when file attributes or security descriptors are changed.</summary>
        /// <remarks>For a directory, the structure specifies when the directory is created. If the underlying file system does not support last write time, this member is zero.</remarks>
        property Nullable<DateTime> WriteTime{Nullable<DateTime> get(); void set(Nullable<DateTime>);}
        /// <summary>Size of file in bytes</summary>
        /// <remarks>This property is not CLS-compliant. CLS-cmplant alternative is to use some of following functions: <see cref="SetFileSize"/>, <see cref="GetFileSize"/>, <see cref="SetFileSizeLow"/>, <see cref="SetFileSizeHigh"/>, <see cref="GetFileSizeLow"/>, <see cref="GetFileSizeHigh"/>.</remarks>
        [CLSCompliantAttribute(false)]
        property QWORD FileSize{QWORD get(); void set(QWORD);}
        /// <summary>If the <see cref="Attributes"/> member includes the <see2 cref2="F:Tools.TotalCommanderT.FileAttributes.ReparsePoint"/> attribute, this member specifies the reparse point tag. Otherwise, this value is undefined and should not be used.</summary>
        /// <remarks>This property is not CLS-copliant. CLS-copliant alternatives are <see cref="GetReparsePointTag"/> and <see cref="SetReparsePointTag"/>.</remarks>
        [CLSCompliantAttribute(false)]
        property ReparsePointTags ReparsePointTag{ReparsePointTags get(); void set(ReparsePointTags);}
        /// <summary>Reserved for future use.</summary>
        /// <remarks>This property is not CLS-copliant. CLS-copliant alternatives are <see cref="GetReserved1"/> and <see cref="SetReserved1"/>.</remarks>
        [EditorBrowsableAttribute(EditorBrowsableState::Never)]
        [CLSCompliantAttribute(false)]
        property DWORD Reserved1{
            [EditorBrowsableAttribute(EditorBrowsableState::Never)] DWORD get();
            void set(DWORD);
        }
        /// <summary>The name of the file.</summary>
        /// <remarks>Currently all string are marshalled between plugin and Total Commander using <see cref="System::Text::Encoding::Default"/>, so not all the Unicode characters are supported.</remarks>
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
    
        /// <summary>Sets <see cref="FileSize"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <param name="value">New value of the <see cref="FileSize"/> property</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative</exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetFileSize(__int64 value);
        /// <summary>Gets <see cref="FileSize"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <returns><see cref="FileSize"/></returns>
        /// <exception cref="InvalidOperationException"><see cref="FileSize"/> is greater than <see cref="Int64::MaxValue"/></exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        __int64 GetFileSize();
        /// <summary>Sets low word <see cref="FileSize"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <param name="value">New value of the <see cref="FileSize"/> property's low word</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative or greater than <see cref="Int32::MaxValue"/></exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetFileSizeLow(__int64 value);
        /// <summary>Gets low word <see cref="FileSize"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <returns>Low word of <see cref="FileSize"/></returns>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        __int64 GetFileSizeLow();
        /// <summary>Sets high word <see cref="FileSize"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <param name="value">New value of the <see cref="FileSize"/> property's high word</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative or greater than <see cref="Int32::MaxValue"/></exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetFileSizeHigh(__int64 value);
        /// <summary>Gets high word <see cref="FileSize"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <returns>High word of <see cref="FileSize"/></returns>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        __int64 GetFileSizeHigh();
#pragma endregion
    };

#pragma region "More enums"   
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
#pragma end region

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
        property Nullable<DateTime> LastWriteTime;
        /// <summary>Attributes of the remote file - should be copied with the file.</summary>
        /// <remarks>This property is not CLS-comliant. CLS-compliant alternative is <see cref="Attributes"/>.</remarks>
        [CLSCompliant(false)]
        property FileAttributes Attr;
        /// <summary>Remote file size. Useful for a progress indicator.</summary>
        /// <remarks>This property is not CLS-compliant. CLS-compliant alternative is to use <see cref="SetSize"/> and <see cref="GetSize"/>.</remarks>
        [CLSCompliantAttribute(false)]
        property QWORD Size{QWORD get();void set(QWORD);}
#pragma region "CLS-compliance"
        /// <summary>CLS-comliant alternative to the <see cref="Attr"/> property - gets or sets file attributes.</summary>
        /// <returns>Bitwise-same value as <see cref="Attr"/> representing file attrbutes</returns>
        /// <value>Value of the <see cref="Attr"/> property (bitwise same)</value>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        property Int32 Attributes{Int32 get(); void set(Int32);}
        /// <summary>Sets <see cref="Size"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <param name="value">New value of the <see cref="Size"/> property</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative</exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetSize(__int64 value);
        /// <summary>Gets <see cref="Size"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <returns><see cref="Size"/></returns>
        /// <exception cref="InvalidOperationException"><see cref="Size"/> is greater than <see cref="Int64::MaxValue"/></exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        __int64 GetSize();
        /// <summary>Sets <see cref="SizeLow"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <param name="value">New value of the <see cref="SizeLow"/> property</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative or greater than <see cref="Int32::MaxValue"/></exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetSizeLow(__int64 value);
        /// <summary>Gets <see cref="SizeLow"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <returns><see cref="SizeLow"/></returns>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        __int64 GetSizeLow();
        /// <summary>Sets <see cref="SizeHigh"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <param name="value">New value of the <see cref="SizeHigh"/> property</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is negative or greater than <see cref="Int32::MaxValue"/></exception>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        void SetSizeHigh(__int64 value);
        /// <summary>Gets <see cref="SizeHigh"/> as CLS-compliant <see cref="Int64"/></summary>
        /// <returns><see cref="SizeHigh"/></returns>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        __int64 GetSizeHigh();
#pragma endregion
    internal:
        /// <summary>CTor from <see cref="RemoteInfoStruct"/></summary>
        /// <param name="ri"><see cref="RemoteInfoStruct"/> to initialize new instance with</param>
        RemoteInfo(const RemoteInfoStruct& ri);
    };

    /// <summary>File system icon extraction flags</summary>
    [FlagsAttribute]
    public enum class IconExtractFlags : int{
        /// <summary>Requests the small 16x16 icon</summary>
        SmallIcon = FS_ICONFLAG_SMALL,
        /// <summary>The function is called from the background thread</summary>
        BackgroundThread = FS_ICONFLAG_BACKGROUND
    };

    /// <summary>Identifies result of icon-obtaining</summary>
    public enum class IconExtractResult : int{
        /// <summary>No icon is returned. The calling app should show the default icon for this file type.</summary>
        UseDefault = FS_ICON_USEDEFAULT,
        /// <summary>An icon was returned in TheIcon. The icon must NOT be freed by the calling app, e.g. because it was loaded with LoadIcon Win32 API, or the DLL handles destruction of the icon.</summary>
        Extracted = FS_ICON_EXTRACTED,
        /// <summary>An icon was returned in TheIcon. The icon MUST be destroyed by the calling app, e.g. because it was created with CreateIcon Win32 API, or extracted with ExtractIconEx Win32 API.</summary>
        ExtractedDestroy = FS_ICON_EXTRACTED_DESTROY,
        /// <summary>This return value is only valid when icon was NOT obtained at background. It tells the calling app to show a default icon, and request the true icon in a background thread.</summary>
        Delayed = FS_ICON_DELAYED
    };

    /// <summary>Contains information about current plugin interface version and ini location</summary>
    public value class DefaultParams{
    private:
        /// <summary>Contains value of the <see cref="Version"/> property</summary>
        System::Version^ version;
        /// <summary>Contains value of the <see cref="DefaultIniName"/> property</summary>
        String^ defaultIniName;
    internal:
        /// <summary>CTor - populates new instance with data from <see cref="FsDefaultParamStruct"/></summary>
        /// <param name="from">The <see cref="FsDefaultParamStruct"/></param>
        DefaultParams(FsDefaultParamStruct& from);
    public:
        /// <summary>Gets the plugin interface version</summary>
        /// <returns>Version of plugin intercase consisifting of Major.Minor.0.0</returns>
        property System::Version^ Version{System::Version^ get();}
        /// <summary>Suggested location+name of the ini file where the plugin could store its data.</summary>
        /// <returns>A fully qualified path+file name, in the same directory as the wincmd.ini. It's recommended to store the plugin data in this file or at least in this directory, because the plugin directory or the Windows directory may not be writable!</returns>
        property String^ DefaultIniName{String^ get();}
    };

    /// <summary>Contains instructions for Total Commander how to handle extracted bitmap</summary>
    public enum class BitmapHandling{
        /// <summary>There is no preview bitmap.</summary>
        None = FS_BITMAP_NONE,
        /// <summary>The image was extracted and is returned in ReturnedBitmap parameter</summary>
        Extracted = FS_BITMAP_EXTRACTED,	
        /// <summary>Tells the caller to extract the image by itself. The full local path to the file needs to be returned in RemoteName parameter. The returned bitmap name must not be longer than <see cref="FindData::MaxPath"/>.</summary>
        ExtractYourself = FS_BITMAP_EXTRACT_YOURSELF,	
        /// <summary>Tells the caller to extract the image by itself, and then delete the temporary image file. The full local path to the temporary image file needs to be returned in RemoteName parameter. The returned bitmap name must not be longer than <see cref="FindData::MaxPath"/>. In this case, the plugin downloads the file to TEMP and then asks TC to extract the image.</summary>
        ExtractAndDelete = FS_BITMAP_EXTRACT_YOURSELF_ANDDELETE,	
        /// <summary>This value must be ADDED to one of the above values if the caller should cache the image. Do NOT add this image if you will cache the image yourself!</summary>
        Cache = FS_BITMAP_CACHE
    };

    /// <summary>Implements the += operator for <see cref="BitmapHandling"/> enumeration</summary>
    /// <param name="a">Reference to <see cref="BitmapHandling"/> to be updated</param>
    /// <param name="b">Value to be added to <paramref name="b"/></param>
    /// <returns>a + b</returns>
    BitmapHandling& operator += (BitmapHandling& a, const BitmapHandling& b);

    /// <summary>Holds reference to file miniature bitmap</summary>
    /// <remarks>If you do not provide bitmap for certain file/directory return null instead of instance of this class</remarks>
    public ref class BitmapResult{
    private:
        /// <summary>Contains value of the <see cref="ImageKey"/> property</summary>
        String^ imagekey;
    public:
        /// <summary>CTor - creates new instance of the <see cref="BitmapResult"/> class from image path</summary>
        /// <param name="ImagePath">Path to image preview bitmap is stored in.</param>
        /// <para name="Temporary">True if <paramref name="ImagePath"/> points to temporary file Total Commander shall delete whan it is no longer needed (after bitmap is read)</para>
        /// <exception cref="IO::PathTooLongException">The length of <paramref name="ImagePath"/> is greater than <see cref="FindData::MaxPath"/> - 1</exception>
        /// <exception cref="ArgumentNullException"><paramref name="ImagePath"/> is null</exception>
        BitmapResult(String^ ImagePath, bool Temporary);
        /// <summary>CTor - creates new instance of the <see cref="BitmapResult"/> class from miniature image</summary>
        /// <param name="Bitmap">Miniature image</param>
        /// <exception cref="ArgumentNullException"><paramref name="Bitmap"/> is null</exception>
        BitmapResult(Drawing::Bitmap^ Bitmap);
        /// <summary>CTor - creates new instance of the <see cref="BitmapResult"/> class from miniature image and caching key</summary>
        /// <param name="Bitmap">Miniature image</param>
        /// <param name="ImageKey">Key that uniquelly identifies the image. Total Commander will internally cache the image using this key. Null to prevent chaching (i.e. the plugin caches the image)</param>
        /// <exception cref="ArgumentNullException"><paramref name="Bitmap"/> is null</exception>
        BitmapResult(Drawing::Bitmap^ Bitmap, String^ ImageKey);
        /// <summary>Gets or sets the miniature image</summary>
        /// <returns>The miniature image</returns>
        /// <value>When value of this property is non-null the image will be passed to Total Commander.
        /// <para>When value of this property is null, Total Commander will be instructed to extract miniature image from file at path <see cref="ImageKey"/>.</para></value>
        /// <remarks>Make sure you scale your image correctly to the desired maximum width+height! Do not fill the rest of the bitmap - instead, create a bitmap which is SMALLER than requested! This way, Total Commander can center your image and fill the rest with the default background color.</remarks>
        property Drawing::Bitmap^ Image;
        /// <summary>Gets or sets image key - it can be either path to image file Total Commander should extract miniature from or unique key of image or null</summary>
        /// <returns>Image key</returns>
        /// <value>When <see cref="Image"/> is null this property represents path to image file containing miniature image.
        /// <para>When <see cref="Image"/> is not null this property represents unique image key. Total Commander then caches the image under that key.</para></value>
        /// <exception cref="IO::PathTooLongException">Value longer than <see ctef="FindData::MaxPath"/> - 1 is set</exception>
        /// <remarks>When both - <see cref="Image"/> and <see cref="ImageKey"/> are null, Total Commander will be left with original image path as path to load miniature from. But the path targets to plugin file system space - so it is invalid from Total Commmander perspective. So, do not set both <see cref="Image"/> and <see cref="ImageKey"/> to null.</remarks>
        property String^ ImageKey{String^ get(); void set(String^);}
        /// <summary>Indicates if image should be cahced by Total Commander</summary>
        /// <value>Set this property to true to make Total Commander cahce the image under <see cref="ImageKey"/>. Do not set this property to true when you cahce the image in plugin.</value>
        /// <remarks>If value of this property is true Total Commander will chache the image under key <see cref="ImageKey"/>. Ignored when <see cref="Image"/> is not null.
        /// <para>When <see cref="Cache"/> is true and <see cref="ImageKey"/> is null, image is cached under key of its original path in plugin file system space by Total Commander.</para></remarks>
        property bool Cache;
        /// <summary>Indicates that image file pointer by path stored in <see cref="ImageKey"/> is temporary and should be deleted by Total Commander when it is no longer necessary.</summary>
        /// <returns>True when image will be deleted by Total Commander; false if not</returns>
        /// <value>True to make Total Commander delete the image when no longer necessary; false to make sure that image file will not be deleted by Total Commander.</value>
        /// <remarks>Ignored when <see cref="Image"/> is not null</remarks>
        property bool Temporary;
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        /// <summary>Gets <see cref="BitmapHandling"/> represented by this instance</summary>
        /// <returns>A <see cref="BitmapHandling"/> value to pass to Total Commander</returns>
        BitmapHandling GetFlag();
    };

    /// <summary>Exception thrown when there is problem in crypt functions (Total Commander secure passsword store)</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    public ref class CryptException sealed: System::Security::Cryptography::CryptographicException{
    public:
        /// <summary>CTor - creates a new instance of the <see cref="CryptException"/> class from error message and reason code</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="reason">One of <seee cref="CryptResult"/> values indicating kind of failure</param>
        /// <exception cref="ArgumentException"><paramref name="reason"/> is <see cref="CryptResult::OK"/></exception>
        /// <exception cref="InvalidEnumArgumentException"><paramref name="reason"/> is not one of <see cref="CryptResult"/> values</exception>
        CryptException(String^ message, CryptResult reason);
        /// <summary>CTor - creates a new instance of the <see cref="CryptException"/> class from error reason</summary>
        /// <param name="reason">One of <seee cref="CryptResult"/> values indicating kind of failure</param>
        /// <exception cref="ArgumentException"><paramref name="reason"/> is <see cref="CryptResult::OK"/></exception>
        /// <exception cref="InvalidEnumArgumentException"><paramref name="reason"/> is not one of <see cref="CryptResult"/> values</exception>
        CryptException(CryptResult reason);
        /// <summary>Gets value indicating why the operation failed</summary>
        property CryptResult Reason{ CryptResult get(); }
    private:
        /// <summary>Gets default string error message from <see cref="CryptResult"/></summary>
        /// <param name="reason">Indicates why the crypto operation failed</param>
        /// <exception cref="ArgumentException"><paramref name="reason"/> is <see cref="CryptResult::OK"/></exception>
        /// <exception cref="InvalidEnumArgumentException"><paramref name="reason"/> is not one of <see cref="CryptResult"/> values</exception>
        static String^ GetMessage(CryptResult reason);
    };

#pragma region "Delegates"
    ref class FileSystemPlugin;//Forward declaration
    /// <summary>Callback function, which the plugin can call to show copy progress.</summary>
    /// <param name="sender">The plugin requesting the operation</param>
    /// <param name="SourceName">Name of the source file being copied. Depending on the direction of the operation (Get, Put), this may be a local file name of a name in the plugin file system.</param>
    /// <param name="TargetName">Name to which the file is copied.</param>
    /// <param name="PercentDone">Percentage of THIS file being copied. Total Commander automatically shows a second percent bar if possible when multiple files are copied.</param>
    /// <returns>Total Commander returns <c>true</c> if the user wants to abort copying, and <c>false</c> if the operation can continue.</returns>
    /// <remarks>You should call this function at least twice in the copy functions <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.GetFile(System.String,System.String@,Tools.TotalCommanderT.CopyFlags,Tools.TotalCommanderT.RemoteInfo)"/>, <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.PutFile(System.String,System.String@,Tools.TotalCommanderT.CopyFlags)"/> and <see2 cref2="M:Tools.TotalCommanderT.RenMovFile(System.String,System.String,System.Boolean,System.Boolean,Tools.TotalCommanderT.RemoteInfo)"/>, at the beginning and at the end. If you can't determine the progress, call it with 0% at the beginning and 100% at the end.
    /// <para>During the <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.FindFirst(System.String,Tools.TotalCommanderT.FindData@)"/>/<see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.FindNext(System.Object,Tools.TotalCommanderT.FindData@)"/>/<see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.FindClose(System.Object)"/> loop, the plugin may now call the <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.ProgressProc(System.String,System.String,System.Int32)"/> to make a progess dialog appear. This is useful for very slow connections. Don't call <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.ProgressProc(System.String,System.String,System.Int32)"/> for fast connections! The progress dialog will only be shown for normal dir changes, not for compound operations like get/put. The calls to <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.ProgressProc(System.String,System.String,System.Int32)"/> will also be ignored during the first 5 seconds, so the user isn't bothered with a progress dialog on every dir change.</para></remarks>
    public delegate bool ProgressCallback(FileSystemPlugin^ sender, String^ SourceName, String^ TargetName,int PercentDone);
    /// <summary>Callback function, which the plugin can call to show the FTP connections toolbar, and to pass log messages to it. Totalcmd can show these messages in the log window (ftp toolbar) and write them to a log file.</summary>
    /// <param name="sender">The plugin requesting the operation</param>
    /// <param name="MsgType">Can be one of the <see cref="LogKind"/> flags</param>
    /// <param name="LogString">String which should be logged.
    /// <para>When <paramref name="MsgType"/>is <see2 cref2="F:Tools.TotalCommanderT.LogKind.Connect"/>, the string MUST have a specific format:</para>
    /// <para><c>"CONNECT"</c> followed by a single whitespace, then the root of the file system which was connected, without trailing backslash. Example: <c>CONNECT \Filesystem</c></para>
    /// <para>When <paramref name="MsgType"/> is <see2 cref2="F:Tools.TotalCommanderT.LogKind.TransferComplete"/>, this parameter should contain both the source and target names, separated by an arrow <c>" -> "</c>, e.g. <c>Download complete: \Filesystem\dir1\file1.txt -> c:\localdir\file1.txt</c></para></param>
    /// <remarks>Do NOT call <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.LogProc(Tools.TotalCommanderT.LogKind,System.String)"/> with <see2 cref2="F:Tools.TotalCommanderT.LogKind.Connect"/> if your plugin does not require connect/disconnect! If you call it with <paramref name="MsgType"/> <see2 cref2="F:Tools.TotalCommanderT.LogKind.Connect"/>, the function <see2 cref2="M:Tools.TotalCommanderT.FileSystemPlugin.Disconnect(System.String)"/> will be called (if defined) when the user presses the Disconnect button.</remarks>
    public delegate void LogCallback(FileSystemPlugin^ sender, LogKind MsgType,String^ LogString);
    /// <summary>Callback function, which the plugin can call to request input from the user. When using one of the standard parameters, the request will be in the selected language.</summary>
    /// <param name="sender">The plugin requesting the operation</param>
    /// <param name="RequestType">Can be one of the <see cref="InputRequestKind"/> flags</param>
    /// <param name="CustomTitle">Custom title for the dialog box. If NULL or empty, it will be "Total Commander"</param>
    /// <param name="CustomText">Override the text defined with <paramref name="RequestType"/>. Set this to NULL or an empty string to use the default text. The default text will be translated to the language set in the calling program.</param>
    /// <param name="DefaultText">This string contains the default text presented to the user. Set <paramref name="DefaultText"/>[0]=0 to have no default text.</param>
    /// <param name="maxlen">Maximum length allowed for returned text.</param>
    /// <returns>User-entered text if user clicked Yes or OK. Null otherwise</returns>
    /// <remarks>Leave <paramref name="CustomText"/> empty if you want to use the (translated) default strings!</remarks>
    /// <exception cref="ArgumentException"><paramref name="DefaultText"/> is longer than <paramref name="maxlen"/></exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxlen"/> is less than 1</exception>
    public delegate String^ RequestCallback(FileSystemPlugin^ sender, InputRequestKind RequestType,String^ CustomTitle, String^ CustomText, String^ DefaultText, int maxlen);
    /// <summary>Callback function, which the plugin can call to store passwords in the secure password store, read them back, or copy them to a new connection.</summary>
    /// <param name="sender">The plugin requesting the operation</param>
    /// <param name="mode">The mode of operation</param>
    /// <param name="connectionName">Name of the connection for this operation</param>
    /// <param name="password">Operation-specific, usually the password to be stored, or the target connection when copying/moving a connection</param>
    /// <param name="maxlen">Maximum length, in characters, the password buffer can store when calling one of the load functions</param>
    /// <returns>Password retrieved. Only when <paramref name="mode"> is <see cref="CryptMode::LoadPassword"/> or <see cref="CryptMode::LoadPasswordNoUI"/>. Otherwise returns <paramref name="password"/>.</returns>
    /// <exception cref="CryptException">Crypto operation failed.</exception>
    /// <remarks>This delegate is used only when Total Commander plugin is used outside Total Commander.</remarks>
    /// <version version="1.5.4">This delegate is new in version 1.5.4</version>
    public delegate String^ CryptCallback(FileSystemPlugin^ sender, CryptMode mode, String^ connectionName, String^ password, int maxlen);  
#pragma endregion

#pragma region "View definition"
    /// <summary>Defines source view column value</summary>
    /// <version version="1.5.3">This class in new in version 1.5.3</version>
    public ref class ColumnSource sealed{
    private:
        /// <summary>Contains value of the <see cref="Source"/> property</summary>
        String^ source;
        /// <summary>Contains value of the <see cref="Field"/> property</summary>
        String^ field;
        /// <summary>Contains value of the <see cref="Unit"/> property</summary>
        String^ unit;
    public:
        /// <summary>CTor</summary>
        /// <param name="source">Source of the value. This is plugin name or "<c>tc</c>" for Total Commander</param>
        /// <param name="field">Name of filed inside <paramref name="source"/> to getf value of column form</param>
        /// <param name="unit">Name of value uinit to use to display value of field <paramref name="field"/>. This can be null. Use null if <paramref name="field"/> does not support units.</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="field"/> is null or an empty string</exception>
        /// <exception cref="ArgumentException"><paramref name="source"/>, <paramref name="field"/> or <paramref name="unit"/> contains dot (.), pipe (|), colon (:), end brace (]) or null character</exception>
        ColumnSource(String^ source, String^ field, String^ unit);
        /// <summary>Gets name of source of column value</summary>
        /// <returns>Name of source of column value. This is name of plugin or "<c>tc</c>" for Total Commander itself.</returns>
        property String^ Source{String^ get();}
        /// <summary>Gets name of field to get value of column from</summary>
        /// <returns>Name of field to get value of column from. Possible names of fields depends on <see cref="Source"/>.</returns>
        property String^ Field{String ^get();}
        /// <summary>Gets name of unit to use to display value of <see cref="Field"/></summary>
        /// <returns>Name of unit to use to display value of <see cref="Field"/>. Null when no unit or default unit shall be used.</returns>
        property String^ Unit{String^ get();}
        /// <summary>Gets string representation of column source as used by Total Commander</summary>
        /// <returns>String in form "<c>[=<see cref="Source"/>.<see cref="Field"/>.<see cref="Unit"/>]</c>"</returns>
        virtual String^ ToString() sealed override;
    };
    /// <summary>Defines a column in Total Commander view</summary>
    /// <version version="1.5.3">This class in new in version 1.5.3</version>
    public ref class ColumnDefinition sealed{
    private:
        /// <summary>Contains value of the <see cref="Source"/> property</summary>
        initonly ColumnSource^ source;//[=<fs>.size.bkM2]\n[=fs.writetime]
        /// <summary>Contains value of the <see cref="Name"/> property</summary>
        initonly String^ name;//\n-separated
        /// <summary>Contains value of the <see cref="Width"/> property</summary>
        initonly int width;//,-separated
    public:
        /// <summary>CTor</summary>
        /// <param name="source">Source of column value</param>
        /// <param name="name">Name of column (as show to the user in header</param>
        /// <param name="width">Initial width of column in pixels. Negative value to right-align.</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null -or- <paramref name="name"/> is null or an empty string</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> contains substring "<c>\n</c>" (not a new line but literally "<c>\n</c>").</exception>
        ColumnDefinition(ColumnSource^ source, String^ name, int width);
        /// <summary>Gets source of colun value</summary>
        /// <returns>Source of column value</returns>
        property ColumnSource^ Source{ColumnSource^ get();}
        /// <summary>Gets name of column as show to user</summary>
        /// <returns>Header name of column</returns>
        property String^ Name{String^ get();}
        /// <summary>Gets width of column</summary>
        /// <returns>Initial width of column. Negative for right align.</returns>
        property int Width{int get();}
    };
    /// <summary>Deines custom view in Total Commander</summary>
    /// <remarks>Custom view is a set of columns, their sources, names and widths as show in Total Commander pane to the user</remarks>
    /// <version version="1.5.3">This class in new in version 1.5.3</version>
    public ref class ViewDefinition sealed{
    private:
        /// <summary>Contains value of the <see cref="Columns"/> property</summary>
        initonly array<ColumnDefinition^>^ columns;
        /// <summary>Contains value of the <see cref="AutoAdjust"/> property</summary>
        initonly bool autoAdjust;
        /// <summary>Contains value of the <see cref="HorizontalScroll"/> property</summary>
        initonly bool horizontalScroll;
        /// <summary>Contains value of the <see cref="FileNameWidth"/> property</summary>
        int fileNameWidth;
        /// <summary>Contains value of the <see cref="ExtensionWidth"/> property</summary>
        int extensionWidth;
    public:
        /// <summary>CTor</summary>
        /// <param name="columns">View columns definition. Cannot be null, but can be empty. <note>File name and extension columns are <strong>not</strong> part of this definition.</note></param>
        /// <param name="FileNameWidth">Width of the File Name column. Negative for right align.</param>
        /// <param name="ExtensionWidth">Width of the Extension column. Negative for right align.</param>
        /// <param name="autoAdjust">True to automatically adjust columns width as Total Commander window resizes; false to keep widths</param>
        /// <param name="horizontalScroll">True to show horizontal scrollbar; false not to show horozontal scrollbar</param>
        /// <exception cref="ArgumentNullException"><paramref name="columns"/> is null</exception>
        ViewDefinition(array<ColumnDefinition^>^ columns, int FileNameWidth, int ExtensionWidth, bool autoAdjust, bool horizontalScroll);
        /// <summary>Gets columns definitions</summary>
        /// <returns>Columns definitions defining sources, names anw widths of columns</returns>
        property array<ColumnDefinition^>^ Columns{array<ColumnDefinition^>^ get();}
        /// <summary>Gets value indicationg if column widths are automatically adjusted as Total Commander window resize or not</summary>
        /// <returns>True when columns are automaticaly resized as Total Commander Window resizes; false when column widths are kept unchanged.</returns>
        property bool AutoAdjust{bool get();}
        /// <summary>Gets value indicating if horizontal scrollbar is shown</summary>
        /// <returns>True when horizontal scrollbar is shown; false if it si not shown</returns>
        property bool HorizontalScroll{bool get();}
        /// <summary>Gets width of the File Name column</summary>
        /// <returns>Width of the File Name column. Negative if the column is right-aligned.</returns>
        property int FileNameWidth{int get();}
        /// <summary>Gets width of the Extension column</summary>
        /// <returns>Width of the Extension column. Negative if the column is right-aligned.</returns>
        property int ExtensionWidth{int get();}
        /// <summary>Gets string in Total-Commander-used format that defines all the columns sources.</summary>
        /// <returns>String in format "<c>[=source.field.unit]\n[=source.filed.unit]</c>" Each column source is represented by <see cref="ColumnSource::ToString"/>, column sources are separated by \n (not a new line but "\n").</returns>
        String^ GetColumnSourcesString();
        /// <summary>Gets string in Total-Commander-used format that defines all the columns names.</summary>
        /// <returns>\n-separated column names (\n is "\n" not a nea line)</returns>
        String^ GetNamesString();
        /// <summary>Gets string in Total-Commander.used format that defines all the columns widths</summary>
        /// <returns>Comma-(,)-separated column widths</returns>
        /// <remarks><note>Column widths always includes at least 2-items (1st and 2nd) for File Name and Extension. Those columns never have source and name configured.</note></remarks>
        String^ GetWidthsString();
        /// <summary>Gets string in Total-Commander-used format that defines view options</summary>
        /// <returns>Returns string consistion of two |-separated parts. Part 1 specifies <see cref="AutoAdjust"/> and it is either "<c>auto-adjust-width</c>" (for true) or "<c>-1</c>" (for false). Part 2 specifies <see cref="HorizontalScroll"/> and it is either 0 or 1 (for false or true respectively).</returns>
        String^ GetOptionsString();
    }; 
#pragma endregion
}}
