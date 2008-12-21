Namespace TestsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmStaticProperiesTest
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
            Me.obwSelectType = New Tools.WindowsT.FormsT.ObjectBrowser
            Me.flpButtons = New System.Windows.Forms.FlowLayoutPanel
            Me.cmdTest = New System.Windows.Forms.Button
            Me.fraTypes = New System.Windows.Forms.GroupBox
            Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
            Me.optTPublic = New System.Windows.Forms.RadioButton
            Me.optTFriend = New System.Windows.Forms.RadioButton
            Me.optTAll = New System.Windows.Forms.RadioButton
            Me.fraProperties = New System.Windows.Forms.GroupBox
            Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel
            Me.chkPPublic = New System.Windows.Forms.CheckBox
            Me.chkPPrivate = New System.Windows.Forms.CheckBox
            Me.chkSuccessfull = New System.Windows.Forms.CheckBox
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.lvwResults = New System.Windows.Forms.ListView
            Me.cohProperty = New System.Windows.Forms.ColumnHeader
            Me.cohType = New System.Windows.Forms.ColumnHeader
            Me.cohStage = New System.Windows.Forms.ColumnHeader
            Me.cohExceptionType = New System.Windows.Forms.ColumnHeader
            Me.cohExceptionMessage = New System.Windows.Forms.ColumnHeader
            Me.fraStatistic = New System.Windows.Forms.GroupBox
            Me.tlpStatistic = New System.Windows.Forms.TableLayoutPanel
            Me.lblSTestedI = New System.Windows.Forms.Label
            Me.lblSTested = New System.Windows.Forms.Label
            Me.lblSSuccessI = New System.Windows.Forms.Label
            Me.lblSSuccess = New System.Windows.Forms.Label
            Me.lblSNonSuccessI = New System.Windows.Forms.Label
            Me.lblSNonSuccess = New System.Windows.Forms.Label
            Me.lblSGetGetMethodI = New System.Windows.Forms.Label
            Me.lblSGetGetMethod = New System.Windows.Forms.Label
            Me.lblSHasGetMethodI = New System.Windows.Forms.Label
            Me.lblSHasGetMethod = New System.Windows.Forms.Label
            Me.lblSIsIndexedI = New System.Windows.Forms.Label
            Me.lblSIsIndexed = New System.Windows.Forms.Label
            Me.lblSInvokeGetterI = New System.Windows.Forms.Label
            Me.lblSInvokeGetter = New System.Windows.Forms.Label
            Me.lblSGetterBeingInvokedI = New System.Windows.Forms.Label
            Me.lblSGeterBeingInvoked = New System.Windows.Forms.Label
            Me.lblSValueIsNullI = New System.Windows.Forms.Label
            Me.lblSValueIsNull = New System.Windows.Forms.Label
            Me.tosToolbar = New System.Windows.Forms.ToolStrip
            Me.tsbOpenAssembly = New System.Windows.Forms.ToolStripButton
            Me.tscCultures = New System.Windows.Forms.ToolStripComboBox
            Me.ofdOpenAssembly = New System.Windows.Forms.OpenFileDialog
            Me.splTop = New System.Windows.Forms.SplitContainer
            Me.prgValueProperties = New System.Windows.Forms.PropertyGrid
            Me.fraPropertyValue = New System.Windows.Forms.GroupBox
            Me.tlpPropertyValue = New System.Windows.Forms.TableLayoutPanel
            Me.lblType = New System.Windows.Forms.Label
            Me.lblToString = New System.Windows.Forms.Label
            Me.flpButtons.SuspendLayout()
            Me.fraTypes.SuspendLayout()
            Me.FlowLayoutPanel1.SuspendLayout()
            Me.fraProperties.SuspendLayout()
            Me.FlowLayoutPanel2.SuspendLayout()
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.fraStatistic.SuspendLayout()
            Me.tlpStatistic.SuspendLayout()
            Me.tosToolbar.SuspendLayout()
            Me.splTop.Panel1.SuspendLayout()
            Me.splTop.Panel2.SuspendLayout()
            Me.splTop.SuspendLayout()
            Me.fraPropertyValue.SuspendLayout()
            Me.tlpPropertyValue.SuspendLayout()
            Me.SuspendLayout()
            '
            'obwSelectType
            '
            Me.obwSelectType.DisplayDescription = False
            Me.obwSelectType.DisplayMemberList = False
            Me.obwSelectType.DisplayProperties = False
            Me.obwSelectType.Dock = System.Windows.Forms.DockStyle.Fill
            Me.obwSelectType.Location = New System.Drawing.Point(0, 0)
            Me.obwSelectType.Name = "obwSelectType"
            Me.obwSelectType.ShowFlatNamespaces = True
            Me.obwSelectType.Size = New System.Drawing.Size(697, 310)
            Me.obwSelectType.TabIndex = 0
            '
            'flpButtons
            '
            Me.flpButtons.AutoSize = True
            Me.flpButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpButtons.Controls.Add(Me.cmdTest)
            Me.flpButtons.Controls.Add(Me.fraTypes)
            Me.flpButtons.Controls.Add(Me.fraProperties)
            Me.flpButtons.Controls.Add(Me.chkSuccessfull)
            Me.flpButtons.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.flpButtons.Location = New System.Drawing.Point(0, 310)
            Me.flpButtons.Margin = New System.Windows.Forms.Padding(0)
            Me.flpButtons.Name = "flpButtons"
            Me.flpButtons.Size = New System.Drawing.Size(997, 49)
            Me.flpButtons.TabIndex = 1
            '
            'cmdTest
            '
            Me.cmdTest.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdTest.AutoSize = True
            Me.cmdTest.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdTest.Location = New System.Drawing.Point(0, 13)
            Me.cmdTest.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdTest.Name = "cmdTest"
            Me.cmdTest.Size = New System.Drawing.Size(38, 23)
            Me.cmdTest.TabIndex = 0
            Me.cmdTest.Text = "Test"
            Me.cmdTest.UseVisualStyleBackColor = True
            '
            'fraTypes
            '
            Me.fraTypes.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.fraTypes.AutoSize = True
            Me.fraTypes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.fraTypes.Controls.Add(Me.FlowLayoutPanel1)
            Me.fraTypes.Location = New System.Drawing.Point(41, 0)
            Me.fraTypes.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
            Me.fraTypes.Name = "fraTypes"
            Me.fraTypes.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
            Me.fraTypes.Size = New System.Drawing.Size(218, 49)
            Me.fraTypes.TabIndex = 1
            Me.fraTypes.TabStop = False
            Me.fraTypes.Text = "Types"
            '
            'FlowLayoutPanel1
            '
            Me.FlowLayoutPanel1.AutoSize = True
            Me.FlowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.FlowLayoutPanel1.Controls.Add(Me.optTPublic)
            Me.FlowLayoutPanel1.Controls.Add(Me.optTFriend)
            Me.FlowLayoutPanel1.Controls.Add(Me.optTAll)
            Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 13)
            Me.FlowLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
            Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
            Me.FlowLayoutPanel1.Size = New System.Drawing.Size(212, 23)
            Me.FlowLayoutPanel1.TabIndex = 0
            '
            'optTPublic
            '
            Me.optTPublic.AutoSize = True
            Me.optTPublic.Location = New System.Drawing.Point(3, 3)
            Me.optTPublic.Name = "optTPublic"
            Me.optTPublic.Size = New System.Drawing.Size(54, 17)
            Me.optTPublic.TabIndex = 1
            Me.optTPublic.Text = "Public"
            Me.optTPublic.UseVisualStyleBackColor = True
            '
            'optTFriend
            '
            Me.optTFriend.AutoSize = True
            Me.optTFriend.Location = New System.Drawing.Point(63, 3)
            Me.optTFriend.Name = "optTFriend"
            Me.optTFriend.Size = New System.Drawing.Size(104, 17)
            Me.optTFriend.TabIndex = 2
            Me.optTFriend.Text = "Public and friend"
            Me.optTFriend.UseVisualStyleBackColor = True
            '
            'optTAll
            '
            Me.optTAll.AutoSize = True
            Me.optTAll.Checked = True
            Me.optTAll.Location = New System.Drawing.Point(173, 3)
            Me.optTAll.Name = "optTAll"
            Me.optTAll.Size = New System.Drawing.Size(36, 17)
            Me.optTAll.TabIndex = 3
            Me.optTAll.TabStop = True
            Me.optTAll.Text = "All"
            Me.optTAll.UseVisualStyleBackColor = True
            '
            'fraProperties
            '
            Me.fraProperties.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.fraProperties.AutoSize = True
            Me.fraProperties.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.fraProperties.Controls.Add(Me.FlowLayoutPanel2)
            Me.fraProperties.Location = New System.Drawing.Point(265, 0)
            Me.fraProperties.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
            Me.fraProperties.Name = "fraProperties"
            Me.fraProperties.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
            Me.fraProperties.Size = New System.Drawing.Size(150, 49)
            Me.fraProperties.TabIndex = 2
            Me.fraProperties.TabStop = False
            Me.fraProperties.Text = "Properties"
            '
            'FlowLayoutPanel2
            '
            Me.FlowLayoutPanel2.AutoSize = True
            Me.FlowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.FlowLayoutPanel2.Controls.Add(Me.chkPPublic)
            Me.FlowLayoutPanel2.Controls.Add(Me.chkPPrivate)
            Me.FlowLayoutPanel2.Location = New System.Drawing.Point(3, 13)
            Me.FlowLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
            Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
            Me.FlowLayoutPanel2.Size = New System.Drawing.Size(144, 23)
            Me.FlowLayoutPanel2.TabIndex = 0
            '
            'chkPPublic
            '
            Me.chkPPublic.AutoSize = True
            Me.chkPPublic.Checked = True
            Me.chkPPublic.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkPPublic.Location = New System.Drawing.Point(3, 3)
            Me.chkPPublic.Name = "chkPPublic"
            Me.chkPPublic.Size = New System.Drawing.Size(55, 17)
            Me.chkPPublic.TabIndex = 0
            Me.chkPPublic.Text = "Public"
            Me.chkPPublic.UseVisualStyleBackColor = True
            '
            'chkPPrivate
            '
            Me.chkPPrivate.AutoSize = True
            Me.chkPPrivate.Checked = True
            Me.chkPPrivate.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkPPrivate.Location = New System.Drawing.Point(64, 3)
            Me.chkPPrivate.Name = "chkPPrivate"
            Me.chkPPrivate.Size = New System.Drawing.Size(77, 17)
            Me.chkPPrivate.TabIndex = 3
            Me.chkPPrivate.Text = "Non-public"
            Me.chkPPrivate.UseVisualStyleBackColor = True
            '
            'chkSuccessfull
            '
            Me.chkSuccessfull.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.chkSuccessfull.AutoSize = True
            Me.chkSuccessfull.Location = New System.Drawing.Point(421, 16)
            Me.chkSuccessfull.Name = "chkSuccessfull"
            Me.chkSuccessfull.Size = New System.Drawing.Size(108, 17)
            Me.chkSuccessfull.TabIndex = 3
            Me.chkSuccessfull.Text = "Show successfull"
            Me.chkSuccessfull.UseVisualStyleBackColor = True
            '
            'splMain
            '
            Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splMain.Location = New System.Drawing.Point(0, 25)
            Me.splMain.Name = "splMain"
            Me.splMain.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'splMain.Panel1
            '
            Me.splMain.Panel1.Controls.Add(Me.splTop)
            Me.splMain.Panel1.Controls.Add(Me.flpButtons)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.lvwResults)
            Me.splMain.Panel2.Controls.Add(Me.fraStatistic)
            Me.splMain.Size = New System.Drawing.Size(997, 573)
            Me.splMain.SplitterDistance = 359
            Me.splMain.TabIndex = 2
            '
            'lvwResults
            '
            Me.lvwResults.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.cohProperty, Me.cohType, Me.cohStage, Me.cohExceptionType, Me.cohExceptionMessage})
            Me.lvwResults.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lvwResults.FullRowSelect = True
            Me.lvwResults.HideSelection = False
            Me.lvwResults.Location = New System.Drawing.Point(0, 0)
            Me.lvwResults.MultiSelect = False
            Me.lvwResults.Name = "lvwResults"
            Me.lvwResults.Size = New System.Drawing.Size(879, 210)
            Me.lvwResults.TabIndex = 2
            Me.lvwResults.UseCompatibleStateImageBehavior = False
            Me.lvwResults.View = System.Windows.Forms.View.Details
            '
            'cohProperty
            '
            Me.cohProperty.Text = "Property"
            Me.cohProperty.Width = 120
            '
            'cohType
            '
            Me.cohType.Text = "Type"
            Me.cohType.Width = 150
            '
            'cohStage
            '
            Me.cohStage.Text = "Stage"
            Me.cohStage.Width = 75
            '
            'cohExceptionType
            '
            Me.cohExceptionType.Text = "Exception type"
            Me.cohExceptionType.Width = 120
            '
            'cohExceptionMessage
            '
            Me.cohExceptionMessage.Text = "Exception"
            Me.cohExceptionMessage.Width = 300
            '
            'fraStatistic
            '
            Me.fraStatistic.AutoSize = True
            Me.fraStatistic.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.fraStatistic.Controls.Add(Me.tlpStatistic)
            Me.fraStatistic.Dock = System.Windows.Forms.DockStyle.Right
            Me.fraStatistic.Location = New System.Drawing.Point(879, 0)
            Me.fraStatistic.Name = "fraStatistic"
            Me.fraStatistic.Size = New System.Drawing.Size(118, 210)
            Me.fraStatistic.TabIndex = 4
            Me.fraStatistic.TabStop = False
            Me.fraStatistic.Text = "Statistic"
            '
            'tlpStatistic
            '
            Me.tlpStatistic.AutoSize = True
            Me.tlpStatistic.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpStatistic.ColumnCount = 2
            Me.tlpStatistic.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpStatistic.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpStatistic.Controls.Add(Me.lblSTestedI, 0, 0)
            Me.tlpStatistic.Controls.Add(Me.lblSTested, 1, 0)
            Me.tlpStatistic.Controls.Add(Me.lblSSuccessI, 0, 1)
            Me.tlpStatistic.Controls.Add(Me.lblSSuccess, 1, 1)
            Me.tlpStatistic.Controls.Add(Me.lblSNonSuccessI, 0, 2)
            Me.tlpStatistic.Controls.Add(Me.lblSNonSuccess, 1, 2)
            Me.tlpStatistic.Controls.Add(Me.lblSGetGetMethodI, 0, 3)
            Me.tlpStatistic.Controls.Add(Me.lblSGetGetMethod, 1, 3)
            Me.tlpStatistic.Controls.Add(Me.lblSHasGetMethodI, 0, 4)
            Me.tlpStatistic.Controls.Add(Me.lblSHasGetMethod, 1, 4)
            Me.tlpStatistic.Controls.Add(Me.lblSIsIndexedI, 0, 5)
            Me.tlpStatistic.Controls.Add(Me.lblSIsIndexed, 1, 5)
            Me.tlpStatistic.Controls.Add(Me.lblSInvokeGetterI, 0, 6)
            Me.tlpStatistic.Controls.Add(Me.lblSInvokeGetter, 1, 6)
            Me.tlpStatistic.Controls.Add(Me.lblSGetterBeingInvokedI, 0, 7)
            Me.tlpStatistic.Controls.Add(Me.lblSGeterBeingInvoked, 1, 7)
            Me.tlpStatistic.Controls.Add(Me.lblSValueIsNullI, 0, 8)
            Me.tlpStatistic.Controls.Add(Me.lblSValueIsNull, 1, 8)
            Me.tlpStatistic.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpStatistic.Location = New System.Drawing.Point(3, 16)
            Me.tlpStatistic.Name = "tlpStatistic"
            Me.tlpStatistic.RowCount = 10
            Me.tlpStatistic.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpStatistic.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpStatistic.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpStatistic.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpStatistic.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpStatistic.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpStatistic.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpStatistic.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpStatistic.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpStatistic.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpStatistic.Size = New System.Drawing.Size(112, 191)
            Me.tlpStatistic.TabIndex = 0
            '
            'lblSTestedI
            '
            Me.lblSTestedI.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSTestedI.AutoSize = True
            Me.lblSTestedI.Location = New System.Drawing.Point(13, 0)
            Me.lblSTestedI.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSTestedI.Name = "lblSTestedI"
            Me.lblSTestedI.Size = New System.Drawing.Size(86, 13)
            Me.lblSTestedI.TabIndex = 0
            Me.lblSTestedI.Text = "Properties tested"
            Me.lblSTestedI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSTested
            '
            Me.lblSTested.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSTested.AutoSize = True
            Me.lblSTested.Location = New System.Drawing.Point(99, 0)
            Me.lblSTested.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSTested.Name = "lblSTested"
            Me.lblSTested.Size = New System.Drawing.Size(13, 13)
            Me.lblSTested.TabIndex = 1
            Me.lblSTested.Text = "0"
            Me.lblSTested.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSSuccessI
            '
            Me.lblSSuccessI.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSSuccessI.AutoSize = True
            Me.lblSSuccessI.BackColor = System.Drawing.Color.Lime
            Me.lblSSuccessI.Location = New System.Drawing.Point(51, 13)
            Me.lblSSuccessI.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSSuccessI.Name = "lblSSuccessI"
            Me.lblSSuccessI.Size = New System.Drawing.Size(48, 13)
            Me.lblSSuccessI.TabIndex = 2
            Me.lblSSuccessI.Text = "Success"
            Me.lblSSuccessI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSSuccess
            '
            Me.lblSSuccess.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSSuccess.AutoSize = True
            Me.lblSSuccess.BackColor = System.Drawing.Color.Lime
            Me.lblSSuccess.Location = New System.Drawing.Point(99, 13)
            Me.lblSSuccess.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSSuccess.Name = "lblSSuccess"
            Me.lblSSuccess.Size = New System.Drawing.Size(13, 13)
            Me.lblSSuccess.TabIndex = 3
            Me.lblSSuccess.Text = "0"
            Me.lblSSuccess.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSNonSuccessI
            '
            Me.lblSNonSuccessI.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSNonSuccessI.AutoSize = True
            Me.lblSNonSuccessI.Location = New System.Drawing.Point(30, 26)
            Me.lblSNonSuccessI.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSNonSuccessI.Name = "lblSNonSuccessI"
            Me.lblSNonSuccessI.Size = New System.Drawing.Size(69, 13)
            Me.lblSNonSuccessI.TabIndex = 4
            Me.lblSNonSuccessI.Text = "Non-success"
            Me.lblSNonSuccessI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSNonSuccess
            '
            Me.lblSNonSuccess.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSNonSuccess.AutoSize = True
            Me.lblSNonSuccess.Location = New System.Drawing.Point(99, 26)
            Me.lblSNonSuccess.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSNonSuccess.Name = "lblSNonSuccess"
            Me.lblSNonSuccess.Size = New System.Drawing.Size(13, 13)
            Me.lblSNonSuccess.TabIndex = 5
            Me.lblSNonSuccess.Text = "0"
            Me.lblSNonSuccess.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSGetGetMethodI
            '
            Me.lblSGetGetMethodI.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSGetGetMethodI.AutoSize = True
            Me.lblSGetGetMethodI.BackColor = System.Drawing.Color.Orange
            Me.lblSGetGetMethodI.Location = New System.Drawing.Point(22, 39)
            Me.lblSGetGetMethodI.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSGetGetMethodI.Name = "lblSGetGetMethodI"
            Me.lblSGetGetMethodI.Size = New System.Drawing.Size(77, 13)
            Me.lblSGetGetMethodI.TabIndex = 6
            Me.lblSGetGetMethodI.Text = "GetGetMethod"
            Me.lblSGetGetMethodI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSGetGetMethod
            '
            Me.lblSGetGetMethod.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSGetGetMethod.AutoSize = True
            Me.lblSGetGetMethod.BackColor = System.Drawing.Color.Orange
            Me.lblSGetGetMethod.Location = New System.Drawing.Point(99, 39)
            Me.lblSGetGetMethod.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSGetGetMethod.Name = "lblSGetGetMethod"
            Me.lblSGetGetMethod.Size = New System.Drawing.Size(13, 13)
            Me.lblSGetGetMethod.TabIndex = 7
            Me.lblSGetGetMethod.Text = "0"
            Me.lblSGetGetMethod.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSHasGetMethodI
            '
            Me.lblSHasGetMethodI.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSHasGetMethodI.AutoSize = True
            Me.lblSHasGetMethodI.BackColor = System.Drawing.Color.White
            Me.lblSHasGetMethodI.Location = New System.Drawing.Point(20, 52)
            Me.lblSHasGetMethodI.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSHasGetMethodI.Name = "lblSHasGetMethodI"
            Me.lblSHasGetMethodI.Size = New System.Drawing.Size(79, 13)
            Me.lblSHasGetMethodI.TabIndex = 8
            Me.lblSHasGetMethodI.Text = "HasGetMethod"
            Me.lblSHasGetMethodI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSHasGetMethod
            '
            Me.lblSHasGetMethod.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSHasGetMethod.AutoSize = True
            Me.lblSHasGetMethod.BackColor = System.Drawing.Color.White
            Me.lblSHasGetMethod.Location = New System.Drawing.Point(99, 52)
            Me.lblSHasGetMethod.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSHasGetMethod.Name = "lblSHasGetMethod"
            Me.lblSHasGetMethod.Size = New System.Drawing.Size(13, 13)
            Me.lblSHasGetMethod.TabIndex = 9
            Me.lblSHasGetMethod.Text = "0"
            Me.lblSHasGetMethod.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSIsIndexedI
            '
            Me.lblSIsIndexedI.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSIsIndexedI.AutoSize = True
            Me.lblSIsIndexedI.BackColor = System.Drawing.Color.White
            Me.lblSIsIndexedI.Location = New System.Drawing.Point(46, 65)
            Me.lblSIsIndexedI.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSIsIndexedI.Name = "lblSIsIndexedI"
            Me.lblSIsIndexedI.Size = New System.Drawing.Size(53, 13)
            Me.lblSIsIndexedI.TabIndex = 10
            Me.lblSIsIndexedI.Text = "IsIndexed"
            Me.lblSIsIndexedI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSIsIndexed
            '
            Me.lblSIsIndexed.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSIsIndexed.AutoSize = True
            Me.lblSIsIndexed.BackColor = System.Drawing.Color.White
            Me.lblSIsIndexed.Location = New System.Drawing.Point(99, 65)
            Me.lblSIsIndexed.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSIsIndexed.Name = "lblSIsIndexed"
            Me.lblSIsIndexed.Size = New System.Drawing.Size(13, 13)
            Me.lblSIsIndexed.TabIndex = 11
            Me.lblSIsIndexed.Text = "0"
            Me.lblSIsIndexed.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSInvokeGetterI
            '
            Me.lblSInvokeGetterI.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSInvokeGetterI.AutoSize = True
            Me.lblSInvokeGetterI.BackColor = System.Drawing.Color.Orange
            Me.lblSInvokeGetterI.Location = New System.Drawing.Point(30, 78)
            Me.lblSInvokeGetterI.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSInvokeGetterI.Name = "lblSInvokeGetterI"
            Me.lblSInvokeGetterI.Size = New System.Drawing.Size(69, 13)
            Me.lblSInvokeGetterI.TabIndex = 12
            Me.lblSInvokeGetterI.Text = "InvokeGetter"
            Me.lblSInvokeGetterI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSInvokeGetter
            '
            Me.lblSInvokeGetter.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSInvokeGetter.AutoSize = True
            Me.lblSInvokeGetter.BackColor = System.Drawing.Color.Orange
            Me.lblSInvokeGetter.Location = New System.Drawing.Point(99, 78)
            Me.lblSInvokeGetter.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSInvokeGetter.Name = "lblSInvokeGetter"
            Me.lblSInvokeGetter.Size = New System.Drawing.Size(13, 13)
            Me.lblSInvokeGetter.TabIndex = 13
            Me.lblSInvokeGetter.Text = "0"
            Me.lblSInvokeGetter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSGetterBeingInvokedI
            '
            Me.lblSGetterBeingInvokedI.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSGetterBeingInvokedI.AutoSize = True
            Me.lblSGetterBeingInvokedI.BackColor = System.Drawing.Color.Red
            Me.lblSGetterBeingInvokedI.Location = New System.Drawing.Point(0, 91)
            Me.lblSGetterBeingInvokedI.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSGetterBeingInvokedI.Name = "lblSGetterBeingInvokedI"
            Me.lblSGetterBeingInvokedI.Size = New System.Drawing.Size(99, 13)
            Me.lblSGetterBeingInvokedI.TabIndex = 14
            Me.lblSGetterBeingInvokedI.Text = "GeterBeingInvoked"
            Me.lblSGetterBeingInvokedI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSGeterBeingInvoked
            '
            Me.lblSGeterBeingInvoked.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSGeterBeingInvoked.AutoSize = True
            Me.lblSGeterBeingInvoked.BackColor = System.Drawing.Color.Red
            Me.lblSGeterBeingInvoked.Location = New System.Drawing.Point(99, 91)
            Me.lblSGeterBeingInvoked.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSGeterBeingInvoked.Name = "lblSGeterBeingInvoked"
            Me.lblSGeterBeingInvoked.Size = New System.Drawing.Size(13, 13)
            Me.lblSGeterBeingInvoked.TabIndex = 15
            Me.lblSGeterBeingInvoked.Text = "0"
            Me.lblSGeterBeingInvoked.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSValueIsNullI
            '
            Me.lblSValueIsNullI.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSValueIsNullI.AutoSize = True
            Me.lblSValueIsNullI.BackColor = System.Drawing.Color.Yellow
            Me.lblSValueIsNullI.Location = New System.Drawing.Point(39, 104)
            Me.lblSValueIsNullI.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSValueIsNullI.Name = "lblSValueIsNullI"
            Me.lblSValueIsNullI.Size = New System.Drawing.Size(60, 13)
            Me.lblSValueIsNullI.TabIndex = 16
            Me.lblSValueIsNullI.Text = "ValueIsNull"
            Me.lblSValueIsNullI.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'lblSValueIsNull
            '
            Me.lblSValueIsNull.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblSValueIsNull.AutoSize = True
            Me.lblSValueIsNull.BackColor = System.Drawing.Color.Yellow
            Me.lblSValueIsNull.Location = New System.Drawing.Point(99, 104)
            Me.lblSValueIsNull.Margin = New System.Windows.Forms.Padding(0)
            Me.lblSValueIsNull.Name = "lblSValueIsNull"
            Me.lblSValueIsNull.Size = New System.Drawing.Size(13, 13)
            Me.lblSValueIsNull.TabIndex = 17
            Me.lblSValueIsNull.Text = "0"
            Me.lblSValueIsNull.TextAlign = System.Drawing.ContentAlignment.MiddleRight
            '
            'tosToolbar
            '
            Me.tosToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.tosToolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbOpenAssembly, Me.tscCultures})
            Me.tosToolbar.Location = New System.Drawing.Point(0, 0)
            Me.tosToolbar.Name = "tosToolbar"
            Me.tosToolbar.Size = New System.Drawing.Size(997, 25)
            Me.tosToolbar.TabIndex = 3
            Me.tosToolbar.Text = "ToolStrip1"
            '
            'tsbOpenAssembly
            '
            Me.tsbOpenAssembly.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.tsbOpenAssembly.Image = Global.Tools.Tests.My.Resources.Resources.openHS
            Me.tsbOpenAssembly.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.tsbOpenAssembly.Name = "tsbOpenAssembly"
            Me.tsbOpenAssembly.Size = New System.Drawing.Size(23, 22)
            Me.tsbOpenAssembly.Text = "ToolStripButton1"
            '
            'tscCultures
            '
            Me.tscCultures.Name = "tscCultures"
            Me.tscCultures.Size = New System.Drawing.Size(121, 25)
            Me.tscCultures.Sorted = True
            '
            'ofdOpenAssembly
            '
            Me.ofdOpenAssembly.DefaultExt = "dll"
            Me.ofdOpenAssembly.Filter = "Assemblies (*.exe, *.dll)|*.exe;*.dll"
            '
            'splTop
            '
            Me.splTop.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splTop.Location = New System.Drawing.Point(0, 0)
            Me.splTop.Name = "splTop"
            '
            'splTop.Panel1
            '
            Me.splTop.Panel1.Controls.Add(Me.obwSelectType)
            '
            'splTop.Panel2
            '
            Me.splTop.Panel2.Controls.Add(Me.fraPropertyValue)
            Me.splTop.Size = New System.Drawing.Size(997, 310)
            Me.splTop.SplitterDistance = 697
            Me.splTop.TabIndex = 2
            '
            'prgValueProperties
            '
            Me.tlpPropertyValue.SetColumnSpan(Me.prgValueProperties, 2)
            Me.prgValueProperties.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgValueProperties.Location = New System.Drawing.Point(3, 16)
            Me.prgValueProperties.Name = "prgValueProperties"
            Me.prgValueProperties.Size = New System.Drawing.Size(284, 272)
            Me.prgValueProperties.TabIndex = 1
            '
            'fraPropertyValue
            '
            Me.fraPropertyValue.Controls.Add(Me.tlpPropertyValue)
            Me.fraPropertyValue.Dock = System.Windows.Forms.DockStyle.Fill
            Me.fraPropertyValue.Location = New System.Drawing.Point(0, 0)
            Me.fraPropertyValue.Name = "fraPropertyValue"
            Me.fraPropertyValue.Size = New System.Drawing.Size(296, 310)
            Me.fraPropertyValue.TabIndex = 2
            Me.fraPropertyValue.TabStop = False
            Me.fraPropertyValue.Text = "Property value"
            '
            'tlpPropertyValue
            '
            Me.tlpPropertyValue.ColumnCount = 2
            Me.tlpPropertyValue.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpPropertyValue.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpPropertyValue.Controls.Add(Me.prgValueProperties, 0, 1)
            Me.tlpPropertyValue.Controls.Add(Me.lblType, 0, 0)
            Me.tlpPropertyValue.Controls.Add(Me.lblToString, 1, 0)
            Me.tlpPropertyValue.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpPropertyValue.Location = New System.Drawing.Point(3, 16)
            Me.tlpPropertyValue.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpPropertyValue.Name = "tlpPropertyValue"
            Me.tlpPropertyValue.RowCount = 2
            Me.tlpPropertyValue.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpPropertyValue.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpPropertyValue.Size = New System.Drawing.Size(290, 291)
            Me.tlpPropertyValue.TabIndex = 2
            '
            'lblType
            '
            Me.lblType.AutoSize = True
            Me.lblType.Location = New System.Drawing.Point(3, 0)
            Me.lblType.Name = "lblType"
            Me.lblType.Size = New System.Drawing.Size(0, 13)
            Me.lblType.TabIndex = 2
            '
            'lblToString
            '
            Me.lblToString.AutoSize = True
            Me.lblToString.Location = New System.Drawing.Point(148, 0)
            Me.lblToString.Name = "lblToString"
            Me.lblToString.Size = New System.Drawing.Size(0, 13)
            Me.lblToString.TabIndex = 3
            '
            'frmStaticProperiesTest
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(997, 598)
            Me.Controls.Add(Me.splMain)
            Me.Controls.Add(Me.tosToolbar)
            Me.Name = "frmStaticProperiesTest"
            Me.Text = "Testing Tools.TestsT.StaticPropertiesTest"
            Me.flpButtons.ResumeLayout(False)
            Me.flpButtons.PerformLayout()
            Me.fraTypes.ResumeLayout(False)
            Me.fraTypes.PerformLayout()
            Me.FlowLayoutPanel1.ResumeLayout(False)
            Me.FlowLayoutPanel1.PerformLayout()
            Me.fraProperties.ResumeLayout(False)
            Me.fraProperties.PerformLayout()
            Me.FlowLayoutPanel2.ResumeLayout(False)
            Me.FlowLayoutPanel2.PerformLayout()
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel1.PerformLayout()
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.Panel2.PerformLayout()
            Me.splMain.ResumeLayout(False)
            Me.fraStatistic.ResumeLayout(False)
            Me.fraStatistic.PerformLayout()
            Me.tlpStatistic.ResumeLayout(False)
            Me.tlpStatistic.PerformLayout()
            Me.tosToolbar.ResumeLayout(False)
            Me.tosToolbar.PerformLayout()
            Me.splTop.Panel1.ResumeLayout(False)
            Me.splTop.Panel2.ResumeLayout(False)
            Me.splTop.ResumeLayout(False)
            Me.fraPropertyValue.ResumeLayout(False)
            Me.tlpPropertyValue.ResumeLayout(False)
            Me.tlpPropertyValue.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents obwSelectType As Tools.WindowsT.FormsT.ObjectBrowser
        Friend WithEvents flpButtons As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents cmdTest As System.Windows.Forms.Button
        Friend WithEvents splMain As System.Windows.Forms.SplitContainer
        Friend WithEvents lvwResults As System.Windows.Forms.ListView
        Friend WithEvents cohProperty As System.Windows.Forms.ColumnHeader
        Friend WithEvents cohType As System.Windows.Forms.ColumnHeader
        Friend WithEvents cohStage As System.Windows.Forms.ColumnHeader
        Friend WithEvents cohExceptionType As System.Windows.Forms.ColumnHeader
        Friend WithEvents cohExceptionMessage As System.Windows.Forms.ColumnHeader
        Friend WithEvents tosToolbar As System.Windows.Forms.ToolStrip
        Friend WithEvents tsbOpenAssembly As System.Windows.Forms.ToolStripButton
        Friend WithEvents ofdOpenAssembly As System.Windows.Forms.OpenFileDialog
        Friend WithEvents fraTypes As System.Windows.Forms.GroupBox
        Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents optTPublic As System.Windows.Forms.RadioButton
        Friend WithEvents optTFriend As System.Windows.Forms.RadioButton
        Friend WithEvents optTAll As System.Windows.Forms.RadioButton
        Friend WithEvents fraProperties As System.Windows.Forms.GroupBox
        Friend WithEvents FlowLayoutPanel2 As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents chkPPublic As System.Windows.Forms.CheckBox
        Friend WithEvents chkPPrivate As System.Windows.Forms.CheckBox
        Friend WithEvents tscCultures As System.Windows.Forms.ToolStripComboBox
        Friend WithEvents chkSuccessfull As System.Windows.Forms.CheckBox
        Friend WithEvents fraStatistic As System.Windows.Forms.GroupBox
        Friend WithEvents tlpStatistic As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents lblSTestedI As System.Windows.Forms.Label
        Friend WithEvents lblSTested As System.Windows.Forms.Label
        Friend WithEvents lblSSuccessI As System.Windows.Forms.Label
        Friend WithEvents lblSSuccess As System.Windows.Forms.Label
        Friend WithEvents lblSNonSuccessI As System.Windows.Forms.Label
        Friend WithEvents lblSNonSuccess As System.Windows.Forms.Label
        Friend WithEvents lblSGetGetMethodI As System.Windows.Forms.Label
        Friend WithEvents lblSGetGetMethod As System.Windows.Forms.Label
        Friend WithEvents lblSHasGetMethodI As System.Windows.Forms.Label
        Friend WithEvents lblSHasGetMethod As System.Windows.Forms.Label
        Friend WithEvents lblSIsIndexedI As System.Windows.Forms.Label
        Friend WithEvents lblSIsIndexed As System.Windows.Forms.Label
        Friend WithEvents lblSInvokeGetterI As System.Windows.Forms.Label
        Friend WithEvents lblSInvokeGetter As System.Windows.Forms.Label
        Friend WithEvents lblSGetterBeingInvokedI As System.Windows.Forms.Label
        Friend WithEvents lblSGeterBeingInvoked As System.Windows.Forms.Label
        Friend WithEvents lblSValueIsNullI As System.Windows.Forms.Label
        Friend WithEvents lblSValueIsNull As System.Windows.Forms.Label
        Friend WithEvents splTop As System.Windows.Forms.SplitContainer
        Friend WithEvents prgValueProperties As System.Windows.Forms.PropertyGrid
        Friend WithEvents fraPropertyValue As System.Windows.Forms.GroupBox
        Friend WithEvents tlpPropertyValue As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents lblType As System.Windows.Forms.Label
        Friend WithEvents lblToString As System.Windows.Forms.Label
    End Class
End Namespace