Namespace GUI
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ProgressMonitor
        Inherits System.Windows.Forms.Form


        

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.tlbMain = New System.Windows.Forms.TableLayoutPanel
            Me.lblMainInfo = New System.Windows.Forms.Label
            Me.pgbProgress = New System.Windows.Forms.ProgressBar
            Me.lblI = New System.Windows.Forms.Label
            Me.cmdCancel = New System.Windows.Forms.Button
            Me.tlbMain.SuspendLayout()
            Me.SuspendLayout()
            '
            'tlbMain
            '
            Me.tlbMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlbMain.Controls.Add(Me.lblMainInfo, 0, 0)
            Me.tlbMain.Controls.Add(Me.pgbProgress, 0, 1)
            Me.tlbMain.Controls.Add(Me.lblI, 0, 2)
            Me.tlbMain.Controls.Add(Me.cmdCancel, 0, 3)
            Me.tlbMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlbMain.Location = New System.Drawing.Point(0, 0)
            Me.tlbMain.Name = "tlbMain"
            Me.tlbMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlbMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlbMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlbMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlbMain.Size = New System.Drawing.Size(297, 118)
            Me.tlbMain.TabIndex = 0
            '
            'lblMainInfo
            '
            Me.lblMainInfo.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.lblMainInfo.AutoSize = True
            Me.lblMainInfo.Location = New System.Drawing.Point(148, 0)
            Me.lblMainInfo.Name = "lblMainInfo"
            Me.lblMainInfo.Size = New System.Drawing.Size(0, 13)
            Me.lblMainInfo.TabIndex = 0
            '
            'pgbProgress
            '
            Me.pgbProgress.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pgbProgress.Location = New System.Drawing.Point(3, 16)
            Me.pgbProgress.Name = "pgbProgress"
            Me.pgbProgress.Size = New System.Drawing.Size(291, 57)
            Me.pgbProgress.TabIndex = 1
            '
            'lblI
            '
            Me.lblI.AutoSize = True
            Me.lblI.Location = New System.Drawing.Point(3, 76)
            Me.lblI.Name = "lblI"
            Me.lblI.Size = New System.Drawing.Size(0, 13)
            Me.lblI.TabIndex = 2
            '
            'cmdCancel
            '
            Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cmdCancel.Location = New System.Drawing.Point(124, 92)
            Me.cmdCancel.Name = "cmdCancel"
            Me.cmdCancel.Size = New System.Drawing.Size(48, 23)
            Me.cmdCancel.TabIndex = 3
            Me.cmdCancel.Text = "&Storno"
            Me.cmdCancel.UseVisualStyleBackColor = True
            '
            'ProgressMonitor
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(297, 118)
            Me.ControlBox = False
            Me.Controls.Add(Me.tlbMain)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "ProgressMonitor"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Průběh"
            Me.tlbMain.ResumeLayout(False)
            Me.tlbMain.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Private WithEvents tlbMain As System.Windows.Forms.TableLayoutPanel
        Private WithEvents lblMainInfo As System.Windows.Forms.Label
        Private WithEvents pgbProgress As System.Windows.Forms.ProgressBar
        Private WithEvents lblI As System.Windows.Forms.Label
        Private WithEvents cmdCancel As System.Windows.Forms.Button
    End Class
End Namespace