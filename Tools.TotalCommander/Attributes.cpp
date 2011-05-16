#include "Attributes.h"

#include "Exceptions.h"
#include "PluginMethodAttribute.h"

using namespace ::System;
using namespace ::System::Text::RegularExpressions;
using namespace ::Tools::TotalCommanderT::ResourcesT;
using namespace Tools::ExtensionsT;

namespace Tools{namespace TotalCommanderT{

#pragma region MethodNotSupportedAttribute
    inline MethodNotSupportedAttribute::MethodNotSupportedAttribute(){/*Do nothing*/}
    inline bool MethodNotSupportedAttribute::IsSupported(Reflection::MethodInfo^){return true;}
    bool MethodNotSupportedAttribute::Supported(Reflection::MethodInfo^ method){
        if(method == nullptr) throw gcnew ArgumentNullException("method");
        auto mns = Tools::TypeTools::GetAttributes<MethodNotSupportedAttribute^>(method, false);
        if(mns == nullptr || mns->Length == 0) return true;
        if(mns->Length == 1) return mns[0]->IsSupported(method);
        throw gcnew InvalidOperationException(String::Format(ResourcesT::Exceptions::ToManyAttributes, MethodNotSupportedAttribute::typeid->Name, method->Name));
    }
#pragma endregion

#pragma region MethodNotSupportedRedirectAttribute
    MethodNotSupportedRedirectAttribute::MethodNotSupportedRedirectAttribute(String^ method):MethodNotSupportedAttribute(){
        if(method == nullptr) throw gcnew ArgumentNullException("method");
        this->method = method;
    }
    inline String^ MethodNotSupportedRedirectAttribute::Method::get(){return this->method;}
    inline bool MethodNotSupportedRedirectAttribute::IsSupported(System::Reflection::MethodInfo^ method){
        return MethodNotSupportedAttribute::Supported(this->GetTargetMethod(method));
    }

    System::Reflection::MethodInfo^ MethodNotSupportedRedirectAttribute::GetTargetMethod(Reflection::MethodInfo^ attributeMethod){
        if(attributeMethod == nullptr) throw gcnew ArgumentNullException("method");
        System::Type^ targetType;
        if(this->Type != nullptr) targetType = this->Type;
        else if (attributeMethod->DeclaringType == nullptr) throw gcnew InvalidOperationException(ResourcesT::Exceptions::TypeMustBeSpecifiedForGlobalMethods);
        else targetType = attributeMethod->DeclaringType;
        
        //Find generic parameter
        if(this->GPar != nullptr){
            if(!targetType->IsGenericType && !targetType->IsGenericTypeDefinition)
                throw gcnew InvalidOperationException(String::Format(ResourcesT::Exceptions::TypeIsNotGeneric, targetType->FullName));
            System::Type^ gpar;
            if(targetType->IsGenericTypeDefinition){
                for each(auto gp in targetType->GetGenericArguments())
                    if(gp->Name == this->GPar){
                        gpar = gp;
                        break;
                    }
            }else {//if(targetType->IsGenericType)
                for each(auto gp in targetType->GetGenericTypeDefinition()->GetGenericArguments())
                    if(gp->Name == this->GPar){
                        gpar = targetType->GetGenericArguments()[gp->GenericParameterPosition];
                        break;
                    }
            }
            if(gpar == nullptr) throw gcnew InvalidOperationException(String::Format(ResourcesT::Exceptions::GenericParameterNotFound, this->GPar, targetType->FullName));
            targetType = gpar;
        }

        System::Type^ hintType = this->Hint == nullptr ? targetType : this->Hint;
        //Find method
        System::Reflection::MethodInfo^ targetMethod;
        const auto methodFlags = System::Reflection::BindingFlags::Public | System::Reflection::BindingFlags::NonPublic | System::Reflection::BindingFlags::Instance | System::Reflection::BindingFlags::Static;
        if(this->MethodParameters == nullptr)
            targetMethod = hintType->GetMethod(this->Method, methodFlags);
        else
            try{
                targetMethod = hintType->GetMethod(this->Method, methodFlags, nullptr, this->MethodParameters, nullptr);
            }catch(ArgumentNullException^ ex){
                throw gcnew InvalidOperationException(ex->Message, ex);
            }
        if(targetMethod == nullptr) throw gcnew MissingMethodException(targetType->FullName, this->Method);
        
        if(this->Hint != nullptr){
            if(!this->Hint->IsAssignableFrom(targetType)) throw gcnew InvalidOperationException(String::Format(ResourcesT::Exceptions::TypeDoesNotDeriveFrom, targetType->FullName, this->Hint->FullName));
            if(this->Hint->IsInterface){
                auto map = targetType->GetInterfaceMap(this->Hint);
                targetMethod = map.TargetMethods[Array::IndexOf(map.InterfaceMethods, targetMethod)];
                if(!targetMethod->DeclaringType->Equals(targetType)){
                    if(targetMethod->IsVirtual){
                        targetMethod = Tools::ReflectionT::ReflectionTools::GetOverridingMethod(targetMethod, targetType);
                        if(targetMethod != nullptr && !targetMethod->DeclaringType->Equals(targetType)) targetMethod == nullptr;
                    }else
                        targetMethod = nullptr;
                }
            }else{
                targetMethod = Tools::ReflectionT::ReflectionTools::GetOverridingMethod(targetMethod, targetType);
                if(targetMethod != nullptr && !targetMethod->DeclaringType->Equals(targetType)) targetMethod == nullptr;
            }
            if(targetMethod == nullptr) throw gcnew MissingMethodException(String::Format(ResourcesT::Exceptions::DerivedMehtodNotFound, this->Hint->FullName, this->Method, targetType->FullName));
        }
    }
#pragma endregion

    inline TotalCommanderPluginAttribute::TotalCommanderPluginAttribute(String^ Name){this->name = Name;}
    inline String^ TotalCommanderPluginAttribute::Name::get(){return this->name;}

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
}}