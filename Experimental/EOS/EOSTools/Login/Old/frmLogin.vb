Namespace Login
    ''' <summary>Pøihlašovací formuláø</summary>
    Friend Class frmLogin
        ''' <summary>(Pøed)vyplnìné heslo</summary>
        Public Property Password() As String
            Get
                Return Me.txtPwd.Text
            End Get
            Set(ByVal value As String)
                Me.txtPwd.Text = value
            End Set
        End Property

        ''' <summary>(Pøed)vyplnìné uživatelské jméno</summary>
        Public Property UserName() As String
            Get
                Return Me.txtUN.Text
            End Get
            Set(ByVal value As String)
                Me.txtUN.Text = value
            End Set
        End Property

        ''' <summary>Nastane po té, co uživatel klikne na tlaèítko OK, ale pøed zavøením formuláøe. Umožòuje ovìøit heslo a zabránit zavøení formuláøe</summary>
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
        ''' <summary>Stav doplòkového tlaèítka</summary>
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
        ''' <summary>Nastane pøi kliknutí na doplòkové tlaèítko</summary>
        ''' <param name="sender">zdroj události</param>
        ''' <param name="e">parametry</param>
        Public Event ButtonClick(ByVal sender As frmLogin, ByVal e As EventArgs)
        Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdButton.Click
            RaiseEvent ButtonClick(Me, e)
        End Sub
        ''' <summary>Text doplòkového tlaèítka</summary>
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
    ''' <summary>Stavy dopòkového tlaèítka na pøihlašovacím formláøi</summary>
    Public Enum LoginButtonState
        ''' <summary>Skryté</summary>
        Hidden
        ''' <summary>Viditelné, ale nepøístupné</summary>
        Disabled
        ''' <summary>Viditelné a pøístupné</summary>
        Visible
    End Enum
End Namespace