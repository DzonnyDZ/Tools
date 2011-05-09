//To be included in AllTCFunctions.h

#ifndef TC_L_INIT_CALL
    #define TC_L_INIT_CALL
#endif

#ifdef TC_L_LOAD
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX HWND TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    ListLoad
    #ifdef TC_FNC_HEADER
        (HWND ParentWin, wchar_t* FileToLoad, int ShowFlags)
    #endif
    #if defined(TC_FNC_BODY)
        {
            TC_L_INIT_CALL;
            return TC_FUNCTION_TARGET->TC_GETFNAME_W(ListLoad)(ParentWin, FileToLoad, ShowFlags
                #ifdef TC_A2W
                    , true
                #endif
            );
        }
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //Ansi
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX HWND TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    ListLoad
    #ifdef TC_FNC_HEADER
        (HWND ParentWin, char* FileToLoad, int ShowFlags)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)
        {return TC_FUNCTION_TARGET->ListLoad(ParentWin, FileToLoad, ShowFlags);}
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        TC_L_INIT_CALL;
        TC_FUNCTION_TARGET->ListLoad(ParentWin, GF::AnsiToUnicode(FileToLoad), ShowFlags);
    } 
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif