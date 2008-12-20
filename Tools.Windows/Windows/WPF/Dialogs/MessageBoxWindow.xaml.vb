#If Config <= Nightly Then  'Stage: Nightly
Namespace WindowsT.WPF.DialogsT
    ''' <summary>Window that impements WPF message box</summary>
    Partial Friend NotInheritable Class MessageBoxWindow
        Private Sub MsgBoxControl_FlowDirectionChanged(ByVal sender As Object, ByVal e As InteropT.DependencyPropertyChangedEventArgsEventArgs) Handles MsgBoxControl.FlowDirectionChanged
            Me.FlowDirection = MsgBoxControl.FlowDirection
        End Sub
        Private Sub MsgBoxControl_TitleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MsgBoxControl.TitleChanged
            If MsgBoxControl.Title Is Nothing Then Me.Title = "" Else Me.Title = MsgBoxControl.Title
        End Sub
        Private Sub MessageBoxWindow_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
            If (MsgBoxControl.MessageBox.Options And IndependentT.MessageBox.MessageBoxOptions.BringToFront) = IndependentT.MessageBox.MessageBoxOptions.BringToFront Then
                Me.Focus()
            End If
        End Sub
        ''' <summary>Raises the <see cref="E:System.Windows.Window.Closing" /> event.</summary>
        ''' <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
        Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
            If Not MsgBoxControl.MessageBox.AllowClose Then
                e.Cancel = True
                Exit Sub
            End If
            MyBase.OnClosing(e)
        End Sub
    End Class
End Namespace
#End If