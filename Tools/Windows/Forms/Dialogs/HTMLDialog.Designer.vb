Namespace WindowsT.FormsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class HTMLDialog
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
            Me.tlpButtons = New System.Windows.Forms.TableLayoutPanel
            Me.cmdOK = New System.Windows.Forms.Button
            Me.webHTML = New System.Windows.Forms.WebBrowser
            Me.tlpButtons.SuspendLayout()
            Me.SuspendLayout()
            '
            'tlpButtons
            '
            Me.tlpButtons.AutoSize = True
            Me.tlpButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.tlpButtons.ColumnCount = 1
            Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
            Me.tlpButtons.Controls.Add(Me.cmdOK, 0, 0)
            Me.tlpButtons.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.tlpButtons.Location = New System.Drawing.Point(0, 454)
            Me.tlpButtons.Name = "tlpButtons"
            Me.tlpButtons.RowCount = 1
            Me.tlpButtons.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpButtons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
            Me.tlpButtons.Size = New System.Drawing.Size(533, 29)
            Me.tlpButtons.TabIndex = 1
            '
            'cmdOK
            '
            Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdOK.AutoSize = True
            Me.cmdOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cmdOK.Location = New System.Drawing.Point(250, 3)
            Me.cmdOK.Name = "cmdOK"
            Me.cmdOK.Size = New System.Drawing.Size(32, 23)
            Me.cmdOK.TabIndex = 0
            Me.cmdOK.Text = "&OK"
            Me.cmdOK.UseVisualStyleBackColor = True
            '
            'webHTML
            '
            Me.webHTML.AllowWebBrowserDrop = False
            Me.webHTML.Dock = System.Windows.Forms.DockStyle.Fill
            Me.webHTML.Location = New System.Drawing.Point(0, 0)
            Me.webHTML.MinimumSize = New System.Drawing.Size(20, 20)
            Me.webHTML.Name = "webHTML"
            Me.webHTML.Size = New System.Drawing.Size(533, 454)
            Me.webHTML.TabIndex = 1
            '
            'HTMLDialog
            '
            Me.AcceptButton = Me.cmdOK
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.cmdOK
            Me.ClientSize = New System.Drawing.Size(533, 483)
            Me.Controls.Add(Me.webHTML)
            Me.Controls.Add(Me.tlpButtons)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "HTMLDialog"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.Text = "HTMLDialog"
            Me.tlpButtons.ResumeLayout(False)
            Me.tlpButtons.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Protected WithEvents tlpButtons As System.Windows.Forms.TableLayoutPanel
        Protected WithEvents cmdOK As System.Windows.Forms.Button
        Protected WithEvents webHTML As System.Windows.Forms.WebBrowser
    End Class
End Namespace