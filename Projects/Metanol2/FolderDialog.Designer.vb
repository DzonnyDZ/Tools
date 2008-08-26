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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFolderDialog))
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
        Me.tlpMain.AccessibleDescription = Nothing
        Me.tlpMain.AccessibleName = Nothing
        resources.ApplyResources(Me.tlpMain, "tlpMain")
        Me.tlpMain.BackgroundImage = Nothing
        Me.tlpMain.Controls.Add(Me.lblI, 0, 0)
        Me.tlpMain.Controls.Add(Me.txtPath, 0, 1)
        Me.tlpMain.Controls.Add(Me.cmdPath, 1, 1)
        Me.tlpMain.Controls.Add(Me.tmpButtons, 0, 2)
        Me.tlpMain.Font = Nothing
        Me.tlpMain.Name = "tlpMain"
        '
        'lblI
        '
        Me.lblI.AccessibleDescription = Nothing
        Me.lblI.AccessibleName = Nothing
        resources.ApplyResources(Me.lblI, "lblI")
        Me.tlpMain.SetColumnSpan(Me.lblI, 2)
        Me.lblI.Font = Nothing
        Me.lblI.Name = "lblI"
        '
        'txtPath
        '
        Me.txtPath.AccessibleDescription = Nothing
        Me.txtPath.AccessibleName = Nothing
        resources.ApplyResources(Me.txtPath, "txtPath")
        Me.txtPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
        Me.txtPath.BackgroundImage = Nothing
        Me.txtPath.Font = Nothing
        Me.txtPath.Name = "txtPath"
        '
        'cmdPath
        '
        Me.cmdPath.AccessibleDescription = Nothing
        Me.cmdPath.AccessibleName = Nothing
        resources.ApplyResources(Me.cmdPath, "cmdPath")
        Me.cmdPath.BackgroundImage = Nothing
        Me.cmdPath.Font = Nothing
        Me.cmdPath.Name = "cmdPath"
        Me.cmdPath.UseVisualStyleBackColor = True
        '
        'tmpButtons
        '
        Me.tmpButtons.AccessibleDescription = Nothing
        Me.tmpButtons.AccessibleName = Nothing
        resources.ApplyResources(Me.tmpButtons, "tmpButtons")
        Me.tmpButtons.BackgroundImage = Nothing
        Me.tlpMain.SetColumnSpan(Me.tmpButtons, 2)
        Me.tmpButtons.Controls.Add(Me.cmdOK, 0, 0)
        Me.tmpButtons.Controls.Add(Me.cmdCancel, 1, 0)
        Me.tmpButtons.Font = Nothing
        Me.tmpButtons.Name = "tmpButtons"
        '
        'cmdOK
        '
        Me.cmdOK.AccessibleDescription = Nothing
        Me.cmdOK.AccessibleName = Nothing
        resources.ApplyResources(Me.cmdOK, "cmdOK")
        Me.cmdOK.BackgroundImage = Nothing
        Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOK.Font = Nothing
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.AccessibleDescription = Nothing
        Me.cmdCancel.AccessibleName = Nothing
        resources.ApplyResources(Me.cmdCancel, "cmdCancel")
        Me.cmdCancel.BackgroundImage = Nothing
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = Nothing
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'fbdBrowse
        '
        resources.ApplyResources(Me.fbdBrowse, "fbdBrowse")
        '
        'frmFolderDialog
        '
        Me.AcceptButton = Me.cmdOK
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Nothing
        Me.CancelButton = Me.cmdCancel
        Me.Controls.Add(Me.tlpMain)
        Me.Font = Nothing
        Me.Icon = Nothing
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFolderDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
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
