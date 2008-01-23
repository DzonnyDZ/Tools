Partial Friend Class LabelSettings
    Private Sub cmdColor_BackColorChanged(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdForeColor.BackColorChanged, cmdBackColor.BackColorChanged
        sender.ForeColor = Color.FromArgb(Not sender.BackColor.R, Not sender.BackColor.G, Not sender.BackColor.B)
    End Sub

    Private Sub cmdColor_Click(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdForeColor.Click, cmdBackColor.Click
        cdlColor.Color = sender.BackColor
        If cdlColor.ShowDialog = DialogResult.OK Then
            sender.BackColor = cdlColor.Color
        End If
    End Sub

    Private Sub cmdFont_Click(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdFont.Click
        fdlFont.Font = sender.Tag
        If fdlFont.ShowDialog = DialogResult.OK Then
            sender.Tag = fdlFont.Font
            sender.Text = FontText(fdlFont.Font)
        End If
    End Sub
    Private Shared Function FontText(ByVal font As Font) As String
        Return String.Format("{0} {1}pt {2}...", font.Name, font.SizeInPoints, If(font.Bold, "B", "") & If(font.Italic, "I", "") & If(font.Underline, "U", "") & If(font.Strikeout, "S", ""))
    End Function

    Private Sub chkEnabled_CheckedChanged(ByVal sender As CheckBox, ByVal e As System.EventArgs) Handles chkEnabled.CheckedChanged
        fraMain.Enabled = sender.Checked
    End Sub

    Private Sub cmdDesigner_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDesigner.Click
        frmProperties.ShowInstance(txtText)
    End Sub

    Private Sub txtText_Enter(ByVal sender As TextBox, ByVal e As System.EventArgs) Handles txtText.Enter
        If frmProperties.IsInstance Then
            frmProperties.ShowInstance(sender)
        End If
    End Sub

    Private Sub tabMain_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabMain.SelectedIndexChanged
        cmdDesigner.Visible = tabMain.SelectedTab Is tapText
    End Sub

    Public Property Settings() As Settings_Label.Structure
        Get
            Dim ls As New Settings_Label.Structure
            ls.Enabled = chkEnabled.Checked
            If Not ls.Enabled Then Return ls
            ls.FgColor = Me.cmdForeColor.BackColor
            ls.FgColor = Color.FromArgb(nudBackTransp.Value, cmdBackColor.BackColor)
            ls.Font = cmdFont.Tag
            ls.Text = txtText.Text
            Return ls
        End Get
        Set(ByVal value As Settings_Label.Structure)
            chkEnabled.Checked = value.Enabled
            If Not value.Enabled Then Return
            cmdForeColor.BackColor = value.FgColor
            cmdBackColor.BackColor = Color.FromArgb(255, value.BgColor)
            nudBackTransp.Value = value.BgColor.A
            txtText.Text = value.Text
            cmdFont.Tag = value.Font
            cmdFont.Text = FontText(value.Font)
        End Set
    End Property

    Private Sub cmdColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdForeColor.Click, cmdBackColor.Click

    End Sub
End Class
