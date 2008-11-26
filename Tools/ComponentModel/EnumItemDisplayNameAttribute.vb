#If Config <= Nightly Then 'Stage: Nightly
Namespace ComponentModelT
    ''' <summary><see cref="DisplayNameAttribute"/> that can be applied on fields</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
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