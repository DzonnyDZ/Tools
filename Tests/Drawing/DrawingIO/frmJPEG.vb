Imports ex = Tools.DrawingT.MetadataT.ExifT
Namespace DrawingT.DrawingIOt
    '#If Config <= Nightly Then Stage co      nditional compilation of this file is set in Tests.vbproj
    ''' <summary>Tests <see cref="Tools.DrawingT.DrawingIOt.JPEG"/></summary>
    Public Class frmJPEG
        ''' <summary>Collors form Exif map</summary>
        Private Colors As New Dictionary(Of ex.ExifReader.ReaderItemKinds, Color)
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon

            Colors.Add(ex.ExifReader.ReaderItemKinds.Unknown, lblMap_Unknown.BackColor)
            Colors.Add(ex.ExifReader.ReaderItemKinds.Bom, lblMap_BOM.BackColor)
            Colors.Add(ex.ExifReader.ReaderItemKinds.BomTest, lblMap_BomTest.BackColor)
            Colors.Add(ex.ExifReader.ReaderItemKinds.Ifd0Offset, lblMap_IFD0Offset.BackColor)
            Colors.Add(ex.ExifReader.ReaderItemKinds.IfdNumberOfEntries, lblMap_IFDNumberOfEntries.BackColor)
            Colors.Add(ex.ExifReader.ReaderItemKinds.TagNumber, lblMap_TagNumber.BackColor)
            Colors.Add(ex.ExifReader.ReaderItemKinds.TagDataType, lblMap_TagDataType.BackColor)
            Colors.Add(ex.ExifReader.ReaderItemKinds.TagComponents, lblMap_TagNumberOfComponents.BackColor)
            Colors.Add(ex.ExifReader.ReaderItemKinds.TagDataOrOffset, lblMap_TagDataOrOffset.BackColor)
            Colors.Add(ex.ExifReader.ReaderItemKinds.ExternalTagData, lblMap_ExternalTagData.BackColor)
            Colors.Add(ex.ExifReader.ReaderItemKinds.NextIfdOffset, lblMap_NextIFDOffset.BackColor)
            Colors.Add(ex.ExifReader.ReaderItemKinds.JpegThumbnail, lblMap_JPEGThumbnail.BackColor)
            Colors.Add(ex.ExifReader.ReaderItemKinds.TiffThumbnailPart, lblMap_TIFFThumbnailPart.BackColor)
            Colors.Add(ex.ExifReader.ReaderItemKinds.SubIfdNumberOfEntries, lblMap_SubIFDNumberOfEntries.BackColor)
            Colors.Add(ex.ExifReader.ReaderItemKinds.NextSubIfdOffset, lblMap_NextSubIFDOffset.BackColor)

        End Sub
        ''' <summary>Runs test</summary>
        Public Shared Sub test()
            Dim frm As New frmJPEG
            frm.Show()
        End Sub

        Dim Map As ex.ExifReader.ReaderItemKinds()
        Dim LastExifData As Byte()
        Dim LastJpeg As Tools.DrawingT.DrawingIOt.JPEG.JPEGReader
        Dim ExifBuilder As System.Text.StringBuilder

        Private Sub cmdParse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdParse.Click
            If ofdOpen.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Map = Nothing
                dgwMap.Rows.Clear()
                If LastJpeg IsNot Nothing Then LastJpeg.Dispose()
                LastJpeg = Nothing
                LastExifData = Nothing
                Try
                    tvwResults.Nodes.Clear()
                    'JPEG file
                    Dim jpeg As Tools.DrawingT.DrawingIOt.JPEG.JPEGReader
                    Try
                        jpeg = New Tools.DrawingT.DrawingIOt.JPEG.JPEGReader(ofdOpen.FileName)
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                        Exit Sub
                    End Try
                    LastJpeg = jpeg
                    jpeg.SupportFujiFilmFineFix2800Zoom = chkFujiFilmFinePix2800Zoom.Checked
                    Dim root As TreeNode = tvwResults.Nodes.Add(New Tools.IOt.Path(ofdOpen.FileName).FileName)
                    root.Text &= "Size " & jpeg.ImageStream.Length & "B"
                    root.Tag = jpeg
                    'JPEG markers
                    For Each Marker As Tools.DrawingT.DrawingIOt.JPEG.JPEGMarkerReader In jpeg.Markers
                        Dim mNode As TreeNode = root.Nodes.Add(Hex(Marker.MarkerCode))
                        If Marker.MarkerCode <> Tools.DrawingT.DrawingIOt.JPEG.JPEGMarkerReader.Markers.Unknown Then
                            mNode.Text &= " " & [Enum].GetName(GetType(Tools.DrawingT.DrawingIOt.JPEG.JPEGMarkerReader.Markers), Marker.MarkerCode)
                        End If
                        mNode.Text &= String.Format(" size {0}B, stream size {1}B", Marker.Length, Marker.Data.Length)
                        mNode.Text &= " " & Marker.Data.ToString
                        mNode.Tag = Marker
                    Next Marker
                    'JPEG image data
                    Dim DataNode As TreeNode = root.Nodes.Add(String.Format("Image stream size {0}B", jpeg.ImageStream.Length))
                    DataNode.Tag = jpeg.ImageStream
                    DataNode.Text &= " " & jpeg.ImageStream.ToString
                    'Exif block
                    Dim ExifStream As System.IO.Stream = jpeg.GetExifStream
                    Dim ExifIndex As Integer = jpeg.ExifMarkerIndex
                    If ExifIndex >= 0 Then
                        Dim ExifNode As TreeNode = root.Nodes(ExifIndex).Nodes.Add(String.Format("Exif size {0}B", ExifStream.Length))
                        ExifNode.Tag = ExifStream
                        ExifNode.Text &= " " & ExifStream.ToString
                        If jpeg.SupportFujiFilmFineFix2800ZoomInEffect Then ExifNode.Text &= " FujiFilm FinePix 2800 zoom"
                        'Exif IFDs
                        Dim ExifSettings As New ex.ExifReaderSettings() With {.ReadThumbnail = True}
                        AddHandler ExifSettings.ReadItem, AddressOf ExifEventHandler
                        AddHandler ExifSettings.ReadError, AddressOf ExifErrorHandler
                        ExifSettings.ErrorRecovery = Tools.DrawingT.MetadataT.ExifT.ErrorRecoveryModes.Custom
                        ExifBuilder = New System.Text.StringBuilder
                        Dim Map As New ex.ExifMapGenerator(ExifSettings)
                        Dim Exif As New ex.ExifReader(jpeg, ExifSettings)
                        'Dim i As Integer = 0
                        ' For Each IFD As Tools.DrawingT.MetadataT.ExifTIFDReader In Exif.IFDs
                        PresentIfd(New ex.IfdMain(Exif.IFDs(0), True), ExifNode, 0)
                        PresentMap(Map, ExifStream)
                        txtEvents.Text = ExifBuilder.ToString
                        'i += 1
                        'Next IFD
                    End If
                    'PhotoShop block
                    Dim PhotoShopStream As System.IO.Stream = jpeg.GetPhotoShopStream
                    Dim PhotoShopIndex As Integer = jpeg.PhotoshopMarkerIndex
                    If PhotoShopIndex >= 0 Then
                        Dim PhotoShopNode As TreeNode = root.Nodes(PhotoShopIndex).Nodes.Add(String.Format("PhotoShop size {0}B", PhotoShopStream.Length))
                        PhotoShopNode.Tag = ExifStream
                        PhotoShopNode.Text &= " " & PhotoShopStream.ToString
                        Dim BIM8s As Tools.CollectionsT.GenericT.IReadOnlyList(Of Tools.DrawingT.DrawingIOt.JPEG.Photoshop8BIMReader) = Nothing
                        Try
                            BIM8s = jpeg.Get8BIMSegments
                        Catch ex As Exception
                            Dim Node8 As TreeNode = PhotoShopNode.Nodes.Add("8BIM: " & ex.Message)
                            Node8.Tag = ex
                        End Try
                        If BIM8s IsNot Nothing Then
                            For Each BIM8 As Tools.DrawingT.DrawingIOt.JPEG.Photoshop8BIMReader In BIM8s
                                Dim Node8 As TreeNode = PhotoShopNode.Nodes.Add(String.Format("8BIM {0} name '{1}' size {2}B", Hex(BIM8.Type), BIM8.Name, BIM8.DataSize))
                                Node8.Tag = BIM8
                                Node8.Text &= " " & BIM8.Data.ToString
                            Next BIM8
                            'IPTC block
                            Dim IPTCStream As System.IO.Stream = jpeg.GetIPTCStream
                            Dim IPTCIndex As Integer = -1
                            Try
                                IPTCIndex = jpeg.IPTC8BIMSegmentIndex
                            Catch ex As Exception
                                Dim IptcNode = root.Nodes(PhotoShopIndex).Nodes(0).Nodes.Add("IPTC: " & ex.Message)
                                IptcNode.Tag = ex
                            End Try
                            If IPTCIndex >= 0 Then
                                Dim IPTCNode As TreeNode = root.Nodes(PhotoShopIndex).Nodes(0).Nodes(IPTCIndex).Nodes.Add _
                                    (String.Format("IPTC size {0}B", IPTCStream.Length))
                                IPTCNode.Tag = IPTCStream
                                IPTCNode.Text &= " " & IPTCStream.ToString
                                'IPTC data
                                Dim IPTC As New Tools.DrawingT.MetadataT.IptcT.IptcReader(jpeg)
                                For Each Record As Tools.DrawingT.MetadataT.IptcT.IptcRecord In IPTC.Records
                                    Dim RecordNode As TreeNode = IPTCNode.Nodes.Add(String.Format("{0}{1} size {2}B data {3}", Hex(Record.RecordNumber), Hex(Record.Tag), Record.Size, Record.StringData))
                                    RecordNode.Tag = Record
                                Next Record
                            End If
                        End If
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                End Try
            End If
        End Sub

        Private Sub ExifEventHandler(ByVal sender As Object, ByVal e As ex.ExifReader.ExifEventArgs)
            ExifBuilder.AppendLine(String.Format("{0:g} {1},{2} ""{3}""", e.ItemKind, e.ItemAbsoluteOffset, e.ItemLength, e.ItemObject))
        End Sub
        Private Sub ExifErrorHandler(ByVal sender As ex.ExifReader, ByVal e As Tools.ComponentModelT.RecoveryExceptionEventArgs)
            ExifBuilder.AppendLine(String.Format("{0}: {1}", e.Exception.GetType.Name, e.Exception.Message))
        End Sub
        ''' <summary>Presents IFD to tree</summary>
        ''' <param name="IFD">IFD to be presented</param>
        ''' <param name="Index">Index of IFD in its owner</param>
        ''' <param name="ParentNode">Parent node to present ifd to</param>
        Private Sub PresentIfd(ByVal IFD As ex.Ifd, ByVal ParentNode As TreeNode, ByVal Index As Integer)
            Dim IFDNode As New TreeNode
            Dim IfdName As String
            If TypeOf IFD Is ex.IfdMain Then
                IfdName = String.Format("IFD {0}", Index)
            ElseIf TypeOf IFD Is ex.IfdExif Then
                IfdName = "Exif Sub IFD"
            ElseIf TypeOf IFD Is ex.IfdGps Then
                IfdName = "GPS Sub IFD"
            ElseIf TypeOf IFD Is ex.IfdInterop Then
                IfdName = "Interop Sub IFD"
            ElseIf TypeOf IFD Is ex.SubIfd Then
                IfdName = "Sub IFD"
            Else
                IfdName = "IFD"
            End If
            IFDNode.Text = String.Format("{0} offset &h{1:x} next &h{2:x}", IfdName, IFD.OriginalOffset, If(IFD.Following IsNot Nothing, IFD.Following.OriginalOffset, 0UI))
            ParentNode.Nodes.Add(IFDNode)
            IFDNode.Tag = IFD

            For Each Entry In IFD.Records
                'Exif tags
                Dim EntryNode As TreeNode = IFDNode.Nodes.Add(String.Format( _
                    "Entry {0:x} type {1} components {2} data {3}", _
                    Entry.Key, Entry.Value.DataType.DataType, _
                    Entry.Value.DataType.NumberOfElements, _
                    Entry.Value.Data))
                EntryNode.Tag = Entry.Value
                If IFD.SubIFDs.ContainsKey(Entry.Key) Then
                    PresentIfd(IFD.SubIFDs(Entry.Key), EntryNode, 0)
                End If
                'Thumbnail
                If TypeOf IFD Is ex.IfdMain Then
                    With DirectCast(IFD, ex.IfdMain)
                        If Entry.Key = ex.IfdMain.Tags.JPEGInterchangeFormat _
                            AndAlso .HasThumbnail AndAlso .Compression = ex.IfdMain.CompressionValues.JPEG Then
                            Dim JpegNode = EntryNode.Nodes.Add("JPEG thumbnail")
                            JpegNode.Tag = IFD
                        ElseIf Entry.Key = ex.IfdMain.Tags.StripOffsets _
                                AndAlso .HasThumbnail AndAlso .Compression = ex.IfdMain.CompressionValues.uncompressed Then
                            Dim TiffNode = EntryNode.Nodes.Add("Uncompressed thumbnail")
                            TiffNode.Tag = IFD
                        End If
                    End With
                End If
            Next Entry
            If IFD.Following IsNot Nothing Then _
               PresentIfd(IFD.Following, ParentNode, Index + 1)
        End Sub

        Private Sub frmJPEG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            nudSize.Value = tvwResults.Font.SizeInPoints
        End Sub

        Private Sub nudSize_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudSize.ValueChanged
            tvwResults.Font = New Font(tvwResults.Font.FontFamily, nudSize.Value, tvwResults.Font.Strikeout, GraphicsUnit.Point)
        End Sub

        Private Sub cmsContext_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsContext.Opening
            If tvwResults.SelectedNode Is Nothing Then
                e.Cancel = True
            Else
                tmiExport.Enabled = TypeOf tvwResults.SelectedNode.Tag Is Tools.DrawingT.DrawingIOt.JPEG.JPEGMarkerReader _
                        OrElse TypeOf tvwResults.SelectedNode.Tag Is Byte() OrElse (TypeOf tvwResults.SelectedNode.Tag Is ex.IfdMain AndAlso tvwResults.SelectedNode.Text Like "*thumbnail*")
            End If
        End Sub

        Private Sub tmiExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmiExport.Click
            If tvwResults.SelectedNode IsNot Nothing AndAlso TypeOf tvwResults.SelectedNode.Tag Is Tools.DrawingT.DrawingIOt.JPEG.JPEGMarkerReader Then
                If sfdSave.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Dim r As Tools.DrawingT.DrawingIOt.JPEG.JPEGMarkerReader = tvwResults.SelectedNode.Tag
                    Dim data = r.Data
                    data.Seek(0, IO.SeekOrigin.Begin)
                    Using f = IO.File.Open(sfdSave.FileName, IO.FileMode.OpenOrCreate, IO.FileAccess.Write)
                        Do
                            Dim buffer(1023) As Byte
                            Dim bcnt = data.Read(buffer, 0, buffer.Length)
                            If bcnt = 0 Then Exit Do
                            f.Write(buffer, 0, bcnt)
                        Loop
                        f.Flush()
                    End Using
                End If
            ElseIf tvwResults.SelectedNode IsNot Nothing AndAlso TypeOf tvwResults.SelectedNode.Tag Is Byte() Then
                If sfdSave.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Using f = IO.File.Open(sfdSave.FileName, IO.FileMode.OpenOrCreate, IO.FileAccess.Write)
                        f.Write(tvwResults.SelectedNode.Tag, 0, CType(tvwResults.SelectedNode.Tag, Byte()).Length)
                        f.Flush()
                    End Using
                End If
            ElseIf tvwResults.SelectedNode IsNot Nothing AndAlso TypeOf tvwResults.SelectedNode.Tag Is ex.IfdMain AndAlso tvwResults.SelectedNode.Text Like "*thumbnail*" Then
                If sfdSave.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Using f = IO.File.Open(sfdSave.FileName, IO.FileMode.OpenOrCreate, IO.FileAccess.Write)
                        f.Write(CType(tvwResults.SelectedNode.Tag, ex.IfdMain).ThumbnailData, 0, CType(tvwResults.SelectedNode.Tag, ex.IfdMain).ThumbnailData.Length)
                        f.Flush()
                    End Using
                End If
            Else
                Beep()
            End If
        End Sub

        Private Sub tvwResults_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwResults.AfterSelect
            prgProperty.Controls(0).BackgroundImage = Nothing
            If e.Node Is Nothing OrElse e.Node.Tag Is Nothing Then
                prgProperty.SelectedObject = Nothing
                lblType.Text = ""
                lblToString.Text = ""
            Else
                prgProperty.SelectedObject = e.Node.Tag
                lblType.Text = e.Node.Tag.GetType.Name
                lblToString.Text = e.Node.Tag.ToString
                If TypeOf e.Node.Tag Is Image Then
                    prgProperty.Controls(0).BackgroundImage = e.Node.Tag
                    prgProperty.Controls(0).BackgroundImageLayout = ImageLayout.Center
                    For Each Control As Control In prgProperty.Controls(0).Controls
                        Control.BackColor = Color.Transparent
                    Next
                ElseIf TypeOf e.Node.Tag Is Byte() AndAlso e.Node.Text Like "*thumbnail*" Then
                    Try
                        Dim b = Bitmap.FromStream(New IO.MemoryStream(e.Node.Tag, False))
                        prgProperty.Controls(0).BackgroundImage = b
                        prgProperty.Controls(0).BackgroundImageLayout = ImageLayout.Center
                        For Each Control As Control In prgProperty.Controls(0).Controls
                            Control.BackColor = Color.Transparent
                        Next
                    Catch ex As Exception
                        Tools.WindowsT.IndependentT.MessageBox.Error(ex)
                    End Try
                ElseIf TypeOf e.Node.Tag Is ex.IfdMain AndAlso e.Node.Text Like "*thumbnail*" Then
                    Try
                        Dim b = DirectCast(e.Node.Tag, ex.IfdMain).Thumbnail
                        prgProperty.Controls(0).BackgroundImage = b
                        prgProperty.Controls(0).BackgroundImageLayout = ImageLayout.Center
                        For Each Control As Control In prgProperty.Controls(0).Controls
                            Control.BackColor = Color.Transparent
                        Next
                    Catch ex As Exception
                        Tools.WindowsT.IndependentT.MessageBox.Error(ex)
                    End Try
                End If
            End If
        End Sub
        ''' <summary>Present Exifmap to user</summary>
        ''' <param name="Map">Map to represent</param>
        Private Sub PresentMap(ByVal Map As ex.ExifMapGenerator, ByVal ExifStream As IO.Stream)
            ReDim LastExifData(ExifStream.Length - 1)
            ExifStream.Position = 0
            Dim pos% = 0
            While pos < LastExifData.Length
                pos += ExifStream.Read(LastExifData, pos, LastExifData.Length - pos)
            End While
            Me.Map = Map.Map
            dgwMap.RowCount = CInt(Math.Ceiling(Map.Map.Length / 16))
        End Sub

        Private Sub chkASCII_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkASCII.CheckedChanged
            dgwMap.Rows.Clear()
            dgwMap.RowCount = CInt(Math.Ceiling(Map.Length / 16))
        End Sub

        Private Sub dgwMap_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgwMap.CellFormatting
            If Map Is Nothing Then Exit Sub
            If e.ColumnIndex > 0 AndAlso e.RowIndex >= 0 Then
                Dim pos = e.RowIndex * 16 + e.ColumnIndex - 1
                If pos >= Map.Length Then Exit Sub
                With e.CellStyle
                    .BackColor = Colors(Map(pos))
                    .SelectionBackColor = .BackColor
                    .SelectionForeColor = Color.Black
                End With
            End If
        End Sub

        Private Sub dgwMap_CellToolTipTextNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellToolTipTextNeededEventArgs) Handles dgwMap.CellToolTipTextNeeded
            If Map Is Nothing Then Exit Sub
            If e.ColumnIndex > 0 AndAlso e.RowIndex >= 1 Then
                Dim pos = e.RowIndex * 16 + e.ColumnIndex - 1
                If pos >= Map.Length Then Exit Sub
                e.ToolTipText = Map(pos).ToString("G")
            End If
        End Sub

        Private Sub dgwMap_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles dgwMap.CellValueNeeded
            If e.ColumnIndex = 0 Then e.Value = e.RowIndex + 1 : Exit Sub
            If LastExifData Is Nothing OrElse Map Is Nothing Then Exit Sub
            Dim pos = e.RowIndex * 16 + e.ColumnIndex - 1
            If pos >= LastExifData.Length Then Exit Sub
            Dim b = LastExifData(pos)
            Dim val = If(chkASCII.Checked, Chr(b), b.ToString("X2"))
            e.Value = val
        End Sub
    End Class
End Namespace