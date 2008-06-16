Namespace DrawingT.IO
    '#If Config <= Nightly Then Stage co      nditional compilation of this file is set in Tests.vbproj
    ''' <summary>Tests <see cref="Tools.DrawingT.IO.JPEG"/></summary>
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
                    Dim jpeg As Tools.DrawingT.IO.JPEG.JPEGReader
                    Try
                        jpeg = New Tools.DrawingT.IO.JPEG.JPEGReader(ofdOpen.FileName)
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                        Exit Sub
                    End Try
                    Dim root As TreeNode = tvwResults.Nodes.Add(New Tools.IOt.Path(ofdOpen.FileName).FileName)
                    root.Text &= "Size " & jpeg.ImageStream.Length & "B"
                    root.Tag = jpeg
                    'JPEG markers
                    For Each Marker As Tools.DrawingT.IO.JPEG.JPEGMarkerReader In jpeg.Markers
                        Dim mNode As TreeNode = root.Nodes.Add(Hex(Marker.MarkerCode))
                        If Marker.MarkerCode <> Tools.DrawingT.IO.JPEG.JPEGMarkerReader.Markers.Unknown Then
                            mNode.Text &= " " & [Enum].GetName(GetType(Tools.DrawingT.IO.JPEG.JPEGMarkerReader.Markers), Marker.MarkerCode)
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
                        'Exif IFDs
                        Dim Exif As New Tools.DrawingT.MetadataT.ExifReader(jpeg)
                        Dim i As Integer = 0
                        For Each IFD As Tools.DrawingT.MetadataT.ExifIFDReader In Exif.IFDs
                            Dim IFDNode As TreeNode = ExifNode.Nodes.Add(String.Format("IFD {0} offset {1} next {2}", i, Hex(IFD.Offest), Hex(IFD.NextIFD)))
                            IFDNode.Tag = IFD
                            For Each Entry As Tools.DrawingT.MetadataT.ExifIFDReader.DirectoryEntry In IFD.Entries
                                Dim EntryNode As TreeNode = IFDNode.Nodes.Add(String.Format( _
                                    "Entry {0} type {1} components {2} data {3}", Hex(Entry.Tag), Entry.DataType, Entry.Components, Entry.Data))
                                EntryNode.Tag = Entry
                            Next Entry
                            i += 1
                        Next IFD
                        'Exif Sub IFD
                        If Exif.ExifSubIFD IsNot Nothing Then
                            Dim ExifSubIFDNode As TreeNode = ExifNode.Nodes(0).Nodes(Exif.ExifSubIFD.ParentRecord)
                            Dim IFDNode As TreeNode = ExifSubIFDNode.Nodes.Add(String.Format("Exif Sub IFD offset {0} next {1}", Hex(Exif.ExifSubIFD.Offest), Hex(Exif.ExifSubIFD.NextIFD)))
                            IFDNode.Tag = Exif.ExifSubIFD
                            For Each Entry As Tools.DrawingT.MetadataT.ExifIFDReader.DirectoryEntry In Exif.ExifSubIFD.Entries
                                Dim EntryNode As TreeNode = IFDNode.Nodes.Add(String.Format( _
                                    "Entry {0} type {1} components {2} data {3}", Hex(Entry.Tag), Entry.DataType, Entry.Components, Entry.Data))
                                EntryNode.Tag = Entry
                            Next Entry
                            'Exif Interoperability Sub IFD
                            If Exif.ExifInteroperabilityIFD IsNot Nothing Then
                                Dim InteropNode As TreeNode = IFDNode.Nodes(Exif.ExifInteroperabilityIFD.ParentRecord).Nodes.Add(String.Format("Exif Interoperability IFD offset {0} next {1}", Hex(Exif.ExifInteroperabilityIFD.Offest), Hex(Exif.ExifInteroperabilityIFD.NextIFD)))
                                InteropNode.Tag = Exif.ExifInteroperabilityIFD
                                For Each Entry As Tools.DrawingT.MetadataT.ExifIFDReader.DirectoryEntry In Exif.ExifInteroperabilityIFD.Entries
                                    Dim EntryNode As TreeNode = InteropNode.Nodes.Add(String.Format( _
                                      "Entry {0} type {1} components {2} data {3}", Hex(Entry.Tag), Entry.DataType, Entry.Components, Entry.Data))
                                    EntryNode.Tag = Entry
                                Next Entry
                            End If
                        End If
                        'GPS SUb IFD
                        If Exif.GPSSubIFD IsNot Nothing Then
                            Dim GPSSubIFDNode As TreeNode = ExifNode.Nodes(0).Nodes(Exif.GPSSubIFD.ParentRecord)
                            Dim GPSNode As TreeNode = GPSSubIFDNode.Nodes.Add(String.Format("GPS Sub IFD offset {0} next {1}", Hex(Exif.GPSSubIFD.Offest), Hex(Exif.GPSSubIFD.NextIFD)))
                            GPSNode.Tag = Exif.GPSSubIFD
                            For Each Entry As Tools.DrawingT.MetadataT.ExifIFDReader.DirectoryEntry In Exif.ExifSubIFD.Entries
                                Dim EntryNode As TreeNode = GPSNode.Nodes.Add(String.Format( _
                                  "Entry {0} type {1} components {2} data {3}", Hex(Entry.Tag), Entry.DataType, Entry.Components, Entry.Data))
                                EntryNode.Tag = Entry
                            Next Entry
                        End If
                    End If
                    'TODO:Show additional IFDs
                    'PhotoShop block
                    Dim PhotoShopStream As System.IO.Stream = jpeg.GetPhotoShopStream
                    Dim PhotoShopIndex As Integer = jpeg.PhotoshopMarkerIndex
                    If PhotoShopIndex >= 0 Then
                        Dim PhotoShopNode As TreeNode = root.Nodes(PhotoShopIndex).Nodes.Add(String.Format("PhotoShop size {0}B", PhotoShopStream.Length))
                        PhotoShopNode.Tag = ExifStream
                        PhotoShopNode.Text &= " " & PhotoShopStream.ToString
                        For Each BIM8 As Tools.DrawingT.IO.JPEG.Photoshop8BIMReader In jpeg.Get8BIMSegments
                            Dim Node8 As TreeNode = PhotoShopNode.Nodes.Add(String.Format("8BIM {0} name '{1}' size {2}B", Hex(BIM8.Type), BIM8.Name, BIM8.DataSize))
                            Node8.Tag = BIM8
                            Node8.Text &= " " & BIM8.Data.ToString
                        Next BIM8
                    End If
                    'IPTC block
                    Dim IPTCStream As System.IO.Stream = jpeg.GetIPTCStream
                    Dim IPTCIndex As Integer = jpeg.IPTC8BIMSegmentIndex
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
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                End Try
            End If
        End Sub

        Private Sub frmJPEG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            nudSize.Value = tvwResults.Font.SizeInPoints
        End Sub

        Private Sub nudSize_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudSize.ValueChanged
            tvwResults.Font = New Font(tvwResults.Font.FontFamily, nudSize.Value, tvwResults.Font.Strikeout, GraphicsUnit.Point)
        End Sub
    End Class
End Namespace