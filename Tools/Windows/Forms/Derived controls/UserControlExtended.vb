Imports System.Windows.Forms, Tools.ComponentModelT
Namespace WindowsT.FormsT
#If Config <= Nightly Then 'Stage:Nightly
    ''' <summary>Provides empty base class for user controls that extends <see cref="UserControl"/> by adding several features.</summary>
    <Author("Đonny", "dzonny@dzonny.cz", "dzonny.cz")> _
    <Version(1, 0, GetType(UserControlExtended), LastChange:="02/11/2008")> _
    <FirstVersion("02/11/2008")> _
    Public Class UserControlExtended
        Inherits UserControl
        ''' <summary>CTor</summary>
        Public Sub New()
            ResetKeyPreview()
        End Sub

        'Public Overrides Function PreProcessMessage(ByRef msg As System.Windows.Forms.Message) As Boolean
        '    Return MyBase.PreProcessMessage(msg)
        'End Function
        ''' <summary>Contains value of the <see cref="KeyPreview"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _KeyPreview As Boolean
        ''' <summary>Gets or sets a value indicating whether the user control will receive key events before the event is passed to the control that has focus.</summary>
        ''' <value>true if the user control will receive all key events; false if the currently selected user control on the control receives key events. The default is false.</value>
        ''' <remarks>
        ''' <para>When this property is set to true, the user control will receive all <see cref="KeyPress"/>, <see cref="KeyDown"/>, and <see cref="KeyUp"/> events. After the user controls's event handlers have completed processing the keystroke, the keystroke is then assigned to the control with focus. For example, if the <see cref="KeyPreview"/> property is set to true and the currently selected control is a <see cref="TextBox"/>, after the keystroke is handled by the event handlers of the user control the <see cref="TextBox"/> control will receive the key that was pressed. To handle keyboard events only at the user control level and not allow controls to receive keyboard events, set the <see cref="KeyPressEventArgs.Handled"/> property in your user controls's <see cref="KeyPress"/> event handler to true.</para>
        ''' <para>You can use this property to process most keystrokes in your user control and either handle the keystroke or call the appropriate control to handle the keystroke. For example, when a user control uses function keys, you might want to process the keystrokes at the user-control level rather than writing code for each control that might receive keystroke events.</para>
        ''' <para>If a user control has no visible or enabled controls, it automatically receives all keyboard events.</para>
        ''' <para>The described behavior is realized in overriden <see cref="PreProcessMessage"/>.</para>
        ''' <para>For more details see <seealso cref="Form.KeyPreview"/>. It works in the same way.</para>
        ''' <para>When you are using derived control that inherits <see cref="UserControlExtended"/> and has nativelly set this property to true, but this behavior interfers with functionality of your form, you can set this property to false (if it is not restricted by creator of derived control) to avoid such interference but with risk that some keyboard-driven functions of the controll will not work.</para>
        ''' <para>Note for inheritors: You do not need to override this property (even you cannot) in order to change its design-time default value. You can simply set value of <see cref="KeyPreviewDefaultValue"/> in CTor. You must also set same default value to <see cref="KeyPreview"/> in CTor.</para>
        ''' </remarks>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <LDescription(GetType(WindowsT.FormsT.DerivedControls), "KeyPreview_d")> _
        Public Overridable Property KeyPreview() As Boolean
            Get
                Return _KeyPreview
            End Get
            Set(ByVal value As Boolean)
                _KeyPreview = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="KeyPreviewDefaultValue"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _KeyPreviewDefaultValue As Boolean = False
        ''' <summary>Contains default value of <see cref="KeyPreview"/> property</summary>
        ''' <remarks>This default value is used by various tools (like Visual Studion WinForms designer) to indicate if value of property have been changed and thus must be serialized.
        ''' You can change value of this in CTor of derived class in order to change design-time behaviour of your derived control</remarks>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <DefaultValue(False)> _
        <LDescription(GetType(WindowsT.FormsT.DerivedControls),"KeyPreviewDefaultValue_d")> _
        Protected Property KeyPreviewDefaultValue() As Boolean
            Get
                Return _KeyPreviewDefaultValue
            End Get
            Set(ByVal value As Boolean)
                _KeyPreviewDefaultValue = value
            End Set
        End Property
        ''' <summary>Gets value indicating if value of the <see cref="KeyPreview"/> property should be serialized (means it has different then default value)</summary>
        Private Function ShouldSerializeKeyPreview() As Boolean
            Return KeyPreview <> KeyPreviewDefaultValue
        End Function
        ''' <summary>Resets value of the <see cref="KeyPreview"/> property to its default value</summary>
        Private Sub ResetKeyPreview()
            KeyPreview = KeyPreviewDefaultValue
        End Sub

        ''' <summary>Previews a keyboard message.</summary>
        ''' <returns>true if the message was processed by the control; otherwise, false.</returns>
        ''' <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
        Protected Overrides Function ProcessKeyPreview(ByRef m As System.Windows.Forms.Message) As Boolean
            Return ((KeyPreview AndAlso Me.ProcessKeyEventArgs(m)) OrElse MyBase.ProcessKeyPreview(m))
        End Function
    End Class
#End If
End Namespace
