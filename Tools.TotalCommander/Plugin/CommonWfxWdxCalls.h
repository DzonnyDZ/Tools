#ifdef TC_WFX
    #define TC_FNAME_PFX_A Fs
    #ifdef TC_LAST_CALL
        #define TC_FNAME_PFX_B
    #else
        #define TC_FNAME_PFX_B Fs
    #endif
#else
    #define TC_FNAME_PFX_A
    #define TC_FNAME_PFX_B
#endif
#ifndef GET_TC_FNAME_A
    #define GET_TC_FNAME_A(name) TC_FNAME_PFX_A##name
#endif
#ifndef GET_TC_FNAME_B
    #define GET_TC_FNAME_B(name) TC_FNAME_PFX_B##name
#endif

#if defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    TCPLUGF int FUNC_MODIF GET_TC_FNAME_A(ContentGetSupportedField)(int FieldIndex,char* FieldName, char* Units,int maxlen){return FUNCTION_TARGET->GET_TC_FNAME_B(ContentGetSupportedField)(FieldIndex, FieldName, Units, maxlen);}
#endif      
#if defined(TC_C_GETVALUE) && defined(TC_C_GETSUPPORTEDFIELD)
    TCPLUGF int GET_TC_FNAME_A(ContentGetValue)(char* FileName,int FieldIndex,int UnitIndex, void* FieldValue,int maxlen,int flags){return FUNCTION_TARGET->GET_TC_FNAME_B(ContentGetValue)(FileName, FieldIndex, UnitIndex, FieldValue, maxlen, flags);}
#endif   
#if defined(TC_C_STOPGETVALUE) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    TCPLUGF void FUNC_MODIF GET_TC_FNAME_A(ContentStopGetValue)(char* FileName){return FUNCTION_TARGET->GET_TC_FNAME_B(ContentStopGetValue)(FileName);}
#endif      
#if defined(TC_C_GETDEFAULTSORTORDER) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    TCPLUGF int FUNC_MODIF GET_TC_FNAME_A(ContentGetDefaultSortOrder)(int FieldIndex){return FUNCTION_TARGET->GET_TC_FNAME_B(ContentGetDefaultSortOrder)(FieldIndex);}
#endif      
#if defined(TC_C_PLUGINUNLOADING) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    TCPLUGF void FUNC_MODIF GET_TC_FNAME_A(ContentPluginUnloading)(void){return FUNCTION_TARGET->GET_TC_FNAME_B(ContentPluginUnloading)();}
#endif   
#if defined(TC_C_GETSUPPORTEDFIELDFLAGS) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    TCPLUGF int FUNC_MODIF GET_TC_FNAME_A(ContentGetSupportedFieldFlags)(int FieldIndex){return FUNCTION_TARGET->GET_TC_FNAME_B(ContentGetSupportedFieldFlags)(FieldIndex);}
#endif       
#if defined(TC_C_SETVALUE) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    TCPLUGF int FUNC_MODIF GET_TC_FNAME_A(ContentSetValue)(char* FileName,int FieldIndex,int UnitIndex,int FieldType, void* FieldValue,int flags){return FUNCTION_TARGET->GET_TC_FNAME_B(ContentSetValue)(FileName, FieldIndex, UnitIndex, FieldType, FieldValue, flags);}
#endif