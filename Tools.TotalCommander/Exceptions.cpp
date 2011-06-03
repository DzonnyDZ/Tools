
#include "Exceptions.h"
namespace Tools {
    namespace TotalCommanderT {
        namespace ResourcesT {
    
    
    inline Exceptions::Exceptions() {
    }
    
    inline ::System::Object^  Exceptions::InternalSyncObject::get() {
        if (System::Object::ReferenceEquals(_internalSyncObject, nullptr)) {
            ::System::Threading::Interlocked::CompareExchange(_internalSyncObject, (gcnew ::System::Object()), nullptr);
        }
        return _internalSyncObject;
    }
    
    inline ::System::Resources::ResourceManager^  Exceptions::ResourceManager::get() {
        if (System::Object::ReferenceEquals(_resourceManager, nullptr)) {
            ::System::Threading::Monitor::Enter(InternalSyncObject);
            try {
                if (System::Object::ReferenceEquals(_resourceManager, nullptr)) {
                    ::System::Threading::Interlocked::Exchange(_resourceManager, (gcnew ::System::Resources::ResourceManager(L"Tools.TotalCommanderT.Exceptions", 
                            Tools::TotalCommanderT::ResourcesT::Exceptions::typeid->Assembly)));
                }
            }
            finally {
                ::System::Threading::Monitor::Exit(InternalSyncObject);
            }
        }
        return _resourceManager;
    }
    
    inline ::System::Globalization::CultureInfo^  Exceptions::Culture::get() {
        return _resourceCulture;
    }
    inline System::Void Exceptions::Culture::set(::System::Globalization::CultureInfo^  value) {
        _resourceCulture = value;
    }
    
    inline System::String^  Exceptions::CannotBeRepresented::get() {
        return ResourceManager->GetString(L"CannotBeRepresented", _resourceCulture);
    }
    
    inline System::String^  Exceptions::CannotCompare::get() {
        return ResourceManager->GetString(L"CannotCompare", _resourceCulture);
    }
    
    inline System::String^  Exceptions::ColumnDefinitionStringTooLong::get() {
        return ResourceManager->GetString(L"ColumnDefinitionStringTooLong", _resourceCulture);
    }
    
    inline System::String^  Exceptions::CryptoAlreadyInitialized::get() {
        return ResourceManager->GetString(L"CryptoAlreadyInitialized", _resourceCulture);
    }
    
    inline System::String^  Exceptions::CryptoNotInitialized::get() {
        return ResourceManager->GetString(L"CryptoNotInitialized", _resourceCulture);
    }
    
    inline System::String^  Exceptions::DefaultTextTooLong::get() {
        return ResourceManager->GetString(L"DefaultTextTooLong", _resourceCulture);
    }
    
    inline System::String^  Exceptions::DerivedMehtodNotFound::get() {
        return ResourceManager->GetString(L"DerivedMehtodNotFound", _resourceCulture);
    }
    
    inline System::String^  Exceptions::EncryptDecryptFailed::get() {
        return ResourceManager->GetString(L"EncryptDecryptFailed", _resourceCulture);
    }
    
    inline System::String^  Exceptions::FieldNameTooLong::get() {
        return ResourceManager->GetString(L"FieldNameTooLong", _resourceCulture);
    }
    
    inline System::String^  Exceptions::GenericParameterNotFound::get() {
        return ResourceManager->GetString(L"GenericParameterNotFound", _resourceCulture);
    }
    
    inline System::String^  Exceptions::InvalidCharacter::get() {
        return ResourceManager->GetString(L"InvalidCharacter", _resourceCulture);
    }
    
    inline System::String^  Exceptions::InvalidFieldIndex::get() {
        return ResourceManager->GetString(L"InvalidFieldIndex", _resourceCulture);
    }
    
    inline System::String^  Exceptions::InvalidFieldNameCharacter::get() {
        return ResourceManager->GetString(L"InvalidFieldNameCharacter", _resourceCulture);
    }
    
    inline System::String^  Exceptions::InvalidFieldType::get() {
        return ResourceManager->GetString(L"InvalidFieldType", _resourceCulture);
    }
    
    inline System::String^  Exceptions::InvalidMacroName::get() {
        return ResourceManager->GetString(L"InvalidMacroName", _resourceCulture);
    }
    
    inline System::String^  Exceptions::InvalidPathFormat::get() {
        return ResourceManager->GetString(L"InvalidPathFormat", _resourceCulture);
    }
    
    inline System::String^  Exceptions::InvalidPluginNumberReinitialization::get() {
        return ResourceManager->GetString(L"InvalidPluginNumberReinitialization", _resourceCulture);
    }
    
    inline System::String^  Exceptions::InvalidUnitNameCharacter::get() {
        return ResourceManager->GetString(L"InvalidUnitNameCharacter", _resourceCulture);
    }
    
    inline System::String^  Exceptions::NameTooLong::get() {
        return ResourceManager->GetString(L"NameTooLong", _resourceCulture);
    }
    
    inline System::String^  Exceptions::NoMasterPassword::get() {
        return ResourceManager->GetString(L"NoMasterPassword", _resourceCulture);
    }
    
    inline System::String^  Exceptions::NotInitialized::get() {
        return ResourceManager->GetString(L"NotInitialized", _resourceCulture);
    }
    
    inline System::String^  Exceptions::ParamAssignedTooLong::get() {
        return ResourceManager->GetString(L"ParamAssignedTooLong", _resourceCulture);
    }
    
    inline System::String^  Exceptions::PathTooLong::get() {
        return ResourceManager->GetString(L"PathTooLong", _resourceCulture);
    }
    
    inline System::String^  Exceptions::PluginInitialized::get() {
        return ResourceManager->GetString(L"PluginInitialized", _resourceCulture);
    }
    
    inline System::String^  Exceptions::PluginInitializedAnsiUnicode::get() {
        return ResourceManager->GetString(L"PluginInitializedAnsiUnicode", _resourceCulture);
    }
    
    inline System::String^  Exceptions::PluginInitializedForTC::get() {
        return ResourceManager->GetString(L"PluginInitializedForTC", _resourceCulture);
    }
    
    inline System::String^  Exceptions::PluginInitializedNotForTC::get() {
        return ResourceManager->GetString(L"PluginInitializedNotForTC", _resourceCulture);
    }
    
    inline System::String^  Exceptions::PluginNotInitialized::get() {
        return ResourceManager->GetString(L"PluginNotInitialized", _resourceCulture);
    }
    
    inline System::String^  Exceptions::PluginNotRegistered::get() {
        return ResourceManager->GetString(L"PluginNotRegistered", _resourceCulture);
    }
    
    inline System::String^  Exceptions::PluginTypeNotSupported::get() {
        return ResourceManager->GetString(L"PluginTypeNotSupported", _resourceCulture);
    }
    
    inline System::String^  Exceptions::PluginWindowHandleMismatch::get() {
        return ResourceManager->GetString(L"PluginWindowHandleMismatch", _resourceCulture);
    }
    
    inline System::String^  Exceptions::PropertyIsReadOnly::get() {
        return ResourceManager->GetString(L"PropertyIsReadOnly", _resourceCulture);
    }
    
    inline System::String^  Exceptions::PropertyWasInitialized::get() {
        return ResourceManager->GetString(L"PropertyWasInitialized", _resourceCulture);
    }
    
    inline System::String^  Exceptions::PropertyWasNotInitialized::get() {
        return ResourceManager->GetString(L"PropertyWasNotInitialized", _resourceCulture);
    }
    
    inline System::String^  Exceptions::PropertyWasNull::get() {
        return ResourceManager->GetString(L"PropertyWasNull", _resourceCulture);
    }
    
    inline System::String^  Exceptions::ReadPasswordStoreFailed::get() {
        return ResourceManager->GetString(L"ReadPasswordStoreFailed", _resourceCulture);
    }
    
    inline System::String^  Exceptions::ReturnedArrayToLong::get() {
        return ResourceManager->GetString(L"ReturnedArrayToLong", _resourceCulture);
    }
    
    inline System::String^  Exceptions::ReturnedStringTooLongForChoice::get() {
        return ResourceManager->GetString(L"ReturnedStringTooLongForChoice", _resourceCulture);
    }
    
    inline System::String^  Exceptions::ToManyAttributes::get() {
        return ResourceManager->GetString(L"ToManyAttributes", _resourceCulture);
    }
    
    inline System::String^  Exceptions::TypeDoesNotDeriveFrom::get() {
        return ResourceManager->GetString(L"TypeDoesNotDeriveFrom", _resourceCulture);
    }
    
    inline System::String^  Exceptions::TypeIsNotGeneric::get() {
        return ResourceManager->GetString(L"TypeIsNotGeneric", _resourceCulture);
    }
    
    inline System::String^  Exceptions::TypeMustBeSpecifiedForGlobalMethods::get() {
        return ResourceManager->GetString(L"TypeMustBeSpecifiedForGlobalMethods", _resourceCulture);
    }
    
    inline System::String^  Exceptions::UnexpectedType::get() {
        return ResourceManager->GetString(L"UnexpectedType", _resourceCulture);
    }
    
    inline System::String^  Exceptions::UnitNamesTooLong::get() {
        return ResourceManager->GetString(L"UnitNamesTooLong", _resourceCulture);
    }
    
    inline System::String^  Exceptions::UnknownError::get() {
        return ResourceManager->GetString(L"UnknownError", _resourceCulture);
    }
    
    inline System::String^  Exceptions::ValueNotSupported::get() {
        return ResourceManager->GetString(L"ValueNotSupported", _resourceCulture);
    }
    
    inline System::String^  Exceptions::WritePasswordStoreFailed::get() {
        return ResourceManager->GetString(L"WritePasswordStoreFailed", _resourceCulture);
    }
    
    inline System::String^  Exceptions::CannotBeRepresentedFormat(System::Object^  arg0, System::Object^  arg1, System::Object^  arg2, 
                System::Object^  arg3) {
        return System::String::Format(_resourceCulture, CannotBeRepresented, arg0, arg1, arg2, arg3);
    }
    
    inline System::String^  Exceptions::CannotCompareFormat(System::Object^  arg0, System::Object^  arg1) {
        return System::String::Format(_resourceCulture, CannotCompare, arg0, arg1);
    }
    
    inline System::String^  Exceptions::ColumnDefinitionStringTooLongFormat(System::Object^  arg0) {
        return System::String::Format(_resourceCulture, ColumnDefinitionStringTooLong, arg0);
    }
    
    inline System::String^  Exceptions::CryptoAlreadyInitializedFormat() {
        return CryptoAlreadyInitialized;
    }
    
    inline System::String^  Exceptions::CryptoNotInitializedFormat() {
        return CryptoNotInitialized;
    }
    
    inline System::String^  Exceptions::DefaultTextTooLongFormat() {
        return DefaultTextTooLong;
    }
    
    inline System::String^  Exceptions::DerivedMehtodNotFoundFormat(System::Object^  arg0, System::Object^  arg1, System::Object^  arg2) {
        return System::String::Format(_resourceCulture, DerivedMehtodNotFound, arg0, arg1, arg2);
    }
    
    inline System::String^  Exceptions::EncryptDecryptFailedFormat() {
        return EncryptDecryptFailed;
    }
    
    inline System::String^  Exceptions::FieldNameTooLongFormat(System::Object^  arg0, System::Object^  arg1) {
        return System::String::Format(_resourceCulture, FieldNameTooLong, arg0, arg1);
    }
    
    inline System::String^  Exceptions::GenericParameterNotFoundFormat(System::Object^  arg0, System::Object^  arg1) {
        return System::String::Format(_resourceCulture, GenericParameterNotFound, arg0, arg1);
    }
    
    inline System::String^  Exceptions::InvalidCharacterFormat(System::Object^  arg0) {
        return System::String::Format(_resourceCulture, InvalidCharacter, arg0);
    }
    
    inline System::String^  Exceptions::InvalidFieldIndexFormat(System::Object^  arg0, System::Object^  arg1, System::Object^  arg2) {
        return System::String::Format(_resourceCulture, InvalidFieldIndex, arg0, arg1, arg2);
    }
    
    inline System::String^  Exceptions::InvalidFieldNameCharacterFormat() {
        return InvalidFieldNameCharacter;
    }
    
    inline System::String^  Exceptions::InvalidFieldTypeFormat(System::Object^  arg0) {
        return System::String::Format(_resourceCulture, InvalidFieldType, arg0);
    }
    
    inline System::String^  Exceptions::InvalidMacroNameFormat(System::Object^  arg0) {
        return System::String::Format(_resourceCulture, InvalidMacroName, arg0);
    }
    
    inline System::String^  Exceptions::InvalidPathFormatFormat(System::Object^  arg0) {
        return System::String::Format(_resourceCulture, InvalidPathFormat, arg0);
    }
    
    inline System::String^  Exceptions::InvalidPluginNumberReinitializationFormat(System::Object^  arg0, System::Object^  arg1) {
        return System::String::Format(_resourceCulture, InvalidPluginNumberReinitialization, arg0, arg1);
    }
    
    inline System::String^  Exceptions::InvalidUnitNameCharacterFormat() {
        return InvalidUnitNameCharacter;
    }
    
    inline System::String^  Exceptions::NameTooLongFormat(System::Object^  arg0) {
        return System::String::Format(_resourceCulture, NameTooLong, arg0);
    }
    
    inline System::String^  Exceptions::NoMasterPasswordFormat() {
        return NoMasterPassword;
    }
    
    inline System::String^  Exceptions::NotInitializedFormat() {
        return NotInitialized;
    }
    
    inline System::String^  Exceptions::ParamAssignedTooLongFormat(System::Object^  arg0, System::Object^  arg1) {
        return System::String::Format(_resourceCulture, ParamAssignedTooLong, arg0, arg1);
    }
    
    inline System::String^  Exceptions::PathTooLongFormat() {
        return PathTooLong;
    }
    
    inline System::String^  Exceptions::PluginInitializedFormat() {
        return PluginInitialized;
    }
    
    inline System::String^  Exceptions::PluginInitializedAnsiUnicodeFormat() {
        return PluginInitializedAnsiUnicode;
    }
    
    inline System::String^  Exceptions::PluginInitializedForTCFormat() {
        return PluginInitializedForTC;
    }
    
    inline System::String^  Exceptions::PluginInitializedNotForTCFormat() {
        return PluginInitializedNotForTC;
    }
    
    inline System::String^  Exceptions::PluginNotInitializedFormat() {
        return PluginNotInitialized;
    }
    
    inline System::String^  Exceptions::PluginNotRegisteredFormat(System::Object^  arg0) {
        return System::String::Format(_resourceCulture, PluginNotRegistered, arg0);
    }
    
    inline System::String^  Exceptions::PluginTypeNotSupportedFormat(System::Object^  arg0) {
        return System::String::Format(_resourceCulture, PluginTypeNotSupported, arg0);
    }
    
    inline System::String^  Exceptions::PluginWindowHandleMismatchFormat() {
        return PluginWindowHandleMismatch;
    }
    
    inline System::String^  Exceptions::PropertyIsReadOnlyFormat(System::Object^  arg0, System::Object^  arg1) {
        return System::String::Format(_resourceCulture, PropertyIsReadOnly, arg0, arg1);
    }
    
    inline System::String^  Exceptions::PropertyWasInitializedFormat(System::Object^  arg0) {
        return System::String::Format(_resourceCulture, PropertyWasInitialized, arg0);
    }
    
    inline System::String^  Exceptions::PropertyWasNotInitializedFormat(System::Object^  arg0) {
        return System::String::Format(_resourceCulture, PropertyWasNotInitialized, arg0);
    }
    
    inline System::String^  Exceptions::PropertyWasNullFormat(System::Object^  arg0) {
        return System::String::Format(_resourceCulture, PropertyWasNull, arg0);
    }
    
    inline System::String^  Exceptions::ReadPasswordStoreFailedFormat() {
        return ReadPasswordStoreFailed;
    }
    
    inline System::String^  Exceptions::ReturnedArrayToLongFormat(System::Object^  arg0) {
        return System::String::Format(_resourceCulture, ReturnedArrayToLong, arg0);
    }
    
    inline System::String^  Exceptions::ReturnedStringTooLongForChoiceFormat(System::Object^  arg0, System::Object^  arg1) {
        return System::String::Format(_resourceCulture, ReturnedStringTooLongForChoice, arg0, arg1);
    }
    
    inline System::String^  Exceptions::ToManyAttributesFormat(System::Object^  arg0, System::Object^  arg1) {
        return System::String::Format(_resourceCulture, ToManyAttributes, arg0, arg1);
    }
    
    inline System::String^  Exceptions::TypeDoesNotDeriveFromFormat(System::Object^  arg0, System::Object^  arg1) {
        return System::String::Format(_resourceCulture, TypeDoesNotDeriveFrom, arg0, arg1);
    }
    
    inline System::String^  Exceptions::TypeIsNotGenericFormat(System::Object^  arg0) {
        return System::String::Format(_resourceCulture, TypeIsNotGeneric, arg0);
    }
    
    inline System::String^  Exceptions::TypeMustBeSpecifiedForGlobalMethodsFormat() {
        return TypeMustBeSpecifiedForGlobalMethods;
    }
    
    inline System::String^  Exceptions::UnexpectedTypeFormat(System::Object^  arg0, System::Object^  arg1) {
        return System::String::Format(_resourceCulture, UnexpectedType, arg0, arg1);
    }
    
    inline System::String^  Exceptions::UnitNamesTooLongFormat(System::Object^  arg0, System::Object^  arg1) {
        return System::String::Format(_resourceCulture, UnitNamesTooLong, arg0, arg1);
    }
    
    inline System::String^  Exceptions::UnknownErrorFormat() {
        return UnknownError;
    }
    
    inline System::String^  Exceptions::ValueNotSupportedFormat(System::Object^  arg0) {
        return System::String::Format(_resourceCulture, ValueNotSupported, arg0);
    }
    
    inline System::String^  Exceptions::WritePasswordStoreFailedFormat() {
        return WritePasswordStoreFailed;
    }
        }
    }
}
 