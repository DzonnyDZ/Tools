//#include "stdafx.h"
#include "ContentPluginBase.h"
#include "ContentFieldSpecification.h"
#include "Exceptions.h"
#include "Common.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace Tools::TotalCommanderT::ResourcesT;
using namespace Tools::ExtensionsT;

namespace Tools {
    namespace TotalCommanderT {
        inline ContentPluginBase::ContentPluginBase() { StreamCache = gcnew System::Collections::Generic::Dictionary<String^, IO::Stream^>(); }
        int ContentPluginBase::ContentGetSupportedField(int fieldIndex, char* fieldName, char* units, int maxlen) {
            this->fieldNameMaxLen = maxlen - 1;
            cli::array<ContentFieldSpecification^>^ fields = SupportedFields;
            if (fields == nullptr) return (int)ContentFieldType::NoMoreFields;
            if (fieldIndex < 0 || fieldIndex >= fields->Length) return (int)ContentFieldType::NoMoreFields;
            ContentFieldSpecification^ field = fields[fieldIndex];
            if (field->FieldIndex != fieldIndex) throw gcnew InvalidOperationException(ResourcesT::Exceptions::InvalidFieldIndexFormat("SupportedFields", fieldIndex, field->FieldIndex));
            if (field->FieldName->Length > maxlen - 1) throw gcnew InvalidOperationException(ResourcesT::Exceptions::FieldNameTooLongFormat("SupportedFields", "FieldNameMaxLen"));
            String^ us;
            if (field->Units == nullptr || field->Units->Length == 0)
                us = "";
            else {
                int i = 0;
                for each(String^ unit in field->Units)
                    if (unit->Contains(".") || unit->Contains("|") || unit->Contains(":") || unit->Contains(gcnew String((Char)0, 1)))
                        throw gcnew InvalidOperationException(ResourcesT::Exceptions::InvalidUnitNameCharacter);
                us = String::Join("|", field->Units);
                if (us->Length > maxlen - 1) throw gcnew InvalidOperationException(ResourcesT::Exceptions::UnitNamesTooLongFormat("SupportedFields", "FieldNameMaxLen"));
            }
            StringCopy(field->FieldName, fieldName, maxlen);
            StringCopy(us, units, maxlen);
            return (int)field->FieldType;
        }
        inline int ContentPluginBase::FieldNameMaxLen::get() { return fieldNameMaxLen; }

        inline cli::array<ContentFieldSpecification^>^ ContentPluginBase::SupportedFields::get() { throw gcnew NotSupportedException(); }

        //ContentGetValue
        int ContentPluginBase::ContentGetValue(wchar_t* fileName, int fieldIndex, int unitIndex, void* fieldValue, int maxlen, int flags, bool wide) {
            //Prepare
            String^ fn = gcnew String(fileName);
            String^ key = fn + "|" + fieldIndex.ToString(System::Globalization::CultureInfo::InvariantCulture);
            Nullable<Double> original;
            if ((((GetFieldValueFlags)flags) & GetFieldValueFlags::PassThrough) == GetFieldValueFlags::PassThrough)
                original = *(Double*)fieldValue;
            //Check stream
            if (StreamCache->ContainsKey(key)) return GetCachedStreamData(key, unitIndex, (Byte*)fieldValue, maxlen);
            //Get the value
            Object^ result;
            try {
                result = GetValue(fn, fieldIndex, unitIndex, maxlen - 1, (GetFieldValueFlags)flags, original);
            }
            catch (ArgumentOutOfRangeException^) {
                return (int)GetContentFieldValueReturnCode::NoSuchField;
            }
            catch (IO::IOException^) {
                return (int)GetContentFieldValueReturnCode::FileError;
            }
            //Interpret the value
            if (result == nullptr) { //null
                return (int)GetContentFieldValueReturnCode::FieldEmpty;
            }
            else if (Int32::typeid->IsAssignableFrom(result->GetType())) { //Int32
                *(Int32*)fieldValue = (Int32)result;
                return (int)ContentFieldType::Integer32;
            }
            else if (Int64::typeid->IsAssignableFrom(result->GetType())) { //In64
                *(Int64*)fieldValue = (Int64)result;
                return (int)ContentFieldType::Integer64;
            }
            else if (Double::typeid->IsAssignableFrom(result->GetType())) { //Double
                *(Double*)fieldValue = (Double)result;
                return (int)ContentFieldType::Double;
            }
            else if (Tools::DataStructuresT::GenericT::IPair<Double, String^>::typeid->IsAssignableFrom(result->GetType())) { //IPair<Double, String^>
                *(Double*)fieldValue = ((Tools::DataStructuresT::GenericT::IPair<Double, String^>^)result)->Value1;
                if (wide)
                    StringCopy(((Tools::DataStructuresT::GenericT::IPair<Double, String^>^)result)->Value2, (wchar_t*)(void*)((int)fieldValue + 4), maxlen - 4);
                else
                    StringCopy(((Tools::DataStructuresT::GenericT::IPair<Double, String^>^)result)->Value2, (char*)(void*)((int)fieldValue + 4), maxlen - 4);
                return (int)ContentFieldType::Double;
            }
            else if (Date::typeid->IsAssignableFrom(result->GetType())) { //Date
                ((Date)result).Populate((pdateformat)fieldValue);
                return (int)ContentFieldType::Date;
            }
            else if (TimeSpan::typeid->IsAssignableFrom(result->GetType())) { //TimeSpan
                PopulateWith((ptimeformat)fieldValue, (TimeSpan)result);
                return (int)ContentFieldType::Time;
            }
            else if (Tools::TimeSpanFormattable::typeid->IsAssignableFrom(result->GetType())) { //TimeSpanFormattable
                PopulateWith((ptimeformat)fieldValue, (TimeSpan)(TimeSpanFormattable)result);
                return (int)ContentFieldType::Time;
            }
            else if (Boolean::typeid->IsAssignableFrom(result->GetType())) { //Boolean
                *(Boolean*)fieldValue = (Boolean)result;
                return (int)ContentFieldType::Boolean;
            }
            else if (cli::array<String^>::typeid->IsAssignableFrom(result->GetType())) { //array<String^>
                cli::array<String^>^ sarr = (cli::array<String^>^)result;
                if (sarr->Length == 0) return (int)GetContentFieldValueReturnCode::FieldEmpty;
                String^ StrValue = String::Join(", ", sarr);
                if (StrValue->Length > maxlen - 1) throw gcnew InvalidOperationException(ResourcesT::Exceptions::ReturnedStringTooLongForChoiceFormat("GetValue", "Enum"));
                if (wide)
                    StringCopy(StrValue, (wchar_t*)fieldValue, maxlen);
                else
                    StringCopy(StrValue, (char*)fieldValue, maxlen);
                return (int)ContentFieldType::Enum;
            }
            else if (String::typeid->IsAssignableFrom(result->GetType())) { //String
                if (wide)
                    StringCopy((String^)result, (char*)fieldValue, maxlen);
                else
                    StringCopy((String^)result, (wchar_t*)fieldValue, maxlen);
                return (int)ContentFieldType::String;
            }
            else if (cli::array<Byte>::typeid->IsAssignableFrom(result->GetType())) { //Byte
                cli::array<Byte>^ barr = (cli::array<Byte>^) result;
                if (barr->Length == 0) return (int)GetContentFieldValueReturnCode::FieldEmpty;
                if (barr->Length > maxlen - 1) throw gcnew InvalidOperationException(ResourcesT::Exceptions::ReturnedArrayToLongFormat("GetValue"));
                for (int i = 0; i < barr->Length; i++) ((Byte*)fieldValue)[i] = barr[i];
                return (int)ContentFieldType::FullText;
            }
            else if (IO::Stream::typeid->IsAssignableFrom(result->GetType())) { //Stream
                StreamCache->Add(key, (IO::Stream^)result);
                return GetCachedStreamData(key, unitIndex, (Byte*)fieldValue, maxlen);
            }
            else if (DateTime::typeid->IsAssignableFrom(result->GetType())) { //DateTime
                *(::FILETIME*)fieldValue = DateTimeToFileTime((DateTime)result);
                return (int)ContentFieldType::DateAndTime;
            }
            else if (GetContentFieldValueReturnCode::typeid->IsAssignableFrom(result->GetType())) { //GetContentFieldValueReturnCode
                ((Byte*)fieldValue)[0] = 0;
                return (int)(GetContentFieldValueReturnCode)result;
            }
            else {
                throw gcnew Tools::TypeMismatchException(ResourcesT::Exceptions::UnexpectedTypeFormat(result->GetType()->Name, "GetValue"), result);
            }
        }
        int ContentPluginBase::GetCachedStreamData(String^ streamKey, int offset, Byte* fieldValue, int maxlen) {
            if (!StreamCache->ContainsKey(streamKey)) return (int)GetContentFieldValueReturnCode::NoSuchField;
            IO::Stream^ stream = StreamCache[streamKey];
            if (offset < 0) {
                StreamCache->Remove(streamKey);
                fieldValue[0] = 0;
                return (int)ContentFieldType::FullText;
            }
            else if (offset >= stream->Length) {
                StreamCache->Remove(streamKey);
                return (int)GetContentFieldValueReturnCode::FieldEmpty;
            }
            else {
                stream->Position = offset;
                cli::array<Byte>^ buffer = gcnew array<Byte>(maxlen - 1);
                int bread = 0;
                int nread = 0;
                do {
                    bread += nread = stream->Read(buffer, bread, maxlen - 1 - bread);
                } while (nread > 0);
                for (int i = 0; i < bread; i++)
                    fieldValue[i] = buffer[i];
                buffer[bread] = 0;
                return (int)ContentFieldType::FullText;
            }
        }
        inline Object^ ContentPluginBase::GetValue(String^, int, int, int, GetFieldValueFlags, Nullable<Double>) { throw gcnew NotSupportedException(); }

        inline void ContentPluginBase::ContentStopGetValue(wchar_t* fileName) {
            StopGetValue(gcnew String(fileName));
        }
        inline void ContentPluginBase::StopGetValue(String^) { throw gcnew NotSupportedException(); }

        int ContentPluginBase::ContentGetDefaultSortOrder(int fieldIndex) {
            try {
                switch (GetDefaultSortOrder(fieldIndex)) {
                case SortOrder::Descending:
                    return -1;
                default: return 1;
                }
            }
            catch (ArgumentOutOfRangeException^) { return 1; }
        }
        inline SortOrder ContentPluginBase::GetDefaultSortOrder(int) { throw gcnew NotSupportedException(); }
        inline void ContentPluginBase::ContentPluginUnloading() { OnContentPluginUnloading(); }
        inline void ContentPluginBase::OnContentPluginUnloading() {}
        int ContentPluginBase::ContentGetSupportedFieldFlags(int FieldIndex) {
            try {
                return (int)GetSupportedFieldFlags(FieldIndex);
            }
            catch (ArgumentOutOfRangeException^) { return 0; }
        }
        FieldFlags ContentPluginBase::GetSupportedFieldFlags(int FieldIndex) {
            if (FieldIndex < 0) {
                bool Subst = false;
                bool Edit = false;
                for each(ContentFieldSpecification^ col in SupportedFields) {
                    if ((col->Flags & FieldFlags::FieldEdit) == FieldFlags::FieldEdit) Edit = true;
                    if ((col->Flags & FieldFlags::SubstMask) != (FieldFlags)0) Subst = true;
                }
                return (Subst ? FieldFlags::SubstMask : (FieldFlags)0) | (Edit ? FieldFlags::FieldEdit : (FieldFlags)0);
            }
            else if (FieldIndex < SupportedFields->Length)
                return SupportedFields[FieldIndex]->Flags;
            else throw gcnew ArgumentOutOfRangeException("FieldIndex");
        }
        int ContentPluginBase::ContentSetValue(wchar_t* fileName, int fieldIndex, int unitIndex, int fieldType, void* fieldValue, int flags, bool wide) {
            if (fileName == NULL) {
                try {
                    SetValue(nullptr, fieldIndex, unitIndex, nullptr, (SetValueFlags)flags);
                    return ContentSetValueSuccess;
                }
                catch (IO::IOException^) { return (int)GetContentFieldValueReturnCode::FileError; }
                catch (ArgumentOutOfRangeException^) { return (int)GetContentFieldValueReturnCode::NoSuchField; }
            }
            else {
                Object^ value;
                switch (fieldType) {
                case ContentFieldType::Boolean: value = *(Boolean*)fieldValue; break;
                case ContentFieldType::Date: value = Date((pdateformat)fieldValue); break;
                case ContentFieldType::DateAndTime: value = FileTimeToDateTime(*(::FILETIME*)fieldValue); break;
                case ContentFieldType::Double: value = *(Double*)fieldValue; break;
                case ContentFieldType::Enum: {
                    array<String^>^ comma = gcnew array<String^>(1);
                    comma[0] = ", ";
                    if (wide)
                        value = (gcnew String((wchar_t*)fieldValue))->Split(comma, StringSplitOptions::None);
                    else
                        value = (gcnew String((char*)fieldValue))->Split(comma, StringSplitOptions::None);
                }break;
                case ContentFieldType::FullText: {
                    int i;
                    System::Collections::Generic::List<Byte>^ buff = gcnew System::Collections::Generic::List<Byte>();
                    for (i = 0; ((Byte*)fieldValue)[i] != 0; i++)
                        buff->Add(((Byte*)fieldValue)[i]);
                    value = buff->ToArray();
                }break;
                case ContentFieldType::Integer32: value = *(Int32*)fieldValue; break;
                case ContentFieldType::Integer64: value = *(Int64*)fieldValue; break;
                case ContentFieldType::String:
                    if (wide)
                        value = gcnew String((wchar_t*)fieldValue);
                    else
                        value = gcnew String((char*)fieldValue);
                case ContentFieldType::Time: value = TimeToTimeSpan((ptimeformat)fieldValue);
                default: value = (IntPtr)fieldValue;
                }
                try {
                    SetValue(gcnew String(fileName), fieldIndex, unitIndex, value, (SetValueFlags)flags);
                    return ContentSetValueSuccess;
                }
                catch (IO::IOException^) { return (int)GetContentFieldValueReturnCode::FileError; }
                catch (ArgumentOutOfRangeException^) { return (int)GetContentFieldValueReturnCode::NoSuchField; }
            }
        }
        inline void ContentPluginBase::SetValue(String^, int, int, Object^, SetValueFlags) { throw gcnew NotSupportedException(); }
    }
}