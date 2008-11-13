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
            Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
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
            Me.splEnumeration.Panel1.SuspendLayout()
            Me.splEnumeration.Panel2.SuspendLayout()
            Me.splEnumeration.SuspendLayout()
            Me.TableLayoutPanel1.SuspendLayout()
            Me.tlpProperties.SuspendLayout()
            Me.SplitContainer1.Panel1.SuspendLayout()
            Me.SplitContainer1.Panel2.SuspendLayout()
            Me.SplitContainer1.SuspendLayout()
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
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
            Me.splEnumeration.Panel1.Controls.Add(Me.TableLayoutPanel1)
            '
            'splEnumeration.Panel2
            '
            Me.splEnumeration.Panel2.Controls.Add(Me.tlpProperties)
            Me.splEnumeration.Size = New System.Drawing.Size(519, 536)
            Me.splEnumeration.SplitterDistance = 172
            Me.splEnumeration.TabIndex = 0
            '
            'TableLayoutPanel1
            '
            Me.TableLayoutPanel1.ColumnCount = 1
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel1.Controls.Add(Me.cmdGetRawInputDeviceList, 0, 0)
            Me.TableLayoutPanel1.Controls.Add(Me.lstDevices, 0, 1)
            Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
            Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
            Me.TableLayoutPanel1.RowCount = 2
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel1.Size = New System.Drawing.Size(172, 536)
            Me.TableLayoutPanel1.TabIndex = 0
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
            Me.splMain.Panel2.Controls.Add(Me.tvwHid)
            Me.splMain.Size = New System.Drawing.Size(952, 536)
            Me.splMain.SplitterDistance = 519
            Me.splMain.TabIndex = 1
            '
            'tvwHid
            '
            Me.tvwHid.CheckBoxes = True
            Me.tvwHid.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tvwHid.Location = New System.Drawing.Point(0, 0)
            Me.tvwHid.Name = "tvwHid"
            Me.tvwHid.Size = New System.Drawing.Size(429, 536)
            Me.tvwHid.TabIndex = 0
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
            Me.TableLayoutPanel1.ResumeLayout(False)
            Me.TableLayoutPanel1.PerformLayout()
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
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents splEnumeration As System.Windows.Forms.SplitContainer
        Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
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
    End Class
End Namespace