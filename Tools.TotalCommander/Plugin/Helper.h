#include "define.h"
#include <vcclr.h>
#include "TCHeaders.h"
#pragma once
using namespace System;
using namespace Tools::TotalCommanderT;
using namespace System::Reflection;
using namespace System::Runtime::Remoting;

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
        #ifdef TC_WFX
            /// <summary>Holds instance of File System plugin (WFX)</summary>
            FileSystemPlugin^ instance;
        #endif//TODO:Other plugin types
        #include "FunctionPrototypes.h"
    };
    private ref struct AppDomainHolder : MarshalByRefObject {
        /// <summary>CTor</summary>
        AppDomainHolder();
        /// <summary>Holds instance of the <see cref="PluginInstanceHolder"/> class</summary>
        PluginInstanceHolder^ holder;
        #include "FunctionPrototypes.h"
    };
}}