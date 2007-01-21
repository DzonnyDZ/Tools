<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EncodingSelector
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
        Me.EncodingSelector1 = New Tools.Windows.Forms.EncodingSelector
        Me.SuspendLayout()
        '
        'EncodingSelector1
        '
        Me.EncodingSelector1.Location = New System.Drawing.Point(55, 52)
        Me.EncodingSelector1.Name = "EncodingSelector1"
        Me.EncodingSelector1.Size = New System.Drawing.Size(199, 146)
        Me.EncodingSelector1.TabIndex = 0
        '
        'EncodingSelector
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.EncodingSelector1)
        Me.Name = "EncodingSelector"
        Me.Text = "EncodingSelector"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents EncodingSelector1 As Tools.Windows.Forms.EncodingSelector
End Class
