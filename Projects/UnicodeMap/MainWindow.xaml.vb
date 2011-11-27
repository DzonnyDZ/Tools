''' <summary>Main window of the Charmap application</summary>
Class MainWindow

    Private Sub MainWindow_Loaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        cmbFont.ItemsSource = System.Windows.Media.Fonts.SystemFontFamilies
    End Sub
End Class
