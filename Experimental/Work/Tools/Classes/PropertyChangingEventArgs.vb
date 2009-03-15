''' <summary>Argument události zmìny vlastnosti</summary>
''' <typeparam name="T">Typ hodnoty vlastnosti</typeparam>
Public Class PropertyChangingEventArgs(Of T)
    Inherits System.EventArgs
    ''' <summary>CTor</summary>
    ''' <param name="Value">Nová hodnota</param>
    Public Sub New(ByVal Value As T)
        _Value = Value
    End Sub
    ''' <summary>Obsahuje hodnotu vlastnosti <see cref="Value"/></summary>
    Private ReadOnly _Value As T
    ''' <summary>Nová hodnota</summary>
    Public ReadOnly Property Value() As T
        Get
            Return _Value
        End Get
    End Property

End Class
