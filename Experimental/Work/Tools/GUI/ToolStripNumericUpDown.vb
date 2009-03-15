Imports System.ComponentModel, EOS.Resources
Imports System.Windows.Forms
Imports System.Drawing
Imports Tools.Experimental.Resources

Namespace GUI
    ''' <summary>Embdeds <see cref="NumericUpDown"/> into <see cref="ToolStrip"/></summary>
    <DefaultEvent("ValueChanged")> _
    <ToolboxBitmap(GetType(NumericUpDown))> _
    Public Class ToolStripNumericUpDown
        Inherits ToolStripControlHost
        Implements IBindableComponent
        ''' <summary>CTor</summary>
        Public Sub New()
            MyBase.New(New NumericUpDown)
            With Me.NumericUpDown
                .BorderStyle = BorderStyle.None
                .Dock = DockStyle.Fill
                AddHandler .ValueChanged, AddressOf NumericUpDown_ValueChanged
                AddHandler .BindingContextChanged, AddressOf NumericUpDown_BindingContextChanged
            End With
        End Sub
        ''' <summary><see cref="NumericUpDown"/> which provides functionality of this instance</summary>
        ''' <returns><see cref="NumericUpDown"/> same as <see cref="Control"/></returns>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public ReadOnly Property NumericUpDown() As NumericUpDown
            Get
                Return Me.Control
            End Get
        End Property
#Region "NumericUpDown"
        ''' <summary>Occurs when the <see cref="System.Windows.Forms.NumericUpDown.Value"/> property has been changed in some way.</summary>
        <WinFormsSRDescription("NumericUpDownOnValueChangedDescr")> _
        <Category("Action")> _
        Public Event ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
        ''' <summary>Handles <see cref="NumericUpDown"/>.<see cref="NumericUpDown.ValueChanged">ValueChanged</see></summary>
        ''' <param name="sender"><see cref="NumericUpDown"/></param>
        ''' <param name="e">Event arguments</param>
        Private Sub NumericUpDown_ValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            OnValueChanged(e)
        End Sub
        ''' <summary>Raises the <see cref="ValueChanged"/> event</summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
            RaiseEvent ValueChanged(Me, e)
        End Sub
        ''' <summary>Gets or sets the number of decimal places to display in the spin box (also known as an up-down control).</summary>
        ''' <returns>The number of decimal places to display in the spin box. The default is 0.</returns>
        ''' <exception cref="System.ArgumentOutOfRangeException">The value assigned is less than 0.-or- The value assigned is greater than 99.</exception>
        ''' <seelaso cref="NumericUpDown.DecimalPlaces"/>
        <WinFormsSRDescriptionAttribute("NumericUpDownDecimalPlacesDescr")> _
        <Category("Data"), DefaultValue(0)> _
        Public Property DecimalPlaces() As Integer
            Get
                Return NumericUpDown.DecimalPlaces
            End Get
            Set(ByVal value As Integer)
                NumericUpDown.DecimalPlaces = value
            End Set
        End Property
        ''' <summary>Gets or sets a value indicating whether the spin box (also known as an up-down control) should display the value it contains in hexadecimal format.</summary>
        ''' <returns>true if the spin box should display its value in hexadecimal format; otherwise, false. The default is false.</returns>
        ''' <seelaso cref="NumericUpDown.Hexadecimal"/>
        <WinFormsSRDescriptionAttribute("NumericUpDownHexadecimalDescr")> _
        <Category("Appearance"), DefaultValue(False)> _
        Public Property Hexadecimal() As Boolean
            Get
                Return NumericUpDown.Hexadecimal
            End Get
            Set(ByVal value As Boolean)
                NumericUpDown.Hexadecimal = value
            End Set
        End Property
        ''' <summary>Gets or sets the value to increment or decrement the spin box (also known as an up-down control) when the up or down buttons are clicked.</summary>
        ''' <returns>The value to increment or decrement the <see cref="System.Windows.Forms.NumericUpDown.Value"/> property when the up or down buttons are clicked on the spin box. The default value is 1.</returns>
        ''' <exception cref="System.ArgumentOutOfRangeException">The assigned value is not a positive number.</exception>
        ''' <seelaso cref="NumericUpDown.Increment"/>
        <WinFormsSRDescriptionAttribute("NumericUpDownIncrementDescr")> _
        <Category("Data")> _
        <DefaultValue(GetType(Decimal), "1")> _
        Public Property Increment() As Decimal
            Get
                Return NumericUpDown.Increment
            End Get
            Set(ByVal value As Decimal)
                NumericUpDown.Increment = value
            End Set
        End Property
        ''' <summary>Gets or sets the maximum value for the spin box (also known as an up-down control).</summary>
        ''' <returns>The maximum value for the spin box. The default value is 100.</returns>
        ''' <seelaso cref="NumericUpDown.Maximum"/>
        <WinFormsSRDescriptionAttribute("NumericUpDownMaximumDescr")> _
        <RefreshProperties(RefreshProperties.All), Category("Data")> _
        <DefaultValue(GetType(Decimal), "100")> _
        Public Property Maximum() As Decimal
            Get
                Return NumericUpDown.Maximum
            End Get
            Set(ByVal value As Decimal)
                NumericUpDown.Maximum = value
            End Set
        End Property
        ''' <summary>Gets or sets the minimum allowed value for the spin box (also known as an up-down control).</summary>
        ''' <returns>The minimum allowed value for the spin box. The default value is 0.</returns>
        ''' <seelaso cref="NumericUpDown.Minimum"/>
        <WinFormsSRDescriptionAttribute("NumericUpDownMinimumDescr")> _
        <Category("Data"), RefreshProperties(RefreshProperties.All)> _
        <DefaultValue(GetType(Decimal), "0")> _
        Public Property Minimum() As Decimal
            Get
                Return NumericUpDown.Minimum
            End Get
            Set(ByVal value As Decimal)
                NumericUpDown.Minimum = value
            End Set
        End Property
        ''' <summary>Gets or sets the text to be displayed in the <see cref="System.Windows.Forms.NumericUpDown"/> control.</summary>
        ''' <seelaso cref="NumericUpDown.Text"/>
        <Browsable(False), Bindable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Property [Text]() As String
            Get
                Return MyBase.Text
            End Get
            Set(ByVal value As String)
                MyBase.Text = value
            End Set
        End Property
        ''' <summary>Gets or sets a value indicating whether a thousands separator is displayed in the spin box (also known as an up-down control) when appropriate.</summary>
        ''' <returns>true if a thousands separator is displayed in the spin box when appropriate; otherwise, false. The default value is false.</returns>
        ''' <seelaso cref="NumericUpDown.ThousandsSeparator"/>
        <WinFormsSRDescriptionAttribute("NumericUpDownThousandsSeparatorDescr")> _
        <Category("Data"), DefaultValue(False), Localizable(True)> _
        Public Property ThousandsSeparator() As Boolean
            Get
                Return NumericUpDown.ThousandsSeparator
            End Get
            Set(ByVal value As Boolean)
                NumericUpDown.ThousandsSeparator = value
            End Set
        End Property
        ''' <summary>Gets or sets the value assigned to the spin box (also known as an up-down control).</summary>
        ''' <returns>The numeric value of the <see cref="System.Windows.Forms.NumericUpDown"/> control.</returns>
        ''' <exception cref="System.ArgumentOutOfRangeException">The assigned value is less than the <see cref="System.Windows.Forms.NumericUpDown.Minimum"/> property value.-or- The assigned value is greater than the <see cref="System.Windows.Forms.NumericUpDown.Maximum"/> property value.</exception>
        ''' <seelaso cref="NumericUpDown.Value"/>
        <WinFormsSRDescriptionAttribute("NumericUpDownValueDescr")> _
        <Bindable(True), Category("Appearance")> _
        <DefaultValue(GetType(Decimal), "0")> _
        Public Property Value() As Decimal
            Get
                Return NumericUpDown.Value
            End Get
            Set(ByVal value As Decimal)
                NumericUpDown.Value = value
            End Set
        End Property

#End Region
        ''' <summary>Gets or sets the <see cref="System.Windows.Forms.BindingContext"/> for the control.</summary>
        ''' <returns>A <see cref="System.Windows.Forms.BindingContext"/> for the control</returns>
        Public Property BindingContext() As System.Windows.Forms.BindingContext Implements System.Windows.Forms.IBindableComponent.BindingContext
            Get
                Return NumericUpDown.BindingContext
            End Get
            Set(ByVal value As System.Windows.Forms.BindingContext)
                NumericUpDown.BindingContext = value
            End Set
        End Property

        <Description("Fired when BindingContextChanged of internal ComboBox occurs"), Category("Data")> _
        Public Event BindingContextChanged(ByVal sender As Object, ByVal e As EventArgs)
        ''' <summary>Handles the <see cref="NumericUpDown"/>.<see cref="NumericUpDown.BindingContextChanged">BindingContextChanged</see> event</summary>
        ''' <param name="sender"><see cref="NumericUpDown"/></param>
        ''' <param name="e">Event arguments</param>
        Private Sub NumericUpDown_BindingContextChanged(ByVal sender As Object, ByVal e As EventArgs)
            OnBindingContextChanged(e)
        End Sub
        ''' <summary>Raises the <see cref="BindingContextChanged"/> event</summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnBindingContextChanged(ByVal e As EventArgs)
            RaiseEvent BindingContextChanged(Me, e)
        End Sub

        ''' <summary>Gets the data bindings for the control.</summary>
        ''' <returns>A <see cref="System.Windows.Forms.ControlBindingsCollection"/> that contains the <see cref="System.Windows.Forms.Binding"/> objects for the control.</returns>
        ''' <seelaso cref="NumericUpDown.DataBindings"/>
        <WinFormsSRDescriptionAttribute("ControlBindingsDescr")> _
        <ParenthesizePropertyName(True), RefreshProperties(RefreshProperties.All), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Data")> _
        Public ReadOnly Property DataBindings() As ControlBindingsCollection Implements System.Windows.Forms.IBindableComponent.DataBindings
            Get
                Return NumericUpDown.DataBindings
            End Get
        End Property
    End Class
End Namespace