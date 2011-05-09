/*
TC_WFX, TC_WDX, TC_WLX, TC_WCX - plugin type
Single function constants (there is a constant defined for each TC function do determine if it should be included or not)

#ifdef TC_SingleFunctionConstant                                      
    //universal                                                       
    #ifdef TC_FNC_HEADER                                              
        TC_LINE_PREFIX rettype TC_NAME_PREFIX TC_FUNC_MEMBEROF        
    #endif                                                            
    TC_GETFNAME_A(name) -or- name                                     
    #ifdef TC_FNC_HEADER                                              
        (params)                                                      
    #ednif                                                            
    #if defined(TC_FNC_BODY)                                          
        {return TC_FUNCTION_TARGET->TC_GETFNAME_B(name)(callparams);} 
        -or-                                                          
        {return TC_FUNCTION_TARGET->name(callparams);}                
    #elif defined(TC_FNC_HEADER)                                      
        ;                                                             
    #endif                                                            

    //Unicode                                                         
    #ifdef TC_FNC_HEADER                                              
        TC_LINE_PREFIX rettype TC_NAME_PREFIX TC_FUNC_MEMBEROF        
    #endif                                                            
    TC_GETFNAME_A(nameW) -or- nameW                                   
    #ifdef TC_FNC_HEADER                                              
        (params)                                                      
    #ednif                                                            
    #if defined(TC_FNC_BODY)                                          
        {return TC_FUNCTION_TARGET->TC_GETFNAME_BW(name)(callparams);}
        -or-                                                          
        {return TC_FUNCTION_TARGET->TC_GETFNAME_W(name)(callparams);} 
    #elif defined(TC_FNC_HEADER)                                      
        ;                                                             
    #endif                                                            

    //ANSI version of Unicode function                                
    #ifdef TC_FNC_HEADER                                              
        TC_LINE_PREFIX rettype TC_NAME_PREFIX TC_FUNC_MEMBEROF        
    #endif                                                            
    TC_GETFNAME_A(name) -or- name                                     
    #ifdef TC_FNC_HEADER                                              
        (params)                                                      
    #endif                                                            
    #if defined(TC_FNC_BODY) && !defined(TC_A2W)                      
        {return TC_FUNCTION_TARGET->TC_GETFNAME_B(name)(callparams);} 
        -or-                                                          
        {return TC_FUNCTION_TARGET->name(callparams);}                
    #elif defined(TC_FNC_BODY) //&& defined(TC_A2W)                   
    {   //ANSI to Unicode                                             
    }                                                                 
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
#ifdef TC_GETFNAME_BW
    #undef TC_GETFNAME_BW
#endif
#ifdef TC_GETFNAME_W
    #undef TC_GETFNAME_W
#endif
#define TC_GETFNAME_A(name) TC_FUNC_PREFIX_A##name
#define TC_GETFNAME_B(name) TC_FUNC_PREFIX_B##name
#if defined(TC_A2W)
    #define TC_GETFNAME_W(name) name
    #define TC_GETFNAME_BW(name) TC_FUNC_PREFIX_B##name
#else
    #define TC_GETFNAME_W(name) name##W
#define TC_GETFNAME_BW(name) TC_FUNC_PREFIX_B##name##W
#endif
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
    #include "WLXFunctions.h"
#endif
//Pack wcx
#ifdef TC_WCX
    //TODO:WCX
#endif