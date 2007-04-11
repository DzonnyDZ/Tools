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
    End Module
End Namespace
