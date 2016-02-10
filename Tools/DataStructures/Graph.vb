Imports Tools.ComponentModelT

#If True
Imports System.Linq, Tools.LinqT
Namespace DataStructuresT.GenericT
    ''' <summary>Represents graph node</summary>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public Class GraphNode
#Region "Common"
        ''' <summary>Contains value of the <see cref="Edges"/> property</summary>
        Private WithEvents _Edges As New ListWithEvents(Of GraphEdge) With {.Owner = Me}
        ''' <summary>Gets edges of this node</summary>
        ''' <returns>Edges from and to this node</returns>
        Public ReadOnly Property Edges() As CollectionsT.GenericT.IReadOnlyList(Of GraphEdge)
            Get
                Return _Edges
            End Get
        End Property
        ''' <summary>Gets edges of this node for adding and removing</summary>
        ''' <returns>Edges from and to this node</returns>
        Friend ReadOnly Property EdgesInternal() As ListWithEvents(Of GraphEdge)
            Get
                Return _Edges
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="Graph"/> property</summary>
        Private _Graph As Graph
        ''' <summary>Gets graph this node belongs to</summary>
        ''' <returns>Graph this node belongs to, null when this node is not in graph</returns>
        Public Property Graph() As Graph
            Get
                Return _Graph
            End Get
            Friend Set(ByVal value As Graph)
                _Graph = value
            End Set
        End Property
#Region "Neighbours"
        ''' <summary>Gets all neighbouring nodes of current node</summary>
        ''' <returns>Nodes at the other sides of all (incoming and outgoing) edges of this node</returns>
        Public ReadOnly Property Neighbours() As IEnumerable(Of GraphNode)
            Get
                Return (From Edge In Edges Select If(Edge.Node1 Is Me, Edge.Node2, Edge.Node1))
            End Get
        End Property
        ''' <summary>Gets all neighbour nodes that can be reached form this node</summary>
        ''' <returns>Nodes edge from this node leads to</returns>
        Public ReadOnly Property ReachableNeighbours() As IEnumerable(Of GraphNode)
            Get
                Return (From edge In Edges Where edge.BiDir OrElse edge.Node1 Is Me Select If(edge.Node1 Is Me, edge.Node2, edge.Node1))
            End Get
        End Property
        ''' <summary>Gets all neighbour nodes that can reach this node</summary>
        ''' <returns>Nodes edge to this node leads from</returns>
        Public ReadOnly Property ReachingNeighbours() As IEnumerable(Of GraphNode)
            Get
                Return (From edge In Edges Where edge.BiDir OrElse edge.Node2 Is Me Select If(edge.Node1 Is Me, edge.Node2, edge.Node1))
            End Get
        End Property
#Region "With edges"
        ''' <summary>Gets edges to all neighbour nodes that can be reached form this node</summary>
        ''' <returns>Nodes edge from this node leads to</returns>
        Public ReadOnly Property EdgesToReachableNeighbours() As IEnumerable(Of GraphEdge)
            Get
                Return (From edge In Edges Where edge.BiDir OrElse edge.Node1 Is Me)
            End Get
        End Property
        ''' <summary>Gets edges to all neighbour nodes that can reach this node</summary>
        ''' <returns>Nodes edge to this node leads from</returns>
        Public ReadOnly Property EdgesToReachingNeighbours() As IEnumerable(Of GraphEdge)
            Get
                Return (From edge In Edges Where edge.BiDir OrElse edge.Node2 Is Me)
            End Get
        End Property
#End Region
#End Region
        ''' <summary>Contains value of the <see cref="Tag"/> property</summary>
        Private _Tag As Object
        ''' <summary>Additional object attached to this node</summary>
        Public Property Tag() As Object
            Get
                Return _Tag
            End Get
            Set(ByVal value As Object)
                _Tag = value
            End Set
        End Property
        ''' <summary>Clones non-graph properties of current node</summary>
        ''' <returns>Cloned node</returns>
        ''' <remarks>This implementation clones only the <see cref="Tag"/> property</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Overridable Function Clone() As GraphNode
            Dim NewNode As New GraphNode
            NewNode.Tag = Me.Tag
            Return NewNode
        End Function
        ''' <summary>Gets value indicating if this node is stand alone</summary>
        ''' <returns>True where tehere are no edges leading to or from this node</returns>
        Public ReadOnly Property StandAlone() As Boolean
            Get
                Return Edges.Count = 0
            End Get
        End Property
        ''' <summary>Gets value indicating if this node is root</summary>
        ''' <returns>True if there are no edges leading to this node</returns>
        Public ReadOnly Property IsRoot() As Boolean
            Get
                Return (From edge In Edges Where edge.Node2 Is Me Or edge.BiDir).Count = 0
            End Get
        End Property
        ''' <summary>Gets value indicating if this node is dead end</summary>
        ''' <returns>True if there are no edges leading from this node</returns>
        Public ReadOnly Property IsDeadEnd() As Boolean
            Get
                Return (From edge In Edges Where edge.Node1 Is Me Or edge.BiDir).Count = 0
            End Get
        End Property
        ''' <summary>Gets total degree of the node</summary>
        ''' <returns>Number of edges inciding with this node</returns>
        Public ReadOnly Property Degree() As Integer
            Get
                Return Edges.Count
            End Get
        End Property
        ''' <summary>Gets input degree of this node</summary>
        ''' <returns>Number of edges incoming to this node</returns>
        Public ReadOnly Property DegreeIn() As Integer
            Get
                Return (From edge In Edges Where edge.Node2 Is Me Or edge.BiDir).Count
            End Get
        End Property
        ''' <summary>Gets output degree of this node</summary>
        ''' <returns>Number of edges outgoing from this node</returns>
        Public ReadOnly Property DegreeOut() As Integer
            Get
                Return (From edge In Edges Where edge.Node2 Is Me Or edge.BiDir).Count
            End Get
        End Property
        ''' <summary>Removes this node from graph</summary>
        ''' <remarks>All edges inciding with this node are removed as well. 
        ''' <para>If <see cref="Graph"/> is null, no action is taken.</para></remarks>
        Public Sub Remove()
            If Graph IsNot Nothing Then Graph.Nodes.Remove(Me)
        End Sub
#End Region
#Region "Events"
        Private Sub Edges_Added(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of GraphEdge), ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphEdge).ItemIndexEventArgs) Handles _Edges.Added
            OnEdgeAdded(e)
        End Sub

        Private Sub Edges_Cleared(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of GraphEdge), ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphEdge).ItemsEventArgs) Handles _Edges.Cleared
            OnEdgesCleared(e)
        End Sub

        Private Sub Edges_ItemChanged(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of GraphEdge), ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphEdge).OldNewItemEventArgs) Handles _Edges.ItemChanged
            OnEdgeChanged(e)
        End Sub

        Private Sub Edges_Removed(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of GraphEdge), ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphEdge).ItemIndexEventArgs) Handles _Edges.Removed
            OnEdgeRemoved(e)
        End Sub
        ''' <summary>When impelmented in derived class reacts to item add to the <see cref="Edges"/> collection</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>This implementation does nothing</remarks>
        Protected Overridable Sub OnEdgeAdded(ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphEdge).ItemIndexEventArgs)
        End Sub
        ''' <summary>When impelmented in derived class reacts to all items removal from the <see cref="Edges"/> collection</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>This implementation does nothing</remarks>
        Protected Overridable Sub OnEdgesCleared(ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphEdge).ItemsEventArgs)
        End Sub
        ''' <summary>When impelmented in derived class reacts to item replacement in the <see cref="Edges"/> collection</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>This implementation does nothing</remarks>
        Protected Overridable Sub OnEdgeChanged(ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphEdge).OldNewItemEventArgs)
        End Sub
        ''' <summary>When impelmented in derived class reacts to item removal from the <see cref="Edges"/> collection</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>This implementation does nothing</remarks>
        Protected Overridable Sub OnEdgeRemoved(ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphEdge).ItemIndexEventArgs)
        End Sub
#End Region
        ''' <summary>Contains alghoritm data</summary>
        ''' <remarks>When particular alghoritm runs over the graph, it stores its data here. Do not change this field when alghoritm runs!</remarks>
        Public AlghoritmData As GraphAlghoritmData
       
        
    End Class
    ''' <summary>Represents graph edge</summary>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public Class GraphEdge
        ''' <summary>Creates new instance of the <see cref="GraphEdge"/> class with <see cref="Value"/> 1</summary>
        Public Sub New()
            Me.New(1)
        End Sub
#Region "Common"
        ''' <summary>Creates new instance of the <see cref="GraphEdge"/> class with given value of <see cref="Value"/></summary>
        ''' <param name="Value">Value (weight) of edge</param>
        Public Sub New(ByVal Value As Double)
            Me.Value = Value
        End Sub
        ''' <summary>Contains value of the <see cref="Value"/> property</summary>
        Private _Value As Double
        ''' <summary>Gets or sets value (weight, length, capacity etc.) of this edge</summary>
        Public Overridable Property Value() As Double
            Get
                Return _Value
            End Get
            Set(ByVal value As Double)
                _Value = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Node1"/> property</summary>
        Private _Node1 As GraphNode
        ''' <summary>Gets node this edge starts in</summary>
        Public Property Node1() As GraphNode
            Get
                Return _Node1
            End Get
            Friend Set(ByVal value As GraphNode)
                _Node1 = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Node2"/> property</summary>
        Private _Node2 As GraphNode
        ''' <summary>Gets node this edge ends in</summary>
        Public Property Node2() As GraphNode
            Get
                Return _Node2
            End Get
            Friend Set(ByVal value As GraphNode)
                _Node2 = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="BiDir"/> rpoeprty</summary>
        Private _BiDir As Boolean = True
        ''' <summary>Gets or sets value idicating if this edge is bidirectional (two-way)</summary>
        ''' <returns>True if this property leads from <see cref="Node1"/> to <see cref="Node2"/> as well as from <see cref="Node2"/> to <see cref="Node1"/>; false if this edge leads from <see cref="Node1"/> to <see cref="Node2"/> only.</returns>
        ''' <value>True to make this edg two-way; false to make it one-way (oriented)</value>
        Public Property BiDir() As Boolean
            Get
                Return _BiDir
            End Get
            Set(ByVal value As Boolean)
                _BiDir = value
            End Set
        End Property
        ''' <summary>Gets graph this edge belongs to</summary>
        Public ReadOnly Property Graph() As Graph
            Get
                If Node1 IsNot Nothing Then Return Node1.Graph Else Return Node2.Graph
            End Get
        End Property
        ''' <summary>Removes the edge from inciding nodes and continer graph</summary>
        Public Sub Remove()
            Me.Node1.EdgesInternal.Remove(Me)
            Me.Node2.EdgesInternal.Remove(Me)
            Me.Node1 = Nothing
            Me.Node2 = Nothing
        End Sub
        ''' <summary>Contains value of the tag property</summary>
        Private _Tag As Object
        ''' <summary>Additional object attached to edge</summary>
        Public Property Tag() As Object
            Get
                Return _Tag
            End Get
            Set(ByVal value As Object)
                _Tag = value
            End Set
        End Property
        ''' <summary>Gets value indicating if edge from same and to same node exits</summary>
        ''' <returns>True if there is another edge from <see cref="Node1"/> to <see cref="Node2"/> with same direction</returns>
        ''' <remarks>Edges where one is bidirectional and the other is not are not considered duplicate</remarks>
        Public ReadOnly Property IsDuplicate() As Boolean
            Get
                If Me.Node1 Is Nothing OrElse Me.Node2 Is Nothing Then Return False
                For Each Other In Node1.Edges
                    If Other.BiDir = Me.BiDir AndAlso Me.Leads(Other.Node1, Other.Node2) Then Return True
                Next
                Return False
            End Get
        End Property
        ''' <summary>Gets value indicating if this edge is loop</summary>
        ''' <returns>True if <see cref="Node1"/> and <see cref="Node2"/> are the same</returns>
        Public ReadOnly Property IsLoop() As Boolean
            Get
                Return Node1 Is Node2
            End Get
        End Property
        ''' <summary>Gets value indicating if this edge connects given nodes in any direction</summary>
        ''' <param name="Node1">A node</param>
        ''' <param name="Node2">A node</param>
        ''' <returns>True if this this one of given nodes and ends in the other</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Node1"/> or <paramref name="Node2"/> is <see langword="null"/></exception>
        ''' <seelaso cref="Leads"/>
        Public Function Connets(ByVal Node1 As GraphNode, ByVal Node2 As GraphNode) As Boolean
            If Node1 Is Nothing Then Throw New ArgumentNullException("Node1")
            If Node2 Is Nothing Then Throw New ArgumentNullException("Node2")
            Return (Node1 Is Me.Node1 AndAlso Node2 Is Me.Node2) OrElse (Node2 Is Me.Node1 AndAlso Node1 Is Me.Node2)
        End Function
        ''' <summary>Gets value indicating if this edge leads from given start node to given end node</summary>
        ''' <param name="Node1">Expected start node</param>
        ''' <param name="Node2">Expected end node</param>
        ''' <returns>True if this edge leads from <paramref name="Node1"/> to <paramref name="Node2"/></returns>
        ''' <remarks>If <see cref="BiDir"/> is true, has same result as <see cref="Connets"/></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Node1"/> or <paramref name="Node2"/> is <see langword="null"/></exception>
        ''' <seelaso cref="Connets"/>
        Public Function Leads(ByVal Node1 As GraphNode, ByVal Node2 As GraphNode) As Boolean
            If Node1 Is Nothing Then Throw New ArgumentNullException("Node1")
            If Node2 Is Nothing Then Throw New ArgumentNullException("Node2")
            If Me.BiDir Then Return Connets(Node1, Node2)
            Return Node1 Is Me.Node1 AndAlso Node2 Is Me.Node2
        End Function
        ''' <summary>Gets the other node of edge</summary>
        ''' <param name="Node">One of edge nodes</param>
        ''' <returns>Other edge node. If <see cref="Node1"/> is <see cref="Node2"/> returns <paramref name="Node"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Node"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Node"/> is neither <see cref="Node1"/> node <see cref="Node2"/></exception>
        Public Function GetOther(ByVal Node As GraphNode) As GraphNode
            If Node Is Nothing Then Throw New ArgumentNullException("Node")
            If Node Is Me.Node1 Then Return Node2
            If Node Is Me.Node2 Then Return Node1
            Throw New ArgumentException("Given node is not at current edge.") 'Localize: Exception
        End Function
        ''' <summary>Clones non-graph properties of edge and <see cref="BiDir"/></summary>
        ''' <returns>Cloned edge</returns>
        ''' <remarks>This implementation clones only <see cref="Tag"/>, <see cref="Value"/> and <see cref="BiDir"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Overridable Function Clone() As GraphEdge
            Dim NewEdge As New GraphEdge
            NewEdge.Tag = Me.Tag
            NewEdge.Value = Me.Value
            NewEdge.BiDir = Me.BiDir
            Return NewEdge
        End Function
#End Region
    End Class
    ''' <summary>Represents whole graph, implements graph alghoritms</summary>
    ''' <remarks>You should always run only one graph alghoritm at time (on single graph)</remarks>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public Class Graph
        Implements ICloneable(Of Graph)
        ''' <summary>Contains value of the <see cref="Nodes"/> property</summary>
        Private WithEvents _Nodes As New ListWithEvents(Of GraphNode) With {.Owner = Me}
        ''' <summary>Gets edges of this graph</summary>
        Public ReadOnly Property Edges() As ListWithEvents(Of GraphEdge)
            Get
                Return (From Node In Nodes Select es = DirectCast(Node.Edges, IEnumerable(Of GraphEdge))).FlatAllDeffered.Distinct
            End Get
        End Property
        ''' <summary>Gets nodes of this graph</summary>
        ''' <exception cref="OperationCanceledException"><see cref="OperationCanceledException"/> may be thrown when adding or changin items in this collection using invalid new items. Invalid node is such node that is already present in this or another graph.</exception>
        Public ReadOnly Property Nodes() As ListWithEvents(Of GraphNode)
            Get
                Return _Nodes
            End Get
        End Property
#Region "Event handlers"
        ''' <summary>Adds or removes handlers of node</summary>
        ''' <param name="item">Node to add/remove handler of</param>
        ''' <param name="Add">True to add, false to remove</param>
        Private Sub Handlers(ByVal item As GraphNode, ByVal Add As Boolean)
            If Add Then
                AddHandler item.EdgesInternal.Added, AddressOf Edges_Added
                AddHandler item.EdgesInternal.Cleared, AddressOf Edges_Cleared
                AddHandler item.EdgesInternal.ItemChanged, AddressOf Edges_ItemChanged
                AddHandler item.EdgesInternal.Removed, AddressOf Edges_Removed
            Else
                RemoveHandler item.EdgesInternal.Added, AddressOf Edges_Added
                RemoveHandler item.EdgesInternal.Cleared, AddressOf Edges_Cleared
                RemoveHandler item.EdgesInternal.ItemChanged, AddressOf Edges_ItemChanged
                RemoveHandler item.EdgesInternal.Removed, AddressOf Edges_Removed
            End If
        End Sub
#Region "Node"
        Private Sub Nodes_Added(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of GraphNode), ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphNode).ItemIndexEventArgs) Handles _Nodes.Added
            e.Item.Graph = Me
            Handlers(e.Item, True)
            OnNodeAdded(e)
        End Sub
        ''' <summary>When overridden in derived class takes additional actions when node is added</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>This implementation does nothing</remarks>
        Protected Overridable Sub OnNodeAdded(ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphNode).ItemIndexEventArgs)
        End Sub

        Private Sub Nodes_Adding(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of GraphNode), ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphNode).CancelableItemIndexEventArgs) Handles _Nodes.Adding
            If Nodes.Contains(e.Item) Then
                e.Cancel = True
                e.CancelMessage = "Cannot add duplicate node"
            ElseIf e.Item.Graph IsNot Nothing AndAlso e.Item.Graph IsNot Me Then
                e.Cancel = True
                e.CancelMessage = "Cannot add node that is already in another graph"
            End If
        End Sub

        Private Sub Nodes_Cleared(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of GraphNode), ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphNode).ItemsEventArgs) Handles _Nodes.Cleared
            Me.Edges.Clear()
            For Each RemovedNode In e.Items
                Handlers(RemovedNode, False)
                RemovedNode.Graph = Nothing
                RemovedNode.EdgesInternal.Clear()
            Next
            OnNodesCleared(e)
        End Sub
        ''' <summary>When overridden in derived class takes additional actions when the <see cref="Nodes"/> collection is cleared</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>This implementation does nothing</remarks>
        Protected Overridable Sub OnNodesCleared(ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphNode).ItemsEventArgs)
        End Sub

        Private Sub Nodes_ItemChanged(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of GraphNode), ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphNode).OldNewItemEventArgs) Handles _Nodes.ItemChanged
            Dim OldEdges As New List(Of GraphEdge)
            If e.Item IsNot e.OldItem Then
                Handlers(e.OldItem, False)
                For Each NodeEdge In e.OldItem.Edges
                    OldEdges.Add(NodeEdge)
                    If NodeEdge.Node1 Is e.Item Then NodeEdge.Node1 = Nothing Else NodeEdge.Node2 = Nothing
                Next
                e.OldItem.Graph = Nothing
            End If
            If e.Item.Graph IsNot Nothing Then e.Item.Graph = Me
            If e.Item IsNot e.OldItem Then
                For Each OldEdge In OldEdges
                    If OldEdge.Node1 Is Nothing Then OldEdge.Node1 = e.Item Else OldEdge.Node2 = e.Item
                    e.Item.EdgesInternal.Add(OldEdge)
                Next
                Handlers(e.Item, True)
                OnNodeChanged(e)
            End If
        End Sub

        ''' <summary>When overridden in derived class takes additional actions when item in the <see cref="Nodes"/> collection is replaced</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>This implementation does nothing</remarks>
        Protected Overridable Sub OnNodeChanged(ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphNode).OldNewItemEventArgs)
        End Sub

        Private Sub Nodes_ItemChanging(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of GraphNode), ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphNode).CancelableItemIndexEventArgs) Handles _Nodes.ItemChanging
            If Nodes.Contains(e.Item) AndAlso Nodes.IndexOf(e.Item) <> e.NewIndex Then
                e.Cancel = True
                e.CancelMessage = "Cannot add duplicate node" 'Localize: error message
            ElseIf e.Item.Graph IsNot Nothing AndAlso e.Item.Graph IsNot Me Then
                e.Cancel = True
                e.CancelMessage = "Cannot add node that is already in another graph"
            End If
        End Sub

        Private Sub Nodes_Removed(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of GraphNode), ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphNode).ItemIndexEventArgs) Handles _Nodes.Removed
            Handlers(e.Item, False)
            For Each NodeEdge In e.Item.Edges
                NodeEdge.Node1 = Nothing
                NodeEdge.Node2 = Nothing
            Next
            e.Item.EdgesInternal.Clear()
            e.Item.Graph = Nothing
            OnNodeRemoved(e)
        End Sub
        ''' <summary>When overridden in derived class takes additional actions when item is added to the <see cref="Nodes"/> collection</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>This implementation does nothing</remarks>
        Protected Overridable Sub OnNodeRemoved(ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphNode).ItemIndexEventArgs)
        End Sub
#End Region
#Region "Edge"
        Private Sub Edges_Added(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of GraphEdge), ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphEdge).ItemIndexEventArgs)
            OnEdgeAdded(sender.Owner, e)
        End Sub

        Private Sub Edges_Cleared(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of GraphEdge), ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphEdge).ItemsEventArgs)
            OnEdgesCleared(sender.Owner, e)
        End Sub

        Private Sub Edges_ItemChanged(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of GraphEdge), ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphEdge).OldNewItemEventArgs)
            OnEdgeChanged(sender.Owner, e)
        End Sub

        Private Sub Edges_Removed(ByVal sender As CollectionsT.GenericT.ListWithEvents(Of GraphEdge), ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphEdge).ItemIndexEventArgs)
            OnEdgeRemoved(sender.Owner, e)
        End Sub
        ''' <summary>When impelmented in derived class reacts to item add to the <see cref="Edges"/> collection</summary>
        ''' <param name="e">Event arguments</param>
        ''' <param name="Node">Node the event occured for</param>
        ''' <remarks>This implementation does nothing. Not called when node is replaced.</remarks>
        Protected Overridable Sub OnEdgeAdded(ByVal Node As GraphNode, ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphEdge).ItemIndexEventArgs)
        End Sub
        ''' <summary>When impelmented in derived class reacts to all items of single node removal from the <see cref="Edges"/> collection</summary>
        ''' <param name="e">Event arguments</param>
        ''' <param name="Node">Node the event occured for</param>
        ''' <remarks>This implementation does nothing. Not called when <see cref="Nodes"/> are claread, or node is removed.</remarks>
        Protected Overridable Sub OnEdgesCleared(ByVal Node As GraphNode, ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphEdge).ItemsEventArgs)
        End Sub
        ''' <summary>When impelmented in derived class reacts to item replacement in the <see cref="Edges"/> collection</summary>
        ''' <param name="e">Event arguments</param>
        ''' <param name="Node">Node the event occured for</param>
        ''' <remarks>This implementation does nothing. Not called.</remarks>
        Protected Overridable Sub OnEdgeChanged(ByVal Node As GraphNode, ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphEdge).OldNewItemEventArgs)
        End Sub
        ''' <summary>When impelmented in derived class reacts to item removal from the <see cref="Edges"/> collection</summary>
        ''' <param name="e">Event arguments</param>
        ''' <param name="Node">Node the event occured for</param>
        ''' <remarks>This implementation does nothing. If node is removed, called only for remaining node (not called for loop edges). Not called when node is replaced.</remarks>
        Protected Overridable Sub OnEdgeRemoved(ByVal Node As GraphNode, ByVal e As CollectionsT.GenericT.ListWithEvents(Of GraphEdge).ItemIndexEventArgs)
        End Sub
#End Region
#End Region
#Region "Basic operations"
        ''' <summary>Connects two nodes using given edge</summary>
        ''' <param name="Node1">Starting node</param>
        ''' <param name="Node2">End node</param>
        ''' <param name="Edge">Connecting edge</param>
        ''' <returns><paramref name="Edge"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Node1"/>, <paramref name="Node2"/> or <paramref name="Edge"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Node1"/> or <paramref name="Node2"/> does not belong to current graph
        ''' -or- <paramref name="Edge"/> belongs to any graph.</exception>
        Public Function Connect(ByVal Node1 As GraphNode, ByVal Node2 As GraphNode, ByVal Edge As GraphEdge) As GraphEdge
            If Node1 Is Nothing Then Throw New ArgumentNullException("Node1")
            If Node2 Is Nothing Then Throw New ArgumentNullException("Node2")
            If Edge Is Nothing Then Throw New ArgumentNullException("Edge")
            If Node1.Graph IsNot Me Then Throw New ArgumentException("Node must be in current graph", "Node1") 'Localize: Exceptions
            If Node2.Graph IsNot Me Then Throw New ArgumentException("Node must be in current graph", "Node2")
            If Edge.Graph IsNot Nothing Then Throw New ArgumentException("Edge must not be in any graph", "Edge")
            Edge.Node1 = Node1
            Edge.Node2 = Node2
            Node1.EdgesInternal.Add(Edge)
            Node2.EdgesInternal.Add(Edge)
            Return Edge
        End Function
        ''' <summary>Connects two nodes using new edge</summary>
        ''' <param name="Node1">Staring node</param>
        ''' <param name="Node2">End node</param>
        ''' <returns>Newly created edge. The edge has <see cref="GraphEdge.Value"/> 1 and <see cref="GraphEdge.BiDir"/> true (unless <see cref="CreateEdge"/> in derived class overrides this behavior)</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Node1"/> or <paramref name="Node2"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Node1"/> or <paramref name="Node2"/> does not belong to current graph</exception>
        Public Function Connect(ByVal Node1 As GraphNode, ByVal Node2 As GraphNode) As GraphEdge
            Return Me.Connect(Node1, Node2, CreateEdge)
        End Function
        ''' <summary>Connects two nodes using new edge with given weight</summary>
        ''' <param name="Node1">Staring node</param>
        ''' <param name="Node2">End node</param>
        ''' <returns>Newly created edge. The edge has <see cref="GraphEdge.Value"/> <paramref name="EdgeValue"/> and <see cref="GraphEdge.BiDir"/> true (unless <see cref="CreateEdge"/> in derived class overrides this behavior)</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Node1"/> or <paramref name="Node2"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Node1"/> or <paramref name="Node2"/> does not belong to current graph</exception>
        Public Function Connect(ByVal Node1 As GraphNode, ByVal Node2 As GraphNode, ByVal EdgeValue As Double) As GraphEdge
            Return Me.Connect(Node1, Node2, CreateEdge(EdgeValue))
        End Function
        ''' <summary>Creates new edge</summary>
        ''' <param name="Value">Weight of edge</param>
        ''' <returns>Newly created edge. Edge has not graph set.</returns>
        ''' <remarks>Used by <see cref="Connect"/></remarks>
        Protected Overridable Function CreateEdge(Optional ByVal Value As Double = 1) As GraphEdge
            Return New GraphEdge(Value)
        End Function
        ''' <summary>Clones the graph</summary>
        ''' <returns>New cloned graph</returns>
        Public Overridable Function Clone() As Graph Implements ICloneable(Of Graph).Clone
            Dim ng As New Graph
            Dim EdgeDic As New Dictionary(Of GraphEdge, GraphEdge)
            Dim NodeDic As New Dictionary(Of GraphNode, GraphNode)
            For Each node In Me.Nodes
                Dim NewNode = node.Clone
                Me.Nodes.Add(NewNode)
                NodeDic.Add(NewNode, node)
            Next
            For Each node In ng.Nodes
                For Each Edge In NodeDic(node).Edges
                    If EdgeDic.ContainsKey(Edge) Then
                        node.EdgesInternal.Add(EdgeDic(Edge))
                    Else
                        Dim NewEdge = Edge.Clone
                        NewEdge.Node1 = NodeDic(Edge.Node1)
                        NewEdge.Node2 = NodeDic(Edge.Node2)
                        node.EdgesInternal.Add(NewEdge)
                        EdgeDic.Add(Edge, NewEdge)
                    End If
                Next
            Next
            Return ng
        End Function
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance.</returns>
        Private Function ICloneable_Clone() As Object Implements ICloneable.Clone
            Return Clone()
        End Function
#End Region
#Region "Alghoritms"
        ''' <summary>Contains value of the <see cref="BackgroundWorker"/> property</summary>
        Private bgw As BackgroundWorker
        ''' <summary>When set, graph alghoritms uses it to report progress (if supported) and to cancel itself</summary>
        ''' <returns>Current background worker used by alghoritms</returns>
        ''' <remarks>State (if reported) is reported in <see cref="T:Tools.WindowsT.FormsT.ProgressMonitor"/>-compatible way</remarks>
        Public Property BackgroundWorker() As BackgroundWorker
            Get
                Return bgw
            End Get
            Set(ByVal value As BackgroundWorker)
                bgw = value
            End Set
        End Property
#Region "Dijkstra"
        ''' <summary>Runs Dijkstra alghoritm of inding the shortest weighted paths from given node</summary>
        ''' <param name="From">Start node to search fro paths from</param>
        ''' <remarks>When alghoritm finishes, each node (N) has set its distance from <paramref name="From"/> in <see cref="GraphAlghoritmData.DoubleDistance"/> and its predecessor in reverse-direction path from N to <paramref name="From"/> in <see cref="GraphAlghoritmData.Predecessor"/> (edge can be determined by <see cref="GraphAlghoritmData.PredecessorEdge"/>.
        ''' <para>This alghoritm cannot work when <see cref="GraphEdge.Value"/> is zero or less.</para>
        ''' <para>This alghoritm reports progress.</para></remarks>
        ''' <exception cref="OperationCanceledException"><see cref="BackgroundWorker"/>.<see cref="BackgroundWorker.CancellationPending">CancellationPending</see> is set to true.</exception>
        ''' <exception cref="InvalidGraphException">Edge with <see cref="GraphEdge.Value"/> less than or equal to zero reached.</exception>
        Public Sub FindPaths(ByVal From As GraphNode)
            If From Is Nothing Then Throw New ArgumentNullException("From")
            If From.Graph IsNot Me Then Throw New ArgumentException("Node must be member of current graph") 'Localize: Exception
            If bgw IsNot Nothing AndAlso bgw.WorkerReportsProgress Then
                bgw.ReportProgress(-1, System.Windows.Forms.ProgressBarStyle.Blocks)
            End If
            'Init-paths
            For Each Node In Nodes
                Node.AlghoritmData = New GraphAlghoritmData With {.DoubleDistance = Double.PositiveInfinity, .Predecessor = Nothing, .PredecessorEdge = Nothing}
            Next
            From.AlghoritmData.DoubleDistance = 0
            Dim Queue As New PriorityQueue(Of GraphNode)(
                    New GenericComparer(Of GraphNode)(Function(a, b) a.AlghoritmData.DoubleDistance.CompareTo(b.AlghoritmData.DoubleDistance)), PriorityTarget.MinimumFirst)
            For Each node In Nodes
                Queue.Push(node)
            Next
            While Not Queue.Count = 0
                Dim u = Queue.Pop
                For Each h In u.EdgesToReachableNeighbours
                    If h.Value <= 0 Then Throw New InvalidGraphException(ResourcesT.Exceptions.AllEdgesValuesMustBePositive)
                    DijkstraRelax(u, h, Queue)
                Next
                If bgw IsNot Nothing AndAlso bgw.CancellationPending Then Throw New OperationCanceledException()
                If bgw IsNot Nothing AndAlso bgw.WorkerReportsProgress Then
                    bgw.ReportProgress(100 - Math.Max(Queue.Count / Nodes.Count, 1) * 100, System.Windows.Forms.ProgressBarStyle.Blocks)
                End If
            End While
        End Sub
        ''' <summary>Relaxes path for Dijkstra alhoritm</summary>
        ''' <param name="u">Parent node</param>
        ''' <param name="h">Observed edge</param>
        ''' <param name="q">Queue</param>
        Private Sub DijkstraRelax(ByVal u As GraphNode, ByVal h As GraphEdge, ByVal q As PriorityQueue(Of GraphNode))
            Dim v = h.GetOther(u)
            If v.AlghoritmData.DoubleDistance > u.AlghoritmData.DoubleDistance + h.Value Then
                v.AlghoritmData.DoubleDistance = u.AlghoritmData.DoubleDistance + h.Value
                v.AlghoritmData.Predecessor = u
                v.AlghoritmData.PredecessorEdge = h
                q.Remove(v)
                q.Push(v)
            End If
        End Sub
#End Region
#End Region
    End Class
    ''' <summary>Data used by graph alghoritm</summary>
    Public Class GraphAlghoritmData

        ''' <summary>Contains value of the <see cref="DoubleDistance"/> property</summary>
        Private _DoubleDistance As Double
        ''' <summary>Contains value of the <see cref="IntegerDistance"/> property</summary>
        Private _IntegerDistance As Integer
        ''' <summary>Contains value of the <see cref="PredecessorEdge"/> property</summary>
        Private _PredecessorEdge As GraphEdge
        ''' <summary>Contains value of the <see cref="Predecessor"/> property</summary>
        Private _Predecessor As GraphNode
        ''' <summary>Contains value of the <see cref="State"/> property</summary>
        Private _State As GraphNodeState
        ''' <summary>Contains value of the <see cref="OpenMark"/> property</summary>
        Private _OpenMark As Integer
        ''' <summary>Contains value of the <see cref="CloseMark"/> property</summary>
        Private _CloseMark As Integer
        ''' <summary>If used by alghoritm contains node close time-stam as integer</summary>
        Public Property CloseMark() As Integer
            Get
                Return _CloseMark
            End Get
            Set(ByVal value As Integer)
                _CloseMark = value
            End Set
        End Property
        ''' <summary>If used by alghoritm contains distance of node from start of searching as double value</summary>
        Public Property DoubleDistance() As Double
            Get
                Return _DoubleDistance
            End Get
            Set(ByVal value As Double)
                _DoubleDistance = value
            End Set
        End Property
        ''' <summary>If used by alghoritm contains ditance of node from start of searching as integer</summary>
        Public Property IntegerDistance() As Integer
            Get
                Return _IntegerDistance
            End Get
            Set(ByVal value As Integer)
                _IntegerDistance = value
            End Set
        End Property
        ''' <summary>If used by alghoritm contains node open time-stam as integer</summary>
        Public Property OpenMark() As Integer
            Get
                Return _OpenMark
            End Get
            Set(ByVal value As Integer)
                _OpenMark = value
            End Set
        End Property
        ''' <summary>If used by alghoritm contains node predecessor (or parent) in alghoritm-deined structure</summary>
        Public Property Predecessor() As GraphNode
            Get
                Return _Predecessor
            End Get
            Set(ByVal value As GraphNode)
                _Predecessor = value
            End Set
        End Property
        ''' <summary>If used by alghoritm contains edge from <see cref="Predecessor"/> to current node</summary>
        Public Property PredecessorEdge() As GraphEdge
            Get
                Return _PredecessorEdge
            End Get
            Set(ByVal value As GraphEdge)
                _PredecessorEdge = value
            End Set
        End Property
        ''' <summary>If used by alghoritm contains node state during alghoritm run</summary>
        ''' <value>Default value if <see cref="GraphNodeState.Fresh"/>. When alghoritm finishes the value should be <see cref="GraphNodeState.Closed"/> (if alghoritm uses this property)</value>
        Public Property State() As GraphNodeState
            Get
                Return _State
            End Get
            Set(ByVal value As GraphNodeState)
                _State = value
            End Set
        End Property
    End Class
    ''' <summary>Possible states of graph node while alghoritm is running</summary>
    Public Enum GraphNodeState
        ''' <summary>The nodes is fresh (never visited)</summary>
        Fresh
        ''' <summary>The node is open (currently being processed)</summary>
        Open
        ''' <summary>The node is closed (already processed)</summary>
        Closed
    End Enum

    ''' <summary>Exception thrown when <see cref="Graph"/> is in invalid for specific alghoritm</summary>
    Public Class InvalidGraphException
        Inherits InvalidOperationException
        ''' <summary>Initializes a new instance of the <see cref="System.InvalidOperationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        ''' <param name="message">The error message that explains the reason for the exception.</param>
        ''' <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception.</param>
        Public Sub New(ByVal Message As String, Optional ByVal InnerException As Exception = Nothing)
            MyBase.New(Message, InnerException)
        End Sub
    End Class
End Namespace
#End If