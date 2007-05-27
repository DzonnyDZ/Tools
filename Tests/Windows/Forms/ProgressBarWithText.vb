'#If Config <= Release This conditional compilation is done in Tests.vbproj
Namespace WindowsT.FormsT
    ''' <summary>Tests <see cref="Tools.WindowsT.FormsT.ProgressBarWithText"/></summary>
    Public Class frmProgressBarWithText
        ''' <summary>Show test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmProgressBarWithText
            frm.ShowDialog()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub
        ''' <summary>Writes log</summary>
        Private Sub Log(ByVal text As String)
            If txtLog.Text <> "" Then txtLog.Text &= vbCrLf
            txtLog.Text &= text
            txtLog.Select(txtLog.Text.Length, 0)
            txtLog.ScrollToCaret()
        End Sub

        Private Sub pbtPgb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbtPgb.GotFocus
            Log("GotFocus")
        End Sub

        Private Sub pbtPgb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbtPgb.LostFocus
            Log("LostFocus")
        End Sub

        Private Sub pbtPgb_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbtPgb.MouseClick
            Log("MouseClick")
        End Sub

        Private Sub pbtPgb_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbtPgb.MouseDown
            Log("MouseDown")
        End Sub

        Private Sub pbtPgb_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbtPgb.MouseEnter
            Log("MouseEnter")
        End Sub

        Private Sub pbtPgb_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbtPgb.MouseHover
            Log("MouseHover")
        End Sub

        Private Sub pbtPgb_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbtPgb.MouseLeave
            Log("MouseLeave")
        End Sub

        Private Sub pbtPgb_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbtPgb.MouseMove
            Log("MouseMove")
        End Sub

        Private Sub pbtPgb_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbtPgb.MouseUp
            Log("MouseUp")
        End Sub

        Private Sub pbtPgb_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbtPgb.MouseWheel
            Log("MouseWheel")
        End Sub
        Private Sub pbtPgb_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles pbtPgb.KeyDown
            Log("KeyDown")
        End Sub
        Private Sub pbtPgb_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles pbtPgb.KeyUp
            Log("KeyUp")
        End Sub
        Private Sub pbtPgb_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles pbtPgb.KeyPress
            Log("KeyPress")
        End Sub
        Private Sub pbtPgb_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbtPgb.Click
            Log("Click")
        End Sub
        Private Sub pbtPgb_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles pbtPgb.DoubleClick
            Log("DoubleClick")
        End Sub
    End Class
End Namespace