Namespace MetadataT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmMetadataProvider
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
            Me.flpProviders = New System.Windows.Forms.FlowLayoutPanel
            Me.cmdJpeg = New System.Windows.Forms.Button
            Me.fraProvider = New System.Windows.Forms.GroupBox
            Me.tabProvider = New System.Windows.Forms.TabControl
            Me.tapCurentMetadata = New System.Windows.Forms.TabPage
            Me.lstCurrentMetadata = New System.Windows.Forms.ListBox
            Me.tapSupportedMetadata = New System.Windows.Forms.TabPage
            Me.lstSupportedMetadata = New System.Windows.Forms.ListBox
            Me.tlpMetadataActions = New System.Windows.Forms.TableLayoutPanel
            Me.txtMetadataName = New System.Windows.Forms.TextBox
            Me.cmdCheckMetadata = New System.Windows.Forms.Button
            Me.cmdGetMetadata = New System.Windows.Forms.Button
            Me.lblProviderName = New System.Windows.Forms.Label
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.fraMatadata = New System.Windows.Forms.GroupBox
            Me.tabMetadata = New System.Windows.Forms.TabControl
            Me.tapCurrentKeys = New System.Windows.Forms.TabPage
            Me.lvwCurrentKeys = New System.Windows.Forms.ListView
            Me.cohKey = New System.Windows.Forms.ColumnHeader
            Me.cohName = New System.Windows.Forms.ColumnHeader
            Me.cohDisplayName = New System.Windows.Forms.ColumnHeader
            Me.cohValue = New System.Windows.Forms.ColumnHeader
            Me.cohMetadataType = New System.Windows.Forms.ColumnHeader
            Me.cohDescription = New System.Windows.Forms.ColumnHeader
            Me.tapPredefinetKeys = New System.Windows.Forms.TabPage
            Me.lstPredefinedKeys = New System.Windows.Forms.ListBox
            Me.tapPredefinedNames = New System.Windows.Forms.TabPage
            Me.lstPredefinedNames = New System.Windows.Forms.ListBox
            Me.tlpMatadataActions = New System.Windows.Forms.TableLayoutPanel
            Me.txtKeyName = New System.Windows.Forms.TextBox
            Me.cmdCheckKey = New System.Windows.Forms.Button
            Me.cmdGetValue = New System.Windows.Forms.Button
            Me.cmdDisplayName = New System.Windows.Forms.Button
            Me.cmdDescription = New System.Windows.Forms.Button
            Me.cmdNameToKey = New System.Windows.Forms.Button
            Me.cmdKeyToName = New System.Windows.Forms.Button
            Me.lblMetadataName = New System.Windows.Forms.Label
            Me.lblMetadataProviderName = New System.Windows.Forms.Label
            Me.totToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.ofdJPEG = New System.Windows.Forms.OpenFileDialog
            Me.flpProviders.SuspendLayout()
            Me.fraProvider.SuspendLayout()
            Me.tabProvider.SuspendLayout()
            Me.tapCurentMetadata.SuspendLayout()
            Me.tapSupportedMetadata.SuspendLayout()
            Me.tlpMetadataActions.SuspendLayout()
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.fraMatadata.SuspendLayout()
            Me.tabMetadata.SuspendLayout()
            Me.tapCurrentKeys.SuspendLayout()
            Me.tapPredefinetKeys.SuspendLayout()
            Me.tapPredefinedNames.SuspendLayout()
            Me.tlpMatadataActions.SuspendLayout()
            Me.SuspendLayout()
            '
            'flpProviders
            '
            Me.flpProviders.AutoSize = True
            Me.flpProviders.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpProviders.Controls.Add(Me.cmdJpeg)
            Me.flpProviders.Dock = System.Windows.Forms.DockStyle.Top
            Me.flpProviders.Location = New System.Drawing.Point(0, 0)
            Me.flpProviders.Name = "flpProviders"
            Me.flpProviders.Size = New System.Drawing.Size(815, 29)
            Me.flpProviders.TabIndex = 0
            '
            'cmdJpeg
            '
            Me.cmdJpeg.AutoSize = True
            Me.cmdJpeg.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdJpeg.Location = New System.Drawing.Point(3, 3)
            Me.cmdJpeg.Name = "cmdJpeg"
            Me.cmdJpeg.Size = New System.Drawing.Size(40, 23)
            Me.cmdJpeg.TabIndex = 0
            Me.cmdJpeg.Text = "&Jpeg"
            Me.cmdJpeg.UseVisualStyleBackColor = True
            '
            'fraProvider
            '
            Me.fraProvider.Controls.Add(Me.tabProvider)
            Me.fraProvider.Controls.Add(Me.tlpMetadataActions)
            Me.fraProvider.Controls.Add(Me.lblProviderName)
            Me.fraProvider.Dock = System.Windows.Forms.DockStyle.Fill
            Me.fraProvider.Enabled = False
            Me.fraProvider.Location = New System.Drawing.Point(0, 0)
            Me.fraProvider.Name = "fraProvider"
            Me.fraProvider.Size = New System.Drawing.Size(271, 461)
            Me.fraProvider.TabIndex = 1
            Me.fraProvider.TabStop = False
            Me.fraProvider.Text = "Provider"
            '
            'tabProvider
            '
            Me.tabProvider.Controls.Add(Me.tapCurentMetadata)
            Me.tabProvider.Controls.Add(Me.tapSupportedMetadata)
            Me.tabProvider.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tabProvider.Location = New System.Drawing.Point(3, 29)
            Me.tabProvider.Name = "tabProvider"
            Me.tabProvider.SelectedIndex = 0
            Me.tabProvider.Size = New System.Drawing.Size(265, 400)
            Me.tabProvider.TabIndex = 1
            '
            'tapCurentMetadata
            '
            Me.tapCurentMetadata.Controls.Add(Me.lstCurrentMetadata)
            Me.tapCurentMetadata.Location = New System.Drawing.Point(4, 22)
            Me.tapCurentMetadata.Name = "tapCurentMetadata"
            Me.tapCurentMetadata.Padding = New System.Windows.Forms.Padding(3)
            Me.tapCurentMetadata.Size = New System.Drawing.Size(257, 374)
            Me.tapCurentMetadata.TabIndex = 0
            Me.tapCurentMetadata.Text = "Current metadata"
            Me.tapCurentMetadata.UseVisualStyleBackColor = True
            '
            'lstCurrentMetadata
            '
            Me.lstCurrentMetadata.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lstCurrentMetadata.FormattingEnabled = True
            Me.lstCurrentMetadata.IntegralHeight = False
            Me.lstCurrentMetadata.Location = New System.Drawing.Point(3, 3)
            Me.lstCurrentMetadata.Name = "lstCurrentMetadata"
            Me.lstCurrentMetadata.Size = New System.Drawing.Size(251, 368)
            Me.lstCurrentMetadata.TabIndex = 0
            '
            'tapSupportedMetadata
            '
            Me.tapSupportedMetadata.Controls.Add(Me.lstSupportedMetadata)
            Me.tapSupportedMetadata.Location = New System.Drawing.Point(4, 22)
            Me.tapSupportedMetadata.Name = "tapSupportedMetadata"
            Me.tapSupportedMetadata.Padding = New System.Windows.Forms.Padding(3)
            Me.tapSupportedMetadata.Size = New System.Drawing.Size(257, 374)
            Me.tapSupportedMetadata.TabIndex = 1
            Me.tapSupportedMetadata.Text = "Supported metadata"
            Me.tapSupportedMetadata.UseVisualStyleBackColor = True
            '
            'lstSupportedMetadata
            '
            Me.lstSupportedMetadata.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lstSupportedMetadata.FormattingEnabled = True
            Me.lstSupportedMetadata.IntegralHeight = False
            Me.lstSupportedMetadata.Location = New System.Drawing.Point(3, 3)
            Me.lstSupportedMetadata.Name = "lstSupportedMetadata"
            Me.lstSupportedMetadata.Size = New System.Drawing.Size(251, 368)
            Me.lstSupportedMetadata.TabIndex = 1
            '
            'tlpMetadataActions
            '
            Me.tlpMetadataActions.AutoSize = True
            Me.tlpMetadataActions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpMetadataActions.ColumnCount = 3
            Me.tlpMetadataActions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMetadataActions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMetadataActions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMetadataActions.Controls.Add(Me.txtMetadataName, 0, 0)
            Me.tlpMetadataActions.Controls.Add(Me.cmdCheckMetadata, 1, 0)
            Me.tlpMetadataActions.Controls.Add(Me.cmdGetMetadata, 2, 0)
            Me.tlpMetadataActions.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.tlpMetadataActions.Location = New System.Drawing.Point(3, 429)
            Me.tlpMetadataActions.Name = "tlpMetadataActions"
            Me.tlpMetadataActions.RowCount = 1
            Me.tlpMetadataActions.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMetadataActions.Size = New System.Drawing.Size(265, 29)
            Me.tlpMetadataActions.TabIndex = 3
            '
            'txtMetadataName
            '
            Me.txtMetadataName.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtMetadataName.Location = New System.Drawing.Point(3, 4)
            Me.txtMetadataName.Name = "txtMetadataName"
            Me.txtMetadataName.Size = New System.Drawing.Size(113, 20)
            Me.txtMetadataName.TabIndex = 0
            '
            'cmdCheckMetadata
            '
            Me.cmdCheckMetadata.AutoSize = True
            Me.cmdCheckMetadata.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdCheckMetadata.Location = New System.Drawing.Point(122, 3)
            Me.cmdCheckMetadata.Name = "cmdCheckMetadata"
            Me.cmdCheckMetadata.Size = New System.Drawing.Size(23, 23)
            Me.cmdCheckMetadata.TabIndex = 2
            Me.cmdCheckMetadata.Text = "?"
            Me.totToolTip.SetToolTip(Me.cmdCheckMetadata, "Check wheather metadata are contained")
            Me.cmdCheckMetadata.UseVisualStyleBackColor = True
            '
            'cmdGetMetadata
            '
            Me.cmdGetMetadata.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdGetMetadata.AutoSize = True
            Me.cmdGetMetadata.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdGetMetadata.Location = New System.Drawing.Point(151, 3)
            Me.cmdGetMetadata.Name = "cmdGetMetadata"
            Me.cmdGetMetadata.Size = New System.Drawing.Size(111, 23)
            Me.cmdGetMetadata.TabIndex = 1
            Me.cmdGetMetadata.Text = ">> Get metadata >>"
            Me.cmdGetMetadata.UseVisualStyleBackColor = True
            '
            'lblProviderName
            '
            Me.lblProviderName.AutoSize = True
            Me.lblProviderName.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblProviderName.Location = New System.Drawing.Point(3, 16)
            Me.lblProviderName.Name = "lblProviderName"
            Me.lblProviderName.Size = New System.Drawing.Size(0, 13)
            Me.lblProviderName.TabIndex = 0
            '
            'splMain
            '
            Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splMain.Location = New System.Drawing.Point(0, 29)
            Me.splMain.Name = "splMain"
            '
            'splMain.Panel1
            '
            Me.splMain.Panel1.Controls.Add(Me.fraProvider)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.fraMatadata)
            Me.splMain.Size = New System.Drawing.Size(815, 461)
            Me.splMain.SplitterDistance = 271
            Me.splMain.TabIndex = 2
            '
            'fraMatadata
            '
            Me.fraMatadata.Controls.Add(Me.tabMetadata)
            Me.fraMatadata.Controls.Add(Me.tlpMatadataActions)
            Me.fraMatadata.Controls.Add(Me.lblMetadataName)
            Me.fraMatadata.Controls.Add(Me.lblMetadataProviderName)
            Me.fraMatadata.Dock = System.Windows.Forms.DockStyle.Fill
            Me.fraMatadata.Enabled = False
            Me.fraMatadata.Location = New System.Drawing.Point(0, 0)
            Me.fraMatadata.Name = "fraMatadata"
            Me.fraMatadata.Size = New System.Drawing.Size(540, 461)
            Me.fraMatadata.TabIndex = 0
            Me.fraMatadata.TabStop = False
            Me.fraMatadata.Text = "Metadata"
            '
            'tabMetadata
            '
            Me.tabMetadata.Controls.Add(Me.tapCurrentKeys)
            Me.tabMetadata.Controls.Add(Me.tapPredefinetKeys)
            Me.tabMetadata.Controls.Add(Me.tapPredefinedNames)
            Me.tabMetadata.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tabMetadata.Location = New System.Drawing.Point(3, 42)
            Me.tabMetadata.Name = "tabMetadata"
            Me.tabMetadata.SelectedIndex = 0
            Me.tabMetadata.Size = New System.Drawing.Size(534, 387)
            Me.tabMetadata.TabIndex = 2
            '
            'tapCurrentKeys
            '
            Me.tapCurrentKeys.Controls.Add(Me.lvwCurrentKeys)
            Me.tapCurrentKeys.Location = New System.Drawing.Point(4, 22)
            Me.tapCurrentKeys.Name = "tapCurrentKeys"
            Me.tapCurrentKeys.Padding = New System.Windows.Forms.Padding(3)
            Me.tapCurrentKeys.Size = New System.Drawing.Size(526, 361)
            Me.tapCurrentKeys.TabIndex = 0
            Me.tapCurrentKeys.Text = "Current keys"
            Me.tapCurrentKeys.UseVisualStyleBackColor = True
            '
            'lvwCurrentKeys
            '
            Me.lvwCurrentKeys.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.cohKey, Me.cohName, Me.cohDisplayName, Me.cohValue, Me.cohMetadataType, Me.cohDescription})
            Me.lvwCurrentKeys.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lvwCurrentKeys.FullRowSelect = True
            Me.lvwCurrentKeys.Location = New System.Drawing.Point(3, 3)
            Me.lvwCurrentKeys.MultiSelect = False
            Me.lvwCurrentKeys.Name = "lvwCurrentKeys"
            Me.lvwCurrentKeys.Size = New System.Drawing.Size(520, 355)
            Me.lvwCurrentKeys.TabIndex = 0
            Me.lvwCurrentKeys.UseCompatibleStateImageBehavior = False
            Me.lvwCurrentKeys.View = System.Windows.Forms.View.Details
            '
            'cohKey
            '
            Me.cohKey.Text = "Key"
            Me.cohKey.Width = 40
            '
            'cohName
            '
            Me.cohName.Text = "Name"
            Me.cohName.Width = 75
            '
            'cohDisplayName
            '
            Me.cohDisplayName.Text = "Display name"
            Me.cohDisplayName.Width = 80
            '
            'cohValue
            '
            Me.cohValue.Text = "Value"
            Me.cohValue.Width = 150
            '
            'cohMetadataType
            '
            Me.cohMetadataType.Text = "Type"
            '
            'cohDescription
            '
            Me.cohDescription.Text = "Description"
            Me.cohDescription.Width = 200
            '
            'tapPredefinetKeys
            '
            Me.tapPredefinetKeys.Controls.Add(Me.lstPredefinedKeys)
            Me.tapPredefinetKeys.Location = New System.Drawing.Point(4, 22)
            Me.tapPredefinetKeys.Name = "tapPredefinetKeys"
            Me.tapPredefinetKeys.Padding = New System.Windows.Forms.Padding(3)
            Me.tapPredefinetKeys.Size = New System.Drawing.Size(526, 361)
            Me.tapPredefinetKeys.TabIndex = 1
            Me.tapPredefinetKeys.Text = "Predefined keys"
            Me.tapPredefinetKeys.UseVisualStyleBackColor = True
            '
            'lstPredefinedKeys
            '
            Me.lstPredefinedKeys.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lstPredefinedKeys.FormattingEnabled = True
            Me.lstPredefinedKeys.IntegralHeight = False
            Me.lstPredefinedKeys.Location = New System.Drawing.Point(3, 3)
            Me.lstPredefinedKeys.Name = "lstPredefinedKeys"
            Me.lstPredefinedKeys.Size = New System.Drawing.Size(520, 355)
            Me.lstPredefinedKeys.TabIndex = 1
            '
            'tapPredefinedNames
            '
            Me.tapPredefinedNames.Controls.Add(Me.lstPredefinedNames)
            Me.tapPredefinedNames.Location = New System.Drawing.Point(4, 22)
            Me.tapPredefinedNames.Name = "tapPredefinedNames"
            Me.tapPredefinedNames.Padding = New System.Windows.Forms.Padding(3)
            Me.tapPredefinedNames.Size = New System.Drawing.Size(526, 361)
            Me.tapPredefinedNames.TabIndex = 2
            Me.tapPredefinedNames.Text = "Predefined names"
            Me.tapPredefinedNames.UseVisualStyleBackColor = True
            '
            'lstPredefinedNames
            '
            Me.lstPredefinedNames.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lstPredefinedNames.FormattingEnabled = True
            Me.lstPredefinedNames.IntegralHeight = False
            Me.lstPredefinedNames.Location = New System.Drawing.Point(3, 3)
            Me.lstPredefinedNames.Name = "lstPredefinedNames"
            Me.lstPredefinedNames.Size = New System.Drawing.Size(520, 355)
            Me.lstPredefinedNames.TabIndex = 1
            '
            'tlpMatadataActions
            '
            Me.tlpMatadataActions.AutoSize = True
            Me.tlpMatadataActions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpMatadataActions.ColumnCount = 7
            Me.tlpMatadataActions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMatadataActions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMatadataActions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMatadataActions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMatadataActions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMatadataActions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMatadataActions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpMatadataActions.Controls.Add(Me.txtKeyName, 0, 0)
            Me.tlpMatadataActions.Controls.Add(Me.cmdCheckKey, 1, 0)
            Me.tlpMatadataActions.Controls.Add(Me.cmdGetValue, 2, 0)
            Me.tlpMatadataActions.Controls.Add(Me.cmdDisplayName, 3, 0)
            Me.tlpMatadataActions.Controls.Add(Me.cmdDescription, 4, 0)
            Me.tlpMatadataActions.Controls.Add(Me.cmdNameToKey, 5, 0)
            Me.tlpMatadataActions.Controls.Add(Me.cmdKeyToName, 6, 0)
            Me.tlpMatadataActions.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.tlpMatadataActions.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns
            Me.tlpMatadataActions.Location = New System.Drawing.Point(3, 429)
            Me.tlpMatadataActions.Name = "tlpMatadataActions"
            Me.tlpMatadataActions.RowCount = 1
            Me.tlpMatadataActions.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMatadataActions.Size = New System.Drawing.Size(534, 29)
            Me.tlpMatadataActions.TabIndex = 4
            '
            'txtKeyName
            '
            Me.txtKeyName.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtKeyName.Location = New System.Drawing.Point(3, 4)
            Me.txtKeyName.Name = "txtKeyName"
            Me.txtKeyName.Size = New System.Drawing.Size(195, 20)
            Me.txtKeyName.TabIndex = 0
            '
            'cmdCheckKey
            '
            Me.cmdCheckKey.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdCheckKey.AutoSize = True
            Me.cmdCheckKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdCheckKey.Location = New System.Drawing.Point(204, 3)
            Me.cmdCheckKey.Name = "cmdCheckKey"
            Me.cmdCheckKey.Size = New System.Drawing.Size(23, 23)
            Me.cmdCheckKey.TabIndex = 1
            Me.cmdCheckKey.Text = "?"
            Me.totToolTip.SetToolTip(Me.cmdCheckKey, "Check if key/name is contained")
            Me.cmdCheckKey.UseVisualStyleBackColor = True
            '
            'cmdGetValue
            '
            Me.cmdGetValue.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdGetValue.AutoSize = True
            Me.cmdGetValue.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdGetValue.Location = New System.Drawing.Point(233, 3)
            Me.cmdGetValue.Name = "cmdGetValue"
            Me.cmdGetValue.Size = New System.Drawing.Size(44, 23)
            Me.cmdGetValue.TabIndex = 2
            Me.cmdGetValue.Text = "Value"
            Me.totToolTip.SetToolTip(Me.cmdGetValue, "Get value of key/name")
            Me.cmdGetValue.UseVisualStyleBackColor = True
            '
            'cmdDisplayName
            '
            Me.cmdDisplayName.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdDisplayName.AutoSize = True
            Me.cmdDisplayName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdDisplayName.Location = New System.Drawing.Point(283, 3)
            Me.cmdDisplayName.Name = "cmdDisplayName"
            Me.cmdDisplayName.Size = New System.Drawing.Size(80, 23)
            Me.cmdDisplayName.TabIndex = 3
            Me.cmdDisplayName.Text = "Display name"
            Me.totToolTip.SetToolTip(Me.cmdDisplayName, "Get display name for key/name")
            Me.cmdDisplayName.UseVisualStyleBackColor = True
            '
            'cmdDescription
            '
            Me.cmdDescription.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdDescription.AutoSize = True
            Me.cmdDescription.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdDescription.Location = New System.Drawing.Point(369, 3)
            Me.cmdDescription.Name = "cmdDescription"
            Me.cmdDescription.Size = New System.Drawing.Size(70, 23)
            Me.cmdDescription.TabIndex = 4
            Me.cmdDescription.Text = "Description"
            Me.totToolTip.SetToolTip(Me.cmdDescription, "Get description for key/name")
            Me.cmdDescription.UseVisualStyleBackColor = True
            '
            'cmdNameToKey
            '
            Me.cmdNameToKey.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdNameToKey.AutoSize = True
            Me.cmdNameToKey.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdNameToKey.Location = New System.Drawing.Point(445, 3)
            Me.cmdNameToKey.Name = "cmdNameToKey"
            Me.cmdNameToKey.Size = New System.Drawing.Size(35, 23)
            Me.cmdNameToKey.TabIndex = 5
            Me.cmdNameToKey.Text = "Key"
            Me.totToolTip.SetToolTip(Me.cmdNameToKey, "Convert name to key")
            Me.cmdNameToKey.UseVisualStyleBackColor = True
            '
            'cmdKeyToName
            '
            Me.cmdKeyToName.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdKeyToName.AutoSize = True
            Me.cmdKeyToName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdKeyToName.Location = New System.Drawing.Point(486, 3)
            Me.cmdKeyToName.Name = "cmdKeyToName"
            Me.cmdKeyToName.Size = New System.Drawing.Size(45, 23)
            Me.cmdKeyToName.TabIndex = 6
            Me.cmdKeyToName.Text = "Name"
            Me.totToolTip.SetToolTip(Me.cmdKeyToName, "Convert key to name")
            Me.cmdKeyToName.UseVisualStyleBackColor = True
            '
            'lblMetadataName
            '
            Me.lblMetadataName.AutoSize = True
            Me.lblMetadataName.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblMetadataName.Location = New System.Drawing.Point(3, 29)
            Me.lblMetadataName.Name = "lblMetadataName"
            Me.lblMetadataName.Size = New System.Drawing.Size(0, 13)
            Me.lblMetadataName.TabIndex = 1
            '
            'lblMetadataProviderName
            '
            Me.lblMetadataProviderName.AutoSize = True
            Me.lblMetadataProviderName.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblMetadataProviderName.Location = New System.Drawing.Point(3, 16)
            Me.lblMetadataProviderName.Name = "lblMetadataProviderName"
            Me.lblMetadataProviderName.Size = New System.Drawing.Size(0, 13)
            Me.lblMetadataProviderName.TabIndex = 5
            '
            'ofdJPEG
            '
            Me.ofdJPEG.DefaultExt = "jpg"
            Me.ofdJPEG.Filter = "JPEG files (*.jpg, *.jpeg)|*.jpg;*.jpeg"
            Me.ofdJPEG.Title = "Open JPEG file"
            '
            'frmMetadataProvider
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(815, 490)
            Me.Controls.Add(Me.splMain)
            Me.Controls.Add(Me.flpProviders)
            Me.Name = "frmMetadataProvider"
            Me.Text = "Testing Tools.MetadataT.IMetadataProvider"
            Me.flpProviders.ResumeLayout(False)
            Me.flpProviders.PerformLayout()
            Me.fraProvider.ResumeLayout(False)
            Me.fraProvider.PerformLayout()
            Me.tabProvider.ResumeLayout(False)
            Me.tapCurentMetadata.ResumeLayout(False)
            Me.tapSupportedMetadata.ResumeLayout(False)
            Me.tlpMetadataActions.ResumeLayout(False)
            Me.tlpMetadataActions.PerformLayout()
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.ResumeLayout(False)
            Me.fraMatadata.ResumeLayout(False)
            Me.fraMatadata.PerformLayout()
            Me.tabMetadata.ResumeLayout(False)
            Me.tapCurrentKeys.ResumeLayout(False)
            Me.tapPredefinetKeys.ResumeLayout(False)
            Me.tapPredefinedNames.ResumeLayout(False)
            Me.tlpMatadataActions.ResumeLayout(False)
            Me.tlpMatadataActions.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents flpProviders As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents cmdJpeg As System.Windows.Forms.Button
        Friend WithEvents fraProvider As System.Windows.Forms.GroupBox
        Friend WithEvents splMain As System.Windows.Forms.SplitContainer
        Friend WithEvents lblProviderName As System.Windows.Forms.Label
        Friend WithEvents tabProvider As System.Windows.Forms.TabControl
        Friend WithEvents tapCurentMetadata As System.Windows.Forms.TabPage
        Friend WithEvents lstCurrentMetadata As System.Windows.Forms.ListBox
        Friend WithEvents tapSupportedMetadata As System.Windows.Forms.TabPage
        Friend WithEvents lstSupportedMetadata As System.Windows.Forms.ListBox
        Friend WithEvents tlpMetadataActions As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents cmdGetMetadata As System.Windows.Forms.Button
        Friend WithEvents txtMetadataName As System.Windows.Forms.TextBox
        Friend WithEvents fraMatadata As System.Windows.Forms.GroupBox
        Friend WithEvents cmdCheckMetadata As System.Windows.Forms.Button
        Friend WithEvents totToolTip As System.Windows.Forms.ToolTip
        Friend WithEvents tabMetadata As System.Windows.Forms.TabControl
        Friend WithEvents tapCurrentKeys As System.Windows.Forms.TabPage
        Friend WithEvents lvwCurrentKeys As System.Windows.Forms.ListView
        Friend WithEvents cohKey As System.Windows.Forms.ColumnHeader
        Friend WithEvents tapPredefinetKeys As System.Windows.Forms.TabPage
        Friend WithEvents lstPredefinedKeys As System.Windows.Forms.ListBox
        Friend WithEvents tapPredefinedNames As System.Windows.Forms.TabPage
        Friend WithEvents lstPredefinedNames As System.Windows.Forms.ListBox
        Friend WithEvents cohName As System.Windows.Forms.ColumnHeader
        Friend WithEvents cohDisplayName As System.Windows.Forms.ColumnHeader
        Friend WithEvents cohValue As System.Windows.Forms.ColumnHeader
        Friend WithEvents cohDescription As System.Windows.Forms.ColumnHeader
        Friend WithEvents tlpMatadataActions As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents txtKeyName As System.Windows.Forms.TextBox
        Friend WithEvents cmdCheckKey As System.Windows.Forms.Button
        Friend WithEvents cmdGetValue As System.Windows.Forms.Button
        Friend WithEvents cmdDisplayName As System.Windows.Forms.Button
        Friend WithEvents cmdDescription As System.Windows.Forms.Button
        Friend WithEvents lblMetadataName As System.Windows.Forms.Label
        Friend WithEvents cmdKeyToName As System.Windows.Forms.Button
        Friend WithEvents cmdNameToKey As System.Windows.Forms.Button
        Friend WithEvents ofdJPEG As System.Windows.Forms.OpenFileDialog
        Friend WithEvents lblMetadataProviderName As System.Windows.Forms.Label
        Friend WithEvents cohMetadataType As System.Windows.Forms.ColumnHeader
    End Class
End Namespace