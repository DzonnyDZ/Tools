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
            InitializeComponent()
            Me.Icon = Tools.ResourcesT.ToolsIcon
            obwControl.Objects.Add(GetType(System.Windows.Forms.Control).Assembly)
            obwControl.Objects.Add(GetType(Tools.WindowsT.FormsT.KeyWordsEditor).Assembly)
            obwControl.Objects.Add(GetType(Tools.WindowsT.FormsT.ExtendedForm).Assembly)
            obwControl.Objects.Add(GetType(Windows.FrameworkElement).Assembly)
        End Sub
        ''' <summary>Enables/disables buttons according to <see cref="iMsg.State"/> of <see cref="Box"/></summary>
        Private Sub ApplyState()
            cmdCreate.Enabled = Box Is Nothing
            cmdShow.Enabled = Box IsNot Nothing AndAlso (Box.State = IndependentT.MessageBox.States.Created Or Box.State = IndependentT.MessageBox.States.Closed)
            cmdShowDialog.Enabled = Box IsNot Nothing AndAlso (Box.State = IndependentT.MessageBox.States.Created Or Box.State = IndependentT.MessageBox.States.Closed)
            cmdShowFloating.Enabled = Box IsNot Nothing AndAlso (Box.State = IndependentT.MessageBox.States.Created Or Box.State = IndependentT.MessageBox.States.Closed)
            cmdClose.Enabled = Box IsNot Nothing AndAlso Box.State = IndependentT.MessageBox.States.Shown
            cmdDestroy.Enabled = Box IsNot Nothing AndAlso (Box.State = IndependentT.MessageBox.States.Created OrElse Box.State = IndependentT.MessageBox.States.Closed)
            splMain.Panel2.Enabled = Box IsNot Nothing
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
            Try
                Box.ShowDialog()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                Log("Error {0}", ex.Message)
            End Try
            ApplyState()
        End Sub

        Private Sub cmdShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShow.Click
            Log("Calling Display")
            Try
                Box.DisplayBox()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                Log("Error {0}", ex.Message)
            End Try
            ApplyState()
        End Sub

        Private Sub cmdShowFloating_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowFloating.Click
            Log("Calling Display(Me)")
            Try
                Box.DisplayBox(Me)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                Log("Error {0}", ex.Message)
            End Try
            ApplyState()
        End Sub

        Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
            Log("Calling close")
            Try
                Box.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                Log("Error {0}", ex.Message)
            End Try
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
            If sender.ClickedButton IsNot Nothing Then _
                Log("{0} Clicked button {1} ""{2}""", vbTab, sender.ClickedButton.Result, sender.ClickedButton.Text)
            If sender.ClosedByTimer Then Log("{0} Closed by timer", vbTab)
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

        Private Sub obwControl_ItemFiltering(ByVal sender As Object, ByVal e As Tools.ComponentModelT.CancelItemEventArgs(Of Object)) Handles obwControl.ItemFiltering
            If TypeOf e.Item Is Type Then
                With DirectCast(e.Item, Type)
                    e.Cancel = Not GetType(Windows.Forms.Control).IsAssignableFrom(.self) AndAlso Not GetType(Windows.UIElement).IsAssignableFrom(.self)
                End With
            End If
        End Sub

        Private Sub cmdSetTop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetTop.Click, cmdSetMid.Click, cmdSetBottom.Click
            If Not TypeOf obwControl.SelectedItem Is Type OrElse (Not GetType(Windows.Forms.Control).IsAssignableFrom(obwControl.SelectedItem) AndAlso Not GetType(Windows.UIElement).IsAssignableFrom(obwControl.SelectedItem)) Then
                MsgBox("Select type derived from Windows.Forms.Control or Windows.FrameworkElement", MsgBoxStyle.Exclamation, "Incorrect type")
                Exit Sub
            End If
            Dim Instance As Object
            Try
                Instance = Activator.CreateInstance(DirectCast(obwControl.SelectedItem, Type))
            Catch ex As Exception
                MsgBox("Error while creating instance of type " & DirectCast(obwControl.SelectedItem, Type).FullName & ":" & vbCrLf & ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                Exit Sub
            End Try
            Try
                If sender Is cmdSetTop Then
                    Box.TopControl = Instance
                    optTop.Checked = True
                    prgControlProperties.SelectedObject = Box.TopControl
                ElseIf sender Is cmdSetBottom Then
                    Box.BottomControl = Instance
                    optBottom.Checked = True
                    prgControlProperties.SelectedObject = Box.BottomControl
                ElseIf sender Is cmdSetMid Then
                    Box.MidControl = Instance
                    optMiddle.Checked = True
                    prgControlProperties.SelectedObject = Box.MidControl
                End If
                tabAdditionalControls.SelectedTab = tapControlProperties
                If TypeOf Instance Is Control Then AddHandler DirectCast(Instance, Control).Disposed, AddressOf Control_Disposed
            Catch ex As Exception
                MsgBox("MessageBox refused the control:" & vbCrLf & ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                Exit Sub
            End Try
        End Sub
        Private Sub Control_Disposed(ByVal sender As Object, ByVal e As EventArgs)
            Log("Control disposed")
        End Sub

        Private Sub optTop_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTop.CheckedChanged
            If optTop.Checked Then prgControlProperties.SelectedObject = Box.TopControl
        End Sub

        Private Sub optMiddle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optMiddle.CheckedChanged
            If optMiddle.Checked Then prgControlProperties.SelectedObject = Box.MidControl
        End Sub

        Private Sub optBottom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optBottom.CheckedChanged
            If optBottom.Checked Then prgControlProperties.SelectedObject = Box.BottomControl
        End Sub
    End Class
End Namespace
