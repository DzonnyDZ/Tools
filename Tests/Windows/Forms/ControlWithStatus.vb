#If ControlWithStatus Then
'#If Config <= Alpha This conditional compilation is done in Tests.vbproj
Namespace WindowsT.FormsT
    ''' <summary>Tests <see cref="Tools.WindowsT.FormsT.TransparentTextBox"/></summary>
    <Obsolete("Tools.WindowsT.FormsT.TransparentTextBox is obsolete")> _
    Public Class frmControlWithStatus
        ''' <summary>Show test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmControlWithStatus
            frm.ShowDialog()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub

        Private Sub TestControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TestControl1.Click
            Debug.Print("Click")
        End Sub

        Private Sub TestControl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TestControl1.KeyDown
            Debug.Print("KeyDown")
        End Sub

        Private Sub TestControl1_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TestControl1.MouseEnter
            Debug.Print("MouseEnter")
            Me.Capture = True
        End Sub

        Private Sub TestControl1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TestControl1.MouseLeave
            Debug.Print("MouseLeave")
        End Sub

        Private Sub TestControl1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TestControl1.MouseMove
            Debug.Print("MouseMove")
        End Sub

        Private Sub TestControl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        End Sub
    End Class

    Friend Class TestControl : Inherits UserControl
        Private WithEvents TextBox As New TextBox
        Private WithEvents Button As New Button
        Public Sub New()
            Me.Controls.Add(TextBox)
            Me.Controls.Add(Button)
            TextBox.Left = 0
            TextBox.Top = 0
            TextBox.Width = Me.ClientSize.Width / 3 * 2
            TextBox.Height = Me.ClientSize.Height
            TextBox.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
            Button.Left = Me.ClientSize.Width / 3 * 2
            Button.Width = Me.ClientSize.Width / 3
            Button.Height = Me.ClientSize.Height
            Button.Text = "Button"
            TextBox.Text = "TextBox"
            Button.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
        End Sub
    End Class
End Namespace
#End If