Namespace WindowsT.FormsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ProgressMonitor
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProgressMonitor))
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
            Me.tlbMain.AccessibleDescription = Nothing
            Me.tlbMain.AccessibleName = Nothing
            resources.ApplyResources(Me.tlbMain, "tlbMain")
            Me.tlbMain.BackgroundImage = Nothing
            Me.tlbMain.Controls.Add(Me.lblMainInfo, 0, 0)
            Me.tlbMain.Controls.Add(Me.pgbProgress, 0, 1)
            Me.tlbMain.Controls.Add(Me.lblI, 0, 2)
            Me.tlbMain.Controls.Add(Me.cmdCancel, 0, 3)
            Me.tlbMain.Font = Nothing
            Me.tlbMain.Name = "tlbMain"
            '
            'lblMainInfo
            '
            Me.lblMainInfo.AccessibleDescription = Nothing
            Me.lblMainInfo.AccessibleName = Nothing
            resources.ApplyResources(Me.lblMainInfo, "lblMainInfo")
            Me.lblMainInfo.Font = Nothing
            Me.lblMainInfo.Name = "lblMainInfo"
            '
            'pgbProgress
            '
            Me.pgbProgress.AccessibleDescription = Nothing
            Me.pgbProgress.AccessibleName = Nothing
            resources.ApplyResources(Me.pgbProgress, "pgbProgress")
            Me.pgbProgress.BackgroundImage = Nothing
            Me.pgbProgress.Font = Nothing
            Me.pgbProgress.Name = "pgbProgress"
            '
            'lblI
            '
            Me.lblI.AccessibleDescription = Nothing
            Me.lblI.AccessibleName = Nothing
            resources.ApplyResources(Me.lblI, "lblI")
            Me.lblI.Font = Nothing
            Me.lblI.Name = "lblI"
            '
            'cmdCancel
            '
            Me.cmdCancel.AccessibleDescription = Nothing
            Me.cmdCancel.AccessibleName = Nothing
            resources.ApplyResources(Me.cmdCancel, "cmdCancel")
            Me.cmdCancel.BackgroundImage = Nothing
            Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cmdCancel.Font = Nothing
            Me.cmdCancel.Name = "cmdCancel"
            Me.cmdCancel.UseVisualStyleBackColor = True
            '
            'ProgressMonitor
            '
            Me.AccessibleDescription = Nothing
            Me.AccessibleName = Nothing
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Nothing
            Me.ControlBox = False
            Me.Controls.Add(Me.tlbMain)
            Me.Font = Nothing
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = Nothing
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "ProgressMonitor"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.tlbMain.ResumeLayout(False)
            Me.tlbMain.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Private WithEvents tlbMain As System.Windows.Forms.TableLayoutPanel
        Private WithEvents lblMainInfo As System.Windows.Forms.Label
        Private WithEvents pgbProgress As System.Windows.Forms.ProgressBar
        Private WithEvents lblI As System.Windows.Forms.Label
        Private WithEvents cmdCancel As System.Windows.Forms.Button
    End Class
End Namespace