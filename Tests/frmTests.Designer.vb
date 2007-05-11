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
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("ReadOnlyListAdapter")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("HashTable")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Generic", New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2})
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Collections", New System.Windows.Forms.TreeNode() {TreeNode3})
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("LCategoryAttribute, LDescriptionAttribute, LDisplayNameAttribute")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("SettingsInheritDefaultValueAttribute , SettingsInheritDescriptionAttribute ")
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("ComponentModel", New System.Windows.Forms.TreeNode() {TreeNode5, TreeNode6})
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("T1orT2")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Generic", New System.Windows.Forms.TreeNode() {TreeNode8})
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("DataStructures", New System.Windows.Forms.TreeNode() {TreeNode9})
        Dim TreeNode11 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("JPEG")
        Dim TreeNode12 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("IO", New System.Windows.Forms.TreeNode() {TreeNode11})
        Dim TreeNode13 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("SystemColorsExtension")
        Dim TreeNode14 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Drawing", New System.Windows.Forms.TreeNode() {TreeNode12, TreeNode13})
        Dim TreeNode15 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("BinaryReader, ConstrainedReadOnlyStream")
        Dim TreeNode16 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("IO", New System.Windows.Forms.TreeNode() {TreeNode15})
        Dim TreeNode17 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Min")
        Dim TreeNode18 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Max")
        Dim TreeNode19 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Math", New System.Windows.Forms.TreeNode() {TreeNode17, TreeNode18})
        Dim TreeNode20 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("TimesSpanFormattable")
        Dim TreeNode21 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("iif")
        Dim TreeNode22 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Interaction", New System.Windows.Forms.TreeNode() {TreeNode21})
        Dim TreeNode23 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("VisualBasic", New System.Windows.Forms.TreeNode() {TreeNode22})
        Dim TreeNode24 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("EncodingSelector")
        Dim TreeNode25 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("LinkLabel")
        Dim TreeNode26 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Forms", New System.Windows.Forms.TreeNode() {TreeNode24, TreeNode25})
        Dim TreeNode27 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Windows", New System.Windows.Forms.TreeNode() {TreeNode26})
        Dim TreeNode28 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Tools", New System.Windows.Forms.TreeNode() {TreeNode4, TreeNode7, TreeNode10, TreeNode14, TreeNode16, TreeNode19, TreeNode20, TreeNode23, TreeNode27})
        Dim TreeNode29 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("ExtendedForm")
        Dim TreeNode30 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("LinkProperties")
        Dim TreeNode31 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Utilities", New System.Windows.Forms.TreeNode() {TreeNode30})
        Dim TreeNode32 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Forms", New System.Windows.Forms.TreeNode() {TreeNode29, TreeNode31})
        Dim TreeNode33 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Windows", New System.Windows.Forms.TreeNode() {TreeNode32})
        Dim TreeNode34 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Tools Win", New System.Windows.Forms.TreeNode() {TreeNode33})
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
        TreeNode1.ImageKey = "Class"
        TreeNode1.Name = "ReadOnlyListAdapter"
        TreeNode1.SelectedImageKey = "Class"
        TreeNode1.Tag = "Tools.Tests.Collections.Generic.frmReadOnlyListAdapter.Test"
        TreeNode1.Text = "ReadOnlyListAdapter"
        TreeNode2.ImageKey = "Class"
        TreeNode2.Name = "HashTable"
        TreeNode2.SelectedImageKey = "Class"
        TreeNode2.Tag = "Tools.Tests.Collections.Generic.frmHashTable.Test"
        TreeNode2.Text = "HashTable"
        TreeNode3.ImageKey = "Namespace"
        TreeNode3.Name = "Generic"
        TreeNode3.SelectedImageKey = "Namespace"
        TreeNode3.Text = "Generic"
        TreeNode4.ImageKey = "Namespace"
        TreeNode4.Name = "Collections"
        TreeNode4.SelectedImageKey = "Namespace"
        TreeNode4.Text = "Collections"
        TreeNode5.ImageKey = "Class"
        TreeNode5.Name = "LocalizableAttributes"
        TreeNode5.SelectedImageKey = "Class"
        TreeNode5.Tag = "Tools.Tests.ComponentModel.frmLocalizableAttributes.Test"
        TreeNode5.Text = "LCategoryAttribute, LDescriptionAttribute, LDisplayNameAttribute"
        TreeNode6.ImageKey = "Class"
        TreeNode6.Name = "SettingsAttributes"
        TreeNode6.SelectedImageKey = "Class"
        TreeNode6.Tag = "Tools.Tests.ComponentModel.frmSettingsAttributes.Test"
        TreeNode6.Text = "SettingsInheritDefaultValueAttribute , SettingsInheritDescriptionAttribute "
        TreeNode7.ImageKey = "Namespace"
        TreeNode7.Name = "ComponentModel"
        TreeNode7.SelectedImageKey = "Namespace"
        TreeNode7.Text = "ComponentModel"
        TreeNode8.ImageKey = "Class"
        TreeNode8.Name = "T1orT2"
        TreeNode8.SelectedImageKey = "Class"
        TreeNode8.Tag = "Tools.Tests.DataStructures.Generic.T1orT2.Test"
        TreeNode8.Text = "T1orT2"
        TreeNode9.ImageKey = "Namespace"
        TreeNode9.Name = "Generic"
        TreeNode9.SelectedImageKey = "Namespace"
        TreeNode9.Text = "Generic"
        TreeNode10.ImageKey = "Namespace"
        TreeNode10.Name = "DataStructures"
        TreeNode10.SelectedImageKey = "Namespace"
        TreeNode10.Text = "DataStructures"
        TreeNode11.ImageKey = "Namespace"
        TreeNode11.Name = "JPEG"
        TreeNode11.SelectedImageKey = "Namespace"
        TreeNode11.Tag = "Tools.Tests.Drawing.IO.frmJPEG.test"
        TreeNode11.Text = "JPEG"
        TreeNode12.ImageKey = "Namespace"
        TreeNode12.Name = "IO"
        TreeNode12.SelectedImageKey = "Namespace"
        TreeNode12.Text = "IO"
        TreeNode13.ImageKey = "Class Sealed (NotInheritable)"
        TreeNode13.Name = "SystemColorsExtension"
        TreeNode13.SelectedImageKey = "Class Sealed (NotInheritable)"
        TreeNode13.Tag = "Tools.Tests.Windows.Forms.Utilities.frmLinkProperties.Test"
        TreeNode13.Text = "SystemColorsExtension"
        TreeNode14.ImageKey = "Namespace"
        TreeNode14.Name = "Drawing"
        TreeNode14.SelectedImageKey = "Namespace"
        TreeNode14.Text = "Drawing"
        TreeNode15.ImageKey = "Class"
        TreeNode15.Name = "BinaryReader, ConstrainedReadOnlyStream"
        TreeNode15.SelectedImageKey = "Class"
        TreeNode15.Tag = "Tools.Tests.Drawing.IO.frmJPEG.test"
        TreeNode15.Text = "BinaryReader, ConstrainedReadOnlyStream"
        TreeNode16.ImageKey = "Namespace"
        TreeNode16.Name = "IO"
        TreeNode16.SelectedImageKey = "Namespace"
        TreeNode16.Text = "IO"
        TreeNode17.ImageKey = "Method Overload"
        TreeNode17.Name = "Min"
        TreeNode17.SelectedImageKey = "Method Overload"
        TreeNode17.Tag = "Tools.Tests.Math.Min"
        TreeNode17.Text = "Min"
        TreeNode18.ImageKey = "Method Overload"
        TreeNode18.Name = "Max"
        TreeNode18.SelectedImageKey = "Method Overload"
        TreeNode18.Tag = "Tools.Tests.Math.Max"
        TreeNode18.Text = "Max"
        TreeNode19.ImageKey = "Class"
        TreeNode19.Name = "Math"
        TreeNode19.SelectedImageKey = "Class"
        TreeNode19.Text = "Math"
        TreeNode20.ImageKey = "Structure"
        TreeNode20.Name = "TimesSpanFormattable"
        TreeNode20.SelectedImageKey = "Structure"
        TreeNode20.Tag = "Tools.Tests.frmTimeSpanFormattable.Test"
        TreeNode20.Text = "TimesSpanFormattable"
        TreeNode21.ImageKey = "Method"
        TreeNode21.Name = "iif"
        TreeNode21.SelectedImageKey = "Method"
        TreeNode21.Tag = "Tools.Tests.VisualBasic.Interaction.iif"
        TreeNode21.Text = "iif"
        TreeNode22.ImageKey = "Module"
        TreeNode22.Name = "Interaction"
        TreeNode22.SelectedImageKey = "Module"
        TreeNode22.Text = "Interaction"
        TreeNode23.ImageKey = "Namespace"
        TreeNode23.Name = "VisualBasic"
        TreeNode23.SelectedImageKey = "Namespace"
        TreeNode23.Text = "VisualBasic"
        TreeNode24.ImageKey = "Class"
        TreeNode24.Name = "EncodingSelector"
        TreeNode24.SelectedImageKey = "Class"
        TreeNode24.Tag = "Tools.Tests.Windows.Forms.frmEncodingSelector.Test"
        TreeNode24.Text = "EncodingSelector"
        TreeNode25.ImageKey = "Class"
        TreeNode25.Name = "LinkLabel"
        TreeNode25.SelectedImageKey = "Class"
        TreeNode25.Tag = "Tools.Tests.Windows.Forms.frmLinkLabel.Test"
        TreeNode25.Text = "LinkLabel"
        TreeNode26.ImageKey = "Namespace"
        TreeNode26.Name = "Forms"
        TreeNode26.SelectedImageKey = "Namespace"
        TreeNode26.Text = "Forms"
        TreeNode27.ImageKey = "Namespace"
        TreeNode27.Name = "Windows"
        TreeNode27.SelectedImageKey = "Namespace"
        TreeNode27.Text = "Windows"
        TreeNode28.ImageKey = "Assembly"
        TreeNode28.Name = "Tools"
        TreeNode28.SelectedImageKey = "Assembly"
        TreeNode28.Text = "Tools"
        TreeNode29.ImageKey = "Class"
        TreeNode29.Name = "ExtendedForm"
        TreeNode29.SelectedImageKey = "Class"
        TreeNode29.Tag = "Tools.Tests.Windows.Forms.frmExtendedForm.Test"
        TreeNode29.Text = "ExtendedForm"
        TreeNode30.ImageKey = "Class Sealed (NotInheritable)"
        TreeNode30.Name = "LinkProperties"
        TreeNode30.SelectedImageKey = "Class Sealed (NotInheritable)"
        TreeNode30.Tag = "Tools.Tests.Windows.Forms.Utilities.frmLinkProperties.Test"
        TreeNode30.Text = "LinkProperties"
        TreeNode31.ImageKey = "Namespace"
        TreeNode31.Name = "Utilities"
        TreeNode31.SelectedImageKey = "Namespace"
        TreeNode31.Text = "Utilities"
        TreeNode32.ImageKey = "Namespace"
        TreeNode32.Name = "Forms"
        TreeNode32.SelectedImageKey = "Namespace"
        TreeNode32.Text = "Forms"
        TreeNode33.ImageKey = "Namespace"
        TreeNode33.Name = "Windows"
        TreeNode33.SelectedImageKey = "Namespace"
        TreeNode33.Text = "Windows"
        TreeNode34.ImageKey = "Assembly"
        TreeNode34.Name = "Tools Win"
        TreeNode34.SelectedImageKey = "Assembly"
        TreeNode34.Text = "Tools Win"
        Me.tvwMain.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode28, TreeNode34})
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
