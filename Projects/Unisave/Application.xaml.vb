Imports <xmlns:config="http://schemas.microsoft.com/.NetConfiguration/v2.0">
Imports <xmlns:my="clr-namespace:Tools.Unisave;assembly=Unisave">

Friend Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.


    ''' <summary>Retreives a XML element that represent default application configuration stored in app.config</summary>
    Public Shared ReadOnly Property DefaultSettings As XElement
        Get
            Dim appconfig = XDocument.Load(System.Configuration.ConfigurationManager.OpenExeConfiguration(Configuration.ConfigurationUserLevel.None).FilePath)
            Return appconfig.<config:configuration>.<my:config>.Single
        End Get
    End Property

    Private Sub Application_Startup(sender As Object, e As System.Windows.StartupEventArgs) Handles Me.Startup
        If e.Args.Count > 0 Then
            Select Case e.Args(0)
                Case "/p" 'TODO
                Case "/c" 'Configuration
                    Dim win As New PropertiesWindow
                    win.Show()
                    Exit Sub
            End Select
        End If
        'TODO: Run screen saver
    End Sub
End Class
