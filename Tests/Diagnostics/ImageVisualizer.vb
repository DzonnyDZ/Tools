Imports Microsoft.VisualStudio.DebuggerVisualizers, Tools.ResourcesT, Tools.DiagnosticsT
'#If Config <= Nightly is set in Tests.vbproj
Namespace DiagnosticsT
    ''' <summary>Testing <see cref="ImageVisualizer"/></summary>
    ''' <remarks><seealso>http://msdn2.microsoft.com/en-us/library/zayyhzts.aspx</seealso></remarks>
    Public Class frmImageVisualizer

        ''' <summary>CTor</summary>
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Icon = ToolsIcon
        End Sub
        ''' <summary>Runs test</summary>
        Public Shared Sub Test()
            Dim frm As New frmImageVisualizer
            frm.Show()
        End Sub

        Private Sub cmdBw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBw.Click
            If ofdOpen.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim img As Bitmap
                Try
                    img = New Bitmap(ofdOpen.FileName)
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                    Exit Sub
                End Try
                picImg.Image = img
                Try
                    Dim dh As New VisualizerDevelopmentHost(img, GetType(ImageVisualizer), GetType(ImageVisualizer.VisualizerImageSource))
                    dh.ShowVisualizer()
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                    Exit Sub
                End Try
            End If
        End Sub
    End Class
End Namespace