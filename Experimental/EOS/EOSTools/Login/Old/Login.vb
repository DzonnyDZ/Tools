Imports System.Windows.Forms

Namespace Login
    ''' <summary>Obsahuje sd�len� metody pro p�ihla�ov�n�</summary>
    <Obsolete("Pou�ijte ActiveDirectory login")> _
    Public Module Login
        ''' <summary>Zobraz� okno pro p�ihl�en� u�ivatele do syst�mu</summary>
        ''' <param name="Owner">Voliteln� vlastn�k okna</param>
        ''' <param name="AllowTools">Zobrazit na okn� tla��tko "Mo�nosti"</param>
        ''' <remarks>Titulek p�ihla�ovac�ho dialogu</remarks>
        ''' <returns>P�ihl�en�ho u�ivatele nebo null pokud stiskl storno</returns>
        Public Function Login(Optional ByVal Owner As IWin32Window = Nothing, Optional ByVal AllowTools As Boolean = False, Optional ByVal WindowTitle As String = "Login") 'TODO:As User
            Dim dlg As New LoginDialog
            dlg.AdditionalButton = LoginButtonState.Visible
            dlg.UserName = My.User.Name.Split("\"c)(1)
            dlg.Text = WindowTitle
            AddHandler dlg.ButtonClick, AddressOf LoginButtonClick
            AddHandler dlg.OKClick, AddressOf VerifyPassword
            Try
                If dlg.ShowDialog(Owner) = DialogResult.OK Then
                    Return dlg.Tag
                Else
                    Return Nothing
                End If
            Finally
                RemoveHandler dlg.ButtonClick, AddressOf LoginButtonClick
                RemoveHandler dlg.OKClick, AddressOf VerifyPassword
            End Try
        End Function
        Private Sub LoginButtonClick(ByVal sender As LoginDialog, ByVal e As LoginDialog.LoginDialogEventArgs)
            'TODO: Login
            'Dim u As User
            'Try
            '    u = New OfflineUser(User.Verify(sender.UserName, sender.Password))
            '    'TODO: Dim frm As New frmUserTools(u)
            '    'frm.ShowDialog(e.Form)
            'Catch ex As UserNotFoundException
            '    MsgBox("Pro p��stup do mo�nost� mus�te zadat platn� u�ivatelsk� jm�no a heslo", MsgBoxStyle.Critical, "Chyba p�ihl�en�")
            '    Exit Sub
            'End Try
        End Sub

        Private Sub VerifyPassword(ByVal sender As LoginDialog, ByVal e As LoginDialog.FormCancelEventArgs)
            Try
                'TODO: sender.Tag = New OfflineUser(User.Verify(sender.UserName, sender.Password))
            Catch ex As Exception
                e.Cancel = True
                MsgBox("P�ihl�en� se nezda�ilo:" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Chyba p�ihl�en�")
            End Try
        End Sub

        ''' <summary>Ov��� p�ihl�en� u�ivatele do syst�mu</summary>
        ''' <param name="UserName">U�ivatelsk� jm�no</param>
        ''' <param name="Password">Heslo</param>
        ''' <returns>P�ihl�en�ho u�ivatele nebo null pokud ov��en� selhalo</returns>
        Public Function Login(ByVal UserName As String, ByVal Password As String) 'TODO:As User
            Try
                'Return New OfflineUser(User.Verify(UserName, Password))
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
    End Module
End Namespace