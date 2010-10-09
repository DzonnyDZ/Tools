Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data
#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converter that passes value to <see cref="String.Format"/></summary>
    ''' <remarks>This is one-wy converter</remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class StringFormatConverter
        Implements IValueConverter

        ''' <summary>Formats a value.</summary>
        ''' <returns>A converted value.
        ''' If <paramref name="parameter"/> is null and <paramref name="value"/> is null, an empty string is returned.
        ''' If <paramref name="value"/> is not null but <paramref name="parameter"/> is null, <paramref name="value"/>.<see cref="System.Object.ToString">ToString</see> is returned. (If <paramref name="value"/> is <see cref="IFormattable"/> <see cref="IFormattable.ToString"/> with format argument null is used instead.)
        ''' Othewise result of <see cref="System.String.Format"/> with <paramref name="culture"/>, <paramref name="parameter"/> as format and <paramref name="value"/> as the only argument is returned.
        ''' </returns>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <exception cref="TypeMismatchException"><paramref name="parameter"/> is neither null nor <see cref="String"/>.</exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If parameter Is Nothing AndAlso value Is Nothing Then Return ""
            If parameter Is Nothing Then Return If(TypeOf value Is IFormattable, DirectCast(value, IFormattable).ToString(Nothing, culture), value.ToString)
            If Not TypeOf parameter Is String Then Throw New TypeMismatchException("parameter", parameter, GetType(String))
            Return String.Format(culture, DirectCast(parameter, String), value)
        End Function

        ''' <summary>Converts a value.</summary>
        ''' <returns>Never returns, always throws <see cref="NotSupportedException"/>.</returns>
        ''' <param name="value">ignored</param>
        ''' <param name="targetType">ignored</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <exception cref="NotSupportedException">Always thrown</exception>
        Private Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            Throw New NotSupportedException(ConverterResources.ex_CannotConvertBack.f(Me.GetType.Name))
        End Function
    End Class
End Namespace
#End If