Imports System.Runtime.CompilerServices
Imports System.ComponentModel.Design
Imports System.Configuration.Provider
Imports System.Configuration
Imports System.Data
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Drawing.Printing.PrinterSettings
Imports System.Drawing.Printing
Imports System.Net

#If Config <= Nightly Then
Imports Tools.CollectionsT.GenericT, System.Linq
Namespace CollectionsT.SpecialzedT
    ''' <summary>Provides abstract base class and static methods for wrapping type-unsafe <see cref="ICollection"/> as type-safe <see cref="IReadOnlyIndexableCollection(Of Integer, T)"/></summary>
    ''' <remarks>Derived classes should derive from <see cref="SpecializedReadOnlyWrapper(Of T)"/> or <see cref="SpecializedWrapper(Of T)"/> instead</remarks>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Partial Public MustInherit Class SpecializedWrapper
        Implements IReadOnlyIndexableCollection(Of Object, Integer), ICollection
        ''' <summary>Collection being wrapped</summary>
        Protected ReadOnly Collection As ICollection
        ''' <summary>CTor</summary>
        ''' <param name="Collection">Collection to be wrapped</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Friend Sub New(ByVal Collection As ICollection)
            If Collection Is Nothing Then Throw New ArgumentException("Collection")
            Me.Collection = Collection
        End Sub
#Region "Interface Implementation"
        ''' <summary>Gets value on specified index</summary>
        ''' <param name="index">Index to obtain value</param>
        ''' <returns>value lying on specified <paramref name="index"/></returns>
        ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
        Protected MustOverride ReadOnly Property UnsafeReadOnlyItem(ByVal index As Integer) As Object Implements GenericT.IReadOnlyIndexable(Of Object, Integer).Item
        ''' <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
        ''' <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
        ''' <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is null. </exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is less than zero. </exception>
        ''' <exception cref="T:System.ArgumentException">
        ''' <paramref name="array" /> is multidimensional.-or- 
        ''' <paramref name="index" /> is equal to or greater than the length of <paramref name="array" />.-or-
        ''' The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination  <paramref name="array" />. </exception>
        ''' <exception cref="T:System.ArgumentException">The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />. </exception>
        ''' <filterpriority>2</filterpriority>
        Protected Sub CopyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo
            Collection.CopyTo(array, index)
        End Sub
        ''' <summary>Copies the elements of the <see cref="IReadOnlyCollection(Of T)"></see> to an <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements copied from <see cref="IReadOnlyCollection(Of T)"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing. </param>
        ''' <param name="index">The zero-based index in array at which copying begins. </param>
        ''' <exception cref="T:System.ArgumentNullException">array is null. </exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException">index is less than zero. </exception>
        ''' <exception cref="T:System.ArgumentException">array is multidimensional.-or- index is equal to or greater than the length of array.-or- The number of elements in the source <see cref="T:System.Collections.ICollection"></see> is greater than the available space from index to the end of the destination array. </exception>
        ''' <exception cref="T:System.InvalidCastException">The type of the source <see cref="IReadOnlyCollection(Of T)"></see> cannot be cast automatically to the type of the destination array. </exception>
        ''' <filterpriority>2</filterpriority>
        Protected Sub Copy(ByVal array As Object(), ByVal index As Integer) Implements IReadOnlyIndexableCollection(Of Object, Integer).CopyTo
            Collection.CopyTo(array, index)
        End Sub

        ''' <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.</summary>
        ''' <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Overridable ReadOnly Property Count() As Integer Implements System.Collections.ICollection.Count, IReadOnlyCollection(Of Object).Count
            <DebuggerStepThrough()> Get
                Return Collection.Count
            End Get
        End Property

        ''' <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
        ''' <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
        ''' <filterpriority>2</filterpriority>
        Private ReadOnly Property IsSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
            <DebuggerStepThrough()> Get
                Return Collection.IsSynchronized
            End Get
        End Property

        ''' <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
        ''' <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
        ''' <filterpriority>2</filterpriority>
        Private ReadOnly Property SyncRoot() As Object Implements System.Collections.ICollection.SyncRoot
            <DebuggerStepThrough()> Get
                Return Collection.SyncRoot
            End Get
        End Property

        ''' <summary>Returns an enumerator that iterates through a collection.</summary>
        ''' <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        ''' <filterpriority>2</filterpriority>
        Private Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Collection.GetEnumerator
        End Function
        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of Object) Implements System.Collections.Generic.IEnumerable(Of Object).GetEnumerator
            Return Collection.GetEnumerator
        End Function
#End Region
        ''' <summary>Provides common base class for wrappers of type-unsafe <see cref="IList"/> to type-safe <see cref="IList(Of T)"/></summary>
        ''' <typeparam name="TCollection">Type of collection being wrapped</typeparam>
        ''' <typeparam name=" TItem">Type of item in collection being wrapped</typeparam>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public MustInherit Class IListTypeSafeWrapper(Of TCollection As IList, TItem)
            Inherits SpecializedWrapper(Of TCollection, TItem)
            Implements IList, IList(Of TItem), IInsertable(Of TItem, Integer), ISearchable(Of TItem, Integer), IAddableRemovable(Of TItem, Integer), IReadOnlyIndexable(Of TItem, Integer)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to wrapp</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            ''' <remarks>Derived class is responsible for allowing only such collections which contains only items of type <typeparamref name="TItem"/></remarks>
            Protected Sub New(ByVal Collection As TCollection)
                MyBase.new(Collection)
            End Sub
            ''' <summary>Determines the index of a specific item in the <see cref="System.Collections.Generic.IList(Of T)" />.</summary>
            ''' <param name="item">The object to locate in the <see cref="System.Collections.Generic.IList(Of T)" />.</param>
            ''' <returns>The index of item if found in the list; otherwise, -1.</returns>
            Public Function IndexOf(ByVal item As TItem) As Integer Implements System.Collections.Generic.IList(Of TItem).IndexOf, GenericT.IReadOnlySearchable(Of TItem, Integer).IndexOf
                Return Collection.IndexOf(item)
            End Function
            ''' <summary>Inserts an item to the <see cref="System.Collections.Generic.IList(Of T)" /> at the specified index.</summary>
            ''' <param name="index">The zero-based index at which item should be inserted.</param>
            ''' <param name="item">The object to insert into the <see cref="System.Collections.Generic.IList(Of T)" />.</param>
            ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="System.Collections.Generic.IList(Of T)" />.</exception>
            ''' <exception cref="System.NotSupportedException">The <see cref="System.Collections.Generic.IList(Of T)" /> is read-only.</exception>
            Public Sub Insert(ByVal index As Integer, ByVal item As TItem) Implements System.Collections.Generic.IList(Of TItem).Insert, GenericT.IInsertable(Of TItem, Integer).Insert
                Collection.Insert(index, item)
            End Sub
            ''' <summary>Gets or sets the element at the specified index.</summary>
            ''' <param name="index">The zero-based index of the element to get or set.</param>
            ''' <returns>The element at the specified index.</returns>
            ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="System.Collections.Generic.IList(Of T)" />.</exception>
            ''' <exception cref="System.NotSupportedException">The property is set and the <see cref="System.Collections.Generic.IList(Of T)" /> is read-only.</exception>
            Default Public NotOverridable Overrides Property Item(ByVal index As Integer) As TItem Implements System.Collections.Generic.IList(Of TItem).Item
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As TItem)
                    Collection.Item(index) = value
                End Set
            End Property
            ''' <summary>Removes the <see cref="System.Collections.Generic.IList(Of T)" /> item at the specified index.</summary>
            ''' <param name="index">The zero-based index of the item to remove.</param>
            ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="System.Collections.Generic.IList(Of T)" />.</exception>
            ''' <exception cref="System.NotSupportedException">The <see cref="System.Collections.Generic.IList(Of T)" /> is read-only.</exception>
            Public Sub RemoveAt(ByVal index As Integer) Implements System.Collections.Generic.IList(Of TItem).RemoveAt, System.Collections.IList.RemoveAt, GenericT.IRemovable(Of Integer).RemoveAt
                Collection.RemoveAt(index)
            End Sub
            ''' <summary>Removes all items from the <see cref="System.Collections.IList" />.</summary>
            ''' <exception cref="System.NotSupportedException">The <see cref="System.Collections.IList" /> is read-only.</exception>
            Public NotOverridable Overrides Sub Clear() Implements System.Collections.IList.Clear
                Collection.Clear()
            End Sub
            ''' <summary>Gets a value indicating whether the <see cref="System.Collections.IList" /> has a fixed size.</summary>
            ''' <returns>true if the <see cref="System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
            Public ReadOnly Property IsFixedSize() As Boolean Implements System.Collections.IList.IsFixedSize
                Get
                    Return Collection.IsFixedSize
                End Get
            End Property
            ''' <summary>Gets a value indicating whether the <see cref="System.Collections.IList" /> is read-only.</summary>
            ''' <returns>true if the <see cref="System.Collections.IList" /> is read-only; otherwise, false.</returns>
            Public NotOverridable Overrides ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.IList.IsReadOnly
                Get
                    Return Collection.IsReadOnly
                End Get
            End Property
            ''' <summary>Gets value indicating if the collection contains given object</summary>
            ''' <param name="item">Object to search for</param>
            ''' <returns>True if collection contains <paramref name="item"/>; false otherwise</returns>
            Public NotOverridable Overrides Function Contains(ByVal item As TItem) As Boolean Implements GenericT.IReadOnlySearchable(Of TItem, Integer).Contains
                Return Collection.Contains(item)
            End Function
            ''' <summary>Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public NotOverridable Overrides Sub Add(ByVal item As TItem)
                Collection.Add(item)
            End Sub
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
            ''' <returns>true if 
            ''' <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if 
            ''' <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
            ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
            Public NotOverridable Overrides Function Remove(ByVal item As TItem) As Boolean
                Dim OldCount As Integer = Me.Count
                Collection.Remove(item)
                Return Me.Count < OldCount
            End Function
#Region "Unsafe"
            ''' <summary>Gets or sets the element at the specified index.</summary>
            ''' <param name="index">The zero-based index of the element to get or set.</param>
            ''' <returns>The element at the specified index.</returns>
            ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="System.Collections.IList" />.</exception>
            ''' <exception cref="System.NotSupportedException">The property is set and the <see cref="System.Collections.IList" /> is read-only.</exception>
            ''' <exception cref="TypeMismatchException">value being set is not of type <typeparemref name="TItem"/></exception>
            Private Property Item_Unsafe(ByVal index As Integer) As Object Implements System.Collections.IList.Item
                Get
                    Return Collection.Item(index)
                End Get
                Set(ByVal value As Object)
                    ThrowType(value)
                    Collection(index) = value
                End Set
            End Property
            ''' <summary>Removes the first occurrence of a specific object from the <see cref="System.Collections.IList" />.</summary>
            ''' <param name="value">The <see cref="System.Object" /> to remove from the <see cref="System.Collections.IList" />.</param>
            ''' <exception cref="System.NotSupportedException">The <see cref="System.Collections.IList" /> is read-only.-or- The <see cref="System.Collections.IList" /> has a fixed size.</exception>
            Private Sub Remove_Unsafe(ByVal value As Object) Implements System.Collections.IList.Remove
                Collection.Remove(value)
            End Sub
            ''' <summary>Determines whether the <see cref="System.Collections.IList" /> contains a specific value.</summary>
            ''' <param name="value">The <see cref="System.Object" /> to locate in the <see cref="System.Collections.IList" />.</param>
            ''' <returns>true if the <see cref="System.Object" /> is found in the <see cref="System.Collections.IList" />; otherwise, false.</returns>
            Private Function Contains_Unsafe(ByVal value As Object) As Boolean Implements System.Collections.IList.Contains
                Return Collection.Contains(value)
            End Function
            ''' <summary>Determines the index of a specific item in the <see cref="System.Collections.IList" />.</summary>
            ''' <param name="value">The <see cref="System.Object" /> to locate in the <see cref="System.Collections.IList" />.</param>
            ''' <returns>The index of value if found in the list; otherwise, -1.</returns>
            Private Function IndexOf_Unsafe(ByVal value As Object) As Integer Implements System.Collections.IList.IndexOf
                Return Collection.IndexOf(value)
            End Function
            ''' <summary>Inserts an item to the <see cref="System.Collections.IList" /> at the specified index.</summary>
            ''' <param name="index">The zero-based index at which value should be inserted.</param>
            ''' <param name="value">The <see cref="System.Object" /> to insert into the <see cref="System.Collections.IList" />.</param>
            ''' <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the <see cref="System.Collections.IList" />.</exception>
            ''' <exception cref="System.NotSupportedException">The <see cref="System.Collections.IList" /> is read-only.-or- The <see cref="System.Collections.IList" /> has a fixed size.</exception>
            ''' <exception cref="System.NullReferenceException">value is null reference in the <see cref="System.Collections.IList" />.</exception>
            ''' <exception cref="TypeMismatchException"><paramref name="value"/> is not of type <typeparemref name="TItem"/></exception>
            Private Sub Insert_Unsafe(ByVal index As Integer, ByVal value As Object) Implements System.Collections.IList.Insert
                ThrowType(value)
                Collection.Insert(index, value)
            End Sub
            ''' <summary>Checks if given object can be stored in this collection</summary>
            ''' <param name="obj">Object to be stested</param>
            ''' <exception cref="TypeMismatchException"><paramref name="obj"/> is not of type <typeparemref name="TItem"/></exception>
            Protected Sub ThrowType(ByVal obj As Object)
                If Not TypeOf obj Is TItem Then Throw New TypeMismatchException("obj", obj, GetType(TItem))
            End Sub
            ''' <summary>Adds an item to the <see cref="System.Collections.IList" />.</summary>
            ''' <param name="value">The <see cref="System.Object" /> to add to the <see cref="System.Collections.IList" />.</param>
            ''' <returns>The position into which the new element was inserted.</returns>
            ''' <exception cref="System.NotSupportedException">The <see cref="System.Collections.IList" /> is read-only.-or- The <see cref="System.Collections.IList" /> has a fixed size.</exception>
            ''' <exception cref="TypeMismatchException"><paramref name="value"/> is not of type <typeparemref name="TItem"/></exception>
            Private Function Add_Unsafe(ByVal value As Object) As Integer Implements System.Collections.IList.Add
                ThrowType(value)
                Add(value)
            End Function
#End Region
        End Class

        '    ''' <summary>Provides abstract base class and static methods for wrappint type-unsafe <see cref="ICollection"/> as type-safe <see cref="Generic.ICollection(Of T)"/></summary>
        '    ''' <remarks>Derived classes should derive from <see cref="SpecializedWrapper(Of T)"/> instead</remarks>
        '    Public MustInherit Class SpecializedWrapper : Inherits SpecializedReadOnlyWrapper : Implements IIndexable(Of Object, Integer)
        '        ''' <summary>CTor</summary>
        '        ''' <param name="Collection">Collection to be wrapped</param>
        '        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        '        Friend Sub New(ByVal Collection As ICollection)
        '            MyBase.New(Collection)
        '        End Sub
        '#Region "Interface Implementation"
        '        ''' <summary>When overriden in derived class gets or sets value on specified index (type-unsafe)</summary>
        '        ''' <param name="index">Index to set or obtain value</param>
        '        ''' <returns>value lying on specified <paramref name="index"/></returns>
        '        ''' <value>New value to be stored at specified index</value>
        '        ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
        '        Protected MustOverride Overloads Property UnsafeItem(ByVal index As Integer) As Object Implements GenericT.IIndexable(Of Object, Integer).Item
        '        ''' <summary>Gets value on specified index</summary>
        '        ''' <param name="index">Index to obtain value</param>
        '        ''' <returns>value lying on specified <paramref name="index"/></returns>
        '        ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
        '        Protected NotOverridable Overrides ReadOnly Property UnsafeReadOnlyItem(ByVal index As Integer) As Object
        '            <DebuggerStepThrough()> Get
        '                Return UnsafeItem(index)
        '            End Get
        '        End Property
        '#End Region

#Region "Specific implementations"
        ''' <summary>Provides common base for specialized wrappers implementations</summary>
        ''' <typeparam name="TCollection">Type of collection being wrapped</typeparam>
        ''' <typeparam name="TItem">Type of item in collection</typeparam>
        ''' <remarks>This class is not intended for direct use. Use <see cref="SpecializedWrapper(Of T)"/> instead.</remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public MustInherit Class SpecializedWrapper(Of TCollection As ICollection, TItem)
            Inherits SpecializedWrapper(Of TItem)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped. It is responsibility of derived class to pass only such collections whichs items are of type <typeparamref name="T"/> in CTor. No check is done.</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Protected Sub New(ByVal Collection As TCollection)
                MyBase.New(Collection)
            End Sub
            ''' <summary>Collection being wrapped</summary>
            Protected Shadows ReadOnly Property Collection() As TCollection
                Get
                    Return MyBase.Collection
                End Get
            End Property
            ''' <summary>Converts <see cref="SpecializedWrapper(Of TCollection, TItem)"/> to <typeparamref name="TCollection"/></summary>
            ''' <param name="a">A <see cref="SpecializedWrapper(Of TCollection, TItem)"/></param>
            ''' <returns><paramref name="a"/>.<see cref="SpecializedWrapper(Of TCollection, TItem).Collection">Collection</see></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Shared Widening Operator CType(ByVal a As SpecializedWrapper(Of TCollection, TItem)) As TCollection
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return a.Collection
            End Operator
        End Class
        ''' <summary>Provides common base for specialized read-only wrappers implementations</summary>
        ''' <typeparam name="TCollection">Type of collection being wrapped</typeparam>
        ''' <typeparam name=" TItem">Type of item in collection</typeparam>
        ''' <remarks>This class is not intended for direct use. Use <see cref="SpecializedReadOnlyWrapper(Of T)"/> instead.</remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public MustInherit Class SpecializedReadOnlyWrapper(Of TCollection As ICollection, TItem)
            Inherits SpecializedReadOnlyWrapper(Of TItem)
            ''' <summary>CTor</summary>
            ''' <param name="Collection">Collection to be wrapped. It is responsibility of derived class to pass only such collections whichs items are of type <typeparamref name="T"/> in CTor. No check is done.</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
            Protected Sub New(ByVal Collection As TCollection)
                MyBase.New(Collection)
            End Sub
            ''' <summary>Collection being wrapped</summary>
            Protected Shadows ReadOnly Property Collection() As TCollection
                Get
                    Return MyBase.Collection
                End Get
            End Property
            ''' <summary>Converts <see cref="SpecializedReadOnlyWrapper(Of TCollection, TItem)"/> to <typeparamref name="TCollection"/></summary>
            ''' <param name="a">A <see cref="SpecializedReadOnlyWrapper(Of TCollection, TItem)"/></param>
            ''' <returns><paramref name="a"/>.<see cref="SpecializedWrapper(Of TCollection, TItem).Collection">Collection</see></returns>
            ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
            Public Shared Widening Operator CType(ByVal a As SpecializedReadOnlyWrapper(Of TCollection, TItem)) As TCollection
                If a Is Nothing Then Throw New ArgumentNullException("a")
                Return a.Collection
            End Operator
        End Class
#End Region
    End Class

    ''' <summary>Provides abstract base for implementation of type-unsafe <see cref="ICollection"/> to type-safe <see cref="ICollection(Of T)"/> wrappers</summary>
    ''' <typeparam name="T">Type of item of collection</typeparam>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public MustInherit Class SpecializedWrapper(Of T)
        Inherits SpecializedReadOnlyWrapper(Of T)
        Implements IIndexableCollection(Of T, Integer)
        ''' <summary>CTor</summary>
        ''' <param name="Collection">Collection to be wrapped. It is responsibility of derived class to pass only such collections whichs items are of type <typeparamref name="T"/> in CTor. No check is done.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Protected Sub New(ByVal Collection As ICollection)
            MyBase.New(Collection)
        End Sub

        ''' <summary>When overriden in derived class adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
        ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        Public MustOverride Sub Add(ByVal item As T) Implements ICollection(Of T).Add, IAddable(Of T).Add

        ''' <summary>When overriden in derived class removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
        Public MustOverride Sub Clear() Implements System.Collections.Generic.ICollection(Of T).Clear

        ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
        ''' <returns>true if 
        ''' <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
        ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        ''' <remarks>If <see cref="Collection"/> provides way how to determine if it contains specific item it is efficiend to override this method and call use that way.</remarks>
        Public Overridable Function Contains(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Contains
            If (item Is Nothing) Then
                Dim j As Integer
                For j = 0 To Me.Count - 1
                    If (Me.Item(j) Is Nothing) Then
                        Return True
                    End If
                Next j
                Return False
            End If
            Dim comparer As EqualityComparer(Of T) = EqualityComparer(Of T).Default
            Dim i As Integer
            For i = 0 To Me.Count - 1
                If comparer.Equals(Me.Item(i), item) Then
                    Return True
                End If
            Next i
            Return False
        End Function

        ''' <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        ''' <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        ''' <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is null.</exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex" /> is less than 0.</exception>
        ''' <exception cref="T:System.ArgumentException">
        ''' <paramref name="array" /> is multidimensional.-or-
        ''' <paramref name="arrayIndex" /> is equal to or greater than the length of <paramref name="array" />.-or-
        ''' The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.-or-
        ''' Type <paramref name="T" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
        Public Overrides Sub CopyTo(ByVal array() As T, ByVal arrayIndex As Integer) Implements ICollection(Of T).CopyTo
            MyBase.CopyTo(array, arrayIndex)
        End Sub

        ''' <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.</summary>
        ''' <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Overrides ReadOnly Property Count() As Integer Implements ICollection(Of T).Count
            Get
                Return MyBase.Count
            End Get
        End Property

        ''' <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</summary>
        ''' <returns>This implementation returns always true.</returns>
        Public Overridable ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of T).IsReadOnly
            Get
                Return True
            End Get
        End Property

        ''' <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
        ''' <returns>true if 
        ''' <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if 
        ''' <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        Public MustOverride Function Remove(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Remove

        ''' <summary>When overridne in derived class gets or sets value on specified index</summary>
        ''' <param name="index">Index to set or obtain value</param>
        ''' <returns>value lying on specified <paramref name="index"/></returns>
        ''' <value>New value to be stored at specified index</value>
        ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
        ''' <exception cref="T:System.NotSupportedException">In setter: The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</exception>
        Default Public MustOverride Overloads Property Item(ByVal index As Integer) As T Implements IIndexable(Of T, Integer).Item

        ''' <summary>Gets value on specified index</summary>
        ''' <param name="index">Index to obtain value</param>
        ''' <returns>value lying on specified <paramref name="index"/></returns>
        ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
        Protected NotOverridable Overrides ReadOnly Property ItemRO(ByVal index As Integer) As T
            Get
                Return Item(index)
            End Get
        End Property
    End Class
    ''' <summary>Provides abstract base for implementation of type-unsafe <see cref="ICollection"/> to type-safe <see cref="IReadOnlyIndexable(Of TItem, TIndex)"/> wrappers</summary>
    ''' <typeparam name="T">Type of item of collection</typeparam>
    Public MustInherit Class SpecializedReadOnlyWrapper(Of T)
        Inherits SpecializedWrapper : Implements IReadOnlyIndexableCollection(Of T, Integer)
        ''' <summary>CTor</summary>
        ''' <param name="Collection">Collection to be wrapped. It is responsibility of derived class to pass only such collections whichs items are of type <typeparamref name="T"/> in CTor. No check is done.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        Protected Sub New(ByVal Collection As ICollection)
            MyBase.New(Collection)
        End Sub
        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        ''' <filterpriority>1</filterpriority>
        Public Overridable Shadows Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
            If TypeOf Collection Is IEnumerable(Of T) Then Return DirectCast(Collection, IEnumerable(Of T)).GetEnumerator
            Return New GenericT.Wrapper(Of T)(Collection)
        End Function

        ''' <summary>Gets or sets value on specified index (type-unsafe)</summary>
        ''' <param name="index">Index to set or obtain value</param>
        ''' <returns>value lying on specified <paramref name="index"/></returns>
        ''' <value>New value to be stored at specified index</value>
        ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
        ''' <remarks>Uses the <see cref="Item"/> property</remarks>
        Protected NotOverridable Overrides ReadOnly Property UnsafeReadOnlyItem(ByVal index As Integer) As Object
            <DebuggerStepThrough()> Get
                Return ItemRO(index)
            End Get
        End Property

        ''' <summary>Gets value on specified index</summary>
        ''' <param name="index">Index to obtain value</param>
        ''' <returns>value lying on specified <paramref name="index"/></returns>
        ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
        Protected MustOverride ReadOnly Property ItemRO(ByVal index As Integer) As T
        ''' <summary>Gets value on specified index</summary>
        ''' <param name="index">Index to obtain value</param>
        ''' <returns>value lying on specified <paramref name="index"/></returns>
        ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
        ''' <remarks>This property cannot be overriden, override <see cref="ItemRO"/> instead</remarks>
        Default Public ReadOnly Property Item(ByVal index As Integer) As T Implements GenericT.IReadOnlyIndexable(Of T, Integer).Item
            <DebuggerStepThrough()> Get
                Return ItemRO(index)
            End Get
        End Property

        ''' <summary>Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.</summary>
        ''' <returns>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Overrides ReadOnly Property Count() As Integer Implements IReadOnlyCollection(Of T).Count
            Get
                Return MyBase.Count
            End Get
        End Property
        ''' <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        ''' <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        ''' <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is null.</exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex" /> is less than 0.</exception>
        ''' <exception cref="T:System.ArgumentException">
        ''' <paramref name="array" /> is multidimensional.-or-
        ''' <paramref name="arrayIndex" /> is equal to or greater than the length of <paramref name="array" />.-or-
        ''' The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.-or-
        ''' Type <paramref name="T" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
        Public Overridable Shadows Sub CopyTo(ByVal array() As T, ByVal arrayIndex As Integer) Implements IReadOnlyCollection(Of T).CopyTo
            MyBase.CopyTo(array, arrayIndex)
        End Sub
    End Class
End Namespace
#End If