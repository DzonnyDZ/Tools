Imports Tools.DevicesT
Imports mBox = Tools.WindowsT.IndependentT.MessageBox
Namespace DevicesT
    ''' <summary>Tests <see cref="LowLevelKeyboardHook"/></summary>
    Friend Class frmLowLevelKeyboardHook : Inherits Form
        Private WithEvents Hook As New LowLevelKeyboardHook

        ''' <summary>CTor</summary>
        Public Sub New()
            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon
            Threading.Thread.CurrentThread.Name = "MainThread"
        End Sub
        ''' <summary>Shows test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmLowLevelKeyboardHook
            frm.ShowDialog()
        End Sub

        Private Sub cmdRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRegister.Click
            Try
                Hook.RegisterHook()
            Catch ex As Exception
                mBox.Error_X(ex)
            End Try
        End Sub

        Private Sub cmdRegisterAsync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRegisterAsync.Click
            Try
                Hook.RegisterAsyncHook()
            Catch ex As Exception
                mBox.Error_X(ex)
            End Try
        End Sub

        Private Sub cmdUnregister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnregister.Click
            Try
                Hook.UnregisterHook()
            Catch ex As Exception
                mBox.Error_X(ex)
            End Try
        End Sub

        Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
            lstMessages.Items.Clear()
        End Sub

        Private Sub Hook_KeyEvent(ByVal e As LowLevelKeyEventArgs, ByVal CallingThread$)
            lstMessages.Items.Add( _
               String.Format("{0} key {1}({1:d}) scan {2} injected {3} extended {4} @{5}", e.Action, e.Key, e.ScanCode, e.IsInjected, e.IsExtended, CallingThread))
            lstMessages.SelectedIndex = lstMessages.Items.Count - 1
        End Sub

        Private Sub Hook_KeyEvent(ByVal sender As LowLevelKeyboardHook, ByVal e As LowLevelKeyEventArgs) Handles Hook.KeyEvent
            If Me.InvokeRequired Then
                Me.Invoke(New Action(Of LowLevelKeyEventArgs, String)(AddressOf Hook_KeyEvent), e, Threading.Thread.CurrentThread.Name)
            Else
                Hook_KeyEvent(e, Threading.Thread.CurrentThread.Name)
            End If
        End Sub
    End Class
End Namespace
