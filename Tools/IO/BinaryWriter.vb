#If True
Namespace IOt
    ''' <summary>Extends <see cref="System.IO.BinaryWriter"/> to be able to read numeric data in both little-endian and big-endian format</summary>
    Public Class BinaryWriter
        Inherits IO.BinaryWriter
        ''' <summary>Contains value of the <see cref="ByteOrder"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ByteOrder As BinaryReader.ByteAlign = BinaryReader.ByteAlign.BigEndian
        ''' <summary>Format in which numeric data are read</summary>
        ''' <remarks>Only integral numbers are affected by this property
        ''' <list><listheader>List of affected functions</listheader>
        ''' <item><see cref="M:Tools.IOt.BinaryWriter.Write(System.Int16)"/></item>
        ''' <item><see cref="M:Tools.IOt.BinaryWriter.Write(System.Int32)"/></item>
        ''' <item><see cref="M:Tools.IOt.BinaryWriter.Write(System.Int64)"/></item>
        ''' <item><see cref="M:Tools.IOt.BinaryWriter.Write(System.UInt16)"/></item>
        ''' <item><see cref="M:Tools.IOt.BinaryWriter.Write(System.UInt32)"/></item>
        ''' <item><see cref="M:Tools.IOt.BinaryWriter.Write(System.UInt64)"/></item>
        ''' </list>
        ''' </remarks>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="BinaryReader.ByteAlign"/></exception>
        Public Property ByteOrder() As BinaryReader.ByteAlign
            Get
                Return _ByteOrder
            End Get
            Set(ByVal value As BinaryReader.ByteAlign)
                If Not InEnum(value) Then Throw New InvalidEnumArgumentException("value", value, value.GetType)
                _ByteOrder = value
            End Set
        End Property


        ''' <summary>Initializes a new instance of the <see cref="Tools.IOt.BinaryReader"/> class based on the supplied stream and using <see cref="System.Text.UTF8Encoding"/>.</summary>
        ''' <param name="input">A stream.</param>
        ''' <param name="Align">Format in which numeric data are read</param>
        ''' <exception cref="System.ArgumentException">The stream does not support reading, the stream is null, or the stream is already closed</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Align"/> is not member of <see cref="BinaryReader.ByteAlign"/></exception>
        Public Sub New(ByVal input As System.IO.Stream, ByVal Align As BinaryReader.ByteAlign)
            MyBase.New(input)
            ByteOrder = Align
        End Sub
        ''' <summary>Initializes a new instance of the <see cref="Tools.IOt.BinaryReader"/> class based on the supplied stream and a specific character encoding.</summary>
        ''' <param name="encoding">The character encoding.</param>
        ''' <param name="input">The supplied stream.</param>
        ''' <param name="Align">Format in which numeric data are read</param>
        ''' <exception cref="System.ArgumentNullException">encoding is null.</exception>
        ''' <exception cref="System.ArgumentException">The stream does not support reading, the stream is null, or the stream is already closed.</exception>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Align"/> is not member of <see cref="BinaryReader.ByteAlign"/></exception>
        Public Sub New(ByVal input As System.IO.Stream, ByVal encoding As System.Text.Encoding, ByVal Align As BinaryReader.ByteAlign)
            MyBase.New(input, encoding)
            ByteOrder = Align
        End Sub


        ''' <summary>Writes a four-byte signed integer to the current stream and advances the stream position by four bytes.</summary>
        ''' <param name="value">The four-byte signed integer to write.</param>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        Public Overrides Sub Write(ByVal value As Integer)
            If ByteOrder = BinaryReader.ByteAlign.BigEndian Then value = MathT.LEBE(value)
            MyBase.Write(value)
        End Sub
        ''' <summary>Writes a four-byte unsigned integer to the current stream and advances the stream position by four bytes.</summary>
        ''' <param name="value">The four-byte unsigned integer to write.</param>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        <CLSCompliant(False)> _
        Public Overrides Sub Write(ByVal value As UInteger)
            If ByteOrder = BinaryReader.ByteAlign.BigEndian Then value = MathT.LEBE(value)
            MyBase.Write(value)
        End Sub
        ''' <summary>Writes an eight-byte signed integer to the current stream and advances the stream position by eight bytes.</summary>
        ''' <param name="value">The eight-byte signed integer to write.</param>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        Public Overrides Sub Write(ByVal value As Long)
            If ByteOrder = BinaryReader.ByteAlign.BigEndian Then value = MathT.LEBE(value)
            MyBase.Write(value)
        End Sub
        ''' <summary>Writes an eight-byte unsigned integer to the current stream and advances the stream position by eight bytes.</summary>
        ''' <param name="value">The eight-byte unsigned integer to write.</param>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        <CLSCompliant(False)> _
        Public Overrides Sub Write(ByVal value As ULong)
            If ByteOrder = BinaryReader.ByteAlign.BigEndian Then value = MathT.LEBE(value)
            MyBase.Write(value)
        End Sub
        ''' <summary>Writes a two-byte signed integer to the current stream and advances the stream position by two bytes.</summary>
        ''' <param name="value">The two-byte signed integer to write.</param>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        Public Overrides Sub Write(ByVal value As Short)
            If ByteOrder = BinaryReader.ByteAlign.BigEndian Then value = MathT.LEBE(value)
            MyBase.Write(value)
        End Sub
        ''' <summary>Writes a two-byte unsigned integer to the current stream and advances the stream position by two bytes.</summary>
        ''' <param name="value">The two-byte unsigned integer to write.</param>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.ObjectDisposedException">The stream is closed.</exception>
        <CLSCompliant(False)> _
        Public Overrides Sub Write(ByVal value As UShort)
            If ByteOrder = BinaryReader.ByteAlign.BigEndian Then value = MathT.LEBE(value)
            MyBase.Write(value)
        End Sub
    End Class
End Namespace
#End If