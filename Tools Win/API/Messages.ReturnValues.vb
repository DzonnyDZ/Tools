Imports System.ComponentModel, Tools.ExtensionsT
#If Config <= Nightly Then 'Stage: Nightly
Namespace API.Messages.ReturnValues
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
    ''' <summary>Used for hight word of return value of <see cref="WindowMessages.WM_GETHOTKEY"/> message. It is also used as <see cref="Short"/> for high-order word of wParam of the <see cref="WindowMessages.WM_SETHOTKEY"/> message.</summary>
    ''' <remarks>You must left-shift by 24 bits this value before it can be used as return value, or use <see cref="WM_GETHOTKEY_high_shifted"/> that can be directly or-ed with virtual key code. For <see cref="WindowMessages.WM_SETHOTKEY"/>'s wParam it must be left-shifted by 16 converted to <see cref="Integer"/> and then or-ed with low-order word of wParam.</remarks>
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
    ''' <summary>Values used as return values for the <see cref="WindowMessages.WM_NCCALCSIZE"/> message</summary>
    <Flags()> Public Enum WM_NCCALCSIZE As Integer
        ''' <summary>A zero (0)</summary>
        zero = 0
        ''' <summary>Specifies that the client area of the window is to be preserved and aligned with the top of the new position of the window. For example, to align the client area to the upper-left corner, return the WVR_ALIGNTOP and WVR_ALIGNLEFT values.</summary>
        WVR_ALIGNTOP = &H10
        ''' <summary>	Specifies that the client area of the window is to be preserved and aligned with the right side of the new position of the window. For example, to align the client area to the lower-right corner, return the WVR_ALIGNRIGHT and WVR_ALIGNBOTTOM values.</summary>
        WVR_ALIGNRIGHT = &H80
        ''' <summary>Specifies that the client area of the window is to be preserved and aligned with the bottom of the new position of the window. For example, to align the client area to the top-left corner, return the WVR_ALIGNTOP and WVR_ALIGNLEFT values.</summary>
        WVR_ALIGNLEFT = &H20
        ''' <summary>Specifies that the client area of the window is to be preserved and aligned with the bottom of the new position of the window. For example, to align the client area to the top-left corner, return the WVR_ALIGNTOP and WVR_ALIGNLEFT values.</summary>
        WVR_ALIGNBOTTOM = &H40
        ''' <summary>Used in combination with any other values, except WVR_VALIDRECTS, causes the window to be completely redrawn if the client rectangle changes size horizontally. This value is similar to CS_HREDRAW class style</summary>
        WVR_HREDRAW = &H100
        ''' <summary>Used in combination with any other values, except WVR_VALIDRECTS, causes the window to be completely redrawn if the client rectangle changes size vertically. This value is similar to CS_VREDRAW class style</summary>
        WVR_VREDRAW = &H200
        ''' <summary>This value causes the entire window to be redrawn. It is a combination of WVR_HREDRAW and WVR_VREDRAW values.</summary>
        WVR_REDRAW = WVR_HREDRAW Or WVR_VREDRAW
        ''' <summary>This value indicates that, upon return from WM_NCCALCSIZE, the rectangles specified by the rgrc[1] and rgrc[2] members of the NCCALCSIZE_PARAMS structure contain valid destination and source area rectangles, respectively. The system combines these rectangles to calculate the area of the window to be preserved. The system copies any part of the window image that is within the source rectangle and clips the image to the destination rectangle. Both rectangles are in parent-relative or screen-relative coordinates. This flag cannot be combined with any other flags.</summary>
        ''' <remarks>This return value allows an application to implement more elaborate client-area preservation strategies, such as centering or preserving a subset of the client area.</remarks>
        WVR_VALIDRECTS = &H400
    End Enum
    ''' <summary>Values used as return values for the <see cref="WindowMessages.WM_NCHITTEST"/> message</summary>
    Public Enum WM_NCHITTEST
        ''' <summary>	In the border of a window that does not have a sizing border.</summary>
        HTBORDER = 18
        ''' <summary>In the lower-horizontal border of a resizable window (the user can click the mouse to resize the window vertically).</summary>
        HTBOTTOM = 15
        ''' <summary>In the lower-left corner of a border of a resizable window (the user can click the mouse to resize the window diagonally).</summary>
        HTBOTTOMLEFT = 16
        ''' <summary>In the lower-right corner of a border of a resizable window (the user can click the mouse to resize the window diagonally).</summary>
        HTBOTTOMRIGHT = 17
        ''' <summary>	In a title bar.</summary>
        HTCAPTION = 2
        ''' <summary>	In a client area.</summary>
        HTCLIENT = 1
        ''' <summary>	In a Close button.</summary>
        HTCLOSE = 20
        ''' <summary>On the screen background or on a dividing line between windows (same as HTNOWHERE, except that the DefWindowProc function produces a system beep to indicate an error).</summary>
        HTERROR = -2
        ''' <summary>	In a size box (same as HTSIZE).</summary>
        HTGROWBOX = 4
        ''' <summary>In a Help button.</summary>
        HTHELP = 21
        ''' <summary>In a horizontal scroll bar.</summary>
        HTHSCROLL = 6
        ''' <summary>In the left border of a resizable window (the user can click the mouse to resize the window horizontally).</summary>
        HTLEFT = 10
        ''' <summary>	In a menu.</summary>
        HTMENU = 5
        ''' <summary>	In a Maximize button.</summary>
        HTMAXBUTTON = 9
        ''' <summary>	In a Minimize button.</summary>
        HTMINBUTTON = 8
        ''' <summary>On the screen background or on a dividing line between windows.</summary>
        HTNOWHERE = 0
        ''' <summary>In a Minimize button.</summary>
        HTREDUCE = HTMINBUTTON
        ''' <summary>In the right border of a resizable window (the user can click the mouse to resize the window horizontally).</summary>
        HTRIGHT = 11
        ''' <summary>In a size box (same as HTGROWBOX).</summary>
        HTSIZE = HTGROWBOX
        ''' <summary>In a window menu or in a Close button in a child window.</summary>
        HTSYSMENU = 3
        ''' <summary>In the upper-horizontal border of a window.</summary>
        HTTOP = 12
        ''' <summary>	In the upper-left corner of a window border.</summary>
        HTTOPLEFT = 13
        ''' <summary>In the upper-right corner of a window border.</summary>
        HTTOPRIGHT = 14
        ''' <summary>	In a window currently covered by another window in the same thread (the message will be sent to underlying windows in the same thread until one of them returns a code that is not HTTRANSPARENT).</summary>
        HTTRANSPARENT = -1
        ''' <summary>In the vertical scroll bar.</summary>
        HTVSCROLL = 7
        ''' <summary>In a Maximize button.</summary>
        HTZOOM = HTMAXBUTTON
    End Enum
    ''' <summary>Values used as return values for the <see cref="WindowMessages.WM_NOTIFYFORMAT"/> message</summary>
    Public Enum WM_NOTIFYFORMAT As Integer
        ''' <summary>ANSI structures should be used in WM_NOTIFY messages sent by the control.</summary>
        NFR_ANSI = 1
        ''' <summary>	Unicode structures should be used in WM_NOTIFY messages sent by the control.</summary>
        NFR_UNICODE = 2
        ''' <summary>An error occurred.</summary>
        zero = 0
    End Enum
    ''' <summary>Values used as return values for the <see cref="WindowMessages.WM_POWER"/> message</summary>
    Public Enum WM_POWER As Integer
        ''' <summary>If wParam is <see cref="wParam.WM_POWER.PWR_SUSPENDRESUME"/> or <see cref="wParam.WM_POWER.PWR_CRITICALRESUME"/>, the return value is zero.</summary>
        zero = 0
        ''' <summary>If wParam is <see cref="wParam.WM_POWER.PWR_SUSPENDREQUEST"/>, the return value is <see cref="PWR_OK"/> not to prevent the system from entering the suspended state.</summary>
        PWR_OK = 1
        ''' <summary>If wParam is <see cref="wParam.WM_POWER.PWR_SUSPENDREQUEST"/>, the return value is <see cref="PWR_FAIL"/> to prevent the system from entering the suspended state.</summary>
        PWR_FAIL = -1
    End Enum
    ''' <summary>Values used as return values for the <see cref="WindowMessages.WM_POWERBROADCAST"/> messag</summary>
    Public Enum WM_POWERBROADCAST As Integer
        ''' <summary>An application should return TRUE if it processes this message.</summary>
        [TRUE] = APIBool.TRUE
        ''' <summary>Windows Server 2003, Windows XP, and Windows 2000:  An application can return <see cref="BROADCAST_QUERY_DENY"/> to deny a <see cref="PBT_APMQUERYSUSPEND"/> or <see cref="PBT_APMQUERYSUSPENDFAILED"/> request.</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> PBT_APMQUERYSUSPENDFAILED = &H2
        ''' <summary>Windows Server 2003, Windows XP, and Windows 2000:  An application can return <see cref="BROADCAST_QUERY_DENY"/> to deny a <see cref="PBT_APMQUERYSUSPEND"/> or <see cref="PBT_APMQUERYSUSPENDFAILED"/> request.</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> BROADCAST_QUERY_DENY = &H424D5144
        ''' <summary>Windows Server 2003, Windows XP, and Windows 2000:  An application can return <see cref="BROADCAST_QUERY_DENY"/> to deny a <see cref="PBT_APMQUERYSUSPEND"/> or <see cref="PBT_APMQUERYSUSPENDFAILED"/> request.</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> PBT_APMQUERYSUSPEND = &H0
    End Enum
    ''' <summary>Values used as return values for the <see cref="WM_SETHOTKEY"/> message</summary>
    Public Enum WM_SETHOTKEY As Integer
        ''' <summary>The function is unsuccessful—the hot key is invalid.</summary>
        minus_1 = -1
        ''' <summary>The function is unsuccessful—the window is invalid.</summary>
        zero = 0
        ''' <summary>The function is successful, and no other window has the same hot key.</summary>
        one = 1
        ''' <summary>The function is successful, but another window already has the same hot key.</summary>
        two = 2
    End Enum
End Namespace
#End If