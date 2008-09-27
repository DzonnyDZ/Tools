Imports System.Runtime.InteropServices

#If Config <= Nightly Then 'Stage:Nightly
Namespace DevicesT
    ''' <summary>Contains methods fro working with keyboard</summary>
    Public Class Keyboard
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
    End Class
    
End Namespace
#End If
