Namespace GeneratorsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmCustomToolBase
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustomToolBase))
            Me.tosMain = New System.Windows.Forms.ToolStrip
            Me.txtVersion = New System.Windows.Forms.ToolStripTextBox
            Me.tsbGetLanguages = New System.Windows.Forms.ToolStripButton
            Me.tsbInstantiateCustomTool = New System.Windows.Forms.ToolStripButton
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.tvwNodes = New System.Windows.Forms.TreeView
            Me.prgProperties = New System.Windows.Forms.PropertyGrid
            Me.tosMain.SuspendLayout()
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.SuspendLayout()
            '
            'tosMain
            '
            Me.tosMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.tosMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtVersion, Me.tsbGetLanguages, Me.tsbInstantiateCustomTool})
            Me.tosMain.Location = New System.Drawing.Point(0, 0)
            Me.tosMain.Name = "tosMain"
            Me.tosMain.Size = New System.Drawing.Size(490, 25)
            Me.tosMain.TabIndex = 0
            Me.tosMain.Text = "ToolStrip1"
            '
            'txtVersion
            '
            Me.txtVersion.Name = "txtVersion"
            Me.txtVersion.Size = New System.Drawing.Size(100, 25)
            Me.txtVersion.Text = "9.0"
            '
            'tsbGetLanguages
            '
            Me.tsbGetLanguages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
            Me.tsbGetLanguages.Image = CType(resources.GetObject("tsbGetLanguages.Image"), System.Drawing.Image)
            Me.tsbGetLanguages.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.tsbGetLanguages.Name = "tsbGetLanguages"
            Me.tsbGetLanguages.Size = New System.Drawing.Size(86, 22)
            Me.tsbGetLanguages.Text = "Get languages"
            '
            'tsbInstantiateCustomTool
            '
            Me.tsbInstantiateCustomTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
            Me.tsbInstantiateCustomTool.Enabled = False
            Me.tsbInstantiateCustomTool.Image = CType(resources.GetObject("tsbInstantiateCustomTool.Image"), System.Drawing.Image)
            Me.tsbInstantiateCustomTool.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.tsbInstantiateCustomTool.Name = "tsbInstantiateCustomTool"
            Me.tsbInstantiateCustomTool.Size = New System.Drawing.Size(133, 22)
            Me.tsbInstantiateCustomTool.Text = "Instantiate custom tool"
            '
            'splMain
            '
            Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splMain.Location = New System.Drawing.Point(0, 25)
            Me.splMain.Name = "splMain"
            '
            'splMain.Panel1
            '
            Me.splMain.Panel1.Controls.Add(Me.tvwNodes)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.prgProperties)
            Me.splMain.Size = New System.Drawing.Size(490, 274)
            Me.splMain.SplitterDistance = 163
            Me.splMain.TabIndex = 1
            '
            'tvwNodes
            '
            Me.tvwNodes.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tvwNodes.Location = New System.Drawing.Point(0, 0)
            Me.tvwNodes.Name = "tvwNodes"
            Me.tvwNodes.Size = New System.Drawing.Size(163, 274)
            Me.tvwNodes.TabIndex = 0
            '
            'prgProperties
            '
            Me.prgProperties.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgProperties.Location = New System.Drawing.Point(0, 0)
            Me.prgProperties.Name = "prgProperties"
            Me.prgProperties.Size = New System.Drawing.Size(323, 274)
            Me.prgProperties.TabIndex = 0
            '
            'frmCustomToolBase
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(490, 299)
            Me.Controls.Add(Me.splMain)
            Me.Controls.Add(Me.tosMain)
            Me.Name = "frmCustomToolBase"
            Me.Text = "Testing Tools.VisualStudioT.GeneratorsT.CustomToolBase"
            Me.tosMain.ResumeLayout(False)
            Me.tosMain.PerformLayout()
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents tosMain As System.Windows.Forms.ToolStrip
        Friend WithEvents txtVersion As System.Windows.Forms.ToolStripTextBox
        Friend WithEvents tsbGetLanguages As System.Windows.Forms.ToolStripButton
        Friend WithEvents tsbInstantiateCustomTool As System.Windows.Forms.ToolStripButton
        Friend WithEvents splMain As System.Windows.Forms.SplitContainer
        Friend WithEvents tvwNodes As System.Windows.Forms.TreeView
        Friend WithEvents prgProperties As System.Windows.Forms.PropertyGrid
    End Class

End Namespace