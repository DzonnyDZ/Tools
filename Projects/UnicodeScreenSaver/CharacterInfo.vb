Public Class CharacterInfo
    Private ReadOnly _code As UInteger
    Private ReadOnly _name$
    Private ReadOnly _font$
    Private ReadOnly _script$
    Private ReadOnly _block$
    Public Sub New(code As UInteger, name$, font$, script$, block$)
        _code = code
        _name = name
        _font = font
        _script = script
        _block = block
    End Sub

    Public ReadOnly Property Character As String
        Get
            Return Char.ConvertFromUtf32(Code)
        End Get
    End Property
    Public ReadOnly Property Code As UInteger
        Get
            Return _code
        End Get
    End Property

    Public ReadOnly Property UnicodeCode As String
        Get
            Return String.Format("U+{0:X4}", Code)
        End Get
    End Property

    Public ReadOnly Property Name As String
        Get
            Return _name
        End Get
    End Property

    Public ReadOnly Property Font As String
        Get
            Return _font
        End Get
    End Property

    Public ReadOnly Property Script$
        Get
            Return _script
        End Get
    End Property

    Public ReadOnly Property Block$
        Get
            Return _block
        End Get
    End Property

End Class
