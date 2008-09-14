Namespace DrawingT.DrawingIOt
    '#If Config <= Nightly Then Stage co      nditional compilation of this file is set in Tests.vbproj
    ''' <summary>Tests <see cref="Tools.DrawingT.DrawingIOt.JPEG"/></summary>
    Public Class frmJPEG
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub
        ''' <summary>Runs test</summary>
        Public Shared Sub test()
            Dim frm As New frmJPEG
            frm.Show()
        End Sub

        Private Sub cmdParse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdParse.Click
            If ofdOpen.ShowDialog = System.Windows.Forms.DialogResult.OK Then
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
                        Dim Exif As New Tools.DrawingT.MetadataT.ExifReader(jpeg)
                        'Dim i As Integer = 0
                        ' For Each IFD As Tools.DrawingT.MetadataT.ExifIFDReader In Exif.IFDs
                        PresentIfd(New Tools.DrawingT.MetadataT.Exif.IFDMain(Exif.IFDs(0), True), ExifNode, 0, Exif)
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
                                Dim IPTC As New Tools.DrawingT.MetadataT.IPTCReader(jpeg)
                                For Each Record As Tools.DrawingT.MetadataT.IPTCReader.IPTCRecord In IPTC.Records
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
        ''' <summary>Presents IFD to tree</summary>
        ''' <param name="IFD">IFD to be presented</param>
        ''' <param name="Index">Index of IFD in its owner</param>
        ''' <param name="ParentNode">Parent node to present ifd to</param>
        ''' <param name="R"><see cref="Tools.DrawingT.MetadataT.ExifReader"/> used for thumbnails</param>
        Private Sub PresentIfd(ByVal IFD As Tools.DrawingT.MetadataT.Exif.IFD, ByVal ParentNode As TreeNode, ByVal Index As Integer, ByVal R As Tools.DrawingT.MetadataT.ExifReader)
            Dim IFDNode As New TreeNode
            Dim IfdName As String
            If TypeOf IFD Is Tools.DrawingT.MetadataT.Exif.IFDMain Then
                IfdName = String.Format("IFD {0}", Index)
            ElseIf TypeOf IFD Is Tools.DrawingT.MetadataT.Exif.IFDExif Then
                IfdName = "Exif Sub IFD"
            ElseIf TypeOf IFD Is Tools.DrawingT.MetadataT.Exif.IFDGPS Then
                IfdName = "GPS Sub IFD"
            ElseIf TypeOf IFD Is Tools.DrawingT.MetadataT.Exif.IFDInterop Then
                IfdName = "Interop Sub IFD"
            ElseIf TypeOf IFD Is Tools.DrawingT.MetadataT.Exif.SubIFD Then
                IfdName = "Sub IFD"
            Else
                IfdName = "IFD"
            End If
            IFDNode.Text = String.Format("{0} offset &h{1:x} next &h{2:x}", IfdName, IFD.OriginalOffset, If(IFD.Following IsNot Nothing, IFD.Following.OriginalOffset, 0UI))
            ParentNode.Nodes.Add(IFDNode)
            IFDNode.Tag = IFD

            For Each Entry In IFD.Records
                Dim EntryNode As TreeNode = IFDNode.Nodes.Add(String.Format( _
                    "Entry {0:x} type {1} components {2} data {3}", _
                    Entry.Key, Entry.Value.DataType.DataType, _
                    Entry.Value.DataType.NumberOfElements, _
                    Entry.Value.Data))
                EntryNode.Tag = Entry.Value
                If IFD.SubIFDs.ContainsKey(Entry.Key) Then
                    PresentIfd(IFD.SubIFDs(Entry.Key), EntryNode, 0, R)
                End If
                If TypeOf IFD Is Tools.DrawingT.MetadataT.Exif.IFDMain Then
                    With DirectCast(IFD, Tools.DrawingT.MetadataT.Exif.IFDMain)
                        If Entry.Key = Tools.DrawingT.MetadataT.Exif.IFDMain.Tags.JPEGInterchangeFormat _
                            AndAlso .HasThumbnail AndAlso .Compression = Tools.DrawingT.MetadataT.Exif.IFDMain.CompressionValues.JPEG Then
                            Dim JpegNode = EntryNode.Nodes.Add("JPEG thumbnail")
                            Try
                                JpegNode.Tag = .GetThumbnail(R)
                            Catch ex As Exception
                                JpegNode.Tag = ex
                            End Try
                        ElseIf Entry.Key = Tools.DrawingT.MetadataT.Exif.IFDMain.Tags.StripOffsets _
                            AndAlso .HasThumbnail AndAlso .Compression = Tools.DrawingT.MetadataT.Exif.IFDMain.CompressionValues.uncompressed Then
                            Dim TiffNode = EntryNode.Nodes.Add("Uncompressed thumbnail")
                            Try
                                TiffNode.Tag = .GetThumbnail(R)
                            Catch ex As Exception
                                TiffNode.Tag = ex
                            End Try
                        End If
                    End With
                End If
            Next Entry
            If IFD.Following IsNot Nothing Then _
               PresentIfd(IFD.Following, ParentNode, Index + 1, R)
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
                tmiExport.Enabled = TypeOf tvwResults.SelectedNode.Tag Is Tools.DrawingT.DrawingIOt.JPEG.JPEGMarkerReader
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
                End If
            End If
        End Sub
    End Class
End Namespace