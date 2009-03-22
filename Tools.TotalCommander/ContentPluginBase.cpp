//#include "stdafx.h"
#include "ContentPluginBase.h"
#include "ContentPluginBase helpers.h"
#include "Exceptions.h"
#include "Common.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace Tools::TotalCommanderT::ResourcesT;
using namespace Tools::ExtensionsT;

namespace Tools{namespace TotalCommanderT{
    inline ContentPluginBase::ContentPluginBase(){StreamCache = gcnew System::Collections::Generic::Dictionary<String^,IO::Stream^>();}
    int ContentPluginBase::ContentGetSupportedField(int FieldIndex,char* FieldName, char* Units,int maxlen){
        this->fieldNameMaxLen = maxlen-1;
        cli::array<ContentFieldSpecification^>^ fields = SupportedFields;
        if(fields == nullptr) return (int)ContentFieldType::NoMoreFields;
        if(FieldIndex < 0 || FieldIndex >= fields->Length) return (int)ContentFieldType::NoMoreFields;
        ContentFieldSpecification^ field = fields[FieldIndex];
        if(field->FieldIndex != FieldIndex) throw gcnew InvalidOperationException(ResourcesT::Exceptions::InvalidFieldIndexFormat("SupportedFields",FieldIndex,field->FieldIndex));
        if(field->FieldName->Length > maxlen-1) throw gcnew InvalidOperationException(ResourcesT::Exceptions::FieldNameTooLongFormat("SupportedFields","FieldNameMaxLen"));
        String^ units;
        if(field->Units == nullptr) units = "";
        else if(field->Units->Length == 0) units = "";
        else{ 
            int i=0;
            for each(String^ unit in field->Units)
                if(unit->Contains(".") || unit->Contains("|") || unit->Contains(":") || unit->Contains(gcnew String((Char)0,1)))
                    throw gcnew InvalidOperationException(ResourcesT::Exceptions::InvalidUnitNameCharacter);
            units = String::Join("|",field->Units);
            if(units->Length > maxlen-1) throw gcnew InvalidOperationException(ResourcesT::Exceptions::UnitNamesTooLongFormat("SupportedFields","FieldNameMaxLen"));
        }
        StringCopy(field->FieldName,FieldName,maxlen);
        StringCopy(units,Units,maxlen);
        return (int)field->FieldType;
    }
    inline int ContentPluginBase::FieldNameMaxLen::get(){return fieldNameMaxLen;}

    inline cli::array<ContentFieldSpecification^>^ ContentPluginBase::SupportedFields::get(){ throw gcnew NotSupportedException();}

    int ContentPluginBase::ContentGetValue(char* FileName,int FieldIndex,int UnitIndex, void* FieldValue,int maxlen,int flags){
        //Prepare
        String^ fileName = gcnew String(FileName);
        String^ key = fileName + "|" + FieldIndex.ToString(System::Globalization::CultureInfo::InvariantCulture);
        Nullable<Double> original;
        if((((GetFieldValueFlags) flags) & GetFieldValueFlags::PassThrough) == GetFieldValueFlags::PassThrough)
            original = *(Double*)FieldValue;
        //Check stream
        if(StreamCache->ContainsKey(key)) return GetCachedStreamData(key,UnitIndex,(Byte*)FieldValue, maxlen); 
        //Get the value
        Object^ result;
        try{
            result = GetValue(fileName, FieldIndex, UnitIndex, maxlen-1, (GetFieldValueFlags)flags, original);
        }catch(ArgumentOutOfRangeException^){
            return (int)GetContentFieldValueReturnCode::NoSuchField;
        }catch(IO::IOException^){
            return (int)GetContentFieldValueReturnCode::FileError;
        }
        //Interpret the value
        if(result == nullptr){
            return (int)GetContentFieldValueReturnCode::FieldEmpty;
        }else if(Int32::typeid->IsAssignableFrom(result->GetType())){
            *(Int32*)FieldValue = (Int32)result;
            return (int)ContentFieldType::Integer32;
        }else if(Int64::typeid->IsAssignableFrom(result->GetType())){
            *(Int64*)FieldValue = (Int64)result;
            return (int)ContentFieldType::Integer64;
        }else if(Double::typeid->IsAssignableFrom(result->GetType())){
            *(Double*)FieldValue = (Double)result;
            return (int)ContentFieldType::Double;
        }else if(Tools::DataStructuresT::GenericT::IPair<Double,String^>::typeid->IsAssignableFrom(result->GetType())){
            *(Double*)FieldValue = ((Tools::DataStructuresT::GenericT::IPair<Double,String^>^)result)->Value1;
            StringCopy(((Tools::DataStructuresT::GenericT::IPair<Double,String^>^)result)->Value2,(char*)(void*)((int)FieldValue + 4),maxlen-4);
            return (int)ContentFieldType::Double;
        }else if(Date::typeid->IsAssignableFrom(result->GetType())){
            ((Date)result).Populate((pdateformat)FieldValue);
            return (int)ContentFieldType::Date;
        }else if(TimeSpan::typeid->IsAssignableFrom(result->GetType())){
            PopulateWith((ptimeformat)FieldValue,(TimeSpan)result);
            return (int)ContentFieldType::Time;
        }else if(Tools::TimeSpanFormattable::typeid->IsAssignableFrom(result->GetType())){
            PopulateWith((ptimeformat)FieldValue,(TimeSpan)(TimeSpanFormattable)result);
            return (int)ContentFieldType::Time;            
        }else if(Boolean::typeid->IsAssignableFrom(result->GetType())){
            *(Boolean*)FieldValue = (Boolean)result;
            return (int)ContentFieldType::Boolean;
        }else if(cli::array<String^>::typeid->IsAssignableFrom(result->GetType())){
            cli::array<String^>^ sarr = (cli::array<String^>^)result;
            if(sarr->Length == 0) return (int)GetContentFieldValueReturnCode::FieldEmpty;
            String^ StrValue = String::Join(", ",sarr);
            if(StrValue->Length > maxlen - 1) throw gcnew InvalidOperationException(ResourcesT::Exceptions::ReturnedStringTooLongForChoiceFormat("GetValue", "Enum"));
            StringCopy(StrValue,(char*)FieldValue,maxlen);
            return (int)ContentFieldType::Enum;
        }else if(String::typeid->IsAssignableFrom(result->GetType())){
            StringCopy((String^)result,(char*)FieldValue,maxlen);
            return (int)ContentFieldType::String;
        }else if(cli::array<Byte>::typeid->IsAssignableFrom(result->GetType())){
            cli::array<Byte>^ barr = (cli::array<Byte>^) result;
            if(barr->Length == 0) return (int)GetContentFieldValueReturnCode::FieldEmpty;
            if(barr->Length > maxlen-1) throw gcnew InvalidOperationException(ResourcesT::Exceptions::ReturnedArrayToLongFormat("GetValue"));
            for(int i = 0; i<barr->Length; i++) ((Byte*)FieldValue)[i]=barr[i];
            return (int)ContentFieldType::FullText;
        }else if(IO::Stream::typeid->IsAssignableFrom(result->GetType())){
            StreamCache->Add(key, (IO::Stream^)result);
            return GetCachedStreamData(key,UnitIndex,(Byte*)FieldValue, maxlen);
        }else if(DateTime::typeid->IsAssignableFrom(result->GetType())){
            *(::FILETIME*)FieldValue = DateTimeToFileTime((DateTime)result);
            return (int)ContentFieldType::DateAndTime;
        }else if(GetContentFieldValueReturnCode::typeid->IsAssignableFrom(result->GetType())){
            ((Byte*)FieldValue)[0]=0;
            return (int)(GetContentFieldValueReturnCode)result;
        }else{
            throw gcnew Tools::TypeMismatchException(ResourcesT::Exceptions::UnexpectedTypeFormat(result->GetType()->Name,"GetValue"),result);
        }
    }
    int ContentPluginBase::GetCachedStreamData(String^ StreamKey, int offset, Byte* FieldValue, int maxlen){
        if(!StreamCache->ContainsKey(StreamKey)) return (int)GetContentFieldValueReturnCode::NoSuchField;
        IO::Stream^ stream = StreamCache[StreamKey];
        if(offset < 0){
            StreamCache->Remove(StreamKey);
            FieldValue[0]=0;
            return (int)ContentFieldType::FullText;
        }else if(offset >= stream->Length){
            StreamCache->Remove(StreamKey);
            return (int)GetContentFieldValueReturnCode::FieldEmpty;
        }else{
            stream->Position = offset;
            cli::array<Byte>^ buffer = gcnew array<Byte>(maxlen-1);
            int bread = 0;
            int nread = 0;
            do{
                bread += nread = stream->Read(buffer,bread,maxlen-1-bread);
            }while(nread>0);
            for(int i=0;i<bread;i++) FieldValue[i]=buffer[i];
            buffer[bread]=0;
            return (int)ContentFieldType::FullText;
        }
    }
    inline Object^ ContentPluginBase::GetValue(String^ FileName, int FieldIndex, int UnitIndex, int maxlen, GetFieldValueFlags flags, Nullable<Double> FieldValueOriginal){throw gcnew NotSupportedException();}
    inline void ContentPluginBase::ContentStopGetValue(char* FileName){
        StopGetValue(gcnew String(FileName));
    }
    inline void ContentPluginBase::StopGetValue(String^ FileName){throw gcnew NotSupportedException();}
    int ContentPluginBase::ContentGetDefaultSortOrder(int FieldIndex){
        try{
            switch(GetDefaultSortOrder(FieldIndex)){
                case Windows::Forms::SortOrder::Descending:
                    return -1;
                default: return 1;
            }
        }catch(ArgumentOutOfRangeException^){return 1;}
    }
    inline Windows::Forms::SortOrder ContentPluginBase::GetDefaultSortOrder(int FieldIndex){throw gcnew NotSupportedException();}
    inline void ContentPluginBase::ContentPluginUnloading(){OnContentPluginUnloading();}
    inline void ContentPluginBase::OnContentPluginUnloading(){}
    int ContentPluginBase::ContentGetSupportedFieldFlags(int FieldIndex){
        try{
            return (int)GetSupportedFieldFlags(FieldIndex);
        }catch(ArgumentOutOfRangeException^){ return 0;}
    }
    FieldFlags ContentPluginBase::GetSupportedFieldFlags(int FieldIndex){
        if(FieldIndex < 0){
            bool Subst=false;
            bool Edit=false;
            for each(ContentFieldSpecification^ col in SupportedFields){
                if((col->Flags & FieldFlags::FieldEdit) == FieldFlags::FieldEdit) Edit = true;
                if((col->Flags & FieldFlags::SubstMask) != (FieldFlags)0) Subst = true;
            }
            return (Subst ? FieldFlags::SubstMask : (FieldFlags)0) | (Edit ? FieldFlags::FieldEdit : (FieldFlags)0);
        }else if(FieldIndex < SupportedFields->Length)
            return SupportedFields[FieldIndex]->Flags;
        else throw gcnew ArgumentOutOfRangeException("FieldIndex");
    }
    int ContentPluginBase::ContentSetValue(char* FileName,int FieldIndex,int UnitIndex,int FieldType, void* FieldValue,int flags){
        if(FileName == NULL){
            try{
                SetValue(nullptr,FieldIndex, UnitIndex,nullptr,(SetValueFlags)flags);
                return ContentSetValueSuccess;
            }catch(IO::IOException^) {return (int)GetContentFieldValueReturnCode::FileError;}
             catch(ArgumentOutOfRangeException^){return (int)GetContentFieldValueReturnCode::NoSuchField;}
        }else{
            Object^ value;
            switch(FieldType){
                case ContentFieldType::Boolean: value = *(Boolean*)FieldValue; break;
                case ContentFieldType::Date: value = Date((pdateformat)FieldValue); break;
                case ContentFieldType::DateAndTime: value = FileTimeToDateTime(*(::FILETIME*)FieldValue); break;
                case ContentFieldType::Double: value = *(Double*)FieldValue; break;
                case ContentFieldType::Enum:{ 
                    array<String^>^ comma = gcnew array<String^>(1);
                    comma[0] = ", ";
                    value = (gcnew String((char*)FieldValue))->Split(comma,StringSplitOptions::None);
                }break;
                case ContentFieldType::FullText:{ 
                    int i;
                    System::Collections::Generic::List<Byte>^ buff = gcnew System::Collections::Generic::List<Byte>();
                    for(i=0;((Byte*)FieldValue)[i]!=0;i++) buff->Add(((Byte*)FieldValue)[i]);
                    value = buff->ToArray();
                }break;
                case ContentFieldType::Integer32: value = *(Int32*)FieldValue; break;
                case ContentFieldType::Integer64: value = *(Int64*)FieldValue; break;
                case ContentFieldType::String: value = gcnew String((char*)FieldValue);
                case ContentFieldType::Time: value = TimeToTimeSpan((ptimeformat)FieldValue);
                default: value = (IntPtr)FieldValue;
            }
            try{
                SetValue(gcnew String(FileName),FieldIndex, UnitIndex, value, (SetValueFlags)flags);
                return ContentSetValueSuccess;
            }catch(IO::IOException^) {return (int)GetContentFieldValueReturnCode::FileError;}
             catch(ArgumentOutOfRangeException^){return (int)GetContentFieldValueReturnCode::NoSuchField;}
        }
    }
    inline void ContentPluginBase::SetValue(String^ FileName, int FieldIndex, int UnitIndex, Object^ value, SetValueFlags flags){throw gcnew NotSupportedException();}
}}