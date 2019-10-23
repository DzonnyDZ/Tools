Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data
Imports System.Windows.Media


Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converts value of type <see cref="Int32"/> to <see cref="Color"/></summary>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class IntColorConverter
        Implements IValueConverter

        ''' <summary>Converts a value.</summary>
        ''' <returns>A converted value. Either <see cref="Color"/>, <see cref="SolidColorBrush"/> or <see cref="System.Drawing.Color"/> depending on <paramref name="targetType"/>. The color of retuned objects if made from <paramref name="value"/> as if it represents ARGB color code.</returns>
        ''' <param name="value">The value produced by the binding source. It must be built-in numeric type representing color ARGB code, <see cref="Color"/>, <see cref="SolidColorBrush"/> or <see cref="System.Drawing.Color"/>.</param>
        ''' <param name="targetType">The type of the binding target property.</param>
        ''' <param name="parameter">ignored</param>
        ''' <param name="culture">ignored</param>
        ''' <remarks>Return type of this method depends on <paramref name="targetType"/>:
        ''' <para>If it <see cref="Type.IsAssignableFrom">is assignable form</see> <see cref="System.Drawing.Color"/> (but not from <see cref="Brush"/> or <see cref="Color"/>), <see cref="System.Drawing.Color"/> is retuned.</para>
        ''' <para>If it <see cref="Type.IsAssignableFrom">is assignable form</see> <see cref="Brush"/> (but not from <see cref="Color"/>), <see cref="SolidColorBrush"/> is returned.</para>
        ''' <para>If it <see cref="Type.IsAssignableFrom">is assignable form</see> <see cref="Color"/>, <see cref="Color"/> is returned.</para>
        ''' <para>If it <see cref="Type.IsAssignableFrom">is assignable form</see> neither from <see cref="Color"/>, <see cref="Brush"/> not <see cref="System.Drawing.Color"/> an exception is thrown.</para></remarks>
        ''' <exception cref="OverflowException">Numeric type conversion from <paramref name="value"/> to <see cref="Integer"/> failed.</exception>
        ''' <exception cref="ArgumentException"><paramref name="value"/> is neither <see cref="Integer"/>, <see cref="SByte"/>, <see cref="Byte"/>, <see cref="UShort"/>, <see cref="Short"/>, <see cref="UInt32"/>, <see cref="ULong"/>, <see cref="Long"/>, <see cref="Decimal"/>, <see cref="Single"/>, <see cref="Double"/>, <see cref="Color"/>, <see cref="System.Drawing.Color"/> nor <see cref="SolidColorBrush"/></exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            Dim val As Int32
            If TypeOf value Is Int32 Then : val = DirectCast(value, Int32)
            ElseIf TypeOf value Is Int16 Then : val = DirectCast(value, Int16)
            ElseIf TypeOf value Is Int64 Then : val = DirectCast(value, Int64)
            ElseIf TypeOf value Is UInt32 Then : val = DirectCast(value, UInt32)
            ElseIf TypeOf value Is UInt16 Then : val = DirectCast(value, UInt16)
            ElseIf TypeOf value Is UInt64 Then : val = DirectCast(value, UInt64)
            ElseIf TypeOf value Is Byte Then : val = DirectCast(value, Byte)
            ElseIf TypeOf value Is SByte Then : val = DirectCast(value, SByte)
            ElseIf TypeOf value Is Decimal Then : val = DirectCast(value, Decimal)
            ElseIf TypeOf value Is Double Then : val = DirectCast(value, Double)
            ElseIf TypeOf value Is Single Then : val = DirectCast(value, Single)
            ElseIf TypeOf value Is System.Drawing.Color Then : val = DirectCast(value, System.Drawing.Color).ToArgb
            ElseIf TypeOf value Is Color Then : val = DirectCast(value, Color).ToArgb
            ElseIf TypeOf value Is SolidColorBrush Then : val = DirectCast(value, SolidColorBrush).Color.ToArgb
            Else : Throw New ArgumentException(ConverterResources.ex_ConvertOnlyFromNumericAndColors)
            End If
            If (targetType.IsAssignableFrom(GetType(System.Drawing.Color)) OrElse targetType.IsAssignableFrom(GetType(System.Drawing.Color?))) AndAlso _
                    Not targetType.IsAssignableFrom(GetType(Brush)) AndAlso _
                    Not (targetType.IsAssignableFrom(GetType(Color)) OrElse targetType.IsAssignableFrom(GetType(Color?))) Then _
                        Return System.Drawing.Color.FromArgb(val)
            If targetType.IsAssignableFrom(GetType(Brush)) AndAlso Not (targetType.IsAssignableFrom(GetType(Color)) OrElse targetType.IsAssignableFrom(GetType(Color?))) Then _
                Return New SolidColorBrush(System.Drawing.Color.FromArgb(val).ToColor)
            Return System.Drawing.Color.FromArgb(val).ToColor
        End Function

        ''' <summary>Converts a value.</summary>
        ''' <returns>Converterd value. Type depends on <paramref name="targetType"/>. Typically is returned integer representing ARGB value of color given in <paramref name="value"/>.</returns>
        ''' <param name="value">>A converted value. This must be either null, <see cref="Color"/>, <see cref="SolidColorBrush"/> or <see cref="System.Drawing.Color"/>.</param>
        ''' <param name="targetType">The type to convert to. It must be either <see cref="Color"/>, <see cref="SolidColorBrush"/>, <see cref="System.Drawing.Color"/> or type <see cref="Integer"/> is <see cref="TypeTools.DynamicCast">danamicly castable</see> to.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <exception cref="TypeMismatchException"><paramref name="value"/> is neither <see cref="Color"/>, <see cref="SolidColorBrush"/> nor <see cref="System.Drawing.Color"/></exception>
        ''' <exception cref="ArgumentNullException"><paramref name="targetType"/> is null</exception>
        ''' <exception cref="InvalidCastException">Failed to convert <see cref="Integer"/> to <paramref name="targetType"/> and <paramref name="targetType"/> is neither <see cref="Color"/>, <see cref="Brush"/> nor <see cref="System.Drawing.Color"/>.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">Cast operators from <see cref="Integer"/> to <paramref name="targetType"/> were found, but no one is most specific and <paramref name="targetType"/> is neither <see cref="Color"/>, <see cref="Brush"/> nor <see cref="System.Drawing.Color"/>.</exception>
        ''' <exception cref="OverflowException">Conversion of type <see cref="Integer"/> to <paramref name="targetType"/> failed due to overwlow exception and <paramref name="targetType"/> is neither <see cref="Color"/>, <see cref="Brush"/> nor <see cref="System.Drawing.Color"/>.</exception>
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            If value Is Nothing Then Return Nothing
            Dim val As Color
            If TypeOf value Is Color Then : val = DirectCast(value, Color)
            ElseIf TypeOf value Is SolidColorBrush Then : val = DirectCast(value, SolidColorBrush).Color
            ElseIf TypeOf value Is System.Drawing.Color Then : val = DirectCast(value, System.Drawing.Color).ToColor
            Else : Throw New TypeMismatchException("value", value, GetType(Color), ConverterResources.ex_CanConvertBackOnlyFromColors.f(Me.GetType.Name, GetType(Color).FullName, GetType(System.Drawing.Color).FullName))
            End If
            If targetType.IsAssignableFrom(GetType(Color)) OrElse targetType.IsAssignableFrom(GetType(Color?)) Then : Return val
            ElseIf targetType.Equals(GetType(System.Drawing.Color)) OrElse targetType.Equals(GetType(System.Drawing.Color?)) Then : Return val.ToColor
            ElseIf targetType.Equals(GetType(Brush)) OrElse targetType.IsSubclassOf(GetType(Brush)) Then : Return New SolidColorBrush(val)
            Else : Return TypeTools.DynamicCast(val.ToArgb, targetType)
            End If
        End Function

    End Class
End Namespace
