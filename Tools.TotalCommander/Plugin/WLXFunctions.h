//To be included in AllTCFunctions.h

#ifndef TC_L_INIT_CALL
    #define TC_L_INIT_CALL
#endif

#ifdef TC_L_LOAD
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX HWND TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    ListLoadW
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
    //ANSI
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
        return TC_FUNCTION_TARGET->ListLoad(ParentWin, GF::AnsiToUnicode(FileToLoad), ShowFlags);
    } 
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
        
#ifdef TC_L_LOADNEXT
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    ListLoadNextW
    #ifdef TC_FNC_HEADER
        (HWND ParentWin, HWND ListWin, wchar_t* FileToLoad, int ShowFlags)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_W(ListLoad)(ParentWin, ListWin, FileToLoad, ShowFlags);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    ListLoad
    #ifdef TC_FNC_HEADER
        (HWND ParentWin, HWND ListWin, char* FileToLoad, int ShowFlags)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)
        {return TC_FUNCTION_TARGET->ListLoad(ParentWin, ListWin, FileToLoad, ShowFlags);}
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        TC_L_INIT_CALL;
        return TC_FUNCTION_TARGET->ListLoad(ParentWin, ListWin, GF::AnsiToUnicode(FileToLoad), ShowFlags);
    } 
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif

#ifdef TC_L_LOAD
    //ANSI only
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    ListCloseWindow
    #ifdef TC_FNC_HEADER
        (HWND ListWin)
    #endif
    #if defined(TC_FNC_BODY)
        {TC_FUNCTION_TARGET->ListCloseWindow(ListWin)
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif

#ifdef TC_L_DETECTSTRING
    //ANSI only
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    ListGetDetectString
    #ifdef TC_FNC_HEADER
        (char* DetectString, int maxlen)
    #endif
    #if defined(TC_FNC_BODY)
        {TC_FUNCTION_TARGET->ListGetDetectString(DetectString, maxlen)
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
               
#ifdef TC_L_SEARCHTEXT
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    ListSearchTextW
    #ifdef TC_FNC_HEADER
        (HWND ListWin, wchar_t* SearchString, int SearchParameter)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_W(ListSearchText)(ListWin, SerachString, SearchParameter);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    ListLoad
    #ifdef TC_FNC_HEADER
        (HWND ListWin,char* SearchString,int SearchParameter)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)
        {return TC_FUNCTION_TARGET->ListSearchText(ListWin, SerachString, SearchParameter);}
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        TC_L_INIT_CALL;
        return TC_FUNCTION_TARGET->ListLoad(ListWin, GF::AnsiToUnicode(SerachString), SearchParameter);
    } 
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif

#ifdef TC_L_SENDCOMMAND
    //ANSI only
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    ListSendCommand
    #ifdef TC_FNC_HEADER
        (HWND ListWin, int Command, int Parameter)
    #endif
    #if defined(TC_FNC_BODY)
        {TC_FUNCTION_TARGET->ListSendCommand(ListWin, Command, Parameter)
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif

#ifdef TC_L_PRINT
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    ListPrintW
    #ifdef TC_FNC_HEADER
        (HWND ListWin, wchar_t* FileToPrint, wchar_t* DefPrinter, int PrintFlags, RECT* Margins)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_W(ListPrint)(ListWin, FileToPrint, DefPrinter, PrintFlags, Margins);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    ListLoad
    #ifdef TC_FNC_HEADER
        (HWND ListWin, char* FileToPrint, char* DefPrinter, int PrintFlags, RECT* Margins)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)
        {return TC_FUNCTION_TARGET->ListPrint(ListWin, GF::AnsiToUnicode(FileToPrint), GF::AnsiToUnicode(DefPrinter), PrintFlags, Margins);}
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        TC_L_INIT_CALL;
        return TC_FUNCTION_TARGET->ListPrint(ListWin, GF::AnsiToUnicode(SerachString), SearchParameter);
    } 
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif