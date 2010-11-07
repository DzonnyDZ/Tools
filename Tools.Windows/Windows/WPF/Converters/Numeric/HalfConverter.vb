Imports System.Windows.Data, Tools.ExtensionsT, System.Windows, Tools.TypeTools
Imports System.Globalization
Imports Tools.NumericsT
Imports System.Numerics

#If Config <= Alpha Then 'Stage: Aplha
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Implements <see cref="IValueConverter"/> for converting numeric values to halfs of them</summary>
    ''' <remarks>
    ''' Supported input types are: <see cref="Byte"/>, <see cref="SByte"/>, <see cref="Short"/>, <see cref="UShort"/>, <see cref="Integer"/>, <see cref="UInteger"/>,
    ''' <see cref="Long"/>, <see cref="ULong"/>, <see cref="Decimal"/>, <see cref="Single"/>, <see cref="Double"/>, <see cref="TimeSpan"/>, <see cref="TimeSpanFormattable"/>,
    ''' <see cref="BigInteger"/>, <see cref="Complex"/>, <see cref="SRational"/>, <see cref="URational"/>, <see cref="System.[Enum]"/>.
    ''' <para>Return value is generated from result of arithmetical operation using <see cref="DynamicCast"/>. When requested target type is <see cref="Nullable(Of T)"/> conversion to underlying type is done.</para>
    ''' </remarks>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Alpha"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    ''' <version version="1.5.3">Reworked to support any target type and more source types. Also allows value and target type to be null.</version>
    Public Class HalfConverter
        Implements IValueConverter
        ''' <summary>Converts a value. </summary>
        ''' <returns>A converted value. <paramref name="value"/> / 2. Null when <paramref name="value"/> is null.</returns>
        ''' <param name="value">The value produced by the binding source. Must be one of supported types.</param>
        ''' <param name="targetType">The type of the binding target property. Ignored when null.</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <exception cref="NotSupportedException">Type of <paramref name="value"/> is not supported.</exception>
        ''' <exception cref="InvalidCastException">Error converting result to <paramref name="targetType"/>.</exception>
        ''' <exception cref="OverflowException">Error during arithmetic operation or conversion of return value to <paramref name="targetType"/>.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">No conversion operator for convertiong return value to <paramref name="targetType"/> is most specific.</exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            Dim ret As Object
            If TypeOf value Is [Enum] Then value = DirectCast(value, [Enum]).GetValue()
            If TypeOf value Is Byte Then
                ret = DirectCast(value, Byte) / CByte(2)
            ElseIf TypeOf value Is SByte Then
                ret = DirectCast(value, SByte) / CSByte(2)
            ElseIf TypeOf value Is Short Then
                ret = DirectCast(value, Short) / 2S
            ElseIf TypeOf value Is UShort Then
                ret = DirectCast(value, UShort) / 2US
            ElseIf TypeOf value Is Integer Then
                ret = DirectCast(value, Integer) / 2I
            ElseIf TypeOf value Is UInteger Then
                ret = DirectCast(value, UInteger) / 2UI
            ElseIf TypeOf value Is Long Then
                ret = DirectCast(value, Long) / 2L
            ElseIf TypeOf value Is ULong Then
                ret = DirectCast(value, ULong) / 2UL
            ElseIf TypeOf value Is Decimal Then
                ret = DirectCast(value, Decimal) / 2D
            ElseIf TypeOf value Is Single Then
                ret = DirectCast(value, Single) / 2.0F
            ElseIf TypeOf value Is Double Then
                ret = DirectCast(value, Double) / 2.0R
            ElseIf TypeOf value Is BigInteger Then
                ret = DirectCast(value, BigInteger) / CType(2, BigInteger)
            ElseIf TypeOf value Is TimeSpan Then
                ret = TimeSpan.FromMilliseconds(DirectCast(value, TimeSpan).TotalMilliseconds / 2)
            ElseIf TypeOf value Is TimeSpanFormattable Then
                ret = CType(TimeSpan.FromMilliseconds(DirectCast(value, TimeSpanFormattable).TotalMilliseconds / 2), TimeSpanFormattable)
            ElseIf TypeOf value Is SRational Then
                ret = DirectCast(value, SRational) / 2
            ElseIf TypeOf value Is URational Then
                ret = DirectCast(value, URational) / 2
            ElseIf TypeOf value Is Complex Then
                ret = DirectCast(value, Complex) / 2
            Else
                Throw New NotSupportedException("Type {0} is not supported by {1}".f(value.GetType.Name, [GetType].Name))
            End If

            If targetType Is Nothing Then Return ret
            If targetType.IsNullable Then targetType = targetType.GetGenericArguments(0)
            Return DynamicCast(ret, targetType)
        End Function

        ''' <summary>Converts a value. </summary>
        ''' <returns>A converted value. <paramref name="value"/> * 2. Null when <paramref name="value"/> is null.</returns>
        ''' <param name="value">The value produced by the binding source. Must be one of supported types.</param>
        ''' <param name="targetType">The type of the binding target property. Ignored when null.</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <exception cref="NotSupportedException">Type of <paramref name="value"/> is not supported.</exception>
        ''' <exception cref="InvalidCastException">Error converting result to <paramref name="targetType"/>.</exception>
        ''' <exception cref="OverflowException">Error during arithmetic operation or conversion of return value to <paramref name="targetType"/>.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">No conversion operator for convertiong return value to <paramref name="targetType"/> is most specific.</exception>
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If value Is Nothing Then Return Nothing
            Dim ret As Object
            If TypeOf value Is [Enum] Then value = DirectCast(value, [Enum]).GetValue()
            If TypeOf value Is Byte Then
                ret = DirectCast(value, Byte) * CByte(2)
            ElseIf TypeOf value Is SByte Then
                ret = DirectCast(value, SByte) * CSByte(2)
            ElseIf TypeOf value Is Short Then
                ret = DirectCast(value, Short) * 2S
            ElseIf TypeOf value Is UShort Then
                ret = DirectCast(value, UShort) * 2US
            ElseIf TypeOf value Is Integer Then
                ret = DirectCast(value, Integer) * 2I
            ElseIf TypeOf value Is UInteger Then
                ret = DirectCast(value, UInteger) * 2UI
            ElseIf TypeOf value Is Long Then
                ret = DirectCast(value, Long) * 2L
            ElseIf TypeOf value Is ULong Then
                ret = DirectCast(value, ULong) * 2UL
            ElseIf TypeOf value Is Decimal Then
                ret = DirectCast(value, Decimal) * 2D
            ElseIf TypeOf value Is Single Then
                ret = DirectCast(value, Single) * 2.0F
            ElseIf TypeOf value Is Double Then
                ret = DirectCast(value, Double) * 2.0R
            ElseIf TypeOf value Is BigInteger Then
                ret = DirectCast(value, BigInteger) * CType(2, BigInteger)
            ElseIf TypeOf value Is TimeSpan Then
                ret = TimeSpan.FromMilliseconds(DirectCast(value, TimeSpan).TotalMilliseconds * 2)
            ElseIf TypeOf value Is TimeSpanFormattable Then
                ret = CType(TimeSpan.FromMilliseconds(DirectCast(value, TimeSpanFormattable).TotalMilliseconds * 2), TimeSpanFormattable)
            ElseIf TypeOf value Is SRational Then
                ret = DirectCast(value, SRational) * 2
            ElseIf TypeOf value Is URational Then
                ret = DirectCast(value, URational) * 2
            ElseIf TypeOf value Is Complex Then
                ret = DirectCast(value, Complex) * 2
            Else
                Throw New NotSupportedException("Type {0} is not supported by {1}".f(value.GetType.Name, [GetType].Name))
            End If
            Return DynamicCast(ret, targetType)
        End Function

    End Class
End Namespace
#End If