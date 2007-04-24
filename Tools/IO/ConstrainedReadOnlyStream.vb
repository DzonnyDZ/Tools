Namespace IO
#If Config <= Nightly Then 'Stage: nightly
    ''' <summary>Implements stream that reads only part of base stream</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(ConstrainedReadOnlyStream), LastChange:="4/24/2007")> _
    Public Class ConstrainedReadOnlyStream : Inherits System.IO.Stream
        Implements Collections.Generic.IReadOnlyIndexable(Of Byte)
        ''' <summary><see cref="System.IO.Stream"/> being constrained</summary>
        Protected ReadOnly Stream As System.IO.Stream
        ''' <summary>Minimum position for seek (0-based)</summary>
        Protected ReadOnly StartPosition As Long
        ''' <summary>Number of bytes allowed to read starting at <see cref="StartPosition"/></summary>
        Protected ReadOnly ConstrainedLenght As Long
        ''' <summary>CTor</summary>
        ''' <param name="BaseStream">Stream to be constrained</param>
        ''' <param name="StartPosition">Minimum position for seek (0-based)</param>
        ''' <param name="Lenght">Number of bytes allowed to read starting at <paramref name="StartPosition"/></param>
        ''' <exception cref="NotSupportedException"><paramref name="Stream"/> doesn't support read or seek (the <see cref="System.IO.Stream.CanRead"/> or <see cref="System.IO.Stream.CanSeek"/> returns false)</exception>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="Lenght"/> is less than zero -or-
        ''' Position of end of constraint is after end of <paramref name="BaseStream"/> and <paramref name="Lenght"/> is greater than zero
        ''' </exception>
        Public Sub New(ByVal BaseStream As System.IO.Stream, ByVal StartPosition As Long, ByVal Lenght As Long)
            If BaseStream.CanRead AndAlso BaseStream.CanSeek Then
                Me.Stream = BaseStream
                Me.StartPosition = StartPosition
                Me.ConstrainedLenght = Lenght
            Else
                Throw New NotSupportedException("Stream must support reading and seeking")
            End If
            If Length < 0 Then Throw New ArgumentException("Lenght must be greater than or equal to zero", "Length")
            If Length > 0 AndAlso StartPosition + Length > Stream.Length Then Throw New ArgumentException("Size of constrained stream must fit into base stream.")
        End Sub
        ''' <summary>Gets a value indicating whether the current stream supports reading.</summary>
        ''' <returns>Always true</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides ReadOnly Property CanRead() As Boolean
            Get
                Return Stream.CanRead
            End Get
        End Property
        ''' <summary>Gets a value indicating whether the current stream supports seeking.</summary>
        ''' <returns>Always true</returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides ReadOnly Property CanSeek() As Boolean
            Get
                Return Stream.CanSeek
            End Get
        End Property
        ''' <summary>Gets a value indicating whether the current stream supports writing.</summary>
        ''' <returns>Always false</returns>
        Public Overrides ReadOnly Property CanWrite() As Boolean
            Get
                Return False
            End Get
        End Property
        ''' <summary>This is readonly stream, does nothing</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Sub Flush()
        End Sub
        ''' <summary>Gets the length in bytes of the constrained stream</summary>
        ''' <returns>A long value representing the length of the sconstrained tream in bytes.</returns>
        ''' <exception cref="System.ObjectDisposedException">Methods were called after the base stream was closed.</exception>
        Public Overrides ReadOnly Property Length() As Long
            Get
                Return ConstrainedLenght
            End Get
        End Property
        ''' <summary>Stream being constrained</summary>
        Public Overridable ReadOnly Property BaseStream() As System.IO.Stream
            Get
                Return Stream
            End Get
        End Property
        ''' <summary>Offset of start of <see cref="BaseStream"/> where constrainment starts</summary>
        Public ReadOnly Property ConstraintStart() As Long
            Get
                Return StartPosition
            End Get
        End Property
        ''' <summary>Gets or sets the position within the current stream.</summary>
        ''' <value>Sets postion in base stream to new position assigned plus <see cref="ConstraintStart"/>. When new value exceeds constrainment limits then it is set too edge values. Position can be set 1 byte after end of stream to determine EOF</value>
        ''' <returns>The current position within the stream. Returns 0 if position of <see cref="BaseStream"/> if before <see cref="ConstraintStart"/>, returns <see cref="Length"/> if position of <see cref="BaseStream"/> is after end of constraint</returns>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.ObjectDisposedException">Methods were called after the stream was closed</exception>
        Public Overrides Property Position() As Long
            Get
                If Stream.Position < StartPosition Then
                    Return 0
                ElseIf Stream.Position >= StartPosition + ConstrainedLenght Then
                    Return ConstrainedLenght
                Else
                    Return Stream.Position - StartPosition
                End If
            End Get
            Set(ByVal value As Long)
                If ConstrainedLenght > 0 AndAlso value <= ConstrainedLenght Then
                    Stream.Position = value + StartPosition
                ElseIf ConstrainedLenght > 0 AndAlso value < 0 Then
                    Stream.Position = StartPosition
                ElseIf ConstrainedLenght > 0 AndAlso value > ConstrainedLenght Then
                    Stream.Position = StartPosition + ConstrainedLenght
                End If
            End Set
        End Property
        ''' <summary>Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
        ''' <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
        ''' <param name="count">The maximum number of bytes to be read from the current stream.</param>
        ''' <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source.</param>
        ''' <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
        ''' <exception cref="System.ArgumentException">The sum of offset and count is larger than the buffer length.</exception>
        ''' <exception cref="System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        ''' <exception cref="System.ArgumentNullException">buffer is null.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.ArgumentOutOfRangeException">offset or count is negative.</exception>
        Public Overrides Function Read(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer) As Integer
            If count + Position >= Length Then count = Length - Position
            If count < 0 Then count = 0
            Return Stream.Read(buffer, offset, count)
        End Function
        ''' <summary>Sets the position within the current stream.</summary>
        ''' <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
        ''' <param name="origin">A value of type <see cref="System.IO.SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        ''' <returns>The new position within the current stream.</returns>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
        Public Overrides Function Seek(ByVal offset As Long, ByVal origin As System.IO.SeekOrigin) As Long
            Select Case origin
                Case System.IO.SeekOrigin.Begin
                    Position = Position
                Case System.IO.SeekOrigin.Current
                    Position += Position
                Case System.IO.SeekOrigin.End
                    Position = Length - 1 + Position
            End Select
            Return Position
        End Function
        ''' <summary>Throws <see cref="System.NotSupportedException"/></summary>
        ''' <param name="value">Ignored</param>
        ''' <exception cref="System.NotSupportedException">Always: The stream does not support both writing and seeking, such as if the stream is constructed from a pipe or console output.</exception>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Sub SetLength(ByVal value As Long)
            Throw New NotSupportedException("ConstrainedReadonlyStream supports neither writing nor seeking")
        End Sub
        ''' <summary>Throws <see cref="System.NotSupportedException"/></summary>
        ''' <param name="offset">Ignored</param>
        ''' <param name="count">Ignored</param>
        ''' <param name="buffer">Ignored</param>
        ''' <exception cref="System.NotSupportedException">Always: The stream does not support writing.</exception>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer)
            Throw New NotSupportedException("ConstrainedReadonlyStream doesn't support writing")
        End Sub
        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="System.Collections.Generic.IEnumerator(Of T1)"/> that can be used to iterate through the collection.</returns>
        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of Byte) Implements System.Collections.Generic.IEnumerable(Of Byte).GetEnumerator
            Return New Collections.Generic.IndexableEnumerator(Of Byte)(Me)
        End Function
        ''' <summary>Returns an enumerator that iterates through a collection.</summary>
        ''' <returns>An <see cref="System.Collections.IEnumerator"/> object that can be used to iterate through the collection.</returns>
        <Obsolete("Use type safe GetEnumerator instead"), EditorBrowsable(EditorBrowsableState.Never)> _
        Private Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function
        ''' <summary>Gets value on specified index</summary>
        ''' <param name="index">Index to obtain value</param>
        ''' <returns>value lying on specified <paramref name="index"/></returns>
        ''' <exception cref="ArgumentException">Specified <paramref name="index"/> is invalid</exception>
        Default Public ReadOnly Property Item(ByVal index As Long) As Byte Implements Collections.Generic.IReadOnlyIndexable(Of Byte, Long).Item
            Get
                If index < 0 OrElse index >= ConstrainedLenght Then Throw New ArgumentOutOfRangeException("index", "index is out of range")
                Dim OldP As Long = Position
                Try
                    Position = index
                    Return Me.ReadByte
                Finally
                    Position = OldP
                End Try
            End Get
        End Property
        ''' <summary>Maximal valid value for index</summary>
        Public ReadOnly Property Maximum() As Long Implements Collections.Generic.IReadOnlyIndexable(Of Byte).Maximum
            Get
                Return ConstrainedLenght - 1
            End Get
        End Property
        ''' <summary>Minimal valid value for index</summary>
        Public ReadOnly Property Minimum() As Long Implements Collections.Generic.IReadOnlyIndexable(Of Byte).Minimum
            Get
                Return 0
            End Get
        End Property
        ''' <summary>Returns <see cref="Position"/> translated into <see cref="System.IO.Stream.Position"/> in <see cref="BaseStream"/> or into <see cref="System.IO.Stream.Position"/> in its <see cref="BaseStream"/></summary>
        ''' <returns>
        ''' <see cref="Position"/> in this <see cref="ConstrainedReadonlyStream"/> + <see cref="ConstraintStart"/> if <see cref="BaseStream"/> is not <see cref="ConstrainedReadonlyStream"/>.
        ''' If <see cref="BaseStream"/> is <see cref="ConstrainedReadonlyStream"/> returns <see cref="Position"/> in <see cref="BaseStream"/>'s <see cref="BaseStream"/> recursively.
        ''' </returns>
        Public ReadOnly Property TruePosition() As Long
            Get
                Return TranslatePosition(Position)
            End Get
        End Property
        ''' <summary>Returns <paramref name="Position"/> translated into <see cref="System.IO.Stream.Position"/> in <see cref="BaseStream"/> or into <see cref="System.IO.Stream.Position"/> in its <see cref="BaseStream"/></summary>
        ''' <returns>
        ''' <see cref="Position"/> in this <see cref="ConstrainedReadonlyStream"/> + <see cref="ConstraintStart"/> if <see cref="BaseStream"/> is not <see cref="ConstrainedReadonlyStream"/>.
        ''' If <see cref="BaseStream"/> is <see cref="ConstrainedReadonlyStream"/> returns <see cref="Position"/> in <see cref="BaseStream"/>'s <see cref="BaseStream"/> recursively.
        ''' </returns>
        Public Function TranslatePosition(ByVal Position As Long) As Long
            If TypeOf BaseStream Is ConstrainedReadOnlyStream Then
                Return DirectCast(BaseStream, ConstrainedReadOnlyStream).TranslatePosition(ConstraintStart + Position)
            Else
                Return ConstraintStart + Position
            End If
        End Function
        ''' <summary>String representation</summary>
        Public Overrides Function ToString() As String
            If Me.Length > 0 Then
                Return String.Format("start {0}, end {1}", Hex(TranslatePosition(0)), Hex(TranslatePosition(Length - 1)))
            Else
                Return "<0>"
            End If
        End Function
    End Class
#End If
End Namespace
