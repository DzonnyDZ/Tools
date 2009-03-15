Imports System.Drawing
Imports System.Windows.Forms

Namespace GUI
    Public Module Misc
        '''' <summary>Vrací delegáta handleru události <see cref="Control.Paint"/>, který kreslí Ð</summary>
        'Public ReadOnly Property PaintÐHandler() As PaintEventHandler
        '    Get
        '        Return AddressOf PaintÐSub
        '    End Get
        'End Property
        '''' <summary>Automaticky ošetøí kreslení Ð</summary>
        '''' <param name="Form">Formuláø, pøi jehož zavøení kreslení zrušit</param>
        '''' <param name="Control">Ovládací prvek, na který kreslit (pokud vynechán, použije se <paramref name="Form"/>)</param>
        'Public Sub HandlePaint(ByVal Form As Form, Optional ByVal Control As Control = Nothing)
        '    If Control Is Nothing Then Control = Form
        '    Form_Control.Add(Form, Control)
        '    AddHandler Control.Paint, PaintÐHandler
        '    AddHandler Form.FormClosed, AddressOf FormClosed
        'End Sub
        '''' <summary>Automaticky ošetøí kreslení Ð</summary>
        '''' <param name="Offset">Offset kreslení od standartní pozice (tj. pravého dolního rohu)</param>
        '''' <param name="Form">Formuláø, pøi jehož zavøení kreslení zrušit</param>
        '''' <param name="Control">Ovládací prvek, na který kreslit (pokud vynechán, použije se <paramref name="Form"/>)</param>
        'Public Sub HandlePaint(ByVal Offset As Size, ByVal Form As Windows.Forms.Form, Optional ByVal Control As Windows.Forms.Control = Nothing)
        '    If Control Is Nothing Then Control = Form
        '    Form_Control.Add(Form, Control)
        '    AddHandler Control.Paint, PaintÐHandler
        '    AddHandler Form.FormClosed, AddressOf FormClosed
        '    Offsets.Add(Control, Offset)
        'End Sub
        'Private ReadOnly Offsets As New Dictionary(Of Control, Size)
        '''' <summary>Seznam dvojic formuláø-ovládací prvek pro odstraòování handlerù</summary>
        'Private ReadOnly Form_Control As New Dictionary(Of Form, Control)
        '''' <summary>Reaguje na zavøení formuláøe a odstraní handlery</summary>
        'Private Sub FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        '    If Form_Control.ContainsKey(sender) Then
        '        If Offsets.ContainsKey(Form_Control(sender)) Then Offsets.Remove(Form_Control(sender))
        '        RemoveHandler Form_Control(sender).Paint, PaintÐHandler
        '        RemoveHandler DirectCast(sender, Form).FormClosed, AddressOf FormClosed
        '        Form_Control.Remove(sender)
        '    End If
        'End Sub
        '''' <summary>Nakreslí Ð</summary>
        '''' <remarks>Handler události <see cref="Control.Paint"/></remarks>
        '<DebuggerStepThrough()> _
        'Private Sub PaintÐSub(ByVal sender As Object, ByVal e As PaintEventArgs)
        '    Static ÐFont As Font
        '    If ÐFont Is Nothing Then ÐFont = New Font("Arial", 6, FontStyle.Regular, GraphicsUnit.Point)
        '    Static ÐBrush As Brush
        '    If ÐBrush Is Nothing Then ÐBrush = New SolidBrush(Color.LightGray)
        '    With DirectCast(sender, Control)
        '        Dim ÐSize As SizeF = e.Graphics.MeasureString("Ð", .Font)
        '        Dim ÐRect As New Rectangle(New Point(.ClientSize.Width - ÐSize.Width, .ClientSize.Height - ÐSize.Height), ÐSize.ToSize)
        '        If Offsets.ContainsKey(sender) Then ÐRect.Location += Offsets(sender)
        '        If e.ClipRectangle.IntersectsWith(ÐRect) Then
        '            e.Graphics.DrawString("Ð", ÐFont, ÐBrush, ÐRect)
        '        End If
        '    End With
        'End Sub
        ''' <summary>Invokes <see cref="Interaction.MsgBox"/> on specified control</summary>
        ''' <param name="Ctl">Control to invoke <see cref="Interaction.MsgBox"/> in context of</param>
        ''' <param name="Prompt">Message box prompt</param>
        ''' <param name="Buttons">Defines buttons and icons of message box</param>
        ''' <param name="title">Message box title</param>
        ''' <returns>Pressed button</returns>
        Public Function MsgBoxOn(ByVal Ctl As Control, ByVal Prompt As String, Optional ByVal Buttons As MsgBoxStyle = MsgBoxStyle.OkOnly, Optional ByVal title As String = Nothing) As MsgBoxResult
            Dim mDel As New dMsgbox(AddressOf MsgBox)
            Return Ctl.Invoke(mDel, Prompt, Buttons, title)
        End Function
        ''' <summary>Delegát na <see cref="MsgBox"/></summary>
        ''' <param name="Prompt">Message box prompt</param>
        ''' <param name="Buttons">Defines buttons and icons of message box</param>
        ''' <param name="title">Message box title</param>
        ''' <returns>Pressed button</returns>
        Private Delegate Function dMsgbox(ByVal Prompt As Object, ByVal Buttons As MsgBoxStyle, ByVal title As Object) As MsgBoxResult
        ''' <summary>Vynutí aktualizaci SysTray</summary>
        Public Sub RefreshSystray()
            Dim Shell_TrayWnd% = API.FindWindow("Shell_TrayWnd", Nothing)
            If Shell_TrayWnd <> 0 Then
                Dim TrayNotifyWnd% = API.FindWindowEx(Shell_TrayWnd, 0, "TrayNotifyWnd", Nothing)
                If TrayNotifyWnd <> 0 Then
                    Dim SysPager% = API.FindWindowEx(TrayNotifyWnd, 0, "SysPager", Nothing)
                    If SysPager <> 0 Then
                        Dim ToolbarWindow32% = API.FindWindowEx(SysPager, 0, "ToolbarWindow32", Nothing)
                        Dim Button% = API.FindWindowEx(TrayNotifyWnd, 0, "Button", Nothing)
                        If Button <> 0 Then
                            API.SendMessage(Button, API.WM_LBUTTONDOWN, API.MK_LBUTTON, 0)
                            API.SendMessage(Button, API.WM_LBUTTONUP, 0, 0)
                        End If
                        If ToolbarWindow32 <> 0 Then
                            Dim Rect As New API.RECT
                            API.GetWindowRect(ToolbarWindow32, Rect)
                            For i As Integer = Rect.Left To Rect.Right Step 8
                                API.SendMessage(ToolbarWindow32, API.WM_MOUSEMOVE, 0, i - Rect.Left Or 8 << 16)
                            Next i
                        End If
                    End If
                End If
            End If
        End Sub
        ''' <summary>Deklarace API pro <see  cref="RefreshSystray"/></summary>
        Private Class API
            Private Sub New()
            End Sub
            Public Declare Auto Function FindWindow Lib "user32.dll" (ByVal lpClassName As String, ByVal lpWindowName As String) As Int32
            Public Declare Auto Function FindWindowEx Lib "user32.dll" (ByVal hWnd1 As Int32, ByVal hWnd2 As Int32, ByVal lpsz1 As String, ByVal lpsz2 As String) As Int32
            Public Declare Auto Function SendMessage Lib "user32.dll" (ByVal hwnd As Int32, ByVal wMsg As Int32, ByVal wParam As Int32, ByVal lParam As Int32) As Int32
            Public Const WM_MOUSEMOVE As Int32 = &H200
            Public Const WM_LBUTTONDOWN As Int32 = &H201
            Public Const WM_LBUTTONUP As Int32 = &H202
            Public Const MK_LBUTTON As Int32 = &H1
            Public Declare Function GetWindowRect Lib "user32.dll" (ByVal hwnd As Int32, ByRef lpRect As RECT) As Int32
            <Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)> _
            Public Structure RECT
                Public Left As Int32
                Public Top As Int32
                Public Right As Int32
                Public Bottom As Int32
            End Structure
        End Class
    End Module
End Namespace