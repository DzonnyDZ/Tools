Imports System.Reflection, <xmlns="http://dzonny.cz/xml/ĐTools/Tests/Tree">
Imports System.Linq, Tools.CollectionsT.SpecializedT
Imports System.Xml.Linq

Friend Class frmTests
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Icon = Tools.ResourcesT.ToolsIcon
        LoadXml()
        For Each nd As TreeNode In tvwMain.Nodes
            nd.Expand()
        Next nd
    End Sub

    Private Sub LoadXml()
        tvwMain.Nodes.Clear()
        Dim doc = XDocument.Load(IO.Path.Combine(My.Application.Info.DirectoryPath, "Tree.xml"))
        tvwMain.Nodes.AddRange( _
            (From node In doc.<tree>.<node> Order By node.@name Select Xml2Tree(node)).ToArray)
    End Sub
    Private Function Xml2Tree(ByVal node As XElement) As TreeNode
        Dim ret As New TreeNode() With {.Text = node.@name, .Name = node.@name, .ImageKey = node.@image, .SelectedImageKey = node.@image, .Tag = node.@tag}
        ret.Nodes.AddRange((From inndernode In node.<node> Order By node.@name Select Xml2Tree(inndernode)).ToArray)
        Return ret
    End Function

    Private Sub tvwMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tvwMain.KeyDown
        If e.KeyCode = Keys.Return AndAlso Not e.Control AndAlso Not e.Shift AndAlso Not e.Alt AndAlso tvwMain.SelectedNode IsNot Nothing AndAlso tvwMain.SelectedNode.Tag IsNot Nothing AndAlso TypeOf tvwMain.SelectedNode.Tag Is String Then
            OpenNode(tvwMain.SelectedNode.Tag)
        End If
    End Sub
    Private Sub tvwMain_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwMain.NodeMouseDoubleClick
        If e.Node.Tag IsNot Nothing AndAlso TypeOf e.Node.Tag Is String Then
            OpenNode(e.Node.Tag)
        End If
    End Sub
    Private Sub OpenNode(ByVal NodeTag As String)
        Dim parts As String() = NodeTag.Split("."c)
        Dim TypeName As String = ""
        For i As Integer = 0 To parts.Length - 2
            If TypeName <> "" Then TypeName &= "."c
            TypeName &= parts(i)
        Next i
        Dim Method As String = parts(parts.Length - 1)
        Dim T As Type
        Try
            T = Type.GetType(TypeName, True, False)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Exit Sub
        End Try
        Try
            T.InvokeMember(Method, _
                    BindingFlags.Static Or BindingFlags.Public Or BindingFlags.InvokeMethod, _
                    Type.DefaultBinder, Nothing, Nothing)
        Catch ex As Exception
            MsgBox(BuildException(ex), MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    ''' <summary>Contactenates messages from <paramref name="ex"/> and all its <see cref="Exception.InnerException"/>s</summary>
    ''' <param name="ex"><see cref="Exception"/> to contactenate messages from</param>
    Private Function BuildException(ByVal ex As Exception) As String
        Dim ret As String = ex.Message
        If ex.InnerException IsNot Nothing Then ret &= ":" & vbCrLf & BuildException(ex.InnerException)
        Return ret
    End Function

    Private Sub cmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExport.Click
        If sfdXml.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                Dim doc = _
                    <?xml version="1.0" encoding="utf-8"?>
                    <tree>
                        <%= From node In tvwMain.Nodes.AsTypeSafe Select NodeFromNode(node) %>
                    </tree>
                doc.Save(sfdXml.FileName)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
            End Try
        End If
    End Sub

    Private Function NodeFromNode(ByVal Node As TreeNode) As XElement
        Return <node name=<%= Node.Name %> image=<%= Node.ImageKey %> tag=<%= Node.Tag %>>
                   <%= From inndernode In Node.Nodes.AsTypeSafe Select NodeFromNode(inndernode) %>
               </node>
    End Function

End Class
