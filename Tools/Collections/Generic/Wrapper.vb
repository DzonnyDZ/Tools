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
    'ASAP:Mark,Forum,Wiki
    ''' <summary>Wraps type-unsafe <see cref="IList"/> as type-safe <see cref="IList(Of T)"/></summary>
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
        'ASAP:Comment members
#Region "IList(Of T)"
        Public Sub Add(ByVal item As T) Implements System.Collections.Generic.ICollection(Of T).Add
            List.Add(item)
        End Sub

        Public Sub Clear() Implements System.Collections.Generic.ICollection(Of T).Clear, System.Collections.IList.Clear
            List.Clear()
        End Sub

        Public Function Contains(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Contains
            Return List.Contains(item)
        End Function

        Public Sub CopyTo(ByVal array() As T, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of T).CopyTo
            Dim i As Integer = arrayIndex
            For Each item As T In Me
                array(i) = item
                i += 1
            Next item
        End Sub

        Public ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of T).Count, System.Collections.ICollection.Count
            Get
                Return List.Count
            End Get
        End Property

        Public ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of T).IsReadOnly, System.Collections.IList.IsReadOnly
            Get
                Return List.IsReadOnly
            End Get
        End Property

        Public Function Remove(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Remove
            Remove = List.IndexOf(item) >= 0
            List.Remove(item)
        End Function

        Public Function IndexOf(ByVal item As T) As Integer Implements System.Collections.Generic.IList(Of T).IndexOf
            Return List.IndexOf(item)
        End Function

        Public Sub Insert(ByVal index As Integer, ByVal item As T) Implements System.Collections.Generic.IList(Of T).Insert
            List.Insert(index, item)
        End Sub

        Default Public Property Item(ByVal index As Integer) As T Implements System.Collections.Generic.IList(Of T).Item
            Get
                Return List(index)
            End Get
            Set(ByVal value As T)
                List(index) = value
            End Set
        End Property

        Public Sub RemoveAt(ByVal index As Integer) Implements System.Collections.Generic.IList(Of T).RemoveAt, System.Collections.IList.RemoveAt
            List.RemoveAt(index)
        End Sub
#End Region
#Region "IList"
        <Obsolete("Use type-safe overload instead")> _
        Private Sub CopyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo
            List.CopyTo(array, index)
        End Sub

        Private ReadOnly Property IsSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
            Get
                Return List.IsSynchronized
            End Get
        End Property

        Private ReadOnly Property SyncRoot() As Object Implements System.Collections.ICollection.SyncRoot
            Get
                Return List.SyncRoot
            End Get
        End Property
        <Obsolete("Use type-safe overload instead")> _
        Private Function Add(ByVal value As Object) As Integer Implements System.Collections.IList.Add
            Add(CType(value, T))
        End Function
        <Obsolete("Use type-safe overload instead")> _
        Private Function Contains(ByVal value As Object) As Boolean Implements System.Collections.IList.Contains
            Return List.Contains(value)
        End Function
        <Obsolete("Use type-safe overload instead")> _
        Private Function IndexOf(ByVal value As Object) As Integer Implements System.Collections.IList.IndexOf
            Return List.IndexOf(value)
        End Function
        <Obsolete("Use type-safe overload instead")> _
        Private Sub Insert(ByVal index As Integer, ByVal value As Object) Implements System.Collections.IList.Insert
            Insert(index, CType(value, T))
        End Sub

        Public ReadOnly Property IsFixedSize() As Boolean Implements System.Collections.IList.IsFixedSize
            Get
                Return List.IsFixedSize
            End Get
        End Property
        <Obsolete("Use type-safe Item")> _
        Private Property UnsafeItem(ByVal index As Integer) As Object Implements System.Collections.IList.Item
            Get
                Return List(index)
            End Get
            Set(ByVal value As Object)
                Me(index) = value
            End Set
        End Property
        <Obsolete("Use type-safe overload instead")> _
        Public Sub Remove(ByVal value As Object) Implements System.Collections.IList.Remove
            List.Remove(value)
        End Sub
#End Region
    End Class
End Namespace
#End If