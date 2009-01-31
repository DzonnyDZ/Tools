#If Config <= Nightly Then 'Stage: Nightly
'#If Framework >= 3.5 Then
Imports System.Linq
'#End If
Namespace CollectionsT.GenericT
    'ASAP: Mark wiki forum
    ''' <summary>Performs union operations for <see cref="IEnumerable(Of T)"/>s</summary>
    ''' <typeparam name="T">Type of item</typeparam>
    Public Class UnionEnumerable(Of T)
        Implements IEnumerable(Of T)
        ''' <summary><see cref="IEnumerable(Of T)"/>s this instance seems to be <see cref="IEnumerable(Of T)"/> of item of</summary>
        Private Enumerables As IEnumerable(Of IEnumerable(Of T))
        ''' <summary>CTor</summary>
        ''' <param name="Enumerables"><see cref="IEnumerable(Of T)"/>s to be unionized</param>
        ''' <version version="1.5.2">Now it is sfe to pass null to <paramref name="Enumerables"/>.</version>
        Public Sub New(ByVal Enumerables As IEnumerable(Of IEnumerable(Of T)))
            If Enumerables Is Nothing Then Enumerables = New List(Of IEnumerable(Of T))
            Me.Enumerables = Enumerables
        End Sub
        ''' <summary>CTor</summary>
        ''' <param name="Enumerables"><see cref="IEnumerable(Of T)"/>s to be unionized</param>
        Public Sub New(ByVal ParamArray Enumerables As IEnumerable(Of T)())
            Me.New(DirectCast(Enumerables, IEnumerable(Of IEnumerable(Of T))))
        End Sub

        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
            Return New UnionEnumerator(Of T)(Enumerables)
        End Function
        ''' <summary>Returns an enumerator that iterates through a collection.</summary>
        ''' <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        ''' <remarks>Use type-safe GetEnumerator instead</remarks>
        <Obsolete("Use type-safe GetEnumerator instead")> _
        Private Function _GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function
    End Class

    ''' <summary>Implements <see cref="IEnumerator(Of T)"/> tha unifies more <see cref="IEnumerator(Of T)"/>s</summary>
    ''' <typeparam name="T">Type of item</typeparam>
    Public Class UnionEnumerator(Of T)
        Implements IEnumerator(Of T)
        ''' <summary>Enumerators to unionize are enumerated through this enumerator</summary>
        Private Enumerators As IEnumerator(Of IEnumerator(Of T))
        ''' <summary>CTor from array of <see cref="IEnumerator(Of T)"/></summary>
        ''' <param name="Enumerators">Array of enumerators to union</param>
        Public Sub New(ByVal ParamArray Enumerators As IEnumerator(Of T)())
            Me.New(CType(Enumerators, IEnumerable(Of IEnumerator(Of T))))
        End Sub
        ''' <summary>CTor from any <see cref="IEnumerable(Of IEnumerator(Of T))"/></summary>
        ''' <param name="Enumerators">Enumerators to union</param>
        Public Sub New(ByVal Enumerators As IEnumerable(Of IEnumerator(Of T)))
            Me.Enumerators = Enumerators.GetEnumerator
            Me.Enumerators.MoveNext()
        End Sub
        ''' <summary>CTor from array of <see cref="IEnumerable(Of T)"/></summary>
        ''' <param name="Enumerables">Array of enumerables to get enumerators from and union them</param>
        Public Sub New(ByVal ParamArray Enumerables As IEnumerable(Of T)())
            Me.New(CType(Enumerables, IEnumerable(Of IEnumerable(Of T))))
        End Sub
        ''' <summary>CTor from any <see cref="IEnumerable(Of IEnumerable(Of T))"/></summary>
        ''' <param name="Enumerables">Enumerables to get enumerators from and union them</param>
        Public Sub New(ByVal Enumerables As IEnumerable(Of IEnumerable(Of T)))
            '#If Framework >= 3.5 Then
            Me.New(From Enumerable In Enumerables Select Enumerable.GetEnumerator)
            '#Else
            '            Dim lst As New List(Of IEnumerator(Of T))
            '            For Each Enumerable In Enumerables
            '                lst.Add(Enumerable.GetEnumerator)
            '            Next Enumerable
            '            Me.Enumerators = lst.GetEnumerator
            '            Me.Enumerators.MoveNext()
            '#End If
        End Sub


        ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        ''' <returns>The element in the collection at the current position of the enumerator.</returns>
        ''' <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.-or- The collection was modified after the enumerator was created.</exception>
        Public ReadOnly Property Current() As T Implements System.Collections.Generic.IEnumerator(Of T).Current
            Get
                Return Enumerators.Current.Current
            End Get
        End Property
        ''' <summary>Gets the current element in the collection.</summary>
        ''' <returns>The current element in the collection.</returns>
        ''' <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.-or- The collection was modified after the enumerator was created.</exception>
        <Obsolete("Use type-safe Current instead")> _
        Private ReadOnly Property _Current() As Object Implements System.Collections.IEnumerator.Current
            Get
                Return Current
            End Get
        End Property

        ''' <summary>Advances the enumerator to the next element of the collection.</summary>
        ''' <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
        ''' <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            If Enumerators.Current.MoveNext Then
                Return True
            ElseIf Enumerators.MoveNext Then
                Return MoveNext()
            Else
                Return False
            End If
        End Function

        ''' <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        ''' <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            Enumerators.Reset()
            While Enumerators.MoveNext
                Enumerators.Current.Reset()
            End While
            Enumerators.Reset()
            Enumerators.MoveNext()
        End Sub



#Region " IDisposable Support "
        ''' <summary>To detect redundant calls</summary>
        Private disposedValue As Boolean = False

        ''' <summary>This code added by Visual Basic to correctly implement the disposable pattern.</summary>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    Enumerators.Dispose()
                End If
            End If
            Me.disposedValue = True
        End Sub

        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace
#End If