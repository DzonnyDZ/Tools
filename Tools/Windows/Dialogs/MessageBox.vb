Imports Tools.CollectionsT.GenericT

#If Config <= Nightly Then 'Stage Nightly
Imports System.Windows.Forms

Namespace WindowsT.DialogsT
    'ASAP:Mark
    ''' <summary>Provides technology-independent base class for WinForms and WPF message boxes</summary>
    Public MustInherit Class MessageBox : Inherits Control
#Region "Shared"
        ''' <summary>Contains value of the <see cref="DefaultImplementation"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private Shared _DefaultImplementation As Type 'TODO: Assign WinForms implementation
        ''' <summary>Gets or sets default implementation used for messageboxes shown by static <see cref="Show"/> methods of this class</summary>
        ''' <returns>Type currently used as default implementation of message box</returns>
        ''' <value>Sets application-wide default implementation of message box</value>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        ''' <see cref="ArgumentException">Value being set represents type that either does not derive from <see cref="MesasageBox"/>, is abstract, is generic non-closed or hasn't parameter-less contructor.</see>
        Public Shared Property DefaultImplementation() As Type
            <DebuggerStepThrough()> Get
                Return _DefaultImplementation
            End Get
            Set(ByVal value As Type)
                If value Is Nothing Then Throw New ArgumentNullException("value")
                If Not value.IsSubclassOf(GetType(MessageBox)) Then Throw New ArgumentException("Type must inherit from MessageBox") 'Localize:Exception
                If value.IsAbstract Then Throw New ArgumentException("Default MessageBox implementation cannot be abstract type.") 'Localize: Exception
                If value.IsGenericTypeDefinition Then Throw New ArgumentException("Deffault MessageBox implementation cannot be generic type definition.") 'Localize:Exception
                If value.GetConstructor(Type.EmptyTypes) Is Nothing Then Throw New ArgumentException("Class that represents default MessageBox implementation must have parameter-less constructor.") 'Localize:Exception
                _DefaultImplementation = value
            End Set
        End Property
        ''' <summary>Returns new instance of default implementation of <see cref="MessageBox"/></summary>
        Public Shared Function GetDefault() As MessageBox
            Return Activator.CreateInstance(DefaultImplementation)
        End Function
#End Region
#Region "MessageBox Definition fields"
        Private _Buttons As New List(Of MessageBoxButton)
        Private _DefaultButton As Integer = 0
        Private _CloseResponce As DialogResult = DialogResult.None 'TODO: In Show/Ctor adjust if Cancel/No/Abort button is defined
        Private _Prompt As String
        Private _Title As String
        Private _Icon As Drawing.Image
        Private _Options As MessageBoxOptions
        Private _CheckBox As MessageBoxCheckBox
        Private _ComboBox As MessageBoxComboBox
        Private _Radios As New List(Of MessageBoxRadioButton)
        Private _TopControl As Object
        Private _MidControl As Object
        Private _BottomControl As Object
        Private _Timer As TimeSpan = TimeSpan.Zero
        Private _TimeButton As Integer = 0
#End Region
#Region "Properties"

#End Region
#Region "CTors"

#End Region
#Region "Shared Show"

#End Region
#Region "Control classes"
        ''' <summary>Common base for predefined message box controls</summary>
        Public MustInherit Class MessageBoxControl : Implements IReportsChange
            ''' <summary>Contains value of the <see cref="Text"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Text As String
            ''' <summary>Contains value of the <see cref="ToolTip"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _ToolTip As String
            ''' <summary>Contains value of the <see cref="Enabled"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Enabled As Boolean = True
            ''' <summary>Raised when value of the <see cref="Text"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            ''' <remarks>If derived control is editable, user can cause this event to be fired.</remarks>
            <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> Public Event TextChanged(ByVal sender As MessageBoxControl, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            ''' <summary>Raised when value of the <see cref="ToolTip"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> Public Event ToolTipChanged(ByVal sender As MessageBoxControl, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            ''' <summary>Raised when value of the <see cref="Enabled"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> Public Event EnabledChanged(ByVal sender As MessageBoxControl, ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
            ''' <summary>Gets or sets text displayed on button</summary>
            Public Property Text() As String
                <DebuggerStepThrough()> Get
                    Return _Text
                End Get
                Set(ByVal value As String)
                    If value <> Text Then
                        Dim old = Text
                        _Text = value
                        Dim e As New IReportsChange.ValueChangedEventArgs(Of String)(old, value, "Text")
                        RaiseEvent TextChanged(Me, e)
                        RaiseEvent Changed(Me, e)
                    End If
                End Set
            End Property
            ''' <summary>Gets or sets tool tip text for the button</summary>
            <DefaultValue(GetType(String), Nothing)> _
            Public Property ToolTip() As String
                <DebuggerStepThrough()> Get
                    Return _ToolTip
                End Get
                Set(ByVal value As String)
                    If value <> ToolTip Then
                        Dim old = ToolTip
                        _ToolTip = value
                        Dim e As New IReportsChange.ValueChangedEventArgs(Of String)(old, value, "ToolTip")
                        RaiseEvent ToolTipChanged(Me, e)
                        RaiseEvent Changed(Me, e)
                    End If
                End Set
            End Property
            ''' <summary>Gets or sets value indicating if button is enabled (accessible) or not</summary>
            <DefaultValue(True)> _
            Public Property Enabled() As Boolean
                <DebuggerStepThrough()> Get
                    Return _Enabled
                End Get
                Set(ByVal value As Boolean)
                    If value <> Enabled Then
                        Dim old = Enabled
                        _Enabled = value
                        Dim e As New IReportsChange.ValueChangedEventArgs(Of Boolean)(old, value, "Enabled")
                        RaiseEvent EnabledChanged(Me, e)
                        RaiseEvent Changed(Me, e)
                    End If
                End Set
            End Property

            Public Event Changed(ByVal sender As IReportsChange, ByVal e As System.EventArgs) Implements IReportsChange.Changed
            Protected Overridable Sub OnChanged(ByVal e As IReportsChange.ValueChangedEventArgsBase)
                RaiseEvent Changed(Me, e)
            End Sub
        End Class
        ''' <summary>Represents button for <see cref="MessageBox"/></summary>
        ''' <completionlist cref="MessageBoxButton"/>
        Public Class MessageBoxButton : Inherits MessageBoxControl
#Region "Backing fields"
            ''' <summary>Contains value of the <see cref="Result"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Result As DialogResult = DialogResult.None
            ''' <summary>Contains value of the <see cref="Button"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Button As Object
            ''' <summary>Contains value of the <see cref="AccessKey"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _AccessKey As Char = vbNullChar
#End Region
            ''' <summary>Raised when button is clicked, before action associated with the button is taken</summary>
            ''' <param name="e">Event arguments. Can be used to cancel the event.</param>
            ''' <param name="sender">Instance of <see cref="MessageBoxButton"/> that have raised the event</param>
            Public Event ClickPreview(ByVal sender As MessageBoxButton, ByVal e As CancelEventArgs)
#Region "Change events"
            ''' <summary>Raised when value of the <see cref="Result"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> Public Event ResultChanged(ByVal sender As MessageBoxButton, ByVal e As IReportsChange.ValueChangedEventArgs(Of DialogResult))
            ''' <summary>Raised when value of the <see cref="AccessKey"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> Public Event AccessKeyChanged(ByVal sender As MessageBoxButton, ByVal e As IReportsChange.ValueChangedEventArgs(Of Char))
#End Region
#Region "Common properties"
            ''' <summary>Gets or sets result produced by this button</summary>
            ''' <remarks>In case you need to define your own buttons you can use this property and set it to value thet is not member of the <see cref="DialogResult"/> enumeration.</remarks>
            <DefaultValue(GetType(DialogResult), "None")> _
            Public Property Result() As DialogResult
                <DebuggerStepThrough()> Get
                    Return _Result
                End Get
                Set(ByVal value As DialogResult)
                    If value <> Result Then
                        Dim old = Result
                        _Result = value
                        Dim e As New IReportsChange.ValueChangedEventArgs(Of DialogResult)(old, value, "Result")
                        RaiseEvent ResultChanged(Me, e)
                        OnChanged(e)
                    End If
                End Set
            End Property
            ''' <summary>Gets or sets access key (access character for the button)</summary>
            ''' <value>Should be one of letters contained in <see cref="Text"/>, otherwise it is not guaranted that accesskey will work.</value>
            ''' <remarks>
            ''' As <see cref="MessageBox"/> is underlaying-technology independent, mnemonics for accesskeys (such as &amp; in WinForms and _ in WPF) should not be used. You should rather use this property.
            ''' When you do not want to use accesskey for your button, set his property to 0 (null char, <see cref="vbNullChar"/>).
            ''' </remarks>
            <DefaultValue(CChar(vbNullChar))> _
            Public Property AccessKey() As Char
                <DebuggerStepThrough()> Get
                    Return _AccessKey
                End Get
                Set(ByVal value As Char)
                    If value <> AccessKey Then
                        Dim old = AccessKey
                        _AccessKey = value
                        Dim e As New IReportsChange.ValueChangedEventArgs(Of Char)(old, value, "AccessKey")
                        RaiseEvent AccessKeyChanged(Me, e)
                        OnChanged(e)
                    End If
                End Set
            End Property
#End Region
            ''' <summary>Class derived from <see cref="MessageBox"/> which owns this button can use this property to store "physical" button object that represents the button on surface of a window.</summary>
            ''' <remarks>
            ''' Use of this property by <see cref="MessageBox">MessageBox</see>-derived class is optional.
            ''' This property should be set only by class derived from <see cref="MessageBox"/> which owns this button.
            ''' </remarks>
            <EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
            Public Property Button() As Object
                <DebuggerStepThrough()> Get
                    Return _Button
                End Get
                <DebuggerStepThrough()> Set(ByVal value As Object)
                    _Button = value
                End Set
            End Property
#Region "CTors"
            'TODO:Null exceptions
            'TODO:Comments
            Public Sub New(ByVal Text$)
                Me.Text = Text
            End Sub
            Public Sub New(ByVal Text$, ByVal AccessKey As Char)
                Me.New(Text)
                Me.AccessKey = AccessKey
            End Sub
            Public Sub New(ByVal Text$, ByVal ToolTip$)
                Me.New(Text)
                Me.ToolTip = ToolTip
            End Sub
            Public Sub New(ByVal Text$, ByVal ToolTip$, ByVal AccessKey As Char)
                Me.New(Text, ToolTip)
                Me.AccessKey = AccessKey
            End Sub
            Public Sub New(ByVal Text$, ByVal Result As DialogResult)
                Me.New(Text)
                Me.Result = Result
            End Sub
            Public Sub New(ByVal Text$, ByVal Result As DialogResult, ByVal AccessKey As Char)
                Me.New(Text, Result)
                Me.AccessKey = AccessKey
            End Sub
            Public Sub New(ByVal Text$, ByVal ToolTip$, ByVal Result As DialogResult)
                Me.New(Text, ToolTip)
                Me.Result = Result
            End Sub
            Public Sub New(ByVal Text$, ByVal ToolTip$, ByVal Result As DialogResult, ByVal AccessKey As Char)
                Me.New(Text, ToolTip, Result)
                Me.AccessKey = AccessKey
            End Sub
            Public Sub New(ByVal Text$, ByVal Enabled As Boolean)
                Me.new(Text)
                Me.Enabled = Enabled
            End Sub
            Public Sub New(ByVal Text$, ByVal Result As DialogResult, ByVal Enabled As Boolean, Optional ByVal ToolTip$ = Nothing)
                Me.New(Text, ToolTip, Result)
                Me.Enabled = Enabled
            End Sub
            Public Sub New(ByVal Text$, ByVal ClickPreview As ClickPreviewEventHandler, Optional ByVal ToolTip$ = Nothing, Optional ByVal Result As DialogResult = DialogResult.None, Optional ByVal Enabled As Boolean = True, Optional ByVal AccessKey As Char = CChar(vbNullChar))
                Me.New(Text, Result, Enabled, ToolTip)
                Me.AccessKey = AccessKey
                AddHandler Me.ClickPreview, ClickPreview
            End Sub
#End Region
            ''' <summary>Called by owner window when appropriate button is clicked. Raises the <see cref="ClickPreview"/> event</summary>
            ''' <returns>True if action associated with button can be performed. False when event was canceled</returns>
            ''' <remarks>This function should be called only by class derived from <see cref="MessageBox"/> which owns the button</remarks>
            <EditorBrowsable(EditorBrowsableState.Advanced)> _
            Public Function OnClick() As Boolean
                Dim e As New CancelEventArgs
                RaiseEvent ClickPreview(Me, e)
                Return Not e.Cancel
            End Function
#Region "Predefined buttons"
            ''' <summary>Default OK button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property OK() As MessageBoxButton
                Get
                    Return New MessageBoxButton("OK", DialogResult.OK) With {.AccessKey = "O"} 'Localize:OK, AccessKey
                End Get
            End Property
            ''' <summary>Default Cancle button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property Cancel() As MessageBoxButton
                Get
                    Return New MessageBoxButton("Cancel", DialogResult.Cancel) With {.AccessKey = "C"} 'Localize:Cancel, AccessKey
                End Get
            End Property
            ''' <summary>Default Yes button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property Yes() As MessageBoxButton
                Get
                    Return New MessageBoxButton("Yes", DialogResult.Yes) With {.AccessKey = "Y"}  'Localize:Yes, AccessKey
                End Get
            End Property
            ''' <summary>Defaut No button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property No() As MessageBoxButton
                Get
                    Return New MessageBoxButton("No", DialogResult.No) With {.AccessKey = "N"}  'Localize:No, AccessKey
                End Get
            End Property
            ''' <summary>Default Abort button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property Abort() As MessageBoxButton
                Get
                    Return New MessageBoxButton("Abort", DialogResult.Abort) With {.AccessKey = "A"}  'Localize:Abort, AccessKey
                End Get
            End Property
            ''' <summary>Default Retry button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property Retry() As MessageBoxButton
                Get
                    Return New MessageBoxButton("Retry", DialogResult.Retry) With {.AccessKey = "R"}  'Localize:Retry, AccessKey
                End Get
            End Property
            ''' <summary>Default Ignore button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property Ignore() As MessageBoxButton
                Get
                    Return New MessageBoxButton("Ignore", DialogResult.Ignore) With {.AccessKey = "I"} 'Localize:Ignore , AccessKey
                End Get
            End Property
            ''' <summary>Default Help button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            ''' <remarks>
            ''' <see cref="MessageBoxButton.Result"/> of this button is <see cref="HelpDialogResult"/>.
            ''' Class that derives from <see cref="MessageBox"/> should implement this special button as message box does not close when the button is clicked and some help is opened.
            ''' </remarks>
            Public Shared ReadOnly Property Help() As MessageBoxButton
                Get
                    Return New MessageBoxButton("Help", HelpDialogResult) With {.AccessKey = "H"}  'Localize:Help, AccessKey
                End Get
            End Property
#End Region
            ''' <summary>Gets buttons specified by WinForms enumeration value</summary>
            ''' <param name="Buttons">Buttons to get</param>
            ''' <returns>Array of buttons as specified in <paramref name="Buttons"/></returns>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="Buttons"/> is not member of <see cref="System.Windows.Forms.MessageBoxButtons"/></exception>
            Public Shared Function GetButtons(ByVal Buttons As System.Windows.Forms.MessageBoxButtons) As MessageBoxButton()
                Select Case Buttons
                    Case MessageBoxButtons.AbortRetryIgnore '2
                        Return New MessageBoxButton() {Abort, Retry, Ignore}
                    Case MessageBoxButtons.OK '0
                        Return New MessageBoxButton() {OK}
                    Case MessageBoxButtons.OKCancel '1
                        Return New MessageBoxButton() {OK, Cancel}
                    Case MessageBoxButtons.RetryCancel '5
                        Return New MessageBoxButton() {Retry, Cancel}
                    Case MessageBoxButtons.YesNo '4
                        Return New MessageBoxButton() {Yes, No}
                    Case MessageBoxButtons.YesNoCancel '3
                        Return New MessageBoxButton() {Yes, No, Cancel}
                    Case Else : Throw New InvalidEnumArgumentException("Buttons", Buttons, GetType(System.Windows.Forms.MessageBoxButtons))
                End Select
            End Function
            ''' <summary>Gets buttons specified by WPF enumeration value</summary>
            ''' <param name="Buttons">Buttons to get</param>
            ''' <returns>Array of buttons as specified in <paramref name="Buttons"/></returns>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="Buttons"/> is not member of <see cref="System.Windows.MessageBoxButton"/></exception>
            Public Shared Function GetButtons(ByVal Buttons As System.Windows.MessageBoxButton) As MessageBoxButton()
                Select Case Buttons
                    Case Windows.MessageBoxButton.OK '0
                        Return New MessageBoxButton() {OK}
                    Case Windows.MessageBoxButton.OKCancel '1
                        Return New MessageBoxButton() {OK, Cancel}
                    Case Windows.MessageBoxButton.YesNo '4
                        Return New MessageBoxButton() {Yes, No}
                    Case Windows.MessageBoxButton.YesNoCancel '3
                        Return New MessageBoxButton() {Yes, No, Cancel}
                    Case Else : Throw New InvalidEnumArgumentException("Buttons", Buttons, GetType(Global.System.Windows.MessageBoxButton))
                End Select
            End Function
            ''' <summary>Gets buttons specified by Visual Basic enumeration value</summary>
            ''' <param name="Buttons">Buttons to get</param>
            ''' <returns>Array of buttons as specified in <paramref name="Buttons"/></returns>
            ''' <exception cref="InvalidEnumArgumentException">Bitwise and of <paramref name="Buttons"/> and 7 is not member of <see cref="System.Windows.Forms.MessageBoxButtons"/> enumeration (values 0÷5)</exception>
            Public Shared Function GetButtons(ByVal Buttons As Microsoft.VisualBasic.MsgBoxStyle) As MessageBoxButton()
                Return GetButtons(CType(Buttons And 7, System.Windows.Forms.MessageBoxButtons))
            End Function
            ''' <summary>Gets buttons by bit aray</summary>
            ''' <param name="Buttons">Bit mask of buttons to get</param>
            ''' <returns>Array of buttons according to bit array <paramref name="Buttons"/></returns>
            Public Shared Function GetButtons(ByVal Buttons As Buttons) As MessageBoxButton()
                Dim ret As New List(Of MessageBoxButton)
                If Buttons And MessageBoxButton.Buttons.OK Then ret.Add(OK)
                If Buttons And MessageBoxButton.Buttons.Cancel Then ret.Add(Cancel)
                If Buttons And MessageBoxButton.Buttons.Yes Then ret.Add(Yes)
                If Buttons And MessageBoxButton.Buttons.No Then ret.Add(No)
                If Buttons And MessageBoxButton.Buttons.Abort Then ret.Add(Abort)
                If Buttons And MessageBoxButton.Buttons.Retry Then ret.Add(Retry)
                If Buttons And MessageBoxButton.Buttons.Ignore Then ret.Add(Ignore)
                If Buttons And MessageBoxButton.Buttons.Help Then ret.Add(Help)
                Return ret.ToArray
            End Function
            ''' <summary>Bit aray for predefined buttons</summary>
            <Flags()> _
            Public Enum Buttons As Byte
                ''' <summary>OK button</summary>
                OK = 1
                ''' <summary>Cancel button</summary>
                Cancel = 2
                ''' <summary>Yes button</summary>
                Yes = 4
                ''' <summary>No button</summary>
                No = 8
                ''' <summary>Abort button</summary>
                Abort = 16
                ''' <summary>Retry button</summary>
                Retry = 32
                ''' <summary>Ignore button</summary>
                Ignore = 64
                ''' <summary>Help button</summary>
                Help = 128
            End Enum
        End Class
        ''' <summary>Value of the <see cref="MessageBoxButton.Result"/> property for predefined <see cref="Help">Help</see> button</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Const HelpDialogResult As DialogResult = Integer.MinValue
        ''' <summary>Represents check box control for <see cref="MessageBox"/></summary>
        Public Class MessageBoxCheckBox : Inherits MessageBoxControl
            ''' <summary>Contains value of the <see cref="ThreeState"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _ThreeState As Boolean
            ''' <summary>Contains value of the <see cref="State"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _State As CheckState
            ''' <summary>Raised when value of the <see cref="ThreeState"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> Public Event ThreeStateChanged(ByVal sender As MessageBoxCheckBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
            ''' <summary>Raised when value of the <see cref="State"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            ''' <remarks><see cref="State"/> can be changed by user or programatically</remarks>
            Public Event StateChanged(ByVal sender As MessageBoxCheckBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of CheckState))
            ''' <summary>Gets or sets value indicating if user can change state of checkbox between 3 or 2 states</summary>
            ''' <remarks>2-state CheckBox allows user to change state only to <see cref="CheckState.Checked"/> or <see cref="CheckState.Unchecked"/></remarks>
            <DefaultValue(False)> _
            Public Property ThreeState() As Boolean
                <DebuggerStepThrough()> Get
                    Return _ThreeState
                End Get
                Set(ByVal value As Boolean)
                    Dim old = ThreeState
                    _ThreeState = value
                    If old <> value Then RaiseEvent ThreeStateChanged(Me, New IReportsChange.ValueChangedEventArgs(Of Boolean)(old, value, "ThreeState"))
                End Set
            End Property
            ''' <summary>Gets or sets current state of check box</summary>
            ''' <remarks>If <see cref="ThreeState"/> is false user cannot set checkbox to <see cref="CheckState.Indeterminate"/>, however you can achieve it programatically</remarks>
            <DefaultValue(GetType(CheckState), "Unchecked")> _
            Public Property State() As CheckState
                <DebuggerStepThrough()> Get
                    Return _State
                End Get
                Set(ByVal value As CheckState)
                    If Not InEnum(value) Then Throw New InvalidEnumArgumentException("value", value, GetType(CheckState))
                    Dim old = value
                    _State = value
                    If old <> value Then RaiseEvent StateChanged(Me, New IReportsChange.ValueChangedEventArgs(Of CheckState)(old, value, "State"))
                End Set
            End Property
            ''' <summary>CTor - initializes new instance of the <see cref="MessageBoxCheckBox"/> class</summary>
            Public Sub New()
            End Sub
            ''' <summary>CTor - initializes new instance of the <see cref="MessageBoxCheckBox"/> class with text</summary>
            ''' <param name="Text">Initial text of the control (<see cref="Text"/> property)</param>
            Public Sub New(ByVal Text$)
                Me.new()
                Me.Text = Text
            End Sub
            ''' <summary>CTor - initializes new instance of the <see cref="MessageBoxCheckBox"/> class with text and check state</summary>
            ''' <param name="Text">Initial text of the control (<see cref="Text"/> property)</param>
            ''' <param name="State">Initial state of the control (<see cref="State"/> property)</param>
            Public Sub New(ByVal Text$, ByVal State As CheckState)
                Me.new(Text)
                Me.State = State
            End Sub
        End Class
        ''' <summary>Represents combo box (drop down list) control for <see cref="MessageBox"/></summary>
        Public Class MessageBoxComboBox : Inherits MessageBoxControl
#Region "Fields"
            ''' <summary>Contains value of the <see cref="Editable"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Editable As Boolean
            ''' <summary>Contains value of the <see cref="Items"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _Items As New ListWithEvents(Of Object)(True, True)
            ''' <summary>Contains value of the <see cref="DisplayMember"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _DisplayMember As String
            ''' <summary>Contains value of the <see cref="SelectedItem"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _SelectedItem As Object
            ''' <summary>Contains value of the <see cref="SelectedIndex"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _SelectedIndex As Integer = -1
#End Region
#Region "Properties"
            ''' <summary>Contains value of the <see cref="Editable"></see> property</summary>
            <DefaultValue(False)> _
            Public Property Editable() As Boolean
                <DebuggerStepThrough()> Get
                    Return _Editable
                End Get
                Set(ByVal value As Boolean)
                    Dim old = Editable
                    _Editable = value
                    If old <> value Then RaiseEvent EditableChanged(Me, New IReportsChange.ValueChangedEventArgs(Of Boolean)(old, value, "Editable"))
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Items"></see> property</summary>
            ''' <remarks>register handlers with events of <see cref="ListWithEvents(Of T)"/> returned by this property in order to track changes of the collection</remarks>
            <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
            Public ReadOnly Property Items() As ListWithEvents(Of Object)
                <DebuggerStepThrough()> Get
                    Return _Items
                End Get
            End Property
            ''' <summary>Contains value of the <see cref="DisplayMember"></see> property</summary>
            <DefaultValue(GetType(String), Nothing)> _
            Public Property DisplayMember() As String
                <DebuggerStepThrough()> Get
                    Return _DisplayMember
                End Get
                Set(ByVal value As String)
                    Dim old = DisplayMember
                    _DisplayMember = value
                    If old <> value Then RaiseEvent DisplayMemberChanged(Me, New IReportsChange.ValueChangedEventArgs(Of String)(old, value, "DisplayMember"))
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="SelectedItem"></see> property</summary>
            <DefaultValue(GetType(Object), Nothing), Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
            Public Property SelectedItem() As Object
                <DebuggerStepThrough()> Get
                    Return _SelectedItem
                End Get
                Set(ByVal value As Object)
                    Dim old = SelectedItem
                    _SelectedItem = value
                    If Not old.Equals(value) Then RaiseEvent SelectedItemChanged(Me, New IReportsChange.ValueChangedEventArgs(Of Object)(old, value, "SelectedItem"))
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="SelectedIndex"></see> property</summary>
            <DefaultValue(-1I)> _
            Public Property SelectedIndex() As Integer
                <DebuggerStepThrough()> Get
                    Return _SelectedIndex
                End Get
                Set(ByVal value As Integer)
                    Dim old = value
                    _SelectedIndex = value
                    If old <> value Then RaiseEvent SelectedIndexChanged(Me, New IReportsChange.ValueChangedEventArgs(Of Integer)(old, value, "SelectedIndex"))
                End Set
            End Property
#End Region
#Region "Events"
            ''' <summary>Raised when value of the <see cref="Editable"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> Public Event EditableChanged(ByVal sender As MessageBoxComboBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
            ''' <summary>Raised when value of the <see cref="DisplayMember"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> Public Event DisplayMemberChanged(ByVal sender As MessageBoxComboBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            ''' <summary>Raised when value of the <see cref="SelectedItem"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            ''' <remarks><see cref="SelectedItem"/> can be changed by user or programatically</remarks>
            Public Event SelectedItemChanged(ByVal sender As MessageBoxComboBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
            ''' <summary>Raised when value of the <see cref="SelectedIndex"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            ''' <remarks><see cref="SelectedIndex"/> can be changed by user or programatically</remarks>
            Public Event SelectedIndexChanged(ByVal sender As MessageBoxComboBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Integer))
#End Region
#Region "CTors"
            ''' <summary>CTor - initializes new instance of the <see cref="MessageBoxComboBox"/> class</summary>
            Public Sub New()
                AddHandler MyBase.TextChanged, AddressOf MyBase_TextChanged
            End Sub
            ''' <summary>CTor - initializes new instance of the <see cref="MessageBoxComboBox"/> class with combo box items and display member</summary>
            ''' <param name="DisplayMember">Initial value of the <see cref="DisplayMember"/> property - name of member used to display items</param>
            ''' <param name="Items">Initial items in combo box</param>
            Public Sub New(ByVal DisplayMember$, ByVal ParamArray Items As Object())
                Me.New(Items)
                Me.DisplayMember = DisplayMember
            End Sub
            ''' <summary>CTor - initializes new instance of the <see cref="MessageBoxComboBox"/> class with items</summary>
            ''' <param name="Items">Initial items in combo box</param>
            Public Sub New(ByVal ParamArray Items As Object())
                Me.new()
                Me.Items.AddRange(Items)
            End Sub
#End Region

            ''' <summary>Raised when value of the <see cref="Text"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            ''' <remarks>
            ''' Value of the <see cref="Text"/> property can be changed programatically or by the user.
            ''' This event shadows base class's event in order to change <see cref="EditorBrowsableAttribute"/> only.
            ''' </remarks>
            Public Shadows Event TextChanged(ByVal sender As MessageBoxComboBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            ''' <summary>Handles <see cref="MessageBoxControl.TextChanged"/> event in order to provide shadowing</summary>
            ''' <param name="sender">Source of event (is always this instance)</param>
            ''' <param name="e">Event arguments informing about old and new value</param>
            Private Sub MyBase_TextChanged(ByVal sender As MessageBoxComboBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
                RaiseEvent TextChanged(Me, e)
            End Sub
        End Class
        ''' <summary>Represents radio button (one from many check box) for <see cref="MessageBox"/></summary>
        Public Class MessageBoxRadioButton : Inherits MessageBoxControl
            ''' <summary>Contains value of the <see cref="Checked"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Checked As Boolean
            ''' <summary>Gets or sets value indicating if control is checked or not</summary>
            <DefaultValue(False)> _
            Public Property Checked() As Boolean
                <DebuggerStepThrough()> Get
                    Return _Checked
                End Get
                Set(ByVal value As Boolean)
                    Dim old = value
                    _Checked = value
                    If old <> value Then
                        RaiseEvent CheckedChanged(Me, New IReportsChange.ValueChangedEventArgs(Of Boolean)(old, value, "Checked"))
                    End If
                End Set
            End Property
            ''' <summary>raised when value of the <see cref="Checked"/> property changes</summary>
            ''' <param name="sender">Source of the event</param>
            ''' <param name="e">Event arguments containing infomation about new and old value</param>
            ''' <remarks>The <see cref="Checked"/> property can be changed programatically or by user</remarks>
            Public Event CheckedChanged(ByVal sender As MessageBoxRadioButton, ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
            ''' <summary>CTor - initializes new instance of the <see cref="MessageBoxRadioButton"/> class</summary>
            Public Sub New()
            End Sub
            ''' <summary>CTor - initializes new instance of the <see cref="MessageBoxRadioButton"/> class with text</summary>
            ''' <param name="Text">Text of control</param>
            Public Sub New(ByVal Text$)
                Me.new()
                Me.Text = Text
            End Sub
            ''' <summary>CTor - initializes new instance of the <see cref="MessageBoxRadioButton"/> class with text and check state</summary>
            ''' <param name="Text">Text of control</param>
            ''' <param name="Checked">Initial check state</param>
            Public Sub New(ByVal Text$, ByVal Checked As Boolean)
                Me.new(Text)
                Me.Checked = Checked
            End Sub
        End Class
#End Region

        ''' <summary>Options for <see cref="MessageBox"/></summary>
        Public Enum MessageBoxOptions As Byte
            ''' <summary>Text is aligned left (default)</summary>
            AlignLeft = 0 '0000
            ''' <summary>Text is aligned right</summary>
            AlignRight = 1 '0001
            ''' <summary>Text is aligned center</summary>
            AlignCenter = 2 '0010                                                                         
            ''' <summary>Text is aligned to block. If target platform does not support <see cref="MessageBoxOptions.AlignJustify"/> treats it as <see cref="MessageBoxOptions.Left"/> (in ltr reading <see cref="MessageBoxOptions.AlignRight"/> in rtl reading)</summary>
            AlignJustify = 3 '0011
            ''' <summary>Bitwise mask for AND-ing text alignment</summary>
            <EditorBrowsable(EditorBrowsableState.Advanced)> AlignMask = 3 '0011
            ''' <summary>Left-to-right reading (default)</summary>
            Ltr = 0 '0000
            ''' <summary>Right-to-left reading</summary>
            Rtl = 4 '0100
            ''' <summary>Force shows message box to the user even if application is not currently active</summary>
            BringToFront = 1 '1000
        End Enum
    End Class
End Namespace
#End If