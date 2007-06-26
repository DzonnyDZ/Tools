Namespace WindowsT.FormsT
#If Config <= Nightly Then
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ThesaurusForm
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
            Me.splVertical = New System.Windows.Forms.SplitContainer
            Me.splAutoComplete = New System.Windows.Forms.SplitContainer
            Me.fraAutoComplete = New System.Windows.Forms.GroupBox
            Me.kweAutoComplete = New Tools.WindowsT.FormsT.KeyWordsEditor
            Me.fraCache = New System.Windows.Forms.GroupBox
            Me.lblCache = New System.Windows.Forms.Label
            Me.tlpClearCache = New System.Windows.Forms.TableLayoutPanel
            Me.cmdClearCache = New System.Windows.Forms.Button
            Me.fraSynonyms = New System.Windows.Forms.GroupBox
            Me.splSynonyms = New System.Windows.Forms.SplitContainer
            Me.fraKeys = New System.Windows.Forms.GroupBox
            Me.kweKeys = New Tools.WindowsT.FormsT.KeyWordsEditor
            Me.fraValues = New System.Windows.Forms.GroupBox
            Me.kweValues = New Tools.WindowsT.FormsT.KeyWordsEditor
            Me.tlpSelect = New System.Windows.Forms.TableLayoutPanel
            Me.cmdDelSyn = New System.Windows.Forms.Button
            Me.cmdAddSyn = New System.Windows.Forms.Button
            Me.cmbSyn = New System.Windows.Forms.ComboBox
            Me.totToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.tlpButtons = New System.Windows.Forms.TableLayoutPanel
            Me.flpButtons = New System.Windows.Forms.FlowLayoutPanel
            Me.cmdOK = New System.Windows.Forms.Button
            Me.cmdCancel = New System.Windows.Forms.Button
            Me.splVertical.Panel1.SuspendLayout()
            Me.splVertical.Panel2.SuspendLayout()
            Me.splVertical.SuspendLayout()
            Me.splAutoComplete.Panel1.SuspendLayout()
            Me.splAutoComplete.Panel2.SuspendLayout()
            Me.splAutoComplete.SuspendLayout()
            Me.fraAutoComplete.SuspendLayout()
            Me.fraCache.SuspendLayout()
            Me.tlpClearCache.SuspendLayout()
            Me.fraSynonyms.SuspendLayout()
            Me.splSynonyms.Panel1.SuspendLayout()
            Me.splSynonyms.Panel2.SuspendLayout()
            Me.splSynonyms.SuspendLayout()
            Me.fraKeys.SuspendLayout()
            Me.fraValues.SuspendLayout()
            Me.tlpSelect.SuspendLayout()
            Me.tlpButtons.SuspendLayout()
            Me.flpButtons.SuspendLayout()
            Me.SuspendLayout()
            '
            'splVertical
            '
            Me.splVertical.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splVertical.Location = New System.Drawing.Point(0, 0)
            Me.splVertical.Name = "splVertical"
            '
            'splVertical.Panel1
            '
            Me.splVertical.Panel1.Controls.Add(Me.splAutoComplete)
            '
            'splVertical.Panel2
            '
            Me.splVertical.Panel2.Controls.Add(Me.fraSynonyms)
            Me.splVertical.Size = New System.Drawing.Size(651, 399)
            Me.splVertical.SplitterDistance = 322
            Me.splVertical.TabIndex = 0
            '
            'splAutoComplete
            '
            Me.splAutoComplete.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splAutoComplete.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
            Me.splAutoComplete.Location = New System.Drawing.Point(0, 0)
            Me.splAutoComplete.Name = "splAutoComplete"
            Me.splAutoComplete.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'splAutoComplete.Panel1
            '
            Me.splAutoComplete.Panel1.Controls.Add(Me.fraAutoComplete)
            '
            'splAutoComplete.Panel2
            '
            Me.splAutoComplete.Panel2.Controls.Add(Me.fraCache)
            Me.splAutoComplete.Size = New System.Drawing.Size(322, 399)
            Me.splAutoComplete.SplitterDistance = 346
            Me.splAutoComplete.TabIndex = 0
            '
            'fraAutoComplete
            '
            Me.fraAutoComplete.Controls.Add(Me.kweAutoComplete)
            Me.fraAutoComplete.Dock = System.Windows.Forms.DockStyle.Fill
            Me.fraAutoComplete.Location = New System.Drawing.Point(0, 0)
            Me.fraAutoComplete.Margin = New System.Windows.Forms.Padding(0)
            Me.fraAutoComplete.Name = "fraAutoComplete"
            Me.fraAutoComplete.Size = New System.Drawing.Size(322, 346)
            Me.fraAutoComplete.TabIndex = 1
            Me.fraAutoComplete.TabStop = False
            Me.fraAutoComplete.Text = "Autocomplete list"
            '
            'kweAutoComplete
            '
            Me.kweAutoComplete.AutoCompleteStable = Nothing
            Me.kweAutoComplete.Dock = System.Windows.Forms.DockStyle.Fill
            Me.kweAutoComplete.Location = New System.Drawing.Point(3, 16)
            Me.kweAutoComplete.MergeButtonState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweAutoComplete.Name = "kweAutoComplete"
            Me.kweAutoComplete.Size = New System.Drawing.Size(316, 327)
            '
            '
            '
            Me.kweAutoComplete.Status.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
            Me.kweAutoComplete.Status.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.kweAutoComplete.Status.AutoChanged = False
            Me.kweAutoComplete.Status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.kweAutoComplete.Status.DeleteMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweAutoComplete.Status.Enabled = False
            Me.kweAutoComplete.Status.Location = New System.Drawing.Point(269, 1)
            Me.kweAutoComplete.Status.Margin = New System.Windows.Forms.Padding(0)
            Me.kweAutoComplete.Status.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
            Me.kweAutoComplete.Status.Name = "stmStatus"
            Me.kweAutoComplete.Status.Size = New System.Drawing.Size(24, 24)
            Me.kweAutoComplete.Status.TabIndex = 2
            Me.kweAutoComplete.Status.TabStop = False
            Me.kweAutoComplete.Status.Visible = False
            Me.kweAutoComplete.StatusState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweAutoComplete.Synonyms = Nothing
            Me.kweAutoComplete.TabIndex = 0
            Me.kweAutoComplete.ThesaurusButtonState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            '
            'fraCache
            '
            Me.fraCache.Controls.Add(Me.lblCache)
            Me.fraCache.Controls.Add(Me.tlpClearCache)
            Me.fraCache.Dock = System.Windows.Forms.DockStyle.Fill
            Me.fraCache.Location = New System.Drawing.Point(0, 0)
            Me.fraCache.Margin = New System.Windows.Forms.Padding(0)
            Me.fraCache.Name = "fraCache"
            Me.fraCache.Size = New System.Drawing.Size(322, 49)
            Me.fraCache.TabIndex = 1
            Me.fraCache.TabStop = False
            Me.fraCache.Text = "Autocomplete chache"
            '
            'lblCache
            '
            Me.lblCache.AutoEllipsis = True
            Me.lblCache.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lblCache.Location = New System.Drawing.Point(3, 16)
            Me.lblCache.Name = "lblCache"
            Me.lblCache.Size = New System.Drawing.Size(292, 30)
            Me.lblCache.TabIndex = 0
            '
            'tlpClearCache
            '
            Me.tlpClearCache.AutoSize = True
            Me.tlpClearCache.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpClearCache.ColumnCount = 1
            Me.tlpClearCache.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpClearCache.Controls.Add(Me.cmdClearCache, 0, 0)
            Me.tlpClearCache.Dock = System.Windows.Forms.DockStyle.Right
            Me.tlpClearCache.Location = New System.Drawing.Point(295, 16)
            Me.tlpClearCache.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpClearCache.Name = "tlpClearCache"
            Me.tlpClearCache.RowCount = 1
            Me.tlpClearCache.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpClearCache.Size = New System.Drawing.Size(24, 30)
            Me.tlpClearCache.TabIndex = 2
            '
            'cmdClearCache
            '
            Me.cmdClearCache.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.cmdClearCache.AutoSize = True
            Me.cmdClearCache.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdClearCache.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cmdClearCache.Image = Global.Tools.My.Resources.Resources.DeleteHS
            Me.cmdClearCache.Location = New System.Drawing.Point(0, 3)
            Me.cmdClearCache.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdClearCache.Name = "cmdClearCache"
            Me.cmdClearCache.Size = New System.Drawing.Size(24, 24)
            Me.cmdClearCache.TabIndex = 1
            Me.totToolTip.SetToolTip(Me.cmdClearCache, "Clear cache")
            Me.cmdClearCache.UseVisualStyleBackColor = True
            '
            'fraSynonyms
            '
            Me.fraSynonyms.Controls.Add(Me.splSynonyms)
            Me.fraSynonyms.Controls.Add(Me.tlpSelect)
            Me.fraSynonyms.Dock = System.Windows.Forms.DockStyle.Fill
            Me.fraSynonyms.Location = New System.Drawing.Point(0, 0)
            Me.fraSynonyms.Margin = New System.Windows.Forms.Padding(0)
            Me.fraSynonyms.Name = "fraSynonyms"
            Me.fraSynonyms.Size = New System.Drawing.Size(325, 399)
            Me.fraSynonyms.TabIndex = 0
            Me.fraSynonyms.TabStop = False
            Me.fraSynonyms.Text = "Synonyms"
            '
            'splSynonyms
            '
            Me.splSynonyms.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splSynonyms.Location = New System.Drawing.Point(3, 40)
            Me.splSynonyms.Name = "splSynonyms"
            Me.splSynonyms.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'splSynonyms.Panel1
            '
            Me.splSynonyms.Panel1.Controls.Add(Me.fraKeys)
            '
            'splSynonyms.Panel2
            '
            Me.splSynonyms.Panel2.Controls.Add(Me.fraValues)
            Me.splSynonyms.Size = New System.Drawing.Size(319, 356)
            Me.splSynonyms.SplitterDistance = 168
            Me.splSynonyms.TabIndex = 0
            '
            'fraKeys
            '
            Me.fraKeys.Controls.Add(Me.kweKeys)
            Me.fraKeys.Dock = System.Windows.Forms.DockStyle.Fill
            Me.fraKeys.Enabled = False
            Me.fraKeys.Location = New System.Drawing.Point(0, 0)
            Me.fraKeys.Margin = New System.Windows.Forms.Padding(0)
            Me.fraKeys.Name = "fraKeys"
            Me.fraKeys.Size = New System.Drawing.Size(319, 168)
            Me.fraKeys.TabIndex = 0
            Me.fraKeys.TabStop = False
            Me.fraKeys.Text = "Keys"
            '
            'kweKeys
            '
            Me.kweKeys.AutoCompleteStable = Nothing
            Me.kweKeys.Dock = System.Windows.Forms.DockStyle.Fill
            Me.kweKeys.Location = New System.Drawing.Point(3, 16)
            Me.kweKeys.MergeButtonState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweKeys.Name = "kweKeys"
            Me.kweKeys.Size = New System.Drawing.Size(313, 149)
            '
            '
            '
            Me.kweKeys.Status.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
            Me.kweKeys.Status.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.kweKeys.Status.AutoChanged = False
            Me.kweKeys.Status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.kweKeys.Status.DeleteMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweKeys.Status.Enabled = False
            Me.kweKeys.Status.Location = New System.Drawing.Point(266, 1)
            Me.kweKeys.Status.Margin = New System.Windows.Forms.Padding(0)
            Me.kweKeys.Status.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
            Me.kweKeys.Status.Name = "stmStatus"
            Me.kweKeys.Status.Size = New System.Drawing.Size(24, 24)
            Me.kweKeys.Status.TabIndex = 2
            Me.kweKeys.Status.TabStop = False
            Me.kweKeys.Status.Visible = False
            Me.kweKeys.StatusState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweKeys.Synonyms = Nothing
            Me.kweKeys.TabIndex = 0
            Me.kweKeys.ThesaurusButtonState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            '
            'fraValues
            '
            Me.fraValues.Controls.Add(Me.kweValues)
            Me.fraValues.Dock = System.Windows.Forms.DockStyle.Fill
            Me.fraValues.Enabled = False
            Me.fraValues.Location = New System.Drawing.Point(0, 0)
            Me.fraValues.Margin = New System.Windows.Forms.Padding(0)
            Me.fraValues.Name = "fraValues"
            Me.fraValues.Size = New System.Drawing.Size(319, 184)
            Me.fraValues.TabIndex = 0
            Me.fraValues.TabStop = False
            Me.fraValues.Text = "Values"
            '
            'kweValues
            '
            Me.kweValues.AutoCompleteStable = Nothing
            Me.kweValues.Dock = System.Windows.Forms.DockStyle.Fill
            Me.kweValues.Location = New System.Drawing.Point(3, 16)
            Me.kweValues.MergeButtonState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweValues.Name = "kweValues"
            Me.kweValues.Size = New System.Drawing.Size(313, 165)
            '
            '
            '
            Me.kweValues.Status.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
            Me.kweValues.Status.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.kweValues.Status.AutoChanged = False
            Me.kweValues.Status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.kweValues.Status.DeleteMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweValues.Status.Enabled = False
            Me.kweValues.Status.Location = New System.Drawing.Point(266, 1)
            Me.kweValues.Status.Margin = New System.Windows.Forms.Padding(0)
            Me.kweValues.Status.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
            Me.kweValues.Status.Name = "stmStatus"
            Me.kweValues.Status.Size = New System.Drawing.Size(24, 24)
            Me.kweValues.Status.TabIndex = 2
            Me.kweValues.Status.TabStop = False
            Me.kweValues.Status.Visible = False
            Me.kweValues.StatusState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweValues.Synonyms = Nothing
            Me.kweValues.TabIndex = 0
            Me.kweValues.ThesaurusButtonState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            '
            'tlpSelect
            '
            Me.tlpSelect.AutoSize = True
            Me.tlpSelect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpSelect.ColumnCount = 3
            Me.tlpSelect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpSelect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpSelect.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpSelect.Controls.Add(Me.cmdDelSyn, 2, 0)
            Me.tlpSelect.Controls.Add(Me.cmdAddSyn, 1, 0)
            Me.tlpSelect.Controls.Add(Me.cmbSyn, 0, 0)
            Me.tlpSelect.Dock = System.Windows.Forms.DockStyle.Top
            Me.tlpSelect.Location = New System.Drawing.Point(3, 16)
            Me.tlpSelect.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpSelect.Name = "tlpSelect"
            Me.tlpSelect.RowCount = 1
            Me.tlpSelect.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpSelect.Size = New System.Drawing.Size(319, 24)
            Me.tlpSelect.TabIndex = 1
            '
            'cmdDelSyn
            '
            Me.cmdDelSyn.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdDelSyn.AutoSize = True
            Me.cmdDelSyn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdDelSyn.Enabled = False
            Me.cmdDelSyn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cmdDelSyn.Image = Global.Tools.My.Resources.Resources.DeleteHS
            Me.cmdDelSyn.Location = New System.Drawing.Point(295, 0)
            Me.cmdDelSyn.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdDelSyn.Name = "cmdDelSyn"
            Me.cmdDelSyn.Size = New System.Drawing.Size(24, 24)
            Me.cmdDelSyn.TabIndex = 2
            Me.totToolTip.SetToolTip(Me.cmdDelSyn, "Delete selected group of synonyms")
            Me.cmdDelSyn.UseVisualStyleBackColor = True
            '
            'cmdAddSyn
            '
            Me.cmdAddSyn.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdAddSyn.AutoSize = True
            Me.cmdAddSyn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdAddSyn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cmdAddSyn.Image = Global.Tools.My.Resources.Resources.Plus
            Me.cmdAddSyn.Location = New System.Drawing.Point(271, 0)
            Me.cmdAddSyn.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdAddSyn.Name = "cmdAddSyn"
            Me.cmdAddSyn.Size = New System.Drawing.Size(24, 24)
            Me.cmdAddSyn.TabIndex = 1
            Me.totToolTip.SetToolTip(Me.cmdAddSyn, "Add new group of synonyms")
            Me.cmdAddSyn.UseVisualStyleBackColor = True
            '
            'cmbSyn
            '
            Me.cmbSyn.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmbSyn.FormattingEnabled = True
            Me.cmbSyn.Location = New System.Drawing.Point(0, 1)
            Me.cmbSyn.Margin = New System.Windows.Forms.Padding(0)
            Me.cmbSyn.Name = "cmbSyn"
            Me.cmbSyn.Size = New System.Drawing.Size(271, 21)
            Me.cmbSyn.TabIndex = 0
            '
            'tlpButtons
            '
            Me.tlpButtons.AutoSize = True
            Me.tlpButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpButtons.ColumnCount = 1
            Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpButtons.Controls.Add(Me.flpButtons, 0, 0)
            Me.tlpButtons.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.tlpButtons.Location = New System.Drawing.Point(0, 399)
            Me.tlpButtons.Name = "tlpButtons"
            Me.tlpButtons.RowCount = 1
            Me.tlpButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpButtons.Size = New System.Drawing.Size(651, 29)
            Me.tlpButtons.TabIndex = 1
            '
            'flpButtons
            '
            Me.flpButtons.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.flpButtons.AutoSize = True
            Me.flpButtons.Controls.Add(Me.cmdOK)
            Me.flpButtons.Controls.Add(Me.cmdCancel)
            Me.flpButtons.Location = New System.Drawing.Point(278, 0)
            Me.flpButtons.Margin = New System.Windows.Forms.Padding(0)
            Me.flpButtons.Name = "flpButtons"
            Me.flpButtons.Size = New System.Drawing.Size(94, 29)
            Me.flpButtons.TabIndex = 0
            '
            'cmdOK
            '
            Me.cmdOK.AutoSize = True
            Me.cmdOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.cmdOK.Location = New System.Drawing.Point(3, 3)
            Me.cmdOK.Name = "cmdOK"
            Me.cmdOK.Size = New System.Drawing.Size(32, 23)
            Me.cmdOK.TabIndex = 0
            Me.cmdOK.Text = "&OK"
            Me.cmdOK.UseVisualStyleBackColor = True
            '
            'cmdCancel
            '
            Me.cmdCancel.AutoSize = True
            Me.cmdCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cmdCancel.Location = New System.Drawing.Point(41, 3)
            Me.cmdCancel.Name = "cmdCancel"
            Me.cmdCancel.Size = New System.Drawing.Size(50, 23)
            Me.cmdCancel.TabIndex = 1
            Me.cmdCancel.Text = "&Cancel"
            Me.cmdCancel.UseVisualStyleBackColor = True
            '
            'ThesaurusForm
            '
            Me.AcceptButton = Me.cmdOK
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.cmdCancel
            Me.ClientSize = New System.Drawing.Size(651, 428)
            Me.ControlBox = False
            Me.Controls.Add(Me.splVertical)
            Me.Controls.Add(Me.tlpButtons)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "ThesaurusForm"
            Me.ShowInTaskbar = False
            Me.Text = "Autocomplete list and synonyms for key words editor"
            Me.splVertical.Panel1.ResumeLayout(False)
            Me.splVertical.Panel2.ResumeLayout(False)
            Me.splVertical.ResumeLayout(False)
            Me.splAutoComplete.Panel1.ResumeLayout(False)
            Me.splAutoComplete.Panel2.ResumeLayout(False)
            Me.splAutoComplete.ResumeLayout(False)
            Me.fraAutoComplete.ResumeLayout(False)
            Me.fraCache.ResumeLayout(False)
            Me.fraCache.PerformLayout()
            Me.tlpClearCache.ResumeLayout(False)
            Me.tlpClearCache.PerformLayout()
            Me.fraSynonyms.ResumeLayout(False)
            Me.fraSynonyms.PerformLayout()
            Me.splSynonyms.Panel1.ResumeLayout(False)
            Me.splSynonyms.Panel2.ResumeLayout(False)
            Me.splSynonyms.ResumeLayout(False)
            Me.fraKeys.ResumeLayout(False)
            Me.fraValues.ResumeLayout(False)
            Me.tlpSelect.ResumeLayout(False)
            Me.tlpSelect.PerformLayout()
            Me.tlpButtons.ResumeLayout(False)
            Me.tlpButtons.PerformLayout()
            Me.flpButtons.ResumeLayout(False)
            Me.flpButtons.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents splVertical As System.Windows.Forms.SplitContainer
        Friend WithEvents splAutoComplete As System.Windows.Forms.SplitContainer
        Friend WithEvents lblCache As System.Windows.Forms.Label
        Friend WithEvents kweAutoComplete As Tools.WindowsT.FormsT.KeyWordsEditor
        Friend WithEvents fraAutoComplete As System.Windows.Forms.GroupBox
        Friend WithEvents fraCache As System.Windows.Forms.GroupBox
        Friend WithEvents fraSynonyms As System.Windows.Forms.GroupBox
        Friend WithEvents splSynonyms As System.Windows.Forms.SplitContainer
        Friend WithEvents cmbSyn As System.Windows.Forms.ComboBox
        Friend WithEvents cmdAddSyn As System.Windows.Forms.Button
        Friend WithEvents cmdDelSyn As System.Windows.Forms.Button
        Friend WithEvents fraKeys As System.Windows.Forms.GroupBox
        Friend WithEvents fraValues As System.Windows.Forms.GroupBox
        Friend WithEvents tlpSelect As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents kweKeys As Tools.WindowsT.FormsT.KeyWordsEditor
        Friend WithEvents kweValues As Tools.WindowsT.FormsT.KeyWordsEditor
        Friend WithEvents totToolTip As System.Windows.Forms.ToolTip
        Friend WithEvents tlpClearCache As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents cmdClearCache As System.Windows.Forms.Button
        Friend WithEvents tlpButtons As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents flpButtons As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents cmdOK As System.Windows.Forms.Button
        Friend WithEvents cmdCancel As System.Windows.Forms.Button
    End Class
#End If
End Namespace
