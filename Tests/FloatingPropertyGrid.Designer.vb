<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FloatingPropertyGrid
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
        Me.prgPrg = New System.Windows.Forms.PropertyGrid
        Me.SuspendLayout()
        '
        'prgPrg
        '
        Me.prgPrg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.prgPrg.Location = New System.Drawing.Point(0, 0)
        Me.prgPrg.Name = "prgPrg"
        Me.prgPrg.Size = New System.Drawing.Size(184, 390)
        Me.prgPrg.TabIndex = 0
        '
        'FloatingPropertyGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(184, 390)
        Me.Controls.Add(Me.prgPrg)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FloatingPropertyGrid"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Floating PropertyGrid"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents prgPrg As System.Windows.Forms.PropertyGrid
End Class
