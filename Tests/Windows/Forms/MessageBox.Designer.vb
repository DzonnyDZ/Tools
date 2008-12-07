
Namespace WindowsT.FormsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmMessageBox
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
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
            Me.flpCommands = New System.Windows.Forms.FlowLayoutPanel
            Me.cmdCreate = New System.Windows.Forms.Button
            Me.cmdShowDialog = New System.Windows.Forms.Button
            Me.cmdShow = New System.Windows.Forms.Button
            Me.cmdShowFloating = New System.Windows.Forms.Button
            Me.cmdClose = New System.Windows.Forms.Button
            Me.cmdDestroy = New System.Windows.Forms.Button
            Me.prgGrid = New System.Windows.Forms.PropertyGrid
            Me.txtLog = New System.Windows.Forms.TextBox
            Me.optWinForms = New System.Windows.Forms.RadioButton
            Me.optWPF = New System.Windows.Forms.RadioButton
            Me.flpCommands.SuspendLayout()
            Me.SuspendLayout()
            '
            'flpCommands
            '
            Me.flpCommands.AutoSize = True
            Me.flpCommands.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpCommands.Controls.Add(Me.cmdCreate)
            Me.flpCommands.Controls.Add(Me.optWinForms)
            Me.flpCommands.Controls.Add(Me.optWPF)
            Me.flpCommands.Controls.Add(Me.cmdShowDialog)
            Me.flpCommands.Controls.Add(Me.cmdShow)
            Me.flpCommands.Controls.Add(Me.cmdShowFloating)
            Me.flpCommands.Controls.Add(Me.cmdClose)
            Me.flpCommands.Controls.Add(Me.cmdDestroy)
            Me.flpCommands.Dock = System.Windows.Forms.DockStyle.Top
            Me.flpCommands.Location = New System.Drawing.Point(0, 0)
            Me.flpCommands.Name = "flpCommands"
            Me.flpCommands.Size = New System.Drawing.Size(294, 58)
            Me.flpCommands.TabIndex = 0
            '
            'cmdCreate
            '
            Me.cmdCreate.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdCreate.AutoSize = True
            Me.cmdCreate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdCreate.Location = New System.Drawing.Point(3, 3)
            Me.cmdCreate.Name = "cmdCreate"
            Me.cmdCreate.Size = New System.Drawing.Size(91, 23)
            Me.cmdCreate.TabIndex = 0
            Me.cmdCreate.Text = "Create instance"
            Me.cmdCreate.UseVisualStyleBackColor = True
            '
            'cmdShowDialog
            '
            Me.cmdShowDialog.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdShowDialog.AutoSize = True
            Me.cmdShowDialog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdShowDialog.Enabled = False
            Me.cmdShowDialog.Location = New System.Drawing.Point(203, 3)
            Me.cmdShowDialog.Name = "cmdShowDialog"
            Me.cmdShowDialog.Size = New System.Drawing.Size(75, 23)
            Me.cmdShowDialog.TabIndex = 1
            Me.cmdShowDialog.Text = "Show dialog"
            Me.cmdShowDialog.UseVisualStyleBackColor = True
            '
            'cmdShow
            '
            Me.cmdShow.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdShow.AutoSize = True
            Me.cmdShow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdShow.Enabled = False
            Me.cmdShow.Location = New System.Drawing.Point(3, 32)
            Me.cmdShow.Name = "cmdShow"
            Me.cmdShow.Size = New System.Drawing.Size(44, 23)
            Me.cmdShow.TabIndex = 2
            Me.cmdShow.Text = "Show"
            Me.cmdShow.UseVisualStyleBackColor = True
            '
            'cmdShowFloating
            '
            Me.cmdShowFloating.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdShowFloating.AutoSize = True
            Me.cmdShowFloating.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdShowFloating.Enabled = False
            Me.cmdShowFloating.Location = New System.Drawing.Point(53, 32)
            Me.cmdShowFloating.Name = "cmdShowFloating"
            Me.cmdShowFloating.Size = New System.Drawing.Size(81, 23)
            Me.cmdShowFloating.TabIndex = 3
            Me.cmdShowFloating.Text = "Show floating"
            Me.cmdShowFloating.UseVisualStyleBackColor = True
            '
            'cmdClose
            '
            Me.cmdClose.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdClose.AutoSize = True
            Me.cmdClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdClose.Enabled = False
            Me.cmdClose.Location = New System.Drawing.Point(140, 32)
            Me.cmdClose.Name = "cmdClose"
            Me.cmdClose.Size = New System.Drawing.Size(43, 23)
            Me.cmdClose.TabIndex = 4
            Me.cmdClose.Text = "Close"
            Me.cmdClose.UseVisualStyleBackColor = True
            '
            'cmdDestroy
            '
            Me.cmdDestroy.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdDestroy.AutoSize = True
            Me.cmdDestroy.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdDestroy.Enabled = False
            Me.cmdDestroy.Location = New System.Drawing.Point(189, 32)
            Me.cmdDestroy.Name = "cmdDestroy"
            Me.cmdDestroy.Size = New System.Drawing.Size(53, 23)
            Me.cmdDestroy.TabIndex = 5
            Me.cmdDestroy.Text = "Destroy"
            Me.cmdDestroy.UseVisualStyleBackColor = True
            '
            'prgGrid
            '
            Me.prgGrid.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgGrid.Location = New System.Drawing.Point(0, 58)
            Me.prgGrid.Name = "prgGrid"
            Me.prgGrid.Size = New System.Drawing.Size(294, 301)
            Me.prgGrid.TabIndex = 1
            '
            'txtLog
            '
            Me.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.txtLog.Location = New System.Drawing.Point(0, 359)
            Me.txtLog.Multiline = True
            Me.txtLog.Name = "txtLog"
            Me.txtLog.ReadOnly = True
            Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtLog.Size = New System.Drawing.Size(294, 139)
            Me.txtLog.TabIndex = 6
            Me.txtLog.WordWrap = False
            '
            'optWinForms
            '
            Me.optWinForms.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.optWinForms.AutoSize = True
            Me.optWinForms.Checked = True
            Me.optWinForms.Location = New System.Drawing.Point(100, 6)
            Me.optWinForms.Name = "optWinForms"
            Me.optWinForms.Size = New System.Drawing.Size(42, 17)
            Me.optWinForms.TabIndex = 6
            Me.optWinForms.TabStop = True
            Me.optWinForms.Text = "WF"
            Me.optWinForms.UseVisualStyleBackColor = True
            '
            'optWPF
            '
            Me.optWPF.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.optWPF.AutoSize = True
            Me.optWPF.Location = New System.Drawing.Point(148, 6)
            Me.optWPF.Name = "optWPF"
            Me.optWPF.Size = New System.Drawing.Size(49, 17)
            Me.optWPF.TabIndex = 7
            Me.optWPF.Text = "WPF"
            Me.optWPF.UseVisualStyleBackColor = True
            '
            'frmMessageBox
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(294, 498)
            Me.Controls.Add(Me.prgGrid)
            Me.Controls.Add(Me.flpCommands)
            Me.Controls.Add(Me.txtLog)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmMessageBox"
            Me.Text = "Testing Tool.WindowsT.FormsT.MessageBox"
            Me.flpCommands.ResumeLayout(False)
            Me.flpCommands.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents flpCommands As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents cmdCreate As System.Windows.Forms.Button
        Friend WithEvents cmdShowDialog As System.Windows.Forms.Button
        Friend WithEvents cmdShow As System.Windows.Forms.Button
        Friend WithEvents cmdShowFloating As System.Windows.Forms.Button
        Friend WithEvents cmdClose As System.Windows.Forms.Button
        Friend WithEvents cmdDestroy As System.Windows.Forms.Button
        Friend WithEvents prgGrid As System.Windows.Forms.PropertyGrid
        Friend WithEvents txtLog As System.Windows.Forms.TextBox
        Friend WithEvents optWinForms As System.Windows.Forms.RadioButton
        Friend WithEvents optWPF As System.Windows.Forms.RadioButton
    End Class

End Namespace
