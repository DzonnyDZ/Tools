''' <summary>Dialog for editing rating</summary>
Public Class RatingEditor
    ''' <summary>CTor - creates a new instance of the <see cref="RatingEditor"/> class</summary>
    ''' <param name="iptc">And IPTC metadata which contains the rating</param>
    Public Sub New(iptc As IptcInternal)
        InitializeComponent()
        DataContext = iptc
    End Sub

    Private Sub btnOK_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnOK.Click
        Me.DialogResult = True
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btnCancel.Click
        Me.DialogResult = False
        Me.Close()
    End Sub
End Class
