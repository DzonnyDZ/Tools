'#If Config <= Nightly Then 'Set in project file
Imports Tools.WindowsT
Imports iMsg = Tools.WindowsT.IndependentT.MessageBox
Imports fMsg = Tools.WindowsT.FormsT.MessageBox
Imports wMsg = Tools.WindowsT.WPF.DialogsT.MessageBox
Namespace WindowsT.FormsT
    ''' <summary>Tests for <see cref="MessageBox"/></summary>
    Public Class frmMessageBox
        ''' <summary><see cref="MessageBox"/> being tested</summary>
        Private WithEvents Box As iMsg

        ''' <summary>Show test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmMessageBox
            frm.ShowDialog()
        End Sub
        ''' <summary>Show test form with prechecked WPF option</summary>
        Public Shared Sub TestWPF()
            Dim frm As New frmMessageBox
            frm.optWPF.Checked = True
            frm.ShowDialog()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon

        End Sub
        ''' <summary>Enables/disables buttons according to <see cref="iMsg.State"/> of <see cref="Box"/></summary>
        Private Sub ApplyState()
            cmdCreate.Enabled = Box Is Nothing
            cmdShow.Enabled = Box IsNot Nothing AndAlso (Box.State = IndependentT.MessageBox.States.Created Or Box.State = IndependentT.MessageBox.States.Closed)
            cmdShowDialog.Enabled = Box IsNot Nothing AndAlso (Box.State = IndependentT.MessageBox.States.Created Or Box.State = IndependentT.MessageBox.States.Closed)
            cmdShowFloating.Enabled = Box IsNot Nothing AndAlso (Box.State = IndependentT.MessageBox.States.Created Or Box.State = IndependentT.MessageBox.States.Closed)
            cmdClose.Enabled = Box IsNot Nothing AndAlso Box.State = IndependentT.MessageBox.States.Shown
            cmdDestroy.Enabled = Box IsNot Nothing AndAlso (Box.State = IndependentT.MessageBox.States.Created OrElse Box.State = IndependentT.MessageBox.States.Closed)
        End Sub

        Private Sub cmdCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreate.Click
            txtLog.Clear()
            If optWinForms.Checked Then
                Log("Creating WF")
                Box = New fMsg
            Else
                Log("Creating WPF")
                Box = New wMsg
            End If
            prgGrid.SelectedObject = Box
            ApplyState()
        End Sub

        Private Sub cmdShowDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowDialog.Click
            Log("Calling Show")
            Box.ShowDialog()
            ApplyState()
        End Sub

        Private Sub cmdShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShow.Click
            Log("Calling Display")
            Box.DisplayBox()
            ApplyState()
        End Sub

        Private Sub cmdShowFloating_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowFloating.Click
            Log("Calling Display(Me)")
            Box.DisplayBox(Me)
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
        Private Sub Box_Closed(ByVal sender As iMsg, ByVal e As System.EventArgs) Handles Box.Closed
            Log("Closed {0}", sender.DialogResult)
            ApplyState()
        End Sub

        Private Sub Box_Recycled(ByVal sender As iMsg, ByVal e As System.EventArgs) Handles Box.Recycled
            Log("Reycled")
            ApplyState()
        End Sub
        ''' <summary>Floating visual tree and property grid for obserwong message box</summary>
        Private WithEvents FloatingTree As ContentTree
        Private Sub Box_Shown(ByVal sender As iMsg, ByVal e As System.EventArgs) Handles Box.Shown
            Log("Shown")
            ApplyState()
            If TypeOf sender Is fMsg Then
                If FloatingTree IsNot Nothing Then
                    Dim NewTree As New ContentTree
                    NewTree.DesktopBounds = FloatingTree.DesktopBounds
                    NewTree.StartPosition = FormStartPosition.Manual
                    FloatingTree = NewTree
                Else : FloatingTree = New ContentTree
                End If
                With DirectCast(sender, fMsg)
                    FloatingTree.Root = .Form
                    FloatingTree.Show(.Form)
                End With
            End If
        End Sub
#End Region
        ''' <summary>Logs messagebox action</summary>
        ''' <param name="Text">Text to be logged</param>
        Private Sub Log(ByVal Text$)
            If txtLog.Text <> "" Then txtLog.Text &= vbCrLf
            txtLog.Text &= Text
            txtLog.Select(txtLog.Text.Length, 0)
            txtLog.ScrollToCaret()
        End Sub
        ''' <summary>Logs message box action with parameters</summary>
        ''' <param name="Text">Formatting text</param>
        ''' <param name="Params">Parameters</param>
        ''' <seealso cref="String.Format"/>
        Private Sub Log(ByVal Text$, ByVal ParamArray Params As Object())
            Log(String.Format(Text, Params))
        End Sub

        Private Sub cmdCreate_EnabledChanged(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdCreate.EnabledChanged
            optWinForms.Enabled = sender.Enabled
            optWPF.Enabled = sender.Enabled
        End Sub
    End Class
End Namespace
