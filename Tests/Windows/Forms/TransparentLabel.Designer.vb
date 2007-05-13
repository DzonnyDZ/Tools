Namespace Windows.Forms
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
            Me.tlbLabel = New Tools.Windows.Forms.TransparentLabel
            Me.pgrGrid = New System.Windows.Forms.PropertyGrid
            Me.lblBg = New System.Windows.Forms.Label
            Me.panContainer.SuspendLayout()
            Me.SuspendLayout()
            '
            'panContainer
            '
            Me.panContainer.Controls.Add(Me.tlbLabel)
            Me.panContainer.Controls.Add(Me.lblBg)
            Me.panContainer.Dock = System.Windows.Forms.DockStyle.Top
            Me.panContainer.Location = New System.Drawing.Point(0, 0)
            Me.panContainer.Name = "panContainer"
            Me.panContainer.Size = New System.Drawing.Size(123, 88)
            Me.panContainer.TabIndex = 0
            '
            'tlbLabel
            '
            Me.tlbLabel.BackColor = System.Drawing.SystemColors.Control
            Me.tlbLabel.Location = New System.Drawing.Point(3, 3)
            Me.tlbLabel.Name = "tlbLabel"
            Me.tlbLabel.Size = New System.Drawing.Size(104, 20)
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
        Friend WithEvents tlbLabel As Tools.Windows.Forms.TransparentLabel
        Friend WithEvents pgrGrid As System.Windows.Forms.PropertyGrid
        Friend WithEvents lblBg As System.Windows.Forms.Label
    End Class
End Namespace