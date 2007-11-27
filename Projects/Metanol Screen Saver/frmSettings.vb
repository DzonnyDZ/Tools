Friend Class frmSettings


    Private Sub frmSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtDir.Text = If(My.Settings.Folder = "", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), My.Settings.Folder)
        txtText.Text = My.Settings.InfoText
        txtMask.Text = Join(New List(Of String)(New Tools.CollectionsT.GenericT.Wrapper(Of String)(My.Settings.Mask)).ToArray, ";")
        cmdFont.Tag = My.Settings.Font.Clone
        cmdFont.Text = GetFontText(cmdFont.Tag)
        cmdBgWin.BackColor = My.Settings.BgColor
        cmdFgInfo.BackColor = My.Settings.FgColor
        cmdBgInfo.BackColor = Color.FromArgb(My.Settings.InfoBgColor.R, My.Settings.InfoBgColor.G, My.Settings.InfoBgColor.B)
        NumericUpDown1.Value = My.Settings.InfoBgColor.A
        Select Case My.Settings.InfoAlign
            Case ContentAlignment.BottomCenter : optBottomCenter.Checked = True
            Case ContentAlignment.BottomLeft : optBottomLeft.Checked = True
            Case ContentAlignment.BottomRight : optBottomRight.Checked = True
            Case ContentAlignment.MiddleCenter : optMiddleCenter.Checked = True
            Case ContentAlignment.MiddleLeft : optMiddleLeft.Checked = True
            Case ContentAlignment.MiddleRight : optMiddleRight.Checked = True
            Case ContentAlignment.TopCenter : optTopCenter.Checked = True
            Case ContentAlignment.TopLeft : optTopLeft.Checked = True
            Case ContentAlignment.TopRight : optTopRight.Checked = True
        End Select
        If My.Settings.Alghoritm = SSAverAlghoritm.Random Then
            optOrderRandom.Checked = True
        Else
            optOrderSequential.Checked = True
        End If
        nudInterval.Value = My.Settings.Timer
    End Sub


    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub cmdDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDir.Click
        If txtDir.Text <> "" Then fbdFolder.SelectedPath = txtDir.Text
        If fbdFolder.ShowDialog = Windows.Forms.DialogResult.OK Then txtDir.Text = fbdFolder.SelectedPath
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Try
            If Not IO.Directory.Exists(txtDir.Text) Then
                MsgBox("Selected folder does not exists.", MsgBoxStyle.Exclamation, "Error")
                txtDir.Select()
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, ex.GetType.Name)
            Exit Sub
        End Try
        Try
            frmSSaver.ParseText(txtText.Text, Nothing, Nothing, Nothing)
        Catch ex As frmSSaver.TextSyntaxErrorException
            MsgBox(String.Format("You have en error in info template text.{0}{1}", vbCrLf, ex.Message), MsgBoxStyle.Exclamation, "Error")
            txtText.Select()
            txtText.Select(ex.Position, 1)
        End Try
        My.Settings.Folder = txtDir.Text
        My.Settings.InfoText = txtText.Text
        My.Settings.Mask = New Specialized.StringCollection
        My.Settings.Mask.AddRange(txtMask.Text.Split(";"c))
        My.Settings.Alghoritm = If(optOrderRandom.Checked, SSAverAlghoritm.Random, SSAverAlghoritm.Sequintial)
        If nudInterval.Value = 0 AndAlso MsgBox("Setting interval to 0 causes only first image to be shown, not extremly quick slideshow. Continue?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Zero interval") <> MsgBoxResult.Yes Then Exit Sub
        My.Settings.Timer = nudInterval.Value
        My.Settings.Font = cmdFont.Tag
        My.Settings.BgColor = cmdBgWin.BackColor
        My.Settings.FgColor = cmdFgInfo.BackColor
        My.Settings.InfoBgColor = Color.FromArgb(NumericUpDown1.Value, cmdBgInfo.BackColor.R, cmdBgInfo.BackColor.G, cmdBgInfo.BackColor.B)
        Select Case True
            Case optBottomCenter.Checked : My.Settings.InfoAlign = ContentAlignment.BottomCenter
            Case optBottomLeft.Checked : My.Settings.InfoAlign = ContentAlignment.BottomLeft
            Case optBottomRight.Checked : My.Settings.InfoAlign = ContentAlignment.BottomRight
            Case optMiddleCenter.Checked : My.Settings.InfoAlign = ContentAlignment.MiddleCenter
            Case optMiddleLeft.Checked : My.Settings.InfoAlign = ContentAlignment.MiddleLeft
            Case optMiddleRight.Checked : My.Settings.InfoAlign = ContentAlignment.MiddleRight
            Case optTopCenter.Checked : My.Settings.InfoAlign = ContentAlignment.TopCenter
            Case optTopLeft.Checked : My.Settings.InfoAlign = ContentAlignment.TopLeft
            Case optTopRight.Checked : My.Settings.InfoAlign = ContentAlignment.TopRight
        End Select
        My.Settings.Save()
        Me.Close()
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub


    Private Sub Button_BackColorChanged(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdBgWin.BackColorChanged, cmdFgInfo.BackColorChanged, cmdBgInfo.BackColorChanged
        sender.ForeColor = Color.FromArgb(sender.BackColor.A, Not sender.BackColor.R, Not sender.BackColor.G, Not sender.BackColor.G)
    End Sub


    Private Sub Color_Click(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdFgInfo.Click, cmdBgWin.Click, cmdBgInfo.Click
        cdlColor.Color = sender.BackColor
        If cdlColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            sender.BackColor = cdlColor.Color
        End If
    End Sub

    Private Sub cmdFont_Click(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdFont.Click
        fdlFont.Font = sender.Tag
        If fdlFont.ShowDialog = Windows.Forms.DialogResult.OK Then
            sender.Tag = fdlFont.Font
            cmdFont.Text = GetFontText(sender.Tag)
        End If
    End Sub
    Private Function GetFontText$(ByVal f As Font)
        Dim fStr$ = ""
        If f.Italic Then fStr = ", italic"
        If f.Bold Then fStr &= If(fStr <> "", ", ", "") & "bold"
        If f.Underline Then fStr &= If(fStr <> "", ", ", "") & "underline"
        If f.Strikeout Then fStr &= If(fStr <> "", ", ", "") & "strikeout"
        Return String.Format("{0} {1}pt{2} ...", f.Name, f.SizeInPoints, fStr)
    End Function


    Private Sub cmdProperties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProperties.Click
        Dim f As New frmProperties
        frmProperties.Show(Me)
    End Sub
End Class