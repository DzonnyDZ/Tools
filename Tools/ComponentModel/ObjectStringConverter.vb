#If Config <= Release Then
Namespace ComponentModelT
    ''' <summary>Allows editing <see cref="Object"/> as <see cref="String"/> in <see cref="System.Windows.Forms.PropertyGrid"/></summary>
    ''' <author www="http://dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Class ObjectStringConverter : Inherits TypeConverter
        ''' <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="sourceType">A <see cref="System.Type"/> that represents the type you want to convert from.</param>
        ''' <returns>true if this converter can perform the conversion; otherwise, false.</returns>
        Public Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
            If sourceType.Equals(GetType(String)) Then
                Return True
            End If
            Return MyBase.CanConvertFrom(context, sourceType)
        End Function
        ''' <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
        ''' <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
        ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        ''' <param name="value">The <see cref="System.Object"/> to convert.</param>
        ''' <returns>An <see cref="System.Object"/> that represents the converted value.</returns>
        ''' <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
        Public Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
            If TypeOf value Is String Then
                Return value
            End If
            Return MyBase.ConvertFrom(context, culture, value)
        End Function
    End Class
End Namespace
#End If