''' <summary>Poskytuje různé vychytávky</summary>
Public Module Utils
    
    ''' <summary>Prohodí obsah proměnných</summary>
    ''' <typeparam name="T">Typ proměnných</typeparam>
    ''' <param name="a">Proměnná 1</param>
    ''' <param name="b">Proměnná 2</param>
    Public Sub Swap(Of T)(ByRef a As T, ByRef b As T)
        Dim tmp As T
        tmp = a
        a = b
        b = tmp
    End Sub
    '''' <summary>Prohodí obsah proměnných</summary>
    '''' <param name="a">Proměnná 1</param>
    '''' <param name="b">Proměnná 2</param>
    'Public Sub Swap(ByRef a As Object, ByRef b As Object)
    '    Swap(Of Object)(a, b)
    'End Sub

    ''' <summary>The GetCaretBlinkTime function returns the elapsed time, in milliseconds, required to invert the caret's pixels. The user can set this value using the Control Panel.</summary>
    ''' <returns>If the function succeeds, the return value is the blink time, in milliseconds. 
    ''' If the function fails, the return value is zero. To get extended error information, call GetLastError. </returns>
    Public Declare Function GetCaretBlinkTime Lib "user32.dll" () As Int32
End Module
