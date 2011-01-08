Imports System.Windows, Tools.WindowsT.InteropT, System.Linq, Tools.WindowsT.WPF.WpfExtensions
Imports iMsg = Tools.WindowsT.IndependentT.MessageBox
Imports Tools.WindowsT.WPF.ConvertersT, Tools.WindowsT.IndependentT
Imports Tools.ComponentModelT, Tools.ExtensionsT
Imports System.Windows.Input

#If Config <= Beta Then  'Stage: Beta
Namespace WindowsT.WPF.DialogsT
    ''' <summary>Control that implements WPF <see cref="MessageBox"/></summary>
    ''' <remarks>This control is not intended to be used separately, to be placed on yopur window. This control implements WPF <see cref="MessageBox"/> and can be styled/templated.
    ''' Teplate parts are panels for additional controls that can be placed on message box and are optional. When not pressent additional control will not be visible! But use of additional controls is rare.
    ''' <para>This control is disposable. When it disposes it should not be used. It disposes automatically wehn message box window closes.</para>
    ''' <para>Due to WPF limitations message box always displays an ugly icon. If you want to hide the icon set <see cref="P:Tools.WindowsT.WPF.NativeExtensions.GloballyHideNullIconsOfWindows"/> to true.</para></remarks>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    ''' <version version="1.5.3." stage="Beta">Added ability to copy all text of message box using Ctrl+C</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    <TemplatePart(Name:=MessageBoxImplementationControl.PART_TopControlPlaceholder, Type:=GetType(Controls.Panel))> _
    <TemplatePart(Name:=MessageBoxImplementationControl.PART_MiddleControlPlaceholder, Type:=GetType(Controls.Panel))> _
    <TemplatePart(Name:=MessageBoxImplementationControl.PART_BottomControlPlaceholder, Type:=GetType(Controls.Panel))> _
    Public Class MessageBoxImplementationControl
        Inherits Windows.Controls.Control
        Implements IDisposable
        ''' <summary>Identifies placeholder panel for additional control on top of message box window</summary>
        Protected Friend Const PART_TopControlPlaceholder As String = "PART_TopControlPlaceholder"
        ''' <summary>Identifies placeholder panel for additional control on bottom of message box window above buttons</summary>
        Protected Friend Const PART_MiddleControlPlaceholder As String = "PART_MiddleControlPlaceholder"
        ''' <summary>Identifies placeholder panel for additional control on bottom of message box window below buttons</summary>
        Protected Friend Const PART_BottomControlPlaceholder As String = "PART_BottomControlPlaceholder"
        ''' <summary>Contains value of the <see cref="MessageBox"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private WithEvents _MessageBox As MessageBox
        ''' <summary>Gest or sets instance of <see cref="WindowsT.WPF.DialogsT.MessageBox"/> this instance is user interface for</summary>
        ''' <returns>Instance of <see cref="WindowsT.WPF.DialogsT.MessageBox"/> this instance is user interface for</returns>
        ''' <value>Set value to associate <see cref="MessageBoxImplementationControl"/> and <see cref="WindowsT.WPF.DialogsT.MessageBox"/></value>
        ''' <exception cref="ArgumentException">Value being set has not <see cref="MessageBox.Control"/> property set to this instance.</exception>
        Public Property MessageBox() As MessageBox
            <DebuggerStepThrough()> Get
                Return _MessageBox
            End Get
            Protected Friend Set(ByVal value As MessageBox)
                'If value Is Nothing Then Throw New ArgumentNullException("value")
                If value IsNot Nothing AndAlso value.Control IsNot Me Then Throw New ArgumentException(ResourcesT.Exceptions.MessageBoxMustOwnThisInstanceInOrderThisInstanceToBe)
                If _MessageBox IsNot Nothing AndAlso _MessageBox.Window IsNot Nothing Then RemoveHandler _MessageBox.Window.Closed, AddressOf Window_Closed
                _MessageBox = value
                If value IsNot Nothing AndAlso value.Window IsNot Nothing Then AddHandler value.Window.Closed, AddressOf Window_Closed
                Me.DataContext = value
            End Set
        End Property
        ''' <summary>Initializer</summary>
        Shared Sub New()
            DefaultStyleKeyProperty.OverrideMetadata(GetType(MessageBoxImplementationControl), New FrameworkPropertyMetadata(GetType(MessageBoxImplementationControl)))
            FlowDirectionProperty.OverrideMetadata(GetType(MessageBoxImplementationControl), New FrameworkPropertyMetadata(PropertyChangedCallback:=AddressOf OnFlowDirectionChanged))
            InitializeCommands()
        End Sub
        ''' <summary>Raises the <see cref="RenderSizeChanged"/> event</summary>
        Protected Overrides Sub OnRenderSizeChanged(ByVal sizeInfo As System.Windows.SizeChangedInfo)
            MyBase.OnRenderSizeChanged(sizeInfo)
            RaiseEvent RenderSizeChanged(Me, sizeInfo)
        End Sub
        ''' <summary>Raised when value of <see cref="ActualHeight"/> or <see cref="ActualWidth"/> property changes</summary>
        Public Event RenderSizeChanged As EventHandler(Of InteropT.SizeChangedInfoEventArgs)
#Region "FlowDirection"
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
#End Region
#Region "Title"
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
#End Region
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
        ''' <exception cref="ObjectDisposedException">Value is being set and <see cref="IsDisposed"/> is true.</exception>
        Protected Overridable Property BottomControl() As UIElement
            Get
                If Me.Template Is Nothing Then Return Nothing
                Dim Placeholder = TryCast(Me.Template.FindName(PART_BottomControlPlaceholder, Me), Controls.Panel)
                If Placeholder Is Nothing Then Return Nothing
                If Placeholder.Children.Count > 0 Then Return Placeholder.Children(0) Else Return Nothing
            End Get
            Set(ByVal value As UIElement)
                If IsDisposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                If value Is BottomControl Then Exit Property
                Dim Placeholder = TryCast(Me.Template.FindName(PART_BottomControlPlaceholder, Me), Controls.Panel)
                If Placeholder Is Nothing Then Exit Property
                Placeholder.Children.Clear()
                If value IsNot Nothing Then Placeholder.Children.Add(value)
                Placeholder.Visibility = If(value Is Nothing, Visibility.Collapsed, Visibility.Visible)
            End Set
        End Property
        ''' <summary>Gets or sets top additional control</summary>
        ''' <returns>Bottom additional control, first child of <see cref="PART_TopControlPlaceholder"/>-named item</returns>
        ''' <value>Removes all children from <see cref="PART_TopControlPlaceholder"/>-named item and places value there.</value>
        ''' <remarks>Override this property when your class does not use <see cref="PART_TopControlPlaceholder"/> of type <see cref="Controls.Panel"/></remarks>
        ''' <exception cref="ObjectDisposedException">Value is being set and <see cref="IsDisposed"/> is true.</exception>
        Protected Overridable Property TopControl() As UIElement
            Get
                If Me.Template Is Nothing Then Return Nothing
                Dim Placeholder = TryCast(Me.Template.FindName(PART_TopControlPlaceholder, Me), Controls.Panel)
                If Placeholder Is Nothing Then Return Nothing
                If Placeholder.Children.Count > 0 Then Return Placeholder.Children(0) Else Return Nothing
            End Get
            Set(ByVal value As UIElement)
                If IsDisposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                If value Is TopControl Then Exit Property
                Dim Placeholder = TryCast(Me.Template.FindName(PART_TopControlPlaceholder, Me), Controls.Panel)
                If Placeholder Is Nothing Then Exit Property
                Placeholder.Children.Clear()
                If value IsNot Nothing Then Placeholder.Children.Add(value)
                Placeholder.Visibility = If(value Is Nothing, Visibility.Collapsed, Visibility.Visible)
            End Set
        End Property
        ''' <summary>Gets or sets middle additional control</summary>
        ''' <returns>Bottom additional control, first child of <see cref="PART_MiddleControlPlaceholder"/>-named item</returns>
        ''' <value>Removes all children from <see cref="PART_MiddleControlPlaceholder"/>-named item and places value there.</value>
        ''' <remarks>Override this property when your class does not use <see cref="PART_MiddleControlPlaceholder"/> of type <see cref="Controls.Panel"/></remarks>
        ''' <exception cref="ObjectDisposedException">Value is being set and <see cref="IsDisposed"/> is true.</exception>
        Protected Overridable Property MidControl() As UIElement
            Get
                If Me.Template Is Nothing Then Return Nothing
                Dim Placeholder = TryCast(Me.Template.FindName(PART_MiddleControlPlaceholder, Me), Controls.Panel)
                If Placeholder Is Nothing Then Return Nothing
                If Placeholder.Children.Count > 0 Then Return Placeholder.Children(0) Else Return Nothing
            End Get
            Set(ByVal value As UIElement)
                If IsDisposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                If value Is MidControl Then Exit Property
                Dim Placeholder = TryCast(Me.Template.FindName(PART_MiddleControlPlaceholder, Me), Controls.Panel)
                If Placeholder Is Nothing Then Exit Property
                Placeholder.Children.Clear()
                If value IsNot Nothing Then Placeholder.Children.Add(value)
                Placeholder.Visibility = If(value Is Nothing, Visibility.Collapsed, Visibility.Visible)
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
        Private Sub Window_Closed(ByVal sender As Object, ByVal e As EventArgs)
            OnWindowClosed()
        End Sub
        ''' <summary>Called wnem message box windows closes</summary>
        Protected Overridable Sub OnWindowClosed()
            Dispose()
        End Sub
        Private Sub MessageBox_WindowChanged(ByVal sender As Object, ByVal e As IReportsChange.ValueChangedEventArgs(Of System.Windows.Window)) Handles _MessageBox.WindowChanged
            OnWindowChanged(e)
        End Sub
        ''' <summary>Called when window associated with <see cref="MessageBox"/> changes</summary>
        Protected Overridable Sub OnWindowChanged(ByVal e As IReportsChange.ValueChangedEventArgs(Of Window))
            If e.OldValue IsNot Nothing Then RemoveHandler e.OldValue.Closed, AddressOf Window_Closed
            If IsDisposed Then Exit Sub
            If e.NewValue IsNot Nothing Then AddHandler e.NewValue.Closed, AddressOf Window_Closed
        End Sub
#End Region
#Region "IDisposable Support"
        ''' <summary>To detect redundant calls</summary>
        Private _IsDisposed As Boolean = False
        ''' <summary>Gets value indicationg if this control was disposed</summary>
        ''' <returns>True if this control was already disposed</returns>
        Public ReadOnly Property IsDisposed() As Boolean
            Get
                Return _IsDisposed
            End Get
        End Property
        ''' <summary>Implements <see cref="IDisposable.Dispose"/></summary>
        ''' <param name="disposing">True when disposing</param>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.IsDisposed Then
                If disposing Then
                    TopControl = Nothing
                    MidControl = Nothing
                    BottomControl = Nothing
                    If MessageBox IsNot Nothing AndAlso MessageBox.Window IsNot Nothing Then RemoveHandler MessageBox.Window.Closed, AddressOf Window_Closed
                    MessageBox = Nothing
                End If
            End If
            Dim WasDisposed As Boolean = _IsDisposed
            Me._IsDisposed = True
            If Not WasDisposed Then RaiseEvent Disposed(Me, EventArgs.Empty)
        End Sub

        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        ''' <filterpriority>2</filterpriority>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
        ''' <summary>Raised when this control is disposed</summary>
        <Browsable(False)> _
        Public Event Disposed As EventHandler
        ''' <summary>Allows an <see cref="T:System.Object" /> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Object" /> is reclaimed by garbage collection.</summary>
        ''' <remarks>This method cannot be overridn, override <see cref="Dispose"/> instead.</remarks>
        Protected NotOverridable Overrides Sub Finalize()
            MyBase.Finalize()
            Dispose(True)
        End Sub
#End Region
        'TODO: Test
        ''' <summary>Gets full textual representation of message box</summary>
        ''' <returns>Textual representation of messagesbox including <see cref="MessageBox.Title"/>, <see cref="MessageBox.Prompt"/>, <see cref="MessageBox.ComboBox"/>, <see cref="MessageBox.CheckBoxes"/>, <see cref="MessageBox.Radios"/> and <see cref="MessageBox.Buttons"/></returns>
        ''' <remarks>Custom controls - <see cref="MessageBox.TopControl"/>, <see cref="MessageBox.MidControl"/> and <see cref="MessageBox.BottomControl"/> are not included in text</remarks>
        ''' <vertion version="1.5.3" stage="Nightly">This function is new in version 1.5.3</vertion>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Overridable Function GetCopyText() As String
            If Me.IsDisposed Then Throw New ObjectDisposedException(Me.Name)
            Dim text As New System.Text.StringBuilder
            If MessageBox.Title = "" Then
                text.AppendLine("========================================")
            Else
                text.AppendLine("================= " & MessageBox.Title & " =================")
            End If
            If MessageBox.Prompt <> "" Then text.AppendLine(MessageBox.Prompt)
            If MessageBox.ComboBox IsNot Nothing Then
                Dim i As Integer = 0
                For Each item In MessageBox.ComboBox.Items
                    Dim ch As Char = If(i = MessageBox.ComboBox.SelectedIndex, "»"c, ">"c)
                    Try
                        If item Is Nothing Then text.AppendLine(ch) : Continue For
                        If MessageBox.ComboBox.DisplayMember = "" Then text.AppendLine(ch & " " & item.ToString) : Continue For
                        Try
                            For Each prd As PropertyDescriptor In TypeDescriptor.GetProperties(item)
                                If prd.Name = MessageBox.ComboBox.DisplayMember Then
                                    Dim value = prd.GetValue(item)
                                    If value Is Nothing Then
                                        text.AppendLine(ch)
                                    Else
                                        text.AppendLine(ch & " " & value.ToString)
                                    End If
                                    Exit For
                                End If
                            Next
                        Catch ex As Exception
                            text.AppendLine(ch & " " & item.ToString)
                        End Try
                    Finally
                        i += 1
                    End Try
                Next
            End If
            If MessageBox.CheckBoxes.Count > 0 Then
                For Each chk In MessageBox.CheckBoxes
                    text.AppendLine(If(chk.State = Forms.CheckState.Checked, "☑", If(chk.State = Forms.CheckState.Unchecked, "☐", "▣")) & " " &
                                    chk.Text)
                Next
            End If
            If MessageBox.Radios.Count > 0 Then
                For Each rad In MessageBox.Radios
                    text.AppendLine(If(rad.Checked, "◉", "◯") & " " & rad.Text)
                Next
            End If
            If MessageBox.Buttons.Count > 0 Then
                Dim ButtonsText As String = ""
                For Each button In MessageBox.Buttons
                    If ButtonsText <> "" Then ButtonsText &= " "
                    ButtonsText &= String.Format("[{0}]", button.Text)
                Next
                text.AppendLine(ButtonsText)
            End If
            Return text.ToString
        End Function
    End Class

    ''' <summary>Implements <see cref="iMsg"/> for Windows Presentation Foundation</summary>
    ''' <remarks>Message box user interface is implemented by <see cref="MessageBoxImplementationControl"/>. To change style or template of message box, use that control.</remarks>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    ''' <version version="1.5.3" stage="Beta">Added support for <see cref="Window"/> as message box owner required by changes in <see cref="iMsg"/></version>
    ''' <version version="1.5.3" stage="Beta">Owner of dialog now can be any <see cref="Windows.DependencyObject"/> hosted in <see cref="Windows.Window"/>.</version>
    ''' <version version="1.5.3">Messages are now centered to thair owner (if some conditions are met).
    ''' The conditions are: Owner is specified and - Owner is <see cref="Window"/> or it's <see cref="DependencyObject"/> for which a <see cref="Window"/> can be determined using <see cref="Window.GetWindow"/> -or- 
    ''' Owner is <see cref="Forms.Control"/> -or-
    ''' Owner is either <see cref="Forms.IWin32Window"/> or <see cref="Interop.IWin32Window"/> and it's handle represents <see cref="Windows.Controls"/> -or-
    ''' <para>
    ''' If owner is <see cref="Forms.Control"/> dialog is centered to control's parent <see cref="Forms.Form"/> (if it can be determined using <see cref="Forms.Control.FindForm"/>). If <see cref="Forms.Form"/> cannot be determined the dialog is centered to control itself.
    ''' If owner is <see cref="DependencyObject"/> dialog is centered to parent <see cref="Window"/> of the <see cref="DependencyObject"/>. If parent <see cref="Window"/> cannot be found (using <see cref="Window.GetWindow"/>) the dialog is not centered at all.
    ''' </para>
    ''' <para>If owner is either <see cref="Forms.IWin32Window"/> or <see cref="Interop.IWin32Window"/> and no corresponding <see cref="Forms.Control"/> can be found (using <see cref="Forms.Control.FromHandle"/>) the dialog is not centered. (This is considered a limitation which may be fixed in one of next versions.)</para></version>
    Public Class MessageBox : Inherits iMsg
        Implements INotifyPropertyChanged
        ''' <summary>Format of title with timer</summary>
        Private Const TitleFormatWithTimer As String = "{0} {1:" & TimerFormat & "}"

        ''' <summary>Closes message box with <see cref="CloseResponse"/></summary>
        ''' <param name="Response">Response to close window with</param>
        Public Overloads Overrides Sub Close(ByVal Response As System.Windows.Forms.DialogResult)
            Window.Close()
            Control.Dispose()
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
                Dim old = _Window
                _Window = value
                If old IsNot value Then OnWindowClosed(New IReportsChange.ValueChangedEventArgs(Of Window)(old, value, "Window"))
            End Set
        End Property
        ''' <summary>Raiese the <see cref="WindowChanged"/> event</summary>
        ''' <param name="e">Event arguments</param>
        Protected Overridable Sub OnWindowClosed(ByVal e As IReportsChange.ValueChangedEventArgs(Of Window))
            RaiseEvent WindowChanged(Me, e)
            OnChanged(e)
            OnPropertyChanged(e)
        End Sub
        ''' <summary>Raised when value of the <see cref="Window"/> property changes</summary>
        <KnownCategory(KnownCategoryAttribute.AnotherCategories.PropertyChanged)> _
        <LDescription(GetType(WindowsT.FormsT.Dialogs), "WindowChanged_d")> _
        Public Event WindowChanged As EventHandler(Of IReportsChange.ValueChangedEventArgs(Of Window))

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
        ''' <param name="Owner">Parent window of dialog (may be null).  This implementation recognizes values of type <see cref="Forms.IWin32Window"/>, <see cref="Interop.IWin32Window"/>, <see cref="Windows.Window"/> and <see cref="Windows.DependencyObject"/> (if hosted in <see cref="Windows.Window"/>). Unrecognized owners are treated as null.</param>
        ''' <exception cref="InvalidOperationException"><see cref="State"/> is not <see cref="States.Created"/>. Overriding method shall check this condition and thrown an exception if condition is vialoted.</exception>
        ''' <version version="1.5.3" stage="Beta">Type of parameter <paramref name="owner"/> changed from <see cref="Forms.IWin32Window"/> to <see cref="Object"/> to support <see cref="Forms.IWin32Window"/>, <see cref="Interop.IWin32Window"/> and <see cref="Windows.Window"/>.</version>
        ''' <version version="1.5.3" stage="Beta">The <paramref name="Owner"/> parameter acceps <see cref="Windows.DependencyObject"/> for which <see cref="Windows.Window.GetWindow"/> returns non-null value.</version>
        ''' <version version="1.5.3">Parameters renamed: <c>Modal</c> to <c>modal</c>; <c>Owner</c> to <c>owner</c></version>
        ''' <version version="1.5.3">Changed so that message box is now centered to it's parent (as long as the parent is <see cref="Forms.Control"/>, <see cref="Windows.Window"/> (or <see cref="DependencyObject"/> from a <see cref="Window"/>) or <see cref="Forms.IWin32Window"/> or <see cref="Interop.IWin32Window"/> representing <see cref="Forms.Control"/>). See class documentation for details.</version>
        Protected Overrides Sub PerformDialog(ByVal modal As Boolean, ByVal owner As Object)
            If State <> States.Created Then Throw New InvalidOperationException(ResourcesT.Exceptions.MessageBoxMustBeInCreatedStateInOrderToBeDisplyedByPerformDialog)
            Window = New MessageBoxWindow()
            Control = DirectCast(Window, MessageBoxWindow).MsgBoxControl
            Control.MessageBox = Me
            'Owner
            If TypeOf owner Is Forms.IWin32Window Then
                Dim hlp As New Interop.WindowInteropHelper(Window)
                hlp.Owner = DirectCast(owner, Forms.IWin32Window).Handle
            ElseIf TypeOf owner Is Interop.IWin32Window Then
                Dim hlp As New Interop.WindowInteropHelper(Window)
                hlp.Owner = DirectCast(owner, Interop.IWin32Window).Handle
            ElseIf TypeOf owner Is Window Then
                Window.Owner = owner
            ElseIf TypeOf owner Is DependencyObject Then
                Window.Owner = Window.GetWindow(owner)
            End If
            'Owner size and position
            Dim ownerRect As Drawing.Rectangle?
            If TypeOf owner Is Forms.Form Then
                ownerRect = DirectCast(owner, Forms.Form).DisplayRectangle
            ElseIf TypeOf owner Is Forms.Control Then
                ownerRect = If(DirectCast(owner, Forms.Control).FindForm, DirectCast(owner, Forms.Control)).DisplayRectangle
            ElseIf TypeOf owner Is Forms.IWin32Window Then 'TODO:SUpport any IWin32Window
                Dim ctl As Forms.Control = Forms.Control.FromHandle(DirectCast(owner, Forms.IWin32Window).Handle)
                If ctl IsNot Nothing Then
                    ownerRect = If(ctl.FindForm, ctl).DisplayRectangle
                End If
            ElseIf TypeOf owner Is Interop.IWin32Window Then 'TODO:SUpport any IWin32Window
                Dim ctl As Forms.Control = Forms.Control.FromHandle(DirectCast(owner, Interop.IWin32Window).Handle)
                If ctl IsNot Nothing Then
                    ownerRect = If(ctl.FindForm, ctl).DisplayRectangle
                End If
            ElseIf TypeOf owner Is DependencyObject Then
                Dim window As Window
                If TypeOf owner Is Window Then window = owner _
                Else window = window.GetWindow(owner)
                If window IsNot Nothing Then
                    ownerRect = New Drawing.Rectangle(window.Left, window.Top, window.ActualWidth, window.ActualHeight)
                End If
            End If
            If ownerRect IsNot Nothing Then
                AddHandler Window.Loaded, Sub(sender, e)
                                              Dim sw As Window = sender
                                              sw.Left = (ownerRect.Value.X * 2 + ownerRect.Value.Width) / 2 - sw.Width / 2
                                              sw.Top = (ownerRect.Value.Y * 2 + ownerRect.Value.Height) / 2 - sw.Height / 2
                                          End Sub
            End If

            If modal Then
                Window.ShowDialog()
            Else
                If TypeOf owner Is Forms.IWin32Window OrElse TypeOf owner Is Interop.IWin32Window Then Forms.Integration.ElementHost.EnableModelessKeyboardInterop(Window)
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
        <KnownCategory(KnownCategoryAttribute.AnotherCategories.PropertyChanged)> _
        <LDescription(GetType(WindowsT.FormsT.Dialogs), "PropertyChanged_d")> _
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