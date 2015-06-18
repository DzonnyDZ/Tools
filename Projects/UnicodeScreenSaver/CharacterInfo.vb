''' <summary>View model of unicode code point</summary>
Friend Class CharacterInfo
    Private ReadOnly _code As UInteger
    Private ReadOnly _name$
    Private ReadOnly _font$
    Private ReadOnly _script$
    Private ReadOnly _block$

    ''' <summary>CTor - creates a new instance of the <see cref="CharacterInfo"/> class</summary>
    ''' <param name="code">Unicode code point</param>
    ''' <param name="name$">Character name</param>
    ''' <param name="font$">Name of font the character is displayed in</param>
    ''' <param name="script$">Name of script the character belongs to</param>
    ''' <param name="block$">Name of Unicode block the charatcer belongs to</param>
    Public Sub New(code As UInteger, name$, font$, script$, block$)
        _code = code
        _name = name
        _font = font
        _script = script
        _block = block
    End Sub

    ''' <summary>Gets the character as string</summary>
    ''' <returns>Usually one character. Non-BMP code pints 2 characters - surrogates</returns>
    Public ReadOnly Property Character As String
        Get
            Return Char.ConvertFromUtf32(Code)
        End Get
    End Property

    ''' <summary>Gets the character code as number</summary>
    Public ReadOnly Property Code As UInteger
        Get
            Return _code
        End Get
    End Property

    ''' <summary>Gets character code formatted as hexadecimal number in Unicode notation</summary>
    Public ReadOnly Property UnicodeCode As String
        Get
            Return String.Format("U+{0:X4}", Code)
        End Get
    End Property

    ''' <summary>Gets name of the character</summary>
    Public ReadOnly Property Name As String
        Get
            Return _name
        End Get
    End Property

    ''' <summary>Gets name of font used to display the character</summary>
    Public ReadOnly Property Font As String
        Get
            Return _font
        End Get
    End Property

    ''' <summary>Gets name of script the character belongs to</summary>
    Public ReadOnly Property Script$
        Get
            Return _script
        End Get
    End Property

    ''' <summary>Gets name of block the character belongs to</summary>
    Public ReadOnly Property Block$
        Get
            Return _block
        End Get
    End Property
End Class
