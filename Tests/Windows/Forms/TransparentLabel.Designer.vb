Namespace WindowsT.FormsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmTransparentLabel
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
            Me.panContainer = New System.Windows.Forms.Panel
            Me.lblBg = New System.Windows.Forms.Label
            Me.BottomToolStripPanel = New System.Windows.Forms.ToolStripPanel
            Me.TopToolStripPanel = New System.Windows.Forms.ToolStripPanel
            Me.RightToolStripPanel = New System.Windows.Forms.ToolStripPanel
            Me.LeftToolStripPanel = New System.Windows.Forms.ToolStripPanel
            Me.ContentPanel = New System.Windows.Forms.ToolStripContentPanel
            Me.tlbLabel = New Tools.WindowsT.FormsT.TransparentLabel
            Me.pgrGrid = New System.Windows.Forms.PropertyGrid
            Me.panContainer.SuspendLayout()
            Me.SuspendLayout()
            '
            'panContainer
            '
            Me.panContainer.Controls.Add(Me.tlbLabel)
            Me.panContainer.Controls.Add(Me.lblBg)
            Me.panContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panContainer.Location = New System.Drawing.Point(0, 0)
            Me.panContainer.Name = "panContainer"
            Me.panContainer.Size = New System.Drawing.Size(123, 443)
            Me.panContainer.TabIndex = 0
            '
            'lblBg
            '
            Me.lblBg.AutoSize = True
            Me.lblBg.BackColor = System.Drawing.Color.Red
            Me.lblBg.ForeColor = System.Drawing.Color.Yellow
            Me.lblBg.Location = New System.Drawing.Point(12, 3)
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
            'tlbLabel
            '
            Me.tlbLabel.AutoSize = False
            Me.tlbLabel.Location = New System.Drawing.Point(3, 3)
            Me.tlbLabel.Name = "tlbLabel"
            Me.tlbLabel.Size = New System.Drawing.Size(90, 13)
            Me.tlbLabel.TabIndex = 0
            Me.tlbLabel.TabStop = True
            Me.tlbLabel.Text = "TransparentLabel"
            '
            'pgrGrid
            '
            Me.pgrGrid.Dock = System.Windows.Forms.DockStyle.Right
            Me.pgrGrid.Location = New System.Drawing.Point(123, 0)
            Me.pgrGrid.Name = "pgrGrid"
            Me.pgrGrid.SelectedObject = Me.tlbLabel
            Me.pgrGrid.Size = New System.Drawing.Size(223, 443)
            Me.pgrGrid.TabIndex = 1
            '
            'frmTransparentLabel
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(346, 443)
            Me.Controls.Add(Me.panContainer)
            Me.Controls.Add(Me.pgrGrid)
            Me.Name = "frmTransparentLabel"
            Me.Text = "Testing Tools.Windows.Forms.TransparentLabel"
            Me.panContainer.ResumeLayout(False)
            Me.panContainer.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents panContainer As System.Windows.Forms.Panel
        Friend WithEvents tlbLabel As Tools.WindowsT.FormsT.TransparentLabel
        Friend WithEvents lblBg As System.Windows.Forms.Label
        Friend WithEvents pgrGrid As System.Windows.Forms.PropertyGrid
        Friend WithEvents BottomToolStripPanel As System.Windows.Forms.ToolStripPanel
        Friend WithEvents TopToolStripPanel As System.Windows.Forms.ToolStripPanel
        Friend WithEvents RightToolStripPanel As System.Windows.Forms.ToolStripPanel
        Friend WithEvents LeftToolStripPanel As System.Windows.Forms.ToolStripPanel
        Friend WithEvents ContentPanel As System.Windows.Forms.ToolStripContentPanel
    End Class
End Namespace