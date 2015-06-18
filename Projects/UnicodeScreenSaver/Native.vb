Imports System.Runtime.InteropServices

''' <summary>Defines Win32 API functions</summary>
Friend Module Native
    ''' <summary>Retrieves the coordinates of a window's client area. The client coordinates specify the upper-left and lower-right corners of the client area. Because client coordinates are relative to the upper-left corner of a window's client area, the coordinates of the upper-left corner are (0,0). </summary>
    ''' <param name="hWnd">A handle to the window whose client coordinates are to be retrieved. </param>
    ''' <param name="lpRect">A pointer to a <see cref="RECT"/> structure that receives the client coordinates. The left and top members are zero. The right and bottom members contain the width and height of the window. </param>
    ''' <returns>If the function succeeds, the return value is true. If the Function fails, the Return value Is false. </returns>
    Public Declare Ansi Function GetClientRect Lib "user32.dll" (hWnd As IntPtr, ByRef lpRect As RECT) As <MarshalAs(UnmanagedType.Bool)> Boolean
End Module

''' <summary>Defines the coordinates of the upper-left and lower-right corners of a rectangle.</summary>
Friend Structure Rect
    ''' <summary>The x-coordinate of the upper-left corner of the rectangle.</summary>
    Public Left%
    ''' <summary>The y-coordinate of the upper-left corner of the rectangle.</summary>
    Public Top%
    ''' <summary>The x-coordinate of the lower-right corner of the rectangle.</summary>
    Public Right%
    ''' <summary>The y-coordinate of the lower-right corner of the rectangle.</summary>
    Public Bottom%
End Structure

''' <summary>Defines various WIndow styles</summary>
Friend Enum WindowStyles As UInteger
    WS_OVERLAPPED = &H00000000
    WS_POPUP = &H80000000UI
    WS_CHILD = &H40000000
    WS_MINIMIZE = &H20000000
    WS_VISIBLE = &H10000000
    WS_DISABLED = &H08000000
    WS_CLIPSIBLINGS = &H04000000
    WS_CLIPCHILDREN = &H02000000
    WS_MAXIMIZE = &H01000000
    WS_CAPTION = &H00C00000 ' WS_BORDER | WS_DLGFRAME  */
    WS_BORDER = &H00800000
    WS_DLGFRAME = &H00400000
    WS_VSCROLL = &H00200000
    WS_HSCROLL = &H00100000
    WS_SYSMENU = &H00080000
    WS_THICKFRAME = &H00040000
    WS_GROUP = &H00020000
    WS_TABSTOP = &H00010000

    WS_MINIMIZEBOX = &H00020000
    WS_MAXIMIZEBOX = &H00010000

    WS_TILED = WS_OVERLAPPED
    WS_ICONIC = WS_MINIMIZE
    WS_SIZEBOX = WS_THICKFRAME
    WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW

    'Common Window Styles

    WS_OVERLAPPEDWINDOW = WS_OVERLAPPED Or WS_CAPTION Or WS_SYSMENU Or WS_THICKFRAME Or WS_MINIMIZEBOX Or WS_MAXIMIZEBOX
    WS_POPUPWINDOW = WS_POPUP Or WS_BORDER Or WS_SYSMENU
    WS_CHILDWINDOW = WS_CHILD

    'Extended Window Styles

    WS_EX_DLGMODALFRAME = &H00000001
    WS_EX_NOPARENTNOTIFY = &H00000004
    WS_EX_TOPMOST = &H00000008
    WS_EX_ACCEPTFILES = &H00000010
    WS_EX_TRANSPARENT = &H00000020

    '#if(WINVER > = &h0400)
    WS_EX_MDICHILD = &H00000040
    WS_EX_TOOLWINDOW = &H00000080
    WS_EX_WINDOWEDGE = &H00000100
    WS_EX_CLIENTEDGE = &H00000200
    WS_EX_CONTEXTHELP = &H00000400

    WS_EX_RIGHT = &H00001000
    WS_EX_LEFT = &H00000000
    WS_EX_RTLREADING = &H00002000
    WS_EX_LTRREADING = &H00000000
    WS_EX_LEFTSCROLLBAR = &H00004000
    WS_EX_RIGHTSCROLLBAR = &H00000000

    WS_EX_CONTROLPARENT = &H00010000
    WS_EX_STATICEDGE = &H00020000
    WS_EX_APPWINDOW = &H00040000

    WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE Or WS_EX_CLIENTEDGE
    WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE Or WS_EX_TOOLWINDOW Or WS_EX_TOPMOST

    WS_EX_LAYERED = &H00080000


    WS_EX_NOINHERITLAYOUT = &H00100000 ' Disable inheritence Of mirroring by children
    WS_EX_LAYOUTRTL = &H00400000 ' Right To left mirroring

    WS_EX_COMPOSITED = &H02000000
    WS_EX_NOACTIVATE = &H08000000
End Enum
