#pragma once

#include "..\Plugin\listplug.h"
#include "WLX EventArgs.h"
#include "WLX Enums.h"
#include "IListerUIInfo.h"
       
using namespace System;
using namespace System::ComponentModel;

namespace Tools{namespace TotalCommanderT{

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
        /// <exception cref="Exception">Any exception but <see cref="NotSupportedException"/> and exceptions usually not caught in .NET framework (such as <see cref="StackOverflowException"/>) can be thrown by implementation of this method to indicate search failure.</exception>
        /// <remarks>
        /// <para>The plugin needs to highlight/select the found text by itself.</para>
        /// If your plugin does not support search, just implement this method always returning false and do not override <see cref="ListerPluginBase::SearchText"/>.
        /// </remarks>
        /// <seelaso cref="ShowSearchDialog"/>
        bool SearchText(String^ serachString, TextSearchOptions searchParameter); 
        /// <summary>Called when the user changes some options in Lister's menu.</summary>
        /// <param name="e">Event arguments</param>
        /// <returns>True if command succeeded, false if it failed. Returning false is equivalent to throwing  any exception but <see cref="NotSupportedException"/>.</returns>
        /// <exception cref="Exception">Any exception but <see cref="NotSupportedException"/> and exceptions usually not caught in .NET framework (such as <see cref="StackOverflowException"/>) can be thrown by implementation of this method to indicate commmand failure.</exception>
        /// <remarks>If your plugin does not support commanding, just implement this method always returning false and do not override <see cref="ListerPluginBase::SendCommand"/>.</remarks>
        bool OnCommand(ListerCommandEventArgs^ e);
        /// <summary>Called when the user chooses the print function.</summary>
        /// <param name="e">Event arguments</param>
        /// <returns>True if command succeeded, false if it failed. Returning false is equivalent to throwing  any exception but <see cref="NotSupportedException"/>.</returns>
        /// <exception cref="Exception">Any exception but <see cref="NotSupportedException"/> and exceptions usually not caught in .NET framework (such as <see cref="StackOverflowException"/>) can be thrown by implementation of this method to indicate commmand failure.</exception>
        /// <remarks>
        /// Total Commander is expected to pass same file name to <paramref name="e"/>.<see cref="PrintEventArgs::FileToPrint"/> as is name of file currently loaded in plugin UI. So, plugin UI implementation may chose either to ignore <paramref name="e"/>.<see cref="PrintEventArgs::FileToPrint"/>, throw an exception when it differs from expected value, or allow printing different file than currently opened in UI (but TC should not request this).
        /// <para>You need to show a print dialog, in which the user can choose what to print, and select a different printer.</para>
        /// <para>If your plugin does not support printing, just implement this method always returning false and do not override <see cref="ListerPluginBase::Print"/>.</para>
        /// </remarks>
        bool Print(PrintEventArgs^ e);
        /// <summary>Called when the parent window receives a notification message from the child window.</summary>
        /// <param name="message">The received message</param>
        /// <param name="wParam">The wParam parameter of the message</param>
        /// <param name="lParam">The lParam parameter of the message</param>
        /// <returns>Return the value described for that message in the Windows API help.</returns>
        /// <remarks>
        /// Total Commander passes only certain messages here: <c>WM_COMMAND</c>, <c>WM_NOTIFY</c>, <c>WM_MEASUREITEM</c> or <c>WM_DRAWITEM</c>.
        /// <para>Possible applications: Owner-drawn Listview control, reacting to scroll messages, etc.</para>
        /// <para>If you don't want to implement this method, do not override <see cref="ListerPluginBase::NotificationReceived"/> in the first place. That this function is never called and it does not matter waht you return from here (preferrably 0).</para>
        /// </remarks>
        int OnNotificationReceived(int message, UIntPtr wParam, IntPtr lParam);
        /// <summary>Called when user tries to find text in the plugin.</summary>
        /// <param name="findNext">True if Find next was chosen from the menu, false if find first was chosen by the user</param>
        /// <returns>
        /// True if this plugin instance (UI instance) shows the search dialog itself (it must show it in this function), false if Total Commander should show it's own dialog and call <see cref="SearchText"/>.
        /// This behavior allows to use both - <see cref="ShowSearchDialog"/> and <see cref="SearchText"/> in one plugin/UI instance.
        /// Do not return false if search fails. (Return true anyway.)
        /// </returns>
        /// <remarks>If you don't want to implement your own custom search dialog, just return false from this function and implement <see cref="SearchText"/> instead.</remarks>
        /// <seealso cref="SearchText"/>
        bool ShowSearchDialog(bool findNext);
    };
}}