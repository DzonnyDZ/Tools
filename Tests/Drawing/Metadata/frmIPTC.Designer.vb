Namespace DrawingT.MetadataT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmIPTC
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
            Me.ofdIPTC = New System.Windows.Forms.OpenFileDialog
            Me.prgProperties = New System.Windows.Forms.PropertyGrid
            Me.picImage = New System.Windows.Forms.PictureBox
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.cmdSave = New System.Windows.Forms.Button
            Me.tlpSave = New System.Windows.Forms.TableLayoutPanel
            CType(Me.picImage, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.tlpSave.SuspendLayout()
            Me.SuspendLayout()
            '
            'ofdIPTC
            '
            Me.ofdIPTC.Filter = "JPEG|*.jpg;*.jpeg"
            '
            'prgProperties
            '
            Me.prgProperties.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgProperties.Location = New System.Drawing.Point(0, 0)
            Me.prgProperties.Name = "prgProperties"
            Me.prgProperties.Size = New System.Drawing.Size(303, 495)
            Me.prgProperties.TabIndex = 0
            '
            'picImage
            '
            Me.picImage.Dock = System.Windows.Forms.DockStyle.Fill
            Me.picImage.Location = New System.Drawing.Point(0, 0)
            Me.picImage.Name = "picImage"
            Me.picImage.Size = New System.Drawing.Size(303, 156)
            Me.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.picImage.TabIndex = 1
            Me.picImage.TabStop = False
            '
            'splMain
            '
            Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
            Me.splMain.Location = New System.Drawing.Point(0, 0)
            Me.splMain.Name = "splMain"
            Me.splMain.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'splMain.Panel1
            '
            Me.splMain.Panel1.Controls.Add(Me.picImage)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.prgProperties)
            Me.splMain.Panel2.Controls.Add(Me.tlpSave)
            Me.splMain.Size = New System.Drawing.Size(303, 684)
            Me.splMain.SplitterDistance = 156
            Me.splMain.TabIndex = 2
            '
            'cmdSave
            '
            Me.cmdSave.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdSave.AutoSize = True
            Me.cmdSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdSave.Location = New System.Drawing.Point(130, 3)
            Me.cmdSave.Name = "cmdSave"
            Me.cmdSave.Size = New System.Drawing.Size(42, 23)
            Me.cmdSave.TabIndex = 1
            Me.cmdSave.Text = "&Save"
            Me.cmdSave.UseVisualStyleBackColor = True
            '
            'tlpSave
            '
            Me.tlpSave.AutoSize = True
            Me.tlpSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpSave.ColumnCount = 1
            Me.tlpSave.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpSave.Controls.Add(Me.cmdSave, 0, 0)
            Me.tlpSave.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.tlpSave.Location = New System.Drawing.Point(0, 495)
            Me.tlpSave.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpSave.Name = "tlpSave"
            Me.tlpSave.RowCount = 1
            Me.tlpSave.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpSave.Size = New System.Drawing.Size(303, 29)
            Me.tlpSave.TabIndex = 2
            '
            'frmIPTC
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(303, 684)
            Me.Controls.Add(Me.splMain)
            Me.Name = "frmIPTC"
            Me.Text = "Testing Tools.DrawingT.MetadataT.IPTC"
            CType(Me.picImage, System.ComponentModel.ISupportInitialize).EndInit()
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.Panel2.PerformLayout()
            Me.splMain.ResumeLayout(False)
            Me.tlpSave.ResumeLayout(False)
            Me.tlpSave.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ofdIPTC As System.Windows.Forms.OpenFileDialog
        Friend WithEvents prgProperties As System.Windows.Forms.PropertyGrid
        Friend WithEvents picImage As System.Windows.Forms.PictureBox
        Friend WithEvents splMain As System.Windows.Forms.SplitContainer
        Friend WithEvents tlpSave As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents cmdSave As System.Windows.Forms.Button
    End Class
End Namespace