#If Config <= Nightly Then 'Stage Nightly
Imports System.Windows.Forms

Namespace WindowsT.DialogsT
    'TODO: Fully comment
    'ASAP:Mark
    ''' <summary>Provides technology-independent base class for WinForms and WPF message boxes</summary>
    Public MustInherit Class MessageBox

#Region "MessageBox Definition"
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
        Private _TopControl As Control
        Private _MidControl As Control
        Private _BottomControl As Control
        Private _Timer As TimeSpan
        Private _TimeButton As Integer
#End Region

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
            <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> Public Event TextChanged(ByVal sender As MessageBoxButton, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            ''' <summary>Raised when value of the <see cref="ToolTip"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> Public Event ToolTipChanged(ByVal sender As MessageBoxButton, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            ''' <summary>Raised when value of the <see cref="Enabled"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> Public Event EnabledChanged(ByVal sender As MessageBoxButton, ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
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
        End Class
        ''' <summary>Value of the <see cref="Result"/> property for predefined <see cref="Help">Help</see> button</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Const HelpDialogResult As DialogResult = Integer.MinValue

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

        Public Class MessageBoxCheckBox : Inherits MessageBoxControl
            'TODO:Implement
        End Class
        Public Class MessageBoxComboBox : Inherits MessageBoxControl
            'TODO:Implement
        End Class
        Public Class MessageBoxRadioButton : Inherits MessageBoxControl
            'TODO:Implement
        End Class
    End Class
End Namespace
#End If