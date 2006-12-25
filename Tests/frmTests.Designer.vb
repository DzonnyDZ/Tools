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
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("ReadOnlyListAdapter")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Generic", New System.Windows.Forms.TreeNode() {TreeNode7})
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Collections", New System.Windows.Forms.TreeNode() {TreeNode8})
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("T1orT2")
        Dim TreeNode11 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Generic", New System.Windows.Forms.TreeNode() {TreeNode10})
        Dim TreeNode12 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("DataStructures", New System.Windows.Forms.TreeNode() {TreeNode11})
        Dim TreeNode13 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Tools", New System.Windows.Forms.TreeNode() {TreeNode3, TreeNode6, TreeNode9, TreeNode12})
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
        TreeNode7.Name = "ReadOnlyListAdapter"
        TreeNode7.Tag = "Tools.Tests.Collections.Generic.frmReadOnlyListAdapter.Test"
        TreeNode7.Text = "ReadOnlyListAdapter"
        TreeNode8.Name = "Generic"
        TreeNode8.Text = "Generic"
        TreeNode9.Name = "Collections"
        TreeNode9.Text = "Collections"
        TreeNode10.Name = "T1orT2"
        TreeNode10.Tag = "Tools.Tests.DataStructures.Generic.T1orT2.Test"
        TreeNode10.Text = "T1orT2"
        TreeNode11.Name = "Generic"
        TreeNode11.Text = "Generic"
        TreeNode12.Name = "DataStructures"
        TreeNode12.Text = "DataStructures"
        TreeNode13.Name = "Tools"
        TreeNode13.Text = "Tools"
        Me.tvwMain.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode13})
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
