//#include "stdafx.h"
#include "FileSystemPlugin.h"
#include "..\Plugin\fsplugin.h"
#include "..\Exceptions.h"
#include <vcclr.h>
#include "..\Common.h"
#include "WFX Callback Wrappers.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace Tools::TotalCommanderT::ResourcesT;
using namespace Tools::ExtensionsT;
using namespace Microsoft::VisualBasic;

namespace Tools{namespace TotalCommanderT{
    //FileSystemPlugin
#pragma region "FileSystemPlugin"
    FileSystemPlugin::FileSystemPlugin() : ContentPluginBase(){
         this->handleDictionary = gcnew Collections::Generic::Dictionary<int,Object^>();
         this->MaxHandle = 0;
         this->HandleSyncObj = gcnew Object();

         this->implementedFunctions = this->GetSupportedFunctions<WfxFunctions>();

         const Reflection::BindingFlags flags = Reflection::BindingFlags::Instance | Reflection::BindingFlags::Public | Reflection::BindingFlags::NonPublic;
         this->ftpModeAdvertisementImplemented = Tools::TypeTools::GetAttribute<MethodNotSupportedAttribute^>(Tools::ReflectionT::ReflectionTools::GetOverridingMethod(FileSystemPlugin::typeid->GetMethod("FtpModeAdvertisement", flags),this->GetType()),false) == nullptr;
         this->openFileImplemented = Tools::TypeTools::GetAttribute<MethodNotSupportedAttribute^>(Tools::ReflectionT::ReflectionTools::GetOverridingMethod(FileSystemPlugin::typeid->GetMethod("OpenFile", flags),this->GetType()),false) == nullptr;
         this->showFileInfoImplemented = Tools::TypeTools::GetAttribute<MethodNotSupportedAttribute^>(Tools::ReflectionT::ReflectionTools::GetOverridingMethod(FileSystemPlugin::typeid->GetMethod("ShowFileInfo",flags),this->GetType()),false) == nullptr;
         this->executeCommandImplemented = Tools::TypeTools::GetAttribute<MethodNotSupportedAttribute^>(Tools::ReflectionT::ReflectionTools::GetOverridingMethod(FileSystemPlugin::typeid->GetMethod("ExecuteCommand", flags),this->GetType()),false) == nullptr;

         this->unicode = true;
         this->isInTotalCommander = false;
    }

    inline FileSystemPlugin^ FileSystemPlugin::GetPluginByNumber(int pluginNr){
        return FileSystemPlugin::registeredPlugins->ContainsKey(pluginNr) ? FileSystemPlugin::registeredPlugins[pluginNr] : nullptr;
    }

    inline Tools::TotalCommanderT::PluginType FileSystemPlugin::PluginType::get(){
        return Tools::TotalCommanderT::PluginType::FileSystem;
    }
    inline Type^ FileSystemPlugin::PluginBaseClass::get(){ return FileSystemPlugin::typeid; }
    inline WfxFunctions FileSystemPlugin::ImplementedFunctions::get(){ return this->implementedFunctions; }
    inline int FileSystemPlugin::MaxPath::get(){ return this->Unicode ? FindData::MaxPathW : FindData::MaxPathA; }
#pragma region TC functions
#pragma region Initialization
    int FileSystemPlugin::FsInit(int PluginNr, tProgressProcW pProgressProc, tLogProcW pLogProc, tRequestProcW pRequestProc){
        if(this->Initialized == true) throw gcnew InvalidOperationException(ResourcesT::Exceptions::PluginInitialized);
        this->pluginNr = PluginNr;
        this->progressProc = gcnew ProgressProcWrapper(pProgressProc);
        this->logProc  = gcnew LogProcWrapper(pLogProc);
        this->requestProc = gcnew RequestProcWrapper(pRequestProc);
        this->initialized = true;
        this->isInTotalCommander = true;
        this->unicode = true;
        FileSystemPlugin::registeredPlugins->Add(PluginNr, this);
        this->OnInit();
        return 0;
    }
    int FileSystemPlugin::FsInit(int PluginNr, tProgressProc pProgressProc, tLogProc pLogProc, tRequestProc pRequestProc){
        if(this->Initialized == true) throw gcnew InvalidOperationException(ResourcesT::Exceptions::PluginInitialized);
        this->pluginNr = PluginNr;
        this->progressProc = gcnew ProgressProcWrapper(pProgressProc);
        this->logProc  = gcnew LogProcWrapper(pLogProc);
        this->requestProc = gcnew RequestProcWrapper(pRequestProc);
        this->initialized = true;
        this->isInTotalCommander = true;
        this->unicode = false;
        FileSystemPlugin::registeredPlugins->Add(PluginNr, this);
        this->OnInit();
        return 0;
    }
    void FileSystemPlugin::InitializePlugin(int PluginNr, ProgressCallback^ progress, LogCallback^ log, RequestCallback^ request){
        if(progress == nullptr) throw gcnew ArgumentNullException("progress");
        if(log == nullptr) throw gcnew ArgumentNullException("log");
        if(request == nullptr) throw gcnew ArgumentNullException("request");
        if(this->Initialized == true) throw gcnew InvalidOperationException(ResourcesT::Exceptions::PluginInitialized);
        this->pluginNr = PluginNr;
        this->progressProc = gcnew ProgressProcWrapper(progress);
        this->logProc = gcnew LogProcWrapper(log);
        this->requestProc = gcnew RequestProcWrapper(request);
        this->initialized = true;
        this->isInTotalCommander = false;
        this->unicode = true;
        FileSystemPlugin::registeredPlugins->Add(PluginNr, this);
        this->OnInit();
    }
    bool FileSystemPlugin::IsInTotalCommander::get(){
        return this->isInTotalCommander;
    }
    bool FileSystemPlugin::Unicode::get(){
        return this->unicode;
    }
#pragma endregion

    HANDLE FileSystemPlugin::FsFindFirst(wchar_t* path, WIN32_FIND_DATAW *findData){
        Tools::TotalCommanderT::FindData% fd = Tools::TotalCommanderT::FindData();// *gcnew Tools::TotalCommanderT::FindData(*FindData);
        Object^ object;
        Exception^ exception = nullptr;
        try{
            object = this->FindFirst(gcnew String(path), fd);
        }catch(IO::DirectoryNotFoundException^ ex){exception=ex;
        }catch(UnauthorizedAccessException^ ex){exception=ex;
        }catch(Security::SecurityException^ ex){exception=ex;
        }catch(IO::IOException^ ex){exception=ex;}
        if(exception != nullptr){
            SetLastError(Marshal::GetHRForException(exception));
            return INVALID_HANDLE_VALUE;
        }else if(object == nullptr){
            SetLastError(ERROR_NO_MORE_FILES);
            return INVALID_HANDLE_VALUE;
        }else{
            int handle = this->HandleAdd(object);
            fd.Populate(*findData);
            return (HANDLE)handle;
        }
    }
    BOOL FileSystemPlugin::FsFindNext(HANDLE hdl, WIN32_FIND_DATAW *findData){
        Tools::TotalCommanderT::FindData% fd = *gcnew Tools::TotalCommanderT::FindData(*findData);
        Object^ object = this->HandleGet((int) hdl);
        Exception^ exception = nullptr;
        bool ret;
        try{ ret = this->FindNext(object, fd); }
        catch(IO::DirectoryNotFoundException^ ex){ exception=ex; }
        catch(UnauthorizedAccessException^ ex){ exception=ex; }
        catch(Security::SecurityException^ ex){ exception=ex; }
        catch(IO::IOException^ ex){ exception=ex; }
        if(exception != nullptr || !ret) 
            return false;
        else{
            fd.Populate(*findData);
            return true;
        }
    }
    int FileSystemPlugin::FsFindClose(HANDLE hdl){
        Object^ object = this->HandleGet((int) hdl);
        this->FindClose(object);
        this->HandleRemove((int) hdl);
        return 0;
    }
#pragma endregion

#pragma region ".NET Functions"
    int FileSystemPlugin::PluginNr::get(){
        if(!this->Initialized) throw gcnew InvalidOperationException(Exceptions::PluginNotInitialized);
        return this->pluginNr;
    }
    void FileSystemPlugin::OnInit(){/*do nothing*/}
    inline Collections::Generic::Dictionary<int,Object^>^ FileSystemPlugin::HandleDictionary::get(){ return this->handleDictionary; }
#pragma region "Handles"
    int FileSystemPlugin::GetNextHandle(){
        System::Threading::Monitor::Enter(this->HandleSyncObj);
        try{
            return ++this->MaxHandle;
        }finally{ System::Threading::Monitor::Exit(this->HandleSyncObj); }
    }
    int FileSystemPlugin::HandleAdd(Object^ object){
        System::Threading::Monitor::Enter(this->HandleSyncObj);
        try{
            this->HandleDictionary->Add(++this->MaxHandle, object);
            return this->MaxHandle;
        }finally{ System::Threading::Monitor::Exit(this->HandleSyncObj); }
    }
    bool FileSystemPlugin::HandleRemove(Object^ object){
        System::Threading::Monitor::Enter(this->HandleSyncObj);
        try{
            for each(Collections::Generic::KeyValuePair<int,Object^> item in this->HandleDictionary)
                if(item.Value->Equals(object)){
                    this->HandleDictionary->Remove(item.Key);
                    return true;
                }
            return false;   
        }finally{ System::Threading::Monitor::Exit(this->HandleSyncObj); }
    }
    bool FileSystemPlugin::HandleRemove(int handle){
        System::Threading::Monitor::Enter(this->HandleSyncObj);
        try{
            if(this->HandleDictionary->ContainsKey(handle)){
                this->HandleDictionary->Remove(handle);
                return true;
            }else return false;
        }finally{ System::Threading::Monitor::Exit(this->HandleSyncObj); }
    }
    Object^ FileSystemPlugin::HandleGet(int handle){
        System::Threading::Monitor::Enter(this->HandleSyncObj);
        try{
            return this->HandleDictionary->ContainsKey(handle) ? this->HandleDictionary[handle] : nullptr;
        }finally{ System::Threading::Monitor::Exit(this->HandleSyncObj); }
    }
    int FileSystemPlugin::HandleGetHandle(Object^ object){
        System::Threading::Monitor::Enter(this->HandleSyncObj);
        try{
            for each(Collections::Generic::KeyValuePair<int, Object^> item in this->HandleDictionary)
                if(item.Value->Equals(object)) return item.Key;
            return -1;
        }finally{ System::Threading::Monitor::Exit(this->HandleSyncObj); }
    }
    void FileSystemPlugin::HandleReplace(int handle, Object^ object){
        System::Threading::Monitor::Enter(this->HandleSyncObj);
        try{
            this->HandleDictionary[handle] = object;    
        }finally{ System::Threading::Monitor::Exit(this->HandleSyncObj); }
    }
#pragma endregion
    inline bool FileSystemPlugin::Initialized::get(){return this->initialized;}
#pragma region "Callbacks"
    bool FileSystemPlugin::ProgressProc(String^ SourceName, String^ TargetName,int PercentDone){
        if(!this->Initialized) throw gcnew InvalidOperationException(Exceptions::PluginNotInitialized);
        return this->progressProc(this, SourceName, TargetName, PercentDone);
    }
    void FileSystemPlugin::LogProc(LogKind MsgType,String^ LogString){
        if(!this->Initialized) throw gcnew InvalidOperationException(Exceptions::PluginNotInitialized);
        this->logProc(this, MsgType, LogString);
    }
    void FileSystemPlugin::LogProcConnect(String^ FileSystem){
        if(FileSystem == nullptr) throw gcnew ArgumentNullException("FileSystem");
        if(!FileSystem->StartsWith("\\") || FileSystem->StartsWith("\\\\")) throw gcnew ArgumentException(Exceptions::InvalidPathFormatFormat(FileSystem),"FileSystem");
        this->LogProc(LogKind::Connect, "CONNECT " + FileSystem);
    }
    void FileSystemPlugin::LogProcTransferComplete(String^ Source, String^ Target){
        if(Source == nullptr) throw gcnew ArgumentNullException("Source");
        if(Target == nullptr) throw gcnew ArgumentNullException("Target");
        this->LogProc(LogKind::TransferComplete,String::Format("{0} -> {1}",Source,Target));
    }
    String^ FileSystemPlugin::RequestProc(InputRequestKind RequestType, String^ CustomTitle, String^ CustomText, String^ DefaultText, int maxlen){
        if(!this->Initialized) throw gcnew InvalidOperationException(Exceptions::PluginNotInitialized);
        return this->requestProc(this, RequestType, CustomTitle, CustomText, DefaultText, maxlen);            
    }
#pragma region Crypto
    String^ FileSystemPlugin::CryptProc(CryptMode mode, String^ connectionName, String^ password, int maxlen){
        if(!this->CryptInitialized) throw gcnew InvalidOperationException(ResourcesT::Exceptions::CryptoNotInitialized);
        return this->cryptProc(this, mode, connectionName, password, maxlen);
    }
    void FileSystemPlugin::SavePassword(String^ connectioName, String^ password){
        if(connectioName == nullptr) throw gcnew ArgumentNullException("connectionName");
        if(password == nullptr) throw gcnew ArgumentNullException("password");
        this->CryptProc(CryptMode::SavePassword, connectioName, password, password->Length);
    }
    String^ FileSystemPlugin::LoadPassword(String^ connectionName, bool showUI){
        if(connectionName == nullptr) throw gcnew ArgumentNullException("connectionName");
        return this->CryptProc(showUI ? CryptMode::LoadPassword : CryptMode::LoadPasswordNoUI, connectionName, nullptr, this->MaxPath);
    }
    void FileSystemPlugin::MovePassword(String^ sourceConnectionName, String^ targetConnectionName, bool deleteOriginal){
        if(sourceConnectionName == nullptr) throw gcnew ArgumentNullException("sourceConnectionName");
        if(targetConnectionName == nullptr) throw gcnew ArgumentNullException("targetConnectionName");
        this->CryptProc(deleteOriginal ? CryptMode::MovePassword : CryptMode::CopyPassword, sourceConnectionName, targetConnectionName, Math::Max(sourceConnectionName->Length, targetConnectionName->Length));
    }
    void FileSystemPlugin::DeletePassword(String^ connectionName){
        if(connectionName == nullptr) throw gcnew ArgumentNullException("connectionName");
        this->CryptProc(CryptMode::DeletePassword, connectionName, nullptr, connectionName->Length);
    }
#pragma endregion
#pragma endregion
    inline void FileSystemPlugin::FindClose(Object^ Status){/*do nothing*/}
#pragma endregion

#pragma region "Optional functions"
#pragma region "Crypto"
    //SetCryptCallback
    void FileSystemPlugin::FsSetCryptCallback(tCryptProc pCryptProc, int cryptoNr, int flags){
        if(this->CryptInitialized) throw gcnew InvalidOperationException(ResourcesT::Exceptions::CryptoAlreadyInitialized);
        this->cryptProc = gcnew CryptProcWrapper(pCryptProc);
        this->cryptoNr = CryptoNr;
        this->cryptInitialized = true;
        this->OnInitializeCryptography((CryptFlags)flags);
    }
    void FileSystemPlugin::FsSetCryptCallbackW(tCryptProcW pCryptProc, int cryptoNr, int flags){
        if(this->CryptInitialized) throw gcnew InvalidOperationException(ResourcesT::Exceptions::CryptoAlreadyInitialized);
        this->cryptProc = gcnew CryptProcWrapper(pCryptProc);
        this->cryptoNr = CryptoNr;
        this->cryptInitialized = true;
        this->OnInitializeCryptography((CryptFlags)flags);
    }
    void FileSystemPlugin::InitializeCryptography(CryptCallback^ cryptProc, int cryptoNr, CryptFlags flags){
        if(this->CryptInitialized) throw gcnew InvalidOperationException(ResourcesT::Exceptions::CryptoAlreadyInitialized);
        if(this->ImplementedFunctions.HasFlag(WfxFunctions::SetCryptCallback)){
            if(cryptProc == nullptr) throw gcnew ArgumentNullException("cryptProc");
            this->cryptProc = gcnew CryptProcWrapper(cryptProc);
            this->cryptoNr = cryptoNr;
            this->cryptInitialized = true;
            this->OnInitializeCryptography(flags);
        }
    }
    inline bool FileSystemPlugin::CryptInitialized::get(){return this->cryptInitialized;}
    inline int FileSystemPlugin::CryptoNr::get(){return this->cryptoNr;}
    inline void FileSystemPlugin::OnInitializeCryptography(CryptFlags){ throw gcnew NotSupportedException();}
#pragma endregion

    //MkDir
    BOOL FileSystemPlugin::FsMkDir(wchar_t* path){
        try{ return this->MkDir(gcnew String(path)); }
        catch(IO::IOException^){return FALSE;}
        catch(Security::SecurityException^){return FALSE;}
        catch(UnauthorizedAccessException^){return FALSE;}
        return TRUE;
    }
    inline bool FileSystemPlugin::MkDir(String^){ throw gcnew NotSupportedException(); }

    //ExecuteFile
    int FileSystemPlugin::FsExecuteFile(HWND mainWin, wchar_t* remoteName, wchar_t* verb){
        Exception^ ex = nullptr;
        String^% rn = gcnew String(remoteName);
        try{
            ExecExitCode ret =  this->ExecuteFile((IntPtr)mainWin, rn, gcnew String(verb));
            String^ old = gcnew String(remoteName);
            if(old != rn) {
                if(rn->Length > this->MaxPath - 1) throw gcnew IO::PathTooLongException(Exceptions::ParamAssignedTooLongFormat("remoteName", "ExecuteFile"));
                StringCopy(rn, remoteName, this->MaxPath);
            }
            return (int) ret;
        }
        catch(InvalidOperationException^){}
        catch(IO::IOException^){}
        catch(Security::SecurityException^){}
        catch(UnauthorizedAccessException^){}
        return (int)ExecExitCode::Error;
    }
    inline int FileSystemPlugin::FsExecuteFile(HANDLE mainWin, wchar_t* remoteName, wchar_t* verb){return this->FsExecuteFile((HWND)mainWin, remoteName, verb);}
    inline ExecExitCode FileSystemPlugin::ExecuteFile(IntPtr hMainWin, String^% remoteName, String^ verb){
        if(!this->ImplementedFunctions.HasFlag(WfxFunctions::ExecuteFile))
            throw gcnew NotSupportedException();
        if(verb->ToLower()->StartsWith("mode ") && ftpModeAdvertisementImplemented)
            return this->FtpModeAdvertisement(hMainWin, remoteName, verb->Substring(5));
        else if(verb->ToLower() == "open" && openFileImplemented)
            return this->OpenFile(hMainWin, remoteName);
        else if(verb->ToLower() == "properties" && showFileInfoImplemented)
            return this->ShowFileInfo(hMainWin, remoteName);
        else if(verb->ToLower()->StartsWith("quote ") && executeCommandImplemented)
            return this->ExecuteCommand(hMainWin, remoteName, verb->Substring(6));
        else
            return ExecExitCode::Error;
    }
#pragma region "ExecuteFile helper methods"
        ExecExitCode FileSystemPlugin::FtpModeAdvertisement(IntPtr, String^, String^){throw gcnew NotSupportedException();}
        ExecExitCode FileSystemPlugin::OpenFile(IntPtr, String^%){throw gcnew NotSupportedException();}
        ExecExitCode FileSystemPlugin::ShowFileInfo(IntPtr, String^){throw gcnew NotSupportedException();}
        ExecExitCode FileSystemPlugin::ExecuteCommand(IntPtr, String^%, String^){throw gcnew NotSupportedException();}
#pragma endregion

    //RenMovFile
    int FileSystemPlugin::FsRenMovFile(wchar_t* oldName, wchar_t* newName, BOOL move, BOOL overWrite, RemoteInfoStruct* ri){
        try{
            return (int)this->RenMovFile(gcnew String(oldName), gcnew String(newName), move == 0 ? false : true, overWrite == 0 ? false : true, RemoteInfo(*ri));
        }
        catch(UnauthorizedAccessException^){ return (int)FileSystemExitCode::ReadError; }
        catch(Security::SecurityException^){ return (int)FileSystemExitCode::ReadError; }
        catch(IO::FileNotFoundException^){ return (int)FileSystemExitCode::FileNotFound; }
        catch(IO::DirectoryNotFoundException^){ return (int)FileSystemExitCode::WriteError; }
        catch(IO::IOException^){ return (int)FileSystemExitCode::ReadError; }
        catch(InvalidOperationException^){ return (int)FileSystemExitCode::NotSupported; }
    }
    inline FileSystemExitCode FileSystemPlugin::RenMovFile(String^, String^, bool, bool, RemoteInfo){ throw gcnew NotSupportedException(); }
    
    //GetFile
    int FileSystemPlugin::FsGetFile(wchar_t* remoteName, wchar_t* localName, int copyFlags, RemoteInfoStruct* ri){
        String^% ln = gcnew String(localName);
        try{
            FileSystemExitCode ret = this->GetFile(gcnew String(remoteName), ln, (CopyFlags)copyFlags, RemoteInfo(*ri));
            String^ old = gcnew String(localName);
            if(old != ln){
                if(ln->Length >= this->MaxPath) throw gcnew IO::PathTooLongException(Exceptions::ParamAssignedTooLongFormat("localName", "GetFile"));
                StringCopy(ln, localName, this->MaxPath);
            }
            return (int)ret;
        }
        catch(UnauthorizedAccessException^){ return (int)FileSystemExitCode::ReadError; }
        catch(Security::SecurityException^){ return (int)FileSystemExitCode::ReadError; }
        catch(IO::FileNotFoundException^){ return (int)FileSystemExitCode::FileNotFound; }
        catch(IO::DirectoryNotFoundException^){ return (int)FileSystemExitCode::WriteError; }
        catch(IO::IOException^){ return (int)FileSystemExitCode::ReadError; }
        catch(InvalidOperationException^){ return (int)FileSystemExitCode::NotSupported; }
    }
    inline FileSystemExitCode FileSystemPlugin::GetFile(String^, String^%, CopyFlags, RemoteInfo){ throw gcnew NotSupportedException(); }
    
    //PutFile
    int FileSystemPlugin::FsPutFile(wchar_t* localName, wchar_t* remoteName, int copyFlags){
        String^% rn = gcnew String(remoteName);
        try{
            FileSystemExitCode ret = this->PutFile(gcnew String(localName), rn, (CopyFlags)copyFlags);
            String^ old = gcnew String(remoteName);
            if(old != rn){
                if(rn->Length >= this->MaxPath) throw gcnew IO::PathTooLongException(Exceptions::ParamAssignedTooLongFormat("remoteName", "PutFile"));
                StringCopy(rn,remoteName, this->MaxPath);
            }
            return (int) ret;
        }
        catch(UnauthorizedAccessException^){ return (int)FileSystemExitCode::ReadError; }
        catch(Security::SecurityException^){ return (int)FileSystemExitCode::ReadError; }
        catch(IO::FileNotFoundException^){ return (int)FileSystemExitCode::FileNotFound; }
        catch(IO::DirectoryNotFoundException^){ return (int)FileSystemExitCode::WriteError; }
        catch(IO::IOException^){ return (int)FileSystemExitCode::ReadError; }
        catch(InvalidOperationException^){ return (int)FileSystemExitCode::NotSupported; }
    }
    inline FileSystemExitCode FileSystemPlugin::PutFile(String^, String^%, CopyFlags){ throw gcnew NotSupportedException(); }
    //Delete file
    BOOL FileSystemPlugin::FsDeleteFile(wchar_t* remoteName){
        try{ return this->DeleteFile(gcnew String(remoteName)); }
        catch(UnauthorizedAccessException^){ return false; }
        catch(Security::SecurityException^){ return false; }
        catch(IO::IOException^){ return false; }
    }
    inline bool FileSystemPlugin::DeleteFile(String^){ throw gcnew NotSupportedException(); }
    //RemoveDir
    BOOL FileSystemPlugin::FsRemoveDir(wchar_t* remoteName){
        try{ return this->RemoveDir(gcnew String(remoteName)); }
        catch(UnauthorizedAccessException^){ return false; }
        catch(Security::SecurityException^){ return false; }
        catch(IO::IOException^){ return false; }
    }
    inline bool FileSystemPlugin::RemoveDir(String^){ throw gcnew NotSupportedException(); }
    //Disconect
    inline BOOL FileSystemPlugin::FsDisconnect(wchar_t* disconnectRoot){
        return this->Disconnect(gcnew String(disconnectRoot));
    }
    inline bool FileSystemPlugin::Disconnect(String^){ throw gcnew NotSupportedException(); }
    //SetAttr
    BOOL FileSystemPlugin::FsSetAttr(wchar_t* remoteName, int newAttr){
        try{ this->SetAttr(gcnew String(remoteName), (StandardFileAttributes)newAttr);}
        catch(UnauthorizedAccessException^){ return FALSE; }
        catch(Security::SecurityException^){ return FALSE; }
        catch(IO::IOException^){ return FALSE; }
        return TRUE;
    }
    inline void FileSystemPlugin::SetAttr(String^, StandardFileAttributes){ throw gcnew NotSupportedException(); }
    //SetTime
    BOOL FileSystemPlugin::FsSetTime(wchar_t* remoteName, ::FILETIME *creationTime, ::FILETIME *lastAccessTime, ::FILETIME *lastWriteTime){
        Nullable<DateTime> create = Nullable<DateTime>();
        Nullable<DateTime> access = Nullable<DateTime>();
        Nullable<DateTime> write = Nullable<DateTime>();
        if(creationTime!=nullptr) create = Nullable<DateTime>(FileTimeToDateTime(*creationTime));
        if(lastAccessTime!=nullptr) access = Nullable<DateTime>(FileTimeToDateTime(*lastAccessTime));
        if(lastWriteTime!=nullptr) write = Nullable<DateTime>(FileTimeToDateTime(*lastWriteTime));
        try{ this->SetTime(gcnew String(remoteName), create, access, write);}
        catch(UnauthorizedAccessException^){ return FALSE; }
        catch(Security::SecurityException^){ return FALSE; }
        catch(IO::IOException^){ return FALSE; }
        return TRUE;
    }
    inline void FileSystemPlugin::SetTime(String^, Nullable<DateTime>, Nullable<DateTime>, Nullable<DateTime>){ throw gcnew NotSupportedException(); }
    //StatusInfo
    void FileSystemPlugin::FsStatusInfo(wchar_t* remoteDir, int infoStartEnd, int infoOperation){
        this->StatusInfo(gcnew String(remoteDir), (OperationStatus)infoStartEnd, (OperationKind)infoOperation);     
    }
    void FileSystemPlugin::StatusInfo(String^ remoteDir, OperationStatus infoStartEnd, OperationKind infoOperation){
        OperationEventArgs^ e = gcnew OperationEventArgs(remoteDir, infoOperation, infoStartEnd);
        if(e->Status == OperationStatus::Start) this->OnOperationStarting(e);
        else if(e->Status == OperationStatus::End) this->OnOperationFinished(e);
        this->OnOperationStatusChanged(e);
    }
    void FileSystemPlugin::OnOperationStatusChanged(OperationEventArgs^ e){/*Do nothing*/}
    void FileSystemPlugin::OnOperationStarting(OperationEventArgs^ e){/*Do nothing*/}
    void FileSystemPlugin::OnOperationFinished(OperationEventArgs^ e){/*Do nothing*/}
    //GetDefRootName
    void FileSystemPlugin::FsGetDefRootName(char* defRootName, int maxlen){
        StringCopy(this->Name, defRootName, maxlen);
    }

    int FileSystemPlugin::FsExtractCustomIcon(wchar_t* remoteName, int extractFlags, HICON* theIcon){
        String^ rn = gcnew String(remoteName);
        Drawing::Icon^ icon = nullptr;
        IconExtractResult ret = this->ExctractCustomIcon(rn, (IconExtractFlags)extractFlags, icon);
        String^ old = gcnew String(remoteName);
        if(old != rn){
            if(rn->Length > this->MaxPath - 1) throw gcnew IO::PathTooLongException(ResourcesT::Exceptions::PathTooLong);
            StringCopy(rn, remoteName, this->MaxPath);
        }
        if(icon != nullptr) theIcon[0] = (HICON)(Int32)icon->Handle; else theIcon[0] = NULL;
        return (int)ret;
    }
    inline IconExtractResult FileSystemPlugin::ExctractCustomIcon(String^%, IconExtractFlags, Drawing::Icon^%){ throw gcnew NotSupportedException(); }

    inline void FileSystemPlugin::FsSetDefaultParams(FsDefaultParamStruct* dps){
        this->SetDefaultParams(DefaultParams(*dps));
    }
    void FileSystemPlugin::SetDefaultParams(DefaultParams dps){
        if(this->pluginParams.HasValue) throw gcnew InvalidOperationException(ResourcesT::Exceptions::PropertyWasInitializedFormat("PluginParams"));
        this->pluginParams = dps;
    }
    
    DefaultParams FileSystemPlugin::PluginParams::get(){
        if(!this->pluginParams.HasValue) throw gcnew InvalidOperationException(ResourcesT::Exceptions::PropertyWasNotInitializedFormat("PluginParams")); 
        return this->pluginParams.Value;
    }

    int FileSystemPlugin::FsGetPreviewBitmap(wchar_t* remoteName, int width, int height, HBITMAP* returnedBitmap){
        BitmapResult^ bmp = this->GetPreviewBitmap(gcnew String(remoteName), width, height);
        if(bmp == nullptr){
            return (int)BitmapHandling::None;
        }
        if(bmp->ImageKey != nullptr)
            StringCopy(bmp->ImageKey, remoteName, this->MaxPath);
        if(bmp->Image != nullptr) 
            returnedBitmap[0] = (HBITMAP)(int)bmp->Image->GetHbitmap();
        else
            returnedBitmap[0] = nullptr;
        return (int)bmp->GetFlag();
    }
    inline BitmapResult^ FileSystemPlugin::GetPreviewBitmap(String^, int, int){throw gcnew NotSupportedException();}
    
    inline BOOL FileSystemPlugin::FsLinksToLocalFiles(void){ return this->LinksToLocalFiles ? 1 : 0;}
    inline bool FileSystemPlugin::LinksToLocalFiles::get(){throw gcnew NotSupportedException();} 

    BOOL FileSystemPlugin::FsGetLocalName(wchar_t* remoteName, int maxlen){
        String^ ret = this->GetLocalName(gcnew String(remoteName), maxlen - 1);
        if(ret != nullptr){
            if(ret->Length > this->MaxPath - 1) throw gcnew IO::PathTooLongException(Exceptions::PathTooLong);
            StringCopy(ret, remoteName, this->MaxPath);
            return TRUE;
        }
        return FALSE;
    }
    inline String^ FileSystemPlugin::GetLocalName(String^, int){throw gcnew NotSupportedException();}

    BOOL FileSystemPlugin::FsContentGetDefaultView(wchar_t* ViewContents, wchar_t* ViewHeaders, wchar_t* ViewWidths, wchar_t* ViewOptions, int maxlen){
        ViewDefinition^ vd = GetDefaultView(maxlen - 1);
        if(vd == nullptr) return (BOOL)false;
        String^ contents = vd->GetColumnSourcesString();
        String^ headers = vd->GetNamesString();
        String^ widths = vd->GetWidthsString();
        String^ options = vd->GetOptionsString();
        if(contents->Length > maxlen-1 || headers->Length > maxlen - 1 || widths->Length > maxlen - 1 || options->Length > maxlen - 1)
            throw gcnew InvalidOperationException(ResourcesT::Exceptions::ColumnDefinitionStringTooLongFormat("GetDefaultView"));
        StringCopy(contents, ViewContents, maxlen);
        StringCopy(headers, ViewHeaders, maxlen);
        StringCopy(widths, ViewWidths, maxlen);
        StringCopy(options, ViewOptions, maxlen);
        return (BOOL)true;
    }
    inline ViewDefinition^ FileSystemPlugin::GetDefaultView(int){throw gcnew NotSupportedException();}

    inline int FileSystemPlugin::FsGetBackgroundFlags(void){return (int)this->BackgroundFlags;}
    inline BackgroundTransferSupport FileSystemPlugin::BackgroundFlags::get(){throw gcnew NotSupportedException();}
#pragma endregion
#pragma endregion

}}