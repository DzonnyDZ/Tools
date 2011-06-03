#pragma once

#include "..\Plugin\listplug.h"
#include "WLX Enums.h"
#include "IListerUI.h"

using namespace System;
using namespace System::ComponentModel;

namespace Tools{namespace TotalCommanderT{

    interface class IListerUI;

    /// <summary>Common base class for <see cref="IListerUI"/>-related event arguments</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    public ref class ListerPluginUIEventArgs: EventArgs{
    private:
        initonly IntPtr pluginWindowHandle;
        initonly IListerUI^ pluginWindow;
    protected:
        /// <summary>CTor - creates a new instance of the <see cref="ListerPluginUIEventArgs"/> class</summary>
        ListerPluginUIEventArgs();
    public:
        /// <summary>CTor - creates a new instance of the <see cref="ListerPluginUIEventArgs"/> class from <see cref="IListerUI"/></summary>
        /// <param name="pluginWindow"/>An instance of <see cref="IListerUI"/> an event relates to. May be null.</param>
        ListerPluginUIEventArgs(IListerUI^ pluginWindow);
        /// <summary>CTor - creates a new instance of the <see cref="ListerPluginUIEventArgs"/> class from <see cref="IListerUI"/> and handle</summary>
        /// <param name="pluginWindow"/>An instance of <see cref="IListerUI"/> an event relates to. May be null.</param>
        /// <param name="pluginWindowHandle">Handle to plugin UI this instance relates to. If <paramref name="pluginWindow"/> is not null must be same as <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</param>
        /// <exception cref="ArgumentException"><paramref name="pluginWindow"/> is not null and <paramref name="pluginWindowHandle"/> is different than <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</exception>
        ListerPluginUIEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow);
    public:
        property IntPtr PluginWindowHandle{virtual IntPtr get();}
        property IListerUI^ PluginWindow{virtual IListerUI^ get();}
    };

    /// <summary>Common template class for info event arguments</summary>
    /// <typeparam name="T">Type of base class to extend with <c>Result</c> property. Should be <see cref="ListerPluginUIEventArgs"/>-derived.</typeparam>
    /// <remarks>Info event arguments are <see cref="ListerPluginUIEventArgs"/>-derived-derived classes which has one more property than parent class - <see cref="InfoEventArgs{T}::Result"/></remarks>
    /// <version version="1.5.4">This template class is new in version 1.5.4</version>
    template<class T>
    public ref class InfoEventArgs: T{
    private:
        initonly bool result;
    public:
        /// <summary>CTor - creates a new instance of <see cref="InfoEventArgs"/> template class</summary>
        /// <param name="base">Base class value to read parameters from</param>
        /// <param name="result">A boolean value indicating result (success)</param>
        /// <exception cref="ArgumentNullException"><paramref name="base"/> is null</exception>
        InfoEventArgs(T base, bool result);
        ///// <summary>Copy CTor - creates a new instance of the <see cref="InfoEventArgs{T}"/> as copy of existing instance</summary>
        ///// <param name="other">An instance to clone</param>
        ///// <exception cref="ArgumentNullException"><paramref name="other"/> is null</exception
        //InfoEventArgs(const InfoEventArgs<T>% other);
        /// <summary>Gets value indicating if operation succeeded</summary>
        /// <returns>True if operation succeeded, false if it failed. Returns fals also when operation failed due to being requestedn on UI handle that is not registered with managed plugin framework</returns>
        property bool Result{virtual bool get();}
    };

    /// <summary>Event arguments describing an environment to load lister plugin to</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    public ref class ListerPluginInitEventArgs : ListerPluginUIEventArgs, IListerUIInfo{
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
        /// <value>Instance of <see cref="IListerUI"/> implementation if plugin was loaded, nulll if load failed</value>
        virtual property IListerUI^ PluginWindow{IListerUI^ get() override; void set(IListerUI^) override;}
        /// <summary>Gets handle of control (window) representing plugin UI</summary>
        /// <returns>This implementation returns <see cref="PluginWindow"/>.<see cref="IListerUI::Handle">Handle</see>, <see cref="IntPtr::Zero"/> if <see cref="PluginWindow"/> is null.</returns>
        property IntPtr PluginWindowHandle{virtual IntPtr get() override = IListerUIInfo::PluginWindowHandle::get;}
    };

    /// <summary>Generic specialized type-safe implementation of <see cref="ListerPluginInitEventArgs"/> class</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    generic<class TUI> where TUI: IListerUI, ref class
    public ref class ListerPluginInitEventArgsSpecialized : ListerPluginInitEventArgs{
    public:
        /// <summary>CTor - creates a new instance of the <see cref="ListerPluginInitEventArgs"/> class</summary>
        /// <param name="parentWindowHandle">Handler of lister lister's window</param>
        /// <param name="fileToLoad">The name and path of the file which has to be loaded</param>
        /// <param name="options">Flags indicating various options to for lister plugin being loaded</param>
        ListerPluginInitEventArgsSpecialized(IntPtr parentWindowHandle, String^ fileToLoad, ListerShowFlags options);
        /// <summary>Internally implements the <see cref="ListerPluginInitEventArgs::PluginWindow"/> property</summary>
        /// <exception cref="TypeMismatchException">Value being set is not of type <typeparamref name="TUI"/></exception>
        [EditorBrowsable(EditorBrowsableState::Never)]
        property IListerUI^ PluginWindowInternal{virtual void set(IListerUI^) sealed = ListerPluginInitEventArgs::PluginWindow::set;}
        /// <summary>Set this property to instance of <typeparamref name="TUI"/> to indicate that your plugin is loaded. Set to (keep) null to indicate that plugin load failed (e.g. the plugin does not support file of given type).</summary>
        /// <value>Instance of <typeparamref name="TUI"/> if plugin was loaded, nulll if load failed</value>
        property TUI PluginWindow{TUI get() new; void set(TUI) new;}
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
        /// <summary>Copy CTor - creates a new instance of the <see cref="ListerPluginReInitEventArgs"/> as copy of existing instance</summary>
        /// <param name="other">An instance to clone</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null</exception>
        ListerPluginReInitEventArgs(ListerPluginReInitEventArgs% other);
    public:
        /// <summary>Overrides setter of <see cref="ListerPluginInitEventArgs::PluginWindow"/> to make the property read-only</summary>
        /// <param name="">Ignored</param>
        /// <exception cref="NotSupportedException">Always, the <see cref="ListerPluginInitEventArgs::PluginWindow"/> property is read-only on <see cref="ListerPluginReInitEventArgs"/>.</exception>
        [EditorBrowsable(EditorBrowsableState::Never)]
        virtual void setPluginWindow(IListerUI^) sealed = ListerPluginInitEventArgs::PluginWindow::set;
        /// <summary>Gets handle of control (window) representing plugin UI</summary>
        /// <returns>Handle of <see cref="ListerPluginInitEventArgs::PluginWindow"/>, null in rare (impossible?) cases when Total Commander calls <see cref="ListerPluginBase::ListLoadNext"/> function for handles that are unknown to managed plugin framework.</returns>
        property IntPtr PluginWindowHandle{virtual IntPtr get() override;}
    };
    
    /// <summary>Event argumenst describing environment for and result of lloading different file to a plugin</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    public ref class ListerPluginReInitInfoEventArgs: InfoEventArgs<ListerPluginReInitEventArgs>{
    public:
        /// <summary>CTor - creates a new instance of the <see cref="ListerPluginReInitInfoEventArgs"/> class</summary>
        /// <param name="parentWindowHandle">Handler of lister lister's window</param>
        /// <param name="fileToLoad">The name and path of the file which has to be loaded</param>
        /// <param name="options">Flags indicating various options to for lister plugin being loaded</param>
        /// <param name="pluginWindowHandle">Handle of window (control) representing target plugin UI</param>
        /// <param name="pluginWindow">An object representing window (control) representing plugin UI. This is an object previously created in <see cref="ListerPluginBase::OnInit"/></param>
        /// <param name="result">A value indicating if current lister plugin implementation loaded the plugin or not</param>
        ListerPluginReInitInfoEventArgs(IntPtr parentWindowHandle, String^ fileToLoad, ListerShowFlags options, IntPtr pluginWindowHandle, IListerUI^ pluginWindow, bool result);
    };

    /// <summary>Event argumenst carrying lister plugin command</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    public ref class ListerCommandEventArgs : ListerPluginUIEventArgs{
    public:
        /// <summary>CTor - creates a new instance of the <see cref="ListerCommandEventArgs"/> class</summary>
        /// <param name="command">One of <see cref="ListerCommand"> commands</param>
        /// <param name="parameter">Used when <paramref name="command"/> is <see cref="ListerCommand::NewParams"/>. Combination of <see cref="ListerShowFlags"/>.</param>
        /// <param name="pluginWindow"/>An instance of <see cref="IListerUI"/> an event relates to. May be null.</param>
        /// <param name="pluginWindowHandle">Handle to plugin UI this instance relates to. If <paramref name="pluginWindow"/> is not null must be same as <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</param>
        /// <exception cref="ArgumentException"><paramref name="pluginWindow"/> is not null and <paramref name="pluginWindowHandle"/> is different than <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</exception>
        ListerCommandEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow, ListerCommand command, ListerShowFlags parameter);
        /// <summary>Copy CTor - creates a new instance of the <see cref="ListerCommandEventArgs"/> as copy of existing instance</summary>
        /// <param name="other">An instance to clone</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null</exception>
        ListerCommandEventArgs(ListerCommandEventArgs% other);
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

    /// <summary>Event argumenst carrying lister plugin command and it's result</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    public ref class ListerCommandInfoEventArgs:InfoEventArgs<ListerCommandEventArgs>{
    public:
        /// <summary>CTor - creates a new instance of the <see cref="ListerCommandEventArgs"/> class</summary>
        /// <param name="command">One of <see cref="ListerCommand"> commands</param>
        /// <param name="parameter">Used when <paramref name="command"/> is <see cref="ListerCommand::NewParams"/>. Combination of <see cref="ListerShowFlags"/>.</param>
        /// <param name="pluginWindow"/>An instance of <see cref="IListerUI"/> an event relates to. May be null.</param>
        /// <param name="pluginWindowHandle">Handle to plugin UI this instance relates to. If <paramref name="pluginWindow"/> is not null must be same as <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</param>
        /// <param name="result">Value indicating if the command was completed successfullly</param>
        /// <exception cref="ArgumentException"><paramref name="pluginWindow"/> is not null and <paramref name="pluginWindowHandle"/> is different than <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</exception>
        ListerCommandInfoEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow, ListerCommand command, ListerShowFlags parameter, bool result);        
    };

    /// <summary>Event argumens of lister print event</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    public ref class PrintEventArgs:ListerPluginUIEventArgs{
    public:
        /// <summary>CTor - creates a new instance of the <see cref="PrintEventArgs"/> class</summary>
        /// <param name="fileToPrint">The full name of the file which needs to be printed. This should be same file as the file opened in plugin UI.</param>
        /// <param name="defPrinter">Name of the printer currently chosen in Total Commander. May be null (use default printer).</param>
        /// <param name="printFlags">Currently not used (set to 0). May be used in a later version.</param>
        /// <param name="margins">The left, top, right and bottom margins of the print area (in 1/100 of inch).</param>
        /// <param name="pluginWindow"/>An instance of <see cref="IListerUI"/> an event relates to. May be null.</param>
        /// <param name="pluginWindowHandle">Handle to plugin UI this instance relates to. If <paramref name="pluginWindow"/> is not null must be same as <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</param>
        /// <exception cref="ArgumentException"><paramref name="pluginWindow"/> is not null and <paramref name="pluginWindowHandle"/> is different than <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</exception>
        PrintEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow, String^ fileToPrint, String^ defPrinter, PrintFlags printFlags, System::Drawing::Printing::Margins^ margins);
        /// <summary>Copy CTor - creates a new instance of the <see cref="PrintEventArgs"/> as copy of existing instance</summary>
        /// <param name="other">An instance to clone</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null</exception>
        PrintEventArgs(PrintEventArgs% other);
    private:
        initonly String^ fileToPrint;
        initonly String^ defPrinter;
        initonly Tools::TotalCommanderT::PrintFlags printFlags;
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

    /// <summary>Event argumens of lister print event and its result</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    public ref struct PrintInfoEventArgs:InfoEventArgs<PrintEventArgs>{
        /// <summary>CTor - creates a new instance of the <see cref="PrintInfoEventArgs"/> class</summary>
        /// <param name="fileToPrint">The full name of the file which needs to be printed. This should be same file as the file opened in plugin UI.</param>
        /// <param name="defPrinter">Name of the printer currently chosen in Total Commander. May be null (use default printer).</param>
        /// <param name="printFlags">Currently not used (set to 0). May be used in a later version.</param>
        /// <param name="margins">The left, top, right and bottom margins of the print area (in 1/100 of inch).</param>
        /// <param name="pluginWindow"/>An instance of <see cref="IListerUI"/> an event relates to. May be null.</param>
        /// <param name="pluginWindowHandle">Handle to plugin UI this instance relates to. If <paramref name="pluginWindow"/> is not null must be same as <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</param>
        /// <exception cref="ArgumentException"><paramref name="pluginWindow"/> is not null and <paramref name="pluginWindowHandle"/> is different than <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</exception>
        /// <param name="result">Result of the print operation reported by plugin UI</param>
        PrintInfoEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow, String^ fileToPrint, String^ defPrinter, Tools::TotalCommanderT::PrintFlags printFlags, System::Drawing::Printing::Margins^ margins, bool result);
    };

    /// <summary>Event arguments containing parameters of text search event for Lister plugin</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version> 
    public ref class TextSearchEventArgs: ListerPluginUIEventArgs{
    private:
        initonly String^ searchString;
        initonly TextSearchOptions searchParameter;
    public:
        /// <summary>CTor - creates a new instance of the <see cref="TextSearchEventArgs"/> class</summary>
        /// <param name="pluginWindow"/>An instance of <see cref="IListerUI"/> an event relates to. May be null.</param>
        /// <param name="pluginWindowHandle">Handle to plugin UI this instance relates to. If <paramref name="pluginWindow"/> is not null must be same as <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</param>
        /// <param name="searchString">String to search for</param>
        /// <param name="searchParameter">Indicates search options</param>
        /// <exception cref="ArgumentException"><paramref name="pluginWindow"/> is not null and <paramref name="pluginWindowHandle"/> is different than <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</exception>
        TextSearchEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow, String^ searchString, TextSearchOptions searchParameter);
        /// <summary>Copy CTor - creates a new instance of the <see cref="TextSearchEventArgs"/> as copy of existing instance</summary>
        /// <param name="other">An instance to clone</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null</exception>
        TextSearchEventArgs(TextSearchEventArgs% other);

        /// <summary>Gets sring to search for</summary>
        property String^ SearchString{String^ get();}
        /// <summary>Gets text search options</summary>
        property TextSearchOptions SearchParameter{TextSearchOptions get();}     
    };

    /// <summary>Event arguments containing parameters and result of text search event for Lister plugin</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    public ref class TextSearchInfoEventArgs:InfoEventArgs<TextSearchEventArgs>{
    public:
        /// <summary>CTor - creates a new instance of the <see cref="TextSearchEventArgs"/> class</summary>
        /// <param name="pluginWindow"/>An instance of <see cref="IListerUI"/> an event relates to. May be null.</param>
        /// <param name="pluginWindowHandle">Handle to plugin UI this instance relates to. If <paramref name="pluginWindow"/> is not null must be same as <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</param>
        /// <param name="searchString">String to search for</param>
        /// <param name="searchParameter">Indicates search options</param>
        /// <param name="result">Result of search operation</param>
        /// <exception cref="ArgumentException"><paramref name="pluginWindow"/> is not null and <paramref name="pluginWindowHandle"/> is different than <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</exception>
        TextSearchInfoEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow, String^ searchString, TextSearchOptions searchParameter, bool result);
    };

    /// <summary>Event arguments describing windows message</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    public ref class MessageEventArgs : ListerPluginUIEventArgs{
    private:
        IntPtr lParam;
        Tools::NumericsT::NativeUnion wParam;
        int message;
    public:
        /// <summary>CTor - creates a new instance of the <see cref="MessageEventArgs"/> class</summary>
        /// <param name="pluginWindow"/>An instance of <see cref="IListerUI"/> an event relates to. May be null.</param>
        /// <param name="pluginWindowHandle">Handle to plugin UI this instance relates to. If <paramref name="pluginWindow"/> is not null must be same as <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</param>
        /// <param name="message">The received message</param>
        /// <param name="wParam">The wParam parameter of the message.</param>
        /// <param name="lParam">The lParam parameter of the message.</param>
        /// <exception cref="ArgumentException"><paramref name="pluginWindow"/> is not null and <paramref name="pluginWindowHandle"/> is different than <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</exception>
        [CLSCompliant(false)]
        MessageEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow , int message, UIntPtr wParam, IntPtr lParam);
        /// <summary>CTor - creates a new instance of the <see cref="MessageEventArgs"/> class (CLS-compliant version)</summary>
        /// <param name="pluginWindow"/>An instance of <see cref="IListerUI"/> an event relates to. May be null.</param>
        /// <param name="pluginWindowHandle">Handle to plugin UI this instance relates to. If <paramref name="pluginWindow"/> is not null must be same as <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</param>
        /// <param name="message">The received message</param>
        /// <param name="wParam">The wParam parameter of the message.</param>
        /// <param name="lParam">The lParam parameter of the message.</param>
        /// <exception cref="ArgumentException"><paramref name="pluginWindow"/> is not null and <paramref name="pluginWindowHandle"/> is different than <paramref name="pluginWindow"/>.<see cref="ILitserUI.Handle">Handle</see>.</exception>
        MessageEventArgs(IntPtr pluginWindowHandle, IListerUI^ pluginWindow , int message, IntPtr wParam, IntPtr lParam);
        /// <summary>Copy CTor - creates a new instance of the <see cref="MessageEventArgs"/> as copy of existing instance</summary>
        /// <param name="other">An instance to clone</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null</exception>
        MessageEventArgs(MessageEventArgs% other);
        /// <summary>Gets lParam of the message</summary>
        /// <remarks>Meaning of the value depends on <see cref="Message"/></remarks>
        property IntPtr LParam{IntPtr get();}
        /// <summary>Gets wParam of the message</summary>
        /// <remarks>Meaning of the value depends on <see cref="Message"/></remarks>
        property IntPtr WParam{IntPtr get();}
        /// <summary>Gets the message code</summary>
        property int Message{int get();}
        /// <summary>Gets or sets result of the message</summary>
        /// <remarks>Meaning of the value depends on <see cref="Message"/>.
        /// <para>If value of this property is not set to non-null in event handler caller supplies default value for the message with meaning that the message was not processed.</para></remarks>
        property Nullable<IntPtr> Result;
    };

}}