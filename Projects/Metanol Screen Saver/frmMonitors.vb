Public Class frmMonitors
    Private Const PrevRatio As Integer = 10
    Private Sub frmMonitors_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim desktop As Rectangle = Rectangle.FromLTRB( _
            (From scr In Screen.AllScreens Select scr.Bounds.Left).Min, _
            (From scr In Screen.AllScreens Select scr.Bounds.Top).Min, _
            (From scr In Screen.AllScreens Select scr.Bounds.Right).Max, _
            (From scr In Screen.AllScreens Select scr.Bounds.Bottom).Max)
        Dim ScreenPoss(Screen.AllScreens.Length - 1) As Rectangle
        Dim i As Integer = 0
        panScreen.ClientSize = New Size(desktop.Width / PrevRatio, desktop.Height / PrevRatio)
        For Each ScreenSize In From Screen In Screen.AllScreens Select _
                               New Rectangle((Screen.Bounds.Left - desktop.Left) / PrevRatio, (Screen.Bounds.Top - desktop.Top) / PrevRatio, Screen.Bounds.Width / PrevRatio, Screen.Bounds.Height / PrevRatio)
            panScreen.Controls.Add(New Label With { _
                .Size = ScreenSize.Size, _
                .Location = ScreenSize.Location, _
                .Text = i + 1, _
                .TextAlign = ContentAlignment.MiddleCenter, _
                .BackColor = Color.Blue, _
                .BorderStyle = BorderStyle.FixedSingle, _
                .ForeColor = Color.Yellow})
            i += 1
        Next ScreenSize
        Me.ClientSize = panScreen.Size
    End Sub
End Class