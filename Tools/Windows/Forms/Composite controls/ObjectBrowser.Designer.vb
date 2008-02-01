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
            Me.cohItem = New System.Windows.Forms.ColumnHeader
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.splLeft = New System.Windows.Forms.SplitContainer
            Me.rtbShort = New System.Windows.Forms.RichTextBox
            Me.splRight = New System.Windows.Forms.SplitContainer
            Me.splTopRight = New System.Windows.Forms.SplitContainer
            Me.prgProperties = New System.Windows.Forms.PropertyGrid
            Me.lblObjType = New System.Windows.Forms.Label
            Me.tosMenu = New System.Windows.Forms.ToolStrip
            Me.tdbShow = New System.Windows.Forms.ToolStripDropDownButton
            Me.tmiShowCTors = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowEvents = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowFields = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowGenericArguments = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowGlobalMembers = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowInheritedMembers = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowInitializers = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowInstanceMembers = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowInternalMembers = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowMethods = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowNestedTypes = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowPrivateMembers = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowProperties = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowProtectedMembers = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowSpecialMembers = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowReferences = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowBaseTypes = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowStaticMembers = New System.Windows.Forms.ToolStripMenuItem
            Me.tmiShowFlatNamespaces = New System.Windows.Forms.ToolStripMenuItem
            Me.splMain.Panel1.SuspendLayout()
            Me.splMain.Panel2.SuspendLayout()
            Me.splMain.SuspendLayout()
            Me.splLeft.Panel1.SuspendLayout()
            Me.splLeft.Panel2.SuspendLayout()
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
            Me.lvwMembers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.cohItem})
            Me.lvwMembers.Dock = System.Windows.Forms.DockStyle.Fill
            Me.lvwMembers.Location = New System.Drawing.Point(0, 0)
            Me.lvwMembers.MultiSelect = False
            Me.lvwMembers.Name = "lvwMembers"
            Me.lvwMembers.Size = New System.Drawing.Size(211, 343)
            Me.lvwMembers.SmallImageList = Me.imlImages
            Me.lvwMembers.TabIndex = 1
            Me.lvwMembers.UseCompatibleStateImageBehavior = False
            Me.lvwMembers.View = System.Windows.Forms.View.List
            '
            'cohItem
            '
            Me.cohItem.Text = "Item"
            Me.cohItem.Width = 208
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
            '
            'splLeft.Panel2
            '
            Me.splLeft.Panel2.Controls.Add(Me.rtbShort)
            Me.splLeft.Size = New System.Drawing.Size(314, 505)
            Me.splLeft.SplitterDistance = 443
            Me.splLeft.TabIndex = 1
            '
            'rtbShort
            '
            Me.rtbShort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.rtbShort.Dock = System.Windows.Forms.DockStyle.Fill
            Me.rtbShort.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
            Me.rtbShort.Location = New System.Drawing.Point(0, 0)
            Me.rtbShort.MaxLength = 0
            Me.rtbShort.Name = "rtbShort"
            Me.rtbShort.ReadOnly = True
            Me.rtbShort.Size = New System.Drawing.Size(314, 58)
            Me.rtbShort.TabIndex = 0
            Me.rtbShort.Text = ""
            Me.rtbShort.WordWrap = False
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
            Me.tosMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tdbShow})
            Me.tosMenu.Location = New System.Drawing.Point(0, 0)
            Me.tosMenu.Name = "tosMenu"
            Me.tosMenu.Size = New System.Drawing.Size(749, 25)
            Me.tosMenu.TabIndex = 2
            Me.tosMenu.Text = "Commands"
            '
            'tdbShow
            '
            Me.tdbShow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
            Me.tdbShow.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiShowCTors, Me.tmiShowEvents, Me.tmiShowFields, Me.tmiShowGenericArguments, Me.tmiShowGlobalMembers, Me.tmiShowInheritedMembers, Me.tmiShowInitializers, Me.tmiShowInstanceMembers, Me.tmiShowInternalMembers, Me.tmiShowMethods, Me.tmiShowNestedTypes, Me.tmiShowPrivateMembers, Me.tmiShowProperties, Me.tmiShowProtectedMembers, Me.tmiShowSpecialMembers, Me.tmiShowReferences, Me.tmiShowBaseTypes, Me.tmiShowStaticMembers, Me.tmiShowFlatNamespaces})
            Me.tdbShow.Image = CType(resources.GetObject("tdbShow.Image"), System.Drawing.Image)
            Me.tdbShow.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.tdbShow.Name = "tdbShow"
            Me.tdbShow.Size = New System.Drawing.Size(49, 22)
            Me.tdbShow.Text = "Show"
            '
            'tmiShowCTors
            '
            Me.tmiShowCTors.Checked = True
            Me.tmiShowCTors.CheckOnClick = True
            Me.tmiShowCTors.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowCTors.Name = "tmiShowCTors"
            Me.tmiShowCTors.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowCTors.Text = "Constructors"
            '
            'tmiShowEvents
            '
            Me.tmiShowEvents.Checked = True
            Me.tmiShowEvents.CheckOnClick = True
            Me.tmiShowEvents.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowEvents.Name = "tmiShowEvents"
            Me.tmiShowEvents.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowEvents.Text = "Events"
            '
            'tmiShowFields
            '
            Me.tmiShowFields.Checked = True
            Me.tmiShowFields.CheckOnClick = True
            Me.tmiShowFields.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowFields.Name = "tmiShowFields"
            Me.tmiShowFields.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowFields.Text = "Fields"
            '
            'tmiShowGenericArguments
            '
            Me.tmiShowGenericArguments.Checked = True
            Me.tmiShowGenericArguments.CheckOnClick = True
            Me.tmiShowGenericArguments.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowGenericArguments.Name = "tmiShowGenericArguments"
            Me.tmiShowGenericArguments.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowGenericArguments.Text = "Generic arguments"
            '
            'tmiShowGlobalMembers
            '
            Me.tmiShowGlobalMembers.Checked = True
            Me.tmiShowGlobalMembers.CheckOnClick = True
            Me.tmiShowGlobalMembers.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowGlobalMembers.Name = "tmiShowGlobalMembers"
            Me.tmiShowGlobalMembers.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowGlobalMembers.Text = "Global members"
            '
            'tmiShowInheritedMembers
            '
            Me.tmiShowInheritedMembers.CheckOnClick = True
            Me.tmiShowInheritedMembers.Name = "tmiShowInheritedMembers"
            Me.tmiShowInheritedMembers.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowInheritedMembers.Text = "Inherited members"
            '
            'tmiShowInitializers
            '
            Me.tmiShowInitializers.Checked = True
            Me.tmiShowInitializers.CheckOnClick = True
            Me.tmiShowInitializers.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowInitializers.Name = "tmiShowInitializers"
            Me.tmiShowInitializers.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowInitializers.Text = "Initializers"
            '
            'tmiShowInstanceMembers
            '
            Me.tmiShowInstanceMembers.Checked = True
            Me.tmiShowInstanceMembers.CheckOnClick = True
            Me.tmiShowInstanceMembers.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowInstanceMembers.Name = "tmiShowInstanceMembers"
            Me.tmiShowInstanceMembers.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowInstanceMembers.Text = "Instance members"
            '
            'tmiShowInternalMembers
            '
            Me.tmiShowInternalMembers.Checked = True
            Me.tmiShowInternalMembers.CheckOnClick = True
            Me.tmiShowInternalMembers.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowInternalMembers.Name = "tmiShowInternalMembers"
            Me.tmiShowInternalMembers.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowInternalMembers.Text = "Internal members"
            '
            'tmiShowMethods
            '
            Me.tmiShowMethods.Checked = True
            Me.tmiShowMethods.CheckOnClick = True
            Me.tmiShowMethods.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowMethods.Name = "tmiShowMethods"
            Me.tmiShowMethods.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowMethods.Text = "Methods"
            '
            'tmiShowNestedTypes
            '
            Me.tmiShowNestedTypes.Checked = True
            Me.tmiShowNestedTypes.CheckOnClick = True
            Me.tmiShowNestedTypes.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowNestedTypes.Name = "tmiShowNestedTypes"
            Me.tmiShowNestedTypes.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowNestedTypes.Text = "Nested types"
            '
            'tmiShowPrivateMembers
            '
            Me.tmiShowPrivateMembers.Checked = True
            Me.tmiShowPrivateMembers.CheckOnClick = True
            Me.tmiShowPrivateMembers.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowPrivateMembers.Name = "tmiShowPrivateMembers"
            Me.tmiShowPrivateMembers.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowPrivateMembers.Text = "Private members"
            '
            'tmiShowProperties
            '
            Me.tmiShowProperties.Checked = True
            Me.tmiShowProperties.CheckOnClick = True
            Me.tmiShowProperties.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowProperties.Name = "tmiShowProperties"
            Me.tmiShowProperties.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowProperties.Text = "Properties"
            '
            'tmiShowProtectedMembers
            '
            Me.tmiShowProtectedMembers.Checked = True
            Me.tmiShowProtectedMembers.CheckOnClick = True
            Me.tmiShowProtectedMembers.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowProtectedMembers.Name = "tmiShowProtectedMembers"
            Me.tmiShowProtectedMembers.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowProtectedMembers.Text = "Protected members"
            '
            'tmiShowSpecialMembers
            '
            Me.tmiShowSpecialMembers.Checked = True
            Me.tmiShowSpecialMembers.CheckOnClick = True
            Me.tmiShowSpecialMembers.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowSpecialMembers.Name = "tmiShowSpecialMembers"
            Me.tmiShowSpecialMembers.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowSpecialMembers.Text = "Special members"
            '
            'tmiShowReferences
            '
            Me.tmiShowReferences.Checked = True
            Me.tmiShowReferences.CheckOnClick = True
            Me.tmiShowReferences.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowReferences.Name = "tmiShowReferences"
            Me.tmiShowReferences.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowReferences.Text = "References"
            '
            'tmiShowBaseTypes
            '
            Me.tmiShowBaseTypes.Checked = True
            Me.tmiShowBaseTypes.CheckOnClick = True
            Me.tmiShowBaseTypes.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowBaseTypes.Name = "tmiShowBaseTypes"
            Me.tmiShowBaseTypes.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowBaseTypes.Text = "Base types"
            '
            'tmiShowStaticMembers
            '
            Me.tmiShowStaticMembers.Checked = True
            Me.tmiShowStaticMembers.CheckOnClick = True
            Me.tmiShowStaticMembers.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowStaticMembers.Name = "tmiShowStaticMembers"
            Me.tmiShowStaticMembers.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowStaticMembers.Text = "Static memebers"
            '
            'tmiShowFlatNamespaces
            '
            Me.tmiShowFlatNamespaces.Checked = True
            Me.tmiShowFlatNamespaces.CheckOnClick = True
            Me.tmiShowFlatNamespaces.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowFlatNamespaces.Name = "tmiShowFlatNamespaces"
            Me.tmiShowFlatNamespaces.Size = New System.Drawing.Size(178, 22)
            Me.tmiShowFlatNamespaces.Text = "Flat namespaces"
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
            Me.splLeft.Panel2.ResumeLayout(False)
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
        Protected WithEvents cohItem As System.Windows.Forms.ColumnHeader
        Protected WithEvents splTopRight As System.Windows.Forms.SplitContainer
        Protected WithEvents prgProperties As System.Windows.Forms.PropertyGrid
        Protected WithEvents splLeft As System.Windows.Forms.SplitContainer
        Protected WithEvents lblObjType As System.Windows.Forms.Label
        Protected WithEvents tosMenu As System.Windows.Forms.ToolStrip
        Protected WithEvents tdbShow As System.Windows.Forms.ToolStripDropDownButton
        Protected WithEvents tmiShowCTors As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiShowEvents As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiShowFields As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiShowGenericArguments As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiShowGlobalMembers As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiShowInheritedMembers As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiShowInitializers As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiShowInternalMembers As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiShowMethods As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiShowNestedTypes As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiShowPrivateMembers As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiShowProperties As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiShowProtectedMembers As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiShowSpecialMembers As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiShowStaticMembers As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiShowInstanceMembers As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents rtbShort As System.Windows.Forms.RichTextBox
        Protected WithEvents tmiShowReferences As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiShowBaseTypes As System.Windows.Forms.ToolStripMenuItem
        Protected WithEvents tmiShowFlatNamespaces As System.Windows.Forms.ToolStripMenuItem

    End Class
End Namespace