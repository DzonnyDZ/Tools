Imports System.Globalization.CultureInfo
Imports Tools.WindowsT.NativeT, Tools.WindowsT.InteropT
Imports Tools.WindowsT.WPF
Imports System.Windows.Forms
Imports System.Windows.Interop
Imports System.Globalization

''' <summary>Entry point class of the application</summary>
Friend Class Application

    ''' <summary>Instance of currently running application</summary>
    Private Shared theApplication

    ''' <summary>Entry point</summary>
    Public Shared Sub Main()
        theApplication = New Application
        If Environment.GetCommandLineArgs.Count > 1 Then
            Select Case Environment.GetCommandLineArgs(1)
                Case "/c" 'Settings modal to foreground window
                    ShowSettings(True)
                Case "/p" '<HWND> show preview as child of given HWND
                    If Environment.GetCommandLineArgs.Count > 2 Then _
                        RunInPreviewMode(Int64.Parse(Environment.GetCommandLineArgs(2), Globalization.NumberStyles.Any, InvariantCulture)) _
                    Else Environment.Exit(1)
                Case "/s" 'Run the screen saver
                    RunScreenSaver()
                Case Else
                    For i = 1 To Environment.GetCommandLineArgs.Length - 1
                        Dim sn%
                        Dim sns As New List(Of Integer)
                        If Environment.GetCommandLineArgs()(i).StartsWith("/") AndAlso Integer.TryParse(Environment.GetCommandLineArgs()(i).Substring(1), NumberStyles.Any, InvariantCulture, sn) Then
                            sns.Add(sn)
                        End If
                        RunScreenSaver(sns.Distinct.ToArray)
                    Next
            End Select
        Else
            ShowSettings(False)
        End If
    End Sub

    ''' <summary>Runs the screensaver in full-screen mode</summary>
    ''' <param name="screenIndices%">When not null screensaver is run only on screens identified by indexes</param>
    Private Shared Sub RunScreenSaver(Optional screenIndices%() = Nothing)
        Dim i = 0
        For Each sc In Screen.AllScreens
            If screenIndices Is Nothing OrElse screenIndices.Contains(i) Then
                Dim window As New UnicodeScreenSaverWindow
                window.Left = sc.Bounds.Left
                window.Top = sc.Bounds.Top
                window.Height = sc.Bounds.Height
                window.Width = sc.Bounds.Width
                window.ShowActivated = True
                window.Topmost = True
                window.Show()
            End If
            i += 1
        Next
        theApplication.run
    End Sub

    ''' <summary>Runs screensaver in preview mode</summary>
    ''' <param name="hwnd">Handle of window to display preview in</param>
    Private Shared Sub RunInPreviewMode(hwnd As IntPtr)

        Dim lpRect = New RECT()
        Dim bGetRect = Native.GetClientRect(hwnd, lpRect)

        Dim sourceParams = New HwndSourceParameters("sourceParams")

        sourceParams.PositionX = 0
        sourceParams.PositionY = 0
        sourceParams.Height = lpRect.Bottom - lpRect.Top
        sourceParams.Width = lpRect.Right - lpRect.Left
        sourceParams.ParentWindow = hwnd
        sourceParams.WindowStyle = WindowStyles.WS_VISIBLE Or WindowStyles.WS_CHILD Or WindowStyles.WS_CLIPCHILDREN

        Dim winWPFContent = New HwndSource(sourceParams)
        AddHandler winWPFContent.Disposed, AddressOf winWPFContent_Disposed
        winWPFContent.RootVisual = New UnicodeScreenSaverWindow

        theApplication.run
    End Sub

    Private Shared Sub winWPFContent_Disposed(sender As Object, e As EventArgs)
        End
    End Sub

    ''' <summary>Displays screensaver settings</summary>
    ''' <param name="modal">True to show settings modal to foreground window, false to show them non modal</param>
    Private Shared Sub ShowSettings(modal As Boolean)
        Dim dialog As New SettingsDialog
        If modal Then
            Dim foregroundWindow = Win32Window.GetForegroundWindow
            dialog.ShowDialog(DirectCast(foregroundWindow, Forms.IWin32Window))
        Else
            dialog.ShowDialog()
        End If
    End Sub
End Class
