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
        Me.cmsMenu.AccessibleDescription = Nothing
        Me.cmsMenu.AccessibleName = Nothing
        resources.ApplyResources(Me.cmsMenu, "cmsMenu")
        Me.cmsMenu.BackgroundImage = Nothing
        Me.cmsMenu.Font = Nothing
        Me.cmsMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiNextImage, Me.tmiPreviousImage, Me.tmiFullScreen, Me.tmiClose, Me.sepContext1, Me.tmiRoL, Me.rmiRoR})
        Me.cmsMenu.Name = "cmsMenu"
        '
        'tmiNextImage
        '
        Me.tmiNextImage.AccessibleDescription = Nothing
        Me.tmiNextImage.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiNextImage, "tmiNextImage")
        Me.tmiNextImage.BackgroundImage = Nothing
        Me.tmiNextImage.Image = Global.Tools.Metanol.My.Resources.Resources.RightArrowHS
        Me.tmiNextImage.Name = "tmiNextImage"
        '
        'tmiPreviousImage
        '
        Me.tmiPreviousImage.AccessibleDescription = Nothing
        Me.tmiPreviousImage.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiPreviousImage, "tmiPreviousImage")
        Me.tmiPreviousImage.BackgroundImage = Nothing
        Me.tmiPreviousImage.Image = Global.Tools.Metanol.My.Resources.Resources.LeftArrowHS
        Me.tmiPreviousImage.Name = "tmiPreviousImage"
        '
        'tmiFullScreen
        '
        Me.tmiFullScreen.AccessibleDescription = Nothing
        Me.tmiFullScreen.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiFullScreen, "tmiFullScreen")
        Me.tmiFullScreen.BackgroundImage = Nothing
        Me.tmiFullScreen.Image = Global.Tools.Metanol.My.Resources.Resources.FullScreen
        Me.tmiFullScreen.Name = "tmiFullScreen"
        '
        'tmiClose
        '
        Me.tmiClose.AccessibleDescription = Nothing
        Me.tmiClose.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiClose, "tmiClose")
        Me.tmiClose.BackgroundImage = Nothing
        Me.tmiClose.Name = "tmiClose"
        '
        'sepContext1
        '
        Me.sepContext1.AccessibleDescription = Nothing
        Me.sepContext1.AccessibleName = Nothing
        resources.ApplyResources(Me.sepContext1, "sepContext1")
        Me.sepContext1.Name = "sepContext1"
        '
        'tmiRoL
        '
        Me.tmiRoL.AccessibleDescription = Nothing
        Me.tmiRoL.AccessibleName = Nothing
        resources.ApplyResources(Me.tmiRoL, "tmiRoL")
        Me.tmiRoL.BackgroundImage = Nothing
        Me.tmiRoL.Image = Global.Tools.Metanol.My.Resources.Resources.RotateLeft
        Me.tmiRoL.Name = "tmiRoL"
        '
        'rmiRoR
        '
        Me.rmiRoR.AccessibleDescription = Nothing
        Me.rmiRoR.AccessibleName = Nothing
        resources.ApplyResources(Me.rmiRoR, "rmiRoR")
        Me.rmiRoR.BackgroundImage = Nothing
        Me.rmiRoR.Image = Global.Tools.Metanol.My.Resources.Resources.RotateRight
        Me.rmiRoR.Name = "rmiRoR"
        '
        'frmLarge
        '
        Me.AccessibleDescription = Nothing
        Me.AccessibleName = Nothing
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = Nothing
        Me.ContextMenuStrip = Me.cmsMenu
        Me.Font = Nothing
        Me.Name = "frmLarge"
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
