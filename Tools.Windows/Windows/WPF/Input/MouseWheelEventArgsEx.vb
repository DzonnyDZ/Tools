Imports System.Windows.Controls
Imports System.Windows.Input

Namespace WindowsT.WPF.InputT
    ''' <summary>Extends <see cref="MouseWheelEventArgs"/> with orientation</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class MouseWheelEventArgsEx
        Inherits MouseWheelEventArgs
        ''' <summary>Initializes a new instance of the <see cref="MouseWheelEventArgsEx" /> class.</summary>
        ''' <param name="mouse">The mouse device associated with this event.</param>
        ''' <param name="timestamp">The time when the input occurred.</param>
        ''' <param name="delta">The amount the wheel has changed.</param>
        ''' <param name="orientation">Oriantation of scrolling</param>
        Public Sub New(mouse As MouseDevice, timestamp As Integer, delta As Integer, orientation As Orientation)
            MyBase.New(mouse, timestamp, delta)
            _orientation = orientation
        End Sub
        Private ReadOnly _orientation As Orientation
        ''' <summary>gets oriantation of scrolling</summary>
        Public ReadOnly Property Orientation() As Orientation
            Get
                Return _orientation
            End Get
        End Property

        Protected Overrides Sub InvokeEventHandler(genericHandler As System.Delegate, genericTarget As Object)
            MyBase.InvokeEventHandler(genericHandler, genericTarget)
        End Sub
    End Class
End Namespace