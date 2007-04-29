Imports System.IO
Namespace Drawing.Metadata
#If Config <= Alpha Then 'Stage: Alpha
    ''' <summary>Provides low level access to stream of Exif data</summary>
    <Author("–onny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(ExifReader), LastChange:="4/25/2007")> _
    Public Class ExifReader
        ''' <summary>Name of Exif Sub IFD (see <see cref="SubIFD.Desc"/>)</summary>
        Public Const ExifSubIFDName As String = "Exif Sub IFD"
        ''' <summary>Name of Exif Sub IFD (see <see cref="SubIFD.Desc"/>)</summary>
        Public Const GPSSubIFDName As String = "GPS Sub IFD"
        ''' <summary>Name of Exif Interoperability Sub IFD (see <see cref="ExifInteroperabilityIFD"/>)</summary>
        Public Const ExifInteroperabilityName As String = "Exif Interoperability IFD"
        ''' <summary>CTor from <see cref="System.IO.Stream"/></summary>
        ''' <param name="Stream"><see cref="System.IO.Stream"/> that contains Exif data</param>
        ''' <exception cref="InvalidDataException">
        ''' Invalid byte order mark (other than 'II' or 'MM') at the beginning of stream -or-
        ''' Byte order test (2 bytes next to byte order mark, 3rd and 4th bytes in stream) don't avaluates to value 2Ah
        ''' </exception>
        ''' <exception cref="System.ObjectDisposedException">The source stream is closed.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached unexpectedly.</exception>
        Public Sub New(ByVal Stream As System.IO.Stream)
            _Stream = Stream
            If Stream Is Nothing OrElse Stream.Length = 0 Then Exit Sub
            Parse()
        End Sub
        ''' <summary>CTor from <see cref="IExifGetter"/></summary>
        ''' <param name="Container">Object that contains <see cref="System.IO.Stream"/> with Exif data</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Container"/> is null</exception>
        ''' <exception cref="InvalidDataException">
        ''' Invalid byte order mark (other than 'II' or 'MM') at the beginning of stream -or-
        ''' Byte order test (2 bytes next to byte order mark, 3rd and 4th bytes in stream) don't avaluates to value 2Ah
        ''' </exception>
        ''' <exception cref="System.ObjectDisposedException">The source stream is closed.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached unexpectedly.</exception>
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
        ''' <exception cref="System.ObjectDisposedException">The source stream is closed.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached unexpectedly.</exception>
        Private Sub Parse()
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
            'Exif Sub IFD
            If IFDs.Count >= 1 Then
                Dim i As Integer = 0
                For Each Entry As ExifIFDReader.DirectoryEntry In IFDs(0).Entries
                    If Entry.Tag = &H8769 AndAlso Entry.DataType = ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32 Then
                        'TODO: Compare to constant
                        _ExifSubIFD = New SubIFD(Me, Entry.Data, ExifSubIFDName, IFDs(0), i)
                        ParseNextSubIFDs(_ExifSubIFD, IFDs(0), i)
                        Exit For
                    End If
                    i += 1
                Next Entry
            End If
            'Exif Interoperability Sub IFD
            If ExifSubIFD IsNot Nothing Then
                Dim i As Integer = 0
                For Each Entry As ExifIFDReader.DirectoryEntry In ExifSubIFD.Entries
                    If Entry.Tag = &HA005 AndAlso Entry.DataType = ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32 Then
                        'TODO: Compare to constant
                        _ExifInteroperabilityIFD = New SubIFD(Me, Entry.Data, ExifInteroperabilityName, ExifSubIFD, i)
                        ParseNextSubIFDs(_ExifInteroperabilityIFD, ExifSubIFD, i)
                        Exit For
                    End If
                    i += 1
                Next Entry
            End If
            'Exif Sub IFD
            If IFDs.Count >= 1 Then
                Dim i As Integer = 0
                For Each Entry As ExifIFDReader.DirectoryEntry In IFDs(0).Entries
                    If Entry.Tag = &H8825 AndAlso Entry.DataType = ExifIFDReader.DirectoryEntry.ExifDataTypes.UInt32 Then
                        'TODO: Compare to constant
                        _GPSSubIFD = New SubIFD(Me, Entry.Data, GPSSubIFDName, IFDs(0), i)
                        ParseNextSubIFDs(_GPSSubIFD, IFDs(0), i)
                        Exit For
                    End If
                    i += 1
                Next Entry
            End If
        End Sub
        ''' <summary>Founds Sub IFDs that follows passed Sub IFD and adds them into <see cref="_OtherSubIFDs"/></summary>
        ''' <param name="Previous">Sub IFD that may contain offset to other Sub IFDs</param>
        ''' <param name="Container">IFD that contains all possibly found Sub IFDs</param>
        ''' <param name="MarkerIndex">Pointer to <see cref="ExifIFDReader.Entries"/></param>
        Private Sub ParseNextSubIFDs(ByVal Previous As ExifIFDReader, ByVal Container As ExifIFDReader, ByVal MarkerIndex As Integer)
            Dim [Next] As UInteger = Previous.NextIFD
            Dim Parent As ExifIFDReader = Previous
            While [Next] <> 0
                Dim SubIFD As New ExifIFDReader(Me, [Next])
                [Next] = SubIFD.NextIFD
                _OtherSubIFDs.Add(New SubIFD(Me, [Next], "", Container, MarkerIndex, Parent))
                Parent = _OtherSubIFDs(_OtherSubIFDs.Count - 1)
            End While
        End Sub
        ''' <summary>Represents Sub IFD</summary>
        Public Class SubIFD : Inherits ExifIFDReader
            ''' <summary>CTor</summary>
            ''' <param name="Exif"><see cref="ExifReader"/> that contains this IFD</param>
            ''' <param name="Offset">Offset of start of this IFD in <paramref name="Stream"/></param>
            ''' <param name="Desc">Descriptive name of this Sub IFD</param>
            ''' <param name="ParentIFD"><see cref="ExifIFDReader"/> that represent IFD that contains current Sub IFD</param>
            ''' <param name="ParentRecord">Point to <see cref="ExifIFDReader.Entries"/> collection that points to record that points to this Sub IFD</param>
            ''' <param name="PreviousSubIFD">Sub IFD which's <see cref="ExifIFDReader.NextIFD"/> points to this Sub IFD. Can be null if this is first Sub IFD in line</param>
            ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
            ''' <exception cref="System.IO.EndOfStreamException">The end of the Exif stream is reached unexpectedly.</exception>
            ''' <exception cref="InvalidEnumArgumentException">Directory entry of unknown data type found</exception>
            ''' <exception cref="InvalidDataException">Tag data of some are placed otside the tag and cannot be read</exception>
            <CLSCompliant(False)> _
            Public Sub New(ByVal Exif As ExifReader, ByVal Offset As UInt32, ByVal Desc As String, ByVal ParentIFD As ExifIFDReader, ByVal ParentRecord As Integer, Optional ByVal PreviousSubIFD As ExifIFDReader = Nothing)
                MyBase.New(Exif, Offset)
                _Desc = Desc
                _ParentIFD = ParentIFD
                _ParentRecord = ParentRecord
                _PreviousSubIFD = PreviousSubIFD
            End Sub
            ''' <summary>Contains value of the <see cref="Desc"/> property</summary>
            Private _Desc As String
            ''' <summary>Contains value of the <see cref="ParentIFD"/> property</summary>
            Private _ParentIFD As ExifIFDReader
            ''' <summary>Contains value of the <see cref="ParentRecord"/> property</summary>
            Private _ParentRecord As Integer
            ''' <summary>Contains value of the <see cref="PreviousSubIFD"/> property</summary>
            Private _PreviousSubIFD As ExifIFDReader
            ''' <summary>Descriptive name of this Sub IFD</summary>
            ''' <returns>Usually contain an empty string for non starndard Sub IFDs and comon English name for standard Sub IFDs. For non-standard Sub IFDs only when library have some ideda what can this Sub IFD mean this Sub IFD is captioned somehow</returns>
            ''' <remarks>Currently there are no Non Standard Sub IFDs that have any caption, Captions of staandard Sub IFDs are public onstants declared in <see cref="ExifReader"/></remarks>
            Public ReadOnly Property Desc() As String
                Get
                    Return _Desc
                End Get
            End Property
            ''' <summary><see cref="ExifIFDReader"/> that represent IFD that contains current Sub IFD</summary>
            Public ReadOnly Property ParentIFD() As ExifIFDReader
                Get
                    Return _ParentIFD
                End Get
            End Property
            ''' <summary>Point to <see cref="ExifIFDReader.Entries"/> collection that points to record that points to this Sub IFD</summary>
            Public ReadOnly Property ParentRecord() As Integer
                Get
                    Return _ParentRecord
                End Get
            End Property
            ''' <summary>Sub IFD which's <see cref="ExifIFDReader.NextIFD"/> points to this Sub IFD. Can be null if this is first Sub IFD in line</summary>
            ''' <remarks>This can be standart Sub IFD (like Exif Sub IFD) or nonstandart one</remarks>
            Public ReadOnly Property PreviousSubIFD() As ExifIFDReader
                Get
                    Return _PreviousSubIFD
                End Get
            End Property
        End Class
        ''' <summary>Contains value of the <see cref="OtherSubIFDs"/> property</summary>
        Private _OtherSubIFDs As New List(Of SubIFD)
        ''' <summary>Contains all unexpectedly (by chance) found Sub IFDs that cannot be recognized as starndard one. Those Sub IFDs are usually found as successors of standard ones</summary>
        ''' <remarks>This collection doesnù contain standard Sub IFDs that was recognized like Exif Sub IFD</remarks>
        Public ReadOnly Property OtherSubIFDs() As Tools.Collections.Generic.IReadOnlyList(Of SubIFD)
            Get
                Return New Tools.Collections.Generic.Wrapper(Of SubIFD)(_OtherSubIFDs)
            End Get
        End Property

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
        Private _ExifSubIFD As SubIFD = Nothing
        ''' <summary>Contains value for the <see cref="GPSSubIFD"/> property</summary>
        Private _GPSSubIFD As SubIFD = Nothing
        ''' <summary>Contains value for the <see cref="ExifInteroperabilityIFD"/> property</summary>
        Private _ExifInteroperabilityIFD As SubIFD = Nothing
        ''' <summary>Returns Exif Sub IFD that contains data that are usually called Exif like setting of camera etc.</summary>
        Public ReadOnly Property ExifSubIFD() As SubIFD
            Get
                Return _ExifSubIFD
            End Get
        End Property
        ''' <summary>Returns GPS Sub IFD that contains GPS information.</summary>
        Public ReadOnly Property GPSSubIFD() As SubIFD
            Get
                Return _GPSSubIFD
            End Get
        End Property
        ''' <summary>Returns Exif Interoperability Sub IFD</summary>
        Public ReadOnly Property ExifInteroperabilityIFD() As SubIFD
            Get
                Return _ExifInteroperabilityIFD
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
