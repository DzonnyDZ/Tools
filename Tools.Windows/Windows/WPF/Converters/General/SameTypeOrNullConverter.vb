Imports Tools.TypeTools, Tools.ReflectionT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data


Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converter thet returns value being converted if tagret type of conversion <see cref="Type.IsAssignableFrom">is assignable from</see> it, null otherwise.</summary>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class SameTypeOrNullConverter
        Implements IValueConverter

        ''' <summary>Converts a value.</summary>
        ''' <returns>If <paramref name="targetType"/> <see cref="Type.IsAssignableFrom">is assignable from</see> <paramref name="value"/> returns <paramref name="value"/>; null otherwise</returns>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property.</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            If targetType.IsAssignableFrom(value.GetType) Then Return value
            Return Nothing
        End Function

        ''' <summary>Converts a value.</summary>
        ''' <returns>If <paramref name="targetType"/> <see cref="Type.IsAssignableFrom">is assignable from</see> <paramref name="value"/> returns <paramref name="value"/>; null otherwise</returns>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property.</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            If value Is Nothing Then Return Nothing
            If targetType.IsAssignableFrom(value.GetType) Then Return value
            Return Nothing
        End Function
    End Class
End Namespace
