Namespace WindowsT.FormsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmProgressBarWithText
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
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.pbtPgb = New Tools.WindowsT.FormsT.ProgressBarWithText
            Me.pgrGrid = New System.Windows.Forms.PropertyGrid
            Me.txtLog = New System.Windows.Forms.TextBox
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.SuspendLayout()
            '
            'splMain
            '
            Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
            Me.splMain.Location = New System.Drawing.Point(0, 0)
            Me.splMain.Name = "splMain"
            '
            'splMain.Panel1
            '
            Me.splMain.Panel1.Controls.Add(Me.txtLog)
            Me.splMain.Panel1.Controls.Add(Me.pbtPgb)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.pgrGrid)
            Me.splMain.Size = New System.Drawing.Size(457, 398)
            Me.splMain.SplitterDistance = 240
            Me.splMain.TabIndex = 0
            '
            'pbtPgb
            '
            Me.pbtPgb.AutoText = True
            Me.pbtPgb.Dock = System.Windows.Forms.DockStyle.Top
            Me.pbtPgb.Location = New System.Drawing.Point(0, 0)
            Me.pbtPgb.Name = "pbtPgb"
            Me.pbtPgb.Size = New System.Drawing.Size(240, 52)
            Me.pbtPgb.TabIndex = 0
            Me.pbtPgb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'pgrGrid
            '
            Me.pgrGrid.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pgrGrid.Location = New System.Drawing.Point(0, 0)
            Me.pgrGrid.Name = "pgrGrid"
            Me.pgrGrid.SelectedObject = Me.pbtPgb
            Me.pgrGrid.Size = New System.Drawing.Size(213, 398)
            Me.pgrGrid.TabIndex = 0
            '
            'txtLog
            '
            Me.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.txtLog.Location = New System.Drawing.Point(0, 274)
            Me.txtLog.Multiline = True
            Me.txtLog.Name = "txtLog"
            Me.txtLog.ReadOnly = True
            Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtLog.Size = New System.Drawing.Size(240, 124)
            Me.txtLog.TabIndex = 1
            Me.txtLog.TabStop = False
            Me.txtLog.WordWrap = False
            '
            'frmProgressBarWithText
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(457, 398)
            Me.Controls.Add(Me.splMain)
            Me.Name = "frmProgressBarWithText"
            Me.Text = "Testing Tools.Windows.Forms.ProgressBarWithText"
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel1.PerformLayout()
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents splMain As System.Windows.Forms.SplitContainer
        Friend WithEvents pgrGrid As System.Windows.Forms.PropertyGrid
        Friend WithEvents pbtPgb As Tools.WindowsT.FormsT.ProgressBarWithText
        Friend WithEvents txtLog As System.Windows.Forms.TextBox
    End Class
End Namespace