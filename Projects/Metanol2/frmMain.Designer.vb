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
        Me.lvwImages = New Tools.Metanol.TotalCommanderListView
        Me.cmsImages = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tmiMerge = New System.Windows.Forms.ToolStripMenuItem
        Me.imlImages = New System.Windows.Forms.ImageList(Me.components)
        Me.tabInfo = New System.Windows.Forms.TabControl
        Me.tapCommon = New System.Windows.Forms.TabPage
        Me.flpCommon = New System.Windows.Forms.FlowLayoutPanel
        Me.panImage = New System.Windows.Forms.Panel
        Me.picPreview = New System.Windows.Forms.PictureBox
        Me.llbLarge = New System.Windows.Forms.LinkLabel
        Me.cmdErrInfo = New System.Windows.Forms.Button
        Me.sptImage = New System.Windows.Forms.Splitter
        Me.fraTitle = New System.Windows.Forms.GroupBox
        Me.tlpTitle = New System.Windows.Forms.TableLayoutPanel
        Me.lblObjectName = New System.Windows.Forms.Label
        Me.txtObjectName = New System.Windows.Forms.TextBox
        Me.lblCaption = New System.Windows.Forms.Label
        Me.txtCaption = New System.Windows.Forms.TextBox
        Me.sptTitle = New System.Windows.Forms.Splitter
        Me.fraLocation = New System.Windows.Forms.GroupBox
        Me.tlpLocation = New System.Windows.Forms.TableLayoutPanel
        Me.lblCity = New System.Windows.Forms.Label
        Me.txtCity = New System.Windows.Forms.TextBox
        Me.lblCountryCode = New System.Windows.Forms.Label
        Me.cmbCountryCode = New System.Windows.Forms.ComboBox
        Me.lblCountry = New System.Windows.Forms.Label
        Me.txtCountry = New System.Windows.Forms.TextBox
        Me.lblProvince = New System.Windows.Forms.Label
        Me.txtProvince = New System.Windows.Forms.TextBox
        Me.lblSublocation = New System.Windows.Forms.Label
        Me.txtSublocation = New System.Windows.Forms.TextBox
        Me.fraKeywords = New System.Windows.Forms.GroupBox
        Me.kweKeywords = New Tools.WindowsT.FormsT.KeyWordsEditor
        Me.sptKeywords = New System.Windows.Forms.Splitter
        Me.fraAuthor = New System.Windows.Forms.GroupBox
        Me.tlpAuthor = New System.Windows.Forms.TableLayoutPanel
        Me.lblCopyright = New System.Windows.Forms.Label
        Me.txtCopyright = New System.Windows.Forms.TextBox
        Me.lblCredit = New System.Windows.Forms.Label
        Me.txtCredit = New System.Windows.Forms.TextBox
        Me.fraStatus = New System.Windows.Forms.GroupBox
        Me.tlpStatus = New System.Windows.Forms.TableLayoutPanel
        Me.lblEditStatus = New System.Windows.Forms.Label
        Me.txtEditStatus = New System.Windows.Forms.TextBox
        Me.lblUrgency = New System.Windows.Forms.Label
        Me.nudUrgency = New System.Windows.Forms.NumericUpDown
        Me.tapIPTC = New System.Windows.Forms.TabPage
        Me.prgIPTC = New System.Windows.Forms.PropertyGrid
        Me.tapExif = New System.Windows.Forms.TabPage
        Me.tscMain = New System.Windows.Forms.ToolStripContainer
        Me.stsStatus = New System.Windows.Forms.StatusStrip
        Me.tpbLoading = New System.Windows.Forms.ToolStripProgressBar
        Me.tslFolder = New System.Windows.Forms.ToolStripStatusLabel
        Me.tslNoFiles = New System.Windows.Forms.ToolStripStatusLabel
        Me.msnMain = New System.Windows.Forms.MenuStrip
        Me.tmiFile = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiBrowse = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiGoTo = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiSaveAll = New System.Windows.Forms.ToolStripMenuItem
        Me.tssFileSep1 = New System.Windows.Forms.ToolStripSeparator
        Me.tmiExit = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiView = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiRefresh = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiNext = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiPrevious = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiTools = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiOptions = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiHelp = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiAbout = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiVersionHistory = New System.Windows.Forms.ToolStripMenuItem
        Me.tosMain = New System.Windows.Forms.ToolStrip
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.tsbForward = New System.Windows.Forms.ToolStripButton
        Me.tsbRefresh = New System.Windows.Forms.ToolStripButton
        Me.tsbSaveAll = New System.Windows.Forms.ToolStripButton
        Me.bgwImages = New System.ComponentModel.BackgroundWorker
        Me.fbdGoTo = New System.Windows.Forms.FolderBrowserDialog
        Me.bgwSave = New System.ComponentModel.BackgroundWorker
        Me.splMain.Panel1.SuspendLayout()
        Me.splMain.Panel2.SuspendLayout()
        Me.splMain.SuspendLayout()
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
        Me.splMain.AccessibleDescription = Nothing
        Me.splMain.AccessibleName = Nothing
        resources.ApplyResources(Me.splMain, "splMain")
        Me.splMain.BackgroundImage = Nothing
        Me.splMain.Font = Nothing
        Me.splMain.Name = "splMain"
        '
        'splMain.Panel1
        '
        Me.splMain.Panel1.AccessibleDescription = Nothing
        Me.splMain.Panel1.AccessibleName = Nothing
        resources.ApplyResources(Me.splMain.Panel1, "splMain.Panel1")
        Me.splMain.Panel1.BackgroundImage = Nothing
        Me.splMain.Panel1.Controls.Add(Me.splBrowser)
        Me.splMain.Panel1.Font = Nothing
        '
        'splMain.Panel2
        '
        Me.splMain.Panel2.AccessibleDescription = Nothing
        Me.splMain.Panel2.AccessibleName = Nothing
        resources.ApplyResources(Me.splMain.Panel2, "splMain.Panel2")
        Me.splMain.Panel2.BackgroundImage = Nothing
        Me.splMain.Panel2.Controls.Add(Me.tabInfo)
        Me.splMain.Panel2.Font = Nothing
        Me.splMain.TabStop = False
        '
        'splBrowser
        '
        Me.splBrowser.AccessibleDescription = Nothing
        Me.splBrowser.AccessibleName = Nothing
        resources.ApplyResources(Me.splBrowser, "splBrowser")
        Me.splBrowser.BackgroundImage = Nothing
        Me.splBrowser.Font = Nothing
        Me.splBrowser.Name = "splBrowser"
        '
        'splBrowser.Panel1
        '
        Me.splBrowser.Panel1.AccessibleDescription = Nothing
        Me.splBrowser.Panel1.AccessibleName = Nothing
        resources.ApplyResources(Me.splBrowser.Panel1, "splBrowser.Panel1")
        Me.splBrowser.Panel1.BackgroundImage = Nothing
        Me.splBrowser.Panel1.Controls.Add(Me.lvwFolders)
        Me.splBrowser.Panel1.Font = Nothing
        '
        'splBrowser.Panel2
        '
        Me.splBrowser.Panel2.AccessibleDescription = Nothing
        Me.splBrowser.Panel2.AccessibleName = Nothing
        resources.ApplyResources(Me.splBrowser.Panel2, "splBrowser.Panel2")
        Me.splBrowser.Panel2.BackgroundImage = Nothing
        Me.splBrowser.Panel2.Controls.Add(Me.lvwImages)
        Me.splBrowser.Panel2.Font = Nothing
        Me.splBrowser.TabStop = False
        '
        'lvwFolders
        '
        Me.lvwFolders.AccessibleDescription = Nothing
        Me.lvwFolders.AccessibleName = Nothing
        resources.ApplyResources(Me.lvwFolders, "lvwFolders")
        Me.lvwFolders.BackgroundImage = Nothing
        Me.lvwFolders.Font = Nothing
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
        Me.lvwImages.AccessibleDescription = Nothing
        Me.lvwImages.AccessibleName = Nothing
        resources.ApplyResources(Me.lvwImages, "lvwImages")
        Me.lvwImages.BackgroundImage = Nothing
        Me.lvwImages.ContextMenuStrip = Me.cmsImages
        Me.lvwImages.Font = Nothing
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
        Me.cmsImages.AccessibleDescription = Nothing
        Me.cmsImages.AccessibleName = Nothing
        resources.ApplyResources(Me.cmsImages, "cmsImages")
        Me.cmsImages.BackgroundImage = Nothing
        Me.cmsImages.Font = Nothing
        Me.cmsImages.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiMerge})
        Me.cmsImages.Name = "cmsImages"
        '
        'tmiMerge
        '
        Me.tmiMerge.AccessibleDescription = Nothing
        Me.tmiMerge.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiMerge, "tmiMerge")
        Me.tmiMerge.BackgroundImage = Nothing
        Me.tmiMerge.Name = "tmiMerge"
        Me.tmiMerge.ShortcutKeyDisplayString = Nothing
        '
        'imlImages
        '
        Me.imlImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        resources.ApplyResources(Me.imlImages, "imlImages")
        Me.imlImages.TransparentColor = System.Drawing.Color.Transparent
        '
        'tabInfo
        '
        Me.tabInfo.AccessibleDescription = Nothing
        Me.tabInfo.AccessibleName = Nothing
        resources.ApplyResources(Me.tabInfo, "tabInfo")
        Me.tabInfo.BackgroundImage = Nothing
        Me.tabInfo.Controls.Add(Me.tapCommon)
        Me.tabInfo.Controls.Add(Me.tapIPTC)
        Me.tabInfo.Controls.Add(Me.tapExif)
        Me.tabInfo.Font = Nothing
        Me.tabInfo.Name = "tabInfo"
        Me.tabInfo.SelectedIndex = 0
        Me.tabInfo.TabStop = False
        '
        'tapCommon
        '
        Me.tapCommon.AccessibleDescription = Nothing
        Me.tapCommon.AccessibleName = Nothing
        resources.ApplyResources(Me.tapCommon, "tapCommon")
        Me.tapCommon.BackgroundImage = Nothing
        Me.tapCommon.Controls.Add(Me.flpCommon)
        Me.tapCommon.Font = Nothing
        Me.tapCommon.Name = "tapCommon"
        Me.tapCommon.UseVisualStyleBackColor = True
        '
        'flpCommon
        '
        Me.flpCommon.AccessibleDescription = Nothing
        Me.flpCommon.AccessibleName = Nothing
        resources.ApplyResources(Me.flpCommon, "flpCommon")
        Me.flpCommon.BackgroundImage = Nothing
        Me.flpCommon.Controls.Add(Me.panImage)
        Me.flpCommon.Controls.Add(Me.sptImage)
        Me.flpCommon.Controls.Add(Me.fraTitle)
        Me.flpCommon.Controls.Add(Me.sptTitle)
        Me.flpCommon.Controls.Add(Me.fraLocation)
        Me.flpCommon.Controls.Add(Me.fraKeywords)
        Me.flpCommon.Controls.Add(Me.sptKeywords)
        Me.flpCommon.Controls.Add(Me.fraAuthor)
        Me.flpCommon.Controls.Add(Me.fraStatus)
        Me.flpCommon.Font = Nothing
        Me.flpCommon.Name = "flpCommon"
        '
        'panImage
        '
        Me.panImage.AccessibleDescription = Nothing
        Me.panImage.AccessibleName = Nothing
        resources.ApplyResources(Me.panImage, "panImage")
        Me.panImage.BackgroundImage = Nothing
        Me.panImage.Controls.Add(Me.picPreview)
        Me.panImage.Controls.Add(Me.llbLarge)
        Me.panImage.Controls.Add(Me.cmdErrInfo)
        Me.panImage.Font = Nothing
        Me.panImage.MinimumSize = New System.Drawing.Size(0, 32)
        Me.panImage.Name = "panImage"
        '
        'picPreview
        '
        Me.picPreview.AccessibleDescription = Nothing
        Me.picPreview.AccessibleName = Nothing
        resources.ApplyResources(Me.picPreview, "picPreview")
        Me.picPreview.BackgroundImage = Nothing
        Me.picPreview.Font = Nothing
        Me.picPreview.ImageLocation = Nothing
        Me.picPreview.InitialImage = Global.Tools.Metanol.My.Resources.Resources.Metanol
        Me.picPreview.Name = "picPreview"
        Me.picPreview.TabStop = False
        '
        'llbLarge
        '
        Me.llbLarge.AccessibleDescription = Nothing
        Me.llbLarge.AccessibleName = Nothing
        resources.ApplyResources(Me.llbLarge, "llbLarge")
        Me.llbLarge.Font = Nothing
        Me.llbLarge.Name = "llbLarge"
        Me.llbLarge.TabStop = True
        '
        'cmdErrInfo
        '
        Me.cmdErrInfo.AccessibleDescription = Nothing
        Me.cmdErrInfo.AccessibleName = Nothing
        resources.ApplyResources(Me.cmdErrInfo, "cmdErrInfo")
        Me.cmdErrInfo.BackgroundImage = Nothing
        Me.cmdErrInfo.FlatAppearance.BorderSize = 0
        Me.cmdErrInfo.Font = Nothing
        Me.cmdErrInfo.Image = Global.Tools.Metanol.My.Resources.Resources.Symbol_Delete
        Me.cmdErrInfo.Name = "cmdErrInfo"
        Me.cmdErrInfo.TabStop = False
        Me.cmdErrInfo.UseVisualStyleBackColor = True
        '
        'sptImage
        '
        Me.sptImage.AccessibleDescription = Nothing
        Me.sptImage.AccessibleName = Nothing
        resources.ApplyResources(Me.sptImage, "sptImage")
        Me.sptImage.BackgroundImage = Nothing
        Me.sptImage.Cursor = System.Windows.Forms.Cursors.HSplit
        Me.sptImage.Font = Nothing
        Me.sptImage.Name = "sptImage"
        Me.sptImage.TabStop = False
        '
        'fraTitle
        '
        Me.fraTitle.AccessibleDescription = Nothing
        Me.fraTitle.AccessibleName = Nothing
        resources.ApplyResources(Me.fraTitle, "fraTitle")
        Me.fraTitle.BackgroundImage = Nothing
        Me.fraTitle.Controls.Add(Me.tlpTitle)
        Me.fraTitle.Font = Nothing
        Me.fraTitle.MinimumSize = New System.Drawing.Size(0, 100)
        Me.fraTitle.Name = "fraTitle"
        Me.fraTitle.TabStop = False
        '
        'tlpTitle
        '
        Me.tlpTitle.AccessibleDescription = Nothing
        Me.tlpTitle.AccessibleName = Nothing
        resources.ApplyResources(Me.tlpTitle, "tlpTitle")
        Me.tlpTitle.BackgroundImage = Nothing
        Me.tlpTitle.Controls.Add(Me.lblObjectName, 0, 0)
        Me.tlpTitle.Controls.Add(Me.txtObjectName, 1, 0)
        Me.tlpTitle.Controls.Add(Me.lblCaption, 0, 1)
        Me.tlpTitle.Controls.Add(Me.txtCaption, 0, 2)
        Me.tlpTitle.Font = Nothing
        Me.tlpTitle.Name = "tlpTitle"
        '
        'lblObjectName
        '
        Me.lblObjectName.AccessibleDescription = Nothing
        Me.lblObjectName.AccessibleName = Nothing
        resources.ApplyResources(Me.lblObjectName, "lblObjectName")
        Me.lblObjectName.Font = Nothing
        Me.lblObjectName.Name = "lblObjectName"
        '
        'txtObjectName
        '
        Me.txtObjectName.AccessibleDescription = Nothing
        Me.txtObjectName.AccessibleName = Nothing
        resources.ApplyResources(Me.txtObjectName, "txtObjectName")
        Me.txtObjectName.BackgroundImage = Nothing
        Me.txtObjectName.Font = Nothing
        Me.txtObjectName.Name = "txtObjectName"
        '
        'lblCaption
        '
        Me.lblCaption.AccessibleDescription = Nothing
        Me.lblCaption.AccessibleName = Nothing
        resources.ApplyResources(Me.lblCaption, "lblCaption")
        Me.tlpTitle.SetColumnSpan(Me.lblCaption, 2)
        Me.lblCaption.Font = Nothing
        Me.lblCaption.Name = "lblCaption"
        '
        'txtCaption
        '
        Me.txtCaption.AccessibleDescription = Nothing
        Me.txtCaption.AccessibleName = Nothing
        resources.ApplyResources(Me.txtCaption, "txtCaption")
        Me.txtCaption.BackgroundImage = Nothing
        Me.tlpTitle.SetColumnSpan(Me.txtCaption, 2)
        Me.txtCaption.Font = Nothing
        Me.txtCaption.Name = "txtCaption"
        '
        'sptTitle
        '
        Me.sptTitle.AccessibleDescription = Nothing
        Me.sptTitle.AccessibleName = Nothing
        resources.ApplyResources(Me.sptTitle, "sptTitle")
        Me.sptTitle.BackgroundImage = Nothing
        Me.sptTitle.Cursor = System.Windows.Forms.Cursors.HSplit
        Me.sptTitle.Font = Nothing
        Me.sptTitle.Name = "sptTitle"
        Me.sptTitle.TabStop = False
        '
        'fraLocation
        '
        Me.fraLocation.AccessibleDescription = Nothing
        Me.fraLocation.AccessibleName = Nothing
        resources.ApplyResources(Me.fraLocation, "fraLocation")
        Me.fraLocation.BackgroundImage = Nothing
        Me.fraLocation.Controls.Add(Me.tlpLocation)
        Me.fraLocation.Font = Nothing
        Me.fraLocation.Name = "fraLocation"
        Me.fraLocation.TabStop = False
        '
        'tlpLocation
        '
        Me.tlpLocation.AccessibleDescription = Nothing
        Me.tlpLocation.AccessibleName = Nothing
        resources.ApplyResources(Me.tlpLocation, "tlpLocation")
        Me.tlpLocation.BackgroundImage = Nothing
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
        Me.tlpLocation.Font = Nothing
        Me.tlpLocation.Name = "tlpLocation"
        '
        'lblCity
        '
        Me.lblCity.AccessibleDescription = Nothing
        Me.lblCity.AccessibleName = Nothing
        resources.ApplyResources(Me.lblCity, "lblCity")
        Me.lblCity.Font = Nothing
        Me.lblCity.Name = "lblCity"
        '
        'txtCity
        '
        Me.txtCity.AccessibleDescription = Nothing
        Me.txtCity.AccessibleName = Nothing
        resources.ApplyResources(Me.txtCity, "txtCity")
        Me.txtCity.BackgroundImage = Nothing
        Me.txtCity.Font = Nothing
        Me.txtCity.Name = "txtCity"
        '
        'lblCountryCode
        '
        Me.lblCountryCode.AccessibleDescription = Nothing
        Me.lblCountryCode.AccessibleName = Nothing
        resources.ApplyResources(Me.lblCountryCode, "lblCountryCode")
        Me.lblCountryCode.Font = Nothing
        Me.lblCountryCode.Name = "lblCountryCode"
        '
        'cmbCountryCode
        '
        Me.cmbCountryCode.AccessibleDescription = Nothing
        Me.cmbCountryCode.AccessibleName = Nothing
        resources.ApplyResources(Me.cmbCountryCode, "cmbCountryCode")
        Me.cmbCountryCode.BackgroundImage = Nothing
        Me.cmbCountryCode.Font = Nothing
        Me.cmbCountryCode.FormattingEnabled = True
        Me.cmbCountryCode.Name = "cmbCountryCode"
        '
        'lblCountry
        '
        Me.lblCountry.AccessibleDescription = Nothing
        Me.lblCountry.AccessibleName = Nothing
        resources.ApplyResources(Me.lblCountry, "lblCountry")
        Me.lblCountry.Font = Nothing
        Me.lblCountry.Name = "lblCountry"
        '
        'txtCountry
        '
        Me.txtCountry.AccessibleDescription = Nothing
        Me.txtCountry.AccessibleName = Nothing
        resources.ApplyResources(Me.txtCountry, "txtCountry")
        Me.txtCountry.BackgroundImage = Nothing
        Me.txtCountry.Font = Nothing
        Me.txtCountry.Name = "txtCountry"
        '
        'lblProvince
        '
        Me.lblProvince.AccessibleDescription = Nothing
        Me.lblProvince.AccessibleName = Nothing
        resources.ApplyResources(Me.lblProvince, "lblProvince")
        Me.lblProvince.Font = Nothing
        Me.lblProvince.Name = "lblProvince"
        '
        'txtProvince
        '
        Me.txtProvince.AccessibleDescription = Nothing
        Me.txtProvince.AccessibleName = Nothing
        resources.ApplyResources(Me.txtProvince, "txtProvince")
        Me.txtProvince.BackgroundImage = Nothing
        Me.txtProvince.Font = Nothing
        Me.txtProvince.Name = "txtProvince"
        '
        'lblSublocation
        '
        Me.lblSublocation.AccessibleDescription = Nothing
        Me.lblSublocation.AccessibleName = Nothing
        resources.ApplyResources(Me.lblSublocation, "lblSublocation")
        Me.lblSublocation.Font = Nothing
        Me.lblSublocation.Name = "lblSublocation"
        '
        'txtSublocation
        '
        Me.txtSublocation.AccessibleDescription = Nothing
        Me.txtSublocation.AccessibleName = Nothing
        resources.ApplyResources(Me.txtSublocation, "txtSublocation")
        Me.txtSublocation.BackgroundImage = Nothing
        Me.txtSublocation.Font = Nothing
        Me.txtSublocation.Name = "txtSublocation"
        '
        'fraKeywords
        '
        Me.fraKeywords.AccessibleDescription = Nothing
        Me.fraKeywords.AccessibleName = Nothing
        resources.ApplyResources(Me.fraKeywords, "fraKeywords")
        Me.fraKeywords.BackgroundImage = Nothing
        Me.fraKeywords.Controls.Add(Me.kweKeywords)
        Me.fraKeywords.Font = Nothing
        Me.fraKeywords.MinimumSize = New System.Drawing.Size(0, 100)
        Me.fraKeywords.Name = "fraKeywords"
        Me.fraKeywords.TabStop = False
        '
        'kweKeywords
        '
        Me.kweKeywords.AccessibleDescription = Nothing
        Me.kweKeywords.AccessibleName = Nothing
        resources.ApplyResources(Me.kweKeywords, "kweKeywords")
        Me.kweKeywords.AutoCompleteCacheName = "Keywords"
        Me.kweKeywords.AutomaticsLists_Designer = True
        Me.kweKeywords.BackgroundImage = Nothing
        Me.kweKeywords.Font = Nothing
        Me.kweKeywords.Name = "kweKeywords"
        Me.kweKeywords.StatusState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
        '
        'sptKeywords
        '
        Me.sptKeywords.AccessibleDescription = Nothing
        Me.sptKeywords.AccessibleName = Nothing
        resources.ApplyResources(Me.sptKeywords, "sptKeywords")
        Me.sptKeywords.BackgroundImage = Nothing
        Me.sptKeywords.Cursor = System.Windows.Forms.Cursors.HSplit
        Me.sptKeywords.Font = Nothing
        Me.sptKeywords.Name = "sptKeywords"
        Me.sptKeywords.TabStop = False
        '
        'fraAuthor
        '
        Me.fraAuthor.AccessibleDescription = Nothing
        Me.fraAuthor.AccessibleName = Nothing
        resources.ApplyResources(Me.fraAuthor, "fraAuthor")
        Me.fraAuthor.BackgroundImage = Nothing
        Me.fraAuthor.Controls.Add(Me.tlpAuthor)
        Me.fraAuthor.Font = Nothing
        Me.fraAuthor.Name = "fraAuthor"
        Me.fraAuthor.TabStop = False
        '
        'tlpAuthor
        '
        Me.tlpAuthor.AccessibleDescription = Nothing
        Me.tlpAuthor.AccessibleName = Nothing
        resources.ApplyResources(Me.tlpAuthor, "tlpAuthor")
        Me.tlpAuthor.BackgroundImage = Nothing
        Me.tlpAuthor.Controls.Add(Me.lblCopyright, 0, 0)
        Me.tlpAuthor.Controls.Add(Me.txtCopyright, 1, 0)
        Me.tlpAuthor.Controls.Add(Me.lblCredit, 0, 1)
        Me.tlpAuthor.Controls.Add(Me.txtCredit, 1, 1)
        Me.tlpAuthor.Font = Nothing
        Me.tlpAuthor.Name = "tlpAuthor"
        '
        'lblCopyright
        '
        Me.lblCopyright.AccessibleDescription = Nothing
        Me.lblCopyright.AccessibleName = Nothing
        resources.ApplyResources(Me.lblCopyright, "lblCopyright")
        Me.lblCopyright.Font = Nothing
        Me.lblCopyright.Name = "lblCopyright"
        '
        'txtCopyright
        '
        Me.txtCopyright.AccessibleDescription = Nothing
        Me.txtCopyright.AccessibleName = Nothing
        resources.ApplyResources(Me.txtCopyright, "txtCopyright")
        Me.txtCopyright.BackgroundImage = Nothing
        Me.txtCopyright.Font = Nothing
        Me.txtCopyright.Name = "txtCopyright"
        '
        'lblCredit
        '
        Me.lblCredit.AccessibleDescription = Nothing
        Me.lblCredit.AccessibleName = Nothing
        resources.ApplyResources(Me.lblCredit, "lblCredit")
        Me.lblCredit.Font = Nothing
        Me.lblCredit.Name = "lblCredit"
        '
        'txtCredit
        '
        Me.txtCredit.AccessibleDescription = Nothing
        Me.txtCredit.AccessibleName = Nothing
        resources.ApplyResources(Me.txtCredit, "txtCredit")
        Me.txtCredit.BackgroundImage = Nothing
        Me.txtCredit.Font = Nothing
        Me.txtCredit.Name = "txtCredit"
        '
        'fraStatus
        '
        Me.fraStatus.AccessibleDescription = Nothing
        Me.fraStatus.AccessibleName = Nothing
        resources.ApplyResources(Me.fraStatus, "fraStatus")
        Me.fraStatus.BackgroundImage = Nothing
        Me.fraStatus.Controls.Add(Me.tlpStatus)
        Me.fraStatus.Font = Nothing
        Me.fraStatus.Name = "fraStatus"
        Me.fraStatus.TabStop = False
        '
        'tlpStatus
        '
        Me.tlpStatus.AccessibleDescription = Nothing
        Me.tlpStatus.AccessibleName = Nothing
        resources.ApplyResources(Me.tlpStatus, "tlpStatus")
        Me.tlpStatus.BackgroundImage = Nothing
        Me.tlpStatus.Controls.Add(Me.lblEditStatus, 0, 0)
        Me.tlpStatus.Controls.Add(Me.txtEditStatus, 1, 0)
        Me.tlpStatus.Controls.Add(Me.lblUrgency, 0, 1)
        Me.tlpStatus.Controls.Add(Me.nudUrgency, 1, 1)
        Me.tlpStatus.Font = Nothing
        Me.tlpStatus.Name = "tlpStatus"
        '
        'lblEditStatus
        '
        Me.lblEditStatus.AccessibleDescription = Nothing
        Me.lblEditStatus.AccessibleName = Nothing
        resources.ApplyResources(Me.lblEditStatus, "lblEditStatus")
        Me.lblEditStatus.Font = Nothing
        Me.lblEditStatus.Name = "lblEditStatus"
        '
        'txtEditStatus
        '
        Me.txtEditStatus.AccessibleDescription = Nothing
        Me.txtEditStatus.AccessibleName = Nothing
        resources.ApplyResources(Me.txtEditStatus, "txtEditStatus")
        Me.txtEditStatus.BackgroundImage = Nothing
        Me.txtEditStatus.Font = Nothing
        Me.txtEditStatus.Name = "txtEditStatus"
        '
        'lblUrgency
        '
        Me.lblUrgency.AccessibleDescription = Nothing
        Me.lblUrgency.AccessibleName = Nothing
        resources.ApplyResources(Me.lblUrgency, "lblUrgency")
        Me.lblUrgency.Font = Nothing
        Me.lblUrgency.Name = "lblUrgency"
        '
        'nudUrgency
        '
        Me.nudUrgency.AccessibleDescription = Nothing
        Me.nudUrgency.AccessibleName = Nothing
        resources.ApplyResources(Me.nudUrgency, "nudUrgency")
        Me.nudUrgency.Font = Nothing
        Me.nudUrgency.Name = "nudUrgency"
        '
        'tapIPTC
        '
        Me.tapIPTC.AccessibleDescription = Nothing
        Me.tapIPTC.AccessibleName = Nothing
        resources.ApplyResources(Me.tapIPTC, "tapIPTC")
        Me.tapIPTC.BackgroundImage = Nothing
        Me.tapIPTC.Controls.Add(Me.prgIPTC)
        Me.tapIPTC.Font = Nothing
        Me.tapIPTC.Name = "tapIPTC"
        Me.tapIPTC.UseVisualStyleBackColor = True
        '
        'prgIPTC
        '
        Me.prgIPTC.AccessibleDescription = Nothing
        Me.prgIPTC.AccessibleName = Nothing
        resources.ApplyResources(Me.prgIPTC, "prgIPTC")
        Me.prgIPTC.BackgroundImage = Nothing
        Me.prgIPTC.Font = Nothing
        Me.prgIPTC.Name = "prgIPTC"
        '
        'tapExif
        '
        Me.tapExif.AccessibleDescription = Nothing
        Me.tapExif.AccessibleName = Nothing
        resources.ApplyResources(Me.tapExif, "tapExif")
        Me.tapExif.BackgroundImage = Nothing
        Me.tapExif.Font = Nothing
        Me.tapExif.Name = "tapExif"
        Me.tapExif.UseVisualStyleBackColor = True
        '
        'tscMain
        '
        Me.tscMain.AccessibleDescription = Nothing
        Me.tscMain.AccessibleName = Nothing
        resources.ApplyResources(Me.tscMain, "tscMain")
        '
        'tscMain.BottomToolStripPanel
        '
        Me.tscMain.BottomToolStripPanel.AccessibleDescription = Nothing
        Me.tscMain.BottomToolStripPanel.AccessibleName = Nothing
        Me.tscMain.BottomToolStripPanel.BackgroundImage = Nothing
        resources.ApplyResources(Me.tscMain.BottomToolStripPanel, "tscMain.BottomToolStripPanel")
        Me.tscMain.BottomToolStripPanel.Controls.Add(Me.stsStatus)
        Me.tscMain.BottomToolStripPanel.Font = Nothing
        '
        'tscMain.ContentPanel
        '
        Me.tscMain.ContentPanel.AccessibleDescription = Nothing
        Me.tscMain.ContentPanel.AccessibleName = Nothing
        resources.ApplyResources(Me.tscMain.ContentPanel, "tscMain.ContentPanel")
        Me.tscMain.ContentPanel.BackgroundImage = Nothing
        Me.tscMain.ContentPanel.Controls.Add(Me.splMain)
        Me.tscMain.ContentPanel.Font = Nothing
        Me.tscMain.Font = Nothing
        '
        'tscMain.LeftToolStripPanel
        '
        Me.tscMain.LeftToolStripPanel.AccessibleDescription = Nothing
        Me.tscMain.LeftToolStripPanel.AccessibleName = Nothing
        Me.tscMain.LeftToolStripPanel.BackgroundImage = Nothing
        resources.ApplyResources(Me.tscMain.LeftToolStripPanel, "tscMain.LeftToolStripPanel")
        Me.tscMain.LeftToolStripPanel.Font = Nothing
        Me.tscMain.Name = "tscMain"
        '
        'tscMain.RightToolStripPanel
        '
        Me.tscMain.RightToolStripPanel.AccessibleDescription = Nothing
        Me.tscMain.RightToolStripPanel.AccessibleName = Nothing
        Me.tscMain.RightToolStripPanel.BackgroundImage = Nothing
        resources.ApplyResources(Me.tscMain.RightToolStripPanel, "tscMain.RightToolStripPanel")
        Me.tscMain.RightToolStripPanel.Font = Nothing
        '
        'tscMain.TopToolStripPanel
        '
        Me.tscMain.TopToolStripPanel.AccessibleDescription = Nothing
        Me.tscMain.TopToolStripPanel.AccessibleName = Nothing
        Me.tscMain.TopToolStripPanel.BackgroundImage = Nothing
        resources.ApplyResources(Me.tscMain.TopToolStripPanel, "tscMain.TopToolStripPanel")
        Me.tscMain.TopToolStripPanel.Controls.Add(Me.msnMain)
        Me.tscMain.TopToolStripPanel.Controls.Add(Me.tosMain)
        Me.tscMain.TopToolStripPanel.Font = Nothing
        '
        'stsStatus
        '
        Me.stsStatus.AccessibleDescription = Nothing
        Me.stsStatus.AccessibleName = Nothing
        resources.ApplyResources(Me.stsStatus, "stsStatus")
        Me.stsStatus.BackgroundImage = Nothing
        Me.stsStatus.Font = Nothing
        Me.stsStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tpbLoading, Me.tslFolder, Me.tslNoFiles})
        Me.stsStatus.Name = "stsStatus"
        Me.stsStatus.ShowItemToolTips = True
        '
        'tpbLoading
        '
        Me.tpbLoading.AccessibleDescription = Nothing
        Me.tpbLoading.AccessibleName = Nothing
        resources.ApplyResources(Me.tpbLoading, "tpbLoading")
        Me.tpbLoading.Name = "tpbLoading"
        '
        'tslFolder
        '
        Me.tslFolder.AccessibleDescription = Nothing
        Me.tslFolder.AccessibleName = Nothing
        resources.ApplyResources(Me.tslFolder, "tslFolder")
        Me.tslFolder.BackgroundImage = Nothing
        Me.tslFolder.Name = "tslFolder"
        '
        'tslNoFiles
        '
        Me.tslNoFiles.AccessibleDescription = Nothing
        Me.tslNoFiles.AccessibleName = Nothing
        resources.ApplyResources(Me.tslNoFiles, "tslNoFiles")
        Me.tslNoFiles.BackgroundImage = Nothing
        Me.tslNoFiles.Name = "tslNoFiles"
        '
        'msnMain
        '
        Me.msnMain.AccessibleDescription = Nothing
        Me.msnMain.AccessibleName = Nothing
        resources.ApplyResources(Me.msnMain, "msnMain")
        Me.msnMain.BackgroundImage = Nothing
        Me.msnMain.Font = Nothing
        Me.msnMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiFile, Me.tmiView, Me.tmiTools, Me.tmiHelp})
        Me.msnMain.Name = "msnMain"
        '
        'tmiFile
        '
        Me.tmiFile.AccessibleDescription = Nothing
        Me.tmiFile.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiFile, "tmiFile")
        Me.tmiFile.BackgroundImage = Nothing
        Me.tmiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiBrowse, Me.tmiGoTo, Me.tmiSaveAll, Me.tssFileSep1, Me.tmiExit})
        Me.tmiFile.Name = "tmiFile"
        Me.tmiFile.ShortcutKeyDisplayString = Nothing
        '
        'tmiBrowse
        '
        Me.tmiBrowse.AccessibleDescription = Nothing
        Me.tmiBrowse.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiBrowse, "tmiBrowse")
        Me.tmiBrowse.BackgroundImage = Nothing
        Me.tmiBrowse.Name = "tmiBrowse"
        Me.tmiBrowse.ShortcutKeyDisplayString = Nothing
        '
        'tmiGoTo
        '
        Me.tmiGoTo.AccessibleDescription = Nothing
        Me.tmiGoTo.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiGoTo, "tmiGoTo")
        Me.tmiGoTo.BackgroundImage = Nothing
        Me.tmiGoTo.Name = "tmiGoTo"
        Me.tmiGoTo.ShortcutKeyDisplayString = Nothing
        '
        'tmiSaveAll
        '
        Me.tmiSaveAll.AccessibleDescription = Nothing
        Me.tmiSaveAll.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiSaveAll, "tmiSaveAll")
        Me.tmiSaveAll.BackgroundImage = Nothing
        Me.tmiSaveAll.Image = Global.Tools.Metanol.My.Resources.Resources.SaveAllHS
        Me.tmiSaveAll.Name = "tmiSaveAll"
        Me.tmiSaveAll.ShortcutKeyDisplayString = Nothing
        '
        'tssFileSep1
        '
        Me.tssFileSep1.AccessibleDescription = Nothing
        Me.tssFileSep1.AccessibleName = Nothing
        resources.ApplyResources(Me.tssFileSep1, "tssFileSep1")
        Me.tssFileSep1.Name = "tssFileSep1"
        '
        'tmiExit
        '
        Me.tmiExit.AccessibleDescription = Nothing
        Me.tmiExit.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiExit, "tmiExit")
        Me.tmiExit.BackgroundImage = Nothing
        Me.tmiExit.Name = "tmiExit"
        '
        'tmiView
        '
        Me.tmiView.AccessibleDescription = Nothing
        Me.tmiView.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiView, "tmiView")
        Me.tmiView.BackgroundImage = Nothing
        Me.tmiView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiRefresh, Me.tmiNext, Me.tmiPrevious})
        Me.tmiView.Name = "tmiView"
        Me.tmiView.ShortcutKeyDisplayString = Nothing
        '
        'tmiRefresh
        '
        Me.tmiRefresh.AccessibleDescription = Nothing
        Me.tmiRefresh.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiRefresh, "tmiRefresh")
        Me.tmiRefresh.BackgroundImage = Nothing
        Me.tmiRefresh.Image = Global.Tools.Metanol.My.Resources.Resources.Refresh
        Me.tmiRefresh.Name = "tmiRefresh"
        '
        'tmiNext
        '
        Me.tmiNext.AccessibleDescription = Nothing
        Me.tmiNext.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiNext, "tmiNext")
        Me.tmiNext.BackgroundImage = Nothing
        Me.tmiNext.Image = Global.Tools.Metanol.My.Resources.Resources.RightArrowHS
        Me.tmiNext.Name = "tmiNext"
        Me.tmiNext.ShortcutKeyDisplayString = Nothing
        '
        'tmiPrevious
        '
        Me.tmiPrevious.AccessibleDescription = Nothing
        Me.tmiPrevious.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiPrevious, "tmiPrevious")
        Me.tmiPrevious.BackgroundImage = Nothing
        Me.tmiPrevious.Image = Global.Tools.Metanol.My.Resources.Resources.LeftArrowHS
        Me.tmiPrevious.Name = "tmiPrevious"
        Me.tmiPrevious.ShortcutKeyDisplayString = Nothing
        '
        'tmiTools
        '
        Me.tmiTools.AccessibleDescription = Nothing
        Me.tmiTools.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiTools, "tmiTools")
        Me.tmiTools.BackgroundImage = Nothing
        Me.tmiTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiOptions})
        Me.tmiTools.Name = "tmiTools"
        Me.tmiTools.ShortcutKeyDisplayString = Nothing
        '
        'tmiOptions
        '
        Me.tmiOptions.AccessibleDescription = Nothing
        Me.tmiOptions.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiOptions, "tmiOptions")
        Me.tmiOptions.BackgroundImage = Nothing
        Me.tmiOptions.Name = "tmiOptions"
        Me.tmiOptions.ShortcutKeyDisplayString = Nothing
        '
        'tmiHelp
        '
        Me.tmiHelp.AccessibleDescription = Nothing
        Me.tmiHelp.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiHelp, "tmiHelp")
        Me.tmiHelp.BackgroundImage = Nothing
        Me.tmiHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiAbout, Me.tmiVersionHistory})
        Me.tmiHelp.Name = "tmiHelp"
        Me.tmiHelp.ShortcutKeyDisplayString = Nothing
        '
        'tmiAbout
        '
        Me.tmiAbout.AccessibleDescription = Nothing
        Me.tmiAbout.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiAbout, "tmiAbout")
        Me.tmiAbout.BackgroundImage = Nothing
        Me.tmiAbout.Name = "tmiAbout"
        Me.tmiAbout.ShortcutKeyDisplayString = Nothing
        '
        'tmiVersionHistory
        '
        Me.tmiVersionHistory.AccessibleDescription = Nothing
        Me.tmiVersionHistory.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiVersionHistory, "tmiVersionHistory")
        Me.tmiVersionHistory.BackgroundImage = Nothing
        Me.tmiVersionHistory.Name = "tmiVersionHistory"
        Me.tmiVersionHistory.ShortcutKeyDisplayString = Nothing
        '
        'tosMain
        '
        Me.tosMain.AccessibleDescription = Nothing
        Me.tosMain.AccessibleName = Nothing
        resources.ApplyResources(Me.tosMain, "tosMain")
        Me.tosMain.BackgroundImage = Nothing
        Me.tosMain.Font = Nothing
        Me.tosMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbBack, Me.tsbForward, Me.tsbRefresh, Me.tsbSaveAll})
        Me.tosMain.Name = "tosMain"
        '
        'tsbBack
        '
        Me.tsbBack.AccessibleDescription = Nothing
        Me.tsbBack.AccessibleName = Nothing
        resources.ApplyResources(Me.tsbBack, "tsbBack")
        Me.tsbBack.BackgroundImage = Nothing
        Me.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbBack.Image = Global.Tools.Metanol.My.Resources.Resources.NavBack
        Me.tsbBack.Name = "tsbBack"
        '
        'tsbForward
        '
        Me.tsbForward.AccessibleDescription = Nothing
        Me.tsbForward.AccessibleName = Nothing
        resources.ApplyResources(Me.tsbForward, "tsbForward")
        Me.tsbForward.BackgroundImage = Nothing
        Me.tsbForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbForward.Image = Global.Tools.Metanol.My.Resources.Resources.NavForward
        Me.tsbForward.Name = "tsbForward"
        '
        'tsbRefresh
        '
        Me.tsbRefresh.AccessibleDescription = Nothing
        Me.tsbRefresh.AccessibleName = Nothing
        resources.ApplyResources(Me.tsbRefresh, "tsbRefresh")
        Me.tsbRefresh.BackgroundImage = Nothing
        Me.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbRefresh.Image = Global.Tools.Metanol.My.Resources.Resources.Refresh
        Me.tsbRefresh.Name = "tsbRefresh"
        '
        'tsbSaveAll
        '
        Me.tsbSaveAll.AccessibleDescription = Nothing
        Me.tsbSaveAll.AccessibleName = Nothing
        resources.ApplyResources(Me.tsbSaveAll, "tsbSaveAll")
        Me.tsbSaveAll.BackgroundImage = Nothing
        Me.tsbSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbSaveAll.Image = Global.Tools.Metanol.My.Resources.Resources.SaveAllHS
        Me.tsbSaveAll.Name = "tsbSaveAll"
        '
        'bgwImages
        '
        Me.bgwImages.WorkerReportsProgress = True
        Me.bgwImages.WorkerSupportsCancellation = True
        '
        'fbdGoTo
        '
        resources.ApplyResources(Me.fbdGoTo, "fbdGoTo")
        '
        'bgwSave
        '
        Me.bgwSave.WorkerReportsProgress = True
        '
        'frmMain
        '
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Nothing
        Me.Controls.Add(Me.tscMain)
        Me.Font = Nothing
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.msnMain
        Me.Name = "frmMain"
        Me.splMain.Panel1.ResumeLayout(False)
        Me.splMain.Panel2.ResumeLayout(False)
        Me.splMain.ResumeLayout(False)
        Me.splBrowser.Panel1.ResumeLayout(False)
        Me.splBrowser.Panel2.ResumeLayout(False)
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

End Class
