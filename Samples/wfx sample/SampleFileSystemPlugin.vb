<TotalCommanderPlugin("wfxSample")> _
Public Class SampleFileSystemPlugin
    Inherits FileSystemPlugin


    Public Overrides Function FindFirst(ByVal Path As String, ByRef FindData As FindData) As Object

    End Function

    Public Overrides Function FindNext(ByVal Status As Object, ByRef FindData As FindData) As Boolean

    End Function

    Public Overrides ReadOnly Property Name() As String
        Get

        End Get
    End Property
End Class
