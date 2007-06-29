#If Config <= RC Then
Namespace DiagnosticsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmImageVisualizer
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
            Me.picImg = New System.Windows.Forms.PictureBox
            Me.cmdBw = New System.Windows.Forms.Button
            Me.ofdOpen = New System.Windows.Forms.OpenFileDialog
            CType(Me.picImg, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'picImg
            '
            Me.picImg.Dock = System.Windows.Forms.DockStyle.Fill
            Me.picImg.Location = New System.Drawing.Point(0, 0)
            Me.picImg.Name = "picImg"
            Me.picImg.Size = New System.Drawing.Size(530, 283)
            Me.picImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.picImg.TabIndex = 0
            Me.picImg.TabStop = False
            '
            'cmdBw
            '
            Me.cmdBw.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.cmdBw.Location = New System.Drawing.Point(0, 260)
            Me.cmdBw.Name = "cmdBw"
            Me.cmdBw.Size = New System.Drawing.Size(530, 23)
            Me.cmdBw.TabIndex = 1
            Me.cmdBw.Text = "&..."
            Me.cmdBw.UseVisualStyleBackColor = True
            '
            'ofdOpen
            '
            Me.ofdOpen.DefaultExt = "jpg"
            Me.ofdOpen.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp;*.wmf;*.emf;*.dib;*.png;*.gif;*.tif;*.tiff"
            Me.ofdOpen.Title = "Open"
            '
            'frmImageVisualizer
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(530, 283)
            Me.Controls.Add(Me.cmdBw)
            Me.Controls.Add(Me.picImg)
            Me.Name = "frmImageVisualizer"
            Me.Text = "Testing Tools.DiagnosticsT.ImageVisualizer"
            CType(Me.picImg, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents picImg As System.Windows.Forms.PictureBox
        Friend WithEvents cmdBw As System.Windows.Forms.Button
        Friend WithEvents ofdOpen As System.Windows.Forms.OpenFileDialog
    End Class
End Namespace
#End If