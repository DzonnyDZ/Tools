//To be included in AllTCFunctions.h

#if defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    TC_GETFNAME_A(ContentGetSupportedField)
    #ifdef TC_FNC_HEADER
        (int FieldIndex,char* FieldName, char* Units,int maxlen)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_B(ContentGetSupportedField)(FieldIndex, FieldName, Units, maxlen);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif      

#if defined(TC_C_GETVALUE) && defined(TC_C_GETSUPPORTEDFIELD)
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    TC_GETFNAME_A(ContentGetValueW)
    #ifdef TC_FNC_HEADER
        (char* FileName, int FieldIndex, int UnitIndex, void* FieldValue, int maxlen, int flags)
    #endif
    #if defined(TC_FNC_BODY)
        {
            return TC_FUNCTION_TARGET->TC_GETFNAME_BW(ContentGetValue)(
                FileName, FieldIndex, UnitIndex, FieldValue, maxlen, flags
                #ifdef TC_A2W
                    , true
                #endif
            );
        }
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    TC_GETFNAME_A(ContentGetValue)
    #ifdef TC_FNC_HEADER
        (char* FileName,int FieldIndex,int UnitIndex, void* FieldValue, int maxlen, int flags)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)                      
        {return TC_FUNCTION_TARGET->TC_GETFNAME_B(ContentGetValue)(FileName, FieldIndex, UnitIndex, FieldValue, maxlen, flags);}
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        TC_FUNCTION_TARGET->TC_GETFNAME_BW(ContentGetValue)(GF::AnsiToUnicode(FileName), FileIndex, UnitIndex, FieldValue, maxlen, flags, false);
    }  
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif   

#if defined(TC_C_STOPGETVALUE) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    TC_GETFNAME_A(ContentStopGetValueW)
    #ifdef TC_FNC_HEADER
        (wchar_t* FileName)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_BW(ContentStopGetValue)(FileName);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    ///ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    TC_GETFNAME_A(ContentStopGetValue)
    #ifdef TC_FNC_HEADER
        (char* FileName)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)                      
        {return TC_FUNCTION_TARGET->TC_GETFNAME_B(ContentStopGetValue)(FileName);}
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        return TC_FUNCTION_TARGET->TC_GETFNAME_BW(ContentStopGetValue)(GF::AnsiToUnicode(FileName));
    }
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif   

#if defined(TC_C_GETDEFAULTSORTORDER) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    TC_GETFNAME_A(ContentGetDefaultSortOrder)
    #ifdef TC_FNC_HEADER
        (int FieldIndex)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_B(ContentGetDefaultSortOrder)(FieldIndex);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif      

#if defined(TC_C_PLUGINUNLOADING) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    TC_GETFNAME_A(ContentPluginUnloading)
    #ifdef TC_FNC_HEADER
        (void)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_B(ContentPluginUnloading)();}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif   
#if defined(TC_C_GETSUPPORTEDFIELDFLAGS) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    TC_GETFNAME_A(ContentGetSupportedFieldFlags)
    #ifdef TC_FNC_HEADER
        (int FieldIndex)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_B(ContentGetSupportedFieldFlags)(FieldIndex);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif       

#if defined(TC_C_SETVALUE) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    TC_GETFNAME_A(ContentSetValue)
    #ifdef TC_FNC_HEADER
        (char* FileName, int FieldIndex, int UnitIndex, int FieldType, void* FieldValue, int flags)
    #endif
    #if defined(TC_FNC_BODY)
        {
            return TC_FUNCTION_TARGET->TC_GETFNAME_BW(ContentSetValue)(
                FileName, FieldIndex, UnitIndex, FieldType, FieldValue, flags
                #ifdef TC_A2W
                    , true
                #endif
            );
        }
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //ANSI
        #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    TC_GETFNAME_A(ContentSetValue)
    #ifdef TC_FNC_HEADER
        (char* FileName,int FieldIndex,int UnitIndex,int FieldType, void* FieldValue,int flags)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)                      
        {return TC_FUNCTION_TARGET->TC_GETFNAME_B(ContentSetValue)(FileName, FieldIndex, UnitIndex, FieldType, FieldValue, flags);}
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        return TC_FUNCTION_TARGET->TC_GETFNAME_BW(ContentSetValue)(GF::AnsiToUnicode(FileName), FieldIndex, UnitIndex, FiledType, FiledValue, flags, false);
    } 
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif