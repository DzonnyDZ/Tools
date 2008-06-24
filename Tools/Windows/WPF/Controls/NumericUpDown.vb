'Localize: Localization should continue here
#If Stage <= Nightly Then 'Stage: Nightly
Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports System.Windows.Automation.Peers
Imports System.Windows.Automation.Provider
Imports System.Windows.Automation
Imports System.Globalization
Imports System.Diagnostics
Imports System.Windows.Data
Imports System.Windows.Controls.Primitives
Imports CultureInfo = System.Globalization.CultureInfo

Namespace WindowsT.WPF.ControlsT
    'ASAP: More comments
    '<SnippetStaticCtorOfCustomClassCommonTasks>
    ''' <summary>Represents a Windows spin box (also known as an up-down control) that displays numeric values.</summary>
    ''' <remarks>
    ''' <para>This is companion class to <see cref="Windows.Forms.NumericUpDown"/>.</para>
    ''' <para>This class is bsed on http://msdn.microsoft.com/en-us/library/ms771573.aspx, converted by http://labs.developerfusion.co.uk/convert/csharp-to-vb.aspx</para>
    ''' </remarks>
    <Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <FirstVersion(2008, 5, 1), Version(1, 0, GetType(NumericUpDown), Note:="Based on http://msdn.microsoft.com/en-us/library/ms771573.aspx")> _
    <TemplatePart(Name:=NumericUpDown.PART_EditableTextBox, Type:=GetType(TextBox))> _
    Public Class NumericUpDown
        Inherits Control
        Friend Const PART_EditableTextBox As String = "PART_EditableTextBox"
        Shared Sub New()
            InitializeCommands()

            ' Listen to MouseLeftButtonDown event to determine if slide should move focus to itself
            EventManager.RegisterClassHandler(GetType(NumericUpDown), Mouse.MouseDownEvent, New MouseButtonEventHandler(AddressOf NumericUpDown.OnMouseLeftButtonDown), True)

            DefaultStyleKeyProperty.OverrideMetadata(GetType(NumericUpDown), New FrameworkPropertyMetadata(GetType(NumericUpDown)))
        End Sub
        '</SnippetStaticCtorOfCustomClassCommonTasks>

        Public Sub New()
            MyBase.New()
            updateValueString()
        End Sub

#Region "Properties"
#Region "Value"
        Public Property Value() As Decimal
            Get
                Return CDec(GetValue(ValueProperty))
            End Get
            Set(ByVal value As Decimal)
                SetValue(ValueProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' Identifies the Value dependency property.
        ''' </summary>
        Public Shared ReadOnly ValueProperty As DependencyProperty = DependencyProperty.Register("Value", GetType(Decimal), GetType(NumericUpDown), New FrameworkPropertyMetadata(DefaultValue, New PropertyChangedCallback(AddressOf OnValueChanged), New CoerceValueCallback(AddressOf CoerceValue)))

        Private Shared Sub OnValueChanged(ByVal obj As DependencyObject, ByVal args As DependencyPropertyChangedEventArgs)
            Dim control As NumericUpDown = DirectCast(obj, NumericUpDown)

            Dim oldValue As Decimal = CDec(args.OldValue)
            Dim newValue As Decimal = CDec(args.NewValue)

            '#Region "Fire Automation events"
            Dim peer As NumericUpDownAutomationPeer = TryCast(UIElementAutomationPeer.FromElement(control), NumericUpDownAutomationPeer)
            If peer IsNot Nothing Then
                peer.RaiseValueChangedEvent(oldValue, newValue)
            End If
            '#End Region

            Dim e As New RoutedPropertyChangedEventArgs(Of Decimal)(oldValue, newValue, ValueChangedEvent)

            control.OnValueChanged(e)

            control.updateValueString()
        End Sub

        ''' <summary>
        ''' Raises the ValueChanged event.
        ''' </summary>
        ''' <param name="args">Arguments associated with the ValueChanged event.</param>
        Protected Overridable Sub OnValueChanged(ByVal args As RoutedPropertyChangedEventArgs(Of Decimal))
            [RaiseEvent](args)
        End Sub

        Private Overloads Shared Function CoerceValue(ByVal element As DependencyObject, ByVal value As Object) As Object
            Dim newValue As Decimal = CDec(value)
            Dim control As NumericUpDown = DirectCast(element, NumericUpDown)
            newValue = control.CoerceValue(newValue)
            Return newValue
        End Function
        ''' <summary>Enforces <see cref="Minimum"/>, <see cref="Maximum"/> and <see cref="DecimalPlaces"/> on given value</summary>
        ''' <param name="Value">Value to be coerced</param>
        ''' <returns>Value derived from <paramref name="Value"/> that fullfills the constrains</returns>
        Protected Overridable Overloads Function CoerceValue(ByVal Value As Decimal) As Decimal
            Value = Math.Max(Me.Minimum, Math.Min(Me.Maximum, Value))
            Value = [Decimal].Round(Value, Me.DecimalPlaces)
            Return Value
        End Function
#End Region
#Region "Minimum"
        Public Property Minimum() As Decimal
            Get
                Return CDec(GetValue(MinimumProperty))
            End Get
            Set(ByVal value As Decimal)
                SetValue(MinimumProperty, value)
            End Set
        End Property

        Public Shared ReadOnly MinimumProperty As DependencyProperty = DependencyProperty.Register("Minimum", GetType(Decimal), GetType(NumericUpDown), New FrameworkPropertyMetadata(DefaultMinValue, New PropertyChangedCallback(AddressOf OnMinimumChanged), New CoerceValueCallback(AddressOf CoerceMinimum)))

        Private Shared Sub OnMinimumChanged(ByVal element As DependencyObject, ByVal args As DependencyPropertyChangedEventArgs)
            element.CoerceValue(MaximumProperty)
            element.CoerceValue(ValueProperty)
        End Sub
        Private Shared Function CoerceMinimum(ByVal element As DependencyObject, ByVal value As Object) As Object
            Dim minimum As Decimal = CDec(value)
            Dim control As NumericUpDown = DirectCast(element, NumericUpDown)
            Return [Decimal].Round(minimum, control.DecimalPlaces)
        End Function
#End Region
#Region "Maximum"
        Public Property Maximum() As Decimal
            Get
                Return CDec(GetValue(MaximumProperty))
            End Get
            Set(ByVal value As Decimal)
                SetValue(MaximumProperty, value)
            End Set
        End Property

        Public Shared ReadOnly MaximumProperty As DependencyProperty = DependencyProperty.Register("Maximum", GetType(Decimal), GetType(NumericUpDown), New FrameworkPropertyMetadata(DefaultMaxValue, New PropertyChangedCallback(AddressOf OnMaximumChanged), New CoerceValueCallback(AddressOf CoerceMaximum)))

        Private Shared Sub OnMaximumChanged(ByVal element As DependencyObject, ByVal args As DependencyPropertyChangedEventArgs)
            element.CoerceValue(ValueProperty)
        End Sub

        Private Shared Function CoerceMaximum(ByVal element As DependencyObject, ByVal value As Object) As Object
            Dim control As NumericUpDown = DirectCast(element, NumericUpDown)
            Dim newMaximum As Decimal = CDec(value)
            Return [Decimal].Round(Math.Max(newMaximum, control.Minimum), control.DecimalPlaces)
        End Function
#End Region
#Region "Change"
        Public Property Change() As Decimal
            Get
                Return CDec(GetValue(ChangeProperty))
            End Get
            Set(ByVal value As Decimal)
                SetValue(ChangeProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ChangeProperty As DependencyProperty = DependencyProperty.Register("Change", GetType(Decimal), GetType(NumericUpDown), New FrameworkPropertyMetadata(DefaultChange, New PropertyChangedCallback(AddressOf OnChangeChanged), New CoerceValueCallback(AddressOf CoerceChange)), New ValidateValueCallback(AddressOf ValidateChange))

        Private Shared Function ValidateChange(ByVal value As Object) As Boolean
            Dim change As Decimal = CDec(value)
            Return change > 0
        End Function

        Private Shared Sub OnChangeChanged(ByVal element As DependencyObject, ByVal args As DependencyPropertyChangedEventArgs)

        End Sub

        Private Shared Function CoerceChange(ByVal element As DependencyObject, ByVal value As Object) As Object
            Dim newChange As Decimal = CDec(value)
            Dim control As NumericUpDown = DirectCast(element, NumericUpDown)

            Dim coercedNewChange As Decimal = [Decimal].Round(newChange, control.DecimalPlaces)

            'If Change is .1 and DecimalPlaces is changed from 1 to 0, we want Change to go to 1, not 0.
            'Put another way, Change should always be rounded to DecimalPlaces, but never smaller than the 
            'previous Change
            If coercedNewChange < newChange Then
                coercedNewChange = smallestForDecimalPlaces(control.DecimalPlaces)
            End If

            Return coercedNewChange
        End Function

        Private Shared Function smallestForDecimalPlaces(ByVal decimalPlaces As Integer) As Decimal
            If decimalPlaces < 0 Then
                Throw New ArgumentException("decimalPlaces")
            End If

            Dim d As Decimal = 1
            For i As Integer = 0 To decimalPlaces - 1

                d /= 10
            Next

            Return d
        End Function

#End Region
#Region "DecimalPlaces"
        Public Property DecimalPlaces() As Integer
            Get
                Return CInt(GetValue(DecimalPlacesProperty))
            End Get
            Set(ByVal value As Integer)
                SetValue(DecimalPlacesProperty, value)
            End Set
        End Property

        Public Shared ReadOnly DecimalPlacesProperty As DependencyProperty = DependencyProperty.Register("DecimalPlaces", GetType(Integer), GetType(NumericUpDown), New FrameworkPropertyMetadata(DefaultDecimalPlaces, New PropertyChangedCallback(AddressOf OnDecimalPlacesChanged)), New ValidateValueCallback(AddressOf ValidateDecimalPlaces))

        Private Shared Sub OnDecimalPlacesChanged(ByVal element As DependencyObject, ByVal args As DependencyPropertyChangedEventArgs)
            Dim control As NumericUpDown = DirectCast(element, NumericUpDown)
            control.CoerceValue(ChangeProperty)
            control.CoerceValue(MinimumProperty)
            control.CoerceValue(MaximumProperty)
            control.CoerceValue(ValueProperty)
            control.updateValueString()
        End Sub

        Private Shared Function ValidateDecimalPlaces(ByVal value As Object) As Boolean
            Dim decimalPlaces As Integer = CInt(value)
            Return decimalPlaces >= 0
        End Function

#End Region

#Region "ValueString"
        Public Property ValueString() As String
            Get
                Return DirectCast(GetValue(ValueStringProperty), String)
            End Get
            Set(ByVal value As String)
                SetValue(ValueStringProperty, value)
            End Set
        End Property

        Public Shared ReadOnly ValueStringProperty As DependencyProperty = DependencyProperty.RegisterAttached("ValueString", GetType(String), GetType(NumericUpDown), New PropertyMetadata(DefaultValue.ToString(CultureInfo.InvariantCulture), New PropertyChangedCallback(AddressOf OnValueStringChanged), New CoerceValueCallback(AddressOf CoerceValueString)), New ValidateValueCallback(AddressOf ValidateValueString))

        'public static readonly DependencyProperty ValueStringProperty = ValueStringPropertyKey.DependencyProperty;

        Private Sub updateValueString()
            Dim newValueString As String = ValueToString(Me.Value)
            Me.SetValue(ValueStringProperty, newValueString)
        End Sub
        Protected Overridable Function ValueToString(ByVal value As Decimal) As String
            Return value.ToString(String.Format("f{0}", Me.DecimalPlaces, CultureInfo.InvariantCulture))
        End Function

        Private Shared Sub OnValueStringChanged(ByVal obj As DependencyObject, ByVal args As DependencyPropertyChangedEventArgs)
            Dim control As NumericUpDown = DirectCast(obj, NumericUpDown)

            Dim oldValue As String = DirectCast(args.OldValue, String)
            Dim newValue As String = DirectCast(args.NewValue, String)

            '#region Fire Automation events
            'NumericUpDownAutomationPeer peer = UIElementAutomationPeer.FromElement(control) as NumericUpDownAutomationPeer;
            'if(peer != null) {
            '    peer.RaiseValueStringChangedEvent(oldValue, newValue);
            '}
            '#endregion

            Dim e As New RoutedPropertyChangedEventArgs(Of String)(oldValue, newValue, ValueStringChangedEvent)

            control.OnValueStringChanged(e)
            control.SetValue(ValueProperty, control.TextToValue(newValue, control.Value))
        End Sub

        ''' <summary>
        ''' Raises the ValueChanged event.
        ''' </summary>
        ''' <param name="args">Arguments associated with the ValueChanged event.</param>
        Protected Overridable Sub OnValueStringChanged(ByVal args As RoutedPropertyChangedEventArgs(Of String))
            [RaiseEvent](args)
        End Sub

        Private Shared Function CoerceValueString(ByVal element As DependencyObject, ByVal value As Object) As Object
            Dim newStrValue As String = DirectCast(value, String)
            Dim control As NumericUpDown = DirectCast(element, NumericUpDown)
            Dim newDecimal As Decimal = control.TextToValue(newStrValue, control.Value)
            newDecimal = control.CoerceValue(newDecimal)
            Dim newStr As String = control.ValueToString(newDecimal)
            Return newStr
        End Function
        Private Shared Function ValidateValueString(ByVal value As Object) As Boolean
            Dim str As String = TryCast(value, String)
            If str Is Nothing Then
                Return False
            End If
            Dim newValue As Decimal
            Dim CanParse As Boolean = Decimal.TryParse(str, newValue)
            Return CanParse
        End Function
#End Region

        Public Property IsEditable() As Boolean
            Get
                Return CBool(GetValue(IsEditableProperty))
            End Get
            Set(ByVal value As Boolean)
                SetValue(IsEditableProperty, value)
            End Set
        End Property

        ' Using a DependencyProperty as the backing store for IsEditable.  This enables animation, styling, binding, etc...
        Public Shared ReadOnly IsEditableProperty As DependencyProperty = DependencyProperty.Register("IsEditable", GetType(Boolean), GetType(NumericUpDown), New UIPropertyMetadata(True))


#End Region

#Region "Events"
        ''' <summary>
        ''' Identifies the ValueChanged routed event.
        ''' </summary>
        Public Shared ReadOnly ValueChangedEvent As RoutedEvent = EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, GetType(RoutedPropertyChangedEventHandler(Of Decimal)), GetType(NumericUpDown))

        ''' <summary>
        ''' Identifies the ValueStringChanged routed event.
        ''' </summary>
        Public Shared ReadOnly ValueStringChangedEvent As RoutedEvent = EventManager.RegisterRoutedEvent("ValueStringChanged", RoutingStrategy.Bubble, GetType(RoutedPropertyChangedEventHandler(Of String)), GetType(NumericUpDown))

        ''' <summary>
        ''' Occurs when the Value property changes.
        ''' </summary>
        Public Custom Event ValueChanged As RoutedPropertyChangedEventHandler(Of Decimal)
            AddHandler(ByVal value As RoutedPropertyChangedEventHandler(Of Decimal))
                [AddHandler](ValueChangedEvent, value)
            End AddHandler
            RemoveHandler(ByVal value As RoutedPropertyChangedEventHandler(Of Decimal))
                [RemoveHandler](ValueChangedEvent, value)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As RoutedPropertyChangedEventArgs(Of Decimal))
                [RaiseEvent](e)
            End RaiseEvent
        End Event
        ''' <summary>
        ''' Occurs when the ValueString property changes.
        ''' </summary>
        Public Custom Event ValueStringChanged As RoutedPropertyChangedEventHandler(Of String)
            AddHandler(ByVal value As RoutedPropertyChangedEventHandler(Of String))
                [AddHandler](ValueStringChangedEvent, value)
            End AddHandler
            RemoveHandler(ByVal value As RoutedPropertyChangedEventHandler(Of String))
                [RemoveHandler](ValueStringChangedEvent, value)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As RoutedPropertyChangedEventArgs(Of String))
                [RaiseEvent](e)
            End RaiseEvent
        End Event
#End Region

#Region "Commands"

        Public Shared ReadOnly Property IncreaseCommand() As RoutedCommand
            Get
                Return _increaseCommand
            End Get
        End Property
        Public Shared ReadOnly Property DecreaseCommand() As RoutedCommand
            Get
                Return _decreaseCommand
            End Get
        End Property

        Private Shared Sub InitializeCommands()
            _increaseCommand = New RoutedCommand("IncreaseCommand", GetType(NumericUpDown))
            CommandManager.RegisterClassCommandBinding(GetType(NumericUpDown), New CommandBinding(_increaseCommand, AddressOf OnIncreaseCommand))
            CommandManager.RegisterClassInputBinding(GetType(NumericUpDown), New InputBinding(_increaseCommand, New KeyGesture(Key.Up)))

            _decreaseCommand = New RoutedCommand("DecreaseCommand", GetType(NumericUpDown))
            CommandManager.RegisterClassCommandBinding(GetType(NumericUpDown), New CommandBinding(_decreaseCommand, AddressOf OnDecreaseCommand))
            CommandManager.RegisterClassInputBinding(GetType(NumericUpDown), New InputBinding(_decreaseCommand, New KeyGesture(Key.Down)))
        End Sub

        Private Shared Sub OnIncreaseCommand(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
            Dim control As NumericUpDown = TryCast(sender, NumericUpDown)
            If control IsNot Nothing Then
                control.OnIncrease()
            End If
        End Sub
        Private Shared Sub OnDecreaseCommand(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
            Dim control As NumericUpDown = TryCast(sender, NumericUpDown)
            If control IsNot Nothing Then
                control.OnDecrease()
            End If
        End Sub

        Protected Overridable Sub OnIncrease()
            Me.Value += Change
        End Sub
        Protected Overridable Sub OnDecrease()
            Me.Value -= Change
        End Sub

        Private Shared _increaseCommand As RoutedCommand
        Private Shared _decreaseCommand As RoutedCommand
#End Region

#Region "Automation"
        '<SnippetOnCreateAutomationPeer>
        Protected Overloads Overrides Function OnCreateAutomationPeer() As AutomationPeer
            Return New NumericUpDownAutomationPeer(Me)
        End Function
        '</SnippetOnCreateAutomationPeer>
#End Region

        ''' <summary>
        ''' This is a class handler for MouseLeftButtonDown event.
        ''' The purpose of this handle is to move input focus to NumericUpDown when user pressed
        ''' mouse left button on any part of slider that is not focusable.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Overloads Shared Sub OnMouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
            Dim control As NumericUpDown = DirectCast(sender, NumericUpDown)

            ' When someone click on a part in the NumericUpDown and it's not focusable
            ' NumericUpDown needs to take the focus in order to process keyboard correctly
            If Not control.IsKeyboardFocusWithin Then
                e.Handled = control.Focus() OrElse e.Handled
            End If
        End Sub

        Private Const DefaultMinValue As Decimal = 0, DefaultValue As Decimal = DefaultMinValue, DefaultMaxValue As Decimal = 100, DefaultChange As Decimal = 1
        Private Const DefaultDecimalPlaces As Integer = 0

#Region "TextBox Validation"
        ''' <summary>Current <see cref="TextBox"/> (if any)</summary>
        Private textBox As TextBox
        ''' <summary>Invoked whenever application code or internal processes call <see cref="System.Windows.FrameworkElement.ApplyTemplate"/>.</summary>
        Public Overloads Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            Dim TemplateObj = Me.Template.FindName(PART_EditableTextBox, Me)
            DetachTextBoxEvents()
            If TypeOf TemplateObj Is TextBox Then
                textBox = DirectCast(TemplateObj, TextBox)
                AttachTextBoxEvents()
            Else
                textBox = Nothing
            End If
        End Sub
        ''' <summary>Hooks events of <see cref="textBox"/></summary>
        Private Sub AttachTextBoxEvents()
            If textBox Is Nothing Then
                Return
            End If
            AddHandler textBox.PreviewTextInput, AddressOf textBox_PreviewTextInput
            AddHandler textBox.LostFocus, AddressOf textBox_LostFocus
        End Sub
        ''' <summary>Unhooks events of <see cref="textBox"/></summary>
        Private Sub DetachTextBoxEvents()
            If textBox Is Nothing Then
                Return
            End If
            AddHandler textBox.PreviewTextInput, AddressOf textBox_PreviewTextInput
            RemoveHandler textBox.LostFocus, AddressOf textBox_LostFocus
        End Sub
        ''' <summary>Prevents non-number characters from being typed</summary>
        Private Sub textBox_PreviewTextInput(ByVal sender As Object, ByVal e As TextCompositionEventArgs)
            If Me.IsEditable Then
                e.Handled = Not AreCharsAccepltable(e.Text)
            End If
        End Sub
        Private Sub textBox_LostFocus(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Me.IsEditable Then
                Me.Value = TextToValue(textBox.Text, Me.Value)
                Me.ValueString = ValueToString(Me.Value)
                textBox.Text = Me.ValueString
            End If
        End Sub
        ''' <summary>Attempts to convert user-typed text to number</summary>
        ''' <param name="text">Text to converts</param>
        ''' <param name="fallback">Value to be retuned if <paramref name="text"/> cannot be converted to decimal</param>
        ''' <returns>
        ''' <paramref name="text"/> converted to number. It conversion is not possible returns <paramref name="fallback"/>.
        ''' Return value should be acceptable by <see cref="Minimum"/>, <see cref="Maximum"/> and <see cref="DecimalPlaces"/> constraints.
        ''' <para>This implementation utilizes <see cref="CoerceValue"/></para>
        ''' </returns>
        Protected Overridable Function TextToValue(ByVal text As String, ByVal fallback As Decimal) As Decimal
            Dim newValue As Decimal
            If Decimal.TryParse(text, newValue) Then
                Return CDec(CoerceValue(Me, newValue))
            Else
                Return fallback
            End If
        End Function
        ''' <summary>For given string gets value indicating if it consists only of characters acceptable as part of number</summary>
        ''' <param name="text">String to verify</param>
        ''' <returns>True if <paramref name="text"/> consists only of characters valid as part of number. Takes current culture and current constrainst in account.</returns>
        ''' <remarks>Returns true even for string which consists for valid character but is invalid number (i.e. "4.-..5" in invariant culture)</remarks>
        Protected Overridable Function AreCharsAccepltable(ByVal text As String) As Boolean
            If String.IsNullOrEmpty(text) Then
                Return True
            End If
            Dim NumberFormat = CultureInfo.CurrentCulture.NumberFormat
            For Each ch As Char In text
                If (NumberFormat.NumberDecimalSeparator.Contains(New String(ch, 1)) AndAlso Me.DecimalPlaces > 0) OrElse (NumberFormat.NumberGroupSeparator.Contains(New String(ch, 1))) OrElse (Char.IsDigit(ch)) OrElse (NumberFormat.NegativeSign.Contains(New String(ch, 1)) AndAlso Me.Minimum < 0) OrElse (NumberFormat.PositiveSign.Contains(New String(ch, 1)) AndAlso Me.Maximum > 0) Then
                    Continue For
                End If
                Return False
            Next
            Return True
        End Function
#End Region
    End Class
    Public Class NumericUpDownAutomationPeer
        Inherits FrameworkElementAutomationPeer
        Implements IRangeValueProvider
        Public Sub New(ByVal control As NumericUpDown)
            MyBase.New(control)
        End Sub

        Protected Overloads Overrides Function GetClassNameCore() As String
            Return "NumericUpDown"
        End Function

        Protected Overloads Overrides Function GetAutomationControlTypeCore() As AutomationControlType
            Return AutomationControlType.Spinner
        End Function


        '<SnippetGetPattern>
        Public Overloads Overrides Function GetPattern(ByVal patternInterface As PatternInterface) As Object
            If patternInterface = patternInterface.RangeValue Then
                Return Me
            End If
            Return MyBase.GetPattern(patternInterface)
        End Function
        '</SnippetGetPattern>

        Friend Sub RaiseValueChangedEvent(ByVal oldValue As Decimal, ByVal newValue As Decimal)
            MyBase.RaisePropertyChangedEvent(RangeValuePatternIdentifiers.ValueProperty, CDbl(oldValue), CDbl(newValue))
        End Sub
        'internal void RaiseValueStringChangedEvent(string oldValue, string newValue) {
        '    base.RaisePropertyChangedEvent(NumericUpDown.ValueStringProperty,
        '        (string)oldValue, (string)newValue);
        '}

#Region "IRangeValueProvider Members"

        Private ReadOnly Property IsReadOnly() As Boolean Implements IRangeValueProvider.IsReadOnly
            Get
                Return Not IsEnabled()
            End Get
        End Property

        Private ReadOnly Property LargeChange() As Double Implements IRangeValueProvider.LargeChange
            Get
                Return CDbl(MyOwner.Change)
            End Get
        End Property

        Private ReadOnly Property Maximum() As Double Implements IRangeValueProvider.Maximum
            Get
                Return CDbl(MyOwner.Maximum)
            End Get
        End Property

        Private ReadOnly Property Minimum() As Double Implements IRangeValueProvider.Minimum
            Get
                Return CDbl(MyOwner.Minimum)
            End Get
        End Property

        Private Sub SetValue(ByVal value As Double) Implements IRangeValueProvider.SetValue
            If Not IsEnabled() Then
                Throw New ElementNotEnabledException()
            End If

            Dim val As Decimal = CDec(value)
            If val < MyOwner.Minimum OrElse val > MyOwner.Maximum Then
                Throw New ArgumentOutOfRangeException("value")
            End If

            MyOwner.Value = val
        End Sub

        Private ReadOnly Property SmallChange() As Double Implements IRangeValueProvider.SmallChange
            Get
                Return CDbl(MyOwner.Change)
            End Get
        End Property

        Private ReadOnly Property Value() As Double Implements IRangeValueProvider.Value
            Get
                Return CDbl(MyOwner.Value)
            End Get
        End Property

#End Region

        Private ReadOnly Property MyOwner() As NumericUpDown
            Get
                Return DirectCast(MyBase.Owner, NumericUpDown)
            End Get
        End Property
        '<SnippetClose>
    End Class
    '</SnippetClose>



End Namespace
#End If