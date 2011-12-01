Public Class frmTranslateBlockNames
    Public Sub New(data As IEnumerable)
        InitializeComponent()
        dgwData.DataSource = data
    End Sub


    Private Sub cmdOK_Click(sender As System.Object, e As System.EventArgs) Handles cmdOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class