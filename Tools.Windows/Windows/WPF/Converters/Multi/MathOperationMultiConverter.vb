Imports System.Windows.Data, Tools.ExtensionsT, System.Windows
Imports System.Globalization, Tools.LinqT

#If True
Namespace WindowsT.WPF.ConvertersT

    ''' <summary>Converter performing given arithmetical operation on collection members</summary>
    ''' <remarks>This converter is one-way. This converter can behave both - as multi-value converter and as single value converter.</remarks>
    ''' <version version="1.5.3" stage="Nightly">This class sis new in version 1.5.3</version>
    Public Class MathOperationMultiConverter
        Implements IMultiValueConverter, IValueConverter

        ''' <summary>Converts source values to a value for the binding target. The data binding engine calls this method when it propagates the values from source bindings to the binding target. <para>Performs an arithmetic operation on given values.</para></summary>
        ''' <param name="values">Operands. First value is used as base for the operation (left operand). Other values are used as right operands of operation chain. Null and <see cref="DependencyProperty.UnsetValue"/> values are ignored.</param>
        ''' <param name="targetType">The type of the binding target property. Ignored if null. <see cref="DynamicCast"/> is used.</param>
        ''' <param name="parameter">Indicates arithmetic operation - supported operations are +, -, *, /, ^, \, &amp;, ,  Should be of type <see cref="Char"/> or <see cref="String"/>, otherwise <see cref="System.Object.ToString"/> is used.
        ''' <para>
        ''' Special processing applies when this argument is <see cref="Array"/>: The array must have 2 or 3 items and 2nd item must be <see cref="IValueConverter"/> otherwise it's not treated specially.
        ''' 1st item is used as if it was <paramref name="parameter"/>.
        ''' 2nd item is used as converter for value otherwise returned by this converter.
        ''' 3rd item is used (if present) as parameter for that converter.
        ''' </para></param>
        ''' <param name="culture">The culture to use in the converter. Ignored (but may be passed to output converter)</param>
        ''' <returns>Result of an arithmetic operation. <see cref="Binding.DoNothing" /> if all values in <paramref name="values"/> are ignored (or there are none). Null if <paramref name="values"/> is null.</returns>
        ''' <remarks>Operations supported are:
        ''' <list type="table"><listheader><term>Operation</term><description>Definition</description></listheader>
        ''' <item><term>+</term><description>Plus (addition). Can perform string concatenation under certain circumstances (VB late-binding rules)</description></item>
        ''' <item><term>-</term><description>Minus (subtraction)</description></item>
        ''' <item><term>*</term><description>Multiplication</description></item>
        ''' <item><term>/</term><description>Division (uses decimal places)</description></item>
        ''' <item><term>^</term><description>Power</description></item>
        ''' <item><term>\</term><description>Division (integral)</description></item>
        ''' <item><term>&amp;</term><description>String concatenation</description></item>
        ''' <item><term>%</term><description>Modulo (integral division remainder)</description></item>
        ''' <item><term>,</term><description>Always returns 1st value</description></item>
        ''' </list>
        ''' All operations use Visual Basic late binding</remarks>
        ''' <exception cref="ArgumentException"><paramref name="parameter"/> does not represent supported arithmetic operation</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="parameter"/> is null</exception>
        ''' <exception cref="InvalidCastException">Value cannot be casted for arithmetic operation or to <paramref name="targetType"/>.</exception>
        ''' <exception cref="OverflowException">Can occur during cast or during arithmetic operation</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">When converting return value to <paramref name="targetType"/>: Conversion operands found but no one is most specific.</exception>
        Public Function Convert(ByVal values() As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IMultiValueConverter.Convert
            If parameter Is Nothing Then Throw New ArgumentNullException("parameter")
            Dim outputConverter As IValueConverter = Nothing
            Dim oParameter As Object = Nothing
            If TypeOf parameter Is Array AndAlso (DirectCast(parameter, Array).Length = 2 OrElse DirectCast(parameter, Array).Length = 3) AndAlso TypeOf DirectCast(parameter, Array).GetValue(1) Is IValueConverter Then
                outputConverter = DirectCast(parameter, Array).GetValue(1)
                If DirectCast(parameter, Array).Length > 2 Then oParameter = DirectCast(parameter, Array).GetValue(2)
                parameter = DirectCast(parameter, Array).GetValue(0)
            End If
            If parameter.ToString.Length <> 1 Then Throw New ArgumentException(WindowsT.WPF.ConvertersT.ConverterResources.ex_UnsupportedOperation.f(parameter))
            If values Is Nothing Then Return Nothing
            Dim op As Char = parameter.ToString
            Dim ret As Object = Binding.DoNothing
            Dim i% = 0
            For Each item In values
                If item Is DependencyProperty.UnsetValue OrElse item Is Nothing Then Continue For
                If ret Is Binding.DoNothing Then
                    ret = item
                    If op = ","c Then Exit For
                ElseIf ret Is Nothing OrElse (TypeOf ret Is String AndAlso DirectCast(ret, String) = "" AndAlso Not TypeOf item Is String) Then
                    ret = item
                Else
                    Select Case op  'Note: Late binding is not mistake
                        Case "+"c : ret += item
                        Case "-"c : ret -= item
                        Case "*"c : ret *= item
                        Case "/"c : ret /= item
                        Case "^"c : ret ^= item
                        Case "\"c : ret \= item
                        Case "&"c : ret &= item
                        Case "%"c : ret = ret Mod item
                        Case Else : Throw New ArgumentException(WindowsT.WPF.ConvertersT.ConverterResources.ex_UnsupportedOperation.f(op))
                    End Select
                End If
                i += 1
            Next
            If ret Is Binding.DoNothing Then Return ret
            If outputConverter IsNot Nothing Then Return outputConverter.Convert(ret, targetType, oParameter, culture)
            If targetType Is Nothing Then Return ret
            Return DynamicCast(ret, targetType)
        End Function

        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <param name="value">ignored</param>
        ''' <param name="targetTypes">ignored</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <returns>Never returns, always throws <see cref="NotSupportedException"/></returns>
        ''' <exception cref="NotSupportedException">Always - this convertor is one-way</exception>
        Private Function IMultiValueConverter_ConvertBack(ByVal value As Object, ByVal targetTypes() As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object() Implements System.Windows.Data.IMultiValueConverter.ConvertBack
            Throw New NotSupportedException(WindowsT.WPF.ConvertersT.ConverterResources.ex_CannotConvertBack.f(Me.GetType.Name))
        End Function

        ''' <summary>Converts a single value</summary>
        ''' <param name="value">Value to be converter. SHould be numeric value. Null and <see cref="DependencyProperty.UnsetValue"/> are ignored.</param>
        ''' <param name="targetType">The type of the binding target property. Ignored if null. <see cref="DynamicCast"/> is used.</param>
        ''' <param name="parameter"><list type="bullet">
        ''' <item>Either <see cref="Array"/> containing operator at index 0 and optionally other operands at indexes 1+</item>
        ''' <item>Or <see cref="String"/> where 1st character is operator and rest is either <see cref="Integer"/> or <see cref="Double"/> number. It it can be parsed neither as <see cref="Integer"/> nor as <see cref="Double"/> it's passed to the operation as string.</item>
        ''' </list></param>
        ''' <param name="culture">Culture - used in string-number parsing</param>
        ''' <returns>Converted value</returns>
        ''' <remarks>See overloaded method <see cref="Convert"/> for details.</remarks>
        ''' <exception cref="ArgumentException"><paramref name="parameter"/> does not supply supported arithmetic operation</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="parameter"/> is null</exception>
        ''' <exception cref="InvalidCastException">Value cannot be casted for arithmetic operation or to <paramref name="targetType"/>.</exception>
        ''' <exception cref="OverflowException">Can occur during cast or during arithmetic operation</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">When converting return value to <paramref name="targetType"/>: Conversion operands found but no one is most specific.</exception>
        ''' <version version="1.5.4">Fix: Didn't work with parameter containing decimal point</version>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If parameter Is Nothing Then Throw New ArgumentNullException("parameter")
            If TypeOf parameter Is Array Then
                Dim a2(DirectCast(parameter, Array).Length - 1) As Object
                a2(0) = value
                Array.ConstrainedCopy(parameter, 1, a2, 1, a2.Length - 1)
                Return Convert(a2, targetType, DirectCast(parameter, Array).GetValue(0), culture)
            Else
                If parameter.ToString.Length < 1 Then Throw New ArgumentException(WindowsT.WPF.ConvertersT.ConverterResources.ex_ParamMustHaveMinOneChar, "parameter")
                Dim val As Object
                Dim op = parameter.ToString()(0)
                Dim int%
                Dim dbl As Double
                If Integer.TryParse(parameter.ToString.Substring(1), NumberStyles.Integer, culture, int) Then
                    val = int
                ElseIf Double.TryParse(parameter.ToString.Substring(1), NumberStyles.Any, culture, dbl) Then
                    val = dbl
                Else : val = parameter.ToString.Substring(1)
                End If
                Return Convert({value, val}, targetType, op, culture)
            End If
        End Function

        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <param name="value">ignored</param>
        ''' <param name="targetType">ignored</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <returns>Never returns, always throws <see cref="NotSupportedException"/></returns>
        ''' <exception cref="NotSupportedException">Always - this convertor is one-way</exception>
        Public Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            Throw New NotSupportedException(WindowsT.WPF.ConvertersT.ConverterResources.ex_CannotConvertBack.f(Me.GetType.Name))
        End Function
    End Class

End Namespace
#End If