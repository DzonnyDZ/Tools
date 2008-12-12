#If Config <= Nightly Then  'Stage: Nightly
Namespace WindowsT.WPF.DialogsT
    ''' <summary>Window that impements WPF message box</summary>
    Partial Friend NotInheritable Class MessageBoxWindow
        Private Sub MsgBoxControl_TitleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MsgBoxControl.TitleChanged
            If MsgBoxControl.Title Is Nothing Then Me.Title = "" Else Me.Title = MsgBoxControl.Title
        End Sub
    End Class
End Namespace
#End If