<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImage
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
        Me.frpTop = New System.Windows.Forms.FlowLayoutPanel
        Me.lblŠíøka = New System.Windows.Forms.Label
        Me.nudŠíøka = New System.Windows.Forms.NumericUpDown
        Me.lblVýška = New System.Windows.Forms.Label
        Me.nudVýška = New System.Windows.Forms.NumericUpDown
        Me.lblPx = New System.Windows.Forms.Label
        Me.chkOsy = New System.Windows.Forms.CheckBox
        Me.cmdNáhled = New System.Windows.Forms.Button
        Me.panMain = New System.Windows.Forms.Panel
        Me.picMain = New System.Windows.Forms.PictureBox
        Me.tlpOKCancel = New System.Windows.Forms.TableLayoutPanel
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdKO = New System.Windows.Forms.Button
        Me.frpTop.SuspendLayout()
        CType(Me.nudŠíøka, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudVýška, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panMain.SuspendLayout()
        CType(Me.picMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tlpOKCancel.SuspendLayout()
        Me.SuspendLayout()
        '
        'frpTop
        '
        Me.frpTop.AutoSize = True
        Me.frpTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.frpTop.Controls.Add(Me.lblŠíøka)
        Me.frpTop.Controls.Add(Me.nudŠíøka)
        Me.frpTop.Controls.Add(Me.lblVýška)
        Me.frpTop.Controls.Add(Me.nudVýška)
        Me.frpTop.Controls.Add(Me.lblPx)
        Me.frpTop.Controls.Add(Me.chkOsy)
        Me.frpTop.Controls.Add(Me.cmdNáhled)
        Me.frpTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.frpTop.Location = New System.Drawing.Point(0, 0)
        Me.frpTop.Name = "frpTop"
        Me.frpTop.Size = New System.Drawing.Size(598, 29)
        Me.frpTop.TabIndex = 0
        '
        'lblŠíøka
        '
        Me.lblŠíøka.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblŠíøka.AutoSize = True
        Me.lblŠíøka.Location = New System.Drawing.Point(3, 8)
        Me.lblŠíøka.Name = "lblŠíøka"
        Me.lblŠíøka.Size = New System.Drawing.Size(34, 13)
        Me.lblŠíøka.TabIndex = 0
        Me.lblŠíøka.Text = "Šíøka"
        '
        'nudŠíøka
        '
        Me.nudŠíøka.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.nudŠíøka.Location = New System.Drawing.Point(43, 4)
        Me.nudŠíøka.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nudŠíøka.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudŠíøka.Name = "nudŠíøka"
        Me.nudŠíøka.Size = New System.Drawing.Size(59, 20)
        Me.nudŠíøka.TabIndex = 1
        Me.nudŠíøka.Value = New Decimal(New Integer() {256, 0, 0, 0})
        '
        'lblVýška
        '
        Me.lblVýška.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblVýška.AutoSize = True
        Me.lblVýška.Location = New System.Drawing.Point(108, 8)
        Me.lblVýška.Name = "lblVýška"
        Me.lblVýška.Size = New System.Drawing.Size(36, 13)
        Me.lblVýška.TabIndex = 2
        Me.lblVýška.Text = "Výška"
        '
        'nudVýška
        '
        Me.nudVýška.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.nudVýška.Location = New System.Drawing.Point(150, 4)
        Me.nudVýška.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.nudVýška.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudVýška.Name = "nudVýška"
        Me.nudVýška.Size = New System.Drawing.Size(58, 20)
        Me.nudVýška.TabIndex = 3
        Me.nudVýška.Value = New Decimal(New Integer() {256, 0, 0, 0})
        '
        'lblPx
        '
        Me.lblPx.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPx.AutoSize = True
        Me.lblPx.Location = New System.Drawing.Point(214, 8)
        Me.lblPx.Name = "lblPx"
        Me.lblPx.Size = New System.Drawing.Size(24, 13)
        Me.lblPx.TabIndex = 4
        Me.lblPx.Text = "[px]"
        '
        'chkOsy
        '
        Me.chkOsy.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkOsy.AutoSize = True
        Me.chkOsy.Checked = True
        Me.chkOsy.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOsy.Location = New System.Drawing.Point(244, 6)
        Me.chkOsy.Name = "chkOsy"
        Me.chkOsy.Size = New System.Drawing.Size(44, 17)
        Me.chkOsy.TabIndex = 5
        Me.chkOsy.Text = "&Osy"
        Me.chkOsy.UseVisualStyleBackColor = True
        '
        'cmdNáhled
        '
        Me.cmdNáhled.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdNáhled.AutoSize = True
        Me.cmdNáhled.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdNáhled.Location = New System.Drawing.Point(294, 3)
        Me.cmdNáhled.Name = "cmdNáhled"
        Me.cmdNáhled.Size = New System.Drawing.Size(51, 23)
        Me.cmdNáhled.TabIndex = 6
        Me.cmdNáhled.Text = "&Náhled"
        Me.cmdNáhled.UseVisualStyleBackColor = True
        '
        'panMain
        '
        Me.panMain.AutoScroll = True
        Me.panMain.Controls.Add(Me.picMain)
        Me.panMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panMain.Location = New System.Drawing.Point(0, 29)
        Me.panMain.Name = "panMain"
        Me.panMain.Size = New System.Drawing.Size(598, 429)
        Me.panMain.TabIndex = 1
        '
        'picMain
        '
        Me.picMain.BackColor = System.Drawing.Color.Black
        Me.picMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picMain.Location = New System.Drawing.Point(0, 0)
        Me.picMain.Name = "picMain"
        Me.picMain.Size = New System.Drawing.Size(598, 429)
        Me.picMain.TabIndex = 1
        Me.picMain.TabStop = False
        '
        'tlpOKCancel
        '
        Me.tlpOKCancel.AutoSize = True
        Me.tlpOKCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpOKCancel.ColumnCount = 2
        Me.tlpOKCancel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpOKCancel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpOKCancel.Controls.Add(Me.cmdOK, 0, 0)
        Me.tlpOKCancel.Controls.Add(Me.cmdKO, 1, 0)
        Me.tlpOKCancel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tlpOKCancel.Location = New System.Drawing.Point(0, 458)
        Me.tlpOKCancel.Name = "tlpOKCancel"
        Me.tlpOKCancel.RowCount = 1
        Me.tlpOKCancel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpOKCancel.Size = New System.Drawing.Size(598, 29)
        Me.tlpOKCancel.TabIndex = 0
        '
        'cmdOK
        '
        Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.cmdOK.AutoSize = True
        Me.cmdOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdOK.Enabled = False
        Me.cmdOK.Location = New System.Drawing.Point(253, 3)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(43, 23)
        Me.cmdOK.TabIndex = 0
        Me.cmdOK.Text = "&Uložit"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdKO
        '
        Me.cmdKO.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmdKO.AutoSize = True
        Me.cmdKO.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdKO.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdKO.Location = New System.Drawing.Point(302, 3)
        Me.cmdKO.Name = "cmdKO"
        Me.cmdKO.Size = New System.Drawing.Size(48, 23)
        Me.cmdKO.TabIndex = 1
        Me.cmdKO.Text = "&Storno"
        Me.cmdKO.UseVisualStyleBackColor = True
        '
        'frmImage
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdKO
        Me.ClientSize = New System.Drawing.Size(598, 487)
        Me.Controls.Add(Me.panMain)
        Me.Controls.Add(Me.frpTop)
        Me.Controls.Add(Me.tlpOKCancel)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImage"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Vykreslit do souboru"
        Me.frpTop.ResumeLayout(False)
        Me.frpTop.PerformLayout()
        CType(Me.nudŠíøka, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudVýška, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panMain.ResumeLayout(False)
        CType(Me.picMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tlpOKCancel.ResumeLayout(False)
        Me.tlpOKCancel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents frpTop As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents lblŠíøka As System.Windows.Forms.Label
    Friend WithEvents nudŠíøka As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblVýška As System.Windows.Forms.Label
    Friend WithEvents nudVýška As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblPx As System.Windows.Forms.Label
    Friend WithEvents cmdNáhled As System.Windows.Forms.Button
    Friend WithEvents panMain As System.Windows.Forms.Panel
    Friend WithEvents chkOsy As System.Windows.Forms.CheckBox
    Friend WithEvents picMain As System.Windows.Forms.PictureBox
    Friend WithEvents tlpOKCancel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdKO As System.Windows.Forms.Button
End Class
