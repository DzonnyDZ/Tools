Imports System.Diagnostics, System.Drawing, Microsoft.VisualStudio.DebuggerVisualizers
#If Config <= Nightly Then 'Stage: Nightly
<Assembly: DebuggerVisualizer(GetType(DiagnosticsT.ImageVisualizer), GetType(Image), Description:="Image visualizer")> 
Namespace DiagnosticsT
   'ASAP:Comment,forum,wiki,mark
    Public Class ImageVisualizer
        Inherits DialogDebuggerVisualizer
        Protected Overrides Sub Show(ByVal windowService As Microsoft.VisualStudio.DebuggerVisualizers.IDialogVisualizerService, ByVal objectProvider As Microsoft.VisualStudio.DebuggerVisualizers.IVisualizerObjectProvider)

        End Sub
    End Class
End Namespace
#End If