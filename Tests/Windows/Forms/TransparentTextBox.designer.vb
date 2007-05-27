Namespace WindowsT.FormsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmTransparentTextBox
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
            Me.lblBg = New System.Windows.Forms.Label
            Me.BottomToolStripPanel = New System.Windows.Forms.ToolStripPanel
            Me.TopToolStripPanel = New System.Windows.Forms.ToolStripPanel
            Me.RightToolStripPanel = New System.Windows.Forms.ToolStripPanel
            Me.LeftToolStripPanel = New System.Windows.Forms.ToolStripPanel
            Me.ContentPanel = New System.Windows.Forms.ToolStripContentPanel
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
            Me.trb = New Tools.WindowsT.FormsT.TransparentTextBox
            Me.pgrGrid = New System.Windows.Forms.PropertyGrid
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            Me.SuspendLayout()
            '
            'lblBg
            '
            Me.lblBg.AutoSize = True
            Me.lblBg.BackColor = System.Drawing.Color.Red
            Me.lblBg.ForeColor = System.Drawing.Color.Yellow
            Me.lblBg.Location = New System.Drawing.Point(12, 29)
            Me.lblBg.Name = "lblBg"
            Me.lblBg.Size = New System.Drawing.Size(65, 13)
            Me.lblBg.TabIndex = 1
            Me.lblBg.Text = "Background"
            '
            'BottomToolStripPanel
            '
            Me.BottomToolStripPanel.Location = New System.Drawing.Point(0, 0)
            Me.BottomToolStripPanel.Name = "BottomToolStripPanel"
            Me.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
            Me.BottomToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
            Me.BottomToolStripPanel.Size = New System.Drawing.Size(0, 0)
            '
            'TopToolStripPanel
            '
            Me.TopToolStripPanel.Location = New System.Drawing.Point(0, 0)
            Me.TopToolStripPanel.Name = "TopToolStripPanel"
            Me.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
            Me.TopToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
            Me.TopToolStripPanel.Size = New System.Drawing.Size(0, 0)
            '
            'RightToolStripPanel
            '
            Me.RightToolStripPanel.Location = New System.Drawing.Point(0, 0)
            Me.RightToolStripPanel.Name = "RightToolStripPanel"
            Me.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
            Me.RightToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
            Me.RightToolStripPanel.Size = New System.Drawing.Size(0, 0)
            '
            'LeftToolStripPanel
            '
            Me.LeftToolStripPanel.Location = New System.Drawing.Point(0, 0)
            Me.LeftToolStripPanel.Name = "LeftToolStripPanel"
            Me.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
            Me.LeftToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
            Me.LeftToolStripPanel.Size = New System.Drawing.Size(0, 0)
            '
            'ContentPanel
            '
            Me.ContentPanel.Size = New System.Drawing.Size(150, 150)
            '
            'SplitContainer1
            '
            Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
            Me.SplitContainer1.Name = "SplitContainer1"
            '
            'SplitContainer1.Panel1
            '
            Me.SplitContainer1.Panel1.Controls.Add(Me.trb)
            Me.SplitContainer1.Panel1.Controls.Add(Me.lblBg)
            '
            'SplitContainer1.Panel2
            '
            Me.SplitContainer1.Panel2.Controls.Add(Me.pgrGrid)
            Me.SplitContainer1.Size = New System.Drawing.Size(524, 494)
            Me.SplitContainer1.SplitterDistance = 174
            Me.SplitContainer1.TabIndex = 2
            '
            'trb
            '
            Me.trb.Location = New System.Drawing.Point(3, 3)
            Me.trb.Name = "trb"
            Me.trb.Size = New System.Drawing.Size(161, 295)
            Me.trb.TabIndex = 2
            Me.trb.Text = "TransparentTextBox"
            '
            'pgrGrid
            '
            Me.pgrGrid.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pgrGrid.Location = New System.Drawing.Point(0, 0)
            Me.pgrGrid.Name = "pgrGrid"
            Me.pgrGrid.SelectedObject = Me.trb
            Me.pgrGrid.Size = New System.Drawing.Size(346, 494)
            Me.pgrGrid.TabIndex = 1
            '
            'frmTransparentTextBox
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(524, 494)
            Me.Controls.Add(Me.SplitContainer1)
            Me.Name = "frmTransparentTextBox"
            Me.Text = "Testing Tools.Windows.Forms.TransparentTextBox"
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel1.PerformLayout()
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            Me.SplitContainer1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents lblBg As System.Windows.Forms.Label
        Friend WithEvents pgrGrid As System.Windows.Forms.PropertyGrid
        Friend WithEvents BottomToolStripPanel As System.Windows.Forms.ToolStripPanel
        Friend WithEvents TopToolStripPanel As System.Windows.Forms.ToolStripPanel
        Friend WithEvents RightToolStripPanel As System.Windows.Forms.ToolStripPanel
        Friend WithEvents LeftToolStripPanel As System.Windows.Forms.ToolStripPanel
        Friend WithEvents ContentPanel As System.Windows.Forms.ToolStripContentPanel
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
        Friend WithEvents trb As Tools.WindowsT.FormsT.TransparentTextBox
    End Class
End Namespace