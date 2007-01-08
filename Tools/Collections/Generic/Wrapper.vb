#If Config <= release Then
Namespace Collections.Generic
    ''' <summary>Wpars type-unsafe <see cref="IEnumerable"/> as type-safe <see cref="IEnumerable(Of T)"/></summary>
    ''' <typeparam name="T">Type that each item of wrapped collection must be of or convertible to</typeparam>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(Wrapper(Of Long)), lastchange:="1/7/2007")> _
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
End Namespace
#End If