#pragma once

#include "..\PluginMethodAttribute.h"

namespace Tools{namespace TotalCommanderT{

    /// <summary>This enumerations enumerates all Total Commander lister (WLX) plugin from TC plugin interface point-of-view</summary>
    /// <seelaso cref="ListerPlugin"/>
    /// <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    [Flags]
    public enum class WlxFunctions{
        /// <summary><see cref="ListerPlugin::ListLoad"/></summary>
        [PluginMethod("OnInit", "TC_L_LOAD")] Load = 1,
        /// <summary><see cref="ListerPlugin::LoadNext"/></summary>
        [PluginMethod("LoadNext", "TC_L_LOADNEXT")] LoadNext = 2,
        /// <summary><see cref="ListerPlugin::CloseWindow"/></summary>
        [PluginMethod("OnInit", "TC_L_LOAD")] CloseWindow = 4,
        /// <summary><see cref="ListerPlugin::DetectString"/></summary>
        [PluginMethod("get_DetectString", "TC_L_DETECTSTRING")] GetDetectString = 8,
        /// <summary><see cref="ListerPlugin::SearchText"/></summary>
        [PluginMethod("SearchText", "TC_L_SEARCHTEXT")] SearchText = 16,
        /// <summary><see cref="ListerPlugin::SendCommand"/></summary>
        [PluginMethod("SendCommand", "TC_L_SENDCOMMAND")] SendCommand = 32,
        Print = 64,
        NotificationReceived = 128,
        SetDefaultParams = 256,
        GetPreviewBitmap = 512,
        SearchDialog = 1024
    };

}}