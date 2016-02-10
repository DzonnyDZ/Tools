#include "WLX common.h"
#include "..\Common.h"
#include "..\Exceptions.h"

namespace Tools {
    namespace TotalCommanderT {

#pragma region ListerPluginUIEventArgs
        inline ListerPluginUIEventArgs::ListerPluginUIEventArgs() {}
        inline ListerPluginUIEventArgs::ListerPluginUIEventArgs(IListerUI^ pluginWindow) { this->pluginWindow = pluginWindow; }
        ListerPluginUIEventArgs::ListerPluginUIEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow) {
            if (pluginWindow != nullptr && pluginWindowHandle != pluginWindow->Handle)
                throw gcnew ArgumentException(ResourcesT::Exceptions::PluginWindowHandleMismatch);
        }
        inline IListerUI^ ListerPluginUIEventArgs::PluginWindow::get() { return this->pluginWindow; }
        IntPtr ListerPluginUIEventArgs::PluginWindowHandle::get() {
            if (this->PluginWindow != nullptr) return this->PluginWindow->Handle;
            return this->PluginWindowHandle;
        }
#pragma endregion

#pragma region InfoEventArgs
        template<class T>
        inline InfoEventArgs<T>::InfoEventArgs(T base, bool result) : T(base) {
            this->result = result;
        }
        template<class T>
        inline bool InfoEventArgs<T>::Result::get() { return this->result; }
        //template<class T>
        //inline InfoEventArgs<T>::InfoEventArgs(const InfoEventArgs<T>% other):T(other){
        //    this->result = other->result;
        //}
#pragma endregion

#pragma region ListerPluginInitEventArgs

        ListerPluginInitEventArgs::ListerPluginInitEventArgs(IntPtr parentWindowHandle, String^ fileToLoad, ListerShowFlags options) {
            this->parentWindowHandle = parentWindowHandle;
            this->fileToLoad = fileToLoad;
            this->options = options;
        }
        inline ListerShowFlags ListerPluginInitEventArgs::Options::get() { return this->options; }
        inline IntPtr ListerPluginInitEventArgs::ParentWindowHandle::get() { return this->parentWindowHandle; }
        inline String^ ListerPluginInitEventArgs::FileToLoad::get() { return this->fileToLoad; }
        inline IntPtr ListerPluginInitEventArgs::PluginWindowHandle::get() { return this->PluginWindow == nullptr ? IntPtr::Zero : this->PluginWindow->Handle; }
        inline IListerUI^ ListerPluginInitEventArgs::PluginWindow::get() { return this->pluginWindow; }
        inline void ListerPluginInitEventArgs::PluginWindow::set(IListerUI^ value) { this->pluginWindow = value; }

#pragma endregion

#pragma region ListerPluginInitEventArgsSpecialized
        generic<class TUI>
            inline ListerPluginInitEventArgsSpecialized<TUI>::ListerPluginInitEventArgsSpecialized(IntPtr parentWindowHandle, String^ fileToLoad, ListerShowFlags options) :ListerPluginInitEventArgs(parentWindowHandle, fileToLoad, options) {}

            generic<class TUI>
                void ListerPluginInitEventArgsSpecialized<TUI>::PluginWindowInternal::set(IListerUI^ value) {
                    if (!Tools::TypeTools::Is<TUI>(value)) throw gcnew Tools::TypeMismatchException(value, "value", TUI::typeid);
                    __super::PluginWindow = value;
                }

                generic<class TUI> where TUI: IListerUI, ref class
                    inline TUI ListerPluginInitEventArgsSpecialized<TUI>::PluginWindow::get() { return (TUI)__super::PluginWindow; }

                generic<class TUI> where TUI : IListerUI, ref class
                    inline void ListerPluginInitEventArgsSpecialized<TUI>::PluginWindow::set(TUI value) { __super::PluginWindow = value; }

#pragma endregion

#pragma region ListerPluginReInitEventArgs

                ListerPluginReInitEventArgs::ListerPluginReInitEventArgs(IntPtr parentWindowHandle, String^ fileToLoad, ListerShowFlags options, IntPtr pluginWindowHandle, IListerUI^ pluginWindow)
                    : ListerPluginInitEventArgs(parentWindowHandle, fileToLoad, options) {
                    __super::PluginWindow = pluginWindow;
                    this->pluginWindowHandle = pluginWindowHandle;
                }

                ListerPluginReInitEventArgs::ListerPluginReInitEventArgs(ListerPluginReInitEventArgs% other)
                    : ListerPluginInitEventArgs(Tools::ExtensionsT::General::ThrowIfNull(%other, "other")->ParentWindowHandle, other.FileToLoad, other.Options) {
                    __super::PluginWindow = other.PluginWindow;
                    this->pluginWindowHandle = other.PluginWindowHandle;
                }

                inline void ListerPluginReInitEventArgs::setPluginWindow(IListerUI^) {
                    throw gcnew NotSupportedException(String::Format(ResourcesT::Exceptions::PropertyIsReadOnly, "PluginWindow", ListerPluginReInitEventArgs::typeid->Name));
                }
                inline IntPtr ListerPluginReInitEventArgs::PluginWindowHandle::get() { return this->pluginWindowHandle; }

#pragma endregion

#pragma region ListerPluginReInitInfoEventArgs
                ListerPluginReInitInfoEventArgs::ListerPluginReInitInfoEventArgs(IntPtr parentWindowHandle, String^ fileToLoad, ListerShowFlags options, IntPtr pluginWindowHandle, IListerUI^ pluginWindow, bool result)
                    : InfoEventArgs<ListerPluginReInitEventArgs>(ListerPluginReInitEventArgs(parentWindowHandle, fileToLoad, options, pluginWindowHandle, pluginWindow), result) {}
#pragma endregion

#pragma region ListerCommandEventArgs
                ListerCommandEventArgs::ListerCommandEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow, ListerCommand command, ListerShowFlags parameter)
                    : ListerPluginUIEventArgs(pluginWindowHandle, pluginWindow) {
                    this->command = command;
                    this->parameter = parameter;
                }
                ListerCommandEventArgs::ListerCommandEventArgs(ListerCommandEventArgs% other)
                    : ListerPluginUIEventArgs(Tools::ExtensionsT::General::ThrowIfNull(%other, "other")->PluginWindowHandle, other.PluginWindow) {
                    this->command = other.Command;
                    this->parameter = other.Parameter;
                }
                inline ListerCommand ListerCommandEventArgs::Command::get() { return this->command; }
                inline ListerShowFlags ListerCommandEventArgs::Parameter::get() { return this->parameter; }
#pragma endregion
#pragma region ListerCommandInfoEventArgs
                ListerCommandInfoEventArgs::ListerCommandInfoEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow, ListerCommand command, ListerShowFlags parameter, bool result)
                    :InfoEventArgs<ListerCommandEventArgs>(ListerCommandEventArgs(pluginWindowHandle, pluginWindow, command, parameter), result) {}
#pragma endregion

#pragma region PrintEventArgs
                PrintEventArgs::PrintEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow, String^ fileToPrint, String^ defPrinter, Tools::TotalCommanderT::PrintFlags printFlags, System::Drawing::Printing::Margins^ margins)
                    : ListerPluginUIEventArgs(pluginWindowHandle, pluginWindow) {
                    this->fileToPrint = fileToPrint;
                    this->defPrinter = defPrinter;
                    this->printFlags = printFlags;
                    this->margins = margins;
                }
                PrintEventArgs::PrintEventArgs(PrintEventArgs% other)
                    :ListerPluginUIEventArgs(Tools::ExtensionsT::General::ThrowIfNull(%other, "other")->PluginWindowHandle, other.PluginWindow) {
                    this->fileToPrint = other.FileToPrint;
                    this->defPrinter = other.DefPrinter;
                    this->printFlags = other.PrintFlags;
                    this->margins = other.Margins;
                }
                inline String^ PrintEventArgs::FileToPrint::get() { return this->fileToPrint; }
                inline String^ PrintEventArgs::DefPrinter::get() { return this->defPrinter; }
                inline Tools::TotalCommanderT::PrintFlags PrintEventArgs::PrintFlags::get() { return this->printFlags; }
                inline System::Drawing::Printing::Margins^ PrintEventArgs::Margins::get() { return this->margins; }
#pragma endregion

#pragma region PrintInfoEventArgs
                PrintInfoEventArgs::PrintInfoEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow, String^ fileToPrint, String^ defPrinter, Tools::TotalCommanderT::PrintFlags printFlags, System::Drawing::Printing::Margins^ margins, bool result)
                    :InfoEventArgs<PrintEventArgs>(PrintEventArgs(pluginWindowHandle, pluginWindow, fileToPrint, defPrinter, printFlags, margins), result) {}
#pragma endregion

#pragma region TextSearchEventArgs
                TextSearchEventArgs::TextSearchEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow, String^ searchString, TextSearchOptions searchParameter)
                    : ListerPluginUIEventArgs(pluginWindowHandle, pluginWindow) {
                    this->searchString = searchString;
                    this->searchParameter = searchParameter;
                }
                TextSearchEventArgs::TextSearchEventArgs(TextSearchEventArgs% other)
                    : ListerPluginUIEventArgs(Tools::ExtensionsT::General::ThrowIfNull(%other, "other")->PluginWindowHandle, other.PluginWindow) {
                    this->searchString = other.SearchString;
                    this->searchParameter = other.SearchParameter;
                }
                inline String^ TextSearchEventArgs::SearchString::get() { return this->searchString; }
                inline TextSearchOptions TextSearchEventArgs::SearchParameter::get() { return this->searchParameter; }
#pragma endregion

#pragma region TextSearchInfoEventArgs
                TextSearchInfoEventArgs::TextSearchInfoEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow, String^ searchString, TextSearchOptions searchParameter, bool result)
                    :InfoEventArgs<TextSearchEventArgs>(TextSearchEventArgs(pluginWindowHandle, pluginWindow, searchString, searchParameter), result) {}
#pragma endregion

#pragma region MessageEventArgs
                MessageEventArgs::MessageEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow, int message, UIntPtr wParam, IntPtr lParam)
                    : ListerPluginUIEventArgs(pluginWindowHandle, pluginWindow) {
                    this->lParam = lParam;
                    this->wParam = wParam;
                    this->message = message;
                }
                MessageEventArgs::MessageEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow, int message, IntPtr wParam, IntPtr lParam)
                    : ListerPluginUIEventArgs(pluginWindowHandle, pluginWindow) {
                    this->lParam = lParam;
                    this->wParam = wParam;
                    this->message = message;
                }
                MessageEventArgs::MessageEventArgs(MessageEventArgs% other)
                    : ListerPluginUIEventArgs(Tools::ExtensionsT::General::ThrowIfNull(%other, "other")->PluginWindowHandle, other.PluginWindow) {
                    this->lParam = other.LParam;
                    this->wParam = other.WParam;
                    this->message = other.Message;
                    this->Result = other.Result;
                }
                inline IntPtr MessageEventArgs::LParam::get() { return this->lParam; }
                inline IntPtr MessageEventArgs::WParam::get() { return this->wParam; }
                inline int MessageEventArgs::Message::get() { return this->message; }
#pragma endregion

#pragma region SearchDialogEventArgs
                SearchDialogEventArgs::SearchDialogEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow, bool findNext)
                    :ListerPluginUIEventArgs(pluginWindowHandle, pluginWindow) {
                    this->findNext = findNext;
                }
                SearchDialogEventArgs::SearchDialogEventArgs(SearchDialogEventArgs% other)
                    : ListerPluginUIEventArgs(Tools::ExtensionsT::General::ThrowIfNull(%other, "other")->PluginWindowHandle, other.PluginWindow) {
                    this->findNext = other.FindNext;
                }
                bool SearchDialogEventArgs::FindNext::get() { return this->findNext; }
#pragma endregion

#pragma region SearchDialogInfoEventArgs
                SearchDialogInfoEventArgs::SearchDialogInfoEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow, bool findNext, bool result)
                    :InfoEventArgs<SearchDialogEventArgs>(SearchDialogEventArgs(pluginWindowHandle, pluginWindow, findNext), result) {}
#pragma endregion

    }
}