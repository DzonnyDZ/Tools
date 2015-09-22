Imports System.ComponentModel.Design.Serialization, Tools.ComponentModelT, Tools.VisualBasicT, Tools.ExtensionsT
Imports System.Collections.Specialized
Imports System.Data
Imports System.Runtime.Serialization
Imports System.Security.Permissions
Imports Tools.ResourcesT

#If True Then
Namespace CollectionsT.GenericT
    ''' <summary>Common non-generic base class for all instance of <see cref="ListWithEvents(Of T)"/></summary>
    ''' <remarks>This class is not intended to be inherited by anything else than <see cref="ListWithEvents(Of T)"/>.
    ''' <para>Althought members f this abstract class are provided with documentation, it may be misleading. ALways study cocumentation of derived class <see cref="ListWithEvents(Of T)"/>.</para></remarks>
    ''' <version version="1.5.2">Class introduced</version>
    ''' <version version="1.5.3">Added implementation for the <see cref="INotifyCollectionChanged"/> interface</version>
    <EditorBrowsable(EditorBrowsableState.Advanced), Serializable()>
    Public MustInherit Class ListWithEventsBase
        Implements IList, IReportsChange, IBindingList, IEnumerable, INotifyPropertyChanged, INotifyCollectionChanged
        ''' <summary>CTor</summary>
        Friend Sub New()
        End Sub
        ''' <summary>When overridden in derived class represents custom property wher owner of the list can be stored to provide bi-directional reference</summary>
        ''' <remarks>Change of this property is reported through <see cref="IReportsChange.Changed"/>.
        ''' <para>This property is here for convenience, <see cref="ListWithEvents(Of T)"/> does not utilize it.</para>
        ''' <para>Change of this property is reported via <see cref="INotifyPropertyChanged.PropertyChanged"/> and <see cref="IReportsChange.Changed"/>.</para></remarks>
        MustOverride Property Owner() As Object

        ''' <summary>When overridden in derived class copies the elements of the <see cref="System.Collections.ICollection"/> to an <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from <see cref="System.Collections.ICollection"/>. The <see cref="System.Array"/> must have zero-based indexing.</param>
        ''' <param name="index">The zero-based index in array at which copying begins.</param>
        ''' <exception cref="System.ArgumentNullException">array is null.</exception>
        ''' <exception cref="System.ArgumentOutOfRangeException">index is less than zero.</exception>
        ''' <exception cref="System.ArgumentException">array is multidimensional.-or- index is equal to or greater than the length of array.-or- The number of elements in the source <see cref="System.Collections.ICollection"/> is greater than the available space from index to the end of the destination array.</exception>
        ''' <exception cref="System.InvalidCastException">The type of the source <see cref="System.Collections.ICollection"/> cannot be cast automatically to the type of the destination array.</exception>
        ''' <remarks>Do not use, use type-safe <see cref="ListWithEvents(Of T).CopyTo"/> instead. Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> Public MustOverride Sub ICollection_CopyTo(array As Array, index As Integer) Implements ICollection.CopyTo

        ''' <summary>When overridden in derived class gets the number of elements contained in the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <returns>The number of elements contained in the <see cref="ListWithEvents(Of T)"/>.</returns>
        ''' <remarks>Change of this property is reported via <see cref="INotifyPropertyChanged.PropertyChanged"/>.</remarks>
        Public MustOverride ReadOnly Property Count() As Integer Implements ICollection.Count

        ''' <summary>When overridden in derived class gets a value indicating whether access to the <see cref="System.Collections.ICollection"/> is synchronized (thread safe).</summary>
        ''' <returns>true if access to the <see cref="System.Collections.ICollection"/> is synchronized (thread safe); otherwise, false.</returns>
        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> Public MustOverride ReadOnly Property IsSynchronized() As Boolean Implements ICollection.IsSynchronized

        ''' <summary>When overridden in derived class gets an object that can be used to synchronize access to the <see cref="System.Collections.ICollection"/>.</summary>
        ''' <returns>An object that can be used to synchronize access to the <see cref="System.Collections.ICollection"/></returns>
        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> Public MustOverride ReadOnly Property SyncRoot() As Object Implements ICollection.SyncRoot

        ''' <summary>When overridden in derived class returns an enumerator that iterates through a collection.</summary>
        ''' <returns>An <see cref="System.Collections.IEnumerator"/> object that can be used to iterate through the collection.</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> Public MustOverride Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator

        ''' <summary>When overridden in derived class adds an item to the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <param name="value">The object to add to the <see cref="ListWithEvents(Of T)"/>.</param>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <exception cref="OperationCanceledException">Operation is canceled in event handler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="ListWithEvents.Adding"/> event -or- Any exception may be thrown by <see cref="ICollectionCancelItem.OnAdding"/> when <see cref="AllowItemCancel"/> is true.</exception>
        ''' <version version="1.5.3">Method renamed from <c>Add</c> to <c>IList_Add</c> to avoid compiler from selecting this method and complaining that it is obsolete (on <see cref="ListWithEvents(Of T)"/>) when <see cref="ListWithEvents(Of T)"/>[<see cref="Object"/>] is used.</version>
        <EditorBrowsable(EditorBrowsableState.Never)> Public MustOverride Function IList_Add(value As Object) As Integer Implements IList.Add

        ''' <summary>When overridden in derived class removes all items from the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <exception cref="OperationCanceledException">Operation is canceled in event handler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="ListWithEvents.Clearing"/> event -or- Any exception can be thrown by <see cref="ICollectionCancelItem.OnClearing"/> when <see cref="AllowItemCancel"/> is true.</exception>
        Public MustOverride Sub Clear() Implements IList.Clear

        ''' <summary>When overridden in derived class determines whether the <see cref="System.Collections.IList"/> contains a specific value.</summary>
        ''' <param name="value">The <see cref="System.Object"/> to locate in the <see cref="System.Collections.IList"/>.</param>
        ''' <returns>true if the <see cref="System.Object"/> is found in the <see cref="System.Collections.IList"/>; otherwise, false.</returns>
        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        Public MustOverride Function Contains(value As Object) As Boolean Implements IList.Contains

        ''' <summary>When overridden in derived class determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
        ''' <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
        ''' <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />. </param>
        Public MustOverride Function IndexOf(value As Object) As Integer Implements IList.IndexOf

        ''' <summary>When overridden in derived class inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.</summary>
        ''' <param name="index">The zero-based index at which <paramref name="value" /> should be inserted. </param>
        ''' <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />. </param>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"> <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />. </exception>
        ''' <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList" /> is read-only.-or- The <see cref="T:System.Collections.IList" /> has a fixed size. </exception>
        ''' <exception cref="T:System.NullReferenceException"><paramref name="value" /> is null reference in the <see cref="T:System.Collections.IList" />.</exception>
        <EditorBrowsable(EditorBrowsableState.Never)> Public MustOverride Sub Insert(index As Integer, value As Object) Implements IList.Insert

        ''' <summary>When overridden in derived class gets a value indicating whether the <see cref="System.Collections.IList"/> has a fixed size.</summary>
        ''' <returns>Always False</returns>
        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        Public MustOverride ReadOnly Property IsFixedSize() As Boolean Implements IList.IsFixedSize

        ''' <summary>When overridden in derived class gets a value indicating whether the <see cref="ListWithEvents(Of T)"/> is read-only (always false).</summary>
        ''' <returns>Always false because <see cref="ListWithEvents(Of T)"/> is not read-only</returns>
        Public MustOverride ReadOnly Property IsReadOnly() As Boolean Implements IList.IsReadOnly

        ''' <summary>When overridden in derived class gets or sets the element at the specified index.</summary>
        ''' <param name="index">The zero-based index of the element to get or set.</param>
        ''' <returns>The element at the specified index.</returns>
        ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="System.Collections.IList"/>.</exception>
        ''' <exception cref="TypeMismatchException">When setting value that cannot be converted to <typeparamref name="T"/></exception>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True (in setter)</exception>
        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> Public MustOverride Property IList_Item(index As Integer) As Object Implements IList.Item

        ''' <summary>When overridden in derived class removes the first occurrence of a specific object from the <see cref="System.Collections.IList"/>.</summary>
        ''' <param name="value">The <see cref="System.Object"/> to remove from the <see cref="System.Collections.IList"/></param>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> Public MustOverride Sub Remove(value As Object) Implements IList.Remove

        ''' <summary>When overridden in derived class removes the <see cref="ListWithEvents(Of T)"/> item at the specified index.</summary>
        ''' <param name="index">The zero-based index of the item to remove.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="ListWithEvents(Of T)"/>.</exception>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <exception cref="OperationCanceledException">Operation is canceled in event handler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="ListWithEvents(Of T).Removing"/> event -or- Any exception may be thrown by <see cref="ICollectionCancelItem.OnRemoving"/> when <see cref="AllowItemCancel"/> is true.</exception>
        Public MustOverride Sub RemoveAt(index As Integer) Implements IList.RemoveAt

        ''' <summary>When overridden in derived class adds the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the indexes used for searching.</summary>
        ''' <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the indexes used for searching. </param>
        <EditorBrowsable(EditorBrowsableState.Never)> Protected MustOverride Sub AddIndex([property] As PropertyDescriptor) Implements IBindingList.AddIndex

        ''' <summary>When overridden in derived class adds a new item to the list.</summary>
        ''' <returns>The item added to the list.</returns>
        ''' <exception cref="T:System.NotSupportedException"><see cref="P:System.ComponentModel.IBindingList.AllowNew" /> is false. </exception>
        <EditorBrowsable(EditorBrowsableState.Never)> Protected MustOverride Function IBindingList_AddNew() As Object Implements IBindingList.AddNew

        ''' <summary>When overridden in derived class gets whether you can update items in the list.</summary>
        ''' <returns>true if you can update the items in the list; otherwise, false.</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> Protected MustOverride ReadOnly Property AllowEdit() As Boolean Implements IBindingList.AllowEdit

        ''' <summary>When overridden in derived class gets whether you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
        ''' <returns>true if you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew" />; otherwise, false.</returns>
        Public MustOverride ReadOnly Property CanAddNew() As Boolean Implements IBindingList.AllowNew

        ''' <summary>When overridden in derived class gets whether you can remove items from the list, using <see cref="M:System.Collections.IList.Remove(System.Object)" /> or <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
        ''' <returns>true if you can remove items from the list; otherwise, false.</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> Protected MustOverride ReadOnly Property AllowRemove() As Boolean Implements IBindingList.AllowRemove
        ''' <summary>When overridden in derived class sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor" /> and a <see cref="T:System.ComponentModel.ListSortDirection" />.</summary>
        ''' <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to sort by. </param>
        ''' <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values. </param>
        ''' <exception cref="T:System.NotSupportedException"><see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
        <EditorBrowsable(EditorBrowsableState.Never)> Protected MustOverride Sub ApplySort([property] As PropertyDescriptor, direction As ListSortDirection) Implements IBindingList.ApplySort

        ''' <summary>When overridden in derived class returns the index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
        ''' <returns>The index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
        ''' <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search on. </param>
        ''' <param name="key">The value of the 
        ''' <paramref name="property" /> parameter to search for. </param>
        ''' <exception cref="T:System.NotSupportedException"><see cref="P:System.ComponentModel.IBindingList.SupportsSearching" /> is false. </exception>
        <EditorBrowsable(EditorBrowsableState.Never)> Protected MustOverride Function Find([property] As PropertyDescriptor, key As Object) As Integer Implements IBindingList.Find

        ''' <summary>When overridden in derived class gets whether the items in the list are sorted.</summary>
        ''' <returns>true if <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" /> has been called and <see cref="M:System.ComponentModel.IBindingList.RemoveSort" /> has not been called; otherwise, false.</returns>
        ''' <exception cref="T:System.NotSupportedException"><see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
        <EditorBrowsable(EditorBrowsableState.Never)> Protected MustOverride ReadOnly Property IsSorted() As Boolean Implements IBindingList.IsSorted

        ''' <summary>Occurs when the list changes or an item in the list changes.</summary>
        Public Event ListChanged As ListChangedEventHandler Implements IBindingList.ListChanged

        ''' <summary>Raises the <see cref="ListChanged"/> event</summary>
        ''' <param name="e">event arguments</param>
        Protected Overridable Sub OnListChanged(e As ListChangedEventArgs)
            RaiseEvent ListChanged(Me, e)
        End Sub

        ''' <summary>When overridden in derived class removes the <see cref="T:System.ComponentModel.PropertyDescriptor" /> from the indexes used for searching.</summary>
        ''' <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the indexes used for searching. </param>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected MustOverride Sub RemoveIndex([property] As PropertyDescriptor) Implements IBindingList.RemoveIndex

        ''' <summary>When overridden in derived class removes any sort applied using <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />.</summary>
        ''' <exception cref="T:System.NotSupportedException"><see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected MustOverride Sub RemoveSort() Implements IBindingList.RemoveSort

        ''' <summary>When overridden in derived class gets the direction of the sort.</summary>
        ''' <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</returns>
        ''' <exception cref="T:System.NotSupportedException"><see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected MustOverride ReadOnly Property SortDirection() As ListSortDirection Implements IBindingList.SortDirection

        ''' <summary>When overridden in derived class gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting.</summary>
        ''' <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting.</returns>
        ''' <exception cref="T:System.NotSupportedException"><see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is false. </exception>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected MustOverride ReadOnly Property SortProperty() As PropertyDescriptor Implements IBindingList.SortProperty

        ''' <summary>When overridden in derived class gets whether a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or an item in the list changes.</summary>
        ''' <returns>true if a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or when an item changes; otherwise, false.</returns>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected MustOverride ReadOnly Property SupportsChangeNotification() As Boolean Implements IBindingList.SupportsChangeNotification

        ''' <summary>When overridden in derived class gets whether the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method.</summary>
        ''' <returns>true if the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method; otherwise, false.</returns>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected MustOverride ReadOnly Property SupportsSearching() As Boolean Implements IBindingList.SupportsSearching

        ''' <summary>When overridden in derived class gets whether the list supports sorting.</summary>
        ''' <returns>true if the list supports sorting; otherwise, false.</returns>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected MustOverride ReadOnly Property SupportsSorting() As Boolean Implements IBindingList.SupportsSorting

        ''' <summary>Raised when value of member changes</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">
        '''     Event information.
        '''     As of this implementation type of <paramref name="e"/> is always one of following types:
        '''     <see cref="ListWithEvents(Of T).ItemIndexEventArgs"/> (<see cref="ListWithEvents(Of T).Added"/>),
        '''     <see cref="ListWithEvents(Of T).ItemsEventArgs"/> (<see cref="ListWithEvents(Of T).Cleared"/>), 
        '''     <see cref="ListWithEvents(Of T).ItemIndexEventArgs"/> (<see cref="ListWithEvents(Of T).Removed"/>), 
        '''     <see cref="ListWithEvents(Of T).OldNewItemEventArgs"/> (<see cref="ListWithEvents(Of T).ItemChanged"/>), 
        '''     <see cref="ListWithEvents(Of T).ItemValueChangedEventArgs"/> (<see cref="ListWithEvents(Of T).ItemValueChanged"/>).
        ''' </param>
        ''' <remarks>
        '''     Raised after <see cref="ListWithEvents(Of T).Added"/>, <see cref="ListWithEvents(Of T).Removed"/>, <see cref="ListWithEvents(Of T).Cleared"/>, 
        '''     <see cref="ListWithEvents(Of T).ItemChanged"/> and <see cref="ListWithEvents(Of T).ItemValueChanged"/> events with the same argument <paramref name="e"/>.
        ''' </remarks>
        Public Event Changed(sender As IReportsChange, e As EventArgs) Implements IReportsChange.Changed

        ''' <summary>Raises the <see cref="Changed"/> event</summary>
        ''' <param name="e">Event parameters</param>
        ''' <remarks>
        '''     Called after <see cref="ListWithEvents(Of T).Added"/>, <see cref="ListWithEvents(Of T).Removed"/>, <see cref="ListWithEvents(Of T).Cleared"/>,
        '''     <see cref="ListWithEvents(Of T).ItemChanged"/> and <see cref="ListWithEvents(Of T).ItemValueChanged"/> events with the same argument <paramref name="e"/>.
        '''     You should call one of overloaded <see cref="ListWithEvents(Of T).OnListChanged(ListChangedEventArgs)"/> methods after calling this.
        ''' </remarks>
        Protected Overridable Sub OnChanged(e As EventArgs)
            RaiseEvent Changed(Me, e)
        End Sub

        ''' <summary>Raises the <see cref="PropertyChanged"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>This is for implement the <see cref="INotifyPropertyChanged"/> interface, but only few properties are reported via this interface.
        ''' Namely <see cref="Count"/>, <see cref="Owner"/> and <see cref="Locked"/>.</remarks>
        ''' <version version="1.5.2">Method added</version>
        Protected Overridable Sub OnPropertyCnaged(e As PropertyChangedEventArgs)
            RaiseEvent PropertyChanged(Me, e)
        End Sub

        ''' <summary>Occurs when a property value changes.</summary>
        ''' <remarks>This is for implement the <see cref="INotifyPropertyChanged"/> interface, but only few properties are reported via this interface.
        ''' Namely <see cref="Count"/>, <see cref="Owner"/> and <see cref="Locked"/>.</remarks>
        Private Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "ListWithEvents-specific"
        ''' <summary>When overridden in derived class copies all elements of this collection to new <see cref="Array"/></summary>
        Public MustOverride Function ToArray1() As Array

        ''' <summary>When overridden in derived class determines <see cref="ListWithEvents(Of T).CancelableItemEventArgs.[ReadOnly]"/> property value for the <see cref="ListWithEvents(Of T).Adding"/> and <see cref="ListWithEvents.ItemChanging"/> events</summary>
        Public MustOverride ReadOnly Property AddingReadOnly() As Boolean

        ''' <summary>When overridden in derived class gets value indicating if an <see cref="OperationCanceledException"/> is thrown when item operation is canceled in event handler.</summary>
        Public MustOverride ReadOnly Property CancelError() As Boolean

        ''' <summary>When overridden in derived class determines if it is allowed to add handlers for events that supports cancellation</summary>
        ''' <exception cref="InvalidOperationException">Trying to set value to True when it if False</exception>
        ''' <remarks>
        ''' Value can be changed only from True (default) to False
        ''' <list>
        ''' <listheader>Those are events:</listheader>
        ''' <item><see cref="ListWithEvents(Of T).Adding"/></item>
        ''' <item><see cref="ListWithEvents(Of T).Removing"/></item>
        ''' <item><see cref="ListWithEvents(Of T).Clearing"/></item>
        ''' <item><see cref="ListWithEvents(Of T).ItemChanging"/></item>
        ''' </list>
        ''' </remarks>
        Public MustOverride Property AllowAddCancelableEventsHandlers() As Boolean
        ''' <summary>When overridden in derived class gets or sets value indicating if items implementing <see cref="ICollectionCancelItem"/> are allowed to cancel itself being added/removed to/from the list.</summary>
        ''' <returns>True if item are allowed to cancel itself being added/removed; false when they are not</returns>
        ''' <value>False to prevent items from cancel itself being added/removed; true to allow it. Default value is true.</value>
        ''' <exception cref="InvalidOperationException">Value is being changed and <see cref="IsAllowItemCancelLocked"/> is true</exception>
        ''' <remarks>When setting this property to false, consider calling <see cref="LockAllowItemCancel"/>, otherwise item can change value of this property and perform cancellation.</remarks>
        ''' <seelaso cref="IsAllowItemCancelLocked"/><seelaso cref="LockAllowItemCancel"/><seelaso cref="ICollectionCancelItem"/>
        Public MustOverride Property AllowItemCancel() As Boolean

        ''' <summary>When overridden in derived class gets value indication if value of the <see cref="AllowItemCancel"/> can be changed</summary>
        ''' <returns>True when value of the <see cref="AllowItemCancel"/> cannot be changed; false if it can.</returns>
        ''' <remarks>Value of this property can be set to true by calling <see cref="LockAllowItemCancel"/></remarks>
        ''' <seelaso cref="LockAllowItemCancel"/><seelaso cref="AllowItemCancel"/><seelaso cref="ICollectionCancelItem"/>
        Public MustOverride ReadOnly Property IsAllowItemCancelLocked() As Boolean

        ''' <summary>When overridden in derived class sets <see cref="IsAllowItemCancelLocked"/>, so <see cref="AllowItemCancel"/> can no longer be changed.</summary>
        ''' <seelaso cref="IsAllowItemCancelLocked"/><seelaso cref="AllowItemCancel"/><seelaso cref="ICollectionCancelItem"/>
        Public MustOverride Sub LockAllowItemCancel()

        ''' <summary>When overridden in derived class determines if the <see cref="ListWithEvents(Of T)"/> isn't locked (being locked prevents if from being edited)</summary>
        ''' <remarks><para>
        ''' <see cref="ListWithEvents(Of T)"/> is usually locked while some events' handlers are being invoked.
        ''' </para><list>
        ''' <listheader><see cref="Locked"/> set to True blocks following methods and causes <see cref="InvalidOperationException"/> exception to be thrown there:</listheader>
        ''' <item><see cref="ListWithEvents(Of T).Add"/></item>
        ''' <item><see cref="ListWithEvents(Of T).Insert(Integer, T)"/></item>
        ''' <item><see cref="ListWithEvents(Of T).Remove(T)"/></item>
        ''' <item><see cref="RemoveAt"/></item>
        ''' <item><see cref="Clear"/></item>
        ''' <item><see cref="ListWithEvents(Of T).Item"/> (only setter)</item>
        ''' </list></remarks>
        Public MustOverride ReadOnly Property Locked() As Boolean

        ''' <summary>When overridden in derived class sets the <see cref="Locked"/> to True</summary>
        Protected MustOverride Sub Lock()

        ''' <summary>When overridden in derived class sets the <see cref="Locked"/> to False</summary>
        Protected MustOverride Sub Unlock()
#End Region

        ''' <summary>Occurs when the collection changes. Implements the <see cref="INotifyCollectionChanged.CollectionChanged"/> event. This event is provided for compatibility with <see cref="INotifyCollectionChanged"/> interface. <see cref="ListWithEvents"/> provides <see cref="ListWithEvents.CollectionChanged"/> event which provides detailed information about what has with the collection.</summary>
        ''' <version version="1.5.3">Event introduced</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Event INotifyCollectionChanged_CollectionChanged As NotifyCollectionChangedEventHandler Implements INotifyCollectionChanged.CollectionChanged

        ''' <summary>Raises the <see cref="INotifyCollectionChanged_CollectionChanged"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>This method shall be called whenever <see cref="ListWithEvents(Of T).OnCollectionChanged"/> is called.</remarks>
        ''' <version version="1.5.3">Method introduced</version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Sub OnINotifyCollectionChanged_CollectionChanged(e As NotifyCollectionChangedEventArgs)
            RaiseEvent INotifyCollectionChanged_CollectionChanged(Me, e)
        End Sub
    End Class

    ''' <summary>List that provides events when changed</summary>
    ''' <typeparam name="T">Type of items to be stored in the list</typeparam>
    ''' <remarks><para>
    ''' If item of type that implements the <see cref="IReportsChange"/> interface is passed to this list, than it's events <see cref="IReportsChange.Changed"/> are reported through <see cref="ListWithEvents.ItemValueChanged"/> event.
    ''' </para><para>
    ''' Implementation of interface <see cref="IList"/> is provided only in order this class to be compatible with <see cref="System.ComponentModel.Design.CollectionEditor"/>.
    ''' </para>
    ''' <para>Implementation of <see cref="IBindingList"/> is only basic. It supports neither sorting or searching. <see cref="IBindingList.AddNew"/> is supported when <typeparamref name="T"/> has default constructor and does not block creating of new instances in other way.</para>
    ''' <para>This class utilizes when item implements <see cref="ICollectionNotifyItem"/> or <see cref="ICollectionCancelItem"/>.</para>
    ''' </remarks>
    ''' <seelaso cref="DictionaryWithEvents(Of TKey, TValue)"/>
    ''' <author www="http://dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    ''' <version version="1.5.2">Added support for <see cref="ICollectionNotifyItem"/> and <see cref="ICollectionCancelItem"/></version>
    ''' <version version="1.5.2">Base class <see cref="ListWithEventsBase"/> introduced</version>
    ''' <version version="1.5.2">Added implementation of the <see cref="INotifyPropertyChanged"/> interface</version>
    ''' <version version="1.5.3">Added implementation for the <see cref="INotifyCollectionChanged"/> interface</version>
    <DesignerSerializer(GetType(CollectionCodeDomSerializer), GetType(CodeDomSerializer))>
    <DebuggerDisplay("Count = {Count}")>
    <Serializable()>
    Public Class ListWithEvents(Of T)
        Inherits ListWithEventsBase
        Implements ISerializable
        Implements IList(Of T), INotifyPropertyChanged

        ''' <summary>CTor</summary>
        ''' <param name="addingReadOnly">Value of <see cref="AddingReadOnly"/> property that determines <see cref="CancelableItemEventArgs.[ReadOnly]"/> property value for the <see cref="Adding"/> and <see cref="ItemChanging"/> events</param>
        ''' <param name="cancelError">Value of <see cref="CancelError"/> that determines if and <see cref="OperationCanceledException"/> is thrown when item operation is canceled in event handler.</param>
        ''' <version version="1.5.4">Parameter <c>AddingReadOnly</c> renamed to <c>addingReadOnly</c> and <c>CancelError</c> to <c>cancelError</c></version>
        Public Sub New(Optional ByVal addingReadOnly As Boolean = False, Optional ByVal cancelError As Boolean = False)
            _addingReadOnly = addingReadOnly
            _cancelError = cancelError
            list = New List(Of T)
        End Sub
        ''' <summary>Copies all elements of this collection to new <see cref="Array"/></summary>
        Public Function ToArray() As T()
            Return InternalList.ToArray
        End Function

        ''' <summary>Copies all elements of this collection to new <see cref="Array"/> (type-unsafe)</summary>
        <EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use Type-Safe ToArray instead")> Public Overrides Function ToArray1() As Array
            Return ToArray()
        End Function

        ''' <summary>CTor - initializes from another <see cref="IEnumerable(Of T)"/></summary>
        ''' <param name="collection"><see cref="IEnumerable(Of T)"/> to initialize new instance of <see cref="ListWithEvents(Of T)"/> with</param>
        ''' <param name="addingReadOnly">Value of <see cref="AddingReadOnly"/> property that determines <see cref="CancelableItemEventArgs.[ReadOnly]"/> property value for the <see cref="Adding"/> and <see cref="ItemChanging"/> events</param>
        ''' <exception cref="System.ArgumentNullException">collection is null</exception>
        ''' <version version="1.5.4">Parameter <c>AddingReadOnly</c> renamed to <c>addingReadOnly</c> and <c>CancelError</c> to <c>cancelError</c></version>
        Public Sub New(collection As IEnumerable(Of T), Optional ByVal addingReadOnly As Boolean = False, Optional ByVal cancelError As Boolean = False)
            _addingReadOnly = addingReadOnly
            _cancelError = cancelError
            list = New List(Of T)(collection)
            AddAllItemHandlers()
        End Sub

        ''' <summary>Initializes a new instance of the <see cref="ListWithEvents(Of T)"/> class that is empty and has the specified initial capacity.</summary>
        ''' <param name="capacity">The number of elements that the new list can initially store.</param>
        ''' <param name="addingReadOnly">Value of <see cref="AddingReadOnly"/> property that determines <see cref="CancelableItemEventArgs.[ReadOnly]"/> property value for the <see cref="Adding"/> and <see cref="ItemChanging"/> events</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">capacity is less than 0</exception>
        ''' <version version="1.5.4">Parameter <c>AddingReadOnly</c> renamed to <c>addingReadOnly</c> and <c>CancelError</c> to <c>cancelError</c></version>
        Public Sub New(capacity As Integer, Optional ByVal addingReadOnly As Boolean = False, Optional ByVal cancelError As Boolean = False)
            _addingReadOnly = addingReadOnly
            _cancelError = cancelError
            list = New List(Of T)(capacity)
        End Sub

        ''' <summary>Internal list that is used for soring values</summary>
        Private list As List(Of T)

        ''' <summary>Contains value of the <see cref="Owner"/> property</summary>
        Private _owner As Object
        ''' <summary>Custom property wher owner of the list can be stored to provide bi-directional reference</summary>
        ''' <remarks>Change of this property is reported through <see cref="IReportsChange.Changed"/>.
        ''' <para>This property is here for convenience, <see cref="ListWithEvents(Of T)"/> does not utilize it.</para>
        ''' <para>Change of this property is reported via <see cref="INotifyPropertyChanged.PropertyChanged"/> and <see cref="IReportsChange.Changed"/>.</para></remarks>
        Public NotOverridable Overrides Property Owner() As Object
            Get
                Return _owner
            End Get
            Set(value As Object)
                Dim changed = value IsNot Owner
                Dim old = Owner
                _owner = value
                If changed Then
                    Dim e As IReportsChange.ValueChangedEventArgs(Of Object) = New IReportsChange.ValueChangedEventArgs(Of Object)(old, value, "Owner")
                    OnChanged(e)
                    OnPropertyCnaged(e)
                End If
            End Set
        End Property

#Region "Locks and allows"
        ''' <summary>Contains value of the <see cref="AddingReadOnly"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Private ReadOnly _addingReadOnly As Boolean = False
        ''' <summary>Determines <see cref="CancelableItemEventArgs.[ReadOnly]"/> property value for the <see cref="Adding"/> and <see cref="ItemChanging"/> events</summary>
        <Browsable(False)>
        Public NotOverridable Overrides ReadOnly Property AddingReadOnly() As Boolean
            <DebuggerStepThrough()> Get
                Return _addingReadOnly
            End Get
        End Property

        ''' <summary>Contains value of the <see cref="CancelError"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Private ReadOnly _cancelError As Boolean = False
        ''' <summary>Gets value indicating if an <see cref="OperationCanceledException"/> is thrown when item operation is canceled in event handler.</summary>
        <Browsable(False)>
        Public NotOverridable Overrides ReadOnly Property CancelError() As Boolean
            <DebuggerStepThrough()> Get
                Return _cancelError
            End Get
        End Property

        ''' <summary>Contains value of the <see cref="AllowAddCancelableEventsHandlers"/> property</summary>
        Private _allowAddCancelableEventsHandlers As Boolean = True
        ''' <summary>Determines if it is allowed to add handlers for events that supports cancellation</summary>
        ''' <exception cref="InvalidOperationException">Trying to set value to True when it if False</exception>
        ''' <remarks>
        ''' Value can be changed only from True (default) to False
        ''' <list>
        ''' <listheader>Those are events:</listheader>
        ''' <item><see cref="Adding"/></item>
        ''' <item><see cref="Removing"/></item>
        ''' <item><see cref="Clearing"/></item>
        ''' <item><see cref="ItemChanging"/></item>
        ''' </list>
        ''' </remarks>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public NotOverridable Overrides Property AllowAddCancelableEventsHandlers() As Boolean
            <DebuggerStepThrough()> Get
                Return _allowAddCancelableEventsHandlers
            End Get
            Set(value As Boolean)
                If value = False Then
                    _allowAddCancelableEventsHandlers = value
                ElseIf value = True AndAlso AllowAddCancelableEventsHandlers = False Then
                    Throw New InvalidOperationException(AllowAddCancelableEventsHandlersCanBeChangedOnlyFromTrueToFalse)
                End If
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="AllowItemCancel"/> property</summary>
        Private _allowItemCancel As Boolean
        ''' <summary>Gets or sets value indicating if items implementing <see cref="ICollectionCancelItem"/> are allowed to cancel itself being added/removed to/from the list.</summary>
        ''' <returns>True if item are allowed to cancel itself being added/removed; false when they are not</returns>
        ''' <value>False to prevent items from cancel itself being added/removed; true to allow it. Default value is true.</value>
        ''' <exception cref="InvalidOperationException">Value is being changed and <see cref="IsAllowItemCancelLocked"/> is true</exception>
        ''' <remarks>When setting this property to false, consider calling <see cref="LockAllowItemCancel"/>, otherwise item can change value of this property and perform cancellation.</remarks>
        ''' <seelaso cref="IsAllowItemCancelLocked"/><seelaso cref="LockAllowItemCancel"/><seelaso cref="ICollectionCancelItem"/>
        ''' <version version="1.5.2">Property introduced</version>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public NotOverridable Overrides Property AllowItemCancel() As Boolean
            Get
                Return _allowItemCancel
            End Get
            Set(value As Boolean)
                If value <> AllowItemCancel AndAlso IsAllowItemCancelLocked Then Throw New InvalidOperationException(CannotChangeValueOfThe0PropertyWhen1IsTrue.f("AllowItemCancel", "IsAllowItemCancelLocked"))
                _allowItemCancel = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="IsAllowItemCancelLocked"/> property</summary>
        Private _lockAllowItemCancel As Boolean
        ''' <summary>Gets value indication if value of the <see cref="AllowItemCancel"/> can be changed</summary>
        ''' <returns>True when value of the <see cref="AllowItemCancel"/> cannot be changed; false if it can.</returns>
        ''' <remarks>Value of this property can be set to true by calling <see cref="LockAllowItemCancel"/></remarks>
        ''' <seelaso cref="LockAllowItemCancel"/><seelaso cref="AllowItemCancel"/><seelaso cref="ICollectionCancelItem"/>
        ''' <version version="1.5.2">Property introduced</version>
        Public NotOverridable Overrides ReadOnly Property IsAllowItemCancelLocked() As Boolean
            Get
                Return _lockAllowItemCancel
            End Get
        End Property

        ''' <summary>Sets <see cref="IsAllowItemCancelLocked"/>, so <see cref="AllowItemCancel"/> can no longer be changed.</summary>
        ''' <seelaso cref="IsAllowItemCancelLocked"/><seelaso cref="AllowItemCancel"/><seelaso cref="ICollectionCancelItem"/>
        ''' <version version="1.5.2">Method introduced</version>
        Public NotOverridable Overrides Sub LockAllowItemCancel()
            _lockAllowItemCancel = True
        End Sub

        ''' <summary>Contains value of the <see cref="Locked"/></summary>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Private _locked As Boolean = False
        ''' <summary>Determines if the <see cref="ListWithEvents(Of T)"/> isn locked (being locked prevents if from being edited)</summary>
        ''' <remarks><para>
        ''' <see cref="ListWithEvents(Of T)"/> is usually locked while some events' handlers are being invoked.
        ''' </para><list>
        ''' <listheader><see cref="Locked"/> set to True blocks following methods and causes <see cref="InvalidOperationException"/> exception to be thrown there:</listheader>
        ''' <item><see cref="Add"/></item>
        ''' <item><see cref="Insert"/></item>
        ''' <item><see cref="Remove"/></item>
        ''' <item><see cref="RemoveAt"/></item>
        ''' <item><see cref="Clear"/></item>
        ''' <item><see cref="Item"/> (only setter)</item>
        ''' </list>
        ''' <para>Change of this property is reported via <see cref="INotifyPropertyChanged.PropertyChanged"/>.</para></remarks>
        ''' <version version="1.5.2">Added <see cref="INotifyPropertyChanged"/> notification</version>
        <Browsable(False)>
        Public NotOverridable Overrides ReadOnly Property Locked() As Boolean
            <DebuggerStepThrough()> Get
                Return _locked
            End Get
        End Property
        ''' <summary>Sets the <see cref="Locked"/> to True</summary>
        Protected NotOverridable Overrides Sub Lock()
            Dim changed = Locked <> True
            _locked = True
            If changed Then OnPropertyCnaged(New IReportsChange.ValueChangedEventArgs(Of Boolean)(False, True, "Locked"))
        End Sub
        ''' <summary>Sets the <see cref="Locked"/> to False</summary>
        Protected NotOverridable Overrides Sub Unlock()
            Dim changed = Locked <> False
            _locked = False
            If changed Then OnPropertyCnaged(New IReportsChange.ValueChangedEventArgs(Of Boolean)(False, True, "Locked"))
        End Sub
#End Region

#Region "Add"
        ''' <summary>List of <see cref="ItemCancelEventHandler"/> delegates to be invoked when the <see cref="Adding"/> event is raised</summary>
        Private ReadOnly addingEventHandlerList As New List(Of ItemCancelEventHandler)
        ''' <summary>Delegate of handler of <see cref="Adding"/>, <see cref="Removing"/> and <see cref="ItemChanging"/> events</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        Public Delegate Sub ItemCancelEventHandler(sender As ListWithEvents(Of T), e As CancelableItemIndexEventArgs)

        ''' <summary>Raised before an item is added to the list. Raised by <see cref="Add"/> and <see cref="Insert"/> methods.</summary>
        ''' <remarks><para>
        ''' This event can be disabled (see <see cref="AllowAddCancelableEventsHandlers"/>.
        ''' This means that <see cref="InvalidOperationException"/> is thrown when adding handler and <see cref="AllowAddCancelableEventsHandlers"/> is False.
        ''' </para><para>
        ''' Value of parameter <paramref name="e"/>'s <see cref="CancelableItemIndexEventArgs.Item"/> can be changed if <see cref="AddingReadOnly"/> is False.
        ''' </para></remarks>
        ''' <exception cref="InvalidOperationException">Adding handler when <see cref="AllowAddCancelableEventsHandlers"/> is false</exception>
        Public Custom Event Adding As ItemCancelEventHandler
            AddHandler(value As ItemCancelEventHandler)
                If AllowAddCancelableEventsHandlers Then
                    addingEventHandlerList.Add(value)
                Else
                    Throw New InvalidOperationException(CannotAddHandlerToTheAddingEventWhenAllowAddCancelableEventsHandlersIsFalse)
                End If
            End AddHandler
            <DebuggerStepThrough()>
            RemoveHandler(value As ItemCancelEventHandler)
                addingEventHandlerList.Remove(value)
            End RemoveHandler
            RaiseEvent(sender As ListWithEvents(Of T), e As CancelableItemIndexEventArgs)
                For Each handler As ItemCancelEventHandler In addingEventHandlerList
                    handler.Invoke(sender, e)
                Next handler
            End RaiseEvent
        End Event

        ''' <summary>Raised after an item is added to the list. Raised by <see cref="Add"/> and <see cref="Insert"/> methods</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        Public Event Added(sender As ListWithEvents(Of T), e As ItemIndexEventArgs)

        ''' <summary>Adds an item to the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <param name="item">The object to add to the <see cref="ListWithEvents(Of T)"/>.</param>
        ''' <remarks>Note for inheritors: Call <see cref="OnAdding"/> before adding an item to the list and <see cref="OnAdded"/> after adding item to the list, do not forgot to check <see cref="CancelableItemIndexEventArgs.Cancel"/></remarks>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <exception cref="OperationCanceledException">Operation is canceled in event handler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Adding"/> event -or- Any exception may be thrown by <see cref="ICollectionCancelItem.OnAdding"/> when <see cref="AllowItemCancel"/> is true.</exception>
        Public Overridable Overloads Sub Add(item As T) Implements ICollection(Of T).Add
            If Locked Then Throw New InvalidOperationException(ListIsLocked)
            Dim e As New CancelableItemIndexEventArgs(item, list.Count, AddingReadOnly)
            OnAdding(e)
            If Not e.Cancel Then
                list.Add(item)
                AddItemHandler(list.Count - 1)
                OnAdded(New ItemIndexEventArgs(item, list.Count - 1))
            End If
        End Sub

        ''' <summary>Adds range of items into list</summary>
        ''' <param name="Items">Collection of items to be added</param>
        ''' <remarks>
        ''' Internally calls <see cref="Add"/> for each item.
        ''' If an exception occurs in <see cref="Add"/> or event handler than no item is added.
        ''' <paramref name="Items"/> can safely be null.
        ''' </remarks>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <exception cref="OperationCanceledException">Operation is canceled in event handler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Adding"/> event -or- Any exception may be thrown by <see cref="ICollectionCancelItem.OnAdding"/> when <see cref="AllowItemCancel"/> is true.</exception>
        ''' <version version="1.5.4">Parameter <c>Items</c> renamed to <c>items</c></version>
        Public Overridable Sub AddRange(items As IEnumerable(Of T))
            If Items Is Nothing Then Exit Sub
            Dim startAdd As Integer = Me.Count
            Try
                For Each itm As T In Items
                    Add(itm)
                Next itm
            Catch
                If Me.Count > startAdd Then
                    Dim old = _allowItemCancel
                    _allowItemCancel = False
                    Try
                        InternalList.RemoveRange(startAdd, Me.Count - startAdd + 1)
                    Finally
                        _allowItemCancel = old
                    End Try
                End If
                Throw
            End Try
        End Sub

        ''' <summary>Inserts an item to the <see cref="ListWithEvents(Of T)"/> at the specified index.</summary>
        ''' <param name="item">The object to insert into the <see cref="ListWithEvents(Of T)"/>.</param>
        ''' <param name="index">The zero-based index at which item should be inserted.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="ListWithEvents(Of T)"/>.</exception>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <exception cref="OperationCanceledException">Operation is canceled in event handler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Adding"/> event -or- Any exception may be thrown by <see cref="ICollectionCancelItem.OnAdding"/> when <see cref="AllowItemCancel"/> is true.</exception>
        ''' <remarks>Note for inheritors: Call <see cref="OnAdding"/> before adding an item to the list and <see cref="OnAdded"/> after adding item to the list, do not forgot to check <see cref="CancelableItemIndexEventArgs.Cancel"/></remarks>
        Public Overridable Overloads Sub Insert(index As Integer, item As T) Implements IList(Of T).Insert
            If Locked Then Throw New InvalidOperationException(ListIsLocked)
            Dim e As New CancelableItemIndexEventArgs(item, index, AddingReadOnly)
            OnAdding(e)
            If Not e.Cancel Then
                list.Add(item)
                AddItemHandler(index)
                OnAdded(New ItemIndexEventArgs(item, index))
            End If
        End Sub

        ''' <summary>Raises <see cref="Adding"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnAdding"/> in order the event to be raised</remarks>
        ''' <exception cref="OperationCanceledException">Operation is canceled in event handler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Adding"/> event -or- Any <see cref="Exception"/> can be thrown by <see cref="ICollectionCancelItem.OnAdding"/> when <see cref="AllowItemCancel"/> is true</exception>
        ''' <version version="1.5.2">Added support for <see cref="ICollectionCancelItem"/></version>
        Protected Overridable Sub OnAdding(e As CancelableItemIndexEventArgs)
            If AllowItemCancel AndAlso TypeOf e.Item Is ICollectionCancelItem Then DirectCast(e.Item, ICollectionCancelItem).OnAdding(Me, e.NewIndex, False)
            RaiseEvent Adding(Me, e)
            If e.Cancel AndAlso CancelError Then Throw New OperationCanceledException(IfNull(e.CancelMessage, "Adding was canceled in event handler"))
            If e.Changed AndAlso Not e.Cancel AndAlso AllowItemCancel AndAlso TypeOf e.Item Is ICollectionCancelItem Then DirectCast(e.Item, ICollectionCancelItem).OnAdding(Me, e.NewIndex, False)
        End Sub

        ''' <summary>Raises <see cref="Added"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnAdded"/> in order the event to be raised</remarks>
        ''' <exception cref="MultipleException">Multiple exceptions were thrown by event handlers being called. All handler are always invoked, even when exception occurs.</exception>
        ''' <version version="1.5.2">Added support for <see cref="ICollectionNotifyItem"/> and <see cref="MultipleException"/>.</version>
        Protected Overridable Sub OnAdded(e As ItemIndexEventArgs)
            Dim exceptions As New List(Of Exception)
            On Error GoTo erh
            If TypeOf e.Item Is ICollectionNotifyItem Then DirectCast(e.Item, ICollectionNotifyItem).OnAdded(Me, e.Index)
            RaiseEvent Added(Me, e)
            OnChanged(e)
            OnCollectionChanged(e, CollectionChangeAction.Add, Nothing, e.Item, e.Index)
            OnPropertyCnaged(New IReportsChange.ValueChangedEventArgs(Of Integer)(Me.Count - 1, Me.Count, "Count"))
            On Error GoTo 0
            If exceptions.Count > 0 Then Throw MultipleException.GetException(exceptions)
            Exit Sub
erh:
            exceptions.Add(Err.GetException)
            Resume Next
        End Sub
#End Region

#Region "Clear"
        ''' <summary>List of <see cref="ClearingEventHandler"/> delegates to be invoked when the <see cref="Clearing"/> event is raised</summary>
        Private ReadOnly clearingEventHandlerList As New List(Of ClearingEventHandler)
        ''' <summary>Delegate of handler of <see cref="Clearing"/> event</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        Public Delegate Sub ClearingEventHandler(sender As ListWithEvents(Of T), e As CancelMessageEventArgs)

        ''' <summary>Raised before the list is cleared. Raised by <see cref="Clear"/> method.</summary>
        ''' <remarks><para>
        ''' This event can be disabled (see <see cref="AllowAddCancelableEventsHandlers"/>.
        ''' This means that<see cref="InvalidOperationException"/> is thrown when adding handler and <see cref="AllowAddCancelableEventsHandlers"/> is False.
        ''' </para><para>
        ''' <see cref="Removing"/> Event is not raised when clearing list.
        ''' </para></remarks>
        ''' <exception cref="InvalidOperationException">Adding handler when <see cref="AllowAddCancelableEventsHandlers"/> is false</exception>
        Public Custom Event Clearing As ClearingEventHandler
            AddHandler(value As ClearingEventHandler)
                If AllowAddCancelableEventsHandlers Then
                    clearingEventHandlerList.Add(value)
                Else
                    Throw New InvalidOperationException(CannotAddHandlerToTheClearigEventWhenAllowAddCancelableEventsHandlersIsFalse)
                End If
            End AddHandler
            <DebuggerStepThrough()>
            RemoveHandler(value As ClearingEventHandler)
                clearingEventHandlerList.Remove(value)
            End RemoveHandler
            RaiseEvent(sender As ListWithEvents(Of T), e As CancelMessageEventArgs)
                For Each handler As ClearingEventHandler In clearingEventHandlerList
                    handler.Invoke(sender, e)
                Next handler
            End RaiseEvent
        End Event

        ''' <summary>Raised after the list is cleared. Raised by <see cref="Clear"/> method.</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        ''' <remarks><see cref="Removed"/> event is not raised when clearing list.</remarks>
        Public Event Cleared(sender As ListWithEvents(Of T), e As ItemsEventArgs)
        ''' <summary>Removes all items from the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <exception cref="OperationCanceledException">Operation is canceled in event handler and <see cref="CancelError"/> is true</exception>
        ''' <remarks>Note for inheritors: Call <see cref="OnClearing"/> before clearing of the list and <see cref="OnCleared"/> after clearing of the list,, do not forgot to check <see cref="CancelEventArgs.Cancel"/></remarks>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Clearing"/> event -or- Any exception can be thrown by <see cref="ICollectionCancelItem.OnClearing"/> when <see cref="AllowItemCancel"/> is true.</exception>
        Public Overrides Sub Clear() Implements ICollection(Of T).Clear ', System.Collections.IList.Clear
            If Locked Then Throw New InvalidOperationException(ListIsLocked)
            Dim e As New CancelMessageEventArgs
            OnClearing(e)
            If Not e.Cancel Then
                Dim e2 As New ItemsEventArgs(list.ToArray)
                RemoveAllItemHandlers()
                list.Clear()
                OnCleared(e2)
            End If
        End Sub

        ''' <summary>Raises <see cref="Clearing"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnClearing"/> in order the event to be raised</remarks>
        ''' <exception cref="OperationCanceledException">Operation is canceled in event handler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Clearing"/> event -or- Any exception may be thrown by <see cref="ICollectionCancelItem.OnClearing"/> when <see cref="AllowItemCancel"/> is true</exception>
        Protected Overridable Sub OnClearing(ByVal e As CancelMessageEventArgs)
            If AllowItemCancel Then
                For Each itm In Me
                    If TypeOf itm Is ICollectionCancelItem Then DirectCast(itm, ICollectionCancelItem).OnClearing(Me)
                Next
            End If
            RaiseEvent Clearing(Me, e)
            If e.Cancel AndAlso Me.CancelError Then Throw New OperationCanceledException(IfNull(e.CancelMessage, ClearingWasCanceledInEventhendler))
        End Sub

        ''' <summary>Raises <see cref="Cleared"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnCleared"/> in order the event to be raised</remarks>
        ''' <exception cref="MultipleException">Multiple exceptions were thrown by event handlers being invoked. All handlers are always invoked, even hen exception occurs.</exception>
        ''' <version version="1.5.2">Added support for <see cref="ICollectionNotifyItem"/> and <see cref="MultipleException"/>.</version>
        Protected Overridable Sub OnCleared(ByVal e As ItemsEventArgs)
            Dim exceptions As New List(Of Exception)
            On Error GoTo erh
            For Each itm In e.Items
                If TypeOf itm Is ICollectionNotifyItem Then DirectCast(itm, ICollectionNotifyItem).OnRemoved(Me)
            Next
            RaiseEvent Cleared(Me, e)
            OnChanged(e)
            OnCollectionChanged(e, CollectionChangeAction.Clear, Nothing, Nothing, -1)
            OnPropertyCnaged(New IReportsChange.ValueChangedEventArgs(Of Integer)(e.Count, Me.Count, "Count"))
            On Error GoTo 0
            If exceptions.Count > 0 Then Throw MultipleException.GetException(exceptions)
            Exit Sub
erh:
            exceptions.Add(Err.GetException())
            Resume Next
        End Sub
#End Region

        ''' <summary>Determines whether the <see cref="ListWithEvents(Of T)"/> contains a specific value.</summary>
        ''' <param name="item">The object to locate in the <see cref="ListWithEvents(Of T)"/>.</param>
        ''' <returns>true if item is found in the <see cref="ListWithEvents(Of T)"/>; otherwise, false.</returns>
        Public Overridable Overloads Function Contains(ByVal item As T) As Boolean Implements ICollection(Of T).Contains
            Return list.Contains(item)
        End Function
        ''' <summary>Copies the elements of the <see cref="ListWithEvents(Of T)"/> to an <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from <see cref="ListWithEvents(Of T)"/>. The <see cref="System.Array"/> must have zero-based indexing.</param>
        ''' <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        ''' <exception cref="System.ArgumentNullException">array is null.</exception>
        ''' <exception cref="System.ArgumentException">array is multidimensional.-or-arrayIndex is equal to or greater than the length of array.-or-The number of elements in the source System.Collections.Generic.ICollection(Of T) is greater than the available space from arrayIndex to the end of the destination array.-or-Type T cannot be cast automatically to the type of the destination array.</exception>
        Public Overridable Sub CopyTo(ByVal array() As T, ByVal arrayIndex As Integer) Implements ICollection(Of T).CopyTo
            list.CopyTo(array, arrayIndex)
        End Sub
        ''' <summary>Gets the number of elements contained in the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <returns>The number of elements contained in the <see cref="ListWithEvents(Of T)"/>.</returns>
        ''' <remarks>Change of this property is reported via <see cref="INotifyPropertyChanged.PropertyChanged"/>.</remarks>
        ''' <version version="1.5.2">Added <see cref="INotifyPropertyChanged"/> notification</version>
        Public Overrides ReadOnly Property Count() As Integer Implements ICollection(Of T).Count ', System.Collections.ICollection.Count
            Get
                Return list.Count
            End Get
        End Property
        ''' <summary>Gets a value indicating whether the <see cref="ListWithEvents(Of T)"/> is read-only (always false).</summary>
        ''' <returns>Always false because <see cref="ListWithEvents(Of T)"/> is not read-only</returns>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public NotOverridable Overrides ReadOnly Property IsReadOnly() As Boolean Implements ICollection(Of T).IsReadOnly ', System.Collections.IList.IsReadOnly
            Get
                Return False
            End Get
        End Property

#Region "Remove"
        ''' <summary>List of <see cref="ItemCancelEventHandler"/> delegates to be invoked when the <see cref="Removing"/> event is raised</summary>
        Private ReadOnly removingEventHandlerList As New List(Of ItemCancelEventHandler)
        ''' <summary>Raised before item is removed from the list. Raised by <see cref="Remove"/> and <see cref="RemoveAt"/> methods.</summary>
        ''' <remarks><para>
        ''' This event can be disabled (see <see cref="AllowAddCancelableEventsHandlers"/>.
        ''' This means that<see cref="InvalidOperationException"/> is thrown when adding handler and <see cref="AllowAddCancelableEventsHandlers"/> is False.
        ''' </para><para>
        ''' Do not change content of list in handler! List is locked.
        ''' </para><para>
        ''' <see cref="Removing"/> event is not raised when list is being cleared.
        ''' </para><para>
        ''' Value of parameter <paramref name="e"/>'s <see cref="CancelableItemIndexEventArgs.Item"/> cannot be changed.
        ''' </para>
        ''' </remarks>
        ''' <exception cref="InvalidOperationException">Adding handler when <see cref="AllowAddCancelableEventsHandlers"/> is false</exception>
        Public Custom Event Removing As ItemCancelEventHandler
            AddHandler(value As ItemCancelEventHandler)
                If AllowAddCancelableEventsHandlers Then
                    removingEventHandlerList.Add(value)
                Else
                    Throw New InvalidOperationException(CannotAddHandlerToTheRemovingEventWhenAllowAddCancelableEventsHandlersIsFalse)
                End If
            End AddHandler
            <DebuggerStepThrough()>
            RemoveHandler(value As ItemCancelEventHandler)
                removingEventHandlerList.Remove(value)
            End RemoveHandler
            RaiseEvent(sender As ListWithEvents(Of T), e As CancelableItemIndexEventArgs)
                For Each Handler As ItemCancelEventHandler In removingEventHandlerList
                    Handler.Invoke(sender, e)
                Next Handler
            End RaiseEvent
        End Event

        ''' <summary>Raised after the list is cleared. Raised by <see cref="Remove"/> and <see cref="RemoveAt"/> methods.</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        ''' <remarks><see cref="Removed"/> event is not raised when the list.</remarks>
        Public Event Removed(sender As ListWithEvents(Of T), e As ItemIndexEventArgs)

        ''' <summary>Raises <see cref="Removing"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks><para>
        ''' Note for inheritors: Always call base class method <see cref="OnRemoving"/> in order the event to be raised
        ''' </para><para>
        ''' Do not change content of list in this method!
        ''' </para><para>
        ''' </para>
        ''' <para><paramref name="e"/><see cref="CancelableItemIndexEventArgs.[ReadOnly]">ReadOnly</see> hould be always true; otherwise unpredictable results may occur.</para></remarks>
        ''' <exception cref="OperationCanceledException">Operation is canceled in event handler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Removing"/> event -or- Any <see cref="Exception"/> can be thrown by <see cref="ICollectionCancelItem.OnRemoving"/> when <see cref="AllowItemCancel"/> is true</exception>
        ''' <version version="1.5.2">Added support for <see cref="ICollectionCancelItem"/></version>
        Protected Overridable Sub OnRemoving(ByVal e As CancelableItemIndexEventArgs)
            If AllowItemCancel AndAlso TypeOf e.Item Is ICollectionCancelItem Then DirectCast(e.Item, ICollectionCancelItem).OnRemoving(Me, e.NewIndex)
            RaiseEvent Removing(Me, e)
            If e.Cancel AndAlso CancelError Then Throw New OperationCanceledException(IfNull(e.CancelMessage, RemovingWasCenceledInEventHandler))
        End Sub

        ''' <summary>Raises <see cref="Removed"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnRemoved"/> in order the event to be raised</remarks>
        ''' <version version="1.5.2">Added support for <see cref="ICollectionNotifyItem"/> and <see cref="MultipleException"/>.</version>
        ''' <exception cref="MultipleException">Multiple exceptions were thrown by event handlers being called. All event handlers are called when when exception is thrown.</exception>
        Protected Overridable Sub OnRemoved(ByVal e As ItemIndexEventArgs)
            Dim exceptions As New List(Of Exception)
            On Error GoTo erh
            If TypeOf e.Item Is ICollectionNotifyItem Then DirectCast(e.Item, ICollectionNotifyItem).OnRemoved(Me)
            RaiseEvent Removed(Me, e)
            OnChanged(e)
            OnCollectionChanged(e, CollectionChangeAction.Remove, e.Item, Nothing, e.Index)
            OnPropertyCnaged(New IReportsChange.ValueChangedEventArgs(Of Integer)(Me.Count + 1, Me.Count, "Count"))
            On Error GoTo 0
            If exceptions.Count <> 0 Then Throw MultipleException.GetException(exceptions)
            Exit Sub
erh:
            exceptions.Add(Err.GetException)
            Resume Next
        End Sub

        ''' <summary>Removes the first occurrence of a specific object from the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <param name="item">The object to remove from the <see cref="ListWithEvents(Of T)"/>.</param>
        ''' <returns>true if item was successfully removed from the <see cref="ListWithEvents(Of T)"/>; otherwise, false. This method also returns false if item is not found in the original <see cref="ListWithEvents(Of T)"/>.</returns>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <exception cref="OperationCanceledException">Operation is canceled in event handler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Removing"/> event -or- Any exception may be thrown by <see cref="ICollectionCancelItem.OnRemoving"/> when <see cref="AllowItemCancel"/> is true.</exception>
        ''' <remarks>Note for inheritors: Call <see cref="OnRemoving"/> before removing item and <see cref="OnRemoved"/> after removing item, do not forgot to check <see cref="CancelableItemIndexEventArgs.Cancel"/></remarks>
        Public Overridable Overloads Function Remove(ByVal item As T) As Boolean Implements ICollection(Of T).Remove
            If Locked Then Throw New InvalidOperationException(ListIsLocked)
            If Contains(item) Then
                Dim e As New CancelableItemIndexEventArgs(item, IndexOf(item), True)
                Lock()
                Try
                    OnRemoving(e)
                Finally
                    Unlock()
                End Try
                If Not e.Cancel Then
                    Dim i As Integer = IndexOf(item)
                    RemoveItemHandler(i)
                    If list.Remove(item) Then
                        OnRemoved(New ItemIndexEventArgs(item, i))
                        Return True
                    Else
                        AddItemHandler(i)
                        Return False
                    End If
                Else : Return False
                End If
            Else
                Return False
            End If
        End Function

        ''' <summary>Removes the <see cref="ListWithEvents(Of T)"/> item at the specified index.</summary>
        ''' <param name="index">The zero-based index of the item to remove.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="ListWithEvents(Of T)"/>.</exception>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <exception cref="OperationCanceledException">Operation is canceled in event handler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Removing"/> event -or- Any exception may be thrown by <see cref="ICollectionCancelItem.OnRemoving"/> when <see cref="AllowItemCancel"/> is true.</exception>
        ''' <remarks>Note for inheritors: Call <see cref="OnRemoving"/> before removing item and <see cref="OnRemoved"/> after removing item, do not forgot to check <see cref="CancelableItemIndexEventArgs.Cancel"/></remarks>
        Public Overrides Sub RemoveAt(ByVal index As Integer) Implements IList(Of T).RemoveAt
            If Locked Then Throw New InvalidOperationException(ListIsLocked)
            Dim e As New CancelableItemIndexEventArgs(Me(index), index, True)
            If index >= 0 AndAlso index < Count Then
                Lock()
                Try
                    OnRemoving(e)
                Finally
                    Unlock()
                End Try
            End If
            If Not e.Cancel Then
                Dim itm As T = Me(index)
                RemoveItemHandler(index)
                Try
                    list.RemoveAt(index)
                Catch ex As Exception
                    AddItemHandler(index)
                    Throw ex
                End Try
                OnRemoved(New ItemIndexEventArgs(itm, index))
            End If
        End Sub

        ''' <summary>Removes all items that matches given predicate</summary>
        ''' <param name="match">Predicate to match. If this predicate returns true, item is removed</param>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <exception cref="OperationCanceledException">Operation is canceled in event handler and <see cref="CancelError"/> is true.</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Removing"/> event -or- Any exception may be thrown by <see cref="ICollectionCancelItem.OnRemoving"/> when <see cref="AllowItemCancel"/> is true.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="match"/> is null</exception>
        ''' <remarks>If any exception is thrown in <seealso cref="RemoveAt"/> or event handler no item is removed (collection stays unchanged)</remarks>
        ''' <version version="1.5.4">Parameter <c>Match</c> renamed to <c>match</c></version>
        Public Overridable Sub RemoveAll(ByVal match As Predicate(Of T))
            If match Is Nothing Then Throw New ArgumentNullException("match")
            Dim [Rem] As New List(Of Integer)
            For i As Integer = 0 To Me.Count - 1
                If match.Invoke(Me(i)) Then
                    [Rem].Add(i)
                End If
            Next i
            Dim removed As New List(Of KeyValuePair(Of Integer, T))
            Try
                For i As Integer = [Rem].Count - 1 To 0 Step -1
                    Dim item As T = Me([Rem](i))
                    Me.RemoveAt([Rem](i))
                    removed.Add(New KeyValuePair(Of Integer, T)(i, item))
                Next i
            Catch
                For Each reAdd As KeyValuePair(Of Integer, T) In removed
                    Me.InternalList.Insert(reAdd.Key, reAdd.Value)
                Next reAdd
                Throw
            End Try
        End Sub

        ''' <summary>Retrieves the all the elements that match the conditions defined by the specified predicate.</summary>
        ''' <param name="match">The <see cref="System.Predicate(Of T)"/> delegate that defines the conditions of the elements to search for.</param>
        ''' <returns>A <see cref="System.Collections.Generic.List(Of T)"/> containing all the elements that match the conditions defined by the specified predicate, if found; otherwise, an empty <see cref="System.Collections.Generic.List(Of T)"/>.</returns>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        ''' <remarks><seealso cref="List(Of T).FindAll"/></remarks>
        ''' <version version="1.5.4">Parameter <c>Match</c> renamed to <c>match</c></version>
        Public Overridable Function FindAll(ByVal match As Predicate(Of T)) As List(Of T)
            Return InternalList.FindAll(match)
        End Function
#End Region

        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="System.Collections.Generic.IEnumerator(Of T)"/> that can be used to iterate through the collection.</returns>
        Public Overridable Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
            Return list.GetEnumerator
        End Function
        ''' <summary>Returns an enumerator that iterates through a collection.</summary>
        ''' <returns>An <see cref="System.Collections.IEnumerator"/> object that can be used to iterate through the collection.</returns>
        ''' <version version="1.5.2">Renamed from <c>GetEnumerator1</c> to <c>IEnumerable_GetEnumerator</c></version>
        <EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use type-safe GetEnumerator instead")>
        Public NotOverridable Overrides Function IEnumerable_GetEnumerator() As IEnumerator 'Implements System.Collections.IEnumerable.GetEnumerator
            Return list.GetEnumerator
        End Function
        ''' <summary>Determines the index of a specific item in the <see cref="ListWithEvents(Of T)"/>.</summary>
        ''' <param name="item">The object to locate in the <see cref="ListWithEvents(Of T)"/>.</param>
        ''' <returns>The index of item if found in the list; otherwise, -1.</returns>
        Public Overridable Overloads Function IndexOf(ByVal item As T) As Integer Implements IList(Of T).IndexOf
            Return list.IndexOf(item)
        End Function

#Region "Item"
        ''' <summary>List of <see cref="ItemCancelEventHandler"/> delegates to be invoked when the <see cref="ItemChanging"/> event is raised</summary>
        Private ReadOnly itemChangingEventHandlerList As New List(Of ItemCancelEventHandler)

        ''' <summary>Raised before an item is changed. Raised by setter of <see cref="Item"/> property.</summary>
        ''' <remarks><para>
        ''' This event can be disabled (see <see cref="AllowAddCancelableEventsHandlers"/>.
        ''' This means that <see cref="InvalidOperationException"/> is thrown when adding handler and <see cref="AllowAddCancelableEventsHandlers"/> is False.
        ''' </para><para>
        ''' Value of parameter <paramref name="e"/>'s <see cref="CancelableItemIndexEventArgs.Item"/> can be changed if <see cref="AddingReadOnly"/> is False.
        ''' </para><para>
        ''' Do not change content of list in handler! List is locked.
        ''' </para><para>
        ''' <paramref name="e"/>'s <see cref="CancelableItemIndexEventArgs.Item"/> contains new value. Use <see cref="Item"/> to determine old value.
        ''' </para></remarks>
        ''' <exception cref="InvalidOperationException">Adding handler when <see cref="AllowAddCancelableEventsHandlers"/> is false</exception>
        Public Custom Event ItemChanging As ItemCancelEventHandler
            AddHandler(value As ItemCancelEventHandler)
                If AllowAddCancelableEventsHandlers Then
                    itemChangingEventHandlerList.Add(value)
                Else
                    Throw New InvalidOperationException(CannotAddHandlerToTheItemChangingEventWhenAllowAddCancelableEventsHandlersIsFalse)
                End If
            End AddHandler
            <DebuggerStepThrough()>
            RemoveHandler(value As ItemCancelEventHandler)
                itemChangingEventHandlerList.Remove(value)
            End RemoveHandler
            RaiseEvent(sender As ListWithEvents(Of T), e As CancelableItemIndexEventArgs)
                For Each handler As ItemCancelEventHandler In itemChangingEventHandlerList
                    handler.Invoke(sender, e)
                Next handler
            End RaiseEvent
        End Event

        ''' <summary>Raised after item in the list is changed. Raised by setter of <see cref="Item"/> property.</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters (<see cref="ItemIndexEventArgs.Item"/> contains old value, use <see cref="Item"/> to determine new value.)</param>
        Public Event ItemChanged(sender As ListWithEvents(Of T), e As OldNewItemEventArgs)

        ''' <summary>Raises <see cref="ItemChanging"/> event</summary>
        ''' <param name="e">Event argument</param>
        ''' <remarks><para>
        ''' Note for inheritors: Alway call base class method <see cref="OnItemChanging"/> in order the event to be raised.
        ''' </para><para>
        ''' Do not change the content of the list in this method!
        ''' </para></remarks>
        ''' <exception cref="OperationCanceledException">Operation is canceled in event handler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="ItemChanging"/> event -or-  Any exception may be throw by <see cref="ICollectionCancelItem.OnRemoving"/> or <see cref="ICollectionCancelItem.OnAdding"/> when <see cref="AllowItemCancel"/> is true</exception>
        ''' <version version="1.5.2">Added support for <see cref="ICollectionCancelItem"/>.</version>
        Protected Overridable Sub OnItemChanging(ByVal e As CancelableItemIndexEventArgs)
            If AllowItemCancel AndAlso TypeOf Me(e.NewIndex) Is ICollectionCancelItem Then DirectCast(Me(e.NewIndex), ICollectionCancelItem).OnRemoving(Me(e.NewIndex), e.NewIndex)
            If AllowItemCancel AndAlso TypeOf e.Item Is ICollectionCancelItem Then DirectCast(e.Item, ICollectionCancelItem).OnAdding(Me, e.NewIndex, True)
            RaiseEvent ItemChanging(Me, e)
            If e.Cancel AndAlso CancelError Then Throw New OperationCanceledException(IfNull(e.CancelMessage, ChangingWasCanceledInEventhandler))
            If e.Changed AndAlso Not e.Cancel AndAlso AllowItemCancel AndAlso TypeOf Me(e.NewIndex) Is ICollectionCancelItem Then DirectCast(Me(e.NewIndex), ICollectionCancelItem).OnRemoving(Me(e.NewIndex), e.NewIndex)
        End Sub

        ''' <summary>Raises <see cref="ItemChanged"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnItemChanged"/> in order the event to be raised.</remarks>
        ''' <version version="1.5.2">Added support for <see cref="ICollectionNotifyItem"/> and <see cref="MultipleException"/></version>
        ''' <exception cref="MultipleException">Multiple exceptions were thrown by delegate being called. All delegates are called even when exception is thrown.</exception>
        Protected Overridable Sub OnItemChanged(ByVal e As OldNewItemEventArgs)
            Dim exceptions As New List(Of Exception)
            On Error GoTo erh
            If TypeOf e.OldItem Is ICollectionNotifyItem Then DirectCast(e.OldItem, ICollectionNotifyItem).OnRemoved(Me)
            If TypeOf e.Item Is ICollectionNotifyItem Then DirectCast(e.Item, ICollectionNotifyItem).OnAdded(Me, e.Index)
            RaiseEvent ItemChanged(Me, e)
            OnChanged(e)
            OnCollectionChanged(e, CollectionChangeAction.Replace, e.OldItem, e.Item, e.Index)
            On Error GoTo 0
            If exceptions.Count <> 0 Then Throw MultipleException.GetException(exceptions)
            Exit Sub
erh:
            exceptions.Add(Err.GetException)
            Resume Next
        End Sub

        ''' <summary>Gets or sets the element at the specified index.</summary>
        ''' <param name="index">The zero-based index of the element to get or set.</param>
        ''' <returns>The element at the specified index.</returns>
        ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="ListWithEvents(Of T)"/>.</exception>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True (in setter)</exception>
        ''' <exception cref="OperationCanceledException">Operation is canceled in event handler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="ItemChanging"/> event -or- Any exception may be thrown by <see cref="ICollectionCancelItem.OnAdding"/> or <see cref="ICollectionCancelItem.OnRemoving"/> when <see cref="AllowItemCancel"/> is true.</exception>
        ''' <version version="1.5.2">When <typeparamref name="T"> is not value type and value being set is (in terms of reference) same as current value at <paramref name="index"/>, no change is done, no events are raised.</typeparamref></version>
        Default Public Overridable Property Item(ByVal index As Integer) As T Implements IList(Of T).Item
            Get
                Return list(index)
            End Get
            Set(ByVal value As T)
                If index >= 0 AndAlso index < Me.Count AndAlso Not GetType(T).IsValueType AndAlso DirectCast(value, Object) Is DirectCast(Me(index), Object) Then Exit Property
                If Locked Then Throw New InvalidOperationException(ListIsLocked)
                Dim e As New CancelableItemIndexEventArgs(value, index, AddingReadOnly)
                If index >= 0 AndAlso index < list.Count Then
                    Lock()
                    Try
                        OnItemChanging(e)
                    Finally
                        Unlock()
                    End Try
                End If
                If Not e.Cancel Then
                    Dim old As T = list(index)
                    RemoveItemHandler(index)
                    list(index) = e.Item
                    AddItemHandler(index)
                    OnItemChanged(New OldNewItemEventArgs(old, list(index), index))
                End If
            End Set
        End Property
#End Region

        ''' <summary>Gives access to underlying <see cref="List(Of T)"/></summary>
        <Browsable(False)>
        Protected ReadOnly Property InternalList() As List(Of T)
            <DebuggerStepThrough()> Get
                Return list
            End Get
        End Property
        ''' <summary>Gives read-only access to underlying <see cref="List(Of T)"/></summary>
        <Browsable(False)>
        Public ReadOnly Property AsReadOnly() As IReadOnlyList(Of T)
            <DebuggerStepThrough()> Get
                Return New ReadOnlyListAdapter(Of T)(list)
            End Get
        End Property
#Region "ItemHandlers"
        ''' <summary>Adds handler to item at specified index if the item is <see cref="IReportsChange"/></summary>
        ''' <param name="Index">Index of item to try add handler</param>
        ''' <remarks>Call after item is added</remarks>
        ''' <version version="1.5.4">Parameter <c>Index</c> renamed to <c>index</c></version>
        Protected Overridable Sub AddItemHandler(ByVal index As Integer)
            If TypeOf Me(index) Is IReportsChange Then
                AddHandler CType(Me(index), IReportsChange).Changed, AddressOf OnItemValueChanged
            End If
        End Sub
        ''' <summary>Removes handler from item at specified index if the item is <see cref="IReportsChange"/></summary>
        ''' <param name="Index">Index of item to try remove handler</param>
        ''' <remarks>Call before item is removed</remarks>
        ''' <version version="1.5.4">Parameter <c>Index</c> renamed to <c>index</c></version>
        Protected Overridable Sub RemoveItemHandler(ByVal index As Integer)
            If TypeOf Me(index) Is IReportsChange Then
                RemoveHandler CType(Me(index), IReportsChange).Changed, AddressOf OnItemValueChanged
            End If
        End Sub
        ''' <summary>Removes handlers from all item that are of type <see cref="IReportsChange"/></summary>
        ''' <remarks>Call before clering list</remarks>
        Protected Overridable Sub RemoveAllItemHandlers()
            For i As Integer = 0 To Count - 1
                RemoveItemHandler(i)
            Next i
        End Sub
        ''' <summary>Adds ahndlers to all items that as of type <see cref="IReportsChange"/></summary>
        ''' <remarks>Call only from CTor when no handlers have been added</remarks>
        Protected Overridable Sub AddAllItemHandlers()
            For i As Integer = 0 To Count - 1
                AddItemHandler(i)
            Next i
        End Sub
        ''' <summary>Raises the <see cref="ItemValueChanged"/> event and handles the <see cref="IReportsChange.Changed"/> event for items</summary>
        ''' <param name="sender">Original source of the event</param>
        ''' <param name="e">Original event parameters</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnItemValueChanged"/> in order the event to be raised</remarks>
        ''' <exception cref="MultipleException">Multiple exceptions were thrown by handler being invoked. All handler are invoked even when exception is thrown.</exception>
        ''' <version version="1.5.2">Added support for <see cref="MultipleException"/></version>
        Protected Overridable Sub OnItemValueChanged(ByVal sender As IReportsChange, ByVal e As EventArgs)
            Dim exceptions As New List(Of Exception)
            On Error GoTo erh
            Dim e2 As New ItemValueChangedEventArgs(sender, e)
            RaiseEvent ItemValueChanged(Me, e2)
            OnChanged(e2)
            OnCollectionChanged(e2, CollectionChangeAction.ItemChange, sender, sender, IndexOf(DirectCast(sender, T)))
            On Error GoTo 0
            If exceptions.Count <> 0 Then Throw MultipleException.GetException(exceptions)
            Exit Sub
erh:
            exceptions.Add(Err.GetException)
            Resume Next
        End Sub
        ''' <summary>Raised when any of items that is of type <see cref="IReportsChange"/> raises <see cref="IReportsChange.Changed"/> event</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event params (contains original source (item) and original arguments</param>
        Public Event ItemValueChanged(sender As ListWithEvents(Of T), e As ItemValueChangedEventArgs)
#End Region

#Region "EventArgs"
        ''' <summary>Parameter of cancelable item events</summary>
        Public Class CancelableItemEventArgs : Inherits CancelMessageEventArgs
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
            ''' <version version="1.5.2">When <typeparamref name="T"/> is not value type and value being set is same (in rerm of references) as current value, no change is done, no exception is thrown (even if <see cref="[ReadOnly]"/> is true).</version>
            Public Property Item() As T
                Get
                    Return _Item
                End Get
                Set(ByVal value As T)
                    If Not GetType(T).IsValueType AndAlso DirectCast(value, Object) Is DirectCast(Item, Object) Then Exit Property
                    If Not [ReadOnly] Then
                        _Item = value
                        _Changed = True
                    Else
                        Throw New ReadOnlyException(CannotChangeItemPropertyWhenReadOnlyIsTrue)
                    End If
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Changed"/> property</summary>
            Private _Changed As Boolean
            ''' <summary>Gets value indication if <see cref="Item"/> was changed (set to another value)</summary>
            ''' <returns>True whan <see cref="Item"/> was changed (set to another value)</returns>
            ''' <version version="1.5.2">Property added</version>
            Public ReadOnly Property Changed() As Boolean
                Get
                    Return _Changed
                End Get
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
            ''' <summary>Index ow newly added or changed item</summary>
            Public ReadOnly Index As Integer
            ''' <summary>CTor</summary>
            ''' <param name="Item">Newly added item</param>
            ''' <param name="index">Index of newly added item</param>
            Public Sub New(ByVal Item As T, ByVal index As Integer)
                MyBase.New(Item)
                Me.Index = index
            End Sub
        End Class
        ''' <summary>Parameter of the <see cref="ItemChanged"/> event</summary>
        Public Class OldNewItemEventArgs : Inherits ItemIndexEventArgs
            ''' <summary>Old item previosly on <see cref="Index"/></summary>
            Public ReadOnly OldItem As T
            ''' <summary>CTor</summary>
            ''' <param name="OldItem">Old item present at position</param>
            ''' <param name="NewItem">New item present at postion</param>
            ''' <param name="index">Position index</param>
            Public Sub New(ByVal OldItem As T, ByVal NewItem As T, ByVal index As Integer)
                MyBase.New(NewItem, index)
                Me.OldItem = OldItem
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
        ''' <summary>Parameter of event that report items</summary>
        Public Class ItemsEventArgs : Inherits CountEventArgs
            ''' <summary>CTor</summary>
            ''' <param name="Items">Items to be reported</param>
            Public Sub New(ByVal Items As T())
                MyBase.New(Items.Length)
                Me.Items = Items
            End Sub
            ''' <summary>Items reported by this event</summary>
            Public ReadOnly Items() As T
        End Class
        ''' <summary>Parameter of the <see cref="ItemValueChanged"/> event</summary>
        Public Class ItemValueChangedEventArgs : Inherits ItemEventArgs
            ''' <summary>Original argument of item's <see cref="IReportsChange.Changed"/> event</summary>
            Public ReadOnly OriginalEventArgs As EventArgs
            ''' <summary>CTor</summary>
            ''' <param name="Item">Item that caused the event</param>
            ''' <param name="OriginalEventArgs">Original argument of item's <see cref="IReportsChange.Changed"/> event</param>
            Public Sub New(ByVal Item As T, ByVal OriginalEventArgs As EventArgs)
                MyBase.New(Item)
                Me.OriginalEventArgs = OriginalEventArgs
            End Sub
        End Class
#End Region
#Region "IList implementation - completely type unsafe, provided for CollectionEditor compatibility"
        ''' <summary>Copies the elements of the <see cref="System.Collections.ICollection"/> to an <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from <see cref="System.Collections.ICollection"/>. The <see cref="System.Array"/> must have zero-based indexing.</param>
        ''' <param name="index">The zero-based index in array at which copying begins.</param>
        ''' <exception cref="System.ArgumentNullException">array is null.</exception>
        ''' <exception cref="System.ArgumentOutOfRangeException">index is less than zero.</exception>
        ''' <exception cref="System.ArgumentException">array is multidimensional.-or- index is equal to or greater than the length of array.-or- The number of elements in the source <see cref="System.Collections.ICollection"/> is greater than the available space from index to the end of the destination array.</exception>
        ''' <exception cref="System.InvalidCastException">The type of the source <see cref="System.Collections.ICollection"/> cannot be cast automatically to the type of the destination array.</exception>
        ''' <remarks>Do not use, use type-safe <see cref="CopyTo"/> instead. Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        ''' <version version="1.5.2">Renamed from <c>CopyTo1</c> to <c>ICollection_CopyTo</c>.</version>
        <Obsolete("Use type-safe CopyTo instead"), EditorBrowsable(EditorBrowsableState.Never)>
        Public NotOverridable Overrides Sub ICollection_CopyTo(ByVal array As Array, ByVal index As Integer) 'Implements System.Collections.ICollection.CopyTo
            CType(list, IList).CopyTo(array, index)
        End Sub

        ''' <summary>Gets a value indicating whether access to the <see cref="System.Collections.ICollection"/> is synchronized (thread safe).</summary>
        ''' <returns>true if access to the <see cref="System.Collections.ICollection"/> is synchronized (thread safe); otherwise, false.</returns>
        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        <Obsolete("Although this is part of IList this is part of neither IList(Of T)  nor List(Of T)")>
        <EditorBrowsable(EditorBrowsableState.Never), Browsable(False)>
        Public NotOverridable Overrides ReadOnly Property IsSynchronized() As Boolean 'Implements System.Collections.ICollection.IsSynchronized
            Get
                Return CType(list, IList).IsSynchronized
            End Get
        End Property
        ''' <summary>Gets an object that can be used to synchronize access to the <see cref="System.Collections.ICollection"/>.</summary>
        ''' <returns>An object that can be used to synchronize access to the <see cref="System.Collections.ICollection"/></returns>
        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        <Obsolete("Although this is part of IList this is part of neither IList(Of T)  nor List(Of T)")>
        <EditorBrowsable(EditorBrowsableState.Never), Browsable(False)>
        Public NotOverridable Overrides ReadOnly Property SyncRoot() As Object 'Implements System.Collections.ICollection.SyncRoot
            Get
                Return Me
            End Get
        End Property
        ''' <summary>Adds an item to the <see cref="System.Collections.IList"/>.</summary>
        ''' <param name="value">The <see cref="System.Object"/> to add to the <see cref="System.Collections.IList"/>.</param>
        ''' <returns>The position into which the new element was inserted.</returns>
        ''' <exception cref="TypeMismatchException"><paramref name="value"/> cannot be converted into type <typeparamref name="T"/></exception>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        ''' <version version="1.5.2"><see cref="InvalidCastException"/> thrown replaced with <see cref="TypeMismatchException"/></version>
        ''' <version version="1.5.3">Renamed from <c>Add</c> to <c>IList_Add</c>. See comment on <see cref="ListWithEventsBase.IList_Add"/>.</version>
        <Obsolete("Use type-safe overload instead")>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public NotOverridable Overrides Function IList_Add(ByVal value As Object) As Integer 'Implements System.Collections.IList.Add
            Try
                Add(CType(value, T))
            Catch ex As InvalidCastException
                Throw New TypeMismatchException("value", value, GetType(T), ex)
            End Try
            Return list.Count - 1
        End Function

        ''' <summary>Determines whether the <see cref="System.Collections.IList"/> contains a specific value.</summary>
        ''' <param name="value">The <see cref="System.Object"/> to locate in the <see cref="System.Collections.IList"/>.</param>
        ''' <returns>true if the <see cref="System.Object"/> is found in the <see cref="System.Collections.IList"/>; otherwise, false.</returns>
        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        <Obsolete("Use type-safe overload instead")>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public NotOverridable Overrides Function Contains(ByVal value As Object) As Boolean 'Implements System.Collections.IList.Contains
            Try
                Return Contains(CType(value, T))
            Catch ex As Exception
                Return False
            End Try
        End Function
        ''' <param name="value">The <see cref="System.Object"/> to locate in the <see cref="System.Collections.IList"/>.</param>
        ''' <returns>The index of value if found in the list; otherwise, -1.</returns>
        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        <Obsolete("Use type-safe overload instead")>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public NotOverridable Overrides Function IndexOf(ByVal value As Object) As Integer
            Try
                Return IndexOf(CType(value, T))
            Catch ex As Exception
                Return -1
            End Try
        End Function
        ''' <param name="value">The <see cref="System.Object"/> to insert into the <see cref="System.Collections.IList"/>.</param>
        ''' <param name="index">The zero-based index at which value should be inserted.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="System.Collections.IList"/>.</exception>
        ''' <exception cref="System.NullReferenceException">value is null reference in the <see cref="System.Collections.IList"/>.</exception>
        ''' <exception cref="TypeMismatchException"><paramref name="value"/> cannot be converted to the type <typeparamref name="T"/></exception>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        ''' <version version="1.5.2"><see cref="InvalidCastException"/> thrown replaced with <see cref="TypeMismatchException"/>.</version>
        <Obsolete("Use type-safe overload instead")>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Public NotOverridable Overrides Sub Insert(ByVal index As Integer, ByVal value As Object)
            Try
                Insert(index, CType(value, T))
            Catch ex As InvalidCastException
                Throw New TypeMismatchException("value", value, GetType(T), ex)
            End Try
        End Sub
        ''' <summary>Gets a value indicating whether the <see cref="System.Collections.IList"/> has a fixed size.</summary>
        ''' <returns>Always False</returns>
        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Never), Browsable(False)>
        Public NotOverridable Overrides ReadOnly Property IsFixedSize() As Boolean
            Get
                Return False
            End Get
        End Property

        ''' <summary>Removes the first occurrence of a specific object from the <see cref="System.Collections.IList"/>.</summary>
        ''' <param name="value">The <see cref="System.Object"/> to remove from the <see cref="System.Collections.IList"/></param>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        <Obsolete("Use type-safe overload instead"), EditorBrowsable(EditorBrowsableState.Never)>
        Public NotOverridable Overrides Sub Remove(ByVal value As Object) 'Implements System.Collections.IList.Remove
            Try
                Remove(CType(value, T))
            Catch : End Try
        End Sub
        ''' <summary>Gets or sets the element at the specified index.</summary>
        ''' <param name="index">The zero-based index of the element to get or set.</param>
        ''' <returns>The element at the specified index.</returns>
        ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="System.Collections.IList"/>.</exception>
        ''' <exception cref="TypeMismatchException">When setting value that cannot be converted to <typeparamref name="T"/></exception>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True (in setter)</exception>
        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        ''' <version version="1.5.2"><see cref="InvalidCastException"/> thrown replaced with <see cref="TypeMismatchException"/></version>
        <Obsolete("Use type-safe Item property instead"), EditorBrowsable(EditorBrowsableState.Never)>
        <Browsable(False)>
        Public NotOverridable Overloads Overrides Property IList_Item(ByVal index As Integer) As Object
            Get
                Return Item(index)
            End Get
            Set(ByVal value As Object)
                Try
                    Item(index) = value
                Catch ex As InvalidCastException
                    Throw New TypeMismatchException("value", value, GetType(T), ex)
                End Try
            End Set
        End Property
#End Region
#Region "ISerializable"
        ''' <summary>Populates a <see cref="System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.</summary>
        ''' <param name="context">The destination (see <see cref="System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        ''' <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission</exception>
        ''' <remarks>
        ''' Only items (see <see cref="Item"/>) are serialized.
        ''' Note for inheritors: Call this base class method in order items to be serialized.
        ''' </remarks>
        <SecurityPermission(SecurityAction.LinkDemand, Flags:=SecurityPermissionFlag.SerializationFormatter)>
        Public Overridable Sub GetObjectData(ByVal info As SerializationInfo, ByVal context As StreamingContext) Implements ISerializable.GetObjectData
            If info Is Nothing Then
                Throw New ArgumentNullException("info")
            End If
            info.AddValue(ItemsName, Me.InternalList)
        End Sub
        ''' <summary>CTor - deserializes <see cref="ListWithEvents(Of T)"/></summary>
        ''' <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/> that contains serialized object</param>
        ''' <param name="context">The source (see <see cref="System.Runtime.Serialization.StreamingContext"/>) of this deserialization.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="info"/> is null</exception>
        ''' <exception cref="InvalidCastException">Serialized value was found but cannot be converted to type of corresponding property</exception>
        ''' <exception cref="Runtime.Serialization.SerializationException">An exception occurred during deserialization</exception>
        ''' <remarks>
        ''' Only items (see <see cref="Item"/>) are deserialized.
        ''' Note for inheritors: Call this base class CTor in order to deserialize items. Another way is to deserialize them into local variable and then use <see cref="List(Of T).Add"/> or <see cref="List(Of T).AddRange"/>.
        ''' </remarks>
        Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
            If info Is Nothing Then
                Throw New ArgumentNullException("info")
            End If
            Try
                MyClass.list = info.GetValue(ItemsName, GetType(List(Of T)))
            Catch ex As InvalidCastException
                Throw
            Catch ex As SerializationException
                Throw
            Catch ex As Exception
                Throw New SerializationException(ErrorWhileDeserializingLinkLabelItem, ex)
            End Try
        End Sub
        ''' <summary>Name used for serialization of the <see cref="InternalList"/> property</summary>
        Protected Const ItemsName$ = "Items"
#End Region


        ''' <summary>Raised when this <see cref="ListWithEvents(Of T)"/> collection changes.</summary>
        ''' <param name="sender">Source ot the event</param>
        ''' <param name="e">Event arguments. The <paramref name="e"/>.<see cref="ListChangedEventArgs.ChangeEventArgs">ChangedEventArgs</see> contains event argument of the <see cref="Changed"/> event raised immediatelly prior this event.
        ''' As of this implementation type of <paramref name="e"/>.<see cref="ListChangedEventArgs.ChangeEventArgs">ChangedEventArgs</see> is always one of following types: <see cref="ItemIndexEventArgs"/> (<see cref="Added"/>), <see cref="ItemsEventArgs"/> (<see cref="Cleared"/>), <see cref="ItemIndexEventArgs"/> (<see cref="Removed"/>), <see cref="OldNewItemEventArgs"/> (<see cref="ItemChanged"/>), <see cref="ItemValueChangedEventArgs"/> (<see cref="ItemValueChanged"/>).
        ''' Value of <paramref name="e"/>.<see cref="ListChangedEventArgs.Collection"/> is always this instance.</param>
        ''' <remarks>This event is raised immediatelly after each <see cref="Changed"/> event.<para>
        ''' The reason for having two duplicit events is that <see cref="Changed"/> implements <see cref="IReportsChange.Changed"/> and you cannot determine action (what happend) through it. The aim of this event is to concentrate <see cref="Added"/>, <see cref="Removed"/>, <see cref="Cleared"/>, <see cref="ItemChanged"/> and <see cref="ItemValueChanged"/> events to one single event which allows handler to easily dinstinguish which action happedned on collection.</para></remarks>
        Public Event CollectionChanged(sender As ListWithEvents(Of T), e As ListChangedEventArgs)
        ''' <summary>Raises the <see cref="CollectionChanged"/> event.</summary>
        ''' <param name="e">Event argument. The <paramref name="e"/>.<see cref="ListChangedEventArgs.ChangeEventArgs">ChangedEventArgs</see> should always contain event argument of preceding call of <see cref="OnChanged"/></param>
        ''' <remarks>You should call one of overloaded <see cref="OnChanged"/> methods after all calls of <see cref="OnChanged"/>.
        ''' This overridable overload is always called by the other overloads.
        ''' <para>This method calls <see cref="OnINotifyCollectionChanged_CollectionChanged"/> and thus raises the <see cref="INotifyCollectionChanged_CollectionChanged"/> event.</para></remarks>
        ''' <version version="1.5.3">Added call to <see cref="OnINotifyCollectionChanged_CollectionChanged"/> to support the <see cref="INotifyCollectionChanged"/> interface</version>
        ''' <filterpriority>2</filterpriority>
        Protected Overridable Sub OnCollectionChanged(ByVal e As ListChangedEventArgs)
            RaiseEvent CollectionChanged(Me, e)
            Select Case e.Action
                Case CollectionChangeAction.Add : OnINotifyCollectionChanged_CollectionChanged(New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, e.NewValue, e.Index))
                Case CollectionChangeAction.Clear, CollectionChangeAction.Other : OnINotifyCollectionChanged_CollectionChanged(New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset))
                Case CollectionChangeAction.Replace : OnINotifyCollectionChanged_CollectionChanged(New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, e.NewValue, e.OldValue, e.Index))
                Case CollectionChangeAction.Remove : OnINotifyCollectionChanged_CollectionChanged(New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, e.OldValue, e.Index))
            End Select
        End Sub
        ''' <summary>Raises the <see cref="CollectionChanged"/> event via calling <see cref="M:Tools.CollectionsT.GenericT.ListWithEvents`1.OnChanged(Tools.CollectionsT.GenericT.ListChangedEventArgs)"/></summary>
        ''' <param name="e">Argument of preceding call of <see cref="OnChanged"/></param>
        ''' <param name="Action">Action taken on collection</param>
        ''' <param name="OldValue">Old value at index <paramref name="index"/> prior to change. Pass null (default value for value types) if not applicable.</param>
        ''' <param name="NewValue">New value at index <paramref name="index"/> after change. pass null (default value for value types) if not applicable</param>
        ''' <param name="index">Index at which change has occurred. Pass -1 if not applicable</param>
        ''' <remarks>You should call one of overloaded <see cref="OnChanged"/> methods after all calls of <see cref="OnChanged"/>.</remarks>
        ''' <filterpriority>1</filterpriority>
        Protected Sub OnCollectionChanged(ByVal e As EventArgs, ByVal Action As CollectionChangeAction, ByVal OldValue As T, ByVal NewValue As T, ByVal index As Integer)
            OnCollectionChanged(New ListChangedEventArgs(Me, e, Action, index, OldValue, NewValue))
            Dim Action2 As ListChangedType
            Select Case Action
                Case CollectionChangeAction.Add : Action2 = ListChangedType.ItemAdded
                Case CollectionChangeAction.Clear : Action2 = ListChangedType.Reset
                Case CollectionChangeAction.ItemChange : Action2 = ListChangedType.ItemChanged
                Case CollectionChangeAction.Other : Action2 = ListChangedType.Reset
                Case CollectionChangeAction.Remove : Action2 = ListChangedType.ItemDeleted
                Case CollectionChangeAction.Replace : Action2 = ListChangedType.ItemChanged
            End Select
            Dim e2 As New ComponentModel.ListChangedEventArgs(Action2, index)
            OnListChanged(e2)
        End Sub

        ''' <summary>Specialized <see cref="CollectionChangeEventArgs(Of T)"/> for <see cref="ListWithEvents(Of T)"/></summary>
        ''' <seelaso cref="DictionaryWithEvents.DictionaryChangedEventArgs"/>
        Public Class ListChangedEventArgs : Inherits CollectionChangeEventArgs(Of T)
#Region "CTors"
            ''' <summary>CTor</summary>
            ''' <param name="ChangeEventArgs">Arguments of event that caused the collection to change</param>
            ''' <param name="Collection">Collection that was changed</param>
            ''' <param name="Action">Action which occurred on collection</param>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="Action"/> is not member of <see cref="CollectionChangeAction"/></exception>
            Public Sub New(ByVal Collection As ListWithEvents(Of T), ByVal ChangeEventArgs As EventArgs, ByVal Action As CollectionChangeAction)
                MyBase.New(Collection, ChangeEventArgs, Action)
            End Sub
            ''' <summary>CTor with index</summary>
            ''' <param name="ChangeEventArgs">Arguments of event that caused the collection to change</param>
            ''' <param name="Collection">Collection that was changed</param>
            ''' <param name="Action">Action which occurred on collection</param>
            ''' <param name="index">Index at which the change has occurred</param>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="Action"/> is not member of <see cref="CollectionChangeAction"/></exception>
            Public Sub New(ByVal Collection As ListWithEvents(Of T), ByVal ChangeEventArgs As EventArgs, ByVal Action As CollectionChangeAction, ByVal index As Integer)
                Me.New(Collection, ChangeEventArgs, Action)
                _Index = index
            End Sub
            ''' <summary>CTor with index and old and new value</summary>
            ''' <param name="ChangeEventArgs">Arguments of event that caused the collection to change</param>
            ''' <param name="Collection">Collection that was changed</param>
            ''' <param name="Action">Action which occurred on collection</param>
            ''' <param name="index">Index at which the change has occurred</param>
            ''' <param name="OldValue">Old value at index <paramref name="index"/></param>
            ''' <param name="NewValue">New value at index <paramref name="index"/></param>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="Action"/> is not member of <see cref="CollectionChangeAction"/></exception>
            Public Sub New(ByVal Collection As ListWithEvents(Of T), ByVal ChangeEventArgs As EventArgs, ByVal Action As CollectionChangeAction, ByVal index As Integer, ByVal OldValue As T, ByVal NewValue As T)
                Me.New(Collection, ChangeEventArgs, Action, index)
                _OldValue = OldValue
                _NewValue = NewValue
            End Sub
#End Region
            ''' <summary>Collection which was changed</summary>
            Public Shadows ReadOnly Property Collection() As ListWithEvents(Of T)
                Get
                    Return MyBase.Collection
                End Get
            End Property
            ''' <summary>Contains value of the <see cref="Index"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _Index As Integer = -1
            ''' <summary>Contains value of the <see cref="OldValue"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _OldValue As T = Nothing
            ''' <summary>Contains value of the <see cref="NewValue"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _NewValue As T = Nothing
            ''' <summary>Gets index at which change occurred (if applicable)</summary>
            ''' <returns>Original index where the change has ocured. If not applicable returns -1</returns>
            Public ReadOnly Property Index() As Integer
                <DebuggerStepThrough()> Get
                    Return _Index
                End Get
            End Property
            ''' <summary>Gets value on index <see cref="Index"/> before change (if applicable)</summary>
            ''' <returns>Original value at index <see cref="Index"/>. If not applicable returns null (for reference types) or type default value (for <see cref="System.ValueType">value types</see>)</returns>
            Public ReadOnly Property OldValue() As T
                Get
                    Return _OldValue
                End Get
            End Property
            ''' <summary>Gets value on index <see cref="Index"/> after change (if applicable)</summary>
            ''' <returns>Valu at index <see cref="Index"/> after changed. If not applicable returns null (for reference types) or type default value (for <see cref="System.ValueType">value types</see>)</returns>
            Public ReadOnly Property NewValue() As T
                Get
                    Return _NewValue
                End Get
            End Property
        End Class
#Region "IBindingList"
        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <param name="property">ignored</param>
        ''' <exception cref="T:System.NotSupportedException">always</exception>
        ''' <version version="1.5.2">Access changed to protected, <see cref="EditorBrowsableAttribute"/> added</version>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected NotOverridable Overrides Sub AddIndex(ByVal [property] As PropertyDescriptor) 'Implements System.ComponentModel.IBindingList.AddIndex
            Throw New NotSupportedException(ListWithEventsDoesNotSupportSearching)
        End Sub

        ''' <summary>Adds a new item to the list.</summary>
        ''' <returns>The item added to the list.</returns>
        ''' <exception cref="T:System.NotSupportedException"><see cref="P:System.ComponentModel.IBindingList.AllowNew" /> is false. </exception>
        ''' <remarks>Use type-safe <see cref="AddNew"/> instead
        ''' <para>Note for inheritors: In order ot override this member override <see cref="AddNew"/>.</para></remarks>
        ''' <seelaso cref="AddNew"/>
        ''' <version version="1.5.2">Access changed to protected</version>
        Protected NotOverridable Overrides Function IBindingList_AddNew() As Object 'Implements System.ComponentModel.IBindingList.AddNew
            Return AddNew()
        End Function
        ''' <summary>Adds a new item to the list.</summary>
        ''' <returns>The item added to the list.</returns>
        ''' <exception cref="T:System.NotSupportedException"><see cref="P:System.ComponentModel.IBindingList.AllowNew" /> is false. </exception>
        ''' <exception cref="System.ArgumentException"><typeparamref name="T"/> is not a RuntimeType. -or- <typeparamref name="T"/> is an open generic type (that is, the <see cref="System.Type.ContainsGenericParameters" /> property returns true).</exception>
        ''' <exception cref="System.NotSupportedException"><typeparamref name="T"/> cannot be a <see cref="System.Reflection.Emit.TypeBuilder" />.  -or- Creation of <see cref="System.TypedReference" />, <see cref="System.ArgIterator" />, <see cref="System.Void" />, and <see cref="System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.</exception>
        ''' <exception cref="System.Reflection.TargetInvocationException">The constructor of <typeparamref name="T"/> being called throws an exception.</exception>
        ''' <exception cref="System.MethodAccessException">The caller does not have permission to call defualt constructor of <typeparamref name="T"/>.</exception>
        ''' <exception cref="System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
        ''' <exception cref="System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through Overload:<see cref="System.Type.GetTypeFromProgID" /> or Overload:<see cref="System.Type.GetTypeFromCLSID" />.</exception>
        ''' <exception cref="System.MissingMethodException">No matching public constructor was found.</exception>
        ''' <exception cref="System.Runtime.InteropServices.COMException"><typeparamref name="T"/> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered.</exception>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <exception cref="OperationCanceledException">Operation is canceled in event handler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Adding"/> event -or- Any exception may be thrown by <see cref="ICollectionCancelItem.OnAdding"/> when <see cref="AllowItemCancel"/> is true.</exception>
        ''' <seelaso cref="CreateInstance"/>
        ''' <remarks>This member implements <see cref="IBindingList.AddNew"/></remarks>
        Public Overridable Function AddNew() As T
            If CanAddNew Then
                Dim NewItem As T = GetType(T).CreateInstance
                Me.Add(NewItem)
                Return NewItem
            Else
                Throw New NotSupportedException("Type {0} does not support instance creation.")
            End If
        End Function


        ''' <summary>Gets whether you can update items in the list.</summary>
        ''' <returns>true</returns>
        ''' <version version="1.5.2">Access changed form private to protected, <see cref="EditorBrowsableAttribute"/> added</version>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected NotOverridable Overrides ReadOnly Property AllowEdit() As Boolean 'Implements System.ComponentModel.IBindingList.AllowEdit
            Get
                Return True
            End Get
        End Property

        ''' <summary>Gets whether you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
        ''' <returns>true if you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew" />; otherwise, false.
        ''' This impúlementation returns ture when type <typeparamref name="T"/> has default constructor that can be used for automatic instance creation</returns>
        ''' <seelaso cref="CanAutomaticallyCreateInstance"/>
        ''' <remarks>This member implements <see cref="IBindingList.AllowNew"/></remarks>
        Public Overrides ReadOnly Property CanAddNew() As Boolean 'Implements System.ComponentModel.IBindingList.AllowNew
            Get
                Return GetType(T).CanAutomaticallyCreateInstance()
            End Get
        End Property

        ''' <summary>Gets whether you can remove items from the list, using <see cref="M:System.Collections.IList.Remove(System.Object)" /> or <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
        ''' <returns>true</returns>
        ''' <version version="1.5.2">Access changed from private to protected. <see cref="EditorBrowsableAttribute"/> added.</version>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected NotOverridable Overrides ReadOnly Property AllowRemove() As Boolean 'Implements System.ComponentModel.IBindingList.AllowRemove
            Get
                Return True
            End Get
        End Property

        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <exception cref="T:System.NotSupportedException">always</exception>
        ''' <param name="direction">Ignored</param><param name="property">Ignored</param>
        ''' <version version="1.5.2">Access changed from private to protected. <see cref="EditorBrowsableAttribute"/> added.</version>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected NotOverridable Overrides Sub ApplySort(ByVal [property] As PropertyDescriptor, ByVal direction As ListSortDirection) 'Implements System.ComponentModel.IBindingList.ApplySort
            Throw New NotSupportedException(ListWithEventsDoesNotSupportSorting)
        End Sub

        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <returns>This function never returns value</returns>
        ''' <exception cref="T:System.NotSupportedException">always</exception>
        ''' <param name="key">Ignored</param><param name="property">Ignored</param>
        ''' <version version="1.5.2">Access changed from private to protected. <see cref="EditorBrowsableAttribute"/> added.</version>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected NotOverridable Overrides Function Find(ByVal [property] As PropertyDescriptor, ByVal key As Object) As Integer ' Implements System.ComponentModel.IBindingList.Find
            Throw New NotSupportedException(ListWithEventsDoesNotSupportSearching)
        End Function

        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <returns>This property never returns value</returns>
        ''' <exception cref="T:System.NotSupportedException">always</exception>
        ''' <version version="1.5.2">Access changed from private to protected. <see cref="EditorBrowsableAttribute"/> added.</version>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected NotOverridable Overrides ReadOnly Property IsSorted() As Boolean ' Implements System.ComponentModel.IBindingList.IsSorted
            Get
                Throw New NotSupportedException(ListWithEventsDoesNotSupportSearching)
            End Get
        End Property

        '''' <summary>Occurs when the list changes or an item in the list changes.</summary>
        'Private Event ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Implements System.ComponentModel.IBindingList.ListChanged
        '''' <summary>Raises the <see cref="ListChanged"/> event</summary>
        '''' <param name="e">Event arguments</param>
        'Private Sub OnListChanged(ByVal e As System.ComponentModel.ListChangedEventArgs)
        '    RaiseEvent ListChanged(Me, e)
        'End Sub
        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <param name="property">ignored</param>
        ''' <exception cref="T:System.NotSupportedException">always</exception>
        ''' <version version="1.5.2">Access changed from private to protected. <see cref="EditorBrowsableAttribute"/> added.</version>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected NotOverridable Overrides Sub RemoveIndex(ByVal [property] As PropertyDescriptor) 'Implements System.ComponentModel.IBindingList.RemoveIndex
            Throw New NotSupportedException(ListWithEventsDoesNotSupportSearching)
        End Sub

        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <exception cref="T:System.NotSupportedException">always</exception>
        ''' <version version="1.5.2">Access changed from private to protected. <see cref="EditorBrowsableAttribute"/> added.</version>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected NotOverridable Overrides Sub RemoveSort() 'Implements System.ComponentModel.IBindingList.RemoveSort
            Throw New NotSupportedException(ListWithEventsDoesNotSupportSorting)
        End Sub
        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <returns>This property never returns value</returns>
        ''' <exception cref="T:System.NotSupportedException">always</exception>
        ''' <version version="1.5.2">Access changed from private to protected. <see cref="EditorBrowsableAttribute"/> added.</version>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected NotOverridable Overrides ReadOnly Property SortDirection() As ListSortDirection 'Implements System.ComponentModel.IBindingList.SortDirection
            Get
                Throw New NotSupportedException(ListWithEventsDoesNotSupportSorting)
            End Get
        End Property

        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <returns>This property never returns value</returns>
        ''' <exception cref="T:System.NotSupportedException">always</exception>
        ''' <version version="1.5.2">Access changed from private to protected. <see cref="EditorBrowsableAttribute"/> added.</version>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected NotOverridable Overrides ReadOnly Property SortProperty() As PropertyDescriptor 'Implements System.ComponentModel.IBindingList.SortProperty
            Get
                Throw New NotSupportedException(ListWithEventsDoesNotSupportSorting)
            End Get
        End Property

        ''' <summary>Gets whether a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or an item in the list changes.</summary>
        ''' <returns>true</returns>
        ''' <version version="1.5.2">Access changed from private to protected. <see cref="EditorBrowsableAttribute"/> added.</version>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected NotOverridable Overrides ReadOnly Property SupportsChangeNotification() As Boolean 'Implements System.ComponentModel.IBindingList.SupportsChangeNotification
            Get
                Return True
            End Get
        End Property

        ''' <summary>Gets whether the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method.</summary>
        ''' <returns>false</returns>
        ''' <version version="1.5.2">Access changed from private to protected. <see cref="EditorBrowsableAttribute"/> added.</version>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected NotOverridable Overrides ReadOnly Property SupportsSearching() As Boolean 'Implements System.ComponentModel.IBindingList.SupportsSearching
            Get
                Return False
            End Get
        End Property

        ''' <summary>Gets whether the list supports sorting.</summary>
        ''' <returns>false</returns>
        ''' <version version="1.5.2">Access changed from private to protected. <see cref="EditorBrowsableAttribute"/> added.</version>
        <EditorBrowsable(EditorBrowsableState.Never)>
        Protected NotOverridable Overrides ReadOnly Property SupportsSorting() As Boolean 'Implements System.ComponentModel.IBindingList.SupportsSorting
            Get
                Return False
            End Get
        End Property
#End Region
    End Class
    ''' <summary>Describes acction on collection</summary>
    Public Enum CollectionChangeAction
        ''' <summary>An item was added. Equals to <see cref="ComponentModel.CollectionChangeAction.Add"/>. Represents <see cref="ListWithEvents(Of Object).Added"/>.</summary>
        Add = ComponentModel.CollectionChangeAction.Add
        ''' <summary>An item was removed. Equals to <see cref="ComponentModel.CollectionChangeAction.Remove"/>. Represents <see cref="ListWithEvents(Of Object).Removed"/></summary>
        Remove = ComponentModel.CollectionChangeAction.Remove
        ''' <summary>The collection was cleared. Represents <see cref="ListWithEvents(Of Object).Cleared"/>.</summary>
        Clear = 4
        ''' <summary>Item of collection was replaced. Represents <see cref="ListWithEvents(Of Object).ItemChanged"/>.</summary>
        Replace = 5
        ''' <summary>Property of item of collection changed. Represents <see cref="ListWithEvents(Of Object).ItemValueChanged"/>.</summary>
        ItemChange = 6
        ''' <summary>Unspecified action. Equals to <see cref="ComponentModel.CollectionChangeAction.Refresh"/>.</summary>
        Other = ComponentModel.CollectionChangeAction.Refresh
    End Enum
    ''' <summary>Represents common base for generic classes <see cref="CollectionChangeEventArgs(Of T)"/></summary>
    Public MustInherit Class CollectionChangedEventArgsBase
        Inherits CollectionChangeEventArgs

        ''' <summary>CTor</summary>
        ''' <param name="changeEventArgs">Arguments of event that caused the collection to change</param>
        ''' <param name="collection">Collection that was changed</param>
        ''' <remarks><see cref="Action"/> is set to <see cref="CollectionChangeAction.Other"/></remarks>
        ''' <version version="1.5.4">Parameter <c>Action</c> renamed to <c>action</c> and <c>ChangeEventArgs</c> to <c>changeEventArgs</c></version>
        Protected Sub New(ByVal collection As IEnumerable, ByVal changeEventArgs As EventArgs)
            Me.New(collection, changeEventArgs, CollectionChangeAction.Other)
        End Sub

        ''' <summary>CTor</summary>
        ''' <param name="changeEventArgs">Arguments of event that caused the collection to change</param>
        ''' <param name="collection">Collection that was changed</param>
        ''' <param name="action">Action which occurred on collection</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Action"/> is not member of <see cref="CollectionChangeAction"/></exception>
        ''' <version version="1.5.4">Parameter <c>Action</c> renamed to <c>action</c>, <c>Collection</c> to <c>collection</c> and <c>ChangeEventArgs</c> to <c>changeEventArgs</c></version>
        Protected Sub New(ByVal collection As IEnumerable, ByVal changeEventArgs As EventArgs, ByVal action As CollectionChangeAction)
            MyBase.New(ConvertAction(action), collection)
            Me.ChangeEventArgs = changeEventArgs
            Me.Action = action
        End Sub

        ''' <summary>Converts <see cref="CollectionChangeAction"/> to <see cref="ComponentModel.CollectionChangeAction"/></summary>
        ''' <param name="action">A <see cref="CollectionChangeAction"/> to be converted</param>
        ''' <returns><see cref="ComponentModel.CollectionChangeAction"/> corresponding to <paramref name="action"/></returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="action"/> is not member of <see cref="CollectionChangeAction"/></exception>
        ''' <remarks>Conversion table follows:
        ''' <list type="table">
        ''' <listheader><term><see cref="CollectionChangeAction"/> (<paramref name="action"/>)</term><description><see cref="ComponentModel.CollectionChangeAction"/></description></listheader>
        ''' <item><term><see cref="CollectionChangeAction.Add"/></term><description><see cref="ComponentModel.CollectionChangeAction.Add"/></description></item>
        ''' <item><term><see cref="CollectionChangeAction.Remove"/></term><description><see cref="ComponentModel.CollectionChangeAction.Remove"/></description></item>
        ''' <item><term><see cref="CollectionChangeAction.Other"/></term><description><see cref="ComponentModel.CollectionChangeAction.Refresh"/></description></item>
        ''' <item><term><see cref="CollectionChangeAction.Clear"/></term><description><see cref="ComponentModel.CollectionChangeAction.Remove"/></description></item>
        ''' <item><term><see cref="CollectionChangeAction.Replace"/></term><description><see cref="ComponentModel.CollectionChangeAction.Refresh"/></description></item>
        ''' <item><term><see cref="CollectionChangeAction.ItemChange"/></term><description><see cref="ComponentModel.CollectionChangeAction.Refresh"/></description></item>
        ''' <item><term>Otherwise</term><description><see cref="InvalidEnumArgumentException"/> thrown</description></item>
        ''' </list>
        ''' </remarks>
        ''' <version version="1.5.4">Parameter <c>Action</c> renamed to <c>action</c></version>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Shared Function ConvertAction(ByVal action As CollectionChangeAction) As ComponentModel.CollectionChangeAction
            Select Case action
                Case CollectionChangeAction.Add, CollectionChangeAction.Remove, CollectionChangeAction.Other
                    Return action
                Case CollectionChangeAction.Clear : Return ComponentModel.CollectionChangeAction.Remove
                Case CollectionChangeAction.Replace, CollectionChangeAction.ItemChange
                    Return ComponentModel.CollectionChangeAction.Refresh
                Case Else : Throw New InvalidEnumArgumentException("action", action, GetType(CollectionChangeAction))
            End Select
        End Function

        ''' <summary>Arguments of event that caused collection to be changed or that was raised by the collection on change</summary>
        Public ReadOnly Property ChangeEventArgs As EventArgs

        ''' <summary>Action taken on collection</summary>
        Public Shadows ReadOnly Property Action As CollectionChangeAction
    End Class

    ''' <summary>Arguments of event raised when collection owned by event source has changed</summary>
    Public Class CollectionChangeEventArgs(Of TItem)
        Inherits CollectionChangedEventArgsBase
        ''' <summary>CTor</summary>
        ''' <param name="ChangeEventArgs">Arguments of event that caused the collection to change</param>
        ''' <param name="Collection">Collection that was changed</param>
        ''' <remarks><see cref="Action"/> is set to <see cref="CollectionChangeAction.Other"/></remarks>
        ''' <version version="1.5.4">Parameter <c>Collection</c> renamed to <c>collection</c> and <c>ChangeEventArgs</c> to <c>changeEventArgs</c></version>
        Public Sub New(ByVal collection As ICollection(Of TItem), ByVal changeEventArgs As EventArgs)
            MyBase.New(collection, changeEventArgs)
        End Sub

        ''' <summary>CTor</summary>
        ''' <param name="ChangeEventArgs">Arguments of event that caused the collection to change</param>
        ''' <param name="Collection">Collection that was changed</param>
        ''' <param name="Action">Action which occurred on collection</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Action"/> is not member of <see cref="CollectionChangeAction"/></exception>
        ''' <version version="1.5.4">Parameter <c>Collection</c> renamed to <c>collection</c>, <c>Action</c> to <c>Action</c> and <c>ChangeEventArgs</c> to <c>changeEventArgs</c></version>
        Public Sub New(ByVal collection As ICollection(Of TItem), ByVal changeEventArgs As EventArgs, ByVal action As CollectionChangeAction)
            MyBase.New(collection, changeEventArgs, action)
        End Sub

        ''' <summary>Collection which was changed</summary>
        Public ReadOnly Property Collection() As ICollection(Of TItem)
            Get
                Return MyBase.Element
            End Get
        End Property
    End Class

    ''' <summary>Interface of collection item notified when added to collection</summary>
    ''' <remarks>Only few collections, such as <see cref="ListWithEvents(Of T)"/> supports this interface</remarks>
    ''' <seelaso cref="ICollectionCancelItem"/>
    ''' <version version="1.5.2" stage="Release">Interface introduced</version>
    Public Interface ICollectionNotifyItem
        ''' <summary>Called after item is added to collection</summary>
        ''' <param name="Collection">Collection item was added into</param>
        ''' <param name="index">Index at which the item was added. Note: Index may change later without notice (i.e. when collection gets sorted). If collection does not support indexing value is null.</param>
        ''' <version version="1.5.4">Parameter <c>Collection</c> renamed to <c>collection</c></version>
        Sub OnAdded(ByVal collection As ICollection, ByVal index? As Integer)
        ''' <summary>Called after item is removed from collection (or after collection was cleared)</summary>
        ''' <param name="Collection">Collection item was removed from</param>
        ''' <version version="1.5.4">Parameter <c>Collection</c> renamed to <c>collection</c></version>
        Sub OnRemoved(ByVal collection As ICollection)
        ''' <summary>If supported by collection item gets all the collections item is in</summary>
        ''' <returns>All the collections item is placed in; null when not supported by item class.</returns>
        ''' <remarks>You should not sport this property when your item is value type (structure).
        ''' <para>If item is placed multiple times in the same collection, this property should contain this collection multiple times.</para></remarks>
        ReadOnly Property Collections() As IEnumerable(Of ICollection)
    End Interface

    ''' <summary>Interface of collection item notified before and after added to collection</summary>
    ''' <remarks>Only few collections, such as <see cref="ListWithEvents(Of T)"/> supports this interface</remarks>
    ''' <version version="1.5.2" stage="Release">Interface introduced</version>
    Public Interface ICollectionCancelItem
        Inherits ICollectionNotifyItem
        ''' <summary>Called before item is placed into collection</summary>
        ''' <param name="collection">Collection item is about to be placed into</param>
        ''' <param name="index">Index item is being to be placed onto; null when collection does not support indexing.</param>
        ''' <remarks>To cancel adding, throw exception. Collection does not call this method when it does not allow cancellation of adding.
        ''' <para>Call to <see cref="OnAdding"/> does not necessarily mean that <see cref="ICollectionNotifyItem.OnAdded"/> will be called, because event can be canceled.</para></remarks>
        ''' <param name="Replace">True when item at index <paramref name="index"/> will be replaced by this instance; false if this instance will be inserted at <paramref name="index"/> and all subsequent items will be moved to nex index.</param>
        ''' <exception cref="Exception">Any exception may be thrown to cancel the operation. Exception is passed by collection to caller.</exception>
        ''' <version version="1.5.4">Parameter <c>Collection</c> renamed to <c>collection</c> and <c>Replace</c> to <c>replace</c></version>
        Sub OnAdding(ByVal collection As ICollection, ByVal index? As Integer, ByVal replace As Boolean)

        ''' <summary>Called before item is removed from collection</summary>
        ''' <param name="Collection">Collection item is about to be removed from</param>
        ''' <param name="index">Index item is currently placed on; null when collection does not support indexing.</param>
        ''' <remarks>To cancel removing, throw exception. Collection does not call this method when it does not allow cancellation of removing.
        ''' <para>Call to <see cref="OnRemoving"/> does not necessarily mean that <see cref="ICollectionNotifyItem.OnRemoved"/> will be called, because event can be canceled.</para></remarks>
        ''' <exception cref="Exception">Any exception may be thrown to cancel the operation. Exception is passed by collection to caller.</exception>
        ''' <version version="1.5.4">Parameter <c>Collection</c> renamed to <c>collection</c></version>
        Sub OnRemoving(ByVal collection As ICollection, ByVal index? As Integer)

        ''' <summary>Called before all items are removed from collection by clearing it</summary>
        ''' <param name="Collection">Collection item is about to be removed from</param>
        ''' <remarks>To cancel clearing, throw exception. Collection does not call this method when it does not allow cancellation of clearing.</remarks>
        ''' <exception cref="Exception">Any exception may be thrown to cancel the operation. Exception is passed by collection to caller.</exception>
        ''' <version version="1.5.4">Parameter <c>Collection</c> renamed to <c>collection</c></version>
        Sub OnClearing(ByVal collection As ICollection)
    End Interface

End Namespace
#End If