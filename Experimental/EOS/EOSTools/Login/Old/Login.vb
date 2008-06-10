Imports System.Windows.Forms

Namespace Login
    ''' <summary>Obsahuje sdílené metody pro pøihlašování</summary>
    <Obsolete("Použijte ActiveDirectory login")> _
    Public Module Login
        ''' <summary>Zobrazí okno pro pøihlášení uživatele do systému</summary>
        ''' <param name="Owner">Volitelný vlastník okna</param>
        ''' <param name="AllowTools">Zobrazit na oknì tlaèítko "Možnosti"</param>
        ''' <remarks>Titulek pøihlašovacího dialogu</remarks>
        ''' <returns>Pøihlášeného uživatele nebo null pokud stiskl storno</returns>
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
            '    MsgBox("Pro pøístup do možností musíte zadat platné uživatelské jméno a heslo", MsgBoxStyle.Critical, "Chyba pøihlášení")
            '    Exit Sub
            'End Try
        End Sub

        Private Sub VerifyPassword(ByVal sender As LoginDialog, ByVal e As LoginDialog.FormCancelEventArgs)
            Try
                'TODO: sender.Tag = New OfflineUser(User.Verify(sender.UserName, sender.Password))
            Catch ex As Exception
                e.Cancel = True
                MsgBox("Pøihlášení se nezdaøilo:" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Chyba pøihlášení")
            End Try
        End Sub

        ''' <summary>Ovìøí pøihlášení uživatele do systému</summary>
        ''' <param name="UserName">Uživatelské jméno</param>
        ''' <param name="Password">Heslo</param>
        ''' <returns>Pøihlášeného uživatele nebo null pokud ovìøení selhalo</returns>
        Public Function Login(ByVal UserName As String, ByVal Password As String) 'TODO:As User
            Try
                'Return New OfflineUser(User.Verify(UserName, Password))
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
    End Module
End Namespace