Namespace CollectionsT.GenericT
#If True
    ''' <summary>Rapresent anything that can be indexed by anything</summary>
    ''' <typeparam name="TIndex">Data type of indexes</typeparam>
    ''' <typeparam name="TItem">Datatype of items</typeparam>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Interface IIndexable(Of TItem, TIndex)
        Inherits IReadOnlyIndexable(Of TItem, TIndex)
        ''' <summary>Gets or sets value on specified index</summary>
        ''' <param name="index">Index to set or obtain value</param>
        ''' <returns>value lying on specified <paramref name="index"/></returns>
        ''' <value>New value to be stored at specified index</value>
        ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
        Default Shadows Property Item(ByVal index As TIndex) As TItem
    End Interface
    ''' <summary>Rapresent anything that can be indexed by anything for readonly access</summary>
    ''' <typeparam name="TIndex">Data type of indexes</typeparam>
    ''' <typeparam name="TItem">Datatype of items</typeparam>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Interface IReadOnlyIndexable(Of TItem, TIndex)
        ''' <summary>Gets value on specified index</summary>
        ''' <param name="index">Index to obtain value</param>
        ''' <returns>value lying on specified <paramref name="index"/></returns>
        ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
        Default ReadOnly Property Item(ByVal index As TIndex) As TItem
    End Interface
    ''' <summary>Represents anythign that can be indexed by <see cref="Long"/></summary>
    ''' <typeparam name="TItem">Data type of items</typeparam>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Interface IIndexableWithRange(Of TItem, TIndex)
        Inherits IReadOnlyIndexableWithRange(Of TItem, TIndex)
        Inherits IIndexable(Of TItem, TIndex)
        Inherits IEnumerable(Of TItem)
    End Interface
    ''' <summary>Represents anythign that can be indexed by <see cref="Integer"/> for readonly acces</summary>
    ''' <typeparam name="TItem">Data type of items</typeparam>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Interface IReadOnlyIndexableWithRange(Of TItem, TIndex)
        Inherits IReadOnlyIndexable(Of TItem, TIndex)
        Inherits IEnumerable(Of TItem)
        ''' <summary>Minimal valid value for index</summary>
        ReadOnly Property Minimum() As TIndex
        ''' <summary>Maximal valid value for index</summary>
        ReadOnly Property Maximum() As TIndex
    End Interface
#End If
#If True
    ''' <summary>Common base for indexable enumerators</summary>
    ''' <typeparam name="TItem">Type of items in collection</typeparam>
    ''' <typeparam name="TIndex">Type of index</typeparam>
    Public MustInherit Class IndexableEnumeratorBase(Of TItem, TIndex)
        Implements IEnumerator(Of TItem)
        ''' <summary>Gets value indicating if one index is smaller than other</summary>
        ''' <param name="a">Index which should be smaller</param>
        ''' <param name="b">Index which should be greater</param>
        ''' <returns>true if <paramref name="a"/> &lt; <paramref name="b"/></returns>
        Protected MustOverride Function IsSmaller(ByVal a As TIndex, ByVal b As TIndex) As Boolean
        ''' <summary>Gets value indicating if one index is greater than other</summary>
        ''' <param name="a">Index which should be smaller</param>
        ''' <param name="b">Index which should be greater</param>
        ''' <returns>true if <paramref name="a"/> > <paramref name="b"/></returns>
        Protected MustOverride Function IsGreater(ByVal a As TIndex, ByVal b As TIndex) As Boolean
        ''' <summary>Gets index by 1 greater than given</summary>
        ''' <param name="a">An index to increment</param>
        ''' <returns><paramref name="a"/> + 1</returns>
        Protected MustOverride Function Increment(ByVal a As TIndex) As TIndex
        ''' <summary>Gets index by 1 smaller than given</summary>
        ''' <param name="a">An index to decrement</param>
        ''' <returns><paramref name="a"/> - 1</returns>
        ''' <remarks>Function must be able to decrement to value <see cref="Collection"/>.<see cref="IReadOnlyIndexableWithRange(Of TItem, TIndex).Minimum">Minimum</see> - 1</remarks>
        Protected MustOverride Function Decrement(ByVal a As TIndex) As TIndex
        ''' <summary>Gets value indicating if one index is equals to other</summary>
        ''' <param name="a">An index</param>
        ''' <param name="b">An index</param>
        ''' <returns>true if <paramref name="a"/> == <paramref name="b"/></returns>
        Protected MustOverride Overloads Function Equals(ByVal a As TIndex, ByVal b As TIndex) As Boolean
        ''' <summary><see cref="IReadOnlyIndexableWithRange(Of TItem, TIndex)"/> being enumerated</summary>
        Protected Collection As IReadOnlyIndexableWithRange(Of TItem, TIndex)
        ''' <summary>Curent position</summary>
        Protected Position As TIndex
        ''' <summary>CTor</summary>
        ''' <param name="Collection"><see cref="IReadOnlyIndexableWithRange(Of TItem, TIndex)"/> to enumerate through</param>
        Protected Sub New(ByVal Collection As IReadOnlyIndexableWithRange(Of TItem, TIndex))
            Me.Collection = Collection
            Position = Decrement(Collection.Minimum)
        End Sub
        ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        ''' <returns>The element in the collection at the current position of the enumerator.</returns>
        ''' <exception cref="System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
        Public ReadOnly Property Current() As TItem Implements System.Collections.Generic.IEnumerator(Of TItem).Current
            Get
                If IsSmaller(Position, Collection.Minimum) OrElse IsGreater(Position, Collection.Maximum) Then Throw New InvalidOperationException(ResourcesT.Exceptions.EnumeratorIsPositionedOutsideIReadOnlyIndexableBounds)
                Return Collection(Position)
            End Get
        End Property
        ''' <summary>Gets the current element in the collection.</summary>
        ''' <returns>The current element in the collection.</returns>
        ''' <exception cref="System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
        <EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use type safe Curent instead")> _
        Private ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
            Get
                Return Current
            End Get
        End Property
        ''' <summary>Advances the enumerator to the next element of the collection.</summary>
        ''' <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            Position = Increment(Position)
            Return IsSmaller(Position, Collection.Maximum) OrElse Equals(Position, Collection.Maximum)
        End Function
        ''' <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            Position = Decrement(Collection.Minimum)
        End Sub
#Region " IDisposable Support "
        ''' <summary>To detect redundant calls</summary>
        Private disposedValue As Boolean = False

        ''' <summary>IDisposable</summary>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    Collection = Nothing
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

    ''' <summary>Implements enumerator of <see cref="IReadOnlyIndexable(Of TItem, TIndex)"/></summary>
    Public Class LongIndexableEnumerator(Of TItem)
        Inherits IndexableEnumeratorBase(Of TItem, Long)
        ''' <summary>CTor</summary>
        ''' <param name="Collection"><see cref="IReadOnlyIndexableWithRange(Of TItem, TIndex)"/> to enumerate through</param>
        Public Sub New(ByVal Collection As IReadOnlyIndexable(Of TItem, Long))
            MyBase.New(Collection)
        End Sub
        ''' <summary>Gets index by 1 smaller than given</summary>
        ''' <param name="a">An index to decrement</param>
        ''' <returns><paramref name="a"/> - 1</returns>
        Protected Overrides Function Decrement(ByVal a As Long) As Long
            Return a - 1
        End Function

        ''' <summary>Gets value indicating if one index is equals to other</summary>
        ''' <param name="a">An index</param>
        ''' <param name="b">An index</param>
        ''' <returns>true if <paramref name="a"/> == <paramref name="b"/></returns>
        Protected Overloads Overrides Function Equals(ByVal a As Long, ByVal b As Long) As Boolean
            Return a = b
        End Function

        ''' <summary>Gets index by 1 greater than given</summary>
        ''' <param name="a">An index to increment</param>
        ''' <returns><paramref name="a"/> + 1</returns>
        Protected Overrides Function Increment(ByVal a As Long) As Long
            Return a + 1
        End Function

        ''' <summary>Gets value indicating if one index is greater than other</summary>
        ''' <param name="a">Index which should be smaller</param>
        ''' <param name="b">Index which should be greater</param>
        ''' <returns>true if <paramref name="a"/> > <paramref name="b"/></returns>
        Protected Overrides Function IsGreater(ByVal a As Long, ByVal b As Long) As Boolean
            Return a > b
        End Function

        ''' <summary>Gets value indicating if one index is smaller than other</summary>
        ''' <param name="a">Index which should be smaller</param>
        ''' <param name="b">Index which should be greater</param>
        ''' <returns>true if <paramref name="a"/> &lt; <paramref name="b"/></returns>
        Protected Overrides Function IsSmaller(ByVal a As Long, ByVal b As Long) As Boolean
            Return a < b
        End Function
    End Class
#End If

#If True
    ''' <summary>Represents simple type-safe interface for read-only collection</summary>
    ''' <typeparam name="T">Type of items in collections</typeparam>
    Public Interface IReadOnlyCollection(Of T) : Inherits IEnumerable(Of T)
        ''' <summary>Gets the number of elements contained in the <see cref="IReadOnlyCollection(Of T)"></see>.</summary>
        ''' <returns>The number of elements contained in the <see cref="IReadOnlyCollection(Of T)"></see>.</returns>
        ''' <filterpriority>2</filterpriority>
        ReadOnly Property Count() As Integer
        ''' <summary>Copies the elements of the <see cref="IReadOnlyCollection(Of T)"></see> to an <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements copied from <see cref="IReadOnlyCollection(Of T)"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing. </param>
        ''' <param name="index">The zero-based index in array at which copying begins. </param>
        ''' <exception cref="T:System.ArgumentNullException">array is null. </exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero. </exception>
        ''' <exception cref="T:System.ArgumentException">array is multidimensional.-or- index is equal to or greater than the length of array.-or- The number of elements in the source <see cref="T:System.Collections.ICollection"></see> is greater than the available space from index to the end of the destination array. </exception>
        ''' <exception cref="T:System.InvalidCastException">The type of the source <see cref="IReadOnlyCollection(Of T)"></see> cannot be cast automatically to the type of the destination array. </exception>
        ''' <filterpriority>2</filterpriority>
        Sub CopyTo(ByVal array As T(), ByVal index As Integer)
    End Interface
    ''' <summary>Represets read-only indexable collection</summary>
    ''' <typeparam name="TIndex">Type of items in collection</typeparam>
    ''' <typeparam name="TItem">Type of index</typeparam>
    Public Interface IReadOnlyIndexableCollection(Of TItem, TIndex)
        Inherits IReadOnlyCollection(Of TItem)
        Inherits IReadOnlyIndexableEnumerable(Of TItem, TIndex)
    End Interface
    ''' <summary>Represents collection that can be indexed</summary>
    ''' <typeparam name="TIndex">Type of items in collection</typeparam>
    ''' <typeparam name="TItem">Type of index</typeparam>
    Public Interface IIndexableCollection(Of TItem, TIndex)
        Inherits ICollection(Of TItem)
        Inherits IAddableIndexable(Of TItem, TIndex)
    End Interface
    ''' <summary>Represents <see cref="IIndexable(Of TItem, TIndex)"/> that provides count of items within it</summary>
    ''' <typeparam name="TIndex">Type of items in collection</typeparam>
    ''' <typeparam name="TItem">Type of index</typeparam>
    Public Interface IIndexableWithCount(Of TItem, TIndex)
        Inherits IReadOnlyIndexableWithCount(Of TItem, TIndex)
        Inherits IIndexableEnumerable(Of TItem, TIndex)
    End Interface
    ''' <summary>Represents <see cref="IReadOnlyIndexable"/> that provides count of items within it</summary>
    ''' <typeparam name="TIndex">Type of items in collection</typeparam>
    ''' <typeparam name="TItem">Type of index</typeparam>
    Public Interface IReadOnlyIndexableWithCount(Of TItem, TIndex)
        Inherits IReadOnlyCollection(Of TItem)
        Inherits IReadOnlyIndexableEnumerable(Of TItem, TIndex)
    End Interface
    ''' <summary>Represents collection which allows adding items</summary>
    ''' <typeparam name="T">Type of items in clollection</typeparam>
    Public Interface IAddable(Of T)
        Inherits IReadOnlyCollection(Of T)
        ''' <summary>Adds item to collection</summary>
        ''' <param name="item">Item to be added</param>
        Sub Add(ByVal item As T)
    End Interface
    ''' <summary>Represents indexable collection which allows adding items</summary>
    ''' <typeparam name="TIndex">Type of items in collection</typeparam>
    ''' <typeparam name="TItem">Type of index</typeparam>
    Public Interface IAddableIndexable(Of TItem, TIndex)
        Inherits IAddable(Of TItem)
        Inherits IIndexableEnumerable(Of TItem, TIndex)
    End Interface
    ''' <summary>Represents something read-only that can be indexed and enumerated</summary>
    ''' <typeparam name="TIndex">Type of items in collection</typeparam>
    ''' <typeparam name="TItem">Type of index</typeparam>
    Public Interface IReadOnlyIndexableEnumerable(Of TItem, TIndex)
        Inherits IEnumerable(Of TItem)
        Inherits IReadOnlyIndexable(Of TItem, TIndex)
    End Interface
    ''' <summary>Represents somethign that can be indexed and enumerated</summary>
    ''' <typeparam name="TIndex">Type of items in collection</typeparam>
    ''' <typeparam name="TItem">Type of index</typeparam>
    Public Interface IIndexableEnumerable(Of TItem, TIndex)
        Inherits IReadOnlyIndexableEnumerable(Of TItem, TIndex)
        Inherits IIndexable(Of TItem, TIndex)
    End Interface
    ''' <summary>Interface of something from which can be removed somethign at specified position (with specified key)</summary>
    ''' <typeparam name="TIndex">Type of index</typeparam>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Interface IRemovable(Of TIndex)
        ''' <summary>Removes item at specified index</summary>
        ''' <param name="Index">Index to remove item at</param>
        ''' <exception cref="ArgumentException">Index is not valid</exception>
        Sub RemoveAt(ByVal Index As TIndex)
    End Interface
    ''' <summary><see cref="IIndexable(Of TItem, TIndex)"/> where items can be removed</summary>
    ''' <typeparam name="TIndex">Type of items in collection</typeparam>
    ''' <typeparam name="TItem">Type of index</typeparam>
    Public Interface IIndexableRemovable(Of TItem, TIndex)
        Inherits IIndexable(Of titem, tindex)
        Inherits IRemovable(Of tindex)
    End Interface
    ''' <summary>Collection where items can be added and removed at specified index</summary>
    ''' <typeparam name="TIndex">Type of items in collection</typeparam>
    ''' <typeparam name="TItem">Type of index</typeparam>
    Public Interface IAddableRemovable(Of TItem, TIndex)
        Inherits IAddableIndexable(Of TItem, TIndex)
        Inherits IRemovable(Of TIndex)
    End Interface
    ''' <summary><see cref="IReadOnlyIndexable(Of TItem, TIndex)"/> that has <see cref="M:Tools.CollectionsT.GenericT.IReadOnlySearchable`2.Contains"/> and <see cref="M:Tools.CollectionsT.GenericT.IReadOnlySearchable`2.IndexOf"/> functions</summary>
    ''' <typeparam name="TIndex">Type of items in collection</typeparam>
    ''' <typeparam name="TItem">Type of index</typeparam>
    Public Interface IReadOnlySearchable(Of TItem, TIndex)
        Inherits IReadOnlyIndexable(Of TItem, TIndex)
        ''' <summary>Gets value indicating if the collection contains given object</summary>
        ''' <param name="item">Object to search for</param>
        ''' <returns>True if collection contains <paramref name="item"/>; false otherwise</returns>
        Function Contains(ByVal item As TItem) As Boolean
        ''' <summary>Gets index at which lies given object</summary>
        ''' <param name="item">Object to search for</param>
        ''' <returns>Index of first occurence of <paramref name="item"/> in collection. If <paramref name="item"/> is not present in collection returns collection-specific value. Number-indexed collections usually returns -1.</returns>
        Function IndexOf(ByVal item As TItem) As TIndex
    End Interface
    ''' <summary><see cref="IIndexable(Of TItem, TIndex)"/> that has <see cref="M:Tools.CollectionsT.GenericT.IReadOnlySearchable`2.Contains"/> and <see cref="M:Tools.CollectionsT.GenericT.IReadOnlySearchable`2.IndexOf"/> functions</summary>
    ''' <typeparam name="TIndex">Type of items in collection</typeparam>
    ''' <typeparam name="TItem">Type of index</typeparam>
    Public Interface ISearchable(Of TItem, TIndex)
        Inherits IReadOnlySearchable(Of TItem, TIndex)
        Inherits IIndexable(Of TItem, TIndex)
    End Interface
    ''' <summary>Represents indexable collection where items can be inserted at specified index</summary>
    ''' <typeparam name="TIndex">Type of items in collection</typeparam>
    ''' <typeparam name="TItem">Type of index</typeparam>
    Public Interface IInsertable(Of TItem, TIndex)
        Inherits IIndexable(Of TItem, TIndex)
        ''' <summary>Inserts item into collection at specified index</summary>
        ''' <param name="index">Index to insert item onto</param>
        ''' <param name="item">Item to be inserted</param>
        Sub Insert(ByVal index As TIndex, ByVal item As TItem)
    End Interface
#End If
End Namespace

