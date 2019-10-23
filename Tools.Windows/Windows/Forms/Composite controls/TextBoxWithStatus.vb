Imports System.Windows.Forms
Namespace WindowsT.FormsT
    'Stage:Nightly
    'xASAP: Comment,Attributes,  Expose everything, inherit ControlWithStatus, conditional file
    'ASAP: Remove
    ''' <summary>Note: This control will be removed and replaced with attachable implementation of <see cref="StatusMarker"/></summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    <Obsolete("This control will be removed and replaced wit attachable StatusMarker")> _
    <DefaultProperty("Text")> _
    <DefaultEvent("TextChanged")> _
     <Version(1, 0, GetType(TextBoxWithStatus), LastChange:="07/22/2007")> _
    <FirstVersion("06/22/2007")> _
    <Drawing.ToolboxBitmap(GetType(StatusMarker), "TextBoxWithStatus.bmp")> _
    <ComponentModelT.Prefix("tws")> _
    Public Class TextBoxWithStatus
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
    End Class
End Namespace

