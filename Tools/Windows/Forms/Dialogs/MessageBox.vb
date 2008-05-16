Imports System.Windows.Forms, Tools.WindowsT, System.ComponentModel
#If Config <= Nightly Then
Namespace WindowsT.FormsT
    ''' <summary>Implements GUI (form) for <see cref="MessageBox"/></summary>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Class MessageBoxForm : Inherits Form
        ''' <summary>Contains value of the <see cref="MessageBox"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _MessageBox As MessageBox
        ''' <summary>Gets <see cref="FormsT.MessageBox"/> this forms is GUI for</summary>
        Public ReadOnly Property MessageBox() As MessageBox
            <DebuggerStepThroughAttribute()> Get
                Return _MessageBox
            End Get
        End Property
        ''' <summary>CTor</summary>
        ''' <param name="MessageBox"><see cref="FormsT.MessageBox"/> to initialize this form by</param>
        Public Sub New(ByVal MessageBox As MessageBox)
            Me._MessageBox = MessageBox
            Me.InitializeComponent()
            Initialize()
        End Sub
        ''' <summary>Initializes tis form by <see cref="MessageBox"/>, called from CTor</summary>
        Protected Overridable Sub Initialize()
         
        End Sub
    End Class

    ''' <summary>Implements <see cref="WindowsT.DialogsT.MessageBox"/> for as <see cref="Form"/></summary>
    Public Class MessageBox
        Inherits DialogsT.MessageBoxBase(Of MessageBox)
        Public Overrides ReadOnly Property ClickedButton() As DialogsT.MessageBox.MessageBoxButton
            Get

            End Get
        End Property

        Public Overloads Overrides Sub Close(ByVal Response As System.Windows.Forms.DialogResult)

        End Sub

        Public Overrides ReadOnly Property DialogResult() As System.Windows.Forms.DialogResult
            Get

            End Get
        End Property
        ''' <summary>Contains value of the <see cref="Form"/> property</summary>
        Private _Form As MessageBoxForm
        ''' <summary>Gets or sets instance of <see cref="MessageBoxForm"/> that currently shows the message box</summary>
        ''' <value>This property should be set only from overriden <see cref="PerformDialog"/> when it does not calls base class method</value>
        Public Property Form() As MessageBoxForm
            Get
                Return _Form
            End Get
            Protected Set(ByVal value As MessageBoxForm)
                _Form = value
            End Set
        End Property

        ''' <summary>Shows the dialog</summary>
        ''' <param name="Modal">Indicates if dialog should be shown modally (true) or modells (false)</param>
        ''' <param name="Owner">Parent window of dialog (may be null)</param>
        ''' <remarks>Note for inheritors: If you override thie method and do not call base class method, you must set value of the <see cref="Form"/> property</remarks>
        Protected Overrides Sub PerformDialog(ByVal Modal As Boolean, ByVal Owner As System.Windows.Forms.IWin32Window)
            Form = New MessageBoxForm(Me)
            If Modal Then
                Form.ShowDialog(Owner)
            Else
                Form.Show(Owner)
            End If
        End Sub
    End Class
End Namespace
#End If