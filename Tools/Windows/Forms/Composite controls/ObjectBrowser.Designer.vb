Namespace WindowsT.FormsT
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ObjectBrowser
        Inherits UserControlExtended

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
            Me.splMain = New System.Windows.Forms.SplitContainer
            Me.splLeft = New System.Windows.Forms.SplitContainer
            Me.tvwObjects = New System.Windows.Forms.TreeView
            Me.imlImages = New System.Windows.Forms.ImageList(Me.components)
            Me.rtbShort = New System.Windows.Forms.RichTextBox
            Me.splRight = New System.Windows.Forms.SplitContainer
            Me.splTopRight = New System.Windows.Forms.SplitContainer
            Me.lvwMembers = New System.Windows.Forms.ListView
            Me.cohItem = New System.Windows.Forms.ColumnHeader
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
            Me.tsbBack = New System.Windows.Forms.ToolStripButton
            Me.tsbForward = New System.Windows.Forms.ToolStripButton
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
            'splMain
            '
            Me.splMain.AccessibleDescription = Nothing
            Me.splMain.AccessibleName = Nothing
            resources.ApplyResources(Me.splMain, "splMain")
            Me.splMain.BackgroundImage = Nothing
            Me.splMain.Font = Nothing
            Me.splMain.Name = "splMain"
            '
            'splMain.Panel1
            '
            Me.splMain.Panel1.AccessibleDescription = Nothing
            Me.splMain.Panel1.AccessibleName = Nothing
            resources.ApplyResources(Me.splMain.Panel1, "splMain.Panel1")
            Me.splMain.Panel1.BackgroundImage = Nothing
            Me.splMain.Panel1.Controls.Add(Me.splLeft)
            Me.splMain.Panel1.Font = Nothing
            '
            'splMain.Panel2
            '
            Me.splMain.Panel2.AccessibleDescription = Nothing
            Me.splMain.Panel2.AccessibleName = Nothing
            resources.ApplyResources(Me.splMain.Panel2, "splMain.Panel2")
            Me.splMain.Panel2.BackgroundImage = Nothing
            Me.splMain.Panel2.Controls.Add(Me.splRight)
            Me.splMain.Panel2.Font = Nothing
            '
            'splLeft
            '
            Me.splLeft.AccessibleDescription = Nothing
            Me.splLeft.AccessibleName = Nothing
            resources.ApplyResources(Me.splLeft, "splLeft")
            Me.splLeft.BackgroundImage = Nothing
            Me.splLeft.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
            Me.splLeft.Font = Nothing
            Me.splLeft.Name = "splLeft"
            '
            'splLeft.Panel1
            '
            Me.splLeft.Panel1.AccessibleDescription = Nothing
            Me.splLeft.Panel1.AccessibleName = Nothing
            resources.ApplyResources(Me.splLeft.Panel1, "splLeft.Panel1")
            Me.splLeft.Panel1.BackgroundImage = Nothing
            Me.splLeft.Panel1.Controls.Add(Me.tvwObjects)
            Me.splLeft.Panel1.Font = Nothing
            '
            'splLeft.Panel2
            '
            Me.splLeft.Panel2.AccessibleDescription = Nothing
            Me.splLeft.Panel2.AccessibleName = Nothing
            resources.ApplyResources(Me.splLeft.Panel2, "splLeft.Panel2")
            Me.splLeft.Panel2.BackgroundImage = Nothing
            Me.splLeft.Panel2.Controls.Add(Me.rtbShort)
            Me.splLeft.Panel2.Font = Nothing
            '
            'tvwObjects
            '
            Me.tvwObjects.AccessibleDescription = Nothing
            Me.tvwObjects.AccessibleName = Nothing
            resources.ApplyResources(Me.tvwObjects, "tvwObjects")
            Me.tvwObjects.BackgroundImage = Nothing
            Me.tvwObjects.Font = Nothing
            Me.tvwObjects.HideSelection = False
            Me.tvwObjects.ImageList = Me.imlImages
            Me.tvwObjects.Name = "tvwObjects"
            '
            'imlImages
            '
            Me.imlImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
            resources.ApplyResources(Me.imlImages, "imlImages")
            Me.imlImages.TransparentColor = System.Drawing.Color.Transparent
            '
            'rtbShort
            '
            Me.rtbShort.AccessibleDescription = Nothing
            Me.rtbShort.AccessibleName = Nothing
            resources.ApplyResources(Me.rtbShort, "rtbShort")
            Me.rtbShort.BackgroundImage = Nothing
            Me.rtbShort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.rtbShort.Name = "rtbShort"
            Me.rtbShort.ReadOnly = True
            '
            'splRight
            '
            Me.splRight.AccessibleDescription = Nothing
            Me.splRight.AccessibleName = Nothing
            resources.ApplyResources(Me.splRight, "splRight")
            Me.splRight.BackgroundImage = Nothing
            Me.splRight.Font = Nothing
            Me.splRight.Name = "splRight"
            '
            'splRight.Panel1
            '
            Me.splRight.Panel1.AccessibleDescription = Nothing
            Me.splRight.Panel1.AccessibleName = Nothing
            resources.ApplyResources(Me.splRight.Panel1, "splRight.Panel1")
            Me.splRight.Panel1.BackgroundImage = Nothing
            Me.splRight.Panel1.Controls.Add(Me.splTopRight)
            Me.splRight.Panel1.Font = Nothing
            '
            'splRight.Panel2
            '
            Me.splRight.Panel2.AccessibleDescription = Nothing
            Me.splRight.Panel2.AccessibleName = Nothing
            resources.ApplyResources(Me.splRight.Panel2, "splRight.Panel2")
            Me.splRight.Panel2.BackgroundImage = Nothing
            Me.splRight.Panel2.Font = Nothing
            '
            'splTopRight
            '
            Me.splTopRight.AccessibleDescription = Nothing
            Me.splTopRight.AccessibleName = Nothing
            resources.ApplyResources(Me.splTopRight, "splTopRight")
            Me.splTopRight.BackgroundImage = Nothing
            Me.splTopRight.Font = Nothing
            Me.splTopRight.Name = "splTopRight"
            '
            'splTopRight.Panel1
            '
            Me.splTopRight.Panel1.AccessibleDescription = Nothing
            Me.splTopRight.Panel1.AccessibleName = Nothing
            resources.ApplyResources(Me.splTopRight.Panel1, "splTopRight.Panel1")
            Me.splTopRight.Panel1.BackgroundImage = Nothing
            Me.splTopRight.Panel1.Controls.Add(Me.lvwMembers)
            Me.splTopRight.Panel1.Font = Nothing
            '
            'splTopRight.Panel2
            '
            Me.splTopRight.Panel2.AccessibleDescription = Nothing
            Me.splTopRight.Panel2.AccessibleName = Nothing
            resources.ApplyResources(Me.splTopRight.Panel2, "splTopRight.Panel2")
            Me.splTopRight.Panel2.BackgroundImage = Nothing
            Me.splTopRight.Panel2.Controls.Add(Me.prgProperties)
            Me.splTopRight.Panel2.Controls.Add(Me.lblObjType)
            Me.splTopRight.Panel2.Font = Nothing
            '
            'lvwMembers
            '
            Me.lvwMembers.AccessibleDescription = Nothing
            Me.lvwMembers.AccessibleName = Nothing
            resources.ApplyResources(Me.lvwMembers, "lvwMembers")
            Me.lvwMembers.BackgroundImage = Nothing
            Me.lvwMembers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.cohItem})
            Me.lvwMembers.Font = Nothing
            Me.lvwMembers.MultiSelect = False
            Me.lvwMembers.Name = "lvwMembers"
            Me.lvwMembers.SmallImageList = Me.imlImages
            Me.lvwMembers.UseCompatibleStateImageBehavior = False
            Me.lvwMembers.View = System.Windows.Forms.View.List
            '
            'cohItem
            '
            resources.ApplyResources(Me.cohItem, "cohItem")
            '
            'prgProperties
            '
            Me.prgProperties.AccessibleDescription = Nothing
            Me.prgProperties.AccessibleName = Nothing
            resources.ApplyResources(Me.prgProperties, "prgProperties")
            Me.prgProperties.BackgroundImage = Nothing
            Me.prgProperties.Font = Nothing
            Me.prgProperties.Name = "prgProperties"
            '
            'lblObjType
            '
            Me.lblObjType.AccessibleDescription = Nothing
            Me.lblObjType.AccessibleName = Nothing
            resources.ApplyResources(Me.lblObjType, "lblObjType")
            Me.lblObjType.AutoEllipsis = True
            Me.lblObjType.Font = Nothing
            Me.lblObjType.Name = "lblObjType"
            '
            'tosMenu
            '
            Me.tosMenu.AccessibleDescription = Nothing
            Me.tosMenu.AccessibleName = Nothing
            resources.ApplyResources(Me.tosMenu, "tosMenu")
            Me.tosMenu.BackgroundImage = Nothing
            Me.tosMenu.Font = Nothing
            Me.tosMenu.GripMargin = New System.Windows.Forms.Padding(0)
            Me.tosMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.tosMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tdbShow, Me.tsbBack, Me.tsbForward})
            Me.tosMenu.Name = "tosMenu"
            '
            'tdbShow
            '
            Me.tdbShow.AccessibleDescription = Nothing
            Me.tdbShow.AccessibleName = Nothing
            resources.ApplyResources(Me.tdbShow, "tdbShow")
            Me.tdbShow.BackgroundImage = Nothing
            Me.tdbShow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
            Me.tdbShow.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmiShowCTors, Me.tmiShowEvents, Me.tmiShowFields, Me.tmiShowGenericArguments, Me.tmiShowGlobalMembers, Me.tmiShowInheritedMembers, Me.tmiShowInitializers, Me.tmiShowInstanceMembers, Me.tmiShowInternalMembers, Me.tmiShowMethods, Me.tmiShowNestedTypes, Me.tmiShowPrivateMembers, Me.tmiShowProperties, Me.tmiShowProtectedMembers, Me.tmiShowSpecialMembers, Me.tmiShowReferences, Me.tmiShowBaseTypes, Me.tmiShowStaticMembers, Me.tmiShowFlatNamespaces})
            Me.tdbShow.Name = "tdbShow"
            '
            'tmiShowCTors
            '
            Me.tmiShowCTors.AccessibleDescription = Nothing
            Me.tmiShowCTors.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowCTors, "tmiShowCTors")
            Me.tmiShowCTors.BackgroundImage = Nothing
            Me.tmiShowCTors.Checked = True
            Me.tmiShowCTors.CheckOnClick = True
            Me.tmiShowCTors.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowCTors.Name = "tmiShowCTors"
            Me.tmiShowCTors.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowEvents
            '
            Me.tmiShowEvents.AccessibleDescription = Nothing
            Me.tmiShowEvents.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowEvents, "tmiShowEvents")
            Me.tmiShowEvents.BackgroundImage = Nothing
            Me.tmiShowEvents.Checked = True
            Me.tmiShowEvents.CheckOnClick = True
            Me.tmiShowEvents.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowEvents.Name = "tmiShowEvents"
            Me.tmiShowEvents.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowFields
            '
            Me.tmiShowFields.AccessibleDescription = Nothing
            Me.tmiShowFields.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowFields, "tmiShowFields")
            Me.tmiShowFields.BackgroundImage = Nothing
            Me.tmiShowFields.Checked = True
            Me.tmiShowFields.CheckOnClick = True
            Me.tmiShowFields.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowFields.Name = "tmiShowFields"
            Me.tmiShowFields.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowGenericArguments
            '
            Me.tmiShowGenericArguments.AccessibleDescription = Nothing
            Me.tmiShowGenericArguments.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowGenericArguments, "tmiShowGenericArguments")
            Me.tmiShowGenericArguments.BackgroundImage = Nothing
            Me.tmiShowGenericArguments.Checked = True
            Me.tmiShowGenericArguments.CheckOnClick = True
            Me.tmiShowGenericArguments.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowGenericArguments.Name = "tmiShowGenericArguments"
            Me.tmiShowGenericArguments.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowGlobalMembers
            '
            Me.tmiShowGlobalMembers.AccessibleDescription = Nothing
            Me.tmiShowGlobalMembers.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowGlobalMembers, "tmiShowGlobalMembers")
            Me.tmiShowGlobalMembers.BackgroundImage = Nothing
            Me.tmiShowGlobalMembers.Checked = True
            Me.tmiShowGlobalMembers.CheckOnClick = True
            Me.tmiShowGlobalMembers.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowGlobalMembers.Name = "tmiShowGlobalMembers"
            Me.tmiShowGlobalMembers.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowInheritedMembers
            '
            Me.tmiShowInheritedMembers.AccessibleDescription = Nothing
            Me.tmiShowInheritedMembers.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowInheritedMembers, "tmiShowInheritedMembers")
            Me.tmiShowInheritedMembers.BackgroundImage = Nothing
            Me.tmiShowInheritedMembers.CheckOnClick = True
            Me.tmiShowInheritedMembers.Name = "tmiShowInheritedMembers"
            Me.tmiShowInheritedMembers.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowInitializers
            '
            Me.tmiShowInitializers.AccessibleDescription = Nothing
            Me.tmiShowInitializers.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowInitializers, "tmiShowInitializers")
            Me.tmiShowInitializers.BackgroundImage = Nothing
            Me.tmiShowInitializers.Checked = True
            Me.tmiShowInitializers.CheckOnClick = True
            Me.tmiShowInitializers.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowInitializers.Name = "tmiShowInitializers"
            Me.tmiShowInitializers.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowInstanceMembers
            '
            Me.tmiShowInstanceMembers.AccessibleDescription = Nothing
            Me.tmiShowInstanceMembers.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowInstanceMembers, "tmiShowInstanceMembers")
            Me.tmiShowInstanceMembers.BackgroundImage = Nothing
            Me.tmiShowInstanceMembers.Checked = True
            Me.tmiShowInstanceMembers.CheckOnClick = True
            Me.tmiShowInstanceMembers.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowInstanceMembers.Name = "tmiShowInstanceMembers"
            Me.tmiShowInstanceMembers.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowInternalMembers
            '
            Me.tmiShowInternalMembers.AccessibleDescription = Nothing
            Me.tmiShowInternalMembers.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowInternalMembers, "tmiShowInternalMembers")
            Me.tmiShowInternalMembers.BackgroundImage = Nothing
            Me.tmiShowInternalMembers.Checked = True
            Me.tmiShowInternalMembers.CheckOnClick = True
            Me.tmiShowInternalMembers.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowInternalMembers.Name = "tmiShowInternalMembers"
            Me.tmiShowInternalMembers.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowMethods
            '
            Me.tmiShowMethods.AccessibleDescription = Nothing
            Me.tmiShowMethods.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowMethods, "tmiShowMethods")
            Me.tmiShowMethods.BackgroundImage = Nothing
            Me.tmiShowMethods.Checked = True
            Me.tmiShowMethods.CheckOnClick = True
            Me.tmiShowMethods.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowMethods.Name = "tmiShowMethods"
            Me.tmiShowMethods.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowNestedTypes
            '
            Me.tmiShowNestedTypes.AccessibleDescription = Nothing
            Me.tmiShowNestedTypes.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowNestedTypes, "tmiShowNestedTypes")
            Me.tmiShowNestedTypes.BackgroundImage = Nothing
            Me.tmiShowNestedTypes.Checked = True
            Me.tmiShowNestedTypes.CheckOnClick = True
            Me.tmiShowNestedTypes.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowNestedTypes.Name = "tmiShowNestedTypes"
            Me.tmiShowNestedTypes.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowPrivateMembers
            '
            Me.tmiShowPrivateMembers.AccessibleDescription = Nothing
            Me.tmiShowPrivateMembers.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowPrivateMembers, "tmiShowPrivateMembers")
            Me.tmiShowPrivateMembers.BackgroundImage = Nothing
            Me.tmiShowPrivateMembers.Checked = True
            Me.tmiShowPrivateMembers.CheckOnClick = True
            Me.tmiShowPrivateMembers.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowPrivateMembers.Name = "tmiShowPrivateMembers"
            Me.tmiShowPrivateMembers.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowProperties
            '
            Me.tmiShowProperties.AccessibleDescription = Nothing
            Me.tmiShowProperties.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowProperties, "tmiShowProperties")
            Me.tmiShowProperties.BackgroundImage = Nothing
            Me.tmiShowProperties.Checked = True
            Me.tmiShowProperties.CheckOnClick = True
            Me.tmiShowProperties.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowProperties.Name = "tmiShowProperties"
            Me.tmiShowProperties.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowProtectedMembers
            '
            Me.tmiShowProtectedMembers.AccessibleDescription = Nothing
            Me.tmiShowProtectedMembers.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowProtectedMembers, "tmiShowProtectedMembers")
            Me.tmiShowProtectedMembers.BackgroundImage = Nothing
            Me.tmiShowProtectedMembers.Checked = True
            Me.tmiShowProtectedMembers.CheckOnClick = True
            Me.tmiShowProtectedMembers.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowProtectedMembers.Name = "tmiShowProtectedMembers"
            Me.tmiShowProtectedMembers.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowSpecialMembers
            '
            Me.tmiShowSpecialMembers.AccessibleDescription = Nothing
            Me.tmiShowSpecialMembers.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowSpecialMembers, "tmiShowSpecialMembers")
            Me.tmiShowSpecialMembers.BackgroundImage = Nothing
            Me.tmiShowSpecialMembers.Checked = True
            Me.tmiShowSpecialMembers.CheckOnClick = True
            Me.tmiShowSpecialMembers.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowSpecialMembers.Name = "tmiShowSpecialMembers"
            Me.tmiShowSpecialMembers.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowReferences
            '
            Me.tmiShowReferences.AccessibleDescription = Nothing
            Me.tmiShowReferences.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowReferences, "tmiShowReferences")
            Me.tmiShowReferences.BackgroundImage = Nothing
            Me.tmiShowReferences.Checked = True
            Me.tmiShowReferences.CheckOnClick = True
            Me.tmiShowReferences.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowReferences.Name = "tmiShowReferences"
            Me.tmiShowReferences.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowBaseTypes
            '
            Me.tmiShowBaseTypes.AccessibleDescription = Nothing
            Me.tmiShowBaseTypes.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowBaseTypes, "tmiShowBaseTypes")
            Me.tmiShowBaseTypes.BackgroundImage = Nothing
            Me.tmiShowBaseTypes.Checked = True
            Me.tmiShowBaseTypes.CheckOnClick = True
            Me.tmiShowBaseTypes.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowBaseTypes.Name = "tmiShowBaseTypes"
            Me.tmiShowBaseTypes.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowStaticMembers
            '
            Me.tmiShowStaticMembers.AccessibleDescription = Nothing
            Me.tmiShowStaticMembers.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowStaticMembers, "tmiShowStaticMembers")
            Me.tmiShowStaticMembers.BackgroundImage = Nothing
            Me.tmiShowStaticMembers.Checked = True
            Me.tmiShowStaticMembers.CheckOnClick = True
            Me.tmiShowStaticMembers.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowStaticMembers.Name = "tmiShowStaticMembers"
            Me.tmiShowStaticMembers.ShortcutKeyDisplayString = Nothing
            '
            'tmiShowFlatNamespaces
            '
            Me.tmiShowFlatNamespaces.AccessibleDescription = Nothing
            Me.tmiShowFlatNamespaces.AccessibleName = Nothing
            resources.ApplyResources(Me.tmiShowFlatNamespaces, "tmiShowFlatNamespaces")
            Me.tmiShowFlatNamespaces.BackgroundImage = Nothing
            Me.tmiShowFlatNamespaces.Checked = True
            Me.tmiShowFlatNamespaces.CheckOnClick = True
            Me.tmiShowFlatNamespaces.CheckState = System.Windows.Forms.CheckState.Checked
            Me.tmiShowFlatNamespaces.Name = "tmiShowFlatNamespaces"
            Me.tmiShowFlatNamespaces.ShortcutKeyDisplayString = Nothing
            '
            'tsbBack
            '
            Me.tsbBack.AccessibleDescription = Nothing
            Me.tsbBack.AccessibleName = Nothing
            resources.ApplyResources(Me.tsbBack, "tsbBack")
            Me.tsbBack.BackgroundImage = Nothing
            Me.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.tsbBack.Image = Global.Tools.My.Resources.Resources.NavBack
            Me.tsbBack.Name = "tsbBack"
            '
            'tsbForward
            '
            Me.tsbForward.AccessibleDescription = Nothing
            Me.tsbForward.AccessibleName = Nothing
            resources.ApplyResources(Me.tsbForward, "tsbForward")
            Me.tsbForward.BackgroundImage = Nothing
            Me.tsbForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.tsbForward.Image = Global.Tools.My.Resources.Resources.NavForward
            Me.tsbForward.Name = "tsbForward"
            '
            'ObjectBrowser
            '
            Me.AccessibleDescription = Nothing
            Me.AccessibleName = Nothing
            resources.ApplyResources(Me, "$this")
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.BackgroundImage = Nothing
            Me.Controls.Add(Me.splMain)
            Me.Controls.Add(Me.tosMenu)
            Me.Font = Nothing
            Me.KeyPreview = True
            Me.Name = "ObjectBrowser"
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
        Friend WithEvents tsbBack As System.Windows.Forms.ToolStripButton
        Friend WithEvents tsbForward As System.Windows.Forms.ToolStripButton

    End Class
End Namespace