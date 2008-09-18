Imports System.IO
Namespace DrawingT.MetadataT.ExifT
#If Config <= Nightly Then 'Stage: Nightly
    ''' <summary>Provides low level access to stream of Exif data</summary>
    <Author("–onny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(ExifReader), LastChange:="04/25/2007")> _
    Public Class ExifReader
        ''' <summary>Name of Exif Sub IFD (see <see cref="SubIFD.Desc"/>)</summary>
        Public Const ExifSubIFDName As String = "Exif Sub IFD"
        ''' <summary>Name of Exif Sub IFD (see <see cref="SubIFD.Desc"/>)</summary>
        Public Const GPSSubIFDName As String = "GPS Sub IFD"
        ''' <summary>Name of Exif Interoperability Sub IFD (see <see cref="IFDInterop"/>)</summary>
        Public Const ExifInteroperabilityName As String = "Exif Interoperability IFD"
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
        ''' <summary>Defines read modes for <see cref="ExifReader"/></summary>
        Public Enum ReadModes
            ''' <summary>Data are read and stored</summary>
            ReadAndStore
            ''' <summary>data are read and are not stored. You can catch data as they are read by handling events of <see cref="ExifReaderSettings"/></summary>
            Read
        End Enum
        ''' <summary>Settings for <see cref="ExifReader"/></summary>
        Public NotInheritable Class ExifReaderSettings
            ''' <summary>Contains value of the <see cref="ErrorRecovery"/> property</summary>
            Private _ErrorRecovery As ErrorRecoveryModes = ErrorRecoveryModes.ThrowException
            ''' <summary>Contains value of the <see cref="ReadMode"/> property</summary>
            Private _ReadMode As ReadModes = ReadModes.ReadAndStore
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
            ''' <summary>Gets or sets read mode</summary>
            ''' <returns>Current read mode</returns>
            ''' <value>Default value is <see cref="ReadModes.ReadAndStore"/></value>
            ''' <exception cref="InvalidEnumArgumentException">Value being set is not member of <see cref="ReadModes"/></exception>
            ''' <remarks>Changes of value of this property after parsing was started does not affect parsing</remarks>
            Public Property ReadMode() As ReadModes
                Get
                    Return _ReadMode
                End Get
                Set(ByVal value As ReadModes)
                    If Not InEnum(value) Then Throw New InvalidEnumArgumentException("value", value, value.GetType)
                    _ReadMode = value
                End Set
            End Property
            ''' <summary>Gets or sets value indicating if thumbnail image from exif is read and cached</summary>
            ''' <returns>True if thumbnail is read and cached to <see cref="IfdMain.ThumbnailData"/>. False when image is not chached</returns>
            ''' <value>Default value is false</value>
            ''' <remarks>Changes of value of this property after parsing was started does not affect parsing</remarks>
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
            Public Custom Event ReadError As EventHandler(Of ExifReader, ComponentModelT.RecovyExceptionEventArgs)
                AddHandler(ByVal value As EventHandler(Of ExifReader, ComponentModelT.RecovyExceptionEventArgs))
                    If ReadErrorHandler Is Nothing Then ReadErrorHandler = value _
                    Else ReadErrorHandler = [Delegate].Combine(ReadErrorHandler, value)
                End AddHandler
                RemoveHandler(ByVal value As EventHandler(Of ExifReader, ComponentModelT.RecovyExceptionEventArgs))
                    If ReadErrorHandler Is Nothing Then Return
                    ReadErrorHandler = [Delegate].Remove(ReadErrorHandler, value)
                End RemoveHandler
                RaiseEvent(ByVal sender As ExifReader, ByVal e As ComponentModelT.RecovyExceptionEventArgs)
                    ReadErrorHandler.Invoke(sender, e)
                End RaiseEvent
            End Event
            ''' <summary>Contains invocation list for the <see cref="ReadError"/> event</summary>
            Friend ReadErrorHandler As EventHandler(Of ExifReader, ComponentModelT.RecovyExceptionEventArgs)
            ''' <summary>Event raised when Exif metadata item is encountered</summary>
            ''' <remarks>Handlers added after parsing was started are ignored.
            ''' <para>The only generic way to cancel reading of Exif metadata before it is completed is throwin an exception from handler. Some Exif items can be skipped.</para></remarks>
            ''' <param name="sender">The actual object which implements reading of current item.</param>
            Public Custom Event ReadItem As EventHandler(Of Object, ExifEventArgs)
                AddHandler(ByVal value As EventHandler(Of Object, ExifEventArgs))
                    ReadItemEventHandler = [Delegate].Combine(ReadItemEventHandler, value)
                End AddHandler
                RemoveHandler(ByVal value As EventHandler(Of Object, ExifEventArgs))
                    ReadItemEventHandler = [Delegate].Remove(ReadItemEventHandler, value)
                End RemoveHandler
                RaiseEvent(ByVal sender As Object, ByVal e As ExifEventArgs)
                    ReadItemEventHandler.Invoke(sender, e)
                End RaiseEvent
            End Event
            ''' <summary>Contains invocatipon list for the <see cref="ReadItem"/> event</summary>
            Friend ReadItemEventHandler As EventHandler(Of Object, ExifEventArgs)
        End Class
        ''' <summary>Arguments of event which occures during Exif metedata reading</summary>
        Public Class ExifEventArgs : Inherits EventArgs
            ''' <summary>Contains value of the <see cref="Skip"/> property</summary>
            Private _Skip As Boolean
            ''' <summary>Contains value of the <see cref="CanSkip"/> property</summary>
            Private ReadOnly _CanSkip As Boolean
            ''' <summary>Contains value of the <see cref="ItemStream"/> property</summary>
            Private ReadOnly _ItemStream As io.Stream
            ''' <summary>Contains value of the <see cref="ItemObject"/> property</summary>
            Private ReadOnly _ItemObject As Object
            ''' <summary>Contains value of the <see cref="ItemKind"/> property</summary>
            Private ReadOnly _ItemKind As ReaderItemKinds
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
            ''' <summary>CTor</summary>
            ''' <param name="ItemKind">Kind of item being currently read</param>
            ''' <param name="CanSkip">Indicates if calle supports skipping of the item</param>
            ''' <param name="ItemStream">If item has stream, the stream; null otherwise. The item is always represented by whole stream. Handler must seek the streem to zero. Strem must support seeking and reading. Streem position can be initially nonzero, but it does not mena that object data starts at current position. Handler can change position in streem - caller must be able to recover from it.</param>
            ''' <param name="ItemObject">If item can be reprsesented as object, the object; null otherwise</param>
            ''' <exception cref="ArgumentException"><paramref name="ItemStream"/> is not null and it either does not support seeking or reading (<see cref="IO.Stream.CanSeek"/> or <see cref="IO.Stream.CanRead"/> is false)</exception>
            Public Sub New(ByVal ItemKind As ReaderItemKinds, Optional ByVal CanSkip As Boolean = False, Optional ByVal ItemStream As IO.Stream = Nothing, Optional ByVal ItemObject As Object = Nothing)
                If ItemStream IsNot Nothing AndAlso (Not ItemStream.CanSeek OrElse Not ItemStream.CanRead) Then _
                    Throw New ArgumentException(ResourcesT.Exceptions.StreamRepresentingExifMetadataItemMustSupportSeekingAndReading)
                Me._ItemKind = ItemKind
                Me._CanSkip = CanSkip
                Me._ItemStream = ItemStream
                Me._ItemObject = ItemObject
            End Sub
        End Class
        ''' <summary>Kinds of Exif metadata items as reported by reader events</summary>
        ''' <seealso cref="ExifEventArgs"/><seealso cref="ExifEventArgs.ItemKind"/>
        Public Enum ReaderItemKinds
            'TODO:Items
        End Enum
        ''' <summary>Run-time read-only representation of <see cref="ExifReaderSettings"/></summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class ExifReaderContext
            ''' <summary>Error recovery mode</summary>
            ''' <seealso cref="ExifReaderSettings.ErrorRecovery"/>
            Public ReadOnly ErrorRecovery As ErrorRecoveryModes
            ''' <summary>Read mode</summary>
            ''' <seealso cref="ExifReaderSettings.ReadMode"/>
            Public ReadOnly ReadMode As ReadModes
            ''' <summary>True to read thumbnail to <see cref="IfdMain.ThumbnailData"/></summary>
            ''' <seealso cref="ExifReaderSettings.ReadThumbnail"/>
            Public ReadOnly ReadThumbnail As Boolean
            ''' <summary><see cref="ExifT.ExifReader"/> these settings are valid for</summary>
            Public ReadOnly ExifReader As ExifReader
            ''' <summary>Handler of error. </summary>
            ''' <seealso cref="ExifReaderSettings.ReadError"/>
            Private ReadOnly OnErrorhandler As EventHandler(Of ExifReader, ComponentModelT.RecovyExceptionEventArgs)
            ''' <summary>Handler for item-read event</summary>
            ''' <seealso cref="ExifReaderSettings.ReadItem"/>
            Public ReadOnly OnItemHandler As EventHandler(Of Object, ExifEventArgs)
            ''' <summary>CTor</summary><param name="Settings">An <see cref="ExifReaderSettings"/> to populate new instance with</param>
            ''' <param name="Owner"><see cref="ExifT.ExifReader"/> these settings are valid for</param>
            ''' <exception cref="ArgumentNullException"><paramref name="Settings"/> is null</exception>
            Public Sub New(ByVal Owner As ExifReader, ByVal Settings As ExifReaderSettings)
                If Settings Is Nothing Then Throw New ArgumentNullException("Settings")
                Me.ErrorRecovery = Settings.ErrorRecovery
                Me.ReadMode = Settings.ReadMode
                Me.ReadThumbnail = Settings.ReadThumbnail
                Me.OnErrorhandler = Settings.ReadErrorHandler
                Me.ExifReader = Owner
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
                        Dim e As New ComponentModelT.RecovyExceptionEventArgs(Ex)
                        OnErrorhandler.Invoke(ExifReader, e)
                        If Not e.Recover Then Throw (Ex)
                    Case ErrorRecoveryModes.ThrowException
                        Throw (Ex)
                End Select
            End Sub
        End Class
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
        Private Sub Parse() 'TODO: Pass context down
            'Todo:Raise events
            Stream.Position = 0
            Dim Reader As New Tools.IOt.BinaryReader(Stream, Tools.IOt.BinaryReader.ByteAlign.BigEndian)
            Dim BOM1 As Char = Reader.ReadChar
            Dim BOM2 As Char = Reader.ReadChar
            If BOM1 = "I"c AndAlso BOM2 = "I"c Then
                Reader.ByteOrder = Tools.IOt.BinaryReader.ByteAlign.LittleEndian
            ElseIf BOM1 = "M"c AndAlso BOM2 = "M"c Then
                Reader.ByteOrder = Tools.IOt.BinaryReader.ByteAlign.BigEndian
            Else
                Throw New InvalidDataException(ResourcesT.Exceptions.UnknownByteOrderMark & BOM1 & BOM2)
            End If
            _ByteOrder = Reader.ByteOrder
            Dim BOMTest As UShort = Reader.ReadUInt16
            If BOMTest <> &H2AUS Then Throw New InvalidDataException(ResourcesT.Exceptions.InvalidValueForByteOrderTestAtExifHeader & Hex(BOMTest))

            Dim IFDOffset As UInt32 = Reader.ReadUInt32
            While IFDOffset <> 0UI
                Stream.Position = IFDOffset
                _IFDs.Add(New ExifIFDReader(Me, IFDOffset))
                IFDOffset = _IFDs(_IFDs.Count - 1).NextIFD
            End While
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
                _OtherSubIFDs.Add(New SubIFDReader(Me, [Next], "", Container, MarkerIndex, Parent))
                Parent = _OtherSubIFDs(_OtherSubIFDs.Count - 1)
            End While
        End Sub
        ''' <summary>Contains value of the <see cref="OtherSubIFDs"/> property</summary>
        Private _OtherSubIFDs As New List(Of SubIFDReader)
        ''' <summary>Contains all unexpectedly (by chance) found Sub IFDs that cannot be recognized as starndard one. Those Sub IFDs are usually found as successors of standard ones</summary>
        ''' <remarks>This collection doesnù contain standard Sub IFDs that was recognized like Exif Sub IFD</remarks>
        Public ReadOnly Property OtherSubIFDs() As Tools.CollectionsT.GenericT.IReadOnlyList(Of SubIFDReader)
            Get
                Return New Tools.CollectionsT.GenericT.ReadOnlyListAdapter(Of SubIFDReader)(_OtherSubIFDs)
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
        Public ReadOnly Property IFDs() As CollectionsT.GenericT.IReadOnlyList(Of ExifIFDReader)
            Get
                Return New CollectionsT.GenericT.ReadOnlyListAdapter(Of ExifIFDReader)(_IFDs)
            End Get
        End Property
    End Class

    ''' <summary>Represents reader of Sub IFD</summary>
    Public Class SubIFDReader : Inherits ExifIFDReader
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
#End If
End Namespace
