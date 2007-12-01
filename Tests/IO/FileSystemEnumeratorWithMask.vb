#If Config <= Nightly Then
Imports Tools.IOt
Namespace IOt
    ''' <summary>Test <see cref="FileSystemEnumeratorWithMask"/></summary>
    Public Class frmFileSystemEnumeratorWithMask
        ''' <summary>Run test</summary>
        Public Shared Shadows Sub Test()
            Dim f As New frmFileSystemEnumeratorWithMask
            f.ShowDialog()
        End Sub
        Protected Overrides Function GetEnumerator() As FileSystemEnumerator
            Dim e As New FileSystemEnumeratorWithMask(txtRoot.Text, txtFileMask.Text, ";"c, chkFoldersFirst.Checked)
            If txtRoot.Text = "" Then e.FilesMasks = New String() {}
            e.FoldersEnterMasks = If(txtFolderContentMask.Text = "", New String() {}, txtFolderContentMask.Text.Split(";"c))
            e.FoldersListMasks = If(txtFolderItselfMask.Text = "", New String() {}, txtFolderItselfMask.Text.Split(";"c))
            Return e
        End Function
    End Class
End Namespace
#End If