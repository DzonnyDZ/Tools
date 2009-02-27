#include "Helper.h"
#include "define.h"
#include "TCHeaders.h"
using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::Remoting;

namespace Tools{namespace TotalCommanderT{
    extern bool RequireInitialize;
    extern gcroot<AppDomainHolder^> holder;
    PluginInstanceHolder::PluginInstanceHolder(){
        instance = TC_WFX;
    }
    AppDomainHolder::AppDomainHolder(){
        holder = gcnew PluginInstanceHolder();
    }
    void Initialize(){
        if(!RequireInitialize) return;
        RequireInitialize = false;
        //AssemblyResolver::Setup();
        AppDomainSetup^ setup = gcnew AppDomainSetup();
        Assembly^ currentAssembly = Assembly::GetExecutingAssembly();
        setup->ApplicationBase = IO::Path::GetDirectoryName(currentAssembly->Location);
        AppDomain^ pluginDomain = AppDomain::CreateDomain(PLUGIN_NAME,nullptr,setup);
        ObjectHandle^ iholder = pluginDomain->CreateInstanceFrom(currentAssembly->CodeBase,AppDomainHolder::typeid->FullName);
        //holder = iholder;
        //InitializePlugin();
    }
#define TCPLUGF
#define FUNC_MODIF AppDomainHolder::
#define FUNCTION_TARGET this->holder
#include "FunctionCalls.h"
#undef FUNC_MODIF
#define FUNC_MODIF PluginInstanceHolder::
#undef FUNCTION_TARGET
#define FUNCTION_TARGET this->instance
#include "FunctionCalls.h"
}}