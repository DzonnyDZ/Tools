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
    Private Sub tvwMain_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwMain.NodeMouseDoubleClick
        If e.Node.Tag IsNot Nothing AndAlso TypeOf e.Node.Tag Is String Then
            Dim parts As String() = CStr(e.Node.Tag).Split("."c)
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
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub
End Class
