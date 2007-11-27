<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
        Me.lblDir = New System.Windows.Forms.Label
        Me.tlpDir = New System.Windows.Forms.TableLayoutPanel
        Me.cmdDir = New System.Windows.Forms.Button
        Me.txtDir = New System.Windows.Forms.TextBox
        Me.lblMask = New System.Windows.Forms.Label
        Me.txtMask = New System.Windows.Forms.TextBox
        Me.lblInterval = New System.Windows.Forms.Label
        Me.nudInterval = New System.Windows.Forms.NumericUpDown
        Me.lblOrder = New System.Windows.Forms.Label
        Me.flpOrder = New System.Windows.Forms.FlowLayoutPanel
        Me.optOrderRandom = New System.Windows.Forms.RadioButton
        Me.optOrderSequential = New System.Windows.Forms.RadioButton
        Me.txtText = New System.Windows.Forms.TextBox
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.lblColors = New System.Windows.Forms.Label
        Me.flpColors = New System.Windows.Forms.FlowLayoutPanel
        Me.cmdBgWin = New System.Windows.Forms.Button
        Me.cmdFgInfo = New System.Windows.Forms.Button
        Me.cmdBgInfo = New System.Windows.Forms.Button
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
        Me.lblTransparency = New System.Windows.Forms.Label
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown
        Me.lblTransparentI = New System.Windows.Forms.Label
        Me.lblFont = New System.Windows.Forms.Label
        Me.cmdFont = New System.Windows.Forms.Button
        Me.lblAlign = New System.Windows.Forms.Label
        Me.tlpAlign = New System.Windows.Forms.TableLayoutPanel
        Me.optTopLeft = New System.Windows.Forms.RadioButton
        Me.optTopCenter = New System.Windows.Forms.RadioButton
        Me.optTopRight = New System.Windows.Forms.RadioButton
        Me.optMiddleLeft = New System.Windows.Forms.RadioButton
        Me.optMiddleCenter = New System.Windows.Forms.RadioButton
        Me.optMiddleRight = New System.Windows.Forms.RadioButton
        Me.optBottomLeft = New System.Windows.Forms.RadioButton
        Me.optBottomCenter = New System.Windows.Forms.RadioButton
        Me.optBottomRight = New System.Windows.Forms.RadioButton
        Me.flpDiplayedText = New System.Windows.Forms.FlowLayoutPanel
        Me.lblText = New System.Windows.Forms.Label
        Me.cmdProperties = New System.Windows.Forms.Button
        Me.fbdFolder = New System.Windows.Forms.FolderBrowserDialog
        Me.cdlColor = New System.Windows.Forms.ColorDialog
        Me.fdlFont = New System.Windows.Forms.FontDialog
        Me.tlpMain.SuspendLayout()
        Me.tlpDir.SuspendLayout()
        CType(Me.nudInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.flpOrder.SuspendLayout()
        Me.flpColors.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpAlign.SuspendLayout()
        Me.flpDiplayedText.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 2
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Controls.Add(Me.lblDir, 0, 0)
        Me.tlpMain.Controls.Add(Me.tlpDir, 1, 0)
        Me.tlpMain.Controls.Add(Me.lblMask, 0, 1)
        Me.tlpMain.Controls.Add(Me.txtMask, 1, 1)
        Me.tlpMain.Controls.Add(Me.lblInterval, 0, 2)
        Me.tlpMain.Controls.Add(Me.nudInterval, 1, 2)
        Me.tlpMain.Controls.Add(Me.lblOrder, 0, 3)
        Me.tlpMain.Controls.Add(Me.flpOrder, 1, 3)
        Me.tlpMain.Controls.Add(Me.txtText, 1, 7)
        Me.tlpMain.Controls.Add(Me.cmdOK, 0, 8)
        Me.tlpMain.Controls.Add(Me.cmdCancel, 1, 8)
        Me.tlpMain.Controls.Add(Me.lblColors, 0, 4)
        Me.tlpMain.Controls.Add(Me.flpColors, 1, 4)
        Me.tlpMain.Controls.Add(Me.lblFont, 0, 5)
        Me.tlpMain.Controls.Add(Me.cmdFont, 1, 5)
        Me.tlpMain.Controls.Add(Me.lblAlign, 0, 6)
        Me.tlpMain.Controls.Add(Me.tlpAlign, 0, 7)
        Me.tlpMain.Controls.Add(Me.flpDiplayedText, 1, 6)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 9
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpMain.Size = New System.Drawing.Size(555, 365)
        Me.tlpMain.TabIndex = 0
        '
        'lblDir
        '
        Me.lblDir.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblDir.AutoSize = True
        Me.lblDir.Location = New System.Drawing.Point(3, 6)
        Me.lblDir.Name = "lblDir"
        Me.lblDir.Size = New System.Drawing.Size(79, 13)
        Me.lblDir.TabIndex = 0
        Me.lblDir.Text = "Image directory"
        '
        'tlpDir
        '
        Me.tlpDir.AutoSize = True
        Me.tlpDir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpDir.ColumnCount = 2
        Me.tlpDir.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpDir.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpDir.Controls.Add(Me.cmdDir, 1, 0)
        Me.tlpDir.Controls.Add(Me.txtDir, 0, 0)
        Me.tlpDir.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpDir.Location = New System.Drawing.Point(163, 0)
        Me.tlpDir.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpDir.Name = "tlpDir"
        Me.tlpDir.RowCount = 1
        Me.tlpDir.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpDir.Size = New System.Drawing.Size(392, 26)
        Me.tlpDir.TabIndex = 1
        '
        'cmdDir
        '
        Me.cmdDir.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cmdDir.AutoSize = True
        Me.cmdDir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdDir.Location = New System.Drawing.Point(366, 1)
        Me.cmdDir.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdDir.Name = "cmdDir"
        Me.cmdDir.Size = New System.Drawing.Size(26, 23)
        Me.cmdDir.TabIndex = 1
        Me.cmdDir.Text = "..."
        Me.cmdDir.UseVisualStyleBackColor = True
        '
        'txtDir
        '
        Me.txtDir.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDir.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtDir.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
        Me.txtDir.Location = New System.Drawing.Point(3, 3)
        Me.txtDir.Name = "txtDir"
        Me.txtDir.Size = New System.Drawing.Size(360, 20)
        Me.txtDir.TabIndex = 0
        '
        'lblMask
        '
        Me.lblMask.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMask.AutoSize = True
        Me.lblMask.Location = New System.Drawing.Point(3, 32)
        Me.lblMask.Name = "lblMask"
        Me.lblMask.Size = New System.Drawing.Size(157, 13)
        Me.lblMask.TabIndex = 2
        Me.lblMask.Text = "Masks (separate by semicolons)"
        '
        'txtMask
        '
        Me.txtMask.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMask.Location = New System.Drawing.Point(166, 29)
        Me.txtMask.Name = "txtMask"
        Me.txtMask.Size = New System.Drawing.Size(386, 20)
        Me.txtMask.TabIndex = 3
        Me.txtMask.Text = "*.jpg;*.jpeg"
        '
        'lblInterval
        '
        Me.lblInterval.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblInterval.AutoSize = True
        Me.lblInterval.Location = New System.Drawing.Point(3, 58)
        Me.lblInterval.Name = "lblInterval"
        Me.lblInterval.Size = New System.Drawing.Size(56, 13)
        Me.lblInterval.TabIndex = 4
        Me.lblInterval.Text = "Interval [s]"
        '
        'nudInterval
        '
        Me.nudInterval.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.nudInterval.Location = New System.Drawing.Point(166, 55)
        Me.nudInterval.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudInterval.Name = "nudInterval"
        Me.nudInterval.Size = New System.Drawing.Size(47, 20)
        Me.nudInterval.TabIndex = 5
        Me.nudInterval.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'lblOrder
        '
        Me.lblOrder.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblOrder.AutoSize = True
        Me.lblOrder.Location = New System.Drawing.Point(3, 83)
        Me.lblOrder.Name = "lblOrder"
        Me.lblOrder.Size = New System.Drawing.Size(47, 13)
        Me.lblOrder.TabIndex = 6
        Me.lblOrder.Text = "Ordering"
        '
        'flpOrder
        '
        Me.flpOrder.AutoSize = True
        Me.flpOrder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpOrder.Controls.Add(Me.optOrderRandom)
        Me.flpOrder.Controls.Add(Me.optOrderSequential)
        Me.flpOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpOrder.Location = New System.Drawing.Point(163, 78)
        Me.flpOrder.Margin = New System.Windows.Forms.Padding(0)
        Me.flpOrder.Name = "flpOrder"
        Me.flpOrder.Size = New System.Drawing.Size(392, 23)
        Me.flpOrder.TabIndex = 7
        '
        'optOrderRandom
        '
        Me.optOrderRandom.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.optOrderRandom.AutoSize = True
        Me.optOrderRandom.Checked = True
        Me.optOrderRandom.Location = New System.Drawing.Point(3, 3)
        Me.optOrderRandom.Name = "optOrderRandom"
        Me.optOrderRandom.Size = New System.Drawing.Size(65, 17)
        Me.optOrderRandom.TabIndex = 0
        Me.optOrderRandom.TabStop = True
        Me.optOrderRandom.Text = "Random"
        Me.optOrderRandom.UseVisualStyleBackColor = True
        '
        'optOrderSequential
        '
        Me.optOrderSequential.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.optOrderSequential.AutoSize = True
        Me.optOrderSequential.Location = New System.Drawing.Point(74, 3)
        Me.optOrderSequential.Name = "optOrderSequential"
        Me.optOrderSequential.Size = New System.Drawing.Size(75, 17)
        Me.optOrderSequential.TabIndex = 1
        Me.optOrderSequential.Text = "Sequential"
        Me.optOrderSequential.UseVisualStyleBackColor = True
        '
        'txtText
        '
        Me.txtText.AcceptsReturn = True
        Me.txtText.AcceptsTab = True
        Me.txtText.AllowDrop = True
        Me.txtText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtText.HideSelection = False
        Me.txtText.Location = New System.Drawing.Point(166, 199)
        Me.txtText.MaxLength = 0
        Me.txtText.Multiline = True
        Me.txtText.Name = "txtText"
        Me.txtText.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtText.Size = New System.Drawing.Size(386, 134)
        Me.txtText.TabIndex = 9
        Me.txtText.WordWrap = False
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdOK.AutoSize = True
        Me.cmdOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdOK.Location = New System.Drawing.Point(65, 339)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(32, 23)
        Me.cmdOK.TabIndex = 10
        Me.cmdOK.Text = "&OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdCancel.AutoSize = True
        Me.cmdCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(334, 339)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(50, 23)
        Me.cmdCancel.TabIndex = 11
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'lblColors
        '
        Me.lblColors.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblColors.AutoSize = True
        Me.lblColors.Location = New System.Drawing.Point(3, 119)
        Me.lblColors.Name = "lblColors"
        Me.lblColors.Size = New System.Drawing.Size(36, 13)
        Me.lblColors.TabIndex = 12
        Me.lblColors.Text = "Colors"
        '
        'flpColors
        '
        Me.flpColors.AutoSize = True
        Me.flpColors.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpColors.Controls.Add(Me.cmdBgWin)
        Me.flpColors.Controls.Add(Me.cmdFgInfo)
        Me.flpColors.Controls.Add(Me.cmdBgInfo)
        Me.flpColors.Controls.Add(Me.FlowLayoutPanel1)
        Me.flpColors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpColors.Location = New System.Drawing.Point(163, 101)
        Me.flpColors.Margin = New System.Windows.Forms.Padding(0)
        Me.flpColors.Name = "flpColors"
        Me.flpColors.Size = New System.Drawing.Size(392, 49)
        Me.flpColors.TabIndex = 13
        '
        'cmdBgWin
        '
        Me.cmdBgWin.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdBgWin.AutoSize = True
        Me.cmdBgWin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdBgWin.Location = New System.Drawing.Point(3, 0)
        Me.cmdBgWin.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.cmdBgWin.Name = "cmdBgWin"
        Me.cmdBgWin.Size = New System.Drawing.Size(87, 23)
        Me.cmdBgWin.TabIndex = 2
        Me.cmdBgWin.Text = "Background ..."
        Me.cmdBgWin.UseVisualStyleBackColor = True
        '
        'cmdFgInfo
        '
        Me.cmdFgInfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdFgInfo.AutoSize = True
        Me.cmdFgInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdFgInfo.Location = New System.Drawing.Point(96, 0)
        Me.cmdFgInfo.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.cmdFgInfo.Name = "cmdFgInfo"
        Me.cmdFgInfo.Size = New System.Drawing.Size(50, 23)
        Me.cmdFgInfo.TabIndex = 1
        Me.cmdFgInfo.Text = "Text ..."
        Me.cmdFgInfo.UseVisualStyleBackColor = True
        '
        'cmdBgInfo
        '
        Me.cmdBgInfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdBgInfo.AutoSize = True
        Me.cmdBgInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdBgInfo.Location = New System.Drawing.Point(152, 0)
        Me.cmdBgInfo.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.cmdBgInfo.Name = "cmdBgInfo"
        Me.cmdBgInfo.Size = New System.Drawing.Size(110, 23)
        Me.cmdBgInfo.TabIndex = 0
        Me.cmdBgInfo.Text = "Text background ..."
        Me.cmdBgInfo.UseVisualStyleBackColor = True
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AutoSize = True
        Me.FlowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.FlowLayoutPanel1.Controls.Add(Me.lblTransparency)
        Me.FlowLayoutPanel1.Controls.Add(Me.NumericUpDown1)
        Me.FlowLayoutPanel1.Controls.Add(Me.lblTransparentI)
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 23)
        Me.FlowLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(232, 26)
        Me.FlowLayoutPanel1.TabIndex = 3
        '
        'lblTransparency
        '
        Me.lblTransparency.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTransparency.AutoSize = True
        Me.lblTransparency.Location = New System.Drawing.Point(3, 6)
        Me.lblTransparency.Name = "lblTransparency"
        Me.lblTransparency.Size = New System.Drawing.Size(72, 13)
        Me.lblTransparency.TabIndex = 0
        Me.lblTransparency.Text = "Transparency"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.NumericUpDown1.Location = New System.Drawing.Point(81, 3)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(42, 20)
        Me.NumericUpDown1.TabIndex = 1
        '
        'lblTransparentI
        '
        Me.lblTransparentI.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTransparentI.AutoSize = True
        Me.lblTransparentI.Location = New System.Drawing.Point(129, 6)
        Me.lblTransparentI.Name = "lblTransparentI"
        Me.lblTransparentI.Size = New System.Drawing.Size(100, 13)
        Me.lblTransparentI.TabIndex = 2
        Me.lblTransparentI.Text = "0 is fully transparent"
        '
        'lblFont
        '
        Me.lblFont.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFont.AutoSize = True
        Me.lblFont.Location = New System.Drawing.Point(3, 155)
        Me.lblFont.Name = "lblFont"
        Me.lblFont.Size = New System.Drawing.Size(28, 13)
        Me.lblFont.TabIndex = 14
        Me.lblFont.Text = "Font"
        '
        'cmdFont
        '
        Me.cmdFont.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdFont.AutoSize = True
        Me.cmdFont.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdFont.Location = New System.Drawing.Point(166, 150)
        Me.cmdFont.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.cmdFont.Name = "cmdFont"
        Me.cmdFont.Size = New System.Drawing.Size(38, 23)
        Me.cmdFont.TabIndex = 15
        Me.cmdFont.Text = "Font"
        Me.cmdFont.UseVisualStyleBackColor = True
        '
        'lblAlign
        '
        Me.lblAlign.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblAlign.AutoSize = True
        Me.lblAlign.Location = New System.Drawing.Point(53, 178)
        Me.lblAlign.Name = "lblAlign"
        Me.lblAlign.Size = New System.Drawing.Size(56, 13)
        Me.lblAlign.TabIndex = 16
        Me.lblAlign.Text = "Text align:"
        '
        'tlpAlign
        '
        Me.tlpAlign.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.tlpAlign.AutoSize = True
        Me.tlpAlign.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpAlign.ColumnCount = 3
        Me.tlpAlign.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpAlign.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpAlign.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpAlign.Controls.Add(Me.optTopLeft, 0, 0)
        Me.tlpAlign.Controls.Add(Me.optTopCenter, 1, 0)
        Me.tlpAlign.Controls.Add(Me.optTopRight, 2, 0)
        Me.tlpAlign.Controls.Add(Me.optMiddleLeft, 0, 1)
        Me.tlpAlign.Controls.Add(Me.optMiddleCenter, 1, 1)
        Me.tlpAlign.Controls.Add(Me.optMiddleRight, 2, 1)
        Me.tlpAlign.Controls.Add(Me.optBottomLeft, 0, 2)
        Me.tlpAlign.Controls.Add(Me.optBottomCenter, 1, 2)
        Me.tlpAlign.Controls.Add(Me.optBottomRight, 2, 2)
        Me.tlpAlign.Location = New System.Drawing.Point(51, 199)
        Me.tlpAlign.Name = "tlpAlign"
        Me.tlpAlign.RowCount = 3
        Me.tlpAlign.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpAlign.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpAlign.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpAlign.Size = New System.Drawing.Size(60, 57)
        Me.tlpAlign.TabIndex = 17
        '
        'optTopLeft
        '
        Me.optTopLeft.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.optTopLeft.AutoSize = True
        Me.optTopLeft.Location = New System.Drawing.Point(3, 3)
        Me.optTopLeft.Name = "optTopLeft"
        Me.optTopLeft.Size = New System.Drawing.Size(14, 13)
        Me.optTopLeft.TabIndex = 0
        Me.optTopLeft.TabStop = True
        Me.optTopLeft.UseVisualStyleBackColor = True
        '
        'optTopCenter
        '
        Me.optTopCenter.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.optTopCenter.AutoSize = True
        Me.optTopCenter.Location = New System.Drawing.Point(23, 3)
        Me.optTopCenter.Name = "optTopCenter"
        Me.optTopCenter.Size = New System.Drawing.Size(14, 13)
        Me.optTopCenter.TabIndex = 1
        Me.optTopCenter.TabStop = True
        Me.optTopCenter.UseVisualStyleBackColor = True
        '
        'optTopRight
        '
        Me.optTopRight.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.optTopRight.AutoSize = True
        Me.optTopRight.Location = New System.Drawing.Point(43, 3)
        Me.optTopRight.Name = "optTopRight"
        Me.optTopRight.Size = New System.Drawing.Size(14, 13)
        Me.optTopRight.TabIndex = 2
        Me.optTopRight.TabStop = True
        Me.optTopRight.UseVisualStyleBackColor = True
        '
        'optMiddleLeft
        '
        Me.optMiddleLeft.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.optMiddleLeft.AutoSize = True
        Me.optMiddleLeft.Location = New System.Drawing.Point(3, 22)
        Me.optMiddleLeft.Name = "optMiddleLeft"
        Me.optMiddleLeft.Size = New System.Drawing.Size(14, 13)
        Me.optMiddleLeft.TabIndex = 3
        Me.optMiddleLeft.TabStop = True
        Me.optMiddleLeft.UseVisualStyleBackColor = True
        '
        'optMiddleCenter
        '
        Me.optMiddleCenter.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.optMiddleCenter.AutoSize = True
        Me.optMiddleCenter.Location = New System.Drawing.Point(23, 22)
        Me.optMiddleCenter.Name = "optMiddleCenter"
        Me.optMiddleCenter.Size = New System.Drawing.Size(14, 13)
        Me.optMiddleCenter.TabIndex = 4
        Me.optMiddleCenter.TabStop = True
        Me.optMiddleCenter.UseVisualStyleBackColor = True
        '
        'optMiddleRight
        '
        Me.optMiddleRight.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.optMiddleRight.AutoSize = True
        Me.optMiddleRight.Location = New System.Drawing.Point(43, 22)
        Me.optMiddleRight.Name = "optMiddleRight"
        Me.optMiddleRight.Size = New System.Drawing.Size(14, 13)
        Me.optMiddleRight.TabIndex = 5
        Me.optMiddleRight.TabStop = True
        Me.optMiddleRight.UseVisualStyleBackColor = True
        '
        'optBottomLeft
        '
        Me.optBottomLeft.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.optBottomLeft.AutoSize = True
        Me.optBottomLeft.Location = New System.Drawing.Point(3, 41)
        Me.optBottomLeft.Name = "optBottomLeft"
        Me.optBottomLeft.Size = New System.Drawing.Size(14, 13)
        Me.optBottomLeft.TabIndex = 6
        Me.optBottomLeft.TabStop = True
        Me.optBottomLeft.UseVisualStyleBackColor = True
        '
        'optBottomCenter
        '
        Me.optBottomCenter.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.optBottomCenter.AutoSize = True
        Me.optBottomCenter.Location = New System.Drawing.Point(23, 41)
        Me.optBottomCenter.Name = "optBottomCenter"
        Me.optBottomCenter.Size = New System.Drawing.Size(14, 13)
        Me.optBottomCenter.TabIndex = 7
        Me.optBottomCenter.TabStop = True
        Me.optBottomCenter.UseVisualStyleBackColor = True
        '
        'optBottomRight
        '
        Me.optBottomRight.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.optBottomRight.AutoSize = True
        Me.optBottomRight.Location = New System.Drawing.Point(43, 41)
        Me.optBottomRight.Name = "optBottomRight"
        Me.optBottomRight.Size = New System.Drawing.Size(14, 13)
        Me.optBottomRight.TabIndex = 8
        Me.optBottomRight.TabStop = True
        Me.optBottomRight.UseVisualStyleBackColor = True
        '
        'flpDiplayedText
        '
        Me.flpDiplayedText.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.flpDiplayedText.AutoSize = True
        Me.flpDiplayedText.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpDiplayedText.Controls.Add(Me.lblText)
        Me.flpDiplayedText.Controls.Add(Me.cmdProperties)
        Me.flpDiplayedText.Location = New System.Drawing.Point(302, 173)
        Me.flpDiplayedText.Margin = New System.Windows.Forms.Padding(0)
        Me.flpDiplayedText.Name = "flpDiplayedText"
        Me.flpDiplayedText.Size = New System.Drawing.Size(114, 23)
        Me.flpDiplayedText.TabIndex = 18
        '
        'lblText
        '
        Me.lblText.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblText.AutoSize = True
        Me.lblText.Location = New System.Drawing.Point(3, 5)
        Me.lblText.Name = "lblText"
        Me.lblText.Size = New System.Drawing.Size(76, 13)
        Me.lblText.TabIndex = 9
        Me.lblText.Text = "Displayed text:"
        '
        'cmdProperties
        '
        Me.cmdProperties.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdProperties.AutoSize = True
        Me.cmdProperties.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdProperties.Location = New System.Drawing.Point(85, 0)
        Me.cmdProperties.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.cmdProperties.Name = "cmdProperties"
        Me.cmdProperties.Size = New System.Drawing.Size(26, 23)
        Me.cmdProperties.TabIndex = 10
        Me.cmdProperties.Text = "..."
        Me.cmdProperties.UseVisualStyleBackColor = True
        '
        'fbdFolder
        '
        Me.fbdFolder.Description = "Select folder to load images from"
        '
        'cdlColor
        '
        Me.cdlColor.AnyColor = True
        Me.cdlColor.SolidColorOnly = True
        '
        'fdlFont
        '
        Me.fdlFont.AllowScriptChange = False
        Me.fdlFont.FontMustExist = True
        '
        'frmSettings
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(555, 365)
        Me.Controls.Add(Me.tlpMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettings"
        Me.ShowInTaskbar = False
        Me.Text = "Metanol Screen Saver"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.tlpDir.ResumeLayout(False)
        Me.tlpDir.PerformLayout()
        CType(Me.nudInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.flpOrder.ResumeLayout(False)
        Me.flpOrder.PerformLayout()
        Me.flpColors.ResumeLayout(False)
        Me.flpColors.PerformLayout()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpAlign.ResumeLayout(False)
        Me.tlpAlign.PerformLayout()
        Me.flpDiplayedText.ResumeLayout(False)
        Me.flpDiplayedText.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblDir As System.Windows.Forms.Label
    Friend WithEvents lblInterval As System.Windows.Forms.Label
    Friend WithEvents nudInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMask As System.Windows.Forms.Label
    Friend WithEvents txtMask As System.Windows.Forms.TextBox
    Friend WithEvents lblOrder As System.Windows.Forms.Label
    Friend WithEvents flpOrder As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents optOrderRandom As System.Windows.Forms.RadioButton
    Friend WithEvents optOrderSequential As System.Windows.Forms.RadioButton
    Friend WithEvents txtText As System.Windows.Forms.TextBox
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents fbdFolder As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents tlpDir As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtDir As System.Windows.Forms.TextBox
    Friend WithEvents cmdDir As System.Windows.Forms.Button
    Friend WithEvents lblColors As System.Windows.Forms.Label
    Friend WithEvents flpColors As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents cmdBgInfo As System.Windows.Forms.Button
    Friend WithEvents cmdFgInfo As System.Windows.Forms.Button
    Friend WithEvents cmdBgWin As System.Windows.Forms.Button
    Friend WithEvents cdlColor As System.Windows.Forms.ColorDialog
    Friend WithEvents lblFont As System.Windows.Forms.Label
    Friend WithEvents cmdFont As System.Windows.Forms.Button
    Friend WithEvents fdlFont As System.Windows.Forms.FontDialog
    Friend WithEvents lblAlign As System.Windows.Forms.Label
    Friend WithEvents tlpAlign As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents optTopLeft As System.Windows.Forms.RadioButton
    Friend WithEvents optTopCenter As System.Windows.Forms.RadioButton
    Friend WithEvents optTopRight As System.Windows.Forms.RadioButton
    Friend WithEvents optMiddleLeft As System.Windows.Forms.RadioButton
    Friend WithEvents optMiddleCenter As System.Windows.Forms.RadioButton
    Friend WithEvents optMiddleRight As System.Windows.Forms.RadioButton
    Friend WithEvents optBottomLeft As System.Windows.Forms.RadioButton
    Friend WithEvents optBottomCenter As System.Windows.Forms.RadioButton
    Friend WithEvents optBottomRight As System.Windows.Forms.RadioButton
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblTransparency As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents flpDiplayedText As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblText As System.Windows.Forms.Label
    Friend WithEvents cmdProperties As System.Windows.Forms.Button
    Friend WithEvents lblTransparentI As System.Windows.Forms.Label
End Class
