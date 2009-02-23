#include "AssemblyResolver.h"

using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::InteropServices;

namespace Tools{namespace TotalCommanderT{
    Assembly^ AssemblyResolver::OnResolveAssembly(Object^ sender, ResolveEventArgs^ args){
        Assembly^ thisAssembly = Assembly::GetExecutingAssembly();
        String^ thisPath = thisAssembly->Location;
        String^ directory = IO::Path::GetDirectoryName(thisPath);
        AssemblyName^ name = gcnew AssemblyName(args->Name);
        Assembly^ newAssembly;
        if(TryLoadAssembly(IO::Path::Combine(directory, name->Name + ".dll"),name,newAssembly))
            return newAssembly;
        if(TryLoadAssembly(IO::Path::Combine(directory, name->Name + ".exe"),name,newAssembly))
            return newAssembly;
        return nullptr;
    }

    bool AssemblyResolver::TryLoadAssembly(String^ path, AssemblyName^ name, [Out] Assembly^% assembly){
        if(!IO::File::Exists(path)) return false;
        AssemblyName^ afname;
        try{
            afname = AssemblyName::GetAssemblyName(path);
        }catch(ArgumentException^){return false;}
        catch(Security::SecurityException^){return false;}
        catch(BadImageFormatException^){return false;}
        catch(IO::FileLoadException^){return false;}

        if(!AssemblyName::ReferenceMatchesDefinition(name,afname)) return false;//This comparison is remomended by http://msdn.microsoft.com/en-us/library/system.reflection.assemblyname.referencematchesdefinition.aspx

        assembly = Assembly::LoadFile(path);
    }

    inline void AssemblyResolver::Setup(){
        AppDomain::CurrentDomain->AssemblyResolve += gcnew ResolveEventHandler( AssemblyResolver::OnResolveAssembly );
    }
}}