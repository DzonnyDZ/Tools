//#include "stdafx.h"
#include "PluginMethodAttribute.h"
#include "Exceptions.h"
#include <vcclr.h>

using namespace ::System;
using namespace ::System::Text::RegularExpressions;
using namespace ::Tools::TotalCommanderT::ResourcesT;
using namespace Tools::ExtensionsT;

namespace Tools{namespace TotalCommanderT{

    //Do not use gcroot<> in this project because Mono.Cecil corrupts it (http://groups.google.com/group/mono-cecil/browse_thread/thread/69533d20197fed58)
    //gcroot<Regex^> MacroRegex = gcnew Regex("^[A-Za-z_][A-Za-z_0-9]*$", RegexOptions::Compiled | RegexOptions::CultureInvariant);
    
    inline PluginMethodAttribute::PluginMethodAttribute(String^ DefinedBy){
        if(DefinedBy == nullptr) throw gcnew ArgumentNullException("DefinedBy");
        if(!PluginMethodAttribute::macroRegex->IsMatch(DefinedBy)) throw gcnew FormatException(Exceptions::InvalidMacroNameFormat(DefinedBy));
        this->definedBy = DefinedBy;
    }
    PluginMethodAttribute::PluginMethodAttribute(String^ ImplementedBy, String^ DefinedBy){
        if(DefinedBy == nullptr) throw gcnew ArgumentNullException("DefinedBy");
        if(!PluginMethodAttribute::macroRegex->IsMatch(DefinedBy)) throw gcnew FormatException(Exceptions::InvalidMacroNameFormat(DefinedBy));
        this->definedBy = DefinedBy;
        this->implementedBy = ImplementedBy;
    }
    inline String^ PluginMethodAttribute::DefinedBy::get(){return this->definedBy;}
    inline String^ PluginMethodAttribute::ImplementedBy::get(){return this->implementedBy;}

    /*inline String^ PluginMethodAttribute::ExportedAs::get(){return this->exportedAs;}
    inline void PluginMethodAttribute::ExportedAs::set(String^ value){this->exportedAs = value;}
    String^ PluginMethodAttribute::GetExportedAs(PluginType pluginType, String^ functionName){
        if(functionName == nullptr) throw gcnew ArgumentNullException("functionName");
        if(ExportedAs == nullptr) return functionName;
        String^ prefix;
        switch(pluginType){
            case PluginType::FileSystem: prefix = "Fs"; break;
            case PluginType::Content: prefix = "Content"; break;
            case PluginType::Lister: prefix = "List"; break;
            case PluginType::Packer: prefix = ""; break;
            default: throw gcnew ComponentModel::InvalidEnumArgumentException("pluginType",(int)pluginType,pluginType.GetType());
        }
        String^ ret;
        if(ExportedAs->StartsWith("*")){
            ret = prefix + ExportedAs->Substring(1);
        }else if(ExportedAs->StartsWith("[*") && ExportedAs->Contains("]")){
            String^ part = ExportedAs->Substring(2,ExportedAs->IndexOf("]")-2);
            ret = (part == prefix ? part : (prefix + part)) + ExportedAs->Substring(ExportedAs->IndexOf("]")+1);
        }else if(ExportedAs->StartsWith("[") && ExportedAs->Contains("*]")){
            String^ part = ExportedAs->Substring(1,ExportedAs->IndexOf("*]")-1);
            ret = (part == prefix ? part : (part + prefix)) + ExportedAs->Substring(ExportedAs->IndexOf("*]")+2);
        }else{
            ret = ExportedAs;
        }
        return ret->Replace("#",functionName);
    }*/
}}