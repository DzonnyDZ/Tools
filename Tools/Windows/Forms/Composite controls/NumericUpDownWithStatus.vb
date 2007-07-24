Imports System.Windows.Forms
Namespace WindowsT.FormsT
    '#If Config <= Nightly Then set in Tools.vbproj
    'Stage:Nightly
    'ASAP: Comment,  Attributes,Expose everything  , inherit ControlWithStatus , conditional file
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(NumericUpDownWithStatus), LastChMMDDYYYY:="07/22/2007")> _
    <Tool(GetType(StatusMarker), FirstVerMMDDYYYY:="06/22/2007")> _
    <Drawing.ToolboxBitmap(GetType(StatusMarker), "NumericUpDownWithStatus.bmp")> _
    <ComponentModelT.Prefix("nws")> _
    <DefaultProperty("Value")> _
    <DefaultEvent("ValueChanged")> _
    Public Class NumericUpDownWithStatus
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property Text() As String
            Get
                Return nudNumber.Text
            End Get
            Set(ByVal value As String)
                nudNumber.Text = value
            End Set
        End Property
        Private Sub txtText_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            OnTextChanged(e)
        End Sub
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        Public ReadOnly Property Status() As StatusMarker
            Get
                Return stmStatus
            End Get
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public ReadOnly Property TextBox() As NumericUpDown
            Get
                Return nudNumber
            End Get
        End Property
        Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
            MyBase.OnTextChanged(e)
        End Sub
        Public Event ValueChanged As EventHandler
        Protected Overridable Sub OnValueChanged(ByVal e As EventArgs)
            ApplyAutoCahnge()
            RaiseEvent ValueChanged(Me, e)
        End Sub
        Private Sub ApplyAutoCahnge()
            If AutoChange Then
                Select Case stmStatus.Status
                    Case StatusMarker.Statuses.Deleted, StatusMarker.Statuses.Error, StatusMarker.Statuses.NA, StatusMarker.Statuses.Normal
                        stmStatus.Status = StatusMarker.Statuses.Changed
                    Case StatusMarker.Statuses.Null
                        stmStatus.Status = StatusMarker.Statuses.[New]
                End Select
            End If
        End Sub
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _AutoChange As Boolean = True
        <DefaultValue(True)> _
        Public Property AutoChange() As Boolean
            Get
                Return _AutoChange
            End Get
            Set(ByVal value As Boolean)
                _AutoChange = value
            End Set
        End Property

        Private Sub nudNumber_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nudNumber.ValueChanged
            OnValueChanged(e)
        End Sub


        <RefreshProperties(RefreshProperties.All), Category(UtilitiesT.CategoryAttributeValues.Data)> _
    Public Property Maximum() As Decimal
            Get
                Return nudNumber.Maximum
            End Get
            Set(ByVal value As Decimal)
                nudNumber.Maximum = value
            End Set
        End Property

        <Category(UtilitiesT.CategoryAttributeValues.Data), RefreshProperties(RefreshProperties.All)> _
        Public Property Minimum() As Decimal
            Get
                Return nudNumber.Minimum
            End Get
            Set(ByVal value As Decimal)
                nudNumber.Minimum = value
            End Set
        End Property

        <Category(UtilitiesT.CategoryAttributeValues.Data)> _
        Public Property Increment() As Decimal
            Get
                Return nudNumber.Increment
            End Get
            Set(ByVal value As Decimal)
                nudNumber.Increment = value
            End Set
        End Property

        <Category(UtilitiesT.CategoryAttributeValues.Data), DefaultValue(0)> _
        Public Property DecimalPlaces() As Integer
            Get
                Return nudNumber.DecimalPlaces
            End Get
            Set(ByVal value As Integer)
                nudNumber.DecimalPlaces = value
            End Set
        End Property

        <Category(UtilitiesT.CategoryAttributeValues.Data), Bindable(True)> _
        Public Property Value() As Decimal
            Get
                Return nudNumber.Value
            End Get
            Set(ByVal value As Decimal)
                nudNumber.Value = value
            End Set
        End Property
    End Class
End Namespace

