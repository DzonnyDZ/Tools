#include "WLX common.h"
#include "..\Common.h"
#include "..\Exceptions.h"

namespace Tools{namespace TotalCommanderT{

#pragma region ListerPluginInitEventArgs

    ListerPluginInitEventArgs::ListerPluginInitEventArgs(IntPtr parentWindowHandle, String^ fileToLoad, ListerShowFlags options){
        this->parentWindowHandle = parentWindowHandle;
        this->fileToLoad = fileToLoad;
        this->options = options;
    }
    inline ListerShowFlags ListerPluginInitEventArgs::Options::get(){ return this->options; }
    inline IntPtr ListerPluginInitEventArgs::ParentWindowHandle::get(){ return this->parentWindowHandle; }
    inline String^ ListerPluginInitEventArgs::FileToLoad::get(){ return this->fileToLoad; }
    inline IntPtr ListerPluginInitEventArgs::PluginWindowHandle::get(){ return this->PluginWindow == nullptr ? IntPtr::Zero : this->PluginWindow->Handle; }

#pragma endregion

#pragma region ListerPluginInitEventArgsSpecialized
    generic<class TUI>
    inline ListerPluginInitEventArgsSpecialized<TUI>::ListerPluginInitEventArgsSpecialized(IntPtr parentWindowHandle, String^ fileToLoad, ListerShowFlags options):ListerPluginInitEventArgs(parentWindowHandle, fileToLoad, options){}
    
    generic<class TUI>
    void ListerPluginInitEventArgsSpecialized<TUI>::PluginWindowInternal::set(IListerUI^ value){
        if(!Tools::TypeTools::Is<TUI>(value)) throw gcnew Tools::TypeMismatchException(value, "value", TUI::typeid);
        __super::PluginWindow = value;
    }
    
    generic<class TUI> where TUI: IListerUI, ref class
    inline TUI ListerPluginInitEventArgsSpecialized<TUI>::PluginWindow::get(){return (TUI)__super::PluginWindow;}
    
    generic<class TUI> where TUI: IListerUI, ref class
    inline void ListerPluginInitEventArgsSpecialized<TUI>::PluginWindow::set(TUI value){__super::PluginWindow = value;}

#pragma endregion

#pragma region ListerPluginReInitEventArgs

    ListerPluginReInitEventArgs::ListerPluginReInitEventArgs(IntPtr parentWindowHandle, String^ fileToLoad, ListerShowFlags options, IntPtr pluginWindowHandle, IListerUI^ pluginWindow) : ListerPluginInitEventArgs(parentWindowHandle, fileToLoad, options){
        __super::PluginWindow = pluginWindow;
        this->pluginWindowHandle = pluginWindowHandle;
    }

    inline void ListerPluginReInitEventArgs::setPluginWindow(IListerUI^){
        throw gcnew NotSupportedException(String::Format(ResourcesT::Exceptions::PropertyIsReadOnly, "PluginWindow", ListerPluginReInitEventArgs::typeid->Name));
    }
    inline IntPtr ListerPluginReInitEventArgs::PluginWindowHandle::get(){return this->pluginWindowHandle;}

#pragma endregion

#pragma region ListerCommandEventArgs
    ListerCommandEventArgs::ListerCommandEventArgs(ListerCommand command, ListerShowFlags parameter){
        this->command = command;
        this->parameter = parameter;
    }
    inline ListerCommand ListerCommandEventArgs::Command::get(){return this->command;}
    inline ListerShowFlags ListerCommandEventArgs::Parameter::get(){return this->parameter;}
#pragma endregion

#pragma region PrintEventArgs
    PrintEventArgs::PrintEventArgs(String^ fileToPrint, String^ defPrinter, Tools::TotalCommanderT::PrintFlags printFlags, System::Drawing::Printing::Margins^ margins){
        this->fileToPrint = fileToPrint;
        this->defPrinter = defPrinter;
        this->printFlags = printFlags;
        this->margins = margins;
    }
    inline String^ PrintEventArgs::FileToPrint::get(){return this->fileToPrint;} 
    inline String^ PrintEventArgs::DefPrinter::get(){return this->defPrinter;}
    inline Tools::TotalCommanderT::PrintFlags PrintEventArgs::PrintFlags::get(){return this->printFlags;}
    inline System::Drawing::Printing::Margins^ PrintEventArgs::Margins::get(){return this->margins;}
#pragma endregion

}}