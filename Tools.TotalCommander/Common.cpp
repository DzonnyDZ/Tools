//#include "Stdafx.h"
#include "Common.h"
#include <vcclr.h>
#include "Exceptions.h"
#include "PluginMethodAttribute.h"

using namespace ::System;
using namespace ::System::Text::RegularExpressions;
using namespace ::Tools::TotalCommanderT::ResourcesT;
using namespace Tools::ExtensionsT;

namespace Tools{namespace TotalCommanderT{

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
#pragma region StringCopy
    void StringCopy(String^ source, char* target, int maxlen){
        if(maxlen <= 0) return;
        if(source == nullptr) target[0]=0;
        else{
            System::Text::Encoding^ enc = System::Text::Encoding::Default;
            cli::array<unsigned char>^ bytes = enc->GetBytes(source);
            for(int i = 0; i < bytes->Length && i < maxlen-1; i++)
                target[i]= bytes[i];
            target[source->Length > maxlen-1 ? maxlen-1 : source->Length] = 0;
        }
    }
    void StringCopy(String^ source, wchar_t* target, int maxlen){
        if(maxlen <= 0) return;
        if(source == nullptr) target[0] = 0;
        else{
            pin_ptr<const wchar_t> str = PtrToStringChars(source);
            for(int i = 0; i < maxlen - 1 && i < source->Length; i++)
                target[i] = str[i];
            target[Math::Min(maxlen - 1, source->Length)] = 0;
        }
    }
    
    String^ StringCopy(const wchar_t* source){
        return source == NULL ? nullptr : gcnew String(source);
    }
    String^ StringCopy(const char* source){
        return source == NULL ? nullptr : gcnew String(source);
    }

    wchar_t* StringCopyW(String^ source){
        if(source == nullptr) return NULL;
        pin_ptr<const wchar_t> str = PtrToStringChars(source);
        wchar_t* ret = new wchar_t[source->Length + 1];
        for(int i = 0; i < source->Length; i++)
            ret[i] = str[i];
        ret[source->Length] = 0;
        return ret;
    }

    char* StringCopyA(String^ source){
        if(source == nullptr) return NULL;
        System::Text::Encoding^ enc = System::Text::Encoding::Default;
        cli::array<unsigned char>^ bytes =  enc->GetBytes(source);
        char* ret = new char[bytes->Length + 1];
        ret[bytes->Length] = 0;
        for(int i = 0; i < bytes->Length; i++)
            ret[i] = bytes[i];
        return ret;
    }

    char* __clrcall UnicodeToAnsi(const wchar_t* source){
        return source == NULL ? NULL : StringCopyA(gcnew String(source));
    }
    wchar_t* __clrcall AnsiToUnicode(const char* source){
        return source == NULL ? NULL : StringCopyW(gcnew String(source));
    }

    char* __clrcall UnicodeToAnsi(const wchar_t* source, int maxlen){
        if(maxlen <= 0) return NULL;
        char* ret = new char[maxlen];
        if(source == NULL) ret[0] = 0;
        else StringCopy(gcnew String(source), ret, maxlen);
        return ret;
    }
    wchar_t* __clrcall AnsiToUnicode(const char* source, int maxlen){
        if(maxlen <= 0) return NULL;
        wchar_t* ret = new wchar_t[maxlen];
        if(source == NULL) ret[0] = 0;
        else StringCopy(gcnew String(source), ret, maxlen);
        return ret;
    }

    void __clrcall UnicodeToAnsi(const wchar_t* source, char* target, int maxlen){
        if(maxlen = 0) return;
        if(source == NULL) target[0] = 0;
        else StringCopy(gcnew String(source), target, maxlen);
    }
    void __clrcall AnsiToUnicode(const char* source, wchar_t* target, int maxlen){
        if(maxlen <= 0) return;
        if(source == NULL) target[0] = 0;
        else StringCopy(gcnew String(source), target, maxlen);
    }
#pragma endregion
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

    inline Object^ CallbackWrapperBase::Invoke(... cli::array<Object^>^ args){
        return this->Delegate->DynamicInvoke(args);
    }
}}