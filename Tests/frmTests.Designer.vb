<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTests
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Min")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Max")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Math", New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2})
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("iif")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Interaction", New System.Windows.Forms.TreeNode() {TreeNode4})
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("VisualBasic", New System.Windows.Forms.TreeNode() {TreeNode5})
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Tools", New System.Windows.Forms.TreeNode() {TreeNode3, TreeNode6})
        Me.tvwMain = New System.Windows.Forms.TreeView
        Me.SuspendLayout()
        '
        'tvwMain
        '
        Me.tvwMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvwMain.Location = New System.Drawing.Point(0, 0)
        Me.tvwMain.Name = "tvwMain"
        TreeNode1.Name = "Min"
        TreeNode1.Tag = "Tools.Tests.Math.Min"
        TreeNode1.Text = "Min"
        TreeNode2.Name = "Max"
        TreeNode2.Tag = "Tools.Tests.Math.Max"
        TreeNode2.Text = "Max"
        TreeNode3.Name = "Math"
        TreeNode3.Text = "Math"
        TreeNode4.Name = "iif"
        TreeNode4.Tag = "Tools.Tests.VisualBasic.Interaction.iif"
        TreeNode4.Text = "iif"
        TreeNode5.Name = "Interaction"
        TreeNode5.Text = "Interaction"
        TreeNode6.Name = "VisualBasic"
        TreeNode6.Text = "VisualBasic"
        TreeNode7.Name = "Tools"
        TreeNode7.Text = "Tools"
        Me.tvwMain.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode7})
        Me.tvwMain.Size = New System.Drawing.Size(401, 249)
        Me.tvwMain.TabIndex = 0
        '
        'frmTests
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(401, 249)
        Me.Controls.Add(Me.tvwMain)
        Me.Name = "frmTests"
        Me.Text = "Tests for project Tools"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tvwMain As System.Windows.Forms.TreeView

End Class
