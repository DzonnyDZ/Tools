#If Config <= Release Then
Namespace Collections.Generic
    ''' <summary>Strongly typed read-only list that provides all applicable methods available in <see cref="List(Of T)"/></summary>
    ''' <typeparam name="T">Type of items in list</typeparam>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(IReadOnlyList(Of String)), LastChange:="12/20/2006")> _
    Public Interface IReadOnlyList(Of T) : Inherits IEnumerable(Of T)
        ''' <summary>Copies a range of elements from the <see cref="IReadOnlyList(Of T)"/> to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
        ''' <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from <see cref="IReadOnlyList(Of T)"/>. The <see cref="System.Array"/> must have zero-based indexing.</param>
        ''' <param name="count">The number of elements to copy.</param>
        ''' <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        ''' <param name="index">The zero-based index in the source <see cref="IReadOnlyList(Of T)"/> at which copying begins.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="array"/> is null.</exception>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0. or <paramref name="arrayIndex"/> is less than 0 or <paramref name="count"/> is less than 0.</exception>
        ''' <exception cref="System.ArgumentException"><paramref name="index"/> is equal to or greater than the <see cref="Count"/> of the source <see cref="IReadOnlyList(Of T)"/> or <paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/> or The number of elements from <paramref name="index"/> to the end of the source <see cref="IReadOnlyList(Of T)"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <see cref="Array"/>.</exception>
        Sub CopyTo(ByVal index As Integer, ByVal array As T(), ByVal arrayIndex As Integer, ByVal count As Integer)
        ''' <summary>Copies the entire <see cref="IReadOnlyList(Of T)"/> to a compatible one-dimensional array, starting at the beginning of the target array.</summary>
        ''' <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from <see cref="IReadOnlyList(Of T)"/>. The <see cref="System.Array"/> must have zero-based indexing.</param>
        ''' <exception cref="System.ArgumentException">The number of elements in the source <see cref="IReadOnlyList(Of T)"/> is greater than the number of elements that the destination array can contain.</exception>
        ''' <exception cref="System.ArgumentNullException"><paramref name="array"/> is null.</exception>
        Sub CopyTo(ByVal array As T())
        ''' <summary>Copies the entire <see cref="IReadOnlyList(Of T)"/> to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
        ''' <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from <see cref="IReadOnlyList(Of T)"/>. The <see cref="System.Array"/> must have zero-based indexing.</param>
        ''' <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        ''' <exception cref="System.ArgumentException"><paramref name="arrayIndex"/> is equal to or greater than the length of array or The number of elements in the source <see cref="IReadOnlyList(Of T)"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination array.</exception>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception>
        ''' <exception cref="System.ArgumentNullException"><paramref name="array"/> is null.</exception>
        Sub CopyTo(ByVal array As T(), ByVal arrayIndex As Integer)
        ''' <summary>Performs the specified action on each element of the <see cref="IReadOnlyList(Of T)"/>.</summary>
        ''' <param name="action">The <see cref="System.Action(Of T)"/> delegate to perform on each element of the <see cref="IReadOnlyList(Of T)"/>.</param>
        ''' <exception cref="ArgumentNullException">action is null.</exception>
        Sub ForEach(ByVal action As Action(Of T))
        ''' <summary>Determines whether an element is in the <see cref="IReadOnlyList(Of T)"/>.</summary>
        ''' <returns>true if item is found in the <see cref="IReadOnlyList(Of T)"/>; otherwise, false.</returns>
        ''' <param name="item">The object to locate in the <see cref="IReadOnlyList(Of T)"/>. The value can be null for reference types.</param>
        Function Contains(ByVal item As T) As Boolean
        ''' <summary>Converts the elements in the current <see cref="IReadOnlyList(Of T)"/> to another type, and returns a list containing the converted elements.</summary>
        ''' <returns>A <see cref="IReadOnlyList(Of T)"/> of the target type containing the converted elements from the current <see cref="IReadOnlyList(Of T)"/>.</returns>
        ''' <param name="converter">A <see cref="System.Converter(Of TInput, TOutput)"/> delegate that converts each element from one type to another type.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="converter"/> is null.</exception>
        Function ConvertAll(Of TOutput)(ByVal converter As Converter(Of T, TOutput)) As List(Of TOutput)
        ''' <summary>Determines whether the <see cref="IReadOnlyList(Of T)"/> contains elements that match the conditions defined by the specified predicate.</summary>
        ''' <returns>true if the <see cref="IReadOnlyList(Of T)"/> contains one or more elements that match the conditions defined by the specified predicate; otherwise, false.</returns>
        ''' <param name="match">The <see cref="System.Predicate(Of T)"/> delegate that defines the conditions of the elements to search for.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Function Exists(ByVal match As Predicate(Of T)) As Boolean
        ''' <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the first occurrence within the entire <see cref="IReadOnlyList(Of T)"/>.</summary>
        ''' <returns>The first element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type T.</returns>
        ''' <param name="match">The <see cref="System.Predicate(Of T)"/> delegate that defines the conditions of the element to search for.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Function Find(ByVal match As Predicate(Of T)) As T
        ''' <summary>Retrieves the all the elements that match the conditions defined by the specified predicate.</summary>
        ''' <returns>A <see cref="IReadOnlyList(Of T)"/> containing all the elements that match the conditions defined by the specified predicate, if found; otherwise, an empty <see cref="IReadOnlyList(Of T)"/>.</returns>
        ''' <param name="match">The <see cref="System.Predicate(Of T)"/> delegate that defines the conditions of the elements to search for.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Function FindAll(ByVal match As Predicate(Of T)) As IReadOnlyList(Of T)
        ''' <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="IReadOnlyList(Of T)"/> that starts at the specified index and contains the specified number of elements.</summary>
        ''' <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match"/>, if found; otherwise, –1.</returns>
        ''' <param name="count">The number of elements in the section to search.</param>
        ''' <param name="startIndex">The zero-based starting index of the search.</param>
        ''' <param name="match">The <see cref="System.Predicate(Of T)"/> delegate that defines the conditions of the element to search for.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for the <see cref="IReadOnlyList(Of T)"/>.-or-<paramref name="count"/> is less than 0.-or-<paramref name="startIndex"/> and <paramref name="count"/> do not specify a valid section in the <see cref="IReadOnlyList(Of T)"/>.</exception>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Function FindIndex(ByVal startIndex As Integer, ByVal count As Integer, ByVal match As Predicate(Of T)) As Integer
        ''' <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="IReadOnlyList(Of T)"/> that extends from the specified index to the last element.</summary>
        ''' <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match"/>, if found; otherwise, –1.</returns>
        ''' <param name="startIndex">The zero-based starting index of the search.</param>
        ''' <param name="match">The <see cref="System.Predicate(Of T)"/> delegate that defines the conditions of the element to search for.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for the <see cref="IReadOnlyList(Of T)"/>.</exception>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Function FindIndex(ByVal startIndex As Integer, ByVal match As Predicate(Of T)) As Integer
        ''' <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the entire <see cref="IReadOnlyList(Of T)"/>.</summary>
        ''' <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match"/>, if found; otherwise, –1.</returns>
        ''' <param name="match">The <see cref="System.Predicate(Of T)"/> delegate that defines the conditions of the element to search for.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Function FindIndex(ByVal match As Predicate(Of T)) As Integer
        ''' <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the last occurrence within the entire <see cref="IReadOnlyList(Of T)"/>.</summary>
        ''' <returns>The last element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type <see cref="T"/>.</returns>
        ''' <param name="match">The <see cref="System.Predicate(Of T)"/> delegate that defines the conditions of the element to search for.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Function FindLast(ByVal match As Predicate(Of T)) As T
        ''' <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="IReadOnlyList(Of T)"/> that contains the specified number of elements and ends at the specified index.</summary>
        ''' <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="match"/>, if found; otherwise, –1.</returns>
        ''' <param name="count">The number of elements in the section to search.</param>
        ''' <param name="startIndex">The zero-based starting index of the backward search.</param>
        ''' <param name="match">The <see cref="System.Predicate(Of T)"/> delegate that defines the conditions of the element to search for.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for the <see cref="IReadOnlyList(Of T)"/>.-or-count is less than 0.-or-startIndex and count do not specify a valid section in the <see cref="IReadOnlyList(Of T)"/>.</exception>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Function FindLastIndex(ByVal startIndex As Integer, ByVal count As Integer, ByVal match As Predicate(Of T)) As Integer
        ''' <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="IReadOnlyList(Of T)"/> that extends from the first element to the specified index.</summary>
        ''' <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
        ''' <param name="startIndex">The zero-based starting index of the backward search.</param>
        ''' <param name="match">The <see cref="System.Predicate(Of T)"/> delegate that defines the conditions of the element to search for.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="startIndex"/> is outside the range of valid indexes for the <see cref="IReadOnlyList(Of T)"/>.</exception>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Function FindLastIndex(ByVal startIndex As Integer, ByVal match As Predicate(Of T)) As Integer
        ''' <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the entire <see cref="IReadOnlyList(Of T)"/>.</summary>
        ''' <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by match, if found; otherwise, –1.</returns>
        ''' <param name="match">The <see cref="System.Predicate(Of T)"/> delegate that defines the conditions of the element to search for.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Function FindLastIndex(ByVal match As Predicate(Of T)) As Integer
        ''' <summary>Creates a shallow copy of a range of elements in the source <see cref="IReadOnlyList(Of T)"/>.</summary>
        ''' <returns>A shallow copy of a range of elements in the source <see cref="IReadOnlyList(Of T)"/>.</returns>
        ''' <param name="count">The number of elements in the range.</param>
        ''' <param name="index">The zero-based <see cref="IReadOnlyList(Of T)"/> index at which the range starts.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">index is less than 0.-or-count is less than 0.</exception>
        ''' <exception cref="System.ArgumentException"><paramref name="index"/> and <paramref name="count"/> do not denote a valid range of elements in the <see cref="IReadOnlyList(Of T)"/>.</exception>
        Function GetRange(ByVal index As Integer, ByVal count As Integer) As IReadOnlyList(Of T)
        ''' <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="IReadOnlyList(Of T)"/>.</summary>
        ''' <returns>The zero-based index of the first occurrence of item within the entire <see cref="IReadOnlyList(Of T)"/>, if found; otherwise, –1.</returns>
        ''' <param name="item">The object to locate in the <see cref="IReadOnlyList(Of T)"/>. The value can be null for reference types.</param>
        Function IndexOf(ByVal item As T) As Integer
        ''' <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the range of elements in the <see cref="IReadOnlyList(Of T)"/> that extends from the specified index to the last element.</summary>
        ''' <returns>The zero-based index of the first occurrence of item within the range of elements in the <see cref="IReadOnlyList(Of T)"/> that extends from <paramref name="index"/> to the last element, if found; otherwise, –1.</returns>
        ''' <param name="item">The object to locate in the <see cref="IReadOnlyList(Of T)"/>. The value can be null for reference types.</param>
        ''' <param name="index">The zero-based starting index of the search.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the <see cref="IReadOnlyList(Of T)"/>.</exception>
        Function IndexOf(ByVal item As T, ByVal index As Integer) As Integer
        ''' <summary>Searches for the specified object and returns the zero-based index of the first occurrence within the range of elements in the <see cref="IReadOnlyList(Of T)"/> that starts at the specified index and contains the specified number of elements.</summary>
        ''' <returns>The zero-based index of the first occurrence of item within the range of elements in the <see cref="IReadOnlyList(Of T)"/> that starts at index and contains count number of elements, if found; otherwise, –1.</returns>
        ''' <param name="count">The number of elements in the section to search.</param>
        ''' <param name="item">The object to locate in the <see cref="IReadOnlyList(Of T)"/>. The value can be null for reference types.</param>
        ''' <param name="index">The zero-based starting index of the search.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the <see cref="IReadOnlyList(Of T)"/>.-or-<paramref name="count"/> is less than 0.-or-<paramref name="index"/> and <paramref name="count"/> do not specify a valid section in the <see cref="IReadOnlyList(Of T)"/>.</exception>
        Function IndexOf(ByVal item As T, ByVal index As Integer, ByVal count As Integer) As Integer
        ''' <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the entire <see cref="IReadOnlyList(Of T)"/>.</summary>
        ''' <returns>The zero-based index of the last occurrence of item within the entire the <see cref="IReadOnlyList(Of T)"/>, if found; otherwise, –1.</returns>
        ''' <param name="item">The object to locate in the <see cref="IReadOnlyList(Of T)"/>. The value can be null for reference types.</param>
        Function LastIndexOf(ByVal item As T) As Integer
        ''' <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the range of elements in the <see cref="IReadOnlyList(Of T)"/> that extends from the first element to the specified index.</summary>
        ''' <returns>The zero-based index of the last occurrence of item within the range of elements in the <see cref="IReadOnlyList(Of T)"/> that extends from the first element to index, if found; otherwise, –1.</returns>
        ''' <param name="item">The object to locate in the <see cref="IReadOnlyList(Of T)"/>. The value can be null for reference types.</param>
        ''' <param name="index">The zero-based starting index of the backward search.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the <see cref="IReadOnlyList(Of T)"/>. </exception>
        Function LastIndexOf(ByVal item As T, ByVal index As Integer) As Integer
        ''' <summary>Searches for the specified object and returns the zero-based index of the last occurrence within the range of elements in the <see cref="IReadOnlyList(Of T)"/> that contains the specified number of elements and ends at the specified index.</summary>
        ''' <returns>The zero-based index of the last occurrence of item within the range of elements in the <see cref="IReadOnlyList(Of T)"/> that contains count number of elements and ends at index, if found; otherwise, –1.</returns>
        ''' <param name="count">The number of elements in the section to search.</param>
        ''' <param name="item">The object to locate in the <see cref="IReadOnlyList(Of T)"/>. The value can be null for reference types.</param>
        ''' <param name="index">The zero-based starting index of the backward search.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the <see cref="IReadOnlyList(Of T)"/>.-or-<paramref name="count"/> is less than 0.-or-<paramref name="index"/> and <paramref name="count"/> do not specify a valid section in the <see cref="IReadOnlyList(Of T)"/>. </exception>
        Function LastIndexOf(ByVal item As T, ByVal index As Integer, ByVal count As Integer) As Integer
        ''' <summary>Copies the elements of the <see cref="IReadOnlyList(Of T)"/> to a new array.</summary>
        ''' <returns>An array containing copies of the elements of the <see cref="IReadOnlyList(Of T)"/>.</returns>
        Function ToArray() As T()
        ''' <summary>Determines whether every element in the <see cref="IReadOnlyList(Of T)"/> matches the conditions defined by the specified predicate.</summary>
        ''' <returns>true if every element in the <see cref="IReadOnlyList(Of T)"/> matches the conditions defined by the specified predicate; otherwise, false. If the list has no elements, the return value is true.</returns>
        ''' <param name="match">The <see cref="System.Predicate(Of T)"/> delegate that defines the conditions to check against the elements.</param>
        ''' <exception cref="System.ArgumentNullException"><paramref name="match"/> is null.</exception>
        Function TrueForAll(ByVal match As System.Predicate(Of T)) As Boolean
        ''' <summary>Gets the number of elements actually contained in the <see cref="IReadOnlyList(Of T)"/>.</summary>
        ''' <returns>The number of elements actually contained in the <see cref="IReadOnlyList(Of T)"/>.</returns>
        ReadOnly Property Count() As Integer
        ''' <summary>Gets or sets the element at the specified index.</summary>
        ''' <returns>The element at the specified index.</returns>
        ''' <param name="index">The zero-based index of the element to get or set.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.-or-<paramref name="index"/> is equal to or greater than <see cref="IReadOnlyList(Of T).Count"/>. </exception>
        Default ReadOnly Property Item(ByVal index As Integer) As T
    End Interface
End Namespace
#End If