Imports Tools.ResourcesT
Namespace ResourcesT
    Public Class frmSystemResources
        ''' <summary>Show test form</summary>
        Public Shared Sub Test()
            Dim frm As New frmSystemResources
            frm.ShowDialog()
        End Sub
        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = Tools.ResourcesT.ToolsIcon

            Dim b As New System.Text.StringBuilder
            For Each kName As String In SystemResources.KeyNames
                b.AppendLine(String.Format("{0} = ""{1}""", kName, SystemResources.Key(kName)))
            Next kName
            txtResources.Text = b.ToString
        End Sub

        Private Sub cmdGet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGet.Click
            Try
                MsgBox(SystemResources.Value(txtResourceName.Text))
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
            End Try
        End Sub

        Private Sub cmdKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdKey.Click
            Try
                MsgBox(SystemResources.Key(txtKey.Text))
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
            End Try
        End Sub

        Private Sub cmdAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAll.Click
            Dim b As New System.Text.StringBuilder
            Dim oldC As Globalization.CultureInfo = Threading.Thread.CurrentThread.CurrentUICulture
            Threading.Thread.CurrentThread.CurrentUICulture = Globalization.CultureInfo.InvariantCulture  'New Globalization.CultureInfo("en-US")
            Try
                For Each kName As String In SystemResources.KeyNames
                    Dim Key As String = SystemResources.Key(kName)
                    Dim Value As String = SystemResources.ObjValue(Key).ToString
                    b.AppendLine("''' <summary>" & String.Format("Key for resource getting something like ""{0}""", Value).Replace("&", "&amp;").Replace("<", "&lt;").Replace(vbCr, "&#" & AscW(vbCr) & ";").Replace(vbLf, "&#" & AscW(vbLf) & ";") & "</summary>")
                    b.AppendLine(String.Format("Public Const {0}$ = ""{1}""", kName, Key))
                Next kName
                txtResources.Text = b.ToString
            Finally
                Threading.Thread.CurrentThread.CurrentUICulture = oldC
            End Try
        End Sub
    End Class
End Namespace