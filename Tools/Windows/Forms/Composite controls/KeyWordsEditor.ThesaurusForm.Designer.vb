Namespace WindowsT.FormsT
#If Config <= Alpha Then
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ThesaurusForm))
            Me.splVertical = New System.Windows.Forms.SplitContainer
            Me.splAutoComplete = New System.Windows.Forms.SplitContainer
            Me.fraAutoComplete = New System.Windows.Forms.GroupBox
            Me.kweAutoComplete = New Tools.WindowsT.FormsT.KeyWordsEditor
            Me.fraCache = New System.Windows.Forms.GroupBox
            Me.kweCache = New Tools.WindowsT.FormsT.KeyWordsEditor
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
            Me.cmdOpen = New System.Windows.Forms.Button
            Me.cmdSave = New System.Windows.Forms.Button
            Me.tlpButtons = New System.Windows.Forms.TableLayoutPanel
            Me.flpButtons = New System.Windows.Forms.FlowLayoutPanel
            Me.cmdOK = New System.Windows.Forms.Button
            Me.cmdCancel = New System.Windows.Forms.Button
            Me.sfdSave = New System.Windows.Forms.SaveFileDialog
            Me.ofdLoad = New System.Windows.Forms.OpenFileDialog
            Me.splVertical.Panel1.SuspendLayout()
            Me.splVertical.Panel2.SuspendLayout()
            Me.splVertical.SuspendLayout()
            Me.splAutoComplete.Panel1.SuspendLayout()
            Me.splAutoComplete.Panel2.SuspendLayout()
            Me.splAutoComplete.SuspendLayout()
            Me.fraAutoComplete.SuspendLayout()
            Me.fraCache.SuspendLayout()
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
            Me.splVertical.AccessibleDescription = Nothing
            Me.splVertical.AccessibleName = Nothing
            resources.ApplyResources(Me.splVertical, "splVertical")
            Me.splVertical.BackgroundImage = Nothing
            Me.splVertical.Font = Nothing
            Me.splVertical.Name = "splVertical"
            '
            'splVertical.Panel1
            '
            Me.splVertical.Panel1.AccessibleDescription = Nothing
            Me.splVertical.Panel1.AccessibleName = Nothing
            resources.ApplyResources(Me.splVertical.Panel1, "splVertical.Panel1")
            Me.splVertical.Panel1.BackgroundImage = Nothing
            Me.splVertical.Panel1.Controls.Add(Me.splAutoComplete)
            Me.splVertical.Panel1.Font = Nothing
            Me.totToolTip.SetToolTip(Me.splVertical.Panel1, resources.GetString("splVertical.Panel1.ToolTip"))
            '
            'splVertical.Panel2
            '
            Me.splVertical.Panel2.AccessibleDescription = Nothing
            Me.splVertical.Panel2.AccessibleName = Nothing
            resources.ApplyResources(Me.splVertical.Panel2, "splVertical.Panel2")
            Me.splVertical.Panel2.BackgroundImage = Nothing
            Me.splVertical.Panel2.Controls.Add(Me.fraSynonyms)
            Me.splVertical.Panel2.Font = Nothing
            Me.totToolTip.SetToolTip(Me.splVertical.Panel2, resources.GetString("splVertical.Panel2.ToolTip"))
            Me.totToolTip.SetToolTip(Me.splVertical, resources.GetString("splVertical.ToolTip"))
            '
            'splAutoComplete
            '
            Me.splAutoComplete.AccessibleDescription = Nothing
            Me.splAutoComplete.AccessibleName = Nothing
            resources.ApplyResources(Me.splAutoComplete, "splAutoComplete")
            Me.splAutoComplete.BackgroundImage = Nothing
            Me.splAutoComplete.Font = Nothing
            Me.splAutoComplete.Name = "splAutoComplete"
            '
            'splAutoComplete.Panel1
            '
            Me.splAutoComplete.Panel1.AccessibleDescription = Nothing
            Me.splAutoComplete.Panel1.AccessibleName = Nothing
            resources.ApplyResources(Me.splAutoComplete.Panel1, "splAutoComplete.Panel1")
            Me.splAutoComplete.Panel1.BackgroundImage = Nothing
            Me.splAutoComplete.Panel1.Controls.Add(Me.fraAutoComplete)
            Me.splAutoComplete.Panel1.Font = Nothing
            Me.totToolTip.SetToolTip(Me.splAutoComplete.Panel1, resources.GetString("splAutoComplete.Panel1.ToolTip"))
            '
            'splAutoComplete.Panel2
            '
            Me.splAutoComplete.Panel2.AccessibleDescription = Nothing
            Me.splAutoComplete.Panel2.AccessibleName = Nothing
            resources.ApplyResources(Me.splAutoComplete.Panel2, "splAutoComplete.Panel2")
            Me.splAutoComplete.Panel2.BackgroundImage = Nothing
            Me.splAutoComplete.Panel2.Controls.Add(Me.fraCache)
            Me.splAutoComplete.Panel2.Font = Nothing
            Me.totToolTip.SetToolTip(Me.splAutoComplete.Panel2, resources.GetString("splAutoComplete.Panel2.ToolTip"))
            Me.totToolTip.SetToolTip(Me.splAutoComplete, resources.GetString("splAutoComplete.ToolTip"))
            '
            'fraAutoComplete
            '
            Me.fraAutoComplete.AccessibleDescription = Nothing
            Me.fraAutoComplete.AccessibleName = Nothing
            resources.ApplyResources(Me.fraAutoComplete, "fraAutoComplete")
            Me.fraAutoComplete.BackgroundImage = Nothing
            Me.fraAutoComplete.Controls.Add(Me.kweAutoComplete)
            Me.fraAutoComplete.Font = Nothing
            Me.fraAutoComplete.Name = "fraAutoComplete"
            Me.fraAutoComplete.TabStop = False
            Me.totToolTip.SetToolTip(Me.fraAutoComplete, resources.GetString("fraAutoComplete.ToolTip"))
            '
            'kweAutoComplete
            '
            Me.kweAutoComplete.AccessibleDescription = Nothing
            Me.kweAutoComplete.AccessibleName = Nothing
            resources.ApplyResources(Me.kweAutoComplete, "kweAutoComplete")
            Me.kweAutoComplete.AutomaticsLists_Designer = True
            Me.kweAutoComplete.BackgroundImage = Nothing
            Me.kweAutoComplete.Font = Nothing
            Me.kweAutoComplete.MergeButtonState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweAutoComplete.Name = "kweAutoComplete"
            Me.kweAutoComplete.StatusState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweAutoComplete.ThesaurusButtonState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.totToolTip.SetToolTip(Me.kweAutoComplete, resources.GetString("kweAutoComplete.ToolTip"))
            '
            'fraCache
            '
            Me.fraCache.AccessibleDescription = Nothing
            Me.fraCache.AccessibleName = Nothing
            resources.ApplyResources(Me.fraCache, "fraCache")
            Me.fraCache.BackgroundImage = Nothing
            Me.fraCache.Controls.Add(Me.kweCache)
            Me.fraCache.Font = Nothing
            Me.fraCache.Name = "fraCache"
            Me.fraCache.TabStop = False
            Me.totToolTip.SetToolTip(Me.fraCache, resources.GetString("fraCache.ToolTip"))
            '
            'kweCache
            '
            Me.kweCache.AccessibleDescription = Nothing
            Me.kweCache.AccessibleName = Nothing
            resources.ApplyResources(Me.kweCache, "kweCache")
            Me.kweCache.AutomaticsLists_Designer = True
            Me.kweCache.BackgroundImage = Nothing
            Me.kweCache.Font = Nothing
            Me.kweCache.MergeButtonState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweCache.Name = "kweCache"
            Me.kweCache.StatusState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweCache.ThesaurusButtonState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.totToolTip.SetToolTip(Me.kweCache, resources.GetString("kweCache.ToolTip"))
            '
            'fraSynonyms
            '
            Me.fraSynonyms.AccessibleDescription = Nothing
            Me.fraSynonyms.AccessibleName = Nothing
            resources.ApplyResources(Me.fraSynonyms, "fraSynonyms")
            Me.fraSynonyms.BackgroundImage = Nothing
            Me.fraSynonyms.Controls.Add(Me.splSynonyms)
            Me.fraSynonyms.Controls.Add(Me.tlpSelect)
            Me.fraSynonyms.Font = Nothing
            Me.fraSynonyms.Name = "fraSynonyms"
            Me.fraSynonyms.TabStop = False
            Me.totToolTip.SetToolTip(Me.fraSynonyms, resources.GetString("fraSynonyms.ToolTip"))
            '
            'splSynonyms
            '
            Me.splSynonyms.AccessibleDescription = Nothing
            Me.splSynonyms.AccessibleName = Nothing
            resources.ApplyResources(Me.splSynonyms, "splSynonyms")
            Me.splSynonyms.BackgroundImage = Nothing
            Me.splSynonyms.Font = Nothing
            Me.splSynonyms.Name = "splSynonyms"
            '
            'splSynonyms.Panel1
            '
            Me.splSynonyms.Panel1.AccessibleDescription = Nothing
            Me.splSynonyms.Panel1.AccessibleName = Nothing
            resources.ApplyResources(Me.splSynonyms.Panel1, "splSynonyms.Panel1")
            Me.splSynonyms.Panel1.BackgroundImage = Nothing
            Me.splSynonyms.Panel1.Controls.Add(Me.fraKeys)
            Me.splSynonyms.Panel1.Font = Nothing
            Me.totToolTip.SetToolTip(Me.splSynonyms.Panel1, resources.GetString("splSynonyms.Panel1.ToolTip"))
            '
            'splSynonyms.Panel2
            '
            Me.splSynonyms.Panel2.AccessibleDescription = Nothing
            Me.splSynonyms.Panel2.AccessibleName = Nothing
            resources.ApplyResources(Me.splSynonyms.Panel2, "splSynonyms.Panel2")
            Me.splSynonyms.Panel2.BackgroundImage = Nothing
            Me.splSynonyms.Panel2.Controls.Add(Me.fraValues)
            Me.splSynonyms.Panel2.Font = Nothing
            Me.totToolTip.SetToolTip(Me.splSynonyms.Panel2, resources.GetString("splSynonyms.Panel2.ToolTip"))
            Me.totToolTip.SetToolTip(Me.splSynonyms, resources.GetString("splSynonyms.ToolTip"))
            '
            'fraKeys
            '
            Me.fraKeys.AccessibleDescription = Nothing
            Me.fraKeys.AccessibleName = Nothing
            resources.ApplyResources(Me.fraKeys, "fraKeys")
            Me.fraKeys.BackgroundImage = Nothing
            Me.fraKeys.Controls.Add(Me.kweKeys)
            Me.fraKeys.Font = Nothing
            Me.fraKeys.Name = "fraKeys"
            Me.fraKeys.TabStop = False
            Me.totToolTip.SetToolTip(Me.fraKeys, resources.GetString("fraKeys.ToolTip"))
            '
            'kweKeys
            '
            Me.kweKeys.AccessibleDescription = Nothing
            Me.kweKeys.AccessibleName = Nothing
            resources.ApplyResources(Me.kweKeys, "kweKeys")
            Me.kweKeys.AutomaticsLists_Designer = True
            Me.kweKeys.BackgroundImage = Nothing
            Me.kweKeys.Font = Nothing
            Me.kweKeys.MergeButtonState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweKeys.Name = "kweKeys"
            Me.kweKeys.StatusState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweKeys.ThesaurusButtonState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.totToolTip.SetToolTip(Me.kweKeys, resources.GetString("kweKeys.ToolTip"))
            '
            'fraValues
            '
            Me.fraValues.AccessibleDescription = Nothing
            Me.fraValues.AccessibleName = Nothing
            resources.ApplyResources(Me.fraValues, "fraValues")
            Me.fraValues.BackgroundImage = Nothing
            Me.fraValues.Controls.Add(Me.kweValues)
            Me.fraValues.Font = Nothing
            Me.fraValues.Name = "fraValues"
            Me.fraValues.TabStop = False
            Me.totToolTip.SetToolTip(Me.fraValues, resources.GetString("fraValues.ToolTip"))
            '
            'kweValues
            '
            Me.kweValues.AccessibleDescription = Nothing
            Me.kweValues.AccessibleName = Nothing
            resources.ApplyResources(Me.kweValues, "kweValues")
            Me.kweValues.AutomaticsLists_Designer = True
            Me.kweValues.BackgroundImage = Nothing
            Me.kweValues.Font = Nothing
            Me.kweValues.MergeButtonState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweValues.Name = "kweValues"
            Me.kweValues.StatusState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.kweValues.ThesaurusButtonState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.totToolTip.SetToolTip(Me.kweValues, resources.GetString("kweValues.ToolTip"))
            '
            'tlpSelect
            '
            Me.tlpSelect.AccessibleDescription = Nothing
            Me.tlpSelect.AccessibleName = Nothing
            resources.ApplyResources(Me.tlpSelect, "tlpSelect")
            Me.tlpSelect.BackgroundImage = Nothing
            Me.tlpSelect.Controls.Add(Me.cmdDelSyn, 2, 0)
            Me.tlpSelect.Controls.Add(Me.cmdAddSyn, 1, 0)
            Me.tlpSelect.Controls.Add(Me.cmbSyn, 0, 0)
            Me.tlpSelect.Font = Nothing
            Me.tlpSelect.Name = "tlpSelect"
            Me.totToolTip.SetToolTip(Me.tlpSelect, resources.GetString("tlpSelect.ToolTip"))
            '
            'cmdDelSyn
            '
            Me.cmdDelSyn.AccessibleDescription = Nothing
            Me.cmdDelSyn.AccessibleName = Nothing
            resources.ApplyResources(Me.cmdDelSyn, "cmdDelSyn")
            Me.cmdDelSyn.BackgroundImage = Nothing
            Me.cmdDelSyn.Font = Nothing
            Me.cmdDelSyn.Image = Global.Tools.My.Resources.Resources.Delete
            Me.cmdDelSyn.Name = "cmdDelSyn"
            Me.totToolTip.SetToolTip(Me.cmdDelSyn, resources.GetString("cmdDelSyn.ToolTip"))
            Me.cmdDelSyn.UseVisualStyleBackColor = True
            '
            'cmdAddSyn
            '
            Me.cmdAddSyn.AccessibleDescription = Nothing
            Me.cmdAddSyn.AccessibleName = Nothing
            resources.ApplyResources(Me.cmdAddSyn, "cmdAddSyn")
            Me.cmdAddSyn.BackgroundImage = Nothing
            Me.cmdAddSyn.Font = Nothing
            Me.cmdAddSyn.Image = Global.Tools.My.Resources.Resources.Plus
            Me.cmdAddSyn.Name = "cmdAddSyn"
            Me.totToolTip.SetToolTip(Me.cmdAddSyn, resources.GetString("cmdAddSyn.ToolTip"))
            Me.cmdAddSyn.UseVisualStyleBackColor = True
            '
            'cmbSyn
            '
            Me.cmbSyn.AccessibleDescription = Nothing
            Me.cmbSyn.AccessibleName = Nothing
            resources.ApplyResources(Me.cmbSyn, "cmbSyn")
            Me.cmbSyn.BackgroundImage = Nothing
            Me.cmbSyn.Font = Nothing
            Me.cmbSyn.FormattingEnabled = True
            Me.cmbSyn.Name = "cmbSyn"
            Me.totToolTip.SetToolTip(Me.cmbSyn, resources.GetString("cmbSyn.ToolTip"))
            '
            'cmdOpen
            '
            Me.cmdOpen.AccessibleDescription = Nothing
            Me.cmdOpen.AccessibleName = Nothing
            resources.ApplyResources(Me.cmdOpen, "cmdOpen")
            Me.cmdOpen.BackgroundImage = Nothing
            Me.cmdOpen.FlatAppearance.BorderSize = 0
            Me.cmdOpen.Font = Nothing
            Me.cmdOpen.Image = Global.Tools.My.Resources.Resources.Open
            Me.cmdOpen.Name = "cmdOpen"
            Me.totToolTip.SetToolTip(Me.cmdOpen, resources.GetString("cmdOpen.ToolTip"))
            Me.cmdOpen.UseVisualStyleBackColor = True
            '
            'cmdSave
            '
            Me.cmdSave.AccessibleDescription = Nothing
            Me.cmdSave.AccessibleName = Nothing
            resources.ApplyResources(Me.cmdSave, "cmdSave")
            Me.cmdSave.BackgroundImage = Nothing
            Me.cmdSave.FlatAppearance.BorderSize = 0
            Me.cmdSave.Font = Nothing
            Me.cmdSave.Image = Global.Tools.My.Resources.Resources.Save
            Me.cmdSave.Name = "cmdSave"
            Me.totToolTip.SetToolTip(Me.cmdSave, resources.GetString("cmdSave.ToolTip"))
            Me.cmdSave.UseVisualStyleBackColor = True
            '
            'tlpButtons
            '
            Me.tlpButtons.AccessibleDescription = Nothing
            Me.tlpButtons.AccessibleName = Nothing
            resources.ApplyResources(Me.tlpButtons, "tlpButtons")
            Me.tlpButtons.BackgroundImage = Nothing
            Me.tlpButtons.Controls.Add(Me.flpButtons, 0, 0)
            Me.tlpButtons.Font = Nothing
            Me.tlpButtons.Name = "tlpButtons"
            Me.totToolTip.SetToolTip(Me.tlpButtons, resources.GetString("tlpButtons.ToolTip"))
            '
            'flpButtons
            '
            Me.flpButtons.AccessibleDescription = Nothing
            Me.flpButtons.AccessibleName = Nothing
            resources.ApplyResources(Me.flpButtons, "flpButtons")
            Me.flpButtons.BackgroundImage = Nothing
            Me.flpButtons.Controls.Add(Me.cmdOK)
            Me.flpButtons.Controls.Add(Me.cmdCancel)
            Me.flpButtons.Controls.Add(Me.cmdOpen)
            Me.flpButtons.Controls.Add(Me.cmdSave)
            Me.flpButtons.Font = Nothing
            Me.flpButtons.Name = "flpButtons"
            Me.totToolTip.SetToolTip(Me.flpButtons, resources.GetString("flpButtons.ToolTip"))
            '
            'cmdOK
            '
            Me.cmdOK.AccessibleDescription = Nothing
            Me.cmdOK.AccessibleName = Nothing
            resources.ApplyResources(Me.cmdOK, "cmdOK")
            Me.cmdOK.BackgroundImage = Nothing
            Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.cmdOK.Font = Nothing
            Me.cmdOK.Name = "cmdOK"
            Me.totToolTip.SetToolTip(Me.cmdOK, resources.GetString("cmdOK.ToolTip"))
            Me.cmdOK.UseVisualStyleBackColor = True
            '
            'cmdCancel
            '
            Me.cmdCancel.AccessibleDescription = Nothing
            Me.cmdCancel.AccessibleName = Nothing
            resources.ApplyResources(Me.cmdCancel, "cmdCancel")
            Me.cmdCancel.BackgroundImage = Nothing
            Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cmdCancel.Font = Nothing
            Me.cmdCancel.Name = "cmdCancel"
            Me.totToolTip.SetToolTip(Me.cmdCancel, resources.GetString("cmdCancel.ToolTip"))
            Me.cmdCancel.UseVisualStyleBackColor = True
            '
            'sfdSave
            '
            Me.sfdSave.DefaultExt = "xml"
            resources.ApplyResources(Me.sfdSave, "sfdSave")
            '
            'ofdLoad
            '
            Me.ofdLoad.DefaultExt = "xml"
            resources.ApplyResources(Me.ofdLoad, "ofdLoad")
            '
            'ThesaurusForm
            '
            Me.AccessibleDescription = Nothing
            Me.AccessibleName = Nothing
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Nothing
            Me.CancelButton = Me.cmdCancel
            Me.ControlBox = False
            Me.Controls.Add(Me.splVertical)
            Me.Controls.Add(Me.tlpButtons)
            Me.Font = Nothing
            Me.Icon = Nothing
            Me.KeyPreview = True
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "ThesaurusForm"
            Me.ShowInTaskbar = False
            Me.totToolTip.SetToolTip(Me, resources.GetString("$this.ToolTip"))
            Me.splVertical.Panel1.ResumeLayout(False)
            Me.splVertical.Panel2.ResumeLayout(False)
            Me.splVertical.ResumeLayout(False)
            Me.splAutoComplete.Panel1.ResumeLayout(False)
            Me.splAutoComplete.Panel2.ResumeLayout(False)
            Me.splAutoComplete.ResumeLayout(False)
            Me.fraAutoComplete.ResumeLayout(False)
            Me.fraCache.ResumeLayout(False)
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
        Friend WithEvents tlpButtons As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents flpButtons As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents cmdOK As System.Windows.Forms.Button
        Friend WithEvents cmdCancel As System.Windows.Forms.Button
        Friend WithEvents cmdSave As System.Windows.Forms.Button
        Friend WithEvents cmdOpen As System.Windows.Forms.Button
        Friend WithEvents sfdSave As System.Windows.Forms.SaveFileDialog
        Friend WithEvents ofdLoad As System.Windows.Forms.OpenFileDialog
        Friend WithEvents kweCache As Tools.WindowsT.FormsT.KeyWordsEditor
    End Class
#End If
End Namespace
