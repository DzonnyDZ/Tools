#If True
Namespace API.Public
    ''' <summary>Predefined window longs for <see cref="WindowsT.NativeT.Win32Window.WindowLong"/></summary>
    Public Enum WindowLongs As Int32
        ''' <summary>Retrieves the extended window styles. For more information, see CreateWindowEx.</summary>
        ''' <remarks>GWL_EXSTYLE</remarks>
        ExStyle = GUI.WindowLongs.GWL_EXSTYLE
        ''' <summary>Retrieves the window styles.</summary>
        ''' <remarks>GWL_STYLE</remarks>
        Style = GUI.WindowLongs.GWL_STYLE
        ''' <summary>Retrieves the address of the window procedure, or a handle representing the address of the window procedure. You must use the CallWindowProc function to call the window procedure.</summary>
        ''' <remarks>GWL_WNDPROC</remarks>
        WndProc = GUI.WindowLongs.GWL_WNDPROC
        ''' <summary>Retrieves a handle to the application instance.</summary>
        ''' <remarks>GWL_HINSTANCE</remarks>
        HInstance = GUI.WindowLongs.GWL_HINSTANCE
        ''' <summary>Retrieves a handle to the parent window, if any.</summary>
        ''' <remarks>GWL_HWNDPARENT</remarks>
        hWndParent = GUI.WindowLongs.GWL_HWNDPARENT
        ''' <summary>Retrieves the identifier of the window.</summary>
        ''' <remarks>GWL_ID</remarks>
        Id = GUI.WindowLongs.GWL_ID
        ''' <summary>Retrieves the user data associated with the window. This data is intended for use by the application that created the window. Its value is initially zero.</summary>
        ''' <remarks>GWL_USERDATA</remarks>
        UserData = GUI.WindowLongs.GWL_USERDATA
        ''' <summary>Retrieves the address of the dialog box procedure, or a handle representing the address of the dialog box procedure. You must use the CallWindowProc function to call the dialog box procedure.</summary>
        ''' <remarks>DWL_DLGPROC</remarks>
        DlgProc = GUI.WindowLongs.DWL_DLGPROC
        ''' <summary>Retrieves the return value of a message processed in the dialog box procedure.</summary>
        ''' <remarks>DWL_MSGRESULT</remarks>
        MsgResult = GUI.WindowLongs.DWL_MSGRESULT
        ''' <summary>Retrieves extra information private to the application, such as handles or pointers.</summary>
        ''' <remarks>DWL_USER</remarks>
        User = GUI.WindowLongs.DWL_USER
    End Enum

    ''' <summary>Defines window extended styles</summary>
    ''' <version version="1.5.3">This enumeration is new in version 1.5.3</version>
    <Flags()>
    Public Enum WindowExtendedStyles As Integer
        ''' <summary>The windo accepts drag &amp; drop files</summary>
        AcceptsFiles = API.GUI.ExStyles.WS_EX_ACCEPTFILES
        ''' <summary>Forces a top-level window onto the taskbar when the window is visible.</summary>
        AppWindow = API.GUI.ExStyles.WS_EX_APPWINDOW
        ''' <summary>The window has a border with a sunken edge.</summary>
        ClientEdge = API.GUI.ExStyles.WS_EX_CLIENTEDGE
        ''' <summary>Paints all descendants of a window in bottom-to-top painting order using double-buffering. This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.</summary>
        ''' <remarks>Windows 2000: This flag is not supported.</remarks>
        Composited = API.GUI.ExStyles.WS_EX_COMPOSITED
        ''' <summary>The title bar of the window includes a question mark. When the user clicks the question mark, the cursor changes to a question mark with a pointer. If the user then clicks a child window, the child receives a <see cref="API.Messages.WindowMessages.WM_HELP"/> message.</summary>
        ''' <remarks><see cref="ContextHelp"/> cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.</remarks>
        ContextHelp = API.GUI.ExStyles.WS_EX_CONTEXTHELP
        ''' <summary>The window itself contains child windows that should take part in dialog box navigation. If this style is specified, the dialog manager recurses into children of this window when performing navigation operations such as handling the TAB key, an arrow key, or a keyboard mnemonic.</summary>
        ControlParent = API.GUI.ExStyles.WS_EX_CONTROLPARENT
        ''' <summary>The window has a double border; the window can, optionally, be created with a title bar by specifying the WS_CAPTION style in the dwStyle parameter.</summary>
        DialogModalFrame = API.GUI.ExStyles.WS_EX_DLGMODALFRAME
        ''' <summary>The window is a layered window. Note that this cannot be used for child System.Windows. Also, this cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.</summary>
        Layered = API.GUI.ExStyles.WS_EX_LAYERED
        ''' <summary>If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the horizontal origin of the window is on the right edge. Increasing horizontal values advance to the left.</summary>
        LayoutRtl = API.GUI.ExStyles.WS_EX_LAYOUTRTL
        ''' <summary>The window has generic left-aligned properties. This is the default.</summary>
        Left = API.GUI.ExStyles.WS_EX_LEFT
        ''' <summary>If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the vertical scroll bar (if present) is to the left of the client area. For other languages, the style is ignored.</summary>
        LeftScrollBar = API.GUI.ExStyles.WS_EX_LEFTSCROLLBAR
        ''' <summary>The window text is displayed using left-to-right reading-order properties. This is the default.</summary>
        LtrReading = API.GUI.ExStyles.WS_EX_LTRREADING
        ''' <summary>The window is a MDI child window.</summary>
        MdiChild = API.GUI.ExStyles.WS_EX_MDICHILD
        ''' <summary>A top-level window created with this style does not become the foreground window when the user clicks it. The system does not bring this window to the foreground when the user minimizes or closes the foreground window.</summary>
        NoActive = API.GUI.ExStyles.WS_EX_NOACTIVATE
        ''' <summary>The window does not pass its window layout to its child System.Windows.</summary>
        NoInheritLayout = API.GUI.ExStyles.WS_EX_NOINHERITLAYOUT
        ''' <summary>The child window created with this style does not send the <see cref="API.Messages.WindowMessages.WM_PARENTNOTIFY"/> message to its parent window when it is created or destroyed.</summary>
        NoParentNotify = API.GUI.ExStyles.WS_EX_NOPARENTNOTIFY
        ''' <summary>The window is an overlapped window.</summary>
        OverlappedWindow = API.GUI.ExStyles.WS_EX_OVERLAPPEDWINDOW
        ''' <summary>TBD</summary>
        PaletteWindow = API.GUI.ExStyles.WS_EX_PALETTEWINDOW
        ''' <summary>The window has generic "right-aligned" properties. This depends on the window class. This style has an effect only if the shell language is Hebrew, Arabic, or another language that supports reading-order alignment; otherwise, the style is ignored.</summary>
        Right = API.GUI.ExStyles.WS_EX_RIGHT
        ''' <summary>The vertical scroll bar (if present) is to the right of the client area. This is the default.</summary>
        RightScrollBar = API.GUI.ExStyles.WS_EX_RIGHTSCROLLBAR
        ''' <summary>If the shell language is Hebrew, Arabic, or another language that supports reading-order alignment, the window text is displayed using right-to-left reading-order properties. For other languages, the style is ignored.</summary>
        RtlReading = API.GUI.ExStyles.WS_EX_RTLREADING
        ''' <summary>The window has a three-dimensional border style intended to be used for items that do not accept user input.</summary>
        StaticEdge = API.GUI.ExStyles.WS_EX_STATICEDGE
        ''' <summary>The window is intended to be used as a floating toolbar. A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font. A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB. If a tool window has a system menu, its icon is not displayed on the title bar. However, you can display the system menu by right-clicking or by typing ALT+SPACE.</summary>
        ToolWindow = API.GUI.ExStyles.WS_EX_TOOLWINDOW
        ''' <summary>The window should be placed above all non-topmost windows and should stay above them, even when the window is deactivated. To add or remove this style, use the SetWindowPos function.</summary>
        TopMost = API.GUI.ExStyles.WS_EX_TOPMOST
        ''' <summary>The window should not be painted until siblings beneath the window (that were created by the same thread) have been painted. The window appears transparent because the bits of underlying sibling windows have already been painted.</summary>
        Transparent = API.GUI.ExStyles.WS_EX_TRANSPARENT
        ''' <summary>The window has a border with a raised edge.</summary>
        WindowEdge = API.GUI.ExStyles.WS_EX_WINDOWEDGE
    End Enum
End Namespace
#End If