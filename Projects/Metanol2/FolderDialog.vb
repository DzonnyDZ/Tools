Public Class frmFolderDialog

    Public Sub New()
        InitializeComponent()
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
    Public Sub New(ByVal Path$)
        Me.New()
        txtPath.Text = Path
    End Sub
    Private Sub cmdPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPath.Click
        Try
            fbdBrowse.SelectedPath = txtPath.Text
        Catch : End Try
        If fbdBrowse.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtPath.Text = fbdBrowse.SelectedPath
        End If
    End Sub
End Class