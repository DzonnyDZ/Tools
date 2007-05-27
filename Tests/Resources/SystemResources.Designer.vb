Namespace ResourcesT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmSystemResources
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
            Me.txtResources = New System.Windows.Forms.TextBox
            Me.panResources = New System.Windows.Forms.Panel
            Me.lblKeyValue = New System.Windows.Forms.Label
            Me.cmdKey = New System.Windows.Forms.Button
            Me.lblSR = New System.Windows.Forms.Label
            Me.txtKey = New System.Windows.Forms.TextBox
            Me.cmdGet = New System.Windows.Forms.Button
            Me.txtResourceName = New System.Windows.Forms.TextBox
            Me.cmdAll = New System.Windows.Forms.Button
            Me.panResources.SuspendLayout()
            Me.SuspendLayout()
            '
            'txtResources
            '
            Me.txtResources.Dock = System.Windows.Forms.DockStyle.Fill
            Me.txtResources.Location = New System.Drawing.Point(0, 0)
            Me.txtResources.Multiline = True
            Me.txtResources.Name = "txtResources"
            Me.txtResources.ReadOnly = True
            Me.txtResources.ScrollBars = System.Windows.Forms.ScrollBars.Both
            Me.txtResources.Size = New System.Drawing.Size(371, 409)
            Me.txtResources.TabIndex = 0
            '
            'panResources
            '
            Me.panResources.Controls.Add(Me.cmdAll)
            Me.panResources.Controls.Add(Me.lblKeyValue)
            Me.panResources.Controls.Add(Me.cmdKey)
            Me.panResources.Controls.Add(Me.lblSR)
            Me.panResources.Controls.Add(Me.txtKey)
            Me.panResources.Controls.Add(Me.cmdGet)
            Me.panResources.Controls.Add(Me.txtResourceName)
            Me.panResources.Dock = System.Windows.Forms.DockStyle.Right
            Me.panResources.Location = New System.Drawing.Point(371, 0)
            Me.panResources.Name = "panResources"
            Me.panResources.Size = New System.Drawing.Size(200, 409)
            Me.panResources.TabIndex = 1
            '
            'lblKeyValue
            '
            Me.lblKeyValue.AutoSize = True
            Me.lblKeyValue.Location = New System.Drawing.Point(3, 46)
            Me.lblKeyValue.Name = "lblKeyValue"
            Me.lblKeyValue.Size = New System.Drawing.Size(54, 13)
            Me.lblKeyValue.TabIndex = 2
            Me.lblKeyValue.Text = "Key value"
            '
            'cmdKey
            '
            Me.cmdKey.Location = New System.Drawing.Point(113, 59)
            Me.cmdKey.Name = "cmdKey"
            Me.cmdKey.Size = New System.Drawing.Size(75, 23)
            Me.cmdKey.TabIndex = 1
            Me.cmdKey.Text = "&Get"
            Me.cmdKey.UseVisualStyleBackColor = True
            '
            'lblSR
            '
            Me.lblSR.AutoSize = True
            Me.lblSR.Location = New System.Drawing.Point(3, 7)
            Me.lblSR.Name = "lblSR"
            Me.lblSR.Size = New System.Drawing.Size(85, 13)
            Me.lblSR.TabIndex = 2
            Me.lblSR.Text = "System resource"
            '
            'txtKey
            '
            Me.txtKey.Location = New System.Drawing.Point(0, 62)
            Me.txtKey.Name = "txtKey"
            Me.txtKey.Size = New System.Drawing.Size(100, 20)
            Me.txtKey.TabIndex = 0
            '
            'cmdGet
            '
            Me.cmdGet.Location = New System.Drawing.Point(113, 20)
            Me.cmdGet.Name = "cmdGet"
            Me.cmdGet.Size = New System.Drawing.Size(75, 23)
            Me.cmdGet.TabIndex = 1
            Me.cmdGet.Text = "&Get"
            Me.cmdGet.UseVisualStyleBackColor = True
            '
            'txtResourceName
            '
            Me.txtResourceName.Location = New System.Drawing.Point(0, 23)
            Me.txtResourceName.Name = "txtResourceName"
            Me.txtResourceName.Size = New System.Drawing.Size(100, 20)
            Me.txtResourceName.TabIndex = 0
            '
            'cmdAll
            '
            Me.cmdAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.cmdAll.Location = New System.Drawing.Point(6, 379)
            Me.cmdAll.Name = "cmdAll"
            Me.cmdAll.Size = New System.Drawing.Size(61, 27)
            Me.cmdAll.TabIndex = 3
            Me.cmdAll.Text = "All"
            Me.cmdAll.UseVisualStyleBackColor = True
            '
            'frmSystemResources
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(571, 409)
            Me.Controls.Add(Me.txtResources)
            Me.Controls.Add(Me.panResources)
            Me.Name = "frmSystemResources"
            Me.Text = "Testing Tools.Resources.SystemResources"
            Me.panResources.ResumeLayout(False)
            Me.panResources.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents txtResources As System.Windows.Forms.TextBox
        Friend WithEvents panResources As System.Windows.Forms.Panel
        Friend WithEvents cmdGet As System.Windows.Forms.Button
        Friend WithEvents txtResourceName As System.Windows.Forms.TextBox
        Friend WithEvents lblKeyValue As System.Windows.Forms.Label
        Friend WithEvents cmdKey As System.Windows.Forms.Button
        Friend WithEvents lblSR As System.Windows.Forms.Label
        Friend WithEvents txtKey As System.Windows.Forms.TextBox
        Friend WithEvents cmdAll As System.Windows.Forms.Button
    End Class
End Namespace