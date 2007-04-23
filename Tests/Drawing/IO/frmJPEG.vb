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
                Try
                    Dim jpeg As New Tools.Drawing.IO.JPEG.JPEGReader(ofdOpen.FileName)
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                End Try
            End If
        End Sub
    End Class
End Namespace