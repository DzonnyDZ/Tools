Imports Tools.DevicesT
Imports mBox = Tools.WindowsT.IndependentT.MessageBox
Namespace DevicesT
    ''' <summary>Tests <see cref="LowLevelMouseHook"/></summary>
    Friend Class frmLowLevelMouseHook : Inherits frmLowLevelKeyboardHook
        ''' <summary>Hook class instance</summary>
        Private WithEvents MyHook As New LowLevelMouseHook
        ''' <summary>CTor</summary>
        Public Sub New()
            Me.Text = String.Format("Testing {0}", GetType(LowLevelMouseHook).FullName)
            Hook.Dispose()
            Hook = MyHook
        End Sub
        ''' <summary>Shows test form</summary>
        Public Shared Shadows Sub Test()
            Dim frm As New frmLowLevelMouseHook
            frm.ShowDialog()
        End Sub


        Private Sub MyHook_ButtonEvent(ByVal sender As LowLevelMouseHook, ByVal e As LowLevelMouseButtonEventArgs) Handles MyHook.ButtonEvent
            e.Handled = GetHandled()
            e.Suppress = GetSuppress()
            If Me.InvokeRequired Then
                Me.Invoke(New EventHandler(Of LowLevelMouseHook, LowLevelMouseButtonEventArgs)(AddressOf MyHook_ButtonEvent), sender, e)
                Exit Sub
            End If
            lstMessages.Items.Add(String.Format( _
                "Button {0} {1} x={2} y={3} injected {4}", e.Button, If(e.MouseUp, "Up", "Down"), e.Location.X, e.Location.Y, e.IsInjected))
            lstMessages.SelectedIndex = lstMessages.Items.Count - 1
        End Sub

        Private Sub MyHook_MouseMove(ByVal sender As LowLevelMouseHook, ByVal e As LowLevelMouseEventArgs) Handles MyHook.MouseMove
            e.Handled = GetHandled()
            e.Suppress = GetSuppress()
            If Me.InvokeRequired Then
                Me.Invoke(New EventHandler(Of LowLevelMouseHook, LowLevelMouseEventArgs)(AddressOf MyHook_MouseMove), sender, e)
                Exit Sub
            End If
            lstMessages.Items.Add(String.Format( _
                  "Move x={0} y={1} injected={2}", e.Location.X, e.Location.Y, e.IsInjected))
            lstMessages.SelectedIndex = lstMessages.Items.Count - 1
        End Sub

        Private Sub MyHook_Wheel(ByVal sender As LowLevelMouseHook, ByVal e As LowLevelMouseWheelEventArgs) Handles MyHook.Wheel
            e.Handled = GetHandled()
            e.Suppress = GetSuppress()
            If Me.InvokeRequired Then
                Me.Invoke(New EventHandler(Of LowLevelMouseHook, LowLevelMouseWheelEventArgs)(AddressOf MyHook_Wheel), sender, e)
                Exit Sub
            End If
            lstMessages.Items.Add(String.Format( _
                "Wheel {0} horizontal {1} x={2} y={3} injected {4}", e.Delta, e.Horizontal, e.Location.X, e.Location.Y, e.IsInjected))
            lstMessages.SelectedIndex = lstMessages.Items.Count - 1
        End Sub
    End Class
End Namespace
