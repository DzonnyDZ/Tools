Imports System.IO, Tools.IO
Namespace Drawing.IO.JPEG 'TODO:Wiki
#If Config <= Nightly Then 'Stage:Nightly
    ''' <summary>Provides tools realted to reading from JPEG graphic file format on low level</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(JPEGReader), LastChange:="4/23/2007")> _
    Public Class JPEGReader
        ''' <summary>Stream of opened file</summary>
        Protected ReadOnly Stream As System.IO.Stream
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
        ''' JPEG stream doesn't end with corect EOI marker
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
            _ImageStream = New ConstrainedReadonlyStream(Stream, Pos, Stream.Length - 2)
            Dim EOI As New JPEGMarkerReader(Stream, Stream.Length - 2)
            If EOI.MarkerCode <> JPEGMarkerReader.Markers.EOI OrElse EOI.Length <> 0 Then _
                Throw New InvalidDataException("JPEG file doesn't end with correct EOI marker")
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
    End Class
    ''' <summary>Represernts marker (block of JPEG file)</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(JPEGMarkerReader), LastChange:="4/23/2007")> _
    Public Class JPEGMarkerReader
        ''' <summary>Known types of JPEG markers</summary>
        Public Enum Markers As Byte
            ''' <summary>Start Of Image marker</summary>
            SOI = &HD8
            ''' <summary>End Of Image marker</summary>
            EOI = &HD9
            ''' <summary>Start Of Stream marker</summary>
            SOS = &HDA
            ''' <summary>APP0 (JFIF) marker</summary>
            APP0 = &HE0
            ''' <summary>APP1 marker (used for Exif and XMP)</summary>
            APP1 = &HE1
            ''' <summary>APP2 marker (used for Exif extension)</summary>
            APP2 = &HE2
            ''' <summary>APP3 marker</summary>
            APP3 = &HE3
            ''' <summary>APP4 marker</summary>
            APP4 = &HE4
            ''' <summary>APP5 marker</summary>
            APP5 = &HE5
            ''' <summary>APP6 marker</summary>
            APP6 = &HE6
            ''' <summary>APP7 marker</summary>
            APP7 = &HE7
            ''' <summary>APP8 marker</summary>
            APP8 = &HE8
            ''' <summary>APP9 marker</summary>
            APP9 = &HE9
            ''' <summary>APP10 marker</summary>
            APP10 = &HEA
            ''' <summary>APP11 marker</summary>
            APP11 = &HEB
            ''' <summary>APP12 marker</summary>
            APP12 = &HEC
            ''' <summary>APP13 marker (used for PhoroShop 8BIM which contains EXIF)</summary>
            APP13 = &HED
            ''' <summary>APP14 marker</summary>
            APP14 = &HEE
            ''' <summary>APP15 marker</summary>
            APP15 = &HEF
            ''' <summary>Define Quantization Table marker</summary>
            DQT = &HDB
            ''' <summary>Define Huffman Table marker</summary>
            DHT = &HC4
            ''' <summary>JPEG comment marker</summary>
            Comment = &HFE
            ''' <summary>Start Of Frame marker</summary>
            SOF = &HC0
            ''' <summary>Unknown marker (not recognized by this application)</summary>
            Unknown = &H0
        End Enum
        ''' <summary>Contains value of the <see cref="Code"/>property</summary>
        Private _Code As UInt16
        ''' <summary>Contains value of the <see cref="Length"/> property</summary>
        Private _Length As UInt16
        ''' <summary>Contains value of the <see cref="Data"/> property</summary>
        Private _Data As Stream
        ''' <summary>CTor - constructs JPEG marker from data present at given offset of given <see cref="System.IO.Stream"/></summary>
        ''' <param name="Stream"><see cref="System.IO.Stream"/> to read data from, should be stream of whole JPEG file</param>
        ''' <param name="Offset">Offset to start reading data at</param>
        ''' <exception cref="InvalidDataException">
        ''' Marker code doesn's start with FFh -or-
        ''' Marker which's Lenght is set to 0 or 1 found (doesn't applly to SOI and EOI)
        ''' </exception>
        Public Sub New(ByVal Stream As Stream, ByVal Offset As Long)
            Stream.Position = Offset
            Dim r As New Tools.IO.BinaryReader(Stream, Tools.IO.BinaryReader.ByteAling.BigEndian)
            _Code = r.ReadUInt16()
            If (Code And &HFF00) >> 8 <> &HFF Then Throw New InvalidDataException("Given marker's code doesn't start with FFh")
            If MarkerCode <> Markers.SOI AndAlso MarkerCode <> Markers.EOI Then
                _Length = r.ReadUInt16
                If Length < 2 Then Throw New InvalidDataException("Only SOI and EOI markers can have lenght set to zero, length 1 is not allowed")
                _Data = New ConstrainedReadonlyStream(Stream, Stream.Position, Length - 2)
            Else
                _Length = 0
                _Data = Net.Sockets.NetworkStream.Null
            End If
        End Sub
        ''' <summary>2 Bytes code of marker</summary>
        <CLSCompliant(False)> _
        Public ReadOnly Property Code() As UInt16
            <DebuggerStepThrough()> Get
                Return _Code
            End Get
        End Property
        ''' <summary>Lenght of marker exluding marker code, including size specification</summary>
        <CLSCompliant(False)> _
        Public ReadOnly Property Length() As UInt16
            <DebuggerStepThrough()> Get
                Return _Length
            End Get
        End Property
        ''' <summary>Stream of marker's data</summary>
        Public ReadOnly Property Data() As Stream
            <DebuggerStepThrough()> Get
                Return _Data
            End Get
        End Property
        ''' <summary>Code of marker</summary>
        ''' <returns>Code of marker if marker was recognized a known marker, <see cref="Markers.Unknown"/> otherwise</returns>
        ''' <remarks>See <see cref="Code"/> property for full code</remarks>
        Public ReadOnly Property MarkerCode() As Markers
            Get
                Dim Marker As Byte = Code And &HFF
                If Array.IndexOf(Of Markers)([Enum].GetValues(GetType(Markers)), Marker) >= 0 Then
                    Return Marker
                Else
                    Return Markers.Unknown
                End If
            End Get
        End Property
    End Class
#End If
End Namespace