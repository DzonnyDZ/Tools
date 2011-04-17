//#include "stdafx.h"
#include "FileSystemPlugin helpers.h"
#include "Plugin\fsplugin.h"
#include "Exceptions.h"
#include <vcclr.h>
#include "Common.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace Tools::TotalCommanderT::ResourcesT;
using namespace Tools::ExtensionsT;
using namespace Microsoft::VisualBasic;
using namespace System::ComponentModel;

namespace Tools{namespace TotalCommanderT{
    //RemoteInfo
#pragma region "RemoteInfo"
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
#pragma endregion
     //FindData
#pragma region "FindData"
    FindData::FindData(WIN32_FIND_DATA& Original){
        this->cAlternateFileName = gcnew String(Original.cAlternateFileName);
        this->cFileName = gcnew String(Original.cFileName);
        this->dwFileAttributes = (FileAttributes) Original.dwFileAttributes;
        this->dwReserved0 = (ReparsePointTags) Original.dwReserved0;
        this->dwReserved1 = Original.dwReserved1;
        this->ftCreationTime = FileTimeToDateTime(Original.ftCreationTime);
        this->ftLastAccessTime = FileTimeToDateTime(Original.ftLastAccessTime);
        this->ftLastAccessTime = FileTimeToDateTime(Original.ftLastWriteTime);
        this->nFileSizeLow = Original.nFileSizeLow;
        this->nFileSizeHigh = Original.nFileSizeHigh;
    }
    void FindData::Populate(WIN32_FIND_DATA &target){
        StringCopy(this->FileName,target.cFileName,FindData::MaxPath);
        for(int i=(this->FileName==nullptr?0:this->FileName->Length); i<FindData::MaxPath; i++)target.cFileName[i]=0;
        StringCopy(this->AlternateFileName,target.cAlternateFileName,14);
        for(int i=(this->AlternateFileName==nullptr?0:this->AlternateFileName->Length); i<14; i++)target.cAlternateFileName[i]=0;
       /* for(int i = 0; i < this->cFileName->Length; i++) target.cFileName[i] = this->cFileName[i];
        for(int i = this->cFileName->Length; i < MaxPath; i++) target.cFileName[i] = 0;
        for(int i = 0; i < this->cAlternateFileName->Length; i++) target.cAlternateFileName[i] = this->cAlternateFileName[i];
        for(int i = this->cAlternateFileName->Length; i < 14; i++) target.cAlternateFileName[i] = 0;*/
        target.dwFileAttributes = (DWORD) this->dwFileAttributes;
        target.dwReserved0 = (DWORD) this->dwReserved0;
        target.dwReserved1 = this->dwReserved1;
        target.ftCreationTime = DateTimeToFileTime(this->ftCreationTime);
        target.ftLastAccessTime = DateTimeToFileTime(this->ftLastAccessTime);
        target.ftLastWriteTime = DateTimeToFileTime(this->ftLastAccessTime);
        target.nFileSizeLow = this->nFileSizeLow;
        target.nFileSizeHigh = this->nFileSizeHigh;
    }

    WIN32_FIND_DATA FindData::ToFindData(){
        WIN32_FIND_DATA ret;
        Populate(ret);
        return ret;
    }
    inline FileAttributes FindData::Attributes::get(){ return this->dwFileAttributes; }
    void FindData::Attributes::set(FileAttributes value){ this->dwFileAttributes = value; }
    inline Nullable<DateTime> FindData::CreationTime::get(){ return this->ftCreationTime; }
    inline void FindData::CreationTime::set(Nullable<DateTime> value){ this->ftCreationTime = value; }
    inline Nullable<DateTime> FindData::AccessTime::get(){ return this->ftLastAccessTime; }
    inline void FindData::AccessTime::set(Nullable<DateTime> value){ this->ftLastAccessTime = value; }
    inline Nullable<DateTime> FindData::WriteTime::get(){ return this->ftLastWriteTime; }
    inline void FindData::WriteTime::set(Nullable<DateTime> value){this->ftLastWriteTime = value; }
    inline QWORD FindData::FileSize::get(){ return (QWORD)this->nFileSizeHigh <<32 | (QWORD)this->nFileSizeLow; }
    void FindData::FileSize::set(QWORD value){
        this->nFileSizeHigh = Numbers::High(value);
        this->nFileSizeLow = Numbers::Low(value);
    }
    inline ReparsePointTags FindData::ReparsePointTag::get(){ return this->dwReserved0;}
    inline void FindData::ReparsePointTag::set(ReparsePointTags value){ this->dwReserved0 = value;}
    inline DWORD FindData::Reserved1::get(){ return this->dwReserved1;}
    inline void FindData::Reserved1::set(DWORD value){ this->dwReserved1 = value;}
    inline String^ FindData::FileName::get(){ return this->cFileName;}
    void FindData::FileName::set(String^ value){
        if(value->Length > MaxPath-1) throw gcnew ArgumentException(Exceptions::NameTooLongFormat(MaxPath-1));
        this->cFileName = value;
    }
    inline String^ FindData::AlternateFileName::get(){ return this->cAlternateFileName; }
    void FindData::AlternateFileName::set(String^ value){
        if(value->Length > 14-1) throw gcnew ArgumentException(Exceptions::NameTooLongFormat(14-1));
        this->cFileName = value;
    }

    __int64 FindData::GetFileSize(){
        if(this->FileSize > Int64::MaxValue) throw gcnew InvalidOperationException(Exceptions::CannotBeRepresentedFormat("FileSize","Int64","GetFileSizeLow","GetFileSizeHigh"));
        return (__int64) this->FileSize;
    }
    void FindData::SetFileSize(__int64 value){
        if(value < 0) throw gcnew ArgumentOutOfRangeException("value");
        this->FileSize = (QWORD) value;
    }
    inline __int64 FindData::GetFileSizeLow(){return (__int64)this->nFileSizeLow;}
    void FindData::SetFileSizeLow(__int64 value){
        if(value < 0 || value > (__int64)Int32::MaxValue) throw gcnew ArgumentOutOfRangeException("value");
        this->nFileSizeLow = (DWORD) value;
    }
    inline __int64 FindData::GetFileSizeHigh(){return (__int64)this->nFileSizeHigh;}
    void FindData::SetFileSizeHigh(__int64 value){
        if(value < 0 || value > (__int64)Int32::MaxValue) throw gcnew ArgumentOutOfRangeException("value");
        this->nFileSizeHigh = (DWORD) value;
    }

    inline Int32 FindData::GetAttributes(){return Numbers::BitwiseSame((UInt32)this->Attributes);}
    inline void FindData::SetAttributes(Int32 value){this->Attributes = (FileAttributes)Numbers::BitwiseSame(value);}
    inline Int32 FindData::GetReserved1(){return Numbers::BitwiseSame((UInt32)this->Reserved1);}
    inline void FindData::SetReserved1(Int32 value){this->Reserved1 = (DWORD)Numbers::BitwiseSame(value);}
    inline Int32 FindData::GetReparsePointTag(){return Numbers::BitwiseSame((UInt32)this->ReparsePointTag);}
    inline void FindData::SetReparsePointTag(Int32 value){this->ReparsePointTag = (ReparsePointTags)Numbers::BitwiseSame(value);}
#pragma endregion
    //OperationEventArgs
#pragma region "OperationEventArgs"
    OperationEventArgs::OperationEventArgs(System::String ^remoteDir, Tools::TotalCommanderT::OperationKind kind, Tools::TotalCommanderT::OperationStatus status){
        if(remoteDir == nullptr) throw gcnew ArgumentNullException("remoteDir");
        this->remoteDir = remoteDir;
        this->kind = kind;
        this->status = status;
    }
    inline String^ OperationEventArgs::RemoteDir::get(){return this->remoteDir;}
    inline OperationKind OperationEventArgs::Kind::get(){return this->kind;}
    inline OperationStatus OperationEventArgs::Status::get(){return this->status;}
#pragma endregion
    //DefaultParams
#pragma region "DefaultParams"
    DefaultParams::DefaultParams(FsDefaultParamStruct& from){
        this->defaultIniName = gcnew String(from.DefaultIniName);
        this->version = gcnew System::Version(from.PluginInterfaceVersionHi,from.PluginInterfaceVersionLow,0,0);
    }
    inline System::Version^ DefaultParams::Version::get(){return this->version;}
    inline String^ DefaultParams::DefaultIniName::get(){return this->defaultIniName;}
#pragma endregion
    //BitmapResult
#pragma region "BitmapResult"
     BitmapResult::BitmapResult(String^ ImagePath, bool Temporary){
        if(ImagePath == nullptr) throw gcnew ArgumentNullException("ImagePath");
        this->ImageKey = ImagePath;
        this->Temporary = Temporary;
    }
   BitmapResult::BitmapResult(Drawing::Bitmap^ Bitmap) {
        if(Bitmap == nullptr)  throw gcnew ArgumentNullException("Bitmap"); 
        this->Image = Bitmap;
        this->ImageKey = nullptr;
   }
   BitmapResult::BitmapResult(Drawing::Bitmap^ Bitmap, String^ ImageKey){
        if(Bitmap == nullptr)  throw gcnew ArgumentNullException("Bitmap"); 
        this->Image = Bitmap;
        this->ImageKey = ImageKey;
    }
   inline String^ BitmapResult::ImageKey::get(){return this->imagekey;}
   void BitmapResult::ImageKey::set(String^ value){
       if(value != nullptr && value->Length > FindData::MaxPath - 1) throw gcnew IO::PathTooLongException(ResourcesT::Exceptions::PathTooLong);
       this->imagekey=value;
   }
   BitmapHandling BitmapResult::GetFlag(){
       BitmapHandling ret;
       if(this->Image == nullptr && this->Temporary)
           ret = BitmapHandling::ExtractAndDelete;
       else if (this->Image == nullptr)
        ret = BitmapHandling::ExtractYourself;
       else
           ret = BitmapHandling::Extracted;
       if(this->Cache) ret += BitmapHandling::Cache;
       return ret;
   }
#pragma endregion
#pragma region "BitmapHandling"
    BitmapHandling& operator += (BitmapHandling& a, const BitmapHandling& b){
        (&a)[0] = (BitmapHandling)((int)a + (int)b);
        return a;
    }
#pragma endregion 

    #pragma region "View definition"
    ColumnSource::ColumnSource(String^ source, String^ field, String^ unit){
        if(source == nullptr || source == "") throw gcnew ArgumentNullException("source");
        if(field == nullptr || field == "") throw gcnew ArgumentNullException("field");
        if(source->Contains(".") || source->Contains("|") || source->Contains(":") || source->Contains("]") || source->Contains(gcnew String((char)0,1)))
            throw gcnew ArgumentException(ResourcesT::Exceptions::InvalidCharacterFormat(source),"source");
        if(field->Contains(".") || field->Contains("|") || field->Contains(":") || field->Contains("]") || field->Contains(gcnew String((char)0,1)))
            throw gcnew ArgumentException(ResourcesT::Exceptions::InvalidCharacterFormat(field),"field");
        if(unit != nullptr && unit != "")
            if(unit->Contains(".") || unit->Contains("|") || unit->Contains(":") || unit->Contains("]") || unit->Contains(gcnew String((char)0,1)))
                throw gcnew ArgumentException(ResourcesT::Exceptions::InvalidCharacterFormat(unit),"unit");
        this->source = source;
        this->field = field;
        if(unit == nullptr || unit == "") this->unit = nullptr; else this->unit = unit;
    }
    inline String^ ColumnSource::Source::get(){return this->source;}
    inline String^ ColumnSource::Field::get(){return this->field;}
    inline String^ ColumnSource::Unit::get(){return this->unit;}
    inline String^ ColumnSource::ToString(){return "[=" + Source + "." + Field + (Unit==nullptr ? "" : ("." + Unit)) + "]";}

   ColumnDefinition::ColumnDefinition(ColumnSource^ source, String^ name, int width){
        if(source == nullptr) throw gcnew ArgumentNullException("source");
        if(name == nullptr) throw gcnew ArgumentNullException("name");
        if(name->Contains("\\n")) throw gcnew ArgumentException(ResourcesT::Exceptions::InvalidCharacterFormat(name),"name");
        this->source = source;
        this->name = name;
        this->width = width;
    }
    inline String^ ColumnDefinition::Name::get(){return this->name;}
    inline ColumnSource^ ColumnDefinition::Source::get(){return this->source;}
    inline int ColumnDefinition::Width::get(){return this->width;}
    
    ViewDefinition::ViewDefinition(array<ColumnDefinition^>^ columns, int FileNameWidth, int ExtensionWidth, bool autoAdjust, bool horizontalScroll){
        if(columns == nullptr) throw gcnew ArgumentNullException("columns");
        this->columns = columns;
        this->fileNameWidth = FileNameWidth;
        this->extensionWidth = ExtensionWidth;
        this->autoAdjust = autoAdjust;
        this->horizontalScroll = horizontalScroll;  
    }
    inline array<ColumnDefinition^>^ ViewDefinition::Columns::get(){return this->columns;}
    inline bool ViewDefinition::AutoAdjust::get(){return this->autoAdjust;}
    inline bool ViewDefinition::HorizontalScroll::get(){return this->horizontalScroll;}
    inline int ViewDefinition::FileNameWidth::get(){return this->fileNameWidth;}
    inline int ViewDefinition::ExtensionWidth::get(){return this->extensionWidth;}
    String^ ViewDefinition::GetColumnSourcesString(){
        System::Text::StringBuilder^ b = gcnew System::Text::StringBuilder();
        for each(ColumnDefinition^ cd in this->Columns){
            if(b->Length > 0) b->Append("\\n");
            b->Append(cd->Source->ToString());
        }
        return b->ToString();
    }
    String^ ViewDefinition::GetNamesString(){
        System::Text::StringBuilder^ b = gcnew System::Text::StringBuilder();
        for each(ColumnDefinition^ cd in this->Columns){
            if(b->Length > 0) b->Append("\\n");
            b->Append(cd->Name);
        }
        return b->ToString();
    }
    String^ ViewDefinition::GetWidthsString(){
        System::Text::StringBuilder^ b = gcnew System::Text::StringBuilder();
        b->Append(this->FileNameWidth.ToString(Globalization::CultureInfo::InvariantCulture) + "," + this->ExtensionWidth.ToString(Globalization::CultureInfo::InvariantCulture));
        for each(ColumnDefinition^ cd in this->Columns)
            b->Append("," + cd->Width.ToString(Globalization::CultureInfo::InvariantCulture));
        return b->ToString();
    }
    inline String^ ViewDefinition::GetOptionsString(){
        return (this->AutoAdjust ? "auto-adjust-width" : "-1") + "|" + (this->HorizontalScroll ? "1" : "0");
    }
#pragma endregion

#pragma region "CryptException"
    CryptException::CryptException(String^ message, CryptResult reason):
        System::Security::Cryptography::CryptographicException(message){
        CryptException::GetMessage(reason);
        this->HResult = (int)reason;
    }
    CryptException::CryptException(CryptResult reason):
        System::Security::Cryptography::CryptographicException(CryptException::GetMessage(reason)){
        this->HResult = (int)reason;
    }
    String^ CryptException::GetMessage(CryptResult reason){
        switch(reason){
        case CryptResult::Fail: return ResourcesT::Exceptions::EncryptDecryptFailed;
        case CryptResult::WriteError: return ResourcesT::Exceptions::WritePasswordStoreFailed;
        case CryptResult::ReadError: return ResourcesT::Exceptions::ReadPasswordStoreFailed;
        case CryptResult::NoMasterPassword: return ResourcesT::Exceptions::NoMasterPassword;
        case CryptResult::OK: throw gcnew ArgumentException(String::Format(ResourcesT::Exceptions::ValueNotSupported, reason), "reason");
        default: throw gcnew InvalidEnumArgumentException("reason", (int)reason, reason.GetType());
        }
    }
    inline CryptResult CryptException::Reason::get(){ return (CryptResult) this->HResult; }
#pragma endregion
}}