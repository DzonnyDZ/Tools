Imports Tools.CollectionsT.GenericT, Tools.ReflectionT
Imports System.Windows.Forms, System.Reflection
Imports System.Drawing, System.Linq
Imports Tools.ComponentModelT

Namespace WindowsT.FormsT
    'ASAP:
    ''' <summary>Control for browsing .NET assemblies</summary>
    Public Class ObjectBrowser
        ''' <summary>Contains value of the <see cref="Objects"/> property</summary>
        ''' <remarks>Can be any object</remarks>
        Private WithEvents Assemblies As New ListWithEvents(Of Object)
        Private Initializing As Boolean = True
        ''' <summary>CTor</summary>
        Public Sub New()
            Assemblies.AllowAddCancelableEventsHandlers = False
            AddHandler ReflectionT.ImageRequested, AddressOf ImageRequested
            InitializeComponent()
            Initializing = False
            tvwObjects.Select()
        End Sub

#Region "Show"
        ''' <summary>Contains value of the <see cref="ShowInheritedMembers"/> property</summary>
        Private _ShowInheritedMembers As Boolean
        ''' <summary>Gets or sets value indicating if inherited members are shown</summary>
        ''' <value>True to show inherited members, false to hide them</value>
        ''' <returns>Value indicating if inherited members are shown</returns>
        <DefaultValue(False)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if inherited members are shown.")> _
        Public Property ShowInheritedMembers() As Boolean 'Localize: Description
            Get
                Return _ShowInheritedMembers
            End Get
            Set(ByVal value As Boolean)
                _ShowInheritedMembers = value
                'TODO: Apply change
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ShowNestedTypes"/> property</summary>
        Private _ShowNestedTypes As Boolean = True
        ''' <summary>Gets or sets value indicating if nested types are shown</summary>
        ''' <value>True to show nested types, false to hide them</value>
        ''' <returns>Value indicating if nested types are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if nested types are shown.")> _
        Public Property ShowNestedTypes() As Boolean 'Localize: Description
            Get
                Return ShowPrivateMembers
            End Get
            Set(ByVal value As Boolean)
                _ShowNestedTypes = value
                'TODO: Apply change
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowPrivateMembers"/> property</summary>
        Private _ShowPrivateMembers As Boolean = True
        ''' <summary>Gets or sets value indicating if private members are shown</summary>
        ''' <value>True to show private members, false to hide them</value>
        ''' <returns>Value indicating if private members are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if private members are shown.")> _
        Public Property ShowPrivateMembers() As Boolean 'Localize: Description
            Get
                Return _ShowPrivateMembers
            End Get
            Set(ByVal value As Boolean)
                _ShowPrivateMembers = value
                'TODO: Apply change
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowInternalMembers"/> property</summary>
        Private _ShowInternalMembers As Boolean = True
        ''' <summary>Gets or sets value indicating if internal members are shown</summary>
        ''' <value>True to show internal members, false to hide them</value>
        ''' <returns>Value indicating if internal members are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if Internal members are shown.")> _
        Public Property ShowInternalMembers() As Boolean 'Localize: Description
            Get
                Return _ShowInternalMembers
            End Get
            Set(ByVal value As Boolean)
                _ShowInternalMembers = value
                'TODO: Apply change
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowProtectedMembers"/> property</summary>
        Private _ShowProtectedMembers As Boolean = True
        ''' <summary>Gets or sets value indicating if protected members are shown</summary>
        ''' <value>True to show protected members, false to hide them</value>
        ''' <returns>Value indicating if protected members are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if Protected members are shown.")> _
        Public Property ShowProtectedMembers() As Boolean 'Localize: Description
            Get
                Return _ShowProtectedMembers
            End Get
            Set(ByVal value As Boolean)
                _ShowProtectedMembers = value
                'TODO: Apply change
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowinstanceMembers"/> property</summary>
        Private _ShowInstanceMembers As Boolean = True
        ''' <summary>Gets or sets value indicating if instance members are shown</summary>
        ''' <value>True to show instance members, false to hide them</value>
        ''' <returns>Value indicating if instance members are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if instance members are shown.")> _
        Public Property ShowInstanceMembers() As Boolean 'Localize: Description
            Get
                Return _ShowinstanceMembers
            End Get
            Set(ByVal value As Boolean)
                _ShowinstanceMembers = value
                'TODO: Apply change
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowstaticMembers"/> property</summary>
        Private _ShowStaticMembers As Boolean = True
        ''' <summary>Gets or sets value indicating if static members are shown</summary>
        ''' <value>True to show static members, false to hide them</value>
        ''' <returns>Value indicating if static members are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if static members are shown.")> _
        Public Property ShowStaticMembers() As Boolean 'Localize: Description
            Get
                Return _ShowstaticMembers
            End Get
            Set(ByVal value As Boolean)
                _ShowstaticMembers = value
                'TODO: Apply change
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowspecialMembers"/> property</summary>
        Private _ShowSpecialMembers As Boolean = True
        ''' <summary>Gets or sets value indicating if special members (parts of properties and events) are shown</summary>
        ''' <value>True to show special members, false to hide them</value>
        ''' <returns>Value indicating if special members are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if special members (parts of properties and events) are shown.")> _
        Public Property ShowSpecialMembers() As Boolean 'Localize: Description
            Get
                Return _ShowSpecialMembers
            End Get
            Set(ByVal value As Boolean)
                _ShowSpecialMembers = value
                'TODO: Apply change
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowGlobalMembers"/> property</summary>
        Private _ShowGlobalMembers As Boolean = True
        ''' <summary>Gets or sets value indicating if Global members (parts of properties and events) are shown</summary>
        ''' <value>True to show Global members, false to hide them</value>
        ''' <returns>Value indicating if Global members are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if Global members (parts of properties and events) are shown.")> _
        Public Property ShowGlobalMembers() As Boolean 'Localize: Description
            Get
                Return _ShowGlobalMembers
            End Get
            Set(ByVal value As Boolean)
                _ShowGlobalMembers = value
                'TODO: Apply change
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowProperties"/> property</summary>
        Private _ShowProperties As Boolean = True
        ''' <summary>Gets or sets value indicating if properties are shown</summary>
        ''' <value>True to show properties  false to hide them</value>
        ''' <returns>Value indicating if properties are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if properties are shown.")> _
        Public Property ShowProperties() As Boolean 'Localize: Description
            Get
                Return _ShowProperties
            End Get
            Set(ByVal value As Boolean)
                _ShowProperties = value
                'TODO: Apply change
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowEvents"/> property</summary>
        Private _ShowEvents As Boolean = True
        ''' <summary>Gets or sets value indicating if Events are shown</summary>
        ''' <value>True to show Events  false to hide them</value>
        ''' <returns>Value indicating if Events are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if Events are shown.")> _
        Public Property ShowEvents() As Boolean 'Localize: Description
            Get
                Return _ShowEvents
            End Get
            Set(ByVal value As Boolean)
                _ShowEvents = value
                'TODO: Apply change
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowMethods"/> property</summary>
        Private _ShowMethods As Boolean = True
        ''' <summary>Gets or sets value indicating if Methods are shown</summary>
        ''' <value>True to show Methods  false to hide them</value>
        ''' <returns>Value indicating if Methods are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if Methods are shown.")> _
        Public Property ShowMethods() As Boolean 'Localize: Description
            Get
                Return _ShowMethods
            End Get
            Set(ByVal value As Boolean)
                _ShowMethods = value
                'TODO: Apply change
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowFields"/> property</summary>
        Private _ShowFields As Boolean = True
        ''' <summary>Gets or sets value indicating if Fields are shown</summary>
        ''' <value>True to show Fields  false to hide them</value>
        ''' <returns>Value indicating if Fields are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if Fields are shown.")> _
        Public Property ShowFields() As Boolean 'Localize: Description
            Get
                Return _ShowFields
            End Get
            Set(ByVal value As Boolean)
                _ShowFields = value
                'TODO: Apply change
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowCTors"/> property</summary>
        Private _ShowCTors As Boolean = True
        ''' <summary>Gets or sets value indicating if CTors are shown</summary>
        ''' <value>True to show CTors  false to hide them</value>
        ''' <returns>Value indicating if CTors are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if CTors are shown.")> _
        Public Property ShowCTors() As Boolean 'Localize: Description
            Get
                Return _ShowCTors
            End Get
            Set(ByVal value As Boolean)
                _ShowCTors = value
                'TODO: Apply change
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowInitializers"/> property</summary>
        Private _ShowInitializers As Boolean = True
        ''' <summary>Gets or sets value indicating if Initializers are shown</summary>
        ''' <value>True to show Initializers  false to hide them</value>
        ''' <returns>Value indicating if Initializers are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if Initializers are shown.")> _
        Public Property ShowInitializers() As Boolean 'Localize: Description
            Get
                Return _ShowInitializers
            End Get
            Set(ByVal value As Boolean)
                _ShowInitializers = value
                'TODO: Apply change
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowgenericArguments"/> property</summary>
        Private _ShowgenericArguments As Boolean = True
        ''' <summary>Gets or sets value indicating if generic arguments (type parameters) are shown</summary>
        ''' <value>True to show generic arguments  false to hide them</value>
        ''' <returns>Value indicating if generic arguments are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if generic arguments (type parameters) are shown.")> _
        Public Property ShowGenericArguments() As Boolean 'Localize: Description
            Get
                Return _ShowgenericArguments
            End Get
            Set(ByVal value As Boolean)
                _ShowgenericArguments = value
                'TODO: Apply change
            End Set
        End Property
#End Region

        ''' <summary>List of assemblies or any other objects listed at top-level of tree-view</summary>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Description("List of assemblies or any other objects listed at top-level of tree-view")> _
        Public ReadOnly Property Objects() As IList(Of Object) 'Localize: Description
            Get
                Return Assemblies
            End Get
        End Property
        ''' <summary>Gets value indicating if the <see cref="Objects"/> property needs to be serialized</summary>
        ''' <returns>True if the is any item in the <see cref="Objects"/> property</returns>
        Private Function ShouldSerializeObjects() As Boolean
            Return Assemblies.Count <> 0
        End Function
        ''' <summary>Resets the <see cref="Objects"/> property to it's initial state (empties it)</summary>
        Private Sub ResetObjects()
            Assemblies.Clear()
        End Sub

        Private Sub Assemblies_Added(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of Object), ByVal e As CollectionsT.GenericT.ListWithEvents(Of Object).ItemIndexEventArgs) Handles Assemblies.Added
            tvwObjects.Nodes.Insert(e.Index, GetNode(e.Item))
        End Sub

        Private Sub Assemblies_Cleared(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of Object), ByVal e As CollectionsT.GenericT.ListWithEvents(Of Object).ItemsEventArgs) Handles Assemblies.Cleared
            tvwObjects.Nodes.Clear()
        End Sub

        Private Sub Assemblies_ItemChanged(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of Object), ByVal e As CollectionsT.GenericT.ListWithEvents(Of Object).OldNewItemEvetArgs) Handles Assemblies.ItemChanged
            tvwObjects.Nodes(e.Index) = GetNode(e.Item)
        End Sub

        Private Sub Assemblies_Removed(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of Object), ByVal e As CollectionsT.GenericT.ListWithEvents(Of Object).ItemIndexEventArgs) Handles Assemblies.Removed
            tvwObjects.Nodes.RemoveAt(e.Index)
        End Sub

        ''' <summary>Converts any object to <see cref="TreeNode"/></summary>
        ''' <param name="Obj">Object to convert</param>
        ''' <returns><see cref="TreeNode"/> to represent <paramref name="Obj"/></returns>
        ''' <remarks>Note to inheritors: Newly created node must have <paramref name="Obj"/> as its <see cref="TreeNode.Tag"/>. If you call any of <see cref="ReflectionT.CodeImages.GetImage"/> methods that returns image with overlay the image is automatically added to <see cref="ImageList"/> used by <see cref="TreeView"/> and <see cref="ListView"/>. You can then set <see cref="TreeNode.ImageKey"/> and <see cref="TreeNode.SelectedImageKey"/> to key in form "{0:d}_{1:d}" (see <see cref="String.Format"/>) where 0 is numeric representation of <see cref="ReflectionT.Objects"/> and 1 is numeric representation of <see cref="ReflectionT.ObjectModifiers"/>. This key is produced by the <see cref="Key"/> function. If you want the node to be expandable, but you do not want to fill items right now, place only item without <see cref="TreeNode.Tag"/> into it.</remarks>
        ''' <exception cref="Exception">An error ocured during creating node (not thrown when <paramref name="Obj"/> is <see cref="Exception"/>). Note to inheritors: Do not throw any exception when <paramref name="Obj"/> is <see cref="Exception"/>!</exception>
        Protected Overridable Function GetNode(ByVal Obj As Object) As TreeNode
            Dim tn As New TreeNode
            Dim Expandable As Boolean = True
            If TypeOf Obj Is Assembly Then
                tn.Text = DirectCast(Obj, Assembly).ToString
                GetImage(CodeImages.Objects.Assembly, ObjectModifiers.None)
            ElseIf TypeOf Obj Is [Module] Then
                tn.Text = DirectCast(Obj, [Module]).Name
                GetImage(CodeImages.Objects.Module, ObjectModifiers.None)
            ElseIf TypeOf Obj Is NamespaceInfo Then
                tn.Text = DirectCast(Obj, NamespaceInfo).Name
                GetImage(CodeImages.Objects.Namespace, ObjectModifiers.None)
            ElseIf TypeOf Obj Is MemberInfo Then
                tn.Text = DirectCast(Obj, MemberInfo).Name
                DirectCast(Obj, MemberInfo).GetImage()
                If DirectCast(Obj, MemberInfo).IsPrivate Then tn.ForeColor = SystemColors.GrayText
                Expandable = _
                    TypeOf Obj Is Type OrElse _
                    ((TypeOf Obj Is PropertyInfo OrElse TypeOf Obj Is EventInfo) AndAlso ShowSpecialMembers) OrElse _
                    (TypeOf Obj Is MethodInfo AndAlso (DirectCast(Obj, MethodInfo).IsGenericMethod OrElse DirectCast(Obj, MethodInfo).IsGenericMethodDefinition) AndAlso ShowGenericArguments)
            ElseIf TypeOf Obj Is Exception Then
                tn.Text = DirectCast(Obj, Exception).Message
                GetImage(CodeImages.Objects.Error, ObjectModifiers.None)
                Expandable = False
            Else
                tn.Text = Obj.ToString
                GetImage(CodeImages.Objects.Question, ObjectModifiers.None)
                Expandable = False
            End If
            tn.ImageKey = LastRequestedKey
            tn.SelectedImageKey = LastRequestedKey
            tn.Tag = Obj
            If Expandable Then tn.Nodes.Add("please wait...") 'Localize: Please wait...
            Return tn
        End Function
        ''' <summary>Converts any object to <see cref="ListViewItem"/></summary>
        ''' <param name="obj">Object to converts</param>
        ''' <returns><see cref="ListViewItem"/> repersenting <paramref name="obj"/></returns>
        ''' <remarks>Note to inheritors: Newly created item must have <paramref name="Obj"/> as its <see cref="ListViewItem.Tag"/>. If you call any of <see cref="ReflectionT.CodeImages.GetImage"/> methods that returns image with overlay the image is automatically added to <see cref="ImageList"/> used by <see cref="TreeView"/> and <see cref="ListView"/>. You can then set <see cref="TreeNode.ImageKey"/> and <see cref="TreeNode.SelectedImageKey"/> to key in form "{0:d}_{1:d}" (see <see cref="String.Format"/>) where 0 is numeric representation of <see cref="ReflectionT.Objects"/> and 1 is numeric representation of <see cref="ReflectionT.ObjectModifiers"/>. This key is produced by the <see cref="Key"/> function. If you want the node to be expandable, but you do not want to fill items right now, place only item without <see cref="TreeNode.Tag"/> into it.</remarks>
        ''' <exception cref="Exception">An error ocured during creating node (not thrown when <paramref name="Obj"/> is <see cref="Exception"/>). Note to inheritors: Do not throw any exception when <paramref name="Obj"/> is <see cref="Exception"/>!</exception>
        Protected Overridable Function GetListItem(ByVal Obj As Object) As ListViewItem
            Dim li As New ListViewItem
            If TypeOf obj Is Attribute Then
                li.Text = obj.GetType.Name
                GetImage(If(obj.GetType.IsGenericType, ReflectionT.Objects.GenericAttributeClosed, ReflectionT.Objects.Attribute), ObjectModifiers.None)
            ElseIf TypeOf obj Is Exception Then
                li.Text = DirectCast(obj, Exception).Message
                GetImage(CodeImages.Objects.Error, ObjectModifiers.None)
            Else
                li.Text = obj.ToString
                GetImage(CodeImages.Objects.Question, ObjectModifiers.None)
            End If
            li.ImageKey = LastRequestedKey
            li.Tag = obj
            Return li
        End Function

        ''' <summary>Holds key of image that was last passed to <see cref="ImageRequested"/></summary>
        Private LastRequestedKey As String
        ''' <summary>Handles the <see cref="ReflectionT.ImageRequested"/> event</summary>
        ''' <param name="img">Image requaested</param>
        ''' <param name="ObjectType">Type of object for that request</param>
        ''' <param name="Modifiers">Object modifiers for that request</param>
        Private Sub ImageRequested(ByVal img As Image, ByVal ObjectType As Objects, ByVal Modifiers As ObjectModifiers)
            LastRequestedKey = Key(ObjectType, Modifiers)
            If Not imlImages.Images.ContainsKey(LastRequestedKey) Then imlImages.Images.Add(LastRequestedKey, img)
        End Sub
        ''' <summary>Converts object type and modifiers to string key</summary>
        ''' <param name="ObjectType">Type of object</param>
        ''' <param name="Modifiers">Ky modifiers</param>
        ''' <returns>String created using <see cref="String.Format"/>("{0:d}_{1:d}", <paramref name="ObjectType"/>, <paramref name="Modifiers"/>)</returns>
        Protected Function Key$(ByVal ObjectType As Objects, ByVal Modifiers As ObjectModifiers)
            Return String.Format("{0:d}_{1:d}", ObjectType, Modifiers)
        End Function

        Private Sub ObjectBrowser_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
            RemoveHandler ReflectionT.ImageRequested, AddressOf ImageRequested
        End Sub

        Private Sub tvwObjects_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwObjects.AfterSelect
            lvwMembers.Items.Clear()
            OnAfterSelect(e)
            OnSelectedItemChangedInternal(sender, e.Node, e.Node.Tag)
        End Sub

        ''' <summary>Called after selected item changes</summary>
        ''' <param name="Control">Control the item is member of (can be either <see cref="TreeView"/> or <see cref="ListView"/>).</param>
        ''' <param name="Item">Item itself (can be <see cref="TreeNode"/> or <see cref="ListViewItem"/>)</param>
        ''' <param name="ItemTag">Tag of item - selected object</param>
        Protected Overridable Sub OnSelectedItemChanged(ByVal Control As Control, ByVal Item As Object, ByVal ItemTag As Object)
            prgProperties.SelectedObject = ItemTag
            lblObjType.Text = ItemTag.GetType.Name
        End Sub
        Private _CurrentSelectedItem As Object
        Protected ReadOnly Property CurrentSelectedItem() As Object
            Get
                Return _CurrentSelectedItem
            End Get
        End Property
        ''' <summary>Called after selected item changes or list-control changes. Reduse unnecesary calls of <see cref="OnSelectedItemChanged"/></summary>
        ''' <param name="Control">Control the item is member of (can be either <see cref="TreeView"/> or <see cref="ListView"/>).</param>
        ''' <param name="Item">Item itself (can be <see cref="TreeNode"/> or <see cref="ListViewItem"/>)</param>
        ''' <param name="ItemTag">Tag of item - selected object</param>
        Private Sub OnSelectedItemChangedInternal(ByVal Control As Control, ByVal Item As Object, ByVal ItemTag As Object)
            If CurrentSelectedItem Is Item Then Exit Sub
            _CurrentSelectedItem = Item
            OnSelectedItemChanged(Control, Item, ItemTag)
        End Sub

        ''' <summary>selected node in <see cref="TreeView"/> changes</summary>
        ''' <param name="e">Arguments of the <see cref="TreeView.AfterSelect"/> event</param>
        ''' <remarks><see cref="ListView"/> is empty when this method is called</remarks>
        Protected Overridable Sub OnAfterSelect(ByVal e As TreeViewEventArgs)
            Try
                Dim Items = GetItems(e.Node.Tag)
                If Items IsNot Nothing Then
                    For Each Item In Items
                        Try
                            lvwMembers.Items.Add(GetListItem(Item))
                        Catch ex As Exception
                            lvwMembers.Items.Add(GetListItem(ex))
                        End Try
                    Next
                End If
            Catch ex As Exception
                lvwMembers.Items.Add(GetListItem(ex))
            End Try
        End Sub

        ''' <summary>Gets list of items to show in <see cref="ListView"/>. Called after node in <see cref="TreeView"/> is selected</summary>
        ''' <param name="obj"><see cref="TreeNode.Tag"/> of selected <see cref="TreeNode"/></param>
        ''' <returns>List of object to pas to <see cref="GetListItem"/> and show</returns>
        Protected Overridable Function GetItems(ByVal obj As Object) As IEnumerable(Of Object)
            Dim ret As New List(Of Object)
            If TypeOf obj Is ICustomAttributeProvider Then
                ret.AddRange(DirectCast(obj, ICustomAttributeProvider).GetCustomAttributes(ShowInheritedMembers))
            End If
            Return ret
        End Function

        Private Sub tvwObjects_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tvwObjects.BeforeExpand
            If e.Node.Nodes.Count = 1 AndAlso e.Node.Nodes(0).Tag Is Nothing Then
                e.Node.Nodes.Clear()
                OnBeforeExpand(e)
            End If
        End Sub
        ''' <summary>en node which contain only one sub-node with empty <see cref="TreeNode.Tag">Tag</see> is about to be expanded.</summary>
        ''' <param name="e">Arguments of <see cref="TreeView.BeforeExpand"/> event</param>
        ''' <remarks><para>The empty sub-node is removed from current node before this method is called.</para>
        ''' <para>The default implementation calls <see cref="GetChildren"/> and passes it's result to <see cref="GetNode"/> to obtain <see cref="TreeNode"/> which is added to node being expanded</para></remarks>
        Protected Overridable Sub OnBeforeExpand(ByVal e As System.Windows.Forms.TreeViewCancelEventArgs)
            Dim Children As IEnumerable(Of Object)
            Try
                Children = GetChildren(e.Node.Tag)
            Catch ex As Exception
                e.Node.Nodes.Add(GetNode(ex))
                Exit Sub
            End Try
            If Children IsNot Nothing Then
                For Each Child In Children
                    Try
                        e.Node.Nodes.Add(GetNode(Child))
                    Catch ex As Exception
                        e.Node.Nodes.Add(GetNode(ex))
                    End Try
                Next Child
            End If
        End Sub
        ''' <summary>This function is called by default implementation of <see cref="OnBeforeExpand"/> to obtain childrens of node being expanded.</summary>
        ''' <param name="obj"><see cref="TreeNode.Tag"/> of node being expanded</param>
        ''' <returns>Object to pass to <see cref="GetNode"/> in order to obtain node to add to current node. Can be null.</returns>
        ''' <remarks>Note to inheritors: Feel free to throw an exception from this function. Default implementation of <see cref="OnBeforeExpand"/> catches it and adds exception to tree instead of list of items. Another possibility is to catch exception and add it to list being returned. This way allows you to denote exception but continue enumeration other children.</remarks>
        ''' <exception cref="Exception">An error ocured while getting children</exception>
        Protected Overridable Function GetChildren(ByVal obj As Object) As IEnumerable(Of Object)
            Dim ret As New List(Of Object)
            If TypeOf obj Is Assembly Then 'Assembly
                ret.AddRange( _
                    (From m In DirectCast(obj, Assembly).GetModules Order By m.Name Ascending Select CObj(m)))
            ElseIf TypeOf obj Is [Module] Then 'Module
                'Namespaces
                'TODO: Type consideration
                ret.AddRange( _
                    From n In DirectCast(obj, [Module]).GetNamespaces( _
                        Function(t As Type) ShouldShowMember(False, False, t.IsNotPublic, t.IsPublic, False, False, , False)) _
                        Select n Order By n.Name Ascending)
                'No-namespace types
                ret.AddRange( _
                    From t In DirectCast(obj, [Module]).GetTypes(False) _
                    Where ShouldShowMember(False, False, t.IsNotPublic, t.IsPublic, False, False, , True) _
                    Select t _
                    Order By t.Name Ascending)
                'Global fields
                If ShowGlobalMembers AndAlso ShowFields Then
                    ret.AddRange( _
                        From f In DirectCast(obj, [Module]).GetFields() _
                        Where ShouldShowMember(f.IsPrivate, f.IsFamily, f.IsAssembly, f.IsPublic, f.IsFamilyAndAssembly, f.IsFamilyOrAssembly, f.IsStatic, True) _
                        Select f Order By f.Name Ascending)
                End If
                'Global methods
                If ShowGlobalMembers AndAlso ShowMethods Then
                    ret.AddRange( _
                        From m In DirectCast(obj, [Module]).GetMethods _
                        Where ShouldShowMember(m.IsPrivate, m.IsFamily, m.IsAssembly, m.IsPublic, m.IsFamilyAndAssembly, m.IsFamilyOrAssembly, m.IsStatic, True) _
                        Select m Order By m.Name Ascending)
                End If
            ElseIf TypeOf obj Is NamespaceInfo Then  'Namespace
                ret.AddRange(From t In DirectCast(obj, NamespaceInfo).GetTypes Where ShouldShowMember(False, False, t.IsNotPublic, t.IsPublic, False, False, , True) Order By t.Name Ascending)
            ElseIf TypeOf obj Is Type Then 'Type
                With DirectCast(obj, Type)
                    'Generic arguments
                    If ShowGenericArguments Then
                        ret.AddRange(.GetGenericArguments)
                    End If
                    'Nested types
                    If ShowNestedTypes Then
                        ret.AddRange( _
                            From st In .GetNestedTypes(BindingFlags.Public Or BindingFlags.NonPublic Or If(ShowInheritedMembers, BindingFlags.Default, BindingFlags.DeclaredOnly)) _
                            Where ShouldShowMember(st.IsNestedPrivate, st.IsNestedFamily, st.IsNestedAssembly, st.IsNestedPublic, st.IsNestedFamANDAssem, st.IsNestedFamORAssem, , True) _
                            Select st _
                            Order By st.Name Ascending)
                    End If
                    'CTors and initializers
                    If ShowCTors OrElse ShowInitializers Then
                        ret.AddRange( _
                            From c In .GetConstructors(BindingFlags.Public Or BindingFlags.NonPublic Or If(ShowCTors, BindingFlags.Instance, BindingFlags.Default) Or If(ShowInitializers, BindingFlags.Static, BindingFlags.Default)) _
                            Where ShouldShowMember(c.IsPrivate, c.IsFamily, c.IsAssembly, c.IsPublic, c.IsFamilyAndAssembly, c.IsFamilyOrAssembly, c.IsStatic, True) _
                            Select c Order By c.Name Ascending)
                    End If
                    'Properties
                    If ShowProperties Then
                        ret.AddRange( _
                            From p In .GetProperties(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.Instance) _
                            Where ShouldShowMember(p.IsPrivate, p.IsFamily, p.IsAssembly, p.IsPublic, p.IsFamilyAndAssembly, p.IsFamilyOrAssembly, p.IsStatic) _
                            Select p Order By p.Name Ascending)
                    End If
                    'Methods
                    If ShowMethods Then
                        ret.AddRange( _
                            From m In .GetMethods(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.Instance) _
                            Where ShouldShowMember(m.IsPrivate, m.IsFamily, m.IsAssembly, m.IsPublic, m.IsFamilyAndAssembly, m.IsFamilyOrAssembly, m.IsStatic) _
                            Select m Order By m.Name Ascending)
                    End If
                    'Events
                    If ShowEvents Then
                        ret.AddRange( _
                            From e In .GetEvents(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.Instance) _
                            Where ShouldShowMember(e.IsPrivate, e.IsFamily, e.IsAssembly, e.IsPublic, e.IsFamilyAndAssembly, e.IsFamilyOrAssembly, e.IsStatic) _
                            Select e Order By e.Name Ascending)
                    End If
                    'Fields
                    If ShowFields Then
                        ret.AddRange( _
                            From f In .GetFields(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.Instance) _
                            Where ShouldShowMember(f.IsPrivate, f.IsFamily, f.IsAssembly, f.IsPublic, f.IsFamilyAndAssembly, f.IsFamilyOrAssembly, f.IsStatic) _
                            Select f Order By f.Name Ascending)
                    End If
                End With
            ElseIf TypeOf obj Is PropertyInfo AndAlso ShowSpecialMembers Then 'Property
                With DirectCast(obj, PropertyInfo)
                    ret.AddRange( _
                        From a In .GetAccessors(True) _
                        Where ShouldShowMember(a.IsPrivate, a.IsFamily, a.IsAssembly, a.IsPublic, a.IsFamilyAndAssembly, a.IsFamilyOrAssembly, a.IsStatic) _
                        Select a Order By a.Name)
                End With
            ElseIf TypeOf obj Is EventInfo AndAlso ShowSpecialMembers Then 'Event
                With DirectCast(obj, EventInfo)
                    ret.AddRange( _
                        From a In .GetOtherMethods().Union(New MethodInfo() {.GetAddMethod, .GetRemoveMethod, .GetRaiseMethod}) _
                        Where a IsNot Nothing AndAlso ShouldShowMember(a.IsPrivate, a.IsFamily, a.IsAssembly, a.IsPublic, a.IsFamilyAndAssembly, a.IsFamilyOrAssembly, a.IsStatic) _
                        Select a Order By a.Name)
                End With
            ElseIf TypeOf obj Is MethodInfo AndAlso (DirectCast(obj, MethodInfo).IsGenericMethodDefinition OrElse DirectCast(obj, MethodInfo).IsGenericMethod) AndAlso ShowGenericArguments Then
                ret.AddRange(DirectCast(obj, MethodInfo).GetGenericArguments)
            End If
            Return ret
        End Function
        ''' <summary>Gets value indicating if member with given accesibility and static/instance behavior should be shown</summary>
        ''' <param name="Private">Member is private</param>
        ''' <param name="Family">Member as family (protected)</param>
        ''' <param name="Assembly">Member is assembly (internal, friend)</param>
        ''' <param name="Public">Member is public</param>
        ''' <param name="FamAndAssem">Member is family-and-assembyl</param>
        ''' <param name="FamOrAssem">Member is family-or-assembyl (protected firend)</param>
        ''' <param name="Static">member is static</param>
        ''' <param name="SkipStaticTest"><paramref name="Static"/> will not be tested</param>
        ''' <returns>True if member should be shown</returns>
        Protected Function ShouldShowMember(ByVal [Private] As Boolean, ByVal [Family] As Boolean, ByVal [Assembly] As Boolean, ByVal [Public] As Boolean, ByVal FamAndAssem As Boolean, ByVal FamOrAssem As Boolean, Optional ByVal [Static] As Boolean = True, Optional ByVal SkipStaticTest As Boolean = False) As Boolean
            If [Private] AndAlso Not ShowPrivateMembers Then Return False
            If [Family] AndAlso Not ShowProtectedMembers Then Return False
            If Assembly AndAlso Not ShowInternalMembers Then Return False
            If FamOrAssem AndAlso Not ShowProtectedMembers AndAlso Not ShowInternalMembers Then Return False
            If FamAndAssem AndAlso (Not ShowProtectedMembers OrElse Not ShowInternalMembers) Then Return False
            If SkipStaticTest Then Return True
            If ([Static] AndAlso Not ShowStaticMembers) OrElse (Not [Static] AndAlso Not ShowInstanceMembers) Then Return False
            Return True
        End Function

        Private Sub prgProperties_ControlAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles prgProperties.ControlAdded
            AddHandler e.Control.ControlAdded, AddressOf prgProperties_ControlAdded
            AddHandler e.Control.ControlRemoved, AddressOf prgProperties_ControlRemoved
            If TypeOf e.Control Is TextBoxBase Then
                DirectCast(e.Control, TextBoxBase).ReadOnly = True
            End If
        End Sub

        Private Sub prgProperties_ControlRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles prgProperties.ControlRemoved
            RemoveHandler e.Control.ControlAdded, AddressOf prgProperties_ControlAdded
        End Sub

        Private Sub lvwMembers_Enter(ByVal sender As ListView, ByVal e As System.EventArgs) Handles lvwMembers.Enter
            If Initializing Then Exit Sub
            OnSelectedItemChangedInternal(sender, sender.SelectedItems(0), sender.SelectedItems(0).Tag)
        End Sub

        Private Sub lvwMembers_SelectedIndexChanged(ByVal sender As ListView, ByVal e As System.EventArgs) Handles lvwMembers.SelectedIndexChanged
            OnSelectedItemChangedInternal(sender, sender.SelectedItems(0), sender.SelectedItems(0).Tag)
        End Sub

        Private Sub tvwObjects_Enter(ByVal sender As TreeView, ByVal e As System.EventArgs) Handles tvwObjects.Enter
            If Initializing Then Exit Sub
            OnSelectedItemChangedInternal(sender, sender.SelectedNode, sender.SelectedNode.Tag)
        End Sub
    End Class
End Namespace