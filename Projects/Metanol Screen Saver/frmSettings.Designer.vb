<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings))
        Me.tabScreens = New System.Windows.Forms.TabControl
        Me.tapScreen1 = New System.Windows.Forms.TabPage
        Me.scsScreen1 = New Metanol.SSaver.ScreenSettings
        Me.tosMenu = New System.Windows.Forms.ToolStrip
        Me.tsbAdd = New System.Windows.Forms.ToolStripButton
        Me.tsbRemove = New System.Windows.Forms.ToolStripButton
        Me.tss1 = New System.Windows.Forms.ToolStripSeparator
        Me.tsbShowMonitors = New System.Windows.Forms.ToolStripButton
        Me.tdbHelp = New System.Windows.Forms.ToolStripDropDownButton
        Me.tmiAbout = New System.Windows.Forms.ToolStripMenuItem
        Me.tsbCancel = New System.Windows.Forms.ToolStripButton
        Me.tsbOK = New System.Windows.Forms.ToolStripButton
        Me.tabScreens.SuspendLayout()
        Me.tapScreen1.SuspendLayout()
        Me.tosMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabScreens
        '
        Me.tabScreens.Controls.Add(Me.tapScreen1)
        Me.tabScreens.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabScreens.Location = New System.Drawing.Point(0, 25)
        Me.tabScreens.Name = "tabScreens"
        Me.tabScreens.SelectedIndex = 0
        Me.tabScreens.Size = New System.Drawing.Size(776, 625)
        Me.tabScreens.TabIndex = 1
        '
        'tapScreen1
        '
        Me.tapScreen1.Controls.Add(Me.scsScreen1)
        Me.tapScreen1.Location = New System.Drawing.Point(4, 22)
        Me.tapScreen1.Name = "tapScreen1"
        Me.tapScreen1.Padding = New System.Windows.Forms.Padding(3)
        Me.tapScreen1.Size = New System.Drawing.Size(768, 599)
        Me.tapScreen1.TabIndex = 0
        Me.tapScreen1.Text = "1"
        Me.tapScreen1.UseVisualStyleBackColor = True
        '
        'scsScreen1
        '
        Me.scsScreen1.AutoScroll = True
        Me.scsScreen1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scsScreen1.Location = New System.Drawing.Point(3, 3)
        Me.scsScreen1.Name = "scsScreen1"
        Me.scsScreen1.Size = New System.Drawing.Size(762, 593)
        Me.scsScreen1.TabIndex = 0
        '
        'tosMenu
        '
        Me.tosMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tosMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbAdd, Me.tsbRemove, Me.tss1, Me.tsbShowMonitors, Me.tdbHelp, Me.tsbCancel, Me.tsbOK})
        Me.tosMenu.Location = New System.Drawing.Point(0, 0)
        Me.tosMenu.Name = "tosMenu"
        Me.tosMenu.Size = New System.Drawing.Size(776, 25)
        Me.tosMenu.TabIndex = 1
        Me.tosMenu.Text = "ToolStrip1"
        '
        'tsbAdd
        '
        Me.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbAdd.Image = CType(resources.GetObject("tsbAdd.Image"), System.Drawing.Image)
        Me.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbAdd.Name = "tsbAdd"
        Me.tsbAdd.Size = New System.Drawing.Size(152, 22)
        Me.tsbAdd.Text = "Add settings for next monitor"
        '
        'tsbRemove
        '
        Me.tsbRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbRemove.Image = CType(resources.GetObject("tsbRemove.Image"), System.Drawing.Image)
        Me.tsbRemove.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbRemove.Name = "tsbRemove"
        Me.tsbRemove.Size = New System.Drawing.Size(167, 22)
        Me.tsbRemove.Text = "Remove settings for last monitor"
        '
        'tss1
        '
        Me.tss1.Name = "tss1"
        Me.tss1.Size = New System.Drawing.Size(6, 25)
        '
        'tsbShowMonitors
        '
        Me.tsbShowMonitors.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbShowMonitors.Image = CType(resources.GetObject("tsbShowMonitors.Image"), System.Drawing.Image)
        Me.tsbShowMonitors.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbShowMonitors.Name = "tsbShowMonitors"
        Me.tsbShowMonitors.Size = New System.Drawing.Size(81, 22)
        Me.tsbShowMonitors.Text = "Show monitors"
        '
        'tdbHelp
        '
        Me.tdbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tdbHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiAbout})
        Me.tdbHelp.Image = CType(resources.GetObject("tdbHelp.Image"), System.Drawing.Image)
        Me.tdbHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tdbHelp.Name = "tdbHelp"
        Me.tdbHelp.Size = New System.Drawing.Size(41, 22)
        Me.tdbHelp.Text = "&Help"
        '
        'tmiAbout
        '
        Me.tmiAbout.Name = "tmiAbout"
        Me.tmiAbout.Size = New System.Drawing.Size(103, 22)
        Me.tmiAbout.Text = "&About"
        '
        'tsbCancel
        '
        Me.tsbCancel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tsbCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbCancel.Image = CType(resources.GetObject("tsbCancel.Image"), System.Drawing.Image)
        Me.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbCancel.Name = "tsbCancel"
        Me.tsbCancel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
        Me.tsbCancel.Size = New System.Drawing.Size(43, 22)
        Me.tsbCancel.Text = "&Cancel"
        '
        'tsbOK
        '
        Me.tsbOK.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tsbOK.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbOK.Image = CType(resources.GetObject("tsbOK.Image"), System.Drawing.Image)
        Me.tsbOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbOK.Name = "tsbOK"
        Me.tsbOK.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
        Me.tsbOK.Size = New System.Drawing.Size(25, 22)
        Me.tsbOK.Text = "&OK"
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(776, 650)
        Me.Controls.Add(Me.tabScreens)
        Me.Controls.Add(Me.tosMenu)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettings"
        Me.ShowInTaskbar = False
        Me.Text = "Metanol Screen Saver"
        Me.tabScreens.ResumeLayout(False)
        Me.tapScreen1.ResumeLayout(False)
        Me.tosMenu.ResumeLayout(False)
        Me.tosMenu.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents scsScreen1 As Metanol.SSaver.ScreenSettings
    Friend WithEvents tabScreens As System.Windows.Forms.TabControl
    Friend WithEvents tapScreen1 As System.Windows.Forms.TabPage
    Friend WithEvents tosMenu As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbRemove As System.Windows.Forms.ToolStripButton
    Friend WithEvents tss1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbShowMonitors As System.Windows.Forms.ToolStripButton
    Friend WithEvents tdbHelp As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents tmiAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbOK As System.Windows.Forms.ToolStripButton
End Class
