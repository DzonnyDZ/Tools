Imports System.IO
Namespace MetadataT.ExifT
#If True
    ''' <summary>Provides low level access to stream of Exif data</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">–onny</author>
    ''' <version version="1.5.2">Exif data of type ASCII are required to be terminated with nullchar. The nullchar is trimmed.</version>
    <Version(1, 0, GetType(ExifReader), LastChange:="04/25/2007")> _
    Public Class ExifReader
        ''' <summary>Name of Exif Sub IFD (see <see cref="SubIFD.Desc"/>)</summary>
        Public Const ExifSubIFDName As String = "Exif Sub IFD"
        ''' <summary>Name of Exif Sub IFD (see <see cref="SubIFD.Desc"/>)</summary>
        Public Const GPSSubIFDName As String = "GPS Sub IFD"
        ''' <summary>Name of Exif Interoperability Sub IFD (see <see cref="IFDInterop"/>)</summary>
        Public Const ExifInteroperabilityName As String = "Exif Interoperability IFD"

#Region "Ctors"
        ''' <summary>CTor from <see cref="System.IO.Stream"/></summary>
        ''' <param name="Stream"><see cref="System.IO.Stream"/> that contains Exif data</param>
        ''' <exception cref="InvalidDataException">
        ''' Invalid byte order mark (other than 'II' or 'MM') at the beginning of stream -or-
        ''' Byte order test (2 bytes next to byte order mark, 3rd and 4th bytes in stream) don't avaluates to value 2Ah
        ''' </exception>
        ''' <exception cref="System.ObjectDisposedException">The source stream is closed.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached unexpectedly.</exception>
        ''' <exception cref="InvalidOperationException"><paramref name="Stream"/> is not zero-lenght or null and does not support seeking and reeding.</exception>
        Public Sub New(ByVal Stream As System.IO.Stream)
            Me.New(Stream, New ExifReaderSettings)
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
        ''' <exception cref="InvalidOperationException">Stream obtained from <paramref name="Container"/> is not zero-lenght or null and does not support seeking and reeding.</exception>
        Public Sub New(ByVal Container As IExifGetter)
            Me.new(Container, New ExifReaderSettings)
        End Sub
        ''' <summary>CTor from <see cref="System.IO.Stream"/></summary>
        ''' <param name="Stream"><see cref="System.IO.Stream"/> that contains Exif data</param>
        ''' <exception cref="InvalidDataException">
        ''' Invalid byte order mark (other than 'II' or 'MM') at the beginning of stream -or-
        ''' Byte order test (2 bytes next to byte order mark, 3rd and 4th bytes in stream) don't avaluates to value 2Ah
        ''' </exception>
        ''' <exception cref="System.ObjectDisposedException">The source stream is closed.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached unexpectedly.</exception>
        ''' <exception cref="InvalidOperationException"><paramref name="Stream"/> is not zero-lenght or null and does not support seeking and reeding.</exception>
        Public Sub New(ByVal Stream As IO.Stream, ByVal Settings As ExifReaderSettings)
            _Stream = Stream
            Me.Settings = New ExifReaderContext(Me, Settings)
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
        ''' <exception cref="InvalidOperationException">Stream obtained from <paramref name="Container"/> is not zero-lenght or null and does not support seeking and reeding.</exception>
        Public Sub New(ByVal Container As IExifGetter, ByVal Settings As ExifReaderSettings)
            If Container Is Nothing Then Throw New ArgumentNullException("Container")
            _Stream = Container.GetExifStream
            Me.Settings = New ExifReaderContext(Me, Settings)
            If _Stream Is Nothing OrElse _Stream.Length = 0 Then Exit Sub
            Parse()
        End Sub
        ''' <summary>Setting that apply on this parsing</summary>
        Private Settings As ExifReaderContext
#End Region
        ''' <summary>Parses stream of Exif data</summary>
        ''' <exception cref="InvalidDataException">
        ''' Invalid byte order mark (other than 'II' or 'MM') at the beginning of stream -or-
        ''' Byte order test (2 bytes next to byte order mark, 3rd and 4th bytes in stream) don't avaluates to value 2Ah
        ''' </exception>
        ''' <exception cref="System.ObjectDisposedException">The source stream is closed.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached unexpectedly.</exception>
        ''' <exception cref="InvalidOperationException"><see cref="Stream"/> does not support seeking and reading.</exception>
        Private Sub Parse()
            If Not Stream.CanSeek OrElse Not Stream.CanRead Then Throw New InvalidOperationException(ResourcesT.Exceptions.StreamDoesNotSupportSeekingOrReading)
            Stream.Position = 0
            Dim Reader As New Tools.IOt.BinaryReader(Stream, Tools.IOt.BinaryReader.ByteAlign.BigEndian)
            If Settings.OnItem(Me, ReaderItemKinds.Exif, True, Stream, , 0, Stream.Length) Then Exit Sub 'Event
            Dim BOM1 As Char = Reader.ReadChar
            Dim BOM2 As Char = Reader.ReadChar
            Settings.OnItem(Me, ReaderItemKinds.Bom, , , BOM1 & BOM2, 0, 2) 'Event
            If BOM1 = "I"c AndAlso BOM2 = "I"c Then
                Reader.ByteOrder = Tools.IOt.BinaryReader.ByteAlign.LittleEndian
            ElseIf BOM1 = "M"c AndAlso BOM2 = "M"c Then
                Reader.ByteOrder = Tools.IOt.BinaryReader.ByteAlign.BigEndian
            Else
                Settings.OnError(New InvalidDataException(ResourcesT.Exceptions.UnknownByteOrderMark & BOM1 & BOM2)) 'Throw?
                Reader.ByteOrder = IOt.BinaryReader.ByteAlign.LittleEndian 'Guess 50:50 probability
            End If
            _ByteOrder = Reader.ByteOrder
            Dim BOMTest As UShort = Reader.ReadUInt16
            Settings.OnItem(Me, ReaderItemKinds.BomTest, , , BOMTest, 2, 2) 'Event
            If BOMTest <> &H2AUS Then Throw New InvalidDataException(ResourcesT.Exceptions.InvalidValueForByteOrderTestAtExifHeader & Hex(BOMTest)) 'Unrecoverable
            Dim IFDOffset As UInt32 = Reader.ReadUInt32
            Settings.OnItem(Me, ReaderItemKinds.Ifd0Offset, , , IFDOffset, 4, 4) 'Event
            While IFDOffset <> 0UI
                Stream.Position = IFDOffset
                Dim CancelIFD As Boolean
                Dim IFDReader = New ExifIfdReader(Me, IFDOffset, Settings, CancelIFD)
                If Not CancelIFD Then _IFDs.Add(IFDReader)
                IFDOffset = IFDReader.NextIFD
            End While
        End Sub
        '''' <summary>Founds Sub IFDs that follows passed Sub IFD and adds them into <see cref="_OtherSubIFDs"/></summary>
        '''' <param name="Previous">Sub IFD that may contain offset to other Sub IFDs</param>
        '''' <param name="Container">IFD that contains all possibly found Sub IFDs</param>
        '''' <param name="MarkerIndex">Pointer to <see cref="ExifIFDReader.Entries"/></param>
        'Private Sub ParseNextSubIFDs(ByVal Previous As ExifIFDReader, ByVal Container As ExifIFDReader, ByVal MarkerIndex As Integer)
        '    Dim [Next] As UInteger = Previous.NextIFD
        '    Dim Parent As ExifIFDReader = Previous
        '    While [Next] <> 0
        '        Dim SubIFD As New ExifIFDReader(Me, [Next])
        '        [Next] = SubIFD.NextIFD
        '        _OtherSubIFDs.Add(New SubIFDReader(Me, [Next], "", Container, MarkerIndex, Parent))
        '        Parent = _OtherSubIFDs(_OtherSubIFDs.Count - 1)
        '    End While
        'End Sub
        '''' <summary>Contains value of the <see cref="OtherSubIFDs"/> property</summary>
        'Private _OtherSubIFDs As New List(Of SubIFDReader)
        '''' <summary>Contains all unexpectedly (by chance) found Sub IFDs that cannot be recognized as starndard one. Those Sub IFDs are usually found as successors of standard ones</summary>
        '''' <remarks>This collection doesnù contain standard Sub IFDs that was recognized like Exif Sub IFD</remarks>
        'Public ReadOnly Property OtherSubIFDs() As Tools.CollectionsT.GenericT.IReadOnlyList(Of SubIFDReader)
        '    Get
        '        Return New Tools.CollectionsT.GenericT.ReadOnlyListAdapter(Of SubIFDReader)(_OtherSubIFDs)
        '    End Get
        'End Property

        ''' <summary>Contains value of the <see cref="Stream"/> property</summary>
        Private _Stream As System.IO.Stream
        ''' <summary>Stream used to obtain Exif data</summary>
        Public ReadOnly Property Stream() As System.IO.Stream
            Get
                Return _Stream
            End Get
        End Property
        ''' <summary>Contains value for the <see cref="IFDs"/> property</summary>
        Private _IFDs As New List(Of ExifIfdReader)
        '''' <summary>Contains value for the <see cref="ExifSubIFD"/> property</summary>
        'Private _ExifSubIFD As SubIFD = Nothing
        '''' <summary>Contains value for the <see cref="GPSSubIFD"/> property</summary>
        'Private _GPSSubIFD As SubIFD = Nothing
        '''' <summary>Contains value for the <see cref="ExifInteroperabilityIFD"/> property</summary>
        'Private _ExifInteroperabilityIFD As SubIFD = Nothing
        '''' <summary>Returns Exif Sub IFD that contains data that are usually called Exif like setting of camera etc.</summary>
        'Public ReadOnly Property ExifSubIFD() As SubIFD
        '    Get
        '        Return _ExifSubIFD
        '    End Get
        'End Property
        '''' <summary>Returns GPS Sub IFD that contains GPS information.</summary>
        'Public ReadOnly Property GPSSubIFD() As SubIFD
        '    Get
        '        Return _GPSSubIFD
        '    End Get
        'End Property
        '''' <summary>Returns Exif Interoperability Sub IFD</summary>
        'Public ReadOnly Property ExifInteroperabilityIFD() As SubIFD
        '    Get
        '        Return _ExifInteroperabilityIFD
        '    End Get
        'End Property
        ''' <summary>Contains value of the <see cref="ByteOrder"/> property</summary>
        Private _ByteOrder As Tools.IOt.BinaryReader.ByteAlign
        ''' <summary>Byte order used by this <see cref="ExifReader"/></summary>
        Public ReadOnly Property ByteOrder() As Tools.IOt.BinaryReader.ByteAlign
            Get
                Return _ByteOrder
            End Get
        End Property
        ''' <summary>Collection of IFDs (Image File Directories) in this Exif block</summary>
        Public ReadOnly Property IFDs() As CollectionsT.GenericT.IReadOnlyList(Of ExifIfdReader)
            Get
                Return New CollectionsT.GenericT.ReadOnlyListAdapter(Of ExifIfdReader)(_IFDs)
            End Get
        End Property
    End Class


#End If
End Namespace
