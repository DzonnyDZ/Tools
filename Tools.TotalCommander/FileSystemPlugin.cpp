//#include "stdafx.h"
#include "FileSystemPlugin.h"
#include "Plugin\fsplugin.h"
#include "Exceptions.h"
#include <vcclr.h>

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace Tools::TotalCommanderT::ResourcesT;
using namespace Tools::ExtensionsT;
using namespace Microsoft::VisualBasic;

namespace Tools{namespace TotalCommanderT{
    //Global functions
    inline Nullable<DateTime> FileTimeToDateTime(::FILETIME value){
        if(value.dwHighDateTime == 0xFFFFFFFF && value.dwLowDateTime == 0xFFFFFFFE) return Nullable<DateTime>();
        return DateTime(1601,1,1).AddTicks(*(__int64*)(void*)&value);
    }
    ::FILETIME DateTimeToFileTime(Nullable<DateTime> value){
        ::FILETIME ret;
        if(value.HasValue){
            ret.dwLowDateTime = Numbers::Low(value.Value.ToFileTime());
            ret.dwHighDateTime = Numbers::High(value.Value.ToFileTime());
        }else{
            ret.dwLowDateTime = 0xFFFFFFFE;
            ret.dwHighDateTime = 0xFFFFFFFF;
        }
        return ret;
    }
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
    //FileSystemPlugin
#pragma region "FileSystemPlugin"
    FileSystemPlugin::FileSystemPlugin(){
         this->handleDictionary = gcnew Collections::Generic::Dictionary<int,Object^>();
         this->MaxHandle = 0;
         this->HandleSyncObj = gcnew Object();
         const Reflection::BindingFlags flags = Reflection::BindingFlags::Instance | Reflection::BindingFlags::Public | Reflection::BindingFlags::NonPublic;
         this->ExecuteFileImplemented = Tools::TypeTools::GetAttribute<MethodNotSupportedAttribute^>(Tools::ReflectionT::ReflectionTools::GetOverridingMethod(FileSystemPlugin::typeid->GetMethod("ExecuteFile", flags),this->GetType()),false) == nullptr;
         this->FtpModeAdvertisementImplemented = Tools::TypeTools::GetAttribute<MethodNotSupportedAttribute^>(Tools::ReflectionT::ReflectionTools::GetOverridingMethod(FileSystemPlugin::typeid->GetMethod("FtpModeAdvertisement", flags),this->GetType()),false) == nullptr;
         this->OpenFileImplemented = Tools::TypeTools::GetAttribute<MethodNotSupportedAttribute^>(Tools::ReflectionT::ReflectionTools::GetOverridingMethod(FileSystemPlugin::typeid->GetMethod("OpenFile", flags),this->GetType()),false) == nullptr;
         this->ShowFileInfoImplemented = Tools::TypeTools::GetAttribute<MethodNotSupportedAttribute^>(Tools::ReflectionT::ReflectionTools::GetOverridingMethod(FileSystemPlugin::typeid->GetMethod("ShowFileInfo",flags),this->GetType()),false) == nullptr;
         this->ExecuteCommandImplemented = Tools::TypeTools::GetAttribute<MethodNotSupportedAttribute^>(Tools::ReflectionT::ReflectionTools::GetOverridingMethod(FileSystemPlugin::typeid->GetMethod("ExecuteCommand", flags),this->GetType()),false) == nullptr;
    }
#pragma region "TC functions"
    int FileSystemPlugin::FsInit(int PluginNr,tProgressProc pProgressProc, tLogProc pLogProc,tRequestProc pRequestProc){
        this->pluginNr = PluginNr;
        this->pProgressProc = pProgressProc;
        this->pLogProc  = pLogProc;
        this->pRequestProc = pRequestProc;
        this->initialized = true;
        this->OnInit();
        return 0;
    }
    void FileSystemPlugin::InitializePlugin(int PluginNr, ProgressCallback^ progress, LogCallback^ log, RequestCallback^ request){
        if(progress == nullptr) throw gcnew ArgumentNullException("progress");
        if(log == nullptr) throw gcnew ArgumentNullException("log");
        if(request == nullptr) throw gcnew ArgumentNullException("request");
        this->pluginNr = PluginNr;
        this->dProgressProc = progress;
        this->dLogProc  = log;
        this->dRequestProc = request;
        this->initialized = true;
        this->OnInit();
    }
    bool FileSystemPlugin::IsInTotalCommander::get(){
        if(!this->Initialized) throw gcnew InvalidOperationException(Exceptions::PluginNotInitialized);
        return this->dProgressProc == nullptr;
    }
    HANDLE FileSystemPlugin::FsFindFirst(char* Path,WIN32_FIND_DATA *FindData){
        Tools::TotalCommanderT::FindData% findData = Tools::TotalCommanderT::FindData();// *gcnew Tools::TotalCommanderT::FindData(*FindData);
        Object^ object;
        Exception^ exception = nullptr;
        try{
            object = this->FindFirst(gcnew String(Path),findData);
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
            findData.Populate(*FindData);
            return (HANDLE)handle;
        }
    }
    BOOL FileSystemPlugin::FsFindNext(HANDLE Hdl,WIN32_FIND_DATA *FindData){
        Tools::TotalCommanderT::FindData% findData = *gcnew Tools::TotalCommanderT::FindData(*FindData);
        Object^ object = this->HandleGet((int) Hdl);
        Exception^ exception = nullptr;
        bool ret;
        try{
            ret = this->FindNext(object,findData);
        }catch(IO::DirectoryNotFoundException^ ex){exception=ex;
        }catch(UnauthorizedAccessException^ ex){exception=ex;
        }catch(Security::SecurityException^ ex){exception=ex;
        }catch(IO::IOException^ ex){exception=ex;}
        if(exception != nullptr || !ret) return false;
        else{
            findData.Populate(*FindData);
            return true;
        }
    }
    int FileSystemPlugin::FsFindClose(HANDLE Hdl){
        Object^ object = this->HandleGet((int) Hdl);
        this->FindClose(object);
        this->HandleRemove((int) Hdl);
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
        if(this->IsInTotalCommander){
            char* sourceName = (char*)(void*)Marshal::StringToHGlobalAnsi(SourceName);
            char* targetName = (char*)(void*)Marshal::StringToHGlobalAnsi(TargetName);
            bool ret = this->pProgressProc(this->PluginNr,sourceName,targetName,PercentDone) == 1 ? true : false;
            Marshal::FreeHGlobal((IntPtr)sourceName);
            Marshal::FreeHGlobal((IntPtr)targetName);
            return ret;
        }else{
            return this->dProgressProc(this, SourceName, TargetName, PercentDone);
        }
    }
    void FileSystemPlugin::LogProc(LogKind MsgType,String^ LogString){
        if(!this->Initialized) throw gcnew InvalidOperationException(Exceptions::PluginNotInitialized);
        if(this->IsInTotalCommander){
            char* logString = (char*)(void*)Marshal::StringToHGlobalAnsi(LogString);
            this->pLogProc(this->PluginNr,(int)MsgType,logString);
            Marshal::FreeHGlobal((IntPtr)logString);
        }else{
            this->dLogProc(this, MsgType, LogString);
        }
    }
    String^ FileSystemPlugin::RequestProc(InputRequestKind RequestType,String^ CustomTitle, String^ CustomText, String^ DefaultText, int maxlen){
        if(!this->Initialized) throw gcnew InvalidOperationException(Exceptions::PluginNotInitialized);
        if(this->IsInTotalCommander){
            if(DefaultText->Length > maxlen) throw gcnew ArgumentException(Exceptions::DefaultTextTooLong);
            if(maxlen < 1) throw gcnew ArgumentOutOfRangeException("maxlen");
            char* customTitle = (char*)(void*)Marshal::StringToHGlobalAnsi(CustomTitle);
            char* customText = (char*)(void*)Marshal::StringToHGlobalAnsi(CustomText);
            char* defaultText = new char[maxlen];
            if(DefaultText != nullptr)
                for(int i = 0; i < DefaultText->Length; i++)
                    defaultText[i] = (char)DefaultText[i];
            Marshal::FreeHGlobal((IntPtr)customTitle);
            Marshal::FreeHGlobal((IntPtr)customText);
            if(this->pRequestProc(this->PluginNr, (int)RequestType, customTitle, customText, defaultText, maxlen)){
                String^ ret = gcnew String(defaultText);
                delete defaultText;
                return ret;
            }
            delete defaultText;
            return nullptr;
        }else{
            return this->dRequestProc(this, RequestType, CustomTitle, CustomText, DefaultText, maxlen);
        }
    }
#pragma endregion
    inline void FileSystemPlugin::FindClose(Object^ Status){/*do nothing*/}
#pragma endregion
#pragma region "Optional functions"

    //MkDir
    BOOL FileSystemPlugin::FsMkDir(char* Path){
        Exception^ ex=nullptr;
        try{
            return this->MkDir(gcnew String(Path));
        }catch(IO::IOException^ ex__){ex=ex__;}catch(Security::SecurityException^ ex__){ex=ex__;}catch(UnauthorizedAccessException^ ex__){ex=ex__;} if(ex!=nullptr){
            return false;
        }
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
        if(!ExecuteFileImplemented || (!FtpModeAdvertisementImplemented && !OpenFileImplemented && !ShowFileInfoImplemented && !ExecuteCommandImplemented)) throw gcnew NotSupportedException();
        if(Verb->ToLower()->StartsWith("mode ") && FtpModeAdvertisementImplemented){
            return this->FtpModeAdvertisement(hMainWin,RemoteName,Verb->Substring(5));
        }else if(Verb->ToLower() == "open" && OpenFileImplemented){
            return this->OpenFile(hMainWin,RemoteName);
        }else if(Verb->ToLower() == "properties" && ShowFileInfoImplemented){
            return this->ShowFileInfo(hMainWin, RemoteName);
        }else if(Verb->ToLower()->StartsWith("quote ") && ExecuteCommandImplemented){
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
    int FileSystemPlugin::FsGetFile(char* RemoteName,char* LocalName,int CopyFlags, RemoteInfoStruct* ri){
        String^% localName = gcnew String(LocalName);
        try{
            FileSystemExitCode ret = this->GetFile(gcnew String(RemoteName), localName, (Tools::TotalCommanderT::CopyFlags)CopyFlags, RemoteInfo(*ri));
            String^ old = gcnew String(LocalName);
            if(old != localName){
                if(localName->Length >= FindData::MaxPath) throw gcnew IO::PathTooLongException(Exceptions::ParamAssignedTooLongFormat("LocalName","GetFile"));
                StringCopy(localName,LocalName,FindData::MaxPath);
            }
            return (int) ret;
        }catch(UnauthorizedAccessException^){ return (int)FileSystemExitCode::ReadError; }
        catch(Security::SecurityException^){ return (int)FileSystemExitCode::ReadError; }
        catch(IO::FileNotFoundException^){ return (int)FileSystemExitCode::FileNotFound; }
        catch(IO::DirectoryNotFoundException^){ return (int)FileSystemExitCode::WriteError; }
        catch(IO::IOException^){ return (int)FileSystemExitCode::ReadError; }
        catch(InvalidOperationException^){ return (int)FileSystemExitCode::NotSupported; }
    }
    inline FileSystemExitCode FileSystemPlugin::GetFile(String^ RemoteName, String^% LocalName, CopyFlags CopyFlags, RemoteInfo info){ throw gcnew NotSupportedException(); }
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
    inline void FileSystemPlugin::SetDefaultParams(DefaultParams dps){throw gcnew NotSupportedException();}

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
#pragma endregion
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
    [EditorBrowsableAttribute(EditorBrowsableState::Never)]
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
    void StringCopy(String^ source, char* target, int maxlen){
        /*if(source == nullptr)
            target[0]=0;
        else{
            for(int i = 0; i < source->Length && i < maxlen-1; i++)
                target[i]=source[i];
            target[source->Length > maxlen-1 ? maxlen-1 : source->Length] = 0;
        }*/
        if(source == nullptr)
            target[0]=0;
        else{
            System::Text::Encoding^ enc = System::Text::Encoding::Default;
            cli::array<unsigned char>^ bytes = enc->GetBytes(source);
            for(int i = 0; i < bytes->Length && i < maxlen-1; i++)
                target[i]= bytes[i];
            target[source->Length > maxlen-1 ? maxlen-1 : source->Length] = 0;
        }
    }
    void StringCopy(String^ source, wchar_t* target, int maxlen){
        StringCopy(source,(char*)(void*)target,maxlen);
    }
}}