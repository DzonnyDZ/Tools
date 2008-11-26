#If Config <= Nightly Then 'Stage:Nightly
Imports Kinds = Tools.MetadataT.ExifT.ExifReader.ReaderItemKinds
Namespace MetadataT.ExifT
    ''' <summary>Generates map of Exif metadata in order to get known which areas can be safely overwritten without loss of unknown data (maker notes, unknown sub-ifds etc.)</summary>
    ''' <remarks>This is advanced class to advancedly deal with Exif metadata. You usually do not need to use it directly. Just instruct another class to take advantage of this one.</remarks>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Class ExifMapGenerator
        ''' <summary>Attaches this map generator to given <see cref="ExifReaderSettings"/></summary>
        ''' <param name="Settings"><see cref="ExifReaderSettings"/> to attach to</param>
        ''' <exception cref="InvalidOperationException">This instance is already attached</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="Settings"/> is null</exception>
        Public Sub AttachTo(ByVal Settings As ExifReaderSettings)
            If Settings Is Nothing Then Throw New ArgumentNullException("Settings")
            Static Attached As Boolean = False
            If Attached = True Then Throw New InvalidOperationException(ResourcesT.Exceptions.ExifMapGeneratorCannotBeAttachedToMultipleReaders)
            AddHandler Settings.ReadItem, AddressOf OnItem
            Attached = True
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()
        End Sub
        ''' <summary>CTpr from <see cref="ExifReaderSettings"/></summary>
        ''' <param name="Settings"><see cref="ExifReaderSettings"/> to attach to</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Settings"/> is null</exception>
        Public Sub New(ByVal Settings As ExifReaderSettings)
            Me.New()
            AttachTo(Settings)
        End Sub
        ''' <summary>Reads all the Exif metadata and creates map of it</summary>
        ''' <param name="Source">Stream which contains the metadata</param>
        ''' <returns>Map of read metadata</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Source"/> is null</exception>
        Public Shared Function CreateMap(ByVal Source As IO.Stream) As ExifMapGenerator
            If Source Is Nothing Then Throw New ArgumentNullException("Source")
            Dim settings As New ExifReaderSettings()
            Dim inst As New ExifMapGenerator(settings)
            Dim R As New ExifReader(Source, settings)
            Dim Exif As New Exif(R)
            Return inst
        End Function
        ''' <summary>Reads all the Exif metadata and creates map of it</summary>
        ''' <param name="Source">Source of exif data</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Source"/> is null</exception>
        ''' <returns>Map of read metadata</returns>
        Public Shared Function CreateMap(ByVal Source As IExifGetter) As ExifMapGenerator
            If Source Is Nothing Then Throw New ArgumentNullException("Source")
            Return CreateMap(Source.GetExifStream)
        End Function
        ''' <summary>Contains value of the <see cref="Map"/> property</summary>
        Private _Map As Kinds()
        ''' <summary>After all metadata was read contains map of them</summary>
        ''' <returns>Array that defines which exif metadata are located where in the stream</returns>
        Public ReadOnly Property Map() As Kinds()
            Get
                Return _Map
            End Get
        End Property
        ''' <summary>Handles <see cref="ExifReaderSettings.ReadItem"/> event and creates the map as metadata are read</summary>
        ''' <param name="sender">Actual instance which parses the metadata</param>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnItem(ByVal sender As Object, ByVal e As ExifReader.ExifEventArgs)
            Select Case e.ItemKind
                Case Kinds.Exif
                    ReDim _Map(e.ItemLength - 1)
                Case Kinds.Bom, Kinds.BomTest, Kinds.Ifd0Offset, Kinds.IfdNumberOfEntries, Kinds.TagNumber, Kinds.TagDataType, _
                        Kinds.TagComponents, Kinds.TagDataOrOffset, Kinds.ExternalTagData, Kinds.NextIfdOffset, Kinds.JpegThumbnail, _
                        Kinds.TiffThumbnailPart, Kinds.SubIfdNumberOfEntries, Kinds.NextSubIfdOffset
                    For i As Integer = e.ItemAbsoluteOffset To e.ItemAbsoluteOffset + e.ItemLength - 1
                        Map(i) = e.ItemKind
                    Next
            End Select
        End Sub
    End Class
End Namespace
#End If
