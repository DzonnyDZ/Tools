Imports System.ComponentModel.Design.Serialization, Tools.ComponentModelT, Tools.VisualBasicT
#If True
Namespace CollectionsT.GenericT
    ''' <summary>List that provides events when changed</summary>
    ''' <typeparam name="TValue">Type of items to be stored in the list</typeparam>
    ''' <typeparam name="TKey">Type of key of dictionary</typeparam>
    ''' <remarks><para>
    ''' If item of type tha implements the <see cref="IReportsChange"/> interface is passed to this list, than it's events <see cref="IReportsChange.Changed"/> are reported through <see cref="ListWithEvents.ItemValueChanged"/> event.
    ''' </para><para>
    ''' Implementation of interface <see cref="IList"/> is provided only in orer this class to be compatible with <see cref="System.ComponentModel.Design.CollectionEditor"/>.
    ''' </para>
    ''' </remarks>
    ''' <seealso cref="ListWithEvents(Of T)"/>
    ''' <author www="http://dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    ''' <version version="1.5.2"><see cref="IEnumerable(Of T)"/>[<typeparamref name="TValue"/>] implemented</version>
    <DebuggerDisplay("Count = {Count}")> _
        Public Class DictionaryWithEvents(Of TKey, TValue)
        Implements IDictionary, IDictionary(Of TKey, TValue), IEnumerable(Of TValue)
        Implements IReportsChange
#Region "CTors"
        ''' <summary>CTor</summary>
        ''' <param name="AddingReadOnly">Value of <see cref="AddingReadOnly"/> property that determines <see cref="CancelableKeyValueEventArgs.[ReadOnly]"/> property value for the <see cref="Adding"/> and <see cref="ItemChanging"/> events</param>
        ''' <param name="CancelError">Value of <see cref="CancelError"/> that determines if and <see cref="OperationCanceledException"/> is thrown when item operation is canceled in event handler.</param>
        Public Sub New(Optional ByVal AddingReadOnly As Boolean = False, Optional ByVal CancelError As Boolean = False)
            _AddingReadOnly = AddingReadOnly
            _CancelError = CancelError
            Dict = New Dictionary(Of TKey, TValue)
        End Sub
        '''' <summary>Copies all elements of this collection to new <see cref="Array"/></summary>
        'Public Function ToArray() As TKey()
        '    Return InternalDict.ToArray
        'End Function
        ''' <summary>CTor - initializes from another <see cref="IDictionary(Of TKey,Tvalue)"/></summary>
        ''' <param name="dictionary">The <see cref="System.Collections.Generic.IDictionary(Of TKey, TValue)"/> whose elements are copied to the new <see cref="DictionaryWithEvents(Of TKey, TValue)"/>.</param>
        ''' <param name="AddingReadOnly">Value of <see cref="AddingReadOnly"/> property that determines <see cref="CancelableKeyValueEventArgs.[ReadOnly]"/> property value for the <see cref="Adding"/> and <see cref="ItemChanging"/> events</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="dictionary"/> is null</exception>
        Public Sub New(ByVal dictionary As IDictionary(Of TKey, TValue), Optional ByVal AddingReadOnly As Boolean = False, Optional ByVal CancelError As Boolean = False)
            _AddingReadOnly = AddingReadOnly
            _CancelError = CancelError
            Dict = New Dictionary(Of TKey, TValue)(dictionary)
            AddAllItemHandlers()
        End Sub
        ''' <summary>Initializes a new instance of the <see cref="DictionaryWithEvents(Of TKey,TValue)"/> class that is empty and has the specified initial capacity.</summary>
        ''' <param name="capacity">The number of elements that the new dictionary can initially store.</param>
        ''' <param name="AddingReadOnly">Value of <see cref="AddingReadOnly"/> property that determines <see cref="CancelableKeyValueEventArgs.[ReadOnly]"/> property value for the <see cref="Adding"/> and <see cref="ItemChanging"/> events</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">capacity is less than 0</exception>
        ''' <param name="CancelError">Value of <see cref="CancelError"/> that determines if and <see cref="OperationCanceledException"/> is thrown when item operation is canceled in event handler.</param>
        Public Sub New(ByVal capacity As Integer, Optional ByVal AddingReadOnly As Boolean = False, Optional ByVal CancelError As Boolean = False)
            _AddingReadOnly = AddingReadOnly
            _CancelError = CancelError
            Dict = New Dictionary(Of TKey, TValue)(capacity)
        End Sub

        ''' <summary>Initializes a new instance of the <see cref="DictionaryWithEvents(Of TKey,TValue)"/> class that is empty and has the specified initial capacity, and uses the specified <see cref="System.Collections.Generic.IEqualityComparer(Of T)"/>.</summary>
        ''' <param name="capacity">The number of elements that the new dictionary can initially store.</param>
        ''' <param name="AddingReadOnly">Value of <see cref="AddingReadOnly"/> property that determines <see cref="CancelableKeyValueEventArgs.[ReadOnly]"/> property value for the <see cref="Adding"/> and <see cref="ItemChanging"/> events</param>
        ''' <param name="Comparer">The <see cref="System.Collections.Generic.IEqualityComparer(Of TKey)"/> implementation to use when comparing keys, or null to use the default <see cref="System.Collections.Generic.EqualityComparer(Of TKey)"/> for the type of the key.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">capacity is less than 0</exception>
        ''' <param name="CancelError">Value of <see cref="CancelError"/> that determines if and <see cref="OperationCanceledException"/> is thrown when item operation is canceled in event handler.</param>
        Public Sub New(ByVal capacity As Integer, ByVal Comparer As IEqualityComparer(Of TKey), Optional ByVal AddingReadOnly As Boolean = False, Optional ByVal CancelError As Boolean = False)
            _AddingReadOnly = AddingReadOnly
            _CancelError = CancelError
            Dict = New Dictionary(Of TKey, TValue)(capacity, Comparer)
        End Sub
        ''' <summary>Initializes a new instance of the <see cref="DictionaryWithEvents(Of TKey,Tvalue)"/> class that is empty, uses the specified <see cref="System.Collections.Generic.IEqualityComparer(Of T)"/>.</summary>
        ''' <param name="AddingReadOnly">Value of <see cref="AddingReadOnly"/> property that determines <see cref="CancelableKeyValueEventArgs.[ReadOnly]"/> property value for the <see cref="Adding"/> and <see cref="ItemChanging"/> events</param>
        ''' <param name="Comparer">The <see cref="System.Collections.Generic.IEqualityComparer(Of TKey)"/> implementation to use when comparing keys, or null to use the default <see cref="System.Collections.Generic.EqualityComparer(Of TKey)"/> for the type of the key.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">capacity is less than 0</exception>
        ''' <param name="CancelError">Value of <see cref="CancelError"/> that determines if and <see cref="OperationCanceledException"/> is thrown when item operation is canceled in event handler.</param>
        Public Sub New(ByVal Comparer As IEqualityComparer(Of TKey), Optional ByVal AddingReadOnly As Boolean = False, Optional ByVal CancelError As Boolean = False)
            _AddingReadOnly = AddingReadOnly
            _CancelError = CancelError
            Dict = New Dictionary(Of TKey, TValue)(Comparer)
        End Sub
        ''' <summary>Initializes a new instance of the <see cref="DictionaryWithEvents(Of TKey,Tvalue)"/> class with given instance of <see cref="IDictionary(Of TKey, Tvalue)"/>, and uses the specified <see cref="System.Collections.Generic.IEqualityComparer(Of T)"/>.</summary>
        ''' <param name="AddingReadOnly">Value of <see cref="AddingReadOnly"/> property that determines <see cref="CancelableKeyValueEventArgs.[ReadOnly]"/> property value for the <see cref="Adding"/> and <see cref="ItemChanging"/> events</param>
        ''' <param name="Comparer">The <see cref="System.Collections.Generic.IEqualityComparer(Of TKey)"/> implementation to use when comparing keys, or null to use the default <see cref="System.Collections.Generic.EqualityComparer(Of TKey)"/> for the type of the key.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">capacity is less than 0</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="dictionary"/> is null</exception>
        ''' <param name="CancelError">Value of <see cref="CancelError"/> that determines if and <see cref="OperationCanceledException"/> is thrown when item operation is canceled in event handler.</param>
        ''' <param name="dictionary">The <see cref="System.Collections.Generic.IDictionary(Of TKey, TValue)"/> whose elements are copied to the new <see cref="DictionaryWithEvents(Of TKey, TValue)"/>.</param>
        Public Sub New(ByVal dictionary As IDictionary(Of TKey, TValue), ByVal Comparer As IEqualityComparer(Of TKey), Optional ByVal AddingReadOnly As Boolean = False, Optional ByVal CancelError As Boolean = False)
            _AddingReadOnly = AddingReadOnly
            _CancelError = CancelError
            Dict = New Dictionary(Of TKey, TValue)(dictionary, Comparer)
        End Sub
#End Region

        ''' <summary>Internal list that is used for soring values</summary>
        Private Dict As Dictionary(Of TKey, TValue)
#Region "Settings"
        ''' <summary>Contains value of the <see cref="AddingReadOnly"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private ReadOnly _AddingReadOnly As Boolean = False
        ''' <summary>Determines <see cref="CancelableKeyValueEventArgs.[ReadOnly]"/> property value for the <see cref="Adding"/> and <see cref="ItemChanging"/> events</summary>
        <Browsable(False)> _
        Public ReadOnly Property AddingReadOnly() As Boolean
            <DebuggerStepThrough()> Get
                Return _AddingReadOnly
            End Get
        End Property

        ''' <summary>Contains value of the <see cref="CancelError"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private ReadOnly _CancelError As Boolean = False
        ''' <summary>Gets value indicating if an <see cref="OperationCanceledException"/> is thrown when item operation is canceled in event handler.</summary>
        <Browsable(False)> _
        Public ReadOnly Property CancelError() As Boolean
            <DebuggerStepThrough()> Get
                Return _CancelError
            End Get
        End Property

        ''' <summary>Contains value of the <see cref="AllowAddCancelableEventsHandlers"/> property</summary>
        Private _AllowAddCancelableEventsHandlers As Boolean = True
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
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property AllowAddCancelableEventsHandlers() As Boolean
            <DebuggerStepThrough()> Get
                Return _AllowAddCancelableEventsHandlers
            End Get
            Set(ByVal value As Boolean)
                If value = False Then
                    _AllowAddCancelableEventsHandlers = value
                ElseIf value = True AndAlso AllowAddCancelableEventsHandlers = False Then
                    Throw New InvalidOperationException(ResourcesT.Exceptions.AllowAddCancelableEventsHandlersCanBeChangedOnlyFromTrueToFalse)
                End If
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Locked"/></summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Locked As Boolean = False
        ''' <summary>Determines if the <see cref="DictionaryWithEvents(Of TKey,TValue)"/> isn locked (being locked prevents if from being edited)</summary>
        ''' <remarks><para>
        ''' <see cref="DictionaryWithEvents(Of TKey,TValue)"/> is usually locked while some events' handlers are being invoked.
        ''' </para><list>
        ''' <listheader><see cref="Locked"/> set to True blocks following methods and causes <see cref="InvalidOperationException"/> exception to be thrown there:</listheader>
        ''' <item><see cref="Add"/></item>
        ''' <item><see cref="Remove"/></item>
        ''' <item><see cref="Clear"/></item>
        ''' <item><see cref="Item"/> (only setter)</item>
        ''' </list></remarks>
        <Browsable(False)> _
        Public ReadOnly Property Locked() As Boolean
            <DebuggerStepThrough()> Get
                Return _Locked
            End Get
        End Property
        ''' <summary>Sets the <see cref="Locked"/> to True</summary>
        Protected Sub Lock()
            _Locked = True
        End Sub
        ''' <summary>Sets the <see cref="Locked"/> to False</summary>
        Protected Sub Unlock()
            _Locked = False
        End Sub
#End Region
#Region "Add"
        ''' <summary>List of <see cref="ItemCancelEventHandler"/> delegates to be invoked when the <see cref="Adding"/> event is raised</summary>
        Private AddingEventHandlerList As New List(Of ItemCancelEventHandler)
        ''' <summary>Delegate of handler of <see cref="Adding"/>, <see cref="Removing"/> and <see cref="ItemChanging"/> events</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        Public Delegate Sub ItemCancelEventHandler(ByVal sender As DictionaryWithEvents(Of TKey, TValue), ByVal e As CancelableKeyValueEventArgs)
        ''' <summary>Raised before an item is added to the list. Raised by the <see cref="Add"/> methods</summary>
        ''' <remarks><para>
        ''' This event can be disabled (see <see cref="AllowAddCancelableEventsHandlers"/>.
        ''' This means that <see cref="InvalidOperationException"/> is thrown when adding handler and <see cref="AllowAddCancelableEventsHandlers"/> is False.
        ''' </para><para>
        ''' Value of parameter <paramref name="e"/>'s <see cref="CancelableKeyValueEventArgs.Item"/> can be changed if <see cref="AddingReadOnly"/> is False.
        ''' </para></remarks>
        ''' <exception cref="InvalidOperationException">Adding handler when <see cref="AllowAddCancelableEventsHandlers"/> is false</exception>
        Public Custom Event Adding As ItemCancelEventHandler
            AddHandler(ByVal value As ItemCancelEventHandler)
                If AllowAddCancelableEventsHandlers Then
                    AddingEventHandlerList.Add(value)
                Else
                    Throw New InvalidOperationException(ResourcesT.Exceptions.CannotAddHandlerToTheAddingEventWhenAllowAddCancelableEventsHandlersIsFalse)
                End If
            End AddHandler
            <DebuggerStepThrough()> _
            RemoveHandler(ByVal value As ItemCancelEventHandler)
                AddingEventHandlerList.Remove(value)
            End RemoveHandler
            RaiseEvent(ByVal sender As DictionaryWithEvents(Of TKey, TValue), ByVal e As DictionaryWithEvents(Of TKey, TValue).CancelableKeyValueEventArgs)
                For Each Handler As ItemCancelEventHandler In AddingEventHandlerList
                    Handler.Invoke(sender, e)
                Next Handler
            End RaiseEvent
        End Event
        ''' <summary>Raised after an item is added to the list. Raised by the <see cref="Add"/> method</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        Public Event Added(ByVal sender As DictionaryWithEvents(Of TKey, TValue), ByVal e As KeyValueEventArgs)

        ''' <summary>Adds an item to the <see cref="DictionaryWithEvents(Of TKey,TValue)"/>.</summary>
        ''' <param name="value">The object to add to the <see cref="DictionaryWithEvents(Of TKey,TValue)"/>.</param>
        ''' <param name="key">Key of object being added</param>
        ''' <remarks>Note for inheritors: Call <see cref="OnAdding"/> before adding an item to the list and <see cref="OnAdded"/> after adding item to the list, do not forgot to check <see cref="CancelableKeyValueEventArgs.Cancel"/></remarks>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <exception cref="OperationCanceledException">Operation is canceled in eventhandler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Adding"/> event</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="key"/> is null</exception>
        ''' <exception cref="ArgumentException">An element with the same key already exists in the <see cref="DictionaryWithEvents(Of TKey, TValue)"/>.</exception>
        Public Overridable Sub Add(ByVal key As TKey, ByVal value As TValue) Implements IDictionary(Of TKey, TValue).Add
            If key Is Nothing Then Throw New ArgumentNullException("key")
            If ContainsKey(key) Then Throw New ArgumentException(ResourcesT.Exceptions.GivenKeyIsAlreadyPresentInTheDictionary)
            If Locked Then Throw New InvalidOperationException(ResourcesT.Exceptions.ListIsLocked)
            Dim e As New CancelableKeyValueEventArgs(key, value, AddingReadOnly)
            OnAdding(e)
            If Not e.Cancel Then
                Dict.Add(key, value)
                AddItemHandler(key)
                OnAdded(New KeyValueEventArgs(key, value))
            End If
        End Sub
        '''' <summary>Adds range of items into list</summary>
        '''' <param name="Items">Collection of items to be added</param>
        '''' <remarks>
        '''' Internally calls <see cref="Add"/> for each item.
        '''' If an exception occures in <see cref="Add"/> or event handler than no item is added.
        '''' <paramref name="Items"/> can safelly be null.
        '''' </remarks>
        '''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        '''' <exception cref="OperationCanceledException">Operation is canceled in eventhandler and <see cref="CancelError"/> is true</exception>
        '''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Adding"/> event</exception>
        'Public Overridable Sub AddRange(ByVal Items As IEnumerable(Of TKey))
        '    If Items Is Nothing Then Exit Sub
        '    Dim StartAdd As Integer = Me.Count
        '    Try
        '        For Each itm As TKey In Items
        '            Add(itm)
        '        Next itm
        '    Catch
        '        If Me.Count > StartAdd Then _
        '            InternalDict.RemoveRange(StartAdd, Me.Count - StartAdd + 1)
        '        Throw
        '    End Try
        'End Sub

        '''' <summary>Inserts an item to the <see cref="DictionaryWithEvents(Of TKey,TValue)"/> at the specified index.</summary>
        '''' <param name="item">The object to insert into the <see cref="DictionaryWithEvents(Of TKey,TValue)"/>.</param>
        '''' <param name="index">The zero-based index at which item should be inserted.</param>
        '''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="DictionaryWithEvents(Of TKey,TValue)"/>.</exception>
        '''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        '''' <exception cref="OperationCanceledException">Operation is canceled in eventhandler and <see cref="CancelError"/> is true</exception>
        '''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Adding"/> event</exception>
        '''' <remarks>Note for inheritors: Call <see cref="OnAdding"/> before adding an item to the list and <see cref="OnAdded"/> after adding item to the list, do not forgot to check <see cref="CancelableItemIndexEventArgs.Cancel"/></remarks>
        'Public Overridable Sub Insert(ByVal index As Integer, ByVal item As TKey) Implements System.Collections.Generic.IList(Of T).Insert
        '    If Locked Then Throw New InvalidOperationException(ResourcesT.Exceptions.ListIsLocked)
        '    Dim e As New CancelableItemKeyEventArgs(item, index, AddingReadOnly)
        '    OnAdding(e)
        '    If Not e.Cancel Then
        '        Dict.Add(item)
        '        AddItemHandler(index)
        '        OnAdded(New ItemIndexEventArgs(item, index))
        '    End If
        'End Sub
        ''' <summary>Raises <see cref="Adding"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnAdding"/> in order the event to be raised</remarks>
        ''' <exception cref="OperationCanceledException">Operation is canceled in eventhandler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Adding"/> event</exception>
        Protected Overridable Sub OnAdding(ByVal e As CancelableKeyValueEventArgs)
            RaiseEvent Adding(Me, e)
            If e.Cancel AndAlso CancelError Then Throw New OperationCanceledException(IfNull(e.CancelMessage, "Adding was canceled in event handler"))
        End Sub
        ''' <summary>Raises <see cref="Added"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnAdded"/> in order the event to be raised</remarks>
        Protected Overridable Sub OnAdded(ByVal e As KeyValueEventArgs)
            RaiseEvent Added(Me, e)
            OnChanged(e)
            OnCollectionChanged(e, CollectionChangeAction.Add, e.Key, Nothing, e.Item)
        End Sub
#End Region
#Region "Clear"
        ''' <summary>List of <see cref="ClearingEventHandler"/> delegates to be invoked when the <see cref="Clearing"/> event is raised</summary>
        Private ClearingEventHandlerList As New List(Of ClearingEventHandler)
        ''' <summary>Delegate of handler of <see cref="Clearing"/> event</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        Public Delegate Sub ClearingEventHandler(ByVal sender As DictionaryWithEvents(Of TKey, TValue), ByVal e As CancelMessageEventArgs)
        ''' <summary>Raised before the list is cleared. Raised by <see cref="Clear"/> method.</summary>
        ''' <remarks><para>
        ''' This event can be disabled (see <see cref="AllowAddCancelableEventsHandlers"/>.
        ''' This means that<see cref="InvalidOperationException"/> is thrown when adding handler and <see cref="AllowAddCancelableEventsHandlers"/> is False.
        ''' </para><para>
        ''' <see cref="Removing"/> Event is not raised when clearing list.
        ''' </para></remarks>
        ''' <exception cref="InvalidOperationException">Adding handler when <see cref="AllowAddCancelableEventsHandlers"/> is false</exception>
        Public Custom Event Clearing As ClearingEventHandler
            AddHandler(ByVal value As ClearingEventHandler)
                If AllowAddCancelableEventsHandlers Then
                    ClearingEventHandlerList.Add(value)
                Else
                    Throw New InvalidOperationException(ResourcesT.Exceptions.CannotAddHandlerToTheClearigEventWhenAllowAddCancelableEventsHandlersIsFalse)
                End If
            End AddHandler
            <DebuggerStepThrough()> _
            RemoveHandler(ByVal value As ClearingEventHandler)
                ClearingEventHandlerList.Remove(value)
            End RemoveHandler
            RaiseEvent(ByVal sender As DictionaryWithEvents(Of TKey, TValue), ByVal e As CancelMessageEventArgs)
                For Each Handler As ClearingEventHandler In ClearingEventHandlerList
                    Handler.Invoke(sender, e)
                Next Handler
            End RaiseEvent
        End Event
        ''' <summary>Raised after the list is cleared. Raised by <see cref="Clear"/> method.</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        ''' <remarks><see cref="Removed"/> event is not raised when clearing list.</remarks>
        Public Event Cleared(ByVal sender As DictionaryWithEvents(Of TKey, TValue), ByVal e As DictionaryItemsEventArgs)
        ''' <summary>Removes all items from the <see cref="DictionaryWithEvents(Of TKey,TValue)"/>.</summary>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <exception cref="OperationCanceledException">Operation is canceled in eventhandler and <see cref="CancelError"/> is true</exception>
        ''' <remarks>Note for inheritors: Call <see cref="OnClearing"/> before clearing of the list and <see cref="OnCleared"/> after clearing of the list,, do not forgot to check <see cref="CancelEventArgs.Cancel"/></remarks>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Clearing"/> event</exception>
        Public Overridable Sub Clear() Implements IDictionary.Clear, IDictionary(Of TKey, TValue).Clear
            If Locked Then Throw New InvalidOperationException(ResourcesT.Exceptions.ListIsLocked)
            Dim e As New CancelMessageEventArgs
            OnClearing(e)
            If Not e.Cancel Then
                Dim e2 As New DictionaryItemsEventArgs(Dict)
                RemoveAllItemHandlers()
                Dict.Clear()
                OnCleared(e2)
            End If
        End Sub

        ''' <summary>Raises <see cref="Clearing"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnClearing"/> in order the event to be raised</remarks>
        ''' <exception cref="OperationCanceledException">Operation is canceled in eventhandler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Clearing"/> event</exception>
        Protected Overridable Sub OnClearing(ByVal e As CancelMessageEventArgs)
            RaiseEvent Clearing(Me, e)
            If e.Cancel AndAlso Me.CancelError Then Throw New OperationCanceledException(IfNull(e.CancelMessage, ResourcesT.Exceptions.ClearingWasCanceledInEventhendler))
        End Sub
        ''' <summary>Raises <see cref="Cleared"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnCleared"/> in order the event to be raised</remarks>
        Protected Overridable Sub OnCleared(ByVal e As DictionaryItemsEventArgs)
            RaiseEvent Cleared(Me, e)
            OnChanged(e)
            OnCollectionChanged(e, CollectionChangeAction.Clear, Nothing, Nothing, Nothing)
        End Sub
#End Region
        '''' <summary>Determines whether the <see cref="DictionaryWithEvents(Of TKey,TValue)"/> contains a specific value.</summary>
        '''' <param name="item">The object to locate in the <see cref="DictionaryWithEvents(Of TKey,TValue)"/>.</param>
        '''' <returns>true if item is found in the <see cref="DictionaryWithEvents(Of TKey,TValue)"/>; otherwise, false.</returns>
        'Public Overridable Function Contains(ByVal item As TKey) As Boolean Implements System.Collections.Generic.ICollection(Of T).Contains
        '    Return Dict.Contains(item)
        'End Function
        '''' <summary>Copies the elements of the <see cref="DictionaryWithEvents(Of TKey,TValue)"/> to an <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> index.</summary>
        '''' <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from <see cref="DictionaryWithEvents(Of TKey,TValue)"/>. The <see cref="System.Array"/> must have zero-based indexing.</param>
        '''' <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        '''' <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        '''' <exception cref="System.ArgumentNullException">array is null.</exception>
        '''' <exception cref="System.ArgumentException">array is multidimensional.-or-arrayIndex is equal to or greater than the length of array.-or-The number of elements in the source System.Collections.Generic.ICollection(Of T) is greater than the available space from arrayIndex to the end of the destination array.-or-Type T cannot be cast automatically to the type of the destination array.</exception>
        'Public Overridable Sub CopyTo(ByVal array() As TKey, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of T).CopyTo
        '    Dict.CopyTo(array, arrayIndex)
        'End Sub
        '''' <summary>Gets the number of elements contained in the <see cref="DictionaryWithEvents(Of TKey,TValue)"/>.</summary>
        '''' <returns>The number of elements contained in the <see cref="DictionaryWithEvents(Of TKey,TValue)"/>.</returns>
        'Public Overridable ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of T).Count, System.Collections.ICollection.Count
        '    Get
        '        Return Dict.Count
        '    End Get
        'End Property
        '''' <summary>Gets a value indicating whether the <see cref="DictionaryWithEvents(Of TKey,TValue)"/> is read-only (always false).</summary>
        '''' <returns>Always false because <see cref="DictionaryWithEvents(Of TKey,TValue)"/> is not read-only</returns>
        '<EditorBrowsable(EditorBrowsableState.Never)> _
        'Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of TKey).IsReadOnly, System.Collections.IList.IsReadOnly
        '    Get
        '        Return False
        '    End Get
        'End Property
#Region "Remove"
        ''' <summary>List of <see cref="ItemCancelEventHandler"/> delegates to be invoked when the <see cref="Removing"/> event is raised</summary>
        Private RemovingEventHandlerList As New List(Of ItemCancelEventHandler)
        ''' <summary>Raised before item is removed from the list. Raised by <see cref="Remove"/> method.</summary>
        ''' <remarks><para>
        ''' This event can be disabled (see <see cref="AllowAddCancelableEventsHandlers"/>.
        ''' This means that<see cref="InvalidOperationException"/> is thrown when adding handler and <see cref="AllowAddCancelableEventsHandlers"/> is False.
        ''' </para><para>
        ''' Do not change content of list in handler! List is locked.
        ''' </para><para>
        ''' <see cref="Removing"/> event is not raised when list is being cleared.
        ''' </para><para>
        ''' Value of parameter <paramref name="e"/>'s <see cref="CancelableKeyValueEventArgs.Newkey"/> cannot be changed.
        ''' </para>
        ''' </remarks>
        ''' <exception cref="InvalidOperationException">Adding handler when <see cref="AllowAddCancelableEventsHandlers"/> is false</exception>
        Public Custom Event Removing As ItemCancelEventHandler
            AddHandler(ByVal value As ItemCancelEventHandler)
                If AllowAddCancelableEventsHandlers Then
                    RemovingEventHandlerList.Add(value)
                Else
                    Throw New InvalidOperationException(ResourcesT.Exceptions.CannotAddHandlerToTheRemovingEventWhenAllowAddCancelableEventsHandlersIsFalse)
                End If
            End AddHandler
            <DebuggerStepThrough()> _
            RemoveHandler(ByVal value As ItemCancelEventHandler)
                RemovingEventHandlerList.Remove(value)
            End RemoveHandler
            RaiseEvent(ByVal sender As DictionaryWithEvents(Of TKey, TValue), ByVal e As CancelableKeyValueEventArgs)
                For Each Handler As ItemCancelEventHandler In RemovingEventHandlerList
                    Handler.Invoke(sender, e)
                Next Handler
            End RaiseEvent
        End Event
        ''' <summary>Raised after the list is cleared. Raised by <see cref="Remove"/> method.</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters</param>
        ''' <remarks><see cref="Removed"/> event is not raised when the list.</remarks>
        Public Event Removed(ByVal sender As DictionaryWithEvents(Of TKey, TValue), ByVal e As KeyValueEventArgs)
        ''' <summary>Raises <see cref="Removing"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks><para>
        ''' Note for inheritors: Always call base class method <see cref="OnRemoving"/> in order the event to be raised
        ''' </para><para>
        ''' Do not change content of list in this method!
        ''' </para><para>
        ''' </para></remarks>
        ''' <exception cref="OperationCanceledException">Operation is canceled in eventhandler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Removing"/> event</exception>
        Protected Overridable Sub OnRemoving(ByVal e As CancelableKeyValueEventArgs)
            RaiseEvent Removing(Me, e)
            If e.Cancel AndAlso CancelError Then Throw New OperationCanceledException(IfNull(e.CancelMessage, ResourcesT.Exceptions.RemovingWasCenceledInEventHandler))
        End Sub
        ''' <summary>Raises <see cref="Removed"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnRemoved"/> in order the event to be raised</remarks>
        Protected Overridable Sub OnRemoved(ByVal e As KeyValueEventArgs)
            RaiseEvent Removed(Me, e)
            OnChanged(e)
            OnCollectionChanged(e, CollectionChangeAction.Remove, e.Key, e.Item, Nothing)
        End Sub
        ''' <summary>Removes item with given key from the <see cref="DictionaryWithEvents(Of TKey,TValue)"/>.</summary>
        ''' <param name="key">The object to remove from the <see cref="DictionaryWithEvents(Of TKey,TValue)"/>.</param>
        ''' <returns>true if item was successfully removed from the <see cref="DictionaryWithEvents(Of TKey,TValue)"/>; otherwise, false. This method also returns false if item is not found in the original <see cref="DictionaryWithEvents(Of TKey,TValue)"/>.</returns>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        ''' <exception cref="OperationCanceledException">Operation is canceled in eventhandler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Removing"/> event</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="key"/> is null</exception>
        ''' <remarks>Note for inheritors: Call <see cref="OnRemoving"/> before removing item and <see cref="OnRemoved"/> after removing item, do not forgot to check <see cref="CancelableKeyValueEventArgs.Cancel"/></remarks>
        Public Overridable Function Remove(ByVal key As TKey) As Boolean Implements IDictionary(Of TKey, TValue).Remove
            If key Is Nothing Then Throw New ArgumentNullException("key")
            If Locked Then Throw New InvalidOperationException(ResourcesT.Exceptions.ListIsLocked)
            If Contains(key) Then
                Dim value As TValue = Me(key)
                Dim e As New CancelableKeyValueEventArgs(key, value, True)
                Lock()
                Try
                    OnRemoving(e)
                Finally
                    Unlock()
                End Try
                If Not e.Cancel Then
                    'Dim i As Integer = IndexOf(Item)
                    RemoveItemHandler(key)
                    If Dict.Remove(key) Then
                        OnRemoved(New KeyValueEventArgs(key, value))
                        Return True
                    Else
                        AddItemHandler(key)
                        Return False
                    End If
                Else : Return False
                End If
            Else
                Return False
            End If
        End Function
        '''' <summary>Removes the <see cref="DictionaryWithEvents(Of TKey,TValue)"/> item at the specified index.</summary>
        '''' <param name="index">The zero-based index of the item to remove.</param>
        '''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="DictionaryWithEvents(Of TKey,TValue)"/>.</exception>
        '''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        '''' <exception cref="OperationCanceledException">Operation is canceled in eventhandler and <see cref="CancelError"/> is true</exception>
        '''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Removing"/> event</exception>
        '''' <remarks>Note for inheritors: Call <see cref="OnRemoving"/> before removing item and <see cref="OnRemoved"/> after removing item, do not forgot to check <see cref="CancelableItemIndexEventArgs.Cancel"/></remarks>
        'Public Overridable Sub RemoveAt(ByVal index As Integer) Implements System.Collections.Generic.IList(Of T).RemoveAt, System.Collections.IList.RemoveAt
        '    If Locked Then Throw New InvalidOperationException(ResourcesT.Exceptions.ListIsLocked)
        '    Dim e As New CancelableItemIndexEventArgs(Me(index), index, True)
        '    If index >= 0 AndAlso index < Count Then
        '        Lock()
        '        Try
        '            OnRemoving(e)
        '        Finally
        '            Unlock()
        '        End Try
        '    End If
        '    If Not e.Cancel Then
        '        Dim itm As TKey = Me(index)
        '        RemoveItemHandler(index)
        '        Try
        '            Dict.RemoveAt(index)
        '        Catch ex As Exception
        '            AddItemHandler(index)
        '            Throw ex
        '        End Try
        '        OnRemoved(New ItemIndexEventArgs(itm, index))
        '    End If
        'End Sub
        '''' <summary>Removes all items that matches given predicate</summary>
        '''' <param name="Match">Predicate to match. If this predicate returns true, item is removed</param>
        '''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        '''' <exception cref="OperationCanceledException">Operation is canceled in eventhandler and <see cref="CancelError"/> is true.</exception>
        '''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="Removing"/> event</exception>
        '''' <exception cref="ArgumentNullException"><paramref name="Match"/> is null</exception>
        '''' <remarks>If any exception is thrown in <seealso cref="RemoveAt"/> or event handler no item is removed (collection stays unchanged)</remarks>
        'Public Overridable Sub RemoveAll(ByVal Match As Predicate(Of TKey))
        '    If Match Is Nothing Then Throw New ArgumentNullException("Match")
        '    Dim [Rem] As New List(Of Integer)
        '    For i As Integer = 0 To Me.Count - 1
        '        If Match.Invoke(Me(i)) Then
        '            [Rem].Add(i)
        '        End If
        '    Next i
        '    Dim Removed As New List(Of KeyValuePair(Of Integer, TKey))
        '    Try
        '        For i As Integer = [Rem].Count - 1 To 0 Step -1
        '            Dim item As TKey = Me([Rem](i))
        '            Me.RemoveAt([Rem](i))
        '            Removed.Add(New KeyValuePair(Of Integer, TKey)(i, item))
        '        Next i
        '    Catch
        '        For Each ReAdd As KeyValuePair(Of Integer, TKey) In Removed
        '            Me.InternalDict.Insert(ReAdd.Key, ReAdd.Value)
        '        Next ReAdd
        '        Throw
        '    End Try
        'End Sub
        '''' <summary>Retrieves the all the elements that match the conditions defined by the specified predicate.</summary>
        '''' <param name="match">The <see cref="System.Predicate(Of T)"/> delegate that defines the conditions of the elements to search for.</param>
        '''' <returns>A <see cref="System.Collections.Generic.List(Of T)"/> containing all the elements that match the conditions defined by the specified predicate, if found; otherwise, an empty <see cref="System.Collections.Generic.List(Of T)"/>.</returns>
        '''' <exception cref="System.ArgumentNullException"><paramref name="Match"/> is null.</exception>
        '''' <remarks><seealso cref="List(Of T).FindAll"/></remarks>
        'Public Overridable Function FindAll(ByVal Match As Predicate(Of TKey)) As List(Of TKey)
        '    Return InternalDict.FindAll(Match)
        'End Function
#End Region
        '''' <summary>Returns an enumerator that iterates through the collection.</summary>
        '''' <returns>A <see cref="System.Collections.Generic.IEnumerator(Of T)"/> that can be used to iterate through the collection.</returns>
        'Public Overridable Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of TKey) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
        '    Return Dict.GetEnumerator
        'End Function
        '''' <summary>Returns an enumerator that iterates through a collection.</summary>
        '''' <returns>An <see cref="System.Collections.IEnumerator"/> object that can be used to iterate through the collection.</returns>
        '<EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use type-safe GetEnumerator instead")> _
        'Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        '    Return Dict.GetEnumerator
        'End Function
        '''' <summary>Determines the index of a specific item in the <see cref="DictionaryWithEvents(Of TKey,TValue)"/>.</summary>
        '''' <param name="item">The object to locate in the <see cref="DictionaryWithEvents(Of TKey,TValue)"/>.</param>
        '''' <returns>The index of item if found in the list; otherwise, -1.</returns>
        'Public Overridable Function IndexOf(ByVal item As TKey) As Integer Implements System.Collections.Generic.IList(Of T).IndexOf
        '    Return Dict.IndexOf(item)
        'End Function
#Region "Item"
        ''' <summary>List of <see cref="ItemCancelEventHandler"/> delegates to be invoked when the <see cref="ItemChanging"/> event is raised</summary>
        Private ItemChangingEventHandlerList As New List(Of ItemCancelEventHandler)
        ''' <summary>Raised before an item is changed. Raised by setter of <see cref="Item"/> property.</summary>
        ''' <remarks><para>
        ''' This event can be disabled (see <see cref="AllowAddCancelableEventsHandlers"/>.
        ''' This means that <see cref="InvalidOperationException"/> is thrown when adding handler and <see cref="AllowAddCancelableEventsHandlers"/> is False.
        ''' </para><para>
        ''' Value of parameter <paramref name="e"/>'s <see cref="CancelableKeyValueEventArgs.Item"/> can be changed if <see cref="AddingReadOnly"/> is False.
        ''' </para><para>
        ''' Do not change content of list in handler! List is locked.
        ''' </para><para>
        ''' <paramref name="e"/>'s <see cref="CancelableKeyValueEventArgs.Item"/> contains new value. Use <see cref="Item"/> to determine old value.
        ''' </para></remarks>
        ''' <exception cref="InvalidOperationException">Adding handler when <see cref="AllowAddCancelableEventsHandlers"/> is false</exception>
        Public Custom Event ItemChanging As ItemCancelEventHandler
            AddHandler(ByVal value As ItemCancelEventHandler)
                If AllowAddCancelableEventsHandlers Then
                    ItemChangingEventHandlerList.Add(value)
                Else
                    Throw New InvalidOperationException(ResourcesT.Exceptions.CannotAddHandlerToTheItemChangingEventWhenAllowAddCancelableEventsHandlersIsFalse)
                End If
            End AddHandler
            <DebuggerStepThrough()> _
            RemoveHandler(ByVal value As ItemCancelEventHandler)
                ItemChangingEventHandlerList.Remove(value)
            End RemoveHandler
            RaiseEvent(ByVal sender As DictionaryWithEvents(Of TKey, TValue), ByVal e As CancelableKeyValueEventArgs)
                For Each Handler As ItemCancelEventHandler In ItemChangingEventHandlerList
                    Handler.Invoke(sender, e)
                Next Handler
            End RaiseEvent
        End Event
        ''' <summary>Raised after item in the list is changed. Raised by setter of <see cref="Item"/> property.</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters (<see cref="ListWithEvents(Of TValue).ItemIndexEventArgs.Item"/> contains old value, use <see cref="Item"/> to determine new value.)</param>
        Public Event ItemChanged(ByVal sender As DictionaryWithEvents(Of TKey, TValue), ByVal e As OldNewValueEventArgs)
        ''' <summary>Raises <see cref="ItemChanging"/> event</summary>
        ''' <param name="e">Event argument</param>
        ''' <remarks><para>
        ''' Note for inheritors: Alway call base class method <see cref="OnItemChanging"/> in order the event to be raised.
        ''' </para><para>
        ''' Do not change the content of the list in this method!
        ''' </para></remarks>
        ''' <exception cref="OperationCanceledException">Operation is canceled in eventhandler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="ItemChanging"/> event</exception>
        Protected Overridable Sub OnItemChanging(ByVal e As CancelableKeyValueEventArgs)
            RaiseEvent ItemChanging(Me, e)
            If e.Cancel AndAlso CancelError Then Throw New OperationCanceledException(IfNull(e.CancelMessage, ResourcesT.Exceptions.ChangingWasCanceledInEventhandler))
        End Sub
        ''' <summary>Raises <see cref="ItemChanged"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnItemChanged"/> in order the event to be raised.</remarks>
        Protected Overridable Sub OnItemChanged(ByVal e As OldNewValueEventArgs)
            RaiseEvent ItemChanged(Me, e)
            OnChanged(e)
            OnCollectionChanged(e, CollectionChangeAction.Replace, e.Key, e.OldValue, e.Item)
        End Sub
        ''' <summary>Gets or sets the element with the specified key.</summary>
        ''' <param name="key">The key of the element to get or set.</param>
        ''' <returns>The element with the specified key.</returns>
        ''' <exception cref="KeyNotFoundException">The property is retrieved and key is not found.</exception>
        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True (in setter)</exception>
        ''' <exception cref="OperationCanceledException">Operation is canceled in eventhandler and <see cref="CancelError"/> is true</exception>
        ''' <exception cref="Exception">Any <see cref="Exception"/> can be thrown by event handler of the <see cref="ItemChanging"/> event</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="key"/> is null</exception>
        Default Public Overridable Property Item(ByVal key As TKey) As TValue Implements IDictionary(Of TKey, TValue).Item
            Get
                If key Is Nothing Then Throw New ArgumentNullException("key")
                Return Dict(key)
            End Get
            Set(ByVal value As TValue)
                If key Is Nothing Then Throw New ArgumentNullException("key")
                If Not InternalDict.ContainsKey(key) Then Throw New KeyNotFoundException(ResourcesT.Exceptions.GivenKeyIsAlreadyPresentInTheDictionary)
                If Locked Then Throw New InvalidOperationException(ResourcesT.Exceptions.ListIsLocked)
                Dim e As New CancelableKeyValueEventArgs(key, value, AddingReadOnly)
                'If key >= 0 AndAlso key < Dict.Count Then
                Lock()
                Try
                    OnItemChanging(e)
                Finally
                    Unlock()
                End Try
                'End If
                If Not e.Cancel Then
                    Dim old As TValue = Dict(key)
                    RemoveItemHandler(key)
                    Dict(key) = e.Item
                    AddItemHandler(key)
                    OnItemChanged(New OldNewValueEventArgs(key, old, Dict(key)))
                End If
            End Set
        End Property
#End Region
        ''' <summary>Gives access to underlying <see cref="Dictionary(Of TKey,TValue)"/></summary>
        <Browsable(False)> _
        Protected ReadOnly Property InternalDict() As Dictionary(Of TKey, TValue)
            <DebuggerStepThrough()> Get
                Return Dict
            End Get
        End Property
        '''' <summary>Gives read-only access to underlying <see cref="List(Of T)"/></summary>
        '<Browsable(False)> _
        'Public ReadOnly Property AsReadOnly() As IReadOnlyList(Of TKey)
        '    <DebuggerStepThrough()> Get
        '        Return New ReadOnlyListAdapter(Of TKey)(Dict)
        '    End Get
        'End Property
        ''' <summary>Adds handler to item at specified index if the item is <see cref="IReportsChange"/></summary>
        ''' <param name="key">Key of item tem to try add handler</param>
        ''' <remarks>Call after item is added</remarks>
        Protected Overridable Sub AddItemHandler(ByVal key As TKey)
            If TypeOf Me(key) Is IReportsChange Then
                AddHandler DirectCast(Me(key), IReportsChange).Changed, AddressOf OnItemValueChanged
            End If
        End Sub
        ''' <summary>Removes handler from item at specified index if the item is <see cref="IReportsChange"/></summary>
        ''' <param name="key">Key of item to try remove handler</param>
        ''' <remarks>Call before item is removed</remarks>
        Protected Overridable Sub RemoveItemHandler(ByVal key As TKey)
            If TypeOf Me(key) Is IReportsChange Then
                RemoveHandler DirectCast(Me(key), IReportsChange).Changed, AddressOf OnItemValueChanged
            End If
        End Sub
        ''' <summary>Removes handlers from all item that are of type <see cref="IReportsChange"/></summary>
        ''' <remarks>Call before clering list</remarks>
        Protected Overridable Sub RemoveAllItemHandlers()
            For Each i As TKey In Me.Keys
                RemoveItemHandler(i)
            Next i
        End Sub
        ''' <summary>Adds ahndlers to all items that as of type <see cref="IReportsChange"/></summary>
        ''' <remarks>Call only from CTor when no handlers have been added</remarks>
        Protected Overridable Sub AddAllItemHandlers()
            For Each i As TKey In Me.Keys
                AddItemHandler(i)
            Next i
        End Sub
        ''' <summary>Raises the <see cref="ItemValueChanged"/> event and handles the <see cref="IReportsChange.Changed"/> event for items</summary>
        ''' <param name="sender">Original source of the event</param>
        ''' <param name="e">Original event parameters</param>
        ''' <remarks>Note for inheritors: Always call base class method <see cref="OnItemValueChanged"/> in order the event to be raised</remarks>
        Protected Overridable Sub OnItemValueChanged(ByVal sender As IReportsChange, ByVal e As EventArgs)
            Dim e2 As New ListWithEvents(Of TValue).ItemValueChangedEventArgs(sender, e)
            RaiseEvent ItemValueChanged(Me, e2)
            OnChanged(e2)
            OnCollectionChanged(e2, CollectionChangeAction.ItemChange, KeyOf(DirectCast(sender, TValue)), sender, sender)
        End Sub
        ''' <summary>Gets key of firts occurence of given item</summary>
        ''' <param name="value">Item to find key of</param>
        ''' <exception cref="KeyNotFoundException"><paramref name="value"/> was not found in dictionary and <typeparamref name="TKey"/> is <see cref="ValueType"/>.</exception>
        ''' <returns>Key of first occurence of <paramref name="value"/> in dictionary. If <paramref name="value"/> is not found returns null (if <typeparamref name="TKey"/> is reference type) or throws an <see cref="KeyNotFoundException"/> (if <typeparamref name="TKey"/> is <see cref="ValueType"/>)</returns>
        Public Function KeyOf(ByVal value As TValue) As TKey
            Dim comp = EqualityComparer(Of TValue).Default
            For Each pair In Dict
                If comp.Equals(value, pair.Value) Then Return pair.Key
            Next
            If GetType(TKey).IsValueType Then Throw New KeyNotFoundException(ResourcesT.Exceptions.ValueWasNotFound) Else Return Nothing
        End Function


        ''' <summary>Raised when any of items that is of type <see cref="IReportsChange"/> raises <see cref="IReportsChange.Changed"/> event</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event params (contains original source (item) and original arguments</param>
        Public Event ItemValueChanged(ByVal sender As DictionaryWithEvents(Of TKey, TValue), ByVal e As ListWithEvents(Of TValue).ItemValueChangedEventArgs)

#Region "EventArgs"
        Public Class DictionaryChangedEventArgs : Inherits CollectionChangeEventArgs(Of KeyValuePair(Of TKey, TValue))
#Region "CTors"
            ''' <summary>CTor</summary>
            ''' <param name="ChangeEventArgs">Argumens of event that caused the collection to change</param>
            ''' <param name="Collection">Collection that was changed</param>
            ''' <param name="Action">Action which occured on collection</param>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="Action"/> is not member of <see cref="CollectionChangeAction"/></exception>
            Public Sub New(ByVal Collection As DictionaryWithEvents(Of TKey, TValue), ByVal ChangeEventArgs As EventArgs, ByVal Action As CollectionChangeAction)
                MyBase.new(Collection, ChangeEventArgs, Action)
            End Sub
            ''' <summary>CTor with index</summary>
            ''' <param name="ChangeEventArgs">Argumens of event that caused the collection to change</param>
            ''' <param name="Collection">Collection that was changed</param>
            ''' <param name="Action">Action which occured on collection</param>
            ''' <param name="key">Key at which the change has occured</param>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="Action"/> is not member of <see cref="CollectionChangeAction"/></exception>
            Public Sub New(ByVal Collection As DictionaryWithEvents(Of TKey, TValue), ByVal ChangeEventArgs As EventArgs, ByVal Action As CollectionChangeAction, ByVal key As TKey)
                Me.new(Collection, ChangeEventArgs, Action)
                _Key = key
            End Sub
            ''' <summary>CTor with index and old and new value</summary>
            ''' <param name="ChangeEventArgs">Argumens of event that caused the collection to change</param>
            ''' <param name="Collection">Collection that was changed</param>
            ''' <param name="Action">Action which occured on collection</param>
            ''' <param name="key">Key at which the change has occured</param>
            ''' <param name="OldValue">Old value at index <paramref name="index"/></param>
            ''' <param name="NewValue">New value at index <paramref name="index"/></param>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="Action"/> is not member of <see cref="CollectionChangeAction"/></exception>
            Public Sub New(ByVal Collection As DictionaryWithEvents(Of TKey, TValue), ByVal ChangeEventArgs As EventArgs, ByVal Action As CollectionChangeAction, ByVal key As TKey, ByVal OldValue As TValue, ByVal NewValue As TValue)
                Me.new(Collection, ChangeEventArgs, Action, key)
                _OldValue = OldValue
                _NewValue = NewValue
            End Sub
#End Region
            ''' <summary>Collection which was changed</summary>
            Public Shadows ReadOnly Property Collection() As DictionaryWithEvents(Of TKey, TValue)
                Get
                    Return MyBase.Collection
                End Get
            End Property
            ''' <summary>Contains value of the <see cref="Key"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _Key As TKey = Nothing
            ''' <summary>Contains value of the <see cref="OldValue"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _OldValue As TValue = Nothing
            ''' <summary>Contains value of the <see cref="NewValue"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _NewValue As TValue = Nothing
            ''' <summary>Gets key at which change occured (if applicable)</summary>
            ''' <returns>Original key where the change has ocured. If not applicable returns null (fo refrence types) or type default value (for <see cref="ValueType">value types</see>)</returns>
            Public ReadOnly Property Key() As TKey
                <DebuggerStepThrough()> Get
                    Return _Key
                End Get
            End Property
            ''' <summary>Gets value on index <see cref="Key"/> before change (if applicable)</summary>
            ''' <returns>Original value at index <see cref="Key"/>. If not applicable returns null (for reference types) or type default value (for <see cref="ValueType">value types</see>)</returns>
            Public ReadOnly Property OldValue() As TValue
                <DebuggerStepThrough()> Get
                    Return _OldValue
                End Get
            End Property
            ''' <summary>Gets value with key <see cref="Key"/> after change (if applicable)</summary>
            ''' <returns>Valu at index <see cref="Key"/> after changed. If not applicable returns null (for reference types) or type default value (for <see cref="ValueType">value types</see>)</returns>
            Public ReadOnly Property NewValue() As TValue
                <DebuggerStepThrough()> Get
                    Return _NewValue
                End Get
            End Property
        End Class
        ''' <summary>Parameter of event that report items</summary>
        Public Class DictionaryItemsEventArgs : Inherits ListWithEvents(Of TValue).ItemsEventArgs
            ''' <summary>CTor</summary>
            ''' <param name="Items">Items to be reported</param>
            Public Sub New(ByVal Items As IDictionary(Of TKey, TValue))
                MyBase.New(Items.Values)
                _Items = New ReadOnlyDictionary(Of TKey, TValue)(Items)
            End Sub
            ''' <summary>Contains value of the <see cref="Items"/> property</summary>
            Private ReadOnly _Items As ReadOnlyDictionary(Of TKey, TValue)
            ''' <summary>Items reported by this event</summary>
            Public Shadows ReadOnly Property Items() As ReadOnlyDictionary(Of TKey, TValue)
                Get
                    Return _Items
                End Get
            End Property
        End Class
        ''' <summary>Argument of <see cref="Adding"/> event</summary>
        Public Class CancelableKeyValueEventArgs : Inherits ListWithEvents(Of TValue).CancelableItemEventArgs
            ''' <summary>CTor</summary>
            ''' <param name="value">Value associated with current event</param>
            ''' <param name="ReadOnly">True to disallow changing of the <see cref="Item"/> property</param>
            ''' <param name="key">Key of newly added item</param>
            Public Sub New(ByVal key As TKey, ByVal value As TValue, Optional ByVal [ReadOnly] As Boolean = False)
                MyBase.New(value, [ReadOnly])
                Newkey = key
            End Sub
            ''' <summary>Key of newly added item</summary>
            ''' <remarks>The key may be invalid when collecion-manipulation is done between raising <see cref="Adding"/> event and using this instance.</remarks>
            Public ReadOnly Newkey As TKey
        End Class

        ''' <summary>Parameter of the <see cref="Added"/> event</summary>
        Public Class KeyValueEventArgs : Inherits ListWithEvents(Of TValue).ItemEventArgs
            ''' <summary>Key of newly added or changed item</summary>
            Public ReadOnly Key As TKey
            ''' <summary>CTor</summary>
            ''' <param name="value">Newly added item</param>
            ''' <param name="key">Key of newly added item</param>
            Public Sub New(ByVal key As TKey, ByVal value As TValue)
                MyBase.New(value)
                Me.Key = key
            End Sub
        End Class
        ''' <summary>Parameter of the <see cref="ItemChanged"/> event</summary>
        Public Class OldNewValueEventArgs : Inherits KeyValueEventArgs
            ''' <summary>Old value previosly on <see cref="Key"/></summary>
            Public ReadOnly OldValue As TValue
            ''' <summary>CTor</summary>
            ''' <param name="OldValue">Old value present at key</param>
            ''' <param name="NewValue">New value present at key</param>
            ''' <param name="key">Identification key</param>
            Public Sub New(ByVal key As TKey, ByVal OldValue As TValue, ByVal NewValue As TValue)
                MyBase.New(key, NewValue)
                Me.OldValue = OldValue
            End Sub
        End Class
#End Region
        '#Region "IList implementation - completely type unsafe, provided for CollectionEditor compatibility"
        '        ''' <summary>Copies the elements of the <see cref="System.Collections.ICollection"/> to an <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> index.</summary>
        '        ''' <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from <see cref="System.Collections.ICollection"/>. The <see cref="System.Array"/> must have zero-based indexing.</param>
        '        ''' <param name="index">The zero-based index in array at which copying begins.</param>
        '        ''' <exception cref="System.ArgumentNullException">array is null.</exception>
        '        ''' <exception cref="System.ArgumentOutOfRangeException">index is less than zero.</exception>
        '        ''' <exception cref="System.ArgumentException">array is multidimensional.-or- index is equal to or greater than the length of array.-or- The number of elements in the source <see cref="System.Collections.ICollection"/> is greater than the available space from index to the end of the destination array.</exception>
        '        ''' <exception cref="System.InvalidCastException">The type of the source <see cref="System.Collections.ICollection"/> cannot be cast automatically to the type of the destination array.</exception>
        '        ''' <remarks>Do not use, use type-safe <see cref="CopyTo"/> instead. Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        '        <Obsolete("Use type-safe CopyTo instead"), EditorBrowsable(EditorBrowsableState.Never)> _
        '        Public Sub CopyTo1(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo
        '            CType(Dict, IList).CopyTo(array, index)
        '        End Sub

        '        ''' <summary>Gets a value indicating whether access to the <see cref="System.Collections.ICollection"/> is synchronized (thread safe).</summary>
        '        ''' <returns>true if access to the <see cref="System.Collections.ICollection"/> is synchronized (thread safe); otherwise, false.</returns>
        '        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        '        <Obsolete("Although this is part of IList this is part of neither IList(Of T)  nor List(Of T)")> _
        '        <EditorBrowsable(EditorBrowsableState.Never), Browsable(False)> _
        '        Public ReadOnly Property IsSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
        '            Get
        '                Return CType(Dict, IList).IsSynchronized
        '            End Get
        '        End Property
        '        ''' <summary>Gets an object that can be used to synchronize access to the <see cref="System.Collections.ICollection"/>.</summary>
        '        ''' <returns>An object that can be used to synchronize access to the <see cref="System.Collections.ICollection"/></returns>
        '        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        '        <Obsolete("Although this is part of IList this is part of neither IList(Of T)  nor List(Of T)")> _
        '        <EditorBrowsable(EditorBrowsableState.Never), Browsable(False)> _
        '        Public ReadOnly Property SyncRoot() As Object Implements System.Collections.ICollection.SyncRoot
        '            Get
        '                Return Me
        '            End Get
        '        End Property
        '        ''' <summary>Adds an item to the <see cref="System.Collections.IList"/>.</summary>
        '        ''' <param name="value">The <see cref="System.Object"/> to add to the <see cref="System.Collections.IList"/>.</param>
        '        ''' <returns>The position into which the new element was inserted.</returns>
        '        ''' <exception cref="InvalidCastException"><paramref name="value"/> cannot be converted into type <typeparamref name="T"/></exception>
        '        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        '        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        '        <Obsolete("Use type-safe overload instead")> _
        '        <EditorBrowsable(EditorBrowsableState.Never)> _
        '        Public Function Add(ByVal value As Object) As Integer Implements System.Collections.IList.Add
        '            Add(CType(value, TKey))
        '            Return Dict.Count - 1
        '        End Function

        '        ''' <summary>Determines whether the <see cref="System.Collections.IList"/> contains a specific value.</summary>
        '        ''' <param name="value">The System.Object to locate in the <see cref="System.Collections.IList"/>.</param>
        '        ''' <returns>true if the <see cref="System.Object"/> is found in the <see cref="System.Collections.IList"/>; otherwise, false.</returns>
        '        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        '        <Obsolete("Use type-safe overload instead")> _
        '        <EditorBrowsable(EditorBrowsableState.Never)> _
        '        Public Function Contains(ByVal value As Object) As Boolean Implements System.Collections.IList.Contains
        '            Try
        '                Return Contains(CType(value, TKey))
        '            Catch ex As Exception
        '                Return False
        '            End Try
        '        End Function
        '        ''' <param name="value">The <see cref="System.Object"/> to locate in the <see cref="System.Collections.IList"/>.</param>
        '        ''' <returns>The index of value if found in the list; otherwise, -1.</returns>
        '        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        '        <Obsolete("Use type-safe overload instead")> _
        '        <EditorBrowsable(EditorBrowsableState.Never)> _
        '        Public Function IndexOf(ByVal value As Object) As Integer Implements System.Collections.IList.IndexOf
        '            Try
        '                Return IndexOf(CType(value, TKey))
        '            Catch ex As Exception
        '                Return -1
        '            End Try
        '        End Function
        '        ''' <param name="value">The <see cref="System.Object"/> to insert into the <see cref="System.Collections.IList"/>.</param>
        '        ''' <param name="index">The zero-based index at which value should be inserted.</param>
        '        ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="System.Collections.IList"/>.</exception>
        '        ''' <exception cref="System.NullReferenceException">value is null reference in the <see cref="System.Collections.IList"/>.</exception>
        '        ''' <exception cref="InvalidCastException"><paramref name="value"/> cannot be converted to the type <typeparamref name="T"/></exception>
        '        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        '        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        '        <Obsolete("Use type-safe overload instead")> _
        '        <EditorBrowsable(EditorBrowsableState.Never)> _
        '        Public Sub Insert(ByVal index As Integer, ByVal value As Object) Implements System.Collections.IList.Insert
        '            Insert(index, CType(value, TKey))
        '        End Sub
        '        ''' <summary>Gets a value indicating whether the <see cref="System.Collections.IList"/> has a fixed size.</summary>
        '        ''' <returns>Always False</returns>
        '        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        '        <EditorBrowsable(EditorBrowsableState.Never), Browsable(False)> _
        '        Public ReadOnly Property IsFixedSize() As Boolean Implements System.Collections.IList.IsFixedSize
        '            Get
        '                Return False
        '            End Get
        '        End Property

        '        ''' <summary>Removes the first occurrence of a specific object from the <see cref="System.Collections.IList"/>.</summary>
        '        ''' <param name="value">The <see cref="System.Object"/> to remove from the <see cref="System.Collections.IList"/></param>
        '        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True</exception>
        '        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        '        <Obsolete("Use type-safe overload instead"), EditorBrowsable(EditorBrowsableState.Never)> _
        '        Public Sub Remove(ByVal value As Object) Implements System.Collections.IList.Remove
        '            Try
        '                Remove(CType(value, TKey))
        '            Catch : End Try
        '        End Sub
        '        ''' <summary>Gets or sets the element at the specified index.</summary>
        '        ''' <param name="index">The zero-based index of the element to get or set.</param>
        '        ''' <returns>The element at the specified index.</returns>
        '        ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="System.Collections.IList"/>.</exception>
        '        ''' <exception cref="InvalidCastException">When setting value that cannot be converted to <typeparamref name="T"/></exception>
        '        ''' <exception cref="InvalidOperationException"><see cref="Locked"/> is True (in setter)</exception>
        '        ''' <remarks>Provided for compatibility with <see cref="System.ComponentModel.Design.CollectionEditor"/></remarks>
        '        <Obsolete("Use type-safe Item property instead"), EditorBrowsable(EditorBrowsableState.Never)> _
        '        <Browsable(False)> _
        '        Public Overloads Property Item1(ByVal index As Integer) As Object Implements System.Collections.IList.Item
        '            Get
        '                Return Item(index)
        '            End Get
        '            Set(ByVal value As Object)
        '                Item(index) = value
        '            End Set
        '        End Property
        '#End Region
        '#Region "ISerializable"
        '        ''' <summary>Populates a <see cref="System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.</summary>
        '        ''' <param name="context">The destination (see <see cref="System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
        '        ''' <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
        '        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission</exception>
        '        ''' <remarks>
        '        ''' Only items (see <see cref="Item"/>) are serialized.
        '        ''' Note for inheritors: Call this base class method in order items to be serialized.
        '        ''' </remarks>
        '        <Security.Permissions.SecurityPermission(Security.Permissions.SecurityAction.LinkDemand, Flags:=Security.Permissions.SecurityPermissionFlag.SerializationFormatter)> _
        '        Public Overridable Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext) Implements System.Runtime.Serialization.ISerializable.GetObjectData
        '            If info Is Nothing Then
        '                Throw New System.ArgumentNullException("info")
        '            End If
        '            info.AddValue(ItemsName, Me.InternalDict)
        '        End Sub
        '        ''' <summary>CTor - deserializes <see cref="DictionaryWithEvents(Of TKey,TValue)"/></summary>
        '        ''' <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/> that contains serialized object</param>
        '        ''' <param name="context">The source (see <see cref="System.Runtime.Serialization.StreamingContext"/>) of this deserialization.</param>
        '        ''' <exception cref="ArgumentNullException"><paramref name="info"/> is null</exception>
        '        ''' <exception cref="InvalidCastException">Serialized value was found but cannot be converted to type of corresponding property</exception>
        '        ''' <exception cref="Runtime.Serialization.SerializationException">An exception occured during deserialization</exception>
        '        ''' <remarks>
        '        ''' Only items (see <see cref="Item"/>) are deserialized.
        '        ''' Note for inheritors: Call this base class CTor in order to deserialize items. Another way is to deserialize them into local variable and then use <see cref="List(Of T).Add"/> or <see cref="List(Of T).AddRange"/>.
        '        ''' </remarks>
        '        Protected Sub New(ByVal info As Runtime.Serialization.SerializationInfo, ByVal context As Runtime.Serialization.StreamingContext)
        '            If info Is Nothing Then
        '                Throw New System.ArgumentNullException("info")
        '            End If
        '            Try
        '                MyClass.Dict = info.GetValue(ItemsName, GetType(List(Of TKey)))
        '            Catch ex As InvalidCastException
        '                Throw
        '            Catch ex As Runtime.Serialization.SerializationException
        '                Throw
        '            Catch ex As Exception
        '                Throw New Runtime.Serialization.SerializationException(ResourcesT.Exceptions.ErrorWhileDeserializingLinkLabelItem, ex)
        '            End Try
        '        End Sub
        '        ''' <summary>Name used for serialization of the <see cref="InternalDict"/> property</summary>
        '        Protected Const ItemsName$ = "Items"
        '#End Region
#Region "Collection changed"
        ''' <summary>Raised when value of member changes</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event information.
        ''' As of this implementation type of <paramref name="e"/> is always one of following types: <see cref="KeyValueEventArgs"/> (<see cref="Added"/>), <see cref="DictionaryItemsEventArgs"/> (<see cref="Cleared"/>), <see cref="KeyValueEventArgs"/> (<see cref="Removed"/>), <see cref="OldNewValueEventArgs"/> (<see cref="ItemChanged"/>), <see cref="ListWithEvents(Of TValue).ItemValueChangedEventArgs"/> (<see cref="ItemValueChanged"/>).</param>
        ''' <remarks>Raised after <see cref="Added"/>, <see cref="Removed"/>, <see cref="Cleared"/>, <see cref="ItemChanged"/> and <see cref="ItemValueChanged"/> events with the same argument <paramref name="e"/></remarks>
        Public Event Changed(ByVal sender As IReportsChange, ByVal e As EventArgs) Implements IReportsChange.Changed
        ''' <summary>Raises the <see cref="Changed"/> event</summary>
        ''' <param name="e">Event parameters</param>
        ''' <remarks>Called after <see cref="Added"/>, <see cref="Removed"/>, <see cref="Cleared"/>, <see cref="ItemChanged"/> and <see cref="ItemValueChanged"/> events with the same argument <paramref name="e"/>.
        ''' You should call one of overloaded <see cref="OnCollectionChanged"/> methods after calling this.</remarks>
        Protected Overridable Sub OnChanged(ByVal e As EventArgs)
            RaiseEvent Changed(Me, e)
        End Sub
        ''' <summary>Raised when this <see cref="DictionaryWithEvents(Of TKey,TValue)"/> collection changes.</summary>
        ''' <param name="sender">Source ot the event</param>
        ''' <param name="e">Event arguments. The <paramref name="e"/>.<see cref="DictionaryChangedEventArgs.ChangeEventArgs">ChangedEventArgs</see> contains event argument of the <see cref="Changed"/> event raised immediatelly prior this event.
        ''' As of this implementation type of <paramref name="e"/>.<see cref="DictionaryChangedEventArgs.ChangeEventArgs">ChangedEventArgs</see> is always one of following types: <see cref="KeyValueEventArgs"/> (<see cref="Added"/>), <see cref="DictionaryItemsEventArgs"/> (<see cref="Cleared"/>), <see cref="KeyValueEventArgs"/> (<see cref="Removed"/>), <see cref="OldNewValueEventArgs"/> (<see cref="ItemChanged"/>), <see cref="ListWithEvents(Of TValue).ItemValueChangedEventArgs"/> (<see cref="ItemValueChanged"/>).
        ''' Value of <paramref name="e"/>.<see cref="DictionaryChangedEventArgs.Collection"/> is always this instance.</param>
        ''' <remarks>This event is raised immediatelly after each <see cref="Changed"/> event.<para>
        ''' The reason for having two duplicit events is that <see cref="Changed"/> implements <see cref="IReportsChange.Changed"/> and you cannot determine action (what happend) through it. The aim of this event is to concentrate <see cref="Added"/>, <see cref="Removed"/>, <see cref="Cleared"/>, <see cref="ItemChanged"/> and <see cref="ItemValueChanged"/> events to one single event which allows handler to easily dinstinguish which action happedned on collection.</para></remarks>
        Public Event CollectionChanged(ByVal sender As DictionaryWithEvents(Of TKey, TValue), ByVal e As DictionaryChangedEventArgs)
        ''' <summary>Raises the <see cref="CollectionChanged"/> event.</summary>
        ''' <param name="e">Event argument. The <paramref name="e"/>.<see cref="DictionaryChangedEventArgs.ChangeEventArgs">ChangedEventArgs</see> should always contain event argument of preceding call of <see cref="OnChanged"/></param>
        ''' <remarks>You should call one of overloaded <see cref="OnChanged"/> methods after all calls of <see cref="OnChanged"/>.
        ''' This overridable overload is always called by the other overloads.</remarks>
        ''' <filterpriority>2</filterpriority>
        Protected Overridable Sub OnCollectionChanged(ByVal e As DictionaryChangedEventArgs)
            RaiseEvent CollectionChanged(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="CollectionChanged"/> event via calling <see cref="M:Tools.CollectionsT.GenericT.DictionaryWithEvents`1.OnChanged(Tools.CollectionsT.GenericT.ListChangedEventArgs)"/></summary>
        ''' <param name="e">Argument of preceding call of <see cref="OnChanged"/></param>
        ''' <param name="Action">Action taken on collection</param>
        ''' <param name="OldValue">Old value at index <paramref name="index"/> prior to change. Pass null (default value for value types) if not applicable.</param>
        ''' <param name="NewValue">New value at index <paramref name="index"/> after change. pass null (default value for value types) if not applicable</param>
        ''' <param name="Key">Index at which change has occured. Pass -1 if not applicable</param>
        ''' <remarks>You should call one of overloaded <see cref="OnChanged"/> methods after all calls of <see cref="OnChanged"/>.</remarks>
        ''' <filterpriority>1</filterpriority>
        Protected Sub OnCollectionChanged(ByVal e As EventArgs, ByVal Action As CollectionChangeAction, ByVal key As TKey, ByVal OldValue As TValue, ByVal NewValue As TValue)
            OnCollectionChanged(New DictionaryChangedEventArgs(Me, e, Action, key, OldValue, NewValue))
        End Sub
#End Region

#Region "Interface implementations"
#Region "ICollection(Of KeyValuePair(Of TKey, TValue))"
        ''' <summary>Adds an item to the <see cref="System.Collections.Generic.ICollection(Of T)" />.</summary>
        ''' <param name="item">The object to add to the <see cref="System.Collections.Generic.ICollection(Of T)" />.</param>
        ''' <exception cref="System.NotSupportedException">The <see cref="System.Collections.Generic.ICollection(Of T)" /> is read-only.</exception>
        ''' <exception cref="ArgumentException">An element with the same key already exists in the <see cref="DictionaryWithEvents(Of TKey, TValue)"/>.</exception>
        Private Sub Add(ByVal item As System.Collections.Generic.KeyValuePair(Of TKey, TValue)) Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Add
            Add(item.Key, item.Value)
        End Sub


        ''' <summary>Determines whether the <see cref="System.Collections.Generic.ICollection(Of T)" /> contains a specific value.</summary>
        ''' <param name="item">The object to locate in the <see cref="System.Collections.Generic.ICollection(Of T)" />.</param>
        ''' <returns>true if item is found in the <see cref="System.Collections.Generic.ICollection(Of T)" />; otherwise, false.</returns>
        Private Function Contains(ByVal item As System.Collections.Generic.KeyValuePair(Of TKey, TValue)) As Boolean Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Contains
            Return CollectionDic.Contains(item)
        End Function

        ''' <summary>Copies the elements of the <see cref="System.Collections.Generic.ICollection(Of T)" /> to an <see cref="System.Array" />, starting at a particular <see cref="System.Array" /> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="System.Array" /> that is the destination of the elements copied from <see cref="System.Collections.Generic.ICollection(Of T)" />. The <see cref="System.Array" /> must have zero-based indexing.</param>
        ''' <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        ''' <exception cref="System.ArgumentNullException">array is null.</exception>
        ''' <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        ''' <exception cref="System.ArgumentException">array is multidimensional.-or-arrayIndex is equal to or greater than the length of array.-or-The number of elements in the source <see cref="System.Collections.Generic.ICollection(Of T)" /> is greater than the available space from arrayIndex to the end of the destination array.-or-Type T cannot be cast automatically to the type of the destination array.</exception>
        Private Sub CopyTo(ByVal array() As System.Collections.Generic.KeyValuePair(Of TKey, TValue), ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).CopyTo
            CollectionDic.CopyTo(array, arrayIndex)
        End Sub
        ''' <summary>Gets <see cref="InternalDict"/> as <see cref="ICollection(Of KeyValuePair(Of TKey, TValue))"/></summary>
        Private ReadOnly Property CollectionDic() As ICollection(Of KeyValuePair(Of TKey, TValue))
            Get
                Return InternalDict
            End Get
        End Property
#End Region
#Region "IDictionary(Of TKey, TValue)"
        ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key.</summary>
        ''' <returns>true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the key; otherwise, false.</returns>
        ''' <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2" />.</param>
        ''' <exception cref="T:System.ArgumentNullException">
        ''' <paramref name="key" /> is null.</exception>
        Public Function ContainsKey(ByVal key As TKey) As Boolean Implements System.Collections.Generic.IDictionary(Of TKey, TValue).ContainsKey
            Return InternalDict.ContainsKey(key)
        End Function


        ''' <summary>Gets an <see cref="System.Collections.Generic.ICollection(Of T)" /> containing the keys of the <see cref="System.Collections.Generic.IDictionary(Of TKey, TValue)" />.</summary>
        ''' <returns>An <see cref="System.Collections.Generic.ICollection(Of T)" /> containing the keys of the object that implements <see cref="System.Collections.Generic.IDictionary(Of TKey, TValue)" />.</returns>
        Public ReadOnly Property Keys() As System.Collections.Generic.ICollection(Of TKey) Implements System.Collections.Generic.IDictionary(Of TKey, TValue).Keys
            Get
                Return InternalDict.Keys
            End Get
        End Property


        ''' <summary>Gets the value associated with the specified key.</summary>
        ''' <returns>true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
        ''' <param name="key">The key whose value to get.</param>
        ''' <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the 
        ''' <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
        ''' <exception cref="T:System.ArgumentNullException">
        ''' <paramref name="key" /> is null.</exception>
        Public Function TryGetValue(ByVal key As TKey, ByRef value As TValue) As Boolean Implements System.Collections.Generic.IDictionary(Of TKey, TValue).TryGetValue
            Return InternalDict.TryGetValue(key, value)
        End Function
        ''' <summary>Gets an <see cref="System.Collections.Generic.ICollection(Of TValue)" /> containing the values in the <see cref="System.Collections.Generic.IDictionary(Of TKey, TValue)" />.</summary>
        ''' <returns>An <see cref="System.Collections.Generic.ICollection(Of TValue)" /> containing the values in the object that implements <see cref="System.Collections.Generic.IDictionary(Of TKey, TValue)" />.</returns>
        Public ReadOnly Property Values() As System.Collections.Generic.ICollection(Of TValue) Implements System.Collections.Generic.IDictionary(Of TKey, TValue).Values
            Get
                Return InternalDict.Values
            End Get
        End Property
        ''' <summary>Returns an enumerator that iterates through the <see cref="DictionaryWithEvents(Of TKey,TValue)"/>.</summary>
        ''' <returns>A <see cref="Dictionary(Of TKey, TValue).Enumerator"/> structure for the <see cref="DictionaryWithEvents(Of TKey, TValue)" />.</returns>
        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)) Implements System.Collections.Generic.IEnumerable(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).GetEnumerator
            Return InternalDict.GetEnumerator
        End Function

        ''' <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.</summary>
        ''' <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
        ''' <filterpriority>2</filterpriority>
        Public ReadOnly Property Count() As Integer Implements System.Collections.ICollection.Count, IDictionary(Of TKey, TValue).Count
            Get
                Return InternalDict.Count
            End Get
        End Property
#End Region
#Region "IDictionary"
        ''' <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" /> object.</summary>
        ''' <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add. </param>
        ''' <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add. </param>
        ''' <exception cref="T:System.ArgumentNullException">
        ''' <paramref name="key" /> is null. </exception>
        ''' <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.IDictionary" /> object. </exception>
        ''' <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary" /> is read-only.-or- The <see cref="T:System.Collections.IDictionary" /> has a fixed size. </exception>
        ''' <exception cref="TypeMismatchException"><paramref name="key"/> is not of type <typeparamref name="TKey"/> -or- <paramref name="value"/> value is not of type <typeparamref name="TValue"/></exception>
        ''' <filterpriority>2</filterpriority>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private Sub Add(ByVal key As Object, ByVal value As Object) Implements System.Collections.IDictionary.Add
            If Not TypeOf key Is TKey Then Throw New TypeMismatchException("key", key, GetType(TKey))
            If Not TypeOf value Is TValue Then Throw New TypeMismatchException("value", value, GetType(TValue))
            Add(DirectCast(key, TKey), DirectCast(value, TValue))
        End Sub

        ''' <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> object contains an element with the specified key.</summary>
        ''' <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
        ''' <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" /> object.</param>
        ''' <exception cref="T:System.ArgumentNullException">
        ''' <paramref name="key" /> is null. </exception>
        ''' <filterpriority>2</filterpriority>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private Function Contains(ByVal key As Object) As Boolean Implements System.Collections.IDictionary.Contains
            If TypeOf key Is TKey Then Return Contains(DirectCast(key, TKey)) Else Return False
        End Function

        ''' <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.IDictionary" /> object.</summary>
        ''' <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.IDictionary" /> object.</returns>
        ''' <filterpriority>2</filterpriority>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private Function IDictionary_GetEnumerator() As System.Collections.IDictionaryEnumerator Implements System.Collections.IDictionary.GetEnumerator
            Return UnsafeInternalDictionary.GetEnumerator
        End Function

        ''' <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object is read-only.</summary>
        ''' <returns>true if the <see cref="T:System.Collections.IDictionary" /> object is read-only; otherwise, false.</returns>
        ''' <filterpriority>2</filterpriority>
        Private ReadOnly Property IsFixedSize() As Boolean Implements System.Collections.IDictionary.IsFixedSize, IDictionary(Of TKey, TValue).IsReadOnly
            Get
                Return False
            End Get
        End Property

        ''' <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object is read-only.</summary>
        ''' <returns>true if the <see cref="T:System.Collections.IDictionary" /> object is read-only; otherwise, false.</returns>
        ''' <filterpriority>2</filterpriority>
        Private ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.IDictionary.IsReadOnly
            Get
                Return False
            End Get
        End Property

        ''' <summary>Gets or sets the element with the specified key.</summary>
        ''' <returns>The element with the specified key.</returns>
        ''' <param name="key">The key of the element to get or set. </param>
        ''' <exception cref="T:System.ArgumentNullException"><paramref name="key" /> is null. </exception>
        ''' <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.IDictionary" /> object is read-only.-or- The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.IDictionary" /> has a fixed size. </exception>
        ''' <exception cref="KeyNotFoundException">Value is being get and <paramref name="key"/> is not of type <typeparamref name="TKey"/></exception>
        ''' <exception cref="TypeMismatchException">Value is being set and <paramref name="key"/> is not of type <typeparamref name="TKey"/> -or- Value is being set and value being set is not of type <typeparamref name="TValue"/>.</exception>
        ''' <filterpriority>2</filterpriority>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private Property IDictionary_Item(ByVal key As Object) As Object Implements System.Collections.IDictionary.Item
            Get
                If key Is Nothing Then Throw New ArgumentException("key")
                If TypeOf key Is TKey Then Return Item(DirectCast(key, TKey))
                Throw New KeyNotFoundException(String.Format(ResourcesT.Exceptions.CanNeverContainItemWithKeyOfType1, Me.GetType.FullName, key.GetType.FullName))
            End Get
            Set(ByVal value As Object)
                If key Is Nothing Then Throw New ArgumentException("key")
                If Not TypeOf key Is TKey Then Throw New TypeMismatchException("key", key, GetType(TKey))
                If Not TypeOf value Is TValue Then Throw New TypeMismatchException("value", value, GetType(TValue))
                Item(DirectCast(key, TKey)) = DirectCast(value, TValue)
            End Set
        End Property

        ''' <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys of the <see cref="T:System.Collections.IDictionary" /> object.</summary>
        ''' <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys of the <see cref="T:System.Collections.IDictionary" /> object.</returns>
        ''' <filterpriority>2</filterpriority>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private ReadOnly Property IDIctionary_Keys() As System.Collections.ICollection Implements System.Collections.IDictionary.Keys
            Get
                Return UnsafeInternalDictionary.Keys
            End Get
        End Property

        ''' <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" /> object.</summary>
        ''' <param name="key">The key of the element to remove. </param>
        ''' <exception cref="T:System.ArgumentNullException">
        ''' <paramref name="key" /> is null. </exception>
        ''' <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary" /> object is read-only.-or- The <see cref="T:System.Collections.IDictionary" /> has a fixed size. </exception>
        ''' <filterpriority>2</filterpriority>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private Sub Remove(ByVal key As Object) Implements System.Collections.IDictionary.Remove
            If TypeOf key Is TKey Then Remove(DirectCast(key, TKey))
        End Sub

        ''' <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.IDictionary" /> object.</summary>
        ''' <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.IDictionary" /> object.</returns>
        ''' <filterpriority>2</filterpriority>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private ReadOnly Property IDictionary_Values() As System.Collections.ICollection Implements System.Collections.IDictionary.Values
            Get
                Return UnsafeInternalDictionary.Values
            End Get
        End Property
        ''' <summary>Gets <see cref="InternalDict"/> as <see cref="IDictionary"/></summary>
        Private ReadOnly Property UnsafeInternalDictionary() As IDictionary
            Get
                Return InternalDict
            End Get
        End Property
#End Region
#Region "ICollection"
        ''' <summary>Removes the first occurrence of a specific object from the <see cref="System.Collections.Generic.ICollection(Of T)" />.</summary>
        ''' <param name="item">The object to remove from the <see cref="System.Collections.Generic.ICollection(Of T)" />.</param>
        ''' <returns>true if item was successfully removed from the <see cref="System.Collections.Generic.ICollection(Of T)" />; otherwise, false. This method also returns false if item is not found in the original <see cref="System.Collections.Generic.ICollection(Of T)" />.</returns>
        Private Function Remove_ICollection(ByVal item As System.Collections.Generic.KeyValuePair(Of TKey, TValue)) As Boolean Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Remove
            Dim comp = EqualityComparer(Of TValue).Default
            If ContainsKey(item.Key) AndAlso comp.Equals(item.Value, Me(item.Key)) Then Return Me.Remove(item.Key)
            Return False
        End Function

        ''' <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
        ''' <param name="index">The zero-based index in 
        ''' <paramref name="array" /> at which copying begins. </param>
        ''' <exception cref="T:System.ArgumentNullException">
        ''' <paramref name="array" /> is null. </exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException">
        ''' <paramref name="index" /> is less than zero. </exception>
        ''' <exception cref="T:System.ArgumentException">
        ''' <paramref name="array" /> is multidimensional.-or- 
        ''' <paramref name="index" /> is equal to or greater than the length of 
        ''' <paramref name="array" />.-or- The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from 
        ''' <paramref name="index" /> to the end of the destination 
        ''' <paramref name="array" />. </exception>
        ''' <exception cref="T:System.ArgumentException">The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination 
        ''' <paramref name="array" />. </exception>
        ''' <filterpriority>2</filterpriority>
        Private Sub CopyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo
            UnsafeInternalDictionary.CopyTo(array, index)
        End Sub

        ''' <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
        ''' <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
        ''' <filterpriority>2</filterpriority>
        Private ReadOnly Property IsSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
            Get
                Return UnsafeInternalDictionary.IsSynchronized
            End Get
        End Property

        ''' <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
        ''' <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
        ''' <filterpriority>2</filterpriority>
        Public ReadOnly Property SyncRoot() As Object Implements System.Collections.ICollection.SyncRoot
            Get
                Return UnsafeInternalDictionary.SyncRoot
            End Get
        End Property
        ''' <summary>Returns an enumerator that iterates through a collection.</summary>
        ''' <returns>An <see cref="System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        ''' <remarks>Use type-safe <see cref="GetEnumerator"/></remarks>
        <Obsolete("Use type-safe GetEnumerator")> _
        Private Function IEnumerable_GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function
#End Region
#End Region


        ''' <summary>Returns an <see cref="IEnumerator(Of TValue)" /> to enumerate values in dictionary</summary>
        ''' <returns>An <see cref="IEnumerator(Of TValue)" /> object for the <see cref="DictionaryWithEvents" /> object.</returns>
        ''' <version version="1.5.2">Function introduced</version>
        Public Function GetValuesEnumerator() As System.Collections.Generic.IEnumerator(Of TValue) Implements System.Collections.Generic.IEnumerable(Of TValue).GetEnumerator
            Return Me.Values.GetEnumerator
        End Function
    End Class
    '''' <summary>Describes acction on collection</summary>
    'Public Enum CollectionChangeAction
    '    ''' <summary>An item was added. Equals to <see cref="ComponentModel.CollectionChangeAction.Add"/>. Represents <see cref="DictionaryWithEvents(Of Object).Added"/>.</summary>
    '    Add = ComponentModel.CollectionChangeAction.Add
    '    ''' <summary>An item was removed. Equals to <see cref="ComponentModel.CollectionChangeAction.Remove"/>. Represents <see cref="DictionaryWithEvents(Of Object).Removed"/></summary>
    '    Remove = ComponentModel.CollectionChangeAction.Remove
    '    ''' <summary>The collection was cleared. Represents <see cref="DictionaryWithEvents(Of Object).Cleared"/>.</summary>
    '    Clear = 4
    '    ''' <summary>Item of collection was replaced. Represents <see cref="DictionaryWithEvents(Of Object).ItemChanged"/>.</summary>
    '    Replace = 5
    '    ''' <summary>Property of item of collection changed. Represents <see cref="DictionaryWithEvents(Of Object).ItemValueChanged"/>.</summary>
    '    ItemChange = 6
    '    ''' <summary>Unspecified action. Equals to <see cref="ComponentModel.CollectionChangeAction.Refresh"/>.</summary>
    '    Other = ComponentModel.CollectionChangeAction.Refresh
    'End Enum
    '''' <summary>Represents common base for generic classes <see cref="CollectionChangeEventArgs(Of T)"/></summary>
    'Public MustInherit Class CollectionChangedEventArgsBase
    '    Inherits ComponentModel.CollectionChangeEventArgs
    '    ''' <summary>Contains value of the <see cref="ChangeEventArgs"/> property</summary>
    '    Private ReadOnly _ChangeEventArgs As EventArgs
    '    ''' <summary>Contains value of the <see cref="Action"/> property</summary>
    '    Private ReadOnly _Action As CollectionChangeAction
    '    ''' <summary>CTor</summary>
    '    ''' <param name="ChangeEventArgs">Argumens of event that caused the collection to change</param>
    '    ''' <param name="Collection">Collection that was changed</param>
    '    ''' <remarks><see cref="Action"/> is set to <see cref="CollectionChangeAction.Other"/></remarks>
    '    Protected Sub New(ByVal Collection As IEnumerable, ByVal ChangeEventArgs As EventArgs)
    '        Me.New(Collection, ChangeEventArgs, CollectionChangeAction.Other)
    '    End Sub
    '    ''' <summary>CTor</summary>
    '    ''' <param name="ChangeEventArgs">Argumens of event that caused the collection to change</param>
    '    ''' <param name="Collection">Collection that was changed</param>
    '    ''' <param name="Action">Action which occured on collection</param>
    '    ''' <exception cref="InvalidEnumArgumentException"><paramref name="Action"/> is not member of <see cref="CollectionChangeAction"/></exception>
    '    Protected Sub New(ByVal Collection As IEnumerable, ByVal ChangeEventArgs As EventArgs, ByVal Action As CollectionChangeAction)
    '        MyBase.New(ConvertAction(Action), Collection)
    '        _ChangeEventArgs = ChangeEventArgs
    '        _Action = Action
    '    End Sub
    '    ''' <summary>Converts <see cref="CollectionChangeAction"/> to <see cref="ComponentModel.CollectionChangeAction"/></summary>
    '    ''' <param name="Action">A <see cref="CollectionChangeAction"/> to be converted</param>
    '    ''' <returns><see cref="ComponentModel.CollectionChangeAction"/> corresponding to <paramref name="Action"/></returns>
    '    ''' <exception cref="InvalidEnumArgumentException"><paramref name="Action"/> is not member of <see cref="CollectionChangeAction"/></exception>
    '    ''' <remarks>Conversion table follows:
    '    ''' <list type="table">
    '    ''' <listheader><term><see cref="CollectionChangeAction"/> (<paramref name="Action"/>)</term><description><see cref="ComponentModel.CollectionChangeAction"/></description></listheader>
    '    ''' <item><term><see cref="CollectionChangeAction.Add"/></term><description><see cref="ComponentModel.CollectionChangeAction.Add"/></description></item>
    '    ''' <item><term><see cref="CollectionChangeAction.Remove"/></term><description><see cref="ComponentModel.CollectionChangeAction.Remove"/></description></item>
    '    ''' <item><term><see cref="CollectionChangeAction.Other"/></term><description><see cref="ComponentModel.CollectionChangeAction.Refresh"/></description></item>
    '    ''' <item><term><see cref="CollectionChangeAction.Clear"/></term><description><see cref="ComponentModel.CollectionChangeAction.Remove"/></description></item>
    '    ''' <item><term><see cref="CollectionChangeAction.Replace"/></term><description><see cref="ComponentModel.CollectionChangeAction.Refresh"/></description></item>
    '    ''' <item><term><see cref="CollectionChangeAction.ItemChange"/></term><description><see cref="ComponentModel.CollectionChangeAction.Refresh"/></description></item>
    '    ''' <item><term>Otherwise</term><description><see cref="InvalidEnumArgumentException"/> thrown</description></item>
    '    ''' </list>
    '    ''' </remarks>
    '    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    '    Public Shared Function ConvertAction(ByVal Action As CollectionChangeAction) As ComponentModel.CollectionChangeAction
    '        Select Case Action
    '            Case CollectionChangeAction.Add, CollectionChangeAction.Remove, CollectionChangeAction.Other
    '                Return Action
    '            Case CollectionChangeAction.Clear : Return ComponentModel.CollectionChangeAction.Remove
    '            Case CollectionChangeAction.Replace, CollectionChangeAction.ItemChange
    '                Return ComponentModel.CollectionChangeAction.Refresh
    '            Case Else : Throw New InvalidEnumArgumentException("Action", Action, GetType(CollectionChangeAction))
    '        End Select
    '    End Function
    '    ''' <summary>Arguments of event that caused collection to be changed or that was raised by the colection on change</summary>
    '    Public ReadOnly Property ChangeEventArgs() As EventArgs
    '        Get
    '            Return _ChangeEventArgs
    '        End Get
    '    End Property
    '    ''' <summary>Action taken on collection</summary>
    '    Public Shadows ReadOnly Property Action() As CollectionChangeAction
    '        Get
    '            Return _Action
    '        End Get
    '    End Property
    'End Class
    '''' <summary>Arguments of event raised when collection owned by event source has changed</summary>
    'Public Class CollectionChangeEventArgs(Of TItem)
    '    Inherits CollectionChangedEventArgsBase
    '    ''' <summary>CTor</summary>
    '    ''' <param name="ChangeEventArgs">Argumens of event that caused the collection to change</param>
    '    ''' <param name="Collection">Collection that was changed</param>
    '    ''' <remarks><see cref="Action"/> is set to <see cref="CollectionChangeAction.Other"/></remarks>
    '    Public Sub New(ByVal Collection As ICollection(Of TItem), ByVal ChangeEventArgs As EventArgs)
    '        MyBase.new(Collection, ChangeEventArgs)
    '    End Sub
    '    ''' <summary>CTor</summary>
    '    ''' <param name="ChangeEventArgs">Argumens of event that caused the collection to change</param>
    '    ''' <param name="Collection">Collection that was changed</param>
    '    ''' <param name="Action">Action which occured on collection</param>
    '    ''' <exception cref="InvalidEnumArgumentException"><paramref name="Action"/> is not member of <see cref="CollectionChangeAction"/></exception>
    '    Public Sub New(ByVal Collection As ICollection(Of TItem), ByVal ChangeEventArgs As EventArgs, ByVal Action As CollectionChangeAction)
    '        MyBase.new(Collection, ChangeEventArgs, Action)
    '    End Sub
    '    ''' <summary>Collection which was changed</summary>
    '    Public ReadOnly Property Collection() As ICollection(Of TItem)
    '        Get
    '            Return MyBase.element
    '        End Get
    '    End Property
    'End Class







    ''' <summary>Implements read-only <see cref="IDictionary(Of TKey, TValue)"/></summary>
    ''' <typeparam name="TKey">Type of dictionary keys</typeparam>
    ''' <typeparam name="TValue">Type of dictionary items</typeparam>
    ''' <remarks>This class can either wrap any <see cref="IDictionary(Of TKey, TValue)"/> or copy any <see cref="IDictionary(Of TKey, TValue)"/></remarks>
    Public NotInheritable Class ReadOnlyDictionary(Of TKey, TValue)
        Implements IDictionary(Of TKey, TValue)
        Implements IReadOnlyIndexableEnumerable(Of TValue, TKey)
        Implements IReadOnlyIndexableWithCount(Of TValue, TKey)
        ''' <summary>CTor from <see cref="IDictionary(Of TKey, TValue)"/> with choice to wrap or copy it</summary>
        ''' <param name="Dictionary"><see cref="IDictionary(Of TKey, TValue)"/> to be wrapped or copyed</param>
        ''' <param name="Wrap">True to wrap  <paramref name="Dictionary"/>, false to create copy of <paramref name="Dictionary"/>. Default is false.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Dictionary"/> is null</exception>
        Public Sub New(ByVal Dictionary As IDictionary(Of TKey, TValue), Optional ByVal Wrap As Boolean = False)
            If Dictionary Is Nothing Then Throw New ArgumentException("Dictionary")
            If Wrap Then
                Me.Dictionary = Dictionary
            Else
                Me.Dictionary = New Dictionary(Of TKey, TValue)(Dictionary)
            End If
        End Sub
        ''' <summary>CTror from <see cref="IDictionary(Of TKey, TValue)"/> with comparer.</summary>
        ''' <remarks>This ctor always creates copy of <paramref name="Dictionary"/> values</remarks>
        ''' <param name="Dictionary"><see cref="IDictionary(Of TKey, TValue)"/> to be copied</param>
        ''' <param name="Comparer">The <see cref="System.Collections.Generic.IEqualityComparer(Of T)"/> implementation to use when comparing keys, or null to use the default <see cref="System.Collections.Generic.EqualityComparer(Of T)"/> for the type of the key.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Dictionary"/> is null</exception>
        Public Sub New(ByVal Dictionary As IDictionary(Of TKey, TValue), ByVal Comparer As IEqualityComparer(Of TKey))
            If Dictionary Is Nothing Then Throw New ArgumentException("Dictionary")
            Me.Dictionary = New Dictionary(Of TKey, TValue)(Dictionary, Comparer)
        End Sub
        ''' <summary>Internal dictionary</summary>
        Private ReadOnly Dictionary As IDictionary(Of TKey, TValue)
        ''' <summary>Gets <see cref="Dictionary"/> as <see cref="ICollection(Of KeyValuePair(Of TKey, TValue))"/></summary>
        Private ReadOnly Property DictionaryCollection() As ICollection(Of KeyValuePair(Of TKey, TValue))
            Get
                Return Dictionary
            End Get
        End Property
#Region "Throw"
        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <exception cref="NotSupportedException">Because this collection is read-only</exception>
        ''' <param name="item">Ignored</param>
        Private Sub Add(ByVal item As System.Collections.Generic.KeyValuePair(Of TKey, TValue)) Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Add
            Throw New NotSupportedException(ResourcesT.Exceptions.CollectionIsReadOnly)
        End Sub
        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <exception cref="NotSupportedException">Because this collection is read-only</exception>
        Private Sub Clear() Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Clear
            Throw New NotSupportedException(ResourcesT.Exceptions.CollectionIsReadOnly)
        End Sub
        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <exception cref="NotSupportedException">Because this collection is read-only</exception>
        ''' <param name="item">Ignored</param>
        ''' <returns>This function never returns and also throws <see cref="NotSupportedException"/></returns>
        Private Function Remove(ByVal item As System.Collections.Generic.KeyValuePair(Of TKey, TValue)) As Boolean Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Remove
            Throw New NotSupportedException(ResourcesT.Exceptions.CollectionIsReadOnly)
        End Function
        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <exception cref="NotSupportedException">Because this collection is read-only</exception>
        ''' <param name="key">Ignored</param><param name="value">Ignored</param>
        Private Sub Add(ByVal key As TKey, ByVal value As TValue) Implements System.Collections.Generic.IDictionary(Of TKey, TValue).Add
            Throw New NotSupportedException(ResourcesT.Exceptions.CollectionIsReadOnly)
        End Sub

        ''' <summary>Gets or sets the element with the specified key.</summary>
        ''' <returns>The element with the specified key.</returns>
        ''' <param name="key">The key of the element to get or set.</param>
        ''' <exception cref="T:System.ArgumentNullException">
        ''' <paramref name="key" /> is null.</exception>
        ''' <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and 
        ''' <paramref name="key" /> is not found.</exception>
        ''' <exception cref="T:System.NotSupportedException">The property being is set.</exception>
        ''' <value>Do not set this property as this collection is read-only and setter thus always throws <see cref="NotSupportedException"/>.</value>
        Private Property IDIctionary_Item(ByVal key As TKey) As TValue Implements System.Collections.Generic.IDictionary(Of TKey, TValue).Item
            Get
                Return Item(key)
            End Get
            Set(ByVal value As TValue)
                Throw New NotSupportedException(ResourcesT.Exceptions.CollectionIsReadOnly)
            End Set
        End Property
        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <exception cref="NotSupportedException">Because this collection is read-only</exception>
        ''' <param name="key">Ignored</param>
        ''' <returns>This function never returns and also throws <see cref="NotSupportedException"/></returns>
        Private Function Remove(ByVal key As TKey) As Boolean Implements System.Collections.Generic.IDictionary(Of TKey, TValue).Remove
            Throw New NotSupportedException(ResourcesT.Exceptions.CollectionIsReadOnly)
        End Function
#End Region
#Region "Private implementation"
        ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
        ''' <returns>true if 
        ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
        ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        Private Function Contains(ByVal item As System.Collections.Generic.KeyValuePair(Of TKey, TValue)) As Boolean Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Contains
            Return DictionaryCollection.Contains(item)
        End Function

        ''' <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        ''' <param name="arrayIndex">The zero-based index in 
        ''' <paramref name="array" /> at which copying begins.</param>
        ''' <exception cref="T:System.ArgumentNullException">
        ''' <paramref name="array" /> is null.</exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException">
        ''' <paramref name="arrayIndex" /> is less than 0.</exception>
        ''' <exception cref="T:System.ArgumentException">
        ''' <paramref name="array" /> is multidimensional.-or-
        ''' <paramref name="arrayIndex" /> is equal to or greater than the length of 
        ''' <paramref name="array" />.-or-The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from 
        ''' <paramref name="arrayIndex" /> to the end of the destination 
        ''' <paramref name="array" />.-or-Type 
        ''' <paramref name="T" /> cannot be cast automatically to the type of the destination 
        ''' <paramref name="array" />.</exception>
        Private Sub CopyTo(ByVal array() As System.Collections.Generic.KeyValuePair(Of TKey, TValue), ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).CopyTo
            DictionaryCollection.CopyTo(array, arrayIndex)
        End Sub
        ''' <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</summary>
        ''' <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.</returns>
        Private ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).IsReadOnly
            Get
                Return True
            End Get
        End Property
        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
        ''' <filterpriority>1</filterpriority>
        ''' <remarks>Use type-safe <see cref="GetEnumerator"/> instead</remarks>
        <Obsolete("Use type-safe GetEnumerator instead")> _
        Private Function IEnumerable_GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Dictionary.GetEnumerator
        End Function
#End Region
#Region "Public implementation"
        ''' <summary>Gets the number of elements contained in the <see cref="ReadOnlyDictionary(Of TKey, TValue)"/>.</summary>
        ''' <returns>The number of elements contained in the <see cref="ReadOnlyDictionary(Of TKey, TValue)" />.</returns>
        Public ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Count, IReadOnlyIndexableWithCount(Of TValue, TKey).Count
            Get
                Return Dictionary.Count
            End Get
        End Property
        ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key.</summary>
        ''' <returns>true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the key; otherwise, false.</returns>
        ''' <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2" />.</param>
        ''' <exception cref="T:System.ArgumentNullException">
        ''' <paramref name="key" /> is null.</exception>
        Public Function ContainsKey(ByVal key As TKey) As Boolean Implements System.Collections.Generic.IDictionary(Of TKey, TValue).ContainsKey
            Return Dictionary.ContainsKey(key)
        End Function
        ''' <summary>Gets or sets the element with the specified key.</summary>
        ''' <returns>The element with the specified key.</returns>
        ''' <param name="key">The key of the element to get or set.</param>
        ''' <exception cref="T:System.ArgumentNullException">
        ''' <paramref name="key" /> is null.</exception>
        ''' <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and 
        ''' <paramref name="key" /> is not found.</exception>
        ''' <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only.</exception>
        Default Public ReadOnly Property Item(ByVal key As TKey) As TValue Implements IReadOnlyIndexable(Of TValue, TKey).Item
            Get
                Return Dictionary(key)
            End Get
        End Property

        ''' <summary>Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2" />.</summary>
        ''' <returns>An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
        Public ReadOnly Property Keys() As System.Collections.Generic.ICollection(Of TKey) Implements System.Collections.Generic.IDictionary(Of TKey, TValue).Keys
            Get
                Return Dictionary.Keys
            End Get
        End Property

        ''' <summary>Gets the value associated with the specified key.</summary>
        ''' <returns>true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
        ''' <param name="key">The key whose value to get.</param>
        ''' <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the 
        ''' <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
        ''' <exception cref="T:System.ArgumentNullException">
        ''' <paramref name="key" /> is null.</exception>
        Public Function TryGetValue(ByVal key As TKey, ByRef value As TValue) As Boolean Implements System.Collections.Generic.IDictionary(Of TKey, TValue).TryGetValue
            Return Dictionary.TryGetValue(key, value)
        End Function

        ''' <summary>Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2" />.</summary>
        ''' <returns>An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
        Public ReadOnly Property Values() As System.Collections.Generic.ICollection(Of TValue) Implements System.Collections.Generic.IDictionary(Of TKey, TValue).Values
            Get
                Return Dictionary.Values
            End Get
        End Property
        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        ''' <filterpriority>1</filterpriority>
        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)) Implements System.Collections.Generic.IEnumerable(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).GetEnumerator
            Return DictionaryCollection.GetEnumerator
        End Function
#End Region

        ''' <summary>Returns an enumerator that iterates through the collection over values only.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        ''' <filterpriority>1</filterpriority>
        Public Function GetValuesEnumerator() As System.Collections.Generic.IEnumerator(Of TValue) Implements System.Collections.Generic.IEnumerable(Of TValue).GetEnumerator
            Return New IndexableEnumerator(Of TValue, TKey)(Me.Keys.GetEnumerator, DirectCast(Me, IReadOnlyIndexable(Of TValue, TKey)))
        End Function

        ''' <summary>Copies the values of the <see cref="T:Tools.CollectionsT.GenericT.ReadOnlyDictionary`2" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.                </summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.                </param>
        ''' <param name="index">The zero-based index in 
        ''' <paramref name="array" /> at which copying begins.</param>
        ''' <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is null.</exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex" /> is less than 0.</exception>
        ''' <exception cref="T:System.ArgumentException"><paramref name="array" /> is multidimensional. -or-
        ''' <paramref name="arrayIndex" /> is equal to or greater than the length of <paramref name="array" />. -or- The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from<paramref name="arrayIndex" /> to the end of the destination  <paramref name="array" />. -or-
        ''' Type  <paramref name="T" /> cannot be cast automatically to the type of the destination  <paramref name="array" />. </exception>
        Public Sub CopyTo(ByVal array() As TValue, ByVal index As Integer) Implements IReadOnlyCollection(Of TValue).CopyTo
            Me.Values.CopyTo(array, index)
        End Sub
    End Class
    ''' <summary>Implements <see cref="IEnumerator(Of TValue)"/> for any <see cref="IReadOnlyIndexable(Of TItem, TIndex)"/> where possible indexes are supplied from ouside</summary>
    ''' <typeparam name="TIndex">Type of index</typeparam>
    ''' <typeparam name="TValue">Type of value</typeparam>
    Public Class IndexableEnumerator(Of TIndex, TValue)
        Implements IEnumerator(Of Tvalue)
        ''' <summary>Outside-supplied indexes</summary>
        Private keys As IEnumerator(Of TIndex)
        ''' <summary>Instance to be indexed</summary>
        Private instance As IReadOnlyIndexable(Of TValue, TIndex)
        ''' <summary>CTor</summary>
        ''' <param name="KeysEnumerator"><see cref="IEnumerator"/> which supplies indexes to enumerate over</param>
        ''' <param name="Instance"><see cref="IReadOnlyIndexable(Of TITem, TIndex)"/> to enumerate over</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Instance"/> or <paramref name="KeysEnumerator"/> is null</exception>
        ''' <version version="1.5.4">Parameters renamed: <c>KeysEnumerator</c> to <c>keysEnumrator</c>, <c>Instance</c> to <c>instance</c></version>
        ''' <version version="1.5.4">Fix: <see cref="InvalidCastException"/> when <paramref name="instance"/> is not <see cref="IIndexable(Of TIndex, TKey)"/>, now <see cref="IReadOnlyIndexable(Of TValue, TIndex)"/> is enough.</version>
        Public Sub New(ByVal keysEnumerator As IEnumerator(Of TIndex), ByVal instance As IReadOnlyIndexable(Of TValue, TIndex))
            If KeysEnumerator Is [Nothing] Then Throw New ArgumentNullException("KeysEnumerator")
            If Instance Is Nothing Then Throw New ArgumentNullException("Instance")
            Me.Keys = KeysEnumerator
            Me.Instance = Instance
        End Sub
        ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        ''' <returns>The element in the collection at the current position of the enumerator.</returns>
        Public ReadOnly Property Current() As TValue Implements System.Collections.Generic.IEnumerator(Of TValue).Current
            Get
                Return Instance(Keys.Current)
            End Get
        End Property
        ''' <summary>Gets the element in the collection at the current position of the enumerator.                </summary>
        ''' <returns>The element in the collection at the current position of the enumerator.                </returns>
        <Obsolete("Use type-safe Current instead")> _
        Private ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
            Get
                Return Current
            End Get
        End Property

        ''' <summary>Advances the enumerator to the next element of the collection.</summary>
        ''' <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.                </returns>
        ''' <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.                 </exception>
        ''' <filterpriority>2</filterpriority>
        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            Return Keys.MoveNext
        End Function

        ''' <summary>Sets the enumerator to its initial position, which is before the first element in the collection.                </summary>
        ''' <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.                 </exception>
        ''' <filterpriority>2</filterpriority>
        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            Keys.Reset()
        End Sub

        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.                </summary>
        ''' <filterpriority>2</filterpriority>
        Public Sub Dispose() Implements IDisposable.Dispose
            Keys.Dispose()
        End Sub

    End Class
End Namespace



#End If

