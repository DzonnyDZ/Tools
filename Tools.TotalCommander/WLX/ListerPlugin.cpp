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

namespace Tools {
    namespace TotalCommanderT {

        generic <class TUI>
            ListerPlugin<TUI>::ListerPlugin() :ListerPluginBase() {}

            generic <class TUI>
                void ListerPlugin<TUI>::OnInit(ListerPluginInitEventArgs^ e) {
                    ListerPluginInitEventArgsSpecialized<TUI>^ e2 = gcnew ListerPluginInitEventArgsSpecialized<TUI>(e->ParentWindowHandle, e->FileToLoad, e->Options);
                    OnInit(e2);
                    e->PluginWindow = e2->PluginWindow;
                }

                generic <class TUI>
                    inline bool ListerPlugin<TUI>::LoadNext(ListerPluginReInitEventArgs^ e) { return __super::LoadNext(e); }

                    generic <class TUI>
                        inline bool ListerPlugin<TUI>::SearchText(IListerUI^ listerUI, IntPtr listerUIHandle, String^ searchString, TextSearchOptions searchParameter) {
                            return __super::SearchText(listerUI, listerUIHandle, searchString, searchParameter);
                        }

                        generic <class TUI>
                            inline bool ListerPlugin<TUI>::SendCommand(IListerUI^ listerUI, IntPtr listerUIHandle, ListerCommand command, ListerShowFlags parameter) {
                                return __super::SendCommand(listerUI, listerUIHandle, command, parameter);
                            }

                            generic <class TUI>
                                inline bool ListerPlugin<TUI>::Print(IListerUI^ listerUI, IntPtr listerUIHandle, String^ fileToPrint, String^ defPrinter, PrintFlags printFlags, System::Drawing::Printing::Margins^ margins) {
                                    return __super::Print(listerUI, listerUIHandle, fileToPrint, defPrinter, printFlags, margins);
                                }

                                generic <class TUI>
                                    inline int ListerPlugin<TUI>::NotificationReceived(IListerUI^ listerUI, IntPtr listerUIHandle, int message, UIntPtr wParam, IntPtr lParam) {
                                        return __super::NotificationReceived(listerUI, listerUIHandle, message, wParam, lParam);
                                    }

                                    generic <class TUI>
                                        inline bool ListerPlugin<TUI>::ShowSearchDialog(IListerUI^ listerUI, IntPtr listerUIHandle, bool findNext) {
                                            return __super::ShowSearchDialog(listerUI, listerUIHandle, findNext);
                                        }

    }
}