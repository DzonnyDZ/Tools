Imports Tools.GeneratorsT, MBox = Tools.WindowsT.IndependentT.MessageBox
Namespace GeneratorsT
    Public Class frmCustomToolBase
        Public Sub New()
            InitializeComponent()
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub
        Public Shared Sub Test()
            Dim frm As New frmCustomToolBase
            frm.ShowDialog()
        End Sub

        Private Sub tsbGetLanguages_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbGetLanguages.Click
            Try
                Dim Langs = CustomToolBase.GetLanguages(txtVersion.Text)
                tvwNodes.Nodes.Clear()
                For Each lang In Langs
                    Dim Node = tvwNodes.Nodes.Add(lang.ToString)
                    Try
                        Node.Text = lang.Name
                    Catch : End Try
                    If Node.Text = "" Then Node.Text = lang.Guid.ToString
                    Node.Tag = lang
                    Node.Nodes.Add("Please wait ...")
                Next
            Catch ex As Exception
                MBox.Error_X(ex)
            End Try
        End Sub

        Private Sub tvwNodes_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwNodes.AfterExpand
            If e.Node.Nodes.Count = 1 AndAlso e.Node.Nodes(0).Tag Is Nothing Then
                If TypeOf e.Node.Tag Is VisualStudioCustomToolLanguage Then
                    e.Node.Nodes.Clear()
                    For Each ct In DirectCast(e.Node.Tag, VisualStudioCustomToolLanguage).GetCustomTools
                        Dim node = e.Node.Nodes.Add(ct.Name)
                        node.Tag = ct
                    Next
                End If
            End If
        End Sub

        Private Sub tvwNodes_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwNodes.AfterSelect
            prgProperties.SelectedObject = e.Node.Tag
            tsbInstantiateCustomTool.Enabled = TypeOf e.Node.Tag Is VisualStudioCustomToolRegistration AndAlso e.Node.Nodes.Count = 0
        End Sub

        Private Sub tsbInstantiateCustomTool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbInstantiateCustomTool.Click
            Try
                Dim ct = DirectCast(tvwNodes.SelectedNode.Tag, VisualStudioCustomToolRegistration).CreateInstance
                Dim Node = tvwNodes.SelectedNode.Nodes.Add(ct.GetType.FullName)
                Node.Tag = ct.GetType
                tvwNodes.SelectedNode = Node
            Catch ex As Exception
                MBox.Error_X(ex)
            End Try
        End Sub
    End Class
End Namespace