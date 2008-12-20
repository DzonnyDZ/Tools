Imports System.Windows, Tools.WindowsT.InteropT, System.Linq
Imports iMsg = Tools.WindowsT.IndependentT.MessageBox
Imports Tools.WindowsT.WPF.ConvertersT, Tools.WindowsT.IndependentT
Imports Tools.ComponentModelT, Tools.ExtensionsT
Imports System.Windows.Input

#If Config <= Nightly Then  'Stage: Nightly
Namespace WindowsT.WPF.DialogsT
    ''' <summary>Control that implements WPF <see cref="MessageBox"/></summary>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    <TemplatePart(Name:=MessageBoxImplementationControl.PART_TopControlPlaceholder, Type:=GetType(Controls.Panel))> _
    <TemplatePart(Name:=MessageBoxImplementationControl.PART_MiddleControlPlaceholder, Type:=GetType(Controls.Panel))> _
    <TemplatePart(Name:=MessageBoxImplementationControl.PART_BottomControlPlaceholder, Type:=GetType(Controls.Panel))> _
    Public Class MessageBoxImplementationControl
        Inherits Windows.Controls.Control
        Protected Friend Const PART_TopControlPlaceholder As String = "PART_TopControlPlaceholder"
        Protected Friend Const PART_MiddleControlPlaceholder As String = "PART_MiddleControlPlaceholder"
        Protected Friend Const PART_BottomControlPlaceholder As String = "PART_BottomControlPlaceholder"
        ''' <summary>Contains value of the <see cref="MessageBox"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private WithEvents _MessageBox As MessageBox
        ''' <summary>Gest or sets instance of <see cref="WindowsT.WPF.DialogsT.MessageBox"/> this instance is user interface for</summary>
        ''' <returns>Instance of <see cref="WindowsT.WPF.DialogsT.MessageBox"/> this instance is user interface for</returns>
        ''' <value>Set value to associate <see cref="MessageBoxImplementationControl"/> and <see cref="WindowsT.WPF.DialogsT.MessageBox"/></value>
        ''' <exception cref="ArgumentNullException">Value being set is null</exception>
        ''' <exception cref="ArgumentException">Value being set has not <see cref="MessageBox.Control"/> property set to this instance.</exception>
        Public Property MessageBox() As MessageBox
            <DebuggerStepThrough()> Get
                Return _MessageBox
            End Get
            Protected Friend Set(ByVal value As MessageBox)
                If value Is Nothing Then Throw New ArgumentNullException("value")
                If value.Control IsNot Me Then Throw New ArgumentException(ResourcesT.Exceptions.MessageBoxMustOwnThisInstanceInOrderThisInstanceToBe)
                _MessageBox = value
                Me.DataContext = value
            End Set
        End Property
        ''' <summary>Initializer</summary>
        Shared Sub New()
            DefaultStyleKeyProperty.OverrideMetadata(GetType(MessageBoxImplementationControl), New FrameworkPropertyMetadata(GetType(MessageBoxImplementationControl)))
            FlowDirectionProperty.OverrideMetadata(GetType(MessageBoxImplementationControl), New FrameworkPropertyMetadata(AddressOf OnFlowDirectionChanged))
            InitializeCommands()
        End Sub
        ''' <summary>Callback called when the <see cref="FlowDirection"/> property is changed</summary>
        Private Shared Sub OnFlowDirectionChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            DirectCast(d, MessageBoxImplementationControl).OnFlowDirectionChanged(e)
        End Sub
        ''' <summary>Raises the <see cref="FlowDirectionChanged"/> event. Caled when value of the <see cref="FlowDirection"/> property changes.</summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnFlowDirectionChanged(ByVal e As InteropT.DependencyPropertyChangedEventArgsEventArgs)
            RaiseEvent FlowDirectionChanged(Me, e)
        End Sub
        ''' <summary>Raised when the value of the <see cref="FlowDirection"/> property changes</summary>
        Public Event FlowDirectionChanged As EventHandler(Of InteropT.DependencyPropertyChangedEventArgsEventArgs)

        ''' <summary>Gets or sets string indicating title of window this control is laced on</summary>
        ''' <returns>Current title</returns>
        ''' <value>Title to be set to window</value>
        ''' <remarks>Its responsibility of window to update its <see cref="Window.Title"/> whenver <see cref="Title"/> changes. It can be also detected using the <see cref="TitleChanged"/> event</remarks>
        Public Property Title() As String
            Get
                Return GetValue(TitleProperty)
            End Get

            Set(ByVal value As String)
                SetValue(TitleProperty, value)
            End Set
        End Property
        ''' <summary>Identifies the <see cref="Title"/> property</summary>
        Public Shared ReadOnly TitleProperty As DependencyProperty = _
                               DependencyProperty.Register("Title", _
                               GetType(String), GetType(MessageBoxImplementationControl), _
                               New FrameworkPropertyMetadata("MessageBox", New PropertyChangedCallback(AddressOf OnTitleChanged)))
        ''' <summary>Callback called wehn the <see cref="TitleProperty"/> is changed</summary>
        Private Shared Sub OnTitleChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            DirectCast(d, MessageBoxImplementationControl).OnTitleChanged(e)
        End Sub
        ''' <summary>Raises the <see cref="TitleChanged"/> event. Called when value of the <see cref="TitleProperty"/> changes</summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnTitleChanged(ByVal e As InteropT.DependencyPropertyChangedEventArgsEventArgs)
            RaiseEvent TitleChanged(Me, e)
        End Sub
        ''' <summary>Raised when value of the <see cref="Title"/> property changes</summary>
        Public Event TitleChanged As EventHandler(Of InteropT.DependencyPropertyChangedEventArgsEventArgs)

#Region "Commands"
        ''' <summary>Gets command to be executed when button is clicked.</summary>
        ''' <returns>Command to be execued when button is clicked</returns>
        ''' <remarks>Pass <see cref="iMsg.MessageBoxButton.Result"/> to command parameter</remarks>
        Public Shared ReadOnly Property ButtonClickCommand() As RoutedCommand
            Get
                Return _ButtonClickCommand
            End Get
        End Property
        ''' <summary>Contains value of the <see cref="ButtonClickCommand"/> property</summary>
        Private Shared _ButtonClickCommand As RoutedCommand
        ''' <summary>Initializes comands</summary>
        Private Shared Sub InitializeCommands()
            _ButtonClickCommand = New RoutedCommand("ButtonClickCommand", GetType(MessageBoxImplementationControl))
            CommandManager.RegisterClassCommandBinding(GetType(MessageBoxImplementationControl), New CommandBinding(_ButtonClickCommand, AddressOf OnButtonClick))
        End Sub
        ''' <summary>Called when <see cref="ButtonClickCommand"/> is invoked</summary>
        ''' <param name="sender">Source of event (<see cref="MessageBoxImplementationControl"/>)</param>
        ''' <param name="e">Event arguments</param>
        Private Shared Sub OnButtonClick(ByVal sender As Object, ByVal e As ExecutedRoutedEventArgs)
            Dim control As MessageBoxImplementationControl = TryCast(sender, MessageBoxImplementationControl)
            If control IsNot Nothing Then
                control.OnButtonClick(e)
            End If
        End Sub
        ''' <summary>Called whne button is clicked</summary>
        ''' <param name="e">Event arguments. <see cref="iMsg.MessageBoxButton"/> can be retrieved from <paramref name="e"/>.<see cref="ExecutedRoutedEventArgs.OriginalSource">OriginalSource</see>.<see cref="FrameworkElement.DataContext">DataContext</see></param>
        ''' <remarks>This implementation calls <see cref="iMsg.MessageBoxButton.OnClick"/></remarks>
        Protected Overridable Sub OnButtonClick(ByVal e As ExecutedRoutedEventArgs)
            If DirectCast(DirectCast(e.OriginalSource, FrameworkElement).DataContext, iMsg.MessageBoxButton).OnClick() Then
                MessageBox.DialogResult = DirectCast(DirectCast(e.OriginalSource, FrameworkElement).DataContext, iMsg.MessageBoxButton).Result
                MessageBox.ClickedButton = DirectCast(DirectCast(e.OriginalSource, FrameworkElement).DataContext, iMsg.MessageBoxButton)
                MessageBox.Window.Close()
            End If
        End Sub
#End Region
#Region "Additional controls"
        ''' <summary>Is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.                </summary>
        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()
            SetCustomControls()
        End Sub
        ''' <summary>Sets custom controls</summary>
        Private Sub SetCustomControls()
            BottomControl = MessageBox.BottomControlControl
            TopControl = MessageBox.TopControlControl
            MidControl = MessageBox.MidControlControl
        End Sub
        ''' <summary>Gets or sets bottom additional control</summary>
        ''' <returns>Bottom additional control, first child of <see cref="PART_BottomControlPlaceholder"/>-named item</returns>
        ''' <value>Removes all children from <see cref="PART_BottomControlPlaceholder"/>-named item and places value there.</value>
        ''' <remarks>Override this property when your class does not use <see cref="PART_BottomControlPlaceholder"/> of type <see cref="Controls.Panel"/></remarks>
        Protected Overridable Property BottomControl() As UIElement
            Get
                Dim Placeholder = TryCast(Me.Template.FindName(PART_BottomControlPlaceholder, Me), Controls.Panel)
                If Placeholder Is Nothing Then Return Nothing
                If Placeholder.Children.Count > 0 Then Return Placeholder.Children(0) Else Return Nothing
            End Get
            Set(ByVal value As UIElement)
                Dim Placeholder = TryCast(Me.Template.FindName(PART_BottomControlPlaceholder, Me), Controls.Panel)
                If Placeholder Is Nothing Then Exit Property
                Placeholder.Children.Clear()
                If value IsNot Nothing Then Placeholder.Children.Add(value)
            End Set
        End Property
        ''' <summary>Gets or sets top additional control</summary>
        ''' <returns>Bottom additional control, first child of <see cref="PART_TopControlPlaceholder"/>-named item</returns>
        ''' <value>Removes all children from <see cref="PART_TopControlPlaceholder"/>-named item and places value there.</value>
        ''' <remarks>Override this property when your class does not use <see cref="PART_TopControlPlaceholder"/> of type <see cref="Controls.Panel"/></remarks>
        Protected Overridable Property TopControl() As UIElement
            Get
                Dim Placeholder = TryCast(Me.Template.FindName(PART_TopControlPlaceholder, Me), Controls.Panel)
                If Placeholder Is Nothing Then Return Nothing
                If Placeholder.Children.Count > 0 Then Return Placeholder.Children(0) Else Return Nothing
            End Get
            Set(ByVal value As UIElement)
                Dim Placeholder = TryCast(Me.Template.FindName(PART_TopControlPlaceholder, Me), Controls.Panel)
                If Placeholder Is Nothing Then Exit Property
                Placeholder.Children.Clear()
                If value IsNot Nothing Then Placeholder.Children.Add(value)
            End Set
        End Property
        ''' <summary>Gets or sets middle additional control</summary>
        ''' <returns>Bottom additional control, first child of <see cref="PART_MiddleControlPlaceholder"/>-named item</returns>
        ''' <value>Removes all children from <see cref="PART_MiddleControlPlaceholder"/>-named item and places value there.</value>
        ''' <remarks>Override this property when your class does not use <see cref="PART_MiddleControlPlaceholder"/> of type <see cref="Controls.Panel"/></remarks>
        Protected Overridable Property MidControl() As UIElement
            Get
                Dim Placeholder = TryCast(Me.Template.FindName(PART_MiddleControlPlaceholder, Me), Controls.Panel)
                If Placeholder Is Nothing Then Return Nothing
                If Placeholder.Children.Count > 0 Then Return Placeholder.Children(0) Else Return Nothing
            End Get
            Set(ByVal value As UIElement)
                Dim Placeholder = TryCast(Me.Template.FindName(PART_MiddleControlPlaceholder, Me), Controls.Panel)
                If Placeholder Is Nothing Then Exit Property
                Placeholder.Children.Clear()
                If value IsNot Nothing Then Placeholder.Children.Add(value)
            End Set
        End Property


        Private Sub MessageBox_BottomControlChanged(ByVal sender As IndependentT.MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Object)) Handles _MessageBox.BottomControlChanged
            BottomControl = MessageBox.BottomControlControl
        End Sub

        Private Sub MessageBox_MidControlChanged(ByVal sender As IndependentT.MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Object)) Handles _MessageBox.MidControlChanged
            MidControl = MessageBox.TopControlControl
        End Sub

        Private Sub MessageBox_TopControlChanged(ByVal sender As IndependentT.MessageBox, ByVal e As IReportsChange.ValueChangedEventArgs(Of Object)) Handles _MessageBox.TopControlChanged
            TopControl = MessageBox.TopControlControl
        End Sub
#End Region
    End Class
    ''' <summary>Implements <see cref="iMsg"/> for Windows Presentation Foundation</summary>
    ''' <remarks>Message box user interface is implemented by <see cref="MessageBoxImplementationControl"/>. To change style or template of message box, use that control.</remarks>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public Class MessageBox : Inherits iMsg
        Implements INotifyPropertyChanged
        ''' <summary>Format of title with timer</summary>
        Private Const TitleFormatWithTimer As String = "{0} {1:" & TimerFormat & "}"

        ''' <summary>Closes message box with <see cref="CloseResponse"/></summary>
        ''' <param name="Response">Response to close window with</param>
        Public Overloads Overrides Sub Close(ByVal Response As System.Windows.Forms.DialogResult)
            Window.Close()
        End Sub
        ''' <summary>Contains value of the <see cref="Window"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private WithEvents _Window As Window
        ''' <summary>Gets or sets window representing message box usre interface</summary>
        ''' <returns>Window representing message box user interface</returns>
        ''' <value>Window to represent message box user interface</value>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property Window() As Window
            Get
                Return _Window
            End Get
            Protected Set(ByVal value As Window)
                _Window = value
            End Set
        End Property
        ''' <summary>Gets or sets value of the <see cref="Control"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Control As MessageBoxImplementationControl
        ''' <summary>Gets or sets control implementing message box user interface</summary>
        ''' <returns>Control implementing message box user interface</returns>
        ''' <value>Control to represent message box user interface</value>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property Control() As MessageBoxImplementationControl
            Get
                Return _Control
            End Get
            Protected Set(ByVal value As MessageBoxImplementationControl)
                _Control = value
            End Set
        End Property

        ''' <summary>Shows the dialog</summary>
        ''' <param name="Modal">Indicates if dialog should be shown modally (true) or modells (false)</param>
        ''' <param name="Owner">Parent window of dialog (may be null)</param>
        ''' <exception cref="InvalidOperationException"><see cref="State"/> is not <see cref="States.Created"/>. Overriding method shall check this condition and thrown an exception if condition is vialoted.</exception>
        Protected Overrides Sub PerformDialog(ByVal Modal As Boolean, ByVal Owner As System.Windows.Forms.IWin32Window)
            If State <> States.Created Then Throw New InvalidOperationException(ResourcesT.Exceptions.MessageBoxMustBeInCreatedStateInOrderToBeDisplyedByPerformDialog)
            Window = New MessageBoxWindow()
            Control = DirectCast(Window, MessageBoxWindow).MsgBoxControl
            Control.MessageBox = Me
            If Owner IsNot Nothing Then
                Dim hlp As New Interop.WindowInteropHelper(Window)
                hlp.Owner = Owner.Handle
            End If
            If Modal Then
                Window.ShowDialog()
            Else
                Forms.Integration.ElementHost.EnableModelessKeyboardInterop(Window)
                Window.Show()
            End If
        End Sub

        ''' <summary>Raises the <see cref="Changed"/> event</summary>
        ''' <param name="e">Event parameters</param>
        ''' <remarks><note type="ineritinfo">This implementtion calls itself with another value of <paramref name="e"/> when specific propeties changed causing <see cref="IReportsChange"/> and <see cref="INotifyPropertyChanged"/> to work for properties introduced by this implementation. Call bas class method in order this implemntation to work.</note></remarks>
        Protected Overrides Sub OnChanged(ByVal e As System.EventArgs)
            MyBase.OnChanged(e)
            If TypeOf e Is IReportsChange.ValueChangedEventArgsBase Then
                OnPropertyChanged(e)
                Select Case DirectCast(e, IReportsChange.ValueChangedEventArgsBase).ValueName
                    Case "Icon"
                        With DirectCast(e, IReportsChange.ValueChangedEventArgs(Of Drawing.Image))
                            Dim Old As Media.Imaging.BitmapSource = Nothing
                            If .OldValue IsNot Nothing Then Old = New Drawing.Bitmap(.OldValue).ToBitmapSource
                            Dim e2 As New IReportsChange.ValueChangedEventArgs(Of Media.Imaging.BitmapSource)(Old, IconImage, "IconImage")
                            OnChanged(e2)
                        End With
                    Case "Options"
                        With DirectCast(e, IReportsChange.ValueChangedEventArgs(Of iMsg.MessageBoxOptions))
                            If (.OldValue And MessageBoxOptions.AlignMask) <> (.NewValue And MessageBoxOptions.AlignMask) Then
                                OnChanged(New IReportsChange.ValueChangedEventArgs(Of HorizontalAlignment)(OptionsToHorizontalAlignment(.OldValue), OptionsToHorizontalAlignment(.NewValue), "PromptAlign"))
                            End If
                            If (.OldValue And MessageBoxOptions.Rtl) <> (.NewValue And MessageBoxOptions.Rtl) Then
                                OnChanged(New IReportsChange.ValueChangedEventArgs(Of FlowDirection)(OptionsToFlowDirection(.OldValue), OptionsToFlowDirection(.NewValue), "FlowDirection"))
                            End If
                        End With
                    Case "Title"
                        OnPropertyChanged(New PropertyChangedEventArgs("TitleWithTimer"))
                    Case "TimeButton"
                        With DirectCast(e, IReportsChange.ValueChangedEventArgs(Of Integer))
                            If (.OldValue = -1) <> (.NewValue = -1) AndAlso IsCountDown Then OnPropertyChanged(New PropertyChangedEventArgs("TitleWithTimer"))
                        End With
                    Case "Timer"
                        If TimeButton = -1 AndAlso IsCountDown Then OnPropertyChanged(New PropertyChangedEventArgs("TitleWithTimer"))
                        'Case "DefaultButton"
                        '    OnPropertyChanged(New PropertyChangedEventArgs("IsDefaultButtonConverter"))
                        'Case "CloseResponse"
                        '    OnPropertyChanged(New PropertyChangedEventArgs("IsCancelButtonConverter"))
                End Select
            End If
        End Sub
        ''' <summary>Raises the <see cref="CountDown"/> event</summary>
        ''' <param name="e">Event argument</param>
        ''' <remarks>Derived class should override this method in order to catch change of count down remaining time and call base class method.</remarks>
        Protected Overrides Sub OnCountDown(ByVal e As System.EventArgs)
            MyBase.OnCountDown(e)
            If TimeButton = -1 AndAlso IsCountDown Then OnPropertyChanged(New PropertyChangedEventArgs("TitleWithTimer"))
        End Sub
        ''' <summary>Gets window title including any possible timer (wehn appropriate)</summary>
        ''' <returns><see cref="Title"/>, with <see cref="CurrentTimer"/> appended when <see cref="TimeButton"/> is -1 and <see cref="IsCountDown"/> is true</returns>
        ''' <remarks>Chage of value of this property is reportyed only via <see cref="INotifyPropertyChanged"/>, not via <see cref="IReportsChange"/>.</remarks>
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public ReadOnly Property TitleWithTimer$()
            Get
                If TimeButton = -1 AndAlso IsCountDown Then Return String.Format(TitleFormatWithTimer, Title, CurrentTimer)
                Return Title
            End Get
        End Property
        ''' <summary>Converts <see cref="iMsg.MessageBoxOptions"/> to <see cref="HorizontalAlignment"/></summary>
        ''' <param name="Options">An <see cref="iMsg.MessageBoxOptions"/></param>
        ''' <returns>Extracted alignment from <paramref name="Options"/> converted to <see cref="HorizontalAlignment"/>. Fallback value is <see cref="HorizontalAlignment.Left"/>.</returns>
        Protected Shared Function OptionsToHorizontalAlignment(ByVal Options As iMsg.MessageBoxOptions) As HorizontalAlignment
            Select Case Options And MessageBoxOptions.AlignMask
                Case MessageBoxOptions.AlignCenter : Return HorizontalAlignment.Center
                Case MessageBoxOptions.AlignRight : Return HorizontalAlignment.Right
                Case MessageBoxOptions.AlignJustify : Return HorizontalAlignment.Stretch
                Case Else : Return HorizontalAlignment.Left
            End Select
        End Function
        ''' <summary>Gets or sets value of alignment stored in <see cref="Options"/></summary>
        ''' <returns>Extracted value of alignent stored in <see cref="Options"/> converted to <see cref="HorizontalAlignment"/>. Never returns <see cref="HorizontalAlignment.Stretch"/>.</returns>
        ''' <value>New valuef alignment to store in <see cref="Options"/>. Any unknown value is converted to <see cref="iMsg.MessageBoxOptions.AlignLeft"/>.</value>
        ''' <remarks>When setting value of this property any alignment-unrelated bits remains untouched.
        ''' <para>Change of this property is reported via <see cref="IReportsChange"/> and <see cref="INotifyPropertyChanged"/>.</para></remarks>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        <LDescription(GetType(WindowsT.FormsT.Dialogs), "TextAlignmentOfPrompt")> _
        <EnumDefaultValue(HorizontalAlignment.Left, GetType(HorizontalAlignment))> _
        Public Property PromptAlign() As HorizontalAlignment
            Get
                Return OptionsToHorizontalAlignment(Me.Options)
            End Get
            Set(ByVal value As HorizontalAlignment)
                Select Case value
                    Case HorizontalAlignment.Right : Options = (Options And Not iMsg.MessageBoxOptions.AlignMask) Or MessageBoxOptions.AlignRight
                    Case HorizontalAlignment.Center : Options = (Options And Not iMsg.MessageBoxOptions.AlignMask) Or MessageBoxOptions.AlignCenter
                    Case HorizontalAlignment.Stretch : Options = (Options And Not iMsg.MessageBoxOptions.AlignMask) Or MessageBoxOptions.AlignJustify
                    Case Else : Options = (Options And Not iMsg.MessageBoxOptions.AlignMask) Or MessageBoxOptions.AlignLeft
                End Select
            End Set
        End Property
        ''' <summary>Converts <see cref="iMsg.MessageBoxOptions"/> to <see cref="FlowDirection"/></summary>
        ''' <param name="Options">An <see cref="iMsg.MessageBoxOptions"/></param>
        ''' <returns><see cref="FlowDirection.RightToLeft"/> when <paramref name="Options"/> has <see cref="iMsg.MessageBoxOptions.Rtl"/> bit set; <see cref="FlowDirection.LeftToRight"/> otherwise.</returns>
        Protected Shared Function OptionsToFlowDirection(ByVal Options As iMsg.MessageBoxOptions) As FlowDirection
            Select Case Options And MessageBoxOptions.Rtl
                Case MessageBoxOptions.Rtl : Return Windows.FlowDirection.RightToLeft
                Case Else : Return Windows.FlowDirection.LeftToRight
            End Select
        End Function
        ''' <summary>gets or set value indicating bidirectionl flow direction stored in <see cref="Options"/></summary>
        ''' <returns><see cref="FlowDirection.RightToLeft"/> when <see cref="Options"/> has <see cref="iMsg.MessageBoxOptions.Rtl"/> bit set; <see cref="FlowDirection.LeftToRight"/> otherwise.</returns>
        ''' <value>If value being set is <see cref="FlowDirection.RightToLeft"/> then the <see cref="iMsg.MessageBoxOptions.Rtl"/> bit of <see cref="Options"/> is set;+ otherwise it is unset.</value>
        ''' <remarks>Change of this property is reported via <see cref="IReportsChange"/> and <see cref="INotifyPropertyChanged"/>.</remarks>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        <KnownCategory(KnownCategoryAttribute.KnownCategories.Appearance)> _
        <LDescription(GetType(WindowsT.FormsT.Dialogs), "ValueIndicatingBidirectionlFlowDirection")> _
        <EnumDefaultValue(FlowDirection.LeftToRight, GetType(FlowDirection))> _
        Public Property FlowDirection() As FlowDirection
            Get
                Return OptionsToFlowDirection(Options)
            End Get
            Set(ByVal value As FlowDirection)
                Select Case value
                    Case Windows.FlowDirection.RightToLeft : Options = Options Or iMsg.MessageBoxOptions.Rtl
                    Case Else : Options = Options And Not iMsg.MessageBoxOptions.Rtl
                End Select
            End Set
        End Property
        ''' <summary>Gets or sets <see cref="Icon"/> image as <see cref="Media.Imaging.BitmapSource"/>.</summary>
        ''' <returns>Value of the <see cref="Icon"/> property as <see cref="Media.Imaging.BitmapSource"/>. This property teruns new instace for each call.</returns>
        ''' <value>Sets value of the <see cref="Icon"/> property</value>
        ''' <remarks>Value of this property is internaly stored as <see cref="Drawing.Image"/> inside the <see cref="Icon"/> property.
        ''' <para>Change of this property is reported via <see cref="IReportsChange"/> and <see cref="INotifyPropertyChanged"/> interfaces</para></remarks>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
        Public Property IconImage() As Media.Imaging.BitmapSource
            Get
                If Icon Is Nothing Then Return Nothing
                Dim bmp As New Drawing.Bitmap(Me.Icon)
                Return bmp.ToBitmapSource
            End Get
            Set(ByVal value As Media.Imaging.BitmapSource)
                If value Is Nothing Then Me.Icon = Nothing : Exit Property
                Icon = value.ToBitmap
            End Set
        End Property
        ''' <summary>Raises the <see cref="PropertyChanged"/> event</summary>
        ''' <param name="e">Event arguments</param>
        ''' <remarks><note type="inheritnfo">Always call bae class method in order the event to be raised.</note></remarks>
        Protected Overridable Sub OnPropertyChanged(ByVal e As PropertyChangedEventArgs)
            RaiseEvent PropertyChanged(Me, e)
        End Sub

        ''' <summary>Occurs when a property value changes.</summary>
        Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Private Sub Window_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _Window.Closed
            OnClosed(e)
        End Sub

        Private Sub Window_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles _Window.Loaded
            OnShown()
        End Sub

        ''' <summary>Gets text which contains Accesskey marker (_)</summary>
        ''' <param name="Text">Text (if it contains character used as accesskey markers they must be escaped)</param>
        ''' <param name="AccessKey">Char representing accesskey (if char is not in <paramref name="Text"/> no accesskey marker should be inserted)</param>
        ''' <returns><paramref name="Text"/> with accesskey denoted in it by _.</returns>
        Protected Overrides Function GetTextWithAccessKey(ByVal Text As String, ByVal AccessKey As Char) As String
            Return GetTextWithAccessKey(Text, AccessKey, "_"c)
        End Function

#Region "Additional controls"
        ''' <summary>gets <see cref="UIelement"/> representation of <see cref="TopControl"/> if possible</summary>
        ''' <returns><see cref="UIelement"/> which represents <see cref="TopControl"/> if possible, null otherwise</returns>
        ''' <seealso cref="GetControl"/><seealso cref="MidControlControl"/><seealso cref="BottomControlControl"/>
        ''' <seealso cref="TopControl"/>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Friend ReadOnly Property TopControlControl() As UIElement
            Get
                Return GetControl(Me.TopControl)
            End Get
        End Property
        ''' <summary>Gets <see cref="UIelement"/> representation of <see cref="MidControl"/> if possible</summary>
        ''' <returns><see cref="UIelement"/> which represents <see cref="MidControl"/> if possible, null otherwise</returns>
        ''' <seealso cref="GetControl"/><seealso cref="TopControlControl"/><seealso cref="BottomControlControl"/>
        ''' <seealso cref="MidControl"/>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Friend ReadOnly Property MidControlControl() As UIElement
            Get
                Return GetControl(Me.MidControl)
            End Get
        End Property
        ''' <summary>Gets <see cref="UIelement"/> representation of <see cref="BottomControl"/> if possible</summary>
        ''' <returns><see cref="UIelement"/> which represents <see cref="BottomControl"/> if possible, null otherwise</returns>
        ''' <seealso cref="GetControl"/><seealso cref="TopControlControl"/><seealso cref="MidControlControl"/>
        ''' <seealso cref="BottomControl"/>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected Friend ReadOnly Property BottomControlControl() As UIElement
            Get
                Return GetControl(Me.BottomControl)
            End Get
        End Property
        ''' <summary>Gets control from object</summary>
        ''' <param name="Control">Object that represents a control. It can be <see cref="System.Windows.Forms.Control"/>, <see cref="Windows.UIElement"/></param>
        ''' <returns><see cref="UIelement"/> which represents <paramref name="Control"/>. For same <paramref name="Control"/> returns same <see cref="Control"/>. Returns null if <paramref name="Control"/> is null or it is of unsupported type.</returns>
        Protected Overridable Function GetControl(ByVal Control As Object) As UIElement
            If Control Is Nothing Then Return Nothing
            If TypeOf Control Is UIElement Then Return Control
            If TypeOf Control Is Forms.Control Then
                Static WFHosts As Dictionary(Of Forms.Control, Forms.Integration.WindowsFormsHost)
                If WFHosts Is Nothing Then WFHosts = New Dictionary(Of Forms.Control, Forms.Integration.WindowsFormsHost)
                If WFHosts.ContainsKey(Control) Then Return WFHosts(Control)
                Dim WFHost As New Forms.Integration.WindowsFormsHost
                WFHost.Child = Control
                WFHosts.Add(Control, WFHost)
                Return WFHost
            End If
            Return Nothing
        End Function
#End Region

    End Class
  

End Namespace
#End If