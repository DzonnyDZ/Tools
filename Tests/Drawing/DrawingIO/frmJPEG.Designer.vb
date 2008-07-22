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
            Me.lblI = New System.Windows.Forms.Label
            Me.tlpI = New System.Windows.Forms.TableLayoutPanel
            Me.flpTop = New System.Windows.Forms.FlowLayoutPanel
            Me.nudSize = New System.Windows.Forms.NumericUpDown
            Me.cmsContext = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.sfdSave = New System.Windows.Forms.SaveFileDialog
            Me.tmiExport = New System.Windows.Forms.ToolStripMenuItem
            Me.tlpI.SuspendLayout()
            Me.flpTop.SuspendLayout()
            CType(Me.nudSize, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.cmsContext.SuspendLayout()
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
            Me.tvwResults.Location = New System.Drawing.Point(0, 26)
            Me.tvwResults.Name = "tvwResults"
            Me.tvwResults.Size = New System.Drawing.Size(543, 320)
            Me.tvwResults.TabIndex = 1
            '
            'lblI
            '
            Me.lblI.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.lblI.AutoSize = True
            Me.lblI.Location = New System.Drawing.Point(26, 0)
            Me.lblI.Name = "lblI"
            Me.lblI.Size = New System.Drawing.Size(491, 26)
            Me.lblI.TabIndex = 2
            Me.lblI.Text = "This from tests Tools.Drawing.IO.JPEG, Tools.IO.BinaryReader, Tools.IO.Constraine" & _
                "dReadOnlyStream, Tools.Math.LEBE"
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
            Me.tlpI.Location = New System.Drawing.Point(0, 346)
            Me.tlpI.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpI.Name = "tlpI"
            Me.tlpI.RowCount = 2
            Me.tlpI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
            Me.tlpI.Size = New System.Drawing.Size(543, 46)
            Me.tlpI.TabIndex = 3
            '
            'flpTop
            '
            Me.flpTop.AutoSize = True
            Me.flpTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpTop.Controls.Add(Me.cmdParse)
            Me.flpTop.Controls.Add(Me.nudSize)
            Me.flpTop.Dock = System.Windows.Forms.DockStyle.Top
            Me.flpTop.Location = New System.Drawing.Point(0, 0)
            Me.flpTop.Margin = New System.Windows.Forms.Padding(0)
            Me.flpTop.Name = "flpTop"
            Me.flpTop.Size = New System.Drawing.Size(543, 26)
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
            'cmsContext
            '
            Me.cmsContext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiExport})
            Me.cmsContext.Name = "cmsContext"
            Me.cmsContext.Size = New System.Drawing.Size(153, 48)
            '
            'tmiExport
            '
            Me.tmiExport.Name = "tmiExport"
            Me.tmiExport.Size = New System.Drawing.Size(152, 22)
            Me.tmiExport.Text = "Export to file ..."
            '
            'frmJPEG
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(543, 392)
            Me.Controls.Add(Me.tvwResults)
            Me.Controls.Add(Me.flpTop)
            Me.Controls.Add(Me.tlpI)
            Me.Name = "frmJPEG"
            Me.Text = "Testing Tools.IO.Drawing.JPEG"
            Me.tlpI.ResumeLayout(False)
            Me.tlpI.PerformLayout()
            Me.flpTop.ResumeLayout(False)
            Me.flpTop.PerformLayout()
            CType(Me.nudSize, System.ComponentModel.ISupportInitialize).EndInit()
            Me.cmsContext.ResumeLayout(False)
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
    End Class
End Namespace