#include "GlobalFunctions.h"

using namespace System;
using namespace System::ComponentModel;
#define TC Tools::TotalCommanderT


namespace Tools{namespace TotalCommanderT{
    inline wchar_t* GlobalFunctions::AnsiToUnicode(const char* source){ return TC::AnsiToUnicode(source); }
}}