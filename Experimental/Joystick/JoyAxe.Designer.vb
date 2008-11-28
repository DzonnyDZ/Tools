Namespace DevicesT.JoystickT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class JoyAxe
        Inherits System.Windows.Forms.UserControl

        'UserControl overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.flpAxe = New System.Windows.Forms.FlowLayoutPanel
            Me.lblAxe = New System.Windows.Forms.Label
            Me.lblMin = New System.Windows.Forms.Label
            Me.pgbProgress = New Tools.WindowsT.FormsT.ProgressBarWithText
            Me.lblMax = New System.Windows.Forms.Label
            Me.flpAxe.SuspendLayout()
            Me.SuspendLayout()
            '
            'flpAxe
            '
            Me.flpAxe.AutoSize = True
            Me.flpAxe.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpAxe.Controls.Add(Me.lblAxe)
            Me.flpAxe.Controls.Add(Me.lblMin)
            Me.flpAxe.Controls.Add(Me.pgbProgress)
            Me.flpAxe.Controls.Add(Me.lblMax)
            Me.flpAxe.Dock = System.Windows.Forms.DockStyle.Fill
            Me.flpAxe.Location = New System.Drawing.Point(0, 0)
            Me.flpAxe.Margin = New System.Windows.Forms.Padding(0)
            Me.flpAxe.Name = "flpAxe"
            Me.flpAxe.Size = New System.Drawing.Size(176, 19)
            Me.flpAxe.TabIndex = 1
            '
            'lblAxe
            '
            Me.lblAxe.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.lblAxe.AutoSize = True
            Me.lblAxe.Location = New System.Drawing.Point(3, 3)
            Me.lblAxe.Name = "lblAxe"
            Me.lblAxe.Size = New System.Drawing.Size(14, 13)
            Me.lblAxe.TabIndex = 0
            Me.lblAxe.Text = "X"
            '
            'lblMin
            '
            Me.lblMin.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.lblMin.AutoSize = True
            Me.lblMin.Location = New System.Drawing.Point(23, 3)
            Me.lblMin.Name = "lblMin"
            Me.lblMin.Size = New System.Drawing.Size(13, 13)
            Me.lblMin.TabIndex = 1
            Me.lblMin.Text = "0"
            '
            'pgbProgress
            '
            Me.pgbProgress.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.pgbProgress.AutoTextFormat = Nothing
            Me.pgbProgress.Location = New System.Drawing.Point(42, 3)
            Me.pgbProgress.MarqueeAnimationSpeed = 1000
            Me.pgbProgress.Name = "pgbProgress"
            Me.pgbProgress.Size = New System.Drawing.Size(100, 13)
            Me.pgbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
            Me.pgbProgress.TabIndex = 2
            '
            'lblMax
            '
            Me.lblMax.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.lblMax.AutoSize = True
            Me.lblMax.Location = New System.Drawing.Point(148, 3)
            Me.lblMax.Name = "lblMax"
            Me.lblMax.Size = New System.Drawing.Size(25, 13)
            Me.lblMax.TabIndex = 3
            Me.lblMax.Text = "100"
            '
            'JoyAxe
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.AutoSize = True
            Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.Controls.Add(Me.flpAxe)
            Me.Name = "JoyAxe"
            Me.Size = New System.Drawing.Size(176, 19)
            Me.flpAxe.ResumeLayout(False)
            Me.flpAxe.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents flpAxe As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents lblAxe As System.Windows.Forms.Label
        Friend WithEvents lblMin As System.Windows.Forms.Label
        Friend WithEvents pgbProgress As Tools.WindowsT.FormsT.ProgressBarWithText
        Friend WithEvents lblMax As System.Windows.Forms.Label

    End Class
End Namespace