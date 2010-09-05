<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CsvFormatConfigurator
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
        Me.fraMain = New System.Windows.Forms.GroupBox()
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel()
        Me.lblSeparator = New System.Windows.Forms.Label()
        Me.txtSeparator = New System.Windows.Forms.TextBox()
        Me.cmdTab = New System.Windows.Forms.Button()
        Me.lblQualifier = New System.Windows.Forms.Label()
        Me.txtQualifier = New System.Windows.Forms.TextBox()
        Me.chkQualifierUsage = New System.Windows.Forms.CheckBox()
        Me.lblNewLine = New System.Windows.Forms.Label()
        Me.cmbNewLine = New System.Windows.Forms.ComboBox()
        Me.lblEncoding = New System.Windows.Forms.Label()
        Me.cmbEncoding = New System.Windows.Forms.ComboBox()
        Me.cmdExcelFriendly = New System.Windows.Forms.Button()
        Me.lblCulture = New System.Windows.Forms.Label()
        Me.cmbCulture = New System.Windows.Forms.ComboBox()
        Me.lblQualifierEscape = New System.Windows.Forms.Label()
        Me.optEscapeDouble = New System.Windows.Forms.RadioButton()
        Me.optEscapeBackSlash = New System.Windows.Forms.RadioButton()
        Me.fraMain.SuspendLayout()
        Me.tlpMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'fraMain
        '
        Me.fraMain.Controls.Add(Me.tlpMain)
        Me.fraMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fraMain.Location = New System.Drawing.Point(0, 0)
        Me.fraMain.Name = "fraMain"
        Me.fraMain.Size = New System.Drawing.Size(639, 259)
        Me.fraMain.TabIndex = 0
        Me.fraMain.TabStop = False
        Me.fraMain.Text = "CSV"
        '
        'tlpMain
        '
        Me.tlpMain.AutoSize = True
        Me.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpMain.ColumnCount = 3
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tlpMain.Controls.Add(Me.lblSeparator, 0, 0)
        Me.tlpMain.Controls.Add(Me.txtSeparator, 1, 0)
        Me.tlpMain.Controls.Add(Me.cmdTab, 2, 0)
        Me.tlpMain.Controls.Add(Me.lblQualifier, 0, 1)
        Me.tlpMain.Controls.Add(Me.txtQualifier, 1, 1)
        Me.tlpMain.Controls.Add(Me.chkQualifierUsage, 2, 1)
        Me.tlpMain.Controls.Add(Me.lblNewLine, 0, 2)
        Me.tlpMain.Controls.Add(Me.cmbNewLine, 1, 2)
        Me.tlpMain.Controls.Add(Me.lblEncoding, 0, 3)
        Me.tlpMain.Controls.Add(Me.cmbEncoding, 1, 3)
        Me.tlpMain.Controls.Add(Me.cmdExcelFriendly, 2, 2)
        Me.tlpMain.Controls.Add(Me.lblCulture, 0, 4)
        Me.tlpMain.Controls.Add(Me.cmbCulture, 1, 4)
        Me.tlpMain.Controls.Add(Me.lblQualifierEscape, 0, 5)
        Me.tlpMain.Controls.Add(Me.optEscapeDouble, 1, 5)
        Me.tlpMain.Controls.Add(Me.optEscapeBackSlash, 1, 6)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(3, 16)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 8
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpMain.Size = New System.Drawing.Size(633, 240)
        Me.tlpMain.TabIndex = 0
        '
        'lblSeparator
        '
        Me.lblSeparator.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblSeparator.AutoSize = True
        Me.lblSeparator.Location = New System.Drawing.Point(33, 9)
        Me.lblSeparator.Name = "lblSeparator"
        Me.lblSeparator.Size = New System.Drawing.Size(53, 13)
        Me.lblSeparator.TabIndex = 0
        Me.lblSeparator.Text = "Separator"
        '
        'txtSeparator
        '
        Me.txtSeparator.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSeparator.Location = New System.Drawing.Point(92, 5)
        Me.txtSeparator.MaxLength = 1
        Me.txtSeparator.Name = "txtSeparator"
        Me.txtSeparator.Size = New System.Drawing.Size(385, 20)
        Me.txtSeparator.TabIndex = 1
        Me.txtSeparator.Text = ","
        '
        'cmdTab
        '
        Me.cmdTab.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdTab.AutoSize = True
        Me.cmdTab.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdTab.Location = New System.Drawing.Point(483, 3)
        Me.cmdTab.Name = "cmdTab"
        Me.cmdTab.Size = New System.Drawing.Size(40, 25)
        Me.cmdTab.TabIndex = 3
        Me.cmdTab.Text = "TAB"
        Me.cmdTab.UseVisualStyleBackColor = True
        '
        'lblQualifier
        '
        Me.lblQualifier.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblQualifier.AutoSize = True
        Me.lblQualifier.Location = New System.Drawing.Point(19, 37)
        Me.lblQualifier.Name = "lblQualifier"
        Me.lblQualifier.Size = New System.Drawing.Size(67, 13)
        Me.lblQualifier.TabIndex = 2
        Me.lblQualifier.Text = "Text qualifier"
        '
        'txtQualifier
        '
        Me.txtQualifier.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtQualifier.Location = New System.Drawing.Point(92, 34)
        Me.txtQualifier.MaxLength = 1
        Me.txtQualifier.Name = "txtQualifier"
        Me.txtQualifier.Size = New System.Drawing.Size(385, 20)
        Me.txtQualifier.TabIndex = 4
        Me.txtQualifier.Text = """"
        '
        'chkQualifierUsage
        '
        Me.chkQualifierUsage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkQualifierUsage.AutoSize = True
        Me.chkQualifierUsage.Location = New System.Drawing.Point(483, 35)
        Me.chkQualifierUsage.Name = "chkQualifierUsage"
        Me.chkQualifierUsage.Size = New System.Drawing.Size(147, 17)
        Me.chkQualifierUsage.TabIndex = 5
        Me.chkQualifierUsage.Text = "Use only when necessary"
        Me.chkQualifierUsage.UseVisualStyleBackColor = True
        '
        'lblNewLine
        '
        Me.lblNewLine.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblNewLine.AutoSize = True
        Me.lblNewLine.Location = New System.Drawing.Point(38, 64)
        Me.lblNewLine.Name = "lblNewLine"
        Me.lblNewLine.Size = New System.Drawing.Size(48, 13)
        Me.lblNewLine.TabIndex = 6
        Me.lblNewLine.Text = "New line"
        '
        'cmbNewLine
        '
        Me.cmbNewLine.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbNewLine.DisplayMember = "Name"
        Me.cmbNewLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNewLine.FormattingEnabled = True
        Me.cmbNewLine.Location = New System.Drawing.Point(92, 60)
        Me.cmbNewLine.Name = "cmbNewLine"
        Me.cmbNewLine.Size = New System.Drawing.Size(385, 21)
        Me.cmbNewLine.TabIndex = 7
        Me.cmbNewLine.ValueMember = "NewLine"
        '
        'lblEncoding
        '
        Me.lblEncoding.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblEncoding.AutoSize = True
        Me.lblEncoding.Location = New System.Drawing.Point(34, 91)
        Me.lblEncoding.Name = "lblEncoding"
        Me.lblEncoding.Size = New System.Drawing.Size(52, 13)
        Me.lblEncoding.TabIndex = 8
        Me.lblEncoding.Text = "Encoding"
        '
        'cmbEncoding
        '
        Me.cmbEncoding.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbEncoding.DisplayMember = "DisplayName"
        Me.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEncoding.FormattingEnabled = True
        Me.cmbEncoding.Location = New System.Drawing.Point(92, 87)
        Me.cmbEncoding.Name = "cmbEncoding"
        Me.cmbEncoding.Size = New System.Drawing.Size(385, 21)
        Me.cmbEncoding.Sorted = True
        Me.cmbEncoding.TabIndex = 9
        '
        'cmdExcelFriendly
        '
        Me.cmdExcelFriendly.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExcelFriendly.AutoSize = True
        Me.cmdExcelFriendly.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdExcelFriendly.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdExcelFriendly.Location = New System.Drawing.Point(489, 156)
        Me.cmdExcelFriendly.Name = "cmdExcelFriendly"
        Me.tlpMain.SetRowSpan(Me.cmdExcelFriendly, 5)
        Me.cmdExcelFriendly.Size = New System.Drawing.Size(141, 25)
        Me.cmdExcelFriendly.TabIndex = 10
        Me.cmdExcelFriendly.Text = "Fill in Excel-friendly values"
        Me.cmdExcelFriendly.UseVisualStyleBackColor = True
        '
        'lblCulture
        '
        Me.lblCulture.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblCulture.AutoSize = True
        Me.lblCulture.Location = New System.Drawing.Point(46, 118)
        Me.lblCulture.Name = "lblCulture"
        Me.lblCulture.Size = New System.Drawing.Size(40, 13)
        Me.lblCulture.TabIndex = 11
        Me.lblCulture.Text = "Culture"
        '
        'cmbCulture
        '
        Me.cmbCulture.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbCulture.DisplayMember = "DisplayName"
        Me.cmbCulture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCulture.FormattingEnabled = True
        Me.cmbCulture.Location = New System.Drawing.Point(92, 114)
        Me.cmbCulture.Name = "cmbCulture"
        Me.cmbCulture.Size = New System.Drawing.Size(385, 21)
        Me.cmbCulture.Sorted = True
        Me.cmbCulture.TabIndex = 12
        '
        'lblQualifierEscape
        '
        Me.lblQualifierEscape.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblQualifierEscape.AutoSize = True
        Me.lblQualifierEscape.Location = New System.Drawing.Point(3, 154)
        Me.lblQualifierEscape.Name = "lblQualifierEscape"
        Me.tlpMain.SetRowSpan(Me.lblQualifierEscape, 2)
        Me.lblQualifierEscape.Size = New System.Drawing.Size(83, 13)
        Me.lblQualifierEscape.TabIndex = 13
        Me.lblQualifierEscape.Text = "Qualifier escape"
        '
        'optEscapeDouble
        '
        Me.optEscapeDouble.AutoSize = True
        Me.optEscapeDouble.Checked = True
        Me.optEscapeDouble.Location = New System.Drawing.Point(92, 141)
        Me.optEscapeDouble.Name = "optEscapeDouble"
        Me.optEscapeDouble.Size = New System.Drawing.Size(59, 17)
        Me.optEscapeDouble.TabIndex = 14
        Me.optEscapeDouble.TabStop = True
        Me.optEscapeDouble.Text = "Double"
        Me.optEscapeDouble.UseVisualStyleBackColor = True
        '
        'optEscapeBackSlash
        '
        Me.optEscapeBackSlash.AutoSize = True
        Me.optEscapeBackSlash.Location = New System.Drawing.Point(92, 164)
        Me.optEscapeBackSlash.Name = "optEscapeBackSlash"
        Me.optEscapeBackSlash.Size = New System.Drawing.Size(30, 17)
        Me.optEscapeBackSlash.TabIndex = 14
        Me.optEscapeBackSlash.Text = "\"
        Me.optEscapeBackSlash.UseVisualStyleBackColor = True
        '
        'CsvFormatConfigurator
        '
        Me.Controls.Add(Me.fraMain)
        Me.Name = "CsvFormatConfigurator"
        Me.Size = New System.Drawing.Size(639, 259)
        Me.fraMain.ResumeLayout(False)
        Me.fraMain.PerformLayout()
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents fraMain As System.Windows.Forms.GroupBox
    Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblSeparator As System.Windows.Forms.Label
    Friend WithEvents txtSeparator As System.Windows.Forms.TextBox
    Friend WithEvents lblQualifier As System.Windows.Forms.Label
    Friend WithEvents cmdTab As System.Windows.Forms.Button
    Friend WithEvents txtQualifier As System.Windows.Forms.TextBox
    Friend WithEvents chkQualifierUsage As System.Windows.Forms.CheckBox
    Friend WithEvents lblNewLine As System.Windows.Forms.Label
    Friend WithEvents cmbNewLine As System.Windows.Forms.ComboBox
    Friend WithEvents lblEncoding As System.Windows.Forms.Label
    Friend WithEvents cmbEncoding As System.Windows.Forms.ComboBox
    Friend WithEvents cmdExcelFriendly As System.Windows.Forms.Button
    Friend WithEvents lblCulture As System.Windows.Forms.Label
    Friend WithEvents cmbCulture As System.Windows.Forms.ComboBox
    Friend WithEvents lblQualifierEscape As System.Windows.Forms.Label
    Friend WithEvents optEscapeDouble As System.Windows.Forms.RadioButton
    Friend WithEvents optEscapeBackSlash As System.Windows.Forms.RadioButton

End Class
