#pragma once

#include "..\PluginMethodAttribute.h"

namespace Tools {
    namespace TotalCommanderT {

        /// <summary>This enumerations enumerates all Total Commander lister (WLX) plugin from TC plugin interface point-of-view</summary>
        /// <seelaso cref="ListerPlugin"/>
        /// <version version="1.5.4">This enumeration is new in version 1.5.4</version>
        [Flags]
        public enum class WlxFunctions {
            /// <summary><see cref="ListerPluginBase::ListLoad"/></summary>
            [PluginMethod("OnInit", "TC_L_LOAD")] Load = 1,
            /// <summary><see cref="ListerPluginBase::LoadNext"/></summary>
            [PluginMethod("LoadNext", "TC_L_LOADNEXT")] LoadNext = 2,
            /// <summary><see cref="ListerPluginBase::CloseWindow"/></summary>
            [PluginMethod("OnInit", "TC_L_LOAD")] CloseWindow = 4,
            /// <summary><see cref="ListerPluginBase::DetectString"/></summary>
            [PluginMethod("get_DetectString", "TC_L_DETECTSTRING")] GetDetectString = 8,
            /// <summary><see cref="ListerPluginBase::SearchText"/></summary>
            [PluginMethod("SearchText", "TC_L_SEARCHTEXT")] SearchText = 16,
            /// <summary><see cref="ListerPluginBase::SendCommand"/></summary>
            [PluginMethod("SendCommand", "TC_L_SENDCOMMAND")] SendCommand = 32,
            /// <summary><see cref="ListerPluginBase::Print"/></summary>
            [PluginMethod("Print", "TC_L_PRINT")] Print = 64,
            /// <summary><see cref="ListerPluginBase::NotificationReceived"/></summary>
            [PluginMethod("Print", "TC_L_NOTIFICATIONRECEIVED")] NotificationReceived = 128,
            /// <summary><see cref="ListerPluginBase::SetDefaultParams"/></summary>
            [PluginMethod("TC_L_SETDEFAULTPARAMS")] SetDefaultParams = 256,
            /// <summary><see cref="ListerPluginBase::GetPreviewBitmap"/></summary>
            [PluginMethod("GetPreviewBitmap", "TC_L_GETPREVIEWBITMAP")] GetPreviewBitmap = 512,
            /// <summary><see cref="ListeerPlugin::ShowSearchDialog"/></summary>
            [PluginMethod("ShowSearchDialog", "TC_L_SHOWSEARCHDIALOG")] SearchDialog = 1024
        };
    }
}