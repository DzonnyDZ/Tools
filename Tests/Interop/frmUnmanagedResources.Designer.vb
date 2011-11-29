Namespace InteropT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmUnmanagedResources
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.


        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.tvwTree = New System.Windows.Forms.TreeView()
            Me.sfdSave = New System.Windows.Forms.SaveFileDialog()
            Me.SuspendLayout()
            '
            'tvwTree
            '
            Me.tvwTree.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tvwTree.Location = New System.Drawing.Point(0, 0)
            Me.tvwTree.Name = "tvwTree"
            Me.tvwTree.Size = New System.Drawing.Size(284, 262)
            Me.tvwTree.TabIndex = 0
            '
            'sfdSave
            '
            Me.sfdSave.Filter = "All files (*.*)|*.*"
            Me.sfdSave.Title = "Save resource"
            '
            'frmUnmanagedResources
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(284, 262)
            Me.Controls.Add(Me.tvwTree)
            Me.Name = "frmUnmanagedResources"
            Me.Text = "Unmanaged resources"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents tvwTree As System.Windows.Forms.TreeView
        Friend WithEvents sfdSave As System.Windows.Forms.SaveFileDialog
    End Class
End Namespace