#If Config <= Nightly Then 'Stage: Nightly
Namespace ComponentModelT
    'ASAP:Wiki,Comment,Mark,Forum
    <AttributeUsage(AttributeTargets.Field Or AttributeTargets.Property Or AttributeTargets.Method Or AttributeTargets.Class Or AttributeTargets.Struct Or AttributeTargets.Enum Or AttributeTargets.Delegate Or AttributeTargets.Event Or AttributeTargets.Interface Or AttributeTargets.Struct)> _
    Public Class FieldDisplayNameAttribute : Inherits DisplayNameAttribute
        'ASAP:Comments
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal displayName As String)
            MyBase.New(displayName)
        End Sub
    End Class
End Namespace
#End If