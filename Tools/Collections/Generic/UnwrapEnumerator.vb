#If True
Namespace CollectionsT.GenericT
    ''' <summary>Enumerator that unwraps its current value form furrent value of another enumerator</summary>
    ''' <typeparam name="TEnvelope">Type of item in base enumerator</typeparam>
    ''' <typeparam name="TItem">Type of item of this enumerator</typeparam>
    ''' <remarks>This enumerator utilizes unwrapping function</remarks>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public Class UnwrapEnumerator(Of TEnvelope, TItem)
        Implements IEnumerator(Of TItem)
        ''' <summary>Base enumerator</summary>
        Private Base As IEnumerator(Of TEnvelope)
        ''' <summary>Unwrapping function</summary>
        Private Unwrap As Func(Of TEnvelope, TItem)
        ''' <summary>CTor</summary>
        ''' <param name="Base">Base enumerator - the enumerator to unwrap value from items of</param>
        ''' <param name="Unwrap">Unwrapping function</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Base"/> or <paramref name="Unwrap"/> is null</exception>
        Public Sub New(ByVal Base As IEnumerator(Of TEnvelope), ByVal Unwrap As Func(Of TEnvelope, TItem))
            If Base Is Nothing Then Throw New ArgumentNullException("Base")
            If Unwrap Is Nothing Then Throw New ArgumentNullException("Unwrap")
            Me.Base = Base
            Me.Unwrap = Unwrap
        End Sub


        ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        ''' <returns>The element in the collection at the current position of the enumerator.</returns>
        Public ReadOnly Property Current() As TItem Implements System.Collections.Generic.IEnumerator(Of TItem).Current
            Get
                Return Unwrap.Invoke(CurrentEnvelope)
            End Get
        End Property
        ''' <summary>Gets current element in base enumerator</summary>
        ''' <returns>Current element in base enumerator</returns>
        Public ReadOnly Property CurrentEnvelope() As TEnvelope
            Get
                Return Base.Current
            End Get
        End Property

        ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        ''' <returns>The element in the collection at the current position of the enumerator.</returns>
        Public ReadOnly Property IEnumerator_Current() As Object Implements System.Collections.IEnumerator.Current
            Get
                Return Current
            End Get
        End Property

        ''' <summary>Advances the enumerator to the next element of the collection.</summary>
        ''' <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
        ''' <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
        ''' <filterpriority>2</filterpriority>
        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            Return Base.MoveNext
        End Function

        ''' <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        ''' <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
        ''' <filterpriority>2</filterpriority>
        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            Base.Reset()
        End Sub
#Region " IDisposable Support "
        ''' <summary>To detect redundant calls</summary>
        Private disposedValue As Boolean = False

        ''' <summary>Implements <see cref="IDisposable"/></summary>
        ''' <param name="disposing">Disposing value</param>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    Base.Dispose()
                End If
            End If
            Me.disposedValue = True
        End Sub


        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.                </summary>
        ''' <filterpriority>2</filterpriority>
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace
#End If

