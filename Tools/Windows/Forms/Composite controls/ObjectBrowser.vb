Imports Tools.CollectionsT.GenericT, Tools.ReflectionT
Imports System.Windows.Forms, System.Reflection
Imports System.Drawing, System.Linq, Tools.LinqT.EnumerableT
Imports Tools.ComponentModelT
#If Config <= Nightly Then
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
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowInheritedMembers As Boolean
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
                If value = ShowInheritedMembers Then Exit Property
                _ShowInheritedMembers = value
                tmiShowInheritedMembers.Checked = value
                OnShowChanged()
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ShowFlatNamespaces"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowFlatNamespaces As Boolean
        ''' <summary>Gets or sets value indicating if namespaces are show flat or hierachically</summary>
        ''' <value>True to show flat namespaces, false to show hierarchic namespaces</value>
        ''' <returns>Value indicating if namespaces are shown flat or hierarchic</returns>
        <DefaultValue(False)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        <Description("Gets or sets value indicating if namespaces are shown flat (true) or hierarchically (false).")> _
        Public Property ShowFlatNamespaces() As Boolean 'Localize: Description
            Get
                Return _ShowFlatNamespaces
            End Get
            Set(ByVal value As Boolean)
                If value = ShowFlatNamespaces Then Exit Property
                _ShowFlatNamespaces = value
                tmiShowflatnamespaces.Checked = value
                OnShowChanged()
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ShowNestedTypes"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowNestedTypes As Boolean = True
        ''' <summary>Gets or sets value indicating if nested types are shown</summary>
        ''' <value>True to show nested types, false to hide them</value>
        ''' <returns>Value indicating if nested types are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if nested types are shown.")> _
        Public Property ShowNestedTypes() As Boolean 'Localize: Description
            Get
                Return _ShowNestedTypes
            End Get
            Set(ByVal value As Boolean)
                If value = ShowNestedTypes Then Exit Property
                _ShowNestedTypes = value
                tmiShowNestedTypes.Checked = value
                OnShowChanged()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowPrivateMembers"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowPrivateMembers As Boolean = True
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
                If value = ShowPrivateMembers Then Exit Property
                _ShowPrivateMembers = value
                tmiShowPrivateMembers.Checked = value
                OnShowChanged()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowInternalMembers"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowInternalMembers As Boolean = True
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
                If value = ShowInternalMembers Then Exit Property
                _ShowInternalMembers = value
                tmiShowInternalMembers.Checked = value
                OnShowChanged()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowProtectedMembers"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowProtectedMembers As Boolean = True
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
                If value = ShowProtectedMembers Then Exit Property
                _ShowProtectedMembers = value
                tmiShowProtectedMembers.Checked = value
                OnShowChanged()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowinstanceMembers"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowInstanceMembers As Boolean = True
        ''' <summary>Gets or sets value indicating if instance members are shown</summary>
        ''' <value>True to show instance members, false to hide them</value>
        ''' <returns>Value indicating if instance members are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if instance members are shown.")> _
        Public Property ShowInstanceMembers() As Boolean 'Localize: Description
            Get
                Return _ShowInstanceMembers
            End Get
            Set(ByVal value As Boolean)
                If value = ShowInstanceMembers Then Exit Property
                _ShowInstanceMembers = value
                tmiShowInstanceMembers.Checked = value
                OnShowChanged()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowstaticMembers"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowStaticMembers As Boolean = True
        ''' <summary>Gets or sets value indicating if static members are shown</summary>
        ''' <value>True to show static members, false to hide them</value>
        ''' <returns>Value indicating if static members are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if static members are shown.")> _
        Public Property ShowStaticMembers() As Boolean 'Localize: Description
            Get
                Return _ShowStaticMembers
            End Get
            Set(ByVal value As Boolean)
                If value = ShowStaticMembers Then Exit Property
                _ShowStaticMembers = value
                tmiShowStaticMembers.Checked = value
                OnShowChanged()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowspecialMembers"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowSpecialMembers As Boolean = True
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
                If value = ShowSpecialMembers Then Exit Property
                _ShowSpecialMembers = value
                tmiShowSpecialMembers.Checked = value
                OnShowChanged()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowGlobalMembers"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowGlobalMembers As Boolean = True
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
                If value = ShowGlobalMembers Then Exit Property
                _ShowGlobalMembers = value
                tmiShowGlobalMembers.Checked = value
                OnShowChanged()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowProperties"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowProperties As Boolean = True
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
                If value = ShowProperties Then Exit Property
                _ShowProperties = value
                tmiShowProperties.Checked = value
                OnShowChanged()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowEvents"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowEvents As Boolean = True
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
                If value = ShowEvents Then Exit Property
                _ShowEvents = value
                tmiShowEvents.Checked = value
                OnShowChanged()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowMethods"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowMethods As Boolean = True
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
                If value = ShowMethods Then Exit Property
                _ShowMethods = value
                tmiShowMethods.Checked = value
                OnShowChanged()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowFields"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowFields As Boolean = True
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
                If value = ShowFields Then Exit Property
                _ShowFields = value
                tmiShowFields.Checked = value
                OnShowChanged()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowCTors"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowCTors As Boolean = True
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
                If value = ShowCTors Then Exit Property
                _ShowCTors = value
                tmiShowCTors.Checked = value
                OnShowChanged()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowInitializers"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowInitializers As Boolean = True
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
                If value = ShowInitializers Then Exit Property
                _ShowInitializers = value
                tmiShowInitializers.Checked = value
                OnShowChanged()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowgenericArguments"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowgenericArguments As Boolean = True
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
                If value = ShowGenericArguments Then Exit Property
                _ShowgenericArguments = value
                tmiShowGenericArguments.Checked = value
                OnShowChanged()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowReferences"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowReferences As Boolean = True
        ''' <summary>Gets or sets value indicating if references are shown</summary>
        ''' <value>True to show references false to hide them</value>
        ''' <returns>Value indicating if references are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if references are shown.")> _
        Public Property ShowReferences() As Boolean 'Localize: Description
            Get
                Return _ShowReferences
            End Get
            Set(ByVal value As Boolean)
                If value = ShowReferences Then Exit Property
                _ShowReferences = value
                tmiShowReferences.Checked = value
                OnShowChanged()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="ShowBaseTypes"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowBaseTypes As Boolean = True
        ''' <summary>Gets or sets value indicating if base types are shown</summary>
        ''' <value>True to show base types  false to hide them</value>
        ''' <returns>Value indicating if base types are shown</returns>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <Description("Gets or sets value indicating if base types are shown.")> _
        Public Property ShowBaseTypes() As Boolean 'Localize: Description
            Get
                Return _ShowBaseTypes
            End Get
            Set(ByVal value As Boolean)
                If value = ShowBaseTypes Then Exit Property
                _ShowBaseTypes = value
                tmiShowBaseTypes.Checked = value
                OnShowChanged()
            End Set
        End Property
        ''' <summary>Called after value of property that controls which members are displayed in <see cref="ListView"/> is changed.</summary>
        ''' <remarks>Those properties are:
        ''' <see cref="ShowCTors"/>, <see cref="ShowEvents"/>, <see cref="ShowFields"/>, <see cref="ShowgenericArguments"/>, <see cref="ShowGlobalMembers"/>, <see cref="ShowInheritedMembers"/>, <see cref="ShowInitializers"/>, <see cref="ShowInstanceMembers"/>, <see cref="ShowMethods"/>, <see cref="ShowInternalMembers"/>, <see cref="ShowNestedTypes"/>, <see cref="ShowPrivateMembers"/>, <see cref="ShowProperties"/>, <see cref="ShowProtectedMembers"/>, <see cref="ShowSpecialMembers"/>, <see cref="ShowStaticMembers"/>
        ''' </remarks>
        Protected Overridable Sub OnShowChanged()
            For Each Node As TreeNode In tvwObjects.Nodes
                If Node.IsExpanded Then
                    ActualizeNode(Node)
                ElseIf Node.Nodes.Count <> 0 Then
                    Node.Nodes.Clear()
                    Node.Nodes.Add(New TreeNode("Please wait...")) 'Localize: Please wait
                End If
            Next Node
        End Sub
        ''' <summary>Raised after property that controls which members are displayed in <see cref="ListView"/> was changed.</summary>
        ''' <remarks>See <seealso cref="OnShowChanged"/> for list of properties that causes this event to be raised.</remarks>
        <Description("Raised after property that controls which members are displayed in ListView was changed.")> _
        <Category(WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.PropertyChanged)> _
        Public Event ShowChanged As ControlEventHandler(Of ObjectBrowser, EventArgs) 'Localize: Description
        ''' <summary>Called recursivelly by <see cref="OnShowChanged"/> and itself in order to actualize content of each expanded node.</summary>
        ''' <param name="Node">Node to actualize content of</param>
        ''' <remarks>Note for inheritors: After actualizing content of this node, if there are any ünchanged nodes (that is your actualization logic is not delete all nodes and create them again) you should call this method on all the expanded nodes and replace content of all colapsed nodes by only tag-less node.</remarks>
        Protected Overridable Sub ActualizeNode(ByVal Node As TreeNode)
            Try
                Dim c = GetChildren(Node.Tag)
                Dim Content As List(Of Object) = If(TypeOf c Is List(Of Object), c, New List(Of Object)(c))
                Dim ToRemove As New List(Of TreeNode)
                For Each MyNode As TreeNode In Node.Nodes
                    If Content.Contains(MyNode.Tag) Then Content.Remove(MyNode.Tag) Else ToRemove.Add(MyNode)
                Next MyNode
                For Each RemNode As TreeNode In ToRemove
                    Node.Nodes.Remove(RemNode)
                Next RemNode
                For Each AddNode In Content
                    Try
                        Node.Nodes.Add(GetNode(AddNode))
                    Catch ex As Exception
                        Node.Nodes.Add(GetNode(ex))
                    End Try
                Next AddNode
            Catch ex As Exception
                Node.Nodes.Clear()
                Node.Nodes.Add(GetNode(ex))
            End Try
            For Each SubNode As TreeNode In Node.Nodes
                If SubNode.IsExpanded Then
                    ActualizeNode(SubNode)
                ElseIf SubNode.Nodes.Count <> 0 Then
                    SubNode.Nodes.Clear()
                    SubNode.Nodes.Add(New TreeNode("Please wait...")) 'Localize: Please wait
                End If
            Next
        End Sub
#End Region
        ''' <summary>Contains value of the <see cref="ShowShowMenu"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowShowMenu As Boolean = True
        ''' <summary>Gets or sets value indicating if the Show menu is shown</summary>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <DefaultValue(True)> _
        <Description("Gets or sets value indicating if Show menu is shown.")> _
        Public Property ShowShowMenu() As Boolean 'Localize:Description
            Get
                Return _ShowShowMenu
            End Get
            Set(ByVal value As Boolean)
                _ShowShowMenu = value
                tdbShow.Visible = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ShowToolbar"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ShowToolbar As Boolean = True
        ''' <summary>Gets or sets value indicating if the toolbar is shown</summary>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <DefaultValue(True)> _
        <Description("Gets or sets value indicating if the toolbar is shown.")> _
        Public Property ShowToolbar() As Boolean 'Localize:Description
            Get
                Return _ShowToolbar
            End Get
            Set(ByVal value As Boolean)
                _ShowToolbar = value
                tosMenu.Visible = value
            End Set
        End Property

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
                tn.Text = SignatureProvider.GetSignature(DirectCast(Obj, Assembly), SignatureFlags.ShortNameOnly)
                GetImage(CodeImages.Objects.Assembly, ObjectModifiers.None)
            ElseIf TypeOf Obj Is [Module] Then
                tn.Text = SignatureProvider.GetSignature(DirectCast(Obj, [Module]), SignatureFlags.ShortNameOnly)
                GetImage(CodeImages.Objects.Module, ObjectModifiers.None)
            ElseIf TypeOf Obj Is NamespaceInfo Then
                tn.Text = SignatureProvider.GetSignature(DirectCast(Obj, NamespaceInfo), If(ShowFlatNamespaces, SignatureFlags.LongName, SignatureFlags.ShortNameOnly))
                GetImage(CodeImages.Objects.Namespace, ObjectModifiers.None)
            ElseIf TypeOf Obj Is MemberInfo Then
                tn.Text = SignatureProvider.GetSignature(DirectCast(Obj, MemberInfo), SignatureFlags.Short)
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
            ElseIf TypeOf Obj Is KeyValuePair(Of String, Object) Then
                With DirectCast(Obj, KeyValuePair(Of String, Object))
                    Select Case .Key
                        Case kpBaseType
                            With DirectCast(.Value, Type)
                                tn.Text = SignatureProvider.GetSignature(.self, SignatureFlags.ShortNameOnly)
                                .GetImage()
                            End With
                        Case kpBaseTypes
                            tn.Text = "Base Types" 'Localize:Base types
                            GetImage(CodeImages.Objects.BackwardReference, ObjectModifiers.None)
                        Case kpReference
                            With DirectCast(.Value, AssemblyName)
                                tn.Text = SignatureProvider.GetSignature(.self, SignatureFlags.ShortNameOnly)
                                GetImage(CodeImages.Objects.Assembly, ObjectModifiers.Shortcut)
                                Expandable = False
                            End With
                        Case kpReferences
                            tn.Text = "References" 'Localize references
                            GetImage(CodeImages.Objects.ForwardReference, ObjectModifiers.None)
                        Case Else
                            tn.Text = .Key
                            GetImage(CodeImages.Objects.Question, ObjectModifiers.None)
                            Expandable = False
                    End Select
                End With
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
            If TypeOf Obj Is Attribute Then
                li.Text = Obj.GetType.Name
                GetImage(If(Obj.GetType.IsGenericType, ReflectionT.Objects.GenericAttributeClosed, ReflectionT.Objects.Attribute), ObjectModifiers.None)
            ElseIf TypeOf Obj Is Exception Then
                li.Text = DirectCast(Obj, Exception).Message
                GetImage(CodeImages.Objects.Error, ObjectModifiers.None)
            Else
                li.Text = Obj.ToString
                GetImage(CodeImages.Objects.Question, ObjectModifiers.None)
            End If
            li.ImageKey = LastRequestedKey
            li.Tag = Obj
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
            prgProperties.SelectedObject = New ReadOnlyObject(ItemTag)
            lblObjType.Text = ItemTag.GetType.Name
        End Sub
        ''' <summary>Contains value oft teh <see cref="CurrentSelectedItem"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _CurrentSelectedItem As Object
        ''' <summary>Contains item that is currently selected</summary>
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
            GetShortDesc(e.Node.Tag)
        End Sub

        ''' <summary>Assigns short description of object selected in <see cref="tvwObjects"/> to <see cref="rtbShort"/></summary>
        ''' <param name="Obj">Object to get description of (it is <see cref="TreeNode.Tag"/> of selected <see cref="TreeNode"/>)</param>
        Protected Overridable Sub GetShortDesc(ByVal Obj As Object)
            If TypeOf Obj Is Assembly Then
                rtbShort.Text = SignatureProvider.GetSignature(DirectCast(Obj, Assembly), SignatureFlags.ObjectType Or SignatureFlags.LongName Or SignatureFlags.AllAttributes)
            ElseIf TypeOf Obj Is [Module] Then
                rtbShort.Text = SignatureProvider.GetSignature(DirectCast(Obj, [Module]), SignatureFlags.ObjectType Or SignatureFlags.LongName Or SignatureFlags.AllAttributes)
            ElseIf TypeOf Obj Is NamespaceInfo Then
                rtbShort.Text = SignatureProvider.GetSignature(DirectCast(Obj, NamespaceInfo), SignatureFlags.ObjectType Or SignatureFlags.LongName)
            ElseIf TypeOf Obj Is MemberInfo Then
                rtbShort.Text = SignatureProvider.GetSignature(DirectCast(Obj, MemberInfo), SignatureFlags.Full)
            ElseIf TypeOf Obj Is Exception Then
                rtbShort.Text = DirectCast(Obj, Exception).Message
            ElseIf TypeOf Obj Is KeyValuePair(Of String, Object) Then
                With DirectCast(Obj, KeyValuePair(Of String, Object))
                    Select Case .Key
                        Case kpBaseType, kpReference : GetShortDesc(.Value)
                        Case kpBaseTypes : rtbShort.Text = "Base types" 'Localize:Base types
                        Case kpReferences : rtbShort.Text = "References" 'Localize: References
                    End Select
                End With
            Else
                rtbShort.Text = Obj.ToString
            End If
        End Sub

        Private SignatureProvider As ISignatureProvider = New VisualBasicSignatureProvider

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
        ''' <summary>Code for node representing all the base types</summary>
        Private Const kpBaseTypes$ = "Base types"
        ''' <summary>Code for node representing all the references</summary>
        Private Const kpReferences$ = "References"
        ''' <summary>Code for node representing single base type</summary>
        Private Const kpBaseType$ = "Base type"
        ''' <summary>Code for node representing single referenced assembly</summary>
        Private Const kpReference$ = "Reference"
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
                If ShowReferences Then
                    ret.Add(New KeyValuePair(Of String, Object)(kpReferences, obj))
                End If
            ElseIf TypeOf obj Is [Module] Then 'Module
                'Namespaces
                ret.AddRange( _
                    From n In DirectCast(obj, [Module]).GetNamespaces( _
                        Function(t As Type) ShouldShowMember(False, False, t.IsNotPublic, t.IsPublic, False, False, , True), , ShowFlatNamespaces) _
                        Order By n.Name Ascending Select CObj(n))
                'No-namespace types
                ret.AddRange( _
                    From t In DirectCast(obj, [Module]).GetTypes(False) _
                    Where ShouldShowMember(False, False, t.IsNotPublic, t.IsPublic, False, False, , True) _
                    Order By t.Name Ascending Select CObj(t))
                'Global fields
                If ShowGlobalMembers AndAlso ShowFields Then
                    ret.AddRange( _
                        From f In DirectCast(obj, [Module]).GetFields() _
                        Where ShouldShowMember(f.IsPrivate, f.IsFamily, f.IsAssembly, f.IsPublic, f.IsFamilyAndAssembly, f.IsFamilyOrAssembly, f.IsStatic, True) _
                        Order By f.Name Ascending Select CObj(f))
                End If
                'Global methods
                If ShowGlobalMembers AndAlso ShowMethods Then
                    ret.AddRange( _
                        From m In DirectCast(obj, [Module]).GetMethods _
                        Where ShouldShowMember(m.IsPrivate, m.IsFamily, m.IsAssembly, m.IsPublic, m.IsFamilyAndAssembly, m.IsFamilyOrAssembly, m.IsStatic, True) _
                        Order By m.Name Ascending Select CObj(m))
                End If
            ElseIf TypeOf obj Is NamespaceInfo Then 'Namespace
                If Not ShowFlatNamespaces Then _
                    ret.AddRange(From n In DirectCast(obj, NamespaceInfo).GetNamespaces(Function(t As Type) ShouldShowMember(False, False, t.IsNotPublic, t.IsPublic, False, False, , True)) Order By n.ShortName Ascending Select CObj(n))
                ret.AddRange(From t In DirectCast(obj, NamespaceInfo).GetTypes Where ShouldShowMember(False, False, t.IsNotPublic, t.IsPublic, False, False, , True) Order By t.Name Ascending Select CObj(t))
            ElseIf TypeOf obj Is Type Then 'Type
                With DirectCast(obj, Type)
                    'Base types
                    If ShowBaseTypes Then
                        ret.Add(New KeyValuePair(Of String, Object)(kpBaseTypes, obj))
                    End If
                    'Generic arguments
                    If ShowGenericArguments Then
                        ret.AddRange(From ga In .GetGenericArguments Select CObj(ga))
                    End If
                    'Nested types
                    If ShowNestedTypes Then
                        ret.AddRange( _
                            From st In .GetNestedTypes(BindingFlags.Public Or BindingFlags.NonPublic Or If(ShowInheritedMembers, BindingFlags.Default, BindingFlags.DeclaredOnly) Or If(ShowInheritedMembers, BindingFlags.Default, BindingFlags.DeclaredOnly)) _
                            Where ShouldShowMember(st.IsNestedPrivate, st.IsNestedFamily, st.IsNestedAssembly, st.IsNestedPublic, st.IsNestedFamANDAssem, st.IsNestedFamORAssem, , True) _
                            Order By st.Name Ascending Select CObj(st))
                    End If
                    'CTors and initializers
                    If ShowCTors OrElse ShowInitializers Then
                        ret.AddRange( _
                            From c In .GetConstructors(BindingFlags.Public Or BindingFlags.NonPublic Or If(ShowCTors, BindingFlags.Instance, BindingFlags.Default) Or If(ShowInitializers, BindingFlags.Static, BindingFlags.Default) Or If(ShowInheritedMembers, BindingFlags.Default, BindingFlags.DeclaredOnly)) _
                            Where ShouldShowMember(c.IsPrivate, c.IsFamily, c.IsAssembly, c.IsPublic, c.IsFamilyAndAssembly, c.IsFamilyOrAssembly, c.IsStatic, True) _
                            Order By c.Name Ascending Select CObj(c))
                    End If
                    'Properties
                    If ShowProperties Then
                        ret.AddRange( _
                            From p In .GetProperties(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.Instance Or If(ShowInheritedMembers, BindingFlags.Default, BindingFlags.DeclaredOnly)) _
                            Where ShouldShowMember(p.IsPrivate, p.IsFamily, p.IsAssembly, p.IsPublic, p.IsFamilyAndAssembly, p.IsFamilyOrAssembly, p.IsStatic) _
                            Order By p.Name Ascending Select CObj(p))
                    End If
                    'Methods
                    If ShowMethods Then
                        ret.AddRange( _
                            From m In .GetMethods(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.Instance Or If(ShowInheritedMembers, BindingFlags.Default, BindingFlags.DeclaredOnly)) _
                            Where ShouldShowMember(m.IsPrivate, m.IsFamily, m.IsAssembly, m.IsPublic, m.IsFamilyAndAssembly, m.IsFamilyOrAssembly, m.IsStatic) AndAlso m.GetEvent Is Nothing AndAlso m.GetProperty Is Nothing _
                            Order By m.Name Ascending Select CObj(m))
                    End If
                    'Events
                    If ShowEvents Then
                        ret.AddRange( _
                            From e In .GetEvents(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.Instance Or If(ShowInheritedMembers, BindingFlags.Default, BindingFlags.DeclaredOnly)) _
                            Where ShouldShowMember(e.IsPrivate, e.IsFamily, e.IsAssembly, e.IsPublic, e.IsFamilyAndAssembly, e.IsFamilyOrAssembly, e.IsStatic) _
                            Order By e.Name Ascending Select CObj(e))
                    End If
                    'Fields
                    If ShowFields Then
                        ret.AddRange( _
                            From f In .GetFields(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.Instance Or If(ShowInheritedMembers, BindingFlags.Default, BindingFlags.DeclaredOnly)) _
                            Where ShouldShowMember(f.IsPrivate, f.IsFamily, f.IsAssembly, f.IsPublic, f.IsFamilyAndAssembly, f.IsFamilyOrAssembly, f.IsStatic) _
                             Order By f.Name Ascending Select CObj(f))
                    End If
                End With
            ElseIf TypeOf obj Is PropertyInfo AndAlso ShowSpecialMembers Then 'Property
                With DirectCast(obj, PropertyInfo)
                    ret.AddRange( _
                        From a In .GetAccessors(True) _
                        Where ShouldShowMember(a.IsPrivate, a.IsFamily, a.IsAssembly, a.IsPublic, a.IsFamilyAndAssembly, a.IsFamilyOrAssembly, a.IsStatic) _
                         Order By a.Name Select CObj(a))
                End With
            ElseIf TypeOf obj Is EventInfo AndAlso ShowSpecialMembers Then 'Event
                With DirectCast(obj, EventInfo)
                    ret.AddRange( _
                        From a In .GetOtherMethods().Union(New MethodInfo() {.GetAddMethod, .GetRemoveMethod, .GetRaiseMethod}) _
                        Where a IsNot Nothing AndAlso ShouldShowMember(a.IsPrivate, a.IsFamily, a.IsAssembly, a.IsPublic, a.IsFamilyAndAssembly, a.IsFamilyOrAssembly, a.IsStatic) _
                         Order By a.Name Select CObj(a))
                End With
            ElseIf TypeOf obj Is MethodInfo AndAlso (DirectCast(obj, MethodInfo).IsGenericMethodDefinition OrElse DirectCast(obj, MethodInfo).IsGenericMethod) AndAlso ShowGenericArguments Then 'Generic method
                ret.AddRange(From ga In DirectCast(obj, MethodInfo).GetGenericArguments Select CObj(ga))
            ElseIf TypeOf obj Is Type Then 'Type
                If ShowBaseTypes Then ret.Add(New KeyValuePair(Of String, Object)(kpBaseType, obj))
            ElseIf TypeOf obj Is KeyValuePair(Of String, Object) Then 'Special nodes
                With DirectCast(obj, KeyValuePair(Of String, Object))
                    Select Case .Key
                        Case kpBaseTypes, kpBaseType  'Base types
                            With DirectCast(.Value, Type)
                                If .BaseType IsNot Nothing Then ret.Add(New KeyValuePair(Of String, Object)(kpBaseType, .BaseType))
                                ret.AddRange(From ii In .GetImplementedInterfaces Select CObj(New KeyValuePair(Of String, Object))(kpBaseType, ii))
                            End With
                        Case kpReferences 'References
                            With DirectCast(.Value, Assembly)
                                ret.AddRange( _
                                    From r In .GetReferencedAssemblies() _
                                    Order By r.ToString Select CObj(New KeyValuePair(Of String, Object)(kpReference, r)))
                            End With
                    End Select
                End With
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

        'Private Sub prgProperties_ControlAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ControlEventArgs) 'Handles prgProperties.ControlAdded
        '    AddHandler e.Control.ControlAdded, AddressOf prgProperties_ControlAdded
        '    AddHandler e.Control.ControlRemoved, AddressOf prgProperties_ControlRemoved
        '    If TypeOf e.Control Is TextBoxBase Then
        '        DirectCast(e.Control, TextBoxBase).ReadOnly = True
        '    ElseIf TypeOf e.Control Is Button Then
        '        e.Control.Enabled = False
        '    End If
        '    If sender IsNot Nothing Then Debug.Print("{0}{1}{2}", e.Control.GetType.Name, vbTab, e.Control.Name) 'TODO:Remove
        'End Sub
        '''' <summary>For all controls in given control including given control itself recursively calls <see cref="prgProperties_ControlAdded"/></summary>
        '''' <param name="control">Root control</param>
        'Private Sub walkcontrol(ByVal control As Control)
        '    Me.prgProperties_ControlAdded(Nothing, New ControlEventArgs(control))
        '    Debug.Print("{0}{1}{2}", control.GetType.Name, vbTab, control.Name) 'TODO:Remove
        '    For Each c2 In control.Controls
        '        walkcontrol(c2)
        '    Next
        'End Sub

        'Private Sub prgProperties_ControlRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles prgProperties.ControlRemoved
        '    RemoveHandler e.Control.ControlAdded, AddressOf prgProperties_ControlAdded
        'End Sub

        Private Sub lvwMembers_Enter(ByVal sender As ListView, ByVal e As System.EventArgs) Handles lvwMembers.Enter
            If Initializing Then Exit Sub
            If sender.SelectedItems.Count = 0 Then Exit Sub
            OnSelectedItemChangedInternal(sender, sender.SelectedItems(0), sender.SelectedItems(0).Tag)
        End Sub

        Private Sub lvwMembers_SelectedIndexChanged(ByVal sender As ListView, ByVal e As System.EventArgs) Handles lvwMembers.SelectedIndexChanged
            If sender.SelectedItems.Count = 0 Then Exit Sub
            OnSelectedItemChangedInternal(sender, sender.SelectedItems(0), sender.SelectedItems(0).Tag)
        End Sub

        Private Sub tvwObjects_Enter(ByVal sender As TreeView, ByVal e As System.EventArgs) Handles tvwObjects.Enter
            If Initializing Then Exit Sub
            If sender.SelectedNode Is Nothing Then Exit Sub
            OnSelectedItemChangedInternal(sender, sender.SelectedNode, sender.SelectedNode.Tag)
        End Sub
#Region "Show..._CheckedChanged"
        Private Sub tmiShowCTors_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowCTors.CheckedChanged
            Me.ShowCTors = sender.Checked
        End Sub
        Private Sub tmiShowEvents_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowEvents.CheckedChanged
            Me.ShowEvents = sender.Checked
        End Sub
        Private Sub tmiShowFields_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowFields.CheckedChanged
            Me.ShowFields = sender.Checked
        End Sub
        Private Sub tmiShowGenericArguments_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowGenericArguments.CheckedChanged
            Me.ShowGenericArguments = sender.Checked
        End Sub
        Private Sub tmiShowGlobalMembers_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowGlobalMembers.CheckedChanged
            Me.ShowGlobalMembers = sender.Checked
        End Sub
        Private Sub tmiShowInheritedmembers_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowInheritedMembers.CheckedChanged
            Me.ShowInheritedMembers = sender.Checked
        End Sub
        Private Sub tmiShowInitializers_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowInitializers.CheckedChanged
            Me.ShowInitializers = sender.Checked
        End Sub
        Private Sub tmiShowInstanceMembers_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowInstanceMembers.CheckedChanged
            Me.ShowInstanceMembers = sender.Checked
        End Sub
        Private Sub tmiShowInternalMembers_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowInternalMembers.CheckedChanged
            Me.ShowInternalMembers = sender.Checked
        End Sub
        Private Sub tmiShowMethods_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowMethods.CheckedChanged
            Me.ShowMethods = sender.Checked
        End Sub
        Private Sub tmiShowNestedtypes_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowNestedTypes.CheckedChanged
            Me.ShowNestedTypes = sender.Checked
        End Sub
        Private Sub tmiShowPrivateMembers_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowPrivateMembers.CheckedChanged
            Me.ShowPrivateMembers = sender.Checked
        End Sub
        Private Sub tmiShowProperties_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowProperties.CheckedChanged
            Me.ShowProperties = sender.Checked
        End Sub
        Private Sub tmiShowProtectedMemebers_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowProtectedMembers.CheckedChanged
            Me.ShowProtectedMembers = sender.Checked
        End Sub
        Private Sub tmiShowSpecialMembers_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowSpecialMembers.CheckedChanged
            Me.ShowSpecialMembers = sender.Checked
        End Sub
        Private Sub tmiShowStaticMembers_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowStaticMembers.CheckedChanged
            Me.ShowStaticMembers = sender.Checked
        End Sub
        Private Sub tmiShowReferences_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowReferences.CheckedChanged
            Me.ShowReferences = sender.Checked
        End Sub
        Private Sub tmiShowBaseTypes_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowBaseTypes.CheckedChanged
            Me.ShowBaseTypes = sender.Checked
        End Sub
        Private Sub tmiShowFlatNamespaces_CheckedChanged(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles tmiShowFlatNamespaces.CheckedChanged
            Me.ShowFlatNamespaces = sender.Checked
        End Sub
#End Region
    End Class
End Namespace
#End If