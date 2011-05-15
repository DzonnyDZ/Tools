#include "ListerPluginBase.h"
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
    inline WlxFunctions ListerPluginBase::ImplementedFunctions::get(){ return this->implementedFunctions; }
    inline Tools::TotalCommanderT::PluginType ListerPluginBase::PluginType::get(){ return Tools::TotalCommanderT::PluginType::Lister; }
    inline Type^ ListerPluginBase::PluginBaseClass::get(){ return ListerPluginBase::typeid; }
#pragma region Load
    inline ListerPluginBase::ListerPluginBase(){
        this->implementedFunctions = this->GetSupportedFunctions<WlxFunctions>();
        this->unicode = true;
        this->isInTotalCommander = false;
        loadedWindows = gcnew Dictionary<IntPtr, IListerUI^>();
    }

    HWND ListerPluginBase::ListLoad(HWND parentWin, wchar_t* fileToLoad, int showFlags, bool wide){
        if(this->Initialized && !this->IsInTotalCommander)
            throw gcnew InvalidOperationException(ResourcesT::Exceptions::PluginInitializedNotForTC);
        if(this->Initialized && this->Unicode != wide)
            throw gcnew InvalidOperationException(ResourcesT::Exceptions::PluginInitializedAnsiUnicode);
        this->initialized = true;
        this->unicode = wide;
        this->isInTotalCommander = true;

        return (HWND)(void*)this->LoadInternal((IntPtr)parentWin, gcnew String(fileToLoad), (ListerShowFlags) showFlags);
    }

    IntPtr ListerPluginBase::Load(IntPtr parentWin, String^ fileToLoad, ListerShowFlags showFlags){
        if(this->Initialized && this->IsInTotalCommander)
            throw gcnew InvalidOperationException(ResourcesT::Exceptions::PluginInitializedForTC);
        if(fileToLoad == nullptr) throw gcnew ArgumentNullException("fileToLoad");
        if(parentWin == IntPtr::Zero) throw gcnew ArgumentNullException("parentWin");
        this->initialized = true;
        this->unicode = true;
        this->isInTotalCommander = false;

        return this->LoadInternal(parentWin, fileToLoad, showFlags);
    }

    IntPtr ListerPluginBase::LoadInternal(IntPtr parentWin, String^ fileToLoad, ListerShowFlags showFlags){
        auto e = gcnew ListerPluginInitEventArgs(parentWin, fileToLoad, showFlags);
        this->OnInit(e);
        if(e->PluginWindow == nullptr){
            return IntPtr::Zero;
        }else{
            loadedWindows->Add(e->PluginWindow->Handle, e->PluginWindow);
            return e->PluginWindow->Handle;
        }
    }

    inline bool ListerPluginBase::Initialized::get(){ return this->initialized; }
    void ListerPluginBase::OnInitInternal(){ this->OnInit(); }
    void ListerPluginBase::OnInit(ListerPluginInitEventArgs^){
        if(this->ImplementedFunctions.HasFlag(WlxFunctions::Load))
            __super::OnInit();    
        else throw gcnew NotSupportedException();
    }
    inline int ListerPluginBase::LoadedWindowsCount::get(){return this->loadedWindows->Count;}
    inline IEnumerator<KeyValuePair<IntPtr, IListerUI^>>^ ListerPluginBase::LoadedWindows::get(){return this->loadedWindows->GetEnumerator();}
    inline IListerUI^ ListerPluginBase::LoadedWindows::get(IntPtr hWnd){return this->loadedWindows->ContainsKey(hWnd) ? this->loadedWindows[hWnd] : nullptr; }

#pragma endregion

    int ListerPluginBase::ListLoadNext(HWND parentWin, HWND listWin, wchar_t* fileToLoad, int showFlags){
        try{
            return this->LoadNext(%ListerPluginReInitEventArgs((IntPtr)parentWin, %String(fileToLoad), (ListerShowFlags)showFlags, (IntPtr)listWin, this->LoadedWindows[(IntPtr)listWin])) ? LISTPLUGIN_OK : LISTPLUGIN_ERROR;
        }catch(NotSupportedException^){
            throw;
        }catch(...){
            return LISTPLUGIN_ERROR;
        }
    }

    bool ListerPluginBase::LoadNext(ListerPluginReInitEventArgs^ e){
        if(!this->ImplementedFunctions.HasFlag(WlxFunctions::LoadNext)) throw gcnew NotSupportedException();
        if(e->PluginWindow != nullptr){
            return e->PluginWindow->LoadNext(e);
        }
        return false;
    }

    inline void ListerPluginBase::ListCloseWindow(HWND listWin){
        this->CloseWindow(this->LoadedWindows[(IntPtr)listWin], (IntPtr)listWin);
    }

    void ListerPluginBase::CloseWindow(IListerUI^ listerUI, IntPtr listerUIHandle){
        if(listerUI != nullptr){
            try{
                this->DispatchCloseWindow(listerUI, listerUIHandle);
                DestroyWindow((HWND)(void*)listerUIHandle);
            }finally{
                this->loadedWindows->Remove(listerUIHandle);
            }
        }
    }
    void ListerPluginBase::DispatchCloseWindow(IListerUI^ listerUI, IntPtr){
        if(listerUI == nullptr) throw gcnew ArgumentNullException("listerUI");
        listerUI->OnBeforeClose();
    }

    void ListerPluginBase::ListGetDetectString(char* detectString, int maxlen){
        this->detectStringMaxLen = maxlen;
        StringCopy(this->DetectString, detectString, maxlen);
    }
    inline Nullable<int> ListerPluginBase::DetectStringMaxLen::get() {return this->detectStringMaxLen; }
    inline String^ ListerPluginBase::DetectString::get(){ throw gcnew NotSupportedException(); }

    int ListerPluginBase::ListSearchText(HWND listWin, wchar_t* searchString, int searchParameter){
        try{
            return this->SearchText(this->LoadedWindows[(IntPtr)listWin], (IntPtr)listWin, gcnew String(searchString), (TextSearchOptions)searchParameter) ? LISTPLUGIN_OK : LISTPLUGIN_ERROR;
        }catch(NotSupportedException^){
            throw;
        }catch(...){
            return LISTPLUGIN_ERROR;
        }
    }
    bool ListerPluginBase::SearchText(IListerUI^ listerUI, IntPtr, String^ searchString, TextSearchOptions searchParameter){
        if(!this->ImplementedFunctions.HasFlag(WlxFunctions::SearchText))
            throw gcnew NotSupportedException();
        if(listerUI != nullptr)
            return listerUI->SearchText(searchString, searchParameter);
        return false;
    }

    int ListerPluginBase::ListSendCommand(HWND listWin, int command, int parameter){
        try{
            return this->SendCommand(this->LoadedWindows[(IntPtr)listWin], (IntPtr)listWin, (ListerCommand)command, (ListerShowFlags)parameter) ? LISTPLUGIN_OK : LISTPLUGIN_ERROR;
        }catch(NotSupportedException^){
            throw;
        }catch(...){
            return LISTPLUGIN_ERROR;
        }
    }
    bool ListerPluginBase::SendCommand(IListerUI^ listerUI, IntPtr, ListerCommand command, ListerShowFlags parameter){
        if(!this->ImplementedFunctions.HasFlag(WlxFunctions::SendCommand))
            throw gcnew NotSupportedException();
        if(listerUI != nullptr)
            return listerUI->OnCommand(gcnew ListerCommandEventArgs(command, parameter));
        return false;
    }

    int ListerPluginBase::ListPrint(HWND listWin, wchar_t* fileToPrint, wchar_t* defPrinter, int printFlags, RECT* margins){
        try{
            return this->Print(this->LoadedWindows[(IntPtr)listWin], (IntPtr)listWin, gcnew String(fileToPrint),
                               gcnew String(defPrinter), (PrintFlags) printFlags,
                               gcnew System::Drawing::Printing::Margins((int)((Single)margins->left * 2.54), (int)((Single)margins->right * 2.54), (int)((Single)margins->top * 2.54), (int)((Single)margins->bottom * 2.54))
                              ) ? LISTPLUGIN_OK : LISTPLUGIN_ERROR;
        }catch(NotSupportedException^){
            throw;
        }catch(...){
            return LISTPLUGIN_ERROR;
        }
    }

    bool ListerPluginBase::Print(IListerUI^ listerUI, IntPtr listerUIHandle, String^ fileToPrint, String^ defPrinter, PrintFlags printFlags, System::Drawing::Printing::Margins^ margins){
        if(!this->ImplementedFunctions.HasFlag(WlxFunctions::Print))
            throw gcnew NotSupportedException();
        if(listerUI != nullptr)
            return listerUI->Print(gcnew PrintEventArgs(fileToPrint, defPrinter, printFlags, margins));
        return false;
    }

    inline int ListerPluginBase::ListNotificationReceived(HWND listWin, int message, WPARAM wParam, LPARAM lParam){
        return this->NotificationReceived(this->LoadedWindows[(IntPtr)listWin], (IntPtr)listWin, message, (UIntPtr)wParam, (IntPtr)lParam);
    }
    int ListerPluginBase::NotificationReceived(IListerUI^ listerUI, IntPtr listerUIHandle, int message, UIntPtr wParam, IntPtr lParam){
        if(!this->ImplementedFunctions.HasFlag(WlxFunctions::NotificationReceived))
            throw gcnew NotSupportedException();
        if(listerUI != nullptr)
            return listerUI->OnNotificationReceived(message, wParam, lParam);
        switch(message){
            case WM_COMMAND: return 1;
            case WM_NOTIFY: return 0;
            case WM_MEASUREITEM: return FALSE;
            case WM_DRAWITEM: return FALSE;
            default: return 0;
        }
    }

    inline void ListerPluginBase::ListSetDefaultParams(ListDefaultParamStruct* dps){
        this->SetDefaultParams(DefaultParams(*dps));
    }

    inline void ListerPluginBase::SetDefaultParams(DefaultParams dps){__super::SetDefaultParams(dps);}

    HBITMAP ListerPluginBase::ListGetPreviewBitmap(wchar_t* fileToLoad, int width, int height, char* contentbuf, int contentbuflen){
        auto bytes = gcnew cli::array<Byte>(contentbuflen);
        for(int i = 0; i < contentbuflen; i++)
            bytes[i] = contentbuf[i];
        auto bmp = this->GetPreviewBitmap(gcnew String(fileToLoad), width, height, bytes);
        if(bmp == nullptr) return NULL;
        return (HBITMAP)(void*)bmp->GetHbitmap();
    }
    inline System::Drawing::Bitmap^ ListerPluginBase::GetPreviewBitmap(String^, int, int, cli::array<Byte>^){
        throw gcnew NotSupportedException();
    }

    inline int ListerPluginBase::ListSearchDialog(HWND listWin, int findNext){
        return this->ShowSearchDialog(this->LoadedWindows[(IntPtr)listWin], (IntPtr)listWin, findNext == 1) ? LISTPLUGIN_OK : LISTPLUGIN_ERROR; 
    }

    bool ListerPluginBase::ShowSearchDialog(IListerUI^ listerUI, IntPtr listerUIHandle, bool findNext){
         if(!this->ImplementedFunctions.HasFlag(WlxFunctions::SearchDialog))
            throw gcnew NotSupportedException();
        if(listerUI != nullptr)
            return listerUI->ShowSearchDialog(findNext);
        return false;
    }
}}