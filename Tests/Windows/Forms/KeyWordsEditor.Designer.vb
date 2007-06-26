Namespace WindowsT.FormsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmKeyWordsEditor
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
            Me.BottomToolStripPanel = New System.Windows.Forms.ToolStripPanel
            Me.TopToolStripPanel = New System.Windows.Forms.ToolStripPanel
            Me.RightToolStripPanel = New System.Windows.Forms.ToolStripPanel
            Me.LeftToolStripPanel = New System.Windows.Forms.ToolStripPanel
            Me.ContentPanel = New System.Windows.Forms.ToolStripContentPanel
            Me.pgrGrid = New System.Windows.Forms.PropertyGrid
            Me.kweEditor = New Tools.WindowsT.FormsT.KeyWordsEditor
            Me.panContainer.SuspendLayout()
            Me.SuspendLayout()
            '
            'panContainer
            '
            Me.panContainer.Controls.Add(Me.kweEditor)
            Me.panContainer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panContainer.Location = New System.Drawing.Point(0, 0)
            Me.panContainer.Name = "panContainer"
            Me.panContainer.Size = New System.Drawing.Size(455, 475)
            Me.panContainer.TabIndex = 0
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
            'pgrGrid
            '
            Me.pgrGrid.Dock = System.Windows.Forms.DockStyle.Right
            Me.pgrGrid.Location = New System.Drawing.Point(455, 0)
            Me.pgrGrid.Name = "pgrGrid"
            Me.pgrGrid.SelectedObject = Me.kweEditor
            Me.pgrGrid.Size = New System.Drawing.Size(223, 475)
            Me.pgrGrid.TabIndex = 1
            '
            'kweEditor
            '
            Me.kweEditor.AutoCompleteStable = Nothing
            Me.kweEditor.Dock = System.Windows.Forms.DockStyle.Fill
            Me.kweEditor.Location = New System.Drawing.Point(0, 0)
            Me.kweEditor.Name = "kweEditor"
            Me.kweEditor.Size = New System.Drawing.Size(455, 475)
            '
            '
            '
            Me.kweEditor.Status.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
            Me.kweEditor.Status.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.kweEditor.Status.AutoChanged = False
            Me.kweEditor.Status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.kweEditor.Status.DeleteMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweEditor.Status.Location = New System.Drawing.Point(384, 1)
            Me.kweEditor.Status.Margin = New System.Windows.Forms.Padding(0)
            Me.kweEditor.Status.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
            Me.kweEditor.Status.Name = "stmStatus"
            Me.kweEditor.Status.Size = New System.Drawing.Size(24, 24)
            Me.kweEditor.Status.TabIndex = 2
            Me.kweEditor.Status.TabStop = False
            Me.kweEditor.Synonyms = Nothing
            Me.kweEditor.TabIndex = 0
            '
            'frmKeyWordsEditor
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(678, 475)
            Me.Controls.Add(Me.panContainer)
            Me.Controls.Add(Me.pgrGrid)
            Me.Name = "frmKeyWordsEditor"
            Me.Text = "Testing Tools.Windows.Forms.TransparentLabel"
            Me.panContainer.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents panContainer As System.Windows.Forms.Panel
        Friend WithEvents pgrGrid As System.Windows.Forms.PropertyGrid
        Friend WithEvents BottomToolStripPanel As System.Windows.Forms.ToolStripPanel
        Friend WithEvents TopToolStripPanel As System.Windows.Forms.ToolStripPanel
        Friend WithEvents RightToolStripPanel As System.Windows.Forms.ToolStripPanel
        Friend WithEvents LeftToolStripPanel As System.Windows.Forms.ToolStripPanel
        Friend WithEvents ContentPanel As System.Windows.Forms.ToolStripContentPanel
        Friend WithEvents kweEditor As Tools.WindowsT.FormsT.KeyWordsEditor
    End Class
End Namespace