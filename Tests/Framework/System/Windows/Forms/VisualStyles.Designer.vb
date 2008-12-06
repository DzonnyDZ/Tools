Namespace Framework.SystemF.WindowsF.FormsF.VisualStylesF
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmVisualStylesTest
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
            Me.tvwList = New System.Windows.Forms.TreeView
            Me.imlImages = New System.Windows.Forms.ImageList(Me.components)
            Me.flpPlayer = New System.Windows.Forms.FlowLayoutPanel
            Me.cmdPlay = New System.Windows.Forms.Button
            Me.cmdPause = New System.Windows.Forms.Button
            Me.nudSpeed = New System.Windows.Forms.NumericUpDown
            Me.panDrawEx = New System.Windows.Forms.Panel
            Me.flpDrwBg = New System.Windows.Forms.FlowLayoutPanel
            Me.panBg16 = New System.Windows.Forms.Panel
            Me.panBg32 = New System.Windows.Forms.Panel
            Me.panBg64 = New System.Windows.Forms.Panel
            Me.panBg256 = New System.Windows.Forms.Panel
            Me.flpI = New System.Windows.Forms.FlowLayoutPanel
            Me.lblClassNameI = New System.Windows.Forms.Label
            Me.lblClassName = New System.Windows.Forms.Label
            Me.lblPartI = New System.Windows.Forms.Label
            Me.lblPart = New System.Windows.Forms.Label
            Me.lblStateI = New System.Windows.Forms.Label
            Me.lblState = New System.Windows.Forms.Label
            Me.txtError = New System.Windows.Forms.TextBox
            Me.tmrPlay = New System.Windows.Forms.Timer(Me.components)
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.flpPlayer.SuspendLayout()
            CType(Me.nudSpeed, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.flpDrwBg.SuspendLayout()
            Me.flpI.SuspendLayout()
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
            Me.splMain.Panel1.Controls.Add(Me.tvwList)
            Me.splMain.Panel1.Controls.Add(Me.flpPlayer)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.panDrawEx)
            Me.splMain.Panel2.Controls.Add(Me.flpDrwBg)
            Me.splMain.Panel2.Controls.Add(Me.flpI)
            Me.splMain.Panel2.Controls.Add(Me.txtError)
            Me.splMain.Size = New System.Drawing.Size(751, 368)
            Me.splMain.SplitterDistance = 338
            Me.splMain.TabIndex = 0
            '
            'tvwList
            '
            Me.tvwList.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tvwList.HideSelection = False
            Me.tvwList.ImageIndex = 0
            Me.tvwList.ImageList = Me.imlImages
            Me.tvwList.Location = New System.Drawing.Point(0, 0)
            Me.tvwList.Name = "tvwList"
            Me.tvwList.SelectedImageIndex = 0
            Me.tvwList.Size = New System.Drawing.Size(338, 340)
            Me.tvwList.TabIndex = 0
            '
            'imlImages
            '
            Me.imlImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
            Me.imlImages.ImageSize = New System.Drawing.Size(16, 16)
            Me.imlImages.TransparentColor = System.Drawing.Color.Fuchsia
            '
            'flpPlayer
            '
            Me.flpPlayer.AutoSize = True
            Me.flpPlayer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpPlayer.Controls.Add(Me.cmdPlay)
            Me.flpPlayer.Controls.Add(Me.cmdPause)
            Me.flpPlayer.Controls.Add(Me.nudSpeed)
            Me.flpPlayer.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.flpPlayer.Location = New System.Drawing.Point(0, 340)
            Me.flpPlayer.Name = "flpPlayer"
            Me.flpPlayer.Size = New System.Drawing.Size(338, 28)
            Me.flpPlayer.TabIndex = 1
            '
            'cmdPlay
            '
            Me.cmdPlay.AutoSize = True
            Me.cmdPlay.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdPlay.Image = Global.Tools.Tests.My.Resources.Resources.PlayHS
            Me.cmdPlay.Location = New System.Drawing.Point(3, 3)
            Me.cmdPlay.Name = "cmdPlay"
            Me.cmdPlay.Size = New System.Drawing.Size(22, 22)
            Me.cmdPlay.TabIndex = 0
            Me.cmdPlay.UseVisualStyleBackColor = True
            '
            'cmdPause
            '
            Me.cmdPause.AutoSize = True
            Me.cmdPause.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.cmdPause.Image = Global.Tools.Tests.My.Resources.Resources.PauseHS
            Me.cmdPause.Location = New System.Drawing.Point(31, 3)
            Me.cmdPause.Name = "cmdPause"
            Me.cmdPause.Size = New System.Drawing.Size(22, 22)
            Me.cmdPause.TabIndex = 1
            Me.cmdPause.UseVisualStyleBackColor = True
            '
            'nudSpeed
            '
            Me.nudSpeed.DecimalPlaces = 2
            Me.nudSpeed.Increment = New Decimal(New Integer() {25, 0, 0, 131072})
            Me.nudSpeed.Location = New System.Drawing.Point(59, 3)
            Me.nudSpeed.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
            Me.nudSpeed.Minimum = New Decimal(New Integer() {25, 0, 0, 131072})
            Me.nudSpeed.Name = "nudSpeed"
            Me.nudSpeed.Size = New System.Drawing.Size(64, 20)
            Me.nudSpeed.TabIndex = 2
            Me.nudSpeed.Value = New Decimal(New Integer() {2, 0, 0, 0})
            '
            'panDrawEx
            '
            Me.panDrawEx.Dock = System.Windows.Forms.DockStyle.Top
            Me.panDrawEx.Location = New System.Drawing.Point(0, 83)
            Me.panDrawEx.Name = "panDrawEx"
            Me.panDrawEx.Size = New System.Drawing.Size(409, 132)
            Me.panDrawEx.TabIndex = 0
            '
            'flpDrwBg
            '
            Me.flpDrwBg.AutoSize = True
            Me.flpDrwBg.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpDrwBg.Controls.Add(Me.panBg16)
            Me.flpDrwBg.Controls.Add(Me.panBg32)
            Me.flpDrwBg.Controls.Add(Me.panBg64)
            Me.flpDrwBg.Controls.Add(Me.panBg256)
            Me.flpDrwBg.Dock = System.Windows.Forms.DockStyle.Top
            Me.flpDrwBg.Location = New System.Drawing.Point(0, 13)
            Me.flpDrwBg.Name = "flpDrwBg"
            Me.flpDrwBg.Size = New System.Drawing.Size(409, 70)
            Me.flpDrwBg.TabIndex = 2
            '
            'panBg16
            '
            Me.panBg16.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.panBg16.Location = New System.Drawing.Point(3, 27)
            Me.panBg16.Name = "panBg16"
            Me.panBg16.Size = New System.Drawing.Size(16, 16)
            Me.panBg16.TabIndex = 0
            '
            'panBg32
            '
            Me.panBg32.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.panBg32.Location = New System.Drawing.Point(25, 19)
            Me.panBg32.Name = "panBg32"
            Me.panBg32.Size = New System.Drawing.Size(32, 32)
            Me.panBg32.TabIndex = 1
            '
            'panBg64
            '
            Me.panBg64.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.panBg64.Location = New System.Drawing.Point(63, 3)
            Me.panBg64.Name = "panBg64"
            Me.panBg64.Size = New System.Drawing.Size(64, 64)
            Me.panBg64.TabIndex = 2
            '
            'panBg256
            '
            Me.panBg256.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.panBg256.Location = New System.Drawing.Point(133, 3)
            Me.panBg256.Name = "panBg256"
            Me.panBg256.Size = New System.Drawing.Size(256, 64)
            Me.panBg256.TabIndex = 2
            '
            'flpI
            '
            Me.flpI.AutoSize = True
            Me.flpI.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.flpI.Controls.Add(Me.lblClassNameI)
            Me.flpI.Controls.Add(Me.lblClassName)
            Me.flpI.Controls.Add(Me.lblPartI)
            Me.flpI.Controls.Add(Me.lblPart)
            Me.flpI.Controls.Add(Me.lblStateI)
            Me.flpI.Controls.Add(Me.lblState)
            Me.flpI.Dock = System.Windows.Forms.DockStyle.Top
            Me.flpI.Location = New System.Drawing.Point(0, 0)
            Me.flpI.Name = "flpI"
            Me.flpI.Size = New System.Drawing.Size(409, 13)
            Me.flpI.TabIndex = 1
            '
            'lblClassNameI
            '
            Me.lblClassNameI.AutoSize = True
            Me.lblClassNameI.Location = New System.Drawing.Point(3, 0)
            Me.lblClassNameI.Name = "lblClassNameI"
            Me.lblClassNameI.Size = New System.Drawing.Size(61, 13)
            Me.lblClassNameI.TabIndex = 0
            Me.lblClassNameI.Text = "Class name"
            '
            'lblClassName
            '
            Me.lblClassName.AutoSize = True
            Me.lblClassName.Location = New System.Drawing.Point(70, 0)
            Me.lblClassName.Name = "lblClassName"
            Me.lblClassName.Size = New System.Drawing.Size(0, 13)
            Me.lblClassName.TabIndex = 1
            '
            'lblPartI
            '
            Me.lblPartI.AutoSize = True
            Me.lblPartI.Location = New System.Drawing.Point(76, 0)
            Me.lblPartI.Name = "lblPartI"
            Me.lblPartI.Size = New System.Drawing.Size(26, 13)
            Me.lblPartI.TabIndex = 2
            Me.lblPartI.Text = "Part"
            '
            'lblPart
            '
            Me.lblPart.AutoSize = True
            Me.lblPart.Location = New System.Drawing.Point(108, 0)
            Me.lblPart.Name = "lblPart"
            Me.lblPart.Size = New System.Drawing.Size(0, 13)
            Me.lblPart.TabIndex = 3
            '
            'lblStateI
            '
            Me.lblStateI.AutoSize = True
            Me.lblStateI.Location = New System.Drawing.Point(114, 0)
            Me.lblStateI.Name = "lblStateI"
            Me.lblStateI.Size = New System.Drawing.Size(32, 13)
            Me.lblStateI.TabIndex = 4
            Me.lblStateI.Text = "State"
            '
            'lblState
            '
            Me.lblState.AutoSize = True
            Me.lblState.Location = New System.Drawing.Point(152, 0)
            Me.lblState.Name = "lblState"
            Me.lblState.Size = New System.Drawing.Size(0, 13)
            Me.lblState.TabIndex = 5
            '
            'txtError
            '
            Me.txtError.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtError.BackColor = System.Drawing.Color.Red
            Me.txtError.ForeColor = System.Drawing.Color.Yellow
            Me.txtError.Location = New System.Drawing.Point(0, 0)
            Me.txtError.Multiline = True
            Me.txtError.Name = "txtError"
            Me.txtError.Size = New System.Drawing.Size(409, 368)
            Me.txtError.TabIndex = 3
            Me.txtError.Visible = False
            '
            'tmrPlay
            '
            Me.tmrPlay.Interval = 2000
            '
            'frmVisualStylesTest
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(751, 368)
            Me.Controls.Add(Me.splMain)
            Me.Name = "frmVisualStylesTest"
            Me.Text = "Testing System.Windows.Forms.VisualStyles"
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel1.PerformLayout()
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.Panel2.PerformLayout()
            Me.splMain.ResumeLayout(False)
            Me.flpPlayer.ResumeLayout(False)
            Me.flpPlayer.PerformLayout()
            CType(Me.nudSpeed, System.ComponentModel.ISupportInitialize).EndInit()
            Me.flpDrwBg.ResumeLayout(False)
            Me.flpI.ResumeLayout(False)
            Me.flpI.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents splMain As System.Windows.Forms.SplitContainer
        Friend WithEvents tvwList As System.Windows.Forms.TreeView
        Friend WithEvents imlImages As System.Windows.Forms.ImageList
        Friend WithEvents flpI As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents lblClassNameI As System.Windows.Forms.Label
        Friend WithEvents lblClassName As System.Windows.Forms.Label
        Friend WithEvents lblPartI As System.Windows.Forms.Label
        Friend WithEvents lblPart As System.Windows.Forms.Label
        Friend WithEvents lblStateI As System.Windows.Forms.Label
        Friend WithEvents lblState As System.Windows.Forms.Label
        Friend WithEvents flpDrwBg As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents panBg16 As System.Windows.Forms.Panel
        Friend WithEvents panBg32 As System.Windows.Forms.Panel
        Friend WithEvents panBg64 As System.Windows.Forms.Panel
        Friend WithEvents panBg256 As System.Windows.Forms.Panel
        Friend WithEvents txtError As System.Windows.Forms.TextBox
        Friend WithEvents flpPlayer As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents cmdPlay As System.Windows.Forms.Button
        Friend WithEvents cmdPause As System.Windows.Forms.Button
        Friend WithEvents tmrPlay As System.Windows.Forms.Timer
        Friend WithEvents nudSpeed As System.Windows.Forms.NumericUpDown
        Friend WithEvents panDrawEx As System.Windows.Forms.Panel
    End Class
End Namespace