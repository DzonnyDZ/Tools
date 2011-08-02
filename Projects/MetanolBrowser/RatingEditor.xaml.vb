''' <summary>Dialog for editing rating</summary>
Public Class RatingEditor
    ''' <summary>CTor - creates a new instance of the <see cref="RatingEditor"/> class</summary>
    ''' <param name="iptc">And IPTC metadata which contains the rating</param>
    Public Sub New(iptc As IptcInternal)
        InitializeComponent()
        DataContext = iptc
    End Sub
End Class
