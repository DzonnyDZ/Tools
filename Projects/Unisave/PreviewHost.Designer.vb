<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PreviewHost
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.ehWpfHost = New System.Windows.Forms.Integration.ElementHost()
        Me.sscSaverScreen = New Tools.Unisave.SaverScreen()
        Me.SuspendLayout()
        '
        'ehWpfHost
        '
        Me.ehWpfHost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ehWpfHost.Location = New System.Drawing.Point(0, 0)
        Me.ehWpfHost.Name = "ehWpfHost"
        Me.ehWpfHost.Size = New System.Drawing.Size(150, 150)
        Me.ehWpfHost.TabIndex = 0
        Me.ehWpfHost.Child = Me.sscSaverScreen
        '
        'PreviewHost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ehWpfHost)
        Me.Name = "PreviewHost"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ehWpfHost As System.Windows.Forms.Integration.ElementHost
    Friend sscSaverScreen As Tools.Unisave.SaverScreen

End Class
