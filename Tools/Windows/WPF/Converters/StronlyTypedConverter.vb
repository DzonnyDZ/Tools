Imports System.Windows.Data
Imports System.Globalization

Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Common base for type-safe implementations of <see cref="IValueConverter"/></summary>
    ''' <typeparam name="TSource">Type values are converted from</typeparam>
    ''' <typeparam name="TTarget">Type values are converted to</typeparam>
    <Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(StronglyTypedConverter(Of ,))), FirstVersion(2008, 5, 1)> _
    Public MustInherit Class StronglyTypedConverter(Of TSource, TTarget)
        Implements IValueConverter

        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <exception cref="ArgumentException">Value is not of type <see cref="TSource"/> or <paramref name="targetType"/> cannot be assigned by value of type <see cref="TTarget"/></exception>
        Private Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If Not TypeOf value Is TSource Then Throw New ArgumentException(String.Format("This converter can convert only from {0}.", GetType(TSource).FullName), "value")
            If Not targetType.IsAssignableFrom(GetType(TTarget)) Then Throw New ArgumentException(String.Format("This converter can convert only to {0}.", GetType(TTarget).FullName), "targetType")
            Return Convert(value, parameter, culture)
        End Function
        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        Public MustOverride Function Convert(ByVal value As TSource, ByVal parameter As Object, ByVal culture As CultureInfo) As TTarget
        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="targetType">The type to convert to.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <exception cref="ArgumentException">Value is not of type <see cref="TTarget"/> or <paramref name="targetType"/> cannot be assigned by value of type <see cref="TSource"/></exception>
        Private Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            If Not TypeOf value Is TTarget Then Throw New ArgumentException(String.Format("This converter ca convert back only from {0}.", GetType(TTarget).FullName), "value")
            If Not targetType.IsAssignableFrom(GetType(TSource)) Then Throw New ArgumentException(String.Format("This converter can convert back only to {0}.", GetType(TSource).FullName), "targetType")
            Return ConvertBack(value, parameter, culture)
        End Function
        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        Public MustOverride Function ConvertBack(ByVal value As TTarget, ByVal parameter As Object, ByVal culture As CultureInfo) As TSource
    End Class
End Namespace