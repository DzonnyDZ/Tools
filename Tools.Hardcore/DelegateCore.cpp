#include "stdafx.h"
#include "DelegateCore.h"

using namespace System;
using namespace Tools;

DelegateCore::DelegateCore() {/*do nothing*/ }

generic<class T> where T:Delegate
T DelegateCore::CombineWith(T a, T b) {
    return (T)Delegate::Combine(a, b);
}

generic <class T> where T : Delegate
T DelegateCore::CombineWith(T a, ... array<T>^ delegates) {
    return (T)Delegate::Combine(a, Delegate::Combine(delegates));
}

generic <class T> where T : Delegate
T DelegateCore::Remove(T source, T value) {
    return (T)Delegate::Remove(source, value);
}

generic <class T> where T : Delegate
T DelegateCore::RemoveAll(T source, T value) {
    return (T)Delegate::RemoveAll(source, value);
}

generic <class T> where T : Delegate
array<T>^ DelegateCore::InvocationList(T delegate) {
    if (delegate == nullptr) throw gcnew ArgumentNullException("source");
    array<Delegate^>^ invList = delegate->GetInvocationList();
    array<T>^ ret = gcnew array<T>(invList->Length);
    for (int i = 0; i < invList->Length; i++)
        ret[i] = (T)invList[i];
    return ret;
}