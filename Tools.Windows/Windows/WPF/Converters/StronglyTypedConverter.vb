Imports System.Windows.Data
Imports System.Globalization
#If Config <= Nightly Then 'Stage: Nightly
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Common base for type-safe implementations of <see cref="IValueConverter"/></summary>
    ''' <typeparam name="TSource">Type values are converted from</typeparam>
    ''' <typeparam name="TTarget">Type values are converted to</typeparam>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    ''' <version version="1.5.2"><see cref="StronglyTypedConverter.Convert"/> and <see cref="StronglyTypedConverter.ConvertBack"/> now accepts <see langword="null"/> when <typeparamref name="TSource"/> or <typeparamref name="TTarget"/> respectivelly is not value type or is <see cref="Nullable(Of T)"/></version>
    Public MustInherit Class StronglyTypedConverter(Of TSource, TTarget)
        Implements IValueConverter

        ''' <summary>Converts a value.</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <exception cref="ArgumentException">Value is not of type <typeparamref name="TSource"/> (or <see langword="null"/> when <typeparamref name="TSource"/> is not value type or is <see cref="Nullable(Of T)"/>) -or- <paramref name="targetType"/> cannot be assigned by value of type <see cref="TTarget"/></exception>
        ''' <version version="1.5.2"><paramref name="value"/> can be <see langword="null"/> for <typeparamref name="TSource"/> not being value type or being <see cref="Nullable(Of T)"/>.</version>
        ''' <version version="1.5.2"><paramref name="value"/> can be of underlying type of <typeparamref name="TSource"/> when <typeparamref name="TSource"/> is <see cref="Nullable(Of T)"/>.</version>
        Private Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If value Is Nothing AndAlso GetType(TSource).IsNullable Then
                value = Activator.CreateInstance(GetType(TSource))
            ElseIf value IsNot Nothing AndAlso GetType(TSource).IsNullableOf(value.GetType) Then
                'Do nothing
            ElseIf value IsNot Nothing OrElse GetType(TSource).IsValueType Then
                If Not TypeOf value Is TSource AndAlso Not (value Is Nothing AndAlso Not GetType(TSource).IsValueType) Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.ThisConverterCanConvertOnlyFrom0, GetType(TSource).FullName), "value")
                If Not targetType.IsAssignableFrom(GetType(TTarget)) Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.ThisConverterCanConvertOnlyTo0, GetType(TTarget).FullName), "targetType")
            End If
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
        ''' <exception cref="ArgumentException">Value is not of type <see cref="TTarget"/> (or <see langword="null"/> when <typeparamref name="TTarget"/> is not value type or is <see cref="Nullable(Of T)"/>) -or- <paramref name="targetType"/> cannot be assigned by value of type <see cref="TSource"/></exception>
        ''' <version version="1.5.2"><paramref name="value"/> can be <see langword="null"/> for <typeparamref name="TTarget"/> not being vakue type or being <see cref="Nullable(Of T)"/>.</version>
        ''' <version version="1.5.2"><paramref name="value"/> can be of underlying type of <typeparamref name="TTarget"/> when <typeparamref name="TTarget"/> is <see cref="Nullable(Of T)"/>.</version>
        Private Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            If value Is Nothing AndAlso GetType(TTarget).IsNullable Then
                value = Activator.CreateInstance(GetType(TTarget))
            ElseIf value IsNot Nothing AndAlso GetType(TTarget).IsNullableOf(value.GetType) Then
                'Do nothing
            ElseIf value IsNot Nothing OrElse GetType(TTarget).IsValueType Then
                If Not TypeOf value Is TTarget Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.ThisConverterCanConvertBackOnlyFrom0, GetType(TTarget).FullName), "value")
                If Not targetType.IsAssignableFrom(GetType(TSource)) Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.ThisConverterCanConvertBackOnlyTo0, GetType(TSource).FullName), "targetType")
            End If
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
#End If