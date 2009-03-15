Imports System.ComponentModel
Imports System.Windows.Forms

Namespace GUI
    ''' <summary><see cref="DateTimePicker"/> capable of displaying null values</summary>
    ''' <remarks>User can set value of null by pressing delete</remarks>
    <DefaultBindingProperty("NullableValue")> _
    <DefaultProperty("Value")> _
    Public Class DateTimePickerNullable : Inherits DateTimePicker
        ''' <summary>CTor</summary>
        Public Sub New()
            _Format = DateTimePickerFormat.Short
            _CustomFormat = ""
            _NullFormat = ""
            IsValueNull = True
            ApplyFormat()
        End Sub
        ''' <summary>Applyes format according to <see cref="Value"/></summary>
        Private Sub ApplyFormat()
            IgnoreSetNullableValue = True
            Try
                If IsValueNull Then
                    MyBase.CustomFormat = "'" & _NullFormat.Replace("'", "\'") & "'"
                    MyBase.Format = DateTimePickerFormat.Custom
                Else
                    MyBase.CustomFormat = _CustomFormat
                    MyBase.Format = _Format
                End If
            Finally
                IgnoreSetNullableValue = False
            End Try
        End Sub
        ''' <summary>Contains value indicating if value is null</summary>
        Private IsValueNull As Boolean = True
        ''' <summary>Gets or sets the date/time value assigned to the control.</summary>
        ''' <returns>The System.DateTime value assign to the control</returns>
        ''' <remarks>Do not use this property for data binding. Use <see cref="NullableValue"/> instead</remarks>
        <Bindable(False)> _
        <DefaultValue(GetType(Nullable(Of Date)), "Null")> _
        <RefreshProperties(RefreshProperties.All), Description("Gets or sets the date/time value assigned to the control."), Category("Behavior")> _
        Public Shadows Property Value() As Nullable(Of Date)
            Get
                If IsValueNull Then Return Nothing Else Return MyBase.Value
            End Get
            Set(ByVal value As Nullable(Of Date))
                If value.HasValue Then
                    IsValueNull = False
                    MyBase.Value = value.Value
                    IsValueNull = False
                Else
                    IsValueNull = True
                End If
                ApplyFormat()
            End Set
        End Property
        ''' <summary>True whan setter of <see cref="NullableValue"/> is ignored</summary>
        Private IgnoreSetNullableValue As Boolean = False
        ''' <summary>Proxy of <see cref="Value"/> for data binding</summary>
        ''' <remarks>Use this property only for data binding. Use <see cref="Value"/> instead.</remarks>
        <Category("Data")> _
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), Bindable(True)> _
        <DefaultValue(GetType(Nullable(Of Date)), "Null")> _
        <RefreshProperties(RefreshProperties.All), Description("Use only for data binding! Use Value instead.")> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property NullableValue() As Nullable(Of Date)
            Get
                Return Value
            End Get
            Set(ByVal value As Nullable(Of Date))
                If Not IgnoreSetNullableValue Then Me.Value = value
            End Set
        End Property
        ''' <summary>Do not use this property</summary>
        <Browsable(False), Bindable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shadows Property Text() As String
            Get
                Return MyBase.Text
            End Get
            Set(ByVal value As String)
                MyBase.Text = value
            End Set
        End Property
        ''' <summary>Raises the <see cref="System.Windows.Forms.DateTimePicker.ValueChanged"/> event.</summary>
        ''' <param name="eventargs">An <see cref="System.EventArgs"/> that contains the event data.</param>
        Protected Overrides Sub OnValueChanged(ByVal eventargs As System.EventArgs)
            Dim WasValueNull As Boolean = IsValueNull
            MyBase.OnValueChanged(eventargs)
            IsValueNull = False
            If WasValueNull Then My.Computer.Keyboard.SendKeys(ChrW(Keys.Escape)) 'To je ale sedmiprasárna
            ApplyFormat()
        End Sub
        ''' <summary>Contains value of the <see cref="CustomFormat"/> property</summary>
        Private _CustomFormat As String
        ''' <summary>Gets or sets the custom date/time format string.</summary>
        ''' <returns>A string that represents the custom date/time format. The default is null.</returns>
        <Description(">Gets or sets the custom date/time format string.")> _
        <RefreshProperties(System.ComponentModel.RefreshProperties.Repaint), Category("Appearance")> _
        <DefaultValue("")> _
        Public Shadows Property CustomFormat() As String
            Get
                Return _CustomFormat
            End Get
            Set(ByVal value As String)
                _CustomFormat = value
                ApplyFormat()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="NullFormat"/> property</summary>
        Private _NullFormat As String
        ''' <summary>String displayed when <see cref="Value"/> is null</summary>
        <DefaultValue("")> _
        <Description("String displayed when Value is null")> _
        <RefreshProperties(System.ComponentModel.RefreshProperties.Repaint), Category("Appearance")> _
        Public Shadows Property NullFormat() As String
            Get
                Return _NullFormat
            End Get
            Set(ByVal value As String)
                _NullFormat = value
                ApplyFormat()
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="Format"/> property</summary>
        Private _Format As DateTimePickerFormat
        ''' <summary>Gets or sets the format of the date and time displayed in the control.</summary> 
        ''' <returns>One of the <see cref="System.Windows.Forms.DateTimePickerFormat"/> values. The default is System.Windows.Forms.DateTimePickerFormat.Long.</returns>
        ''' <exception cref="System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the System.Windows.Forms.DateTimePickerFormat values.</exception>
        <RefreshProperties(System.ComponentModel.RefreshProperties.Repaint), Category("Appearance")> _
        <Description("Gets or sets the format of the date and time displayed in the control.")> _
        <DefaultValue(GetType(DateTimePickerFormat), "Short")> _
        Public Shadows Property Format() As DateTimePickerFormat
            Get
                Return _Format
            End Get
            Set(ByVal value As DateTimePickerFormat)
                _Format = value
                ApplyFormat()
            End Set
        End Property

        Private Sub DateTimePickerNullable_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
            If e.KeyCode = Keys.Delete Then
                IsValueNull = True
                ApplyFormat()
            End If
        End Sub
    End Class
End Namespace