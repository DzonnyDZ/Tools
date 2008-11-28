Namespace Framework.SystemF
    ''' <summary>Tests <see cref="Type.GetTypeFromCLSID"/></summary>
    Public Class frmGetTypeFromCLSID
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            Me.Icon = Tools.ResourcesT.ToolsIcon

        End Sub
        ''' <summary>Runs test</summary>
        Public Shared Sub Test()
            Dim Inst As New frmGetTypeFromCLSID
            Inst.ShowDialog()
        End Sub

        Private Sub cmdGetType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetType.Click
            Try
                Dim t As Type = Type.GetTypeFromCLSID(New Guid(txtGuid.Text))
                prgType.SelectedObject = t
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                prgType.SelectedObject = Nothing
            End Try
            prgInstance.SelectedObject = Nothing
        End Sub

        Private Sub cmdInstance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInstance.Click
            Try
                prgInstance.SelectedObject = DirectCast(prgType.SelectedObject, Type).CreateInstance
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                prgInstance.SelectedObject = Nothing
            End Try
        End Sub
    End Class
End Namespace