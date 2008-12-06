Namespace MetadataT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmExif
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExif))
            Me.tscMain = New System.Windows.Forms.ToolStripContainer
            Me.mnsMain = New System.Windows.Forms.MenuStrip
            Me.tmiFile = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiOpen = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiSave = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiSaveAs = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiExit = New System.Windows.Forms.ToolStripMenuItem
            Me.ofdOpen = New System.Windows.Forms.OpenFileDialog
            Me.sfdSave = New System.Windows.Forms.SaveFileDialog
            Me.stsStatus = New System.Windows.Forms.StatusStrip
            Me.tslFileName = New System.Windows.Forms.ToolStripStatusLabel
            Me.tslChanged = New System.Windows.Forms.ToolStripStatusLabel
            Me.tscMain.TopToolStripPanel.SuspendLayout()
            Me.tscMain.SuspendLayout()
            Me.mnsMain.SuspendLayout()
            Me.stsStatus.SuspendLayout()
            Me.SuspendLayout()
            '
            'tscMain
            '
            '
            'tscMain.ContentPanel
            '
            Me.tscMain.ContentPanel.Size = New System.Drawing.Size(522, 317)
            Me.tscMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tscMain.Location = New System.Drawing.Point(0, 0)
            Me.tscMain.Name = "tscMain"
            Me.tscMain.Size = New System.Drawing.Size(522, 341)
            Me.tscMain.TabIndex = 0
            Me.tscMain.Text = "ToolStripContainer1"
            '
            'tscMain.TopToolStripPanel
            '
            Me.tscMain.TopToolStripPanel.Controls.Add(Me.mnsMain)
            '
            'mnsMain
            '
            Me.mnsMain.Dock = System.Windows.Forms.DockStyle.None
            Me.mnsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiFile})
            Me.mnsMain.Location = New System.Drawing.Point(0, 0)
            Me.mnsMain.Name = "mnsMain"
            Me.mnsMain.Size = New System.Drawing.Size(522, 24)
            Me.mnsMain.TabIndex = 0
            Me.mnsMain.Text = "MenuStrip1"
            '
            'tmiFile
            '
            Me.tmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiOpen, Me.tmiSave, Me.tmiSaveAs, Me.tmiExit})
            Me.tmiFile.Name = "tmiFile"
            Me.tmiFile.Size = New System.Drawing.Size(37, 20)
            Me.tmiFile.Text = "&File"
            '
            'tmiOpen
            '
            Me.tmiOpen.Image = Global.Tools.Tests.My.Resources.Resources.openHS
            Me.tmiOpen.Name = "tmiOpen"
            Me.tmiOpen.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
            Me.tmiOpen.Size = New System.Drawing.Size(158, 22)
            Me.tmiOpen.Text = "&Open ..."
            '
            'tmiSave
            '
            Me.tmiSave.Image = Global.Tools.Tests.My.Resources.Resources.saveHS
            Me.tmiSave.Name = "tmiSave"
            Me.tmiSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
            Me.tmiSave.Size = New System.Drawing.Size(158, 22)
            Me.tmiSave.Text = "&Save"
            '
            'tmiSaveAs
            '
            Me.tmiSaveAs.Name = "tmiSaveAs"
            Me.tmiSaveAs.Size = New System.Drawing.Size(158, 22)
            Me.tmiSaveAs.Text = "&Save as ..."
            '
            'tmiExit
            '
            Me.tmiExit.Name = "tmiExit"
            Me.tmiExit.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
            Me.tmiExit.Size = New System.Drawing.Size(158, 22)
            Me.tmiExit.Text = "&Exit"
            '
            'ofdOpen
            '
            Me.ofdOpen.DefaultExt = "jpg"
            Me.ofdOpen.FileName = "ofdOpen"
            Me.ofdOpen.Filter = "Jpeg files (*.jpg, *.jpeg, *.jfif)|*.jpg;*.jpeg;*.jfif|Exif files (*.exif)|*.exif" & _
                "|Binary files (*.bin)|*.bin"
            Me.ofdOpen.ShowReadOnly = True
            Me.ofdOpen.Title = "Open file"
            '
            'sfdSave
            '
            Me.sfdSave.DefaultExt = "exif"
            Me.sfdSave.Filter = resources.GetString("sfdSave.Filter")
            Me.sfdSave.Title = "Export Exif data"
            '
            'stsStatus
            '
            Me.stsStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslFileName, Me.tslChanged})
            Me.stsStatus.Location = New System.Drawing.Point(0, 319)
            Me.stsStatus.Name = "stsStatus"
            Me.stsStatus.Size = New System.Drawing.Size(522, 22)
            Me.stsStatus.TabIndex = 1
            Me.stsStatus.Text = "StatusStrip1"
            '
            'tslFileName
            '
            Me.tslFileName.Name = "tslFileName"
            Me.tslFileName.Size = New System.Drawing.Size(0, 17)
            '
            'tslChanged
            '
            Me.tslChanged.Name = "tslChanged"
            Me.tslChanged.Size = New System.Drawing.Size(0, 17)
            '
            'frmExif
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(522, 341)
            Me.Controls.Add(Me.stsStatus)
            Me.Controls.Add(Me.tscMain)
            Me.MainMenuStrip = Me.mnsMain
            Me.Name = "frmExif"
            Me.Text = "Testing Tools.MetadataT.ExifT"
            Me.tscMain.TopToolStripPanel.ResumeLayout(False)
            Me.tscMain.TopToolStripPanel.PerformLayout()
            Me.tscMain.ResumeLayout(False)
            Me.tscMain.PerformLayout()
            Me.mnsMain.ResumeLayout(False)
            Me.mnsMain.PerformLayout()
            Me.stsStatus.ResumeLayout(False)
            Me.stsStatus.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents tscMain As System.Windows.Forms.ToolStripContainer
        Friend WithEvents mnsMain As System.Windows.Forms.MenuStrip
        Friend WithEvents tmiFile As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiOpen As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiSave As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiSaveAs As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiExit As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ofdOpen As System.Windows.Forms.OpenFileDialog
        Friend WithEvents sfdSave As System.Windows.Forms.SaveFileDialog
        Friend WithEvents stsStatus As System.Windows.Forms.StatusStrip
        Friend WithEvents tslFileName As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents tslChanged As System.Windows.Forms.ToolStripStatusLabel
    End Class
End Namespace