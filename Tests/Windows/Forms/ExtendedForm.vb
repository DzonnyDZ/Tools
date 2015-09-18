Namespace WindowsT.FormsT
    '#If TrueStage conditional compilation of this file is set in Tests.vbproj
    ''' <summary>Test functionality of <see cref="Tools.WindowsT.FormsT.ExtendedForm"/></summary>
    Public Class frmExtendedForm
        ''' <summary>Runs test GUI</summary>
        Public Shared Sub Test()
            Dim ef As New frmExtendedForm
            ef.ShowDialog()
        End Sub

        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub

        Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
            lstEvents.Items.Clear()
        End Sub

        Private Sub frmExtendedForm_ApplicationCommand(ByVal sender As Object, ByVal e As Tools.WindowsT.FormsT.ApplicationCommandEventArgs) Handles Me.ApplicationCommand
            Add("ApplicationCommand {0} device {1}", e.Command, e.Device)
        End Sub
        Private Sub Add(ByVal Text$, ByVal ParamArray obj As Object())
            lstEvents.Items.Add(String.Format(Text, obj))
            lstEvents.SelectedIndex = lstEvents.Items.Count - 1
        End Sub
        Private Sub frmExtendedForm_WindowStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.WindowStateChanged
            Add("WindowStateChanged to {0}", Me.windowstate)
        End Sub
    End Class
End Namespace