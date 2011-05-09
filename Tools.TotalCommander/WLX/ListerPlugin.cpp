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
    }

    HWND ListerPlugin::ListLoad(HWND parentWin, wchar_t* fileToLoad, int showFlags, bool wide){
        if(this->Initialized) throw gcnew InvalidOperationException(ResourcesT::Exceptions::PluginInitialized);
        this->initialized = true;
        this->unicode = wide;
        this->isInTotalCommander = true;

        this->parentWindowHandle = (IntPtr)parentWin;
        this->fileName = gcnew String(fileToLoad);
        this->options = (ListerShowFlags) showFlags;

        IntPtr hWnd = this->OnInit();
        this->controlHandle = hWnd;
        return (HWND)(void *)hWnd;
    }

    IntPtr ListerPlugin::Load(IntPtr parentWin, String^ fileToLoad, ListerShowFlags showFlags){
        if(this->Initialized) throw gcnew InvalidOperationException(ResourcesT::Exceptions::PluginInitialized);
        if(fileToLoad == nullptr) throw gcnew ArgumentNullException("fileToLoad");
        if(parentWin == IntPtr::Zero) throw gcnew ArgumentNullException("parentWin");
        this->initialized = true;
        this->unicode = true;
        this->isInTotalCommander = false;

        this->parentWindowHandle = parentWin;
        this->fileName = fileToLoad;
        this->options = showFlags;

        IntPtr hWnd = this->OnInit();
        this->controlHandle = hWnd;
        return hWnd;
    }

    inline IntPtr ListerPlugin::ParentWindowHandle::get(){ return this->parentWindowHandle; }
    inline String^ ListerPlugin::FileName::get(){ return this->fileName; }
    inline ListerShowFlags ListerPlugin::Options::get(){ return this->options; }
    inline bool ListerPlugin::Initialized::get(){ return this->initialized; }
    inline IntPtr ListerPlugin::ControlHandle::get() {return this->controlHandle; }
    void ListerPlugin::OnInitInternal(){ this->OnInit(); }
    IntPtr ListerPlugin::OnInit(){
        if(this->ImplementedFunctions.HasFlag(WlxFunctions::Load))
            __super::OnInit();    
        else throw gcnew NotSupportedException();
        return IntPtr::Zero;
    }

#pragma endregion
}}