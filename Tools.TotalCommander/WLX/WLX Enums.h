#pragma once

#include "..\Plugin\listplug.h"

using namespace System;
using namespace System::ComponentModel;

namespace Tools {
    namespace TotalCommanderT {

        /// <summary>Flags indicating various options of Lister Plugin</summary>
        /// <version version="1.5.4">This enumeration is new in version 1.5.4</version>
        [Flags]
        public enum class ListerShowFlags {
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

        /// <summary>Test serach options used by Lister plugin</summary>
        /// <version version="1.5.4">This enum is new in version 1.5.4</version>
        [Flags]
        public enum class TextSearchOptions {
            /// <summary>Search from the beginning of the first displayed line (not set: find next)</summary>
            FindFirst = lcs_findfirst,
            /// <summary>The search string is to be treated case-sensitively.</summary>
            MatchCase = lcs_matchcase,
            /// <summary>Find whole words only.</summary>
            WholeWords = lcs_wholewords,
            /// <summary>Search backwards towards the beginning of the file.</summary>
            Backwards = lcs_backwards
        };

        /// <summary>Enumerates predefined Lister plugin commands</summary>
        /// <version version="1.5.4">This enum is new in version 1.5.4</version>
        public enum class ListerCommand {
            /// <summary>Copy current selection to the clipboard</summary>
            Copy = lc_copy,
            /// <summary>New parameters passed to plugin</summary>
            NewParams = lc_newparams,
            /// <summary>Select the whole contents</summary>
            SelectAll = lc_selectall,
            /// <summary>Go to new position in document (in percent).</summary>
            SetPercent = lc_setpercent
        };

        /// <summary>Printing flags</summary>
        /// <remarks>Currently (TC 7.56a / Plugin Interface 2.0) not used. May be used in future versions of Total Commander.</remarks>
        /// <version version="1.5.4">This enum is new in version 1.5.4</version>
        [Flags, EditorBrowsable(EditorBrowsableState::Never)]
        public enum class PrintFlags {};

    }
}