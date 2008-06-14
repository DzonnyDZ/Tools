Namespace IOt
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmShellLink
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
            Me.prgGrid = New System.Windows.Forms.PropertyGrid
            Me.flpCommands = New System.Windows.Forms.FlowLayoutPanel
            Me.cmdCreate = New System.Windows.Forms.Button
            Me.cmdOpen = New System.Windows.Forms.Button
            Me.tlpSave = New System.Windows.Forms.TableLayoutPanel
            Me.cmdSave = New System.Windows.Forms.Button
            Me.ofdOpenLink = New System.Windows.Forms.OpenFileDialog
            Me.ofdSelectFile = New System.Windows.Forms.OpenFileDialog
            Me.sfdSaveLink = New System.Windows.Forms.SaveFileDialog
            Me.flpCommands.SuspendLayout()
            Me.tlpSave.SuspendLayout()
            Me.SuspendLayout()
            '
            'prgGrid
            '
            Me.prgGrid.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgGrid.Location = New System.Drawing.Point(0, 29)
            Me.prgGrid.Name = "prgGrid"
            Me.prgGrid.Size = New System.Drawing.Size(370, 349)
            Me.prgGrid.TabIndex = 0
            '
            'flpCommands
            '
            Me.flpCommands.AutoSize = True
            Me.flpCommands.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpCommands.Controls.Add(Me.cmdCreate)
            Me.flpCommands.Controls.Add(Me.cmdOpen)
            Me.flpCommands.Dock = System.Windows.Forms.DockStyle.Top
            Me.flpCommands.Location = New System.Drawing.Point(0, 0)
            Me.flpCommands.Name = "flpCommands"
            Me.flpCommands.Size = New System.Drawing.Size(370, 29)
            Me.flpCommands.TabIndex = 1
            '
            'cmdCreate
            '
            Me.cmdCreate.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdCreate.AutoSize = True
            Me.cmdCreate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdCreate.Location = New System.Drawing.Point(3, 3)
            Me.cmdCreate.Name = "cmdCreate"
            Me.cmdCreate.Size = New System.Drawing.Size(48, 23)
            Me.cmdCreate.TabIndex = 0
            Me.cmdCreate.Text = "&Create"
            Me.cmdCreate.UseVisualStyleBackColor = True
            '
            'cmdOpen
            '
            Me.cmdOpen.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdOpen.AutoSize = True
            Me.cmdOpen.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdOpen.Location = New System.Drawing.Point(57, 3)
            Me.cmdOpen.Name = "cmdOpen"
            Me.cmdOpen.Size = New System.Drawing.Size(43, 23)
            Me.cmdOpen.TabIndex = 1
            Me.cmdOpen.Text = "&Open"
            Me.cmdOpen.UseVisualStyleBackColor = True
            '
            'tlpSave
            '
            Me.tlpSave.AutoSize = True
            Me.tlpSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpSave.ColumnCount = 1
            Me.tlpSave.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpSave.Controls.Add(Me.cmdSave, 0, 0)
            Me.tlpSave.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.tlpSave.Location = New System.Drawing.Point(0, 378)
            Me.tlpSave.Name = "tlpSave"
            Me.tlpSave.RowCount = 1
            Me.tlpSave.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpSave.Size = New System.Drawing.Size(370, 29)
            Me.tlpSave.TabIndex = 2
            '
            'cmdSave
            '
            Me.cmdSave.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdSave.AutoSize = True
            Me.cmdSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdSave.Enabled = False
            Me.cmdSave.Location = New System.Drawing.Point(164, 3)
            Me.cmdSave.Name = "cmdSave"
            Me.cmdSave.Size = New System.Drawing.Size(42, 23)
            Me.cmdSave.TabIndex = 0
            Me.cmdSave.Text = "&Save"
            Me.cmdSave.UseVisualStyleBackColor = True
            '
            'ofdOpenLink
            '
            Me.ofdOpenLink.DefaultExt = "lnk"
            Me.ofdOpenLink.DereferenceLinks = False
            Me.ofdOpenLink.Filter = "Shotrcuts (*.lnk)|*.lnk|All files (*.*)|*.*"
            Me.ofdOpenLink.Title = "Open link"
            '
            'ofdSelectFile
            '
            Me.ofdSelectFile.Filter = "All files (*.*)|*.*"
            Me.ofdSelectFile.Title = "Select link target"
            '
            'sfdSaveLink
            '
            Me.sfdSaveLink.DefaultExt = "lnk"
            Me.sfdSaveLink.DereferenceLinks = False
            Me.sfdSaveLink.Filter = "Shortcuts (*.lnk)|*.lnk|All files (*.*)|*.*"
            Me.sfdSaveLink.Title = "Save link"
            '
            'frmShellLink
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(370, 407)
            Me.Controls.Add(Me.prgGrid)
            Me.Controls.Add(Me.tlpSave)
            Me.Controls.Add(Me.flpCommands)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Name = "frmShellLink"
            Me.Text = "Testing Tools.IOt.ShellLink"
            Me.flpCommands.ResumeLayout(False)
            Me.flpCommands.PerformLayout()
            Me.tlpSave.ResumeLayout(False)
            Me.tlpSave.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents prgGrid As System.Windows.Forms.PropertyGrid
        Friend WithEvents flpCommands As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents cmdCreate As System.Windows.Forms.Button
        Friend WithEvents cmdOpen As System.Windows.Forms.Button
        Friend WithEvents tlpSave As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents cmdSave As System.Windows.Forms.Button
        Friend WithEvents ofdOpenLink As System.Windows.Forms.OpenFileDialog
        Friend WithEvents ofdSelectFile As System.Windows.Forms.OpenFileDialog
        Friend WithEvents sfdSaveLink As System.Windows.Forms.SaveFileDialog
    End Class
End Namespace