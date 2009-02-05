#pragma once
#include "fsplugin.h"
using namespace System;
namespace Tools{namespace TotalCommanderT{

    public enum class FileSystemExitCode{
        OK = FS_FILE_OK,
        FileExists = FS_FILE_EXISTS,
        FileNotFound = FS_FILE_NOTFOUND,
        ReadError = FS_FILE_READERROR,
        WriteError = FS_FILE_WRITEERROR,
        UserAbort = FS_FILE_USERABORT,
        NotSupported = FS_FILE_NOTSUPPORTED,
        ExistsResumeAllowed = FS_FILE_EXISTSRESUMEALLOWED
    };
    public enum class FileSystemExecExitCode{
        OK = FS_EXEC_OK,
        Error = FS_EXEC_ERROR,
        Yourself = FS_EXEC_YOURSELF,
        Symlink = FS_EXEC_SYMLINK
    };
    
    [FlagsAttribute()] 
    public enum class CopyFlags{
        Overwrite = FS_COPYFLAGS_OVERWRITE,
        Resume = FS_COPYFLAGS_RESUME,
        Move = FS_COPYFLAGS_MOVE,
        SameCase = FS_COPYFLAGS_EXISTS_SAMECASE,
        DiferentCase = FS_COPYFLAGS_EXISTS_DIFFERENTCASE
    };
    /// <summary>When user input is requested by plugin, one of those values may be used</summary>
    public enum class InpuRequestKind{
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
    /// <remarks>Total Commander supports logging to files. While one log file will store all messages, the other will only store important errors, connects, disconnects and complete operations/transfers, but not messages of type Details.</remarks>
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

    public enum class FileSystemStatus{
        Start = FS_STATUS_START,
        End = FS_STATUS_END
    };
    public enum class FileSystemOperationStatus{
        List = FS_STATUS_OP_LIST,
        GetSingle = FS_STATUS_OP_GET_SINGLE,
        GetMulti = FS_STATUS_OP_GET_MULTI,
        PuSingle = FS_STATUS_OP_PUT_SINGLE,
        PuMulti = FS_STATUS_OP_PUT_MULTI,
        RenMovSingle = FS_STATUS_OP_RENMOV_SINGLE,
        RenMovMulti = FS_STATUS_OP_RENMOV_MULTI,
        Delete = FS_STATUS_OP_DELETE,
        Attrib = FS_STATUS_OP_ATTRIB,
        MkDir = FS_STATUS_OP_MKDIR,
        Exec = FS_STATUS_OP_EXEC,
        CalcSize = FS_STATUS_OP_CALCSIZE
    };

    public value class RemoteInfo{
    public:
        property DWORD SizeLow;
        property DWORD SizeHigh;
        property int Attr;
    internal:
        RemoteInfo(const RemoteInfoStruct& ri);
    };

    /// <summary>Abstract base class for Total Commander file-system plugins</summary>
    public ref class FileSystemPlugin abstract{
    internal:
#pragma region "TC functions"
        int FsInit(int PluginNr,tProgressProc pProgressProc, tLogProc pLogProc,tRequestProc pRequestProc);
        HANDLE FsFindFirst(char* Path,WIN32_FIND_DATA *FindData);
        BOOL FsFindNext(HANDLE Hdl,WIN32_FIND_DATA *FindData);
        int FsFindClose(HANDLE Hdl);
        BOOL FsMkDir(char* Path);
        int FsExecuteFile(HWND MainWin,char* RemoteName,char* Verb);
        int FsRenMovFile(char* OldName,char* NewName,BOOL Move, BOOL OverWrite,RemoteInfoStruct* ri);
        int FsGetFile(char* RemoteName,char* LocalName,int CopyFlags, RemoteInfoStruct* ri);
        int FsPutFile(char* LocalName,char* RemoteName,int CopyFlags);
        BOOL FsDeleteFile(char* RemoteName);
        BOOL FsRemoveDir(char* RemoteName);
        BOOL FsDisconnect(char* DisconnectRoot);
        BOOL FsSetAttr(char* RemoteName,int NewAttr);
        BOOL FsSetTime(char* RemoteName,FILETIME *CreationTime, FILETIME *LastAccessTime,FILETIME *LastWriteTime);
        void FsStatusInfo(char* RemoteDir,int InfoStartEnd,int InfoOperation);
        void FsGetDefRootName(char* DefRootName,int maxlen);
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
        /// <summary>Gets value indicating if this plugin instance was initialized or not</summary>
        property bool Initialized{bool get();}
        /// <summary>When overriden in derived class provides custom code invoked when plugin is initialized.</summary>
        /// <remarks>When this method is called the <see cref="Initialized"/> property has value true and <see cref="PluginNr"/> is already set.
        /// <para>Default implementation of this method does nothing</para></remarks>
        virtual void OnInit();
#pragma endregion
    };
}}