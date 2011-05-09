#pragma once

#include "..\PluginMethodAttribute.h"
#include "..\ContentFunctions.h"

using namespace System;
using namespace System::ComponentModel;

namespace Tools{namespace TotalCommanderT{

    /// <summary>This enumerations enumerates all Total Commander file system (WFX) plugin from TC plugin interface point-of-view</summary>
    /// <seelaso cref="FileSystemPlugin"/>
    /// <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    [Flags]
    public enum class WfxFunctions{
        /// <summary><see cref="FileSystemPlugin::OnInit"/></summary>
        [PluginMethod("TC_FS_INIT")] Init = 1, //2^0
        /// <summary><see cref="FileSystemPlugin::FindFirst"/></summary>
        [PluginMethod("TC_FS_FINDFIRST")] FindFirst = 2, //2^1
        /// <summary><see cref="FileSystemPlugin::FindNext"/></summary>
        [PluginMethod("TC_FS_FINDNEXT")] FsFindNext = 4, //2^2
        /// <summary><see cref="FileSystemPlugin::FindClose"/></summary>
        [PluginMethod("TC_FS_FINDCLOSE")] FindClose = 8, //2^3

        /// <summary><see cref="FileSystemPlugin::OnInitializeCryptography"/></summary>
        [PluginMethod("OnInitializeCryptography", "TC_FS_SETCRYPTCALLBACK")] SetCryptCallback = 0x10, //2^4 
        /// <summary><see cref="PluginBase::Name"/> (getter)</summary>
        [PluginMethod("TC_FS_GETDEFROOTNAME")] GetDefRootName = 0x20, //2^5
        /// <summary><see cref="FileSystemPlugin::GetFile"/></summary>
        [PluginMethod("GetFile", "TC_FS_GETFILE")] GetFile = 0x40, //2^6
        /// <summary><see cref="FileSystemPlugin::PutFile"/></summary>
        [PluginMethod("PutFile", "TC_FS_PUTFILE")] PutFile = 0x80, //2^7
        /// <summary><see cref="FileSystemPlugin::RenMovFile"/></summary>
        [PluginMethod("RenMovFile", "TC_FS_RENMOVFILE")] RenMoveFile = 0x100, //2^8
        /// <summary><see cref="FileSystemPlugin::DeleteFile"/></summary>
        [PluginMethod("DeleteFile", "TC_FS_DELETEFILE")] DeleteFile = 0x200, //2^9
        /// <summary><see cref="FileSystemPlugin::RemoveDir"/></summary>
        [PluginMethod("RemoveDir", "TC_FS_REMOVEDIR")] RemoveDir = 0x400, //2^10
        /// <summary><see cref="FileSystemPlugin::MkDir"/></summary>
        [PluginMethod("MkDir", "TC_FS_MKDIR")] MkDir = 0x800, //2^11
        /// <summary><see cref="FileSystemPlugin::ExecuteFile"/></summary>
        [PluginMethod("ExecuteFile", "TC_FS_EXECUTEFILE")] ExecuteFile = 0x1000, //2^12
        /// <summary><see cref="FileSystemPlugin::SetAttr"/></summary>
        [PluginMethod("SetAttr", "TC_FS_SETATTR")] SetAttr = 0x2000, //2^13
        /// <summary><see cref="FileSystemPlugin::SetTime"/></summary>
        [PluginMethod("SetTime", "TC_FS_SETTIME")] SetTime = 0x4000, //2^14
        /// <summary><see cref="FileSystemPlugin::Disconnect"/></summary>
        [PluginMethod("Disconnect", "TC_FS_DISCONNECT")] Disconnect = 0x8000, //2^15
        /// <summary><see cref="FileSystemPlugin::StatusInfo"/></summary>
        [PluginMethod("StatusInfo", "TC_FS_STATUSINFO")] StatusInfo = 0x10000, //2^16
        /// <summary><see cref="FileSystemPlugin::ExctractCustomIcon"/></summary>
        [PluginMethod("ExctractCustomIcon", "TC_FS_EXTRACTCUSTOMICON")] ExtractCustomIcon = 0x20000, //2^17
        /// <summary><see cref="FileSystemPlugin::SetDefaultParams"/></summary>
        [PluginMethod("TC_FS_SETDEFAULTPARAMS")] SetDefaultParams = 0x40000, //2^18
        /// <summary><see cref="FileSystemPlugin::GetPreviewBitmap"/></summary>
        [PluginMethod("GetPreviewBitmap", "TC_FS_GETPREVIEWBITMAP")] GetPreviewBitmap = 0x80000, //2^19
        /// <summary><see cref="FileSystemPlugin::get_LinksToLocalFiles"/></summary>
        [PluginMethod("get_LinksToLocalFiles", "TC_FS_LINKSTOLOCALFILES")] LiksToLocalFiles = 1<<20,
        /// <summary><see cref="FileSystemPlugin::GetLocalName"/></summary>
        [PluginMethod("GetLocalName", "TC_FS_GETLOCALNAME")] GetLocalName = 1<<21,

        /// <summary><see cref="ContentPluginBase::SupportedFields"/> (getter)</summary>
        /// <seelaso cref="ContentFunctions::GetSupportedField"/>
        [PluginMethod("get_SupportedFields","TC_C_GETSUPPORTEDFIELD")] GetSupportedField = (int)ContentFunctions::GetSupportedField,
        /// <summary><see cref="ContentPluginBase::GetValue"/></summary>
        /// <seelaso cref="ContentFunctions::GetValue"/>
        [PluginMethod("GetValue","TC_C_GETVALUE")] GetValue = (int)ContentFunctions::GetValue,
        /// <summary><see cref="ContentPluginBase::StopGetValue"/></summary>
        /// <seelaso cref="ContentFunctions::StopGetValue"/>
        [PluginMethod("StopGetValue","TC_C_STOPGETVALUE")] StopGetValue = (int)ContentFunctions::StopGetValue,
        /// <summary><see cref="ContentPluginBase::GetDefaultSortOrder"/></summary>
        /// <seelaso cref="ContentFunctions::GetDefaultSortOrder"/>
        [PluginMethod("GetDefaultSortOrder","TC_C_GETDEFAULTSORTORDER")] GetDefaultSortOrder = (int)ContentFunctions::GetDefaultSortOrder,
        /// <summary><see cref="ContentPluginBase::OnContentPluginUnloading"/></summary>
        /// <seelaso cref="ContentFunctions::PluginUnloading"/>
        [PluginMethod("OnContentPluginUnloading","TC_C_PLUGINUNLOADING")] PluginUnloading = (int)ContentFunctions::PluginUnloading,
        /// <summary><see cref="ContentPluginBase::GetSupportedFieldFlags"/></summary>
        /// <seelaso cref="ContentFunctions::GetSupportedFieldFlags"/>
        [PluginMethod("GetSupportedFieldFlags","TC_C_GETSUPPORTEDFIELDFLAGS")] GetSupportedFieldFlags = (int)ContentFunctions::GetSupportedFieldFlags,
        /// <summary><see cref="ContentPluginBase::SetValue"/></summary>
        /// <seelaso cref="ContentFunctions::SetValue"/>
        [PluginMethod("SetValue","TC_C_SETVALUE")] SetValue = (int)ContentFunctions::SetValue,

        /// <summary>Reserved for future used when <c>FsContentGetDefaultView<c/> would be implemented</summary>
        [PluginMethod("GetDefaultView", "TC_FS_GETDEFAULTVIEW")] GetDefaultView = 1<<29,
        /// <summary><see cref="FileSystemPlugin::BackgroundFlags"/> (getter)</summary>
        [PluginMethod("get_BackgroundFlags", "TC_FS_GETBACKGROUNDFLAGS")] GetBackgroundFlags = 1<<30
    };

}}