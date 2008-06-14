Imports Tools.IOt
Namespace IOt
    '#If Config <= Nightly Then Stage conditional compilation of this file is set in Tests.vbproj
    ''' <summary>Tests <see cref="Tools.IOt.ShellLink"/></summary>
    Public Class frmShellLink
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub
        ''' <summary>Runs test</summary>
        Public Shared Sub test()
            Dim frm As New frmShellLink
            frm.Show()
        End Sub

        'Private _link As ShellLink
        'Private Property Link() As ShellLink
        '    Get
        '        Return _link
        '    End Get
        '    Set(ByVal value As ShellLink)
        '        If value IsNot Link AndAlso Link IsNot Nothing Then Link.Dispose()
        '        cmdSave.Enabled = value IsNot Nothing
        '        prgGrid.SelectedObject = value
        '        _link = value
        '    End Set
        'End Property

        'Private Sub cmdCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreate.Click
        '    If ofdSelectFile.ShowDialog = Windows.Forms.DialogResult.OK AndAlso sfdSaveLink.ShowDialog = Windows.Forms.DialogResult.OK Then
        '        Try
        '            Link = ShellLink.CreateLink(ofdSelectFile.FileName, sfdSaveLink.FileName)
        '        Catch ex As Exception
        '            Tools.WindowsT.IndependentT.MessageBox.Error(ex)
        '        End Try
        '    End If
        'End Sub

        'Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        '    If ofdOpenLink.ShowDialog = Windows.Forms.DialogResult.OK Then
        '        Try
        '            Link = New ShellLink(ofdOpenLink.FileName)
        '        Catch ex As Exception
        '            Tools.WindowsT.IndependentT.MessageBox.Error(ex)
        '        End Try
        '    End If
        'End Sub

        'Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        '    Try
        '        Link.Save()
        '    Catch ex As Exception
        '        Tools.WindowsT.IndependentT.MessageBox.Error(ex)
        '    End Try
        'End Sub
    End Class
End Namespace