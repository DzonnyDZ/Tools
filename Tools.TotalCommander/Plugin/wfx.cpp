#include "define.h"
#include "Helper.h"
#ifdef TC_WFX

#include "fsplugin.h"
#include <vcclr.h>
#include "AssemblyResolver.h"
#include "Misc.h"
#include "Export.h"

using namespace System;
using namespace Tools::TotalCommanderT;

#undef TC_NAME_PREFIX 
#undef TC_FUNCTION_TARGET 
#undef TC_LINE_PREFIX
#undef TC_FUNC_MEMBEROF
#undef TC_FUNC_PREFIX_A
#undef TC_FUNC_PREFIX_B
#define TC_FNC_BODY

#pragma unmanaged
/// <summary>Unmanaged DLL entry point. This method canot contain calls to managed code.</summary>
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
/// <summary>Backs the <see ctef="FsInit"/> function</summary>
int Init(int PluginNr,tProgressProc pProgressProc, tLogProc pLogProc,tRequestProc pRequestProc);

#define TC_NAME_PREFIX __stdcall
#define TC_FUNCTION_TARGET Tools::TotalCommanderT::holder 
#define TC_LINE_PREFIX
#define TC_FUNC_MEMBEROF
#define TC_FUNC_PREFIX_A Fs
#define TC_FUNC_PREFIX_B

#ifdef TC_FS_INIT
    TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF FsInit(int PluginNr,tProgressProc pProgressProc, tLogProc pLogProc,tRequestProc pRequestProc){
        Initialize();
        return Init(PluginNr,pProgressProc,pLogProc,pRequestProc);
    }
    int Init(int PluginNr,tProgressProc pProgressProc, tLogProc pLogProc,tRequestProc pRequestProc){
        return TC_FUNCTION_TARGET->FsInit(PluginNr,pProgressProc,pLogProc,pRequestProc);
    }
#endif
#ifdef TC_FS_GETDEFROOTNAME
    TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF FsGetDefRootName(char* DefRootName,int maxlen){
        Initialize();
        return GetDefRootName(DefRootName,maxlen);
    }
    void GetDefRootName(char* DefRootName,int maxlen){
        return TC_FUNCTION_TARGET->FsGetDefRootName(DefRootName, maxlen);    
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

#include "AllTCFunctions.h"

#ifdef TC_FS_INIT_2
    #define TC_FS_INIT
#endif
#ifdef TC_FS_GETDEFROOTNAME_2
    #define TC_FS_GETDEFROOTNAME
#endif

#endif