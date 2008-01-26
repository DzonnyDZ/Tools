Imports System.Diagnostics, System.Drawing, Microsoft.VisualStudio.DebuggerVisualizers, System.Windows.Forms
#If Config <= Nightly Then 'Stage: Nightly
<Assembly: DebuggerVisualizer(GetType(DiagnosticsT.ImageVisualizer), GetType(DiagnosticsT.ImageVisualizer.VisualizerImageSource), Description:="Image visualizer", Target:=GetType(Image))> 
Namespace DiagnosticsT
    'TODO: ImageVisualizer Testing needed
    ''' <summary>Implements <see cref="DialogDebuggerVisualizer"/> for datatype <see cref="Image"/></summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(0, 0, GetType(ImageVisualizer), LastChange:="07/22/2007")> _
    <FirstVersion("06/27/2007")> _
    Public Class ImageVisualizer
        Inherits DialogDebuggerVisualizer
        ''' <summary>Shows visualizer dialog</summary>
        ''' <param name="objectProvider">An object of type <see cref="Microsoft.VisualStudio.DebuggerVisualizers.IVisualizerObjectProvider"/>. This object provides communication from the debugger side of the visualizer to the object source (<see cref="Microsoft.VisualStudio.DebuggerVisualizers.VisualizerObjectSource"/>) on the debuggee side.</param>
        ''' <param name="windowService">An object of type <see cref="Microsoft.VisualStudio.DebuggerVisualizers.IDialogVisualizerService"/>, which provides methods your visualizer can use to display Windows forms, controls, and dialogs.</param>
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
        ''' <summary>Realizes <see cref="Form"/> that shows <see cref="Image"/> being visualized</summary>
        Protected Class VisualizerForm : Inherits Form
            ''' <summary><see cref="PictureBox"/> to show <see cref="Drawing.Image"/> in</summary>
            Private Image As New PictureBox
            ''' <summary><see cref="Label"/> that displays additional information about <see cref="Drawing.Image"/></summary>
            Private Label As New Label
            ''' <summary>CTor</summary>
            ''' <param name="Image"><see cref="Drawing.Image"/> to be shown</param>
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
        ''' <summary>Implements <see cref="VisualizerObjectSource"/> for <see cref="Image"/></summary>
        Public Class VisualizerImageSource
            Inherits VisualizerObjectSource
            ''' <summary>Writes data that represents <paramref name="target"/> into <paramref name="outgoingData"/></summary>
            ''' <param name="outgoingData">Outgoing data stream.</param>
            ''' <param name="target">Object being visualized</param>
            Public Overrides Sub GetData(ByVal target As Object, ByVal outgoingData As System.IO.Stream)
                DirectCast(target, Image).Save(outgoingData, Drawing.Imaging.ImageFormat.Png)
            End Sub
        End Class
    End Class
End Namespace
#End If