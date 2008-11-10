'Ahoj
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTests))
        Me.tvwMain = New System.Windows.Forms.TreeView
        Me.imlImages = New System.Windows.Forms.ImageList(Me.components)
        Me.cmdExport = New System.Windows.Forms.Button
        Me.flpBottom = New System.Windows.Forms.FlowLayoutPanel
        Me.cmdInfo = New System.Windows.Forms.Button
        Me.sfdXml = New System.Windows.Forms.SaveFileDialog
        Me.flpBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'tvwMain
        '
        Me.tvwMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvwMain.ImageIndex = 0
        Me.tvwMain.ImageList = Me.imlImages
        Me.tvwMain.Location = New System.Drawing.Point(0, 0)
        Me.tvwMain.Name = "tvwMain"
        Me.tvwMain.SelectedImageIndex = 0
        Me.tvwMain.Size = New System.Drawing.Size(401, 226)
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
        'cmdExport
        '
        Me.cmdExport.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdExport.AutoSize = True
        Me.cmdExport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdExport.Location = New System.Drawing.Point(333, 0)
        Me.cmdExport.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdExport.Name = "cmdExport"
        Me.cmdExport.Size = New System.Drawing.Size(68, 23)
        Me.cmdExport.TabIndex = 1
        Me.cmdExport.Text = "Export tree"
        Me.cmdExport.UseVisualStyleBackColor = True
        '
        'flpBottom
        '
        Me.flpBottom.AutoSize = True
        Me.flpBottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.flpBottom.Controls.Add(Me.cmdExport)
        Me.flpBottom.Controls.Add(Me.cmdInfo)
        Me.flpBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.flpBottom.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.flpBottom.Location = New System.Drawing.Point(0, 226)
        Me.flpBottom.Margin = New System.Windows.Forms.Padding(0)
        Me.flpBottom.Name = "flpBottom"
        Me.flpBottom.Size = New System.Drawing.Size(401, 23)
        Me.flpBottom.TabIndex = 2
        '
        'cmdInfo
        '
        Me.cmdInfo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdInfo.AutoSize = True
        Me.cmdInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.cmdInfo.Location = New System.Drawing.Point(252, 0)
        Me.cmdInfo.Margin = New System.Windows.Forms.Padding(0)
        Me.cmdInfo.Name = "cmdInfo"
        Me.cmdInfo.Size = New System.Drawing.Size(81, 23)
        Me.cmdInfo.TabIndex = 2
        Me.cmdInfo.Text = "Assembly info"
        Me.cmdInfo.UseVisualStyleBackColor = True
        '
        'sfdXml
        '
        Me.sfdXml.DefaultExt = "xml"
        Me.sfdXml.Filter = "xml files (*.xml)|*.xml"
        Me.sfdXml.Title = "Save nodes"
        '
        'frmTests
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(401, 249)
        Me.Controls.Add(Me.tvwMain)
        Me.Controls.Add(Me.flpBottom)
        Me.Name = "frmTests"
        Me.Text = "Tests for project Tools"
        Me.flpBottom.ResumeLayout(False)
        Me.flpBottom.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tvwMain As System.Windows.Forms.TreeView
    Friend WithEvents imlImages As System.Windows.Forms.ImageList
    Friend WithEvents cmdExport As System.Windows.Forms.Button
    Friend WithEvents flpBottom As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents sfdXml As System.Windows.Forms.SaveFileDialog
    Friend WithEvents cmdInfo As System.Windows.Forms.Button

End Class
