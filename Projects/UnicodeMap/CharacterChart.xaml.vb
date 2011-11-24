Imports Tools.ExtensionsT, Tools.LinqT
Imports Tools
Imports Tools.CollectionsT.GenericT

''' <summary>A control that visualy displays Unicode characters</summary>
Public Class CharacterChart

    ''' <summary>CTor - creates a new instance of the <see cref="CharacterChart"/> class</summary>
    Public Sub New()
        InitializeComponent()
        ResetDataSource()
        ApplyColumns()
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
        ResetDataSource()
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
        ApplyColumns()
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

#Region "ColumnWidth"
    ''' <summary>Gets or sets width of column for characters</summary>      
    Public Property ColumnWidth As DataGridLength
        Get
            Return GetValue(ColumnWidthProperty)
        End Get
        Set(value As DataGridLength)
            SetValue(ColumnWidthProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="ColumnWidth"/> dependency property</summary>                                                   
    Public Shared ReadOnly ColumnWidthProperty As DependencyProperty = DependencyProperty.Register(
        "ColumnWidth", GetType(DataGridLength), GetType(CharacterChart), New FrameworkPropertyMetadata(New DataGridLength(1, DataGridLengthUnitType.Star)))
#End Region

#Region "MinRowHeight"
    ''' <summary>Gets or sets row height in grid</summary>      
    Public Property RowHeight As Double
        Get
            Return GetValue(RowHeightProperty)
        End Get
        Set(ByVal value As Double)
            SetValue(RowHeightProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="RowHeight"/> dependency property</summary>                                                   
    Public Shared ReadOnly RowHeightProperty As DependencyProperty = DependencyProperty.Register(
        "RowHeight", GetType(Double), GetType(CharacterChart), New FrameworkPropertyMetadata(32.0#))
#End Region



#Region "NameSource"
    ''' <summary>Gets or sets object that provides names of characters</summary>
    Public Property NameSource() As ICharNameProvider
        <DebuggerStepThrough()> Get
            Return GetValue(NameSourceProperty)
        End Get
        <DebuggerStepThrough()> Set(ByVal value As ICharNameProvider)
            SetValue(NameSourceProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="NameSource"/> dependency property</summary>
    Public Shared ReadOnly NameSourceProperty As DependencyProperty =
                           DependencyProperty.Register("NameSource", GetType(ICharNameProvider), GetType(CharacterChart),
                           New FrameworkPropertyMetadata(UnicodeCharacterDatabase.Default, AddressOf OnNameSourceChanged))
    ''' <summary>Called when value of the <see cref="NameSource"/> property changes for any <see cref="characterchart"/></summary>
    ''' <param name="d">A <see cref="characterchart"/> <see cref="NameSource"/> has changed for</param>
    ''' <param name="e">Event arguments</param>
    ''' <exception cref="Tools.TypeMismatchException"><paramref name="d"/> is not <see cref="characterchart"/></exception>
    ''' <exception cref="ArgumentNullException"><paramref name="d"/> is null</exception>
    <DebuggerStepThrough()> _
    Private Shared Sub OnNameSourceChanged(ByVal d As System.Windows.DependencyObject, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        If d Is Nothing Then Throw New ArgumentNullException("d")
        If Not TypeOf d Is CharacterChart Then Throw New Tools.TypeMismatchException("d", d, GetType(CharacterChart))
        DirectCast(d, CharacterChart).OnNameSourceChanged(e)
    End Sub
    ''' <summary>Called whan value of the <see cref="NameSource"/> property changes</summary>
    ''' <param name="e">Event arguments</param>
    Protected Overridable Sub OnNameSourceChanged(ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        SetValue(NameSourceInternalPropertyKey, New NameSourceWrapper(NameSource))
    End Sub
#End Region


#Region "NameSourceInternal"
    ''' <summary>Gets internal name source - <see cref="NameSource"/> wrapped as <see cref="NameSourceWrapper"/></summary>
    Friend ReadOnly Property NameSourceInternal As NameSourceWrapper
        Get
            Return GetValue(CharacterChart.NameSourceInternalProperty)
        End Get
    End Property

    ''' <summary>Key of the <see cref="NameSourceInternal"/> dependency property</summary>
    Private Shared ReadOnly NameSourceInternalPropertyKey As DependencyPropertyKey = _
                            DependencyProperty.RegisterReadOnly("NameSourceInternal", _
                            GetType(NameSourceWrapper), GetType(CharacterChart), _
                            New FrameworkPropertyMetadata(New NameSourceWrapper(UnicodeCharacterDatabase.Default)))

    ''' <summary>Metadata of the <see cref="NameSourceInternal"/> dependency property</summary>
    Friend Shared ReadOnly NameSourceInternalProperty As DependencyProperty = NameSourceInternalPropertyKey.DependencyProperty
#End Region

    ''' <summary>Resets source of data for main grid</summary>
    Private Sub ResetDataSource()
        dgChars.ItemsSource = New CharsSource(If(DataSource, CharsList.Empty)) With {.Columns = Me.ColumnCount}
    End Sub
    ''' <summary>Applies changes of the <see cref="ColumnCount"/> property</summary>
    Private Sub ApplyColumns()
        dgChars.BeginInit()
        Try
            dgChars.Columns.Clear()
            For i = 0 To ColumnCount - 1
                'http://stackoverflow.com/questions/613158/programmatically-create-wpf-datagridtemplatecolumn-for-datagrid
                Dim factory = New FrameworkElementFactory(GetType(CharPresenter))
                factory.SetValue(CharPresenter.CodePointProperty, New Binding(NumericsT.ConversionsT.Dec2Xxx(i, ColumnCount)))
                Dim col = New DataGridTemplateColumn() With {
                    .Header = NumericsT.ConversionsT.Dec2Xxx(i, ColumnCount),
                    .CellTemplate = New DataTemplate() With {.VisualTree = factory},
                    .Width = ColumnWidth,
                    .IsReadOnly = True
                }
                dgChars.Columns.Add(col)
            Next
        Finally
            dgChars.EndInit()
        End Try
    End Sub

End Class

''' <summary>Wraps <see cref="ICharNameProvider"/> so it can be used in WPF finding</summary>
Friend Class NameSourceWrapper
    Implements ICharNameProvider
    ''' <summary>Wrapped instance, can be null</summary>
    Private wraps As ICharNameProvider
    ''' <summary>CTor - creates a new instance of the <see cref="ICharNameProvider"/> class</summary>
    ''' <param name="wrap">INstance to wrap. Can be null. In such case all character names will be null.</param>
    Public Sub New(wrap As ICharNameProvider)
        wraps = wrap
    End Sub
    ''' <summary>Gets name of a character</summary>
    ''' <param name="codePoint">A Unicode (UTF-32) code-point</param>
    ''' <returns>Name of the character, nulll of the source is not capable of providing character name</returns>
    ''' <exception cref="ArgumentOutOfRangeException"><paramref name="codePoint"/> is less than zero or greater than <see cref="UnicodeCharacterDatabase.MaxCodePoint"/>.</exception>
    Public Function GetName(codePoint As Integer) As String Implements ICharNameProvider.GetName
        If wraps Is Nothing Then Return Nothing
        Return wraps.GetName(codePoint)
    End Function
    ''' <summary>Gets name of a character</summary>
    ''' <param name="codePoint">A Unicode (UTF-32) code-point</param>
    ''' <returns>Name of the character, nulll of the source is not capable of providing character name</returns>
    ''' <exception cref="ArgumentOutOfRangeException"><paramref name="codePoint"/> is greater than <see cref="UnicodeCharacterDatabase.MaxCodePoint"/>.</exception>
    <CLSCompliant(False)>
    Default Public ReadOnly Property Names(codePoint As UInteger) As String
        Get
            If codePoint > UnicodeCharacterDatabase.MaxCodePoint Then Throw New ArgumentOutOfRangeException("codePoint")
            Return GetName(codePoint)
        End Get
    End Property
    ''' <summary>Gets name of a character</summary>
    ''' <param name="codePoint">A Unicode (UTF-32) code-point</param>
    ''' <returns>Name of the character, nulll of the source is not capable of providing character name</returns>
    ''' <exception cref="ArgumentOutOfRangeException"><paramref name="codePoint"/> is less than zero or greater than <see cref="UnicodeCharacterDatabase.MaxCodePoint"/>.</exception>
    Default Public ReadOnly Property Names(codePoint As Integer) As String
        Get
            Return GetName(codePoint)
        End Get
    End Property
End Class
