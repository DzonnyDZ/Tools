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
End Namespace