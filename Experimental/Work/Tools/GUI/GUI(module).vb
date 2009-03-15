Imports System.Runtime.InteropServices
'From ÐTools http://codeplex.com/DTools
Namespace API
    ''' <summary>Contains declarations of Win32 API related to GUI</summary>
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

        ''' <summary>inserts a new menu item at the specified position in a menu.</summary>
        ''' <param name="hMenu">Handle to the menu in which the new menu item is inserted. </param>
        ''' <param name="fByPosition">[in] Value specifying the meaning of uItem. If this parameter is FALSE, uItem is a menu item identifier. Otherwise, it is a menu item position. See Menu Programming Considerations for more information.</param>
        ''' <param name="uItem">[in] Identifier or position of the menu item before which to insert the new item. The meaning of this parameter depends on the value of fByPosition. </param>
        ''' <param name="lpmii">[in] Pointer to a MENUITEMINFO structure that contains information about the new menu item. </param>
        ''' <returns>If the function succeeds, the return value is nonzero.</returns>
        Public Declare Auto Function InsertMenuItem Lib "user32.dll" (ByVal hMenu As Int32, ByVal uItem As Int32, ByVal fByPosition As Boolean, ByRef lpmii As MENUITEMINFO) As Int32


        'Public Declare Function GetMenuInfo Lib "user32.dll" (ByVal hmenu As Int32, ByRef LPMENUINFO As MENUINFO) As Int32

        ''' <summary>contains information about a menu item. </summary>
        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
        Public Structure MENUITEMINFO
            ''' <summary>Size of structure, in bytes. The caller must set this to sizeof(MENUITEMINFO). </summary>
            Public cbSize As Integer
            ''' <summary>Members to retrieve or set.</summary>
            Public fMask As MenuMember
            ''' <summary>Menu item type.</summary>
            ''' <remarks>The MFT_BITMAP, MFT_SEPARATOR, and MFT_STRING values cannot be combined with one another. Set fMask to MIIM_TYPE to use fType.</remarks>
            Public fType As MenuItemType
            ''' <summary>Menu item state. </summary>
            Public fState As MenuState
            ''' <summary>Application-defined 16-bit value that identifies the menu item. Set fMask to MIIM_ID to use wID.</summary>
            Public wID As Integer
            ''' <summary>Handle to the drop-down menu or submenu associated with the menu item. If the menu item is not an item that opens a drop-down menu or submenu, this member is NULL. Set fMask to MIIM_SUBMENU to use hSubMenu.</summary>
            Public hSubMenu As IntPtr
            ''' <summary>Handle to the bitmap to display next to the item if it is selected. If this member is NULL, a default bitmap is used. If the MFT_RADIOCHECK type value is specified, the default bitmap is a bullet. Otherwise, it is a check mark. Set fMask to MIIM_CHECKMARKS to use hbmpChecked.</summary>
            Public hbmpChecked As IntPtr
            ''' <summary>Handle to the bitmap to display next to the item if it is not selected. If this member is NULL, no bitmap is used. Set fMask to MIIM_CHECKMARKS to use hbmpUnchecked. </summary>
            Public hbmpUnchecked As IntPtr
            ''' <summary>Application-defined value associated with the menu item. Set fMask to MIIM_DATA to use dwItemData.</summary>
            Public dwItemData As IntPtr
            ''' <summary>Content of the menu item. The meaning of this member depends on the value of fType and is used only if the MIIM_TYPE flag is set in the fMask member.
            ''' <para>To retrieve a menu item of type MFT_STRING, first find the size of the string by setting the dwTypeData member of MENUITEMINFO to NULL and then calling GetMenuItemInfo. The value of cch+1 is the size needed. Then allocate a buffer of this size, place the pointer to the buffer in dwTypeData, increment cch, and call GetMenuItemInfo once again to fill the buffer with the string. If the retrieved menu item is of some other type, then GetMenuItemInfo sets the dwTypeData member to a value whose type is specified by the fType member.</para>
            ''' <para>When using with the SetMenuItemInfo function, this member should contain a value whose type is specified by the fType member.</para>
            ''' <para>Windows 98/Me and Windows 2000/XP: dwTypeData is used only if the MIIM_STRING flag is set in the fMask member. </para></summary>
            Public dwTypeData As String
            ''' <summary>Length of the menu item text, in TCHARs, when information is received about a menu item of the MFT_STRING type. However, cch is used only if the MIIM_TYPE flag is set in the fMask member and is zero otherwise. Also, cch is ignored when the content of a menu item is set by calling SetMenuItemInfo. Note that, before calling GetMenuItemInfo, the application must set cch to the length of the buffer pointed to by the dwTypeData member. If the retrieved menu item is of type MFT_STRING (as indicated by the fType member), then GetMenuItemInfo changes cch to the length of the menu item text. If the retrieved menu item is of some other type, GetMenuItemInfo sets the cch field to zero. </summary>
            Public cch As Integer
        End Structure
        ''' <summary>Menu item states</summary>
        <Flags()> Public Enum MenuState As Integer
            ''' <summary>Checks the menu item. For more information about selected menu items, see the hbmpChecked member.</summary>
            CHECKED = MenuFlags.CHECKED
            ''' <summary>Specifies that the menu item is the default. A menu can contain only one default menu item, which is displayed in bold.</summary>
            [DEFAULT] = MenuFlags.DEFAULT
            ''' <summary>Disables the menu item and grays it so that it cannot be selected. This is equivalent to MFS_GRAYED.</summary>
            DISABLED = GRAYED
            ''' <summary>Enables the menu item so that it can be selected. This is the default state.</summary>
            ENABLED = MenuFlags.ENABLED
            ''' <summary>Disables the menu item and grays it so that it cannot be selected. This is equivalent to MFS_DISABLED.</summary>
            GRAYED = &H3I
            ''' <summary>Highlights the menu item.</summary>
            HILITE = MenuFlags.HILITE
            ''' <summary>Unchecks the menu item. For more information about clear menu items, see the hbmpChecked member.</summary>
            UNCHECKED = MenuFlags.UNCHECKED
            ''' <summary>Removes the highlight from the menu item. This is the default state.</summary>
            UNHILITE = MenuFlags.UNHILITE
        End Enum
        ''' <summary>Identifes menu property</summary>
        <Flags()> _
        Public Enum MenuMember As Integer
            ''' <summary>Retrieves or sets the fType and dwTypeData members. Windows 98/Me, Windows 2000/XP: MIIM_TYPE is replaced by MIIM_BITMAP, MIIM_FTYPE, and MIIM_STRING.</summary>
            TYPE = &H10
            ''' <summary>Retrieves or sets the hSubMenu member.</summary>
            SUBMENU = &H4
            ''' <summary>Windows 98/Windows Me, Windows 2000/Windows XP: Retrieves or sets the dwTypeData member.</summary>
            [STRING] = &H40
            ''' <summary>Retrieves or sets the fState member.</summary>
            STATE = &H1
            ''' <summary>Retrieves or sets the wID member.</summary>
            ID = &H2
            ''' <summary>Windows 98/Windows Me, Windows 2000/Windows XP: Retrieves or sets the fType member.</summary>
            FTYPE = &H100
            ''' <summary>Retrieves or sets the hbmpChecked and hbmpUnchecked members.</summary>
            CHECKMARKS = &H8
            ''' <summary>Microsoft Windows 98/Windows Millennium Edition (Windows Me), Windows 2000/Windows XP: Retrieves or sets the hbmpItem member.</summary>
            BITMAP = &H80
            ''' <summary>Retrieves or sets the dwItemData member.</summary>
            DATA = &H20
        End Enum
        ''' <summary>Menu item types</summary>
        <Flags()> Public Enum MenuItemType As Integer
            ''' <summary>Displays the menu item using a bitmap. The low-order word of the dwTypeData member is the bitmap handle, and the cch member is ignored.</summary>
            BITMAP = MenuFlags.BITMAP
            ''' <summary>Places the menu item on a new line (for a menu bar) or in a new column (for a drop-down menu, submenu, or shortcut menu). For a drop-down menu, submenu, or shortcut menu, a vertical line separates the new column from the old.</summary>
            MENUBARBREAK = MenuFlags.MENUBARBREAK
            ''' <summary>Places the menu item on a new line (for a menu bar) or in a new column (for a drop-down menu, submenu, or shortcut menu). For a drop-down menu, submenu, or shortcut menu, the columns are not separated by a vertical line.</summary>
            MENUBREAK = MenuFlags.MENUBREAK
            ''' <summary>Assigns responsibility for drawing the menu item to the window that owns the menu. The window receives a WM_MEASUREITEM message before the menu is displayed for the first time, and a WM_DRAWITEM message whenever the appearance of the menu item must be updated. If this value is specified, the dwTypeData member contains an application-defined value.</summary>
            OWNERDRAW = MenuFlags.OWNERDRAW
            ''' <summary>Displays selected menu items using a radio-button mark instead of a check mark if the hbmpChecked member is NULL.</summary>
            RADIOCHECK = &H200I
            ''' <summary>Right-justifies the menu item and any subsequent items. This value is valid only if the menu item is in a menu bar.</summary>
            RIGHTJUSTIFY = MenuFlags.RIGHTJUSTIFY
            ''' <summary>Windows 95/98/Me, Windows 2000/XP: Specifies that menus cascade right-to-left (the default is left-to-right). This is used to support right-to-left languages, such as Arabic and Hebrew.</summary>
            RIGHTORDER = &H2000I
            ''' <summary>Specifies that the menu item is a separator. A menu item separator appears as a horizontal dividing line. The dwTypeData and cch members are ignored. This value is valid only in a drop-down menu, submenu, or shortcut menu.</summary>
            SEPARATOR = MenuFlags.SEPARATOR
            ''' <summary>Displays the menu item using a text string. The dwTypeData member is the pointer to a null-terminated string, and the cch member is the length of the string.</summary>
            [STRING] = MenuFlags.STRING
        End Enum

        <Flags()> Private Enum MenuFlags As Integer
            BITMAP = &H4I
            MENUBARBREAK = &H20I
            MENUBREAK = &H40I
            OWNERDRAW = &H100I
            RIGHTJUSTIFY = &H4000I
            SEPARATOR = &H800I
            [STRING] = &H0I

            CHECKED = &H8I
            [DEFAULT] = &H1000I
            ENABLED = &H0I
            HILITE = &H80I
            UNCHECKED = &H0I
            UNHILITE = &H0I
        End Enum

    End Module

    ''' <summary>Various messages used by this library</summary>
    Friend Enum Messages As Int32
        ''' <summary>System command</summary>
        WM_SYSCOMMAND = &H112
        WM_ACTIVATE = &H6
        WM_SIZE = &H5
        WM_SYNCPAINT = &H88
        WM_NCCREATE = &H81
        WM_NCPAINT = &H85
        WM_NCACTIVATE = &H86
        WM_PAINT = &HF
        WM_NCHITTEST = &H84
    End Enum

    ''' <summary>Generic exception caused by Win32 API</summary>
    Public Class Win32APIException : Inherits System.ComponentModel.Win32Exception
        ''' <summary>CTor with error number</summary>
        ''' <param name="Number">Error number</param>
        Public Sub New(ByVal Number As Integer)
            MyBase.new(Number)
        End Sub
        ''' <summary>CTor - error number will be obtained automatically via <see cref="GetLastError"/></summary>
        Public Sub New()
            MyBase.new()
        End Sub
    End Class


    ''' <summary>Common Win32 API declarations</summary>
    Friend Module Common
        ''' <summary>Value representing NULL</summary>
        Public Const NULL As Integer = 0
        ''' <summary>Boolean type as used in Win32 API</summary>
        Public Enum APIBool As Integer
            ''' <summary>True</summary>
            [TRUE] = 1
            ''' <summary>False</summary>
            [FALSE] = 0
        End Enum

        ''' <summary>Retrieves the calling thread's last-error code value. The last-error code is maintained on a per-thread basis. Multiple threads do not overwrite each other's last-error code.</summary>
        ''' <returns>The return value is the calling thread's last-error code.
        ''' <para>The Return Value section of the documentation for each function that sets the last-error code notes the conditions under which the function sets the last-error code. Most functions that set the thread's last-error code set it when they fail. However, some functions also set the last-error code when they succeed. If the function is not documented to set the last-error code, the value returned by this function is simply the most recent last-error code to have been set; some functions set the last-error code to 0 on success and others do not.</para>
        ''' <para>Windows Me/98/95:  Functions that are actually implemented in 16-bit code do not set the last-error code. You should ignore the last-error code when you call these functions. They include window management functions, GDI functions, and Multimedia functions. For functions that do set the last-error code, you should not rely on GetLastError returning the same value as it does under other versions of Windows.</para></returns>
        ''' <remarks>Visual Basic:  Applications should call <see cref="ErrObject.LastDllError"/> of <see cref="err"/> instead of <see cref="GetLastError"/>.</remarks>
        Public Declare Function GetLastError Lib "kernel32" () As Integer
        ''' <summary>Formats a message string. The function requires a message definition as input. The message definition can come from a buffer passed into the function. It can come from a message table resource in an already-loaded module. Or the caller can ask the function to search the system's message table resource(s) for the message definition. The function finds the message definition in a message table resource based on a message identifier and a language identifier. The function copies the formatted message text to an output buffer, processing any embedded insert sequences if requested.</summary>
        ''' <param name="dwFlags">The formatting options, and how to interpret the lpSource parameter. The low-order byte of dwFlags specifies how the function handles line breaks in the output buffer. The low-order byte can also specify the maximum width of a formatted output line. This parameter can be one or more of enumerated values. If the low-order byte is a nonzero value other than FORMAT_MESSAGE_MAX_WIDTH_MASK, it specifies the maximum number of characters in an output line. The function ignores regular line breaks in the message definition text. The function never splits a string delimited by white space across a line break. The function stores hard-coded line breaks in the message definition text into the output buffer. Hard-coded line breaks are coded with the %n escape sequence.</param>
        ''' <param name="lpSource">The location of the message definition. The type of this parameter depends upon the settings in the dwFlags parameter. 
        ''' <para><see cref="FormatMessageFlags.FORMAT_MESSAGE_FROM_HMODULE"/>: A handle to the module that contains the message table to search.</para>
        ''' <para><see cref="FormatMessageFlags.FORMAT_MESSAGE_FROM_STRING"/>: Pointer to a string that consists of unformatted message text. It will be scanned for inserts and formatted accordingly.</para></param>
        ''' <param name="dwMessageId">The message identifier for the requested message. This parameter is ignored if dwFlags includes FORMAT_MESSAGE_FROM_STRING. </param>
        ''' <param name="dwLanguageId">The language identifier for the requested message. This parameter is ignored if dwFlags includes FORMAT_MESSAGE_FROM_STRING. 
        ''' If you pass a specific LANGID in this parameter, FormatMessage will return a message for that LANGID only. If the function cannot find a message for that LANGID, it returns ERROR_RESOURCE_LANG_NOT_FOUND. If you pass in zero, FormatMessage looks for a message for LANGIDs in the following order:
        ''' <list><item>Language neutral</item>
        ''' <item>Thread LANGID, based on the thread's locale value</item> 
        ''' <item>User default LANGID, based on the user's default locale value</item> 
        ''' <item>System default LANGID, based on the system default locale value</item> 
        ''' <item>US English</item> 
        ''' </list>
        ''' If FormatMessage does not locate a message for any of the preceding LANGIDs, it returns any language message string that is present. If that fails, it returns ERROR_RESOURCE_LANG_NOT_FOUND.
        ''' </param>
        ''' <param name="lpBuffer">[out] A pointer to a buffer that receives the null-terminated string that specifies the formatted message. If dwFlags includes <see cref="FormatMessageFlags.FORMAT_MESSAGE_ALLOCATE_BUFFER"/>, the function allocates a buffer using the LocalAlloc function, and places the pointer to the buffer at the address specified in lpBuffer. This buffer cannot be larger than 64K bytes.</param>
        ''' <param name="nSize">If the <see cref="FormatMessageFlags.FORMAT_MESSAGE_ALLOCATE_BUFFER"/> flag is not set, this parameter specifies the size of the output buffer, in TCHARs. If FORMAT_MESSAGE_ALLOCATE_BUFFER is set, this parameter specifies the minimum number of TCHARs to allocate for an output buffer. The output buffer cannot be larger than 64K bytes.</param>
        ''' <param name="Arguments">An array of values that are used as insert values in the formatted message. A %1 in the format string indicates the first value in the Arguments array; a %2 indicates the second argument; and so on.  The interpretation of each value depends on the formatting information associated with the insert in the message definition. The default is to treat each value as a pointer to a null-terminated string. By default, the Arguments parameter is of type va_list*, which is a language- and implementation-specific data type for describing a variable number of arguments. The state of the va_list argument is undefined upon return from the function. If the caller is to use the va_list again, it must destroy the variable argument list pointer using va_end and reinitialize it with va_start. If you do not have a pointer of type va_list*, then specify the FORMAT_MESSAGE_ARGUMENT_ARRAY flag and pass a pointer to an array of DWORD_PTR values; those values are input to the message formatted as the insert values. Each insert must have a corresponding element in the array.</param>
        ''' <returns>If the function succeeds, the return value is the number of TCHARs stored in the output buffer, excluding the terminating null character. If the function fails, the return value is zero. To get extended error information, call <see cref="GetLastError"/>.</returns>
        ''' <remarks>Within the message text, several escape sequences are supported for dynamically formatting the message. These escape sequences and their meanings are shown in the following table. All escape sequences start with the percent character (%).</remarks>
        Public Declare Function FormatMessage Lib "kernel32" Alias "FormatMessageA" (ByVal dwFlags As Integer, ByRef lpSource As Integer, ByVal dwMessageId As Integer, ByVal dwLanguageId As Languages, ByVal lpBuffer As String, ByVal nSize As Integer, ByRef Arguments As Integer) As Integer
        ''' <summary>Flags for the <see cref="FormatMessage"/> function</summary>
        ''' <remarks>If the low-order byte is a nonzero value other than <see cref="FormatMessageFlags.FORMAT_MESSAGE_MAX_WIDTH_MASK"/>, it specifies the maximum number of characters in an output line. The function ignores regular line breaks in the message definition text. The function never splits a string delimited by white space across a line break. The function stores hard-coded line breaks in the message definition text into the output buffer. Hard-coded line breaks are coded with the %n escape sequence.</remarks>
        Public Enum FormatMessageFlags As Integer
            ''' <summary>The lpBuffer parameter is a pointer to a PVOID pointer, and that the nSize parameter specifies the minimum number of TCHARs to allocate for an output message buffer. The function allocates a buffer large enough to hold the formatted message, and places a pointer to the allocated buffer at the address specified by lpBuffer. The caller should use the LocalFree function to free the buffer when it is no longer needed.</summary>
            FORMAT_MESSAGE_ALLOCATE_BUFFER = &H100I
            ''' <summary>Arguments parameter is not a va_list structure, but is a pointer to an array of values that represent the arguments. This flag cannot be used with 64-bit integer values. If you are using a 64-bit integer, you must use the va_list structure.</summary>
            FORMAT_MESSAGE_ARGUMENT_ARRAY = &H2000I
            ''' <summary>The lpSource parameter is a module handle containing the message-table resource(s) to search. If this lpSource handle is NULL, the current process's application image file will be searched. Cannot be used with FORMAT_MESSAGE_FROM_STRING. If the module has no message table resource, the function fails with ERROR_RESOURCE_TYPE_NOT_FOUND.</summary>
            FORMAT_MESSAGE_FROM_HMODULE = &H800I
            ''' <summary>The lpSource parameter is a pointer to a null-terminated message definition. The message definition may contain insert sequences, just as the message text in a message table resource may. Cannot be used with FORMAT_MESSAGE_FROM_HMODULE or FORMAT_MESSAGE_FROM_SYSTEM.</summary>
            FORMAT_MESSAGE_FROM_STRING = &H400I
            ''' <summary>The function should search the system message-table resource(s) for the requested message. If this flag is specified with FORMAT_MESSAGE_FROM_HMODULE, the function searches the system message table if the message is not found in the module specified by lpSource. Cannot be used with FORMAT_MESSAGE_FROM_STRING. If this flag is specified, an application can pass the result of the GetLastError function to retrieve the message text for a system-defined error.</summary>
            FORMAT_MESSAGE_FROM_SYSTEM = &H1000I
            ''' <summary>Insert sequences in the message definition are to be ignored and passed through to the output buffer unchanged. This flag is useful for fetching a message for later formatting. If this flag is set, the Arguments parameter is ignored.</summary>
            FORMAT_MESSAGE_IGNORE_INSERTS = &H200I
            ''' <summary>There are no output line width restrictions. The function stores line breaks that are in the message definition text into the output buffer.</summary>
            Zero = 0I
            ''' <summary>The function ignores regular line breaks in the message definition text. The function stores hard-coded line breaks in the message definition text into the output buffer. The function generates no new line breaks.</summary>
            FORMAT_MESSAGE_MAX_WIDTH_MASK = &HFFI
        End Enum
        ''' <summary>Gets inforemation about error in last API call</summary>
        ''' <returns>Description of error</returns>
        Public Function LastDllErrorInfo() As String
            Return LastDllErrorInfo(GetLastError)
        End Function
        ''' <summary>Gets information about error with specified number</summary>
        ''' <param name="ErrN">Number of error</param>
        ''' <returns>Description of error</returns>
        Public Function LastDllErrorInfo(ByVal ErrN As Integer) As String
            Dim Buffer As String
            Buffer = Space(200)
            FormatMessage(FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM, 0, ErrN, Languages.LANG_NEUTRAL, Buffer, 200, 0)
            Return Buffer.Trim
        End Function

        ''' <summary>Langauage constants used by various API functions</summary>
        Public Enum Languages As Short
            ''' <summary>Neutral language</summary>
            LANG_NEUTRAL = &H0S
            ''' <summary>Default sublanguage</summary>
            SUBLANG_DEFAULT = &H1S
        End Enum

     


    End Module
End Namespace