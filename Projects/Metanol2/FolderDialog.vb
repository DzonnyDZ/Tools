''' <summary>Dialog for selecting folder either by typing or by browsing</summary>
Friend Class frmFolderDialog
    ''' <summary>CTor</summary>
    Public Sub New()
        InitializeComponent()
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
    ''' <summary>CTor with path</summary>
    ''' <param name="Path">Path to be shown by default</param>
    Public Sub New(ByVal Path$)
        Me.New()
        txtPath.Text = Path
    End Sub
    Private Sub cmdPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPath.Click
        Try
            fbdBrowse.SelectedPath = txtPath.Text
        Catch : End Try
        If fbdBrowse.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                txtPath.Text = fbdBrowse.SelectedPath
            Catch ex As Exception
                WindowsT.IndependentT.MessageBox.[Error_XT](ex, My.Resources.Error_)
            End Try
        End If
    End Sub
End Class