#If Config <= Release Then
Namespace Collections.Generic
    ''' <summary>Adapter that adapts <see cref="List(Of TFrom)"/> into <see cref="IReadOnlyList(Of TTo)"/> where TFrom is cublass of TTo</summary>
    ''' <typeparam name="TFrom">Type of items stored in <see cref="List(Of TFrom)"/> being adapted</typeparam>
    ''' <typeparam name="TTo">
    ''' Type of items this <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> should appear to be <see cref="IReadOnlyList(Of TTo)"/> of.
    ''' TFrom must inherit from or implement TTo
    ''' </typeparam>
    ''' <remarks>If you doesn't need type conversion than you can use <seealso cref="ReadOnlyListAdapter(Of T)"/></remarks>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(ReadOnlyListAdapter(Of Object, Object)), LastChange:="12/20/2006")> _
    <DebuggerDisplay("Count = {Count}")> _
    Public Class ReadOnlyListAdapter(Of TFrom As TTo, TTo)
        Implements IReadOnlyList(Of TTo)
        ''' <summary>CTor</summary>
        ''' <param name="AdaptThis">The <see cref="List(Of T)"/> to be adapted.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="AdaptThis"/> is null</exception>
        Public Sub New(ByVal AdaptThis As List(Of TFrom))
            If AdaptThis Is Nothing Then Throw New ArgumentNullException("AdaptThis cannot be null", "AdaptThis")
            _InnerList = AdaptThis
        End Sub
        ''' <summary>Contains value of the <see cref="InnerList"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _InnerList As List(Of TFrom)
        ''' <summary>The <see cref="List(Of TFrom)"/> being adapted</summary>
        Public Overridable ReadOnly Property InnerList() As List(Of TFrom)
            Get
                Return _InnerList
            End Get
        End Property
        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="System.Collections.Generic.IEnumerator(Of TTo)"/> that can be used to iterate through the collection.</returns>
        Public Overridable Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of TTo) Implements System.Collections.Generic.IEnumerable(Of TTo).GetEnumerator
            Return New ReadOnlyListAdapterEnumerator(InnerList.GetEnumerator)
        End Function
        ''' <summary>Returns an enumerator that iterates through a collection.</summary>
        ''' <returns>An <see cref="System.Collections.IEnumerator"/> object that can be used to iterate through the collection.</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

        ''' <summary>Determines whether an element is in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</summary>
        ''' <returns>true if item is found in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>; otherwise, false.</returns>
        ''' <param name="item">The object to locate in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>. The value can be null for reference types.</param>
        Public Overridable Function Contains(ByVal item As TTo) As Boolean Implements IReadOnlyList(Of TTo).Contains
            If TypeOf item Is TFrom Then
                Return InnerList.Contains(item)
            Else : Return False
            End If
        End Function

        ''' <summary>Converts the elements in the current <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> to another type, and returns a list containing the converted elements.</summary>
        ''' <returns>A <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> of the target type containing the converted elements from the current <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</returns>
        ''' <param name="converter">A <see cref="System.Converter(Of TInput, TOutput)"/> delegate that converts each element from one type to another type.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="converter"/> is null.</exception>
        Public Overridable Function ConvertAll(Of TOutput)(ByVal converter As System.Converter(Of TTo, TOutput)) As System.Collections.Generic.List(Of TOutput) Implements IReadOnlyList(Of TTo).ConvertAll
            Return InnerList.ConvertAll(Of TOutput)(AddressOf New ConverterAdaptor(Of TOutput)(converter).Convert)
        End Function

        ''' <summary>Copies a range of elements from the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
        ''' <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>. The <see cref="System.Array"/> must have zero-based indexing.</param>
        ''' <param name="count">The number of elements to copy.</param>
        ''' <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        ''' <param name="index">The zero-based index in the source <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> at which copying begins.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="array"/> is null.</exception>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0. or <paramref name="arrayIndex"/> is less than 0 or <paramref name="count"/> is less than 0.</exception>
        ''' <exception cref="System.ArgumentException"><paramref name="index"/> is equal to or greater than the <see cref="Count"/> of the source <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> or <paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/> or The number of elements from <paramref name="index"/> to the end of the source <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <see cref="Array"/>.</exception>
        Public Overridable Sub CopyTo(ByVal index As Integer, ByVal array() As TTo, ByVal arrayIndex As Integer, ByVal count As Integer) Implements IReadOnlyList(Of TTo).CopyTo
            ToList.CopyTo(index, array, arrayIndex, count)
        End Sub

        ''' <summary>Copies the entire <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> to a compatible one-dimensional array, starting at the beginning of the target array.</summary>
        ''' <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>. The <see cref="System.Array"/> must have zero-based indexing.</param>
        ''' <exception cref="System.ArgumentException">The number of elements in the source <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> is greater than the number of elements that the destination array can contain.</exception>
        ''' <exception cref="System.ArgumentNullException"><paramref name="array"/> is null.</exception>
        Public Overridable Sub CopyTo(ByVal array() As TTo) Implements IReadOnlyList(Of TTo).CopyTo
            ToList.CopyTo(array)
        End Sub

        ''' <summary>Copies the entire <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
        ''' <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>. The <see cref="System.Array"/> must have zero-based indexing.</param>
        ''' <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        ''' <exception cref="System.ArgumentException"><paramref name="arrayIndex"/> is equal to or greater than the length of array or The number of elements in the source <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination array.</exception>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception>
        ''' <exception cref="System.ArgumentNullException"><paramref name="array"/> is null.</exception>
        Public Overridable Sub CopyTo(ByVal array() As TTo, ByVal arrayIndex As Integer) Implements IReadOnlyList(Of TTo).CopyTo
            ToList.CopyTo(array, arrayIndex)
        End Sub

        ''' <summary>Creates new instance of <see cref="List(Of TTo)"/> tha contains all members present in current instance</summary>
        ''' <returns>New <see cref="List(Of TTo)"/> initialized with members of <see cref="InnerList"/></returns>
        Public Overridable Function ToList() As List(Of TTo)
            Dim ret As New List(Of TTo)(InnerList.Count)
            For Each From As TFrom In InnerList
                ret.Add(From)
            Next From
            Return ret
        End Function

        ''' <summary>Gets the number of elements actually contained in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</summary>
        ''' <returns>The number of elements actually contained in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</returns>
        Public Overridable ReadOnly Property Count() As Integer Implements IReadOnlyList(Of TTo).Count
            Get
                Return InnerList.Count
            End Get
        End Property

        ''' <summary>Determines whether the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> contains elements that match the conditions defined by the specified predicate.</summary>
        ''' <returns>true if the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> contains one or more elements that match the conditions defined by the specified predicate; otherwise, false.</returns>
        ''' <param name="match">The <see cref="System.Predicate(Of TTo)"/> delegate that defines the conditions of the elements to search for.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Public Overridable Function Exists(ByVal match As System.Predicate(Of TTo)) As Boolean Implements IReadOnlyList(Of TTo).Exists
            Return InnerList.Exists(AddressOf New PredicateAdaptor(match).Predicate)
        End Function

        ''' <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the first occurrence within the entire <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</summary>
        ''' <returns>The first element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type T.</returns>
        ''' <param name="match">The <see cref="System.Predicate(Of TTo)"/> delegate that defines the conditions of the element to search for.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Public Overridable Function Find(ByVal match As System.Predicate(Of TTo)) As TTo Implements IReadOnlyList(Of TTo).Find
            Return InnerList.Find(AddressOf New PredicateAdaptor(match).Predicate)
        End Function

        ''' <summary>Retrieves the all the elements that match the conditions defined by the specified predicate.</summary>
        ''' <returns>A <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> containing all the elements that match the conditions defined by the specified predicate, if found; otherwise, an empty <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</returns>
        ''' <param name="match">The <see cref="System.Predicate(Of TTo)"/> delegate that defines the conditions of the elements to search for.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Public Overridable Function FindAll(ByVal match As System.Predicate(Of TTo)) As IReadOnlyList(Of TTo) Implements IReadOnlyList(Of TTo).FindAll
            Return InnerList.FindAll(AddressOf New PredicateAdaptor(match).Predicate)
        End Function

        ''' <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> that starts at the specified index and contains the specified number of elements.</summary>
        ''' <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match"/>, if found; otherwise, –1.</returns>
        ''' <param name="count">The number of elements in the section to search.</param>
        ''' <param name="startIndex">The zero-based starting index of the search.</param>
        ''' <param name="match">The <see cref="System.Predicate(Of TTo)"/> delegate that defines the conditions of the element to search for.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.-or-<paramref name="count"/> is less than 0.-or-<paramref name="startIndex"/> and <paramref name="count"/> do not specify a valid section in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</exception>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Public Overridable Function FindIndex(ByVal startIndex As Integer, ByVal count As Integer, ByVal match As System.Predicate(Of TTo)) As Integer Implements IReadOnlyList(Of TTo).FindIndex
            Return InnerList.FindIndex(startIndex, count, AddressOf New PredicateAdaptor(match).Predicate)
        End Function

        ''' <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> that extends from the specified index to the last element.</summary>
        ''' <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match"/>, if found; otherwise, –1.</returns>
        ''' <param name="startIndex">The zero-based starting index of the search.</param>
        ''' <param name="match">The <see cref="System.Predicate(Of TTo)"/> delegate that defines the conditions of the element to search for.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</exception>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Public Overridable Function FindIndex(ByVal startIndex As Integer, ByVal match As System.Predicate(Of TTo)) As Integer Implements IReadOnlyList(Of TTo).FindIndex
            Return InnerList.FindIndex(startIndex, AddressOf New PredicateAdaptor(match).Predicate)
        End Function

        ''' <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the entire <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</summary>
        ''' <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match"/>, if found; otherwise, –1.</returns>
        ''' <param name="match">The <see cref="System.Predicate(Of TTo)"/> delegate that defines the conditions of the element to search for.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Public Overridable Function FindIndex(ByVal match As System.Predicate(Of TTo)) As Integer Implements IReadOnlyList(Of TTo).FindIndex
            Return InnerList.FindIndex(AddressOf New PredicateAdaptor(match).Predicate)
        End Function

        ''' <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the last occurrence within the entire <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</summary>
        ''' <returns>The last element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type <see cref="TTo"/>.</returns>
        ''' <param name="match">The <see cref="System.Predicate(Of TTo)"/> delegate that defines the conditions of the element to search for.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Public Overridable Function FindLast(ByVal match As System.Predicate(Of TTo)) As TTo Implements IReadOnlyList(Of TTo).FindLast
            Return InnerList.FindLast(AddressOf New PredicateAdaptor(match).Predicate)
        End Function

        ''' <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> that contains the specified number of elements and ends at the specified index.</summary>
        ''' <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="match"/>, if found; otherwise, –1.</returns>
        ''' <param name="count">The number of elements in the section to search.</param>
        ''' <param name="startIndex">The zero-based starting index of the backward search.</param>
        ''' <param name="match">The <see cref="System.Predicate(Of TTo)"/> delegate that defines the conditions of the element to search for.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.-or-count is less than 0.-or-startIndex and count do not specify a valid section in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</exception>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Public Overridable Function FindLastIndex(ByVal startIndex As Integer, ByVal count As Integer, ByVal match As System.Predicate(Of TTo)) As Integer Implements IReadOnlyList(Of TTo).FindLastIndex
            Return InnerList.FindIndex(startIndex, count, AddressOf New PredicateAdaptor(match).Predicate)
        End Function

        ''' <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> that extends from the first element to the specified index.</summary>
        ''' <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
        ''' <param name="startIndex">The zero-based starting index of the backward search.</param>
        ''' <param name="match">The <see cref="System.Predicate(Of TTo)"/> delegate that defines the conditions of the element to search for.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</exception>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Public Overridable Function FindLastIndex(ByVal startIndex As Integer, ByVal match As System.Predicate(Of TTo)) As Integer Implements IReadOnlyList(Of TTo).FindLastIndex
            Return InnerList.FindLastIndex(startIndex, AddressOf New PredicateAdaptor(match).Predicate)
        End Function

        ''' <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the entire <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</summary>
        ''' <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
        ''' <param name="match">The <see cref="System.Predicate(Of TTo)"/> delegate that defines the conditions of the element to search for.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Public Overridable Function FindLastIndex(ByVal match As System.Predicate(Of TTo)) As Integer Implements IReadOnlyList(Of TTo).FindLastIndex
            Return InnerList.FindLastIndex(AddressOf New PredicateAdaptor(match).Predicate)
        End Function

        ''' <summary>Performs the specified action on each element of the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</summary>
        ''' <param name="action">The <see cref="System.Action(Of TTo)"/> delegate to perform on each element of the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</param>
        ''' <exception cref="ArgumentNullException">action is null.</exception>
        Public Overridable Sub ForEach(ByVal action As System.Action(Of TTo)) Implements IReadOnlyList(Of TTo).ForEach
            InnerList.ForEach(AddressOf New ActionAdaptor(action).Do)
        End Sub

        ''' <summary>Creates a shallow copy of a range of elements in the source <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</summary>
        ''' <returns>A shallow copy of a range of elements in the source <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</returns>
        ''' <param name="count">The number of elements in the range.</param>
        ''' <param name="index">The zero-based <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> index at which the range starts.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">index is less than 0.-or-count is less than 0.</exception>
        ''' <exception cref="System.ArgumentException"><paramref name="index"/> and <paramref name="count"/> do not denote a valid range of elements in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</exception>
        Public Overridable Function GetRange(ByVal index As Integer, ByVal count As Integer) As IReadOnlyList(Of TTo) Implements IReadOnlyList(Of TTo).GetRange
            Return InnerList.GetRange(index, count)
        End Function

        ''' <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</summary>
        ''' <returns>The zero-based index of the first occurrence of item within the entire <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>, if found; otherwise, –1.</returns>
        ''' <param name="item">The object to locate in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>. The value can be null for reference types.</param>
        Public Overridable Function IndexOf(ByVal item As TTo) As Integer Implements IReadOnlyList(Of TTo).IndexOf
            If TypeOf item Is TFrom Then _
                Return InnerList.IndexOf(item) _
            Else Return -1
        End Function

        ''' <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the range of elements in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> that extends from the specified index to the last element.</summary>
        ''' <returns>The zero-based index of the first occurrence of item within the range of elements in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> that extends from <paramref name="index"/> to the last element, if found; otherwise, –1.</returns>
        ''' <param name="item">The object to locate in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>. The value can be null for reference types.</param>
        ''' <param name="index">The zero-based starting index of the search.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</exception>
        Public Overridable Function IndexOf(ByVal item As TTo, ByVal index As Integer) As Integer Implements IReadOnlyList(Of TTo).IndexOf
            If TypeOf item Is TFrom Then _
                Return InnerList.IndexOf(item, index) _
            Else Return -1
        End Function

        ''' <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the range of elements in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> that starts at the specified index and contains the specified number of elements.</summary>
        ''' <returns>The zero-based index of the first occurrence of item within the range of elements in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> that starts at index and contains count number of elements, if found; otherwise, –1.</returns>
        ''' <param name="count">The number of elements in the section to search.</param>
        ''' <param name="item">The object to locate in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>. The value can be null for reference types.</param>
        ''' <param name="index">The zero-based starting index of the search.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.-or-<paramref name="count"/> is less than 0.-or-<paramref name="index"/> and <paramref name="count"/> do not specify a valid section in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</exception>
        Public Overridable Function IndexOf(ByVal item As TTo, ByVal index As Integer, ByVal count As Integer) As Integer Implements IReadOnlyList(Of TTo).IndexOf
            If TypeOf item Is TFrom Then _
                Return InnerList.IndexOf(item, index, count) _
            Else Return -1
        End Function

        ''' <summary>Gets or sets the element at the specified index.</summary>
        ''' <returns>The element at the specified index.</returns>
        ''' <param name="index">The zero-based index of the element to get or set.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.-or-<paramref name="index"/> is equal to or greater than <see cref="ReadOnlyListAdapter(Of TFrom, TTo).Count"/>. </exception>
        Default Public Overridable ReadOnly Property Item(ByVal index As Integer) As TTo Implements IReadOnlyList(Of TTo).Item
            Get
                Return InnerList(index)
            End Get
        End Property

        ''' <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the entire <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</summary>
        ''' <returns>The zero-based index of the last occurrence of item within the entire the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>, if found; otherwise, –1.</returns>
        ''' <param name="item">The object to locate in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>. The value can be null for reference types.</param>
        Public Overridable Function LastIndexOf(ByVal item As TTo) As Integer Implements IReadOnlyList(Of TTo).LastIndexOf
            If TypeOf item Is TFrom Then _
                Return InnerList.LastIndexOf(item) _
            Else Return -1
        End Function

        ''' <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the range of elements in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> that extends from the first element to the specified index.</summary>
        ''' <returns>The zero-based index of the last occurrence of item within the range of elements in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> that extends from the first element to index, if found; otherwise, –1.</returns>
        ''' <param name="item">The object to locate in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>. The value can be null for reference types.</param>
        ''' <param name="index">The zero-based starting index of the backward search.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>. </exception>
        Public Overridable Function LastIndexOf(ByVal item As TTo, ByVal index As Integer) As Integer Implements IReadOnlyList(Of TTo).LastIndexOf
            If TypeOf item Is TFrom Then _
                Return InnerList.LastIndexOf(item, index) _
            Else Return -1
        End Function

        ''' <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the range of elements in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> that contains the specified number of elements and ends at the specified index.</summary>
        ''' <returns>The zero-based index of the last occurrence of item within the range of elements in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> that contains count number of elements and ends at index, if found; otherwise, –1.</returns>
        ''' <param name="count">The number of elements in the section to search.</param>
        ''' <param name="item">The object to locate in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>. The value can be null for reference types.</param>
        ''' <param name="index">The zero-based starting index of the backward search.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.-or-<paramref name="count"/> is less than 0.-or-<paramref name="index"/> and <paramref name="count"/> do not specify a valid section in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>. </exception>
        Public Overridable Function LastIndexOf(ByVal item As TTo, ByVal index As Integer, ByVal count As Integer) As Integer Implements IReadOnlyList(Of TTo).LastIndexOf
            If TypeOf item Is TFrom Then _
                Return InnerList.LastIndexOf(item, index, count) _
            Else Return -1
        End Function

        ''' <summary>Copies the elements of the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> to a new array.</summary>
        ''' <returns>An array containing copies of the elements of the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/>.</returns>
        Public Overridable Function ToArray() As TTo() Implements IReadOnlyList(Of TTo).ToArray
            Return ToList.ToArray
        End Function

        ''' <summary>Determines whether every element in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> matches the conditions defined by the specified predicate.</summary>
        ''' <returns>true if every element in the <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> matches the conditions defined by the specified predicate; otherwise, false. If the list has no elements, the return value is true.</returns>
        ''' <param name="match">The <see cref="System.Predicate(Of TTo)"/> delegate that defines the conditions to check against the elements.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Public Overridable Function TrueForAll(ByVal match As System.Predicate(Of TTo)) As Boolean Implements IReadOnlyList(Of TTo).TrueForAll
            Return InnerList.TrueForAll(AddressOf New PredicateAdaptor(match).Predicate)
        End Function

        ''' <summary>Wraps instance of <see cref="List(Of TFrom)"/> with instance of <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/></summary>
        ''' <param name="a">Instance to be wrapped</param>
        ''' <returns>New instance of <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/> initialized with <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As List(Of TFrom)) As ReadOnlyListAdapter(Of TFrom, TTo)
            Return New ReadOnlyListAdapter(Of TFrom, TTo)(a)
        End Operator

        ''' <summary>String representation of current instance</summary>
        Public Overrides Function ToString() As String
            Return "{" & Me.GetType.FullName & "} Count = " & Me.Count
        End Function

#Region "Nested classes"
        ''' <summary>Supports simple enumeration over <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/></summary>
        Protected Class ReadOnlyListAdapterEnumerator : Implements IEnumerator(Of TTo)
            ''' <summary>Contains value of the <see cref="InnerEnumerator"/> property</summary>
            Private _InnerEnumerator As IEnumerator(Of TFrom)
            ''' <summary>CTor</summary>
            ''' <param name="innerEnumerator">The enumerator that enumerates through <see cref="ReadOnlyListAdapter(Of TFrom, TTo).InnerList"/></param>
            Public Sub New(ByVal InnerEnumerator As IEnumerator(Of TFrom))
                _InnerEnumerator = InnerEnumerator
            End Sub
            ''' <summary>The enumerator that enumerates through <see cref="ReadOnlyListAdapter(Of TFrom, TTo).InnerList"/></summary>
            Public ReadOnly Property InnerEnumerator() As IEnumerator(Of TFrom)
                Get
                    Return _InnerEnumerator
                End Get
            End Property
            ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
            ''' <returns>The element in the collection at the current position of the enumerator</returns>
            Public ReadOnly Property Current() As TTo Implements System.Collections.Generic.IEnumerator(Of TTo).Current
                Get
                    Return InnerEnumerator.Current
                End Get
            End Property
            ''' <summary>Gets the current element in the collection.</summary>
            ''' <returns>The current element in the collection.</returns>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Public ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
                Get
                    Return Current
                End Get
            End Property

            ''' <summary>Advances the enumerator to the next element of the collection.</summary>
            ''' <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
            ''' <exception cref="System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
            Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
                Return InnerEnumerator.MoveNext
            End Function
            ''' <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
            ''' <exception cref="System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
            Public Sub Reset() Implements System.Collections.IEnumerator.Reset
                InnerEnumerator.Reset()
            End Sub
#Region " IDisposable Support "
            ''' <summary>To detect redundant calls</summary>
            Private disposedValue As Boolean = False

            ''' <summary>IDisposable</summary>
            Protected Sub Dispose(ByVal disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                        ' TODO_: free unmanaged resources when explicitly called
                    End If

                    ' TODO_: free shared unmanaged resources
                    InnerEnumerator.Dispose()
                End If
                Me.disposedValue = True
            End Sub
            ''' <summary>This code added by Visual Basic to correctly implement the disposable pattern.</summary>
            Public Sub Dispose() Implements IDisposable.Dispose
                ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
                Dispose(True)
                GC.SuppressFinalize(Me)
            End Sub
#End Region
        End Class

        ''' <summary>Wraps converter from <see cref="TTo"/> to TOut to work as converter from <see cref="TFrom"/> to TOut</summary>
        ''' <typeparam name="TOut">Type of output value of this <see cref="ConverterAdaptor(Of TOut)"/></typeparam>
        Protected Class ConverterAdaptor(Of TOut)
            ''' <summary>Contains value of the <see cref="InnerConverter"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _InnerConverter As Converter(Of TTo, TOut)
            ''' <summary>CTor</summary>
            ''' <param name="InnerConverter">The <see cref="Converter(Of TTo, TOut)"/> to wrap</param>
            Public Sub New(ByVal InnerConverter As Converter(Of TTo, TOut))
                _InnerConverter = InnerConverter
            End Sub
            ''' <summary>The <see cref="Converter(Of TTo, TOut)"/> to wrap</summary>
            Public ReadOnly Property InnerConverter() As Converter(Of TTo, TOut)
                Get
                    Return _InnerConverter
                End Get
            End Property
            ''' <summary>Invokes <see cref="InnerConverter"/></summary>
            ''' <param name="input">Value to be converted</param>
            ''' <returns>The <see cref="TOut"/> that represents the converted <see cref="TFrom"/>.</returns>
            Public Function Convert(ByVal input As TFrom) As TOut
                Return InnerConverter.Invoke(input)
            End Function
        End Class

        ''' <summary>Wraps <see cref="Predicate"/> of <see cref="TTo"/> so it looks like <see cref="Predicate"/> of <see cref="TFrom"/></summary>
        Protected Class PredicateAdaptor
            ''' <summary>Contains value of the <see cref="InnerPredicate"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _InnerPredicate As Predicate(Of TTo)
            ''' <summary>CTor</summary>
            ''' <param name="InnerPredicate"><see cref="Predicate(Of TTo)"/> to wrap</param>
            Public Sub New(ByVal InnerPredicate As Predicate(Of TTo))
                _InnerPredicate = InnerPredicate
            End Sub
            ''' <summary><see cref="Predicate(Of TTo)"/> being wrapped</summary>
            Public ReadOnly Property InnerPredicate() As Predicate(Of TTo)
                Get
                    Return _InnerPredicate
                End Get
            End Property
            ''' <summary>Invokes <see cref="InnerPredicate"/></summary>
            ''' <param name="obj">Object to be tested</param>
            ''' <returns>true if obj meets the criteria defined within the <see cref="InnerPredicate"/>; otherwise, false.</returns>
            Public Function Predicate(ByVal obj As TFrom) As Boolean
                Return InnerPredicate.Invoke(obj)
            End Function
        End Class

        ''' <summary>Wraps <see cref="Action"/> of <see cref="TTo"/> so it looks like <see cref="Action"/> of <see cref="TFrom"/></summary>
        Protected Class ActionAdaptor
            ''' <summary>Contains value of the <see cref="InnerAction"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _InnerAction As Action(Of TTo)
            ''' <summary>CTor</summary>
            ''' <param name="InnerPredicate"><see cref="Action(Of TTo)"/> to wrap</param>
            Public Sub New(ByVal InnerPredicate As Action(Of TTo))
                _InnerAction = InnerPredicate
            End Sub
            ''' <summary><see cref="Predicate(Of TTo)"/> being wrapped</summary>
            Public ReadOnly Property InnerAction() As Action(Of TTo)
                Get
                    Return _InnerAction
                End Get
            End Property
            ''' <summary>Invokes <see cref="InnerAction"/></summary>
            ''' <param name="obj">Object to <see cref="InnerAction"/> be performed on</param>
            Public Sub [Do](ByVal obj As TFrom)
                InnerAction.Invoke(obj)
            End Sub
        End Class
#End Region
    End Class

    ''' <summary>Adapter that adapts <see cref="List(Of T)"/> into <see cref="IReadOnlyList(Of T)"/> in order to prevent changes of adapted list</summary>
    ''' <typeparam name="T">Type of items of list</typeparam>
    ''' <remarks>If you need convert list of values of inherited type to list of values of parent type use <see cref="ReadOnlyListAdapter(Of TFrom, TTo)"/></remarks>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(ReadOnlyListAdapter(Of Object, Object)), LastChange:="12/20/2006")> _
    <DebuggerDisplay("Count = {Count}")> _
    Public Class ReadOnlyListAdapter(Of T)
        Inherits ReadOnlyListAdapter(Of T, T)
        ''' <summary>CTor</summary>
        ''' <param name="AdaptThis"><see cref="List(Of T)"/> to be adapted</param>
        Public Sub New(ByVal AdaptThis As List(Of T))
            MyBase.New(AdaptThis)
        End Sub
    End Class
End Namespace
#End If