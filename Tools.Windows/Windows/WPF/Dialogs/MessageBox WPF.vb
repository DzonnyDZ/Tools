Imports System.Windows
Imports iMsg = Tools.WindowsT.IndependentT.MessageBox
#If Config <= Nightly Then  'Stage: Nightly
Namespace WindowsT.WPF.DialogsT
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Class MessageBoxImplementationControl
        Inherits Windows.Controls.Control
        <EditorBrowsable(EditorBrowsableState.Never)> Private _MessageBox As MessageBox
        Public Property MessageBox() As MessageBox
            <DebuggerStepThrough()> Get
                Return _MessageBox
            End Get
            Protected Friend Set(ByVal value As MessageBox)
                _MessageBox = value
            End Set
        End Property
        Shared Sub New()
            DefaultStyleKeyProperty.OverrideMetadata(GetType(MessageBoxImplementationControl), New FrameworkPropertyMetadata(GetType(MessageBoxImplementationControl))) 'GetType(MessageBoxWindow)))
        End Sub



        Public Property Title() As String
            Get
                Return GetValue(TitleProperty)
            End Get

            Set(ByVal value As String)
                SetValue(TitleProperty, value)
            End Set
        End Property

        Public Shared ReadOnly TitleProperty As DependencyProperty = _
                               DependencyProperty.Register("Title", _
                               GetType(String), GetType(MessageBoxImplementationControl), _
                               New FrameworkPropertyMetadata("MessageBox"))

        Protected Overrides Sub OnPropertyChanged(ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
            If e.Property.Equals(Title) Then RaiseEvent TitleChanged(Me, EventArgs.Empty)
            MyBase.OnPropertyChanged(e)
        End Sub
        Public Event TitleChanged As EventHandler



    End Class
    Public Class MessageBox : Inherits iMsg

        Public Overloads Overrides Sub Close(ByVal Response As System.Windows.Forms.DialogResult)

        End Sub
        <EditorBrowsable(EditorBrowsableState.Never)> Private _Window As Window
        Public Property Window() As Window
            Get
                Return _Window
            End Get
            Protected Set(ByVal value As Window)
                _Window = value
            End Set
        End Property

        <EditorBrowsable(EditorBrowsableState.Never)> Private _Control As MessageBoxImplementationControl
        Public Property Control() As MessageBoxImplementationControl
            Get
                Return _Control
            End Get
            Protected Set(ByVal value As MessageBoxImplementationControl)
                _Control = value
            End Set
        End Property

        Protected Overrides Sub PerformDialog(ByVal Modal As Boolean, ByVal Owner As System.Windows.Forms.IWin32Window)
            If State <> States.Created Then Throw New InvalidOperationException(ResourcesT.Exceptions.MessageBoxMustBeInCreatedStateInOrderToBeDisplyedByPerformDialog)
            Window = New MessageBoxWindow()
            Control = DirectCast(Window, MessageBoxWindow).MsgBoxControl
            If Modal Then
                Window.ShowDialog()
            Else
                Window.Show()
            End If
        End Sub
    End Class
End Namespace
#End If