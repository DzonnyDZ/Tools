Imports System.IO, Tools.IOt
Namespace DrawingT.IO.JPEG
#If Config <= Alpha Then 'Stage: Alpha
    ''' <summary>Represernts marker (block of JPEG file)</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(JPEGMarkerReader), LastChange:="04/24/2007")> _
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    <FirstVersion("04/24/2007")> _
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
            ''' <summary>Define Restart Interoperability marker</summary>
            DRI = &HDD
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
        ''' <exception cref="IOException">IO error while reding <paramref name="Stream"/></exception>
        ''' <exception cref="EndOfStreamException">End of stream <paramref name="Stream"/> reached unexpectedly</exception>
        Public Sub New(ByVal Stream As Stream, ByVal Offset As Long)
            _Offset = Offset
            Stream.Position = Offset
            Dim r As New Tools.IOt.BinaryReader(Stream, Tools.IOt.BinaryReader.ByteAling.BigEndian)
            _Code = r.ReadUInt16()
            If (Code And &HFF00) >> 8 <> &HFF Then Throw New InvalidDataException("Given marker's code doesn't start with FFh")
            If MarkerCode <> Markers.SOI AndAlso MarkerCode <> Markers.EOI Then
                _Length = r.ReadUInt16
                If Length < 2 Then Throw New InvalidDataException("Only SOI and EOI markers can have lenght set to zero, length 1 is not allowed")
                _Data = New ConstrainedReadOnlyStream(Stream, Offset + 4, Length - 2)
            Else
                _Length = 0
                _Data = Net.Sockets.NetworkStream.Null
            End If
        End Sub
        ''' <summary>Contains value of the <see cref="Offset"/> property</summary>
        Private _Offset As Long
        ''' <summary>Offset of marker in stream it was constructed of</summary>
        Public ReadOnly Property Offset() As Long
            Get
                Return _Offset
            End Get
        End Property
        ''' <summary>2 Bytes code of marker</summary>
        <CLSCompliant(False)> _
        Public ReadOnly Property Code() As UInt16
            <DebuggerStepThrough()> Get
                Return _Code
            End Get
        End Property
        ''' <summary>Lenght of marker exluding marker code, including size specification</summary>
        ''' <remarks>Actual length of whole segment including marker code is always 2 bytes more</remarks>
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