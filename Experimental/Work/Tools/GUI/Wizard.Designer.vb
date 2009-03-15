Namespace GUI
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
            Me.tlpMain.ColumnCount = 3
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
            Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
            Me.tlpMain.Controls.Add(Me.panControlHost, 0, 0)
            Me.tlpMain.Controls.Add(Me.cmdBack, 0, 1)
            Me.tlpMain.Controls.Add(Me.cmdCancel, 1, 1)
            Me.tlpMain.Controls.Add(Me.cmdNext, 2, 1)
            Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tlpMain.Location = New System.Drawing.Point(0, 0)
            Me.tlpMain.Margin = New System.Windows.Forms.Padding(0)
            Me.tlpMain.Name = "tlpMain"
            Me.tlpMain.RowCount = 2
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpMain.Size = New System.Drawing.Size(470, 279)
            Me.tlpMain.TabIndex = 0
            '
            'panControlHost
            '
            Me.tlpMain.SetColumnSpan(Me.panControlHost, 3)
            Me.panControlHost.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panControlHost.Location = New System.Drawing.Point(0, 0)
            Me.panControlHost.Margin = New System.Windows.Forms.Padding(0)
            Me.panControlHost.Name = "panControlHost"
            Me.panControlHost.Size = New System.Drawing.Size(470, 250)
            Me.panControlHost.TabIndex = 0
            '
            'cmdBack
            '
            Me.cmdBack.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdBack.AutoSize = True
            Me.cmdBack.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdBack.Location = New System.Drawing.Point(54, 253)
            Me.cmdBack.Name = "cmdBack"
            Me.cmdBack.Size = New System.Drawing.Size(48, 23)
            Me.cmdBack.TabIndex = 1
            Me.cmdBack.Text = "< &Zpět"
            Me.cmdBack.UseVisualStyleBackColor = True
            '
            'cmdCancel
            '
            Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdCancel.AutoSize = True
            Me.cmdCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cmdCancel.Location = New System.Drawing.Point(210, 253)
            Me.cmdCancel.Name = "cmdCancel"
            Me.cmdCancel.Size = New System.Drawing.Size(48, 23)
            Me.cmdCancel.TabIndex = 2
            Me.cmdCancel.Text = "&Storno"
            Me.cmdCancel.UseVisualStyleBackColor = True
            '
            'cmdNext
            '
            Me.cmdNext.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdNext.AutoSize = True
            Me.cmdNext.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdNext.Location = New System.Drawing.Point(365, 253)
            Me.cmdNext.Name = "cmdNext"
            Me.cmdNext.Size = New System.Drawing.Size(51, 23)
            Me.cmdNext.TabIndex = 3
            Me.cmdNext.Text = "&Další >"
            Me.cmdNext.UseVisualStyleBackColor = True
            '
            'Wizard
            '
            Me.AcceptButton = Me.cmdNext
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.cmdCancel
            Me.ClientSize = New System.Drawing.Size(470, 279)
            Me.Controls.Add(Me.tlpMain)
            Me.Name = "Wizard"
            Me.Text = "Wizard"
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