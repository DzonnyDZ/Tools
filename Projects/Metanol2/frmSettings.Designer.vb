<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings))
        Me.prgProperties = New System.Windows.Forms.PropertyGrid
        Me.tlpButtons = New System.Windows.Forms.TableLayoutPanel
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.tlpButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'prgProperties
        '
        Me.prgProperties.AccessibleDescription = Nothing
        Me.prgProperties.AccessibleName = Nothing
        resources.ApplyResources(Me.prgProperties, "prgProperties")
        Me.prgProperties.BackgroundImage = Nothing
        Me.prgProperties.Font = Nothing
        Me.prgProperties.Name = "prgProperties"
        '
        'tlpButtons
        '
        Me.tlpButtons.AccessibleDescription = Nothing
        Me.tlpButtons.AccessibleName = Nothing
        resources.ApplyResources(Me.tlpButtons, "tlpButtons")
        Me.tlpButtons.BackgroundImage = Nothing
        Me.tlpButtons.Controls.Add(Me.cmdOK, 0, 0)
        Me.tlpButtons.Controls.Add(Me.cmdCancel, 1, 0)
        Me.tlpButtons.Font = Nothing
        Me.tlpButtons.Name = "tlpButtons"
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
        'frmSettings
        '
        Me.AcceptButton = Me.cmdOK
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Nothing
        Me.CancelButton = Me.cmdCancel
        Me.Controls.Add(Me.prgProperties)
        Me.Controls.Add(Me.tlpButtons)
        Me.Font = Nothing
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettings"
        Me.ShowInTaskbar = False
        Me.tlpButtons.ResumeLayout(False)
        Me.tlpButtons.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents prgProperties As System.Windows.Forms.PropertyGrid
    Friend WithEvents tlpButtons As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
End Class
