Namespace DevicesT
    '#If Config <= RC Then Stage conditional compilation of this file is set in Tests.vbproj
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmLowLevelKeyboardHook
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
            Me.cmdRegister = New System.Windows.Forms.Button
            Me.cmdRegisterAsync = New System.Windows.Forms.Button
            Me.lstMessages = New System.Windows.Forms.ListBox
            Me.cmdClear = New System.Windows.Forms.Button
            Me.cmdUnregister = New System.Windows.Forms.Button
            Me.chkSuppress = New System.Windows.Forms.CheckBox
            Me.chkHandled = New System.Windows.Forms.CheckBox
            Me.tlpMain.SuspendLayout()
            Me.SuspendLayout()
            '
            'tlpMain
            '
            Me.tlpMain.ColumnCount = 3
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
            Me.tlpMain.Controls.Add(Me.cmdRegister, 0, 0)
            Me.tlpMain.Controls.Add(Me.cmdRegisterAsync, 1, 0)
            Me.tlpMain.Controls.Add(Me.lstMessages, 0, 2)
            Me.tlpMain.Controls.Add(Me.cmdClear, 0, 3)
            Me.tlpMain.Controls.Add(Me.cmdUnregister, 2, 0)
            Me.tlpMain.Controls.Add(Me.chkSuppress, 0, 1)
            Me.tlpMain.Controls.Add(Me.chkHandled, 2, 1)
            Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpMain.Location = New System.Drawing.Point(0, 0)
            Me.tlpMain.Name = "tlpMain"
            Me.tlpMain.RowCount = 4
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.Size = New System.Drawing.Size(328, 353)
            Me.tlpMain.TabIndex = 0
            '
            'cmdRegister
            '
            Me.cmdRegister.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdRegister.AutoSize = True
            Me.cmdRegister.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdRegister.Location = New System.Drawing.Point(26, 3)
            Me.cmdRegister.Name = "cmdRegister"
            Me.cmdRegister.Size = New System.Drawing.Size(56, 23)
            Me.cmdRegister.TabIndex = 0
            Me.cmdRegister.Text = "Register"
            Me.cmdRegister.UseVisualStyleBackColor = True
            '
            'cmdRegisterAsync
            '
            Me.cmdRegisterAsync.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdRegisterAsync.AutoSize = True
            Me.cmdRegisterAsync.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdRegisterAsync.Location = New System.Drawing.Point(120, 3)
            Me.cmdRegisterAsync.Name = "cmdRegisterAsync"
            Me.cmdRegisterAsync.Size = New System.Drawing.Size(87, 23)
            Me.cmdRegisterAsync.TabIndex = 1
            Me.cmdRegisterAsync.Text = "Register async"
            Me.cmdRegisterAsync.UseVisualStyleBackColor = True
            '
            'lstMessages
            '
            Me.tlpMain.SetColumnSpan(Me.lstMessages, 3)
            Me.lstMessages.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lstMessages.FormattingEnabled = True
            Me.lstMessages.IntegralHeight = False
            Me.lstMessages.Location = New System.Drawing.Point(3, 55)
            Me.lstMessages.Name = "lstMessages"
            Me.lstMessages.Size = New System.Drawing.Size(322, 266)
            Me.lstMessages.TabIndex = 2
            '
            'cmdClear
            '
            Me.cmdClear.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdClear.AutoSize = True
            Me.cmdClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpMain.SetColumnSpan(Me.cmdClear, 3)
            Me.cmdClear.Location = New System.Drawing.Point(143, 327)
            Me.cmdClear.Name = "cmdClear"
            Me.cmdClear.Size = New System.Drawing.Size(41, 23)
            Me.cmdClear.TabIndex = 3
            Me.cmdClear.Text = "Clear"
            Me.cmdClear.UseVisualStyleBackColor = True
            '
            'cmdUnregister
            '
            Me.cmdUnregister.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdUnregister.AutoSize = True
            Me.cmdUnregister.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdUnregister.Enabled = False
            Me.cmdUnregister.Location = New System.Drawing.Point(240, 3)
            Me.cmdUnregister.Name = "cmdUnregister"
            Me.cmdUnregister.Size = New System.Drawing.Size(65, 23)
            Me.cmdUnregister.TabIndex = 4
            Me.cmdUnregister.Text = "Unregister"
            Me.cmdUnregister.UseVisualStyleBackColor = True
            '
            'chkSuppress
            '
            Me.chkSuppress.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.chkSuppress.AutoSize = True
            Me.chkSuppress.Location = New System.Drawing.Point(19, 32)
            Me.chkSuppress.Name = "chkSuppress"
            Me.chkSuppress.Size = New System.Drawing.Size(70, 17)
            Me.chkSuppress.TabIndex = 5
            Me.chkSuppress.Text = "Suppress"
            Me.chkSuppress.UseVisualStyleBackColor = True
            '
            'chkHandled
            '
            Me.chkHandled.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.chkHandled.AutoSize = True
            Me.chkHandled.Location = New System.Drawing.Point(240, 32)
            Me.chkHandled.Name = "chkHandled"
            Me.chkHandled.Size = New System.Drawing.Size(66, 17)
            Me.chkHandled.TabIndex = 6
            Me.chkHandled.Text = "Handled"
            Me.chkHandled.UseVisualStyleBackColor = True
            '
            'frmLowLevelKeyboardHook
            '
            Me.ClientSize = New System.Drawing.Size(328, 353)
            Me.Controls.Add(Me.tlpMain)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmLowLevelKeyboardHook"
            Me.ShowInTaskbar = False
            Me.Text = "Testing LowLevelKeyboardHook"
            Me.tlpMain.ResumeLayout(False)
            Me.tlpMain.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents cmdRegister As System.Windows.Forms.Button
        Friend WithEvents cmdRegisterAsync As System.Windows.Forms.Button
        Friend WithEvents lstMessages As System.Windows.Forms.ListBox
        Friend WithEvents cmdClear As System.Windows.Forms.Button
        Friend WithEvents cmdUnregister As System.Windows.Forms.Button
        Friend WithEvents chkSuppress As System.Windows.Forms.CheckBox
        Friend WithEvents chkHandled As System.Windows.Forms.CheckBox
    End Class
End Namespace