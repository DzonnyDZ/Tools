Imports System.IO
Namespace DrawingT.MetadataT
#If Config <= Alpha Then 'Stage: Alpha
    ''' <summary>Provides low level access to stream of IPTC data</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(IPTCReader), LastChMMDDYYYY:="04/25/2007")> _
    Public Class IPTCReader
        ''' <summary>CTor from <see cref="System.IO.Stream"/></summary>
        ''' <param name="Stream"><see cref="System.IO.Stream"/> that contains IPTC data</param>
        ''' <exception cref="InvalidDataException">Tag marker other than 1Ch found</exception>
        ''' <exception cref="NotSupportedException">Extended-size tag found</exception>
        Public Sub New(ByVal Stream As System.IO.Stream)
            _Stream = Stream
            If Stream Is Nothing OrElse Stream.Length = 0 Then Exit Sub
            Parse()
        End Sub
        ''' <summary>CTor from <see cref="IIPTCGetter"/></summary>
        ''' <param name="Container">Object that contains <see cref="System.IO.Stream"/> with IPTC data</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Container"/> is null</exception>
        ''' <exception cref="InvalidDataException">Tag marker other than 1Ch found</exception>
        ''' <exception cref="NotSupportedException">Extended-size tag found</exception>
        Public Sub New(ByVal Container As IIPTCGetter)
            If Container Is Nothing Then Throw New ArgumentNullException("Container", "Container cannot be null")
            _Stream = Container.GetIPTCStream
            If _Stream Is Nothing OrElse _Stream.Length = 0 Then Exit Sub
            Parse()
        End Sub
        ''' <summary>Parses stream of IPTC data</summary>
        ''' <exception cref="InvalidDataException">Tag marker other than 1Ch found</exception>
        ''' <exception cref="NotSupportedException">Extended-size tag found</exception>
        Private Sub Parse()
            Stream.Position = 0
            Dim r As New Tools.IOt.BinaryReader(Stream, Tools.IOt.BinaryReader.ByteAling.BigEndian)
            Do
                Dim TagMarker As Byte = r.ReadByte
                If TagMarker <> &H1C Then Throw New InvalidDataException("Tag marker must be 1Ch")
                Dim RecordNumber As Byte = r.ReadByte
                Dim Tag As Byte = r.ReadByte
                Dim Size As UShort = r.ReadUInt16
                If Size >= &H8000 Then Throw New NotSupportedException("Extended-size tags are not supported")
                Dim Data As Byte() = r.ReadBytes(Size)
                _Records.Add(New IPTCRecord(RecordNumber, Tag, Size, Data))
            Loop While Stream.Position + 3 < Stream.Length
        End Sub
        ''' <summary>Contains value of the <see cref="Records"/> property</summary>
        Private _Records As New List(Of IPTCRecord)
        ''' <summary>Contains value of the <see cref="Stream"/> property</summary>
        Private _Stream As System.IO.Stream
        ''' <summary>Stream used to obtain Exif data</summary>
        Public ReadOnly Property Stream() As System.IO.Stream
            Get
                Return _Stream
            End Get
        End Property
        ''' <summary>Records in IPTC stream</summary>
        Public ReadOnly Property Records() As CollectionsT.GenericT.IReadOnlyList(Of IPTCRecord)
            Get
                Return New CollectionsT.GenericT.ReadOnlyListAdapter(Of IPTCRecord)(_Records)
            End Get
        End Property

        ''' <summary>Repreesents one IPTC record</summary>
        Public Class IPTCRecord
            ''' <summary>CTor</summary>
            ''' <param name="RecordNumber">Record group number</param>
            ''' <param name="Tag">Data set number (usually called tag)</param>
            ''' <param name="Size">Size of record - only not-extended sizes are supported</param>
            ''' <param name="Data">Record data</param>
            <CLSCompliant(False)> _
            Public Sub New(ByVal RecordNumber As Byte, ByVal Tag As Byte, ByVal Size As UShort, ByVal Data As Byte())
                _RecordNumber = RecordNumber
                _Tag = Tag
                _Size = Size
                _Data = Data
            End Sub
            Private _RecordNumber As Byte
            Private _Tag As Byte
            Private _Size As UShort
            Private _Data As Byte()
            ''' <summary>Group number of record</summary>
            ''' <remarks>Usually 02h</remarks>
            Public ReadOnly Property RecordNumber() As Byte
                Get
                    Return _RecordNumber
                End Get
            End Property
            ''' <summary>Data set number (usually called tag)</summary>
            Public ReadOnly Property Tag() As Byte
                Get
                    Return _Tag
                End Get
            End Property
            ''' <summary>Size of record data</summary>
            <CLSCompliant(False)> _
            Public ReadOnly Property Size() As UShort
                Get
                    Return _Size
                End Get
            End Property
            ''' <summary>Record data as <see cref="Array"/> of <see cref="Byte"/>s</summary>
            Public ReadOnly Property Data() As Byte()
                Get
                    Return _Data
                End Get
            End Property
            ''' <summary>Record data as <see cref="String"/></summary>
            ''' <param name="Encoding"><see cref="System.Text.Encoding"/> used to convert <see cref="Byte"/>s to <see cref="String"/>. If ommited <see cref="System.Text.Encoding.[Default]"/> is used</param>
            Public ReadOnly Property StringData(Optional ByVal Encoding As System.Text.Encoding = Nothing) As String
                Get
                    If Encoding Is Nothing Then Encoding = System.Text.Encoding.Default
                    Return Encoding.GetString(Data)
                End Get
            End Property
        End Class
    End Class
#End If
End Namespace
