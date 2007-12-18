#If Config <= Nightly Then 'Stage: Nightly
Namespace CollectionsT.GenericT
    'ASAP: Mark, WiKi, Forum
    ''' <summary>Implements type-safe <see cref="IEnumerator(Of T)"/> for 1-dimensional array of any type</summary>
    ''' <typeparam name="T">Type of array element</typeparam>
    ''' <remarks>Supports arrays with non-zero lower bound.</remarks>
    Public NotInheritable Class TypedArrayEnumerator(Of T)
        Implements IBiDirEnumerator(Of T)
        ''' <summary>Array to enumerate through</summary>
        Private Array As T()
        ''' <summary>Current index into array</summary>
        Private Index As Integer
        ''' <summary>True if enumeration goes from end to start of an array</summary>
        Private Inverse As Boolean
        ''' <summary>CTor</summary>
        ''' <param name="Array">Array to enumerate through</param>
        ''' <param name="Inverse">True if enumeration should be done from end to beginning of an array</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Array"/> is null</exception>
        Public Sub New(ByVal Array As T(), Optional ByVal Inverse As Boolean = False)
            If Array Is Nothing Then Throw New ArgumentNullException("Array", "Array cannot be null.")
            Me.Array = Array
            Me.Inverse = Inverse
            Index = If(Inverse, Array.GetUpperBound(0) + 1, Array.GetLowerBound(0) - 1)
        End Sub

        ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        ''' <returns>The element in the collection at the current position of the enumerator.</returns>
        ''' <exception cref="InvalidOperationException">Enumeration has not started yet or it has already finished.</exception>
        Public ReadOnly Property Current() As T Implements System.Collections.Generic.IEnumerator(Of T).Current
            Get
                If Index >= Array.GetLowerBound(0) AndAlso Index <= Array.GetUpperBound(0) Then
                    Return Array(Index)
                Else
                    Throw New InvalidOperationException("Enumerator is either not initialized yed or enumeration has already finished")
                End If
            End Get
        End Property
        ''' <summary>Gets the current element in the collection.</summary>
        ''' <returns>The current element in the collection.</returns>
        ''' <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
        ''' <remarks>Use type-safe <see cref="Current"/> instead</remarks>
        <Obsolete("Use type-safe Current instead")> _
        Private ReadOnly Property _Current() As Object Implements System.Collections.IEnumerator.Current
            Get
                Return Current
            End Get
        End Property
        ''' <summary>Gets current index into array the enumerator points to</summary>
        ''' <remarks>Index may be in range <see cref="System.Array.GetLowerBound"/> - 1 to <see cref="System.Array.GetUpperBound"/> + 1</remarks>
        Public ReadOnly Property CurrentIndex() As Integer
            Get
                Return Index
            End Get
        End Property


        ''' <summary>Advances the enumerator to the next element of the collection.</summary>
        ''' <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
        ''' <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            If Not Inverse AndAlso Index <= Array.GetUpperBound(0) Then
                Index += 1
                Return Index <= Array.GetUpperBound(0)
            ElseIf Inverse AndAlso Index >= Array.GetLowerBound(0) Then
                Index -= 1
                Return Index >= Array.GetLowerBound(0)
            Else
                Return False
            End If
        End Function

        ''' <summary>Moves internal pointer of enumerator to previos member of collection</summary>
        ''' <returns>True when pointer was succesfully mowed to item inside the collection. False if it was moved before start of collection or if it already was before start of collection. If pointer was after end of collection, it is moved to last item of collection and return value is true.</returns>
        Public Function MovePrevious() As Boolean Implements IBiDirEnumerator(Of T).MovePrevious
            If Not Inverse AndAlso Index >= Array.GetUpperBound(0) Then
                Index -= 1
                Return Index >= Array.GetUpperBound(0)
            ElseIf Inverse AndAlso Index <= Array.GetLowerBound(0) Then
                Index += 1
                Return Index <= Array.GetLowerBound(0)
            Else
                Return False
            End If
        End Function


        ''' <summary>Resets enumerator to start position (depends on direction of enumeration)</summary>
        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            Index = If(Inverse, Array.GetUpperBound(0) + 1, Array.GetLowerBound(0) - 1)
        End Sub


#Region " IDisposable Support "
        ''' <summary>To detect redundant calls</summary>
        Private disposedValue As Boolean = False

        ''' <summary>This code added by Visual Basic to correctly implement the disposable pattern.</summary>
        ''' <param name="disposing">Set to True by <see cref="Dispose"/></param>
        Protected Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    Array = Nothing
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