Imports System.Windows.Forms, Tools.ResourcesT, Tools.ComponentModelT, System.Drawing.Design
Namespace WindowsT.FormsT
    '#If Config <= Nightly Then set in Tools.vbproj
    'Stage:Nightly
    'ASAP: Comment,Attributes, Expose everything
    ''' <summary>Composite control of <see cref="MaskedTextBox"/> and <see cref="StatusMarker"/></summary>
    <DefaultProperty("Text")> _
    <DefaultEvent("TextChanged")> _
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(MaskedTextBoxWithStatus), LastChMMDDYYYY:="07/22/2007")> _
    <Tool(GetType(StatusMarker), FirstVerMMDDYYYY:="06/22/2007")> _
    <Drawing.ToolboxBitmap(GetType(StatusMarker), "MaskedTextBoxWithStatus.bmp")> _
    <ComponentModelT.Prefix("mxs")> _
    Public Class MaskedTextBoxWithStatus : Inherits ControlWithStatus
#Region "TextBox"
        ''' <summary><see cref="MaskedTextBox"/> that realizes this control</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public ReadOnly Property TextBox() As MaskedTextBox
            Get
                Return mtbText
            End Get
        End Property
#Region "Events"
        ''' <summary>Occurs after the insert mode has changed. </summary>
        <SRCategory("CatPropertyChanged"), SRDescription("MaskedTextBoxIsOverwriteModeChangedDescr")> _
        Public Event IsOverwriteModeChanged As EventHandler
        ''' <summary>Occurs after the input mask is changed.</summary>
        <SRDescription("MaskedTextBoxMaskChangedDescr"), SRCategory("CatPropertyChanged")> _
        Public Event MaskChanged As EventHandler
        ''' <summary>Occurs when the user's input or assigned character does not match the corresponding format element of the input mask.</summary>
        <SRCategory("CatBehavior"), SRDescription("MaskedTextBoxMaskInputRejectedDescr")> _
        Public Event MaskInputRejected As MaskInputRejectedEventHandler
        ''' <summary>Typically occurs when the value of the <see cref="P:System.Windows.Forms.MaskedTextBox.Multiline"></see> property has changed; however, this event is not raised by <see cref="T:System.Windows.Forms.MaskedTextBox"></see>.</summary>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
        Public Event MultilineChanged As EventHandler
        ''' <summary>Occurs when the text alignment is changed. </summary>
        <SRCategory("CatPropertyChanged"), SRDescription("RadioButtonOnTextAlignChangedDescr")> _
        Public Event TextAlignChanged As EventHandler
        ''' <summary>Occurs when <see cref="T:System.Windows.Forms.MaskedTextBox"></see> has finished parsing the current value using the <see cref="P:System.Windows.Forms.MaskedTextBox.ValidatingType"></see> property.</summary>
        <SRCategory("CatFocus"), SRDescription("MaskedTextBoxTypeValidationCompletedDescr")> _
        Public Event TypeValidationCompleted As TypeValidationEventHandler
        ''' <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.HideSelection"></see> property has changed.</summary>
        <SRDescription("TextBoxBaseOnHideSelectionChangedDescr"), SRCategory("CatPropertyChanged")> _
        Public Event HideSelectionChanged As EventHandler
        ''' <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.Modified"></see> property has changed.</summary>
        <SRDescription("TextBoxBaseOnModifiedChangedDescr"), SRCategory("CatPropertyChanged")> _
        Public Event ModifiedChanged As EventHandler
        ''' <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.ReadOnly"></see> property has changed.</summary>
        <SRDescription("TextBoxBaseOnReadOnlyChangedDescr"), SRCategory("CatPropertyChanged")> _
        Public Event ReadOnlyChanged As EventHandler
        ''' <summary>Occurs when the control is finished validating.</summary>
        <SRDescription("ControlOnValidatedDescr"), SRCategory("CatFocus")> _
        Public Event TextBoxValidated As EventHandler
        ''' <summary>Occurs when the control is validating.</summary>
        <SRDescription("ControlOnValidatingDescr"), SRCategory("CatFocus")> _
        Public Event TextBoxValidating As CancelEventHandler
        Private Sub mtbText_ModifiedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mtbText.ModifiedChanged
            OnModifiedChanged(e)
        End Sub
        Private Sub mtbText_ReadOnlyChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mtbText.ReadOnlyChanged
            OnReadOnlyChanged(e)
        End Sub
        Private Sub mtbText_IsOverwriteModeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mtbText.IsOverwriteModeChanged
            OnIsOverwriteModeChanged(e)
        End Sub
        Private Sub mtbText_MaskChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mtbText.MaskChanged
            OnMaskChanged(e)
        End Sub
        Private Sub mtbText_MaskInputRejected(ByVal sender As Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles mtbText.MaskInputRejected
            OnMaskInputRejected(e)
        End Sub
        Private Sub mtbText_TextAlignChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mtbText.TextAlignChanged
            OnTextAlignChanged(e)
        End Sub
        Private Sub mtbText_TypeValidationCompleted(ByVal sender As Object, ByVal e As System.Windows.Forms.TypeValidationEventArgs) Handles mtbText.TypeValidationCompleted
            OnTypeValidationCompleted(e)
        End Sub
        Private Sub mtbText_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles mtbText.Validated
            OnTextBoxValidated(e)
        End Sub
        Private Sub mtbText_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mtbText.Validating
            OnTextBoxValidating(e)
        End Sub
        ''' <summary>Raises the <see cref="TextBoxValidated"/> event</summary>
        ''' <param name="e">event arguments</param>
        Protected Overridable Sub OnTextBoxValidated(ByVal e As EventArgs)
            RaiseEvent TextBoxValidated(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="textBoxValidating"/> event</summary>
        ''' <param name="e">event arguments</param>
        Protected Overridable Sub OnTextBoxValidating(ByVal e As CancelEventArgs)
            RaiseEvent TextBoxValidating(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="ModifiedChanged"/> event</summary>
        ''' <param name="e">event arguments</param>
        Protected Overridable Sub OnModifiedChanged(ByVal e As System.EventArgs)
            RaiseEvent ModifiedChanged(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="ReadOnlyChanged"/> event</summary>
        ''' <param name="e">event arguments</param>
        Protected Overridable Sub OnReadOnlyChanged(ByVal e As System.EventArgs)
            RaiseEvent ReadOnlyChanged(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="IsOverwriteModeChanged"/> event</summary>
        ''' <param name="e">event arguments</param>
        Protected Overridable Sub OnIsOverwriteModeChanged(ByVal e As System.EventArgs)
            RaiseEvent IsOverwriteModeChanged(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="MaskChanged"/> event</summary>
        ''' <param name="e">event arguments</param>
        Protected Overridable Sub OnMaskChanged(ByVal e As System.EventArgs)
            RaiseEvent MaskChanged(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="MaskInputRejected"/> event</summary>
        ''' <param name="e">event arguments</param>
        Protected Overridable Sub OnMaskInputRejected(ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs)
            RaiseEvent MaskInputRejected(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="TextAlignChanged"/> event</summary>
        ''' <param name="e">event arguments</param>
        Protected Overridable Sub OnTextAlignChanged(ByVal e As System.EventArgs)
            RaiseEvent TextAlignChanged(Me, e)
        End Sub
        ''' <summary>Raises the <see cref="TypeValidationCompleted"/> event</summary>
        ''' <param name="e">event arguments</param>
        Protected Overridable Sub OnTypeValidationCompleted(ByVal e As System.Windows.Forms.TypeValidationEventArgs)
            RaiseEvent TypeValidationCompleted(Me, e)
        End Sub
#End Region
        ''' <summary>Gets or sets a value indicating whether <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar"></see> can be entered as valid data by the user. </summary>
        ''' <returns>true if the user can enter the prompt character into the control; otherwise, false. The default is true. </returns>
        <SRCategory("CatBehavior"), DefaultValue(True), SRDescription("MaskedTextBoxAllowPromptAsInputDescr")> _
        Public Property AllowPromptAsInput() As Boolean
            Get
                Return TextBox.AllowPromptAsInput
            End Get
            Set(ByVal value As Boolean)
                TextBox.AllowPromptAsInput = value
            End Set
        End Property
        ''' <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MaskedTextBox"></see> control accepts characters outside of the ASCII character set.</summary>
        ''' <returns>true if only ASCII is accepted; false if the <see cref="T:System.Windows.Forms.MaskedTextBox"></see> control can accept any arbitrary Unicode character. The default is false.</returns>
        <SRCategory("CatBehavior"), SRDescription("MaskedTextBoxAsciiOnlyDescr"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(False)> _
        Public Property AsciiOnly() As Boolean
            Get
                Return TextBox.AsciiOnly
            End Get
            Set(ByVal value As Boolean)
                TextBox.AsciiOnly = value
            End Set
        End Property

        ''' <summary>Gets or sets a value indicating whether the masked text box control raises the system beep for each user key stroke that it rejects.</summary>
        ''' <returns>true if the <see cref="T:System.Windows.Forms.MaskedTextBox"></see> control should beep on invalid input; otherwise, false. The default is false.</returns>
        ''' <filterpriority>1</filterpriority>
        <DefaultValue(False), SRCategory("CatBehavior"), SRDescription("MaskedTextBoxBeepOnErrorDescr")> _
        Public Property BeepOnError() As Boolean
            Get
                Return TextBox.BeepOnError
            End Get
            Set(ByVal value As Boolean)
                TextBox.BeepOnError = value
            End Set
        End Property


        ''' <summary>Gets or sets the border type of the text box control.</summary>
        ''' <returns>A <see cref="T:System.Windows.Forms.BorderStyle"></see> that represents the border type of the text box control. The default is Fixed3D.</returns>
        ''' <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property. </exception>
        <SRCategory("CatAppearance"), DefaultValue(2), SRDescription("TextBoxBorderDescr")> _
        Public Property TextBoxBorderStyle() As BorderStyle
            Get
                Return TextBox.BorderStyle
            End Get
            Set(ByVal value As BorderStyle)
                TextBox.BorderStyle = value
            End Set
        End Property

        ''' <summary>Gets or sets a value indicating whether the control causes validation to be performed on any controls that require validation when it receives focus.</summary>
        ''' <returns>true if the control causes validation to be performed on any controls requiring validation when it receives focus; otherwise, false. The default is true.</returns>
        <SRDescription("ControlCausesValidationDescr"), DefaultValue(True), SRCategory("CatFocus")> _
        Public Property TextBoxCausesValidation() As Boolean
            Get
                Return TextBox.CausesValidation
            End Get
            Set(ByVal value As Boolean)
                TextBox.CausesValidation = value
            End Set
        End Property
        ''' <summary>Gets or sets the <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> associated with this control.</summary>
        ''' <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip"></see> for this control, or null if there is no <see cref="T:System.Windows.Forms.ContextMenuStrip"></see>. The default is null.</returns>
        <SRDescription("ControlContextMenuDescr"), SRCategory("CatBehavior"), DefaultValue(CStr(Nothing))> _
        Public Overridable Property TextBoxContextMenuStrip() As ContextMenuStrip
            Get
                Return TextBox.ContextMenuStrip
            End Get
            Set(ByVal value As ContextMenuStrip)
                TextBox.ContextMenuStrip = value
            End Set
        End Property

        ''' <summary>Gets or sets the culture information associated with the masked text box.</summary>
        ''' <returns>A <see cref="T:System.Globalization.CultureInfo"></see> representing the culture supported by the <see cref="T:System.Windows.Forms.MaskedTextBox"></see>.</returns>
        ''' <exception cref="T:System.ArgumentNullException"><see cref="P:System.Windows.Forms.MaskedTextBox.Culture"></see> was set to null.</exception>
        <RefreshProperties(RefreshProperties.Repaint), SRCategory("CatBehavior"), SRDescription("MaskedTextBoxCultureDescr")> _
        Public Property Culture() As Globalization.CultureInfo
            Get
                Return TextBox.Culture
            End Get
            Set(ByVal value As Globalization.CultureInfo)
                TextBox.Culture = value
            End Set
        End Property

        ''' <summary>Gets or sets a value that determines whether literals and prompt characters are copied to the clipboard.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.MaskFormat"></see> values. The default is <see cref="F:System.Windows.Forms.MaskFormat.IncludeLiterals"></see>.</returns>
        ''' <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Property set with a <see cref="T:System.Windows.Forms.MaskFormats"></see> value that is not valid. </exception>
        <RefreshProperties(RefreshProperties.Repaint), DefaultValue(2), SRCategory("CatBehavior"), SRDescription("MaskedTextBoxCutCopyMaskFormat")> _
        Public Property CutCopyMaskFormat() As MaskFormat
            Get
                Return TextBox.CutCopyMaskFormat
            End Get
            Set(ByVal value As MaskFormat)
                TextBox.CutCopyMaskFormat = value
            End Set
        End Property

        ''' <summary>Gets or sets a value indicating whether the control can respond to user interaction.</summary>
        ''' <returns>true if the control can respond to user interaction; otherwise, false. The default is true.</returns>
        ''' <filterpriority>1</filterpriority>
        ''' <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /></PermissionSet>
        <Localizable(True), SRDescription("ControlEnabledDescr"), SRCategory("CatBehavior")> _
        Public Property TextBoxEnabled() As Boolean
            Get
                Return TextBox.Enabled
            End Get
            Set(ByVal value As Boolean)
                TextBox.Enabled = value
            End Set
        End Property


        ''' <summary>Gets or sets a value indicating whether the selected text in the text box control remains highlighted when the control loses focus.</summary>
        ''' <returns>true if the selected text does not appear highlighted when the text box control loses focus; false, if the selected text remains highlighted when the text box control loses focus. The default is true.</returns>
        <SRDescription("TextBoxHideSelectionDescr"), SRCategory("CatBehavior"), DefaultValue(True)> _
        Public Property HideSelection() As Boolean
            Get
                Return TextBox.HideSelection
            End Get
            Set(ByVal value As Boolean)
                TextBox.HideSelection = value
            End Set
        End Property


        ''' <summary>Gets or sets the text insertion mode of the masked text box control.</summary>
        ''' <returns>An <see cref="T:System.Windows.Forms.InsertKeyMode"></see> value that indicates the current insertion mode. The default is <see cref="F:System.Windows.Forms.InsertKeyMode.Default"></see>.</returns>
        ''' <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">An invalid <see cref="T:System.Windows.Forms.InsertKeyMode"></see> value was supplied when setting this property.</exception>
        <SRDescription("MaskedTextBoxInsertKeyModeDescr"), DefaultValue(0), SRCategory("CatBehavior")> _
        Public Property InsertKeyMode() As InsertKeyMode
            Get
                Return TextBox.InsertKeyMode
            End Get
            Set(ByVal value As InsertKeyMode)
                TextBox.InsertKeyMode = value
            End Set
        End Property

        ''' <summary>Gets or sets the current text in the text box.</summary>
        ''' <returns>The text displayed in the control.</returns>
        <Localizable(True), Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))> _
        Public Overrides Property [Text]() As String
            Get
                Return mtbText.Text
            End Get
            Set(ByVal value As String)
                mtbText.Text = value
            End Set
        End Property
        ''' <summary>Gets or sets the input mask to use at run time. </summary>
        ''' <returns>A <see cref="T:System.String"></see> representing the current mask. The default value is the empty string which allows any input.</returns>
        ''' <exception cref="T:System.ArgumentException">The string supplied to the <see cref="P:System.Windows.Forms.MaskedTextBox.Mask"></see> property is not a valid mask. Invalid masks include masks containing non-printable characters.</exception>
        <Localizable(True), SRCategory("CatBehavior"), Editor("System.Windows.Forms.Design.MaskPropertyEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor)), SRDescription("MaskedTextBoxMaskDescr"), RefreshProperties(RefreshProperties.Repaint), DefaultValue("")> _
        Public Property Mask() As String
            Get
                Return mtbText.Mask
            End Get
            Set(ByVal value As String)
                mtbText.Mask = value
            End Set
        End Property
        ''' <summary>Gets or sets the character to be displayed in substitute for user input.</summary>
        ''' <returns>The <see cref="T:System.Char"></see> value used as the password character.</returns>
        ''' <exception cref="T:System.ArgumentException">The character specified when setting this property is not a valid password character, as determined by the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidPasswordChar(System.Char)"></see> method of the <see cref="T:System.ComponentModel.MaskedTextProvider"></see> class.</exception>
        ''' <exception cref="T:System.InvalidOperationException">The password character specified is the same as the current prompt character, <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar"></see>. The two are required to be different.</exception>
        <SRCategory("CatBehavior"), SRDescription("MaskedTextBoxPasswordCharDescr"), RefreshProperties(RefreshProperties.Repaint), DefaultValue(ChrW(0))> _
        Public Property PasswordChar() As Char
            Get
                Return TextBox.PasswordChar
            End Get
            Set(ByVal value As Char)
                TextBox.PasswordChar = value
            End Set
        End Property

        ''' <summary>Gets or sets a value indicating whether the prompt characters in the input mask are hidden when the masked text box loses focus.</summary>
        ''' <returns>true if <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar"></see> is hidden when <see cref="T:System.Windows.Forms.MaskedTextBox"></see> does not have focus; otherwise, false. The default is false.</returns>
        <SRDescription("MaskedTextBoxHidePromptOnLeaveDescr"), RefreshProperties(RefreshProperties.Repaint), SRCategory("CatBehavior"), DefaultValue(False)> _
        Public Property HidePromptOnLeave() As Boolean
            Get
                Return mtbText.HidePromptOnLeave
            End Get
            Set(ByVal value As Boolean)
                mtbText.HidePromptOnLeave = value
            End Set
        End Property

        ''' <summary>Gets or sets the character used to represent the absence of user input in <see cref="T:System.Windows.Forms.MaskedTextBox"></see>.</summary>
        ''' <returns>The character used to prompt the user for input. The default is an underscore (_). </returns>
        ''' <exception cref="T:System.InvalidOperationException">The prompt character specified is the same as the current password character, <see cref="P:System.Windows.Forms.MaskedTextBox.PasswordChar"></see>. The two are required to be different.</exception>
        ''' <exception cref="T:System.ArgumentException">The character specified when setting this property is not a valid prompt character, as determined by the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidPasswordChar(System.Char)"></see> method of the <see cref="T:System.ComponentModel.MaskedTextProvider"></see> class.</exception>
        <Localizable(True), SRCategory("CatAppearance"), SRDescription("MaskedTextBoxPromptCharDescr"), DefaultValue("_"c), RefreshProperties(RefreshProperties.Repaint)> _
        Public Property PromptChar() As Char
            Get
                Return mtbText.PromptChar
            End Get
            Set(ByVal value As Char)
                mtbText.PromptChar = value
            End Set
        End Property

        ''' <summary>Gets or sets a value indicating whether text in the text box is read-only.</summary>
        ''' <returns>true if the text box is read-only; otherwise, false. The default is false.</returns>
        <DefaultValue(False), RefreshProperties(RefreshProperties.Repaint), SRCategory("CatBehavior"), SRDescription("TextBoxReadOnlyDescr")> _
        Public Property [ReadOnly]() As Boolean
            Get
                Return TextBox.ReadOnly
            End Get
            Set(ByVal value As Boolean)
                TextBox.ReadOnly = value
            End Set
        End Property
        ''' <summary>Gets or sets a value indicating whether the defined shortcuts are enabled.</summary>
        ''' <returns>true to enable the shortcuts; otherwise, false.</returns>
        <DefaultValue(True), SRCategory("CatBehavior"), SRDescription("TextBoxShortcutsEnabledDescr")> _
        Public Overridable Property ShortcutsEnabled() As Boolean
            Get
                Return TextBox.ShortcutsEnabled
            End Get
            Set(ByVal value As Boolean)
                TextBox.ShortcutsEnabled = value
            End Set
        End Property
        ''' <summary>Gets or sets a value indicating whether the user is allowed to reenter literal values.</summary>
        ''' <returns>true to allow literals to be reentered; otherwise, false to prevent the user from overwriting literal characters. The default is true.</returns>
        <SRDescription("MaskedTextBoxSkipLiterals"), DefaultValue(True), SRCategory("CatBehavior")> _
        Public Property SkipLiterals() As Boolean
            Get
                Return TextBox.SkipLiterals
            End Get
            Set(ByVal value As Boolean)
                TextBox.SkipLiterals = value
            End Set
        End Property
        ''' <summary>Gets or sets a value indicating whether the parsing of user input should stop after the first invalid character is reached.</summary>
        ''' <returns>true if processing of the input string should be terminated at the first parsing error; otherwise, false if processing should ignore all errors. The default is false.</returns>
        <SRCategory("CatBehavior"), DefaultValue(False), SRDescription("MaskedTextBoxRejectInputOnFirstFailureDescr")> _
        Public Property RejectInputOnFirstFailure() As Boolean
            Get
                Return mtbText.RejectInputOnFirstFailure
            End Get
            Set(ByVal value As Boolean)
                mtbText.RejectInputOnFirstFailure = value
            End Set
        End Property
        ''' <summary>Gets or sets a value that determines how an input character that matches the prompt character should be handled.</summary>
        ''' <returns>true if the prompt character entered as input causes the current editable position in the mask to be reset; otherwise, false to indicate that the prompt character is to be processed as a normal input character. The default is true.</returns>
        <SRCategory("CatBehavior"), SRDescription("MaskedTextBoxResetOnPrompt"), DefaultValue(True)> _
        Public Property ResetOnPrompt() As Boolean
            Get
                Return mtbText.ResetOnPrompt
            End Get
            Set(ByVal value As Boolean)
                mtbText.ResetOnPrompt = value
            End Set
        End Property
        ''' <summary>Gets or sets a value that determines how a space input character should be handled.</summary>
        ''' <returns>true if the space input character causes the current editable position in the mask to be reset; otherwise, false to indicate that it is to be processed as a normal input character. The default is true.</returns>
        <DefaultValue(True), SRDescription("MaskedTextBoxResetOnSpace"), SRCategory("CatBehavior")> _
        Public Property ResetOnSpace() As Boolean
            Get
                Return mtbText.ResetOnSpace
            End Get
            Set(ByVal value As Boolean)
                mtbText.ResetOnSpace = value
            End Set
        End Property

        ''' <summary>Gets or sets a value that determines whether literals and prompt characters are included in the formatted string.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.MaskFormat"></see> values. The default is <see cref="F:System.Windows.Forms.MaskFormat.IncludeLiterals"></see>.</returns>
        ''' <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">Property set with a <see cref="T:System.Windows.Forms.MaskFormat"></see> value that is not valid. </exception>
        <SRCategory("CatBehavior"), DefaultValue(2), RefreshProperties(RefreshProperties.Repaint), SRDescription("MaskedTextBoxTextMaskFormat")> _
        Public Property TextMaskFormat() As MaskFormat
            Get
                Return mtbText.TextMaskFormat
            End Get
            Set(ByVal value As MaskFormat)
                mtbText.TextMaskFormat = value
            End Set
        End Property
        ''' <summary>Gets or sets a value indicating whether the operating system-supplied password character should be used.</summary>
        ''' <returns>true if the system password should be used as the prompt character; otherwise, false. The default is false.</returns>
        ''' <exception cref="T:System.InvalidOperationException">The password character specified is the same as the current prompt character, <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar"></see>. The two are required to be different.</exception>
        <DefaultValue(False), SRCategory("CatBehavior"), SRDescription("MaskedTextBoxUseSystemPasswordCharDescr"), RefreshProperties(RefreshProperties.Repaint)> _
        Public Property UseSystemPasswordChar() As Boolean
            Get
                Return TextBox.UseSystemPasswordChar
            End Get
            Set(ByVal value As Boolean)
                TextBox.UseSystemPasswordChar = False
            End Set
        End Property
        ''' <summary>Gets or sets the data type used to verify the data input by the user. </summary>
        ''' <returns>A <see cref="T:System.Type"></see> representing the data type used in validation. The default is null.</returns>
        <DefaultValue(CStr(Nothing)), Browsable(False)> _
                Public Property ValidatingType() As Type
            Get
                Return TextBox.ValidatingType
            End Get
            Set(ByVal value As Type)
                TextBox.ValidatingType = value
            End Set
        End Property





#End Region
        Private Sub txtText_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles mtbText.TextChanged
            OnTextChanged(e)
        End Sub
        ''' <summary>Raises the <see cref="TextChanged"/> event</summary>
        Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
            ApplyAutoCahnge()
            MyBase.OnTextChanged(e)
        End Sub
        ''' <summary>Applies <see cref="AutoChanged"/> property after change of <see cref="Text"/></summary>
        ''' <remarks>If <see cref="Status"/> is <see cref="StatusMarker.Statuses.Deleted"/> or <see cref="StatusMarker.Statuses.Error"/> or <see cref="StatusMarker.Statuses.NA"/> or <see cref="StatusMarker.Statuses.Normal"/> than it changes to <see cref="StatusMarker.Statuses.Changed"/>, if it is <see cref="StatusMarker.Statuses.Null"/> then it changes to <see cref="StatusMarker.Statuses.New"/></remarks>
        Private Sub ApplyAutoCahnge()
            If AutoChanged Then
                Select Case stmStatus.Status
                    Case StatusMarker.Statuses.Deleted, StatusMarker.Statuses.Error, StatusMarker.Statuses.NA, StatusMarker.Statuses.Normal
                        stmStatus.Status = StatusMarker.Statuses.Changed
                    Case StatusMarker.Statuses.Null
                        stmStatus.Status = StatusMarker.Statuses.[New]
                End Select
            End If
        End Sub
    End Class
End Namespace

