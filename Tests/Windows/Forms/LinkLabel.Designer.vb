Namespace Windows.Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmLinkLabel
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
            Me.llbLabel = New Tools.Windows.Forms.LinkLabel
            Me.pgrLabel = New System.Windows.Forms.PropertyGrid
            Me.SuspendLayout()
            '
            'llbLabel
            '
            Me.llbLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.llbLabel.AutoSize = True
            Me.llbLabel.Location = New System.Drawing.Point(12, 109)
            Me.llbLabel.Name = "llbLabel"
            Me.llbLabel.Size = New System.Drawing.Size(0, 13)
            Me.llbLabel.TabIndex = 2
            '
            'pgrLabel
            '
            Me.pgrLabel.Dock = System.Windows.Forms.DockStyle.Right
            Me.pgrLabel.Location = New System.Drawing.Point(166, 0)
            Me.pgrLabel.Name = "pgrLabel"
            Me.pgrLabel.SelectedObject = Me.llbLabel
            Me.pgrLabel.Size = New System.Drawing.Size(356, 300)
            Me.pgrLabel.TabIndex = 1
            '
            'frmLinkLabel
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(522, 300)
            Me.Controls.Add(Me.llbLabel)
            Me.Controls.Add(Me.pgrLabel)
            Me.Name = "frmLinkLabel"
            Me.ShowInTaskbar = False
            Me.Text = "Testing Tools.Windows.Forms.LinkLabel"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents pgrLabel As System.Windows.Forms.PropertyGrid
        Friend WithEvents llbLabel As Tools.Windows.Forms.LinkLabel
    End Class
End Namespace