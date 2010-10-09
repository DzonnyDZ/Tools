Imports System.Windows.Data, Tools.ExtensionsT, System.Windows
Imports System.Globalization

#If Config <= Alpha Then 'Stage: Aplha
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Implements <see cref="IValueConverter"/> which negates <see cref="Boolean"/> value</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Alpha"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Class NotBooleanValueConverter
        Inherits StronglyTypedConverter(Of Boolean, Boolean)
        ''' <summary>Converts a value - makes boolean negation of it.</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="parameter">The converter parameter to use. Ignored.</param>
        ''' <param name="culture">The culture to use in the converter. Ignored.</param>
        ''' <returns>Boolean negation of <paramref name="value"/></returns>
        Public Overrides Function Convert(ByVal value As Boolean, ByVal parameter As Object, ByVal culture As CultureInfo) As Boolean
            Return Not value
        End Function
        ''' <summary>Converts a value - makes boolean negation of it.</summary>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="parameter">The converter parameter to use. Ignored.</param>
        ''' <param name="culture">The culture to use in the converter. Ignored.</param>
        ''' <returns>Boolean negation of <paramref name="value"/>.</returns>
        Public Overrides Function ConvertBack(ByVal value As Boolean, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Boolean
            Return Not value
        End Function
    End Class
End Namespace
#End If