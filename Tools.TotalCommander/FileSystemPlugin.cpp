#include "stdafx.h"
#include "FileSystemPlugin.h"
#include "fsplugin.h"
#include "Exceptions.h"
using namespace System;
//using namespace Tools::TotalCommanderT::ResourcesT;

namespace Tools{namespace TotalCommanderT{
    //RemoteInfo
    RemoteInfo::RemoteInfo(const RemoteInfoStruct &ri){
        this->SizeLow = ri.SizeLow;
        this->SizeHigh = ri.SizeHigh;
        this->Attr = ri.Attr;
    }

    //FileSystemPlugin
#pragma region "FileSystemPlugin"
#pragma region "TC functions"
    int FileSystemPlugin::FsInit(int PluginNr,tProgressProc pProgressProc, tLogProc pLogProc,tRequestProc pRequestProc){
        throw gcnew NotImplementedException();
    }
    HANDLE FileSystemPlugin::FsFindFirst(char* Path,WIN32_FIND_DATA *FindData){
        throw gcnew NotImplementedException();
    }
    BOOL FileSystemPlugin::FsFindNext(HANDLE Hdl,WIN32_FIND_DATA *FindData){
        throw gcnew NotImplementedException();
    }
    int FileSystemPlugin::FsFindClose(HANDLE Hdl){
        throw gcnew NotImplementedException();
    }
    BOOL FileSystemPlugin::FsMkDir(char* Path){
        throw gcnew NotImplementedException();
    }
    int FileSystemPlugin::FsExecuteFile(HWND MainWin,char* RemoteName,char* Verb){
        throw gcnew NotImplementedException();
    }
    int FileSystemPlugin::FsRenMovFile(char* OldName,char* NewName,BOOL Move, BOOL OverWrite,RemoteInfoStruct* ri){
        throw gcnew NotImplementedException();
    }
    int FileSystemPlugin::FsGetFile(char* RemoteName,char* LocalName,int CopyFlags, RemoteInfoStruct* ri){
        throw gcnew NotImplementedException();
    }
    int FileSystemPlugin::FsPutFile(char* LocalName,char* RemoteName,int CopyFlags){
        throw gcnew NotImplementedException();
    }
    BOOL FileSystemPlugin::FsDeleteFile(char* RemoteName){
        throw gcnew NotImplementedException();
    }
    BOOL FileSystemPlugin::FsRemoveDir(char* RemoteName){
        throw gcnew NotImplementedException();
    }
    BOOL FileSystemPlugin::FsDisconnect(char* DisconnectRoot){
        throw gcnew NotImplementedException();
    }
    BOOL FileSystemPlugin::FsSetAttr(char* RemoteName,int NewAttr){
        throw gcnew NotImplementedException();
    }
    BOOL FileSystemPlugin::FsSetTime(char* RemoteName,FILETIME *CreationTime, FILETIME *LastAccessTime,FILETIME *LastWriteTime){
        throw gcnew NotImplementedException();
    }
    void FileSystemPlugin::FsStatusInfo(char* RemoteDir,int InfoStartEnd,int InfoOperation){
        throw gcnew NotImplementedException();
    }
    void FileSystemPlugin::FsGetDefRootName(char* DefRootName,int maxlen){
        throw gcnew NotImplementedException();
    }
#pragma endregion
#pragma region ".NET Functions"
    int FileSystemPlugin::PluginNr::get(){
        if(!this->Initialized) throw gcnew InvalidOperationException(Tools::TotalCommanderT::ResourcesT::Exceptions::PluginNotInitialized);
        return this->pluginNr;
    }
    void FileSystemPlugin::OnInit(){/*do nothing*/}
    inline bool FileSystemPlugin::Initialized::get(){return this->initialized;}
#pragma endregion
#pragma endregion
}}