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
            If fbdSelectFolder.ShowDialog = Windows.Forms.DialogResult.OK Then
                txtSrc.Text = fbdSelectFolder.SelectedPath
            End If
        End Sub

        Private Sub btnDest_Click(sender As Object, e As EventArgs) Handles btnDest.Click
            If fbdSelectFolder.ShowDialog = Windows.Forms.DialogResult.OK Then
                txtDest.Text = fbdSelectFolder.SelectedPath
            End If
        End Sub

        Private Sub cmdSym2Lnk_Click(sender As Object, e As EventArgs) Handles cmdSym2Lnk.Click
            Try
                For Each fse In IO.Directory.EnumerateFileSystemEntries(txtSrc.Text, txtMask.Text, If(chkRecursive.Checked, IO.SearchOption.AllDirectories, IO.SearchOption.TopDirectoryOnly))
                    Dim target = FileSystemTools.ResolveSymbolicLink(fse)
                    If target <> "" Then
                        ShellLink.CreateLink(target, IO.Path.Combine(txtDest.Text, fse.Substring(txtSrc.Text.Length)))
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
                    FileSystemTools.CreateSymbolicLink(link.TargetPath, IO.Path.Combine(txtDest.Text, fse.Substring(txtSrc.Text.Length)))
                Next
            Catch ex As Exception
                mBox.Error_X(ex)
            End Try
        End Sub

        Private Sub cmdLnk2Relative_Click(sender As Object, e As EventArgs) Handles cmdLnk2Relative.Click
            Try
                For Each fse In IO.Directory.EnumerateFileSystemEntries(txtSrc.Text, txtMask.Text, If(chkRecursive.Checked, IO.SearchOption.AllDirectories, IO.SearchOption.TopDirectoryOnly))
                    Dim link As New ShellLink(fse)
                    If IO.Path.IsPathRooted(link.TargetPath) Then
                        If fse.StartsWith("//") <> link.TargetPath.StartsWith("//") Then Continue For
                        Dim linkSegments = fse.Split("/"c)
                        Dim targetSegments = fse.Split("/"c)
                        For i = 0 To Math.Min(linkSegments.Length, targetSegments.Length) - 1
                            If Not StringComparer.CurrentCultureIgnoreCase.Equals(linkSegments(i), targetSegments(i)) Then
                                If i > 0 Then
                                    'TODO:
                                End If
                                Exit For
                            End If
                        Next
                    End If
                Next
            Catch ex As Exception
                mBox.Error_X(ex)
            End Try
        End Sub
    End Class
End Namespace