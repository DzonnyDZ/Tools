#include "define.h"
#ifdef TC_WFX
#define TCPLUGF __declspec(dllexport)
#include "..\fsplugin.h"
#include <vcclr.h>

using namespace System;
using namespace Tools::TotalCommanderT;

/// <summary>Gets plugin class instance</summary>
/// <returns>Plugin class instance</returns>
inline FileSystemPlugin^ GetWfx(){
    return new TC_WFX();
}

/// <summary>Plugin instance</summary>
gcroot<FileSystemPlugin^> wfx;


//Ensure plugin class instance
#define WFX (!wfx ? wfx = GetWfx() : wfx)
 
#ifdef TC_FS_INIT
    inline TCPLUGF int __stdcall FsInit(int PluginNr,tProgressProc pProgressProc, tLogProc pLogProc,tRequestProc pRequestProc){return WFX->FsInit(PluginNr,pProgressProc,pLogProc,pRequestProc);}
#endif
#ifdef TC_FS_FINDFIRST
    inline TCPLUGF HANDLE __stdcall FsFindFirst(char* Path,WIN32_FIND_DATA *FindData){return WFX->FsFindFirst(Path, FindData);}
#endif
#ifdef TC_FS_FINDNEXT
    inline TCPLUGF BOOL __stdcall FsFindNext(HANDLE Hdl,WIN32_FIND_DATA *FindData){return WFX->FsFindNext(Hdl, FindData);}
#endif
#ifdef TC_FS_FINDCLOSE
    inline TCPLUGF int __stdcall FsFindClose(HANDLE Hdl){return WFX->FsFindClose(Hdl);}
#endif
#ifdef TC_FS_MKDIR
    inline TCPLUGF BOOL __stdcall FsMkDir(char* Path){return WFX->FsMkDir(Path);}
#endif
#ifdef TC_FS_EXECUTEFILE
    inline TCPLUGF int __stdcall FsExecuteFile(HWND MainWin,char* RemoteName,char* Verb){return WFX->FsExecuteFile((HANDLE)MainWin, RemoteName, Verb);}
#endif
#ifdef TC_FS_RENMOVFILE
    inline TCPLUGF int __stdcall FsRenMovFile(char* OldName,char* NewName,BOOL Move,  BOOL OverWrite,RemoteInfoStruct* ri){return WFX->FsRenMovFile(OldName, NewName, Move, OverWrite, ri);}
#endif
#ifdef TC_FS_GETFILE
    inline TCPLUGF int __stdcall FsGetFile(char* RemoteName,char* LocalName,int CopyFlags, RemoteInfoStruct* ri){return WFX->FsGetFile(RemoteName, LocalName, CopyFlags, ri);}
#endif
#ifdef TC_FS_PUTFILE
    inline TCPLUGF int __stdcall FsPutFile(char* LocalName,char* RemoteName,int CopyFlags){return WFX->FsPutFile(LocalName, RemoteName, CopyFlags);}
#endif
#ifdef TC_FS_DELETEFILE
    inline TCPLUGF BOOL __stdcall FsDeleteFile(char* RemoteName){return WFX->FsDeleteFile(RemoteName);}
#endif
#ifdef TC_FS_REMOVEDIR
    inline TCPLUGF BOOL __stdcall FsRemoveDir(char* RemoteName){return WFX->FsRemoveDir(RemoteName);}
#endif
#ifdef TC_FS_DISCONNECT
    inline TCPLUGF BOOL __stdcall FsDisconnect(char* DisconnectRoot){return WFX->FsDisconnect(DisconnectRoot);}
#endif
#ifdef TC_FS_SETATTR 
    inline TCPLUGF BOOL __stdcall FsSetAttr(char* RemoteName,int NewAttr){return WFX->FsSetAttr(RemoteName,NewAttr);}
#endif
#ifdef TC_FS_SETTIME
    inline TCPLUGF BOOL __stdcall FsSetTime(char* RemoteName,FILETIME *CreationTime,FILETIME *LastAccessTime,FILETIME *LastWriteTime){return WFX->FsSetTime(RemoteName, CreationTime, LastAccessTime, LastWriteTime);}
#endif
#ifdef TC_FS_STATUSINFO
    inline TCPLUGF void __stdcall FsStatusInfo(char* RemoteDir,int InfoStartEnd,int InfoOperation){return WFX->FsStatusInfo(RemoteDir, InfoStartEnd, InfoOperation);}
#endif
#ifdef TC_FS_GETDEFROOTNAME
    inline TCPLUGF void __stdcall FsGetDefRootName(char* DefRootName,int maxlen){return WFX->FsGetDefRootName(DefRootName, maxlen);}
#endif
#ifdef TC_FS_EXTRACTCUSTOMICON
    inline TCPLUGF int __stdcall FsExtractCustomIcon(char* RemoteName,int ExtractFlags,HICON* TheIcon){return WFX->FsExtractCustomIcon(RemoteName, ExtractFlags, TheIcon);}
#endif
#ifdef TC_FS_SETDEFAULTPARAMS
    inline TCPLUGF void __stdcall FsSetDefaultParams(FsDefaultParamStruct* dps){return WFX->FsSetDefaultParams(dps);}
#endif
#ifdef TC_FS_GETPREVIEWBITMAP
    inline TCPLUGF int __stdcall FsGetPreviewBitmap(char* RemoteName,int width,int height,HBITMAP* ReturnedBitmap){return WFX->FsGetPreviewBitmap(RemoteName, width, height, ReturnedBitmap);}
#endif
#ifdef TC_FS_LINKSTOLOCALFILES
    inline TCPLUGF BOOL __stdcall FsLinksToLocalFiles(void){return WFX->FsLinksToLocalFiles();}
#endif
#ifdef TC_FS_GETLOCALNAME
    inline TCPLUGF BOOL __stdcall FsGetLocalName(char* RemoteName,int maxlen){return WFX->FsGetLocalName(RemoteName, maxlen);}
#endif
#endif