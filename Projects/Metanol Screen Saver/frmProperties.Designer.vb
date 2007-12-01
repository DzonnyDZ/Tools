<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProperties
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProperties))
        Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
        Me.fraIPTC = New System.Windows.Forms.GroupBox
        Me.lstIPTC = New System.Windows.Forms.ListBox
        Me.fraExif = New System.Windows.Forms.GroupBox
        Me.lstExif = New System.Windows.Forms.ListBox
        Me.fraOther = New System.Windows.Forms.GroupBox
        Me.lstOther = New System.Windows.Forms.ListBox
        Me.txtInfo = New System.Windows.Forms.TextBox
        Me.fraInfo = New System.Windows.Forms.GroupBox
        Me.txtHelp = New System.Windows.Forms.TextBox
        Me.tlpMain.SuspendLayout()
        Me.fraIPTC.SuspendLayout()
        Me.fraExif.SuspendLayout()
        Me.fraOther.SuspendLayout()
        Me.fraInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpMain
        '
        Me.tlpMain.ColumnCount = 3
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.tlpMain.Controls.Add(Me.fraIPTC, 0, 0)
        Me.tlpMain.Controls.Add(Me.fraExif, 1, 0)
        Me.tlpMain.Controls.Add(Me.fraOther, 2, 0)
        Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpMain.Location = New System.Drawing.Point(0, 0)
        Me.tlpMain.Name = "tlpMain"
        Me.tlpMain.RowCount = 1
        Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpMain.Size = New System.Drawing.Size(429, 137)
        Me.tlpMain.TabIndex = 0
        '
        'fraIPTC
        '
        Me.fraIPTC.Controls.Add(Me.lstIPTC)
        Me.fraIPTC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fraIPTC.Location = New System.Drawing.Point(0, 0)
        Me.fraIPTC.Margin = New System.Windows.Forms.Padding(0)
        Me.fraIPTC.Name = "fraIPTC"
        Me.fraIPTC.Size = New System.Drawing.Size(143, 137)
        Me.fraIPTC.TabIndex = 0
        Me.fraIPTC.TabStop = False
        Me.fraIPTC.Text = "IPTC"
        '
        'lstIPTC
        '
        Me.lstIPTC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstIPTC.FormattingEnabled = True
        Me.lstIPTC.Location = New System.Drawing.Point(3, 16)
        Me.lstIPTC.Name = "lstIPTC"
        Me.lstIPTC.Size = New System.Drawing.Size(137, 108)
        Me.lstIPTC.TabIndex = 0
        '
        'fraExif
        '
        Me.fraExif.Controls.Add(Me.lstExif)
        Me.fraExif.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fraExif.Location = New System.Drawing.Point(143, 0)
        Me.fraExif.Margin = New System.Windows.Forms.Padding(0)
        Me.fraExif.Name = "fraExif"
        Me.fraExif.Size = New System.Drawing.Size(143, 137)
        Me.fraExif.TabIndex = 1
        Me.fraExif.TabStop = False
        Me.fraExif.Text = "Exif"
        '
        'lstExif
        '
        Me.lstExif.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstExif.FormattingEnabled = True
        Me.lstExif.Location = New System.Drawing.Point(3, 16)
        Me.lstExif.Name = "lstExif"
        Me.lstExif.Size = New System.Drawing.Size(137, 108)
        Me.lstExif.TabIndex = 0
        '
        'fraOther
        '
        Me.fraOther.Controls.Add(Me.lstOther)
        Me.fraOther.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fraOther.Location = New System.Drawing.Point(286, 0)
        Me.fraOther.Margin = New System.Windows.Forms.Padding(0)
        Me.fraOther.Name = "fraOther"
        Me.fraOther.Size = New System.Drawing.Size(143, 137)
        Me.fraOther.TabIndex = 2
        Me.fraOther.TabStop = False
        Me.fraOther.Text = "Other"
        '
        'lstOther
        '
        Me.lstOther.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstOther.FormattingEnabled = True
        Me.lstOther.Location = New System.Drawing.Point(3, 16)
        Me.lstOther.Name = "lstOther"
        Me.lstOther.Size = New System.Drawing.Size(137, 108)
        Me.lstOther.TabIndex = 0
        '
        'txtInfo
        '
        Me.txtInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtInfo.Location = New System.Drawing.Point(3, 16)
        Me.txtInfo.MaxLength = 0
        Me.txtInfo.Multiline = True
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.ReadOnly = True
        Me.txtInfo.Size = New System.Drawing.Size(423, 50)
        Me.txtInfo.TabIndex = 1
        Me.txtInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'fraInfo
        '
        Me.fraInfo.Controls.Add(Me.txtInfo)
        Me.fraInfo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.fraInfo.Location = New System.Drawing.Point(0, 137)
        Me.fraInfo.Name = "fraInfo"
        Me.fraInfo.Size = New System.Drawing.Size(429, 69)
        Me.fraInfo.TabIndex = 2
        Me.fraInfo.TabStop = False
        Me.fraInfo.Text = "GroupBox1"
        '
        'txtHelp
        '
        Me.txtHelp.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtHelp.Location = New System.Drawing.Point(0, 206)
        Me.txtHelp.MaxLength = 0
        Me.txtHelp.Multiline = True
        Me.txtHelp.Name = "txtHelp"
        Me.txtHelp.ReadOnly = True
        Me.txtHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtHelp.Size = New System.Drawing.Size(429, 82)
        Me.txtHelp.TabIndex = 3
        Me.txtHelp.Text = resources.GetString("txtHelp.Text")
        '
        'frmProperties
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(429, 288)
        Me.Controls.Add(Me.tlpMain)
        Me.Controls.Add(Me.fraInfo)
        Me.Controls.Add(Me.txtHelp)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProperties"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Info template designer"
        Me.tlpMain.ResumeLayout(False)
        Me.fraIPTC.ResumeLayout(False)
        Me.fraExif.ResumeLayout(False)
        Me.fraOther.ResumeLayout(False)
        Me.fraInfo.ResumeLayout(False)
        Me.fraInfo.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents fraIPTC As System.Windows.Forms.GroupBox
    Friend WithEvents lstIPTC As System.Windows.Forms.ListBox
    Friend WithEvents fraExif As System.Windows.Forms.GroupBox
    Friend WithEvents lstExif As System.Windows.Forms.ListBox
    Friend WithEvents fraOther As System.Windows.Forms.GroupBox
    Friend WithEvents lstOther As System.Windows.Forms.ListBox
    Friend WithEvents txtInfo As System.Windows.Forms.TextBox
    Friend WithEvents fraInfo As System.Windows.Forms.GroupBox
    Friend WithEvents txtHelp As System.Windows.Forms.TextBox
End Class
