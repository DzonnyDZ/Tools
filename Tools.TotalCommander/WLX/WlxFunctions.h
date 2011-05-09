#pragma once

#include "..\PluginMethodAttribute.h"

namespace Tools{namespace TotalCommanderT{

    /// <summary>This enumerations enumerates all Total Commander lister (WLX) plugin from TC plugin interface point-of-view</summary>
    /// <seelaso cref="ListerPlugin"/>
    /// <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    [Flags]
    public enum class WlxFunctions{
        /// <summary><see cref="ListerPlugin::ListLoad"/></summary>
        [PluginMethod("OnInit", "TC_L_LOAD")]
        Load = 1,
        LoadNext = 2,
        CloseWindow = 4,
        GetDetectString = 8,
        SerachText = 16,
        SendCommand = 32,
        Print = 64,
        NotificationReceived = 128,
        SetDefaultParams = 256,
        GetPreviewBitmap = 512,
        SearchDialog = 1024
    };

}}