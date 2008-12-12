Imports System.Windows
Imports iMsg = Tools.WindowsT.IndependentT.MessageBox
#If Config <= Nightly Then  'Stage: Nightly
Namespace WindowsT.WPF.DialogsT
    ''' <summary>Control that implements WPF <see cref="MessageBox"/></summary>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Class MessageBoxImplementationControl
        Inherits Windows.Controls.Control
        ''' <summary>Contains value of the <see cref="MessageBox"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _MessageBox As MessageBox
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
        End Sub


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
    End Class
    ''' <summary>Implements <see cref="iMsg"/> for Windows Presentation Foundation</summary>
    ''' <remarks>Message box user interface is implemented by <see cref="MessageBoxImplementationControl"/>. To change style or template of message box, use that control.</remarks>
    Public Class MessageBox : Inherits iMsg

        ''' <summary>Closes message box with <see cref="CloseResponse"/></summary>
        ''' <param name="Response">Response to close window with</param>
        Public Overloads Overrides Sub Close(ByVal Response As System.Windows.Forms.DialogResult)
            Window.Close()
        End Sub
        ''' <summary>Contains value of the <see cref="Window"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Window As Window
        ''' <summary>Gets or sets window representing message box usre interface</summary>
        ''' <returns>Window representing message box user interface</returns>
        ''' <value>Window to represent message box user interface</value>
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
            If Modal Then
                Window.ShowDialog()
            Else
                Window.Show()
            End If
        End Sub
    End Class
End Namespace
#End If