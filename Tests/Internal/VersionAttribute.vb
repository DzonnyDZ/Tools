Imports Tools.InternalT
Namespace InternalT
#If Config <= Release Then
    <Version(1, 0, GetType(VersionAttributeTest), LastChange:="05/15/2007")> _
    Public Class VersionAttributeTest
        Public Shared Sub Test()
            Dim t As Type = GetType(VersionAttributeTest)
            Dim vType As Type = GetType(VersionAttribute)
            Dim Attrs As Object() = t.GetCustomAttributes(vType, False)
            Dim attr As VersionAttribute = Attrs(0)
            MsgBox(attr.LastChange & vbCrLf & attr.LastChangeDate & vbCrLf & attr.Major & "." & attr.Minor & "." & attr.Revision & "." & attr.Build)
        End Sub

    End Class
#End If
End Namespace