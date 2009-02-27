#include "Helper.h"
#include "define.h"
#include "TCHeaders.h"
#include "AssemblyResolver.h"
using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::Remoting;

namespace Tools{namespace TotalCommanderT{
    extern bool RequireInitialize;
    extern gcroot<AppDomainHolder^> holder;
    PluginInstanceHolder::PluginInstanceHolder(){
        this->instance = TC_WFX;
    }
    AppDomainHolder::AppDomainHolder(){
        this->holder = gcnew PluginInstanceHolder();
    }
    void Initialize(){
        if(!RequireInitialize) return;
        RequireInitialize = false;
        PluginSelfAssemblyResolver::Setup();
        AppDomainSetup^ setup = gcnew AppDomainSetup();
        Assembly^ currentAssembly = Assembly::GetExecutingAssembly();
        setup->ApplicationBase = IO::Path::GetDirectoryName(currentAssembly->Location);
        AppDomain^ pluginDomain = AppDomain::CreateDomain(PLUGIN_NAME,nullptr,setup);
        AppDomainHolder^ iholder = (AppDomainHolder^)pluginDomain->CreateInstanceFromAndUnwrap(currentAssembly->CodeBase,AppDomainHolder::typeid->FullName);
        Tools::TotalCommanderT::holder = iholder;
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