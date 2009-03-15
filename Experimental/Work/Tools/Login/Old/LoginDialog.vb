Imports System.ComponentModel, System.Windows.Forms
Namespace Login
    ''' <summary>P�ihla�ovac� dialog</summary>
    <DefaultProperty("Text"), DefaultEvent("ButtonClick")> _
    Public Class LoginDialog : Inherits CommonDialog
        ''' <summary>P�ihla�ovac� formul��</summary>
        Private WithEvents LoginForm As New frmLogin
        ''' <summary>(P�ed)vypln�n� heslo</summary>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property Password() As String
            Get
                Return LoginForm.Password
            End Get
            Set(ByVal value As String)
                LoginForm.Password = value
            End Set
        End Property
        ''' <summary>(P�ed) vypln�n� u�ivatelsk� jm�no</summary>
        <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property UserName() As String
            Get
                Return LoginForm.UserName
            End Get
            Set(ByVal value As String)
                LoginForm.UserName = value
            End Set
        End Property
        ''' <summary>Text v titulku p�ihla�ovac�ho okna</summary>
        <DefaultValue("EOS login")> _
        <Description("Titulek p�ihla�ovac�ho okna")> _
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
        ''' <summary>Nastane p�i kliknut� na dopl�kov� tla��tko</summary>
        ''' <param name="sender">Zdroj ud�losti</param>
        ''' <param name="e">Parametry</param>
        Public Event ButtonClick(ByVal sender As LoginDialog, ByVal e As LoginDialogEventArgs)

        ''' <summary>Nastane kdy� u�ivatel klikne na tla��tko OK p�ikla�ovac�ho formul��e a umo��uje ov��it p�ihla�ovac� �daje p�ed zav�en�m formul��e</summary>
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
        ''' <summary>Paramatr ud�losti <see cref="ButtonClick"/></summary>
        Public Class LoginDialogEventArgs : Inherits EventArgs
            ''' <summary>Udr�uje hodnotu vlastnosti <see cref="Form"/></summary>
            Private _Form As Form
            ''' <summary>CTor</summary>
            ''' <param name="Form">P�ihla�ovac� formul��</param>
           Public Sub New(ByVal Form As Form)
                _Form = Form
            End Sub
            ''' <summary>P�ihla�ovac� formul��</summary>
            Public ReadOnly Property Form() As Form
                Get
                    Return _Form
                End Get
            End Property
        End Class
        ''' <summary>Paramatr ud�losti <see cref="ButtonClick"/></summary>
        Public Class FormCancelEventArgs : Inherits CancelEventArgs
            ''' <summary>Udr�uje hodnotu vlastnosti <see cref="Form"/></summary>
            Private _Form As Form
            ''' <summary>CTor</summary>
            ''' <param name="Form">P�ihla�ovac� formul��</param>
            Public Sub New(ByVal Form As Form)
                _Form = Form
            End Sub
            ''' <summary>P�ihla�ovac� formul��</summary>
            Public ReadOnly Property Form() As Form
                Get
                    Return _Form
                End Get
            End Property
        End Class
        ''' <summary>Stav dopl�uj�c�ho tla��tka na formul��i</summary>
        <Description("Stav dopl�uj�c�ho tla��tka na formul��i")> _
        <DefaultValue(GetType(LoginButtonState), "Hidden")> _
        Public Property AdditionalButton() As LoginButtonState
            Get
                Return LoginForm.Button
            End Get
            Set(ByVal value As LoginButtonState)
                LoginForm.Button = value
            End Set
        End Property
        ''' <summary>Text dopl�uj�c�ho tla��tka formul��e</summary>
        <Description("Text dopl�uj�c�ho tla��tka formul��e")> _
        <DefaultValue("&Mo�nosti ...")> _
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