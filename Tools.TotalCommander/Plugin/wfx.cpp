#include "define.h"
#ifdef TC_WFX
#define TCPLUGF 
//__declspec(dllexport)
#include "fsplugin.h"
#include <vcclr.h>
#include "AssemblyResolver.h"

using namespace System;
using namespace Tools::TotalCommanderT;


#pragma unmanaged
BOOL APIENTRY DllMain( HANDLE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved){return TRUE;}
#pragma managed

/// <summary>FileSystem plugin helper class - holds file system plugin instance</summary>
private ref struct FSHelper{
    /// <summary>Gets plugin class instance</summary>
    /// <returns>Plugin class instance</returns>
    /// <remarks>Called only once</remarks>
    inline static void EnsureWfx(){ wfx = TC_WFX; }
    /// <summary>Instance of file system plugin class</summary>
    static FileSystemPlugin^ wfx;
};
/// <summary>Helper class - holds value indicating if <see cref="FSHelper"/> was initialized</summary>
private ref struct Helper{
    /// <summary>Holds value indicating if <see cref="FSHelper"/> was initialized</summary>
    static bool done = false;
};
/// <summary>Calls <see cref="FSHelper::EnsureWfx"/></summary>
void SetupWfx(){ FSHelper::EnsureWfx(); }
/// <summary>Gets <see cref="FSHelper::wfx"/> as <see cref="Object"/></summary>
Object^ GetWfx(){ return FSHelper::wfx; }
/// <summary>Ensures assembly loader and plugin instance</summary>
/// <returns><se cref="GetWfx"/></returns>
Object^ Ensure(){
    if(!Helper::done){
        AssemblyResolver::Setup();
        SetupWfx();
    }
    return GetWfx();
}


//Ensure plugin class instance
#define WFX ((FileSystemPlugin^) Ensure())
 
#ifdef TC_FS_INIT
    TCPLUGF int __stdcall FsInit(int PluginNr,tProgressProc pProgressProc, tLogProc pLogProc,tRequestProc pRequestProc){return WFX->FsInit(PluginNr,pProgressProc,pLogProc,pRequestProc);}
#endif
#ifdef TC_FS_FINDFIRST
    TCPLUGF HANDLE __stdcall FsFindFirst(char* Path,WIN32_FIND_DATA *FindData){return WFX->FsFindFirst(Path, FindData);}
#endif
#ifdef TC_FS_FINDNEXT
    TCPLUGF BOOL __stdcall FsFindNext(HANDLE Hdl,WIN32_FIND_DATA *FindData){return WFX->FsFindNext(Hdl, FindData);}
#endif
#ifdef TC_FS_FINDCLOSE
    TCPLUGF int __stdcall FsFindClose(HANDLE Hdl){return WFX->FsFindClose(Hdl);}
#endif
#ifdef TC_FS_MKDIR
    TCPLUGF BOOL __stdcall FsMkDir(char* Path){return WFX->FsMkDir(Path);}
#endif
#ifdef TC_FS_EXECUTEFILE
    TCPLUGF int __stdcall FsExecuteFile(HWND MainWin,char* RemoteName,char* Verb){return WFX->FsExecuteFile((HANDLE)MainWin, RemoteName, Verb);}
#endif
#ifdef TC_FS_RENMOVFILE
    TCPLUGF int __stdcall FsRenMovFile(char* OldName,char* NewName,BOOL Move,  BOOL OverWrite,RemoteInfoStruct* ri){return WFX->FsRenMovFile(OldName, NewName, Move, OverWrite, ri);}
#endif
#ifdef TC_FS_GETFILE
    TCPLUGF int __stdcall FsGetFile(char* RemoteName,char* LocalName,int CopyFlags, RemoteInfoStruct* ri){return WFX->FsGetFile(RemoteName, LocalName, CopyFlags, ri);}
#endif
#ifdef TC_FS_PUTFILE
    TCPLUGF int __stdcall FsPutFile(char* LocalName,char* RemoteName,int CopyFlags){return WFX->FsPutFile(LocalName, RemoteName, CopyFlags);}
#endif
#ifdef TC_FS_DELETEFILE
    TCPLUGF BOOL __stdcall FsDeleteFile(char* RemoteName){return WFX->FsDeleteFile(RemoteName);}
#endif
#ifdef TC_FS_REMOVEDIR
    TCPLUGF BOOL __stdcall FsRemoveDir(char* RemoteName){return WFX->FsRemoveDir(RemoteName);}
#endif
#ifdef TC_FS_DISCONNECT
    TCPLUGF BOOL __stdcall FsDisconnect(char* DisconnectRoot){return WFX->FsDisconnect(DisconnectRoot);}
#endif
#ifdef TC_FS_SETATTR 
    TCPLUGF BOOL __stdcall FsSetAttr(char* RemoteName,int NewAttr){return WFX->FsSetAttr(RemoteName,NewAttr);}
#endif
#ifdef TC_FS_SETTIME
    TCPLUGF BOOL __stdcall FsSetTime(char* RemoteName,FILETIME *CreationTime,FILETIME *LastAccessTime,FILETIME *LastWriteTime){return WFX->FsSetTime(RemoteName, CreationTime, LastAccessTime, LastWriteTime);}
#endif
#ifdef TC_FS_STATUSINFO
    TCPLUGF void __stdcall FsStatusInfo(char* RemoteDir,int InfoStartEnd,int InfoOperation){return WFX->FsStatusInfo(RemoteDir, InfoStartEnd, InfoOperation);}
#endif
#ifdef TC_FS_GETDEFROOTNAME
    TCPLUGF void __stdcall FsGetDefRootName(char* DefRootName,int maxlen){
        /*return WFX->FsGetDefRootName(DefRootName, maxlen);*/
        WFX;
        DefRootName[0]='T';
        DefRootName[1]='e';
        DefRootName[2]='s';
        DefRootName[3]='t';
        DefRootName[4]=0;
    }
#endif
#ifdef TC_FS_EXTRACTCUSTOMICON
    TCPLUGF int __stdcall FsExtractCustomIcon(char* RemoteName,int ExtractFlags,HICON* TheIcon){return WFX->FsExtractCustomIcon(RemoteName, ExtractFlags, TheIcon);}
#endif
#ifdef TC_FS_SETDEFAULTPARAMS
    TCPLUGF void __stdcall FsSetDefaultParams(FsDefaultParamStruct* dps){return WFX->FsSetDefaultParams(dps);}
#endif
#ifdef TC_FS_GETPREVIEWBITMAP
    TCPLUGF int __stdcall FsGetPreviewBitmap(char* RemoteName,int width,int height,HBITMAP* ReturnedBitmap){return WFX->FsGetPreviewBitmap(RemoteName, width, height, ReturnedBitmap);}
#endif
#ifdef TC_FS_LINKSTOLOCALFILES
    TCPLUGF BOOL __stdcall FsLinksToLocalFiles(void){return WFX->FsLinksToLocalFiles();}
#endif
#ifdef TC_FS_GETLOCALNAME
    TCPLUGF BOOL __stdcall FsGetLocalName(char* RemoteName,int maxlen){return WFX->FsGetLocalName(RemoteName, maxlen);}
#endif
#endif