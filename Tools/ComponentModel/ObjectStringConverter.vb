#If Config <= Release Then
Namespace ComponentModel
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, GetType(ObjectStringConverter), lastchange:="1/7/2007")> _
    Public Class ObjectStringConverter : Inherits TypeConverter
        Public Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
            If sourceType.Equals(GetType(String)) Then
                Return True
            End If
            Return MyBase.CanConvertFrom(context, sourceType)
        End Function

        Public Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
            If TypeOf value Is String Then
                Return value
            End If
            Return MyBase.ConvertFrom(context, culture, value)
        End Function
    End Class
End Namespace
#End If