Public Class SSaver : Inherits Tools.WindowsT.FormsT.ScreenSaverBase
    <STAThread()> _
    Public Shared Sub Main()
        Application.EnableVisualStyles()
        Run(Of SSaver)(My.Application.CommandLineArgs.ToArray)
    End Sub
    Protected Overrides Sub FormatScreenForm(ByVal Form As System.Windows.Forms.Form)
    End Sub
    Protected Overrides Function GetScreenForm(ByVal Screen As System.Windows.Forms.Screen) As System.Windows.Forms.Form
        Return New frmSSaver
    End Function

    Protected Overrides ReadOnly Property SettingsForm() As System.Windows.Forms.Form
        Get
            Return New frmSettings
        End Get
    End Property
    Protected Overrides ReadOnly Property ThreadingApartment() As System.Threading.ApartmentState
        Get
            Return Threading.ApartmentState.STA
        End Get
    End Property
End Class


Friend Enum SSAverAlghoritm
    Random
    Sequintial
End Enum