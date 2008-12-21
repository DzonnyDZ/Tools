Imports Tools, Tools.ReflectionT, System.Linq
''' <summary>Browses visual tree of <see cref="Control"/></summary>
Public Class ContentTreeBase
    Protected Overridable Sub tvwTree_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwTree.AfterSelect
    End Sub
End Class