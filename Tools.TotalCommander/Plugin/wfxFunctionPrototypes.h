#ifdef TC_WFX
#ifdef TC_FS_INIT
    int FsInit(int PluginNr,tProgressProc pProgressProc, tLogProc pLogProc,tRequestProc pRequestProc);
#endif
#ifdef TC_FS_FINDFIRST
    HANDLE FsFindFirst(char* Path,WIN32_FIND_DATA *FindData);
#endif
#ifdef TC_FS_FINDNEXT
    BOOL FsFindNext(HANDLE Hdl,WIN32_FIND_DATA *FindData);
#endif
#ifdef TC_FS_FINDCLOSE
    int FsFindClose(HANDLE Hdl);
#endif
#ifdef TC_FS_MKDIR
    BOOL FsMkDir(char* Path);
#endif
#ifdef TC_FS_EXECUTEFILE
    int FsExecuteFile(HWND MainWin,char* RemoteName,char* Verb);
#endif
#ifdef TC_FS_RENMOVFILE
    int FsRenMovFile(char* OldName,char* NewName,BOOL Move,  BOOL OverWrite,RemoteInfoStruct* ri);
#endif
#ifdef TC_FS_GETFILE
    int FsGetFile(char* RemoteName,char* LocalName,int CopyFlags, RemoteInfoStruct* ri);
#endif
#ifdef TC_FS_PUTFILE
    int FsPutFile(char* LocalName,char* RemoteName,int CopyFlags);
#endif
#ifdef TC_FS_DELETEFILE
    BOOL FsDeleteFile(char* RemoteName);
#endif
#ifdef TC_FS_REMOVEDIR
    BOOL FsRemoveDir(char* RemoteName);
#endif
#ifdef TC_FS_DISCONNECT
    BOOL FsDisconnect(char* DisconnectRoot);
#endif
#ifdef TC_FS_SETATTR 
    BOOL FsSetAttr(char* RemoteName,int NewAttr);
#endif
#ifdef TC_FS_SETTIME
    BOOL FsSetTime(char* RemoteName,FILETIME *CreationTime,FILETIME *LastAccessTime,FILETIME *LastWriteTime);
#endif
#ifdef TC_FS_STATUSINFO
    void FsStatusInfo(char* RemoteDir,int InfoStartEnd,int InfoOperation);
#endif
#ifdef TC_FS_GETDEFROOTNAME
    void FsGetDefRootName(char* DefRootName,int maxlen);
#endif
#ifdef TC_FS_EXTRACTCUSTOMICON
    int FsExtractCustomIcon(char* RemoteName,int ExtractFlags,HICON* TheIcon);
#endif
#ifdef TC_FS_SETDEFAULTPARAMS
    void FsSetDefaultParams(FsDefaultParamStruct* dps);
#endif
#ifdef TC_FS_GETPREVIEWBITMAP
    int FsGetPreviewBitmap(char* RemoteName,int width,int height,HBITMAP* ReturnedBitmap);
#endif
#ifdef TC_FS_LINKSTOLOCALFILES
    BOOL FsLinksToLocalFiles(void);
#endif
#ifdef TC_FS_GETLOCALNAME
    BOOL FsGetLocalName(char* RemoteName,int maxlen);
#endif
#include "CommonWfxWdxPrototypes.h"
#ifdef TC_FS_GETDEFAULTVIEW
    BOOL FsContentGetDefaultView(char* ViewContents,char* ViewHeaders,char* ViewWidths,char* ViewOptions,int maxlen);
#endif
#endif