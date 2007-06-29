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
        Me.splFolder = New System.Windows.Forms.SplitContainer
        Me.lvwImages = New System.Windows.Forms.ListView
        Me.imlImages = New System.Windows.Forms.ImageList(Me.components)
        Me.lvwFolder = New System.Windows.Forms.ListView
        Me.imlFolder = New System.Windows.Forms.ImageList(Me.components)
        Me.tlpPath = New System.Windows.Forms.TableLayoutPanel
        Me.cmdBrowse = New System.Windows.Forms.Button
        Me.cmdGo = New System.Windows.Forms.Button
        Me.txtPath = New System.Windows.Forms.TextBox
        Me.splHorizontal = New System.Windows.Forms.SplitContainer
        Me.splVertical = New System.Windows.Forms.SplitContainer
        Me.cmdLarge = New System.Windows.Forms.Button
        Me.picPreview = New System.Windows.Forms.PictureBox
        Me.fraKeyWords = New System.Windows.Forms.GroupBox
        Me.kweKeyWords = New Tools.WindowsT.FormsT.KeyWordsEditor
        Me.tabChoices = New System.Windows.Forms.TabControl
        Me.tapCommon = New System.Windows.Forms.TabPage
        Me.tlpItems = New System.Windows.Forms.TableLayoutPanel
        Me.fraTitle = New System.Windows.Forms.GroupBox
        Me.tlpTitle = New System.Windows.Forms.TableLayoutPanel
        Me.lblObjectName = New System.Windows.Forms.Label
        Me.txwObjectName = New Tools.WindowsT.FormsT.TextBoxWithStatus
        Me.lblCaptionAbstract = New System.Windows.Forms.Label
        Me.mxwCaptionAbstract = New Tools.WindowsT.FormsT.MultiLineTextBoxWithStatus
        Me.fraStatus = New System.Windows.Forms.GroupBox
        Me.tlpStatus = New System.Windows.Forms.TableLayoutPanel
        Me.lblEditStatus = New System.Windows.Forms.Label
        Me.txwEditStatus = New Tools.WindowsT.FormsT.TextBoxWithStatus
        Me.lblUrgency = New System.Windows.Forms.Label
        Me.nwsUrgency = New Tools.WindowsT.FormsT.NumericUpDownWithStatus
        Me.fraLocation = New System.Windows.Forms.GroupBox
        Me.tlpLocation = New System.Windows.Forms.TableLayoutPanel
        Me.lblCity = New System.Windows.Forms.Label
        Me.txwCity = New Tools.WindowsT.FormsT.TextBoxWithStatus
        Me.lblCountryCode = New System.Windows.Forms.Label
        Me.cbwCountryCode = New Tools.WindowsT.FormsT.ComboWithStatus
        Me.lblCountry = New System.Windows.Forms.Label
        Me.txwCountry = New Tools.WindowsT.FormsT.TextBoxWithStatus
        Me.lblProvince = New System.Windows.Forms.Label
        Me.txwProvince = New Tools.WindowsT.FormsT.TextBoxWithStatus
        Me.lblSubLocation = New System.Windows.Forms.Label
        Me.txwSubLocation = New Tools.WindowsT.FormsT.TextBoxWithStatus
        Me.fraAuthor = New System.Windows.Forms.GroupBox
        Me.tlpAuthor = New System.Windows.Forms.TableLayoutPanel
        Me.lblCopyright = New System.Windows.Forms.Label
        Me.txwCopyright = New Tools.WindowsT.FormsT.TextBoxWithStatus
        Me.lblCredit = New System.Windows.Forms.Label
        Me.txwCredit = New Tools.WindowsT.FormsT.TextBoxWithStatus
        Me.tapAll = New System.Windows.Forms.TabPage
        Me.prgAll = New System.Windows.Forms.PropertyGrid
        Me.fbdBrowse = New System.Windows.Forms.FolderBrowserDialog
        Me.bgwThumb = New System.ComponentModel.BackgroundWorker
        Me.stsStatus = New System.Windows.Forms.StatusStrip
        Me.tsgLoadImages = New System.Windows.Forms.ToolStripProgressBar
        Me.tslChange = New System.Windows.Forms.ToolStripStatusLabel
        Me.splMain.Panel1.SuspendLayout()
        Me.splMain.Panel2.SuspendLayout()
        Me.splMain.SuspendLayout()
        Me.splFolder.Panel1.SuspendLayout()
        Me.splFolder.Panel2.SuspendLayout()
        Me.splFolder.SuspendLayout()
        Me.tlpPath.SuspendLayout()
        Me.splHorizontal.Panel1.SuspendLayout()
        Me.splHorizontal.Panel2.SuspendLayout()
        Me.splHorizontal.SuspendLayout()
        Me.splVertical.Panel1.SuspendLayout()
        Me.splVertical.Panel2.SuspendLayout()
        Me.splVertical.SuspendLayout()
        CType(Me.picPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraKeyWords.SuspendLayout()
        Me.tabChoices.SuspendLayout()
        Me.tapCommon.SuspendLayout()
        Me.tlpItems.SuspendLayout()
        Me.fraTitle.SuspendLayout()
        Me.tlpTitle.SuspendLayout()
        Me.fraStatus.SuspendLayout()
        Me.tlpStatus.SuspendLayout()
        Me.fraLocation.SuspendLayout()
        Me.tlpLocation.SuspendLayout()
        Me.fraAuthor.SuspendLayout()
        Me.tlpAuthor.SuspendLayout()
        Me.tapAll.SuspendLayout()
        Me.stsStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'splMain
        '
        Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.splMain.Location = New System.Drawing.Point(0, 0)
        Me.splMain.Name = "splMain"
        '
        'splMain.Panel1
        '
        Me.splMain.Panel1.Controls.Add(Me.splFolder)
        Me.splMain.Panel1.Controls.Add(Me.tlpPath)
        '
        'splMain.Panel2
        '
        Me.splMain.Panel2.Controls.Add(Me.splHorizontal)
        Me.splMain.Size = New System.Drawing.Size(846, 514)
        Me.splMain.SplitterDistance = 230
        Me.splMain.TabIndex = 0
        Me.splMain.TabStop = False
        '
        'splFolder
        '
        Me.splFolder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splFolder.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.splFolder.Location = New System.Drawing.Point(0, 29)
        Me.splFolder.Name = "splFolder"
        Me.splFolder.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splFolder.Panel1
        '
        Me.splFolder.Panel1.Controls.Add(Me.lvwImages)
        '
        'splFolder.Panel2
        '
        Me.splFolder.Panel2.Controls.Add(Me.lvwFolder)
        Me.splFolder.Size = New System.Drawing.Size(230, 485)
        Me.splFolder.SplitterDistance = 337
        Me.splFolder.TabIndex = 1
        Me.splFolder.TabStop = False
        '
        'lvwImages
        '
        Me.lvwImages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwImages.HideSelection = False
        Me.lvwImages.LargeImageList = Me.imlImages
        Me.lvwImages.Location = New System.Drawing.Point(0, 0)
        Me.lvwImages.Name = "lvwImages"
        Me.lvwImages.Size = New System.Drawing.Size(230, 337)
        Me.lvwImages.TabIndex = 0
        Me.lvwImages.TabStop = False
        Me.lvwImages.UseCompatibleStateImageBehavior = False
        '
        'imlImages
        '
        Me.imlImages.ImageStream = CType(resources.GetObject("imlImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlImages.TransparentColor = System.Drawing.Color.Transparent
        Me.imlImages.Images.SetKeyName(0, "IrfanView")
        '
        'lvwFolder
        '
        Me.lvwFolder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwFolder.Location = New System.Drawing.Point(0, 0)
        Me.lvwFolder.MultiSelect = False
        Me.lvwFolder.Name = "lvwFolder"
        Me.lvwFolder.Size = New System.Drawing.Size(230, 144)
        Me.lvwFolder.SmallImageList = Me.imlFolder
        Me.lvwFolder.TabIndex = 0
        Me.lvwFolder.TabStop = False
        Me.lvwFolder.UseCompatibleStateImageBehavior = False
        Me.lvwFolder.View = System.Windows.Forms.View.List
        '
        'imlFolder
        '
        Me.imlFolder.ImageStream = CType(resources.GetObject("imlFolder.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlFolder.TransparentColor = System.Drawing.Color.Fuchsia
        Me.imlFolder.Images.SetKeyName(0, "Folder")
        Me.imlFolder.Images.SetKeyName(1, "Up")
        '
        'tlpPath
        '
        Me.tlpPath.AutoSize = True
        Me.tlpPath.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpPath.ColumnCount = 3
        Me.tlpPath.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpPath.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpPath.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpPath.Controls.Add(Me.cmdBrowse, 0, 0)
        Me.tlpPath.Controls.Add(Me.cmdGo, 0, 0)
        Me.tlpPath.Controls.Add(Me.txtPath, 0, 0)
        Me.tlpPath.Dock = System.Windows.Forms.DockStyle.Top
        Me.tlpPath.Location = New System.Drawing.Point(0, 0)
        Me.tlpPath.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpPath.Name = "tlpPath"
        Me.tlpPath.RowCount = 1
        Me.tlpPath.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpPath.Size = New System.Drawing.Size(230, 29)
        Me.tlpPath.TabIndex = 4
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdBrowse.AutoSize = True
        Me.cmdBrowse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdBrowse.Location = New System.Drawing.Point(201, 3)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(26, 23)
        Me.cmdBrowse.TabIndex = 0
        Me.cmdBrowse.TabStop = False
        Me.cmdBrowse.Text = "&..."
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'cmdGo
        '
        Me.cmdGo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdGo.AutoSize = True
        Me.cmdGo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdGo.Image = Global.Metanol.My.Resources.Resources.GoLtrHS
        Me.cmdGo.Location = New System.Drawing.Point(173, 3)
        Me.cmdGo.Name = "cmdGo"
        Me.cmdGo.Size = New System.Drawing.Size(22, 22)
        Me.cmdGo.TabIndex = 3
        Me.cmdGo.TabStop = False
        Me.cmdGo.UseVisualStyleBackColor = True
        '
        'txtPath
        '
        Me.txtPath.AcceptsReturn = True
        Me.txtPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
        Me.txtPath.Location = New System.Drawing.Point(3, 4)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(164, 20)
        Me.txtPath.TabIndex = 2
        Me.txtPath.TabStop = False
        '
        'splHorizontal
        '
        Me.splHorizontal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splHorizontal.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.splHorizontal.Location = New System.Drawing.Point(0, 0)
        Me.splHorizontal.Margin = New System.Windows.Forms.Padding(0)
        Me.splHorizontal.Name = "splHorizontal"
        '
        'splHorizontal.Panel1
        '
        Me.splHorizontal.Panel1.Controls.Add(Me.splVertical)
        '
        'splHorizontal.Panel2
        '
        Me.splHorizontal.Panel2.Controls.Add(Me.tabChoices)
        Me.splHorizontal.Size = New System.Drawing.Size(612, 514)
        Me.splHorizontal.SplitterDistance = 286
        Me.splHorizontal.TabIndex = 0
        Me.splHorizontal.TabStop = False
        '
        'splVertical
        '
        Me.splVertical.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splVertical.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.splVertical.Location = New System.Drawing.Point(0, 0)
        Me.splVertical.Margin = New System.Windows.Forms.Padding(0)
        Me.splVertical.Name = "splVertical"
        Me.splVertical.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splVertical.Panel1
        '
        Me.splVertical.Panel1.Controls.Add(Me.cmdLarge)
        Me.splVertical.Panel1.Controls.Add(Me.picPreview)
        '
        'splVertical.Panel2
        '
        Me.splVertical.Panel2.Controls.Add(Me.fraKeyWords)
        Me.splVertical.Size = New System.Drawing.Size(286, 514)
        Me.splVertical.SplitterDistance = 275
        Me.splVertical.TabIndex = 0
        Me.splVertical.TabStop = False
        '
        'cmdLarge
        '
        Me.cmdLarge.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdLarge.AutoSize = True
        Me.cmdLarge.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdLarge.BackColor = System.Drawing.Color.Transparent
        Me.cmdLarge.FlatAppearance.BorderColor = System.Drawing.SystemColors.WindowFrame
        Me.cmdLarge.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdLarge.Location = New System.Drawing.Point(240, 250)
        Me.cmdLarge.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdLarge.Name = "cmdLarge"
        Me.cmdLarge.Size = New System.Drawing.Size(46, 25)
        Me.cmdLarge.TabIndex = 1
        Me.cmdLarge.TabStop = False
        Me.cmdLarge.Text = "&Large"
        Me.cmdLarge.UseVisualStyleBackColor = False
        '
        'picPreview
        '
        Me.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picPreview.Location = New System.Drawing.Point(0, 0)
        Me.picPreview.Margin = New System.Windows.Forms.Padding(0)
        Me.picPreview.Name = "picPreview"
        Me.picPreview.Size = New System.Drawing.Size(286, 275)
        Me.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picPreview.TabIndex = 0
        Me.picPreview.TabStop = False
        '
        'fraKeyWords
        '
        Me.fraKeyWords.Controls.Add(Me.kweKeyWords)
        Me.fraKeyWords.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fraKeyWords.Location = New System.Drawing.Point(0, 0)
        Me.fraKeyWords.Name = "fraKeyWords"
        Me.fraKeyWords.Padding = New System.Windows.Forms.Padding(0)
        Me.fraKeyWords.Size = New System.Drawing.Size(286, 235)
        Me.fraKeyWords.TabIndex = 0
        Me.fraKeyWords.TabStop = False
        Me.fraKeyWords.Text = "Keywords"
        '
        'kweKeyWords
        '
        Me.kweKeyWords.AutoChange = True
        Me.kweKeyWords.AutoCompleteCacheName = "KeyWords"
        Me.kweKeyWords.AutoCompleteStable = Nothing
        Me.kweKeyWords.Dock = System.Windows.Forms.DockStyle.Fill
        Me.kweKeyWords.Enabled = False
        Me.kweKeyWords.Location = New System.Drawing.Point(0, 13)
        Me.kweKeyWords.Name = "kweKeyWords"
        Me.kweKeyWords.Size = New System.Drawing.Size(286, 222)
        '
        '
        '
        Me.kweKeyWords.Status.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.kweKeyWords.Status.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.kweKeyWords.Status.AutoChanged = False
        Me.kweKeyWords.Status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.kweKeyWords.Status.DeleteMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
        Me.kweKeyWords.Status.Location = New System.Drawing.Point(215, 0)
        Me.kweKeyWords.Status.Margin = New System.Windows.Forms.Padding(0)
        Me.kweKeyWords.Status.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.kweKeyWords.Status.Name = "stmStatus"
        Me.kweKeyWords.Status.ResetMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
        Me.kweKeyWords.Status.Size = New System.Drawing.Size(24, 24)
        Me.kweKeyWords.Status.TabIndex = 2
        Me.kweKeyWords.Status.TabStop = False
        Me.kweKeyWords.Synonyms = Nothing
        Me.kweKeyWords.TabIndex = 0
        '
        'tabChoices
        '
        Me.tabChoices.Controls.Add(Me.tapCommon)
        Me.tabChoices.Controls.Add(Me.tapAll)
        Me.tabChoices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabChoices.Enabled = False
        Me.tabChoices.Location = New System.Drawing.Point(0, 0)
        Me.tabChoices.Name = "tabChoices"
        Me.tabChoices.SelectedIndex = 0
        Me.tabChoices.Size = New System.Drawing.Size(322, 514)
        Me.tabChoices.TabIndex = 1
        Me.tabChoices.TabStop = False
        '
        'tapCommon
        '
        Me.tapCommon.BackColor = System.Drawing.SystemColors.Control
        Me.tapCommon.Controls.Add(Me.tlpItems)
        Me.tapCommon.Location = New System.Drawing.Point(4, 22)
        Me.tapCommon.Name = "tapCommon"
        Me.tapCommon.Padding = New System.Windows.Forms.Padding(3)
        Me.tapCommon.Size = New System.Drawing.Size(314, 488)
        Me.tapCommon.TabIndex = 0
        Me.tapCommon.Text = "Common"
        '
        'tlpItems
        '
        Me.tlpItems.AutoSize = True
        Me.tlpItems.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpItems.ColumnCount = 1
        Me.tlpItems.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpItems.Controls.Add(Me.fraTitle, 0, 3)
        Me.tlpItems.Controls.Add(Me.fraStatus, 0, 2)
        Me.tlpItems.Controls.Add(Me.fraLocation, 0, 1)
        Me.tlpItems.Controls.Add(Me.fraAuthor, 0, 0)
        Me.tlpItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpItems.Location = New System.Drawing.Point(3, 3)
        Me.tlpItems.Name = "tlpItems"
        Me.tlpItems.RowCount = 4
        Me.tlpItems.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpItems.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpItems.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpItems.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpItems.Size = New System.Drawing.Size(308, 482)
        Me.tlpItems.TabIndex = 4
        '
        'fraTitle
        '
        Me.fraTitle.AutoSize = True
        Me.fraTitle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.fraTitle.Controls.Add(Me.tlpTitle)
        Me.fraTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fraTitle.Location = New System.Drawing.Point(3, 274)
        Me.fraTitle.Name = "fraTitle"
        Me.fraTitle.Size = New System.Drawing.Size(302, 205)
        Me.fraTitle.TabIndex = 3
        Me.fraTitle.TabStop = False
        Me.fraTitle.Text = "Title"
        '
        'tlpTitle
        '
        Me.tlpTitle.AutoSize = True
        Me.tlpTitle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpTitle.ColumnCount = 2
        Me.tlpTitle.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpTitle.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpTitle.Controls.Add(Me.lblObjectName, 0, 0)
        Me.tlpTitle.Controls.Add(Me.txwObjectName, 1, 0)
        Me.tlpTitle.Controls.Add(Me.lblCaptionAbstract, 0, 1)
        Me.tlpTitle.Controls.Add(Me.mxwCaptionAbstract, 0, 2)
        Me.tlpTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpTitle.Location = New System.Drawing.Point(3, 16)
        Me.tlpTitle.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpTitle.Name = "tlpTitle"
        Me.tlpTitle.RowCount = 3
        Me.tlpTitle.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpTitle.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpTitle.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpTitle.Size = New System.Drawing.Size(296, 186)
        Me.tlpTitle.TabIndex = 0
        '
        'lblObjectName
        '
        Me.lblObjectName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblObjectName.AutoSize = True
        Me.lblObjectName.Location = New System.Drawing.Point(0, 5)
        Me.lblObjectName.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblObjectName.Name = "lblObjectName"
        Me.lblObjectName.Size = New System.Drawing.Size(69, 13)
        Me.lblObjectName.TabIndex = 0
        Me.lblObjectName.Text = "Object Name"
        '
        'txwObjectName
        '
        Me.txwObjectName.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txwObjectName.Location = New System.Drawing.Point(72, 0)
        Me.txwObjectName.Margin = New System.Windows.Forms.Padding(0)
        Me.txwObjectName.MinimumSize = New System.Drawing.Size(40, 20)
        Me.txwObjectName.Name = "txwObjectName"
        Me.txwObjectName.Size = New System.Drawing.Size(224, 24)
        '
        '
        '
        Me.txwObjectName.Status.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.txwObjectName.Status.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txwObjectName.Status.AutoChanged = False
        Me.txwObjectName.Status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.txwObjectName.Status.Location = New System.Drawing.Point(200, 0)
        Me.txwObjectName.Status.Margin = New System.Windows.Forms.Padding(0)
        Me.txwObjectName.Status.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.txwObjectName.Status.Name = "stmStatus"
        Me.txwObjectName.Status.ResetMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
        Me.txwObjectName.Status.Size = New System.Drawing.Size(24, 24)
        Me.txwObjectName.Status.TabIndex = 1
        Me.txwObjectName.Status.TabStop = False
        Me.txwObjectName.TabIndex = 1
        '
        'lblCaptionAbstract
        '
        Me.lblCaptionAbstract.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCaptionAbstract.AutoSize = True
        Me.tlpTitle.SetColumnSpan(Me.lblCaptionAbstract, 2)
        Me.lblCaptionAbstract.Location = New System.Drawing.Point(0, 24)
        Me.lblCaptionAbstract.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblCaptionAbstract.Name = "lblCaptionAbstract"
        Me.lblCaptionAbstract.Size = New System.Drawing.Size(87, 13)
        Me.lblCaptionAbstract.TabIndex = 2
        Me.lblCaptionAbstract.Text = "Caption/Abstract"
        '
        'mxwCaptionAbstract
        '
        Me.tlpTitle.SetColumnSpan(Me.mxwCaptionAbstract, 2)
        Me.mxwCaptionAbstract.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mxwCaptionAbstract.Location = New System.Drawing.Point(0, 37)
        Me.mxwCaptionAbstract.Margin = New System.Windows.Forms.Padding(0)
        Me.mxwCaptionAbstract.MinimumSize = New System.Drawing.Size(40, 40)
        Me.mxwCaptionAbstract.Name = "mxwCaptionAbstract"
        Me.mxwCaptionAbstract.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.mxwCaptionAbstract.Size = New System.Drawing.Size(296, 149)
        '
        '
        '
        Me.mxwCaptionAbstract.Status.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.mxwCaptionAbstract.Status.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.mxwCaptionAbstract.Status.AutoChanged = False
        Me.mxwCaptionAbstract.Status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.mxwCaptionAbstract.Status.Location = New System.Drawing.Point(272, 0)
        Me.mxwCaptionAbstract.Status.Margin = New System.Windows.Forms.Padding(0)
        Me.mxwCaptionAbstract.Status.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.mxwCaptionAbstract.Status.Name = "stmStatus"
        Me.mxwCaptionAbstract.Status.ResetMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
        Me.mxwCaptionAbstract.Status.Size = New System.Drawing.Size(24, 24)
        Me.mxwCaptionAbstract.Status.TabIndex = 1
        Me.mxwCaptionAbstract.Status.TabStop = False
        Me.mxwCaptionAbstract.TabIndex = 3
        '
        'fraStatus
        '
        Me.fraStatus.AutoSize = True
        Me.fraStatus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.fraStatus.Controls.Add(Me.tlpStatus)
        Me.fraStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fraStatus.Location = New System.Drawing.Point(3, 201)
        Me.fraStatus.Name = "fraStatus"
        Me.fraStatus.Size = New System.Drawing.Size(302, 67)
        Me.fraStatus.TabIndex = 2
        Me.fraStatus.TabStop = False
        Me.fraStatus.Text = "Status"
        '
        'tlpStatus
        '
        Me.tlpStatus.AutoSize = True
        Me.tlpStatus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpStatus.ColumnCount = 2
        Me.tlpStatus.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpStatus.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpStatus.Controls.Add(Me.lblEditStatus, 0, 0)
        Me.tlpStatus.Controls.Add(Me.txwEditStatus, 1, 0)
        Me.tlpStatus.Controls.Add(Me.lblUrgency, 0, 1)
        Me.tlpStatus.Controls.Add(Me.nwsUrgency, 1, 1)
        Me.tlpStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpStatus.Location = New System.Drawing.Point(3, 16)
        Me.tlpStatus.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpStatus.Name = "tlpStatus"
        Me.tlpStatus.RowCount = 2
        Me.tlpStatus.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpStatus.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpStatus.Size = New System.Drawing.Size(296, 48)
        Me.tlpStatus.TabIndex = 0
        '
        'lblEditStatus
        '
        Me.lblEditStatus.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblEditStatus.AutoSize = True
        Me.lblEditStatus.Location = New System.Drawing.Point(0, 5)
        Me.lblEditStatus.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblEditStatus.Name = "lblEditStatus"
        Me.lblEditStatus.Size = New System.Drawing.Size(58, 13)
        Me.lblEditStatus.TabIndex = 0
        Me.lblEditStatus.Text = "Edit Status"
        '
        'txwEditStatus
        '
        Me.txwEditStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txwEditStatus.Location = New System.Drawing.Point(61, 0)
        Me.txwEditStatus.Margin = New System.Windows.Forms.Padding(0)
        Me.txwEditStatus.MinimumSize = New System.Drawing.Size(40, 20)
        Me.txwEditStatus.Name = "txwEditStatus"
        Me.txwEditStatus.Size = New System.Drawing.Size(235, 24)
        '
        '
        '
        Me.txwEditStatus.Status.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.txwEditStatus.Status.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txwEditStatus.Status.AutoChanged = False
        Me.txwEditStatus.Status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.txwEditStatus.Status.Location = New System.Drawing.Point(211, 0)
        Me.txwEditStatus.Status.Margin = New System.Windows.Forms.Padding(0)
        Me.txwEditStatus.Status.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.txwEditStatus.Status.Name = "stmStatus"
        Me.txwEditStatus.Status.ResetMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
        Me.txwEditStatus.Status.Size = New System.Drawing.Size(24, 24)
        Me.txwEditStatus.Status.TabIndex = 1
        Me.txwEditStatus.Status.TabStop = False
        Me.txwEditStatus.TabIndex = 1
        '
        'lblUrgency
        '
        Me.lblUrgency.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblUrgency.AutoSize = True
        Me.lblUrgency.Location = New System.Drawing.Point(0, 29)
        Me.lblUrgency.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblUrgency.Name = "lblUrgency"
        Me.lblUrgency.Size = New System.Drawing.Size(47, 13)
        Me.lblUrgency.TabIndex = 2
        Me.lblUrgency.Text = "Urgency"
        '
        'nwsUrgency
        '
        Me.nwsUrgency.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nwsUrgency.Increment = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nwsUrgency.Location = New System.Drawing.Point(61, 24)
        Me.nwsUrgency.Margin = New System.Windows.Forms.Padding(0)
        Me.nwsUrgency.Maximum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.nwsUrgency.Minimum = New Decimal(New Integer() {0, 0, 0, 0})
        Me.nwsUrgency.MinimumSize = New System.Drawing.Size(40, 20)
        Me.nwsUrgency.Name = "nwsUrgency"
        Me.nwsUrgency.Size = New System.Drawing.Size(235, 24)
        '
        '
        '
        Me.nwsUrgency.Status.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.nwsUrgency.Status.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.nwsUrgency.Status.AutoChanged = False
        Me.nwsUrgency.Status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.nwsUrgency.Status.Location = New System.Drawing.Point(211, 0)
        Me.nwsUrgency.Status.Margin = New System.Windows.Forms.Padding(0)
        Me.nwsUrgency.Status.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.nwsUrgency.Status.Name = "stmStatus"
        Me.nwsUrgency.Status.ResetMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
        Me.nwsUrgency.Status.Size = New System.Drawing.Size(24, 24)
        Me.nwsUrgency.Status.TabIndex = 1
        Me.nwsUrgency.Status.TabStop = False
        Me.nwsUrgency.TabIndex = 3
        Me.nwsUrgency.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'fraLocation
        '
        Me.fraLocation.AutoSize = True
        Me.fraLocation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.fraLocation.Controls.Add(Me.tlpLocation)
        Me.fraLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fraLocation.Location = New System.Drawing.Point(3, 76)
        Me.fraLocation.Name = "fraLocation"
        Me.fraLocation.Size = New System.Drawing.Size(302, 119)
        Me.fraLocation.TabIndex = 1
        Me.fraLocation.TabStop = False
        Me.fraLocation.Text = "Location"
        '
        'tlpLocation
        '
        Me.tlpLocation.AutoSize = True
        Me.tlpLocation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpLocation.ColumnCount = 2
        Me.tlpLocation.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpLocation.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpLocation.Controls.Add(Me.lblCity, 0, 0)
        Me.tlpLocation.Controls.Add(Me.txwCity, 1, 0)
        Me.tlpLocation.Controls.Add(Me.lblCountryCode, 0, 1)
        Me.tlpLocation.Controls.Add(Me.cbwCountryCode, 1, 1)
        Me.tlpLocation.Controls.Add(Me.lblCountry, 0, 2)
        Me.tlpLocation.Controls.Add(Me.txwCountry, 1, 2)
        Me.tlpLocation.Controls.Add(Me.lblProvince, 0, 3)
        Me.tlpLocation.Controls.Add(Me.txwProvince, 1, 3)
        Me.tlpLocation.Controls.Add(Me.lblSubLocation, 0, 4)
        Me.tlpLocation.Controls.Add(Me.txwSubLocation, 1, 4)
        Me.tlpLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpLocation.Location = New System.Drawing.Point(3, 16)
        Me.tlpLocation.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpLocation.Name = "tlpLocation"
        Me.tlpLocation.RowCount = 5
        Me.tlpLocation.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpLocation.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpLocation.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpLocation.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpLocation.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpLocation.Size = New System.Drawing.Size(296, 100)
        Me.tlpLocation.TabIndex = 0
        '
        'lblCity
        '
        Me.lblCity.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCity.AutoSize = True
        Me.lblCity.Location = New System.Drawing.Point(0, 3)
        Me.lblCity.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(24, 13)
        Me.lblCity.TabIndex = 0
        Me.lblCity.Text = "City"
        '
        'txwCity
        '
        Me.txwCity.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txwCity.Location = New System.Drawing.Point(160, 0)
        Me.txwCity.Margin = New System.Windows.Forms.Padding(0)
        Me.txwCity.MinimumSize = New System.Drawing.Size(40, 20)
        Me.txwCity.Name = "txwCity"
        Me.txwCity.Size = New System.Drawing.Size(136, 20)
        '
        '
        '
        Me.txwCity.Status.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.txwCity.Status.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txwCity.Status.AutoChanged = False
        Me.txwCity.Status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.txwCity.Status.Location = New System.Drawing.Point(112, 0)
        Me.txwCity.Status.Margin = New System.Windows.Forms.Padding(0)
        Me.txwCity.Status.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.txwCity.Status.Name = "stmStatus"
        Me.txwCity.Status.ResetMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
        Me.txwCity.Status.Size = New System.Drawing.Size(24, 24)
        Me.txwCity.Status.TabIndex = 1
        Me.txwCity.Status.TabStop = False
        Me.txwCity.TabIndex = 1
        '
        'lblCountryCode
        '
        Me.lblCountryCode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCountryCode.AutoSize = True
        Me.lblCountryCode.Location = New System.Drawing.Point(0, 23)
        Me.lblCountryCode.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblCountryCode.Name = "lblCountryCode"
        Me.lblCountryCode.Size = New System.Drawing.Size(154, 13)
        Me.lblCountryCode.TabIndex = 2
        Me.lblCountryCode.Text = "Country/Primary Location Code"
        '
        'cbwCountryCode
        '
        Me.cbwCountryCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbwCountryCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
        Me.cbwCountryCode.Location = New System.Drawing.Point(160, 20)
        Me.cbwCountryCode.Margin = New System.Windows.Forms.Padding(0)
        Me.cbwCountryCode.MinimumSize = New System.Drawing.Size(40, 20)
        Me.cbwCountryCode.Name = "cbwCountryCode"
        Me.cbwCountryCode.Size = New System.Drawing.Size(136, 20)
        '
        '
        '
        Me.cbwCountryCode.Status.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.cbwCountryCode.Status.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cbwCountryCode.Status.AutoChanged = False
        Me.cbwCountryCode.Status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cbwCountryCode.Status.Location = New System.Drawing.Point(112, 0)
        Me.cbwCountryCode.Status.Margin = New System.Windows.Forms.Padding(0)
        Me.cbwCountryCode.Status.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.cbwCountryCode.Status.Name = "stmStatus"
        Me.cbwCountryCode.Status.ResetMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
        Me.cbwCountryCode.Status.Size = New System.Drawing.Size(24, 24)
        Me.cbwCountryCode.Status.TabIndex = 1
        Me.cbwCountryCode.Status.TabStop = False
        Me.cbwCountryCode.TabIndex = 3
        '
        'lblCountry
        '
        Me.lblCountry.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCountry.AutoSize = True
        Me.lblCountry.Location = New System.Drawing.Point(0, 43)
        Me.lblCountry.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblCountry.Name = "lblCountry"
        Me.lblCountry.Size = New System.Drawing.Size(157, 13)
        Me.lblCountry.TabIndex = 4
        Me.lblCountry.Text = "Country/Primary Location Name"
        '
        'txwCountry
        '
        Me.txwCountry.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txwCountry.Location = New System.Drawing.Point(160, 40)
        Me.txwCountry.Margin = New System.Windows.Forms.Padding(0)
        Me.txwCountry.MinimumSize = New System.Drawing.Size(40, 20)
        Me.txwCountry.Name = "txwCountry"
        Me.txwCountry.Size = New System.Drawing.Size(136, 20)
        '
        '
        '
        Me.txwCountry.Status.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.txwCountry.Status.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txwCountry.Status.AutoChanged = False
        Me.txwCountry.Status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.txwCountry.Status.Location = New System.Drawing.Point(112, 0)
        Me.txwCountry.Status.Margin = New System.Windows.Forms.Padding(0)
        Me.txwCountry.Status.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.txwCountry.Status.Name = "stmStatus"
        Me.txwCountry.Status.ResetMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
        Me.txwCountry.Status.Size = New System.Drawing.Size(24, 24)
        Me.txwCountry.Status.TabIndex = 1
        Me.txwCountry.Status.TabStop = False
        Me.txwCountry.TabIndex = 5
        '
        'lblProvince
        '
        Me.lblProvince.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblProvince.AutoSize = True
        Me.lblProvince.Location = New System.Drawing.Point(0, 63)
        Me.lblProvince.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblProvince.Name = "lblProvince"
        Me.lblProvince.Size = New System.Drawing.Size(79, 13)
        Me.lblProvince.TabIndex = 6
        Me.lblProvince.Text = "Province/State"
        '
        'txwProvince
        '
        Me.txwProvince.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txwProvince.Location = New System.Drawing.Point(160, 60)
        Me.txwProvince.Margin = New System.Windows.Forms.Padding(0)
        Me.txwProvince.MinimumSize = New System.Drawing.Size(40, 20)
        Me.txwProvince.Name = "txwProvince"
        Me.txwProvince.Size = New System.Drawing.Size(136, 20)
        '
        '
        '
        Me.txwProvince.Status.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.txwProvince.Status.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txwProvince.Status.AutoChanged = False
        Me.txwProvince.Status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.txwProvince.Status.Location = New System.Drawing.Point(112, 0)
        Me.txwProvince.Status.Margin = New System.Windows.Forms.Padding(0)
        Me.txwProvince.Status.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.txwProvince.Status.Name = "stmStatus"
        Me.txwProvince.Status.ResetMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
        Me.txwProvince.Status.Size = New System.Drawing.Size(24, 24)
        Me.txwProvince.Status.TabIndex = 1
        Me.txwProvince.Status.TabStop = False
        Me.txwProvince.TabIndex = 7
        '
        'lblSubLocation
        '
        Me.lblSubLocation.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSubLocation.AutoSize = True
        Me.lblSubLocation.Location = New System.Drawing.Point(0, 83)
        Me.lblSubLocation.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblSubLocation.Name = "lblSubLocation"
        Me.lblSubLocation.Size = New System.Drawing.Size(63, 13)
        Me.lblSubLocation.TabIndex = 8
        Me.lblSubLocation.Text = "Sublocation"
        '
        'txwSubLocation
        '
        Me.txwSubLocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txwSubLocation.Location = New System.Drawing.Point(160, 80)
        Me.txwSubLocation.Margin = New System.Windows.Forms.Padding(0)
        Me.txwSubLocation.MinimumSize = New System.Drawing.Size(40, 20)
        Me.txwSubLocation.Name = "txwSubLocation"
        Me.txwSubLocation.Size = New System.Drawing.Size(136, 20)
        '
        '
        '
        Me.txwSubLocation.Status.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.txwSubLocation.Status.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txwSubLocation.Status.AutoChanged = False
        Me.txwSubLocation.Status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.txwSubLocation.Status.Location = New System.Drawing.Point(112, 0)
        Me.txwSubLocation.Status.Margin = New System.Windows.Forms.Padding(0)
        Me.txwSubLocation.Status.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.txwSubLocation.Status.Name = "stmStatus"
        Me.txwSubLocation.Status.ResetMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
        Me.txwSubLocation.Status.Size = New System.Drawing.Size(24, 24)
        Me.txwSubLocation.Status.TabIndex = 1
        Me.txwSubLocation.Status.TabStop = False
        Me.txwSubLocation.TabIndex = 9
        '
        'fraAuthor
        '
        Me.fraAuthor.AutoSize = True
        Me.fraAuthor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.fraAuthor.Controls.Add(Me.tlpAuthor)
        Me.fraAuthor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fraAuthor.Location = New System.Drawing.Point(3, 3)
        Me.fraAuthor.Name = "fraAuthor"
        Me.fraAuthor.Size = New System.Drawing.Size(302, 67)
        Me.fraAuthor.TabIndex = 0
        Me.fraAuthor.TabStop = False
        Me.fraAuthor.Text = "Author"
        '
        'tlpAuthor
        '
        Me.tlpAuthor.AutoSize = True
        Me.tlpAuthor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpAuthor.ColumnCount = 2
        Me.tlpAuthor.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpAuthor.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpAuthor.Controls.Add(Me.lblCopyright, 0, 0)
        Me.tlpAuthor.Controls.Add(Me.txwCopyright, 1, 0)
        Me.tlpAuthor.Controls.Add(Me.lblCredit, 0, 1)
        Me.tlpAuthor.Controls.Add(Me.txwCredit, 1, 1)
        Me.tlpAuthor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpAuthor.Location = New System.Drawing.Point(3, 16)
        Me.tlpAuthor.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpAuthor.Name = "tlpAuthor"
        Me.tlpAuthor.RowCount = 2
        Me.tlpAuthor.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpAuthor.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpAuthor.Size = New System.Drawing.Size(296, 48)
        Me.tlpAuthor.TabIndex = 0
        '
        'lblCopyright
        '
        Me.lblCopyright.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCopyright.AutoSize = True
        Me.lblCopyright.Location = New System.Drawing.Point(0, 5)
        Me.lblCopyright.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(85, 13)
        Me.lblCopyright.TabIndex = 0
        Me.lblCopyright.Text = "Copyright Notice"
        '
        'txwCopyright
        '
        Me.txwCopyright.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txwCopyright.Location = New System.Drawing.Point(88, 0)
        Me.txwCopyright.Margin = New System.Windows.Forms.Padding(0)
        Me.txwCopyright.MinimumSize = New System.Drawing.Size(40, 20)
        Me.txwCopyright.Name = "txwCopyright"
        Me.txwCopyright.Size = New System.Drawing.Size(208, 24)
        '
        '
        '
        Me.txwCopyright.Status.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.txwCopyright.Status.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txwCopyright.Status.AutoChanged = False
        Me.txwCopyright.Status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.txwCopyright.Status.Location = New System.Drawing.Point(184, 0)
        Me.txwCopyright.Status.Margin = New System.Windows.Forms.Padding(0)
        Me.txwCopyright.Status.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.txwCopyright.Status.Name = "stmStatus"
        Me.txwCopyright.Status.ResetMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
        Me.txwCopyright.Status.Size = New System.Drawing.Size(24, 24)
        Me.txwCopyright.Status.TabIndex = 1
        Me.txwCopyright.Status.TabStop = False
        Me.txwCopyright.TabIndex = 1
        '
        'lblCredit
        '
        Me.lblCredit.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCredit.AutoSize = True
        Me.lblCredit.Location = New System.Drawing.Point(0, 29)
        Me.lblCredit.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblCredit.Name = "lblCredit"
        Me.lblCredit.Size = New System.Drawing.Size(34, 13)
        Me.lblCredit.TabIndex = 2
        Me.lblCredit.Text = "Credit"
        '
        'txwCredit
        '
        Me.txwCredit.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txwCredit.Location = New System.Drawing.Point(88, 24)
        Me.txwCredit.Margin = New System.Windows.Forms.Padding(0)
        Me.txwCredit.MinimumSize = New System.Drawing.Size(40, 20)
        Me.txwCredit.Name = "txwCredit"
        Me.txwCredit.Size = New System.Drawing.Size(208, 24)
        '
        '
        '
        Me.txwCredit.Status.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.txwCredit.Status.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txwCredit.Status.AutoChanged = False
        Me.txwCredit.Status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.txwCredit.Status.Location = New System.Drawing.Point(184, 0)
        Me.txwCredit.Status.Margin = New System.Windows.Forms.Padding(0)
        Me.txwCredit.Status.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.txwCredit.Status.Name = "stmStatus"
        Me.txwCredit.Status.ResetMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
        Me.txwCredit.Status.Size = New System.Drawing.Size(24, 24)
        Me.txwCredit.Status.TabIndex = 1
        Me.txwCredit.Status.TabStop = False
        Me.txwCredit.TabIndex = 3
        '
        'tapAll
        '
        Me.tapAll.Controls.Add(Me.prgAll)
        Me.tapAll.Location = New System.Drawing.Point(4, 22)
        Me.tapAll.Name = "tapAll"
        Me.tapAll.Padding = New System.Windows.Forms.Padding(3)
        Me.tapAll.Size = New System.Drawing.Size(314, 488)
        Me.tapAll.TabIndex = 1
        Me.tapAll.Text = "All"
        Me.tapAll.UseVisualStyleBackColor = True
        '
        'prgAll
        '
        Me.prgAll.Dock = System.Windows.Forms.DockStyle.Fill
        Me.prgAll.Location = New System.Drawing.Point(3, 3)
        Me.prgAll.Name = "prgAll"
        Me.prgAll.Size = New System.Drawing.Size(308, 482)
        Me.prgAll.TabIndex = 0
        '
        'fbdBrowse
        '
        Me.fbdBrowse.Description = "Select folder to show images from"
        '
        'bgwThumb
        '
        Me.bgwThumb.WorkerReportsProgress = True
        Me.bgwThumb.WorkerSupportsCancellation = True
        '
        'stsStatus
        '
        Me.stsStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsgLoadImages, Me.tslChange})
        Me.stsStatus.Location = New System.Drawing.Point(0, 514)
        Me.stsStatus.Name = "stsStatus"
        Me.stsStatus.Size = New System.Drawing.Size(846, 22)
        Me.stsStatus.TabIndex = 1
        Me.stsStatus.Text = "StatusStrip1"
        '
        'tsgLoadImages
        '
        Me.tsgLoadImages.Name = "tsgLoadImages"
        Me.tsgLoadImages.Size = New System.Drawing.Size(100, 16)
        '
        'tslChange
        '
        Me.tslChange.Name = "tslChange"
        Me.tslChange.Size = New System.Drawing.Size(0, 17)
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(846, 536)
        Me.Controls.Add(Me.splMain)
        Me.Controls.Add(Me.stsStatus)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmMain"
        Me.Text = "Metanol"
        Me.splMain.Panel1.ResumeLayout(False)
        Me.splMain.Panel1.PerformLayout()
        Me.splMain.Panel2.ResumeLayout(False)
        Me.splMain.ResumeLayout(False)
        Me.splFolder.Panel1.ResumeLayout(False)
        Me.splFolder.Panel2.ResumeLayout(False)
        Me.splFolder.ResumeLayout(False)
        Me.tlpPath.ResumeLayout(False)
        Me.tlpPath.PerformLayout()
        Me.splHorizontal.Panel1.ResumeLayout(False)
        Me.splHorizontal.Panel2.ResumeLayout(False)
        Me.splHorizontal.ResumeLayout(False)
        Me.splVertical.Panel1.ResumeLayout(False)
        Me.splVertical.Panel1.PerformLayout()
        Me.splVertical.Panel2.ResumeLayout(False)
        Me.splVertical.ResumeLayout(False)
        CType(Me.picPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraKeyWords.ResumeLayout(False)
        Me.tabChoices.ResumeLayout(False)
        Me.tapCommon.ResumeLayout(False)
        Me.tapCommon.PerformLayout()
        Me.tlpItems.ResumeLayout(False)
        Me.tlpItems.PerformLayout()
        Me.fraTitle.ResumeLayout(False)
        Me.fraTitle.PerformLayout()
        Me.tlpTitle.ResumeLayout(False)
        Me.tlpTitle.PerformLayout()
        Me.fraStatus.ResumeLayout(False)
        Me.fraStatus.PerformLayout()
        Me.tlpStatus.ResumeLayout(False)
        Me.tlpStatus.PerformLayout()
        Me.fraLocation.ResumeLayout(False)
        Me.fraLocation.PerformLayout()
        Me.tlpLocation.ResumeLayout(False)
        Me.tlpLocation.PerformLayout()
        Me.fraAuthor.ResumeLayout(False)
        Me.fraAuthor.PerformLayout()
        Me.tlpAuthor.ResumeLayout(False)
        Me.tlpAuthor.PerformLayout()
        Me.tapAll.ResumeLayout(False)
        Me.stsStatus.ResumeLayout(False)
        Me.stsStatus.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents splMain As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents fbdBrowse As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents tlpPath As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmdGo As System.Windows.Forms.Button
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents splFolder As System.Windows.Forms.SplitContainer
    Friend WithEvents lvwImages As System.Windows.Forms.ListView
    Friend WithEvents lvwFolder As System.Windows.Forms.ListView
    Friend WithEvents imlFolder As System.Windows.Forms.ImageList
    Friend WithEvents imlImages As System.Windows.Forms.ImageList
    Friend WithEvents bgwThumb As System.ComponentModel.BackgroundWorker
    Friend WithEvents stsStatus As System.Windows.Forms.StatusStrip
    Friend WithEvents tsgLoadImages As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents splHorizontal As System.Windows.Forms.SplitContainer
    Friend WithEvents splVertical As System.Windows.Forms.SplitContainer
    Friend WithEvents picPreview As System.Windows.Forms.PictureBox
    Friend WithEvents cmdLarge As System.Windows.Forms.Button
    Friend WithEvents fraKeyWords As System.Windows.Forms.GroupBox
    Friend WithEvents kweKeyWords As Tools.WindowsT.FormsT.KeyWordsEditor
    Friend WithEvents tabChoices As System.Windows.Forms.TabControl
    Friend WithEvents tapCommon As System.Windows.Forms.TabPage
    Friend WithEvents tapAll As System.Windows.Forms.TabPage
    Friend WithEvents prgAll As System.Windows.Forms.PropertyGrid
    Friend WithEvents fraLocation As System.Windows.Forms.GroupBox
    Friend WithEvents tlpLocation As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblCity As System.Windows.Forms.Label
    Friend WithEvents txwCity As Tools.WindowsT.FormsT.TextBoxWithStatus
    Friend WithEvents lblCountryCode As System.Windows.Forms.Label
    Friend WithEvents cbwCountryCode As Tools.WindowsT.FormsT.ComboWithStatus
    Friend WithEvents lblCountry As System.Windows.Forms.Label
    Friend WithEvents txwCountry As Tools.WindowsT.FormsT.TextBoxWithStatus
    Friend WithEvents lblProvince As System.Windows.Forms.Label
    Friend WithEvents fraAuthor As System.Windows.Forms.GroupBox
    Friend WithEvents tlpAuthor As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblCopyright As System.Windows.Forms.Label
    Friend WithEvents txwCopyright As Tools.WindowsT.FormsT.TextBoxWithStatus
    Friend WithEvents lblCredit As System.Windows.Forms.Label
    Friend WithEvents txwCredit As Tools.WindowsT.FormsT.TextBoxWithStatus
    Friend WithEvents fraStatus As System.Windows.Forms.GroupBox
    Friend WithEvents tlpStatus As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblEditStatus As System.Windows.Forms.Label
    Friend WithEvents txwEditStatus As Tools.WindowsT.FormsT.TextBoxWithStatus
    Friend WithEvents lblUrgency As System.Windows.Forms.Label
    Friend WithEvents txwProvince As Tools.WindowsT.FormsT.TextBoxWithStatus
    Friend WithEvents lblSubLocation As System.Windows.Forms.Label
    Friend WithEvents txwSubLocation As Tools.WindowsT.FormsT.TextBoxWithStatus
    Friend WithEvents nwsUrgency As Tools.WindowsT.FormsT.NumericUpDownWithStatus
    Friend WithEvents tlpItems As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents fraTitle As System.Windows.Forms.GroupBox
    Friend WithEvents tlpTitle As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblObjectName As System.Windows.Forms.Label
    Friend WithEvents txwObjectName As Tools.WindowsT.FormsT.TextBoxWithStatus
    Friend WithEvents lblCaptionAbstract As System.Windows.Forms.Label
    Friend WithEvents mxwCaptionAbstract As Tools.WindowsT.FormsT.MultiLineTextBoxWithStatus
    Friend WithEvents tslChange As System.Windows.Forms.ToolStripStatusLabel

End Class
