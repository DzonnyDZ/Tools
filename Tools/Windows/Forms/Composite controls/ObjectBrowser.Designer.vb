<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ObjectBrowser
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
        Me.tvwObjects = New System.Windows.Forms.TreeView
        Me.lvwMembers = New System.Windows.Forms.ListView
        Me.splMain = New System.Windows.Forms.SplitContainer
        Me.splRight = New System.Windows.Forms.SplitContainer
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.splMain.Panel1.SuspendLayout()
        Me.splMain.Panel2.SuspendLayout()
        Me.splMain.SuspendLayout()
        Me.splRight.Panel1.SuspendLayout()
        Me.splRight.SuspendLayout()
        Me.SuspendLayout()
        '
        'tvwObjects
        '
        Me.tvwObjects.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvwObjects.Location = New System.Drawing.Point(0, 0)
        Me.tvwObjects.Name = "tvwObjects"
        Me.tvwObjects.Size = New System.Drawing.Size(314, 530)
        Me.tvwObjects.TabIndex = 0
        '
        'lvwMembers
        '
        Me.lvwMembers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwMembers.Location = New System.Drawing.Point(0, 0)
        Me.lvwMembers.Name = "lvwMembers"
        Me.lvwMembers.Size = New System.Drawing.Size(431, 340)
        Me.lvwMembers.TabIndex = 1
        Me.lvwMembers.UseCompatibleStateImageBehavior = False
        Me.lvwMembers.View = System.Windows.Forms.View.Details
        '
        'splMain
        '
        Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splMain.Location = New System.Drawing.Point(0, 0)
        Me.splMain.Name = "splMain"
        '
        'splMain.Panel1
        '
        Me.splMain.Panel1.Controls.Add(Me.tvwObjects)
        '
        'splMain.Panel2
        '
        Me.splMain.Panel2.Controls.Add(Me.splRight)
        Me.splMain.Size = New System.Drawing.Size(749, 530)
        Me.splMain.SplitterDistance = 314
        Me.splMain.TabIndex = 2
        '
        'splRight
        '
        Me.splRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splRight.Location = New System.Drawing.Point(0, 0)
        Me.splRight.Name = "splRight"
        Me.splRight.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splRight.Panel1
        '
        Me.splRight.Panel1.Controls.Add(Me.lvwMembers)
        Me.splRight.Size = New System.Drawing.Size(431, 530)
        Me.splRight.SplitterDistance = 340
        Me.splRight.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'ObjectBrowser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.splMain)
        Me.Name = "ObjectBrowser"
        Me.Size = New System.Drawing.Size(749, 530)
        Me.splMain.Panel1.ResumeLayout(False)
        Me.splMain.Panel2.ResumeLayout(False)
        Me.splMain.ResumeLayout(False)
        Me.splRight.Panel1.ResumeLayout(False)
        Me.splRight.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tvwObjects As System.Windows.Forms.TreeView
    Friend WithEvents lvwMembers As System.Windows.Forms.ListView
    Friend WithEvents splMain As System.Windows.Forms.SplitContainer
    Friend WithEvents splRight As System.Windows.Forms.SplitContainer
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList

End Class
