Imports System.Configuration
Imports System.Xml
Imports <xmlns:my="clr-namespace:Tools.Unisave;assembly=Unisave">

''' <summary>A class that runs the Unisave application</summary>
Friend Class UnisaveApplication
    Inherits Application

    ''' <summary>Application entrypoint</summary>
    Public Shared Sub Main(args As String())
        If args.Length > 0 Then
            Select Case args(0)
                Case "/p" 'TODO: Preview
                    Exit Sub
                Case "/c" 'Configuration
                    Dim win As New PropertiesWindow
                    Dim app As New UnisaveApplication
                    app.Run(win)
                    Exit Sub
            End Select
        End If
        'TODO: Run screen saver
    End Sub
    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.


    ''' <summary>Retreives a XML element that represent default application configuration stored in app.config</summary>
    Public Shared ReadOnly Property DefaultSettings As XElement
        Get
            Dim settings = DirectCast(ConfigurationManager.GetSection("DefaultSettings"), ClientSettingsSection).Settings.Get("DefaultSettings").Value.ValueXml
            Dim el As XElement = XElement.ReadFrom(New XmlNodeReader(settings))
            Return el
        End Get
    End Property
End Class
