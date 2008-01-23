<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMonitors
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
        Me.panScreen = New System.Windows.Forms.Panel
        Me.SuspendLayout()
        '
        'panScreen
        '
        Me.panScreen.BackColor = System.Drawing.Color.Black
        Me.panScreen.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.panScreen.ForeColor = System.Drawing.Color.Yellow
        Me.panScreen.Location = New System.Drawing.Point(0, 0)
        Me.panScreen.Name = "panScreen"
        Me.panScreen.Size = New System.Drawing.Size(200, 100)
        Me.panScreen.TabIndex = 0
        '
        'frmMonitors
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.panScreen)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "frmMonitors"
        Me.Text = "Monitors layout"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panScreen As System.Windows.Forms.Panel
End Class
