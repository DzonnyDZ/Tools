''' <summary>Dialog for editing IPTC metadata</summary>
Public Class IptcEditor

    ''' <summary>CTor - creates a new instance of the <see cref="IptcEditor"/> class</summary>
    ''' <param name="iptc">An instance of IPTC metadata</param>
    ''' <exception cref="ArgumentNullException"><paramref name="iptc"/> is nulll</exception>
    Public Sub New(iptc As IptcInternal)
        If iptc Is Nothing Then Throw New ArgumentNullException("iptc")
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        DataContext = iptc
        prgIptc.SelectedObject = iptc
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
