#pragma once
#include "Common.h"

using namespace System;
using namespace System::ComponentModel;


namespace Tools {
    namespace TotalCommanderT {
        /// <summary>Defines shared global functions</summary>
        /// <remarks>
        /// This class is intended only as workaround to C++/CLI's inability to share global functions across DLLs.
        /// Preferred way of calling functions defined in this class is to call their original implementation - the global functions.
        /// Once (if ever) I'll figure out how to workaround this C++/CLI's limitation this class'd be removed.
        /// </remarks>
        /// <version verssion="1.5.4">This class is new in version 1.5.4</version>
        [EditorBrowsable(EditorBrowsableState::Never)]
        public ref struct GlobalFunctions abstract sealed {
            /// <summary>
            /// Exposes <see cref="Tools::TotalCommander::AnsiToUnicode(const char*)"/> function for languages unable to call public global functions.
            /// (Converts array of ANSI characters to array of Unicode characters)
            /// </summary>
            /// <param name="source">An array of ANSI characters to convert to Unicode</param>
            /// <returns>An array of Unicode characters. Null if <paramref name="source"/> is null</returns>
            /// <remarks>Caller is responsible for disposing returned array.
            /// <para><see cref="Tools::TotalCommander::AnsiToUnicode(char*)"/> is preferred alias of this function</para></remarks>
            /// <seealso cref="Tools::TotalCommander::AnsiToUnicode(char*)"/>
            static wchar_t* AnsiToUnicode(const char* source);

            /// <summary>
            /// Exposes <see cref="Tools::TotalCommander::AnsiToUnicode(const char*,wchar_t*,int)"/> function for languages unable to call public global functions.
            /// (Converts array of ANSI characters to array of Unicode characters and reserves Unicode buffer of given size.)
            /// </summary>
            /// <param name="source">Pointer to an array of ANSI characters to convert to Unicode</param>
            /// <param name="maxlen">Length of buffer to reserve (including terminating nullchar). Maximally <paramref name="maxlen"/> - 1 characters from <paramref name="source"/> is copied to returned array.</param>
            /// <returns>An array of Unicode characters of length <paramref name="maxlen"/>. A nullchar is placed after last character from <paramref name="source"/>. If <paramref name="source"/> is null first char of returned array is nullchar. Null if <paramref name="maxlen"/> is &lt;= 0.</returns>
            /// <remarks>Caller is responsible for disposing returned array</remarks>
            /// <seealso cref="Tools::TotalCommander::AnsiToUnicode(const char*,wchar_t*,int)"/>
            static void AnsiToUnicode(const char* source, wchar_t* target, int maxlen);
            /// <summary>
            /// Exposes <see cref="Tools::TotalCommander::UnicodeToAnsi(const wchar_t*,char*,int)"/> function for languages unable to call public global functions.
            /// (Copies given number of characters from ANSI character buffer to Unicode character buffer.)
            /// </summary>
            /// <param name="source">Pointer to an ANSI character buffer to copy characters from. If null <paramref name="target"/>[0] is set to 0.</param>
            /// <param name="target">Pointer to a Unicode character buffer of size <paramref name="maxlen"/> to copy characters to. The buffer must be pre-initialized!</param>
            /// <param name="maxlen">Size of <paramref name="target"/> buffer. Maximally <paramref name="maxlen"/> - 1 characters from <paramref name="source"/> is copied to <paramref name="target"/>. if &lt;= 0 nothing is copied.</param>
            /// <seealso cref="Tools::TotalCommander::UnicodeToAnsi(const wchar_t*,char*,int)"/>
            static void UnicodeToAnsi(const wchar_t* source, char* target, int maxlen);
        };
    }
}