'ASAP:Mark, WiKi,Forum
Public NotInheritable Class Convert
    'ASAP:Comment
    Public Shared Function DynamicCast(Of TSrc, TDest)(ByVal Src As TSrc) As TDest
        Return System.Convert.ChangeType(Src, GetType(TDest))
    End Function
    'ASAP:Comment
    Public Shared Function DynamicCast(Of TDest)(ByVal Src As Object) As TDest
        Return System.Convert.ChangeType(Src, GetType(TDest))
    End Function
End Class
