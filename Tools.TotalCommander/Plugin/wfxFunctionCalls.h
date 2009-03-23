#ifdef TC_WFX
#ifdef TC_FS_INIT
    TCPLUGF int FUNC_MODIF FsInit(int PluginNr,tProgressProc pProgressProc, tLogProc pLogProc,tRequestProc pRequestProc){return FUNCTION_TARGET->FsInit(PluginNr,pProgressProc,pLogProc,pRequestProc);}
#endif
#ifdef TC_FS_FINDFIRST
    TCPLUGF HANDLE FUNC_MODIF FsFindFirst(char* Path,WIN32_FIND_DATA *FindData){return FUNCTION_TARGET->FsFindFirst(Path, FindData);}
#endif
#ifdef TC_FS_FINDNEXT
    TCPLUGF BOOL FUNC_MODIF FsFindNext(HANDLE Hdl,WIN32_FIND_DATA *FindData){return FUNCTION_TARGET->FsFindNext(Hdl, FindData);}
#endif
#ifdef TC_FS_FINDCLOSE
    TCPLUGF int FUNC_MODIF FsFindClose(HANDLE Hdl){return FUNCTION_TARGET->FsFindClose(Hdl);}
#endif
#ifdef TC_FS_MKDIR
    TCPLUGF BOOL FUNC_MODIF FsMkDir(char* Path){return FUNCTION_TARGET->FsMkDir(Path);}
#endif
#ifdef TC_FS_EXECUTEFILE
    TCPLUGF int FUNC_MODIF FsExecuteFile(HWND MainWin,char* RemoteName,char* Verb){return FUNCTION_TARGET->FsExecuteFile(/*(HANDLE)*/MainWin, RemoteName, Verb);}
#endif
#ifdef TC_FS_RENMOVFILE
    TCPLUGF int FUNC_MODIF FsRenMovFile(char* OldName,char* NewName,BOOL Move,  BOOL OverWrite,RemoteInfoStruct* ri){return FUNCTION_TARGET->FsRenMovFile(OldName, NewName, Move, OverWrite, ri);}
#endif
#ifdef TC_FS_GETFILE
    TCPLUGF int FUNC_MODIF FsGetFile(char* RemoteName,char* LocalName,int CopyFlags, RemoteInfoStruct* ri){return FUNCTION_TARGET->FsGetFile(RemoteName, LocalName, CopyFlags, ri);}
#endif
#ifdef TC_FS_PUTFILE
    TCPLUGF int FUNC_MODIF FsPutFile(char* LocalName,char* RemoteName,int CopyFlags){return FUNCTION_TARGET->FsPutFile(LocalName, RemoteName, CopyFlags);}
#endif
#ifdef TC_FS_DELETEFILE
    TCPLUGF BOOL FUNC_MODIF FsDeleteFile(char* RemoteName){return FUNCTION_TARGET->FsDeleteFile(RemoteName);}
#endif
#ifdef TC_FS_REMOVEDIR
    TCPLUGF BOOL FUNC_MODIF FsRemoveDir(char* RemoteName){return FUNCTION_TARGET->FsRemoveDir(RemoteName);}
#endif
#ifdef TC_FS_DISCONNECT
    TCPLUGF BOOL FUNC_MODIF FsDisconnect(char* DisconnectRoot){return FUNCTION_TARGET->FsDisconnect(DisconnectRoot);}
#endif
#ifdef TC_FS_SETATTR 
    TCPLUGF BOOL FUNC_MODIF FsSetAttr(char* RemoteName,int NewAttr){return FUNCTION_TARGET->FsSetAttr(RemoteName,NewAttr);}
#endif
#ifdef TC_FS_SETTIME
    TCPLUGF BOOL FUNC_MODIF FsSetTime(char* RemoteName,::FILETIME *CreationTime,::FILETIME *LastAccessTime,::FILETIME *LastWriteTime){return FUNCTION_TARGET->FsSetTime(RemoteName, CreationTime, LastAccessTime, LastWriteTime);}
#endif
#ifdef TC_FS_STATUSINFO
    TCPLUGF void FUNC_MODIF FsStatusInfo(char* RemoteDir,int InfoStartEnd,int InfoOperation){FUNCTION_TARGET->FsStatusInfo(RemoteDir, InfoStartEnd, InfoOperation);}
#endif
#ifdef TC_FS_GETDEFROOTNAME
    TCPLUGF void FUNC_MODIF FsGetDefRootName(char* DefRootName,int maxlen){return FUNCTION_TARGET->FsGetDefRootName(DefRootName, maxlen);}
#endif
#ifdef TC_FS_EXTRACTCUSTOMICON
    TCPLUGF int FUNC_MODIF FsExtractCustomIcon(char* RemoteName,int ExtractFlags,HICON* TheIcon){return FUNCTION_TARGET->FsExtractCustomIcon(RemoteName, ExtractFlags, TheIcon);}
#endif
#ifdef TC_FS_SETDEFAULTPARAMS
    TCPLUGF void FUNC_MODIF FsSetDefaultParams(FsDefaultParamStruct* dps){return FUNCTION_TARGET->FsSetDefaultParams(dps);}
#endif
#ifdef TC_FS_GETPREVIEWBITMAP
    TCPLUGF int FUNC_MODIF FsGetPreviewBitmap(char* RemoteName,int width,int height,HBITMAP* ReturnedBitmap){return FUNCTION_TARGET->FsGetPreviewBitmap(RemoteName, width, height, ReturnedBitmap);}
#endif
#ifdef TC_FS_LINKSTOLOCALFILES
    TCPLUGF BOOL FUNC_MODIF FsLinksToLocalFiles(void){return FUNCTION_TARGET->FsLinksToLocalFiles();}
#endif
#ifdef TC_FS_GETLOCALNAME
    TCPLUGF BOOL FUNC_MODIF FsGetLocalName(char* RemoteName,int maxlen){return FUNCTION_TARGET->FsGetLocalName(RemoteName, maxlen);}
#endif
#include "CommonWfxWdxCalls.h"
#ifdef TC_FS_GETDEFAULTVIEW
    TCPLUGF BOOL FUNC_MODIF FsContentGetDefaultView(char* ViewContents,char* ViewHeaders,char* ViewWidths,char* ViewOptions,int maxlen){return FUNCTION_TARGET->FsContentGetDefaultView(ViewContents, ViewHeaders, ViewWidths, ViewOptions, maxlen);}
#endif
#endif