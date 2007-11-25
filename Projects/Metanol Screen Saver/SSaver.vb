Public Class SSaver : Inherits Tools.WindowsT.FormsT.ScreenSaverBase
    Public Shared Sub Main()
        Application.EnableVisualStyles()
        Run(Of SSaver)(My.Application.CommandLineArgs.ToArray)
    End Sub
    Protected Overrides Sub FormatScreenForm(ByVal Form As System.Windows.Forms.Form)
        MyBase.FormatScreenForm(Form)
        Form.BackColor = Color.Yellow
    End Sub
    Protected Overrides ReadOnly Property SettingsForm() As System.Windows.Forms.Form
        Get
            Return New frmSettings
        End Get
    End Property
End Class
