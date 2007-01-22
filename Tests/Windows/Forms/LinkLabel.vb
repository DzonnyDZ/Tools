Namespace Windows.Forms
    ''' <summary>Tests <see cref="Tools.Windows.Forms.LinkLabel"/></summary>
    Public Class frmLinkLabel
        ''' <summary>Show test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmLinkLabel
            frm.ShowDialog()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.Resources.ToolsIcon
        End Sub

        Private Sub llbLabel_LinkClicked(ByVal sender As Tools.Windows.Forms.LinkLabel, ByVal e As Tools.Windows.Forms.LinkLabel.LinkClickedEventArgs) Handles llbLabel.LinkClicked
            If Not TypeOf e.Item Is Tools.Windows.Forms.LinkLabel.AutoLink Then
                Try
                    Process.Start(CStr(e.Item.LinkData))
                    e.Item.Visited = True
                Catch : End Try
            End If
        End Sub
    End Class
End Namespace