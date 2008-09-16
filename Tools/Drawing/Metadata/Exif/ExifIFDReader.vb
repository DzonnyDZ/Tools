Imports System.IO
Namespace DrawingT.MetadataT.ExifT
#If Config <= Nightly Then 'Stage: Nightly
    ''' <summary>Provides low level access to stream containing exif IFD (Image File Directory) or SubIFD</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 1, GetType(ExifIFDReader), LastChange:="07/22/2008")> _
    <FirstVersion("04/24/2007")> _
    Public Class ExifIFDReader
        ''' <summary>CTor</summary>
        ''' <param name="Exif"><see cref="ExifReader"/> that contains this IFD</param>
        ''' <param name="Offset">Offset of start of this IFD in <paramref name="Stream"/></param>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the Exif stream is reached unexpectedly.</exception>
        ''' <exception cref="InvalidEnumArgumentException">Directory entry of unknown data type found</exception>
        ''' <exception cref="InvalidDataException">Tag data of some are placed otside the tag and cannot be read</exception>
        <CLSCompliant(False)> _
        Public Sub New(ByVal Exif As ExifReader, ByVal Offset As UInt32)
            _ExifReader = Exif
            _Offset = Offset
            Dim r As New Tools.IOt.BinaryReader(Exif.Stream, Exif.ByteOrder)
            Exif.Stream.Position = Offset
            Dim Entries As UShort = r.ReadUInt16

            Dim Pos As Long = Exif.Stream.Position
            For i As Integer = 1 To Entries
                Exif.Stream.Position = Pos
                Dim Tag As UShort = r.ReadUInt16
                Dim Kind As UShort = r.ReadUInt16
                Dim Components As UInt32 = r.ReadUInt32
                Dim Data As Byte() = r.ReadBytes(4)
                Pos = Exif.Stream.Position
                Me._Entries.Add(New DirectoryEntry(Tag, Kind, Components, Data, Exif))
            Next i
            Exif.Stream.Position = Pos
            _NextIFD = r.ReadUInt32
        End Sub
        ''' <summary>Contains value of the <see cref="ExifReader"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _ExifReader As ExifReader
        ''' <summary>Gets <see cref="ExifReader"/> this <see cref="ExifIFDReader"/> have read data from.</summary>
        ''' <returns>Instance of <see cref="ExifReader"/> that was passed to CTor of this instance.</returns>
        Public ReadOnly Property ExifReader() As ExifReader
            <DebuggerStepThrough()> Get
                Return _ExifReader
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="Entries"/> property</summary>
        Private _Entries As New List(Of DirectoryEntry)
        ''' <summary>Contains value of the <see cref="NextIFD"/> property</summary>
        Private _NextIFD As UInt32
        ''' <summary>Offset of following IFD (or 0 if this is last IFD)</summary>
        <CLSCompliant(False)> _
        Public ReadOnly Property NextIFD() As UInt32
            Get
                Return _NextIFD
            End Get
        End Property
        ''' <summary>Entries in this IFD</summary>
        Public ReadOnly Property Entries() As CollectionsT.GenericT.IReadOnlyList(Of DirectoryEntry)
            Get
                Return New CollectionsT.GenericT.ReadOnlyListAdapter(Of DirectoryEntry)(_Entries)
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="Offest"/> property</summary>
        Private _Offset As UInt32
        ''' <summary>Offset of this IFD in Exif data block</summary>
        <CLSCompliant(False)> _
        Public ReadOnly Property Offest() As UInt32
            Get
                Return _Offset
            End Get
        End Property
    End Class

    ''' <summary>Represents read-only directory entry of Exif data</summary>
    Public Class DirectoryEntry
        ''' <summary>Contains value of the <see cref="Tag"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Tag As UShort
        Private _DataType As ExifDataTypes
        ''' <summary>Contains value of the <see cref="Components"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Componets As UInt32
        ''' <summary>Contains value of the <see cref="DataIsStoredOutside"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _DataIsStoredOutside As Boolean
        ''' <summary>Contains value of the <see cref="Data"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Data As Object
        ''' <summary>CTor</summary>
        ''' <param name="Tag">Tag identifier</param>
        ''' <param name="Kind">Data type</param>
        ''' <param name="Components">Number of components</param>
        ''' <param name="Data">Data or offset to data</param>
        ''' <param name="Exif"><see cref="ExifReader"/> to obtain data from when <paramref name="Data"/> doesn't contain data but offset to data</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Kind"/> is not member of <see cref="ExifDataTypes"/></exception>
        ''' <exception cref="InvalidDataException">Tag data are placed otside the tag and cannot be read</exception>
        <CLSCompliant(False)> _
         Public Sub New(ByVal Tag As UShort, ByVal Kind As ExifDataTypes, ByVal Components As UInt32, ByVal Data As Byte(), ByVal Exif As ExifReader)
            _Tag = Tag
            _DataType = Kind
            _Componets = Components
            Dim SizeTotal As Long = BytesPerComponent(Kind) * Components
            _DataIsStoredOutside = SizeTotal > 4
            Dim TagData As Byte()
            If _DataIsStoredOutside Then
                Dim Str As New MemoryStream(Data, False)
                Dim r As New Tools.IOt.BinaryReader(Str, Exif.ByteOrder)
                Str.Position = 0
                Dim Offset As UInt32 = r.ReadUInt32
                Exif.Stream.Position = Offset
                ReDim TagData(SizeTotal - 1)
                If Exif.Stream.Read(TagData, 0, SizeTotal) <> SizeTotal Then
                    Throw New InvalidDataException(ResourcesT.Exceptions.CannotReadTagDataFromStream)
                End If
            Else
                TagData = Data
            End If
            _Data = ReadData(Kind, TagData, Components, Exif.ByteOrder)
        End Sub
        ''' <summary>Reads data of specified type freom given <see cref="Array"/> of <see cref="Byte"/>s</summary>
        ''' <param name="Type">Type of data to read</param>
        ''' <param name="Align">Format in whicvh data are stored</param>
        ''' <param name="Buffer">Buffer to read data from</param>
        ''' <param name="Components">Number of components to be read</param>
        ''' <returns>Data read from buffer. If <paramref name="Components"/> is 1 scalar of specified type is returned, <see cref="Array"/> otherwise with exceptions: 1 component of type <see cref="ExifDataTypes.ASCII"/> resuts to <see cref="Char"/>, more components results to <see cref="String"/>; <see cref="ExifDataTypes.NA"/> always results to <see cref="Array"/> of <see cref="Byte"/>s</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Type"/> is not member of <see cref="ExifDataTypes"/></exception>
        Private Shared Function ReadData(ByVal Type As ExifDataTypes, ByVal Buffer As Byte(), ByVal Components As Integer, ByVal Align As Tools.IOt.BinaryReader.ByteAlign) As Object
            Dim Str As New MemoryStream(Buffer, False)
            Str.Position = 0
            Dim r As New Tools.IOt.BinaryReader(Str, System.Text.Encoding.Default, Align)
            Select Case Type
                Case ExifDataTypes.Byte
                    If Components = 1 Then Return CByte(Buffer(0)) Else Return Buffer.Clone
                Case ExifDataTypes.ASCII
                    If Components = 1 Then
                        Return r.ReadChar()
                    Else
                        Return New String(r.ReadChars(Buffer.Length))
                    End If
                Case ExifDataTypes.UInt16
                    If Components = 1 Then
                        Return r.ReadUInt16
                    Else
                        Dim ret(Components - 1) As UInt16
                        For i As Integer = 1 To Components
                            ret(i - 1) = r.ReadUInt16
                        Next i
                        Return ret
                    End If
                Case ExifDataTypes.UInt32
                    If Components = 1 Then
                        Return r.ReadUInt32
                    Else
                        Dim ret(Components - 1) As UInt32
                        For i As Integer = 1 To Components
                            ret(i - 1) = r.ReadUInt32
                        Next i
                        Return ret
                    End If
                Case ExifDataTypes.URational
                    If Components = 1 Then
                        Return New URational(r.ReadUInt16, r.ReadUInt16)
                    Else
                        Dim ret(Components - 1) As URational
                        For i As Integer = 1 To Components
                            ret(i - 1) = New URational(r.ReadUInt16, r.ReadUInt16)
                        Next i
                        Return ret
                    End If
                Case ExifDataTypes.SByte
                    If Components = 1 Then
                        Return r.ReadSByte
                    Else
                        Dim ret(Components - 1) As SByte
                        For i As Integer = 1 To Components
                            ret(i - 1) = r.ReadSByte
                        Next i
                        Return ret
                    End If
                Case ExifDataTypes.NA
                    Return Buffer.Clone
                Case ExifDataTypes.Int16
                    If Components = 1 Then
                        Return r.ReadInt16
                    Else
                        Dim ret(Components - 1) As Int16
                        For i As Integer = 1 To Components
                            ret(i - 1) = r.ReadInt16
                        Next i
                        Return ret
                    End If
                Case ExifDataTypes.Int32
                    If Components = 1 Then
                        Return r.ReadInt32
                    Else
                        Dim ret(Components - 1) As Int32
                        For i As Integer = 1 To Components
                            ret(i - 1) = r.ReadInt32
                        Next i
                        Return ret
                    End If
                Case ExifDataTypes.SRational
                    If Components = 1 Then
                        Return New SRational(r.ReadInt16, r.ReadInt16)
                    Else
                        Dim ret(Components - 1) As SRational
                        For i As Integer = 1 To Components
                            ret(i - 1) = New SRational(r.ReadInt16, r.ReadInt16)
                        Next i
                        Return ret
                    End If
                Case ExifDataTypes.Single
                    If Components = 1 Then
                        Return r.ReadSingle
                    Else
                        Dim ret(Components - 1) As Single
                        For i As Integer = 1 To Components
                            ret(i - 1) = r.ReadSingle
                        Next i
                        Return ret
                    End If
                Case ExifDataTypes.Double
                    If Components = 1 Then
                        Return r.ReadDouble
                    Else
                        Dim ret(Components - 1) As Double
                        For i As Integer = 1 To Components
                            ret(i - 1) = r.ReadDouble
                        Next i
                        Return ret
                    End If
                Case Else : Throw New InvalidEnumArgumentException("Type", Type, GetType(ExifDataTypes))
            End Select
        End Function
        ''' <summary>Gets number of bytes per component of specified Exif data type</summary>
        ''' <param name="DataType">Data type to get number of bytes per component of</param>
        ''' <returns>Number of bytes per one component of specified Exif data type</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="DataType"/> is not member of <see cref="ExifDataTypes"/></exception>
        <CLSCompliant(False)> _
        Public Shared Function BytesPerComponent(ByVal DataType As ExifDataTypes) As Byte
            Select Case DataType
                Case ExifDataTypes.Byte : Return 1
                Case ExifDataTypes.ASCII : Return 1
                Case ExifDataTypes.UInt16 : Return 2
                Case ExifDataTypes.UInt32 : Return 4
                Case ExifDataTypes.URational : Return 8
                Case ExifDataTypes.SByte : Return 1
                Case ExifDataTypes.NA : Return 1
                Case ExifDataTypes.Int16 : Return 2
                Case ExifDataTypes.Int32 : Return 4
                Case ExifDataTypes.SRational : Return 8
                Case ExifDataTypes.Single : Return 4
                Case ExifDataTypes.Double : Return 8
                Case Else : Throw New InvalidEnumArgumentException("DataType", DataType, GetType(ExifDataTypes))
            End Select
        End Function
        ''' <summary>Identifier of this directory entry (tag)</summary>
        <CLSCompliant(False)> _
        Public ReadOnly Property Tag() As UShort
            Get
                Return _Tag
            End Get
        End Property
        ''' <summary>Kind of data stored in this directory entry</summary>
        <CLSCompliant(False)> _
        Public ReadOnly Property DataType() As ExifDataTypes
            Get
                Return _DataType
            End Get
        End Property
        ''' <summary>Number of components of data stored in this directory entry</summary>
        <CLSCompliant(False)> _
        Public ReadOnly Property Components() As UInt32
            Get
                Return _Componets
            End Get
        End Property
        ''' <summary>Gets wheather data is store inside directory entry (true) or outside (false)</summary>
        Public ReadOnly Property DataIsStoredOutside() As Boolean
            Get
                Return _DataIsStoredOutside
            End Get
        End Property
        ''' <summary>Data of this directory entry</summary>
        ''' <remarks>Actual data type of this property depends on <see cref="DataType"/></remarks>
        Public ReadOnly Property Data() As Object
            Get
                Return _Data
            End Get
        End Property
    End Class
    ''' <summary>Possible data types for Exif values</summary>
    <CLSCompliant(False)> _
    Public Enum ExifDataTypes As UShort
        ''' <summary>Unsigned 1-byte integer (<see cref="Byte"/>)</summary>
        [Byte] = 1US
        ''' <summary>ASCII string</summary>
        ASCII = 2US
        ''' <summary>Unsigned 2-bytes integer (<see cref="UInt16"/>)</summary>
        UInt16 = 3US
        ''' <summary>Unsigned 4-bytes integer (<see cref="UInt32"/>)</summary>
        UInt32 = 4US
        ''' <summary>Unsigned rational (2 4-bytes unsigned integers), first is numerator and second is deniminator</summary>
        URational = 5US
        ''' <summary>Singned 1-byte integer (<see cref="SByte"/>)</summary>
        [SByte] = 6US
        ''' <summary>Unknown data type</summary>
        NA = 7US
        ''' <summary>Signed 2-bytes integer (<see cref="Int16"/>)</summary>
        Int16 = 8US
        ''' <summary>Signed 4-bytes integer (<see cref="Int32"/>)</summary>
        Int32 = 9US
        ''' <summary>Signed rational (2 4-bytes signed integers), first is numerator and second is deniminator</summary>
        SRational = 10US
        ''' <summary>Single floating point number (<see cref="Single"/>)</summary>
        [Single] = 11US
        ''' <summary>Double floating point number (<see cref="Double"/>)</summary>
        [Double] = 12US
    End Enum
#End If
End Namespace
