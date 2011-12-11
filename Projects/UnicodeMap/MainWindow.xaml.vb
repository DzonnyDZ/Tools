Imports Tools.WindowsT.WPF.ConvertersT
Imports Tools.WindowsT.InteropT

''' <summary>Main window of the Charmap application</summary>
Class MainWindow

    Private Sub MainWindow_Loaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        cmbFont.ItemsSource = System.Windows.Media.Fonts.SystemFontFamilies

    End Sub


    Private Sub btnUnicode_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles btnUnicode.Click
        Dim dlg As New WindowsT.FormsT.PropertyDialog(From cp In cchChart.SelectedCodePoints Where cp.HasValue Select UnicodeCharacterDatabase.Default.FindCodePoint(cp.Value)) With {.Buttons = WindowsT.IndependentT.MessageBox.MessageBoxButton.Buttons.OK}
        dlg.ShowDialog(Me)
    End Sub

    Private Sub btnCSUR_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles btnCSUR.Click
        Dim dlg As New WindowsT.FormsT.PropertyDialog(From cp In cchChart.SelectedCodePoints Where cp.HasValue Let xcp = UnicodeCharacterDatabase.Default.FindCodePoint(cp.Value) Select xcp.Csur Where Csur IsNot Nothing) With {.Buttons = WindowsT.IndependentT.MessageBox.MessageBoxButton.Buttons.OK}
        dlg.ShowDialog(Me)
    End Sub
End Class
