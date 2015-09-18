Imports Tools.IOt
Namespace IOt
    '#If TrueStage conditional compilation of this file is set in Tests.vbproj
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
        ''' <summary>Contains value of the <see cref="Link"/> property</summary>
        Private _link As ShellLink
        ''' <summary>Current link</summary>
        Private Property Link() As ShellLink
            Get
                Return _link
            End Get
            Set(ByVal value As ShellLink)
                If value IsNot Link AndAlso Link IsNot Nothing Then Link.Dispose()
                cmdSave.Enabled = value IsNot Nothing
                cmdSaveAs.Enabled = value IsNot Nothing
                prgGrid.SelectedObject = value
                _link = value
            End Set
        End Property

        Private Sub cmdCreate_Click(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdCreate.Click
            If ofdSelectFile.ShowDialog = System.Windows.Forms.DialogResult.OK AndAlso sfdSaveLink.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Try
                    Link = ShellLink.CreateLink(ofdSelectFile.FileName, sfdSaveLink.FileName)
                Catch ex As Exception
                    Tools.WindowsT.IndependentT.MessageBox.[Error_X](ex)
                End Try
            End If
        End Sub

        Private Sub cmdOpen_Click(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdOpen.Click
            If ofdOpenLink.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Try
                    Link = New ShellLink(ofdOpenLink.FileName)
                Catch ex As Exception
                    Tools.WindowsT.IndependentT.MessageBox.[Error_X](ex)
                End Try
            End If
        End Sub

        Private Sub cmdSave_Click(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdSave.Click
            Try
                Link.Save()
            Catch ex As Exception
                Tools.WindowsT.IndependentT.MessageBox.[Error_X](ex)
            End Try
        End Sub

        Private Sub cmdSaveAs_Click(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdSaveAs.Click
            If sfdSaveLink.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Try
                    Link.SaveAs(sfdSaveLink.FileName)
                Catch ex As Exception
                    Tools.WindowsT.IndependentT.MessageBox.[Error_X](ex)
                End Try
            End If
        End Sub


        Private Sub cmdOpenByPath_Click(ByVal sender As Button, ByVal e As System.EventArgs) Handles cmdOpenByPath.Click
            Dim result = InputBox("Etner path", "Open link")
            If result = "" Then Return
            Try
                Link = New ShellLink(result)
            Catch ex As Exception
                Tools.WindowsT.IndependentT.MessageBox.[Error_X](ex)
            End Try
        End Sub
    End Class
End Namespace