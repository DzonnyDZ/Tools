#If Config <= release Then
Namespace CollectionsT.GenericT
    ''' <summary>Wpars type-unsafe <see cref="IEnumerable"/> as type-safe <see cref="IEnumerable(Of T)"/></summary>
    ''' <typeparam name="T">Type that each item of wrapped collection must be of or convertible to</typeparam>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(Wrapper(Of )), LastChMMDDYYYY:="01/07/2007")> _
    Public Class Wrapper(Of T) : Implements IEnumerable(Of T)
        ''' <summary>Contains value of the <see cref="Wrapped"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Wrapped As IEnumerable
        ''' <summary>CTor</summary>
        ''' <param name="WrapThis">Collection to be wrapped</param>
        ''' <exception cref=" ArgumentNullException"><paramref name="WrapThis"/> is null</exception>
        Public Sub New(ByVal WrapThis As IEnumerable)
            If WrapThis Is Nothing Then Throw New ArgumentNullException("WrapThis", "WrapThis cannot be null")
            Wrapped = WrapThis
        End Sub
        ''' <summary>Wrapped value</summary>
        ''' <exception cref="ArgumentNullException">Setting value to null</exception>
        ''' <remarks>Changing this value doesn't invalidate enumerators, so enumerations continues although the content of wrapper has changed</remarks>
        Public Overridable Property Wrapped() As IEnumerable
            <DebuggerStepThrough()> Get
                Return _Wrapped
            End Get
            <DebuggerStepThrough()> Set(ByVal value As IEnumerable)
                If value Is Nothing Then Throw New ArgumentNullException("value", "Wrapped cannot be set to null")
                _Wrapped = value
            End Set
        End Property
        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="System.Collections.Generic.IEnumerator(Of T)"/> that can be used to iterate through the collection.</returns>
        Public Overridable Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
            Return New Enumerator(Wrapped.GetEnumerator)
        End Function
        ''' <summary>Returns an enumerator that iterates through a collection.</summary>
        ''' <returns>An <see cref="System.Collections.IEnumerator"/> object that can be used to iterate through the collection</returns>
        <EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use type-safe GetEnumerator instead")> _
        Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

        ''' <summary>Wraps type-unsafe <see cref="IEnumerator"/> as type-safe <see cref="IEnumerator(Of T)"/></summary>
        Protected Class Enumerator : Implements IEnumerator(Of T)
            ''' <summary>type-unsafe <see cref="IEnumerator"/> to be wrapped</summary>
            Private ReadOnly Wrap As IEnumerator
            ''' <summary>CTor</summary>
            ''' <param name="Wrap">type-unsafe <see cref="IEnumerator"/> to be wrapped</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Wrap"/> is null</exception>
            Public Sub New(ByVal Wrap As IEnumerator)
                If Wrap Is Nothing Then Throw New ArgumentNullException("Wrap", "Wrap cannot be null")
                Me.Wrap = Wrap
            End Sub
            ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
            ''' <returns>The element in the collection at the current position of the enumerator.</returns>
            ''' <exception cref="InvalidCastException">Current value from collection cannot be converted to <see cref="T"/>. Also another exception can be throw if thrown by cast operator.</exception>
            Public Overridable ReadOnly Property Current() As T Implements System.Collections.Generic.IEnumerator(Of T).Current
                Get
                    Return Wrap.Current
                End Get
            End Property
            ''' <summary>Gets the current element in the collection.</summary>
            ''' <returns>The current element in the collection.</returns>
            ''' <exception cref="System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
            <EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use type-safe Current instead")> _
            Public ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
                Get
                    Return Wrap
                End Get
            End Property
            ''' <summary>Advances the enumerator to the next element of the collection.</summary>
            ''' <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
            ''' <exception cref="System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
            Public Overridable Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
                Return Wrap.MoveNext
            End Function
            ''' <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
            ''' <exception cref="System.InvalidOperationException">The collection was modified after the enumerator was created</exception>
            Public Overridable Sub Reset() Implements System.Collections.IEnumerator.Reset
                Wrap.Reset()
            End Sub
#Region " IDisposable Support "
            ''' <summary>To detect redundant calls</summary>
            Private disposedValue As Boolean = False

            ''' <summary><see cref="IDisposable"/></summary>
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

    ''' <summary>Wraps type-unsafe <see cref="IList"/> as type-safe <see cref="IList(Of T)"/></summary>
    <Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(ListWrapper(Of )), LastChMMDDYYYY:="07/22/2007")> _
    Public Class ListWrapper(Of T)
        Inherits Wrapper(Of T)
        Implements IList(Of T), IList
        ''' <summary>CTor</summary>
        ''' <param name="List">Item to be wrapped</param>
        Public Sub New(ByVal List As IList)
            MyBase.New(List)
        End Sub
        ''' <summary>Wrapped list</summary>
        Public ReadOnly Property List() As IList
            Get
                Return Wrapped
            End Get
        End Property
        ''' <summary>Wrapped value</summary>
        ''' <exception cref="ArgumentNullException">Setting value to null</exception>
        ''' <remarks>Changing this value doesn't invalidate enumerators, so enumerations continues although the content of wrapper has changed</remarks>
        ''' <exception cref="ArgumentException">Value being set does not implement <see cref="IList"/></exception>
        Public Overrides Property Wrapped() As System.Collections.IEnumerable
            Get
                Return MyBase.Wrapped
            End Get
            Set(ByVal value As System.Collections.IEnumerable)
                If Not TypeOf value Is IList Then Throw New ArgumentException("Only IList instances can be used in ListWrapper")
                MyBase.Wrapped = value
            End Set
        End Property
#Region "IList(Of T)"
        ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</summary>
        ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        ''' <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        Public Sub Add(ByVal item As T) Implements System.Collections.Generic.ICollection(Of T).Add
            List.Add(item)
        End Sub
        ''' <summary>Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</summary>
        ''' <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only. </exception>
        Public Sub Clear() Implements System.Collections.Generic.ICollection(Of T).Clear, System.Collections.IList.Clear
            List.Clear()
        End Sub
        ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific value.</summary>
        ''' <returns>true if item is found in the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.</returns>
        ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        Public Function Contains(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Contains
            Return List.Contains(item)
        End Function
        ''' <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"></see> to an <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing.</param>
        ''' <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception>
        ''' <exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception>
        ''' <exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.-or-arrayIndex is equal to or greater than the length of <paramref name="array"/>.-or-The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"></see> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination array.</exception>
        ''' <exception cref="InvalidCastException"><see cref="List"/> contains item that cannot be automatically cast to <see cref="T"/></exception>
        Public Sub CopyTo(ByVal array() As T, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of T).CopyTo
            If arrayIndex < 0 Then Throw New ArgumentOutOfRangeException("arrayIndex", "arrayIndex must be non-negative")
            If array Is Nothing Then Throw New ArgumentNullException("array")
            If array.Rank <> 1 Then Throw New ArgumentException("array is multidimensional", "array")
            If arrayIndex >= array.Length Then Throw New ArgumentException("arrayIndex is greater than or equal to lenght of array")
            If array.Length - arrayIndex < Me.Count Then Throw New ArgumentException("There is not engough space in array after arrayIndex to place all items from collection")
            Dim i As Integer = arrayIndex
            For Each item As T In Me
                array(i) = item
                i += 1
            Next item
        End Sub
        ''' <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</summary>
        ''' <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</returns>
        Public ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of T).Count, System.Collections.ICollection.Count
            Get
                Return List.Count
            End Get
        End Property
        ''' <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</summary>
        ''' <returns><see cref="List">List</see>.<see cref="System.Collections.IList.IsReadOnly">IsReadOnly</see></returns>
        Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of T).IsReadOnly, System.Collections.IList.IsReadOnly
            Get
                Return List.IsReadOnly
            End Get
        End Property
        ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</summary>
        ''' <returns>true if item was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false. This method also returns false if item is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"></see>.</returns>
        ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        ''' <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        Public Function Remove(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Remove
            Remove = List.IndexOf(item) >= 0
            List.Remove(item)
        End Function
        ''' <summary>Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1"></see>.</summary>
        ''' <returns>The index of item if found in the list; otherwise, -1.</returns>
        ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1"></see>.</param>
        Public Function IndexOf(ByVal item As T) As Integer Implements System.Collections.Generic.IList(Of T).IndexOf
            Return List.IndexOf(item)
        End Function
        ''' <summary>Inserts an item to the <see cref="T:System.Collections.Generic.IList`1"></see> at the specified index.</summary>
        ''' <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1"></see>.</param>
        ''' <param name="index">The zero-based index at which item should be inserted.</param>
        ''' <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1"></see> is read-only.</exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"></see>.</exception>
        Public Sub Insert(ByVal index As Integer, ByVal item As T) Implements System.Collections.Generic.IList(Of T).Insert
            List.Insert(index, item)
        End Sub
        ''' <summary>Gets or sets the element at the specified index.</summary>
        ''' <returns>The element at the specified index.</returns>
        ''' <param name="index">The zero-based index of the element to get or set.</param>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"></see>.</exception>
        ''' <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.Generic.IList`1"></see> is read-only.</exception>
        Default Public Property Item(ByVal index As Integer) As T Implements System.Collections.Generic.IList(Of T).Item
            Get
                Return List(index)
            End Get
            Set(ByVal value As T)
                List(index) = value
            End Set
        End Property
        ''' <summary>Removes the <see cref="T:System.Collections.Generic.IList`1"></see> item at the specified index.</summary>
        ''' <param name="index">The zero-based index of the item to remove.</param>
        ''' <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1"></see> is read-only.</exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"></see>.</exception>
        Public Sub RemoveAt(ByVal index As Integer) Implements System.Collections.Generic.IList(Of T).RemoveAt, System.Collections.IList.RemoveAt
            List.RemoveAt(index)
        End Sub
#End Region
#Region "IList"
        ''' <summary>Copies the elements of the <see cref="T:System.Collections.ICollection"></see> to an <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing. </param>
        ''' <param name="index">The zero-based index in array at which copying begins. </param>
        ''' <exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null. </exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero. </exception>
        ''' <exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.-or- <paramref name="index"/> is equal to or greater than the length of <paramref name="array"/>.-or- The number of elements in the source <see cref="T:System.Collections.ICollection"></see> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>. </exception>
        ''' <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.ICollection"></see> cannot be cast automatically to the type of the destination <paramref name="array"/>. </exception>
        ''' <remarks>This method is provided only for compatibility with <see cref="IList"/>. Use type-safe <see cref="CopyTo"/> instead</remarks>
        <Obsolete("Use type-safe overload instead")> _
        Private Sub CopyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo
            List.CopyTo(array, index)
        End Sub
        ''' <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"></see> is synchronized (thread safe).</summary>
        ''' <returns>true if access to the <see cref="T:System.Collections.ICollection"></see> is synchronized (thread safe); otherwise, false.</returns>
        Private ReadOnly Property IsSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
            Get
                Return List.IsSynchronized
            End Get
        End Property
        ''' <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"></see>.</summary>
        ''' <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"></see>.</returns>
        Private ReadOnly Property SyncRoot() As Object Implements System.Collections.ICollection.SyncRoot
            Get
                Return List.SyncRoot
            End Get
        End Property
        ''' <summary>Adds an item to the <see cref="T:System.Collections.IList"></see>.</summary>
        ''' <returns>The position into which the new element was inserted.</returns>
        ''' <param name="value">The <see cref="T:System.Object"></see> to add to the <see cref="T:System.Collections.IList"></see>. </param>
        ''' <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList"></see> is read-only.-or- The <see cref="T:System.Collections.IList"></see> has a fixed size. </exception>
        ''' <remarks>This function is provided only for compatibility with <see cref="IList"/>. Use type-safe <see cref="Add"/> instead</remarks>
        <Obsolete("Use type-safe overload instead")> _
        Private Function Add(ByVal value As Object) As Integer Implements System.Collections.IList.Add
            Add(CType(value, T))
        End Function
        ''' <summary>Determines whether the <see cref="T:System.Collections.IList"></see> contains a specific value.</summary>
        ''' <returns>true if the <see cref="T:System.Object"></see> is found in the <see cref="T:System.Collections.IList"></see>; otherwise, false.</returns>
        ''' <param name="value">The <see cref="T:System.Object"></see> to locate in the <see cref="T:System.Collections.IList"></see>. </param>
        ''' <remarks>This function is provided only for compatibility with <see cref="IList"/>. Use type-safe <see cref="Contains"/> instead</remarks>
        <Obsolete("Use type-safe overload instead")> _
        Private Function Contains(ByVal value As Object) As Boolean Implements System.Collections.IList.Contains
            Return List.Contains(value)
        End Function
        ''' <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList"></see>.</summary>
        ''' <returns>The index of value if found in the list; otherwise, -1.</returns>
        ''' <param name="value">The <see cref="T:System.Object"></see> to locate in the <see cref="T:System.Collections.IList"></see>. </param>
        ''' <remarks>This function is provided only for compatibility with <see cref="IList"/>. Use type-safe <see cref="IndexOf"/> instead</remarks>
        <Obsolete("Use type-safe overload instead")> _
        Private Function IndexOf(ByVal value As Object) As Integer Implements System.Collections.IList.IndexOf
            Return List.IndexOf(value)
        End Function
        ''' <summary>Inserts an item to the <see cref="T:System.Collections.IList"></see> at the specified index.</summary>
        ''' <param name="value">The <see cref="T:System.Object"></see> to insert into the <see cref="T:System.Collections.IList"></see>. </param>
        ''' <param name="index">The zero-based index at which value should be inserted. </param>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.IList"></see>. </exception>
        ''' <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList"></see> is read-only.-or- The <see cref="T:System.Collections.IList"></see> has a fixed size. </exception>
        ''' <exception cref="T:System.NullReferenceException">value is null reference in the <see cref="T:System.Collections.IList"></see>.</exception>
        ''' <remarks>This function is provided only for compatibility with <see cref="IList"/>. Use type-safe <see cref="Insert"/> instead. This method allows you to insert item of othert type than <see cref="T"/></remarks>
        <Obsolete("Use type-safe overload instead")> _
        Private Sub Insert(ByVal index As Integer, ByVal value As Object) Implements System.Collections.IList.Insert
            Insert(index, CType(value, T))
        End Sub
        ''' <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList"></see> has a fixed size.</summary>
        ''' <returns>true if the <see cref="T:System.Collections.IList"></see> has a fixed size; otherwise, false.</returns>
        Public ReadOnly Property IsFixedSize() As Boolean Implements System.Collections.IList.IsFixedSize
            Get
                Return List.IsFixedSize
            End Get
        End Property
        ''' <summary>Gets or sets the element at the specified index.</summary>
        ''' <returns>The element at the specified index.</returns>
        ''' <param name="index">The zero-based index of the element to get or set. </param>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.IList"></see>. </exception>
        ''' <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.IList"></see> is read-only. </exception>
        ''' <remarks>This function is provided only for compatibility with <see cref="IList"/>. Use type-safe <see cref="Item"/> instead. This property allows you to set item that is not of type <see cref="T"/></remarks>
        <Obsolete("Use type-safe Item")> _
        Private Property UnsafeItem(ByVal index As Integer) As Object Implements System.Collections.IList.Item
            Get
                Return List(index)
            End Get
            Set(ByVal value As Object)
                Me(index) = value
            End Set
        End Property
        ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList"></see>.</summary>
        ''' <param name="value">The <see cref="T:System.Object"></see> to remove from the <see cref="T:System.Collections.IList"></see>. </param>
        ''' <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList"></see> is read-only.-or- The <see cref="T:System.Collections.IList"></see> has a fixed size. </exception>
        ''' <remarks>This function is provided only for compatibility with <see cref="IList"/>. Use type-safe <see cref="Item"/> instead.</remarks>
        <Obsolete("Use type-safe overload instead")> _
        Public Sub Remove(ByVal value As Object) Implements System.Collections.IList.Remove
            List.Remove(value)
        End Sub
#End Region
    End Class
End Namespace
#End If