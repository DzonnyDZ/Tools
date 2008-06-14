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
        Me.tpbLoading = New System.Windows.Forms.ToolStripProgressBar
        Me.tslFolder = New System.Windows.Forms.ToolStripStatusLabel
        Me.msnMain = New System.Windows.Forms.MenuStrip
        Me.tmiFile = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiBrowse = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiGoTo = New System.Windows.Forms.ToolStripMenuItem
        Me.tssFileSep1 = New System.Windows.Forms.ToolStripSeparator
        Me.tmiExit = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiHelp = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiAbout = New System.Windows.Forms.ToolStripMenuItem
        Me.tosMain = New System.Windows.Forms.ToolStrip
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.tsbForward = New System.Windows.Forms.ToolStripButton
        Me.tsbRefresh = New System.Windows.Forms.ToolStripButton
        Me.fbdGoTo = New System.Windows.Forms.FolderBrowserDialog
        Me.tmiTools = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiOptions = New System.Windows.Forms.ToolStripMenuItem
        Me.splMain.Panel1.SuspendLayout()
        Me.splMain.SuspendLayout()
        Me.splBrowser.Panel1.SuspendLayout()
        Me.splBrowser.Panel2.SuspendLayout()
        Me.splBrowser.SuspendLayout()
        Me.tscMain.BottomToolStripPanel.SuspendLayout()
        Me.tscMain.ContentPanel.SuspendLayout()
        Me.tscMain.TopToolStripPanel.SuspendLayout()
        Me.tscMain.SuspendLayout()
        Me.stsStatus.SuspendLayout()
        Me.msnMain.SuspendLayout()
        Me.tosMain.SuspendLayout()
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
        Me.splMain.Size = New System.Drawing.Size(495, 263)
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
        Me.splBrowser.Size = New System.Drawing.Size(230, 263)
        Me.splBrowser.SplitterDistance = 89
        Me.splBrowser.TabIndex = 0
        '
        'lvwFolders
        '
        Me.lvwFolders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwFolders.Location = New System.Drawing.Point(0, 0)
        Me.lvwFolders.MultiSelect = False
        Me.lvwFolders.Name = "lvwFolders"
        Me.lvwFolders.Size = New System.Drawing.Size(230, 89)
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
        Me.lvwImages.Size = New System.Drawing.Size(230, 170)
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
        Me.tscMain.ContentPanel.Size = New System.Drawing.Size(495, 263)
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
        Me.tscMain.TopToolStripPanel.Controls.Add(Me.tosMain)
        '
        'stsStatus
        '
        Me.stsStatus.Dock = System.Windows.Forms.DockStyle.None
        Me.stsStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tpbLoading, Me.tslFolder})
        Me.stsStatus.Location = New System.Drawing.Point(0, 0)
        Me.stsStatus.Name = "stsStatus"
        Me.stsStatus.ShowItemToolTips = True
        Me.stsStatus.Size = New System.Drawing.Size(495, 22)
        Me.stsStatus.TabIndex = 1
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
        '
        'msnMain
        '
        Me.msnMain.Dock = System.Windows.Forms.DockStyle.None
        Me.msnMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiFile, Me.tmiTools, Me.tmiHelp})
        Me.msnMain.Location = New System.Drawing.Point(0, 0)
        Me.msnMain.Name = "msnMain"
        Me.msnMain.Size = New System.Drawing.Size(495, 24)
        Me.msnMain.TabIndex = 0
        Me.msnMain.Text = "MenuStrip1"
        '
        'tmiFile
        '
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
        'tmiHelp
        '
        Me.tmiHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiAbout})
        Me.tmiHelp.Name = "tmiHelp"
        Me.tmiHelp.Size = New System.Drawing.Size(44, 20)
        Me.tmiHelp.Text = "&Help"
        '
        'tmiAbout
        '
        Me.tmiAbout.Name = "tmiAbout"
        Me.tmiAbout.Size = New System.Drawing.Size(152, 22)
        Me.tmiAbout.Text = "&About ..."
        '
        'tosMain
        '
        Me.tosMain.Dock = System.Windows.Forms.DockStyle.None
        Me.tosMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbBack, Me.tsbForward, Me.tsbRefresh})
        Me.tosMain.Location = New System.Drawing.Point(3, 24)
        Me.tosMain.Name = "tosMain"
        Me.tosMain.Size = New System.Drawing.Size(81, 25)
        Me.tosMain.TabIndex = 1
        '
        'tsbBack
        '
        Me.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbBack.Enabled = False
        Me.tsbBack.Image = Global.Tools.Metanol.My.Resources.Resources.NavBack
        Me.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbBack.Name = "tsbBack"
        Me.tsbBack.Size = New System.Drawing.Size(23, 22)
        Me.tsbBack.Text = "Navigate backward"
        '
        'tsbForward
        '
        Me.tsbForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbForward.Enabled = False
        Me.tsbForward.Image = Global.Tools.Metanol.My.Resources.Resources.NavForward
        Me.tsbForward.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbForward.Name = "tsbForward"
        Me.tsbForward.Size = New System.Drawing.Size(23, 22)
        Me.tsbForward.Text = "Navigate forward"
        '
        'tsbRefresh
        '
        Me.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbRefresh.Image = Global.Tools.Metanol.My.Resources.Resources.Refresh
        Me.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbRefresh.Name = "tsbRefresh"
        Me.tsbRefresh.Size = New System.Drawing.Size(23, 22)
        Me.tsbRefresh.Text = "Refresh folder"
        '
        'tmiTools
        '
        Me.tmiTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiOptions})
        Me.tmiTools.Name = "tmiTools"
        Me.tmiTools.Size = New System.Drawing.Size(48, 20)
        Me.tmiTools.Text = "&Tools"
        '
        'tmiOptions
        '
        Me.tmiOptions.Name = "tmiOptions"
        Me.tmiOptions.Size = New System.Drawing.Size(152, 22)
        Me.tmiOptions.Text = "&Options ..."
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(495, 334)
        Me.Controls.Add(Me.tscMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
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
        Me.stsStatus.ResumeLayout(False)
        Me.stsStatus.PerformLayout()
        Me.msnMain.ResumeLayout(False)
        Me.msnMain.PerformLayout()
        Me.tosMain.ResumeLayout(False)
        Me.tosMain.PerformLayout()
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
    Friend WithEvents tosMain As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbForward As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents tmiHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiOptions As System.Windows.Forms.ToolStripMenuItem

End Class
