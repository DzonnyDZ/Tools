//To be included in AllTCFunctions.h
#ifdef TC_FS_INIT
    //ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsInit
    #ifdef TC_FNC_HEADER
        (int PluginNr, tProgressProc pProgressProc, tLogProc pLogProc, tRequestProc pRequestProc)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsInit(PluginNr, pProgressProc, pLogProc, pRequestProc);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //Unicode
    //Note: FsInit-specific - names of the functions ANSI and Unicode are same
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsInit
    #ifdef TC_FNC_HEADER
        (int PluginNr, tProgressProcW pProgressProc, tLogProcW pLogProc, tRequestProcW pRequestProc)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsInit(PluginNr, pProgressProc, pLogProc, pRequestProc);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif

#ifdef TC_FS_FINDFIRST
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX HANDLE TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsFindFirstW
    #ifdef TC_FNC_HEADER
        (wchar_t* Path, WIN32_FIND_DATAW *FindData)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_W(FsFindFirst)(Path, FindData);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //ANSI
    #ifdef TC_FNC_HEADER                                                                        
        TC_LINE_PREFIX HANDLE TC_NAME_PREFIX TC_FUNC_MEMBEROF                                  
    #endif                                                                                      
    FsFindFirst                                                               
    #ifdef TC_FNC_HEADER                                                                        
        (char* Path, WIN32_FIND_DATAA *FindData)                                                                                
    #endif                                                                                      
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)                                                                                                                             
        {return TC_FUNCTION_TARGET->FsFindFirst(Path, FindData);}                                          
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        wchar_t* pt = GF::AnsiToUnicode(Path);
        try{
            WIN32_FIND_DATAW fd;
            FindData::FindDataToUnicode(*FindData, fd);
            HANDLE ret = TC_FUNCTION_TARGET->FsFindFirst(pt, &fd);
            FindData::FindDataToAnsi(fd, *FindData);
            return ret;
        }finally{ delete[] pt; }
    }
    #elif defined(TC_FNC_HEADER)                                                                
        ;                                                                                       
    #endif                                                                                      
#endif

#ifdef TC_FS_FINDNEXT
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsFindNextW
    #ifdef TC_FNC_HEADER
        (HANDLE Hdl, WIN32_FIND_DATAW *FindData)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_W(FsFindNext)(Hdl, FindData);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //ANSI
    #ifdef TC_FNC_HEADER                                                                        
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF                                  
    #endif                                                                                      
    FsFindNext                                                               
    #ifdef TC_FNC_HEADER                                                                        
        (HANDLE Hdl, WIN32_FIND_DATAA *FindData)
    #endif                                                                                      
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)                                                                                                                             
        {return TC_FUNCTION_TARGET->FsFindNext(Hdl, FindData);}                                          
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        WIN32_FIND_DATAW fd;
        FindData::FindDataToUnicode(*FindData, fd);
        BOOL ret = TC_FUNCTION_TARGET->FsFindNext(Hdl, &fd);
        FindData::FindDataToAnsi(fd, *FindData);
        return ret;
    }
    #elif defined(TC_FNC_HEADER)                                                                
        ;                                                                                       
    #endif
#endif

#ifdef TC_FS_FINDCLOSE
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsFindClose
    #ifdef TC_FNC_HEADER
        (HANDLE Hdl)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsFindClose(Hdl);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_MKDIR
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsMkDirW
    #ifdef TC_FNC_HEADER
        (wchar_t* Path)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_W(FsMkDir)(Path);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsMkDir
    #ifdef TC_FNC_HEADER
        (char* Path)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)                      
        {return TC_FUNCTION_TARGET->FsMkDir(Path);}
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        return TC_FUNCTION_TARGET->FsMkDir(GF::AnsiToUnicode(Path));
    } 
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif

#ifdef TC_FS_EXECUTEFILE
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsExecuteFileW
    #ifdef TC_FNC_HEADER
        (HWND MainWin, wchar_t* RemoteName, wchar_t* Verb)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_W(FsExecuteFile)(/*(HANDLE)*/MainWin, RemoteName, Verb);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsExecuteFile
    #ifdef TC_FNC_HEADER
        (HWND MainWin, char* RemoteName, char* Verb)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)                      
        {return TC_FUNCTION_TARGET->FsExecuteFile(/*(HANDLE)*/MainWin, RemoteName, Verb);}
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        wchar_t rn[FindData::MaxPath];
        GF::AnsiToUnicode(RemoteName, rn, FindData::MaxPath);
        auto ret = TC_FUNCTION_TARGET->FsExecuteFile(/*(HANDLE)*/MainWin, rn, GF::AnsiToUnicode(Verb));
        GF::UnicodeToAnsi(rn, RemoteName, FindData::MaxPath);
        return ret;
    }
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif

#ifdef TC_FS_RENMOVFILE
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsRenMovFileW
    #ifdef TC_FNC_HEADER
        (wchar_t* OldName, wchar_t* NewName, BOOL Move, BOOL OverWrite, RemoteInfoStruct* ri)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_W(FsRenMovFile)(OldName, NewName, Move, OverWrite, ri);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsRenMovFile
    #ifdef TC_FNC_HEADER
        (char* OldName, char* NewName, BOOL Move, BOOL OverWrite, RemoteInfoStruct* ri)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)                      
        {return TC_FUNCTION_TARGET->FsRenMovFile(OldName, NewName, Move, OverWrite, ri);}
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        return TC_FUNCTION_TARGET->FsRenMovFile(GF::AnsiToUnicode(OldName), GF::AnsiToUnicode(NewName), Move, OverWrite, ri);
    }   
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif

#ifdef TC_FS_GETFILE
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsGetFileW
    #ifdef TC_FNC_HEADER
        (wchar_t* RemoteName, wchar_t* LocalName, int CopyFlags, RemoteInfoStruct* ri)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_W(FsGetFile)(RemoteName, LocalName, CopyFlags, ri);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsGetFile
    #ifdef TC_FNC_HEADER
        (char* RemoteName, char* LocalName, int CopyFlags, RemoteInfoStruct* ri)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)
        {return TC_FUNCTION_TARGET->FsGetFile(RemoteName, LocalName, CopyFlags, ri);}
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)                   
    {   //ANSI to Unicode
        wchar_t* rn = GF::AnsiToUnicode(RemoteName);
        try{
            int lnSize = Math::Max((size_t)FindData::MaxPath, strlen(LocalName) + 1);
            wchar_t* ln = new wchar_t[lnSize];
            try{
                GF::AnsiToUnicode(LocalName, ln, lnSize - 1);
                int ret = TC_FUNCTION_TARGET->FsGetFile(rn, ln, CopyFlags, ri);
                GF::UnicodeToAnsi(ln, LocalName, lnSize - 1);
                return ret;
            }finally{ delete[] ln; }
        }finally{ delete[] rn; }
    }
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif

#ifdef TC_FS_PUTFILE
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsPutFileW
    #ifdef TC_FNC_HEADER
        (wchar_t* LocalName, wchar_t* RemoteName, int CopyFlags)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_W(FsPutFile)(LocalName, RemoteName, CopyFlags);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsPutFile
    #ifdef TC_FNC_HEADER
        (char* LocalName, char* RemoteName, int CopyFlags)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)
        {return TC_FUNCTION_TARGET->FsPutFile(LocalName, RemoteName, CopyFlags);}
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        auto ln = GF::AnsiToUnicode(LocalName);
        try{
            int rnSize = Math::Max((size_t)FindData::MaxPath, strlen(RemoteName) + 1);
            wchar_t* rn = new wchar_t[rnSize];
            try{
                GF::AnsiToUnicode(RemoteName, rn, rnSize - 1);
                int ret = TC_FUNCTION_TARGET->FsPutFile(ln, rn, CopyFlags);
                GF::UnicodeToAnsi(rn, RemoteName, rnSize - 1);
                return ret;
            }finally{ delete[] rn; }

        }finally{ delete[] ln; }
    }      
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif

#ifdef TC_FS_DELETEFILE
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsDeleteFileW
    #ifdef TC_FNC_HEADER
        (wchar_t* RemoteName)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_W(FsDeleteFile)(RemoteName);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsDeleteFile
    #ifdef TC_FNC_HEADER
        (char* RemoteName)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)
        {return TC_FUNCTION_TARGET->FsDeleteFile(RemoteName);}
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        return TC_FUNCTION_TARGET->FsDeleteFile(GF::AnsiToUnicode(RemoteName));
    }
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif

#ifdef TC_FS_REMOVEDIR
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsRemoveDirW
    #ifdef TC_FNC_HEADER
        (wchar_t* RemoteName)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_W(FsRemoveDir)(RemoteName);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    ///ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsRemoveDir
    #ifdef TC_FNC_HEADER
        (char* RemoteName)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)                      
        {return TC_FUNCTION_TARGET->FsRemoveDir(RemoteName);}
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        return TC_FUNCTION_TARGET->FsRemoveDir(GF::AnsiToUnicode(RemoteName));
    } 
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif

#ifdef TC_FS_DISCONNECT
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsDisconnectW
    #ifdef TC_FNC_HEADER
        (wchar_t* DisconnectRoot)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_W(FsDisconnect)(DisconnectRoot);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsDisconnect
    #ifdef TC_FNC_HEADER
        (char* DisconnectRoot)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)                      
        {return TC_FUNCTION_TARGET->FsDisconnect(DisconnectRoot);}
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        return TC_FUNCTION_TARGET->FsDisconnect(GF::AnsiToUnicode(DisconnectRoot));
    } 
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif

#ifdef TC_FS_SETATTR 
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsSetAttrW
    #ifdef TC_FNC_HEADER
        (wchar_t* RemoteName, int NewAttr)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_W(FsSetAttr)(RemoteName, NewAttr);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsSetAttr
    #ifdef TC_FNC_HEADER
        (char* RemoteName,int NewAttr)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)                      
        {return TC_FUNCTION_TARGET->FsSetAttr(RemoteName, NewAttr);}
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        return TC_FUNCTION_TARGET->FsSetAttr(GF::AnsiToUnicode(RemoteName), NewAttr);
    } 
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif

#ifdef TC_FS_SETTIME
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsSetTimeW
    #ifdef TC_FNC_HEADER
        (wchar_t* RemoteName, ::FILETIME *CreationTime, ::FILETIME *LastAccessTime, ::FILETIME *LastWriteTime)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_W(FsSetTime)(RemoteName, CreationTime, LastAccessTime, LastWriteTime);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsSetTime
    #ifdef TC_FNC_HEADER
        (char* RemoteName,::FILETIME *CreationTime,::FILETIME *LastAccessTime,::FILETIME *LastWriteTime)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)                      
        {return TC_FUNCTION_TARGET->FsSetTime(RemoteName, CreationTime, LastAccessTime, LastWriteTime);}
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        return TC_FUNCTION_TARGET->FsSetTime(GF::AnsiToUnicode(RemoteName), CreationTime, LastAccessTime, LastWriteTime);
    } 
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif

#ifdef TC_FS_STATUSINFO
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsStatusInfoW
    #ifdef TC_FNC_HEADER
        (wchar_t* RemoteDir, int InfoStartEnd, int InfoOperation)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_W(FsStatusInfo)(RemoteDir, InfoStartEnd, InfoOperation);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsStatusInfo
    #ifdef TC_FNC_HEADER
        (char* RemoteDir, int InfoStartEnd, int InfoOperation)
    #endif
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)                      
        {return TC_FUNCTION_TARGET->FsStatusInfo(RemoteDir, InfoStartEnd, InfoOperation);}
        #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)
    {   //ANSI to Unicode
        return TC_FUNCTION_TARGET->FsStatusInfo(GF::AnsiToUnicode(RemoteDir), InfoStartEnd, InfoOperation);}
    }          
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif

#ifdef TC_FS_GETDEFROOTNAME
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsGetDefRootName
    #ifdef TC_FNC_HEADER
        (char* DefRootName,int maxlen)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsGetDefRootName(DefRootName, maxlen);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_EXTRACTCUSTOMICON
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsExtractCustomIcon
    #ifdef TC_FNC_HEADER
        (char* RemoteName,int ExtractFlags,HICON* TheIcon)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsExtractCustomIcon(RemoteName, ExtractFlags, TheIcon);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_SETDEFAULTPARAMS
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsSetDefaultParams
    #ifdef TC_FNC_HEADER
        (FsDefaultParamStruct* dps)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsSetDefaultParams(dps);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_GETPREVIEWBITMAP
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsGetPreviewBitmap
    #ifdef TC_FNC_HEADER
        (char* RemoteName,int width,int height,HBITMAP* ReturnedBitmap)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsGetPreviewBitmap(RemoteName, width, height, ReturnedBitmap);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_LINKSTOLOCALFILES
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsLinksToLocalFiles
    #ifdef TC_FNC_HEADER
        (void)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsLinksToLocalFiles();}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_GETLOCALNAME
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsGetLocalName
    #ifdef TC_FNC_HEADER
        (char* RemoteName,int maxlen)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsGetLocalName(RemoteName, maxlen);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_GETDEFAULTVIEW
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsContentGetDefaultView
    #ifdef TC_FNC_HEADER
        (char* ViewContents,char* ViewHeaders,char* ViewWidths,char* ViewOptions,int maxlen)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsContentGetDefaultView(ViewContents, ViewHeaders, ViewWidths, ViewOptions, maxlen);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_SETCRYPTCALLBACK
    //ANSI
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsSetCryptCallback
    #ifdef TC_FNC_HEADER
        (tCryptProc pCryptProc, int CryptoNr, int Flags)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsSetCryptCallback(pCryptProc, CryptoNr, Flags);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
    //Unicode
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsSetCryptCallbackW
    #ifdef TC_FNC_HEADER
        (tCryptProcW pCryptProc, int CryptoNr, int Flags)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsSetCryptCallbackW(pCryptProc, CryptoNr, Flags);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_GETBACKGROUNDFLAGS
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsGetBackgroundFlags
    #ifdef TC_FNC_HEADER
        (void)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsGetBackgroundFlags();}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif