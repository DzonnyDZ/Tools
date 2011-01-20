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