Namespace Login
    ''' <summary>P�ihla�ovac� formul��</summary>
    Friend Class frmLogin
        ''' <summary>(P�ed)vypln�n� heslo</summary>
        Public Property Password() As String
            Get
                Return Me.txtPwd.Text
            End Get
            Set(ByVal value As String)
                Me.txtPwd.Text = value
            End Set
        End Property

        ''' <summary>(P�ed)vypln�n� u�ivatelsk� jm�no</summary>
        Public Property UserName() As String
            Get
                Return Me.txtUN.Text
            End Get
            Set(ByVal value As String)
                Me.txtUN.Text = value
            End Set
        End Property

        ''' <summary>Nastane po t�, co u�ivatel klikne na tla��tko OK, ale p�ed zav�en�m formul��e. Umo��uje ov��it heslo a zabr�nit zav�en� formul��e</summary>
        ''' <param name="sender">zdroj</param>
        ''' <param name="e">parametry</param>
        Public Event BeforeOK(ByVal sender As frmLogin, ByVal e As System.ComponentModel.CancelEventArgs)

        Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
            Dim Mye As New System.ComponentModel.CancelEventArgs
            RaiseEvent BeforeOK(Me, Mye)
            If Not Mye.Cancel Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        End Sub

        Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub
        ''' <summary>Stav dopl�kov�ho tla��tka</summary>
        Public Property Button() As LoginButtonState
            Get
                Select Case True
                    Case cmdButton.Visible And cmdButton.Enabled
                        Return LoginButtonState.Visible
                    Case Not cmdButton.Visible
                        Return LoginButtonState.Hidden
                    Case Else
                        Return LoginButtonState.Disabled
                End Select
            End Get
            Set(ByVal value As LoginButtonState)
                Select Case value
                    Case LoginButtonState.Disabled
                        cmdButton.Visible = True
                        cmdButton.Enabled = False
                    Case LoginButtonState.Hidden
                        cmdButton.Visible = False
                    Case LoginButtonState.Visible
                        cmdButton.Enabled = True
                        cmdButton.Visible = True
                End Select
            End Set
        End Property
        ''' <summary>Nastane p�i kliknut� na dopl�kov� tla��tko</summary>
        ''' <param name="sender">zdroj ud�losti</param>
        ''' <param name="e">parametry</param>
        Public Event ButtonClick(ByVal sender As frmLogin, ByVal e As EventArgs)
        Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdButton.Click
            RaiseEvent ButtonClick(Me, e)
        End Sub
        ''' <summary>Text dopl�kov�ho tla��tka</summary>
        Public Property ButtonText() As String
            Get
                Return cmdButton.Text
            End Get
            Set(ByVal value As String)
                cmdButton.Text = value
            End Set
        End Property
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            'GUI.Misc.HandlePaint(New Drawing.Size(3, 3), Me)
        End Sub
    End Class
    ''' <summary>Stavy dop�kov�ho tla��tka na p�ihla�ovac�m forml��i</summary>
    Public Enum LoginButtonState
        ''' <summary>Skryt�</summary>
        Hidden
        ''' <summary>Viditeln�, ale nep��stupn�</summary>
        Disabled
        ''' <summary>Viditeln� a p��stupn�</summary>
        Visible
    End Enum
End Namespace