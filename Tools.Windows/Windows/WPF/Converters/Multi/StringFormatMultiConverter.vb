Imports System.Windows.Data, Tools.ExtensionsT, System.Windows
Imports System.Globalization

#If True
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Multivalue converter that converts passes given values to <see cref="String.Format"/></summary>
    ''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
    Public Class StringFormatMultiConverter
        Implements IMultiValueConverter

        ''' <summary>Converts source values to a value for the binding target. This implementation passes given value to <see cref="String.Format"/> with given formatting string.</summary>
        ''' <returns>A converted value. <see cref="String"/> by default. Resturn value is result of <see cref="String.Format"/>(<paramref name="culture"/>, <paramref name="parameter"/>, <paramref name="values"/>) call.</returns>
        ''' <param name="values">The array of values that the source bindings in the <see cref="T:System.Windows.Data.MultiBinding" /> produces. The value <see cref="F:System.Windows.DependencyProperty.UnsetValue" /> indicates that the source binding has no value to provide for conversion. When null an empty array is used isntead.</param>
        ''' <param name="targetType">The type of the binding target property. Ignored if null. If this type is not <see cref="Type.IsAssignableFrom">assignable from</see> <see cref="String"/> <see cref="DynamicCast"/> is used.</param>
        ''' <param name="parameter">The converter parameter to use. This must represent valid formatting string for <see cref="String.Format"/>. If type of this argument is not <see cref="String"/>, <see cref="System.Object.ToString"/> is used. Null is treated as an empty string.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <exception cref="FormatException"><paramref name="parameter"/> represents an invalid format. -or- The index of a format item is less than zero, or greater than or equal to the length of the args array.</exception>
        Public Function Convert(ByVal values() As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IMultiValueConverter.Convert
            If parameter Is Nothing Then parameter = ""
            If values Is Nothing Then values = New Object() {}
            Dim ret = String.Format(culture, parameter.ToString, values)
            If targetType Is Nothing OrElse targetType.IsAssignableFrom(GetType(String)) Then Return ret
            Return DynamicCast(ret, targetType)
        End Function

        ''' <summary>Always throws <see cref="NotSupportedException"/> - this is one way converter.</summary>
        ''' <returns>Never returns - always throws <see cref="NotSupportedException"/>.</returns>
        ''' <param name="value">Ignored</param>
        ''' <param name="targetTypes">Ignored</param>
        ''' <param name="parameter">Ignored</param>
        ''' <param name="culture">Ignored</param>
        ''' <exception cref="NotSupportedException">Always. This converter is one-way.</exception>
        Private Function IMultiValueConverter_ConvertBack(ByVal value As Object, ByVal targetTypes() As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object() Implements System.Windows.Data.IMultiValueConverter.ConvertBack
            Throw New NotSupportedException(WindowsT.WPF.ConvertersT.ConverterResources.ex_CannotConvertBack.f([GetType].Name))
        End Function
    End Class
End Namespace
#End If