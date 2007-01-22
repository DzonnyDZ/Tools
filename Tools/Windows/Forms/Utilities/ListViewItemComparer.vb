Imports System.Windows.Forms, System.ComponentModel
#If Config <= Release Then
Namespace Windows.Forms.Utilities
    ''' <summary>Implements the manual sorting of items in <see cref="ListView"/> by columns.</summary>
    ''' <remarks>
    ''' <seealso>http://msdn2.microsoft.com/en-us/library/system.windows.forms.listview.listviewitemsorter.aspx</seealso>
    ''' </remarks>
    <Editor(GetType(ExpandableObjectConverter), GetType(Drawing.Design.UITypeEditor))> _
    Public NotInheritable Class ListViewItemComparer : Implements IComparer, IComparer(Of ListViewItem)
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
        ''' <remarks>Columns are index in zero-based way including first column. If column is out of range no exceprion is thrown but no sorting done. You can set column to e.g. -1 to avoid sorting.</remarks>
        <NotifyParentProperty(True)> _
        <Description("Gets or sets sort column index. Set this property to value out of range of columns (e.g. -1) to avoid sorting.")> _
        <DefaultValue(0I)> Public Property Column() As Integer
            <DebuggerStepThrough()> Get
                Return _Column
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Integer)
                _Column = value
            End Set
        End Property
        ''' <summary>Gets or sets if sorting order is reversed</summary>
        <NotifyParentProperty(True)> _
        <DefaultValue(False), Description("Gets or sets if sorting order is reversed")> _
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
        ''' <returns>Value Condition Less than zero <paramref name="x"/> is less than <paramref name="y"/>. Zero <paramref name="x"/> equals <paramref name="y"/>. Greater than zero <paramref name="x"/> is greater than <paramref name="y"/>.</returns>
        ''' <exception cref="System.ArgumentException">Either <paramref name="x"/> or <paramref name="y"/> is not <see cref="ListViewItem"/></exception>
        ''' <remarks>Internally uses type-safe overload of <see cref="Compare"/>. Use it instead.</remarks>
        <Obsolete("Use type-safe overload instead")> _
        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            Dim liX, liY As ListViewItem
            Try
                liX = x
                liY = y
            Catch ex As Exception
                Throw New ArgumentException("Conversion to ListViewItemFailed", ex)
            End Try
            Return Compare(liX, liY)
        End Function
        ''' <summary>Compares two <see cref="ListViewItemComparer"/>s</summary>
        ''' <param name="a">Item to compare</param>
        ''' <param name="b">Item to compare</param>
        ''' <returns>True if both <see cref="ListViewItemComparer.Column"/> and <see cref="ListViewItemComparer.Descending"/> are equal or both <paramref name="a"/> and <paramref name="b"/> are null. Otherwise False.</returns>
        Public Shared Operator =(ByVal a As ListViewItemComparer, ByVal b As ListViewItemComparer) As Boolean
            If a Is Nothing AndAlso b Is Nothing Then
                Return True
            ElseIf a Is Nothing OrElse b Is Nothing Then
                Return False
            Else
                Return a.Column = b.Column AndAlso a.Descending = b.Descending
            End If
        End Operator
        ''' <summary>Compares two <see cref="ListViewItemComparer"/>s</summary>
        ''' <param name="a">Item to compare</param>
        ''' <param name="b">Item to compare</param>
        ''' <returns>False if both <see cref="ListViewItemComparer.Column"/> and <see cref="ListViewItemComparer.Descending"/> are equal or both <paramref name="a"/> and <paramref name="b"/> are null. Otherwise True.</returns>
        Public Shared Operator <>(ByVal a As ListViewItemComparer, ByVal b As ListViewItemComparer) As Boolean
            Return Not a = b
        End Operator
        ''' <summary>Inverts <see cref="ListViewItemComparer"/>'s sort order</summary>
        ''' <param name="a">Original <see cref="ListViewItemComparer"/></param>
        ''' <returns>A new instance of <see cref="ListViewItemComparer"/> with <see cref="Descending"/> property set to negation of <see cref="Descending"/> property of <paramref name="a"/></returns>
        Public Shared Operator Not(ByVal a As ListViewItemComparer) As ListViewItemComparer
            Return New ListViewItemComparer(a.Column, Not a.Descending)
        End Operator
        ''' <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
        ''' <param name="liY">The second object to compare.</param>
        ''' <param name="liX">The first object to compare.</param>
        ''' <returns>Value Condition Less than zero <paramref name="liX"/> is less than <paramref name="liY"/>. Zero <paramref name="liX"/> equals <paramref name="liY"/>. Greater than zero <paramref name="liX"/> is greater than <paramref name="liY"/>.</returns>
        Public Function Compare(ByVal liX As System.Windows.Forms.ListViewItem, ByVal liY As System.Windows.Forms.ListViewItem) As Integer Implements System.Collections.Generic.IComparer(Of System.Windows.Forms.ListViewItem).Compare
            Dim strX As String, strY As String
            If Column = 0 Then
                strX = liX.Text
            Else
                If Column >= 0 AndAlso liX.SubItems.Count >= Column AndAlso liX.SubItems(Column - 1) IsNot Nothing Then
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