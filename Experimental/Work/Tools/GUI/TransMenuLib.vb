'Use this component in your code as follows by using in your sub main 
'routine:
'
'Sub main()
'    TransMenuLib.Hook(Process.GetCurrentProcess)
'    Application.EnableVisualStyles()
'    Application.DoEvents()
'    Application.Run(New Form1)
'    and unhook your program at the end
'    TransMenuLib.Unhook()
'End Sub
'
'Code Copyright (c) 1tg46
'******************************************************************
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Diagnostics
Imports System.Resources
Imports System.Runtime.InteropServices
Imports System.Drawing

Namespace GUI
    <ToolboxItem(True)> _
    Friend Class TransMenuLib
        Inherits System.ComponentModel.Component
#Region " Component Designer generated code "

        Public Sub New(ByVal Container As System.ComponentModel.IContainer)
            MyClass.New()

            'Required for Windows.Forms Class Composition Designer support
            Container.Add(Me)
        End Sub

        Public Sub New()
            MyBase.New()

            'This call is required by the Component Designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call

        End Sub

        'Component overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Component Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Component Designer
        'It can be modified using the Component Designer.
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            components = New System.ComponentModel.Container
        End Sub

#End Region

#Region "API"
#Region "Calls"
        <DllImport("User32.dll", CharSet:=CharSet.Auto)> _
        Friend Shared Function ReleaseDC(ByVal hWnd As IntPtr, ByVal hDC As IntPtr) As Integer
        End Function

        Friend Declare Function SetWindowPos Lib "user32.dll" ( _
        ByVal hwnd As IntPtr, _
        ByVal hWndInsertAfter As Int32, _
        ByVal x As Int32, _
        ByVal y As Int32, _
        ByVal cx As Int32, _
        ByVal cy As Int32, _
        ByVal wFlags As Int32) As Int32

        Friend Declare Function SetLayeredWindowAttributes Lib "user32.dll" ( _
          ByVal hwnd As Int32, _
          ByVal crKey As Int32, _
          ByVal bAlpha As Byte, _
          ByVal dwFlags As Int32) As Int32

        Friend Declare Function DrawMenuBar Lib "User32.dll" _
               (ByVal hwnd As IntPtr) As Int32

        <DllImport("User32.dll", CharSet:=CharSet.Auto)> _
            Friend Shared Function CallNextHookEx(ByVal hookHandle As IntPtr, ByVal code As Integer, ByVal wparam As IntPtr, ByRef cwp As CWPSTRUCT) As Integer
        End Function

        <DllImport("User32.dll", CharSet:=CharSet.Auto)> _
        Friend Shared Function CallWindowProc(ByVal wndProc As IntPtr, ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wparam As IntPtr, ByVal lparam As IntPtr) As Integer
        End Function

        Public Declare Auto Function GetWindowDC Lib "user32" _
        Alias "GetWindowDC" _
        (ByVal hwnd As System.IntPtr) As System.IntPtr

        Public Declare Function GetWindowLong Lib "user32" _
        Alias "GetWindowLongA" (ByVal hWnd As IntPtr, _
        ByVal nIndex As Int32) As Int32

        Public Declare Function SetWindowLong Lib "user32" _
        Alias "SetWindowLongA" (ByVal hWnd As IntPtr, _
                                ByVal nIndex As Int32, _
                                ByVal dwNewint32 As Int32) As Int32

        <DllImport("User32.dll", CharSet:=CharSet.Auto)> _
        Friend Shared Function SetWindowLong(ByVal hwnd As IntPtr, ByVal index As Integer, ByVal wp As WndProcDel) As IntPtr
        End Function

        <DllImport("User32.dll", CharSet:=CharSet.Auto)> _
        Friend Shared Function SetWindowLong(ByVal hwnd As IntPtr, ByVal index As Integer, ByVal dwNewLong As IntPtr) As IntPtr
        End Function

        <DllImport("User32.dll", CharSet:=CharSet.Auto)> _
        Friend Shared Function SetWindowsHookEx(ByVal type As Integer, ByVal hook As HookProcDel, ByVal instance As IntPtr, ByVal threadID As Integer) As IntPtr
        End Function

        <DllImport("User32.dll", CharSet:=CharSet.Auto)> _
        Friend Shared Function UnhookWindowsHookEx(ByVal hookHandle As IntPtr) As Boolean
        End Function

        <DllImport("User32.dll", CharSet:=CharSet.Auto)> _
        Friend Shared Function GetWindowThreadProcessId(ByVal hwnd As IntPtr, ByVal ID As Integer) As Integer
        End Function

        <DllImport("User32.dll", CharSet:=CharSet.Auto)> _
        Friend Shared Function GetClassName(ByVal hwnd As IntPtr, ByVal className As System.Text.StringBuilder, ByVal maxCount As Integer) As Integer
        End Function

#End Region
#Region "Delegates"
        Public Delegate Function WndProcDel(ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wparam As IntPtr, ByVal lparam As IntPtr) As Integer
        Public Delegate Function HookProcDel(ByVal code As Integer, ByVal wparam As IntPtr, ByRef cwp As CWPSTRUCT) As Integer
#End Region
#Region "Structures"
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure WINDOWPOS
            Public hWnd As IntPtr
            Public hWndInsertAfter As IntPtr
            Public x As Integer
            Public y As Integer
            Public cx As Integer
            Public cy As Integer
            Public flags As Integer
        End Structure
        <StructLayout(LayoutKind.Sequential)> _
          Public Structure NCCALCSIZE_PARAMS
            Public rgrc0, rgrc1, rgrc2 As RECT
            Public lppos As IntPtr
        End Structure
        <StructLayout(LayoutKind.Sequential)> _
        Public Structure RECT
            Public Left As Integer
            Public Top As Integer
            Public Right As Integer
            Public Bottom As Integer
        End Structure
        <StructLayout(LayoutKind.Sequential)> _
           Public Structure CWPSTRUCT
            Public lparam As IntPtr
            Public wparam As IntPtr
            Public message As Integer
            Public hwnd As IntPtr
        End Structure

#End Region
#Region "Constants"
        Public Const MENU_CLASS As String = "#32768"
        Public Const SWP_NOACTIVATE As Int32 = &H10
        Public Const SWP_HIDEWINDOW As Int32 = &H80
        Public Const SWP_FRAMECHANGED As Int32 = &H20
        Public Const SWP_NOMOVE As Int32 = &H2
        Public Const SWP_NOOWNERZORDER As Int32 = &H200
        Public Const SWP_NOREPOSITION As Int32 = SWP_NOOWNERZORDER
        Public Const SWP_SHOWWINDOW As Int32 = &H40
        Public Const SWP_NOZORDER As Int32 = &H4
        Public Const SWP_NOSIZE As Int32 = &H1
        Public Const LWA_ALPHA As Int32 = &H2
        Public Const HC_ACTION As Integer = 0
        Public Const WS_EX_LAYERED As Int32 = &H80000
        Public Const GWL_EXSTYLE As Int32 = -20
        Public Const WM_MOUSEMOVE As Int32 = &H200
        Public Const WM_NCCALCSIZE As Integer = &H83
        Public Const WM_WINDOWPOSCHANGING As Integer = &H46
        Public Const WM_NCPAINT As Int32 = &H85
        Public Const WH_CALLWNDPROC As Integer = 4
        Public Const WM_DESTROY As Int32 = &H2
        Public Const WM_PRINT As Int32 = &H317
        Public Const WM_SHARED_MENU As Integer = 482
        Public Const WM_SHOWWINDOW As Int32 = &H18
        Public Const WM_CREATE As Int32 = &H1
        Public Const GWL_WNDPROC As Integer = -4
#End Region
#End Region

#Region "Variables"
        Private Const cintShadow As Integer = 3
        Private Const cintWndProc As Integer = 0
        Private Const cintMnuRec As Integer = 1
        Private Const cintHwndRec As Integer = 2
        Private Const cintMnuHwnd As Integer = 3
        Private Const cintHwndMnu As Integer = 4
        Private Shared sdelHookProc As HookProcDel
        Private Shared sdelWndProc As WndProcDel
        Private Shared spintHookHandle As IntPtr
        Private Shared sarrSubClass() As Hashtable
        Private Shared smnuLastSelected As MenuItem
        Private Shared sblnDrawShadow As Boolean
#End Region
#Region "GetShadowPens"
        Private Shared Function GetShadowPens() As Pen()
            Dim arrPens(cintShadow - 1) As Pen
            Dim intAlphaOffset As Integer = 35
            Dim intMaxAlpha As Integer = cintShadow * intAlphaOffset
            For intIndex As Integer = 0 To arrPens.GetUpperBound(0)
                arrPens(intIndex) = New Pen(Color.FromArgb(intMaxAlpha - (intIndex * intAlphaOffset), Color.Black))
            Next
            Return arrPens
        End Function

#End Region
#Region "Hook"
        Public Shared Sub Hook(ByVal Form As Form)
            Unhook()
            With Environment.OSVersion
                sblnDrawShadow = Not (.Platform = PlatformID.Win32NT AndAlso (.Version.Major > 5 OrElse (.Version.Major = 5 AndAlso .Version.Minor > 0)))
            End With
            sarrSubClass = New Hashtable() {New Hashtable, New Hashtable, New Hashtable, New Hashtable, New Hashtable}
            sdelHookProc = New HookProcDel(AddressOf HookProc)
            sdelWndProc = New WndProcDel(AddressOf WndProc)
            spintHookHandle = SetWindowsHookEx(WH_CALLWNDPROC, sdelHookProc, IntPtr.Zero, GetWindowThreadProcessId(Form.Handle, 0))
        End Sub
        Public Shared Sub Hook(ByVal hwnd As IntPtr)
            Unhook()
            With Environment.OSVersion
                sblnDrawShadow = Not (.Platform = PlatformID.Win32NT AndAlso (.Version.Major > 5 OrElse (.Version.Major = 5 AndAlso .Version.Minor > 0)))
            End With
            sarrSubClass = New Hashtable() {New Hashtable, New Hashtable, New Hashtable, New Hashtable, New Hashtable}
            sdelHookProc = New HookProcDel(AddressOf HookProc)
            sdelWndProc = New WndProcDel(AddressOf WndProc)
            spintHookHandle = SetWindowsHookEx(WH_CALLWNDPROC, sdelHookProc, IntPtr.Zero, GetWindowThreadProcessId(hwnd, 0))
        End Sub
        Public Shared Sub Hook(ByVal p As Process)
            Unhook()
            With Environment.OSVersion
                sblnDrawShadow = Not (.Platform = PlatformID.Win32NT AndAlso (.Version.Major > 5 OrElse (.Version.Major = 5 AndAlso .Version.Minor > 0)))
            End With
            sarrSubClass = New Hashtable() {New Hashtable, New Hashtable, New Hashtable, New Hashtable, New Hashtable}
            sdelHookProc = New HookProcDel(AddressOf HookProc)
            sdelWndProc = New WndProcDel(AddressOf WndProc)
            spintHookHandle = SetWindowsHookEx(WH_CALLWNDPROC, sdelHookProc, IntPtr.Zero, p.Threads(0).Id)
        End Sub
        Public Shared Sub Hook(ByVal processID As Integer)
            Unhook()
            With Environment.OSVersion
                sblnDrawShadow = Not (.Platform = PlatformID.Win32NT AndAlso (.Version.Major > 5 OrElse (.Version.Major = 5 AndAlso .Version.Minor > 0)))
            End With
            sarrSubClass = New Hashtable() {New Hashtable, New Hashtable, New Hashtable, New Hashtable, New Hashtable}
            sdelHookProc = New HookProcDel(AddressOf HookProc)
            sdelWndProc = New WndProcDel(AddressOf WndProc)
            spintHookHandle = SetWindowsHookEx(WH_CALLWNDPROC, sdelHookProc, IntPtr.Zero, processID)
        End Sub

#End Region
#Region "Unhook"
        Public Overloads Shared Sub Unhook()
            If spintHookHandle.Equals(IntPtr.Zero) Then Return
            UnhookWindowsHookEx(spintHookHandle)
            spintHookHandle = IntPtr.Zero
            sarrSubClass = Nothing
            sdelHookProc = Nothing
            sdelWndProc = Nothing
        End Sub
#End Region
#Region "Unsubclass"
        Private Shared Sub Unsubclass(ByVal hwnd As IntPtr, ByVal wndproc As IntPtr)
            sarrSubClass(cintWndProc).Remove(hwnd)
            sarrSubClass(cintHwndRec).Remove(hwnd)
            If Not sarrSubClass(cintHwndMnu)(hwnd) Is Nothing Then
                Dim mnu As MenuItem = DirectCast(sarrSubClass(cintHwndMnu)(hwnd), MenuItem)
                sarrSubClass(cintMnuHwnd).Remove(mnu)
                With mnu
                    .Enabled = Not .Enabled
                    .Enabled = Not .Enabled
                End With
                sarrSubClass(cintHwndMnu).Remove(hwnd)
            End If
            SetWindowLong(hwnd, GWL_WNDPROC, wndproc)
        End Sub

#End Region
#Region "WndProc"
        Private Shared Function WndProc(ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wparam As IntPtr, ByVal lparam As IntPtr) As Integer
            Dim pintWndProc As IntPtr = DirectCast(sarrSubClass(cintWndProc)(hwnd), IntPtr)
            Select Case msg
                Case WM_MOUSEMOVE
                    DrawMenuBar(hwnd)
                Case WM_NCCALCSIZE
                    If sblnDrawShadow Then
                        Dim intResult As Integer = CallWindowProc(pintWndProc, hwnd, msg, wparam, lparam)
                        Dim ncp As NCCALCSIZE_PARAMS = DirectCast(Runtime.InteropServices.Marshal.PtrToStructure(lparam, GetType(NCCALCSIZE_PARAMS)), NCCALCSIZE_PARAMS)
                        ncp.rgrc0.Right -= cintShadow
                        ncp.rgrc0.Bottom -= cintShadow
                        Runtime.InteropServices.Marshal.StructureToPtr(ncp, lparam, True)
                        Return intResult
                    End If
                Case WM_WINDOWPOSCHANGING
                    If sblnDrawShadow OrElse sarrSubClass(cintHwndRec)(hwnd) Is Nothing Then
                        Dim wpos As WINDOWPOS = DirectCast(Runtime.InteropServices.Marshal.PtrToStructure(lparam, GetType(WINDOWPOS)), WINDOWPOS)
                        If sarrSubClass(cintHwndRec)(hwnd) Is Nothing Then
                            wpos.x += cintShadow + 2
                            wpos.y += cintShadow
                        End If
                        If sblnDrawShadow Then
                            wpos.cx += cintShadow
                            wpos.cy += cintShadow
                        End If
                        Runtime.InteropServices.Marshal.StructureToPtr(wpos, lparam, True)
                        Return 0
                    End If
                Case WM_NCPAINT
                    Dim dwstyle As Integer = GetWindowLong(hwnd, GWL_EXSTYLE)
                    SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED Or dwstyle)
                    SetWindowPos(hwnd, -1, 0, 0, 0, 0, SWP_FRAMECHANGED Or SWP_NOACTIVATE Or SWP_SHOWWINDOW Or SWP_NOMOVE Or SWP_NOSIZE)
                    SetLayeredWindowAttributes(hwnd.ToInt32, 0, 150, LWA_ALPHA)
                    DrawBorder(hwnd, IntPtr.Zero)
                    Return 0
                Case WM_PRINT
                    Dim intResult As Integer = CallWindowProc(pintWndProc, hwnd, msg, wparam, lparam)
                    DrawBorder(hwnd, wparam)
                    Return intResult
                Case WM_DESTROY
                    Unsubclass(hwnd, pintWndProc)
                Case WM_SHOWWINDOW
                    If wparam.Equals(IntPtr.Zero) Then Unsubclass(hwnd, pintWndProc)
            End Select
            Return CallWindowProc(pintWndProc, hwnd, msg, wparam, lparam)
        End Function

#End Region
#Region "HookProc"
        Private Shared Function HookProc(ByVal code As Integer, ByVal wparam As IntPtr, ByRef cwp As CWPSTRUCT) As Integer
            If code = HC_ACTION AndAlso (cwp.message = WM_CREATE OrElse (cwp.message = WM_SHARED_MENU AndAlso sarrSubClass(cintWndProc)(cwp.hwnd) Is Nothing)) Then
                Dim sbClassName As New System.Text.StringBuilder(10)
                GetClassName(cwp.hwnd, sbClassName, sbClassName.Capacity)
                If sbClassName.ToString = MENU_CLASS Then
                    sarrSubClass(cintWndProc)(cwp.hwnd) = SetWindowLong(cwp.hwnd, GWL_WNDPROC, sdelWndProc)
                    If Not (smnuLastSelected Is Nothing OrElse sarrSubClass(cintMnuRec)(smnuLastSelected) Is Nothing) Then
                        sarrSubClass(cintHwndRec)(cwp.hwnd) = sarrSubClass(cintMnuRec)(smnuLastSelected)
                        sarrSubClass(cintMnuHwnd)(smnuLastSelected) = cwp.hwnd
                        sarrSubClass(cintHwndMnu)(cwp.hwnd) = smnuLastSelected
                        With smnuLastSelected
                            .Enabled = Not .Enabled
                            .Enabled = Not .Enabled
                        End With
                    End If
                End If
            End If
            Return CallNextHookEx(spintHookHandle, code, wparam, cwp)
        End Function

#End Region
#Region "DrawBorder"
        Private Shared Sub DrawBorder(ByVal hwnd As IntPtr, ByVal hDc As IntPtr)
            Dim blnRelease As Boolean
            If hDc.Equals(IntPtr.Zero) Then
                hDc = GetWindowDC(hwnd)
                blnRelease = True
            End If
            With Graphics.FromHdc(hDc)
                Dim rec As New Rectangle(0, 0, Convert.ToInt32(.VisibleClipBounds.Width), Convert.ToInt32(.VisibleClipBounds.Height))
                Dim pen As Pen
                If sblnDrawShadow Then
                    rec.Width -= cintShadow
                    rec.Height -= cintShadow
                    Dim recShadowRight As New Rectangle(rec.Right, rec.Top + (cintShadow * 2), cintShadow, rec.Height - cintShadow)
                    .FillRectangle(SystemBrushes.Control, recShadowRight)
                    Dim recShadowBottom As New Rectangle(rec.X + (cintShadow * 2), rec.Bottom, rec.Width - cintShadow, cintShadow)
                    .FillRectangle(SystemBrushes.Control, recShadowBottom)
                    Dim arrPens As Pen() = GetShadowPens()
                    For intIndex As Integer = 0 To arrPens.GetUpperBound(0)
                        pen = arrPens(intIndex)
                        .DrawLine(pen, recShadowRight.X + intIndex, recShadowRight.Y, recShadowRight.X + intIndex, recShadowRight.Bottom - cintShadow + intIndex)
                        .DrawLine(pen, recShadowBottom.X, recShadowBottom.Y + intIndex, recShadowBottom.Right - cintShadow + intIndex, recShadowBottom.Y + intIndex)
                        pen.Dispose()
                    Next
                End If
                rec.Width -= 1
                rec.Height -= 1
                .DrawRectangle(SystemPens.ControlDark, rec)
                pen = SystemPens.Control
                rec.X += 1
                rec.Y += 1
                rec.Width -= 2
                rec.Height -= 2
                .DrawRectangle(pen, rec)
                rec.X += 1
                rec.Y += 1
                rec.Width -= 2
                rec.Height -= 2
                .DrawRectangle(pen, rec)
                If Not sarrSubClass(cintHwndRec)(hwnd) Is Nothing Then
                    rec = DirectCast(sarrSubClass(cintHwndRec)(hwnd), Rectangle)
                    Dim intWidth As Integer = rec.Width + rec.X + Form.ActiveForm.Location.X
                    If intWidth > rec.Width Then intWidth = rec.Width
                    intWidth -= 1
                    If intWidth > 0 Then .DrawLine(pen, 1, 0, intWidth, 0)
                End If
                .Dispose()
            End With
            If blnRelease Then ReleaseDC(hwnd, hDc)
        End Sub

#End Region

    End Class
End Namespace