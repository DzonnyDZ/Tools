Namespace Data
    ''' <summary>Edits connection string in <see cref="PropertyGrid"/></summary>
    Friend Class ConnectionStringEditor
        ''' <summary>CTor</summary>
        ''' <param name="ConnectionString">COnnection string to be edited</param>
        Public Sub New(ByVal ConnectionString As Common.DbConnectionStringBuilder)
            InitializeComponent()
            _ConnectionString = ConnectionString
            prgProperties.SelectedObject = ConnectionString
            txtConnectionString.Text = ConnectionString.ConnectionString
        End Sub
        ''' <summary>Contains value of the <see cref="ConnectionString"/> property</summary>
        Private ReadOnly _ConnectionString As Common.DbConnectionStringBuilder
        ''' <summary>Gets connection string being edited</summary>
        ''' <returns>Connection string being edited</returns>
        Public ReadOnly Property ConnectionString() As Common.DbConnectionStringBuilder
            Get
                Return _ConnectionString
            End Get
        End Property
        Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
            Me.Close()
        End Sub

        Private Sub prgProperties_PropertyValueChanged(ByVal s As System.Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles prgProperties.PropertyValueChanged
            txtConnectionString.Text = ConnectionString.ConnectionString
        End Sub
    End Class
End Namespace