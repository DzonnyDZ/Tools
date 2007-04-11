Namespace Windows.Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmExtendedForm
        Inherits Tools.Windows.Forms.ExtendedForm

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.pgrProperty = New System.Windows.Forms.PropertyGrid
            Me.SuspendLayout()
            '
            'pgrProperty
            '
            Me.pgrProperty.Dock = System.Windows.Forms.DockStyle.Fill
            Me.pgrProperty.Location = New System.Drawing.Point(0, 0)
            Me.pgrProperty.Name = "pgrProperty"
            Me.pgrProperty.SelectedObject = Me
            Me.pgrProperty.Size = New System.Drawing.Size(442, 345)
            Me.pgrProperty.TabIndex = 6
            '
            'frmExtendedForm
            '
            Me.ClientSize = New System.Drawing.Size(442, 345)
            Me.Controls.Add(Me.pgrProperty)
            Me.Name = "frmExtendedForm"
            Me.Text = "Testing Tools.Windows.Forms.ExtendedForm"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents pgrProperty As System.Windows.Forms.PropertyGrid

    End Class
End Namespace