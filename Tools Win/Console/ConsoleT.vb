#If Config <= Beta Then 'Stage:Beta
''' <summary>Contains methods for working with console</summary>
''' <seelaso cref="Console"/>
Public Class ConsoleT
    ''' <summary>Creates console fro process without console</summary>
    ''' <exception cref="API.Win32APIException">The process already has console</exception>
    ''' <remarks>After this function is called, you can use the <see cref="Console"/> class to interact with console.</remarks>
    Public Shared Sub AllocateConsole()
        If Not API.Console.AllocConsole Then Throw New API.Win32APIException
    End Sub
    ''' <summary>Detaches process with console from its console</summary>
    ''' <exception cref="API.Win32APIException">Error occured while detaching console (i.e. the porcess has no console)</exception>
    Public Shared Sub DetachConsole()
        If Not API.Console.FreeConsole Then Throw New API.Win32APIException
    End Sub
    ''' <summary>Gets handle of console window of console attached to surrent process</summary>
    ''' <returns><see cref="IWin32Window"/> carring console handle</returns>
    ''' <remarks>See <a href="http://support.microsoft.com/kb/124103">kb 124103</a>.</remarks>
    ''' <exception cref="API.Win32APIException">Console handle cannot be obtained.</exception>
    Public Shared Function GetHandle() As IWin32Window
        Dim oldTitle = Console.Title
        Try
            Dim newTitle = New Guid().ToString
            Console.Title = newTitle
            Threading.Thread.Sleep(40)
            Dim hwnd = API.GUI.FindWindow(Nothing, newTitle)
            If hwnd = 0 Then Throw New API.Win32APIException
            Return New WindowsT.NativeT.Win32Window(hwnd)
        Finally
            Console.Title = oldTitle
        End Try
    End Function
    ''' <summary>Sets console icon</summary>
    ''' <value>Icon to be set</value>
    ''' <exception cref="API.Win32APIException">An error ocured while setting the icon</exception>
    Public Shared WriteOnly Property Icon() As Icon
        Set(ByVal value As Icon)
            If value Is Nothing Then Throw New ArgumentNullException("value")
            If Not API.Console.SetConsoleIcon(value.Handle) Then Throw New API.Win32APIException
        End Set
    End Property
End Class
#End If