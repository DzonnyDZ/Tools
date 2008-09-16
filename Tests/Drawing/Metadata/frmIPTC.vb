Imports Tools.DrawingT.DrawingIOt, Tools.DrawingT.MetadataT
Namespace DrawingT.MetadataT.IptcT
    '#If Config <= Nightly Then Stage conditional compilation of this file is set in Tests.vbproj
    ''' <summary>Tests <see cref="Tools.DrawingT.DrawingIOt.JPEG"/></summary>
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
        Private IPTC As Tools.DrawingT.MetadataT.IptcT.Iptc
        Private JPEG As JPEG.JPEGReader

        Private Sub frmIPTC_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
            If JPEG IsNot Nothing Then JPEG.Dispose()
        End Sub
        Private Sub frmIPTC_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If ofdIPTC.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Try
                    picImage.Load(ofdIPTC.FileName)
                    JPEG = New JPEG.JPEGReader(ofdIPTC.FileName, True)
                    IPTC = New Tools.DrawingT.MetadataT.IptcT.Iptc(JPEG)
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                    Me.Close()
                    Exit Sub
                End Try
                prgProperties.SelectedObject = IPTC
            Else
                Me.Close()
            End If
        End Sub

        Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
            'Try
            JPEG.IPTCEmbed(IPTC.GetBytes)
            'Catch ex As Exception
            ' MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
            ' End Try
        End Sub
    End Class
End Namespace