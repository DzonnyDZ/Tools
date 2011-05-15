#pragma once

#include "..\Plugin\listplug.h"
#include "WLX Enums.h"
#include "IListerUI.h"

using namespace System;
using namespace System::ComponentModel;

namespace Tools{namespace TotalCommanderT{

    interface class IListerUI;

    /// <summary>Event arguments describing an environment to load lister plugin to</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    public ref class ListerPluginInitEventArgs : EventArgs, IListerUIInfo{
    private:
        initonly ListerShowFlags options;
        initonly IntPtr parentWindowHandle;
        initonly String^ fileToLoad;
        IListerUI^ pluginWindow;
    public:
        /// <summary>CTor - creates a new instance of the <see cref="ListerPluginInitEventArgs"/> class</summary>
        /// <param name="parentWindowHandle">Handler of lister lister's window</param>
        /// <param name="fileToLoad">The name and path of the file which has to be loaded</param>
        /// <param name="options">Flags indicating various options to for lister plugin being loaded</param>
        ListerPluginInitEventArgs(IntPtr parentWindowHandle, String^ fileToLoad, ListerShowFlags options);
        /// <summary>Gets flags indicating various options to for lister plugin being loaded. You can ignore the flags if you don't use them.</summary>
        /// <remarks>If <see cref="Options"/> has <see cref="ListerShowFlags::ForceShow"/> flag set, you may try to load the file even if the plugin wasn't made for it. Example: A plugin with line numbers may only show the file as such when the user explicitly chooses 'Image/Multimedia' from the menu.</remarks>
        property ListerShowFlags Options{virtual ListerShowFlags get() sealed;}
        /// <summary>Gets handler of lister lister's window. Create your plugin window as a child of this window.</summary>
        property IntPtr ParentWindowHandle{virtual IntPtr get() sealed;}
        /// <summary>Gets the name and path of the file which has to be loaded.</summary>
        property String^ FileToLoad{virtual String^ get() sealed = IListerUIInfo::FileName::get;}
        /// <summary>Set this property to instance of <see cref="IListerUI"/> implementation to indicate that your plugin is loaded. Set to (keep) null to indicate that plugin load failed (e.g. the plugin does not support file of given type).</summary>
        virtual property IListerUI^ PluginWindow;
    protected:
        /// <summary>Gets handle of control (window) representing plugin UI</summary>
        /// <returns>This implementation returns <see cref="PluginWindow"/>.<see cref="IListerUI::Handle">Handle</see>, <see cref="IntPtr::Zero"/> if <see cref="PluginWindow"/> is null.</returns>
        property IntPtr PluginWindowHandle{virtual IntPtr get() = IListerUIInfo::PluginWindowHandle::get;}
    };

    /// <summary>Event arguments describing environment for loading different file to a plugin</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    public ref class ListerPluginReInitEventArgs : ListerPluginInitEventArgs{
    private:
        initonly IntPtr pluginWindowHandle;
    public:
        /// <summary>CTor - creates a new instance of the <see cref="ListerPluginReInitEventArgs"/> class</summary>
        /// <param name="parentWindowHandle">Handler of lister lister's window</param>
        /// <param name="fileToLoad">The name and path of the file which has to be loaded</param>
        /// <param name="options">Flags indicating various options to for lister plugin being loaded</param>
        /// <param name="pluginWindowHandle">Handle of window (control) representing target plugin UI</param>
        /// <param name="pluginWindow">An object representing window (control) representing plugin UI. This is an object previously created in <see cref="ListerPluginBase::OnInit"/></param>
        ListerPluginReInitEventArgs(IntPtr parentWindowHandle, String^ fileToLoad, ListerShowFlags options, IntPtr pluginWindowHandle, IListerUI^ pluginWindow);
        /// <summary>Overrides setter of <see cref="ListerPluginInitEventArgs::PluginWindow"/> to make the property read-only</summary>
        /// <param name="">Ignored</param>
        /// <exception cref="NotSupportedException">Always, the <see cref="ListerPluginInitEventArgs::PluginWindow"/> property is read-only on <see cref="ListerPluginReInitEventArgs"/>.</exception>
        [EditorBrowsable(EditorBrowsableState::Never)]
        virtual void setPluginWindow(IListerUI^) sealed = ListerPluginInitEventArgs::PluginWindow::set;
        /// <summary>Gets handle of control (window) representing plugin UI</summary>
        /// <returns>Handle of <see cref="ListerPluginInitEventArgs::PluginWindow"/>, null in rare (impossible?) cases when Total Commander calls <see cref="ListerPluginBase::ListLoadNext"/> function for handles that are unknown to managed plugin framework.</returns>
        property IntPtr PluginWindowHandle{virtual IntPtr get() override;}
    };

    /// <summary>Event argumenst carrying lister plugin command</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    public ref class ListerCommandEventArgs : EventArgs{
    public:
        /// <summary>CTor - creates a new instance of the <see cref="ListerCommandEventArgs"/> class</summary>
        /// <param name="command">One of <see cref="ListerCommand"> commands</param>
        /// <param name="parameter">Used when <paramref name="command"/> is <see cref="ListerCommand::NewParams"/>. Combination of <see cref="ListerShowFlags"/>.</param>
        ListerCommandEventArgs(ListerCommand command, ListerShowFlags parameter);
    private:
        initonly ListerCommand command;
        initonly ListerShowFlags parameter;
    public:
        /// <summary>Gets of <see cref="ListerCommand"> commands - the command that occured</summary>
        property ListerCommand Command{ListerCommand get();}
        /// <summary>Gets combination of <see cref="ListerShowFlags"/>.</summary>
        /// <remarks>Used when <see cref="Command"/> is <see cref="ListerCommand::NewParams"/>.</remarks>
        property ListerShowFlags Parameter{ListerShowFlags get();}
    };

    /// <summary>Event argumens of lister print event</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    public ref class PrintEventArgs{
    public:
        /// <summary>CTor - creates a new instance of the <see cref="PrintEventArgs"/> class</summary>
        /// <param name="fileToPrint">The full name of the file which needs to be printed. This should be same file as the file opened in plugin UI.</param>
        /// <param name="defPrinter">Name of the printer currently chosen in Total Commander. May be null (use default printer).</param>
        /// <param name="printFlags">Currently not used (set to 0). May be used in a later version.</param>
        /// <param name="margins">The left, top, right and bottom margins of the print area (in 1/100 of inch).</param>
        PrintEventArgs(String^ fileToPrint, String^ defPrinter, PrintFlags printFlags, System::Drawing::Printing::Margins^ margins);
    private:
        initonly String^ fileToPrint;
        initonly String^ defPrinter;
        initonly PrintFlags printFlags;
        initonly System::Drawing::Printing::Margins^ margins;
    public:
        /// <summary>Gets the full name of the file which needs to be printed.</summary>
        /// <remarks>This should be same file as the file opened in plugin UI. Plugin UI implementation may either ignore it or throw an exception if it has unexpected value. It may, of course, also be aple to print any file passed to this path, but Total Commander should pass only original file paths here.</param>
        property String^ FileToPrint{String^ get();}
        /// <summary>Gets name of the printer currently chosen in Total Commander.</summary>
        /// <returns>Name of current printer selected in Total Commander. Null if default printer should be used.</returns>
        property String^ DefPrinter{String^ get();}
        /// <summary>Gets print flags</summary>
        /// <remarks>Currently not used (TC 7.56a / Plugin Interface 2.0). May be used in future versions of TC</remarks>
        [EditorBrowsable(EditorBrowsableState::Never)]
        property Tools::TotalCommanderT::PrintFlags PrintFlags{Tools::TotalCommanderT::PrintFlags get();}
        /// <summary>Gets the left, top, right and bottom margins of the print area (in 1/100 of inch).</summary>
        /// <remarks>May be ignored by plugin UI implementation</remarks>
        property System::Drawing::Printing::Margins^ Margins{System::Drawing::Printing::Margins^ get();}
    };
}}