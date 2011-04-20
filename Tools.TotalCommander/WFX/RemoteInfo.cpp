//#include "stdafx.h"
#include "WFX common.h"
#include "..\Plugin\fsplugin.h"
#include "..\Exceptions.h"
#include <vcclr.h>
#include "..\Common.h"
#include "RemoteInfo.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace Tools::TotalCommanderT::ResourcesT;
using namespace Tools::ExtensionsT;
using namespace Microsoft::VisualBasic;
using namespace System::ComponentModel;

namespace Tools{namespace TotalCommanderT{

    RemoteInfo::RemoteInfo(const RemoteInfoStruct &ri){
        this->SizeLow = ri.SizeLow;
        this->SizeHigh = ri.SizeHigh;
        this->Attr = (FileAttributes)ri.Attr;
        this->LastWriteTime = FileTimeToDateTime(ri.LastWriteTime);
    }
    inline QWORD RemoteInfo::Size::get(){ return (QWORD)this->SizeHigh << 32 | (QWORD)this->SizeLow;}
    void RemoteInfo::Size::set(QWORD value){
        this->SizeHigh = Numbers::High(value);
        this->SizeHigh = Numbers::Low(value);
    }
    __int64 RemoteInfo::GetSize(){
        if(this->Size > Int64::MaxValue) throw gcnew InvalidOperationException(Exceptions::CannotBeRepresentedFormat("Size","Int64","GetSizeLow","GetSizeHigh"));
        return (__int64) this->Size;
    }
    void RemoteInfo::SetSize(__int64 value){
        if(value < 0) throw gcnew ArgumentOutOfRangeException("value");
        this->Size = (QWORD) value;
    }
    inline __int64 RemoteInfo::GetSizeLow(){return (__int64)this->SizeLow;}
    void RemoteInfo::SetSizeLow(__int64 value){
        if(value < 0 || value > (__int64)Int32::MaxValue) throw gcnew ArgumentOutOfRangeException("value");
        this->SizeLow = (DWORD) value;
    }
    inline __int64 RemoteInfo::GetSizeHigh(){return (__int64)this->SizeHigh;}
    void RemoteInfo::SetSizeHigh(__int64 value){
        if(value < 0 || value > (__int64)Int32::MaxValue) throw gcnew ArgumentOutOfRangeException("value");
        this->SizeHigh = (DWORD) value;
    }
    inline Int32 RemoteInfo::Attributes::get(){return Numbers::BitwiseSame((UInt32)this->Attr);}
    inline void RemoteInfo::Attributes::set(Int32 value){this->Attr = (FileAttributes)Numbers::BitwiseSame(value);}
}}