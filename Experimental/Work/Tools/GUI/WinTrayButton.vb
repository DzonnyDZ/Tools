'For each WinTrayButton that you add to a form their will appear a caption
'bar button that you can use for your programming purposes.
'
'Code Copyright (c) 1tg46
'******************************************************************
'http://www.codeproject.com/KB/vb/transmenuandtitlebuttons.aspx?display=PrintAll&fid=212737&df=90&mpp=25&noise=3&sort=Position&view=Quick&select=2198830
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data

Imports System.Diagnostics
Imports System.Resources
Imports System.Runtime.InteropServices
Namespace GUI
    Public Class WinTrayButton
        Inherits System.Windows.Forms.NativeWindow : Implements IDisposable
        Public Delegate Sub MinTrayBtnClickedEventHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Public Delegate Sub MinTrayBtnHelpClickedEventHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Public Delegate Sub MinTrayBtnStateChangeEventHandler(ByVal sender As Object, ByVal st As state)
        Public Delegate Sub MinTrayBtnIndexChangeEventHandler(ByVal sender As Object, ByVal e As EventArgs)

#Region "Misc Variables"
        Private drawn As Boolean = False
        Private sstate As state = state.Normal
        Private wnd_size As New Size
        Friend captured As Boolean
        Private parent As Form
        Public Event MinTrayBtnClicked As MinTrayBtnClickedEventHandler
        Public Event MinTrayBtnHelpClicked As MinTrayBtnHelpClickedEventHandler
        'This event will handle any drawing that needs to be done.
        Public Event MinTrayBtnStateChanged As MinTrayBtnStateChangeEventHandler
        Public Event MinTrayBtnIndexChange As MinTrayBtnIndexChangeEventHandler
#End Region

#Region "Property Variables"
        Private bmenuID As Integer = 0
        Private bmenutext As String = ""
        Private bhelpbutton As Boolean = False
        Private bimageonly As Boolean = False
        Private bimagelist As ImageList = Nothing
        Private bEnabled As Boolean = True
        Private bindex As Integer = 4
        Private bicon As Bitmap = Nothing
#End Region

#Region "Constants"
        Const SC_CONTEXTHELP As Int32 = &HF180

        Const MF_REMOVE% = &H1000&
        Const MF_BYCOMMAND As Int32 = &H0I
        Const MF_STRING As Int32 = &H0I
        Const MF_BYPOSITION% = &H400&

        Const WM_SYSCOMMAND As Int32 = &H112 ' System menu 
        Const WM_SIZE As Integer = 5
        Const WM_SYNCPAINT As Integer = 136
        Const WM_MOVE As Integer = 3
        Const WM_ACTIVATE As Integer = 6
        Const WM_LBUTTONDOWN As Integer = 513
        Const WM_LBUTTONUP As Integer = 514
        Const WM_LBUTTONDBLCLK As Integer = 515
        Const WM_MOUSEMOVE As Integer = 512
        Const WM_PAINT As Integer = 15
        Const WM_GETTEXT As Integer = 13
        Const WM_NCCREATE As Integer = 129
        Const WM_NCLBUTTONDOWN As Integer = 161
        Const WM_NCLBUTTONUP As Integer = 162
        Const WM_NCMOUSEMOVE As Integer = 160
        Const WM_NCACTIVATE As Integer = 134
        Const WM_NCPAINT As Integer = 133
        Const WM_NCHITTEST As Integer = 132
        Const WM_NCLBUTTONDBLCLK As Integer = 163
        Const WM_INITMENUPOPUP As Int32 = &H117
        Const WM_MENUSELECT As Int32 = &H11F
        Private Const WM_APPCOMMAND As Int32 = &H319

        Const WM_CONTEXTMENU As Int32 = &H7B

        Const VK_LBUTTON As Integer = 1

        Const SM_CXSIZE As Integer = 30
        Const SM_CYSIZE As Integer = 31
#End Region

#Region "WinAPI Imports"
#Region "User32.dll"
        Private Declare Function RemoveMenu Lib "user32" Alias "RemoveMenu" (ByVal hMenu As Int32, ByVal nPosition As Int32, ByVal wFlags As Int32) As Int32

        Private Declare Function AppendMenu Lib "user32" Alias "AppendMenuA" (ByVal hMenu As Int32, ByVal wFlags As Int32, ByVal wIDNewItem As Int32, ByVal lpNewItem As String) As Int32

        Private Declare Function GetSystemMenu Lib "user32" Alias "GetSystemMenu" (ByVal hwnd As IntPtr, ByVal bRevert As Int32) As Int32

        Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Int32, ByVal msg As Int32, ByVal wParam As Int32, ByRef lParam As RECT) As Int32

        <DllImport("user32")> _
        Private Shared Function GetWindowDC(ByVal hwnd As Integer) As Integer
        End Function

        <DllImport("user32")> _
        Private Shared Function GetAsyncKeyState(ByVal vKey As Integer) As Short
        End Function


        <DllImport("user32")> _
        Private Shared Function SetCapture(ByVal hwnd As Integer) As Integer
        End Function


        <DllImport("user32")> _
        Private Shared Function ReleaseCapture() As Boolean
        End Function


        <DllImport("user32")> _
        Private Shared Function GetSysColor(ByVal nIndex As Integer) As Integer
        End Function


        <DllImport("user32")> _
        Private Shared Function GetSystemMetrics(ByVal nIndex As Integer) As Integer
        End Function

#End Region
        '#Region "UxTheme.dll"
        '        <DllImport("Uxtheme", EntryPoint:="OpenThemeData", CharSet:=CharSet.Unicode)> _
        '    Private Shared Function openThemeData(ByVal hWnd As IntPtr, ByVal classList As String) As IntPtr
        '        End Function
        '        <DllImport("Uxtheme", EntryPoint:="CloseThemeData", SetLastError:=True)> _
        '        Private Shared Function closeThemeData(ByVal hTheme As IntPtr) As <MarshalAs(UnmanagedType.Error)> Int32
        '        End Function
        '        <DllImport("Uxtheme", EntryPoint:="IsAppThemed")> _
        '        Private Shared Function isAppThemed() As <MarshalAs(UnmanagedType.Bool)> Boolean
        '        End Function
        '        <DllImport("Uxtheme", EntryPoint:="IsThemeActive")> _
        '        Private Shared Function isThemeActive() As <MarshalAs(UnmanagedType.Bool)> Boolean
        '        End Function
        '        <DllImport("Uxtheme", EntryPoint:="DrawThemeBackground", SetLastError:=True)> _
        '        Private Shared Function drawThemeBackground(ByVal hTheme As IntPtr, ByVal hDC As IntPtr, ByVal iPartId As Int32, ByVal iStateId As Int32, ByRef pRect As RECT, ByVal nullRECT As IntPtr) As <MarshalAs(UnmanagedType.Error)> Int32
        '        End Function
        '#End Region
#Region "Structures"
        <StructLayout(LayoutKind.Sequential)> _
               Private Structure RECT
            Public left As Integer
            Public top As Integer
            Public right As Integer
            Public bottom As Integer


            Public Sub New(ByVal rect As Rectangle)
                MyClass.New(rect.Left, rect.Top, rect.Right, rect.Bottom)

            End Sub 'New


            Public Sub New(ByVal left As Integer, ByVal top As Integer, ByVal right As Integer, ByVal bottom As Integer)
                Me.left = left
                Me.top = top
                Me.right = right
                Me.bottom = bottom

            End Sub 'New



            ' Handy method for converting to a System.Drawing.Rectangle
            Public Function ToRectangle() As Rectangle
                Return New Rectangle(Me.left, Me.top, Me.right - Me.left, Me.bottom - Me.top)

            End Function 'ToRectangle
        End Structure 'RECT
#End Region
#Region "Enums"
        'Their are four possbile states for any windows button
        Public Enum state
            Normal
            Hot
            Pushed
            Disabled
        End Enum
#End Region
#End Region


#Region "Constructor and Handle-Handler ^^"

        Public Sub New(ByVal parent As Form)
            AddHandler parent.HandleCreated, AddressOf Me.OnHandleCreated
            AddHandler parent.HandleDestroyed, AddressOf Me.OnHandleDestroyed
            AddHandler parent.TextChanged, AddressOf Me.OnTextChanged
            AddHandler MinTrayBtnStateChanged, AddressOf Me.StateChange
            AddHandler MinTrayBtnHelpClicked, AddressOf Me.Helpbtnclicked
            AddHandler parent.Disposed, AddressOf Parent_Disposed
            Me.parent = parent
            If parent.IsHandleCreated Then
                Me.OnHandleCreated(parent, EventArgs.Empty)
                wnd_size = parent.Size
            End If
        End Sub 'New

        Private Sub Parent_Disposed(ByVal sender As Object, ByVal e As EventArgs)
            Me.Dispose()
        End Sub


        ' Listen for the control's window creation and then hook into it.
        Private Sub OnHandleCreated(ByVal sender As Object, ByVal e As EventArgs)
            ' Window is now created, assign handle to NativeWindow.
            If Me.Handle <> DirectCast(sender, Form).Handle Then _
                AssignHandle(CType(sender, Form).Handle)

        End Sub 'OnHandleCreated
        Private Sub OnHandleDestroyed(ByVal sender As Object, ByVal e As EventArgs)
            ' Window was destroyed, release hook.
            ReleaseHandle()
        End Sub 'OnHandleDestroyed


        ' Changing the Text invalidates the Window, so we got to Draw the Button again
        Private Sub OnTextChanged(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent MinTrayBtnStateChanged(Me, state.Normal)
        End Sub 'OnTextChanged

        Private Sub StateChange(ByVal sender As Object, ByVal s As state)
            DrawButton()
        End Sub

        Private Sub Helpbtnclicked(ByVal sender As Object, ByVal e As EventArgs)
            If bhelpbutton Then
                SendMessage(parent.Handle.ToInt32, WM_SYSCOMMAND, SC_CONTEXTHELP, Nothing)
            End If
        End Sub

#End Region

#Region "WndProc"
        <System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name:="FullTrust")> _
        <DebuggerStepThrough()> _
        Protected Overrides Sub WndProc(ByRef m As Message)
            If IsDisposed Then MyBase.WndProc(m) : Exit Sub
            ' Change the Pressed-State of the Button when the User pressed the
            ' left mouse button and moves the cursor over the button
            If m.Msg = WM_SYSCOMMAND Then
                If m.WParam.ToInt32 = bmenuID Then
                    'if button menu is pressed the button is clicked so
                    'determine what event should be sent
                    If bhelpbutton = False Then
                        RaiseEvent MinTrayBtnClicked(Me, New EventArgs)
                    Else
                        RaiseEvent MinTrayBtnHelpClicked(Me, New EventArgs)
                    End If
                End If
            End If

            If m.Msg = WM_MOUSEMOVE Then
                If bEnabled = True Then
                    Dim pnt As New Point(m.LParam.ToInt32)
                    'sstate = state.Hot
                    If sstate = state.Pushed Then
                        If MouseinBtn(pnt) = True Then
                            Checkstate(state.Hot)
                        Else
                            Checkstate(state.Normal)
                        End If
                    Else
                        If MouseinBtn(pnt) = True Then
                            If sstate <> state.Hot And captured = False Then
                                Checkstate(state.Hot)
                            ElseIf captured = True Then
                                Checkstate(state.Pushed)
                            End If
                        Else
                            Checkstate(state.Normal)
                        End If
                        If (GetAsyncKeyState(VK_LBUTTON) And -32768) <> 0 Then
                            If MouseinBtn(pnt) Then
                                Checkstate(state.Pushed)
                            End If
                        End If
                    End If
                End If
            End If

            ' Ignore Double-Clicks on the Traybutton
            If m.Msg = WM_NCLBUTTONDBLCLK Then
                Dim pnt As New Point(m.LParam.ToInt32)
                pnt = parent.PointToClient(pnt)
                If MouseinBtn(pnt) Then
                    Return
                End If
            End If
            ' Button released and eventually clicked
            If m.Msg = WM_LBUTTONUP Then
                Dim pnt As New Point(m.LParam.ToInt32)
                ReleaseCapture()
                captured = False
                'RaiseEvent MinTrayBtnClicked(Me, New EventArgs)
                If sstate = state.Pushed Then
                    'if this is a help button raise a help event else regular event
                    If bhelpbutton = False Then
                        RaiseEvent MinTrayBtnClicked(Me, New EventArgs)
                    Else
                        RaiseEvent MinTrayBtnHelpClicked(Me, New EventArgs)
                    End If
                    If bEnabled = True Then
                        SetState(state.Hot)
                        Return
                    End If
                End If
            End If

            ' Clicking the Button - Capture the Mouse and await until the User relases the Button again
            If m.Msg = WM_NCLBUTTONDOWN Then
                Dim pnt As New Point(m.LParam.ToInt32)

                If MouseinBtn(pnt) Then
                    If parent.Enabled = True And bEnabled = True Then
                        SetCapture(parent.Handle.ToInt32)
                        captured = True
                        SetState(state.Pushed)
                    End If
                    Return
                End If
            End If

            ' Drawing the Button and getting the Real Size of the Window
            If m.Msg = WM_ACTIVATE OrElse m.Msg = WM_SIZE OrElse m.Msg = WM_SYNCPAINT OrElse m.Msg = WM_NCCREATE OrElse m.Msg = WM_NCPAINT OrElse m.Msg = WM_NCACTIVATE OrElse m.Msg = WM_NCHITTEST OrElse m.Msg = WM_PAINT OrElse m.Msg = WM_INITMENUPOPUP OrElse m.Msg = WM_MENUSELECT OrElse m.Msg = WM_APPCOMMAND Then
                If m.Msg = WM_SIZE Then
                    wnd_size = New Size(New Point(m.LParam.ToInt32))
                End If
                Dim pnt As New Point(m.LParam.ToInt32)
                If drawn = False Then
                    RaiseEvent MinTrayBtnStateChanged(Me, sstate)
                End If
                If parent.Enabled = False Or bEnabled = False Then
                    SetState(state.Disabled)
                Else
                    If MouseinBtn(pnt) = False Then
                        If Not Form.ActiveForm Is parent Then
                            Checkstate(state.Disabled)
                        Else
                            Checkstate(state.Normal)
                        End If
                    Else
                        Checkstate(state.Hot)
                    End If
                End If
            End If

            MyBase.WndProc(m)

        End Sub 'WndProc
#End Region

#Region "Button-Specific Functions"

        Private Function MouseinBtn(ByVal click As Point) As Boolean
            click = parent.PointToClient(click)
            click.Y = click.Y / -1
            Dim btn_width As Integer = GetSystemMetrics(SM_CXSIZE)
            Dim btn_height As Integer = GetSystemMetrics(SM_CYSIZE)
            Dim btn_size As New Size(btn_width, btn_height)
            Dim pos As Point
            Select Case parent.FormBorderStyle
                Case FormBorderStyle.SizableToolWindow, FormBorderStyle.FixedToolWindow
                    pos = New Point(wnd_size.Width - ((bindex - 1) * btn_width / 1.5) - 12 - (btn_width - 18), 5)
                    btn_height = btn_height / 1.5
                    btn_width = btn_width / 1.5
                    btn_size = New Size(btn_width, btn_height)
                Case FormBorderStyle.None
                Case Else
                    pos = New Point(wnd_size.Width - ((bindex - 1) * btn_width) - 12 - (btn_width - 18), 5)
            End Select
            ' real button size
            'btn_width -= 2
            'btn_height -= 4
            Return click.X >= pos.X AndAlso click.X <= pos.X + btn_size.Width AndAlso click.Y >= pos.Y AndAlso click.Y <= pos.Y + btn_size.Height - pos.Y

        End Function 'MouseinBtn


        Protected Sub DrawButton()
            Dim g As Graphics = Graphics.FromHdc(New IntPtr(GetWindowDC(parent.Handle.ToInt32))) 'm.HWnd));
            DrawButton(g, sstate)

        End Sub 'DrawButton

        Private Sub DrawThemedButtonBg(ByVal g As Graphics, ByVal Element As VisualStyles.VisualStyleElement, ByVal Rect As Rectangle)
            Dim r As New VisualStyles.VisualStyleRenderer(Element)
            r.DrawBackground(g, Rect)
        End Sub

        Private Sub DrawButton(ByVal g As Graphics, ByVal state As state)
            Dim btn_width As Integer = GetSystemMetrics(SM_CXSIZE)
            Dim btn_height As Integer = GetSystemMetrics(SM_CYSIZE)
            Dim pos As Point
            Select Case parent.FormBorderStyle
                Case FormBorderStyle.SizableToolWindow, FormBorderStyle.FixedToolWindow
                    pos = New Point(wnd_size.Width - ((bindex - 1) * btn_width / 1.5) - 12 - (btn_width - 18), 5)
                Case FormBorderStyle.None
                Case Else
                    pos = New Point(wnd_size.Width - ((bindex - 1) * btn_width) - 12 - (btn_width - 18), 5)
            End Select

            ' real button size
            btn_width -= 2
            btn_height -= 4

            Select Case parent.FormBorderStyle
                Case FormBorderStyle.SizableToolWindow, FormBorderStyle.FixedToolWindow
                    'small caption button
                    Dim ButtonState%
                    Dim ButtonPart%
                    Dim ButtonBgRect As Rectangle = New Rectangle(pos.X, pos.Y, btn_width / 1.5, btn_height / 1.5)
                    Select Case state
                        Case state.Normal : ButtonState = 1
                        Case state.Hot : ButtonState = 2
                        Case state.Pushed : ButtonState = 3
                        Case state.Disabled : ButtonState = 4
                    End Select
                    If bhelpbutton = True Then ButtonPart = 23 Else ButtonPart = 15
                    Dim bgDrawn As Boolean = False
                    If VisualStyles.VisualStyleRenderer.IsSupported Then
                        Dim el As VisualStyles.VisualStyleElement = VisualStyles.VisualStyleElement.CreateElement("Window", ButtonPart, ButtonState)
                        If Not VisualStyles.VisualStyleRenderer.IsElementDefined(el) Then el = VisualStyles.VisualStyleElement.CreateElement("Window", ButtonPart, 1)
                        If VisualStyles.VisualStyleRenderer.IsElementDefined(el) Then 'Draw with themes
                            DrawThemedButtonBg(g, el, ButtonBgRect)
                            bgDrawn = True
                        End If
                    End If
                    If Not bgDrawn Then 'Draw w/o themes
                        Dim nthState As ButtonState
                        Select Case state
                            Case WinTrayButton.state.Disabled : nthState = Windows.Forms.ButtonState.Inactive
                            Case WinTrayButton.state.Hot : nthState = Windows.Forms.ButtonState.Normal
                            Case WinTrayButton.state.Normal : nthState = Windows.Forms.ButtonState.Normal
                            Case WinTrayButton.state.Pushed : nthState = Windows.Forms.ButtonState.Pushed
                        End Select
                        If bhelpbutton = True Then
                            ControlPaint.DrawCaptionButton(g, ButtonBgRect, CaptionButton.Help, nthState)
                        Else
                            ControlPaint.DrawButton(g, ButtonBgRect, nthState)
                        End If
                    End If
                    g = Graphics.FromHdc(New IntPtr(GetWindowDC(parent.Handle.ToInt32)))  'm.HWnd));
                    If bhelpbutton = False Then
                        If Not bimagelist Is Nothing Then
                            If bimagelist.Images.Count = 1 Then
                                If state <> state.Disabled Then
                                    g.DrawImage(bimagelist.Images(0), pos.X, pos.Y)
                                Else
                                    ControlPaint.DrawImageDisabled(g, bimagelist.Images(0), pos.X, pos.Y, Color.Transparent)
                                End If
                            ElseIf bimagelist.Images.Count = 4 Then
                                Select Case state
                                    Case state.Disabled
                                        g.DrawImage(bimagelist.Images(3), pos.X, pos.Y)
                                    Case state.Hot
                                        g.DrawImage(bimagelist.Images(1), pos.X, pos.Y)
                                    Case state.Normal
                                        g.DrawImage(bimagelist.Images(0), pos.X, pos.Y)
                                    Case state.Pushed
                                        g.DrawImage(bimagelist.Images(2), pos.X, pos.Y)
                                End Select
                            ElseIf bimagelist.Images.Count > 0 And bimagelist.Images.Count <> 4 Then
                                g.DrawImage(bimagelist.Images(0), pos.X, pos.Y)
                            End If
                        End If
                    End If
                    g.Dispose()
                Case FormBorderStyle.None
                    'draw nothing because their is not title bar to draw to
                Case Else
                    'regular size caption button
                    Dim ButtonState%
                    Dim ButtonPart%
                    Dim ButtonBgRect As Rectangle = New Rectangle(pos.X, pos.Y, btn_width, btn_height)
                    If bhelpbutton = True Then ButtonPart = 23 Else ButtonPart = 15
                    Select Case state
                        Case state.Normal : ButtonState = 1
                        Case state.Hot : ButtonState = 2
                        Case state.Pushed : ButtonState = 3
                        Case state.Disabled : ButtonState = 4
                    End Select
                    Dim bgDrawn As Boolean = False
                    If VisualStyles.VisualStyleRenderer.IsSupported Then
                        Dim el As VisualStyles.VisualStyleElement = VisualStyles.VisualStyleElement.CreateElement("Window", ButtonPart, ButtonState)
                        If Not VisualStyles.VisualStyleRenderer.IsElementDefined(el) Then el = VisualStyles.VisualStyleElement.CreateElement("Window", ButtonPart, 1)
                        If VisualStyles.VisualStyleRenderer.IsElementDefined(el) Then 'Draw with themes
                            DrawThemedButtonBg(g, el, ButtonBgRect)
                            bgDrawn = True
                        End If
                    End If
                    If Not bgDrawn Then 'Draw w/o themes
                        Dim nthState As ButtonState
                        Select Case state
                            Case WinTrayButton.state.Disabled : nthState = Windows.Forms.ButtonState.Inactive
                            Case WinTrayButton.state.Hot : nthState = Windows.Forms.ButtonState.Normal
                            Case WinTrayButton.state.Normal : nthState = Windows.Forms.ButtonState.Normal
                            Case WinTrayButton.state.Pushed : nthState = Windows.Forms.ButtonState.Pushed
                        End Select
                        If bhelpbutton = True Then
                            ControlPaint.DrawCaptionButton(g, ButtonBgRect, CaptionButton.Help, nthState)
                        Else
                            ControlPaint.DrawButton(g, ButtonBgRect, nthState)
                        End If
                    End If
                    g = Graphics.FromHdc(New IntPtr(GetWindowDC(parent.Handle.ToInt32)))  'm.HWnd));
                    If bhelpbutton = False Then
                        If Not bimagelist Is Nothing Then
                            'offset x, y values to center image in button
                            pos.Offset(Math.Sqrt(btn_width) - 1, Math.Sqrt(btn_height) - 1)
                            If bimagelist.Images.Count = 1 Then
                                If state <> state.Disabled Then
                                    g.DrawImage(bimagelist.Images(0), pos.X, pos.Y)
                                Else
                                    ControlPaint.DrawImageDisabled(g, bimagelist.Images(0), pos.X, pos.Y, Color.Transparent)
                                End If
                            ElseIf bimagelist.Images.Count = 4 Then
                                Select Case state
                                    Case state.Disabled
                                        g.DrawImage(bimagelist.Images(3), pos.X, pos.Y)
                                    Case state.Hot
                                        g.DrawImage(bimagelist.Images(1), pos.X, pos.Y)
                                    Case state.Normal
                                        g.DrawImage(bimagelist.Images(0), pos.X, pos.Y)
                                    Case state.Pushed
                                        g.DrawImage(bimagelist.Images(2), pos.X, pos.Y)
                                End Select
                            ElseIf bimagelist.Images.Count > 0 Then
                                g.DrawImage(bimagelist.Images(0), pos.X, pos.Y)
                            End If
                        End If
                    End If
                    g.Dispose()
            End Select
        End Sub 'DrawButton
#End Region

#Region "Properties"
        Public ReadOnly Property ButtonDrawn() As Boolean
            Get
                Return drawn
            End Get
        End Property
        Public ReadOnly Property ButtonState() As state
            Get
                Return sstate
            End Get
        End Property
        <Description("When you create this button you can have a menu created with it and when the menu is clicked the button will be clicked. ")> _
        Public Property ButtonMenuText() As String
            Get
                Return bmenutext
            End Get
            Set(ByVal Value As String)
                bmenutext = Value
                If bmenutext <> "" And bmenuID = 0 Then
                    'supply a random ID so that wndproc from other wintraybuttons don't get confused
                    Dim b As New Random
                    bmenuID = b.Next(1000, 2000)
                    Dim sysmenu As Int32 = GetSystemMenu(parent.Handle, 0)
                    AppendMenu(sysmenu, MF_STRING, bmenuID, bmenutext)
                ElseIf bmenuID <> 0 Then
                    Dim sysmenu As Int32 = GetSystemMenu(parent.Handle, 0)
                    RemoveMenu(sysmenu, bmenuID, MF_BYCOMMAND Or MF_REMOVE)
                    If bmenutext <> "" Then
                        Dim b As New Random
                        bmenuID = b.Next(1000, 2000)
                        AppendMenu(sysmenu, MF_STRING, bmenuID, bmenutext)
                    End If
                End If
            End Set
        End Property
        Public Property ButtonIsHelpButton() As Boolean
            Get
                Return bhelpbutton
            End Get
            Set(ByVal Value As Boolean)
                bhelpbutton = Value
                RaiseEvent MinTrayBtnStateChanged(Me, state.Normal)
                SendMessage(parent.Handle.ToInt32, WM_NCPAINT, 0, Nothing)
                RaiseEvent MinTrayBtnStateChanged(Me, state.Normal)
            End Set
        End Property
        Public Property ButtonDrawImageOnly() As Boolean
            Get
                Return bimageonly
            End Get
            Set(ByVal Value As Boolean)
                bimageonly = Value
                RaiseEvent MinTrayBtnStateChanged(Me, state.Disabled)
                SendMessage(parent.Handle.ToInt32, WM_NCPAINT, 0, Nothing)
                RaiseEvent MinTrayBtnStateChanged(Me, state.Disabled)
            End Set
        End Property
        <Description("This is an imagelist consisting of 1 or 4 images. If 1 image this will be used for all states of the button, if 4 each image will represent a state of the button in the following order: Normal, Hot, Pushed, Disabled")> _
        Public Property ButtonImageList() As ImageList
            Get
                Return bimagelist
            End Get
            Set(ByVal Value As ImageList)
                bimagelist = Value
                RaiseEvent MinTrayBtnStateChanged(Me, sstate)
            End Set
        End Property
        <Description("If the button is not enabled it will appear gray and will not respond to user input.")> _
        Public Property ButtonEnabled() As Boolean
            Get
                Return bEnabled
            End Get
            Set(ByVal Value As Boolean)
                bEnabled = Value
                If Value = True Then
                    'this will invalidate the parent and will
                    'not allow old buttons to be visible
                    RaiseEvent MinTrayBtnStateChanged(Me, state.Normal)
                    SendMessage(parent.Handle.ToInt32, WM_NCPAINT, 0, Nothing)
                    RaiseEvent MinTrayBtnStateChanged(Me, state.Normal)
                Else
                    'this will invalidate the parent and will
                    'not allow old buttons to be visible. Same reason
                    'for the double raiseevent
                    RaiseEvent MinTrayBtnStateChanged(Me, state.Disabled)
                    SendMessage(parent.Handle.ToInt32, WM_NCPAINT, 0, Nothing)
                    RaiseEvent MinTrayBtnStateChanged(Me, state.Disabled)
                End If
            End Set
        End Property
        <Description("A zero-based index of all buttons in the window title bar. This index also includes the min/max/close buttons.")> _
        Public Property ButtonTrayIndex() As Integer
            Get
                Return bindex
            End Get
            Set(ByVal Value As Integer)
                If Value = bindex Then Exit Property
                bindex = Value
                'this will invalidate the parent and will
                'not allow old buttons to be visible. Same reason
                'for the double raiseevent
                RaiseEvent MinTrayBtnStateChanged(Me, state.Normal)
                RaiseEvent MinTrayBtnIndexChange(Me, New EventArgs)
                SendMessage(parent.Handle.ToInt32, WM_NCPAINT, 0, Nothing)
                RaiseEvent MinTrayBtnStateChanged(Me, state.Normal)
            End Set
        End Property
#End Region


#Region "SetState"
        Public Sub SetState(ByVal newstate As state)
            sstate = newstate
            RaiseEvent MinTrayBtnStateChanged(Me, sstate)
        End Sub
#End Region

#Region "CheckState"
        Public Sub Checkstate(ByVal newstate As state)
            'if the state is new then change state to the new state and return true
            If sstate <> newstate Then
                sstate = newstate
                RaiseEvent MinTrayBtnStateChanged(Me, sstate)
            End If
        End Sub
#End Region

#Region " IDisposable Support "
        Public ReadOnly Property IsDisposed() As Boolean
            Get
                Return disposedValue
            End Get
        End Property
        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then

                End If
                ReleaseHandle()
            End If
            Me.disposedValue = True
        End Sub


        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
        ''' <summary>Releases the resources associated with this window</summary>
        Protected Overrides Sub Finalize()
            Dispose()
            MyBase.Finalize()
        End Sub
#End Region

    End Class
End Namespace