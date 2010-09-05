<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExport))
        Me.tabMain = New System.Windows.Forms.TabControl()
        Me.tapFields = New System.Windows.Forms.TabPage()
        Me.tvwFields = New System.Windows.Forms.TreeView()
        Me.tapFormat = New System.Windows.Forms.TabPage()
        Me.pnlFormatHolder = New System.Windows.Forms.Panel()
        Me.flpFormatSelector = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblFormat = New System.Windows.Forms.Label()
        Me.cmbFormat = New System.Windows.Forms.ComboBox()
        Me.tlpButtons = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.sfdSave = New System.Windows.Forms.SaveFileDialog()
        Me.tabMain.SuspendLayout()
        Me.tapFields.SuspendLayout()
        Me.tapFormat.SuspendLayout()
        Me.flpFormatSelector.SuspendLayout()
        Me.tlpButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabMain
        '
        Me.tabMain.Controls.Add(Me.tapFields)
        Me.tabMain.Controls.Add(Me.tapFormat)
        Me.tabMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabMain.Location = New System.Drawing.Point(0, 0)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedIndex = 0
        Me.tabMain.Size = New System.Drawing.Size(553, 392)
        Me.tabMain.TabIndex = 0
        '
        'tapFields
        '
        Me.tapFields.Controls.Add(Me.tvwFields)
        Me.tapFields.Location = New System.Drawing.Point(4, 22)
        Me.tapFields.Name = "tapFields"
        Me.tapFields.Padding = New System.Windows.Forms.Padding(3)
        Me.tapFields.Size = New System.Drawing.Size(545, 366)
        Me.tapFields.TabIndex = 0
        Me.tapFields.Text = "Fields"
        Me.tapFields.UseVisualStyleBackColor = True
        '
        'tvwFields
        '
        Me.tvwFields.CheckBoxes = True
        Me.tvwFields.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvwFields.Location = New System.Drawing.Point(3, 3)
        Me.tvwFields.Name = "tvwFields"
        Me.tvwFields.Size = New System.Drawing.Size(539, 360)
        Me.tvwFields.TabIndex = 0
        '
        'tapFormat
        '
        Me.tapFormat.Controls.Add(Me.pnlFormatHolder)
        Me.tapFormat.Controls.Add(Me.flpFormatSelector)
        Me.tapFormat.Location = New System.Drawing.Point(4, 22)
        Me.tapFormat.Name = "tapFormat"
        Me.tapFormat.Padding = New System.Windows.Forms.Padding(3)
        Me.tapFormat.Size = New System.Drawing.Size(545, 366)
        Me.tapFormat.TabIndex = 1
        Me.tapFormat.Text = "Format"
        Me.tapFormat.UseVisualStyleBackColor = True
        '
        'pnlFormatHolder
        '
        Me.pnlFormatHolder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFormatHolder.Location = New System.Drawing.Point(3, 30)
        Me.pnlFormatHolder.Name = "pnlFormatHolder"
        Me.pnlFormatHolder.Size = New System.Drawing.Size(539, 333)
        Me.pnlFormatHolder.TabIndex = 2
        '
        'flpFormatSelector
        '
        Me.flpFormatSelector.AutoSize = True
        Me.flpFormatSelector.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpFormatSelector.Controls.Add(Me.lblFormat)
        Me.flpFormatSelector.Controls.Add(Me.cmbFormat)
        Me.flpFormatSelector.Dock = System.Windows.Forms.DockStyle.Top
        Me.flpFormatSelector.Location = New System.Drawing.Point(3, 3)
        Me.flpFormatSelector.Name = "flpFormatSelector"
        Me.flpFormatSelector.Size = New System.Drawing.Size(539, 27)
        Me.flpFormatSelector.TabIndex = 1
        '
        'lblFormat
        '
        Me.lblFormat.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblFormat.AutoSize = True
        Me.lblFormat.Location = New System.Drawing.Point(3, 7)
        Me.lblFormat.Name = "lblFormat"
        Me.lblFormat.Size = New System.Drawing.Size(39, 13)
        Me.lblFormat.TabIndex = 0
        Me.lblFormat.Text = "Format"
        '
        'cmbFormat
        '
        Me.cmbFormat.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmbFormat.DisplayMember = "FormatName"
        Me.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFormat.FormattingEnabled = True
        Me.cmbFormat.Location = New System.Drawing.Point(48, 3)
        Me.cmbFormat.Name = "cmbFormat"
        Me.cmbFormat.Size = New System.Drawing.Size(121, 21)
        Me.cmbFormat.TabIndex = 1
        '
        'tlpButtons
        '
        Me.tlpButtons.AutoSize = True
        Me.tlpButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpButtons.ColumnCount = 2
        Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.Controls.Add(Me.cmdOK, 0, 0)
        Me.tlpButtons.Controls.Add(Me.cmdCancel, 1, 0)
        Me.tlpButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tlpButtons.Location = New System.Drawing.Point(0, 392)
        Me.tlpButtons.Name = "tlpButtons"
        Me.tlpButtons.RowCount = 1
        Me.tlpButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpButtons.Size = New System.Drawing.Size(553, 29)
        Me.tlpButtons.TabIndex = 0
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdOK.Location = New System.Drawing.Point(100, 3)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 0
        Me.cmdOK.Text = "Exp&ort ..."
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(377, 3)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'sfdSave
        '
        Me.sfdSave.Title = "Export metadata"
        '
        'frmExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(553, 421)
        Me.Controls.Add(Me.tabMain)
        Me.Controls.Add(Me.tlpButtons)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "frmExport"
        Me.ShowInTaskbar = False
        Me.Text = "Export metadata"
        Me.tabMain.ResumeLayout(False)
        Me.tapFields.ResumeLayout(False)
        Me.tapFormat.ResumeLayout(False)
        Me.tapFormat.PerformLayout()
        Me.flpFormatSelector.ResumeLayout(False)
        Me.flpFormatSelector.PerformLayout()
        Me.tlpButtons.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tabMain As System.Windows.Forms.TabControl
    Friend WithEvents tapFields As System.Windows.Forms.TabPage
    Friend WithEvents tapFormat As System.Windows.Forms.TabPage
    Friend WithEvents tlpButtons As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents tvwFields As System.Windows.Forms.TreeView
    Friend WithEvents pnlFormatHolder As System.Windows.Forms.Panel
    Friend WithEvents flpFormatSelector As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblFormat As System.Windows.Forms.Label
    Friend WithEvents cmbFormat As System.Windows.Forms.ComboBox
    Friend WithEvents sfdSave As System.Windows.Forms.SaveFileDialog
End Class
