''' <summary>Dialog pto vykreslen� do obr�zku</summary>
''' <remarks>N�kter� ud�losti handlov�ny v <see cref="frmMain"/></remarks>
Public Class frmImage
    Private Sub cmdKO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdKO.Click
        Me.Close()
    End Sub

    Private Sub frmImage_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        My.Computer.Clipboard.SetImage(picMain.BackgroundImage)
    End Sub
End Class