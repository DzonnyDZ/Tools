<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderByEditor
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
        Me.lstLeft = New System.Windows.Forms.ListBox
        Me.lstRight = New System.Windows.Forms.ListBox
        Me.cmdUp = New System.Windows.Forms.Button
        Me.cmdLeft = New System.Windows.Forms.Button
        Me.cmdRight = New System.Windows.Forms.Button
        Me.cmdDown = New System.Windows.Forms.Button
        Me.cmdChangeDir = New System.Windows.Forms.Button
        Me.lblDostupne = New System.Windows.Forms.Label
        Me.lblPouzite = New System.Windows.Forms.Label
        Me.lblI = New System.Windows.Forms.Label
        Me.totToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.tlpMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 3
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpMain.Controls.Add(Me.lstLeft, 0, 1)
        Me.tlpMain.Controls.Add(Me.lstRight, 2, 1)
        Me.tlpMain.Controls.Add(Me.cmdUp, 1, 1)
        Me.tlpMain.Controls.Add(Me.cmdLeft, 1, 4)
        Me.tlpMain.Controls.Add(Me.cmdRight, 1, 3)
        Me.tlpMain.Controls.Add(Me.cmdDown, 1, 5)
        Me.tlpMain.Controls.Add(Me.cmdChangeDir, 1, 2)
        Me.tlpMain.Controls.Add(Me.lblDostupne, 0, 0)
        Me.tlpMain.Controls.Add(Me.lblPouzite, 2, 0)
        Me.tlpMain.Controls.Add(Me.lblI, 2, 6)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 6
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.94872!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.94872!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.64103!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.64103!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.82051!))
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.Size = New System.Drawing.Size(384, 314)
        Me.tlpMain.TabIndex = 0
        '
        'lstLeft
        '
        Me.lstLeft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstLeft.FormattingEnabled = True
        Me.lstLeft.ItemHeight = 15
        Me.lstLeft.Location = New System.Drawing.Point(3, 18)
        Me.lstLeft.Name = "lstLeft"
        Me.tlpMain.SetRowSpan(Me.lstLeft, 6)
        Me.lstLeft.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstLeft.Size = New System.Drawing.Size(165, 289)
        Me.lstLeft.TabIndex = 0
        Me.totToolTip.SetToolTip(Me.lstLeft, "Dostupné sloupce")
        '
        'lstRight
        '
        Me.lstRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstRight.FormattingEnabled = True
        Me.lstRight.ItemHeight = 15
        Me.lstRight.Location = New System.Drawing.Point(216, 18)
        Me.lstRight.Name = "lstRight"
        Me.tlpMain.SetRowSpan(Me.lstRight, 5)
        Me.lstRight.Size = New System.Drawing.Size(165, 274)
        Me.lstRight.TabIndex = 6
        Me.totToolTip.SetToolTip(Me.lstRight, "Sloupce použité k řazení")
        '
        'cmdUp
        '
        Me.cmdUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdUp.AutoSize = True
        Me.cmdUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdUp.Enabled = False
        Me.cmdUp.Font = New System.Drawing.Font("Arial Unicode MS", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cmdUp.Location = New System.Drawing.Point(174, 18)
        Me.cmdUp.Name = "cmdUp"
        Me.cmdUp.Size = New System.Drawing.Size(36, 35)
        Me.cmdUp.TabIndex = 1
        Me.cmdUp.Text = "⇑"
        Me.totToolTip.SetToolTip(Me.cmdUp, "Zvýšit prioritu řazení" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[Ctrl+Up]")
        Me.cmdUp.UseVisualStyleBackColor = True
        '
        'cmdLeft
        '
        Me.cmdLeft.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdLeft.AutoSize = True
        Me.cmdLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdLeft.Enabled = False
        Me.cmdLeft.Font = New System.Drawing.Font("Arial Unicode MS", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cmdLeft.Location = New System.Drawing.Point(174, 190)
        Me.cmdLeft.Name = "cmdLeft"
        Me.cmdLeft.Size = New System.Drawing.Size(36, 35)
        Me.cmdLeft.TabIndex = 4
        Me.cmdLeft.Text = "⇐"
        Me.totToolTip.SetToolTip(Me.cmdLeft, "Odebrat vybraný sloupec" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[Del]")
        Me.cmdLeft.UseVisualStyleBackColor = True
        '
        'cmdRight
        '
        Me.cmdRight.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdRight.AutoSize = True
        Me.cmdRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdRight.Enabled = False
        Me.cmdRight.Font = New System.Drawing.Font("Arial Unicode MS", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cmdRight.Location = New System.Drawing.Point(174, 149)
        Me.cmdRight.Name = "cmdRight"
        Me.cmdRight.Size = New System.Drawing.Size(36, 35)
        Me.cmdRight.TabIndex = 3
        Me.cmdRight.Text = "⇒"
        Me.totToolTip.SetToolTip(Me.cmdRight, "Přidat vybrané sloupce" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[Enter]")
        Me.cmdRight.UseVisualStyleBackColor = True
        '
        'cmdDown
        '
        Me.cmdDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDown.AutoSize = True
        Me.cmdDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdDown.Enabled = False
        Me.cmdDown.Font = New System.Drawing.Font("Arial Unicode MS", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cmdDown.Location = New System.Drawing.Point(174, 276)
        Me.cmdDown.Name = "cmdDown"
        Me.tlpMain.SetRowSpan(Me.cmdDown, 2)
        Me.cmdDown.Size = New System.Drawing.Size(36, 35)
        Me.cmdDown.TabIndex = 5
        Me.cmdDown.Text = "⇓"
        Me.totToolTip.SetToolTip(Me.cmdDown, "Snížit prioritu řazení" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[Ctrl+Dn]")
        Me.cmdDown.UseVisualStyleBackColor = True
        '
        'cmdChangeDir
        '
        Me.cmdChangeDir.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cmdChangeDir.AutoSize = True
        Me.cmdChangeDir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdChangeDir.Enabled = False
        Me.cmdChangeDir.Font = New System.Drawing.Font("Arial Unicode MS", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.cmdChangeDir.Location = New System.Drawing.Point(174, 72)
        Me.cmdChangeDir.Name = "cmdChangeDir"
        Me.cmdChangeDir.Size = New System.Drawing.Size(36, 35)
        Me.cmdChangeDir.TabIndex = 2
        Me.cmdChangeDir.Text = "⇅"
        Me.totToolTip.SetToolTip(Me.cmdChangeDir, "Změnit směr řazení" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "[Ins]")
        Me.cmdChangeDir.UseVisualStyleBackColor = True
        '
        'lblDostupne
        '
        Me.lblDostupne.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDostupne.AutoSize = True
        Me.lblDostupne.Location = New System.Drawing.Point(3, 0)
        Me.lblDostupne.Name = "lblDostupne"
        Me.lblDostupne.Size = New System.Drawing.Size(165, 15)
        Me.lblDostupne.TabIndex = 7
        Me.lblDostupne.Text = "Dostupné sloupce"
        Me.lblDostupne.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPouzite
        '
        Me.lblPouzite.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPouzite.AutoSize = True
        Me.lblPouzite.Location = New System.Drawing.Point(216, 0)
        Me.lblPouzite.Name = "lblPouzite"
        Me.lblPouzite.Size = New System.Drawing.Size(165, 15)
        Me.lblPouzite.TabIndex = 8
        Me.lblPouzite.Text = "Použité sloupce"
        Me.lblPouzite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblI
        '
        Me.lblI.AutoSize = True
        Me.lblI.Location = New System.Drawing.Point(216, 295)
        Me.lblI.Name = "lblI"
        Me.lblI.Size = New System.Drawing.Size(0, 15)
        Me.lblI.TabIndex = 9
        '
        'OrderByEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tlpMain)
        Me.Font = New System.Drawing.Font("Arial Unicode MS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Name = "OrderByEditor"
        Me.Size = New System.Drawing.Size(384, 314)
        Me.tlpMain.ResumeLayout(False)
        Me.tlpMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lstLeft As System.Windows.Forms.ListBox
    Friend WithEvents lstRight As System.Windows.Forms.ListBox
    Friend WithEvents cmdUp As System.Windows.Forms.Button
    Friend WithEvents cmdLeft As System.Windows.Forms.Button
    Friend WithEvents cmdRight As System.Windows.Forms.Button
    Friend WithEvents cmdDown As System.Windows.Forms.Button
    Friend WithEvents cmdChangeDir As System.Windows.Forms.Button
    Friend WithEvents totToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents lblDostupne As System.Windows.Forms.Label
    Friend WithEvents lblPouzite As System.Windows.Forms.Label
    Friend WithEvents lblI As System.Windows.Forms.Label

End Class
