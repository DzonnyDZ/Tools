Namespace DrawingT.DrawingIOt
    '#If Config <= Nightly Then Stage conditional compilation of this file is set in Tests.vbproj
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
            Me.components = New System.ComponentModel.Container
            Me.cmdParse = New System.Windows.Forms.Button
            Me.ofdOpen = New System.Windows.Forms.OpenFileDialog
            Me.tvwResults = New System.Windows.Forms.TreeView
            Me.cmsContext = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.tmiExport = New System.Windows.Forms.ToolStripMenuItem
            Me.lblI = New System.Windows.Forms.Label
            Me.tlpI = New System.Windows.Forms.TableLayoutPanel
            Me.flpTop = New System.Windows.Forms.FlowLayoutPanel
            Me.nudSize = New System.Windows.Forms.NumericUpDown
            Me.chkFujiFilmFinePix2800Zoom = New System.Windows.Forms.CheckBox
            Me.sfdSave = New System.Windows.Forms.SaveFileDialog
            Me.splSplit = New System.Windows.Forms.SplitContainer
            Me.prgProperty = New System.Windows.Forms.PropertyGrid
            Me.lblToString = New System.Windows.Forms.Label
            Me.lblType = New System.Windows.Forms.Label
            Me.totToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.cmsContext.SuspendLayout()
            Me.tlpI.SuspendLayout()
            Me.flpTop.SuspendLayout()
            CType(Me.nudSize, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.splSplit.Panel1.SuspendLayout()
            Me.splSplit.Panel2.SuspendLayout()
            Me.splSplit.SuspendLayout()
            Me.SuspendLayout()
            '
            'cmdParse
            '
            Me.cmdParse.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmdParse.AutoSize = True
            Me.cmdParse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdParse.Location = New System.Drawing.Point(0, 0)
            Me.cmdParse.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdParse.Name = "cmdParse"
            Me.cmdParse.Size = New System.Drawing.Size(53, 23)
            Me.cmdParse.TabIndex = 0
            Me.cmdParse.Text = "&Parse..."
            Me.cmdParse.UseVisualStyleBackColor = True
            '
            'ofdOpen
            '
            Me.ofdOpen.Filter = "JPEG (*.jpg,*.jpeg,*.jfif)|*.jpg;*.jpeg;*.jfif"
            Me.ofdOpen.Title = "Parse JPEG"
            '
            'tvwResults
            '
            Me.tvwResults.ContextMenuStrip = Me.cmsContext
            Me.tvwResults.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tvwResults.Location = New System.Drawing.Point(0, 0)
            Me.tvwResults.Name = "tvwResults"
            Me.tvwResults.Size = New System.Drawing.Size(278, 366)
            Me.tvwResults.TabIndex = 1
            '
            'cmsContext
            '
            Me.cmsContext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiExport})
            Me.cmsContext.Name = "cmsContext"
            Me.cmsContext.Size = New System.Drawing.Size(152, 26)
            '
            'tmiExport
            '
            Me.tmiExport.Name = "tmiExport"
            Me.tmiExport.Size = New System.Drawing.Size(151, 22)
            Me.tmiExport.Text = "Export to file ..."
            '
            'lblI
            '
            Me.lblI.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.lblI.AutoSize = True
            Me.lblI.Location = New System.Drawing.Point(22, 0)
            Me.lblI.Name = "lblI"
            Me.lblI.Size = New System.Drawing.Size(503, 26)
            Me.lblI.TabIndex = 2
            Me.lblI.Text = "This from tests Tools.DrawingT.IOt.Jpeg, Tools.IOt.BinaryReader, Tools.IOt.Constr" & _
                "ainedReadOnlyStream, Tools.MathT.LEBE, Tools.DrawingT.MetadataT.ExifT, Toolos.Dr" & _
                "awingT.MetadataT.IptcT"
            Me.lblI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'tlpI
            '
            Me.tlpI.AutoSize = True
            Me.tlpI.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpI.ColumnCount = 1
            Me.tlpI.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpI.Controls.Add(Me.lblI, 0, 0)
            Me.tlpI.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.tlpI.Location = New System.Drawing.Point(0, 392)
            Me.tlpI.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpI.Name = "tlpI"
            Me.tlpI.RowCount = 2
            Me.tlpI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
            Me.tlpI.Size = New System.Drawing.Size(547, 46)
            Me.tlpI.TabIndex = 3
            '
            'flpTop
            '
            Me.flpTop.AutoSize = True
            Me.flpTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpTop.Controls.Add(Me.cmdParse)
            Me.flpTop.Controls.Add(Me.nudSize)
            Me.flpTop.Controls.Add(Me.chkFujiFilmFinePix2800Zoom)
            Me.flpTop.Dock = System.Windows.Forms.DockStyle.Top
            Me.flpTop.Location = New System.Drawing.Point(0, 0)
            Me.flpTop.Margin = New System.Windows.Forms.Padding(0)
            Me.flpTop.Name = "flpTop"
            Me.flpTop.Size = New System.Drawing.Size(547, 26)
            Me.flpTop.TabIndex = 3
            '
            'nudSize
            '
            Me.nudSize.Location = New System.Drawing.Point(56, 3)
            Me.nudSize.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
            Me.nudSize.Name = "nudSize"
            Me.nudSize.Size = New System.Drawing.Size(58, 20)
            Me.nudSize.TabIndex = 1
            Me.nudSize.Value = New Decimal(New Integer() {8, 0, 0, 0})
            '
            'chkFujiFilmFinePix2800Zoom
            '
            Me.chkFujiFilmFinePix2800Zoom.AutoSize = True
            Me.chkFujiFilmFinePix2800Zoom.Location = New System.Drawing.Point(120, 3)
            Me.chkFujiFilmFinePix2800Zoom.Name = "chkFujiFilmFinePix2800Zoom"
            Me.chkFujiFilmFinePix2800Zoom.Size = New System.Drawing.Size(152, 17)
            Me.chkFujiFilmFinePix2800Zoom.TabIndex = 2
            Me.chkFujiFilmFinePix2800Zoom.Text = "FujiFilm FinePix 2800 zoom"
            Me.totToolTip.SetToolTip(Me.chkFujiFilmFinePix2800Zoom, "Support Exif where  with additional 4 bytes at start of APP1 marker (FFE1 and siz" & _
                    "e-4)")
            Me.chkFujiFilmFinePix2800Zoom.UseVisualStyleBackColor = True
            '
            'splSplit
            '
            Me.splSplit.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splSplit.Location = New System.Drawing.Point(0, 26)
            Me.splSplit.Name = "splSplit"
            '
            'splSplit.Panel1
            '
            Me.splSplit.Panel1.Controls.Add(Me.tvwResults)
            '
            'splSplit.Panel2
            '
            Me.splSplit.Panel2.Controls.Add(Me.prgProperty)
            Me.splSplit.Panel2.Controls.Add(Me.lblToString)
            Me.splSplit.Panel2.Controls.Add(Me.lblType)
            Me.splSplit.Size = New System.Drawing.Size(547, 366)
            Me.splSplit.SplitterDistance = 278
            Me.splSplit.TabIndex = 4
            '
            'prgProperty
            '
            Me.prgProperty.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgProperty.Location = New System.Drawing.Point(0, 26)
            Me.prgProperty.Name = "prgProperty"
            Me.prgProperty.Size = New System.Drawing.Size(265, 340)
            Me.prgProperty.TabIndex = 0
            '
            'lblToString
            '
            Me.lblToString.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblToString.Location = New System.Drawing.Point(0, 13)
            Me.lblToString.Name = "lblToString"
            Me.lblToString.Size = New System.Drawing.Size(265, 13)
            Me.lblToString.TabIndex = 2
            Me.lblToString.TextAlign = System.Drawing.ContentAlignment.TopCenter
            '
            'lblType
            '
            Me.lblType.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.lblType.Location = New System.Drawing.Point(0, 0)
            Me.lblType.Name = "lblType"
            Me.lblType.Size = New System.Drawing.Size(265, 13)
            Me.lblType.TabIndex = 1
            Me.lblType.TextAlign = System.Drawing.ContentAlignment.TopCenter
            '
            'frmJPEG
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(547, 438)
            Me.Controls.Add(Me.splSplit)
            Me.Controls.Add(Me.flpTop)
            Me.Controls.Add(Me.tlpI)
            Me.Name = "frmJPEG"
            Me.Text = "Testing Tools.IO.Drawing.JPEG"
            Me.cmsContext.ResumeLayout(False)
            Me.tlpI.ResumeLayout(False)
            Me.tlpI.PerformLayout()
            Me.flpTop.ResumeLayout(False)
            Me.flpTop.PerformLayout()
            CType(Me.nudSize, System.ComponentModel.ISupportInitialize).EndInit()
            Me.splSplit.Panel1.ResumeLayout(False)
            Me.splSplit.Panel2.ResumeLayout(False)
            Me.splSplit.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents cmdParse As System.Windows.Forms.Button
        Friend WithEvents ofdOpen As System.Windows.Forms.OpenFileDialog
        Friend WithEvents tvwResults As System.Windows.Forms.TreeView
        Friend WithEvents lblI As System.Windows.Forms.Label
        Friend WithEvents tlpI As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents flpTop As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents nudSize As System.Windows.Forms.NumericUpDown
        Friend WithEvents cmsContext As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents tmiExport As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents sfdSave As System.Windows.Forms.SaveFileDialog
        Friend WithEvents splSplit As System.Windows.Forms.SplitContainer
        Friend WithEvents prgProperty As System.Windows.Forms.PropertyGrid
        Friend WithEvents lblToString As System.Windows.Forms.Label
        Friend WithEvents lblType As System.Windows.Forms.Label
        Friend WithEvents chkFujiFilmFinePix2800Zoom As System.Windows.Forms.CheckBox
        Friend WithEvents totToolTip As System.Windows.Forms.ToolTip
    End Class
End Namespace