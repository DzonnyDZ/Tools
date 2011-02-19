#include "stdafx.h"
#include "EnumCore.h"

using namespace System;
using namespace Tools;
using namespace System::Collections::Generic;

EnumCore::EnumCore(){/*do nothing*/}

generic <class T> where T:Enum
bool EnumCore::HasFlagSet(T value, T flag){
    return value->HasFlag(flag);
}

generic <class T> where T:Enum, gcnew()
array<T>^ EnumCore::GetValues(){
    Array^ values = Enum::GetValues(T::typeid);
    array<T>^ ret = gcnew array<T>(values->Length);
    for(int i = 0; i < values->Length; i++)
        ret[i] = (T)values->GetValue(i);
    return ret;
}

generic <class TKey,class TValue> where TValue:Enum
bool EnumCore::HasFlagSet(IDictionary<TKey, TValue>^ dic, TValue flag){
    if(dic == nullptr) return false;
    for each(KeyValuePair<TKey, TValue> item in dic)
        if(item.Value->HasFlag(flag)) return true;
    return false;
}

generic <class T> where T:Enum, gcnew()
T EnumCore::Parse(String^ value){
    return (T)Enum::Parse(T::typeid, value);
}

generic <class T> where T:Enum, gcnew()
T EnumCore::Parse(String^ value, bool ignoreCase){
    return (T)Enum::Parse(T::typeid, value, ignoreCase);
}

generic <class T> where T:Enum, value class
bool EnumCore::TryParse(String^ value, T% result){
    return Enum::TryParse<T>(value, result);
}

generic <class T> where T:Enum, value class
bool EnumCore::TryParse(String^ value, bool ignoreCase, T% result){
    return Enum::TryParse<T>(value, ignoreCase, result);
}