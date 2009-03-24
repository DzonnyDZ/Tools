#pragma once
#include "define.h"
#include "plug_common.h"
#ifdef TC_WFX
    #include "fsplugin.h"
#endif
#ifdef TC_WDX
    #include "contplug.h" 
#endif
#ifdef TC_WLC
    #include "listplug.h"
#endif
#ifdef TC_WCX4
    #include "wxchead.h"
#endif