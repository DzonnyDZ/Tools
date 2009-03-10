#include "AssemblyResolver.h"

using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::InteropServices;

namespace Tools{namespace TotalCommanderT{
    Assembly^ PluginSelfAssemblyResolver::OnResolveAssembly(Object^ sender, ResolveEventArgs^ args){
        Assembly^ thisAssembly = Assembly::GetExecutingAssembly();
        AssemblyName^ name = gcnew AssemblyName(args->Name);
        if(AssemblyName::ReferenceMatchesDefinition(name,thisAssembly->GetName())) return thisAssembly;
        else return nullptr;
    }
    inline void PluginSelfAssemblyResolver::Setup(){
        AppDomain::CurrentDomain->AssemblyResolve += gcnew ResolveEventHandler( PluginSelfAssemblyResolver::OnResolveAssembly );
    }
}}