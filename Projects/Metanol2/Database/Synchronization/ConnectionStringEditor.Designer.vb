Namespace Data
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ConnectionStringEditor
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConnectionStringEditor))
            Me.prgProperties = New System.Windows.Forms.PropertyGrid
            Me.txtConnectionString = New System.Windows.Forms.TextBox
            Me.tlpButtons = New System.Windows.Forms.TableLayoutPanel
            Me.cmdClose = New System.Windows.Forms.Button
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
            'txtConnectionString
            '
            Me.txtConnectionString.AccessibleDescription = Nothing
            Me.txtConnectionString.AccessibleName = Nothing
            resources.ApplyResources(Me.txtConnectionString, "txtConnectionString")
            Me.txtConnectionString.BackgroundImage = Nothing
            Me.txtConnectionString.Font = Nothing
            Me.txtConnectionString.Name = "txtConnectionString"
            Me.txtConnectionString.ReadOnly = True
            '
            'tlpButtons
            '
            Me.tlpButtons.AccessibleDescription = Nothing
            Me.tlpButtons.AccessibleName = Nothing
            resources.ApplyResources(Me.tlpButtons, "tlpButtons")
            Me.tlpButtons.BackgroundImage = Nothing
            Me.tlpButtons.Controls.Add(Me.cmdClose, 0, 0)
            Me.tlpButtons.Font = Nothing
            Me.tlpButtons.Name = "tlpButtons"
            '
            'cmdClose
            '
            Me.cmdClose.AccessibleDescription = Nothing
            Me.cmdClose.AccessibleName = Nothing
            resources.ApplyResources(Me.cmdClose, "cmdClose")
            Me.cmdClose.BackgroundImage = Nothing
            Me.cmdClose.Font = Nothing
            Me.cmdClose.Name = "cmdClose"
            Me.cmdClose.UseVisualStyleBackColor = True
            '
            'ConnectionStringEditor
            '
            Me.AcceptButton = Me.cmdClose
            Me.AccessibleDescription = Nothing
            Me.AccessibleName = Nothing
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Nothing
            Me.Controls.Add(Me.prgProperties)
            Me.Controls.Add(Me.txtConnectionString)
            Me.Controls.Add(Me.tlpButtons)
            Me.Font = Nothing
            Me.Icon = Nothing
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "ConnectionStringEditor"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.tlpButtons.ResumeLayout(False)
            Me.tlpButtons.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents prgProperties As System.Windows.Forms.PropertyGrid
        Friend WithEvents txtConnectionString As System.Windows.Forms.TextBox
        Friend WithEvents tlpButtons As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents cmdClose As System.Windows.Forms.Button
    End Class
End Namespace