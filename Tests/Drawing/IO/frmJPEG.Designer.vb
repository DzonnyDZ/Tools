Namespace Drawing.IO
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmJPEG
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
            Me.cmdParse = New System.Windows.Forms.Button
            Me.ofdOpen = New System.Windows.Forms.OpenFileDialog
            Me.SuspendLayout()
            '
            'cmdParse
            '
            Me.cmdParse.AutoSize = True
            Me.cmdParse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdParse.Location = New System.Drawing.Point(3, 3)
            Me.cmdParse.Name = "cmdParse"
            Me.cmdParse.Size = New System.Drawing.Size(44, 23)
            Me.cmdParse.TabIndex = 0
            Me.cmdParse.Text = "Parse"
            Me.cmdParse.UseVisualStyleBackColor = True
            '
            'ofdOpen
            '
            Me.ofdOpen.Filter = "JPEG (*.jpg,*.jpeg,*.jfif)|*.jpg;*.jpeg;*.jfif"
            Me.ofdOpen.Title = "Parse JPEG"
            '
            'frmJPEG
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(543, 392)
            Me.Controls.Add(Me.cmdParse)
            Me.Name = "frmJPEG"
            Me.Text = "Testing Tools.IO.Drawing.JPEG"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents cmdParse As System.Windows.Forms.Button
        Friend WithEvents ofdOpen As System.Windows.Forms.OpenFileDialog
    End Class
End Namespace