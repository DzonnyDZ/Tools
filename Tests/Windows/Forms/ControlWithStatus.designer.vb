Namespace WindowsT.FormsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmControlWithStatus
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
            Me.BottomToolStripPanel = New System.Windows.Forms.ToolStripPanel
            Me.TopToolStripPanel = New System.Windows.Forms.ToolStripPanel
            Me.RightToolStripPanel = New System.Windows.Forms.ToolStripPanel
            Me.LeftToolStripPanel = New System.Windows.Forms.ToolStripPanel
            Me.ContentPanel = New System.Windows.Forms.ToolStripContentPanel
            Me.TestControl1 = New Tools.Tests.WindowsT.FormsT.TestControl
            Me.TextBoxWithStatus1 = New Tools.WindowsT.FormsT.MaskedTextBoxWithStatus
            Me.SuspendLayout()
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
            'TestControl1
            '
            Me.TestControl1.Location = New System.Drawing.Point(53, 33)
            Me.TestControl1.Name = "TestControl1"
            Me.TestControl1.Size = New System.Drawing.Size(154, 33)
            Me.TestControl1.TabIndex = 0
            '
            'TextBoxWithStatus1
            '
            Me.TextBoxWithStatus1.Location = New System.Drawing.Point(91, 213)
            Me.TextBoxWithStatus1.MinimumSize = New System.Drawing.Size(40, 20)
            Me.TextBoxWithStatus1.Name = "TextBoxWithStatus1"
            Me.TextBoxWithStatus1.Size = New System.Drawing.Size(222, 42)
            '
            '
            '
            
            Me.TextBoxWithStatus1.TabIndex = 1
            '
            'frmControlWithStatus
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(524, 494)
            Me.Controls.Add(Me.TextBoxWithStatus1)
            Me.Controls.Add(Me.TestControl1)
            Me.Name = "frmControlWithStatus"
            Me.Text = "Testing Tools.Windows.Forms.TransparentTextBox"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents BottomToolStripPanel As System.Windows.Forms.ToolStripPanel
        Friend WithEvents TopToolStripPanel As System.Windows.Forms.ToolStripPanel
        Friend WithEvents RightToolStripPanel As System.Windows.Forms.ToolStripPanel
        Friend WithEvents LeftToolStripPanel As System.Windows.Forms.ToolStripPanel
        Friend WithEvents ContentPanel As System.Windows.Forms.ToolStripContentPanel
        Friend WithEvents TestControl1 As Tools.Tests.WindowsT.FormsT.TestControl
        Friend WithEvents TextBoxWithStatus1 As Tools.WindowsT.FormsT.MaskedTextBoxWithStatus
    End Class
End Namespace