Imports System.Globalization.CultureInfo
Imports Tools.WindowsT.InteropT
Imports Tools.WindowsT.WPF
Imports System.Windows.Forms
Imports System.Windows.Interop
Imports System.Globalization

Class Application

    Private Shared theApplication
    Public Shared Sub Main()
        theApplication = New Application
        If Environment.GetCommandLineArgs.Count > 1 Then
            Select Case Environment.GetCommandLineArgs(1)
                Case "/c" 'Settings modal to foreground window
                    ShowSettings()
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
            Select Case MsgBox("No settings implemented. Run the screen saver?", MsgBoxStyle.YesNo, "Unicode Screen Saver")
                Case MsgBoxResult.Yes : RunScreenSaver()
                Case Else : End
            End Select
        End If
    End Sub

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

    Private Shared Sub ShowSettings()
        MsgBox("No settings implemented yet")
    End Sub
End Class
