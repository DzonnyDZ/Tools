//#include "stdafx.h"
#include "PluginBase.h"
#include "Exceptions.h"
#include "PluginMethodAttribute.h"
#include "Attributes.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace Tools::TotalCommanderT::ResourcesT;
using namespace Tools::ExtensionsT;
using namespace Tools::ReflectionT;
using namespace System::Reflection;

namespace Tools{namespace TotalCommanderT{

    ref class FileSystemPlugin;

    inline PluginBase::PluginBase(){/*Do nothing*/}

     generic <class T> where T:Enum, gcnew()
     T PluginBase::GetSupportedFunctions(){
         int ret = 0;
         for each(FieldInfo^ constant in T::typeid->GetFields(BindingFlags::Public | BindingFlags::Static)){
            auto attr = TypeTools::GetAttribute<PluginMethodAttribute^>(constant, false);
            if(attr == nullptr) continue;
            if(attr->ImplementedBy == nullptr){
                ret |= ((IConvertible^)constant->GetValue(nullptr))->ToInt32(nullptr);
                continue;
            }
            const Reflection::BindingFlags flags = Reflection::BindingFlags::Instance | Reflection::BindingFlags::Public | Reflection::BindingFlags::NonPublic;
            bool implemented = MethodNotSupportedAttribute::Supported(ReflectionTools::GetOverridingMethod(this->PluginBaseClass->GetMethod(attr->ImplementedBy, flags), this->GetType()));
            if(implemented) ret |= ((IConvertible^)constant->GetValue(nullptr))->ToInt32(nullptr);
         }
         return (T)Enum::ToObject(T::typeid, ret);
     }

     inline void PluginBase::OnInit(){/*do nothing*/}

     inline Nullable<DefaultParams> PluginBase::PluginParams::get(){return this->pluginParams;}

     void PluginBase::SetDefaultParams(DefaultParams dps){
        if(this->PluginParams.HasValue) throw gcnew InvalidOperationException(ResourcesT::Exceptions::PropertyWasInitializedFormat("PluginParams"));
        this->pluginParams = dps;
    }
}}