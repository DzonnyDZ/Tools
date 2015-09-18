Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data

#If True
Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converter that converts null values to false and non-null values to true.</summary>
    ''' <remarks>Additionally if targetType is <see cref="System.Windows.Visibility"/> it converts null to <see cref="System.Windows.Visibility.Collapsed"/> and non-null to <see cref="System.Windows.Visibility.Visible"/>.
    ''' <para>This converter is designed as one-way.</para></remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class NullFalseConverter
        Implements IValueConverter
        ''' <summary>Converts a value.</summary>
        ''' <returns>
        ''' If <paramref name="targetType"/> <see cref="Type.IsAssignableFrom">is assignable from</see> <see cref="Boolean"/> returns true when <paramref name="value"/> is not null; false when <paramref name="value"/> is null.
        ''' If <paramref name="targetType"/> <see cref="Type.IsAssignableFrom">is not assignable from</see> from <see cref="Boolean"/> but it <see cref="Type.IsAssignableFrom">is assignable from</see> <see cref="System.Windows.Visibility"/> returns <see cref="System.Windows.Visibility.Visible"/> when <paramref name="parameter"/> is not null; <see cref="System.Windows.Visibility.Collapsed"/> when <paramref name="value"/> is null.
        ''' </returns>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property. Must <see cref="Type.IsAssignableFrom">be assignable from</see> either <see cref="Boolean"/> or <see cref="System.Windows.Visibility"/>.</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <exception cref="NotSupportedException"><paramref name="targetType"/> <see cref="Type.IsAssignableFrom">is assignable from</see> neither <see cref="Boolean"/> nor <see cref="System.Windows.Visibility"/>.</exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If targetType.IsAssignableFrom(GetType(Boolean)) Then
                Return value IsNot Nothing
            ElseIf targetType.IsAssignableFrom(GetType(System.Windows.Visibility)) Then
                Return If(value Is Nothing, System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible)
            Else
                Throw New NotSupportedException(ConverterResources.ex_ConvertsOnlyToBool.f(Me.GetType.Name))
            End If
        End Function

        ''' <summary>Converts a value.</summary>
        ''' <returns>A converted value. If <paramref name="value"/> is <see cref="Boolean"/> false or <paramref name="value"/> is <see cref="System.Windows.Visibility.Collapsed"/> returns null. Otherwise an exception is thrown.</returns>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="targetType">ignored</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <exception cref="NotSupportedException"><paramref name="value"/> is neither false nor <see cref="System.Windows.Visibility.Collapsed"/> - this converter is not designed to be used with <see cref="ConvertBack"/></exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            If TypeOf value Is Boolean AndAlso DirectCast(value, Boolean) = False Then Return Nothing
            Throw New NotSupportedException(ConverterResources.ex_CanConvertBackOnlyFromFalse.f(Me.GetType.Name))
        End Function
    End Class
End Namespace
#End If