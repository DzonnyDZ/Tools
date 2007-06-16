Imports Tools.CollectionsT.GenericT, Tools.ComponentModelT
Namespace DataStructuresT.GenericT
#If Config <= Nightly Then 'Stage:Nightly
    ''' <summary>Represents tree or sub-tree</summary>
    <Author("Ðonny", eMail:="dzonny@dzonny.cz", WWW:="http://dzonny.cz")> _
    <Version(1, 0, GetType(Tree(Of DBNull)), LastChMMDDYYYY:="06/16/2007")> _
    <StandAloneTool(FirstVerMMDDYYYY:="05/19/2007")> _
    <DebuggerDisplay("{ToString}")> _
    Public Class Tree(Of T) : Implements ICloneable(Of Tree(Of T))
#Region "CTors"
        ''' <summary>CTor - an empty tree with no value</summary>
        Public Sub New()
            Me._Nodes.AllowAddCancelableEventsHandlers = False
        End Sub
        ''' <summary>Copy CTor - clones instance of tree</summary>
        ''' <param name="a">Instance to clone</param>
        Public Sub New(ByVal a As Tree(Of T))
            Me.New()
            Me.Value = a.Value
            For Each itm As Tree(Of T) In a.Nodes
                Me.Nodes.Add(itm.Clone)
            Next itm
        End Sub
        ''' <summary>CTor - from value and optionally values of nodes</summary>
        ''' <param name="value">value for root</param>
        ''' <param name="Nodes">values for nodes</param>
        Public Sub New(ByVal value As T, Optional ByVal Nodes As IEnumerable(Of T) = Nothing)
            Me.New()
            Me.Value = value
            If Nodes IsNot Nothing Then
                For Each node As T In Nodes
                    Me.Nodes.Add(New Tree(Of T)(node))
                Next node
            End If
        End Sub
        ''' <summary>CTor - from value and nodes</summary>
        ''' <param name="value">value for root</param>
        ''' <param name="Nodes">node to fill <see cref="Nodes"/> with</param>
        Public Sub New(ByVal value As T, ByVal Nodes As IEnumerable(Of Tree(Of T)))
            Me.New()
            Me.Value = value
            If Nodes IsNot Nothing Then _
                Me._Nodes.AddRange(Nodes)
        End Sub
#End Region
        ''' <summary>Contains value of the <see cref="Value"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Value As T
        ''' <summary>Value of this node</summary>
        Public Overridable Property Value() As T
            Get
                Return _Value
            End Get
            Set(ByVal value As T)
                _Value = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Nodes"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private WithEvents _Nodes As New ListWithEvents(Of Tree(Of T))(True, True)
        ''' <summary>Sub-trees of this tree</summary>
        ''' <exception cref="NullReferenceException">Is thrown by <see cref="IList(Of T).Add"/> or <see cref="IList(Of T).Item"/>'s setter of <see cref="Nodes"/> when null is passed there.</exception>
        ''' <exception cref="OperationCanceledException">Is thrown by <see cref="IList(Of T).Add"/> or <see cref="IList(Of T).Item"/>'s setter of <see cref="Nodes"/> when: Attempting to pass node which's <see cref="Parent"/> is already set to non-null value -or- attempting to pass node which is <see cref="Root"/> of current tree.</exception>
        ''' <remarks>See also <seealso cref="OnAdding"/>, <seealso cref="OnItemChanging"/></remarks>
        Public ReadOnly Property Nodes() As IList(Of Tree(Of T))
            Get
                Return _Nodes
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="Parent"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Parent As Tree(Of T)
        ''' <summary>Node this node is placed in</summary>
        ''' <returns>parent node of this node or null when this node is root</returns>
        ''' <value>New parent node of this node. Setting root to another value causes moving current node and whole its sub-tree into node specified in <paramref name="value"/>, so it is no longer present in old parent</value>
        Public Overridable Property Parent() As Tree(Of T)
            <DebuggerStepThrough()> Get
                Return _Parent
            End Get
            Set(ByVal value As Tree(Of T))
                If value Is Parent Then Return
                Dim oldP As Tree(Of T) = Parent
                Dim OldI As Integer = Parent.Nodes.IndexOf(Me)
                If Parent IsNot Nothing Then Parent.Nodes.Remove(Me)
                Try
                    value.Nodes.Add(Me)
                Catch ex As Exception
                    If Not Parent.Nodes.Contains(Me) Then oldP.Nodes.Insert(OldI, Me)
                End Try
            End Set
        End Property
        ''' <summary>String representation of this instance</summary>
        Public Overrides Function ToString() As String
            Return String.Format("Node {0}, {1} items", Value, Nodes.Count)
        End Function
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        ''' <remarks>Use type-safe <see cref="Clone"/> instead</remarks>
        <EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use type safe Clone instead")> _
        Private Function Clone1() As Object Implements System.ICloneable.Clone
            Return Clone()
        End Function
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Public Overridable Function Clone() As Tree(Of T) Implements ICloneable(Of Tree(Of T)).Clone
            Return New Tree(Of T)(Me)
        End Function

#Region "Events"
        <EditorBrowsable(EditorBrowsableState.Never)> Private Sub Nodes_Clearing(ByVal sender As ListWithEvents(Of Tree(Of T)), ByVal e As CancelMessageEventArgs) Handles _Nodes.Clearing
            OnClearing(e)
        End Sub
        <EditorBrowsable(EditorBrowsableState.Never)> Private Sub Nodes_Cleared(ByVal sender As ListWithEvents(Of Tree(Of T)), ByVal e As ListWithEvents(Of Tree(Of T)).ItemsEventArgs) Handles _Nodes.Cleared
            OnCleared(e)
        End Sub
        ''' <exception cref="NullReferenceException">Attepmt to assign null reference</exception>
        <EditorBrowsable(EditorBrowsableState.Never)> Private Sub Nodes_ItemChanging(ByVal sender As ListWithEvents(Of Tree(Of T)), ByVal e As ListWithEvents(Of Tree(Of T)).CancelableItemIndexEventArgs) Handles _Nodes.ItemChanging
            OnItemChanging(e)
        End Sub
        <EditorBrowsable(EditorBrowsableState.Never)> Private Sub _Nodes_ItemChanged(ByVal sender As ListWithEvents(Of Tree(Of T)), ByVal e As ListWithEvents(Of Tree(Of T)).OldNewItemEvetArgs) Handles _Nodes.ItemChanged
            OnItemChanged(e)
        End Sub
        <EditorBrowsable(EditorBrowsableState.Never)> Private Sub Nodes_Added(ByVal sender As ListWithEvents(Of Tree(Of T)), ByVal e As ListWithEvents(Of Tree(Of T)).ItemIndexEventArgs) Handles _Nodes.Added
            OnAdded(e)
        End Sub
        ''' <exception cref="NullReferenceException">Attepmt to add null reference</exception>
        <EditorBrowsable(EditorBrowsableState.Never)> Private Sub Nodes_Adding(ByVal sender As ListWithEvents(Of Tree(Of T)), ByVal e As ListWithEvents(Of Tree(Of T)).CancelableItemIndexEventArgs) Handles _Nodes.Adding
            OnAdding(e)
        End Sub
        <EditorBrowsable(EditorBrowsableState.Never)> Private Sub Nodes_Removed(ByVal sender As ListWithEvents(Of Tree(Of T)), ByVal e As ListWithEvents(Of Tree(Of T)).ItemIndexEventArgs) Handles _Nodes.Removed
            OnRemoved(e)
        End Sub
        <EditorBrowsable(EditorBrowsableState.Never)> Private Sub Nodes_Removing(ByVal sender As ListWithEvents(Of Tree(Of T)), ByVal e As ListWithEvents(Of Tree(Of T)).CancelableItemIndexEventArgs) Handles _Nodes.Removing
            OnRemoving(e)
        End Sub
        ''' <summary>Called before adding item to <see cref="Nodes"/></summary>
        ''' <param name="e">Event parameters. You can use it to cancel the operation.</param>
        ''' <exception cref="NullReferenceException">Attepmt to add null reference</exception>
        ''' <remarks>When node that is being added has already parent set or this node is root of current tree then <see cref="ListWithEvents(Of Tree(Of T)).CancelableItemIndexEventArgs.Cancel"/> is set to true, which causes <see cref="OperationCanceledException"/> to be thrown by <see cref="ListWithEvents(Of Tree(Of T)).Add"/> or <see cref="ListWithEvents(Of Tree(Of T)).Insert"/></remarks>
        Protected Overridable Sub OnAdding(ByVal e As ListWithEvents(Of Tree(Of T)).CancelableItemIndexEventArgs)
            If e Is Nothing Then Throw New NullReferenceException("Cannot add null node into tree")
            If e.Item.Parent IsNot Nothing Then
                e.Cancel = True
                e.CancelMessage = "Cannot add node that have parent already set"
            ElseIf e.Item Is Me.Root Then
                e.Cancel = True
                e.CancelMessage = "Attempt to create cyclic tree"
            End If
        End Sub
        ''' <summary>Called when item was added into <see cref="Nodes"/></summary>
        ''' <param name="e">Event parameters</param>
        Protected Overridable Sub OnAdded(ByVal e As ListWithEvents(Of Tree(Of T)).ItemIndexEventArgs)
            e.Item.Parent = Me
        End Sub
        ''' <summary>Called before item of <see cref="Nodes"/> is changed</summary>
        ''' <param name="e">Event parameters. You can use it to cancel the operation.</param>
        ''' <exception cref="NullReferenceException">Attepmt to assign null reference</exception>
        ''' <remarks>When node that is being passed has already parent set or this node is root of current tree then <see cref="ListWithEvents(Of Tree(Of T)).CancelableItemIndexEventArgs.Cancel"/> is set to true, which causes <see cref="OperationCanceledException"/> to be thrown by <see cref="ListWithEvents(Of Tree(Of T)).Item"/>'s setter</remarks>
        Protected Overridable Sub OnItemChanging(ByVal e As ListWithEvents(Of Tree(Of T)).CancelableItemIndexEventArgs)
            If e Is Nothing Then Throw New NullReferenceException("Cannot put null node into tree")
            If e.Item.Parent IsNot Nothing AndAlso Nodes(e.NewIndex) IsNot e.Item Then
                e.Cancel = True
                e.CancelMessage = "Cannot assign node that gave parent already set"
            ElseIf e.Item Is Me.Root Then
                e.Cancel = True
                e.CancelMessage = "Attempt to create cyclic tree"
            End If
        End Sub
        ''' <summary>Called after item of <see cref="Nodes"/> is changed</summary>
        ''' <param name="e">Event parameters</param>
        Protected Overridable Sub OnItemChanged(ByVal e As ListWithEvents(Of Tree(Of T)).OldNewItemEvetArgs)
            e.Item.Parent = Me
            e.OldItem.Parent = Nothing
        End Sub
        ''' <summary>Called before single item is removed from <see cref="Nodes"/></summary>
        ''' <param name="e">Event parameters. You can use it to cancel the operation.</param>
        ''' <remarks>This method is not called when <see cref="Nodes"/> is to be cleared using <see cref="IList(Of T).Clear"/>. Use <seealso cref="OnClearing"/> instead.</remarks>
        Protected Overridable Sub OnRemoving(ByVal e As ListWithEvents(Of Tree(Of T)).CancelableItemIndexEventArgs)
            'Do nothing
        End Sub
        ''' <summary>Called after single item is removed from <see cref="Nodes"/></summary>
        ''' <param name="e">Event parameters</param>
        ''' <remarks>This method is not called after <see cref="Nodes"/> has been cleared using <see cref="IList(Of T).Clear"/>. Use <seealso cref="OnCleared"/> instead.</remarks>
        Protected Overridable Sub OnRemoved(ByVal e As ListWithEvents(Of Tree(Of T)).ItemIndexEventArgs)
            e.Item.Parent = Nothing
        End Sub
        ''' <summary>Called before whole <see cref="Nodes"/> is cleared at once</summary>
        ''' <param name="e">Event parameters. You can use it to cancel the operation.</param>
        ''' <remarks>In case of clearing <see cref="Nodes"/> at once the <see cref="OnRemoving"/> method is not called.</remarks>
        Protected Overridable Sub OnClearing(ByVal e As CancelEventArgs)
            'Do nothing
        End Sub
        ''' <summary>Called after whole <see cref="Nodes"/> was cleared at once</summary>
        ''' <param name="e">Event parameters</param>
        ''' <remarks>In case of clearing <see cref="Nodes"/> at once the <see cref="OnRemoved"/> method is not called.</remarks>
        Protected Overridable Sub OnCleared(ByVal e As ListWithEvents(Of Tree(Of T)).ItemsEventArgs)
            For Each Itm As Tree(Of T) In e.Items
                Itm.Parent = Nothing
            Next Itm
        End Sub
#End Region
        ''' <summary>Adpots specified sub-tree and removes it from its old <see cref="Parent"/></summary>
        ''' <param name="Node"><see cref="Tree(Of T)"/> to be adopted</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Node"/> is null</exception>
        Public Sub Adopt(ByVal Node As Tree(Of T))
            If Node Is Nothing Then Throw New ArgumentNullException("Node", "Cannot adopt null")
            Node.Parent = Me
        End Sub
        ''' <summary>Adopts clone of given sub-tree</summary>
        ''' <param name="Node"><see cref="Tree(Of T)"/> to be adopted</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Node"/> is null</exception>
        Public Sub AdoptClone(ByVal Node As Tree(Of T))
            If Node Is Nothing Then Throw New ArgumentNullException("Node", "Cannot adopt null")
            Me.Nodes.Add(New Tree(Of T)(Node))
        End Sub
        ''' <summary>Determines if current node is root</summary>
        ''' <returns>True when <see cref="Parent"/> is null</returns>
        Public ReadOnly Property IsRoot() As Boolean
            Get
                Return Parent Is Nothing
            End Get
        End Property
        ''' <summary>determines if current node is leaf</summary>
        ''' <returns>True when current node has no child</returns>
        Public ReadOnly Property IsLeaf() As Boolean
            Get
                Return Me.Nodes.Count = 0
            End Get
        End Property
        ''' <summary>Finds root of tree</summary>
        ''' <returns><see cref="Tree(Of T)"/> that doesnt have <see cref="Parent"/></returns>
        Public ReadOnly Property Root() As Tree(Of T)
            Get
                If Me.Parent Is Nothing Then Return Me Else Return Me.Parent.Root
            End Get
        End Property
        ''' <summary>Computes depth of this node</summary>
        ''' <returns>Number of nodes above current</returns>
        Public ReadOnly Property Depth() As Integer
            Get
                If Me.Parent Is Nothing Then Return 0 Else Return Me.Parent.Depth + 1
            End Get
        End Property
#Region "Enumerators"
        ''' <summary>Gets enumerator that itterates through tree in pre-order manner</summary>
        Public ReadOnly Property PreOrder() As IEnumerable(Of Tree(Of T))
            Get
                Return New TreeEnumerator(Me, Tree.EnumOrders.PreOrder, Tree.EnumDirections.FirstToLast)
            End Get
        End Property
        ''' <summary>Gets enumerator that itterates through tree in post-order manner</summary>
        Public ReadOnly Property PostOrder() As IEnumerable(Of Tree(Of T))
            Get
                Return New TreeEnumerator(Me, Tree.EnumOrders.PostOrder, Tree.EnumDirections.FirstToLast)
            End Get
        End Property
        ''' <summary>Gets enumerator that itterates through tree in in-order manner</summary>
        ''' <remarks>You should use in-order enumerator only on binary trees. Otherwise in-order semantic is ugly: First the bigger half of children from left to right is returned, then parent and then remaining children.</remarks>
        Public ReadOnly Property InOrder() As IEnumerable(Of Tree(Of T))
            Get
                Return New TreeEnumerator(Me, Tree.EnumOrders.InOrder, Tree.EnumDirections.FirstToLast)
            End Get
        End Property
        ''' <summary>Gets enumerator that itterates through tree in pre-order manner from last to first item (from right to left)</summary>
        Public ReadOnly Property PreOrderBackward() As IEnumerable(Of Tree(Of T))
            Get
                Return New TreeEnumerator(Me, Tree.EnumOrders.PreOrder, Tree.EnumDirections.LastToFirst)
            End Get
        End Property
        ''' <summary>Gets enumerator that itterates through tree in post-order manner from last to first item (from right to left)</summary>
        Public ReadOnly Property PostOrderBackward() As IEnumerable(Of Tree(Of T))
            Get
                Return New TreeEnumerator(Me, Tree.EnumOrders.PostOrder, Tree.EnumDirections.LastToFirst)
            End Get
        End Property
        ''' <summary>Gets enumerator that itterates through tree in in-order manner from last to first item (from right to left)</summary>
        ''' <remarks>You should use in-order enumerator only on binary trees. Otherwise in-order semantic is ugly: First the bigger half of children from right to left is returned, then parent and then remaining children.</remarks>
        Public ReadOnly Property InOrderBackward() As IEnumerable(Of Tree(Of T))
            Get
                Return New TreeEnumerator(Me, Tree.EnumOrders.InOrder, Tree.EnumDirections.LastToFirst)
            End Get
        End Property
#End Region
#Region "Enumeration helpers"
        ''' <summary>Left most node of current sub-tree</summary>
        ''' <returns>The most left (first) leaf of current subtree (the <see cref="LeftMost"/> property of first item in <see cref="Nodes"/>) if has any nodes, itself otherwise</returns>
        Public ReadOnly Property LeftMost() As Tree(Of T)
            Get
                If Me.Nodes.Count > 0 Then
                    Return Me.Nodes(0).LeftMost
                Else
                    Return Me
                End If
            End Get
        End Property
        ''' <summary>Right most node of current sub-tree</summary>
        ''' <returns>The most right (last) leaf of current subtree (the <see cref="RightMost"/> property of first item in <see cref="Nodes"/>) if has any nodes, itself otherwise</returns>
        Public ReadOnly Property RightMost() As Tree(Of T)
            Get
                If Me.Nodes.Count > 0 Then
                    Return Me.Nodes(Me.Nodes.Count - 1).RightMost
                Else
                    Return Me
                End If
            End Get
        End Property
        ''' <summary>Next node in forward pre-order order</summary>
        ''' <param name="Root">Optional root of subtree being parsed</param>
        ''' <returns>If node has sub-nodes returns first sub-node. Otherwise serches for right neighbour of the nearest parent as possible. Returns null if nothing found.</returns>
        Public ReadOnly Property PreOrderNextRight(Optional ByVal Root As Tree(Of T) = Nothing) As Tree(Of T)
            Get
                If Me.Nodes.Count > 0 Then
                    Return Me.Nodes(0)
                Else
                    Dim curr As Tree(Of T) = Me
                    While curr IsNot Root AndAlso curr IsNot Nothing
                        Dim n As Tree(Of T) = curr.RightNeighbour
                        If n IsNot Nothing Then Return n
                        curr = curr.Parent
                    End While
                    Return Nothing
                End If
            End Get
        End Property
        ''' <summary>Next node in backward pre-order order</summary>
        ''' <param name="Root">Optional root of subtree being parsed</param>
        ''' <returns>If node has sub-nodes returns last sub-node. Otherwise serches for left neighbour of the nearest parent as possible. Returns null if nothing found.</returns>
        Public ReadOnly Property PreOrderNextLeft(Optional ByVal Root As Tree(Of T) = Nothing) As Tree(Of T)
            Get
                If Me.Nodes.Count > 0 Then
                    Return Me.Nodes(Me.Nodes.Count - 1)
                Else
                    Dim curr As Tree(Of T) = Me
                    While curr IsNot Root AndAlso curr IsNot Nothing
                        Dim n As Tree(Of T) = curr.LeftNeighbour
                        If n IsNot Nothing Then Return n
                        curr = curr.Parent
                    End While
                    Return Nothing
                End If
            End Get
        End Property
        ''' <summary>Index of current node in collection of nodes of <see cref="Parent"/></summary>
        ''' <returns>Index of the current node in collection of nodes of <see cref="Parent"/> if has parent, -1 otherwise</returns>
        Public ReadOnly Property Index() As Integer
            Get
                If Me.Parent Is Nothing Then Return -1
                Return Me.Parent.Nodes.IndexOf(Me)
            End Get
        End Property
        ''' <summary>Next node in collection of nodes of <see cref="Parent"/></summary>
        ''' <returns>Next node if any or null</returns>
        Public ReadOnly Property RightNeighbour() As Tree(Of T)
            Get
                If Me.Parent IsNot Nothing Then
                    Dim MyIndex As Integer = Index
                    If MyIndex < Parent.Nodes.Count - 1 Then Return Parent.Nodes(MyIndex + 1)
                End If
                Return Nothing
            End Get
        End Property
        ''' <summary>Previous node in collection of nodes of <see cref="Parent"/></summary>
        ''' <returns>Previous node if any or null</returns>
        Public ReadOnly Property LeftNeighbour() As Tree(Of T)
            Get
                If Me.Parent IsNot Nothing Then
                    Dim MyIndex As Integer = Index
                    If MyIndex > 0 Then Return Parent.Nodes(MyIndex - 1)
                End If
                Return Nothing
            End Get
        End Property

        ''' <summary>Index of next child in forward in-order order (previous in backward in-order)</summary>
        ''' <returns>Index of node or -1</returns>
        Protected ReadOnly Property InOrderNextChildIndex() As Integer
            Get
                If Me.Nodes.Count <= 1 Then
                    Return -1
                ElseIf Me.Nodes.Count Mod 2 = 0 Then
                    Return Me.Nodes.Count / 2
                Else
                    Return Me.Nodes.Count \ 2 + 1
                End If
            End Get
        End Property
        ''' <summary>Index of previous child in forward in-order order (next in backward in-order)</summary>
        ''' <returns>Indexd of node or -1</returns>
        Protected ReadOnly Property InOrderPrevChildIndex() As Integer
            Get
                If Me.Nodes.Count = 0 Then : Return -1
                ElseIf Me.Nodes.Count Mod 2 <> 0 Then
                    Return Me.Nodes.Count \ 2
                Else
                    Return Me.Nodes.Count / 2 - 1
                End If
            End Get
        End Property
#End Region
        ''' <summary>Gets nearest node on the right (after) current node at same level of tree (not necessaryly under same parent)</summary>
        ''' <remarks>Node on the same level as current node on the right or null if no node found</remarks>
        Public ReadOnly Property Right() As Tree(Of T)
            Get
                Dim Neighbour As Tree(Of T) = Me.RightNeighbour
                If Neighbour IsNot Nothing Then Return Neighbour
                If Parent IsNot Nothing Then
                    Dim IT As New TreeEnumerator(Me.Root, Tree.EnumOrders.PreOrder, Tree.EnumDirections.FirstToLast)
                    IT.CurrentNode = Me
                    While IT.MoveNext
                        If IT.CurrentNode.Depth = Me.Depth Then Return IT.CurrentNode
                    End While
                    Return Nothing
                Else : Return Nothing
                End If
            End Get
        End Property
        ''' <summary>Gets nearest node on the left (before) current node at same level of tree (not necessaryly under same parent)</summary>
        ''' <remarks>Node on the same level as current node on the left or null if no node found</remarks>
        Public ReadOnly Property Left() As Tree(Of T)
            Get
                Dim Neighbour As Tree(Of T) = Me.RightNeighbour
                If Neighbour IsNot Nothing Then Return Neighbour
                If Parent IsNot Nothing Then
                    Dim IT As New TreeEnumerator(Me.Root, Tree.EnumOrders.PreOrder, Tree.EnumDirections.LastToFirst)
                    IT.CurrentNode = Me
                    While IT.MoveNext
                        If IT.CurrentNode.Depth = Me.Depth Then Return IT.CurrentNode
                    End While
                    Return Nothing
                Else : Return Nothing
                End If
            End Get
        End Property
        ''' <summary>Gets value indicating if current node contains given node</summary>
        ''' <param name="Node">Node to be found</param>
        ''' <returns>True if current node is <paramref name="Node"/> or <paramref name="Node"/> is sub-node of current node</returns>
        Public Function Contains(ByVal Node As Tree(Of T)) As Boolean
            For Each n As Tree(Of T) In Me.PreOrder
                If n Is Node Then Return True
            Next n
            Return False
        End Function
        ''' <summary>Gets value indicating if current node contains given value</summary>
        ''' <param name="Value">Value to be found</param>
        ''' <returns>True if <see cref="Value"/> of current node or one of it'S subnodes returns true for the <see cref="System.Object.Equals"/> function.</returns>
        ''' <remarks>If you want to get node that contains such value, use the <see cref="Find"/> function</remarks>
        Public Function Contains(ByVal Value As T) As Boolean
            For Each n As Tree(Of T) In Me.PreOrder
                If n.Value.Equals(Value) Then Return True
            Next n
            Return False
        End Function
        ''' <summary>Searches for given value in current tree</summary>
        ''' <param name="Value">Value to be found</param>
        ''' <returns>First node which's <see cref="Value"/>'s <see cref="System.Object.Equals"/> returns true for <paramref name="Value"/></returns>
        ''' <remarks>This function uses <see cref="PreOrder"/> enumerator.</remarks>
        Public Function Find(ByVal Value As T) As Tree(Of T)
            For Each n As Tree(Of T) In Me.PreOrder
                If n.Value.Equals(Value) Then Return n
            Next n
            Return Nothing
        End Function
        ''' <summary>Enumerates through <see cref="Tree(Of T)"/> (or its sub-tree) in given order</summary>
        Public Class TreeEnumerator : Implements IEnumerator(Of T), IEnumerable(Of T), IEnumerator(Of Tree(Of T)), IEnumerable(Of Tree(Of T))
            ''' <summary>Contains value of the <see cref="Direction"/> property</summary>
            Private _Direction As Tree.EnumDirections
            ''' <summary>Contains value of the <see cref="Order"/> property</summary>
            Private _Order As Tree.EnumOrders
            ''' <summary>Contains value of the <see cref="Root"/> property</summary>
            Private _Root As Tree(Of T)
            ''' <summary>Contains value of the <see cref="CurrentNode"/> property or null when the enumerator is before start or after end of the collection</summary>
            Private Current As Tree(Of T)
            ''' <summary>If <see cref="Current"/> is null distinguishes between position before start (True) and after end (False) of the collection</summary>
            Private Before As Boolean = True
            ''' <summary>Direction of enumerating</summary>
            Public ReadOnly Property Direction() As Tree.EnumDirections
                Get
                    Return _Direction
                End Get
            End Property
            ''' <summary>Order of enumerating</summary>
            Public ReadOnly Property Order() As Tree.EnumOrders
                Get
                    Return _Order
                End Get
            End Property
            ''' <summary>The node that is root for this enumerator</summary>
            Public ReadOnly Property Root() As Tree(Of T)
                Get
                    Return _Root
                End Get
            End Property
            ''' <summary>CTor</summary>
            ''' <param name="Root">The node to start and end enumeration with</param>
            ''' <param name="Order">Order of enumerating</param>
            ''' <param name="Direction">Direction of enumerating</param>
            Public Sub New(ByVal Root As Tree(Of T), ByVal Order As Tree.EnumOrders, ByVal Direction As Tree.EnumDirections)
                _Direction = Direction
                _Order = Order
                _Root = Root
            End Sub

            ''' <summary>Returns an enumerator that iterates through the collection.</summary>
            ''' <returns>If this enumerator was not initialized or disposed, returns itself. If it was returns clone of itself.</returns>
            Public Function GetValueEnumerator() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
                If CurrentNode Is Nothing AndAlso Before AndAlso Not disposedValue Then
                    Return Me
                Else
                    Return New TreeEnumerator(Me.Root, Me.Order, Me.Direction)
                End If
            End Function
            ''' <summary>Returns an enumerator that iterates through the collection.</summary>
            ''' <returns>If this enumerator was not initialized or disposed, returns itself. If it was returns clone of itself.</returns>
            Public Function GetNodeEnumerator() As System.Collections.Generic.IEnumerator(Of Tree(Of T)) Implements System.Collections.Generic.IEnumerable(Of Tree(Of T)).GetEnumerator
                Return GetValueEnumerator()
            End Function
            ''' <summary>Returns an enumerator that iterates through a collection.</summary>
            ''' <returns><see cref="GetValueEnumerator"/></returns>
            ''' <remarks>Use type-safe <see cref="GetValueEnumerator"/> or <see cref="GetNodeEnumerator"/> instead</remarks>
            <EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use type-safe GetEnumerator instead")> _
            Private Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
                Return GetValueEnumerator()
            End Function
            ''' <summary>Gets value of the node in the tree at the current position of the enumerator.</summary>
            ''' <returns>Value of the node in the collection at the current position of the enumerator</returns>
            Public ReadOnly Property CurrentValue() As T Implements System.Collections.Generic.IEnumerator(Of T).Current
                Get
                    Return Current.Value
                End Get
            End Property
            ''' <summary>Gets or sets current position of the enumerator</summary>
            ''' <returns>The node in the collection at the current position of the enumerator</returns>
            ''' <value>New position of the enumerator. New posotion must be in sub-tree of <see cref="Parent"/> or can be null which has same efect as <see cref="Reset"/></value>
            ''' <exception cref="ArgumentException">Attempt to set postion otside of current tree</exception>
            Public Property CurrentNode() As Tree(Of T)
                Get
                    Return Current
                End Get
                Set(ByVal value As Tree(Of T))
                    If Not Root.Contains(value) Then Throw New ArgumentException("Cannot set position otside of current tree")
                    Current = value
                    Before = value Is Nothing
                End Set
            End Property
            ''' <summary>Gets the node in the tree at the current position of the enumerator.</summary>
            ''' <returns>The node in the collection at the current position of the enumerator</returns>
            Private ReadOnly Property CurrentNode1() As Tree(Of T) Implements System.Collections.Generic.IEnumerator(Of Tree(Of T)).Current
                Get
                    Return CurrentNode
                End Get
            End Property
            ''' <summary>Returns <see cref="CurrentValue"/></summary>
            ''' <remarks>Use type-safe <see cref="CurrentNode"/> and <see cref="CurrentValue"/> instead</remarks>
            <Obsolete("Use type-safe CurrentValue and CurrentNode instead"), EditorBrowsable(EditorBrowsableState.Never)> _
            Private ReadOnly Property CurrentValue1() As Object Implements IEnumerator(Of T).Current
                Get
                    Return CurrentValue
                End Get
            End Property
            ''' <summary>Advances the enumerator to the next element of the collection.</summary>
            ''' <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
            Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
                Try
                    If Current Is Nothing AndAlso Before Then
                        'Get first node
                        Select Case Order
                            Case Tree.EnumOrders.PreOrder
                                Current = Root
                            Case Else
                                If Direction = Tree.EnumDirections.FirstToLast Then
                                    Current = Root.LeftMost
                                Else
                                    Current = Root.RightMost
                                End If
                        End Select
                        Before = False
                        Return True
                    ElseIf Current Is Nothing Then
                        Return False
                    Else 'Get next node
                        Select Case Order
                            Case Tree.EnumOrders.PreOrder
                                If Direction = Tree.EnumDirections.FirstToLast Then
                                    Current = Me.Current.PreOrderNextRight(Root)
                                Else
                                    Current = Me.Current.PreOrderNextLeft(Root)
                                End If
                            Case Tree.EnumOrders.PostOrder
                                If Current Is Root Then
                                    Current = Nothing
                                ElseIf Direction = Tree.EnumDirections.FirstToLast Then
                                    Dim Right As Tree(Of T) = Me.Current.RightNeighbour
                                    If Right IsNot Nothing Then
                                        Current = Right.LeftMost
                                    Else : Current = Current.Parent
                                    End If
                                Else
                                    Dim Left As Tree(Of T) = Me.Current.LeftNeighbour
                                    If Left IsNot Nothing Then
                                        Current = Left.RightMost
                                    Else : Current = Current.Parent
                                    End If
                                End If
                            Case Else 'In-order
                                Dim Index As Integer
                                If Direction = Tree.EnumDirections.FirstToLast Then
                                    Index = Me.Current.InOrderNextChildIndex
                                Else
                                    Index = Me.Current.InOrderPrevChildIndex
                                End If
                                If Index >= 0 Then
                                    Current = Me.Current.Nodes(Index)
                                ElseIf Me.Current.Parent Is Nothing OrElse Me.Current.Parent Is Root Then
                                    Current = Nothing
                                Else
                                    Dim MyIndex As Integer = Me.Current.Index
                                    If (Index = Me.Current.Parent.InOrderPrevChildIndex AndAlso Direction = Tree.EnumDirections.FirstToLast) OrElse (Index = Me.Current.Parent.InOrderNextChildIndex AndAlso Direction = Tree.EnumDirections.LastToFirst) Then
                                        Current = Current.Parent
                                    Else
                                        Dim NextNode As Tree(Of T)
                                        If Direction = Tree.EnumDirections.FirstToLast Then
                                            NextNode = Me.Current.RightNeighbour
                                        Else
                                            NextNode = Me.Current.LeftNeighbour
                                        End If
                                        If NextNode IsNot Nothing Then
                                            Current = NextNode
                                        ElseIf Direction = Tree.EnumDirections.FirstToLast Then
                                            Current = Current.PreOrderNextRight(Root)
                                        Else
                                            Current = Current.PreOrderNextLeft(Root)
                                        End If
                                    End If
                                End If
                        End Select
                    End If
                Finally
                    If Current Is Nothing Then MoveNext = False
                End Try
            End Function
            ''' <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
            Public Sub Reset() Implements System.Collections.IEnumerator.Reset
                Current = Nothing
                Before = True
            End Sub
#Region " IDisposable Support "
            ''' <summary>To detect redundant calls</summary>
            Private disposedValue As Boolean = False
            ''' <summary>IDisposable</summary>
            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                    End If
                End If
                Me.disposedValue = True
            End Sub

            ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
            ''' <remarks>This code added by Visual Basic to correctly implement the disposable pattern.</remarks>
            Public Sub Dispose() Implements IDisposable.Dispose
                ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
                Dispose(True)
                GC.SuppressFinalize(Me)
            End Sub
#End Region

        End Class
    End Class

    ''' <summary>Contains shared utilities for all trees</summary>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public NotInheritable Class Tree
        ''' <summary>This class cannot be instantiated</summary>
        Private Sub New()
        End Sub
        ''' <summary>Directions of tree enumerationg</summary>
        Public Enum EnumDirections
            ''' <summary>Frøom frist to last (left to right)</summary>
            FirstToLast = 1
            ''' <summary>From last to first (right to left)</summary>
            LastToFirst = -1
        End Enum
        ''' <summary>Tree enumeration orders</summary>
        Public Enum EnumOrders
            ''' <summary>Pre-order: Me, then children</summary>
            PreOrder = -1
            ''' <summary>In-order: Me between children. Good only for binary trees. Current node is enumerated after first half of children. In it has only one child the current node is enumerated second</summary>
            InOrder = 0
            ''' <summary>Post-order: Children, then me</summary>
            PostOrder = 1
        End Enum
    End Class
#End If
End Namespace
