<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DayView
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
        Me.panMain = New System.Windows.Forms.Panel
        Me.tlpHead = New System.Windows.Forms.TableLayoutPanel
        Me.vsbVert = New System.Windows.Forms.VScrollBar
        Me.hsbHoriz = New System.Windows.Forms.HScrollBar
        Me.totToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmsItem = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tmiDeleteItem = New System.Windows.Forms.ToolStripMenuItem
        Me.panMain.SuspendLayout()
        Me.cmsItem.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.AutoSize = True
        Me.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpMain.ColumnCount = 1
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 1
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1.0!))
        Me.tlpMain.Size = New System.Drawing.Size(0, 0)
        Me.tlpMain.TabIndex = 0
        '
        'panMain
        '
        Me.panMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panMain.Controls.Add(Me.tlpMain)
        Me.panMain.Location = New System.Drawing.Point(0, 0)
        Me.panMain.Margin = New System.Windows.Forms.Padding(0)
        Me.panMain.Name = "panMain"
        Me.panMain.Size = New System.Drawing.Size(673, 521)
        Me.panMain.TabIndex = 1
        '
        'tlpHead
        '
        Me.tlpHead.AutoSize = True
        Me.tlpHead.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpHead.ColumnCount = 1
        Me.tlpHead.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.tlpHead.Location = New System.Drawing.Point(0, 0)
        Me.tlpHead.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpHead.Name = "tlpHead"
        Me.tlpHead.RowCount = 1
        Me.tlpHead.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpHead.Size = New System.Drawing.Size(22, 0)
        Me.tlpHead.TabIndex = 1
        '
        'vsbVert
        '
        Me.vsbVert.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.vsbVert.Location = New System.Drawing.Point(673, 0)
        Me.vsbVert.Name = "vsbVert"
        Me.vsbVert.Size = New System.Drawing.Size(17, 521)
        Me.vsbVert.TabIndex = 1
        '
        'hsbHoriz
        '
        Me.hsbHoriz.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.hsbHoriz.LargeChange = 180
        Me.hsbHoriz.Location = New System.Drawing.Point(0, 521)
        Me.hsbHoriz.Maximum = 1620
        Me.hsbHoriz.Name = "hsbHoriz"
        Me.hsbHoriz.Size = New System.Drawing.Size(673, 17)
        Me.hsbHoriz.SmallChange = 30
        Me.hsbHoriz.TabIndex = 2
        '
        'cmsItem
        '
        Me.cmsItem.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiDeleteItem})
        Me.cmsItem.Name = "cmsItem"
        Me.cmsItem.Size = New System.Drawing.Size(153, 48)
        '
        'tmiDeleteItem
        '
        Me.tmiDeleteItem.Image = My.Resources.Resources.DeleteHS
        Me.tmiDeleteItem.Name = "tmiDeleteItem"
        Me.tmiDeleteItem.ShortcutKeyDisplayString = "Del"
        Me.tmiDeleteItem.Size = New System.Drawing.Size(152, 22)
        Me.tmiDeleteItem.Text = "&Odstranit"
        '
        'DayView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tlpHead)
        Me.Controls.Add(Me.panMain)
        Me.Controls.Add(Me.hsbHoriz)
        Me.Controls.Add(Me.vsbVert)
        Me.Name = "DayView"
        Me.Size = New System.Drawing.Size(690, 538)
        Me.panMain.ResumeLayout(False)
        Me.panMain.PerformLayout()
        Me.cmsItem.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents panMain As System.Windows.Forms.Panel
    Friend WithEvents vsbVert As System.Windows.Forms.VScrollBar
    Friend WithEvents hsbHoriz As System.Windows.Forms.HScrollBar
    Friend WithEvents tlpHead As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents totToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents cmsItem As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tmiDeleteItem As System.Windows.Forms.ToolStripMenuItem

End Class
