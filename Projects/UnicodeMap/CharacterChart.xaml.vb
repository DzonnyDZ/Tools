Imports Tools.ExtensionsT, Tools.LinqT
Imports Tools, Tools.WindowsT.WPF
Imports Tools.CollectionsT.GenericT
Imports System.ComponentModel

''' <summary>A control that visualy displays Unicode characters</summary>
''' <remarks>This class implements the <see cref="INotifyPropertyChanged"/> interface. However only some non-dependency properties such as <see cref="CharacterChart.SelectedCodePoints"/> changes are reported via this interface. Dependency properties has their own mechanism for reporting changes.</remarks>
Public Class CharacterChart
    Implements INotifyPropertyChanged

    ''' <summary>CTor - creates a new instance of the <see cref="CharacterChart"/> class</summary>
    Public Sub New()
        InitializeComponent()
        SetBinding(ChartFontFamilyProperty, New Binding() With {.RelativeSource = New RelativeSource(RelativeSourceMode.Self), .Path = New PropertyPath(FontFamilyProperty.Name)})
        SetBinding(ChartFontSizeProperty, New Binding() With {.RelativeSource = New RelativeSource(RelativeSourceMode.Self), .Path = New PropertyPath(FontSizeProperty.Name)})
        SetBinding(ChartFontStretchProperty, New Binding() With {.RelativeSource = New RelativeSource(RelativeSourceMode.Self), .Path = New PropertyPath(FontStretchProperty.Name)})
        SetBinding(ChartFontStyleProperty, New Binding() With {.RelativeSource = New RelativeSource(RelativeSourceMode.Self), .Path = New PropertyPath(FontStyleProperty.Name)})
        SetBinding(ChartFontWeightProperty, New Binding() With {.RelativeSource = New RelativeSource(RelativeSourceMode.Self), .Path = New PropertyPath(FontWeightProperty.Name)})
    End Sub

    Private Sub CharacterChart_Loaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        MaxHeight = 1024
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
                           New FrameworkPropertyMetadata(UnicodeCharacterDatabase.Default))
#End Region

#Region "SelectedCodePoint"
    ''' <summary>Gets currently selected and active code point (just one)</summary>
    ''' <remarks>If property value is null either no cell is selected or an empty cell is seleccted</remarks>
    Public ReadOnly Property SelectedCodePoint As UInteger?
        Get
            Return GetValue(CharacterChart.SelectedCodePointProperty)
        End Get
    End Property
    ''' <summary>Key of the <see cref="SelectedCodePoint"/> dependency property</summary>
    Private Shared ReadOnly SelectedCodePointPropertyKey As DependencyPropertyKey = _
                            DependencyProperty.RegisterReadOnly("SelectedCodePoint", _
                            GetType(UInteger?), GetType(CharacterChart), _
                            New FrameworkPropertyMetadata(New UInteger?))
    ''' <summary>Metadata of the <see cref="SelectedCodePoint"/> dependency property</summary>
    Public Shared ReadOnly SelectedCodePointProperty As DependencyProperty = _
                           SelectedCodePointPropertyKey.DependencyProperty
#End Region

#Region "ChartFontFamily"
    ''' <summary>Gets or sets font familly used for displayed characters</summary>      
    Public Property ChartFontFamily As FontFamily
        Get
            Return GetValue(ChartFontFamilyProperty)
        End Get
        Set(ByVal value As FontFamily)
            SetValue(ChartFontFamilyProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="ChartFontFamily"/> dependency property</summary>                                                   
    Public Shared ReadOnly ChartFontFamilyProperty As DependencyProperty = DependencyProperty.Register(
        "ChartFontFamily", GetType(FontFamily), GetType(CharacterChart), New FrameworkPropertyMetadata(FontFamilyProperty.GetMetadata(GetType(CharacterChart)).DefaultValue))
#End Region

#Region "ChartFontSize"
    ''' <summary>Gets or sets size of font used for displayed characters</summary>      
    Public Property ChartFontSize As Double
        Get
            Return GetValue(ChartFontSizeProperty)
        End Get
        Set(value As Double)
            SetValue(ChartFontSizeProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="ChartFontSize"/> dependency property</summary>                                                   
    Public Shared ReadOnly ChartFontSizeProperty As DependencyProperty = DependencyProperty.Register(
        "ChartFontSize", GetType(Double), GetType(CharacterChart), New FrameworkPropertyMetadata(FontSizeProperty.GetMetadata(GetType(CharacterChart)).DefaultValue))
#End Region

#Region "ChartFontStretch"
    ''' <summary>Gets or sets font stretch used for displayed characters</summary>      
    Public Property ChartFontStretch As FontStretch
        Get
            Return GetValue(ChartFontStretchProperty)
        End Get
        Set(ByVal value As FontStretch)
            SetValue(ChartFontStretchProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="ChartFontStretch"/> dependency property</summary>                                                   
    Public Shared ReadOnly ChartFontStretchProperty As DependencyProperty = DependencyProperty.Register(
        "ChartFontStretch", GetType(FontStretch), GetType(CharacterChart), New FrameworkPropertyMetadata(FontStretchProperty.GetMetadata(GetType(CharacterChart)).DefaultValue))
#End Region

#Region "ChartFontStyle"
    ''' <summary>Gets or sets font style used for displayed characters</summary>      
    Public Property ChartFontStyle As FontStyle
        Get
            Return GetValue(ChartFontStyleProperty)
        End Get
        Set(ByVal value As FontStyle)
            SetValue(ChartFontStyleProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="ChartFontStyle"/> dependency property</summary>                                                   
    Public Shared ReadOnly ChartFontStyleProperty As DependencyProperty = DependencyProperty.Register(
        "ChartFontStyle", GetType(FontStyle), GetType(CharacterChart), New FrameworkPropertyMetadata(FontStyleProperty.GetMetadata(GetType(CharacterChart)).DefaultValue))
#End Region

#Region "ChartFontWeight"
    ''' <summary>Gets or sets font weight used for displayed characters</summary>      
    Public Property ChartFontWeight As FontWeight
        Get
            Return GetValue(ChartFontWeightProperty)
        End Get
        Set(ByVal value As FontWeight)
            SetValue(ChartFontWeightProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="ChartFontWeight"/> dependency property</summary>                                                   
    Public Shared ReadOnly ChartFontWeightProperty As DependencyProperty = DependencyProperty.Register(
        "ChartFontWeight", GetType(FontWeight), GetType(CharacterChart), New FrameworkPropertyMetadata(FontWeightProperty.GetMetadata(GetType(CharacterChart)).DefaultValue))
#End Region

    Private _selectedCodePoints As UInteger?() = Nothing
    ''' <summary>Gets array of currently selected code-points</summary>
    ''' <remarks>Changes of this non-dependency property are reported via <see cref="INotifyPropertyChanged"/>/<see cref="PropertyChanged"/>.
    ''' <para>If araray returned contains null values, empty cells are selected</para></remarks>
    Public ReadOnly Property SelectedCodePoints As UInteger?()
        Get
            If _selectedCodePoints Is Nothing Then
                Dim arr(0 To dgChars.SelectedCells.Count - 1) As Nullable(Of UInteger)
                Dim i% = 0
                For Each c In dgChars.SelectedCells
                    arr(i) = GetCodePointFromCell(c)
                    i += 1
                Next
                _selectedCodePoints = arr
            End If
            Return _selectedCodePoints
        End Get
    End Property


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

    Private Sub dgChars_CurrentCellChanged(sender As Object, e As System.EventArgs) Handles dgChars.CurrentCellChanged
        SetValue(SelectedCodePointPropertyKey, GetCodePointFromCell(dgChars.CurrentCell))
        OnSelectedCodePointChanged(e)
    End Sub

    Private Sub dgChars_SelectedCellsChanged(sender As Object, e As System.Windows.Controls.SelectedCellsChangedEventArgs) Handles dgChars.SelectedCellsChanged
        _selectedCodePoints = Nothing
        OnSelectedCodePointsChanged(e)
    End Sub

    ''' <summary>Gets conde-point value form cell selected</summary>
    Protected Function GetCodePointFromCell(cell As DataGridCellInfo) As UInteger?
        Dim line As CharsLine = cell.Item
        Return line(dgChars.Columns.IndexOf(cell.Column))
    End Function

    ''' <summary>Raises the <see cref="PropertyChanged"/> event</summary>
    ''' <param name="e">Event arguments</param>
    Protected Overridable Overloads Sub OnPropertyChanged(e As PropertyChangedEventArgs)
        RaiseEvent PropertyChanged(Me, e)
    End Sub

    ''' <summary>Raises the <see cref="PropertyChanged"/> event</summary>
    ''' <param name="propertyName">Name of changed property</param>
    Protected Overloads Sub OnPropertyChanged(propertyName$)
        OnPropertyChanged(New PropertyChangedEventArgs(propertyName))
    End Sub

    ''' <summary>Occurs when a property value changes.</summary>
    ''' <remarks>This class reports only changes of certain non-dependency properties such as <see cref="SelectedCodePoints"/> via this property</remarks>
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    ''' <summary>Raised when value of the <see cref="SelectedCodePoint"/> property changes</summary>
    Public Event SelectedCodePointChanged As EventHandler
    ''' <summary>Raised when value of the <see cref="SelectedCodePoints"/> property changes</summary>
    Public Event SelectedCodePointsChanged As EventHandler

    ''' <summary>Raises the <see cref="SelectedCodePointChanged"/> event</summary>
    ''' <param name="e">Event arrguments</param>
    Protected Overridable Sub OnSelectedCodePointChanged(e As EventArgs)
        RaiseEvent SelectedCodePointChanged(Me, e)
    End Sub
    ''' <summary>Raises the <see cref="SelectedCodePointChanged"/> event</summary>
    ''' <param name="e">Event arrguments</param>
    Protected Overridable Sub OnSelectedCodePointsChanged(e As EventArgs)
        OnPropertyChanged("SelectedCodePoints")
        RaiseEvent SelectedCodePointsChanged(Me, e)
    End Sub
End Class