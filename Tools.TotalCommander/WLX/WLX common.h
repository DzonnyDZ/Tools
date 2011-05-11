#pragma once

#include "..\Plugin\listplug.h"

using namespace System;

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

    /// <summary>This interface defines minimum required interface for an object representing user interface of lister plugin</summary>
    /// <remarks>You typically implement this interface as <see cref="T:System.Windows.Forms.Control"/>-derived class.</remarks>
    /// <version version="1.5.4">This interface is new in version 1.5.4</version>
    public interface struct IListerUI:IListerUIInfo{
        /// <summary>Gets handle of control (window) representing plugin UI</summary>
        /// <remarks>When implemented by a class implementing <see cref="T:System.Windows.Forms.IWin32Window"/> this property should be implemented by the same property as <see cref="T:System.Windows.Forms.IWin32Window.Handle"/>.
        /// This property is meant to implement <see cref="IListerUIInfo::PluginWindowHandle"/>.</remarks>
        /// <seelaso cref="T:System.Windows.Forms.IWin32Window.Handle"/>
        property IntPtr Handle{IntPtr get() = IListerUIInfo::PluginWindowHandle::get;}
    };

    /// <summary>Event arguments describing an environment to load lister plugin to</summary>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    public ref class ListerPluginInitEventArgs : EventArgs, IListerUIInfo{
    private:
        ListerShowFlags options;
        IntPtr parentWindowHandle;
        String^ fileToLoad;
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
        /// <summary>Set this property to instance of <see cref="IListerUI"/> implementation to indicate that your plugin is loaded. Set to (keep) nulll to indicate that plugin load failed (e.g. the plugin does not support file of given type).</summary>
        virtual property IListerUI^ PluginWindow;
    private:
        property IntPtr PluginWindowHandle{virtual IntPtr get() sealed = IListerUIInfo::PluginWindowHandle::get;}
    };
}}