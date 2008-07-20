<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLarge
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLarge))
        Me.cmsMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tmiNextImage = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiPreviousImage = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiFullScreen = New System.Windows.Forms.ToolStripMenuItem
        Me.tmiClose = New System.Windows.Forms.ToolStripMenuItem
        Me.sepContext1 = New System.Windows.Forms.ToolStripSeparator
        Me.tmiRoL = New System.Windows.Forms.ToolStripMenuItem
        Me.rmiRoR = New System.Windows.Forms.ToolStripMenuItem
        Me.cmsMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmsMenu
        '
        Me.cmsMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiNextImage, Me.tmiPreviousImage, Me.tmiFullScreen, Me.tmiClose, Me.sepContext1, Me.tmiRoL, Me.rmiRoR})
        Me.cmsMenu.Name = "cmsMenu"
        Me.cmsMenu.Size = New System.Drawing.Size(205, 164)
        '
        'tmiNextImage
        '
        Me.tmiNextImage.Image = Global.Tools.Metanol.My.Resources.Resources.RightArrowHS
        Me.tmiNextImage.Name = "tmiNextImage"
        Me.tmiNextImage.ShortcutKeyDisplayString = "Right"
        Me.tmiNextImage.Size = New System.Drawing.Size(204, 22)
        Me.tmiNextImage.Text = "Next image"
        '
        'tmiPreviousImage
        '
        Me.tmiPreviousImage.Image = Global.Tools.Metanol.My.Resources.Resources.LeftArrowHS
        Me.tmiPreviousImage.Name = "tmiPreviousImage"
        Me.tmiPreviousImage.ShortcutKeyDisplayString = "Left"
        Me.tmiPreviousImage.Size = New System.Drawing.Size(204, 22)
        Me.tmiPreviousImage.Text = "Previous image"
        '
        'tmiFullScreen
        '
        Me.tmiFullScreen.Image = Global.Tools.Metanol.My.Resources.Resources.FullScreen
        Me.tmiFullScreen.Name = "tmiFullScreen"
        Me.tmiFullScreen.ShortcutKeyDisplayString = "Enter"
        Me.tmiFullScreen.Size = New System.Drawing.Size(204, 22)
        Me.tmiFullScreen.Text = "Toggle full-screen"
        '
        'tmiClose
        '
        Me.tmiClose.Name = "tmiClose"
        Me.tmiClose.ShortcutKeyDisplayString = "Esc"
        Me.tmiClose.Size = New System.Drawing.Size(204, 22)
        Me.tmiClose.Text = "Close"
        '
        'sepContext1
        '
        Me.sepContext1.Name = "sepContext1"
        Me.sepContext1.Size = New System.Drawing.Size(201, 6)
        '
        'tmiRoL
        '
        Me.tmiRoL.Image = Global.Tools.Metanol.My.Resources.Resources.RotateLeft
        Me.tmiRoL.Name = "tmiRoL"
        Me.tmiRoL.ShortcutKeyDisplayString = "L"
        Me.tmiRoL.Size = New System.Drawing.Size(204, 22)
        Me.tmiRoL.Text = "Rotale left"
        '
        'rmiRoR
        '
        Me.rmiRoR.Image = Global.Tools.Metanol.My.Resources.Resources.RotateRight
        Me.rmiRoR.Name = "rmiRoR"
        Me.rmiRoR.ShortcutKeyDisplayString = "R"
        Me.rmiRoR.Size = New System.Drawing.Size(204, 22)
        Me.rmiRoR.Text = "Rotate right"
        '
        'frmLarge
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(284, 264)
        Me.ContextMenuStrip = Me.cmsMenu
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLarge"
        Me.Text = "Preview"
        Me.cmsMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmsMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tmiNextImage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiPreviousImage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiFullScreen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmiClose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sepContext1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tmiRoL As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rmiRoR As System.Windows.Forms.ToolStripMenuItem
End Class
