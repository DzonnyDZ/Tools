#If Config <= RC Then 'Stage: RC
Namespace CollectionsT.GenericT
    'HashTable(Of TKey, TValue) cancele due to being duplicate of Dictionary(Of TKey, TValue)
    '    ''' <summary>type-safe class defived from <see cref="System.Collections.Hashtable"/></summary>
    '    ''' <typeparam name="TKey">Type of keys</typeparam>
    '    ''' <typeparam name="TValue">type of values</typeparam>
    '    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(HashTable(Of String, Long)), LastChange:="12/25/2006")> _
    '    <DebuggerDisplay("Count = {Count}")> _
    '    Public Class HashTable(Of TKey, TValue)
    '        Inherits HashTable
    '        Implements IDictionary(Of TKey, TValue)
    '        ''' <summary>Adds an element with the specified key and value into the System.Collections.Hashtable.</summary>
    '        ''' <param name="value">The value of the element to add. The value can be null.</param>
    '        ''' <param name="key">The key of the element to add.</param>
    '        ''' <exception cref="System.NotSupportedException">The <see cref="System.Collections.Hashtable"/> is read-only.-or- The <see cref="System.Collections.Hashtable"/> has a fixed size.</exception>
    '        ''' <exception cref="System.ArgumentException">An element with the same key already exists in the <see cref="System.Collections.Hashtable"/>.</exception>
    '        ''' <exception cref="System.ArgumentNullException"><paramref name="key"/> is null.</exception>
    '        Public Overridable Overloads Sub Add(ByVal key As TKey, ByVal value As TValue) Implements IDictionary(Of TKey, TValue).Add
    '            MyBase.Add(key, value)
    '        End Sub
    '        ''' <summary>Adds an element with the specified key and value into the <see cref="System.Collections.Hashtable"/>.</summary>
    '        ''' <param name="value">The value of the element to add. The value can be null.</param>
    '        ''' <param name="key">The key of the element to add.</param>
    '        ''' <exception cref="System.NotSupportedException">The <see cref="System.Collections.Hashtable"/> is read-only.-or- The <see cref="System.Collections.Hashtable"/> has a fixed size.</exception>
    '        ''' <exception cref="System.ArgumentException">An element with the same key already exists in the <see cref="System.Collections.Hashtable"/>.</exception>
    '        ''' <exception cref="System.ArgumentNullException">key is null.</exception>
    '        ''' <exception cref="InvalidCastException">Either <paramref name="key"/> or <paramref name="value"/> is not of type used by this class</exception>
    '        ''' <remarks>Internaly uses type-safe overload of this function, use it instead</remarks>
    '        <Obsolete("Use type-safe variant instead"), EditorBrowsable(EditorBrowsableState.Never)> _
    '        Public NotOverridable Overloads Overrides Sub Add(ByVal key As Object, ByVal value As Object)
    '            If TypeOf key Is TKey AndAlso TypeOf value Is TValue Then
    '                Me.Add(CType(key, TKey), CType(value, TValue))
    '            Else
    '                Throw New InvalidCastException("Either key or type cannot be converted to the type used by this class")
    '            End If
    '        End Sub
    '#Region "CTors"
    '        ''' <summary>Copies the System.Collections.Hashtable elements to a one-dimensional System.Array instance at the specified index.</summary>
    '        ''' <param name="array">The one-dimensional System.Array that is the destination of the System.Collections.DictionaryEntry objects copied from System.Collections.Hashtable. The System.Array must have zero-based indexing.</param>
    '        ''' <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    '        ''' <exception cref="System.ArgumentNullException">array is null.</exception>
    '        ''' <exception cref="System.InvalidCastException">The type of the source <see cref="System.Collections.Hashtable"/> cannot be cast automatically to the type of the destination array.</exception>
    '        ''' <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than zero.</exception>
    '        ''' <exception cref="System.ArgumentException">array is multidimensional.-or- arrayIndex is equal to or greater than the length of array.-or- The number of elements in the source System.Collections.Hashtable is greater than the available space from arrayIndex to the end of the destination array.</exception>
    '        Public Shadows Sub CopyTo(ByVal array As TValue(), ByVal arrayIndex As Integer)
    '            Try
    '                MyBase.CopyTo(array, arrayIndex)
    '            Catch ex As ArgumentNullException
    '                Throw
    '            Catch ex As ArgumentOutOfRangeException
    '                Throw
    '            Catch ex As ArgumentException
    '                Throw
    '            Catch ex As InvalidCastException
    '                Throw
    '            Catch ex As Exception
    '                Throw New InvalidCastException("An exception occured when casting to array TValue()", ex)
    '            End Try
    '        End Sub

    '        ''' <summary>Initializes a new, empty instance of the System.Collections.Hashtable class using the specified initial capacity, and the default load factor, hash code provider, and comparer.</summary>
    '        ''' <param name="capacity">The approximate number of elements that the System.Collections.Hashtable object can initially contain.</param>
    '        ''' <exception cref="System.ArgumentOutOfRangeException">capacity is less than zero.</exception>
    '        Public Sub New(ByVal capacity As Integer)
    '            MyBase.New(capacity)
    '        End Sub

    '        ''' <summary>Initializes a new, empty instance of the System.Collections.Hashtable class using the specified initial capacity and load factor, and the default hash code provider and comparer.</summary>
    '        ''' <param name="capacity">The approximate number of elements that the System.Collections.Hashtable object can initially contain.</param>
    '        ''' <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
    '        ''' <exception cref="System.ArgumentOutOfRangeException">capacity is less than zero.-or- loadFactor is less than 0.1.-or- loadFactor is greater than 1.0.</exception>
    '        Public Sub New(ByVal capacity As Integer, ByVal loadFactor As Single)
    '            MyBase.New(capacity, loadFactor)
    '        End Sub

    '        ''' <summary>Initializes a new, empty instance of the System.Collections.Hashtable class using the specified initial capacity, load factor, and System.Collections.IEqualityComparer object.</summary>
    '        ''' <param name="capacity">The approximate number of elements that the System.Collections.Hashtable object can initially contain.</param>
    '        ''' <param name="equalityComparer">The System.Collections.IEqualityComparer object that defines the hash code provider and the comparer to use with the System.Collections.Hashtable.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="System.Object.GetHashCode"/> and the default comparer is each key's implementation of <see cref="System.Object.Equals"/>.</param>
    '        ''' <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
    '        ''' <exception cref="System.ArgumentOutOfRangeException">capacity is less than zero.-or- loadFactor is less than 0.1.-or- loadFactor is greater than 1.0.</exception>
    '        Public Sub New(ByVal capacity As Integer, ByVal loadFactor As Single, ByVal equalityComparer As IEqualityComparer(Of TKey))
    '            MyBase.New(capacity, loadFactor, New EqualityComparerAdapter(equalityComparer))
    '        End Sub

    '        ''' <summary>Initializes a new, empty instance of the System.Collections.Hashtable class using the specified initial capacity and System.Collections.IEqualityComparer, and the default load factor.</summary>
    '        ''' <param name="capacity">The approximate number of elements that the System.Collections.Hashtable object can initially contain.</param>
    '        ''' <param name="equalityComparer">The System.Collections.IEqualityComparer object that defines the hash code provider and the comparer to use with the System.Collections.Hashtable.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="System.Object.GetHashCode"/> and the default comparer is each key's implementation of <see cref="System.Object.Equals"/>.</param>
    '        ''' <exception cref="System.ArgumentOutOfRangeException">capacity is less than zero.</exception>
    '        Public Sub New(ByVal capacity As Integer, ByVal equalityComparer As IEqualityComparer(Of TKey))
    '            MyBase.New(capacity, New EqualityComparerAdapter(equalityComparer))
    '        End Sub
    '        ''' <summary>Initializes a new, empty instance of the System.Collections.Hashtable class using the specified initial capacity, load factor, and System.Collections.IEqualityComparer object.</summary>
    '        ''' <param name="capacity">The approximate number of elements that the System.Collections.Hashtable object can initially contain.</param>
    '        ''' <param name="equalityComparer">The System.Collections.IEqualityComparer object that defines the hash code provider and the comparer to use with the System.Collections.Hashtable.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="System.Object.GetHashCode"/> and the default comparer is each key's implementation of <see cref="System.Object.Equals"/>.</param>
    '        ''' <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
    '        ''' <exception cref="System.ArgumentOutOfRangeException">capacity is less than zero.-or- loadFactor is less than 0.1.-or- loadFactor is greater than 1.0.</exception>
    '        ''' <remarks>Use type-safe variant of this CTor instead</remarks>
    '        <Obsolete("Use type-safe variant instead"), EditorBrowsable(EditorBrowsableState.Never)> _
    '        Public Sub New(ByVal capacity As Integer, ByVal loadFactor As Single, ByVal equalityComparer As IEqualityComparer)
    '            MyBase.New(capacity, loadFactor, equalityComparer)
    '        End Sub

    '        ''' <summary>Initializes a new, empty instance of the System.Collections.Hashtable class using the specified initial capacity and System.Collections.IEqualityComparer, and the default load factor.</summary>
    '        ''' <param name="capacity">The approximate number of elements that the System.Collections.Hashtable object can initially contain.</param>
    '        ''' <param name="equalityComparer">The System.Collections.IEqualityComparer object that defines the hash code provider and the comparer to use with the System.Collections.Hashtable.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="System.Object.GetHashCode"/> and the default comparer is each key's implementation of <see cref="System.Object.Equals"/>.</param>
    '        ''' <exception cref="System.ArgumentOutOfRangeException">capacity is less than zero.</exception>
    '        ''' <remarks>Use type-safe variant of this CTor instead</remarks>
    '        <Obsolete("Use type-safe variant instead"), EditorBrowsable(EditorBrowsableState.Never)> _
    '        Public Sub New(ByVal capacity As Integer, ByVal equalityComparer As IEqualityComparer)
    '            MyBase.New(capacity, equalityComparer)
    '        End Sub
    '        ''' <summary>Initializes a new instance of the System.Collections.Hashtable class by copying the elements from the specified dictionary to the new System.Collections.Hashtable object. The new System.Collections.Hashtable object has an initial capacity equal to the number of elements copied, and uses the default load factor, hash code provider, and comparer.</summary>
    '        ''' <param name="d">The System.Collections.IDictionary object to copy to a new System.Collections.Hashtable object.</param>
    '        ''' <exception cref="System.ArgumentNullException">d is null.</exception>
    '        Public Sub New(ByVal d As IDictionary(Of TKey, TValue))
    '            MyBase.New(d.Count)
    '            For Each itm As KeyValuePair(Of TKey, TValue) In d
    '                MyBase.Add(itm.Key, itm.Value)
    '            Next itm
    '        End Sub
    '        ''' <summary>Initializes a new instance of the System.Collections.Hashtable class by copying the elements from the specified dictionary to the new System.Collections.Hashtable object. The new System.Collections.Hashtable object has an initial capacity equal to the number of elements copied, and uses the specified load factor, and the default hash code provider and comparer.</summary>
    '        ''' <param name="d">The System.Collections.IDictionary object to copy to a new System.Collections.Hashtable object.</param>
    '        ''' <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
    '        ''' <exception cref="System.ArgumentOutOfRangeException">loadFactor is less than 0.1.-or- loadFactor is greater than 1.0.</exception>
    '        ''' <exception cref="System.ArgumentNullException">d is null.</exception>
    '        ''' <exception cref="InvalidCastException">Value from collection cannot be converted to <see cref="TValue"/> -or- key from collection cannot pe converted to <see cref="TKey"/>.</exception>
    '        ''' <remarks>Use type-safe variant of this CTor instead</remarks>
    '        <Obsolete("Use type-safe variant instead"), EditorBrowsable(EditorBrowsableState.Never)> _
    '        Public Sub New(ByVal d As System.Collections.IDictionary, ByVal loadFactor As Single)
    '            MyBase.New(d.Count, loadFactor)
    '            Try
    '                For Each itm As KeyValuePair(Of TKey, TValue) In d

    '                    MyBase.Add(itm.Key, itm.Value)
    '                Next itm
    '            Catch ex As InvalidCastException
    '                Throw
    '            Catch ex As Exception
    '                Throw New InvalidCastException("Errow when casting value to be added to type of collection", ex)
    '            End Try
    '        End Sub
    '        ''' <summary>Initializes a new instance of the <see cref="System.Collections.Hashtable"/> class by copying the elements from the specified dictionary to the new System.Collections.Hashtable object. The new System.Collections.Hashtable object has an initial capacity equal to the number of elements copied, and uses the specified load factor and System.Collections.IEqualityComparer object.</summary>
    '        ''' <param name="d">The System.Collections.IDictionary object to copy to a new <see cref="System.Collections.Hashtable"/> object.</param>
    '        ''' <param name="equalityComparer">The <see cref="System.Collections.IEqualityComparer"/> object that defines the hash code provider and the comparer to use with the <see cref="System.Collections.Hashtable"/>.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="System.Object.GetHashCode"/> and the default comparer is each key's implementation of <see cref="System.Object.Equals"/>.</param>
    '        ''' <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
    '        ''' <exception cref="System.ArgumentOutOfRangeException">loadFactor is less than 0.1.-or- loadFactor is greater than 1.0.</exception>
    '        ''' <exception cref="System.ArgumentNullException">d is null.</exception>
    '        ''' <remarks>Use type-safe variant of this CTor instead</remarks>
    '        <Obsolete("Use type-safe variant instead"), EditorBrowsable(EditorBrowsableState.Never)> _
    '        Public Sub New(ByVal d As System.Collections.IDictionary, ByVal loadFactor As Single, ByVal equalityComparer As IEqualityComparer)
    '            MyBase.New(d.Count, loadFactor, equalityComparer)
    '            For Each itm As KeyValuePair(Of TKey, TValue) In d
    '                MyClass.Add(itm.Key, itm.Value)
    '            Next
    '        End Sub
    '        ''' <summary>Initializes a new instance of the <see cref="System.Collections.Hashtable"/> class by copying the elements from the specified dictionary to the new <see cref="System.Collections.Hashtable"/> object. The new System.Collections.Hashtable object has an initial capacity equal to the number of elements copied, and uses the specified load factor and <see cref="System.Collections.IEqualityComparer"/> object.</summary>
    '        ''' <param name="d">The <see cref="System.Collections.IDictionary"/> object to copy to a new <see cref="System.Collections.Hashtable"/> object.</param>
    '        ''' <param name="equalityComparer">The <see cref="System.Collections.IEqualityComparer"/> object that defines the hash code provider and the comparer to use with the <see cref="System.Collections.Hashtable"/>.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="System.Object.GetHashCode"/> and the default comparer is each key's implementation of <see cref="System.Object.Equals"/>.</param>
    '        ''' <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
    '        ''' <exception cref="System.ArgumentOutOfRangeException">loadFactor is less than 0.1.-or- loadFactor is greater than 1.0.</exception>
    '        ''' <exception cref="System.ArgumentNullException">d is null.</exception>
    '        ''' <exception cref="InvalidCastException">Value from collection cannot be converted to <see cref="TValue"/> -or- key from collection cannot pe converted to <see cref="TKey"/>.</exception>
    '        ''' <remarks>Use type-safe variant of this CTor instead</remarks>
    '        <Obsolete("Use type-safe variant instead"), EditorBrowsable(EditorBrowsableState.Never)> _
    '        Public Sub New(ByVal d As System.Collections.IDictionary, ByVal loadFactor As Single, ByVal equalityComparer As IEqualityComparer(Of TKey))
    '            MyBase.New(d.Count, loadFactor, New EqualityComparerAdapter(equalityComparer))
    '            Try
    '                For Each itm As KeyValuePair(Of TKey, TValue) In d
    '                    MyBase.Add(itm.Key, itm.Value)
    '                Next itm
    '            Catch ex As InvalidCastException
    '                Throw
    '            Catch ex As Exception
    '                Throw New InvalidCastException("Errow when casting value to be added to type of collection", ex)
    '            End Try
    '        End Sub
    '        ''' <summary>Initializes a new instance of the <see cref="System.Collections.Hashtable"/> class by copying the elements from the specified dictionary to a new System.Collections.Hashtable object. The new <see cref="System.Collections.Hashtable"/> object has an initial capacity equal to the number of elements copied, and uses the default load factor and the specified <see cref="System.Collections.IEqualityComparer"/> object.</summary>
    '        ''' <param name="d">The <see cref="System.Collections.IDictionary"/> object to copy to a new <see cref="System.Collections.Hashtable"/> object.</param>
    '        ''' <param name="equalityComparer">The <see cref="System.Collections.IEqualityComparer"/> object that defines the hash code provider and the comparer to use with the System.Collections.Hashtable.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="System.Object.GetHashCode"/> and the default comparer is each key's implementation of <see cref="System.Object.Equals"/>.</param>
    '        ''' <exception cref="System.ArgumentNullException"><paramref name="d"/> is null.</exception>
    '        ''' <exception cref="InvalidCastException">Value from collection cannot be converted to <see cref="TValue"/> -or- key from collection cannot pe converted to <see cref="TKey"/>.</exception>
    '        ''' <remarks>Use type-safe variant of this CTor instead</remarks>
    '        <Obsolete("Use type-safe variant instead"), EditorBrowsable(EditorBrowsableState.Never)> _
    '        Public Sub New(ByVal d As System.Collections.IDictionary, ByVal equalityComparer As IEqualityComparer)
    '            MyBase.New(d.Count, equalityComparer)
    '            Try
    '                For Each itm As KeyValuePair(Of TKey, TValue) In d
    '                    MyClass.Add(itm.Key, itm.Value)
    '                Next itm
    '            Catch ex As InvalidCastException
    '                Throw
    '            Catch ex As Exception
    '                Throw New InvalidCastException("Errow when casting value to be added to type of collection", ex)
    '            End Try
    '        End Sub
    '        ''' <summary>Initializes a new instance of the System.Collections.Hashtable class by copying the elements from the specified dictionary to the new System.Collections.Hashtable object. The new System.Collections.Hashtable object has an initial capacity equal to the number of elements copied, and uses the specified load factor, and the default hash code provider and comparer.</summary>
    '        ''' <param name="d">The System.Collections.IDictionary object to copy to a new System.Collections.Hashtable object.</param>
    '        ''' <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
    '        ''' <exception cref="System.ArgumentOutOfRangeException">loadFactor is less than 0.1.-or- loadFactor is greater than 1.0.</exception>
    '        ''' <exception cref="System.ArgumentNullException">d is null.</exception>
    '        Public Sub New(ByVal d As IDictionary(Of TKey, TValue), ByVal loadFactor As Single)
    '            MyBase.New(d.Count, loadFactor)
    '            For Each itm As KeyValuePair(Of TKey, TValue) In d
    '                MyBase.Add(itm.Key, itm.Value)
    '            Next itm
    '        End Sub
    '        ''' <summary>Initializes a new instance of the System.Collections.Hashtable class by copying the elements from the specified dictionary to the new System.Collections.Hashtable object. The new System.Collections.Hashtable object has an initial capacity equal to the number of elements copied, and uses the specified load factor and System.Collections.IEqualityComparer object.</summary>
    '        ''' <param name="d">The System.Collections.IDictionary object to copy to a new System.Collections.Hashtable object.</param>
    '        ''' <param name="equalityComparer">The System.Collections.IEqualityComparer object that defines the hash code provider and the comparer to use with the System.Collections.Hashtable.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="System.Object.GetHashCode"/> and the default comparer is each key's implementation of <see cref="System.Object.Equals"/>.</param>
    '        ''' <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
    '        ''' <exception cref="System.ArgumentOutOfRangeException">loadFactor is less than 0.1.-or- loadFactor is greater than 1.0.</exception>
    '        ''' <remarks>Use type-safe variant of this CTor instead</remarks>
    '        <Obsolete("Use type-safe variant instead"), EditorBrowsable(EditorBrowsableState.Never)> _
    '        Public Sub New(ByVal d As IDictionary(Of TKey, TValue), ByVal loadFactor As Single, ByVal equalityComparer As IEqualityComparer)
    '            MyBase.New(d.Count, loadFactor, equalityComparer)
    '            For Each itm As KeyValuePair(Of TKey, TValue) In d
    '                MyClass.Add(itm.Key, itm.Value)
    '            Next
    '        End Sub
    '        ''' <summary>Initializes a new instance of the System.Collections.Hashtable class by copying the elements from the specified dictionary to the new System.Collections.Hashtable object. The new System.Collections.Hashtable object has an initial capacity equal to the number of elements copied, and uses the specified load factor and System.Collections.IEqualityComparer object.</summary>
    '        ''' <param name="d">The System.Collections.IDictionary object to copy to a new System.Collections.Hashtable object.</param>
    '        ''' <param name="equalityComparer">The System.Collections.IEqualityComparer object that defines the hash code provider and the comparer to use with the System.Collections.Hashtable.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="System.Object.GetHashCode"/> and the default comparer is each key's implementation of <see cref="System.Object.Equals"/>.</param>
    '        ''' <param name="loadFactor">A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
    '        ''' <exception cref="System.ArgumentOutOfRangeException">loadFactor is less than 0.1.-or- loadFactor is greater than 1.0.</exception>
    '        ''' <exception cref="System.ArgumentNullException">d is null.</exception>
    '        Public Sub New(ByVal d As IDictionary(Of TKey, TValue), ByVal loadFactor As Single, ByVal equalityComparer As IEqualityComparer(Of TKey))
    '            MyBase.New(d.Count, loadFactor, New EqualityComparerAdapter(equalityComparer))
    '            For Each itm As KeyValuePair(Of TKey, TValue) In d
    '                MyBase.Add(itm.Key, itm.Value)
    '            Next
    '        End Sub
    '        ''' <summary>Initializes a new instance of the <see cref="System.Collections.Hashtable"/> class by copying the elements from the specified dictionary to a new System.Collections.Hashtable object. The new System.Collections.Hashtable object has an initial capacity equal to the number of elements copied, and uses the default load factor and the specified <see cref="System.Collections.IEqualityComparer"/> object.</summary>
    '        ''' <param name="d">The <see cref="System.Collections.IDictionary"/> object to copy to a new <see cref="System.Collections.Hashtable"/> object.</param>
    '        ''' <param name="equalityComparer">The <see cref="System.Collections.IEqualityComparer"/> object that defines the hash code provider and the comparer to use with the System.Collections.Hashtable.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="System.Object.GetHashCode"/> and the default comparer is each key's implementation of <see cref="System.Object.Equals"/>.</param>
    '        ''' <exception cref="System.ArgumentNullException"><paramref name="d"/> is null.</exception>
    '        ''' <remarks>Use type-safe variant of this CTor instead</remarks>
    '        <Obsolete("Use type-safe variant instead"), EditorBrowsable(EditorBrowsableState.Never)> _
    '        Public Sub New(ByVal d As IDictionary(Of TKey, TValue), ByVal equalityComparer As IEqualityComparer)
    '            MyBase.New(d.Count, equalityComparer)
    '            For Each itm As KeyValuePair(Of TKey, TValue) In d
    '                MyClass.Add(itm.Key, itm.Value)
    '            Next
    '        End Sub
    '        ''' <summary>Initializes a new instance of the <see cref="System.Collections.Hashtable"/> class by copying the elements from the specified dictionary to a new System.Collections.Hashtable object. The new System.Collections.Hashtable object has an initial capacity equal to the number of elements copied, and uses the default load factor and the specified System.Collections.IEqualityComparer object.</summary>
    '        ''' <param name="d">The <see cref="System.Collections.IDictionary"/> object to copy to a new <see cref="System.Collections.Hashtable"/> object.</param>
    '        ''' <param name="equalityComparer">The <see cref="System.Collections.IEqualityComparer"/> object that defines the hash code provider and the comparer to use with the <see cref="System.Collections.Hashtable"/>.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="System.Object.GetHashCode"/> and the default comparer is each key's implementation of <see cref="System.Object.Equals"/>.</param>
    '        ''' <exception cref="System.ArgumentNullException">d is null.</exception>
    '        Public Sub New(ByVal d As IDictionary(Of TKey, TValue), ByVal equalityComparer As IEqualityComparer(Of TKey))
    '            MyBase.New(d.Count, New EqualityComparerAdapter(equalityComparer))
    '            For Each itm As KeyValuePair(Of TKey, TValue) In d
    '                MyBase.Add(itm.Key, itm.Value)
    '            Next
    '        End Sub
    '        ''' <summary>Initializes a new, empty instance of the <see cref="System.Collections.Hashtable"/> class using the default initial capacity and load factor, and the specified System.Collections.IEqualityComparer object.</summary>
    '        ''' <param name="equalityComparer">The <see cref="System.Collections.IEqualityComparer"/> object that defines the hash code provider and the comparer to use with the System.Collections.Hashtable object.-or- null to use the default hash code provider and the default comparer. The default hash code provider is each key's implementation of <see cref="System.Object.GetHashCode"/> and the default comparer is each key's implementation of <see cref="System.Object.Equals"/>.</param>
    '        Public Sub New(ByVal equalityComparer As IEqualityComparer(Of TKey))
    '            MyBase.New(New EqualityComparerAdapter(equalityComparer))
    '        End Sub
    '        ''' <summary>Initializes a new, empty instance of the <see cref="System.Collections.Hashtable"/> class that is serializable using the specified System.Runtime.Serialization.SerializationInfo and System.Runtime.Serialization.StreamingContext objects.</summary>
    '        ''' <param name="context">A <see cref="System.Runtime.Serialization.StreamingContext"/> object containing the source and destination of the serialized stream associated with the System.Collections.Hashtable.</param>
    '        ''' <param name="info">A <see cref="System.Runtime.Serialization.SerializationInfo"/> object containing the information required to serialize the System.Collections.Hashtable object.</param>
    '        ''' <exception cref="System.ArgumentNullException">info is null.</exception>
    '        Protected Sub New(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext)
    '            MyBase.New(info, context)
    '        End Sub
    '#End Region
    '        ''' <summary>Removes the element with the specified key from the <see cref="System.Collections.Hashtable"/>.</summary>
    '        ''' <param name="key">The key of the element to remove.</param>
    '        ''' <exception cref="System.NotSupportedException">The <see cref="System.Collections.Hashtable"/> is read-only.-or- The <see cref="System.Collections.Hashtable"/> has a fixed size.</exception>
    '        ''' <exception cref="System.ArgumentNullException">key is null.</exception>
    '        Public Overridable Shadows Sub Remove(ByVal Key As TKey)
    '            MyBase.Remove(Key)
    '        End Sub
    '        ''' <summary>Determines whether the <see cref="System.Collections.Hashtable"/> contains a specific key.</summary>
    '        ''' <param name="key">The key to locate in the <see cref="System.Collections.Hashtable"/>.</param>
    '        ''' <returns>true if the <see cref="System.Collections.Hashtable"/> contains an element with the specified key; otherwise, false.</returns>
    '        ''' <exception cref="System.ArgumentNullException"><paramref name="key"/> is null.</exception>
    '        Public Overridable Shadows Function Contains(ByVal key As TKey) As Boolean
    '            Return MyBase.Contains(key)
    '        End Function
    '        ''' <summary>Determines whether the <see cref="System.Collections.Hashtable"/> contains a specific key.</summary>
    '        ''' <param name="key">The key to locate in the <see cref="System.Collections.Hashtable"/>.</param>
    '        ''' <returns>true if the <see cref="System.Collections.Hashtable"/> contains an element with the specified key; otherwise, false.</returns>
    '        ''' <exception cref="System.ArgumentNullException">key is null.</exception>
    '        Public Overridable Shadows Function ContainsKey(ByVal key As TKey) As Boolean Implements IDictionary(Of TKey, TValue).ContainsKey
    '            Return MyBase.ContainsKey(key)
    '        End Function
    '        ''' <summary>Determines whether the <see cref="System.Collections.Hashtable"/> contains a specific value.</summary>
    '        ''' <param name="value">The value to locate in the <see cref="System.Collections.Hashtable"/>. The value can be null.</param>
    '        ''' <returns>true if the <see cref="System.Collections.Hashtable"/> contains an element with the specified value; otherwise, false.</returns>
    '        Public Overridable Shadows Function ContainsValue(ByVal value As TValue) As Boolean
    '            Return MyBase.ContainsValue(value)
    '        End Function
    '        ''' <summary>Returns the hash code for the specified key.</summary>
    '        ''' <param name="key">The <see cref="TKey"/> for which a hash code is to be returned.</param>
    '        ''' <returns>The hash code for key.</returns>
    '        ''' <exception cref="System.NullReferenceException"><paramref name="key"/> is null.</exception>
    '        Protected Overridable Overloads Function GetHash(ByVal key As TKey) As Integer
    '            Return MyBase.GetHash(key)
    '        End Function

    '        ''' <summary>Returns the hash code for the specified key.</summary>
    '        ''' <param name="key">The <see cref="System.Object"/> for which a hash code is to be returned.</param>
    '        ''' <returns>The hash code for key.</returns>
    '        ''' <exception cref="System.NullReferenceException">key is null.</exception>
    '        ''' <exception cref="InvalidCastException">Value of <paramref name="key"/> cannot be converted to <see cref="TKey"/>.</exception>
    '        ''' <remarks>Internally calls type-safe overload of this function, use it instead.</remarks>
    '        <Obsolete("Use type-safe variant instead"), EditorBrowsable(EditorBrowsableState.Never)> _
    '        Protected NotOverridable Overloads Overrides Function GetHash(ByVal key As Object) As Integer
    '            Try
    '                Return GetHash(CType(key, TKey))
    '            Catch ex As NullReferenceException
    '                Throw
    '            Catch ex As InvalidCastException
    '                Throw
    '            Catch ex As Exception
    '                Throw New InvalidCastException("Error while casting key to TKey", ex)
    '            End Try
    '        End Function
    '        ''' <summary>Compares a specific <see cref="TKey"/> with a specific key in the <see cref="System.Collections.Hashtable"/>.</summary>
    '        ''' <param name="item">The <see cref="TKey"/> to compare with key.</param>
    '        ''' <param name="key">The key in the <see cref="System.Collections.Hashtable"/> to compare with item.</param>
    '        ''' <returns>true if <paramref name="item"/> and <paramref name="key"/> are equal; otherwise, false.</returns>
    '        ''' <exception cref="System.ArgumentNullException"><paramref name="item"/> is null.-or- <paramref name="key"/> is null.</exception>
    '        Protected Overridable Overloads Function KeyEquals(ByVal item As TKey, ByVal key As TKey) As Integer
    '            Return MyBase.KeyEquals(item, key)
    '        End Function
    '        ''' <summary>Compares a specific <see cref="System.Object"/> with a specific key in the <see cref="System.Collections.Hashtable"/>.</summary>
    '        ''' <param name="item">The <see cref="System.Object"/> to compare with key.</param>
    '        ''' <param name="key">The key in the <see cref="System.Collections.Hashtable"/> to compare with <see cref="item"/>.</param>
    '        ''' <returns>true if <paramref name="item"/> and <paramref name="key"/> are equal; otherwise, false.</returns>
    '        ''' <exception cref="System.ArgumentNullException"><paramref name="item"/> is null.-or- <paramref name="key"/> is null.</exception>
    '        ''' <remarks>Internally calls type-safe overload of this function, use it instead.</remarks>
    '        <Obsolete("Use type-safe variant instead"), EditorBrowsable(EditorBrowsableState.Never)> _
    '        Protected NotOverridable Overloads Overrides Function KeyEquals(ByVal item As Object, ByVal key As Object) As Boolean
    '            If TypeOf item Is TKey AndAlso TypeOf key Is TKey Then
    '                Return KeyEquals(CType(item, TKey), CType(key, TKey))
    '            Else
    '                Return item.Equals(key)
    '            End If
    '        End Function
    '        ''' <summary>Gets or sets the value associated with the specified key.</summary>
    '        ''' <param name="key">The key whose value to get or set.</param>
    '        ''' <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new element using the specified key.</returns>
    '        ''' <exception cref="System.ArgumentNullException"><paramref name="key"/> is null.</exception>
    '        ''' <exception cref="InvalidCastException">Value of <paramref name="key"/> cannot be converted to type <see cref="TKey"/> -or- value of new value cannot be casted to TValue</exception>
    '        ''' <remarks>Internaly uses type-safe overload of this property, use it instead</remarks>
    '        <Obsolete("Use type-safe variant instead"), EditorBrowsable(EditorBrowsableState.Never)> _
    '        Default Public Overrides Property Item(ByVal key As Object) As Object
    '            Get
    '                Try
    '                    Return Me.Item(CType(key, TKey))
    '                Catch ex As ArgumentNullException
    '                    Throw
    '                Catch ex As InvalidCastException
    '                    Throw
    '                Catch ex As Exception
    '                    Throw New InvalidCastException("Error while casting key into TKey", ex)
    '                End Try
    '            End Get
    '            Set(ByVal value As Object)
    '                Try
    '                    Me.Item(CType(key, TKey)) = value
    '                Catch ex As ArgumentNullException
    '                    Throw
    '                Catch ex As InvalidCastException
    '                    Throw
    '                Catch ex As Exception
    '                    Throw New InvalidCastException("Error while casting key into TKey", ex)
    '                End Try
    '            End Set
    '        End Property
    '        ''' <summary>Gets or sets the value associated with the specified key.</summary>
    '        ''' <param name="key">The key whose value to get or set.</param>
    '        ''' <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new element using the specified key.</returns>
    '        ''' <exception cref="System.NotSupportedException">The property is set and the <see cref="System.Collections.Hashtable"/> is read-only.-or- The property is set, key does not exist in the collection, and the <see cref="System.Collections.Hashtable"/> has a fixed size.</exception>
    '        ''' <exception cref="System.ArgumentNullException"><paramref name="key"/> is null.</exception>
    '        Default Public Overridable Overloads Property Item(ByVal key As TKey) As TValue Implements IDictionary(Of TKey, TValue).Item
    '            Get
    '                Return MyBase.Item(key)
    '            End Get
    '            Set(ByVal value As TValue)
    '                MyBase.Item(key) = value
    '            End Set
    '        End Property

    '        ''' <summary>Gets an <see cref="System.Collections.Generic.ICollection(Of TKey)"/> containing the keys in the <see cref="System.Collections.Hashtable"/>.</summary>
    '        ''' <returns>An <see cref="System.Collections.Generic.ICollection(Of TKey)"/> containing the keys in the <see cref="System.Collections.Hashtable"/>.</returns>
    '        Public Shadows ReadOnly Property Keys() As ICollection(Of TKey) Implements IDictionary(Of TKey, TValue).Keys
    '            Get
    '                Dim MyK As ICollection = MyBase.Keys
    '                Dim RetKeys As New List(Of TKey)(MyK.Count)
    '                For Each K As Object In MyK
    '                    RetKeys.Add(K)
    '                Next K
    '                Return RetKeys.AsReadOnly
    '            End Get
    '        End Property
    '        ''' <summary>Gets an <see cref="System.Collections.Generic.ICollection(Of TValue)"/> containing the values in the <see cref="System.Collections.Hashtable"/>.</summary>
    '        ''' <returns>An <see cref="System.Collections.Generic.ICollection(Of TVlaue)"/> containing the values in the <see cref="System.Collections.Hashtable"/></returns>
    '        Public Shadows ReadOnly Property values() As ICollection(Of TValue) Implements IDictionary(Of TKey, TValue).Values
    '            Get
    '                Dim MyV As ICollection = MyBase.Values
    '                Dim RetVals As New List(Of TValue)(MyV.Count)
    '                For Each V As Object In MyV
    '                    RetVals.Add(V)
    '                Next V
    '                Return RetVals.AsReadOnly
    '            End Get
    '        End Property



    '        ''' <summary>Adds an item to the <see cref="System.Collections.Generic.ICollection(Of T)"/>.</summary>
    '        ''' <param name="item">The object to add to the <see cref="System.Collections.Generic.ICollection(Of T)"/>.</param>
    '        Public Overloads Sub Add(ByVal item As System.Collections.Generic.KeyValuePair(Of TKey, TValue)) Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Add
    '            Add(item.Key, item.Value)
    '        End Sub
    '        ''' <summary>Removes all elements from the System.Collections.Hashtable.</summary>
    '        ''' <exception cref="System.NotSupportedException">The System.Collections.Hashtable is read-only</exception>
    '        Public Overrides Sub Clear() Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Clear
    '            MyBase.Clear()
    '        End Sub

    '        ''' <summary>Gets the number of key/value pairs contained in the <see cref="System.Collections.Hashtable"/>.</summary>
    '        ''' <returns>The number of key/value pairs contained in the <see cref="System.Collections.Hashtable"/>.</returns>
    '        Public Overrides ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Count
    '            Get
    '                Return MyBase.Count
    '            End Get
    '        End Property
    '        ''' <summary>Gets a value indicating whether the <see cref="System.Collections.Hashtable"/> is read-only.</summary>
    '        ''' <returns>true if the <see cref="System.Collections.Hashtable"/> is read-only; otherwise, false. The default is false.</returns>
    '        Public Overrides ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).IsReadOnly
    '            Get
    '                Return MyBase.IsReadOnly
    '            End Get
    '        End Property
    '        ''' <summary>Removes the first occurrence of a specific object from the <see cref="System.Collections.Generic.ICollection(Of T)"/>.</summary>
    '        ''' <param name="item">The object to remove from the <see cref="System.Collections.Generic.ICollection(Of T)"/>.</param>
    '        ''' <returns>true if item was successfully removed from the <see cref="System.Collections.Generic.ICollection(Of T)"/>; otherwise, false. This method also returns false if item is not found in the original <see cref="System.Collections.Generic.ICollection(Of T)"/>.</returns>
    '        ''' <remarks>This implementation removes item from <see cref="HashTable(Of TKey,TValue)"/> when there is value with the same key as <see cref="KeyValuePair(Of TKey, TValue).Key"/> and its value is same as value of <paramref name="item"/>.</remarks>
    '        ''' <exception cref="System.NotSupportedException">The <see cref="System.Collections.Generic.ICollection(Of T)"/> is read-only.</exception>
    '        Public Overridable Overloads Function TryRemove(ByVal item As System.Collections.Generic.KeyValuePair(Of TKey, TValue)) As Boolean Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Remove
    '            If Me.Contains(item.Key) AndAlso Me.Item(item.Key).Equals(item.Value) Then
    '                Try
    '                    Me.Remove(item.Key)
    '                    Return Not Me.Contains(item.Key)
    '                Catch
    '                    Return False
    '                End Try
    '            Else
    '                Return False
    '            End If
    '        End Function

    '        ''' <summary>Determines whether the <see cref="System.Collections.Generic.ICollection(Of T)"/> contains a specific value.</summary>
    '        ''' <param name="item">The object to locate in the <see cref="System.Collections.Generic.ICollection(Of T)"/>.</param>
    '        ''' <returns>true if item is found in the <see cref="System.Collections.Generic.ICollection(Of T)"/>; otherwise, false.</returns>
    '        ''' <remarks>This implementation returns true when there is item with the same key as key passed in <paramref name="item"/> and its value is also same as value passed in <paramref name="item"/></remarks>
    '        Public Overridable Shadows Function Contains(ByVal item As System.Collections.Generic.KeyValuePair(Of TKey, TValue)) As Boolean Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Contains
    '            Return Me.Contains(item.Key) AndAlso Me.Item(item.Key).Equals(item.Value)
    '        End Function
    '        ''' <summary>Copies the elements of the <see cref="System.Collections.Generic.ICollection(Of T)"/> to an <see cref="System.Array"/>, starting at a particular <see cref="System.Array"/> index.</summary>
    '        ''' <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from <see cref="System.Collections.Generic.ICollection(Of T)"/>. The <see cref="System.Array"/> must have zero-based indexing.</param>
    '        ''' <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
    '        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception>
    '        ''' <exception cref="System.ArgumentNullException"><paramref name="array"/> is null.</exception>
    '        ''' <exception cref="System.ArgumentException">
    '        ''' <paramref name="array"/> is multidimensional.-or-
    '        ''' <paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.-or-
    '        ''' The number of elements in the source <see cref="System.Collections.Generic.ICollection(Of T)"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/></exception>
    '        Public Shadows Sub CopyTo(ByVal array() As System.Collections.Generic.KeyValuePair(Of TKey, TValue), ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).CopyTo
    '            If arrayIndex < 0 Then Throw New ArgumentOutOfRangeException("arrayIndex", "Index of within array must be greater than or equal to zero")
    '            If array Is Nothing Then Throw New ArgumentNullException("array", "Array cannot be null")
    '            If array.Rank <> 1 OrElse _
    '                    arrayIndex >= array.Length OrElse _
    '                    Me.Count > array.Length - arrayIndex Then _
    '                Throw New ArgumentException("Array is multidimensional or there is not enough space between arrayIndex and the end of array for all items in this collection.")
    '            Dim Index As Integer = arrayIndex
    '            For Each kv As KeyValuePair(Of TKey, TValue) In Me
    '                array(Index) = kv
    '                Index += 1
    '            Next kv
    '        End Sub


    '        ''' <summary>Removes the element with the specified key from the <see cref="System.Collections.Generic.IDictionary(Of TKey, TValue)"/>.</summary>
    '        ''' <param name="key">The key of the element to remove.</param>
    '        ''' <returns>true if the element is successfully removed; otherwise, false. This method also returns false if key was not found in the original <see cref="System.Collections.Generic.IDictionary(Of TKey, TValue)"/>.</returns>
    '        ''' <exception cref="System.NotSupportedException">The <see cref="System.Collections.Generic.IDictionary(Of TKey, TValue)"/> is read-only.</exception>
    '        ''' <exception cref="System.ArgumentNullException"><paramref name="key"/> is null.</exception>
    '        Public Overridable Overloads Function TryRemove(ByVal key As TKey) As Boolean Implements System.Collections.Generic.IDictionary(Of TKey, TValue).Remove
    '            If Me.Contains(key) Then
    '                Try
    '                    Me.Remove(key)
    '                    Return Not Me.Contains(key)
    '                Catch
    '                    Return False
    '                End Try
    '            Else
    '                Return False
    '            End If
    '        End Function
    '        ''' <summary>Gets the value associated with the specified key. </summary>
    '        ''' <param name="key">The key whose value to get.</param>
    '        ''' <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
    '        ''' <returns>true if the object that implements <see cref="IDictionary"/> contains an element with the specified key; otherwise, false. </returns>
    '        Public Function TryGetValue(ByVal key As TKey, ByRef value As TValue) As Boolean Implements System.Collections.Generic.IDictionary(Of TKey, TValue).TryGetValue
    '            If Me.Contains(key) Then
    '                TryGetValue = True
    '                value = Me(key)
    '            Else
    '                TryGetValue = False
    '                Dim ret As TValue
    '                value = ret
    '            End If
    '        End Function
    '        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
    '        ''' <returns>A System.Collections.Generic.IEnumerator(Of T) that can be used to iterate through the collection</returns>
    '        Public Overridable Shadows Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)) Implements System.Collections.Generic.IEnumerable(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).GetEnumerator
    '            Return New KVEnumerator(Me.GetEnumerator)
    '        End Function

    '        ''' <summary>Adapts <see cref="IDictionaryEnumerator"/> into <see cref="IEnumerator(Of KeyValuePair(Of TKey, TValue))"/></summary>
    '        Protected Class KVEnumerator : Implements IEnumerator(Of KeyValuePair(Of TKey, TValue))
    '            ''' <summary><see cref="IDictionaryEnumerator"/> to be adapted</summary>
    '            Private internal As IDictionaryEnumerator
    '            ''' <summary>CTor</summary>
    '            ''' <param name="E"><see cref="IDictionaryEnumerator"/> to be adapted</param>
    '            Public Sub New(ByVal E As IDictionaryEnumerator)
    '                internal = E
    '            End Sub
    '            ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
    '            ''' <returns>The element in the collection at the current position of the enumerator</returns>
    '            Public Overridable ReadOnly Property Current() As System.Collections.Generic.KeyValuePair(Of TKey, TValue) Implements System.Collections.Generic.IEnumerator(Of System.Collections.Generic.KeyValuePair(Of TKey, TValue)).Current
    '                Get
    '                    Return New KeyValuePair(Of TKey, TValue)(internal.Key, internal.Value)
    '                End Get
    '            End Property
    '            ''' <summary>Gets the current element in the collection.</summary>
    '            ''' <returns>The current element in the collection.</returns>
    '            ''' <exception cref="System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
    '            <Obsolete(), EditorBrowsable(EditorBrowsableState.Never)> _
    '            Public Overridable ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
    '                Get
    '                    Return Current
    '                End Get
    '            End Property
    '            ''' <summary>Advances the enumerator to the next element of the collection.</summary>
    '            ''' <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
    '            ''' <exception cref="System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
    '            Public Overridable Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
    '                Return internal.MoveNext
    '            End Function
    '            ''' <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
    '            ''' <exception cref="System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
    '            Public Overridable Sub Reset() Implements System.Collections.IEnumerator.Reset
    '                internal.Reset()
    '            End Sub
    '            ''' <summary>To detect redundant calls</summary>
    '            Private disposedValue As Boolean = False
    '#Region " IDisposable Support "
    '            ''' <summary><see cref="IDisposable"/></summary>
    '            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
    '                If Not Me.disposedValue Then
    '                    If disposing Then
    '                    End If
    '                End If
    '                Me.disposedValue = True
    '            End Sub

    '            ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources</summary>
    '            ''' <remarks>This code added by Visual Basic to correctly implement the disposable pattern.</remarks>
    '            Public Sub Dispose() Implements IDisposable.Dispose
    '                ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '                Dispose(True)
    '                GC.SuppressFinalize(Me)
    '            End Sub
    '#End Region

    '        End Class
    '    End Class
    ''' <summary>Provides HashTable designed for storing only keys and quickly testing if key is in collection or not</summary>
    ''' <remarks>Internally uses <see cref="Dictionary(Of T, Object)"/>. Can be used as List of unique items.</remarks>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(HashTable(Of Long)), LastChMMDDYYYY:="12/25/2006")> _
    Public Class HashTable(Of T)
        Implements ICollection(Of T)
        ''' <summary><see cref="Dictionary(Of T, Object)"/> internally used by this instance</summary>
        Protected Internal As Dictionary(Of T, Object)
#Region "CTors"
        ''' <summary>CTor (optionally with comparer)</summary>
        ''' <param name="EqualityComparer"><see cref="EqualityComparer(Of T)"/> used to compare values (or null to use default)</param>
        Public Sub New(Optional ByVal EqualityComparer As IEqualityComparer(Of T) = Nothing)
            If EqualityComparer Is Nothing Then
                Internal = New Dictionary(Of T, Object)
            Else
                Internal = New Dictionary(Of T, Object)(EqualityComparer)
            End If
        End Sub
        ''' <summary>CTor (with capacity and optionally with comparer)</summary>
        ''' <param name="Capacity">Initial capacity of internal <see cref="Hashtable"/></param>
        ''' <param name="EqualityComparer"><see cref="EqualityComparer(Of T)"/> used to compare values (or null to use default)</param>
        ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="capacity"/> is less than zero</exception>
        Public Sub New(ByVal Capacity As Integer, Optional ByVal EqualityComparer As IEqualityComparer(Of T) = Nothing)
            If EqualityComparer Is Nothing Then
                Internal = New Dictionary(Of T, Object)(Capacity)
            Else
                Internal = New Dictionary(Of T, Object)(Capacity, EqualityComparer)
            End If
        End Sub
        '''' <summary>CTor (with capacity, load factor and optionally comparer)</summary>
        '''' <param name="Capacity">Initial capacity of internal <see cref="Hashtable"/></param>
        '''' <param name="LoadFactor"> A number in the range from 0.1 through 1.0 that is multiplied by the default value which provides the best performance. The result is the maximum ratio of elements to buckets.</param>
        '''' <param name="EqualityComparer"><see cref="EqualityComparer(Of T)"/> used to compare values (or null to use default)</param>
        '''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="capacity"/> is less than zero.-or- <paramref name="loadFactor"/> is less than 0.1.-or- <paramref name="loadFactor"/> is greater than 1.0.</exception>
        'Public Sub New(ByVal Capacity As Integer, ByVal LoadFactor As Single, Optional ByVal EqualityComparer As IEqualityComparer(Of T) = Nothing)
        '    If EqualityComparer Is Nothing Then
        '        Internal = New Dictionary(Of T, Object)(Capacity, LoadFactor)
        '    Else
        '        Internal = New Dictionary(Of T, Object)(Capacity, LoadFactor, New EqualityComparerAdapter(EqualityComparer))
        '    End If
        'End Sub
#End Region
        ''' <summary>Adds an element into collection.</summary>
        ''' <param name="item">The value of the element to add. The value cannot be null.</param>
        ''' <exception cref="System.ArgumentException">An element already exists in internall <see cref="System.Collections.Hashtable"/>.</exception>
        ''' <exception cref="System.ArgumentNullException">An element is null</exception>
        Public Sub Add(ByVal item As T) Implements System.Collections.Generic.ICollection(Of T).Add
            Internal.Add(item, Nothing)
        End Sub

        ''' <summary>Clears internal <see cref="Hashtable"/> in order to contain no elements</summary>
        Public Sub Clear() Implements System.Collections.Generic.ICollection(Of T).Clear
            Internal.Clear()
        End Sub
        ''' <summary>Determines whether the collection contains a specific value.</summary>
        ''' <param name="item">The object to locate in the collection.</param>
        ''' <returns>true if item is found in the internal <see cref="Hashtable"/>; otherwise, false.</returns>
        Public Function Contains(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Contains
            Return Internal.ContainsKey(item)
        End Function
        ''' <summary>Copies the elements of the System.Collections.Generic.ICollection(Of T) to an System.Array, starting at a particular System.Array index.</summary>
        ''' <param name="array">The one-dimensional System.Array that is the destination of the elements copied from System.Collections.Generic.ICollection(Of T). The System.Array must have zero-based indexing.</param>
        ''' <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        ''' <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
        ''' <exception cref="System.ArgumentNullException">array is null.</exception>
        ''' <exception cref="System.ArgumentException">array is multidimensional.-or-arrayIndex is equal to or greater than the length of array.-or-The number of elements in the source <see cref="System.Collections.Generic.ICollection(Of T)"/> is greater than the available space from arrayIndex to the end of the destination array.-or-Type T cannot be cast automatically to the type of the destination array.</exception>
        Public Sub CopyTo(ByVal array() As T, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of T).CopyTo
            If arrayIndex < 0 Then Throw New ArgumentOutOfRangeException("arrayIndex", "arrayIndex must be greater than or equal to zero")
            If array Is Nothing Then Throw (New ArgumentNullException("array", "array canot be null"))
            If array.Rank <> 1 Then Throw New ArgumentException("array must be 1-dimensional", "array")
            If arrayIndex >= array.Length OrElse array.Length - arrayIndex < Internal.Count Then Throw New ArgumentException("There is not enough space between arrayIndex and the end of the array for items stored in collection")
            Dim i As Integer = arrayIndex
            For Each itm As KeyValuePair(Of T, Object) In Internal
                array(i) = itm.Key
                i += 1
            Next itm
        End Sub
        ''' <summary>Gets the number of elements contained in the internal <see cref="Hashtable"/>.</summary>
        ''' <returns>The number of elements contained in the internal <see cref="Hashtable"/>.</returns>
        Public ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of T).Count
            Get
                Return Internal.Count
            End Get
        End Property
        ''' <summary>Determines if this collection is read-only or not (always retruns False)</summary>
        ''' <returns>Always False</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of T).IsReadOnly
            Get
                Return False
            End Get
        End Property
        ''' <summary>Removes the first occurrence of a specific object from the internal <see cref="Hashtable"/>.</summary>
        ''' <param name="item">The object to remove from the internal <see cref="Hashtable"/>.</param>
        ''' <returns>true if item was successfully removed from the internal <see cref="Hashtable"/>; otherwise, false. This method also returns false if item is not found in the original internal <see cref="Hashtable"/>.</returns>
        Public Function Remove(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Remove
            If Internal.ContainsKey(item) Then
                Try
                    Internal.Remove(item)
                    Return Not Internal.ContainsKey(item)
                Catch
                    Return False
                End Try
            Else
                Return False
            End If
        End Function
        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="IEnumerator(Of T)"/> that can be used to iterate through the collection</returns>
        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
            Return New KeyValuePairToKeyEnumeratorAdapter(Of Object)(Internal.GetEnumerator)
        End Function

        ''' <summary>Returns an enumerator that iterates through a collection.</summary>
        ''' <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection</returns>
        <Obsolete(), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

        ''' <summary>Adapts <see cref="IEnumerator(Of KeyValuePair(Of T, TValue))"/> into <see cref="IEnumerator(Of T)"/></summary>
        ''' <typeparam name="TVlaue">Type of values stored in <see cref="KeyValuePair(Of T, TVlaue)"/>. Ignored, but must be specified in order the adapter to work in type-safe way</typeparam>
        Public Class KeyValuePairToKeyEnumeratorAdapter(Of TVlaue) : Implements IEnumerator(Of T)
            ''' <summary><see cref="IEnumerator"/> to be adapted</summary>
            Private AdaptThis As IEnumerator(Of KeyValuePair(Of T, TVlaue))
            ''' <summary>CTor</summary>
            ''' <param name="AdaptThis"><see cref="IEnumerator"/> to be adapted</param>
            Public Sub New(ByVal AdaptThis As IEnumerator)
                Me.AdaptThis = AdaptThis
            End Sub
            ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
            ''' <returns>The element in the collection at the current position of the enumerator</returns>
            Public ReadOnly Property Current() As T Implements System.Collections.Generic.IEnumerator(Of T).Current
                Get
                    Return AdaptThis.Current.Key
                End Get
            End Property
            ''' <summary>Gets the current element in the collection.</summary>
            ''' <returns>The current element in the collection.</returns>
            ''' <exception cref="System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
            <Obsolete(), EditorBrowsable(EditorBrowsableState.Never)> _
            Public ReadOnly Property Current1() As Object Implements System.Collections.IEnumerator.Current
                Get
                    Return Current
                End Get
            End Property
            ''' <summary>Advances the enumerator to the next element of the collection.</summary>
            ''' <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
            ''' <exception cref="System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
            Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
                Return AdaptThis.MoveNext
            End Function
            ''' <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
            ''' <exception cref="System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
            Public Sub Reset() Implements System.Collections.IEnumerator.Reset
                AdaptThis.Reset()
            End Sub
            ''' <summary>To detect redundant calls</summary>
            Private disposedValue As Boolean = False
#Region " IDisposable Support "
            ''' <summary><see cref="IDisposable"/></summary>
            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                    End If
                End If
                Me.disposedValue = True
            End Sub
            ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources</summary>
            ''' <remarks>This code added by Visual Basic to correctly implement the disposable pattern.</remarks> 
            Public Sub Dispose() Implements IDisposable.Dispose
                ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
                Dispose(True)
                GC.SuppressFinalize(Me)
            End Sub
#End Region

        End Class

        '''' <summary>Adapts type-safe <see cref="IEnumerator(Of TKey)"/> into type-unsafe <see cref="IEqualityComparer"/></summary>
        'Public Class EqualityComparerAdapter : Implements IEqualityComparer(Of T), IEqualityComparer

        '    ''' <summary><see cref="IEqualityComparer(Of T)"/> to be adapted</summary>
        '    Private TSEqComp As IEqualityComparer(Of T)
        '    ''' <summary>CTor</summary>
        '    ''' <param name="AdaptThis"><see cref="IEqualityComparer(Of T)"/> to be adapted</param>
        '    Public Sub New(ByVal AdaptThis As IEqualityComparer(Of T))
        '        TSEqComp = AdaptThis
        '    End Sub

        '    ''' <summary>Determines whether the specified objects are equal.</summary>
        '    ''' <param name="y">The second object of type T to compare.</param>
        '    ''' <param name="x">The first object of type T to compare.</param>
        '    ''' <returns>true if the specified objects are equal; otherwise, false.</returns>
        '    Public Overloads Function Equals(ByVal x As T, ByVal y As T) As Boolean Implements System.Collections.Generic.IEqualityComparer(Of T).Equals
        '        Return TSEqComp.Equals(x, y)
        '    End Function
        '    ''' <summary>Returns a hash code for the specified object.</summary>
        '    ''' <param name="obj">The <see cref="T"/> for which a hash code is to be returned.</param>
        '    ''' <returns>A hash code for the specified object.</returns>
        '    ''' <exception cref="System.ArgumentNullException">The type of obj is a reference type and obj is null.</exception>
        '    Public Overloads Function GetHashCode(ByVal obj As T) As Integer Implements System.Collections.Generic.IEqualityComparer(Of T).GetHashCode
        '        Return TSEqComp.GetHashCode(obj)
        '    End Function
        '    ''' <summary>Determines whether the specified objects are equal.</summary>
        '    ''' <param name="y">The second object to compare.</param>
        '    ''' <param name="x">The first object to compare.</param>
        '    ''' <returns>true if the specified objects are equal; otherwise, false.</returns>
        '    ''' <exception cref="System.ArgumentException">x and y are of different types and neither one can handle comparisons with the other.</exception>
        '    ''' <remarks>Provided only for calls by objact that this adapter is used by</remarks>
        '    <Obsolete("Provided only for calls by objact that this adapter is used by")> _
        '    Public Overloads Function Equals(ByVal x As Object, ByVal y As Object) As Boolean Implements System.Collections.IEqualityComparer.Equals
        '        If TypeOf x Is T AndAlso TypeOf y Is T Then
        '            Return TSEqComp.Equals(x, y)
        '        Else
        '            Return y.Equals(y)
        '        End If
        '    End Function
        '    ''' <param name="obj">The <see cref="System.Object"/> for which a hash code is to be returned.</param>
        '    ''' <returns>A hash code for the specified object.</returns>
        '    ''' <exception cref="System.ArgumentNullException">The type of <paramref name="obj"/> is a reference type and obj is null.</exception>
        '    ''' <remarks>Provided only for calls by objact that this adapter is used by</remarks>
        '    <Obsolete("Provided only for calls by objact that this adapter is used by")> _
        '    Public Overloads Function GetHashCode(ByVal obj As Object) As Integer Implements System.Collections.IEqualityComparer.GetHashCode
        '        Return TSEqComp.GetHashCode(obj)
        '    End Function
        'End Class
    End Class
End Namespace
#End If