#include "define.h"
#include <vcclr.h>
#include "TCHeaders.h"
#pragma once
using namespace System;
using namespace Tools::TotalCommanderT;
using namespace System::Reflection;
using namespace System::Runtime::Remoting;

#undef TC_FNC_HEADER
#undef TC_LINE_PREFIX
#undef TC_NAME_PREFIX
#undef TC_FUNC_MEMBEROF
#undef TC_FNC_BODY

namespace Tools{namespace TotalCommanderT{
    /// <summary>Holds plugin Application domain. Instantiated in plugin application domain.</summary>
    ref struct AppDomainHolder;
   /* /// <summary>Indicates if plugin initialization is needed</summary>
    extern bool RequireInitialize = true;
    /// <summary>Holds instance of the <see cref="AppDomainHolder"/> class</summary>
    extern gcroot<AppDomainHolder^> holder;*/
    /// <summary>Initializes the plugin instance</summary>
    void InitializePlugin();
    /// <summary>Initializes application domain</summary>
    void Initialize();
    /// <summary>Holds plugin instance. Instantiated in plugin application domain.</summary>
    private ref struct PluginInstanceHolder{
        /// <summary>CTor</summary>
        PluginInstanceHolder();
        #if defined(TC_WFX)
            /// <summary>Holds instance of File System plugin (WFX)</summary>
            FileSystemPlugin^ instance;
        #elif defined(TC_WDX)
            /// <summary>Holds instance of Content plugin (WDX)</summary>
            ContentPlugin^ instance;
        #elif defined(TC_WLX)
            /// <summary>Holds instance of Lister plugin (WLX)</summary>
            ListerPlugin^ instance;
        #elif defined(TC_WDX)
            /// <summary>Holds instance of Packer plugin (WCX)</summary>
            PackerPlugin^ instance;
        #endif
        #define TC_FNC_HEADER
        #define TC_LINE_PREFIX
        #define TC_NAME_PREFIX
        #define TC_FUNC_MEMBEROF
        #undef TC_FNC_BODY
        #include "AllTCFunctions.h"
    };

#undef TC_FNC_HEADER
#undef TC_LINE_PREFIX
#undef TC_NAME_PREFIX
#undef TC_FUNC_MEMBEROF
#undef TC_FNC_BODY

    private ref struct AppDomainHolder : MarshalByRefObject {
        /// <summary>CTor</summary>
        AppDomainHolder();
        /// <summary>Holds instance of the <see cref="PluginInstanceHolder"/> class</summary>
        PluginInstanceHolder^ holder;
        #define TC_FNC_HEADER
        #define TC_LINE_PREFIX
        #define TC_NAME_PREFIX
        #define TC_FUNC_MEMBEROF
        #undef TC_FNC_BODY
        #include "AllTCFunctions.h"
    };
}}