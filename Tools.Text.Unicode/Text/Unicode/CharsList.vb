Imports Tools.CollectionsT.GenericT
Imports Tools.LinqT
Imports System.Collections.Specialized
Imports Tools.TextT.UnicodeT

Namespace TextT.UnicodeT

    ''' <summary>An abstract base class for sources of characters</summary>
    ''' <remarks>
    ''' This class implements <see cref="IList(Of T)"/> - but only it's readonly part (though derived class may support read-write access).
    ''' <note type="inheritinfo">If derived class implements also write access for the collection, consider implementing <see cref="INotifyCollectionChanged"/>.</note>
    ''' </remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <CLSCompliant(False)>
    Public MustInherit Class CharsList
        Implements IList(Of UInteger), IReadOnlyIndexableCollection(Of UInteger, Integer)

#Region "Abstract"
        ''' <summary>When overriden in derived class gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
        ''' <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        Public MustOverride ReadOnly Property Count As Integer Implements System.Collections.Generic.ICollection(Of UInteger).Count, IReadOnlyCollection(Of UInteger).Count
        ''' <summary>When overriden in derived class gets the element at the specified index.</summary>
        ''' <returns>The element at the specified index.</returns>
        ''' <param name="index">The zero-based index of the element to get or set.</param>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.</exception>
        Default Public MustOverride ReadOnly Property Item(index As Integer) As UInteger Implements IReadOnlyIndexable(Of UInteger, Integer).Item
        ''' <summary>When overriden in derived class gets value indicating if this collection is guaranteed to contain only characters that folllow one each other without any gaps</summary>
        Public MustOverride ReadOnly Property Continuous As Boolean
#End Region

#Region "Default implementation"
        ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
        ''' <returns>true if  <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
        ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        ''' <remarks><note type="ineritinfo">This implementation wlaks entire collection untill first occurence of <paramref name="item"/> is found. Derived class should provide more effective implementation.</note></remarks>
        ''' <seelaso cref="IndexOf"/>
        Public Overridable Function Contains(item As UInteger) As Boolean Implements System.Collections.Generic.ICollection(Of UInteger).Contains
            For Each itm In Me
                If item = item Then Return True
            Next
            Return False
        End Function

        ''' <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        ''' <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        ''' <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is null.</exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex" /> is less than 0 or greater than or equal to length of <paramref name="array"/>.</exception>
        ''' <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
        Public Overridable Sub CopyTo(array() As UInteger, arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of UInteger).CopyTo, IReadOnlyCollection(Of UInteger).CopyTo
            If array Is Nothing Then Throw New ArgumentNullException("array")
            If arrayIndex < array.GetLowerBound(0) OrElse arrayIndex > array.GetUpperBound(0) Then Throw New ArgumentOutOfRangeException("arrayIndex")
            If Not (array.Length - arrayIndex > Me.Count) Then Throw New ArgumentException(TextT.UnicodeT.UnicodeResources.ex_CollectionCopyToSmallArray)
            Dim i% = 0
            For Each value In Me
                array(arrayIndex + i) = value
                i += 1
            Next
        End Sub

        ''' <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</summary>
        ''' <returns>This implementation always returns true.</returns>
        ''' <remarks><note type="inheritinfo">In case derived class overrides this method to return false, it shall override write-access methods.</note></remarks>
        Protected Overridable ReadOnly Property IsReadOnly As Boolean Implements System.Collections.Generic.ICollection(Of UInteger).IsReadOnly
            Get
                Return True
            End Get
        End Property

        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        ''' <remarks><note type="inheritinfo">This implementation returns <see cref="IndexableEnumerator(Of TIndex, TItem)"/>. Derived class may opt for retruning more efficient implementation</note></remarks>
        ''' <filterpriority>1</filterpriority>
        Public Overridable Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of UInteger) Implements System.Collections.Generic.IEnumerable(Of UInteger).GetEnumerator
            Return New IndexableEnumerator(Of Integer, UInteger)(New ForLoopCollection(Of Integer)(0, Count - 1, Function(i) i), Me)
        End Function

        ''' <summary>Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.</summary>
        ''' <returns>The index of 
        ''' <paramref name="item" /> if found in the list; otherwise, -1.</returns>
        ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.</param>
        ''' <remarks><note type="ineritinfo">This implementation wlaks entire collection untill first occurence of <paramref name="item"/> is found. Derived class should provide more effective implementation.</note></remarks>
        ''' <seelaso cref="Contains"/>
        Public Overridable Function IndexOf(item As UInteger) As Integer Implements System.Collections.Generic.IList(Of UInteger).IndexOf
            Dim i% = 0
            For Each xi In Me
                If xi = item Then Return i
                i += 1
            Next
            Return -1
        End Function

        ''' <summary>Returns an enumerator that iterates through a collection.</summary>
        ''' <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        ''' <filterpriority>2</filterpriority>
        Private Function IEnumerable_GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function
#End Region

#Region "NotSupported"
        ''' <summary>When overriden in derived class adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
        ''' <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        ''' <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only (this implementation always throws this exception).</exception>
        Protected Overridable Sub Add(item As UInteger) Implements System.Collections.Generic.ICollection(Of UInteger).Add
            Throw New NotSupportedException
        End Sub

        ''' <summary>When overriden in derived class removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
        ''' <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only (this implementation always throws this exception).</exception>
        Protected Overridable Sub Clear() Implements System.Collections.Generic.ICollection(Of UInteger).Clear
            Throw New NotSupportedException
        End Sub
        ''' <summary>When overriden in derived class removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
        ''' <returns>true if 
        ''' <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if 
        ''' <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        ''' <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        ''' <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only (this implementation always throws this exception).</exception>
        Protected Overridable Function Remove(item As UInteger) As Boolean Implements System.Collections.Generic.ICollection(Of UInteger).Remove
            Throw New NotSupportedException
        End Function
        ''' <summary>When overriden in derived class removes the <see cref="T:System.Collections.Generic.IList`1" /> item at the specified index.</summary>
        ''' <param name="index">The zero-based index of the item to remove.</param>
        ''' <exception cref="T:System.ArgumentOutOfRangeException">
        ''' <paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.</exception>
        ''' <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1" /> is read-only (this implementation always throws this exception).</exception>
        Protected Overridable Sub RemoveAt(index As Integer) Implements System.Collections.Generic.IList(Of UInteger).RemoveAt
            Throw New NotSupportedException
        End Sub
        ''' <summary>When overriden in derived class inserts an item to the <see cref="T:System.Collections.Generic.IList`1" /> at the specified index.</summary>
        ''' <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        ''' <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1" />.</param>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.</exception>
        ''' <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1" /> is read-only (this implementation always throws this exception).</exception>
        Protected Sub Insert(index As Integer, item As UInteger) Implements System.Collections.Generic.IList(Of UInteger).Insert
            Throw New NotSupportedException
        End Sub
        ''' <summary>Gets or, when overriden in derived class, sets the element at the specified index.</summary>
        ''' <returns>The element at the specified index.</returns>
        ''' <param name="index">The zero-based index of the element to get or set.</param>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.</exception>
        ''' <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.Generic.IList`1" /> is read-only (this implementation always throws this exception when property is set).</exception>
        ''' <remarks><note type="inheritinfo">This property implements <see cref="P:System.COllections.Generic.IList`1.Item"/>. Getter of this property calls <see cref="Item"/>.</note></remarks>
        ''' <seelaso cref="Item"/>
        Protected Overridable Property IList_Item(index As Integer) As UInteger Implements System.Collections.Generic.IList(Of UInteger).Item
            Get
                Return Item(index)
            End Get
            Set(value As UInteger)
                Throw New NotSupportedException
            End Set
        End Property
#End Region


        Private Shared _empty As CharsList = New CharsArray(New UInteger() {})
        ''' <summary>Gets an empty <see cref="CharsList"/></summary>
        Public Shared ReadOnly Property Empty As CharsList
            Get
                Return _empty
            End Get
        End Property
    End Class


    ''' <summary>Implements array-backed <see cref="CharsList"/></summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <CLSCompliant(False)>
    Public Class CharsArray
        Inherits CharsList
        ''' <summary>The array</summary>
        Private ReadOnly array As UInteger()
        ''' <summary>CTor - creates a new instance of the <see cref="CharsArray"/> class</summary>
        ''' <param name="array">The array that contains characters new instance will provide</param>
        ''' <exception cref="ArgumentNullException"><paramref name="array"/> is null</exception>
        Public Sub New(array As UInteger())
            If array Is Nothing Then Throw New ArgumentNullException("array")
            Me.array = array
        End Sub

        ''' <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
        ''' <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        Public Overrides ReadOnly Property Count As Integer
            Get
                Return array.Length
            End Get
        End Property

        ''' <summary>Gets the element at the specified index.</summary>
        ''' <returns>The element at the specified index.</returns>
        ''' <param name="index">The zero-based index of the element to get or set.</param>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.</exception>
        Default Public Overrides ReadOnly Property Item(index As Integer) As UInteger
            Get
                Return array(index)
            End Get
        End Property

        ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
        ''' <returns>true if  <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
        ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        ''' <seelaso cref="IndexOf"/>
        Public Overrides Function Contains(item As UInteger) As Boolean
            Return System.Array.IndexOf(array, item) >= 0
        End Function
        ''' <summary>Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.</summary>
        ''' <returns>The index of 
        ''' <paramref name="item" /> if found in the list; otherwise, -1.</returns>
        ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.</param>
        ''' <seelaso cref="Contains"/>
        Public Overrides Function IndexOf(item As UInteger) As Integer
            Return System.Array.IndexOf(array, item)
        End Function
        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        ''' <filterpriority>1</filterpriority>
        Public Overrides Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of UInteger)
            Return array.GetEnumerator
        End Function
        ''' <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        ''' <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        ''' <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is null.</exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex" /> is less than 0 or greater than or equal to length of <paramref name="array"/>.</exception>
        ''' <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
        Public Overrides Sub CopyTo(array() As UInteger, arrayIndex As Integer)
            Me.array.CopyTo(array, arrayIndex)
        End Sub


        ''' <summary>Gets value indicating if this collection is guaranteed to contain only characters that folllow one each other without any gaps</summary>
        ''' <returns>This implementation returns true only if <see cref="Count"/> is 0 or 1.</returns>
        Public Overrides ReadOnly Property Continuous As Boolean
            Get
                Return array.Length <= 1
            End Get
        End Property
    End Class

    ''' <summary>Implements character-block backed <see cref="CharsList"/></summary>
    ''' <remarks>This collection is virtual</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <CLSCompliant(False)>
    Public Class CharsRange
        Inherits CharsList
        Implements IEnumerator(Of UInteger), ICloneable(Of CharsRange)

        Private ReadOnly _start As UInteger
        Private ReadOnly _end As UInteger
        ''' <summary>CTor - creates a new instance of the <see cref="CharsRange"/> that contains all Unicode code-points (including surrogates)</summary>
        Public Sub New()
            Me.New(0UI, UnicodeCharacterDatabase.MaxCodePoint)
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="CharsRange"/> form given character range</summary>
        ''' <param name="first">First character in range</param>
        ''' <param name="last">Last character in range</param>
        ''' <exception cref="ArgumentException"><paramref name="last"/> is less than <paramref name="first"/></exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="last"/> is greater than <see cref="UnicodeCharacterDatabase.MaxCodePoint"/></exception>
        Public Sub New(first As UInteger, last As UInteger)
            If first > last Then Throw New ArgumentException(TextT.UnicodeT.UnicodeResources.ex_FirstLastCharSwapped)
            If last > UnicodeCharacterDatabase.MaxCodePoint Then Throw New ArgumentOutOfRangeException("last")
            _start = first
            _end = last
        End Sub

        ''' <summary>Gets code if first code-point in range</summary>
        Public ReadOnly Property Start() As UInteger
            Get
                Return _start
            End Get
        End Property
        ''' <summary>Gets code of last code-point in range</summary>
        Public ReadOnly Property [End]() As UInteger
            Get
                Return _end
            End Get
        End Property
        ''' <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
        ''' <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        Public Overrides ReadOnly Property Count As Integer
            Get
                Return [End] - Start + 1
            End Get
        End Property

        ''' <summary>Gets the element at the specified index.</summary>
        ''' <returns>The element at the specified index.</returns>
        ''' <param name="index">The zero-based index of the element to get or set.</param>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1" />.</exception>
        Default Public Overrides ReadOnly Property Item(index As Integer) As UInteger
            Get
                If index < 0 Then Throw New IndexOutOfRangeException()
                Dim ret = Start + CUInt(index)
                If ret > [End] Then Throw New IndexOutOfRangeException
                Return ret
            End Get
        End Property


        ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
        ''' <returns>true if  <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
        ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        ''' <seelaso cref="IndexOf"/>
        Public Overrides Function Contains(item As UInteger) As Boolean
            Return item >= Start AndAlso item <= [End]
        End Function
        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        ''' <remarks>Returns clone of this instance.</remarks>
        ''' <filterpriority>1</filterpriority>
        Public Overrides Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of UInteger)
            Return Clone()
        End Function
        ''' <summary>Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.</summary>
        ''' <returns>The index of 
        ''' <paramref name="item" /> if found in the list; otherwise, -1.</returns>
        ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.</param>
        ''' <seelaso cref="Contains"/>
        Public Overrides Function IndexOf(item As UInteger) As Integer
            If item < Start OrElse item > [End] Then Return -1
            Return item - Start
        End Function

#Region "IEnumerator"
        Private _current As UInteger?
        ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        ''' <returns>The element in the collection at the current position of the enumerator.</returns>
        Private ReadOnly Property Current As UInteger Implements System.Collections.Generic.IEnumerator(Of UInteger).Current
            Get
                If Me.disposedValue Then Throw New ObjectDisposedException([GetType].Name)
                If _current.HasValue AndAlso _current.Value <= [End] Then Return _current
                Throw New InvalidOperationException("Enumeration has not started yet, or it has already ended")
            End Get
        End Property

        ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        ''' <returns>The element in the collection at the current position of the enumerator.</returns>
        Private ReadOnly Property IEnumerator_Current As Object Implements System.Collections.IEnumerator.Current
            Get
                Return Current
            End Get
        End Property

        ''' <summary>Advances the enumerator to the next element of the collection.</summary>
        ''' <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
        ''' <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        ''' <filterpriority>2</filterpriority>
        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            If Me.disposedValue Then
                Throw New ObjectDisposedException([GetType].Name)
            ElseIf _current Is Nothing Then
                _current = Start
                Return True
            ElseIf _current.Value < [End] Then
                _current += 1UI
                Return True
            ElseIf _current.Value = [End] Then
                _current += 1UI
                Return False
            Else
                Return False
            End If
        End Function

        ''' <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        ''' <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        ''' <filterpriority>2</filterpriority>
        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            If Me.disposedValue Then Throw New ObjectDisposedException([GetType].Name)
            _current = Nothing
        End Sub

#Region "IDisposable Support"
        ''' <summary>To detect redundant calls</summary>
        Private disposedValue As Boolean

        ''' <summary>Implements <see cref="IDisposable.Dispose"/></summary>
        ''' <param name="disposing">True when disposing</param>
        Protected Overridable Sub Dispose(disposing As Boolean)
            Me.disposedValue = True
        End Sub

        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        ''' <filterpriority>2</filterpriority>
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
#End Region

        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Private Function ICloneable_Clone() As Object Implements System.ICloneable.Clone
            Return Clone()
        End Function

        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        Public Overridable Function Clone() As CharsRange Implements ICloneable(Of CharsRange).Clone
            Return New CharsRange(Start, [End])
        End Function


        ''' <summary>Gets value indicating if this collection is guaranteed to contain only characters that folllow one each other without any gaps</summary>
        ''' <returns>This implementation always returns true</returns>
        Public Overrides ReadOnly Property Continuous As Boolean
            Get
                Return True
            End Get
        End Property
    End Class
End Namespace