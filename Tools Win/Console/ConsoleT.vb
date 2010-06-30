Imports System.ComponentModel

#If Config <= Beta Then 'Stage:Beta
''' <summary>Contains methods for working with console</summary>
''' <seelaso cref="Console"/>
Public Class ConsoleT
    ''' <summary>Creates console fro process without console</summary>
    ''' <exception cref="API.Win32APIException">The process already has console</exception>
    ''' <remarks>After this function is called, you can use the <see cref="Console"/> class to interact with console.
    ''' <para>When console is allocated for windows application, closing the console terminates whole application.</para></remarks>
    Public Shared Sub AllocateConsole()
        If Not API.Console.AllocConsole Then Throw New API.Win32APIException
        If ClosingEventHandler IsNot Nothing Then If API.Console.SetConsoleCtrlHandler(dHandlerRoutine, True) = True Then HandlerSet = True
    End Sub
    ''' <summary>Detaches process with console from its console</summary>
    ''' <exception cref="API.Win32APIException">Error occured while detaching console (i.e. the porcess has no console)</exception>
    Public Shared Sub DetachConsole()
        If Not API.Console.FreeConsole Then Throw New API.Win32APIException
        HandlerSet = False
    End Sub

    'Public Shared Sub SubclassConsoleWindow()
    '    Subclass = New ConsoleWindowsSubclass(ConsoleT.GetHandle.Handle)
    'End Sub

    'Private Shared Subclass As ConsoleWindowsSubclass

    'Private Sub OnClosing(ByVal e As FormClosingEventArgs)

    'End Sub


    'Private Class ConsoleWindowsSubclass
    '    Inherits WindowsT.NativeT.SubclassedNativeWindow
    '    Public Sub New(ByVal ConsoleHandle As IntPtr)
    '        MyBase.New(ConsoleHandle)
    '    End Sub
    '    Protected Overrides Function NewWndProc(ByVal hwnd As System.IntPtr, ByVal msg As API.Messages.WindowMessages, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    '        Select Case msg
    '            Case API.Messages.WindowMessages.WM_CLOSE
    '                If Me.CloseReason = Windows.Forms.CloseReason.None Then Me.CloseReason = Windows.Forms.CloseReason.TaskManagerClosing
    '                Return WmClose(hwnd, msg, wParam, lParam)
    '            Case API.Messages.WindowMessages.WM_QUERYENDSESSION, API.Messages.WindowMessages.WM_ENDSESSION
    '                Me.CloseReason = Windows.Forms.CloseReason.WindowsShutDown
    '                Return WmClose(hwnd, msg, wParam, lParam)
    '            Case Else : Return MyBase.NewWndProc(hwnd, msg, wParam, lParam)
    '        End Select
    '    End Function
    '    Private Function WmClose(ByVal hwnd As System.IntPtr, ByVal msg As API.Messages.WindowMessages, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    '        Dim e As New FormClosingEventArgs(Me.CloseReason, False)
    '        OnClosing(e)
    '        Return
    '    End Function


    '    Private _CloseReason As CloseReason = CloseReason.None
    '    Protected Property CloseReason() As CloseReason
    '        Get
    '            Return _CloseReason
    '        End Get
    '        Set(ByVal value As CloseReason)
    '            _CloseReason = value
    '        End Set
    '    End Property
    'End Class        

    ''' <summary>Gets handle of console window of console attached to surrent process</summary>
    ''' <returns><see cref="IWin32Window"/> carring console handle; null when no console is associated with current process</returns>
    ''' <version version="1.5.2">New more reliable inmplementation using GetConsoleWindow API. No exception thrown, can return null.</version>
    Public Shared Function GetHandle() As IWin32Window
        'Dim oldTitle = Console.Title
        'Try
        '    Dim newTitle = New Guid().ToString
        '    Console.Title = newTitle
        '    Threading.Thread.Sleep(40)
        '    Dim hwnd = API.GUI.FindWindow(Nothing, newTitle)
        '    If hwnd = 0 Then Throw New API.Win32APIException
        '    Return New WindowsT.NativeT.Win32Window(hwnd)
        'Finally
        '    Console.Title = oldTitle
        'End Try
        Dim cwnd = API.Console.GetConsoleWindow
        If cwnd = IntPtr.Zero Then Return Nothing
        Return New WindowsT.NativeT.Win32Window(cwnd)
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
    ''' <summary>Contains invocation list of the <see cref="Closing"/> event</summary>
    Private Shared ClosingEventHandler As EventHandler(Of ConsoleClosingEventArgs)
    ''' <summary>Handles callback for console closing event</summary>
    ''' <param name="dwCtrlType">Reason why console is being closed</param>
    ''' <returns>False when event was handled, true when it shoudl be cancelled</returns>
    Private Shared Function HandlerRoutine(ByVal dwCtrlType As API.ControlType) As Boolean
        Dim e As New ConsoleClosingEventArgs(dwCtrlType)
        RaiseEvent Closing(Nothing, e)
        Return e.Cancel
    End Function
    ''' <summary>Delegate to <see cref="HandlerRoutine"/></summary>
    ''' <remarks>Keeping delegate in field prevents it from being garbage collcted. Garbage collecting of delegate which was passed to unmanaged code, can lead to unpredictable behavior.</remarks>
    Private Shared dHandlerRoutine As API.Console.HandlerRoutine = AddressOf HandlerRoutine
    ''' <summary>True when <see cref="API.Console.SetConsoleCtrlHandler"/>(True) was successfully called; false when it haven't been called or it was successfully called with false.</summary>
    Private Shared HandlerSet As Boolean
    ''' <summary>Raised when console is being closed</summary>
    ''' <remarks>This event is raised in another thread than it was attached.
    ''' <para>As this method is static, <paramref name="sender"/> is null</para>.
    ''' <para>This event can be cancelled, but it actually does not prevent user from terminating the application. System dialog to close the application is shown when event is cancelled. The dialog is also show when application does not process this event in some amount of time (usually 5 seconds). On Vista, the application is simply terminated. Ctrl+C and Ctrl+Break can be cancelled this way.</para>
    ''' <para>This event can be attached even befor console is allocated using <see cref="AllocateConsole"/>. Events will be raised after console is allocated. Do not use other means of attaching console to your application than methods of <see cref="ConsoleT"/> class and using default console of console application, or event will not be raised.</para>
    ''' </remarks>
    ''' <exception cref="API.Win32APIException">Unable to create console control handler when attaching first handler and this application has console allocated</exception>
    ''' <version version="1.5.2">Event introduced</version>
    Public Shared Custom Event Closing As EventHandler(Of ConsoleClosingEventArgs)
        AddHandler(ByVal value As EventHandler(Of ConsoleClosingEventArgs))
            If API.Console.GetConsoleWindow <> IntPtr.Zero Then
                If value IsNot Nothing AndAlso ClosingEventHandler Is Nothing AndAlso Not HandlerSet Then
                    If API.Console.SetConsoleCtrlHandler(dHandlerRoutine, True) Then
                        HandlerSet = True
                    Else
                        Throw New API.Win32APIException
                    End If
                End If
            End If
            ClosingEventHandler = [Delegate].Combine(ClosingEventHandler, value)
        End AddHandler
        RemoveHandler(ByVal value As EventHandler(Of ConsoleClosingEventArgs))
            ClosingEventHandler = [Delegate].Remove(ClosingEventHandler, value)
            If ClosingEventHandler Is Nothing AndAlso HandlerSet Then
                If API.Console.SetConsoleCtrlHandler(dHandlerRoutine, False) Then HandlerSet = False
            End If
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal e As ConsoleClosingEventArgs)
            If ClosingEventHandler IsNot Nothing Then ClosingEventHandler.Invoke(sender, e)
        End RaiseEvent
    End Event
    ''' <summary>Argument of <see cref="Closing"/> event</summary>
    ''' <remarks>Cancelling the event does not prevent user from closing the console. System dialog mey be shown, or application is simply terminated (Vista)</remarks>
    Public Class ConsoleClosingEventArgs
        Inherits CancelEventArgs
        ''' <summary>CTor</summary>
        ''' <param name="Reason">REason why console is being closed</param>
        Public Sub New(ByVal Reason As ConsoleClosingReason)
            _Reason = Reason
        End Sub
        ''' <summary>COntains value of the <see cref="Reason"/> property</summary>
        Private ReadOnly _Reason As ConsoleClosingReason
        ''' <summary>Gets reason indicating why console is being closed</summary>
        ''' <returns>Reason why console is being closed</returns>
        Public ReadOnly Property Reason() As ConsoleClosingReason
            Get
                Return _Reason
            End Get
        End Property
    End Class
    ''' <summary>Reasons for <see cref="Closing"/> event</summary>
    Public Enum ConsoleClosingReason
        ''' <summary>CTRL+C was pressed</summary>
        CtrlC = API.Console.ControlType.CTRL_C_EVENT
        ''' <summary>CTRL+Break was pressed</summary>
        CtrlBreak = API.Console.ControlType.CTRL_BREAK_EVENT
        ''' <summary>Console window is closing. Either because user clicked close button of window or closed application from task manager.</summary>
        Close = API.Console.ControlType.CTRL_CLOSE_EVENT
        ''' <summary>System si shutting down. Only services can receive this event. Interactive applications are not present at shut down time.</summary>
        ShutDown = API.Console.ControlType.CTRL_SHUTDOWN_EVENT
        ''' <summary>User is logging of. Only services can receive this event. Interactive applications are not present at log-off time.</summary>
        ''' <remarks>It is not indicated which user is being logging-off.</remarks>
        LogOff = API.Console.ControlType.CTRL_LOGOFF_EVENT
    End Enum

    ''' <summary>Permanently prevents console window from being closed by user (removes/dsiables the close button)</summary>
    ''' <remarks>This change cannot be easily reverted.
    ''' <para>This method uses approch descriped here: <a href="http://support.microsoft.com/?scid=kb%3Ben-us%3B818361&amp;x=10&amp;y=12">http://support.microsoft.com/?scid=kb%3Ben-us%3B818361&amp;x=10&amp;y=12</a>.</para>
    ''' <para>User still can close console window via task manager or via taskbar context menu in Windows 7.</para>
    ''' </remarks>
    ''' <exception cref="API.Win32APIException">An error occured</exception>
    ''' <version version="1.5.3">This function is new in version 1.5.3</version>
    Shared Sub PreventClose()
        'http://support.microsoft.com/?scid=kb%3Ben-us%3B818361&x=10&y=12
        Dim hMenu As Integer = API.GetSystemMenu(GetHandle.Handle, False)
        If Not API.DeleteMenu(hMenu, 6, API.enmSelectMenuMethod.MF_BYPOSITION) Then
            Throw New API.Win32APIException
        End If
    End Sub
End Class


#End If