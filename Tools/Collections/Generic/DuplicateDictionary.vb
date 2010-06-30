Imports System.Collections.Generic
Imports System.Text
Imports System.Collections
Imports System.Linq

Namespace CollectionsT.GenericT
    ''' <summary>Ordered dictionary which allws duplicate entries</summary>
    ''' <remarks>This dictionary allows duplicate keys and is kept ordered.</remarks>
    ''' <typeparam name="TKey">Type of dictionary key</typeparam>
    ''' <typeparam name="TValue">Type of dictionary value</typeparam>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class DuplicateDictionary(Of TKey, TValue)
        Implements IDictionary(Of TKey, TValue)
        Implements ICloneable
        ''' <summary>Internal list to store dictionary data in</summary>
        Private list As List(Of KeyValuePair(Of TKey, TValue))
        Private m_comparison As Comparison(Of TKey)
        ''' <summary>Comparison used for comparing keys in dictionary</summary>
        Public ReadOnly Property Comparison() As Comparison(Of TKey)
            Get
                Return m_comparison
            End Get
        End Property
        ''' <summary>Compares two <see cref="KeyValuePair(Of TKey, TValue)"/> values by their <see cref="KeyValuePair(Of TKey, TValue).Key">Keys</see></summary>
        ''' <param name="x">A <see cref="KeyValuePair(Of TKey, TValue)"/></param>
        ''' <param name="y">A <see cref="KeyValuePair(Of TKey, TValue)"/></param>
        ''' <returns>Value Condition Less than 0 <paramref name="x"/>.<see cref="KeyValuePair(Of TKey, TValue).Key">Key</see> is less than <paramref name="y"/>.<see cref="KeyValuePair(Of TKey, TValue).Key">Key</see>.  0 <paramref name="x"/>.<see cref="KeyValuePair(Of TKey, TValue).Key">Key</see> equals <paramref name="y"/>.<see cref="KeyValuePair(Of TKey, TValue).Key">Key</see>. Greater than 0 <paramref name="x"/>.<see cref="KeyValuePair(Of TKey, TValue).Key">Key</see> is greater than <paramref name="y"/>.<see cref="KeyValuePair(Of TKey, TValue).Key">Key</see>.</returns>
        ''' <seealso cref="Comparison"/>
        Private Function PairComparison(ByVal x As KeyValuePair(Of TKey, TValue), ByVal y As KeyValuePair(Of TKey, TValue)) As Integer
            Return Comparison(x.Key, y.Key)
        End Function

#Region "CTors"
        ''' <summary>CTor - creates a new instance of <see cref="DuplicateDictionary"/> class</summary>
        ''' <param name="dictionary">Contains data to populate dictionary with</param>
        ''' <param name="comparison">Comparison used for comparing keys</param>
        ''' <exception cref="ArgumentNullException"><paramref name="dictionary"/> or <paramref name="comparison"/> is null</exception>
        Public Sub New(ByVal dictionary As IEnumerable(Of KeyValuePair(Of TKey, TValue)), ByVal comparison As Comparison(Of TKey))
            If dictionary Is Nothing Then
                Throw New ArgumentNullException("dictionary")
            End If
            If comparison Is Nothing Then
                Throw New ArgumentNullException("comparison")
            End If
            Me.m_comparison = comparison
            list = New List(Of KeyValuePair(Of TKey, TValue))(dictionary)
            list.Sort(AddressOf PairComparison)
        End Sub
        ''' <summary>CTor - creates a new instance of <see cref="DuplicateDictionary"/> class</summary>
        ''' <param name="dictionary">Contains data to populate dictionary with</param>
        ''' <param name="comparer">Comparer used for comparing keys</param>
        ''' <exception cref="ArgumentNullException"><paramref name="dictionary"/> or <paramref name="comparer"/> is null</exception>
        Public Sub New(ByVal dictionary As IEnumerable(Of KeyValuePair(Of TKey, TValue)), ByVal comparer As IComparer(Of TKey))
            If dictionary Is Nothing Then
                Throw New ArgumentNullException("dictionary")
            End If
            If comparer Is Nothing Then
                Throw New ArgumentNullException("comparer")
            End If
            Me.m_comparison = AddressOf comparer.Compare
            list = New List(Of KeyValuePair(Of TKey, TValue))(dictionary)
            list.Sort(AddressOf PairComparison)
        End Sub
        ''' <summary>Copy CTor - creates a copy of given instance of <see cref="DuplicateDictionary(Of TKey, TValue)"/></summary>
        ''' <param name="dictionary">A dictionary to create clone of</param>
        ''' <exception cref="ArgumentNullException"><paramref name="dictionary"/> is null</exception>
        Public Sub New(ByVal dictionary As DuplicateDictionary(Of TKey, TValue))
            If dictionary Is Nothing Then
                Throw New ArgumentNullException("dictionary")
            End If
            list = New List(Of KeyValuePair(Of TKey, TValue))(dictionary.Count)
            Me.m_comparison = dictionary.Comparison
            For Each item As KeyValuePair(Of TKey, TValue) In dictionary.list
                list.Add(item)
            Next
        End Sub
#End Region

#Region "Indexing"
        ''' <summary>Finds first index of key in dictionary</summary>
        ''' <param name="key">A key to find index of</param>
        ''' <returns>Index of first occurence of <paramref name="key"/> in the dictionary. -1 if <paramref name="key"/> is not present in the dictionary.</returns>
        Public Function FirstIndexOf(ByVal key As TKey) As Integer
            If list.Count = 0 Then
                Return -1
            End If
            Dim minIndex As Integer = 0
            Dim maxIndex As Integer = list.Count - 1
            Do
                Dim index As Integer = (maxIndex + minIndex) \ 2
                Dim result As Integer = Comparison(list(index).Key, key)
                If result > 0 Then
                    'list[index] > key - need to search in lower part
                    maxIndex = index - 1
                ElseIf result < 0 Then
                    'list[index] < key - need to search in upper part
                    minIndex = index + 1
                Else
                    While index - 1 > 0 AndAlso Comparison(list(index - 1).Key, key) = 0
                        index -= 1
                    End While
                    Return index
                End If
            Loop While maxIndex >= minIndex
            Return -1
        End Function

        ''' <summary>Finds last index of key in dictionary</summary>
        ''' <param name="key">A key to find index of</param>
        ''' <returns>Index of last occurence of <paramref name="key"/> in the dictionary. -1 if <paramref name="key"/> is not present in the dictionary.</returns>        
        ''' <remarks><see cref="DuplicateDictionary"/> guarantees that there are only items with key <paramref name="key"/> between indexes <see cref="FirstIndexOf"/> and <see cref="LastIndexOf"/>.</remarks>
        Public Function LastIndexOf(ByVal key As TKey) As Integer
            Return LastIndexOf(key, FirstIndexOf(key))
        End Function

        ''' <summary>Finds last index of key in dictionary (after given first index of it)</summary>
        ''' <param name="key">A key to find index of</param>
        ''' <param name="firstIndex">Index where the <paramref name="key"/> occurs for the first time</param>
        ''' <returns>Index of last occurence of <paramref name="key"/> after <paramref name="firstIndex"/>. <paramref name="firstIndex"/> when <paramref name="firstIndex"/> less then zero or there are no more occurences of <paramref name="key"/> after <paramref name="firstIndex"/>.</returns>
        Private Function LastIndexOf(ByVal key As TKey, ByVal firstIndex As Integer) As Integer
            If firstIndex < 0 Then
                Return firstIndex
            End If
            Dim index As Integer = firstIndex
            While index + 1 < list.Count AndAlso Comparison(list(index + 1).Key, key) = 0
                index += 1
            End While
            Return index
        End Function

        ''' <summary>Gets value of first occurence of given key</summary>
        ''' <param name="key">A key to get value of</param>
        ''' <returns>Value associated with first occurence of <paramref name="key"/></returns>
        ''' <exception cref="KeyNotFoundException"><paramref name="key"/> is not present in this dictionary</exception>
        Public Function GetFirst(ByVal key As TKey) As TValue
            Dim index As Integer = FirstIndexOf(key)
            If index < 0 Then
                Throw New KeyNotFoundException(String.Format("Key '{0}' not present in dictionary", key))
            End If
            Return Me(index).Value
        End Function

        ''' <summary>Gets all the values associated with all occurences of given key</summary>
        ''' <param name="key">A key to get values of</param>
        ''' <returns>Array containing all the value associated with <paramref name="key"/>. An empty array if <paramref name="key"/> is not contained in the dictionary.</returns>
        Public Function GetAll(ByVal key As TKey) As TValue()
            Dim firstIndex As Integer = FirstIndexOf(key)
            If firstIndex < 0 Then
                Return New TValue() {}
            End If
            Dim lastIndex As Integer = LastIndexOf(key, firstIndex)
            Dim ret As TValue() = New TValue(lastIndex - firstIndex) {}
            For i As Integer = 0 To ret.Length - 1
                ret(i) = Me(firstIndex + i).Value
            Next
            Return ret
        End Function

        ''' <summary>Gets value from this dictionary at given index</summary>
        ''' <param name="index">Index to get value at</param>
        ''' <returns>Key and value present at given <paramref name="index"/> of this dictionary</returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.  -or- <paramref name="index"/> is equal to or greater than <see cref="Count"/>.</exception>
        Default Public ReadOnly Property Item(ByVal index As Integer) As KeyValuePair(Of TKey, TValue)
            Get
                Return list(index)
            End Get
        End Property
#End Region

#Region "IDictionary Members"

        ''' <summary>Add an item to dictionary</summary>
        ''' <param name="key">Key of item to be added</param>
        ''' <param name="value">Value of item to be added</param>
        ''' <remarks>This method is inefficient when called repeatedly as it requires re-sorting the dictionary</remarks>
        Private Sub IDictionary_Add(ByVal key As TKey, ByVal value As TValue) Implements IDictionary(Of TKey, TValue).Add
            list.Add(New KeyValuePair(Of TKey, TValue)(key, value))
            list.Sort(AddressOf PairComparison)
        End Sub

        ''' <summary>Gets value indicating if given key is present in the dictionary</summary>
        ''' <param name="key">A key to be found in dictionary</param>
        ''' <returns>True if <paramref name="key"/> is present in dictionary, false if it is not</returns>
        Public Function ContainsKey(ByVal key As TKey) As Boolean Implements IDictionary(Of TKey, TValue).ContainsKey
            Return FirstIndexOf(key) >= 0
        End Function

        ''' <summary>Gets a collection containing all the keys of this dictionary (duplicate keys are contained repeatedly)</summary>
        ''' <returns>A read-only collection of keys</returns>
        Public ReadOnly Property Keys() As ICollection(Of TKey) Implements IDictionary(Of TKey, TValue).Keys
            Get
                Return New KeyCollection(Me)
            End Get
        End Property

        ''' <summary>Removes first occurence of given key from dictionary</summary>
        ''' <param name="key">Key of item to remove</param>
        ''' <returns>True when item with given <paramref name="key"/> was originally present in the dictionary and was removed. False when <paramref name="key"/> was not in the dictionary and thus no change has occured.</returns>
        Public Function Remove(ByVal key As TKey) As Boolean Implements IDictionary(Of TKey, TValue).Remove
            Dim index As Integer = FirstIndexOf(key)
            If index < 0 Then
                Return False
            End If
            list.RemoveAt(index)
            Return True
        End Function

        ''' <summary>Attempts to get value identified by key from the dictionary</summary>
        ''' <param name="key">A key to get value of</param>
        ''' <param name="value">When this methods returns true is assigned value of item with key <paramref name="key"/></param>
        ''' <returns>True when <paramref name="key"/> was found in the dictionary and asssociated value was assigned to <paramref name="value"/>; false otherwise.</returns>
        Public Function TryGetValue(ByVal key As TKey, ByRef value As TValue) As Boolean Implements IDictionary(Of TKey, TValue).TryGetValue
            Dim index As Integer = FirstIndexOf(key)
            If index < 0 Then
                value = Nothing
                Return False
            End If
            value = Me(index).Value
            Return True
        End Function

        ''' <summary>Gets a collection containing all the values of this dictionary</summary>
        ''' <returns>A read-only collection of values</returns>
        Public ReadOnly Property Values() As ICollection(Of TValue) Implements IDictionary(Of TKey, TValue).Values
            Get
                Return New ValueCollection(Me)
            End Get
        End Property

        ''' <summary>Gets or sets value of of item in the dictionary identified by a key</summary>
        ''' <param name="key">Key ot get or set value of</param>
        ''' <returns>Value of first occurence of <paramref name="Key"/> in the dictionary.</returns>
        ''' <value>A new value to be assigned to first occurence of <paramref name="Key"/> in the dictionary.</value>
        ''' <exception cref="KeyNotFoundException"><paramref name="key"/> is not present in the dictionary.</exception>
        Default Public Property Item(ByVal key As TKey) As TValue Implements IDictionary(Of TKey, TValue).Item
            Get
                Return GetFirst(key)
            End Get
            Set(ByVal value As TValue)
                Dim index As Integer = FirstIndexOf(key)
                If index < 0 Then
                    Throw New KeyNotFoundException(String.Format("The key '{0}' was not found in the dictionary", key))
                End If
                list(index) = New KeyValuePair(Of TKey, TValue)(list(index).Key, value)
            End Set
        End Property

        ''' <summary>Adds an item to this dictionary</summary>
        ''' <param name="item">An item - key and value - to be added</param>
        ''' <remarks>This method is inneficient when called repeatedly because it needs the dictionary to be resorted.</remarks>
        Private Sub ICollection_Add(ByVal item As KeyValuePair(Of TKey, TValue)) Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Add
            DirectCast(Me, IDictionary(Of TKey, TValue)).Add(item.Key, item.Value)
        End Sub

        ''' <summary>Clears the dictionary</summary>
        Private Sub ICollection_Clear() Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Clear
            list.Clear()
        End Sub

        ''' <summary>Determines whether the dictionary contains a specific value.</summary>
        ''' <param name="item">The object to locate in the dictionary.</param>
        ''' <returns>true if item is found in the dictionary; otherwise, false.</returns>
        ''' <remarks><paramref name="item"/>.<see cref="KeyValuePair(Of TKey, TValue).Key">Key</see> part is compared using <see cref="Comparison"/>, <paramref name="item"/>.<see cref="KeyValuePair(Of TKey, TValue).Value">Value</see> part is comparet using <see cref="M:System.Object.Equals(System.Object,System.Object)"/>.</remarks>
        Private Function ICollection_Contains(ByVal item As KeyValuePair(Of TKey, TValue)) As Boolean Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Contains
            For Each value As TValue In GetAll(item.Key)
                If Object.Equals(item.Value, value) Then
                    Return True
                End If
            Next
            Return False
        End Function
        ''' <summary>Copies the elements of the dictionary to an <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from the dictionary. The <see cref="System.Array"/> must have zero-based indexing.</param>
        ''' <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="array"/> is null</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception>
        ''' <exception cref="ArgumentException"><paramref name="array"/> is multidimensional.  -or-
        ''' <paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.  -or-
        ''' The number of elements in the source dictionary is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.  -or-
        ''' Type <typeparamref name="KeyValuePair{TKey, TValue}"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.</exception>
        Public Sub CopyTo(ByVal array As KeyValuePair(Of TKey, TValue)(), ByVal arrayIndex As Integer) Implements IDictionary(Of TKey, TValue).CopyTo
            list.CopyTo(array, arrayIndex)
        End Sub

        ''' <summary>Gets number of items in the dictionary</summary>
        Public ReadOnly Property Count() As Integer Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Count
            Get
                Return list.Count
            End Get
        End Property

        ''' <summary>Gets value indicating if this dictionary is read-only</summary>
        ''' <returns>Always returns false</returns>
        Private ReadOnly Property ICollection_IsReadOnly() As Boolean Implements ICollection(Of KeyValuePair(Of TKey, TValue)).IsReadOnly
            Get
                Return False
            End Get
        End Property

        ''' <summary>Removes first occurence of given item from the dictionary</summary>
        ''' <param name="item">An item to be removed</param>
        ''' <returns>True when <paramref name="item"/> was found in dictionary and removed, false otherwise</returns>
        ''' <remarks><paramref name="item"/>.<see cref="KeyValuePair(Of TKey, TValue).Key">Key</see> part is compared using <see cref="Comparison"/>, <paramref name="item"/>.<see cref="KeyValuePair(Of TKey, TValue).Value">Value</see> part is comparet using <see cref="M:System.Object.Equals(System.Object,System.Object)"/>.</remarks>
        Private Function ICollection_Remove(ByVal item As KeyValuePair(Of TKey, TValue)) As Boolean Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Remove
            Dim firstIndex As Integer = FirstIndexOf(item.Key)
            If firstIndex < 0 Then
                Return False
            End If
            Dim lastIndex As Integer = LastIndexOf(item.Key, firstIndex)
            For i As Integer = firstIndex To lastIndex
                If Object.Equals(item.Value, list(i).Value) Then
                    list.RemoveAt(i)
                    Return True
                End If
            Next
            Return False
        End Function

        ''' <summary>Gets type-safe generic enumerator for this dictionary</summary>
        ''' <returns>Type-safe generic enumerator for this dictionary</returns>
        Public Function GetEnumerator() As IEnumerator(Of KeyValuePair(Of TKey, TValue)) Implements IEnumerable(Of KeyValuePair(Of TKey, TValue)).GetEnumerator
            Return list.GetEnumerator()
        End Function
        ''' <summary>Gets type-unsafe generic enumerator for this dictionary</summary>
        ''' <returns>Type-unsafe generic enumerator for this dictionary</returns>
        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function
#Region "Helper classes"
        Private Class KeyCollection
            Implements ICollection(Of TKey)
            Private dictionary As DuplicateDictionary(Of TKey, TValue)
            Public Sub New(ByVal dictionary As DuplicateDictionary(Of TKey, TValue))
                If dictionary Is Nothing Then
                    Throw New ArgumentNullException("dictionary")
                End If
                Me.dictionary = dictionary
            End Sub

            Private Sub ICollection_Add(ByVal item As TKey) Implements ICollection(Of TKey).Add
                Throw New NotSupportedException()
            End Sub

            Private Sub ICollection_Clear() Implements ICollection(Of TKey).Clear
                Throw New NotSupportedException()
            End Sub

            Public Function Contains(ByVal item As TKey) As Boolean Implements ICollection(Of TKey).Contains
                Return dictionary.ContainsKey(item)
            End Function

            Public Sub CopyTo(ByVal array As TKey(), ByVal arrayIndex As Integer) Implements ICollection(Of TKey).CopyTo
                For i As Integer = 0 To dictionary.Count - 1
                    array(i + arrayIndex) = dictionary(i).Key
                Next
            End Sub

            Public ReadOnly Property Count() As Integer Implements ICollection(Of TKey).Count
                Get
                    Return dictionary.Count
                End Get
            End Property

            Private ReadOnly Property ICollection_IsReadOnly() As Boolean Implements ICollection(Of TKey).IsReadOnly
                Get
                    Return True
                End Get
            End Property

            Private Function ICollection_Remove(ByVal item As TKey) As Boolean Implements ICollection(Of TKey).Remove
                Throw New NotSupportedException()
            End Function

            Public Function GetEnumerator() As IEnumerator(Of TKey) Implements IEnumerable(Of TKey).GetEnumerator
                Return From item In dictionary Select item.Key
                '           For Each item As KeyValuePair(Of TKey, TValue) In dictionary
                'yield Return item.Key
                '           Next
            End Function

            Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
                Return GetEnumerator()
            End Function
        End Class

        Private Class ValueCollection
            Implements ICollection(Of TValue)
            Private dictionary As DuplicateDictionary(Of TKey, TValue)
            Public Sub New(ByVal dictionary As DuplicateDictionary(Of TKey, TValue))
                If dictionary Is Nothing Then
                    Throw New ArgumentNullException("dictionary")
                End If
                Me.dictionary = dictionary
            End Sub

            Private Sub ICollection_Add(ByVal item As TValue) Implements ICollection(Of TValue).Add
                Throw New NotSupportedException()
            End Sub

            Private Sub ICollection_Clear() Implements ICollection(Of TValue).Clear
                Throw New NotSupportedException()
            End Sub

            Public Function Contains(ByVal item As TValue) As Boolean Implements ICollection(Of TValue).Contains
                For Each itm As KeyValuePair(Of TKey, TValue) In dictionary
                    If Object.Equals(item, itm.Value) Then
                        Return True
                    End If
                Next
                Return False
            End Function

            Public Sub CopyTo(ByVal array As TValue(), ByVal arrayIndex As Integer) Implements ICollection(Of TValue).CopyTo
                For i As Integer = 0 To dictionary.Count - 1
                    array(i + arrayIndex) = dictionary(i).Value
                Next
            End Sub

            Public ReadOnly Property Count() As Integer Implements ICollection(Of TValue).Count
                Get
                    Return dictionary.Count
                End Get
            End Property

            Private ReadOnly Property ICollection_IsReadOnly() As Boolean Implements ICollection(Of TValue).IsReadOnly
                Get
                    Return True
                End Get
            End Property

            Private Function ICollection_Remove(ByVal item As TValue) As Boolean Implements ICollection(Of TValue).Remove
                Throw New NotSupportedException()
            End Function

            Public Function GetEnumerator() As IEnumerator(Of TValue) Implements IEnumerable(Of TValue).GetEnumerator
                Return From item In dictionary Select item.Value
                '           For Each item As KeyValuePair(Of TKey, TValue) In dictionary
                'yield Return item.Value
                '           Next
            End Function

            Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
                Return GetEnumerator()
            End Function
        End Class
#End Region
#End Region

#Region "ICloneable Members"

        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance.</returns>
        Public Function Clone() As DuplicateDictionary(Of TKey, TValue)
            Return CloneInternal()
        End Function

        ''' <summary>Internally implements the <see cref="Clone"/> method.</summary>
        ''' <returns>Cloned instance of current instance. Derived class must always override this method and return instance of derived class.</returns>
        Protected Overridable Function CloneInternal() As DuplicateDictionary(Of TKey, TValue)
            Return New DuplicateDictionary(Of TKey, TValue)(Me)
        End Function

        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance.</returns>
        Private Function ICloneable_Clone() As Object Implements ICloneable.Clone
            Return Clone()
        End Function

#End Region
    End Class
End Namespace