Namespace WindowsT.FormsT
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ObjectBrowser))
            Me.tvwObjects = New System.Windows.Forms.TreeView
            Me.imlImages = New System.Windows.Forms.ImageList(Me.components)
            Me.lvwMembers = New System.Windows.Forms.ListView
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.splLeft = New System.Windows.Forms.SplitContainer
            Me.splRight = New System.Windows.Forms.SplitContainer
            Me.splTopRight = New System.Windows.Forms.SplitContainer
            Me.prgProperties = New System.Windows.Forms.PropertyGrid
            Me.lblObjType = New System.Windows.Forms.Label
            Me.tosMenu = New System.Windows.Forms.ToolStrip
            Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton
            Me.ConstructorsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.EventsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.FieldsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.GenericArgumentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.GlobalMembersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.InheritedMembersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.InitializersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.InstanceMembersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.InternalMembersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.MethodsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.NestedTypesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.PrivateMembersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.PropertiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.ProtectedMembersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.SpecialMembersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.StaticMemebersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.splLeft.Panel1.SuspendLayout()
            Me.splLeft.SuspendLayout()
            Me.splRight.Panel1.SuspendLayout()
            Me.splRight.SuspendLayout()
            Me.splTopRight.Panel1.SuspendLayout()
            Me.splTopRight.Panel2.SuspendLayout()
            Me.splTopRight.SuspendLayout()
            Me.tosMenu.SuspendLayout()
            Me.SuspendLayout()
            '
            'tvwObjects
            '
            Me.tvwObjects.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tvwObjects.HideSelection = False
            Me.tvwObjects.ImageIndex = 0
            Me.tvwObjects.ImageList = Me.imlImages
            Me.tvwObjects.Location = New System.Drawing.Point(0, 0)
            Me.tvwObjects.Name = "tvwObjects"
            Me.tvwObjects.SelectedImageIndex = 0
            Me.tvwObjects.Size = New System.Drawing.Size(314, 443)
            Me.tvwObjects.TabIndex = 0
            '
            'imlImages
            '
            Me.imlImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
            Me.imlImages.ImageSize = New System.Drawing.Size(16, 16)
            Me.imlImages.TransparentColor = System.Drawing.Color.Transparent
            '
            'lvwMembers
            '
            Me.lvwMembers.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lvwMembers.Location = New System.Drawing.Point(0, 0)
            Me.lvwMembers.MultiSelect = False
            Me.lvwMembers.Name = "lvwMembers"
            Me.lvwMembers.Size = New System.Drawing.Size(211, 343)
            Me.lvwMembers.SmallImageList = Me.imlImages
            Me.lvwMembers.TabIndex = 1
            Me.lvwMembers.UseCompatibleStateImageBehavior = False
            Me.lvwMembers.View = System.Windows.Forms.View.Details
            '
            'splMain
            '
            Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splMain.Location = New System.Drawing.Point(0, 25)
            Me.splMain.Name = "splMain"
            '
            'splMain.Panel1
            '
            Me.splMain.Panel1.Controls.Add(Me.splLeft)
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.Controls.Add(Me.splRight)
            Me.splMain.Size = New System.Drawing.Size(749, 505)
            Me.splMain.SplitterDistance = 314
            Me.splMain.TabIndex = 2
            '
            'splLeft
            '
            Me.splLeft.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splLeft.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
            Me.splLeft.Location = New System.Drawing.Point(0, 0)
            Me.splLeft.Name = "splLeft"
            Me.splLeft.Orientation = System.Windows.Forms.Orientation.Horizontal
            '
            'splLeft.Panel1
            '
            Me.splLeft.Panel1.Controls.Add(Me.tvwObjects)
            Me.splLeft.Size = New System.Drawing.Size(314, 505)
            Me.splLeft.SplitterDistance = 443
            Me.splLeft.TabIndex = 1
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
            Me.splRight.Panel1.Controls.Add(Me.splTopRight)
            Me.splRight.Size = New System.Drawing.Size(431, 505)
            Me.splRight.SplitterDistance = 343
            Me.splRight.TabIndex = 0
            '
            'splTopRight
            '
            Me.splTopRight.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splTopRight.Location = New System.Drawing.Point(0, 0)
            Me.splTopRight.Name = "splTopRight"
            '
            'splTopRight.Panel1
            '
            Me.splTopRight.Panel1.Controls.Add(Me.lvwMembers)
            '
            'splTopRight.Panel2
            '
            Me.splTopRight.Panel2.Controls.Add(Me.prgProperties)
            Me.splTopRight.Panel2.Controls.Add(Me.lblObjType)
            Me.splTopRight.Size = New System.Drawing.Size(431, 343)
            Me.splTopRight.SplitterDistance = 211
            Me.splTopRight.TabIndex = 2
            '
            'prgProperties
            '
            Me.prgProperties.Dock = System.Windows.Forms.DockStyle.Fill
            Me.prgProperties.Location = New System.Drawing.Point(0, 13)
            Me.prgProperties.Name = "prgProperties"
            Me.prgProperties.Size = New System.Drawing.Size(216, 330)
            Me.prgProperties.TabIndex = 0
            '
            'lblObjType
            '
            Me.lblObjType.AutoEllipsis = True
            Me.lblObjType.Dock = System.Windows.Forms.DockStyle.Top
            Me.lblObjType.Location = New System.Drawing.Point(0, 0)
            Me.lblObjType.Name = "lblObjType"
            Me.lblObjType.Size = New System.Drawing.Size(216, 13)
            Me.lblObjType.TabIndex = 1
            Me.lblObjType.TextAlign = System.Drawing.ContentAlignment.TopCenter
            '
            'tosMenu
            '
            Me.tosMenu.GripMargin = New System.Windows.Forms.Padding(0)
            Me.tosMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.tosMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton1})
            Me.tosMenu.Location = New System.Drawing.Point(0, 0)
            Me.tosMenu.Name = "tosMenu"
            Me.tosMenu.Size = New System.Drawing.Size(749, 25)
            Me.tosMenu.TabIndex = 2
            Me.tosMenu.Text = "Commands"
            '
            'ToolStripDropDownButton1
            '
            Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
            Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConstructorsToolStripMenuItem, Me.EventsToolStripMenuItem, Me.FieldsToolStripMenuItem, Me.GenericArgumentsToolStripMenuItem, Me.GlobalMembersToolStripMenuItem, Me.InheritedMembersToolStripMenuItem, Me.InitializersToolStripMenuItem, Me.InstanceMembersToolStripMenuItem, Me.InternalMembersToolStripMenuItem, Me.MethodsToolStripMenuItem, Me.NestedTypesToolStripMenuItem, Me.PrivateMembersToolStripMenuItem, Me.PropertiesToolStripMenuItem, Me.ProtectedMembersToolStripMenuItem, Me.SpecialMembersToolStripMenuItem, Me.StaticMemebersToolStripMenuItem})
            Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
            Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
            Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(46, 22)
            Me.ToolStripDropDownButton1.Text = "Show"
            '
            'ConstructorsToolStripMenuItem
            '
            Me.ConstructorsToolStripMenuItem.Checked = True
            Me.ConstructorsToolStripMenuItem.CheckOnClick = True
            Me.ConstructorsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
            Me.ConstructorsToolStripMenuItem.Name = "ConstructorsToolStripMenuItem"
            Me.ConstructorsToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
            Me.ConstructorsToolStripMenuItem.Text = "Constructors"
            '
            'EventsToolStripMenuItem
            '
            Me.EventsToolStripMenuItem.Checked = True
            Me.EventsToolStripMenuItem.CheckOnClick = True
            Me.EventsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
            Me.EventsToolStripMenuItem.Name = "EventsToolStripMenuItem"
            Me.EventsToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
            Me.EventsToolStripMenuItem.Text = "Events"
            '
            'FieldsToolStripMenuItem
            '
            Me.FieldsToolStripMenuItem.Checked = True
            Me.FieldsToolStripMenuItem.CheckOnClick = True
            Me.FieldsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
            Me.FieldsToolStripMenuItem.Name = "FieldsToolStripMenuItem"
            Me.FieldsToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
            Me.FieldsToolStripMenuItem.Text = "Fields"
            '
            'GenericArgumentsToolStripMenuItem
            '
            Me.GenericArgumentsToolStripMenuItem.Checked = True
            Me.GenericArgumentsToolStripMenuItem.CheckOnClick = True
            Me.GenericArgumentsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
            Me.GenericArgumentsToolStripMenuItem.Name = "GenericArgumentsToolStripMenuItem"
            Me.GenericArgumentsToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
            Me.GenericArgumentsToolStripMenuItem.Text = "Generic arguments"
            '
            'GlobalMembersToolStripMenuItem
            '
            Me.GlobalMembersToolStripMenuItem.Checked = True
            Me.GlobalMembersToolStripMenuItem.CheckOnClick = True
            Me.GlobalMembersToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
            Me.GlobalMembersToolStripMenuItem.Name = "GlobalMembersToolStripMenuItem"
            Me.GlobalMembersToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
            Me.GlobalMembersToolStripMenuItem.Text = "Global members"
            '
            'InheritedMembersToolStripMenuItem
            '
            Me.InheritedMembersToolStripMenuItem.Checked = True
            Me.InheritedMembersToolStripMenuItem.CheckOnClick = True
            Me.InheritedMembersToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
            Me.InheritedMembersToolStripMenuItem.Name = "InheritedMembersToolStripMenuItem"
            Me.InheritedMembersToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
            Me.InheritedMembersToolStripMenuItem.Text = "Inherited members"
            '
            'InitializersToolStripMenuItem
            '
            Me.InitializersToolStripMenuItem.Checked = True
            Me.InitializersToolStripMenuItem.CheckOnClick = True
            Me.InitializersToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
            Me.InitializersToolStripMenuItem.Name = "InitializersToolStripMenuItem"
            Me.InitializersToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
            Me.InitializersToolStripMenuItem.Text = "Initializers"
            '
            'InstanceMembersToolStripMenuItem
            '
            Me.InstanceMembersToolStripMenuItem.Checked = True
            Me.InstanceMembersToolStripMenuItem.CheckOnClick = True
            Me.InstanceMembersToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
            Me.InstanceMembersToolStripMenuItem.Name = "InstanceMembersToolStripMenuItem"
            Me.InstanceMembersToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
            Me.InstanceMembersToolStripMenuItem.Text = "Instance members"
            '
            'InternalMembersToolStripMenuItem
            '
            Me.InternalMembersToolStripMenuItem.Checked = True
            Me.InternalMembersToolStripMenuItem.CheckOnClick = True
            Me.InternalMembersToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
            Me.InternalMembersToolStripMenuItem.Name = "InternalMembersToolStripMenuItem"
            Me.InternalMembersToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
            Me.InternalMembersToolStripMenuItem.Text = "Internal members"
            '
            'MethodsToolStripMenuItem
            '
            Me.MethodsToolStripMenuItem.Checked = True
            Me.MethodsToolStripMenuItem.CheckOnClick = True
            Me.MethodsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
            Me.MethodsToolStripMenuItem.Name = "MethodsToolStripMenuItem"
            Me.MethodsToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
            Me.MethodsToolStripMenuItem.Text = "Methods"
            '
            'NestedTypesToolStripMenuItem
            '
            Me.NestedTypesToolStripMenuItem.Checked = True
            Me.NestedTypesToolStripMenuItem.CheckOnClick = True
            Me.NestedTypesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
            Me.NestedTypesToolStripMenuItem.Name = "NestedTypesToolStripMenuItem"
            Me.NestedTypesToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
            Me.NestedTypesToolStripMenuItem.Text = "Nested types"
            '
            'PrivateMembersToolStripMenuItem
            '
            Me.PrivateMembersToolStripMenuItem.Checked = True
            Me.PrivateMembersToolStripMenuItem.CheckOnClick = True
            Me.PrivateMembersToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
            Me.PrivateMembersToolStripMenuItem.Name = "PrivateMembersToolStripMenuItem"
            Me.PrivateMembersToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
            Me.PrivateMembersToolStripMenuItem.Text = "Private members"
            '
            'PropertiesToolStripMenuItem
            '
            Me.PropertiesToolStripMenuItem.Checked = True
            Me.PropertiesToolStripMenuItem.CheckOnClick = True
            Me.PropertiesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
            Me.PropertiesToolStripMenuItem.Name = "PropertiesToolStripMenuItem"
            Me.PropertiesToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
            Me.PropertiesToolStripMenuItem.Text = "Properties"
            '
            'ProtectedMembersToolStripMenuItem
            '
            Me.ProtectedMembersToolStripMenuItem.Checked = True
            Me.ProtectedMembersToolStripMenuItem.CheckOnClick = True
            Me.ProtectedMembersToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
            Me.ProtectedMembersToolStripMenuItem.Name = "ProtectedMembersToolStripMenuItem"
            Me.ProtectedMembersToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
            Me.ProtectedMembersToolStripMenuItem.Text = "Protected members"
            '
            'SpecialMembersToolStripMenuItem
            '
            Me.SpecialMembersToolStripMenuItem.Checked = True
            Me.SpecialMembersToolStripMenuItem.CheckOnClick = True
            Me.SpecialMembersToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
            Me.SpecialMembersToolStripMenuItem.Name = "SpecialMembersToolStripMenuItem"
            Me.SpecialMembersToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
            Me.SpecialMembersToolStripMenuItem.Text = "Special members"
            '
            'StaticMemebersToolStripMenuItem
            '
            Me.StaticMemebersToolStripMenuItem.Checked = True
            Me.StaticMemebersToolStripMenuItem.CheckOnClick = True
            Me.StaticMemebersToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
            Me.StaticMemebersToolStripMenuItem.Name = "StaticMemebersToolStripMenuItem"
            Me.StaticMemebersToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
            Me.StaticMemebersToolStripMenuItem.Text = "Static memebers"
            '
            'ObjectBrowser
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.splMain)
            Me.Controls.Add(Me.tosMenu)
            Me.Name = "ObjectBrowser"
            Me.Size = New System.Drawing.Size(749, 530)
            Me.splMain.Panel1.ResumeLayout(False)
            Me.splMain.Panel2.ResumeLayout(False)
            Me.splMain.ResumeLayout(False)
            Me.splLeft.Panel1.ResumeLayout(False)
            Me.splLeft.ResumeLayout(False)
            Me.splRight.Panel1.ResumeLayout(False)
            Me.splRight.ResumeLayout(False)
            Me.splTopRight.Panel1.ResumeLayout(False)
            Me.splTopRight.Panel2.ResumeLayout(False)
            Me.splTopRight.ResumeLayout(False)
            Me.tosMenu.ResumeLayout(False)
            Me.tosMenu.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Protected WithEvents tvwObjects As System.Windows.Forms.TreeView
        Protected WithEvents lvwMembers As System.Windows.Forms.ListView
        Protected WithEvents splMain As System.Windows.Forms.SplitContainer
        Protected WithEvents splRight As System.Windows.Forms.SplitContainer
        Protected WithEvents imlImages As System.Windows.Forms.ImageList
        Friend WithEvents splTopRight As System.Windows.Forms.SplitContainer
        Friend WithEvents prgProperties As System.Windows.Forms.PropertyGrid
        Friend WithEvents splLeft As System.Windows.Forms.SplitContainer
        Friend WithEvents lblObjType As System.Windows.Forms.Label
        Friend WithEvents tosMenu As System.Windows.Forms.ToolStrip
        Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
        Friend WithEvents ConstructorsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents EventsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents FieldsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents GenericArgumentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents GlobalMembersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents InheritedMembersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents InitializersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents InstanceMembersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents InternalMembersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents MethodsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents NestedTypesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents PrivateMembersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents PropertiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents ProtectedMembersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents SpecialMembersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents StaticMemebersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    End Class
End Namespace