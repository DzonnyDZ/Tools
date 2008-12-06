Namespace WindowsT.FormsT.VisualStylesT
    Public Class frmSafeButtonRenderer
        ''' <summary>Show test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmSafeButtonRenderer
            frm.ShowDialog()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon
            chkVisualStyle.Checked = cmdTest.UseVisualStyle
        End Sub

        Private Sub chkVisualStyle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVisualStyle.CheckedChanged
            cmdTest.UseVisualStyle = chkVisualStyle.Checked
            chkVisualStyle.Checked = cmdTest.UseVisualStyle

        End Sub

        Private Class CustomButton : Inherits ButtonBase
            Private Renderer As Tools.WindowsT.FormsT.VisualStylesT.VisualStyleSafeObject.SafeButtonRenderer = Tools.WindowsT.FormsT.VisualStylesT.VisualStyleSafeObject.CreateButton(VisualStyles.PushButtonState.Normal)
            Public Property UseVisualStyle() As Boolean
                Get
                    Return Renderer.UseVisualStyle
                End Get
                Set(ByVal value As Boolean)
                    Renderer.UseVisualStyle = value
                    Me.Invalidate()
                End Set
            End Property
            Protected Overrides Sub OnEnabledChanged(ByVal e As System.EventArgs)
                ChangeState()
                MyBase.OnEnabledChanged(e)
            End Sub
            Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
                ChangeState()
                MyBase.OnMouseEnter(e)
            End Sub
            Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
                ChangeState()
                MyBase.OnMouseLeave(e)
            End Sub
            Protected Overrides Sub OnMouseDown(ByVal mevent As System.Windows.Forms.MouseEventArgs)
                ChangeState()
                MyBase.OnMouseDown(mevent)
            End Sub
            Protected Overrides Sub OnMouseUp(ByVal mevent As System.Windows.Forms.MouseEventArgs)
                ChangeState()
                MyBase.OnMouseUp(mevent)
            End Sub

            Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
                If m.Msg = API.Messages.WindowMessages.WM_THEMECHANGE Then
                    Renderer = Tools.WindowsT.FormsT.VisualStylesT.VisualStyleSafeObject.CreateButton(VisualStyles.PushButtonState.Normal)
                    ChangeState()
                    m.Result = 0
                End If
                MyBase.WndProc(m)
                If m.Msg = API.Messages.WindowMessages.WM_THEMECHANGE Then
                    RaiseEvent ThemeChanged(Me, New API.Messages.WindowMessage(m))
                End If
            End Sub

            Public Event ThemeChanged As EventHandler
            
            Protected Overrides Sub OnPaint(ByVal pevent As System.Windows.Forms.PaintEventArgs)
                Renderer.DrawBackground(pevent.Graphics, New Rectangle(0, 0, Me.Width, Me.Height), pevent.ClipRectangle)
                If Me.Focused Then
                    Renderer.DrawFocusRectangle(pevent.Graphics, New Rectangle(0, 0, Me.Width, Me.Height))
                End If
                Dim f As New StringFormat
                f.Alignment = StringAlignment.Center
                f.LineAlignment = StringAlignment.Center
                pevent.Graphics.DrawString(Me.Text, Me.Font, New SolidBrush(Me.ForeColor), Renderer.GetContentRectangle(pevent.Graphics, New Rectangle(0, 0, Me.Width, Me.Height)), f)
            End Sub

            Private Sub ChangeState()
                If Not Me.Enabled Then
                    Renderer.State = VisualStyles.PushButtonState.Disabled
                ElseIf New Rectangle(0, 0, Me.Width, Me.Height).Contains(Me.PointToClient(Control.MousePosition)) AndAlso (Control.MouseButtons And MouseButtons.Left) = MouseButtons.Left Then
                    Renderer.State = VisualStyles.PushButtonState.Pressed
                ElseIf New Rectangle(0, 0, Me.Width, Me.Height).Contains(Me.PointToClient(Control.MousePosition)) Then
                    Renderer.State = VisualStyles.PushButtonState.Hot
                ElseIf Me.IsDefault Then
                    Renderer.State = VisualStyles.PushButtonState.Default
                Else
                    Renderer.State = VisualStyles.PushButtonState.Normal
                End If
            End Sub
        End Class


        Private Sub cmdTest_ThemeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTest.ThemeChanged
            chkVisualStyle.Checked = cmdTest.UseVisualStyle
        End Sub
    End Class
End Namespace
