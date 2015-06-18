''' <summary>UI for setting up the screensaver</summary>
Public Class SettingsDialog

    Private Sub OK_Click(sender As Object, e As RoutedEventArgs)
        My.Settings.Save()
        Close()
    End Sub

    Private Sub Cancel_Click(sender As Object, e As RoutedEventArgs)
        My.Settings.Reload()
        Close()
    End Sub

    Private Sub SettingsDialog_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        DataContext = My.Settings
    End Sub
End Class
