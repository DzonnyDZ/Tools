<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSSaver
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSSaver))
        Me.tmrImg = New System.Windows.Forms.Timer(Me.components)
        Me.picMain = New System.Windows.Forms.PictureBox
        Me.tlbInfo = New Tools.WindowsT.FormsT.TransparentLabel
        CType(Me.picMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tmrImg
        '
        '
        'picMain
        '
        Me.picMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picMain.Location = New System.Drawing.Point(0, 0)
        Me.picMain.Name = "picMain"
        Me.picMain.Size = New System.Drawing.Size(284, 264)
        Me.picMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picMain.TabIndex = 0
        Me.picMain.TabStop = False
        '
        'tlbInfo
        '
        Me.tlbInfo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.tlbInfo.Location = New System.Drawing.Point(0, 0)
        Me.tlbInfo.Name = "tlbInfo"
        Me.tlbInfo.Size = New System.Drawing.Size(25, 13)
        Me.tlbInfo.TabIndex = 1
        Me.tlbInfo.Text = "Info"
        '
        'frmSSaver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(284, 264)
        Me.ControlBox = False
        Me.Controls.Add(Me.tlbInfo)
        Me.Controls.Add(Me.picMain)
        Me.ForeColor = System.Drawing.Color.Lime
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSSaver"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Metanol Screen Saver"
        CType(Me.picMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tmrImg As System.Windows.Forms.Timer
    Friend WithEvents picMain As System.Windows.Forms.PictureBox
    Friend WithEvents tlbInfo As Tools.WindowsT.FormsT.TransparentLabel
End Class
