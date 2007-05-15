Imports System.Reflection
Friend Class frmTests
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Icon = Tools.Resources.ToolsIcon
        For Each nd As TreeNode In tvwMain.Nodes
            nd.Expand()
        Next nd
    End Sub

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
End Class
