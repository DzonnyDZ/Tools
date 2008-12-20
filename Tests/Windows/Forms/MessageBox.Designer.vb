
Namespace WindowsT.FormsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmMessageBox
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
            Me.flpCommands = New System.Windows.Forms.FlowLayoutPanel
            Me.cmdCreate = New System.Windows.Forms.Button
            Me.optWinForms = New System.Windows.Forms.RadioButton
            Me.optWPF = New System.Windows.Forms.RadioButton
            Me.cmdShowDialog = New System.Windows.Forms.Button
            Me.cmdShow = New System.Windows.Forms.Button
            Me.cmdShowFloating = New System.Windows.Forms.Button
            Me.cmdClose = New System.Windows.Forms.Button
            Me.cmdDestroy = New System.Windows.Forms.Button
            Me.prgGrid = New System.Windows.Forms.PropertyGrid
            Me.txtLog = New System.Windows.Forms.TextBox
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.tabAdditionalControls = New System.Windows.Forms.TabControl
            Me.tapSelectControl = New System.Windows.Forms.TabPage
            Me.obwControl = New Tools.WindowsT.FormsT.ObjectBrowser
            Me.flpControls = New System.Windows.Forms.FlowLayoutPanel
            Me.cmdSetTop = New System.Windows.Forms.Button
            Me.cmdSetMid = New System.Windows.Forms.Button
            Me.cmdSetBottom = New System.Windows.Forms.Button
            Me.tapControlProperties = New System.Windows.Forms.TabPage
            Me.prgControlProperties = New System.Windows.Forms.PropertyGrid
            Me.flpSelectControl = New System.Windows.Forms.FlowLayoutPanel
            Me.optTop = New System.Windows.Forms.RadioButton
            Me.optMiddle = New System.Windows.Forms.RadioButton
            Me.optBottom = New System.Windows.Forms.RadioButton
            Me.flpCommands.SuspendLayout()
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.tabAdditionalControls.SuspendLayout()
            Me.tapSelectControl.SuspendLayout()
            Me.flpControls.SuspendLayout()
            Me.tapControlProperties.SuspendLayout()
            Me.flpSelectControl.SuspendLayout()
            Me.SuspendLayout()
            '
            'flpCommands
            '
            Me.flpCommands.AutoSize = True
            Me.flpCommands.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpCommands.Controls.Add(Me.cmdCreate)
            Me.flpCommands.Controls.Add(Me.optWinForms)
            Me.flpCommands.Controls.Add(Me.optWPF)
            Me.flpCommands.Controls.Add(Me.cmdShowDialog)
            Me.flpCommands.Controls.Add(Me.cmdShow)
            Me.flpCommands.Controls.Add(Me.cmdShowFloating)
            Me.flpCommands.Controls.Add(Me.cmdClose)
            Me.flpCommands.Controls.Add(Me.cmdDestroy)
            Me.flpCommands.Dock = System.Windows.Forms.DockStyle.Top
            Me.flpCommands.Location = New System.Drawing.Point(0, 0)
            Me.flpCommands.Name = "flpCommands"
            Me.flpCommands.Size = New System.Drawing.Size(270, 87)
            Me.flpCommands.TabIndex = 0
            '
            'cmdCreate
            '
            Me.cmdCreate.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdCreate.AutoSize = True
            Me.cmdCreate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdCreate.Location = New System.Drawing.Point(3, 3)
            Me.cmdCreate.Name = "cmdCreate"
            Me.cmdCreate.Size = New System.Drawing.Size(91, 23)
            Me.cmdCreate.TabIndex = 0
            Me.cmdCreate.Text = "Create instance"
            Me.cmdCreate.UseVisualStyleBackColor = True
            '
            'optWinForms
            '
            Me.optWinForms.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.optWinForms.AutoSize = True
            Me.optWinForms.Checked = True
            Me.optWinForms.Location = New System.Drawing.Point(100, 6)
            Me.optWinForms.Name = "optWinForms"
            Me.optWinForms.Size = New System.Drawing.Size(42, 17)
            Me.optWinForms.TabIndex = 6
            Me.optWinForms.TabStop = True
            Me.optWinForms.Text = "WF"
            Me.optWinForms.UseVisualStyleBackColor = True
            '
            'optWPF
            '
            Me.optWPF.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.optWPF.AutoSize = True
            Me.optWPF.Location = New System.Drawing.Point(148, 6)
            Me.optWPF.Name = "optWPF"
            Me.optWPF.Size = New System.Drawing.Size(49, 17)
            Me.optWPF.TabIndex = 7
            Me.optWPF.Text = "WPF"
            Me.optWPF.UseVisualStyleBackColor = True
            '
            'cmdShowDialog
            '
            Me.cmdShowDialog.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdShowDialog.AutoSize = True
            Me.cmdShowDialog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdShowDialog.Enabled = False
            Me.cmdShowDialog.Location = New System.Drawing.Point(3, 32)
            Me.cmdShowDialog.Name = "cmdShowDialog"
            Me.cmdShowDialog.Size = New System.Drawing.Size(75, 23)
            Me.cmdShowDialog.TabIndex = 1
            Me.cmdShowDialog.Text = "Show dialog"
            Me.cmdShowDialog.UseVisualStyleBackColor = True
            '
            'cmdShow
            '
            Me.cmdShow.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdShow.AutoSize = True
            Me.cmdShow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdShow.Enabled = False
            Me.cmdShow.Location = New System.Drawing.Point(84, 32)
            Me.cmdShow.Name = "cmdShow"
            Me.cmdShow.Size = New System.Drawing.Size(44, 23)
            Me.cmdShow.TabIndex = 2
            Me.cmdShow.Text = "Show"
            Me.cmdShow.UseVisualStyleBackColor = True
            '
            'cmdShowFloating
            '
            Me.cmdShowFloating.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdShowFloating.AutoSize = True
            Me.cmdShowFloating.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdShowFloating.Enabled = False
            Me.cmdShowFloating.Location = New System.Drawing.Point(134, 32)
            Me.cmdShowFloating.Name = "cmdShowFloating"
            Me.cmdShowFloating.Size = New System.Drawing.Size(81, 23)
            Me.cmdShowFloating.TabIndex = 3
            Me.cmdShowFloating.Text = "Show floating"
            Me.cmdShowFloating.UseVisualStyleBackColor = True
            '
            'cmdClose
            '
            Me.cmdClose.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdClose.AutoSize = True
            Me.cmdClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdClose.Enabled = False
            Me.cmdClose.Location = New System.Drawing.Point(221, 32)
            Me.cmdClose.Name = "cmdClose"
            Me.cmdClose.Size = New System.Drawing.Size(43, 23)
            Me.cmdClose.TabIndex = 4
            Me.cmdClose.Text = "Close"
            Me.cmdClose.UseVisualStyleBackColor = True
            '
            'cmdDestroy
            '
            Me.cmdDestroy.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdDestroy.AutoSize = True
            Me.cmdDestroy.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdDestroy.Enabled = False
            Me.cmdDestroy.Location = New System.Drawing.Point(3, 61)
            Me.cmdDestroy.Name = "cmdDestroy"
            Me.cmdDestroy.Size = New System.Drawing.Size(53, 23)
            Me.cmdDestroy.TabIndex = 5
            Me.cmdDestroy.Text = "Destroy"
            Me.cmdDestroy.UseVisualStyleBackColor = True
            '
            'prgGrid
            '
            Me.prgGrid.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgGrid.Location = New System.Drawing.Point(0, 87)
            Me.prgGrid.Name = "prgGrid"
            Me.prgGrid.Size = New System.Drawing.Size(270, 367)
            Me.prgGrid.TabIndex = 1
            '
            'txtLog
            '
            Me.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.txtLog.Location = New System.Drawing.Point(0, 454)
            Me.txtLog.Multiline = True
            Me.txtLog.Name = "txtLog"
            Me.txtLog.ReadOnly = True
            Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtLog.Size = New System.Drawing.Size(270, 139)
            Me.txtLog.TabIndex = 6
            Me.txtLog.WordWrap = False
            '
            'splMain
            '
            Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splMain.Location = New System.Drawing.Point(0, 0)
            Me.splMain.Name = "splMain"
            '
            'splMain.Panel1
            '
            Me.splMain.Panel1.Controls.Add(Me.prgGrid)
            Me.splMain.Panel1.Controls.Add(Me.flpCommands)
            Me.splMain.Panel1.Controls.Add(Me.txtLog)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.tabAdditionalControls)
            Me.splMain.Panel2.Enabled = False
            Me.splMain.Size = New System.Drawing.Size(911, 593)
            Me.splMain.SplitterDistance = 270
            Me.splMain.TabIndex = 7
            '
            'tabAdditionalControls
            '
            Me.tabAdditionalControls.Controls.Add(Me.tapSelectControl)
            Me.tabAdditionalControls.Controls.Add(Me.tapControlProperties)
            Me.tabAdditionalControls.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tabAdditionalControls.Location = New System.Drawing.Point(0, 0)
            Me.tabAdditionalControls.Name = "tabAdditionalControls"
            Me.tabAdditionalControls.SelectedIndex = 0
            Me.tabAdditionalControls.Size = New System.Drawing.Size(637, 593)
            Me.tabAdditionalControls.TabIndex = 9
            '
            'tapSelectControl
            '
            Me.tapSelectControl.Controls.Add(Me.obwControl)
            Me.tapSelectControl.Controls.Add(Me.flpControls)
            Me.tapSelectControl.Location = New System.Drawing.Point(4, 22)
            Me.tapSelectControl.Name = "tapSelectControl"
            Me.tapSelectControl.Padding = New System.Windows.Forms.Padding(3)
            Me.tapSelectControl.Size = New System.Drawing.Size(629, 567)
            Me.tapSelectControl.TabIndex = 0
            Me.tapSelectControl.Text = "Select control"
            Me.tapSelectControl.UseVisualStyleBackColor = True
            '
            'obwControl
            '
            Me.obwControl.DisplayDescription = False
            Me.obwControl.DisplayMemberList = False
            Me.obwControl.DisplayProperties = False
            Me.obwControl.Dock = System.Windows.Forms.DockStyle.Fill
            Me.obwControl.Location = New System.Drawing.Point(3, 3)
            Me.obwControl.Name = "obwControl"
            Me.obwControl.ShowCTors = False
            Me.obwControl.ShowEvents = False
            Me.obwControl.ShowFields = False
            Me.obwControl.ShowFlatNamespaces = True
            Me.obwControl.ShowGenericArguments = False
            Me.obwControl.ShowGlobalMembers = False
            Me.obwControl.ShowInheritedMembers = True
            Me.obwControl.ShowInitializers = False
            Me.obwControl.ShowInstanceMembers = False
            Me.obwControl.ShowInternalMembers = False
            Me.obwControl.ShowMethods = False
            Me.obwControl.ShowPrivateMembers = False
            Me.obwControl.ShowProperties = False
            Me.obwControl.ShowProtectedMembers = False
            Me.obwControl.ShowReferences = False
            Me.obwControl.ShowShowMenu = False
            Me.obwControl.ShowSpecialMembers = False
            Me.obwControl.ShowStaticMembers = False
            Me.obwControl.Size = New System.Drawing.Size(623, 532)
            Me.obwControl.TabIndex = 7
            '
            'flpControls
            '
            Me.flpControls.AutoSize = True
            Me.flpControls.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpControls.Controls.Add(Me.cmdSetTop)
            Me.flpControls.Controls.Add(Me.cmdSetMid)
            Me.flpControls.Controls.Add(Me.cmdSetBottom)
            Me.flpControls.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.flpControls.Location = New System.Drawing.Point(3, 535)
            Me.flpControls.Name = "flpControls"
            Me.flpControls.Size = New System.Drawing.Size(623, 29)
            Me.flpControls.TabIndex = 8
            '
            'cmdSetTop
            '
            Me.cmdSetTop.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdSetTop.AutoSize = True
            Me.cmdSetTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdSetTop.Location = New System.Drawing.Point(3, 3)
            Me.cmdSetTop.Name = "cmdSetTop"
            Me.cmdSetTop.Size = New System.Drawing.Size(36, 23)
            Me.cmdSetTop.TabIndex = 0
            Me.cmdSetTop.Text = "Top"
            Me.cmdSetTop.UseVisualStyleBackColor = True
            '
            'cmdSetMid
            '
            Me.cmdSetMid.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdSetMid.AutoSize = True
            Me.cmdSetMid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdSetMid.Location = New System.Drawing.Point(45, 3)
            Me.cmdSetMid.Name = "cmdSetMid"
            Me.cmdSetMid.Size = New System.Drawing.Size(48, 23)
            Me.cmdSetMid.TabIndex = 1
            Me.cmdSetMid.Text = "Middle"
            Me.cmdSetMid.UseVisualStyleBackColor = True
            '
            'cmdSetBottom
            '
            Me.cmdSetBottom.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdSetBottom.AutoSize = True
            Me.cmdSetBottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdSetBottom.Location = New System.Drawing.Point(99, 3)
            Me.cmdSetBottom.Name = "cmdSetBottom"
            Me.cmdSetBottom.Size = New System.Drawing.Size(50, 23)
            Me.cmdSetBottom.TabIndex = 2
            Me.cmdSetBottom.Text = "Bottom"
            Me.cmdSetBottom.UseVisualStyleBackColor = True
            '
            'tapControlProperties
            '
            Me.tapControlProperties.Controls.Add(Me.prgControlProperties)
            Me.tapControlProperties.Controls.Add(Me.flpSelectControl)
            Me.tapControlProperties.Location = New System.Drawing.Point(4, 22)
            Me.tapControlProperties.Name = "tapControlProperties"
            Me.tapControlProperties.Padding = New System.Windows.Forms.Padding(3)
            Me.tapControlProperties.Size = New System.Drawing.Size(629, 567)
            Me.tapControlProperties.TabIndex = 1
            Me.tapControlProperties.Text = "Control properties"
            Me.tapControlProperties.UseVisualStyleBackColor = True
            '
            'prgControlProperties
            '
            Me.prgControlProperties.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgControlProperties.Location = New System.Drawing.Point(3, 26)
            Me.prgControlProperties.Name = "prgControlProperties"
            Me.prgControlProperties.Size = New System.Drawing.Size(623, 538)
            Me.prgControlProperties.TabIndex = 1
            '
            'flpSelectControl
            '
            Me.flpSelectControl.AutoSize = True
            Me.flpSelectControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpSelectControl.Controls.Add(Me.optTop)
            Me.flpSelectControl.Controls.Add(Me.optMiddle)
            Me.flpSelectControl.Controls.Add(Me.optBottom)
            Me.flpSelectControl.Dock = System.Windows.Forms.DockStyle.Top
            Me.flpSelectControl.Location = New System.Drawing.Point(3, 3)
            Me.flpSelectControl.Name = "flpSelectControl"
            Me.flpSelectControl.Size = New System.Drawing.Size(623, 23)
            Me.flpSelectControl.TabIndex = 0
            '
            'optTop
            '
            Me.optTop.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.optTop.AutoSize = True
            Me.optTop.Location = New System.Drawing.Point(3, 3)
            Me.optTop.Name = "optTop"
            Me.optTop.Size = New System.Drawing.Size(44, 17)
            Me.optTop.TabIndex = 0
            Me.optTop.TabStop = True
            Me.optTop.Text = "Top"
            Me.optTop.UseVisualStyleBackColor = True
            '
            'optMiddle
            '
            Me.optMiddle.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.optMiddle.AutoSize = True
            Me.optMiddle.Location = New System.Drawing.Point(53, 3)
            Me.optMiddle.Name = "optMiddle"
            Me.optMiddle.Size = New System.Drawing.Size(56, 17)
            Me.optMiddle.TabIndex = 1
            Me.optMiddle.TabStop = True
            Me.optMiddle.Text = "Middle"
            Me.optMiddle.UseVisualStyleBackColor = True
            '
            'optBottom
            '
            Me.optBottom.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.optBottom.AutoSize = True
            Me.optBottom.Location = New System.Drawing.Point(115, 3)
            Me.optBottom.Name = "optBottom"
            Me.optBottom.Size = New System.Drawing.Size(58, 17)
            Me.optBottom.TabIndex = 2
            Me.optBottom.TabStop = True
            Me.optBottom.Text = "Bottom"
            Me.optBottom.UseVisualStyleBackColor = True
            '
            'frmMessageBox
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(911, 593)
            Me.Controls.Add(Me.splMain)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmMessageBox"
            Me.Text = "Testing Tool.WindowsT.FormsT.MessageBox"
            Me.flpCommands.ResumeLayout(False)
            Me.flpCommands.PerformLayout()
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel1.PerformLayout()
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.ResumeLayout(False)
            Me.tabAdditionalControls.ResumeLayout(False)
            Me.tapSelectControl.ResumeLayout(False)
            Me.tapSelectControl.PerformLayout()
            Me.flpControls.ResumeLayout(False)
            Me.flpControls.PerformLayout()
            Me.tapControlProperties.ResumeLayout(False)
            Me.tapControlProperties.PerformLayout()
            Me.flpSelectControl.ResumeLayout(False)
            Me.flpSelectControl.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents flpCommands As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents cmdCreate As System.Windows.Forms.Button
        Friend WithEvents cmdShowDialog As System.Windows.Forms.Button
        Friend WithEvents cmdShow As System.Windows.Forms.Button
        Friend WithEvents cmdShowFloating As System.Windows.Forms.Button
        Friend WithEvents cmdClose As System.Windows.Forms.Button
        Friend WithEvents cmdDestroy As System.Windows.Forms.Button
        Friend WithEvents prgGrid As System.Windows.Forms.PropertyGrid
        Friend WithEvents txtLog As System.Windows.Forms.TextBox
        Friend WithEvents optWinForms As System.Windows.Forms.RadioButton
        Friend WithEvents optWPF As System.Windows.Forms.RadioButton
        Friend WithEvents splMain As System.Windows.Forms.SplitContainer
        Friend WithEvents obwControl As Tools.WindowsT.FormsT.ObjectBrowser
        Friend WithEvents tabAdditionalControls As System.Windows.Forms.TabControl
        Friend WithEvents tapSelectControl As System.Windows.Forms.TabPage
        Friend WithEvents flpControls As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents cmdSetTop As System.Windows.Forms.Button
        Friend WithEvents cmdSetMid As System.Windows.Forms.Button
        Friend WithEvents cmdSetBottom As System.Windows.Forms.Button
        Friend WithEvents tapControlProperties As System.Windows.Forms.TabPage
        Friend WithEvents flpSelectControl As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents optTop As System.Windows.Forms.RadioButton
        Friend WithEvents optMiddle As System.Windows.Forms.RadioButton
        Friend WithEvents optBottom As System.Windows.Forms.RadioButton
        Friend WithEvents prgControlProperties As System.Windows.Forms.PropertyGrid
    End Class

End Namespace
