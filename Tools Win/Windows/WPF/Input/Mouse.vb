Imports System.Windows
Imports System.Windows.Interop
Imports System.Windows.Input
Imports System.Windows.Controls

Namespace WindowsT.WPF.InputT

    ''' <summary>Provides additional mouse events such as horizontal scrolling</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class MouseT
        ''' <summary>No CTor - this is static class</summary>
        Partial Private Sub New()
        End Sub

#Region "HorizontalMouseWheel"
        ''' <summary>Provides metadata for the <c>HorizontalMouseWheel</c> event</summary>
        ''' <remarks>The event occurs when horizonlat mouse wheel is scrolled (this is bubbling event)</remarks>
        Public Shared ReadOnly HorizontalMouseWheelEvent As RoutedEvent = EventManager.RegisterRoutedEvent("HorizontalMouseWheel", RoutingStrategy.Bubble, GetType(EventHandler(Of MouseWheelEventArgsEx)), GetType(MouseT))
        ''' <summary>Adds an event handler for the <c>HorizontalMouseWheel</c> event</summary>
        ''' <param name="d">Dependency object to add handler for</param>
        ''' <param name="handler">The handler to be invoked</param>
        Public Shared Sub AddHorizontalMouseWheelHandler(ByVal d As DependencyObject, ByVal handler As RoutedEventHandler)
            Dim uie As UIElement = TryCast(d, UIElement)
            Dim src = PresentationSource.FromDependencyObject(d)
            If uie IsNot Nothing Then
                RegisterHorizontalMouseWheelMessages(uie, src)
                uie.AddHandler(MouseT.HorizontalMouseWheelEvent, handler)
            End If
        End Sub
        ''' <summary>Removes an event handler from the <c>HorizontalMouseWheel</c> event</summary>
        ''' <param name="d">Dependency object to remove handler for</param>
        ''' <param name="handler">The handler to be removed</param>
        Public Shared Sub RemoveHorizontalMouseWheelHandler(ByVal d As DependencyObject, ByVal handler As RoutedEventHandler)
            Dim uie As UIElement = TryCast(d, UIElement)
            If uie IsNot Nothing Then
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
                uie.AddHandler(MouseT.PreviewHorizontalMouseWheelEvent, handler)
            End If
        End Sub
        ''' <summary>Removes an event handler from the <c>PreviewHorizontalMouseWheel</c> event</summary>
        ''' <param name="d">Dependency object to remove handler for</param>
        ''' <param name="handler">The handler to be removed</param>
        Public Shared Sub RemovePreviewHorizontalMouseWheelHandler(ByVal d As DependencyObject, ByVal handler As RoutedEventHandler)
            Dim uie As UIElement = TryCast(d, UIElement)
            Dim src = PresentationSource.FromDependencyObject(d)
            If uie IsNot Nothing Then
                RegisterHorizontalMouseWheelMessages(uie, src)
                uie.RemoveHandler(MouseT.PreviewHorizontalMouseWheelEvent, handler)
            End If
        End Sub

        ''' <summary>Performs all the necessary actions to register events horizontal mouse wheel events</summary>
        ''' <param name="element">Element to register events in context of</param>
        ''' <param name="src">Current presentation source for <paramref name="element"/></param>
        Private Shared Sub RegisterHorizontalMouseWheelMessages(element As UIElement, src As PresentationSource)
            If TypeOf src Is HwndSource Then
                Dim hwndSrc As HwndSource = src
                hwndSrc.AddHook(
                    Function(hwnd As IntPtr, msg As Integer, wParam As IntPtr, lParam As IntPtr, ByRef handled As Boolean) As IntPtr
                        If Not handled AndAlso msg = API.Messages.WindowMessages.WM_MOUSEHWHEEL Then
                            Dim uiElement = src.RootVisual.FindVisualChild(Of UIElement)()
                            If uiElement IsNot Nothing Then
                                Dim e = New MouseWheelEventArgsEx(Mouse.PrimaryDevice, 0, (wParam.ToInt32 And &HFFFF0000) >> 16, Orientation.Horizontal) With {.Source = element}
                                e.RoutedEvent = PreviewHorizontalMouseWheelEvent
                                uiElement.RaiseEvent(e)
                                If Not e.Handled Then
                                    e.RoutedEvent = HorizontalMouseWheelEvent
                                    uiElement.RaiseEvent(e)
                                End If
                            End If
                            handled = True
                            Return IntPtr.Zero
                        End If
                    End Function
                )
            End If
            PresentationSource.AddSourceChangedHandler(element, AddressOf OnSourceChanged)
        End Sub

        ''' <summary>Called when presentation source for an element which has horizontal mouse wheel events registered changes</summary>
        ''' <param name="sender">Source of the event</param>
        ''' <param name="e">Event arguments</param>
        Private Shared Sub OnSourceChanged(ByVal sender As Object, ByVal e As System.Windows.SourceChangedEventArgs)
            RegisterHorizontalMouseWheelMessages(e.Element, e.NewSource)
        End Sub
#End Region

    End Class

    
End Namespace
