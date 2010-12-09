#include "stdafx.h"
#include "EnumCore.h"

using namespace System;
using namespace Tools;

EnumCore::EnumCore(){/*do nothing*/}

generic <class T> where T:Enum
bool EnumCore::HasFlagSet(T value, T flag){
    return value->HasFlag(flag);
}

generic <class T> where T:Enum
array<T>^ EnumCore::GetValues(){
    Array^ values = Enum::GetValues(T::typeid);
    array<T>^ ret = gcnew array<T>(values->Length);
    for(int i = 0; i < values->Length; i++)
        ret[i] = (T)values->GetValue(i);
    return ret;
}