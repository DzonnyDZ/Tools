<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ContentTree
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
        Me.splMain = New System.Windows.Forms.SplitContainer
        Me.tvwTree = New System.Windows.Forms.TreeView
        Me.prgProperty = New System.Windows.Forms.PropertyGrid
        Me.imlImages = New System.Windows.Forms.ImageList(Me.components)
        Me.splMain.Panel1.SuspendLayout()
        Me.splMain.Panel2.SuspendLayout()
        Me.splMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'splMain
        '
        Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splMain.Location = New System.Drawing.Point(0, 0)
        Me.splMain.Name = "splMain"
        '
        'splMain.Panel1
        '
        Me.splMain.Panel1.Controls.Add(Me.tvwTree)
        '
        'splMain.Panel2
        '
        Me.splMain.Panel2.Controls.Add(Me.prgProperty)
        Me.splMain.Size = New System.Drawing.Size(284, 264)
        Me.splMain.SplitterDistance = 132
        Me.splMain.TabIndex = 0
        '
        'tvwTree
        '
        Me.tvwTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvwTree.HideSelection = False
        Me.tvwTree.ImageIndex = 0
        Me.tvwTree.ImageList = Me.imlImages
        Me.tvwTree.Location = New System.Drawing.Point(0, 0)
        Me.tvwTree.Name = "tvwTree"
        Me.tvwTree.SelectedImageIndex = 0
        Me.tvwTree.Size = New System.Drawing.Size(132, 264)
        Me.tvwTree.TabIndex = 0
        '
        'prgProperty
        '
        Me.prgProperty.Dock = System.Windows.Forms.DockStyle.Fill
        Me.prgProperty.Location = New System.Drawing.Point(0, 0)
        Me.prgProperty.Name = "prgProperty"
        Me.prgProperty.Size = New System.Drawing.Size(148, 264)
        Me.prgProperty.TabIndex = 0
        '
        'imlImages
        '
        Me.imlImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.imlImages.ImageSize = New System.Drawing.Size(16, 16)
        Me.imlImages.TransparentColor = System.Drawing.Color.Fuchsia
        '
        'ContentTree
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 264)
        Me.Controls.Add(Me.splMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ContentTree"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "ContentTree"
        Me.splMain.Panel1.ResumeLayout(False)
        Me.splMain.Panel2.ResumeLayout(False)
        Me.splMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents splMain As System.Windows.Forms.SplitContainer
    Friend WithEvents tvwTree As System.Windows.Forms.TreeView
    Friend WithEvents prgProperty As System.Windows.Forms.PropertyGrid
    Friend WithEvents imlImages As System.Windows.Forms.ImageList
End Class
