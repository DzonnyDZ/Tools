Imports Tools.Windows.Forms
Namespace Windows.Forms
#If Config <= Beta Then
    ''' <summary>Test from for <see cref="EncodingSelector"/></summary>
    Friend Class frmEncodingSelector
        ''' <summary>Show test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmEncodingSelector
            frm.ShowDialog()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.Resources.ToolsIcon
        End Sub

        Private Sub ensSelect_SelectedIndexChanged(ByVal sender As Tools.Windows.Forms.EncodingSelector, ByVal e As System.EventArgs) Handles ensSelect.SelectedIndexChanged
            If ensSelect.SelectedEncoding IsNot Nothing Then
                lblSelected.Text = ensSelect.SelectedEncoding.DisplayName
            Else
                lblSelected.Text = ""
            End If
        End Sub

        Private Sub ensSelect_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ensSelect.KeyDown
            If e.KeyCode = Keys.Delete AndAlso ensSelect.SelectedEncoding IsNot Nothing Then _
                ensSelect.RemoveEncoding(ensSelect.SelectedEncoding)
        End Sub

        Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
            ensSelect.RefreshEncodings()
        End Sub

        Private Sub ensSelect_ItemClick(ByVal sender As Tools.Windows.Forms.EncodingSelector, ByVal e As Tools.Windows.Forms.EncodingSelector.EncodingSelectorItemClickEventArgs) Handles ensSelect.ItemDoubleClick
            MsgBox(e.Item.DisplayName)
        End Sub
    End Class
#End If
End Namespace