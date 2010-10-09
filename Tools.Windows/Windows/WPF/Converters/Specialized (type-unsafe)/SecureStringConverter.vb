Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.WPF.ConvertersT
    ''' <summary><see cref="IValueConverter"/> which performs conversion between <see cref="String"/> and <see cref="SecureString"/></summary>
    ''' <remarks>Converting <see cref="SecureString"/> to plain <see cref="String"/> causes string data to be stored plain in memory which can be security risk.</remarks>
    ''' <seelaso cref="String"/><seelaso cref="SecureString"/>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class SecureStringConverter
        Implements IValueConverter

        ''' <summary>Converts a value.</summary>
        ''' <returns>A converted value.
        ''' When <paramref name="value"/> is <see cref="String"/> it's converted to <see cref="SecureString"/> (unless <paramref name="targetType"/> is <see cref="String"/>).
        ''' When <paramref name="value"/> is <see cref="SecureString"/> it's converted to <see cref="String"/> (unless <paramref name="targetType"/> is <see cref="SecureString"/>).
        ''' Returns null whan <paramref name="value"/> is null.
        ''' </returns>
        ''' <param name="value">The value produced by the binding source (value to be converted). It must be either <see cref="String"/> or <see cref="SecureString"/>.</param>
        ''' <param name="targetType">The type of the binding target property. The type must <see cref="Type.IsAssignableFrom">be assignable form</see> either <see cref="String"/> or <see cref="SecureString"/>.</param>
        ''' <param name="parameter">The converter parameter to use. Ignored.</param>
        ''' <param name="culture">The culture to use in the converter. Ignored.</param>
        ''' <exception cref="NotSupportedException"><paramref name="value"/> is neither <see cref="String"/>, <see cref="SecureString"/> or null. -or- <paramref name="targetType"/> <see cref="Type.IsAssignableFrom">is assignable</see> neither from <see cref="String"/> nor from <see cref="SecureString"/>.</exception>
        ''' <remarks>This method implements both - <see cref="IValueConverter.Convert"/> and <see cref="IValueConverter.ConvertBack"/>.</remarks>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert, IValueConverter.ConvertBack
            If value Is Nothing Then Return Nothing
            If TypeOf value Is String Then
                If targetType.IsAssignableFrom(GetType(SecureString)) Then
                    Dim ret As New SecureString
                    For Each ch In DirectCast(value, String)
                        ret.AppendChar(ch)
                    Next
                    Return ret
                ElseIf targetType.IsAssignableFrom(GetType(String)) Then
                    Return value
                Else
                    Throw New NotSupportedException(WindowsT.WPF.ConvertersT.ConverterResources.ex_ConverterCanConvertOnlyTo_2.f([GetType].Name, GetType(String).Name, GetType(SecureString).Name))
                End If
            ElseIf TypeOf value Is SecureString Then
                If targetType.IsAssignableFrom(GetType(String)) Then
                    Return DirectCast(value, SecureString).ToString
                ElseIf targetType.IsAssignableFrom(GetType(SecureString)) Then
                    Return value
                Else
                    Throw New NotSupportedException(WindowsT.WPF.ConvertersT.ConverterResources.ex_ConverterCanConvertOnlyTo_2.f([GetType].Name, GetType(String).Name, GetType(SecureString).Name))
                End If
            Else
                Throw New NotSupportedException(WindowsT.WPF.ConvertersT.ConverterResources.ex_ConverterCanConvertOnlyFrom_2.f([GetType].Name, GetType(String).Name, GetType(SecureString).Name))
            End If
        End Function
    End Class
End Namespace
#End If