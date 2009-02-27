#include "define.h"
#include "Helper.h"
#ifdef TC_WFX
#define TCPLUGF 

#include "fsplugin.h"
#include <vcclr.h>
#include "AssemblyResolver.h"
#include "Misc.h"

using namespace System;
using namespace Tools::TotalCommanderT;

#define FUNCTION_TARGET Tools::TotalCommanderT::holder 

#pragma unmanaged
/// <summary>Inmanaged DLL entry point. This method canot contain calls to managed code.</summary>
BOOL APIENTRY DllMain( HANDLE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved){return TRUE;}
#pragma managed
///// <summary>Contains value indicationg if it is necessary to initialize runtime environment and plugin instance</summary>
//bool RequireInitialize = true;

///// <summary>FileSystem plugin helper class - holds file system plugin instance</summary>
//private ref struct FSHelper{
//    /// <summary>Instance of file system plugin class</summary>
//    static FileSystemPlugin^ wfx;
//};
/// <summary>Backs the <see cref="FsGetDefRootName"/> method</summary>
void GetDefRootName(char* DefRootName,int maxlen);
/// <summary>Bactsk the <see ctef="FsInit"/> function</summary>
int Init(int PluginNr,tProgressProc pProgressProc, tLogProc pLogProc,tRequestProc pRequestProc);

#ifdef TC_FS_INIT
    TCPLUGF int __stdcall FsInit(int PluginNr,tProgressProc pProgressProc, tLogProc pLogProc,tRequestProc pRequestProc){
        Initialize();
        return Init(PluginNr,pProgressProc,pLogProc,pRequestProc);
    }
    int Init(int PluginNr,tProgressProc pProgressProc, tLogProc pLogProc,tRequestProc pRequestProc){
        return FUNCTION_TARGET->FsInit(PluginNr,pProgressProc,pLogProc,pRequestProc);
    }
#endif
#ifdef TC_FS_GETDEFROOTNAME
    TCPLUGF void __stdcall FsGetDefRootName(char* DefRootName,int maxlen){
        Initialize();
        return GetDefRootName(DefRootName,maxlen);
    }
    void GetDefRootName(char* DefRootName,int maxlen){
        return FUNCTION_TARGET->FsGetDefRootName(DefRootName, maxlen);    
    }
#endif
#ifdef TC_FS_INIT
#define TC_FS_INIT_2
#endif
#ifdef TC_FS_GETDEFROOTNAME
#define TC_FS_GETDEFROOTNAME_2
#endif
#undef TC_FS_INIT

#undef TC_FS_GETDEFROOTNAME
#define FUNC_MODIF __stdcall 
#include "wfxFunctionCalls.h"

#ifdef TC_FS_INIT_2
#define TC_FS_INIT
#endif
#ifdef TC_FS_GETDEFROOTNAME_2
#define TC_FS_GETDEFROOTNAME
#endif
#endif