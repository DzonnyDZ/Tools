Namespace Drawing.IO
    ''' <summary>Tests <see cref="Tools.Drawing.IO.JPEG"/></summary>
    Public Class frmJPEG
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.Resources.ToolsIcon
        End Sub
        ''' <summary>Runs test</summary>
        Public Shared Sub test()
            Dim frm As New frmJPEG
            frm.Show()
        End Sub

        Private Sub cmdParse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdParse.Click
            If ofdOpen.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                tvwResults.Nodes.Clear()
                'JPEG file
                Dim jpeg As Tools.Drawing.IO.JPEG.JPEGReader
                Try
                    jpeg = New Tools.Drawing.IO.JPEG.JPEGReader(ofdOpen.FileName)
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                    Exit Sub
                End Try
                Dim root As TreeNode = tvwResults.Nodes.Add(New Tools.IO.Path(ofdOpen.FileName).FileName)
                root.Text &= "Size " & jpeg.ImageStream.Length & "B"
                root.Tag = jpeg
                'JPEG markers
                For Each Marker As Tools.Drawing.IO.JPEG.JPEGMarkerReader In jpeg.Markers
                    Dim mNode As TreeNode = root.Nodes.Add(Hex(Marker.MarkerCode))
                    If Marker.MarkerCode <> Tools.Drawing.IO.JPEG.JPEGMarkerReader.Markers.Unknown Then
                        mNode.Text &= " " & [Enum].GetName(GetType(Tools.Drawing.IO.JPEG.JPEGMarkerReader.Markers), Marker.MarkerCode)
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
                End If
                'PhotoShop block
                Dim PhotoShopStream As System.IO.Stream = jpeg.GetPhotoShopStream
                Dim PhotoShopIndex As Integer = jpeg.PhotoshopMarkerIndex
                If PhotoShopIndex >= 0 Then
                    Dim PhotoShopNode As TreeNode = root.Nodes(PhotoShopIndex).Nodes.Add(String.Format("PhotoShop size {0}B", PhotoShopStream.Length))
                    PhotoShopNode.Tag = ExifStream
                    PhotoShopNode.Text &= " " & PhotoShopStream.ToString
                    For Each BIM8 As Tools.Drawing.IO.JPEG.Photoshop8BIMReader In jpeg.Get8BIMSegments
                        Dim Node8 As TreeNode = PhotoShopNode.Nodes.Add(String.Format("8BIM {0} name '{1}' size {2}B", Hex(BIM8.Type), BIM8.Name, BIM8.DataSize))
                        Node8.Tag = BIM8
                        Node8.Text &= " " & BIM8.Data.ToString
                    Next BIM8
                End If
            End If
        End Sub
    End Class
End Namespace