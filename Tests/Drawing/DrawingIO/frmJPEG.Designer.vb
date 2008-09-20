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
            Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Me.cmdParse = New System.Windows.Forms.Button
            Me.ofdOpen = New System.Windows.Forms.OpenFileDialog
            Me.cmsContext = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.tmiExport = New System.Windows.Forms.ToolStripMenuItem
            Me.lblI = New System.Windows.Forms.Label
            Me.tlpI = New System.Windows.Forms.TableLayoutPanel
            Me.flpTop = New System.Windows.Forms.FlowLayoutPanel
            Me.nudSize = New System.Windows.Forms.NumericUpDown
            Me.chkFujiFilmFinePix2800Zoom = New System.Windows.Forms.CheckBox
            Me.sfdSave = New System.Windows.Forms.SaveFileDialog
            Me.splSplit = New System.Windows.Forms.SplitContainer
            Me.tvwResults = New System.Windows.Forms.TreeView
            Me.prgProperty = New System.Windows.Forms.PropertyGrid
            Me.lblToString = New System.Windows.Forms.Label
            Me.lblType = New System.Windows.Forms.Label
            Me.totToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.tabTabs = New System.Windows.Forms.TabControl
            Me.tapTree = New System.Windows.Forms.TabPage
            Me.tapMap = New System.Windows.Forms.TabPage
            Me.tlpMap = New System.Windows.Forms.TableLayoutPanel
            Me.dgwMap = New System.Windows.Forms.DataGridView
            Me.txcOffset = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txc0 = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txc1 = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txc2 = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txc3 = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txc4 = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txc5 = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txc6 = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txc7 = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txc8 = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txc9 = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txcA = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txcB = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txcC = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txcD = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txcE = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txcF = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.lblMap_BOM = New System.Windows.Forms.Label
            Me.lblMap_IFD0Offset = New System.Windows.Forms.Label
            Me.lblMap_IFDNumberOfEntries = New System.Windows.Forms.Label
            Me.lblMap_TagNumber = New System.Windows.Forms.Label
            Me.lblMap_TagDataType = New System.Windows.Forms.Label
            Me.lblMap_TagNumberOfComponents = New System.Windows.Forms.Label
            Me.lblMap_TagDataOrOffset = New System.Windows.Forms.Label
            Me.lblMap_ExternalTagData = New System.Windows.Forms.Label
            Me.lblMap_NextIFDOffset = New System.Windows.Forms.Label
            Me.lblMap_JPEGThumbnail = New System.Windows.Forms.Label
            Me.lblMap_TIFFThumbnailPart = New System.Windows.Forms.Label
            Me.lblMap_SubIFDNumberOfEntries = New System.Windows.Forms.Label
            Me.lblMap_NextSubIFDOffset = New System.Windows.Forms.Label
            Me.lblMap_BomTest = New System.Windows.Forms.Label
            Me.lblMap_Unknown = New System.Windows.Forms.Label
            Me.chkASCII = New System.Windows.Forms.CheckBox
            Me.tapEvents = New System.Windows.Forms.TabPage
            Me.txtEvents = New System.Windows.Forms.TextBox
            Me.cmsContext.SuspendLayout()
            Me.tlpI.SuspendLayout()
            Me.flpTop.SuspendLayout()
            CType(Me.nudSize, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.splSplit.Panel1.SuspendLayout()
            Me.splSplit.Panel2.SuspendLayout()
            Me.splSplit.SuspendLayout()
            Me.tabTabs.SuspendLayout()
            Me.tapTree.SuspendLayout()
            Me.tapMap.SuspendLayout()
            Me.tlpMap.SuspendLayout()
            CType(Me.dgwMap, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.tapEvents.SuspendLayout()
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
            Me.tlpI.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpI.Controls.Add(Me.lblI, 0, 0)
            Me.tlpI.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.tlpI.Location = New System.Drawing.Point(0, 412)
            Me.tlpI.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpI.Name = "tlpI"
            Me.tlpI.RowCount = 1
            Me.tlpI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpI.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
            Me.tlpI.Size = New System.Drawing.Size(547, 26)
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
            Me.splSplit.Location = New System.Drawing.Point(3, 3)
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
            Me.splSplit.Size = New System.Drawing.Size(533, 354)
            Me.splSplit.SplitterDistance = 270
            Me.splSplit.TabIndex = 4
            '
            'tvwResults
            '
            Me.tvwResults.ContextMenuStrip = Me.cmsContext
            Me.tvwResults.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tvwResults.Location = New System.Drawing.Point(0, 0)
            Me.tvwResults.Name = "tvwResults"
            Me.tvwResults.Size = New System.Drawing.Size(270, 354)
            Me.tvwResults.TabIndex = 1
            '
            'prgProperty
            '
            Me.prgProperty.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgProperty.Location = New System.Drawing.Point(0, 26)
            Me.prgProperty.Name = "prgProperty"
            Me.prgProperty.Size = New System.Drawing.Size(259, 328)
            Me.prgProperty.TabIndex = 0
            '
            'lblToString
            '
            Me.lblToString.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblToString.Location = New System.Drawing.Point(0, 13)
            Me.lblToString.Name = "lblToString"
            Me.lblToString.Size = New System.Drawing.Size(259, 13)
            Me.lblToString.TabIndex = 2
            Me.lblToString.TextAlign = System.Drawing.ContentAlignment.TopCenter
            '
            'lblType
            '
            Me.lblType.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.lblType.Location = New System.Drawing.Point(0, 0)
            Me.lblType.Name = "lblType"
            Me.lblType.Size = New System.Drawing.Size(259, 13)
            Me.lblType.TabIndex = 1
            Me.lblType.TextAlign = System.Drawing.ContentAlignment.TopCenter
            '
            'tabTabs
            '
            Me.tabTabs.Controls.Add(Me.tapTree)
            Me.tabTabs.Controls.Add(Me.tapMap)
            Me.tabTabs.Controls.Add(Me.tapEvents)
            Me.tabTabs.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tabTabs.Location = New System.Drawing.Point(0, 26)
            Me.tabTabs.Name = "tabTabs"
            Me.tabTabs.SelectedIndex = 0
            Me.tabTabs.Size = New System.Drawing.Size(547, 386)
            Me.tabTabs.TabIndex = 5
            '
            'tapTree
            '
            Me.tapTree.Controls.Add(Me.splSplit)
            Me.tapTree.Location = New System.Drawing.Point(4, 22)
            Me.tapTree.Name = "tapTree"
            Me.tapTree.Padding = New System.Windows.Forms.Padding(3)
            Me.tapTree.Size = New System.Drawing.Size(539, 360)
            Me.tapTree.TabIndex = 0
            Me.tapTree.Text = "Tree"
            Me.tapTree.UseVisualStyleBackColor = True
            '
            'tapMap
            '
            Me.tapMap.Controls.Add(Me.tlpMap)
            Me.tapMap.Location = New System.Drawing.Point(4, 22)
            Me.tapMap.Name = "tapMap"
            Me.tapMap.Padding = New System.Windows.Forms.Padding(3)
            Me.tapMap.Size = New System.Drawing.Size(539, 360)
            Me.tapMap.TabIndex = 1
            Me.tapMap.Text = "Exif map"
            Me.tapMap.UseVisualStyleBackColor = True
            '
            'tlpMap
            '
            Me.tlpMap.ColumnCount = 2
            Me.tlpMap.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMap.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMap.Controls.Add(Me.dgwMap, 0, 0)
            Me.tlpMap.Controls.Add(Me.lblMap_BOM, 1, 1)
            Me.tlpMap.Controls.Add(Me.lblMap_IFD0Offset, 1, 3)
            Me.tlpMap.Controls.Add(Me.lblMap_IFDNumberOfEntries, 1, 4)
            Me.tlpMap.Controls.Add(Me.lblMap_TagNumber, 1, 5)
            Me.tlpMap.Controls.Add(Me.lblMap_TagDataType, 1, 6)
            Me.tlpMap.Controls.Add(Me.lblMap_TagNumberOfComponents, 1, 7)
            Me.tlpMap.Controls.Add(Me.lblMap_TagDataOrOffset, 1, 8)
            Me.tlpMap.Controls.Add(Me.lblMap_ExternalTagData, 1, 9)
            Me.tlpMap.Controls.Add(Me.lblMap_NextIFDOffset, 1, 10)
            Me.tlpMap.Controls.Add(Me.lblMap_JPEGThumbnail, 1, 11)
            Me.tlpMap.Controls.Add(Me.lblMap_TIFFThumbnailPart, 1, 12)
            Me.tlpMap.Controls.Add(Me.lblMap_SubIFDNumberOfEntries, 1, 13)
            Me.tlpMap.Controls.Add(Me.lblMap_NextSubIFDOffset, 1, 14)
            Me.tlpMap.Controls.Add(Me.lblMap_BomTest, 1, 2)
            Me.tlpMap.Controls.Add(Me.lblMap_Unknown, 1, 0)
            Me.tlpMap.Controls.Add(Me.chkASCII, 1, 15)
            Me.tlpMap.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpMap.Location = New System.Drawing.Point(3, 3)
            Me.tlpMap.Name = "tlpMap"
            Me.tlpMap.RowCount = 16
            Me.tlpMap.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMap.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMap.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMap.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMap.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMap.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMap.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMap.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMap.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMap.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMap.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMap.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMap.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMap.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMap.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMap.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMap.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMap.Size = New System.Drawing.Size(533, 354)
            Me.tlpMap.TabIndex = 0
            '
            'dgwMap
            '
            Me.dgwMap.AllowUserToAddRows = False
            Me.dgwMap.AllowUserToDeleteRows = False
            Me.dgwMap.AllowUserToResizeColumns = False
            Me.dgwMap.AllowUserToResizeRows = False
            Me.dgwMap.BackgroundColor = System.Drawing.SystemColors.Window
            Me.dgwMap.BorderStyle = System.Windows.Forms.BorderStyle.None
            Me.dgwMap.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
            Me.dgwMap.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
            DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
            DataGridViewCellStyle6.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.dgwMap.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
            Me.dgwMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgwMap.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.txcOffset, Me.txc0, Me.txc1, Me.txc2, Me.txc3, Me.txc4, Me.txc5, Me.txc6, Me.txc7, Me.txc8, Me.txc9, Me.txcA, Me.txcB, Me.txcC, Me.txcD, Me.txcE, Me.txcF})
            DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
            DataGridViewCellStyle8.BackColor = System.Drawing.Color.White
            DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
            DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White
            DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.dgwMap.DefaultCellStyle = DataGridViewCellStyle8
            Me.dgwMap.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgwMap.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
            Me.dgwMap.EnableHeadersVisualStyles = False
            Me.dgwMap.GridColor = System.Drawing.Color.Black
            Me.dgwMap.Location = New System.Drawing.Point(0, 0)
            Me.dgwMap.Margin = New System.Windows.Forms.Padding(0)
            Me.dgwMap.Name = "dgwMap"
            Me.dgwMap.ReadOnly = True
            Me.dgwMap.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
            DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
            DataGridViewCellStyle9.BackColor = System.Drawing.Color.White
            DataGridViewCellStyle9.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            DataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
            DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
            DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.dgwMap.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
            Me.dgwMap.RowHeadersVisible = False
            Me.dgwMap.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
            DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
            DataGridViewCellStyle10.BackColor = System.Drawing.Color.White
            DataGridViewCellStyle10.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            DataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle10.Format = "X"
            DataGridViewCellStyle10.NullValue = Nothing
            DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.dgwMap.RowsDefaultCellStyle = DataGridViewCellStyle10
            Me.tlpMap.SetRowSpan(Me.dgwMap, 16)
            Me.dgwMap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
            Me.dgwMap.ShowCellErrors = False
            Me.dgwMap.ShowEditingIcon = False
            Me.dgwMap.ShowRowErrors = False
            Me.dgwMap.Size = New System.Drawing.Size(390, 354)
            Me.dgwMap.TabIndex = 4
            Me.dgwMap.VirtualMode = True
            '
            'txcOffset
            '
            Me.txcOffset.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
            DataGridViewCellStyle7.BackColor = System.Drawing.Color.White
            DataGridViewCellStyle7.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle7.Format = "X"
            DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White
            DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
            DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
            Me.txcOffset.DefaultCellStyle = DataGridViewCellStyle7
            Me.txcOffset.Frozen = True
            Me.txcOffset.HeaderText = ""
            Me.txcOffset.Name = "txcOffset"
            Me.txcOffset.ReadOnly = True
            Me.txcOffset.Width = 18
            '
            'txc0
            '
            Me.txc0.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.txc0.HeaderText = "0"
            Me.txc0.Name = "txc0"
            Me.txc0.ReadOnly = True
            Me.txc0.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.txc0.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'txc1
            '
            Me.txc1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.txc1.HeaderText = "1"
            Me.txc1.Name = "txc1"
            Me.txc1.ReadOnly = True
            Me.txc1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.txc1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'txc2
            '
            Me.txc2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.txc2.HeaderText = "2"
            Me.txc2.Name = "txc2"
            Me.txc2.ReadOnly = True
            Me.txc2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.txc2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'txc3
            '
            Me.txc3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.txc3.HeaderText = "3"
            Me.txc3.Name = "txc3"
            Me.txc3.ReadOnly = True
            Me.txc3.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.txc3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'txc4
            '
            Me.txc4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.txc4.HeaderText = "4"
            Me.txc4.Name = "txc4"
            Me.txc4.ReadOnly = True
            Me.txc4.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.txc4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'txc5
            '
            Me.txc5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.txc5.HeaderText = "5"
            Me.txc5.Name = "txc5"
            Me.txc5.ReadOnly = True
            Me.txc5.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.txc5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'txc6
            '
            Me.txc6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.txc6.HeaderText = "6"
            Me.txc6.Name = "txc6"
            Me.txc6.ReadOnly = True
            Me.txc6.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.txc6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'txc7
            '
            Me.txc7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.txc7.HeaderText = "7"
            Me.txc7.Name = "txc7"
            Me.txc7.ReadOnly = True
            Me.txc7.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.txc7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'txc8
            '
            Me.txc8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.txc8.HeaderText = "8"
            Me.txc8.Name = "txc8"
            Me.txc8.ReadOnly = True
            Me.txc8.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.txc8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'txc9
            '
            Me.txc9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.txc9.HeaderText = "9"
            Me.txc9.Name = "txc9"
            Me.txc9.ReadOnly = True
            Me.txc9.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.txc9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'txcA
            '
            Me.txcA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.txcA.HeaderText = "A"
            Me.txcA.Name = "txcA"
            Me.txcA.ReadOnly = True
            Me.txcA.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.txcA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'txcB
            '
            Me.txcB.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.txcB.HeaderText = "B"
            Me.txcB.Name = "txcB"
            Me.txcB.ReadOnly = True
            Me.txcB.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.txcB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'txcC
            '
            Me.txcC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.txcC.HeaderText = "C"
            Me.txcC.Name = "txcC"
            Me.txcC.ReadOnly = True
            Me.txcC.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.txcC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'txcD
            '
            Me.txcD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.txcD.HeaderText = "D"
            Me.txcD.Name = "txcD"
            Me.txcD.ReadOnly = True
            Me.txcD.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.txcD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'txcE
            '
            Me.txcE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.txcE.HeaderText = "E"
            Me.txcE.Name = "txcE"
            Me.txcE.ReadOnly = True
            Me.txcE.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.txcE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'txcF
            '
            Me.txcF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.txcF.HeaderText = "F"
            Me.txcF.Name = "txcF"
            Me.txcF.ReadOnly = True
            Me.txcF.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.txcF.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'lblMap_BOM
            '
            Me.lblMap_BOM.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.lblMap_BOM.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblMap_BOM.Location = New System.Drawing.Point(393, 13)
            Me.lblMap_BOM.Name = "lblMap_BOM"
            Me.lblMap_BOM.Size = New System.Drawing.Size(137, 13)
            Me.lblMap_BOM.TabIndex = 1
            Me.lblMap_BOM.Text = "BOM"
            Me.lblMap_BOM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblMap_IFD0Offset
            '
            Me.lblMap_IFD0Offset.BackColor = System.Drawing.Color.Cyan
            Me.lblMap_IFD0Offset.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblMap_IFD0Offset.Location = New System.Drawing.Point(393, 39)
            Me.lblMap_IFD0Offset.Name = "lblMap_IFD0Offset"
            Me.lblMap_IFD0Offset.Size = New System.Drawing.Size(137, 13)
            Me.lblMap_IFD0Offset.TabIndex = 1
            Me.lblMap_IFD0Offset.Text = "IFD0 offset"
            Me.lblMap_IFD0Offset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblMap_IFDNumberOfEntries
            '
            Me.lblMap_IFDNumberOfEntries.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.lblMap_IFDNumberOfEntries.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblMap_IFDNumberOfEntries.Location = New System.Drawing.Point(393, 52)
            Me.lblMap_IFDNumberOfEntries.Name = "lblMap_IFDNumberOfEntries"
            Me.lblMap_IFDNumberOfEntries.Size = New System.Drawing.Size(137, 13)
            Me.lblMap_IFDNumberOfEntries.TabIndex = 1
            Me.lblMap_IFDNumberOfEntries.Text = "IFD number of entries"
            Me.lblMap_IFDNumberOfEntries.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblMap_TagNumber
            '
            Me.lblMap_TagNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.lblMap_TagNumber.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblMap_TagNumber.Location = New System.Drawing.Point(393, 65)
            Me.lblMap_TagNumber.Name = "lblMap_TagNumber"
            Me.lblMap_TagNumber.Size = New System.Drawing.Size(137, 13)
            Me.lblMap_TagNumber.TabIndex = 1
            Me.lblMap_TagNumber.Text = "Tag number"
            Me.lblMap_TagNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblMap_TagDataType
            '
            Me.lblMap_TagDataType.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
            Me.lblMap_TagDataType.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblMap_TagDataType.Location = New System.Drawing.Point(393, 78)
            Me.lblMap_TagDataType.Name = "lblMap_TagDataType"
            Me.lblMap_TagDataType.Size = New System.Drawing.Size(137, 13)
            Me.lblMap_TagDataType.TabIndex = 1
            Me.lblMap_TagDataType.Text = "Tag data type"
            Me.lblMap_TagDataType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblMap_TagNumberOfComponents
            '
            Me.lblMap_TagNumberOfComponents.BackColor = System.Drawing.Color.Red
            Me.lblMap_TagNumberOfComponents.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblMap_TagNumberOfComponents.Location = New System.Drawing.Point(393, 91)
            Me.lblMap_TagNumberOfComponents.Name = "lblMap_TagNumberOfComponents"
            Me.lblMap_TagNumberOfComponents.Size = New System.Drawing.Size(137, 13)
            Me.lblMap_TagNumberOfComponents.TabIndex = 1
            Me.lblMap_TagNumberOfComponents.Text = "Tag number of components"
            Me.lblMap_TagNumberOfComponents.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblMap_TagDataOrOffset
            '
            Me.lblMap_TagDataOrOffset.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.lblMap_TagDataOrOffset.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblMap_TagDataOrOffset.Location = New System.Drawing.Point(393, 104)
            Me.lblMap_TagDataOrOffset.Name = "lblMap_TagDataOrOffset"
            Me.lblMap_TagDataOrOffset.Size = New System.Drawing.Size(137, 13)
            Me.lblMap_TagDataOrOffset.TabIndex = 1
            Me.lblMap_TagDataOrOffset.Text = "Tag data or offset"
            Me.lblMap_TagDataOrOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblMap_ExternalTagData
            '
            Me.lblMap_ExternalTagData.BackColor = System.Drawing.Color.Silver
            Me.lblMap_ExternalTagData.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblMap_ExternalTagData.Location = New System.Drawing.Point(393, 117)
            Me.lblMap_ExternalTagData.Name = "lblMap_ExternalTagData"
            Me.lblMap_ExternalTagData.Size = New System.Drawing.Size(137, 13)
            Me.lblMap_ExternalTagData.TabIndex = 1
            Me.lblMap_ExternalTagData.Text = "External tag data"
            Me.lblMap_ExternalTagData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblMap_NextIFDOffset
            '
            Me.lblMap_NextIFDOffset.BackColor = System.Drawing.Color.Yellow
            Me.lblMap_NextIFDOffset.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblMap_NextIFDOffset.Location = New System.Drawing.Point(393, 130)
            Me.lblMap_NextIFDOffset.Name = "lblMap_NextIFDOffset"
            Me.lblMap_NextIFDOffset.Size = New System.Drawing.Size(137, 13)
            Me.lblMap_NextIFDOffset.TabIndex = 1
            Me.lblMap_NextIFDOffset.Text = "Next IFD offset"
            Me.lblMap_NextIFDOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblMap_JPEGThumbnail
            '
            Me.lblMap_JPEGThumbnail.BackColor = System.Drawing.Color.Fuchsia
            Me.lblMap_JPEGThumbnail.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblMap_JPEGThumbnail.Location = New System.Drawing.Point(393, 143)
            Me.lblMap_JPEGThumbnail.Name = "lblMap_JPEGThumbnail"
            Me.lblMap_JPEGThumbnail.Size = New System.Drawing.Size(137, 13)
            Me.lblMap_JPEGThumbnail.TabIndex = 1
            Me.lblMap_JPEGThumbnail.Text = "JPEG thumbnail"
            Me.lblMap_JPEGThumbnail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblMap_TIFFThumbnailPart
            '
            Me.lblMap_TIFFThumbnailPart.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.lblMap_TIFFThumbnailPart.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblMap_TIFFThumbnailPart.Location = New System.Drawing.Point(393, 156)
            Me.lblMap_TIFFThumbnailPart.Name = "lblMap_TIFFThumbnailPart"
            Me.lblMap_TIFFThumbnailPart.Size = New System.Drawing.Size(137, 13)
            Me.lblMap_TIFFThumbnailPart.TabIndex = 1
            Me.lblMap_TIFFThumbnailPart.Text = "TTIFF thumbnail part"
            Me.lblMap_TIFFThumbnailPart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblMap_SubIFDNumberOfEntries
            '
            Me.lblMap_SubIFDNumberOfEntries.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
            Me.lblMap_SubIFDNumberOfEntries.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblMap_SubIFDNumberOfEntries.Location = New System.Drawing.Point(393, 169)
            Me.lblMap_SubIFDNumberOfEntries.Name = "lblMap_SubIFDNumberOfEntries"
            Me.lblMap_SubIFDNumberOfEntries.Size = New System.Drawing.Size(137, 13)
            Me.lblMap_SubIFDNumberOfEntries.TabIndex = 1
            Me.lblMap_SubIFDNumberOfEntries.Text = "SubIFD number of entries"
            Me.lblMap_SubIFDNumberOfEntries.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblMap_NextSubIFDOffset
            '
            Me.lblMap_NextSubIFDOffset.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.lblMap_NextSubIFDOffset.BackColor = System.Drawing.Color.Lime
            Me.lblMap_NextSubIFDOffset.Location = New System.Drawing.Point(393, 182)
            Me.lblMap_NextSubIFDOffset.Name = "lblMap_NextSubIFDOffset"
            Me.lblMap_NextSubIFDOffset.Size = New System.Drawing.Size(137, 13)
            Me.lblMap_NextSubIFDOffset.TabIndex = 1
            Me.lblMap_NextSubIFDOffset.Text = "Next SubIFD offset"
            Me.lblMap_NextSubIFDOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblMap_BomTest
            '
            Me.lblMap_BomTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.lblMap_BomTest.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblMap_BomTest.Location = New System.Drawing.Point(393, 26)
            Me.lblMap_BomTest.Name = "lblMap_BomTest"
            Me.lblMap_BomTest.Size = New System.Drawing.Size(137, 13)
            Me.lblMap_BomTest.TabIndex = 2
            Me.lblMap_BomTest.Text = "BOM test"
            Me.lblMap_BomTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblMap_Unknown
            '
            Me.lblMap_Unknown.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
            Me.lblMap_Unknown.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblMap_Unknown.Location = New System.Drawing.Point(393, 0)
            Me.lblMap_Unknown.Name = "lblMap_Unknown"
            Me.lblMap_Unknown.Size = New System.Drawing.Size(137, 13)
            Me.lblMap_Unknown.TabIndex = 3
            Me.lblMap_Unknown.Text = "Unknown"
            Me.lblMap_Unknown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'chkASCII
            '
            Me.chkASCII.AutoSize = True
            Me.chkASCII.Location = New System.Drawing.Point(393, 198)
            Me.chkASCII.Name = "chkASCII"
            Me.chkASCII.Size = New System.Drawing.Size(53, 17)
            Me.chkASCII.TabIndex = 5
            Me.chkASCII.Text = "ASCII"
            Me.chkASCII.UseVisualStyleBackColor = True
            '
            'tapEvents
            '
            Me.tapEvents.Controls.Add(Me.txtEvents)
            Me.tapEvents.Location = New System.Drawing.Point(4, 22)
            Me.tapEvents.Name = "tapEvents"
            Me.tapEvents.Padding = New System.Windows.Forms.Padding(3)
            Me.tapEvents.Size = New System.Drawing.Size(539, 360)
            Me.tapEvents.TabIndex = 2
            Me.tapEvents.Text = "Events"
            Me.tapEvents.UseVisualStyleBackColor = True
            '
            'txtEvents
            '
            Me.txtEvents.Dock = System.Windows.Forms.DockStyle.Fill
            Me.txtEvents.Location = New System.Drawing.Point(3, 3)
            Me.txtEvents.Multiline = True
            Me.txtEvents.Name = "txtEvents"
            Me.txtEvents.ReadOnly = True
            Me.txtEvents.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.txtEvents.Size = New System.Drawing.Size(533, 354)
            Me.txtEvents.TabIndex = 1
            Me.txtEvents.WordWrap = False
            '
            'frmJPEG
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(547, 438)
            Me.Controls.Add(Me.tabTabs)
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
            Me.tabTabs.ResumeLayout(False)
            Me.tapTree.ResumeLayout(False)
            Me.tapMap.ResumeLayout(False)
            Me.tlpMap.ResumeLayout(False)
            Me.tlpMap.PerformLayout()
            CType(Me.dgwMap, System.ComponentModel.ISupportInitialize).EndInit()
            Me.tapEvents.ResumeLayout(False)
            Me.tapEvents.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents cmdParse As System.Windows.Forms.Button
        Friend WithEvents ofdOpen As System.Windows.Forms.OpenFileDialog
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
        Friend WithEvents tabTabs As System.Windows.Forms.TabControl
        Friend WithEvents tapTree As System.Windows.Forms.TabPage
        Friend WithEvents tapMap As System.Windows.Forms.TabPage
        Friend WithEvents tlpMap As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents tapEvents As System.Windows.Forms.TabPage
        Friend WithEvents txtEvents As System.Windows.Forms.TextBox
        Friend WithEvents lblMap_BOM As System.Windows.Forms.Label
        Friend WithEvents lblMap_IFD0Offset As System.Windows.Forms.Label
        Friend WithEvents lblMap_IFDNumberOfEntries As System.Windows.Forms.Label
        Friend WithEvents lblMap_TagNumber As System.Windows.Forms.Label
        Friend WithEvents lblMap_TagDataType As System.Windows.Forms.Label
        Friend WithEvents lblMap_TagNumberOfComponents As System.Windows.Forms.Label
        Friend WithEvents lblMap_TagDataOrOffset As System.Windows.Forms.Label
        Friend WithEvents lblMap_ExternalTagData As System.Windows.Forms.Label
        Friend WithEvents lblMap_NextIFDOffset As System.Windows.Forms.Label
        Friend WithEvents lblMap_JPEGThumbnail As System.Windows.Forms.Label
        Friend WithEvents lblMap_TIFFThumbnailPart As System.Windows.Forms.Label
        Friend WithEvents lblMap_SubIFDNumberOfEntries As System.Windows.Forms.Label
        Friend WithEvents lblMap_NextSubIFDOffset As System.Windows.Forms.Label
        Friend WithEvents lblMap_BomTest As System.Windows.Forms.Label
        Friend WithEvents lblMap_Unknown As System.Windows.Forms.Label
        Friend WithEvents tvwResults As System.Windows.Forms.TreeView
        Friend WithEvents dgwMap As System.Windows.Forms.DataGridView
        Friend WithEvents txcOffset As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txc0 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txc1 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txc2 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txc3 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txc4 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txc5 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txc6 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txc7 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txc8 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txc9 As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txcA As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txcB As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txcC As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txcD As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txcE As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txcF As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents chkASCII As System.Windows.Forms.CheckBox
    End Class
End Namespace