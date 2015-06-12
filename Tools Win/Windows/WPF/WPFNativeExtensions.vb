Imports System.Runtime.CompilerServices, System.Windows
Imports Tools.WindowsT.NativeT
Imports System.ComponentModel

Namespace WindowsT.WPF
#If Config <= Nightly Then 'Stage: Nightly
    ''' <summary>Provides extension methods for Windows Presentation Foundation which provide functionality based on native Win 32 API</summary>
    ''' <seealso cref="Win32Window"/>
    ''' <version version="1.5.3" stage="Nightly">This module is new in version 1.5.3</version>
    Public Module NativeExtensions
#Region "HideIcon"
        ''' <summary>Hides icon of window, so it's no longed displayed</summary>
        ''' <remarks>This method should be called from <see cref="Window.OnSourceInitialized"/></remarks>
        ''' <seelaso cref="Win32Window.HideIcon"/>
        <Extension()> Public Sub HideIcon(ByVal window As Window)
            If window Is Nothing Then Throw New ArgumentNullException("window")
            window.Icon = Nothing
            Dim nw As New Win32Window(window)
            'API.Messages.WindowMessage.Send(nw, API.Messages.WindowMessages.WM_SETICON, 0, 0)
            'API.Messages.WindowMessage.Send(nw, API.Messages.WindowMessages.WM_SETICON, API.Messages.wParam.WM_GETICON.ICON_SMALL, 0)
            nw.HideIcon()
        End Sub

        Private _GloballyHideNullIconsOfWindows As Boolean = False
        ''' <summary>Indicates if class handler was registered for <see cref="Window">Window</see>.<see cref="Window.LoadedEvent">LoadedEvent</see></summary>
        Private globallyHideNullIconsOfWindowsRegistered As Boolean = False
        ''' <summary>Gets or sets value indicating if icons of all WPF windows in this application with <see cref="Window.Icon"/> null will be hidden</summary>
        ''' <remarks>This property affacts all <see cref="Window">Windows</see> with <see cref="Window.Icon"/> = null in current application domain. This property should be set during application startup and then should not be changed. When changed it has effect only on newly opened System.Windows.</remarks>
        Public Property GloballyHideNullIconsOfWindows As Boolean
            Get
                Return _GloballyHideNullIconsOfWindows
            End Get
            Set(ByVal value As Boolean)
                If GloballyHideNullIconsOfWindows <> value Then
                    _GloballyHideNullIconsOfWindows = value
                    If GloballyHideNullIconsOfWindows AndAlso Not globallyHideNullIconsOfWindowsRegistered Then
                        EventManager.RegisterClassHandler(GetType(Window), Window.LoadedEvent, New RoutedEventHandler(AddressOf OnWindowLoaded), True)
                        globallyHideNullIconsOfWindowsRegistered = True
                    End If
                End If
            End Set
        End Property
        ''' <summary>When <see cref="GloballyHideNullIconsOfWindows"/> is true executed whenever window is loaded</summary>
        ''' <param name="sender">A <see cref="Window"/>.</param>
        ''' <param name="e">Event arguments</param>
        Private Sub OnWindowLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Not GloballyHideNullIconsOfWindows Then Exit Sub
            If Not TypeOf sender Is Window Then Exit Sub
            Dim win As Window = sender
            If win.Icon Is Nothing Then win.HideIcon()
            System.Windows.Add(win)
            AddHandler win.Closed, AddressOf OnWindowClosed
            AddHandler win.SourceInitialized, AddressOf OnIconChanged
            Dim desc = DependencyPropertyDescriptor.FromProperty(Window.IconProperty, GetType(Window))
            desc.AddValueChanged(win, AddressOf OnIconChanged)
        End Sub
        ''' <summary>Keeps list of windows handlers were addded to</summary>
        Private windows As New List(Of Window)
        ''' <summary>Called whenever window <see cref="OnWindowLoaded"/> added handlers to is closed</summary>
        ''' <param name="sender">A <see cref="Window"/></param>
        ''' <param name="e">Event arguments</param>
        Private Sub OnWindowClosed(ByVal sender As Object, ByVal e As EventArgs)
            If Not TypeOf sender Is Window Then Exit Sub
            Dim win As Window = sender
            RemoveHandler win.Closed, AddressOf OnWindowClosed
            RemoveHandler win.SourceInitialized, AddressOf OnIconChanged
            Dim desc = DependencyPropertyDescriptor.FromProperty(Window.IconProperty, GetType(Window))
            desc.RemoveValueChanged(win, AddressOf OnIconChanged)
            System.Windows.Remove(win)
        End Sub
        ''' <summary>Called whenever icon of window changes</summary>
        ''' <param name="sender">A <see cref="Window"/></param>
        ''' <param name="e">Event arguments</param>
        Private Sub OnIconChanged(ByVal sender As Object, ByVal e As EventArgs)
            If Not TypeOf sender Is Window Then Exit Sub
            Dim win As Window = sender
            If win.Icon Is Nothing Then
                If GloballyHideNullIconsOfWindows Then win.HideIcon()
            Else
                Dim win32 As New Win32Window(win)
                win32.ExtendedStyle = win32.ExtendedStyle And Not API.Public.WindowExtendedStyles.DialogModalFrame
                API.SetWindowPos(win32.Handle, IntPtr.Zero, 0, 0, 0, 0, API.SetWindowPosFlags.SWP_NOMOVE Or API.SetWindowPosFlags.SWP_NOSIZE Or API.SetWindowPosFlags.SWP_NOZORDER Or API.SetWindowPosFlags.SWP_FRAMECHANGED) 'Error ignored
            End If
        End Sub
#End Region
    End Module
#End If
End Namespace