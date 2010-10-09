Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Comnverter of type <see cref="Windows.Visibility"/> that converts is to oposite value</summary>
    ''' <remarks>Both, <see cref="IValueConverter.Convert"/> and <see cref="IValueConverter.ConvertBack"/> functions are implemented by the same <see cref="NotVisibilityConverter.Convert"/> function</remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class NotVisibilityConverter
        Implements IValueConverter

        ''' <summary>Converts a value</summary>
        ''' <param name="value">A value to be converted. It must be either <see cref="Windows.Visibility"/> or <see cref="Boolean"/></param>
        ''' <param name="targetType">Type to return. It must be assignable from <see cref="Windows.Visibility"/> or <see cref="Boolean"/>.</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <returns>
        ''' If <paramref name="value"/> is <see cref="Boolean"/> it's treated as <see cref="Windows.Visibility.Collapsed"/> (false) or <see cref="Windows.Visibility.Visible"/> (true).
        ''' <paramref name="value"/> is converted to output: <see cref="Windows.Visibility.Visible"/> to <see cref="Windows.Visibility.Collapsed"/>; <see cref="Windows.Visibility.Collapsed"/> to <see cref="Windows.Visibility.Visible"/>; any other value is left unchanged.
        ''' If <paramref name="targetType"/> <see cref="Type.IsAssignableFrom">is assignable from</see> <see cref="Boolean"/> but not from <see cref="Windows.Visibility"/>, <see cref="Boolean"/> (true for <see cref="Windows.Visibility.Visible"/>, false for <see cref="Windows.Visibility.Collapsed"/>); otherwise <see cref="Windows.Visibility"/> is returned.
        ''' </returns>
        ''' <exception cref="TypeMismatchException"><paramref name="value"/> is neither <see cref="Windows.Visibility"/> nor <see cref="Boolean"/></exception>
        ''' <exception cref="ArgumentException"><paramref name="targetType"/> is neither <see cref="Windows.Visibility"/> nor <see cref="Boolean"/> or <paramref name="targetType"/> is <see cref="Boolean"/> and <paramref name="value"/> is neither <see cref="Boolean"/>, <see cref="Windows.Visibility.Visible"/> nor <see cref="Windows.Visibility.Collapsed"/>.</exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert, System.Windows.Data.IValueConverter.ConvertBack
            If TypeOf value Is Boolean Then value = If(DirectCast(value, Boolean), Windows.Visibility.Visible, Windows.Visibility.Collapsed)
            If Not TypeOf value Is Windows.Visibility Then Throw New TypeMismatchException("value", value, GetType(Windows.Visibility))
            Dim ret As Windows.Visibility
            Select Case DirectCast(value, Windows.Visibility)
                Case Windows.Visibility.Collapsed : ret = Windows.Visibility.Visible
                Case Windows.Visibility.Visible : ret = Windows.Visibility.Collapsed
                Case Else
                    If Not targetType.IsAssignableFrom(GetType(Windows.Visibility)) Then Throw New ArgumentException(ConverterResources.ex_IsNotAssignableFrom.f("targetType", GetType(Windows.Visibility).Name))
                    Return value
            End Select
            If targetType.IsAssignableFrom(GetType(Boolean)) AndAlso Not targetType.IsAssignableFrom(GetType(Windows.Visibility)) Then
                Return If(ret = Windows.Visibility.Visible, True, False)
            ElseIf targetType.IsAssignableFrom(GetType(Windows.Visibility)) Then
                Return ret
            Else
                Throw New ArgumentNullException(ConverterResources.ex_IsAssignableFromNeither, "targetType".f(GetType(Windows.Visibility).Name, GetType(Boolean).Name))
            End If
        End Function

    End Class
End Namespace
#End If