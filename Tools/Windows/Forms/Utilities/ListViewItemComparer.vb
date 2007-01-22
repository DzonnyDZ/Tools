Imports System.Windows.Forms
#If Config <= Release Then
Namespace Windows.Forms.Utilities
    ''' <summary>Implements the manual sorting of items in <see cref="ListView"/> by columns.</summary>
    ''' <remarks><seealso>http://msdn2.microsoft.com/en-us/library/system.windows.forms.listview.listviewitemsorter.aspx</seealso></remarks>
    Public NotInheritable Class ListViewItemComparer : Implements IComparer
        ''' <summary>Contains value of the <see cref="Column"/> property</summary>
        Private _Column As Integer = 0
        ''' <summary>Contains value of the <see cref="Descending"/> property</summary>
        Private _Descending As Boolean = False
        ''' <summary>CTor with default <see cref="Column"/> 0</summary>
        <DebuggerStepThrough()> Public Sub New()
        End Sub

        ''' <summary>CTor</summary>
        ''' <param name="column">Sort column index</param>
        ''' <param name="Descending">Indicates reversed order of sorting</param>
        <DebuggerStepThrough()> Public Sub New(ByVal column As Integer, Optional ByVal Descending As Boolean = False)
            Me.Column = column
            Me.Descending = Descending
        End Sub

        ''' <summary>Gets or sets sort column index</summary>
        ''' <remarks>Columns are index in zero-based way including first column</remarks>
        <DefaultValue(0I)> Public Property Column() As Integer
            <DebuggerStepThrough()> Get
                Return _Column
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Integer)
                _Column = value
            End Set
        End Property
        ''' <summary>Gets or sets if sorting order is reversed</summary>
        <DefaultValue(False)> _
        Public Property Descending() As Boolean
            <DebuggerStepThrough()> Get
                Return _Descending
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Boolean)
                _Descending = value
            End Set
        End Property
        ''' <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
        ''' <param name="y">The second object to compare.</param>
        ''' <param name="x">The first object to compare.</param>
        ''' <returns>Value Condition Less than zero x is less than y. Zero x equals y. Greater than zero x is greater than y.</returns>
        ''' <exception cref="System.ArgumentException">Either <paramref name="x"/> or <paramref name="y"/> is not <see cref="ListViewItem"/></exception>
        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            Dim liX, liY As ListViewItem
            Try
                liX = x
                liY = y
            Catch ex As Exception
                Throw New ArgumentException("Conversion to ListViewItemFailed", ex)
            End Try
            Dim strX As String, strY As String
            If Column = 0 Then
                strX = liX.Text
            Else
                If liX.SubItems.Count >= Column AndAlso liX.SubItems(Column - 1) IsNot Nothing Then
                    strX = liX.SubItems(Column - 1).Text
                Else
                    strX = ""
                End If
            End If
            If Column = 0 Then
                strY = liY.Text
            Else
                If liY.SubItems.Count >= Column AndAlso liY.SubItems(Column - 1) IsNot Nothing Then
                    strY = liY.SubItems(Column - 1).Text
                Else
                    strY = ""
                End If
            End If
            If Descending Then
                Return -String.Compare(strX, strY)
            Else
                Return String.Compare(strX, strY)
            End If
        End Function
    End Class
End Namespace
#End If