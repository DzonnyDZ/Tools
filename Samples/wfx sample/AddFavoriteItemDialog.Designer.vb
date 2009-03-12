<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddFavoriteItemDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddFavoriteItemDialog))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
        Me.lblPath = New System.Windows.Forms.Label
        Me.txtPath = New System.Windows.Forms.TextBox
        Me.cmdFolder = New System.Windows.Forms.Button
        Me.cmdFile = New System.Windows.Forms.Button
        Me.flpButtons = New System.Windows.Forms.FlowLayoutPanel
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.lblName = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.totToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.fbdFolder = New System.Windows.Forms.FolderBrowserDialog
        Me.ofdFile = New System.Windows.Forms.OpenFileDialog
        Me.tlpMain.SuspendLayout()
        Me.flpButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 3
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpMain.Controls.Add(Me.lblPath, 0, 2)
        Me.tlpMain.Controls.Add(Me.txtPath, 0, 3)
        Me.tlpMain.Controls.Add(Me.cmdFolder, 1, 3)
        Me.tlpMain.Controls.Add(Me.cmdFile, 2, 3)
        Me.tlpMain.Controls.Add(Me.flpButtons, 0, 4)
        Me.tlpMain.Controls.Add(Me.lblName, 0, 0)
        Me.tlpMain.Controls.Add(Me.txtName, 0, 1)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 5
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666!))
        Me.tlpMain.Size = New System.Drawing.Size(354, 178)
        Me.tlpMain.TabIndex = 0
        '
        'lblPath
        '
        Me.lblPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblPath.AutoSize = True
        Me.tlpMain.SetColumnSpan(Me.lblPath, 3)
        Me.lblPath.Location = New System.Drawing.Point(3, 62)
        Me.lblPath.Name = "lblPath"
        Me.lblPath.Size = New System.Drawing.Size(29, 13)
        Me.lblPath.TabIndex = 2
        Me.lblPath.Text = "Path"
        '
        'txtPath
        '
        Me.txtPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
        Me.txtPath.Location = New System.Drawing.Point(3, 80)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(288, 20)
        Me.txtPath.TabIndex = 3
        '
        'cmdFolder
        '
        Me.cmdFolder.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdFolder.AutoSize = True
        Me.cmdFolder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdFolder.Image = Global.Tools.TotalCommanderT.WfxSample.My.Resources.Resources.openHS
        Me.cmdFolder.Location = New System.Drawing.Point(297, 78)
        Me.cmdFolder.Name = "cmdFolder"
        Me.cmdFolder.Padding = New System.Windows.Forms.Padding(1)
        Me.cmdFolder.Size = New System.Drawing.Size(24, 24)
        Me.cmdFolder.TabIndex = 4
        Me.totToolTip.SetToolTip(Me.cmdFolder, "Select folder")
        Me.cmdFolder.UseVisualStyleBackColor = True
        '
        'cmdFile
        '
        Me.cmdFile.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdFile.AutoSize = True
        Me.cmdFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdFile.Image = Global.Tools.TotalCommanderT.WfxSample.My.Resources.Resources.DocumentHS
        Me.cmdFile.Location = New System.Drawing.Point(327, 78)
        Me.cmdFile.Name = "cmdFile"
        Me.cmdFile.Padding = New System.Windows.Forms.Padding(1)
        Me.cmdFile.Size = New System.Drawing.Size(24, 24)
        Me.cmdFile.TabIndex = 5
        Me.totToolTip.SetToolTip(Me.cmdFile, "Select file")
        Me.cmdFile.UseVisualStyleBackColor = True
        '
        'flpButtons
        '
        Me.flpButtons.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.flpButtons.AutoSize = True
        Me.flpButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpMain.SetColumnSpan(Me.flpButtons, 3)
        Me.flpButtons.Controls.Add(Me.cmdOK)
        Me.flpButtons.Controls.Add(Me.cmdCancel)
        Me.flpButtons.Location = New System.Drawing.Point(130, 127)
        Me.flpButtons.Name = "flpButtons"
        Me.flpButtons.Size = New System.Drawing.Size(94, 29)
        Me.flpButtons.TabIndex = 6
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdOK.AutoSize = True
        Me.cmdOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdOK.Location = New System.Drawing.Point(3, 3)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(32, 23)
        Me.cmdOK.TabIndex = 0
        Me.cmdOK.Text = "&OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdCancel.AutoSize = True
        Me.cmdCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(41, 3)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(50, 23)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'lblName
        '
        Me.lblName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(3, 23)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(35, 13)
        Me.lblName.TabIndex = 0
        Me.lblName.Text = "Name"
        '
        'txtName
        '
        Me.txtName.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtName.Location = New System.Drawing.Point(3, 39)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(288, 20)
        Me.txtName.TabIndex = 1
        '
        'ofdFile
        '
        Me.ofdFile.Filter = "All files (*.*)|*.*"
        Me.ofdFile.Title = "Select file"
        '
        'AddFavoriteItemDialog
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(354, 178)
        Me.Controls.Add(Me.tlpMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AddFavoriteItemDialog"
        Me.ShowInTaskbar = False
        Me.Text = "Add favorite item"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.flpButtons.ResumeLayout(False)
        Me.flpButtons.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Private WithEvents lblPath As System.Windows.Forms.Label
    Private WithEvents txtPath As System.Windows.Forms.TextBox
    Private WithEvents cmdFolder As System.Windows.Forms.Button
    Private WithEvents cmdFile As System.Windows.Forms.Button
    Private WithEvents flpButtons As System.Windows.Forms.FlowLayoutPanel
    Private WithEvents cmdOK As System.Windows.Forms.Button
    Private WithEvents cmdCancel As System.Windows.Forms.Button
    Private WithEvents totToolTip As System.Windows.Forms.ToolTip
    Private WithEvents fbdFolder As System.Windows.Forms.FolderBrowserDialog
    Private WithEvents ofdFile As System.Windows.Forms.OpenFileDialog
    Private WithEvents lblName As System.Windows.Forms.Label
    Private WithEvents txtName As System.Windows.Forms.TextBox
End Class
