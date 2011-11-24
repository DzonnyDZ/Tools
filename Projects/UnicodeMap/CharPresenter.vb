''' <summary>Represents single character in character map</summary>
Public Class CharPresenter
    Inherits Control
    ''' <summary>Type initiallizer - initializes the <see cref="CharPresenter"/> type</summary>
    Shared Sub New()
        DefaultStyleKeyProperty.OverrideMetadata(GetType(CharPresenter), New FrameworkPropertyMetadata(GetType(CharPresenter)))
    End Sub
    ''' <summary>CTor - creates a new instance of the <see cref="CharPresenter"/> property</summary>
    Public Sub New()
        SetBinding(ChartProperty, New Binding() With {
                   .RelativeSource = New RelativeSource(RelativeSourceMode.FindAncestor) With {.AncestorType = GetType(CharacterChart)},
                   .Mode = BindingMode.OneWay
               })
    End Sub


#Region "CodePoint"
    ''' <summary>Gets or sets code-point code of displayed code-point (character)</summary>
    ''' <remarks>This property is not CLS-compliant. CLS-compliant alternative is <see cref="Character"/>.</remarks>
    <CLSCompliant(False)>
    Public Property CodePoint() As UInteger
        <DebuggerStepThrough()> Get
            Return GetValue(CodePointProperty)
        End Get
        <DebuggerStepThrough()> Set(ByVal value As UInteger)
            If value > UnicodeCharacterDatabase.MaxCodePoint Then Throw New ArgumentOutOfRangeException("Code point out of Unicode range.")
            SetValue(CodePointProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="CodePoint"/> dependency property</summary>
    <CLSCompliant(False)>
    Public Shared ReadOnly CodePointProperty As DependencyProperty =
                           DependencyProperty.Register("CodePoint", GetType(UInteger), GetType(CharPresenter),
                           New FrameworkPropertyMetadata(0UI, AddressOf OnCodePointChanged, AddressOf CoerceCodePointValue))
    ''' <summary>Called when value of the <see cref="CodePoint"/> property changes for any <see cref="charpresenter"/></summary>
    ''' <param name="d">A <see cref="charpresenter"/> <see cref="CodePoint"/> has changed for</param>
    ''' <param name="e">Event arguments</param>
    ''' <exception cref="Tools.TypeMismatchException"><paramref name="d"/> is not <see cref="charpresenter"/></exception>
    ''' <exception cref="ArgumentNullException"><paramref name="d"/> is null</exception>
    <DebuggerStepThrough()> _
    Private Shared Sub OnCodePointChanged(ByVal d As System.Windows.DependencyObject, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        If d Is Nothing Then Throw New ArgumentNullException("d")
        If Not TypeOf d Is CharPresenter Then Throw New Tools.TypeMismatchException("d", d, GetType(CharPresenter))
        DirectCast(d, CharPresenter).OnCodePointChanged(e)
    End Sub
    ''' <summary>Called when value of the <see cref="CodePoint"/> property changes</summary>
    ''' <param name="e">Event arguments</param>
    <CLSCompliant(False)>
    Protected Overridable Sub OnCodePointChanged(ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        If CodePoint <> Char.ConvertToUtf32(Character, 0) Then Character = Char.ConvertFromUtf32(CodePoint)
    End Sub
    ''' <summary>Called whenever a value of the <see cref="CodePoint"/> dependency property is being re-evaluated, or coercion is specifically requested.</summary>
    ''' <param name="d">The object that the property exists on. When the callback is invoked, the property system passes this value.</param>
    ''' <param name="baseValue">The new value of the property, prior to any coercion attempt.</param>
    ''' <returns>The coerced value (with appropriate type).</returns>
    ''' <exception cref="Tools.TypeMismatchException"><paramref name="d"/> is not of type <see cref="charpresenter"/> -or- <paramref name="baseValue"/> is not of type <see cref="UInteger "/></exception>
    ''' <exception cref="ArgumentNullException"><paramref name="d"/> is null</exception>
    Private Shared Function CoerceCodePointValue(ByVal d As System.Windows.DependencyObject, ByVal baseValue As Object) As Object
        If d Is Nothing Then Throw New ArgumentNullException("d")
        If Not TypeOf d Is CharPresenter Then Throw New Tools.TypeMismatchException("d", d, GetType(CharPresenter))
        If Not TypeOf baseValue Is UInteger AndAlso Not baseValue Is Nothing Then Throw New Tools.TypeMismatchException("baseValue", baseValue, GetType(UInteger))
        Return DirectCast(d, CharPresenter).CoerceCodePointValue(baseValue)
    End Function
    ''' <summary>Called whenever a value of the <see cref="CodePoint"/> dependency property is being re-evaluated, or coercion is specifically requested.</summary>
    ''' <param name="baseValue">The new value of the property, prior to any coercion attempt, but ensured to be of correct type.</param>
    ''' <returns>The coerced value (with appropriate type).</returns>
    <CLSCompliant(False)>
    Protected Overridable Function CoerceCodePointValue(ByVal baseValue As UInteger) As UInteger
        If baseValue > UnicodeCharacterDatabase.MaxCodePoint Then Return 0 Else Return baseValue
    End Function
#End Region

#Region "Character"
    ''' <summary>Gets or sets string representing single character or surrogate pair indicating character displayed</summary>
    ''' <exception cref="ArgumentException">Value being set is either</exception>
    Public Property Character() As String
        <DebuggerStepThrough()> Get
            Return GetValue(CharacterProperty)
        End Get
        <DebuggerStepThrough()> Set(ByVal value As String)
            If value Is Nothing Then Throw New ArgumentNullException("value")
            If value.Length < 0 OrElse value.Length > 2 OrElse (value.Length = 2 AndAlso (Not Char.IsHighSurrogate(value(0)) OrElse Not Char.IsLowSurrogate(value(1)))) Then
                Throw New ArgumentException("Character must be string which contains either exactly one character or exactly one properly formed UTF-16 surrogate pair.")
            End If
            SetValue(CharacterProperty, value)
        End Set
    End Property
    ''' <summary>Metadata of the <see cref="Character"/> dependency property</summary>
    Public Shared ReadOnly CharacterProperty As DependencyProperty =
                           DependencyProperty.Register("Character", GetType(String), GetType(CharPresenter),
                           New FrameworkPropertyMetadata(CStr(ChrW(0)), AddressOf OnCharacterChanged, AddressOf CoerceCharacterValue))
    ''' <summary>Called when value of the <see cref="Character"/> property changes for any <see cref="charpresenter"/></summary>
    ''' <param name="d">A <see cref="charpresenter"/> <see cref="Character"/> has changed for</param>
    ''' <param name="e">Event arguments</param>
    ''' <exception cref="Tools.TypeMismatchException"><paramref name="d"/> is not <see cref="charpresenter"/></exception>
    ''' <exception cref="ArgumentNullException"><paramref name="d"/> is null</exception>
    <DebuggerStepThrough()> _
    Private Shared Sub OnCharacterChanged(ByVal d As System.Windows.DependencyObject, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        If d Is Nothing Then Throw New ArgumentNullException("d")
        If Not TypeOf d Is CharPresenter Then Throw New Tools.TypeMismatchException("d", d, GetType(CharPresenter))
        DirectCast(d, CharPresenter).OnCharacterChanged(e)
    End Sub
    ''' <summary>Called when value of the <see cref="Character"/> property changes</summary>
    ''' <param name="e">Event arguments</param>
    Protected Overridable Sub OnCharacterChanged(ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        If CodePoint <> Char.ConvertToUtf32(Character, 0) Then CodePoint = Char.ConvertToUtf32(Character, 0)
    End Sub
    ''' <summary>Called whenever a value of the <see cref="Character"/> dependency property is being re-evaluated, or coercion is specifically requested.</summary>
    ''' <param name="d">The object that the property exists on. When the callback is invoked, the property system passes this value.</param>
    ''' <param name="baseValue">The new value of the property, prior to any coercion attempt.</param>
    ''' <returns>The coerced value (with appropriate type).</returns>
    ''' <exception cref="Tools.TypeMismatchException"><paramref name="d"/> is not of type <see cref="charpresenter"/> -or- <paramref name="baseValue"/> is not of type <see cref="string"/></exception>
    ''' <exception cref="ArgumentNullException"><paramref name="d"/> is null</exception>
    Private Shared Function CoerceCharacterValue(ByVal d As System.Windows.DependencyObject, ByVal baseValue As Object) As Object
        If d Is Nothing Then Throw New ArgumentNullException("d")
        If Not TypeOf d Is CharPresenter Then Throw New Tools.TypeMismatchException("d", d, GetType(CharPresenter))
        If Not TypeOf baseValue Is String AndAlso Not baseValue Is Nothing Then Throw New Tools.TypeMismatchException("baseValue", baseValue, GetType(String))
        Return DirectCast(d, CharPresenter).CoerceCharacterValue(baseValue)
    End Function
    ''' <summary>Called whenever a value of the <see cref="Character"/> dependency property is being re-evaluated, or coercion is specifically requested.</summary>
    ''' <param name="baseValue">The new value of the property, prior to any coercion attempt, but ensured to be of correct type.</param>
    ''' <returns>The coerced value (with appropriate type).</returns>
    Protected Overridable Function CoerceCharacterValue(ByVal baseValue As String) As String
        If baseValue Is Nothing OrElse baseValue.Length < 1 Then Return ChrW(0)
        If baseValue.Length > 2 OrElse (baseValue.Length = 2 AndAlso (Not Char.IsHighSurrogate(baseValue(0)) OrElse Not Char.IsLowSurrogate(baseValue(1)))) Then Return baseValue(0)
        Return baseValue
    End Function
#End Region

#Region "Chart"
    ''' <summary>Key of the <see cref="ChartProperty"/></summary>
    Private Shared ReadOnly ChartProperty As DependencyProperty = _
                            DependencyProperty.Register("Chart", _
                            GetType(CharacterChart), GetType(CharPresenter), _
                            New FrameworkPropertyMetadata(Nothing, AddressOf OnChartChanged))
#End Region


    Private Shared ReadOnly CharNamePropertyKey As DependencyPropertyKey = _
                            DependencyProperty.RegisterReadOnly("CharName", _
                            GetType(String), GetType(CharPresenter), _
                            New FrameworkPropertyMetadata(Nothing))

    Public Shared ReadOnly CharNameProperty As DependencyProperty = CharNamePropertyKey.DependencyProperty




    ''' <summary>Called when value of the <see cref="ChartProperty"/> property changes for any <see cref="charpresenter"/></summary>
    ''' <param name="d">A <see cref="charpresenter"/> <see cref="CodePoint"/> has changed for</param>
    ''' <param name="e">Event arguments</param>
    ''' <exception cref="Tools.TypeMismatchException"><paramref name="d"/> is not <see cref="charpresenter"/></exception>
    ''' <exception cref="ArgumentNullException"><paramref name="d"/> is null</exception>
    <DebuggerStepThrough()> _
    Private Shared Sub OnChartChanged(ByVal d As System.Windows.DependencyObject, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        If d Is Nothing Then Throw New ArgumentNullException("d")
        If Not TypeOf d Is CharPresenter Then Throw New Tools.TypeMismatchException("d", d, GetType(CharPresenter))
        DirectCast(d, CharPresenter).OnCodePointChanged(e)
    End Sub
    ''' <summary>Called when value of the <see cref="ChartProperty"/> property changes</summary>
    ''' <param name="e">Event arguments</param>
    <CLSCompliant(False)>
    Protected Overridable Sub OnChartChanged(ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        Dim chart As CharacterChart = DirectCast(GetValue(ChartProperty), CharacterChart)
        SetValue(CharNamePropertyKey, If(chart Is Nothing, Nothing, chart.NameSourceInternal(CodePoint)))
    End Sub

End Class