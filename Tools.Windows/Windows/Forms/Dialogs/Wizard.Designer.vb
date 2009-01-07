Namespace WindowsT.FormsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Wizard
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Wizard))
            Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
            Me.panControlHost = New System.Windows.Forms.Panel
            Me.cmdBack = New System.Windows.Forms.Button
            Me.cmdCancel = New System.Windows.Forms.Button
            Me.cmdNext = New System.Windows.Forms.Button
            Me.tlpMain.SuspendLayout()
            Me.SuspendLayout()
            '
            'tlpMain
            '
            Me.tlpMain.AccessibleDescription = Nothing
            Me.tlpMain.AccessibleName = Nothing
            resources.ApplyResources(Me.tlpMain, "tlpMain")
            Me.tlpMain.BackgroundImage = Nothing
            Me.tlpMain.Controls.Add(Me.panControlHost, 0, 0)
            Me.tlpMain.Controls.Add(Me.cmdBack, 0, 1)
            Me.tlpMain.Controls.Add(Me.cmdCancel, 1, 1)
            Me.tlpMain.Controls.Add(Me.cmdNext, 2, 1)
            Me.tlpMain.Font = Nothing
            Me.tlpMain.Name = "tlpMain"
            '
            'panControlHost
            '
            Me.panControlHost.AccessibleDescription = Nothing
            Me.panControlHost.AccessibleName = Nothing
            resources.ApplyResources(Me.panControlHost, "panControlHost")
            Me.panControlHost.BackgroundImage = Nothing
            Me.tlpMain.SetColumnSpan(Me.panControlHost, 3)
            Me.panControlHost.Font = Nothing
            Me.panControlHost.Name = "panControlHost"
            '
            'cmdBack
            '
            Me.cmdBack.AccessibleDescription = Nothing
            Me.cmdBack.AccessibleName = Nothing
            resources.ApplyResources(Me.cmdBack, "cmdBack")
            Me.cmdBack.BackgroundImage = Nothing
            Me.cmdBack.Font = Nothing
            Me.cmdBack.Name = "cmdBack"
            Me.cmdBack.UseVisualStyleBackColor = True
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
            'cmdNext
            '
            Me.cmdNext.AccessibleDescription = Nothing
            Me.cmdNext.AccessibleName = Nothing
            resources.ApplyResources(Me.cmdNext, "cmdNext")
            Me.cmdNext.BackgroundImage = Nothing
            Me.cmdNext.Font = Nothing
            Me.cmdNext.Name = "cmdNext"
            Me.cmdNext.UseVisualStyleBackColor = True
            '
            'Wizard
            '
            Me.AcceptButton = Me.cmdNext
            Me.AccessibleDescription = Nothing
            Me.AccessibleName = Nothing
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Nothing
            Me.CancelButton = Me.cmdCancel
            Me.Controls.Add(Me.tlpMain)
            Me.Font = Nothing
            Me.Icon = Nothing
            Me.Name = "Wizard"
            Me.tlpMain.ResumeLayout(False)
            Me.tlpMain.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Protected WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
        Protected WithEvents panControlHost As System.Windows.Forms.Panel
        Protected WithEvents cmdBack As System.Windows.Forms.Button
        Protected WithEvents cmdCancel As System.Windows.Forms.Button
        Protected WithEvents cmdNext As System.Windows.Forms.Button
    End Class
End Namespace