Namespace IOt.StreamTools
    '#If TrueStage conditional compilation of this file is set in Tests.vbproj
    ''' <summary>Tests <see cref="Tools.DrawingT.DrawingIOt.JPEG"/></summary>
    Public Class frmInsertInto
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub
        ''' <summary>Runs test</summary>
        Public Shared Sub test()
            Dim frm As New frmInsertInto
            frm.Show()
        End Sub

        Private Sub cmdInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInsert.Click
            Dim stream As New IO.MemoryStream
            Dim Original As Byte() = System.Text.Encoding.Default.GetBytes(txtBefore.Text)
            stream.Write(Original, 0, Original.Length)
            stream.SetLength(Original.Length)
            Dim insert As Byte() = System.Text.Encoding.Default.GetBytes(txtInsert.Text)
            Try
                Tools.IOt.StreamTools.InsertInto(stream, nudPosition.Value, nudBytesToreplace.Value, insert)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                txtAfter.Text = ""
                Exit Sub
            End Try
            Dim After(stream.Length) As Byte
            stream.Seek(0, IO.SeekOrigin.Begin)
            stream.Read(After, 0, After.Length)
            txtAfter.Text = System.Text.Encoding.Default.GetString(After)
        End Sub
        
    End Class
End Namespace