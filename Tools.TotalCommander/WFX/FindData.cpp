//#include "stdafx.h"
#include "FindData.h"
#include "..\Plugin\fsplugin.h"
#include "..\Exceptions.h"
#include <vcclr.h>
#include "..\Common.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace Tools::TotalCommanderT::ResourcesT;
using namespace Tools::ExtensionsT;
using namespace Microsoft::VisualBasic;
using namespace System::ComponentModel;

namespace Tools {
    namespace TotalCommanderT {

        FindData::FindData(const WIN32_FIND_DATAW& original) {
            this->cAlternateFileName = gcnew String(original.cAlternateFileName);
            this->cFileName = gcnew String(original.cFileName);
            this->dwFileAttributes = (FileAttributes)original.dwFileAttributes;
            this->dwReserved0 = (ReparsePointTags)original.dwReserved0;
            this->dwReserved1 = original.dwReserved1;
            this->ftCreationTime = FileTimeToDateTime(original.ftCreationTime);
            this->ftLastAccessTime = FileTimeToDateTime(original.ftLastAccessTime);
            this->ftLastAccessTime = FileTimeToDateTime(original.ftLastWriteTime);
            this->nFileSizeLow = original.nFileSizeLow;
            this->nFileSizeHigh = original.nFileSizeHigh;
        }
        FindData::FindData(const WIN32_FIND_DATAA& original) {
            this->cAlternateFileName = gcnew String(original.cAlternateFileName);
            this->cFileName = gcnew String(original.cFileName);
            this->dwFileAttributes = (FileAttributes)original.dwFileAttributes;
            this->dwReserved0 = (ReparsePointTags)original.dwReserved0;
            this->dwReserved1 = original.dwReserved1;
            this->ftCreationTime = FileTimeToDateTime(original.ftCreationTime);
            this->ftLastAccessTime = FileTimeToDateTime(original.ftLastAccessTime);
            this->ftLastAccessTime = FileTimeToDateTime(original.ftLastWriteTime);
            this->nFileSizeLow = original.nFileSizeLow;
            this->nFileSizeHigh = original.nFileSizeHigh;
        }
        void FindData::Populate(WIN32_FIND_DATAW &target) {
            StringCopy(this->FileName, target.cFileName, FindData::MaxPath);
            for (int i = (this->FileName == nullptr ? 0 : this->FileName->Length); i < FindData::MaxPath; i++)
                target.cFileName[i] = 0;
            StringCopy(this->AlternateFileName, target.cAlternateFileName, 14);
            for (int i = (this->AlternateFileName == nullptr ? 0 : this->AlternateFileName->Length); i < 14; i++)
                target.cAlternateFileName[i] = 0;
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

        void FindData::Populate(WIN32_FIND_DATAA &target) {
            StringCopy(this->FileName, target.cFileName, FindData::MaxPath);
            for (int i = (this->FileName == nullptr ? 0 : this->FileName->Length); i < FindData::MaxPath; i++)
                target.cFileName[i] = 0;
            StringCopy(this->AlternateFileName, target.cAlternateFileName, 14);
            for (int i = (this->AlternateFileName == nullptr ? 0 : this->AlternateFileName->Length); i < 14; i++)
                target.cAlternateFileName[i] = 0;
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

        WIN32_FIND_DATAW FindData::ToFindData() {
            WIN32_FIND_DATAW ret;
            Populate(ret);
            return ret;
        }
        inline FileAttributes FindData::Attributes::get() { return this->dwFileAttributes; }
        void FindData::Attributes::set(FileAttributes value) { this->dwFileAttributes = value; }
        inline Nullable<DateTime> FindData::CreationTime::get() { return this->ftCreationTime; }
        inline void FindData::CreationTime::set(Nullable<DateTime> value) { this->ftCreationTime = value; }
        inline Nullable<DateTime> FindData::AccessTime::get() { return this->ftLastAccessTime; }
        inline void FindData::AccessTime::set(Nullable<DateTime> value) { this->ftLastAccessTime = value; }
        inline Nullable<DateTime> FindData::WriteTime::get() { return this->ftLastWriteTime; }
        inline void FindData::WriteTime::set(Nullable<DateTime> value) { this->ftLastWriteTime = value; }
        inline QWORD FindData::FileSize::get() { return (QWORD)this->nFileSizeHigh << 32 | (QWORD)this->nFileSizeLow; }
        void FindData::FileSize::set(QWORD value) {
            this->nFileSizeHigh = Numbers::High(value);
            this->nFileSizeLow = Numbers::Low(value);
        }
        inline ReparsePointTags FindData::ReparsePointTag::get() { return this->dwReserved0; }
        inline void FindData::ReparsePointTag::set(ReparsePointTags value) { this->dwReserved0 = value; }
        inline DWORD FindData::Reserved1::get() { return this->dwReserved1; }
        inline void FindData::Reserved1::set(DWORD value) { this->dwReserved1 = value; }
        inline String^ FindData::FileName::get() { return this->cFileName; }
        void FindData::FileName::set(String^ value) {
            if (value->Length > MaxPath - 1) throw gcnew ArgumentException(Exceptions::NameTooLongFormat(MaxPath - 1));
            this->cFileName = value;
        }
        inline String^ FindData::AlternateFileName::get() { return this->cAlternateFileName; }
        void FindData::AlternateFileName::set(String^ value) {
            if (value->Length > 14 - 1) throw gcnew ArgumentException(Exceptions::NameTooLongFormat(14 - 1));
            this->cFileName = value;
        }

        __int64 FindData::GetFileSize() {
            if (this->FileSize > Int64::MaxValue) throw gcnew InvalidOperationException(Exceptions::CannotBeRepresentedFormat("FileSize", "Int64", "GetFileSizeLow", "GetFileSizeHigh"));
            return (__int64) this->FileSize;
        }
        void FindData::SetFileSize(__int64 value) {
            if (value < 0) throw gcnew ArgumentOutOfRangeException("value");
            this->FileSize = (QWORD)value;
        }
        inline __int64 FindData::GetFileSizeLow() { return (__int64)this->nFileSizeLow; }
        void FindData::SetFileSizeLow(__int64 value) {
            if (value < 0 || value >(__int64)Int32::MaxValue) throw gcnew ArgumentOutOfRangeException("value");
            this->nFileSizeLow = (DWORD)value;
        }
        inline __int64 FindData::GetFileSizeHigh() { return (__int64)this->nFileSizeHigh; }
        void FindData::SetFileSizeHigh(__int64 value) {
            if (value < 0 || value >(__int64)Int32::MaxValue) throw gcnew ArgumentOutOfRangeException("value");
            this->nFileSizeHigh = (DWORD)value;
        }

        inline Int32 FindData::GetAttributes() { return Numbers::BitwiseSame((UInt32)this->Attributes); }
        inline void FindData::SetAttributes(Int32 value) { this->Attributes = (FileAttributes)Numbers::BitwiseSame(value); }
        inline Int32 FindData::GetReserved1() { return Numbers::BitwiseSame((UInt32)this->Reserved1); }
        inline void FindData::SetReserved1(Int32 value) { this->Reserved1 = (DWORD)Numbers::BitwiseSame(value); }
        inline Int32 FindData::GetReparsePointTag() { return Numbers::BitwiseSame((UInt32)this->ReparsePointTag); }
        inline void FindData::SetReparsePointTag(Int32 value) { this->ReparsePointTag = (ReparsePointTags)Numbers::BitwiseSame(value); }

        void FindData::FindDataToAnsi(const WIN32_FIND_DATAW& source, WIN32_FIND_DATAA& target) {
            target.dwFileAttributes = source.dwFileAttributes;
            target.ftCreationTime = source.ftCreationTime;
            target.ftLastAccessTime = source.ftLastAccessTime;
            target.ftLastWriteTime = source.ftLastWriteTime;
            target.nFileSizeHigh = source.nFileSizeHigh;
            target.nFileSizeLow = source.nFileSizeLow;
            target.dwReserved0 = source.dwReserved0;
            target.dwReserved1 = source.dwReserved1;
            UnicodeToAnsi(source.cFileName, target.cFileName, MAX_PATH);
            UnicodeToAnsi(source.cAlternateFileName, target.cAlternateFileName, 14);
        }
        void FindData::FindDataToUnicode(const WIN32_FIND_DATAA& source, WIN32_FIND_DATAW& target) {
            target.dwFileAttributes = source.dwFileAttributes;
            target.ftCreationTime = source.ftCreationTime;
            target.ftLastAccessTime = source.ftLastAccessTime;
            target.ftLastWriteTime = source.ftLastWriteTime;
            target.nFileSizeHigh = source.nFileSizeHigh;
            target.nFileSizeLow = source.nFileSizeLow;
            target.dwReserved0 = source.dwReserved0;
            target.dwReserved1 = source.dwReserved1;
            AnsiToUnicode(source.cFileName, target.cFileName, MAX_PATH);
            AnsiToUnicode(source.cAlternateFileName, target.cAlternateFileName, 14);
        }
    }
}