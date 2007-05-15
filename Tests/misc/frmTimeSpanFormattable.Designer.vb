'#If Config <= RC Then Stage conditional compilation of this file is set in Tests.vbproj
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTimeSpanFormattable
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
        Me.flpFull = New System.Windows.Forms.FlowLayoutPanel
        Me.lblDays = New System.Windows.Forms.Label
        Me.txtDays = New System.Windows.Forms.TextBox
        Me.lblHours = New System.Windows.Forms.Label
        Me.txtHours = New System.Windows.Forms.TextBox
        Me.lblMinutes = New System.Windows.Forms.Label
        Me.txtMinutes = New System.Windows.Forms.TextBox
        Me.lblSeconds = New System.Windows.Forms.Label
        Me.txtSeconds = New System.Windows.Forms.TextBox
        Me.lblMillseconds = New System.Windows.Forms.Label
        Me.txtMilliseconds = New System.Windows.Forms.TextBox
        Me.lblFormat = New System.Windows.Forms.Label
        Me.txtFormat = New System.Windows.Forms.TextBox
        Me.cmdFormat = New System.Windows.Forms.Button
        Me.lblRes = New System.Windows.Forms.Label
        Me.flpTicks = New System.Windows.Forms.FlowLayoutPanel
        Me.lblTicks = New System.Windows.Forms.Label
        Me.txtTicks = New System.Windows.Forms.TextBox
        Me.lblTicksFormat = New System.Windows.Forms.Label
        Me.txtTicksFormat = New System.Windows.Forms.TextBox
        Me.cmdTicks = New System.Windows.Forms.Button
        Me.lblTicksRes = New System.Windows.Forms.Label
        Me.flpFull.SuspendLayout()
        Me.flpTicks.SuspendLayout()
        Me.SuspendLayout()
        '
        'flpFull
        '
        Me.flpFull.AutoSize = True
        Me.flpFull.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpFull.Controls.Add(Me.lblDays)
        Me.flpFull.Controls.Add(Me.txtDays)
        Me.flpFull.Controls.Add(Me.lblHours)
        Me.flpFull.Controls.Add(Me.txtHours)
        Me.flpFull.Controls.Add(Me.lblMinutes)
        Me.flpFull.Controls.Add(Me.txtMinutes)
        Me.flpFull.Controls.Add(Me.lblSeconds)
        Me.flpFull.Controls.Add(Me.txtSeconds)
        Me.flpFull.Controls.Add(Me.lblMillseconds)
        Me.flpFull.Controls.Add(Me.txtMilliseconds)
        Me.flpFull.Controls.Add(Me.lblFormat)
        Me.flpFull.Controls.Add(Me.txtFormat)
        Me.flpFull.Controls.Add(Me.cmdFormat)
        Me.flpFull.Controls.Add(Me.lblRes)
        Me.flpFull.Dock = System.Windows.Forms.DockStyle.Top
        Me.flpFull.Location = New System.Drawing.Point(0, 0)
        Me.flpFull.Name = "flpFull"
        Me.flpFull.Size = New System.Drawing.Size(564, 55)
        Me.flpFull.TabIndex = 0
        '
        'lblDays
        '
        Me.lblDays.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblDays.AutoSize = True
        Me.lblDays.Location = New System.Drawing.Point(3, 6)
        Me.lblDays.Name = "lblDays"
        Me.lblDays.Size = New System.Drawing.Size(31, 13)
        Me.lblDays.TabIndex = 0
        Me.lblDays.Text = "Days"
        '
        'txtDays
        '
        Me.txtDays.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtDays.Location = New System.Drawing.Point(40, 3)
        Me.txtDays.Name = "txtDays"
        Me.txtDays.Size = New System.Drawing.Size(25, 20)
        Me.txtDays.TabIndex = 1
        Me.txtDays.Text = "0"
        '
        'lblHours
        '
        Me.lblHours.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblHours.AutoSize = True
        Me.lblHours.Location = New System.Drawing.Point(71, 6)
        Me.lblHours.Name = "lblHours"
        Me.lblHours.Size = New System.Drawing.Size(35, 13)
        Me.lblHours.TabIndex = 2
        Me.lblHours.Text = "Hours"
        '
        'txtHours
        '
        Me.txtHours.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtHours.Location = New System.Drawing.Point(112, 3)
        Me.txtHours.Name = "txtHours"
        Me.txtHours.Size = New System.Drawing.Size(25, 20)
        Me.txtHours.TabIndex = 3
        Me.txtHours.Text = "0"
        '
        'lblMinutes
        '
        Me.lblMinutes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMinutes.AutoSize = True
        Me.lblMinutes.Location = New System.Drawing.Point(143, 6)
        Me.lblMinutes.Name = "lblMinutes"
        Me.lblMinutes.Size = New System.Drawing.Size(44, 13)
        Me.lblMinutes.TabIndex = 4
        Me.lblMinutes.Text = "Minutes"
        '
        'txtMinutes
        '
        Me.txtMinutes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMinutes.Location = New System.Drawing.Point(193, 3)
        Me.txtMinutes.Name = "txtMinutes"
        Me.txtMinutes.Size = New System.Drawing.Size(25, 20)
        Me.txtMinutes.TabIndex = 5
        Me.txtMinutes.Text = "0"
        '
        'lblSeconds
        '
        Me.lblSeconds.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSeconds.AutoSize = True
        Me.lblSeconds.Location = New System.Drawing.Point(224, 6)
        Me.lblSeconds.Name = "lblSeconds"
        Me.lblSeconds.Size = New System.Drawing.Size(49, 13)
        Me.lblSeconds.TabIndex = 6
        Me.lblSeconds.Text = "Seconds"
        '
        'txtSeconds
        '
        Me.txtSeconds.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtSeconds.Location = New System.Drawing.Point(279, 3)
        Me.txtSeconds.Name = "txtSeconds"
        Me.txtSeconds.Size = New System.Drawing.Size(25, 20)
        Me.txtSeconds.TabIndex = 7
        Me.txtSeconds.Text = "0"
        '
        'lblMillseconds
        '
        Me.lblMillseconds.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMillseconds.AutoSize = True
        Me.lblMillseconds.Location = New System.Drawing.Point(310, 6)
        Me.lblMillseconds.Name = "lblMillseconds"
        Me.lblMillseconds.Size = New System.Drawing.Size(64, 13)
        Me.lblMillseconds.TabIndex = 8
        Me.lblMillseconds.Text = "Milliseconds"
        '
        'txtMilliseconds
        '
        Me.txtMilliseconds.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtMilliseconds.Location = New System.Drawing.Point(380, 3)
        Me.txtMilliseconds.Name = "txtMilliseconds"
        Me.txtMilliseconds.Size = New System.Drawing.Size(25, 20)
        Me.txtMilliseconds.TabIndex = 9
        Me.txtMilliseconds.Text = "0"
        '
        'lblFormat
        '
        Me.lblFormat.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFormat.AutoSize = True
        Me.lblFormat.Location = New System.Drawing.Point(411, 6)
        Me.lblFormat.Name = "lblFormat"
        Me.lblFormat.Size = New System.Drawing.Size(39, 13)
        Me.lblFormat.TabIndex = 10
        Me.lblFormat.Text = "Format"
        '
        'txtFormat
        '
        Me.txtFormat.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtFormat.Location = New System.Drawing.Point(3, 30)
        Me.txtFormat.Name = "txtFormat"
        Me.txtFormat.Size = New System.Drawing.Size(200, 20)
        Me.txtFormat.TabIndex = 11
        Me.txtFormat.Text = "G"
        '
        'cmdFormat
        '
        Me.cmdFormat.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdFormat.AutoSize = True
        Me.cmdFormat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdFormat.Location = New System.Drawing.Point(209, 29)
        Me.cmdFormat.Name = "cmdFormat"
        Me.cmdFormat.Size = New System.Drawing.Size(49, 23)
        Me.cmdFormat.TabIndex = 12
        Me.cmdFormat.Text = "Format"
        Me.cmdFormat.UseVisualStyleBackColor = True
        '
        'lblRes
        '
        Me.lblRes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblRes.AutoSize = True
        Me.lblRes.Location = New System.Drawing.Point(264, 34)
        Me.lblRes.Name = "lblRes"
        Me.lblRes.Size = New System.Drawing.Size(0, 13)
        Me.lblRes.TabIndex = 13
        '
        'flpTicks
        '
        Me.flpTicks.AutoSize = True
        Me.flpTicks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpTicks.Controls.Add(Me.lblTicks)
        Me.flpTicks.Controls.Add(Me.txtTicks)
        Me.flpTicks.Controls.Add(Me.lblTicksFormat)
        Me.flpTicks.Controls.Add(Me.txtTicksFormat)
        Me.flpTicks.Controls.Add(Me.cmdTicks)
        Me.flpTicks.Controls.Add(Me.lblTicksRes)
        Me.flpTicks.Dock = System.Windows.Forms.DockStyle.Top
        Me.flpTicks.Location = New System.Drawing.Point(0, 55)
        Me.flpTicks.Name = "flpTicks"
        Me.flpTicks.Size = New System.Drawing.Size(564, 29)
        Me.flpTicks.TabIndex = 1
        '
        'lblTicks
        '
        Me.lblTicks.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTicks.AutoSize = True
        Me.lblTicks.Location = New System.Drawing.Point(3, 8)
        Me.lblTicks.Name = "lblTicks"
        Me.lblTicks.Size = New System.Drawing.Size(33, 13)
        Me.lblTicks.TabIndex = 0
        Me.lblTicks.Text = "Ticks"
        '
        'txtTicks
        '
        Me.txtTicks.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtTicks.Location = New System.Drawing.Point(42, 4)
        Me.txtTicks.Name = "txtTicks"
        Me.txtTicks.Size = New System.Drawing.Size(100, 20)
        Me.txtTicks.TabIndex = 1
        Me.txtTicks.Text = "0"
        '
        'lblTicksFormat
        '
        Me.lblTicksFormat.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTicksFormat.AutoSize = True
        Me.lblTicksFormat.Location = New System.Drawing.Point(148, 8)
        Me.lblTicksFormat.Name = "lblTicksFormat"
        Me.lblTicksFormat.Size = New System.Drawing.Size(39, 13)
        Me.lblTicksFormat.TabIndex = 2
        Me.lblTicksFormat.Text = "Format"
        '
        'txtTicksFormat
        '
        Me.txtTicksFormat.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtTicksFormat.Location = New System.Drawing.Point(193, 4)
        Me.txtTicksFormat.Name = "txtTicksFormat"
        Me.txtTicksFormat.Size = New System.Drawing.Size(200, 20)
        Me.txtTicksFormat.TabIndex = 3
        Me.txtTicksFormat.Text = "G"
        '
        'cmdTicks
        '
        Me.cmdTicks.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdTicks.AutoSize = True
        Me.cmdTicks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdTicks.Location = New System.Drawing.Point(399, 3)
        Me.cmdTicks.Name = "cmdTicks"
        Me.cmdTicks.Size = New System.Drawing.Size(49, 23)
        Me.cmdTicks.TabIndex = 4
        Me.cmdTicks.Text = "Format"
        Me.cmdTicks.UseVisualStyleBackColor = True
        '
        'lblTicksRes
        '
        Me.lblTicksRes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTicksRes.AutoSize = True
        Me.lblTicksRes.Location = New System.Drawing.Point(454, 8)
        Me.lblTicksRes.Name = "lblTicksRes"
        Me.lblTicksRes.Size = New System.Drawing.Size(0, 13)
        Me.lblTicksRes.TabIndex = 5
        '
        'frmTimeSpanFormattable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(564, 93)
        Me.Controls.Add(Me.flpTicks)
        Me.Controls.Add(Me.flpFull)
        Me.Name = "frmTimeSpanFormattable"
        Me.Text = "Testing Tools.TimeSpanFormattable"
        Me.flpFull.ResumeLayout(False)
        Me.flpFull.PerformLayout()
        Me.flpTicks.ResumeLayout(False)
        Me.flpTicks.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents flpFull As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblDays As System.Windows.Forms.Label
    Friend WithEvents txtDays As System.Windows.Forms.TextBox
    Friend WithEvents lblHours As System.Windows.Forms.Label
    Friend WithEvents txtHours As System.Windows.Forms.TextBox
    Friend WithEvents lblMinutes As System.Windows.Forms.Label
    Friend WithEvents txtMinutes As System.Windows.Forms.TextBox
    Friend WithEvents lblSeconds As System.Windows.Forms.Label
    Friend WithEvents txtSeconds As System.Windows.Forms.TextBox
    Friend WithEvents lblMillseconds As System.Windows.Forms.Label
    Friend WithEvents txtMilliseconds As System.Windows.Forms.TextBox
    Friend WithEvents lblFormat As System.Windows.Forms.Label
    Friend WithEvents txtFormat As System.Windows.Forms.TextBox
    Friend WithEvents cmdFormat As System.Windows.Forms.Button
    Friend WithEvents lblRes As System.Windows.Forms.Label
    Friend WithEvents flpTicks As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblTicks As System.Windows.Forms.Label
    Friend WithEvents txtTicks As System.Windows.Forms.TextBox
    Friend WithEvents lblTicksFormat As System.Windows.Forms.Label
    Friend WithEvents txtTicksFormat As System.Windows.Forms.TextBox
    Friend WithEvents cmdTicks As System.Windows.Forms.Button
    Friend WithEvents lblTicksRes As System.Windows.Forms.Label
End Class
