#If Config <= RC Then 'Stage:RC
''' <summary>Contains character constants</summary>
Module Chars
    ''' <summary>Carriage return (CR) character (\r, code 13 = 0xD)</summary>
    ''' <seealso cref="vbCrLf"/>
    Public Const Cr As Char = vbCr
    ''' <summary>Line feed (LF) caharcter (\n, code 10 = 0xA)</summary>
    ''' <seealso cref="vbLf"/>
    Public Const Lf As Char = vbLf
    ''' <summary>Null character (code 0)</summary>
    ''' <seeaso cref="vbNullChar"/>
    Public Const NullChar As Char = vbNullChar
    ''' <summary>Horizontal tabulator character (\t, code 9)</summary>
    ''' <seeaso cref="vbTab"/>
    Public Const Tab As Char = vbTab
    ''' <summary>Vertical tabulator character (code 11 = 0xB)</summary>
    ''' <seealso cref="vbVerticalTab"/>
    Public Const VerticalTab As Char = vbVerticalTab
    ''' <summary>Backspace character (code 8)</summary>
    ''' <seealso cref="vbBack"/>
    Public Const Back As Char = vbBack
End Module
#End If