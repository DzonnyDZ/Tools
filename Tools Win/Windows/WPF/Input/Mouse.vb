Imports System.Windows
Imports System.Windows.Interop
Imports System.Windows.Input
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.ComponentModel

Namespace WindowsT.WPF.InputT

    ''' <summary>Provides additional mouse events for WPF such as horizontal scrolling</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class MouseT
        ''' <summary>No CTor - this is static class</summary>
        Partial Private Sub New()
        End Sub

#Region "Horizontal wheel"
#Region "HorizontalMouseWheel"
        ''' <summary>Provides metadata for the <c>HorizontalMouseWheel</c> event</summary>
        ''' <remarks>The event occurs when horizonlat mouse wheel is scrolled (this is bubbling event)</remarks>
        Public Shared ReadOnly HorizontalMouseWheelEvent As RoutedEvent = EventManager.RegisterRoutedEvent("HorizontalMouseWheel", RoutingStrategy.Bubble, GetType(EventHandler(Of MouseWheelEventArgsEx)), GetType(MouseT))
        ''' <summary>Adds an event handler for the <c>HorizontalMouseWheel</c> event</summary>
        ''' <param name="d">Dependency object to add handler for</param>
        ''' <param name="handler">The handler to be invoked</param>
        Public Shared Sub AddHorizontalMouseWheelHandler(ByVal d As DependencyObject, ByVal handler As RoutedEventHandler)
            Dim uie As UIElement = TryCast(d, UIElement)
            If uie IsNot Nothing Then
                RegisterHorizontalMouseWheelEvents(uie)
                uie.AddHandler(MouseT.HorizontalMouseWheelEvent, handler)
            End If
        End Sub
        ''' <summary>Removes an event handler from the <c>HorizontalMouseWheel</c> event</summary>
        ''' <param name="d">Dependency object to remove handler for</param>
        ''' <param name="handler">The handler to be removed</param>
        Public Shared Sub RemoveHorizontalMouseWheelHandler(ByVal d As DependencyObject, ByVal handler As RoutedEventHandler)
            Dim uie As UIElement = TryCast(d, UIElement)
            If uie IsNot Nothing Then
                UnregisterHorizontalMouseWheelEvents(uie)
                uie.RemoveHandler(MouseT.HorizontalMouseWheelEvent, handler)
            End If
        End Sub

        ''' <summary>Provides metadata for the <c>PreviewHorizontalMouseWheel</c> event</summary>
        ''' <remarks>The event occurs when horizonlat mouse wheel is scrolled (this is tunelling event)</remarks>
        Public Shared ReadOnly PreviewHorizontalMouseWheelEvent As RoutedEvent = EventManager.RegisterRoutedEvent("PreviewHorizontalMouseWheel", RoutingStrategy.Tunnel, GetType(EventHandler(Of MouseWheelEventArgsEx)), GetType(MouseT))
        ''' <summary>Adds an event handler for the <c>PreviewHorizontalMouseWheel</c> event</summary>
        ''' <param name="d">Dependency object to add handler for</param>
        ''' <param name="handler">The handler to be invoked</param>
        Public Shared Sub AddPreviewHorizontalMouseWheelHandler(ByVal d As DependencyObject, ByVal handler As RoutedEventHandler)
            Dim uie As UIElement = TryCast(d, UIElement)
            If uie IsNot Nothing Then
                RegisterHorizontalMouseWheelEvents(uie)
                uie.AddHandler(MouseT.PreviewHorizontalMouseWheelEvent, handler)
            End If
        End Sub
        ''' <summary>Removes an event handler from the <c>PreviewHorizontalMouseWheel</c> event</summary>
        ''' <param name="d">Dependency object to remove handler for</param>
        ''' <param name="handler">The handler to be removed</param>
        Public Shared Sub RemovePreviewHorizontalMouseWheelHandler(ByVal d As DependencyObject, ByVal handler As RoutedEventHandler)
            Dim uie As UIElement = TryCast(d, UIElement)
            If uie IsNot Nothing Then
                UnregisterHorizontalMouseWheelEvents(uie)
                uie.RemoveHandler(MouseT.PreviewHorizontalMouseWheelEvent, handler)
            End If
        End Sub
#End Region

#Region "HorizontalMouseWheelRegistrationCounter"
        ''' <summary>Gets value of the <c>HorizontalMouseWheelRegistrationCounter</c> private attached dependency property</summary>
        ''' <param name="element">Element to get value for</param>
        ''' <returns>Value of the private attached dependency property <c>HorizontalMouseWheelRegistrationCounter</c> for <paramref name="element"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        Private Shared Function GetHorizontalMouseWheelRegistrationCounter(ByVal element As DependencyObject) As Integer
            If element Is Nothing Then Throw New ArgumentNullException("element")
            Return element.GetValue(HorizontalMouseWheelRegistrationCounterProperty)
        End Function

        ''' <summary>Sets value of the <c>HorizontalMouseWheelRegistrationCounter</c> private attached dependency property</summary>
        ''' <param name="element">Element to set value for</param>
        ''' <param name="value">New value of the <c>HorizontalMouseWheelRegistrationCounter</c> private attached dependency property</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        Private Shared Sub SetHorizontalMouseWheelRegistrationCounter(ByVal element As DependencyObject, ByVal value As Integer)
            If element Is Nothing Then Throw New ArgumentNullException("element")
            element.SetValue(HorizontalMouseWheelRegistrationCounterProperty, value)
        End Sub

        ''' <summary>Metadata of the <c>HorizontalMouseWheelRegistrationCounter</c> private attached dependenvy property</summary>
        Private Shared ReadOnly HorizontalMouseWheelRegistrationCounterProperty As DependencyProperty =
            DependencyProperty.RegisterAttached("HorizontalMouseWheelRegistrationCounter",
                GetType(Integer), GetType(MouseT), New FrameworkPropertyMetadata(0))
#End Region

        ''' <summary>Registers receieving of horizontal mouse wheel events for given <see cref="UIElement"/></summary>
        ''' <param name="element">Element to register receieving of horizontal mouse wheel events for</param>
        ''' <remarks>
        ''' Calling this method registers horizonla mouse wheel events for entire <see cref="PresentationSource"/> <paramref name="element"/> belongs to and if <see cref="PresentationSource"/> for <paramref name="element"/> changes events are registered for new <see cref="PresentationSource"/>.
        ''' <para>Calling rhis method multiple times on same <paramref name="element"/> does nothing. Due to internal registration counter it's necessary to call <see cref="UnregisterHorizontalMouseWheelEvents"/> as many times as <see cref="RegisterHorizontalMouseWheelEvents"/> was called to unregistere the events.</para>
        ''' <para>If element "jumps" between different presentation sources, current implementation supports horizontal mosue wheel events only inside <see cref="HwndSource"/>. Horizontal mouse wheel events in other types of presentation sources are not provided (by this implementation).</para>
        ''' </remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        ''' <exception cref="ArgumentException">Cannot retrieve <see cref="PresentationSource"/> for <paramref name="element"/>.</exception>
        ''' <exception cref="NotSupportedException"><see cref="PresentationSource"/> for <paramref name="element"/> is not <see cref="HwndSource"/>.</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Shared Sub RegisterHorizontalMouseWheelEvents(element As UIElement)
            If element Is Nothing Then Throw New ArgumentNullException("element")
            If GetHorizontalMouseWheelRegistrationCounter(element) > 0 Then
                SetHorizontalMouseWheelRegistrationCounter(element, GetHorizontalMouseWheelRegistrationCounter(element) + 1)
            Else
                Dim src = PresentationSource.FromDependencyObject(element)
                If src Is Nothing Then Throw New ArgumentException("Given dependecy object does not belong to any presentation source.")
                RegisterHorizontalMouseWheelEvents(src)
                PresentationSource.AddSourceChangedHandler(element, AddressOf OnSourceChanged)
                SetHorizontalMouseWheelRegistrationCounter(element, 1)
            End If
        End Sub

        ''' <summary>Unregisters receieving mouse wheel events for given <see cref="UIElement"/>.</summary>
        ''' <param name="element">Element to unregister receieving of horizontal mouse wheel events for</param>
        ''' <remarks>
        ''' In case <see cref="RegisterHorizontalMouseWheelEvents"/> was called multiple times for this <paramref name="element"/> <see cref="UnregisterHorizontalMouseWheelEvents"/> must be called at least as mayn time as <see cref="RegisterHorizontalMouseWheelEvents"/> was called to really unregister the events.
        ''' <para>If horizontal mouse wheel events were registrered for another control in the same <see cref="PresentationSource"/> they must be unregisterered for that control as well.</para>
        ''' <para>This method is provided as compagnon to <see cref="RegisterHorizontalMouseWheelEvents"/> but calle shall not rely on not receieving any horizontal mouse wheel events after calling this method.</para>
        ''' </remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        ''' <exception cref="ArgumentException">Cannot retrieve <see cref="PresentationSource"/> for <paramref name="element"/>.</exception>
        ''' <exception cref="NotSupportedException"><see cref="PresentationSource"/> for <paramref name="element"/> is not <see cref="HwndSource"/>.</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Shared Sub UnregisterHorizontalMouseWheelEvents(element As UIElement)
            If element Is Nothing Then Throw New ArgumentNullException("element")
            If GetHorizontalMouseWheelRegistrationCounter(element) = 1 Then
                Dim src = PresentationSource.FromDependencyObject(element)
                If src Is Nothing Then Throw New ArgumentException("Given dependecy object does not belong to any presentation source.")
                UnregisterHorizontalMouseWheelEvents(src)
                PresentationSource.RemoveSourceChangedHandler(element, AddressOf OnSourceChanged)
                SetHorizontalMouseWheelRegistrationCounter(element, False)
            ElseIf GetHorizontalMouseWheelRegistrationCounter(element) > 1 Then
                SetHorizontalMouseWheelRegistrationCounter(element, GetHorizontalMouseWheelRegistrationCounter(element) - 1)
            End If
        End Sub


        Private Shared ReadOnly sourcesRegisteredForHorizontalMouseWheelEvents As New Dictionary(Of PresentationSource, Integer)

        ''' <summary>Registers horizontal mouse wheel events for given <see cref="PresentationSource"/></summary>
        ''' <param name="src">A <see cref="PresentationSource"/> to register events within</param>
        ''' <remarks>If this method is called multiple times it has no effect. Only due to internal call counter it's necessary to call <see cref="UnregisterHorizontalMouseWheelEvents"/> as mayn time as <see cref="RegisterHorizontalMouseWheelEvents"/> was called for the <paramref name="src"/>.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="src"/> is nulll</exception>
        ''' <exception cref="NotSupportedException"><paramref name="src"/> is not <see cref="HwndSource"/></exception>
        ''' <threadsafety>This method is thread-safe. This method contains critical section shared with <see cref="UnregisterHorizontalMouseWheelEvents"/>.</threadsafety>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Shared Sub RegisterHorizontalMouseWheelEvents(src As PresentationSource)
            If src Is Nothing Then Throw New ArgumentNullException("src")
            If Not TypeOf src Is HwndSource Then Throw New NotSupportedException("Horizontal mouse wheel events registration is allowed only for HWND-based presentations")
            SyncLock sourcesRegisteredForHorizontalMouseWheelEvents
                Dim found = False

                If sourcesRegisteredForHorizontalMouseWheelEvents.ContainsKey(src) Then
                    sourcesRegisteredForHorizontalMouseWheelEvents(src) += 1
                Else
                    DirectCast(src, HwndSource).AddHook(AddressOf HwndSrc_Hook)
                End If
            End SyncLock
        End Sub

        ''' <summary>Unregisters horizontal mouse wheel events for given <see cref="PresentationSource"/>.</summary>
        ''' <param name="src">A <see cref="PresentationSource"/> to unregister the events from</param>
        ''' <exception cref="ArgumentNullException"><paramref name="src"/> is nulll</exception>
        ''' <exception cref="NotSupportedException"><paramref name="src"/> is not <see cref="HwndSource"/></exception>
        ''' <remarks>In case the <see cref="RegisterHorizontalMouseWheelEvents"/> method was called more than once for <paramref name="src"/> <see cref="UnregisterHorizontalMouseWheelEvents"/> must be called as many time s as <see cref="RegisterHorizontalMouseWheelEvents"/> was called. Note that horizobtal mouse wheel registration for <see cref="UIElement"/> also registeres horizontal mouse wheel events for <see cref="PresentationSource"/>.</remarks>
        ''' <threadsafety>This method is thread-safe. This method contains critical section shared with <see cref="RegisterHorizontalMouseWheelEvents"/>.</threadsafety>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Shared Sub UnregisterHorizontalMouseWheelEvents(src As PresentationSource)
            If src Is Nothing Then Throw New ArgumentNullException("src")
            If Not TypeOf src Is HwndSource Then Throw New NotSupportedException("Horizontal mouse wheel events registration is allowed only for HWND-based presentations")
            SyncLock sourcesRegisteredForHorizontalMouseWheelEvents
                If Not sourcesRegisteredForHorizontalMouseWheelEvents.ContainsKey(src) Then
                ElseIf sourcesRegisteredForHorizontalMouseWheelEvents(src) = 1 Then
                    sourcesRegisteredForHorizontalMouseWheelEvents.Remove(src)
                    DirectCast(src, HwndSource).RemoveHook(AddressOf HwndSrc_Hook)
                Else
                    sourcesRegisteredForHorizontalMouseWheelEvents(src) -= 1
                End If
            End SyncLock
        End Sub

        Private Shared Function HwndSrc_Hook(hwnd As IntPtr, msg As Integer, wParam As IntPtr, lParam As IntPtr, ByRef handled As Boolean) As IntPtr
            If Not handled AndAlso msg = API.Messages.WindowMessages.WM_MOUSEHWHEEL Then
                Dim e = New MouseWheelEventArgsEx(Mouse.PrimaryDevice, 0, (wParam.ToInt32 And &HFFFF0000) >> 16, Orientation.Horizontal) With {
                    .RoutedEvent = HorizontalMouseWheelEvent
                }
                InputManager.Current.ProcessInput(e)
                handled = e.Handled
                Return IntPtr.Zero
            End If
        End Function

        ''' <summary>Called when presentation source for an element which has horizontal mouse wheel events registered changes</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        Private Shared Sub OnSourceChanged(ByVal sender As Object, ByVal e As System.Windows.SourceChangedEventArgs)
            If TypeOf e.OldSource Is HwndSource Then UnregisterHorizontalMouseWheelEvents(e.OldSource)
            If TypeOf e.NewSource Is HwndSource Then RegisterHorizontalMouseWheelEvents(e.NewSource)
        End Sub
#End Region

    End Class


End Namespace
