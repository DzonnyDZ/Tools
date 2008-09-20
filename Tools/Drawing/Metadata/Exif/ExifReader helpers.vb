Imports System.IO
#If Config <= Nightly Then 'Stage: Nightly
Namespace DrawingT.MetadataT.ExifT
    Partial Class ExifReader
        ''' <summary>Arguments of event which occures during Exif metedata reading</summary>
        Public Class ExifEventArgs : Inherits EventArgs
#Region "Fields"
            ''' <summary>Contains value of the <see cref="Skip"/> property</summary>
            Private _Skip As Boolean
            ''' <summary>Contains value of the <see cref="CanSkip"/> property</summary>
            Private ReadOnly _CanSkip As Boolean
            ''' <summary>Contains value of the <see cref="ItemStream"/> property</summary>
            Private ReadOnly _ItemStream As IO.Stream
            ''' <summary>Contains value of the <see cref="ItemObject"/> property</summary>
            Private ReadOnly _ItemObject As Object
            ''' <summary>Contains value of the <see cref="ItemKind"/> property</summary>
            Private ReadOnly _ItemKind As ReaderItemKinds
            ''' <summary>Contains value of the <see cref="ItemAbsoluteOffset"/> property</summary>
            Private ReadOnly _ItemAbsoluteOffset As Long = -1
            ''' <summary>Contains value of the the <see cref="ItemLength"/> property</summary>
            Private ReadOnly _ItemLength As Long = -1
#End Region
#Region "Properties"
            ''' <summary>Gets or sets value indicating if current Exif metadata item should be skipped</summary>
            ''' <returns>True to skip the item (do not read its interior), false to read and parse the item</returns>
            ''' <value>Set this property to true to skip part of Exif metadata and save time by skipping reading of information which you do not need.</value>
            ''' <exception cref="InvalidOperationException"><see cref="CanSkip"/> is valsa and value is being set to true</exception>
            ''' <remarks>When some metadata are skipped, the in-memory representation is incomplete and should not be used for round-trip reads and writes because it can cause data loss.</remarks>
            Public Property Skip() As Boolean
                Get
                    Return _Skip
                End Get
                Set(ByVal value As Boolean)
                    If Not CanSkip AndAlso value = True Then _
                        Throw New InvalidOperationException(String.Format(ResourcesT.Exceptions.CannotBeSetTo1When2Is3, "Skip", "True", "CanSkip", "False"))
                    _Skip = value
                End Set
            End Property
            ''' <summary>Gets value indicating if the <see cref="Skip"/> property can be set to true</summary>
            ''' <returns>True when the <see cref="Skip"/> property can be set to true; false otherwise.</returns>
            ''' <seealso cref="Skip"/>
            Public ReadOnly Property CanSkip() As Boolean
                Get
                    Return _CanSkip
                End Get
            End Property
            ''' <summary>Gets stream that contains data of currently read Exif metadata item</summary>
            ''' <returns>Stream associated with current metadata item or null if no stream is associated.</returns>
            ''' <remarks>The stream always supports seeking and is read-only. The stream posicion can be invalid. Always seek to strem start before reading it!</remarks>
            Public ReadOnly Property ItemStream() As IO.Stream
                Get
                    Return _ItemStream
                End Get
            End Property
            ''' <summary>Gets object associated with currently read Exif metadata item. Can be null.</summary>
            ''' <returns>Associated object or null when no object is associated</returns>
            Public ReadOnly Property ItemObject() As Object
                Get
                    Return _ItemObject
                End Get
            End Property
            ''' <summary>Gets value indicating which kind of item was read</summary>
            Public ReadOnly Property ItemKind() As ReaderItemKinds
                Get
                    Return _ItemKind
                End Get
            End Property
            ''' <summary>Gets absolute position (from start of Exif (TIFF) header) of Exif metadata item beig red</summary>
            ''' <returns>TIFF header-relative position or -1 if position is not set</returns>
            Public ReadOnly Property ItemAbsoluteOffset() As Long
                Get
                    Return _ItemAbsoluteOffset
                End Get
            End Property
            ''' <summary>Gets length of Exif metadata item being read in Bytes</summary>
            ''' <returns>The lenght of bytes or -1 if lenght is not used by this item</returns>
            Public ReadOnly Property ItemLength() As Long
                Get
                    Return _ItemLength
                End Get
            End Property
#End Region
            ''' <summary>CTor</summary>
            ''' <param name="ItemKind">Kind of item being currently read</param>
            ''' <param name="CanSkip">Indicates if calle supports skipping of the item</param>
            ''' <param name="ItemStream">If item has stream, the stream; null otherwise. The item is always represented by whole stream. Handler must seek the streem to zero. Strem must support seeking and reading. Streem position can be initially nonzero, but it does not mena that object data starts at current position. Handler can change position in streem - caller must be able to recover from it.</param>
            ''' <param name="ItemObject">If item can be reprsesented as object, the object; null otherwise</param>
            ''' <exception cref="ArgumentException"><paramref name="ItemStream"/> is not null and it either does not support seeking or reading (<see cref="IO.Stream.CanSeek"/> or <see cref="IO.Stream.CanRead"/> is false)</exception>
            ''' <param name="Pos">Tiff-header realetive position of start of item (-1 if not supported by item)</param>
            ''' <param name="Len">Lenght (in bytes) of item. (-1 if not supported by the item)</param>
            Public Sub New(ByVal ItemKind As ReaderItemKinds, Optional ByVal CanSkip As Boolean = False, Optional ByVal ItemStream As IO.Stream = Nothing, Optional ByVal ItemObject As Object = Nothing, Optional ByVal Pos As Long = -1, Optional ByVal Len As Long = -1)
                If ItemStream IsNot Nothing AndAlso (Not ItemStream.CanSeek OrElse Not ItemStream.CanRead) Then _
                    Throw New ArgumentException(ResourcesT.Exceptions.StreamRepresentingExifMetadataItemMustSupportSeekingAndReading)
                Me._ItemKind = ItemKind
                Me._CanSkip = CanSkip
                Me._ItemStream = ItemStream
                Me._ItemObject = ItemObject
                Me._ItemLength = Len
                Me._ItemAbsoluteOffset = Pos
            End Sub
        End Class
        ''' <summary>Kinds of Exif metadata items as reported by reader events</summary>
        ''' <seealso cref="ExifEventArgs"/><seealso cref="ExifEventArgs.ItemKind"/>
        ''' <remarks>This enumeration is also used by <see cref="ExifMapGenerator"/>, but only some (the most low-level ones) members are valid for it.</remarks>
        Public Enum ReaderItemKinds As Byte
            ''' <summary>Not used by <see cref="ExifReaderSettings.ReadItem"/>, but used by <see cref="ExifMapGenerator"/> to indicate bytes of unknown purpose in Exif metadata block</summary>
            <EditorBrowsable(EditorBrowsableState.Advanced)> Unknown
            ''' <summary>
            ''' Raised only once at the beginning or reading.
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> contains whole strem.</item>
            ''' <item>Sender is <see cref="ExifReader"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is null.</item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is true.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> is 0.</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is length of whole stream.</item>
            ''' </list>
            ''' <para>Caught by <see cref="ExifMapGenerator"/> to determine length of stream but not used in map.</para></summary>
            Exif = 1
            ''' <summary>
            ''' Raised when the Byte Order mark (BOM) is read. This is the 2nd event and occures only once.
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> is null.</item>
            ''' <item>Sender is <see cref="ExifReader"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is BOM text ("II" or "MM" or any 2-characters long string in case <see cref="InvalidDataException"/> passed to <see cref="ExifReaderSettings.ReadError"/> will immediatelly follow; you can recover the exception and Little Endian byte order will be used.)</item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is false.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> is 0.</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is 2.</item>
            ''' </list><para>Used by <see cref="ExifMapGenerator"/></para>
            ''' </summary>
            Bom = 2
            ''' <summary>
            ''' Raised when the Byte Order mark test is read. This is the 3rd event and occures only once.
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> is null.</item>
            ''' <item>Sender is <see cref="ExifReader"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is 0x2A (<see cref="UShort"/>) (or any other number when unrecoverable <see cref="InvalidDataException"/> will be throwm immediatelly.)</item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is false.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> is 2.</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is 2.</item>
            ''' </list><para>Used by <see cref="ExifMapGenerator"/></para>
            ''' </summary>
            BomTest = 3
            ''' <summary>
            ''' Raised when offset to 1st IFD (IFD0) is read. This is the 4th event and occures only once.
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> is null.</item>
            ''' <item>Sender is <see cref="ExifReader"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is <see cref="UInt32"/> offset to 1st IFD (usually 0x8)</item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is false.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> is 4.</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is 4.</item>
            ''' </list><para>Used by <see cref="ExifMapGenerator"/></para>
            ''' </summary>
            Ifd0Offset = 4
            ''' <summary>
            ''' Raised when number of entries in IFD is read. Occurs for each IFD and SubIFD.
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> is null.</item>
            ''' <item>Sender is <see cref="ExifIfdReader"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is <see cref="UInt16"/> number of entries</item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is false.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> is position of the number.</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is 2.</item>
            ''' </list><para>Used by <see cref="ExifMapGenerator"/></para>
            ''' </summary>
            IfdNumberOfEntries = 5
            ''' <summary>
            ''' Raised before IFD is read.
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> is null.</item>
            ''' <item>Sender is <see cref="ExifIfdReader"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is null</item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is true.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> is position of first entry.</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is 12 * numjber of entries.</item>
            ''' </list>
            ''' </summary>
            Ifd = 6
            ''' <summary>
            ''' Raised when tag number is read.
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> is null.</item>
            ''' <item>Sender is <see cref="ExifIfdReader"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is <see cref="UInt16"/> tag number</item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is false.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> is position of tag number.</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is 2.</item>
            ''' </list><para>Used by <see cref="ExifMapGenerator"/></para>
            ''' </summary>
            TagNumber = 7
            ''' <summary>
            ''' Raised when tag data type is read.
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> is null.</item>
            ''' <item>Sender is <see cref="ExifIfdReader"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is <see cref="UInt16"/> tag data type (see <see cref="ExifDataTypes"/>)</item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is false.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> is position of data type number.</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is 2.</item>
            ''' </list><para>Used by <see cref="ExifMapGenerator"/></para>
            ''' </summary>
            TagDataType = 8
            ''' <summary>
            ''' Raised when tag number of components is read.
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> is null.</item>
            ''' <item>Sender is <see cref="ExifIfdReader"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is <see cref="UInt32"/> number of components</item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is false.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> is position of tag number of bcomponents.</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is 4.</item>
            ''' </list><para>Used by <see cref="ExifMapGenerator"/></para>
            ''' </summary>
            TagComponents = 9
            ''' <summary>
            ''' Raised when tag data part is read. Note: Tag data part contains either data or pointer to data depending on if data fits into 4 bytes.
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> is null.</item>
            ''' <item>Sender is <see cref="ExifIfdReader"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is <see cref="Byte()"/> 4-bytes data part of tag</item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is false.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> is position data part of tag.</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is 4.</item>
            ''' </list><para>Used by <see cref="ExifMapGenerator"/></para>
            ''' </summary>
            TagDataOrOffset = 10
            ''' <summary>
            ''' Raised when whole tag header is read, before tag data are parsed.
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> is null.</item>
            ''' <item>Sender is <see cref="ExifIfdReader"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is null</item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is true.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> is position of tag number.</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is 12.</item>
            ''' </list>
            ''' This event carrys no information about tag being red. All necessary informations can be collected from events <see cref="TagNumber"/>, <see cref="TagDataType"/>, <see cref="TagComponents"/> and <see cref="TagDataOrOffset"/> which always preceed this event in given order.
            ''' </summary>
            TagHeader = 11
            ''' <summary>
            ''' Raised when tag data stored outside of tag are read. Raised ony for tags which has data longer than 4 bytes.
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> is null.</item>
            ''' <item>Sender is <see cref="DirectoryEntry"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is <see cref="Byte()"/> array with data.</item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is false.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> is position of data.</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is length of data (if previously the <see cref="InvalidOperationException"/> was recovered the actual lenght of data is less).</item>
            ''' </list><para>Used by <see cref="ExifMapGenerator"/></para></summary>
            ExternalTagData = 12
            ''' <summary>
            ''' Raised after whole tag if read and parsed
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> is null.</item>
            ''' <item>Sender is <see cref="ExifIFDReader"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is <see cref="DirectoryEntry"/></item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is false.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> is -1 (not supported).</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is -1 (not supported).</item>
            ''' </list></summary>
            Tag = 13
            ''' <summary>
            ''' Raised when pointer to next IFD is read at the end of each IFD
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> is null.</item>
            ''' <item>Sender is <see cref="ExifIFDReader"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is <see cref="UInt32"/> pointer to next IFD.</item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is false.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> position of pointer.</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is 4.</item>
            ''' </list><para>Used by <see cref="ExifMapGenerator"/></para></summary>
            NextIfdOffset = 14
            ''' <summary>
            ''' Raised before thumbnail is read.
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> is null.</item>
            ''' <item>Sender is <see cref="IfdMain"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is null.</item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is true.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> is -1 (not supported).</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is -1 (not supported).</item>
            ''' </list></summary>
            Thumbnail = 15
            ''' <summary>
            ''' Raised when reading JPEG thumbnail data.
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> is JPEG thumbnail data stream.</item>
            ''' <item>Sender is <see cref="IfdMain"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is null.</item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is false.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> is offset of thumbnail SOI (where JPEG data starts).</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is length of JPEG data.</item>
            ''' </list><para>Used by <see cref="ExifMapGenerator"/></para></summary>
            JpegThumbnail = 16
            ''' <summary>
            ''' Raised when reading part of TIFF thumbnailo data (TIFF thumbnail can be placed in more than one part).
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> is stream to that part of data.</item>
            ''' <item>Sender is <see cref="IfdMain"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is null.</item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is false.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> is offset to start of stream of part.</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is legnth of part.</item>
            ''' </list><para>Used by <see cref="ExifMapGenerator"/></para></summary>
            TiffThumbnailPart = 17
            ''' <summary>
            ''' Raised after all TIFF thumbnail sub-streams are detected.
            ''' <list type="bullet">
            ''' <item><see cref="ExifEventArgs.ItemStream"/> stream of all TIFF thumbnail data.</item>
            ''' <item>Sender is <see cref="IfdMain"/>.</item>
            ''' <item><see cref="ExifEventArgs.ItemObject"/> is null.</item>
            ''' <item><see cref="ExifEventArgs.CanSkip"/> is false.</item>
            ''' <item><see cref="ExifEventArgs.ItemAbsoluteOffset"/> is -1 (not supported).</item>
            ''' <item><see cref="ExifEventArgs.ItemLength"/> is -1 (not supported).</item>
            ''' </list></summary>
            TiffThumbNail = 18
            ''' <summary>Or mask that applies to <see cref="IfdNumberOfEntries"/>, <see cref="Ifd"/> and <see cref="NextIfdOffset"/>
            ''' to make <see cref="SubIfdNumberOfEntries"/>, <see cref="SubIfd"/> and <see cref="NextSubIfdOffset"/> respectively.
            ''' This value itself is never used as event identifier.</summary>
            <EditorBrowsable(EditorBrowsableState.Advanced)> SubIfdMask = 128
            ''' <summary>Same meaning as <see cref="IfdNumberOfEntries"/> but for SubIFD. <para>Used by <see cref="ExifMapGenerator"/></para></summary>
            SubIfdNumberOfEntries = IfdNumberOfEntries Or SubIfdMask
            ''' <summary>Same meaning as <see cref="Ifd"/> but for SubIFD. <para>Used by <see cref="ExifMapGenerator"/></para></summary>
            SubIfd = Ifd Or SubIfdMask
            ''' <summary>Same meaning as <see cref="NextIfdOffset"/> but for SubIFD. <para>Used by <see cref="ExifMapGenerator"/></para></summary>
            NextSubIfdOffset = NextIfdOffset Or SubIfdMask
        End Enum
        ''' <summary>Run-time read-only representation of <see cref="ExifReaderSettings"/></summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ExifReaderContext
            ''' <summary>Error recovery mode</summary>
            ''' <seealso cref="ExifReaderSettings.ErrorRecovery"/>
            Public ReadOnly ErrorRecovery As ErrorRecoveryModes
            '''' <summary>Read mode</summary>
            '''' <seealso cref="ExifReaderSettings.ReadMode"/>
            'Public ReadOnly ReadMode As ReadModes
            ''' <summary>True to read thumbnail to <see cref="IfdMain.ThumbnailData"/></summary>
            ''' <seealso cref="ExifReaderSettings.ReadThumbnail"/>
            Public ReadOnly ReadThumbnail As Boolean
            ''' <summary><see cref="ExifT.ExifReader"/> these settings are valid for</summary>
            Public ReadOnly ExifReader As ExifReader
            ''' <summary>Handler of error. </summary>
            ''' <seealso cref="ExifReaderSettings.ReadError"/>
            Private ReadOnly OnErrorhandler As EventHandler(Of ExifReader, ComponentModelT.RecoveryExceptionEventArgs)
            ''' <summary>Handler for item-read event</summary>
            ''' <seealso cref="ExifReaderSettings.ReadItem"/>
            Private ReadOnly OnItemHandler As EventHandler(Of Object, ExifEventArgs)
            ''' <summary>Contains value of the <see cref="TiffHeaderOffset"/> property</summary>
            Private _TiffHeaderOffset As Long
            ''' <summary>Contains value indicating if value of the <see cref="TiffHeaderOffset"/> was already set</summary>
            Private TiffHeaderOffsetSet As Boolean
            ''' <summary>Absolute of start of TIFF header in all nested streams</summary>
            ''' <returns>In case the stream from which Exif data are read is <see cref="Tools.IOt.ConstrainedReadOnlyStream"/> contains value of the <see cref="IOt.ConstrainedReadOnlyStream.TruePosition"/> property when value of tzhe <see cref="IO.Stream.Position"/> is zero. In case the stream is not <see cref="IOt.ConstrainedReadOnlyStream"/> contains zero.</returns>
            ''' <value>Value of this property can be set only once. By default it is done by <see cref="ExifT.ExifReader"/> at the very beginning of reading.</value>
            ''' <exception cref="InvalidOperationException">Value of the property was alread set and is being set again</exception>
            Public Property TiffHeaderOffset() As Long
                Get
                    Return _TiffHeaderOffset
                End Get
                Set(ByVal value As Long)
                    If TiffHeaderOffsetSet Then Throw New InvalidOperationException(String.Format(ResourcesT.Exceptions.ValueOfThe0PropertyCannotBeChangedAfterItWasSet, "TiffHeaderOffset"))
                    _TiffHeaderOffset = value
                    TiffHeaderOffsetSet = True
                End Set
            End Property
            ''' <summary>CTor</summary><param name="Settings">An <see cref="ExifReaderSettings"/> to populate new instance with</param>
            ''' <param name="Owner"><see cref="ExifT.ExifReader"/> these settings are valid for</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Settings"/> is null</exception>
            Public Sub New(ByVal Owner As ExifReader, ByVal Settings As ExifReaderSettings)
                If Settings Is Nothing Then Throw New ArgumentNullException("Settings")
                Me.ErrorRecovery = Settings.ErrorRecovery
                'Me.ReadMode = Settings.ReadMode
                Me.ReadThumbnail = Settings.ReadThumbnail
                Me.OnErrorhandler = Settings.ReadErrorHandler
                Me.ExifReader = Owner
                Me.OnItemHandler = Settings.ReadItemEventHandler
            End Sub
            ''' <summary>Handles error during Exif parsing - either throws it or returns</summary>
            ''' <param name="Ex"><see cref="Exception"/> that is about to be thrown</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Ex"/> is null</exception>
            ''' <exception cref="Exception">Throws <paramref name="Ex"/> when either <see cref="ErrorRecovery"/> is <see cref="ErrorRecoveryModes.ThrowException"/> or <see cref="ErrorRecovery"/> is <see cref="ErrorRecoveryModes.Custom"/> and handler indicates that exception should be thrown.</exception>
            ''' <remarks>The aim of this method is to throw <paramref name="Ex"/> in case is should not be recoverd</remarks>
            Public Sub OnError(ByVal Ex As Exception)
                If Ex Is Nothing Then Throw New ArgumentNullException("Ex")
                Select Case ErrorRecovery
                    Case ErrorRecoveryModes.Custom
                        Dim e As New ComponentModelT.RecoveryExceptionEventArgs(Ex)
                        If OnErrorhandler IsNot Nothing Then OnErrorhandler.Invoke(ExifReader, e)
                        If Not e.Recover Then Throw (Ex)
                    Case ErrorRecoveryModes.ThrowException
                        Throw (Ex)
                End Select
            End Sub
            ''' <summary>Raises the <see cref="ExifReaderSettings.ReadItem"/> event</summary>
            ''' <param name="sender">Actual object that performs reading of now-read part of Exif metadata</param>
            ''' <param name="e">Event arguments</param>
            ''' <returns><paramref name="e"/>.<see cref="ExifEventArgs.Skip">Skip</see></returns>
            Public Function OnItem(ByVal sender As Object, ByVal e As ExifEventArgs) As Boolean
                If OnItemHandler Is Nothing Then Return e.Skip
                OnItemHandler.Invoke(sender, e)
                Return e.Skip
            End Function
            ''' <summary>Raqises the <see cref="ExifReaderSettings.ReadItem"/> event</summary>
            ''' <param name="sender">Actual object that performs reading of now-read part of Exif metadata</param>
            ''' <param name="Kind">Kind of item being currently read</param>
            ''' <param name="CanSkip">Indicates if calle supports skipping of the item</param>
            ''' <param name="str">If item has stream, the stream; null otherwise. The item is always represented by whole stream. Handler must seek the streem to zero. Strem must support seeking and reading. Streem position can be initially nonzero, but it does not mena that object data starts at current position. Handler can change position in streem - caller must be able to recover from it.</param>
            ''' <param name="obj">If item can be reprsesented as object, the object; null otherwise</param>
            ''' <exception cref="ArgumentException"><paramref name="ItemStream"/> is not null and it either does not support seeking or reading (<see cref="IO.Stream.CanSeek"/> or <see cref="IO.Stream.CanRead"/> is false)</exception>
            ''' <returns>Value indicating if now-read item should be skipped (see <see cref="ExifEventArgs.Skip"/>)</returns>
            ''' <param name="MaintainStreamPosition">True to ensure that <see cref="IO.Stream.Position"/> is same before and after the handler is called</param>
            ''' <param name="Pos">Tiff-header realetive position of start of item (-1 if not supported by item)</param>
            ''' <param name="Len">Lenght (in bytes) of item. (-1 if not supported by the item)</param>
            Public Function OnItem(ByVal sender As Object, ByVal Kind As ReaderItemKinds, Optional ByVal CanSkip As Boolean = False, Optional ByVal str As IO.Stream = Nothing, Optional ByVal obj As Object = Nothing, Optional ByVal Pos As Long = -1, Optional ByVal Len As Long = -1, Optional ByVal MaintainStreamPosition As Boolean = True) As Boolean
                Dim e As New ExifEventArgs(Kind, CanSkip, str, obj, Pos, Len)
                Dim OldPos As Long
                If str IsNot Nothing Then OldPos = str.Position
                Try
                    Return OnItem(sender, e)
                Finally
                    If str IsNot Nothing AndAlso MaintainStreamPosition Then str.Position = OldPos
                End Try
            End Function
        End Class
    End Class

    ''' <summary>Defines error recovery modes for <see cref="ExifReader"/></summary>
    Public Enum ErrorRecoveryModes
        ''' <summary>An exception is thrown for each error and reading is terminated</summary>
        ThrowException
        ''' <summary>If possible, errors are ignored, exceptions are not thrown and reading continues.</summary>
        ''' <remarks>You can get incomplete results in this scenario</remarks>
        Recover
        ''' <summary>The <see cref="ExifReaderSettings.ReadError"/> event is raised. Reading continues (if possible) or terminates depending on event handler</summary>
        Custom
    End Enum
    '''' <summary>Defines read modes for <see cref="ExifReader"/></summary>
    'Public Enum ReadModes
    '    ''' <summary>Data are read and stored</summary>
    '    ReadAndStore
    '    ''' <summary>data are read and are not stored. You can catch data as they are read by handling events of <see cref="ExifReaderSettings"/></summary>
    '    Read
    'End Enum
    ''' <summary>Settings for <see cref="ExifReader"/></summary>
    Public NotInheritable Class ExifReaderSettings
        ''' <summary>Contains value of the <see cref="ErrorRecovery"/> property</summary>
        Private _ErrorRecovery As ErrorRecoveryModes = ErrorRecoveryModes.ThrowException
        '''' <summary>Contains value of the <see cref="ReadMode"/> property</summary>
        'Private _ReadMode As ReadModes = ReadModes.ReadAndStore
        ''' <summary>Contains value of the <see cref="ReadThumbnail"/> property</summary>
        Private _ReadThumbnail As Boolean
        ''' <summary>Gets or sets error recovery mode</summary>
        ''' <returns>Current error recover mode</returns>
        ''' <value>Default value is <see cref="ErrorRecoveryModes.ThrowException"/>.</value>
        ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="ErrorRecoveryModes"/></exception>
        ''' <remarks>Changes of value of this property after parsing was started does not affect parsing</remarks>
        Public Property ErrorRecovery() As ErrorRecoveryModes
            Get
                Return _ErrorRecovery
            End Get
            Set(ByVal value As ErrorRecoveryModes)
                If Not InEnum(value) Then Throw New InvalidEnumArgumentException("value", value, value.GetType)
                _ErrorRecovery = value
            End Set
        End Property
        '''' <summary>Gets or sets read mode</summary>
        '''' <returns>Current read mode</returns>
        '''' <value>Default value is <see cref="ReadModes.ReadAndStore"/></value>
        '''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="ReadModes"/></exception>
        '''' <remarks>Changes of value of this property after parsing was started does not affect parsing</remarks>
        'Public Property ReadMode() As ReadModes
        '    Get
        '        Return _ReadMode
        '    End Get
        '    Set(ByVal value As ReadModes)
        '        If Not InEnum(value) Then Throw New InvalidEnumArgumentException("value", value, value.GetType)
        '        _ReadMode = value
        '    End Set
        'End Property
        ''' <summary>Gets or sets value indicating if thumbnail image from exif is read and cached</summary>
        ''' <returns>True if thumbnail is read and cached to <see cref="IfdMain.ThumbnailData"/>. False when image is not chached</returns>
        ''' <value>Default value is false</value>
        ''' <remarks>Changes of value of this property after parsing was started does not affect parsing.</remarks>
        Public Property ReadThumbnail() As Boolean
            Get
                Return _ReadThumbnail
            End Get
            Set(ByVal value As Boolean)
                _ReadThumbnail = value
            End Set
        End Property
        ''' <summary>Event is raised when there is an error during reading of Exif data and <see cref="ErrorRecovery"/> is <see cref="ErrorRecoveryModes.Custom"/></summary>
        ''' <remarks>Handler can decide to throw an exception or to recover. However it is not guaranted that recovery is always possible.
        ''' <para>Handlers added after parsing was started are ignored.</para>
        ''' <para>Allowing reader to recover from error can cause the in-memory representation of Exif metadata to be incomplete. Incomplete representation should never be used for round-trip reads and writes because it can cause data losss.</para>
        ''' </remarks>
        Public Custom Event ReadError As EventHandler(Of ExifReader, ComponentModelT.RecoveryExceptionEventArgs)
            AddHandler(ByVal value As EventHandler(Of ExifReader, ComponentModelT.RecoveryExceptionEventArgs))
                If ReadErrorHandler Is Nothing Then ReadErrorHandler = value _
                Else ReadErrorHandler = [Delegate].Combine(ReadErrorHandler, value)
            End AddHandler
            RemoveHandler(ByVal value As EventHandler(Of ExifReader, ComponentModelT.RecoveryExceptionEventArgs))
                If ReadErrorHandler Is Nothing Then Return
                ReadErrorHandler = [Delegate].Remove(ReadErrorHandler, value)
            End RemoveHandler
            RaiseEvent(ByVal sender As ExifReader, ByVal e As ComponentModelT.RecoveryExceptionEventArgs)
                ReadErrorHandler.Invoke(sender, e)
            End RaiseEvent
        End Event
        ''' <summary>Contains invocation list for the <see cref="ReadError"/> event</summary>
        Friend ReadErrorHandler As EventHandler(Of ExifReader, ComponentModelT.RecoveryExceptionEventArgs)
        ''' <summary>Event raised when Exif metadata item is encountered</summary>
        ''' <remarks>Handlers added after parsing was started are ignored.
        ''' <para>The only generic way to cancel reading of Exif metadata before it is completed is throwin an exception from handler. Some Exif items can be skipped.</para></remarks>
        ''' <param name="sender">The actual object which implements reading of current item.</param>
        Public Custom Event ReadItem As EventHandler(Of Object, ExifReader.ExifEventArgs)
            AddHandler(ByVal value As EventHandler(Of Object, exifreader.ExifEventArgs))
                ReadItemEventHandler = [Delegate].Combine(ReadItemEventHandler, value)
            End AddHandler
            RemoveHandler(ByVal value As EventHandler(Of Object, exifreader.ExifEventArgs))
                ReadItemEventHandler = [Delegate].Remove(ReadItemEventHandler, value)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As exifreader.ExifEventArgs)
                ReadItemEventHandler.Invoke(sender, e)
            End RaiseEvent
        End Event
        ''' <summary>Contains invocatipon list for the <see cref="ReadItem"/> event</summary>
        Friend ReadItemEventHandler As EventHandler(Of Object, ExifReader.ExifEventArgs)
    End Class
End Namespace
#End If