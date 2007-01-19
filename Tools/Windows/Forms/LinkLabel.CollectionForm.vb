'Imports System.Windows.Forms, Tools.Collections.Generic, Tools.Windows.Forms.Utilities
'Imports System.Drawing.Design, System.ComponentModel.Design, Tools.ComponentModel
'#If Config <= RC Then 'Do not change unless corresponding statement in LinkLabel.vb is also cnaged
'Namespace Windows.Forms : Partial Class LinkLabel : Partial Class LinkLabelItemsEditor
'            ''' <summary>Provides a modal dialog box for editing the contents of the <see cref="ListWithEvents(Of LinkLabelItem)"/>  using a <see cref="System.Drawing.Design.UITypeEditor"/>.</summary>
'            <Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
'            <Localizable(True)> _
'            Protected Shadows Class CollectionForm
'                Inherits CollectionEditor.CollectionForm

'#Region "WindowsForms Designer generated code"
'                ''' <summary>Form overrides dispose to clean up the component list.</summary>
'                <System.Diagnostics.DebuggerNonUserCode()> _
'                Protected Overrides Sub Dispose(ByVal disposing As Boolean)
'                    Try
'                        If disposing AndAlso components IsNot Nothing Then
'                            components.Dispose()
'                        End If
'                    Finally
'                        MyBase.Dispose(disposing)
'                    End Try
'                End Sub
'                ''' <summary>Shows information about select items</summary>
'                Private WithEvents lblItemInfo As System.Windows.Forms.Label
'                ''' <summary>Required by the Windows Form Designer</summary>
'                Private components As System.ComponentModel.IContainer
'                ''' <summary>Resources for this form</summary>
'                <EditorBrowsable(EditorBrowsableState.Advanced)> _
'                Private resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LinkLabel))
'                ''' <summary>Initializes components</summary>
'                ''' <remarks>
'                ''' NOTE: The following procedure is required by the Windows Form Designer
'                ''' It can be modified using the Windows Form Designer.  
'                ''' Do not modify it using the code editor.
'                ''' </remarks>
'                <System.Diagnostics.DebuggerStepThrough()> _
'                <EditorBrowsable(EditorBrowsableState.Advanced)> _
'                Private Sub InitializeComponent()
'                    Me.components = New System.ComponentModel.Container
'                    Me.splMain = New System.Windows.Forms.SplitContainer
'                    Me.tlpItems = New System.Windows.Forms.TableLayoutPanel
'                    Me.lstItems = New System.Windows.Forms.ListBox
'                    Me.cmdUp = New System.Windows.Forms.Button
'                    Me.cmdDown = New System.Windows.Forms.Button
'                    Me.tosAdd = New System.Windows.Forms.ToolStrip
'                    Me.tsbAdd = New System.Windows.Forms.ToolStripSplitButton
'                    Me.tosRemove = New System.Windows.Forms.ToolStrip
'                    Me.tsbRemove = New System.Windows.Forms.ToolStripButton
'                    Me.cmdCancel = New System.Windows.Forms.Button
'                    Me.cmdOK = New System.Windows.Forms.Button
'                    Me.pgrProperty = New System.Windows.Forms.PropertyGrid
'                    Me.lblItemInfo = New System.Windows.Forms.Label
'                    Me.totTT = New System.Windows.Forms.ToolTip(Me.components)
'                    Me.splMain.Panel1.SuspendLayout()
'                    Me.splMain.Panel2.SuspendLayout()
'                    Me.splMain.SuspendLayout()
'                    Me.tlpItems.SuspendLayout()
'                    Me.tosAdd.SuspendLayout()
'                    Me.tosRemove.SuspendLayout()
'                    Me.SuspendLayout()
'                    '
'                    'splMain
'                    '
'                    Me.splMain.AccessibleDescription = Nothing
'                    Me.splMain.AccessibleName = Nothing
'                    resources.ApplyResources(Me.splMain, "splMain")
'                    Me.splMain.BackgroundImage = Nothing
'                    Me.splMain.Font = Nothing
'                    Me.splMain.Name = "splMain"
'                    '
'                    'splMain.Panel1
'                    '
'                    Me.splMain.Panel1.AccessibleDescription = Nothing
'                    Me.splMain.Panel1.AccessibleName = Nothing
'                    resources.ApplyResources(Me.splMain.Panel1, "splMain.Panel1")
'                    Me.splMain.Panel1.BackgroundImage = Nothing
'                    Me.splMain.Panel1.Controls.Add(Me.tlpItems)
'                    Me.splMain.Panel1.Font = Nothing
'                    Me.totTT.SetToolTip(Me.splMain.Panel1, resources.GetString("splMain.Panel1.ToolTip"))
'                    '
'                    'splMain.Panel2
'                    '
'                    Me.splMain.Panel2.AccessibleDescription = Nothing
'                    Me.splMain.Panel2.AccessibleName = Nothing
'                    resources.ApplyResources(Me.splMain.Panel2, "splMain.Panel2")
'                    Me.splMain.Panel2.BackgroundImage = Nothing
'                    Me.splMain.Panel2.Controls.Add(Me.pgrProperty)
'                    Me.splMain.Panel2.Controls.Add(Me.lblItemInfo)
'                    Me.splMain.Panel2.Font = Nothing
'                    Me.totTT.SetToolTip(Me.splMain.Panel2, resources.GetString("splMain.Panel2.ToolTip"))
'                    Me.totTT.SetToolTip(Me.splMain, resources.GetString("splMain.ToolTip"))
'                    '
'                    'tlpItems
'                    '
'                    Me.tlpItems.AccessibleDescription = Nothing
'                    Me.tlpItems.AccessibleName = Nothing
'                    resources.ApplyResources(Me.tlpItems, "tlpItems")
'                    Me.tlpItems.BackgroundImage = Nothing
'                    Me.tlpItems.Controls.Add(Me.lstItems, 0, 0)
'                    Me.tlpItems.Controls.Add(Me.cmdUp, 2, 0)
'                    Me.tlpItems.Controls.Add(Me.cmdDown, 2, 1)
'                    Me.tlpItems.Controls.Add(Me.tosAdd, 0, 3)
'                    Me.tlpItems.Controls.Add(Me.tosRemove, 1, 3)
'                    Me.tlpItems.Controls.Add(Me.cmdCancel, 2, 3)
'                    Me.tlpItems.Controls.Add(Me.cmdOK, 2, 2)
'                    Me.tlpItems.Font = Nothing
'                    Me.tlpItems.Name = "tlpItems"
'                    Me.totTT.SetToolTip(Me.tlpItems, resources.GetString("tlpItems.ToolTip"))
'                    '
'                    'lstItems
'                    '
'                    Me.lstItems.AccessibleDescription = Nothing
'                    Me.lstItems.AccessibleName = Nothing
'                    resources.ApplyResources(Me.lstItems, "lstItems")
'                    Me.lstItems.BackgroundImage = Nothing
'                    Me.tlpItems.SetColumnSpan(Me.lstItems, 2)
'                    Me.lstItems.Font = Nothing
'                    Me.lstItems.FormattingEnabled = True
'                    Me.lstItems.Name = "lstItems"
'                    Me.tlpItems.SetRowSpan(Me.lstItems, 3)
'                    Me.lstItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
'                    Me.totTT.SetToolTip(Me.lstItems, resources.GetString("lstItems.ToolTip"))
'                    '
'                    'cmdUp
'                    '
'                    Me.cmdUp.AccessibleDescription = Nothing
'                    Me.cmdUp.AccessibleName = Nothing
'                    resources.ApplyResources(Me.cmdUp, "cmdUp")
'                    Me.cmdUp.BackgroundImage = Nothing
'                    Me.cmdUp.Name = "cmdUp"
'                    Me.totTT.SetToolTip(Me.cmdUp, resources.GetString("cmdUp.ToolTip"))
'                    Me.cmdUp.UseVisualStyleBackColor = True
'                    '
'                    'cmdDown
'                    '
'                    Me.cmdDown.AccessibleDescription = Nothing
'                    Me.cmdDown.AccessibleName = Nothing
'                    resources.ApplyResources(Me.cmdDown, "cmdDown")
'                    Me.cmdDown.BackgroundImage = Nothing
'                    Me.cmdDown.Name = "cmdDown"
'                    Me.totTT.SetToolTip(Me.cmdDown, resources.GetString("cmdDown.ToolTip"))
'                    Me.cmdDown.UseVisualStyleBackColor = True
'                    '
'                    'tosAdd
'                    '
'                    Me.tosAdd.AccessibleDescription = Nothing
'                    Me.tosAdd.AccessibleName = Nothing
'                    resources.ApplyResources(Me.tosAdd, "tosAdd")
'                    Me.tosAdd.BackgroundImage = Nothing
'                    Me.tosAdd.CanOverflow = False
'                    Me.tosAdd.Font = Nothing
'                    Me.tosAdd.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
'                    Me.tosAdd.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbAdd})
'                    Me.tosAdd.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
'                    Me.tosAdd.Name = "tosAdd"
'                    Me.tosAdd.ShowItemToolTips = False
'                    Me.tosAdd.TabStop = True
'                    Me.totTT.SetToolTip(Me.tosAdd, resources.GetString("tosAdd.ToolTip"))
'                    '
'                    'tsbAdd
'                    '
'                    Me.tsbAdd.AccessibleDescription = Nothing
'                    Me.tsbAdd.AccessibleName = Nothing
'                    resources.ApplyResources(Me.tsbAdd, "tsbAdd")
'                    Me.tsbAdd.AutoToolTip = False
'                    Me.tsbAdd.BackgroundImage = Nothing
'                    Me.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
'                    Me.tsbAdd.Name = "tsbAdd"
'                    Me.tsbAdd.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
'                    Me.tsbAdd.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
'                    '
'                    'tosRemove
'                    '
'                    Me.tosRemove.AccessibleDescription = Nothing
'                    Me.tosRemove.AccessibleName = Nothing
'                    resources.ApplyResources(Me.tosRemove, "tosRemove")
'                    Me.tosRemove.BackgroundImage = Nothing
'                    Me.tosRemove.CanOverflow = False
'                    Me.tosRemove.Font = Nothing
'                    Me.tosRemove.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
'                    Me.tosRemove.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbRemove})
'                    Me.tosRemove.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
'                    Me.tosRemove.Name = "tosRemove"
'                    Me.tosRemove.ShowItemToolTips = False
'                    Me.tosRemove.TabStop = True
'                    Me.totTT.SetToolTip(Me.tosRemove, resources.GetString("tosRemove.ToolTip"))
'                    '
'                    'tsbRemove
'                    '
'                    Me.tsbRemove.AccessibleDescription = Nothing
'                    Me.tsbRemove.AccessibleName = Nothing
'                    Me.tsbRemove.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
'                    resources.ApplyResources(Me.tsbRemove, "tsbRemove")
'                    Me.tsbRemove.AutoToolTip = False
'                    Me.tsbRemove.BackgroundImage = Nothing
'                    Me.tsbRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
'                    Me.tsbRemove.DoubleClickEnabled = True
'                    Me.tsbRemove.Name = "tsbRemove"
'                    Me.tsbRemove.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
'                    '
'                    'cmdCancel
'                    '
'                    Me.cmdCancel.AccessibleDescription = Nothing
'                    Me.cmdCancel.AccessibleName = Nothing
'                    resources.ApplyResources(Me.cmdCancel, "cmdCancel")
'                    Me.cmdCancel.BackgroundImage = Nothing
'                    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
'                    Me.cmdCancel.Font = Nothing
'                    Me.cmdCancel.Name = "cmdCancel"
'                    Me.totTT.SetToolTip(Me.cmdCancel, resources.GetString("cmdCancel.ToolTip"))
'                    Me.cmdCancel.UseVisualStyleBackColor = True
'                    '
'                    'cmdOK
'                    '
'                    Me.cmdOK.AccessibleDescription = Nothing
'                    Me.cmdOK.AccessibleName = Nothing
'                    resources.ApplyResources(Me.cmdOK, "cmdOK")
'                    Me.cmdOK.BackgroundImage = Nothing
'                    Me.cmdOK.Font = Nothing
'                    Me.cmdOK.Name = "cmdOK"
'                    Me.totTT.SetToolTip(Me.cmdOK, resources.GetString("cmdOK.ToolTip"))
'                    Me.cmdOK.UseVisualStyleBackColor = True
'                    '
'                    'pgrProperty
'                    '
'                    Me.pgrProperty.AccessibleDescription = Nothing
'                    Me.pgrProperty.AccessibleName = Nothing
'                    resources.ApplyResources(Me.pgrProperty, "pgrProperty")
'                    Me.pgrProperty.BackgroundImage = Nothing
'                    Me.pgrProperty.Font = Nothing
'                    Me.pgrProperty.Name = "pgrProperty"
'                    Me.totTT.SetToolTip(Me.pgrProperty, resources.GetString("pgrProperty.ToolTip"))
'                    '
'                    'lblItemInfo
'                    '
'                    Me.lblItemInfo.AccessibleDescription = Nothing
'                    Me.lblItemInfo.AccessibleName = Nothing
'                    resources.ApplyResources(Me.lblItemInfo, "lblItemInfo")
'                    Me.lblItemInfo.AutoEllipsis = True
'                    Me.lblItemInfo.Font = Nothing
'                    Me.lblItemInfo.Name = "lblItemInfo"
'                    Me.totTT.SetToolTip(Me.lblItemInfo, resources.GetString("lblItemInfo.ToolTip"))
'                    '
'                    'Class1
'                    '
'                    Me.AcceptButton = Me.cmdOK
'                    Me.AccessibleDescription = Nothing
'                    Me.AccessibleName = Nothing
'                    resources.ApplyResources(Me, "$this")
'                    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
'                    Me.BackgroundImage = Nothing
'                    Me.CancelButton = Me.cmdCancel
'                    Me.Controls.Add(Me.splMain)
'                    Me.Font = Nothing
'                    Me.Icon = Nothing
'                    Me.MaximizeBox = False
'                    Me.MinimizeBox = False
'                    Me.Name = "Class1"
'                    Me.ShowIcon = False
'                    Me.ShowInTaskbar = False
'                    Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
'                    Me.totTT.SetToolTip(Me, resources.GetString("$this.ToolTip"))
'                    Me.splMain.Panel1.ResumeLayout(False)
'                    Me.splMain.Panel2.ResumeLayout(False)
'                    Me.splMain.Panel2.PerformLayout()
'                    Me.splMain.ResumeLayout(False)
'                    Me.tlpItems.ResumeLayout(False)
'                    Me.tlpItems.PerformLayout()
'                    Me.tosAdd.ResumeLayout(False)
'                    Me.tosAdd.PerformLayout()
'                    Me.tosRemove.ResumeLayout(False)
'                    Me.tosRemove.PerformLayout()
'                    Me.ResumeLayout(False)

'                End Sub
'                ''' <summary>Main <see cref="SplitContainer"/> that splits form into part of list and part of <see cref="PropertyGrid"/></summary>
'                Private WithEvents splMain As System.Windows.Forms.SplitContainer
'                ''' <summary>List part of collection is located here</summary>
'                Private WithEvents tlpItems As System.Windows.Forms.TableLayoutPanel
'                ''' <summary>Shows list of collection items</summary>
'                Private WithEvents lstItems As System.Windows.Forms.ListBox
'                ''' <summary>Moves selected item up</summary>
'                Private WithEvents cmdUp As System.Windows.Forms.Button
'                ''' <summary>Moves selected item down</summary>
'                Private WithEvents cmdDown As System.Windows.Forms.Button
'                ''' <summary>Contains <see cref="tsbAdd"/></summary>
'                Private WithEvents tosAdd As System.Windows.Forms.ToolStrip
'                ''' <summary>Contains items for adding new items to the collection</summary>
'                Private WithEvents tsbAdd As System.Windows.Forms.ToolStripSplitButton
'                ''' <summary>Contains <see cref="tsbRemove"/></summary>
'                Private WithEvents tosRemove As System.Windows.Forms.ToolStrip
'                ''' <summary>Removes selected items</summary>
'                Private WithEvents tsbRemove As System.Windows.Forms.ToolStripButton
'                ''' <summary>Displays and allows edit properties of selected items</summary>
'                Private WithEvents pgrProperty As System.Windows.Forms.PropertyGrid
'                ''' <summary>Closes form with no changes on collection</summary>
'                Private WithEvents cmdCancel As System.Windows.Forms.Button
'                ''' <summary>Closes form and applies changes on collection</summary>
'                Private WithEvents cmdOK As System.Windows.Forms.Button
'                ''' <summary>Displays tool tip text on some controls</summary>
'                Private WithEvents totTT As System.Windows.Forms.ToolTip
'#End Region
'                ''' <summary>Contains value of the <see cref="Editor"/> property</summary>
'                <EditorBrowsable(EditorBrowsableState.Never)> _
'                Private _Editor As LinkLabelItemsEditor
'                ''' <summary>Initializes a new instance of the <see cref="LinkLabelItemsEditor"/> class.</summary>
'                ''' <param name="editor">The <see cref="LinkLabelItemsEditor"/> to use for editing the collection.</param>
'                ''' <exception cref="ArgumentException">
'                ''' <see cref="LinkLabelItemsEditor.CollectionType"/> of <paramref name="Editor"/> is not <see cref="ListWithEvents(Of LinkLabelItem)"/>
'                ''' -or-
'                ''' <see cref="LinkLabelItemsEditor.CollectionItemType"/> of <paramref name="Editor"/> is not <see cref="LinkLabelItem"/>
'                ''' -or-
'                ''' Any <see cref="LinkLabelItemsEditor.NewItemTypes"/> of <paramref name="Editor"/> is not <see cref="LinkLabelItem"/>
'                ''' </exception>
'                Public Sub New(ByVal Editor As LinkLabelItemsEditor)
'                    MyBase.New(Editor)
'                    If Not Editor.CollectionType.Equals(GetType(ListWithEvents(Of LinkLabelItem))) AndAlso Not Editor.CollectionType.IsSubclassOf(GetType(ListWithEvents(Of LinkLabelItem))) Then
'                        Throw New ArgumentException("Editor.CollectionType must be ListWithEvents(Of LinkLabelItem)")
'                    End If
'                    If Not Editor.CollectionItemType.Equals(GetType(LinkLabelItem)) AndAlso Not Editor.CollectionType.IsSubclassOf(GetType(LinkLabelItem)) Then
'                        Throw (New ArgumentException("Editor.CollectionItemType must be LinkLabelItem"))
'                    End If
'                    Me.Editor = Editor

'                    MyClass.InitializeComponent()
'                    lblItemInfo.MaximumSize = New Drawing.Size(lblItemInfo.MaximumSize.Width, lblItemInfo.Size.Height * 3)

'                    'Show types tha can be added into collection
'                    For Each t As Type In Editor.NewItemTypes
'                        If Not t.IsSubclassOf(GetType(LinkLabelItem)) AndAlso Not t.Equals(GetType(LinkLabelItem)) Then
'                            Throw New ArgumentException("All types in Editor.NewItemTypes must inherit from LinkLabelItem")
'                        Else
'                            Dim itm As ToolStripItem = tsbAdd.DropDownItems.Add(t.Name)
'                            itm.Tag = t
'                            Dim inhB As Object() = t.GetCustomAttributes(GetType(Drawing.ToolboxBitmapAttribute), True)
'                            Dim nInhB As Object() = t.GetCustomAttributes(GetType(Drawing.ToolboxBitmapAttribute), False)
'                            Dim Bitmap As Drawing.ToolboxBitmapAttribute = Nothing
'                            If nInhB IsNot Nothing AndAlso nInhB.Length > 0 Then
'                                Bitmap = nInhB(0)
'                            ElseIf inhB IsNot Nothing AndAlso inhB.Length > 0 Then
'                                Bitmap = inhB(0)
'                            End If
'                            If Bitmap IsNot Nothing Then
'                                itm.Image = Bitmap.GetImage(t)
'                            End If
'                            AddHandler itm.Click, AddressOf Add_Click
'                        End If
'                    Next t
'                    'Acording to editor setting set whether multiple instances can be selected or not
'                    If Not Editor.CanSelectMultipleInstances Then lstItems.SelectionMode = SelectionMode.One
'                End Sub

'                ''' <summary>Adds item to collection</summary>
'                Private Sub Add_Click(ByVal sender As Object, ByVal e As [EventArgs])
'                    Try
'                        Dim index As Integer = _
'                                lstItems.Items.Add(MyBase.CreateInstance(CType(sender, ToolStripItem).Tag))
'                        lstItems.SelectedItems.Clear()
'                        lstItems.SelectedIndex = index
'                        MyBase.Items = New ArrayList(lstItems.Items).ToArray
'                    Catch ex As Exception
'                        MsgBox("Cannot create instance of type " & CType(CType(sender, ToolStripItem).Tag, Type).FullName & ". " & ex.GetType.FullName & " was thrown when obtaining new instance:" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "LinkLabel Items Editor")
'                    End Try
'                End Sub

'                ''' <summary><see cref="LinkLabelItemsEditor"/> used for editin collection</summary>
'                Protected Property Editor() As LinkLabelItemsEditor
'                    <DebuggerStepThrough()> Get
'                        Return _Editor
'                    End Get
'                    <DebuggerStepThrough()> Private Set(ByVal value As LinkLabelItemsEditor)
'                        _Editor = value
'                    End Set
'                End Property
'                ''' <summary>
'                ''' Provides an opportunity to perform processing when a collection value has changed.
'                ''' Shows items of collection in <see cref="ListBox"/>.
'                ''' </summary>
'                ''' <exception cref="InvalidCastException"><see cref="System.ComponentModel.Design.CollectionEditor.CollectionForm.EditValue"/> is not of type <see cref="ListWithEvents(Of LinkLabelItem)"/></exception>
'                Protected Overrides Sub OnEditValueChanged()
'                    If MyBase.EditValue IsNot Nothing AndAlso Not TypeOf MyBase.EditValue Is ListWithEvents(Of LinkLabelItem) Then
'                        Throw New InvalidCastException("Value of type " & Me.EditValue.GetType.FullName & " cannot be converted to " & GetType(ListWithEvents(Of LinkLabelItem)).FullName)
'                    End If
'                    lstItems.Items.Clear()
'                    If EditValue IsNot Nothing Then
'                        pgrProperty.Site = New PropertyGridSite(MyBase.Context, pgrProperty)
'                        For Each itm As LinkLabelItem In Me.Editor.GetItems(EditValue)
'                            lstItems.Items.Add(itm)
'                        Next itm
'                    End If
'                End Sub
'                ''' <summary>Shows the dialog box for the collection editor using the specified <see cref="System.Windows.Forms.Design.IWindowsFormsEditorService"/> object.</summary>
'                ''' <param name="edSvc">An <see cref="System.Windows.Forms.Design.IWindowsFormsEditorService"/> that can be used to show the dialog box.</param>
'                ''' <returns>A <see cref="System.Windows.Forms.DialogResult"/> that indicates the result code returned from the dialog box.</returns>
'                Protected Overrides Function ShowEditorDialog(ByVal edSvc As System.Windows.Forms.Design.IWindowsFormsEditorService) As System.Windows.Forms.DialogResult
'                    Dim service1 As IComponentChangeService = Nothing
'                    Dim result1 As DialogResult = DialogResult.OK
'                    Try
'                        service1 = DirectCast(Me.Editor.Context.GetService(GetType(IComponentChangeService)), IComponentChangeService)
'                        If (Not service1 Is Nothing) Then
'                            'AddHandler service1.ComponentChanged, New ComponentChangedEventHandler(AddressOf Me.OnComponentChanged)
'                        End If
'                        MyBase.ActiveControl = Me.lstItems
'                        result1 = MyBase.ShowEditorDialog(edSvc)
'                    Finally
'                        If (Not service1 Is Nothing) Then
'                            'RemoveHandler service1.ComponentChanged, New ComponentChangedEventHandler(AddressOf Me.OnComponentChanged)
'                        End If
'                    End Try
'                    Return result1
'                End Function
'                'Private Sub OnComponentChanged(ByVal sender As Object, ByVal e As ComponentChangedEventArgs)
'                'End Sub

'                ''' <summary>This class was copied from Friend Class System.ComponentModel.Design.CollectionEditor.PropertyGridSite using Reflector</summary>
'                ''' <remarks>
'                ''' I'm not very sure what exactly this class does or what exactly is used for. It supports <see cref="CollectionForm"/> functionality. Its instance is passed to <see cref="PropertyGrid.Site"/> property of <see cref="pgrProperty"/> in <see cref="OnEditValueChanged"/>
'                ''' IMHO it supports design-time interaction between <see cref="PropertyGrid"/> and the object being edited.
'                ''' </remarks>
'                <EditorBrowsable(EditorBrowsableState.Advanced)> _
'                <DebuggerNonUserCode()> _
'                Private Class PropertyGridSite : Implements ISite, IServiceProvider
'                    ''' <summary>Contains value of the <see cref="Component"/> property</summary>
'                    <EditorBrowsable(EditorBrowsableState.Never)> _
'                    Private comp As IComponent
'                    ''' <summary>Identifies if <see cref="GetService"/> is currently lying on callstack</summary>
'                    Private inGetService As Boolean
'                    ''' <summary>Contains instance of <see cref="IServiceProvider"/></summary>
'                    Private sp As IServiceProvider
'                    ''' <summary>Gets the component associated with the <see cref="System.ComponentModel.ISite"/> when implemented by a class.</summary>
'                    ''' <returns>The <see cref="System.ComponentModel.IComponent"/> instance associated with the <see cref="System.ComponentModel.ISite"/>.</returns>
'                    Public ReadOnly Property Component() As System.ComponentModel.IComponent Implements System.ComponentModel.ISite.Component
'                        <DebuggerHidden()> Get
'                            Return Me.comp
'                        End Get
'                    End Property
'                    ''' <summary>Gets the <see cref="System.ComponentModel.IContainer"/> associated with the <see cref="System.ComponentModel.ISite"/> when implemented by a class.</summary>
'                    ''' <returns>Always null in this implementation.</returns>
'                    Public ReadOnly Property Container() As System.ComponentModel.IContainer Implements System.ComponentModel.ISite.Container
'                        <DebuggerHidden()> Get
'                            Return Nothing
'                        End Get
'                    End Property
'                    ''' <summary>Determines whether the component is in design mode when implemented by a class.</summary>
'                    ''' <returns>Always false in this implementation</returns>
'                    Public ReadOnly Property DesignMode() As Boolean Implements System.ComponentModel.ISite.DesignMode
'                        <DebuggerHidden()> Get
'                            Return False
'                        End Get
'                    End Property

'                    ''' <summary>Gets or sets the name of the component associated with the <see cref="System.ComponentModel.ISite"/> when implemented by a class.</summary>
'                    ''' <returns>Always an empty string in this implementation</returns>
'                    ''' <value>Setting value has no effect</value>
'                    Public Property Name() As String Implements System.ComponentModel.ISite.Name
'                        <DebuggerHidden()> Get
'                            Return Nothing
'                        End Get
'                        <DebuggerHidden()> Set(ByVal value As String)
'                        End Set
'                    End Property
'                    ''' <summary>Gets the service object of the specified type.</summary>
'                    ''' <param name="serviceType">An object that specifies the type of service object to get.</param>
'                    ''' <returns>A service object of type serviceType.-or- null if there is no service object of type <paramref name="serviceType"/>.</returns>
'                    <DebuggerHidden()> Public Function GetService(ByVal serviceType As System.Type) As Object Implements System.IServiceProvider.GetService
'                        If (Not Me.inGetService AndAlso (Not Me.sp Is Nothing)) Then
'                            Try
'                                Me.inGetService = True
'                                Return Me.sp.GetService(serviceType)
'                            Finally
'                                Me.inGetService = False
'                            End Try
'                        End If
'                        Return Nothing
'                    End Function
'                    ''' <summary>CTor</summary>
'                    ''' <param name="sp">An instance of <see cref="IServiceProvider"/>. This value is never used.</param>
'                    ''' <param name="comp">Value that will be returned by the <see cref="Component"/> property</param>
'                    <DebuggerHidden()> Public Sub New(ByVal sp As IServiceProvider, ByVal comp As IComponent)
'                        Me.inGetService = False
'                        Me.sp = sp
'                        Me.comp = comp
'                    End Sub
'                End Class

'                ''' <summary>Gets or sets the collection object to edit.</summary>
'                ''' <returns>The collection object to edit.</returns>
'                Public Shadows Property EditValue() As ListWithEvents(Of LinkLabelItem)
'                    Get
'                        Return MyBase.EditValue
'                    End Get
'                    Set(ByVal value As ListWithEvents(Of LinkLabelItem))
'                        MyBase.EditValue = value
'                    End Set
'                End Property
'                ''' <summary>Closes form with no changes on collection</summary>
'                ''' <remarks>Changes on collection items' properties are not discarded</remarks>
'                Private Sub cmdClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
'                    'Me.Editor.SetItems(EditValue, New ArrayList(EditValue).ToArray)
'                    ClosingCancel = True
'                    Me.Close()
'                End Sub
'                ''' <summary>If true than <see cref="CollectionForm_FormClosing"/> cancels editing</summary>
'                Private ClosingCancel As Boolean = True

'                Private Sub lstItems_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstItems.KeyDown
'                    If e.KeyCode = Keys.Delete Then tsbRemove_Click(sender, e)
'                End Sub

'                Private Sub lstItems_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstItems.SelectedIndexChanged
'                    If lstItems.SelectedItems.Count < 1 Then
'                        pgrProperty.SelectedObject = Nothing
'                        Enable(False)
'                        lblItemInfo.Text = resources.GetString("lblItemInfo.Text")
'                    Else
'                        pgrProperty.SelectedObjects = (New ArrayList(lstItems.SelectedItems)).ToArray
'                        Enable(VisualBasic.iif(lstItems.SelectedItems.Count > 1, EnableMode.Multi, EnableMode.True))
'                        If lstItems.SelectedItems.Count = 1 Then
'                            lblItemInfo.Text = lstItems.SelectedItems(0).GetType.Name & ": " & lstItems.SelectedItems(0).ToString.Replace(vbCrLf, " ").Replace(vbCr, " ").Replace(vbLf, " ").Replace(vbTab, " ")
'                        Else
'                            lblItemInfo.Text = resources.GetString("lblItemInfo.Text multiple")
'                        End If
'                    End If
'                End Sub
'                ''' <summary>Modes for <see cref="Enable"/></summary>
'                Private Enum EnableMode
'                    ''' <summary>Set to False</summary>
'                    [False] = False
'                    ''' <summary>Set to True</summary>
'                    [True] = True
'                    ''' <summary>Set to True (only controls that can be used when multiple items are selected)</summary>
'                    Multi = 99
'                End Enum
'                ''' <summary>Sets <see cref="Control.Enabled"/> for item-related buttons (<see cref="cmdUp"/>, <see cref="cmdDown"/>, <see cref="tsbRemove"/>)</summary>
'                ''' <param name="Enabled">Mode of setting value</param>
'                Private Sub Enable(ByVal Enabled As EnableMode)
'                    cmdDown.Enabled = Enabled = EnableMode.True AndAlso lstItems.SelectedIndex < lstItems.Items.Count - 1
'                    cmdUp.Enabled = Enabled = EnableMode.True AndAlso lstItems.SelectedIndex > 0
'                    tsbRemove.Enabled = Enabled <> EnableMode.False
'                End Sub
'                ''' <summary>Moves selected item up</summary>
'                Private Sub cmdUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUp.Click
'                    Dim index As Integer = lstItems.SelectedIndex
'                    Dim item As LinkLabelItem = lstItems.SelectedItem
'                    lstItems.Items.RemoveAt(index)
'                    lstItems.Items.Insert(index - 1, item)
'                    lstItems.SelectedIndex = index - 1
'                End Sub
'                ''' <summary>Moves selected item down</summary>
'                Private Sub cmdDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDown.Click
'                    Dim index As Integer = lstItems.SelectedIndex
'                    Dim item As LinkLabelItem = lstItems.SelectedItem
'                    lstItems.Items.RemoveAt(index)
'                    lstItems.Items.Insert(index + 1, item)
'                    lstItems.SelectedIndex = index + 1
'                End Sub
'                ''' <summary>Closes form and applies changes</summary>
'                Private Sub cmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click
'                    Me.Editor.SetItems(Me.EditValue, New System.Collections.ArrayList(lstItems.Items).ToArray)
'                    ClosingCancel = False
'                    Me.Close()
'                End Sub
'                ''' <summary>Removes selected items</summary>
'                Private Sub tsbRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsbRemove.Click
'                    Dim RemoveIndex As Integer = 0
'                    Dim OldMaxSI As Integer = -1
'                    While lstItems.SelectedItems.Count > RemoveIndex
'                        OldMaxSI = Math.Max(lstItems.SelectedIndices(RemoveIndex), OldMaxSI)
'                        If MyBase.CanRemoveInstance(lstItems.SelectedItems(RemoveIndex)) Then
'                            MyBase.DestroyInstance(lstItems.SelectedItems(RemoveIndex))
'                            lstItems.Items.Remove(lstItems.SelectedItems(RemoveIndex))
'                        Else
'                            RemoveIndex += 1
'                        End If
'                    End While
'                    If lstItems.SelectedItems.Count = 0 AndAlso lstItems.Items.Count > 0 Then
'                        lstItems.SelectedIndex = Math.Min(lstItems.Items.Count - 1, Math.Max(0, OldMaxSI - 1))
'                    End If
'                End Sub

'                Private Sub pgrProperty_PropertyValueChanged(ByVal s As Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles pgrProperty.PropertyValueChanged
'                    For Each itm As Integer In lstItems.SelectedIndices
'                        lstItems.Items(itm) = lstItems.Items(itm)
'                    Next itm
'                End Sub

'                Private Sub CollectionForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
'                    If ClosingCancel Then
'                        Me.Editor.Context.OnComponentChanged()
'                    End If
'                    MyBase.DialogResult = System.Windows.Forms.DialogResult.OK
'                End Sub

'                Private Sub CollectionForm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
'                    ResizeLabel()
'                End Sub

'                Private Sub splMain_SplitterMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles splMain.SplitterMoved
'                    ResizeLabel()
'                End Sub
'                ''' <summary>Changes <see cref="Label.MaximumSize"/> of <see cref="lblItemInfo"/> in order not to be wider than its container.</summary>
'                Private Sub ResizeLabel()
'                    lblItemInfo.MaximumSize = New Drawing.Size(splMain.Panel2.ClientSize.Width, lblItemInfo.MaximumSize.Height)
'                End Sub
'            End Class : End Class : End Class : End Namespace
'#End If