Namespace WindowsT.FormsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class KeyWordsEditor
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
            Me.components = New System.ComponentModel.Container
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(KeyWordsEditor))
            Me.txtEdit = New System.Windows.Forms.TextBox
            Me.lstKW = New System.Windows.Forms.ListBox
            Me.tlpTop = New System.Windows.Forms.TableLayoutPanel
            Me.stmStatus = New Tools.WindowsT.FormsT.StatusMarker
            Me.cmdThesaurus = New System.Windows.Forms.Button
            Me.cmdMerge = New System.Windows.Forms.Button
            Me.cmdAdd = New System.Windows.Forms.Button
            Me.cmsThesaurus = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.tmiLabel = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiAddSelected = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiRemoveSelected = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiClearCache = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiManage = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiSynonyms = New System.Windows.Forms.ToolStripMenuItem
            Me.totToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.cmsContext = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.tmiCut = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiCopy = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiPaste = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiDelete = New System.Windows.Forms.ToolStripMenuItem
            Me.tlpTop.SuspendLayout()
            Me.cmsThesaurus.SuspendLayout()
            Me.cmsContext.SuspendLayout()
            Me.SuspendLayout()
            '
            'txtEdit
            '
            Me.txtEdit.AcceptsReturn = True
            resources.ApplyResources(Me.txtEdit, "txtEdit")
            Me.txtEdit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.txtEdit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
            Me.txtEdit.Name = "txtEdit"
            '
            'lstKW
            '
            resources.ApplyResources(Me.lstKW, "lstKW")
            Me.lstKW.FormattingEnabled = True
            Me.lstKW.Name = "lstKW"
            Me.lstKW.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
            '
            'tlpTop
            '
            resources.ApplyResources(Me.tlpTop, "tlpTop")
            Me.tlpTop.Controls.Add(Me.txtEdit, 0, 0)
            Me.tlpTop.Controls.Add(Me.stmStatus, 2, 0)
            Me.tlpTop.Controls.Add(Me.cmdThesaurus, 3, 0)
            Me.tlpTop.Controls.Add(Me.cmdMerge, 4, 0)
            Me.tlpTop.Controls.Add(Me.cmdAdd, 1, 0)
            Me.tlpTop.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns
            Me.tlpTop.Name = "tlpTop"
            '
            'stmStatus
            '
            Me.stmStatus.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
            resources.ApplyResources(Me.stmStatus, "stmStatus")
            Me.stmStatus.AutoChanged = False
            Me.stmStatus.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
            Me.stmStatus.Name = "stmStatus"
            Me.stmStatus.StatusedControl = Nothing
            Me.stmStatus.TabStop = False
            '
            'cmdThesaurus
            '
            resources.ApplyResources(Me.cmdThesaurus, "cmdThesaurus")
            Me.cmdThesaurus.Image = Global.Tools.My.Resources.Resources.T
            Me.cmdThesaurus.Name = "cmdThesaurus"
            Me.cmdThesaurus.TabStop = False
            Me.totToolTip.SetToolTip(Me.cmdThesaurus, resources.GetString("cmdThesaurus.ToolTip"))
            Me.cmdThesaurus.UseVisualStyleBackColor = True
            '
            'cmdMerge
            '
            resources.ApplyResources(Me.cmdMerge, "cmdMerge")
            Me.cmdMerge.BackColor = System.Drawing.Color.Orange
            Me.cmdMerge.Image = Global.Tools.My.Resources.Resources.Zip
            Me.cmdMerge.Name = "cmdMerge"
            Me.cmdMerge.TabStop = False
            Me.totToolTip.SetToolTip(Me.cmdMerge, resources.GetString("cmdMerge.ToolTip"))
            Me.cmdMerge.UseVisualStyleBackColor = False
            '
            'cmdAdd
            '
            resources.ApplyResources(Me.cmdAdd, "cmdAdd")
            Me.cmdAdd.Image = Global.Tools.My.Resources.Resources.Plus
            Me.cmdAdd.Name = "cmdAdd"
            Me.cmdAdd.TabStop = False
            Me.totToolTip.SetToolTip(Me.cmdAdd, resources.GetString("cmdAdd.ToolTip"))
            Me.cmdAdd.UseVisualStyleBackColor = True
            '
            'cmsThesaurus
            '
            Me.cmsThesaurus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiLabel, Me.tmiAddSelected, Me.tmiRemoveSelected, Me.tmiClearCache, Me.tmiManage, Me.tmiSynonyms})
            Me.cmsThesaurus.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table
            Me.cmsThesaurus.Name = "cmsThesaurus"
            Me.cmsThesaurus.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
            resources.ApplyResources(Me.cmsThesaurus, "cmsThesaurus")
            '
            'tmiLabel
            '
            Me.tmiLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar
            Me.tmiLabel.BackColor = System.Drawing.SystemColors.ActiveCaption
            resources.ApplyResources(Me.tmiLabel, "tmiLabel")
            Me.tmiLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
            Me.tmiLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
            Me.tmiLabel.Name = "tmiLabel"
            '
            'tmiAddSelected
            '
            resources.ApplyResources(Me.tmiAddSelected, "tmiAddSelected")
            Me.tmiAddSelected.Image = Global.Tools.My.Resources.Resources.Plus
            Me.tmiAddSelected.Name = "tmiAddSelected"
            '
            'tmiRemoveSelected
            '
            resources.ApplyResources(Me.tmiRemoveSelected, "tmiRemoveSelected")
            Me.tmiRemoveSelected.Image = Global.Tools.My.Resources.Resources.Delete
            Me.tmiRemoveSelected.Name = "tmiRemoveSelected"
            '
            'tmiClearCache
            '
            resources.ApplyResources(Me.tmiClearCache, "tmiClearCache")
            Me.tmiClearCache.Name = "tmiClearCache"
            '
            'tmiManage
            '
            resources.ApplyResources(Me.tmiManage, "tmiManage")
            Me.tmiManage.Name = "tmiManage"
            '
            'tmiSynonyms
            '
            resources.ApplyResources(Me.tmiSynonyms, "tmiSynonyms")
            Me.tmiSynonyms.Image = Global.Tools.My.Resources.Resources.T
            Me.tmiSynonyms.Name = "tmiSynonyms"
            '
            'cmsContext
            '
            Me.cmsContext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiCut, Me.tmiCopy, Me.tmiPaste, Me.tmiDelete})
            Me.cmsContext.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table
            Me.cmsContext.Name = "cmsThesaurus"
            Me.cmsContext.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
            resources.ApplyResources(Me.cmsContext, "cmsContext")
            '
            'tmiCut
            '
            Me.tmiCut.Image = Global.Tools.My.Resources.Resources.CutIcon
            Me.tmiCut.Name = "tmiCut"
            resources.ApplyResources(Me.tmiCut, "tmiCut")
            '
            'tmiCopy
            '
            Me.tmiCopy.Image = Global.Tools.My.Resources.Resources.CopyIcon
            Me.tmiCopy.Name = "tmiCopy"
            resources.ApplyResources(Me.tmiCopy, "tmiCopy")
            '
            'tmiPaste
            '
            Me.tmiPaste.Image = Global.Tools.My.Resources.Resources.PasteIcon
            Me.tmiPaste.Name = "tmiPaste"
            resources.ApplyResources(Me.tmiPaste, "tmiPaste")
            '
            'tmiDelete
            '
            Me.tmiDelete.Image = Global.Tools.My.Resources.Resources.Delete
            Me.tmiDelete.Name = "tmiDelete"
            resources.ApplyResources(Me.tmiDelete, "tmiDelete")
            '
            'KeyWordsEditor
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.lstKW)
            Me.Controls.Add(Me.tlpTop)
            Me.Name = "KeyWordsEditor"
            Me.tlpTop.ResumeLayout(False)
            Me.tlpTop.PerformLayout()
            Me.cmsThesaurus.ResumeLayout(False)
            Me.cmsContext.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents txtEdit As System.Windows.Forms.TextBox
        Friend WithEvents lstKW As System.Windows.Forms.ListBox
        Friend WithEvents tlpTop As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents stmStatus As Tools.WindowsT.FormsT.StatusMarker
        Friend WithEvents cmdThesaurus As System.Windows.Forms.Button
        Friend WithEvents cmsThesaurus As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents tmiLabel As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiAddSelected As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiRemoveSelected As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiClearCache As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiManage As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents cmdMerge As System.Windows.Forms.Button
        Friend WithEvents cmdAdd As System.Windows.Forms.Button
        Friend WithEvents tmiSynonyms As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents totToolTip As System.Windows.Forms.ToolTip
        Friend WithEvents cmsContext As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents tmiCut As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiCopy As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiPaste As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents tmiDelete As System.Windows.Forms.ToolStripMenuItem

    End Class
End Namespace