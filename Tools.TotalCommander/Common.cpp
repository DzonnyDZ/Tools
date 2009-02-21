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
}}