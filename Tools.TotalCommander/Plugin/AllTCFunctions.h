/*
TC_WFX, TC_WDX, TC_WLX, TC_WCX - plugin type
Single function constants
#ifdef TC_FNC_HEADER
TC_LINE_PREFIX rettype TC_NAME_PREFIX TC_FUNC_MEMBEROF
#endif
[TC_FUNC_PREFIX_A]name
#ifdef TC_FNC_HEADER
(params)
#ednif
#if defined(TC_FNC_BODY)
{return TC_FUNCTION_TARGET->[TC_FUNC_PREFIX_B]name(callparams);}
#elif defined(TC_FNC_HEADER)
;
#endif
*/
//Definitions
#ifdef TC_GETFNAME_A
    #undef TC_GETFNAME_A
#endif
#ifdef TC_GETFNAME_B
    #undef TC_GETFNAME_B
#endif
#define TC_GETFNAME_A(name) TC_FUNC_PREFIX_A##name
#define TC_GETFNAME_B(name) TC_FUNC_PREFIX_B##name
//File system  wfx
#ifdef TC_WFX
    #include "WFXFunctions.h"
#endif
//File system + Content common
#if defined(TC_WFX) || defined(TC_WDX)
    #include "WFX+WDXFunctions.h"
#endif

//Content wdx
#ifdef TC_WDX
    //TODO:WDX
#endif
//Lister wlx
#ifdef TC_WLX
    //TODO:WLX
#endif
//Pack wcx
#ifdef TC_WCX
    //TODO:WCX
#endif