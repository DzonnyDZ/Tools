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
        /// <exception cref="InvalidOperationException"><see cref="Initialized"/> is true and <see cref="IsInTotalCommander"/> is false. -or- <see cref="Initialized"/> is true and <see cref="Unicode"/> differes from <paramref name="wide"/>.</exception>
        /// <remarks>
        /// <para>Lister will subclass your window to catch some hotkeys like 'n' or 'p'.</para>
        /// <para>When lister is activated, it will set the focus to your window. If your window contains child windows, then make sure that you set the focus to the correct child when your main window receives the focus!</para>
        /// <para>If <see cref="ListerShowFlags::ForceShow"/> is defined, you may try to load the file even if the plugin wasn't made for it. Example: A plugin with line numbers may only show the file as such when the user explicitly chooses 'Image/Multimedia' from the menu.</para>
        /// <para>Lister plugins which only create thumbnail images do not need to implement this function. To instantiate a plugin outside Total Commander from managed code use <see cref="Load"/>.</para>
        /// <para>This function is called by Total Commander and is not intended for direct use.</para>
        /// <para>This plugin function is implemented by <see cref="Load"/>.</para>
        /// <para>This function can be called multiple times on one plugin class instance.</para>
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
        /// <exception cref="ArgumentNullException"><paramref name="parentWin"/> is <see cref="IntPtr::Zero"/> or <paramref name="fileToLoad"/> is null.</exception>
        /// <exception cref="InvalidOperationException"><see cref="Initialized"/> is true and <see cref="IsInTotalCommander"/> is true.</exception>
        /// <remarks>Use this function when loading plugin from managed environment instead of <see cref="ListLoad"/>. For details about this function see <see cref="ListLoad"/>
        /// <para>This function can be called multiple times on one plugin class instance.</para></remarks>
        /// <seealso cref="ListLoad"/><seealso cref="OnLoad"/>
        [EditorBrowsableAttribute(EditorBrowsableState::Advanced)]
        IntPtr Load(IntPtr parentWin, String^ fileToLoad, ListerShowFlags showFlags);
    private:
        /// <summary>Internaly performs the initialization operations</summary>
        /// <param name="parentWin">Handle to lister window. Create your plugin window as a child of this window.</param>
        /// <param name="fileToLoad">The name of the file which has to be loaded.</param>
        /// <param name="showFlags">A combination of <see cref="ListerShowFlags"/> flags.</param>
        /// <returns>Handle to window created by the plugin if plugin is loaded successfully, <see cref="IntPtr::Zero"/> otherwise. If <see cref="IntPtr::Zero"/> is returned it' signal for owner application to try next plugin.</returns>
        IntPtr LoadInternal(IntPtr parentWin, String^ fileToLoad, ListerShowFlags showFlags);
    private:
        bool initialized;
        bool unicode;
        bool isInTotalCommander;
        Dictionary<IntPtr, IListerUI^>^ loadedWindows;
    public:
        /// <summary>Gets value indicating if this plugin instance was initialized or not</summary>
        property bool Initialized{virtual bool get() override sealed;}
    protected:
        /// <summary>Internal initialization function. Never call it. Use <see cref="OnInit"/> instead.</summary>
        /// <remarks>This implementation does nothing. This method overrides <see cref="PluginBase::OnInit"/></remarks>
        [EditorBrowsable(EditorBrowsableState::Never)]
        virtual void OnInitInternal() sealed = PluginBase::OnInit;
        /// <summary>When overriden in derived class called when user opens lister.</summary>
        /// <param name="e">Event arguments. You must populate them with information about plugin UI to load the plugin. If you don't populate it your plugin is not loaded and Total Commander tries next plugin.</param>
        /// <exception cref="NotSupportedException">The actual implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
        /// <remarks>
        /// <para>You typically MUST override this function in your derived plugin class. Lister plugins which only create thumbnail images do not need to implement this function. But then plugin functionality is limited.</para>
        /// <para>When this method is called, <see cref="Initialized"/> is true.</para>
        /// <para>Lister will subclass your window to catch some hotkeys like 'n' or 'p'.</para>
        /// <para>When lister is activated, it will set the focus to your window. If your window contains child windows, then make sure that you set the focus to the correct child when your main window receives the focus!</para>
        /// <para>If <paramref name="e"/>.<see cref="ListerPluginInitEventArgs::Options"/> has <see cref="ListerShowFlags::ForceShow"/> flag set, you may try to load the file even if the plugin wasn't made for it. Example: A plugin with line numbers may only show the file as such when the user explicitly chooses 'Image/Multimedia' from the menu.</para>
        /// <note type="inheritinfo">Do not throw any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note>
        /// <note>WinForms controls can be placed directly inside lister window. With WPF controls some kind of interop is necessary - the easiest option is probably to place WPF control onto WinForms control.</note>
        /// <para>Please note that multiple Lister windows can be open at the same time!</para>
        /// <para>This function can be called multiple times on one plugin class instance.</para>
        /// </remarks>
        /// <seealso cref="Load"/><seealso cref="ListLoad"/>
        /// <seelaso cref="T:System::Windows::Forms::IWin32Window"/><seealso cref="System::Windows::Interop::IWin32Window"/>
        /// <seealso cref="T:System::Windows::Forms::Control"/><seelalso cref="System::Windows::Control"/>
        [MethodNotSupported]
        virtual void OnInit(ListerPluginInitEventArgs^ e) new;
    public:
        /// <summary>Gets count of successfully loaded, not yett unloaded, plugin windows (UIs)</summary>
        property int LoadedWindowsCount{int get();}
        /// <summary>Gets enumerator that iterrates through all currently lloaded plugin windows (UIs)</summary>
        property IEnumerator<KeyValuePair<IntPtr, IListerUI^>>^ LoadedWindows {
            IEnumerator<KeyValuePair<IntPtr, IListerUI^>>^ get();
        }
        /// <summary>Gets lister plugin windows by handle</summary>
        /// <param name="hWnd">Handle of lister plugin window (control, UI) to load</param>
        /// <returns>An object representing UI of lister plugin of given handle. Null if window with given handle is not currently loaded</returns>
        property IListerUI^ LoadedWindows[IntPtr]{IListerUI^ get(IntPtr hWnd);}
#pragma endregion
    public:
        /// <summary>Called when a user switches to the next or previous file in lister with 'n' or 'p' keys, or goes to the next/previous file in the Quick View Panel, and when the definition string either doesn't exist, or its evaluation returns true.</summary>
        /// <param name="parentWin">This is lister's window. Your plugin window needs to be a child of this window</param>
        /// <param name="listWin">The plugin window returned by <see cref="ListLoad"/></param>
        /// <param name="fileToLoad">The name of the file which has to be loaded.</param>
        /// <param name="showFlags">A combination of <see cref="ListerShowFlags"/> flags.</param>
        /// <returns>Return <c>LISTPLUGIN_OK</c> (0) if load succeeds, <c>LISTPLUGIN_ERROR</c> (1) otherwise. If <c>LISTPLUGIN_ERROR</c> (1) is returned, Lister will try to load the file with the normal <see cref="ListLoad"/> function (also with other plugins).</returns>
        /// <remarks>
        /// This function is new in Total Commander 7.
        /// <para>This function is called by Total Commander and is not intended for direct use.</para>
        /// <para>This plugin function is implemented by <see cref="LoadNext"/>.</para>
        /// <remarks>
        /// <seelaso cref="LoadNext"/>
        [EditorBrowsable(EditorBrowsableState::Never)]
        [CLSCompliant(false)]
        [PluginMethod("LoadNext", "TC_L_LOADNEXT")]
        int ListLoadNext(HWND parentWin, HWND listWin, wchar_t* fileToLoad, int showFlags);
        
        /// <summary>Called when user switches to the next or previous file in lister using 'n' or 'p' keys, or goes to the next/previous file in the Quick View Panel, and when the definition string either doesn't exist, or its evaluation returns true.</summary>
        /// <param name="e">Event arguments containing enformation about current lister window.</param>
        /// <returns>True if current lister implementation can load the file, false otherwise. Returning false is equivalent to throwing any exception but <see cref="NotSupportedException"/>.</returns>
        /// <exception cref="NotSupportedException">The most-derived implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin doesnot support operation provided by the method.</exception>
        /// <exception cref="Exception">Any axception but <see cref="NotSupportedException"/> and exceptions that are not caught by .NET framework by default can be thrown from derived method to indicate that the file cannot be loaded. It's equivalent to returning false from this method.</exception>
        /// <remarks>Only Total Commander 7 and newer calls this function.
        /// <note type="inheritinfo">
        /// This default implementation contains dispatching logic for dispatching this function call to appropriate <see cref="IListerUI"/> instance (<see cref="IListerUI::LoadNext"/>).
        /// However you must still override this method and call base class method to prevent <see cref="NotSupportedException"/> exception from being thrown.
        /// <para>You may also chose not to call base class method and dispatch the event to user interface yourself.</para>
        /// </note>
        /// </remarks>
        /// <seealso cref="IListerUI::LoadNext"/>
        [MethodNotSupported]
        virtual bool LoadNext(ListerPluginReInitEventArgs^ e);

    public:
        /// <summary>Called when a user closes lister, or loads a different file.</summary>
        /// <param name="listWin">This is the window handle which needs to be destroyed.</param>
        /// <remarks>
        /// <para>This function is called by Total Commander and is not intended for direct use.</para>
        /// <para>This plugin function is implemented by <see cref="CloseWindow"/>.</para>
        /// </remarks>
        /// <seealso cref="CloseWindow"/>
        [EditorBrowsable(EditorBrowsableState::Never)]
        [CLSCompliant(false)]
        [PluginMethod("OnInit", "TC_L_LOAD")]
        void ListCloseWindow(HWND listWin);

        /// <summary>Called to unload lister UI window</summary>
        /// <param name="listerUI">An instance of lister UI previously created in <see cref="OnInit"/>. It may be null in rare (impossible?) cases when Total Commmander callls <see cref="ListCloseWindow"/> for handle that is not known to managed plugin framework.</param>
        /// <param name="listerUIHandle">Handle of <paramref name="listerUI"/></param>
        /// <exception cref="NotSupportedException"><see cref="OnInit"/> is not implemented</exception>
        /// <remarks>
        /// <para>This function is automatically implemented always when <see cref="OnInit"/> is implemented.</para>
        /// <note type="inheritinfo">
        /// You may override this function but you should always call base class method.
        /// <para>Default implementation contains logic for cleaning up internal register of opened winodws. For event dispatching it calls <see cref="DispatchCloseWindow"/>.</para>
        /// </note>
        /// <note type="inheritinfo">Do not throw any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note>
        /// </remarks>
        /// <seealso cref="DispatchCloseWindow"/><seealso cref="IListerUI::OnBeforeClose"/>
        virtual void CloseWindow(IListerUI^ listerUI, IntPtr listerUIHandle);
    protected:
        /// <summary>Dispatches event informing lister plugin UI that it is about to close from Total Commander to lister UI implementation</summary>
        /// <param name="listerUI">An instance of lister UI previously created in <see cref="OnInit"/>. It may be null in rare (impossible?) cases when Total Commmander callls <see cref="ListCloseWindow"/> for handle that is not known to managed plugin framework.</param>
        /// <param name="listerUIHandle">Handle of <paramref name="listerUI"/></param>
        /// <exception cref="ArgumentNullException"><paramref name="listerUI"/> is null</exception>
        /// <remarks>Default implementation contains logic for callling <see cref="IListerUI::OnBeforeClose"/> on <paramref name="listerUI"/>.</remarks>
        /// <seealso cref="CloseWindow"/><seealso cref="IListerUI::OnBeforeClose"/>
        [EditorBrowsable(EditorBrowsableState::Advanced)]
        virtual void DispatchCloseWindow(IListerUI^ listerUI, IntPtr listerUIHandle);

    public:
        /// <summary>Called when the plugin is loaded for the first time.
        /// It should return a parse function which allows Lister to find out whether your plugin can probably handle the file or not.
        /// You can use this as a first test - more thorough tests may be performed in <see cref="ListLoad"/>.
        /// It's very important to define a good test string, especially when there are dozens of plugins loaded!
        /// The test string allows lister to load only those plugins relevant for that specific file type.
        /// </summary>
        /// <param name="detectString">Return the detection string here. See <see cref="DetectString"/> for the syntax.</param>
        /// <param name="maxlen">Maximum length, in bytes, of the detection string (currently 2k). Managed plugin framework expects same value to be passed to all calls of this method.</param>
        /// <remarks>
        /// <para>This function is called by Total Commander and is not intended for direct use.</para>
        /// <para>This plugin function is implemented by getter of the <see cref="DetectString"/> property.</para>
        /// <remarks>
        /// <seealso cref="DetectString"/><seealso cref="DetectStringMaxLen"/>
        [EditorBrowsable(EditorBrowsableState::Never)]
        [CLSCompliant(false)]
        [PluginMethod("get_DetectString", "TC_L_DETECTSTRING")]
        void ListGetDetectString(char* detectString, int maxlen);
    private:
        Nullable<int> detectStringMaxLen;
    protected:
        /// <summary>Gets maximum allowed lenght of string returned by <see cref="DetectString"/></summary>
        /// <returns>Maximum allowed length of string for last call to <see cref="DetectString"/> getter. Null when plugin is loaded by managed application or <see cref="ListGetDetectString"/> was not called yet.</returns>
        /// <remarks>This property is initialized by <see cref="ListGetDetectString"/> and it's expected to have the same value of all <see cref="ListGetDetectString"/> calls.</remarks>
        property Nullable<int> DetectStringMaxLen{Nullable<int> get();}
    public:
        /// <summary>When overriden in derived class gets a parse function which allows Lister to find out whether your plugin can probably handle the file or not.</summary>
        /// <returns>
        /// A detection string which represents simple script used to detect if a file can be handled by this plugin.
        /// The string must contain only ANSI characters (preferably only ASCII characters as ANSI character coverage can vary from system to system based on <see cref="System::Text::Encoding::Default"/>).
        /// Maximum lenght of string returned should be <see cref="DetectStringMaxLen"/> - 1 characters. If <see cref="DetectStringMaxLen"/> is null lenght is not limited.
        /// <para>If ANSI restriction is violated, non-ANSI characters are converted (most likely to question marks (?)). If length constraint is violated excessive characters are ignored. Both can lead in your detection expression to be recognized as invalid!</para>
        /// </returns>
        /// <exception cref="NotSupportedException">Actual implementation of <see cref="DetectString"/> getter is decorated with <see cref="MethodNotSupportedAttribute"/></exception>
        /// <remarks>
        /// You can use detect string as a first test - more thorough tests may be performed in <see cref="OnInit"/>.
        /// It's very important to define a good test string, especially when there are dozens of plugins loaded!
        /// The test string allows lister to load only those plugins relevant for that specific file type.
        /// <note type="inheritinfo">Do not throw any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note>
        /// The syntax of the detection string is as follows. There are operands, operators and functions.
        /// <para>Operands:</para>
        /// <list type="table">
        /// <item><term><c>EXT</c></term><description>The extension of the file to be loaded (always uppercase).</description></term>
        /// <item><term><c>SIZE</c></term><description>The size of the file to be loaded.</description></item>
        /// <item><term><c>FORCE</c></term><description>1 if the user chose 'Image/Multimedia' from the menu, 0 otherwise.</description></item>
        /// <item><term><c>MULTIMEDIA</c></term><description>This detect string is special: It is always TRUE (also in older TC versions). If it is present in the string, this plugin overrides internal multimedia viewers in TC. If not, the internal viewers are used. Check the example below!</description></item>
        /// <item><term><c>[5]</c></term><description>The fifth byte in the file to be loaded. The first 8192 bytes can be checked for a match.</description></item>
        /// <item><term><c>12345</c></term><description>The number 12345</description></item>
        /// <item><term><c>"TEST"</c></term><description>The string "TEST"</description></item>
        /// </list>
        /// <para>Operators:</para>
        /// <list type="table">
        /// <item><term><c>&amp;</c></term><description>AND. The left AND the right expression must be true (!=0).</description></item>
        /// <item><term><c>|</c></term><description>OR: Either the left OR the right expression needs to be true (!=0).</description></item>
        /// <item><term><c>=</c></term><description>EQUAL: The left and right expression need to be equal.</description></item>
        /// <item><term><c>!=</c></term><description>UNEQUAL: The left and right expression must not be equal.</description></item>
        /// <item><term><c>&lt;</c></term><description>SMALLER: The left expression is smaller than the right expression. Comparing a number and a string returns false (0). Booleans are stored as 0 (false) and 1 (true).</description></item>
        /// <item><term><c>></c></term><description>LARGER: The left expression is larger than the right expression.</description></item>
        /// </list>
        /// <para>Functions:</para>
        /// <list type="table">
        /// <item><term><c>()</c></term><description>Braces: The expression inside the braces is evaluated as a whole.</description></item>
        /// <item><term><c>!()</c></term><description>NOT: The expression inside the braces will be inverted. Note that the braces are necessary!</description></item>
        /// <item><term><c>FIND()</c></term><description>The text inside the braces is searched in the first 8192 bytes of the file. Returns 1 for success and 0 for failure.</description></item>
        /// <item><term><c>FINDI()</c></term><description>The text inside the braces is searched in the first 8192 bytes of the file. Upper/lowercase is ignored.</description></item>
        /// </list>
        /// <para>Internal handling of variables:</para>
        /// <para>
        /// Varialbes can store numbers and strings.
        /// Operators can compare numbers with numbers and strings with strings, but not numbers with strings.
        /// Exception: A single char can also be compared with a number. Its value is its ANSI character code (e.g. "A"=65).
        /// Boolean values of comparisons are stored as 1 (true) and 0 (false).
        /// </para>
        /// <para>Operator precedence:</para>
        /// <para>The strongest operators are =, != &lt; and >, then comes &amp;, and finally |. What does this mean? Example:
        /// <code><![CDATA[expr1="a" & expr2 | expr3<5 & expr4!=b will be evaluated as ((expr1="a") & expr2) | ((expr3<5) & (expr4!="b"))]]></code>
        /// <para>If in doubt, simply use braces to make the evaluation order clear.</para>
        /// <para>Unlike many other Total Commander functions, this function is implemented by property getter. In case you need to use <see cref="MethodNotSupportedAttribute"/> use it on property getter (not the property itself).</para>
        /// </remarks>
        /// <example>
        /// <list type="table"><listheader><term>String</term><description>Interpretation</description></listheader>
        /// <item><term><code><![CDATA[EXT="WAV" | EXT="AVI"]]></code></term><description>The file may be a Wave or AVI file.</description></item>
        /// <item><term><code><![CDATA[EXT="WAV" & [0]="R" & [1]="I" & [2]="F" & [3]="F" & FIND("WAVEfmt")]]></code></term><description>Also checks for Wave header "RIFF" and string "WAVEfmt"</description></item>
        /// <item><term><code><![CDATA[EXT="WAV" & (SIZE<1000000 | FORCE)]]></code></term><description>Load wave files smaller than 1000000 bytes at startup/file change, and all wave files if the user explictly chooses 'Image/Multimedia' from the menu.</description></item>
        /// <item><term><code><![CDATA[([0]="P" & [1]="K" & [2]=3 & [3]=4) | ([0]="P" & [1]="K" & [2]=7 & [3]=8)]]></code></term><description>Checks for the ZIP header PK#3#4 or PK#7#8 (the latter is used for multi-volume zip files).</description></item>
        /// <item><term><code><![CDATA[EXT="TXT" & !(FINDI("<HEAD>") | FINDI("<BODY>"))]]></code></term><description>This plugin handles text files which aren't HTML files. A first detection is done with the &lt;HEAD> and &lt;BODY> tags. If these are not found, a more thorough check may be done in the plugin itself.</description></item>
        /// <item><term><code><![CDATA[MULTIMEDIA & (EXT="WAV" | EXT="MP3")]]></code></term><description>Replace the internal player for WAV and MP3 files (which normally uses Windows Media Player as a plugin). Requires TC 6.0 or later!</description></item>
        /// </list>
        /// </example>
        property String^ DetectString{ [MethodNotSupported] virtual String^ get(); }

    public:
        /// <summary>Called when the user tries to find text in the plugin.</summary>
        /// <param name="listWin">Hande to your list window created with <see cref="ListLoad"/></param>
        /// <param name="searchString">String to be searched</param>
        /// <param name="searchParameter">A combination of <see cref="TextSearchOptions"/> flags</param>
        /// <returns>Either <c>LISTPLUGIN_OK</c> (0) or <c>LISTPLUGIN_ERROR</c> (1).</returns>
        /// <remarks>
        /// The plugin needs to highlight/select the found text by itself.
        /// <para>This function is called by Total Commander and is not intended for direct use.</para>
        /// <para>This plugin function is implemented by getter of the <see cref="SearchText"/> property.</para>
        /// <remarks>
        /// <seealso cref="SearchText"/>
        [EditorBrowsable(EditorBrowsableState::Never)]
        [CLSCompliant(false)]
        [PluginMethod("SearchText", "TC_L_SEARCHTEXT")]
        int ListSearchText(HWND listWin, wchar_t* searchString, int searchParameter);
        
        /// <summary>When overriden in derived class called when the user tries to find text in the plugin.</summary>
        /// <param name="listerUI">An instance of lister plugin UI. Null in rare cases whan Total Commander calls <see cref="ListSearchText"/> with handle unknown to managed plugin framework</param>
        /// <param name="listerUIHandle">Handle of <paramref name="listerUI"/></param>
        /// <param name="searchString">String to search for</param>
        /// <param name="searchParameter">Indicates search options</param>
        /// <returns>True if search succeeded, false if it failed (nothing was found). Returning false is equivalent to throwing  any exception but <see cref="NotSupportedException"/>.</returns>
        /// <exception cref="NotSupportedException">Actual implementation of <see cref="SearchText"/> is decorated with <see cref="NotSupportedAttribute"/></exception>
        /// <exception cref="Exception">Any other exception but <see cref="NotSupportedException"/> and exceptions usually not caught in .NET framework (such as <see cref="StackOverflowException"/>) can be thrown by implementation of this method to indicate search failure.</exception>
        /// <remarks>
        /// The plugin needs to highlight/select the found text by itself.
        /// <note type="inheritinfo">
        /// This default implementation contains dispatching logic for dispatching this function call to appropriate <see cref="IListerUI"/> instance (<see cref="IListerUI::SearchText"/>).
        /// However you must still override this method and call base class method to prevent <see cref="NotSupportedException"/> exception from being thrown.
        /// <para>You may also chose not to call base class method and dispatch the event to user interface yourself.</para>
        /// </note>
        /// </remarks>
        /// <seealso cref="IListerUI::SearchText"/>
        [MethodNotSupported]
        virtual bool SearchText(IListerUI^ listerUI, IntPtr listerUIHandle, String^ searchString, TextSearchOptions searchParameter);

    public:
        /// <summary>Called when the user changes some options in Lister's menu.</summary>
        /// <param name="listWin">Hande to your list window created with <see cref="ListLoad"/></param>
        /// <param name="command">One of <see cref="ListerCommand"> commands</param>
        /// <param name="parameter">Used when <paramref name="command"/> is <see cref="ListerCommand::NewParams"/>. Combination of <see cref="ListerShowFlags"/></param>
        /// <returns>Either <c>LISTPLUGIN_OK</c> (0) or <c>LISTPLUGIN_ERROR</c> (1).</returns>
        /// <remarks>
        /// <para>This function is called by Total Commander and is not intended for direct use.</para>
        /// <para>This plugin function is implemented by getter of the <see cref="SendCommand"/> property.</para>
        /// <remarks>
        /// <seealso cref="SendCommand"/>
        [EditorBrowsable(EditorBrowsableState::Never)]
        [CLSCompliant(false)]
        [PluginMethod("SendCommand", "TC_L_SENDCOMMAND")]
        int ListSendCommand(HWND listWin, int command, int parameter);
        
        /// <summary>When overriden in derived class called when the user changes some options in Lister's menu.</summary>
        /// <param name="listerUI">An instance of lister plugin UI. Null in rare cases whan Total Commander calls <see cref="ListSearchText"/> with handle unknown to managed plugin framework</param>
        /// <param name="listerUIHandle">Handle of <paramref name="listerUI"/></param>
        /// <param name="command">One of <see cref="ListerCommand"> commands</param>
        /// <param name="parameter">Used when <paramref name="command"/> is <see cref="ListerCommand::NewParams"/>. Combination of <see cref="ListerShowFlags"/>.</param>
        /// <returns>Value indicating if command succeeded. Raturning false is equal to throwing any exception but <see cref="NotSupportedException"/>.</returns>
        /// <exception cref="NotSupportedException">Actual implementation of <see cref="SearchText"/> is decorated with <see cref="NotSupportedAttribute"/></exception>
        /// <exception cref="Exception">Any other exception but <see cref="NotSupportedException"/> and exceptions usually not caught in .NET framework (such as <see cref="StackOverflowException"/>) can be thrown by implementation of this method to indicate command failure.</exception>
        /// <remarks>
        /// <note type="inheritinfo">
        /// This default implementation contains dispatching logic for dispatching this function call to appropriate <see cref="IListerUI"/> instance (<see cref="IListerUI::OnCommand"/>).
        /// However you must still override this method and call base class method to prevent <see cref="NotSupportedException"/> exception from being thrown.
        /// <para>You may also chose not to call base class method and dispatch the event to user interface yourself.</para>
        /// </note>
        /// </remarks>
        /// <seealso cref="IListerUI::OnCommand"/>
        [MethodNotSupported]
        bool SendCommand(IListerUI^ listerUI, IntPtr listerUIHandle, ListerCommand command, ListerShowFlags parameter);
    };
}}
