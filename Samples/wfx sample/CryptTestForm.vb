Imports mBox = Tools.WindowsT.IndependentT.MessageBox
''' <summary>Form for testing Total Commander secure password store</summary>
Friend Class CryptTestForm
    ''' <summary>CTor - creates a new instance of the <see cref="CryptTestForm"/> class</summary>
    ''' <param name="plugin">Current instance of the plugin</param>
    ''' <exception cref="ArgumentNullException"><paramref name="plugin"/> is null</exception>
    Public Sub New(plugin As SampleFileSystemPlugin)
        If plugin Is Nothing Then Throw New ArgumentNullException("plugin")
        Me.plugin = plugin
        InitializeComponent()
    End Sub
    ''' <summary>Current instance of the plugin</summary>
    Private plugin As SampleFileSystemPlugin

    Private Sub cmdGet_Click(sender As System.Object, e As System.EventArgs) Handles cmdGet.Click
        Dim pwd As String
        Try
            pwd = plugin.LoadPassword(txtConnection.Text, True)
        Catch ex As Exception
            mBox.Error_XW(ex, Me)
            Exit Sub
        End Try
        mBox.MsgBoxFW("Password is '{0}'", MsgBoxStyle.Information, "Password", Me, pwd)
    End Sub

    Private Sub cmdGetNoUI_Click(sender As System.Object, e As System.EventArgs) Handles cmdGetNoUI.Click
        Dim pwd As String
        Try
            pwd = plugin.LoadPassword(txtConnection.Text, False)
        Catch ex As CryptException When ex.Reason = CryptResult.NoMasterPassword
            mBox.Error_XPTIBWO(ex, "You should consider allowing user interface", "Error", Owner:=Me)
            Exit Sub
        Catch ex As Exception
            mBox.Error_XW(ex, Me)
            Exit Sub
        End Try
        mBox.MsgBoxFW("Password is '{0}'", MsgBoxStyle.Information, "Password", Me, pwd)
    End Sub

    Private Sub cmdDel_Click(sender As System.Object, e As System.EventArgs) Handles cmdDel.Click
        Try
            plugin.DeletePassword(txtConnection.Text)
        Catch ex As Exception
            mBox.Error_XW(ex, Me)
            Exit Sub
        End Try
        mBox.MsgBox("Password deleted", MsgBoxStyle.Information, "Password", Me)
    End Sub

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Try
            plugin.SavePassword(txtConnection.Text, txtPassword.Text)
        Catch ex As Exception
            mBox.Error_XW(ex, Me)
            Exit Sub
        End Try
        mBox.MsgBox("Password saved", MsgBoxStyle.Information, "Password", Me)
    End Sub

    Private Sub cmdMove_Click(sender As System.Object, e As System.EventArgs) Handles cmdMove.Click
        Try
            plugin.MovePassword(txtConnection.Text, txtOther.Text, True)
        Catch ex As Exception
            mBox.Error_XW(ex, Me)
            Exit Sub
        End Try
        mBox.MsgBox("Password moved", MsgBoxStyle.Information, "Password", Me)
    End Sub

    Private Sub cmdCopy_Click(sender As System.Object, e As System.EventArgs) Handles cmdCopy.Click
        Try
            plugin.MovePassword(txtConnection.Text, txtOther.Text, False)
        Catch ex As Exception
            mBox.Error_XW(ex, Me)
            Exit Sub
        End Try
        mBox.MsgBox("Password copyied", MsgBoxStyle.Information, "Password", Me)
    End Sub

    Private allowClose As Boolean
    Private Sub cmdClose_Click(sender As System.Object, e As System.EventArgs) Handles cmdClose.Click
        allowClose = True
        Me.Close()
    End Sub

    Private Sub CryptTestForm_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason = System.Windows.Forms.CloseReason.None AndAlso Not allowClose Then e.Cancel = True
    End Sub

End Class