Imports System.Linq
#If True
Namespace IOt
    ''' <summary>Implements read-only stream which reads across multiple other streams</summary>
    Public Class UnionReadOnlyStream
        Inherits IO.Stream
        ''' <summary>CTor from array of streams</summary>
        ''' <param name="Streams">Array of streams to read across. If any of streams does not support seeking all streems must be positioned at position when reading should start. If all streams supports seeking, they can be positioned anywhere and will be seeked to start.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Streams"/> is null</exception>
        ''' <exception cref="ArgumentException">Any of streams does not support reading</exception>
        Public Sub New(ByVal ParamArray Streams As IO.Stream())
            Me.new(DirectCast(Streams, IEnumerable(Of IO.Stream)))
        End Sub
        ''' <summary>Streams to read across</summary>
        Private ReadOnly Streams As IO.Stream()
        ''' <summary>CTor from array of streams</summary>
        ''' <param name="Streams">Array of streams to read across. If any of streams does not support seeking all streems must be positioned at position when reading should start. If all streams supports seeking, they can be positioned anywhere and will be seeked to start.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Streams"/> is null</exception>
        ''' <exception cref="ArgumentException">Any of streams does not support reading</exception>
        Public Sub New(ByVal Streams As IEnumerable(Of IO.Stream))
            Me.Streams = New List(Of IO.Stream)(Streams).ToArray
            _CanSeek = True
            For Each stream In Streams
                If Not stream.CanRead Then Throw New ArgumentException("Underlying sream must be able to read")
                _CanSeek = _CanSeek AndAlso stream.CanSeek
            Next
        End Sub
        ''' <summary>Contains value of the <see cref="CanSeek"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _CanSeek As Boolean
        ''' <summary>Index of current stream in <see cref="Streams"/></summary>
        Private CurrentStream As Integer = 0
        ''' <summary>Current position (number of bytes read from start of first stream)</summary>
        Private CurrentPosition As Long = 0
#Region "Simple implementation"
        ''' <summary>Gets a value indicating whether the current stream supports reading.</summary>
        ''' <returns>Always true</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> Public Overrides ReadOnly Property CanRead() As Boolean
            Get
                Return True
            End Get
        End Property
        ''' <summary>Gets a value indicating whether the current stream supports seeking.</summary>
        ''' <returns>True if all underlying streams support seeking</returns>
        Public Overrides ReadOnly Property CanSeek() As Boolean
            Get
                Return _CanSeek
            End Get
        End Property
        ''' <summary>Gets a value indicating whether the current stream supports writing.</summary>
        ''' <returns>Always false</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> Public Overrides ReadOnly Property CanWrite() As Boolean
            Get
                Return False
            End Get
        End Property
        ''' <summary>Does nothing</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Public Overrides Sub Flush()
        End Sub

        ''' <summary>Gets the length in bytes of the stream.</summary>
        ''' <returns>A long value representing the length of the stream in bytes.</returns>
        ''' <exception cref="T:System.NotSupportedException">A class derived from Stream does not support seeking.</exception>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        Public Overrides ReadOnly Property Length() As Long
            Get
                If CanSeek Then
                    Return (From stream In Streams Select l = stream.Length).Sum
                Else
                    Throw New NotSupportedException(ResourcesT.Exceptions.ThisStreamDoesNotSupportSeeking)
                End If
            End Get
        End Property
        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <param name="value">Ignored</param>
        ''' <exception cref="T:System.NotSupportedException">Always</exception>
        <EditorBrowsable(EditorBrowsableState.Never)> Public Overrides Sub SetLength(ByVal value As Long)
            Throw New NotSupportedException(ResourcesT.Exceptions.ThisStreamDoesNotSupportWriting)
        End Sub
        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <param name="buffer">Ignored</param>
        ''' <param name="offset">Ignored </param>
        ''' <param name="count">Ignored</param>
        ''' <exception cref="T:System.NotSupportedException">Always</exception>
        <EditorBrowsable(EditorBrowsableState.Never)> Public Overrides Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer)
            Throw New NotSupportedException(ResourcesT.Exceptions.ThisStreamDoesNotSupportWriting)
        End Sub
        ''' <summary>Gets or sets the position within the current stream.</summary>
        ''' <returns>The current position within the stream.</returns>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="T:System.NotSupportedException">The stream does not support seeking.</exception>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        ''' <filterpriority>1</filterpriority>
        Public Overrides Property Position() As Long
            Get
                Return CurrentPosition
            End Get
            Set(ByVal value As Long)
                Seek(0, IO.SeekOrigin.Begin)
            End Set
        End Property
#End Region
        ''' <summary>Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
        ''' <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
        ''' <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source.</param>
        ''' <param name="offset">The zero-based byte offset in 
        ''' <paramref name="buffer" /> at which to begin storing the data read from the current stream.</param>
        ''' <param name="count">The maximum number of bytes to be read from the current stream.</param>
        ''' <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="buffer" /> is null.</exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        ''' <filterpriority>1</filterpriority>
        Public Overrides Function Read(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer) As Integer
            Dim ret As Integer = 0
            Do While CurrentStream < Streams.Length
                ret = Streams(CurrentStream).Read(buffer, offset, count)
                If ret <> 0 Then Exit Do
                If CurrentStream + 1 < Streams.Length Then
                    CurrentStream += 1
                    If CanSeek Then Streams(CurrentStream).Seek(0, IO.SeekOrigin.Begin)
                End If
            Loop
            If CurrentStream >= Streams.Length AndAlso Streams.Length > 0 Then CurrentStream = Streams.Length - 1
            Return ret
        End Function

        ''' <summary>Sets the position within the current stream.</summary>
        ''' <returns>The new position within the current stream.</returns>
        ''' <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter.</param>
        ''' <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point used to obtain the new position.</param>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output.</exception>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        ''' <version version="1.5.3">Fix: ALways returns zero.</version>
        ''' <filterpriority>1</filterpriority>
        Public Overrides Function Seek(ByVal offset As Long, ByVal origin As System.IO.SeekOrigin) As Long
            If Not CanSeek Then Throw New NotSupportedException(ResourcesT.Exceptions.ThisStreamDoesNotSupportSeeking)
            Dim target As Long
            Select Case origin
                Case IO.SeekOrigin.Begin : target = offset
                Case IO.SeekOrigin.Current : target = Position + offset
                Case IO.SeekOrigin.End : target = Length - offset
            End Select
            Dim SumLen As Long = 0
            Dim iStream As Integer = 0
            While SumLen + Streams(iStream).Length < target
                iStream += 1
                If iStream >= Streams.Length Then Throw New IO.IOException(ResourcesT.Exceptions.AttemptToSeekAfterEndOfStream)
            End While
            Return Position
        End Function
    End Class
End Namespace
#End If