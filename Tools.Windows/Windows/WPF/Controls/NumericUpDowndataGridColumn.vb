Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows
Imports System.Windows.Documents
Imports Tools.ComponentModelT

#If Stage <= Nightly Then 'Stage: Nightly
Namespace WindowsT.WPF.ControlsT

    ''' <summary>A column for WPF <see cref="DataGrid"/> for entering numbers using <see cref="NumericUpDown"/>.</summary>
    ''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
    Public Class NumericUpDowndataGridColumn
        Inherits DataGridBoundColumn

        ''' <summary>Gets an editing element that is bound to the column's <see cref="P:System.Windows.Controls.DataGridBoundColumn.Binding" /> property value.</summary>
        ''' <returns>A new editing element that is bound to the column's <see cref="P:System.Windows.Controls.DataGridBoundColumn.Binding" /> property value. (A <see cref="NumericUpDown"/>)</returns>
        ''' <param name="cell">The cell that will contain the generated element.</param>
        ''' <param name="dataItem">The data item represented by the row that contains the intended cell.</param>
        Protected Overrides Function GenerateEditingElement(ByVal cell As System.Windows.Controls.DataGridCell, ByVal dataItem As Object) As System.Windows.FrameworkElement
            Dim e As New NumericUpDown
            ApplyStyle(True, False, e)
            Dim binding As BindingBase = Me.Binding
            If (Not binding Is Nothing) Then
                BindingOperations.SetBinding(e, NumericUpDown.ValueProperty, binding)
            Else
                BindingOperations.ClearBinding(e, NumericUpDown.ValueProperty)
            End If
            Return e
        End Function

        ''' <summary>When overridden in a derived class, gets a read-only element that is bound to the column's <see cref="P:System.Windows.Controls.DataGridBoundColumn.Binding" /> property value.</summary>
        ''' <returns>A new, read-only element that is bound to the column's <see cref="P:System.Windows.Controls.DataGridBoundColumn.Binding" /> property value. (A <see cref="TextBlock"/>)</returns>
        ''' <param name="cell">The cell that will contain the generated element.</param>
        ''' <param name="dataItem">The data item represented by the row that contains the intended cell.</param>
        Protected Overrides Function GenerateElement(ByVal cell As System.Windows.Controls.DataGridCell, ByVal dataItem As Object) As System.Windows.FrameworkElement
            Dim e As New TextBlock
            ApplyStyle(False, False, e)
            Dim binding As BindingBase = Me.Binding
            If (Not binding Is Nothing) Then
                BindingOperations.SetBinding(e, TextBlock.TextProperty, binding)
            Else
                BindingOperations.ClearBinding(e, TextBlock.TextProperty)
            End If
            Return e
        End Function


#Region "Helpers"
        ''' <summary>Applies a style to a <see cref="FrameworkElement"/></summary>
        ''' <param name="isEditing">True when the element is an editing element</param>
        ''' <param name="defaultToElementStyle">True to use <see cref="ElementStyle"/> when <paramref name="isEditing"/> is true and <see cref="EditingElementStyle"/> is null</param>
        ''' <param name="element">The element to apply style onto</param>
        ''' <remarks>Code of this method was obtained from Reflector on <see cref="M:System.Windows.Controls.DataGridBoundColumn.ApplyStyle"/></remarks>
        Friend Sub ApplyStyle(ByVal isEditing As Boolean, ByVal defaultToElementStyle As Boolean, ByVal element As FrameworkElement)
            Dim style As Windows.Style = Me.PickStyle(isEditing, defaultToElementStyle)
            If (Not style Is Nothing) Then
                element.Style = style
            End If
        End Sub

        ''' <summary>Gets style for an element</summary>
        ''' <param name="isEditing">True when the element is editing element</param>
        ''' <param name="defaultToElementStyle">True to use <see cref="ElementStyle"/> when <paramref name="isEditing"/> is true and <see cref="EditingElementStyle"/> is null</param>
        ''' <returns>Either <see cref="ElementStyle"/> or <see cref="EditingElementStyle"/>.</returns>
        ''' <remarks>Code of this method was obtained from Reflector on <see cref="M:System.Windows.Controls.DataGridBoundColumn.PickStyle"/></remarks>
        Private Function PickStyle(ByVal isEditing As Boolean, ByVal defaultToElementStyle As Boolean) As Style
            Dim elementStyle As Style = If(isEditing, Me.EditingElementStyle, Me.ElementStyle)
            If isEditing AndAlso defaultToElementStyle AndAlso elementStyle Is Nothing Then
                elementStyle = Me.ElementStyle
            End If
            Return elementStyle
        End Function


        ''' <summary>Synchronizes column property with element</summary>
        ''' <param name="column">The column to synchronize value with</param>
        ''' <param name="content">A <see cref="DependencyObject"/> to synchronize value to</param>
        ''' <param name="contentProperty">Property to synchronize value to</param>
        ''' <param name="columnProperty">Property to snychronize value with</param>
        ''' <remarks>Code of this method was obtained from Reflector on <see cref="M:System.Windows.Controls.DataGridHelper.SyncColumnProperty"/></remarks>
        Friend Shared Sub SyncColumnProperty(ByVal column As DependencyObject, ByVal content As DependencyObject, ByVal contentProperty As DependencyProperty, ByVal columnProperty As DependencyProperty)
            If IsDefaultValue(column, columnProperty) Then
                content.ClearValue(contentProperty)
            Else
                content.SetValue(contentProperty, column.GetValue(columnProperty))
            End If
        End Sub

        ''' <summary>Determines wheather property has it's default value</summary>
        ''' <param name="d">A <see cref="DependencyObject"/> to tell that for</param>
        ''' <param name="dp">A property to tell that for</param>
        ''' <returns>True if property's value is default</returns>
        ''' <remarks>Code of this method was obtained from Reflector on <see cref="M:System.Windows.Controls.DataGridHelper.IsDefaultValue"/></remarks>
        Public Shared Function IsDefaultValue(ByVal d As DependencyObject, ByVal dp As DependencyProperty) As Boolean
            Return (DependencyPropertyHelper.GetValueSource(d, dp).BaseValueSource = BaseValueSource.Default)
        End Function

        ''' <summary>Synchronizes properties of editing element and column</summary>
        Private Sub SyncProperties(ByVal e As FrameworkElement)
            SyncColumnProperty(Me, e, NumericUpDown.MinimumProperty, NumericUpDowndataGridColumn.MinimumProperty)
            SyncColumnProperty(Me, e, NumericUpDown.MaximumProperty, NumericUpDowndataGridColumn.MaximumProperty)
            SyncColumnProperty(Me, e, NumericUpDown.ChangeProperty, NumericUpDowndataGridColumn.ChangeProperty)
            SyncColumnProperty(Me, e, NumericUpDown.DecimalPlacesProperty, NumericUpDowndataGridColumn.DecimalPlacesProperty)
        End Sub

#End Region

#Region "Properties"

#Region "Minimum"
        ''' <summary>Gets or sets minimum allowed value.</summary>
        ''' <returns>Minimum allowed value.</returns>
        ''' <value>Minimum allowed value.</value>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        Public Property Minimum() As Decimal
            Get
                Return CDec(GetValue(MinimumProperty))
            End Get
            Set(ByVal value As Decimal)
                SetValue(MinimumProperty, value)
            End Set
        End Property
        ''' <summary>Identifies the <see cref="Minimum"/> property</summary>
        Public Shared ReadOnly MinimumProperty As DependencyProperty =
            DependencyProperty.Register("Minimum", GetType(Decimal), GetType(NumericUpDowndataGridColumn),
                                        New FrameworkPropertyMetadata(NumericUpDown.DefaultMinValue, New PropertyChangedCallback(AddressOf OnMinimumChanged),
                                                                      New CoerceValueCallback(AddressOf CoerceMinimum)))
        ''' <summary>Handles change of the <see cref="Minimum"/> property</summary>
        ''' <param name="element">Source of event</param>
        ''' <param name="args">Event arguments</param>
        Private Shared Sub OnMinimumChanged(ByVal element As DependencyObject, ByVal args As DependencyPropertyChangedEventArgs)
            element.CoerceValue(MaximumProperty)
        End Sub
        ''' <summary>Coerces value of the <see cref="Minimum"/> property</summary>
        ''' <param name="element">Element to coerce value for. Must be <see cref="NumericUpDown"/>.</param>
        ''' <param name="value">Value to be coerced. Must be <see cref="Decimal"/></param>
        Private Shared Function CoerceMinimum(ByVal element As DependencyObject, ByVal value As Object) As Object
            Dim minimum As Decimal = CDec(value)
            Dim control As NumericUpDowndataGridColumn = DirectCast(element, NumericUpDowndataGridColumn)
            Return [Decimal].Round(minimum, control.DecimalPlaces)
        End Function
#End Region
#Region "Maximum"
        ''' <summary>Gets or sets maximum allowed value.</summary>
        ''' <returns>Maximum allowed value</returns>
        ''' <value>Maximum allowed value</value>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        Public Property Maximum() As Decimal
            Get
                Return CDec(GetValue(MaximumProperty))
            End Get
            Set(ByVal value As Decimal)
                SetValue(MaximumProperty, value)
            End Set
        End Property
        ''' <summary>Identifies the <see cref="Maximum"/> property</summary>
        Public Shared ReadOnly MaximumProperty As DependencyProperty =
            DependencyProperty.Register("Maximum", GetType(Decimal), GetType(NumericUpDowndataGridColumn),
                                        New FrameworkPropertyMetadata(NumericUpDown.DefaultMaxValue, Nothing,
                                                                      New CoerceValueCallback(AddressOf CoerceMaximum)))
        ''' <summary>Coerces value of the <see cref="Maximum"/> property</summary>
        ''' <param name="element">Element to coerce value for. Must be <see cref="NumericUpDown"/>.</param>
        ''' <param name="value">Value to be coerced. Must be <see cref="Decimal"/></param>
        Private Shared Function CoerceMaximum(ByVal element As DependencyObject, ByVal value As Object) As Object
            Dim control As NumericUpDowndataGridColumn = DirectCast(element, NumericUpDowndataGridColumn)
            Dim newMaximum As Decimal = CDec(value)
            Return [Decimal].Round(Math.Max(newMaximum, control.Minimum), control.DecimalPlaces)
        End Function
#End Region
#Region "Change"
        ''' <summary>Gets or sets value indicating step value changes when user increnets/decrements the value</summary>
        ''' <returns>Value of incerement/decrement step</returns>
        ''' <value>Value of increment/decrement step</value>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        Public Property Change() As Decimal
            Get
                Return CDec(GetValue(ChangeProperty))
            End Get
            Set(ByVal value As Decimal)
                SetValue(ChangeProperty, value)
            End Set
        End Property
        ''' <summary>Identifies the <see cref="Change"/> dependency property</summary>
        Public Shared ReadOnly ChangeProperty As DependencyProperty =
            DependencyProperty.Register("Change", GetType(Decimal), GetType(NumericUpDowndataGridColumn),
                                        New FrameworkPropertyMetadata(NumericUpDown.DefaultChange, Nothing,
                                                                      New CoerceValueCallback(AddressOf CoerceChange)),
                                                                  New ValidateValueCallback(AddressOf ValidateChange))
        ''' <summary>Valudates value of the <see cref="Change"/> property</summary>
        ''' <param name="value">New value of the <see cref="Change"/> property. Must be <see cref="Decimal"/></param>
        ''' <returns>True if <paramref name="value"/> is greater than zero; false otherwise</returns>
        Private Shared Function ValidateChange(ByVal value As Object) As Boolean
            Dim change As Decimal = CDec(value)
            Return change > 0
        End Function

        ''' <summary>Corces value of the <see cref="Change"/> property</summary>
        ''' <param name="element">Element to corece value for. Must be <see cref="NumericUpDown"/>.</param>
        ''' <param name="value">Proposed value of the <see cref="Change"/> property</param>
        ''' <returns>Coerced value (<see cref="Decimal"/>)</returns>
        Private Shared Function CoerceChange(ByVal element As DependencyObject, ByVal value As Object) As Object
            Dim newChange As Decimal = CDec(value)
            Dim control As NumericUpDowndataGridColumn = DirectCast(element, NumericUpDowndataGridColumn)

            Dim coercedNewChange As Decimal = [Decimal].Round(newChange, control.DecimalPlaces)

            'If Change is .1 and DecimalPlaces is changed from 1 to 0, we want Change to go to 1, not 0.
            'Put another way, Change should always be rounded to DecimalPlaces, but never smaller than the 
            'previous Change
            If coercedNewChange < newChange Then
                coercedNewChange = smallestForDecimalPlaces(control.DecimalPlaces)
            End If

            Return coercedNewChange
        End Function
        ''' <summary>Gets smallest  value by decimal places</summary>
        ''' <param name="decimalPlaces">Number of decimal places</param>
        ''' <returns>Smallest value for given <paramref name="decimalPlaces"/>.</returns>
        ''' <exception cref="ArgumentException"><paramref name="decimalPlaces"/> is less than zero</exception>
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
        ''' <summary>Gets or sets value indication decimal place precision of number</summary>
        ''' <remarks>Drecimal-places pecision of number</remarks>
        ''' <value>Number of decimal places to be entered</value>
        <KnownCategoryAttribute(KnownCategoryAttribute.KnownCategories.Appearance)> _
        Public Property DecimalPlaces() As Integer
            Get
                Return CInt(GetValue(DecimalPlacesProperty))
            End Get
            Set(ByVal value As Integer)
                SetValue(DecimalPlacesProperty, value)
            End Set
        End Property
        ''' <summary>Identifies the <see cref="DecimalPlaces"/> property</summary>
        Public Shared ReadOnly DecimalPlacesProperty As DependencyProperty =
            DependencyProperty.Register("DecimalPlaces", GetType(Integer), GetType(NumericUpDowndataGridColumn),
                                        New FrameworkPropertyMetadata(NumericUpDown.DefaultDecimalPlaces, New PropertyChangedCallback(AddressOf OnDecimalPlacesChanged)),
                                        New ValidateValueCallback(AddressOf ValidateDecimalPlaces))
        ''' <summary>Handles change of value of <see cref="DecimalPlaces"/> property</summary>
        ''' <param name="element">Element must be <see cref="NumericUpDown"/>.</param>
        ''' <param name="args">Event arguments</param>
        Private Shared Sub OnDecimalPlacesChanged(ByVal element As DependencyObject, ByVal args As DependencyPropertyChangedEventArgs)
            Dim control As NumericUpDowndataGridColumn = DirectCast(element, NumericUpDowndataGridColumn)
            control.CoerceValue(ChangeProperty)
            control.CoerceValue(MinimumProperty)
            control.CoerceValue(MaximumProperty)
        End Sub
        ''' <summary>Validates value of the <see cref="DecimalPlaces"/> property</summary>
        ''' <param name="value">Number of decimapl places. Must be <see cref="Integer"/></param>
        ''' <returns>True when <paramref name="value"/> is greater than or equal to zero; false otherwise</returns>
        Private Shared Function ValidateDecimalPlaces(ByVal value As Object) As Boolean
            Dim decimalPlaces As Integer = CInt(value)
            Return decimalPlaces >= 0
        End Function

#End Region
#End Region
    End Class

End Namespace
#End If