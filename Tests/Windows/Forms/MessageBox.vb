'#If Config <= Nightly Then 'Set in project file
Imports Tools.WindowsT, Tools.WindowsT.FormsT
Namespace WindowsT.FormsT
    ''' <summary>Tests for <see cref="MessageBox"/></summary>
    Public Class frmMessageBox
        ''' <summary><see cref="MessageBox"/> being tested</summary>
        Private WithEvents Box As MessageBox

        ''' <summary>Show test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmMessageBox
            frm.ShowDialog()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon

        End Sub

        Private Sub ApplyState()
            cmdCreate.Enabled = Box Is Nothing
            cmdShow.Enabled = Box IsNot Nothing AndAlso Box.State = IndependentT.MessageBox.States.Created
            cmdShowDialog.Enabled = Box IsNot Nothing AndAlso Box.State = IndependentT.MessageBox.States.Created
            cmdShowFloating.Enabled = Box IsNot Nothing AndAlso Box.State = IndependentT.MessageBox.States.Created
            cmdClose.Enabled = Box IsNot Nothing AndAlso Box.State = IndependentT.MessageBox.States.Shown
            cmdDestroy.Enabled = Box IsNot Nothing AndAlso (Box.State = IndependentT.MessageBox.States.Created OrElse Box.State = IndependentT.MessageBox.States.Closed)
        End Sub

        Private Sub cmdCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreate.Click
            txtLog.Clear()
            Log("Creating")
            Box = New MessageBox
            prgGrid.SelectedObject = Box
            ApplyState()
        End Sub

        Private Sub cmdShowDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowDialog.Click
            Log("Calling Show")
            Box.Show()
            ApplyState()
        End Sub

        Private Sub cmdShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShow.Click
            Log("Calling Display")
            Box.Display()
            ApplyState()
        End Sub

        Private Sub cmdShowFloating_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowFloating.Click
            Log("Calling Display(Me)")
            Box.Display(Me)
            ApplyState()
        End Sub

        Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
            Log("Calling close")
            Box.Close()
            ApplyState()
        End Sub

        Private Sub cmdDestroy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDestroy.Click
            Log("Destroying")
            Box.Dispose()
            Box = Nothing
            prgGrid.SelectedObject = Nothing
            ApplyState()
        End Sub
#Region "Events"
        Private Sub Box_Closed(ByVal sender As MessageBox, ByVal e As System.EventArgs) Handles Box.Closed
            Log("Closed")
            ApplyState()
        End Sub

        Private Sub Box_Shown(ByVal sender As MessageBox, ByVal e As System.EventArgs) Handles Box.Shown
            Log("Shown")
            ApplyState()
        End Sub
#End Region
        Private Sub Log(ByVal Text$)
            If txtLog.Text <> "" Then txtLog.Text &= vbCrLf
            txtLog.Text &= Text
            txtLog.Select(txtLog.Text.Length, 0)
            txtLog.ScrollToCaret()
        End Sub
        Private Sub Log(ByVal Text$, ByVal ParamArray Params As Object())
            Log(String.Format(Text, Params))
        End Sub
    End Class
End Namespace
