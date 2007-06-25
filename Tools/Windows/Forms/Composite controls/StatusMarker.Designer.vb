Namespace WindowsT.FormsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class StatusMarker
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StatusMarker))
            Me.imlImages = New System.Windows.Forms.ImageList(Me.components)
            Me.cmsStatus = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.tmiAdd = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiReset = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiMarkAsChanged = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiDelete = New System.Windows.Forms.ToolStripMenuItem
            Me.totToolTip = New System.Windows.Forms.ToolTip(Me.components)
            Me.cmdStatus = New System.Windows.Forms.Button
            Me.cmsStatus.SuspendLayout()
            Me.SuspendLayout()
            '
            'imlImages
            '
            Me.imlImages.ImageStream = CType(resources.GetObject("imlImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imlImages.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imlImages.Images.SetKeyName(0, "Normal")
            Me.imlImages.Images.SetKeyName(1, "New")
            Me.imlImages.Images.SetKeyName(2, "Changed")
            Me.imlImages.Images.SetKeyName(3, "Deleted")
            Me.imlImages.Images.SetKeyName(4, "Error")
            Me.imlImages.Images.SetKeyName(5, "N/A")
            Me.imlImages.Images.SetKeyName(6, "Null")
            '
            'cmsStatus
            '
            Me.cmsStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiAdd, Me.tmiReset, Me.tmiMarkAsChanged, Me.tmiDelete})
            Me.cmsStatus.Name = "cmsStatus"
            resources.ApplyResources(Me.cmsStatus, "cmsStatus")
            '
            'tmiAdd
            '
            resources.ApplyResources(Me.tmiAdd, "tmiAdd")
            Me.tmiAdd.Image = Global.Tools.My.Resources.Resources.NewReportHS
            Me.tmiAdd.Name = "tmiAdd"
            '
            'tmiReset
            '
            Me.tmiReset.Image = Global.Tools.My.Resources.Resources.RepeatHS
            Me.tmiReset.Name = "tmiReset"
            resources.ApplyResources(Me.tmiReset, "tmiReset")
            '
            'tmiMarkAsChanged
            '
            resources.ApplyResources(Me.tmiMarkAsChanged, "tmiMarkAsChanged")
            Me.tmiMarkAsChanged.Image = Global.Tools.My.Resources.Resources.CommentHS
            Me.tmiMarkAsChanged.Name = "tmiMarkAsChanged"
            '
            'tmiDelete
            '
            Me.tmiDelete.Image = Global.Tools.My.Resources.Resources.DeleteHS
            Me.tmiDelete.Name = "tmiDelete"
            resources.ApplyResources(Me.tmiDelete, "tmiDelete")
            '
            'cmdStatus
            '
            resources.ApplyResources(Me.cmdStatus, "cmdStatus")
            Me.cmdStatus.ImageList = Me.imlImages
            Me.cmdStatus.Name = "cmdStatus"
            Me.cmdStatus.UseVisualStyleBackColor = True
            '
            'StatusMarker
            '
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.cmdStatus)
            Me.Name = "StatusMarker"
            Me.cmsStatus.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents cmdStatus As System.Windows.Forms.Button
        Protected WithEvents imlImages As System.Windows.Forms.ImageList
        Protected WithEvents cmsStatus As System.Windows.Forms.ContextMenuStrip
        Protected WithEvents tmiDelete As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiReset As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiAdd As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents totToolTip As System.Windows.Forms.ToolTip
        Friend WithEvents tmiMarkAsChanged As System.Windows.Forms.ToolStripMenuItem

    End Class
End Namespace