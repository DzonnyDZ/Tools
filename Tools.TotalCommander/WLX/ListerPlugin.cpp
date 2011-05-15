#include "ListerPlugin.h"
#include "..\Plugin\listplug.h"
#include "..\Exceptions.h"
#include <vcclr.h>
#include "..\Common.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace Tools::TotalCommanderT::ResourcesT;
using namespace Tools::ExtensionsT;
using namespace Microsoft::VisualBasic;

namespace Tools{namespace TotalCommanderT{
    //ListerPlugin
    inline WlxFunctions ListerPlugin::ImplementedFunctions::get(){ return this->implementedFunctions; }
    inline Tools::TotalCommanderT::PluginType ListerPlugin::PluginType::get(){ return Tools::TotalCommanderT::PluginType::Lister; }
    inline Type^ ListerPlugin::PluginBaseClass::get(){ return ListerPlugin::typeid; }
#pragma region Load
    inline ListerPlugin::ListerPlugin(){
        this->implementedFunctions = this->GetSupportedFunctions<WlxFunctions>();
        this->unicode = true;
        this->isInTotalCommander = false;
        loadedWindows = gcnew Dictionary<IntPtr, IListerUI^>();
    }

    HWND ListerPlugin::ListLoad(HWND parentWin, wchar_t* fileToLoad, int showFlags, bool wide){
        if(this->Initialized && !this->IsInTotalCommander)
            throw gcnew InvalidOperationException(ResourcesT::Exceptions::PluginInitializedNotForTC);
        if(this->Initialized && this->Unicode != wide)
            throw gcnew InvalidOperationException(ResourcesT::Exceptions::PluginInitializedAnsiUnicode);
        this->initialized = true;
        this->unicode = wide;
        this->isInTotalCommander = true;

        return (HWND)(void*)this->LoadInternal((IntPtr)parentWin, gcnew String(fileToLoad), (ListerShowFlags) showFlags);
    }

    IntPtr ListerPlugin::Load(IntPtr parentWin, String^ fileToLoad, ListerShowFlags showFlags){
        if(this->Initialized && this->IsInTotalCommander)
            throw gcnew InvalidOperationException(ResourcesT::Exceptions::PluginInitializedForTC);
        if(fileToLoad == nullptr) throw gcnew ArgumentNullException("fileToLoad");
        if(parentWin == IntPtr::Zero) throw gcnew ArgumentNullException("parentWin");
        this->initialized = true;
        this->unicode = true;
        this->isInTotalCommander = false;

        return this->LoadInternal(parentWin, fileToLoad, showFlags);
    }

    IntPtr ListerPlugin::LoadInternal(IntPtr parentWin, String^ fileToLoad, ListerShowFlags showFlags){
        auto e = gcnew ListerPluginInitEventArgs(parentWin, fileToLoad, showFlags);
        this->OnInit(e);
        if(e->PluginWindow == nullptr){
            return IntPtr::Zero;
        }else{
            loadedWindows->Add(e->PluginWindow->Handle, e->PluginWindow);
            return e->PluginWindow->Handle;
        }
    }

    inline bool ListerPlugin::Initialized::get(){ return this->initialized; }
    void ListerPlugin::OnInitInternal(){ this->OnInit(); }
    void ListerPlugin::OnInit(ListerPluginInitEventArgs^){
        if(this->ImplementedFunctions.HasFlag(WlxFunctions::Load))
            __super::OnInit();    
        else throw gcnew NotSupportedException();
    }
    inline int ListerPlugin::LoadedWindowsCount::get(){return this->loadedWindows->Count;}
    inline IEnumerator<KeyValuePair<IntPtr, IListerUI^>>^ ListerPlugin::LoadedWindows::get(){return this->loadedWindows->GetEnumerator();}
    inline IListerUI^ ListerPlugin::LoadedWindows::get(IntPtr hWnd){return this->loadedWindows->ContainsKey(hWnd) ? this->loadedWindows[hWnd] : nullptr; }

#pragma endregion

    int ListerPlugin::ListLoadNext(HWND parentWin, HWND listWin, wchar_t* fileToLoad, int showFlags){
        try{
            return this->LoadNext(%ListerPluginReInitEventArgs((IntPtr)parentWin, %String(fileToLoad), (ListerShowFlags)showFlags, (IntPtr)listWin, this->LoadedWindows[(IntPtr)listWin])) ? LISTPLUGIN_OK : LISTPLUGIN_ERROR;
        }catch(NotSupportedException^){
            throw;
        }catch(...){
            return LISTPLUGIN_ERROR;
        }
    }

    bool ListerPlugin::LoadNext(ListerPluginReInitEventArgs^ e){
        if(!this->ImplementedFunctions.HasFlag(WlxFunctions::LoadNext)) throw gcnew NotSupportedException();
        if(e->PluginWindow != nullptr){
            return e->PluginWindow->LoadNext(e);
        }
        return false;
    }

    inline void ListerPlugin::ListCloseWindow(HWND listWin){
        this->CloseWindow(this->LoadedWindows[(IntPtr)listWin], (IntPtr)listWin);
    }

    void ListerPlugin::CloseWindow(IListerUI^ listerUI, IntPtr listerUIHandle){
        if(listerUI != nullptr){
            try{
                this->DispatchCloseWindow(listerUI, listerUIHandle);
                DestroyWindow((HWND)(void*)listerUIHandle);
            }finally{
                this->loadedWindows->Remove(listerUIHandle);
            }
        }
    }
    void ListerPlugin::DispatchCloseWindow(IListerUI^ listerUI, IntPtr){
        if(listerUI == nullptr) throw gcnew ArgumentNullException("listerUI");
        listerUI->OnBeforeClose();
    }

    void ListerPlugin::ListGetDetectString(char* detectString, int maxlen){
        this->detectStringMaxLen = maxlen;
        StringCopy(this->DetectString, detectString, maxlen);
    }
    inline Nullable<int> ListerPlugin::DetectStringMaxLen::get() {return this->detectStringMaxLen; }
    inline String^ ListerPlugin::DetectString::get(){ throw gcnew NotSupportedException(); }

    int ListerPlugin::ListSearchText(HWND listWin, wchar_t* searchString, int searchParameter){
        try{
            return this->SearchText(this->LoadedWindows[(IntPtr)listWin], (IntPtr)listWin, gcnew String(searchString), (TextSearchOptions)searchParameter) ? LISTPLUGIN_OK : LISTPLUGIN_ERROR;
        }catch(NotSupportedException^){
            throw;
        }catch(...){
            return LISTPLUGIN_ERROR;
        }
    }
    bool ListerPlugin::SearchText(IListerUI^ listerUI, IntPtr, String^ searchString, TextSearchOptions searchParameter){
        if(!this->ImplementedFunctions.HasFlag(WlxFunctions::SearchText))
            throw gcnew NotSupportedException();
        if(listerUI != nullptr)
            return listerUI->SearchText(searchString, searchParameter);
    }

    int ListerPlugin::ListSendCommand(HWND listWin, int command, int parameter){
        try{
            return this->SendCommand(this->LoadedWindows[(IntPtr)listWin], (IntPtr)listWin, (ListerCommand)command, (ListerShowFlags)parameter) ? LISTPLUGIN_OK : LISTPLUGIN_ERROR;
        }catch(NotSupportedException^){
            throw;
        }catch(...){
            return LISTPLUGIN_ERROR;
        }
    }
    bool ListerPlugin::SendCommand(IListerUI^ listerUI, IntPtr, ListerCommand command, ListerShowFlags parameter){
        if(!this->ImplementedFunctions.HasFlag(WlxFunctions::SendCommand))
            throw gcnew NotSupportedException();
        if(listerUI != nullptr)
            return listerUI->OnCommand(gcnew ListerCommandEventArgs(command, parameter));
    }
}}