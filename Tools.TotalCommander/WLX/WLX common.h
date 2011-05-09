#pragma once

#include "..\Plugin\listplug.h"

namespace Tools{namespace TotalCommanderT{

    [Flags]
    public enum class ListerShowFlags{
        /// <summary>No other options are selected</summary>
        none = 0,
        /// <summary>Text: Word wrap mode is checked</summary>
        WrapText = lcp_wraptext,
        /// <summary>Images: Fit image to window is checked</summary>
        FitToWindow = lcp_fittowindow,
        /// <summary>Fit image to window only if larger than the window. Always set together with <see cref="FitToWindow"/>.</summary>
        FitLargerOnly = lcp_fitlargeronly,
        /// <summary>Center image in viewer window</summary>
        Center = lcp_center,
        /// <summary>ANSI charset is checked</summary>
        Ansi = lcp_ansi,
        /// <summary>ASCII (DOS) charset is checked</summary>
        Ascii = lcp_ascii,
        /// <summary>Variable width charset is checked</summary>
        Variable = lcp_variable,
        /// <summary>User chose 'Image/Multimedia' from the menu.</summary>
        /// <remarks>When this flag is ste you may attempt to load your plugin even for unsupported file type.</remarks>
        ForceShow = lcp_forceshow
    };
}}