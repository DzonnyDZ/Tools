Imports Tools.CollectionsT.GenericT, System.Linq

#If Config <= Nightly Then 'Stage Nightly
Imports System.Windows.Forms

Namespace WindowsT.DialogsT
    'ASAP:Mark
    ''' <summary>Provides technology-independent base class for WinForms and WPF message boxes</summary>
    ''' <remarks>
    ''' This class implements <see cref="IReportsChange"/> and has plenty of events fo reporting changes of property values. Also types of some properties reports events when their properties are changed.
    ''' The aim of such behavior is to provide dynamic message box which can be changed as it is displayd.
    ''' However it is up to derived class which changes it will track and interpret as changes of dialog.
    ''' </remarks>
    Public MustInherit Class MessageBox : Inherits Component : Implements IReportsChange
#Region "Shared"
        ''' <summary>Contains value of the <see cref="DefaultImplementation"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private Shared _DefaultImplementation As Type 'TODO: Assign WinForms implementation
        ''' <summary>Gets or sets default implementation used for messageboxes shown by static <see cref="Show"/> methods of this class</summary>
        ''' <returns>Type currently used as default implementation of message box</returns>
        ''' <value>Sets application-wide default implementation of message box</value>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        ''' <see cref="ArgumentException">Value being set represents type that either does not derive from <see cref="MessageBox"/>, is abstract, is generic non-closed or hasn't parameter-less contructor.</see>
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
        ''' <summary>Contaions value of the <see cref="Buttons"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _Buttons As New ListWithEvents(Of MessageBoxButton)(True, True)
        ''' <summary>Contaions value of the <see cref="DefaultButton"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _DefaultButton As Integer = 0
        ''' <summary>Contaions value of the <see cref="CloseResponse"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _CloseResponse As DialogResult = DialogResult.None 'TODO: In Show/Ctor adjust if Cancel/No/Abort button is defined
        ''' <summary>Contaions value of the <see cref="Prompt"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Prompt As String
        ''' <summary>Contaions value of the <see cref="Title"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Title As String
        ''' <summary>Contaions value of the <see cref="Icon"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Icon As Drawing.Image
        ''' <summary>Contaions value of the <see cref="Options"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Options As MessageBoxOptions
        ''' <summary>Contaions value of the <see cref="CheckBox"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _CheckBox As MessageBoxCheckBox
        ''' <summary>Contaions value of the <see cref="ComboBox"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ComboBox As MessageBoxComboBox
        ''' <summary>Contaions value of the <see cref="Radios"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _Radios As New ListWithEvents(Of MessageBoxRadioButton)(True, True)
        ''' <summary>Contaions value of the <see cref="TopControl"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _TopControl As Object
        ''' <summary>Contaions value of the <see cref="MidControl"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _MidControl As Object
        ''' <summary>Contaions value of the <see cref="BottomControl"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _BottomControl As Object
        ''' <summary>Contaions value of the <see cref="Timer"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Timer As TimeSpan = TimeSpan.Zero
        ''' <summary>Contaions value of the <see cref="TimeButton"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _TimeButton As Integer = -1
#End Region
#Region "Properties"
        ''' <summary>Defines buttons displayed on message box</summary>
        ''' <remarks>This collection reports event. You can use them to track changed of the collection either via events of the collection itself or via the <see cref="ButtonsChanged"/> event.</remarks>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        Public ReadOnly Property Buttons() As ListWithEvents(Of MessageBoxButton)
            <DebuggerStepThrough()> Get
                Return _Buttons
            End Get
        End Property
        ''' <summary>gets value indicating if value of the <see cref="Buttons"/> property have been changed and sou it should be serialized</summary>
        ''' <returns>Ture if <see cref="Buttons"/> should be serialized, false if it has its default value</returns>
        Private Function ShouldSerializeButtons() As Boolean
            Return Buttons.Count <> 1 OrElse (Buttons.Count = 1 AndAlso Buttons(0).Result <> DialogResult.OK OrElse Buttons(0).HasChanged)
        End Function
        ''' <summary>Resets the <see cref="Buttons"/> to its initial value</summary>
        Private Sub ResetButtons()
            Buttons.Clear()
            Buttons.Add(MessageBoxButton.OK)
        End Sub
        ''' <summary>Indicates 0-based index of button that has focus when message box is shown and that is default button for message box</summary>
        ''' <remarks>Default button is treated as being clicked when user presses Enter. If value ios set outside of range of <see cref="Buttons"/> (i.e. -1), message box has no default button.</remarks>
        ''' <seealso cref="System.Windows.Forms.Form.AcceptButton"/>
        ''' <seealso cref="System.Windows.Controls.Button.IsDefault"/>
        <DefaultValue(0I)> _
        Public Property DefaultButton() As Integer
            <DebuggerStepThrough()> Get
                Return _DefaultButton
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Integer)
                Dim old = DefaultButton
                _DefaultButton = value
                If old <> value Then OnDefaultButtonChanged(New IReportsChange.ValueChangedEventArgs(Of Integer)(old, value, "DefaultButton"))
            End Set
        End Property
        ''' <summary>Gets or sets value returned by <see cref="Show"/> function when user closes the message box by closing window or by pressin escape</summary>
        ''' <remarks>Values that are not members of the <see cref="DialogResult"/> enumeration can be safely used.</remarks>
        Public Property CloseResponse() As DialogResult
            <DebuggerStepThrough()> Get
                Return _CloseResponse
            End Get
            <DebuggerStepThrough()> Set(ByVal value As DialogResult)
                Dim old = CloseResponse
                _CloseResponse = value
                If old <> value Then OnCloseResponseChanged(New IReportsChange.ValueChangedEventArgs(Of DialogResult)(old, value, "CloseResponse"))
            End Set
        End Property
        ''' <summary>gets value idicating if the <see cref="CloseResponse"/> property should be serialuzed</summary>
        ''' <returns>True when <see cref="CloseResponse"/> differs from <see cref="GetDefaultCloseResponse"/></returns>
        Private Function ShouldSerializeCloseResponse() As Boolean
            Return CloseResponse <> GetDefaultCloseResponse()
        End Function
        ''' <summary>Resets value of the <see cref="CloseResponse"/> property to <see cref="GetDefaultCloseResponse"/></summary>
        Private Sub ResetCloseResponse()
            CloseResponse = GetDefaultCloseResponse()
        End Sub
        ''' <summary>Gets value indicating if the <see cref="CloseResponse"/> property has its default value</summary>
        ''' <returns>True when <see cref="CloseResponse"/> equals to <see cref="GetDefaultCloseResponse"/>; false otherwise</returns>
        ''' <value>Setting value of the property to true causes reseting value of the <see cref="CloseResponse"/> to its default value (<see cref="GetDefaultCloseResponse"/>). Setting the property to false is ignored.</value>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property UseDefaultCloseResponse() As Boolean
            Get
                Return CloseResponse = GetDefaultCloseResponse()
            End Get
            Set(ByVal value As Boolean)
                If value = True Then CloseResponse = GetDefaultCloseResponse()
            End Set
        End Property
        ''' <summary>Gets default value for the <see cref="CloseResponse"/> property. The value depends on current content of <see cref="Buttons"/> collection.</summary>
        ''' <returns>Default value for the <see cref="CloseResponse"/> property</returns>
        ''' <remarks>Following rules apply in given order:
        ''' <list type="list">
        ''' <item>If <see cref="Buttons"/> is empty, returns <see cref="DialogResult.None"/></item>
        ''' <item>If <see cref="Buttons"/> has only one element, returns <see cref="MessageBoxButton.Result">Result</see> of that button</item>
        ''' <item>If any button with <see cref="MessageBoxButton.Result">Result</see> <see cref="DialogResult.Cancel"/> exists returns <see cref="DialogResult.Cancel"/></item>
        ''' <item>If any button with <see cref="MessageBoxButton.Result">Result</see> <see cref="DialogResult.No"/> exists returns <see cref="DialogResult.No"/></item>
        ''' <item>If any button with <see cref="MessageBoxButton.Result">Result</see> <see cref="DialogResult.Abort"/> exists returns <see cref="DialogResult.Abort"/></item>
        ''' <item>If any button with <see cref="MessageBoxButton.Result">Result</see> <see cref="DialogResult.Ignore"/> exists returns <see cref="DialogResult.Ignore"/></item>
        ''' <item>In all other cases returns <see cref="DialogResult.None"/></item>
        ''' </list>
        ''' </remarks>
        Protected Function GetDefaultCloseResponse() As DialogResult
            If Buttons.Count = 0 Then
                Return DialogResult.None
            ElseIf Buttons.Count = 1 Then
                Return Buttons(0).Result
            End If
            Dim Cancel = (From Button In Buttons Where Button.Result = DialogResult.Cancel).FirstOrDefault
            If Cancel IsNot Nothing Then Return DialogResult.Cancel
            Dim No = (From Button In Buttons Where Button.Result = DialogResult.No).FirstOrDefault
            If No IsNot Nothing Then Return DialogResult.No
            Dim Abort = (From Button In Buttons Where Button.Result = DialogResult.Abort).FirstOrDefault
            If Abort IsNot Nothing Then Return DialogResult.Abort
            Dim Ignore = (From Button In Buttons Where Button.Result = DialogResult.Ignore).FirstOrDefault
            If Ignore IsNot Nothing Then Return DialogResult.Ignore
            Return DialogResult.None
        End Function
        ''' <summary>Gets or sets text of prompt of message box.</summary>
        <DefaultValue(GetType(String), Nothing)> _
        Public Property Prompt() As String
            <DebuggerStepThrough()> Get
                Return _Prompt
            End Get
            <DebuggerStepThrough()> Set(ByVal value As String)
                Dim old = Prompt
                _Prompt = value
                If old <> value Then OnPromptChanged(New IReportsChange.ValueChangedEventArgs(Of String)(old, value, "Prompt"))
            End Set
        End Property
        ''' <summary>Gets or sets title text of message box</summary>
        ''' <remarks>If value of thsi property is null or an empty string, application title is used (see <see cref="Microsoft.VisualBasic.ApplicationServices.AssemblyInfo.Title"/>)</remarks>
        <DefaultValue(GetType(String), Nothing)> _
        Public Property Title() As String
            <DebuggerStepThrough()> Get
                Return _Title
            End Get
            <DebuggerStepThrough()> Set(ByVal value As String)
                Dim old = Title
                _Title = value
                If old <> value Then OnTitleChanged(New IReportsChange.ValueChangedEventArgs(Of String)(old, value, "Title"))
            End Set
        End Property
        ''' <summary>Gets or sets icon image to display on the message box</summary>
        ''' <remarks>Expected image size is 32×32px. Image is resized proportionaly to fit this size. This may be changed by derived class.</remarks>
        <DefaultValue(GetType(Drawing.Image), Nothing)> _
        Public Property Icon() As Drawing.Image
            <DebuggerStepThrough()> Get
                Return _Icon
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Drawing.Image)
                Dim old = Icon
                _Icon = value
                If old IsNot value Then OnIconChanged(New IReportsChange.ValueChangedEventArgs(Of Drawing.Image)(old, value, "Icon"))
            End Set
        End Property
        ''' <summary>Gets or sets options of the message box</summary>
        <DefaultValue(GetType(MessageBoxOptions), "0")> _
        Public Property Options() As MessageBoxOptions
            <DebuggerStepThrough()> Get
                Return _Options
            End Get
            <DebuggerStepThrough()> Set(ByVal value As MessageBoxOptions)
                Dim old = Options
                _Options = value
                If old <> value Then OnOptionsChanged(New IReportsChange.ValueChangedEventArgs(Of MessageBoxOptions)(old, value, "Options"))
            End Set
        End Property
        ''' <summary>Gets or sets check box displayed in messaqge box</summary>
        <DefaultValue(GetType(MessageBoxCheckBox), Nothing)> _
        Public Property CheckBox() As MessageBoxCheckBox
            <DebuggerStepThrough()> Get
                Return _CheckBox
            End Get
            <DebuggerStepThrough()> Set(ByVal value As MessageBoxCheckBox)
                Dim old = CheckBox
                _CheckBox = value
                If old IsNot value Then OnCheckBoxChanged(New IReportsChange.ValueChangedEventArgs(Of MessageBoxCheckBox)(old, value, "CheckBox"))
            End Set
        End Property
        ''' <summary>Gets or sets combo box (drop down list) displayed in message box</summary>
        <DefaultValue(GetType(MessageBoxComboBox), Nothing)> _
        Public Property ComboBox() As MessageBoxComboBox
            <DebuggerStepThrough()> Get
                Return _ComboBox
            End Get
            <DebuggerStepThrough()> Set(ByVal value As MessageBoxComboBox)
                Dim old = ComboBox
                _ComboBox = value
                If old IsNot value Then OnComboBoxChanged(New IReportsChange.ValueChangedEventArgs(Of MessageBoxComboBox)(old, value, "comboBox"))
            End Set
        End Property
        ''' <summary>Radio buttons displayed on message box</summary>
        ''' <remarks>This collection reports event. You can use them to track changes of the collection either via handling events of the collection or via the <see cref="RadiosChanged"/> event.</remarks>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        Public ReadOnly Property Radios() As ListWithEvents(Of MessageBoxRadioButton)
            <DebuggerStepThrough()> Get
                Return _Radios
            End Get
        End Property
        ''' <summary>Gets value idiciating if the <see cref="Radios"/> property should be serialized</summary>
        ''' <returns>True if count of items of <see cref="Radios"/> is greater than zero</returns>
        Private Function ShouldSerializeRadios() As Boolean
            Return Radios.Count <> 0
        End Function
        ''' <summary>Resets value of the <see cref="Radios"/> property to its default value (an empty list)</summary>
        Private Sub ResetRadios()
            Radios.Clear()
        End Sub
        ''' <summary>Gets or sets additional control displayed at top of the message box (above message)</summary>
        ''' <remarks>Implementation of message box (derived class) may accept only controls of specified type(s) like <see cref="Windows.Forms.Control"/> or <see cref="Windows.FrameworkElement"/> and ignore any other types.</remarks>
        <DefaultValue(GetType(Object), Nothing)> _
        Public Property TopControl() As Object
            <DebuggerStepThrough()> Get
                Return _TopControl
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Object)
                Dim old = TopControl
                _TopControl = value
                If old IsNot value Then OnTopControlChanged(New IReportsChange.ValueChangedEventArgs(Of Object)(old, value, "TopControl"))
            End Set
        End Property
        ''' <summary>Gets or sets additional control displayed in the middle of the message box (above buttons)</summary>
        ''' <remarks>Implementation of message box (derived class) may accept only controls of specified type(s) like <see cref="Windows.Forms.Control"/> or <see cref="Windows.FrameworkElement"/> and ignore any other types.</remarks>
        Public Property MidControl() As Object
            <DebuggerStepThrough()> Get
                Return _MidControl
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Object)
                Dim old = MidControl
                _MidControl = value
                If old IsNot value Then OnMidControlChanged(New IReportsChange.ValueChangedEventArgs(Of Object)(old, value, "MidControl"))
            End Set
        End Property
        ''' <summary>Gets or sets additional control displayed at bottom of the message box (below buttons)</summary>
        ''' <remarks>Implementation of message box (derived class) may accept only controls of specified type(s) like <see cref="Windows.Forms.Control"/> or <see cref="Windows.FrameworkElement"/> and ignore any other types.</remarks>
        Public Property BottomControl() As Object
            <DebuggerStepThrough()> Get
                Return _BottomControl
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Object)
                Dim old = BottomControl
                _BottomControl = value
                If old IsNot value Then OnBottomControlChanged(New IReportsChange.ValueChangedEventArgs(Of Object)(old, value, "BottomControl"))
            End Set
        End Property
        ''' <summary>Gets or sets value indicating for how long the message box will be displayed before it closes with <see cref="CloseResponse"/> as result.</summary>
        ''' <remarks><see cref="TimeSpan.Zero"/> or less vaklue meand then no count-down takes effect</remarks>
        <DefaultValue(GetType(TimeSpan), "0:00:00")> _
        Public Property Timer() As TimeSpan
            <DebuggerStepThrough()> Get
                Return _Timer
            End Get
            <DebuggerStepThrough()> Set(ByVal value As TimeSpan)
                Dim old = Timer
                _Timer = value
                If old <> value Then OnTimerChanged(New IReportsChange.ValueChangedEventArgs(Of TimeSpan)(old, value, "Timer"))
            End Set
        End Property
        ''' <summary>Gets or sets value indicating 0-based index of button when count-down time is displayed</summary>
        ''' <remarks>Following values has special meaning:
        ''' <list type="table"><listheader><term>value</term><description>efect</description></listheader>
        ''' <item><term>-1</term><description>Button is chosed automatically depending on <see cref="CloseResponse"/> property (if there are more buttons with same <see cref="MessageBoxButton.Result"/> first is used)</description></item>
        ''' <item><term>&lt; -1</term><description>Count down time is displayed in message box title</description></item>
        ''' <item><term>>= <see cref="Buttons">Buttons</see>.<see cref="List(Of MessageBoxButton).Count">Count</see></term><description>Count down is not displayed</description></item>
        ''' </list>
        ''' Count down is displayed as time in format h:mm:ss, m:ss or s depending on current value of time remaining (always the shortest possible format is used).</remarks>
        <DefaultValue(-1I)> _
        Public Property TimeButton() As Integer
            <DebuggerStepThrough()> Get
                Return _TimeButton
            End Get
            <DebuggerStepThrough()> Set(ByVal value As Integer)
                Dim old = TimeButton
                _TimeButton = value
                If old <> value Then OnTimeButtonChanged(New IReportsChange.ValueChangedEventArgs(Of Integer)(old, value, "TimeButton"))
            End Set
        End Property
#End Region
#Region "CTors"
        ''' <summary>Adds event handlers to collections</summary>
        Private Sub AddHandlers()
            AddHandler Buttons.CollectionChanged, AddressOf OnButtonsChanged
            AddHandler Radios.CollectionChanged, AddressOf OnRadiosChanged
        End Sub
        ''' <summary>Default CTor - creates messagebox with just one button <see cref="MessageBoxButton.OK"/></summary>
        Public Sub New()
            AddHandlers()
            Me.Buttons.Add(MessageBoxButton.OK)
        End Sub
#End Region
#Region "Shared Show"

#End Region
#Region "Property events"
        ''' <summary>Raised when value of member changes</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event information. For changes of <see cref="Buttons"/> and <see cref="Radios"/> collections event argument of <see cref="ListWithEvents.CollectionChanged"/> is passed instead of argument of <see cref="ListWithEvents.Changed"/>.</param>
        ''' <remarks><paramref name="e"/>Should contain additional information that can be used in event-handling code (e.g. use <see cref="IReportsChange.ValueChangedEventArgs"/> class)</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event Changed(ByVal sender As IReportsChange, ByVal e As System.EventArgs) Implements IReportsChange.Changed
        ''' <summary>Raises the <see cref="Changed"/> event</summary>
        ''' <param name="e">Event parameters</param>
        Protected Overridable Sub OnChanged(ByVal e As EventArgs)
            RaiseEvent Changed(Me, e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="DefaultButton"/> property changes</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event DefaultButtonChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Integer))
        ''' <summary>Raises the <see cref="DefaultButtonChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnDefaultButtonChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Integer))
            RaiseEvent DefaultButtonChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="CloseResponse"/> property changes</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event CloseResponseChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of DialogResult))
        ''' <summary>Raises the <see cref="DeafaultButtonChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnCloseResponseChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of DialogResult))
            RaiseEvent CloseResponseChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="Prompt"/> property changes</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event PromptChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
        ''' <summary>Raises the <see cref="PromptChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnPromptChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            RaiseEvent PromptChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="Title"/> property changes</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event TitleChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
        ''' <summary>Raises the <see cref="TitleChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnTitleChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            RaiseEvent TitleChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="Icon"/> property changes</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event IconChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Drawing.Image))
        ''' <summary>Raises the <see cref="IconChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnIconChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Drawing.Image))
            RaiseEvent IconChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="Options"/> property changes</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event OptionsChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of MessageBoxOptions))
        ''' <summary>Raises the <see cref="OptionsChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnOptionsChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of MessageBoxOptions))
            RaiseEvent OptionsChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="CheckBox"/> property changes</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>This event tracks only changes of value of the <see cref="CheckBox"/> property itsels. It does not track changes of values of its inner properties.</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event CheckBoxChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of MessageBoxCheckBox))
        ''' <summary>Raises the <see cref="CheckBoxChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnCheckBoxChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of MessageBoxCheckBox))
            RaiseEvent CheckBoxChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="ComboBox"/> property changes</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>This event tracks only changes of value of the <see cref="ComboBox"/> property itsels. It does not track changes of values of its inner properties.</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event ComboBoxChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of MessageBoxComboBox))
        ''' <summary>Raises the <see cref="ComboBoxChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnComboBoxChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of MessageBoxComboBox))
            RaiseEvent ComboBoxChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="TopControl"/> property changes</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event TopControlChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
        ''' <summary>Raises the <see cref="TopControlChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnTopControlChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
            RaiseEvent TopControlChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="MidControl"/> property changes</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event MidControlChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
        ''' <summary>Raises the <see cref="MidControlChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnMidControlChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
            RaiseEvent TopControlChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="BottomControl"/> property changes</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event BottomControlChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
        ''' <summary>Raises the <see cref="BottomControlChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnBottomControlChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
            RaiseEvent BottomControlChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="Timer"/> property changes</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event TimerChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of TimeSpan))
        ''' <summary>Raises the <see cref="TimerChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnTimerChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of TimeSpan))
            RaiseEvent TimerChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="TimeButton"/> property changes</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event TimeButtonChanged(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Integer))
        ''' <summary>Raises the <see cref="TimeButtonChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnTimeButtonChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Integer))
            RaiseEvent TimeButtonChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised when content of the <see cref="Radios"/> collection changes</summary>
        ''' <param name="sender">Source of the event - always this instance of <see  cref="MessageBox"/></param>
        ''' <param name="e">Event arguments. Argument e of <see cref="Radios">Radios</see>.<see cref="ListWithEvents(Of MessageBoxRadioButton).CollectionChanged"/> is passed directly here.</param>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event RadiosChanged(ByVal sender As MessageBox, ByVal e As ListWithEvents(Of MessageBoxRadioButton).ListChangedEventArgs)
        ''' <summary>Raises the <see cref="RadiosChanged"/> event. Handles <see cref="Radios">Radios</see>.<see cref="ListWithEvents(Of MessageBoxRadioButton).CollectionChanged">CollectionChanged</see> event. Calls <see cref="OnChanged"/>.</summary>
        ''' <param name="sender">Source ot the event - <see cref="Radios"/></param>
        ''' <param name="e">Event arguments. Those arguemnts are directly passed to <see cref="RadiosChanged"/> and <see cref="OnChanged"/></param>
        Protected Overridable Sub OnRadiosChanged(ByVal sender As ListWithEvents(Of MessageBoxRadioButton), ByVal e As ListWithEvents(Of MessageBoxRadioButton).ListChangedEventArgs)
            RaiseEvent RadiosChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised when content of the <see cref="Buttons"/> collection changes</summary>
        ''' <param name="sender">Source of the event - always this instance of <see  cref="MessageBox"/></param>
        ''' <param name="e">Event arguments. Argument e of <see cref="Buttons">Radios</see>.<see cref="ListWithEvents(Of MessageBoxButton).CollectionChanged"/> is passed directly here.</param>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event ButtonsChanged(ByVal sender As MessageBox, ByVal e As ListWithEvents(Of MessageBoxButton).ListChangedEventArgs)
        ''' <summary>Raises the <see cref="ButtonsChanged"/> event. Handles <see cref="Buttons">Radios</see>.<see cref="ListWithEvents(Of MessageBoxButton).CollectionChanged">CollectionChanged</see> event. Calls <see cref="OnChanged"/>.</summary>
        ''' <param name="sender">Source ot the event - <see cref="Buttons"/></param>
        ''' <param name="e">Event arguments. Those arguemnts are directly passed to <see cref="ButtonsChanged"/> and <see cref="OnChanged"/></param>
        Protected Overridable Sub OnButtonsChanged(ByVal sender As ListWithEvents(Of MessageBoxButton), ByVal e As ListWithEvents(Of MessageBoxButton).ListChangedEventArgs)
            RaiseEvent ButtonsChanged(Me, e)
            OnChanged(e)
        End Sub
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
                        OnTextChanged(e)
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
                        OnToolTipChanged(e)
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
                        OnEnabledChanged(e)
                    End If
                End Set
            End Property
            ''' <summary>Raises the <see cref="TextChanged"/> event, calls <see cref="OnChanged"/></summary>
            ''' <param name="e">Event parameters</param>
            Protected Overridable Sub OnTextChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
                RaiseEvent TextChanged(Me, e)
                OnChanged(e)
            End Sub
            ''' <summary>Raises the <see cref="EnabledChanged"/> event, calls <see cref="OnChanged"/></summary>
            ''' <param name="e">Event parameters</param>
            Protected Overridable Sub OnEnabledChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
                RaiseEvent EnabledChanged(Me, e)
                OnChanged(e)
            End Sub
            ''' <summary>Raises the <see cref="ToolTipChanged"/> event, calls <see cref="OnChanged"/></summary>
            ''' <param name="e">Event parameters</param>
            Protected Overridable Sub OnToolTipChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
                RaiseEvent ToolTipChanged(Me, e)
                OnChanged(e)
            End Sub


            ''' <summary>Raised when value of member changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Event information</param>
            ''' <remarks><paramref name="e"/>Additionla information - is <see cref="IReportsChange.ValueChangedEventArgs(Of T)"/> or <see cref="CollectionChangeEventArgs(Of T)"/></remarks>
            Public Event Changed(ByVal sender As IReportsChange, ByVal e As System.EventArgs) Implements IReportsChange.Changed
            ''' <summary>Raises the <see cref="Changed"/> event</summary>
            ''' <param name="e">Event parameters - should be <see cref="IReportsChange.ValueChangedEventArgs(Of T)"/> or <see cref="CollectionChangeEventArgs(Of T)"/></param>
            Protected Overridable Sub OnChanged(ByVal e As EventArgs)
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
            ''' <summary>Contains value of the <see cref="Changed"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _HasChanged As Boolean
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
#Region "Properties"
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
            ''' <summary>Gets value indicating if any property of this instance have been changed since its construction</summary>
            ''' <remarks>Changing this property does not cause the <see cref="Changed"/> event to be raised</remarks>
            Friend Property HasChanged() As Boolean
                <DebuggerStepThrough()> Get
                    Return _HasChanged
                End Get
                <DebuggerStepThrough()> Private Set(ByVal value As Boolean)
                    _HasChanged = value
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
            ''' <summary>Ture indicates that this instance is currently being constructed</summary>
            Private ReadOnly IsConstructing As Boolean = True
            ''' <summary>CTor - creates new instance of the <see cref="MessageBoxButton"/> class</summary>
            Public Sub New()
                IsConstructing = False
            End Sub
            ''' <summary>CTor from button text</summary>
            ''' <param name="Text">Button's text (see <see cref="Text"/>)</param>
            Public Sub New(ByVal Text$)
                Me.New()
                IsConstructing = True
                Me.Text = Text
                IsConstructing = False
            End Sub
            ''' <summary>CTor from button text and access key</summary>
            ''' <param name="Text">Button's text (see <see cref="Text"/>)</param>
            ''' <param name="AccessKey">Character used as button shortcut (see <see cref="AccessKey"/>)</param>
            Public Sub New(ByVal Text$, ByVal AccessKey As Char)
                Me.New(Text)
                IsConstructing = True
                Me.AccessKey = AccessKey
                IsConstructing = False
            End Sub
            ''' <summary>CTor from text and tool tip text</summary>
            ''' <param name="Text">Button's text (see <see cref="Text"/>)</param>
            ''' <param name="ToolTip">Tool tip (help) text for button (see <see cref="ToolTip"/>)</param>
            Public Sub New(ByVal Text$, ByVal ToolTip$)
                Me.New(Text)
                IsConstructing = True
                Me.ToolTip = ToolTip
                IsConstructing = False
            End Sub
            ''' <summary>CTor from text, tool tip text and access key</summary>
            ''' <param name="Text">Button's text (see <see cref="Text"/>)</param>
            ''' <param name="ToolTip">Tool tip (help) text for button (see <see cref="ToolTip"/>)</param>
            ''' <param name="AccessKey">Character used as button shortcut (see <see cref="AccessKey"/>)</param>
            Public Sub New(ByVal Text$, ByVal ToolTip$, ByVal AccessKey As Char)
                Me.New(Text, ToolTip)
                IsConstructing = True
                Me.AccessKey = AccessKey
                IsConstructing = False
            End Sub
            ''' <summary>CTor from text and dialog result</summary>
            ''' <param name="Text">Button's text (see <see cref="Text"/>)</param>
            ''' <param name="Result">Result returned by <see cref="Show"/> function when the button is clicked (see <see cref="Result"/>)</param>
            Public Sub New(ByVal Text$, ByVal Result As DialogResult)
                Me.New(Text)
                IsConstructing = True
                Me.Result = Result
                IsConstructing = False
            End Sub
            ''' <summary>CTor from text, dialog result and access key</summary>
            ''' <param name="Text">Button's text (see <see cref="Text"/>)</param>
            ''' <param name="Result">Result returned by <see cref="Show"/> function when the button is clicked (see <see cref="Result"/>)</param>
            ''' <param name="AccessKey">Character used as button shortcut (see <see cref="AccessKey"/>)</param>
            Public Sub New(ByVal Text$, ByVal Result As DialogResult, ByVal AccessKey As Char)
                Me.New(Text, Result)
                IsConstructing = True
                Me.AccessKey = AccessKey
                IsConstructing = False
            End Sub
            ''' <summary>CTor from text, tool tip text and dialog result</summary>
            ''' <param name="Text">Button's text (see <see cref="Text"/>)</param>
            ''' <param name="Result">Result returned by <see cref="Show"/> function when the button is clicked (see <see cref="Result"/>)</param>
            ''' <param name="ToolTip">Tool tip (help) text for button (see <see cref="ToolTip"/>)</param>
            Public Sub New(ByVal Text$, ByVal ToolTip$, ByVal Result As DialogResult)
                Me.New(Text, ToolTip)
                IsConstructing = True
                Me.Result = Result
                IsConstructing = False
            End Sub
            ''' <summary>CTor from text, tool tip text, dialog result and access key</summary>
            ''' <param name="Text">Button's text (see <see cref="Text"/>)</param>
            ''' <param name="Result">Result returned by <see cref="Show"/> function when the button is clicked (see <see cref="Result"/>)</param>
            ''' <param name="ToolTip">Tool tip (help) text for button (see <see cref="ToolTip"/>)</param>
            ''' <param name="AccessKey">Character used as button shortcut (see <see cref="AccessKey"/>)</param>
            Public Sub New(ByVal Text$, ByVal ToolTip$, ByVal Result As DialogResult, ByVal AccessKey As Char)
                Me.New(Text, ToolTip, Result)
                IsConstructing = True
                Me.AccessKey = AccessKey
                IsConstructing = False
            End Sub
            ''' <summary>CTor from text and enabled value</summary>
            ''' <param name="Text">Button's text (see <see cref="Text"/>)</param>
            ''' <param name="Enabled">Initial value of the <see cref="Enabled"/> property</param>
            Public Sub New(ByVal Text$, ByVal Enabled As Boolean)
                Me.new(Text)
                IsConstructing = True
                Me.Enabled = Enabled
                IsConstructing = False
            End Sub
            ''' <summary>CTor from text, dialog result, tool tip text and enabled value</summary>
            ''' <param name="Text">Button's text (see <see cref="Text"/>)</param>
            ''' <param name="Enabled">Initial value of the <see cref="Enabled"/> property</param>
            ''' <param name="Result">Result returned by <see cref="Show"/> function when the button is clicked (see <see cref="Result"/>)</param>
            ''' <param name="ToolTip">Tool tip (help) text for button (see <see cref="ToolTip"/>)</param>
            Public Sub New(ByVal Text$, ByVal Result As DialogResult, ByVal Enabled As Boolean, Optional ByVal ToolTip$ = Nothing)
                Me.New(Text, ToolTip, Result)
                Me.Enabled = Enabled
            End Sub
            ''' <summary>CTor from text, click event handler and optionally tool tip text, dialog result, access key and enabled value</summary>
            ''' <param name="Text">Button's text (see <see cref="Text"/>)</param>
            ''' <param name="ClickPreview">Delegate handler for <see cref="ClickPreview"/> event</param>
            ''' <param name="ToolTip">Tool tip (help) text for button (see <see cref="ToolTip"/>)</param>
            ''' <param name="Result">Result returned by <see cref="Show"/> function when the button is clicked (see <see cref="Result"/>)</param>
            ''' <param name="Enabled">Initial value of the <see cref="Enabled"/> property</param>
            ''' <param name="AccessKey">Character used as button shortcut (see <see cref="AccessKey"/>)</param>
            Public Sub New(ByVal Text$, ByVal ClickPreview As ClickPreviewEventHandler, Optional ByVal ToolTip$ = Nothing, Optional ByVal Result As DialogResult = DialogResult.None, Optional ByVal Enabled As Boolean = True, Optional ByVal AccessKey As Char = CChar(vbNullChar))
                Me.New(Text, Result, Enabled, ToolTip)
                IsConstructing = True
                Me.AccessKey = AccessKey
                AddHandler Me.ClickPreview, ClickPreview
                IsConstructing = False
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
                    Return New MessageBoxButton("OK", DialogResult.OK, "O"c) 'Localize:OK, AccessKey
                End Get
            End Property
            ''' <summary>Default Cancle button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property Cancel() As MessageBoxButton
                Get
                    Return New MessageBoxButton("Cancel", DialogResult.Cancel, "C"c) 'Localize:Cancel, AccessKey
                End Get
            End Property
            ''' <summary>Default Yes button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property Yes() As MessageBoxButton
                Get
                    Return New MessageBoxButton("Yes", DialogResult.Yes, "Y"c) 'Localize:Yes, AccessKey
                End Get
            End Property
            ''' <summary>Defaut No button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property No() As MessageBoxButton
                Get
                    Return New MessageBoxButton("No", DialogResult.No, "N"c) 'Localize:No, AccessKey
                End Get
            End Property
            ''' <summary>Default Abort button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property Abort() As MessageBoxButton
                Get
                    Return New MessageBoxButton("Abort", DialogResult.Abort, "A"c) 'Localize:Abort, AccessKey
                End Get
            End Property
            ''' <summary>Default Retry button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property Retry() As MessageBoxButton
                Get
                    Return New MessageBoxButton("Retry", DialogResult.Retry, "R"c) 'Localize:Retry, AccessKey
                End Get
            End Property
            ''' <summary>Default Ignore button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property Ignore() As MessageBoxButton
                Get
                    Return New MessageBoxButton("Ignore", DialogResult.Ignore, "I"c) 'Localize:Ignore , AccessKey
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
                    Return New MessageBoxButton("Help", HelpDialogResult, "H"c) 'Localize:Help, AccessKey
                End Get
            End Property
#End Region
#Region "GetButtons"
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
            ''' <remarks>Order of buttons is Yes - No - OK - Abort - Retry - Ignore - Cancel - Help</remarks>
            Public Shared Function GetButtons(ByVal Buttons As Buttons) As MessageBoxButton()
                Dim ret As New List(Of MessageBoxButton)
                If Buttons And MessageBoxButton.Buttons.Yes Then ret.Add(Yes)
                If Buttons And MessageBoxButton.Buttons.No Then ret.Add(No)
                If Buttons And MessageBoxButton.Buttons.OK Then ret.Add(OK)
                If Buttons And MessageBoxButton.Buttons.Abort Then ret.Add(Abort)
                If Buttons And MessageBoxButton.Buttons.Retry Then ret.Add(Retry)
                If Buttons And MessageBoxButton.Buttons.Ignore Then ret.Add(Ignore)
                If Buttons And MessageBoxButton.Buttons.Cancel Then ret.Add(Cancel)
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
#End Region
            Private Sub MessageBoxButton_Changed(ByVal sender As IReportsChange, ByVal e As System.EventArgs) Handles Me.Changed
                If Not IsConstructing Then HasChanged = True
            End Sub
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
                    If old <> value Then
                        Dim e As New IReportsChange.ValueChangedEventArgs(Of Boolean)(old, value, "ThreeState")
                        OnThreeStateChanged(e)
                    End If
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
                    If old <> value Then
                        Dim e As New IReportsChange.ValueChangedEventArgs(Of CheckState)(old, value, "State")
                        OnStateChanged(e)
                    End If
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
            ''' <summary>Raises the <see cref="StateChanged"/> event, calls <see cref="OnChanged"/></summary>
            ''' <param name="e">Event parameters</param>
            Protected Overridable Sub OnStateChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of CheckState))
                RaiseEvent StateChanged(Me, e)
                OnChanged(e)
            End Sub
            ''' <summary>Raises the <see cref="ThreeStateChanged"/> event, calls <see cref="OnChanged"/></summary>
            ''' <param name="e">Event parameters</param>
            Protected Overridable Sub OnThreeStateChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
                RaiseEvent ThreeStateChanged(Me, e)
                OnChanged(e)
            End Sub
        End Class
        ''' <summary>Represents combo box (drop down list) control for <see cref="MessageBox"/></summary>
        ''' <remarks>This class inherits implementation of <see cref="IReportsChange"/> from <see cref="MessageBoxControl"/>. The <see cref="MessageBoxComboBox.Changed"/> event when raised for change of <see cref="MessageBoxComboBox.Items"/> collection, reports event arguments of <see cref="MessageBoxComboBox.Items">Items</see>.<see cref="ListWithEvents(Of Object).CollectionChanged">CollectionChanged</see> event (instead of the <see cref="ListWithEvents(Of Object).Changed">Changed</see>).</remarks>
        ''' <seealco cref="MessageBoxComboBox.OnItemsChanged"/>
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
                    If old <> value Then
                        Dim e As New IReportsChange.ValueChangedEventArgs(Of Boolean)(old, value, "Editable")
                        OnEditableChanged(e)
                    End If
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Items"></see> property</summary>
            ''' <remarks>Register handlers with events of <see cref="ListWithEvents(Of T)"/> returned by this property or use <see cref="ItemsChanged"/> event in order to track changes of the collection.</remarks>
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
                    If old <> value Then
                        Dim e As New IReportsChange.ValueChangedEventArgs(Of String)(old, value, "DisplayMember")
                        OnDisplayMemberChanged(e)
                    End If
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
                    If Not old.Equals(value) Then
                        Dim e As New IReportsChange.ValueChangedEventArgs(Of Object)(old, value, "SelectedItem")
                        OnSelectedItemChanged(e)
                    End If
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
                    If old <> value Then
                        Dim e As New IReportsChange.ValueChangedEventArgs(Of Integer)(old, value, "SelectedIndex")
                        OnSelectedIndexChanged(e)
                    End If
                End Set
            End Property
#End Region
#Region "Events"
            ''' <summary>Raised when value of the <see cref="Editable"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> Public Event EditableChanged(ByVal sender As MessageBoxComboBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
            ''' <summary>Raises the <see cref="EditableChanged"/> event, calls <see cref="OnChanged"/></summary>
            ''' <param name="e">Event parameters</param>
            Protected Overridable Sub OnEditableChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
                RaiseEvent EditableChanged(Me, e)
                OnChanged(e)
            End Sub
            ''' <summary>Raised when value of the <see cref="DisplayMember"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> Public Event DisplayMemberChanged(ByVal sender As MessageBoxComboBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            ''' <summary>Raises the <see cref="DisplayMemberChanged"/> event, calls <see cref="OnChanged"/></summary>
            ''' <param name="e">Event parameters</param>
            Protected Overridable Sub OnDisplayMemberChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
                RaiseEvent DisplayMemberChanged(Me, e)
                OnChanged(e)
            End Sub
            ''' <summary>Raised when value of the <see cref="SelectedItem"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            ''' <remarks><see cref="SelectedItem"/> can be changed by user or programatically</remarks>
            Public Event SelectedItemChanged(ByVal sender As MessageBoxComboBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
            ''' <summary>Raises the <see cref="SelectedItemChanged"/> event, calls <see cref="OnChanged"/></summary>
            ''' <param name="e">Event parameters</param>
            Protected Overridable Sub OnSelectedItemChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
                RaiseEvent SelectedItemChanged(Me, e)
                OnChanged(e)
            End Sub
            ''' <summary>Raised when value of the <see cref="SelectedIndex"/> property changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Information about old and new value</param>
            ''' <remarks><see cref="SelectedIndex"/> can be changed by user or programatically</remarks>
            Public Event SelectedIndexChanged(ByVal sender As MessageBoxComboBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Integer))
            ''' <summary>Raises the <see cref="SelectedIndexChanged"/> event, calls <see cref="OnChanged"/></summary>
            ''' <param name="e">Event parameters</param>
            Protected Overridable Sub OnSelectedIndexChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Integer))
                RaiseEvent SelectedIndexChanged(Me, e)
                OnChanged(e)
            End Sub
            ''' <summary>Raised when something happens to the <see cref="Items"/> collection (when it raises the <see cref="ListWithEvents(Of Object).CollectionChanged"/> event).</summary>
            ''' <param name="sender">Source of event (this instance of <see cref="MessageBoxComboBox"/>)</param>
            ''' <param name="e">Event arguments (originally passesd by <see cref="Items">Items</see>.<see cref="ListWithEvents(Of Object).CollectionChanged">CollectionChanged</see></param>
            Public Event ItemsChanged(ByVal sender As MessageBoxComboBox, ByVal e As ListWithEvents(Of Object).ListChangedEventArgs)
            ''' <summary>Raises the <see cref="ItemsChanged"/> event. Handles <see cref="Items">Items</see>.<see cref="ListWithEvents(Of Object).CollectionChanged">CollectionChanged</see> event.</summary>
            ''' <param name="sender"><see cref="Items"/> (event source)</param>
            ''' <param name="e">Event arguments</param>
            ''' <remarks>This method (after the <see cref="ItemsChanged"/> event is raised) also calls <see cref="OnChanged"/> with <paramref name="e"/> as argument.</remarks>
            Protected Overridable Sub OnItemsChanged(ByVal sender As ListWithEvents(Of Object), ByVal e As ListWithEvents(Of Object).ListChangedEventArgs)
                RaiseEvent ItemsChanged(Me, e)
                OnChanged(e)
            End Sub
            ''' <summary>Raised when value of the <see cref="Text"/> is changed</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Event arguments</param>
            ''' <remarks>This event shadows <see cref="MessageBoxControl.TextChanged"/> event only in order to change it's <see cref="BrowsableAttribute"/> and <see cref="EditorBrowsableAttribute"/></remarks>
            <Browsable(True), EditorBrowsable(EditorBrowsableState.Always)> _
            Public Shadows Event TextChanged(ByVal sender As MessageBoxComboBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            ''' <summary>Raises the <see cref="TextChanged"/> event, calls <see cref="MessageBoxComboBox.OnChanged"/> base class method</summary>
            ''' <param name="e">Event argumernts</param>
            Protected Overrides Sub OnTextChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
                RaiseEvent TextChanged(Me, e)
                MyBase.OnTextChanged(e)
            End Sub
#End Region
#Region "CTors"
            ''' <summary>CTor - initializes new instance of the <see cref="MessageBoxComboBox"/> class</summary>
            Public Sub New()
                AddHandler Items.CollectionChanged, AddressOf Me.OnItemsChanged
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
                        Dim e As New IReportsChange.ValueChangedEventArgs(Of Boolean)(old, value, "Checked")
                        OnCheckedChanged(e)
                    End If
                End Set
            End Property
            ''' <summary>raised when value of the <see cref="Checked"/> property changes</summary>
            ''' <param name="sender">Source of the event</param>
            ''' <param name="e">Event arguments containing infomation about new and old value</param>
            ''' <remarks>The <see cref="Checked"/> property can be changed programatically or by user</remarks>
            Public Event CheckedChanged(ByVal sender As MessageBoxRadioButton, ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
            ''' <summary>Raises the <see cref="CheckedChanged"/> event, calls <see cref="OnChanged"/></summary>
            ''' <param name="e">Event parameters</param>
            Protected Overridable Sub OnCheckedChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
                RaiseEvent CheckedChanged(Me, e)
                OnChanged(e)
            End Sub
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
            ''' <summary>Text is aligned to block. If target platform does not support <see cref="MessageBoxOptions.AlignJustify"/> treats it as <see cref="MessageBoxOptions.AlignLeft"/> (in ltr reading <see cref="MessageBoxOptions.AlignRight"/> in rtl reading)</summary>
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
#Region "Action"
        ''' <summary>Shows modal dialog (and waits until the dialog is closed)</summary>
        ''' <returns>Dialog result (<see cref="MessageBoxButton.Result"/> of clicked button)</returns>
        Function Show() As DialogResult
            Return Show(Nothing)
        End Function
        ''' <summary>Show modal dialog (and waits until the dialog is closed)</summary>
        ''' <param name="Owner">Parent window of dialog (may be null)</param>
        ''' <returns>Dialog result (<see cref="MessageBoxButton.Result"/> of clicked button)</returns>
        Function Show(ByVal Owner As IWin32Window) As DialogResult
            PerformDialog(True, Owner)
            Return Me.DialogResult
        End Function
        ''' <summary>Displays the dialog non-modally (execution continues immediatelly)</summary>
        Sub Display()
            Display(Nothing)
        End Sub
        ''' <summary>Displays the dialog non-modally (execution continues immediatelly)</summary>
        ''' <param name="Owner">Parent window of dialog (may be null)</param>
        Sub Display(ByVal Owner As IWin32Window)
            PerformDialog(False, Owner)
        End Sub
        ''' <summary>If overriden in derived class shows the dialog</summary>
        ''' <param name="Modal">Indicates if dialog should be shown modally (true) or modells (false)</param>
        ''' <param name="Owner">Parent window of dialog (may be null)</param>
        Protected MustOverride Sub PerformDialog(ByVal Modal As Boolean, ByVal Owner As IWin32Window)

        ''' <summary>Closes message box with <see cref="CloseResponse"/></summary>
        Public Sub Close()
            Me.Close(Me.CloseResponse)
        End Sub
        ''' <summary>If overriden in derived class closes the message box with given response</summary>
        ''' <param name="Response">Response returned by the <see cref="Show"/> function</param>
        Public MustOverride Sub Close(ByVal Response As DialogResult)
        ''' <summary>raises the <see cref="Closed"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Derived class should call this method when dialog is closed</remarks>
        Protected Sub OnClosed(ByVal e As EventArgs)
            CountDownTimer.Enabled = False
            State = States.Closed
            RaiseEvent Closed(Me, e)
        End Sub
        ''' <summary>Called when dialog is shown. Performs some initialization (timer). Raises the <see cref="Shown"/> event.</summary>
        ''' <remarks>Derived class should call thism methow after dialog is shown.</remarks>
        Protected Sub OnShown()
            If Me.Timer > TimeSpan.Zero Then
                Me.CurrentTimer = Me.Timer
                Me.CountDownTimer.Enabled = True
                OnCountDown(New EventArgs)
            End If
            State = States.Shown
            RaiseEvent Shown(Me, New EventArgs)
        End Sub
        ''' <summary>Possible state of the message box class instance</summary>
        Public Enum States
            ''' <summary>Instance have been created, but message box have not been shown yet. You can modify message box properties.</summary>
            Created
            ''' <summary>Message bos was shown to user and waits for user action. Only some properties can be modificated with effect.</summary>
            Shown
            ''' <summary>Message box was closed</summary>
            Closed
        End Enum
        ''' <summary>Contains value of the <see cref="State"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _State As States = States.Created
        ''' <summary>Gets or sets value indicating current state of the message box</summary>
        ''' <remarks>Value of this property is set by <see cref="OnShown"/> and <see cref="OnClosed"/> methods</remarks>
        Public Property State() As States
            <DebuggerStepThrough()> Get
                Return _State
            End Get
            <DebuggerStepThrough()> Private Set(ByVal value As States)
                _State = value
            End Set
        End Property
        ''' <summary>If overriden in derived class gets result of dialog (<see cref="MessageBoxButton.Result"/> of button user has clicked on)</summary>
        ''' <returns><see cref="MessageBoxButton.Result"/> of button user have clicked to or <see cref="CloseResponse"/> when message box was closed by pressing escape, closing the window or timer.</returns>
        ''' <remarks>Value of this property is valid only when <see cref="State"/> is <see cref="States.Closed"/></remarks>
        Public MustOverride ReadOnly Property DialogResult() As DialogResult
        ''' <summary>When overriden in derived class gets button user have clicked on</summary>
        ''' <returns>Button user have clicked on (or null if dialog was closed by window close button, pressing escape or timer)</returns>
        ''' <remarks>Value of this property is valid only when <see cref="State"/> is <see cref="States.Closed"/></remarks>
        Public MustOverride ReadOnly Property ClickedButton() As MessageBoxButton
#End Region
#Region "Timer"
        Private Sub CountDownTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles CountDownTimer.Tick
            OnCountDown(New EventArgs)
            If Not Me.IsCountDown Then Exit Sub
            CurrentTimer -= TimeSpan.FromSeconds(1)
            If CurrentTimer <= TimeSpan.Zero Then Me.Close() : CountDownTimer.Enabled = False
        End Sub
        ''' <summary>Gets value indicationg if counting down is curently in progress</summary>
        ''' <remarks>In order to cahnge value of this prioperty use <see cref="ResumeCountDown"/> and <see cref="StopCountDown"/> methods</remarks>
        <Browsable(False)> _
        Public ReadOnly Property IsCountDown() As Boolean
            Get
                Return CountDownTimer.Enabled
            End Get
        End Property
        ''' <summary>Timer that performs count downs</summary>
        Private WithEvents CountDownTimer As New Timer With {.interval = 1000}
        ''' <summary>Raises the <see cref="CountDown"/> event</summary>
        ''' <param name="e">Event argument</param>
        ''' <remarks>Derived class should override this method in order to catch change of count down remaining time and call base class method.</remarks>
        Protected Overridable Sub OnCountDown(ByVal e As EventArgs)
            RaiseEvent CountDown(Me, e)
        End Sub
        ''' <summary>Called when count-down is stopped by calling <see cref="StopCountDown"/></summary>
        ''' <remarks>Derived class should override this method in order to catch count-down stoped event and react somehow (hide count down text).<para>This implementation does nothing.</para></remarks>
        Protected Overridable Sub OnCountDownStopped()
        End Sub
        ''' <summary>Contains value of the <see cref="CurrentTimer"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _CurrentTimer As TimeSpan
        ''' <summary>Gets or sets current remaining time of count-down timer</summary>
        ''' <remarks>If value id <see cref="TimeSpan.Zero"/> or less, count down ends and dialog is about to be closed</remarks>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property CurrentTimer() As TimeSpan
            <DebuggerStepThrough()> Get
                Return _CurrentTimer
            End Get
            <DebuggerStepThrough()> Protected Set(ByVal value As TimeSpan)
                _CurrentTimer = value
            End Set
        End Property
        ''' <summary>Stops count-down timer ticking</summary>
        Public Sub StopCountDown()
            CountDownTimer.Enabled = False
            OnCountDownStopped()
        End Sub
        ''' <summary>Resumes previously stopped count down timer</summary>
        Public Sub ResumeCountDown()
            If Me.CurrentTimer <= TimeSpan.Zero Then Throw New InvalidOperationException("Cannot resume count-down timer when there is no time left.") 'Localize:Exception
            CountDownTimer.Enabled = True
            OnCountDown(New EventArgs)
        End Sub
        ''' <summary>Resumes previouskly stopped count down timer with new timer value</summary>
        ''' <param name="TimeLeft">Count down timer time (after which the message box is closed)</param>
        Public Sub ResumeCountDown(ByVal TimeLeft As TimeSpan)
            If TimeLeft <= TimeSpan.Zero Then Throw New ArgumentOutOfRangeException("TimeLeft", "Count down time must be greater than zero.") 'Localize:Exception
            Me.CurrentTimer = TimeLeft
            ResumeCountDown()
        End Sub
#End Region
#Region "Events"
        ''' <summary>Raised when count down timer ticks</summary>
        ''' <remarks>Count down timer ticks each second once. First the event is raised immediatelly after the dialog is shown or count-down is resumed</remarks>
        ''' <param name="sender">Source of the event (current instance)</param>
        ''' <param name="e">Event argument</param>
        Public Event CountDown(ByVal sender As MessageBox, ByVal e As EventArgs)
        ''' <summary>Raised after dialog is shown</summary>
        ''' <param name="sender">Source of the event (current instance)</param>
        ''' <param name="e">Event argument</param>
        Public Event Shown(ByVal sender As MessageBox, ByVal e As EventArgs)
        ''' <summary>Raised after dialog is closed</summary>
        ''' <param name="sender">Source of the event (current instance)</param>
        ''' <param name="e">Event argument</param>
        Public Event Closed(ByVal sender As MessageBox, ByVal e As EventArgs)
#End Region
    End Class
End Namespace
#End If