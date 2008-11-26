Imports System.IO, Tools.IOt
Namespace DrawingT.DrawingIOt.JPEG
#If Config <= Alpha Then 'Stage: Alpha
    ''' <summary>Provides tools realted to reading from JPEG graphic file format on low level</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
  ''' <version version="1.5.2" stage="Alpha"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Class JPEGReader
        Implements MetadataT.ExifT.IExifGetter, MetadataT.IptcT.IIptcGetter
        Implements MetadataT.IptcT.IIptcWriter
        Implements IDisposable
        ''' <summary>Stream of opened file</summary>
        Protected ReadOnly Stream As System.IO.Stream
        ''' <summary>Stream of whole JPEG file</summary>
        Public ReadOnly Property JPEGStream() As System.IO.Stream
            Get
                Return Stream
            End Get
        End Property
        ''' <summary>CTor from file</summary>
        ''' <param name="Path">Path to file to read from</param>
        ''' <param name="Write">Opens file for writing as well as for reading if true. This is necessary for <see cref="IPTCEmbed"/> to work</param>
        ''' <exception cref="System.IO.DirectoryNotFoundException">The specified <paramref name="path"/> is invalid, such as being on an unmapped drive.</exception>
        ''' <exception cref="System.ArgumentNullException"><paramref name="path"/> is null.</exception>
        ''' <exception cref="System.UnauthorizedAccessException">The access requested (readonly) is not permitted by the operating system for the specified path.</exception>
        ''' <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        ''' <exception cref="System.ArgumentException"><paramref name="path"/> is an empty string (""), contains only white space, or contains one or more invalid characters.</exception>
        ''' <exception cref="System.IO.FileNotFoundException">The file cannot be found.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.PathTooLongException">The specified <paramref name="path"/>, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
        ''' <exception cref="InvalidDataException">
        ''' Invalid JPEG marker found (code doesn't start with FFh, length set to 0 or 2) -or-
        ''' JPEG stream doesn't start with corect SOI marker -or-
        ''' JPEG stream doesn't end with corect EOI marker
        ''' </exception>
        Public Sub New(ByVal Path As String, Optional ByVal Write As Boolean = False)
            '#If VBC_VER >= 9 Then
            Stream = New System.IO.FileStream(Path, System.IO.FileMode.Open, If(Write, System.IO.FileAccess.ReadWrite, System.IO.FileAccess.Read), System.IO.FileShare.Read)
            '#Else
            '            Stream = New System.IO.FileStream(Path, System.IO.FileMode.Open, VisualBasicT.iif(Write, System.IO.FileAccess.ReadWrite, System.IO.FileAccess.Read), System.IO.FileShare.Read)
            '#End If
            CloseStreamOnDispose = True
            Parse()
        End Sub
        ''' <summary>CTor from stream</summary>
        ''' <param name="Stream"><see cref="System.IO.Stream"/> to read data from</param>
        ''' <exception cref="NotSupportedException"><paramref name="Stream"/> doesn't support read or seek (the <see cref="System.IO.Stream.CanRead"/> or <see cref="System.IO.Stream.CanSeek"/> returns false)</exception>
        ''' <exception cref="InvalidDataException">
        ''' Invalid JPEG marker found (code doesn't start with FFh, length set to 0 or 2) -or-
        ''' JPEG stream doesn't start with corect SOI marker -or-
        ''' JPEG stream doesn't end with corect EOI marker
        ''' </exception>
        ''' <remarks>The <paramref name="Stream"/> is not automatically closed when instance is disposed</remarks>
        Public Sub New(ByVal Stream As System.IO.Stream)
            If Stream.CanRead AndAlso Stream.CanSeek Then
                Me.Stream = Stream
                CloseStreamOnDispose = False
            Else
                Throw New NotSupportedException(ResourcesT.Exceptions.StreamToReadJPEGFromMustBeAbleToSeekAndRead)
            End If
            Parse()
        End Sub
        ''' <summary>Contains value of the <see cref="Markers"/> property</summary>
        Private _Markers As New List(Of JPEGMarkerReader)
        ''' <summary>Contains value of the <see cref="ImageStream"/> property</summary>
        Private _ImageStream As Stream
        ''' <summary>Parses given JPEG file and extracts JPEG markers and image data streams</summary>
        ''' <exception cref="InvalidDataException">
        ''' Invalid JPEG marker found (code doesn't start with FFh, length set to 0 or 2) -or-
        ''' JPEG stream doesn't start with corect SOI marker -or-
        ''' JPEG stream doesn't end with corect EOI marker -or-
        ''' EOI not found at the end of image
        ''' </exception>
        Private Sub Parse()
            Dim SOI As New JPEGMarkerReader(Stream, 0)
            If SOI.MarkerCode <> JPEGMarkerReader.Markers.SOI OrElse SOI.Length <> 0 Then _
                Throw New InvalidDataException(ResourcesT.Exceptions.JPEGFileDoesnTStartWithCorrectSOIMarker)
            _Markers.Add(SOI)
            Dim Pos As Long = 2
            Dim Marker As JPEGMarkerReader
            Do
                Marker = New JPEGMarkerReader(Stream, Pos)
                Pos += 2 + Marker.Length
                _Markers.Add(Marker)
            Loop Until Marker.MarkerCode = JPEGMarkerReader.Markers.SOS
            Dim CS As New Tools.IOt.ConstrainedReadOnlyStream(Stream, Pos, Stream.Length - Pos)
            Dim EOI As JPEGMarkerReader = Nothing
            For i As Long = CS.Length - 1 To 1 Step -1
                Dim EOI1 As Byte = CS(i - 1)
                Dim EOI2 As Byte = CS(i)
                If EOI1 = &HFF AndAlso EOI2 = &HD9 Then
                    EOI = New JPEGMarkerReader(Stream, Pos + i - 1)
                    _ImageStream = New ConstrainedReadOnlyStream(Stream, Pos, i - 1)
                    Exit For
                End If
            Next i
            If EOI Is Nothing Then Throw New InvalidDataException(ResourcesT.Exceptions.EOINotFound)
            If EOI.MarkerCode <> JPEGMarkerReader.Markers.EOI OrElse EOI.Length <> 0 Then _
                Throw New InvalidDataException(ResourcesT.Exceptions.JPEGFileDoesnTEndWithCorrectEOIMarker)
            _Markers.Add(EOI)
        End Sub
        ''' <summary>List of markers this JPEG stream</summary>
        Public ReadOnly Property Markers() As CollectionsT.GenericT.IReadOnlyList(Of JPEGMarkerReader)
            Get
                Return New CollectionsT.GenericT.ReadOnlyListAdapter(Of JPEGMarkerReader)(_Markers)
            End Get
        End Property
        ''' <summary>Stream constrained to Image stream part of JPEG stream</summary>
        Public ReadOnly Property ImageStream() As Stream
            Get
                Return _ImageStream
            End Get
        End Property
        ''' <summary>Gets stream of Exif data</summary>
        ''' <remarks>
        ''' <para>Stream content starts with TIFF header</para>
        ''' <para>If there is no Exif data in file stream can be null or have zero length</para>
        ''' <para>Stream supports reading and seeking</para>
        ''' </remarks>
        Public Function GetExifStream() As System.IO.Stream Implements MetadataT.ExifT.IExifGetter.GetExifStream
            Const Exif$ = "Exif"
            Const NormalOffset% = 6
            Const FujiFilmFineFix2800ZoomOffset% = 10
            If _ExifMarkerIndex = -1 Then
                Return Nothing
            ElseIf _ExifMarkerIndex >= 0 Then
                Dim Offset = If(SupportFujiFilmFineFix2800ZoomInEffect, FujiFilmFineFix2800ZoomOffset, NormalOffset)
                Return New ConstrainedReadOnlyStream(Me.Markers(ExifMarkerIndex).Data, Offset, Me.Markers(ExifMarkerIndex).Data.Length - Offset)
            End If
            Dim i As Integer = 0
            For Each m As JPEGMarkerReader In Me.Markers
                If m.MarkerCode = JPEGMarkerReader.Markers.APP1 Then
                    m.Data.Position = 0
                    Dim Bytes(If(SupportFujiFilmFineFix2800Zoom, 9, 6)) As Byte
                    If m.Data.Read(Bytes, 0, 6) = 6 AndAlso System.Text.Encoding.ASCII.GetString(Bytes, 0, 4) = Exif AndAlso Bytes(4) = 0 AndAlso Bytes(5) = 0 Then
                        _ExifMarkerIndex = i
                        _SupportFujiFilmFineFix2800ZoomInEffect = False
                        Return New ConstrainedReadOnlyStream(m.Data, NormalOffset, m.Data.Length - NormalOffset)
                    ElseIf SupportFujiFilmFineFix2800Zoom AndAlso m.Data.Read(Bytes, 6, 4) = 4 AndAlso Bytes(0) = &HFF AndAlso Bytes(1) = &HE1 _
                           AndAlso m.Length - 4 = (CUShort(Bytes(2)) << 8 Or CUShort(Bytes(3))) _
                           AndAlso System.Text.Encoding.ASCII.GetString(Bytes, 4, 4) = Exif AndAlso Bytes(8) = 0 AndAlso Bytes(9) = 0 Then
                        'This format have been seen in image from FujiFilm FinePix 2800 zoom
                        'I consider it non-standard and stupid and there fore it is supported only on special request
                        'The format is: APP1 marker starts with FFE1 (as usual) then 2-bytes lenght folloes (as usual)
                        '               Then FFE1 is repeated and size of Exif stream + size itself follows (it is m.Length - 4)

                        'Dim b1 = SupportFujiFilmFineFix2800Zoom
                        'Dim b2 = m.Data.Read(Bytes, 6, 4) = 4
                        'Dim b3 = Bytes(0) = &HFF
                        'Dim b4 = Bytes(1) = &HE1
                        'Dim b5 = m.Length - 4 = (CUShort(Bytes(2)) << 8 Or CUShort(Bytes(3)))
                        'Dim b6 = System.Text.Encoding.ASCII.GetString(Bytes, 4, 4) = Exif
                        'Dim b7 = Bytes(8) = 0
                        'Dim b8 = Bytes(9) = 0
                        'Dim b = b1 AndAlso b2 AndAlso b3 AndAlso b4 AndAlso b5 AndAlso b6 AndAlso b7 AndAlso b8
                        'If b Then
                        _ExifMarkerIndex = i
                        _SupportFujiFilmFineFix2800ZoomInEffect = True
                        Return New ConstrainedReadOnlyStream(m.Data, FujiFilmFineFix2800ZoomOffset, m.Data.Length - FujiFilmFineFix2800ZoomOffset)
                        'End If
                    End If
                End If
                i += 1
            Next m
            _SupportFujiFilmFineFix2800ZoomInEffect = False
            _ExifMarkerIndex = -1
            Return Nothing
        End Function
        ''' <summary>Contains value of the <see cref="SupportFujiFilmFineFix2800Zoom"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _SupportFujiFilmFineFix2800Zoom As Boolean = False
        ''' <summary>Gets or sets value indicating if non-standard way of embdeding Exif data seen in file from FujiFilm FineFix 2800 is supported</summary>
        ''' <returns>True if the format is supported, false if not</returns>
        ''' <value>Set value of this property prior of calling <see cref="GetExifStream"/> or <see cref="ExifMarkerIndex"/>.</value>
        ''' <remarks>FujiFilm FinePix 2800 Zomm adds 4 more bytes between APP1 marker size indication and start of Exif block. This is considered non-standard and supported only when this property is set to true.
        ''' <para>The bytes added has following meaning: 2 bytes FFE1 (repeated APP1 marker); 2 bytes size (unsigned, big endian). The is by 4 bytes lower then size stored in APP1 marker itself.</para></remarks>
        Public Property SupportFujiFilmFineFix2800Zoom() As Boolean
            Get
                Return _SupportFujiFilmFineFix2800Zoom
            End Get
            Set(ByVal value As Boolean)
                _SupportFujiFilmFineFix2800Zoom = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="SupportFujiFilmFineFix2800ZoomInEffect"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _SupportFujiFilmFineFix2800ZoomInEffect As Boolean = False
        ''' <summary>Gets value indicating if <see cref="SupportFujiFilmFineFix2800Zoom"/> property being set to true has effect</summary>
        ''' <returns>True if Exif data was found and are in format recognized only when the <see cref="SupportFujiFilmFineFix2800Zoom"/> was true</returns>
        Public ReadOnly Property SupportFujiFilmFineFix2800ZoomInEffect() As Boolean
            Get
                Return _SupportFujiFilmFineFix2800ZoomInEffect
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="ExifMarkerIndex"/> property</summary>
        ''' <remarks>If value is less than -1 <see cref="ExifMarkerIndex"/> has not been aquired, if value is -1 then there if no Exif data</remarks>
        Private _ExifMarkerIndex As Integer = -2
        ''' <summary>Gets index of marker which stores Exif data</summary>
        ''' <returns>Index of marker into <see cref="Markers"/> collection in which Exif data are stored, -1 if there are no Exif data</returns>
        Public ReadOnly Property ExifMarkerIndex() As Integer
            Get
                If _ExifMarkerIndex < -1 Then GetExifStream()
                Return _ExifMarkerIndex
            End Get
        End Property
        ''' <summary>Gets stream of IPTC data</summary>
        ''' <remarks>
        ''' <para>Stream content starts with first tag marker 1Ch of IPTC stream</para>
        ''' <para>If there is no IPTC data in file stream can be null or have zero length</para>
        ''' <para>Stream supports reading and seeking</para>
        ''' </remarks>
        ''' <exception cref="IOException">IO error while reding Photoshop block stream</exception>
        ''' <exception cref="EndOfStreamException">End of stream Photoshop block stream reached unexpectedly</exception>
        ''' <exception cref="InvalidDataException">
        ''' An 8BIM segment doesn't start with string '8BIM'
        ''' Sum of reported size and start of an 8BIM segment data exceeds length of Photoshop block stream
        ''' </exception>
        Public Function GetIPTCStream() As System.IO.Stream Implements MetadataT.IptcT.IIptcGetter.GetIptcStream
            For Each BIM8 As Photoshop8BIMReader In Get8BIMSegments()
                If BIM8.Type = IPTC8BIM Then
                    Return BIM8.Data
                End If
            Next BIM8
            Return Nothing
        End Function
        ''' <summary>Value of <see cref="Photoshop8BIMReader.Type"/> of 8BIM segment that contains IPTC stream</summary>
        Private Const IPTC8BIM As UShort = &H404
        ''' <summary>Gets index of 8BIM segment which stores IPTC data</summary>
        ''' <returns>Index of 8BIM segment into <see cref="Get8BIMSegments"/> collection in which IPTC data are stored, -1 if there are no IPTC data</returns>
        Public ReadOnly Property IPTC8BIMSegmentIndex() As Integer
            Get
                Dim i As Integer = 0
                For Each BIM8 As Photoshop8BIMReader In Get8BIMSegments()
                    If BIM8.Type = IPTC8BIM Then
                        Return i
                    End If
                    i += 1
                Next BIM8
                Return -1
            End Get
        End Property
        ''' <summary>Parses Photoshop segment in current JPEG image and return all 8BIM segments contained in it</summary>
        ''' <returns>Collection of <see cref="Photoshop8BIMReader"/> representing all 8BIM segments contained in Photoshop block of current image, an empty collection if there are no 8BIM segments (or no Photoshop block or no APP13 marker)</returns>
        ''' <exception cref="IOException">IO error while reding Photoshop block stream</exception>
        ''' <exception cref="EndOfStreamException">End of stream Photoshop block stream reached unexpectedly</exception>
        ''' <exception cref="InvalidDataException">
        ''' An 8BIM segment doesn't start with string '8BIM'
        ''' Sum of reported size and start of an 8BIM segment data exceeds length of Photoshop block stream
        ''' </exception>
        Public Function Get8BIMSegments() As CollectionsT.GenericT.IReadOnlyList(Of Photoshop8BIMReader)
            Dim Segments As New List(Of Photoshop8BIMReader)
            Dim Pos As Long = 0
            Dim Photoshop As System.IO.Stream = GetPhotoShopStream()
            If Photoshop Is Nothing OrElse Photoshop.Length = 0 Then _
                Return New CollectionsT.GenericT.ReadOnlyListAdapter(Of Photoshop8BIMReader)(Segments)
            Do
                Dim Sgm As New Photoshop8BIMReader(Photoshop, Pos)
                Pos += Sgm.WholeSize
                Segments.Add(Sgm)
            Loop While Pos < Photoshop.Length
            Return New CollectionsT.GenericT.ReadOnlyListAdapter(Of Photoshop8BIMReader)(Segments)
        End Function
        ''' <summary>Gets stream of PhotoShop data</summary>
        ''' <remarks>
        ''' <para>Stream content starts with marker of first 8BIM segment</para>
        ''' <para>If there is no Photoshop data in file stream can be null or have zero length</para>
        ''' <para>Stream supports reading and seeking</para>
        ''' </remarks>
        Public Function GetPhotoShopStream() As System.IO.Stream
            Const PhotoShop$ = "Photoshop 3.0"
            If _PhotoshopMarkerIndex = -1 Then
                Return Nothing
            ElseIf _PhotoshopMarkerIndex >= 0 Then
                Return New ConstrainedReadOnlyStream(Me.Markers(PhotoshopMarkerIndex).Data, 14, Me.Markers(PhotoshopMarkerIndex).Data.Length - 14)
            End If
            Dim i As Integer = 0
            For Each m As JPEGMarkerReader In Me.Markers
                If m.MarkerCode = JPEGMarkerReader.Markers.APP13 Then
                    m.Data.Position = 0
                    Dim Bytes(13) As Byte
                    If m.Data.Read(Bytes, 0, 14) = 14 Then
                        Dim PhotoshopH As String = System.Text.Encoding.ASCII.GetString(Bytes, 0, 13)
                        If PhotoshopH = PhotoShop AndAlso Bytes(13) = 0 Then
                            _PhotoshopMarkerIndex = i
                            Return New ConstrainedReadOnlyStream(m.Data, 14, m.Data.Length - 14)
                        End If
                    End If
                End If
                i += 1
            Next m
            _PhotoshopMarkerIndex = -1
            Return Nothing
        End Function
        ''' <summary>Contains value of the <see cref="PhotoshopMarkerIndex"/> property</summary>
        ''' <remarks>If value is less than -1 <see cref="PhotoshopMarkerIndex"/> has not been aquired yet, if value is -1 then there if no Photoshop data</remarks>
        Private _PhotoshopMarkerIndex As Integer = -2
        ''' <summary>Gets index of marker which stores Photoshop data</summary>
        ''' <returns>Index of marker into <see cref="Markers"/> collection in which Photoshop data are stored, -1 if there are no Photoshop data</returns>
        Public ReadOnly Property PhotoshopMarkerIndex() As Integer
            Get
                If _PhotoshopMarkerIndex < -1 Then GetPhotoShopStream()
                Return _PhotoshopMarkerIndex
            End Get
        End Property
        ''' <summary>Writes given IPTC data to stream of JPEG file</summary>
        ''' <param name="IPTCData">Data to be written</param>
        ''' <remarks>either replaces existing IPTC data, adds new 8BIM segment or adds new APP14 marker</remarks>
        ''' <exception cref="InvalidOperationException">No JPEG marker found</exception>
        ''' <exception cref="IOException">An IO error occurs</exception>
        ''' <exception cref="ObjectDisposedException"><see cref="Stream"/> is closed</exception>
        ''' <exception cref="NotSupportedException">
        ''' <see cref="Stream"/> does not support seeking -or-
        ''' <see cref="Stream"/> does not support writing -or-
        ''' <see cref="Stream"/> does not suport reading
        ''' </exception>
        Public Sub IPTCEmbed(ByVal IPTCData() As Byte) Implements MetadataT.IptcT.IIptcWriter.IptcEmbded
            Dim PreData As Byte() 'Write before IPTCData
            Dim PreDataPos As Integer 'Where to start writing of PreData
            Dim LenghtToReplace As Integer 'Number of types to replace after PreDataPos
            Dim PostData As Byte() 'Write after IPTCData
            Dim Overwrite As New Dictionary(Of Integer, UShort) 'Another bytes to be owerwriten
            If Me.PhotoshopMarkerIndex >= 0 Then
                Dim APP14SizePos As Integer = DirectCast(Me.Markers(Me.PhotoshopMarkerIndex).Data, ConstrainedReadOnlyStream).TranslatePosition(0)
                APP14SizePos -= 2 'Byte where APP13's size is stored
                If Me.IPTC8BIMSegmentIndex >= 0 Then
                    Dim BIM8 As Photoshop8BIMReader = Me.Get8BIMSegments(Me.IPTC8BIMSegmentIndex)
                    'Embed data into existing 8BIM segment
                    PreDataPos = DirectCast(BIM8.Data, ConstrainedReadOnlyStream).TranslatePosition(0) 'Start of this 8BIM
                    PreDataPos -= 4 'Position of size identifier of 8BIM
                    Dim s As New MemoryStream(4)
                    Dim w As New IO.BinaryWriter(s)
                    w.Write(MathT.LEBE(CUInt(IPTCData.Length)))
                    ReDim PreData(3)
                    Array.ConstrainedCopy(s.GetBuffer, 0, PreData, 0, 4)

                    Dim CurrIPTCStreamLen As Long = Me.GetIPTCStream.Length
                    '#If Framework >= 3.5 Then
                    Dim Pad As Byte = If((CurrIPTCStreamLen) Mod 2 = 0, 0, 1)
                    '#Else
                    '                    Dim Pad As Byte = VisualBasicT.iif((CurrIPTCStreamLen) Mod 2 = 0, 0, 1)
                    '#End If
                    Overwrite.Add(APP14SizePos, Me.Markers(Me.PhotoshopMarkerIndex).Length + (IPTCData.Length - (CurrIPTCStreamLen + Pad))) 'New length of APP14
                    LenghtToReplace = 4 + CurrIPTCStreamLen + Pad
                Else
                    'Add new 8BIM segment into existing Photoshop segment
                    Dim BIM8s As IList(Of Photoshop8BIMReader) = Me.Get8BIMSegments(Me.IPTC8BIMSegmentIndex)
                    'Position where to write new 8BIM segment
                    If BIM8s.Count > 0 Then
                        PreDataPos = DirectCast(Me.GetPhotoShopStream, ConstrainedReadOnlyStream).TranslatePosition(0) + 14
                    Else
                        PreDataPos = DirectCast(BIM8s(BIM8s.Count - 1).Data, ConstrainedReadOnlyStream)(BIM8s(BIM8s.Count - 1).Data.Length - 1)
                        If BIM8s(BIM8s.Count - 1).DataPadNeeded Then PreDataPos += 1
                    End If
                    PreData = BIM8Header(IPTCData.Length)
                    Overwrite.Add(APP14SizePos, Me.Markers(Me.PhotoshopMarkerIndex).Length + IPTCData.Length + PreData.Length) 'New length of APP14
                    LenghtToReplace = 0 'Insert
                End If
                If IPTCData.Length Mod 2 <> 0 Then Overwrite(APP14SizePos) += 1
            Else
                'Embed new APP13 with IPTC data
                Dim InsertAfter As JPEGMarkerReader = Nothing
                For Each Marker As JPEGMarkerReader In Me.Markers
                    If Marker.Code = JPEGMarkerReader.Markers.SOS Then Exit For
                    InsertAfter = Marker
                    If Marker.Code > JPEGMarkerReader.Markers.APP13 OrElse (Marker.Code < JPEGMarkerReader.Markers.APP0 AndAlso Marker.Code <> JPEGMarkerReader.Markers.EOI) Then Exit For
                Next Marker
                If InsertAfter Is Nothing Then Throw New InvalidOperationException(ResourcesT.Exceptions.NoJPEGMarkerFound)
                If InsertAfter.MarkerCode = JPEGMarkerReader.Markers.SOI Then
                    PreDataPos = 2
                Else
                    If InsertAfter.MarkerCode = JPEGMarkerReader.Markers.SOI Then
                        PreDataPos = InsertAfter.Offset + 2
                    End If
                    If InsertAfter.Data.Length = 0 Then
                        PreDataPos = InsertAfter.Offset + 4
                    Else
                        PreDataPos = DirectCast(InsertAfter.Data, ConstrainedReadOnlyStream).TranslatePosition(0) + InsertAfter.Data.Length
                    End If
                End If
                Dim s As New MemoryStream(34)
                Dim w As New IO.BinaryWriter(s)
                w.Write(CByte(&HFF))
                w.Write(JPEGMarkerReader.Markers.APP13)
                w.Write(New Byte() {0, 0})
                w.Write(New Byte() {&H50, &H68, &H6F, &H74, &H6F, &H73, &H68, &H6F, &H70, &H20, &H33, &H2E, &H30, &H0}) 'Photoshop 3.0\00
                w.Write(BIM8Header(IPTCData.Length))
                Dim Bytes As UShort = s.Position + IPTCData.Length
                s.Seek(2, SeekOrigin.Begin)
                If IPTCData.Length Mod 2 <> 0 Then
                    w.Write(MathT.LEBE(Bytes - 2US + 1US))
                Else
                    w.Write(MathT.LEBE(Bytes - 2US))
                End If
                ReDim PreData(Bytes - IPTCData.Length - 1)
                Array.ConstrainedCopy(s.GetBuffer, 0, PreData, 0, Bytes - IPTCData.Length)
                ' If IPTCData.Length Mod 2 <> 0 Then Bytes += 1
                LenghtToReplace = 0
            End If
            If IPTCData.Length Mod 2 <> 0 Then
                PostData = New Byte() {0}
            Else
                PostData = New Byte() {}
            End If
            Dim EmbedW As New IO.BinaryWriter(Me.Stream)
            For Each item As KeyValuePair(Of Integer, UShort) In Overwrite
                Me.Stream.Seek(item.Key, SeekOrigin.Begin)
                EmbedW.Write(MathT.LEBE(item.Value))
            Next item
            Dim AllData(PreData.Length + IPTCData.Length + PostData.Length - 1) As Byte
            For i As Integer = 0 To AllData.Length - 1
                If i < PreData.Length Then
                    AllData(i) = PreData(i)
                ElseIf i < PreData.Length + IPTCData.Length Then
                    AllData(i) = IPTCData(i - PreData.Length)
                Else
                    AllData(i) = PostData(i - PreData.Length - IPTCData.Length)
                End If
            Next i
            StreamTools.InsertInto(Me.Stream, PreDataPos, LenghtToReplace, AllData)
            Me.Stream.Flush()
        End Sub
        ''' <summary>Gets bytes of header of 8BIM 1C02 segment</summary>
        ''' <param name="IPTCDataLength">Size of segment data part to be reported</param>
        Private Function BIM8Header(ByVal IPTCDataLength As UInteger) As Byte()
            Dim s As New MemoryStream(16)
            Dim w As New IO.BinaryWriter(s)
            w.Write(New Byte() {&H38, &H42, &H49, &H4D}) '"8BIM"
            w.Write(New Byte() {&H4, &H4}) '&h1C02 - IPTC type
            w.Write(CByte(5)) 'Lenght of following string including this byte
            w.Write(System.Text.Encoding.ASCII.GetBytes("IPTC")) 'Just small text - this is not complusory
            w.Write(CByte(0)) 'Pad string + specifier to even lenght (6)
            w.Write(MathT.LEBE(IPTCDataLength)) 'Lenght of IPTC data
            Dim Bytes(s.Position - 1) As Byte
            Array.ConstrainedCopy(s.GetBuffer, 0, Bytes, 0, Bytes.Length) '8BIM Header 
            Return Bytes
        End Function

#Region " IDisposable Support "

        ''' <summary>To detect redundant calls</summary>
        Private disposedValue As Boolean = False
        ''' <summary>stream will be closed when <see cref="Dispose"/> is invoked</summary>
        Private CloseStreamOnDispose As Boolean = False
        ''' <summary><see cref="IDisposable"/></summary>
        ''' <param name="disposing">Free shared unmanaged resources</param>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                End If
                If CloseStreamOnDispose AndAlso Stream IsNot Nothing Then Stream.Close()
            End If
            Me.disposedValue = True
        End Sub

        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        ''' <remarks>This code added by Visual Basic to correctly implement the disposable pattern.</remarks>
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

    ''' <summary>Represents Photoshop 8BIM segment</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Alpha"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Class Photoshop8BIMReader
        ''' <summary>CTor</summary>
        ''' <param name="Stream">Steam which contains segment data</param>
        ''' <param name="Offset">Offest of start of segment within <paramref name="Stream"/></param>
        ''' <exception cref="IOException">IO error while reding <paramref name="Stream"/></exception>
        ''' <exception cref="EndOfStreamException">End of stream <paramref name="Stream"/> reached unexpectedly</exception>
        ''' <exception cref="InvalidDataException">
        ''' Segment doesn't start with string '8BIM'
        ''' Sum of reported size and start of segment data exceeds length of <paramref name="Stream"/>
        ''' </exception>
        Public Sub New(ByVal Stream As System.IO.Stream, ByVal Offset As Long)
            Const Header8BIM$ = "8BIM"
            Stream.Position = Offset
            Dim r As New Tools.IOt.BinaryReader(Stream, System.Text.Encoding.ASCII, Tools.IOt.BinaryReader.ByteAlign.BigEndian)
            Dim Bytes8BIM(3) As Byte
            If Stream.Read(Bytes8BIM, 0, 4) = 4 Then
                Dim Str8BIM As String = System.Text.Encoding.ASCII.GetString(Bytes8BIM, 0, 4)
                If Str8BIM <> Header8BIM Then _
                    Throw New InvalidDataException(ResourcesT.Exceptions.BIMSegmentDoesnTStartWithSting8BIM)
            Else
                Throw New InvalidDataException(ResourcesT.Exceptions.BIMSegmentDoesnTStartWithSting8BIM)
            End If
            _Type = r.ReadUInt16
            Dim StrLen As Byte = r.ReadByte
            If StrLen > 1 Then
                Dim Bytes As Byte() = r.ReadBytes(StrLen - 1)
                _Name = System.Text.Encoding.Default.GetString(Bytes)
            ElseIf StrLen = 0 Then '0 is incorrect value, but is used - treated as 2 bytes (size+pad)
                Stream.ReadByte()
                _Name = ""
            Else
                _Name = ""
            End If
            _NamePaddNeeded = StrLen Mod 2 <> 0
            If NamePaddNeeded Then Stream.ReadByte()
            _DataSize = r.ReadUInt32
            If Stream.Position + DataSize > Stream.Length Then _
                Throw New InvalidDataException(ResourcesT.Exceptions.ReportedLengthOf8BIMSegmentDoesnFFitIntoBaseStream)
            _Data = New ConstrainedReadOnlyStream(Stream, Stream.Position, DataSize)
        End Sub
        ''' <summary>True when name is padded to odd lenght (event with size specification) by one null byte</summary>
        Private _NamePaddNeeded As Boolean = False
        ''' <summary>Contains value of the <see cref="Type"/> property</summary>
        Private _Type As UShort
        ''' <summary>Contains value of the <see cref="Name"/> property</summary>
        Private _Name As String
        ''' <summary>Contains value of the <see cref="DataSize"/> property</summary>
        Private _DataSize As UInteger
        ''' <summary>Contains value of the <see cref="Data"/> property</summary>
        Private _Data As System.IO.Stream
        ''' <summary>Size of whole 8BIM segment including all header information</summary>
        ''' <remarks>See <see cref="DataSize"/> for size of data part of segment</remarks>
        Public ReadOnly Property WholeSize() As Long
            Get
                '#If Framework >= 3.5 Then
                Return DataSize + 2 + 1 + If(Name.Length = 0, 2, Name.Length) + 4 + 4 + _
                    If(NamePaddNeeded, 1, 0) + If(DataPadNeeded, 1, 0)
                '#Else
                '                Return DataSize + 2 + 1 + VisualBasicT.iif(Name.Length = 0, 2, Name.Length) + 4 + 4 + _
                '                    Tools.VisualBasicT.iif(NamePaddNeeded, 1, 0) + Tools.VisualBasicT.iif(DataPadNeeded, 1, 0)
                '#End If
                '2 - Type
                '1 - Length of Pascal string
                '4 - Size
                '4 - '8BIM'
            End Get
        End Property
        ''' <summary>True when data must be padded with one null byte to even lenght</summary>
        Public ReadOnly Property DataPadNeeded() As Boolean
            Get
                Return DataSize Mod 2 <> 0
            End Get
        End Property
        ''' <summary>True when name is padded to odd lenght (event with size specification) by one null byte</summary>
        Public ReadOnly Property NamePaddNeeded() As Boolean
            Get
                Return _NamePaddNeeded
            End Get
        End Property
        ''' <summary>Type of segment</summary>
        <CLSCompliant(False)> _
        Public ReadOnly Property Type() As UShort
            Get
                Return _Type
            End Get
        End Property
        ''' <summary>Name of segment</summary>
        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property
        ''' <summary>Size of data part of segment</summary>
        ''' <remarks>See <see cref="WholeSize"/> for size of whole segment including all header information</remarks>
        <CLSCompliant(False)> _
        Public ReadOnly Property DataSize() As UInteger
            Get
                Return _DataSize
            End Get
        End Property
        ''' <summary>Stream of data part of segment</summary>
        Public ReadOnly Property Data() As System.IO.Stream
            Get
                Return _Data
            End Get
        End Property
    End Class
#End If
End Namespace