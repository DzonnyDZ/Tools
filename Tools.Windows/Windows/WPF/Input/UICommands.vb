Imports System.Windows.Input
Imports System.Windows.Controls


Namespace WindowsT.WPF.InputT
    ''' <summary>Defines additional standard commands that are not defined in core WPF</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class UICommands
        ''' <summary>This is static class (no CTor)</summary>
        Partial Private Sub New()
        End Sub

        Private Shared ReadOnly _scrollDown As New RoutedUICommand(WPF.Resources.ScrollDown, "ScrollDown", GetType(UICommands), New InputGestureCollection From {New MouseWheelGesture(MouseWheelDirection.Minus)})
        ''' <summary>Gets a scroll down command - generic scroll down</summary>
        Public Shared ReadOnly Property ScrollDown As RoutedUICommand
            Get
                Return _scrollDown
            End Get
        End Property

        Private Shared ReadOnly _scrollUp As New RoutedUICommand(WPF.Resources.ScrollUp, "ScrollUp", GetType(UICommands), New InputGestureCollection From {New MouseWheelGesture(MouseWheelDirection.Plus)})
        ''' <summary>Gets a scroll up command - generic scroll up</summary>
        Public Shared ReadOnly Property ScrollUp As RoutedUICommand
            Get
                Return _scrollUp
            End Get
        End Property

        Private Shared ReadOnly _scrollLeft As New RoutedUICommand(WPF.Resources.ScrollLeft, "ScrollLeft", GetType(UICommands), New InputGestureCollection From {
            New MouseWheelGesture(MouseWheelDirection.Plus, , ModifierKeys.Shift),
            New MouseWheelGesture(MouseWheelDirection.Plus) With {.Orientation = Orientation.Horizontal}
        })
        ''' <summary>Gets a scroll left command - generic scroll left</summary>
        Public Shared ReadOnly Property ScrollLeft As RoutedUICommand
            Get
                Return _scrollLeft
            End Get
        End Property

        Private Shared ReadOnly _scrollRight As New RoutedUICommand(WPF.Resources.ScrollRight, "ScrollRight", GetType(UICommands), New InputGestureCollection From {
            New MouseWheelGesture(MouseWheelDirection.Minus, , ModifierKeys.Shift),
            New MouseWheelGesture(MouseWheelDirection.Minus) With {.Orientation = Orientation.Horizontal}
        })
        ''' <summary>Gets a scroll right command - generic scroll right</summary>
        Public Shared ReadOnly Property ScrollRight As RoutedUICommand
            Get
                Return _scrollRight
            End Get
        End Property

        Private Shared ReadOnly _zoomIn As New RoutedUICommand(WPF.Resources.ZoomIn, "ZoomIn", GetType(UICommands), New InputGestureCollection From {New MouseWheelGesture(MouseWheelDirection.Plus, , ModifierKeys.Control)})
        ''' <summary>Gets a zoom in command (magnification)</summary>
        Public Shared ReadOnly Property ZoomIn As RoutedUICommand
            Get
                Return _zoomIn
            End Get
        End Property

        Private Shared ReadOnly _zoomOut As New RoutedUICommand(WPF.Resources.ZoomOut, "ZoomOut", GetType(UICommands), New InputGestureCollection From {New MouseWheelGesture(MouseWheelDirection.Minus, , ModifierKeys.Control)})
        ''' <summary>Gets a zoom out command (unmagnification)</summary>
        Public Shared ReadOnly Property ZoomOut As RoutedUICommand
            Get
                Return _zoomOut
            End Get
        End Property
    End Class

End Namespace