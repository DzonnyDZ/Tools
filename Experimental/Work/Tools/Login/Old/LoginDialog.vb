Imports System.ComponentModel, System.Windows.Forms
Namespace Login
    ''' <summary>Pøihlašovací dialog</summary>
    <DefaultProperty("Text"), DefaultEvent("ButtonClick")> _
    Public Class LoginDialog : Inherits CommonDialog
        ''' <summary>Pøihlašovací formuláø</summary>
        Private WithEvents LoginForm As New frmLogin
        ''' <summary>(Pøed)vyplnìné heslo</summary>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property Password() As String
            Get
                Return LoginForm.Password
            End Get
            Set(ByVal value As String)
                LoginForm.Password = value
            End Set
        End Property
        ''' <summary>(Pøed) vyplnìné uživatelské jméno</summary>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property UserName() As String
            Get
                Return LoginForm.UserName
            End Get
            Set(ByVal value As String)
                LoginForm.UserName = value
            End Set
        End Property
        ''' <summary>Text v titulku pøihlašovacího okna</summary>
        <DefaultValue("EOS login")> _
        <Description("Titulek pøihlašovacího okna")> _
        Public Property Text() As String
            Get
                Return LoginForm.Text
            End Get
            Set(ByVal value As String)
                LoginForm.Text = value
            End Set
        End Property

        ''' <summary>When overridden in a derived class, resets the properties of a common dialog box to their default values.</summary>
        Public Overrides Sub Reset()
            Password = ""
            UserName = ""
        End Sub
        ''' <summary>When overridden in a derived class, specifies a common dialog box.</summary>
        ''' <param name="hwndOwner">A value that represents the window handle of the owner window for the common dialog box.</param>
        ''' <returns>true if the dialog box was successfully run; otherwise, false.</returns>
        Protected Overrides Function RunDialog(ByVal hwndOwner As System.IntPtr) As Boolean
            ShowDialog(Form.FromHandle(hwndOwner))
        End Function
        ''' <summary>Runs a common dialog box with the specified owner.</summary>
        ''' <param name="owner">Any object that implements System.Windows.Forms.IWin32Window that represents the top-level window that will own the modal dialog box.</param>
        ''' <returns>System.Windows.Forms.DialogResult.OK if the user clicks OK in the dialog box; otherwise, System.Windows.Forms.DialogResult.Cancel</returns>
        Public Shadows Function ShowDialog(Optional ByVal Owner As IWin32Window = Nothing) As DialogResult
            Return LoginForm.ShowDialog(Owner)
        End Function
        ''' <summary>Nastane pøi kliknutí na doplòkové tlaèítko</summary>
        ''' <param name="sender">Zdroj události</param>
        ''' <param name="e">Parametry</param>
        Public Event ButtonClick(ByVal sender As LoginDialog, ByVal e As LoginDialogEventArgs)

        ''' <summary>Nastane když uživatel klikne na tlaèítko OK pøiklašovacího formuláøe a umožòuje ovìøit pøihlašovací údaje pøed zavøením formuláøe</summary>
        ''' <param name="sender">zdroj</param>
        ''' <param name="e">parametry</param>
        Public Event OKClick(ByVal sender As LoginDialog, ByVal e As FormCancelEventArgs)

        Private Sub LoginForm_BeforeOK(ByVal sender As frmLogin, ByVal e As System.ComponentModel.CancelEventArgs) Handles LoginForm.BeforeOK
            Dim Mye As New FormCancelEventArgs(sender)
            RaiseEvent OKClick(Me, Mye)
            e.Cancel = Mye.Cancel
        End Sub
        Private Sub LoginForm_ButtonClick(ByVal sender As frmLogin, ByVal e As System.EventArgs) Handles LoginForm.ButtonClick
            RaiseEvent ButtonClick(Me, New LoginDialogEventArgs(sender))
        End Sub
        ''' <summary>Paramatr události <see cref="ButtonClick"/></summary>
        Public Class LoginDialogEventArgs : Inherits EventArgs
            ''' <summary>Udržuje hodnotu vlastnosti <see cref="Form"/></summary>
            Private _Form As Form
            ''' <summary>CTor</summary>
            ''' <param name="Form">Pøihlašovací formuláø</param>
           Public Sub New(ByVal Form As Form)
                _Form = Form
            End Sub
            ''' <summary>Pøihlašovací formuláø</summary>
            Public ReadOnly Property Form() As Form
                Get
                    Return _Form
                End Get
            End Property
        End Class
        ''' <summary>Paramatr události <see cref="ButtonClick"/></summary>
        Public Class FormCancelEventArgs : Inherits CancelEventArgs
            ''' <summary>Udržuje hodnotu vlastnosti <see cref="Form"/></summary>
            Private _Form As Form
            ''' <summary>CTor</summary>
            ''' <param name="Form">Pøihlašovací formuláø</param>
            Public Sub New(ByVal Form As Form)
                _Form = Form
            End Sub
            ''' <summary>Pøihlašovací formuláø</summary>
            Public ReadOnly Property Form() As Form
                Get
                    Return _Form
                End Get
            End Property
        End Class
        ''' <summary>Stav doplòujícího tlaèítka na formuláøi</summary>
        <Description("Stav doplòujícího tlaèítka na formuláøi")> _
        <DefaultValue(GetType(LoginButtonState), "Hidden")> _
        Public Property AdditionalButton() As LoginButtonState
            Get
                Return LoginForm.Button
            End Get
            Set(ByVal value As LoginButtonState)
                LoginForm.Button = value
            End Set
        End Property
        ''' <summary>Text doplòujícího tlaèítka formuláøe</summary>
        <Description("Text doplòujícího tlaèítka formuláøe")> _
        <DefaultValue("&Možnosti ...")> _
        Public Property AdditionalButtonText() As String
            Get
                Return LoginForm.ButtonText
            End Get
            Set(ByVal value As String)
                LoginForm.ButtonText = value
            End Set
        End Property
    End Class
End Namespace