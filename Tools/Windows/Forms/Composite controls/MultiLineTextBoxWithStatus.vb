Imports System.Windows.Forms
Namespace WindowsT.FormsT
#If Config <= Nightly Then 'Stage:Nightly
    'ASAP: Comment, mark, Forum, Wiki , bitmap
    'TODO: Attributes
    'TODO: Expose everything
    <DefaultProperty("Text")> _
    <DefaultEvent("TextChanged")> _
    Public Class MultiLineTextBoxWithStatus
        Public Overrides Property Text() As String
            Get
                Return txtText.Text
            End Get
            Set(ByVal value As String)
                txtText.Text = value
            End Set
        End Property
        Private Sub txtText_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtText.TextChanged
            OnTextChanged(e)
        End Sub
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        Public ReadOnly Property Status() As StatusMarker
            Get
                Return stmStatus
            End Get
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
       Public ReadOnly Property TextBox() As TextBox
            Get
                Return txtText
            End Get
        End Property
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
        <Localizable(True), DefaultValue(0), Category(UtilitiesT.CategoryAttributeValues.Appearance)> _
        Public Property ScrollBars() As ScrollBars
            Get
                Return txtText.ScrollBars
            End Get
            Set(ByVal value As ScrollBars)
                txtText.ScrollBars = value
            End Set
        End Property
        <DefaultValue(True), Category(UtilitiesT.CategoryAttributeValues.Appearance), Localizable(True)> _
        Public Property WordWrap() As Boolean
            Get
                Return txtText.WordWrap
            End Get
            Set(ByVal value As Boolean)
                txtText.WordWrap = value
            End Set
        End Property
    End Class
#End If
End Namespace

