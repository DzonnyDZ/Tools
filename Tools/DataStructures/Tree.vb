Imports Tools.Collections.Generic, Tools.ComponentModel
Namespace DataStructures
#If Config <= Nightly Then 'Stage:Nightly
    'ASAP:Wiki & forum
    'TODO:In-oder, Pre-order and post-order enumerators
    ''' <summary>Represents tree or sub-tree</summary>
    <Author("Ðonny", eMail:="dzonny@dzonny.cz", WWW:="http://dzonny.cz")> _
    <Version(1, 0, GetType(Tree(Of DBNull)), LastChMMDDYYYY:="05/19/2007")> _
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
            <DebuggerStepperBoundary()> Get
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
        Protected Overridable Sub OnAdding(ByVal e As ListWithEvents(Of Tree(Of T)).CancelableItemIndexEventArgs)
            If e Is Nothing Then Throw New NullReferenceException("Cannot add null node into tree")
            If e.Item.Parent IsNot Nothing Then
                e.Cancel = True
                e.CancelMessage = "Cannot add node that have parent already set"
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
        Protected Overridable Sub OnItemChanging(ByVal e As ListWithEvents(Of Tree(Of T)).CancelableItemIndexEventArgs)
            If e Is Nothing Then Throw New NullReferenceException("Cannot put null node into tree")
            If e.Item.Parent IsNot Nothing AndAlso Nodes(e.NewIndex) IsNot e.Item Then
                e.Cancel = True
                e.CancelMessage = "Cannot assign node that gave parent already set"
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
    End Class
#End If
End Namespace
