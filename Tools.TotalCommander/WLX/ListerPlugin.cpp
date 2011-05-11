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
}}