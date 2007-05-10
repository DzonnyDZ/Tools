Imports Tools.Windows.Forms.Utilities, Tools.Drawing
Namespace Windows.Forms.Utilities
    ''' <summary>Testing <see cref="Tools.Windows.Forms.Utilities.LinkProperties"/></summary>
    Public Class frmLinkProperties
        ''' <summary>Show test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmLinkProperties
            frm.ShowDialog()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.Resources.ToolsIcon

            lblLink.ForeColor = LinkProperties.Color
            lblVisited.ForeColor = LinkProperties.VisitedColor
            lblLink.Font = LinkProperties.LinkFontDecoration(lblLink.Font)
            lblVisited.Font = LinkProperties.LinkFontDecoration(lblVisited.Font)
            lblLink2.ForeColor = SystemColorsExtension.BrowserLink
            lblVisited2.ForeColor = SystemColorsExtension.BrowserVisitedLink
        End Sub

        Private Sub lblLink_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblVisited.MouseEnter, lblLink.MouseEnter
            With DirectCast(sender, Label)
                .Font = LinkProperties.HoverLinkFontDecoration(.Font)
                .ForeColor = LinkProperties.HoveredColor
            End With
        End Sub

        Private Sub lblLink_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblVisited.MouseLeave, lblLink.MouseLeave
            With DirectCast(sender, Label)
                .Font = LinkProperties.LinkFontDecoration(.Font)
                If sender Is lblLink Then
                    .ForeColor = LinkProperties.Color
                Else
                    .ForeColor = LinkProperties.VisitedColor
                End If
            End With
        End Sub

        Private Sub lblLink2_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblLink2.MouseEnter, lblVisited2.MouseEnter
            With DirectCast(sender, Label)
                .ForeColor = SystemColorsExtension.BrowserActiveLink
            End With
        End Sub

        Private Sub lblLink2_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblLink2.MouseLeave, lblVisited2.MouseLeave
            With DirectCast(sender, Label)
                If sender Is lblLink2 Then
                    .ForeColor = SystemColorsExtension.BrowserLink
                Else
                    .ForeColor = SystemColorsExtension.BrowserVisitedLink
                End If
            End With
        End Sub
    End Class
End Namespace