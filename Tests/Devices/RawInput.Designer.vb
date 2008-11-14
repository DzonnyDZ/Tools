Namespace DevicesT.RawInputT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmRawInput
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
            Me.splEnumeration = New System.Windows.Forms.SplitContainer
            Me.tlpList = New System.Windows.Forms.TableLayoutPanel
            Me.cmdGetRawInputDeviceList = New System.Windows.Forms.Button
            Me.lstDevices = New System.Windows.Forms.ListBox
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
            Me.tvwHid = New System.Windows.Forms.TreeView
            Me.splRegistration = New System.Windows.Forms.SplitContainer
            Me.tlpRegistration = New System.Windows.Forms.TableLayoutPanel
            Me.dgwRegistration = New System.Windows.Forms.DataGridView
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
            Me.txcUsagePage = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.txcUsage = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.chcApplicationKeys = New System.Windows.Forms.DataGridViewCheckBoxColumn
            Me.cmcBackgroundEvents = New System.Windows.Forms.DataGridViewComboBoxColumn
            Me.chcCaptureMouse = New System.Windows.Forms.DataGridViewCheckBoxColumn
            Me.chcDisableLegacyEvents = New System.Windows.Forms.DataGridViewCheckBoxColumn
            Me.chcExclude = New System.Windows.Forms.DataGridViewCheckBoxColumn
            Me.chcHotKeys = New System.Windows.Forms.DataGridViewCheckBoxColumn
            Me.chcRemove = New System.Windows.Forms.DataGridViewCheckBoxColumn
            Me.chcWholePage = New System.Windows.Forms.DataGridViewCheckBoxColumn
            Me.txcWindow = New System.Windows.Forms.DataGridViewTextBoxColumn
            Me.splEnumeration.Panel1.SuspendLayout()
            Me.splEnumeration.Panel2.SuspendLayout()
            Me.splEnumeration.SuspendLayout()
            Me.tlpList.SuspendLayout()
            Me.tlpProperties.SuspendLayout()
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.splRegistration.Panel1.SuspendLayout()
            Me.splRegistration.Panel2.SuspendLayout()
            Me.splRegistration.SuspendLayout()
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
            Me.splEnumeration.Location = New System.Drawing.Point(0, 0)
            Me.splEnumeration.Name = "splEnumeration"
            '
            'splEnumeration.Panel1
            '
            Me.splEnumeration.Panel1.Controls.Add(Me.tlpList)
            '
            'splEnumeration.Panel2
            '
            Me.splEnumeration.Panel2.Controls.Add(Me.tlpProperties)
            Me.splEnumeration.Size = New System.Drawing.Size(519, 536)
            Me.splEnumeration.SplitterDistance = 172
            Me.splEnumeration.TabIndex = 0
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
            Me.tlpList.Size = New System.Drawing.Size(172, 536)
            Me.tlpList.TabIndex = 0
            '
            'cmdGetRawInputDeviceList
            '
            Me.cmdGetRawInputDeviceList.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdGetRawInputDeviceList.AutoSize = True
            Me.cmdGetRawInputDeviceList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdGetRawInputDeviceList.Location = New System.Drawing.Point(21, 3)
            Me.cmdGetRawInputDeviceList.Name = "cmdGetRawInputDeviceList"
            Me.cmdGetRawInputDeviceList.Size = New System.Drawing.Size(130, 23)
            Me.cmdGetRawInputDeviceList.TabIndex = 0
            Me.cmdGetRawInputDeviceList.Text = "GetRawInputDeviceList"
            Me.cmdGetRawInputDeviceList.UseVisualStyleBackColor = True
            '
            'lstDevices
            '
            Me.lstDevices.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lstDevices.FormattingEnabled = True
            Me.lstDevices.IntegralHeight = False
            Me.lstDevices.Location = New System.Drawing.Point(3, 32)
            Me.lstDevices.Name = "lstDevices"
            Me.lstDevices.Size = New System.Drawing.Size(166, 501)
            Me.lstDevices.TabIndex = 1
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
            Me.tlpProperties.Size = New System.Drawing.Size(343, 536)
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
            '
            'lblDeviceName
            '
            Me.lblDeviceName.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.lblDeviceName.AutoSize = True
            Me.lblDeviceName.Location = New System.Drawing.Point(104, 0)
            Me.lblDeviceName.Name = "lblDeviceName"
            Me.lblDeviceName.Size = New System.Drawing.Size(0, 13)
            Me.lblDeviceName.TabIndex = 1
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
            Me.SplitContainer1.Size = New System.Drawing.Size(337, 504)
            Me.SplitContainer1.SplitterDistance = 252
            Me.SplitContainer1.TabIndex = 3
            '
            'prgDeviceInfo
            '
            Me.prgDeviceInfo.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgDeviceInfo.Location = New System.Drawing.Point(0, 13)
            Me.prgDeviceInfo.Name = "prgDeviceInfo"
            Me.prgDeviceInfo.Size = New System.Drawing.Size(337, 239)
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
            Me.prgName.Name = "prgName"
            Me.prgName.Size = New System.Drawing.Size(337, 235)
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
            '
            'lblDeviceDescriptionI
            '
            Me.lblDeviceDescriptionI.AutoSize = True
            Me.lblDeviceDescriptionI.Location = New System.Drawing.Point(3, 523)
            Me.lblDeviceDescriptionI.Name = "lblDeviceDescriptionI"
            Me.lblDeviceDescriptionI.Size = New System.Drawing.Size(95, 13)
            Me.lblDeviceDescriptionI.TabIndex = 4
            Me.lblDeviceDescriptionI.Text = "Device description"
            '
            'lblDeviceDescription
            '
            Me.lblDeviceDescription.AutoSize = True
            Me.lblDeviceDescription.Location = New System.Drawing.Point(104, 523)
            Me.lblDeviceDescription.Name = "lblDeviceDescription"
            Me.lblDeviceDescription.Size = New System.Drawing.Size(0, 13)
            Me.lblDeviceDescription.TabIndex = 5
            '
            'splMain
            '
            Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splMain.Location = New System.Drawing.Point(0, 0)
            Me.splMain.Name = "splMain"
            '
            'splMain.Panel1
            '
            Me.splMain.Panel1.Controls.Add(Me.splEnumeration)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.splRegistration)
            Me.splMain.Size = New System.Drawing.Size(952, 536)
            Me.splMain.SplitterDistance = 519
            Me.splMain.TabIndex = 1
            '
            'tvwHid
            '
            Me.tvwHid.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tvwHid.Location = New System.Drawing.Point(0, 0)
            Me.tvwHid.Name = "tvwHid"
            Me.tvwHid.Size = New System.Drawing.Size(429, 258)
            Me.tvwHid.TabIndex = 0
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
            Me.splRegistration.Panel1.Controls.Add(Me.tvwHid)
            '
            'splRegistration.Panel2
            '
            Me.splRegistration.Panel2.Controls.Add(Me.tlpRegistration)
            Me.splRegistration.Size = New System.Drawing.Size(429, 536)
            Me.splRegistration.SplitterDistance = 258
            Me.splRegistration.TabIndex = 1
            '
            'tlpRegistration
            '
            Me.tlpRegistration.ColumnCount = 5
            Me.tlpRegistration.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpRegistration.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpRegistration.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpRegistration.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpRegistration.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpRegistration.Controls.Add(Me.dgwRegistration, 0, 1)
            Me.tlpRegistration.Controls.Add(Me.lblUsagePage, 0, 0)
            Me.tlpRegistration.Controls.Add(Me.nudUsagePage, 1, 0)
            Me.tlpRegistration.Controls.Add(Me.lblUsage, 2, 0)
            Me.tlpRegistration.Controls.Add(Me.nudUsage, 3, 0)
            Me.tlpRegistration.Controls.Add(Me.cmdAdd, 4, 0)
            Me.tlpRegistration.Controls.Add(Me.flpRegistrationButtons, 0, 2)
            Me.tlpRegistration.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpRegistration.Location = New System.Drawing.Point(0, 0)
            Me.tlpRegistration.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpRegistration.Name = "tlpRegistration"
            Me.tlpRegistration.RowCount = 3
            Me.tlpRegistration.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpRegistration.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpRegistration.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpRegistration.Size = New System.Drawing.Size(429, 274)
            Me.tlpRegistration.TabIndex = 0
            '
            'dgwRegistration
            '
            Me.dgwRegistration.BackgroundColor = System.Drawing.SystemColors.Window
            Me.dgwRegistration.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.dgwRegistration.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.txcUsagePage, Me.txcUsage, Me.chcApplicationKeys, Me.cmcBackgroundEvents, Me.chcCaptureMouse, Me.chcDisableLegacyEvents, Me.chcExclude, Me.chcHotKeys, Me.chcRemove, Me.chcWholePage, Me.txcWindow})
            Me.tlpRegistration.SetColumnSpan(Me.dgwRegistration, 5)
            Me.dgwRegistration.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dgwRegistration.Location = New System.Drawing.Point(3, 32)
            Me.dgwRegistration.Name = "dgwRegistration"
            Me.dgwRegistration.Size = New System.Drawing.Size(423, 216)
            Me.dgwRegistration.TabIndex = 0
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
            '
            'nudUsagePage
            '
            Me.nudUsagePage.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.nudUsagePage.Location = New System.Drawing.Point(69, 4)
            Me.nudUsagePage.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
            Me.nudUsagePage.Name = "nudUsagePage"
            Me.nudUsagePage.Size = New System.Drawing.Size(132, 20)
            Me.nudUsagePage.TabIndex = 2
            '
            'lblUsage
            '
            Me.lblUsage.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.lblUsage.AutoSize = True
            Me.lblUsage.Location = New System.Drawing.Point(207, 8)
            Me.lblUsage.Name = "lblUsage"
            Me.lblUsage.Size = New System.Drawing.Size(38, 13)
            Me.lblUsage.TabIndex = 3
            Me.lblUsage.Text = "Usage"
            '
            'nudUsage
            '
            Me.nudUsage.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.nudUsage.Location = New System.Drawing.Point(251, 4)
            Me.nudUsage.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
            Me.nudUsage.Name = "nudUsage"
            Me.nudUsage.Size = New System.Drawing.Size(132, 20)
            Me.nudUsage.TabIndex = 4
            '
            'cmdAdd
            '
            Me.cmdAdd.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdAdd.AutoSize = True
            Me.cmdAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdAdd.Location = New System.Drawing.Point(389, 3)
            Me.cmdAdd.Name = "cmdAdd"
            Me.cmdAdd.Size = New System.Drawing.Size(36, 23)
            Me.cmdAdd.TabIndex = 5
            Me.cmdAdd.Text = "Add"
            Me.cmdAdd.UseVisualStyleBackColor = True
            '
            'flpRegistrationButtons
            '
            Me.flpRegistrationButtons.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.flpRegistrationButtons.AutoSize = True
            Me.flpRegistrationButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpRegistration.SetColumnSpan(Me.flpRegistrationButtons, 5)
            Me.flpRegistrationButtons.Controls.Add(Me.cmdRegister)
            Me.flpRegistrationButtons.Controls.Add(Me.cmdUnregister)
            Me.flpRegistrationButtons.Controls.Add(Me.cmdLoadRegistered)
            Me.flpRegistrationButtons.Controls.Add(Me.cmdUnregisterAll)
            Me.flpRegistrationButtons.Controls.Add(Me.cmdClear)
            Me.flpRegistrationButtons.Location = New System.Drawing.Point(49, 251)
            Me.flpRegistrationButtons.Margin = New System.Windows.Forms.Padding(0)
            Me.flpRegistrationButtons.Name = "flpRegistrationButtons"
            Me.flpRegistrationButtons.Size = New System.Drawing.Size(330, 23)
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
            Me.cmdRegister.Text = "Regsiter"
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
            Me.cmdUnregister.Text = "Unregister"
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
            Me.cmdLoadRegistered.Text = "Load registered"
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
            Me.cmdUnregisterAll.Text = "Unregister all"
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
            Me.cmdClear.Text = "Clear"
            Me.cmdClear.UseVisualStyleBackColor = True
            '
            'txcUsagePage
            '
            Me.txcUsagePage.DataPropertyName = "UsagePage"
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
            'cmcBackgroundEvents
            '
            Me.cmcBackgroundEvents.DataPropertyName = "BackgroundEvents"
            Me.cmcBackgroundEvents.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cmcBackgroundEvents.HeaderText = "BgEvents"
            Me.cmcBackgroundEvents.Items.AddRange(New Object() {"Background", "BackgroundWhenNotHandled", "ForegroundOnly"})
            Me.cmcBackgroundEvents.Name = "cmcBackgroundEvents"
            Me.cmcBackgroundEvents.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
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
            'frmRawInput
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(952, 536)
            Me.Controls.Add(Me.splMain)
            Me.Name = "frmRawInput"
            Me.Text = "Testing Tools.DevicesT.RawInputT"
            Me.splEnumeration.Panel1.ResumeLayout(False)
            Me.splEnumeration.Panel2.ResumeLayout(False)
            Me.splEnumeration.ResumeLayout(False)
            Me.tlpList.ResumeLayout(False)
            Me.tlpList.PerformLayout()
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
            Me.splRegistration.Panel1.ResumeLayout(False)
            Me.splRegistration.Panel2.ResumeLayout(False)
            Me.splRegistration.ResumeLayout(False)
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
        Friend WithEvents txcUsagePage As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents txcUsage As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents chcApplicationKeys As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents cmcBackgroundEvents As System.Windows.Forms.DataGridViewComboBoxColumn
        Friend WithEvents chcCaptureMouse As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents chcDisableLegacyEvents As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents chcExclude As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents chcHotKeys As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents chcRemove As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents chcWholePage As System.Windows.Forms.DataGridViewCheckBoxColumn
        Friend WithEvents txcWindow As System.Windows.Forms.DataGridViewTextBoxColumn
    End Class
End Namespace