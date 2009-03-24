#include "Helper.h"
#include "define.h"
#include "TCHeaders.h"
#include "AssemblyResolver.h"
using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::Remoting;

#undef TC_FNC_HEADER
#undef TC_FNC_BODY
#undef TC_LINE_PREFIX
#undef TC_NAME_PREFIX
#undef TC_FUNC_MEMBEROF
#undef TC_FUNC_PREFIX_A
#undef TC_FUNC_PREFIX_B

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
    }
#define TC_FNC_HEADER
#define TC_FNC_BODY
#define TC_LINE_PREFIX
#define TC_NAME_PREFIX
#define TC_FUNC_MEMBEROF AppDomainHolder::
#define TC_FUNC_PREFIX_A
#define TC_FUNC_PREFIX_B
#define TC_FUNCTION_TARGET this->holder
#include "AllTCFunctions.h"

#undef TC_FUNC_MEMBEROF
#define TC_FUNC_MEMBEROF PluginInstanceHolder::
#undef TC_FUNCTION_TARGET
#define TC_FUNCTION_TARGET this->instance
#include "AllTCFunctions.h"
}}