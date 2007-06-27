Imports System.Diagnostics, System.Drawing
#If Config <= Nightly Then 'Stage: Nightly
<Assembly: DebuggerVisualizer (gettype(ImageVisualizer),gettype(Image),Description:="Image visualizer")> 
Namespace DiagnosticsT
   'ASAP:Comment,forum,wiki,mark
    Public Class ImageVisualizer
        Inherits dialogdebuggervisualizer
    End Class
End Namespace
#End If