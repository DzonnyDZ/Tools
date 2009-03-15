Imports system.ComponentModel
''' <summary>Implements FIFO queue that automatically pops old items when capacity limit is reached</summary>
Public Class BlackHoleQueue(Of T)
    Implements ICollection(Of T)
    ''' <summary>Contains value of the <see cref="Queue"/> property</summary>
    Private ReadOnly _Queue As Queue(Of T)
    ''' <summary>CTor</summary>
    ''' <param name="Capacity">Capacity of queue</param>
   Public Sub New(ByVal Capacity As Integer)
        _Queue = New Queue(Of T)(Capacity)
        Me.Capacity = Capacity
    End Sub
    ''' <summary>Contains value of the <see cref="Capacity"/> property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> _
    Private _Capacity As Integer = 0
    ''' <summary>Gets maximum number of items in queue</summary>
    ''' <value>New maximum number of items in queue. 0 means intinity. It there are more items in the queue than allowed by new value, those items will be dequeued.</value>
    ''' <returns>Current capacity of queue.</returns>
    Public Property Capacity() As Integer
        Get
            Return _Capacity
        End Get
        Set(ByVal value As Integer)
            If value >= 0 Then
                _Capacity = value
                Trim()
            Else
                Throw New ArgumentOutOfRangeException("value", value, "Limit must be greater than or equal to zero")
            End If
        End Set
    End Property
    ''' <summary>Dequeues items the exceeds <see cref="Capacity"/></summary>
    Protected Overridable Sub Trim()
        If Capacity > 0 Then
            While Queue.Count > Capacity
                Queue.Dequeue()
            End While
        End If
    End Sub
    ''' <summary>Internal queue</summary>
    ''' <remarks><see cref="Queue(Of T)"/> Items are stored in</remarks>
    Protected ReadOnly Property Queue() As Queue(Of T)
        Get
            Return _Queue
        End Get
    End Property
    ''' <summary>Adds an object to the end of the queue.</summary>
    ''' <param name="item">The object to add to the <see cref="BlackHoleQueue(Of T)"/>. The value can be null for reference types.</param>
    ''' <remarks>Adding item to <see cref="BlackHoleQueue(Of T)"/> can result another item to be dequeued.</remarks>
    Public Overridable Sub Enqueue(ByVal item As T) Implements System.Collections.Generic.ICollection(Of T).Add
        Queue.Enqueue(item)
        Trim()
    End Sub
    ''' <summary>Removes all objects from the <see cref=" BlackHoleQueue(Of T)"/>.</summary>
    Public Overridable Sub Clear() Implements System.Collections.Generic.ICollection(Of T).Clear
        Queue.Clear()
    End Sub
    ''' <summary>Determines whether an element is in the <see cref="BlackHoleQueue(Of T)"/>.</summary>
    ''' <param name="item">The object to locate in the <see cref="BlackHoleQueue(Of T)"/>. The value can be null for reference types.</param>
    ''' <returns>true if item is found in the <see cref="BlackHoleQueue(Of T)"/>; otherwise, false.</returns>
    Public Overridable Function Contains(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Contains
        Return Queue.Contains(item)
    End Function
    ''' <summary>Copies the <see cref="BlackHoleQueue(Of T)"/> elements to an existing one-dimensional <see cref="System.Array"/>, starting at the specified array index.</summary>
    ''' <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from <see cref="BlackHoleQueue(Of T)"/>. The <see cref="System.Array"/> must have zero-based indexing.</param>
    ''' <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    ''' <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.</exception>
    ''' <exception cref="System.ArgumentNullException"><paramref name="array"/> is null.</exception>
    ''' <exception cref="System.ArgumentException"><paramref name="index"/>is equal to or greater than the length of <paramref name="array"/>.-or-The number of elements in the source <see cref="BlackHoleQueue(Of T)"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.</exception>
    Public Overridable Sub CopyTo(ByVal array() As T, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of T).CopyTo
        Queue.CopyTo(array, arrayIndex)
    End Sub
    ''' <summary>Gets the number of elements contained in the <see cref="BlackHoleQueue(Of T)"/>.</summary>
    ''' <returns>The number of elements contained in the <see cref="BlackHoleQueue(Of T)"/></returns>
    Public Overridable ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of T).Count
        Get
            Return Queue.Count
        End Get
    End Property
    ''' <summary>Gets a value indicating whether the <see cref="System.Collections.Generic.ICollection(Of T)"/> is read-only.</summary>
    ''' <returns>false</returns>
    Private ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of T).IsReadOnly
        Get
            Return False
        End Get
    End Property
    ''' <summary>Removes the first occurrence of a specific object from the <see cref="System.Collections.Generic.ICollection(Of T)"/>.</summary>
    ''' <param name="item">The object to remove from the <see cref="System.Collections.Generic.ICollection(Of T)"/>.</param>
    ''' <returns>true if item was successfully removed from the <see cref="System.Collections.Generic.ICollection(Of T)"/>; otherwise, false. This method also returns false if item is not found in the original <see cref="System.Collections.Generic.ICollection(Of T)"/>.</returns>
    ''' <remarks>This method is not optimized for using! It is here only in order to implement <see cref="ICollection(Of T)"/> and for convenience when deriving from this class. It is not intended to be used (however it works).</remarks>
    <Obsolete("This method is not optimized for using. It is here only in order to implement ICollection`1 and for convenience when deriving from this class. It is not intended to be used (however it works).")> _
    Protected Overridable Function Remove(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Remove
        Dim List As New List(Of T)(Me)
        Dim ret As Boolean = List.Remove(item)
        Queue.Clear()
        For Each lItem As T In List
            Queue.Enqueue(lItem)
        Next lItem
        Return ret
    End Function
    ''' <summary>Returns an enumerator that iterates through the <see cref="BlackHoleQueue(Of T)"/>.</summary>
    ''' <returns>An <see cref="System.Collections.Generic.Queue(Of T).Enumerator"/> for the <see cref="BlackHoleQueue(Of T)"/>.</returns>
    Public Overridable Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
        Return Queue.GetEnumerator
    End Function
    ''' <summary>Returns an enumerator that iterates through a collection.</summary>
    ''' <returns>An <see cref="System.Collections.IEnumerator"/> object that can be used to iterate through the collection.</returns>
    ''' <remarks>Use type-safe <see cref="GetEnumerator"/> instead.</remarks>
    <Obsolete("Use type-safe GetEnumerator instead")> _
    Private Function _GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return GetEnumerator()
    End Function
    ''' <summary>Removes and returns the object at the beginning of the <see cref="BlackHoleQueue(Of T)"/>.</summary>
    ''' <returns>The object that is removed from the beginning of the <see cref="BlackHoleQueue(Of T)"/>. This is the oldest item in the queue that haven't been dequeued (automatically due to excess of <see cref="Capacity"/> or manually using <see cref="Dequeue"/>) yet.</returns>
    ''' <exception cref="System.InvalidOperationException">The <see cref="BlackHoleQueue(Of T)"/> is empty.</exception>
    Public Overridable Function Dequeue() As T
        Return Queue.Dequeue
    End Function
End Class
