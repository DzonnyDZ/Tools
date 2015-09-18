Namespace ComponentModelT
    '#If TrueStage conditional compilation of this file is set in Tests.vbproj
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmLocalizableAttributes
        Friend WithEvents prgMain As System.Windows.Forms.PropertyGrid
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.prgMain = New System.Windows.Forms.PropertyGrid
            Me.cmdReset = New System.Windows.Forms.Button
            Me.SuspendLayout()
            '
            'prgMain
            '
            Me.prgMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgMain.Location = New System.Drawing.Point(0, 0)
            Me.prgMain.Name = "prgMain"
            Me.prgMain.Size = New System.Drawing.Size(328, 353)
            Me.prgMain.TabIndex = 0
            '
            'cmdReset
            '
            Me.cmdReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmdReset.AutoSize = True
            Me.cmdReset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdReset.Location = New System.Drawing.Point(283, 330)
            Me.cmdReset.Name = "cmdReset"
            Me.cmdReset.Size = New System.Drawing.Size(45, 23)
            Me.cmdReset.TabIndex = 1
            Me.cmdReset.Text = "&Reset"
            Me.cmdReset.UseVisualStyleBackColor = True
            '
            'frmLocalizableAttributes
            '
            Me.ClientSize = New System.Drawing.Size(328, 353)
            Me.Controls.Add(Me.cmdReset)
            Me.Controls.Add(Me.prgMain)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmLocalizableAttributes"
            Me.ShowInTaskbar = False
            Me.Text = "Testing localizable attributes"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents cmdReset As System.Windows.Forms.Button
    End Class
End Namespace