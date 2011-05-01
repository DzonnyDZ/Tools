#pragma once
#include "Common.h"

using namespace System;
using namespace System::ComponentModel;


namespace Tools{namespace TotalCommanderT{
    /// <summary>Defines shared global functions</summary>
    /// <remarks>
    /// This class is intended onyl as workaround to C++/CLI's inability to share global functions across DLLs.
    /// Preffered way of calling functions defined in this class is to call their original implementation - the global functions.
    /// Once (if ever) I'll figure out how to workaround this C++/CLI's limitation this class'd be removed.
    /// </remarks>
    /// <version verssion="1.5.4">This class is new in version 1.5.4</version>
    [EditorBrowsable(EditorBrowsableState::Never)]
    public ref struct GlobalFunctions abstract sealed{
        /// <summary>
        /// Exposes <see cref="Tools::TotalCommander::AnsiToUnicode(char*)"/> function for languages unable to call public global functions.
        /// (Converts array of ANSI characters to array of Unicode characters)
        /// </summary>
        /// <param name="source">An array of ANSI characters to convert to Unicode</param>
        /// <returns>An array of Unicode characters. Null if <paramref name="source"/> is null</returns>
        /// <remarks>Caller is responsible for disposing returned array.
        /// <para><see cref="Tools::TotalCommander::AnsiToUnicode(char*)"/> is preffered alias of this function</para></remarks>
        /// <seealso cref="Tools::TotalCommander::AnsiToUnicode(char*)"/>
        static wchar_t* AnsiToUnicode(const char* source);
    };
}}