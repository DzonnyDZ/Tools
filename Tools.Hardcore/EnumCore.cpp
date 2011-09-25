#include "stdafx.h"
#include "EnumCore.h"

using namespace System;
using namespace Tools;
using namespace System::Collections::Generic;
using namespace System::Reflection;

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

generic <class T> where T:Enum, gcnew()
bool EnumCore::IsDefined(T value){
    return Array::IndexOf(Enum::GetValues(T::typeid), value) >= 0;
}

generic <class T> where T:Enum, gcnew()
FieldInfo^ EnumCore::GetConstant(T value){
    String^ name = Enum::GetName(T::typeid, value);
    if(name == nullptr) return nullptr;
    return T::typeid->GetField(name, BindingFlags::Public | BindingFlags::Static);
}

#pragma region GetFlags
inline cli::array<SByte>^  EnumCore::GetFlags(SByte  value){return EnumCore::GetFlagsS<SByte, Byte>  (value, 8);}
inline cli::array<Int16>^  EnumCore::GetFlags(Int16  value){return EnumCore::GetFlagsS<Int16, UInt16>(value, 16);}
inline cli::array<Int32>^  EnumCore::GetFlags(Int32  value){return EnumCore::GetFlagsS<Int32, UInt32>(value, 32);}
inline cli::array<Int64>^  EnumCore::GetFlags(Int64  value){return EnumCore::GetFlagsS<Int64, UInt64>(value, 64);}

inline cli::array<Byte>^   EnumCore::GetFlags(Byte   value){return EnumCore::GetFlagsU(value, 8);}
inline cli::array<UInt16>^ EnumCore::GetFlags(UInt16 value){return EnumCore::GetFlagsU(value, 16);}
inline cli::array<UInt32>^ EnumCore::GetFlags(UInt32 value){return EnumCore::GetFlagsU(value, 32);}
inline cli::array<UInt64>^ EnumCore::GetFlags(UInt64 value){return EnumCore::GetFlagsU(value, 64);}

template <class TU>
cli::array<TU>^ EnumCore::GetFlagsU(TU value, int size){
    auto ret = gcnew List<TU>();
    for(TU i = 1; i <= TU::MaxValue / 2; i *= 2)
        if((value & i) != 0) ret->Add(i);
    return ret->ToArray();
}
template <class TS, class TU>
cli::array<TS>^ EnumCore::GetFlagsS(TS value, int size){
    auto u = EnumCore::GetFlagsU((TU)value, size);
    auto ret = gcnew cli::array<TS>(u->Length);
    for(int i = 0; i < ret->Length; i++)
        ret[i] = (TS)u[i];
    return ret;
}

generic <class T> where T:Enum, gcnew()
cli::array<T>^ EnumCore::GetFlags(T value){
    auto et = Enum::GetUnderlyingType(T::typeid);
    Array^ flags;

             if(et->Equals(SByte::typeid))  flags = GetFlags((SByte)value);
        else if(et->Equals(Byte::typeid))   flags = GetFlags((Byte)value);
        else if(et->Equals(Int16::typeid))  flags = GetFlags((Int16)value);
        else if(et->Equals(UInt16::typeid)) flags = GetFlags((UInt16)value);
        else if(et->Equals(Int32::typeid))  flags = GetFlags((Int32)value);
        else if(et->Equals(UInt32::typeid)) flags = GetFlags((UInt32)value);
        else if(et->Equals(Int64::typeid))  flags = GetFlags((Int64)value);
        else if(et->Equals(UInt64::typeid)) flags = GetFlags((UInt64)value);
        else throw gcnew NotSupportedException(String::Format("Underlying type {1} of enumeration {0} is not supported.", et->Name, T::typeid->FullName));

    cli::array<T>^ ret = gcnew cli::array<T>(flags->Length);
    for(int i = 0; i < ret->Length; i++)
        ret[i] = (T)flags->GetValue(i);
    return ret;
}
#pragma endregion