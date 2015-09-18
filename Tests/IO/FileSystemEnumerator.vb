'#If True
Imports Tools.IOt
Namespace IOt
    ''' <summary>Test for <see cref="Tools.IOt.FileSystemEnumerator"/></summary>
    Public Class frmFileSystemEnumerator
        ''' <summary>CTor</summary>
        Public Sub New()
            InitializeComponent()
            Me.Icon = Tools.ResourcesT.ToolsIcon
        End Sub
        ''' <summary>Runs test</summary>
        Public Shared Sub Test()
            Dim f As New frmFileSystemEnumerator
            f.ShowDialog()
        End Sub

        Private Sub cmdRoot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRoot.Click
            fbdRoot.SelectedPath = txtRoot.Text
            If fbdRoot.ShowDialog = System.Windows.Forms.DialogResult.OK Then _
                txtRoot.Text = fbdRoot.SelectedPath
        End Sub

        Private Sub cmdGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGo.Click
            lstList.Items.Clear()
            Dim en As FileSystemEnumerator = GetEnumerator()
            Dim i As Integer = 0
            While en.MoveNext
                lstList.Items.Add(New String("-"c, en.CurrentLevel) & en.CurrentPath.FileName)
                i += 1
            End While
        End Sub
        ''' <summary>Get enumerator for file system</summary>
        Protected Overridable Function GetEnumerator() As FileSystemEnumerator
            Return New FileSystemEnumerator(txtRoot.Text, chkFoldersFirst.Checked)
        End Function
    End Class
End Namespace