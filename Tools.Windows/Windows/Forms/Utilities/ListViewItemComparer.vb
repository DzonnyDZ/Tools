Imports System.Windows.Forms, System.ComponentModel
Imports Tools.ComponentModelT

#If Config <= Release Then
Namespace WindowsT.FormsT.UtilitiesT
    ''' <summary>Implements the manual sorting of items in <see cref="ListView"/> by columns.</summary>
    ''' <remarks>
    ''' <seealso>http://msdn2.microsoft.com/en-us/library/system.windows.forms.listview.listviewitemsorter.aspx</seealso>
    ''' </remarks>
    Public NotInheritable Class ListViewItemComparer : Implements IComparer, IComparer(Of ListViewItem), IReportsChange
        ''' <summary>Possible ways of sorting</summary>
        Public Enum SortModes
            ''' <summary>Sort items as <see cref="String"/></summary>
            [String]
            ''' <summary>Sort items as numbers (<see cref="Decimal"/> is used)</summary>
            [Numeric]
            ''' <summary>Sor as <see cref="String"/> filled on left side by zeros ("0") to specified width</summary>
            [Zerofill]
        End Enum
        ''' <summary>Contains value of the <see cref="Column"/> property</summary>
        Private _Column As Integer = 0
        ''' <summary>Contains value of the <see cref="Descending"/> property</summary>
        Private _Descending As Boolean = False
        ''' <summary>Contains value of the <see cref="SortMode"/> property</summary>
        Private _SortMode As SortModes
        ''' <summary>Contains value of the <see cref="ZerofillWidth"/> property</summary>
        Private _ZerofillWidth As Byte

        ''' <summary>CTor with default <see cref="Column"/> 0</summary>
        <DebuggerStepThrough()> Public Sub New()
        End Sub

        ''' <summary>CTor</summary>
        ''' <param name="column">Sort column index</param>
        ''' <param name="Descending">Indicates reversed order of sorting</param>
        <DebuggerStepThrough()> Public Sub New(ByVal column As Integer, Optional ByVal Descending As Boolean = False)
            Me._Column = column
            Me._Descending = Descending
        End Sub
        ''' <summary>Defines fill width applicable when <see cref="SortMode"/> is <see cref="SortModes.Zerofill"/></summary>
        ''' <exception cref="ArgumentOutOfRangeException">Setting value less than zero</exception>
        ''' <remarks>When setting to value that differs from contemporary value the <see cref="Changed"/> event is raised</remarks>
        <NotifyParentProperty(True)> _
        <LDescription(GetType(ResourcesT.Components),"ZerofillWidth_d")> _
        <DefaultValue(CByte(0))> _
        Public Property ZerofillWidth() As Byte
            <DebuggerStepThrough()> Get
                Return _ZerofillWidth
            End Get
            Set(ByVal value As Byte)
                If value < 0 Then Throw New ArgumentOutOfRangeException("value", String.Format(ResourcesT.Exceptions.MustBeGreaterThanOrEqualToZero, "ZerofillWidth"))
                Dim raise As Boolean = value <> _ZerofillWidth
                _ZerofillWidth = value
                If raise Then RaiseEvent Changed(Me, EventArgs.Empty)
            End Set
        End Property

        ''' <summary>Gets or sets the way of sorting</summary>
        ''' <remarks>When setting to value that differs from contemporary value the <see cref="Changed"/> event is raised</remarks>
        <NotifyParentProperty(True)> _
        <LDescription(GetType(ResourcesT.Components), "SortMode_d")> _
        <DefaultValue(GetType(SortModes), "String")> _
        Public Property SortMode() As SortModes
            <DebuggerStepThrough()> Get
                Return _SortMode
            End Get
            Set(ByVal value As SortModes)
                Dim raise As Boolean = value <> _SortMode
                _SortMode = value
                If raise Then RaiseEvent Changed(Me, EventArgs.Empty)
            End Set
        End Property

        ''' <summary>Gets or sets sort column index</summary>
        ''' <remarks><para>Columns are index in zero-based way including first column. If column is out of range no exceprion is thrown but no sorting done. You can set column to e.g. -1 to avoid sorting.</para>
        ''' <para>When setting to value that differs from contemporary value the <see cref="Changed"/> event is raised.</para></remarks>
        <NotifyParentProperty(True)> _
        <LDescription(GetType(ResourcesT.Components), "Column_d")> _
        <DefaultValue(0I)> Public Property Column() As Integer
            <DebuggerStepThrough()> Get
                Return _Column
            End Get
            Set(ByVal value As Integer)
                Dim raise As Boolean = value <> _Column
                _Column = value
                If raise Then RaiseEvent Changed(Me, EventArgs.Empty)
            End Set
        End Property
        ''' <summary>Gets or sets if sorting order is reversed</summary>
        ''' <remarks>When setting to value that differs from contemporary value the <see cref="Changed"/> event is raised</remarks>
        <NotifyParentProperty(True)> _
        <DefaultValue(False), LDescription(GetType(ResourcesT.Components),"Descending_d")> _
        Public Property Descending() As Boolean
            <DebuggerStepThrough()> Get
                Return _Descending
            End Get
            Set(ByVal value As Boolean)
                Dim raise As Boolean = value <> _Descending
                _Descending = value
                If raise Then RaiseEvent Changed(Me, [EventArgs].Empty)
            End Set
        End Property
        ''' <summary>Sets <see cref="Column"/> and <see cref="Descending"/> properties at one step and prevents multiple raiseing of the <see cref="Changed"/> event</summary>
        ''' <param name="Column">New value for the <see cref="Column"/> property</param>
        ''' <param name="Descending">New value for the <see cref="Descending"/> property</param>
        ''' <param name="SortMode">New value for the <see cref="SortMode"/> property</param>
        ''' <param name="ZeroFillWidth">New value for the <see cref="ZerofillWidth"/> property</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="ZeroFillWidth"/> is less than zero</exception>
        ''' <remarks>In case some of parameters differs from value of corresponding property the <see cref="Changed"/> event is raised</remarks>
        Public Sub [Set](ByVal Column As Integer, Optional ByVal Descending As Boolean = False, Optional ByVal SortMode As SortModes = SortModes.String, Optional ByVal ZeroFillWidth As Integer = 0)
            If ZeroFillWidth < 0 Then Throw New ArgumentOutOfRangeException("Zerofillwidth", String.Format(ResourcesT.Exceptions.MustBeGreaterThanOrEqualToZero, "ZerofillWidth"))
            Dim raise As Boolean = Column <> _Column OrElse Descending <> _Descending OrElse SortMode <> _SortMode OrElse ZeroFillWidth <> ZeroFillWidth
            _Column = Column
            _Descending = Descending
            _SortMode = SortMode
            _ZerofillWidth = ZeroFillWidth
            If raise Then RaiseEvent Changed(Me, EventArgs.Empty)
        End Sub
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
                Throw New ArgumentException(String.Format(ResourcesT.Exceptions.ConversionTo0Failed, "ListViewItem"), ex)
            End Try
            Return Compare(liX, liY)
        End Function
        ''' <summary>Compares two <see cref="ListViewItemComparer"/>s</summary>
        ''' <param name="a">Item to compare</param>
        ''' <param name="b">Item to compare</param>
        ''' <returns>True if both <see cref="ListViewItemComparer.Column"/>, <see cref="ListViewItemComparer.Descending"/>, <see cref="ListViewItemComparer.SortMode"/> and <see cref="ListViewItemComparer.ZerofillWidth"/> are equal or both <paramref name="a"/> and <paramref name="b"/> are null. Otherwise False.</returns>
        Public Shared Operator =(ByVal a As ListViewItemComparer, ByVal b As ListViewItemComparer) As Boolean
            If a Is Nothing AndAlso b Is Nothing Then
                Return True
            ElseIf a Is Nothing OrElse b Is Nothing Then
                Return False
            Else
                Return a.Column = b.Column AndAlso a.Descending = b.Descending AndAlso a.SortMode = b.SortMode AndAlso a.ZerofillWidth = b.ZerofillWidth
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
                If Column >= 0 AndAlso liX.SubItems.Count >= Column AndAlso liX.SubItems(Column) IsNot Nothing Then
                    strX = liX.SubItems(Column).Text
                Else
                    strX = ""
                End If
            End If
            If Column = 0 Then
                strY = liY.Text
            Else
                If liY.SubItems.Count >= Column AndAlso liY.SubItems(Column) IsNot Nothing Then
                    strY = liY.SubItems(Column).Text
                Else
                    strY = ""
                End If
            End If
            '#If Framework >= 3.5 Then
            Dim mul As Integer = If(Descending, -1, 1)
            '#Else
            '            Dim mul As Integer = Tools.VisualBasicT.iif(Descending, -1, 1)
            '#End If
            Select Case SortMode
                Case SortModes.Numeric
                    If strX = "" Or strY = "" Then Return mul * String.Compare(strX, strY)
                    Try : Return mul * Decimal.Compare(CDec(strX), CDec(strY))
                    Catch : Return mul * String.Compare(strX, strY)
                    End Try
                Case SortModes.Zerofill
                    Return mul * String.Compare(New String("0"c, Math.Max(0, ZerofillWidth - strX.Length)) & strX, New String("0"c, Math.Max(0, ZerofillWidth - strY.Length)) & strY)
                Case Else : Return mul * String.Compare(strX, strY)
            End Select
        End Function

        ''' <summary>Raised when value of member changes</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event information (always <see cref="[EventArgs].Empty"/> in this implementation</param>
        ''' <remarks>Raiser when value of <see cref="Column"/> or <see cref="Descending"/> changes. Not raised in CTor</remarks>
        Public Event Changed(ByVal sender As IReportsChange, ByVal e As System.EventArgs) Implements IReportsChange.Changed
    End Class
End Namespace
#End If