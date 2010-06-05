Imports System.IO
Imports Tools.NumericsT

Namespace MetadataT.ExifT
#If Config <= Nightly Then 'Stage: Nightly
    ''' <summary>Provides low level access to stream containing exif IFD (Image File Directory) or SubIFD</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    ''' <version version="1.5.2">ASCII Exif data are required to end with nullchar and the nullchar is trimmed</version>
    Public Class ExifIfdReader
        ''' <summary>Settings that apply to reading</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public ReadOnly Settings As ExifReader.ExifReaderContext
        ''' <summary>Advanced CTor used by <see cref="ExifT.ExifReader"/>. Allows passing context and indication of read cancellation.</summary>
        ''' <param name="Exif"><see cref="ExifReader"/> that contains this IFD</param>
        ''' <param name="Offset">Offset of start of this IFD in <paramref name="Stream"/></param>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the Exif stream is reached unexpectedly.</exception>
        ''' <exception cref="InvalidEnumArgumentException">Directory entry of unknown data type found</exception>
        ''' <exception cref="InvalidDataException">Tag data of some are placed outside the tag and cannot be read -or- (recoverable) ASCII data does not end with nullchar</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Context"/> is null.</exception>
        ''' <param name="Context">Contains context and event handlers for this reading</param>
        ''' <param name="Cancelled">Output parameter. Is set to true when handler cancells reading of whole IFD body</param>
        ''' <filterpriority>2</filterpriority>
        <CLSCompliant(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Sub New(ByVal Exif As ExifReader, ByVal Offset As UInt32, ByVal Context As ExifReader.ExifReaderContext, <Runtime.InteropServices.Out()> ByRef Cancelled As Boolean)
            If Context Is Nothing Then Throw New ArgumentNullException("Context")
            Settings = Context
            _ExifReader = Exif
            _Offset = Offset
            Dim r As New Tools.IOt.BinaryReader(Exif.Stream, Exif.ByteOrder)
            Exif.Stream.Position = Offset
            Dim Entries As UShort = r.ReadUInt16
            Dim OrM As ExifReader.ReaderItemKinds = If(IsSubIfd, ExifReader.ReaderItemKinds.SubIfdMask, 0)
            Context.OnItem(Me, ExifT.ExifReader.ReaderItemKinds.IfdNumberOfEntries Or OrM, , , Entries, Offest, 2) 'Event
            Dim Pos As Long = Exif.Stream.Position
            If Not Context.OnItem(Me, ExifT.ExifReader.ReaderItemKinds.Ifd Or OrM, True, , , Pos, 12 * Entries) Then 'Event
                For i As Integer = 1 To Entries
                    Exif.Stream.Position = Pos
                    Dim Tag As UShort = r.ReadUInt16
                    Context.OnItem(Me, ExifT.ExifReader.ReaderItemKinds.TagNumber, , , Tag, Pos, 2) 'Event
                    Dim Kind As UShort = r.ReadUInt16
                    Context.OnItem(Me, ExifT.ExifReader.ReaderItemKinds.TagDataType, , , Kind, Pos + 2, 2) 'Event
                    Dim Components As UInt32 = r.ReadUInt32
                    Context.OnItem(Me, ExifT.ExifReader.ReaderItemKinds.TagComponents, , , Components, Pos + 4, 4) 'Event
                    Dim Data As Byte() = r.ReadBytes(4)
                    Context.OnItem(Me, ExifT.ExifReader.ReaderItemKinds.TagDataOrOffset, , , Data, Pos + 8, 4) 'Event
                    Pos = Exif.Stream.Position
                    If Not Context.OnItem(Me, ExifT.ExifReader.ReaderItemKinds.TagHeader, True, , , Pos - 12, 12) Then
                        Dim TagRead As New DirectoryEntry(Tag, Kind, Components, Data, Exif, Context)
                        Context.OnItem(Me, ExifT.ExifReader.ReaderItemKinds.Tag, , , TagRead) 'Event
                        Me._Entries.Add(TagRead)
                    End If
                Next i
                Exif.Stream.Position = Pos
            Else
                Exif.Stream.Position = Pos + 12 * Entries
                Cancelled = True
            End If
            _NextIFD = r.ReadUInt32
            Context.OnItem(Me, ExifT.ExifReader.ReaderItemKinds.NextIfdOffset Or OrM, , , _NextIFD, Exif.Stream.Position - 4, 4) 'Event
        End Sub
        ''' <summary>CTor (uses default settings)</summary>
        ''' <param name="Exif"><see cref="ExifReader"/> that contains this IFD</param>
        ''' <param name="Offset">Offset of start of this IFD in <paramref name="Stream"/></param>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the Exif stream is reached unexpectedly.</exception>
        ''' <exception cref="InvalidEnumArgumentException">Directory entry of unknown data type found</exception>
        ''' <exception cref="InvalidDataException">Tag data of some are placed outside the tag and cannot be read</exception>
        ''' <filterpriority>1</filterpriority>
        <CLSCompliant(False)> _
        Public Sub New(ByVal Exif As ExifReader, ByVal Offset As UInt32)
            Me.New(Exif, Offset, New ExifReader.ExifReaderContext(Exif, New ExifReaderSettings), False)
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

        ''' <summary>Gets value indicating if this instance is reader of IFD or SubIFD</summary>
        ''' <returns>True if this instance is reader of SubIFD. This implementation always returns false.</returns>
        Protected Overridable ReadOnly Property IsSubIfd() As Boolean
            Get
                Return False
            End Get
        End Property
    End Class

    ''' <summary>Represents reader of Sub IFD</summary>
    Public Class SubIFDReader : Inherits ExifIfdReader
        ''' <summary>CTor</summary>
        ''' <param name="Exif"><see cref="ExifReader"/> that contains this IFD</param>
        ''' <param name="Offset">Offset of start of this IFD in <paramref name="Stream"/></param>
        ''' <param name="Desc">Descriptive name of this Sub IFD</param>
        ''' <param name="ParentIFD"><see cref="ExifIFDReader"/> that represent IFD that contains current Sub IFD</param>
        ''' <param name="ParentRecord">Point to <see cref="ExifIFDReader.Entries"/> collection that points to record that points to this Sub IFD</param>
        ''' <param name="PreviousSubIFD">Sub IFD which's <see cref="ExifIFDReader.NextIFD"/> points to this Sub IFD. Can be null if this is first Sub IFD in line</param>
        ''' <param name="Cancelled">Output parameter set to true if reading of the ifd was cancelled in event handler.</param>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the Exif stream is reached unexpectedly.</exception>
        ''' <exception cref="InvalidEnumArgumentException">Directory entry of unknown data type found</exception>
        ''' <exception cref="InvalidDataException">Tag data of some are placed otside the tag and cannot be read</exception>
        <CLSCompliant(False)> _
        Public Sub New(ByVal Exif As ExifReader, ByVal Offset As UInt32, ByVal Desc As String, ByVal ParentIFD As ExifIfdReader, ByVal ParentRecord As Integer, Optional ByVal PreviousSubIFD As ExifIfdReader = Nothing, <Runtime.InteropServices.Out()> Optional ByRef Cancelled As Boolean = False)
            MyBase.New(Exif, Offset, ParentIFD.Settings, Cancelled)
            If Cancelled Then Exit Sub
            _Desc = Desc
            _ParentIFD = ParentIFD
            _ParentRecord = ParentRecord
            _PreviousSubIFD = PreviousSubIFD
        End Sub
        ''' <summary>Gets value indicating if this instance is reader of IFD or SubIFD</summary>
        ''' <returns>True if this instance is reader of SubIFD. This implementation always returns true.</returns>
        Protected Overrides ReadOnly Property IsSubIfd() As Boolean
            Get
                Return True
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="Desc"/> property</summary>
        Private _Desc As String
        ''' <summary>Contains value of the <see cref="ParentIFD"/> property</summary>
        Private _ParentIFD As ExifIfdReader
        ''' <summary>Contains value of the <see cref="ParentRecord"/> property</summary>
        Private _ParentRecord As Integer
        ''' <summary>Contains value of the <see cref="PreviousSubIFD"/> property</summary>
        Private _PreviousSubIFD As ExifIfdReader
        ''' <summary>Descriptive name of this Sub IFD</summary>
        ''' <returns>Usually contain an empty string for non starndard Sub IFDs and comon English name for standard Sub IFDs. For non-standard Sub IFDs only when library have some ideda what can this Sub IFD mean this Sub IFD is captioned somehow</returns>
        ''' <remarks>Currently there are no Non Standard Sub IFDs that have any caption, Captions of staandard Sub IFDs are public onstants declared in <see cref="ExifReader"/></remarks>
        Public ReadOnly Property Desc() As String
            Get
                Return _Desc
            End Get
        End Property
        ''' <summary><see cref="ExifIFDReader"/> that represent IFD that contains current Sub IFD</summary>
        Public ReadOnly Property ParentIFD() As ExifIfdReader
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
        Public ReadOnly Property PreviousSubIFD() As ExifIfdReader
            Get
                Return _PreviousSubIFD
            End Get
        End Property
    End Class

    ''' <summary>Represents read-only directory entry of Exif data</summary>
    ''' <version version="1.5.2" stage="Nightly">Updated to use 2×32 bit <see cref="SRational"/> and <see cref="URational"/> instead of 2×16 bits ones.</version>
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
        ''' <summary>CTor with <see cref="ExifReader.ExifReaderContext"/></summary>
        ''' <param name="Tag">Tag identifier</param>
        ''' <param name="Kind">Data type</param>
        ''' <param name="Components">Number of components</param>
        ''' <param name="Data">Data or offset to data</param>
        ''' <param name="Exif"><see cref="ExifReader"/> to obtain data from when <paramref name="Data"/> doesn't contain data but offset to data</param>
        ''' <param name="Context">Setting which takes effect on reading.</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Kind"/> is not member of <see cref="ExifDataTypes"/></exception>
        ''' <exception cref="InvalidDataException">Tag data are placed otside the tag and cannot be read -or- <paramref name="Kind"/> is <see cref="ExifDataTypes.ASCII"/> and ASCII string is not ended with nullchar. Not thrown when exception is recoverable via <paramref name="Context"/></exception>
        ''' <exception cref="ArgumentnullException"><paramref name="Context"/> is null</exception>
        ''' <version version="1.5.2"><see cref="InvalidDataException"/> is thrown when <paramref name="Kind"/> is <see cref="ExifDataTypes.ASCII"/> and string does not end with nullchar.</version>
        <CLSCompliant(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Sub New(ByVal Tag As UShort, ByVal Kind As ExifDataTypes, ByVal Components As UInt32, ByVal Data As Byte(), ByVal Exif As ExifReader, ByVal Context As ExifReader.ExifReaderContext)
            _Tag = Tag
            _DataType = Kind
            _Componets = Components
            Dim SizeTotal As Long
            Try
                SizeTotal = BytesPerComponent(Kind) * Components
            Catch ex As InvalidEnumArgumentException
                Context.OnError(ex) 'Throw
                SizeTotal = Components
            End Try
            _DataIsStoredOutside = SizeTotal > 4
            Dim TagData As Byte()
            If _DataIsStoredOutside Then
                Dim Str As New MemoryStream(Data, False)
                Dim r As New Tools.IOt.BinaryReader(Str, Exif.ByteOrder)
                Str.Position = 0
                Dim Offset As UInt32 = r.ReadUInt32
                Exif.Stream.Position = Offset
                ReDim TagData(SizeTotal - 1)
                Dim BytesRead As UInteger = 0
                While BytesRead < SizeTotal
                    Dim NowRead = Exif.Stream.Read(TagData, BytesRead, SizeTotal - BytesRead)
                    If NowRead = 0 Then Exit While
                    BytesRead += NowRead
                End While
                If BytesRead <> SizeTotal Then
                    Context.OnError(New InvalidDataException(ResourcesT.Exceptions.CannotReadTagDataFromStream)) 'Throw
                    If BytesRead > 0 Then _
                        ReDim Preserve TagData(BytesRead - 1) _
                    Else Erase TagData
                End If
                Context.OnItem(Me, ExifReader.ReaderItemKinds.ExternalTagData, , , TagData, Offset, SizeTotal) 'Event
            Else
                TagData = Data
            End If
            _Data = ReadData(Kind, TagData, Components, Exif.ByteOrder, Context)
        End Sub
        ''' <summary>CTor (uses default settings)</summary>
        ''' <param name="Tag">Tag identifier</param>
        ''' <param name="Kind">Data type</param>
        ''' <param name="Components">Number of components</param>
        ''' <param name="Data">Data or offset to data</param>
        ''' <param name="Exif"><see cref="ExifReader"/> to obtain data from when <paramref name="Data"/> doesn't contain data but offset to data</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Kind"/> is not member of <see cref="ExifDataTypes"/></exception>
        ''' <exception cref="InvalidDataException">Tag data are placed otside the tag and cannot be read</exception>
        <CLSCompliant(False)> _
        Public Sub New(ByVal Tag As UShort, ByVal Kind As ExifDataTypes, ByVal Components As UInt32, ByVal Data As Byte(), ByVal Exif As ExifReader)
            Me.new(Tag, Kind, Components, Data, Exif, New ExifReader.ExifReaderContext(Exif, New ExifReaderSettings()))
        End Sub

        ''' <summary>Reads data of specified type freom given <see cref="Array"/> of <see cref="Byte"/>s</summary>
        ''' <param name="Type">Type of data to read</param>
        ''' <param name="Align">Format in whicvh data are stored</param>
        ''' <param name="Buffer">Buffer to read data from</param>
        ''' <param name="Components">Number of components to be read</param>
        ''' <returns>Data read from buffer. If <paramref name="Components"/> is 1 scalar of specified type is returned, <see cref="Array"/> otherwise with exceptions: 1 component of type <see cref="ExifDataTypes.ASCII"/> resuts to <see cref="Char"/>, more components results to <see cref="String"/>; <see cref="ExifDataTypes.NA"/> always results to <see cref="Array"/> of <see cref="Byte"/>s</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="Type"/> is not member of <see cref="ExifDataTypes"/> (and error recovery is not allowed).</exception>
        ''' <exception cref="InvalidDataException"><paramref name="Type"/> is <see cref="ExifDataTypes.ASCII"/> and string is not terminated with nullchar  (and error recovery is not allowed).</exception>
        ''' <param name="Context">Setting which takes effect on reading.</param>
        ''' <version version="1.5.2">Updated to use 2×32 bits <see cref="SRational"/> and <see cref="URational"/> instead fo 2×16 bits.</version>
        ''' <version version="1.5.2">ASCII values are required to end with nullchar, it's trimmed and <see cref="InvalidDataException"/> is thrown when they don't end.</version>
        Private Shared Function ReadData(ByVal Type As ExifDataTypes, ByVal Buffer As Byte(), ByVal Components As Integer, ByVal Align As Tools.IOt.BinaryReader.ByteAlign, ByVal Context As ExifReader.ExifReaderContext) As Object
            Dim Str As New MemoryStream(Buffer, False)
            Str.Position = 0
            Dim r As New Tools.IOt.BinaryReader(Str, System.Text.Encoding.Default, Align)
            Select Case Type
                Case ExifDataTypes.Byte
                    If Components = 1 Then Return CByte(Buffer(0)) Else Return Buffer.Clone
                Case ExifDataTypes.ASCII
                    If Components = 1 Then
                        Dim ret = r.ReadChar()
                        If ret <> vbNullChar Then
                            Context.OnError(New InvalidDataException(ResourcesT.Exceptions.ValueIsInvalidASCIIValueBecauseItIsNotTerminatedWith)) 'Throw
                        End If
                        Return ret
                    Else
                        Dim ret As New String(r.ReadChars(Buffer.Length))
                        If ret(ret.Length - 1) = vbNullChar Then
                            Return ret.Substring(0, ret.Length - 1)
                        Else
                            Context.OnError(New InvalidDataException(ResourcesT.Exceptions.ValueIsInvalidASCIIValueBecauseItIsNotTerminatedWith)) 'Throw
                            Return ret
                        End If
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
                        Return New URational(r.ReadUInt32, r.ReadUInt32)
                    Else
                        Dim ret(Components - 1) As URational
                        For i As Integer = 1 To Components
                            ret(i - 1) = New URational(r.ReadUInt32, r.ReadUInt32)
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
                        Return New SRational(r.ReadInt32, r.ReadInt32)
                    Else
                        Dim ret(Components - 1) As SRational
                        For i As Integer = 1 To Components
                            ret(i - 1) = New SRational(r.ReadInt32, r.ReadInt32)
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
                Case Else
                    Context.OnError(New InvalidEnumArgumentException("Type", Type, GetType(ExifDataTypes))) 'Throw
                    Return Buffer.Clone
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
