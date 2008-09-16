Namespace IOt
#If Config <= Alpha Then 'Stage:Alpha
    ''' <summary>Extends <see cref="System.IO.BinaryReader"/> to be able to read numeric data in both little-endian and big-endian format</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(BinaryReader), LastChange:="04/23/2007")> _
    Public Class BinaryReader : Inherits System.IO.BinaryReader
        ''' <summary>Possible byte orders</summary>
        Public Enum ByteAlign
            ''' <summary>LittleEndian (Intel)</summary>
            ''' <remarks>Stores 0A0B0C0D as 0D0C0B0A</remarks>
            LittleEndian
            ''' <summary>BigEndian (Motorola)</summary>
            ''' <remarks>Stores 0A0B0C0D as 0A0B0C0D</remarks>
            BigEndian
        End Enum
        ''' <summary>Contains value of the <see cref="ByteOrder"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ByteOrder As ByteAlign = ByteAlign.BigEndian
        ''' <summary>Format in which numeric data are read</summary>
        ''' <remarks>Only integral numbers are affected by this property
        ''' <list><listheader>List of affected functions</listheader>
        ''' <item><see cref="ReadInt16"/></item>
        ''' <item><see cref="ReadInt32"/></item>
        ''' <item><see cref="ReadInt64"/></item>
        ''' <item><see cref="ReadUInt16"/></item>
        ''' <item><see cref="ReadUInt32"/></item>
        ''' <item><see cref="ReadUInt64"/></item>
        ''' </list>
        ''' </remarks>
        Public Property ByteOrder() As ByteAlign
            Get
                Return _ByteOrder
            End Get
            Set(ByVal value As ByteAlign)
                _ByteOrder = value
            End Set
        End Property
        ''' <summary>Initializes a new instance of the <see cref="Tools.IOt.BinaryReader"/> class based on the supplied stream and using <see cref="System.Text.UTF8Encoding"/>.</summary>
        ''' <param name="input">A stream.</param>
        ''' <param name="Align">Format in which numeric data are read</param>
        ''' <exception cref="System.ArgumentException">The stream does not support reading, the stream is null, or the stream is already closed</exception>
        Public Sub New(ByVal input As System.IO.Stream, ByVal Align As ByteAlign)
            MyBase.New(input)
            ByteOrder = Align
        End Sub
        ''' <summary>Initializes a new instance of the <see cref="Tools.IOt.BinaryReader"/> class based on the supplied stream and a specific character encoding.</summary>
        ''' <param name="encoding">The character encoding.</param>
        ''' <param name="input">The supplied stream.</param>
        ''' <param name="Align">Format in which numeric data are read</param>
        ''' <exception cref="System.ArgumentNullException">encoding is null.</exception>
        ''' <exception cref="System.ArgumentException">The stream does not support reading, the stream is null, or the stream is already closed.</exception>
        Public Sub New(ByVal input As System.IO.Stream, ByVal encoding As System.Text.Encoding, ByVal Align As ByteAlign)
            MyBase.New(input, encoding)
            ByteOrder = Align
        End Sub
        ''' <summary>Reads a 2-byte signed integer from the current stream using specified endian encoding accorifng to <see cref="ByteOrder"/> and advances the current position of the stream by two bytes.</summary>
        ''' <returns>A 2-byte signed integer read from the current stream.</returns>
        ''' <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
        Public Overrides Function ReadInt16() As Short
            If ByteOrder = ByteAlign.BigEndian Then
                Return MathT.LEBE(MyBase.ReadInt16())
            Else
                Return MyBase.ReadInt16()
            End If
        End Function
        ''' <summary>Reads a 4-byte signed integer from the current stream using specified endian encoding accorifng to <see cref="ByteOrder"/> and advances the current position of the stream by four bytes.</summary>
        ''' <returns>A 4-byte signed integer read from the current stream.</returns>
        ''' <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
        Public Overrides Function ReadInt32() As Integer
            If ByteOrder = ByteAlign.BigEndian Then
                Return MathT.LEBE(MyBase.ReadInt32())
            Else
                Return MyBase.ReadInt32()
            End If
        End Function
        ''' <summary>Reads an 8-byte signed integer from the current stream using specified endian encoding accorifng to <see cref="ByteOrder"/> and advances the current position of the stream by eight bytes.</summary>
        ''' <returns>An 8-byte signed integer read from the current stream.</returns>
        ''' <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
        Public Overrides Function ReadInt64() As Long
            If ByteOrder = ByteAlign.BigEndian Then
                Return MathT.LEBE(MyBase.ReadInt64())
            Else
                Return MyBase.ReadInt64()
            End If
        End Function
        ''' <summary>Reads a 2-byte unsigned integer from the current stream using specified endian encoding accorifng to <see cref="ByteOrder"/> and advances the position of the stream by two bytes.</summary>
        ''' <returns>A 2-byte unsigned integer read from this stream.</returns>
        ''' <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
        <CLSCompliant(False)> _
        Public Overrides Function ReadUInt16() As UShort
            If ByteOrder = ByteAlign.BigEndian Then
                Return MathT.LEBE(MyBase.ReadUInt16())
            Else
                Return MyBase.ReadUInt16()
            End If
        End Function
        ''' <summary>Reads a 4-byte unsigned integer from the current stream using specified endian encoding accorifng to <see cref="ByteOrder"/> and advances the position of the stream by four bytes.</summary>
        ''' <returns>A 4-byte unsigned integer read from this stream.</returns>
        ''' <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
        <CLSCompliant(False)> _
        Public Overrides Function ReadUInt32() As UInteger
            If ByteOrder = ByteAlign.BigEndian Then
                Return MathT.LEBE(MyBase.ReadUInt32())
            Else
                Return MyBase.ReadUInt32()
            End If
        End Function
        ''' <summary>Reads an 8-byte unsigned integer from the current stream using specified endian encoding accorifng to <see cref="ByteOrder"/> and advances the position of the stream by eight bytes.</summary>
        ''' <returns>An 8-byte unsigned integer read from this stream.</returns>
        ''' <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached.</exception>
        <CLSCompliant(False)> _
        Public Overrides Function ReadUInt64() As ULong
            If ByteOrder = ByteAlign.BigEndian Then
                Return MathT.LEBE(MyBase.ReadUInt64())
            Else
                Return MyBase.ReadUInt64()
            End If
        End Function
    End Class
#End If
End Namespace