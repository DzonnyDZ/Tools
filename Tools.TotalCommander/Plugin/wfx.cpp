#include "define.h"
#include "Helper.h"
#ifdef TC_WFX
//This file defines Total Commander File System (WFX) plugin
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

//#pragma unmanaged
//    /// <summary>Unmanaged DLL entry point. This method canot contain calls to managed code.</summary>
//    /// <param name="hModule">A handle to the DLL module. The value is the base address of the DLL.</param>
//    /// <param name="ul_reason_for_call">The reason code that indicates why the DLL entry-point function is being called.</param>
//    /// <param name="lpReserved">
//    /// <para>If <paramref name="ul_reason_for_call"/> is <c>DLL_PROCESS_ATTACH</c>, <paramref name="lpReserved"/> is null for dynamic loads and non-null for static loads.</para>
//    /// <para>If <paramref name="ul_reason_for_call"/> is <c>DLL_PROCESS_DETACH</c>, <paramref name="lpReserved"/> is null if <c>FreeLibrary</c> has been called or the DLL load failed and non-null if the process is terminating.</para>
//    /// </param>
//    /// <returns>This implementation always returns true</returns>
//    BOOL APIENTRY DllMain( HANDLE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved){return TRUE;}
//#pragma managed


#ifdef TC_FS_INIT
    /// <summary>Backs the <see ctef="FsInit"/> function</summary>
    int Init(int PluginNr, tProgressProcW pProgressProc, tLogProcW pLogProc, tRequestProcW pRequestProc);
#endif

#ifdef TC_FS_GETDEFROOTNAME
    /// <summary>Backs the <see cref="FsGetDefRootName"/> method</summary>
    void GetDefRootName(char* DefRootName, int maxlen);
#endif

#define TC_NAME_PREFIX __stdcall
#define TC_FUNCTION_TARGET Tools::TotalCommanderT::holder 
#define TC_LINE_PREFIX
#define TC_FUNC_MEMBEROF
#define TC_FUNC_PREFIX_A Fs
#define TC_FUNC_PREFIX_B
#ifdef TC_A2W
    #undef TC_A2W
#endif

#ifdef TC_FS_INIT
    //Unicode
    TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF FsInitW(int PluginNr, tProgressProcW pProgressProc, tLogProcW pLogProc, tRequestProcW pRequestProc){
        Initialize();
        return Init(PluginNr, pProgressProc, pLogProc, pRequestProc);
    }
    inline int Init(int PluginNr, tProgressProcW pProgressProc, tLogProcW pLogProc, tRequestProcW pRequestProc){
        return TC_FUNCTION_TARGET->FsInit(PluginNr, pProgressProc, pLogProc, pRequestProc);
    }
    //ANSI
    inline int Init(int PluginNr, tProgressProc pProgressProc, tLogProc pLogProc, tRequestProc pRequestProc){
        return TC_FUNCTION_TARGET->FsInit(PluginNr, pProgressProc, pLogProc, pRequestProc);
    }
    TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF FsInit(int PluginNr, tProgressProc pProgressProc, tLogProc pLogProc, tRequestProc pRequestProc){
        Initialize();
        return Init(PluginNr, pProgressProc, pLogProc, pRequestProc);
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