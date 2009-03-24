#include "define.h"
#include "TCHeaders.h"

#undef TC_FNC_HEADER
#undef TC_LINE_PREFIX
#undef TC_NAME_PREFIX __stdcall
#undef TC_FUNC_MEMBEROF

#undef TC_FNC_BODY
#define TC_FNC_HEADER
#define TC_LINE_PREFIX 
//__declspec(dllexport)
#define TC_NAME_PREFIX __stdcall
#define TC_FUNC_MEMBEROF
#ifdef TC_WFX
    #define TC_FUNC_PREFIX_A Fs
#else
    #undef TC_FUNC_PREFIX_A
#endif

#include "AllTCFunctions.h" 