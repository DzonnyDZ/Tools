Imports System.Runtime.InteropServices

Namespace API
    ''' <summary>Contains declarations of Win32 API related to GUI</summary>
    <DoNotApplyAuthorAndVersionAttributes()> _
    Friend Module GUI
        ''' <summary>The <see cref="GetSystemMenu"/> function allows the application to access the window menu (also known as the System menu or the Control menu) for copying and modifying.</summary>
        ''' <param name="hwnd">Identifies the window that will own a copy of the window menu.</param>
        ''' <param name="revert">
        ''' Specifies the action to be taken. If this parameter is <see cref="APIBool.FALSE"/>, <see cref="GetSystemMenu"/> returns the handle of the copy of the window menu currently in use. The copy is initially identical to the window menu, but it can be modified.
        ''' If this parameter is <see cref="APIBool.TRUE"/>, <see cref="GetSystemMenu"/> resets the window menu back to the Windows default state. The previous window menu, if any, is destroyed.</param>
        ''' <returns>If the <paramref name="bRevert"/> parameter is <see cref="APIBool.FALSE"/>, the return value is the handle of a copy of the window menu. If the <paramref name="bRevert"/> parameter is <see cref="APIBool.TRUE"/>, the return value is <see cref="NULL"/>. </returns>
        Public Declare Function GetSystemMenu Lib "user32" (ByVal hwnd As Integer, ByVal revert As APIBool) As Integer
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
        Public Declare Function EnableMenuItem Lib "user32" (ByVal menu As Integer, ByVal ideEnableItem As Integer, ByVal enable As enmEnableMenuItemStatus) As enmPreviousMenuItemStatus
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

        ''' <summary>Notifications connected with windows state changing passed to <see cref="System.Windows.Forms.Form.WndProc"/> with the <see cref="Messages.WM_SYSCOMMAND"/> message</summary>
        Public Enum WindowStateChangedNotifications As Int32
            ''' <summary>Windows size has been restored</summary>
            SC_RESTORE = &HF120
            ''' <summary>Window has been maximized</summary>
            SC_MAXIMIZE = &HF030I
            ''' <summary>Window has been minimized</summary>
            SC_MINIMIZE = &HF020I
        End Enum
        ''' <summary>The <see cref="RemoveMenu"/> function deletes a menu item or detaches a submenu from the specified menu. If the menu item opens a drop-down menu or submenu, <see cref="RemoveMenu"/> does not destroy the menu or its handle, allowing the menu to be reused. Before this function is called, the GetSubMenu function should retrieve a handle to the drop-down menu or submenu. </summary>
        ''' <param name="hMenu">Handle to the menu to be changed. </param>
        ''' <param name="nPosition">Specifies the menu item to be deleted, as determined by the <paramref name="wFlags"/> parameter. </param>
        ''' <param name="wFlags">Specifies how the <paramref name="nPosition"/> parameter is interpreted. This parameter must be one of the following values: <see cref="enmSelectMenuMethod.MF_BYCOMMAND"/> or <see cref="enmSelectMenuMethod.MF_BYPOSITION"/></param>
        ''' <returns>If the function succeeds, the return value is nonzero.
        ''' If the function fails, the return value is zero. To get extended error information, call GetLastError.
        ''' </returns>
        ''' <remarks>The application must call the <see cref="DrawMenuBar"/> function whenever a menu changes, whether or not the menu is in a displayed window.</remarks>
        Public Declare Function RemoveMenu Lib "user32.dll" (ByVal hMenu As Int32, ByVal nPosition As Int32, ByVal wFlags As enmSelectMenuMethod) As Int32
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
        Public Declare Function DrawMenuBar Lib "user32.dll" (ByVal hwnd As Int32) As Int32
        ''' <summary>The GetWindowRect function retrieves the dimensions of the bounding rectangle of the specified window. The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.</summary>
        ''' <param name="hwnd">Identifies the window.</param>
        ''' <param name="lpRect">Points to a RECT structure that receives the screen coordinates of the upper-left and lower-right corners of the window.</param>
        ''' <returns>If the function succeeds, the return value is nonzero.
        ''' If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        Public Declare Function GetWindowRect Lib "user32.dll" (ByVal hwnd As Int32, ByRef lpRect As RECT) As Boolean
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
            Shared Widening Operator CType(ByVal a As Rectangle) As RECT
                Return New RECT(a.Left, a.Top, a.Right, a.Bottom)
            End Operator
            ''' <summary>Converts <see cref="RECT"/> to <see cref="Rectangle"/></summary>
            ''' <param name="a">A <see cref="RECT"/></param>
            ''' <returns><see cref="Rectangle"/> equivalent to <paramref name="a"/></returns>
            Shared Widening Operator CType(ByVal a As RECT) As Rectangle
                Return New Rectangle(a.Left, a.Top, a.Right - a.Left, a.Bottom - a.Top)
            End Operator
        End Structure
        ''' <summary>The SetParent function changes the parent window of the specified child window.</summary>
        ''' <param name="hWndChild">Identifies the child window.</param>
        ''' <param name="hWndNewParent">Identifies the new parent window. If this parameter is NULL, the desktop window becomes the new parent window.</param>
        ''' <returns>If the function succeeds, the return value is nonzero.
        ''' If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        Public Declare Function SetParent Lib "user32.dll" (ByVal hWndChild As Int32, ByVal hWndNewParent As Int32) As Boolean
        ''' <summary>The SetWindowLong function changes an attribute of the specified window. The function also sets a 32-bit (long) value at the specified offset into the extra window memory of a window.</summary>
        ''' <param name="hwnd">Identifies the window and, indirectly, the class to which the window belongs.</param>
        ''' <param name="nIndex">Specifies the zero-based offset to the value to be set. Valid values are in the range zero through the number of bytes of extra window memory, minus 4; for example, if you specified 12 or more bytes of extra memory, a value of 8 would be an index to the third 32-bit integer.</param>
        ''' <param name="dwNewLong">Specifies the replacement value.</param>
        ''' <returns>If the function succeeds, the return value is the previous value of the specified 32-bit integer. </returns>
        Public Declare Auto Function SetWindowLong Lib "user32.dll" (ByVal hwnd As Int32, ByVal nIndex As WindowLongs, ByVal dwNewLong As Int32) As Int32
        ''' <summary>The GetWindowLong function retrieves information about the specified window. The function also retrieves the 32-bit (long) value at the specified offset into the extra window memory of a window.</summary>
        ''' <param name="hwnd">Identifies the window and, indirectly, the class to which the window belongs.</param>
        ''' <param name="nIndex">Specifies the zero-based offset to the value to be retrieved. Valid values are in the range zero through the number of bytes of extra window memory, minus four; for example, if you specified 12 or more bytes of extra memory, a value of 8 would be an index to the third 32-bit integer.</param>
        ''' <returns>If the function succeeds, the return value is the requested 32-bit value. 
        ''' If the function fails, the return value is zero. To get extended error information, call GetLastError. </returns>
        Public Declare Auto Function GetWindowLong Lib "user32.dll" (ByVal hwnd As Int32, ByVal nIndex As WindowLongs) As Int32
        ''' <summary>Predefined window longs for <see cref="GetWindowLong"/> and <see cref="SetWindowLong"/></summary>
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
    End Module
End Namespace
