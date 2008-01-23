<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LabelSettings
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
        Me.fraMain = New System.Windows.Forms.GroupBox
        Me.cmdDesigner = New System.Windows.Forms.Button
        Me.tabMain = New System.Windows.Forms.TabControl
        Me.tapSettings = New System.Windows.Forms.TabPage
        Me.flpSettings = New System.Windows.Forms.FlowLayoutPanel
        Me.cmdForeColor = New System.Windows.Forms.Button
        Me.flpBackColor = New System.Windows.Forms.FlowLayoutPanel
        Me.cmdBackColor = New System.Windows.Forms.Button
        Me.lblTransp = New System.Windows.Forms.Label
        Me.nudBackTransp = New System.Windows.Forms.NumericUpDown
        Me.lblTranspI = New System.Windows.Forms.Label
        Me.flpFont = New System.Windows.Forms.FlowLayoutPanel
        Me.lblFont = New System.Windows.Forms.Label
        Me.cmdFont = New System.Windows.Forms.Button
        Me.flpRefresh = New System.Windows.Forms.FlowLayoutPanel
        Me.lblRefresh = New System.Windows.Forms.Label
        Me.optRImage = New System.Windows.Forms.RadioButton
        Me.optR1s = New System.Windows.Forms.RadioButton
        Me.optR1m = New System.Windows.Forms.RadioButton
        Me.tapText = New System.Windows.Forms.TabPage
        Me.txtText = New System.Windows.Forms.TextBox
        Me.chkEnabled = New System.Windows.Forms.CheckBox
        Me.fdlFont = New System.Windows.Forms.FontDialog
        Me.cdlColor = New System.Windows.Forms.ColorDialog
        Me.fraMain.SuspendLayout()
        Me.tabMain.SuspendLayout()
        Me.tapSettings.SuspendLayout()
        Me.flpSettings.SuspendLayout()
        Me.flpBackColor.SuspendLayout()
        CType(Me.nudBackTransp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.flpFont.SuspendLayout()
        Me.flpRefresh.SuspendLayout()
        Me.tapText.SuspendLayout()
        Me.SuspendLayout()
        '
        'fraMain
        '
        Me.fraMain.Controls.Add(Me.cmdDesigner)
        Me.fraMain.Controls.Add(Me.tabMain)
        Me.fraMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fraMain.Location = New System.Drawing.Point(0, 0)
        Me.fraMain.Margin = New System.Windows.Forms.Padding(0)
        Me.fraMain.Name = "fraMain"
        Me.fraMain.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.fraMain.Size = New System.Drawing.Size(243, 140)
        Me.fraMain.TabIndex = 0
        Me.fraMain.TabStop = False
        '
        'cmdDesigner
        '
        Me.cmdDesigner.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDesigner.AutoSize = True
        Me.cmdDesigner.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdDesigner.Location = New System.Drawing.Point(160, 12)
        Me.cmdDesigner.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdDesigner.Name = "cmdDesigner"
        Me.cmdDesigner.Size = New System.Drawing.Size(81, 23)
        Me.cmdDesigner.TabIndex = 5
        Me.cmdDesigner.Text = "Text designer"
        Me.cmdDesigner.UseVisualStyleBackColor = True
        Me.cmdDesigner.Visible = False
        '
        'tabMain
        '
        Me.tabMain.Controls.Add(Me.tapSettings)
        Me.tabMain.Controls.Add(Me.tapText)
        Me.tabMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabMain.Location = New System.Drawing.Point(0, 16)
        Me.tabMain.Margin = New System.Windows.Forms.Padding(0)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedIndex = 0
        Me.tabMain.Size = New System.Drawing.Size(243, 124)
        Me.tabMain.TabIndex = 7
        '
        'tapSettings
        '
        Me.tapSettings.Controls.Add(Me.flpSettings)
        Me.tapSettings.Location = New System.Drawing.Point(4, 22)
        Me.tapSettings.Margin = New System.Windows.Forms.Padding(0)
        Me.tapSettings.Name = "tapSettings"
        Me.tapSettings.Size = New System.Drawing.Size(235, 98)
        Me.tapSettings.TabIndex = 0
        Me.tapSettings.Text = "Settings"
        Me.tapSettings.UseVisualStyleBackColor = True
        '
        'flpSettings
        '
        Me.flpSettings.AutoSize = True
        Me.flpSettings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpSettings.Controls.Add(Me.cmdForeColor)
        Me.flpSettings.Controls.Add(Me.flpBackColor)
        Me.flpSettings.Controls.Add(Me.flpFont)
        Me.flpSettings.Controls.Add(Me.flpRefresh)
        Me.flpSettings.Dock = System.Windows.Forms.DockStyle.Top
        Me.flpSettings.Location = New System.Drawing.Point(0, 0)
        Me.flpSettings.Margin = New System.Windows.Forms.Padding(0)
        Me.flpSettings.Name = "flpSettings"
        Me.flpSettings.Size = New System.Drawing.Size(235, 99)
        Me.flpSettings.TabIndex = 0
        '
        'cmdForeColor
        '
        Me.cmdForeColor.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdForeColor.AutoSize = True
        Me.cmdForeColor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdForeColor.BackColor = System.Drawing.Color.Lime
        Me.cmdForeColor.FlatAppearance.BorderSize = 0
        Me.cmdForeColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdForeColor.ForeColor = System.Drawing.Color.Fuchsia
        Me.cmdForeColor.Location = New System.Drawing.Point(0, 0)
        Me.cmdForeColor.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdForeColor.Name = "cmdForeColor"
        Me.cmdForeColor.Size = New System.Drawing.Size(97, 23)
        Me.cmdForeColor.TabIndex = 0
        Me.cmdForeColor.Text = "Foreground color"
        Me.cmdForeColor.UseVisualStyleBackColor = False
        '
        'flpBackColor
        '
        Me.flpBackColor.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.flpBackColor.AutoSize = True
        Me.flpBackColor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpBackColor.Controls.Add(Me.cmdBackColor)
        Me.flpBackColor.Controls.Add(Me.lblTransp)
        Me.flpBackColor.Controls.Add(Me.nudBackTransp)
        Me.flpBackColor.Controls.Add(Me.lblTranspI)
        Me.flpBackColor.Location = New System.Drawing.Point(0, 23)
        Me.flpBackColor.Margin = New System.Windows.Forms.Padding(0)
        Me.flpBackColor.Name = "flpBackColor"
        Me.flpBackColor.Size = New System.Drawing.Size(235, 36)
        Me.flpBackColor.TabIndex = 3
        '
        'cmdBackColor
        '
        Me.cmdBackColor.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdBackColor.AutoSize = True
        Me.cmdBackColor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdBackColor.BackColor = System.Drawing.Color.White
        Me.cmdBackColor.FlatAppearance.BorderSize = 0
        Me.cmdBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdBackColor.ForeColor = System.Drawing.Color.Black
        Me.cmdBackColor.Location = New System.Drawing.Point(0, 0)
        Me.cmdBackColor.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdBackColor.Name = "cmdBackColor"
        Me.cmdBackColor.Size = New System.Drawing.Size(101, 23)
        Me.cmdBackColor.TabIndex = 0
        Me.cmdBackColor.Text = "Background color"
        Me.cmdBackColor.UseVisualStyleBackColor = False
        '
        'lblTransp
        '
        Me.lblTransp.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTransp.AutoSize = True
        Me.lblTransp.Location = New System.Drawing.Point(104, 5)
        Me.lblTransp.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblTransp.Name = "lblTransp"
        Me.lblTransp.Size = New System.Drawing.Size(72, 13)
        Me.lblTransp.TabIndex = 1
        Me.lblTransp.Text = "Transparency"
        '
        'nudBackTransp
        '
        Me.nudBackTransp.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.nudBackTransp.Location = New System.Drawing.Point(176, 1)
        Me.nudBackTransp.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.nudBackTransp.Name = "nudBackTransp"
        Me.nudBackTransp.Size = New System.Drawing.Size(56, 20)
        Me.nudBackTransp.TabIndex = 2
        '
        'lblTranspI
        '
        Me.lblTranspI.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTranspI.AutoSize = True
        Me.lblTranspI.Location = New System.Drawing.Point(0, 23)
        Me.lblTranspI.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTranspI.Name = "lblTranspI"
        Me.lblTranspI.Size = New System.Drawing.Size(100, 13)
        Me.lblTranspI.TabIndex = 3
        Me.lblTranspI.Text = "0 is fully transparent"
        '
        'flpFont
        '
        Me.flpFont.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.flpFont.AutoSize = True
        Me.flpFont.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpFont.Controls.Add(Me.lblFont)
        Me.flpFont.Controls.Add(Me.cmdFont)
        Me.flpFont.Location = New System.Drawing.Point(0, 59)
        Me.flpFont.Margin = New System.Windows.Forms.Padding(0)
        Me.flpFont.Name = "flpFont"
        Me.flpFont.Size = New System.Drawing.Size(107, 23)
        Me.flpFont.TabIndex = 2
        '
        'lblFont
        '
        Me.lblFont.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFont.AutoSize = True
        Me.lblFont.Location = New System.Drawing.Point(3, 5)
        Me.lblFont.Name = "lblFont"
        Me.lblFont.Size = New System.Drawing.Size(28, 13)
        Me.lblFont.TabIndex = 0
        Me.lblFont.Text = "Font"
        '
        'cmdFont
        '
        Me.cmdFont.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdFont.AutoSize = True
        Me.cmdFont.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdFont.Location = New System.Drawing.Point(34, 0)
        Me.cmdFont.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdFont.Name = "cmdFont"
        Me.cmdFont.Size = New System.Drawing.Size(73, 23)
        Me.cmdFont.TabIndex = 1
        Me.cmdFont.Text = "Arial 12pt ..."
        Me.cmdFont.UseVisualStyleBackColor = True
        '
        'flpRefresh
        '
        Me.flpRefresh.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.flpRefresh.AutoSize = True
        Me.flpRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpRefresh.Controls.Add(Me.lblRefresh)
        Me.flpRefresh.Controls.Add(Me.optRImage)
        Me.flpRefresh.Controls.Add(Me.optR1s)
        Me.flpRefresh.Controls.Add(Me.optR1m)
        Me.flpRefresh.Location = New System.Drawing.Point(0, 82)
        Me.flpRefresh.Margin = New System.Windows.Forms.Padding(0)
        Me.flpRefresh.Name = "flpRefresh"
        Me.flpRefresh.Size = New System.Drawing.Size(172, 17)
        Me.flpRefresh.TabIndex = 4
        '
        'lblRefresh
        '
        Me.lblRefresh.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblRefresh.AutoSize = True
        Me.lblRefresh.Location = New System.Drawing.Point(0, 2)
        Me.lblRefresh.Margin = New System.Windows.Forms.Padding(0)
        Me.lblRefresh.Name = "lblRefresh"
        Me.lblRefresh.Size = New System.Drawing.Size(44, 13)
        Me.lblRefresh.TabIndex = 0
        Me.lblRefresh.Text = "Refresh"
        '
        'optRImage
        '
        Me.optRImage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.optRImage.AutoSize = True
        Me.optRImage.Checked = True
        Me.optRImage.Location = New System.Drawing.Point(44, 0)
        Me.optRImage.Margin = New System.Windows.Forms.Padding(0)
        Me.optRImage.Name = "optRImage"
        Me.optRImage.Size = New System.Drawing.Size(53, 17)
        Me.optRImage.TabIndex = 1
        Me.optRImage.TabStop = True
        Me.optRImage.Text = "image"
        Me.optRImage.UseVisualStyleBackColor = True
        '
        'optR1s
        '
        Me.optR1s.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.optR1s.AutoSize = True
        Me.optR1s.Location = New System.Drawing.Point(97, 0)
        Me.optR1s.Margin = New System.Windows.Forms.Padding(0)
        Me.optR1s.Name = "optR1s"
        Me.optR1s.Size = New System.Drawing.Size(36, 17)
        Me.optR1s.TabIndex = 2
        Me.optR1s.Text = "1s"
        Me.optR1s.UseVisualStyleBackColor = True
        '
        'optR1m
        '
        Me.optR1m.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.optR1m.AutoSize = True
        Me.optR1m.Location = New System.Drawing.Point(133, 0)
        Me.optR1m.Margin = New System.Windows.Forms.Padding(0)
        Me.optR1m.Name = "optR1m"
        Me.optR1m.Size = New System.Drawing.Size(39, 17)
        Me.optR1m.TabIndex = 3
        Me.optR1m.Text = "1m"
        Me.optR1m.UseVisualStyleBackColor = True
        '
        'tapText
        '
        Me.tapText.Controls.Add(Me.txtText)
        Me.tapText.Location = New System.Drawing.Point(4, 22)
        Me.tapText.Margin = New System.Windows.Forms.Padding(0)
        Me.tapText.Name = "tapText"
        Me.tapText.Size = New System.Drawing.Size(235, 98)
        Me.tapText.TabIndex = 1
        Me.tapText.Text = "Text"
        Me.tapText.UseVisualStyleBackColor = True
        '
        'txtText
        '
        Me.txtText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtText.Location = New System.Drawing.Point(0, 0)
        Me.txtText.Margin = New System.Windows.Forms.Padding(0)
        Me.txtText.Multiline = True
        Me.txtText.Name = "txtText"
        Me.txtText.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtText.Size = New System.Drawing.Size(235, 98)
        Me.txtText.TabIndex = 6
        Me.txtText.WordWrap = False
        '
        'chkEnabled
        '
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Checked = True
        Me.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEnabled.Location = New System.Drawing.Point(0, -1)
        Me.chkEnabled.Margin = New System.Windows.Forms.Padding(0)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(103, 17)
        Me.chkEnabled.TabIndex = 0
        Me.chkEnabled.Text = "Enable this label"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'fdlFont
        '
        Me.fdlFont.AllowScriptChange = False
        Me.fdlFont.AllowVerticalFonts = False
        Me.fdlFont.FontMustExist = True
        '
        'cdlColor
        '
        Me.cdlColor.AnyColor = True
        '
        'LabelSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.chkEnabled)
        Me.Controls.Add(Me.fraMain)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.MinimumSize = New System.Drawing.Size(243, 140)
        Me.Name = "LabelSettings"
        Me.Size = New System.Drawing.Size(243, 140)
        Me.fraMain.ResumeLayout(False)
        Me.fraMain.PerformLayout()
        Me.tabMain.ResumeLayout(False)
        Me.tapSettings.ResumeLayout(False)
        Me.tapSettings.PerformLayout()
        Me.flpSettings.ResumeLayout(False)
        Me.flpSettings.PerformLayout()
        Me.flpBackColor.ResumeLayout(False)
        Me.flpBackColor.PerformLayout()
        CType(Me.nudBackTransp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.flpFont.ResumeLayout(False)
        Me.flpFont.PerformLayout()
        Me.flpRefresh.ResumeLayout(False)
        Me.flpRefresh.PerformLayout()
        Me.tapText.ResumeLayout(False)
        Me.tapText.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents fraMain As System.Windows.Forms.GroupBox
    Friend WithEvents flpSettings As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents cmdForeColor As System.Windows.Forms.Button
    Friend WithEvents flpBackColor As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents cmdBackColor As System.Windows.Forms.Button
    Friend WithEvents lblTransp As System.Windows.Forms.Label
    Friend WithEvents nudBackTransp As System.Windows.Forms.NumericUpDown
    Friend WithEvents flpFont As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblFont As System.Windows.Forms.Label
    Friend WithEvents cmdFont As System.Windows.Forms.Button
    Friend WithEvents flpRefresh As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblRefresh As System.Windows.Forms.Label
    Friend WithEvents optRImage As System.Windows.Forms.RadioButton
    Friend WithEvents optR1s As System.Windows.Forms.RadioButton
    Friend WithEvents optR1m As System.Windows.Forms.RadioButton
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents lblTranspI As System.Windows.Forms.Label
    Friend WithEvents txtText As System.Windows.Forms.TextBox
    Friend WithEvents cmdDesigner As System.Windows.Forms.Button
    Friend WithEvents fdlFont As System.Windows.Forms.FontDialog
    Friend WithEvents cdlColor As System.Windows.Forms.ColorDialog
    Friend WithEvents tabMain As System.Windows.Forms.TabControl
    Friend WithEvents tapSettings As System.Windows.Forms.TabPage
    Friend WithEvents tapText As System.Windows.Forms.TabPage

End Class
