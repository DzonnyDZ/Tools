Namespace Collections.Generic
#If Config <= Release Then
    ''' <summary>Rapresent anything that can be indexed by anything</summary>
    ''' <typeparam name="TIndex">Data type of indexes</typeparam>
    ''' <typeparam name="TItem">Datatype of items</typeparam>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(IIndexable(Of Byte, Byte)), LastChMMDDYYYY:="04/23/2007")> _
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
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(IReadOnlyIndexable(Of Byte, Byte)), LastChMMDDYYYY:="04/23/2007")> _
    Public Interface IReadOnlyIndexable(Of TItem, TIndex)
        ''' <summary>Gets value on specified index</summary>
        ''' <param name="index">Index to obtain value</param>
        ''' <returns>value lying on specified <paramref name="index"/></returns>
        ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
        Default ReadOnly Property Item(ByVal index As TIndex) As TItem
    End Interface
    ''' <summary>Represents anythign that can be indexed by <see cref="Long"/></summary>
    ''' <typeparam name="TItem">Data type of items</typeparam>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(IIndexable(Of Byte)), LastChMMDDYYYY:="04/23/2007")> _
    Public Interface IIndexable(Of TItem)
        Inherits IReadOnlyIndexable(Of TItem)
        Inherits IEnumerable(Of TItem)
    End Interface
    ''' <summary>Represents anythign that can be indexed by <see cref="Long"/> for readonly acces</summary>
    ''' <typeparam name="TItem">Data type of items</typeparam>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(IReadOnlyIndexable(Of Byte)), LastChMMDDYYYY:="04/23/2007")> _
    Public Interface IReadOnlyIndexable(Of TItem)
        Inherits IReadOnlyIndexable(Of TItem, Long)
        Inherits IEnumerable(Of TItem)
        ''' <summary>Minimal valid value for index</summary>
        ReadOnly Property Minimum() As Long
        ''' <summary>Maximal valid value for index</summary>
        ReadOnly Property Maximum() As Long
    End Interface
#End If
#If Config <= Beta Then 'Stage: Beta
    ''' <summary>Implements enumerator of <see cref="IReadOnlyIndexable(Of TItem)"/></summary>
    Public Class IndexableEnumerator(Of TItem)
        Implements IEnumerator(Of TItem)
        ''' <summary><see cref="IReadOnlyIndexable(Of TItem)"/> being enumerated</summary>
        Private Collection As IReadOnlyIndexable(Of TItem)
        ''' <summary>Curent position</summary>
        Private Position As Long
        ''' <summary>CTor</summary>
        ''' <param name="Collection"><see cref="IReadOnlyIndexable(Of TItem)"/> to enumerate through</param>
        Public Sub New(ByVal Collection As IReadOnlyIndexable(Of TItem))
            Me.Collection = Collection
            Position = Collection.Minimum - 1
        End Sub
        ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        ''' <returns>The element in the collection at the current position of the enumerator.</returns>
        ''' <exception cref="System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
        Public ReadOnly Property Current() As TItem Implements System.Collections.Generic.IEnumerator(Of TItem).Current
            Get
                If Position < Collection.Minimum OrElse Position > Collection.Maximum Then Throw New InvalidOperationException("Enumerator is positioned outside IReadOnlyIndexable bounds")
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
            Position += 1
            Return Position <= Collection.Maximum
        End Function
        ''' <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            Position = Collection.Minimum - 1
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
#End If
End Namespace

