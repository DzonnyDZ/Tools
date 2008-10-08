Imports System.Xml.Linq, Tools.CodeDomT, System.CodeDom
Namespace CodeDomT
    ''' <summary>Contains tests for <see cref="Tools.CodeDomT.Xml2CodeDom"/></summary>
    Public Class frmXml2CodeDom
        ''' <summary>Performs test</summary>
        Public Shared Sub Test()
            Dim inst As New frmXml2CodeDom
            inst.ShowDialog()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()
            InitializeComponent()
            Me.Icon = Tools.ResourcesT.ToolsIcon

            tmiVB.Tag = New Microsoft.VisualBasic.VBCodeProvider
            tmiCS.Tag = New Microsoft.CSharp.CSharpCodeProvider
            tmiCPPStandard.Tag = New Microsoft.VisualC.CppCodeProvider
            tmiCPP7.Tag = New Microsoft.VisualC.CppCodeProvider7
            tmiCPPVS.Tag = New Microsoft.VisualC.VSCodeProvider
            tmiJSP.Tag = New Microsoft.VJSharp.VJSharpCodeProvider
            tmiJS.Tag = New Microsoft.JScript.JScriptCodeProvider
        End Sub

        Private Sub cmdXML2CodeDOM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles cmdXML2CodeDOML.Click, cmdXML2CodeDOMR.Click
            Dim Source As TextBox = If(sender Is cmdXML2CodeDOML, txtLeft, txtRight)
            Dim Obj As codeobject
            Try
                Dim XML = XDocument.Parse(Source.Text)
                Dim X2C As New Xml2CodeDom
                Obj = X2C.Xml2CodeDom(XML)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                Exit Sub
            End Try
            tvwCodeDom.Nodes.Clear()
            tvwCodeDom.Nodes.Add(GetTreeNode(Obj))
        End Sub

        Private Function GetTreeNode(ByVal Obj As Object) As TreeNode
            Dim Node As New TreeNode
            Node.Text = If(Obj Is Nothing, "<nothing>", Obj.GetType.Name)
            If Node.Text.StartsWith("Code") Then Node.Text = Node.Text.Substring(4)
            Node.Tag = Obj
            Node.Nodes.Add("Please wait ...")
            Return Node
        End Function

        Private Sub tvwCodeDom_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwCodeDom.AfterExpand
            If e.Node.Nodes.Count = 1 AndAlso e.Node.Nodes(0).Tag Is Nothing Then
                e.Node.Nodes.Clear()
                e.Node.Nodes.AddRange(GetSubNodes(e.Node.Tag))
            End If
        End Sub

        Private Function GetSubNodes(ByVal Parent As Object) As TreeNode()
            Dim ret As New List(Of TreeNode)
            If TypeOf Parent Is ICollection Then
                For Each item In DirectCast(Parent, ICollection)
                    ret.Add(GetTreeNode(item))
                Next
            Else
                For Each prp In Parent.GetType.GetProperties()
                    If prp.GetGetMethod Is Nothing Then Continue For
                    If prp.GetIndexParameters.Length <> 0 Then Continue For
                    Dim Value As Object = prp.GetValue(Parent, Nothing)
                    Dim NewNode As TreeNode = GetTreeNode(Value)
                    NewNode.Text = prp.Name
                    If Value Is Nothing Then
                        NewNode.Nodes(0).Tag = "<nothing>"
                        NewNode.Nodes(0).Text = ""
                    ElseIf TypeOf Value Is String OrElse TypeOf Value Is Integer OrElse TypeOf Value Is Char OrElse TypeOf Value Is Byte OrElse TypeOf Value Is SByte OrElse TypeOf Value Is Short OrElse TypeOf Value Is UShort OrElse TypeOf Value Is Integer OrElse TypeOf Value Is UInteger OrElse TypeOf Value Is Long OrElse TypeOf Value Is ULong OrElse TypeOf Value Is Single OrElse TypeOf Value Is Double OrElse TypeOf Value Is Decimal OrElse TypeOf Value Is Boolean OrElse TypeOf Value Is Date OrElse TypeOf Value Is TimeSpan OrElse TypeOf Value Is IntPtr Then
                        NewNode.Nodes(0).Tag = Value
                        NewNode.Nodes(0).Text = Value.ToString
                    End If
                    ret.Add(NewNode)
                Next
            End If
            Return ret.ToArray
        End Function

        Private Sub cmdCodeDOM2XMLL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles cmdCodeDOM2XMLL.Click, cmdCodeDOM2XMLR.Click
            If tvwCodeDom.SelectedNode Is Nothing Then MsgBox("Select node, please", MsgBoxStyle.Exclamation, "No node selected") : Exit Sub
            If Not TypeOf tvwCodeDom.SelectedNode.Tag Is CodeObject Then MsgBox("Select node which represents CodeObject, please", MsgBoxStyle.Exclamation, "Bad node selected") : Exit Sub
            Dim Target As TextBox = If(sender Is cmdCodeDOM2XMLL, txtLeft, txtRight)
            Dim Source As CodeObject = tvwCodeDom.SelectedNode.Tag
            Dim C2X As New Xml2CodeDom
            Dim Xml As XDocument
            Try
                Xml = C2X.CodeDom2Xml(Source)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                Exit Sub
            End Try
            'Dim sb As New System.Text.StringBuilder
            'Dim w = System.Xml.XmlWriter.Create(sb)
            'Xml.WriteTo(w)
            'w.Flush()
            Target.Text = Xml.ToString
        End Sub

        Private Sub tvwCodeDom_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwCodeDom.AfterSelect
            prgCodeDom.SelectedObject = e.Node.Tag
            lblType.Text = If(e.Node.Tag Is Nothing, "", e.Node.Tag.GetType.FullName)
        End Sub

        Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseR.Click, cmdBrowseL.Click
            Dim Target As TextBox = If(sender Is cmdBrowseL, txtLeft, txtRight)
            If ofdOpen.ShowDialog = Windows.Forms.DialogResult.OK Then
                Try
                    Target.Text = My.Computer.FileSystem.ReadAllText(ofdOpen.FileName)
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                End Try
            End If
        End Sub

        Private Sub tmiLanguage_Click(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) _
            Handles tmiVB.Click, tmiJSP.Click, tmiJS.Click, tmiCS.Click, tmiCPPVS.Click, tmiCPPStandard.Click, tmiCPP7.Click
            If tvwCodeDom.SelectedNode Is Nothing Then MsgBox("Select node, please", MsgBoxStyle.Exclamation, "No node selected") : Exit Sub
            If Not TypeOf tvwCodeDom.SelectedNode.Tag Is CodeObject Then MsgBox("Select node which represents CodeObject, please", MsgBoxStyle.Exclamation, "Bad node selected") : Exit Sub
            Dim provider As Compiler.CodeDomProvider = sender.Tag
            Dim Obj As CodeObject = tvwCodeDom.SelectedNode.Tag
            Dim b As New System.Text.StringBuilder
            Dim w As New IO.StringWriter(b)
            Dim opt As New Compiler.CodeGeneratorOptions
            Try
                If TypeOf Obj Is CodeCompileUnit Then
                    provider.GenerateCodeFromCompileUnit(Obj, w, opt)
                ElseIf TypeOf Obj Is CodeNamespace Then
                    provider.GenerateCodeFromNamespace(Obj, w, opt)
                ElseIf TypeOf Obj Is CodeTypeDeclaration Then
                    provider.GenerateCodeFromType(Obj, w, opt)
                ElseIf TypeOf Obj Is CodeTypeMember Then
                    provider.GenerateCodeFromMember(Obj, w, opt)
                ElseIf TypeOf Obj Is CodeStatement Then
                    provider.GenerateCodeFromStatement(Obj, w, opt)
                ElseIf TypeOf Obj Is CodeExpression Then
                    provider.GenerateCodeFromExpression(Obj, w, opt)
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                Exit Sub
            End Try
            w.Flush()
            txtRight.Text = b.ToString
        End Sub

        Private Sub cmdInLanguage_Click(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdInLanguage.Click
            cmsLanguages.Show(sender, 0, sender.ClientSize.Height)
        End Sub

    End Class
End Namespace