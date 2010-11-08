Imports System.Windows.Data, Tools.ExtensionsT, System.Windows
Imports System.Globalization, System.Globalization.CultureInfo

#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.WPF.ConvertersT

    ''' <summary>Type converter between <see cref="String"/> and <see cref="Boolean"/> which converts XML (XSD) string representation of bootlan to <see cref="Boolean"/> and vice-versa</summary>
    ''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
    Public Class XmlBooleanConverter
        Implements IValueConverter
        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored. Always uses invariant culture.</param>
        ''' <param name="targetType"> The type of the binding target property. Ignored if null (<see cref="Boolean"/> is returned).</param>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.
        ''' Returns true if string representation of <paramref name="value"/> is "true" or "1", false if string representation of <paramref name="value"/> is "false" or "0" (both case-insensitive).
        ''' Returns null if <paramref name="value"/> is nulll or an empty string</returns>
        ''' <exception cref="ArgumentException">String representation of <paramref name="value"/> is neither "true", "false" (case-insensitive), "0", "1", null nor an empty string.</exception>
        ''' <exception cref="InvalidCastException">Return <see cref="Boolean"/> value cannot be converted to <paramref name="targetType"/>.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">Cast operators from <see cref="Boolean"/> to <paramref name="targetType"/> were found, but no one is most specific.</exception>
        ''' <exception cref="OverflowException">Conversion of return <see cref="Boolean"/> value to <paramref name="targetType"/> failed doe to overflow.</exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            Dim val = value.ToString
            If val = "" Then Return Nothing
            Dim ret As Boolean
            Select Case val.ToLowerInvariant
                Case "true", "1" : ret = True
                Case "false", "0" : ret = False
                Case Else : Throw New ArgumentException(ResourcesT.Exceptions.InvalidBooleanValue.f(value))
            End Select
            If targetType Is Nothing Then Return ret
            Return DynamicCast(ret, targetType)
        End Function

        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored. Always uses invariant culture.</param>
        ''' <param name="targetType">The type to convert to. Ignored when null (<see cref="String"/> is returned)</param>
        ''' <returns>A converted value. If the method returns null, the valid null value is used. Returns "true" whan <paramref name="value"/> is true, "false" when <paramref name="value"/> is false, null when <paramref name="value"/> is null.</returns>
        ''' <exception cref="InvalidCastException"><paramref name="value"/> is not <see cref="IConvertible"/> and it cannot by converted to <see cref="Boolean"/> -or- Conversion of <see cref="String"/> to <paramref name="targetType"/> failed.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">Cast operators were found, but no one is most specific. When converting <paramref name="value"/> which is not <see cref="IConvertible"/> to <see cref="Boolean"/>, or when converting <see cref="String"/> to <paramref name="targetType"/>.</exception>
        ''' <exception cref="OverflowException">Convertion from <paramref name="value"/> which is not <see cref="IConvertible"/> to <see cref="Boolean"/> failed doe to overflow. -or- Conversion of <see cref="String"/> to <paramref name="targetType"/> failed due to overflow.</exception>
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            If value Is Nothing Then Return Nothing
            Dim val As Boolean
            If TypeOf value Is IConvertible Then
                val = DirectCast(value, IConvertible).ToBoolean(InvariantCulture)
            Else
                val = DynamicCast(Of Boolean)(val)
            End If
            Dim ret = If(val, "true", "false")
            If targetType Is Nothing Then Return Nothing
            Return DynamicCast(ret, targetType)
        End Function
    End Class
End Namespace
#End If