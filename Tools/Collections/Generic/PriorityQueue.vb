Imports Tools.ComponentModelT, System.Linq
Imports Tools.DataStructuresT.GenericT

#If Config <= Nightly Then 'Stage:Nightly
Namespace CollectionsT.GenericT
    ''' <summary>Indicates, which priority is poped first from the front</summary>
    ''' <version version="1.5.2" stage="Nightly">Enumeration introduced</version>
    Public Enum PriorityTarget As Integer
        ''' <summary>Items with greater priority are poped first from priority front</summary>
        MaximumFirst = 0
        ''' <summary>  Items with lower priority are poped first from priority front</summary>
        MinimumFirst = -2
    End Enum


    ''' <summary>Priority queue based on comparer</summary>
    ''' <remarks>Can be also used as sorted list.
    ''' <para>Note: When sorting value property used by comparer changes, list is not re-sorted manually.</para></remarks>
    ''' <typeparam name="T">Type of item in queue</typeparam>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public Class PriorityQueue(Of T)
        Implements IList(Of T), IAddableIndexable(Of T, Integer)
        ''' <summary>Internal list</summary>
        Private ReadOnly List As List(Of T) = New List(Of T)
        ''' <summary>Contains value of the <see cref="PriorityTarget"/> property</summary>
        Private _PriorityTarget As PriorityTarget
        ''' <summary>Defines which items are poped firts</summary>
        ''' <remarks>This property does not affect sort order of internal list</remarks>
        ''' <seelaso cref="Pop"/><seelaso cref="Peek"/>
        Public Property PriorityTarget() As PriorityTarget
            Get
                Return _PriorityTarget
            End Get
            Set(ByVal value As PriorityTarget)
                _PriorityTarget = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Comparer"/> property</summary>
        Private ReadOnly _Comparer As IComparer(Of T)
        ''' <summary>Comparer used to compare items</summary>
        ''' <remarks>This comparer is used for sorting purposes only. It is not used as equality comparer when equality must be tested (such as <see cref="IndexOf"/>).</remarks>
        Public ReadOnly Property Comparer() As IComparer(Of T)
            Get
                Return _Comparer
            End Get
        End Property
#Region "CTors"
        ''' <summary>CTor from camparer</summary>
        ''' <param name="Comparer">Comperer for comparing items</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Comaprer"/> is null</exception>
        ''' <seelaso cref="Comparer"/>
        Public Sub New(ByVal Comparer As IComparer(Of T))
            If Comparer Is Nothing Then Throw New ArgumentNullException("Comparer")
            Me._Comparer = Comparer
        End Sub
        ''' <summary>CTor using default comparer</summary>
        ''' <remarks>Uses <see cref="Collections.Generic.Comparer(Of T).Default"/>. Use this constuctor only when default comparer is meaningful.</remarks>
        ''' <seelaso cref="Collections.Generic.Comparer(Of T).Default"/>
        Public Sub New()
            Me.New(Collections.Generic.Comparer(Of T).Default)
        End Sub
        ''' <summary>CTor from camparer and <see cref="PriorityTarget"/></summary>
        ''' <param name="Comparer">Comperer for comparing items</param>
        ''' <param name="PriorityTarget">Defines which items are poped first</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Comaprer"/> is null</exception>
        ''' <seelaso cref="Comparer"/>
        Public Sub New(ByVal Comparer As IComparer(Of T), ByVal PriorityTarget As PriorityTarget)
            Me.New(Comparer)
            Me.PriorityTarget = PriorityTarget
        End Sub
#End Region
        ''' <summary>CTor from existing collection</summary>
        ''' <param name="Collection">Items to be in this sorted list</param>
        ''' <param name="Comparer">Comaprer for comparing items</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Comaprer"/> is null</exception>
        ''' <seelaso cref="Comparer"/>
        Public Sub New(ByVal Collection As IEnumerable(Of T), ByVal Comparer As IComparer(Of T))
            Me.New(Comparer)
            List.AddRange(Collection)
            List.Sort(Comparer)
        End Sub
        ''' <summary>Gets the number of elements contained in the <see cref="System.Collections.Generic.ICollection(Of T)" />.</summary>
        ''' <returns>The number of elements contained in the <see cref="System.Collections.Generic.ICollection(Of T)" />.</returns>
        Public ReadOnly Property Count%() Implements IList(Of T).Count, IReadOnlyCollection(Of T).Count
            Get
                Return List.Count
            End Get
        End Property
        ''' <summary>Gets value on specified index</summary>
        ''' <param name="index">Index to obtain value</param>
        ''' <returns>value lying on specified <paramref name="index"/></returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.  -or- <paramref name="index"/> is equal to or greater than <see cref="Count"/>.</exception>
        Default Public ReadOnly Property Item(ByVal Index As Integer) As T Implements IReadOnlyIndexable(Of T, Integer).Item
            Get
                Return List(Index)
            End Get
        End Property
        ''' <summary>Adds item to collection</summary>
        ''' <param name="Value">Item to be added</param>
        ''' <remarks>Item is immediatelly sorted at correct index</remarks>
        ''' <returns>Index at which the item was pushed</returns>
        Public Function Push(ByVal Value As T) As Integer
            List.Insert(GetInsertIndex(Value), Value)
        End Function
        ''' <summary>Gets index where item item is expected to be</summary>
        ''' <param name="Value">Item to find</param>
        ''' <returns>Either last index where item equal (in terms of <see cref="Comparer"/>) is or index where it should be inserted.</returns>
        Protected Friend Function GetInsertIndex(ByVal Value As T) As Integer
            If List.Count = 0 Then
                Return 0
            Else
                Dim MinIndex = 0
                Dim MaxIndex = Count
                Do
                    Dim index As Integer = (MinIndex + MaxIndex) \ 2
                    Select Case Comparer.Compare(Value, Me(index))
                        Case Is < 0
                            MaxIndex = index
                        Case Is > 0
                            MinIndex = index + 1
                        Case Else
                            While index + 1 < Count AndAlso Comparer.Compare(Value, Me(index + 1)) = 0
                                index += 1
                            End While
                            Return index
                    End Select
                Loop While MinIndex < MaxIndex
                Return MaxIndex
            End If
        End Function
        ''' <summary>Gets items that have same priority as given item</summary>
        ''' <param name="item">Item to get items with same priority as</param>
        ''' <returns>Items with same priority as <paramref name="item"/> or an empty enumeration if there are no such items</returns>
        Public Function GetItemsWithSamePriority(ByVal item As T) As IEnumerable(Of T)
            Dim Index = GetInsertIndex(item)
            If Index = Me.Count OrElse Comparer.Compare(Me(Index), item) <> 0 Then Return New T() {}
            Dim ret As New List(Of T)
            While Index >= 0 AndAlso Comparer.Compare(Me(Index), item) = 0
                ret.Insert(0, Me(Index))
                Index -= 1
            End While
            Return ret
        End Function

        ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.                </summary>
        ''' <returns>true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if 
        ''' <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.                </returns>
        ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.                </param>
        Public Function Remove(ByVal item As T) As Boolean Implements IList(Of T).Remove
            Return List.Remove(item)
        End Function
        ''' <summary>Removes the <see cref="T:System.Collections.Generic.IList`1" /> item at the specified index.</summary>
        ''' <param name="index">The zero-based index of the item to remove.</param>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.                </exception>
        Public Sub RemoveAt(ByVal Index%) Implements IList(Of T).RemoveAt
            List.RemoveAt(Index)
        End Sub
        ''' <summary>Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.                </summary>
        Public Sub Clear() Implements System.Collections.Generic.ICollection(Of T).Clear
            List.Clear()
        End Sub

        ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.                </summary>
        ''' <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.                </returns>
        ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.                </param>
        Public Function Contains(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Contains
            Return List.Contains(item)
        End Function

        ''' <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.                </summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.                </param>
        ''' <param name="arrayIndex">The zero-based index in 
        ''' <paramref name="array" /> at which copying begins.</param>
        ''' <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is null.</exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex" /> is less than 0.</exception>
        ''' <exception cref="T:System.ArgumentException"><paramref name="array" /> is multidimensional.
        ''' -or- <paramref name="arrayIndex" /> is equal to or greater than the length of  <paramref name="array" />.
        ''' -or- The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from  <paramref name="arrayIndex" /> to the end of the destination  <paramref name="array" />.
        ''' -or- Type  <paramref name="T" /> cannot be cast automatically to the type of the destination  <paramref name="array" />.</exception>
        Public Sub CopyTo(ByVal array() As T, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of T).CopyTo, IReadOnlyCollection(Of T).CopyTo
            List.CopyTo(array, arrayIndex)
        End Sub

        ''' <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</summary>
        ''' <returns>false</returns>
        Private ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of T).IsReadOnly
            Get
                Return False
            End Get
        End Property


        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
            Return List.GetEnumerator
        End Function

        ''' <summary>Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.</summary>
        ''' <returns>The index of first occurence <paramref name="item" /> if found in the list; otherwise, -1.</returns>
        ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.</param>
        Public Function IndexOf(ByVal item As T) As Integer Implements System.Collections.Generic.IList(Of T).IndexOf
            Dim Index = GetInsertIndex(item)
            Dim retidx = -1
            While Index >= 0 AndAlso Comparer.Compare(Me(Index), item) = 0
                If Me(Index).Equals(item) Then
                    retidx = Index
                End If
                Index -= 1
            End While
            Return retidx
        End Function

        ''' <summary>Inserts an item to the <see cref="T:System.Collections.Generic.IList`1" /> at the specified index.</summary>
        ''' <param name="index">Ignored</param>
        ''' <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1" />.</param>
        Private Sub Insert(ByVal index As Integer, ByVal item As T) Implements System.Collections.Generic.IList(Of T).Insert
            Push(item)
        End Sub

        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
        Public Function IEnumerable_GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

        ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
        ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        ''' <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.                </exception>
        Private Sub ICollection_Add(ByVal item As T) Implements System.Collections.Generic.ICollection(Of T).Add, IAddable(Of T).Add
            Push(item)
        End Sub

        ''' <summary>Gets or sets the element at the specified index.</summary>
        ''' <returns>The element at the specified index.</returns>
        ''' <param name="index">The zero-based index of the element to get or set.</param>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.</exception>
        ''' <remarks>Whan value is set the item is removed at old <paramref name="index"/> and new value is placed at index determined by correct position of item in sorted list</remarks>
        Private Overloads Property ILits_Item(ByVal index As Integer) As T Implements System.Collections.Generic.IList(Of T).Item, IIndexable(Of T, Integer).Item
            Get
                Return Item(index)
            End Get
            Set(ByVal value As T)
                RemoveAt(index)
                Push(value)
            End Set
        End Property
        ''' <summary>Gets index of first item in queue</summary>
        ''' <returns>Zero or <see cref="Count"/> - 1 dpending on <see cref="PriorityTarget"/>; -1 when collection is empty</returns>
        ''' <remarks>When <see cref="PriorityTarget"/> is <see cref="PriorityTarget.MaximumFirst"/> returns <see cref="Count"/> - 1; 0 otherwise (unless <see cref="Count"/> is 0).</remarks>
        Public ReadOnly Property PeekIndex%()
            Get
                If Count = 0 Then Return -1
                If PriorityTarget = GenericT.PriorityTarget.MaximumFirst Then Return Count - 1 Else Return 0
            End Get
        End Property
        ''' <summary>Gets first item in priority queue</summary>
        ''' <exception cref="InvalidOperationException">Queue is empty</exception>
        Public Function Peek() As T
            If Count = 0 Then Throw New InvalidOperationException("Collection is empty")
            Return Me(PeekIndex)
        End Function
        ''' <summary>Gets first item from priority queue and removes it from list</summary>
        ''' <exception cref="InvalidOperationException">Queue is empty</exception>
        Public Function Pop() As T
            If Count = 0 Then Throw New InvalidOperationException("Collection is empty")
            Dim retval = Me(PeekIndex)
            Me.RemoveAt(PeekIndex)
            Return retval
        End Function
        ''' <summary>Forces queue to re-sort</summary>
        ''' <remarks>Call this method only when properties comparer sorts by changed for many items.
        ''' When sort proprty changes fo single item its better to <see cref="Remove"/> it and <see cref="Push"/> it back.</remarks>
        Public Sub Sort()
            List.Sort(Comparer)
        End Sub
    End Class

    ''' <summary>Priority queue with separeted item and priority</summary>
    ''' <typeparam name="TValue">Type of item in queue</typeparam>
    ''' <typeparam name="TPriority">Type of priority</typeparam>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    <DebuggerStepThrough()> _
    Public Class PriorityQueue(Of TPriority, TValue)
        Inherits PriorityQueue(Of KeyValuePair(Of TPriority, TValue))
        Implements IEnumerable(Of TValue)
        ''' <summary>Comaprer of priority</summary>
        Private ReadOnly MyComparer As IComparer(Of TPriority)
        ''' <summary>CTor</summary>
        ''' <param name="Comparer">Comparer of priority</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Comparer"/> is null</exception>
        Public Sub New(ByVal Comparer As IComparer(Of TPriority))
            MyBase.New(New GenericComparer(Of KeyValuePair(Of TPriority, TValue))(Function(a, b) Comparer.Compare(a.Key, b.Key)))
            If Comparer Is Nothing Then Throw New ArgumentNullException("Comparer")
            Me.MyComparer = Comparer
        End Sub
        ''' <summary>CTor using default comarer</summary>
        Public Sub New()
            Me.New(System.Collections.Generic.Comparer(Of TPriority).Default)
        End Sub
        ''' <summary>Adds item to priority queue using its priority</summary>
        ''' <param name="Priority">Item priority</param>
        ''' <param name="Value">Item itself</param>
        ''' <returns>Index at which the item was pushed</returns>
        Public Overloads Function Push(ByVal Priority As TPriority, ByVal Value As TValue) As Integer
            Return Me.Push(New KeyValuePair(Of TPriority, TValue)(Priority, Value))
        End Function
        ''' <summary>Gets item on top of priority queue</summary>
        ''' <returns>Item on top of priority queue</returns>
        ''' <exception cref="InvalidOperationException">Queue is empty</exception>
        Public Overloads Function Peek() As TValue
            Return MyBase.Peek().Value
        End Function
        ''' <summary>Gets item on top of priority queue and removes it from list</summary>
        ''' <returns>Item previously on top of riority queue</returns>
        ''' <exception cref="InvalidOperationException">Queue is empty</exception>
        Public Overloads Function Pop() As TValue
            Return MyBase.Pop.Value
        End Function
        ''' <summary>Gets priority of item on top of queue</summary>
        ''' <returns>Priority value of item on top of priority queue</returns>
        ''' <exception cref="InvalidOperationException">Queue is empty</exception>
        Public Function PeekPriority() As TPriority
            Return MyBase.Peek.Key
        End Function
        ''' <summary>Returns index of item in priority queues</summary>
        ''' <param name="item">Item to get inde of</param>
        ''' <returns>Index of item in queue; -1 whe item was not found</returns>
        Public Overloads Function IndexOf(ByVal item As TValue) As Integer
            Dim i% = 0
            For Each ii In DirectCast(Me, IEnumerable(Of KeyValuePair(Of TPriority, TValue)))
                If ii.Value.Equals(item) Then Return i
                i += 1
            Next
            Return -1
        End Function
        ''' <summary>Removes item from queue</summary>
        ''' <param name="item">Item to be removed</param>
        ''' <returns>True if item was removed; false if it was not (because it was not in the queue)</returns>
        Public Overloads Function Remove(ByVal item As TValue) As Boolean
            Dim i% = 0
            For Each ii In DirectCast(Me, IEnumerable(Of KeyValuePair(Of TPriority, TValue)))
                If ii.Value.Equals(item) Then
                    Me.RemoveAt(i)
                    Return True
                End If
                i += 1
            Next
            Return False
        End Function
        ''' <summary>Gets all the items with given priority</summary>
        ''' <param name="priority">Priority to get items with</param>
        ''' <returns>All items with same priority as <paramref name="priority"/>. Empty enumeration if there are no such items.</returns>
        Public Overloads Function GetItemsWithSamePriority(ByVal priority As TPriority) As IEnumerable(Of TValue)
            Dim Dumy As New KeyValuePair(Of TPriority, TValue)(priority, Nothing)
            Return From itm In Me.GetItemsWithSamePriority(Dumy) Select itm.Value
        End Function
        ''' <summary>Gets value indicating if collection contains given item</summary>
        ''' <param name="item">Item to find</param>
        ''' <returns>Ture if equal item is present; false if not</returns>
        Public Overloads Function Contains(ByVal item As TValue) As Boolean
            For Each ii In DirectCast(Me, IEnumerable(Of KeyValuePair(Of TPriority, TValue)))
                If ii.Value.Equals(item) Then Return True
            Next
            Return False
        End Function

        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        Public Overloads Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of TValue) Implements System.Collections.Generic.IEnumerable(Of TValue).GetEnumerator
            Return New UnwrapEnumerator(Of KeyValuePair(Of TPriority, TValue), TValue)(MyBase.GetEnumerator, Function(a) a.Value)
        End Function
    End Class
End Namespace
#End If