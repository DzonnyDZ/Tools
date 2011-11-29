Namespace TextT.UnicodeT
    Public Class frmUcdTranslate

        Private Sub TableLayoutPanel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel1.Paint

        End Sub

        Private Sub cmdTargetPathBrowse_Click(sender As System.Object, e As System.EventArgs) Handles cmdTargetPathBrowse.Click
            If sfdSaveXml.ShowDialog = Windows.Forms.DialogResult.OK Then
                txtTargetPath.Text = sfdSaveXml.FileName
            End If
        End Sub
    End Class
End Namespace