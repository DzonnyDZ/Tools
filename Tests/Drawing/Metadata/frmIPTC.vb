Imports Tools.DrawingT.IO, Tools.DrawingT.MetadataT
Namespace DrawingT.MetadataT
    '#If Config <= Nightly Then Stage conditional compilation of this file is set in Tests.vbproj
    ''' <summary>Tests <see cref="Tools.DrawingT.IO.JPEG"/></summary>
    Public Class frmIPTC
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub
        ''' <summary>Runs test</summary>
        Public Shared Sub test()
            Dim frm As New frmIPTC
            frm.Show()
        End Sub
        Private IPTC As IPTC
        Private Sub frmIPTC_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If ofdIPTC.ShowDialog() = Windows.Forms.DialogResult.OK Then
                picImage.Load(ofdIPTC.FileName)
                Dim JPEG As New JPEG.JPEGReader(ofdIPTC.FileName)
                IPTC = New IPTC(JPEG)
                prgProperties.SelectedObject = IPTC
            Else
                Me.Close()
            End If
        End Sub
    End Class
End Namespace