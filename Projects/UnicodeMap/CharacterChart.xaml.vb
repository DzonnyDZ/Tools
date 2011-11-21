Imports Tools.ExtensionsT, Tools.LinqT
Imports Tools
Imports Tools.CollectionsT.GenericT

''' <summary>A control that visualy displays Unicode characters</summary>
Public Class CharacterChart

    ''' <summary>CTor - creates a new instance of the <see cref="CharacterChart"/> class</summary>
    Public Sub New()
        InitializeComponent()
        ResetDataSource()
    End Sub
#Region "DataSource"
    ''' <summary>Gets or sets source of data to be shown in charmap</summary>
    Public Property DataSource() As CharsList
        <DebuggerStepThrough()> Get
            Return GetValue(DataSourceProperty)
        End Get
        <DebuggerStepThrough()> Set(ByVal value As CharsList)
            SetValue(DataSourceProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="DataSource"/> dependency property</summary>
    Public Shared ReadOnly DataSourceProperty As DependencyProperty =
                           DependencyProperty.Register("DataSource", GetType(CharsList), GetType(CharacterChart),
                           New FrameworkPropertyMetadata(New CharsRange, AddressOf OnDataSourceChanged))
    ''' <summary>Called when value of the <see cref="DataSource"/> property changes for any <see cref="CharacterChart "/></summary>
    ''' <param name="d">A <see cref="CharacterChart "/> <see cref="DataSource"/> has changed for</param>
    ''' <param name="e">Event arguments</param>
    ''' <exception cref="Tools.TypeMismatchException"><paramref name="d"/> is not <see cref="CharacterChart "/></exception>
    ''' <exception cref="ArgumentNullException"><paramref name="d"/> is null</exception>
    <DebuggerStepThrough()> _
    Private Shared Sub OnDataSourceChanged(ByVal d As System.Windows.DependencyObject, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        If d Is Nothing Then Throw New ArgumentNullException("d")
        If Not TypeOf d Is CharacterChart Then Throw New Tools.TypeMismatchException("d", d, GetType(CharacterChart))
        DirectCast(d, CharacterChart).OnDataSourceChanged(e)
    End Sub
    ''' <summary>Called whan value of the <see cref="DataSource"/> property changes</summary>
    ''' <param name="e">Event arguments</param>
    Protected Overridable Sub OnDataSourceChanged(ByVal e As DependencyPropertyChangedEventArgs)
        resetdatasource()
    End Sub
#End Region

#Region "ColumnCount"
    ''' <summary>Gets or sets number of columns displayed</summary>
    Public Property ColumnCount() As Integer
        <DebuggerStepThrough()> Get
            Return GetValue(ColumnCountProperty)
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Integer)
            SetValue(ColumnCountProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="ColumnCount"/> dependency property</summary>
    Public Shared ReadOnly ColumnCountProperty As DependencyProperty =
                           DependencyProperty.Register("ColumnCount", GetType(Integer), GetType(CharacterChart),
                           New FrameworkPropertyMetadata(16, AddressOf OnColumnCountChanged, AddressOf CoerceColumnCountValue))
    ''' <summary>Called when value of the <see cref="ColumnCount"/> property changes for any <see cref="characterchart"/></summary>
    ''' <param name="d">A <see cref="characterchart"/> <see cref="ColumnCount"/> has changed for</param>
    ''' <param name="e">Event arguments</param>
    ''' <exception cref="Tools.TypeMismatchException"><paramref name="d"/> is not <see cref="characterchart"/></exception>
    ''' <exception cref="ArgumentNullException"><paramref name="d"/> is null</exception>
    <DebuggerStepThrough()> _
    Private Shared Sub OnColumnCountChanged(ByVal d As System.Windows.DependencyObject, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        If d Is Nothing Then Throw New ArgumentNullException("d")
        If Not TypeOf d Is CharacterChart Then Throw New Tools.TypeMismatchException("d", d, GetType(CharacterChart))
        DirectCast(d, CharacterChart).OnColumnCountChanged(e)
    End Sub
    ''' <summary>Called whan value of the <see cref="ColumnCount"/> property changes</summary>
    ''' <param name="e">Event arguments</param>
    Protected Overridable Sub OnColumnCountChanged(ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        ResetDataSource()
    End Sub
    ''' <summary>Called whenever a value of the <see cref="ColumnCount"/> dependency property is being re-evaluated, or coercion is specifically requested.</summary>
    ''' <param name="d">The object that the property exists on. When the callback is invoked, the property system passes this value.</param>
    ''' <param name="baseValue">The new value of the property, prior to any coercion attempt.</param>
    ''' <returns>The coerced value (with appropriate type).</returns>
    ''' <exception cref="Tools.TypeMismatchException"><paramref name="d"/> is not of type <see cref="characterchart"/> -or- <paramref name="baseValue"/> is not of type <see cref="integer"/></exception>
    ''' <exception cref="ArgumentNullException"><paramref name="d"/> is null</exception>
    Private Shared Function CoerceColumnCountValue(ByVal d As System.Windows.DependencyObject, ByVal baseValue As Object) As Object
        If d Is Nothing Then Throw New ArgumentNullException("d")
        If Not TypeOf d Is CharacterChart Then Throw New Tools.TypeMismatchException("d", d, GetType(CharacterChart))
        If Not TypeOf baseValue Is Integer AndAlso Not baseValue Is Nothing Then Throw New Tools.TypeMismatchException("baseValue", baseValue, GetType(Integer))
        Return DirectCast(d, CharacterChart).CoerceColumnCountValue(baseValue)
    End Function
    ''' <summary>Called whenever a value of the <see cref="ColumnCount"/> dependency property is being re-evaluated, or coercion is specifically requested.</summary>
    ''' <param name="baseValue">The new value of the property, prior to any coercion attempt, but ensured to be of correct type.</param>
    ''' <returns>The coerced value (with appropriate type).</returns>
    Protected Overridable Function CoerceColumnCountValue(ByVal baseValue As Integer) As Integer
        If baseValue <= 0 Then Return 16
        Return baseValue
    End Function
#End Region

    ''' <summary>Resets source of data for main grid</summary>
    Private Sub ResetDataSource()
        dgChars.ItemsSource = New CharsSource(If(DataSource, CharsList.Empty)) With {.Columns = Me.ColumnCount}
    End Sub

End Class
