Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data

#If True
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converter used to test if enumerated value is one of given values</summary>
    ''' <remarks>For normal enumerations checks exact value, for flags enumerations tests flass in addition.</remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class EnumInConverter
        Implements IValueConverter

        ''' <summary>Converts a value. </summary>
        ''' <returns>A converted value. True when <paramref name="value"/> is one of values given in <paramref name="parameter"/>; false otherwise. When <paramref name="targetType"/> is <see cref="System.Windows.Visibility"/> returns either <see cref="System.Windows.Visibility.Visible"/> or <see cref="System.Windows.Visibility.Hidden"/>.</returns>
        ''' <param name="value">The value produced by the binding source. Value must be of type <see cref="[Enum]"/> or null.</param>
        ''' <param name="targetType">The type of the binding target property. It must be <see cref="Boolean"/>, <see cref="System.Windows.Visibility"/> or <see cref="Nullable(Of T)"/> of that type.</param>
        ''' <param name="parameter">The converter parameter to use. String. Comma-seperated list of value to test <paramref name="value"/> for.</param>
        ''' <param name="culture">The culture to use in the converter. Ignored.</param>
        ''' <exception cref="ArgumentException"><paramref name="targetType"/> <see cref="Type.IsAssignableFrom">is assignable from</see> neither from <see cref="Boolean"/>, <see cref="System.Windows.Visibility"/> nor from <see cref="Nullable(Of T)"/> of <see cref="Boolean"/> or <see cref="System.Windows.Visibility"/>.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="parameter"/> is null</exception>
        ''' <exception cref="TypeMismatchException"><paramref name="parameter"/> is not <see cref="String"/></exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If Not targetType.IsAssignableFrom(GetType(Boolean)) AndAlso Not targetType.IsAssignableFrom(GetType(System.Windows.Visibility)) AndAlso
                Not targetType.IsAssignableFrom(GetType(Boolean?)) AndAlso Not targetType.IsAssignableFrom(GetType(System.Windows.Visibility?)) Then _
                Throw New ArgumentException(WindowsT.WPF.ConvertersT.ConverterResources.ex_ConverterCanConvertOnlyTo_2.f([GetType].Name, GetType(Boolean), GetType(System.Windows.Visibility)))
            Dim ret As Boolean
            If parameter Is Nothing Then Throw New ArgumentNullException("parameter")
            If Not TypeOf parameter Is String Then Throw New TypeMismatchException("parameter", parameter, GetType(String))
            Dim Parts = DirectCast(parameter, String).Split(",", StringSplitOptions.RemoveEmptyEntries)
            If value Is Nothing Then
                ret = False
            ElseIf Not TypeOf value Is [Enum] Then
                Throw New TypeMismatchException("value", value, GetType([Enum]))
            Else
                ret = False
                For Each name In Parts
                    Dim enmVal As [Enum]
                    Try
                        enmVal = [Enum].Parse(value.GetType, name.Trim, True)
                    Catch ex As ArgumentException
                        Continue For
                    End Try
                    If (DirectCast(value, [Enum]).IsFlags AndAlso DirectCast(value, [Enum]).HasFlag(enmVal)) OrElse
                        value.Equals(enmVal) Then
                        ret = True
                        Exit For
                    End If
                Next
            End If
            If targetType.IsAssignableFrom(GetType(Boolean)) OrElse targetType.IsAssignableFrom(GetType(Boolean?)) Then
                Return ret
            Else 'Visibility
                Return If(ret, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed)
            End If
        End Function

        ''' <summary>Throws a <see cref="NotSupportedException"/></summary>
        ''' <returns>Never returns. This function always throws a <see cref="NotSupportedException"/>.</returns>
        ''' <param name="value">Ignored.</param>
        ''' <param name="targetType">Ignored.</param>
        ''' <param name="parameter">Ignored.</param>
        ''' <param name="culture">Ignored.</param>
        ''' <exception cref="NotSupportedException">This function always throws a <see cref="NotSupportedException"/>, because <see cref="EnumInConverter"/> does not support backward conversion.</exception>
        Private Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            Throw New NotSupportedException(WindowsT.WPF.ConvertersT.ConverterResources.ex_CannotConvertBack.f([GetType].Name))
        End Function
    End Class
End Namespace
#End If