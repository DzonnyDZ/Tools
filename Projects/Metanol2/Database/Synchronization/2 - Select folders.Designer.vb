Namespace Data
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class SelectFoldersStep
        Inherits System.Windows.Forms.UserControl

        'UserControl overrides dispose to clean up the component list.
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectFoldersStep))
            Me.tlpLayout = New System.Windows.Forms.TableLayoutPanel
            Me.lblName = New System.Windows.Forms.Label
            Me.lblFolder = New System.Windows.Forms.Label
            Me.cmdFolder = New System.Windows.Forms.Button
            Me.txtFolder = New System.Windows.Forms.TextBox
            Me.cmdAddFolder = New System.Windows.Forms.Button
            Me.fraFolders = New System.Windows.Forms.GroupBox
            Me.lvwFolders = New System.Windows.Forms.ListView
            Me.imlFolderImages = New System.Windows.Forms.ImageList(Me.components)
            Me.lblI = New System.Windows.Forms.Label
            Me.fbdFolders = New System.Windows.Forms.FolderBrowserDialog
            Me.tlpLayout.SuspendLayout()
            Me.fraFolders.SuspendLayout()
            Me.SuspendLayout()
            '
            'tlpLayout
            '
            resources.ApplyResources(Me.tlpLayout, "tlpLayout")
            Me.tlpLayout.Controls.Add(Me.lblName, 0, 0)
            Me.tlpLayout.Controls.Add(Me.lblFolder, 0, 2)
            Me.tlpLayout.Controls.Add(Me.cmdFolder, 2, 2)
            Me.tlpLayout.Controls.Add(Me.txtFolder, 1, 2)
            Me.tlpLayout.Controls.Add(Me.cmdAddFolder, 3, 2)
            Me.tlpLayout.Controls.Add(Me.fraFolders, 0, 3)
            Me.tlpLayout.Controls.Add(Me.lblI, 0, 1)
            Me.tlpLayout.Name = "tlpLayout"
            '
            'lblName
            '
            resources.ApplyResources(Me.lblName, "lblName")
            Me.tlpLayout.SetColumnSpan(Me.lblName, 4)
            Me.lblName.Name = "lblName"
            '
            'lblFolder
            '
            resources.ApplyResources(Me.lblFolder, "lblFolder")
            Me.lblFolder.Name = "lblFolder"
            '
            'cmdFolder
            '
            resources.ApplyResources(Me.cmdFolder, "cmdFolder")
            Me.cmdFolder.Name = "cmdFolder"
            Me.cmdFolder.UseVisualStyleBackColor = True
            '
            'txtFolder
            '
            resources.ApplyResources(Me.txtFolder, "txtFolder")
            Me.txtFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
            Me.txtFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
            Me.txtFolder.Name = "txtFolder"
            '
            'cmdAddFolder
            '
            resources.ApplyResources(Me.cmdAddFolder, "cmdAddFolder")
            Me.cmdAddFolder.Name = "cmdAddFolder"
            Me.cmdAddFolder.UseVisualStyleBackColor = True
            '
            'fraFolders
            '
            Me.tlpLayout.SetColumnSpan(Me.fraFolders, 4)
            Me.fraFolders.Controls.Add(Me.lvwFolders)
            resources.ApplyResources(Me.fraFolders, "fraFolders")
            Me.fraFolders.Name = "fraFolders"
            Me.fraFolders.TabStop = False
            '
            'lvwFolders
            '
            resources.ApplyResources(Me.lvwFolders, "lvwFolders")
            Me.lvwFolders.Name = "lvwFolders"
            Me.lvwFolders.SmallImageList = Me.imlFolderImages
            Me.lvwFolders.UseCompatibleStateImageBehavior = False
            Me.lvwFolders.View = System.Windows.Forms.View.List
            '
            'imlFolderImages
            '
            Me.imlFolderImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
            resources.ApplyResources(Me.imlFolderImages, "imlFolderImages")
            Me.imlFolderImages.TransparentColor = System.Drawing.Color.Transparent
            '
            'lblI
            '
            resources.ApplyResources(Me.lblI, "lblI")
            Me.tlpLayout.SetColumnSpan(Me.lblI, 4)
            Me.lblI.Name = "lblI"
            '
            'SelectFoldersStep
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.tlpLayout)
            Me.Name = "SelectFoldersStep"
            Me.tlpLayout.ResumeLayout(False)
            Me.tlpLayout.PerformLayout()
            Me.fraFolders.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Private WithEvents tlpLayout As System.Windows.Forms.TableLayoutPanel
        Private WithEvents lblName As System.Windows.Forms.Label
        Private WithEvents lblFolder As System.Windows.Forms.Label
        Private WithEvents txtFolder As System.Windows.Forms.TextBox
        Private WithEvents cmdFolder As System.Windows.Forms.Button
        Private WithEvents cmdAddFolder As System.Windows.Forms.Button
        Private WithEvents fbdFolders As System.Windows.Forms.FolderBrowserDialog
        Private WithEvents fraFolders As System.Windows.Forms.GroupBox
        Private WithEvents lvwFolders As System.Windows.Forms.ListView
        Private WithEvents lblI As System.Windows.Forms.Label
        Private WithEvents imlFolderImages As System.Windows.Forms.ImageList

    End Class
End Namespace