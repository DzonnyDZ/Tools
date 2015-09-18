Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data

#If True
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converter tha converts <see cref="Boolean"/> to <see cref="System.Windows.Visibility"/></summary>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class BooleanVisibilityConverter
        Implements IValueConverter

        ''' <summary>Converts a value.</summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property.</param>
        ''' <param name="parameter">The converter parameter to use. If string "!" converter negates <paramref name="value"/> first.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <exception cref="NotSupportedException"><paramref name="value"/> is not <see cref="Boolean"/> or <paramref name="targetType"/> is not <see cref="System.Windows.Visibility"/> or one of its base types.</exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If TypeOf value Is Boolean AndAlso targetType.IsAssignableFrom(GetType(System.Windows.Visibility)) Then
                If If(TypeOf parameter Is String AndAlso DirectCast(parameter, String) = "!", Not DirectCast(value, Boolean), DirectCast(value, Boolean)) Then
                    Return System.Windows.Visibility.Visible
                Else
                    Return System.Windows.Visibility.Collapsed
                End If
            End If
            Throw New NotSupportedException(ConverterResources.ex_ConverterCanConvertOnlyFromTo.f(Me.GetType.Name, GetType(Boolean).Name, GetType(System.Windows.Visibility).Name))
        End Function

        ''' <summary>Converts a value.</summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="targetType">The type to convert to.</param>
        ''' <param name="parameter">The converter parameter to use. If string "!" converter negates return value.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <exception cref="ArgumentException"><paramref name="value"/> is  neither <see cref="System.Windows.Visibility.Collapsed"/> nor <see cref="System.Windows.Visibility.Visible"/></exception> 
        ''' <exception cref="NotSupportedException"><paramref name="value"/> is not <see cref="System.Windows.Visibility"/> or <paramref name="targetType"/> is not <see cref="Boolean"/> or one of its base types.</exception>
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            Dim ret As Boolean
            If TypeOf value Is System.Windows.Visibility AndAlso targetType.IsAssignableFrom(GetType(Boolean)) Then
                Select Case DirectCast(value, System.Windows.Visibility)
                    Case System.Windows.Visibility.Collapsed : ret = False
                    Case System.Windows.Visibility.Visible : ret = True
                    Case Else : Throw New ArgumentException(ConverterResources.ex_ConverterCannotConvertValueBack.f(Me.GetType, GetType(System.Windows.Visibility).Name, value))
                End Select
            Else
                Throw New NotSupportedException(ConverterResources.ex_ConverterCanConvertBackOnlyFromTo.f(Me.GetType.Name, GetType(System.Windows.Visibility).Name, GetType(Boolean).Name))
            End If
            Return If(TypeOf parameter Is String AndAlso DirectCast(parameter, String) = "!", Not ret, ret)
        End Function
    End Class
End Namespace
#End If