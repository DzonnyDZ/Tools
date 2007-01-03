#If Config <= Nightly Then
Namespace Collections.Generic
    <Author("Ðonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(ListWithEvents(Of String)), LastChange:="1/3/2007")> _
    Public Class ListWithEvents(Of T) : Implements IList(Of T)
        Private List As List(Of T)
        ''' <summary>Contains value of the <see cref="AddingReadOnly"/> property</summary>
        Private _AddingReadOnly As Boolean
        ''' <summary>Determines <see cref="CancelableItemEventArgs.[ReadOnly]"/> property value for the <see cref="Adding"/> events</summary>
        Public ReadOnly Property AddingReadOnly() As Boolean
            Get
                Return _AddingReadOnly
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="AllowAddCancelableEventsHandlers"/> property</summary>
        Private _AllowAddCancelableEventsHandlers As Boolean = True
        ''' <summary>Determines if it is allowed to add handlers for events that supports cancellation</summary>
        ''' <remarks>
        ''' <list>
        ''' <listheader>Those are events:</listheader>
        ''' <item><see cref="Adding"/></item>
        ''' </list>
        ''' </remarks>
        Public Property AllowAddCancelableEventsHandlers() As Boolean
            Get
                Return _AllowAddCancelableEventsHandlers
            End Get
            Set(ByVal value As Boolean)
                If value = False Then
                    _AllowAddCancelableEventsHandlers = value
                ElseIf value = True AndAlso AllowAddCancelableEventsHandlers = False Then
                    Throw New InvalidOperationException("AllowAddCancelableEventsHandlers  can be changed only from True to False")
                End If
            End Set
        End Property

#Region "Add"
        ''' <summary>List of <see cref="ItemCancelEventHandler"/> delegates to be invoked when the <see cref="Adding"/> event is raised</summary>
        Private AddingEventHandlerList As New List(Of ItemCancelEventHandler)
        ''' <summary>Delegate of handler of <see cref="Adding"/> and <see cref="Removing"/> events</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        Public Delegate Sub ItemCancelEventHandler(ByVal sender As ListWithEvents(Of T), ByVal e As CancelableItemIndexEventArgs)
        ''' <summary>Raised before an item is added to the list</summary>
        ''' <remarks>
        ''' This event can be canceled (see <see cref="AllowAddCancelableEventsHandlers"/>.
        ''' This means then <see cref="InvalidOperationException"/> is thrown when adding handler and <see cref="AllowAddCancelableEventsHandlers"/> is False.
        ''' </remarks>
        Public Custom Event Adding As ItemCancelEventHandler
            AddHandler(ByVal value As ItemCancelEventHandler)
                If AllowAddCancelableEventsHandlers Then
                    AddingEventHandlerList.Add(value)
                Else
                    Throw New InvalidOperationException("Cannot add handler to the Adding event when AllowAddCancelableEventsHandlers is False")
                End If
            End AddHandler
            <DebuggerStepThrough()> _
            RemoveHandler(ByVal value As ItemCancelEventHandler)
                AddingEventHandlerList.Remove(value)
            End RemoveHandler
            RaiseEvent(ByVal sender As ListWithEvents(Of T), ByVal e As ListWithEvents(Of T).CancelableItemIndexEventArgs)
                For Each Handler As ItemCancelEventHandler In AddingEventHandlerList
                    Handler.Invoke(sender, e)
                Next Handler
            End RaiseEvent
        End Event
        ''' <summary>Raised after an item is added to the list</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        Public Event Added(ByVal sender As ListWithEvents(Of T), ByVal e As ItemIndexEventArgs)

        ''' <summary>Adds an item to the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <param name="item">The object to add to the <see cref="ListWithEvents(Of T)"/>.</param>
        ''' <remarks>Note for inheritors: Call <see cref="OnAdding"/> before adding an item to the list and <see cref="OnAdded"/> after adding item to the list, do not forgot to check <see cref="CancelableItemIndexEventArgs.Cancel"/></remarks>
        Public Overridable Sub Add(ByVal item As T) Implements System.Collections.Generic.ICollection(Of T).Add
            Dim e As New CancelableItemIndexEventArgs(item, List.Count, AddingReadOnly)
            OnAdding(e)
            If Not e.Cancel Then
                List.Add(item)
                OnAdded(New ItemIndexEventArgs(item, List.Count - 1))
            End If
        End Sub
        ''' <summary>Inserts an item to the <see cref="ListWithEvents(Of T)"/> at the specified index.</summary>
        ''' <param name="item">The object to insert into the <see cref="ListWithEvents(Of T)"/>.</param>
        ''' <param name="index">The zero-based index at which item should be inserted.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="ListWithEvents(Of T)"/>.</exception>
        ''' <remarks>Note for inheritors: Call <see cref="OnAdding"/> before adding an item to the list and <see cref="OnAdded"/> after adding item to the list, do not forgot to check <see cref="CancelableItemIndexEventArgs.Cancel"/></remarks>
        Public Overridable Sub Insert(ByVal index As Integer, ByVal item As T) Implements System.Collections.Generic.IList(Of T).Insert
            Dim e As New CancelableItemIndexEventArgs(item, index, AddingReadOnly)
            OnAdding(e)
            If Not e.Cancel Then
                List.Add(item)
                OnAdded(New ItemIndexEventArgs(item, index))
            End If
        End Sub
        ''' <summary>Raises <see cref="Adding"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnAdding"/> in order the event to be raised</remarks>
        Protected Overridable Sub OnAdding(ByVal e As CancelableItemIndexEventArgs)
            RaiseEvent Adding(Me, e)
        End Sub
        ''' <summary>Raises <see cref="Added"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnAdded"/> in order the event to be raised</remarks>
        Protected Overridable Sub OnAdded(ByVal e As ItemIndexEventArgs)
            RaiseEvent Added(Me, e)
        End Sub
#End Region
#Region "Clear"
        ''' <summary>List of <see cref="ClearingEventHandler"/> delegates to be invoked when the <see cref="Clearing"/> event is raised</summary>
        Private ClearingEventHandlerList As New List(Of ClearingEventHandler)
        ''' <summary>Delegate of handler of <see cref="Clearing"/> event</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        Public Delegate Sub ClearingEventHandler(ByVal sender As ListWithEvents(Of T), ByVal e As CancelEventArgs)
        ''' <summary>Raised before the list is cleared</summary>
        ''' <remarks>
        ''' This event can be canceled (see <see cref="AllowAddCancelableEventsHandlers"/>.
        ''' This means then <see cref="InvalidOperationException"/> is thrown when adding handler and <see cref="AllowAddCancelableEventsHandlers"/> is False.
        ''' </remarks>
        Public Custom Event Clearing As ClearingEventHandler
            AddHandler(ByVal value As ClearingEventHandler)
                If AllowAddCancelableEventsHandlers Then
                    ClearingEventHandlerList.Add(value)
                Else
                    Throw New InvalidOperationException("Cannot add handler to the Clearig event when AllowAddCancelableEventsHandlers is False")
                End If
            End AddHandler
            <DebuggerStepThrough()> _
            RemoveHandler(ByVal value As ClearingEventHandler)
                ClearingEventHandlerList.Remove(value)
            End RemoveHandler
            RaiseEvent(ByVal sender As ListWithEvents(Of T), ByVal e As CancelEventArgs)
                For Each Handler As ClearingEventHandler In ClearingEventHandlerList
                    Handler.Invoke(sender, e)
                Next Handler
            End RaiseEvent
        End Event
        ''' <summary>Raised after the list is cleared</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        Public Event Cleared(ByVal sender As ListWithEvents(Of T), ByVal e As CountEventArgs)
        ''' <summary>Removes all items from the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <remarks>Note for inheritors: Call <see cref="OnClearing"/> before clearing of the list and <see cref="OnCleared"/> after clearing of the list,, do not forgot to check <see cref="CancelEventArgs.Cancel"/></remarks>
        Public Overridable Sub Clear() Implements System.Collections.Generic.ICollection(Of T).Clear
            Dim e As New CancelEventArgs
            OnClearing(e)
            If Not e.Cancel Then
                Dim e2 As New CountEventArgs(List.Count)
                List.Clear()
                OnCleared(e2)
            End If
        End Sub
        ''' <summary>Raises <see cref="Clearing"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnClearing"/> in order the event to be raised</remarks>
        Protected Overridable Sub OnClearing(ByVal e As CancelEventArgs)
            RaiseEvent Clearing(Me, e)
        End Sub
        ''' <summary>Raises <see cref="Cleared"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnCleared"/> in order the event to be raised</remarks>
        Protected Overridable Sub OnCleared(ByVal e As CountEventArgs)
            RaiseEvent Cleared(Me, e)
        End Sub
#End Region
        ''' <summary>Determines whether the <see cref="ListWithEvents(Of T)"/> contains a specific value.</summary>
        ''' <param name="item">The object to locate in the <see cref="ListWithEvents(Of T)"/>.</param>
        ''' <returns>true if item is found in the <see cref="ListWithEvents(Of T)"/>; otherwise, false.</returns>
        Public Overridable Function Contains(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Contains
            Return List.Contains(item)
        End Function
        ''' <summary>Copies the elements of the <see cref="ListWithEvents(Of T)"/> to an <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from <see cref="ListWithEvents(Of T)"/>. The <see cref="System.Array"/> must have zero-based indexing.</param>
        ''' <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        ''' <exception cref="System.ArgumentNullException">array is null.</exception>
        ''' <exception cref="System.ArgumentException">array is multidimensional.-or-arrayIndex is equal to or greater than the length of array.-or-The number of elements in the source System.Collections.Generic.ICollection(Of T) is greater than the available space from arrayIndex to the end of the destination array.-or-Type T cannot be cast automatically to the type of the destination array.</exception>
        Public Overridable Sub CopyTo(ByVal array() As T, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of T).CopyTo
            List.CopyTo(array, arrayIndex)
        End Sub
        ''' <summary>Gets the number of elements contained in the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <returns>The number of elements contained in the <see cref="ListWithEvents(Of T)"/>.</returns>
        Public Overridable ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of T).Count
            Get
                Return List.Count
            End Get
        End Property
        ''' <summary>Gets a value indicating whether the <see cref="ListWithEvents(Of T)"/> is read-only (always false).</summary>
        ''' <returns>Always false because <see cref="ListWithEvents(Of T)"/> is not read-only</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of T).IsReadOnly
            Get
                Return False
            End Get
        End Property
#Region "Remove"
        ''' <summary>List of <see cref="ItemCancelEventHandler"/> delegates to be invoked when the <see cref="Removing"/> event is raised</summary>
        Private RemovingEventHandlerList As New List(Of ItemCancelEventHandler)
        ''' <summary>Raised before item is removed from the list</summary>
        ''' <remarks>
        ''' This event can be canceled (see <see cref="AllowAddCancelableEventsHandlers"/>.
        ''' This means then <see cref="InvalidOperationException"/> is thrown when adding handler and <see cref="AllowAddCancelableEventsHandlers"/> is False.
        ''' Do not change content of list in handler
        ''' </remarks>
        Public Custom Event Removing As ItemCancelEventHandler
            AddHandler(ByVal value As ItemCancelEventHandler)
                If AllowAddCancelableEventsHandlers Then
                    RemovingEventHandlerList.Add(value)
                Else
                    Throw New InvalidOperationException("Cannot add handler to the Removing event when AllowAddCancelableEventsHandlers is False")
                End If
            End AddHandler
            <DebuggerStepThrough()> _
            RemoveHandler(ByVal value As ItemCancelEventHandler)
                RemovingEventHandlerList.Remove(value)
            End RemoveHandler
            RaiseEvent(ByVal sender As ListWithEvents(Of T), ByVal e As CancelableItemIndexEventArgs)
                For Each Handler As ItemCancelEventHandler In RemovingEventHandlerList
                    Handler.Invoke(sender, e)
                Next Handler
            End RaiseEvent
        End Event
        ''' <summary>Raised after the list is cleared</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        Public Event Removed(ByVal sender As ListWithEvents(Of T), ByVal e As ItemIndexEventArgs)
        ''' <summary>Raises <see cref="Removing"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>
        ''' Note for inheritors: Always call base class method <see cref="OnRemoving"/> in order the event to be raised
        ''' Do not change content of list in this method
        ''' </remarks>
        Protected Overridable Sub OnRemoving(ByVal e As CancelableItemIndexEventArgs)
            RaiseEvent Removing(Me, e)
        End Sub
        ''' <summary>Raises <see cref="Removed"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnRemoved"/> in order the event to be raised</remarks>
        Protected Overridable Sub OnRemoved(ByVal e As ItemIndexEventArgs)
            RaiseEvent Removed(Me, e)
        End Sub
        ''' <summary>Removes the first occurrence of a specific object from the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <param name="item">The object to remove from the <see cref="ListWithEvents(Of T)"/>.</param>
        ''' <returns>true if item was successfully removed from the <see cref="ListWithEvents(Of T)"/>; otherwise, false. This method also returns false if item is not found in the original <see cref="ListWithEvents(Of T)"/>.</returns>
        ''' <remarks>Note for inheritors: Call <see cref="OnRemoving"/> before removing item and <see cref="OnRemoved"/> after removing item, do not forgot to check <see cref="CancelableItemIndexEventArgs.Cancel"/></remarks>
        Public Overridable Function Remove(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Remove
            If Contains(item) Then
                Dim e As New CancelableItemIndexEventArgs(item, IndexOf(item), True)
                OnRemoving(e)
                If Not e.Cancel Then
                    Dim i As Integer = IndexOf(item)
                    If List.Remove(item) Then
                        OnRemoved(New ItemIndexEventArgs(item, i))
                        Return True
                    Else
                        Return False
                    End If
                End If
            Else
                Return False
            End If
        End Function
        ''' <summary>Removes the <see cref="ListWithEvents(Of T)"/> item at the specified index.</summary>
        ''' <param name="index">The zero-based index of the item to remove.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="ListWithEvents(Of T)"/>.</exception>
        ''' <remarks>Note for inheritors: Call <see cref="OnRemoving"/> before removing item and <see cref="OnRemoved"/> after removing item, do not forgot to check <see cref="CancelableItemIndexEventArgs.Cancel"/></remarks>
        Public Overridable Sub RemoveAt(ByVal index As Integer) Implements System.Collections.Generic.IList(Of T).RemoveAt
            Dim e As New CancelableItemIndexEventArgs(Me(index), index, True)
            If index >= 0 AndAlso index < Count Then
                OnRemoving(e)
            End If
            If Not e.Cancel Then
                Dim itm As T = Me(index)
                List.RemoveAt(index)
                OnRemoved(New ItemIndexEventArgs(itm, index))
            End If
        End Sub
#End Region
        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="System.Collections.Generic.IEnumerator(Of T)"/> that can be used to iterate through the collection.</returns>
        Public Overridable Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
            Return List.GetEnumerator
        End Function
        ''' <summary>Returns an enumerator that iterates through a collection.</summary>
        ''' <returns>An <see cref="System.Collections.IEnumerator"/> object that can be used to iterate through the collection.</returns>
        <EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use type-safe GetEnumerator instead")> _
        Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return List.GetEnumerator
        End Function
        ''' <summary>Determines the index of a specific item in the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <param name="item">The object to locate in the <see cref="ListWithEvents(Of T)"/>.</param>
        ''' <returns>The index of item if found in the list; otherwise, -1.</returns>
        Public Overridable Function IndexOf(ByVal item As T) As Integer Implements System.Collections.Generic.IList(Of T).IndexOf
            Return List.IndexOf(item)
        End Function
        Default Public Overridable Property Item(ByVal index As Integer) As T Implements System.Collections.Generic.IList(Of T).Item
            Get
                Return List(index)
            End Get
            Set(ByVal value As T)
                'TODO:
            End Set
        End Property
        ''' <summary>Gives access to underlying <see cref="List(Of T)"/></summary>
        Protected ReadOnly Property InternalList() As List(Of T)
            Get
                Return List
            End Get
        End Property
        ''' <summary>Gives read-only access to underlying <see cref="List(Of T)"/></summary>
        Public ReadOnly Property AsReadOnly() As IReadOnlyList(Of T)
            Get
                Return New ReadOnlyListAdapter(Of T)(List)
            End Get
        End Property
#Region "EventArgs"
        ''' <summary>Parameter of cancelable item events</summary>
        Public Class CancelableItemEventArgs : Inherits CancelEventArgs
            ''' <summary>Contains value of the <see cref="Item"/> property</summary>
            Private _Item As T
            ''' <summary>Contains value of the <see cref="[ReadOnly]"/> property</summary>
            Private _ReadOnly As Boolean
            ''' <summary>CTor</summary>
            ''' <param name="Item">Item associated with current event</param>
            ''' <param name="ReadOnly">True to disallow changing of the <see cref="Item"/> property</param>
            Public Sub New(ByVal Item As T, Optional ByVal [ReadOnly] As Boolean = False)
                _Item = Item
                _ReadOnly = [ReadOnly]
            End Sub
            ''' <summary>Item associated with current event</summary>
            ''' <exception cref="system.Data.ReadOnlyException">Using setter when <see cref="[ReadOnly]"/> is True</exception>
            Public Property Item() As T
                Get
                    Return _Item
                End Get
                Set(ByVal value As T)
                    If Not [ReadOnly] Then
                        _Item = value
                    Else
                        Throw New System.Data.ReadOnlyException("Cannot change Item property when ReadOnly is True")
                    End If
                End Set
            End Property
            ''' <summary>Indicates if this instance's property <see cref="Item"/> is read-only or not</summary>
            Public ReadOnly Property [ReadOnly]() As Boolean
                Get
                    Return _ReadOnly
                End Get
            End Property
        End Class
        ''' <summary>Argument of <see cref="Adding"/> event</summary>
        Public Class CancelableItemIndexEventArgs : Inherits CancelableItemEventArgs
            ''' <summary>CTor</summary>
            ''' <param name="Item">Item associated with current event</param>
            ''' <param name="ReadOnly">True to disallow changing of the <see cref="Item"/> property</param>
            ''' <param name="Index">Index of newly added item</param>
            Public Sub New(ByVal Item As T, ByVal Index As Integer, Optional ByVal [ReadOnly] As Boolean = False)
                MyBase.New(Item, [ReadOnly])
                NewIndex = Index
            End Sub
            ''' <summary>Index of newly added item</summary>
            ''' <remarks>The index may be invalid when collecion-manipulation is done between raising <see cref="Adding"/> event and performing <see cref="List(Of T).Add"/> operation on underlying <see cref="List(Of T)"/>. (it's always valid when performing <see cref="List(Of T).Insert"/> - event raised through <see cref="Insert"/>.)</remarks>
            Public ReadOnly NewIndex As Integer
        End Class

        ''' <summary>Parameter od non-cancelable item events</summary>
        Public Class ItemEventArgs : Inherits EventArgs
            ''' <summary>CTor</summary>
            ''' <param name="Item">Item associated with current event</param>
            Public Sub New(ByVal Item As T)
                Me.Item = Item
            End Sub
            ''' <summary>Item associated with current event</summary>
            Public ReadOnly Item As T
        End Class
        ''' <summary>Parameter of the <see cref="Added"/> event</summary>
        Public Class ItemIndexEventArgs : Inherits ItemEventArgs
            ''' <summary>Index ow newly added item</summary>
            Public ReadOnly Index As Integer
            ''' <summary>CTor</summary>
            ''' <param name="Item">Newly added item</param>
            ''' <param name="index">Index of newly added item</param>
            Public Sub New(ByVal Item As T, ByVal index As Integer)
                MyBase.New(Item)
                Me.Index = index
            End Sub
        End Class
        ''' <summary>Parameter of events that reports count</summary>
        Public Class CountEventArgs : Inherits EventArgs
            ''' <summary>CTor</summary>
            ''' <param name="Count">Count to be reported</param>
            Public Sub New(ByVal Count As Integer)
                Me.Count = Count
            End Sub
            ''' <summary>Reported count</summary>
            Public ReadOnly Count As Integer
        End Class

#End Region
    End Class
End Namespace
#End If