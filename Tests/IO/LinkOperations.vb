Imports mBox = Tools.WindowsT.IndependentT.MessageBox
Imports Tools.IOt

Namespace IOt
    Public Class LinkOperations
        Public Shared Sub Test()
            Using f As New LinkOperations
                f.ShowDialog()
            End Using
        End Sub

        Private Sub btnSrc_Click(sender As Object, e As EventArgs) Handles btnSrc.Click
            If fbdSelectFolder.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                txtSrc.Text = fbdSelectFolder.SelectedPath
            End If
        End Sub

        Private Sub btnDest_Click(sender As Object, e As EventArgs) Handles btnDest.Click
            If fbdSelectFolder.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                txtDest.Text = fbdSelectFolder.SelectedPath
            End If
        End Sub

        Private Sub cmdSym2Lnk_Click(sender As Object, e As EventArgs) Handles cmdSym2Lnk.Click
            Try
                For Each fse In IO.Directory.EnumerateFileSystemEntries(txtSrc.Text, txtMask.Text, If(chkRecursive.Checked, IO.SearchOption.AllDirectories, IO.SearchOption.TopDirectoryOnly))
                    Dim target = FileSystemTools.ResolveSymbolicLink(fse)
                    If target <> "" Then
                        ShellLink.CreateLink(target, IO.Path.Combine(txtDest.Text, fse.Substring(txtSrc.Text.Length + 1)) & ".lnk")
                    End If
                Next
            Catch ex As Exception
                mBox.Error_X(ex)
            End Try
        End Sub

        Private Sub cmdLnk2Sym_Click(sender As Object, e As EventArgs) Handles cmdLnk2Sym.Click
            Try
                For Each fse In IO.Directory.EnumerateFileSystemEntries(txtSrc.Text, txtMask.Text, If(chkRecursive.Checked, IO.SearchOption.AllDirectories, IO.SearchOption.TopDirectoryOnly))
                    Dim link As New ShellLink(fse)
                    Dim relativePath = fse.Substring(txtSrc.Text.Length + 1)
                    FileSystemTools.CreateSymbolicLink(
                        link.TargetPath,
                        IO.Path.Combine(txtDest.Text, IO.Path.GetDirectoryName(relativePath), IO.Path.GetFileNameWithoutExtension(relativePath) & IO.Path.GetExtension(link.TargetPath))
                    )
                Next
            Catch ex As Exception
                mBox.Error_X(ex)
            End Try
        End Sub

        'Private Sub cmdLnk2Relative_Click(sender As Object, e As EventArgs) Handles cmdLnk2Relative.Click
        '    Try
        '        For Each fse In IO.Directory.EnumerateFiles(txtSrc.Text, txtMask.Text, If(chkRecursive.Checked, IO.SearchOption.AllDirectories, IO.SearchOption.TopDirectoryOnly))
        '            Dim link As New ShellLink(fse)
        '            Dim difference = Path.GetDifference(IO.Path.GetDirectoryName(fse), link.TargetPath)
        '        Next
        '    Catch ex As Exception
        '        mBox.Error_X(ex)
        '    End Try
        'End Sub
    End Class
End Namespace