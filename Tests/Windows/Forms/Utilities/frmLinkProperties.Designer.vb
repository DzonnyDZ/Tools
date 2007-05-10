Namespace Windows.Forms.Utilities
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmLinkProperties
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
            Me.lblLink = New System.Windows.Forms.Label
            Me.lblVisited = New System.Windows.Forms.Label
            Me.fraToolsWindowsFormsUtilitiesLinkProperites = New System.Windows.Forms.GroupBox
            Me.fraToolsDrawingSystemColorsExtension = New System.Windows.Forms.GroupBox
            Me.lblVisited2 = New System.Windows.Forms.Label
            Me.lblLink2 = New System.Windows.Forms.Label
            Me.fraToolsWindowsFormsUtilitiesLinkProperites.SuspendLayout()
            Me.fraToolsDrawingSystemColorsExtension.SuspendLayout()
            Me.SuspendLayout()
            '
            'lblLink
            '
            Me.lblLink.AutoSize = True
            Me.lblLink.Location = New System.Drawing.Point(6, 16)
            Me.lblLink.Name = "lblLink"
            Me.lblLink.Size = New System.Drawing.Size(56, 13)
            Me.lblLink.TabIndex = 0
            Me.lblLink.Text = "This is link"
            '
            'lblVisited
            '
            Me.lblVisited.AutoSize = True
            Me.lblVisited.Location = New System.Drawing.Point(6, 38)
            Me.lblVisited.Name = "lblVisited"
            Me.lblVisited.Size = New System.Drawing.Size(89, 13)
            Me.lblVisited.TabIndex = 0
            Me.lblVisited.Text = "This is visited link"
            '
            'fraToolsWindowsFormsUtilitiesLinkProperites
            '
            Me.fraToolsWindowsFormsUtilitiesLinkProperites.Controls.Add(Me.lblLink)
            Me.fraToolsWindowsFormsUtilitiesLinkProperites.Controls.Add(Me.lblVisited)
            Me.fraToolsWindowsFormsUtilitiesLinkProperites.Location = New System.Drawing.Point(3, 1)
            Me.fraToolsWindowsFormsUtilitiesLinkProperites.Name = "fraToolsWindowsFormsUtilitiesLinkProperites"
            Me.fraToolsWindowsFormsUtilitiesLinkProperites.Size = New System.Drawing.Size(237, 61)
            Me.fraToolsWindowsFormsUtilitiesLinkProperites.TabIndex = 1
            Me.fraToolsWindowsFormsUtilitiesLinkProperites.TabStop = False
            Me.fraToolsWindowsFormsUtilitiesLinkProperites.Text = "Tools.Windows.Forms.Utilities.LinkProperties"
            '
            'fraToolsDrawingSystemColorsExtension
            '
            Me.fraToolsDrawingSystemColorsExtension.Controls.Add(Me.lblLink2)
            Me.fraToolsDrawingSystemColorsExtension.Controls.Add(Me.lblVisited2)
            Me.fraToolsDrawingSystemColorsExtension.Location = New System.Drawing.Point(246, 6)
            Me.fraToolsDrawingSystemColorsExtension.Name = "fraToolsDrawingSystemColorsExtension"
            Me.fraToolsDrawingSystemColorsExtension.Size = New System.Drawing.Size(235, 56)
            Me.fraToolsDrawingSystemColorsExtension.TabIndex = 2
            Me.fraToolsDrawingSystemColorsExtension.TabStop = False
            Me.fraToolsDrawingSystemColorsExtension.Text = "Tools.Drawing.SystemColorsExtension"
            '
            'lblVisited2
            '
            Me.lblVisited2.AutoSize = True
            Me.lblVisited2.Location = New System.Drawing.Point(6, 38)
            Me.lblVisited2.Name = "lblVisited2"
            Me.lblVisited2.Size = New System.Drawing.Size(89, 13)
            Me.lblVisited2.TabIndex = 0
            Me.lblVisited2.Text = "This is visited link"
            '
            'lblLink2
            '
            Me.lblLink2.AutoSize = True
            Me.lblLink2.Location = New System.Drawing.Point(6, 16)
            Me.lblLink2.Name = "lblLink2"
            Me.lblLink2.Size = New System.Drawing.Size(56, 13)
            Me.lblLink2.TabIndex = 0
            Me.lblLink2.Text = "This is link"
            '
            'frmLinkProperties
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(486, 71)
            Me.Controls.Add(Me.fraToolsDrawingSystemColorsExtension)
            Me.Controls.Add(Me.fraToolsWindowsFormsUtilitiesLinkProperites)
            Me.Name = "frmLinkProperties"
            Me.Text = "Testing Tools.Windows.Forms.Utilities.LinkProperties"
            Me.fraToolsWindowsFormsUtilitiesLinkProperites.ResumeLayout(False)
            Me.fraToolsWindowsFormsUtilitiesLinkProperites.PerformLayout()
            Me.fraToolsDrawingSystemColorsExtension.ResumeLayout(False)
            Me.fraToolsDrawingSystemColorsExtension.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents lblLink As System.Windows.Forms.Label
        Friend WithEvents lblVisited As System.Windows.Forms.Label
        Friend WithEvents fraToolsWindowsFormsUtilitiesLinkProperites As System.Windows.Forms.GroupBox
        Friend WithEvents fraToolsDrawingSystemColorsExtension As System.Windows.Forms.GroupBox
        Friend WithEvents lblLink2 As System.Windows.Forms.Label
        Friend WithEvents lblVisited2 As System.Windows.Forms.Label
    End Class
End Namespace