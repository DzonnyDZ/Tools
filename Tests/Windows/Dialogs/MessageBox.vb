Namespace WindowsT.DialogsT
    Public Class frmMessageBox
        Public Shared Sub Test()
            Dim frm As New frmMessageBox
            frm.ShowDialog()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()
            InitializeComponent()
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub

        Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
            MsgBox("MsgBox", NumericUpDown1.Value)
        End Sub

        Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
            MessageBox.Show("MessageBox", "MessageBox", CType(NumericUpDown1.Value, MessageBoxButtons))
        End Sub

        Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
            System.Windows.MessageBox.Show("MessageBox", "MessageBox", CType(NumericUpDown1.Value, System.Windows.MessageBoxButton))
        End Sub
    End Class
End Namespace