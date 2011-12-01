Namespace TextT.UnicodeT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmUcdTranslate
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
            Me.components = New System.ComponentModel.Container()
            Me.tlpMainAndOnly = New System.Windows.Forms.TableLayoutPanel()
            Me.lblInfo = New System.Windows.Forms.Label()
            Me.fraChars = New System.Windows.Forms.GroupBox()
            Me.tlpChars = New System.Windows.Forms.TableLayoutPanel()
            Me.chkChars = New System.Windows.Forms.CheckBox()
            Me.lblCharsI = New System.Windows.Forms.Label()
            Me.lblCharsLanguage = New System.Windows.Forms.Label()
            Me.cmbCharsLanguage = New System.Windows.Forms.ComboBox()
            Me.fraBlocks = New System.Windows.Forms.GroupBox()
            Me.tlpBlocks = New System.Windows.Forms.TableLayoutPanel()
            Me.chkBlocks = New System.Windows.Forms.CheckBox()
            Me.tlpCharsI = New System.Windows.Forms.Label()
            Me.lblTraget = New System.Windows.Forms.Label()
            Me.txtTarget = New System.Windows.Forms.TextBox()
            Me.cmdTargetPathBrowse = New System.Windows.Forms.Button()
            Me.cmdGo = New System.Windows.Forms.Button()
            Me.tlpToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.sfdSaveXml = New System.Windows.Forms.SaveFileDialog()
            Me.lblLanguage = New System.Windows.Forms.Label()
            Me.txtLanguage = New System.Windows.Forms.TextBox()
            Me.chkComments = New System.Windows.Forms.CheckBox()
            Me.lblCheck = New System.Windows.Forms.Label()
            Me.txtCheck = New System.Windows.Forms.TextBox()
            Me.cmdCheckAgainst = New System.Windows.Forms.Button()
            Me.ofdOpenXml = New System.Windows.Forms.OpenFileDialog()
            Me.cmdAddMui = New System.Windows.Forms.Button()
            Me.ofdMui = New System.Windows.Forms.OpenFileDialog()
            Me.tlpMainAndOnly.SuspendLayout()
            Me.fraChars.SuspendLayout()
            Me.tlpChars.SuspendLayout()
            Me.fraBlocks.SuspendLayout()
            Me.tlpBlocks.SuspendLayout()
            Me.SuspendLayout()
            '
            'tlpMainAndOnly
            '
            Me.tlpMainAndOnly.ColumnCount = 5
            Me.tlpMainAndOnly.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.tlpMainAndOnly.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpMainAndOnly.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.tlpMainAndOnly.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpMainAndOnly.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.tlpMainAndOnly.Controls.Add(Me.lblInfo, 0, 0)
            Me.tlpMainAndOnly.Controls.Add(Me.fraChars, 0, 1)
            Me.tlpMainAndOnly.Controls.Add(Me.fraBlocks, 0, 2)
            Me.tlpMainAndOnly.Controls.Add(Me.lblTraget, 0, 3)
            Me.tlpMainAndOnly.Controls.Add(Me.txtTarget, 1, 3)
            Me.tlpMainAndOnly.Controls.Add(Me.cmdTargetPathBrowse, 4, 3)
            Me.tlpMainAndOnly.Controls.Add(Me.lblLanguage, 0, 4)
            Me.tlpMainAndOnly.Controls.Add(Me.txtLanguage, 1, 4)
            Me.tlpMainAndOnly.Controls.Add(Me.chkComments, 0, 5)
            Me.tlpMainAndOnly.Controls.Add(Me.lblCheck, 2, 5)
            Me.tlpMainAndOnly.Controls.Add(Me.txtCheck, 3, 5)
            Me.tlpMainAndOnly.Controls.Add(Me.cmdCheckAgainst, 4, 5)
            Me.tlpMainAndOnly.Controls.Add(Me.cmdGo, 0, 6)
            Me.tlpMainAndOnly.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpMainAndOnly.Location = New System.Drawing.Point(0, 0)
            Me.tlpMainAndOnly.Name = "tlpMainAndOnly"
            Me.tlpMainAndOnly.RowCount = 8
            Me.tlpMainAndOnly.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMainAndOnly.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMainAndOnly.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMainAndOnly.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMainAndOnly.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMainAndOnly.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMainAndOnly.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMainAndOnly.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpMainAndOnly.Size = New System.Drawing.Size(685, 434)
            Me.tlpMainAndOnly.TabIndex = 0
            '
            'lblInfo
            '
            Me.lblInfo.AutoSize = True
            Me.tlpMainAndOnly.SetColumnSpan(Me.lblInfo, 5)
            Me.lblInfo.Location = New System.Drawing.Point(3, 0)
            Me.lblInfo.Name = "lblInfo"
            Me.lblInfo.Size = New System.Drawing.Size(652, 26)
            Me.lblInfo.TabIndex = 0
            Me.lblInfo.Text = "This from generates localization XML file that can be used to localize certain te" & _
                "xts exposed by UnicodeCharacterDatabase class. Namely character and block names." & _
                ""
            '
            'fraChars
            '
            Me.fraChars.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.fraChars.AutoSize = True
            Me.fraChars.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpMainAndOnly.SetColumnSpan(Me.fraChars, 5)
            Me.fraChars.Controls.Add(Me.tlpChars)
            Me.fraChars.Location = New System.Drawing.Point(3, 29)
            Me.fraChars.Name = "fraChars"
            Me.fraChars.Size = New System.Drawing.Size(679, 71)
            Me.fraChars.TabIndex = 1
            Me.fraChars.TabStop = False
            Me.fraChars.Text = "Char names"
            '
            'tlpChars
            '
            Me.tlpChars.AutoSize = True
            Me.tlpChars.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpChars.ColumnCount = 3
            Me.tlpChars.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.tlpChars.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.tlpChars.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpChars.Controls.Add(Me.chkChars, 0, 0)
            Me.tlpChars.Controls.Add(Me.lblCharsI, 1, 0)
            Me.tlpChars.Controls.Add(Me.lblCharsLanguage, 0, 1)
            Me.tlpChars.Controls.Add(Me.cmbCharsLanguage, 1, 1)
            Me.tlpChars.Controls.Add(Me.cmdAddMui, 2, 1)
            Me.tlpChars.Dock = System.Windows.Forms.DockStyle.Top
            Me.tlpChars.Location = New System.Drawing.Point(3, 16)
            Me.tlpChars.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpChars.Name = "tlpChars"
            Me.tlpChars.RowCount = 2
            Me.tlpChars.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpChars.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpChars.Size = New System.Drawing.Size(673, 55)
            Me.tlpChars.TabIndex = 0
            '
            'chkChars
            '
            Me.chkChars.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.chkChars.AutoSize = True
            Me.chkChars.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.chkChars.Location = New System.Drawing.Point(3, 3)
            Me.chkChars.Name = "chkChars"
            Me.chkChars.Size = New System.Drawing.Size(152, 17)
            Me.chkChars.TabIndex = 0
            Me.chkChars.Text = "Generate character names"
            Me.chkChars.UseVisualStyleBackColor = True
            '
            'lblCharsI
            '
            Me.lblCharsI.AutoSize = True
            Me.tlpChars.SetColumnSpan(Me.lblCharsI, 2)
            Me.lblCharsI.Location = New System.Drawing.Point(161, 0)
            Me.lblCharsI.Name = "lblCharsI"
            Me.lblCharsI.Size = New System.Drawing.Size(490, 26)
            Me.lblCharsI.TabIndex = 1
            Me.lblCharsI.Text = "Character names are generated using the Windows Charmap program (specifically get" & _
                "uname.dll). You need to switch Windows to desired language. For this you need Ul" & _
                "timate edition of Windows Vista or 7."
            '
            'lblCharsLanguage
            '
            Me.lblCharsLanguage.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblCharsLanguage.AutoSize = True
            Me.lblCharsLanguage.Enabled = False
            Me.lblCharsLanguage.Location = New System.Drawing.Point(63, 34)
            Me.lblCharsLanguage.Name = "lblCharsLanguage"
            Me.lblCharsLanguage.Size = New System.Drawing.Size(92, 13)
            Me.lblCharsLanguage.TabIndex = 2
            Me.lblCharsLanguage.Text = "Source Langauge"
            '
            'cmbCharsLanguage
            '
            Me.cmbCharsLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbCharsLanguage.Enabled = False
            Me.cmbCharsLanguage.FormattingEnabled = True
            Me.cmbCharsLanguage.Location = New System.Drawing.Point(161, 29)
            Me.cmbCharsLanguage.Name = "cmbCharsLanguage"
            Me.cmbCharsLanguage.Size = New System.Drawing.Size(121, 21)
            Me.cmbCharsLanguage.TabIndex = 3
            '
            'fraBlocks
            '
            Me.fraBlocks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.fraBlocks.AutoSize = True
            Me.fraBlocks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpMainAndOnly.SetColumnSpan(Me.fraBlocks, 5)
            Me.fraBlocks.Controls.Add(Me.tlpBlocks)
            Me.fraBlocks.Location = New System.Drawing.Point(3, 106)
            Me.fraBlocks.Name = "fraBlocks"
            Me.fraBlocks.Size = New System.Drawing.Size(679, 42)
            Me.fraBlocks.TabIndex = 2
            Me.fraBlocks.TabStop = False
            Me.fraBlocks.Text = "Block names"
            '
            'tlpBlocks
            '
            Me.tlpBlocks.AutoSize = True
            Me.tlpBlocks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpBlocks.ColumnCount = 2
            Me.tlpBlocks.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.tlpBlocks.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpBlocks.Controls.Add(Me.chkBlocks, 0, 0)
            Me.tlpBlocks.Controls.Add(Me.tlpCharsI, 1, 0)
            Me.tlpBlocks.Dock = System.Windows.Forms.DockStyle.Top
            Me.tlpBlocks.Location = New System.Drawing.Point(3, 16)
            Me.tlpBlocks.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpBlocks.Name = "tlpBlocks"
            Me.tlpBlocks.RowCount = 2
            Me.tlpBlocks.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpBlocks.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.tlpBlocks.Size = New System.Drawing.Size(673, 23)
            Me.tlpBlocks.TabIndex = 0
            '
            'chkBlocks
            '
            Me.chkBlocks.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.chkBlocks.AutoSize = True
            Me.chkBlocks.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.chkBlocks.Location = New System.Drawing.Point(3, 3)
            Me.chkBlocks.Name = "chkBlocks"
            Me.chkBlocks.Size = New System.Drawing.Size(133, 17)
            Me.chkBlocks.TabIndex = 0
            Me.chkBlocks.Text = "Generate block names"
            Me.chkBlocks.UseVisualStyleBackColor = True
            '
            'tlpCharsI
            '
            Me.tlpCharsI.AutoSize = True
            Me.tlpCharsI.Location = New System.Drawing.Point(142, 0)
            Me.tlpCharsI.Name = "tlpCharsI"
            Me.tlpCharsI.Size = New System.Drawing.Size(366, 13)
            Me.tlpCharsI.TabIndex = 1
            Me.tlpCharsI.Text = "This requires manual work. You willl be given list of block names to translate."
            '
            'lblTraget
            '
            Me.lblTraget.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblTraget.AutoSize = True
            Me.lblTraget.Location = New System.Drawing.Point(4, 159)
            Me.lblTraget.Name = "lblTraget"
            Me.lblTraget.Size = New System.Drawing.Size(54, 13)
            Me.lblTraget.TabIndex = 3
            Me.lblTraget.Text = "Target file"
            '
            'txtTarget
            '
            Me.txtTarget.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtTarget.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.txtTarget.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
            Me.tlpMainAndOnly.SetColumnSpan(Me.txtTarget, 3)
            Me.txtTarget.Location = New System.Drawing.Point(64, 155)
            Me.txtTarget.Name = "txtTarget"
            Me.txtTarget.Size = New System.Drawing.Size(585, 20)
            Me.txtTarget.TabIndex = 4
            '
            'cmdTargetPathBrowse
            '
            Me.cmdTargetPathBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmdTargetPathBrowse.AutoSize = True
            Me.cmdTargetPathBrowse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdTargetPathBrowse.Location = New System.Drawing.Point(655, 154)
            Me.cmdTargetPathBrowse.Name = "cmdTargetPathBrowse"
            Me.cmdTargetPathBrowse.Size = New System.Drawing.Size(27, 23)
            Me.cmdTargetPathBrowse.TabIndex = 5
            Me.cmdTargetPathBrowse.Text = "..."
            Me.tlpToolTip.SetToolTip(Me.cmdTargetPathBrowse, "Browse")
            Me.cmdTargetPathBrowse.UseVisualStyleBackColor = True
            '
            'cmdGo
            '
            Me.cmdGo.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdGo.AutoSize = True
            Me.cmdGo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpMainAndOnly.SetColumnSpan(Me.cmdGo, 5)
            Me.cmdGo.Location = New System.Drawing.Point(327, 238)
            Me.cmdGo.Name = "cmdGo"
            Me.cmdGo.Size = New System.Drawing.Size(31, 23)
            Me.cmdGo.TabIndex = 8
            Me.cmdGo.Text = "&Go"
            Me.cmdGo.UseVisualStyleBackColor = True
            '
            'sfdSaveXml
            '
            Me.sfdSaveXml.DefaultExt = "xml"
            Me.sfdSaveXml.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*"
            Me.sfdSaveXml.OverwritePrompt = False
            '
            'lblLanguage
            '
            Me.lblLanguage.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblLanguage.AutoSize = True
            Me.lblLanguage.Location = New System.Drawing.Point(3, 186)
            Me.lblLanguage.Name = "lblLanguage"
            Me.lblLanguage.Size = New System.Drawing.Size(55, 13)
            Me.lblLanguage.TabIndex = 6
            Me.lblLanguage.Text = "Langauge"
            Me.tlpToolTip.SetToolTip(Me.lblLanguage, "Enter language code, e.g. en-US")
            '
            'txtLanguage
            '
            Me.txtLanguage.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.txtLanguage.Location = New System.Drawing.Point(64, 183)
            Me.txtLanguage.Name = "txtLanguage"
            Me.txtLanguage.Size = New System.Drawing.Size(100, 20)
            Me.txtLanguage.TabIndex = 7
            Me.tlpToolTip.SetToolTip(Me.txtLanguage, "Enter language code, e.g. en-US")
            '
            'chkComments
            '
            Me.chkComments.AutoSize = True
            Me.tlpMainAndOnly.SetColumnSpan(Me.chkComments, 2)
            Me.chkComments.Location = New System.Drawing.Point(3, 209)
            Me.chkComments.Name = "chkComments"
            Me.chkComments.Size = New System.Drawing.Size(189, 17)
            Me.chkComments.TabIndex = 9
            Me.chkComments.Text = "Generate comments for debugging"
            Me.chkComments.UseVisualStyleBackColor = True
            '
            'lblCheck
            '
            Me.lblCheck.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.lblCheck.AutoSize = True
            Me.lblCheck.Location = New System.Drawing.Point(319, 214)
            Me.lblCheck.Name = "lblCheck"
            Me.lblCheck.Size = New System.Drawing.Size(75, 13)
            Me.lblCheck.TabIndex = 10
            Me.lblCheck.Text = "Check against"
            Me.tlpToolTip.SetToolTip(Me.lblCheck, "If file is specified only includes characters which names differe form specified " & _
                    "file")
            '
            'txtCheck
            '
            Me.txtCheck.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtCheck.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.txtCheck.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
            Me.txtCheck.Location = New System.Drawing.Point(400, 210)
            Me.txtCheck.Name = "txtCheck"
            Me.txtCheck.Size = New System.Drawing.Size(249, 20)
            Me.txtCheck.TabIndex = 11
            Me.tlpToolTip.SetToolTip(Me.txtCheck, "If file is specified only includes characters which names differe form specified " & _
                    "file")
            '
            'cmdCheckAgainst
            '
            Me.cmdCheckAgainst.AutoSize = True
            Me.cmdCheckAgainst.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdCheckAgainst.Location = New System.Drawing.Point(655, 209)
            Me.cmdCheckAgainst.Name = "cmdCheckAgainst"
            Me.cmdCheckAgainst.Size = New System.Drawing.Size(26, 23)
            Me.cmdCheckAgainst.TabIndex = 12
            Me.cmdCheckAgainst.Text = "..."
            Me.tlpToolTip.SetToolTip(Me.cmdCheckAgainst, "Browse")
            Me.cmdCheckAgainst.UseVisualStyleBackColor = True
            '
            'ofdOpenXml
            '
            Me.ofdOpenXml.DefaultExt = "xml"
            Me.ofdOpenXml.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*"
            '
            'cmdAddMui
            '
            Me.cmdAddMui.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.cmdAddMui.AutoSize = True
            Me.cmdAddMui.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdAddMui.Enabled = False
            Me.cmdAddMui.Location = New System.Drawing.Point(288, 29)
            Me.cmdAddMui.Name = "cmdAddMui"
            Me.cmdAddMui.Size = New System.Drawing.Size(48, 23)
            Me.cmdAddMui.TabIndex = 4
            Me.cmdAddMui.Text = "Add ..."
            Me.cmdAddMui.UseVisualStyleBackColor = True
            '
            'ofdMui
            '
            Me.ofdMui.DefaultExt = "dll.mui"
            Me.ofdMui.Filter = "MUI files (*.mui)|*.mui|DLLs (*.dll)|*.dll|All files (*.*)|*.*"
            '
            'frmUcdTranslate
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(685, 434)
            Me.Controls.Add(Me.tlpMainAndOnly)
            Me.Name = "frmUcdTranslate"
            Me.Text = "UCD Translate"
            Me.tlpMainAndOnly.ResumeLayout(False)
            Me.tlpMainAndOnly.PerformLayout()
            Me.fraChars.ResumeLayout(False)
            Me.fraChars.PerformLayout()
            Me.tlpChars.ResumeLayout(False)
            Me.tlpChars.PerformLayout()
            Me.fraBlocks.ResumeLayout(False)
            Me.fraBlocks.PerformLayout()
            Me.tlpBlocks.ResumeLayout(False)
            Me.tlpBlocks.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents tlpMainAndOnly As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents lblInfo As System.Windows.Forms.Label
        Friend WithEvents fraChars As System.Windows.Forms.GroupBox
        Friend WithEvents fraBlocks As System.Windows.Forms.GroupBox
        Friend WithEvents lblTraget As System.Windows.Forms.Label
        Friend WithEvents txtTarget As System.Windows.Forms.TextBox
        Friend WithEvents cmdTargetPathBrowse As System.Windows.Forms.Button
        Friend WithEvents cmdGo As System.Windows.Forms.Button
        Friend WithEvents tlpChars As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents tlpBlocks As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents tlpToolTip As System.Windows.Forms.ToolTip
        Friend WithEvents sfdSaveXml As System.Windows.Forms.SaveFileDialog
        Friend WithEvents chkChars As System.Windows.Forms.CheckBox
        Friend WithEvents chkBlocks As System.Windows.Forms.CheckBox
        Friend WithEvents lblCharsI As System.Windows.Forms.Label
        Friend WithEvents lblCharsLanguage As System.Windows.Forms.Label
        Friend WithEvents cmbCharsLanguage As System.Windows.Forms.ComboBox
        Friend WithEvents tlpCharsI As System.Windows.Forms.Label
        Friend WithEvents lblLanguage As System.Windows.Forms.Label
        Friend WithEvents txtLanguage As System.Windows.Forms.TextBox
        Friend WithEvents chkComments As System.Windows.Forms.CheckBox
        Friend WithEvents lblCheck As System.Windows.Forms.Label
        Friend WithEvents txtCheck As System.Windows.Forms.TextBox
        Friend WithEvents cmdCheckAgainst As System.Windows.Forms.Button
        Friend WithEvents ofdOpenXml As System.Windows.Forms.OpenFileDialog
        Friend WithEvents cmdAddMui As System.Windows.Forms.Button
        Friend WithEvents ofdMui As System.Windows.Forms.OpenFileDialog
    End Class
End Namespace