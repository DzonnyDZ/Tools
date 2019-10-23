Imports System.Windows.Data, Tools.ExtensionsT, Tools.ReflectionT, System.Linq

Namespace WindowsT.WPF.ConvertersT

    ''' <summary>Implemens <see cref="IValueConverter"/> returning count of items in collection</summary>
    Public Class CountConverter
        Implements IValueConverter

        ''' <summary>Converts a value - gets count of items in a collection</summary>
        ''' <returns>Count of items in collection represented by <paramref name="value"/>. Default return type is <see cref="Integer"/>. Returns null when <paramref name="value"/> is null.</returns>
        ''' <param name="value">The value produced by the binding source. A collection.</param>
        ''' <param name="targetType">The type of the binding target property.</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <remarks>
        ''' Supported collection types are: <see cref="ICollection"/>, <see cref="Array"/> and <see cref="IEnumerable"/>.
        ''' When value of unsupported type is passed function looks for property named <c>Count</c> via <see cref="TypeDescriptor"/>. If the property is found value of that property is returned, otherwise 0 is returned.
        ''' </remarks>
        ''' <exception cref="InvalidCastException">Count (value of type <see cref="Integer"/>, or other type when <paramref name="value"/> is not one of supported types but has property named <c>Count</c>) cannot be converted to <paramref name="targetType"/> (when <paramref name="targetType"/> is null <see cref="Integer"/> is supplied).</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">Cannot convert return value to <paramref name="targetType"/> because no suitable operator is most specific.</exception>
        ''' <exception cref="OverflowException">There was an error converting return value to <paramref name="targetType"/> due to that value to be returned does not fit to given <paramref name="targetType"/> (and causes overflow).</exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            Dim ret%
            If TypeOf value Is ICollection Then
                ret = DirectCast(value, ICollection).Count
            ElseIf TypeOf value Is Array Then
                ret = DirectCast(value, Array).Length
                'TODO: ElseIf TypeOf value is ICollection(Of )
            ElseIf TypeOf value Is IEnumerable Then
                ret = DirectCast(value, IEnumerable).OfType(Of Object).Count
            Else
                For Each prop As PropertyDescriptor In TypeDescriptor.GetProperties(value)
                    If prop.Name = "Count" Then
                        Return DynamicCast(prop.GetValue(value), If(targetType, GetType(Integer)))
                    End If
                Next
            End If
            If targetType Is Nothing Then Return ret
            Return DynamicCast(ret, targetType)
        End Function

        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <returns>This function never returns and always throws <see cref="NotSupportedException"/></returns>
        ''' <param name="value">ignored</param>
        ''' <param name="targetType">ignored</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <exception cref="NotSupportedException">Always. This converter supports only one-way conversion.</exception>
        Private Function IValueConverter_ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            Throw New NotSupportedException(WindowsT.WPF.ConvertersT.ConverterResources.ex_CannotConvertBack)
        End Function
    End Class
End Namespace