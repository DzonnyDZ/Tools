Namespace DevicesT.RawInputT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmRawInput
        Inherits Tools.WindowsT.FormsT.ExtendedForm

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
            Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
            Me.splEnumeration = New System.Windows.Forms.SplitContainer
            Me.splDeviceList = New System.Windows.Forms.SplitContainer
            Me.tlpList = New System.Windows.Forms.TableLayoutPanel
            Me.cmdGetRawInputDeviceList = New System.Windows.Forms.Button
            Me.lstDevices = New System.Windows.Forms.ListBox
            Me.fraDevCap = New System.Windows.Forms.GroupBox
            Me.prgDevCap = New System.Windows.Forms.PropertyGrid
            Me.tlpProperties = New System.Windows.Forms.TableLayoutPanel
            Me.lblDeviceNameI = New System.Windows.Forms.Label
            Me.lblDeviceName = New System.Windows.Forms.Label
            Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
            Me.prgDeviceInfo = New System.Windows.Forms.PropertyGrid
            Me.lblDeviceInfoI = New System.Windows.Forms.Label
            Me.prgName = New System.Windows.Forms.PropertyGrid
            Me.lblRawDeviceNameI = New System.Windows.Forms.Label
            Me.lblDeviceDescriptionI = New System.Windows.Forms.Label
            Me.lblDeviceDescription = New System.Windows.Forms.Label
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.fraEnumeration = New System.Windows.Forms.GroupBox
            Me.splRegistration = New System.Windows.Forms.SplitContainer
            Me.splRegistrationTop = New System.Windows.Forms.SplitContainer
            Me.tvwHid = New System.Windows.Forms.TreeView
            Me.fraEventLog = New System.Windows.Forms.GroupBox
            Me.splEventLog = New System.Windows.Forms.SplitContainer
            Me.tlpEventLog = New System.Windows.Forms.TableLayoutPanel
            Me.dgwEvents = New System.Windows.Forms.DataGridView
            Me.txcTime = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txcEvent = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txcEventDeviceType = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txcEventDevice = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.cmdClearEventLog = New System.Windows.Forms.Button
            Me.flpEventChecks = New System.Windows.Forms.FlowLayoutPanel
            Me.chkKeyboard = New System.Windows.Forms.CheckBox
            Me.chkMouse = New System.Windows.Forms.CheckBox
            Me.chkHID = New System.Windows.Forms.CheckBox
            Me.chkMediaCenter = New System.Windows.Forms.CheckBox
            Me.chkEvAutoScroll = New System.Windows.Forms.CheckBox
            Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
            Me.prgEvent = New System.Windows.Forms.PropertyGrid
            Me.cmdWhich = New System.Windows.Forms.Button
            Me.tlpNonRawEvents = New System.Windows.Forms.TableLayoutPanel
            Me.dgwNonRawEventLog = New System.Windows.Forms.DataGridView
            Me.txcNonRawTime = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txcNonRawType = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txcNoRawDetail = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.cmdClearNonRaw = New System.Windows.Forms.Button
            Me.flpNonRawChecks = New System.Windows.Forms.FlowLayoutPanel
            Me.chkNonRawKyeboard = New System.Windows.Forms.CheckBox
            Me.chkNonRawMouse = New System.Windows.Forms.CheckBox
            Me.chkNonRawApp = New System.Windows.Forms.CheckBox
            Me.chkNonRawAutoScroll = New System.Windows.Forms.CheckBox
            Me.fraRegistration = New System.Windows.Forms.GroupBox
            Me.tlpRegistration = New System.Windows.Forms.TableLayoutPanel
            Me.dgwRegistration = New System.Windows.Forms.DataGridView
            Me.txcUsagePage = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txcUsage = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.chcApplicationKeys = New System.Windows.Forms.DataGridViewCheckBoxColumn
            Me.txcBackgroundEvents = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.chcCaptureMouse = New System.Windows.Forms.DataGridViewCheckBoxColumn
            Me.chcDisableLegacyEvents = New System.Windows.Forms.DataGridViewCheckBoxColumn
            Me.chcExclude = New System.Windows.Forms.DataGridViewCheckBoxColumn
            Me.chcHotKeys = New System.Windows.Forms.DataGridViewCheckBoxColumn
            Me.chcRemove = New System.Windows.Forms.DataGridViewCheckBoxColumn
            Me.chcWholePage = New System.Windows.Forms.DataGridViewCheckBoxColumn
            Me.txcWindow = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.lblUsagePage = New System.Windows.Forms.Label
            Me.nudUsagePage = New System.Windows.Forms.NumericUpDown
            Me.lblUsage = New System.Windows.Forms.Label
            Me.nudUsage = New System.Windows.Forms.NumericUpDown
            Me.cmdAdd = New System.Windows.Forms.Button
            Me.flpRegistrationButtons = New System.Windows.Forms.FlowLayoutPanel
            Me.cmdRegister = New System.Windows.Forms.Button
            Me.cmdUnregister = New System.Windows.Forms.Button
            Me.cmdLoadRegistered = New System.Windows.Forms.Button
            Me.cmdUnregisterAll = New System.Windows.Forms.Button
            Me.cmdClear = New System.Windows.Forms.Button
            Me.cmdMediaCenterRemote = New System.Windows.Forms.Button
            Me.chkHex = New System.Windows.Forms.CheckBox
            Me.totToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.splEnumeration.Panel1.SuspendLayout()
            Me.splEnumeration.Panel2.SuspendLayout()
            Me.splEnumeration.SuspendLayout()
            Me.splDeviceList.Panel1.SuspendLayout()
            Me.splDeviceList.Panel2.SuspendLayout()
            Me.splDeviceList.SuspendLayout()
            Me.tlpList.SuspendLayout()
            Me.fraDevCap.SuspendLayout()
            Me.tlpProperties.SuspendLayout()
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.fraEnumeration.SuspendLayout()
            Me.splRegistration.Panel1.SuspendLayout()
            Me.splRegistration.Panel2.SuspendLayout()
            Me.splRegistration.SuspendLayout()
            Me.splRegistrationTop.Panel1.SuspendLayout()
            Me.splRegistrationTop.Panel2.SuspendLayout()
            Me.splRegistrationTop.SuspendLayout()
            Me.fraEventLog.SuspendLayout()
            Me.splEventLog.Panel1.SuspendLayout()
            Me.splEventLog.Panel2.SuspendLayout()
            Me.splEventLog.SuspendLayout()
            Me.tlpEventLog.SuspendLayout()
            CType(Me.dgwEvents, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.flpEventChecks.SuspendLayout()
            Me.SplitContainer2.Panel1.SuspendLayout()
            Me.SplitContainer2.Panel2.SuspendLayout()
            Me.SplitContainer2.SuspendLayout()
            Me.tlpNonRawEvents.SuspendLayout()
            CType(Me.dgwNonRawEventLog, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.flpNonRawChecks.SuspendLayout()
            Me.fraRegistration.SuspendLayout()
            Me.tlpRegistration.SuspendLayout()
            CType(Me.dgwRegistration, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.nudUsagePage, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.nudUsage, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.flpRegistrationButtons.SuspendLayout()
            Me.SuspendLayout()
            '
            'splEnumeration
            '
            Me.splEnumeration.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splEnumeration.Location = New System.Drawing.Point(3, 16)
            Me.splEnumeration.Name = "splEnumeration"
            '
            'splEnumeration.Panel1
            '
            Me.splEnumeration.Panel1.Controls.Add(Me.splDeviceList)
            '
            'splEnumeration.Panel2
            '
            Me.splEnumeration.Panel2.Controls.Add(Me.tlpProperties)
            Me.splEnumeration.Size = New System.Drawing.Size(417, 517)
            Me.splEnumeration.SplitterDistance = 167
            Me.splEnumeration.TabIndex = 0
            '
            'splDeviceList
            '
            Me.splDeviceList.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splDeviceList.Location = New System.Drawing.Point(0, 0)
            Me.splDeviceList.Margin = New System.Windows.Forms.Padding(0)
            Me.splDeviceList.Name = "splDeviceList"
            Me.splDeviceList.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'splDeviceList.Panel1
            '
            Me.splDeviceList.Panel1.Controls.Add(Me.tlpList)
            '
            'splDeviceList.Panel2
            '
            Me.splDeviceList.Panel2.Controls.Add(Me.fraDevCap)
            Me.splDeviceList.Size = New System.Drawing.Size(167, 517)
            Me.splDeviceList.SplitterDistance = 230
            Me.splDeviceList.TabIndex = 1
            '
            'tlpList
            '
            Me.tlpList.ColumnCount = 1
            Me.tlpList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpList.Controls.Add(Me.cmdGetRawInputDeviceList, 0, 0)
            Me.tlpList.Controls.Add(Me.lstDevices, 0, 1)
            Me.tlpList.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpList.Location = New System.Drawing.Point(0, 0)
            Me.tlpList.Name = "tlpList"
            Me.tlpList.RowCount = 2
            Me.tlpList.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpList.Size = New System.Drawing.Size(167, 230)
            Me.tlpList.TabIndex = 0
            '
            'cmdGetRawInputDeviceList
            '
            Me.cmdGetRawInputDeviceList.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdGetRawInputDeviceList.AutoSize = True
            Me.cmdGetRawInputDeviceList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdGetRawInputDeviceList.Location = New System.Drawing.Point(18, 3)
            Me.cmdGetRawInputDeviceList.Name = "cmdGetRawInputDeviceList"
            Me.cmdGetRawInputDeviceList.Size = New System.Drawing.Size(130, 23)
            Me.cmdGetRawInputDeviceList.TabIndex = 0
            Me.cmdGetRawInputDeviceList.Text = "&GetRawInputDeviceList"
            Me.totToolTip.SetToolTip(Me.cmdGetRawInputDeviceList, "Enlists all devices")
            Me.cmdGetRawInputDeviceList.UseVisualStyleBackColor = True
            '
            'lstDevices
            '
            Me.lstDevices.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lstDevices.FormattingEnabled = True
            Me.lstDevices.IntegralHeight = False
            Me.lstDevices.Location = New System.Drawing.Point(3, 32)
            Me.lstDevices.Name = "lstDevices"
            Me.lstDevices.Size = New System.Drawing.Size(161, 195)
            Me.lstDevices.TabIndex = 1
            '
            'fraDevCap
            '
            Me.fraDevCap.Controls.Add(Me.prgDevCap)
            Me.fraDevCap.Dock = System.Windows.Forms.DockStyle.Fill
            Me.fraDevCap.Location = New System.Drawing.Point(0, 0)
            Me.fraDevCap.Margin = New System.Windows.Forms.Padding(0)
            Me.fraDevCap.Name = "fraDevCap"
            Me.fraDevCap.Size = New System.Drawing.Size(167, 283)
            Me.fraDevCap.TabIndex = 0
            Me.fraDevCap.TabStop = False
            Me.fraDevCap.Text = "Capabilities"
            '
            'prgDevCap
            '
            Me.prgDevCap.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgDevCap.Location = New System.Drawing.Point(3, 16)
            Me.prgDevCap.Name = "prgDevCap"
            Me.prgDevCap.Size = New System.Drawing.Size(161, 264)
            Me.prgDevCap.TabIndex = 0
            '
            'tlpProperties
            '
            Me.tlpProperties.ColumnCount = 2
            Me.tlpProperties.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpProperties.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpProperties.Controls.Add(Me.lblDeviceNameI, 0, 0)
            Me.tlpProperties.Controls.Add(Me.lblDeviceName, 1, 0)
            Me.tlpProperties.Controls.Add(Me.SplitContainer1, 0, 1)
            Me.tlpProperties.Controls.Add(Me.lblDeviceDescriptionI, 0, 2)
            Me.tlpProperties.Controls.Add(Me.lblDeviceDescription, 1, 2)
            Me.tlpProperties.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpProperties.Location = New System.Drawing.Point(0, 0)
            Me.tlpProperties.Name = "tlpProperties"
            Me.tlpProperties.RowCount = 3
            Me.tlpProperties.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpProperties.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpProperties.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpProperties.Size = New System.Drawing.Size(246, 517)
            Me.tlpProperties.TabIndex = 1
            '
            'lblDeviceNameI
            '
            Me.lblDeviceNameI.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblDeviceNameI.AutoSize = True
            Me.lblDeviceNameI.Location = New System.Drawing.Point(28, 0)
            Me.lblDeviceNameI.Name = "lblDeviceNameI"
            Me.lblDeviceNameI.Size = New System.Drawing.Size(70, 13)
            Me.lblDeviceNameI.TabIndex = 0
            Me.lblDeviceNameI.Text = "Device name"
            Me.totToolTip.SetToolTip(Me.lblDeviceNameI, "Device file name")
            '
            'lblDeviceName
            '
            Me.lblDeviceName.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.lblDeviceName.AutoSize = True
            Me.lblDeviceName.Location = New System.Drawing.Point(104, 0)
            Me.lblDeviceName.Name = "lblDeviceName"
            Me.lblDeviceName.Size = New System.Drawing.Size(0, 13)
            Me.lblDeviceName.TabIndex = 1
            Me.totToolTip.SetToolTip(Me.lblDeviceName, "Device file name")
            '
            'SplitContainer1
            '
            Me.tlpProperties.SetColumnSpan(Me.SplitContainer1, 2)
            Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer1.Location = New System.Drawing.Point(3, 16)
            Me.SplitContainer1.Name = "SplitContainer1"
            Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'SplitContainer1.Panel1
            '
            Me.SplitContainer1.Panel1.Controls.Add(Me.prgDeviceInfo)
            Me.SplitContainer1.Panel1.Controls.Add(Me.lblDeviceInfoI)
            '
            'SplitContainer1.Panel2
            '
            Me.SplitContainer1.Panel2.Controls.Add(Me.prgName)
            Me.SplitContainer1.Panel2.Controls.Add(Me.lblRawDeviceNameI)
            Me.SplitContainer1.Size = New System.Drawing.Size(240, 485)
            Me.SplitContainer1.SplitterDistance = 242
            Me.SplitContainer1.TabIndex = 3
            '
            'prgDeviceInfo
            '
            Me.prgDeviceInfo.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgDeviceInfo.Location = New System.Drawing.Point(0, 13)
            Me.prgDeviceInfo.Margin = New System.Windows.Forms.Padding(0)
            Me.prgDeviceInfo.Name = "prgDeviceInfo"
            Me.prgDeviceInfo.Size = New System.Drawing.Size(240, 229)
            Me.prgDeviceInfo.TabIndex = 4
            '
            'lblDeviceInfoI
            '
            Me.lblDeviceInfoI.AutoSize = True
            Me.lblDeviceInfoI.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblDeviceInfoI.Location = New System.Drawing.Point(0, 0)
            Me.lblDeviceInfoI.Name = "lblDeviceInfoI"
            Me.lblDeviceInfoI.Size = New System.Drawing.Size(61, 13)
            Me.lblDeviceInfoI.TabIndex = 5
            Me.lblDeviceInfoI.Text = "Device info"
            '
            'prgName
            '
            Me.prgName.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgName.Location = New System.Drawing.Point(0, 13)
            Me.prgName.Margin = New System.Windows.Forms.Padding(0)
            Me.prgName.Name = "prgName"
            Me.prgName.Size = New System.Drawing.Size(240, 226)
            Me.prgName.TabIndex = 4
            '
            'lblRawDeviceNameI
            '
            Me.lblRawDeviceNameI.AutoSize = True
            Me.lblRawDeviceNameI.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblRawDeviceNameI.Location = New System.Drawing.Point(0, 0)
            Me.lblRawDeviceNameI.Name = "lblRawDeviceNameI"
            Me.lblRawDeviceNameI.Size = New System.Drawing.Size(93, 13)
            Me.lblRawDeviceNameI.TabIndex = 5
            Me.lblRawDeviceNameI.Text = "Raw device name"
            Me.totToolTip.SetToolTip(Me.lblRawDeviceNameI, "Grid below shows information parsed from device name")
            '
            'lblDeviceDescriptionI
            '
            Me.lblDeviceDescriptionI.AutoSize = True
            Me.lblDeviceDescriptionI.Location = New System.Drawing.Point(3, 504)
            Me.lblDeviceDescriptionI.Name = "lblDeviceDescriptionI"
            Me.lblDeviceDescriptionI.Size = New System.Drawing.Size(95, 13)
            Me.lblDeviceDescriptionI.TabIndex = 4
            Me.lblDeviceDescriptionI.Text = "Device description"
            Me.totToolTip.SetToolTip(Me.lblDeviceDescriptionI, "escription obtained from registry")
            '
            'lblDeviceDescription
            '
            Me.lblDeviceDescription.AutoSize = True
            Me.lblDeviceDescription.Location = New System.Drawing.Point(104, 504)
            Me.lblDeviceDescription.Name = "lblDeviceDescription"
            Me.lblDeviceDescription.Size = New System.Drawing.Size(0, 13)
            Me.lblDeviceDescription.TabIndex = 5
            Me.totToolTip.SetToolTip(Me.lblDeviceDescription, "escription obtained from registry")
            '
            'splMain
            '
            Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splMain.Location = New System.Drawing.Point(0, 0)
            Me.splMain.Name = "splMain"
            '
            'splMain.Panel1
            '
            Me.splMain.Panel1.Controls.Add(Me.fraEnumeration)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.splRegistration)
            Me.splMain.Size = New System.Drawing.Size(1200, 536)
            Me.splMain.SplitterDistance = 423
            Me.splMain.TabIndex = 1
            '
            'fraEnumeration
            '
            Me.fraEnumeration.Controls.Add(Me.splEnumeration)
            Me.fraEnumeration.Dock = System.Windows.Forms.DockStyle.Fill
            Me.fraEnumeration.Location = New System.Drawing.Point(0, 0)
            Me.fraEnumeration.Margin = New System.Windows.Forms.Padding(0)
            Me.fraEnumeration.Name = "fraEnumeration"
            Me.fraEnumeration.Size = New System.Drawing.Size(423, 536)
            Me.fraEnumeration.TabIndex = 1
            Me.fraEnumeration.TabStop = False
            Me.fraEnumeration.Text = "Device listing and properties"
            '
            'splRegistration
            '
            Me.splRegistration.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splRegistration.Location = New System.Drawing.Point(0, 0)
            Me.splRegistration.Name = "splRegistration"
            Me.splRegistration.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'splRegistration.Panel1
            '
            Me.splRegistration.Panel1.Controls.Add(Me.splRegistrationTop)
            '
            'splRegistration.Panel2
            '
            Me.splRegistration.Panel2.Controls.Add(Me.fraRegistration)
            Me.splRegistration.Size = New System.Drawing.Size(773, 536)
            Me.splRegistration.SplitterDistance = 258
            Me.splRegistration.TabIndex = 1
            '
            'splRegistrationTop
            '
            Me.splRegistrationTop.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splRegistrationTop.Location = New System.Drawing.Point(0, 0)
            Me.splRegistrationTop.Name = "splRegistrationTop"
            '
            'splRegistrationTop.Panel1
            '
            Me.splRegistrationTop.Panel1.Controls.Add(Me.tvwHid)
            '
            'splRegistrationTop.Panel2
            '
            Me.splRegistrationTop.Panel2.Controls.Add(Me.fraEventLog)
            Me.splRegistrationTop.Size = New System.Drawing.Size(773, 258)
            Me.splRegistrationTop.SplitterDistance = 145
            Me.splRegistrationTop.TabIndex = 2
            '
            'tvwHid
            '
            Me.tvwHid.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tvwHid.Location = New System.Drawing.Point(0, 0)
            Me.tvwHid.Name = "tvwHid"
            Me.tvwHid.Size = New System.Drawing.Size(145, 258)
            Me.tvwHid.TabIndex = 0
            '
            'fraEventLog
            '
            Me.fraEventLog.Controls.Add(Me.splEventLog)
            Me.fraEventLog.Dock = System.Windows.Forms.DockStyle.Fill
            Me.fraEventLog.Location = New System.Drawing.Point(0, 0)
            Me.fraEventLog.Margin = New System.Windows.Forms.Padding(0)
            Me.fraEventLog.Name = "fraEventLog"
            Me.fraEventLog.Size = New System.Drawing.Size(624, 258)
            Me.fraEventLog.TabIndex = 1
            Me.fraEventLog.TabStop = False
            Me.fraEventLog.Text = "Events"
            '
            'splEventLog
            '
            Me.splEventLog.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splEventLog.Location = New System.Drawing.Point(3, 16)
            Me.splEventLog.Name = "splEventLog"
            '
            'splEventLog.Panel1
            '
            Me.splEventLog.Panel1.Controls.Add(Me.tlpEventLog)
            '
            'splEventLog.Panel2
            '
            Me.splEventLog.Panel2.Controls.Add(Me.SplitContainer2)
            Me.splEventLog.Size = New System.Drawing.Size(618, 239)
            Me.splEventLog.SplitterDistance = 238
            Me.splEventLog.TabIndex = 2
            '
            'tlpEventLog
            '
            Me.tlpEventLog.ColumnCount = 2
            Me.tlpEventLog.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpEventLog.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpEventLog.Controls.Add(Me.dgwEvents, 0, 0)
            Me.tlpEventLog.Controls.Add(Me.cmdClearEventLog, 0, 2)
            Me.tlpEventLog.Controls.Add(Me.flpEventChecks, 0, 1)
            Me.tlpEventLog.Controls.Add(Me.chkEvAutoScroll, 1, 2)
            Me.tlpEventLog.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpEventLog.Location = New System.Drawing.Point(0, 0)
            Me.tlpEventLog.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpEventLog.Name = "tlpEventLog"
            Me.tlpEventLog.RowCount = 3
            Me.tlpEventLog.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpEventLog.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpEventLog.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpEventLog.Size = New System.Drawing.Size(238, 239)
            Me.tlpEventLog.TabIndex = 1
            '
            'dgwEvents
            '
            Me.dgwEvents.AllowUserToAddRows = False
            Me.dgwEvents.AllowUserToDeleteRows = False
            Me.dgwEvents.BackgroundColor = System.Drawing.SystemColors.Window
            Me.dgwEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgwEvents.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.txcTime, Me.txcEvent, Me.txcEventDeviceType, Me.txcEventDevice})
            Me.tlpEventLog.SetColumnSpan(Me.dgwEvents, 2)
            Me.dgwEvents.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgwEvents.Location = New System.Drawing.Point(0, 0)
            Me.dgwEvents.Margin = New System.Windows.Forms.Padding(0)
            Me.dgwEvents.MultiSelect = False
            Me.dgwEvents.Name = "dgwEvents"
            Me.dgwEvents.ReadOnly = True
            Me.dgwEvents.RowHeadersVisible = False
            Me.dgwEvents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgwEvents.Size = New System.Drawing.Size(238, 193)
            Me.dgwEvents.TabIndex = 0
            '
            'txcTime
            '
            DataGridViewCellStyle1.Format = "hh:mm:ss.ff"
            DataGridViewCellStyle1.NullValue = Nothing
            Me.txcTime.DefaultCellStyle = DataGridViewCellStyle1
            Me.txcTime.HeaderText = "Time"
            Me.txcTime.Name = "txcTime"
            Me.txcTime.ReadOnly = True
            Me.txcTime.Width = 50
            '
            'txcEvent
            '
            Me.txcEvent.HeaderText = "Name"
            Me.txcEvent.Name = "txcEvent"
            Me.txcEvent.ReadOnly = True
            '
            'txcEventDeviceType
            '
            Me.txcEventDeviceType.DataPropertyName = "DeviceType"
            Me.txcEventDeviceType.HeaderText = "DeviceType"
            Me.txcEventDeviceType.Name = "txcEventDeviceType"
            Me.txcEventDeviceType.ReadOnly = True
            '
            'txcEventDevice
            '
            Me.txcEventDevice.DataPropertyName = "Device"
            Me.txcEventDevice.HeaderText = "Device"
            Me.txcEventDevice.Name = "txcEventDevice"
            Me.txcEventDevice.ReadOnly = True
            '
            'cmdClearEventLog
            '
            Me.cmdClearEventLog.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdClearEventLog.AutoSize = True
            Me.cmdClearEventLog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdClearEventLog.Location = New System.Drawing.Point(39, 216)
            Me.cmdClearEventLog.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdClearEventLog.Name = "cmdClearEventLog"
            Me.cmdClearEventLog.Size = New System.Drawing.Size(41, 23)
            Me.cmdClearEventLog.TabIndex = 1
            Me.cmdClearEventLog.Text = "Clear"
            Me.totToolTip.SetToolTip(Me.cmdClearEventLog, "Clear event log")
            Me.cmdClearEventLog.UseVisualStyleBackColor = True
            '
            'flpEventChecks
            '
            Me.flpEventChecks.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.flpEventChecks.AutoSize = True
            Me.flpEventChecks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpEventLog.SetColumnSpan(Me.flpEventChecks, 2)
            Me.flpEventChecks.Controls.Add(Me.chkKeyboard)
            Me.flpEventChecks.Controls.Add(Me.chkMouse)
            Me.flpEventChecks.Controls.Add(Me.chkHID)
            Me.flpEventChecks.Controls.Add(Me.chkMediaCenter)
            Me.flpEventChecks.Location = New System.Drawing.Point(0, 193)
            Me.flpEventChecks.Margin = New System.Windows.Forms.Padding(0)
            Me.flpEventChecks.Name = "flpEventChecks"
            Me.flpEventChecks.Size = New System.Drawing.Size(237, 23)
            Me.flpEventChecks.TabIndex = 2
            '
            'chkKeyboard
            '
            Me.chkKeyboard.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.chkKeyboard.AutoSize = True
            Me.chkKeyboard.Checked = True
            Me.chkKeyboard.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkKeyboard.Location = New System.Drawing.Point(3, 3)
            Me.chkKeyboard.Name = "chkKeyboard"
            Me.chkKeyboard.Size = New System.Drawing.Size(71, 17)
            Me.chkKeyboard.TabIndex = 0
            Me.chkKeyboard.Text = "Keyboard"
            Me.totToolTip.SetToolTip(Me.chkKeyboard, "Log keyboard events")
            Me.chkKeyboard.UseVisualStyleBackColor = True
            '
            'chkMouse
            '
            Me.chkMouse.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.chkMouse.AutoSize = True
            Me.chkMouse.Checked = True
            Me.chkMouse.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkMouse.Location = New System.Drawing.Point(80, 3)
            Me.chkMouse.Name = "chkMouse"
            Me.chkMouse.Size = New System.Drawing.Size(58, 17)
            Me.chkMouse.TabIndex = 1
            Me.chkMouse.Text = "Mouse"
            Me.totToolTip.SetToolTip(Me.chkMouse, "Log mouse events")
            Me.chkMouse.UseVisualStyleBackColor = True
            '
            'chkHID
            '
            Me.chkHID.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.chkHID.AutoSize = True
            Me.chkHID.Checked = True
            Me.chkHID.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkHID.Location = New System.Drawing.Point(144, 3)
            Me.chkHID.Name = "chkHID"
            Me.chkHID.Size = New System.Drawing.Size(45, 17)
            Me.chkHID.TabIndex = 2
            Me.chkHID.Text = "HID"
            Me.totToolTip.SetToolTip(Me.chkHID, "Log HID events")
            Me.chkHID.UseVisualStyleBackColor = True
            '
            'chkMediaCenter
            '
            Me.chkMediaCenter.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.chkMediaCenter.AutoSize = True
            Me.chkMediaCenter.Checked = True
            Me.chkMediaCenter.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkMediaCenter.Location = New System.Drawing.Point(192, 3)
            Me.chkMediaCenter.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
            Me.chkMediaCenter.Name = "chkMediaCenter"
            Me.chkMediaCenter.Size = New System.Drawing.Size(42, 17)
            Me.chkMediaCenter.TabIndex = 6
            Me.chkMediaCenter.Text = "MC"
            Me.totToolTip.SetToolTip(Me.chkMediaCenter, "Enable Media Center events parsing" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "If not checked MC events are logged generic H" & _
                    "ID")
            Me.chkMediaCenter.UseVisualStyleBackColor = True
            '
            'chkEvAutoScroll
            '
            Me.chkEvAutoScroll.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.chkEvAutoScroll.AutoSize = True
            Me.chkEvAutoScroll.Checked = True
            Me.chkEvAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkEvAutoScroll.Location = New System.Drawing.Point(142, 219)
            Me.chkEvAutoScroll.Name = "chkEvAutoScroll"
            Me.chkEvAutoScroll.Size = New System.Drawing.Size(72, 17)
            Me.chkEvAutoScroll.TabIndex = 3
            Me.chkEvAutoScroll.Text = "Autoscroll"
            Me.totToolTip.SetToolTip(Me.chkEvAutoScroll, "Autoscroll on event arival")
            Me.chkEvAutoScroll.UseVisualStyleBackColor = True
            '
            'SplitContainer2
            '
            Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
            Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
            Me.SplitContainer2.Name = "SplitContainer2"
            '
            'SplitContainer2.Panel1
            '
            Me.SplitContainer2.Panel1.Controls.Add(Me.prgEvent)
            Me.SplitContainer2.Panel1.Controls.Add(Me.cmdWhich)
            '
            'SplitContainer2.Panel2
            '
            Me.SplitContainer2.Panel2.Controls.Add(Me.tlpNonRawEvents)
            Me.SplitContainer2.Size = New System.Drawing.Size(376, 239)
            Me.SplitContainer2.SplitterDistance = 172
            Me.SplitContainer2.TabIndex = 3
            '
            'prgEvent
            '
            Me.prgEvent.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgEvent.Location = New System.Drawing.Point(0, 23)
            Me.prgEvent.Name = "prgEvent"
            Me.prgEvent.Size = New System.Drawing.Size(172, 216)
            Me.prgEvent.TabIndex = 2
            '
            'cmdWhich
            '
            Me.cmdWhich.AutoSize = True
            Me.cmdWhich.Dock = System.Windows.Forms.DockStyle.Top
            Me.cmdWhich.Location = New System.Drawing.Point(0, 0)
            Me.cmdWhich.Name = "cmdWhich"
            Me.cmdWhich.Size = New System.Drawing.Size(172, 23)
            Me.cmdWhich.TabIndex = 3
            Me.cmdWhich.UseVisualStyleBackColor = True
            '
            'tlpNonRawEvents
            '
            Me.tlpNonRawEvents.ColumnCount = 2
            Me.tlpNonRawEvents.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpNonRawEvents.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpNonRawEvents.Controls.Add(Me.dgwNonRawEventLog, 0, 0)
            Me.tlpNonRawEvents.Controls.Add(Me.cmdClearNonRaw, 0, 2)
            Me.tlpNonRawEvents.Controls.Add(Me.flpNonRawChecks, 0, 1)
            Me.tlpNonRawEvents.Controls.Add(Me.chkNonRawAutoScroll, 1, 2)
            Me.tlpNonRawEvents.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpNonRawEvents.Location = New System.Drawing.Point(0, 0)
            Me.tlpNonRawEvents.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpNonRawEvents.Name = "tlpNonRawEvents"
            Me.tlpNonRawEvents.RowCount = 3
            Me.tlpNonRawEvents.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpNonRawEvents.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpNonRawEvents.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpNonRawEvents.Size = New System.Drawing.Size(200, 239)
            Me.tlpNonRawEvents.TabIndex = 2
            '
            'dgwNonRawEventLog
            '
            Me.dgwNonRawEventLog.AllowUserToAddRows = False
            Me.dgwNonRawEventLog.AllowUserToDeleteRows = False
            Me.dgwNonRawEventLog.BackgroundColor = System.Drawing.SystemColors.Window
            Me.dgwNonRawEventLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgwNonRawEventLog.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.txcNonRawTime, Me.txcNonRawType, Me.txcNoRawDetail})
            Me.tlpNonRawEvents.SetColumnSpan(Me.dgwNonRawEventLog, 2)
            Me.dgwNonRawEventLog.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgwNonRawEventLog.Location = New System.Drawing.Point(0, 0)
            Me.dgwNonRawEventLog.Margin = New System.Windows.Forms.Padding(0)
            Me.dgwNonRawEventLog.MultiSelect = False
            Me.dgwNonRawEventLog.Name = "dgwNonRawEventLog"
            Me.dgwNonRawEventLog.ReadOnly = True
            Me.dgwNonRawEventLog.RowHeadersVisible = False
            Me.dgwNonRawEventLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.dgwNonRawEventLog.Size = New System.Drawing.Size(200, 193)
            Me.dgwNonRawEventLog.TabIndex = 0
            '
            'txcNonRawTime
            '
            DataGridViewCellStyle2.Format = "hh:mm:ss.ff"
            DataGridViewCellStyle2.NullValue = Nothing
            Me.txcNonRawTime.DefaultCellStyle = DataGridViewCellStyle2
            Me.txcNonRawTime.HeaderText = "Time"
            Me.txcNonRawTime.Name = "txcNonRawTime"
            Me.txcNonRawTime.ReadOnly = True
            Me.txcNonRawTime.Width = 50
            '
            'txcNonRawType
            '
            Me.txcNonRawType.HeaderText = "Type"
            Me.txcNonRawType.Name = "txcNonRawType"
            Me.txcNonRawType.ReadOnly = True
            Me.txcNonRawType.Width = 75
            '
            'txcNoRawDetail
            '
            Me.txcNoRawDetail.HeaderText = "Info"
            Me.txcNoRawDetail.Name = "txcNoRawDetail"
            Me.txcNoRawDetail.ReadOnly = True
            '
            'cmdClearNonRaw
            '
            Me.cmdClearNonRaw.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdClearNonRaw.AutoSize = True
            Me.cmdClearNonRaw.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdClearNonRaw.Location = New System.Drawing.Point(29, 216)
            Me.cmdClearNonRaw.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdClearNonRaw.Name = "cmdClearNonRaw"
            Me.cmdClearNonRaw.Size = New System.Drawing.Size(41, 23)
            Me.cmdClearNonRaw.TabIndex = 1
            Me.cmdClearNonRaw.Text = "Clear"
            Me.totToolTip.SetToolTip(Me.cmdClearNonRaw, "Clear event log")
            Me.cmdClearNonRaw.UseVisualStyleBackColor = True
            '
            'flpNonRawChecks
            '
            Me.flpNonRawChecks.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.flpNonRawChecks.AutoSize = True
            Me.flpNonRawChecks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpNonRawEvents.SetColumnSpan(Me.flpNonRawChecks, 2)
            Me.flpNonRawChecks.Controls.Add(Me.chkNonRawKyeboard)
            Me.flpNonRawChecks.Controls.Add(Me.chkNonRawMouse)
            Me.flpNonRawChecks.Controls.Add(Me.chkNonRawApp)
            Me.flpNonRawChecks.Location = New System.Drawing.Point(3, 193)
            Me.flpNonRawChecks.Margin = New System.Windows.Forms.Padding(0)
            Me.flpNonRawChecks.Name = "flpNonRawChecks"
            Me.flpNonRawChecks.Size = New System.Drawing.Size(194, 23)
            Me.flpNonRawChecks.TabIndex = 2
            '
            'chkNonRawKyeboard
            '
            Me.chkNonRawKyeboard.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.chkNonRawKyeboard.AutoSize = True
            Me.chkNonRawKyeboard.Location = New System.Drawing.Point(3, 3)
            Me.chkNonRawKyeboard.Name = "chkNonRawKyeboard"
            Me.chkNonRawKyeboard.Size = New System.Drawing.Size(71, 17)
            Me.chkNonRawKyeboard.TabIndex = 0
            Me.chkNonRawKyeboard.Text = "Keyboard"
            Me.totToolTip.SetToolTip(Me.chkNonRawKyeboard, "Log keyboard events")
            Me.chkNonRawKyeboard.UseVisualStyleBackColor = True
            '
            'chkNonRawMouse
            '
            Me.chkNonRawMouse.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.chkNonRawMouse.AutoSize = True
            Me.chkNonRawMouse.Location = New System.Drawing.Point(80, 3)
            Me.chkNonRawMouse.Name = "chkNonRawMouse"
            Me.chkNonRawMouse.Size = New System.Drawing.Size(58, 17)
            Me.chkNonRawMouse.TabIndex = 1
            Me.chkNonRawMouse.Text = "Mouse"
            Me.totToolTip.SetToolTip(Me.chkNonRawMouse, "Log mouse events")
            Me.chkNonRawMouse.UseVisualStyleBackColor = True
            '
            'chkNonRawApp
            '
            Me.chkNonRawApp.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.chkNonRawApp.AutoSize = True
            Me.chkNonRawApp.Checked = True
            Me.chkNonRawApp.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkNonRawApp.Location = New System.Drawing.Point(144, 3)
            Me.chkNonRawApp.Name = "chkNonRawApp"
            Me.chkNonRawApp.Size = New System.Drawing.Size(47, 17)
            Me.chkNonRawApp.TabIndex = 2
            Me.chkNonRawApp.Text = "APP"
            Me.totToolTip.SetToolTip(Me.chkNonRawApp, "Log APP commands")
            Me.chkNonRawApp.UseVisualStyleBackColor = True
            '
            'chkNonRawAutoScroll
            '
            Me.chkNonRawAutoScroll.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.chkNonRawAutoScroll.AutoSize = True
            Me.chkNonRawAutoScroll.Checked = True
            Me.chkNonRawAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkNonRawAutoScroll.Location = New System.Drawing.Point(114, 219)
            Me.chkNonRawAutoScroll.Name = "chkNonRawAutoScroll"
            Me.chkNonRawAutoScroll.Size = New System.Drawing.Size(72, 17)
            Me.chkNonRawAutoScroll.TabIndex = 3
            Me.chkNonRawAutoScroll.Text = "Autoscroll"
            Me.totToolTip.SetToolTip(Me.chkNonRawAutoScroll, "Autoscroll on event arival")
            Me.chkNonRawAutoScroll.UseVisualStyleBackColor = True
            '
            'fraRegistration
            '
            Me.fraRegistration.Controls.Add(Me.tlpRegistration)
            Me.fraRegistration.Dock = System.Windows.Forms.DockStyle.Fill
            Me.fraRegistration.Location = New System.Drawing.Point(0, 0)
            Me.fraRegistration.Margin = New System.Windows.Forms.Padding(0)
            Me.fraRegistration.Name = "fraRegistration"
            Me.fraRegistration.Size = New System.Drawing.Size(773, 274)
            Me.fraRegistration.TabIndex = 1
            Me.fraRegistration.TabStop = False
            Me.fraRegistration.Text = "Registration"
            '
            'tlpRegistration
            '
            Me.tlpRegistration.ColumnCount = 6
            Me.tlpRegistration.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpRegistration.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpRegistration.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpRegistration.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpRegistration.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpRegistration.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpRegistration.Controls.Add(Me.dgwRegistration, 0, 1)
            Me.tlpRegistration.Controls.Add(Me.lblUsagePage, 0, 0)
            Me.tlpRegistration.Controls.Add(Me.nudUsagePage, 1, 0)
            Me.tlpRegistration.Controls.Add(Me.lblUsage, 2, 0)
            Me.tlpRegistration.Controls.Add(Me.nudUsage, 3, 0)
            Me.tlpRegistration.Controls.Add(Me.cmdAdd, 4, 0)
            Me.tlpRegistration.Controls.Add(Me.flpRegistrationButtons, 0, 2)
            Me.tlpRegistration.Controls.Add(Me.chkHex, 5, 0)
            Me.tlpRegistration.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpRegistration.Location = New System.Drawing.Point(3, 16)
            Me.tlpRegistration.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpRegistration.Name = "tlpRegistration"
            Me.tlpRegistration.RowCount = 3
            Me.tlpRegistration.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpRegistration.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpRegistration.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpRegistration.Size = New System.Drawing.Size(767, 255)
            Me.tlpRegistration.TabIndex = 0
            '
            'dgwRegistration
            '
            Me.dgwRegistration.BackgroundColor = System.Drawing.SystemColors.Window
            Me.dgwRegistration.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgwRegistration.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.txcUsagePage, Me.txcUsage, Me.chcApplicationKeys, Me.txcBackgroundEvents, Me.chcCaptureMouse, Me.chcDisableLegacyEvents, Me.chcExclude, Me.chcHotKeys, Me.chcRemove, Me.chcWholePage, Me.txcWindow})
            Me.tlpRegistration.SetColumnSpan(Me.dgwRegistration, 6)
            Me.dgwRegistration.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgwRegistration.Location = New System.Drawing.Point(3, 32)
            Me.dgwRegistration.Name = "dgwRegistration"
            Me.dgwRegistration.Size = New System.Drawing.Size(761, 197)
            Me.dgwRegistration.TabIndex = 0
            '
            'txcUsagePage
            '
            Me.txcUsagePage.DataPropertyName = "UsagePage"
            DataGridViewCellStyle3.Format = "d"
            DataGridViewCellStyle3.NullValue = Nothing
            Me.txcUsagePage.DefaultCellStyle = DataGridViewCellStyle3
            Me.txcUsagePage.HeaderText = "Usage page"
            Me.txcUsagePage.Name = "txcUsagePage"
            Me.txcUsagePage.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            '
            'txcUsage
            '
            Me.txcUsage.DataPropertyName = "Usage"
            Me.txcUsage.HeaderText = "Usage"
            Me.txcUsage.Name = "txcUsage"
            Me.txcUsage.Width = 50
            '
            'chcApplicationKeys
            '
            Me.chcApplicationKeys.DataPropertyName = "ApplicationKeys"
            Me.chcApplicationKeys.HeaderText = "AppKeys"
            Me.chcApplicationKeys.Name = "chcApplicationKeys"
            Me.chcApplicationKeys.Width = 50
            '
            'txcBackgroundEvents
            '
            Me.txcBackgroundEvents.DataPropertyName = "BackgroundEvents"
            DataGridViewCellStyle4.Format = "G"
            DataGridViewCellStyle4.NullValue = Nothing
            Me.txcBackgroundEvents.DefaultCellStyle = DataGridViewCellStyle4
            Me.txcBackgroundEvents.HeaderText = "BgEvents"
            Me.txcBackgroundEvents.Name = "txcBackgroundEvents"
            Me.txcBackgroundEvents.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
            Me.txcBackgroundEvents.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
            '
            'chcCaptureMouse
            '
            Me.chcCaptureMouse.DataPropertyName = "CaptureMouse"
            Me.chcCaptureMouse.HeaderText = "CaptMouse"
            Me.chcCaptureMouse.Name = "chcCaptureMouse"
            Me.chcCaptureMouse.Width = 50
            '
            'chcDisableLegacyEvents
            '
            Me.chcDisableLegacyEvents.DataPropertyName = "DisableLegacy"
            Me.chcDisableLegacyEvents.HeaderText = "NoLegacy"
            Me.chcDisableLegacyEvents.Name = "chcDisableLegacyEvents"
            Me.chcDisableLegacyEvents.Width = 50
            '
            'chcExclude
            '
            Me.chcExclude.DataPropertyName = "Exclude"
            Me.chcExclude.HeaderText = "Exclude"
            Me.chcExclude.Name = "chcExclude"
            Me.chcExclude.Width = 50
            '
            'chcHotKeys
            '
            Me.chcHotKeys.DataPropertyName = "HotKeys"
            Me.chcHotKeys.HeaderText = "HotKeys"
            Me.chcHotKeys.Name = "chcHotKeys"
            Me.chcHotKeys.Width = 50
            '
            'chcRemove
            '
            Me.chcRemove.DataPropertyName = "Remove"
            Me.chcRemove.HeaderText = "Remove"
            Me.chcRemove.Name = "chcRemove"
            Me.chcRemove.ReadOnly = True
            Me.chcRemove.Width = 50
            '
            'chcWholePage
            '
            Me.chcWholePage.DataPropertyName = "WholePage"
            Me.chcWholePage.HeaderText = "Whole"
            Me.chcWholePage.Name = "chcWholePage"
            Me.chcWholePage.Width = 50
            '
            'txcWindow
            '
            Me.txcWindow.DataPropertyName = "Window"
            Me.txcWindow.HeaderText = "Window"
            Me.txcWindow.Name = "txcWindow"
            '
            'lblUsagePage
            '
            Me.lblUsagePage.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.lblUsagePage.AutoSize = True
            Me.lblUsagePage.Location = New System.Drawing.Point(3, 8)
            Me.lblUsagePage.Name = "lblUsagePage"
            Me.lblUsagePage.Size = New System.Drawing.Size(60, 13)
            Me.lblUsagePage.TabIndex = 1
            Me.lblUsagePage.Text = "Uage page"
            Me.totToolTip.SetToolTip(Me.lblUsagePage, "Select device from device listing or predefined usage from tree and values will b" & _
                    "e written here")
            '
            'nudUsagePage
            '
            Me.nudUsagePage.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.nudUsagePage.Location = New System.Drawing.Point(69, 4)
            Me.nudUsagePage.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
            Me.nudUsagePage.Name = "nudUsagePage"
            Me.nudUsagePage.Size = New System.Drawing.Size(279, 20)
            Me.nudUsagePage.TabIndex = 2
            Me.totToolTip.SetToolTip(Me.nudUsagePage, "Select device from device listing or predefined usage from tree and values will b" & _
                    "e written here")
            '
            'lblUsage
            '
            Me.lblUsage.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.lblUsage.AutoSize = True
            Me.lblUsage.Location = New System.Drawing.Point(354, 8)
            Me.lblUsage.Name = "lblUsage"
            Me.lblUsage.Size = New System.Drawing.Size(38, 13)
            Me.lblUsage.TabIndex = 3
            Me.lblUsage.Text = "Usage"
            Me.totToolTip.SetToolTip(Me.lblUsage, "Select device from device listing or predefined usage from tree and values will b" & _
                    "e written here")
            '
            'nudUsage
            '
            Me.nudUsage.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.nudUsage.Location = New System.Drawing.Point(398, 4)
            Me.nudUsage.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
            Me.nudUsage.Name = "nudUsage"
            Me.nudUsage.Size = New System.Drawing.Size(279, 20)
            Me.nudUsage.TabIndex = 4
            Me.totToolTip.SetToolTip(Me.nudUsage, "Select device from device listing or predefined usage from tree and values will b" & _
                    "e written here")
            '
            'cmdAdd
            '
            Me.cmdAdd.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdAdd.AutoSize = True
            Me.cmdAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdAdd.Location = New System.Drawing.Point(683, 3)
            Me.cmdAdd.Name = "cmdAdd"
            Me.cmdAdd.Size = New System.Drawing.Size(36, 23)
            Me.cmdAdd.TabIndex = 5
            Me.cmdAdd.Text = "Add"
            Me.totToolTip.SetToolTip(Me.cmdAdd, "Add to list below")
            Me.cmdAdd.UseVisualStyleBackColor = True
            '
            'flpRegistrationButtons
            '
            Me.flpRegistrationButtons.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.flpRegistrationButtons.AutoSize = True
            Me.flpRegistrationButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpRegistration.SetColumnSpan(Me.flpRegistrationButtons, 6)
            Me.flpRegistrationButtons.Controls.Add(Me.cmdRegister)
            Me.flpRegistrationButtons.Controls.Add(Me.cmdUnregister)
            Me.flpRegistrationButtons.Controls.Add(Me.cmdLoadRegistered)
            Me.flpRegistrationButtons.Controls.Add(Me.cmdUnregisterAll)
            Me.flpRegistrationButtons.Controls.Add(Me.cmdClear)
            Me.flpRegistrationButtons.Controls.Add(Me.cmdMediaCenterRemote)
            Me.flpRegistrationButtons.Location = New System.Drawing.Point(155, 232)
            Me.flpRegistrationButtons.Margin = New System.Windows.Forms.Padding(0)
            Me.flpRegistrationButtons.Name = "flpRegistrationButtons"
            Me.flpRegistrationButtons.Size = New System.Drawing.Size(457, 23)
            Me.flpRegistrationButtons.TabIndex = 6
            '
            'cmdRegister
            '
            Me.cmdRegister.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdRegister.AutoSize = True
            Me.cmdRegister.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdRegister.Location = New System.Drawing.Point(0, 0)
            Me.cmdRegister.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdRegister.Name = "cmdRegister"
            Me.cmdRegister.Size = New System.Drawing.Size(56, 23)
            Me.cmdRegister.TabIndex = 0
            Me.cmdRegister.Text = "&Register"
            Me.totToolTip.SetToolTip(Me.cmdRegister, "Register enlisted devices")
            Me.cmdRegister.UseVisualStyleBackColor = True
            '
            'cmdUnregister
            '
            Me.cmdUnregister.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdUnregister.AutoSize = True
            Me.cmdUnregister.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdUnregister.Location = New System.Drawing.Point(56, 0)
            Me.cmdUnregister.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdUnregister.Name = "cmdUnregister"
            Me.cmdUnregister.Size = New System.Drawing.Size(65, 23)
            Me.cmdUnregister.TabIndex = 1
            Me.cmdUnregister.Text = "&Unregister"
            Me.totToolTip.SetToolTip(Me.cmdUnregister, "Unregister enlisted devices")
            Me.cmdUnregister.UseVisualStyleBackColor = True
            '
            'cmdLoadRegistered
            '
            Me.cmdLoadRegistered.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdLoadRegistered.AutoSize = True
            Me.cmdLoadRegistered.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdLoadRegistered.Location = New System.Drawing.Point(121, 0)
            Me.cmdLoadRegistered.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdLoadRegistered.Name = "cmdLoadRegistered"
            Me.cmdLoadRegistered.Size = New System.Drawing.Size(90, 23)
            Me.cmdLoadRegistered.TabIndex = 2
            Me.cmdLoadRegistered.Text = "&Load registered"
            Me.totToolTip.SetToolTip(Me.cmdLoadRegistered, "Fill list with already registered devices")
            Me.cmdLoadRegistered.UseVisualStyleBackColor = True
            '
            'cmdUnregisterAll
            '
            Me.cmdUnregisterAll.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdUnregisterAll.AutoSize = True
            Me.cmdUnregisterAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdUnregisterAll.Location = New System.Drawing.Point(211, 0)
            Me.cmdUnregisterAll.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdUnregisterAll.Name = "cmdUnregisterAll"
            Me.cmdUnregisterAll.Size = New System.Drawing.Size(78, 23)
            Me.cmdUnregisterAll.TabIndex = 3
            Me.cmdUnregisterAll.Text = "Unregister &all"
            Me.totToolTip.SetToolTip(Me.cmdUnregisterAll, "Unregister all registered devices")
            Me.cmdUnregisterAll.UseVisualStyleBackColor = True
            '
            'cmdClear
            '
            Me.cmdClear.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdClear.AutoSize = True
            Me.cmdClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdClear.Location = New System.Drawing.Point(289, 0)
            Me.cmdClear.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdClear.Name = "cmdClear"
            Me.cmdClear.Size = New System.Drawing.Size(41, 23)
            Me.cmdClear.TabIndex = 4
            Me.cmdClear.Text = "&Clear"
            Me.totToolTip.SetToolTip(Me.cmdClear, "Clear list")
            Me.cmdClear.UseVisualStyleBackColor = True
            '
            'cmdMediaCenterRemote
            '
            Me.cmdMediaCenterRemote.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdMediaCenterRemote.AutoSize = True
            Me.cmdMediaCenterRemote.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdMediaCenterRemote.Location = New System.Drawing.Point(342, 0)
            Me.cmdMediaCenterRemote.Margin = New System.Windows.Forms.Padding(12, 0, 0, 0)
            Me.cmdMediaCenterRemote.Name = "cmdMediaCenterRemote"
            Me.cmdMediaCenterRemote.Size = New System.Drawing.Size(115, 23)
            Me.cmdMediaCenterRemote.TabIndex = 5
            Me.cmdMediaCenterRemote.Text = "Media Center remote"
            Me.totToolTip.SetToolTip(Me.cmdMediaCenterRemote, "Fill list with devices for Windows Media Center remote")
            Me.cmdMediaCenterRemote.UseVisualStyleBackColor = True
            '
            'chkHex
            '
            Me.chkHex.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.chkHex.AutoSize = True
            Me.chkHex.Location = New System.Drawing.Point(722, 6)
            Me.chkHex.Margin = New System.Windows.Forms.Padding(0)
            Me.chkHex.Name = "chkHex"
            Me.chkHex.Size = New System.Drawing.Size(45, 17)
            Me.chkHex.TabIndex = 7
            Me.chkHex.Text = "Hex"
            Me.chkHex.UseVisualStyleBackColor = True
            '
            'frmRawInput
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1200, 536)
            Me.Controls.Add(Me.splMain)
            Me.Name = "frmRawInput"
            Me.Text = "Testing Tools.DevicesT.RawInputT"
            Me.splEnumeration.Panel1.ResumeLayout(False)
            Me.splEnumeration.Panel2.ResumeLayout(False)
            Me.splEnumeration.ResumeLayout(False)
            Me.splDeviceList.Panel1.ResumeLayout(False)
            Me.splDeviceList.Panel2.ResumeLayout(False)
            Me.splDeviceList.ResumeLayout(False)
            Me.tlpList.ResumeLayout(False)
            Me.tlpList.PerformLayout()
            Me.fraDevCap.ResumeLayout(False)
            Me.tlpProperties.ResumeLayout(False)
            Me.tlpProperties.PerformLayout()
            Me.SplitContainer1.Panel1.ResumeLayout(False)
            Me.SplitContainer1.Panel1.PerformLayout()
            Me.SplitContainer1.Panel2.ResumeLayout(False)
            Me.SplitContainer1.Panel2.PerformLayout()
            Me.SplitContainer1.ResumeLayout(False)
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.ResumeLayout(False)
            Me.fraEnumeration.ResumeLayout(False)
            Me.splRegistration.Panel1.ResumeLayout(False)
            Me.splRegistration.Panel2.ResumeLayout(False)
            Me.splRegistration.ResumeLayout(False)
            Me.splRegistrationTop.Panel1.ResumeLayout(False)
            Me.splRegistrationTop.Panel2.ResumeLayout(False)
            Me.splRegistrationTop.ResumeLayout(False)
            Me.fraEventLog.ResumeLayout(False)
            Me.splEventLog.Panel1.ResumeLayout(False)
            Me.splEventLog.Panel2.ResumeLayout(False)
            Me.splEventLog.ResumeLayout(False)
            Me.tlpEventLog.ResumeLayout(False)
            Me.tlpEventLog.PerformLayout()
            CType(Me.dgwEvents, System.ComponentModel.ISupportInitialize).EndInit()
            Me.flpEventChecks.ResumeLayout(False)
            Me.flpEventChecks.PerformLayout()
            Me.SplitContainer2.Panel1.ResumeLayout(False)
            Me.SplitContainer2.Panel1.PerformLayout()
            Me.SplitContainer2.Panel2.ResumeLayout(False)
            Me.SplitContainer2.ResumeLayout(False)
            Me.tlpNonRawEvents.ResumeLayout(False)
            Me.tlpNonRawEvents.PerformLayout()
            CType(Me.dgwNonRawEventLog, System.ComponentModel.ISupportInitialize).EndInit()
            Me.flpNonRawChecks.ResumeLayout(False)
            Me.flpNonRawChecks.PerformLayout()
            Me.fraRegistration.ResumeLayout(False)
            Me.tlpRegistration.ResumeLayout(False)
            Me.tlpRegistration.PerformLayout()
            CType(Me.dgwRegistration, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.nudUsagePage, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.nudUsage, System.ComponentModel.ISupportInitialize).EndInit()
            Me.flpRegistrationButtons.ResumeLayout(False)
            Me.flpRegistrationButtons.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents splEnumeration As System.Windows.Forms.SplitContainer
        Friend WithEvents tlpList As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents cmdGetRawInputDeviceList As System.Windows.Forms.Button
        Friend WithEvents lstDevices As System.Windows.Forms.ListBox
        Friend WithEvents tlpProperties As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents lblDeviceNameI As System.Windows.Forms.Label
        Friend WithEvents lblDeviceName As System.Windows.Forms.Label
        Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
        Friend WithEvents prgDeviceInfo As System.Windows.Forms.PropertyGrid
        Friend WithEvents lblDeviceInfoI As System.Windows.Forms.Label
        Friend WithEvents prgName As System.Windows.Forms.PropertyGrid
        Friend WithEvents lblRawDeviceNameI As System.Windows.Forms.Label
        Friend WithEvents lblDeviceDescriptionI As System.Windows.Forms.Label
        Friend WithEvents lblDeviceDescription As System.Windows.Forms.Label
        Friend WithEvents splMain As System.Windows.Forms.SplitContainer
        Friend WithEvents tvwHid As System.Windows.Forms.TreeView
        Friend WithEvents splRegistration As System.Windows.Forms.SplitContainer
        Friend WithEvents tlpRegistration As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents dgwRegistration As System.Windows.Forms.DataGridView
        Friend WithEvents lblUsagePage As System.Windows.Forms.Label
        Friend WithEvents nudUsagePage As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblUsage As System.Windows.Forms.Label
        Friend WithEvents nudUsage As System.Windows.Forms.NumericUpDown
        Friend WithEvents cmdAdd As System.Windows.Forms.Button
        Friend WithEvents flpRegistrationButtons As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents cmdRegister As System.Windows.Forms.Button
        Friend WithEvents cmdUnregister As System.Windows.Forms.Button
        Friend WithEvents cmdLoadRegistered As System.Windows.Forms.Button
        Friend WithEvents cmdUnregisterAll As System.Windows.Forms.Button
        Friend WithEvents cmdClear As System.Windows.Forms.Button
        Friend WithEvents splRegistrationTop As System.Windows.Forms.SplitContainer
        Friend WithEvents fraEventLog As System.Windows.Forms.GroupBox
        Friend WithEvents dgwEvents As System.Windows.Forms.DataGridView
        Friend WithEvents tlpEventLog As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents cmdClearEventLog As System.Windows.Forms.Button
        Friend WithEvents splEventLog As System.Windows.Forms.SplitContainer
        Friend WithEvents prgEvent As System.Windows.Forms.PropertyGrid
        Friend WithEvents txcUsagePage As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txcUsage As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents chcApplicationKeys As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents txcBackgroundEvents As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents chcCaptureMouse As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents chcDisableLegacyEvents As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents chcExclude As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents chcHotKeys As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents chcRemove As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents chcWholePage As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents txcWindow As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents flpEventChecks As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents chkKeyboard As System.Windows.Forms.CheckBox
        Friend WithEvents chkMouse As System.Windows.Forms.CheckBox
        Friend WithEvents chkHID As System.Windows.Forms.CheckBox
        Friend WithEvents splDeviceList As System.Windows.Forms.SplitContainer
        Friend WithEvents fraDevCap As System.Windows.Forms.GroupBox
        Friend WithEvents prgDevCap As System.Windows.Forms.PropertyGrid
        Friend WithEvents cmdMediaCenterRemote As System.Windows.Forms.Button
        Friend WithEvents chkMediaCenter As System.Windows.Forms.CheckBox
        Friend WithEvents chkEvAutoScroll As System.Windows.Forms.CheckBox
        Friend WithEvents fraEnumeration As System.Windows.Forms.GroupBox
        Friend WithEvents totToolTip As System.Windows.Forms.ToolTip
        Friend WithEvents fraRegistration As System.Windows.Forms.GroupBox
        Friend WithEvents chkHex As System.Windows.Forms.CheckBox
        Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
        Friend WithEvents tlpNonRawEvents As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents dgwNonRawEventLog As System.Windows.Forms.DataGridView
        Friend WithEvents cmdClearNonRaw As System.Windows.Forms.Button
        Friend WithEvents flpNonRawChecks As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents chkNonRawKyeboard As System.Windows.Forms.CheckBox
        Friend WithEvents chkNonRawMouse As System.Windows.Forms.CheckBox
        Friend WithEvents chkNonRawApp As System.Windows.Forms.CheckBox
        Friend WithEvents chkNonRawAutoScroll As System.Windows.Forms.CheckBox
        Friend WithEvents cmdWhich As System.Windows.Forms.Button
        Friend WithEvents txcTime As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txcEvent As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txcEventDeviceType As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txcEventDevice As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txcNonRawTime As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txcNonRawType As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txcNoRawDetail As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace