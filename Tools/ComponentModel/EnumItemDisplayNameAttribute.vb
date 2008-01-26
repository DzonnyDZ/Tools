#If Config <= Nightly Then 'Stage: Nightly
Namespace ComponentModelT
    ''' <summary><see cref="DisplayNameAttribute"/> that can be applied on fields</summary>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(FieldDisplayNameAttribute), LastChange:="07/22/2007")> _
    <AttributeUsage(AttributeTargets.Field Or AttributeTargets.Property Or AttributeTargets.Method Or AttributeTargets.Class Or AttributeTargets.Struct Or AttributeTargets.Enum Or AttributeTargets.Delegate Or AttributeTargets.Event Or AttributeTargets.Interface Or AttributeTargets.Struct)> _
    Public Class FieldDisplayNameAttribute : Inherits DisplayNameAttribute
        ''' <summary>CTor</summary>
        Public Sub New()
            MyBase.New()
        End Sub
        ''' <summary>CTor</summary>
        ''' <param name="displayName">The display name.</param>
        Public Sub New(ByVal displayName As String)
            MyBase.New(displayName)
        End Sub
    End Class
End Namespace
#End If