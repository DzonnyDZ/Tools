Imports Tools.CollectionsT.GenericT, Tools.XmlT.XPathT, System.Xml.XPath
Namespace XmlT.XPathT
    '#If Config <= Nightly This conditional compilation is done in Tests.vbproj
    ''' <summary>Test Form for testing <see cref="HashTable(Of String)"/></summary>
    Friend Class frmXPathObjectNavigator
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon

            cmbSource.Items.Add(New List(Of String)(New String() {"Item1", "Item2", "Item3"}))
            cmbSource.Items.Add(New String() {"Item1", "Item2", "Item3"})
            cmbSource.Items.Add("String item")
            cmbSource.Items.Add(New TimeSpan(14, 10, 15))
            cmbSource.Items.Add(New Tools.DataStructuresT.GenericT.Pair(Of String, Tools.DataStructuresT.GenericT.Pair(Of Long, Char))("String item", New Tools.DataStructuresT.GenericT.Pair(Of Long, Char)(1435L, "\"c)))
            cmbSource.SelectedIndex = 0
        End Sub
      
        ''' <summary>Displays test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmXPathObjectNavigator
            frm.ShowDialog()
        End Sub

        Private Sub cmbSource_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSource.SelectedIndexChanged
            prgSource.SelectedObject = cmbSource.SelectedItem
        End Sub

        Private Sub cmdDo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDo.Click
            tvwResult.Nodes.Clear()
            Dim XObj As New XPathObjectNavigator(cmbSource.SelectedItem)
            Dim ret As Object = XObj.Evaluate(txtQuery.Text)
            If TypeOf ret Is Boolean OrElse TypeOf ret Is String OrElse TypeOf ret Is Double Then
                tvwResult.Nodes.Add(ret.ToString).Tag = ret
            ElseIf TypeOf ret Is XPathNodeIterator Then
                FillWi(tvwResult.Nodes, DirectCast(ret, XPathNodeIterator))
            End If
        End Sub
        Private Sub FillWi(ByVal Parent As TreeNodeCollection, ByVal it As XPathNodeIterator)
            Do While it.MoveNext
                FillWi(Parent, it.Current)
            Loop
        End Sub
        Private Sub FillWi(ByVal Parent As TreeNodeCollection, ByVal Nav As XPathNavigator)
            Dim Node As TreeNode = Parent.Add(Nav.Name)
            Select Case Nav.NodeType
                Case XPathNodeType.Attribute
                    Node.Text = String.Format("@{0} = ""{1}""", Nav.Name, Nav.Value)
                Case XPathNodeType.Comment
                    Node.Text = String.Format("<!--{0}-->", Nav.Value)
                Case XPathNodeType.Element
                    Node.Text = String.Format("<{0}>{1}</{0}>", Nav.Name, Nav.Value)
                Case XPathNodeType.Namespace
                    Node.Text = String.Format("{0}:", Nav.Name)
                Case XPathNodeType.ProcessingInstruction
                    Node.Text = String.Format("<?{0} {1}?>", Nav.Name, Nav.Value)
                Case XPathNodeType.Root
                    Node.Text = String.Format("/ {0} ""{1}""", Nav.Name, Nav.Value)
                Case XPathNodeType.SignificantWhitespace
                    Node.Text = String.Format("SignificantWhitespace ""{0}""", Nav.Value.Replace(vbCr, "&cr;").Replace(vbLf, "&lf").Replace(vbTab, "&tab;"))
                Case XPathNodeType.Text
                    Node.Text = String.Format("""{0}""", Nav.Value.Replace(vbCr, "&cr;").Replace(vbLf, "&lf").Replace(vbTab, "&tab;"))
                Case XPathNodeType.Whitespace
                    Node.Text = String.Format("Whitespace ""{0}""", Nav.Value.Replace(vbCr, "&cr;").Replace(vbLf, "&lf").Replace(vbTab, "&tab;"))
                Case Else
                    Node.Text = String.Format("? {0} ""{1}""", Nav.Name, Nav.Value)
            End Select
            Node.Tag = Nav.Clone
            Dim NavClone As XPathNavigator = Nav.Clone
            If NavCLone.MoveToFirstAttribute Then
                Do
                    FillWi(Node.Nodes, NavClone.Clone)
                Loop While NavClone.MoveToNextAttribute
            End If
            If Nav.MoveToFirstChild Then
                Do
                    FillWi(Node.Nodes, Nav.Clone)
                Loop While Nav.MoveToNext
            End If
        End Sub

        Private Sub cmdWalk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWalk.Click
            tvwResult.Nodes.Clear()
            Dim XObj As New XPathObjectNavigator(cmbSource.SelectedItem)
            FillWi(tvwResult.Nodes, XObj)
        End Sub
    End Class
End Namespace