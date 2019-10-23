Imports System.Windows.Data, Tools.ExtensionsT, System.Windows
Imports System.Globalization, System.Globalization.CultureInfo
Imports System.Xml
Imports System.Xml.Linq


Namespace WindowsT.WPF.ConvertersT

    ''' <summary>Type converter between <see cref="String"/> and <see cref="Boolean"/> which converts XML (XSD) string representation of bootlan to <see cref="Boolean"/> and vice-versa</summary>
    ''' <version version="1.5.3" stage="Nightly">This class is new in version 1.5.3</version>
    Public Class XmlBooleanConverter
        Implements IValueConverter
        ''' <summary>Converts a value.</summary>
        ''' <param name="value">
        ''' The value produced by the binding source. It's expected to be <see cref="String"/> if it is not, <see cref="ToString"/> is used.
        ''' Special support for <see cref="Visibility"/> is implemented: All values but <see cref="Visibility.Visible"/> are treated as "false".
        ''' Special support is alos implemented to following XML types: <see cref="XmlNode"/>, <see cref="XElement"/>, <see cref="XAttribute"/>, <see cref="XText"/>, <see cref="XCData"/>, <see cref="XProcessingInstruction"/>, <see cref="XComment"/>: Value of these node (<see cref="XProcessingInstruction.Data">Data</see> in case of <see cref="XProcessingInstruction"/>.)
        ''' Note: COnversion for these XML types is usually one way.
        ''' </param>
        ''' <param name="parameter">When ! or "Not" (case-insensitive) converter performs a Not operation. Otherwise ignored</param>
        ''' <param name="culture">ignored. Always uses invariant culture.</param>
        ''' <param name="targetType"> The type of the binding target property. Ignored if null (<see cref="Boolean"/> is returned). Special support for <see cref="Visibility"/>.</param>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.
        ''' Returns true if string representation of <paramref name="value"/> is "true" or "1", false if string representation of <paramref name="value"/> is "false" or "0" (both case-insensitive).
        ''' Returns null if <paramref name="value"/> is nulll or an empty string</returns>
        ''' <exception cref="ArgumentException">String representation of <paramref name="value"/> is neither "true", "false" (case-insensitive), "0", "1", null nor an empty string.</exception>
        ''' <exception cref="InvalidCastException">Return <see cref="Boolean"/> value cannot be converted to <paramref name="targetType"/>.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">Cast operators from <see cref="Boolean"/> to <paramref name="targetType"/> were found, but no one is most specific.</exception>
        ''' <exception cref="OverflowException">Conversion of return <see cref="Boolean"/> value to <paramref name="targetType"/> failed doe to overflow.</exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            Dim val$
            If TypeOf value Is Visibility Then
                val = If(DirectCast(value, Visibility), "true", "false")
            ElseIf TypeOf value Is XmlNode Then
                val = DirectCast(value, XmlNode).Value
            ElseIf TypeOf value Is XElement Then
                val = DirectCast(value, XElement).Value
            ElseIf TypeOf value Is XAttribute Then
                val = DirectCast(value, XAttribute).Value
            ElseIf TypeOf value Is XText Then
                val = DirectCast(value, XText).Value
            ElseIf TypeOf value Is XCData Then
                val = DirectCast(value, XCData).Value
            ElseIf TypeOf value Is XProcessingInstruction Then
                val = DirectCast(value, XProcessingInstruction).Data
            ElseIf TypeOf value Is XComment Then
                val = DirectCast(value, XComment).Value
            Else
                val = value.ToString()
            End If
            If val = "" Then Return Nothing
            Dim ret As Boolean
            Select Case val.ToLowerInvariant
                Case "true", "1" : ret = True
                Case "false", "0" : ret = False
                Case Else : Throw New ArgumentException(ResourcesT.Exceptions.InvalidBooleanValue.f(value))
            End Select
            If (TypeOf parameter Is Char AndAlso CChar(parameter) = "!"c) OrElse (TypeOf parameter Is String AndAlso (CStr(parameter) = "!" OrElse CStr(ParagraphSeparator).ToLowerInvariant = "not")) Then ret = Not ret
            If targetType Is Nothing Then Return ret
            If targetType.Equals(GetType(Visibility)) Then Return If(ret, Visibility.Visible, Visibility.Collapsed)
            Return DynamicCast(ret, targetType)
        End Function

        ''' <summary>Converts a value.</summary>
        ''' <param name="value">
        ''' The value that is produced by the binding target.
        ''' It's ecxpected to be <see cref="Boolean"/>.
        ''' If it is <see cref="Visibility"/> all values but <see cref="Visibility.Visible"/> are treated as false.
        ''' If it is <see cref="IConvertible"/> <see cref="IConvertible.ToBoolean"/> is used with invariant culture.
        ''' Otherwise <see cref="DynamicCast(Of T)"/> to <see cref="Boolean"/> is used
        ''' </param>
        ''' <param name="parameter">When ! or "Not" (case-insensitive) converter performs a Not operation. Otherwise ignored</param>
        ''' <param name="culture">ignored. Always uses invariant culture.</param>
        ''' <param name="targetType">
        ''' The type to convert to. Ignored when null (<see cref="String"/> is returned).
        ''' Special support for <see cref="Visibility"/>, <see cref="XText"/>, <see cref="XCData"/> and <see cref="XComment"/>.
        ''' </param>
        ''' <returns>A converted value. If the method returns null, the valid null value is used. Returns "true" whan <paramref name="value"/> is true, "false" when <paramref name="value"/> is false, null when <paramref name="value"/> is null.</returns>
        ''' <exception cref="InvalidCastException"><paramref name="value"/> is not <see cref="IConvertible"/> and it cannot by converted to <see cref="Boolean"/> -or- Conversion of <see cref="String"/> to <paramref name="targetType"/> failed.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">Cast operators were found, but no one is most specific. When converting <paramref name="value"/> which is not <see cref="IConvertible"/> to <see cref="Boolean"/>, or when converting <see cref="String"/> to <paramref name="targetType"/>.</exception>
        ''' <exception cref="OverflowException">Convertion from <paramref name="value"/> which is not <see cref="IConvertible"/> to <see cref="Boolean"/> failed doe to overflow. -or- Conversion of <see cref="String"/> to <paramref name="targetType"/> failed due to overflow.</exception>
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            If value Is Nothing Then Return Nothing
            Dim val As Boolean
            If TypeOf value Is Visibility Then
                val = DirectCast(value, Visibility) = Visibility.Visible
            ElseIf TypeOf value Is IConvertible Then
                val = DirectCast(value, IConvertible).ToBoolean(InvariantCulture)
            Else
                val = DynamicCast(Of Boolean)(val)
            End If
            Dim ret = If(val, "true", "false")
            If (TypeOf parameter Is Char AndAlso CChar(parameter) = "!"c) OrElse (TypeOf parameter Is String AndAlso (CStr(parameter) = "!" OrElse CStr(ParagraphSeparator).ToLowerInvariant = "not")) Then ret = Not ret
            If targetType Is Nothing Then Return Nothing
            If targetType.Equals(GetType(Visibility)) Then Return If(ret, Visibility.Visible, Visibility.Collapsed)
            If targetType.Equals(GetType(XText)) Then Return New XText(ret)
            If targetType.Equals(GetType(XComment)) Then Return New XComment(ret)
            If targetType.Equals(GetType(XCData)) Then Return New XCData(ret)
            Return DynamicCast(ret, targetType)
        End Function
    End Class
End Namespace
