//#include "Stdafx.h"
#include "Common.h"
#include <vcclr.h>
#include "Exceptions.h"

using namespace ::System;
using namespace ::System::Text::RegularExpressions;
using namespace ::Tools::TotalCommanderT::ResourcesT;

namespace Tools{namespace TotalCommanderT{
    inline MethodNotSupportedAttribute::MethodNotSupportedAttribute(){/*Do nothing*/}

    inline TotalCommanderPluginAttribute::TotalCommanderPluginAttribute(String^ Name){this->name = Name;}
    inline String^ TotalCommanderPluginAttribute::Name::get(){return this->name;}

    gcroot<Regex^> MacroRegex = gcnew Regex("^[A-Za-z_][A-Za-z_0-9]*$", RegexOptions::Compiled | RegexOptions::CultureInvariant);
    
    inline PluginMethodAttribute::PluginMethodAttribute(String^ DefinedBy){
        this->init(DefinedBy);    
    }
    void PluginMethodAttribute::init(String^ DefinedBy){
        if(DefinedBy == nullptr) throw gcnew ArgumentNullException("DefinedBy");
        if(!MacroRegex->IsMatch(DefinedBy)) throw gcnew FormatException(Exceptions::InvalidMacroNameFormat(DefinedBy));
        this->definedBy = DefinedBy;
    }
    PluginMethodAttribute::PluginMethodAttribute(String^ ImplementedBy, String^ DefinedBy){
        this->init(DefinedBy);
        this->implementedBy = ImplementedBy;
    }
    inline String^ PluginMethodAttribute::DefinedBy::get(){return this->definedBy;}
    inline String^ PluginMethodAttribute::ImplementedBy::get(){return this->implementedBy;}

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
        this->itemName = ItemName;
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
}}