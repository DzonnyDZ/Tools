<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConsoleClosing
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
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
        Me.cmdAllocate = New System.Windows.Forms.Button
        Me.cmdDealoccate = New System.Windows.Forms.Button
        Me.cmdWrite = New System.Windows.Forms.Button
        Me.cmdWriteLine = New System.Windows.Forms.Button
        Me.chkDetachOnClosing = New System.Windows.Forms.CheckBox
        Me.txtWrite = New System.Windows.Forms.TextBox
        Me.chkCancelClosing = New System.Windows.Forms.CheckBox
        Me.cmdAddHandler = New System.Windows.Forms.Button
        Me.cmdRemoveHandler = New System.Windows.Forms.Button
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.cmdAllocate)
        Me.FlowLayoutPanel1.Controls.Add(Me.cmdDealoccate)
        Me.FlowLayoutPanel1.Controls.Add(Me.txtWrite)
        Me.FlowLayoutPanel1.Controls.Add(Me.cmdWrite)
        Me.FlowLayoutPanel1.Controls.Add(Me.cmdWriteLine)
        Me.FlowLayoutPanel1.Controls.Add(Me.chkDetachOnClosing)
        Me.FlowLayoutPanel1.Controls.Add(Me.chkCancelClosing)
        Me.FlowLayoutPanel1.Controls.Add(Me.cmdAddHandler)
        Me.FlowLayoutPanel1.Controls.Add(Me.cmdRemoveHandler)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(284, 264)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'cmdAllocate
        '
        Me.cmdAllocate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdAllocate.AutoSize = True
        Me.cmdAllocate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdAllocate.Location = New System.Drawing.Point(3, 3)
        Me.cmdAllocate.Name = "cmdAllocate"
        Me.cmdAllocate.Size = New System.Drawing.Size(48, 23)
        Me.cmdAllocate.TabIndex = 0
        Me.cmdAllocate.Text = "Attach"
        Me.cmdAllocate.UseVisualStyleBackColor = True
        '
        'cmdDealoccate
        '
        Me.cmdDealoccate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdDealoccate.AutoSize = True
        Me.cmdDealoccate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdDealoccate.Location = New System.Drawing.Point(57, 3)
        Me.cmdDealoccate.Name = "cmdDealoccate"
        Me.cmdDealoccate.Size = New System.Drawing.Size(52, 23)
        Me.cmdDealoccate.TabIndex = 1
        Me.cmdDealoccate.Text = "Detach"
        Me.cmdDealoccate.UseVisualStyleBackColor = True
        '
        'cmdWrite
        '
        Me.cmdWrite.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdWrite.AutoSize = True
        Me.cmdWrite.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdWrite.Location = New System.Drawing.Point(221, 3)
        Me.cmdWrite.Name = "cmdWrite"
        Me.cmdWrite.Size = New System.Drawing.Size(42, 23)
        Me.cmdWrite.TabIndex = 3
        Me.cmdWrite.Text = "Write"
        Me.cmdWrite.UseVisualStyleBackColor = True
        '
        'cmdWriteLine
        '
        Me.cmdWriteLine.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdWriteLine.AutoSize = True
        Me.cmdWriteLine.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdWriteLine.Location = New System.Drawing.Point(3, 32)
        Me.cmdWriteLine.Name = "cmdWriteLine"
        Me.cmdWriteLine.Size = New System.Drawing.Size(61, 23)
        Me.cmdWriteLine.TabIndex = 4
        Me.cmdWriteLine.Text = "Write line"
        Me.cmdWriteLine.UseVisualStyleBackColor = True
        '
        'chkDetachOnClosing
        '
        Me.chkDetachOnClosing.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkDetachOnClosing.AutoSize = True
        Me.chkDetachOnClosing.Location = New System.Drawing.Point(70, 35)
        Me.chkDetachOnClosing.Name = "chkDetachOnClosing"
        Me.chkDetachOnClosing.Size = New System.Drawing.Size(112, 17)
        Me.chkDetachOnClosing.TabIndex = 5
        Me.chkDetachOnClosing.Text = "Detach on closing"
        Me.chkDetachOnClosing.UseVisualStyleBackColor = True
        '
        'txtWrite
        '
        Me.txtWrite.Location = New System.Drawing.Point(115, 3)
        Me.txtWrite.Name = "txtWrite"
        Me.txtWrite.Size = New System.Drawing.Size(100, 20)
        Me.txtWrite.TabIndex = 8
        '
        'chkCancelClosing
        '
        Me.chkCancelClosing.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.chkCancelClosing.AutoSize = True
        Me.chkCancelClosing.Location = New System.Drawing.Point(3, 64)
        Me.chkCancelClosing.Name = "chkCancelClosing"
        Me.chkCancelClosing.Size = New System.Drawing.Size(95, 17)
        Me.chkCancelClosing.TabIndex = 9
        Me.chkCancelClosing.Text = "Cancel closing"
        Me.chkCancelClosing.UseVisualStyleBackColor = True
        '
        'cmdAddHandler
        '
        Me.cmdAddHandler.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdAddHandler.AutoSize = True
        Me.cmdAddHandler.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdAddHandler.Location = New System.Drawing.Point(104, 61)
        Me.cmdAddHandler.Name = "cmdAddHandler"
        Me.cmdAddHandler.Size = New System.Drawing.Size(74, 23)
        Me.cmdAddHandler.TabIndex = 4
        Me.cmdAddHandler.Text = "Add handler"
        Me.cmdAddHandler.UseVisualStyleBackColor = True
        '
        'cmdRemoveHandler
        '
        Me.cmdRemoveHandler.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdRemoveHandler.AutoSize = True
        Me.cmdRemoveHandler.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdRemoveHandler.Location = New System.Drawing.Point(184, 61)
        Me.cmdRemoveHandler.Name = "cmdRemoveHandler"
        Me.cmdRemoveHandler.Size = New System.Drawing.Size(95, 23)
        Me.cmdRemoveHandler.TabIndex = 4
        Me.cmdRemoveHandler.Text = "Remove handler"
        Me.cmdRemoveHandler.UseVisualStyleBackColor = True
        '
        'frmConsoleClosing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 264)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Name = "frmConsoleClosing"
        Me.Text = "Testing Tools.ConsoleT.Closing"
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents cmdAllocate As System.Windows.Forms.Button
    Friend WithEvents cmdDealoccate As System.Windows.Forms.Button
    Friend WithEvents txtWrite As System.Windows.Forms.TextBox
    Friend WithEvents cmdWrite As System.Windows.Forms.Button
    Friend WithEvents cmdWriteLine As System.Windows.Forms.Button
    Friend WithEvents chkDetachOnClosing As System.Windows.Forms.CheckBox
    Friend WithEvents chkCancelClosing As System.Windows.Forms.CheckBox
    Friend WithEvents cmdAddHandler As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveHandler As System.Windows.Forms.Button
End Class
