#If Config <= Nightly Then
Namespace ComponentModelT
    ''' <summary>Indicates if enum may allow values that are not member of it or not</summary>
    ''' <remarks>Tools that use this attribute should treat enums with no <see cref="RestrictAttribute"/> as restricted (<see cref="RestrictAttribute.Restrict"/> is True)</remarks>
    <Author("Ðonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(RestrictAttribute), LastChange:="07/22/2007")> _
    <FirstVersion("06/20/2007")> _
    <AttributeUsage(AttributeTargets.Enum)> _
    Public Class RestrictAttribute : Inherits Attribute
        ''' <summary>Contains value of the <see cref="Restrict"/> property</summary>
        Private _Restrict As Boolean
        ''' <summary>CTor</summary>
        ''' <param name="Restrict">State of restriction</param>
        Public Sub New(ByVal Restrict As Boolean)
            _Restrict = Restrict
        End Sub
        ''' <summary>Inidicates if values should be restricted to enum members</summary>
        Public ReadOnly Property Restrict() As Boolean
            Get
                Return _Restrict
            End Get
        End Property
    End Class
End Namespace
#End If