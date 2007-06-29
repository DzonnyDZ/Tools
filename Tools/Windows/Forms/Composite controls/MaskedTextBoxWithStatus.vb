Imports System.Windows.Forms
Namespace WindowsT.FormsT
#If Config <= Nightly Then 'Stage:Nightly
    'ASAP: Comment, mark, Forum, Wiki,Bitmap
    'TODO: Attributes
    'TODO: Expose everything
    <DefaultProperty("Text")> _
    <DefaultEvent("TextChanged")> _
    Public Class MaskedTextBoxWithStatus
        Public Overrides Property Text() As String
            Get
                Return mtbText.Text
            End Get
            Set(ByVal value As String)
                mtbText.Text = value
            End Set
        End Property
        Private Sub txtText_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mtbText.TextChanged
            OnTextChanged(e)
        End Sub
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        Public ReadOnly Property Status() As StatusMarker
            Get
                Return stmStatus
            End Get
        End Property
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public ReadOnly Property TextBox() As MaskedTextBox
            Get
                Return mtbText
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
        <Localizable(True), Category(Tools.WindowsT.FormsT.UtilitiesT.CategoryAttributeValues.Behavior)> _
        <Editor("System.Windows.Forms.Design.MaskPropertyEditor", GetType(Drawing.Design.UITypeEditor))> _
        <RefreshProperties(RefreshProperties.Repaint), DefaultValue("")> _
        Public Property Mask() As String
            Get
                Return mtbText.Mask
            End Get
            Set(ByVal value As String)
                mtbText.Mask = value
            End Set
        End Property
        <RefreshProperties(RefreshProperties.Repaint), Category(UtilitiesT.CategoryAttributeValues.Behavior), DefaultValue(False)> _
        Public Property HidePromptOnLeave() As Boolean
            Get
                Return mtbText.HidePromptOnLeave
            End Get
            Set(ByVal value As Boolean)
                mtbText.HidePromptOnLeave = value
            End Set
        End Property
        <DefaultValue(False), Category(UtilitiesT.CategoryAttributeValues.Behavior)> _
        Public Property BeepOnError() As Boolean
            Get
                Return mtbText.BeepOnError
            End Get
            Set(ByVal value As Boolean)
                mtbText.BeepOnError = value
            End Set
        End Property
        <Localizable(True), Category(UtilitiesT.CategoryAttributeValues.Appearance), DefaultValue("_"c), RefreshProperties(RefreshProperties.Repaint)> _
         Public Property PromptChar() As Char
            Get
                Return mtbText.PromptChar
            End Get
            Set(ByVal value As Char)
                mtbText.PromptChar = value
            End Set
        End Property

        <Category(UtilitiesT.CategoryAttributeValues.Behavior), DefaultValue(False)> _
        Public Property RejectInputOnFirstFailure() As Boolean
            Get
                Return mtbText.RejectInputOnFirstFailure
            End Get
            Set(ByVal value As Boolean)
                mtbText.RejectInputOnFirstFailure = value
            End Set
        End Property
        <Category(UtilitiesT.CategoryAttributeValues.Behavior), DefaultValue(True)> _
        Public Property ResetOnPrompt() As Boolean
            Get
                Return mtbText.ResetOnPrompt
            End Get
            Set(ByVal value As Boolean)
                mtbText.ResetOnPrompt = value
            End Set
        End Property

        <DefaultValue(True), Category(UtilitiesT.CategoryAttributeValues.Behavior)> _
        Public Property ResetOnSpace() As Boolean
            Get
                Return mtbText.ResetOnSpace
            End Get
            Set(ByVal value As Boolean)
                mtbText.ResetOnSpace = value
            End Set
        End Property
        <DefaultValue(True), Category(UtilitiesT.CategoryAttributeValues.Behavior)> _
        Public Property SkipLiterals() As Boolean
            Get
                Return mtbText.SkipLiterals
            End Get
            Set(ByVal value As Boolean)
                mtbText.SkipLiterals = value
            End Set
        End Property
        <Category(UtilitiesT.CategoryAttributeValues.Behavior), DefaultValue(2), RefreshProperties(RefreshProperties.Repaint)> _
        Public Property TextMaskFormat() As MaskFormat
            Get
                Return mtbText.TextMaskFormat
            End Get
            Set(ByVal value As MaskFormat)
                mtbText.TextMaskFormat = value
            End Set
        End Property

    End Class
#End If
End Namespace

