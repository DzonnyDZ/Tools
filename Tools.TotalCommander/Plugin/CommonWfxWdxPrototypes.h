#ifdef TC_WFX
    #define TC_FNAME_PFX Fs
#else
    #define TC_FNAME_PFX
#endif
#ifndef GET_TC_FNAME
    #define GET_TC_FNAME(name) TC_FNAME_PFX##name
#endif

#if defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    int GET_TC_FNAME(ContentGetSupportedField)(int FieldIndex,char* FieldName, char* Units,int maxlen);
#endif      
#if defined(TC_C_GETVALUE) && defined(TC_C_GETSUPPORTEDFIELD)
    int GET_TC_FNAME(ContentGetValue)(char* FileName,int FieldIndex,int UnitIndex, void* FieldValue,int maxlen,int flags);
#endif   
#if defined(TC_C_STOPGETVALUE) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    void GET_TC_FNAME(ContentStopGetValue)(char* FileName);
#endif      
#if defined(TC_C_GETDEFAULTSORTORDER) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    int GET_TC_FNAME(ContentGetDefaultSortOrder)(int FieldIndex);
#endif      
#if defined(TC_C_PLUGINUNLOADING) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    void GET_TC_FNAME(ContentPluginUnloading)(void);
#endif   
#if defined(TC_C_GETSUPPORTEDFIELDFLAGS) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    int GET_TC_FNAME(ContentGetSupportedFieldFlags)(int FieldIndex);
#endif       
#if defined(TC_C_SETVALUE) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    int GET_TC_FNAME(ContentSetValue)(char* FileName,int FieldIndex,int UnitIndex,int FieldType, void* FieldValue,int flags);
#endif