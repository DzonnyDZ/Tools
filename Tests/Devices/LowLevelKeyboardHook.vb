Imports Tools.DevicesT
Imports mBox = Tools.WindowsT.IndependentT.MessageBox
Namespace DevicesT
    ''' <summary>Tests <see cref="LowLevelKeyboardHook"/></summary>
    Friend Class frmLowLevelKeyboardHook : Inherits Form
        ''' <summary>Hook class instance</summary>
        Protected WithEvents Hook As API.Hooks.Win32Hook = New LowLevelKeyboardHook

        ''' <summary>CTor</summary>
        Public Sub New()
            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon
            Threading.Thread.CurrentThread.Name = "MainThread"
            AddHandler DirectCast(Hook, LowLevelKeyboardHook).KeyEvent, AddressOf Hook_KeyEvent
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
            ApplyEnabled()
        End Sub
        Private Sub ApplyEnabled()
            cmdRegister.Enabled = Not Hook.Registered
            cmdRegisterAsync.Enabled = Not Hook.Registered
            cmdUnregister.Enabled = Hook.Registered
        End Sub
        Private Sub cmdRegisterAsync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRegisterAsync.Click
            Try
                Hook.RegisterAsyncHook()
            Catch ex As Exception
                mBox.Error_X(ex)
            End Try
            Threading.Thread.Sleep(100)
            ApplyEnabled()
        End Sub

        Private Sub cmdUnregister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnregister.Click
            Try
                Hook.UnregisterHook()
            Catch ex As Exception
                mBox.Error_X(ex)
            End Try
            ApplyEnabled()
        End Sub

        Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
            lstMessages.Items.Clear()
        End Sub
        ''' <summary>Called by handler of <see cref="LowLevelKeyboardHook.KeyEvent"/> in context of window thread</summary>
        Private Sub Hook_KeyEvent(ByVal e As LowLevelKeyEventArgs, ByVal CallingThread$)
            lstMessages.Items.Add( _
               String.Format("{0} key {1}({1:d}) scan {2} injected {3} extended {4} ALT {6} @{5}", e.Action, e.Key, e.ScanCode, e.IsInjected, e.IsExtended, CallingThread, e.AltState))
            lstMessages.SelectedIndex = lstMessages.Items.Count - 1
        End Sub

        Private Sub Hook_KeyEvent(ByVal sender As LowLevelKeyboardHook, ByVal e As LowLevelKeyEventArgs)
            If Me.InvokeRequired Then
                Me.Invoke(New Action(Of LowLevelKeyEventArgs, String)(AddressOf Hook_KeyEvent), e, Threading.Thread.CurrentThread.Name)
            Else
                Hook_KeyEvent(e, Threading.Thread.CurrentThread.Name)
            End If
            e.Handled = GetHandled()
            e.Suppress = GetSuppress()
        End Sub
        ''' <summary>Gets <see cref="chkSuppress"/>.<see cref="CheckBox.Checked">Checked</see> in thread safe way</summary>
        ''' <returns><see cref="chkSuppress"/>.<see cref="CheckBox.Checked">Checked</see></returns>
        Protected Function GetSuppress() As Boolean
            If Me.InvokeRequired Then
                Return Me.Invoke(New Func(Of Boolean)(AddressOf GetSuppress))
            Else
                Return chkSuppress.CheckState
            End If
        End Function
        ''' <summary>Gets <see cref="chkHandled"/>.<see cref="CheckBox.Checked">Checked</see> in thread safe way</summary>
        ''' <returns><see cref="chkHandled"/>.<see cref="CheckBox.Checked">Checked</see></returns>
        Protected Function GetHandled() As Boolean
            If Me.InvokeRequired Then
                Return Me.Invoke(New Func(Of Boolean)(AddressOf GetHandled))
            Else
                Return chkHandled.Checked
            End If
        End Function
    End Class
End Namespace
