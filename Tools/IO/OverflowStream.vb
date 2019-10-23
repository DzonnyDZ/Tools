Imports Tools.IOt.StreamTools
#If True
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
        ''' <summary>In case the <see cref="SetLength"/> method of this instance was used and the lenght set was smaller than <see cref="SharedLength"/> this property gets the difference bethween those two lengths (always positive value).</summary>
        ''' <remarks>Number of bytes this stream is shorter than <see cref="SharedLength"/> or zero if this stream is same zize or longer than <see cref="SharedLength"/></remarks>
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
        ''' <param name="SharedLength">The lenght (in bytes) of area (starting at <paramref name="Start"/> new instance will have access to. (See <see cref="SharedLength"/>)</param>
        Public Sub New(ByVal BaseStream As IO.Stream, ByVal Start As Long, ByVal SharedLength As Long)
            Me.New(BaseStream, Start, SharedLength, 0)
        End Sub
        ''' <summary>Creates new instace of the <see cref="OverflowStream"/> class</summary>
        ''' <param name="BaseStream">Stream to insert data to.</param>
        ''' <param name="Start">The byte offset from start of <paramref name="BaseStream"/> to start of area new instance will have access to. (See <see cref="Start"/>)</param>
        ''' <param name="SharedLength">The lenght (in bytes) of area (starting at <paramref name="Start"/> new instance will have access to. (See <see cref="SharedLength"/>)</param>
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
            If value = Length Then Exit Sub
            If IsAtEnd Then
                Dim OldSharedLength = SharedLength
                BaseStream.SetLength(Start + value)
                _SharedLength = BaseStream.Length - Start
                OnAfterFlush(New FlushEventArgs(OldSharedLength, True))
            ElseIf value < SharedLength Then
                _Underflow = SharedLength - value
                OverflowStream.SetLength(0)
            Else
                _Underflow = 0
                OverflowStream.SetLength(value - SharedLength)
            End If
            If Position > value Then _Position = value
        End Sub
        ''' <summary>Gets value indicating if if this stream operates at the end of <see cref="BaseStream"/>, so buffering to <see cref="OverflowStream"/> can be ommitted.</summary>
        ''' <returns>True when sum of <see cref="Start"/> and <see cref="SharedLength"/> is greater than of equal to lenght of <see cref="BaseStream"/></returns>
        Protected ReadOnly Property IsAtEnd() As Boolean
            Get
                Return Start + SharedLength >= BaseStream.Length
            End Get
        End Property
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
#Region "Implementation"
        ''' <summary>Clears all buffers for this stream and causes any buffered data to be written to the underlying device.                </summary>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        Public Overrides Sub Flush()
            Dim OldSharedLenght = SharedLength
            If Underflow = 0 AndAlso OverflowStream.Length > 0 Then
                BaseStream.InsertInto(Start, SharedLength, OverflowStream.GetBuffer, 0, OverflowStream.Length)
                _SharedLength += OverflowStream.Length
                OverflowStream.SetLength(0)
            ElseIf Underflow > 0 Then
                OverflowStream.Flush()
                BaseStream.InsertInto(Start + Length, SharedLength - (Start + Length), New Byte() {})
                _SharedLength -= Underflow
                _Underflow = 0
                OverflowStream.SetLength(0)
            End If
            OnAfterFlush(New FlushEventArgs(OldSharedLenght))
            BaseStream.Flush()
        End Sub

        ''' <summary>Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
        ''' <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
        ''' <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source.</param>
        ''' <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream.</param>
        ''' <param name="count">The maximum number of bytes to be read from the current stream.</param>
        ''' <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="buffer" /> is null.</exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        Public Overrides Function Read(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer) As Integer
            If offset + count > buffer.Length Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.WasGreaterThanLegnthOf2, "offset", "count", "buffer"))
            If buffer Is Nothing Then Throw New ArgumentException("buffer")
            If offset < 0 Then Throw New ArgumentOutOfRangeException("offset", String.Format(ResourcesT.Exceptions.MustBeGreaterThanOrEqualToZero, "offset"))
            If count < 0 Then Throw New ArgumentOutOfRangeException("offset", String.Format(ResourcesT.Exceptions.MustBeGreaterThanOrEqualToZero, "count"))
            If Position >= Length OrElse count = 0 Then Return 0
            If IsAtEnd Then
                If BaseStream.Position <> Start + Position Then BaseStream.Position = Start + Position
                Dim ret = BaseStream.Read(buffer, offset, count)
                Position += ret
                Return ret
            End If
            Dim BytesRead%
            'Read from base stream
            If Position < SharedLength Then
                If BaseStream.Position <> Start + Position Then BaseStream.Position = Start + Position
                Dim BaseStreamToReadCount = Math.Max(CLng(count), SharedLength - Underflow - Position)
                Dim BaseStreamReadCount = BaseStream.Read(buffer, offset, BaseStreamToReadCount)
                If BaseStreamReadCount < BaseStreamToReadCount Then
                    Position += BaseStreamReadCount
                    Return BaseStreamReadCount
                Else
                    BytesRead = BaseStreamReadCount
                End If
            End If
            Dim Remains = count - BytesRead
            If Remains = 0 OrElse Underflow > 0 OrElse OverflowStream.Length = 0 Then
                Position += BytesRead
                Return BytesRead
            End If
            'Read from overflow stream
            Dim OverflowPosition = If(Position < SharedLength, 0, Position - SharedLength)
            If OverflowStream.Position <> OverflowPosition Then OverflowStream.Position = OverflowPosition
            BytesRead += OverflowStream.Read(buffer, offset + BytesRead, count - BytesRead)
            Position += BytesRead
            Return BytesRead
        End Function
        ''' <summary>Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.</summary>
        ''' <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream.</param>
        ''' <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
        ''' <param name="count">The number of bytes to be written to the current stream.</param>
        ''' <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is greater than the buffer length.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="buffer" /> is null.</exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        Public Overrides Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer)
            If offset + count > buffer.Length Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.WasGreaterThanLegnthOf2, "offset", "count", "buffer"))
            If buffer Is Nothing Then Throw New ArgumentException("buffer")
            If offset < 0 Then Throw New ArgumentOutOfRangeException("offset", String.Format(ResourcesT.Exceptions.MustBeGreaterThanOrEqualToZero, "offset"))
            If count < 0 Then Throw New ArgumentOutOfRangeException("offset", String.Format(ResourcesT.Exceptions.MustBeGreaterThanOrEqualToZero, "count"))
            If count = 0 Then Exit Sub
            If IsAtEnd Then
                Dim OldSharedLenght = SharedLength
                If BaseStream.Position <> Start + Position Then BaseStream.Position = Start + Position
                BaseStream.Write(buffer, offset, count)
                _SharedLength = BaseStream.Length - Start
                If SharedLength <> OldSharedLenght Then OnAfterFlush(New FlushEventArgs(OldSharedLenght, True))
                Return
            End If
            Dim BytesWritten%
            'Write to base stream
            If Position < SharedLength Then
                If BaseStream.Position <> Start + Position Then BaseStream.Position = Start + Position
                Dim BaseStreamToWriteCount = Math.Max(CLng(count), SharedLength - Position)
                BaseStream.Write(buffer, offset, BaseStreamToWriteCount)
                BytesWritten = BaseStreamToWriteCount
            End If
            If BytesWritten = count Then
                Position += BytesWritten
                Exit Sub
            End If
            'Write to overflow stream
            Dim OverflowPosition = If(Position < SharedLength, 0, Position - SharedLength)
            If OverflowStream.Position <> OverflowPosition Then OverflowStream.Position = OverflowPosition
            OverflowStream.Write(buffer, offset + BytesWritten, count - BytesWritten)
            BytesWritten = count
            Position += BytesWritten
        End Sub

        ''' <summary>Reads a byte from the stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream.</summary>
        ''' <returns>The unsigned byte cast to an Int32, or -1 if at the end of the stream.</returns>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        Public Overrides Function ReadByte() As Integer
            If IsAtEnd OrElse Position < SharedLength - Underflow Then
                If BaseStream.Position <> Start + Position Then BaseStream.Position = Start + Position
                Dim ret = BaseStream.ReadByte()
                Position += 1
                Return ret
            ElseIf Underflow > 0 Then
                Return -1
            ElseIf Position < Length Then
                If OverflowStream.Position <> Position - SharedLength Then OverflowStream.Position = Position - SharedLength
                Dim ret = OverflowStream.ReadByte
                Position += 1
                Return ret
            Else
                Return -1
            End If
        End Function
        ''' <summary>Writes a byte to the current position in the stream and advances the position within the stream by one byte.</summary>
        ''' <param name="value">The byte to write to the stream.</param>
        ''' <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Sub WriteByte(ByVal value As Byte)
            If IsAtEnd Then
                Dim OldSharedLenght = SharedLength
                If BaseStream.Position <> Start + Position Then BaseStream.Position = Start + Position
                BaseStream.WriteByte(value)
                Position += 1
                _SharedLength = BaseStream.Length - Start
                If SharedLength <> OldSharedLenght Then OnAfterFlush(New FlushEventArgs(OldSharedLenght, True))
            ElseIf Position < SharedLength AndAlso Position >= SharedLength - Underflow Then
                If BaseStream.Position <> Start + Position Then BaseStream.Position = Start + Position
                BaseStream.WriteByte(value)
                Position += 1
                _Underflow = SharedLength - Position + 1
            ElseIf Position < SharedLength Then
                If BaseStream.Position <> Start + Position Then BaseStream.Position = Start + Position
                BaseStream.WriteByte(value)
                Position += 1
            Else
                If OverflowStream.Position <> Position - SharedLength Then OverflowStream.Position = Position - SharedLength
                OverflowStream.WriteByte(value)
                Position += 1
            End If
        End Sub
#End Region
#Region "Other stuff"
        ''' <summary>Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream.</summary>
        Public Overrides Sub Close()
            MyBase.Close()
            OnAfterClose(New EventArgs)
            If CloseBaseStream Then BaseStream.Close()
        End Sub
        ''' <summary>Contains value of the <paramref name="CloseBaseStream"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _CloseBaseStream As Boolean
        ''' <summary>Gets or sets value indicating when <see cref="BaseStream">BaseStream</see>.<see cref="IO.Stream.Close">Close</see> is called when <see cref="Close"/> is called.</summary>
        ''' <remarks>True if base stream is closed when this stream is closed.</remarks>
        Public Property CloseBaseStream() As Boolean
            <DebuggerStepThrough()> Get
                Return _CloseBaseStream
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Boolean)
                _CloseBaseStream = value
            End Set
        End Property
#End Region
#Region "Events"
        ''' <summary>Raises the <see cref="AfterFlush"/> event</summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnAfterFlush(ByVal e As FlushEventArgs)
            RaiseEvent AfterFlush(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="AfterClose"/> event</summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnAfterClose(ByVal e As EventArgs)
            RaiseEvent AfterClose(Me, e)
        End Sub
        ''' <summary>Raised after the <see cref="Flush"/> method is called</summary>
        ''' <remarks>Use this event to detect changes of lenght of embdeded stream.
        ''' <para>When stream is fkushed all data are written to <see cref="BaseStream"/> and original data of <see cref="BaseStream"/> positioned after resticted area of this stream is moved (left or right) depending on if <see cref="SharedLength"/> is shinking or growing. <see cref="SharedLength"/> is updated so it is same as <see cref="Length"/>.</para>
        ''' <para>When this stream is placed at the end of <see cref="BaseStream"/> this event is raised for each call of <see cref="Write"/> which causes change of length of stream and for each call of <see cref="SetLength"/> because buffering is ommited.</para>
        ''' <para>The <see cref="Flush"/> method also calls the <see cref="IO.Stream.Flush"/> method of <see cref="BaseStream"/>. This event is raised before it is called.</para></remarks>
        Public Event AfterFlush As EventHandler(Of OverflowStream, FlushEventArgs)
        ''' <summary>Raised after the <see cref="Close"/> method is called</summary>
        ''' <remarks>If <see cref="CloseBaseStream"/> is true, this event is raised before <see cref="BaseStream"/> is closed.</remarks>
        Public Event AfterClose As EventHandler(Of OverflowStream, EventArgs)
        Public Class FlushEventArgs
            Inherits EventArgs
            ''' <summary>Contains value of the <see cref="OldSharedLenght"/> property</summary>
            Private ReadOnly _OldSharedLength As Long
            ''' <summary>CTor</summary>
            ''' <param name="OldSharedLength"><see cref="SharedLength"/> before fhush was performed.</param>
            ''' <param name="ForWrite">Indicates if event is being raised by <see cref="Write"/> or <see cref="SetLength"/> method (true) or <see cref="Flush"/> method (false)</param>
            Public Sub New(ByVal OldSharedLength As Long, Optional ByVal ForWrite As Boolean = False)
                _OldSharedLength = OldSharedLength
            End Sub
            ''' <summary>Contains cvalue of the <see cref="ForWrite"/> property</summary>
            Private ReadOnly _ForWrite As Boolean
            ''' <summary>Gest value idicating if event was raised for <see cref="Write"/> or <see cref="SetLength"/> method or for <see cref="Flush"/></summary>
            ''' <returns>True if event was raised for <see cref="Write"/> or <see cref="SetLength"/> method. False if event was raised for <see cref="Flush"/></returns>
            ''' <remarks>The <see cref="IOt.OverflowStream"/> class raises the <see cref="AfterFlush"/> event with this property set to true whenever <see cref="Write"/> which causes change of length of stream or <see cref="SetLength"/> is called while buffering mechanism is ommited because of <see cref="IOt.OverflowStream"/> resides at the end of <see cref="BaseStream"/> </remarks>
            Public ReadOnly Property ForWrite() As Boolean
                Get
                    Return _ForWrite
                End Get
            End Property
            ''' <summary>Value of the <see cref="SharedLength"/> property before call of the <see cref="Flush"/> method</summary>
            ''' <returns>Gets old value of the <see cref="SharedLength"/> property before the action thät caused the event to be raised was taken.</returns>
            Public ReadOnly Property OldSharedLenght() As Long
                Get
                    Return _OldSharedLength
                End Get
            End Property
        End Class
#End Region
    End Class
End Namespace
#End If