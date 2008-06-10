Namespace Login
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmGroupOverride
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGroupOverride))
            Me.clbGroups = New System.Windows.Forms.CheckedListBox
            Me.lblI = New System.Windows.Forms.Label
            Me.tlpButtons = New System.Windows.Forms.TableLayoutPanel
            Me.cmdOK = New System.Windows.Forms.Button
            Me.cmdKO = New System.Windows.Forms.Button
            Me.tlpButtons.SuspendLayout()
            Me.SuspendLayout()
            '
            'clbGroups
            '
            Me.clbGroups.Dock = System.Windows.Forms.DockStyle.Fill
            Me.clbGroups.FormattingEnabled = True
            Me.clbGroups.Location = New System.Drawing.Point(0, 13)
            Me.clbGroups.Name = "clbGroups"
            Me.clbGroups.Size = New System.Drawing.Size(481, 319)
            Me.clbGroups.TabIndex = 0
            '
            'lblI
            '
            Me.lblI.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblI.Location = New System.Drawing.Point(0, 0)
            Me.lblI.Name = "lblI"
            Me.lblI.Size = New System.Drawing.Size(481, 13)
            Me.lblI.TabIndex = 1
            Me.lblI.Text = "Pro testovací úèely mùžete pro aktuální relaci zrušit svoje èlensví v nìktrých sk" & _
                "upinách"
            Me.lblI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'tlpButtons
            '
            Me.tlpButtons.AutoSize = True
            Me.tlpButtons.ColumnCount = 2
            Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpButtons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.tlpButtons.Controls.Add(Me.cmdOK, 0, 0)
            Me.tlpButtons.Controls.Add(Me.cmdKO, 1, 0)
            Me.tlpButtons.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.tlpButtons.Location = New System.Drawing.Point(0, 345)
            Me.tlpButtons.Name = "tlpButtons"
            Me.tlpButtons.RowCount = 1
            Me.tlpButtons.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.tlpButtons.Size = New System.Drawing.Size(481, 29)
            Me.tlpButtons.TabIndex = 2
            '
            'cmdOK
            '
            Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Right
            Me.cmdOK.AutoSize = True
            Me.cmdOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdOK.Location = New System.Drawing.Point(205, 3)
            Me.cmdOK.Name = "cmdOK"
            Me.cmdOK.Size = New System.Drawing.Size(32, 23)
            Me.cmdOK.TabIndex = 0
            Me.cmdOK.Text = "&OK"
            Me.cmdOK.UseVisualStyleBackColor = True
            '
            'cmdKO
            '
            Me.cmdKO.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.cmdKO.AutoSize = True
            Me.cmdKO.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdKO.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cmdKO.Location = New System.Drawing.Point(243, 3)
            Me.cmdKO.Name = "cmdKO"
            Me.cmdKO.Size = New System.Drawing.Size(48, 23)
            Me.cmdKO.TabIndex = 1
            Me.cmdKO.Text = "&Storno"
            Me.cmdKO.UseVisualStyleBackColor = True
            '
            'frmGroupOverride
            '
            Me.AcceptButton = Me.cmdOK
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.cmdKO
            Me.ClientSize = New System.Drawing.Size(481, 374)
            Me.Controls.Add(Me.clbGroups)
            Me.Controls.Add(Me.tlpButtons)
            Me.Controls.Add(Me.lblI)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmGroupOverride"
            Me.ShowInTaskbar = False
            Me.Text = "Odhlásit se ze skupin"
            Me.tlpButtons.ResumeLayout(False)
            Me.tlpButtons.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents clbGroups As System.Windows.Forms.CheckedListBox
        Friend WithEvents lblI As System.Windows.Forms.Label
        Friend WithEvents tlpButtons As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents cmdOK As System.Windows.Forms.Button
        Friend WithEvents cmdKO As System.Windows.Forms.Button
    End Class
End Namespace