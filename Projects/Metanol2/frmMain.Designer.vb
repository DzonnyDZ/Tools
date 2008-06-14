<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.splMain = New System.Windows.Forms.SplitContainer
        Me.splBrowser = New System.Windows.Forms.SplitContainer
        Me.lvwFolders = New System.Windows.Forms.ListView
        Me.imlFolders = New System.Windows.Forms.ImageList(Me.components)
        Me.lvwImages = New System.Windows.Forms.ListView
        Me.imlImages = New System.Windows.Forms.ImageList(Me.components)
        Me.bgwImages = New System.ComponentModel.BackgroundWorker
        Me.tscMain = New System.Windows.Forms.ToolStripContainer
        Me.stsStatus = New System.Windows.Forms.StatusStrip
        Me.msnMain = New System.Windows.Forms.MenuStrip
        Me.tpbLoading = New System.Windows.Forms.ToolStripProgressBar
        Me.tslFolder = New System.Windows.Forms.ToolStripStatusLabel
        Me.tmiFile = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiBrowse = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiGoTo = New System.Windows.Forms.ToolStripMenuItem
        Me.tssFileSep1 = New System.Windows.Forms.ToolStripSeparator
        Me.tmiExit = New System.Windows.Forms.ToolStripMenuItem
        Me.fbdGoTo = New System.Windows.Forms.FolderBrowserDialog
        Me.splMain.Panel1.SuspendLayout()
        Me.splMain.SuspendLayout()
        Me.splBrowser.Panel1.SuspendLayout()
        Me.splBrowser.Panel2.SuspendLayout()
        Me.splBrowser.SuspendLayout()
        Me.tscMain.BottomToolStripPanel.SuspendLayout()
        Me.tscMain.ContentPanel.SuspendLayout()
        Me.tscMain.TopToolStripPanel.SuspendLayout()
        Me.tscMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'splMain
        '
        Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splMain.Location = New System.Drawing.Point(0, 0)
        Me.splMain.Name = "splMain"
        '
        'splMain.Panel1
        '
        Me.splMain.Panel1.Controls.Add(Me.splBrowser)
        Me.splMain.Size = New System.Drawing.Size(495, 288)
        Me.splMain.SplitterDistance = 230
        Me.splMain.TabIndex = 0
        '
        'splBrowser
        '
        Me.splBrowser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splBrowser.Location = New System.Drawing.Point(0, 0)
        Me.splBrowser.Name = "splBrowser"
        Me.splBrowser.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splBrowser.Panel1
        '
        Me.splBrowser.Panel1.Controls.Add(Me.lvwFolders)
        '
        'splBrowser.Panel2
        '
        Me.splBrowser.Panel2.Controls.Add(Me.lvwImages)
        Me.splBrowser.Size = New System.Drawing.Size(230, 288)
        Me.splBrowser.SplitterDistance = 98
        Me.splBrowser.TabIndex = 0
        '
        'lvwFolders
        '
        Me.lvwFolders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwFolders.Location = New System.Drawing.Point(0, 0)
        Me.lvwFolders.MultiSelect = False
        Me.lvwFolders.Name = "lvwFolders"
        Me.lvwFolders.Size = New System.Drawing.Size(230, 98)
        Me.lvwFolders.SmallImageList = Me.imlFolders
        Me.lvwFolders.TabIndex = 2
        Me.lvwFolders.UseCompatibleStateImageBehavior = False
        Me.lvwFolders.View = System.Windows.Forms.View.List
        '
        'imlFolders
        '
        Me.imlFolders.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.imlFolders.ImageSize = New System.Drawing.Size(16, 16)
        Me.imlFolders.TransparentColor = System.Drawing.Color.Transparent
        '
        'lvwImages
        '
        Me.lvwImages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwImages.HideSelection = False
        Me.lvwImages.LargeImageList = Me.imlImages
        Me.lvwImages.Location = New System.Drawing.Point(0, 0)
        Me.lvwImages.Name = "lvwImages"
        Me.lvwImages.Size = New System.Drawing.Size(230, 186)
        Me.lvwImages.TabIndex = 0
        Me.lvwImages.UseCompatibleStateImageBehavior = False
        '
        'imlImages
        '
        Me.imlImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.imlImages.ImageSize = New System.Drawing.Size(16, 16)
        Me.imlImages.TransparentColor = System.Drawing.Color.Transparent
        '
        'bgwImages
        '
        Me.bgwImages.WorkerReportsProgress = True
        Me.bgwImages.WorkerSupportsCancellation = True
        '
        'tscMain
        '
        '
        'tscMain.BottomToolStripPanel
        '
        Me.tscMain.BottomToolStripPanel.Controls.Add(Me.stsStatus)
        '
        'tscMain.ContentPanel
        '
        Me.tscMain.ContentPanel.Controls.Add(Me.splMain)
        Me.tscMain.ContentPanel.Size = New System.Drawing.Size(495, 288)
        Me.tscMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tscMain.Location = New System.Drawing.Point(0, 0)
        Me.tscMain.Name = "tscMain"
        Me.tscMain.Size = New System.Drawing.Size(495, 334)
        Me.tscMain.TabIndex = 3
        Me.tscMain.Text = "ToolStripContainer1"
        '
        'tscMain.TopToolStripPanel
        '
        Me.tscMain.TopToolStripPanel.Controls.Add(Me.msnMain)
        '
        'stsStatus
        '
        Me.stsStatus.Dock = System.Windows.Forms.DockStyle.None
        Me.stsStatus.Location = New System.Drawing.Point(0, 0)
        Me.stsStatus.Name = "stsStatus"
        Me.stsStatus.Size = New System.Drawing.Size(495, 22)
        Me.stsStatus.TabIndex = 1
        '
        'msnMain
        '
        Me.msnMain.Dock = System.Windows.Forms.DockStyle.None
        Me.msnMain.Location = New System.Drawing.Point(0, 0)
        Me.msnMain.Name = "msnMain"
        Me.msnMain.Size = New System.Drawing.Size(495, 24)
        Me.msnMain.TabIndex = 0
        Me.msnMain.Text = "MenuStrip1"
        '
        'tpbLoading
        '
        Me.tpbLoading.Name = "tpbLoading"
        Me.tpbLoading.Size = New System.Drawing.Size(100, 16)
        Me.tpbLoading.ToolTipText = "Loading images"
        Me.tpbLoading.Visible = False
        '
        'tslFolder
        '
        Me.tslFolder.Name = "tslFolder"
        Me.tslFolder.Size = New System.Drawing.Size(23, 17)
        Me.tslFolder.Text = "C:\"
        stsStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tpbLoading, Me.tslFolder})
        '
        'tmiFile
        '
        msnMain.Items.Add(tmiFile)
        Me.tmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiBrowse, Me.tmiGoTo, Me.tssFileSep1, Me.tmiExit})
        Me.tmiFile.Name = "tmiFile"
        Me.tmiFile.Size = New System.Drawing.Size(37, 20)
        Me.tmiFile.Text = "&File"
        '
        'tmiBrowse
        '
        Me.tmiBrowse.Name = "tmiBrowse"
        Me.tmiBrowse.Size = New System.Drawing.Size(176, 22)
        Me.tmiBrowse.Text = "&Browse for folder ..."
        '
        'tmiGoTo
        '
        Me.tmiGoTo.Name = "tmiGoTo"
        Me.tmiGoTo.Size = New System.Drawing.Size(176, 22)
        Me.tmiGoTo.Text = "&Go to folder ..."
        '
        'tssFileSep1
        '
        Me.tssFileSep1.Name = "tssFileSep1"
        Me.tssFileSep1.Size = New System.Drawing.Size(173, 6)
        '
        'tmiExit
        '
        Me.tmiExit.Name = "tmiExit"
        Me.tmiExit.Size = New System.Drawing.Size(176, 22)
        Me.tmiExit.Text = "&Exit"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(495, 334)
        Me.Controls.Add(Me.tscMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.msnMain
        Me.Name = "frmMain"
        Me.Text = "Metanol"
        Me.splMain.Panel1.ResumeLayout(False)
        Me.splMain.ResumeLayout(False)
        Me.splBrowser.Panel1.ResumeLayout(False)
        Me.splBrowser.Panel2.ResumeLayout(False)
        Me.splBrowser.ResumeLayout(False)
        Me.tscMain.BottomToolStripPanel.ResumeLayout(False)
        Me.tscMain.BottomToolStripPanel.PerformLayout()
        Me.tscMain.ContentPanel.ResumeLayout(False)
        Me.tscMain.TopToolStripPanel.ResumeLayout(False)
        Me.tscMain.TopToolStripPanel.PerformLayout()
        Me.tscMain.ResumeLayout(False)
        Me.tscMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents splMain As System.Windows.Forms.SplitContainer
    Friend WithEvents splBrowser As System.Windows.Forms.SplitContainer
    Friend WithEvents lvwImages As System.Windows.Forms.ListView
    Friend WithEvents lvwFolders As System.Windows.Forms.ListView
    Friend WithEvents imlImages As System.Windows.Forms.ImageList
    Friend WithEvents bgwImages As System.ComponentModel.BackgroundWorker
    Friend WithEvents tscMain As System.Windows.Forms.ToolStripContainer
    Friend WithEvents msnMain As System.Windows.Forms.MenuStrip
    Friend WithEvents stsStatus As System.Windows.Forms.StatusStrip
    Friend WithEvents tmiFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiBrowse As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tssFileSep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tmiExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents fbdGoTo As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents tslFolder As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tmiGoTo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tpbLoading As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents imlFolders As System.Windows.Forms.ImageList

End Class
