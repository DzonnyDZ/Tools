''' <summary>Dialog pto vykreslení do obrázku</summary>
''' <remarks>Nìkteré události handlovány v <see cref="frmMain"/></remarks>
Public Class frmImage
    Private Sub cmdKO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdKO.Click
        Me.Close()
    End Sub

    Private Sub frmImage_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        My.Computer.Clipboard.SetImage(picMain.BackgroundImage)
    End Sub
End Class