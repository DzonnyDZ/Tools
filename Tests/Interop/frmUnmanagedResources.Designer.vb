Namespace InteropT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmUnmanagedResources
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.


        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.tvwTree = New System.Windows.Forms.TreeView()
            Me.sfdSave = New System.Windows.Forms.SaveFileDialog()
            Me.txtPreview = New System.Windows.Forms.TextBox()
            Me.splSplit = New System.Windows.Forms.SplitContainer()
            Me.flpLoadString = New System.Windows.Forms.FlowLayoutPanel()
            Me.nudStringId = New System.Windows.Forms.NumericUpDown()
            Me.cmdLoadString = New System.Windows.Forms.Button()
            CType(Me.splSplit, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.splSplit.Panel1.SuspendLayout()
            Me.splSplit.Panel2.SuspendLayout()
            Me.splSplit.SuspendLayout()
            Me.flpLoadString.SuspendLayout()
            CType(Me.nudStringId, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'tvwTree
            '
            Me.tvwTree.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tvwTree.Location = New System.Drawing.Point(0, 0)
            Me.tvwTree.Name = "tvwTree"
            Me.tvwTree.Size = New System.Drawing.Size(263, 394)
            Me.tvwTree.TabIndex = 0
            '
            'sfdSave
            '
            Me.sfdSave.Filter = "All files (*.*)|*.*"
            Me.sfdSave.Title = "Save resource"
            '
            'txtPreview
            '
            Me.txtPreview.Dock = System.Windows.Forms.DockStyle.Fill
            Me.txtPreview.Location = New System.Drawing.Point(0, 29)
            Me.txtPreview.Multiline = True
            Me.txtPreview.Name = "txtPreview"
            Me.txtPreview.ReadOnly = True
            Me.txtPreview.Size = New System.Drawing.Size(284, 365)
            Me.txtPreview.TabIndex = 1
            '
            'splSplit
            '
            Me.splSplit.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splSplit.Location = New System.Drawing.Point(0, 0)
            Me.splSplit.Name = "splSplit"
            '
            'splSplit.Panel1
            '
            Me.splSplit.Panel1.Controls.Add(Me.tvwTree)
            '
            'splSplit.Panel2
            '
            Me.splSplit.Panel2.Controls.Add(Me.txtPreview)
            Me.splSplit.Panel2.Controls.Add(Me.flpLoadString)
            Me.splSplit.Size = New System.Drawing.Size(551, 394)
            Me.splSplit.SplitterDistance = 263
            Me.splSplit.TabIndex = 2
            '
            'flpLoadString
            '
            Me.flpLoadString.AutoSize = True
            Me.flpLoadString.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpLoadString.Controls.Add(Me.nudStringId)
            Me.flpLoadString.Controls.Add(Me.cmdLoadString)
            Me.flpLoadString.Dock = System.Windows.Forms.DockStyle.Top
            Me.flpLoadString.Location = New System.Drawing.Point(0, 0)
            Me.flpLoadString.Name = "flpLoadString"
            Me.flpLoadString.Size = New System.Drawing.Size(284, 29)
            Me.flpLoadString.TabIndex = 2
            '
            'nudStringId
            '
            Me.nudStringId.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.nudStringId.Location = New System.Drawing.Point(3, 4)
            Me.nudStringId.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
            Me.nudStringId.Minimum = New Decimal(New Integer() {65535, 0, 0, -2147483648})
            Me.nudStringId.Name = "nudStringId"
            Me.nudStringId.Size = New System.Drawing.Size(120, 20)
            Me.nudStringId.TabIndex = 0
            '
            'cmdLoadString
            '
            Me.cmdLoadString.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdLoadString.Location = New System.Drawing.Point(129, 3)
            Me.cmdLoadString.Name = "cmdLoadString"
            Me.cmdLoadString.Size = New System.Drawing.Size(75, 23)
            Me.cmdLoadString.TabIndex = 1
            Me.cmdLoadString.Text = "LoadString"
            Me.cmdLoadString.UseVisualStyleBackColor = True
            '
            'frmUnmanagedResources
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(551, 394)
            Me.Controls.Add(Me.splSplit)
            Me.Name = "frmUnmanagedResources"
            Me.Text = "Unmanaged resources"
            Me.splSplit.Panel1.ResumeLayout(False)
            Me.splSplit.Panel2.ResumeLayout(False)
            Me.splSplit.Panel2.PerformLayout()
            CType(Me.splSplit, System.ComponentModel.ISupportInitialize).EndInit()
            Me.splSplit.ResumeLayout(False)
            Me.flpLoadString.ResumeLayout(False)
            CType(Me.nudStringId, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents tvwTree As System.Windows.Forms.TreeView
        Friend WithEvents sfdSave As System.Windows.Forms.SaveFileDialog
        Friend WithEvents txtPreview As System.Windows.Forms.TextBox
        Friend WithEvents splSplit As System.Windows.Forms.SplitContainer
        Friend WithEvents flpLoadString As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents nudStringId As System.Windows.Forms.NumericUpDown
        Friend WithEvents cmdLoadString As System.Windows.Forms.Button
    End Class
End Namespace