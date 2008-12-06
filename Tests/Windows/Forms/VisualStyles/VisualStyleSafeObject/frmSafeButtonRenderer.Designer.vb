Namespace WindowsT.FormsT.VisualStylesT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmSafeButtonRenderer
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
            Me.chkVisualStyle = New System.Windows.Forms.CheckBox
            Me.cmdTest = New CustomButton
            Me.SuspendLayout()
            '
            'chkVisualStyle
            '
            Me.chkVisualStyle.AutoSize = True
            Me.chkVisualStyle.Location = New System.Drawing.Point(12, 12)
            Me.chkVisualStyle.Name = "chkVisualStyle"
            Me.chkVisualStyle.Size = New System.Drawing.Size(107, 17)
            Me.chkVisualStyle.TabIndex = 0
            Me.chkVisualStyle.Text = "Use Visual Styles"
            Me.chkVisualStyle.UseVisualStyleBackColor = True
            '
            'cmdTest
            '
            Me.cmdTest.Location = New System.Drawing.Point(12, 35)
            Me.cmdTest.Name = "cmdTest"
            Me.cmdTest.Size = New System.Drawing.Size(92, 47)
            Me.cmdTest.TabIndex = 1
            Me.cmdTest.Text = "Test" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
            Me.cmdTest.UseVisualStyleBackColor = True
            '
            'frmSafeButtonRenderer
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(647, 337)
            Me.Controls.Add(Me.cmdTest)
            Me.Controls.Add(Me.chkVisualStyle)
            Me.Name = "frmSafeButtonRenderer"
            Me.Text = "Testiong Tools.WindowsT.FormsT.VisualStylesT.VisualStyleSafeObject.afeButtonRende" & _
                "rer"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents chkVisualStyle As System.Windows.Forms.CheckBox
        Private WithEvents cmdTest As CustomButton
    End Class
End Namespace