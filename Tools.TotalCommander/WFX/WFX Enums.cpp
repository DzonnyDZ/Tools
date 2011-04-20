//#include "stdafx.h"
#include "WFX Enums.h"
#include "..\Exceptions.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace Tools::TotalCommanderT::ResourcesT;
using namespace Tools::ExtensionsT;
using namespace Microsoft::VisualBasic;
using namespace System::ComponentModel;

namespace Tools{namespace TotalCommanderT{
    BitmapHandling& operator += (BitmapHandling& a, const BitmapHandling& b){
        (&a)[0] = (BitmapHandling)((int)a + (int)b);
        return a;
    }
}}