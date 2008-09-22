Imports Tools.IOt.StreamTools
#If config <= Nightly Then 'Stage: Nightly
Namespace IOt
    ''' <summary>Implements sream that can be used to write data into another streem where writer does not see the basë sream and it is possible to write more data the possibli fits into free space in base stream</summary>
    ''' <seealso cref="IO.Stream"/>
    ''' <remarks>This stream is attached to another stream that can read, write and seek. It is possible to read, write and seek this stream. If data are written which exceeds size of restricted area within base stream, the data are cached in memore and when <see cref="OverflowStream.Flush"/> is called the data are insert at position of end of restricted area in base stream. Actual data in base stream are moved.
    ''' The overflow mechanism is ommited when resticted area ends at the end of base steream.</remarks>
    Public Class OverflowStream
        Inherits IO.Stream
#Region "Settings"
        ''' <summary>Contains value of the <see cref="BaseStream"/> property</summary>
        Private ReadOnly _BaseStream As IO.Stream
        ''' <summary>Contains value of the <see cref="Start"/> property</summary>
        Private ReadOnly _Start As Long
        ''' <summary>Contains value of the <see cref="SharedLength"/> property</summary>
        Private _SharedLength As Long
        ''' <summary>Contains value of the <see cref="BaseStream"/> property</summary>
        Private _OverflowStream As IO.MemoryStream
        ''' <summary>Contains value of the <see cref="Underflow"/> property</summary>
        Private _Underflow As Long
        ''' <summary>Contains value of the <see cref="Position"/> property</summary>
        Private _Position As Long
        ''' <summary>Gets base stream of this stream</summary>
        ''' <returns>The stream this strem writes data into and reads data from</returns>
        Protected ReadOnly Property BaseStream() As IO.Stream
            Get
                Return _BaseStream
            End Get
        End Property
        ''' <summary>Gets offset of area in <see cref="BaseStream"/> this stream has access to</summary>
        ''' <returns>Offset of first byte this stream has assce to in <see cref="BaseStream"/></returns>
        Public ReadOnly Property Start() As Long
            Get
                Return _Start
            End Get
        End Property
        ''' <summary>Gets size (in bytes) of area in <see cref="BaseStream"/> this stream has access to</summary>
        ''' <returns>Number of bytes of <see cref="BaseStream"/> starting at <see cref="Start"/> this stream has access to. If more bytes is written to this stream, they are stored in overflow area and are inserted into <see cref="BaseStream"/> on <see cref="Flush"/></returns>
        ''' <remarks>Value of this property changes on <see cref="Flush"/></remarks>
        Public ReadOnly Property SharedLength() As Long
            Get
                Return _SharedLength
            End Get
        End Property
        ''' <summary>Gets stream bytes which does not fit into restricted area of <see cref="BaseStream"/> are written into</summary>
        ''' <returns>Stream of bytes that does not fit into restricted area of <see cref="BaseStream"/></returns>
        ''' <remarks>Note for inheritors: Avoid writing to this stream when <see cref="Underflow"/> is nonzero.</remarks>
        Protected ReadOnly Property OverflowStream() As IO.MemoryStream
            Get
                Return _OverflowStream
            End Get
        End Property
        ''' <summary>In case the <see cref="SetLength"/> method of this instance was used and the lenght set was smaller than <see cref="SharedLenght"/> this property gets the difference bethween those two lengths (always positive value).</summary>
        ''' <remarks>Number of bytes this stream is shorter than <see cref="SharedLenght"/> or zero if this stream is same zize or longer than <see cref="SharedLenght"/></remarks>
        Protected ReadOnly Property Underflow() As Long
            Get
                Return _Underflow
            End Get
        End Property
#End Region
#Region "CTors"
        ''' <summary>Creates new instace of the <see cref="OverflowStream"/> class</summary>
        ''' <param name="BaseStream">Stream to insert data to.</param>
        ''' <param name="Start">The byte offset from start of <paramref name="BaseStream"/> to start of area new instance will have access to. (See <see cref="Start"/>)</param>
        ''' <param name="SharedLength">The lenght (in bytes) of area (starting at <paramref name="Start"/> new instance will have access to. (See <see cref="SharedLenght"/>)</param>
        Public Sub New(ByVal BaseStream As IO.Stream, ByVal Start As Long, ByVal SharedLength As Long)
            Me.New(BaseStream, Start, SharedLength, 0)
        End Sub
        ''' <summary>Creates new instace of the <see cref="OverflowStream"/> class</summary>
        ''' <param name="BaseStream">Stream to insert data to.</param>
        ''' <param name="Start">The byte offset from start of <paramref name="BaseStream"/> to start of area new instance will have access to. (See <see cref="Start"/>)</param>
        ''' <param name="SharedLength">The lenght (in bytes) of area (starting at <paramref name="Start"/> new instance will have access to. (See <see cref="SharedLenght"/>)</param>
        ''' <param name="OverflowCapacity">The initial size of <see cref="OverflowStream"/> in bytes</param>
        Public Sub New(ByVal BaseStream As IO.Stream, ByVal Start As Long, ByVal SharedLength As Long, ByVal OverflowCapacity As Long)
            If BaseStream Is Nothing Then Throw New ArgumentNullException("BaseStream")
            If Not BaseStream.CanWrite OrElse Not BaseStream.CanSeek OrElse Not BaseStream.CanRead Then Throw New ArgumentException(ResourcesT.Exceptions.BaseStreamOfOverflowStreamMustBeAbleToReadWriteAndSeek)
            If SharedLength < 0 Then Throw New ArgumentOutOfRangeException("SharedLength", String.Format(ResourcesT.Exceptions.MustBeGreaterThanOrEqualToZero, "SharedLength"))

            _OverflowStream = New IO.MemoryStream(OverflowCapacity)
        End Sub
#End Region
#Region "Simple implementation"
        ''' <summary>Gets a value indicating whether the current stream supports reading.</summary>
        ''' <returns>true</returns>
        Public NotOverridable Overrides ReadOnly Property CanRead() As Boolean
            Get
                Return True
            End Get
        End Property
        ''' <summary>Gets a value indicating whether the current stream supports seeking.</summary>
        ''' <returns>true</returns>
        Public NotOverridable Overrides ReadOnly Property CanSeek() As Boolean
            Get
                Return True
            End Get
        End Property
        ''' <summary>Gets a value indicating whether the current stream supports writing.</summary>
        ''' <returns>true</returns>
        Public NotOverridable Overrides ReadOnly Property CanWrite() As Boolean
            Get
                Return True
            End Get
        End Property
        ''' <summary>Gets the length in bytes of the stream.</summary>
        ''' <returns>A long value representing the length of the stream in bytes.</returns>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        Public Overrides ReadOnly Property Length() As Long
            Get
                Return SharedLength + If(Underflow = 0, OverflowStream.Length, -Underflow)
            End Get
        End Property
        ''' <summary>Sets the position within the current stream.</summary>
        ''' <returns>The new position within the current stream.</returns>
        ''' <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter.</param>
        ''' <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point used to obtain the new position.</param>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        Public Overrides Function Seek(ByVal offset As Long, ByVal origin As System.IO.SeekOrigin) As Long
            Select Case origin
                Case IO.SeekOrigin.Begin : Position = offset
                Case IO.SeekOrigin.Current : Position += offset
                Case IO.SeekOrigin.End : Position = Length + offset
            End Select
            Return Me.Position
        End Function
        ''' <summary>Sets the length of the current stream.</summary>
        ''' <param name="value">The desired length of the current stream in bytes.</param>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        Public Overrides Sub SetLength(ByVal value As Long)
            If value < SharedLength Then _
                _Underflow = SharedLength - value _
                : OverflowStream.SetLength(0) _
            Else _
                _Underflow = 0 _
                : OverflowStream.SetLength(value - SharedLength)
            If Position > value Then _Position = value
        End Sub
        ''' <summary>Gets or sets the position within the current stream.</summary>
        ''' <returns>The current position within the stream.</returns>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        Public Overrides Property Position() As Long
            Get
                Return _Position
            End Get
            Set(ByVal value As Long)
                If value < 0 Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.MustBeGreaterThanOrEqualToZero, "Position"), "value")
                _Position = value
            End Set
        End Property
#End Region
        ''' <summary>Clears all buffers for this stream and causes any buffered data to be written to the underlying device.                </summary>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        Public Overrides Sub Flush()
            If Underflow = 0 AndAlso OverflowStream.Length > 0 Then
                BaseStream.InsertInto(Start, SharedLength, OverflowStream.GetBuffer, 0, OverflowStream.Length)
            ElseIf Underflow > 0 Then
                BaseStream.InsertInto(Start + Length, SharedLength - (Start + Length), New Byte() {})
            End If
        End Sub

        ''' <summary>Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
        ''' <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
        ''' <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source.</param>
        ''' <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream.</param>
        ''' <param name="count">The maximum number of bytes to be read from the current stream.</param>
        ''' <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
        ''' <exception cref="T:System.ArgumentNullException"><paramref name="buffer" /> is null.</exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        Public Overrides Function Read(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer) As Integer
            If offset + count > buffer.Length Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.WasGreaterThanLegnthOf2, "offset", "count", "buffer"))
            If buffer Is Nothing Then Throw New ArgumentException("buffer")
            If offset < 0 Then Throw New ArgumentOutOfRangeException("offset", String.Format(ResourcesT.Exceptions.MustBeGreaterThanOrEqualToZero, "offset"))
            If count < 0 Then Throw New ArgumentOutOfRangeException("offset", String.Format(ResourcesT.Exceptions.MustBeGreaterThanOrEqualToZero, "count"))
            If Position >= Length OrElse count = 0 Then Return 0
            'TODO:Implement
        End Function
        ''' <summary>Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.</summary>
        ''' <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream.</param>
        ''' <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
        ''' <param name="count">The number of bytes to be written to the current stream.</param>
        ''' <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is greater than the buffer length.</exception>
        ''' <exception cref="T:System.ArgumentNullException"><paramref name="buffer" /> is null.</exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        Public Overrides Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer)
            If offset + count > buffer.Length Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.WasGreaterThanLegnthOf2, "offset", "count", "buffer"))
            If buffer Is Nothing Then Throw New ArgumentException("buffer")
            If offset < 0 Then Throw New ArgumentOutOfRangeException("offset", String.Format(ResourcesT.Exceptions.MustBeGreaterThanOrEqualToZero, "offset"))
            If count < 0 Then Throw New ArgumentOutOfRangeException("offset", String.Format(ResourcesT.Exceptions.MustBeGreaterThanOrEqualToZero, "count"))
            If count = 0 Then Exit Sub
            'TODO:Implement
        End Sub
    End Class
End Namespace
#End If