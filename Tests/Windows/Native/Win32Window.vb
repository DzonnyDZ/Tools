#If Config <= Nightly Then
Imports Tools.WindowsT.NativeT, System.Linq
Namespace WindowsT.NativeT

    ''' <summary>Test for <see cref="Tools.IOt.FileSystemEnumerator"/></summary>
    Public Class frmWin32Window
        ''' <summary>CTor</summary>
        Public Sub New()
            InitializeComponent()
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub
        ''' <summary>Runs test</summary>
        Public Shared Sub Test()
            Dim f As New frmWin32Window
            f.ShowDialog()
        End Sub
        Private TestForms(1) As frmTestForm
        Private Sub frmWin32Window_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            cmbObject.Items.Clear()
            Dim top As Integer = Me.Top
            For i = 1 To 2
                Dim frm As New frmTestForm With {.Text = String.Format("TestForm{0}", i), .Name = .Text}
                cmbObject.Items.Add(New Win32Window(frm))
                For Each Control As Control In frm.Controls
                    Control.Text &= i
                    Control.Name = Control.Text
                    cmbObject.Items.Add(New Win32Window(Control))
                Next Control
                frm.Show(Me)
                frm.Left = Me.Right
                frm.Top = top
                top += frm.Height
                TestForms(i - 1) = frm
            Next i
            'cmbObject.DisplayMember = "Text"
            cmbSetParent.Items.Clear()
            cmbSetParent.Items.Add("<nothing>")
            cmbSetParent.Items.AddRange(cmbObject.Items.OfType(Of Object).ToArray)
            cmbObject.SelectedIndex = 0
            cmbSetParent.SelectedIndex = 0
            'cmbSetParent.DisplayMember = "Text"
            CreateNativeWindow()
        End Sub

        Private Sub cmdSetParent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetParent.Click
            Try
                If cmbSetParent.SelectedIndex > 0 Then
                    DirectCast(prgProperties.SelectedObject, Win32Window).Parent = cmbSetParent.SelectedItem
                    SelectedObjectChanged()
                ElseIf cmbSetParent.SelectedIndex = 0 Then
                    DirectCast(prgProperties.SelectedObject, Win32Window).Parent = Nothing
                    SelectedObjectChanged()
                Else
                    MsgBox("You must select parent!")
                End If
            Catch ex As API.Win32APIException
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
            End Try
        End Sub

        Private Sub cmbObject_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbObject.SelectedIndexChanged
            If cmbObject.SelectedIndex < 0 Then Exit Sub
            prgProperties.SelectedObject = cmbObject.SelectedItem
            SelectedObjectChanged()
        End Sub

        Private Sub SelectedObjectChanged()
            If cmbObject.Items.Contains(prgProperties.SelectedObject) Then
                cmbObject.SelectedItem = prgProperties.SelectedObject
            Else
                cmbObject.SelectedIndex = -1
            End If
            If lstWindowList.Items.Contains(prgProperties.SelectedObject) Then
                lstWindowList.SelectedItem = prgProperties.SelectedObject
            Else
                lstWindowList.SelectedIndex = -1
            End If
            With DirectCast(prgProperties.SelectedObject, Win32Window)
                If .Parent Is Nothing Then
                    cmbSetParent.SelectedIndex = 0
                ElseIf cmbSetParent.Items.Contains(.Parent) Then
                    cmbSetParent.SelectedItem = .Parent
                Else
                    cmbSetParent.SelectedIndex = -1
                End If
            End With
        End Sub


        Private Sub cmdLoadParent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadParent.Click
            With DirectCast(prgProperties.SelectedObject, Win32Window)
                If .Parent Is Nothing Then
                    MsgBox("Current object has no parent")
                Else
                    prgProperties.SelectedObject = .Parent
                    SelectedObjectChanged()
                End If
            End With
        End Sub

        Private Sub cmdLoadDesktop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadDesktop.Click
            prgProperties.SelectedObject = Win32Window.Desktop
            SelectedObjectChanged()
        End Sub


        Private Sub cmdLoadTopLevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadTopLevel.Click
            lstWindowList.Items.Clear()
            Try
                lstWindowList.Items.AddRange(Win32Window.TopLevelWindows.OfType(Of Object).ToArray)
                'lstWindowList.DisplayMember = "Text"
            Catch ex As API.Win32APIException
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
            End Try
        End Sub

        Private Sub cmdLoadChildren_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadChildren.Click
            lstWindowList.Items.Clear()
            Try
                lstWindowList.Items.AddRange(DirectCast(prgProperties.SelectedObject, Win32Window).Children.OfType(Of Object).ToArray)
                'lstWindowList.DisplayMember = "Text"
            Catch ex As API.Win32APIException
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
            End Try
        End Sub

        Private Sub lstWindowList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstWindowList.SelectedIndexChanged
            If lstWindowList.SelectedIndex < 0 Then Exit Sub
            prgProperties.SelectedObject = lstWindowList.SelectedItem
            SelectedObjectChanged()
        End Sub

        Private Sub chkPrintMessage_CheckedChanged(ByVal sender As CheckBox, ByVal e As System.EventArgs) Handles chkPrintMessage2.CheckedChanged, chkPrintMessage1.CheckedChanged
            Dim i = If(sender Is chkPrintMessage1, 1, 2)
            TestForms(i - 1).WndProcWrites = sender.Checked
        End Sub

        Private Sub cmdWndReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWndReplace.Click
            With CType(prgProperties.SelectedObject, Win32Window)
                If OldWndProcs.ContainsKey(.hWnd) Then
                    MsgBox("Wnd proc of this window has already been replaced.", MsgBoxStyle.Information, "Wnd proc")
                Else
                    Dim OldProc As API.Messages.WndProc = Nothing
                    Dim OldWndProcPtr As IntPtr = 0
                    Try
                        OldProc = .WndProc
                        OldWndProcPtr = .WndProcPointer
                    Catch ex As Exception
                        If MsgBox(String.Format("Error backing current wnd proc up:{0}{1}{0}{0}Continue anyway", vbCrLf, ex.Message), MsgBoxStyle.Critical Or MsgBoxStyle.YesNo, ex.GetType.Name) <> MsgBoxResult.Yes Then Exit Sub
                    End Try
                    OldWndProcs.Add(.hWnd, New KeyValuePair(Of IntPtr, API.Messages.WndProc)(OldWndProcPtr, OldProc))
                    Try
                        .WndProc = ReplacementWndProcDelegate
                    Catch ex As Exception
                        OldWndProcs.Remove(.hWnd)
                        MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                    End Try
                End If
            End With
        End Sub

        Private Sub cmdWndRenew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWndRenew.Click
            With CType(prgProperties.SelectedObject, Win32Window)
                If Not OldWndProcs.ContainsKey(.hWnd) Then
                    MsgBox("Wnd proc of this window is not backed up.", MsgBoxStyle.Information, "Wnd proc")
                Else
                    Try
                        .WndProcPointer = OldWndProcs(.hWnd).Key
                        OldWndProcs.Remove(.hWnd)
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                    End Try
                End If
            End With
        End Sub

        Private OldWndProcs As New Dictionary(Of Integer, KeyValuePair(Of IntPtr, API.Messages.WndProc))

        Private CurrentWndProcs As New List(Of Integer)

        Private ReplacementWndProcDelegate As API.Messages.WndProc = AddressOf ReplacementWndProc

        Private Function ReplacementWndProc(ByVal hwnd As Integer, ByVal msg As API.Messages.WindowMessages, ByVal wparam As Integer, ByVal lparam As Integer) As Integer
            Dim ret = If( _
               OldWndProcs.ContainsKey(hwnd), _
               OldWndProcs(hwnd).Value, _
               Win32Window.DefWndProc).Invoke(hwnd, msg, wparam, lparam)
            If CurrentWndProcs.Contains(hwnd) Then Return ret 'Because Win32Window.Text causes message to be sent
            Try
                CurrentWndProcs.Add(hwnd)
                Dim Text$ = ""
                Try
                    Text = New Win32Window(hwnd).Text
                Catch : End Try
                Debug.Print("New: hwnd {0} text {1} message {2}", hwnd, Text, msg)
                Return ret
            Finally
                CurrentWndProcs.Remove(hwnd)
            End Try
        End Function
#Region "API"

        'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
        Private Declare Function CreateWindowEx Lib "user32" Alias "CreateWindowExA" (ByVal dwExStyle As Integer, ByVal lpClassName As String, ByVal lpWindowName As String, ByVal dwStyle As Integer, ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hWndParent As Integer, ByVal hMenu As Integer, ByVal hInstance As Integer, ByRef lpParam As CREATESTRUCT) As Integer
        Private Declare Function ShowWindow Lib "user32" (ByVal hwnd As Integer, ByVal nCmdShow As Integer) As Integer
        Private Declare Function DestroyWindow Lib "user32" (ByVal hwnd As Integer) As Integer
        Const WS_EX_STATICEDGE As Integer = &H20000
        Const WS_EX_TRANSPARENT As Integer = &H20
        Const WS_CHILD As Integer = &H40000000
        Const CW_USEDEFAULT As Integer = &H80000000
        Const SW_NORMAL As Short = 1
        Private Structure CREATESTRUCT
            Dim lpCreateParams As Integer
            Dim hInstance As Integer
            Dim hMenu As Integer
            Dim hWndParent As Integer
            Dim cy As Integer
            Dim cx As Integer
            Dim y As Integer
            Dim x As Integer
            Dim style As Integer
            Dim lpszName As String
            Dim lpszClass As String
            Dim ExStyle As Integer
        End Structure
        Dim mWnd As Integer
        Private Sub CreateNativeWindow()
            Dim CS As New CREATESTRUCT
            mWnd = CreateWindowEx(WS_EX_STATICEDGE Or WS_EX_TRANSPARENT, "STATIC", "Native label", WS_CHILD, 0, 0, 75, 20, TestForms(0).Handle, 0, VB6.GetHInstance.ToInt32, CS)
            ShowWindow(mWnd, SW_NORMAL)
            Dim MyLabel As New Win32Window(mWnd)
            MyLabel.Left = TestForms(0).ClientSize.Width - MyLabel.Width
            MyLabel.Top = TestForms(0).ClientSize.Height - MyLabel.Height
            AddHandler TestForms(0).FormClosed, AddressOf Me.DestroyNativeWindow
            cmbObject.Items.Add(MyLabel)
        End Sub
        Private Sub DestroyNativeWindow(ByVal sender As Form, ByVal e As FormClosedEventArgs)
            DestroyWindow(mWnd)
            Dim x As FormClosedEventHandler = AddressOf Me.DestroyNativeWindow
            RemoveHandler sender.FormClosed, x
        End Sub

#End Region

    End Class
End Namespace
#End If