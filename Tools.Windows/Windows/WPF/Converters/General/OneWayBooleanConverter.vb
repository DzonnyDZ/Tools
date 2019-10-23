Imports System.Windows.Data, Tools, Tools.ExtensionsT
Imports System.Globalization, System.Windows

Namespace WindowsT.WPF.ConvertersT

    'TODO: Derive other boolean converters from this one

    ''' <summary>Commmon abstract base class for value one-way converters converting something to boolean</summary>
    ''' <remarks>This class takes care about return value conversions</remarks>
    ''' <version version="1.5.3">This abstract class is new in version 1.5.3</version>
    Public MustInherit Class OneWayBooleanConverterBase
        Implements IValueConverter
        ''' <summary>Converts a value. </summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used. By default returns <see cref="Boolean"/> Different return type can be required by <paramref name="targetType"/>.</returns>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property. Ignored if null.
        ''' Preffered types are <see cref="Boolean"/> and <see cref="Nullable(Of T)"/>[<see cref="Boolean"/>].
        ''' Specially supported types are <see cref="Visibility"/> and <see cref="Nullable(Of T)"/>[<see cref="Visibility"/>].
        ''' If target type is not one ow types mentioned above <see cref="DynamicCast"/> is used.
        ''' If <see cref="DynamicCast"/> throws <see cref="InvalidCastException"/> or <see cref="Reflection.AmbiguousMatchException"/> unconverted return value is returned instead.
        ''' </param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            Dim ret = Convert(value, parameter, culture)
            Return ProcessReturnValue(ret, targetType)
        End Function
        ''' <summary>Converts <see cref="Boolean"/> return value to given target type</summary>
        ''' <param name="ret">Value produced by converter</param>
        ''' <param name="targetType">The type of the binding target property. Ignored if null.
        ''' Preffered types are <see cref="Boolean"/> and <see cref="Nullable(Of T)"/>[<see cref="Boolean"/>].
        ''' Specially supported types are <see cref="Visibility"/> and <see cref="Nullable(Of T)"/>[<see cref="Visibility"/>].
        ''' If target type is not one ow types mentioned above <see cref="DynamicCast"/> is used.
        ''' If <see cref="DynamicCast"/> throws <see cref="InvalidCastException"/> or <see cref="Reflection.AmbiguousMatchException"/> unconverted return value is returned instead.
        ''' </param>
        ''' <returns><paramref name="ret"/> converted to <paramref name="targetType"/>.</returns>
        ''' <remarks>This implementation uses <see cref="ProcessReturnValue"/></remarks>
        Protected Overridable Function ProcessReturnValueInternal(ByVal ret As Boolean?, ByVal targetType As System.Type) As Object
            Return ProcessReturnValue(ret, targetType)
        End Function

        ''' <summary>Converts <see cref="Boolean"/> return value to given target type</summary>
        ''' <param name="ret">Value produced by converter</param>
        ''' <param name="targetType">The type of the binding target property. Ignored if null.
        ''' Preffered types are <see cref="Boolean"/> and <see cref="Nullable(Of T)"/>[<see cref="Boolean"/>].
        ''' Specially supported types are <see cref="Visibility"/> and <see cref="Nullable(Of T)"/>[<see cref="Visibility"/>].
        ''' If target type is not one ow types mentioned above <see cref="DynamicCast"/> is used.
        ''' If <see cref="DynamicCast"/> throws <see cref="InvalidCastException"/> or <see cref="Reflection.AmbiguousMatchException"/> unconverted return value is returned instead.
        ''' </param>
        ''' <returns><paramref name="ret"/> converted to <paramref name="targetType"/>.</returns>
        Public Shared Function ProcessReturnValue(ByVal ret As Boolean?, ByVal targetType As System.Type) As Object
            If targetType Is Nothing OrElse ret Is Nothing Then Return ret
            If targetType.IsAssignableFrom(GetType(Boolean)) OrElse targetType.IsAssignableFrom(GetType(Boolean?)) Then Return ret
            If targetType.IsAssignableFrom(GetType(Visibility)) OrElse targetType.IsAssignableFrom(GetType(Visibility?)) Then Return If(ret, Visibility.Visible, Visibility.Collapsed)
            Try
                Return DynamicCast(ret, targetType)
            Catch ex As Exception When TypeOf ex Is InvalidCastException OrElse TypeOf ex Is Reflection.AmbiguousMatchException
                Return ret
            End Try
        End Function

        ''' <summary>When implemented in derived class converts a value to <see cref="Boolean"/> value</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        Protected MustOverride Function Convert(ByVal value As Object, ByVal parameter As Object, ByVal culture As CultureInfo) As Boolean?

        ''' <summary>If implemented in derived class converts a value back.</summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="targetType">The type to convert to.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <exception cref="NotSupportedException">This implementation always throws <see cref="NotSupportedException"/>.</exception>
        Protected Overridable Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            Throw New NotSupportedException(WindowsT.WPF.ConvertersT.ConverterResources.ex_CannotConvertBack.f(Me.GetType().Name))
        End Function
    End Class
End Namespace