//#include "Stdafx.h"
#include "Common.h"
#include <vcclr.h>
#include "Exceptions.h"

using namespace ::System;
using namespace ::System::Text::RegularExpressions;
using namespace ::Tools::TotalCommanderT::ResourcesT;
using namespace Tools::ExtensionsT;

namespace Tools{namespace TotalCommanderT{
    inline MethodNotSupportedAttribute::MethodNotSupportedAttribute(){/*Do nothing*/}

    inline TotalCommanderPluginAttribute::TotalCommanderPluginAttribute(String^ Name){this->name = Name;}
    inline String^ TotalCommanderPluginAttribute::Name::get(){return this->name;}

    gcroot<Regex^> MacroRegex = gcnew Regex("^[A-Za-z_][A-Za-z_0-9]*$", RegexOptions::Compiled | RegexOptions::CultureInvariant);
    
    inline PluginMethodAttribute::PluginMethodAttribute(String^ DefinedBy){
        if(DefinedBy == nullptr) throw gcnew ArgumentNullException("DefinedBy");
        if(!MacroRegex->IsMatch(DefinedBy)) throw gcnew FormatException(Exceptions::InvalidMacroNameFormat(DefinedBy));
        this->definedBy = DefinedBy;
    }
    PluginMethodAttribute::PluginMethodAttribute(String^ ImplementedBy, String^ DefinedBy){
        if(DefinedBy == nullptr) throw gcnew ArgumentNullException("DefinedBy");
        if(!MacroRegex->IsMatch(DefinedBy)) throw gcnew FormatException(Exceptions::InvalidMacroNameFormat(DefinedBy));
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

    inline NotAPluginAttribute::NotAPluginAttribute(){/*do nothing*/}

#pragma region "Icons"
    PluginIconBaseAttribute::PluginIconBaseAttribute(){/*do nothing*/}
    inline FilePluginIconAttribute::FilePluginIconAttribute(String^ IconPath){this->iconPath = IconPath;}
    Drawing::Icon^ FilePluginIconAttribute::getIcon(Type^ AttributeTarget){
        if(IconPath == nullptr) throw gcnew InvalidOperationException(Exceptions::PropertyWasNullFormat("IconPath"));
        String^ RealPath;
        if(IO::Path::IsPathRooted(IconPath))
            RealPath = IconPath;
        else{
            if(AttributeTarget == nullptr) throw gcnew ArgumentNullException("AttributeTarget");
            try{
                RealPath = IO::Path::GetFullPath(IO::Path::Combine(IO::Path::GetDirectoryName(AttributeTarget->Assembly->Location),IconPath));
            }catch(Exception^ ex){throw gcnew InvalidOperationException(ex->Message,ex);}
        }
        try{
            return gcnew Drawing::Icon(RealPath);
        }catch(Exception^ ex){throw gcnew InvalidOperationException(ex->Message,ex);}
    }
    inline String^ FilePluginIconAttribute::IconPath::get(){ return this->iconPath;}
    ResourcePluginIconAttribute::ResourcePluginIconAttribute(Type^ TypeInAssembly, String^ ResourceName){
        if(TypeInAssembly == nullptr) throw gcnew ArgumentNullException("TypeInAssembly");
        this->assembly = TypeInAssembly->Assembly;
        this->resourceName = ResourceName;
    }
    ResourcePluginIconAttribute::ResourcePluginIconAttribute(Type^ TypeInAssembly, String^ ResourceName, String^ ResourceItem){
        if(TypeInAssembly == nullptr) throw gcnew ArgumentNullException("TypeInAssembly");
        this->assembly = TypeInAssembly->Assembly;
        this->resourceName = ResourceName;
        this->itemName = ResourceItem;
    }
    Drawing::Icon^ ResourcePluginIconAttribute::getIcon(Type^ AttributeTarget){
        try{
            if(ItemName == nullptr){
                return gcnew Drawing::Icon(Assembly->GetManifestResourceStream(ResourceName));
            }else{
                Resources::ResourceManager^ rm = gcnew Resources::ResourceManager(ResourceName,Assembly);
                return (Drawing::Icon^)rm->GetObject(ItemName);
            }
        }catch(Exception^ ex){
            throw gcnew InvalidOperationException(ex->Message,ex);
        }
    }
    inline Reflection::Assembly^ ResourcePluginIconAttribute::Assembly::get(){return this->assembly;}
    inline String^ ResourcePluginIconAttribute::ResourceName::get(){return this->resourceName;}
    inline String^ ResourcePluginIconAttribute::ItemName::get(){return this->itemName;}
#pragma endregion
    //Global functions
#pragma region "Global functions"
    Nullable<DateTime> FileTimeToDateTime(::FILETIME value){
        if(value.dwHighDateTime == 0xFFFFFFFF && value.dwLowDateTime == 0xFFFFFFFE) return Nullable<DateTime>();
        return DateTime(1601,1,1).AddTicks(*(__int64*)(void*)&value);
    }
    ::FILETIME DateTimeToFileTime(Nullable<DateTime> value){
        ::FILETIME ret;
        if(value.HasValue){
            ret.dwLowDateTime = Numbers::Low(value.Value.ToFileTime());
            ret.dwHighDateTime = Numbers::High(value.Value.ToFileTime());
        }else{
            ret.dwLowDateTime = 0xFFFFFFFE;
            ret.dwHighDateTime = 0xFFFFFFFF;
        }
        return ret;
    }
    void StringCopy(String^ source, char* target, int maxlen){
        /*if(source == nullptr)
            target[0]=0;
        else{
            for(int i = 0; i < source->Length && i < maxlen-1; i++)
                target[i]=source[i];
            target[source->Length > maxlen-1 ? maxlen-1 : source->Length] = 0;
        }*/
        if(source == nullptr)
            target[0]=0;
        else{
            System::Text::Encoding^ enc = System::Text::Encoding::Default;
            cli::array<unsigned char>^ bytes = enc->GetBytes(source);
            for(int i = 0; i < bytes->Length && i < maxlen-1; i++)
                target[i]= bytes[i];
            target[source->Length > maxlen-1 ? maxlen-1 : source->Length] = 0;
        }
    }
    void StringCopy(String^ source, wchar_t* target, int maxlen){
        StringCopy(source,(char*)(void*)target,maxlen);
    }
#pragma endregion

    void PopulateWith(ptimeformat target, TimeSpan source){
        if(target==NULL) throw gcnew ArgumentNullException("target");
        target->wHour = (WORD)Math::Floor(source.TotalHours);
        target->wMinute = source.Minutes;
        target->wSecond = source.Seconds;
    }
    TimeSpan TimeToTimeSpan(ptimeformat source){
        if(source==NULL) throw gcnew ArgumentNullException("source");
        return TimeSpan(source->wHour,source->wMinute,source->wSecond);
    }
}}