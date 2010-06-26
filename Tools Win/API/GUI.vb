Imports System.Runtime.InteropServices

Namespace API
    ''' <summary>Contains declarations of Win32 API related to GUI</summary>
    Friend Module GUI
        ''' <summary>The <see cref="GetSystemMenu"/> function allows the application to access the window menu (also known as the System menu or the Control menu) for copying and modifying.</summary>
        ''' <param name="hwnd">Identifies the window that will own a copy of the window menu.</param>
        ''' <param name="revert">
        ''' Specifies the action to be taken. If this parameter is <see cref="APIBool.FALSE"/>, <see cref="GetSystemMenu"/> returns the handle of the copy of the window menu currently in use. The copy is initially identical to the window menu, but it can be modified.
        ''' If this parameter is <see cref="APIBool.TRUE"/>, <see cref="GetSystemMenu"/> resets the window menu back to the Windows default state. The previous window menu, if any, is destroyed.</param>
        ''' <returns>If the <paramref name="bRevert"/> parameter is <see cref="APIBool.FALSE"/>, the return value is the handle of a copy of the window menu. If the <paramref name="bRevert"/> parameter is <see cref="APIBool.TRUE"/>, the return value is <see cref="NULL"/>. </returns>
        Public Declare Function GetSystemMenu Lib "user32" (ByVal hwnd As IntPtr, ByVal revert As APIBool) As IntPtr
        ''' <summary>The <see cref="EnableMenuItem"/> function enables, disables, or grays the specified menu item.</summary>
        ''' <param name="menu">Handle to the menu</param>
        ''' <param name="ideEnableItem">Specifies the menu item to be enabled, disabled, or grayed, as determined by the <paramref name="uEnable"/> parameter. This parameter specifies an item in a menu bar, menu, or submenu. Some menu items that can be manipuleated when <paramref name="enable"/> is combination of <see cref="enmEnableMenuItemStatus.MF_BYCOMMAND"/> are listed in <see cref="SystemMenuItems"/></param>
        ''' <param name="enable">
        ''' Controls the interpretation of the <paramref name="uIDEnableItem"/> parameter and indicate whether the menu item is enabled, disabled, or grayed. This parameter must be a combination of either <see cref="enmEnableMenuItemStatus.MF_BYCOMMAND"/> or <see cref="enmEnableMenuItemStatus.MF_BYPOSITION"/> and <see cref="enmEnableMenuItemStatus.MF_ENABLED"/>, <see cref="enmEnableMenuItemStatus.MF_DISABLED"/>, or <see cref="enmEnableMenuItemStatus.MF_GRAYED"/>. 
        ''' </param>
        ''' <returns>The return value specifies the previous state of the menu item (it is either <see cref="enmEnableMenuItemStatus.MF_DISABLED"/>, <see cref="enmEnableMenuItemStatus.MF_ENABLED"/>, or <see cref="enmEnableMenuItemStatus.MF_GRAYED"/>). If the menu item does not exist, the return value is -1.</returns>
        ''' <remarks>
        ''' <para>An application must use the <see cref="enmEnableMenuItemStatus.MF_BYPOSITION"/> flag to specify the correct menu handle. If the menu handle to the menu bar is specified, the top-level menu item (an item in the menu bar) is affected. To set the state of an item in a drop-down menu or submenu by position, an application must specify a handle to the drop-down menu or submenu. </para>
        ''' <para>When an application specifies the <see cref="enmEnableMenuItemStatus.MF_BYCOMMAND"/> flag, the system checks all items that open submenus in the menu identified by the specified menu handle. Therefore, unless duplicate menu items are present, specifying the menu handle to the menu bar is sufficient. </para>
        ''' <para>The InsertMenu, InsertMenuItem, LoadMenuIndirect, ModifyMenu, and SetMenuItemInfo API functions can also set the state (enabled, disabled, or grayed) of a menu item.</para>
        ''' <para>When you change a window menu, the menu bar is not immediately updated. To force the update, call API DrawMenuBar.</para>
        ''' </remarks>
        Public Declare Function EnableMenuItem Lib "user32" (ByVal menu As IntPtr, ByVal ideEnableItem As Integer, ByVal enable As enmEnableMenuItemStatus) As enmPreviousMenuItemStatus
        ''' <summary>Values for <see cref="EnableMenuItem"/>'s enable parameter</summary>
        Public Enum enmEnableMenuItemStatus As Integer
            ''' <summary>Indicates that uIDEnableItem gives the identifier of the menu item. If neither the MF_BYCOMMAND nor MF_BYPOSITION flag is specified, the MF_BYCOMMAND flag is the default flag.</summary>
            MF_BYCOMMAND = &H0I
            ''' <summary>Indicates that uIDEnableItem gives the zero-based relative position of the menu item.</summary>
            MF_BYPOSITION = &H400I
            ''' <summary>Indicates that the menu item is disabled, but not grayed, so it cannot be selected.</summary>
            MF_DISABLED = &H2I
            ''' <summary>Indicates that the menu item is disabled and grayed so that it cannot be selected.</summary>
            MF_GRAYED = &H1I
            ''' <summary>Indicates that the menu item is enabled and restored from a grayed state so that it can be selected.</summary>
            MF_ENABLED = &H0I
        End Enum
        ''' <summary>Values returned by <see cref="EnableMenuItem"/> function</summary>
        Public Enum enmPreviousMenuItemStatus As Integer
            ''' <summary>Indicates that the menu item is disabled, but not grayed, so it cannot be selected.</summary>
            MF_DISABLED = &H2I
            ''' <summary>Indicates that the menu item is disabled and grayed so that it cannot be selected.</summary>
            MF_GRAYED = &H1I
            ''' <summary>Indicates that the menu item is enabled and restored from a grayed state so that it can be selected.</summary>
            MF_ENABLED = &H0I
            ''' <summary>Indicates that the menu item does not exist.</summary>
            DoesNotExist = -1
        End Enum

        ''' <summary>Win32 window system menu standard items</summary>
        ''' <remarks>Used by <see cref="EnableMenuItem"/>'s ideEnableItem parameter when the enable parameter is combination of <see cref="enmEnableMenuItemStatus.MF_BYCOMMAND"/></remarks>
        Public Enum SystemMenuItems As Int32
            ''' <summary>Close (X) button</summary>
            SC_CLOSE = &HF060I
            ''' <summary>Move menu item (doesn't work)</summary>
            SC_MOVE = &HF010
            ''' <summary>Maximize button (doesn't work)</summary>
            SC_MAXIMIZE = &HF030I
            ''' <summary>Mninimize button (doesn't work)</summary>
            SC_MINIMIZE = &HF020I
            ''' <summary>Resize menu item (doesn't work)</summary>
            SC_SIZE = &HF000
            ''' <summary>Restore button (doesn't work)</summary>
            SC_RESTORE = &HF120
        End Enum

        ''' <summary>The <see cref="RemoveMenu"/> function deletes a menu item or detaches a submenu from the specified menu. If the menu item opens a drop-down menu or submenu, <see cref="RemoveMenu"/> does not destroy the menu or its handle, allowing the menu to be reused. Before this function is called, the GetSubMenu function should retrieve a handle to the drop-down menu or submenu. </summary>
        ''' <param name="hMenu">Handle to the menu to be changed. </param>
        ''' <param name="nPosition">Specifies the menu item to be deleted, as determined by the <paramref name="wFlags"/> parameter. </param>
        ''' <param name="wFlags">Specifies how the <paramref name="nPosition"/> parameter is interpreted. This parameter must be one of the following values: <see cref="enmSelectMenuMethod.MF_BYCOMMAND"/> or <see cref="enmSelectMenuMethod.MF_BYPOSITION"/></param>
        ''' <returns>If the function succeeds, the return value is nonzero.
        ''' If the function fails, the return value is zero. To get extended error information, call GetLastError.
        ''' </returns>
        ''' <remarks>The application must call the <see cref="DrawMenuBar"/> function whenever a menu changes, whether or not the menu is in a displayed window.</remarks>
        Public Declare Function RemoveMenu Lib "user32.dll" (ByVal hMenu As IntPtr, ByVal nPosition As Int32, ByVal wFlags As enmSelectMenuMethod) As Int32
        ''' <summary>Deletes an item from the specified menu. If the menu item opens a menu or submenu, this function destroys the handle to the menu or submenu and frees the memory used by the menu or submenu.</summary>
        ''' <param name="hMenu">Handle to the menu to be changed. </param>
        ''' <param name="uPosition">Specifies the menu item to be deleted, as determined by the <paramref name="uFlags"/> parameter. </param>
        ''' <param name="uFlags">Specifies how the <paramref name="uPosition"/> parameter is interpreted. This parameter must be one of the <see cref="enmSelectMenuMethod"/> values. </param>
        ''' <returns>If the function succeeds, the return value is True. If the function fails, the return value is False.</returns>
        Public Declare Function DeleteMenu Lib "user32" (ByVal hMenu As IntPtr, ByVal uPosition As Integer, ByVal uFlags As enmSelectMenuMethod) As Boolean
        ''' <summary>Values for <see cref="EnableMenuItem"/>'s enable parameter</summary>
        Public Enum enmSelectMenuMethod As Integer
            ''' <summary>Indicates that uIDEnableItem gives the identifier of the menu item. If neither the MF_BYCOMMAND nor MF_BYPOSITION flag is specified, the MF_BYCOMMAND flag is the default flag.</summary>
            MF_BYCOMMAND = &H0I
            ''' <summary>Indicates that uIDEnableItem gives the zero-based relative position of the menu item.</summary>
            MF_BYPOSITION = &H400I
        End Enum
        ''' <summary>The DrawMenuBar function redraws the menu bar of the specified window. If the menu bar changes after the system has created the window, this function must be called to draw the changed menu bar. </summary>
        ''' <param name="hwnd">Handle to the window whose menu bar needs redrawing.</param>
        ''' <returns>If the function succeeds, the return value is nonzero.
        ''' If the function fails, the return value is zero. To get extended error information, call GetLastError. 
        ''' </returns>
        Public Declare Function DrawMenuBar Lib "user32.dll" (ByVal hwnd As IntPtr) As Int32
        ''' <summary>The GetWindowRect function retrieves the dimensions of the bounding rectangle of the specified window. The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.</summary>
        ''' <param name="hwnd">Identifies the window.</param>
        ''' <param name="lpRect">Points to a RECT structure that receives the screen coordinates of the upper-left and lower-right corners of the window.</param>
        ''' <returns>If the function succeeds, the return value is nonzero.
        ''' If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        Public Declare Function GetWindowRect Lib "user32.dll" (ByVal hwnd As IntPtr, ByRef lpRect As RECT) As Boolean
        ''' <summary>The RECT structure defines the coordinates of the upper-left and lower-right corners of a rectangle.</summary>
        ''' <remarks>By convention, the right and bottom edges of the rectangle are normally considered exclusive. In other words, the pixel whose coordinates are (right, bottom) lies immediately outside of the the rectangle. For example, when RECT is passed to the FillRect function, the rectangle is filled up to, but not including, the right column and bottom row of pixels. This structure is identical to the RECTL structure.</remarks>
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure RECT
            ''' <summary>Specifies the x-coordinate of the upper-left corner of the rectangle.</summary>
            Public Left As Int32
            ''' <summary>Specifies the y-coordinate of the upper-left corner of the rectangle.</summary>
            Public Top As Int32
            ''' <summary>Specifies the x-coordinate of the lower-right corner of the rectangle.</summary>
            Public Right As Int32
            ''' <summary>Specifies the y-coordinate of the lower-right corner of the rectangle.</summary>
            Public Bottom As Int32
            ''' <summary>Initializes <see cref="RECT"/> structure</summary>
            ''' <param name="Left">Specifies the x-coordinate of the upper-left corner of the rectangle.</param>
            ''' <param name="Top">Specifies the y-coordinate of the upper-left corner of the rectangle.</param>
            ''' <param name="Right">Specifies the x-coordinate of the lower-right corner of the rectangle.</param>
            ''' <param name="Bottom">Specifies the y-coordinate of the lower-right corner of the rectangle.</param>
            Public Sub New(ByVal Left%, ByVal Top%, ByVal Right%, ByVal Bottom%)
                Me.Left = Left : Me.Bottom = Bottom : Me.Right = Right : Me.Top = Top
            End Sub
            ''' <summary>Converts <see cref="Rectangle"/> to <see cref="RECT"/></summary>
            ''' <param name="a">A <see cref="Rectangle"/></param>
            ''' <returns><see cref="RECT"/> equivalent to <paramref name="a"/></returns>
            Public Shared Widening Operator CType(ByVal a As Rectangle) As RECT
                Return New RECT(a.Left, a.Top, a.Right, a.Bottom)
            End Operator
            ''' <summary>Converts <see cref="RECT"/> to <see cref="Rectangle"/></summary>
            ''' <param name="a">A <see cref="RECT"/></param>
            ''' <returns><see cref="Rectangle"/> equivalent to <paramref name="a"/></returns>
            Public Shared Widening Operator CType(ByVal a As RECT) As Rectangle
                Return New Rectangle(a.Left, a.Top, a.Right - a.Left, a.Bottom - a.Top)
            End Operator
        End Structure
        ''' <summary>The MoveWindow function changes the position and dimensions of the specified window. For a top-level window, the position and dimensions are relative to the upper-left corner of the screen. For a child window, they are relative to the upper-left corner of the parent window’s client area.</summary>
        ''' <param name="bRepaint">Specifies whether the window is to be repainted. If this parameter is TRUE, the window receives a WM_PAINT message. If the parameter is FALSE, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of moving a child window. If this parameter is FALSE, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</param>
        ''' <param name="hwnd">Identifies the window.</param>
        ''' <param name="nHeight">Specifies the new height of the window.</param>
        ''' <param name="nWidth">Specifies the new width of the window.</param>
        ''' <param name="x">Specifies the new position of the left side of the window.</param>
        ''' <param name="y">Specifies the new position of the top of the window.</param>
        ''' <returns>If the function succeeds, the return value is nonzero.
        ''' If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        Public Declare Function MoveWindow Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal x As Int32, ByVal y As Int32, ByVal nWidth As Int32, ByVal nHeight As Int32, ByVal bRepaint As Int32) As Boolean
        ''' <summary>The SetParent function changes the parent window of the specified child window.</summary>
        ''' <param name="hWndChild">Identifies the child window.</param>
        ''' <param name="hWndNewParent">Identifies the new parent window. If this parameter is NULL, the desktop window becomes the new parent window.</param>
        ''' <returns>If the function succeeds, the return value is nonzero.
        ''' If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        Public Declare Function SetParent Lib "user32.dll" (ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As Boolean
        ''' <summary>The GetParent function retrieves the handle of the specified child window’s parent window.</summary>
        ''' <param name="hwnd">Identifies the window whose parent window handle is to be retrieved.</param>
        ''' <remarks>If the function succeeds, the return value is the handle of the parent window. If the window has no parent window, the return value is NULL. To get extended error information, call GetLastError.</remarks>
        Public Declare Function GetParent Lib "user32.dll" (ByVal hwnd As IntPtr) As Int32
        ''' <summary>The SetWindowLong function changes an attribute of the specified window. The function also sets a 32-bit (long) value at the specified offset into the extra window memory of a window.</summary>
        ''' <param name="hwnd">Identifies the window and, indirectly, the class to which the window belongs.</param>
        ''' <param name="nIndex">Specifies the zero-based offset to the value to be set. Valid values are in the range zero through the number of bytes of extra window memory, minus 4; for example, if you specified 12 or more bytes of extra memory, a value of 8 would be an index to the third 32-bit integer.</param>
        ''' <param name="dwNewLong">Specifies the replacement value.</param>
        ''' <returns>If the function succeeds, the return value is the previous value of the specified 32-bit integer.</returns>
        Public Declare Auto Function SetWindowLong Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal nIndex As WindowLongs, ByVal dwNewLong As Int32) As Int32
        ''' <summary>Overload of the <see cref="SetWindowLong"/> function used to set window proc.</summary>
        ''' <param name="hwnd">Identifies the window and, indirectly, the class to which the window belongs.</param>
        ''' <param name="nIndex">Specifies the zero-based offset to the value to be set. This overload expects one of the <see cref="WindowProcs"/> values</param>
        ''' <param name="NewProc">New window procedure - converted to pointer.</param>
        ''' <returns>If the function succeeds, the return value is the previous value of the specified 32-bit integer.</returns>
        Public Declare Auto Function SetWindowLong Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal nIndex As WindowProcs, ByVal NewProc As Messages.WndProc) As Boolean
        ''' <summary>The GetWindowLong function retrieves information about the specified window. The function also retrieves the 32-bit (long) value at the specified offset into the extra window memory of a window.</summary>
        ''' <param name="hwnd">Identifies the window and, indirectly, the class to which the window belongs.</param>
        ''' <param name="nIndex">Specifies the zero-based offset to the value to be retrieved. Valid values are in the range zero through the number of bytes of extra window memory, minus four; for example, if you specified 12 or more bytes of extra memory, a value of 8 would be an index to the third 32-bit integer.</param>
        ''' <returns>If the function succeeds, the return value is the requested 32-bit value. 
        ''' If the function fails, the return value is zero. To get extended error information, call GetLastError. </returns>
        Public Declare Auto Function GetWindowLong Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal nIndex As WindowLongs) As Int32
        ''' <summary>Overload of the <see cref="GetWindowLong"/> function used to get window proc.</summary>
        ''' <param name="hwnd">Identifies the window and, indirectly, the class to which the window belongs.</param>
        ''' <param name="nIndex">Specifies the zero-based offset to the value to be retrieved. This overload expects one of <see cref="WindowProcs"/> values.</param>
        ''' <returns>If the function succeeds, the return value is requested delegate. If it fails the return value is null.</returns>
        Public Declare Auto Function GetWindowLong Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal nIndex As WindowProcs) As Messages.WndProc
        ''' <summary>passes message information to the specified window procedure. </summary>
        ''' <param name="lpPrevWndFunc">[in] Pointer to the previous window procedure. If this value is obtained by calling the <see cref="GetWindowLong"/> function with the <paramref name="nIndex"/> parameter set to <see cref="WindowLongs.GWL_WNDPROC"/> or <see cref="WindowLongs.DWL_DLGPROC"/>, it is actually either the address of a window or dialog box procedure, or a special internal value meaningful only to <see cref="CallWindowProc"/>. </param>
        ''' <param name="hwnd">[in] Handle to the window procedure to receive the message. </param>
        ''' <param name="msg">[in] Specifies the message. </param>
        ''' <param name="lParam">[in] Specifies additional message-specific information. The contents of this parameter depend on the value of the <paramref name="Msg"/> parameter. </param>
        ''' <param name="wParam">[in] Specifies additional message-specific information. The contents of this parameter depend on the value of the <paramref name="Msg"/> parameter. </param>
        ''' <returns>The return value specifies the result of the message processing and depends on the message sent. </returns>
        Public Declare Auto Function CallWindowProc Lib "user32.dll" Alias "CallWindowProc" (ByVal lpPrevWndFunc As IntPtr, ByVal hwnd As IntPtr, ByVal msg As API.Messages.WindowMessages, ByVal wParam As Int32, ByVal lParam As Int32) As Int32
        ''' <summary>Changes the size, position, and Z order of a child, pop-up, or top-level window. These windows are ordered according to their appearance on the screen. The topmost window receives the highest rank and is the first window in the Z order</summary>
        ''' <param name="hWnd">A handle to the window.</param>
        ''' <param name="hWndInsertAfter">A handle to the window to precede the positioned window in the Z order. This parameter must be a window handle or one of the <see cref="SpecialWindowHandles"/> values.</param>
        ''' <param name="X">The new position of the left side of the window, in client coordinates. </param>
        ''' <param name="Y">The new position of the top of the window, in client coordinates. </param>
        ''' <param name="cx">The new width of the window, in pixels. </param>
        ''' <param name="cy">The new height of the window, in pixels. </param>
        ''' <param name="uFlags">    The window sizing and positioning flags.</param>
        ''' <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
        ''' <version version="1.5.3">This DLL-imported function is new in version 1.5.3</version>
        Public Declare Function SetWindowPos Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As SetWindowPosFlags) As Boolean
        ''' <summary>Possible values for <see cref="SetWindowPos"/> <paramref name="uFlags"/> parameter</summary>
        ''' <version version="1.5.3">This enumeration is new in version 1.5.3</version>
        <Flags()>
        Public Enum SetWindowPosFlags As UInteger
            ''' <summary>If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.</summary>
            SWP_ASYNCWINDOWPOS = &H4000
            ''' <summary>Prevents generation of the <see cref="API.Messages.WindowMessages.WM_SYNCPAINT"/> message.</summary>
            SWP_DEFERERASE = &H2000
            ''' <summary>Draws a frame (defined in the window's class description) around the window.</summary>
            SWP_DRAWFRAME = SWP_FRAMECHANGED
            ''' <summary>Applies new frame styles set using the <see cref="SetWindowLong"/> function. Sends a <see cref="API.Messages.WindowMessages.WM_NCCALCSIZE"/> message to the window, even if the window's size is not being changed. If this flag is not specified, <see cref="API.Messages.WindowMessages.WM_NCCALCSIZE"/> is sent only when the window's size is being changed.</summary>
            SWP_FRAMECHANGED = &H20
            ''' <summary>Hides the window.</summary>
            SWP_HIDEWINDOW = &H80
            ''' <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the <paramref name="hWndInsertAfter"/> parameter).</summary>
            SWP_NOACTIVATE = &H10
            ''' <summary>Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.</summary>
            SWP_NOCOPYBITS = &H100
            ''' <summary>Retains the current position (ignores X and Y parameters).</summary>
            SWP_NOMOVE = &H2
            ''' <summary>Does not change the owner window's position in the Z order.</summary>
            SWP_NOOWNERZORDER = &H200
            ''' <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
            SWP_NOREDRAW = &H8
            ''' <summary>Same as the <see cref="SWP_NOOWNERZORDER"/> flag.</summary>
            SWP_NOREPOSITION = SWP_NOOWNERZORDER
            ''' <summary>Prevents the window from receiving the <see cref="API.Messages.WindowMessages.WM_WINDOWPOSCHANGING"/> message.</summary>
            SWP_NOSENDCHANGING = &H400
            ''' <summary>Retains the current size (ignores the cx and cy parameters).</summary>
            SWP_NOSIZE = &H1
            ''' <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
            SWP_NOZORDER = &H4
            ''' <summary>Displays the window.</summary>
            SWP_SHOWWINDOW = &H40
        End Enum

        ''' <summary>Defines various values that can be passed where windows handle is expected</summary>
        ''' <remarks>Value of this enumeration should be cased to <see cref="IntPtr"/> before used as special values for window handles.</remarks>
        ''' <version version="1.5.3">This enumeration is new in version 1.5.3</version>
        Public Enum SpecialWindowHandles
            ''' <summary>Special handle meaning broadcast to all windows</summary>
            HWND_BROADCAST = 65535
            ''' <summary>Places the window at the top of the Z order.</summary>
            HWND_TOP = 0
            ''' <summary>Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.</summary>
            HWND_BOTTOM = 1
            ''' <summary>Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.</summary>
            HWND_TOPMOST = -1
            ''' <summary>Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.</summary>
            HWND_NOTOPMOST = -2
            'HWND_MESSAGE = -3
        End Enum

        ''' <summary>Predefined window longs for <see cref="GetWindowLong"/> and <see cref="SetWindowLong"/></summary>
        ''' <remarks>Publicly visible alternative of this enumeration is <see cref="[Public].WindowLongs"/></remarks>
        Public Enum WindowLongs As Int32
            ''' <summary>Retrieves the extended window styles. For more information, see CreateWindowEx.</summary>
            GWL_EXSTYLE = -20
            ''' <summary>Retrieves the window styles.</summary>
            GWL_STYLE = -16
            ''' <summary>Retrieves the address of the window procedure, or a handle representing the address of the window procedure. You must use the CallWindowProc function to call the window procedure.</summary>
            GWL_WNDPROC = -4
            ''' <summary>Retrieves a handle to the application instance.</summary>
            GWL_HINSTANCE = -6
            ''' <summary>Retrieves a handle to the parent window, if any.</summary>
            GWL_HWNDPARENT = -8
            ''' <summary>Retrieves the identifier of the window.</summary>
            GWL_ID = -12
            ''' <summary>Retrieves the user data associated with the window. This data is intended for use by the application that created the window. Its value is initially zero.</summary>
            GWL_USERDATA = -21
            ''' <summary>Retrieves the address of the dialog box procedure, or a handle representing the address of the dialog box procedure. You must use the CallWindowProc function to call the dialog box procedure.</summary>
            DWL_DLGPROC = 4
            ''' <summary>Retrieves the return value of a message processed in the dialog box procedure.</summary>
            DWL_MSGRESULT = 0
            ''' <summary>Retrieves extra information private to the application, such as handles or pointers.</summary>
            DWL_USER = 8
        End Enum
        ''' <summary>Defines window extended styles</summary>
        ''' <seelaso cref="API.[Public].WindowExtendedStyles"/>
        ''' <version version="1.5.3">This enumeration is new in version 1.5.3</version>
        <Flags()>
        Public Enum ExStyles As Integer
            ''' <summary>The windo accepts drag &amp; drop files</summary>
            WS_EX_ACCEPTFILES = &H10I
            ''' <summary>Forces a top-level window onto the taskbar when the window is visible.</summary>
            WS_EX_APPWINDOW = &H40000I
            ''' <summary>The window has a border with a sunken edge.</summary>
            WS_EX_CLIENTEDGE = &H200I
            ''' <summary>Paints all descendants of a window in bottom-to-top painting order using double-buffering. This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.</summary>
            ''' <remarks>Windows 2000:  This flag is not supported.</remarks>
            WS_EX_COMPOSITED = &H2000000I
            ''' <summary>The title bar of the window includes a question mark. When the user clicks the question mark, the cursor changes to a question mark with a pointer. If the user then clicks a child window, the child receives a <see cref="API.Messages.WindowMessages.WM_HELP"/> message. The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command. The Help application displays a pop-up window that typically contains help for the child window.</summary>
            ''' <remarks><see cref="WS_EX_CONTEXTHELP"/> cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.</remarks>
            WS_EX_CONTEXTHELP = &H400L
            ''' <summary>The window itself contains child windows that should take part in dialog box navigation. If this style is specified, the dialog manager recurses into children of this window when performing navigation operations such as handling the TAB key, an arrow key, or a keyboard mnemonic.</summary>
            WS_EX_CONTROLPARENT = &H10000I
            ''' <summary>The window has a double border; the window can, optionally, be created with a title bar by specifying the WS_CAPTION style in the dwStyle parameter.</summary>
            WS_EX_DLGMODALFRAME = &H1I
            ''' <summary>The window is a layered window. Note that this cannot be used for child windows. Also, this cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.</summary>
            WS_EX_LAYERED = &H80000
            ''' <summary>If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the horizontal origin of the window is on the right edge. Increasing horizontal values advance to the left.</summary>
            WS_EX_LAYOUTRTL = &H400000I
            ''' <summary>The window has generic left-aligned properties. This is the default.</summary>
            WS_EX_LEFT = &H0L
            ''' <summary>If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the vertical scroll bar (if present) is to the left of the client area. For other languages, the style is ignored.</summary>
            WS_EX_LEFTSCROLLBAR = &H4000I
            ''' <summary>The window text is displayed using left-to-right reading-order properties. This is the default.</summary>
            WS_EX_LTRREADING = &H0I
            ''' <summary>The window is a MDI child window.</summary>
            WS_EX_MDICHILD = &H40I
            ''' <summary>A top-level window created with this style does not become the foreground window when the user clicks it. The system does not bring this window to the foreground when the user minimizes or closes the foreground window.</summary>
            ''' <remarks>To activate the window, use the SetActiveWindow or SetForegroundWindow function. The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the <see cref="WS_EX_APPWINDOW"/> style.</remarks>
            WS_EX_NOACTIVATE = &H8000000I
            ''' <summary>The window does not pass its window layout to its child windows.</summary>
            WS_EX_NOINHERITLAYOUT = &H100000I
            ''' <summary>The child window created with this style does not send the <see cref="API.Messages.WindowMessages.WM_PARENTNOTIFY"/> message to its parent window when it is created or destroyed.</summary>
            WS_EX_NOPARENTNOTIFY = &H4I
            ''' <summary>The window is an overlapped window.</summary>
            WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE Or WS_EX_CLIENTEDGE)
            ''' <summary>TBD</summary>
            WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE Or WS_EX_TOOLWINDOW Or WS_EX_TOPMOST)
            ''' <summary>The window has generic "right-aligned" properties. This depends on the window class. This style has an effect only if the shell language is Hebrew, Arabic, or another language that supports reading-order alignment; otherwise, the style is ignored.</summary>
            ''' <remarks>Using the <see cref="WS_EX_RIGHT"/> style for static or edit controls has the same effect as using the SS_RIGHT or ES_RIGHT style, respectively. Using this style with button controls has the same effect as using BS_RIGHT and BS_RIGHTBUTTON styles.</remarks>
            WS_EX_RIGHT = &H1000I
            ''' <summary>The vertical scroll bar (if present) is to the right of the client area. This is the default.</summary>
            WS_EX_RIGHTSCROLLBAR = &H0I
            ''' <summary>If the shell language is Hebrew, Arabic, or another language that supports reading-order alignment, the window text is displayed using right-to-left reading-order properties. For other languages, the style is ignored.</summary>
            WS_EX_RTLREADING = &H2000I
            ''' <summary>The window has a three-dimensional border style intended to be used for items that do not accept user input.</summary>
            WS_EX_STATICEDGE = &H20000I
            ''' <summary>The window is intended to be used as a floating toolbar. A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font. A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB. If a tool window has a system menu, its icon is not displayed on the title bar. However, you can display the system menu by right-clicking or by typing ALT+SPACE.</summary>
            WS_EX_TOOLWINDOW = &H80I
            ''' <summary>The window should be placed above all non-topmost windows and should stay above them, even when the window is deactivated. To add or remove this style, use the SetWindowPos function.</summary>
            WS_EX_TOPMOST = &H8I
            ''' <summary>The window should not be painted until siblings beneath the window (that were created by the same thread) have been painted. The window appears transparent because the bits of underlying sibling windows have already been painted.</summary>
            ''' <remarks>To achieve transparency without these restrictions, use the SetWindowRgn function.</remarks>
            WS_EX_TRANSPARENT = &H20I
            ''' <summary>The window has a border with a raised edge.</summary>
            WS_EX_WINDOWEDGE = &H100I
        End Enum
        ''' <summary>Subset of <see cref="WindowLongs"/> values related to window procedures</summary>
        Public Enum WindowProcs As Int32
            ''' <summary>Retrieves the address of the window procedure, or a handle representing the address of the window procedure. You must use the CallWindowProc function to call the window procedure.</summary>
            GWL_WNDPROC = WindowLongs.GWL_WNDPROC
            ''' <summary>Retrieves the address of the dialog box procedure, or a handle representing the address of the dialog box procedure. You must use the CallWindowProc function to call the dialog box procedure.</summary>
            DWL_DLGPROC = WindowLongs.DWL_DLGPROC
        End Enum
        ''' <summary>The SetWindowText function changes the text of the specified window’s title bar (if it has one). If the specified window is a control, the text of the control is changed.</summary>
        ''' <param name="hwnd">Identifies the window or control whose text is to be changed.</param>
        ''' <param name="lpString">Points to a null-terminated string to be used as the new title or control text.</param>
        ''' <returns>If the function succeeds, the return value is nonzero.
        ''' If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        Public Declare Auto Function SetWindowText Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal lpString As String) As Boolean
        ''' <summary>The GetWindowText function copies the text of the specified window’s title bar (if it has one) into a buffer. If the specified window is a control, the text of the control is copied.</summary>
        ''' <param name="cch">Specifies the maximum number of characters to copy to the buffer, including the NULL character. If the text exceeds this limit, it is truncated.</param>
        ''' <param name="hwnd">Identifies the window or control containing the text.</param>
        ''' <param name="lpString">Points to the buffer that will receive the text.</param>
        ''' <returns>If the function succeeds, the return value is the length, in characters, of the copied string, not including the terminating null character. If the window has no title bar or text, if the title bar is empty, or if the window or control handle is invalid, the return value is zero. To get extended error information, call GetLastError. 
        ''' This function cannot retrieve the text of an edit control in another application. </returns>
        Public Declare Auto Function GetWindowText Lib "user32.dll" (ByVal hwnd As IntPtr, <Out()> ByVal lpString As System.Text.StringBuilder, ByVal cch As Int32) As Int32
        ''' <summary>The GetWindowTextLength function retrieves the length, in characters, of the specified window’s title bar text (if the window has a title bar). If the specified window is a control, the function retrieves the length of the text within the control.</summary>
        ''' <param name="hwnd">Identifies the window or control.</param>
        ''' <returns>If the function succeeds, the return value is the length, in characters, of the text. Under certain conditions, this value may actually be greater than the length of the text. For more information, see the following Remarks section. 
        ''' If the window has no text, the return value is zero. To get extended error information, call GetLastError. </returns>
        Public Declare Auto Function GetWindowTextLength Lib "user32.dll" (ByVal hwnd As IntPtr) As Int32
        ''' <summary>The ScreenToClient function converts the screen coordinates of a specified point on the screen to client-area coordinates.</summary>
        ''' <param name="hwnd">Handle to the window whose client area will be used for the conversion.</param>
        ''' <param name="lpPoint">Pointer to a POINT structure that specifies the screen coordinates to be converted.</param>
        ''' <returns>If the function succeeds, the return value is nonzero.
        ''' If the function fails, the return value is zero. </returns>
        Public Declare Function ScreenToClient Lib "user32.dll" (ByVal hwnd As IntPtr, ByRef lpPoint As POINTAPI) As Boolean
        ''' <summary>The POINT structure defines the x- and y- coordinates of a point.</summary>
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure POINTAPI
            ''' <summary>Specifies the x-coordinate of the point.</summary>
            Public x As Int32
            ''' <summary>Specifies the y-coordinate of the point.</summary>
            Public y As Int32
            ''' <summary>CTor</summary>
            ''' <param name="x">the x-coordinate of the point</param>
            ''' <param name="y">the y-coordinate of the point.</param>
            Public Sub New(ByVal x As Integer, ByVal y As Integer)
                Me.x = x
                Me.y = y
            End Sub
            ''' <summary>Converts <see cref="POINTAPI"/> to <see cref="Point"/></summary>
            ''' <param name="a">A <see cref="POINTAPI"/></param>
            ''' <returns>A <see cref="Point"/></returns>
            Public Shared Widening Operator CType(ByVal a As POINTAPI) As Point
                Return New Point(a.x, a.y)
            End Operator
            ''' <summary>Converts <see cref="Point"/> to <see cref="POINTAPI"/></summary>
            ''' <param name="a">A <see cref="Point"/></param>
            ''' <returns>A <see cref="POINTAPI"/></returns>
            Public Shared Widening Operator CType(ByVal a As Point) As POINTAPI
                Return New POINTAPI(a.X, a.Y)
            End Operator
        End Structure
        ''' <summary>The GetDesktopWindow function returns the handle of the Windows desktop window. The desktop window covers the entire screen. The desktop window is the area on top of which all icons and other windows are painted.</summary>
        ''' <returns>The return value is the handle of the desktop window.</returns>
        Public Declare Function GetDesktopWindow Lib "user32.dll" () As IntPtr
        ''' <summary>The EnumChildWindows function enumerates the child windows that belong to the specified parent window by passing the handle to each child window, in turn, to an application-defined callback function. EnumChildWindows continues until the last child window is enumerated or the callback function returns FALSE.</summary>
        ''' <param name="hWndParent">Identifies the parent window whose child windows are to be enumerated.</param>
        ''' <param name="lpEnumFunc">Points to an application-defined callback function. For more information about the callback function, see the EnumChildProc callback function.</param>
        ''' <param name="lParam">Specifies a 32-bit, application-defined value to be passed to the callback function.</param>
        ''' <returns>If the function succeeds, the return value is nonzero.
        ''' If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        Public Declare Function EnumChildWindows Lib "user32.dll" (ByVal hWndParent As IntPtr, ByVal lpEnumFunc As EnumWindowsProc, ByVal lParam As Int32) As Boolean
        ''' <summary>The EnumChildProc function is an application-defined callback function used with the EnumChildWindows function. It receives the child window handles. The WNDENUMPROC type defines a pointer to this callback function. EnumChildProc is a placeholder for the application-defined function name.</summary>
        ''' <param name="hWnd">Handle to a child window of the parent window specified in EnumChildWindows.</param>
        ''' <param name="lParam">Specifies the application-defined value given in EnumChildWindows.</param>
        ''' <returns>To continue enumeration, the callback function must return TRUE; to stop enumeration, it must return FALSE.</returns>
        Public Delegate Function EnumWindowsProc(ByVal hWnd As IntPtr, ByVal lParam As Integer) As Boolean
        ''' <summary>The EnumWindows function enumerates all top-level windows on the screen by passing the handle of each window, in turn, to an application-defined callback function. EnumWindows continues until the last top-level window is enumerated or the callback function returns FALSE.</summary>
        ''' <param name="lpEnumFunc">Points to an application-defined callback function. For more information, see the EnumWindowsProc callback function.</param>
        ''' <param name="lParam">Specifies a 32-bit, application-defined value to be passed to the callback function.</param>
        ''' <remarks>If the function succeeds, the return value is nonzero.
        ''' If the function fails, the return value is zero. To get extended error information, call GetLastError.</remarks>
        Public Declare Function EnumWindows Lib "user32.dll" (ByVal lpEnumFunc As EnumWindowsProc, ByVal lParam As Int32) As Int32
        ''' <summary>The DefWindowProc function calls the default window procedure to provide default processing for any window messages that an application does not process. This function ensures that every message is processed. DefWindowProc is called with the same parameters received by the window procedure.</summary>
        ''' <param name="hwnd">Identifies the window procedure that received the message.</param>
        ''' <param name="wMsg">Specifies the message.</param>
        ''' <param name="wParam">Specifies additional message information. The content of this parameter depends on the value of the Msg parameter.</param>
        ''' <param name="lParam">Specifies additional message information. The content of this parameter depends on the value of the Msg parameter.</param>
        ''' <returns>The return value is the result of the message processing and depends on the message.</returns>
        Public Declare Auto Function DefWindowProc Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal wMsg As Messages.WindowMessages, ByVal wParam As Int32, ByVal lParam As Int32) As Int32
        ''' <summary>The FindWindow function retrieves a handle to the top-level window whose class name and window name match the specified strings. This function does not search child windows. This function does not perform a case-sensitive search.</summary>
        ''' <param name="lpClassName">[in] Pointer to a null-terminated string that specifies the class name or a class atom created by a previous call to the RegisterClass or RegisterClassEx function. The atom must be in the low-order word of lpClassName; the high-order word must be zero.
        ''' <para>If lpClassName points to a string, it specifies the window class name. The class name can be any name registered with RegisterClass or RegisterClassEx, or any of the predefined control-class names.</para>
        ''' <para>If lpClassName is NULL, it finds any window whose title matches the lpWindowName parameter. </para></param>
        ''' <param name="lpWindowName">[in] Pointer to a null-terminated string that specifies the window name (the window's title). If this parameter is NULL, all window names match.</param>
        ''' <returns>If the function succeeds, the return value is a handle to the window that has the specified class name and window name.
        ''' <para>If the function fails, the return value is NULL. To get extended error information, call GetLastError.</para></returns>
        ''' <remarks>If the lpWindowName parameter is not NULL, FindWindow calls the GetWindowText function to retrieve the window name for comparison. For a description of a potential problem that can arise, see the Remarks for GetWindowText. </remarks>
        Public Declare Auto Function FindWindow Lib "user32.dll" (ByVal lpClassName As String, ByVal lpWindowName As String) As Int32
        ''' <summary>The GetCaretBlinkTime function returns the elapsed time, in milliseconds, required to invert the caret's pixels. The user can set this value using the Control Panel.</summary>
        ''' <returns>If the function succeeds, the return value is the blink time, in milliseconds. 
        ''' If the function fails, the return value is zero. To get extended error information, call GetLastError. </returns>
        Public Declare Function GetCaretBlinkTime Lib "user32.dll" () As Int32

        ''' <summary>Retrieves the name of the class to which the specified window belongs. </summary>
        ''' <param name="hwnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        ''' <param name="lpClassName">The class name string.</param>
        ''' <param name="nMaxCount">The length, in characters, of the buffer pointed to by the <paramref name="lpClassName"/> parameter. The class name string is truncated if it is longer than the buffer and is always null-terminated. </param>
        ''' <returns>If the function succeeds, the return value is the number of characters copied to the specified buffer. If the function fails, the return value is zero.</returns>
        Public Declare Auto Function GetClassName Lib "User32.dll" (ByVal hwnd As IntPtr, <Out()> ByVal lpClassName As System.Text.StringBuilder, ByVal nMaxCount As Integer) As Integer

        ''' <summary>Contains information about a GUI thread.</summary>
        ''' <remarks>This structure is used with the <see cref="GetGUIThreadInfo"/>  function to retrieve information about the active window or a specified GUI thread. </remarks>
        <StructLayout(LayoutKind.Sequential)>
        Public Structure GUITHREADINFO
            ''' <summary>The size of this structure, in bytes. The caller must set this member to sizeof <see cref="GUITHREADINFO"/></summary>
            Public cbSize As Integer
            ''' <summary>The thread state.</summary>
            Public flags As GuiThreadInfoFlags
            ''' <summary>A handle to the active window within the thread. </summary>
            Public hwndActive As IntPtr
            ''' <summary>A handle to the window that has the keyboard focus.</summary>
            Public hwndFocus As IntPtr
            ''' <summary>A handle to the window that has captured the mouse. </summary>
            Public hwndCapture As IntPtr
            ''' <summary>A handle to the window that owns any active menus. </summary>
            Public hwndMenuOwner As IntPtr
            ''' <summary>A handle to the window in a move or size loop. </summary>
            Public hwndMoveSize As IntPtr
            ''' <summary>A handle to the window that is displaying the caret. </summary>
            Public hwndCaret As IntPtr
            ''' <summary>The caret's bounding rectangle, in client coordinates, relative to the window specified by the <see cref="hwndCaret"/> member. </summary>
            Public rcCaret As RECT
        End Structure

        Public Enum GuiThreadInfoFlags As Integer
            GUI_CARETBLINKING = &H1
            GUI_INMENUMODE = &H4
            GUI_INMOVESIZE = &H2
            GUI_POPUPMENUMODE = &H10
            GUI_SYSTEMMENUMODE = &H8
        End Enum

        ''' <summary>Retrieves information about the active window or a specified GUI thread.</summary>
        ''' <param name="idThread">The identifier for the thread for which information is to be retrieved. To retrieve this value, use the GetWindowThreadProcessId  function. If this parameter is zero, the function returns information for the foreground thread. </param>
        ''' <param name="lpgui">A pointer to a <see cref="GUITHREADINFO"/>  structure that receives information describing the thread. Note that you must set the <see cref="GUITHREADINFO.cbSize"/> member to sizeof <see cref="GUITHREADINFO"/>  before calling this function.</param>
        ''' <remarks>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</remarks>
        Public Declare Auto Function GetGUIThreadInfo Lib "User32.dll" (ByVal idThread As Integer, <[In](), Out()> ByRef lpgui As GUITHREADINFO) As Boolean

        ''' <summary>Retrieves the identifier of the thread that created the specified window and, optionally, the identifier of the process that created the window.</summary>
        ''' <param name="hWnd">A handle to the window.</param>
        ''' <param name="processId">A pointer to a variable that receives the process identifier. If this parameter is not zero, <see cref="GetWindowThreadProcessId"/> copies the identifier of the process to the variable; otherwise, it does not.</param>
        ''' <returns>The return value is the identifier of the thread that created the window.</returns>
        Public Declare Auto Function GetWindowThreadProcessId Lib "user32.dll" (ByVal hWnd As IntPtr, <Out()> Optional ByRef processId As Integer = 0) As Integer

        ''' <summary>Brings the thread that created the specified window into the foreground and activates the window. Keyboard input is directed to the window, and various visual cues are changed for the user. The system assigns a slightly higher priority to the thread that created the foreground window than it does to other threads. </summary>
        ''' <param name="hwnd">A handle to the window that should be activated and brought to the foreground. </param>
        ''' <returns>If the window was brought to the foreground, the return value is nonzero.
        ''' If the window was not brought to the foreground, the return value is zero.</returns>
        Public Declare Ansi Function SetForegroundWindow Lib "user32.dll" Alias "SetForegroundWindow" (ByVal hwnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Module
End Namespace