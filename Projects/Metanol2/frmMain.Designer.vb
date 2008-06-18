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
        Me.tabInfo = New System.Windows.Forms.TabControl
        Me.tapCommon = New System.Windows.Forms.TabPage
        Me.flpCommon = New System.Windows.Forms.FlowLayoutPanel
        Me.panImage = New System.Windows.Forms.Panel
        Me.picPreview = New System.Windows.Forms.PictureBox
        Me.llbLarge = New System.Windows.Forms.LinkLabel
        Me.cmdErrInfo = New System.Windows.Forms.Button
        Me.sptImage = New System.Windows.Forms.Splitter
        Me.fraKeywords = New System.Windows.Forms.GroupBox
        Me.sptKeywords = New System.Windows.Forms.Splitter
        Me.fraAuthor = New System.Windows.Forms.GroupBox
        Me.tlpAuthor = New System.Windows.Forms.TableLayoutPanel
        Me.lblCopyright = New System.Windows.Forms.Label
        Me.txtCopyright = New System.Windows.Forms.TextBox
        Me.lblCredit = New System.Windows.Forms.Label
        Me.txtCredit = New System.Windows.Forms.TextBox
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
        Me.fraStatus = New System.Windows.Forms.GroupBox
        Me.tlpStatus = New System.Windows.Forms.TableLayoutPanel
        Me.lblEditStatus = New System.Windows.Forms.Label
        Me.txtEditStatus = New System.Windows.Forms.TextBox
        Me.lblUrgency = New System.Windows.Forms.Label
        Me.nudUrgency = New System.Windows.Forms.NumericUpDown
        Me.fraTitle = New System.Windows.Forms.GroupBox
        Me.tlpTitle = New System.Windows.Forms.TableLayoutPanel
        Me.lblObjectName = New System.Windows.Forms.Label
        Me.txtObjectName = New System.Windows.Forms.TextBox
        Me.lblCaption = New System.Windows.Forms.Label
        Me.txtCaption = New System.Windows.Forms.TextBox
        Me.sptTitle = New System.Windows.Forms.Splitter
        Me.tapIPTC = New System.Windows.Forms.TabPage
        Me.prgIPTC = New System.Windows.Forms.PropertyGrid
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
        Me.tmiTools = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiOptions = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiHelp = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiAbout = New System.Windows.Forms.ToolStripMenuItem
        Me.tosMain = New System.Windows.Forms.ToolStrip
        Me.tsbBack = New System.Windows.Forms.ToolStripButton
        Me.tsbForward = New System.Windows.Forms.ToolStripButton
        Me.tsbRefresh = New System.Windows.Forms.ToolStripButton
        Me.fbdGoTo = New System.Windows.Forms.FolderBrowserDialog
        Me.kweKeywords = New Tools.WindowsT.FormsT.KeyWordsEditor
        Me.splMain.Panel1.SuspendLayout()
        Me.splMain.Panel2.SuspendLayout()
        Me.splMain.SuspendLayout()
        Me.splBrowser.Panel1.SuspendLayout()
        Me.splBrowser.Panel2.SuspendLayout()
        Me.splBrowser.SuspendLayout()
        Me.tabInfo.SuspendLayout()
        Me.tapCommon.SuspendLayout()
        Me.flpCommon.SuspendLayout()
        Me.panImage.SuspendLayout()
        CType(Me.picPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraKeywords.SuspendLayout()
        Me.fraAuthor.SuspendLayout()
        Me.tlpAuthor.SuspendLayout()
        Me.fraLocation.SuspendLayout()
        Me.tlpLocation.SuspendLayout()
        Me.fraStatus.SuspendLayout()
        Me.tlpStatus.SuspendLayout()
        CType(Me.nudUrgency, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraTitle.SuspendLayout()
        Me.tlpTitle.SuspendLayout()
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
        Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splMain.Location = New System.Drawing.Point(0, 0)
        Me.splMain.Name = "splMain"
        '
        'splMain.Panel1
        '
        Me.splMain.Panel1.Controls.Add(Me.splBrowser)
        '
        'splMain.Panel2
        '
        Me.splMain.Panel2.Controls.Add(Me.tabInfo)
        Me.splMain.Panel2.Enabled = False
        Me.splMain.Size = New System.Drawing.Size(720, 425)
        Me.splMain.SplitterDistance = 231
        Me.splMain.TabIndex = 0
        Me.splMain.TabStop = False
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
        Me.splBrowser.Size = New System.Drawing.Size(231, 425)
        Me.splBrowser.SplitterDistance = 143
        Me.splBrowser.TabIndex = 0
        Me.splBrowser.TabStop = False
        '
        'lvwFolders
        '
        Me.lvwFolders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwFolders.Location = New System.Drawing.Point(0, 0)
        Me.lvwFolders.MultiSelect = False
        Me.lvwFolders.Name = "lvwFolders"
        Me.lvwFolders.Size = New System.Drawing.Size(231, 143)
        Me.lvwFolders.SmallImageList = Me.imlFolders
        Me.lvwFolders.TabIndex = 2
        Me.lvwFolders.TabStop = False
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
        Me.lvwImages.Size = New System.Drawing.Size(231, 278)
        Me.lvwImages.TabIndex = 0
        Me.lvwImages.TabStop = False
        Me.lvwImages.UseCompatibleStateImageBehavior = False
        '
        'imlImages
        '
        Me.imlImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.imlImages.ImageSize = New System.Drawing.Size(16, 16)
        Me.imlImages.TransparentColor = System.Drawing.Color.Transparent
        '
        'tabInfo
        '
        Me.tabInfo.Controls.Add(Me.tapCommon)
        Me.tabInfo.Controls.Add(Me.tapIPTC)
        Me.tabInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabInfo.Location = New System.Drawing.Point(0, 0)
        Me.tabInfo.Name = "tabInfo"
        Me.tabInfo.SelectedIndex = 0
        Me.tabInfo.Size = New System.Drawing.Size(485, 425)
        Me.tabInfo.TabIndex = 0
        Me.tabInfo.TabStop = False
        '
        'tapCommon
        '
        Me.tapCommon.Controls.Add(Me.flpCommon)
        Me.tapCommon.Location = New System.Drawing.Point(4, 22)
        Me.tapCommon.Name = "tapCommon"
        Me.tapCommon.Padding = New System.Windows.Forms.Padding(3)
        Me.tapCommon.Size = New System.Drawing.Size(477, 399)
        Me.tapCommon.TabIndex = 0
        Me.tapCommon.Text = "Common"
        Me.tapCommon.UseVisualStyleBackColor = True
        '
        'flpCommon
        '
        Me.flpCommon.Controls.Add(Me.panImage)
        Me.flpCommon.Controls.Add(Me.sptImage)
        Me.flpCommon.Controls.Add(Me.fraKeywords)
        Me.flpCommon.Controls.Add(Me.sptKeywords)
        Me.flpCommon.Controls.Add(Me.fraAuthor)
        Me.flpCommon.Controls.Add(Me.fraLocation)
        Me.flpCommon.Controls.Add(Me.fraStatus)
        Me.flpCommon.Controls.Add(Me.fraTitle)
        Me.flpCommon.Controls.Add(Me.sptTitle)
        Me.flpCommon.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpCommon.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flpCommon.Location = New System.Drawing.Point(3, 3)
        Me.flpCommon.Margin = New System.Windows.Forms.Padding(0)
        Me.flpCommon.Name = "flpCommon"
        Me.flpCommon.Size = New System.Drawing.Size(471, 393)
        Me.flpCommon.TabIndex = 0
        '
        'panImage
        '
        Me.panImage.Controls.Add(Me.picPreview)
        Me.panImage.Controls.Add(Me.llbLarge)
        Me.panImage.Controls.Add(Me.cmdErrInfo)
        Me.panImage.Location = New System.Drawing.Point(0, 0)
        Me.panImage.Margin = New System.Windows.Forms.Padding(0)
        Me.panImage.MinimumSize = New System.Drawing.Size(0, 32)
        Me.panImage.Name = "panImage"
        Me.panImage.Size = New System.Drawing.Size(157, 100)
        Me.panImage.TabIndex = 0
        '
        'picPreview
        '
        Me.picPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picPreview.Location = New System.Drawing.Point(0, 0)
        Me.picPreview.Margin = New System.Windows.Forms.Padding(0)
        Me.picPreview.Name = "picPreview"
        Me.picPreview.Size = New System.Drawing.Size(157, 100)
        Me.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picPreview.TabIndex = 2
        Me.picPreview.TabStop = False
        '
        'llbLarge
        '
        Me.llbLarge.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.llbLarge.AutoSize = True
        Me.llbLarge.Location = New System.Drawing.Point(123, 87)
        Me.llbLarge.Name = "llbLarge"
        Me.llbLarge.Size = New System.Drawing.Size(34, 13)
        Me.llbLarge.TabIndex = 0
        Me.llbLarge.TabStop = True
        Me.llbLarge.Text = "Large"
        '
        'cmdErrInfo
        '
        Me.cmdErrInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.cmdErrInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmdErrInfo.FlatAppearance.BorderSize = 0
        Me.cmdErrInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdErrInfo.Image = Global.Tools.Metanol.My.Resources.Resources.Symbol_Delete
        Me.cmdErrInfo.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.cmdErrInfo.Location = New System.Drawing.Point(0, 0)
        Me.cmdErrInfo.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdErrInfo.Name = "cmdErrInfo"
        Me.cmdErrInfo.Size = New System.Drawing.Size(157, 100)
        Me.cmdErrInfo.TabIndex = 1
        Me.cmdErrInfo.TabStop = False
        Me.cmdErrInfo.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.cmdErrInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.cmdErrInfo.UseVisualStyleBackColor = True
        Me.cmdErrInfo.Visible = False
        '
        'sptImage
        '
        Me.sptImage.Cursor = System.Windows.Forms.Cursors.HSplit
        Me.sptImage.Location = New System.Drawing.Point(0, 100)
        Me.sptImage.Margin = New System.Windows.Forms.Padding(0)
        Me.sptImage.Name = "sptImage"
        Me.sptImage.Size = New System.Drawing.Size(3, 3)
        Me.sptImage.TabIndex = 1
        Me.sptImage.TabStop = False
        '
        'fraKeywords
        '
        Me.fraKeywords.Controls.Add(Me.kweKeywords)
        Me.fraKeywords.Location = New System.Drawing.Point(0, 103)
        Me.fraKeywords.Margin = New System.Windows.Forms.Padding(0)
        Me.fraKeywords.MinimumSize = New System.Drawing.Size(0, 100)
        Me.fraKeywords.Name = "fraKeywords"
        Me.fraKeywords.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.fraKeywords.Size = New System.Drawing.Size(160, 100)
        Me.fraKeywords.TabIndex = 2
        Me.fraKeywords.TabStop = False
        Me.fraKeywords.Text = "Keywords"
        '
        'sptKeywords
        '
        Me.sptKeywords.Cursor = System.Windows.Forms.Cursors.HSplit
        Me.sptKeywords.Location = New System.Drawing.Point(0, 203)
        Me.sptKeywords.Margin = New System.Windows.Forms.Padding(0)
        Me.sptKeywords.Name = "sptKeywords"
        Me.sptKeywords.Size = New System.Drawing.Size(3, 3)
        Me.sptKeywords.TabIndex = 3
        Me.sptKeywords.TabStop = False
        '
        'fraAuthor
        '
        Me.fraAuthor.AutoSize = True
        Me.fraAuthor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.fraAuthor.Controls.Add(Me.tlpAuthor)
        Me.fraAuthor.Location = New System.Drawing.Point(0, 206)
        Me.fraAuthor.Margin = New System.Windows.Forms.Padding(0)
        Me.fraAuthor.Name = "fraAuthor"
        Me.fraAuthor.Padding = New System.Windows.Forms.Padding(0, 0, 2, 3)
        Me.fraAuthor.Size = New System.Drawing.Size(153, 56)
        Me.fraAuthor.TabIndex = 4
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
        Me.tlpAuthor.Controls.Add(Me.txtCopyright, 1, 0)
        Me.tlpAuthor.Controls.Add(Me.lblCredit, 0, 1)
        Me.tlpAuthor.Controls.Add(Me.txtCredit, 1, 1)
        Me.tlpAuthor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpAuthor.Location = New System.Drawing.Point(0, 13)
        Me.tlpAuthor.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpAuthor.Name = "tlpAuthor"
        Me.tlpAuthor.RowCount = 2
        Me.tlpAuthor.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpAuthor.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpAuthor.Size = New System.Drawing.Size(151, 40)
        Me.tlpAuthor.TabIndex = 0
        '
        'lblCopyright
        '
        Me.lblCopyright.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCopyright.AutoSize = True
        Me.lblCopyright.Location = New System.Drawing.Point(0, 3)
        Me.lblCopyright.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(51, 13)
        Me.lblCopyright.TabIndex = 0
        Me.lblCopyright.Text = "Copyright"
        '
        'txtCopyright
        '
        Me.txtCopyright.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCopyright.Location = New System.Drawing.Point(51, 0)
        Me.txtCopyright.Margin = New System.Windows.Forms.Padding(0)
        Me.txtCopyright.Name = "txtCopyright"
        Me.txtCopyright.Size = New System.Drawing.Size(100, 20)
        Me.txtCopyright.TabIndex = 1
        '
        'lblCredit
        '
        Me.lblCredit.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCredit.AutoSize = True
        Me.lblCredit.Location = New System.Drawing.Point(0, 23)
        Me.lblCredit.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCredit.Name = "lblCredit"
        Me.lblCredit.Size = New System.Drawing.Size(34, 13)
        Me.lblCredit.TabIndex = 2
        Me.lblCredit.Text = "Credit"
        '
        'txtCredit
        '
        Me.txtCredit.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCredit.Location = New System.Drawing.Point(51, 20)
        Me.txtCredit.Margin = New System.Windows.Forms.Padding(0)
        Me.txtCredit.Name = "txtCredit"
        Me.txtCredit.Size = New System.Drawing.Size(100, 20)
        Me.txtCredit.TabIndex = 3
        '
        'fraLocation
        '
        Me.fraLocation.AutoSize = True
        Me.fraLocation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.fraLocation.Controls.Add(Me.tlpLocation)
        Me.fraLocation.Location = New System.Drawing.Point(0, 262)
        Me.fraLocation.Margin = New System.Windows.Forms.Padding(0)
        Me.fraLocation.Name = "fraLocation"
        Me.fraLocation.Padding = New System.Windows.Forms.Padding(0, 0, 2, 3)
        Me.fraLocation.Size = New System.Drawing.Size(202, 117)
        Me.fraLocation.TabIndex = 5
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
        Me.tlpLocation.Controls.Add(Me.txtCity, 1, 0)
        Me.tlpLocation.Controls.Add(Me.lblCountryCode, 0, 1)
        Me.tlpLocation.Controls.Add(Me.cmbCountryCode, 1, 1)
        Me.tlpLocation.Controls.Add(Me.lblCountry, 0, 2)
        Me.tlpLocation.Controls.Add(Me.txtCountry, 1, 2)
        Me.tlpLocation.Controls.Add(Me.lblProvince, 0, 3)
        Me.tlpLocation.Controls.Add(Me.txtProvince, 1, 3)
        Me.tlpLocation.Controls.Add(Me.lblSublocation, 0, 4)
        Me.tlpLocation.Controls.Add(Me.txtSublocation, 1, 4)
        Me.tlpLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpLocation.Location = New System.Drawing.Point(0, 13)
        Me.tlpLocation.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpLocation.Name = "tlpLocation"
        Me.tlpLocation.RowCount = 5
        Me.tlpLocation.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpLocation.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpLocation.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpLocation.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpLocation.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpLocation.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpLocation.Size = New System.Drawing.Size(200, 101)
        Me.tlpLocation.TabIndex = 1
        '
        'lblCity
        '
        Me.lblCity.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCity.AutoSize = True
        Me.lblCity.Location = New System.Drawing.Point(0, 3)
        Me.lblCity.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(24, 13)
        Me.lblCity.TabIndex = 0
        Me.lblCity.Text = "City"
        '
        'txtCity
        '
        Me.txtCity.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCity.Location = New System.Drawing.Point(79, 0)
        Me.txtCity.Margin = New System.Windows.Forms.Padding(0)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(121, 20)
        Me.txtCity.TabIndex = 1
        '
        'lblCountryCode
        '
        Me.lblCountryCode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCountryCode.AutoSize = True
        Me.lblCountryCode.Location = New System.Drawing.Point(0, 24)
        Me.lblCountryCode.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCountryCode.Name = "lblCountryCode"
        Me.lblCountryCode.Size = New System.Drawing.Size(65, 13)
        Me.lblCountryCode.TabIndex = 4
        Me.lblCountryCode.Text = "Contry Code"
        '
        'cmbCountryCode
        '
        Me.cmbCountryCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbCountryCode.FormattingEnabled = True
        Me.cmbCountryCode.Location = New System.Drawing.Point(79, 20)
        Me.cmbCountryCode.Margin = New System.Windows.Forms.Padding(0)
        Me.cmbCountryCode.Name = "cmbCountryCode"
        Me.cmbCountryCode.Size = New System.Drawing.Size(121, 21)
        Me.cmbCountryCode.TabIndex = 5
        '
        'lblCountry
        '
        Me.lblCountry.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCountry.AutoSize = True
        Me.lblCountry.Location = New System.Drawing.Point(0, 44)
        Me.lblCountry.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCountry.Name = "lblCountry"
        Me.lblCountry.Size = New System.Drawing.Size(43, 13)
        Me.lblCountry.TabIndex = 6
        Me.lblCountry.Text = "Country"
        '
        'txtCountry
        '
        Me.txtCountry.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCountry.Location = New System.Drawing.Point(79, 41)
        Me.txtCountry.Margin = New System.Windows.Forms.Padding(0)
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Size = New System.Drawing.Size(121, 20)
        Me.txtCountry.TabIndex = 7
        '
        'lblProvince
        '
        Me.lblProvince.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblProvince.AutoSize = True
        Me.lblProvince.Location = New System.Drawing.Point(0, 64)
        Me.lblProvince.Margin = New System.Windows.Forms.Padding(0)
        Me.lblProvince.Name = "lblProvince"
        Me.lblProvince.Size = New System.Drawing.Size(79, 13)
        Me.lblProvince.TabIndex = 8
        Me.lblProvince.Text = "Province/State"
        '
        'txtProvince
        '
        Me.txtProvince.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtProvince.Location = New System.Drawing.Point(79, 61)
        Me.txtProvince.Margin = New System.Windows.Forms.Padding(0)
        Me.txtProvince.Name = "txtProvince"
        Me.txtProvince.Size = New System.Drawing.Size(121, 20)
        Me.txtProvince.TabIndex = 9
        '
        'lblSublocation
        '
        Me.lblSublocation.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblSublocation.AutoSize = True
        Me.lblSublocation.Location = New System.Drawing.Point(0, 84)
        Me.lblSublocation.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSublocation.Name = "lblSublocation"
        Me.lblSublocation.Size = New System.Drawing.Size(63, 13)
        Me.lblSublocation.TabIndex = 10
        Me.lblSublocation.Text = "Sublocation"
        '
        'txtSublocation
        '
        Me.txtSublocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSublocation.Location = New System.Drawing.Point(79, 81)
        Me.txtSublocation.Margin = New System.Windows.Forms.Padding(0)
        Me.txtSublocation.Name = "txtSublocation"
        Me.txtSublocation.Size = New System.Drawing.Size(121, 20)
        Me.txtSublocation.TabIndex = 11
        '
        'fraStatus
        '
        Me.fraStatus.AutoSize = True
        Me.fraStatus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.fraStatus.Controls.Add(Me.tlpStatus)
        Me.fraStatus.Location = New System.Drawing.Point(202, 0)
        Me.fraStatus.Margin = New System.Windows.Forms.Padding(0)
        Me.fraStatus.Name = "fraStatus"
        Me.fraStatus.Padding = New System.Windows.Forms.Padding(0, 0, 2, 3)
        Me.fraStatus.Size = New System.Drawing.Size(178, 56)
        Me.fraStatus.TabIndex = 6
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
        Me.tlpStatus.Controls.Add(Me.txtEditStatus, 1, 0)
        Me.tlpStatus.Controls.Add(Me.lblUrgency, 0, 1)
        Me.tlpStatus.Controls.Add(Me.nudUrgency, 1, 1)
        Me.tlpStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpStatus.Location = New System.Drawing.Point(0, 13)
        Me.tlpStatus.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpStatus.Name = "tlpStatus"
        Me.tlpStatus.RowCount = 2
        Me.tlpStatus.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpStatus.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpStatus.Size = New System.Drawing.Size(176, 40)
        Me.tlpStatus.TabIndex = 1
        '
        'lblEditStatus
        '
        Me.lblEditStatus.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblEditStatus.AutoSize = True
        Me.lblEditStatus.Location = New System.Drawing.Point(0, 3)
        Me.lblEditStatus.Margin = New System.Windows.Forms.Padding(0)
        Me.lblEditStatus.Name = "lblEditStatus"
        Me.lblEditStatus.Size = New System.Drawing.Size(56, 13)
        Me.lblEditStatus.TabIndex = 0
        Me.lblEditStatus.Text = "Edit status"
        '
        'txtEditStatus
        '
        Me.txtEditStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEditStatus.Location = New System.Drawing.Point(56, 0)
        Me.txtEditStatus.Margin = New System.Windows.Forms.Padding(0)
        Me.txtEditStatus.Name = "txtEditStatus"
        Me.txtEditStatus.Size = New System.Drawing.Size(120, 20)
        Me.txtEditStatus.TabIndex = 1
        '
        'lblUrgency
        '
        Me.lblUrgency.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblUrgency.AutoSize = True
        Me.lblUrgency.Location = New System.Drawing.Point(0, 23)
        Me.lblUrgency.Margin = New System.Windows.Forms.Padding(0)
        Me.lblUrgency.Name = "lblUrgency"
        Me.lblUrgency.Size = New System.Drawing.Size(47, 13)
        Me.lblUrgency.TabIndex = 2
        Me.lblUrgency.Text = "Urgency"
        '
        'nudUrgency
        '
        Me.nudUrgency.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nudUrgency.Location = New System.Drawing.Point(56, 20)
        Me.nudUrgency.Margin = New System.Windows.Forms.Padding(0)
        Me.nudUrgency.Name = "nudUrgency"
        Me.nudUrgency.Size = New System.Drawing.Size(120, 20)
        Me.nudUrgency.TabIndex = 3
        '
        'fraTitle
        '
        Me.fraTitle.AutoSize = True
        Me.fraTitle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.fraTitle.Controls.Add(Me.tlpTitle)
        Me.fraTitle.Location = New System.Drawing.Point(202, 56)
        Me.fraTitle.Margin = New System.Windows.Forms.Padding(0)
        Me.fraTitle.MinimumSize = New System.Drawing.Size(0, 100)
        Me.fraTitle.Name = "fraTitle"
        Me.fraTitle.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.fraTitle.Size = New System.Drawing.Size(182, 114)
        Me.fraTitle.TabIndex = 7
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
        Me.tlpTitle.Controls.Add(Me.txtObjectName, 1, 0)
        Me.tlpTitle.Controls.Add(Me.lblCaption, 0, 1)
        Me.tlpTitle.Controls.Add(Me.txtCaption, 0, 2)
        Me.tlpTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpTitle.Location = New System.Drawing.Point(0, 13)
        Me.tlpTitle.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpTitle.Name = "tlpTitle"
        Me.tlpTitle.RowCount = 3
        Me.tlpTitle.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpTitle.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpTitle.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpTitle.Size = New System.Drawing.Size(179, 98)
        Me.tlpTitle.TabIndex = 1
        '
        'lblObjectName
        '
        Me.lblObjectName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblObjectName.AutoSize = True
        Me.lblObjectName.Location = New System.Drawing.Point(0, 3)
        Me.lblObjectName.Margin = New System.Windows.Forms.Padding(0)
        Me.lblObjectName.Name = "lblObjectName"
        Me.lblObjectName.Size = New System.Drawing.Size(67, 13)
        Me.lblObjectName.TabIndex = 0
        Me.lblObjectName.Text = "Object name"
        '
        'txtObjectName
        '
        Me.txtObjectName.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtObjectName.Location = New System.Drawing.Point(67, 0)
        Me.txtObjectName.Margin = New System.Windows.Forms.Padding(0)
        Me.txtObjectName.Name = "txtObjectName"
        Me.txtObjectName.Size = New System.Drawing.Size(112, 20)
        Me.txtObjectName.TabIndex = 1
        '
        'lblCaption
        '
        Me.lblCaption.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblCaption.AutoSize = True
        Me.tlpTitle.SetColumnSpan(Me.lblCaption, 2)
        Me.lblCaption.Location = New System.Drawing.Point(0, 20)
        Me.lblCaption.Margin = New System.Windows.Forms.Padding(0)
        Me.lblCaption.Name = "lblCaption"
        Me.lblCaption.Size = New System.Drawing.Size(86, 13)
        Me.lblCaption.TabIndex = 2
        Me.lblCaption.Text = "Caption/abstract"
        '
        'txtCaption
        '
        Me.txtCaption.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpTitle.SetColumnSpan(Me.txtCaption, 2)
        Me.txtCaption.Location = New System.Drawing.Point(3, 33)
        Me.txtCaption.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.txtCaption.Multiline = True
        Me.txtCaption.Name = "txtCaption"
        Me.txtCaption.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtCaption.Size = New System.Drawing.Size(176, 65)
        Me.txtCaption.TabIndex = 3
        '
        'sptTitle
        '
        Me.sptTitle.Cursor = System.Windows.Forms.Cursors.HSplit
        Me.sptTitle.Location = New System.Drawing.Point(202, 170)
        Me.sptTitle.Margin = New System.Windows.Forms.Padding(0)
        Me.sptTitle.Name = "sptTitle"
        Me.sptTitle.Size = New System.Drawing.Size(25, 1)
        Me.sptTitle.TabIndex = 8
        Me.sptTitle.TabStop = False
        '
        'tapIPTC
        '
        Me.tapIPTC.Controls.Add(Me.prgIPTC)
        Me.tapIPTC.Location = New System.Drawing.Point(4, 22)
        Me.tapIPTC.Name = "tapIPTC"
        Me.tapIPTC.Padding = New System.Windows.Forms.Padding(3)
        Me.tapIPTC.Size = New System.Drawing.Size(477, 399)
        Me.tapIPTC.TabIndex = 1
        Me.tapIPTC.Text = "IPTC"
        Me.tapIPTC.UseVisualStyleBackColor = True
        '
        'prgIPTC
        '
        Me.prgIPTC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.prgIPTC.Location = New System.Drawing.Point(3, 3)
        Me.prgIPTC.Name = "prgIPTC"
        Me.prgIPTC.Size = New System.Drawing.Size(471, 393)
        Me.prgIPTC.TabIndex = 0
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
        Me.tscMain.ContentPanel.Size = New System.Drawing.Size(720, 425)
        Me.tscMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tscMain.Location = New System.Drawing.Point(0, 0)
        Me.tscMain.Name = "tscMain"
        Me.tscMain.Size = New System.Drawing.Size(720, 496)
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
        Me.stsStatus.Size = New System.Drawing.Size(720, 22)
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
        Me.msnMain.Size = New System.Drawing.Size(720, 24)
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
        Me.tmiOptions.Size = New System.Drawing.Size(128, 22)
        Me.tmiOptions.Text = "&Options ..."
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
        Me.tmiAbout.Size = New System.Drawing.Size(119, 22)
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
        'kweKeywords
        '
        Me.kweKeywords.Dock = System.Windows.Forms.DockStyle.Fill
        Me.kweKeywords.Location = New System.Drawing.Point(2, 13)
        Me.kweKeywords.Name = "kweKeywords"
        Me.kweKeywords.Size = New System.Drawing.Size(156, 87)
        '
        '
        '
        Me.kweKeywords.Status.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.kweKeywords.Status.AutoChanged = False
        Me.kweKeywords.Status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.kweKeywords.Status.Enabled = False
        Me.kweKeywords.Status.Location = New System.Drawing.Point(85, 0)
        Me.kweKeywords.Status.Margin = New System.Windows.Forms.Padding(0)
        Me.kweKeywords.Status.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
        Me.kweKeywords.Status.Name = "stmStatus"
        Me.kweKeywords.Status.Size = New System.Drawing.Size(24, 24)
        Me.kweKeywords.Status.StatusedControl = Nothing
        Me.kweKeywords.Status.TabIndex = 2
        Me.kweKeywords.Status.TabStop = False
        Me.kweKeywords.Status.Visible = False
        Me.kweKeywords.StatusState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
        Me.kweKeywords.TabIndex = 0
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(720, 496)
        Me.Controls.Add(Me.tscMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.msnMain
        Me.Name = "frmMain"
        Me.Text = "Metanol"
        Me.splMain.Panel1.ResumeLayout(False)
        Me.splMain.Panel2.ResumeLayout(False)
        Me.splMain.ResumeLayout(False)
        Me.splBrowser.Panel1.ResumeLayout(False)
        Me.splBrowser.Panel2.ResumeLayout(False)
        Me.splBrowser.ResumeLayout(False)
        Me.tabInfo.ResumeLayout(False)
        Me.tapCommon.ResumeLayout(False)
        Me.flpCommon.ResumeLayout(False)
        Me.flpCommon.PerformLayout()
        Me.panImage.ResumeLayout(False)
        Me.panImage.PerformLayout()
        CType(Me.picPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraKeywords.ResumeLayout(False)
        Me.fraAuthor.ResumeLayout(False)
        Me.fraAuthor.PerformLayout()
        Me.tlpAuthor.ResumeLayout(False)
        Me.tlpAuthor.PerformLayout()
        Me.fraLocation.ResumeLayout(False)
        Me.fraLocation.PerformLayout()
        Me.tlpLocation.ResumeLayout(False)
        Me.tlpLocation.PerformLayout()
        Me.fraStatus.ResumeLayout(False)
        Me.fraStatus.PerformLayout()
        Me.tlpStatus.ResumeLayout(False)
        Me.tlpStatus.PerformLayout()
        CType(Me.nudUrgency, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraTitle.ResumeLayout(False)
        Me.fraTitle.PerformLayout()
        Me.tlpTitle.ResumeLayout(False)
        Me.tlpTitle.PerformLayout()
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

End Class
