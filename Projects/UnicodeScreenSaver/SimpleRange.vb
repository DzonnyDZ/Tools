Public Class SimpleRange

    Public Sub New(first As UInteger, last As UInteger)
        If last < first Then Throw New ArgumentException("last value must be greater than or equal to first")
        Me.First = first
        Me.Last = last
    End Sub
    Public ReadOnly Property First As UInteger
    Public ReadOnly Property Last As UInteger

End Class
