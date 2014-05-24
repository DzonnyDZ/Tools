
Namespace InteropT

    ''' <summary>Provides versioning information about unmanaged module</summary>
    Public Class ModuleVersionInformation

        Private ReadOnly _fixedInfo As API.VS_FIXEDFILEINFO
        Private ReadOnly _locData As IDictionary(Of String, IDictionary(Of String, IDictionary(Of String, String)))

        ''' <summary>CTor - creates a new instance of the <see cref="ModuleVersionInformation"/> class</summary>
        ''' <param name="fixedInfo">Fixed file information</param>
        ''' <param name="locData">Localized file information</param>
        Friend Sub New(fixedInfo As API.VS_FIXEDFILEINFO, locData As IDictionary(Of String, IDictionary(Of String, IDictionary(Of String, String))))
            _fixedInfo = fixedInfo
            _locData = locData
        End Sub

        ''' <summary>Gets localized file information</summary>
        Public ReadOnly Property LocalizedData As IDictionary(Of String, IDictionary(Of String, IDictionary(Of String, String)))
            Get
                Return _locData
            End Get
        End Property

    End Class
End Namespace