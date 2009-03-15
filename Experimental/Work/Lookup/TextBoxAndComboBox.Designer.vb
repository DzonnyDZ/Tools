<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TextBoxAndComboBox
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
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
        Me.txtTextBox = New System.Windows.Forms.TextBox
        Me.cmbComboBox = New System.Windows.Forms.ComboBox
        Me.tlpMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.AutoSize = True
        Me.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpMain.ColumnCount = 2
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.tlpMain.Controls.Add(Me.txtTextBox, 0, 0)
        Me.tlpMain.Controls.Add(Me.cmbComboBox, 1, 0)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 1
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.Size = New System.Drawing.Size(174, 28)
        Me.tlpMain.TabIndex = 0
        '
        'txtTextBox
        '
        Me.txtTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTextBox.Location = New System.Drawing.Point(0, 4)
        Me.txtTextBox.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.txtTextBox.Name = "txtTextBox"
        Me.txtTextBox.Size = New System.Drawing.Size(113, 20)
        Me.txtTextBox.TabIndex = 0
        '
        'cmbComboBox
        '
        Me.cmbComboBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbComboBox.FormattingEnabled = True
        Me.cmbComboBox.Location = New System.Drawing.Point(119, 3)
        Me.cmbComboBox.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.cmbComboBox.Name = "cmbComboBox"
        Me.cmbComboBox.Size = New System.Drawing.Size(55, 21)
        Me.cmbComboBox.TabIndex = 1
        '
        'TextBoxAndComboBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tlpMain)
        Me.MinimumSize = New System.Drawing.Size(64, 22)
        Me.Name = "TextBoxAndComboBox"
        Me.Size = New System.Drawing.Size(174, 28)
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Private WithEvents txtTextBox As System.Windows.Forms.TextBox
    Private WithEvents cmbComboBox As System.Windows.Forms.ComboBox

End Class
