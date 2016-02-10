//#include "stdafx.h"
#include "WFX common.h"
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

        //OperationEventArgs
#pragma region "OperationEventArgs"
        OperationEventArgs::OperationEventArgs(System::String ^remoteDir, Tools::TotalCommanderT::OperationKind kind, Tools::TotalCommanderT::OperationStatus status) {
            if (remoteDir == nullptr) throw gcnew ArgumentNullException("remoteDir");
            this->remoteDir = remoteDir;
            this->kind = kind;
            this->status = status;
        }
        inline String^ OperationEventArgs::RemoteDir::get() { return this->remoteDir; }
        inline OperationKind OperationEventArgs::Kind::get() { return this->kind; }
        inline OperationStatus OperationEventArgs::Status::get() { return this->status; }
#pragma endregion

        //BitmapResult
#pragma region "BitmapResult"
        BitmapResult::BitmapResult(String^ ImagePath, bool Temporary) {
            if (ImagePath == nullptr) throw gcnew ArgumentNullException("ImagePath");
            this->ImageKey = ImagePath;
            this->Temporary = Temporary;
        }
        BitmapResult::BitmapResult(Drawing::Bitmap^ Bitmap) {
            if (Bitmap == nullptr)  throw gcnew ArgumentNullException("Bitmap");
            this->Image = Bitmap;
            this->ImageKey = nullptr;
        }
        BitmapResult::BitmapResult(Drawing::Bitmap^ Bitmap, String^ ImageKey) {
            if (Bitmap == nullptr)  throw gcnew ArgumentNullException("Bitmap");
            this->Image = Bitmap;
            this->ImageKey = ImageKey;
        }
        inline String^ BitmapResult::ImageKey::get() { return this->imagekey; }
        void BitmapResult::ImageKey::set(String^ value) {
            if (value != nullptr && value->Length > FindData::MaxPath - 1) throw gcnew IO::PathTooLongException(ResourcesT::Exceptions::PathTooLong);
            this->imagekey = value;
        }
        BitmapHandling BitmapResult::GetFlag() {
            BitmapHandling ret;
            if (this->Image == nullptr && this->Temporary)
                ret = BitmapHandling::ExtractAndDelete;
            else if (this->Image == nullptr)
                ret = BitmapHandling::ExtractYourself;
            else
                ret = BitmapHandling::Extracted;
            if (this->Cache) ret += BitmapHandling::Cache;
            return ret;
        }
#pragma endregion

#pragma region "CryptException"
        CryptException::CryptException(String^ message, CryptResult reason) :
            System::Security::Cryptography::CryptographicException(message) {
            CryptException::GetMessage(reason);
            this->HResult = (int)reason;
        }
        CryptException::CryptException(CryptResult reason) :
            System::Security::Cryptography::CryptographicException(CryptException::GetMessage(reason)) {
            this->HResult = (int)reason;
        }
        String^ CryptException::GetMessage(CryptResult reason) {
            switch (reason) {
            case CryptResult::Fail: return ResourcesT::Exceptions::EncryptDecryptFailed;
            case CryptResult::WriteError: return ResourcesT::Exceptions::WritePasswordStoreFailed;
            case CryptResult::ReadError: return ResourcesT::Exceptions::ReadPasswordStoreFailed;
            case CryptResult::NoMasterPassword: return ResourcesT::Exceptions::NoMasterPassword;
            case CryptResult::OK: throw gcnew ArgumentException(String::Format(ResourcesT::Exceptions::ValueNotSupported, reason), "reason");
            default: throw gcnew InvalidEnumArgumentException("reason", (int)reason, reason.GetType());
            }
        }
        inline CryptResult CryptException::Reason::get() { return (CryptResult) this->HResult; }
#pragma endregion
    }
}