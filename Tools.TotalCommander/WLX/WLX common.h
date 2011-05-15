#pragma once

#include "..\Plugin\listplug.h"

using namespace System;
using namespace System::ComponentModel;

namespace Tools{namespace TotalCommanderT{

    /// <summary>Flags indicating various options of Lister Plugin</summary>
    /// <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    [Flags]
    public enum class ListerShowFlags{
        /// <summary>No other options are selected</summary>
        none = 0,
        /// <summary>Text: Word wrap mode is checked</summary>
        WrapText = lcp_wraptext,
        /// <summary>Images: Fit image to window is checked</summary>
        FitToWindow = lcp_fittowindow,
        /// <summary>Fit image to window only if larger than the window. Always set together with <see cref="FitToWindow"/>.</summary>
        FitLargerOnly = lcp_fitlargeronly,
        /// <summary>Center image in viewer window</summary>
        Center = lcp_center,
        /// <summary>ANSI charset is checked</summary>
        Ansi = lcp_ansi,
        /// <summary>ASCII (DOS) charset is checked</summary>
        Ascii = lcp_ascii,
        /// <summary>Variable width charset is checked</summary>
        Variable = lcp_variable,
        /// <summary>User chose 'Image/Multimedia' from the menu.</summary>
        /// <remarks>When this flag is ste you may attempt to load your plugin even for unsupported file type.</remarks>
        ForceShow = lcp_forceshow
    };

    ref class ListerPluginReInitEventArgs;

    /// <summary>Provides basic information about lister plugin user interface</summary>
    /// <version version="1.5.4">This interface is new in version 1.5.4</version>
    public interface struct IListerUIInfo{
        /// <summary>Gets handle of control (window) representing plugin UI</summary>
        property IntPtr PluginWindowHandle{IntPtr get();}
        /// <summary>Gets name of file the plugin shows</summary>
        property String^ FileName{String^ get();}
        /// <summary>Gets load options of the plugin</summary>
        property ListerShowFlags Options{ListerShowFlags get();}
        /// <summary>Gets handle of parent window (the lister window this plugin UI is loaded in)</summary>
        property IntPtr ParentWindowHandle{IntPtr get();}
    };

    /// <summary>Test serach options used by Lister plugin</summary>
    /// <version version="1.5.4">This enum is new in version 1.5.4</version>
    [Flags]
    public enum class TextSearchOptions{
        /// <summary>Search from the beginning of the first displayed line (not set: find next)</summary>
        FindFirst = lcs_findfirst,
        /// <summary>The search string is to be treated case-sensitively.</summary>
        MatchCase = lcs_matchcase,
        /// <summary>Find whole words only.</summary>
        WholeWords = lcs_wholewords,
        /// <summary>Search backwards towards the beginning of the file.</summary>
        Backwards = lcs_backwards
    };

    /// <summary>Enumerates predefined Lister plugin commands</summary>
    /// <version version="1.5.4">This enum is new in version 1.5.4</version>
    public enum class ListerCommand{
        /// <summary>Copy current selection to the clipboard</summary>
        Copy = lc_copy,
        /// <summary>New parameters passed to plugin</summary>
        NewParams = lc_newparams,
        /// <summary>Select the whole contents</summary>
        SelectAll = lc_selectall,
        /// <summary>Go to new position in document (in percent).</summary>
        SetPercent = lc_setpercent
    };

    ref class ListerCommandEventArgs;

    /// <summary>This interface defines minimum required interface for an object representing user interface of lister plugin</summary>
    /// <remarks>You typically implement this interface as <see cref="T:System.Windows.Forms.Control"/>-derived class.</remarks>
    /// <version version="1.5.4">This interface is new in version 1.5.4</version>
    public interface struct IListerUI : IListerUIInfo{
        /// <summary>Gets handle of control (window) representing plugin UI</summary>
        /// <remarks>When implemented by a class implementing <see cref="T:System.Windows.Forms.IWin32Window"/> this property should be implemented by the same property as <see cref="T:System.Windows.Forms.IWin32Window.Handle"/>.
        /// This property is meant to implement <see cref="IListerUIInfo::PluginWindowHandle"/>.</remarks>
        /// <seelaso cref="T:System.Windows.Forms.IWin32Window.Handle"/>
        property IntPtr Handle{IntPtr get() = IListerUIInfo::PluginWindowHandle::get;}
        /// <summmary>Called by Total Commander plugin implementation when user wants to load different file to the same plugin instance</summary>
        /// <param name="e">Event arguments describe conditions of loading new file</param>
        /// <returns>True when the file was successfully loaded, false when this implementation cannot load it. Returning false is equivalent of throwing anny exception but <see cref="NotSupportedException"/>.</returns>
        /// <exception cref="Exception">Any axception but <see cref="NotSupportedException"/> and exceptions that are not caught by .NET framework by default can be thrown from implementing method to indicate that the file cannot be loaded. It's equivalent to returning false from this method.</exception>
        bool LoadNext(ListerPluginReInitEventArgs^ e);
        /// <summary>Called just before the lister UI is unloaded</summary>
        /// <remarks>There is no way to prevent unload! Just do any necessary cleanup in this method because the control/window/UI is gonna be destroyed using <c>DestroyWindow</c>.</remarks>
        void OnBeforeClose();
        /// <summary>Called when the user tries to find text in the plugin.</summary>
        /// <param name="searchString">String to search for</param>
        /// <param name="searchParameter">Indicates search options</param>
        /// <returns>True if search succeeded, false if it failed (nothing was found). Returning false is equivalent to throwing  any exception but <see cref="NotSupportedException"/>.</returns>
        /// <exception cref="Exception">Any other exception but <see cref="NotSupportedException"/> and exceptions usually not caught in .NET framework (such as <see cref="StackOverflowException"/>) can be thrown by implementation of this method to indicate search failure.</exception>
        /// <remarks>If your plugin does not support search, just implement this method always returning false and do not override <see cref="ListerPlugin::SearchText"/>.</remarks>
        bool SearchText(String^ serachString, TextSearchOptions searchParameter); 
        /// <summary>Called when the user changes some options in Lister's menu.</summary>
        /// <param name="e">Event arguments</param>
        /// <returns>True if command succeeded, false if it failed. Returning false is equivalent to throwing  any exception but <see cref="NotSupportedException"/>.</returns>
        /// <exception cref="Exception">Any other exception but <see cref="NotSupportedException"/> and exceptions usually not caught in .NET framework (such as <see cref="StackOverflowException"/>) can be thrown by implementation of this method to indicate commmand failure.</exception>
        /// <remarks>If your plugin does not support commanding, just implement this method always returning false and do not override <see cref="ListerPlugin::SendCommand"/>.</remarks>
        bool OnCommand(ListerCommandEventArgs^ e);
    };

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
        /// <param name="pluginWindow">An object representing window (control) representing plugin UI. This is an object previously created in <see cref="ListerPlugin::OnInit"/></param>
        ListerPluginReInitEventArgs(IntPtr parentWindowHandle, String^ fileToLoad, ListerShowFlags options, IntPtr pluginWindowHandle, IListerUI^ pluginWindow);
        /// <summary>Overrides setter of <see cref="ListerPluginInitEventArgs::PluginWindow"/> to make the property read-only</summary>
        /// <param name="">Ignored</param>
        /// <exception cref="NotSupportedException">Always, the <see cref="ListerPluginInitEventArgs::PluginWindow"/> property is read-only on <see cref="ListerPluginReInitEventArgs"/>.</exception>
        [EditorBrowsable(EditorBrowsableState::Never)]
        virtual void setPluginWindow(IListerUI^) sealed new = ListerPluginInitEventArgs::PluginWindow::set;
        /// <summary>Gets handle of control (window) representing plugin UI</summary>
        /// <returns>Handle of <see cref="ListerPluginInitEventArgs::PluginWindow"/>, null in rare (impossible?) cases when Total Commander calls <see cref="ListerPlugin::ListLoadNext"/> function for handles that are unknown to managed plugin framework.</returns>
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
}}