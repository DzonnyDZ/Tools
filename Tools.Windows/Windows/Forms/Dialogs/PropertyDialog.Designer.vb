Namespace WindowsT.FormsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class PropertyDialog
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PropertyDialog))
            Me.prgProperties = New System.Windows.Forms.PropertyGrid()
            Me.cmdOK = New System.Windows.Forms.Button()
            Me.cmdCancel = New System.Windows.Forms.Button()
            Me.flpButtons = New System.Windows.Forms.FlowLayoutPanel()
            Me.tlpButtons = New System.Windows.Forms.TableLayoutPanel()
            Me.flpButtons.SuspendLayout()
            Me.tlpButtons.SuspendLayout()
            Me.SuspendLayout()
            '
            'prgProperties
            '
            resources.ApplyResources(Me.prgProperties, "prgProperties")
            Me.prgProperties.Name = "prgProperties"
            '
            'cmdOK
            '
            resources.ApplyResources(Me.cmdOK, "cmdOK")
            Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.cmdOK.Name = "cmdOK"
            Me.cmdOK.UseVisualStyleBackColor = True
            '
            'cmdCancel
            '
            resources.ApplyResources(Me.cmdCancel, "cmdCancel")
            Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cmdCancel.Name = "cmdCancel"
            Me.cmdCancel.UseVisualStyleBackColor = True
            '
            'flpButtons
            '
            resources.ApplyResources(Me.flpButtons, "flpButtons")
            Me.flpButtons.Controls.Add(Me.cmdOK)
            Me.flpButtons.Controls.Add(Me.cmdCancel)
            Me.flpButtons.Name = "flpButtons"
            '
            'tlpButtons
            '
            resources.ApplyResources(Me.tlpButtons, "tlpButtons")
            Me.tlpButtons.Controls.Add(Me.flpButtons, 0, 0)
            Me.tlpButtons.Name = "tlpButtons"
            '
            'PropertyDialog
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.prgProperties)
            Me.Controls.Add(Me.tlpButtons)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "PropertyDialog"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.flpButtons.ResumeLayout(False)
            Me.flpButtons.PerformLayout()
            Me.tlpButtons.ResumeLayout(False)
            Me.tlpButtons.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents prgProperties As System.Windows.Forms.PropertyGrid
        Friend WithEvents cmdOK As System.Windows.Forms.Button
        Friend WithEvents cmdCancel As System.Windows.Forms.Button
        Friend WithEvents flpButtons As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents tlpButtons As System.Windows.Forms.TableLayoutPanel
    End Class
End Namespace