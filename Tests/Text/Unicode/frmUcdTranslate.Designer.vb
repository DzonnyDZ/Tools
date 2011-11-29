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
            Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            Me.GroupBox2 = New System.Windows.Forms.GroupBox()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.txtTargetPath = New System.Windows.Forms.TextBox()
            Me.cmdTargetPathBrowse = New System.Windows.Forms.Button()
            Me.Button2 = New System.Windows.Forms.Button()
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
            Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
            Me.sfdSaveXml = New System.Windows.Forms.SaveFileDialog()
            Me.CheckBox1 = New System.Windows.Forms.CheckBox()
            Me.CheckBox2 = New System.Windows.Forms.CheckBox()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.TableLayoutPanel1.SuspendLayout()
            Me.GroupBox1.SuspendLayout()
            Me.GroupBox2.SuspendLayout()
            Me.TableLayoutPanel2.SuspendLayout()
            Me.TableLayoutPanel3.SuspendLayout()
            Me.SuspendLayout()
            '
            'TableLayoutPanel1
            '
            Me.TableLayoutPanel1.ColumnCount = 3
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
            Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 1)
            Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 0, 2)
            Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 3)
            Me.TableLayoutPanel1.Controls.Add(Me.txtTargetPath, 1, 3)
            Me.TableLayoutPanel1.Controls.Add(Me.cmdTargetPathBrowse, 2, 3)
            Me.TableLayoutPanel1.Controls.Add(Me.Button2, 0, 4)
            Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
            Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
            Me.TableLayoutPanel1.RowCount = 6
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
            Me.TableLayoutPanel1.Size = New System.Drawing.Size(685, 434)
            Me.TableLayoutPanel1.TabIndex = 0
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.TableLayoutPanel1.SetColumnSpan(Me.Label1, 3)
            Me.Label1.Location = New System.Drawing.Point(3, 0)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(652, 26)
            Me.Label1.TabIndex = 0
            Me.Label1.Text = "This from generates localization XML file that can be used to localize certain te" & _
                "xts exposed by UnicodeCharacterDatabase class. Namely character and block names." & _
                ""
            '
            'GroupBox1
            '
            Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox1.AutoSize = True
            Me.GroupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.TableLayoutPanel1.SetColumnSpan(Me.GroupBox1, 3)
            Me.GroupBox1.Controls.Add(Me.TableLayoutPanel2)
            Me.GroupBox1.Location = New System.Drawing.Point(3, 29)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(679, 42)
            Me.GroupBox1.TabIndex = 1
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Char names"
            '
            'GroupBox2
            '
            Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GroupBox2.AutoSize = True
            Me.GroupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.TableLayoutPanel1.SetColumnSpan(Me.GroupBox2, 3)
            Me.GroupBox2.Controls.Add(Me.TableLayoutPanel3)
            Me.GroupBox2.Location = New System.Drawing.Point(3, 77)
            Me.GroupBox2.Name = "GroupBox2"
            Me.GroupBox2.Size = New System.Drawing.Size(679, 65)
            Me.GroupBox2.TabIndex = 2
            Me.GroupBox2.TabStop = False
            Me.GroupBox2.Text = "Block names"
            '
            'Label2
            '
            Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(3, 153)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(54, 13)
            Me.Label2.TabIndex = 3
            Me.Label2.Text = "Target file"
            '
            'txtTargetPath
            '
            Me.txtTargetPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtTargetPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.txtTargetPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
            Me.txtTargetPath.Location = New System.Drawing.Point(63, 149)
            Me.txtTargetPath.Name = "txtTargetPath"
            Me.txtTargetPath.Size = New System.Drawing.Size(587, 20)
            Me.txtTargetPath.TabIndex = 4
            '
            'cmdTargetPathBrowse
            '
            Me.cmdTargetPathBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cmdTargetPathBrowse.AutoSize = True
            Me.cmdTargetPathBrowse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdTargetPathBrowse.Location = New System.Drawing.Point(656, 148)
            Me.cmdTargetPathBrowse.Name = "cmdTargetPathBrowse"
            Me.cmdTargetPathBrowse.Size = New System.Drawing.Size(26, 23)
            Me.cmdTargetPathBrowse.TabIndex = 5
            Me.cmdTargetPathBrowse.Text = "..."
            Me.ToolTip1.SetToolTip(Me.cmdTargetPathBrowse, "Browse")
            Me.cmdTargetPathBrowse.UseVisualStyleBackColor = True
            '
            'Button2
            '
            Me.Button2.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.Button2.AutoSize = True
            Me.Button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.TableLayoutPanel1.SetColumnSpan(Me.Button2, 3)
            Me.Button2.Location = New System.Drawing.Point(327, 177)
            Me.Button2.Name = "Button2"
            Me.Button2.Size = New System.Drawing.Size(31, 23)
            Me.Button2.TabIndex = 6
            Me.Button2.Text = "&Go"
            Me.Button2.UseVisualStyleBackColor = True
            '
            'TableLayoutPanel2
            '
            Me.TableLayoutPanel2.AutoSize = True
            Me.TableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.TableLayoutPanel2.ColumnCount = 2
            Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel2.Controls.Add(Me.CheckBox1, 0, 0)
            Me.TableLayoutPanel2.Controls.Add(Me.Label3, 1, 0)
            Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top
            Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 16)
            Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
            Me.TableLayoutPanel2.RowCount = 2
            Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel2.Size = New System.Drawing.Size(673, 26)
            Me.TableLayoutPanel2.TabIndex = 0
            '
            'TableLayoutPanel3
            '
            Me.TableLayoutPanel3.AutoSize = True
            Me.TableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.TableLayoutPanel3.ColumnCount = 2
            Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.TableLayoutPanel3.Controls.Add(Me.CheckBox2, 0, 0)
            Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top
            Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 16)
            Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
            Me.TableLayoutPanel3.RowCount = 2
            Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.TableLayoutPanel3.Size = New System.Drawing.Size(673, 46)
            Me.TableLayoutPanel3.TabIndex = 0
            '
            'sfdSaveXml
            '
            Me.sfdSaveXml.DefaultExt = "xml"
            Me.sfdSaveXml.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*"
            Me.sfdSaveXml.OverwritePrompt = False
            '
            'CheckBox1
            '
            Me.CheckBox1.AutoSize = True
            Me.CheckBox1.Location = New System.Drawing.Point(3, 3)
            Me.CheckBox1.Name = "CheckBox1"
            Me.CheckBox1.Size = New System.Drawing.Size(152, 17)
            Me.CheckBox1.TabIndex = 0
            Me.CheckBox1.Text = "Generate character names"
            Me.CheckBox1.UseVisualStyleBackColor = True
            '
            'CheckBox2
            '
            Me.CheckBox2.AutoSize = True
            Me.CheckBox2.Location = New System.Drawing.Point(3, 3)
            Me.CheckBox2.Name = "CheckBox2"
            Me.CheckBox2.Size = New System.Drawing.Size(133, 17)
            Me.CheckBox2.TabIndex = 0
            Me.CheckBox2.Text = "Generate block names"
            Me.CheckBox2.UseVisualStyleBackColor = True
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(161, 0)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(494, 26)
            Me.Label3.TabIndex = 1
            Me.Label3.Text = "Character names are generated using the Windows Charmap program. You need to swit" & _
                "ch Windows to desired language. For this you need Ultimate edition of Windows Vi" & _
                "sta or 7."
            '
            'frmUcdTranslate
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(685, 434)
            Me.Controls.Add(Me.TableLayoutPanel1)
            Me.Name = "frmUcdTranslate"
            Me.Text = "UCD Translate"
            Me.TableLayoutPanel1.ResumeLayout(False)
            Me.TableLayoutPanel1.PerformLayout()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.GroupBox2.ResumeLayout(False)
            Me.GroupBox2.PerformLayout()
            Me.TableLayoutPanel2.ResumeLayout(False)
            Me.TableLayoutPanel2.PerformLayout()
            Me.TableLayoutPanel3.ResumeLayout(False)
            Me.TableLayoutPanel3.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents txtTargetPath As System.Windows.Forms.TextBox
        Friend WithEvents cmdTargetPathBrowse As System.Windows.Forms.Button
        Friend WithEvents Button2 As System.Windows.Forms.Button
        Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
        Friend WithEvents sfdSaveXml As System.Windows.Forms.SaveFileDialog
        Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
        Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
        Friend WithEvents Label3 As System.Windows.Forms.Label
    End Class
End Namespace