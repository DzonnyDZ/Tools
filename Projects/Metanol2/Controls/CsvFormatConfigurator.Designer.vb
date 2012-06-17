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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CsvFormatConfigurator))
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
        resources.ApplyResources(Me.fraMain, "fraMain")
        Me.fraMain.Controls.Add(Me.tlpMain)
        Me.fraMain.Name = "fraMain"
        Me.fraMain.TabStop = False
        '
        'tlpMain
        '
        resources.ApplyResources(Me.tlpMain, "tlpMain")
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
        Me.tlpMain.Name = "tlpMain"
        '
        'lblSeparator
        '
        resources.ApplyResources(Me.lblSeparator, "lblSeparator")
        Me.lblSeparator.Name = "lblSeparator"
        '
        'txtSeparator
        '
        resources.ApplyResources(Me.txtSeparator, "txtSeparator")
        Me.txtSeparator.Name = "txtSeparator"
        '
        'cmdTab
        '
        resources.ApplyResources(Me.cmdTab, "cmdTab")
        Me.cmdTab.Name = "cmdTab"
        Me.cmdTab.UseVisualStyleBackColor = True
        '
        'lblQualifier
        '
        resources.ApplyResources(Me.lblQualifier, "lblQualifier")
        Me.lblQualifier.Name = "lblQualifier"
        '
        'txtQualifier
        '
        resources.ApplyResources(Me.txtQualifier, "txtQualifier")
        Me.txtQualifier.Name = "txtQualifier"
        '
        'chkQualifierUsage
        '
        resources.ApplyResources(Me.chkQualifierUsage, "chkQualifierUsage")
        Me.chkQualifierUsage.Name = "chkQualifierUsage"
        Me.chkQualifierUsage.UseVisualStyleBackColor = True
        '
        'lblNewLine
        '
        resources.ApplyResources(Me.lblNewLine, "lblNewLine")
        Me.lblNewLine.Name = "lblNewLine"
        '
        'cmbNewLine
        '
        resources.ApplyResources(Me.cmbNewLine, "cmbNewLine")
        Me.cmbNewLine.DisplayMember = "Name"
        Me.cmbNewLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNewLine.FormattingEnabled = True
        Me.cmbNewLine.Name = "cmbNewLine"
        Me.cmbNewLine.ValueMember = "NewLine"
        '
        'lblEncoding
        '
        resources.ApplyResources(Me.lblEncoding, "lblEncoding")
        Me.lblEncoding.Name = "lblEncoding"
        '
        'cmbEncoding
        '
        resources.ApplyResources(Me.cmbEncoding, "cmbEncoding")
        Me.cmbEncoding.DisplayMember = "DisplayName"
        Me.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEncoding.FormattingEnabled = True
        Me.cmbEncoding.Name = "cmbEncoding"
        Me.cmbEncoding.Sorted = True
        '
        'cmdExcelFriendly
        '
        resources.ApplyResources(Me.cmdExcelFriendly, "cmdExcelFriendly")
        Me.cmdExcelFriendly.Name = "cmdExcelFriendly"
        Me.tlpMain.SetRowSpan(Me.cmdExcelFriendly, 5)
        Me.cmdExcelFriendly.UseVisualStyleBackColor = True
        '
        'lblCulture
        '
        resources.ApplyResources(Me.lblCulture, "lblCulture")
        Me.lblCulture.Name = "lblCulture"
        '
        'cmbCulture
        '
        resources.ApplyResources(Me.cmbCulture, "cmbCulture")
        Me.cmbCulture.DisplayMember = "DisplayName"
        Me.cmbCulture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCulture.FormattingEnabled = True
        Me.cmbCulture.Name = "cmbCulture"
        Me.cmbCulture.Sorted = True
        '
        'lblQualifierEscape
        '
        resources.ApplyResources(Me.lblQualifierEscape, "lblQualifierEscape")
        Me.lblQualifierEscape.Name = "lblQualifierEscape"
        Me.tlpMain.SetRowSpan(Me.lblQualifierEscape, 2)
        '
        'optEscapeDouble
        '
        resources.ApplyResources(Me.optEscapeDouble, "optEscapeDouble")
        Me.optEscapeDouble.Checked = True
        Me.optEscapeDouble.Name = "optEscapeDouble"
        Me.optEscapeDouble.TabStop = True
        Me.optEscapeDouble.UseVisualStyleBackColor = True
        '
        'optEscapeBackSlash
        '
        resources.ApplyResources(Me.optEscapeBackSlash, "optEscapeBackSlash")
        Me.optEscapeBackSlash.Name = "optEscapeBackSlash"
        Me.optEscapeBackSlash.UseVisualStyleBackColor = True
        '
        'CsvFormatConfigurator
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.fraMain)
        Me.Name = "CsvFormatConfigurator"
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
