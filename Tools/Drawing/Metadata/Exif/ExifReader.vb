Imports System.IO
Namespace Drawing.Metadata
#If Config <= Nightly Then 'Stage: Nigtly
    ''' <summary>Provides low level access to stream of Exif data</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(ExifReader), LastChange:="4/24/2007")> _
    Public Class ExifReader
        ''' <summary>CTor from <see cref="System.IO.Stream"/></summary>
        ''' <param name="Stream"><see cref="System.IO.Stream"/> that contains Exif data</param>
        Public Sub New(ByVal Stream As System.IO.Stream)
            _Stream = Stream
            If Stream Is Nothing OrElse Stream.Length = 0 Then Exit Sub
            Parse()
        End Sub
        ''' <summary>CTor from <see cref="IExifGetter"/></summary>
        ''' <param name="Container">Object that contains <see cref="System.IO.Stream"/> with Exif data</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Container"/> is null</exception>
        Public Sub New(ByVal Container As IExifGetter)
            If Container Is Nothing Then Throw New ArgumentNullException("Container", "Container cannot be null")
            _Stream = Container.GetExifStream
            If _Stream Is Nothing OrElse _Stream.Length = 0 Then Exit Sub
            Parse()
        End Sub
        ''' <summary>Parses stream of Exif data</summary>
        ''' <exception cref="InvalidDataException">
        ''' Invalid byte order mark (other than 'II' or 'MM') at the beginning of stream -or-
        ''' Byte order test (2 bytes next to byte order mark, 3rd and 4th bytes in stream) don't avaluates to value 2Ah
        ''' </exception>
        Private Sub Parse() 'ASAP:Excepotions and exceptions for callers
            Stream.Position = 0
            Dim Reader As New Tools.IO.BinaryReader(Stream, Tools.IO.BinaryReader.ByteAling.BigEndian)
            Dim BOM1 As Char = Reader.ReadChar
            Dim BOM2 As Char = Reader.ReadChar
            If BOM1 = "I"c AndAlso BOM2 = "I"c Then
                Reader.ByteOrder = Tools.IO.BinaryReader.ByteAling.LittleEndian
            ElseIf BOM1 = "M"c AndAlso BOM2 = "M"c Then
                Reader.ByteOrder = Tools.IO.BinaryReader.ByteAling.BigEndian
            Else
                Throw New InvalidDataException("Unknown byte order mark " & BOM1 & BOM2)
            End If
            _ByteOrder = Reader.ByteOrder
            Dim BOMTest As UShort = Reader.ReadUInt16
            If BOMTest <> &H2AUS Then Throw New InvalidDataException("Invalid value for byte order test at Exif header " & Hex(BOMTest))

            Dim IFDOffset As UInt32 = Reader.ReadUInt32
            While IFDOffset <> 0UI
                Stream.Position = IFDOffset
                _IFDs.Add(New ExifIFDReader(Me, IFDOffset))
                IFDOffset = _IFDs(_IFDs.Count - 1).NextIFD
            End While
            If IFDs.Count >= 1 Then
                Dim i As Integer = 0
                For Each Entry As ExifIFDReader.DirectoryEntry In IFDs(0).Entries
                    If Entry.Tag = &H8769 AndAlso Entry.DataType = ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32 Then
                        _ExifSubIFDIndex = i
                        _ExifSubIFDOffset = Entry.Data
                        _ExifSubIFD = New ExifIFDReader(Me, _ExifSubIFDOffset)
                        Exit For
                    End If
                    i += 1
                Next Entry
            End If
        End Sub
        ''' <summary>Contains value of the <see cref="Stream"/> property</summary>
        Private _Stream As System.IO.Stream
        ''' <summary>Stream used to obtain Exif data</summary>
        Public ReadOnly Property Stream() As System.IO.Stream
            Get
                Return _Stream
            End Get
        End Property
        ''' <summary>Contains value for the <see cref="IFDs"/> property</summary>
        Private _IFDs As New List(Of ExifIFDReader)
        ''' <summary>Contains value for the <see cref="ExifSubIFD"/> property</summary>
        Private _ExifSubIFD As ExifIFDReader = Nothing
        ''' <summary>Contains value for the <see cref="ExifSubIFDIndex"/> property</summary>
        Private _ExifSubIFDIndex As Integer = -1
        ''' <summary>Contains value for the <see cref="ExifSubIFDOffset"/> property</summary>
        Private _ExifSubIFDOffset As UInt32 = 0
        ''' <summary>Returns Exif Sub IFD that contains data that are usually called Exif like setting of camera etc.</summary>
        Public ReadOnly Property ExifSubIFD() As ExifIFDReader
            Get
                Return _ExifSubIFD
            End Get
        End Property
        ''' <summary>Index of entry that points to Exif Sub IFD</summary>
        ''' <returns>Index to collection <see cref="ExifIFDReader.Entries"/> of 0th element of <see cref="IFDs"/> of tag that points to Exif Sub IFD or -1 whre there are no Exif Sub IFD</returns>
        Public ReadOnly Property ExifSubIFDIndex() As Integer
            Get
                Return _ExifSubIFDIndex
            End Get
        End Property
        ''' <summary>Offset of Exif Sub IFD relative to TIFF header</summary>
        ''' <returns>Offset of Exif Sub IFD relative to TIFF header or 0 if there if no Exif Sub IFD</returns>
        <CLSCompliant(False)> _
        Public ReadOnly Property ExifSubIFDOffset() As UInt32
            Get
                Return _ExifSubIFDOffset
            End Get
        End Property

        ''' <summary>Contains value of the <see cref="ByteOrder"/> property</summary>
        Private _ByteOrder As Tools.IO.BinaryReader.ByteAling
        ''' <summary>Byte order used by this <see cref="ExifReader"/></summary>
        Public ReadOnly Property ByteOrder() As Tools.IO.BinaryReader.ByteAling
            Get
                Return _ByteOrder
            End Get
        End Property
        ''' <summary>Collection of IFDs (Image File Directories) in this Exif block</summary>
        Public ReadOnly Property IFDs() As Collections.Generic.IReadOnlyList(Of ExifIFDReader)
            Get
                Return New Collections.Generic.ReadOnlyListAdapter(Of ExifIFDReader)(_IFDs)
            End Get
        End Property
    End Class
#End If
End Namespace
