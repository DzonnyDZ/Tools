'Extracted
'TODO: Automatic sounds
Imports Tools.CollectionsT.GenericT, System.Linq, Tools.CollectionsT.CollectionTools
Imports Tools.DrawingT.DesignT
Imports System.Drawing.Design
Imports Tools.ComponentModelT
Imports System.Reflection, System.Drawing
Imports Icons = Tools.ResourcesT.Icons, Tools.TypeTools
Imports Tools.DrawingT

#If Config <= Nightly Then 'Stage Nightly
Imports System.Windows.Forms

Namespace WindowsT.IndependentT
    ''' <summary>Provides technology-independent managed base class for WinForms and WPF message boxes</summary>
    ''' <remarks>
    ''' This class implements <see cref="IReportsChange"/> and has plenty of events fo reporting changes of property values. Also types of some properties reports events when their properties are changed.
    ''' The aim of such behavior is to provide dynamic message box which can be changed as it is displayd.
    ''' However it is up to derived class which changes it will track and interpret as changes of dialog.
    ''' <para>After message box is closed, it can be shown again (so called re-cycling; see <see cref="messagebox.Recycle"/>).</para>
    ''' <para>
    ''' In order to prevent confusing multiple overloads, names of Modal_*, ModalF_*, ModelEx_* and Error_* functions are suffixed with abberivations of accepted parameters. The <see cref="MessageBox.Show"/> method stays overloaded.
    ''' Meaning of abberivations are following:
    ''' <list type="table">
    ''' <listheader><term>Abbr.</term><description>Meaning</description></listheader>
    ''' <item><term>a</term><description>args - Formatting string arguments</description></item>
    ''' <item><term>B</term><description>Buttons - Either <see cref="MessageBox.MessageBoxButton"/> objects or or-ed values of <see cref="MessageBox.MessageBoxButton.Buttons"/></description></item>
    ''' <item><term>E</term><description>Items - assorted items to be shown on message box such as buttons, checkboxes, comboboxes etc.</description></item>
    ''' <item><term>H</term><description>ShowHandler - delegate to be called when message box is shown</description></item>
    ''' <item><term>I</term><description>Icon - Either <see cref="Drawing.Icon"/>, <see cref="Drawing.Image"/> or <see cref="MessageBox.MessageBoxIcons"/> defining picture to be shown on message box</description></item>
    ''' <item><term>M</term><description>Timer - Defines count-down time for self-closing message box</description></item>
    ''' <item><term>O</term><description>Options - A <see cref="MessageBox.MessageBoxOptions"/> value</description></item>
    ''' <item><term>P</term><description>Prompt - Main text to be shown to user</description></item>
    ''' <item><term>S</term><description>Sound - Sound to be played when message box is shown</description></item>
    ''' <item><term>T</term><description>Title - Text of message box header (title bar)</description></item>
    ''' <item><term>W</term><description>Owner - Owner of messagebox - the window to which the message box will be modal</description></item>
    ''' <item><term>X</term><description>Exception - Exception message box will show information about</description></item>
    ''' </list>
    ''' </para>
    ''' </remarks>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    ''' <version version="1.5.2">Fixed: Some static functions throws exception when icon is not set or when default button is used (even implicitly)</version>
    <DefaultProperty("Prompt"), DefaultEvent("Closed")> _
    Public MustInherit Class MessageBox : Inherits Component : Implements IReportsChange
#Region "Shared"
        ''' <summary>Contains value of the <see cref="DefaultImplementation"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private Shared _DefaultImplementation As Type = GetType(FormsT.MessageBox)
        ''' <summary>Gets or sets default implementation used for messageboxes shown by static <see cref="Show"/> methods of this class</summary>
        ''' <returns>Type currently used as default implementation of message box</returns>
        ''' <value>Sets application-wide default implementation of message box</value>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        ''' <exception cref="ArgumentException">Value being set represents type that either does not derive from <see cref="MessageBox"/>, is abstract, is generic non-closed or hasn't parameter-less contructor.</exception>
        ''' <remarks>Default implementation used is <see cref="WindowsT.FormsT.MessageBox"/> which uses WinForms technology.
        ''' You can use this static poperty to change implementation of messagebox that is globaly used in your application. This property does not involve direct calls to derived classes, only calls of static methods on <see cref="MessageBox"/>.</remarks>
        Public Shared Property DefaultImplementation() As Type
            <DebuggerStepThrough()> Get
                Return _DefaultImplementation
            End Get
            Set(ByVal value As Type)
                If value Is Nothing Then Throw New ArgumentNullException("value")
                If Not value.IsSubclassOf(GetType(MessageBox)) Then Throw New ArgumentException(ResourcesT.Exceptions.TypeMustInheritFromMessageBox)
                If value.IsAbstract Then Throw New ArgumentException(ResourcesT.Exceptions.DefaultMessageBoxImplementationCannotBeAbstractType)
                If value.IsGenericTypeDefinition Then Throw New ArgumentException(ResourcesT.Exceptions.DefaultMessageBoxImplementationCannotBeGenericTypeDefinition)
                If value.GetConstructor(Type.EmptyTypes) Is Nothing Then Throw New ArgumentException(ResourcesT.Exceptions.ClassThatRepresentsDefaultMessageBoxImplementationMustHaveParameterLessConstructor)
                _DefaultImplementation = value
            End Set
        End Property
#End Region
#Region "MessageBox Definition fields"
        ''' <summary>Contaions value of the <see cref="Buttons"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _Buttons As New ListWithEvents(Of MessageBoxButton)(False, True) With {.Owner = Me}
        ''' <summary>Contaions value of the <see cref="DefaultButton"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _DefaultButton As Integer = 0
        ''' <summary>Contaions value of the <see cref="CloseResponse"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _CloseResponse As DialogResult = DialogResult.None
        ''' <summary>Contaions value of the <see cref="Prompt"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Prompt As String
        ''' <summary>Contaions value of the <see cref="Title"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Title As String
        ''' <summary>Contaions value of the <see cref="Icon"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Icon As Drawing.Image
        ''' <summary>Contaions value of the <see cref="Options"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Options As MessageBoxOptions
        ''' <summary>Contaions value of the <see cref="CheckBoxes"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _CheckBoxes As New ListWithEvents(Of MessageBoxCheckBox)(False, True) With {.Owner = Me}
        ''' <summary>Contaions value of the <see cref="ComboBox"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ComboBox As MessageBoxComboBox
        ''' <summary>Contaions value of the <see cref="Radios"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _Radios As New ListWithEvents(Of MessageBoxRadioButton)(False, True) With {.Owner = Me}
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
        ''' <summary>Contains value of the <see cref="AllowClose"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _AllowClose As Boolean = True
        ''' <summary>Contains value of the <see cref="PlayOnShow"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _PlayOnShow As MediaT.Sound
#End Region
#Region "Properties"
        ''' <summary>Gets or sets value indicating if dialog can be closed without clicking any of buttons</summary>
        ''' <remarks>This does not affacet possibility to close message box programatically using the <see cref="Close"/> method.</remarks>
        <DefaultValue(True)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <LDescription(GetType(ResourcesT.Components), "AllowClose_d")> _
        Public Property AllowClose() As Boolean
            Get
                Return _AllowClose
            End Get
            Set(ByVal value As Boolean)
                If AllowClose <> value Then
                    _AllowClose = value
                    OnAllowCloseChanged(New IReportsChange.ValueChangedEventArgs(Of Boolean)(Not value, value, "AllowClose"))
                End If
            End Set
        End Property
        ''' <summary>Defines buttons displayed on message box</summary>
        ''' <remarks>This collection reports event. You can use them to track changed of the collection either via events of the collection itself or via the <see cref="ButtonsChanged"/> event.
        ''' <para>Do not store nulls in the collection. It won't accept them and <see cref="OperationCanceledException"/> will be thrown.</para></remarks>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.WindowStyle)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <LDescription(GetType(ResourcesT.Components), "Buttons_d")> _
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
        ''' <remarks>Default button is treated as being clicked when user presses Enter. If value is set outside of range of <see cref="Buttons"/> (i.e. -1), message box has no default button.
        ''' <para>If messagebox implementation supports changes of <see cref="Buttons"/> collection when displayed, this property is changed on buttom insert/removal and it points strill to the same physical button.</para>
        ''' </remarks>
        ''' <seealso cref="System.Windows.Forms.Form.AcceptButton"/>
        ''' <seealso cref="System.Windows.Controls.Button.IsDefault"/>
        <DefaultValue(0I)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <LDescription(GetType(ResourcesT.Components), "DefaultButton_d")> _
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
        ''' <remarks>Values that are not members of the <see cref="DialogResult"/> enumeration can be safely used.
        ''' <para>If <see cref="AllowClose"/> is false this property ahs effect only when mapped to one of buttons (has same value as <see cref="MessageBoxButton.Result"/> of one buttons) and user presses escape.</para></remarks>
        ''' <seealeo cref="DialogResult"/><seealso cref="Show"/>
        <DefaultValue(GetType(DialogResult), "None")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <LDescription(GetType(ResourcesT.Components), "CloseResponse_d")> _
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
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        <LDescription(GetType(ResourcesT.Components), "Prompt_d")> _
        <Localizable(True), Editor(GetType(System.ComponentModel.Design.MultilineStringEditor), GetType(UITypeEditor))> _
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
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        <LDescription(GetType(ResourcesT.Components), "Title_d")> _
        <Localizable(True)> _
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
        ''' <remarks>Expected image size is 64×64px. Image is resized proportionaly to fit this size. This may be changed by derived class.</remarks>
        <DefaultValue(GetType(Drawing.Image), Nothing)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        <LDescription(GetType(ResourcesT.Components), "Icon_d")> _
        <Localizable(True)> _
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
        ''' <remarks>Text align applies only to prompt part of message box. In right-to-left reading text align has oposite meaning - <see cref="MessageBoxOptions.AlignLeft"/> aligns to right and <see cref="MessageBoxOptions.AlignRight"/> aligns to left.</remarks>
        <DefaultValue(GetType(MessageBoxOptions), "0")> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.WindowStyle)> _
        <LDescription(GetType(ResourcesT.Components), "Options_d")> _
        <Editor(GetType(DropDownControlEditor(Of MessageBoxOptions, MessageBoxOptionsEditor)), GetType(UITypeEditor))> _
        <Localizable(True)> _
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
        ''' <summary>Check boxes displayed in messaqge box</summary>
        ''' <remarks>This collection reports event. You can use them to track changes of the collection either via handling events of the collection or via the <see cref="CheckBoxesChanged"/> event.
        ''' <para>Do not store nulls in the collection. It won't accept them and <see cref="OperationCanceledException"/> will be thrown.</para></remarks>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.WindowStyle)> _
        <LDescription(GetType(ResourcesT.Components), "CheckBoxes_d")> _
        Public ReadOnly Property CheckBoxes() As ListWithEvents(Of MessageBoxCheckBox)
            <DebuggerStepThrough()> Get
                Return _CheckBoxes
            End Get
        End Property
        ''' <summary>Gets value indicating if the <see cref="CheckBoxes"/> property has changed and thus should be serialized</summary>
        ''' <returns>True when <see cref="CheckBoxes"/>.<see cref="ListWithEvents.Count">Count</see> is non-zero</returns>
        Private Function ShouldSerializeCheckBoxes() As Boolean
            Return CheckBoxes.Count <> 0
        End Function
        ''' <summary>Resets value of the <see cref="CheckBoxes"/> property to its default value (clears it)</summary>
        Private Sub ResetCheckBoxes()
            CheckBoxes.Clear()
        End Sub
        ''' <summary>Gets or sets combo box (drop down list) displayed in message box</summary>
        <DefaultValue(GetType(MessageBoxComboBox), Nothing)> _
        <TypeConverter(GetType(ExpandableObjectConverter))> _
        <Editor(GetType(NewEditor), GetType(UITypeEditor))> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.WindowStyle)> _
        <LDescription(GetType(ResourcesT.Components), "ComboBox_d")> _
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
        ''' <remarks>This collection reports event. You can use them to track changes of the collection either via handling events of the collection or via the <see cref="RadiosChanged"/> event.
        ''' <para>Do not store nulls in the collection. It won't accept them and <see cref="OperationCanceledException"/> will be thrown.</para></remarks>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.WindowStyle)> _
        <LDescription(GetType(ResourcesT.Components), "Radios_d")> _
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
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
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
        <DefaultValue(GetType(Object), Nothing)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
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
        <DefaultValue(GetType(Object), Nothing)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
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
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <LDescription(GetType(ResourcesT.Components), "Timer_d")> _
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
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <LDescription(GetType(ResourcesT.Components), "TimeButton_d")> _
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
        ''' <summary>Gets or sets sound played when message box is shown</summary>
        ''' <value>Sound played when message box is shown. Null if no sound shall be played.</value>
        ''' <remarks>Current sound to be played when message box is show. Null if no sound is played.</remarks>
        <DefaultValue(GetType(MediaT.Sound), Nothing)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
        <LDescription(GetType(ResourcesT.Components), "PlayOnShow_d")> _
        Public Property PlayOnShow() As MediaT.Sound
            Get
                Return _PlayOnShow
            End Get
            Set(ByVal value As MediaT.Sound)
                _PlayOnShow = value
            End Set
        End Property
#End Region
#Region "CTors"
        ''' <summary>Adds event handlers to collections</summary>
        ''' <version version="1.5.2">Access changed from private to protected, made virtual</version>
        ''' <remarks>In derived class override this method to attach cancelable handlers - later it is impossible. Do not forget to call base-class method. Note: This method is called by CTor.</remarks>
        Protected Overridable Sub AddHandlers()
            AddHandler Buttons.CollectionChanged, AddressOf OnButtonsChanged
            AddHandler Radios.CollectionChanged, AddressOf OnRadiosChanged
            AddHandler CheckBoxes.CollectionChanged, AddressOf OnCheckBoxesChanged
            AddHandler Buttons.Adding, AddressOf Buttons_Adding
            AddHandler Radios.Adding, AddressOf Radios_Adding
            AddHandler CheckBoxes.Adding, AddressOf CheckBoxes_Adding
            AddHandler Buttons.ItemChanging, AddressOf Buttons_ItemChanging
            AddHandler Radios.ItemChanging, AddressOf Radios_ItemChanging
            AddHandler CheckBoxes.ItemChanging, AddressOf CheckBoxes_ItemChanging
            CheckBoxes.AllowAddCancelableEventsHandlers = False
            Radios.AllowAddCancelableEventsHandlers = False
            Buttons.AllowAddCancelableEventsHandlers = False
        End Sub
        ''' <summary>Default CTor - creates messagebox with just one button <see cref="MessageBoxButton.OK"/></summary>
        Public Sub New()
            AddHandlers()
            Me.Buttons.Add(MessageBoxButton.OK)
        End Sub
#End Region
#Region "Property events"
        ''' <summary>Raised when value of the <see cref="AllowClose"/> property changes</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event AllowCloseChanged As EventHandler(Of MessageBox, IReportsChange.ValueChangedEventArgs(Of Boolean))
        ''' <summary>Raises tha <see cref="AllowCloseChanged"/> event. Calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnAllowCloseChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Boolean))
            RaiseEvent AllowCloseChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised when value of member changes</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event information. For changes of <see cref="Buttons"/>, <see cref="CheckBoxes"/> and <see cref="Radios"/> collections event argument of <see cref="ListWithEvents.CollectionChanged"/> is passed instead of argument of <see cref="ListWithEvents.Changed"/>.</param>
        ''' <remarks><paramref name="e"/>Should contain additional information that can be used in event-handling code (e.g. use <see cref="IReportsChange.ValueChangedEventArgs"/> class)</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event Changed(ByVal sender As IReportsChange, ByVal e As System.EventArgs) Implements IReportsChange.Changed
        ''' <summary>Raises the <see cref="Changed"/> event</summary>
        ''' <param name="e">Event parameters</param>
        Protected Overridable Sub OnChanged(ByVal e As EventArgs)
            RaiseEvent Changed(Me, e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="DefaultButton"/> property changes</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event DefaultButtonChanged As EventHandler(Of MessageBox, IReportsChange.ValueChangedEventArgs(Of Integer))
        ''' <summary>Raises the <see cref="DefaultButtonChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnDefaultButtonChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Integer))
            RaiseEvent DefaultButtonChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="CloseResponse"/> property changes</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event CloseResponseChanged As EventHandler(Of MessageBox, IReportsChange.ValueChangedEventArgs(Of DialogResult))
        ''' <summary>Raises the <see cref="OnCloseResponseChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnCloseResponseChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of DialogResult))
            RaiseEvent CloseResponseChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="Prompt"/> property changes</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event PromptChanged As EventHandler(Of MessageBox, IReportsChange.ValueChangedEventArgs(Of String))
        ''' <summary>Raises the <see cref="PromptChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnPromptChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            RaiseEvent PromptChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="Title"/> property changes</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event TitleChanged As EventHandler(Of MessageBox, IReportsChange.ValueChangedEventArgs(Of String))
        ''' <summary>Raises the <see cref="TitleChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnTitleChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of String))
            RaiseEvent TitleChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="Icon"/> property changes</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event IconChanged As EventHandler(Of MessageBox, IReportsChange.ValueChangedEventArgs(Of Drawing.Image))
        ''' <summary>Raises the <see cref="IconChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnIconChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Drawing.Image))
            RaiseEvent IconChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="Options"/> property changes</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event OptionsChanged As EventHandler(Of MessageBox, IReportsChange.ValueChangedEventArgs(Of MessageBoxOptions))
        ''' <summary>Raises the <see cref="OptionsChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnOptionsChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of MessageBoxOptions))
            RaiseEvent OptionsChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised when content of the <see cref="CheckBoxes"/> collection changes</summary>
        ''' <param name="sender">Source of the event - always this instance of <see  cref="MessageBox"/></param>
        ''' <param name="e">Event arguments. Argument e of <see cref="Radios">Radios</see>.<see cref="ListWithEvents(Of MessageBoxCheckBox).CollectionChanged"/> is passed directly here.</param>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event CheckBoxesChanged(ByVal sender As MessageBox, ByVal e As ListWithEvents(Of MessageBoxCheckBox).ListChangedEventArgs)
        ''' <summary>Raises the <see cref="CheckBoxesChanged"/> event. Handles <see cref="Radios">Radios</see>.<see cref="ListWithEvents(Of MessageBoxRadioButton).CollectionChanged">CollectionChanged</see> event. Calls <see cref="OnChanged"/>.</summary>
        ''' <param name="sender">Source ot the event - <see cref="Radios"/></param>
        ''' <param name="e">Event arguments. Those arguemnts are directly passed to <see cref="RadiosChanged"/> and <see cref="OnChanged"/></param>
        Protected Overridable Sub OnCheckBoxesChanged(ByVal sender As ListWithEvents(Of MessageBoxCheckBox), ByVal e As ListWithEvents(Of MessageBoxCheckBox).ListChangedEventArgs)
            RaiseEvent CheckBoxesChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="ComboBox"/> property changes</summary>
        ''' <remarks>This event tracks only changes of value of the <see cref="ComboBox"/> property itsels. It does not track changes of values of its inner properties.</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event ComboBoxChanged As EventHandler(Of MessageBox, IReportsChange.ValueChangedEventArgs(Of MessageBoxComboBox))
        ''' <summary>Raises the <see cref="ComboBoxChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnComboBoxChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of MessageBoxComboBox))
            RaiseEvent ComboBoxChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="TopControl"/> property changes</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event TopControlChanged As ControlChangedEventHandler
        ''' <summary>Raises the <see cref="TopControlChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnTopControlChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
            RaiseEvent TopControlChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="MidControl"/> property changes</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event MidControlChanged As ControlChangedEventHandler
        ''' <summary>Raises the <see cref="MidControlChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnMidControlChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
            RaiseEvent TopControlChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="BottomControl"/> property changes</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event BottomControlChanged As ControlChangedEventHandler
        ''' <summary>Delegate for events <see cref="BottomControlChanged"/>, <see cref="TopControlChanged"/> and <see cref="MidControlChanged"/></summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Delegate Sub ControlChangedEventHandler(ByVal sender As MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
        ''' <summary>Raises the <see cref="BottomControlChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnBottomControlChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Object))
            RaiseEvent BottomControlChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="Timer"/> property changes</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event TimerChanged As EventHandler(Of MessageBox, IReportsChange.ValueChangedEventArgs(Of TimeSpan))
        ''' <summary>Raises the <see cref="TimerChanged"/> event, calls <see cref="OnChanged"/></summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnTimerChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of TimeSpan))
            RaiseEvent TimerChanged(Me, e)
            OnChanged(e)
        End Sub
        ''' <summary>Raised wghen value of the <see cref="TimeButton"/> property changes</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)> _
        Public Event TimeButtonChanged As EventHandler(Of MessageBox, IReportsChange.ValueChangedEventArgs(Of Integer))
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
        ''' <version version="1.5.2">Added implementation of the <see cref="INotifyPropertyChanged"/> interface. No changes needed in derived classes when they call <see cref="MessageBoxControl.OnChanged"/> with <see cref="IReportsChange.ValueChangedEventArgsBase"/> or <see cref="PropertyChangedEventArgs"/>.</version>
        ''' <version version="1.5.2">Added implementation of <see cref="CollectionsT.GenericT.ICollectionCancelItem"/></version>
        <DefaultProperty("Text"), DefaultEvent("Changed")> _
        Public MustInherit Class MessageBoxControl : Implements IReportsChange, INotifyPropertyChanged, CollectionsT.GenericT.ICollectionCancelItem
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
            ''' <summary>Gets or sets text displayed on control</summary>
            <DefaultValue(GetType(String), Nothing), KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
            <LDescription(GetType(ResourcesT.Components), "MessageBoxButton_Text_d")> _
            <Localizable(True)> _
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
            <DefaultValue(GetType(String), Nothing), KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
            <LDescription(GetType(ResourcesT.Components), "ToolTip_d")> _
            <Localizable(True)> _
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
            <DefaultValue(True), KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
            <LDescription(GetType(ResourcesT.Components), "Enabled_d")> _
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
            ''' <summary>Raises the <see cref="Changed"/> event (and <see cref="PropertyChanged"/> when <paramref name="e"/> is <see cref="PropertyChangedEventArgs"/>)</summary>
            ''' <param name="e">Event parameters - should be <see cref="IReportsChange.ValueChangedEventArgs(Of T)"/> or <see cref="CollectionChangeEventArgs(Of T)"/></param>
            ''' <version version="1.5.2">Added raising of <see cref="PropertyChanged"/></version>
            Protected Overridable Sub OnChanged(ByVal e As EventArgs)
                RaiseEvent Changed(Me, e)
                If TypeOf e Is PropertyChangedEventArgs Then OnPropertyChanged(e)
            End Sub
            ''' <summary>Raises the <see cref="PropertyChanged"/> event</summary>
            ''' <param name="e">Event arguments</param>
            ''' <version version="1.5.2">Method added</version>
            Protected Overridable Sub OnPropertyChanged(ByVal e As PropertyChangedEventArgs)
                RaiseEvent PropertyChanged(Me, e)
            End Sub

            ''' <summary>Contains value of the <see cref="Control"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Control As Object
            ''' <summary>Gets or sets physical control that currently implements this control</summary>
            ''' <remarks>
            ''' This property is intended to be used by GUI implementation to store instance of for example <see cref="Button"/> that currently represents the control.
            ''' Its up to implementation how and if it will use this property. Caller should not rely on content of the property.
            ''' </remarks>
            <EditorBrowsable(EditorBrowsableState.Advanced), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
            Public Property Control() As Object
                <DebuggerStepThrough()> Get
                    Return _Control
                End Get
                <DebuggerStepThrough()> Protected Friend Set(ByVal value As Object)
                    _Control = value
                End Set
            End Property

            ''' <summary>Occurs when a property value changes.</summary>
            ''' <version version="1.5.2">Event introduced</version>
            Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
#Region "ICollectionCancelItem"
            ''' <summary>Called before item is placed into collection</summary>
            ''' <param name="Collection">Collection item is aboutto be placed into</param>
            ''' <param name="index">Index item is being to be placed onto; null when collection does not support indexing.</param>
            ''' <param name="Replace">True when item at index <paramref name="index"/> will be replaced by this instance; false if this instance will be inserted at <paramref name="index"/> and all subsequent items will be moved to nex index.</param>
            ''' <exception cref="InvalidOperationException">This control was already added to collection owned by <see cref="MessageBox"/> and now attempt to add it to another such collection is done.</exception>
            Private Sub OnBeingAddedToCollection(ByVal Collection As System.Collections.ICollection, ByVal index As Integer?, ByVal Replace As Boolean) Implements CollectionsT.GenericT.ICollectionCancelItem.OnAdding
                If _Collection IsNot Nothing AndAlso TypeOf Collection Is ListWithEventsBase AndAlso TypeOf DirectCast(Collection, ListWithEventsBase).Owner Is MessageBox Then Throw New InvalidOperationException(ResourcesT.Exceptions.ThisControlIsAlreadyUsedByMessageBoxItCannotBeUsedTwice)
            End Sub
            ''' <summary>Called before all items are removed from collection by clearing it</summary>
            ''' <param name="Collection">Collection item is about to be removed from</param>
            Private Sub OnCollectionClearing(ByVal Collection As System.Collections.ICollection) Implements CollectionsT.GenericT.ICollectionCancelItem.OnClearing
            End Sub
            ''' <summary>Called before item is removed from collection</summary>
            ''' <param name="Collection">Collection item is about to be removed from</param>
            ''' <param name="index">Index item is currently placed on; null when collection does not support indexing.</param>
            Private Sub OnBeingRemovedFromCollection(ByVal Collection As System.Collections.ICollection, ByVal index As Integer?) Implements CollectionsT.GenericT.ICollectionCancelItem.OnRemoving
            End Sub
            ''' <summary>Contains value of the <see cref="Collections"/> property</summary>
            Private _Collections As New List(Of ICollection)
            ''' <summary>If supported by collection item gets all the collections item is in</summary>
            ''' <returns>All the collections item is placed in; null when not supported by item class.</returns>
            ''' <remarks><para>If item is placed multiple times in the same collection, this property contains this collection multiple times.</para></remarks>
            Private ReadOnly Property CollectionsThisControlIsIn() As System.Collections.Generic.IEnumerable(Of System.Collections.ICollection) Implements CollectionsT.GenericT.ICollectionNotifyItem.Collections
                Get
                    Return _Collections
                End Get
            End Property
            ''' <summary>Collection which's owner owns this control</summary>
            Private WithEvents _Collection As ListWithEventsBase
            ''' <summary>Gets collection which's owner owns this control</summary>
            ''' <returns>Collection which's <see cref="ListWithEventsBase.Owner"/> owns this control</returns>
            ''' <version version="1.5.2">Property added</version>
            Protected Property Collection() As ListWithEventsBase
                Get
                    Return _Collection
                End Get
                Private Set(ByVal value As ListWithEventsBase)
                    _Collection = value
                End Set
            End Property

            ''' <summary>Gets messagebox this control is owned by</summary>
            ''' <returns><see cref="IndependentT.MessageBox"/> this control is owned by; or null</returns>
            ''' <version version="1.5.2">Property added</version>
            <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
            Public ReadOnly Property OwnerMessageBox() As MessageBox
                Get
                    If Collection Is Nothing Then Return Nothing Else Return Collection.Owner
                End Get
            End Property

            ''' <summary>Called after item is added to collection</summary>
            ''' <param name="Collection">Collection item was added into</param>
            ''' <param name="index">Index at which the item was added. Note: Index may change later without notice (i.e. when collection gets sorted). Ic collection does not support indexing value is null.</param>
            ''' <version version="1.5.2">Method added</version>
            Protected Overridable Sub OnAddedToCollection(ByVal Collection As System.Collections.ICollection, ByVal index As Integer?) Implements CollectionsT.GenericT.ICollectionNotifyItem.OnAdded
                _Collections.Add(Collection)
                If TypeOf Collection Is ListWithEventsBase AndAlso TypeOf DirectCast(Collection, ListWithEventsBase).Owner Is MessageBox Then _
                    Me.Collection = Collection
            End Sub

            ''' <summary>Called after item is removed from collection (or after collection was cleared)</summary>
            ''' <param name="Collection">Collection item was removed from</param>
            ''' <version version="1.5.2">Method added</version>
            Protected Overridable Sub OnRemovedFromCollection(ByVal Collection As System.Collections.ICollection) Implements CollectionsT.GenericT.ICollectionNotifyItem.OnRemoved
                _Collections.Remove(Collection)
                If Me.Collection Is Collection Then Me.Collection = Nothing
            End Sub
#End Region

            Private Sub Collection_Changed(ByVal sender As IReportsChange, ByVal e As System.EventArgs) Handles _Collection.Changed
                If TypeOf e Is IReportsChange.ValueChangedEventArgs(Of Object) AndAlso DirectCast(e, IReportsChange.ValueChangedEventArgs(Of Object)).ValueName = "Owner" Then
                    With DirectCast(e, IReportsChange.ValueChangedEventArgs(Of Object))
                        If Not TypeOf .NewValue Is MessageBox Then Collection = Nothing _
                        Else OnOwnerChanged(New IReportsChange.ValueChangedEventArgs(Of MessageBox)(.OldValue, .NewValue, "OwnerMessageBox"))
                    End With
                End If
            End Sub
            ''' <summary>Raises the <see cref="OwnerChanged"/> event, calse <see cref="OnChanged"/></summary>
            ''' <param name="e">Event arguments</param>
            ''' <version version="1.5.2">Method added</version>
            Protected Overridable Sub OnOwnerChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of MessageBox))
                RaiseEvent OwnerChanged(Me, e)
                OnChanged(e)
            End Sub
            ''' <summary>Raised when value of the <see cref="OwnerMessageBox"/> property changes</summary>
            ''' <version version="1.5.2">Event added</version>
            Public Event OwnerChanged As EventHandler(Of IReportsChange.ValueChangedEventArgs(Of MessageBox))
        End Class
        ''' <summary>Represents button for <see cref="MessageBox"/></summary>
        ''' <completionlist cref="MessageBoxButton"/>
        <DefaultEvent("ClickPreview")> _
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
            ''' <summary>Raised when button is clicked, before action associated with the button is taken. This event can be canceled.</summary>
            ''' <param name="e">Event arguments. Can be used to cancel the event. <paramref name="e"/>.<see cref="CancelEventArgs.Cancel">Cancel</see> false means tha message box will be closed; false means the message box will remain open.</param>
            ''' <param name="sender">Instance of <see cref="MessageBoxButton"/> that have raised the event</param>
            ''' <remarks>If <see cref="Result"/> is <see cref="HelpDialogResult"/> <paramref name="e"/>.<see cref="CancelEventArgs.Cancel">Cancel</see> is pre-set to true. That means that if it is not set to false, message box is not closed when help button is clicked.</remarks>
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Action)> _
            <LDescription(GetType(ResourcesT.Components), "ClickPreview_d")> _
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
            ''' <remarks>In case you need to define your own buttons you can use this property and set it to value thet is not member of the <see cref="DialogResult"/> enumeration.
            ''' <para>Special result value <see cref="HelpDialogResult"/> defines help button. The <see cref="MessageBox"/> class does not pefrom any help-providing actions for that button, only, by default, thius button does not cause the messagebox to be closed.</para>
            ''' </remarks>
            ''' <seealso cref="MessageBoxButton.Help"/>
            <DefaultValue(GetType(DialogResult), "None")> _
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
            <LDescription(GetType(ResourcesT.Components), "Result_d")> _
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
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
            <LDescription(GetType(ResourcesT.Components), "AccessKey_d")> _
            <Localizable(True)> _
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

#Region "CTors"
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
            ''' <summary>CTor from <see cref="DialogResult"/></summary>
            ''' <param name="Result">Result of this button. Also determines text.</param>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="Result"/> is not member of <see cref="DialogResult"/></exception>
            ''' <exception cref="ArgumentException"><paramref name="Result"/> is <see cref="DialogResult.None"/></exception>
            Public Sub New(ByVal Result As DialogResult)
                Select Case Result
                    Case Windows.Forms.DialogResult.Abort : InitBy(Abort)
                    Case Windows.Forms.DialogResult.Cancel : InitBy(Cancel)
                    Case Windows.Forms.DialogResult.Ignore : InitBy(Ignore)
                    Case Windows.Forms.DialogResult.No : InitBy(No)
                    Case Windows.Forms.DialogResult.None : Throw New ArgumentException(ResourcesT.Exceptions.ResultCannotBeNone, "Result")
                    Case Windows.Forms.DialogResult.OK : InitBy(OK)
                    Case Windows.Forms.DialogResult.Retry : InitBy(Retry)
                    Case Windows.Forms.DialogResult.Yes : InitBy(Yes)
                    Case Else : Throw New InvalidEnumArgumentException("Result", Result, Result.GetType)
                End Select
            End Sub
            ''' <summary>CTor from <see cref="Windows.MessageBoxResult"/></summary>
            ''' <param name="Result">Result of this button. Also determines text.</param>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="Result"/> is not member of <see cref="Windows.MessageBoxResult"/></exception>
            ''' <exception cref="ArgumentException"><paramref name="Result"/> is <see cref="Windows.MessageBoxResult.None"/></exception>
            Public Sub New(ByVal Result As Windows.MessageBoxResult)
                Select Case Result
                    Case Windows.MessageBoxResult.Cancel : InitBy(Cancel)
                    Case Windows.MessageBoxResult.No : InitBy(No)
                    Case Windows.MessageBoxResult.No : Throw New ArgumentException(ResourcesT.Exceptions.ResultCannotBeNone, "Result")
                    Case Windows.MessageBoxResult.OK : InitBy(OK)
                    Case Windows.MessageBoxResult.Yes : InitBy(Yes)
                    Case Else : Throw New InvalidEnumArgumentException("Result", Result, Result.GetType)
                End Select
            End Sub
            ''' <summary>CTor from <see cref="Microsoft.VisualBasic.MsgBoxResult"/></summary>
            ''' <param name="Result">Result of this button. Also determines text.</param>
            ''' <exception cref="InvalidEnumArgumentException"><paramref name="Result"/> is not member of <see cref="Microsoft.VisualBasic.MsgBoxResult"/></exception>
            Public Sub New(ByVal Result As Microsoft.VisualBasic.MsgBoxResult)
                Select Case Result
                    Case Microsoft.VisualBasic.MsgBoxResult.Abort : InitBy(Abort)
                    Case Microsoft.VisualBasic.MsgBoxResult.Cancel : InitBy(Cancel)
                    Case Microsoft.VisualBasic.MsgBoxResult.Ignore : InitBy(Ignore)
                    Case Microsoft.VisualBasic.MsgBoxResult.No : InitBy(No)
                    Case Microsoft.VisualBasic.MsgBoxResult.Ok : InitBy(OK)
                    Case Microsoft.VisualBasic.MsgBoxResult.Retry : InitBy(Retry)
                    Case Microsoft.VisualBasic.MsgBoxResult.Yes : InitBy(Yes)
                    Case Else : Throw New InvalidEnumArgumentException("Result", Result, Result.GetType)
                End Select
            End Sub

            ''' <summary>Cloning CTor</summary>
            ''' <param name="Other">Instance to initialize new instance with</param>
            ''' <remarks>Does not copy event handler, uses properties only.</remarks>
            ''' <exception cref="ArgumentNullException"><paramref name="Other"/> is null</exception>
            Public Sub New(ByVal Other As MessageBoxButton)
                Try
                    Me.InitBy(Other)
                Catch ex As ArgumentNullException
                    Throw New ArgumentNullException("Other", ex)
                End Try
            End Sub
            ''' <summary>Initializes current instance by another instance</summary>
            ''' <param name="Button">Instance to initialize current instance with</param>
            ''' <remarks>Does not initializes button events</remarks>
            ''' <exception cref="ArgumentNullException"><paramref name="Button"/> is null</exception>
            Private Sub InitBy(ByVal Button As MessageBoxButton)
                If Button Is Nothing Then Throw New ArgumentNullException("Button")
                With Button
                    Me.Text = .Text
                    Me.AccessKey = .AccessKey
                    Me.ToolTip = .ToolTip
                    Me.Enabled = .Enabled
                    Me.Result = .Result
                End With
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
            ''' <remarks>This function should be called only by class derived from <see cref="MessageBox"/> which owns the button.</remarks>
            <EditorBrowsable(EditorBrowsableState.Advanced)> _
            Protected Friend Function OnClick() As Boolean
                Dim e As New CancelEventArgs
                If Me.Result = HelpDialogResult Then e.Cancel = True
                RaiseEvent ClickPreview(Me, e)
                Return Not e.Cancel
            End Function
#Region "Predefined buttons"
            ''' <summary>Default OK button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property OK() As MessageBoxButton
                Get
                    Return New MessageBoxButton(My.Resources.OK, DialogResult.OK, My.Resources.OK_access)
                End Get
            End Property
            ''' <summary>Default Cancle button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property Cancel() As MessageBoxButton
                Get
                    Return New MessageBoxButton(My.Resources.Cancel, DialogResult.Cancel, My.Resources.Cancel_access)
                End Get
            End Property
            ''' <summary>Default Yes button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property Yes() As MessageBoxButton
                Get
                    Return New MessageBoxButton(My.Resources.Yes, DialogResult.Yes, My.Resources.Yes_access)
                End Get
            End Property
            ''' <summary>Defaut No button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property No() As MessageBoxButton
                Get
                    Return New MessageBoxButton(My.Resources.No, DialogResult.No, My.Resources.No_access)
                End Get
            End Property
            ''' <summary>Default Abort button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property Abort() As MessageBoxButton
                Get
                    Return New MessageBoxButton(My.Resources.Abort, DialogResult.Abort, My.Resources.Abort_access)
                End Get
            End Property
            ''' <summary>Default Retry button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property Retry() As MessageBoxButton
                Get
                    Return New MessageBoxButton(My.Resources.Retry, DialogResult.Retry, My.Resources.Retry_access)
                End Get
            End Property
            ''' <summary>Default Ignore button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            Public Shared ReadOnly Property Ignore() As MessageBoxButton
                Get
                    Return New MessageBoxButton(My.Resources.Ignore, DialogResult.Ignore, My.Resources.Ignore_access)
                End Get
            End Property
            ''' <summary>Default Help button</summary>
            ''' <returns>On each call retirns another (newly created instance) of button</returns>
            ''' <remarks>
            ''' <see cref="MessageBoxButton.Result"/> of this button is <see cref="HelpDialogResult"/>.
            ''' The only difference between treating button with <see cref="Result"/> set to <see cref="HelpDialogResult"/> and other buttons is that by default dialog is not closed when help button is clicked.
            ''' <see cref="MessageBox"/> does not take any help-providing action. See the <see cref="ClickPreview"/> event in order to see how is controlled if dialog closes when button is clicked or not.
            ''' </remarks>
            Public Shared ReadOnly Property Help() As MessageBoxButton
                Get
                    Return New MessageBoxButton(My.Resources.Help, HelpDialogResult, My.Resources.Help_access)
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
#Region "IsDefault / IsCancel"
            ''' <summary>Gets value indication if this button is default of message box</summary>
            ''' <returns>True if this button is default; false if it is not</returns>
            ''' <remarks>This poperty returns correct value only when button is stored in <see cref="ListWithEvents(Of T)"/> with <see cref="ListWithEvents.Owner"/> set to <see cref="MessageBox"/>. Then its cange is reported via <see cref="INotifyPropertyChanged"/>.
            ''' <para>Individual <see cref="MessageBox"/> implementation may, or may not utilize this property. It can ignore defualt button at all or determine it from <see cref="MessageBox.DefaultButton"/>.</para></remarks>
            ''' <version version="1.5.2">Property added</version>
            <Browsable(False)> _
            Public ReadOnly Property IsDefault() As Boolean
                Get
                    Return OwnerMessageBox IsNot Nothing AndAlso OwnerMessageBox.Buttons.IndexOf(Me) = OwnerMessageBox.DefaultButton
                End Get
            End Property
            ''' <summary>Gets value if this button should be considered cance button</summary>
            ''' <returns>True if this button should be treated as cancel button; false it should not.</returns>
            ''' <remarks>This poperty returns correct value only when button is stored in <see cref="ListWithEvents(Of T)"/> with <see cref="ListWithEvents.Owner"/> set to <see cref="MessageBox"/>. Then its cange is reported via <see cref="INotifyPropertyChanged"/>.
            ''' <para>Individual <see cref="MessageBox"/> implementation may, or may not utilize this property. It can ignore defualt button at all or determine it from <see cref="MessageBox.CloseResponse"/>.</para></remarks>
            ''' <version version="1.5.2">Property added</version>
            <Browsable(False)> _
            Public ReadOnly Property IsCancel() As Boolean
                Get
                    Return OwnerMessageBox IsNot Nothing AndAlso OwnerMessageBox.CloseResponse = Me.Result AndAlso OwnerMessageBox.Buttons.FirstOrDefault(Function(a) a.Result = Me.Result) Is Me
                End Get
            End Property
            ''' <summary>Raises the <see cref="OwnerChanged"/> event, calse <see cref="OnChanged"/></summary>
            ''' <param name="e">Event arguments</param>
            ''' <version version="1.5.2">Method added</version>
            Protected Overrides Sub OnOwnerChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of MessageBox))
                If e.OldValue IsNot Nothing Then
                    RemoveHandler e.OldValue.DefaultButtonChanged, AddressOf OwnerMessageBox_DefaultButtonChanged
                    RemoveHandler e.OldValue.CloseResponseChanged, AddressOf OwnerMessageBox_CloseResponseChanged
                End If
                If e.NewValue IsNot Nothing Then
                    AddHandler e.NewValue.DefaultButtonChanged, AddressOf OwnerMessageBox_DefaultButtonChanged
                    AddHandler e.NewValue.CloseResponseChanged, AddressOf OwnerMessageBox_CloseResponseChanged
                End If
                MyBase.OnOwnerChanged(e)
                OnPropertyChanged(New PropertyChangedEventArgs("IsDefault"))
                OnPropertyChanged(New PropertyChangedEventArgs("IsCancel"))
            End Sub
            ''' <summary>Handles change of <see cref="OwnerMessageBox"/>.<see cref="MessageBox.DefaultButton">DefaultButton</see></summary>
            ''' <param name="sender"><see cref="OwnerMessageBox"/></param>
            ''' <param name="e">Event arguments</param>
            Private Sub OwnerMessageBox_DefaultButtonChanged(ByVal sender As MessageBox, ByVal e As EventArgs)
                OnPropertyChanged(New PropertyChangedEventArgs("IsDefault"))
            End Sub
            ''' <summary>Handles change of <see cref="OwnerMessageBox"/>.<see cref="MessageBox.DefaultButton">DefaultButton</see></summary>
            ''' <param name="sender"><see cref="OwnerMessageBox"/></param>
            ''' <param name="e">Event arguments</param>
            Private Sub OwnerMessageBox_CloseResponseChanged(ByVal sender As MessageBox, ByVal e As EventArgs)
                OnPropertyChanged(New PropertyChangedEventArgs("IsCancel"))
            End Sub
#End Region
            ''' <summary>Gets text of button with platform-specific accesskey indication</summary>
            ''' <returns>If <see cref="OwnerMessageBox"/> is not set returns <see cref="Text"/>; if it is set uses <see cref="GetTextWithAccessKey"/>.</returns>
            ''' <version version="1.5.2">Property added</version>
            <Browsable(False)> _
            Public Overridable ReadOnly Property TextIncludingAccessKey() As String
                Get
                    If OwnerMessageBox Is Nothing Then Return Text
                    Return OwnerMessageBox.GetTextWithAccessKey(Me.Text, Me.AccessKey)
                End Get
            End Property
        End Class
        ''' <summary>Value of the <see cref="MessageBoxButton.Result"/> property for predefined <see cref="Help">Help</see> button</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Const HelpDialogResult As DialogResult = Integer.MinValue
        ''' <summary>Represents check box control for <see cref="MessageBox"/></summary>
        <DefaultEvent("StateChanged")> _
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
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Action)> _
            <LDescription(GetType(ResourcesT.Components), "StateChanged_d")> _
            Public Event StateChanged(ByVal sender As MessageBoxCheckBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of CheckState))
            ''' <summary>Gets or sets value indicating if user can change state of checkbox between 3 or 2 states</summary>
            ''' <remarks>2-state CheckBox allows user to change state only to <see cref="CheckState.Checked"/> or <see cref="CheckState.Unchecked"/></remarks>
            <DefaultValue(False)> _
            <LDescription(GetType(ResourcesT.Components), "ThreeState_d")> _
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
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
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
            <LDescription(GetType(ResourcesT.Components), "State_d")> _
            Public Property State() As CheckState
                <DebuggerStepThrough()> Get
                    Return _State
                End Get
                Set(ByVal value As CheckState)
                    If Not InEnum(value) Then Throw New InvalidEnumArgumentException("value", value, GetType(CheckState))
                    Dim old = State
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
        <DefaultProperty("Items"), DefaultEvent("SelectedIndexChanged")> _
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
            ''' <summary>gets or sets value indicating if user can type any text to combo box</summary>
            <DefaultValue(False)> _
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Behavior)> _
            <LDescription(GetType(ResourcesT.Components), "Editable_d")> _
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
            ''' <summary>Itels in drop down of combo box</summary>
            ''' <remarks>Register handlers with events of <see cref="ListWithEvents(Of T)"/> returned by this property or use <see cref="ItemsChanged"/> event in order to track changes of the collection.</remarks>
            <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
            <LDescription(GetType(ResourcesT.Components), "Items_d")> _
            <Localizable(True)> _
            Public ReadOnly Property Items() As ListWithEvents(Of Object)
                <DebuggerStepThrough()> Get
                    Return _Items
                End Get
            End Property
            ''' <summary>Gets value indicating if the <see cref="Items"/> property should be serialized</summary>
            Private Function ShouldSerializeItems() As Boolean
                Return Items.Count > 0
            End Function
            ''' <summary>Resets the <see cref="Items"/> property to its decault value - removes all items.</summary>
            Private Sub ResetItems()
                Items.Clear()
            End Sub
            ''' <summary>Indicates member (property or field) used to obtain text displayed in drop down for each item.</summary>
            ''' <seealso cref="ComboBox.DisplayMember"/>
            <DefaultValue(GetType(String), Nothing)> _
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
            <LDescription(GetType(ResourcesT.Components), "DisplayMember_d")> _
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
            ''' <summary>Gets or sets value indicating currently selected item</summary>
            ''' <seealso cref="SelectedIndex"/>
            <DefaultValue(GetType(Object), Nothing), Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
            Public Property SelectedItem() As Object
                <DebuggerStepThrough()> Get
                    Return _SelectedItem
                End Get
                Set(ByVal value As Object)
                    Dim old = SelectedItem
                    _SelectedItem = value
                    If (old Is Nothing Xor value Is Nothing) OrElse (old IsNot Nothing AndAlso Not old.Equals(value)) Then
                        Dim e As New IReportsChange.ValueChangedEventArgs(Of Object)(old, value, "SelectedItem")
                        OnSelectedItemChanged(e)
                    End If
                End Set
            End Property
            ''' <summary>Indicates 0-based index of item diaplyed in combo box</summary>
            ''' <seealso cref="SelectedItem"/>
            <DefaultValue(-1I)> _
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Data)> _
            <LDescription(GetType(ResourcesT.Components), "SelectedIndex_d")> _
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
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Action)> _
            <LDescription(GetType(ResourcesT.Components), "SelectedItemChanged_d")> _
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
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Action)> _
            <LDescription(GetType(ResourcesT.Components), "SelectedIndexChanged_d")> _
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
        <DefaultEvent("CheckedChanged")> _
        Public Class MessageBoxRadioButton : Inherits MessageBoxControl
            ''' <summary>Contains value of the <see cref="Checked"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Checked As Boolean
            ''' <summary>Gets or sets value indicating if control is checked or not</summary>
            <DefaultValue(False)> _
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
            <LDescription(GetType(ResourcesT.Components), "Checked_d")> _
            Public Property Checked() As Boolean
                <DebuggerStepThrough()> Get
                    Return _Checked
                End Get
                Set(ByVal value As Boolean)
                    Dim old = Checked
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
            <KnownCategory(KnownCategoryAttribute.KnownCategories.Action)> _
            <LDescription(GetType(ResourcesT.Components), "CheckedChanged_d")> _
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
#Region "Options"
        ''' <summary>Options for <see cref="MessageBox"/></summary>
        ''' <remarks>Values of this enumeration can be combined as long as they fall to different groups. There are three groups of values -
        ''' Align (<see cref="MessageBoxOptions.AlignCenter"/>,<see cref="MessageBoxOptions.AlignJustify"/>, <see cref="MessageBoxOptions.AlignLeft"/>, <see cref="MessageBoxOptions.AlignRight"/>),
        ''' Text flow (<see cref="MessageBoxOptions.Ltr"/>, <see cref="MessageBoxOptions.Rtl"/>) and
        ''' Focus (<see cref="MessageBoxOptions.BringToFront"/>).</remarks>
        <Flags()> _
        <Editor(GetType(DropDownControlEditor(Of MessageBoxOptions, MessageBoxOptionsEditor)), GetType(UITypeEditor))> _
        <TypeConverter(GetType(MessageBoxOptionsConverter))> _
        Public Enum MessageBoxOptions As Byte
            ''' <summary>Text is aligned left (default). In rtl reading aligns text to right.</summary>
            AlignLeft = 0 '0000
            ''' <summary>Text is aligned right. In rtl reading aligns text to left.</summary>
            AlignRight = 1 '0001
            ''' <summary>Text is aligned center</summary>
            AlignCenter = 2 '0010                                                                         
            ''' <summary>Text is aligned to block. If target technology does not support <see cref="MessageBoxOptions.AlignJustify"/> treats it as <see cref="MessageBoxOptions.AlignLeft"/>.</summary>
            AlignJustify = 3 '0011
            ''' <summary>Bitwise mask for AND-ing text alignment</summary>
            ''' <remarks>This is actually not walue of enumeration.</remarks>
            <EditorBrowsable(EditorBrowsableState.Advanced)> AlignMask = 3 '0011
            ''' <summary>Left-to-right reading (default)</summary>
            Ltr = 0 '0000
            ''' <summary>Right-to-left reading</summary>
            Rtl = 4 '0100
            ''' <summary>Force shows message box to the user even if application is not currently active</summary>
            BringToFront = 8 '1000
        End Enum
        Private Class MessageBoxOptionsConverter
            Inherits TypeConverter(Of MessageBoxOptions, String)

            ''' <summary>Performs conversion from type <see cref="String"/> to type <see cref="MessageBoxOptions"/></summary>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
            ''' <param name="value">Value to be converted to type <see cref="MessageBoxOptions"/></param>
            ''' <returns>Value of type <see cref="MessageBoxOptions"/> initialized by <paramref name="value"/></returns>
            Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As String) As MessageBoxOptions
                Return FlagsFromString(Of MessageBoxOptions)(value)
            End Function

            ''' <summary>Performs conversion from type <see cref="MessageBoxOptions"/> to type <see cref="String"/></summary>
            ''' <param name="context"> An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is assumed.</param>
            ''' <param name="value">Value to be converted</param>
            ''' <returns>Representation of <paramref name="value"/> in type <see cref="String"/></returns>
            Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As MessageBoxOptions) As String
                Dim ret As String = ""
                Select Case value And MessageBoxOptions.AlignMask
                    Case MessageBoxOptions.AlignCenter : ret = "AlignCenter"
                    Case MessageBoxOptions.AlignJustify : ret = "AlignJustify"
                    Case MessageBoxOptions.AlignLeft : ret = "AlignLeft"
                    Case MessageBoxOptions.AlignRight : ret = "AlignRight"
                End Select
                ret &= Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator
                If (value And MessageBoxOptions.Rtl) = MessageBoxOptions.Ltr Then
                    ret &= "Ltr"
                Else
                    ret &= "Rtl"
                End If
                If (value And MessageBoxOptions.BringToFront) = MessageBoxOptions.BringToFront Then _
                    ret &= Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator & "BringToFront"
                Return ret
            End Function
        End Class
#End Region
#Region "Action"
        ''' <summary>Shows modal dialog (and waits until the dialog is closed)</summary>
        ''' <returns>Dialog result (<see cref="MessageBoxButton.Result"/> of clicked button)</returns>
        ''' <exception cref="InvalidOperationException"><see cref="State"/> is <see cref="States.Shown"/></exception>
        Public Function ShowDialog() As DialogResult
            Return Me.ShowDialog(Nothing)
        End Function
        ''' <summary>Show modal dialog (and waits until the dialog is closed)</summary>
        ''' <param name="Owner">Parent window of dialog (may be null)</param>
        ''' <returns>Dialog result (<see cref="MessageBoxButton.Result"/> of clicked button)</returns>
        ''' <exception cref="InvalidOperationException"><see cref="State"/> is <see cref="States.Shown"/></exception>
        Public Function ShowDialog(ByVal Owner As IWin32Window) As DialogResult
            PrePerformDialog(True, Owner)
            Return Me.DialogResult
        End Function
        ''' <summary>Displays the dialog non-modally (execution continues immediatelly)</summary>
        ''' <exception cref="InvalidOperationException"><see cref="State"/> is <see cref="States.Shown"/></exception>
        Public Sub DisplayBox()
            Me.DisplayBox(Nothing)
        End Sub
        ''' <summary>Displays the dialog non-modally (execution continues immediatelly)</summary>
        ''' <param name="Owner">Parent window of dialog (may be null)</param>
        ''' <exception cref="InvalidOperationException"><see cref="State"/> is <see cref="States.Shown"/></exception>
        Public Sub DisplayBox(ByVal Owner As IWin32Window)
            PrePerformDialog(False, Owner)
        End Sub
        ''' <summary>If overriden in derived class shows the dialog</summary>
        ''' <param name="Modal">Indicates if dialog should be shown modally (true) or modells (false)</param>
        ''' <param name="Owner">Parent window of dialog (may be null)</param>
        ''' <exception cref="InvalidOperationException"><see cref="State"/> is not <see cref="States.Created"/>. Overriding method shall check this condition and thrown an exception if condition is vialoted.</exception>
        Protected MustOverride Sub PerformDialog(ByVal Modal As Boolean, ByVal Owner As IWin32Window)
        ''' <summary>Calls <see cref="Recycle"/> if necessary, then calls <see cref="PerformDialog"/></summary>
        ''' <param name="Modal">Indicates if dialog should be shown modally (true) or modells (false)</param>
        ''' <param name="Owner">Parent window of dialog (may be null)</param>
        ''' <exception cref="InvalidOperationException"><see cref="State"/> is <see cref="States.Shown"/></exception>
        Private Sub PrePerformDialog(ByVal Modal As Boolean, ByVal Owner As IWin32Window)
            If State <> States.Created Then Recycle()
            PerformDialog(Modal, Owner)
        End Sub

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
        Protected Friend Overridable Sub OnClosed(ByVal e As EventArgs)
            CountDownTimer.Enabled = False
            State = States.Closed
            RaiseEvent Closed(Me, e)
        End Sub
        ''' <summary>Called when dialog is shown. Performs some initialization (timer). Calls <see cref="PlaySound"/>. Raises the <see cref="Shown"/> event.</summary>
        ''' <remarks>Derived class should call this method after dialog is shown.</remarks>
        Protected Friend Overridable Sub OnShown()
            PlaySound()
            If Me.Timer > TimeSpan.Zero Then
                Me.CurrentTimer = Me.Timer
                Me.CountDownTimer.Enabled = True
                OnCountDown(New EventArgs)
            End If
            State = States.Shown
            RaiseEvent Shown(Me, New EventArgs)
        End Sub
        ''' <summary>If <see cref="PlayOnShow"/> is not null, plays it</summary>
        Protected Overridable Sub PlaySound()
            If Me.PlayOnShow IsNot Nothing Then
                Try
                    Me.PlayOnShow.PlayOnBackground()
                Catch : End Try
            End If
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
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
        Public Property State() As States
            <DebuggerStepThrough()> Get
                Return _State
            End Get
            <DebuggerStepThrough()> Private Set(ByVal value As States)
                _State = value
            End Set
        End Property
#Region "Result"
        ''' <summary>Gets result of dialog (<see cref="MessageBoxButton.Result"/> of button user has clicked on)</summary>
        ''' <returns><see cref="MessageBoxButton.Result"/> of button user have clicked to or <see cref="CloseResponse"/> when message box was closed by pressing escape, closing the window or timer.</returns>
        ''' <value>Should be set by derived class when dialog is closed</value>
        ''' <remarks>Value of this property is valid only when <see cref="State"/> is <see cref="States.Closed"/></remarks>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property DialogResult() As DialogResult
            Get
                Return _DialogResult
            End Get
            Protected Friend Set(ByVal value As DialogResult)
                _DialogResult = value
            End Set
        End Property
        ''' <summary>Contains value of the <see cref="DialogResult"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _DialogResult As DialogResult = Windows.Forms.DialogResult.None
        ''' <summary>Gets button user have clicked on</summary>
        ''' <returns>Button user have clicked on (or null if dialog was closed by window close button, pressing escape or timer)</returns>
        ''' <remarks>Value of this property is valid only when <see cref="State"/> is <see cref="States.Closed"/></remarks>
        ''' <value>Should be set by derived class when dialog is closed</value>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overridable Property ClickedButton() As MessageBoxButton
            Get
                Return _ClickedButton
            End Get
            Protected Friend Set(ByVal value As MessageBoxButton)
                _ClickedButton = value
            End Set
        End Property
        ''' <summary>Contaisn value of the <see cref="ClickedButton"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ClickedButton As MessageBox.MessageBoxButton
        ''' <summary>Contains value of the <see cref="ClosedByTimer"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _ClosedByTimer As Boolean
        ''' <summary>Gets value indicationg if the message box was closed automatically after the time specified in <see cref="Timer"/> elapsed</summary>
        ''' <returns>True if the message box was closed due to time elapsed, false otherwise</returns>
        <Browsable(False)> _
        Public ReadOnly Property ClosedByTimer() As Boolean
            <DebuggerStepThrough()> Get
                Return _ClosedByTimer
            End Get
        End Property
#End Region
#End Region
#Region "Timer"
        Private Sub CountDownTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles CountDownTimer.Tick
            OnCountDown(New EventArgs)
            If Not Me.IsCountDown Then Exit Sub
            CurrentTimer -= TimeSpan.FromSeconds(1)
            If CurrentTimer <= TimeSpan.Zero Then
                Select Case Me.TimeButton
                    Case 0 To Me.Buttons.Count - 1
                        Me.Close(Me.Buttons(Me.TimeButton).Result)
                    Case -1
                        Me.Close(Me.CloseResponse)
                    Case Else : Me.Close()
                End Select
                CountDownTimer.Enabled = False
                _ClosedByTimer = True
            End If
        End Sub
        ''' <summary>Gets value indicationg if counting down is curently in progress</summary>
        ''' <remarks>In order to cahnge value of this prioperty use <see cref="ResumeCountDown"/> and <see cref="StopCountDown"/> methods</remarks>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
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
        <EditorBrowsable(EditorBrowsableState.Never)> Private _CurrentTimer As TimeSpanFormattable
        ''' <summary>Gets or sets current remaining time of count-down timer</summary>
        ''' <remarks>If value id <see cref="TimeSpan.Zero"/> or less, count down ends and dialog is about to be closed</remarks>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property CurrentTimer() As TimeSpanFormattable
            <DebuggerStepThrough()> Get
                Return _CurrentTimer
            End Get
            <DebuggerStepThrough()> Protected Set(ByVal value As TimeSpanFormattable)
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
            If Me.CurrentTimer <= TimeSpan.Zero Then Throw New InvalidOperationException(ResourcesT.Exceptions.CannotResumeCountDownTimerWhenThereIsNoTimeLeft)
            CountDownTimer.Enabled = True
            OnCountDown(New EventArgs)
        End Sub
        ''' <summary>Resumes previouskly stopped count down timer with new timer value</summary>
        ''' <param name="TimeLeft">Count down timer time (after which the message box is closed)</param>
        Public Sub ResumeCountDown(ByVal TimeLeft As TimeSpan)
            If TimeLeft <= TimeSpan.Zero Then Throw New ArgumentOutOfRangeException("TimeLeft", ResourcesT.Exceptions.CountDownTimeMustBeGreaterThanZero)
            Me.CurrentTimer = TimeLeft
            ResumeCountDown()
        End Sub
#End Region
#Region "Events"
        ''' <summary>Raised when count-down timer ticks</summary>
        ''' <remarks>Count down timer ticks each second once. First the event is raised immediatelly after the dialog is shown or count-down is resumed</remarks>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Action)> _
        <LDescription(GetType(ResourcesT.Components), "CountDown_d")> _
        Public Event CountDown As EventHandler(Of IndependentT.MessageBox, EventArgs)
        ''' <summary>Raised after dialog is shown</summary>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Action)> _
        <LDescription(GetType(ResourcesT.Components), "Shown_d")> _
        Public Event Shown As EventHandler(Of IndependentT.MessageBox, EventArgs)
        ''' <summary>Raised after dialog is closed</summary>
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Action)> _
        <LDescription(GetType(ResourcesT.Components), "Closed_d")> _
        Public Event Closed As EventHandler(Of IndependentT.MessageBox, EventArgs)
#End Region
#Region "Recycle"
        ''' <summary>Switches <see cref="MessageBox"/> from <see cref="States.Closed"/> to <see cref="States.Created"/> <see cref="State"/></summary>
        ''' <remarks>This method cannot be overriden. Override <see cref="RecycleInternal"/> instead which is called only when necessary.
        ''' <para>Calling this method has no effect when <see  cref="State"/> is <see cref="States.Created"/> and causes <see cref="InvalidOperationException"/> when <see cref="State"/> is <see cref="States.Shown"/>.</para>
        ''' <para><see cref="Show"/> and <see cref="DisplayTemplate"/> instance methods call <see cref="Recycle"/> if necessary.</para>
        ''' <para>When re-cycling message boxex, you should keep in mind that youre can change state of it (check boxes, radio buttons, combo boxes, custom controls)</para></remarks>
        ''' <exception cref="InvalidOperationException"><see cref="State"/> is <see cref="States.Shown"/> or <see cref="State"/> is not member of <see cref="States"/></exception>
        Public Sub Recycle()
            Select Case Me.State
                Case States.Created 'Do nothing
                Case States.Shown : Throw New InvalidOperationException(ResourcesT.Exceptions.MessageBoxCannotBeReCycledWhenItIsShown)
                Case Else : RecycleInternal() : OnRecycled(New EventArgs)
            End Select
        End Sub
        ''' <summary>Raised when instance recycling process is completed</summary>
        ''' <seealso cref="Recycle"/>
        Public Event Recycled As EventHandler(Of MessageBox, EventArgs)
        ''' <summary>Raises the <see cref="Recycled"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks>Called by <see cref="Recycle"/> after call of <see cref="RecycleInternal"/></remarks>
        Protected Overridable Sub OnRecycled(ByVal e As EventArgs)
            RaiseEvent Recycled(Me, e)
        End Sub
        ''' <summary>Performs all operations needed to switch <see cref="MessageBox"/> form <see cref="State"/> <see cref="States.Closed"/> to <see cref="States.Created"/></summary>
        ''' <remarks>Called by <see cref="Recycle"/>.
        ''' <para>Note to inheritors: Always call base-class method <see cref="RecycleInternal"/>.</para></remarks>
        ''' <exception cref="InvalidOperationException"><see cref="State"/> is not <see cref="States.Closed"/>. This exception never occures in this implementation because <see cref="Recycle"/> ensures that <see cref="RecycleInternal"/> is caled only when <see cref="State"/> is <see cref="States.Closed"/>.</exception>
        Protected Overridable Sub RecycleInternal()
            If Me.State <> States.Closed Then Throw New InvalidOperationException(ResourcesT.Exceptions.RecycleInternalCanBeCalledOnlyOnClosedMessagebox)
            Me.DialogResult = Windows.Forms.DialogResult.None
            Me.ClickedButton = Nothing
            State = States.Created
            _ClosedByTimer = False
        End Sub
#End Region
#Region "Internal handlers"
        Private Sub Buttons_Adding(ByVal sender As ListWithEvents(Of MessageBoxButton), ByVal e As ListWithEvents(Of MessageBoxButton).CancelableItemIndexEventArgs)
            If e.Item Is Nothing Then e.Cancel = True
        End Sub
        Private Sub Radios_Adding(ByVal sender As ListWithEvents(Of MessageBoxRadioButton), ByVal e As ListWithEvents(Of MessageBoxRadioButton).CancelableItemIndexEventArgs)
            If e.Item Is Nothing Then e.Cancel = True
        End Sub
        Private Sub CheckBoxes_Adding(ByVal sender As ListWithEvents(Of MessageBoxCheckBox), ByVal e As ListWithEvents(Of MessageBoxCheckBox).CancelableItemIndexEventArgs)
            If e.Item Is Nothing Then e.Cancel = True
        End Sub
        Private Sub Buttons_ItemChanging(ByVal sender As ListWithEvents(Of MessageBoxButton), ByVal e As ListWithEvents(Of MessageBoxButton).CancelableItemIndexEventArgs)
            If e.Item Is Nothing Then e.Cancel = True
        End Sub
        Private Sub Radios_ItemChanging(ByVal sender As ListWithEvents(Of MessageBoxRadioButton), ByVal e As ListWithEvents(Of MessageBoxRadioButton).CancelableItemIndexEventArgs)
            If e.Item Is Nothing Then e.Cancel = True
        End Sub
        Private Sub CheckBoxes_ItemChanging(ByVal sender As ListWithEvents(Of MessageBoxCheckBox), ByVal e As ListWithEvents(Of MessageBoxCheckBox).CancelableItemIndexEventArgs)
            If e.Item Is Nothing Then e.Cancel = True
        End Sub
#End Region
#Region "Shared show"
#Region "Helpers"
        ''' <summary>Gets instance of default implementation of message box</summary>
        ''' <returns>Instance of type which specified by the <see cref="DefaultImplementation"/> property</returns>
        ''' <exception cref="System.ArgumentException"><see cref="DefaultImplementation"/> is not <see cref="T:System.RuntimeType"/></exception>
        ''' <exception cref="System.NotSupportedException"><see cref="DefaultImplementation"/> cannot be a <see cref="System.Reflection.Emit.TypeBuilder" /> .-or- Creation of <see cref="System.TypedReference" />, <see cref="System.ArgIterator" />, <see cref="System.Void" />, and <see cref="System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.</exception>
        ''' <exception cref="System.Reflection.TargetInvocationException">The constructor being called throws an exception.</exception>
        ''' <exception cref="System.MethodAccessException">The caller does not have permission to call default constructor of type which is specified in <see cref="DefaultImplementation"/>.</exception>
        ''' <exception cref="System.MemberAccessException">Cannot create an instance of an abstract class, or member was invoked with a late-binding mechanism.</exception>
        ''' <exception cref="System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through Overload:<see cref="System.Type.GetTypeFromProgID" /> or Overload:<see cref="System.Type.GetTypeFromCLSID" />.</exception>
        ''' <exception cref="System.MissingMethodException">No matching public constructor was found.</exception>
        ''' <exception cref="System.Runtime.InteropServices.COMException"><see cref="DefaultImplementation"/> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered.</exception>
        ''' <exception cref="System.TypeLoadException"><see cref="DefaultImplementation"/> is not a valid type.</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function GetDefault() As MessageBox
            Return Activator.CreateInstance(DefaultImplementation)
        End Function
        ''' <summary>Does not implement <see cref="MessageBox"/>. Used for initializing messageboxes using <see cref="InitializeFrom"/>.</summary>
        ''' <remarks>This class cannot be used as message box directly. But you can create instance of it and use it as template for another message boxes.</remarks>
        ''' <seelaso cref="ShowTemplate"/>
        ''' <seelaso cref="DisplayTemplate"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public NotInheritable Class FakeBox : Inherits MessageBox
            ''' <summary>Gets text which contains Accesskey marker (like &amp; in WinForms or _ in WPF)</summary>
            ''' <param name="Text">Text (if it contains character used as accesskey markers they must be escaped)</param>
            ''' <param name="AccessKey">Char representing accesskey (if char is not in <paramref name="Text"/> no accesskey marker should be inserted)</param>
            ''' <returns><paramref name="Text"/></returns>
            Protected Overrides Function GetTextWithAccessKey(ByVal Text As String, ByVal AccessKey As Char) As String
                Return Text
            End Function
            ''' <summary>If overriden in derived class closes the message box with given response</summary>
            ''' <param name="Response">Response returned by the <see cref="Show"/> function</param>
            ''' <exception cref="NotImplementedException">Always</exception>
            Public Overloads Overrides Sub Close(ByVal Response As System.Windows.Forms.DialogResult)
                Throw New NotImplementedException(ResourcesT.Exceptions.ClassCannotBeUsedAsMessageBox)
            End Sub
            ''' <summary>If overriden in derived class shows the dialog</summary>
            ''' <param name="Modal">Indicates if dialog should be shown modally (true) or modells (false)</param>
            ''' <param name="Owner">Parent window of dialog (may be null)</param>
            ''' <exception cref="InvalidOperationException"><see cref="State"/> is not <see cref="States.Created"/>. Overriding method shall check this condition and thrown an exception if condition is vialoted.</exception>
            ''' <exception cref="NotImplementedException">Always</exception>
            Protected Overrides Sub PerformDialog(ByVal Modal As Boolean, ByVal Owner As System.Windows.Forms.IWin32Window)
                Throw New NotImplementedException(ResourcesT.Exceptions.ClassCannotBeUsedAsMessageBox)
            End Sub
            ''' <summary>Default CTor</summary>
            Public Sub New()
            End Sub
            ''' <summary>CTor from buttons specified as OR-ed mask of <see cref="MessageBoxButton.Buttons"/> values</summary>
            ''' <param name="Buttons">OR-ed values from <see cref="MessageBoxButton.Buttons"/></param>
            ''' <seelaso cref="MessageBoxButton.GetButtons"/>
            Public Sub New(ByVal Buttons As MessageBoxButton.Buttons)
                Me.New()
                Me.Buttons.Clear()
                Me.Buttons.AddRange(MessageBoxButton.GetButtons(Buttons))
            End Sub
        End Class
        ''' <summary>Initializes instance of <see cref="MessageBox"/> obtained via <see cref="GetDefault"/> using <see cref="InitializeFrom"/></summary>
        ''' <param name="Other">Instance properties of which will be used to initialize returned instance</param>
        ''' <returns>Initialized instance of default implementation</returns>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Shared Function InitializeDafault(ByVal Other As MessageBox) As MessageBox
            Dim Inst As MessageBox
            Try
                Inst = GetDefault()
            Catch ex As Exception
                Throw New TargetInvocationException(ResourcesT.Exceptions.ThereWasAnErrorObtaininInstanceOfDefaultImplementationOfMessageBoxSeeInnerExceptionForDetails, ex)
            End Try
            Inst.InitializeFrom(Other)
            Return Inst
        End Function
        ''' <summary>Initializes current instance of <see cref="MessageBox"/> with setting of another <see cref="MessageBox"/></summary>
        ''' <param name="Other"><see cref="MessageBox"/> to initialize this instance with</param>
        ''' <remarks>Do not use this method for vloning message boxes. This method is mainly intended for internal use. The <paramref name="Other"/> <see cref="MessageBox"/> should be only used for initializing this instance and should be never shown.
        ''' This is because values of properties are simply copied form <paramref name="Other"/> to this instance ant thus both instances then shares same buttons and other controls which can cause instability when both instances are shown.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Other"/> is null</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Sub InitializeFrom(ByVal Other As MessageBox)
            If Other Is Nothing Then Throw New ArgumentNullException("Other")
            Me.AllowClose = Other.AllowClose
            Me.BottomControl = Other.BottomControl
            Me.Buttons.Clear()
            Me.Buttons.AddRange(Other.Buttons)
            Me.CloseResponse = Other.CloseResponse
            Me.ComboBox = Other.ComboBox
            Me.DefaultButton = Other.DefaultButton
            Me.CheckBoxes.Clear()
            Me.CheckBoxes.AddRange(Other.CheckBoxes)
            Me.Icon = Other.Icon
            Me.MidControl = Other.MidControl
            Me.Options = Other.Options
            Me.Prompt = Other.Prompt
            Me.Radios.Clear()
            Me.Radios.AddRange(Other.Radios)
            Me.TimeButton = Other.TimeButton
            Me.Timer = Other.Timer
            Me.Title = Other.Title
            Me.TopControl = Other.TopControl
            Me.PlayOnShow = Other.PlayOnShow
        End Sub
        ''' <summary>Shows given modal message box initialized with given instance of <see cref="MessageBox"/></summary>
        ''' <param name="Instance">Instance to be show</param>
        ''' <param name="InitializeFrom">Instance to initialize <paramref name="Instance"/> with</param>
        ''' <param name="Owner">Owner window (can be null)</param>
        ''' <param name="Prompt">If not null sets dfferent prompt then <paramref name="InitializeFrom"/></param>
        ''' <param name="Title">Is not null sets diffetent title then <paramref name="InitializeFrom"/></param>
        ''' <returns>Message box result</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Instance"/> or <paramref name="InitializeFrom"/> is null</exception>
        ''' <remarks>For same reason as <see cref="InitializeFrom"/>, do not use <paramref name="InitializeFrom"/> to clonning live message boxes</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Shared Function ShowTemplate(ByVal Instance As MessageBox, ByVal InitializeFrom As MessageBox, Optional ByVal Owner As IWin32Window = Nothing, Optional ByVal Prompt$ = Nothing, Optional ByVal Title$ = Nothing) As DialogResult
            If Instance Is Nothing Then Throw New ArgumentNullException("Instance")
            If InitializeFrom Is Nothing Then Throw New ArgumentNullException("InitializeFrom")
            Instance.InitializeFrom(InitializeFrom)
            If Prompt IsNot Nothing Then Instance.Prompt = Prompt
            If Title IsNot Nothing Then Instance.Title = Title
            Return Instance.ShowDialog(Owner)
        End Function
        ''' <summary>Display given message box initialized with given instance of <see cref="MessageBox"/> modeless</summary>
        ''' <param name="Instance">Instance to be show</param>
        ''' <param name="InitializeFrom">Instance to initialize <paramref name="Instance"/> with</param>
        ''' <param name="Owner">Owner window (can be null)</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Instance"/> or <paramref name="InitializeFrom"/> is null</exception>
        ''' <remarks>For same reason as <see cref="InitializeFrom"/>, do not use <paramref name="InitializeFrom"/> to clonning live message boxes</remarks>
        ''' <param name="Prompt">If not null sets dfferent prompt then <paramref name="InitializeFrom"/></param>
        ''' <param name="Title">Is not null sets diffetent title then <paramref name="InitializeFrom"/></param>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Shared Function DisplayTemplate(ByVal Instance As MessageBox, ByVal InitializeFrom As MessageBox, Optional ByVal Owner As IWin32Window = Nothing, Optional ByVal Prompt$ = Nothing, Optional ByVal Title$ = Nothing) As MessageBox
            If Instance Is Nothing Then Throw New ArgumentNullException("Instance")
            If InitializeFrom Is Nothing Then Throw New ArgumentNullException("InitializeFrom")
            Instance.InitializeFrom(InitializeFrom)
            If Prompt IsNot Nothing Then Instance.Prompt = Prompt
            If Title IsNot Nothing Then Instance.Title = Title
            Instance.DisplayBox(Owner)
            Return Instance
        End Function
        ''' <summary>Shows default (<see cref="GetDefault"/>) modal message box initialized with given instance of <see cref="MessageBox"/></summary>
        ''' <param name="InitializeFrom">Instance to initialize default message box with</param>
        ''' <param name="Owner">Owner window (can be null)</param>
        ''' <returns>Message box result</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="InitializeFrom"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Ther was an error obtainin default implementation instance via <see cref="GetDefault"/>. See <see cref="Exception.InnerException"/> for details.</exception>
        ''' <remarks>For same reason as <see cref="InitializeFrom"/>, do not use <paramref name="InitializeFrom"/> to clonning live message boxes</remarks>
        ''' <param name="Prompt">If not null sets dfferent prompt then <paramref name="InitializeFrom"/></param>
        ''' <param name="Title">Is not null sets diffetent title then <paramref name="InitializeFrom"/></param>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function ShowTemplate(ByVal InitializeFrom As MessageBox, Optional ByVal Owner As IWin32Window = Nothing, Optional ByVal Prompt$ = Nothing, Optional ByVal Title$ = Nothing) As DialogResult
            Dim lGetDefault As Tools.WindowsT.IndependentT.MessageBox
            Try
                lGetDefault = GetDefault()
            Catch ex As Exception
                Throw New TargetInvocationException(ResourcesT.Exceptions.ThereWasAnErrorObtaininInstanceOfDefaultImplementationOfMessageBoxSeeInnerExceptionForDetails, ex)
            End Try
            Return ShowTemplate(lGetDefault, InitializeFrom, Owner, Prompt, Title)
        End Function
        ''' <summary>Display default (<see cref="GetDefault"/>) message box initialized with given instance of <see cref="MessageBox"/> modeless</summary>
        ''' <param name="InitializeFrom">Instance to initialize default message box with</param>
        ''' <param name="Owner">Owner window (can be null)</param>
        ''' <exception cref="ArgumentNullException"><paramref name="InitializeFrom"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Ther was an error obtainin default implementation instance via <see cref="GetDefault"/>. See <see cref="Exception.InnerException"/> for details.</exception>
        ''' <remarks>For same reason as <see cref="InitializeFrom"/>, do not use <paramref name="InitializeFrom"/> to clonning live message boxes</remarks>
        ''' <param name="Prompt">If not null sets dfferent prompt then <paramref name="InitializeFrom"/></param>
        ''' <param name="Title">Is not null sets diffetent title then <paramref name="InitializeFrom"/></param>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function DisplayTemplate(ByVal InitializeFrom As MessageBox, Optional ByVal Owner As IWin32Window = Nothing, Optional ByVal Prompt$ = Nothing, Optional ByVal Title$ = Nothing) As MessageBox
            Dim lGetDefault As Tools.WindowsT.IndependentT.MessageBox
            Try
                lGetDefault = GetDefault()
            Catch ex As Exception
                Throw New TargetInvocationException(ResourcesT.Exceptions.ThereWasAnErrorObtaininInstanceOfDefaultImplementationOfMessageBoxSeeInnerExceptionForDetails, ex)
            End Try
            Return DisplayTemplate(lGetDefault, InitializeFrom, Owner, Prompt, Title)
        End Function
        ''' <summary>Default function used for converting enumeration values to icons for message box</summary>
        ''' <param name="code">Code of icon to be obtained</param>
        ''' <returns>Appropriate icon to code or null if no icon is associated with code</returns>
        ''' <remarks>You can change which function <see cref="MessageBox"/> globaly uses for obtaining icons by setting the <see cref="GetIconDelegate"/> static property</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function GetIcon(ByVal code As MessageBoxIcons) As IconOrBitmap
            Select Case code
                Case MessageBoxIcons.Asterisk : Return Icons.Asterisk
                Case MessageBoxIcons.Error : Return Icons.ErrorIcon
                Case MessageBoxIcons.Exclamation : Return Icons.Exclamation
                Case MessageBoxIcons.Hand : Return Icons.Hand
                Case MessageBoxIcons.Information : Return Icons.Information
                Case MessageBoxIcons.OK : Return Icons.OK
                Case MessageBoxIcons.Question : Return Icons.Question
                Case MessageBoxIcons.SecurityError : Return Icons.SecurityError
                Case MessageBoxIcons.SecurityInformation : Return Icons.SecurityInformation
                Case MessageBoxIcons.SecurityOK : Return Icons.SecurityOK
                Case MessageBoxIcons.SecurityQuestion : Return Icons.SecurityQuestion
                Case MessageBoxIcons.SecurityWarning : Return Icons.SecurityWarning
                Case MessageBoxIcons.Stop : Return Icons.StopIcon
                Case MessageBoxIcons.Warning : Return Icons.Warning
                Case Else : Return Nothing
            End Select
        End Function
        ''' <summary>Converts <see cref="Windows.Forms.Messageboxicon"/> to <see cref="MessageBoxIcons"/> value</summary>
        ''' <param name="code">A <see cref="Windows.Forms.MessageBoxIcon"/></param>
        ''' <returns>Appropriate <see cref="MessageBoxIcons"/> value. If <paramref name="code"/> is not member of <see cref="Windows.Forms.MessageBoxIcon"/> returns <see cref="MessageBoxIcons.None"/></returns>
        ''' <remarks>Several <see cref="Windows.Forms.MessageBoxIcon"/> values are converted to the same <see cref="MessageBoxIcons"/> value because they have same numerical values and it is not possible to distinguish between them. You'd better using <see cref="MessageBoxIcons"/> directly</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function ConvertIconConstant(ByVal code As Windows.Forms.MessageBoxIcon) As MessageBoxIcons
            Select Case code
                Case MessageBoxIcon.Information : Return MessageBoxIcons.Information
                Case MessageBoxIcon.Asterisk : Return MessageBoxIcons.Asterisk
                Case MessageBoxIcon.Error : Return MessageBoxIcons.Error
                Case MessageBoxIcon.Hand : Return MessageBoxIcons.Hand
                Case MessageBoxIcon.Stop : Return MessageBoxIcons.Stop
                Case MessageBoxIcon.Exclamation : Return MessageBoxIcons.Exclamation
                Case MessageBoxIcon.Warning : Return MessageBoxIcons.Warning
                Case MessageBoxIcon.Question : Return MessageBoxIcons.Question
                Case Else : Return MessageBoxIcons.None
            End Select
        End Function
        ''' <summary>Converts <see cref="MsgBoxStyle"/> to <see cref="MessageBoxIcons"/> value</summary>
        ''' <param name="code">A <see cref="MsgBoxStyle"/></param>
        ''' <returns>Appropriate <see cref="MessageBoxIcons"/> value. If <paramref name="code"/> is not member of <see cref="Windows.Forms.MessageBoxIcon"/> returns <see cref="MessageBoxIcons.None"/></returns>
        ''' <remarks>Only bits masked with 0x70 mask are considered for conversion</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function ConvertIconConstant(ByVal code As MsgBoxStyle) As MessageBoxIcons
            Select Case code And (MsgBoxStyle.Critical Or MsgBoxStyle.Exclamation Or MsgBoxStyle.Information Or MsgBoxStyle.Question)
                Case MsgBoxStyle.Critical : Return MessageBoxIcons.Error
                Case MsgBoxStyle.Exclamation : Return MessageBoxIcons.Exclamation
                Case MsgBoxStyle.Information : Return MessageBoxIcons.Information
                Case MsgBoxStyle.Question : Return MessageBoxIcons.Question
                Case Else : Return MessageBoxIcons.None
            End Select
        End Function
        ''' <summary>Converts <see cref="Windows.MessageBoxImage"/> to <see cref="MessageBoxIcons"/> value</summary>
        ''' <param name="code">A <see cref="Windows.MessageBoxImage"/></param>
        ''' <returns>Appropriate <see cref="MessageBoxIcons"/> value. If <paramref name="code"/> is not member of <see cref="Windows.Forms.MessageBoxIcon"/> returns <see cref="MessageBoxIcons.None"/></returns>
        ''' <remarks>Several <see cref="Windows.MessageBoxImage"/> values are converted to the same <see cref="MessageBoxIcons"/> value because they have same numerical values and it is not possible to distinguish between them. You'd better using <see cref="MessageBoxIcons"/> directly</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
         Public Shared Function ConvertIconConstant(ByVal code As Windows.MessageBoxImage) As MessageBoxIcons
            Select Case code
                Case Windows.MessageBoxImage.Information : Return MessageBoxIcons.Information
                Case Windows.MessageBoxImage.Asterisk : Return MessageBoxIcons.Asterisk
                Case Windows.MessageBoxImage.Error : Return MessageBoxIcons.Error
                Case Windows.MessageBoxImage.Hand : Return MessageBoxIcons.Hand
                Case Windows.MessageBoxImage.Stop : Return MessageBoxIcons.Stop
                Case Windows.MessageBoxImage.Exclamation : Return MessageBoxIcons.Exclamation
                Case Windows.MessageBoxImage.Warning : Return MessageBoxIcons.Warning
                Case Windows.MessageBoxImage.Question : Return MessageBoxIcons.Question
                Case Else : Return MessageBoxIcons.None
            End Select
        End Function
        ''' <summary>Contains value of the <see cref="GetIconDelegate"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private Shared _GetIconDelegate As Func(Of MessageBoxIcons, IconOrBitmap) = AddressOf GetIcon
        ''' <summary>Gets or sets delegate which is used for converting enumeration values to icons for message box</summary>
        ''' <value>New delegate to be shared by all messageboxes for converting enumeration members to icons</value>
        ''' <returns>Current delegate that converts enumeration values to icons for message box</returns>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        ''' <remarks>Default value is <see cref="GetIcon"/> function</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Property GetIconDelegate() As Func(Of MessageBoxIcons, IconOrBitmap)
            <DebuggerStepThrough()> Get
                Return _GetIconDelegate
            End Get
            Set(ByVal value As Func(Of MessageBoxIcons, IconOrBitmap))
                If value Is Nothing Then Throw New ArgumentNullException("value")
                _GetIconDelegate = value
            End Set
        End Property
        ''' <summary>Enumeration of built-in icons for <see cref="MessageBox"/></summary>
        ''' <remarks>The <see cref="MessageBox"/> API allows you to pass any <see cref="Drawing.Image"/> as your own icon</remarks>
        Public Enum MessageBoxIcons
            ''' <summary>By default represented by a yellow bulb</summary>
            ''' <seealso cref="Windows.Forms.MessageBoxIcon.Asterisk"/>
            ''' <seealso cref="Icons.Asterisk"/>
            Asterisk = 1
            ''' <summary>By default represented by white X in red circle</summary>
            ''' <seealso cref="Windows.Forms.MessageBoxIcon.Error"/>
            ''' <seealso cref="Icons.ErrorIcon"/>
            [Error] = 2
            ''' <summary>By default represented by black exclamation mark (!) in yellow triangle</summary>
            ''' <seealso cref="Windows.Forms.MessageBoxIcon.Exclamation"/>
            ''' <seealso cref="Icons.Exclamation"/>
            Exclamation = 3
            ''' <summary>By default represented by white hand in red circle</summary>
            ''' <seealso cref="Windows.Forms.MessageBoxIcon.Hand"/>
            ''' <seealso cref="Icons.Hand"/>
            Hand = 4
            ''' <summary>By default represented by white lowercase i in blue circle</summary>
            ''' <seealso cref="Windows.Forms.MessageBoxIcon.Information"/>
            ''' <seealso cref="Icons.Information"/>
            Information = 5
            ''' <summary>By default represented by white question mark (?) in blue circle</summary>
            ''' <seealso cref="Windows.Forms.MessageBoxIcon.Question"/>
            ''' <seealso cref="Icons.Question"/>
            Question = 6
            ''' <summary>By default represented by default represented by no-entry (one way) traffic sign</summary>
            ''' <seealso cref="Windows.Forms.MessageBoxIcon.Stop"/>
            ''' <seealso cref="Icons.StopIcon"/>
            [Stop] = 7
            ''' <summary>By default represented by black exclamation mark (!) in yellow circle</summary>
            ''' <seealso cref="Windows.Forms.MessageBoxIcon.Warning"/>
            ''' <seealso cref="Icons.Warning"/>
            Warning = 8
            ''' <summary>By default represented by white check mark (✔) in green circle</summary>
            ''' <seealso cref="Icons.OK"/>
            OK = 9
            ''' <summary>By default represented by white X in red shield</summary>
            ''' <seealso cref="Icons.SecurityError"/>
            SecurityError = 16 Or [Error]
            ''' <summary>By default represented by shield with for fields - red, green, blue and yellow</summary>
            ''' <seealso cref="Icons.SecurityInformation"/>
            SecurityInformation = 16 Or Information
            ''' <summary>By default represented by black exclamation mark (!) in yellow shield</summary>
            ''' <seealso cref="Icons.SecurityWarning"/>
            SecurityWarning = 16 Or Warning
            ''' <summary>By default represented by white check mark (✔) in green shield</summary>
            ''' <seealso cref="Icons.SecurityOK"/>
            SecurityOK = 16 Or OK
            ''' <summary>By default represented by black quastion mark (?) in yellow shield</summary>
            ''' <seealso cref="Icons.SecurityQuestion"/>
            SecurityQuestion = 16 Or Question
            ''' <summary>Represents no icon</summary>
            ''' <seealso cref="Windows.Forms.MessageBoxIcon.None"/>
            None = 0
        End Enum
#End Region

#Region "System.Windows"
#Region "Helpers"
        ''' <summary>The simplies possible implementation of <see cref="IWin32Window"/></summary>
        Private Class WPFWindow : Implements IWin32Window
            ''' <summary>CTor</summary>
            ''' <param name="handle">Handle new instance will point to</param>
            Public Sub New(ByVal handle As IntPtr)
                _handle = handle
            End Sub
            ''' <summary>Contains value of the <see cref="Handle"/> property</summary>
            Private ReadOnly _handle As IntPtr

            ''' <summary>Gets the handle to the window represented by the implementer.</summary>
            ''' <returns>A handle to the window represented by the implementer.</returns>
            Public ReadOnly Property Handle() As System.IntPtr Implements System.Windows.Forms.IWin32Window.Handle
                Get
                    Return _handle
                End Get
            End Property
        End Class
        ''' <summary>Gets value indicating if given value is valid <see cref="System.Windows.MessageBoxButton"/></summary>
        ''' <param name="value">Value to test</param>
        ''' <returns>Ture if <paramref name="value"/> is valid <see cref="System.Windows.MessageBoxButton"/></returns>
        Private Shared Function IsValidMessageBoxButton(ByVal value As System.Windows.MessageBoxButton) As Boolean
            If (((value <> System.Windows.MessageBoxButton.OK) AndAlso (value <> System.Windows.MessageBoxButton.OKCancel)) AndAlso (value <> System.Windows.MessageBoxButton.YesNo)) Then
                Return (value = System.Windows.MessageBoxButton.YesNoCancel)
            End If
            Return True
        End Function

        ''' <summary>Gets value indicating if given value is valid <see cref="System.Windows.MessageBoxImage"/></summary>
        ''' <param name="value">Value to test</param>
        ''' <returns>Ture if <paramref name="value"/> is valid <see cref="System.Windows.MessageBoxImage"/></returns>
        Private Shared Function IsValidMessageBoxImage(ByVal value As System.Windows.MessageBoxImage) As Boolean
            If ((((value <> System.Windows.MessageBoxImage.Asterisk) AndAlso (value <> System.Windows.MessageBoxImage.Hand)) AndAlso ((value <> System.Windows.MessageBoxImage.Exclamation) AndAlso (value <> System.Windows.MessageBoxImage.Hand))) AndAlso (((value <> System.Windows.MessageBoxImage.Asterisk) AndAlso (value <> System.Windows.MessageBoxImage.None)) AndAlso ((value <> System.Windows.MessageBoxImage.Question) AndAlso (value <> System.Windows.MessageBoxImage.Hand)))) Then
                Return (value = System.Windows.MessageBoxImage.Exclamation)
            End If
            Return True
        End Function
        ''' <summary>Gets value indicating if given value is valid <see cref="System.Windows.MessageBoxOptions"/></summary>
        ''' <param name="value">Value to test</param>
        ''' <returns>Ture if <paramref name="value"/> is valid <see cref="System.Windows.MessageBoxOptions"/></returns>
        Private Shared Function IsValidMessageBoxOptions(ByVal value As System.Windows.MessageBoxOptions) As Boolean
            Return ((value And -3801089) = System.Windows.MessageBoxOptions.None)
        End Function
        ''' <summary>Gets value indicating if given value is valid <see cref="System.Windows.MessageBoxResult"/></summary>
        ''' <param name="value">Value to test</param>
        ''' <returns>Ture if <paramref name="value"/> is valid <see cref="System.Windows.MessageBoxResult"/></returns>
        Private Shared Function IsValidMessageBoxResult(ByVal value As System.Windows.MessageBoxResult) As Boolean
            If (((value <> System.Windows.MessageBoxResult.Cancel) AndAlso (value <> System.Windows.MessageBoxResult.No)) AndAlso ((value <> System.Windows.MessageBoxResult.None) AndAlso (value <> System.Windows.MessageBoxResult.OK))) Then
                Return (value = System.Windows.MessageBoxResult.Yes)
            End If
            Return True
        End Function
#End Region
#Region "WPF non-overloadable"
        ''' <summary>Displays a message box that has a message and that returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <remarks>This function is provided for compatibility with <see cref="Windows.MessageBox"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
Public Shared Function ShowWPF(ByVal messageBoxText As String) As Windows.MessageBoxResult
            Return MessageBox.ShowCore(IntPtr.Zero, messageBoxText, String.Empty, Windows.MessageBoxButton.OK, Windows.MessageBoxImage.None, Windows.MessageBoxResult.None, Windows.MessageBoxOptions.None)
        End Function

        ''' <summary>Displays a message box that has a message and title bar caption; and that returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <remarks>This function is provided for compatibility with <see cref="Windows.MessageBox"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shared Function ShowWPF(ByVal messageBoxText As String, ByVal caption As String) As Windows.MessageBoxResult
            Return MessageBox.ShowCore(IntPtr.Zero, messageBoxText, caption, Windows.MessageBoxButton.OK, Windows.MessageBoxImage.None, Windows.MessageBoxResult.None, Windows.MessageBoxOptions.None)
        End Function
#End Region
#Region "Show"
        ''' <summary>Displays a message box in front of the specified window. The message box displays a message and returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="owner">A <see cref="T:System.Windows.Window" /> that represents the owner window of the message box.</param>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.MessageBox.Show"/> function but provides </remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function Show(ByVal owner As System.Windows.Window, ByVal messageBoxText As String) As System.Windows.MessageBoxResult
            Return MessageBox.ShowCore(New System.Windows.Interop.WindowInteropHelper(owner).Handle, messageBoxText, String.Empty, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.None, System.Windows.MessageBoxResult.None, System.Windows.MessageBoxOptions.None)
        End Function

        ''' <summary>Displays a message box that has a message, title bar caption, and button; and that returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <param name="button">A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to display.</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="button"/> is not member of <see cref="Windows.MessageBoxButton"/></exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.MessageBox.Show"/> function but provides </remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function Show(ByVal messageBoxText As String, ByVal caption As String, ByVal button As System.Windows.MessageBoxButton) As System.Windows.MessageBoxResult
            Return MessageBox.ShowCore(IntPtr.Zero, messageBoxText, caption, button, System.Windows.MessageBoxImage.None, System.Windows.MessageBoxResult.None, System.Windows.MessageBoxOptions.None)
        End Function

        ''' <summary>Displays a message box in front of the specified window. The message box displays a message and title bar caption; and it returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="owner">A <see cref="T:System.Windows.Window" /> that represents the owner window of the message box.</param>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.MessageBox.Show"/> function but provides </remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function Show(ByVal owner As System.Windows.Window, ByVal messageBoxText As String, ByVal caption As String) As System.Windows.MessageBoxResult
            Return MessageBox.ShowCore(New System.Windows.Interop.WindowInteropHelper(owner).Handle, messageBoxText, caption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.None, System.Windows.MessageBoxResult.None, System.Windows.MessageBoxOptions.None)
        End Function
        ''' <summary>Displays a message box that has a message, title bar caption, button, and icon; and that returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <param name="button">A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to display.</param>
        ''' <param name="icon">A <see cref="T:System.Windows.MessageBoxImage" /> value that specifies the icon to display.</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="button"/> is not member of <see cref="Windows.MessageBoxButton"/>
        ''' =or = <paramref name="icon"/> is not member of <see cref="Windows.MessageBoxImage"/></exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function is provided mainly for compatibility with <see cref="Windows.MessageBox"/>. You'd bete use overload which's <paramref name="icon"/> parameter is <see cref="MessageBoxIcons"/>. See <see cref="ConvertIconConstant"/> for explanation.</remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shared Function Show(ByVal messageBoxText As String, ByVal caption As String, ByVal button As System.Windows.MessageBoxButton, ByVal icon As System.Windows.MessageBoxImage) As System.Windows.MessageBoxResult
            Return MessageBox.ShowCore(IntPtr.Zero, messageBoxText, caption, button, icon, System.Windows.MessageBoxResult.None, System.Windows.MessageBoxOptions.None)
        End Function
        ''' <summary>Displays a message box that has a message, title bar caption, button, and icon; and that returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <param name="button">A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to display.</param>
        ''' <param name="icon">A <see cref="system.Windows.MessageBoxImage" /> value that specifies the icon to display. Values <see cref="System.Windows.MessageBoxImage.Asterisk"/>, <see cref="System.Windows.MessageBoxImage.Hand"/>, <see cref="System.Windows.MessageBoxImage.Exclamation"/> and <see cref="System.Windows.MessageBoxImage.Question"/> are assciated with appropriate <see cref="Media.SystemSounds"/>.</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="button"/> is not member of <see cref="Windows.MessageBoxButton"/></exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.MessageBox.Show"/> function but provides the <paramref name="icon"/> parameter as <see cref="MessageBoxIcons"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function Show(ByVal messageBoxText As String, ByVal caption As String, ByVal button As System.Windows.MessageBoxButton, ByVal icon As MessageBoxIcons) As System.Windows.MessageBoxResult
            Return MessageBox.ShowCore(IntPtr.Zero, messageBoxText, caption, button, icon, System.Windows.MessageBoxResult.None, System.Windows.MessageBoxOptions.None)
        End Function
        ''' <summary>Displays a message box in front of the specified window. The message box displays a message, title bar caption, and button; and it also returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="owner">A <see cref="T:System.Windows.Window" /> that represents the owner window of the message box.</param>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <param name="button">A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to display.</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="button"/> is not member of <see cref="Windows.MessageBoxButton"/></exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.MessageBox.Show"/> function</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function Show(ByVal owner As System.Windows.Window, ByVal messageBoxText As String, ByVal caption As String, ByVal button As System.Windows.MessageBoxButton) As System.Windows.MessageBoxResult
            Return MessageBox.ShowCore(New System.Windows.Interop.WindowInteropHelper(owner).Handle, messageBoxText, caption, button, System.Windows.MessageBoxImage.None, System.Windows.MessageBoxResult.None, System.Windows.MessageBoxOptions.None)
        End Function
        ''' <summary>Displays a message box that has a message, title bar caption, button, and icon; and that accepts a default message box result and returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <param name="button">A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to display.</param>
        ''' <param name="icon">A <see cref="T:System.Windows.MessageBoxImage" /> value that specifies the icon to display.</param>
        ''' <param name="defaultResult">A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies the default result of the message box.</param>
        ''' <remarks>This function is provided mainly for compatibility with <see cref="Windows.MessageBox"/>. You'd bete use overload which's <paramref name="icon"/> parameter is <see cref="MessageBoxIcons"/>. See <see cref="ConvertIconConstant"/> for explanation.</remarks>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="button"/> is not member of <see cref="Windows.MessageBoxButton"/>
        ''' =or= <paramref name="defaultResult"/> is not member of <see cref="Windows.MessageBoxResult"/>
        ''' =or = <paramref name="icon"/> is not member of <see cref="Windows.MessageBoxImage"/></exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shared Function Show(ByVal messageBoxText As String, ByVal caption As String, ByVal button As System.Windows.MessageBoxButton, ByVal icon As System.Windows.MessageBoxImage, ByVal defaultResult As System.Windows.MessageBoxResult) As System.Windows.MessageBoxResult
            Return MessageBox.ShowCore(IntPtr.Zero, messageBoxText, caption, button, icon, defaultResult, System.Windows.MessageBoxOptions.None)
        End Function
        ''' <summary>Displays a message box that has a message, title bar caption, button, and icon; and that accepts a default message box result and returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <param name="button">A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to display.</param>
        ''' <param name="icon">A <see cref="system.Windows.MessageBoxImage" /> value that specifies the icon to display. Values <see cref="System.Windows.MessageBoxImage.Asterisk"/>, <see cref="System.Windows.MessageBoxImage.Hand"/>, <see cref="System.Windows.MessageBoxImage.Exclamation"/> and <see cref="System.Windows.MessageBoxImage.Question"/> are assciated with appropriate <see cref="Media.SystemSounds"/>.</param>
        ''' <param name="defaultResult">A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies the default result of the message box.</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="button"/> is not member of <see cref="Windows.MessageBoxButton"/>
        ''' =or= <paramref name="defaultResult"/> is not member of <see cref="Windows.MessageBoxResult"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.MessageBox.Show"/> function but provides the <paramref name="icon"/> parameter as <see cref="MessageBoxIcons"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function Show(ByVal messageBoxText As String, ByVal caption As String, ByVal button As System.Windows.MessageBoxButton, ByVal icon As MessageBoxIcons, ByVal defaultResult As System.Windows.MessageBoxResult) As System.Windows.MessageBoxResult
            Return MessageBox.ShowCore(IntPtr.Zero, messageBoxText, caption, button, icon, defaultResult, System.Windows.MessageBoxOptions.None)
        End Function
        ''' <summary>Displays a message box in front of the specified window. The message box displays a message, title bar caption, button, and icon; and it also returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="owner">A <see cref="T:System.Windows.Window" /> that represents the owner window of the message box.</param>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <param name="button">A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to display.</param>
        ''' <param name="icon">A <see cref="T:System.Windows.MessageBoxImage" /> value that specifies the icon to display.</param>
        ''' <remarks>This function is provided mainly for compatibility with <see cref="Windows.MessageBox"/>. You'd bete use overload which's <paramref name="icon"/> parameter is <see cref="MessageBoxIcons"/>. See <see cref="ConvertIconConstant"/> for explanation.</remarks>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="button"/> is not member of <see cref="Windows.MessageBoxButton"/>
        ''' =or = <paramref name="icon"/> is not member of <see cref="Windows.MessageBoxImage"/></exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shared Function Show(ByVal owner As System.Windows.Window, ByVal messageBoxText As String, ByVal caption As String, ByVal button As System.Windows.MessageBoxButton, ByVal icon As System.Windows.MessageBoxImage) As System.Windows.MessageBoxResult
            Return MessageBox.ShowCore(New System.Windows.Interop.WindowInteropHelper(owner).Handle, messageBoxText, caption, button, icon, System.Windows.MessageBoxResult.None, System.Windows.MessageBoxOptions.None)
        End Function
        ''' <summary>Displays a message box in front of the specified window. The message box displays a message, title bar caption, button, and icon; and it also returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="owner">A <see cref="T:System.Windows.Window" /> that represents the owner window of the message box.</param>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <param name="button">A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to display.</param>
        ''' <param name="icon">A <see cref="system.Windows.MessageBoxImage" /> value that specifies the icon to display. Values <see cref="System.Windows.MessageBoxImage.Asterisk"/>, <see cref="System.Windows.MessageBoxImage.Hand"/>, <see cref="System.Windows.MessageBoxImage.Exclamation"/> and <see cref="System.Windows.MessageBoxImage.Question"/> are assciated with appropriate <see cref="Media.SystemSounds"/>.</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="button"/> is not member of <see cref="Windows.MessageBoxButton"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.MessageBox.Show"/> function but provides the <paramref name="icon"/> parameter as <see cref="MessageBoxIcons"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function Show(ByVal owner As System.Windows.Window, ByVal messageBoxText As String, ByVal caption As String, ByVal button As System.Windows.MessageBoxButton, ByVal icon As MessageBoxIcons) As System.Windows.MessageBoxResult
            Return MessageBox.ShowCore(New System.Windows.Interop.WindowInteropHelper(owner).Handle, messageBoxText, caption, button, icon, System.Windows.MessageBoxResult.None, System.Windows.MessageBoxOptions.None)
        End Function
        ''' <summary>Displays a message box that has a message, title bar caption, button, and icon; and that accepts a default message box result, complies with the specified options, and returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <param name="button">A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to display.</param>
        ''' <param name="icon">A <see cref="T:System.Windows.MessageBoxImage" /> value that specifies the icon to display.</param>
        ''' <param name="defaultResult">A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies the default result of the message box.</param>
        ''' <param name="options">A <see cref="T:System.Windows.MessageBoxOptions" /> value object that specifies the options.</param>
        ''' <remarks>This function is provided mainly for compatibility with <see cref="Windows.MessageBox"/>. You'd bete use overload which's <paramref name="icon"/> parameter is <see cref="MessageBoxIcons"/>. See <see cref="ConvertIconConstant"/> for explanation.</remarks>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="button"/> is not member of <see cref="Windows.MessageBoxButton"/>
        ''' =or= <paramref name="defaultResult"/> is not member of <see cref="Windows.MessageBoxResult"/>
        ''' =or= <paramref name="options"/> is not valid <see cref="windows.MessageBoxOptions"/> value
        ''' =or = <paramref name="icon"/> is not member of <see cref="Windows.MessageBoxImage"/></exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shared Function Show(ByVal messageBoxText As String, ByVal caption As String, ByVal button As System.Windows.MessageBoxButton, ByVal icon As System.Windows.MessageBoxImage, ByVal defaultResult As System.Windows.MessageBoxResult, ByVal options As System.Windows.MessageBoxOptions) As System.Windows.MessageBoxResult
            Return MessageBox.ShowCore(IntPtr.Zero, messageBoxText, caption, button, icon, defaultResult, options)
        End Function
        ''' <summary>Displays a message box that has a message, title bar caption, button, and icon; and that accepts a default message box result, complies with the specified options, and returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <param name="button">A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to display.</param>
        ''' <param name="icon">A <see cref="system.Windows.MessageBoxImage" /> value that specifies the icon to display. Values <see cref="System.Windows.MessageBoxImage.Asterisk"/>, <see cref="System.Windows.MessageBoxImage.Hand"/>, <see cref="System.Windows.MessageBoxImage.Exclamation"/> and <see cref="System.Windows.MessageBoxImage.Question"/> are assciated with appropriate <see cref="Media.SystemSounds"/>.</param>
        ''' <param name="defaultResult">A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies the default result of the message box.</param>
        ''' <param name="options">A <see cref="T:System.Windows.MessageBoxOptions" /> value object that specifies the options.</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="button"/> is not member of <see cref="Windows.MessageBoxButton"/>
        ''' =or= <paramref name="defaultResult"/> is not member of <see cref="Windows.MessageBoxResult"/>
        ''' =or= <paramref name="options"/> is not valid <see cref="windows.MessageBoxOptions"/> value
        '''</exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.MessageBox.Show"/> function but provides the <paramref name="icon"/> parameter as <see cref="MessageBoxIcons"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function Show(ByVal messageBoxText As String, ByVal caption As String, ByVal button As System.Windows.MessageBoxButton, ByVal icon As MessageBoxIcons, ByVal defaultResult As System.Windows.MessageBoxResult, ByVal options As System.Windows.MessageBoxOptions) As System.Windows.MessageBoxResult
            Return MessageBox.ShowCore(IntPtr.Zero, messageBoxText, caption, button, icon, defaultResult, options)
        End Function
        ''' <summary>Displays a message box in front of the specified window. The message box displays a message, title bar caption, button, and icon; and accepts a default message box result and returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="owner">A <see cref="T:System.Windows.Window" /> that represents the owner window of the message box.</param>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <param name="button">A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to display.</param>
        ''' <param name="icon">A <see cref="T:System.Windows.MessageBoxImage" /> value that specifies the icon to display.</param>
        ''' <param name="defaultResult">A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies the default result of the message box.</param>
        ''' <remarks>This function is provided mainly for compatibility with <see cref="Windows.MessageBox"/>. You'd bete use overload which's <paramref name="icon"/> parameter is <see cref="MessageBoxIcons"/>. See <see cref="ConvertIconConstant"/> for explanation.</remarks>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="button"/> is not member of <see cref="Windows.MessageBoxButton"/>
        ''' =or= <paramref name="options"/> is not valid <see cref="windows.MessageBoxOptions"/> value
        ''' =or = <paramref name="icon"/> is not member of <see cref="Windows.MessageBoxImage"/></exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shared Function Show(ByVal owner As System.Windows.Window, ByVal messageBoxText As String, ByVal caption As String, ByVal button As System.Windows.MessageBoxButton, ByVal icon As System.Windows.MessageBoxImage, ByVal defaultResult As System.Windows.MessageBoxResult) As System.Windows.MessageBoxResult
            Return MessageBox.ShowCore(New System.Windows.Interop.WindowInteropHelper(owner).Handle, messageBoxText, caption, button, icon, defaultResult, System.Windows.MessageBoxOptions.None)
        End Function
        ''' <summary>Displays a message box in front of the specified window. The message box displays a message, title bar caption, button, and icon; and accepts a default message box result and returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="owner">A <see cref="T:System.Windows.Window" /> that represents the owner window of the message box.</param>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <param name="button">A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to display.</param>
        ''' <param name="icon">A <see cref="system.Windows.MessageBoxImage" /> value that specifies the icon to display. Values <see cref="System.Windows.MessageBoxImage.Asterisk"/>, <see cref="System.Windows.MessageBoxImage.Hand"/>, <see cref="System.Windows.MessageBoxImage.Exclamation"/> and <see cref="System.Windows.MessageBoxImage.Question"/> are assciated with appropriate <see cref="Media.SystemSounds"/>.</param>
        ''' <param name="defaultResult">A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies the default result of the message box.</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="button"/> is not member of <see cref="Windows.MessageBoxButton"/>
        ''' =or= <paramref name="defaultResult"/> is not member of <see cref="Windows.MessageBoxResult"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.MessageBox.Show"/> function but provides the <paramref name="icon"/> parameter as <see cref="MessageBoxIcons"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function Show(ByVal owner As System.Windows.Window, ByVal messageBoxText As String, ByVal caption As String, ByVal button As System.Windows.MessageBoxButton, ByVal icon As MessageBoxIcons, ByVal defaultResult As System.Windows.MessageBoxResult) As System.Windows.MessageBoxResult
            Return MessageBox.ShowCore(New System.Windows.Interop.WindowInteropHelper(owner).Handle, messageBoxText, caption, button, icon, defaultResult, System.Windows.MessageBoxOptions.None)
        End Function
        ''' <summary>Displays a message box in front of the specified window. The message box displays a message, title bar caption, button, and icon; and accepts a default message box result, complies with the specified options, and returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="owner">A <see cref="T:System.Windows.Window" /> that represents the owner window of the message box.</param>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <param name="button">A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to display.</param>
        ''' <param name="icon">A <see cref="T:System.Windows.MessageBoxImage" /> value that specifies the icon to display.</param>
        ''' <param name="defaultResult">A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies the default result of the message box.</param>
        ''' <param name="options">A <see cref="T:System.Windows.MessageBoxOptions" /> value object that specifies the options.</param>
        ''' <remarks>This function is provided mainly for compatibility with <see cref="Windows.MessageBox"/>. You'd bete use overload which's <paramref name="icon"/> parameter is <see cref="MessageBoxIcons"/>. See <see cref="ConvertIconConstant"/> for explanation.</remarks>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="button"/> is not member of <see cref="Windows.MessageBoxButton"/>
        ''' =or= <paramref name="defaultResult"/> is not member of <see cref="Windows.MessageBoxResult"/>
        ''' =or= <paramref name="options"/> is not valid <see cref="windows.MessageBoxOptions"/> value
        ''' =or = <paramref name="icon"/> is not member of <see cref="Windows.MessageBoxImage"/></exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shared Function Show(ByVal owner As System.Windows.Window, ByVal messageBoxText As String, ByVal caption As String, ByVal button As System.Windows.MessageBoxButton, ByVal icon As System.Windows.MessageBoxImage, ByVal defaultResult As System.Windows.MessageBoxResult, ByVal options As System.Windows.MessageBoxOptions) As System.Windows.MessageBoxResult
            Return MessageBox.ShowCore(New System.Windows.Interop.WindowInteropHelper(owner).Handle, messageBoxText, caption, button, icon, defaultResult, options)
        End Function
        ''' <summary>Displays a message box in front of the specified window. The message box displays a message, title bar caption, button, and icon; and accepts a default message box result, complies with the specified options, and returns a result.</summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="owner">A <see cref="T:System.Windows.Window" /> that represents the owner window of the message box.</param>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <param name="button">A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to display.</param>
        ''' <param name="icon">A <see cref="system.Windows.MessageBoxImage" /> value that specifies the icon to display. Values <see cref="System.Windows.MessageBoxImage.Asterisk"/>, <see cref="System.Windows.MessageBoxImage.Hand"/>, <see cref="System.Windows.MessageBoxImage.Exclamation"/> and <see cref="System.Windows.MessageBoxImage.Question"/> are assciated with appropriate <see cref="Media.SystemSounds"/>.</param>
        ''' <param name="defaultResult">A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies the default result of the message box.</param>
        ''' <param name="options">A <see cref="T:System.Windows.MessageBoxOptions" /> value object that specifies the options.</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="button"/> is not member of <see cref="Windows.MessageBoxButton"/>
        ''' =or= <paramref name="defaultResult"/> is not member of <see cref="Windows.MessageBoxResult"/>
        ''' =or= <paramref name="options"/> is not valid <see cref="windows.MessageBoxOptions"/> value
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.MessageBox.Show"/> function but provides the <paramref name="icon"/> parameter as <see cref="MessageBoxIcons"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Shared Function Show(ByVal owner As System.Windows.Window, ByVal messageBoxText As String, ByVal caption As String, ByVal button As System.Windows.MessageBoxButton, ByVal icon As MessageBoxIcons, ByVal defaultResult As System.Windows.MessageBoxResult, ByVal options As System.Windows.MessageBoxOptions) As System.Windows.MessageBoxResult
            Return MessageBox.ShowCore(New System.Windows.Interop.WindowInteropHelper(owner).Handle, messageBoxText, caption, button, icon, defaultResult, options)
        End Function
#End Region
#Region "ShowCore"
        ''' <summary>Performs WPF-like message box with <see cref="Windows.MessageBoxImage"/></summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="owner">A <see cref="T:System.Windows.Window" /> that represents the owner window of the message box.</param>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <param name="button">A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to display.</param>
        ''' <param name="icon">A <see cref="system.Windows.MessageBoxImage" /> value that specifies the icon to display. Values <see cref="System.Windows.MessageBoxImage.Asterisk"/>, <see cref="System.Windows.MessageBoxImage.Hand"/>, <see cref="System.Windows.MessageBoxImage.Exclamation"/> and <see cref="System.Windows.MessageBoxImage.Question"/> are assciated with appropriate <see cref="Media.SystemSounds"/>.</param>
        ''' <param name="defaultResult">A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies the default result of the message box.</param>
        ''' <param name="options">A <see cref="T:System.Windows.MessageBoxOptions" /> value object that specifies the options.</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="button"/> is not member of <see cref="Windows.MessageBoxButton"/>
        ''' =or= <paramref name="defaultResult"/> is not member of <see cref="Windows.MessageBoxResult"/>
        ''' =or= <paramref name="options"/> is not valid <see cref="windows.MessageBoxOptions"/> value
        ''' =or = <paramref name="icon"/> is not member of <see cref="Windows.MessageBoxImage"/></exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        Private Shared Function ShowCore(ByVal owner As IntPtr, ByVal messageBoxText As String, ByVal caption As String, ByVal button As System.Windows.MessageBoxButton, ByVal icon As Windows.MessageBoxImage, ByVal defaultResult As System.Windows.MessageBoxResult, ByVal options As System.Windows.MessageBoxOptions) As System.Windows.MessageBoxResult
            If Not MessageBox.IsValidMessageBoxImage(icon) Then _
               Throw New InvalidEnumArgumentException("icon", CInt(icon), GetType(System.Windows.MessageBoxImage))
            Return ShowCore(owner, messageBoxText, caption, button, ConvertIconConstant(icon), defaultResult, options)
        End Function
        ''' <summary>Performs WPF-like message box with <see cref="MessageBoxIcon"/></summary>
        ''' <returns>A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies which message box button is clicked by the user.</returns>
        ''' <param name="owner">A <see cref="T:System.Windows.Window" /> that represents the owner window of the message box.</param>
        ''' <param name="messageBoxText">A <see cref="T:System.String" /> that specifies the text to display.</param>
        ''' <param name="caption">A <see cref="T:System.String" /> that specifies the title bar caption to display.</param>
        ''' <param name="button">A <see cref="T:System.Windows.MessageBoxButton" /> value that specifies which button or buttons to display.</param>
        ''' <param name="icon">A <see cref="system.Windows.MessageBoxImage" /> value that specifies the icon to display. Values <see cref="System.Windows.MessageBoxImage.Asterisk"/>, <see cref="System.Windows.MessageBoxImage.Hand"/>, <see cref="System.Windows.MessageBoxImage.Exclamation"/> and <see cref="System.Windows.MessageBoxImage.Question"/> are assciated with appropriate <see cref="Media.SystemSounds"/>.</param>
        ''' <param name="defaultResult">A <see cref="T:System.Windows.MessageBoxResult" /> value that specifies the default result of the message box.</param>
        ''' <param name="options">A <see cref="T:System.Windows.MessageBoxOptions" /> value object that specifies the options.</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="button"/> is not member of <see cref="Windows.MessageBoxButton"/>
        ''' =or= <paramref name="defaultResult"/> is not member of <see cref="Windows.MessageBoxResult"/>
        ''' =or= <paramref name="options"/> is not valid <see cref="windows.MessageBoxOptions"/> value</exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        Private Shared Function ShowCore(ByVal owner As IntPtr, ByVal messageBoxText As String, ByVal caption As String, ByVal button As System.Windows.MessageBoxButton, ByVal icon As MessageBoxIcons, ByVal defaultResult As System.Windows.MessageBoxResult, ByVal options As System.Windows.MessageBoxOptions) As System.Windows.MessageBoxResult
            If Not MessageBox.IsValidMessageBoxButton(button) Then _
                Throw New InvalidEnumArgumentException("button", CInt(button), GetType(System.Windows.MessageBoxButton))
            If Not MessageBox.IsValidMessageBoxResult(defaultResult) Then _
                Throw New InvalidEnumArgumentException("defaultResult", CInt(defaultResult), GetType(System.Windows.MessageBoxResult))
            If Not MessageBox.IsValidMessageBoxOptions(options) Then _
                Throw New InvalidEnumArgumentException("options", CInt(options), GetType(System.Windows.MessageBoxOptions))
            Try
                Dim box As New FakeBox With {.Prompt = messageBoxText, .Title = caption}
                box.Buttons.Clear()
                box.Buttons.AddRange(MessageBoxButton.GetButtons(button))
                Select Case defaultResult
                    Case Windows.MessageBoxResult.Cancel
                        Select Case button
                            Case Windows.MessageBoxButton.OKCancel : box.DefaultButton = 1
                            Case Windows.MessageBoxButton.YesNoCancel : box.DefaultButton = 2
                            Case Else : box.DefaultButton = -1
                        End Select
                    Case Windows.MessageBoxResult.OK
                        Select Case button
                            Case Windows.MessageBoxButton.OK, Windows.MessageBoxButton.OKCancel : box.DefaultButton = 0
                            Case Else : box.DefaultButton = -1
                        End Select
                    Case Windows.MessageBoxResult.No
                        Select Case button
                            Case Windows.MessageBoxButton.YesNo, Windows.MessageBoxButton.YesNoCancel : box.DefaultButton = 1
                            Case Else : box.DefaultButton = -1
                        End Select
                    Case Windows.MessageBoxResult.None : box.DefaultButton = -1
                    Case Windows.MessageBoxResult.Yes
                        Select Case button
                            Case Windows.MessageBoxButton.YesNo, Windows.MessageBoxButton.YesNoCancel : box.DefaultButton = 0
                            Case Else : box.DefaultButton = -1
                        End Select
                End Select
                box.Icon = GetIconDelegate.Invoke(icon)
                Select Case icon
                    Case MessageBoxIcons.Asterisk : box.PlayOnShow = Media.SystemSounds.Asterisk
                    Case MessageBoxIcons.Hand : box.PlayOnShow = Media.SystemSounds.Hand
                    Case MessageBoxIcons.Exclamation : box.PlayOnShow = Media.SystemSounds.Exclamation
                    Case MessageBoxIcons.Question : box.PlayOnShow = Media.SystemSounds.Question
                End Select
                If (options And Windows.MessageBoxOptions.RightAlign) = Windows.MessageBoxOptions.RightAlign Then box.Options = box.Options Or MessageBoxOptions.AlignRight
                If (options And Windows.MessageBoxOptions.RtlReading) = Windows.MessageBoxOptions.RtlReading Then box.Options = box.Options Or MessageBoxOptions.Rtl
                Return ShowTemplate(box, New WPFWindow(owner))
            Catch ex As Exception When Not TypeOf ex Is TargetInvocationException
                Throw New TargetInvocationException(ResourcesT.Exceptions.ThereWasAnErrorInvokingMessageBoxSeeInnerExceptionForDetails, ex)
            End Try
        End Function
#End Region
#End Region

#Region "Windows.Forms.MessageBox"
#Region "Show"
        ''' <summary>Displays a message box with specified text.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="text">The text to display in the message box. </param>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.Forms.MessageBox.Show"/> function</remarks>
        <EditorBrowsable(EditorBrowsableState.Always)> _
        Public Shared Function Show(ByVal [text] As String) As DialogResult
            Return MessageBox.ShowCore(Nothing, [text], String.Empty, MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0)
        End Function

        ''' <summary>Displays a message box with specified text and caption.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="caption">The text to display in the title bar of the message box. </param>
        ''' <param name="text">The text to display in the message box. </param>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.Forms.MessageBox.Show"/> function</remarks>
        <EditorBrowsable(EditorBrowsableState.Always)> _
         Public Shared Function Show(ByVal [text] As String, ByVal caption As String) As DialogResult
            Return MessageBox.ShowCore(Nothing, [text], caption, MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0)
        End Function

        ''' <summary>Displays a message box in front of the specified object and with the specified text.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="Text">The text to display in the message box. </param>
        ''' <param name="Owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window"></see> that will own the modal dialog box. </param>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.Forms.MessageBox.Show"/> function</remarks>
        <EditorBrowsable(EditorBrowsableState.Always)> _
         Public Shared Function Show(ByVal owner As IWin32Window, ByVal [text] As String) As DialogResult
            Return MessageBox.ShowCore(owner, [text], String.Empty, MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0)
        End Function

        ''' <summary>Displays a message box with specified text, caption, and buttons.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="caption">The text to display in the title bar of the message box. </param>
        ''' <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons"></see> values that specifies which buttons to display in the message box. </param>
        ''' <param name="text">The text to display in the message box. </param>
        ''' <exception cref="InvalidEnumArgumentException">
        ''' <paramref name="buttons"/> is not member of <see cref="MessageBoxButtons"/> =or=
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.Forms.MessageBox.Show"/> function</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
           Public Shared Function Show(ByVal [text] As String, ByVal caption As String, ByVal buttons As MessageBoxButtons) As DialogResult
            Return MessageBox.ShowCore(Nothing, [text], caption, buttons, Windows.Forms.MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0)
        End Function

        ''' <summary>Displays a message box in front of the specified object and with the specified text and caption.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window"></see> that will own the modal dialog box.</param>
        ''' <param name="caption">The text to display in the title bar of the message box. </param>
        ''' <param name="text">The text to display in the message box. </param>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.Forms.MessageBox.Show"/> function</remarks>
        <EditorBrowsable(EditorBrowsableState.Always)> _
           Public Shared Function Show(ByVal owner As IWin32Window, ByVal [text] As String, ByVal caption As String) As DialogResult
            Return MessageBox.ShowCore(owner, [text], caption, MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0)
        End Function

        ''' <summary>Displays a message box with specified text, caption, buttons, and icon.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon"></see> values that specifies which icon to display in the message box. Values <see cref="System.Windows.Forms.MessageBoxIcon.Asterisk"/>, <see cref="System.Windows.Forms.MessageBoxIcon.Exclamation"/>, <see cref="System.Windows.Forms.MessageBoxIcon.Hand"/> and <see cref="System.Windows.Forms.MessageBoxIcon.Question"/> are associated with appropriate <see cref="Media.SystemSounds"/>.</param>
        ''' <param name="caption">The text to display in the title bar of the message box. </param>
        ''' <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons"></see> values that specifies which buttons to display in the message box. </param>
        ''' <param name="text">The text to display in the message box. </param>
        ''' <exception cref="InvalidEnumArgumentException">
        ''' <paramref name="buttons"/> is not member of <see cref="MessageBoxButtons"/> =or=
        ''' <paramref name="icon"/> is not member of <see cref="MessageBoxIcon"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This overload is provided mainly for compatibility with <see cref="Windows.Forms.MessageBox"/>. You'd better use
        ''' <see cref="M:Tools.WindowsT.IndependentT.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,Tools.WindowsT.IndependentT.MessageBox.MessageBoxIcons)">overload which's <paramref name="icon"/> parameter is <see cref="MessageBoxIcons"/></see>.</remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shared Function Show(ByVal [text] As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As Windows.Forms.MessageBoxIcon) As DialogResult
            Return MessageBox.ShowCore(Nothing, [text], caption, buttons, icon, MessageBoxDefaultButton.Button1, 0)
        End Function
        ''' <summary>Displays a message box with specified text, caption, buttons, and icon.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon"></see> values that specifies which icon to display in the message box. Values <see cref="System.Windows.Forms.MessageBoxIcon.Asterisk"/>, <see cref="System.Windows.Forms.MessageBoxIcon.Exclamation"/>, <see cref="System.Windows.Forms.MessageBoxIcon.Hand"/> and <see cref="System.Windows.Forms.MessageBoxIcon.Question"/> are associated with appropriate <see cref="Media.SystemSounds"/>.</param>
        ''' <param name="caption">The text to display in the title bar of the message box. </param>
        ''' <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons"></see> values that specifies which buttons to display in the message box. </param>
        ''' <param name="text">The text to display in the message box. </param>
        ''' <exception cref="InvalidEnumArgumentException">
        ''' <paramref name="buttons"/> is not member of <see cref="MessageBoxButtons"/> =or=
        ''' <paramref name="icon"/> is not member of <see cref="MessageBoxIcons"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.Forms.MessageBox.Show"/> function, but the <paramref name="icon"/> parameter as <see cref="MessageBoxIcons"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
         Public Shared Function Show(ByVal [text] As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcons) As DialogResult
            Return MessageBox.ShowCore(Nothing, [text], caption, buttons, icon, MessageBoxDefaultButton.Button1, 0)
        End Function
        ''' <summary>Displays a message box in front of the specified object and with the specified text, caption, and buttons.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="Owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window"></see> that will own the modal dialog box.</param>
        ''' <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons"></see> values that specifies which buttons to display in the message box. </param>
        ''' <param name="Caption">The text to display in the title bar of the message box. </param>
        ''' <param name="text">The text to display in the message box. </param>
        ''' <exception cref="InvalidEnumArgumentException">
        ''' <paramref name="buttons"/> is not member of <see cref="MessageBoxButtons"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.Forms.MessageBox.Show"/> function</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
           Public Shared Function Show(ByVal owner As IWin32Window, ByVal [text] As String, ByVal caption As String, ByVal buttons As MessageBoxButtons) As DialogResult
            Return MessageBox.ShowCore(owner, [text], caption, buttons, Windows.Forms.MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0)
        End Function

        ''' <summary>Displays a message box with the specified text, caption, buttons, icon, and default button.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon"></see> values that specifies which icon to display in the message box. </param>
        ''' <param name="caption">The text to display in the title bar of the message box. </param>
        ''' <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons"></see> values that specifies which buttons to display in the message box. </param>
        ''' <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton"></see> values that specifies the default button for the message box. </param>
        ''' <param name="text">The text to display in the message box. </param>
        ''' <exception cref="InvalidEnumArgumentException">
        ''' <paramref name="buttons"/> is not member of <see cref="MessageBoxButtons"/> =or=
        ''' <paramref name="icon"/> is not member of <see cref="MessageBoxIcon"/> =or=
        ''' <paramref name="defaultButton"/> is not membember of <see cref="MessageBoxDefaultButton"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This overload is provided mainly for compatibility with <see cref="Windows.Forms.MessageBox"/>. You'd better use
        ''' <see cref="M:Tools.WindowsT.IndependentT.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,Tools.WindowsT.IndependentT.MessageBox.MessageBoxIcons,System.Windows.Forms.MessageBoxDefaultButton)">overload which's <paramref name="icon"/> parameter is <see cref="MessageBoxIcons"/></see>.</remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shared Function Show(ByVal [text] As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As Windows.Forms.MessageBoxIcon, ByVal defaultButton As MessageBoxDefaultButton) As DialogResult
            Return MessageBox.ShowCore(Nothing, [text], caption, buttons, icon, defaultButton, 0)
        End Function
        ''' <summary>Displays a message box with the specified text, caption, buttons, icon, and default button.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon"></see> values that specifies which icon to display in the message box. Values <see cref="System.Windows.Forms.MessageBoxIcon.Asterisk"/>, <see cref="System.Windows.Forms.MessageBoxIcon.Exclamation"/>, <see cref="System.Windows.Forms.MessageBoxIcon.Hand"/> and <see cref="System.Windows.Forms.MessageBoxIcon.Question"/> are associated with appropriate <see cref="Media.SystemSounds"/>.</param>
        ''' <param name="caption">The text to display in the title bar of the message box. </param>
        ''' <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons"></see> values that specifies which buttons to display in the message box. </param>
        ''' <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton"></see> values that specifies the default button for the message box. </param>
        ''' <param name="text">The text to display in the message box. </param>
        ''' <exception cref="InvalidEnumArgumentException">
        ''' <paramref name="buttons"/> is not member of <see cref="MessageBoxButtons"/> =or=
        ''' <paramref name="icon"/> is not member of <see cref="MessageBoxIcons"/> =or=
        ''' <paramref name="defaultButton"/> is not membember of <see cref="MessageBoxDefaultButton"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.Forms.MessageBox.Show"/> function, but the <paramref name="icon"/> parameter as <see cref="MessageBoxIcons"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
           Public Shared Function Show(ByVal [text] As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcons, ByVal defaultButton As MessageBoxDefaultButton) As DialogResult
            Return MessageBox.ShowCore(Nothing, [text], caption, buttons, icon, defaultButton, 0)
        End Function

        ''' <summary>Displays a message box in front of the specified object and with the specified text, caption, buttons, and icon.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="Text">The text to display in the message box. </param>
        ''' <param name="Icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon"></see> values that specifies which icon to display in the message box. </param>
        ''' <param name="Owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window"></see> that will own the modal dialog box.</param>
        ''' <param name="Buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons"></see> values that specifies which buttons to display in the message box. </param>
        ''' <param name="Caption">The text to display in the title bar of the message box. </param>
        ''' <exception cref="InvalidEnumArgumentException">
        ''' <paramref name="buttons"/> is not member of <see cref="MessageBoxButtons"/> =or=
        ''' <paramref name="icon"/> is not member of <see cref="MessageBoxIcon"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This overload is provided mainly for compatibility with <see cref="Windows.Forms.MessageBox"/>. You'd better use
        ''' <see cref="M:Tools.WindowsT.IndependentT.MessageBox.Show(System.Windows.Forms.IWin32Window,System.String,System.String,System.Windows.Forms.MessageBoxButtons,Tools.WindowsT.IndependentT.MessageBox.MessageBoxIcons)">overload which's <paramref name="icon"/> parameter is <see cref="MessageBoxIcons"/></see>.</remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shared Function Show(ByVal owner As IWin32Window, ByVal [text] As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As Windows.Forms.MessageBoxIcon) As DialogResult
            Return MessageBox.ShowCore(owner, [text], caption, buttons, icon, MessageBoxDefaultButton.Button1, 0)
        End Function
        ''' <summary>Displays a message box in front of the specified object and with the specified text, caption, buttons, and icon.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="Text">The text to display in the message box. </param>
        ''' <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon"></see> values that specifies which icon to display in the message box. Values <see cref="System.Windows.Forms.MessageBoxIcon.Asterisk"/>, <see cref="System.Windows.Forms.MessageBoxIcon.Exclamation"/>, <see cref="System.Windows.Forms.MessageBoxIcon.Hand"/> and <see cref="System.Windows.Forms.MessageBoxIcon.Question"/> are associated with appropriate <see cref="Media.SystemSounds"/>.</param>
        ''' <param name="Owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window"></see> that will own the modal dialog box.</param>
        ''' <param name="Buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons"></see> values that specifies which buttons to display in the message box. </param>
        ''' <param name="Caption">The text to display in the title bar of the message box. </param>
        ''' <exception cref="InvalidEnumArgumentException">
        ''' <paramref name="buttons"/> is not member of <see cref="MessageBoxButtons"/> =or=
        ''' <paramref name="icon"/> is not member of <see cref="MessageBoxIcons"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.Forms.MessageBox.Show"/> function, but the <paramref name="icon"/> parameter as <see cref="MessageBoxIcons"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
         Public Shared Function Show(ByVal owner As IWin32Window, ByVal [text] As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcons) As DialogResult
            Return MessageBox.ShowCore(owner, [text], caption, buttons, icon, MessageBoxDefaultButton.Button1, 0)
        End Function

        ''' <summary>Displays a message box with the specified text, caption, buttons, icon, default button, and options.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon"></see> values that specifies which icon to display in the message box. </param>
        ''' <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions"></see> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        ''' <param name="caption">The text to display in the title bar of the message box. </param>
        ''' <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons"></see> values that specifies which buttons to display in the message box. </param>
        ''' <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton"></see> values that specifies the default button for the message box. </param>
        ''' <param name="text">The text to display in the message box. </param>
        ''' <exception cref="InvalidEnumArgumentException">
        ''' <paramref name="buttons"/> is not member of <see cref="MessageBoxButtons"/> =or=
        ''' <paramref name="icon"/> is not member of <see cref="MessageBoxIcon"/> =or=
        ''' <paramref name="defaultButton"/> is not membember of <see cref="MessageBoxDefaultButton"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This overload is provided mainly for compatibility with <see cref="Windows.Forms.MessageBox"/>. You'd better use
        ''' <see cref="M:Tools.WindowsT.IndependentT.MessageBox.Show(System.String,System.String,System.Windows.Forms.MessageBoxButtons,Tools.WindowsT.IndependentT.MessageBox.MessageBoxIcons,System.Windows.Forms.MessageBoxDefaultButton,System.Windows.Forms.MessageBoxOptions)">overload which's <paramref name="icon"/> parameter is <see cref="MessageBoxIcons"/></see>.</remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Shared Function Show(ByVal [text] As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As Windows.Forms.MessageBoxIcon, ByVal defaultButton As MessageBoxDefaultButton, ByVal options As Windows.Forms.MessageBoxOptions) As DialogResult
            Return MessageBox.ShowCore(Nothing, [text], caption, buttons, icon, defaultButton, options)
        End Function
        ''' <summary>Displays a message box with the specified text, caption, buttons, icon, default button, and options.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon"></see> values that specifies which icon to display in the message box. Values <see cref="System.Windows.Forms.MessageBoxIcon.Asterisk"/>, <see cref="System.Windows.Forms.MessageBoxIcon.Exclamation"/>, <see cref="System.Windows.Forms.MessageBoxIcon.Hand"/> and <see cref="System.Windows.Forms.MessageBoxIcon.Question"/> are associated with appropriate <see cref="Media.SystemSounds"/>.</param>
        ''' <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions"></see> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        ''' <param name="caption">The text to display in the title bar of the message box. </param>
        ''' <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons"></see> values that specifies which buttons to display in the message box. </param>
        ''' <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton"></see> values that specifies the default button for the message box. </param>
        ''' <param name="text">The text to display in the message box. </param>
        ''' <exception cref="InvalidEnumArgumentException">
        ''' <paramref name="buttons"/> is not member of <see cref="MessageBoxButtons"/> =or=
        ''' <paramref name="icon"/> is not member of <see cref="MessageBoxIcons"/> =or=
        ''' <paramref name="defaultButton"/> is not membember of <see cref="MessageBoxDefaultButton"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.Forms.MessageBox.Show"/> function, but the <paramref name="icon"/> parameter as <see cref="MessageBoxIcons"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
           Public Shared Function Show(ByVal [text] As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcons, ByVal defaultButton As MessageBoxDefaultButton, ByVal options As Windows.Forms.MessageBoxOptions) As DialogResult
            Return MessageBox.ShowCore(Nothing, [text], caption, buttons, icon, defaultButton, options)
        End Function

        ''' <summary>Displays a message box in front of the specified object and with the specified text, caption, buttons, icon, and default button.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="Icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon"></see> values that specifies which icon to display in the message box. </param>
        ''' <param name="Owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window"></see> that will own the modal dialog box.</param>
        ''' <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton"></see> values that specifies the default button for the message box. </param>
        ''' <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons"></see> values that specifies which buttons to display in the message box. </param>
        ''' <param name="Caption">The text to display in the title bar of the message box. </param>
        ''' <param name="text">The text to display in the message box. </param>
        ''' <exception cref="InvalidEnumArgumentException">
        ''' <paramref name="buttons"/> is not member of <see cref="MessageBoxButtons"/> =or=
        ''' <paramref name="icon"/> is not member of <see cref="MessageBoxIcon"/> =or=
        ''' <paramref name="defaultButton"/> is not membember of <see cref="MessageBoxDefaultButton"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This overload is provided mainly for compatibility with <see cref="Windows.Forms.MessageBox"/>. You'd better use
        ''' <see cref="M:Tools.WindowsT.IndependentT.MessageBox.Show(System.Windows.Forms.IWin32Window,System.String,System.String,System.Windows.Forms.MessageBoxButtons,Tools.WindowsT.IndependentT.MessageBox.MessageBoxIcons,System.Windows.Forms.MessageBoxDefaultButton)">overload which's <paramref name="icon"/> parameter is <see cref="MessageBoxIcons"/></see>.</remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
         Public Shared Function Show(ByVal owner As IWin32Window, ByVal [text] As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As Windows.Forms.MessageBoxIcon, ByVal defaultButton As MessageBoxDefaultButton) As DialogResult
            Return MessageBox.ShowCore(owner, [text], caption, buttons, icon, defaultButton, 0)
        End Function
        ''' <summary>Displays a message box in front of the specified object and with the specified text, caption, buttons, icon, and default button.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon"></see> values that specifies which icon to display in the message box. Values <see cref="System.Windows.Forms.MessageBoxIcon.Asterisk"/>, <see cref="System.Windows.Forms.MessageBoxIcon.Exclamation"/>, <see cref="System.Windows.Forms.MessageBoxIcon.Hand"/> and <see cref="System.Windows.Forms.MessageBoxIcon.Question"/> are associated with appropriate <see cref="Media.SystemSounds"/>.</param>
        ''' <param name="Owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window"></see> that will own the modal dialog box.</param>
        ''' <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton"></see> values that specifies the default button for the message box. </param>
        ''' <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons"></see> values that specifies which buttons to display in the message box. </param>
        ''' <param name="Caption">The text to display in the title bar of the message box. </param>
        ''' <param name="text">The text to display in the message box. </param>
        ''' <exception cref="InvalidEnumArgumentException">
        ''' <paramref name="buttons"/> is not member of <see cref="MessageBoxButtons"/> =or=
        ''' <paramref name="icon"/> is not member of <see cref="MessageBoxIcons"/> =or=
        ''' <paramref name="defaultButton"/> is not membember of <see cref="MessageBoxDefaultButton"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.Forms.MessageBox.Show"/> function, but the <paramref name="icon"/> parameter as <see cref="MessageBoxIcons"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
          Public Shared Function Show(ByVal owner As IWin32Window, ByVal [text] As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcons, ByVal defaultButton As MessageBoxDefaultButton) As DialogResult
            Return MessageBox.ShowCore(owner, [text], caption, buttons, icon, defaultButton, 0)
        End Function

        ''' <summary>Displays a message box in front of the specified object and with the specified text, caption, buttons, icon, default button, and options.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="Text">The text to display in the message box. </param>
        ''' <param name="Icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon"></see> values that specifies which icon to display in the message box. </param>
        ''' <param name="Options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions"></see> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        ''' <param name="Owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window"></see> that will own the modal dialog box.</param>
        ''' <param name="Buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons"></see> values that specifies which buttons to display in the message box. </param>
        ''' <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton"></see> values the specifies the default button for the message box. </param>
        ''' <param name="Caption">The text to display in the title bar of the message box. </param>
        ''' <exception cref="InvalidEnumArgumentException">
        ''' <paramref name="buttons"/> is not member of <see cref="MessageBoxButtons"/> =or=
        ''' <paramref name="icon"/> is not member of <see cref="MessageBoxIcon"/> =or=
        ''' <paramref name="defaultButton"/> is not membember of <see cref="MessageBoxDefaultButton"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This overload is provided mainly for compatibility with <see cref="Windows.Forms.MessageBox"/>. You'd better use
        ''' <see cref="M:Tools.WindowsT.IndependentT.MessageBox.Show(System.Windows.Forms.IWin32Window,System.String,System.String,System.Windows.Forms.MessageBoxButtons,Tools.WindowsT.IndependentT.MessageBox.MessageBoxIcons,System.Windows.Forms.MessageBoxDefaultButton,System.Windows.Forms.MessageBoxOptions)">overload which's <paramref name="icon"/> parameter is <see cref="MessageBoxIcons"/></see>.</remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
         Public Shared Function Show(ByVal owner As IWin32Window, ByVal [text] As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As Windows.Forms.MessageBoxIcon, ByVal defaultButton As MessageBoxDefaultButton, ByVal options As Windows.Forms.MessageBoxOptions) As DialogResult
            Return MessageBox.ShowCore(owner, [text], caption, buttons, icon, defaultButton, options)
        End Function
        ''' <summary>Displays a message box in front of the specified object and with the specified text, caption, buttons, icon, default button, and options.</summary>
        ''' <returns>One of the <see cref="T:System.Windows.Forms.DialogResult"></see> values.</returns>
        ''' <param name="Text">The text to display in the message box. </param>
        ''' <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon"></see> values that specifies which icon to display in the message box. Values <see cref="System.Windows.Forms.MessageBoxIcon.Asterisk"/>, <see cref="System.Windows.Forms.MessageBoxIcon.Exclamation"/>, <see cref="System.Windows.Forms.MessageBoxIcon.Hand"/> and <see cref="System.Windows.Forms.MessageBoxIcon.Question"/> are associated with appropriate <see cref="Media.SystemSounds"/>.</param>
        ''' <param name="Options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions"></see> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        ''' <param name="Owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window"></see> that will own the modal dialog box.</param>
        ''' <param name="Buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons"></see> values that specifies which buttons to display in the message box. </param>
        ''' <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton"></see> values the specifies the default button for the message box. </param>
        ''' <param name="Caption">The text to display in the title bar of the message box. </param>
        ''' <exception cref="InvalidEnumArgumentException">
        ''' <paramref name="buttons"/> is not member of <see cref="MessageBoxButtons"/> =or=
        ''' <paramref name="icon"/> is not member of <see cref="MessageBoxIcons"/> =or=
        ''' <paramref name="defaultButton"/> is not membember of <see cref="MessageBoxDefaultButton"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimics the <see cref="Windows.Forms.MessageBox.Show"/> function, but the <paramref name="icon"/> parameter as <see cref="MessageBoxIcons"/></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
          Public Shared Function Show(ByVal owner As IWin32Window, ByVal [text] As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcons, ByVal defaultButton As MessageBoxDefaultButton, ByVal options As Windows.Forms.MessageBoxOptions) As DialogResult
            Return MessageBox.ShowCore(owner, [text], caption, buttons, icon, defaultButton, options)
        End Function
#End Region
#Region "ShowCore"
        ''' <summary>Performs modal dialog for WinForms-like functions with <see cref="MessageBoxIcons"/></summary>
        ''' <param name="Text">The text to display in the message box. </param>
        ''' <param name="Icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon"></see> values that specifies which icon to display in the message box. </param>
        ''' <param name="Options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions"></see> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        ''' <param name="Owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window"></see> that will own the modal dialog box.</param>
        ''' <param name="Buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons"></see> values that specifies which buttons to display in the message box. </param>
        ''' <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton"></see> values the specifies the default button for the message box. </param>
        ''' <param name="Caption">The text to display in the title bar of the message box. </param>
        ''' <exception cref="InvalidEnumArgumentException">
        ''' <paramref name="buttons"/> is not member of <see cref="MessageBoxButtons"/> =or=
        ''' <paramref name="icon"/> is not member of <see cref="MessageBoxIcon"/> =or=
        ''' <paramref name="defaultButton"/> is not membember of <see cref="MessageBoxDefaultButton"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        Private Shared Function ShowCore(ByVal owner As IWin32Window, ByVal [text] As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcons, ByVal defaultButton As MessageBoxDefaultButton, ByVal options As Windows.Forms.MessageBoxOptions) As DialogResult
            If Not InEnum(buttons) Then _
                Throw New InvalidEnumArgumentException("buttons", buttons, GetType(MessageBoxButtons))
            If Not InEnum(icon) Then _
                Throw New InvalidEnumArgumentException("icon", icon, GetType(MessageBoxIcon))
            If Not InEnum(defaultButton) Then _
                Throw New InvalidEnumArgumentException("defaultButton", defaultButton, GetType(DialogResult))
            Try
                Dim box As New FakeBox With {.Prompt = text, .Title = caption}
                box.Buttons.Clear()
                box.Buttons.AddRange(MessageBoxButton.GetButtons(buttons))
                Select Case defaultButton
                    Case MessageBoxDefaultButton.Button1 : box.DefaultButton = 0
                    Case MessageBoxDefaultButton.Button2 : box.DefaultButton = 1
                    Case MessageBoxDefaultButton.Button3 : box.DefaultButton = 2
                End Select
                Dim gIcon = GetIconDelegate.Invoke(icon)
                If gIcon Is Nothing Then
                    box.Icon = Nothing
                Else : box.Icon = gIcon
                End If
                Select Case icon
                    Case MessageBoxIcons.Asterisk : box.PlayOnShow = Media.SystemSounds.Asterisk
                    Case MessageBoxIcons.Hand : box.PlayOnShow = Media.SystemSounds.Hand
                    Case MessageBoxIcons.Exclamation : box.PlayOnShow = Media.SystemSounds.Exclamation
                    Case MessageBoxIcons.Question : box.PlayOnShow = Media.SystemSounds.Question
                End Select
                If (options And Windows.Forms.MessageBoxOptions.RightAlign) = Windows.Forms.MessageBoxOptions.RightAlign Then box.Options = box.Options Or MessageBoxOptions.AlignRight
                If (options And Windows.Forms.MessageBoxOptions.RtlReading) = Windows.Forms.MessageBoxOptions.RtlReading Then box.Options = box.Options Or MessageBoxOptions.Rtl
                Return ShowTemplate(box, owner)
            Catch ex As Exception When Not TypeOf ex Is TargetInvocationException
                Throw New TargetInvocationException(ResourcesT.Exceptions.ThereWasAnErrorInvokingMessageBoxSeeInnerExceptionForDetails, ex)
            End Try
        End Function
        ''' <summary>Performs modal dialog for WinForms-like functions with <see cref="Windows.Forms.MessageBoxIcon"/></summary>
        ''' <param name="Text">The text to display in the message box. </param>
        ''' <param name="Icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon"></see> values that specifies which icon to display in the message box. </param>
        ''' <param name="Options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions"></see> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
        ''' <param name="Owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window"></see> that will own the modal dialog box.</param>
        ''' <param name="Buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons"></see> values that specifies which buttons to display in the message box. </param>
        ''' <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton"></see> values the specifies the default button for the message box. </param>
        ''' <param name="Caption">The text to display in the title bar of the message box. </param>
        ''' <exception cref="InvalidEnumArgumentException">
        ''' <paramref name="buttons"/> is not member of <see cref="MessageBoxButtons"/> =or=
        ''' <paramref name="icon"/> is not member of <see cref="MessageBoxIcon"/> =or=
        ''' <paramref name="defaultButton"/> is not membember of <see cref="MessageBoxDefaultButton"/>
        ''' </exception>
        ''' <exception cref="TargetInvocationException">There was an error worink working with customized static properties such as <see cref="DefaultImplementation"/></exception>
        Private Shared Function ShowCore(ByVal owner As IWin32Window, ByVal [text] As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As Windows.Forms.MessageBoxIcon, ByVal defaultButton As MessageBoxDefaultButton, ByVal options As Windows.Forms.MessageBoxOptions) As DialogResult
            Return ShowCore(owner, text, caption, buttons, ConvertIconConstant(icon), defaultButton, options)
        End Function
#End Region
#End Region

#Region "Microsoft.VisualBasic.Interaction.MsgBox"
        ''' <summary>Displays a message in a dialog box, waits for the user to click a button, and then returns an integer indicating which button the user clicked.</summary>
        ''' <param name="Prompt">Required. String expression displayed as the message in the dialog box.</param>
        ''' <param name="Buttons">Optional. Numeric expression that is the sum of values specifying the number and type of buttons to display, the icon style to use, the identity of the default button, and the modality of the message box. If you omit Buttons, the default value is zero. Values <see cref="MsgBoxStyle.Critical"/>, <see cref="MsgBoxStyle.Exclamation"/> and <see cref="MsgBoxStyle.Question"/> are associated with appropriate <see cref="Media.SystemSounds"/>.</param>
        ''' <param name="Title">Optional. String expression displayed in the title bar of the dialog box. If you omit Title, the application name is placed in the title bar.</param>
        ''' <returns>The result of message box indicatin pressed button.</returns>
        ''' <exception cref="TargetInvocationException">There was an error working working with customized static properties such as <see cref="DefaultImplementation"/> or message box implementation failed.</exception>
        ''' <remarks>This function mimisc behaviour of the <see cref="Microsoft.VisualBasic.Interaction.MsgBox"/> function</remarks>
        Public Shared Function MsgBox(ByVal Prompt As Object, Optional ByVal Buttons As MsgBoxStyle = 0, Optional ByVal Title As Object = Nothing) As MsgBoxResult
            Try
                Dim box As New FakeBox With {.Prompt = Prompt.ToString, .Title = Title.ToString}
                box.Buttons.Clear()
                box.Buttons.AddRange(MessageBoxButton.GetButtons(Buttons))
                box.Icon = GetIconDelegate.Invoke(ConvertIconConstant(Buttons))
                Select Case Buttons And (MsgBoxStyle.DefaultButton1 Or MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.DefaultButton3)
                    Case MsgBoxStyle.DefaultButton1 : box.DefaultButton = 0
                    Case MsgBoxStyle.DefaultButton2 : box.DefaultButton = 1
                    Case MsgBoxStyle.DefaultButton3 : box.DefaultButton = 2
                    Case Else : box.DefaultButton = -1
                End Select
                Select Case Buttons And (MsgBoxStyle.Critical Or MsgBoxStyle.Exclamation Or MsgBoxStyle.Information Or MsgBoxStyle.Question)
                    Case MsgBoxStyle.Critical : box.PlayOnShow = Media.SystemSounds.Hand
                    Case MsgBoxStyle.Exclamation : box.PlayOnShow = Media.SystemSounds.Exclamation
                    Case MsgBoxStyle.Question : box.PlayOnShow = Media.SystemSounds.Question
                End Select
                If (Buttons And MsgBoxStyle.MsgBoxSetForeground) = MsgBoxStyle.MsgBoxSetForeground Then box.Options = box.Options Or MessageBoxOptions.BringToFront
                If (Buttons And MsgBoxStyle.MsgBoxRight) = MsgBoxStyle.MsgBoxRight Then box.Options = box.Options Or MessageBoxOptions.AlignRight
                If (Buttons And MsgBoxStyle.MsgBoxRtlReading) = MsgBoxStyle.MsgBoxRtlReading Then box.Options = box.Options Or MessageBoxOptions.Rtl
                Dim result As DialogResult = ShowTemplate(box)
                Select Case result
                    Case Windows.Forms.DialogResult.Abort : Return MsgBoxResult.Abort
                    Case Windows.Forms.DialogResult.Cancel : Return MsgBoxResult.Cancel
                    Case Windows.Forms.DialogResult.Ignore : Return MsgBoxResult.Ignore
                    Case Windows.Forms.DialogResult.No : Return MsgBoxResult.No
                    Case Windows.Forms.DialogResult.OK : Return MsgBoxResult.Ok
                    Case Windows.Forms.DialogResult.Retry : Return MsgBoxResult.Retry
                    Case Windows.Forms.DialogResult.Yes : Return MsgBoxResult.Yes
                    Case Else : Return result
                End Select
            Catch ex As Exception When Not TypeOf ex Is TargetInvocationException
                Throw New TargetInvocationException(ResourcesT.Exceptions.ThereWasAnErrorInvokingMessageBoxSeeInnerExceptionForDetails, ex)
            End Try
        End Function
#End Region

#Region "Geniue"
        ''' <summary>Displays modal message box with given prompt</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function Modal_P(ByVal Prompt$) As DialogResult
            Return Modal_PT(Prompt, CType(Nothing, String))
        End Function
        ''' <summary>Displays modal message box with given prompt and title</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function Modal_PT(ByVal Prompt$, ByVal Title$) As DialogResult
            Return Modal_PTB(Prompt, Title, MessageBoxButton.Buttons.OK)
        End Function
        ''' <summary>Displays modal message box with formated prompt</summary>
        ''' <param name="Prompt">Format string for promt to be shown to user</param>
        ''' <param name="arguments">Formating arguments for prompt. Arguments are placed in place of placeholders in <paramref name="Prompt"/> using the <see cref="String.Format"/> function.</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function ModalF_Pa(ByVal Prompt$, ByVal ParamArray arguments As Object()) As DialogResult
            Return Modal_PTWBIO(Prompt, CType(Nothing, String), arguments)
        End Function
        ''' <summary>Displays modal message box with formated prompt and given title</summary>
        ''' <param name="Prompt">Format string for promt to be shown to user</param>
        ''' <param name="arguments">Formating arguments for prompt. Arguments are placed in place of placeholders in <paramref name="Prompt"/> using the <see cref="String.Format"/> function.</param>
        ''' <param name="Title">Message box title</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function ModalF_PTa(ByVal Prompt$, ByVal Title$, ByVal ParamArray arguments As Object()) As DialogResult
            Return ModalF_PTBa(Prompt, Title, MessageBoxButton.Buttons.OK, arguments)
        End Function
        ''' <summary>Displays modal message box with given promt, title and buttons</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function Modal_PTB(ByVal Prompt$, ByVal Title$, ByVal Buttons As MessageBoxButton.Buttons) As DialogResult
            Return Modal_PTBI(Prompt, Title, Buttons, MessageBoxIcons.None)
        End Function
        ''' <summary>Displays modal message box with given prompt and buttons</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function Modal_PB(ByVal Prompt$, ByVal Buttons As MessageBoxButton.Buttons) As DialogResult
            Return Modal_PTB(Prompt, CType(Nothing, String), Buttons)
        End Function
        ''' <summary>Displays modal message box with formate prompt, given title an buttons</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="arguments">Formating arguments for prompt. Arguments are placed in place of placeholders in <paramref name="Prompt"/> using the <see cref="String.Format"/> function.</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function ModalF_PTBa(ByVal Prompt$, ByVal Title$, ByVal Buttons As MessageBoxButton.Buttons, ByVal ParamArray arguments As Object()) As DialogResult
            Return ModalF_PTBIa(Prompt, Title, Buttons, MessageBoxIcons.None, arguments)
        End Function
        ''' <summary>Displays modal message box with given prompt, title and icon</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Icon">Defines one of predefined icons to show to user. Actual image is obtained via <see cref="GetIconDelegate"/></param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function Modal_PTI(ByVal Prompt$, ByVal Title$, ByVal Icon As MessageBoxIcons) As DialogResult
            Return Modal_PTBI(Prompt, Title, MessageBoxButton.Buttons.OK, Icon)
        End Function
        ''' <summary>Displays modal message with given prompt, title, buttons and icon</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="Icon">Defines one of predefined icons to show to user. Actual image is obtained via <see cref="GetIconDelegate"/></param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function Modal_PTBI(ByVal Prompt$, ByVal Title$, ByVal Buttons As MessageBoxButton.Buttons, ByVal Icon As MessageBoxIcons) As DialogResult
            Return Modal_PTOBIS(Prompt, Title, MessageBoxOptions.AlignLeft, Buttons, Icon)
        End Function
        ''' <summary>Displays modal message box with formated prompt, given title and icon</summary>
        ''' <param name="Prompt">Format string for promt to be shown to user</param>
        ''' <param name="arguments">Formating arguments for prompt. Arguments are placed in place of placeholders in <paramref name="Prompt"/> using the <see cref="String.Format"/> function.</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Icon">Defines one of predefined icons to show to user. Actual image is obtained via <see cref="GetIconDelegate"/>. Some icons are associated with sound (<see cref="GetAssociatedSound"/>).</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function ModalF_PTIa(ByVal Prompt$, ByVal Title$, ByVal Icon As MessageBoxIcons, ByVal ParamArray arguments As Object()) As DialogResult
            Return ModalF_PTBIa(Prompt, Title, MessageBoxButton.Buttons.OK, Icon, arguments)
        End Function
        ''' <summary>Displays modal message with formated prompt, given title, buttons and icon</summary>
        ''' <param name="Prompt">Format string for promt to be shown to user</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="Icon">Defines one of predefined icons to show to user. Actual image is obtained via <see cref="GetIconDelegate"/> Some icons are associated with sound (<see cref="GetAssociatedSound"/>).</param>
        ''' <returns>Indicates button clicked by user</returns>
        ''' <param name="arguments">Formating arguments for prompt. Arguments are placed in place of placeholders in <paramref name="Prompt"/> using the <see cref="String.Format"/> function.</param>
        Public Shared Function ModalF_PTBIa(ByVal Prompt$, ByVal Title$, ByVal Buttons As MessageBoxButton.Buttons, ByVal Icon As MessageBoxIcons, ByVal ParamArray arguments As Object()) As DialogResult
            Return ModalF_PTOBIa(Prompt, Title, MessageBoxOptions.AlignLeft, Buttons, Icon, arguments)
        End Function
        ''' <summary>Displays modal message box with given prompt, tile and options. Optinally also buttons and icon.</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="Icon">Defines one of predefined icons to show to user. Actual image is obtained via <see cref="GetIconDelegate"/></param>
        ''' <param name="Options">Options that controls messagebox layout and behaviour</param>
        ''' <param name="Sound">Sound to be played when message box is shown. If null, it is chosen automatically.</param>
        Public Shared Function Modal_PTOBIS(ByVal Prompt$, ByVal Title$, ByVal Options As MessageBoxOptions, Optional ByVal Buttons As MessageBoxButton.Buttons = MessageBoxButton.Buttons.OK, Optional ByVal Icon As MessageBoxIcons = MessageBoxIcons.None, Optional ByVal Sound As MediaT.Sound = Nothing) As DialogResult
            If Sound Is Nothing Then Sound = GetAssociatedSound(Icon)
            Return Modal_PTOIBS(Prompt, Title, Options, GetIconDelegate.Invoke(Icon), Buttons, Sound)
        End Function
        ''' <summary>Gets sound associated with given icon</summary>
        ''' <param name="Icon">Icon to get sound for</param>
        ''' <returns>An instance of <see cref="MediaT.SystemSoundPlayer"/> if there is sound associated with <paramref name="Icon"/>; null otherwise</returns>
        ''' <remarks>Current associatins are:
        ''' <list type="table"><listheader><term>Icon</term><description>Sound</description></listheader>
        ''' <item><see cref="MessageBoxIcons.Asterisk"/><description><see cref="Media.SystemSounds.Asterisk"/></description></item>
        ''' <item><term><see cref="MessageBoxIcons.[Error]"/>, <see cref="MessageBoxIcons.Hand"/>, <see cref="MessageBoxIcons.[Stop]"/></term><description><see cref="Media.SystemSounds.Hand"/></description></item>
        ''' <item><term><see cref="MessageBoxIcons.Question"/></term><description><see cref="Media.SystemSounds.Question"/></description></item>
        ''' <item><term><see cref="MessageBoxIcons.Exclamation"/>, <see cref="MessageBoxIcons.Warning"/></term><see cref="Media.SystemSounds.Exclamation"/></item>
        ''' </list>
        ''' </remarks>
        Protected Shared Function GetAssociatedSound(ByVal Icon As MessageBoxIcons) As MediaT.SystemSoundPlayer
            Select Case Icon
                Case MessageBoxIcons.Asterisk : Return Media.SystemSounds.Asterisk
                Case MessageBoxIcons.Error, MessageBoxIcons.Hand, MessageBoxIcons.Stop : Return Media.SystemSounds.Hand
                Case MessageBoxIcons.Question : Return Media.SystemSounds.Question
                Case MessageBoxIcons.Exclamation, MessageBoxIcons.Warning : Return Media.SystemSounds.Exclamation
                Case Else : Return Nothing
            End Select
        End Function
        ''' <summary>Displays modal message box with formated prompt, given title and options </summary>
        ''' <param name="Prompt">Format string for promt to be shown to user</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Options">Options that controls messagebox layout and behaviour</param>
        ''' <param name="arguments">Formating arguments for prompt. Arguments are placed in place of placeholders in <paramref name="Prompt"/> using the <see cref="String.Format"/> function.</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function ModalF_PTOa(ByVal Prompt$, ByVal Title$, ByVal Options As MessageBoxOptions, ByVal ParamArray arguments As Object()) As DialogResult
            Return ModalF_PTOBa(Prompt, Title, Options, MessageBoxButton.Buttons.OK, arguments)
        End Function
        ''' <summary>Displays modal message box with formated prompt, given title, options and buttons</summary>
        ''' <param name="Prompt">Format string for promt to be shown to user</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Options">Options that controls messagebox layout and behaviour</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="arguments">Formating arguments for prompt. Arguments are placed in place of placeholders in <paramref name="Prompt"/> using the <see cref="String.Format"/> function.</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function ModalF_PTOBa(ByVal Prompt$, ByVal Title$, ByVal Options As MessageBoxOptions, ByVal Buttons As MessageBoxButton.Buttons, ByVal ParamArray arguments As Object()) As DialogResult
            Return ModalF_PTOBIa(Prompt, Title, Options, Buttons, MessageBoxIcons.None, arguments)
        End Function
        ''' <summary>Displays modal message box with formated prompt, given title, options and predefined icon</summary>
        ''' <param name="Prompt">Format string for promt to be shown to user</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Options">Options that controls messagebox layout and behaviour</param>
        ''' <param name="Icon">Defines one of predefined icons to show to user. Actual image is obtained via <see cref="GetIconDelegate"/> Some icons are associated with sound (<see cref="GetAssociatedSound"/>).</param>
        ''' <param name="arguments">Formating arguments for prompt. Arguments are placed in place of placeholders in <paramref name="Prompt"/> using the <see cref="String.Format"/> function.</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function ModalF_PTOIa(ByVal Prompt$, ByVal Title$, ByVal Options As MessageBoxOptions, ByVal Icon As MessageBoxIcons, ByVal ParamArray arguments As Object()) As DialogResult
            Return ModalF_PTOBIa(Prompt, Title, Options, MessageBoxButton.Buttons.OK, Icon, arguments)
        End Function
        ''' <summary>Displays modal message box with formated prompt, given title, options, buttons and predefined icon</summary>
        ''' <param name="Prompt">Format string for promt to be shown to user</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Options">Options that controls messagebox layout and behaviour</param>
        ''' <param name="Icon">Defines one of predefined icons to show to user. Actual image is obtained via <see cref="GetIconDelegate"/>. Some icons are associated with sound (<see cref="GetAssociatedSound"/>).</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="arguments">Formating arguments for prompt. Arguments are placed in place of placeholders in <paramref name="Prompt"/> using the <see cref="String.Format"/> function.</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function ModalF_PTOBIa(ByVal Prompt$, ByVal Title$, ByVal Options As MessageBoxOptions, ByVal Buttons As MessageBoxButton.Buttons, ByVal Icon As MessageBoxIcons, ByVal ParamArray arguments As Object()) As DialogResult
            Return ModalF_PTOBISa(Prompt, Title, Options, Buttons, GetIconDelegate.Invoke(Icon), GetAssociatedSound(Icon), arguments)
        End Function
#Region "Custom Icon"
        ''' <summary>Displays modal message with given prompt, title, buttons and custom icon</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function Modal_PTBI(ByVal Prompt$, ByVal Title$, ByVal Buttons As MessageBoxButton.Buttons, ByVal Icon As Image) As DialogResult
            Return Modal_PTMBIOW(Prompt, Title, MessageBoxOptions.AlignLeft, Buttons, Icon)
        End Function
        ''' <summary>Displays modal message box with formated prompt, given title and custom icon</summary>
        ''' <param name="Prompt">Format string for promt to be shown to user</param>
        ''' <param name="arguments">Formating arguments for prompt. Arguments are placed in place of placeholders in <paramref name="Prompt"/> using the <see cref="String.Format"/> function.</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function ModalF_PTIa(ByVal Prompt$, ByVal Title$, ByVal Icon As Image, ByVal ParamArray arguments As Object()) As DialogResult
            Return ModalF_PTBIa(Prompt, Title, MessageBoxButton.Buttons.OK, Icon, arguments)
        End Function
        ''' <summary>Displays modal message with formated prompt, given title, buttons and custom  icon</summary>
        ''' <param name="Prompt">Format string for promt to be shown to user</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <returns>Indicates button clicked by user</returns>
        ''' <param name="arguments">Formating arguments for prompt. Arguments are placed in place of placeholders in <paramref name="Prompt"/> using the <see cref="String.Format"/> function.</param>
        Public Shared Function ModalF_PTBIa(ByVal Prompt$, ByVal Title$, ByVal Buttons As MessageBoxButton.Buttons, ByVal Icon As Image, ByVal ParamArray arguments As Object()) As DialogResult
            Return ModalF_PTOBIa(Prompt, Title, MessageBoxOptions.AlignLeft, Buttons, Icon, arguments)
        End Function
        ''' <summary>Displays modal message box with given prompt, tile and options and custom icon. Optinally also buttons.</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <param name="Options">Options that controls messagebox layout and behaviour</param>
        ''' <param name="Sound">Sound to be played when message box is shown</param>
        Public Shared Function Modal_PTOIBS(ByVal Prompt$, ByVal Title$, ByVal Options As MessageBoxOptions, ByVal Icon As Image, Optional ByVal Buttons As MessageBoxButton.Buttons = MessageBoxButton.Buttons.OK, Optional ByVal Sound As MediaT.Sound = Nothing) As DialogResult
            Dim box As New FakeBox With {.Prompt = Prompt, .Title = Title, .Options = Options, .Icon = Icon, .PlayOnShow = Sound}
            box.Buttons.Clear()
            box.Buttons.AddRange(MessageBoxButton.GetButtons(Buttons))
            Return ShowTemplate(box)
        End Function
        ''' <summary>Displays modal message box with formated prompt, given title, options and custom icon</summary>
        ''' <param name="Prompt">Format string for promt to be shown to user</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Options">Options that controls messagebox layout and behaviour</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <param name="arguments">Formating arguments for prompt. Arguments are placed in place of placeholders in <paramref name="Prompt"/> using the <see cref="String.Format"/> function.</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function ModalF_PTOIa(ByVal Prompt$, ByVal Title$, ByVal Options As MessageBoxOptions, ByVal Icon As Image, ByVal ParamArray arguments As Object()) As DialogResult
            Return ModalF_PTOBIa(Prompt, Title, Options, MessageBoxButton.Buttons.OK, Icon, arguments)
        End Function
        ''' <summary>Displays modal message box with formated prompt, given title, options and custom icon</summary>
        ''' <param name="Prompt">Format string for promt to be shown to user</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Options">Options that controls messagebox layout and behaviour</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"></paramref> can be null. Some icons are associated with sound (<see cref="GetAssociatedSound"/>).</param>
        ''' <param name="arguments">Formating arguments for prompt. Arguments are placed in place of placeholders in <paramref name="Prompt"></paramref> using the <see cref="String.Format"></see> function.</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function ModalF_PTOISa(ByVal Prompt$, ByVal Title$, ByVal Options As MessageBoxOptions, ByVal Icon As Image, ByVal Sound As MediaT.Sound, ByVal ParamArray arguments As Object()) As DialogResult
            Return ModalF_PTOa(Prompt, Title, Options, MessageBoxButtons.OK, Icon, Sound, arguments)
        End Function
        ''' <summary>Displays modal message box with formated prompt, given title, options, buttons and custom icon</summary>
        ''' <param name="Prompt">Format string for promt to be shown to user</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Options">Options that controls messagebox layout and behaviour</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="arguments">Formating arguments for prompt. Arguments are placed in place of placeholders in <paramref name="Prompt"/> using the <see cref="String.Format"/> function.</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function ModalF_PTOBIa(ByVal Prompt$, ByVal Title$, ByVal Options As MessageBoxOptions, ByVal Buttons As MessageBoxButton.Buttons, ByVal Icon As Image, ByVal ParamArray arguments As Object()) As DialogResult
            Return ModalF_PTOBISa(Prompt$, Title$, Options, Buttons, Icon, DirectCast(Nothing, MediaT.Sound), arguments)
        End Function
        ''' <summary>Displays modal message box with formated prompt, given title, options, buttons and custom icon</summary>
        ''' <param name="Prompt">Format string for promt to be shown to user</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Options">Options that controls messagebox layout and behaviour</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="arguments">Formating arguments for prompt. Arguments are placed in place of placeholders in <paramref name="Prompt"/> using the <see cref="String.Format"/> function.</param>
        ''' <param name="Sound">Sound to be played when message box is shown</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function ModalF_PTOBISa(ByVal Prompt$, ByVal Title$, ByVal Options As MessageBoxOptions, ByVal Buttons As MessageBoxButton.Buttons, ByVal Icon As Image, ByVal Sound As MediaT.Sound, ByVal ParamArray arguments As Object()) As DialogResult
            Dim box As New FakeBox With {.Prompt = String.Format(Prompt, arguments), .Title = Title, .Options = Options, .Icon = Icon, .PlayOnShow = Sound}
            box.Buttons.Clear()
            box.Buttons.AddRange(MessageBoxButton.GetButtons(Buttons))
            Return ShowTemplate(box)
        End Function
        ''' <summary>Displays modal message box with given prompt, title and custom icon</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function Modal_PTI(ByVal Prompt$, ByVal Title$, ByVal Icon As Image) As DialogResult
            Return Modal_PTBI(Prompt, Title, MessageBoxButton.Buttons.OK, Icon)
        End Function
#End Region
        ''' <summary>Displays modal message box with given prompt, title, custom icon and custom buttons</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <param name="Buttons">Custom buttons. Each button should have different <see cref="MessageBoxButton.Result"/>, so you can distinguish which button was clicked.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Buttons"/> is null</exception>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function Modal_PTIB(ByVal Prompt$, ByVal Title$, ByVal Icon As Image, ByVal ParamArray Buttons As MessageBoxButton()) As DialogResult
            Return Modal_PTOIB(Prompt, Title, MessageBoxOptions.AlignLeft, Icon, Buttons)
        End Function
        ''' <summary>Displays modal message box with given prompt, title, options, custom icon and custom buttons</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <param name="Buttons">Custom buttons. Each button should have different <see cref="MessageBoxButton.Result"/>, so you can distinguish which button was clicked.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Buttons"/> is null</exception>
        ''' <param name="Options">Options that controls messagebox layout and behaviour</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function Modal_PTOIB(ByVal Prompt$, ByVal Title$, ByVal Options As MessageBoxOptions, ByVal Icon As Image, ByVal ParamArray Buttons As MessageBoxButton()) As DialogResult
            Dim box As New FakeBox With {.Prompt = Prompt, .Title = Title, .Options = Options, .Icon = Icon}
            box.SetButtons(Buttons)
            Return ShowTemplate(box)
        End Function
        ''' <summary>Display modal message box with given prompt, title and owner. Optionally specifies buttons, icon and options</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <param name="Options">Options that controls messagebox layout and behaviour</param>
        ''' <returns>Indicates button clicked by user</returns>
        ''' <param name="Owner">The window message box window will be modal to (can be null)</param>
        Public Shared Function Modal_PTWBIO(ByVal Prompt$, ByVal Title$, ByVal Owner As IWin32Window, Optional ByVal Buttons As MessageBoxButton.Buttons = MessageBoxButton.Buttons.OK, Optional ByVal Icon As Image = Nothing, Optional ByVal Options As MessageBoxOptions = MessageBoxOptions.AlignLeft) As DialogResult
            Dim box As New FakeBox With { _
                .Prompt = Prompt, .title = Title, _
                .Options = Options, .icon = Icon}
            box.SetButtons(Buttons)
            ShowTemplate(box, Owner)
        End Function
        ''' <summary>Display modal message box with formated promt, given title, owner, buttons, icon</summary>
        ''' <param name="Prompt">Format string for promt to be shown to user</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Owner">The window message box window will be modal to (can be null)</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="arguments">Formating arguments for prompt. Arguments are placed in place of placeholders in <paramref name="Prompt"/> using the <see cref="String.Format"/> function.</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function ModalF_PTWBIa(ByVal Prompt$, ByVal Title$, ByVal Owner As IWin32Window, ByVal Buttons As MessageBoxButton.Buttons, ByVal Icon As Image, ByVal ParamArray arguments As Object()) As DialogResult
            Return Modal_PTWBIO(String.Format(Prompt, arguments), Title, Owner, Buttons, Icon)
        End Function
        ''' <summary>Dsiplays modal message box with formated prompt, given title and owner</summary>
        ''' <param name="Prompt">Format string for promt to be shown to user</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="arguments">Formating arguments for prompt. Arguments are placed in place of placeholders in <paramref name="Prompt"/> using the <see cref="String.Format"/> function.</param>
        ''' <param name="Owner">The window message box window will be modal to (can be null)</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function ModalF_PTWa(ByVal Prompt$, ByVal Title$, ByVal Owner As IWin32Window, ByVal ParamArray arguments As Object()) As DialogResult
            ModalF_PTWBIa(Prompt, Title, Owner, MessageBoxButton.Buttons.OK, CType(Nothing, Image), arguments)
        End Function
        ''' <summary>Displays autoclosing modal message box</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Timer">Time after which the message box will close automatically</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <param name="Options">Options that controls messagebox layout and behaviour</param>
        ''' <param name="Owner">The window message box window will be modal to (can be null)</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function Modal_PTMBIOW(ByVal Prompt$, ByVal Title$, ByVal Timer As TimeSpan, Optional ByVal Buttons As MessageBoxButton.Buttons = MessageBoxButton.Buttons.OK, Optional ByVal Icon As Image = Nothing, Optional ByVal Options As MessageBoxOptions = MessageBoxOptions.AlignLeft, Optional ByVal Owner As IWin32Window = Nothing) As DialogResult
            Dim box As New FakeBox With { _
                .Prompt = Prompt, .title = Title, _
                .Options = Options, .icon = Icon, .Timer = Timer}
            box.SetButtons(Buttons)
            ShowTemplate(box, Owner)
        End Function
        ''' <summary>Displays autoclosing modal message box</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Timer">Time (in seconds) after which the message box will close automatically</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <param name="Options">Options that controls messagebox layout and behaviour</param>
        ''' <param name="Owner">The window message box window will be modal to (can be null)</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function Modal_PTMBIOW(ByVal Prompt$, ByVal Title$, ByVal Timer As Integer, Optional ByVal Buttons As MessageBoxButton.Buttons = MessageBoxButton.Buttons.OK, Optional ByVal Icon As Image = Nothing, Optional ByVal Options As MessageBoxOptions = MessageBoxOptions.AlignLeft, Optional ByVal Owner As IWin32Window = Nothing) As DialogResult
            Return Modal_PTMBIOW(Prompt, Title, TimeSpan.FromSeconds(Timer), Buttons, Icon, Options, Owner)
        End Function
#End Region
#Region "ModalEx"
        ''' <summary>Displays modal messagebox with given prompt, title, items and optionally icon, options, owner, timer, show handler and sound</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Items">Items to be shown in message box. Place items of type <see cref="MessageBoxButton"/>, <see cref="MessageBoxCheckBox"/>, <see cref="MessageBoxRadioButton"/> and <see cref="String"/> here. <see cref="String"/> items are placed inside <see cref="ComboBox"/>. Items of other types are ignored.</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <param name="Options">Options that controls messagebox layout and behaviour</param>
        ''' <param name="Owner">The window message box window will be modal to (can be null)</param>
        ''' <param name="Timer">Time (in seconds) after which the message box will close automatically</param>
        ''' <param name="ShownHandler">Delegate that will handle the <see cref="Shown"/> event of message box</param>
        ''' <param name="Sound">Sound to be played whne message box is shown.</param>
        ''' <returns>Instance of message box. The instance is alredy closed when this function returns.</returns>
        Public Shared Function ModalEx_PTEIOWMHS(ByVal Prompt$, ByVal Title$, ByVal Items As IEnumerable(Of Object), Optional ByVal Icon As Image = Nothing, Optional ByVal Options As MessageBoxOptions = MessageBoxOptions.AlignLeft, Optional ByVal Owner As IWin32Window = Nothing, Optional ByVal Timer As Integer = 0, Optional ByVal ShownHandler As EventHandler(Of MessageBox, EventArgs) = Nothing, Optional ByVal Sound As MediaT.Sound = Nothing) As MessageBox
            Dim box As New FakeBox With {.Options = Options, .Prompt = Prompt, .Title = Title, .Timer = TimeSpan.FromSeconds(Timer), .PlayOnShow = Sound}
            box.Buttons.Clear()
            box.Buttons.AddRange(Items.OfType(Of MessageBoxButton))
            box.CheckBoxes.AddRange(Items.OfType(Of MessageBoxCheckBox))
            box.Radios.AddRange(Items.OfType(Of MessageBoxRadioButton))
            Dim Strings = Items.OfType(Of String)()
            If Not Strings.IsEmpty Then
                box.ComboBox = New MessageBoxComboBox
                box.ComboBox.Items.AddRange(Strings)
            End If
            Dim ret = InitializeDafault(box)
            If ShownHandler IsNot Nothing Then AddHandler ret.Shown, ShownHandler
            ret.ShowDialog(Owner)
            Return ret
        End Function
        ''' <summary>Displays modal messagebox with given prompt, title, items, icon, options, owner, timer and show handler</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Items">Items to be shown in message box. Place items of type <see cref="MessageBoxButton"/>, <see cref="MessageBoxCheckBox"/>, <see cref="MessageBoxRadioButton"/> and <see cref="String"/> here. <see cref="String"/> items are placed inside <see cref="ComboBox"/>. Items of other types are ignored.</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <param name="Options">Options that controls messagebox layout and behaviour</param>
        ''' <param name="Owner">The window message box window will be modal to (can be null)</param>
        ''' <param name="Timer">Time (in seconds) after which the message box will close automatically</param>
        ''' <param name="ShownHandler">Delegate that will handle the <see cref="Shown"/> event of message box</param>
        ''' <returns>Instance of message box. The instance is alredy closed when this function returns.</returns>
        Public Shared Function ModalEx_PTIOWMHE(ByVal Prompt$, ByVal Title$, ByVal Icon As Image, ByVal Options As MessageBoxOptions, ByVal Owner As IWin32Window, ByVal Timer As Integer, ByVal ShownHandler As EventHandler(Of MessageBox, EventArgs), ByVal ParamArray Items As Object()) As MessageBox
            '              Prompt, Title, Items,                                   [Icon], [Options],[Owner], [Timer], [ShownHandler]
            Return ModalEx_PTEIOWMHS(Prompt, Title, DirectCast(Items, IEnumerable(Of Object)), Icon, Options, Owner, Timer, ShownHandler)
        End Function
        ''' <summary>Displays modal message box with given prompt, title, icon, owner, timer, show ahndler and items</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Items">Items to be shown in message box. Place items of type <see cref="MessageBoxButton"/>, <see cref="MessageBoxCheckBox"/>, <see cref="MessageBoxRadioButton"/> and <see cref="String"/> here. <see cref="String"/> items are placed inside <see cref="ComboBox"/>. Items of other types are ignored.</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <param name="Owner">The window message box window will be modal to (can be null)</param>
        ''' <param name="Timer">Time (in seconds) after which the message box will close automatically</param>
        ''' <param name="ShownHandler">Delegate that will handle the <see cref="Shown"/> event of message box</param>
        ''' <returns>Instance of message box. The instance is alredy closed when this function returns.</returns>
        Public Shared Function ModalEx_PTIWMHE(ByVal Prompt$, ByVal Title$, ByVal Icon As Image, ByVal Owner As IWin32Window, ByVal Timer As Integer, ByVal ShownHandler As EventHandler(Of MessageBox, EventArgs), ByVal ParamArray Items As Object()) As MessageBox
            '              Prompt, Title, Items,                                   [Icon], [Options],                 [Owner], [Timer], [ShownHandler]
            Return ModalEx_PTEIOWMHS(Prompt, Title, DirectCast(Items, IEnumerable(Of Object)), Icon, MessageBoxOptions.AlignLeft, Owner, Timer, ShownHandler)
        End Function
        ''' <summary>Displays modal message box with given prompt, title, icon, owner, timer, show ahndler and buttons</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Buttons">Custom buttons. Each button should have different <see cref="MessageBoxButton.Result"/>, so you can distinguish which button was clicked.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Buttons"/> is null</exception>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <param name="Owner">The window message box window will be modal to (can be null)</param>
        ''' <param name="Timer">Time (in seconds) after which the message box will close automatically</param>
        ''' <param name="ShownHandler">Delegate that will handle the <see cref="Shown"/> event of message box</param>
        ''' <returns>Instance of message box. The instance is alredy closed when this function returns.</returns>
        Public Shared Function ModalEx_PTIWMHB(ByVal Prompt$, ByVal Title$, ByVal Icon As Image, ByVal Owner As IWin32Window, ByVal Timer As Integer, ByVal ShownHandler As EventHandler(Of MessageBox, EventArgs), ByVal ParamArray Buttons As MessageBoxButton()) As MessageBox
            If Buttons Is Nothing Then Throw New ArgumentNullException("Buttons")
            '              Prompt, Title, Items,                          [Icon], [Options],                 [Owner], [Timer], [ShownHandler]
            Return ModalEx_PTEIOWMHS(Prompt, Title, New Wrapper(Of Object)(Buttons), Icon, MessageBoxOptions.AlignLeft, Owner, Timer, ShownHandler)
        End Function
        ''' <summary>Displays modal message box with given prompt, title, icon, owner and items</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Items">Items to be shown in message box. Place items of type <see cref="MessageBoxButton"/>, <see cref="MessageBoxCheckBox"/>, <see cref="MessageBoxRadioButton"/> and <see cref="String"/> here. <see cref="String"/> items are placed inside <see cref="ComboBox"/>. Items of other types are ignored.</param>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <param name="Owner">The window message box window will be modal to (can be null)</param>
        ''' <returns>Instance of message box. The instance is alredy closed when this function returns.</returns>
        Public Shared Function ModalEx_PTIWS(ByVal Prompt$, ByVal Title$, ByVal Icon As Image, ByVal Owner As IWin32Window, ByVal ParamArray Items As Object()) As MessageBox
            '              Prompt, Title, Items,                                    [Icon], [Options],                 [Owner], [Timer], [ShownHandler]
            Return ModalEx_PTEIOWMHS(Prompt, Title, DirectCast(Items, IEnumerable(Of Object)), Icon, MessageBoxOptions.AlignLeft, Owner, 0, Nothing)
        End Function
        ''' <summary>Displays modal message box with given prompt, title, icon and owner and buttons</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Buttons">Custom buttons. Each button should have different <see cref="MessageBoxButton.Result"/>, so you can distinguish which button was clicked.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Buttons"/> is null</exception>
        ''' <param name="Icon">Icon that will be shown on messagebox. Default preffered size is 64×64 px (can be changed in derived class). <paramref name="Icon"/> can be null.</param>
        ''' <param name="Owner">The window message box window will be modal to (can be null)</param>
        ''' <returns>Instance of message box. The instance is alredy closed when this function returns.</returns>
        Public Shared Function ModalEx_PTIWB(ByVal Prompt$, ByVal Title$, ByVal Icon As Image, ByVal Owner As IWin32Window, ByVal ParamArray Buttons As MessageBoxButton()) As MessageBox
            If Buttons Is Nothing Then Throw New ArgumentNullException("Buttons")
            '              Prompt, Title, Items,                          [Icon], [Options],                 [Owner], [Timer], [ShownHandler]
            Return ModalEx_PTEIOWMHS(Prompt, Title, New Wrapper(Of Object)(Buttons), Icon, MessageBoxOptions.AlignLeft, Owner, 0, Nothing)
        End Function
        ''' <summary>Displays modal message box with given prompt, title and items</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Items">Items to be shown in message box. Place items of type <see cref="MessageBoxButton"/>, <see cref="MessageBoxCheckBox"/>, <see cref="MessageBoxRadioButton"/> and <see cref="String"/> here. <see cref="String"/> items are placed inside <see cref="ComboBox"/>. Items of other types are ignored.</param>
        ''' <returns>Instance of message box. The instance is alredy closed when this function returns.</returns>
        Public Shared Function ModalEx_PTS(ByVal Prompt$, ByVal Title$, ByVal ParamArray Items As Object()) As MessageBox
            '              Prompt, Title, Items,                                    [Icon], [Options],                 [Owner], [Timer], [ShownHandler]
            Return ModalEx_PTEIOWMHS(Prompt, Title, DirectCast(Items, IEnumerable(Of Object)), Nothing, MessageBoxOptions.AlignLeft, Nothing, 0, Nothing)
        End Function
        ''' <summary>Displays modal message box with given prompt, title and buttons</summary>
        ''' <param name="Prompt">Prompt to be shown</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Buttons">Custom buttons. Each button should have different <see cref="MessageBoxButton.Result"/>, so you can distinguish which button was clicked.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Buttons"/> is null</exception>
        ''' <returns>Instance of message box. The instance is alredy closed when this function returns.</returns>
        Public Shared Function ModalEx_PTB(ByVal Prompt$, ByVal Title$, ByVal ParamArray Buttons As MessageBoxButton()) As MessageBox
            If Buttons Is Nothing Then Throw New ArgumentNullException("Buttons")
            '              Prompt, Title, Items,                          [Icon], [Options],                 [Owner], [Timer], [ShownHandler]
            Return ModalEx_PTEIOWMHS(Prompt, Title, New Wrapper(Of Object)(Buttons), Nothing, MessageBoxOptions.AlignLeft, Nothing, 0, Nothing)
        End Function
#End Region
#Region "Error"
        ''' <summary>Displays modal message box with information about <see cref="Exception"/></summary>
        ''' <param name="ex">Exception to show <see cref="Exception.Message"/> of</param>
        ''' <returns>Indicates button clicked by user</returns>
        ''' <remarks>Title will contain <see cref="Type.Name"/> of exception</remarks>
        Public Shared Function [Error_X](ByVal ex As Exception) As DialogResult
            Return Modal_PTI(ex.Message, ex.GetType.Name, MessageBoxIcons.Error)
        End Function
        ''' <summary>Displays modal message box with information about <see cref="Exception"/> and custom title</summary>
        ''' <param name="ex">Exception to show <see cref="Exception.Message"/> of</param>
        ''' <param name="Title">Message box title</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function [Error_XT](ByVal ex As Exception, ByVal Title$) As DialogResult
            Return Modal_PTI(ex.Message, Title, MessageBoxIcons.Error)
        End Function

        ''' <summary>Displays modal message box with information about <see cref="Exception"/> with given title and owner</summary>
        ''' <param name="ex">Exception to show <see cref="Exception.Message"/> of</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Owner">The window message box window will be modal to (can be null)</param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function [Error_XTW](ByVal ex As Exception, ByVal Title$, ByVal Owner As IWin32Window) As DialogResult
            Return Modal_PTWBIO(ex.Message, Title, Owner, MessageBoxIcons.Error)
        End Function
        ''' <summary>Displays modal message box with information about <see cref="Exception"/></summary>
        ''' <param name="ex">Exception to show <see cref="Exception.Message"/> of</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="Icon">Defines one of predefined icons to show to user. Actual image is obtained via <see cref="GetIconDelegate"/></param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function [Error_XBI](ByVal ex As Exception, ByVal Buttons As MessageBoxButton.Buttons, Optional ByVal Icon As MessageBoxIcons = MessageBoxIcons.Error) As DialogResult
            Return Modal_PTOBIS(ex.Message, ex.GetType.Name, Buttons, Buttons, MessageBoxIcons.Error)
        End Function
        ''' <summary>Displays modal message box with information about <see cref="Exception"/></summary>
        ''' <param name="ex">Exception to show <see cref="Exception.Message"/> of</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="Icon">Defines one of predefined icons to show to user. Actual image is obtained via <see cref="GetIconDelegate"/></param>
        ''' <returns>Indicates button clicked by user</returns>
        Public Shared Function [Error_XTBI](ByVal ex As Exception, ByVal Title$, ByVal Buttons As MessageBoxButton.Buttons, Optional ByVal Icon As MessageBoxIcons = MessageBoxIcons.Error) As DialogResult
            Return Modal_PTI(ex.Message, Title, MessageBoxIcons.Error)
        End Function
        ''' <summary>Displays modal message box with information about <see cref="Exception"/></summary>
        ''' <param name="ex">Exception to show <see cref="Exception.Message"/> of</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="Icon">Defines one of predefined icons to show to user. Actual image is obtained via <see cref="GetIconDelegate"/></param>
        ''' <returns>Indicates button clicked by user</returns>
        ''' <param name="Owner">The window message box window will be modal to (can be null)</param>
        Public Shared Function [Error_XBWI](ByVal ex As Exception, ByVal Buttons As MessageBoxButton.Buttons, ByVal Owner As IWin32Window, Optional ByVal Icon As MessageBoxIcons = MessageBoxIcons.Error) As DialogResult
            Return ModalF_PTWa(ex.Message, ex.GetType.Name, Owner, Buttons, MessageBoxIcons.Error)
        End Function
        ''' <summary>Displays modal message box with information about <see cref="Exception"/></summary>
        ''' <param name="ex">Exception to show <see cref="Exception.Message"/> of</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="Icon">Defines one of predefined icons to show to user. Actual image is obtained via <see cref="GetIconDelegate"/></param>
        ''' <returns>Indicates button clicked by user</returns>
        ''' <param name="Owner">The window message box window will be modal to (can be null)</param>
        Public Shared Function [Error_XTBWI](ByVal ex As Exception, ByVal Title$, ByVal Buttons As MessageBoxButton.Buttons, ByVal Owner As IWin32Window, Optional ByVal Icon As MessageBoxIcons = MessageBoxIcons.Error) As DialogResult
            Return ModalF_PTWa(ex.Message, Title, Owner, Buttons, MessageBoxIcons.Error)
        End Function
        ''' <summary>Displays modal message box with information about <see cref="Exception"/></summary>
        ''' <param name="ex">Exception to show <see cref="Exception.Message"/> of</param>
        ''' <param name="Title">Message box title</param>
        ''' <param name="Buttons">Defines which buttons will be available to user</param>
        ''' <param name="Icon">Defines one of predefined icons to show to user. Actual image is obtained via <see cref="GetIconDelegate"/></param>
        ''' <returns>Indicates button clicked by user</returns>
        ''' <param name="Owner">The window message box window will be modal to (can be null)</param>
        ''' <param name="Prompt">Prompt to be shown</param>
        Public Shared Function [Error_XPTIBWO](ByVal ex As Exception, ByVal Prompt$, ByVal Title$, Optional ByVal Icon As MessageBoxIcons = MessageBoxIcons.Error, Optional ByVal Buttons As MessageBoxButton.Buttons = MessageBoxButton.Buttons.OK, Optional ByVal Owner As IWin32Window = Nothing, Optional ByVal Options As MessageBoxOptions = MessageBoxOptions.AlignLeft) As DialogResult
            Return ModalF_PTWa(Prompt & vbCrLf & ex.Message, Title, Owner, Buttons, Icon, Options)
        End Function
#End Region
#End Region
#Region "Other methods"
        ''' <summary>Replaces <see cref="Buttons"/> with given buttons</summary>
        ''' <param name="Buttons">New buttons</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Buttons"/> is null</exception>
        Public Sub SetButtons(ByVal ParamArray Buttons As MessageBoxButton())
            If Buttons Is Nothing Then Throw New ArgumentNullException("Buttons")
            Me.Buttons.Clear()
            Me.Buttons.AddRange(Buttons)
        End Sub
        ''' <summary>Replaces <see cref="Buttons"/> with buttons created from their <see cref="MessageBoxButton.Buttons"/> specification</summary>
        ''' <param name="Buttons">Indicates buttons to create</param>
        Public Sub SetButtons(ByVal Buttons As MessageBoxButton.Buttons)
            Me.Buttons.Clear()
            Me.Buttons.AddRange(MessageBoxButton.GetButtons(Buttons))
        End Sub
        ''' <summary>When overriden in derived class gets text which contains Accesskey marker (like &amp; in WinForms or _ in WPF)</summary>
        ''' <param name="Text">Text (if it contains character used as accesskey markers they must be escaped)</param>
        ''' <param name="AccessKey">Char representing accesskey (if char is not in <paramref name="Text"/> no accesskey marker should be inserted)</param>
        ''' <returns><paramref name="Text"/> with accesskey denoted in it; or <paramref name="Text"/> if platfrom derived class implements messagebox for does not indicate accesskey in text.</returns>
        ''' <version version="1.5.2">Function added</version>
        Protected MustOverride Function GetTextWithAccessKey(ByVal Text$, ByVal AccessKey As Char) As String

        ''' <summary>Creates accesskey text by prepending given char before accesskey character</summary>
        ''' <param name="Text">Text of control</param>
        ''' <param name="AccessKey">Accesskey to prepend char in front of</param>
        ''' <param name="PrependChar">Char to be prepended</param>
        ''' <returns><paramref name="Text"/> with first occurence of <paramref name="AccessKey"/> replaced with <paramref name="PrependChar"/> and <paramref name="AccessKey"/>.</returns>
        ''' <remarks><paramref name="AccessKey"/> is escaped by duplication</remarks>
        ''' <version version="1.5.2">Function added</version>
        Protected Shared Function GetTextWithAccessKey(ByVal Text As String, ByVal AccessKey As Char, ByVal PrependChar As Char) As String
            If Text = "" Then Return Text
            Text = Text.Replace(PrependChar, PrependChar & PrependChar)
            If Text.Contains(AccessKey) Then
                Text = Text.Substring(0, Text.IndexOf(AccessKey)) & PrependChar & Text.Substring(Text.IndexOf(AccessKey))
            End If
            Return Text
        End Function
#End Region
#Region "Modal sync"
        ''' <summary>Displays modal messagebox in sync with given control</summary>
        ''' <param name="Control">Control to diplay dialog in thread control was created by</param>
        ''' <param name="Owner">Optional owner of dialog (the window dialog will be modal to)</param>
        ''' <returns>Result of diloag identifiing pressed button</returns>
        ''' <remarks>This function can be used to display dialogs from background worker thread</remarks>
        Public Function ModalSync(ByVal Control As Control, Optional ByVal Owner As IWin32Window = Nothing) As DialogResult
            If Control.InvokeRequired Then
                Dim shd As Func(Of IWin32Window, DialogResult) = AddressOf Me.ShowDialog
                Return Control.Invoke(shd, Owner)
            Else
                Return Me.ShowDialog(Owner)
            End If
        End Function
        ''' <summary>Displays modal message box in sync with given control</summary>
        ''' <param name="Control">Control to diplay dialog in thread control was created by</param>
        ''' <param name="Template">Instance to initialize default message box with</param>
        ''' <param name="Prompt">If not null specified different prompt of messagebox</param>
        ''' <param name="Title">If not null specifies different title of messagebox</param>
        ''' <param name="Owner">Optional owner of messagebox - the window messagebox will be modal to</param>
        ''' <returns>Result of messagebox which identified button that was pressed</returns>
        ''' <seelaso cref="FakeBox"/>
        ''' <seelaso cref="ShowTemplate"/>
        ''' <seelaso cref="DisplayTemplate"/>
        ''' <seelaso cref="ModalSync"/>
        Public Shared Function ModalSyncTemplate(ByVal Control As Control, ByVal Template As MessageBox, Optional ByVal Prompt$ = Nothing, Optional ByVal Title$ = Nothing, Optional ByVal Owner As IWin32Window = Nothing) As DialogResult
            Dim Instance As Tools.WindowsT.IndependentT.MessageBox
            Try
                Instance = GetDefault()
            Catch ex As Exception
                Throw New TargetInvocationException(ResourcesT.Exceptions.ThereWasAnErrorObtaininInstanceOfDefaultImplementationOfMessageBoxSeeInnerExceptionForDetails, ex)
            End Try
            If Template Is Nothing Then Throw New ArgumentNullException("InitializeFrom")
            Instance.InitializeFrom(Template)
            If Prompt IsNot Nothing Then Instance.Prompt = Prompt
            If Title IsNot Nothing Then Instance.Title = Title
            Return Instance.ModalSync(Control, Owner)
        End Function
#End Region
    End Class
End Namespace
#End If