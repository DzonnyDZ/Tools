''' <summary>Dialog to add a new favorite item</summary>
Friend Class AddFavoriteItemDialog

    Private Sub cmdFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFile.Click
        If ofdFile.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtPath.Text = ofdFile.FileName
        End If
    End Sub

    Private Sub cmdFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFolder.Click
        If fbdFolder.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtPath.Text = fbdFolder.SelectedPath
        End If
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub
    ''' <summary>CTor</summary>
    Public Sub New()
        InitializeComponent()
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        If txtName.Text.Contains("\"c) OrElse txtName.Text.Contains(":"c) OrElse txtName.Text.Contains(ChrW(0)) Then
            MsgBox(My.Resources.CharactersNotAllowed, MsgBoxStyle.Critical, My.Resources.Error_)
            Exit Sub
        End If
        If txtName.Text = "" Then
            MsgBox(My.Resources.EnterName, MsgBoxStyle.Critical, My.Resources.Error_)
            Exit Sub
        End If
        txtPath.Text = txtPath.Text.TrimEnd("\"c)
        If txtPath.Text.StartsWith("\\") AndAlso txtPath.Text.Length > 2 AndAlso txtPath.Text.IndexOf("\"c, 2) < 0 AndAlso (From ch In txtPath.Text.Substring(2) Where IO.Path.GetInvalidFileNameChars.Contains(ch)).Count = 0 Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
            Exit Sub
        End If
        Try
            If IO.File.Exists(txtPath.Text) OrElse IO.Directory.Exists(txtPath.Text) Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                MsgBox(My.Resources.SelectValidFileOrDirectoryPlase, MsgBoxStyle.Critical, My.Resources.Error_)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
        End Try
    End Sub
    ''' <summary>Gets or sets target path</summary>
    Public Property Target() As String
        Get
            Return txtPath.Text
        End Get
        Set(ByVal value As String)
            txtPath.Text = value
        End Set
    End Property
    ''' <summary>Gets or sets name of item</summary>
    Public Property ItemName() As String
        Get
            Return txtName.Text
        End Get
        Set(ByVal value As String)
            txtName.Text = value
        End Set
    End Property
End Class