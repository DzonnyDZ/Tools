Imports System.Windows.Forms
Namespace WindowsT.FormsT
    '#If Config <= Nightly Then set in Tools.vbproj
    'Stage:Nightly
    'ASAP: Comment,Attributes,Expose everything, inherit ControlWithStatus, Conditional file
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(ComboWithStatus), LastChMMDDYYYY:="07/22/2007")> _
    <Tool(GetType(StatusMarker), FirstVerMMDDYYYY:="06/22/2007")> _
    <Drawing.ToolboxBitmap(GetType(StatusMarker), "ComboWithStatus.bmp")> _
    <ComponentModelT.Prefix("cws")> _
    <DefaultProperty("Text")> _
    <DefaultEvent("TextChanged")> _
    Public Class ComboWithStatus
        Public Overrides Property Text() As String
            Get
                Return cmbCombo.Text
            End Get
            Set(ByVal value As String)
                cmbCombo.Text = value
            End Set
        End Property
        Private Sub cmbCombo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCombo.TextChanged
            OnTextChanged(e)
        End Sub

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        Public ReadOnly Property Status() As StatusMarker
            Get
                Return stmStatus
            End Get
        End Property
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property SelectedIndex() As Integer
            Get
                Return cmbCombo.SelectedIndex
            End Get
            Set(ByVal value As Integer)
                cmbCombo.SelectedIndex = value
            End Set
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        Public ReadOnly Property Items() As ComboBox.ObjectCollection
            Get
                Return cmbCombo.Items
            End Get
        End Property
        Public Property DropDownStyle() As ComboBoxStyle
            Get
                Return cmbCombo.DropDownStyle
            End Get
            Set(ByVal value As ComboBoxStyle)
                cmbCombo.DropDownStyle = value
            End Set
        End Property
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property SelectedItem() As Object
            Get
                Return cmbCombo.SelectedItem
            End Get
            Set(ByVal value As Object)
                cmbCombo.SelectedItem = value
            End Set
        End Property

        Public Event SelectdIndexChanged As EventHandler


        Private Sub cmbCombo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCombo.SelectedIndexChanged
            OnSelectedIndexChanged(e)
        End Sub
        Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
            ApplyAutoCahnge()
            MyBase.OnTextChanged(e)
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
        Protected Overridable Sub OnSelectedIndexChanged(ByVal e As EventArgs)
            ApplyAutoCahnge()
            RaiseEvent SelectdIndexChanged(Me, e)
        End Sub
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public ReadOnly Property ComboBox() As ComboBox
            Get
                Return cmbCombo
            End Get
        End Property
    End Class
End Namespace

