#pragma once

#include "ListerPluginBase.h"
#include "WLX common.h"

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections::Generic;

namespace Tools {
    namespace TotalCommanderT {

        /// <summary>Abstract base class for Total Commander lister plugins (wlx)</summary>
        /// <remarks>See <see2 cref2="T:Tools.TotalCommanderT.PluginBuilder.Generator"/> for more information about how to generate Total Commander plugin from .NET.
        /// <para>To keep fingerprint and dependencies of Tools.TotalCommander.dll assembly low, this class does not provide some utility functions. You may consider deriving from derived class from Tools.TotalCommander.Extensions.dll instead.</para></remarks>
        /// <typeparam name="TUI">Type of class that represents plugin User Interface</typeparam>
        /// <version version="1.5.4">This class is new in version 1.5.4</version>
        generic <class TUI> where TUI : IListerUI, ref class
            public ref class ListerPlugin abstract : ListerPluginBase {
            protected:
                /// <summary>CTor - creates a new instance of the <see cref="ListerPluginBase"/> class</summary>
                /// <remarks>The plugin instance is not ready to be used until <see cref="ListerPluginBase::OnInit"/> or <see cref="ListerPluginBase::SetDefaultParams"/> is called.</remarks>
                ListerPlugin();

            protected:
                /// <summary>Overrides and implements <see cref="ListerPluginBase::OnInit"/></summary>
                /// <param name="e">Event arguments</param>
                /// <remarks>This method cannot be overridden and is not intended for direct use. Use <see cref="OnInit(ListerPluginInitEventArgsSpecialized{TUI})"/> instead</remarks>
                /// <seealso cref="OnInit">
                [EditorBrowsable(EditorBrowsableState::Never)]
                virtual void OnInit(ListerPluginInitEventArgs^ e) sealed override;

                /// <summary>When overridden in derived class implements <see cref="ListerPluginBase::OnInit"/> - called when user opens lister.</summary>
                /// <param name="e">Event arguments. You must populate them with information about plugin UI to load the plugin. If you don't populate it your plugin is not loaded and Total Commander tries next plugin.</param>
                /// <remarks>
                /// <para>When this method is called <see cref="Initialized"/> is true.</para>
                /// <para>Lister will subclass your window to catch some hotkeys like 'n' or 'p'.</para>
                /// <para>When lister is activated, it will set the focus to your window. If your window contains child windows, then make sure that you set the focus to the correct child when your main window receives the focus!</para>
                /// <para>If <paramref name="e"/>.<see cref="ListerPluginInitEventArgs::Options"/> has <see cref="ListerShowFlags::ForceShow"/> flag set, you may try to load the file even if the plugin wasn't made for it. Example: A plugin with line numbers may only show the file as such when the user explicitly chooses 'Image/Multimedia' from the menu.</para>
                /// <note type="inheritinfo">Do not throw any other exceptions. Such exception will be passed to Total Commander which cannot handle it.</note>
                /// <note>WinForms controls can be placed directly inside lister window. With WPF controls some kind of interop is necessary - the easiest option is probably to place WPF control onto WinForms control.</note>
                /// <para>Please note that multiple Lister windows can be open at the same time!</para>
                /// <para>This function can be called multiple times on one plugin class instance.</para>
                /// <note type="inheritinfo">
                /// <see cref="ListerPlugin{T}"/>-derived plugin must always implement this method.
                /// You cannot apply <see cref="MethodNotSupportedAttribute"/> on your method implementation. The attribute would be ignored because this method is not override of base class method (it's overload). (The override is <see langword="sealed"/>).
                /// <para>If you really want to implement <see cref="ListerPlugin{T}"/>-derived plugin that never shows UI (i.e. is used only for thumbnails generation) override <see cref="DetectString"/> and return detection function which always returns false and then do not create any UI in this method.
                /// However in this case preffered way is to derive your plugin class from <see cref="ListerPluginBase"/> directly instead.</para>
                /// </remarks>
                virtual void OnInit(ListerPluginInitEventArgsSpecialized<TUI>^ e) abstract;

            public:
                /// <summary>Called when user switches to the next or previous file in lister using 'n' or 'p' keys, or goes to the next/previous file in the Quick View Panel, and when the definition string either doesn't exist, or its evaluation returns true.</summary>
                /// <param name="e">Event arguments containing information about current lister window.</param>
                /// <returns>True if current lister implementation can load the file, false otherwise. Returning false is equivalent to throwing any exception but <see cref="NotSupportedException"/>.</returns>
                /// <exception cref="NotSupportedException">The most-derived implementation is marked with <see cref="MethodNotSupportedAttribute"/> which means that the plugin does not support operation provided by the method.</exception>
                /// <exception cref="Exception">Any exception but <see cref="NotSupportedException"/> and exceptions that are not caught by .NET framework by default can be thrown from derived method to indicate that the file cannot be loaded. It's equivalent to returning false from this method.</exception>
                /// <remarks>
                /// Only Total Commander 7 and newer calls this function.
                /// <para>This method is not decorated with <see cref="MethodNotSupportedAttribute"/> which means that all <see cref="ListerPlugin{T}"/>-derived classes must support load next functionality (unless they override this method - see below).</para>
                /// <note type="inheritinfo">
                /// This default implementation contains dispatching logic for dispatching this function call to appropriate <see cref="IListerUI"/> instance (<see cref="IListerUI::LoadNext"/>).
                /// There is usually no need to override it.
                /// <para>You may override it to:</para>
                /// <list type="bullet">
                /// <item>Perform additional actions before the event is dispatched or after it is processed by plugin UI.</item>
                /// <item>Implement you own dispatching logic</item>
                /// <item>Indicate that your plugin does not support load next functionality. Do do so, just decorate your override with <see cref="MethodNotSupportedAttribute"/> and call base class method.</item>
                /// </list></note></remarks>
                /// <seealso cref="IListerUI::LoadNext"/>
                virtual bool LoadNext(ListerPluginReInitEventArgs^ e) override;

                /// <summary>Called when the user tries to find text in the plugin.</summary>
                /// <param name="listerUI">An instance of lister plugin UI. Null in rare cases when Total Commander calls <see cref="ListSearchText"/> with handle unknown to managed plugin framework</param>
                /// <param name="listerUIHandle">Handle of <paramref name="listerUI"/></param>
                /// <param name="searchString">String to search for</param>
                /// <param name="searchParameter">Indicates search options</param>
                /// <returns>True if search succeeded, false if it failed (nothing was found). Returning false is equivalent to throwing  any exception but <see cref="NotSupportedException"/>.</returns>
                /// <exception cref="NotSupportedException">Actual implementation of <see cref="SearchText"/> is decorated with <see cref="NotSupportedAttribute"/> -or- this method is not overridden and method implementing <see cref="ListerUI::SearchText"/> on type <typeparamref name="TUI"/> is decorated with <see cref="MethodNotSupportedAttribute"/>.</exception>
                /// <exception cref="Exception">Any other exception but <see cref="NotSupportedException"/> and exceptions usually not caught in .NET framework (such as <see cref="StackOverflowException"/>) can be thrown by implementation of this method to indicate search failure.</exception>
                /// <remarks>
                /// The plugin needs to highlight/select the found text by itself.
                /// <note type="inheritinfo">
                /// This default implementation contains dispatching logic for dispatching this function call to appropriate <see cref="IListerUI"/> instance (<see cref="IListerUI::SearchText"/>).
                /// You usaually does not need to override it.
                /// <para>When you override this method you typically should call base class method to use default event dispatching logic.</para>
                /// <para>
                /// This method is decorated with <see cref="MethodNotSupportedRedirectAttribute"/> which causes actual <see cref="MethodNotSupportedAttribute"/> value to be read form method which implements <see cref="IListerUI::SearchText"/> on type <typeparamref name="TUI"/>.
                /// If you do not want to loose this automatic redirection you should decorate your method with <see cref="MethodNotSupportedRedirectAttribute"/> as well.
                /// </para></note></remarks>
                /// <seealso cref="IListerUI::SearchText"/><seelaso cref="ShowSearchDialog"/>
                [MethodNotSupportedRedirect("SearchText", GPar = "TUI", Hint = IListerUI::typeid)]
                virtual bool SearchText(IListerUI^ listerUI, IntPtr listerUIHandle, String^ searchString, TextSearchOptions searchParameter) override;

                /// <summary>Called when the user changes some options in Lister's menu.</summary>
                /// <param name="listerUI">An instance of lister plugin UI. Null in rare cases when Total Commander calls <see cref="ListSearchText"/> with handle unknown to managed plugin framework</param>
                /// <param name="listerUIHandle">Handle of <paramref name="listerUI"/></param>
                /// <param name="command">One of <see cref="ListerCommand"> commands</param>
                /// <param name="parameter">Used when <paramref name="command"/> is <see cref="ListerCommand::NewParams"/>. Combination of <see cref="ListerShowFlags"/>.</param>
                /// <returns>Value indicating if command succeeded. Returning false is equal to throwing any exception but <see cref="NotSupportedException"/>.</returns>
                /// <exception cref="NotSupportedException">Most-derived implementation of <see cref="SendCommand"/> is decorated with <see cref="MethodNotSupportedAttribute"/> or this method is not overridden and method implementing <see cref="IListerUI::OnCOmmand"/> on <typeparamref name="TUI"/> is decorated with <see cref="MethodNotSupportedAttribute"/>.</exception>
                /// <exception cref="Exception">Any other exception but <see cref="NotSupportedException"/> and exceptions usually not caught in .NET framework (such as <see cref="StackOverflowException"/>) can be thrown by implementation of this method to indicate command failure.</exception>
                /// <remarks>
                /// <note type="inheritinfo">
                /// This default implementation contains dispatching logic for dispatching this function call to appropriate <see cref="IListerUI"/> instance (<see cref="IListerUI::OnCommand"/>).
                /// You usually does not need to override it.
                /// <para>When you override this method you typically should call base class method to use default event dispatching logic.</para>
                /// <para>
                /// This method is decorated with <see cref="MethodNotSupportedRedirectAttribute"/> which causes actual <see cref="MethodNotSupportedAttribute"/> value to be read form method which implements <see cref="IListerUI::SearchText"/> on type <typeparamref name="TUI"/>.
                /// If you do not want to loose this automatic redirection you should decorate your method with <see cref="MethodNotSupportedRedirectAttribute"/> as well.
                /// </para></note></remarks>
                /// <seealso cref="IListerUI::OnCommand"/>
                [MethodNotSupportedRedirect("OnCommand", GPar = "TUI", Hint = IListerUI::typeid)]
                virtual bool SendCommand(IListerUI^ listerUI, IntPtr listerUIHandle, ListerCommand command, ListerShowFlags parameter) override;

                /// <summary>Called when the user chooses the print function.</summary>
                /// <param name="listerUI">An instance of lister plugin UI. Null in rare cases when Total Commander calls <see cref="ListSearchText"/> with handle unknown to managed plugin framework</param>
                /// <param name="listerUIHandle">Handle of <paramref name="listerUI"/></param>
                /// <param name="fileToPrint">The full name of the file which needs to be printed. This is the same file as loaded with <see cref="OnInit"/>.</param>
                /// <param name="defPrinter">Name of the printer currently chosen in Total Commander. May be null (use default printer).</param>
                /// <param name="printFlags">Currently not used (set to 0). May be used in a later version of TC.</param>
                /// <param name="margins">The left, top, right and bottom margins of the print area (in 1/100 of inch). May be ignored.</margins>
                /// <returns>Value indicating if command succeeded. Returning false is equal to throwing any exception but <see cref="NotSupportedException"/>.</returns>
                /// <exception cref="NotSupportedException">Actual implementation of <see cref="Print"/> is decorated with <see cref="NotSupportedAttribute"/></exception>
                /// <exception cref="Exception">Any other exception but <see cref="NotSupportedException"/> and exceptions usually not caught in .NET framework (such as <see cref="StackOverflowException"/>) can be thrown by implementation of this method to indicate command failure.</exception>
                /// <remarks>
                /// <note type="inheritinfo">
                /// This default implementation contains dispatching logic for dispatching this function call to appropriate <see cref="IListerUI"/> instance (<see cref="IListerUI::Print"/>).
                /// You usually does not need to override it.
                /// <para>When you override this method you typically should call base class method to use default event dispatching logic.</para>
                /// <para>
                /// This method is decorated with <see cref="MethodNotSupportedRedirectAttribute"/> which causes actual <see cref="MethodNotSupportedAttribute"/> value to be read form method which implements <see cref="IListerUI::SearchText"/> on type <typeparamref name="TUI"/>.
                /// If you do not want to loose this automatic redirection you should decorate your method with <see cref="MethodNotSupportedRedirectAttribute"/> as well.
                /// </para></note></remarks>
                /// <seealso cref="IListerUI::Print"/>
                [MethodNotSupportedRedirect("Print", GPar = "TUI", Hint = IListerUI::typeid)]
                virtual bool Print(IListerUI^ listerUI, IntPtr listerUIHandle, String^ fileToPrint, String^ defPrinter, PrintFlags printFlags, System::Drawing::Printing::Margins^ margins) override;

                /// <summary>Called when the parent window receives a notification message from the child window.</summary>
                /// <param name="listerUI">An instance of lister plugin UI. Null in rare cases when Total Commander calls <see cref="ListSearchText"/> with handle unknown to managed plugin framework</param>
                /// <param name="listerUIHandle">Handle of <paramref name="listerUI"/></param>
                /// <param name="message">The received message, one of the following.</param>
                /// <param name="wParam">The wParam parameter of the message.</param>
                /// <param name="lParam">The lParam parameter of the message.</param>
                /// <returns>Return the value described for that message in the Windows API help.</returns>
                /// <remarks>
                /// Do not implement this function if you don't use any owner-drawn controls and don't require any notification messages! Possible applications: Owner-drawn Listview control, reacting to scroll messages, etc.
                /// <para>Tocal Commanders sends only certain windows messages this way: <c>WM_COMMAND</c>, <c>WM_NOTIFY</c>, <c>WM_MEASUREITEM</c> or <c>WM_DRAWITEM</c></para>
                /// <note type="inheritinfo">
                /// This default implementation contains dispatching logic for dispatching this function call to appropriate <see cref="IListerUI"/> instance (<see cref="IListerUI::OnNotificationReceived"/>).
                /// You usually does not need to override it.
                /// <para>When you override this method you typically should call base class method to use default event dispatching logic.</para>
                /// <para>
                /// This method is decorated with <see cref="MethodNotSupportedRedirectAttribute"/> which causes actual <see cref="MethodNotSupportedAttribute"/> value to be read form method which implements <see cref="IListerUI::SearchText"/> on type <typeparamref name="TUI"/>.
                /// If you do not want to loose this automatic redirection you should decorate your method with <see cref="MethodNotSupportedRedirectAttribute"/> as well.
                /// </para></note></remarks>
                /// <seealso cref="IListerUI::OnNotificationreceived"/>
                [MethodNotSupportedRedirect("OnNotificationreceived", GPar = "TUI", Hint = IListerUI::typeid)]
                [EditorBrowsable(EditorBrowsableState::Advanced)]
                virtual int NotificationReceived(IListerUI^ listerUI, IntPtr listerUIHandle, int message, UIntPtr wParam, IntPtr lParam) override;

                /// <summary>Called when the user tries to find text in the plugin.</summary>
                /// <param name="listerUI">An instance of lister plugin UI. Null in rare cases when Total Commander calls <see cref="SearchDialog"/> with handle unknown to managed plugin framework</param>
                /// <param name="listerUIHandle">Handle of <paramref name="listerUI"/></param>
                /// <param name="findNext">True if Find next was chosen from the menu, false if find first was chosen by the user</param>
                /// <returns>
                /// True if this plugin instance (UI instance) shows the search dialog itself (it must show it in this function), false if Total Commander should show it's own dialog and call <see cref="SearchText"/>.
                /// This behavior allows to use both - <see cref="ShowSearchDialog"/> and <see cref="SearchText"/> in one plugin/UI instance.
                /// Do not return false if search fails. (Return true anyway.)
                /// </returns>
                /// <exception cref="NotSupportedException">Most-derived implementation of <see cref="ShowSearchDialog"/> is decorated with <see cref="MethodNotSupportedAttribute"/></exception>
                /// <remarks>
                /// <para>Only implement this function if your plugin requires a plugin-specific search dialog! For searching text, please implement <see cref="SearchText"/> instead!</para>
                /// <para>The plugin needs to show the search dialog and highlight/select the found text by itself. Requires Total Commander 7 or later.</para>
                /// <note type="inheritinfo">Do not throw any exceptions from this function. It'd be passed to Total Commander which cannot handle it.</note>
                /// <note type="inheritinfo">
                /// This default implementation contains dispatching logic for dispatching this function call to appropriate <see cref="IListerUI"/> instance (<see cref="IListerUI::ShowSearchDialog"/>).
                /// You usually does not need to override it.
                /// <para>When you override this method you typically should call base class method to use default event dispatching logic.</para>
                /// <para>
                /// This method is decorated with <see cref="MethodNotSupportedRedirectAttribute"/> which causes actual <see cref="MethodNotSupportedAttribute"/> value to be read form method which implements <see cref="IListerUI::SearchText"/> on type <typeparamref name="TUI"/>.
                /// If you do not want to loose this automatic redirection you should decorate your method with <see cref="MethodNotSupportedRedirectAttribute"/> as well.
                /// </para></note></remarks>
                /// <seealso cref="SearchText"/><seealso cref="IListerUI::ShowSerachDialog"/>
                [MethodNotSupportedRedirect("ShowSerachDialog", GPar = "TUI", Hint = IListerUI::typeid)]
                virtual bool ShowSearchDialog(IListerUI^ listerUI, IntPtr listerUIHandle, bool findNext) override;
        };
    }
}
