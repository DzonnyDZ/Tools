Imports Tools.WindowsT.FormsT
Namespace WindowsT.FormsT
    Public Class ScreenSaverBase : Inherits Tools.WindowsT.FormsT.ScreenSaverBase
        Public Shared Sub Test()
            Run(Of ScreenSaverBase)("/s")
        End Sub
        Protected Overrides ReadOnly Property SettingsForm() As System.Windows.Forms.Form
            Get
                Return New Form
            End Get
        End Property
    End Class
End Namespace
