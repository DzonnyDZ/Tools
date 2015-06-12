Imports System.ComponentModel, Tools.ExtensionsT
#If Config <= Nightly Then 'Stage: Nightly
Namespace API.Messages
    ''' <summary>Window messages</summary>
    ''' <version version="1.5.3">Added TCM_... (Tab control) and CCM_... (commmon control) messages</version>
    Public Enum WindowMessages As Integer
#Region "WM_..."
        ''' <summary>The WM_ACTIVATE message is sent to both the window being activated and the window being deactivated. If the windows use the same input queue, the message is sent synchronously, first to the window procedure of the top-level window being deactivated, then to the window procedure of the top-level window being activated. If the windows use different input queues, the message is sent asynchronously, so the window is activated immediately.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The low-order word specifies whether the window is being activated or deactivated. This parameter can be one of the following values. The high-order word specifies the minimized state of the window being activated or deactivated. A nonzero value indicates the window is minimized. <seealso cref="wParam.WM_ACTIVATE"/></description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the window being activated or deactivated, depending on the value of the wParam parameter. If the low-order word of wParam is WA_INACTIVE, lParam is the handle to the window being activated. If the low-order word of wParam is WA_ACTIVE or WA_CLICKACTIVE, lParam is the handle to the window being deactivated. This handle can be NULL.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646274.aspx</seealso></remarks>
        WM_ACTIVATE = &H6
        ''' <summary>The WM_ACTIVATEAPP message is sent when a window belonging to a different application than the active window is about to be activated. The message is sent to the application whose window is being activated and to the application whose window is being deactivated.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies whether the window is being activated or deactivated. This parameter is TRUE if the window is being activated; it is FALSE if the window is being deactivated.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies a thread identifier (a DWORD). If the wParam parameter is TRUE, lParam is the identifier of the thread that owns the window being deactivated. If wParam is FALSE, lParam is the identifier of the thread that owns the window being activated.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632614.aspx</seealso></remarks>
        WM_ACTIVATEAPP = &H1C
        ''' <summary>The lowest value for AFX message.</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_AFXFIRST = &H360
        ''' <summary>The highest value for AFX message.</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_AFXLAST = &H37F
        ''' <summary>The WM_APP constant is used by applications to help define private messages, usually of the form WM_APP+X, where X is an integer value.</summary>
        ''' <remarks>The WM_APP constant is used to distinguish between message values that are reserved for use by the system and values that can be used by an application to send messages within a private window class. The following are the ranges of message numbers available.
        ''' <list type="table">
        ''' <listheader><term>Range</term><description>Meaning</description></listheader>
        ''' <item><term>0 through <see cref="WM_USER"/> - 1</term>
        ''' <description>Messages reserved for use by the system.</description></item>
        ''' <item><term><see cref="WM_USER"/> through <see cref="X7FFF"/></term>
        ''' <description>Integer messages for use by private window classes.</description></item>
        ''' <item><term><see cref="WM_APP"/> throught <see cref="XBFFF"/></term>
        ''' <description>Messages available for use by applications.</description></item>
        ''' <item><term><see cref="XC000"/> through <see cref="XFFFF"/></term>
        ''' <description>String messages for use by applications.</description></item>
        ''' <item><term>Greater than <see cref="XFFFF"/></term>
        ''' <description>Reserved by the system.</description></item>
        ''' </list><seealso></seealso>
        ''' </remarks>
        WM_APP = &H8000
        ''' <summary>The WM_APPCOMMAND message notifies a window that the user generated an application command event, for example, by clicking an application command button using the mouse or typing an application command key on the keyboard.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the window where the user clicked the button or pressed the key. This can be a child window of the window receiving the message. For more information about processing this message, see the Remarks section.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Use the following code to get the information contained in the lParam parameter.
        ''' <code language="C++"><![CDATA[cmd  = GET_APPCOMMAND_LPARAM(lParam);
        ''' uDevice = GET_DEVICE_LPARAM(lParam);
        ''' dwKeys = GET_KEYSTATE_LPARAM(lParam);]]></code></description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return TRUE. For more information about processing the return value, see the Remarks section.</description></item>
        ''' </list>
        ''' <para>DefWindowProc generates the <see cref="WM_APPCOMMAND"/> message when it processes the <see cref="WM_XBUTTONUP"/> or <see cref="WM_NCXBUTTONUP"/> message, or when the user types an application command key.</para>
        ''' <para>If a child window does not process this message and instead calls DefWindowProc, DefWindowProc will send the message to its parent window. If a top level window does not process this message and instead calls DefWindowProc, DefWindowProc will call a shell hook with the hook code equal to HSHELL_APPCOMMAND.</para>
        ''' <para>To get the coordinates of the cursor if the message was generated by a button click on the mouse, the application can call GetMessagePos. An application can test whether the message was generated by the mouse by checking whether lParam contains FAPPCOMMAND_MOUSE.</para>
        ''' <para>Unlike other windows messages, an application should return TRUE from this message if it processes it. Doing so will allow software that simulates this message on Microsoft Windows systems earlier than Windows 2000 to determine whether the window procedure processed the message or called DefWindowProc to process it.</para>
        ''' </remarks>
        WM_APPCOMMAND = &H319
        ''' <summary>The WM_ASKCBFORMATNAME message is sent to the clipboard owner by a clipboard viewer window to request the name of a CF_OWNERDISPLAY clipboard format.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the size, in characters, of the buffer pointed to by the lParam parameter.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to the buffer that is to receive the clipboard format name.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms649018.aspx</seealso></remarks>
        WM_ASKCBFORMATNAME = &H30C
        ''' <summary>The WM_CANCELJOURNAL message is posted to an application when a user cancels the application's journaling activities. The message is posted with a NULL window handle.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>Return value</term>
        ''' <description>This message does not return a value. It is meant to be processed from within an application's main loop or a GetMessage hook procedure, not from a window procedure.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms644971.aspx</seealso></remarks>
        WM_CANCELJOURNAL = &H4B
        ''' <summary>The WM_CANCELMODE message is sent to cancel certain modes, such as mouse capture. For example, the system sends this message to the active window when a dialog box or message box is displayed. Certain functions also send this message explicitly to the specified window regardless of whether it is the active window. For example, the EnableWindow function sends this message when disabling the specified window.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632615.aspx</seealso>
        ''' When the WM_CANCELMODE message is sent, the DefWindowProc function cancels internal processing of standard scroll bar input, cancels internal menu processing, and releases the mouse capture.</remarks>
        WM_CANCELMODE = &H1F
        ''' <summary>The WM_CAPTURECHANGED message is sent to the window that is losing the mouse capture.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the window gaining the mouse capture.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645605.aspx</seealso></remarks>
        WM_CAPTURECHANGED = &H215
        ''' <summary>The WM_CHANGECBCHAIN message is sent to the first window in the clipboard viewer chain when a window is being removed from the chain.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the window being removed from the clipboard viewer chain.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the next window in the chain following the window being removed. This parameter is NULL if the window being removed is the last window in the chain.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms649019.aspx</seealso></remarks>
        WM_CHANGECBCHAIN = &H30D
        ''' <summary>An application sends the WM_CHANGEUISTATE message to indicate that the user interface (UI) state should be changed.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The low-order word specifies the action to be taken. This member can be one of the following values: <seealso cref="wParam.WM_CHANGEUISTATE"/></description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used. Must be set to 0.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>No return value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646342.aspx</seealso></remarks>
        WM_CHANGEUISTATE = &H127
        ''' <summary>Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_CHAR message.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The low-order word specifies the value of the key the user pressed. The high-order word specifies the current position of the caret.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the list box.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value specifies the action that the application performed in response to the message. A return value of –1 or –2 indicates that the application handled all aspects of selecting the item and requires no further action by the list box. A return value of 0 or greater specifies the zero-based index of an item in the list box and indicates that the list box should perform the default action for the keystroke on the specified item.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb761358.aspx</seealso></remarks>
        WM_CHARTOITEM = &H2F
        ''' <summary>The WM_CHILDACTIVATE message is sent to a child window when the user clicks the window's title bar or when the window is activated, moved, or sized.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632616.aspx</seealso></remarks>
        WM_CHILDACTIVATE = &H22
        ''' <summary>An application sends a WM_CLEAR message to an edit control or combo box to delete (clear) the current selection, if any, from the edit control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>This message does not return a value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms649020.aspx</seealso></remarks>
        WM_CLEAR = &H303
        ''' <summary>The WM_CLOSE message is sent as a signal that a window or an application should terminate.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632617.aspx</seealso></remarks>
        WM_CLOSE = &H10
        ''' <summary>The WM_COMMAND message is sent when the user selects a command item from a menu, when a control sends a notification message to its parent window, or when an accelerator keystroke is translated.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>If high word is 0 (message source is menu) or 1 (message source is accelerator), low word is one of <see cref="wParam.WM_COMMAND_low"/> constants (menu identifier or acceleretor identifier). If high word is control-defined notification code (source of message is control), low word is control identifier.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>If high word of wParam is not control defined, low word is zero, otherwise it is handle to control window.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms647591.aspx</seealso></remarks>
        WM_COMMAND = &H111
        ''' <summary>The WM_COMPACTING message is sent to all top-level windows when the system detects more than 12.5 percent of system time over a 30- to 60-second interval is being spent compacting memory. This indicates that system memory is low.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks>This message is provided only for compatibility with 16-bit Microsoft Windows-based applications.
        ''' <list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the ratio of central processing unit (CPU) time currently spent by the system compacting memory to CPU time currently spent by the system performing other operations. For example, 0x8000 represents 50 percent of CPU time spent compacting memory.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632618.aspx</seealso></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_COMPACTING = &H41
        ''' <summary>The system sends the WM_COMPAREITEM message to determine the relative position of a new item in the sorted list of an owner-drawn combo box or list box. Whenever the application adds a new item, the system sends this message to the owner of a combo box or list box created with the CBS_SORT or LBS_SORT style.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the identifier of the control that sent the WM_COMPAREITEM message.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a COMPAREITEMSTRUCT structure that contains the identifiers and application-supplied data for two items in the combo or list box.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value indicates the relative position of the two items. It may be any of the values from <see cref="ReturnValues.WM_COMPAREITEM"/></description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb775921.aspx</seealso></remarks>
        WM_COMPAREITEM = &H39
        ''' <summary>The WM_CONTEXTMENU message notifies a window that the user clicked the right mouse button (right-clicked) in the window.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the window in which the user right-clicked the mouse. This can be a child window of the window receiving the message. For more information about processing this message, see the Remarks section.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the horizontal position of the cursor, in screen coordinates, at the time of the mouse click.
        ''' The high-order word specifies the vertical position of the cursor, in screen coordinates, at the time of the mouse click. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>No return value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms647592.aspx</seealso></remarks>
        WM_CONTEXTMENU = &H7B
        ''' <summary>An application sends the WM_COPY message to an edit control or combo box to copy the current selection to the clipboard in CF_TEXT format.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>This message does not return a value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms649022.aspx</seealso></remarks>
        WM_COPY = &H301
        ''' <summary>An application sends the WM_COPYDATA message to pass data to another application.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the window passing the data.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a COPYDATASTRUCT structure that contains the data to be passed.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If the receiving application processes this message, it should return TRUE; otherwise, it should return FALSE.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms649011.aspx</seealso></remarks>
        WM_COPYDATA = &H4A
        ''' <summary>The WM_CREATE message is sent when an application requests that a window be created by calling the CreateWindowEx or CreateWindow function. (The message is sent before the function returns.) The window procedure of the new window receives this message after the window is created, but before the window becomes visible.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a CREATESTRUCT structure that contains information about the window being created.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero to continue creation of the window. If the application returns –1, the window is destroyed and the CreateWindowEx or CreateWindow function returns a NULL handle.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632619.aspx</seealso></remarks>
        WM_CREATE = &H1
#Region "WM_CTLCOLOR..."
        ''' <summary>The WM_CTLCOLORBTN message is sent to the parent window of a button before drawing the button. The parent window can change the button's text and background colors. However, only owner-drawn buttons respond to the parent window processing this message.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>A handle to the display context for the button.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>A handle to the button.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it must return a handle to a brush. The system uses the brush to paint the background of the button. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb761849.aspx</seealso></remarks>
        WM_CTLCOLORBTN = &H135
        ''' <summary>The WM_CTLCOLORDLG message is sent to a dialog box before the system draws the dialog box. By responding to this message, the dialog box can set its text and background colors using the specified display device context handle.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the device context for the dialog box.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the dialog box.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it must return a handle to a brush. The system uses the brush to paint the background of the dialog box.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645417.aspx</seealso></remarks>
        WM_CTLCOLORDLG = &H136
        ''' <summary>An edit control that is not read-only or disabled sends the WM_CTLCOLOREDIT message to its parent window when the control is about to be drawn. By responding to this message, the parent window can use the specified device context handle to set the text and background colors of the edit control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>A handle to the device context for the edit control window.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>A handle to the edit control.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it must return the handle of a brush. The system uses the brush to paint the background of the edit control.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb761691.aspx</seealso></remarks>
        WM_CTLCOLOREDIT = &H133
        ''' <summary>Sent to the parent window of a list box before the system draws the list box. By responding to this message, the parent window can set the text and background colors of the list box by using the specified display device context handle.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the device context for the list box.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the list box.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it must return a handle to a brush. The system uses the brush to paint the background of the list box.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb761360.aspx</seealso></remarks>
        WM_CTLCOLORLISTBOX = &H134
        ''' <summary>The WM_CTLCOLORMSGBOX message is sent to the owner window of a message box before Windows draws the message box. By responding to this message, the owner window can set the text and background colors of the message box by using the given display device context handle.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Value of wParam. Identifies the device context for the message box. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>Value of lParam. Identifies the message box.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it must return the handle of a brush. Windows uses the brush to paint the background of the message box.</description></item>
        ''' </list><seealso>http://www.piclist.com/techref/os/win/api/win32/mess/src/msg23.htm</seealso></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_CTLCOLORMSGBOX = &H132
        ''' <summary>This message is sent to the parent window of a scroll bar control when the control is about to be drawn. By responding to this message, the parent window can use the specified display context handle to set the background color of the scroll bar control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the device context for the scroll bar control.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the scroll bar.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it must return the handle to a brush. The system uses the brush to paint the background of the scroll bar control.
        ''' Default Action: The DefWindowProc function selects the default system colors for the scroll bar control.</description></item>
        ''' </list><seealso>http://mtbeta.msdn.microsoft.com/en-us/library/aa931510.aspx?altlang=en-us</seealso></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_CTLCOLORSCROLLBAR = &H137
        ''' <summary>A static control, or an edit control that is read-only or disabled, sends the WM_CTLCOLORSTATIC message to its parent window when the control is about to be drawn. By responding to this message, the parent window can use the specified device context handle to set the text and background colors of the static control.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the device context for the static control window.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the static control.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, the return value is a handle to a brush that the system uses to paint the background of the static control.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb787524.aspx</seealso></remarks>
        WM_CTLCOLORSTATIC = &H138
#End Region
        ''' <summary>An application sends a WM_CUT message to an edit control or combo box to delete (cut) the current selection, if any, in the edit control and copy the deleted text to the clipboard in CF_TEXT format.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>This message does not return a value. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms649023.aspx</seealso></remarks>
        WM_CUT = &H300
        ''' <summary>The WM_DEADCHAR message is posted to the window with the keyboard focus when a WM_KEYUP message is translated by the TranslateMessage function. WM_DEADCHAR specifies a character code generated by a dead key. A dead key is a key that generates a character, such as the umlaut (double-dot), that is combined with another character to form a composite character. For example, the umlaut-O character (Ö) is generated by typing the dead key for the umlaut character, and then typing the O key.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the character code generated by the dead key.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies the repeat count, scan code, extended-key flag, context code, previous key-state flag, and transition-state flag, as shown by <see cref="lParam.WM_CHAR"/>.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646277.aspx</seealso></remarks>
        WM_DEADCHAR = &H103
        ''' <summary>Sent to the owner of a list box or combo box when the list box or combo box is destroyed or when items are removed by the LB_DELETESTRING, LB_RESETCONTENT, CB_DELETESTRING, or CB_RESETCONTENT message. The system sends a WM_DELETEITEM message for each deleted item. The system sends the WM_DELETEITEM message for any deleted list box or combo box item with nonzero item data.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the identifier of the control that sent the WM_DELETEITEM message.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a DELETEITEMSTRUCT structure that contains information about the item deleted from a list box.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return TRUE if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb761362.aspx</seealso></remarks>
        WM_DELETEITEM = &H2D
        ''' <summary>The WM_DESTROY message is sent when a window is being destroyed. It is sent to the window procedure of the window being destroyed after the window is removed from the screen.
        ''' This message is sent first to the window being destroyed and then to the child windows (if any) as they are destroyed. During the processing of the message, it can be assumed that all child windows still exist.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632620.aspx</seealso></remarks>
        WM_DESTROY = &H2
        ''' <summary>The WM_DESTROYCLIPBOARD message is sent to the clipboard owner when a call to the EmptyClipboard function empties the clipboard.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms649024.aspx</seealso></remarks>
        WM_DESTROYCLIPBOARD = &H307
        ''' <summary>Notifies an application of a change to the hardware configuration of a device or the computer.
        ''' A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The event that has occurred. This parameter can be one of the <see cref="wParam.WM_DEVICECHANGE"/> values from the Dbt.h header file.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>A pointer to a structure that contains event-specific data. Its format depends on the value of the wParam parameter. For more information, refer to the documentation for each event.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Return <see cref="ReturnValues.WM_DEVICECHANGE.[TRUE]"/> to grant the request.
        ''' Return <see cref="ReturnValues.WM_DEVICECHANGE.BROADCAST_QUERY_DENY"/> to deny the request.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/aa363480.aspx</seealso></remarks>
        WM_DEVICECHANGE = &H219
        ''' <summary>The WM_DEVMODECHANGE message is sent to all top-level windows whenever the user changes device-mode settings.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a string that specifies the device name.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms533204.aspx</seealso></remarks>
        WM_DEVMODECHANGE = &H1B
        ''' <summary>The WM_DISPLAYCHANGE message is sent to all windows when the display resolution has changed.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the new image depth of the display, in bits per pixel.</description></item>
        ''' <item><term>lParam</term>
        ''' <description></description>The low-order word specifies the horizontal resolution of the screen.
        ''' The high-order word specifies the vertical resolution of the screen. </item>
        ''' <item><term>Return value</term>
        ''' <description>N/A</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms534847.aspx</seealso></remarks>
        WM_DISPLAYCHANGE = &H7E
        ''' <summary>The WM_DRAWCLIPBOARD message is sent to the first window in the clipboard viewer chain when the content of the clipboard changes. This enables a clipboard viewer window to display the new content of the clipboard.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>No return value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms649025.aspx</seealso></remarks>
        WM_DRAWCLIPBOARD = &H308
        ''' <summary>The WM_DRAWITEM message is sent to the parent window of an owner-drawn button, combo box, list box, or menu when a visual aspect of the button, combo box, list box, or menu has changed.
        ''' A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the identifier of the control that sent the WM_DRAWITEM message. If the message was sent by a menu, this parameter is zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a DRAWITEMSTRUCT structure containing information about the item to be drawn and the type of drawing required.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return TRUE. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb775923.aspx</seealso></remarks>
        WM_DRAWITEM = &H2B
        ''' <summary>Sent when the user drops a file on the window of an application that has registered itself as a recipient of dropped files.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>A handle to an internal structure describing the dropped files. Pass this handle DragFinish, DragQueryFile, or DragQueryPoint to retrieve information about the dropped files.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb774303.aspx</seealso></remarks>
        WM_DROPFILES = &H233
        ''' <summary>The WM_ENABLE message is sent when an application changes the enabled state of a window. It is sent to the window whose enabled state is changing. This message is sent before the EnableWindow function returns, but after the enabled state (WS_DISABLED style bit) of the window has changed.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies whether the window has been enabled or disabled. This parameter is TRUE if the window has been enabled or FALSE if the window has been disabled.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632621.aspx</seealso></remarks>
        WM_ENABLE = &HA
        ''' <summary>The WM_ENDSESSION message is sent to an application after the system processes the results of the WM_QUERYENDSESSION message. The WM_ENDSESSION message informs the application whether the session is ending.
        ''' A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>If the session is being ended, this parameter is TRUE; the session can end any time after all applications have returned from processing this message. Otherwise, it is FALSE.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter can be one or more of the <see cref="lParam.WM_ENDSESSION"/> values. If this parameter is 0, the system is shutting down or restarting (it is not possible to determine which event is occurring).
        ''' Note that this parameter is a bit mask. To test for this value, use a bit-wise operation; do not test for equality.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso></seealso></remarks>
        WM_ENDSESSION = &H16
        ''' <summary>The WM_ENTERIDLE message is sent to the owner window of a modal dialog box or menu that is entering an idle state. A modal dialog box or menu enters an idle state when no messages are waiting in its queue after it has processed one or more previous messages.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies whether the message is the result of a dialog box or a menu being displayed. This parameter can be one of the <see cref="wParam.WM_ENTERIDLE"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the dialog box (if wParam is MSGF_DIALOGBOX) or window containing the displayed menu (if wParam is MSGF_MENU).</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645422.aspx</seealso></remarks>
        WM_ENTERIDLE = &H121
        ''' <summary>The WM_ENTERMENULOOP message informs an application's main window procedure that a menu modal loop has been entered.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>    Specifies whether the window menu was entered using the TrackPopupMenu function. This parameter has a value of TRUE if the window menu was entered using TrackPopupMenu, and FALSE if it was not.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms647595.aspx</seealso></remarks>
        WM_ENTERMENULOOP = &H211
        ''' <summary>The WM_ENTERSIZEMOVE message is sent one time to a window after it enters the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns.
        ''' The system sends the WM_ENTERSIZEMOVE message regardless of whether the dragging of full windows is enabled.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632622.aspx</seealso></remarks>
        WM_ENTERSIZEMOVE = &H231
        ''' <summary>The WM_ERASEBKGND message is sent when the window background must be erased (for example, when a window is resized). The message is sent to prepare an invalidated portion of a window for painting.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the device context.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return nonzero if it erases the background; otherwise, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms648055.aspx</seealso></remarks>
        WM_ERASEBKGND = &H14
        ''' <summary>The WM_EXITMENULOOP message informs an application's main window procedure that a menu modal loop has been exited.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies whether the menu is a shortcut menu. This parameter has a value of TRUE if it is a shortcut menu, FALSE if it is not.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms647599.aspx</seealso></remarks>
        WM_EXITMENULOOP = &H212
        ''' <summary>The WM_EXITSIZEMOVE message is sent one time to a window, after it has exited the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632623.aspx</seealso></remarks>
        WM_EXITSIZEMOVE = &H232
        ''' <summary>An application sends the WM_FONTCHANGE message to all top-level windows in the system after changing the pool of font resources.
        ''' To send this message, call the SendMessage function with the following parameters. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This message has no parameters.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This message has no parameters.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>N/A</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms533930.aspx</seealso></remarks>
        WM_FONTCHANGE = &H1D
        ''' <summary>The WM_GETDLGCODE message is sent to the window procedure associated with a control. By default, the system handles all keyboard input to the control; the system interprets certain types of keyboard input as dialog box navigation keys. To override this default behavior, the control can respond to the WM_GETDLGCODE message to indicate the types of input it wants to process itself.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The virtual key, pressed by the user, that prompted Microsoft Windows to issue this notification. The handler must selectively handle these keys. For instance, the handler might accept and process VK_RETURN but delegate VK_TAB to the owner window.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to an MSG structure (or NULL if the system is performing a query).</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value is one or more of the <see cref="ReturnValues.WM_GETDLGCODE"/> values, indicating which type of input the application processes.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645425.aspx</seealso></remarks>
        WM_GETDLGCODE = &H87
        ''' <summary>An application sends a WM_GETFONT message to a control to retrieve the font with which the control is currently drawing its text.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value is a handle to the font used by the control, or NULL if the control is using the system font. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632624.aspx</seealso></remarks>
        WM_GETFONT = &H31
        ''' <summary>An application sends a WM_GETHOTKEY message to determine the hot key associated with a window.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value is the virtual-key code and modifiers for the hot key, or NULL if no hot key is associated with the window. The virtual-key code is in the low byte of the return value and the modifiers are in the high byte. The modifiers can be a combination of the <see cref="ReturnValues.WM_GETHOTKEY_high"/> flags.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646278.aspx</seealso></remarks>
        WM_GETHOTKEY = &H33
        ''' <summary>The WM_GETICON message is sent to a window to retrieve a handle to the large or small icon associated with a window. The system displays the large icon in the ALT+TAB dialog, and the small icon in the window caption.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the type of icon being retrieved. This parameter can be one of the <see cref="wParam.WM_GETICON"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value is a handle to the large or small icon, depending on the value of wParam. When an application receives this message, it can return a handle to a large or small icon, or pass the message to the DefWindowProc function.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632625.aspx</seealso></remarks>
        WM_GETICON = &H7F
        ''' <summary>The WM_GETMINMAXINFO message is sent to a window when the size or position of the window is about to change. An application can use this message to override the window's default maximized size and position, or its default minimum or maximum tracking size.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>    Pointer to a MINMAXINFO structure that contains the default maximized position and dimensions, and the default minimum and maximum tracking sizes. An application can override the defaults by setting the members of this structure.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632626.aspx</seealso></remarks>
        WM_GETMINMAXINFO = &H24
        ''' <summary>Active Accessibility sends the WM_GETOBJECT message to obtain information about an accessible object contained in a server application.
        ''' Applications never send this message directly. It is sent only by Active Accessibility in response to calls to AccessibleObjectFromPoint, AccessibleObjectFromEvent, or AccessibleObjectFromWindow. However, server applications handle this message. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Provides additional information about the message and is used only by the system. Servers pass dwFlags as the wParam parameter in the call to LresultFromObject when handling the message.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Object identifier. This value is one of the object identifier constants or a custom object identifier. Servers usually process WM_GETOBJECT only if the dwObjId is OBJID_CLIENT.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value depends on whether the window or control that receives the message implements IAccessible:
        ''' <list type="list"><item>If implementing IAccessible for the object, the application returns the result obtained from LresultFromObject.</item>
        ''' <item>If not implementing IAccessible, or if dwObjID is not OBJID_CLIENT, server applications should allow the message to pass to DefWindowProc.</item>
        ''' </list></description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms696155.aspx</seealso></remarks>
        WM_GETOBJECT = &H3D
        ''' <summary>An application sends a WM_GETTEXT message to copy the text that corresponds to a window into a buffer provided by the caller.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the maximum number of TCHARs to be copied, including the terminating null character.
        ''' Windows NT/2000/XP:ANSI applications may have the string in the buffer reduced in size (to a minimum of half that of the wParam value) due to conversion from ANSI to Unicode. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to the buffer that is to receive the text.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>    The return value is the number of TCHARs copied, not including the terminating null character.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632627.aspx</seealso></remarks>
        WM_GETTEXT = &HD
        ''' <summary>An application sends a WM_GETTEXTLENGTH message to determine the length, in characters, of the text associated with a window.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used and must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used and must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value is the length of the text in TCHARs, not including the terminating null character. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632628.aspx</seealso></remarks>
        WM_GETTEXTLENGTH = &HE
#Region "WM_HANDHELD..."
        ''' <summary>The lowest value for handheld message.</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_HANDHELDFIRST = &H358
        ''' <summary>The highest value for handheld message.</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_HANDHELDLAST = &H35F
#End Region
        ''' <summary>Indicates that the user pressed the F1 key. If a menu is active when F1 is pressed, WM_HELP is sent to the window associated with the menu; otherwise, WM_HELP is sent to the window that has the keyboard focus. If no window has the keyboard focus, WM_HELP is sent to the currently active window.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The address of a HELPINFO structure that contains information about the menu item, control, dialog box, or window for which Help is requested.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns TRUE.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb774305.aspx</seealso></remarks>
        WM_HELP = &H53
        ''' <summary>The WM_HOTKEY message is posted when the user presses a hot key registered by the RegisterHotKey function. The message is placed at the top of the message queue associated with the thread that registered the hot key.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the identifier of the hot key that generated the message. If the message was generated by a system-defined hot key, this parameter will be one of the <see cref="wParam.WM_HOTKEY"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the keys that were to be pressed in combination with the key specified by the high-order word to generate the WM_HOTKEY message. This word can be one or more of the <see cref="lParam.WM_HOTKEY_low"/> values. The high-order word specifies the virtual key code of the hot key.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>No return value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646279.aspx</seealso></remarks>
        WM_HOTKEY = &H312
        ''' <summary>The WM_HSCROLL message is sent to a window when a scroll event occurs in the window's standard horizontal scroll bar. This message is also sent to the owner of a horizontal scroll bar control when a scroll event occurs in the control.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The low-order word specifies a scroll bar value that indicates the user's scrolling request. This word can be one of the <see cref="wParam.WM_HSCROLL_low"/> values.
        ''' The high-order word specifies the current position of the scroll box if the low-order word is SB_THUMBPOSITION or SB_THUMBTRACK; otherwise, this word is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>If the message is sent by a scroll bar, then this parameter is the handle to the scroll bar control. If the message is not sent by a scroll bar, this parameter is NULL.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb787575.aspx</seealso></remarks>
        WM_HSCROLL = &H114
        ''' <summary>The WM_HSCROLLCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window. This occurs when the clipboard contains data in the CF_OWNERDISPLAY format and an event occurs in the clipboard viewer's horizontal scroll bar. The owner should scroll the clipboard image and update the scroll bar values.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the clipboard viewer window.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word of lParam specifies a scroll bar event. This parameter can be one of the <see cref="wParam.WM_HSCROLL_low"/> values. The high-order word of lParam specifies the current position of the scroll box if the low-order word of lParam is SB_THUMBPOSITION; otherwise, the high-order word is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms649026.aspx</seealso></remarks>
        WM_HSCROLLCLIPBOARD = &H30E
        ''' <summary>Windows NT 3.51 and earlier: The WM_ICONERASEBKGND message is sent to a minimized window when the background of the icon must be filled before painting the icon. A window receives this message only if a class icon is defined for the window; otherwise, WM_ERASEBKGND is sent. This message is not sent by newer versions of System.Windows.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the device context of the icon.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return nonzero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms648056.aspx</seealso></remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> WM_ICONERASEBKGND = &H27
#Region "WM_IME_..."
        ''' <summary>Sent to an application when the IME gets a character of the conversion result. A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>DBCS: A single- or double-byte character value. For a double-byte character, (BYTE)(wParam >> 8) contains the lead byte. Note that the parentheses are necessary because the cast operator has higher precedence than the shift operator.
        ''' Unicode: A Unicode character value. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>The repeat count, scan code, extended key flag, context code, previous key state flag, and transition state flag, with values as defined in <see cref="lParam.WM_CHAR"/>.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>N/A</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776090.aspx</seealso></remarks>
        WM_IME_CHAR = &H286
        ''' <summary>Sent to an application when the IME changes composition status as a result of a keystroke. A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>DBCS character representing the latest change to the composition string.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Value specifying how the composition string or character changed. This parameter can be one or more of the <see cref="lparam.WM_IME_COMPOSITION"/> values. For more information about these values, see IME Composition String Values (http://msdn2.microsoft.com/en-us/library/ms776087.aspx).</description></item>
        ''' <item><term>Return value</term>
        ''' <description>This message has no return value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776172.aspx</seealso></remarks>
        WM_IME_COMPOSITION = &H10F
        ''' <summary>Sent to an application when the IME window finds no space to extend the area for the composition window. A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This message has no parameters.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This message has no parameters.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>This message has no return value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776188.aspx</seealso></remarks>
        WM_IME_COMPOSITIONFULL = &H284
        ''' <summary>Sent by an application to direct the IME window to carry out the requested command. The application uses this message to control the IME window that it has created. To send this message, the application calls the SendMessage function with the following parameters.</summary>
        ''' <remarks>The command. This parameter can have one of the following values:<list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The command. This parameter can have one of the <see cref="wParam.WM_IME_CONTROL"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Command-specific data, with format dependent on the value of the wParam parameter. For more information, refer to the documentation for each command.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The message returns a command-specific value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776174.aspx</seealso></remarks>
        WM_IME_CONTROL = &H283
        ''' <summary>Sent to an application when the IME ends composition. A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This message has no parameters.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This message has no parameters.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>This message has no return value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776081.aspx</seealso></remarks>
        WM_IME_ENDCOMPOSITION = &H10E
        ''' <summary>Sent to an application by the IME to notify the application of a key press and to keep message order. A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Virtual key code of the key.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Repeat count, scan code, extended key flag, context code, previous key state flag, and transition state flag, as shown in <see cref="lParam.WM_CHAR"/>.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return 0 if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776177.aspx</seealso></remarks>
        WM_IME_KEYDOWN = &H290
        ''' <summary>The highest value of IME keyboard message.</summary>
        ''' <remarks>Note: There is no WM_IME_KEYFIRST</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_IME_KEYLAST = &H10F
        ''' <summary>Sent to an application by the IME to notify the application of a key release and to keep message order. A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Virtual key code of the key.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Repeat count, scan code, extended key flag, context code, previous key state flag, and transition state flag, as shown in <see cref="lParam.WM_CHAR"/> .</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return 0 if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776074.aspx</seealso></remarks>
        WM_IME_KEYUP = &H291
        ''' <summary>Sent to an application to notify it of changes to the IME window. A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The command. This parameter can be one of the <see cref="wParam.WM_IME_NOTIFY"/>.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Command-specific data, with format dependent on the value of the wParam parameter. For more information, refer to the documentation for each command.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value depends on the command sent.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776096.aspx</seealso></remarks>
        WM_IME_NOTIFY = &H282
        ''' <summary>Sent to an application to provide commands and request information. A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Command. This parameter can have one of the <see cref="wParam.WM_IME_REQUEST"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Command-specific data. For more information, see the description for each command.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns a command-specific value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776130.aspx</seealso></remarks>
        WM_IME_REQUEST = &H288
        ''' <summary>Sent to an application when the operating system is about to change the current IME. A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Selection indicator. This parameter specifies TRUE if the indicated IME is selected. The parameter is set to FALSE if the specified IME is no longer selected.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Input locale identifier associated with the IME.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>This message has no return value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776083.aspx</seealso></remarks>
        WM_IME_SELECT = &H285
        ''' <summary>Sent to an application when a window is activated. A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>TRUE if the window is active, and FALSE otherwise.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>isplay options. This parameter can have one or more of the <see cref="lParam.WM_IME_SETCONTEXT"/> values:</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the value returned by DefWindowProc or ImmIsUIMessage.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776117.aspx</seealso></remarks>
        WM_IME_SETCONTEXT = &H281
        ''' <summary>Sent immediately before the IME generates the composition string as a result of a keystroke. A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This message has no parameters.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This message has no parameters.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>This message has no return value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776181.aspx</seealso></remarks>
        WM_IME_STARTCOMPOSITION = &H10D
#End Region
        ''' <summary>The WM_INITDIALOG message is sent to the dialog box procedure immediately before a dialog box is displayed. Dialog box procedures typically use this message to initialize controls and carry out any other initialization tasks that affect the appearance of the dialog box.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the control to receive the default keyboard focus. The system assigns the default keyboard focus only if the dialog box procedure returns TRUE.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies additional initialization data. This data is passed to the system as the lParam parameter in a call to the CreateDialogIndirectParam, CreateDialogParam, DialogBoxIndirectParam, or DialogBoxParam function used to create the dialog box. For property sheets, this parameter is a pointer to the PROPSHEETPAGE structure used to create the page. This parameter is zero if any other dialog box creation function is used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The dialog box procedure should return TRUE to direct the system to set the keyboard focus to the control specified by wParam. Otherwise, it should return FALSE to prevent the system from setting the default keyboard focus.
        ''' The dialog box procedure should return the value directly. The DWL_MSGRESULT value set by the SetWindowLong function is ignored. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645428.aspx</seealso></remarks>
        WM_INITDIALOG = &H110
        ''' <summary>The WM_INITMENU message is sent when a menu is about to become active. It occurs when the user clicks an item on the menu bar or presses a menu key. This allows the application to modify the menu before it is displayed.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the menu to be initialized.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646344.aspx</seealso></remarks>
        WM_INITMENU = &H116
        ''' <summary>The WM_INITMENUPOPUP message is sent when a drop-down menu or submenu is about to become active. This allows an application to modify the menu before it is displayed, without changing the entire menu.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the drop-down menu or submenu.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the zero-based relative position of the menu item that opens the drop-down menu or submenu.
        ''' The high-order word indicates whether the drop-down menu is the window menu. If the menu is the window menu, this parameter is TRUE; otherwise, it is FALSE. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646347.aspx</seealso></remarks>
        WM_INITMENUPOPUP = &H117
        ''' <summary>The WM_INPUT message is sent to the window that is getting raw input. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Input code. This parameter can be one of the <see cref="wParam.WM_INPUT"/> values. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the <see cref="API.RawInput.RAWINPUT_Marshalling"/> structure that contains the raw input from the device. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><para>To get the wParam value, use the GET_RAWINPUT_CODE_WPARAM macro.</para>
        ''' <para>Note that lParam has the handle to the RAWINPUT structure, not a pointer to it. To get the raw data, use the handle in the call to GetRawInputData.</para>
        ''' <para>Raw input is available only when the application calls RegisterRawInputDevices with valid device specifications</para></remarks>
        WM_INPUT = &HFF
        ''' <summary>The WM_INPUTLANGCHANGE message is sent to the topmost affected window after an application's input language has been changed. You should make any application-specific settings and pass the message to the DefWindowProc function, which passes the message to all first-level child System.Windows. These child windows can pass the message to DefWindowProc to have it pass the message to their child windows, and so on.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the character set of the new locale.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Input locale identifier. For more information, see Languages, Locales, and Keyboard Layouts.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return nonzero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632629.aspx</seealso></remarks>
        WM_INPUTLANGCHANGE = &H51
        ''' <summary>The WM_INPUTLANGCHANGEREQUEST message is posted to the window with the focus when the user chooses a new input language, either with the hotkey (specified in the Keyboard control panel application) or from the indicator on the system taskbar. An application can accept the change by passing the message to the DefWindowProc function or reject the change (and prevent it from taking place) by returning immediately.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Information about the new input locale. This parameter can be a combination of the <see cref="wParam.WM_INPUTLANGCHANGEREQUEST"/> flags.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Input locale identifier. For more information, see Languages, Locales, and Keyboard Layouts.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>This message is posted, not sent, to the application, so the return value is ignored. To accept the change, the application should pass the message to DefWindowProc. To reject the change, the application should return zero without calling DefWindowProc.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632630.aspx</seealso></remarks>
        WM_INPUTLANGCHANGEREQUEST = &H50
#Region "Keyboard"
        ''' <summary>WM_KEYDOWN</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the virtual-key code of the nonsystem key.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies the repeat count, scan code, extended-key flag, context code, previous key-state flag, and transition-state flag, as shown in <see cref="lParam.WM_CHAR"/> table.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646280.aspx</seealso></remarks>
        WM_KEYDOWN = &H100
        ''' <summary>This message filters for keyboard messages. (This is the lowest value of keyboard message.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>None.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/aa931746.aspx</seealso></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_KEYFIRST = &H100
        ''' <summary>This message filters for keyboard messages. (This is the highest value of keyboard message)</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>None.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/aa453875.aspx</seealso></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_KEYLAST = &H108
        ''' <summary>The WM_KEYUP message is posted to the window with the keyboard focus when a nonsystem key is released. A nonsystem key is a key that is pressed when the ALT key is not pressed, or a keyboard key that is pressed when a window has the keyboard focus.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the virtual-key code of the nonsystem key.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies the repeat count, scan code, extended-key flag, context code, previous key-state flag, and transition-state flag, as shown in <see cref="lParam.WM_CHAR"/>.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646281.aspx</seealso></remarks>
        WM_KEYUP = &H101
        ''' <summary>The WM_CHAR message is posted to the window with the keyboard focus when a WM_KEYDOWN message is translated by the TranslateMessage function. The WM_CHAR message contains the character code of the key that was pressed.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the character code of the key.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies the repeat count, scan code, extended-key flag, context code, previous key-state flag, and transition-state flag, as shown in <see cref="lParam.WM_CHAR"/>.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646276.aspx</seealso></remarks>
        WM_CHAR = &H102
        ''' <summary>The WM_SYSKEYDOWN message is posted to the window with the keyboard focus when the user presses the F10 key (which activates the menu bar) or holds down the ALT key and then presses another key. It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYDOWN message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the virtual-key code of the key being pressed.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies the repeat count, scan code, extended-key flag, context code, previous key-state flag, and transition-state flag, as shown in the <see cref="lParam.WM_CHAR"/>.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646286.aspx</seealso></remarks>
        WM_SYSKEYDOWN = &H104
        ''' <summary>The WM_SYSKEYUP message is posted to the window with the keyboard focus when the user releases a key that was pressed while the ALT key was held down. It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYUP message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the virtual-key code of the key being released.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies the repeat count, scan code, extended-key flag, context code, previous key-state flag, and transition-state flag, as shown in the <see cref="lParam.WM_CHAR"/>.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646287.aspx</seealso></remarks>
        WM_SYSKEYUP = &H105
        ''' <summary>The WM_SYSCHAR message is posted to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. It specifies the character code of a system character key — that is, a character key that is pressed while the ALT key is down.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the character code of the window menu key.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies the repeat count, scan code, extended-key flag, context code, previous key-state flag, and transition-state flag, as shown in the <see cref="lParam.WM_CHAR"/>.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646357.aspx</seealso></remarks>
        WM_SYSCHAR = &H106
#End Region
        ''' <summary>The WM_KILLFOCUS message is sent to a window immediately before it loses the keyboard focus.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the window that receives the keyboard focus. This parameter can be NULL.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646282.aspx</seealso></remarks>
        WM_KILLFOCUS = &H8
#Region "Mouse buttons"
#Region "LBUTTON"
        ''' <summary>The <see cref="WM_LBUTTONDBLCLK"/> message is posted when the user double-clicks the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Indicates whether various virtual keys are down. This parameter can be one or more of the <see cref="wParam.WM_LBUTTONDBLCLK"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.
        ''' The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645606.aspx</seealso></remarks>
        WM_LBUTTONDBLCLK = &H203
        ''' <summary>The <see cref="WM_LBUTTONDOWN"/> message is posted when the user presses the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Indicates whether various virtual keys are down. This parameter can be one or more of the <see cref="wParam.WM_LBUTTONDBLCLK"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.
        ''' The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645607.aspx</seealso></remarks>
        WM_LBUTTONDOWN = &H201
        ''' <summary>The <see cref="WM_LBUTTONUP"/> message is posted when the user releases the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        ''' A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Indicates whether various virtual keys are down. This parameter can be one or more of the <see cref="wParam.WM_LBUTTONDBLCLK"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.
        ''' The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645608.aspx</seealso></remarks>
        WM_LBUTTONUP = &H202
#End Region
#Region "MBUTTON"
        ''' <summary>The <see cref="WM_MBUTTONDBLCLK"/> message is posted when the user double-clicks the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Indicates whether various virtual keys are down. This parameter can be one or more of the <see cref="wParam.WM_LBUTTONDBLCLK"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.
        ''' The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645609.aspx</seealso></remarks>
        WM_MBUTTONDBLCLK = &H209
        ''' <summary>The <see cref="WM_MBUTTONDOWN"/> message is posted when the user presses the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Indicates whether various virtual keys are down. This parameter can be one or more of the <see cref="wParam.WM_LBUTTONDBLCLK"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.
        ''' The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645610.aspx</seealso></remarks>
        WM_MBUTTONDOWN = &H207
        ''' <summary>The <see cref="WM_MBUTTONUP"/> message is posted when the user releases the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Indicates whether various virtual keys are down. This parameter can be one or more of the <see cref="wParam.WM_LBUTTONDBLCLK"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.
        ''' The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645611.aspx</seealso></remarks>
        WM_MBUTTONUP = &H208
#End Region
#Region "RBUTTON"
        ''' <summary>The WM_RBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Indicates whether various virtual keys are down. This parameter can be one or more of the <see cref="wParam.WM_LBUTTONDBLCLK"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.
        ''' The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646241.aspx</seealso></remarks>
        WM_RBUTTONDBLCLK = &H206
        ''' <summary>The WM_RBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Indicates whether various virtual keys are down. This parameter can be one or more of the <see cref="wParam.WM_LBUTTONDBLCLK"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.
        ''' The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646242.aspx</seealso></remarks>
        WM_RBUTTONDOWN = &H204
        ''' <summary>The WM_RBUTTONUP message is posted when the user releases the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Indicates whether various virtual keys are down. This parameter can be one or more of the <see cref="wParam.WM_LBUTTONDBLCLK"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.
        ''' The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646243.aspx</seealso></remarks>
        WM_RBUTTONUP = &H205
#End Region
#Region "XBUTTON"
        ''' <summary>The WM_XBUTTONDBLCLK message is posted when the user double-clicks the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The low-order word indicates whether various virtual keys are down. It can be one or more of the <see cref="wParam.WM_LBUTTONDBLCLK"/> values.
        ''' The high-order word indicates which button was double-clicked. It can be one of the <see cref="wParam.WM_PARENTNOTIFY_WM_XBUTTONDOWN_high"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.
        ''' The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return TRUE. For more information about processing the return value, see the Remarks section (http://msdn2.microsoft.com/en-us/library/ms646244.aspx).</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646244.aspx</seealso></remarks>
        WM_XBUTTONDBLCLK = &H20D
        ''' <summary>The WM_XBUTTONDOWN message is posted when the user presses the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        ''' A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The low-order word indicates whether various virtual keys are down. It can be one or more of the <see cref="wParam.WM_LBUTTONDBLCLK"/> values.
        ''' The high-order word indicates which button was clicked. It can be one of the <see cref="wParam.WM_PARENTNOTIFY_WM_XBUTTONDOWN_high"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.
        ''' The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return TRUE. For more information about processing the return value, see the Remarks section (http://msdn2.microsoft.com/en-us/library/ms646245.aspx).</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646245.aspx</seealso></remarks>
        WM_XBUTTONDOWN = &H20B
        ''' <summary>The WM_XBUTTONUP message is posted when the user releases the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The low-order word indicates whether various virtual keys are down. It can be one or more of the <see cref="wParam.WM_LBUTTONDBLCLK"/> values.
        ''' The high-order word indicates which button was double-clicked. It can be one of the <see cref="wParam.WM_PARENTNOTIFY_WM_XBUTTONDOWN_high"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.
        ''' The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return TRUE. For more information about processing the return value, see the Remarks section.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646246.aspx</seealso></remarks>
        WM_XBUTTONUP = &H20C
#End Region
#End Region
#Region "WM_MDI..."
        ''' <summary>An application sends the <see cref="WM_MDIACTIVATE"/> message to a multiple-document interface (MDI) client window to instruct the client window to activate a different MDI child window.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the MDI child window to be activated.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application sends this message to an MDI client window, the return value is zero.
        ''' An MDI child window should return zero if it processes this message. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms644911.aspx</seealso></remarks>
        WM_MDIACTIVATE = &H222
        ''' <summary>An application sends the WM_MDICASCADE message to a multiple-document interface (MDI) client window to arrange all its child windows in a cascade format.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the cascade behavior. This parameter can be one or more of the <see cref="wParam.WM_MDICASCADE"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If the message succeeds, the return value is TRUE.
        ''' If the message fails, the return value is FALSE. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms644912.aspx</seealso></remarks>
        WM_MDICASCADE = &H227
        ''' <summary>An application sends the WM_MDICREATE message to a multiple-document interface (MDI) client window to create an MDI child window.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to an MDICREATESTRUCT structure containing information that the system uses to create the MDI child window.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If the message succeeds, the return value is the handle to the new child window.
        ''' If the message fails, the return value is NULL. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms644913.aspx</seealso></remarks>
        WM_MDICREATE = &H220
        ''' <summary>An application sends the WM_MDIDESTROY message to a multiple-document interface (MDI) client window to close an MDI child window.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the MDI child window to be closed.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>This message always returns zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms644914.aspx</seealso></remarks>
        WM_MDIDESTROY = &H221
        ''' <summary>An application sends the WM_MDIGETACTIVE message to a multiple-document interface (MDI) client window to retrieve the handle to the active MDI child window.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies the maximized state. If this parameter is not NULL, it is a pointer to a value that indicates the maximized state of the MDI child window. If the value is TRUE, the window is maximized; a value of FALSE indicates that it is not. If this parameter is NULL, the parameter is ignored.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value is the handle to the active MDI child window. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms644915.aspx</seealso></remarks>
        WM_MDIGETACTIVE = &H229
        ''' <summary>An application sends the WM_MDIICONARRANGE message to a multiple document interface (MDI) client window to arrange all minimized MDI child System.Windows. It does not affect child windows that are not minimized.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>not used; must be zero </description></item>
        ''' <item><term>lParam</term>
        ''' <description>not used; must be zero </description></item>
        ''' <item><term>Return value</term>
        ''' <description>N/A</description></item>
        ''' </list><seealso>http://www.piclist.com/techref/os/win/api/win32/mess/src/msg25_16.htm</seealso></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_MDIICONARRANGE = &H228
        ''' <summary>An application sends the WM_MDIMAXIMIZE message to a multiple-document interface (MDI) client window to maximize an MDI child window. The system resizes the child window to make its client area fill the client window. The system places the child window's window menu icon in the rightmost position of the frame window's menu bar, and places the child window's restore icon in the leftmost position. The system also appends the title bar text of the child window to that of the frame window.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the MDI child window to be maximized.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value is always zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms644917.aspx</seealso></remarks>
        WM_MDIMAXIMIZE = &H225
        ''' <summary>An application sends the WM_MDINEXT message to a multiple-document interface (MDI) client window to activate the next or previous child window.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the MDI child window. The system activates the child window that is immediately before or after the specified child window, depending on the value of the lParam parameter. If the wParam parameter is NULL, the system activates the child window that is immediately before or after the currently active child window.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>If this parameter is zero, the system activates the next MDI child window and places the child window identified by the wParam parameter behind all other child System.Windows. If this parameter is nonzero, the system activates the previous child window, placing it in front of the child window identified by wParam.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value is always zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms644918.aspx</seealso></remarks>
        WM_MDINEXT = &H224
        ''' <summary>An application sends the WM_MDIREFRESHMENU message to a multiple-document interface (MDI) client window to refresh the window menu of the MDI frame window.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If the message succeeds, the return value is the handle to the frame window menu.
        ''' If the message fails, the return value is NULL. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms644919.aspx</seealso></remarks>
        WM_MDIREFRESHMENU = &H234
        ''' <summary>An application sends the WM_MDIRESTORE message to a multiple-document interface (MDI) client window to restore an MDI child window from maximized or minimized size.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the MDI child window to be restored.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value is always zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms644920.aspx</seealso></remarks>
        WM_MDIRESTORE = &H223
        ''' <summary>Handle to the new frame window menu. If this parameter is NULL, the frame window menu is not changed.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the new frame window menu. If this parameter is NULL, the frame window menu is not changed.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the new window menu. If this parameter is NULL, the window menu is not changed.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If the message succeeds, the return value is the handle to the old frame window menu.
        ''' If the message fails, the return value is zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms644921.aspx</seealso></remarks>
        WM_MDISETMENU = &H230
        ''' <summary>An application sends the WM_MDITILE message to a multiple-document interface (MDI) client window to arrange all of its MDI child windows in a tile format.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the tiling option. This parameter can be one of the <see cref="wParam.WM_MDITILE"/> values, optionally combined with MDITILE_SKIPDISABLED to prevent disabled MDI child windows from being tiled.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If the message succeeds, the return value is TRUE.
        ''' If the message fails, the return value is FALSE. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms644922.aspx</seealso></remarks>
        WM_MDITILE = &H226
#End Region
        ''' <summary>The WM_MEASUREITEM message is sent to the owner window of a combo box, list box, list view control, or menu item when the control or menu is created.
        ''' A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Contains the value of the CtlID member of the MEASUREITEMSTRUCT structure pointed to by the lpMeasureItem parameter. This value identifies the control that sent the WM_MEASUREITEM message. If the value is zero, the message was sent by a menu. If the value is nonzero, the message was sent by a combo box or by a list box. If the value is nonzero, and the value of the itemID member of the MEASUREITEMSTRUCT pointed to by lpMeasureItem is (UINT) –1, the message was sent by a combo edit field.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a MEASUREITEMSTRUCT structure that contains the dimensions of the owner-drawn control or menu item.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return TRUE. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb775925.aspx</seealso></remarks>
        WM_MEASUREITEM = &H2C
        ''' <summary>The WM_MENUCHAR message is sent when a menu is active and the user presses a key that does not correspond to any mnemonic or accelerator key. This message is sent to the window that owns the menu.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The low-order word specifies the character code that corresponds to the key the user pressed.
        ''' The high-order word specifies the active menu type. This parameter can be one of the <see cref="wParam.WM_MENUCHAR_high"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the active menu.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application that processes this message should return one of the <see cref="ReturnValues.WM_MENUCHAR"/> values in the high-order word of the return value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646349.aspx</seealso></remarks>
#Region "WM_MENU..."
        WM_MENUCHAR = &H120
        ''' <summary>The WM_MENUCOMMAND message is sent when the user makes a selection from a menu.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the zero-based index of the item selected.
        ''' Windows 98/Me: The high word is the zero-based index of the item selected. The low word is the item ID. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the menu for the item selected.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>No return value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms647603.aspx</seealso></remarks>
        WM_MENUCOMMAND = &H126
        ''' <summary>The WM_MENUDRAG message is sent to the owner of a drag-and-drop menu when the user drags a menu item.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the position of the item where the drag operation began.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the menu containing the item.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The application should return one of the <see cref="ReturnValues.WM_MENUDRAG"/> values.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms647606.aspx</seealso></remarks>
        WM_MENUDRAG = &H123
        ''' <summary>The WM_MENUGETOBJECT message is sent to the owner of a drag-and-drop menu when the mouse cursor enters a menu item or moves from the center of the item to the top or bottom of the item.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a MENUGETOBJECTINFO structure.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The application should return one of the <see cref="ReturnValues.WM_MENUGETOBJECT"/> values.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms647607.aspx</seealso></remarks>
        WM_MENUGETOBJECT = &H124
        ''' <summary>The WM_MENURBUTTONUP message is sent when the user releases the right mouse button while the cursor is on a menu item.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the position of the item when the mouse was released.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the menu containing the item.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>No return value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms647610.aspx</seealso></remarks>
        WM_MENURBUTTONUP = &H122
        ''' <summary>The WM_MENUSELECT message is sent to a menu's owner window when the user selects a menu item. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The low-order word specifies the menu item or submenu index. If the selected item is a command item, this parameter contains the identifier of the menu item. If the selected item opens a drop-down menu or submenu, this parameter contains the index of the drop-down menu or submenu in the main menu, and the lParam parameter contains the handle to the main (clicked) menu; use the GetSubMenu function to get the menu handle to the drop-down menu or submenu.
        ''' The high-order word specifies one or more menu flags. This parameter can be one or more of the <see cref="wParam.WM_MENUSELECT_high"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the menu that was clicked.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646352.aspx</seealso></remarks>
        WM_MENUSELECT = &H11F
        ''' <summary>The WM_MOUSEACTIVATE message is sent when the cursor is in an inactive window and the user presses a mouse button. The parent window receives this message only if the child window passes it to the DefWindowProc function.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the top-level parent window of the window being activated.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the hit-test value returned by the DefWindowProc function as a result of processing the WM_NCHITTEST message. For a list of hit-test values, see WM_NCHITTEST.
        ''' The high-order word specifies the identifier of the mouse message generated when the user pressed a mouse button. The mouse message is either discarded or posted to the window, depending on the return value.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value specifies whether the window should be activated and whether the identifier of the mouse message should be discarded. It must be one of the <see cref="ReturnValues.WM_MOUSEACTIVATE"/> values.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645612.aspx</seealso></remarks>
        WM_MOUSEACTIVATE = &H21
#End Region
#Region "WM_MOUSE..."
        ''' <summary>Lowest value of mouse message</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_MOUSEFIRST = &H200
        ''' <summary>The WM_MOUSEHOVER message is posted to a window when the cursor hovers over the client area of the window for the period of time specified in a prior call to TrackMouseEvent.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Indicates whether various virtual keys are down. This parameter can be one or more of the <see cref="wParam.WM_LBUTTONDBLCLK"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.
        ''' The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645613.aspx</seealso></remarks>
        WM_MOUSEHOVER = &H2A1
        ''' <summary>Highest value of mouse message</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_MOUSELAST = &H20D
        ''' <summary>The WM_MOUSELEAVE message is posted to a window when the cursor leaves the client area of the window specified in a prior call to TrackMouseEvent.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645615.aspx</seealso></remarks>
        WM_MOUSELEAVE = &H2A3
        ''' <summary>The WM_MOUSEMOVE message is posted to a window when the cursor moves. If the mouse is not captured, the message is posted to the window that contains the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Indicates whether various virtual keys are down. This parameter can be one or more of the <see cref="wParam.WM_LBUTTONDBLCLK"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.
        ''' The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645616.aspx</seealso></remarks>
        WM_MOUSEMOVE = &H200
        ''' <summary>The WM_MOUSEWHEEL message is sent to the focus window when the mouse wheel is rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message, since DefWindowProc propagates it up the parent chain until it finds a window that processes it.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The high-order word indicates the distance the wheel is rotated, expressed in multiples or divisions of WHEEL_DELTA, which is 120. A positive value indicates that the wheel was rotated forward, away from the user; a negative value indicates that the wheel was rotated backward, toward the user.
        ''' The low-order word indicates whether various virtual keys are down. This parameter can be one or more of the <see cref="wParam.WM_LBUTTONDBLCLK"/> values (but <see cref="wParam.WM_LBUTTONDBLCLK"/> are <see cref="Integer">Integers</see> while low-order word is <see cref="Short"/>). </description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the x-coordinate of the pointer, relative to the upper-left corner of the screen.
        ''' The high-order word specifies the y-coordinate of the pointer, relative to the upper-left corner of the screen. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645617.aspx</seealso></remarks>
        WM_MOUSEWHEEL = &H20A
        ''' <summary><para>The WM_MOUSEHWHEEL message is sent to the focus window when the mouse's horizontal scroll wheel is tilted or rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message, since DefWindowProc propagates it up the parent chain until it finds a window that processes it.</para>
        ''' <para>A window receives this message through its WindowProc function.</para></summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The high-order word indicates the distance the wheel is rotated, expressed in multiples or divisions of WHEEL_DELTA, which is 120. A positive value indicates that the wheel was rotated to the right; a negative value indicates that the wheel was rotated to the left. The low-order word indicates whether various virtual keys are down. This parameter can be one or more of the <see cref="wParam.WM_LBUTTONDBLCLK"/> values (but <see cref="wParam.WM_LBUTTONDBLCLK"/> are <see cref="Integer">Integers</see> while low-order word is <see cref="Short"/>).</description></item>
        ''' <item><term>lParam</term>
        ''' <description><para>The low-order word specifies the x-coordinate of the pointer, relative to the upper-left corner of the screen.</para>
        ''' <para>The high-order word specifies the y-coordinate of the pointer, relative to the upper-left corner of the screen.</para></description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list>
        ''' <para>This message is new in Windows Vista</para></remarks>
        WM_MOUSEHWHEEL = &H20E
#End Region
        ''' <summary>The WM_MOVE message is sent after a window has been moved.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies the x and y coordinates of the upper-left corner of the client area of the window. The low-order word contains the x-coordinate while the high-order word contains the y coordinate.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632631.aspx</seealso></remarks>
        WM_MOVE = &H3
        ''' <summary>The WM_MOVING message is sent to a window that the user is moving. By processing this message, an application can monitor the position of the drag rectangle and, if needed, change its position.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a RECT structure with the current position of the window, in screen coordinates. To change the position of the drag rectangle, an application must change the members of this structure.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return TRUE if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632632.aspx</seealso></remarks>
        WM_MOVING = &H216
#Region "WM_NC..."
        ''' <summary>The WM_NCACTIVATE message is sent to a window when its nonclient area needs to be changed to indicate an active or inactive state.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies when a title bar or icon needs to be changed to indicate an active or inactive state. If an active title bar or icon is to be drawn, the wParam parameter is TRUE. It is FALSE for an inactive title bar or icon.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>When the wParam parameter is FALSE, an application should return TRUE to indicate that the system should proceed with the default processing, or it should return FALSE to prevent the title bar or icon from being deactivated. When wParam is TRUE, the return value is ignored. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632633.aspx</seealso></remarks>
        WM_NCACTIVATE = &H86
        ''' <summary>The WM_NCCALCSIZE message is sent when the size and position of a window's client area must be calculated. By processing this message, an application can control the content of the window's client area when the size or position of the window changes.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>If wParam is TRUE, it specifies that the application should indicate which part of the client area contains valid information. The system copies the valid information to the specified area within the new client area.
        ''' If wParam is FALSE, the application does not need to indicate the valid part of the client area.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>If wParam is TRUE, lParam points to an NCCALCSIZE_PARAMS structure that contains information an application can use to calculate the new size and position of the client rectangle.
        ''' If wParam is FALSE, lParam points to a RECT structure. On entry, the structure contains the proposed window rectangle for the window. On exit, the structure should contain the screen coordinates of the corresponding window client area.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If the wParam parameter is FALSE, the application should return zero.
        ''' If wParam is TRUE, the application should return zero or a combination of the <see cref="ReturnValues.WM_NCCALCSIZE"/> values.
        ''' If wParam is TRUE and an application returns zero, the old client area is preserved and is aligned with the upper-left corner of the new client area.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632634.aspx</seealso></remarks>
        WM_NCCALCSIZE = &H83
        ''' <summary>The WM_NCCREATE message is sent prior to the WM_CREATE message when a window is first created.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to the CREATESTRUCT structure that contains information about the window being created. The members of CREATESTRUCT are identical to the parameters of the CreateWindowEx function.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return TRUE to continue creation of the window. If the application returns FALSE, the CreateWindow or CreateWindowEx function will return a NULL handle. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632635.aspx</seealso></remarks>
        WM_NCCREATE = &H81
        ''' <summary>The WM_NCDESTROY message informs a window that its nonclient area is being destroyed. The DestroyWindow function sends the WM_NCDESTROY message to the window following the WM_DESTROY message. WM_DESTROY is used to free the allocated memory object associated with the window.
        ''' The WM_NCDESTROY message is sent after the child windows have been destroyed. In contrast, WM_DESTROY is sent before the child windows are destroyed.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632636.aspx</seealso></remarks>
        WM_NCDESTROY = &H82
        ''' <summary>The WM_NCHITTEST message is sent to a window when the cursor moves, or when a mouse button is pressed or released. If the mouse is not captured, the message is sent to the window beneath the cursor. Otherwise, the message is sent to the window that has captured the mouse.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the screen.
        ''' The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the screen. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value of the DefWindowProc function is one of the <see cref="ReturnValues.WM_NCHITTEST"/> values, indicating the position of the cursor hot spot.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645618.aspx</seealso></remarks>
        WM_NCHITTEST = &H84
        ''' <summary>The WM_NCLBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the hit-test value returned by the DefWindowProc function as a result of processing the WM_NCHITTEST message. For a list of hit-test values, see WM_NCHITTEST.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies a POINTS structure that contains the x- and y-coordinates of the cursor. The coordinates are relative to the upper-left corner of the screen.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645619.aspx</seealso></remarks>
        WM_NCLBUTTONDBLCLK = &HA3
        ''' <summary>The WM_NCLBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the hit-test value returned by the DefWindowProc function as a result of processing the WM_NCHITTEST message. For a list of hit-test values, see WM_NCHITTEST.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies a POINTS structure that contains the x- and y-coordinates of the cursor. The coordinates are relative to the upper-left corner of the screen.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645620.aspx</seealso></remarks>
        WM_NCLBUTTONDOWN = &HA1
        ''' <summary>The WM_NCLBUTTONUP message is posted when the user releases the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the hit-test value returned by the DefWindowProc function as a result of processing the WM_NCHITTEST message. For a list of hit-test values, see WM_NCHITTEST.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies a POINTS structure that contains the x- and y-coordinates of the cursor. The coordinates are relative to the upper-left corner of the screen.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645621.aspx</seealso></remarks>
        WM_NCLBUTTONUP = &HA2
        ''' <summary>The WM_NCMBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the hit-test value returned by the DefWindowProc function as a result of processing the WM_NCHITTEST message. For a list of hit-test values, see WM_NCHITTEST.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies a POINTS structure that contains the x- and y-coordinates of the cursor. The coordinates are relative to the upper-left corner of the screen.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645622.aspx</seealso></remarks>
        WM_NCMBUTTONDBLCLK = &HA9
        ''' <summary>The WM_NCMBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the hit-test value returned by the DefWindowProc function as a result of processing the WM_NCHITTEST message. For a list of hit-test values, see WM_NCHITTEST.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies a POINTS structure that contains the x- and y-coordinates of the cursor. The coordinates are relative to the upper-left corner of the screen.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645623.aspx</seealso></remarks>
        WM_NCMBUTTONDOWN = &HA7
        ''' <summary>The WM_NCMBUTTONUP message is posted when the user releases the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the hit-test value returned by the DefWindowProc function as a result of processing the WM_NCHITTEST message. For a list of hit-test values, see WM_NCHITTEST.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies a POINTS structure that contains the x- and y-coordinates of the cursor. The coordinates are relative to the upper-left corner of the screen.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645624.aspx</seealso></remarks>
        WM_NCMBUTTONUP = &HA8
        ''' <summary>The WM_NCMOUSEMOVE message is posted to a window when the cursor is moved within the nonclient area of the window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the hit-test value returned by the DefWindowProc function as a result of processing the WM_NCHITTEST message. For a list of hit-test values, see WM_NCHITTEST.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies a POINTS structure that contains the x- and y-coordinates of the cursor. The coordinates are relative to the upper-left corner of the screen.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645627.aspx</seealso></remarks>
        WM_NCMOUSEMOVE = &HA0
        ''' <summary>The WM_NCPAINT message is sent to a window when its frame must be painted.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the update region of the window. The update region is clipped to the window frame.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application returns zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms534905.aspx</seealso></remarks>
        WM_NCPAINT = &H85
        ''' <summary>The WM_NCRBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the hit-test value returned by the DefWindowProc function as a result of processing the WM_NCHITTEST message. For a list of hit-test values, see WM_NCHITTEST.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies a POINTS structure that contains the x- and y-coordinates of the cursor. The coordinates are relative to the upper-left corner of the screen.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645628.aspx</seealso></remarks>
        WM_NCRBUTTONDBLCLK = &HA6
        ''' <summary>The WM_NCRBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the hit-test value returned by the DefWindowProc function as a result of processing the WM_NCHITTEST message. For a list of hit-test values, see WM_NCHITTEST.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies a POINTS structure that contains the x- and y-coordinates of the cursor. The coordinates are relative to the upper-left corner of the screen.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645629.aspx</seealso></remarks>
        WM_NCRBUTTONDOWN = &HA4
        ''' <summary>The WM_NCRBUTTONUP message is posted when the user releases the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the hit-test value returned by the DefWindowProc function as a result of processing the WM_NCHITTEST message. For a list of hit-test values, see WM_NCHITTEST.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies a POINTS structure that contains the x- and y-coordinates of the cursor. The coordinates are relative to the upper-left corner of the screen.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645630.aspx</seealso></remarks>
        WM_NCRBUTTONUP = &HA5
        ''' <summary>The WM_NCXBUTTONUP message is posted when the user releases the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description><para>The low-order word specifies the hit-test value returned by the DefWindowProc function from processing the WM_NCHITTEST message. For a list of hit-test values, see WM_NCHITTEST.</para>
        ''' <para>The high-order word indicates which button was released. It can be one of the <see cref="wParam.WM_PARENTNOTIFY_WM_XBUTTONDOWN_high"/> values.</para></description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a POINTS structure that contains the x- and y-coordinates of the cursor. The coordinates are relative to the upper-left corner of the screen.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return TRUE.</description></item>
        ''' </list></remarks>
        WM_NCXBUTTONUP = &HAC
        ''' <summary>The WM_NCXBUTTONDOWN message is posted when the user presses the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description><para>The low-order word specifies the hit-test value returned by the DefWindowProc function from processing the WM_NCHITTEST message. For a list of hit-test values, see WM_NCHITTEST.</para>
        ''' <para>The high-order word indicates which button was released. It can be one of the <see cref="wParam.WM_PARENTNOTIFY_WM_XBUTTONDOWN_high"/> values.</para></description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a POINTS structure that contains the x- and y-coordinates of the cursor. The coordinates are relative to the upper-left corner of the screen.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return TRUE.</description></item>
        ''' </list></remarks>
        WM_NCXBUTTONDOWN = &HAB
        ''' <summary>The WM_NCXBUTTONDBLCLK message is posted when the user double-clicks the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description><para>The low-order word specifies the hit-test value returned by the DefWindowProc function from processing the WM_NCHITTEST message. For a list of hit-test values, see WM_NCHITTEST.</para>
        ''' <para>The high-order word indicates which button was released. It can be one of the <see cref="wParam.WM_PARENTNOTIFY_WM_XBUTTONDOWN_high"/> values.</para></description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a POINTS structure that contains the x- and y-coordinates of the cursor. The coordinates are relative to the upper-left corner of the screen.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return TRUE.</description></item>
        ''' </list></remarks>
        WM_NCXBUTTONDBLCLK = &HAD
#End Region
        ''' <summary>The WM_NEXTDLGCTL message is sent to a dialog box procedure to set the keyboard focus to a different control in the dialog box.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>If lParam is TRUE, this parameter identifies the control that receives the focus. If lParam is FALSE, this parameter indicates whether the next or previous control with the WS_TABSTOP style receives the focus. If wParam is zero, the next control receives the focus; otherwise, the previous control with the WS_TABSTOP style receives the focus.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word indicates how the system uses wParam. If the low-order word is TRUE, wParam is a handle associated with the control that receives the focus; otherwise, wParam is a flag that indicates whether the next or previous control with the WS_TABSTOP style receives the focus.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645432.aspx</seealso></remarks>
        WM_NEXTDLGCTL = &H28
        ''' <summary>The WM_NEXTMENU message is sent to an application when the right or left arrow key is used to switch between the menu bar and the system menu.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the virtual-key code of the key.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a MDINEXTMENU structure that contains information about the menu to be activated.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>No return value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms647612.aspx</seealso></remarks>
        WM_NEXTMENU = &H213
        ''' <summary>Sent by a common control to its parent window when an event has occurred or the control requires some information.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The identifier of the common control sending the message. This identifier is not guaranteed to be unique. An application should use the hwndFrom or idFrom member of the NMHDR structure (passed as the lParam parameter) to identify the control.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>A pointer to an <see cref="Notifications.NMHDR"/> structure that contains the notification code and additional information. For some notification messages, this parameter points to a larger structure that has the <see cref="Notifications.NMHDR"/> structure as its first member.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value is ignored except for notification messages that specify otherwise.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb775583.aspx</seealso>
        ''' <remarks>You can use the <see cref="WindowsT.NativeT.Win32Window.SendNotification"/> function to send a notification without dealing with structure marshaling.</remarks></remarks>
        ''' <seelaso cref="WindowsT.NativeT.Win32Window.SendNotification"/>
        ''' <seelaso cref="Notifications.NMHDR"/><seelaso cref="Notifications.Notification"/>
        WM_NOTIFY = &H4E
        ''' <summary>Determines if a window accepts ANSI or Unicode structures in the WM_NOTIFY notification message. WM_NOTIFYFORMAT messages are sent from a common control to its parent window and from the parent window to the common control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>A handle to the window that is sending the WM_NOTIFYFORMAT message. If Command is NF_QUERY, this parameter is the handle to a control. If Command is NF_REQUERY, this parameter is the handle to the parent window of a control.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The command value that specifies the nature of the WM_NOTIFYFORMAT message. This will be one of the <see cref="lParam.WM_NOTIFYFORMAT"/> values.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns one of the <see cref="ReturnValues.WM_NOTIFYFORMAT"/> values.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb775584.aspx</seealso></remarks>
        WM_NOTIFYFORMAT = &H55
        ''' <summary>The WM_NULL message performs no operation. An application sends the WM_NULL message if it wants to post a message that the recipient window will ignore.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application returns zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632637.aspx</seealso></remarks>
        WM_NULL = &H0
        ''' <summary>The WM_PAINT message is sent when the system or another application makes a request to paint a portion of an application's window. The message is sent when the UpdateWindow or RedrawWindow function is called, or by the DispatchMessage function when the application obtains a WM_PAINT message by using the GetMessage or PeekMessage function.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application returns zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms534901.aspx</seealso></remarks>
        WM_PAINT = &HF
        ''' <summary>The WM_PAINTCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and the clipboard viewer's client area needs repainting.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the clipboard viewer window.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to a global memory object that contains a PAINTSTRUCT structure. The structure defines the part of the client area to paint.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms649027.aspx</seealso></remarks>
        WM_PAINTCLIPBOARD = &H309
        ''' <summary>Windows NT 3.51 and earlier: The WM_PAINTICON message is sent to a minimized window when the icon is to be painted. This message is not sent by newer versions of Microsoft Windows, except in unusual circumstances explained in the Remarks.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms648057.aspx</seealso></remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> WM_PAINTICON = &H26
        ''' <summary>The WM_PALETTECHANGED message is sent to all top-level and overlapped windows after the window with the keyboard focus has realized its logical palette, thereby changing the system palette. This message enables a window that uses a color palette but does not have the keyboard focus to realize its logical palette and update its client area.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the window that caused the system palette to change.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>N/A</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms532653.aspx</seealso></remarks>
        WM_PALETTECHANGED = &H311
        ''' <summary>The WM_PALETTEISCHANGING message informs applications that an application is going to realize its logical palette.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the window that is going to realize its logical palette.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms532632.aspx</seealso></remarks>
        WM_PALETTEISCHANGING = &H310
        ''' <summary>The WM_PARENTNOTIFY message is sent to the parent of a child window when the child window is created or destroyed, or when the user clicks a mouse button while the cursor is over the child window. When the child window is being created, the system sends WM_PARENTNOTIFY just before the CreateWindow or CreateWindowEx function that creates the window returns. When the child window is being destroyed, the system sends the message before any processing to destroy the window takes place.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The low-order word of wParam specifies the event for which the parent is being notified. This parameter can be one of the <see cref="wParam.WM_PARENTNOTIFY_low"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Meaning depends on value of low-order word of wParam</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632638.aspx</seealso></remarks>
        WM_PARENTNOTIFY = &H210
        ''' <summary>An application sends a WM_PASTE message to an edit control or combo box to copy the current content of the clipboard to the edit control at the current caret position. Data is inserted only if the clipboard contains data in CF_TEXT format.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>This message does not return a value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms649028.aspx</seealso></remarks>
        WM_PASTE = &H302
#Region "WM_PEN..."
        ''' <summary>The lowes value for pen messages.</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_PENWINFIRST = &H380
        ''' <summary>The highest value for pen messages.</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_PENWINLAST = &H38F
#End Region
        ''' <summary>Notifies applications that the system, typically a battery-powered personal computer, is about to enter a suspended mode.
        ''' Note  The WM_POWER message is obsolete. It is provided only for compatibility with 16-bit Windows-based applications. Applications should use the WM_POWERBROADCAST message.
        ''' A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The power-event notification. This parameter can be one of the <see cref="wParam.WM_POWER"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The value an application returns depends on the value of the wParam parameter. If wParam is <see cref="wParam.WM_POWER.PWR_SUSPENDREQUEST"/>, the return value is <see cref="ReturnValues.WM_POWER.PWR_FAIL"/> to prevent the system from entering the suspended state; otherwise, it is <see cref="ReturnValues.WM_POWER.PWR_OK"/>. If wParam is <see cref="wParam.WM_POWER.PWR_SUSPENDRESUME"/> or <see cref="wParam.WM_POWER.PWR_CRITICALRESUME"/>, the return value is zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/aa373245.aspx</seealso></remarks>
        WM_POWER = &H48
        ''' <summary>Notifies applications that a power-management event has occurred.
        ''' A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The power-management event. This parameter can be one of the <see cref="wParam.WM_POWERBROADCAST"/> event identifiers.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>    The event-specific data. For most events, this parameter is reserved and not used.
        ''' If the wParam parameter is <see cref="wParam.WM_POWERBROADCAST.PBT_POWERSETTINGCHANGE"/>, the lParam parameter is a pointer to a POWERBROADCAST_SETTING structure.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return TRUE if it processes this message.
        ''' Windows Server 2003, Windows XP, and Windows 2000:  An application can return <see cref="ReturnValues.WM_POWERBROADCAST.BROADCAST_QUERY_DENY"/> to deny a <see cref="ReturnValues.WM_POWERBROADCAST.PBT_APMQUERYSUSPEND"/> or <see cref="ReturnValues.WM_POWERBROADCAST.PBT_APMQUERYSUSPENDFAILED"/> request.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/aa373247.aspx</seealso></remarks>
        WM_POWERBROADCAST = &H218
        ''' <summary>The WM_PRINT message is sent to a window to request that it draw itself in the specified device context, most commonly in a printer device context.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the device context to draw in.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies the drawing options. This parameter can be one or more of the <see cref="lParam.WM_PRINT"/> values.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>N/A</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms534856.aspx</seealso></remarks>
        WM_PRINT = &H317
        ''' <summary>The WM_PRINTCLIENT message is sent to a window to request that it draw its client area in the specified device context, most commonly in a printer device context.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the device context to draw in.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies drawing options. This parameter can be one or more of the <see cref="lParam.WM_PRINT"/> values.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>N/A</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms534913.aspx</seealso></remarks>
        WM_PRINTCLIENT = &H318
        ''' <summary>The WM_QUERYDRAGICON message is sent to a minimized (iconic) window. The window is about to be dragged by the user but does not have an icon defined for its class. An application can return a handle to an icon or cursor. The system displays this cursor or icon while the user drags the icon.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return a handle to a cursor or icon that the system is to display while the user drags the icon. The cursor or icon must be compatible with the display driver's resolution. If the application returns NULL, the system displays the default cursor.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632639.aspx</seealso></remarks>
        WM_QUERYDRAGICON = &H37
        ''' <summary>The WM_QUERYENDSESSION message is sent when the user chooses to end the session or when an application calls one of the system shutdown functions. If any application returns zero, the session is not ended. The system stops sending WM_QUERYENDSESSION messages as soon as one application returns zero.
        ''' After processing this message, the system sends the WM_ENDSESSION message with the wParam parameter set to the results of the WM_QUERYENDSESSION message.
        ''' A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is reserved for future use.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter can be one or more of the <seealso cref="lParam.WM_QUERYENDSESSION"/> values. If this parameter is 0, the system is shutting down or restarting (it is not possible to determine which event is occurring).</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Applications should respect the user's intentions and return TRUE. By default, the DefWindowProc function returns TRUE for this message.
        ''' If shutting down would corrupt the system or media that is being burned, the application can return FALSE. However, it is good practice to respect the user's actions.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/aa376890.aspx</seealso></remarks>
        WM_QUERYENDSESSION = &H11
        ''' <summary>The WM_QUERYNEWPALETTE message informs a window that it is about to receive the keyboard focus, giving the window the opportunity to realize its logical palette when it receives the focus.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This message has no parameters.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This message has no parameters.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If the window realizes its logical palette, it must return TRUE; otherwise, it must return FALSE.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms532654.aspx</seealso></remarks>
        WM_QUERYNEWPALETTE = &H30F
        ''' <summary>The WM_QUERYOPEN message is sent to an icon when the user requests that the window be restored to its previous size and position.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If the icon can be opened, an application that processes this message should return TRUE; otherwise, it should return FALSE to prevent the icon from being opened.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632640.aspx</seealso></remarks>
        WM_QUERYOPEN = &H13
        ''' <summary>The WM_QUEUESYNC message is sent by a computer-based training (CBT) application to separate user-input messages from other messages sent through the WH_JOURNALPLAYBACK Hook procedure.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>N/A</description></item>
        ''' <item><term>lParam</term>
        ''' <description>N/A</description></item>
        ''' <item><term>Return value</term>
        ''' <description>A CBT application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms644972.aspx</seealso></remarks>
        WM_QUEUESYNC = &H23
        ''' <summary>The WM_QUIT message indicates a request to terminate an application and is generated when the application calls the PostQuitMessage function. It causes the GetMessage function to return zero.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the exit code given in the PostQuitMessage function.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>This message does not have a return value because it causes the message loop to terminate before the message is sent to the application's window procedure.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632641.aspx</seealso></remarks>
        WM_QUIT = &H12
        ''' <summary>The WM_RENDERALLFORMATS message is sent to the clipboard owner before it is destroyed, if the clipboard owner has delayed rendering one or more clipboard formats. For the content of the clipboard to remain available to other applications, the clipboard owner must render data in all the formats it is capable of generating, and place the data on the clipboard by calling the SetClipboardData function.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms649029.aspx</seealso></remarks>
        WM_RENDERALLFORMATS = &H306
        ''' <summary>The WM_RENDERFORMAT message is sent to the clipboard owner if it has delayed rendering a specific clipboard format and if an application has requested data in that format. The clipboard owner must render data in the specified format and place it on the clipboard by calling the SetClipboardData function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the clipboard format to be rendered.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms649030.aspx</seealso></remarks>
        WM_RENDERFORMAT = &H305
        ''' <summary>The WM_SETCURSOR message is sent to a window if the mouse causes the cursor to move within a window and mouse input is not captured.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the window that contains the cursor.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word of lParam specifies the hit-test code.
        ''' The high-order word of lParam specifies the identifier of the mouse message. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return TRUE to halt further processing or FALSE to continue.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms648382.aspx</seealso></remarks>
        WM_SETCURSOR = &H20
        ''' <summary>The WM_SETFOCUS message is sent to a window after it has gained the keyboard focus.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the window that has lost the keyboard focus. This parameter can be NULL.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646283.aspx</seealso></remarks>
        WM_SETFOCUS = &H7
        ''' <summary>An application sends a WM_SETFONT message to specify the font that a control is to use when drawing text.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the font (HFONT). If this parameter is NULL, the control uses the default system font to draw text.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word of lParam specifies whether the control should be redrawn immediately upon setting the font. If this parameter is TRUE, the control redraws itself.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>This message does not return a value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632642.aspx</seealso></remarks>
        WM_SETFONT = &H30
        ''' <summary>An application sends a WM_SETHOTKEY message to a window to associate a hot key with the window. When the user presses the hot key, the system activates the window.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The low-order word specifies the virtual-key code to associate with the window.
        ''' The high-order word can be one or more of the <see cref="ReturnValues.WM_GETHOTKEY_high"/> values.
        ''' Setting wParam to NULL removes the hot key associated with a window.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value is one of the <see cref="ReturnValues.WM_SETHOTKEY"/>.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646284.aspx</seealso></remarks>
        WM_SETHOTKEY = &H32
        ''' <summary>An application sends the WM_SETICON message to associate a new large or small icon with a window. The system displays the large icon in the ALT+TAB dialog box, and the small icon in the window caption.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the type of icon to be set. This parameter can be one of the following values: <see cref="wParam.WM_GETICON.ICON_BIG"/>, <see cref="wParam.WM_GETICON.ICON_SMALL"/>.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the new large or small icon. If this parameter is NULL, the icon indicated by wParamis removed.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value is a handle to the previous large or small icon, depending on the value of wParam. It is NULL if the window previously had no icon of the type indicated by wParam.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632643.aspx</seealso></remarks>
        WM_SETICON = &H80
        ''' <summary>An application sends the WM_SETREDRAW message to a window to allow changes in that window to be redrawn or to prevent changes in that window from being redrawn.
        ''' To send this message, call the SendMessage function with the following parameters. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the redraw state. If this parameter is TRUE, the content can be redrawn after a change. If this parameter is FALSE, the content cannot be redrawn after a change.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application returns zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms534853.aspx</seealso></remarks>
        WM_SETREDRAW = &HB
        ''' <summary>An application sends a WM_SETTEXT message to set the text of a window.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a null-terminated string that is the window text.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value is TRUE if the text is set. It is FALSE (for an edit control), LB_ERRSPACE (for a list box), or CB_ERRSPACE (for a combo box) if insufficient space is available to set the text in the edit control. It is CB_ERR if this message is sent to a combo box without an edit control.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632644.aspx</seealso></remarks>
        WM_SETTEXT = &HC
        ''' <summary>A message that is sent to all top-level windows when the SystemParametersInfo function changes a system-wide setting or when policy settings have changed.
        ''' Applications should send WM_SETTINGCHANGE to all top-level windows when they make changes to system parameters. (This message cannot be sent directly to a window.) To send the WM_SETTINGCHANGE message to all top-level windows, use the SendMessageTimeout function with the hwnd parameter set to HWND_BROADCAST.
        ''' A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>When the system sends this message as a result of a SystemParametersInfo call, wParam is a flag that indicates the system parameter that was changed. For a list of values, see SystemParametersInfo.
        ''' When the system sends this message as a result of a change in policy settings, this parameter indicates the type of policy that was applied. This value is 1 if computer policy was applied or zero if user policy was applied.
        ''' When the system sends this message as a result of a change in locale settings, this parameter is zero.
        ''' When an application sends this message, this parameter must be NULL.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>When the system sends this message as a result of a SystemParametersInfo call, lParam is a pointer to a string that indicates the area containing the system parameter that was changed. This parameter does not usually indicate which specific system parameter changed. (Note that some applications send this message with lParam set to NULL.) In general, when you receive this message, you should check and reload any system parameter settings that are used by your application.
        ''' This string can be the name of a registry key or the name of a section in the Win.ini file. When the string is a registry name, it typically indicates only the leaf node in the registry, not the full path.
        ''' When the system sends this message as a result of a change in policy settings, this parameter points to the string "Policy".
        ''' When the system sends this message as a result of a change in locale settings, this parameter points to the string "intl".
        ''' To effect a change in the environment variables for the system or the user, broadcast this message with lParam set to the string "Environment".
        ''' </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If you process this message, return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms725497.aspx</seealso></remarks>
        WM_SETTINGCHANGE = &H1A
        ''' <summary>The WM_SHOWWINDOW message is sent to a window when the window is about to be hidden or shown.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies whether a window is being shown. If wParam is TRUE, the window is being shown. If wParam is FALSE, the window is being hidden.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies the status of the window being shown. If lParam is zero, the message was sent because of a call to the ShowWindow function; otherwise, lParam is one of the <see cref="lParam.WM_SHOWWINDOW"/> values.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632645.aspx</seealso></remarks>
        WM_SHOWWINDOW = &H18
        ''' <summary>The WM_SIZE message is sent to a window after its size has changed.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the type of resizing requested. This parameter can be one of the <see cref="wParam.WM_SIZE"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word of lParam specifies the new width of the client area.
        ''' The high-order word of lParam specifies the new height of the client area. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632646.aspx</seealso></remarks>
        WM_SIZE = &H5
        ''' <summary>The WM_SIZECLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and the clipboard viewer's client area has changed size.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the clipboard viewer window.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>    Handle to a global memory object that contains a RECT structure. The structure specifies the new dimensions of the clipboard viewer's client area.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms649031.aspx</seealso></remarks>
        WM_SIZECLIPBOARD = &H30B
        ''' <summary>The WM_SIZING message is sent to a window that the user is resizing. By processing this message, an application can monitor the size and position of the drag rectangle and, if needed, change its size or position.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies which edge of the window is being sized. This parameter can be one of the <see cref="wParam.WM_SIZING"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a RECT structure with the screen coordinates of the drag rectangle. To change the size or position of the drag rectangle, an application must change the members of this structure.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return TRUE if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632647.aspx</seealso></remarks>
        WM_SIZING = &H214
        ''' <summary>The WM_SPOOLERSTATUS message is sent from Print Manager whenever a job is added to or removed from the Print Manager queue.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the PR_JOBSTATUS flag.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the number of jobs remaining in the Print Manager queue.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms535643.aspx</seealso></remarks>
        WM_SPOOLERSTATUS = &H2A
        ''' <summary>The WM_STYLECHANGED message is sent to a window after the SetWindowLong function has changed one or more of the window's styles.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies whether the window's styles or extended window styles have changed. This parameter can be one or more of the <see cref="wParam.WM_STYLECHANGED"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a STYLESTRUCT structure that contains the new styles for the window. An application can examine the styles, but cannot change them.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632648.aspx</seealso></remarks>
        WM_STYLECHANGED = &H7D
        ''' <summary>The WM_STYLECHANGING message is sent to a window when the SetWindowLong function is about to change one or more of the window's styles.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies whether the window's styles or extended window styles are changing. This parameter can be one or more of the <see cref="wParam.WM_STYLECHANGED"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a STYLESTRUCT structure that contains the proposed new styles for the window. An application can examine the styles and, if necessary, change them.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632649.aspx</seealso></remarks>
        WM_STYLECHANGING = &H7C
        ''' <summary>The WM_SYNCPAINT message is used to synchronize painting while avoiding linking independent GUI threads.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This message has no parameters.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This message has no parameters.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application returns zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms534855.aspx</seealso></remarks>
        WM_SYNCPAINT = &H88
        ''' <summary>The <see cref="WM_SYSCOLORCHANGE"/> message is sent to all top-level windows when a change is made to a system color setting.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This message has no parameters.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This message has no parameters.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>N/A</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms532603.aspx</seealso></remarks>
        WM_SYSCOLORCHANGE = &H15
        ''' <summary>A window receives this message when the user chooses a command from the Window menu (formerly known as the system or control menu) or when the user chooses the maximize button, minimize button, restore button, or close button.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the type of system command requested. This parameter can be one of the <see cref="wParam.WM_SYSCOMMAND"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word specifies the horizontal position of the cursor, in screen coordinates, if a window menu command is chosen with the mouse. Otherwise, this parameter is not used.
        ''' The high-order word specifies the vertical position of the cursor, in screen coordinates, if a window menu command is chosen with the mouse. This parameter is –1 if the command is chosen using a system accelerator, or zero if using a mnemonic. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646360.aspx</seealso></remarks>
        WM_SYSCOMMAND = &H112
        ''' <summary>The WM_SYSDEADCHAR message is sent to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. WM_SYSDEADCHAR specifies the character code of a system dead key — that is, a dead key that is pressed while holding down the ALT key.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Specifies the character code generated by the system dead key — that is, a dead key that is pressed while holding down the ALT key.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Specifies the repeat count, scan code, extended-key flag, context code, previous key-state flag, and transition-state flag, as shown in the <see cref="lParam.WM_CHAR"/>.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646285.aspx</seealso></remarks>
        WM_SYSDEADCHAR = &H107
        ''' <summary>Sent to an application that has initiated a training card with Microsoft Windows Help. The message informs the application when the user clicks an authorable button. An application initiates a training card by specifying the HELP_TCARD command in a call to the WinHelp function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>A value that indicates the action the user has taken. This can be one of the <see cref="wParam.WM_TCARD"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>If idAction specifies HELP_TCARD_DATA, this parameter is a long specified by the Help author. Otherwise, this parameter is zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value is ignored; use zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb774307.aspx</seealso></remarks>
        WM_TCARD = &H52
        ''' <summary>A message that is sent whenever there is a change in the system time.
        ''' A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms725498.aspx</seealso></remarks>
        WM_TIMECHANGE = &H1E
        ''' <summary>The WM_TIMER message is posted to the installing thread's message queue when a timer expires. The message is posted by the GetMessage or PeekMessage function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>[in] Specifies the timer identifier.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>[in] Pointer to an application-defined callback function that was passed to the SetTimer function when the timer was installed.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms644902.aspx</seealso></remarks>
        WM_TIMER = &H113
        ''' <summary>The WM_THEMECHANGED message is broadcast to every window following a theme change event. Examples of theme change events are the activation of a theme, the deactivation of a theme, or a transition from one theme to another.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Reserved. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>Reserved. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/ms632650.aspx</seealso></remarks>
        WM_THEMECHANGE = &H31A
        ''' <summary>An application sends a WM_UNDO message to an edit control to undo the last operation. When this message is sent to an edit control, the previously deleted text is restored or the previously added text is deleted.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used; must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If the message succeeds, the return value is TRUE.
        ''' If the message fails, the return value is FALSE.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb761693.aspx</seealso></remarks>
        WM_UNDO = &H304
        ''' <summary>The WM_UNINITMENUPOPUP message is sent when a drop-down menu or submenu has been destroyed.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the menu</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The high-order word identifies the menu that was destroyed. Currently, it can only be MF_SYSMENU (the window menu).</description></item>
        ''' <item><term>Return value</term>
        ''' <description>No return value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms647614.aspx</seealso></remarks>
        WM_UNINITMENUPOPUP = &H125
        ''' <summary>This message is used by applications to help define private messages.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>None.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>None.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>None.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/aa928069.aspx</seealso></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_USER = &H400
        ''' <summary>The WM_USERCHANGED message is sent to all windows after the user has logged on or off. When the user logs on or off, the system updates the user-specific settings. The system sends this message immediately after updating the settings.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return zero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632651.aspx</seealso></remarks>
        WM_USERCHANGED = &H54
        ''' <summary>Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_KEYDOWN message.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The low-order word specifies the virtual-key code of the key the user pressed. The high-order word specifies the current position of the caret.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the list box.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value specifies the action that the application performed in response to the message. A return value of –2 indicates that the application handled all aspects of selecting the item and requires no further action by the list box. (See Remarks.) A return value of –1 indicates that the list box should perform the default action in response to the keystroke. A return value of 0 or greater specifies the index of an item in the list box and indicates that the list box should perform the default action for the keystroke on the specified item.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb761364.aspx</seealso></remarks>
        WM_VKEYTOITEM = &H2E
        ''' <summary>The WM_VSCROLL message is sent to a window when a scroll event occurs in the window's standard vertical scroll bar. This message is also sent to the owner of a vertical scroll bar control when a scroll event occurs in the control.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The high-order word specifies the current position of the scroll box if the low-order word is SB_THUMBPOSITION or SB_THUMBTRACK; otherwise, this word is not used.
        ''' The low-order word specifies a scroll bar value that indicates the user's scrolling request. This parameter can be one of the <see cref="wParam.WM_HSCROLL_low"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>If the message is sent by a scroll bar, this parameter is the handle to the scroll bar control. If the message is not sent by a scroll bar, this parameter is NULL.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/bb787577.aspx</seealso></remarks>
        WM_VSCROLL = &H115
        ''' <summary>The WM_VSCROLLCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and an event occurs in the clipboard viewer's vertical scroll bar. The owner should scroll the clipboard image and update the scroll bar values.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the clipboard viewer window.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The low-order word of lParam specifies a scroll bar event. This parameter can be one of the <see cref="wParam.WM_HSCROLL_low"/> values. The high-order word of lParam specifies the current position of the scroll box if the low-order word of lParam is SB_THUMBPOSITION; otherwise, the high-order word of lParam is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms649032.aspx</seealso></remarks>
        WM_VSCROLLCLIPBOARD = &H30A
        ''' <summary>The WM_WINDOWPOSCHANGED message is sent to a window whose size, position, or place in the Z order has changed as a result of a call to the SetWindowPos function or another window-management function.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a WINDOWPOS structure that contains information about the window's new size and position.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632652.aspx</seealso></remarks>
        WM_WINDOWPOSCHANGED = &H47
        ''' <summary>The WM_WINDOWPOSCHANGING message is sent to a window whose size, position, or place in the Z order is about to change as a result of a call to the SetWindowPos function or another window-management function.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a WINDOWPOS structure that contains information about the window's new size and position.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms632653.aspx</seealso></remarks>
        WM_WINDOWPOSCHANGING = &H46
        ''' <summary>An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.
        ''' Note  The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.
        ''' A window receives this message through its WindowProc function.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>A pointer to a string containing the name of the system parameter that was changed. For example, this string can be the name of a registry key or the name of a section in the Win.ini file. This parameter is not particularly useful in determining which system parameter changed. For example, when the string is a registry name, it typically indicates only the leaf node in the registry, not the whole path. In addition, some applications send this message with lParam set to NULL. In general, when you receive this message, you should check and reload any system parameter settings that are used by your application.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>If you process this message, return zero.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms725499.aspx</seealso></remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> WM_WININICHANGE = &H1A
#End Region
#Region "Ranges"
        ''' <summary>Messages in range <see cref="X0000"/> through <see cref="WM_USER_minus_1"/> are reserved for use by the system.</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> X0000 = &H0
        ''' <summary>Messages in range <see cref="X0000"/> through <see cref="WM_USER_minus_1"/> are reserved for use by the system.</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_USER_minus_1 = WM_USER - 1
        ''' <summary>Messages in range <see cref="WM_USER"/> through <see cref="X7FFF"/> are integer messages for use by private window classes.</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> X7FFF = &H7FFF
        ''' <summary>Messages in range <see cref="WM_APP"/> through <see cref="XBFFF"/> are available for use by applications.</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> XBFFF = &HBFFF
        ''' <summary>Messages in range <see cref="XC000"/> through <see cref="XFFFF"/> are string messages for use by applications</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> XC000 = &HC000
        ''' <summary><para>Messages in range <see cref="XC000"/> through <see cref="XFFFF"/> are string messages for use by applications</para>
        ''' <para>Messages greater than <see cref="XFFFF"/> are reserved by the system</para></summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> XFFFF = &HFFF
#End Region
#Region "TCM_..."
        ''' <summary>Value of first Tab control message</summary>
        ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)> TCM_FIRST = &H1300
        ''' <summary>Calculates a tab control's display area given a window rectangle, or calculates the window rectangle that would correspond to a specified display area.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Operation to perform. If this parameter is TRUE, lParam specifies a display rectangle and receives the corresponding window rectangle. If this parameter is FALSE, lParam specifies a window rectangle and receives the corresponding display area.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a RECT  structure that specifies the given rectangle and receives the calculated rectangle. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>No return value.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760573.aspx</seealso>
        ''' This message applies only to tab controls that are at the top. It does not apply to tab controls that are on the sides or bottom.</remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_ADJUSTRECT = TCM_FIRST + 40
        ''' <summary>Removes all items from a tab control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns TRUE if successful, or FALSE otherwise.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760575.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_DELETEALLITEMS = TCM_FIRST + 9
        ''' <summary>Removes an item from a tab control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Index of the item to delete.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns TRUE if successful, or FALSE otherwise.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760577.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_DELETEITEM = TCM_FIRST + 8
        ''' <summary>Resets items in a tab control, clearing any that were set to the TCIS_BUTTONPRESSED state.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Flag that specifies the scope of the item deselection. If this parameter is set to FALSE, all tab items will be reset. If it is set to TRUE, then all tab items except for the one currently selected will be reset.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value for this message is not used.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760579.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_DESELECTALL = TCM_FIRST + 50
        ''' <summary>Returns the index of the item that has the focus in a tab control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the index of the tab item that has the focus.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760581.aspx</seealso>
        ''' The item that has the focus may be different than the selected item. </remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_GETCURFOCUS = TCM_FIRST + 47
        ''' <summary>Determines the currently selected tab in a tab control. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the index of the selected tab if successful, or -1 if no tab is selected.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/ms644927.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_GETCURSEL = TCM_FIRST + 11
        ''' <summary>Retrieves the extended styles that are currently in use for the tab control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns a DWORD value that represents the extended styles currently in use for the tab control. This value is a combination of tab control extended styles.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760585.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_GETEXTENDEDSTYLE = TCM_FIRST + 53
        ''' <summary>Retrieves the image list associated with a tab control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the handle to the image list if successful, or NULL otherwise. </description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760588.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_GETIMAGELIST = TCM_FIRST + 2
        ''' <summary>Retrieves information about a tab in a tab control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Index of the tab. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a TCITEM  structure that specifies the information to retrieve and receives information about the tab. When the message is sent, the mask member specifies which attributes to return. If the mask member specifies the TCIF_TEXT value, the pszText member must contain the address of the buffer that receives the item text, and the cchTextMax member must specify the size of the buffer.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns TRUE if successful, or FALSE otherwise.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760589.aspx</seealso>
        ''' This constant defines TCM_GETITEMW message (Unicode version), for ansi version see <see cref="TCM_GETITEMA"/></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_GETITEM = TCM_FIRST + 60
        ''' <summary>Defines ANSI version of then <see cref="TCM_GETITEM"/> message.</summary>
        ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)> TCM_GETITEMA = TCM_FIRST + 5
        ''' <summary>Retrieves the number of tabs in the tab control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the number of items if successful, or zero otherwise.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760592.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_GETITEMCOUNT = TCM_FIRST + 4
        ''' <summary>Retrieves the bounding rectangle for a tab in a tab control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Index of the tab. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>A lParam parameter of the message</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Pointer to a RECT  structure that receives the bounding rectangle of the tab, in viewport coordinates. </description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760594.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_GETITEMRECT = TCM_FIRST + 10
        ''' <summary>Retrieves the current number of rows of tabs in a tab control. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the number of rows of tabs.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb7605969.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_GETROWCOUNT = TCM_FIRST + 44
        ''' <summary>Retrieves the handle to the tooltip control associated with a tab control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the handle to the tooltip control if successful, or NULL otherwise. </description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760598.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_GETTOOLTIPS = TCM_FIRST + 45
        ''' <summary>Retrieves the Unicode character format flag for the control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the Unicode format flag for the control. If this value is nonzero, the control is using Unicode characters. If this value is zero, the control is using ANSI characters.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760600.aspx</seealso></remarks>
        ''' <seelaso cref="CCM_GETUNICODEFORMAT"/>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT
        ''' <summary>Sets the highlight state of a tab item.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>An INT value that specifies the zero-based index of a tab control item. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>The LOWORD  is a BOOL specifying the highlight state to be set. If this value is TRUE, the tab is highlighted; if FALSE, the tab is set to its default state. The HIWORD  must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns nonzero if successful, or zero otherwise.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760602.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_HIGHLIGHTITEM = TCM_FIRST + 51
        ''' <summary>Determines which tab, if any, is at a specified screen position.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a TCHITTESTINFO  structure that specifies the screen position to test. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the index of the tab, or -1 if no tab is at the specified position. </description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760604.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_HITTEST = TCM_FIRST + 13
        ''' <summary>Inserts a new tab in a tab control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Index of the new tab.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a TCITEM  structure that specifies the attributes of the tab. The dwState  and dwStateMask members of this structure are ignored by this message.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the index of the new tab if successful, or -1 otherwise.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760606.aspx</seealso>
        ''' This constant defines Unicode version of the message (TCM_INSERTITEMW). For ANSI version see <see cref="TCM_INSERTITEMA"/>.</remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_INSERTITEM = TCM_FIRST + 62
        ''' <summary>Defines ANSI version of the <see cref="TCM_INSERTITEM"/> message.</summary>
        ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)> TCM_INSERTITEMA = TCM_FIRST + 7
        ''' <summary>Removes an image from a tab control's image list.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Index of the image to remove. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>No return value.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760608.aspx</seealso>
        ''' The tab control updates each tab's image index, so each tab remains associated with the same image as before. If a tab is using the image being removed, the tab will be set to have no image. </remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_REMOVEIMAGE = TCM_FIRST + 42
        ''' <summary>Sets the focus to a specified tab in a tab control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Index of the tab that gets the focus. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>No return value.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760610.aspx</seealso>
        ''' If the tab control has the TCS_BUTTONS style (button mode), the tab with the focus may be different from the selected tab. For example, when a tab is selected, the user can press the arrow keys to set the focus to a different tab without changing the selected tab. In button mode, <see cref="TCM_SETCURFOCUS"/> sets the input focus to the button associated with the specified tab, but it does not change the selected tab.
        ''' <para>If the tab control does not have the TCS_BUTTONS style, changing the focus also changes the selected tab. In this case, the tab control sends the TCN_SELCHANGING and TCN_SELCHANGE notification codes to its parent window.</para></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_SETCURFOCUS = TCM_FIRST + 48
        ''' <summary>Selects a tab in a tab control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>    Index of the tab to select.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the index of the previously selected tab if successful, or -1 otherwise.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760612.aspx</seealso>
        ''' A tab control does not send a TCN_SELCHANGING  or TCN_SELCHANGE  notification code when a tab is selected using this message. </remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_SETCURSEL = TCM_FIRST + 12
        ''' <summary>Sets the extended styles that the tab control will use.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>A DWORD value that indicates which styles in lParam are to be affected. Only the extended styles in wParam  will be changed. All other styles will be maintained as they are. If this parameter is zero, then all of the styles in lParam will be affected. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>Value specifying the extended tab control styles. This value is a combination of tab control extended styles.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns a DWORD value that contains the previous tab control extended styles.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760627.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_SETEXTENDEDSTYLE = TCM_FIRST + 52
        ''' <summary>Assigns an image list to a tab control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the image list to assign to the tab control. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the handle to the previous image list, or NULL if there is no previous image list.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760629.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_SETIMAGELIST = TCM_FIRST + 3
        ''' <summary>Sets some or all of a tab's attributes.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Index of the item. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a TCITEM  structure that contains the new item attributes. The mask  member specifies which attributes to set. If the mask member specifies the TCIF_TEXT value, the pszText member is the address of a null-terminated string and the cchTextMax  member is ignored.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns TRUE if successful, or FALSE otherwise.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760631.aspx</seealso>
        ''' This constant defines Unicode version of the message (TCM_SETITEMW). For ANSI version see <see cref="TCM_SETITEMA"/>.</remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_SETITEM = TCM_FIRST + 61
        ''' <summary>Defines ANSI version of then <see cref="TCM_SETITEM"/> message.</summary>
        ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)> TCM_SETITEMA = TCM_FIRST + 6
        ''' <summary>Sets the number of bytes per tab reserved for application-defined data in a tab control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Number of extra bytes.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns TRUE if successful, or FALSE otherwise.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760633.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_SETITEMEXTRA = TCM_FIRST + 14
        ''' <summary>Sets the width and height of tabs in a fixed-width or owner-drawn tab control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>    The LOWORD  is an INT value that specifies the new width, in pixels. The HIWORD  is an INT value that specifies the new height, in pixels.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the old width and height. The width is in the LOWORD  of the return value, and the height is in the HIWORD.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760635.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_SETITEMSIZE = TCM_FIRST + 41
        ''' <summary>Sets the minimum width of items in a tab control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Minimum width to be set for a tab control item. If this parameter is set to -1, the control will use the default tab width. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns an INT value that represents the previous minimum tab width.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760637.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_SETMINTABWIDTH = TCM_FIRST + 49
        ''' <summary>Sets the amount of space (padding) around each tab's icon and label in a tab control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>N/A</description></item>
        ''' <item><term>lParam</term>
        ''' <description>The LOWORD  is an INT value that specifies the amount of horizontal padding, in pixels. The HIWORD  is an INT value that specifies the amount of vertical padding, in pixels.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>No return value.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760639.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_SETPADDING = TCM_FIRST + 43
        ''' <summary>Assigns a tooltip control to a tab control</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the tooltip control. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>    Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>You can retrieve the tooltip control associated with a tab control by using the <see cref="TCM_GETTOOLTIPS"/> message.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760641.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_SETTOOLTIPS = TCM_FIRST + 46
        ''' <summary>Sets the Unicode character format flag for the control. This message allows you to change the character set used by the control at run time rather than having to re-create the control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Determines the character set that is used by the control. If this value is nonzero, the control will use Unicode characters. If this value is zero, the control will use ANSI characters. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the previous Unicode format flag for the control. </description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760643.aspx</seealso></remarks>    
        ''' <seelaso cref="CCM_SETUNICODEFORMAT"/>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        TCM_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT
#End Region
#Region "CCM_..."
        ''' <summary>Defines a value of first common control message</summary>
        ''' <version version="1.5.3">This constant is new in version 1.5.3</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)> CCM_FIRST = &H2000
        ''' <summary>Sets the background color of the control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>COLORREF  value that contains the new background color. If this value is -1, the control will revert to using the system color for the background color. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns a COLORREF  value that represents the previous background color. If this value is -1, the control was using the system color for the background color.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb773741.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        CCM_SETBKCOLOR = CCM_FIRST + &H1
        ''' <summary>Sets the color scheme information for the control. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a COLORSCHEME  structure that contains the color scheme information.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value for this message is not used.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb787421.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        CCM_SETCOLORSCHEME = CCM_FIRST + &H2
        ''' <summary>Retrieves the color scheme information from the control. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to a COLORSCHEME  structure that will receive the color scheme information. You must set the cbSize member of this structure to sizeof(COLORSCHEME) before sending this message. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns nonzero if successful, or zero otherwise.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb787327.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        CCM_GETCOLORSCHEME = CCM_FIRST + &H3
        ''' <summary>Retrieves a pager control's IDropTarget  interface pointer</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Pointer to an IDropTarget  pointer that receives the interface pointer. It is the caller's responsibility to call Release on this pointer when it is no longer needed. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value for this message is not used. </description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760872.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        CCM_GETDROPTARGET = CCM_FIRST + &H4
        ''' <summary>Sets the Unicode character format flag for the control. This message allows you to change the character set used by the control at run time rather than having to re-create the control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Determines the character set that is used by the control. If this value is nonzero, the control will use Unicode characters. If this value is zero, the control will use ANSI characters. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the previous Unicode format flag for the control. </description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760643.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        CCM_SETUNICODEFORMAT = CCM_FIRST + &H5
        ''' <summary>Retrieves the Unicode character format flag for the control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the Unicode format flag for the control. If this value is nonzero, the control is using Unicode characters. If this value is zero, the control is using ANSI characters.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb760600.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        CCM_GETUNICODEFORMAT = CCM_FIRST + &H6
        ''' <summary>This message is used to inform the control that you are expecting a behavior associated with a particular version.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>The version number. </description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the version specified in the previous <see cref="CCM_SETVERSION"/>  message. If wParam is set to a value greater than the current DLL version, it returns -1.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb775581.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        CCM_SETVERSION = CCM_FIRST + &H7
        ''' <summary>Gets the version number for a control set by the most recent <see cref="CCM_SETVERSION"/>  message.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>Returns the version number set by the most recent <see cref="CCM_SETVERSION"/>  message. If no such message has been sent, it returns zero.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb775579.aspx</seealso></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        CCM_GETVERSION = CCM_FIRST + &H8
        ''' <summary>(?) Registers a window that will handle messages in response to all events from an object. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>N/A (window handle to send message to?)</description></item>
        ''' <item><term>lParam</term>
        ''' <description>N/A (pointer to message being send?)</description></item>
        ''' <item><term>Return value</term>
        ''' <description>N/A</description></item>
        ''' </list></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        CCM_SETNOTIFYWINDOW = CCM_FIRST + &H9
        ''' <summary>Sets the visual style of a control.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>A pointer to a Unicode string that contains the control visual style to set.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value is not used.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb775582.aspx</seealso>
        ''' <note>To use this message, you must provide a manifest specifying Comclt32.dll version 6.0. For more information on manifests, see Enabling Visual Styles.</note></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        CCM_SETWINDOWTHEME = CCM_FIRST + &HB
        ''' <summary>Enables automatic high dots per inch (dpi) scaling in Tree-View controls, List-View controls, ComboBoxEx controls, Header controls, Buttons, Toolbar controls, Animation controls, and Image Lists. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Set to TRUE.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Must be zero.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>The return value is not used.</description></item>
        ''' </list><seealso>http://msdn.microsoft.com/en-us/library/bb775574.aspx</seealso>Quick Launch and Taskbar  should not specify a dpi scaling, because the images are already scaled.
        ''' <para>Any control that uses an image list created with the SmallIcon metric should not scale its icons.</para>
        ''' <note>To use this API, you must provide a manifest that specifies Comclt32.dll version 6.0. For more information on manifests, see Enabling Visual Styles.</note></remarks>
        ''' <version version="1.5.3">This message is new in version 1.5.3</version>
        CCM_DPISCALE = CCM_FIRST + &HC
#End Region
    End Enum
End Namespace
#End If