Public Class frmLarge

    Private Sub frmLarge_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.LargeLocation = Me.Location
        My.Settings.LargeSize = Me.Size
        My.Settings.LargeState = Me.WindowState
    End Sub

    Private Sub frmLarge_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Location = My.Settings.LargeLocation
        Me.Size = My.Settings.LargeSize
        Me.WindowState = My.Settings.LargeState
        If Me.WindowState = FormWindowState.Normal Then
            Dim Intersects As Boolean = False
            For Each scr As Screen In Screen.AllScreens
                If scr.WorkingArea.IntersectsWith(Me.DesktopBounds) Then
                    Intersects = True
                    Exit For
                End If
            Next scr
            If Not Intersects Then Me.DesktopLocation = My.Computer.Screen.Bounds.Location
        End If
    End Sub
End Class