Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data
Imports System.Windows.Media


Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converter that converts color to color negative to given color</summary>
    ''' <remarks>This converter does not change alpha cannel of color.
    ''' <para>This converter accepts and can return types <see cref="Color"/>, <see cref="System.Drawing.Color"/>, <see cref="SolidColorBrush"/> and <see cref="Integer"/> (ARGB value).</para></remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class NotColorConverter
        Implements IValueConverter
        ''' <summary>Converts a value.</summary>
        ''' <returns>A converted value. Null when <paramref name="value"/> is null; othervice solor negative to given color. Depending on <paramref name="targetType"/> value of following type is returned (in order of precedence, first type for which <paramref name="targetType"/>.<see cref="Type.IsAssignableFrom">IsAssignableFrom</see> returns true is used): <see cref="Color"/>, <see cref="System.Drawing.Color"/>, <see cref="SolidColorBrush"/>, <see cref="Integer"/>.</returns>
        ''' <param name="value">The value that is produced by the binding target. It shall be <see cref="Color"/>, <see cref="System.Drawing.Color"/>, <see cref="SolidColorBrush"/> or <see cref="Integer"/>.</param>
        ''' <param name="targetType">The type to convert to. Shall be one of following type or their base types: <see cref="Color"/>, <see cref="System.Drawing.Color"/>, <see cref="SolidColorBrush"/>, <see cref="Integer"/></param>
        ''' <param name="parameter">Ignored.</param>
        ''' <param name="culture">Ignored.</param>
        ''' <exception cref="ArgumentException"><paramref name="targetType"/> is assignable neither from <see cref="Color"/> nor from <see cref="System.Drawing.Color"/> nor from <see cref="SolidColorBrush"/> nor from <see cref="Integer"/></exception>
        ''' <exception cref="TypeMismatchException"><paramref name="value"/> is neither <see cref="Color"/> nor <see cref="System.Drawing.Color"/> nor <see cref="SolidColorBrush"/> nor <see cref="Integer"/></exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert, System.Windows.Data.IValueConverter.ConvertBack
            If value Is Nothing Then Return Nothing
            Dim col As System.Drawing.Color
            If TypeOf value Is System.Drawing.Color Then
                col = value
            ElseIf TypeOf value Is Color Then
                col = DirectCast(value, Color).ToColor
            ElseIf TypeOf value Is SolidColorBrush Then
                col = DirectCast(value, SolidColorBrush).Color.ToColor
            ElseIf TypeOf value Is Integer Then
                col = System.Drawing.Color.FromArgb(value)
            Else
                Throw New TypeMismatchException("value", value, GetType(Color), WindowsT.WPF.ConvertersT.ConverterResources.ex_ConverterAcceptsValuesOfType_3.f([GetType].Name, GetType(Color).FullName, GetType(System.Drawing.Color).FullName, GetType(Int32).FullName))
            End If
            col = System.Drawing.Color.FromArgb(col.A, Not col.R, Not col.G, Not col.B)
            If targetType Is Nothing OrElse targetType.IsAssignableFrom(GetType(Color)) Then
                Return col.ToColor
            ElseIf targetType.IsAssignableFrom(GetType(System.Drawing.Color)) Then
                Return col
            ElseIf targetType.IsAssignableFrom(GetType(SolidColorBrush)) Then
                Return New SolidColorBrush(col.ToColor)
            ElseIf targetType.IsAssignableFrom(GetType(Integer)) Then
                Return col.ToArgb
            Else
                Throw New ArgumentException(WindowsT.WPF.ConvertersT.ConverterResources.ex_ConverterCanConvertOnlyTo_3.f([GetType]().Name, GetType(Color).FullName, GetType(System.Drawing.Color).FullName, GetType(Int32).FullName))
            End If
        End Function


    End Class
End Namespace
