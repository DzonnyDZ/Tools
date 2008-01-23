<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScreenSettings
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
        Me.lblRoot = New System.Windows.Forms.Label
        Me.tlpRoot = New System.Windows.Forms.TableLayoutPanel
        Me.txtRoot = New System.Windows.Forms.TextBox
        Me.cmdRoot = New System.Windows.Forms.Button
        Me.lblFolderMask = New System.Windows.Forms.Label
        Me.txtFolderMask = New System.Windows.Forms.TextBox
        Me.lblFileMask = New System.Windows.Forms.Label
        Me.txtFileMask = New System.Windows.Forms.TextBox
        Me.lblInterval = New System.Windows.Forms.Label
        Me.flpInterval = New System.Windows.Forms.FlowLayoutPanel
        Me.nudInterval = New System.Windows.Forms.NumericUpDown
        Me.lblIntS = New System.Windows.Forms.Label
        Me.lblIntRand = New System.Windows.Forms.Label
        Me.nudIntRand = New System.Windows.Forms.NumericUpDown
        Me.lblIntRandPrc = New System.Windows.Forms.Label
        Me.lblBgColor = New System.Windows.Forms.Label
        Me.cmdBgColor = New System.Windows.Forms.Button
        Me.lblAlghoritm = New System.Windows.Forms.Label
        Me.flpAlghoritm = New System.Windows.Forms.FlowLayoutPanel
        Me.optARand = New System.Windows.Forms.RadioButton
        Me.optASeq = New System.Windows.Forms.RadioButton
        Me.fraLabelsSettings = New System.Windows.Forms.GroupBox
        Me.flpLabels = New System.Windows.Forms.TableLayoutPanel
        Me.lsgBL = New Metanol.SSaver.LabelSettings
        Me.lsgBC = New Metanol.SSaver.LabelSettings
        Me.lsgBR = New Metanol.SSaver.LabelSettings
        Me.lsgTL = New Metanol.SSaver.LabelSettings
        Me.lsgTC = New Metanol.SSaver.LabelSettings
        Me.lsgTR = New Metanol.SSaver.LabelSettings
        Me.lsgML = New Metanol.SSaver.LabelSettings
        Me.lsgMC = New Metanol.SSaver.LabelSettings
        Me.lsgMR = New Metanol.SSaver.LabelSettings
        Me.lblMaskI = New System.Windows.Forms.Label
        Me.cdlColor = New System.Windows.Forms.ColorDialog
        Me.fbdRoot = New System.Windows.Forms.FolderBrowserDialog
        Me.tlpMain.SuspendLayout()
        Me.tlpRoot.SuspendLayout()
        Me.flpInterval.SuspendLayout()
        CType(Me.nudInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudIntRand, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.flpAlghoritm.SuspendLayout()
        Me.fraLabelsSettings.SuspendLayout()
        Me.flpLabels.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 3
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpMain.Controls.Add(Me.lblRoot, 0, 0)
        Me.tlpMain.Controls.Add(Me.tlpRoot, 1, 0)
        Me.tlpMain.Controls.Add(Me.lblFolderMask, 0, 1)
        Me.tlpMain.Controls.Add(Me.txtFolderMask, 1, 1)
        Me.tlpMain.Controls.Add(Me.lblFileMask, 0, 2)
        Me.tlpMain.Controls.Add(Me.txtFileMask, 1, 2)
        Me.tlpMain.Controls.Add(Me.lblInterval, 0, 3)
        Me.tlpMain.Controls.Add(Me.flpInterval, 1, 3)
        Me.tlpMain.Controls.Add(Me.lblBgColor, 0, 4)
        Me.tlpMain.Controls.Add(Me.cmdBgColor, 1, 4)
        Me.tlpMain.Controls.Add(Me.lblAlghoritm, 0, 5)
        Me.tlpMain.Controls.Add(Me.flpAlghoritm, 1, 5)
        Me.tlpMain.Controls.Add(Me.fraLabelsSettings, 0, 6)
        Me.tlpMain.Controls.Add(Me.lblMaskI, 2, 1)
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpMain.MinimumSize = New System.Drawing.Size(730, 586)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 7
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpMain.Size = New System.Drawing.Size(730, 586)
        Me.tlpMain.TabIndex = 0
        '
        'lblRoot
        '
        Me.lblRoot.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblRoot.AutoSize = True
        Me.lblRoot.Location = New System.Drawing.Point(3, 6)
        Me.lblRoot.Name = "lblRoot"
        Me.lblRoot.Size = New System.Drawing.Size(73, 13)
        Me.lblRoot.TabIndex = 0
        Me.lblRoot.Text = "Root directory"
        '
        'tlpRoot
        '
        Me.tlpRoot.AutoSize = True
        Me.tlpRoot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpRoot.ColumnCount = 2
        Me.tlpMain.SetColumnSpan(Me.tlpRoot, 2)
        Me.tlpRoot.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpRoot.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpRoot.Controls.Add(Me.txtRoot, 0, 0)
        Me.tlpRoot.Controls.Add(Me.cmdRoot, 1, 0)
        Me.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpRoot.Location = New System.Drawing.Point(97, 0)
        Me.tlpRoot.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpRoot.Name = "tlpRoot"
        Me.tlpRoot.RowCount = 1
        Me.tlpRoot.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpRoot.Size = New System.Drawing.Size(633, 26)
        Me.tlpRoot.TabIndex = 1
        '
        'txtRoot
        '
        Me.txtRoot.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRoot.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtRoot.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
        Me.txtRoot.Location = New System.Drawing.Point(3, 3)
        Me.txtRoot.Name = "txtRoot"
        Me.txtRoot.Size = New System.Drawing.Size(595, 20)
        Me.txtRoot.TabIndex = 0
        '
        'cmdRoot
        '
        Me.cmdRoot.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdRoot.AutoSize = True
        Me.cmdRoot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdRoot.Location = New System.Drawing.Point(604, 1)
        Me.cmdRoot.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.cmdRoot.Name = "cmdRoot"
        Me.cmdRoot.Size = New System.Drawing.Size(26, 23)
        Me.cmdRoot.TabIndex = 1
        Me.cmdRoot.Text = "..."
        Me.cmdRoot.UseVisualStyleBackColor = True
        '
        'lblFolderMask
        '
        Me.lblFolderMask.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFolderMask.AutoSize = True
        Me.lblFolderMask.Location = New System.Drawing.Point(3, 32)
        Me.lblFolderMask.Name = "lblFolderMask"
        Me.lblFolderMask.Size = New System.Drawing.Size(64, 13)
        Me.lblFolderMask.TabIndex = 2
        Me.lblFolderMask.Text = "Folder mask"
        '
        'txtFolderMask
        '
        Me.txtFolderMask.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFolderMask.Location = New System.Drawing.Point(100, 29)
        Me.txtFolderMask.Name = "txtFolderMask"
        Me.txtFolderMask.Size = New System.Drawing.Size(559, 20)
        Me.txtFolderMask.TabIndex = 3
        Me.txtFolderMask.Text = "*"
        '
        'lblFileMask
        '
        Me.lblFileMask.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFileMask.AutoSize = True
        Me.lblFileMask.Location = New System.Drawing.Point(3, 58)
        Me.lblFileMask.Name = "lblFileMask"
        Me.lblFileMask.Size = New System.Drawing.Size(51, 13)
        Me.lblFileMask.TabIndex = 5
        Me.lblFileMask.Text = "File mask"
        '
        'txtFileMask
        '
        Me.txtFileMask.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFileMask.Location = New System.Drawing.Point(100, 55)
        Me.txtFileMask.Name = "txtFileMask"
        Me.txtFileMask.Size = New System.Drawing.Size(559, 20)
        Me.txtFileMask.TabIndex = 6
        Me.txtFileMask.Text = "*.jpg;*.jpeg"
        '
        'lblInterval
        '
        Me.lblInterval.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblInterval.AutoSize = True
        Me.lblInterval.Location = New System.Drawing.Point(3, 84)
        Me.lblInterval.Name = "lblInterval"
        Me.lblInterval.Size = New System.Drawing.Size(67, 13)
        Me.lblInterval.TabIndex = 7
        Me.lblInterval.Text = "Time interval"
        '
        'flpInterval
        '
        Me.flpInterval.AutoSize = True
        Me.flpInterval.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpMain.SetColumnSpan(Me.flpInterval, 2)
        Me.flpInterval.Controls.Add(Me.nudInterval)
        Me.flpInterval.Controls.Add(Me.lblIntS)
        Me.flpInterval.Controls.Add(Me.lblIntRand)
        Me.flpInterval.Controls.Add(Me.nudIntRand)
        Me.flpInterval.Controls.Add(Me.lblIntRandPrc)
        Me.flpInterval.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpInterval.Location = New System.Drawing.Point(97, 78)
        Me.flpInterval.Margin = New System.Windows.Forms.Padding(0)
        Me.flpInterval.Name = "flpInterval"
        Me.flpInterval.Size = New System.Drawing.Size(633, 26)
        Me.flpInterval.TabIndex = 8
        '
        'nudInterval
        '
        Me.nudInterval.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.nudInterval.Location = New System.Drawing.Point(3, 3)
        Me.nudInterval.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.nudInterval.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.nudInterval.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudInterval.Name = "nudInterval"
        Me.nudInterval.Size = New System.Drawing.Size(54, 20)
        Me.nudInterval.TabIndex = 0
        Me.nudInterval.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'lblIntS
        '
        Me.lblIntS.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblIntS.AutoSize = True
        Me.lblIntS.Location = New System.Drawing.Point(57, 6)
        Me.lblIntS.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblIntS.Name = "lblIntS"
        Me.lblIntS.Size = New System.Drawing.Size(12, 13)
        Me.lblIntS.TabIndex = 1
        Me.lblIntS.Text = "s"
        '
        'lblIntRand
        '
        Me.lblIntRand.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblIntRand.AutoSize = True
        Me.lblIntRand.Location = New System.Drawing.Point(75, 6)
        Me.lblIntRand.Name = "lblIntRand"
        Me.lblIntRand.Size = New System.Drawing.Size(111, 13)
        Me.lblIntRand.TabIndex = 2
        Me.lblIntRand.Text = "Randomize interval by"
        '
        'nudIntRand
        '
        Me.nudIntRand.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.nudIntRand.Location = New System.Drawing.Point(192, 3)
        Me.nudIntRand.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.nudIntRand.Name = "nudIntRand"
        Me.nudIntRand.Size = New System.Drawing.Size(54, 20)
        Me.nudIntRand.TabIndex = 3
        '
        'lblIntRandPrc
        '
        Me.lblIntRandPrc.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblIntRandPrc.AutoSize = True
        Me.lblIntRandPrc.Location = New System.Drawing.Point(246, 6)
        Me.lblIntRandPrc.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.lblIntRandPrc.Name = "lblIntRandPrc"
        Me.lblIntRandPrc.Size = New System.Drawing.Size(15, 13)
        Me.lblIntRandPrc.TabIndex = 4
        Me.lblIntRandPrc.Text = "%"
        '
        'lblBgColor
        '
        Me.lblBgColor.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblBgColor.AutoSize = True
        Me.lblBgColor.Location = New System.Drawing.Point(3, 109)
        Me.lblBgColor.Name = "lblBgColor"
        Me.lblBgColor.Size = New System.Drawing.Size(91, 13)
        Me.lblBgColor.TabIndex = 9
        Me.lblBgColor.Text = "Background color"
        '
        'cmdBgColor
        '
        Me.cmdBgColor.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdBgColor.AutoSize = True
        Me.cmdBgColor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdBgColor.BackColor = System.Drawing.Color.Black
        Me.tlpMain.SetColumnSpan(Me.cmdBgColor, 2)
        Me.cmdBgColor.FlatAppearance.BorderSize = 0
        Me.cmdBgColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmdBgColor.ForeColor = System.Drawing.Color.White
        Me.cmdBgColor.Location = New System.Drawing.Point(100, 104)
        Me.cmdBgColor.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.cmdBgColor.Name = "cmdBgColor"
        Me.cmdBgColor.Size = New System.Drawing.Size(103, 23)
        Me.cmdBgColor.TabIndex = 10
        Me.cmdBgColor.Text = "Click to change ..."
        Me.cmdBgColor.UseVisualStyleBackColor = False
        '
        'lblAlghoritm
        '
        Me.lblAlghoritm.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblAlghoritm.AutoSize = True
        Me.lblAlghoritm.Location = New System.Drawing.Point(3, 132)
        Me.lblAlghoritm.Name = "lblAlghoritm"
        Me.lblAlghoritm.Size = New System.Drawing.Size(62, 13)
        Me.lblAlghoritm.TabIndex = 11
        Me.lblAlghoritm.Text = "Sort images"
        '
        'flpAlghoritm
        '
        Me.flpAlghoritm.AutoSize = True
        Me.flpAlghoritm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpMain.SetColumnSpan(Me.flpAlghoritm, 2)
        Me.flpAlghoritm.Controls.Add(Me.optARand)
        Me.flpAlghoritm.Controls.Add(Me.optASeq)
        Me.flpAlghoritm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpAlghoritm.Location = New System.Drawing.Point(97, 127)
        Me.flpAlghoritm.Margin = New System.Windows.Forms.Padding(0)
        Me.flpAlghoritm.Name = "flpAlghoritm"
        Me.flpAlghoritm.Size = New System.Drawing.Size(633, 23)
        Me.flpAlghoritm.TabIndex = 12
        '
        'optARand
        '
        Me.optARand.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.optARand.AutoSize = True
        Me.optARand.Location = New System.Drawing.Point(3, 3)
        Me.optARand.Name = "optARand"
        Me.optARand.Size = New System.Drawing.Size(65, 17)
        Me.optARand.TabIndex = 0
        Me.optARand.TabStop = True
        Me.optARand.Text = "Random"
        Me.optARand.UseVisualStyleBackColor = True
        '
        'optASeq
        '
        Me.optASeq.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.optASeq.AutoSize = True
        Me.optASeq.Location = New System.Drawing.Point(74, 3)
        Me.optASeq.Name = "optASeq"
        Me.optASeq.Size = New System.Drawing.Size(115, 17)
        Me.optASeq.TabIndex = 1
        Me.optASeq.TabStop = True
        Me.optASeq.Text = "As they are on disk"
        Me.optASeq.UseVisualStyleBackColor = True
        '
        'fraLabelsSettings
        '
        Me.tlpMain.SetColumnSpan(Me.fraLabelsSettings, 3)
        Me.fraLabelsSettings.Controls.Add(Me.flpLabels)
        Me.fraLabelsSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fraLabelsSettings.Location = New System.Drawing.Point(0, 150)
        Me.fraLabelsSettings.Margin = New System.Windows.Forms.Padding(0)
        Me.fraLabelsSettings.MinimumSize = New System.Drawing.Size(730, 440)
        Me.fraLabelsSettings.Name = "fraLabelsSettings"
        Me.fraLabelsSettings.Padding = New System.Windows.Forms.Padding(0)
        Me.fraLabelsSettings.Size = New System.Drawing.Size(730, 440)
        Me.fraLabelsSettings.TabIndex = 13
        Me.fraLabelsSettings.TabStop = False
        Me.fraLabelsSettings.Text = "Information labels (as positioned on screen)"
        '
        'flpLabels
        '
        Me.flpLabels.ColumnCount = 3
        Me.flpLabels.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.flpLabels.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.flpLabels.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.flpLabels.Controls.Add(Me.lsgBL, 0, 2)
        Me.flpLabels.Controls.Add(Me.lsgBC, 0, 2)
        Me.flpLabels.Controls.Add(Me.lsgBR, 0, 2)
        Me.flpLabels.Controls.Add(Me.lsgTL, 0, 0)
        Me.flpLabels.Controls.Add(Me.lsgTC, 1, 0)
        Me.flpLabels.Controls.Add(Me.lsgTR, 2, 0)
        Me.flpLabels.Controls.Add(Me.lsgML, 0, 1)
        Me.flpLabels.Controls.Add(Me.lsgMC, 1, 1)
        Me.flpLabels.Controls.Add(Me.lsgMR, 2, 1)
        Me.flpLabels.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpLabels.Location = New System.Drawing.Point(0, 13)
        Me.flpLabels.Margin = New System.Windows.Forms.Padding(0)
        Me.flpLabels.MinimumSize = New System.Drawing.Size(730, 427)
        Me.flpLabels.Name = "flpLabels"
        Me.flpLabels.RowCount = 3
        Me.flpLabels.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.flpLabels.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.flpLabels.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.flpLabels.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.flpLabels.Size = New System.Drawing.Size(730, 427)
        Me.flpLabels.TabIndex = 0
        '
        'lsgBL
        '
        Me.lsgBL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsgBL.Location = New System.Drawing.Point(0, 284)
        Me.lsgBL.Margin = New System.Windows.Forms.Padding(0)
        Me.lsgBL.MinimumSize = New System.Drawing.Size(243, 140)
        Me.lsgBL.Name = "lsgBL"
        Me.lsgBL.Size = New System.Drawing.Size(243, 143)
        Me.lsgBL.TabIndex = 6
        '
        'lsgBC
        '
        Me.lsgBC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsgBC.Location = New System.Drawing.Point(243, 284)
        Me.lsgBC.Margin = New System.Windows.Forms.Padding(0)
        Me.lsgBC.MinimumSize = New System.Drawing.Size(243, 140)
        Me.lsgBC.Name = "lsgBC"
        Me.lsgBC.Size = New System.Drawing.Size(243, 143)
        Me.lsgBC.TabIndex = 7
        '
        'lsgBR
        '
        Me.lsgBR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsgBR.Location = New System.Drawing.Point(486, 284)
        Me.lsgBR.Margin = New System.Windows.Forms.Padding(0)
        Me.lsgBR.MinimumSize = New System.Drawing.Size(243, 140)
        Me.lsgBR.Name = "lsgBR"
        Me.lsgBR.Size = New System.Drawing.Size(244, 143)
        Me.lsgBR.TabIndex = 8
        '
        'lsgTL
        '
        Me.lsgTL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsgTL.Location = New System.Drawing.Point(0, 0)
        Me.lsgTL.Margin = New System.Windows.Forms.Padding(0)
        Me.lsgTL.MinimumSize = New System.Drawing.Size(243, 140)
        Me.lsgTL.Name = "lsgTL"
        Me.lsgTL.Size = New System.Drawing.Size(243, 142)
        Me.lsgTL.TabIndex = 0
        '
        'lsgTC
        '
        Me.lsgTC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsgTC.Location = New System.Drawing.Point(243, 0)
        Me.lsgTC.Margin = New System.Windows.Forms.Padding(0)
        Me.lsgTC.MinimumSize = New System.Drawing.Size(243, 140)
        Me.lsgTC.Name = "lsgTC"
        Me.lsgTC.Size = New System.Drawing.Size(243, 142)
        Me.lsgTC.TabIndex = 1
        '
        'lsgTR
        '
        Me.lsgTR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsgTR.Location = New System.Drawing.Point(486, 0)
        Me.lsgTR.Margin = New System.Windows.Forms.Padding(0)
        Me.lsgTR.MinimumSize = New System.Drawing.Size(243, 140)
        Me.lsgTR.Name = "lsgTR"
        Me.lsgTR.Size = New System.Drawing.Size(244, 142)
        Me.lsgTR.TabIndex = 2
        '
        'lsgML
        '
        Me.lsgML.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsgML.Location = New System.Drawing.Point(0, 142)
        Me.lsgML.Margin = New System.Windows.Forms.Padding(0)
        Me.lsgML.MinimumSize = New System.Drawing.Size(243, 140)
        Me.lsgML.Name = "lsgML"
        Me.lsgML.Size = New System.Drawing.Size(243, 142)
        Me.lsgML.TabIndex = 3
        '
        'lsgMC
        '
        Me.lsgMC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsgMC.Location = New System.Drawing.Point(243, 142)
        Me.lsgMC.Margin = New System.Windows.Forms.Padding(0)
        Me.lsgMC.MinimumSize = New System.Drawing.Size(243, 140)
        Me.lsgMC.Name = "lsgMC"
        Me.lsgMC.Size = New System.Drawing.Size(243, 142)
        Me.lsgMC.TabIndex = 4
        '
        'lsgMR
        '
        Me.lsgMR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsgMR.Location = New System.Drawing.Point(486, 142)
        Me.lsgMR.Margin = New System.Windows.Forms.Padding(0)
        Me.lsgMR.MinimumSize = New System.Drawing.Size(243, 140)
        Me.lsgMR.Name = "lsgMR"
        Me.lsgMR.Size = New System.Drawing.Size(244, 142)
        Me.lsgMR.TabIndex = 5
        '
        'lblMaskI
        '
        Me.lblMaskI.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMaskI.Location = New System.Drawing.Point(662, 26)
        Me.lblMaskI.Margin = New System.Windows.Forms.Padding(0)
        Me.lblMaskI.Name = "lblMaskI"
        Me.tlpMain.SetRowSpan(Me.lblMaskI, 2)
        Me.lblMaskI.Size = New System.Drawing.Size(68, 52)
        Me.lblMaskI.TabIndex = 4
        Me.lblMaskI.Text = "Separate by semicolons (;)"
        Me.lblMaskI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cdlColor
        '
        Me.cdlColor.AnyColor = True
        '
        'ScreenSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.Controls.Add(Me.tlpMain)
        Me.Name = "ScreenSettings"
        Me.Size = New System.Drawing.Size(730, 586)
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.tlpRoot.ResumeLayout(False)
        Me.tlpRoot.PerformLayout()
        Me.flpInterval.ResumeLayout(False)
        Me.flpInterval.PerformLayout()
        CType(Me.nudInterval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudIntRand, System.ComponentModel.ISupportInitialize).EndInit()
        Me.flpAlghoritm.ResumeLayout(False)
        Me.flpAlghoritm.PerformLayout()
        Me.fraLabelsSettings.ResumeLayout(False)
        Me.flpLabels.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblRoot As System.Windows.Forms.Label
    Friend WithEvents tlpRoot As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtRoot As System.Windows.Forms.TextBox
    Friend WithEvents cmdRoot As System.Windows.Forms.Button
    Friend WithEvents lblFolderMask As System.Windows.Forms.Label
    Friend WithEvents txtFolderMask As System.Windows.Forms.TextBox
    Friend WithEvents lblFileMask As System.Windows.Forms.Label
    Friend WithEvents txtFileMask As System.Windows.Forms.TextBox
    Friend WithEvents lblInterval As System.Windows.Forms.Label
    Friend WithEvents flpInterval As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents nudInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblIntRand As System.Windows.Forms.Label
    Friend WithEvents nudIntRand As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblBgColor As System.Windows.Forms.Label
    Friend WithEvents cmdBgColor As System.Windows.Forms.Button
    Friend WithEvents lblAlghoritm As System.Windows.Forms.Label
    Friend WithEvents flpAlghoritm As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents optARand As System.Windows.Forms.RadioButton
    Friend WithEvents optASeq As System.Windows.Forms.RadioButton
    Friend WithEvents fraLabelsSettings As System.Windows.Forms.GroupBox
    Friend WithEvents lblIntRandPrc As System.Windows.Forms.Label
    Friend WithEvents lblMaskI As System.Windows.Forms.Label
    Friend WithEvents lblIntS As System.Windows.Forms.Label
    Friend WithEvents flpLabels As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lsgTL As Metanol.SSaver.LabelSettings
    Friend WithEvents lsgBL As Metanol.SSaver.LabelSettings
    Friend WithEvents lsgBC As Metanol.SSaver.LabelSettings
    Friend WithEvents lsgBR As Metanol.SSaver.LabelSettings
    Friend WithEvents lsgTC As Metanol.SSaver.LabelSettings
    Friend WithEvents lsgTR As Metanol.SSaver.LabelSettings
    Friend WithEvents lsgML As Metanol.SSaver.LabelSettings
    Friend WithEvents lsgMC As Metanol.SSaver.LabelSettings
    Friend WithEvents lsgMR As Metanol.SSaver.LabelSettings
    Friend WithEvents cdlColor As System.Windows.Forms.ColorDialog
    Friend WithEvents fbdRoot As System.Windows.Forms.FolderBrowserDialog

End Class
