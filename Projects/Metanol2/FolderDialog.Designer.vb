<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFolderDialog
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
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
        Me.lblI = New System.Windows.Forms.Label
        Me.txtPath = New System.Windows.Forms.TextBox
        Me.cmdPath = New System.Windows.Forms.Button
        Me.tmpButtons = New System.Windows.Forms.TableLayoutPanel
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.fbdBrowse = New System.Windows.Forms.FolderBrowserDialog
        Me.tlpMain.SuspendLayout()
        Me.tmpButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.AutoSize = True
        Me.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpMain.ColumnCount = 2
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpMain.Controls.Add(Me.lblI, 0, 0)
        Me.tlpMain.Controls.Add(Me.txtPath, 0, 1)
        Me.tlpMain.Controls.Add(Me.cmdPath, 1, 1)
        Me.tlpMain.Controls.Add(Me.tmpButtons, 0, 2)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 3
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.Size = New System.Drawing.Size(354, 94)
        Me.tlpMain.TabIndex = 0
        '
        'lblI
        '
        Me.lblI.AutoSize = True
        Me.tlpMain.SetColumnSpan(Me.lblI, 2)
        Me.lblI.Location = New System.Drawing.Point(3, 0)
        Me.lblI.Name = "lblI"
        Me.lblI.Size = New System.Drawing.Size(66, 13)
        Me.lblI.TabIndex = 0
        Me.lblI.Text = "Select folder"
        '
        'txtPath
        '
        Me.txtPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
        Me.txtPath.Location = New System.Drawing.Point(3, 17)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(316, 20)
        Me.txtPath.TabIndex = 1
        '
        'cmdPath
        '
        Me.cmdPath.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdPath.AutoSize = True
        Me.cmdPath.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdPath.Location = New System.Drawing.Point(325, 16)
        Me.cmdPath.Name = "cmdPath"
        Me.cmdPath.Size = New System.Drawing.Size(26, 23)
        Me.cmdPath.TabIndex = 2
        Me.cmdPath.Text = "..."
        Me.cmdPath.UseVisualStyleBackColor = True
        '
        'tmpButtons
        '
        Me.tmpButtons.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tmpButtons.AutoSize = True
        Me.tmpButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tmpButtons.ColumnCount = 2
        Me.tlpMain.SetColumnSpan(Me.tmpButtons, 2)
        Me.tmpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tmpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tmpButtons.Controls.Add(Me.cmdOK, 0, 0)
        Me.tmpButtons.Controls.Add(Me.cmdCancel, 1, 0)
        Me.tmpButtons.Location = New System.Drawing.Point(3, 53)
        Me.tmpButtons.Name = "tmpButtons"
        Me.tmpButtons.RowCount = 1
        Me.tmpButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tmpButtons.Size = New System.Drawing.Size(348, 29)
        Me.tmpButtons.TabIndex = 4
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdOK.AutoSize = True
        Me.cmdOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOK.Location = New System.Drawing.Point(3, 3)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(32, 23)
        Me.cmdOK.TabIndex = 0
        Me.cmdOK.Text = "&OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cmdCancel.AutoSize = True
        Me.cmdCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(295, 3)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(50, 23)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'frmFolderDialog
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(354, 94)
        Me.Controls.Add(Me.tlpMain)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFolderDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Select folder"
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.tmpButtons.ResumeLayout(False)
        Me.tmpButtons.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblI As System.Windows.Forms.Label
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents cmdPath As System.Windows.Forms.Button
    Friend WithEvents tmpButtons As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents fbdBrowse As System.Windows.Forms.FolderBrowserDialog
End Class
