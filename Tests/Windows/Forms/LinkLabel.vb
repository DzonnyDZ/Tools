Namespace Windows.Forms
    ''' <summary>Tests <see cref="Tools.Windows.Forms.LinkLabel"/></summary>
    Public Class frmLinkLabel
        ''' <summary>Show test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmLinkLabel
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