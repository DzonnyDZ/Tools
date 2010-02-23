Imports System.Linq
Imports RecordDic = Tools.CollectionsT.GenericT.DictionaryWithEvents(Of UShort, Tools.MetadataT.ExifT.ExifRecord)
Imports SubIFDDic = Tools.CollectionsT.GenericT.DictionaryWithEvents(Of UShort, Tools.MetadataT.ExifT.SubIfd)
Imports RecordList = Tools.CollectionsT.GenericT.ListWithEvents(Of Tools.MetadataT.ExifT.ExifRecord)
Imports SubIFDList = Tools.CollectionsT.GenericT.ListWithEvents(Of Tools.MetadataT.ExifT.SubIfd)
Imports Tools.ComponentModelT, Tools.IOt.StreamTools, Tools.ExtensionsT
Imports System.Runtime.InteropServices

Namespace MetadataT.ExifT
#If Config <= Nightly Then
    ''' <summary>Provides high-level acces to Exif metadata</summary>
    ''' <remarks>This <see cref="IMetadata"/> supports metadata keys in format "X:RecordNumber" where X is either number of main IFD or E for Exif sub-IFD, G for GPS sub-IFD or I for interop sub-IFD.</remarks>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    ''' <version version="1.5.2">Methods <see cref="Exif.Load"/>, <see cref="Exif.LoadForUpdating"/>, <see cref="Exif.Save"/> and <see cref="Exif.Update"/> added.</version>
    ''' <version version="1.5.2">Added implementation of <see cref="IReportsChange"/></version>
    ''' <version version="1.5.2">The <see cref="IMetadata"/> interface implemented</version>
    <Serializable()> _
    Public Class Exif
        Implements IReportsChange, Runtime.Serialization.ISerializable
        Implements IMetadata
        ''' <summary>Name used to identify Exif metadata in <see cref="IMetadataProvider"/></summary>
        ''' <version version="1.5.2">Constant introduced</version>
        Public Const ExifName$ = "Exif"
        ''' <summary>Do nothing CTor</summary>
        Public Sub New()
        End Sub
        ''' <summary>CTor - loads data from <see cref="ExifReader"/></summary>
        ''' <param name="reader"><see cref="ExifReader"/> to load data from</param>
        ''' <remarks>Instance created by this constructor cannot be easily used to updated Exif data stored in stream (i.e. file). Use <see cref="LoadForUpdating"/> for this purpose.</remarks>
        ''' <seelaso cref="LoadForUpdating"/><seelaso cref="Load"/>
        Public Sub New(ByVal Reader As ExifReader)
            Me.New()
            If Reader Is Nothing Then Exit Sub
            Dim i As Integer = 0
            Dim LastIFD As Ifd = Nothing
            For Each IFDReader As ExifIfdReader In Reader.IFDs
                If i = 0 Then
                    LastIFD = New IfdMain(IFDReader)
                    _IFD0 = LastIFD
                ElseIf i = 1 Then
                    LastIFD.Following = New IfdMain(IFDReader)
                    LastIFD = LastIFD.Following
                Else
                    LastIFD.Following = New Ifd(IFDReader)
                    LastIFD = LastIFD.Following
                End If
                LastIFD.Exif = Me
                i += 1
            Next IFDReader
            '    _ExifSubIFD = New IFDExif(reader.ExifSubIFD)
            '    _InteropSubIFD = New IFDInterop(reader.ExifInteroperabilityIFD)
            '    _GPSSubIFD = New IFDGPS(reader.GPSSubIFD)
            '    For Each SubIFD As ExifReader.SubIFD In reader.OtherSubIFDs
            '        _AdditionalIFDs.Add(RetrieveParent(SubIFD, reader), New IFD(SubIFD))
            '    Next SubIFD
        End Sub
        ''' <summary>Contains value of the <see cref="IFD0"/> property</summary>
        Private WithEvents _IFD0 As IfdMain
        ''' <summary>Gets or sets firts IFD of this instance (so-called Main IFD)</summary>
        ''' <returns>First IFD (so-called IFD0 or Main IFD) of current Exif metadata</returns>
        ''' <value>Sets firts IFD (so-called IFD0 or Main  IFD). It must be of type <see cref="IFDMain"/></value>
        ''' <exception cref="ArgumentException"><see cref="IFD.Exif"/> of value being set is non-null and is not current instance -or-
        ''' <see cref="IFD.Previous"/> of value being set is non-null. -or-
        ''' Value being set is already used somewhere else in this <see cref="Exif"/>.</exception>
        ''' <exception cref="TypeMismatchException">Value being set has <see cref="IFD.Following"/> set but it is not of type <see cref="IFDMain"/>.</exception>
        ''' <seelaso cref="ThumbnailIFD"/>
        Public Property IFD0() As IfdMain
            Get
                Return _IFD0
            End Get
            Set(ByVal value As IfdMain)
                If value.Exif IsNot Nothing AndAlso value.Exif IsNot Me Then _
                    Throw New ArgumentException(ResourcesT.Exceptions.IFDPassedToTheIFD0PropertyMustEitherHaveNoExifAsociatedOrMustHaveAssociatedCurrrentInstance)
                If value.Previous IsNot Nothing Then _
                    Throw New ArgumentException(ResourcesT.Exceptions.IFDPassedToTheIFD0PropertyCannotHaveThePreviousPropertySet)
                If Me.ContainsIFD(value) Then _
                    Throw New ArgumentException(ResourcesT.Exceptions.GivenIFDIsAlreadyInUse)
                If value.Following IsNot Nothing AndAlso Not TypeOf value.Following Is IfdMain Then _
                    Throw New TypeMismatchException(ResourcesT.Exceptions.TypeOfIFDFollowingAfterIFD0MustBeIFDMain, value.Following, GetType(IfdMain))
                _IFD0 = value
                value.Exif = Me
            End Set
        End Property
        ''' <summary>Gets or sets IFD1 - so called Thumbnail IFD</summary>
        ''' <returns>IFD1 if there is any; null otherwise</returns>
        ''' <value>Sets IFD1 - the <see cref="IFD.Following">following</see> IFD of <see cref="IFD0"/>. If <see cref="IFD0"/> is null it is set to an empty instance of <see cref="IFDMain"/></value>
        ''' <exception cref="ArgumentException">Value being set have <see cref="IFD0"/> as one of its <see cref="IFD.Following"/> IFDs =or= Value being set has non-null value of the <see cref="IFD.Previous"/> property. =or= Value being set has non-null <see cref="IFD.Exif"/> property which is different from current instance. =or= Value being set is already used as IFD at another position in this instance.</exception>
        ''' <seelaso cref="IFD0"/><seelaso cref="IFD.Following"/>
        Public Property ThumbnailIFD() As IfdMain
            Get
                If IFD0 IsNot Nothing Then Return IFD0.Following Else Return Nothing
            End Get
            Set(ByVal value As IfdMain)
                If IFD0 Is Nothing Then IFD0 = New IfdMain
                IFD0.Following = value
            End Set
        End Property
        ''' <summary>Gets all subIFDs and sub-subIFDs etc. present in this instance</summary>
        ''' <remarks>Collection of all subIFDs in this instance. This collection does not contain IFDs folowing (linked-list connected) to subIFDs, but contains any possible subIFDs linked from such subIFD-following IFD.</remarks>
        Public ReadOnly Property SubIFDs() As IEnumerable(Of SubIfd)
            Get
                Dim ret As IEnumerable(Of SubIfd) = New List(Of SubIfd)
                Dim Current As Ifd = IFD0
                Dim CurrentStack As New Stack(Of Ifd)
                While Current IsNot Nothing
                    ret = ret.Union(Current.SubIFDs)
                    If Current.SubIFDs.Count > 0 Then
                        If Current.Following IsNot Nothing Then CurrentStack.Push(Current.Following)
                        For Each si In Current.SubIFDs
                            CurrentStack.Push(si.Value)
                        Next
                        Current = CurrentStack.Pop
                    ElseIf Current.Following IsNot Nothing Then
                        Current = Current.Following
                    ElseIf CurrentStack.Count > 0 Then
                        Current = CurrentStack.Pop
                    Else
                        Current = Nothing
                    End If
                End While
                Return ret
            End Get
        End Property
        ''' <summary>Gets value indicationg if given <see cref="IFD"/> is used somewhere in this <see cref="Exif"/></summary>
        ''' <param name="IFD">Instance to look for</param>
        ''' <remarks>True if given instance is used somewhere in current instance</remarks>
        Protected Friend Function ContainsIFD(ByVal IFD As Ifd) As Boolean
            Dim Current As Ifd = Me.IFD0
            While Current IsNot Nothing
                If Current Is IFD Then Return True
                Current = Current.Following
            End While
            For Each SubIfd As SubIfd In SubIFDs
                Current = SubIfd
                While Current IsNot Nothing
                    If Current Is IFD Then Return True
                    Current = Current.Following
                End While
            Next SubIfd
            Return False
        End Function
#Region "Load and save"
        ''' <summary>Loads Exif data from stream.</summary>
        ''' <param name="Stream">Stream to load data from</param>
        ''' <returns>Exif data loaded from <paramref name="Stream"/></returns>
        ''' <exception cref="IO.InvalidDataException">
        ''' Invalid byte order mark (other than 'II' or 'MM') at the beginning of stream -or-
        ''' Byte order test (2 bytes next to byte order mark, 3rd and 4th bytes in stream) don't avaluates to value 2Ah
        ''' </exception>
        ''' <exception cref="System.ObjectDisposedException">The source stream is closed.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached unexpectedly.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Stream"/> is null.</exception>
        ''' <exception cref="InvalidOperationException"><paramref name="Stream"/> is not zero-lenght and does not support seeking and reeding.</exception>
        ''' <remarks>Data loaded by this method are not intended to be edited and saved (though it is possible via <see cref="ExifWriter"/>).</remarks>
        ''' <seelaso cref="LoadForUpdating"/>
        ''' <version version="1.5.2">Function introduced</version>
        Public Shared Function Load(ByVal Stream As IO.Stream) As Exif
            If Stream Is Nothing Then Throw New ArgumentNullException("Stream")
            Dim r As New ExifReader(Stream)
            Return New Exif(r)
        End Function
        ''' <summary>Loads Exif data from <see cref="IExifGetter"/>.</summary>
        ''' <param name="Source"><see cref="IExifGetter"/> to load data from.</param>
        ''' <returns>Exif data loaded from <paramref name="Source"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name=" Source "/> is null</exception>
        ''' <exception cref="IO.InvalidDataException">
        ''' Invalid byte order mark (other than 'II' or 'MM') at the beginning of stream -or-
        ''' Byte order test (2 bytes next to byte order mark, 3rd and 4th bytes in stream) don't avaluates to value 2Ah
        ''' </exception>
        ''' <exception cref="System.ObjectDisposedException">The source stream is closed.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached unexpectedly.</exception>
        ''' <exception cref="InvalidOperationException">Stream obtained from <paramref name="Source"/> is not zero-lenght and does not support seeking and reeding.</exception>
        ''' <remarks>Data loaded by this method are not intended to be edited and saved (though it is possible via <see cref="ExifWriter"/>).</remarks>
        ''' <seelaso cref="LoadForUpdating"/>
        ''' <version version="1.5.2">Function introduced</version>
        Public Shared Function Load(ByVal Source As IExifGetter) As Exif
            If Source Is Nothing Then Throw New ArgumentNullException("Source")
            Return Load(Source.GetExifStream)
        End Function
        ''' <summary>Loads Exif data from stream. Data loaded by this method can be updated and than saved back.</summary>
        ''' <param name="Stream">Stream to load data from</param>
        ''' <returns>Exif data loaded from <paramref name="Stream"/></returns>
        ''' <exception cref="IO.InvalidDataException">
        ''' Invalid byte order mark (other than 'II' or 'MM') at the beginning of stream -or-
        ''' Byte order test (2 bytes next to byte order mark, 3rd and 4th bytes in stream) don't avaluates to value 2Ah
        ''' </exception>
        ''' <exception cref="System.ObjectDisposedException">The source stream is closed.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached unexpectedly.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Stream"/> is null.</exception>
        ''' <exception cref="InvalidOperationException"><paramref name="Stream"/> is not zero-lenght and does not support seeking and reeding.</exception>
        ''' <seelaso cref="Update"/>
        ''' <version version="1.5.2">Function introduced</version>
        Public Shared Function LoadForUpdating(ByVal Stream As IO.Stream) As Exif
            If Stream Is Nothing Then Throw New ArgumentNullException("Stream")
            Dim Reader As New ExifReader(Stream)
            Dim ExifData As New Exif(Reader)
            Stream.Position = 0
            Dim Backup As New IO.MemoryStream(Stream.Length)
            Backup.Write(Stream)
            Backup.Flush()
            ExifData.OriginalData = Backup
            Return ExifData
        End Function
        ''' <summary>Keeps original data this instance was loaded from.</summary>
        ''' <remarks>Data re kept for saving purposes.</remarks>
        Private OriginalData As IO.MemoryStream
        ''' <summary>Loads Exif data from <see cref="IExifGetter"/>. Data loaded by this method can be updated and than saved back.</summary>
        ''' <param name="Source"><see cref="IExifGetter"/> providing stream to load data from</param>
        ''' <returns>Exif data loaded from <paramref name="Stream"/></returns>
        ''' <exception cref="IO.InvalidDataException">
        ''' Invalid byte order mark (other than 'II' or 'MM') at the beginning of stream -or-
        ''' Byte order test (2 bytes next to byte order mark, 3rd and 4th bytes in stream) don't avaluates to value 2Ah
        ''' </exception>
        ''' <exception cref="System.ObjectDisposedException">The source stream is closed.</exception>
        ''' <exception cref="System.IO.IOException">An I/O error occurs.</exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached unexpectedly.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Source"/> is null.</exception>
        ''' <seelaso cref="Update"/>
        ''' <exception cref="InvalidOperationException">Stream obtained from <paramref name="Source"/> is not zero-lenght and does not support seeking and reeding.</exception>
        ''' <version version="1.5.2">Function introduced</version>
        Public Shared Function LoadForUpdating(ByVal Source As IExifGetter) As Exif
            If Source Is Nothing Then Throw New ArgumentNullException("Source")
            Return LoadForUpdating(Source.GetExifStream)
        End Function
        ''' <summary>Saves Exif data to stream</summary>
        ''' <param name="Stream">Stream to save data to</param>
        ''' <remarks>This method simply replace any Exif data previously in stream. It does not preseve any unknown data such as Maker notes. Use <see cref="Update"/> to preserv unknown data.</remarks>
        ''' <seelaso cref="Update"/>
        ''' <exception cref="ArgumentNullException"><paramref name="Stream"/> is null</exception>
        ''' <exception cref="InvalidOperationException">The <see cref="ExifWriter.Save"/> method failed. See <see cref="InvalidOperationException.InnerException"/> for details.</exception>
        ''' <exception cref="ArgumentException"><paramref name="Stream"/> does not support reading, seeking and writing</exception>
        ''' <version version="1.5.2">Method introduced</version>
        Public Sub Save(ByVal Stream As IO.Stream)
            If Stream Is Nothing Then Throw New ArgumentNullException("Stream")
            Dim w As New ExifWriter(Stream)
            Try
                w.Save(Me)
            Catch ex As Exception When TypeOf ex Is ArgumentException OrElse TypeOf ex Is TypeMismatchException OrElse TypeOf ex Is ArgumentNullException OrElse TypeOf ex Is InvalidEnumArgumentException
                Throw New InvalidOperationException(ResourcesT.Exceptions.CannotSaveExifDataBecauseOfInvalidContent, ex)
            End Try
        End Sub
        ''' <summary>Saves Exif data to <see cref="IExifWriter"/></summary>
        ''' <param name="Target"><see cref="IExifWriter"/> providing stream to save data to</param>
        ''' <remarks>This method simply replace any Exif data previously in stream. It does not preseve any unknown data such as Maker notes. Use <see cref="Update"/> to preserv unknown data.</remarks>
        ''' <seelaso cref="Update"/>
        ''' <exception cref="ArgumentNullException"><paramref name="Target"/> is null</exception>
        ''' <exception cref="InvalidOperationException">The <see cref="ExifWriter.Save"/> method failed. See <see cref="InvalidOperationException.InnerException"/> for details.</exception>
        ''' <exception cref="ArgumentException">Stream obtained from <paramref name="Target"/> does not support reading, seeking and writing</exception>
        ''' <version version="1.5.2">Method introduced</version>
        Public Sub Save(ByVal Target As IExifWriter)
            If Target Is Nothing Then Throw New ArgumentException("Target")
            Dim Stream As System.IO.Stream = Target.GetWritableExifStream
            If Stream IsNot Nothing Then
                Try
                    Save(Stream)
                Catch ex As Exception When TypeOf ex Is ArgumentException OrElse TypeOf ex Is TypeMismatchException OrElse TypeOf ex Is ArgumentNullException OrElse TypeOf ex Is InvalidEnumArgumentException
                    Throw New InvalidOperationException(ResourcesT.Exceptions.CannotSaveExifDataBecauseOfInvalidContent, ex)
                End Try
            Else
                Using ms As New IO.MemoryStream
                    Try
                        Save(ms)
                    Catch ex As Exception When TypeOf ex Is ArgumentException OrElse TypeOf ex Is TypeMismatchException OrElse TypeOf ex Is ArgumentNullException OrElse TypeOf ex Is InvalidEnumArgumentException
                        Throw New InvalidOperationException(ResourcesT.Exceptions.CannotSaveExifDataBecauseOfInvalidContent, ex)
                    End Try
                    ms.Flush()
                    Dim Buff = ms.GetBuffer
                    If Buff.Length <> ms.Length Then
                        Dim Buff2(ms.Length - 1) As Byte
                        Array.ConstrainedCopy(Buff, 0, Buff2, 0, ms.Length)
                        Buff = Buff2
                    End If
                    Target.ExifEmbded(Buff)
                End Using
            End If
        End Sub
        ''' <summary>Updates Exif data in stream with new values preserving all unknown data.</summary>
        ''' <param name="Stream">Stream to updata data in</param>
        ''' <remarks>This method can only update data in stream with same content as stream this instance was created from using <see cref="LoadForUpdating"/>.
        ''' <para>When this method finishes succesfully, this instance can be used to update <paramref name="Stream"/> again, but it can no longer be used to update original stream it was created from.</para>
        ''' <para>Although this method preserves any data in Exif stream that this implementation of Exif does not undertand it is still possible to break the data. When you change value of record that is pointer to such unknown data. So, do not edit values of records you do not know what are the records good for.</para></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Stream"/> is null.</exception>
        ''' <exception cref="InvalidOperationException">This instance was not created using <see cref="LoadForUpdating"/>. 
        ''' -or- Length of stream this instance was created from and length of <paramref name="Stream"/> differs.
        ''' -or- The <see cref="ExifWriter.Save"/> method failed. Seee <see cref="InvalidOperationException.InnerException"/> for details.</exception>
        ''' <exception cref="System.Data.DBConcurrencyException">Content of stream this instance was created from differs from content of <paramref name="Stream"/>.</exception>
        ''' <version version="1.5.2">Method introduced</version>
        Public Sub Update(ByVal Stream As IO.Stream)
            If Stream Is Nothing Then Throw New ArgumentNullException("Stream")
            If OriginalData Is Nothing Then Throw New InvalidOperationException(ResourcesT.Exceptions.ThisInstanceWasNotCreatedForUpdating)
            If OriginalData.Length <> Stream.Length Then Throw New InvalidOperationException(ResourcesT.Exceptions.StreamCannotBeUpdatedBycauseItHasDifferentLengthFrom)
            Stream.Position = 0 : OriginalData.Position = 0
            For i As Integer = 0 To OriginalData.Length - 1
                If Stream.ReadByte <> OriginalData.ReadByte Then Throw New System.Data.DBConcurrencyException(ResourcesT.Exceptions.StreamCannotBeUpdatedBecauseItsContentDiffersFromContent)
            Next
            Dim OriginalReaderSettings As New ExifReaderSettings
            Dim OriginalMapGenerator As New ExifMapGenerator(OriginalReaderSettings)
            Dim OriginalReader As New ExifReader(OriginalData, OriginalReaderSettings)
            Dim OriginalMap = OriginalMapGenerator.Map
            Stream.Position = 0
            Dim Writer As New ExifWriter(Stream, OriginalMap)
            Writer.PreserveThumbnail = Me.ThumbnailIFD IsNot Nothing AndAlso Me.ThumbnailIFD.HasThumbnail
            Try
                Writer.Save(Me)
            Catch ex As Exception When TypeOf ex Is ArgumentException OrElse TypeOf ex Is TypeMismatchException OrElse TypeOf ex Is ArgumentNullException OrElse TypeOf ex Is InvalidEnumArgumentException
                Throw New InvalidOperationException(ResourcesT.Exceptions.CannotSaveExifDataBecauseOfInvalidContent, ex)
            End Try
            Stream.Position = 0
            OriginalData.SetLength(Stream.Length)
            OriginalData.Position = 0
            OriginalData.Write(Stream)
        End Sub
        ''' <summary>Updates Exif data in <see cref="IExifWriter"/> with new values preserving all unknown data.</summary>
        ''' <param name="Target">Stream to updata data in</param>
        ''' <remarks>This method can only update data in stream with same content as stream this instance was created from using <see cref="LoadForUpdating"/>.
        ''' <para>When this method finishes succesfully, this instance can be used to update <paramref name="Target"/> again, but it can no longer be used to update original stream it was created from.</para>
        ''' <para>Although this method preserves any data in Exif stream that this implementation of Exif does not undertand it is still possible to break the data. When you change value of record that is pointer to such unknown data. So, do not edit values of records you do not know what are the records good for.</para></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Target"/> is null.</exception>
        ''' <exception cref="InvalidOperationException">This instance was not created using <see cref="LoadForUpdating"/>. 
        ''' -or- Length of stream this instance was created from and length of stream obtained from <paramref name="Target"/> differs.
        ''' -or- The <see cref="ExifWriter.Save"/> method failed. Seee <see cref="InvalidOperationException.InnerException"/> for details.
        ''' -or- Stream obtained from <paramref name="Target"/> is null or zero-lenght.</exception>
        ''' <exception cref="Data.DBConcurrencyException">Content of stream this instance was created from differs from content of <paramref name="Stream"/>.</exception>
        ''' <version version="1.5.2">Method introduced</version>
        Public Sub Update(ByVal Target As IExifWriter)
            If Target Is Nothing Then Throw New ArgumentException("Target")
            Dim Stream = Target.GetWritableExifStream
            If Stream Is Nothing OrElse Stream.Length = 0 Then Throw New InvalidOperationException(ResourcesT.Exceptions.TargetCannotBeUpdatedBecauseItContainsNoExifData)
            Update(Stream)
        End Sub
#End Region
        ''' <summary>Raises the <see cref="OnChanged"/> event; caled when <see cref="IFD0"/>.<see cref="IfdMain.Changed">Changed</see> occurs.</summary>
        ''' <param name="e">Event parameters (passed from <see cref="IFD0"/>.<see cref="IfdMain.Changed">Changed</see>)</param>
        ''' <remarks><note type="inheritnifo">Always call base class method in order event to be raised.</note></remarks>
        ''' <version version="1.5.2">Method added</version>
        Protected Overridable Sub OnChanged(ByVal e As EventArgs)
            RaiseEvent Changed(Me, e)
        End Sub
        ''' <summary>Raised when value of member changes</summary>
        ''' <remarks><paramref name="e"/>Should contain additional information that can be used in event-handling code (e.g. use <see cref="IReportsChange.ValueChangedEventArgs(Of T)"/> class)</remarks>
        ''' <version version="1.5.2">Event added</version>
        Public Event Changed(ByVal sender As IReportsChange, ByVal e As System.EventArgs) Implements IReportsChange.Changed

        Private Sub IFD0_Changed(ByVal sender As IReportsChange, ByVal e As System.EventArgs) Handles _IFD0.Changed
            OnChanged(e)
        End Sub

        ''' <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
        ''' <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
        ''' <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization. Ignored in this implementation. </param>
        ''' <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        ''' <exception cref="ArgumentNullException"><paramref name="info"/> is nul</exception>
        ''' <exception cref="InvalidOperationException">The <see cref="ExifWriter.Save"/> method failed. See <see cref="InvalidOperationException.InnerException"/> for details.</exception>
        ''' <version version="1.5.2">Method added</version>
        Protected Overridable Sub GetObjectData(ByVal info As System.Runtime.Serialization.SerializationInfo, ByVal context As System.Runtime.Serialization.StreamingContext) Implements System.Runtime.Serialization.ISerializable.GetObjectData
            If info Is Nothing Then Throw New ArgumentNullException("info")
            Using ms As New IO.MemoryStream
                Me.Save(ms)
                ms.Flush()
                Dim data As Byte() = ms.GetBuffer
                If ms.Length <> data.Length Then
                    Dim data2(ms.Length - 1) As Byte
                    Array.ConstrainedCopy(data, 0, data2, 0, data2.Length)
                    data = data2
                End If
                info.AddValue("data", data)
            End Using
        End Sub
        ''' <summary>Deserialization CTor</summary>
        ''' <param name="information"><see cref="Runtime.Serialization.SerializationInfo"/> to read data from</param>
        ''' <param name="context">Serialization context. Ignored.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="information"/> is null -or- Value of "data" in <paramref name="information"/> is null.</exception>
        ''' <exception cref="Runtime.Serialization.SerializationException"><paramref name="information"/> does not contain value named "data".</exception>
        ''' <exception cref="InvalidCastException"><paramref name="information"/> contain value named "data", but it cannot be converted to <see cref="Byte"/>[].</exception>
        ''' <exception cref="IO.InvalidDataException">
        ''' Invalid byte order mark (other than 'II' or 'MM') at the beginning of stream -or-
        ''' Byte order test (2 bytes next to byte order mark, 3rd and 4th bytes in stream) don't avaluates to value 2Ah
        ''' </exception>
        ''' <exception cref="System.IO.EndOfStreamException">The end of the stream is reached unexpectedly.</exception>
        ''' <version version="1.5.2">CTor added</version>
        Protected Sub New(ByVal information As Runtime.Serialization.SerializationInfo, ByVal context As Runtime.Serialization.StreamingContext)
            Me.New(New ExifReader(New IO.MemoryStream(DirectCast(information.ThrowIfNull("information").GetValue("data", GetType(Byte())), Byte()))))
        End Sub
#Region "IMetadata"
        'Proposed format: IFDNumber:Tag (ie. 0:124, 1:124); E:Tag (Exif sub-IFD); G:Tag (GPS), I:Tag (Interop)
        'TODO: Implement, version
        ''' <summary>Stores either non-negative (any, even undefined in this enumeration) value for IFD number or negative value for known sub-IFD</summary>
        Private Enum IFDIndexes
            ''' <summary>Exif sub-IFD</summary>
            Exif = -1
            ''' <summary>GPS sub-IFD</summary>
            GPS = -2
            ''' <summary>Interoperability sub-IFD</summary>
            Interop = -3
            ''' <summary>IFD 0 (main IFD)</summary>
            IFD0 = 0
            ''' <summary>Thumbnail IFD (IFD 1)</summary>
            ThumbnailIFD = 1
        End Enum
        ''' <summary>Interprets Exif metadata key in format "X:RecordNumber" as IFD index and record number</summary>
        ''' <param name="Key">Key to interpret</param>
        ''' <param name="IFD">When method returns contains index of IFD</param>
        ''' <param name="RecordNumber">When method returns contains record number</param>
        ''' <param name="AllowPredefined">True to allow predefined names to be utilized; false to allow keys in specified format only.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Key"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> does not represent predefined Exif property name (when <paramref name="AllowPredefined"/> is true) and: <paramref name="Key"/> does not contain exactly 2 :-separated parts or 1st part of <paramref name="Key"/> is neither E, G, I or integer number. or Record number part or <paramref name="Key"/> is not valid <see cref="UShort"/> number.</exception>
        ''' <version version="1.5.2">Method added</version>
        Private Sub InterpretKey(ByVal Key As String, <Out()> ByRef IFD As IFDIndexes, <Out()> ByRef RecordNumber As UShort, Optional ByVal AllowPredefined As Boolean = True)
            If Key Is Nothing Then Throw New ArgumentNullException("Key")
            If AllowPredefined Then
                'TODO: Return when matches predefined name
            End If
            Dim Parts = Key.Split(":"c)
            If Parts.Length <> 2 Then Throw New ArgumentException(ResourcesT.Exceptions.ExifMEtadataKeyMustContain2SeparatedParts, "Key")
            Select Case Parts(0)
                Case "E"c : IFD = IFDIndexes.Exif
                Case "G"c : IFD = IFDIndexes.GPS
                Case "I"c : IFD = IFDIndexes.Interop
                Case Else
                    Dim value%
                    If Not Integer.TryParse(Parts(0), Globalization.NumberStyles.Integer, Globalization.CultureInfo.InvariantCulture, value) Then
                        Throw New ArgumentException(ResourcesT.Exceptions.IFDIdentificationPartOfExifMetadataKeyIsInvalidItMust)
                    End If
                    IFD = value
            End Select
            Try
                RecordNumber = UShort.Parse(Parts(1), Globalization.CultureInfo.InvariantCulture)
            Catch ex As Exception When TypeOf ex Is FormatException OrElse TypeOf ex Is OverflowException
                Throw New ArgumentException(ResourcesT.Exceptions.RecordNumberPartOfExifMetadataIdentificationIsInvalid, ex)
            End Try
        End Sub
        ''' <summary>Gets IFD identified by <see cref="IFDIndexes"/></summary>
        ''' <param name="Index">Index identifiying the IFD</param>
        ''' <returns>IFD identified by index, if exists; null otherwise</returns>
        ''' <version version="1.5.2">Property added</version>
        Private ReadOnly Property IFD(ByVal Index As IFDIndexes) As Ifd
            Get
                Select Case Index
                    Case IFDIndexes.Exif : Return If(IFD0 IsNot Nothing, IFD0.ExifSubIFD, Nothing)
                    Case IFDIndexes.GPS : Return If(IFD0 IsNot Nothing, IFD0.GPSSubIFD, Nothing)
                    Case IFDIndexes.Interop : Return If(IFD0 IsNot Nothing AndAlso IFD0.ExifSubIFD IsNot Nothing, IFD0.ExifSubIFD.InteropSubIFD, Nothing)
                    Case Is >= 0
                        Dim i As Integer = 0
                        Dim MainIFD As Ifd = IFD0
                        Do
                            If i = Index Then Return MainIFD
                            i += 1
                            If MainIFD IsNot Nothing Then MainIFD = MainIFD.Following
                        Loop While MainIFD IsNot Nothing
                End Select
                Return Nothing
            End Get
        End Property
        ''' <summary>Gets value indicating wheather metadata value with given key is present in current instance</summary>
        ''' <param name="Key">Key (or name) to check presence of (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <returns>True when value for given key is present; false otherwise</returns>
        ''' <remarks>The <paramref name="Key"/> parameter can be either key in metadata-specific format or predefined name of metadata item (if predefined names are supported).</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Key"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> does not represent predefined Exif property name and: <paramref name="Key"/> does not contain exactly 2 :-separated parts or 1st part of <paramref name="Key"/> is neither E, G, I or integer number. or Record number part or <paramref name="Key"/> is not valid <see cref="UShort"/> number.</exception>
        ''' <version version="1.5.2">Function added</version>
        Public Function ContainsKey(ByVal Key As String) As Boolean Implements IMetadata.ContainsKey
            Dim IFD As IFDIndexes, RecordNumber As UShort
            InterpretKey(Key, IFD, RecordNumber)
            If Me.IFD(IFD) IsNot Nothing Then Return Me.IFD(IFD).Records.ContainsKey(RecordNumber)
        End Function

        ''' <summary>Gets keys of all the metadata present in curent instance</summary>
        ''' <returns>Keys in metadata-specific format of all the metadata present in curent instance. Never returns null; may return anempt enumeration.</returns>
        ''' <version version="1.5.2">Function added</version>
        Public Function GetContainedKeys() As System.Collections.Generic.IEnumerable(Of String) Implements IMetadata.GetContainedKeys
            Dim ret As New List(Of String)
            Dim IFD As Ifd = IFD0
            Dim i% = 0
            While IFD IsNot Nothing
                ret.AddRange(From tag In IFD.GetRecordKeys Select String.Format(Globalization.CultureInfo.InvariantCulture, "{0}:{1}", i, tag))
                IFD = IFD.Following
                i += 1
            End While
            If IFD0 IsNot Nothing AndAlso IFD0.ExifSubIFD IsNot Nothing Then
                ret.AddRange(From tag In IFD0.ExifSubIFD.GetRecordKeys Select String.Format(Globalization.CultureInfo.InvariantCulture, "E:{0}", tag))
                If IFD0.ExifSubIFD.InteropSubIFD IsNot Nothing Then
                    ret.AddRange(From tag In IFD0.ExifSubIFD.InteropSubIFD.GetRecordKeys Select String.Format(Globalization.CultureInfo.InvariantCulture, "I:{0}", tag))
                End If
            End If
            If IFD0 IsNot Nothing AndAlso IFD0.GPSSubIFD IsNot Nothing Then
                ret.AddRange(From tag In IFD0.GPSSubIFD.GetRecordKeys Select String.Format(Globalization.CultureInfo.InvariantCulture, "G:{0}", tag))
            End If
            Return ret
        End Function

        ''' <summary>Gets localized description for given key (or name)</summary>
        ''' <param name="Key">Key (or name) to get description of</param>
        ''' <returns>Localized description of purpose of metadata item identified by <paramref name="Key"/>; nul when description is not available.</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> is in invalid format or it is not one of predefined names.</exception>
        ''' <version version="1.5.2"></version>
        Public Function GetDescription(ByVal Key As String) As String Implements IMetadata.GetDescription
            Dim prp = GetProperty(Key)
            If prp Is Nothing Then Return Nothing
            Dim attr = prp.GetAttribute(Of DescriptionAttribute)()
            If attr Is Nothing Then Return Nothing
            Return attr.Description
        End Function

        ''' <summary>Gets localized human-readable name for given key (or name)</summary>
        ''' <param name="Key">Key (or name) to get name for</param>
        ''' <returns>Human-readable descriptive name of metadata item identified by <paramref name="Key"/>; null when no such name is defined/known.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Key"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> does not represent predefined Exif property name and: <paramref name="Key"/> does not contain exactly 2 :-separated parts or 1st part of <paramref name="Key"/> is neither E, G, I or integer number. or Record number part or <paramref name="Key"/> is not valid <see cref="UShort"/> number.</exception>
        ''' <version version="1.5.2">Function added</version>
        Public Function GetHumanName(ByVal Key As String) As String Implements IMetadata.GetHumanName
            'TODO: DisplayName attributes
            Dim prp = GetProperty(Key)
            If prp Is Nothing Then Return Nothing
            Dim attr = prp.GetAttribute(Of DisplayNameAttribute)()
            If attr Is Nothing Then Return Nothing
            Return attr.DisplayName
        End Function
        ''' <summary>Gets IFD property from tag key</summary>
        ''' <param name="Key">Key to get property for</param>
        ''' <returns>Property of <see cref="ExifT.Ifd"/>-derived class representing metadata item with key <paramref name="Key"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Key"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> does not represent predefined Exif property name and: <paramref name="Key"/> does not contain exactly 2 :-separated parts or 1st part of <paramref name="Key"/> is neither E, G, I or integer number. or Record number part or <paramref name="Key"/> is not valid <see cref="UShort"/> number.</exception>
        ''' <version version="1.5.2">Function added</version>
        Private Function GetProperty(ByVal Key As String) As Reflection.PropertyInfo
            Dim IFD As IFDIndexes, RecordNumber As UShort
            InterpretKey(Key, IFD, RecordNumber)
            Dim IfdType As Type
            Select Case IFD
                Case IFDIndexes.Exif : IfdType = GetType(IfdExif)
                Case IFDIndexes.GPS : IfdType = GetType(IfdGps)
                Case IFDIndexes.Interop : IfdType = GetType(IfdInterop)
                Case IFDIndexes.IFD0, IFDIndexes.ThumbnailIFD : IfdType = GetType(IfdMain)
                Case Else : Return Nothing
            End Select
            Dim enmtype = IfdType.GetNestedType("Tags")
            Dim Constant = TypeTools.GetConstant(TypeTools.GetEnumValue(enmtype, RecordNumber))
            If Constant Is Nothing Then Return Nothing
            Return IfdType.GetProperty(Constant.Name)
        End Function

        ''' <summary>Gets key for predefined name</summary>
        ''' <param name="Name">Name to get key for</param>
        ''' <returns>Key in metadata-specific format for given predefined metadata item name</returns>
        ''' <exception cref="ArgumentException"><paramref name="Name"/> is not one of predefined names retuened by <see cref="GetPredefinedNames"/>.</exception>
        ''' <version version="1.5.2">Function added</version>
        Public Function GetKeyOfName(ByVal Name As String) As String Implements IMetadata.GetKeyOfName
            If [Enum].GetNames(GetType(IfdMain.Tags)).Contains(Name) Then Return "0:" & DirectCast(TypeTools.GetConstant(Name, GetType(IfdMain.Tags)).GetValue(Nothing), IFormattable).ToString("d", Globalization.CultureInfo.InvariantCulture)
            If [Enum].GetNames(GetType(IfdExif.Tags)).Contains(Name) Then Return "E:" & DirectCast(TypeTools.GetConstant(Name, GetType(IfdExif.Tags)).GetValue(Nothing), IFormattable).ToString("d", Globalization.CultureInfo.InvariantCulture)
            If [Enum].GetNames(GetType(IfdGps.Tags)).Contains(Name) Then Return "G:" & DirectCast(TypeTools.GetConstant(Name, GetType(IfdGps.Tags)).GetValue(Nothing), IFormattable).ToString("d", Globalization.CultureInfo.InvariantCulture)
            If [Enum].GetNames(GetType(IfdInterop.Tags)).Contains(Name) Then Return "I:" & DirectCast(TypeTools.GetConstant(Name, GetType(IfdInterop.Tags)).GetValue(Nothing), IFormattable).ToString("d", Globalization.CultureInfo.InvariantCulture)
            Throw New ArgumentException(ResourcesT.Exceptions.UnknownPredefinedName0.f(Name))
        End Function

        ''' <summary>Gets name for key</summary>
        ''' <param name="Key">Key to get name for</param>
        ''' <returns>One of predefined names to use instead of <paramref name="Key"/>; null when given key has no corresponding name.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Key"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> does not contain exactly 2 :-separated parts or 1st part of <paramref name="Key"/> is neither E, G, I or integer number. or Record number part or <paramref name="Key"/> is not valid <see cref="UShort"/> number.</exception>
        ''' <version version="1.5.2">Function added</version>
        Public Function GetNameOfKey(ByVal Key As String) As String Implements IMetadata.GetNameOfKey
            Dim IFD As IFDIndexes, RecordNumber As UShort
            InterpretKey(Key, IFD, RecordNumber, False)
            Dim enm As Type
            Select Case IFD
                Case IFDIndexes.Exif : enm = GetType(IfdExif.Tags)
                Case IFDIndexes.GPS : enm = GetType(IfdGps.Tags)
                Case IFDIndexes.Interop : enm = GetType(IfdInterop.Tags)
                Case IFDIndexes.IFD0 : enm = GetType(IfdMain.Tags)
                Case Else : Return Nothing
            End Select
            Dim cns = TypeTools.GetConstant(TypeTools.GetEnumValue(enm, RecordNumber))
            If cns Is Nothing Then Return Nothing
            If GetKeyOfName(cns.Name) = Key Then Return cns.Name Else Return Nothing
        End Function

        ''' <summary>Gets all keys predefined for curent metadata format</summary>
        ''' <returns>Eumeration containing all predefined (well-known) keys of metadata for this metadata format. Returns always the same enumeration event when values for some keys are not present. Never returns null; may return an empty enumeration.</returns>
        ''' <version version="1.5.2">Function added</version>
        Public Function GetPredefinedKeys() As System.Collections.Generic.IEnumerable(Of String) Implements IMetadata.GetPredefinedKeys
            Dim ret As New List(Of String)
            ret.AddRange(From val As IfdMain.Tags In [Enum].GetValues(GetType(IfdMain.Tags)) Select String.Format(Globalization.CultureInfo.InvariantCulture, "0:{0:d}", val))
            ret.AddRange(From val As IfdExif.Tags In [Enum].GetValues(GetType(IfdExif.Tags)) Select String.Format(Globalization.CultureInfo.InvariantCulture, "E:{0:d}", val))
            ret.AddRange(From val As IfdGps.Tags In [Enum].GetValues(GetType(IfdGps.Tags)) Select String.Format(Globalization.CultureInfo.InvariantCulture, "G:{0:d}", val))
            ret.AddRange(From val As IfdInterop.Tags In [Enum].GetValues(GetType(IfdInterop.Tags)) Select String.Format(Globalization.CultureInfo.InvariantCulture, "I:{0:d}", val))
            Return ret
        End Function

        ''' <summary>Gets all predefined names for metadata keys</summary>
        ''' <returns>Enumeration containing all predefined names of metadata items for this metadada format. Never returns null; may return an empty enumeration.</returns>
        ''' <remarks>Names retuned are actually names of items from enumerations <see cref="IfdMain.Tags"/>, <see cref="IfdExif.Tags"/>, <see cref="IfdGps.Tags"/> and <see cref="IfdInterop.Tags"/>. There are no names predefined for IFD1 and following.</remarks>
        ''' <version version="1.5.2">Function added</version>
        Public Function GetPredefinedNames() As System.Collections.Generic.IEnumerable(Of String) Implements IMetadata.GetPredefinedNames
            Dim ret As New List(Of String)
            ret.AddRange([Enum].GetNames(GetType(IfdMain.Tags)))
            'TODO: Debug if there are duplicities
            For Each Name As String In [Enum].GetNames(GetType(IfdExif.Tags))
                If Not ret.Contains(Name) Then ret.Add(Name)
            Next
            For Each Name As String In [Enum].GetNames(GetType(IfdExif.Tags))
                If Not ret.Contains(Name) Then ret.Add(Name)
            Next
            For Each Name As String In [Enum].GetNames(GetType(IfdGps.Tags))
                If Not ret.Contains(Name) Then ret.Add(Name)
            Next
            For Each Name As String In [Enum].GetNames(GetType(IfdInterop.Tags))
                If Not ret.Contains(Name) Then ret.Add(Name)
            Next
            Return ret
        End Function

        ''' <summary>Gets name of metadata format represented by implementation</summary>
        ''' <returns><see cref="ExifName"/></returns>
        ''' <remarks>All <see cref="IMetadataProvider">IMetadataProviders</see> returning Exif format should identify the format by this name.</remarks>
        ''' <version version="1.5.2">Property added</version>
        Private ReadOnly Property Name() As String Implements IMetadata.Name
            Get
                Return ExifName
            End Get
        End Property

        ''' <summary>Gets medata value with given key</summary>
        ''' <param name="Key">Key (or name) to get vaue for (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <returns>Value of metadata item with given key; or null if given metadata value is not supported</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> has invalid format and it is not one of predefined names</exception>
        ''' <remarks>The <paramref name="Key"/> peremeter can be either key in metadata-specific format or predefined name of metadata item (if predefined names are supported).</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Key"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> does not represent predefined Exif property name and: <paramref name="Key"/> does not contain exactly 2 :-separated parts or 1st part of <paramref name="Key"/> is neither E, G, I or integer number. or Record number part or <paramref name="Key"/> is not valid <see cref="UShort"/> number.</exception>
        ''' <version version="1.5.2">Property added</version>
        Public ReadOnly Property Value(ByVal Key As String) As Object Implements IMetadata.Value
            Get
                Dim IFD As IFDIndexes, RecordNumber As UShort
                InterpretKey(Key, IFD, RecordNumber)
                Dim IFDObject = Me.IFD(IFD)
                If IFDObject Is Nothing Then Return Nothing
                If Not IFDObject.Records.ContainsKey(RecordNumber) Then Return Nothing
                Return IFDObject.Records(RecordNumber).Data
            End Get
        End Property
        ''' <summary>Gets metadata value with given key as string</summary>
        ''' <param name="Key">Key (or name) to get vaue for (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <returns>Value of metadata item with given key as string; or null if given metadata value is not supported</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> has invalid format and it is not one of predefined names</exception>
        ''' <remarks>The <paramref name="Key"/> peremeter can be either key in metadata-specific format or predefined name of metadata item (if predefined names are supported).</remarks>
        ''' <version version="1.5.2">Method added</version>
        Public Function GetStringValue(ByVal Key As String) As String Implements IMetadata.GetStringValue
            Dim ret = Value(Key)
            If ret Is Nothing Then Return Nothing
            If TypeOf ret Is IEnumerable(Of Byte) Then
                Dim r2 As New System.Text.StringBuilder
                For Each b In DirectCast(ret, IEnumerable(Of Byte))
                    r2.Append(b.ToString("x2"))
                Next
                Return r2.ToString
            ElseIf TypeOf ret Is IEnumerable AndAlso Not TypeOf ret Is String Then
                Dim r2 As New System.Text.StringBuilder
                For Each item In DirectCast(ret, IEnumerable)
                    If r2.Length <> 0 Then r2.Append(Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator & " ")
                    r2.Append(item.ToString)
                Next
                Return r2.ToString
            End If
            Return ret.ToString
        End Function
#End Region

        
    End Class

    ''' <summary>Describes one Exif record</summary>
    ''' <remarks>Descibes which data type record actually contains, how many items of such datatype. For recognized tags also possible format is specified via <see cref="ExifTagFormat"/></remarks>
    <CLSCompliant(False)> _
    Public Class ExifRecordDescription
        ''' <summary>Contains value of the <see cref="DataType"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _DataType As ExifDataTypes
        ''' <summary>Contains value of the <see cref="NumberOfElements"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _NumberOfElements As UShort
        ''' <summary>Number of elements of type <see cref="DataType"/> contained in record</summary>
        ''' <remarks>Note for inheritors: Do not expose setter of this property. Do not change value of this property during live-time of instance. This restriction is here because <see cref="ExifRecord"/> cannot track changes of this property.</remarks>
        Public Property NumberOfElements() As UShort
            Get
                Return _NumberOfElements
            End Get
            Protected Friend Set(ByVal value As UShort)
                _NumberOfElements = value
            End Set
        End Property
        ''' <summary>Data type of items in record</summary>
        ''' <remarks>Note for inheritors: Do not expose setter of this property. Do not change value of this property during live-time of instance. This restriction is here because <see cref="ExifRecord"/> cannot track changes of this property.</remarks>
        Public Property DataType() As ExifDataTypes
            Get
                Return _DataType
            End Get
            Protected Set(ByVal value As ExifDataTypes)
                _DataType = value
            End Set
        End Property
        ''' <summary>CTor</summary>
        ''' <param name="DataType">Data type of record</param>
        ''' <param name="NumberOfElements">Number of elements of type <paramref name="DataType"/> in record.</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="NumberOfElements"/> is 0</exception>
        Public Sub New(ByVal DataType As ExifDataTypes, ByVal NumberOfElements As UShort)
            If NumberOfElements = 0 Then Throw New ArgumentOutOfRangeException("NumberOfElements", ResourcesT.Exceptions.NumberOfElementsCannotBe0)
            Me.DataType = DataType
            Me.NumberOfElements = NumberOfElements
        End Sub
        ''' <summary>Protected CTor that allows <see cref="NumberOfElements"/> to be zero</summary>
        ''' <param name="NumberOfElements">Number of elements of type <paramref name="DataType"/> in record.</param>
        ''' <param name="DataType"></param>
        Protected Sub New(ByVal NumberOfElements As UShort, ByVal DataType As ExifDataTypes)
            Me.DataType = DataType
            Me.NumberOfElements = NumberOfElements
        End Sub
    End Class

    ''' <summary>Describes which data can be stored in recognized Exif tag</summary>
    ''' <remarks>Describas which datatype(s) and lengt if allowed for specific recognized Exif record. Actual content of record is described by <see cref="ExifRecordDescription"/></remarks>
    <CLSCompliant(False)> _
    Public Class ExifTagFormat : Inherits ExifRecordDescription
        ''' <summary>CTor</summary>
        ''' <param name="NumberOfElements">Number of elemets that must exactly be in tag. If number of elements can varry pass 0 here</param>
        ''' <param name="Tag">Number of tag</param>
        ''' <param name="Name">Short name of tag</param>
        ''' <param name="DataTypes">Possible datatypes of tag. First datatype specified must be the widest and must be always specified and will be used as default</param>
        ''' <exception cref="ArgumentNullException"><paramref name="DataTypes"/> is null or contains no element</exception>
        Public Sub New(ByVal NumberOfElements As UShort, ByVal Tag As UShort, ByVal Name As String, ByVal ParamArray DataTypes As ExifDataTypes())
            MyBase.New(NumberOfElements, TestThrowReturn(DataTypes)(0))
            _Tag = Tag
            _Name = Name
            OtherDatatypes.AddRange(DataTypes)
            OtherDatatypes.RemoveAt(0)
        End Sub
        ''' <summary>Test if <paramref name="DataTypes"/> is null or containc no element</summary>
        ''' <param name="DataTypes">Array to test</param>
        ''' <returns><paramref name="DataTypes"/></returns>
        ''' <remarks>Used by ctor</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="DataTypes"/> is null or contains no element</exception>
        Private Shared Function TestThrowReturn(ByVal DataTypes As ExifDataTypes()) As ExifDataTypes()
            If DataTypes Is Nothing OrElse DataTypes.Length = 0 Then Throw New ArgumentNullException("DataTypes", ResourcesT.Exceptions.DataTypesCannotBeNullAndMustContainAtLeastOneElement)
            Return DataTypes
        End Function
        ''' <summary>Contains value of the <see cref="Tag"/> property</summary>
        Private _Tag As UShort
        ''' <summary>Contains value of the <see cref="Name"/> property</summary>
        Private _Name As String
        ''' <summary>Contains list of possible datatypes for tag excepting datatype specified in <see cref="DataType"/></summary>
        Private OtherDatatypes As New List(Of ExifDataTypes)
        ''' <summary>Represents short unique name of tag used to reference it</summary>
        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property
        ''' <summary>Represents tag code in Exif</summary>
        Public ReadOnly Property Tag() As UShort
            Get
                Return _Tag
            End Get
        End Property
        ''' <summary>Datatypes allowed for this tag</summary>
        ''' <returns>Array of datatypes allowed for this tag. First element of the array is same as <see cref="DataType"/> amd represents default and preffered datatype</returns>
        Public ReadOnly Property DataTypes() As ExifDataTypes()
            Get
                Dim arr(OtherDatatypes.Count) As ExifDataTypes
                If OtherDatatypes.Count > 0 Then _
                    OtherDatatypes.CopyTo(0, arr, 1, OtherDatatypes.Count)
                arr(0) = DataType
                Return arr
            End Get
        End Property
    End Class

    ''' <summary>Represents one Exif record</summary>
    Public Class ExifRecord
        Implements IReportsChange
        ''' <summary>Contains value of the <see cref="Data"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _Data As Object
        ''' <summary>Contains value of the <see cref="DataType"/> property</summary>
        Private _DataType As ExifRecordDescription
        ''' <summary>Contains value of the <see cref="Fixed"/> property</summary>
        Private _Fixed As Boolean
        ''' <summary>True if <see cref="ExifRecordDescription.NumberOfElements"/> of this record is fixed</summary>
        Public ReadOnly Property Fixed() As Boolean
            Get
                Return Fixed
            End Get
        End Property
        ''' <summary>Datatype and number of items of record</summary>
        <CLSCompliant(False)> _
        Public ReadOnly Property DataType() As ExifRecordDescription
            Get
                Return _DataType
            End Get
        End Property
        ''' <summary>Value of record</summary>
        ''' <remarks>Actual type depends on <see cref="DataType"/></remarks>
        ''' <exception cref="InvalidCastException">Setting value of incompatible type</exception>
        ''' <exception cref="ArgumentException">Attempt to assigne value with other number of components when <see cref="Fixed"/> set to true</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
        Public Property Data() As Object
            Get
                Return _Data
            End Get
            Protected Set(ByVal value As Object)
                If value Is Nothing Then Throw New ArgumentNullException("value")
                Select Case DataType.DataType
                    Case ExifDataTypes.ASCII
                        If TypeOf value Is Char Then value = CStr(CChar(value))
                        If TryCast(value, String) IsNot Nothing Then
                            Dim newV As String = System.Text.Encoding.Default.GetString(System.Text.Encoding.Default.GetBytes(CStr(value)))
                            If Me.DataType.NumberOfElements = newV.Length OrElse Not Fixed Then
                                _Data = newV
                                Me.DataType.NumberOfElements = CStr(_Data).Length
                            Else
                                Throw New ArgumentException(ResourcesT.Exceptions.CannotChangeNumberOfComponentsOfThisRecord)
                            End If
                        Else
                            Throw New InvalidCastException(ResourcesT.Exceptions.ValueOfIncompatibleTypePassedToASCIIRecord)
                        End If
                    Case ExifDataTypes.Byte
                        SetDataValue(Of Byte)(value)
                    Case ExifDataTypes.Double
                        SetDataValue(Of Double)(value)
                    Case ExifDataTypes.Int16
                        SetDataValue(Of Int16)(value)
                    Case ExifDataTypes.Int32
                        SetDataValue(Of Int32)(value)
                    Case ExifDataTypes.NA
                        SetDataValue(Of Byte)(value)
                    Case ExifDataTypes.SByte
                        SetDataValue(Of SByte)(value)
                    Case ExifDataTypes.Single
                        SetDataValue(Of Single)(value)
                    Case ExifDataTypes.SRational
                        SetDataValue(Of SRational)(value)
                    Case ExifDataTypes.UInt16
                        SetDataValue(Of UInt16)(value)
                    Case ExifDataTypes.UInt32
                        SetDataValue(Of UInt32)(value)
                    Case ExifDataTypes.URational
                        SetDataValue(Of URational)(value)
                End Select
                _Data = value
            End Set
        End Property
        ''' <summary>Sets <paramref name="value"/> to <see cref="_Data"/> according to <see cref="DataType"/></summary>
        ''' <param name="value">Value to be set</param>
        ''' <typeparam name="T">Type of value to be set</typeparam>
        ''' <exception cref="InvalidCastException">Setting value of incompatible type</exception>
        ''' <exception cref="ArgumentException">Attempt to assigne value with other number of components when <see cref="Fixed"/> set to true</exception>
        Private Sub SetDataValue(Of T)(ByVal value As Object)
            Dim old As Object = _Data
            Dim changed As Boolean = False
            If Not IsArray(value) Then
                Try
                    Dim newV As T = CType(value, T)
                    If Me.DataType.NumberOfElements = 1 OrElse Not Me.Fixed Then
                        _Data = newV
                        Me.DataType.NumberOfElements = 1
                        changed = True
                    Else
                        Throw New ArgumentException(ResourcesT.Exceptions.CannotChangeNumberOfComponentsOfThisRecord)
                    End If
                Catch ex As Exception
                    Throw New InvalidCastException(ResourcesT.Exceptions.ValueOfIncompatibleTypePassedToExifRecord)
                End Try
            Else
                'Catch
                Try
                    Dim newV As T() = CType(value, T())
                    If Me.DataType.NumberOfElements = newV.Length OrElse Not Fixed Then
                        _Data = newV
                        Me.DataType.NumberOfElements = newV.Length
                        changed = True
                    Else
                        Throw New ArgumentException(ResourcesT.Exceptions.CannotChangeNumberOfComponentsOfThisRecord)
                    End If
                Catch ex As Exception
                    Throw New InvalidCastException(ResourcesT.Exceptions.ValueOfIncompatibleTypePassedToExifRecord)
                End Try
                'End Try
            End If
            If changed Then
                OnChanged(New IReportsChange.ValueChangedEventArgs(Of Object)(old, _Data, "Data"))
            End If
        End Sub
        ''' <summary>CTor</summary>
        ''' <param name="Data">Initial value of this record</param>
        ''' <param name="Type">Describes type of data contained in this flag</param>
        ''' <param name="Fixed">Determines if length of data can be changed</param>
        ''' <exception cref="InvalidCastException">Value passed to <paramref name="Data"/> is not compatible with <paramref name="Type"/> specified</exception>
        ''' <exception cref="ArgumentException"><paramref name="Fixed"/> is set to true and lenght of <paramref name="Data"/> violates this constaint</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Data"/> is null</exception>
        <CLSCompliant(False)> _
        Public Sub New(ByVal Type As ExifRecordDescription, ByVal Data As Object, Optional ByVal Fixed As Boolean = False)
            Me._DataType = Type
            Me._Fixed = Fixed
            Me.Data = Data
        End Sub
        ''' <summary>CTor</summary>
        ''' <param name="Data">Initial value of this record</param>
        ''' <param name="Type">Data type of record</param>
        ''' <param name="NumberOfComponents">Number of components of <paramref name="Type"/></param>
        ''' <param name="fixed">Determines if length of data can be changed</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Data"/> is null</exception>
        <CLSCompliant(False)> _
         Public Sub New(ByVal Data As Object, ByVal Type As ExifDataTypes, Optional ByVal NumberOfComponents As UShort = 1, Optional ByVal Fixed As Boolean = False)
            Me.New(New ExifRecordDescription(Type, NumberOfComponents), Data, Fixed)
        End Sub
        ''' <summary>Raises the <see cref="Changed"/> event</summary>
        ''' <param name="e">Event argument. Should be <see cref="IReportsChange.ValueChangedEventArgsBase"/>.</param>
        ''' <remarks>Changes of properties of <see cref="DataType"/> are not tracked.</remarks>
        Protected Overridable Sub OnChanged(ByVal e As EventArgs)
            RaiseEvent Changed(Me, e)
        End Sub
        ''' <summary>Raised when value of member changes</summary>
        ''' <remarks><paramref name="e"/>Should contain additional information that can be used in event-handling code (e.g. use <see cref="ireportschange.ValueChangedEventArgs(Of T)"/> class)
        ''' <para>Changes of properties of <see cref="DataType"/> are not tracked.</para></remarks>
        Public Event Changed As IReportsChange.ChangedEventHandler Implements IReportsChange.Changed
    End Class
#End If
End Namespace
