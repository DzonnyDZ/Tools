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
         const Reflection::BindingFlags flags = Reflection::BindingFlags::Instance | Reflection::BindingFlags::Public | Reflection::BindingFlags::NonPublic;
         this->executeFileImplemented = Tools::TypeTools::GetAttribute<MethodNotSupportedAttribute^>(Tools::ReflectionT::ReflectionTools::GetOverridingMethod(FileSystemPlugin::typeid->GetMethod("ExecuteFile", flags),this->GetType()),false) == nullptr;
         this->ftpModeAdvertisementImplemented = Tools::TypeTools::GetAttribute<MethodNotSupportedAttribute^>(Tools::ReflectionT::ReflectionTools::GetOverridingMethod(FileSystemPlugin::typeid->GetMethod("FtpModeAdvertisement", flags),this->GetType()),false) == nullptr;
         this->openFileImplemented = Tools::TypeTools::GetAttribute<MethodNotSupportedAttribute^>(Tools::ReflectionT::ReflectionTools::GetOverridingMethod(FileSystemPlugin::typeid->GetMethod("OpenFile", flags),this->GetType()),false) == nullptr;
         this->showFileInfoImplemented = Tools::TypeTools::GetAttribute<MethodNotSupportedAttribute^>(Tools::ReflectionT::ReflectionTools::GetOverridingMethod(FileSystemPlugin::typeid->GetMethod("ShowFileInfo",flags),this->GetType()),false) == nullptr;
         this->executeCommandImplemented = Tools::TypeTools::GetAttribute<MethodNotSupportedAttribute^>(Tools::ReflectionT::ReflectionTools::GetOverridingMethod(FileSystemPlugin::typeid->GetMethod("ExecuteCommand", flags),this->GetType()),false) == nullptr;
         this->onInitializeCryptographyImplemented = Tools::TypeTools::GetAttribute<MethodNotSupportedAttribute^> (Tools::ReflectionT::ReflectionTools::GetOverridingMethod(FileSystemPlugin::typeid->GetMethod("OnInitializeCryptography", flags), this->GetType()), false) == nullptr;
         this->unicode = true;
         this->isInTotalCommander = false;
    }

    FileSystemPlugin^ FileSystemPlugin::GetPluginByNumber(int pluginNr){
        if(FileSystemPlugin::registeredPlugins->ContainsKey(pluginNr)) return FileSystemPlugin::registeredPlugins[pluginNr];
        return nullptr;
    }
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
        return this->CryptProc(showUI ? CryptMode::LoadPassword : CryptMode::LoadPasswordNoUI, connectionName, nullptr, FindData::MaxPath);
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
        if(this->onInitializeCryptographyImplemented){
            if(cryptProc == nullptr) throw gcnew ArgumentNullException("cryptProc");
            this->cryptProc = gcnew CryptProcWrapper(cryptProc);
            this->cryptoNr = cryptoNr;
            this->cryptInitialized = true;
            this->OnInitializeCryptography(flags);
        }
    }
    inline bool FileSystemPlugin::CryptInitialized::get(){return this->cryptInitialized;}
    inline int FileSystemPlugin::CryptoNr::get(){return this->cryptoNr;}
    inline void FileSystemPlugin::OnInitializeCryptography(CryptFlags flags){ throw gcnew NotSupportedException();}
#pragma endregion

    //MkDir
    BOOL FileSystemPlugin::FsMkDir(char* Path){
        Exception^ ex=nullptr;
        try{
            return this->MkDir(gcnew String(Path));
        }catch(IO::IOException^ ex__){ex=ex__;}
        catch(Security::SecurityException^ ex__){ex=ex__;}
        catch(UnauthorizedAccessException^ ex__){ex=ex__;}
        if(ex!=nullptr) return false; 
        return true;
    }
    inline bool FileSystemPlugin::MkDir(String^ Path){ throw gcnew NotSupportedException(); }

    //ExecuteFile
    int FileSystemPlugin::FsExecuteFile(HWND MainWin,char* RemoteName,char* Verb){
        Exception^ ex=nullptr;
        String^% remoteName = gcnew String(RemoteName);
        try{
            ExecExitCode ret =  this->ExecuteFile((IntPtr)MainWin, remoteName, gcnew String(Verb));
            String^ old = gcnew String(RemoteName);
            if(old != remoteName) {
                if(remoteName->Length > FindData::MaxPath-1) throw gcnew IO::PathTooLongException(Exceptions::ParamAssignedTooLongFormat("RemoteName","ExecuteFile"));
                StringCopy(remoteName,RemoteName,FindData::MaxPath);
            }
            return (int) ret;
        }catch(InvalidOperationException^ ex__){ ex=ex__;
        }catch(IO::IOException^ ex__){ex=ex__;}catch(Security::SecurityException^ ex__){ex=ex__;}catch(UnauthorizedAccessException^ ex__){ex=ex__;} if(ex!=nullptr){}
        return (int) ExecExitCode::Error;
    }
    inline int FileSystemPlugin::FsExecuteFile(HANDLE MainWin,char* RemoteName,char* Verb){return this->FsExecuteFile((HWND)MainWin, RemoteName, Verb);}
    inline ExecExitCode FileSystemPlugin::ExecuteFile(IntPtr hMainWin, String^% RemoteName, String^ Verb){
        if(!executeFileImplemented || (!ftpModeAdvertisementImplemented && !openFileImplemented && !showFileInfoImplemented && !executeCommandImplemented)) throw gcnew NotSupportedException();
        if(Verb->ToLower()->StartsWith("mode ") && ftpModeAdvertisementImplemented){
            return this->FtpModeAdvertisement(hMainWin,RemoteName,Verb->Substring(5));
        }else if(Verb->ToLower() == "open" && openFileImplemented){
            return this->OpenFile(hMainWin,RemoteName);
        }else if(Verb->ToLower() == "properties" && showFileInfoImplemented){
            return this->ShowFileInfo(hMainWin, RemoteName);
        }else if(Verb->ToLower()->StartsWith("quote ") && executeCommandImplemented){
            return this->ExecuteCommand(hMainWin, RemoteName, Verb->Substring(6));
        }else return ExecExitCode::Error;
    }
#pragma region "ExecuteFile helper methods"
        ExecExitCode FileSystemPlugin::FtpModeAdvertisement(IntPtr hMainWin, String^ RemoteName, String^ mode){throw gcnew NotSupportedException();}
        ExecExitCode FileSystemPlugin::OpenFile(IntPtr hMainWin, String^% RemoteName){throw gcnew NotSupportedException();}
        ExecExitCode FileSystemPlugin::ShowFileInfo(IntPtr hMainWin, String^ RemoteName){throw gcnew NotSupportedException();}
        ExecExitCode FileSystemPlugin::ExecuteCommand(IntPtr hMainWin, String^% RemoteName, String^ command){throw gcnew NotSupportedException();}
#pragma endregion
    //RenMovFile
    int FileSystemPlugin::FsRenMovFile(char* OldName,char* NewName,BOOL Move, BOOL OverWrite,RemoteInfoStruct* ri){
        try{
            return (int)this->RenMovFile(gcnew String(OldName), gcnew String(NewName), Move==0?false:true, OverWrite==0?false:true, RemoteInfo(*ri));
        }catch(UnauthorizedAccessException^){ return (int)FileSystemExitCode::ReadError; }
        catch(Security::SecurityException^){ return (int)FileSystemExitCode::ReadError; }
        catch(IO::FileNotFoundException^){ return (int)FileSystemExitCode::FileNotFound; }
        catch(IO::DirectoryNotFoundException^){ return (int)FileSystemExitCode::WriteError; }
        catch(IO::IOException^){ return (int)FileSystemExitCode::ReadError; }
        catch(InvalidOperationException^){ return (int)FileSystemExitCode::NotSupported; }
    }
    inline FileSystemExitCode FileSystemPlugin::RenMovFile(String^ OldName, String^ NewName, bool move, bool OverWrite, RemoteInfo info){ throw gcnew NotSupportedException(); }
    
    //GetFile
    int FileSystemPlugin::FsGetFile(wchar_t* remoteName, wchar_t* localName, int copyFlags, RemoteInfoStruct* ri){
        String^% ln = gcnew String(localName);
        try{
            FileSystemExitCode ret = this->GetFile(gcnew String(remoteName), ln, (CopyFlags)copyFlags, RemoteInfo(*ri));
            String^ old = gcnew String(localName);
            if(old != ln){
                if(ln->Length >= FindData::MaxPath) throw gcnew IO::PathTooLongException(Exceptions::ParamAssignedTooLongFormat("localName", "GetFile"));
                StringCopy(ln, localName, FindData::MaxPath);
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
    inline FileSystemExitCode FileSystemPlugin::GetFile(String^ remoteName, String^% localName, CopyFlags copyFlags, RemoteInfo info){ throw gcnew NotSupportedException(); }
    
    //PutFile
    int FileSystemPlugin::FsPutFile(char* LocalName,char* RemoteName,int CopyFlags){
        String^% remoteName = gcnew String(RemoteName);
        try{
            FileSystemExitCode ret = this->PutFile(gcnew String(LocalName), remoteName, (Tools::TotalCommanderT::CopyFlags)CopyFlags);
            String^ old = gcnew String(RemoteName);
            if(old != remoteName){
                if(remoteName->Length >= FindData::MaxPath) throw gcnew IO::PathTooLongException(Exceptions::ParamAssignedTooLongFormat("RemoteName","PutFile"));
                StringCopy(remoteName,RemoteName, FindData::MaxPath);
            }
            return (int) ret;
        }catch(UnauthorizedAccessException^){ return (int)FileSystemExitCode::ReadError; }
        catch(Security::SecurityException^){ return (int)FileSystemExitCode::ReadError; }
        catch(IO::FileNotFoundException^){ return (int)FileSystemExitCode::FileNotFound; }
        catch(IO::DirectoryNotFoundException^){ return (int)FileSystemExitCode::WriteError; }
        catch(IO::IOException^){ return (int)FileSystemExitCode::ReadError; }
        catch(InvalidOperationException^){ return (int)FileSystemExitCode::NotSupported; }
    }
    inline FileSystemExitCode FileSystemPlugin::PutFile(String^ LocalName, String^% RemoteName, CopyFlags CopyFlags){ throw gcnew NotSupportedException(); }
    //Delete file
    BOOL FileSystemPlugin::FsDeleteFile(char* RemoteName){
        try{ return this->DeleteFile(gcnew String(RemoteName)); }
        catch(UnauthorizedAccessException^){ return false; }
        catch(Security::SecurityException^){ return false; }
        catch(IO::IOException^){ return false; }
    }
    inline bool FileSystemPlugin::DeleteFile(String^ RemoteName){ throw gcnew NotSupportedException(); }
    //RemoveDir
    BOOL FileSystemPlugin::FsRemoveDir(char* RemoteName){
        try{ return this->RemoveDir(gcnew String(RemoteName)); }
        catch(UnauthorizedAccessException^){ return false; }
        catch(Security::SecurityException^){ return false; }
        catch(IO::IOException^){ return false; }
    }
    inline bool FileSystemPlugin::RemoveDir(String^ RemoteName){ throw gcnew NotSupportedException(); }
    //Disconect
    inline BOOL FileSystemPlugin::FsDisconnect(char* DisconnectRoot){
        return this->Disconnect(gcnew String(DisconnectRoot));
    }
    inline bool FileSystemPlugin::Disconnect(String^ DisconnectRoot){ throw gcnew NotSupportedException(); }
    //SetAttr
    BOOL FileSystemPlugin::FsSetAttr(char* RemoteName,int NewAttr){
        try{
            this->SetAttr(gcnew String(RemoteName), (StandardFileAttributes) NewAttr);}
        catch(UnauthorizedAccessException^){ return false; }
        catch(Security::SecurityException^){ return false; }
        catch(IO::IOException^){ return false; }
        return true;
    }
    inline void FileSystemPlugin::SetAttr(String^ RemoteName, StandardFileAttributes NewAttr){ throw gcnew NotSupportedException(); }
    //SetTime
    BOOL FileSystemPlugin::FsSetTime(char* RemoteName,::FILETIME *CreationTime, ::FILETIME *LastAccessTime,::FILETIME *LastWriteTime){
        Nullable<DateTime> create = Nullable<DateTime>();
        Nullable<DateTime> access = Nullable<DateTime>();
        Nullable<DateTime> write = Nullable<DateTime>();
        if(CreationTime!=nullptr) create = Nullable<DateTime>(FileTimeToDateTime(*CreationTime));
        if(LastAccessTime!=nullptr) access = Nullable<DateTime>(FileTimeToDateTime(*LastAccessTime));
        if(LastWriteTime!=nullptr) write = Nullable<DateTime>(FileTimeToDateTime(*LastWriteTime));
        try{ this->SetTime(gcnew String(RemoteName), create, access, write);}
        catch(UnauthorizedAccessException^){ return false; }
        catch(Security::SecurityException^){ return false; }
        catch(IO::IOException^){ return false; }
        return true;
    }
    inline void FileSystemPlugin::SetTime(String^ RemoteName, Nullable<DateTime> CreationTime, Nullable<DateTime> LastAccessTime, Nullable<DateTime> LastWriteTime){ throw gcnew NotSupportedException(); }
    //StatusInfo
    void FileSystemPlugin::FsStatusInfo(char* RemoteDir,int InfoStartEnd,int InfoOperation){
        this->StatusInfo(gcnew String(RemoteDir),(OperationStatus)InfoStartEnd,(OperationKind)InfoOperation);     
    }
    void FileSystemPlugin::StatusInfo(String^ RemoteDir,OperationStatus InfoStartEnd,OperationKind InfoOperation){
        OperationEventArgs^ e = gcnew OperationEventArgs(RemoteDir,InfoOperation, InfoStartEnd);
        if(e->Status == OperationStatus::Start) this->OnOperationStarting(e);
        else if(e->Status == OperationStatus::End) this->OnOperationFinished(e);
        this->OnOperationStatusChanged(e);
    }
    void FileSystemPlugin::OnOperationStatusChanged(OperationEventArgs^ e){/*Do nothing*/}
    void FileSystemPlugin::OnOperationStarting(OperationEventArgs^ e){/*Do nothing*/}
    void FileSystemPlugin::OnOperationFinished(OperationEventArgs^ e){/*Do nothing*/}
    //GetDefRootName
    void FileSystemPlugin::FsGetDefRootName(char* DefRootName,int maxlen){
        StringCopy(this->Name,DefRootName,maxlen);
    }

    int FileSystemPlugin::FsExtractCustomIcon(char* RemoteName,int ExtractFlags,HICON* TheIcon){
        String^ remoteName = gcnew String(RemoteName);
        Drawing::Icon^ icon = nullptr;
        IconExtractResult ret = this->ExctractCustomIcon(remoteName, (IconExtractFlags)ExtractFlags, icon);
        String^ old = gcnew String(RemoteName);
        if(old != remoteName){
            if(remoteName->Length > FindData::MaxPath - 1) throw gcnew IO::PathTooLongException(ResourcesT::Exceptions::PathTooLong);
            StringCopy(remoteName,RemoteName,FindData::MaxPath);
        }
        if(icon != nullptr) TheIcon[0] = (HICON)(Int32)icon->Handle; else TheIcon[0] = NULL;
        return (int)ret;
    }
    inline IconExtractResult FileSystemPlugin::ExctractCustomIcon(String^% RemoteName, IconExtractFlags ExtractFlags, Drawing::Icon^% TheIcon){ throw gcnew NotSupportedException(); }

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

    int FileSystemPlugin::FsGetPreviewBitmap(char* RemoteName,int width,int height, HBITMAP* ReturnedBitmap){
        BitmapResult^ bmp = this->GetPreviewBitmap(gcnew String(RemoteName), width, height);
        if(bmp == nullptr){
            return (int)BitmapHandling::None;
        }
        if(bmp->ImageKey != nullptr){
            StringCopy(bmp->ImageKey,RemoteName,FindData::MaxPath);
        }
        if(bmp->Image != nullptr) 
            ReturnedBitmap[0] = (HBITMAP)(int)bmp->Image->GetHbitmap();
        else ReturnedBitmap[0] = nullptr;
        return (int)bmp->GetFlag();
    }
    inline BitmapResult^ FileSystemPlugin::GetPreviewBitmap(String^ RemoteName, int width, int height){throw gcnew NotSupportedException();}
    
    inline BOOL FileSystemPlugin::FsLinksToLocalFiles(void){ return this->LinksToLocalFiles ? 1 : 0;}
    inline bool FileSystemPlugin::LinksToLocalFiles::get(){throw gcnew NotSupportedException();} 

    BOOL FileSystemPlugin::FsGetLocalName(char* RemoteName,int maxlen){
        String^ ret = this->GetLocalName(gcnew String(RemoteName), maxlen - 1);
        if(ret != nullptr){
            if(ret->Length > FindData::MaxPath-1) throw gcnew IO::PathTooLongException(Exceptions::PathTooLong);
            StringCopy(ret,RemoteName, FindData::MaxPath);
            return TRUE;
        }
        return FALSE;
    }
    inline String^ FileSystemPlugin::GetLocalName(String^ RemoteName, int maxlen){throw gcnew NotSupportedException();}

    BOOL FileSystemPlugin::FsContentGetDefaultView(char* ViewContents,char* ViewHeaders,char* ViewWidths,char* ViewOptions,int maxlen){
        ViewDefinition^ vd = GetDefaultView(maxlen-1);
        if(vd == nullptr) return (BOOL)false;
        String^ contents = vd->GetColumnSourcesString();
        String^ headers = vd->GetNamesString();
        String^ widths = vd->GetWidthsString();
        String^ options = vd->GetOptionsString();
        if(contents->Length > maxlen-1 || headers->Length > maxlen-1 || widths->Length > maxlen-1 || options->Length > maxlen-1)
            throw gcnew InvalidOperationException(ResourcesT::Exceptions::ColumnDefinitionStringTooLongFormat("GetDefaultView"));
        StringCopy(contents,ViewContents,maxlen);
        StringCopy(headers,ViewHeaders,maxlen);
        StringCopy(widths,ViewWidths,maxlen);
        StringCopy(options,ViewOptions,maxlen);
        return (BOOL)true;
    }
    inline ViewDefinition^ FileSystemPlugin::GetDefaultView(int maxlen){throw gcnew NotSupportedException();}

    inline int FileSystemPlugin::FsGetBackgroundFlags(void){return (int)this->BackgroundFlags;}
    inline BackgroundTransferSupport FileSystemPlugin::BackgroundFlags::get(){throw gcnew NotSupportedException();}
#pragma endregion
#pragma endregion

}}