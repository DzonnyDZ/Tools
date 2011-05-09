#pragma once

#include "..\Plugin\fsplugin.h"
#include "..\Common.h"
#include "..\ContentPluginBase.h"
#include "WLX common.h"
#include "..\Attributes.h"
#include "WlxFunctions.h"

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections::Generic;


//TODO: CHange plugin phylosophy: One plugin instance per one TC instance. The one instance loads multiple windows.

namespace Tools{namespace TotalCommanderT{

    /// <summary>Abstract base class for Total Commander lister plugins (wlx)</summary>
    /// <remarks>See <see2 cref2="T:Tools.TotalCommanderT.PluginBuilder.Generator"/> for more information about how to generate Total Commander plugin from .NET.
    /// <para>To keep fingerprint and dependenciew of Tools.TotalCommander.dll assembly low, this class does not provide some utility functions. You may consider deriving from derived class from Tools.TotalCommander.Extensions.dll instead.</para></remarks>
    /// <version version="1.5.4">This class is new in version 1.5.4</version>
    public ref class ListerPlugin abstract : PluginBase {
    private:
        WlxFunctions implementedFunctions;
    public:
        /// <summary>Gets flags indicating which of supported Total Commander files system functions (from TC plugin interface point-of-view) are implemented by current plugin</summary>
        property WlxFunctions ImplementedFunctions{ WlxFunctions get(); }
        /// <summary>Gets basic type of the plugin</summary>
        /// <returns>This implementation always returns <see cref="Tools::TotalCommanderT::PluginType::Lister"/></returns>
        virtual property Tools::TotalCommanderT::PluginType PluginType{Tools::TotalCommanderT::PluginType get() override sealed;}
        /// <summary>Gets a base class of plugin type</summary>
        /// <returns>This implementation always returns <seee cref="ListerPlugin"/></returns>
        virtual property Type^ PluginBaseClass{Type^ get() override sealed;}
#pragma region Load
    protected:
        /// <summary>CTor - creates a new instance of the <see cref="ListerPlugin"/> class</summary>
        /// <remarks>The plugin instance is not ready to be used untill <see cref="OnInit"/> is called.</remarks>
        ListerPlugin();
    public:
        /// <summary>Called when a user opens lister with F3 or the Quick View Panel with Ctrl+Q, and when the definition string either doesn't exist, or its evaluation returns true.</summary>
        /// <param name="parentWin">This is lister's window. Create your plugin window as a child of this window.</param>
        /// <param name="fileToLoad">The name of the file which has to be loaded.</param>
        /// <param name="showFlags">A combination of <see cref="ListerShowFlags"/> flags. You may ignore these parameters if they don't apply to your document type.</param>
        /// <param name="wide">True when plugin is loaded for Unicode environment, false if it is loaded for ANSI environment. This parameter is not part of Total Commander plugin interface constract, but it is used to distinguish between the two situations.</param>
        /// <returns>Return a handle to your window if load succeeds, null otherwise. If null is returned, Lister will try the next plugin.</returns>
        /// <exception cref="InvalidOperationException"><see cref="Initialized"/> is true</exception>
        /// <remarks>
        /// <para>Lister will subclass your window to catch some hotkeys like 'n' or 'p'.</para>
        /// <para>When lister is activated, it will set the focus to your window. If your window contains child windows, then make sure that you set the focus to the correct child when your main window receives the focus!</para>
        /// <para>If lcp_forceshow is defined, you may try to load the file even if the plugin wasn't made for it. Example: A plugin with line numbers may only show the file as such when the user explicitly chooses 'Image/Multimedia' from the menu.</para>
        /// <para>Lister plugins which only create thumbnail images do not need to implement this function. To instantiate a plugin outside Total Commander from managed code use <see cref="Load"/>.</para>
        /// <para>This function is called by Total Commander and is not intended for direct use.</para>
        /// <para>This plugin function is implemented by <see cref="Load"/>.</para>
        /// </remarks>
        /// <seealso cref="Load"/><seealso cref="OnLoad"/>
        [EditorBrowsable(EditorBrowsableState::Never)]
        [CLSCompliant(false)]
        [PluginMethod("OnInit", "TC_L_LOAD")]
        HWND ListLoad(HWND parentWin, wchar_t* fileToLoad, int showFlags, bool wide);
        /// <summary>Called when user opens lister. This function is intended for loading plugins from managed environment.</summary>
        /// <param name="parentWin">Handle to lister window. Create your plugin window as a child of this window.</param>
        /// <param name="fileToLoad">The name of the file which has to be loaded.</param>
        /// <param name="showFlags">A combination of <see cref="ListerShowFlags"/> flags.</param>
        /// <returns>Handle to window created by the plugin if plugin is loaded successfully, <see cref="IntPtr::Zero"/> otherwise. If <see cref="IntPtr::Zero"/> is returned it' signal for owner application to try next plugin.</returns>
        /// <exception cref="InvalidOperationException"><see cref="Initialized"/> is true</exception>
        /// <exception cref="ArgumentNullException"><paramref name="parentWin"/> is <see cref="IntPtr::Zero"/> or <paramref name="fileToLoad"/> is null.</exception>
        /// <remarks>Use this function when loading plugin from managed environment instead of <see cref="ListLoad"/>. For details about this function see <see cref="ListLoad"/></remarks>
        /// <seealso cref="ListLoad"/><seealso cref="OnLoad"/>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        IntPtr Load(IntPtr parentWin, String^ fileToLoad, ListerShowFlags showFlags);
    private:
        IntPtr parentWindowHandle;
        String^ fileName;
        ListerShowFlags options;
        IntPtr controlHandle;
    public:
        /// <summary>Get handle to parent window lister plugin UI should be rendered inside</summary>
        /// <returns>Handle to parent window. <see cref="IntPtr::Zero"/> if <see cref="Initialized"/> is false.</returns>
        property IntPtr ParentWindowHandle{ IntPtr get(); }
        /// <summary>Gets path of file to provide preview for</summary>
        /// <returns>Path of file to provide preview for. Null if <see cref="Initialized"/> is false.</returns>
        property String^ FileName{ String^ get(); }
        /// <summary>Gets options set to the plugin at time of load</summary>
        /// <returns>Plugin options set when Total Commander initialized it. <see cref="ListerShowFlags::none"/> if <see cref="Initialized"/> si false.</returns>
        property ListerShowFlags Options{ ListerShowFlags get(); }
        /// <summary>After successfull initialization retuns handle of control which represents user interface of the plugin</summary>
        /// <returns>Handle of a control which represents a user interface of the plugin instance.
        /// Returns <see cref="IntPtr::Zero"/> when <see cref="OnInit"/> was not called yet or it was called but returned <see cref="IntPtr::Zero"/>.</returns>
        property IntPtr ControlHandle{ IntPtr get(); }
    private:
        bool initialized;
        bool unicode;
        bool isInTotalCommander;
    public:
        /// <summary>Gets value indicating if this plugin instance was initialized or not</summary>
        property bool Initialized{virtual bool get() override sealed;}
    protected:
        /// <summary>Internal initialization function. Never call it. Use <see cref="OnInit"/> instead.</summary>
        /// <remarks>This implementation does nothing. This method overrides <see cref="PluginBase::OnInit"/></remarks>
        [EditorBrowsable(EditorBrowsableState::Never)]
        virtual void OnInitInternal() sealed = PluginBase::OnInit;
        /// <summary>When overriden in derived class called when user opens lister.</summary>
        /// <returns>
        /// Handle of window (control) that lister plugin uses to display it's content.
        /// <note>For Win32 point-of-view WinForms controls are windows, WPF controls are not windows. WinForms froms are windows, WPF windows are windows (but it needs some work to get handle from them).</note>
        /// Returns <see cref="IntPtr::Zero"/> if plugin load was unsuccessfull (i.e. plugin does not support this file type). In this case Total Commander will attempt to load next plugin.
        /// </returns>
        /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
        /// <remarks>
        /// Use <see cref="ParentWindowHandle"/>, <see cref="FileName"/> and <see cref="Options"/> properties to detect which file and under which conditions you should display.
        /// <para>You tipically MUST override this function in your derived plugin class. Lister plugins which only create thumbnail images do not need to implement this function. But then plugin functionality is limited.</para>
        /// <para>When this method is called, <see cref="Initialized"/> is true.</para>
        /// <para>Lister will subclass your window to catch some hotkeys like 'n' or 'p'.</para>
        /// <para>When lister is activated, it will set the focus to your window. If your window contains child windows, then make sure that you set the focus to the correct child when your main window receives the focus!</para>
        /// <para>If <see cref="Options"/> has <see cref="ListerShowFlags::ForceShow"/> flag set, you may try to load the file even if the plugin wasn't made for it. Example: A plugin with line numbers may only show the file as such when the user explicitly chooses 'Image/Multimedia' from the menu.</para>
        /// <note type="inheritinfo">Do not throw any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note>
        /// <note>WinForms controls can be placed directly inside lister window. With WPF controls some kind of interop is necessary - the easiest option is probably to place WPF control onto WinForms control.</note>
        /// <para>Please note that multiple Lister windows can be open at the same time!</para>
        /// </remarks>
        /// <seealso cref="Load"/><seealso cref="ListLoad"/>
        /// <seelaso cref="T:System::Windows::Forms::IWin32Window"/><seealso cref="System::Windows::Interop::IWin32Window"/>
        /// <seealso cref="T:System::Windows::Forms::Control"/><seelalso cref="System::Windows::Control"/>
        [MethodNotSupported]
        virtual IntPtr OnInit() new;
#pragma endregion
    };
}}
