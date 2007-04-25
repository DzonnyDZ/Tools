Imports System.IO, Tools.IO
Namespace Drawing.IO.JPEG
#If Config <= Alpha Then 'Stage: Alpha
    ''' <summary>Provides tools realted to reading from JPEG graphic file format on low level</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(JPEGReader), LastChange:="4/23/2007")> _
    Public Class JPEGReader
        Implements Metadata.IExifGetter, Metadata.IIPTCGetter
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
        Public Sub New(ByVal Path As String)
            Stream = New System.IO.FileStream(Path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read)
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
        Public Sub New(ByVal Stream As System.IO.Stream)
            If Stream.CanRead AndAlso Stream.CanSeek Then
                Me.Stream = Stream
            Else
                Throw New NotSupportedException("Stream to read JPEG from must be able to seek and read")
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
                Throw New InvalidDataException("JPEG file doesn't start with correct SOI marker")
            _Markers.Add(SOI)
            Dim Pos As Long = 2
            Dim Marker As JPEGMarkerReader
            Do
                Marker = New JPEGMarkerReader(Stream, Pos)
                Pos += 2 + Marker.Length
                _Markers.Add(Marker)
            Loop Until Marker.MarkerCode = JPEGMarkerReader.Markers.SOS
            Dim CS As New Tools.IO.ConstrainedReadOnlyStream(Stream, Pos, Stream.Length - Pos)
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
            If EOI Is Nothing Then Throw New InvalidDataException("EOI not found")
            If EOI.MarkerCode <> JPEGMarkerReader.Markers.EOI OrElse EOI.Length <> 0 Then _
                Throw New InvalidDataException("JPEG file doesn't end with correct EOI marker")
            _Markers.Add(EOI)
        End Sub
        ''' <summary>List of markers this JPEG stream</summary>
        Public ReadOnly Property Markers() As Collections.Generic.IReadOnlyList(Of JPEGMarkerReader)
            Get
                Return New Collections.Generic.ReadOnlyListAdapter(Of JPEGMarkerReader)(_Markers)
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
        Public Function GetExifStream() As System.IO.Stream Implements Metadata.IExifGetter.GetExifStream
            Const Exif$ = "Exif"
            If _ExifMarkerIndex = -1 Then
                Return Nothing
            ElseIf _ExifMarkerIndex >= 0 Then
                Return New ConstrainedReadOnlyStream(Me.Markers(ExifMarkerIndex).Data, 6, Me.Markers(ExifMarkerIndex).Data.Length - 6)
            End If
            Dim i As Integer = 0
            For Each m As JPEGMarkerReader In Me.Markers
                If m.MarkerCode = JPEGMarkerReader.Markers.APP1 Then
                    m.Data.Position = 0
                    Dim Bytes(5) As Byte
                    If m.Data.Read(Bytes, 0, 6) = 6 Then
                        Dim ExifH As String = System.Text.Encoding.ASCII.GetString(Bytes, 0, 4)
                        If ExifH = Exif AndAlso Bytes(4) = 0 AndAlso Bytes(5) = 0 Then
                            _ExifMarkerIndex = i
                            Return New ConstrainedReadOnlyStream(m.Data, 6, m.Data.Length - 6)
                        End If
                    End If
                End If
                i += 1
            Next m
            _ExifMarkerIndex = -1
            Return Nothing
        End Function
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
        Public Function GetIPTCStream() As System.IO.Stream Implements Metadata.IIPTCGetter.GetIPTCStream
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
        Public Function Get8BIMSegments() As Collections.Generic.IReadOnlyList(Of Photoshop8BIMReader)
            Dim Segments As New List(Of Photoshop8BIMReader)
            Dim Pos As Long = 0
            Dim Photoshop As System.IO.Stream = GetPhotoShopStream()
            If Photoshop Is Nothing OrElse Photoshop.Length = 0 Then _
                Return New Collections.Generic.ReadOnlyListAdapter(Of Photoshop8BIMReader)(Segments)
            Do
                Dim Sgm As New Photoshop8BIMReader(Photoshop, Pos)
                Pos += Sgm.WholeSize
                Segments.Add(Sgm)
            Loop While Pos < Photoshop.Length
            Return New Collections.Generic.ReadOnlyListAdapter(Of Photoshop8BIMReader)(Segments)
        End Function
        ''' <summary>Gets stream of PhotoShop data</summary>
        ''' <remarks>
        ''' <para>Stream content starts with marker of first 8BIM segment</para>
        ''' <para>If there is no Photoshop data in file stream can be null or have zero length</para>
        ''' <para>Stream supports reading and seeking</para>
        ''' </remarks>
        Public Function GetPhotoShopStream() As System.IO.Stream
            Const PhotoShop$ = "Photoshop 3.0"
            If PhotoshopMarkerIndex = -1 Then
                Return Nothing
            ElseIf PhotoshopMarkerIndex >= 0 Then
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
                If _PhotoshopMarkerIndex < -1 Then GetExifStream()
                Return _PhotoshopMarkerIndex
            End Get
        End Property
    End Class

    ''' <summary>Represents Photoshop 8BIM segment</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(Photoshop8BIMReader), LastChange:="4/24/2007")> _
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
            Dim r As New Tools.IO.BinaryReader(Stream, System.Text.Encoding.ASCII, Tools.IO.BinaryReader.ByteAling.BigEndian)
            Dim Bytes8BIM(3) As Byte
            If Stream.Read(Bytes8BIM, 0, 4) = 4 Then
                Dim Str8BIM As String = System.Text.Encoding.ASCII.GetString(Bytes8BIM, 0, 4)
                If Str8BIM <> Header8BIM Then _
                    Throw New InvalidDataException("8BIM segment doesn't start with sting '8BIM'")
            Else
                Throw New InvalidDataException("8BIM segment doesn't start with sting '8BIM'")
            End If
            _Type = r.ReadUInt16
            _Name = r.ReadString
            _NamePaddNeeded = Name.Length Mod 2 = 0
            If NamePaddNeeded Then Stream.ReadByte()
            _DataSize = r.ReadUInt32
            If Stream.Position + DataSize > Stream.Length Then _
                Throw New InvalidDataException("Reported length of 8BIM segment doesn'f fit into base stream")
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
                Return DataSize + 2 + 1 + Name.Length + 4 + 4 + _
                    Tools.VisualBasic.iif(NamePaddNeeded, 1, 0) + Tools.VisualBasic.iif(DataPadNeeded, 1, 0)
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