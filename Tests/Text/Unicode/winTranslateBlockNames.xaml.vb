Imports System.Windows, System.Linq

Namespace TextT.UnicodeT
    Public Class winTranslateBlockNames
        Inherits Window

        Friend Sub New(data As IEnumerable(Of TranslateData))
            InitializeComponent()
            dgData.ItemsSource = data.ToList
        End Sub

        Private Sub btnDone_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnDone.Click
            Me.DialogResult = True
            Me.Close()
        End Sub
    End Class
End Namespace