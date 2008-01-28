Namespace WindowsT.FormsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class frmObjectBrowser
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
            Me.obTest = New Tools.WindowsT.FormsT.ObjectBrowser
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.flpBottom = New System.Windows.Forms.FlowLayoutPanel
            Me.cmbType = New System.Windows.Forms.ComboBox
            Me.cmbAccess = New System.Windows.Forms.ComboBox
            Me.chkStatic = New System.Windows.Forms.CheckBox
            Me.chkSealed = New System.Windows.Forms.CheckBox
            Me.chkShortcut = New System.Windows.Forms.CheckBox
            Me.cmdShow = New System.Windows.Forms.Button
            Me.picBig = New System.Windows.Forms.PictureBox
            Me.picSmall = New System.Windows.Forms.PictureBox
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.flpBottom.SuspendLayout()
            CType(Me.picBig, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.picSmall, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'obTest
            '
            Me.obTest.Dock = System.Windows.Forms.DockStyle.Fill
            Me.obTest.Location = New System.Drawing.Point(0, 0)
            Me.obTest.Name = "obTest"
            Me.obTest.Size = New System.Drawing.Size(675, 428)
            Me.obTest.TabIndex = 0
            '
            'splMain
            '
            Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
            Me.splMain.Location = New System.Drawing.Point(0, 0)
            Me.splMain.Name = "splMain"
            Me.splMain.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'splMain.Panel1
            '
            Me.splMain.Panel1.Controls.Add(Me.obTest)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.flpBottom)
            Me.splMain.Size = New System.Drawing.Size(675, 505)
            Me.splMain.SplitterDistance = 428
            Me.splMain.TabIndex = 1
            '
            'flpBottom
            '
            Me.flpBottom.AutoScroll = True
            Me.flpBottom.Controls.Add(Me.cmbType)
            Me.flpBottom.Controls.Add(Me.cmbAccess)
            Me.flpBottom.Controls.Add(Me.chkStatic)
            Me.flpBottom.Controls.Add(Me.chkSealed)
            Me.flpBottom.Controls.Add(Me.chkShortcut)
            Me.flpBottom.Controls.Add(Me.cmdShow)
            Me.flpBottom.Controls.Add(Me.picBig)
            Me.flpBottom.Controls.Add(Me.picSmall)
            Me.flpBottom.Dock = System.Windows.Forms.DockStyle.Fill
            Me.flpBottom.Location = New System.Drawing.Point(0, 0)
            Me.flpBottom.Name = "flpBottom"
            Me.flpBottom.Size = New System.Drawing.Size(675, 73)
            Me.flpBottom.TabIndex = 0
            '
            'cmbType
            '
            Me.cmbType.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbType.FormattingEnabled = True
            Me.cmbType.Location = New System.Drawing.Point(3, 24)
            Me.cmbType.Name = "cmbType"
            Me.cmbType.Size = New System.Drawing.Size(121, 21)
            Me.cmbType.Sorted = True
            Me.cmbType.TabIndex = 0
            '
            'cmbAccess
            '
            Me.cmbAccess.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmbAccess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbAccess.FormattingEnabled = True
            Me.cmbAccess.Location = New System.Drawing.Point(130, 24)
            Me.cmbAccess.Name = "cmbAccess"
            Me.cmbAccess.Size = New System.Drawing.Size(121, 21)
            Me.cmbAccess.TabIndex = 1
            '
            'chkStatic
            '
            Me.chkStatic.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.chkStatic.AutoSize = True
            Me.chkStatic.Location = New System.Drawing.Point(257, 26)
            Me.chkStatic.Name = "chkStatic"
            Me.chkStatic.Size = New System.Drawing.Size(53, 17)
            Me.chkStatic.TabIndex = 2
            Me.chkStatic.Text = "Static"
            Me.chkStatic.UseVisualStyleBackColor = True
            '
            'chkSealed
            '
            Me.chkSealed.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.chkSealed.AutoSize = True
            Me.chkSealed.Location = New System.Drawing.Point(316, 26)
            Me.chkSealed.Name = "chkSealed"
            Me.chkSealed.Size = New System.Drawing.Size(59, 17)
            Me.chkSealed.TabIndex = 3
            Me.chkSealed.Text = "Sealed"
            Me.chkSealed.UseVisualStyleBackColor = True
            '
            'chkShortcut
            '
            Me.chkShortcut.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.chkShortcut.AutoSize = True
            Me.chkShortcut.Location = New System.Drawing.Point(381, 26)
            Me.chkShortcut.Name = "chkShortcut"
            Me.chkShortcut.Size = New System.Drawing.Size(66, 17)
            Me.chkShortcut.TabIndex = 4
            Me.chkShortcut.Text = "Shortcut"
            Me.chkShortcut.UseVisualStyleBackColor = True
            '
            'cmdShow
            '
            Me.cmdShow.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.cmdShow.Location = New System.Drawing.Point(453, 23)
            Me.cmdShow.Name = "cmdShow"
            Me.cmdShow.Size = New System.Drawing.Size(75, 23)
            Me.cmdShow.TabIndex = 5
            Me.cmdShow.Text = "Show"
            Me.cmdShow.UseVisualStyleBackColor = True
            '
            'picBig
            '
            Me.picBig.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.picBig.Location = New System.Drawing.Point(534, 3)
            Me.picBig.Name = "picBig"
            Me.picBig.Size = New System.Drawing.Size(64, 64)
            Me.picBig.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            Me.picBig.TabIndex = 6
            Me.picBig.TabStop = False
            '
            'picSmall
            '
            Me.picSmall.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.picSmall.Location = New System.Drawing.Point(604, 25)
            Me.picSmall.Name = "picSmall"
            Me.picSmall.Size = New System.Drawing.Size(20, 20)
            Me.picSmall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
            Me.picSmall.TabIndex = 7
            Me.picSmall.TabStop = False
            '
            'frmObjectBrowser
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(675, 505)
            Me.Controls.Add(Me.splMain)
            Me.Name = "frmObjectBrowser"
            Me.Text = "Testing Tools.Windows.Forms.TransparentLabel"
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.ResumeLayout(False)
            Me.flpBottom.ResumeLayout(False)
            Me.flpBottom.PerformLayout()
            CType(Me.picBig, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.picSmall, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents obTest As Tools.WindowsT.FormsT.ObjectBrowser
        Friend WithEvents splMain As System.Windows.Forms.SplitContainer
        Friend WithEvents flpBottom As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents cmbType As System.Windows.Forms.ComboBox
        Friend WithEvents cmbAccess As System.Windows.Forms.ComboBox
        Friend WithEvents chkStatic As System.Windows.Forms.CheckBox
        Friend WithEvents chkSealed As System.Windows.Forms.CheckBox
        Friend WithEvents chkShortcut As System.Windows.Forms.CheckBox
        Friend WithEvents cmdShow As System.Windows.Forms.Button
        Friend WithEvents picBig As System.Windows.Forms.PictureBox
        Friend WithEvents picSmall As System.Windows.Forms.PictureBox
    End Class
End Namespace