Namespace CollectionsT.GenericT
#If True
    'ASAP: Forum, Mark , Wiki

    ''' <summary>Filterg given <see cref="IEnumerator(Of T)"/> with given <see cref="Predicate(Of T)"/></summary>
    Public Class FilteredEnumerator(Of T)
        Implements IEnumerator(Of T)
        ''' <summary>Filtered <see cref="IEnumerator(Of T)"/></summary>
        Private internal As IEnumerator(Of T)
        ''' <summary>Predicate filter. Include only items for which predicate is true</summary>
        Private Filter As Predicate(Of T)

        ''' <summary>CTor</summary>
        ''' <param name="Other">The <see cref="IEnumerator(Of T)"/> to be filtered</param>
        ''' <param name="Filter">Filter predicate. Only items for which predicate is True are included in filtered enumeration</param>
        Public Sub New(ByVal Other As IEnumerator(Of T), ByVal Filter As Predicate(Of T))
            internal = Other
            Me.Filter = Filter
        End Sub
        ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        ''' <returns>The element in the collection at the current position of the enumerator.</returns>
        ''' <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.-or- The collection was modified after the enumerator was created.</exception>
        Public ReadOnly Property Current() As T Implements System.Collections.Generic.IEnumerator(Of T).Current
            Get
                Return internal.Current
            End Get
        End Property

        ''' <summary>Gets the current element in the collection.</summary>
        ''' <returns>The current element in the collection.</returns>
        ''' <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.-or- The collection was modified after the enumerator was created.</exception>
        Private ReadOnly Property _Current() As Object Implements System.Collections.IEnumerator.Current
            Get
                Return Current
            End Get
        End Property
        
        ''' <summary>Advances the enumerator to the next element of the collection while exluding elements for which predicate given in CTor is false.</summary>
        ''' <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
        ''' <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            While internal.MoveNext
                If Filter(internal.Current) Then Return True
            End While
            Return False
        End Function

        ''' <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        ''' <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            internal.Reset()
        End Sub

#Region " IDisposable Support "
        ''' <summary>To detect redundant calls</summary>
        Private disposedValue As Boolean = False

        ''' <summary>This code added by Visual Basic to correctly implement the disposable pattern.</summary>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    internal.Dispose()
                    Filter = Nothing
                    internal = Nothing
                End If
            End If
            Me.disposedValue = True
        End Sub

        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        ''' <remarks>Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.</remarks>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
#End If
End Namespace
