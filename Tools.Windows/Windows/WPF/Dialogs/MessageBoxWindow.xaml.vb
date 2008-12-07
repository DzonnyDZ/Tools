#If Config <= Nightly Then  'Stage: Nightly
Namespace WindowsT.WPF.DialogsT
    Partial Class MessageBoxWindow
        Private Sub MsgBoxControl_TitleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MsgBoxControl.TitleChanged
            Me.Title = MsgBoxControl.Title
        End Sub
    End Class
End Namespace
#End If