Imports System.Runtime.InteropServices, System.Threading.Thread
Imports System.ComponentModel
Imports System.Threading


#If Config <= Nightly Then 'Stage:Nightly
Namespace DevicesT
    ''' <summary>Contains methods for working with keyboard</summary>
    Public Class Keyboard
        ''' <summary>Gets carret (text cursor) blink time in milliseconds</summary>
        ''' <returns>Carret blink time in milliseconds</returns>
        ''' <exception cref="API.Win32APIException">API call failed</exception>
        ''' <version version="1.5.2">Property introduced</version>
        Public Shared ReadOnly Property CarretBlinkTime() As Integer
            Get
                Dim ret = API.GetCaretBlinkTime()
                If ret = 0 Then Throw New API.Win32APIException
                Return ret
            End Get
        End Property


        ''' <summary>There is no CTor</summary>
        Partial Private Sub New()
        End Sub
        ''' <summary>Gets state of particular key.</summary>
        ''' <param name="Key">Key to get status of. Do not use shift keys (<see cref="Keys.Shift"/>, <see cref="Keys.Alt"/>, <see cref="Keys.Control"/>). <see cref="Keys.ShiftKey"/>, <see cref="Keys.Menu"/>, <see cref="Keys.ControlKey"/>, <see cref="Keys.RShiftKey"/>, <see cref="Keys.LShiftKey"/>, <see cref="Keys.LMenu"/>, <see cref="Keys.RMenu"/>, <see cref="Keys.LControlKey"/> and <see cref="Keys.RControlKey"/> are alloved.</param>
        ''' <returns>True if key is pressed at time when the property is being got.</returns>
        ''' <remarks>This function can be also use with mouse buttons and respects situation when buttons are swapped.
        ''' To get state of togglable keys (<see cref="Keys.CapsLock"/>, <see cref="Keys.Scroll"/>, <see cref="Keys.NumLock"/>; this function returns actual Up/Down state of them rather then toggle on/off state) use <see cref="Microsoft.VisualBasic.Devices.Keyboard.CapsLock"/>, <see cref="Microsoft.VisualBasic.Devices.Keyboard.ScrollLock"/> and <see cref="Microsoft.VisualBasic.Devices.Keyboard.NumLock"/>. The <see cref="Microsoft.VisualBasic.Devices.Keyboard"/> class also allows you to get state of shif keys (<see cref="Keys.Shift"/>, <see cref="Keys.Control"/> and <see cref="Keys.Alt"/>).</remarks>
        Public Shared ReadOnly Property KeyStatus(ByVal Key As Keys) As Boolean
            Get
                If Key = Keys.LButton AndAlso My.Computer.Mouse.ButtonsSwapped Then
                    Key = Keys.RButton
                ElseIf Key = Keys.RButton AndAlso My.Computer.Mouse.ButtonsSwapped Then
                    Key = Keys.LButton
                End If
                Return API.Devices.GetAsyncKeyState(Key) And &H8000US
            End Get
        End Property

        ''' <summary>Registers low-level keyboard hook</summary>
        ''' <returns><see cref="LowLevelKeyboardHook"/> class instance ready to fire low-level keyboard hook events</returns>
        Public Shared Function GetLowLevelHook() As LowLevelKeyboardHook
            Return New LowLevelKeyboardHook(True)
        End Function
        ''' <summary>Register low-level keyboard hook with ecents fired in it's own thread</summary>
        ''' <returns><see cref="LowLevelKeyboardHook"/> class instance ready to fire low-level keyboard hook events in different thread than calling thread</returns>
        Public Shared Function GetAsyncLowLevelHook() As LowLevelKeyboardHook
            Dim Hook As New LowLevelKeyboardHook
            Hook.RegisterAsyncHook()
            Return Hook
        End Function

    End Class
   

End Namespace
#End If
