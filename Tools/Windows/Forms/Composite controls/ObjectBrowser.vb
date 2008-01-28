Imports Tools.CollectionsT.GenericT, Tools.ReflectionT
Imports System.Windows.Forms, System.Reflection
Imports System.Drawing
Imports Tools.ComponentModelT

Namespace WindowsT.FormsT
    'ASAP:
    ''' <summary>Control for browsing .NET assemblies</summary>
    Public Class ObjectBrowser
        ''' <summary>Contains value of the <see cref="Objects"/> property</summary>
        ''' <remarks>Can be any object</remarks>
        Private WithEvents Assemblies As New ListWithEvents(Of Object)
        ''' <summary>CTor</summary>
        Public Sub New()
            Assemblies.AllowAddCancelableEventsHandlers = False
            AddHandler ReflectionT.ImageRequested, AddressOf ImageRequested
            InitializeComponent()
        End Sub
        Private _ShowInheritedMembers As Boolean
        <DefaultValue(False)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        Public Property ShowInheritedMembers() As Boolean
            Get
                Return _ShowInheritedMembers
            End Get
            Set(ByVal value As Boolean)
                _ShowInheritedMembers = value
                'TODO: Apply change
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

        End Sub

        Private Sub tvwObjects_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tvwObjects.BeforeExpand
            If e.Node.Nodes.Count = 1 AndAlso e.Node.Nodes(0).Tag Is Nothing Then
                e.Node.Nodes.Clear()
                OnBeforeExpand(e)
            End If
        End Sub
        ''' <summary>en node which contain only one sub-node with empty <see cref="TreeNode.Tag">Tag</see> is about to be expanded.</summary>
        ''' <param name="e">Arguments of <see cref="TreeView.BeforeExpand"/> event</param>
        ''' <remarks>The empty sub-node is removed from current node before this method is called</remarks>
        Protected Overridable Sub OnBeforeExpand(ByVal e As System.Windows.Forms.TreeViewCancelEventArgs)
            If TypeOf e.Node.Tag Is Assembly Then
                For Each [Module] In DirectCast(e.Node.Tag, Assembly).GetModules
                    e.Node.Nodes.Add(GetNode([Module]))
                Next [Module]
            ElseIf TypeOf e.Node.Tag Is [Module] Then
                For Each [Namespace] In DirectCast(e.Node.Tag, [Module]).GetNamespaces
                    e.Node.Nodes.Add(GetNode([Namespace]))
                Next [Namespace]
                For Each Type In DirectCast(e.Node.Tag, [Module]).GetTypes(False)
                    e.Node.Nodes.Add(GetNode(Type))
                Next Type
                For Each Field In DirectCast(e.Node.Tag, [Module]).GetFields
                    e.Node.Nodes.Add(GetNode(Field))
                Next Field
                For Each Method In DirectCast(e.Node.Tag, [Module]).GetMethods
                    e.Node.Nodes.Add(GetNode(Method))
                Next Method
            ElseIf TypeOf e.Node.Tag Is NamespaceInfo Then
                For Each Type As Type In DirectCast(e.Node.Tag, NamespaceInfo).GetTypes
                    e.Node.Nodes.Add(GetNode(Type))
                Next Type
            ElseIf TypeOf e.Node.Tag Is Type Then
                With DirectCast(e.Node.Tag, Type)
                    For Each garg In .GetGenericArguments
                        e.Node.Nodes.Add(GetNode(garg))
                    Next
                    For Each SubType In .GetNestedTypes(BindingFlags.Public Or BindingFlags.NonPublic Or If(ShowInheritedMembers, BindingFlags.Default, BindingFlags.DeclaredOnly))
                        e.Node.Nodes.Add(GetNode(SubType))
                    Next SubType
                    For Each CTor In .GetConstructors(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.Static Or If(ShowInheritedMembers, BindingFlags.Default, BindingFlags.DeclaredOnly))
                        e.Node.Nodes.Add(GetNode(CTor))
                    Next CTor
                    For Each prp In .GetProperties(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.Static Or If(ShowInheritedMembers, BindingFlags.Default, BindingFlags.DeclaredOnly))
                        e.Node.Nodes.Add(GetNode(prp))
                    Next prp
                    For Each method In .GetMethods(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.Static Or If(ShowInheritedMembers, BindingFlags.Default, BindingFlags.DeclaredOnly))
                        e.Node.Nodes.Add(GetNode(method))
                    Next method
                    For Each ev In .GetEvents(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.Static Or If(ShowInheritedMembers, BindingFlags.Default, BindingFlags.DeclaredOnly))
                        e.Node.Nodes.Add(GetNode(ev))
                    Next ev
                    For Each field In .GetFields(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.Static Or If(ShowInheritedMembers, BindingFlags.Default, BindingFlags.DeclaredOnly))
                        e.Node.Nodes.Add(GetNode(field))
                    Next field
                End With
            ElseIf TypeOf e.Node.Tag Is PropertyInfo Then
                With DirectCast(e.Node.Tag, PropertyInfo)
                    For Each method In .GetAccessors(True)
                        e.Node.Nodes.Add(GetNode(method))
                    Next method
                End With
            ElseIf TypeOf e.Node.Tag Is EventInfo Then
                With DirectCast(e.Node.Tag, EventInfo)
                    Dim Methods As New List(Of MethodInfo)
                    Methods.Add(.GetAddMethod(True))
                    Methods.Add(.GetRemoveMethod(True))
                    Methods.Add(.GetRaiseMethod(True))
                    Methods.AddRange(.GetOtherMethods(True))
                    For Each method In Methods
                        If method IsNot Nothing Then e.Node.Nodes.Add(GetNode(method))
                    Next method
                End With
            End If
        End Sub
    End Class
End Namespace