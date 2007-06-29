Imports System.Diagnostics, System.Drawing, Microsoft.VisualStudio.DebuggerVisualizers, System.Windows.Forms
#If Config <= Nightly Then 'Stage: Nightly
<Assembly: DebuggerVisualizer(GetType(DiagnosticsT.ImageVisualizer), GetType(VisualizerObjectSource), Description:="Image visualizer", Target:=GetType(Image))> 
Namespace DiagnosticsT
   'ASAP:Comment,forum,wiki,mark
    Public Class ImageVisualizer
        Inherits DialogDebuggerVisualizer
        Protected Overrides Sub Show(ByVal windowService As Microsoft.VisualStudio.DebuggerVisualizers.IDialogVisualizerService, ByVal objectProvider As Microsoft.VisualStudio.DebuggerVisualizers.IVisualizerObjectProvider)
            Dim frm As VisualizerForm
            Dim bmp As Image
            Try
                bmp = New Bitmap(objectProvider.GetData)
            Catch
                Try
                    bmp = objectProvider.GetObject
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, ex.GetType.Name)
                    Exit Sub
                End Try
            End Try
            frm = New VisualizerForm(bmp)
            windowService.ShowDialog(frm)
        End Sub
        Protected Class VisualizerForm : Inherits Form
            Private Image As New PictureBox
            Private Label As New Label
            Public Sub New(ByVal Image As Image)
                Me.Image.Image = Image
                Me.Image.Dock = DockStyle.Fill
                Me.Image.SizeMode = PictureBoxSizeMode.Zoom
                Me.Controls.Add(Me.Image)
                Me.Text = "Image visualizer"
                Me.Label.Dock = DockStyle.Bottom
                Me.Label.Text = Image.Size.ToString
                Me.Controls.Add(Label)
                Me.Icon = Tools.ResourcesT.Resources.ToolsIcon
                Me.ShowInTaskbar = False
                Me.MinimizeBox = False
                Me.MaximizeBox = False
            End Sub
        End Class
    End Class
    Public Class VisualizerImageSource
        Inherits VisualizerObjectSource
        Public Overrides Sub GetData(ByVal target As Object, ByVal outgoingData As System.IO.Stream)
            DirectCast(target, Image).Save(outgoingData, Drawing.Imaging.ImageFormat.Png)
        End Sub
    End Class
End Namespace
#End If