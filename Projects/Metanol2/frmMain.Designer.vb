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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.splMain = New System.Windows.Forms.SplitContainer()
        Me.splBrowser = New System.Windows.Forms.SplitContainer()
        Me.lvwFolders = New System.Windows.Forms.ListView()
        Me.imlFolders = New System.Windows.Forms.ImageList(Me.components)
        Me.lvwImages = New Tools.Metanol.TotalCommanderListView()
        Me.cmsImages = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tmiMerge = New System.Windows.Forms.ToolStripMenuItem()
        Me.imlImages = New System.Windows.Forms.ImageList(Me.components)
        Me.tabInfo = New System.Windows.Forms.TabControl()
        Me.tapCommon = New System.Windows.Forms.TabPage()
        Me.flpCommon = New System.Windows.Forms.FlowLayoutPanel()
        Me.panImage = New System.Windows.Forms.Panel()
        Me.lblExifDateTime = New System.Windows.Forms.Label()
        Me.picPreview = New System.Windows.Forms.PictureBox()
        Me.llbLarge = New System.Windows.Forms.LinkLabel()
        Me.cmdErrInfo = New System.Windows.Forms.Button()
        Me.sptImage = New System.Windows.Forms.Splitter()
        Me.fraTitle = New System.Windows.Forms.GroupBox()
        Me.tlpTitle = New System.Windows.Forms.TableLayoutPanel()
        Me.lblObjectName = New System.Windows.Forms.Label()
        Me.txtObjectName = New System.Windows.Forms.TextBox()
        Me.lblCaption = New System.Windows.Forms.Label()
        Me.txtCaption = New System.Windows.Forms.TextBox()
        Me.sptTitle = New System.Windows.Forms.Splitter()
        Me.fraLocation = New System.Windows.Forms.GroupBox()
        Me.tlpLocation = New System.Windows.Forms.TableLayoutPanel()
        Me.lblCity = New System.Windows.Forms.Label()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.lblCountryCode = New System.Windows.Forms.Label()
        Me.cmbCountryCode = New System.Windows.Forms.ComboBox()
        Me.lblCountry = New System.Windows.Forms.Label()
        Me.txtCountry = New System.Windows.Forms.TextBox()
        Me.lblProvince = New System.Windows.Forms.Label()
        Me.txtProvince = New System.Windows.Forms.TextBox()
        Me.lblSublocation = New System.Windows.Forms.Label()
        Me.txtSublocation = New System.Windows.Forms.TextBox()
        Me.fraKeywords = New System.Windows.Forms.GroupBox()
        Me.kweKeywords = New Tools.WindowsT.FormsT.KeyWordsEditor()
        Me.sptKeywords = New System.Windows.Forms.Splitter()
        Me.fraAuthor = New System.Windows.Forms.GroupBox()
        Me.tlpAuthor = New System.Windows.Forms.TableLayoutPanel()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.txtCopyright = New System.Windows.Forms.TextBox()
        Me.lblCredit = New System.Windows.Forms.Label()
        Me.txtCredit = New System.Windows.Forms.TextBox()
        Me.fraStatus = New System.Windows.Forms.GroupBox()
        Me.tlpStatus = New System.Windows.Forms.TableLayoutPanel()
        Me.lblEditStatus = New System.Windows.Forms.Label()
        Me.txtEditStatus = New System.Windows.Forms.TextBox()
        Me.lblUrgency = New System.Windows.Forms.Label()
        Me.nudUrgency = New System.Windows.Forms.NumericUpDown()
        Me.tapIPTC = New System.Windows.Forms.TabPage()
        Me.prgIPTC = New System.Windows.Forms.PropertyGrid()
        Me.tapExif = New System.Windows.Forms.TabPage()
        Me.tabExif = New System.Windows.Forms.TabControl()
        Me.tapExifMain = New System.Windows.Forms.TabPage()
        Me.prgExifMain = New System.Windows.Forms.PropertyGrid()
        Me.tapExifExif = New System.Windows.Forms.TabPage()
        Me.prgExifExif = New System.Windows.Forms.PropertyGrid()
        Me.tapExifGPS = New System.Windows.Forms.TabPage()
        Me.prgExifGPS = New System.Windows.Forms.PropertyGrid()
        Me.tapExifInterop = New System.Windows.Forms.TabPage()
        Me.prgExifInterop = New System.Windows.Forms.PropertyGrid()
        Me.tapExifThumbnail = New System.Windows.Forms.TabPage()
        Me.prgExifThumbnail = New System.Windows.Forms.PropertyGrid()
        Me.tscMain = New System.Windows.Forms.ToolStripContainer()
        Me.stsStatus = New System.Windows.Forms.StatusStrip()
        Me.tpbLoading = New System.Windows.Forms.ToolStripProgressBar()
        Me.tslFolder = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslNoFiles = New System.Windows.Forms.ToolStripStatusLabel()
        Me.msnMain = New System.Windows.Forms.MenuStrip()
        Me.tmiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiBrowse = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiGoTo = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiSaveAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.tssFileSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tmiExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiView = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiRefresh = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiNext = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiPrevious = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiOptions = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiSynchronizeWithDatabase = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmiVersionHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.tosMain = New System.Windows.Forms.ToolStrip()
        Me.tsbBack = New System.Windows.Forms.ToolStripButton()
        Me.tsbForward = New System.Windows.Forms.ToolStripButton()
        Me.tsbRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tsbSaveAll = New System.Windows.Forms.ToolStripButton()
        Me.bgwImages = New System.ComponentModel.BackgroundWorker()
        Me.fbdGoTo = New System.Windows.Forms.FolderBrowserDialog()
        Me.bgwSave = New System.ComponentModel.BackgroundWorker()
        Me.tmiExport = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.splMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splMain.Panel1.SuspendLayout()
        Me.splMain.Panel2.SuspendLayout()
        Me.splMain.SuspendLayout()
        CType(Me.splBrowser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splBrowser.Panel1.SuspendLayout()
        Me.splBrowser.Panel2.SuspendLayout()
        Me.splBrowser.SuspendLayout()
        Me.cmsImages.SuspendLayout()
        Me.tabInfo.SuspendLayout()
        Me.tapCommon.SuspendLayout()
        Me.flpCommon.SuspendLayout()
        Me.panImage.SuspendLayout()
        CType(Me.picPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraTitle.SuspendLayout()
        Me.tlpTitle.SuspendLayout()
        Me.fraLocation.SuspendLayout()
        Me.tlpLocation.SuspendLayout()
        Me.fraKeywords.SuspendLayout()
        Me.fraAuthor.SuspendLayout()
        Me.tlpAuthor.SuspendLayout()
        Me.fraStatus.SuspendLayout()
        Me.tlpStatus.SuspendLayout()
        CType(Me.nudUrgency, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tapIPTC.SuspendLayout()
        Me.tapExif.SuspendLayout()
        Me.tabExif.SuspendLayout()
        Me.tapExifMain.SuspendLayout()
        Me.tapExifExif.SuspendLayout()
        Me.tapExifGPS.SuspendLayout()
        Me.tapExifInterop.SuspendLayout()
        Me.tapExifThumbnail.SuspendLayout()
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
        resources.ApplyResources(Me.splMain, "splMain")
        Me.splMain.Name = "splMain"
        '
        'splMain.Panel1
        '
        Me.splMain.Panel1.Controls.Add(Me.splBrowser)
        '
        'splMain.Panel2
        '
        Me.splMain.Panel2.Controls.Add(Me.tabInfo)
        resources.ApplyResources(Me.splMain.Panel2, "splMain.Panel2")
        Me.splMain.TabStop = False
        '
        'splBrowser
        '
        resources.ApplyResources(Me.splBrowser, "splBrowser")
        Me.splBrowser.Name = "splBrowser"
        '
        'splBrowser.Panel1
        '
        Me.splBrowser.Panel1.Controls.Add(Me.lvwFolders)
        '
        'splBrowser.Panel2
        '
        Me.splBrowser.Panel2.Controls.Add(Me.lvwImages)
        Me.splBrowser.TabStop = False
        '
        'lvwFolders
        '
        resources.ApplyResources(Me.lvwFolders, "lvwFolders")
        Me.lvwFolders.MultiSelect = False
        Me.lvwFolders.Name = "lvwFolders"
        Me.lvwFolders.SmallImageList = Me.imlFolders
        Me.lvwFolders.TabStop = False
        Me.lvwFolders.UseCompatibleStateImageBehavior = False
        Me.lvwFolders.View = System.Windows.Forms.View.List
        '
        'imlFolders
        '
        Me.imlFolders.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        resources.ApplyResources(Me.imlFolders, "imlFolders")
        Me.imlFolders.TransparentColor = System.Drawing.Color.Transparent
        '
        'lvwImages
        '
        Me.lvwImages.ContextMenuStrip = Me.cmsImages
        resources.ApplyResources(Me.lvwImages, "lvwImages")
        Me.lvwImages.HideSelection = False
        Me.lvwImages.LargeImageList = Me.imlImages
        Me.lvwImages.Name = "lvwImages"
        Me.lvwImages.OwnerDraw = True
        Me.lvwImages.ShowItemToolTips = True
        Me.lvwImages.TabStop = False
        Me.lvwImages.UseCompatibleStateImageBehavior = False
        '
        'cmsImages
        '
        Me.cmsImages.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiMerge, Me.tmiExport})
        Me.cmsImages.Name = "cmsImages"
        resources.ApplyResources(Me.cmsImages, "cmsImages")
        '
        'tmiMerge
        '
        Me.tmiMerge.Name = "tmiMerge"
        resources.ApplyResources(Me.tmiMerge, "tmiMerge")
        '
        'imlImages
        '
        Me.imlImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        resources.ApplyResources(Me.imlImages, "imlImages")
        Me.imlImages.TransparentColor = System.Drawing.Color.Transparent
        '
        'tabInfo
        '
        Me.tabInfo.Controls.Add(Me.tapCommon)
        Me.tabInfo.Controls.Add(Me.tapIPTC)
        Me.tabInfo.Controls.Add(Me.tapExif)
        resources.ApplyResources(Me.tabInfo, "tabInfo")
        Me.tabInfo.Name = "tabInfo"
        Me.tabInfo.SelectedIndex = 0
        Me.tabInfo.TabStop = False
        '
        'tapCommon
        '
        Me.tapCommon.Controls.Add(Me.flpCommon)
        resources.ApplyResources(Me.tapCommon, "tapCommon")
        Me.tapCommon.Name = "tapCommon"
        Me.tapCommon.UseVisualStyleBackColor = True
        '
        'flpCommon
        '
        Me.flpCommon.Controls.Add(Me.panImage)
        Me.flpCommon.Controls.Add(Me.sptImage)
        Me.flpCommon.Controls.Add(Me.fraTitle)
        Me.flpCommon.Controls.Add(Me.sptTitle)
        Me.flpCommon.Controls.Add(Me.fraLocation)
        Me.flpCommon.Controls.Add(Me.fraKeywords)
        Me.flpCommon.Controls.Add(Me.sptKeywords)
        Me.flpCommon.Controls.Add(Me.fraAuthor)
        Me.flpCommon.Controls.Add(Me.fraStatus)
        resources.ApplyResources(Me.flpCommon, "flpCommon")
        Me.flpCommon.Name = "flpCommon"
        '
        'panImage
        '
        Me.panImage.Controls.Add(Me.lblExifDateTime)
        Me.panImage.Controls.Add(Me.picPreview)
        Me.panImage.Controls.Add(Me.llbLarge)
        Me.panImage.Controls.Add(Me.cmdErrInfo)
        resources.ApplyResources(Me.panImage, "panImage")
        Me.panImage.MinimumSize = New System.Drawing.Size(0, 32)
        Me.panImage.Name = "panImage"
        '
        'lblExifDateTime
        '
        resources.ApplyResources(Me.lblExifDateTime, "lblExifDateTime")
        Me.lblExifDateTime.Name = "lblExifDateTime"
        '
        'picPreview
        '
        resources.ApplyResources(Me.picPreview, "picPreview")
        Me.picPreview.InitialImage = Global.Tools.Metanol.My.Resources.Resources.Metanol
        Me.picPreview.Name = "picPreview"
        Me.picPreview.TabStop = False
        '
        'llbLarge
        '
        resources.ApplyResources(Me.llbLarge, "llbLarge")
        Me.llbLarge.Name = "llbLarge"
        Me.llbLarge.TabStop = True
        '
        'cmdErrInfo
        '
        resources.ApplyResources(Me.cmdErrInfo, "cmdErrInfo")
        Me.cmdErrInfo.FlatAppearance.BorderSize = 0
        Me.cmdErrInfo.Image = Global.Tools.Metanol.My.Resources.Resources.Symbol_Delete
        Me.cmdErrInfo.Name = "cmdErrInfo"
        Me.cmdErrInfo.TabStop = False
        Me.cmdErrInfo.UseVisualStyleBackColor = True
        '
        'sptImage
        '
        Me.sptImage.Cursor = System.Windows.Forms.Cursors.HSplit
        resources.ApplyResources(Me.sptImage, "sptImage")
        Me.sptImage.Name = "sptImage"
        Me.sptImage.TabStop = False
        '
        'fraTitle
        '
        resources.ApplyResources(Me.fraTitle, "fraTitle")
        Me.fraTitle.Controls.Add(Me.tlpTitle)
        Me.fraTitle.MinimumSize = New System.Drawing.Size(0, 100)
        Me.fraTitle.Name = "fraTitle"
        Me.fraTitle.TabStop = False
        '
        'tlpTitle
        '
        resources.ApplyResources(Me.tlpTitle, "tlpTitle")
        Me.tlpTitle.Controls.Add(Me.lblObjectName, 0, 0)
        Me.tlpTitle.Controls.Add(Me.txtObjectName, 1, 0)
        Me.tlpTitle.Controls.Add(Me.lblCaption, 0, 1)
        Me.tlpTitle.Controls.Add(Me.txtCaption, 0, 2)
        Me.tlpTitle.Name = "tlpTitle"
        '
        'lblObjectName
        '
        resources.ApplyResources(Me.lblObjectName, "lblObjectName")
        Me.lblObjectName.Name = "lblObjectName"
        '
        'txtObjectName
        '
        resources.ApplyResources(Me.txtObjectName, "txtObjectName")
        Me.txtObjectName.Name = "txtObjectName"
        '
        'lblCaption
        '
        resources.ApplyResources(Me.lblCaption, "lblCaption")
        Me.tlpTitle.SetColumnSpan(Me.lblCaption, 2)
        Me.lblCaption.Name = "lblCaption"
        '
        'txtCaption
        '
        resources.ApplyResources(Me.txtCaption, "txtCaption")
        Me.tlpTitle.SetColumnSpan(Me.txtCaption, 2)
        Me.txtCaption.Name = "txtCaption"
        '
        'sptTitle
        '
        Me.sptTitle.Cursor = System.Windows.Forms.Cursors.HSplit
        resources.ApplyResources(Me.sptTitle, "sptTitle")
        Me.sptTitle.Name = "sptTitle"
        Me.sptTitle.TabStop = False
        '
        'fraLocation
        '
        resources.ApplyResources(Me.fraLocation, "fraLocation")
        Me.fraLocation.Controls.Add(Me.tlpLocation)
        Me.fraLocation.Name = "fraLocation"
        Me.fraLocation.TabStop = False
        '
        'tlpLocation
        '
        resources.ApplyResources(Me.tlpLocation, "tlpLocation")
        Me.tlpLocation.Controls.Add(Me.lblCity, 0, 0)
        Me.tlpLocation.Controls.Add(Me.txtCity, 1, 0)
        Me.tlpLocation.Controls.Add(Me.lblCountryCode, 0, 1)
        Me.tlpLocation.Controls.Add(Me.cmbCountryCode, 1, 1)
        Me.tlpLocation.Controls.Add(Me.lblCountry, 0, 2)
        Me.tlpLocation.Controls.Add(Me.txtCountry, 1, 2)
        Me.tlpLocation.Controls.Add(Me.lblProvince, 0, 3)
        Me.tlpLocation.Controls.Add(Me.txtProvince, 1, 3)
        Me.tlpLocation.Controls.Add(Me.lblSublocation, 0, 4)
        Me.tlpLocation.Controls.Add(Me.txtSublocation, 1, 4)
        Me.tlpLocation.Name = "tlpLocation"
        '
        'lblCity
        '
        resources.ApplyResources(Me.lblCity, "lblCity")
        Me.lblCity.Name = "lblCity"
        '
        'txtCity
        '
        resources.ApplyResources(Me.txtCity, "txtCity")
        Me.txtCity.Name = "txtCity"
        '
        'lblCountryCode
        '
        resources.ApplyResources(Me.lblCountryCode, "lblCountryCode")
        Me.lblCountryCode.Name = "lblCountryCode"
        '
        'cmbCountryCode
        '
        resources.ApplyResources(Me.cmbCountryCode, "cmbCountryCode")
        Me.cmbCountryCode.FormattingEnabled = True
        Me.cmbCountryCode.Name = "cmbCountryCode"
        '
        'lblCountry
        '
        resources.ApplyResources(Me.lblCountry, "lblCountry")
        Me.lblCountry.Name = "lblCountry"
        '
        'txtCountry
        '
        resources.ApplyResources(Me.txtCountry, "txtCountry")
        Me.txtCountry.Name = "txtCountry"
        '
        'lblProvince
        '
        resources.ApplyResources(Me.lblProvince, "lblProvince")
        Me.lblProvince.Name = "lblProvince"
        '
        'txtProvince
        '
        resources.ApplyResources(Me.txtProvince, "txtProvince")
        Me.txtProvince.Name = "txtProvince"
        '
        'lblSublocation
        '
        resources.ApplyResources(Me.lblSublocation, "lblSublocation")
        Me.lblSublocation.Name = "lblSublocation"
        '
        'txtSublocation
        '
        resources.ApplyResources(Me.txtSublocation, "txtSublocation")
        Me.txtSublocation.Name = "txtSublocation"
        '
        'fraKeywords
        '
        Me.fraKeywords.Controls.Add(Me.kweKeywords)
        resources.ApplyResources(Me.fraKeywords, "fraKeywords")
        Me.fraKeywords.MinimumSize = New System.Drawing.Size(0, 100)
        Me.fraKeywords.Name = "fraKeywords"
        Me.fraKeywords.TabStop = False
        '
        'kweKeywords
        '
        Me.kweKeywords.AutoCompleteCacheName = "Keywords"
        Me.kweKeywords.AutomaticsLists_Designer = True
        resources.ApplyResources(Me.kweKeywords, "kweKeywords")
        Me.kweKeywords.Name = "kweKeywords"
        Me.kweKeywords.StatusState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
        '
        'sptKeywords
        '
        Me.sptKeywords.Cursor = System.Windows.Forms.Cursors.HSplit
        resources.ApplyResources(Me.sptKeywords, "sptKeywords")
        Me.sptKeywords.Name = "sptKeywords"
        Me.sptKeywords.TabStop = False
        '
        'fraAuthor
        '
        resources.ApplyResources(Me.fraAuthor, "fraAuthor")
        Me.fraAuthor.Controls.Add(Me.tlpAuthor)
        Me.fraAuthor.Name = "fraAuthor"
        Me.fraAuthor.TabStop = False
        '
        'tlpAuthor
        '
        resources.ApplyResources(Me.tlpAuthor, "tlpAuthor")
        Me.tlpAuthor.Controls.Add(Me.lblCopyright, 0, 0)
        Me.tlpAuthor.Controls.Add(Me.txtCopyright, 1, 0)
        Me.tlpAuthor.Controls.Add(Me.lblCredit, 0, 1)
        Me.tlpAuthor.Controls.Add(Me.txtCredit, 1, 1)
        Me.tlpAuthor.Name = "tlpAuthor"
        '
        'lblCopyright
        '
        resources.ApplyResources(Me.lblCopyright, "lblCopyright")
        Me.lblCopyright.Name = "lblCopyright"
        '
        'txtCopyright
        '
        resources.ApplyResources(Me.txtCopyright, "txtCopyright")
        Me.txtCopyright.Name = "txtCopyright"
        '
        'lblCredit
        '
        resources.ApplyResources(Me.lblCredit, "lblCredit")
        Me.lblCredit.Name = "lblCredit"
        '
        'txtCredit
        '
        resources.ApplyResources(Me.txtCredit, "txtCredit")
        Me.txtCredit.Name = "txtCredit"
        '
        'fraStatus
        '
        resources.ApplyResources(Me.fraStatus, "fraStatus")
        Me.fraStatus.Controls.Add(Me.tlpStatus)
        Me.fraStatus.Name = "fraStatus"
        Me.fraStatus.TabStop = False
        '
        'tlpStatus
        '
        resources.ApplyResources(Me.tlpStatus, "tlpStatus")
        Me.tlpStatus.Controls.Add(Me.lblEditStatus, 0, 0)
        Me.tlpStatus.Controls.Add(Me.txtEditStatus, 1, 0)
        Me.tlpStatus.Controls.Add(Me.lblUrgency, 0, 1)
        Me.tlpStatus.Controls.Add(Me.nudUrgency, 1, 1)
        Me.tlpStatus.Name = "tlpStatus"
        '
        'lblEditStatus
        '
        resources.ApplyResources(Me.lblEditStatus, "lblEditStatus")
        Me.lblEditStatus.Name = "lblEditStatus"
        '
        'txtEditStatus
        '
        resources.ApplyResources(Me.txtEditStatus, "txtEditStatus")
        Me.txtEditStatus.Name = "txtEditStatus"
        '
        'lblUrgency
        '
        resources.ApplyResources(Me.lblUrgency, "lblUrgency")
        Me.lblUrgency.Name = "lblUrgency"
        '
        'nudUrgency
        '
        resources.ApplyResources(Me.nudUrgency, "nudUrgency")
        Me.nudUrgency.Name = "nudUrgency"
        '
        'tapIPTC
        '
        Me.tapIPTC.Controls.Add(Me.prgIPTC)
        resources.ApplyResources(Me.tapIPTC, "tapIPTC")
        Me.tapIPTC.Name = "tapIPTC"
        Me.tapIPTC.UseVisualStyleBackColor = True
        '
        'prgIPTC
        '
        resources.ApplyResources(Me.prgIPTC, "prgIPTC")
        Me.prgIPTC.Name = "prgIPTC"
        '
        'tapExif
        '
        Me.tapExif.Controls.Add(Me.tabExif)
        resources.ApplyResources(Me.tapExif, "tapExif")
        Me.tapExif.Name = "tapExif"
        Me.tapExif.UseVisualStyleBackColor = True
        '
        'tabExif
        '
        Me.tabExif.Controls.Add(Me.tapExifMain)
        Me.tabExif.Controls.Add(Me.tapExifExif)
        Me.tabExif.Controls.Add(Me.tapExifGPS)
        Me.tabExif.Controls.Add(Me.tapExifInterop)
        Me.tabExif.Controls.Add(Me.tapExifThumbnail)
        resources.ApplyResources(Me.tabExif, "tabExif")
        Me.tabExif.Name = "tabExif"
        Me.tabExif.SelectedIndex = 0
        '
        'tapExifMain
        '
        Me.tapExifMain.Controls.Add(Me.prgExifMain)
        resources.ApplyResources(Me.tapExifMain, "tapExifMain")
        Me.tapExifMain.Name = "tapExifMain"
        Me.tapExifMain.UseVisualStyleBackColor = True
        '
        'prgExifMain
        '
        resources.ApplyResources(Me.prgExifMain, "prgExifMain")
        Me.prgExifMain.Name = "prgExifMain"
        '
        'tapExifExif
        '
        Me.tapExifExif.Controls.Add(Me.prgExifExif)
        resources.ApplyResources(Me.tapExifExif, "tapExifExif")
        Me.tapExifExif.Name = "tapExifExif"
        Me.tapExifExif.UseVisualStyleBackColor = True
        '
        'prgExifExif
        '
        resources.ApplyResources(Me.prgExifExif, "prgExifExif")
        Me.prgExifExif.Name = "prgExifExif"
        '
        'tapExifGPS
        '
        Me.tapExifGPS.Controls.Add(Me.prgExifGPS)
        resources.ApplyResources(Me.tapExifGPS, "tapExifGPS")
        Me.tapExifGPS.Name = "tapExifGPS"
        Me.tapExifGPS.UseVisualStyleBackColor = True
        '
        'prgExifGPS
        '
        resources.ApplyResources(Me.prgExifGPS, "prgExifGPS")
        Me.prgExifGPS.Name = "prgExifGPS"
        '
        'tapExifInterop
        '
        Me.tapExifInterop.Controls.Add(Me.prgExifInterop)
        resources.ApplyResources(Me.tapExifInterop, "tapExifInterop")
        Me.tapExifInterop.Name = "tapExifInterop"
        Me.tapExifInterop.UseVisualStyleBackColor = True
        '
        'prgExifInterop
        '
        resources.ApplyResources(Me.prgExifInterop, "prgExifInterop")
        Me.prgExifInterop.Name = "prgExifInterop"
        '
        'tapExifThumbnail
        '
        Me.tapExifThumbnail.Controls.Add(Me.prgExifThumbnail)
        resources.ApplyResources(Me.tapExifThumbnail, "tapExifThumbnail")
        Me.tapExifThumbnail.Name = "tapExifThumbnail"
        Me.tapExifThumbnail.UseVisualStyleBackColor = True
        '
        'prgExifThumbnail
        '
        resources.ApplyResources(Me.prgExifThumbnail, "prgExifThumbnail")
        Me.prgExifThumbnail.Name = "prgExifThumbnail"
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
        resources.ApplyResources(Me.tscMain.ContentPanel, "tscMain.ContentPanel")
        resources.ApplyResources(Me.tscMain, "tscMain")
        Me.tscMain.Name = "tscMain"
        '
        'tscMain.TopToolStripPanel
        '
        Me.tscMain.TopToolStripPanel.Controls.Add(Me.msnMain)
        Me.tscMain.TopToolStripPanel.Controls.Add(Me.tosMain)
        '
        'stsStatus
        '
        resources.ApplyResources(Me.stsStatus, "stsStatus")
        Me.stsStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tpbLoading, Me.tslFolder, Me.tslNoFiles})
        Me.stsStatus.Name = "stsStatus"
        Me.stsStatus.ShowItemToolTips = True
        '
        'tpbLoading
        '
        Me.tpbLoading.Name = "tpbLoading"
        resources.ApplyResources(Me.tpbLoading, "tpbLoading")
        '
        'tslFolder
        '
        Me.tslFolder.Name = "tslFolder"
        resources.ApplyResources(Me.tslFolder, "tslFolder")
        '
        'tslNoFiles
        '
        Me.tslNoFiles.Name = "tslNoFiles"
        resources.ApplyResources(Me.tslNoFiles, "tslNoFiles")
        '
        'msnMain
        '
        resources.ApplyResources(Me.msnMain, "msnMain")
        Me.msnMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiFile, Me.tmiView, Me.tmiTools, Me.tmiHelp})
        Me.msnMain.Name = "msnMain"
        '
        'tmiFile
        '
        Me.tmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiBrowse, Me.tmiGoTo, Me.tmiSaveAll, Me.tssFileSep1, Me.tmiExit})
        Me.tmiFile.Name = "tmiFile"
        resources.ApplyResources(Me.tmiFile, "tmiFile")
        '
        'tmiBrowse
        '
        Me.tmiBrowse.Name = "tmiBrowse"
        resources.ApplyResources(Me.tmiBrowse, "tmiBrowse")
        '
        'tmiGoTo
        '
        Me.tmiGoTo.Name = "tmiGoTo"
        resources.ApplyResources(Me.tmiGoTo, "tmiGoTo")
        '
        'tmiSaveAll
        '
        Me.tmiSaveAll.Image = Global.Tools.Metanol.My.Resources.Resources.SaveAllHS
        Me.tmiSaveAll.Name = "tmiSaveAll"
        resources.ApplyResources(Me.tmiSaveAll, "tmiSaveAll")
        '
        'tssFileSep1
        '
        Me.tssFileSep1.Name = "tssFileSep1"
        resources.ApplyResources(Me.tssFileSep1, "tssFileSep1")
        '
        'tmiExit
        '
        Me.tmiExit.Name = "tmiExit"
        resources.ApplyResources(Me.tmiExit, "tmiExit")
        '
        'tmiView
        '
        Me.tmiView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiRefresh, Me.tmiNext, Me.tmiPrevious})
        Me.tmiView.Name = "tmiView"
        resources.ApplyResources(Me.tmiView, "tmiView")
        '
        'tmiRefresh
        '
        Me.tmiRefresh.Image = Global.Tools.Metanol.My.Resources.Resources.Refresh
        Me.tmiRefresh.Name = "tmiRefresh"
        resources.ApplyResources(Me.tmiRefresh, "tmiRefresh")
        '
        'tmiNext
        '
        Me.tmiNext.Image = Global.Tools.Metanol.My.Resources.Resources.RightArrowHS
        Me.tmiNext.Name = "tmiNext"
        resources.ApplyResources(Me.tmiNext, "tmiNext")
        '
        'tmiPrevious
        '
        Me.tmiPrevious.Image = Global.Tools.Metanol.My.Resources.Resources.LeftArrowHS
        resources.ApplyResources(Me.tmiPrevious, "tmiPrevious")
        Me.tmiPrevious.Name = "tmiPrevious"
        '
        'tmiTools
        '
        Me.tmiTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiOptions, Me.tmiSynchronizeWithDatabase})
        Me.tmiTools.Name = "tmiTools"
        resources.ApplyResources(Me.tmiTools, "tmiTools")
        '
        'tmiOptions
        '
        Me.tmiOptions.Name = "tmiOptions"
        resources.ApplyResources(Me.tmiOptions, "tmiOptions")
        '
        'tmiSynchronizeWithDatabase
        '
        Me.tmiSynchronizeWithDatabase.Name = "tmiSynchronizeWithDatabase"
        resources.ApplyResources(Me.tmiSynchronizeWithDatabase, "tmiSynchronizeWithDatabase")
        '
        'tmiHelp
        '
        Me.tmiHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiAbout, Me.tmiVersionHistory})
        Me.tmiHelp.Name = "tmiHelp"
        resources.ApplyResources(Me.tmiHelp, "tmiHelp")
        '
        'tmiAbout
        '
        Me.tmiAbout.Name = "tmiAbout"
        resources.ApplyResources(Me.tmiAbout, "tmiAbout")
        '
        'tmiVersionHistory
        '
        Me.tmiVersionHistory.Name = "tmiVersionHistory"
        resources.ApplyResources(Me.tmiVersionHistory, "tmiVersionHistory")
        '
        'tosMain
        '
        resources.ApplyResources(Me.tosMain, "tosMain")
        Me.tosMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbBack, Me.tsbForward, Me.tsbRefresh, Me.tsbSaveAll})
        Me.tosMain.Name = "tosMain"
        '
        'tsbBack
        '
        Me.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.tsbBack, "tsbBack")
        Me.tsbBack.Image = Global.Tools.Metanol.My.Resources.Resources.NavBack
        Me.tsbBack.Name = "tsbBack"
        '
        'tsbForward
        '
        Me.tsbForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        resources.ApplyResources(Me.tsbForward, "tsbForward")
        Me.tsbForward.Image = Global.Tools.Metanol.My.Resources.Resources.NavForward
        Me.tsbForward.Name = "tsbForward"
        '
        'tsbRefresh
        '
        Me.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbRefresh.Image = Global.Tools.Metanol.My.Resources.Resources.Refresh
        resources.ApplyResources(Me.tsbRefresh, "tsbRefresh")
        Me.tsbRefresh.Name = "tsbRefresh"
        '
        'tsbSaveAll
        '
        Me.tsbSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSaveAll.Image = Global.Tools.Metanol.My.Resources.Resources.SaveAllHS
        resources.ApplyResources(Me.tsbSaveAll, "tsbSaveAll")
        Me.tsbSaveAll.Name = "tsbSaveAll"
        '
        'bgwImages
        '
        Me.bgwImages.WorkerReportsProgress = True
        Me.bgwImages.WorkerSupportsCancellation = True
        '
        'bgwSave
        '
        Me.bgwSave.WorkerReportsProgress = True
        '
        'tmiExport
        '
        Me.tmiExport.Name = "tmiExport"
        resources.ApplyResources(Me.tmiExport, "tmiExport")
        '
        'frmMain
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tscMain)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.msnMain
        Me.Name = "frmMain"
        Me.splMain.Panel1.ResumeLayout(False)
        Me.splMain.Panel2.ResumeLayout(False)
        CType(Me.splMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splMain.ResumeLayout(False)
        Me.splBrowser.Panel1.ResumeLayout(False)
        Me.splBrowser.Panel2.ResumeLayout(False)
        CType(Me.splBrowser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splBrowser.ResumeLayout(False)
        Me.cmsImages.ResumeLayout(False)
        Me.tabInfo.ResumeLayout(False)
        Me.tapCommon.ResumeLayout(False)
        Me.flpCommon.ResumeLayout(False)
        Me.flpCommon.PerformLayout()
        Me.panImage.ResumeLayout(False)
        Me.panImage.PerformLayout()
        CType(Me.picPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraTitle.ResumeLayout(False)
        Me.fraTitle.PerformLayout()
        Me.tlpTitle.ResumeLayout(False)
        Me.tlpTitle.PerformLayout()
        Me.fraLocation.ResumeLayout(False)
        Me.fraLocation.PerformLayout()
        Me.tlpLocation.ResumeLayout(False)
        Me.tlpLocation.PerformLayout()
        Me.fraKeywords.ResumeLayout(False)
        Me.fraAuthor.ResumeLayout(False)
        Me.fraAuthor.PerformLayout()
        Me.tlpAuthor.ResumeLayout(False)
        Me.tlpAuthor.PerformLayout()
        Me.fraStatus.ResumeLayout(False)
        Me.fraStatus.PerformLayout()
        Me.tlpStatus.ResumeLayout(False)
        Me.tlpStatus.PerformLayout()
        CType(Me.nudUrgency, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tapIPTC.ResumeLayout(False)
        Me.tapExif.ResumeLayout(False)
        Me.tabExif.ResumeLayout(False)
        Me.tapExifMain.ResumeLayout(False)
        Me.tapExifExif.ResumeLayout(False)
        Me.tapExifGPS.ResumeLayout(False)
        Me.tapExifInterop.ResumeLayout(False)
        Me.tapExifThumbnail.ResumeLayout(False)
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
    Friend WithEvents lvwImages As TotalCommanderListView
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
    Friend WithEvents tabInfo As System.Windows.Forms.TabControl
    Friend WithEvents tapCommon As System.Windows.Forms.TabPage
    Friend WithEvents flpCommon As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents tapIPTC As System.Windows.Forms.TabPage
    Friend WithEvents panImage As System.Windows.Forms.Panel
    Friend WithEvents fraAuthor As System.Windows.Forms.GroupBox
    Friend WithEvents fraLocation As System.Windows.Forms.GroupBox
    Friend WithEvents fraStatus As System.Windows.Forms.GroupBox
    Friend WithEvents tlpAuthor As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblCopyright As System.Windows.Forms.Label
    Friend WithEvents txtCopyright As System.Windows.Forms.TextBox
    Friend WithEvents lblCredit As System.Windows.Forms.Label
    Friend WithEvents txtCredit As System.Windows.Forms.TextBox
    Friend WithEvents tlpLocation As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblCity As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents lblCountryCode As System.Windows.Forms.Label
    Friend WithEvents cmbCountryCode As System.Windows.Forms.ComboBox
    Friend WithEvents lblCountry As System.Windows.Forms.Label
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents tlpStatus As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblEditStatus As System.Windows.Forms.Label
    Friend WithEvents txtEditStatus As System.Windows.Forms.TextBox
    Friend WithEvents lblUrgency As System.Windows.Forms.Label
    Friend WithEvents fraTitle As System.Windows.Forms.GroupBox
    Friend WithEvents tlpTitle As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblObjectName As System.Windows.Forms.Label
    Friend WithEvents txtObjectName As System.Windows.Forms.TextBox
    Friend WithEvents lblCaption As System.Windows.Forms.Label
    Friend WithEvents lblProvince As System.Windows.Forms.Label
    Friend WithEvents txtProvince As System.Windows.Forms.TextBox
    Friend WithEvents lblSublocation As System.Windows.Forms.Label
    Friend WithEvents txtSublocation As System.Windows.Forms.TextBox
    Friend WithEvents nudUrgency As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtCaption As System.Windows.Forms.TextBox
    Friend WithEvents fraKeywords As System.Windows.Forms.GroupBox
    Friend WithEvents llbLarge As System.Windows.Forms.LinkLabel
    Friend WithEvents cmdErrInfo As System.Windows.Forms.Button
    Friend WithEvents sptImage As System.Windows.Forms.Splitter
    Friend WithEvents sptKeywords As System.Windows.Forms.Splitter
    Friend WithEvents sptTitle As System.Windows.Forms.Splitter
    Friend WithEvents picPreview As System.Windows.Forms.PictureBox
    Friend WithEvents prgIPTC As System.Windows.Forms.PropertyGrid
    Friend WithEvents kweKeywords As Tools.WindowsT.FormsT.KeyWordsEditor
    Friend WithEvents bgwSave As System.ComponentModel.BackgroundWorker
    Friend WithEvents tsbSaveAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents tmiSaveAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiView As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiRefresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiNext As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiPrevious As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tapExif As System.Windows.Forms.TabPage
    Friend WithEvents tslNoFiles As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cmsImages As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tmiMerge As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiVersionHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tabExif As System.Windows.Forms.TabControl
    Friend WithEvents tapExifMain As System.Windows.Forms.TabPage
    Friend WithEvents prgExifMain As System.Windows.Forms.PropertyGrid
    Friend WithEvents tapExifExif As System.Windows.Forms.TabPage
    Friend WithEvents prgExifExif As System.Windows.Forms.PropertyGrid
    Friend WithEvents tapExifGPS As System.Windows.Forms.TabPage
    Friend WithEvents prgExifGPS As System.Windows.Forms.PropertyGrid
    Friend WithEvents tapExifInterop As System.Windows.Forms.TabPage
    Friend WithEvents prgExifInterop As System.Windows.Forms.PropertyGrid
    Friend WithEvents tapExifThumbnail As System.Windows.Forms.TabPage
    Friend WithEvents prgExifThumbnail As System.Windows.Forms.PropertyGrid
    Friend WithEvents tmiSynchronizeWithDatabase As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblExifDateTime As System.Windows.Forms.Label
    Friend WithEvents tmiExport As System.Windows.Forms.ToolStripMenuItem

End Class
