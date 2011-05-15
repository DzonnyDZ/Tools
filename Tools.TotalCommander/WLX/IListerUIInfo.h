#pragma once

#include "..\Plugin\listplug.h"
#include "WLX Enums.h"

using namespace System;
using namespace System::ComponentModel;

namespace Tools{namespace TotalCommanderT{

    /// <summary>Provides basic information about lister plugin user interface</summary>
    /// <version version="1.5.4">This interface is new in version 1.5.4</version>
    public interface struct IListerUIInfo{
        /// <summary>Gets handle of control (window) representing plugin UI</summary>
        property IntPtr PluginWindowHandle{IntPtr get();}
        /// <summary>Gets name of file the plugin shows</summary>
        property String^ FileName{String^ get();}
        /// <summary>Gets load options of the plugin</summary>
        property ListerShowFlags Options{ListerShowFlags get();}
        /// <summary>Gets handle of parent window (the lister window this plugin UI is loaded in)</summary>
        property IntPtr ParentWindowHandle{IntPtr get();}
    };

}}