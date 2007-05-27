Namespace InternalT
    Partial Class ToolAssembly : Inherits Form
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ToolAssembly))
            Me.imlImages = New System.Windows.Forms.ImageList(Me.components)
            Me.lvwErrors = New System.Windows.Forms.ListView
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.tosErrors = New System.Windows.Forms.ToolStrip
            Me.tsbError = New System.Windows.Forms.ToolStripButton
            Me.tsbWarning = New System.Windows.Forms.ToolStripButton
            Me.tsbInfo = New System.Windows.Forms.ToolStripButton
            Me.cohWhat = New System.Windows.Forms.ColumnHeader
            Me.cohWhere = New System.Windows.Forms.ColumnHeader
            Me.cohDescription = New System.Windows.Forms.ColumnHeader
            Me.tvwTools = New System.Windows.Forms.TreeView
            Me.imlTools = New System.Windows.Forms.ImageList(Me.components)
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.tosErrors.SuspendLayout()
            Me.SuspendLayout()
            '
            'imlImages
            '
            Me.imlImages.ImageStream = CType(resources.GetObject("imlImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imlImages.TransparentColor = System.Drawing.Color.Transparent
            Me.imlImages.Images.SetKeyName(0, "Error")
            Me.imlImages.Images.SetKeyName(1, "Warning")
            Me.imlImages.Images.SetKeyName(2, "Message")
            '
            'lvwErrors
            '
            Me.lvwErrors.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.cohWhat, Me.cohWhere, Me.cohDescription})
            Me.lvwErrors.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lvwErrors.Location = New System.Drawing.Point(0, 25)
            Me.lvwErrors.Name = "lvwErrors"
            Me.lvwErrors.Size = New System.Drawing.Size(515, 205)
            Me.lvwErrors.TabIndex = 0
            Me.lvwErrors.UseCompatibleStateImageBehavior = False
            Me.lvwErrors.View = System.Windows.Forms.View.Details
            '
            'splMain
            '
            Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splMain.Location = New System.Drawing.Point(0, 0)
            Me.splMain.Name = "splMain"
            Me.splMain.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'splMain.Panel1
            '
            Me.splMain.Panel1.Controls.Add(Me.tvwTools)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.lvwErrors)
            Me.splMain.Panel2.Controls.Add(Me.tosErrors)
            Me.splMain.Size = New System.Drawing.Size(515, 468)
            Me.splMain.SplitterDistance = 234
            Me.splMain.TabIndex = 1
            '
            'tosErrors
            '
            Me.tosErrors.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.tosErrors.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbError, Me.tsbWarning, Me.tsbInfo})
            Me.tosErrors.Location = New System.Drawing.Point(0, 0)
            Me.tosErrors.Name = "tosErrors"
            Me.tosErrors.Size = New System.Drawing.Size(515, 25)
            Me.tosErrors.TabIndex = 2
            Me.tosErrors.Text = "ToolStrip1"
            '
            'tsbError
            '
            Me.tsbError.CheckOnClick = True
            Me.tsbError.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.tsbError.Image = Global.Tools.Tests.My.Resources.Resources.CriticalError
            Me.tsbError.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.tsbError.Name = "tsbError"
            Me.tsbError.Size = New System.Drawing.Size(23, 22)
            Me.tsbError.Text = "ToolStripButton1"
            '
            'tsbWarning
            '
            Me.tsbWarning.CheckOnClick = True
            Me.tsbWarning.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.tsbWarning.Image = Global.Tools.Tests.My.Resources.Resources.warning
            Me.tsbWarning.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.tsbWarning.Name = "tsbWarning"
            Me.tsbWarning.Size = New System.Drawing.Size(23, 22)
            Me.tsbWarning.Text = "ToolStripButton2"
            '
            'tsbInfo
            '
            Me.tsbInfo.CheckOnClick = True
            Me.tsbInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.tsbInfo.Image = Global.Tools.Tests.My.Resources.Resources.infoBubble
            Me.tsbInfo.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.tsbInfo.Name = "tsbInfo"
            Me.tsbInfo.Size = New System.Drawing.Size(23, 22)
            Me.tsbInfo.Text = "ToolStripButton3"
            '
            'cohWhat
            '
            Me.cohWhat.Text = "What"
            '
            'cohWhere
            '
            Me.cohWhere.Text = "Where"
            Me.cohWhere.Width = 120
            '
            'cohDescription
            '
            Me.cohDescription.Text = "Description"
            Me.cohDescription.Width = 500
            '
            'tvwTools
            '
            Me.tvwTools.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tvwTools.Location = New System.Drawing.Point(0, 0)
            Me.tvwTools.Name = "tvwTools"
            Me.tvwTools.Size = New System.Drawing.Size(515, 234)
            Me.tvwTools.TabIndex = 0
            '
            'imlTools
            '
            Me.imlTools.ImageStream = CType(resources.GetObject("imlTools.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imlTools.TransparentColor = System.Drawing.Color.Transparent
            Me.imlTools.Images.SetKeyName(0, "Tool")
            Me.imlTools.Images.SetKeyName(1, "Namespace")
            Me.imlTools.Images.SetKeyName(2, "Assembly")
            '
            'ToolAssembly
            '
            Me.ClientSize = New System.Drawing.Size(515, 468)
            Me.Controls.Add(Me.splMain)
            Me.Name = "ToolAssembly"
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.Panel2.PerformLayout()
            Me.splMain.ResumeLayout(False)
            Me.tosErrors.ResumeLayout(False)
            Me.tosErrors.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents imlImages As System.Windows.Forms.ImageList
        Private components As System.ComponentModel.IContainer
        Friend WithEvents lvwErrors As System.Windows.Forms.ListView
        Friend WithEvents cohWhat As System.Windows.Forms.ColumnHeader
        Friend WithEvents cohWhere As System.Windows.Forms.ColumnHeader
        Friend WithEvents cohDescription As System.Windows.Forms.ColumnHeader
        Friend WithEvents splMain As System.Windows.Forms.SplitContainer
        Friend WithEvents tvwTools As System.Windows.Forms.TreeView
        Friend WithEvents tosErrors As System.Windows.Forms.ToolStrip
        Friend WithEvents tsbError As System.Windows.Forms.ToolStripButton
        Friend WithEvents tsbWarning As System.Windows.Forms.ToolStripButton
        Friend WithEvents tsbInfo As System.Windows.Forms.ToolStripButton
        Friend WithEvents imlTools As System.Windows.Forms.ImageList
    End Class
End Namespace