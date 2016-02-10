#include "GlobalFunctions.h"

using namespace System;
using namespace System::ComponentModel;
#define TC Tools::TotalCommanderT


namespace Tools {
    namespace TotalCommanderT {
        inline wchar_t* GlobalFunctions::AnsiToUnicode(const char* source) { return TC::AnsiToUnicode(source); }
        inline void GlobalFunctions::AnsiToUnicode(const char* source, wchar_t* target, int maxlen) { TC::AnsiToUnicode(source, target, maxlen); }
        inline void GlobalFunctions::UnicodeToAnsi(const wchar_t* source, char* target, int maxlen) { TC::UnicodeToAnsi(source, target, maxlen); }
    }
}