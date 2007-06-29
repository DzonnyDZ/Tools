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
            Me.tlpTop.SuspendLayout()
            Me.cmsThesaurus.SuspendLayout()
            Me.SuspendLayout()
            '
            'txtEdit
            '
            Me.txtEdit.AcceptsReturn = True
            Me.txtEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtEdit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.txtEdit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
            Me.txtEdit.Location = New System.Drawing.Point(0, 2)
            Me.txtEdit.Margin = New System.Windows.Forms.Padding(0)
            Me.txtEdit.Name = "txtEdit"
            Me.txtEdit.Size = New System.Drawing.Size(390, 20)
            Me.txtEdit.TabIndex = 0
            '
            'lstKW
            '
            Me.lstKW.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lstKW.FormattingEnabled = True
            Me.lstKW.Location = New System.Drawing.Point(0, 24)
            Me.lstKW.Name = "lstKW"
            Me.lstKW.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
            Me.lstKW.Size = New System.Drawing.Size(485, 238)
            Me.lstKW.TabIndex = 1
            '
            'tlpTop
            '
            Me.tlpTop.AutoSize = True
            Me.tlpTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpTop.ColumnCount = 5
            Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpTop.Controls.Add(Me.txtEdit, 0, 0)
            Me.tlpTop.Controls.Add(Me.stmStatus, 2, 0)
            Me.tlpTop.Controls.Add(Me.cmdThesaurus, 3, 0)
            Me.tlpTop.Controls.Add(Me.cmdMerge, 4, 0)
            Me.tlpTop.Controls.Add(Me.cmdAdd, 1, 0)
            Me.tlpTop.Dock = System.Windows.Forms.DockStyle.Top
            Me.tlpTop.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns
            Me.tlpTop.Location = New System.Drawing.Point(0, 0)
            Me.tlpTop.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpTop.Name = "tlpTop"
            Me.tlpTop.RowCount = 1
            Me.tlpTop.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpTop.Size = New System.Drawing.Size(485, 24)
            Me.tlpTop.TabIndex = 1
            '
            'stmStatus
            '
            Me.stmStatus.AddMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
            Me.stmStatus.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.stmStatus.AutoChanged = False
            Me.stmStatus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.stmStatus.DeleteMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Hidden
            Me.stmStatus.Location = New System.Drawing.Point(414, 0)
            Me.stmStatus.Margin = New System.Windows.Forms.Padding(0)
            Me.stmStatus.MarkAsChangedMenuState = Tools.WindowsT.FormsT.UtilitiesT.ControlState.Enabled
            Me.stmStatus.Name = "stmStatus"
            Me.stmStatus.Size = New System.Drawing.Size(24, 24)
            Me.stmStatus.TabIndex = 2
            Me.stmStatus.TabStop = False
            '
            'cmdThesaurus
            '
            Me.cmdThesaurus.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdThesaurus.AutoSize = True
            Me.cmdThesaurus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdThesaurus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cmdThesaurus.Image = Global.Tools.My.Resources.Resources.T
            Me.cmdThesaurus.Location = New System.Drawing.Point(438, 0)
            Me.cmdThesaurus.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdThesaurus.Name = "cmdThesaurus"
            Me.cmdThesaurus.Size = New System.Drawing.Size(23, 24)
            Me.cmdThesaurus.TabIndex = 3
            Me.cmdThesaurus.TabStop = False
            Me.totToolTip.SetToolTip(Me.cmdThesaurus, "Thesaurus")
            Me.cmdThesaurus.UseVisualStyleBackColor = True
            '
            'cmdMerge
            '
            Me.cmdMerge.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdMerge.AutoSize = True
            Me.cmdMerge.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdMerge.BackColor = System.Drawing.Color.Orange
            Me.cmdMerge.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cmdMerge.Image = Global.Tools.My.Resources.Resources.Zip
            Me.cmdMerge.Location = New System.Drawing.Point(461, 0)
            Me.cmdMerge.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdMerge.Name = "cmdMerge"
            Me.cmdMerge.Size = New System.Drawing.Size(24, 24)
            Me.cmdMerge.TabIndex = 4
            Me.cmdMerge.TabStop = False
            Me.totToolTip.SetToolTip(Me.cmdMerge, "Merge enabled for multiple items")
            Me.cmdMerge.UseVisualStyleBackColor = False
            '
            'cmdAdd
            '
            Me.cmdAdd.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdAdd.AutoSize = True
            Me.cmdAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.cmdAdd.Image = Global.Tools.My.Resources.Resources.Plus
            Me.cmdAdd.Location = New System.Drawing.Point(390, 0)
            Me.cmdAdd.Margin = New System.Windows.Forms.Padding(0)
            Me.cmdAdd.Name = "cmdAdd"
            Me.cmdAdd.Size = New System.Drawing.Size(24, 24)
            Me.cmdAdd.TabIndex = 1
            Me.totToolTip.SetToolTip(Me.cmdAdd, "Add word (Enter)")
            Me.cmdAdd.UseVisualStyleBackColor = True
            '
            'cmsThesaurus
            '
            Me.cmsThesaurus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiLabel, Me.tmiAddSelected, Me.tmiRemoveSelected, Me.tmiClearCache, Me.tmiManage, Me.tmiSynonyms})
            Me.cmsThesaurus.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table
            Me.cmsThesaurus.Name = "cmsThesaurus"
            Me.cmsThesaurus.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
            Me.cmsThesaurus.Size = New System.Drawing.Size(206, 136)
            '
            'tmiLabel
            '
            Me.tmiLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar
            Me.tmiLabel.BackColor = System.Drawing.SystemColors.ActiveCaption
            Me.tmiLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
            Me.tmiLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
            Me.tmiLabel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
            Me.tmiLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
            Me.tmiLabel.Name = "tmiLabel"
            Me.tmiLabel.Size = New System.Drawing.Size(205, 22)
            Me.tmiLabel.Text = "Thesaurus menu"
            '
            'tmiAddSelected
            '
            Me.tmiAddSelected.Enabled = False
            Me.tmiAddSelected.Image = Global.Tools.My.Resources.Resources.Plus
            Me.tmiAddSelected.Name = "tmiAddSelected"
            Me.tmiAddSelected.Size = New System.Drawing.Size(205, 22)
            Me.tmiAddSelected.Text = "Add selected keywords"
            Me.tmiAddSelected.Visible = False
            '
            'tmiRemoveSelected
            '
            Me.tmiRemoveSelected.Enabled = False
            Me.tmiRemoveSelected.Image = Global.Tools.My.Resources.Resources.DeleteHS
            Me.tmiRemoveSelected.Name = "tmiRemoveSelected"
            Me.tmiRemoveSelected.Size = New System.Drawing.Size(205, 22)
            Me.tmiRemoveSelected.Text = "Remove selected keywords"
            Me.tmiRemoveSelected.Visible = False
            '
            'tmiClearCache
            '
            Me.tmiClearCache.Enabled = False
            Me.tmiClearCache.Name = "tmiClearCache"
            Me.tmiClearCache.Size = New System.Drawing.Size(205, 22)
            Me.tmiClearCache.Text = "&Clear cache"
            Me.tmiClearCache.Visible = False
            '
            'tmiManage
            '
            Me.tmiManage.Enabled = False
            Me.tmiManage.Name = "tmiManage"
            Me.tmiManage.Size = New System.Drawing.Size(205, 22)
            Me.tmiManage.Text = "&Manage..."
            Me.tmiManage.Visible = False
            '
            'tmiSynonyms
            '
            Me.tmiSynonyms.Enabled = False
            Me.tmiSynonyms.Image = Global.Tools.My.Resources.Resources.T
            Me.tmiSynonyms.Name = "tmiSynonyms"
            Me.tmiSynonyms.Size = New System.Drawing.Size(205, 22)
            Me.tmiSynonyms.Text = "&Synonyms"
            Me.tmiSynonyms.ToolTipText = "Adds new group of synonyms with all selected words as keys"
            Me.tmiSynonyms.Visible = False
            '
            'KeyWordsEditor
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.lstKW)
            Me.Controls.Add(Me.tlpTop)
            Me.Name = "KeyWordsEditor"
            Me.Size = New System.Drawing.Size(485, 264)
            Me.tlpTop.ResumeLayout(False)
            Me.tlpTop.PerformLayout()
            Me.cmsThesaurus.ResumeLayout(False)
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

    End Class
End Namespace