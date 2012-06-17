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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExport))
        Me.tabMain = New System.Windows.Forms.TabControl()
        Me.tapFields = New System.Windows.Forms.TabPage()
        Me.tvwFields = New System.Windows.Forms.TreeView()
        Me.tapFormat = New System.Windows.Forms.TabPage()
        Me.pnlFormatHolder = New System.Windows.Forms.Panel()
        Me.flpFormatSelector = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblFormat = New System.Windows.Forms.Label()
        Me.cmbFormat = New System.Windows.Forms.ComboBox()
        Me.lblCulture = New System.Windows.Forms.Label()
        Me.cmbCulture = New System.Windows.Forms.ComboBox()
        Me.tlpButtons = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.sfdSave = New System.Windows.Forms.SaveFileDialog()
        Me.totToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.tabMain.SuspendLayout()
        Me.tapFields.SuspendLayout()
        Me.tapFormat.SuspendLayout()
        Me.flpFormatSelector.SuspendLayout()
        Me.tlpButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabMain
        '
        resources.ApplyResources(Me.tabMain, "tabMain")
        Me.tabMain.Controls.Add(Me.tapFields)
        Me.tabMain.Controls.Add(Me.tapFormat)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedIndex = 0
        Me.totToolTip.SetToolTip(Me.tabMain, resources.GetString("tabMain.ToolTip"))
        '
        'tapFields
        '
        resources.ApplyResources(Me.tapFields, "tapFields")
        Me.tapFields.Controls.Add(Me.tvwFields)
        Me.tapFields.Name = "tapFields"
        Me.totToolTip.SetToolTip(Me.tapFields, resources.GetString("tapFields.ToolTip"))
        Me.tapFields.UseVisualStyleBackColor = True
        '
        'tvwFields
        '
        resources.ApplyResources(Me.tvwFields, "tvwFields")
        Me.tvwFields.CheckBoxes = True
        Me.tvwFields.Name = "tvwFields"
        Me.totToolTip.SetToolTip(Me.tvwFields, resources.GetString("tvwFields.ToolTip"))
        '
        'tapFormat
        '
        resources.ApplyResources(Me.tapFormat, "tapFormat")
        Me.tapFormat.Controls.Add(Me.pnlFormatHolder)
        Me.tapFormat.Controls.Add(Me.flpFormatSelector)
        Me.tapFormat.Name = "tapFormat"
        Me.totToolTip.SetToolTip(Me.tapFormat, resources.GetString("tapFormat.ToolTip"))
        Me.tapFormat.UseVisualStyleBackColor = True
        '
        'pnlFormatHolder
        '
        resources.ApplyResources(Me.pnlFormatHolder, "pnlFormatHolder")
        Me.pnlFormatHolder.Name = "pnlFormatHolder"
        Me.totToolTip.SetToolTip(Me.pnlFormatHolder, resources.GetString("pnlFormatHolder.ToolTip"))
        '
        'flpFormatSelector
        '
        resources.ApplyResources(Me.flpFormatSelector, "flpFormatSelector")
        Me.flpFormatSelector.Controls.Add(Me.lblFormat)
        Me.flpFormatSelector.Controls.Add(Me.cmbFormat)
        Me.flpFormatSelector.Controls.Add(Me.lblCulture)
        Me.flpFormatSelector.Controls.Add(Me.cmbCulture)
        Me.flpFormatSelector.Name = "flpFormatSelector"
        Me.totToolTip.SetToolTip(Me.flpFormatSelector, resources.GetString("flpFormatSelector.ToolTip"))
        '
        'lblFormat
        '
        resources.ApplyResources(Me.lblFormat, "lblFormat")
        Me.lblFormat.Name = "lblFormat"
        Me.totToolTip.SetToolTip(Me.lblFormat, resources.GetString("lblFormat.ToolTip"))
        '
        'cmbFormat
        '
        resources.ApplyResources(Me.cmbFormat, "cmbFormat")
        Me.cmbFormat.DisplayMember = "FormatName"
        Me.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFormat.FormattingEnabled = True
        Me.cmbFormat.Name = "cmbFormat"
        Me.totToolTip.SetToolTip(Me.cmbFormat, resources.GetString("cmbFormat.ToolTip"))
        '
        'lblCulture
        '
        resources.ApplyResources(Me.lblCulture, "lblCulture")
        Me.lblCulture.Name = "lblCulture"
        Me.totToolTip.SetToolTip(Me.lblCulture, resources.GetString("lblCulture.ToolTip"))
        '
        'cmbCulture
        '
        resources.ApplyResources(Me.cmbCulture, "cmbCulture")
        Me.cmbCulture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCulture.FormattingEnabled = True
        Me.cmbCulture.Name = "cmbCulture"
        Me.totToolTip.SetToolTip(Me.cmbCulture, resources.GetString("cmbCulture.ToolTip"))
        '
        'tlpButtons
        '
        resources.ApplyResources(Me.tlpButtons, "tlpButtons")
        Me.tlpButtons.Controls.Add(Me.cmdOK, 0, 0)
        Me.tlpButtons.Controls.Add(Me.cmdCancel, 1, 0)
        Me.tlpButtons.Name = "tlpButtons"
        Me.totToolTip.SetToolTip(Me.tlpButtons, resources.GetString("tlpButtons.ToolTip"))
        '
        'cmdOK
        '
        resources.ApplyResources(Me.cmdOK, "cmdOK")
        Me.cmdOK.Name = "cmdOK"
        Me.totToolTip.SetToolTip(Me.cmdOK, resources.GetString("cmdOK.ToolTip"))
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        resources.ApplyResources(Me.cmdCancel, "cmdCancel")
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Name = "cmdCancel"
        Me.totToolTip.SetToolTip(Me.cmdCancel, resources.GetString("cmdCancel.ToolTip"))
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'sfdSave
        '
        resources.ApplyResources(Me.sfdSave, "sfdSave")
        '
        'frmExport
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.Controls.Add(Me.tabMain)
        Me.Controls.Add(Me.tlpButtons)
        Me.MinimizeBox = False
        Me.Name = "frmExport"
        Me.ShowInTaskbar = False
        Me.totToolTip.SetToolTip(Me, resources.GetString("$this.ToolTip"))
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
    Friend WithEvents lblCulture As System.Windows.Forms.Label
    Friend WithEvents cmbCulture As System.Windows.Forms.ComboBox
    Friend WithEvents totToolTip As System.Windows.Forms.ToolTip
End Class
