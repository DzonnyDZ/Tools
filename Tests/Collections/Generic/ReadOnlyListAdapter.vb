Imports Tools.Collections.Generic
Namespace Collections.Generic
    Public Class frmReadOnlyListAdapter

        Public Overloads Shared Sub Test()
            Dim frm As New frmReadOnlyListAdapter
            frm.ShowDialog()
        End Sub

        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.Resources.ToolsIcon
        End Sub
    End Class
End Namespace