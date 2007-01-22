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
        Me.components = New System.ComponentModel.Container
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Min")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Max")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Math", New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2})
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("iif")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Interaction", New System.Windows.Forms.TreeNode() {TreeNode4})
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("VisualBasic", New System.Windows.Forms.TreeNode() {TreeNode5})
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("ReadOnlyListAdapter")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("HashTable")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Generic", New System.Windows.Forms.TreeNode() {TreeNode7, TreeNode8})
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Collections", New System.Windows.Forms.TreeNode() {TreeNode9})
        Dim TreeNode11 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("LCategoryAttribute, LDescriptionAttribute, LDisplayNameAttribute")
        Dim TreeNode12 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("SettingsInheritDefaultValueAttribute , SettingsInheritDescriptionAttribute ")
        Dim TreeNode13 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("ComponentModel", New System.Windows.Forms.TreeNode() {TreeNode11, TreeNode12})
        Dim TreeNode14 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("T1orT2")
        Dim TreeNode15 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Generic", New System.Windows.Forms.TreeNode() {TreeNode14})
        Dim TreeNode16 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("DataStructures", New System.Windows.Forms.TreeNode() {TreeNode15})
        Dim TreeNode17 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("EncodingSelector")
        Dim TreeNode18 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("LinkLabel")
        Dim TreeNode19 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Forms", New System.Windows.Forms.TreeNode() {TreeNode17, TreeNode18})
        Dim TreeNode20 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Windows", New System.Windows.Forms.TreeNode() {TreeNode19})
        Dim TreeNode21 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Tools", New System.Windows.Forms.TreeNode() {TreeNode3, TreeNode6, TreeNode10, TreeNode13, TreeNode16, TreeNode20})
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTests))
        Me.tvwMain = New System.Windows.Forms.TreeView
        Me.imlImages = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'tvwMain
        '
        Me.tvwMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvwMain.ImageIndex = 0
        Me.tvwMain.ImageList = Me.imlImages
        Me.tvwMain.Location = New System.Drawing.Point(0, 0)
        Me.tvwMain.Name = "tvwMain"
        TreeNode1.ImageKey = "Method Overload"
        TreeNode1.Name = "Min"
        TreeNode1.SelectedImageKey = "Method Overload"
        TreeNode1.Tag = "Tools.Tests.Math.Min"
        TreeNode1.Text = "Min"
        TreeNode2.ImageKey = "Method Overload"
        TreeNode2.Name = "Max"
        TreeNode2.SelectedImageKey = "Method Overload"
        TreeNode2.Tag = "Tools.Tests.Math.Max"
        TreeNode2.Text = "Max"
        TreeNode3.ImageKey = "Class"
        TreeNode3.Name = "Math"
        TreeNode3.SelectedImageKey = "Class"
        TreeNode3.Text = "Math"
        TreeNode4.ImageKey = "Method"
        TreeNode4.Name = "iif"
        TreeNode4.SelectedImageKey = "Method"
        TreeNode4.Tag = "Tools.Tests.VisualBasic.Interaction.iif"
        TreeNode4.Text = "iif"
        TreeNode5.ImageKey = "Module"
        TreeNode5.Name = "Interaction"
        TreeNode5.SelectedImageKey = "Module"
        TreeNode5.Text = "Interaction"
        TreeNode6.ImageKey = "Namespace"
        TreeNode6.Name = "VisualBasic"
        TreeNode6.SelectedImageKey = "Namespace"
        TreeNode6.Text = "VisualBasic"
        TreeNode7.ImageKey = "Class"
        TreeNode7.Name = "ReadOnlyListAdapter"
        TreeNode7.SelectedImageKey = "Class"
        TreeNode7.Tag = "Tools.Tests.Collections.Generic.frmReadOnlyListAdapter.Test"
        TreeNode7.Text = "ReadOnlyListAdapter"
        TreeNode8.ImageKey = "Class"
        TreeNode8.Name = "HashTable"
        TreeNode8.SelectedImageKey = "Class"
        TreeNode8.Tag = "Tools.Tests.Collections.Generic.frmHashTable.Test"
        TreeNode8.Text = "HashTable"
        TreeNode9.ImageKey = "Namespace"
        TreeNode9.Name = "Generic"
        TreeNode9.SelectedImageKey = "Namespace"
        TreeNode9.Text = "Generic"
        TreeNode10.ImageKey = "Namespace"
        TreeNode10.Name = "Collections"
        TreeNode10.SelectedImageKey = "Namespace"
        TreeNode10.Text = "Collections"
        TreeNode11.ImageKey = "Class"
        TreeNode11.Name = "LocalizableAttributes"
        TreeNode11.SelectedImageKey = "Class"
        TreeNode11.Tag = "Tools.Tests.ComponentModel.frmLocalizableAttributes.Test"
        TreeNode11.Text = "LCategoryAttribute, LDescriptionAttribute, LDisplayNameAttribute"
        TreeNode12.ImageKey = "Class"
        TreeNode12.Name = "SettingsAttributes"
        TreeNode12.SelectedImageKey = "Class"
        TreeNode12.Tag = "Tools.Tests.ComponentModel.frmSettingsAttributes.Test"
        TreeNode12.Text = "SettingsInheritDefaultValueAttribute , SettingsInheritDescriptionAttribute "
        TreeNode13.ImageKey = "Namespace"
        TreeNode13.Name = "ComponentModel"
        TreeNode13.SelectedImageKey = "Namespace"
        TreeNode13.Text = "ComponentModel"
        TreeNode14.ImageKey = "Class"
        TreeNode14.Name = "T1orT2"
        TreeNode14.SelectedImageKey = "Class"
        TreeNode14.Tag = "Tools.Tests.DataStructures.Generic.T1orT2.Test"
        TreeNode14.Text = "T1orT2"
        TreeNode15.ImageKey = "Namespace"
        TreeNode15.Name = "Generic"
        TreeNode15.SelectedImageKey = "Namespace"
        TreeNode15.Text = "Generic"
        TreeNode16.ImageKey = "Namespace"
        TreeNode16.Name = "DataStructures"
        TreeNode16.SelectedImageKey = "Namespace"
        TreeNode16.Text = "DataStructures"
        TreeNode17.ImageKey = "Class"
        TreeNode17.Name = "EncodingSelector"
        TreeNode17.SelectedImageKey = "Class"
        TreeNode17.Tag = "Tools.Tests.Windows.Forms.frmEncodingSelector.Test"
        TreeNode17.Text = "EncodingSelector"
        TreeNode18.ImageKey = "Class"
        TreeNode18.Name = "LinkLabel"
        TreeNode18.SelectedImageKey = "Class"
        TreeNode18.Tag = "Tools.Tests.Windows.Forms.frmLinkLabel.Test"
        TreeNode18.Text = "LinkLabel"
        TreeNode19.ImageKey = "Namespace"
        TreeNode19.Name = "Forms"
        TreeNode19.SelectedImageKey = "Namespace"
        TreeNode19.Text = "Forms"
        TreeNode20.ImageKey = "Namespace"
        TreeNode20.Name = "Windows"
        TreeNode20.SelectedImageKey = "Namespace"
        TreeNode20.Text = "Windows"
        TreeNode21.ImageKey = "Assembly"
        TreeNode21.Name = "Tools"
        TreeNode21.SelectedImageKey = "Assembly"
        TreeNode21.Text = "Tools"
        Me.tvwMain.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode21})
        Me.tvwMain.SelectedImageIndex = 0
        Me.tvwMain.Size = New System.Drawing.Size(401, 249)
        Me.tvwMain.TabIndex = 0
        '
        'imlImages
        '
        Me.imlImages.ImageStream = CType(resources.GetObject("imlImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlImages.TransparentColor = System.Drawing.Color.Fuchsia
        Me.imlImages.Images.SetKeyName(0, "Value Type Private")
        Me.imlImages.Images.SetKeyName(1, "Assembly")
        Me.imlImages.Images.SetKeyName(2, "BSC")
        Me.imlImages.Images.SetKeyName(3, "Class")
        Me.imlImages.Images.SetKeyName(4, "Class Friend")
        Me.imlImages.Images.SetKeyName(5, "Class Private")
        Me.imlImages.Images.SetKeyName(6, "Class Protected")
        Me.imlImages.Images.SetKeyName(7, "Class Sealed (NotInheritable)")
        Me.imlImages.Images.SetKeyName(8, "Class Shortcut")
        Me.imlImages.Images.SetKeyName(9, "Constant")
        Me.imlImages.Images.SetKeyName(10, "Constant Friend")
        Me.imlImages.Images.SetKeyName(11, "Constant Private")
        Me.imlImages.Images.SetKeyName(12, "Constant Protected")
        Me.imlImages.Images.SetKeyName(13, "Constant Sealed (NotOverridable)")
        Me.imlImages.Images.SetKeyName(14, "Constant Shortcut")
        Me.imlImages.Images.SetKeyName(15, "Delegate")
        Me.imlImages.Images.SetKeyName(16, "Delegate Friend")
        Me.imlImages.Images.SetKeyName(17, "Delegate Private")
        Me.imlImages.Images.SetKeyName(18, "Delegate Protected")
        Me.imlImages.Images.SetKeyName(19, "Delegate Sealed (NotOverridable)")
        Me.imlImages.Images.SetKeyName(20, "Delegate Shortcut")
        Me.imlImages.Images.SetKeyName(21, "Dialog ID")
        Me.imlImages.Images.SetKeyName(22, "Enum")
        Me.imlImages.Images.SetKeyName(23, "Enum Friend")
        Me.imlImages.Images.SetKeyName(24, "Enum Protected")
        Me.imlImages.Images.SetKeyName(25, "Enum Sealed (NotInheritable)")
        Me.imlImages.Images.SetKeyName(26, "Enum Shortcut")
        Me.imlImages.Images.SetKeyName(27, "Enum Item")
        Me.imlImages.Images.SetKeyName(28, "Enum Item Friend")
        Me.imlImages.Images.SetKeyName(29, "Enum Item Private")
        Me.imlImages.Images.SetKeyName(30, "Enum Item Protected")
        Me.imlImages.Images.SetKeyName(31, "Enum Item Sealed (NotOverridable)")
        Me.imlImages.Images.SetKeyName(32, "Enum Item Shortcu")
        Me.imlImages.Images.SetKeyName(33, "Enum Private")
        Me.imlImages.Images.SetKeyName(34, "Event")
        Me.imlImages.Images.SetKeyName(35, "Event Friend")
        Me.imlImages.Images.SetKeyName(36, "Event Private")
        Me.imlImages.Images.SetKeyName(37, "Event Protected")
        Me.imlImages.Images.SetKeyName(38, "Event Sealed (NotOverridable)")
        Me.imlImages.Images.SetKeyName(39, "Event Shortcut")
        Me.imlImages.Images.SetKeyName(40, "Exception")
        Me.imlImages.Images.SetKeyName(41, "Exception Friend")
        Me.imlImages.Images.SetKeyName(42, "Exception Protected")
        Me.imlImages.Images.SetKeyName(43, "Exception Sealed (NotInheritable)")
        Me.imlImages.Images.SetKeyName(44, "Exception Shortcut")
        Me.imlImages.Images.SetKeyName(45, "Exception Private")
        Me.imlImages.Images.SetKeyName(46, "Field")
        Me.imlImages.Images.SetKeyName(47, "Field Friend")
        Me.imlImages.Images.SetKeyName(48, "Field Private")
        Me.imlImages.Images.SetKeyName(49, "Field Protected")
        Me.imlImages.Images.SetKeyName(50, "Field Sealed (NotOverridable)")
        Me.imlImages.Images.SetKeyName(51, "Field Shortcut")
        Me.imlImages.Images.SetKeyName(52, "Interface")
        Me.imlImages.Images.SetKeyName(53, "Interface Friend")
        Me.imlImages.Images.SetKeyName(54, "Interface Private")
        Me.imlImages.Images.SetKeyName(55, "Interface Protected")
        Me.imlImages.Images.SetKeyName(56, "Interface Sealed (NotInheritable)")
        Me.imlImages.Images.SetKeyName(57, "Interface Shortcut")
        Me.imlImages.Images.SetKeyName(58, "Library")
        Me.imlImages.Images.SetKeyName(59, "Macro")
        Me.imlImages.Images.SetKeyName(60, "Macro Friend")
        Me.imlImages.Images.SetKeyName(61, "Macro Private")
        Me.imlImages.Images.SetKeyName(62, "Macro Protected")
        Me.imlImages.Images.SetKeyName(63, "Macro Sealed (NotOverridable)")
        Me.imlImages.Images.SetKeyName(64, "Macro Shortcut")
        Me.imlImages.Images.SetKeyName(65, "Map")
        Me.imlImages.Images.SetKeyName(66, "Map Friend")
        Me.imlImages.Images.SetKeyName(67, "Map Private")
        Me.imlImages.Images.SetKeyName(68, "Map Protected")
        Me.imlImages.Images.SetKeyName(69, "Map Sealed (NotInheritable)")
        Me.imlImages.Images.SetKeyName(70, "Map Shortcut")
        Me.imlImages.Images.SetKeyName(71, "Map Item")
        Me.imlImages.Images.SetKeyName(72, "Map Item Friend")
        Me.imlImages.Images.SetKeyName(73, "Map Item Private")
        Me.imlImages.Images.SetKeyName(74, "Map Item Protected")
        Me.imlImages.Images.SetKeyName(75, "Map Item Sealed (NotOverridable)")
        Me.imlImages.Images.SetKeyName(76, "Map Item Shortcut")
        Me.imlImages.Images.SetKeyName(77, "Method")
        Me.imlImages.Images.SetKeyName(78, "Method Friend")
        Me.imlImages.Images.SetKeyName(79, "Method Private")
        Me.imlImages.Images.SetKeyName(80, "Method Protected")
        Me.imlImages.Images.SetKeyName(81, "Method Sealed (NotOverridable)")
        Me.imlImages.Images.SetKeyName(82, "Method Shortcut")
        Me.imlImages.Images.SetKeyName(83, "Method Overload")
        Me.imlImages.Images.SetKeyName(84, "Method Overload Friend")
        Me.imlImages.Images.SetKeyName(85, "Method Overload Private")
        Me.imlImages.Images.SetKeyName(86, "Method Overload Protected")
        Me.imlImages.Images.SetKeyName(87, "Method Overload Sealed (NotOverridable)")
        Me.imlImages.Images.SetKeyName(88, "Method Overload Shortcut")
        Me.imlImages.Images.SetKeyName(89, "Module")
        Me.imlImages.Images.SetKeyName(90, "Module Friend")
        Me.imlImages.Images.SetKeyName(91, "Module Private")
        Me.imlImages.Images.SetKeyName(92, "Module Protected")
        Me.imlImages.Images.SetKeyName(93, "Module Sealed (NotInheritable)")
        Me.imlImages.Images.SetKeyName(94, "Module Shortcut")
        Me.imlImages.Images.SetKeyName(95, "Namespace")
        Me.imlImages.Images.SetKeyName(96, "Namespace Friend")
        Me.imlImages.Images.SetKeyName(97, "Namespace Sealed (NotInheritable)")
        Me.imlImages.Images.SetKeyName(98, "Namespace Shortcut")
        Me.imlImages.Images.SetKeyName(99, "Namespace Private")
        Me.imlImages.Images.SetKeyName(100, "Namespace Protected")
        Me.imlImages.Images.SetKeyName(101, "Object")
        Me.imlImages.Images.SetKeyName(102, "Object Friend")
        Me.imlImages.Images.SetKeyName(103, "Object Private")
        Me.imlImages.Images.SetKeyName(104, "Object Protected")
        Me.imlImages.Images.SetKeyName(105, "Object Sealed (NotInheritable)")
        Me.imlImages.Images.SetKeyName(106, "Object Shortcut")
        Me.imlImages.Images.SetKeyName(107, "Operator")
        Me.imlImages.Images.SetKeyName(108, "Operator Friend")
        Me.imlImages.Images.SetKeyName(109, "Operator Private")
        Me.imlImages.Images.SetKeyName(110, "Operator Protected")
        Me.imlImages.Images.SetKeyName(111, "Operator Sealed (NotOverridable)")
        Me.imlImages.Images.SetKeyName(112, "Operator Shortcut")
        Me.imlImages.Images.SetKeyName(113, "Properties")
        Me.imlImages.Images.SetKeyName(114, "Properties Friend")
        Me.imlImages.Images.SetKeyName(115, "Properties Private")
        Me.imlImages.Images.SetKeyName(116, "Properties Protected")
        Me.imlImages.Images.SetKeyName(117, "Properties Sealed (NotOverridable)")
        Me.imlImages.Images.SetKeyName(118, "Properties Shortcut")
        Me.imlImages.Images.SetKeyName(119, "Structure")
        Me.imlImages.Images.SetKeyName(120, "Structure Friend")
        Me.imlImages.Images.SetKeyName(121, "Structure Private")
        Me.imlImages.Images.SetKeyName(122, "Structure Protected")
        Me.imlImages.Images.SetKeyName(123, "Structure Sealed (NotInheritable)")
        Me.imlImages.Images.SetKeyName(124, "Structure Shortcut")
        Me.imlImages.Images.SetKeyName(125, "Template")
        Me.imlImages.Images.SetKeyName(126, "Template Friend")
        Me.imlImages.Images.SetKeyName(127, "Template Private")
        Me.imlImages.Images.SetKeyName(128, "Template Protected")
        Me.imlImages.Images.SetKeyName(129, "Template Sealed (NotOverridable)")
        Me.imlImages.Images.SetKeyName(130, "Template Shortcut")
        Me.imlImages.Images.SetKeyName(131, "Type")
        Me.imlImages.Images.SetKeyName(132, "Type Friend")
        Me.imlImages.Images.SetKeyName(133, "Type Private")
        Me.imlImages.Images.SetKeyName(134, "Type Protected")
        Me.imlImages.Images.SetKeyName(135, "Type Sealed")
        Me.imlImages.Images.SetKeyName(136, "Type Shortcut")
        Me.imlImages.Images.SetKeyName(137, "TypeDef")
        Me.imlImages.Images.SetKeyName(138, "TypeDef Friend")
        Me.imlImages.Images.SetKeyName(139, "TypeDef Private")
        Me.imlImages.Images.SetKeyName(140, "TypeDef Protected")
        Me.imlImages.Images.SetKeyName(141, "TypeDef Sealed (NotOverridable)")
        Me.imlImages.Images.SetKeyName(142, "TypeDef Shortcut")
        Me.imlImages.Images.SetKeyName(143, "Union")
        Me.imlImages.Images.SetKeyName(144, "Union Friend")
        Me.imlImages.Images.SetKeyName(145, "Union Protected")
        Me.imlImages.Images.SetKeyName(146, "Union Sealed (NotInheritable)")
        Me.imlImages.Images.SetKeyName(147, "Union Shortcut")
        Me.imlImages.Images.SetKeyName(148, "Union Private")
        Me.imlImages.Images.SetKeyName(149, "Value Type")
        Me.imlImages.Images.SetKeyName(150, "Value Type Friend")
        Me.imlImages.Images.SetKeyName(151, "Value Type Protected")
        Me.imlImages.Images.SetKeyName(152, "Value Type Sealed")
        Me.imlImages.Images.SetKeyName(153, "Value Type Shortcut")
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
    Friend WithEvents imlImages As System.Windows.Forms.ImageList

End Class
