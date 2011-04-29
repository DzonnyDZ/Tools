#pragma once

#include "..\Plugin\fsplugin.h"

namespace Tools{namespace TotalCommanderT{
    using namespace System;
    using namespace System::ComponentModel;
 
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
    /// <version version="1.5.4">Constants <see cref2="F:Tools.TotalCommanderT.PutMultiThread"/> and <see cref2="F:Tools.TotalCommanderT.GetMultiThread"/> for background thread transfers added</version>
    /// <version version="1.5.4">Fix: Constants renamed <c>PuSingle</c> to <c>PutSingle</c>, <c>PuMulti</c> to <c>PutMulti</c></version>
    public enum class OperationKind{
        /// <summary>Retrieve a directory listing</summary>
        List = FS_STATUS_OP_LIST,
        /// <summary>Get a single file from the plugin file system</summary>
        GetSingle = FS_STATUS_OP_GET_SINGLE,
        /// <summary>Get multiple files, may include subdirs</summary>
        GetMulti = FS_STATUS_OP_GET_MULTI,
        /// <summary>Get multiple files in separate thread, may include subdirs</summary>
        /// <version version="1.5.4">This constant is new in version 1.5.4</version>
        GetMultiThread = FS_STATUS_OP_GET_MULTI_THREAD,
        /// <summary>Put a single file to the plugin file system</summary>
        /// <version version="1.5.4">Renamed from <c>PuSingle</c> to <c>PutSingle</c></version>
        PutSingle = FS_STATUS_OP_PUT_SINGLE,
        /// <summary>Put multiple files, may include subdirs</summary>
        /// <version version="1.5.4">Renamed form <c>PuMulti</c> to <c>PutMulti</c></version>
        PutMulti = FS_STATUS_OP_PUT_MULTI,
        /// <summary>Put multiple files in separate thread, may include subdirs</summary>
        /// <version version="1.5.4">This constant is new in version 1.5.4</version>
        PutMultiThread = FS_STATUS_OP_PUT_MULTI_THREAD,
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

    /// <summary>Flags determining if and how plugin supports background transfers</summary>
    /// <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    [Flags]
    public enum class BackgroundTransferSupport{
        /// <summary>Plugin supports uploads in background</summary>
        BackgroundUpload = BG_UPLOAD,
        /// <summary>Plugin requires separate connection for background transfers -> ask user first</summary>
        AskUser = BG_ASK_USER
    };
}}