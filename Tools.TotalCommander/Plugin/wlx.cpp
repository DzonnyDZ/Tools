#include "define.h"
#include "Helper.h"
#ifdef TC_WLX
//This file defines Total Commander Lister (WLX) plugin
#include "listplug.h"
#include <vcclr.h>
#include "AssemblyResolver.h"
#include "Misc.h"
#include "Export.h"

using namespace System;
using namespace Tools::TotalCommanderT;

#undef TC_NAME_PREFIX 
#undef TC_FUNCTION_TARGET 
#undef TC_LINE_PREFIX
#undef TC_FUNC_MEMBEROF
#undef TC_FUNC_PREFIX_A
#undef TC_FUNC_PREFIX_B
#define TC_FNC_BODY

#ifdef TC_L_INIT_CALL
    #undef TC_L_INIT_CALL
#endif
#define TC_L_INIT_CALL Initialize()
#include "AllTCFunctions.h"
#undef TC_L_INIT_CALL

#endif