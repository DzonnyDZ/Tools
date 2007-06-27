<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.splMain = New System.Windows.Forms.SplitContainer
        Me.splFolder = New System.Windows.Forms.SplitContainer
        Me.lvwImages = New System.Windows.Forms.ListView
        Me.imlImages = New System.Windows.Forms.ImageList(Me.components)
        Me.lvwFolder = New System.Windows.Forms.ListView
        Me.imlFolder = New System.Windows.Forms.ImageList(Me.components)
        Me.tlpPath = New System.Windows.Forms.TableLayoutPanel
        Me.cmdBrowse = New System.Windows.Forms.Button
        Me.cmdGo = New System.Windows.Forms.Button
        Me.txtPath = New System.Windows.Forms.TextBox
        Me.fbdBrowse = New System.Windows.Forms.FolderBrowserDialog
        Me.bgwThumb = New System.ComponentModel.BackgroundWorker
        Me.splMain.Panel1.SuspendLayout()
        Me.splMain.SuspendLayout()
        Me.splFolder.Panel1.SuspendLayout()
        Me.splFolder.Panel2.SuspendLayout()
        Me.splFolder.SuspendLayout()
        Me.tlpPath.SuspendLayout()
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
        Me.splMain.Panel1.Controls.Add(Me.splFolder)
        Me.splMain.Panel1.Controls.Add(Me.tlpPath)
        Me.splMain.Size = New System.Drawing.Size(692, 536)
        Me.splMain.SplitterDistance = 230
        Me.splMain.TabIndex = 0
        '
        'splFolder
        '
        Me.splFolder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splFolder.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.splFolder.Location = New System.Drawing.Point(0, 29)
        Me.splFolder.Name = "splFolder"
        Me.splFolder.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splFolder.Panel1
        '
        Me.splFolder.Panel1.Controls.Add(Me.lvwImages)
        '
        'splFolder.Panel2
        '
        Me.splFolder.Panel2.Controls.Add(Me.lvwFolder)
        Me.splFolder.Size = New System.Drawing.Size(230, 507)
        Me.splFolder.SplitterDistance = 359
        Me.splFolder.TabIndex = 1
        '
        'lvwImages
        '
        Me.lvwImages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwImages.LargeImageList = Me.imlImages
        Me.lvwImages.Location = New System.Drawing.Point(0, 0)
        Me.lvwImages.Name = "lvwImages"
        Me.lvwImages.Size = New System.Drawing.Size(230, 359)
        Me.lvwImages.TabIndex = 0
        Me.lvwImages.UseCompatibleStateImageBehavior = False
        '
        'imlImages
        '
        Me.imlImages.ImageStream = CType(resources.GetObject("imlImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlImages.TransparentColor = System.Drawing.Color.Transparent
        Me.imlImages.Images.SetKeyName(0, "IrfanView")
        '
        'lvwFolder
        '
        Me.lvwFolder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwFolder.Location = New System.Drawing.Point(0, 0)
        Me.lvwFolder.Name = "lvwFolder"
        Me.lvwFolder.Size = New System.Drawing.Size(230, 144)
        Me.lvwFolder.SmallImageList = Me.imlFolder
        Me.lvwFolder.TabIndex = 0
        Me.lvwFolder.UseCompatibleStateImageBehavior = False
        Me.lvwFolder.View = System.Windows.Forms.View.List
        '
        'imlFolder
        '
        Me.imlFolder.ImageStream = CType(resources.GetObject("imlFolder.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlFolder.TransparentColor = System.Drawing.Color.Fuchsia
        Me.imlFolder.Images.SetKeyName(0, "Folder")
        Me.imlFolder.Images.SetKeyName(1, "Up")
        '
        'tlpPath
        '
        Me.tlpPath.AutoSize = True
        Me.tlpPath.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tlpPath.ColumnCount = 3
        Me.tlpPath.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpPath.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpPath.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.tlpPath.Controls.Add(Me.cmdBrowse, 0, 0)
        Me.tlpPath.Controls.Add(Me.cmdGo, 0, 0)
        Me.tlpPath.Controls.Add(Me.txtPath, 0, 0)
        Me.tlpPath.Dock = System.Windows.Forms.DockStyle.Top
        Me.tlpPath.Location = New System.Drawing.Point(0, 0)
        Me.tlpPath.Name = "tlpPath"
        Me.tlpPath.RowCount = 1
        Me.tlpPath.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.tlpPath.Size = New System.Drawing.Size(230, 29)
        Me.tlpPath.TabIndex = 4
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdBrowse.AutoSize = True
        Me.cmdBrowse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdBrowse.Location = New System.Drawing.Point(201, 3)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(26, 23)
        Me.cmdBrowse.TabIndex = 0
        Me.cmdBrowse.Text = "&..."
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'cmdGo
        '
        Me.cmdGo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdGo.AutoSize = True
        Me.cmdGo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdGo.Image = Global.Metanol.My.Resources.Resources.GoLtrHS
        Me.cmdGo.Location = New System.Drawing.Point(173, 3)
        Me.cmdGo.Name = "cmdGo"
        Me.cmdGo.Size = New System.Drawing.Size(22, 22)
        Me.cmdGo.TabIndex = 3
        Me.cmdGo.UseVisualStyleBackColor = True
        '
        'txtPath
        '
        Me.txtPath.AcceptsReturn = True
        Me.txtPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
        Me.txtPath.Location = New System.Drawing.Point(3, 4)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(164, 20)
        Me.txtPath.TabIndex = 2
        '
        'fbdBrowse
        '
        Me.fbdBrowse.Description = "Select folder to show images from"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(692, 536)
        Me.Controls.Add(Me.splMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.Text = "Metanol"
        Me.splMain.Panel1.ResumeLayout(False)
        Me.splMain.Panel1.PerformLayout()
        Me.splMain.ResumeLayout(False)
        Me.splFolder.Panel1.ResumeLayout(False)
        Me.splFolder.Panel2.ResumeLayout(False)
        Me.splFolder.ResumeLayout(False)
        Me.tlpPath.ResumeLayout(False)
        Me.tlpPath.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents splMain As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents fbdBrowse As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents tlpPath As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents cmdGo As System.Windows.Forms.Button
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents splFolder As System.Windows.Forms.SplitContainer
    Friend WithEvents lvwImages As System.Windows.Forms.ListView
    Friend WithEvents lvwFolder As System.Windows.Forms.ListView
    Friend WithEvents imlFolder As System.Windows.Forms.ImageList
    Friend WithEvents imlImages As System.Windows.Forms.ImageList
    Friend WithEvents bgwThumb As System.ComponentModel.BackgroundWorker

End Class
