Imports System.ComponentModel
#If Config <= Nightly Then 'Stage: Nightly
Namespace API.Messages
    ''' <summary>The WindowProc function is an application-defined function that processes messages sent to a window. The WNDPROC type defines a pointer to this callback function. WindowProc is a placeholder for the application-defined function name.</summary>
    ''' <param name="hWnd">Handle to the window.</param>
    ''' <param name="msg">Specifies the message.</param>
    ''' <param name="wParam">Specifies additional message information. The contents of this parameter depend on the value of the uMsg parameter.</param>
    ''' <param name="lParam">Specifies additional message information. The contents of this parameter depend on the value of the uMsg parameter.</param>
    ''' <returns>The return value is the result of the message processing and depends on the message sent.</returns>
    Public Delegate Function WndProc(ByVal hWnd As Integer, ByVal msg As Messages.WindowMessages, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    ''' <summary>Window messages</summary>
    Public Enum WindowMessages As Integer
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
        ''' <summary>N/A</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> WM_AFXFIRST = &H360
        ''' <summary>N/A</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> WM_AFXLAST = &H37F
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
        ''' <summary>N/A</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> WM_HANDHELDFIRST = &H358
        ''' <summary>N/A</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> WM_HANDHELDLAST = &H35F
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
        ''' <description>The low-order word specifies the keys that were to be pressed in combination with the key specified by the high-order word to generate the WM_HOTKEY message. This word can be one or more of the <see cref="lParam.WM_HOTEKY_low"/> values. The high-order word specifies the virtual key code of the hot key.</description></item>
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
        ''' <summary>Windows NT 3.51 and earlier: The WM_ICONERASEBKGND message is sent to a minimized window when the background of the icon must be filled before painting the icon. A window receives this message only if a class icon is defined for the window; otherwise, WM_ERASEBKGND is sent. This message is not sent by newer versions of Windows.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Handle to the device context of the icon.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>This parameter is not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application should return nonzero if it processes this message.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms648056.aspx</seealso></remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> WM_ICONERASEBKGND = &H27
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
        ''' <summary>N/A</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> WM_IME_KEYLAST = &H10F
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
        ''' <summary>The WM_INPUTLANGCHANGE message is sent to the topmost affected window after an application's input language has been changed. You should make any application-specific settings and pass the message to the DefWindowProc function, which passes the message to all first-level child windows. These child windows can pass the message to DefWindowProc to have it pass the message to their child windows, and so on.
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
        ''' <summary>This message filters for keyboard messages.</summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Not used.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Not used.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>None.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/aa931746.aspx</seealso></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> WM_KEYFIRST = &H100
        ''' <summary>This message filters for keyboard messages.</summary>
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
        ''' <summary>The WM_LBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
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
        ''' <summary>The WM_LBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        ''' A window receives this message through its WindowProc function. </summary>
        ''' <remarks><list type="table">
        ''' <item><term>wParam</term>
        ''' <description>Indicates whether various virtual keys are down. This parameter can be one or more of the <see cref="wParam.WM_LBUTTONDBLCLK"/> values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>he low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.
        ''' The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area. </description></item>
        ''' <item><term>Return value</term>
        ''' <description>If an application processes this message, it should return zero. </description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms645607.aspx</seealso></remarks>
        WM_LBUTTONDOWN = &H201
        ''' <summary>The WM_LBUTTONUP message is posted when the user releases the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
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
        ''' <summary>The WM_MBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
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
        ''' <summary>The WM_MBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
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
        ''' <summary>The WM_MBUTTONUP message is posted when the user releases the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
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
        ''' <summary>An application sends the WM_MDIACTIVATE message to a multiple-document interface (MDI) client window to instruct the client window to activate a different MDI child window.</summary>
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
        ''' <summary>An application sends the WM_MDIICONARRANGE message to a multiple document interface (MDI) client window to arrange all minimized MDI child windows. It does not affect child windows that are not minimized.</summary>
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
        ''' <description>If this parameter is zero, the system activates the next MDI child window and places the child window identified by the wParam parameter behind all other child windows. If this parameter is nonzero, the system activates the previous child window, placing it in front of the child window identified by wParam.</description></item>
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
        ''' The high-order word specifies the active menu type. This parameter can be one of the <see cref="wParam.WM_MENUCHAR_high" values.</description></item>
        ''' <item><term>lParam</term>
        ''' <description>Handle to the active menu.</description></item>
        ''' <item><term>Return value</term>
        ''' <description>An application that processes this message should return one of the <see cref="ReturnValues.WM_MENUCHAR"/> values in the high-order word of the return value.</description></item>
        ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms646349.aspx</seealso></remarks>
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
        WM_MOUSEFIRST = &H200
        WM_MOUSEHOVER = &H2A1
        WM_MOUSELAST = &H20D
        WM_MOUSELEAVE = &H2A3
        WM_MOUSEMOVE = &H200
        WM_MOUSEWHEEL = &H20A
        WM_MOVE = &H3
        WM_MOVING = &H216
        WM_NCACTIVATE = &H86
        WM_NCCALCSIZE = &H83
        WM_NCCREATE = &H81
        WM_NCDESTROY = &H82
        WM_NCHITTEST = &H84
        WM_NCLBUTTONDBLCLK = &HA3
        WM_NCLBUTTONDOWN = &HA1
        WM_NCLBUTTONUP = &HA2
        WM_NCMBUTTONDBLCLK = &HA9
        WM_NCMBUTTONDOWN = &HA7
        WM_NCMBUTTONUP = &HA8
        WM_NCMOUSEMOVE = &HA0
        WM_NCPAINT = &H85
        WM_NCRBUTTONDBLCLK = &HA6
        WM_NCRBUTTONDOWN = &HA4
        WM_NCRBUTTONUP = &HA5
        WM_NEXTDLGCTL = &H28
        WM_NEXTMENU = &H213
        WM_NOTIFY = &H4E
        WM_NOTIFYFORMAT = &H55
        WM_NULL = &H0
        WM_PAINT = &HF
        WM_PAINTCLIPBOARD = &H309
        WM_PAINTICON = &H26
        WM_PALETTECHANGED = &H311
        WM_PALETTEISCHANGING = &H310
        WM_PARENTNOTIFY = &H210
        WM_PASTE = &H302
        WM_PENWINFIRST = &H380
        WM_PENWINLAST = &H38F
        WM_POWER = &H48
        WM_POWERBROADCAST = &H218
        WM_PRINT = &H317
        WM_PRINTCLIENT = &H318
        WM_QUERYDRAGICON = &H37
        WM_QUERYENDSESSION = &H11
        WM_QUERYNEWPALETTE = &H30F
        WM_QUERYOPEN = &H13
        WM_QUEUESYNC = &H23
        WM_QUIT = &H12
        WM_RBUTTONDBLCLK = &H206
        WM_RBUTTONDOWN = &H204
        WM_RBUTTONUP = &H205
        WM_RENDERALLFORMATS = &H306
        WM_RENDERFORMAT = &H305
        WM_SETCURSOR = &H20
        WM_SETFOCUS = &H7
        WM_SETFONT = &H30
        WM_SETHOTKEY = &H32
        WM_SETICON = &H80
        WM_SETREDRAW = &HB
        WM_SETTEXT = &HC
        WM_SETTINGCHANGE = &H1A
        WM_SHOWWINDOW = &H18
        WM_SIZE = &H5
        WM_SIZECLIPBOARD = &H30B
        WM_SIZING = &H214
        WM_SPOOLERSTATUS = &H2A
        WM_STYLECHANGED = &H7D
        WM_STYLECHANGING = &H7C
        WM_SYNCPAINT = &H88
        WM_SYSCHAR = &H106
        WM_SYSCOLORCHANGE = &H15
        WM_SYSCOMMAND = &H112
        WM_SYSDEADCHAR = &H107
        WM_SYSKEYDOWN = &H104
        WM_SYSKEYUP = &H105
        WM_TCARD = &H52
        WM_TIMECHANGE = &H1E
        WM_TIMER = &H113
        WM_UNDO = &H304
        WM_UNINITMENUPOPUP = &H125
        WM_USER = &H400
        WM_USERCHANGED = &H54
        WM_VKEYTOITEM = &H2E
        WM_VSCROLL = &H115
        WM_VSCROLLCLIPBOARD = &H30A
        WM_WINDOWPOSCHANGED = &H47
        WM_WINDOWPOSCHANGING = &H46
        WM_WININICHANGE = &H1A
        WM_XBUTTONDBLCLK = &H20D
        WM_XBUTTONDOWN = &H20B
        WM_XBUTTONUP = &H20C
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
    End Enum

    ''' <summary>These values are used with ImmGetCompositionString and WM_IME_COMPOSITION.</summary>
    ''' <remarks><seealso>http://msdn2.microsoft.com/en-us/library/ms776087.aspx</seealso></remarks>
    Friend Enum IMECompositionStringValues As Integer
        ''' <summary>Retrieves or updates the attribute of the composition string.</summary>
        GCS_COMPATTR = &H10
        ''' <summary>Retrieves or updates clause information of the composition string.</summary>
        GCS_COMPCLAUSE = &H20
        ''' <summary>Retrieves or updates the attributes of the reading string of the current composition.</summary>
        GCS_COMPREADATTR = &H2
        ''' <summary> 	Retrieves or updates the clause information of the reading string of the composition string.</summary>
        GCS_COMPREADCLAUSE = &H4
        ''' <summary> 	Retrieves or updates the reading string of the current composition.</summary>
        GCS_COMPREADSTR = &H1
        ''' <summary> 	Retrieves or updates the current composition string.</summary>
        GCS_COMPSTR = &H8
        ''' <summary> 	Retrieves or updates the cursor position in composition string.</summary>
        GCS_CURSORPOS = &H80
        ''' <summary>Retrieves or updates clause information of the result string.</summary>
        GCS_DELTASTART = &H100
        ''' <summary>Retrieves or updates clause information of the result string.</summary>
        GCS_RESULTCLAUSE = &H1000
        ''' <summary> 	Retrieves or updates the reading string.</summary>
        GCS_RESULTREADCLAUSE = &H400
        ''' <summary> 	Retrieves or updates the reading string.</summary>
        GCS_RESULTREADSTR = &H200
        ''' <summary>Retrieves or updates the string of the composition result.</summary>
        GCS_RESULTSTR = &H800
    End Enum

    Namespace wParam
        ''' <summary>Values used for wParam used by <see cref="WindowMessages.WM_ACTIVATE"/> message</summary>
        Public Enum WM_ACTIVATE As Integer
            ''' <summary>Activated by some method other than a mouse click (for example, by a call to the SetActiveWindow function or by use of the keyboard interface to select the window).</summary>
            WA_ACTIVE = 1
            ''' <summary>Activated by a mouse click.</summary>
            WA_CLICKACTIVE = 2
            ''' <summary>Deactivated.</summary>
            WA_INACTIVE = 0
        End Enum
        ''' <summary>Values for wParam used by <see cref="WindowMessages.WM_CHANGEUISTATE"/>. Those constants are used for low-order word of wParam.</summary>
        Public Enum WM_CHANGEUISTATE_low As Short
            ''' <summary>The UI state flags specified by the high-order word should be cleared.</summary>
            UIS_CLEAR = 2
            ''' <summary>The UI state flags specified by the high-order word should be changed based on the last input event. For more information, see Remarks.</summary>
            UIS_INITIALIZE = 3
            ''' <summary>The UI state flags specified by the high-order word should be set.</summary>
            UIS_SET = 1
        End Enum
        ''' <summary>Values for wParam used by <see cref="WindowMessages.WM_CHANGEUISTATE"/>. Those constants are used for high-order word of wParam.</summary>
        Public Enum WM_CHANGEUISTATE_high As Short
            ''' <summary>Keyboard accelerators are hidden.</summary>
            UISF_HIDEACCEL = &H2
            ''' <summary>Focus indicators are hidden.</summary>
            UISF_HIDEFOCUS = &H1
            ''' <summary>Windows XP: A control should be drawn in the style used for active controls.</summary>
            UISF_ACTIVE = 4
        End Enum
        ''' <summary>Values for wParam used by <see cref="WindowMessages.WM_CHANGEUISTATE"/>.</summary>
        ''' <remarks>Actual value of wParam can be or-ed UIS_* and UISF_* constant</remarks>
        Public Enum WM_CHANGEUISTATE As Integer
            ''' <summary>The UI state flags specified by the high-order word should be cleared.</summary>
            UIS_CLEAR = WM_CHANGEUISTATE_low.UIS_CLEAR
            ''' <summary>The UI state flags specified by the high-order word should be changed based on the last input event. For more information, see Remarks.</summary>
            UIS_INITIALIZE = WM_CHANGEUISTATE_low.UIS_INITIALIZE
            ''' <summary>The UI state flags specified by the high-order word should be set.</summary>
            UIS_SET = WM_CHANGEUISTATE_low.UIS_SET
            ''' <summary>''' <summary>Keyboard accelerators are hidden.</summary></summary>
            UISF_HIDEACCEL = CInt(WM_CHANGEUISTATE_high.UISF_HIDEACCEL) << 16
            ''' <summary>Focus indicators are hidden.</summary>
            UISF_HIDEFOCUS = CInt(WM_CHANGEUISTATE_high.UISF_HIDEFOCUS) << 16
            ''' <summary>Windows XP: A control should be drawn in the style used for active controls.</summary>
            UISF_ACTIVE = CInt(WM_CHANGEUISTATE_high.UISF_ACTIVE) << 16
        End Enum
        ''' <summary>Constants used for low word of wParam of <see cref="WindowMessages.WM_COMMAND"/> message</summary>
        ''' <remarks>Sorry, I haven't found any documentation for those constants</remarks>
        Public Enum WM_COMMAND_low As Short
            'TODO: Docummentation
            IDM_1D = 2170
            IDM_ADDFAVORITES = 2261
            IDM_ADDRESS = 2189
            IDM_ADDTOGLYPHTABLE = 2337
            IDM_ALIGNBOTTOM = 1
            IDM_ALIGNHORIZONTALCENTERS = 2
            IDM_ALIGNLEFT = 3
            IDM_ALIGNRIGHT = 4
            IDM_ALIGNTOGRID = 5
            IDM_ALIGNTOP = 6
            IDM_ALIGNVERTICALCENTERS = 7
            IDM_APPLYHEADING1 = 2255
            IDM_APPLYHEADING2 = 2256
            IDM_APPLYHEADING3 = 2257
            IDM_APPLYNORMAL = 2254
            IDM_ARRANGEBOTTOM = 8
            IDM_ARRANGERIGHT = 9
            IDM_AUTODETECT = 2329
            IDM_BACKCOLOR = 51
            IDM_BASELINEFONT1 = 2141
            IDM_BASELINEFONT2 = 2142
            IDM_BASELINEFONT3 = 2143
            IDM_BASELINEFONT4 = 2144
            IDM_BASELINEFONT5 = 2145
            IDM_BLINK = 2190
            IDM_BLOCKDIRLTR = 2352
            IDM_BLOCKDIRRTL = 2353
            IDM_BLOCKFMT = 2234
            IDM_BOLD = 52
            IDM_BOOKMARK = 2123
            IDM_BORDERCOLOR = 53
            IDM_BREAKATNEXT = 2311
            IDM_BRINGFORWARD = 10
            IDM_BRINGTOFRONT = 11
            IDM_BROWSEMODE = 2126
            IDM_BUTTON = 2167
            IDM_CANCEL = 89
            IDM_CAPTIONINSERT = 2203
            IDM_CELLINSERT = 2202
            IDM_CELLMERGE = 2204
            IDM_CELLPROPERTIES = 2211
            IDM_CELLSELECT = 2206
            IDM_CELLSPLIT = 2205
            IDM_CENTERALIGNPARA = 2250
            IDM_CENTERHORIZONTALLY = 12
            IDM_CENTERVERTICALLY = 13
            IDM_CHANGECASE = 2246
            IDM_CHANGEFONT = 2240
            IDM_CHANGEFONTSIZE = 2241
            IDM_CHECKBOX = 2163
            IDM_CHISELED = 64
            IDM_CLEARSELECTION = 2007
            IDM_CODE = 14
            IDM_COLUMNINSERT = 2213
            IDM_COLUMNSELECT = 2208
            IDM_COMMENT = 2173
            IDM_COMPOSESETTINGS = 2318
            IDM_CONTEXT = 1
            IDM_CONTEXTMENU = 2280
            IDM_CONVERTOBJECT = 82
            IDM_COPY = 15
            IDM_COPYBACKGROUND = 2265
            IDM_COPYCONTENT = 2291
            IDM_COPYFORMAT = 2237
            IDM_COPYSHORTCUT = 2262
            IDM_CREATELINK = 2290
            IDM_CREATESHORTCUT = 2266
            IDM_CUSTOMCONTROL = 83
            IDM_CUSTOMIZEITEM = 84
            IDM_CUT = 16
            IDM_DECFONTSIZE = 2243
            IDM_DECFONTSIZE1PT = 2245
            IDM_DELETE = 17
            IDM_DELETEWORD = 92
            IDM_DIRLTR = 2350
            IDM_DIRRTL = 2351
            IDM_DIV = 2191
            IDM_DOCPROPERTIES = 2260
            IDM_DROPDOWNBOX = 2165
            IDM_DYNSRCPLAY = 2271
            IDM_DYNSRCSTOP = 2272
            IDM_EDITMODE = 2127
            IDM_EDITSOURCE = 2122
            IDM_EMPTYGLYPHTABLE = 2336
            IDM_ENABLE_INTERACTION = 2302
            IDM_ETCHED = 65
            IDM_EXECPRINT = 93
            IDM_FILE = 2172
            IDM_FIND = 67
            IDM_FLAT = 54
            IDM_FOLLOW_ANCHOR = 2008
            IDM_FOLLOWLINKC = 2136
            IDM_FOLLOWLINKN = 2137
            IDM_FONT = 90
            IDM_FONTNAME = 18
            IDM_FONTSIZE = 19
            IDM_FORECOLOR = 55
            IDM_FORM = 2181
            IDM_FORMATMARK = 2132
            IDM_GETBLOCKFMTS = 2233
            IDM_GETBYTESDOWNLOADED = 2331
            IDM_GETZOOM = 68
            IDM_GETZOOMDENOMINATOR = 2346
            IDM_GETZOOMNUMERATOR = 2345
            IDM_GOBACKWARD = 2282
            IDM_GOFORWARD = 2283
            IDM_GOTO = 2239
            IDM_GROUP = 20
            IDM_HELP_ABOUT = 2221
            IDM_HELP_CONTENT = 2220
            IDM_HELP_README = 2222
            IDM_HORIZONTALLINE = 2150
            IDM_HORIZSPACECONCATENATE = 21
            IDM_HORIZSPACEDECREASE = 22
            IDM_HORIZSPACEINCREASE = 23
            IDM_HORIZSPACEMAKEEQUAL = 24
            IDM_HTMLAREA = 2178
            IDM_HTMLCONTAIN = 2159
            IDM_HTMLEDITMODE = 2316
            IDM_HTMLSOURCE = 2157
            IDM_HWND = 2
            IDM_HYPERLINK = 2124
            IDM_IFRAME = 2158
            IDM_IMAGE = 2168
            IDM_IMAGEMAP = 2171
            IDM_IMGARTPLAY = 2274
            IDM_IMGARTREWIND = 2276
            IDM_IMGARTSTOP = 2275
            IDM_IMPORT = 86
            IDM_INCFONTSIZE = 2242
            IDM_INCFONTSIZE1PT = 2244
            IDM_INDENT = 2186
            IDM_INLINEDIRLTR = 2354
            IDM_INLINEDIRRTL = 2355
            IDM_INSERTOBJECT = 25
            IDM_INSERTSPAN = 2357
            IDM_INSFIELDSET = 2119
            IDM_INSINPUTBUTTON = 2115
            IDM_INSINPUTHIDDEN = 2312
            IDM_INSINPUTIMAGE = 2114
            IDM_INSINPUTPASSWORD = 2313
            IDM_INSINPUTRESET = 2116
            IDM_INSINPUTSUBMIT = 2117
            IDM_INSINPUTUPLOAD = 2118
            IDM_ISTRUSTEDDLG = 2356
            IDM_ITALIC = 56
            IDM_JAVAAPPLET = 2175
            IDM_JUSTIFYCENTER = 57
            IDM_JUSTIFYFULL = 50
            IDM_JUSTIFYGENERAL = 58
            IDM_JUSTIFYLEFT = 59
            IDM_JUSTIFYNONE = 94
            IDM_JUSTIFYRIGHT = 60
            IDM_LANGUAGE = 2292
            IDM_LAUNCHDEBUGGER = 2310
            IDM_LEFTALIGNPARA = 2251
            IDM_LINEBREAKBOTH = 2154
            IDM_LINEBREAKLEFT = 2152
            IDM_LINEBREAKNORMAL = 2151
            IDM_LINEBREAKRIGHT = 2153
            IDM_LIST = 2183
            IDM_LISTBOX = 2166
            IDM_LOCALIZEEDITOR = 2358
            IDM_MARQUEE = 2182
            IDM_MENUEXT_COUNT = 3733
            IDM_MENUEXT_FIRST__ = 3700
            IDM_MENUEXT_LAST__ = 3732
            IDM_MIMECSET__FIRST__ = 3609
            IDM_MIMECSET__LAST__ = 3699
            IDM_MOVE = 88
            IDM_MULTILEVELREDO = 30
            IDM_MULTILEVELUNDO = 44
            IDM_NEW = 2001
            IDM_NEWPAGE = 87
            IDM_NOACTIVATEDESIGNTIMECONTROLS = 2333
            IDM_NOACTIVATEJAVAAPPLETS = 2334
            IDM_NOACTIVATENORMALOLECONTROLS = 2332
            IDM_NOFIXUPURLSONPASTE = 2335
            IDM_NONBREAK = 2155
            IDM_OBJECT = 2169
            IDM_OBJECTVERBLIST0 = 72
            IDM_OBJECTVERBLIST1 = 73
            IDM_OBJECTVERBLIST2 = 74
            IDM_OBJECTVERBLIST3 = 75
            IDM_OBJECTVERBLIST4 = 76
            IDM_OBJECTVERBLIST5 = 77
            IDM_OBJECTVERBLIST6 = 78
            IDM_OBJECTVERBLIST7 = 79
            IDM_OBJECTVERBLIST8 = 80
            IDM_OBJECTVERBLIST9 = 81
            IDM_OBJECTVERBLISTLAST = IDM_OBJECTVERBLIST9
            IDM_OPEN = 2000
            IDM_OPTIONS = 2135
            IDM_ORDERLIST = 2184
            IDM_OUTDENT = 2187
            IDM_OVERWRITE = 2314
            IDM_PAGE = 2267
            IDM_PAGEBREAK = 2177
            IDM_PAGEINFO = 2231
            IDM_PAGESETUP = 2004
            IDM_PARAGRAPH = 2180
            IDM_PARSECOMPLETE = 2315
            IDM_PASTE = 26
            IDM_PASTEFORMAT = 2238
            IDM_PASTEINSERT = 2120
            IDM_PASTESPECIAL = 2006
            IDM_PERSISTSTREAMSYNC = 2341
            IDM_PLUGIN = 2176
            IDM_PREFORMATTED = 2188
            IDM_PRESTOP = 2284
            IDM_PRINT = 27
            IDM_PRINTPREVIEW = 2003
            IDM_PRINTQUERYJOBSPENDING = 2277
            IDM_PRINTTARGET = 2273
            IDM_PROPERTIES = 28
            IDM_RADIOBUTTON = 2164
            IDM_RAISED = 61
            IDM_RCINSERT = 2201
            IDM_REDO = 29
            IDM_REFRESH = 2300
            IDM_REGISTRYREFRESH = 2317
            IDM_REMOVEFORMAT = 2230
            IDM_REMOVEFROMGLYPHTABLE = 2338
            IDM_REMOVEPARAFORMAT = 2253
            IDM_RENAME = 85
            IDM_REPLACE = 2121
            IDM_REPLACEGLYPHCONTENTS = 2339
            IDM_RIGHTALIGNPARA = 2252
            IDM_ROWINSERT = 2212
            IDM_ROWSELECT = 2207
            IDM_RUNURLSCRIPT = 2343
            IDM_SAVE = 70
            IDM_SAVEAS = 71
            IDM_SAVEBACKGROUND = 2263
            IDM_SAVECOPYAS = 2002
            IDM_SAVEPICTURE = 2270
            IDM_SAVEPRETRANSFORMSOURCE = 2370
            IDM_SAVETARGET = 2268
            IDM_SCRIPT = 2174
            IDM_SCRIPTDEBUGGER = 2330
            IDM_SELECTALL = 31
            IDM_SENDBACKWARD = 32
            IDM_SENDTOBACK = 33
            IDM_SETDESKTOPITEM = 2278
            IDM_SETDIRTY = 2342
            IDM_SETWALLPAPER = 2264
            IDM_SHADOWED = 66
            IDM_SHOWALIGNEDSITETAGS = 2321
            IDM_SHOWALLTAGS = 2327
            IDM_SHOWAREATAGS = 2325
            IDM_SHOWCOMMENTTAGS = 2324
            IDM_SHOWGRID = 69
            IDM_SHOWHIDE_CODE = 2235
            IDM_SHOWMISCTAGS = 2320
            IDM_SHOWPICTURE = 2269
            IDM_SHOWSCRIPTTAGS = 2322
            IDM_SHOWSPECIALCHAR = 2249
            IDM_SHOWSTYLETAGS = 2323
            IDM_SHOWTABLE = 34
            IDM_SHOWUNKNOWNTAGS = 2326
            IDM_SHOWWBRTAGS = 2340
            IDM_SHOWZEROBORDERATDESIGNTIME = 2328
            IDM_SIZETOCONTROL = 35
            IDM_SIZETOCONTROLHEIGHT = 36
            IDM_SIZETOCONTROLWIDTH = 37
            IDM_SIZETOFIT = 38
            IDM_SIZETOGRID = 39
            IDM_SNAPTOGRID = 40
            IDM_SPECIALCHAR = 2156
            IDM_SPELL = 2005
            IDM_STATUSBAR = 2131
            IDM_STOP = 2138
            IDM_STOPDOWNLOAD = 2301
            IDM_STRIKETHROUGH = 91
            IDM_SUBSCRIPT = 2247
            IDM_SUNKEN = 62
            IDM_SUPERSCRIPT = 2248
            IDM_TABLE = 2236
            IDM_TABLEINSERT = 2200
            IDM_TABLEPROPERTIES = 2210
            IDM_TABLESELECT = 2209
            IDM_TABORDER = 41
            IDM_TELETYPE = 2232
            IDM_TEXTAREA = 2162
            IDM_TEXTBOX = 2161
            IDM_TEXTONLY = 2133
            IDM_TOOLBARS = 2130
            IDM_TOOLBOX = 42
            IDM_TRIED_ABSOLUTE_DROP_MODE = 13
            IDM_TRIED_ACTIVATEACTIVEXCONTROLS = 23
            IDM_TRIED_ACTIVATEAPPLETS = 24
            IDM_TRIED_ACTIVATEDTCS = 25
            IDM_TRIED_BACKCOLOR = 26
            IDM_TRIED_BLOCKFMT = 27
            IDM_TRIED_BOLD = 28
            IDM_TRIED_BRING_ABOVE_TEXT = 11
            IDM_TRIED_BRING_FORWARD = 9
            IDM_TRIED_BRING_TO_FRONT = 7
            IDM_TRIED_BROWSEMODE = 29
            IDM_TRIED_CONSTRAIN = 12
            IDM_TRIED_COPY = 30
            IDM_TRIED_CUT = 31
            IDM_TRIED_DELETE = 32
            IDM_TRIED_DELETECELLS = 21
            IDM_TRIED_DELETECOLS = 17
            IDM_TRIED_DELETEROWS = 16
            IDM_TRIED_DOVERB = 61
            IDM_TRIED_EDITMODE = 33
            IDM_TRIED_FIND = 34
            IDM_TRIED_FONT = 35
            IDM_TRIED_FONTNAME = 36
            IDM_TRIED_FONTSIZE = 37
            IDM_TRIED_FORECOLOR = 38
            IDM_TRIED_GETBLOCKFMTS = 39
            IDM_TRIED_HYPERLINK = 40
            IDM_TRIED_IMAGE = 41
            IDM_TRIED_INDENT = 42
            IDM_TRIED_INSERTCELL = 20
            IDM_TRIED_INSERTCOL = 15
            IDM_TRIED_INSERTROW = 14
            IDM_TRIED_INSERTTABLE = 22
            IDM_TRIED_IS_1D_ELEMENT = 0
            IDM_TRIED_IS_2D_ELEMENT = 1
            IDM_TRIED_ITALIC = 43
            IDM_TRIED_JUSTIFYCENTER = 44
            IDM_TRIED_JUSTIFYLEFT = 45
            IDM_TRIED_JUSTIFYRIGHT = 46
            IDM_TRIED_LAST_CID = IDM_TRIED_DOVERB
            IDM_TRIED_LOCK_ELEMENT = 5
            IDM_TRIED_MAKE_ABSOLUTE = 4
            IDM_TRIED_MERGECELLS = 18
            IDM_TRIED_NUDGE_ELEMENT = 2
            IDM_TRIED_ORDERLIST = 47
            IDM_TRIED_OUTDENT = 48
            IDM_TRIED_PASTE = 50
            IDM_TRIED_PRINT = 51
            IDM_TRIED_REDO = 52
            IDM_TRIED_REMOVEFORMAT = 53
            IDM_TRIED_SELECTALL = 54
            IDM_TRIED_SEND_BACKWARD = 8
            IDM_TRIED_SEND_BEHIND_1D = IDM_TRIED_SEND_BELOW_TEXT
            IDM_TRIED_SEND_BELOW_TEXT = 10
            IDM_TRIED_SEND_FORWARD = IDM_TRIED_BRING_FORWARD
            IDM_TRIED_SEND_FRONT_1D = IDM_TRIED_BRING_ABOVE_TEXT
            IDM_TRIED_SEND_TO_BACK = 6
            IDM_TRIED_SEND_TO_FRONT = IDM_TRIED_BRING_TO_FRONT
            IDM_TRIED_SET_2D_DROP_MODE = IDM_TRIED_ABSOLUTE_DROP_MODE
            IDM_TRIED_SET_ALIGNMENT = 3
            IDM_TRIED_SHOWBORDERS = 55
            IDM_TRIED_SHOWDETAILS = 56
            IDM_TRIED_SPLITCELL = 19
            IDM_TRIED_UNDERLINE = 57
            IDM_TRIED_UNDO = 58
            IDM_TRIED_UNLINK = 59
            IDM_TRIED_UNORDERLIST = 60
            IDM_UNBOOKMARK = 2128
            IDM_UNDERLINE = 63
            IDM_UNDO = 43
            IDM_UNGROUP = 45
            IDM_UNKNOWN = 0
            IDM_UNLINK = 2125
            IDM_UNORDERLIST = 2185
            IDM_VERTSPACECONCATENATE = 46
            IDM_VERTSPACEDECREASE = 47
            IDM_VERTSPACEINCREASE = 48
            IDM_VERTSPACEMAKEEQUAL = 49
            IDM_VIEWPRETRANSFORMSOURCE = 2371
            IDM_VIEWSOURCE = 2139
            IDM_ZOOMPERCENT = 50
            IDM_ZOOMPOPUP = 2140
            IDM_ZOOMRATIO = 2344
        End Enum
        ''' <summary>Constants used for wParam of <see cref="WindowMessages.WM_DEVICECHANGE"/> message</summary>
        Public Enum WM_DEVICECHANGE As Integer
            ''' <summary>A request to change the current configuration (dock or undock) has been canceled.</summary>
            DBT_CONFIGCHANGECANCELED = &H19
            ''' <summary>The current configuration has changed, due to a dock or undock.</summary>
            DBT_CONFIGCHANGED = &H18
            ''' <summary>A custom event has occurred.</summary>
            DBT_CUSTOMEVENT = &H8006
            ''' <summary>A device or piece of media has been inserted and is now available.</summary>
            DBT_DEVICEARRIVAL = &H8000
            ''' <summary>Permission is requested to remove a device or piece of media. Any application can deny this request and cancel the removal.</summary>
            DBT_DEVICEQUERYREMOVE = &H8001
            ''' <summary>A request to remove a device or piece of media has been canceled.</summary>
            DBT_DEVICEQUERYREMOVEFAILED = &H8002
            ''' <summary>A device or piece of media has been removed.</summary>
            DBT_DEVICEREMOVECOMPLETE = &H8004
            ''' <summary>A device or piece of media is about to be removed. Cannot be denied.</summary>
            DBT_DEVICEREMOVEPENDING = &H8003
            ''' <summary>A device-specific event has occurred.</summary>
            DBT_DEVICETYPESPECIFIC = &H8005
            ''' <summary>A device has been added to or removed from the system.</summary>
            DBT_DEVNODES_CHANGED = &H7
            ''' <summary>Permission is requested to change the current configuration (dock or undock).</summary>
            DBT_QUERYCHANGECONFIG = &H17
            ''' <summary>The meaning of this message is user-defined.</summary>
            DBT_USERDEFINED = &HFFFF
        End Enum
        ''' <summary>Values used by wParam of <see cref="WindowMessages.WM_ENTERIDLE"/> message</summary>
        Public Enum WM_ENTERIDLE As Integer
            ''' <summary>The system is idle because a dialog box is displayed.</summary>
            MSGF_DIALOGBOX = 0
            ''' <summary>The system is idle because a menu is displayed.</summary>
            MSGF_MENU = 2
        End Enum
        ''' <summary>Values used for wParam of the <see cref="WindowMessages.WM_GETICON"/> message</summary>
        Public Enum WM_GETICON As Integer
            ''' <summary>Retrieve the large icon for the window.</summary>
            ICON_BIG = 1
            ''' <summary>Retrieve the small icon for the window.</summary>
            ICON_SMALL = 0
            ''' <summary>Windows XP: Retrieves the small icon provided by the application. If the application does not provide one, the system uses the system-generated icon for that window.</summary>
            ICON_SMALL2 = 2
        End Enum
        ''' <summary>Values used for wParam of <see cref="WindowMessages.WM_HOTKEY"/> message</summary>
        Public Enum WM_HOTKEY As Integer
            ''' <summary>The "snap desktop" hot key was pressed.</summary>
            IDHOT_SNAPDESKTOP = (-2)
            ''' <summary>The "snap window" hot key was pressed.</summary>
            IDHOT_SNAPWINDOW = (-1)
        End Enum
        ''' <summary>Used for low word of wParam of <see cref="WindowMessages.WM_HSCROLL"/> message and for low word of lParam of <see cref="WindowMessages.WM_HSCROLLCLIPBOARD"/></summary>
        Public Enum WM_HSCROLL_low As Short
            ''' <summary>Ends scroll.</summary>
            SB_ENDSCROLL = 8
            ''' <summary>Scrolls to the upper left.</summary>
            SB_LEFT = 6
            ''' <summary>Scrolls to the lower right.</summary>
            SB_RIGHT = 7
            ''' <summary>Scrolls left by one unit.</summary>
            SB_LINELEFT = 0
            ''' <summary>Scrolls right by one unit.</summary>
            SB_LINERIGHT = 1
            ''' <summary>Scrolls left by the width of the window.</summary>
            SB_PAGELEFT = 2
            ''' <summary>Scrolls right by the width of the window.</summary>
            SB_PAGERIGHT = 3
            ''' <summary>The user has dragged the scroll box (thumb) and released the mouse button. The high-order word indicates the position of the scroll box at the end of the drag operation.</summary>
            SB_THUMBPOSITION = 4
            ''' <summary>The user is dragging the scroll box. This message is sent repeatedly until the user releases the mouse button. The high-order word indicates the position that the scroll box has been dragged to.</summary>
            SB_THUMBTRACK = 5
        End Enum
        ''' <summary>Values used for wParam of <see cref="WindowMessages.WM_IME_CONTROL"/> message</summary>
        Public Enum WM_IME_CONTROL As Integer
            ''' <summary>Instructs the IME window to hide the status window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776088.aspx</seealso></remarks>
            IMC_CLOSESTATUSWINDOW = &H21
            ''' <summary>Instructs an IME window to get the position of the candidate window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a CANDIDATEFORM structure that contains the position of the candidate window.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776097.aspx</seealso></remarks>
            IMC_GETCANDIDATEPOS = &H7
            ''' <summary>Instructs an IME window to retrieve the logical font used for displaying intermediate characters in the composition window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a LOGFONT structure that receives information about the logical font.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776132.aspx</seealso></remarks>
            IMC_GETCOMPOSITIONFONT = &H9
            ''' <summary>Instructs an IME window to get the position of the composition window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a COMPOSITIONFORM structure that contains the position of the composition window.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776104.aspx</seealso></remarks>
            IMC_GETCOMPOSITIONWINDOW = &HB
            ''' <summary>Instructs an IME window to get the position of the status window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>Returns a POINTS structure that contains the x coordinate and y coordinate of the status window position in screen coordinates, relative to the upper left corner of the display screen.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776079.aspx</seealso></remarks>
            IMC_GETSTATUSWINDOWPOS = &HF
            ''' <summary>Instructs the IME window to show the status window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776106.aspx</seealso></remarks>
            IMC_OPENSTATUSWINDOW = &H22
            ''' <summary>Instructs an IME window to set the position of the candidate window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a CANDIDATEFORM structure that contains the x coordinate and y coordinate for the candidate window. The application should set the dwIndex member of this structure.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776165.aspx</seealso></remarks>
            IMC_SETCANDIDATEPOS = &H8
            ''' <summary>Instructs an IME window to specify the logical font to use for displaying intermediate characters in the composition window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a LOGFONT structure that contains information about the logical font.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776168.aspx</seealso></remarks>
            IMC_SETCOMPOSITIONFONT = &HA
            ''' <summary>Instructs an IME window to set the style of the composition window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a COMPOSITIONFORM structure that contains the style information.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776180.aspx</seealso></remarks>
            IMC_SETCOMPOSITIONWINDOW = &HC
            ''' <summary>Instructs an IME window to set the position of the status window. To send this command, the application uses the WM_IME_CONTROL message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a POINTS structure that contains the x coordinate and y coordinate of the position of the status window. The coordinates are in screen coordinates, relative to the upper left corner of the display screen.</description></item>
            ''' <item><term>Return value</term><description>Returns 0 if successful or a nonzero value otherwise.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776113.aspx</seealso></remarks>
            IMC_SETSTATUSWINDOWPOS = &H10
        End Enum
        ''' <summary>Values used for wParam of message <see cref="WindowMessages.WM_IME_NOTIFY"/></summary>
        Public Enum WM_IME_NOTIFY As Integer
            ''' <summary>Notifies the application when an IME is about to change the content of the candidate window. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Candidate list flag. Each bit corresponds to a candidate list: bit 0 to the first list, bit 1 to the second list, and so on. If a specified bit is 1, the corresponding candidate window is about to be changed.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776085.aspx</seealso></remarks>
            IMN_CHANGECANDIDATE = 3
            ''' <summary>Notifies an application when an IME is about to close the candidate window. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Candidate list flag. Each bit corresponds to a candidate list: bit 0 to the first list, bit 1 to the second, and so on. If a specified bit is 1, the corresponding candidate window is about to be closed.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776137.aspx</seealso></remarks>
            IMN_CLOSECANDIDATE = &H4
            ''' <summary>Notifies an application when an IME is about to close the status window. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776129.aspx</seealso></remarks>
            IMN_CLOSESTATUSWINDOW = &H1
            ''' <summary>Notifies an application when an IME is about to show an error message or other information. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776142.aspx</seealso></remarks>
            IMN_GUIDELINE = &HD
            ''' <summary>Notifies an application when an IME is about to open the candidate window. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Candidate list flag. Each bit corresponds to a candidate list: bit 0 to the first list, bit 1 to the second, and so on. If a specified bit is 1, the corresponding candidate window is about to be opened.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776142.aspx</seealso></remarks>
            IMN_OPENCANDIDATE = &H5
            ''' <summary>Notifies an application when an IME is about to open the candidate window. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Candidate list flag. Each bit corresponds to a candidate list: bit 0 to the first list, bit 1 to the second, and so on. If a specified bit is 1, the corresponding candidate window is about to be opened.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776148.aspx</seealso></remarks>
            IMN_OPENSTATUSWINDOW = &H2
            ''' <summary>Notifies an application when an IME is about to create the status window. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776166.aspx</seealso></remarks>
            IMN_SETCANDIDATEPOS = &H9
            ''' <summary>Notifies an application when the font of the input context is updated. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776123.aspx</seealso></remarks>
            IMN_SETCOMPOSITIONFONT = &HA
            ''' <summary>Notifies an application when the style or position of the composition window is updated. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776098.aspx</seealso></remarks>
            IMN_SETCOMPOSITIONWINDOW = &HB
            ''' <summary>Notifies an application when the conversion mode of the input context is updated. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776163.aspx</seealso></remarks>
            IMN_SETCONVERSIONMODE = &H6
            ''' <summary>Notifies an application when the open status of the input context is updated. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776139.aspx</seealso></remarks>
            IMN_SETOPENSTATUS = &H8
            ''' <summary>Notifies an application when the sentence mode of the input context is updated. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso></seealso></remarks>
            IMN_SETSENTENCEMODE = &H7
            ''' <summary>Notifies an application when the status window position in the input context is updated. The application receives this command through the WM_IME_NOTIFY message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>This parameter is not used.</description></item>
            ''' <item><term>Return value</term><description>This command has no return value.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776184.aspx</seealso></remarks>
            IMN_SETSTATUSWINDOWPOS = &HC
        End Enum
        ''' <summary>Values used for wParam of <see cref="WindowMessages.WM_IME_REQUEST"/> message</summary>
        Public Enum WM_IME_REQUEST As Integer
            ''' <summary>Notfies an application when a selected IME needs information about the candidate window. The application receives this command through the WM_IME_REQUEST message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a buffer containing a CANDIDATEFORM structure. Its dwIndex member contains the index to the candidate window referenced.</description></item>
            ''' <item><term>Return value</term><description>Returns a nonzero value if the application fills in the CANDIDATEFORM structure. Otherwise, the command returns 0.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776080.aspx</seealso></remarks>
            IMR_CANDIDATEWINDOW = &H2
            ''' <summary>Notifies an application when a selected IME needs information about the font used by the composition window. The application receives this command through the WM_IME_REQUEST message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a buffer containing a LOGFONT structure. The application fills in the values for the current composition window.</description></item>
            ''' <item><term>Return value</term><description>Returns a nonzero value if the application fills in the LOGFONT structure. Otherwise, the command returns 0.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776149.aspx</seealso></remarks>
            IMR_COMPOSITIONFONT = &H3
            ''' <summary>Notifies an application when a selected IME needs information about the composition window. The application receives this command through the WM_IME_REQUEST message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a buffer containing a COMPOSITIONFORM structure.</description></item>
            ''' <item><term>Return value</term><description>Returns a nonzero value if the application fills in the COMPOSITIONFORM structure. Otherwise, the command returns 0.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776169.aspx</seealso></remarks>
            IMR_COMPOSITIONWINDOW = &H1
            ''' <summary>Notifies an application when the IME needs to change the RECONVERTSTRING structure. The application receives this command through the WM_IME_REQUEST message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a RECONVERTSTRING structure from the IME. For more information, see the Remarks section.</description></item>
            ''' <item><term>Return value</term><description>Returns a nonzero value if the application accepts the changed RECONVERTSTRING structure. Otherwise, the command returns 0 and the IME uses the original RECONVERTSTRING structure.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776182.aspx</seealso></remarks>
            IMR_CONFIRMRECONVERTSTRING = &H5
            ''' <summary>Notifies an application when the selected IME needs the converted string from the application. The application receives this command through the WM_IME_REQUEST message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a buffer to contain theRECONVERTSTRING structure.</description></item>
            ''' <item><term>Return value</term><description>Returns the current reconversion string structure. If lParam is set to a null pointer, the application returns the required size for the buffer to hold the structure. The command returns 0 if it does not succeed.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776164.aspx</seealso></remarks>
            IMR_DOCUMENTFEED = &H7
            ''' <summary>Notifies an application when the selected IME needs information about the coordinates of a character in the composition string. The application receives this command through the WM_IME_REQUEST message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to an IMECHARPOSITION structure that contains the position of the character in the composition window.</description></item>
            ''' <item><term>Return value</term><description>Returns a nonzero value if the application fills the IMECHARPOSITION structure. Otherwise, the command returns 0.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776110.aspx</seealso></remarks>
            IMR_QUERYCHARPOSITION = &H6
            ''' <summary>Notifies an application when a selected IME needs a string for reconversion. The application receives this command through the WM_IME_REQUEST message with wParam and lParam set as follows.</summary>
            ''' <remarks><list type="table">
            ''' <item><term>lParam</term><description>Pointer to a buffer containing the RECONVERTSTRING structure and strings.</description></item>
            ''' <item><term>Return value</term><description>Returns the current reconversion string structure. If lParam is set to a null pointer, the application returns the size for the buffer required to hold the structure. The command returns 0 if it does not succeed.</description></item>
            ''' </list><seealso>http://msdn2.microsoft.com/en-us/library/ms776135.aspx</seealso></remarks>
            IMR_RECONVERTSTRING = &H4
        End Enum
        ''' <summary>Values used for wParam of <see cref="WindowMessages.WM_INPUTLANGCHANGEREQUEST"/> message</summary>
        <Flags()> Public Enum WM_INPUTLANGCHANGEREQUEST As Integer
            ''' <summary>Windows 2000/XP: A hot key was used to choose the previous input locale in the installed list of input locales. This flag cannot be used with the INPUTLANGCHANGE_FORWARD flag.</summary>
            INPUTLANGCHANGE_BACKWARD = &H4
            ''' <summary>Windows 2000/XP: A hot key was used to choose the next input locale in the installed list of input locales. This flag cannot be used with the INPUTLANGCHANGE_BACKWARD flag.</summary>
            INPUTLANGCHANGE_FORWARD = &H2
            ''' <summary>Windows 2000/XP:The new input locale's keyboard layout can be used with the system character set.</summary>
            INPUTLANGCHANGE_SYSCHARSET = &H1
        End Enum
        ''' <summary>Values used for wParam of <see cref="WindowMessages.WM_LBUTTONDBLCLK"/>, <see cref="WindowMessages.WM_LBUTTONDOWN"/>, <see cref="WindowMessages.WM_LBUTTONUP"/>, <see cref="WindowMessages.WM_MBUTTONDBLCLK"/>, <see cref="WindowMessages.WM_MBUTTONDOWN"/>, <see cref="WindowMessages.WM_MBUTTONUP"/></summary>
        <Flags()> Public Enum WM_LBUTTONDBLCLK As Integer
            ''' <summary>The CTRL key is down.</summary>
            MK_CONTROL = &H8
            ''' <summary>The left mouse button is down.</summary>
            MK_LBUTTON = &H1
            ''' <summary>The middle mouse button is down.</summary>
            MK_MBUTTON = &H10
            ''' <summary>The right mouse button is down.</summary>
            MK_RBUTTON = &H2
            ''' <summary>The SHIFT key is down.</summary>
            MK_SHIFT = &H4
            ''' <summary>Windows 2000/XP: The first X button is down.</summary>
            MK_XBUTTON1 = &H20
            ''' <summary>Windows 2000/XP: The second X button is down.</summary>
            MK_XBUTTON2 = &H40
        End Enum
        ''' <summary>Values used fro wParam as <see cref="WindowMessages.WM_MDICASCADE"/></summary>
        <Flags()> Public Enum WM_MDICASCADE As Integer
            ''' <summary>Prevents disabled MDI child windows from being cascaded.</summary>
            MDITILE_SKIPDISABLED = &H2
            ''' <summary>Windows 2000/XP: Arranges the windows in Z order.</summary>
            MDITILE_ZORDER = &H4
        End Enum
        ''' <summary>Values for wParam of <see cref="WindowMessages.WM_MDITILE"/> message</summary>
        Public Enum WM_MDITILE As Integer
            ''' <summary>Tiles windows horizontally.</summary>
            MDITILE_HORIZONTAL = &H1
            ''' <summary>Tiles windows vertically.</summary>
            MDITILE_VERTICAL = &H0
            ''' <summary>prevent disabled MDI child windows from being tiled</summary>
            MDITILE_SKIPDISABLED = &H2
        End Enum
        ''' <summary>Values used for high word of wParam of the <see cref="WindowMessages.WM_MENUCHAR"/> message</summary>
        Public Enum WM_MENUCHAR_high As Short
            ''' <summary>A drop-down menu, submenu, or shortcut menu.</summary>
            MF_POPUP = &H10S
            ''' <summary>The window menu.</summary>
            MF_SYSMENU = &H2000S
        End Enum
        ''' <summary>Values used for high word of wParam of the <see cref="WindowMessages.WM_MENUSELECT"/></summary>
        <Flags(), CLSCompliant(False)> Public Enum WM_MENUSELECT_high As UShort
            ''' <summary>Item displays a bitmap.</summary>
            MF_BITMAP = &H4US
            ''' <summary></summary>
            MF_CHECKED = &H8US
            ''' <summary>Item is disabled.</summary>
            MF_DISABLED = &H2US
            ''' <summary>Item is grayed.</summary>
            MF_GRAYED = &H1US
            ''' <summary>Item is highlighted.</summary>
            MF_HILITE = &H80US
            ''' <summary>Item is selected with the mouse.</summary>
            MF_MOUSESELECT = &H8000US
            ''' <summary>    Item is an owner-drawn item.</summary>
            MF_OWNERDRAW = &H100US
            ''' <summary>Item is contained in the window menu. The lParam parameter contains a handle to the menu associated with the message.</summary>
            MF_POPUP = &H10US
            ''' <summary>Item is contained in the window menu. The lParam parameter contains a handle to the menu associated with the message.</summary>
            MF_SYSMENU = &H2000US
        End Enum
    End Namespace
    Namespace lParam
        ''' <summary>Bit-masks used by <see cref="WindowMessages.WM_CHAR"/>, <see cref="WindowMessages.WM_DEADCHAR"/>, <see cref="WindowMessages.WM_IME_CHAR"/>, <see cref="WindowMessages.WM_IME_KEYDOWN"/>, <see cref="WindowMessages.WM_IME_KEYUP"/>, <see cref="WindowMessages.WM_KEYDOWN"/>, <see cref="WindowMessages.WM_KEYUP"/> message for lParam</summary>
        ''' <remarks>And lParam with mask and to get appropriete value</remarks>
        Public Enum WM_CHAR As Integer
            ''' <summary>Bits 0÷15: Specifies the repeat count for the current message. The value is the number of times the keystroke is autorepeated as a result of the user holding down the key. If the keystroke is held long enough, multiple messages are sent. However, the repeat count is not cumulative.</summary>
            ''' <remarks><see cref="WindowMessages.WM_IME_CHAR"/>: Repeat count. Since the first byte and second byte is continuous, this is always 1.</remarks>
            RepeatCountMask = &HFFFF
            ''' <summary>Bits 16÷23: Specifies the scan code. The value depends on the OEM.</summary>
            ''' <remarks><see cref="WindowMessages.WM_IME_CHAR"/>: Scan code for a complete Asian character.</remarks>
            ScanCodeMask = &HFF0000
            ''' <summary>Bit 24: Specifies whether the key is an extended key, such as the right-hand ALT and CTRL keys that appear on an enhanced 101- or 102-key keyboard. The value is 1 if it is an extended key; otherwise, it is 0.</summary>
            ExtendedMask = &H1000000
            ''' <summary>Bits 25÷28: Reserved; do not use.</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> ReservedMask = &H1E000000
            ''' <summary>Bit 29: Specifies the context code. The value is 1 if the ALT key is held down while the key is pressed; otherwise, the value is 0.</summary>
            ContextCodeMask = &H20000000
            ''' <summary>Bit 30: Specifies the previous key state. The value is 1 if the key is down before the message is sent, or it is 0 if the key is up.</summary>
            PreviousKeyStateMask = &H40000000
            ''' <summary>Bit 31: Specifies the transition state. The value is 1 if the key is being released, or it is 0 if the key is being pressed.</summary>
            TransitionStateMask = &H80000000
        End Enum
        ''' <summary>Bit masks used by the <see cref="WindowMessages.WM_ENDSESSION"/> message</summary>
        <Flags()> Public Enum WM_ENDSESSION As Integer
            ''' <summary>the system is shutting down or restarting (it is not possible to determine which event is occurring)</summary>
            <EditorBrowsable(EditorBrowsableState.Advanced)> Zero = 0
            ''' <summary>f wParam is TRUE, the application must shut down. Any data should be saved automatically without prompting the user (for more information, see Remarks). The Restart Manager sends this message when the application is using a file that needs to be replaced, when it must service the system, or when system resources are exhausted. The application will be restarted if it has registered for restart using the RegisterApplicationRestart function. For more information, see Guidelines for Applications.
            ''' If wParam is FALSE, the application should not shut down.</summary>
            ENDSESSION_CLOSEAPP = 1
            ''' <summary>The user is logging off. For more information, see Logging Off (http://msdn2.microsoft.com/en-us/library/aa376876.aspx).</summary>
            ENDSESSION_LOGOFF = &H80000000
        End Enum
        ''' <summary>Values osed for low word of lParam of <see cref="WindowMessages.WM_HOTKEY"/> message</summary>
        <Flags()> Public Enum WM_HOTKEY_low As Short
            ''' <summary>Either ALT key was held down.</summary>
            MOD_ALT = &H1
            ''' <summary>Either CTRL key was held down.</summary>
            MOD_CONTROL = &H2
            ''' <summary>Either SHIFT key was held down.</summary>
            MOD_SHIFT = &H4
            ''' <summary>Either WINDOWS key was held down. These keys are labeled with the Microsoft Windows logo. Hotkeys that involve the Windows key are reserved for use by the operating system.</summary>
            MOD_WIN = &H8
        End Enum
        ''' <summary>Values used for lParam of <see cref="WindowMessages.WM_IME_COMPOSITION"/></summary>
        <Flags()> Public Enum WM_IME_COMPOSITION
            ''' <summary>Retrieves or updates the attribute of the composition string.</summary>
            GCS_COMPATTR = IMECompositionStringValues.GCS_COMPATTR
            ''' <summary>Retrieves or updates clause information of the composition string.</summary>
            GCS_COMPCLAUSE = IMECompositionStringValues.GCS_COMPCLAUSE
            ''' <summary> 	Retrieves or updates the reading string of the current composition.</summary>
            GCS_COMPREADSTR = IMECompositionStringValues.GCS_COMPREADSTR
            ''' <summary>Retrieves or updates the attributes of the reading string of the current composition.</summary>
            GCS_COMPREADATTR = IMECompositionStringValues.GCS_COMPREADATTR
            ''' <summary> 	Retrieves or updates the clause information of the reading string of the composition string.</summary>
            GCS_COMPREADCLAUSE = IMECompositionStringValues.GCS_COMPREADCLAUSE
            ''' <summary> 	Retrieves or updates the current composition string.</summary>
            GCS_COMPSTR = IMECompositionStringValues.GCS_COMPSTR
            ''' <summary> 	Retrieves or updates the cursor position in composition string.</summary>
            GCS_CURSORPOS = IMECompositionStringValues.GCS_CURSORPOS
            ''' <summary> 	Retrieves or updates the cursor position in composition string.</summary>
            GCS_DELTASTART = IMECompositionStringValues.GCS_DELTASTART
            ''' <summary>Retrieves or updates clause information of the result string.</summary>
            GCS_RESULTCLAUSE = IMECompositionStringValues.GCS_RESULTCLAUSE
            ''' <summary> 	Retrieves or updates the reading string.</summary>
            GCS_RESULTREADCLAUSE = IMECompositionStringValues.GCS_RESULTREADCLAUSE
            ''' <summary> 	Retrieves or updates the reading string.</summary>
            GCS_RESULTREADSTR = IMECompositionStringValues.GCS_RESULTREADSTR
            ''' <summary>Retrieves or updates the string of the composition result.</summary>
            GCS_RESULTSTR = IMECompositionStringValues.GCS_RESULTSTR
            ''' <summary> 	Insert the wParam composition character at the current insertion point. An application should display the composition character if it processes this message.</summary>
            CS_INSERTCHAR = &H2000
            ''' <summary>Do not move the caret position as a result of processing the message. For example, if an IME specifies a combination of CS_INSERTCHAR and CS_NOMOVECARET, the application should insert the specified character at the current caret position but should not move the caret to the next position. A subsequent WM_IME_COMPOSITION message with GCS_RESULTSTR will replace this character.</summary>
            CS_NOMOVECARET = &H4000
        End Enum
        ''' <summary>Values used for lparam of <see cref="WindowMessages.WM_IME_SETCONTEXT"/> message</summary>
        <Flags()> Public Enum WM_IME_SETCONTEXT As Integer
            ''' <summary>Show the composition window by user interface window.</summary>
            ISC_SHOWUICOMPOSITIONWINDOW = &H80000000
            'ASAP: Find values of these constants!!!
            ''' <summary>Show the guide window by user interface window.</summary>
            <Obsolete("Value of this constant is unknown, do not use it!", True), EditorBrowsable(EditorBrowsableState.Never)> _
            ISC_SHOWUIGUIDWINDOW
            ''' <summary>Show the candidate window of index 0 by user interface window.</summary>
            ISC_SHOWUICANDIDATEWINDOW = &H1
            ''' <summary>Show the soft keyboard by user interface window.</summary>
            <Obsolete("Value of this constant is unknown, do not use it!", True), EditorBrowsable(EditorBrowsableState.Never)> _
            ISC_SHOWUISOFTKBD
            ''' <summary>Show the candidate window of index 1 by user interface window.</summary>
            ISC_SHOWUICANDIDATEWINDOW_l1 = ISC_SHOWUICANDIDATEWINDOW << 1
            ''' <summary> 	Show the candidate window of index 2 by user interface window.</summary>
            ISC_SHOWUICANDIDATEWINDOW_l2 = ISC_SHOWUICANDIDATEWINDOW << 2
            ''' <summary>Show the candidate window of index 3 by user interface window.</summary>
            ISC_SHOWUICANDIDATEWINDOW_l3 = ISC_SHOWUICANDIDATEWINDOW << 3
        End Enum
    End Namespace
    Namespace ReturnValues
        ''' <summary>Return values used by the <see cref="WindowMessages.WM_COMPAREITEM"/> message</summary>
        Public Enum WM_COMPAREITEM As Integer
            ''' <summary>Item 1 precedes item 2 in the sorted order.</summary>
            Smaller = -1
            ''' <summary>Items 1 and 2 are equivalent in the sorted order.</summary>
            Equal = 0
            ''' <summary>Item 1 follows item 2 in the sorted order.</summary>
            Greater = 1
        End Enum
        ''' <summary>Values used as return values for the <see cref="WindowMessages.WM_DEVICECHANGE"/> message</summary>
        Public Enum WM_DEVICECHANGE As Integer
            ''' <summary>Request granted</summary>
            [TRUE] = API.APIBool.TRUE
            ''' <summary>Request denied</summary>
            BROADCAST_QUERY_DENY = &H424D5144
        End Enum
        ''' <summary>Return values used by the <see cref="WindowMessages.WM_GETDLGCODE"/> message</summary>
        <Flags()> Public Enum WM_GETDLGCODE As Integer
            ''' <summary>Button.</summary>
            DLGC_BUTTON = &H2000
            ''' <summary>Default push button.</summary>
            DLGC_DEFPUSHBUTTON = &H10
            ''' <summary>EM_SETSEL messages.</summary>
            DLGC_HASSETSEL = &H8
            ''' <summary>Radio button.</summary>
            DLGC_RADIOBUTTON = &H40
            ''' <summary>Static control.</summary>
            DLGC_STATIC = &H100
            ''' <summary>Non-default push button.</summary>
            DLGC_UNDEFPUSHBUTTON = &H20
            ''' <summary>All keyboard input.</summary>
            DLGC_WANTALLKEYS = &H4
            ''' <summary>Direction keys.</summary>
            DLGC_WANTARROWS = &H1
            ''' <summary>WM_CHAR messages.</summary>
            DLGC_WANTCHARS = &H80
            ''' <summary>All keyboard input (the application passes this message in the MSG structure to the control).</summary>
            DLGC_WANTMESSAGE = &H4
            ''' <summary>TAB key.</summary>
            DLGC_WANTTAB = &H2
        End Enum
        ''' <summary>Used for hight word of return value of <see cref="WindowMessages.WM_GETHOTKEY"/> message</summary>
        ''' <remarks>You must left-shift by 24 bits this value before it can be used as return value, or use <see cref="WM_GETHOTKEY_high_shifted"/> that can be directly or-ed with virtual key code</remarks>
        <Flags()> Public Enum WM_GETHOTKEY_high As Byte
            ''' <summary>ALT key</summary>
            HOTKEYF_ALT = &H4
            ''' <summary>CTRL key</summary>
            HOTKEYF_CONTROL = &H2
            ''' <summary>Extended key</summary>
            HOTKEYF_EXT = &H80
            ''' <summary>SHIFT key</summary>
            HOTKEYF_SHIFT = &H1
        End Enum
        ''' <summary>Used to be or-ed with virtual key codes and returned as return value for <see cref="WindowMessages.WM_GETHOTKEY"/></summary>
        ''' <remarks>Those values are 24-bits-left-shifted from original values (so they are placed in high byte of double-word) for high word of <see cref="WM_GETHOTKEY_high"/></remarks>
        <Flags()> Public Enum WM_GETHOTKEY_high_shifted As Integer
            ''' <summary>ALT key</summary>
            HOTKEYF_ALT = CInt(WM_GETHOTKEY_high.HOTKEYF_ALT) << 24
            ''' <summary>CTRL key</summary>
            HOTKEYF_CONTROL = CInt(WM_GETHOTKEY_high.HOTKEYF_CONTROL) << 24
            ''' <summary>Extended key</summary>
            HOTKEYF_EXT = CInt(WM_GETHOTKEY_high.HOTKEYF_EXT) << 24
            ''' <summary>SHIFT key</summary>
            HOTKEYF_SHIFT = CInt(WM_GETHOTKEY_high.HOTKEYF_SHIFT) << 24
        End Enum
        ''' <summary>Values used as return values for <see cref="WindowMessages.WM_MENUCHAR"/> messages</summary>
        Public Enum WM_MENUCHAR As Integer
            ''' <summary>Informs the system that it should discard the character the user pressed and create a short beep on the system speaker.</summary>
            MNC_IGNORE = 0
            ''' <summary>Informs the system that it should close the active menu.</summary>
            MNC_CLOSE = 1
            ''' <summary>Informs the system that it should choose the item specified in the low-order word of the return value. The owner window receives a WM_COMMAND message.</summary>
            MNC_EXECUTE = 2
            ''' <summary>Informs the system that it should select the item specified in the low-order word of the return value.</summary>
            MNC_SELECT = 3
        End Enum
        ''' <summary>Values used as return values for message <see cref="WindowMessages.WM_MENUDRAG"/></summary>
        Public Enum WM_MENUDRAG As Integer
            ''' <summary>Menu should remain active. If the mouse is released, it should be ignored.</summary>
            MND_CONTINUE = 0
            ''' <summary>	Menu should be ended.</summary>
            MND_ENDMENU = 1
        End Enum
        ''' <summary>Values used as return values for message <see cref="WindowMessages.WM_MENUGETOBJECT"/></summary>
        Public Enum WM_MENUGETOBJECT As Integer
            ''' <summary>An interface pointer was returned in the pvObj member of MENUGETOBJECTINFO</summary>
            MNGO_NOERROR = &H1
            ''' <summary>The interface is not supported.</summary>
            MNGO_NOINTERFACE = &H0
        End Enum
        ''' <summary>Values used as return values for the <see cref="WindowMessages.WM_MOUSEACTIVATE"/> message</summary>
        Public Enum WM_MOUSEACTIVATE As Integer
            ''' <summary>	Activates the window, and does not discard the mouse message.</summary>
            MA_ACTIVATE = 1
            ''' <summary>Activates the window, and discards the mouse message.</summary>
            MA_ACTIVATEANDEAT = 2
            ''' <summary>	Does not activate the window, and does not discard the mouse message.</summary>
            MA_NOACTIVATE = 3
            ''' <summary>Does not activate the window, but discards the mouse message.</summary>
            MA_NOACTIVATEANDEAT = 4
        End Enum
    End Namespace
End Namespace
#End If




