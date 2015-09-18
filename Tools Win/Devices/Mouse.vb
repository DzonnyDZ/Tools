Imports System.Runtime.InteropServices, System.Threading.Thread
Imports System.ComponentModel
Imports System.Threading


#If True
Namespace DevicesT
    ''' <summary>Contains methods for working with mouse</summary>
    Public Class Mouse
        ''' <summary>Private CTor to achieve pseudo-static class</summary>
        ''' <exception cref="NotSupportedException">Always</exception>
        Private Sub New()
            Throw New NotSupportedException("This is static class")
        End Sub

        ''' <summary>Value raised by mouse-wheel-related events for one wheel click</summary>
        Public Const WheelStep As Short = 120
        ''' <summary>Registers low-level mouse hook</summary>
        ''' <returns><see cref="LowLevelKeyboardHook"/> class instance ready to fire low-level mouse hook events</returns>
        Public Shared Function GetLowLevelHook() As LowLevelMouseHook
            Return New LowLevelMouseHook(True)
        End Function
        ''' <summary>Register low-level mouse hook with ecents fired in it's own thread</summary>
        ''' <returns><see cref="LowLevelKeyboardHook"/> class instance ready to fire low-level mouse hook events in different thread than calling thread</returns>
        Public Shared Function GetAsyncLowLevelHook() As LowLevelMouseHook
            Dim Hook As New LowLevelMouseHook
            Hook.RegisterAsyncHook()
            Return Hook
        End Function
    End Class
End Namespace
#End If
