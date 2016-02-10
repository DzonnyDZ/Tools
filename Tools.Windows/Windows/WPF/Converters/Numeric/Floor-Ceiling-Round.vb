Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data, System.Globalization.CultureInfo

#If True
Namespace WindowsT.WPF.ConvertersT

    ''' <summary>Base class for rounding converters</summary>
    ''' <remarks>
    ''' See documentation of the <see cref="BaseRoundingConverter.Convert"/> function and documentation of derived classes.
    ''' <para>This converter is intended to be one-way, however when <see cref="IValueConverter.ConvertBack"/> is called it returns the same value as passed to onversion method.</para>
    ''' </remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public MustInherit Class BaseRoundingConverter
        Implements IValueConverter

        ''' <summary>Converts a value. </summary>
        ''' <param name="value">
        ''' The value produced by the binding source.
        ''' Supported types are: <see cref="Single"/>, <see cref="Double"/>, <see cref="Decimal"/>.
        ''' <see cref="String"/> is converted to <see cref="Double"/> using <see cref="System.Double.Parse"/>.
        ''' <see cref="IConvertible"/> is converted to <see cref="Double"/> using <see cref="IConvertible.ToDouble"/>.
        ''' Values of other type are <see cref="DynamicCast">dynamically casted</see> to <see cref="Double"/>.
        ''' </param>
        ''' <param name="targetType">The type of the binding target property. Ignored if null. If <paramref name="targetType"/> is not same as type of <paramref name="value"/> (after conversion), <see cref="DynamicCast"/> is used to convert return value to <paramref name="targetType"/>.</param>
        ''' <param name="parameter">
        ''' The converter parameter to use. See documentation of derived class if it uses parameter.
        ''' Parameter should be of type <see cref="Integer"/>. Null defaults to 0. <see cref="String"/> is converted using <see cref="Int32.Parse"/> (with <see cref="InvariantCulture"/>). <see cref="IConvertible"/> is converted using <see cref="IConvertible.ToInt32"/> (with <see cref="InvariantCulture"/>). Values of other types are converted using <see cref="DynamicCast"/>.
        ''' If derived class supports parameter the meaning of the parameter is number of decimal places.
        ''' </param>
        ''' <param name="culture">The culture to use in the converter. Used only when <paramref name="value"/> is <see cref="String"/> or another <see cref="IConvertible"/>.</param>
        ''' <returns>A converted value. Null if <paramref name="value"/> is null. If <paramref name="value"/> is one of directly supported types (<see cref="Single"/>, <see cref="Double"/>, <see cref="Decimal"/>) default return type is same as type of <paramref name="value"/>. Otherwise default return type is <see cref="Double"/>. If <paramref name="targetType"/> is specified return value is converted to that type using <see cref="DynamicCast"/>.</returns>
        ''' <exception cref="InvalidCastException">A dynamic conversion failed. See <see cref="DynamicCast"/>.</exception>
        ''' <exception cref="OverflowAction">Numeric falue conversion failed</exception>
        ''' <exception cref="Format">Attempt to convert string to number failed.</exception>
        ''' <seelaso cref="Reflection.AmbiguousMatchException">Conversion operators were found but no one is most spcecific.</seelaso>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            Dim par As Integer = 0
            If TypeOf parameter Is Integer Then
                par = DirectCast(parameter, Integer)
            ElseIf TypeOf parameter Is String Then
                par = Integer.Parse(parameter, InvariantCulture)
            ElseIf TypeOf parameter Is IConvertible Then
                par = DirectCast(parameter, IConvertible).ToInt32(InvariantCulture)
            ElseIf parameter IsNot Nothing Then
                par = DynamicCast(Of Integer)(parameter)
            End If
            Dim ret As Object
            If TypeOf value Is Single Then
                ret = Convert(DirectCast(value, Single), par)
            ElseIf TypeOf value Is Double Then
                ret = Convert(DirectCast(value, Double), par)
            ElseIf TypeOf value Is Decimal Then
                ret = Convert(DirectCast(value, Decimal), par)
            ElseIf TypeOf value Is String Then
                ret = Convert(Double.Parse(value, culture), par)
            ElseIf TypeOf value Is IConvertible Then
                ret = Convert(DirectCast(value, IConvertible).ToDouble(culture), par)
            Else
                ret = Convert(DynamicCast(Of Double)(value), par)
            End If
            If targetType Is Nothing Then Return ret
            Return DynamicCast(ret, targetType)
        End Function

        ''' <summary>Converts a value back.</summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used. This impleementation returns <paramref name="value"/>. When <paramref name="targetType"/> is specified <paramref name="value"/> is converted to <paramref name="targetType"/> using <see cref="DynamicCast"/>.</returns>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="targetType">The type to convert to.</param>
        ''' <param name="parameter">Ignored.</param>
        ''' <param name="culture">Ignored.</param>
        ''' <exception cref="InvalidCastException"><see cref="DynamicCast"/> failed.</exception>
        ''' <exception cref="OverflowException">Overflow when converting numeric values.</exception>
        ''' <exception cref="FormatException">Error during conversion</exception>
        ''' <seelaso cref="Reflection.AmbiguousMatchException">Conversion operators were found but no one is most spcecific.</seelaso>
        Private Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            If targetType Is Nothing Then Return value
            Return DynamicCast(value, targetType)
        End Function

        ''' <summary>When overridden in derived class converts a value of type <see cref="Single"/>.</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="parameter">Conversion parameter. Number of decimal places if derived class supports the parameter; ignored otherwise.</param>
        ''' <remarks>A converted value</remarks>
        Protected MustOverride Function Convert(ByVal value As Single, ByVal parameter As Integer) As Single
        ''' <summary>When overridden in derived class converts a value of type <see cref="Double"/>.</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="parameter">Conversion parameter. Number of decimal places if derived class supports the parameter; ignored otherwise.</param>
        ''' <remarks>A converted value</remarks>
        Protected MustOverride Function Convert(ByVal value As Double, ByVal parameter As Integer) As Double
        ''' <summary>When overridden in derived class converts a value of type <see cref="Decimal"/>.</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="parameter">Conversion parameter. Number of decimal places if derived class supports the parameter; ignored otherwise.</param>
        ''' <remarks>A converted value</remarks>
        Protected MustOverride Function Convert(ByVal value As Decimal, ByVal parameter As Integer) As Decimal
    End Class

    ''' <summary>Converter which performs the <see cref="Math.Floor"/> operation (gets integral part of number)</summary>
    ''' <remarks>This <see cref="BaseRoundingConverter"/>-derived class does not support parameter.</remarks>
    ''' <seelaso cref="Math.Floor"/>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class FloorConverter
        Inherits BaseRoundingConverter
        ''' <summary>Converts a value of type <see cref="Single"/> using <see cref="Math.Floor"/>.</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="parameter">Ignored (not supported).</param>
        ''' <remarks>A converted value - integral part of <paramref name="value"/>.</remarks>
        ''' <seelaso cref="Math.Floor"/>
        Protected Overloads Overrides Function Convert(ByVal value As Decimal, ByVal parameter As Integer) As Decimal
            Return Math.Floor(value)
        End Function
        ''' <summary>Converts a value of type <see cref="Double"/> using <see cref="Math.Floor"/>.</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="parameter">Ignored (not supported).</param>
        ''' <remarks>A converted value - integral part of <paramref name="value"/>.</remarks>
        ''' <seelaso cref="Math.Floor"/>
        Protected Overloads Overrides Function Convert(ByVal value As Double, ByVal parameter As Integer) As Double
            Return Math.Floor(value)
        End Function
        ''' <summary>Converts a value of type <see cref="Decimal"/> using <see cref="Math.Floor"/>.</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="parameter">Ignored (not supported).</param>
        ''' <remarks>A converted value - integral part of <paramref name="value"/>.</remarks>
        ''' <seelaso cref="Math.Floor"/>
        Protected Overloads Overrides Function Convert(ByVal value As Single, ByVal parameter As Integer) As Single
            Return Math.Floor(value)
        End Function
    End Class

    ''' <summary>Converter which performs the <see cref="Math.Ceiling"/> operation (gets smallest integer value greater or equal to given value)</summary>
    ''' <remarks>This <see cref="BaseRoundingConverter"/>-derived class does not support parameter.</remarks>
    ''' <seelaso cref="Math.Ceiling"/>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class CeilingConverter
        Inherits BaseRoundingConverter
        ''' <summary>Converts a value of type <see cref="Single"/> using <see cref="Math.Ceiling"/>.</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="parameter">Ignored (not supported).</param>
        ''' <remarks>A converted value - lowest inegral number greater than or equal to <paramref name="value"/>.</remarks>
        ''' <seelaso cref="Math.Ceiling"/>
        Protected Overloads Overrides Function Convert(ByVal value As Decimal, ByVal parameter As Integer) As Decimal
            Return Math.Ceiling(value)
        End Function
        ''' <summary>Converts a value of type <see cref="Double"/> using <see cref="Math.Ceiling"/>.</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="parameter">Ignored (not supported).</param>
        ''' <remarks>A converted value - lowest inegral number greater than or equal to <paramref name="value"/>.</remarks>
        ''' <seelaso cref="Math.Ceiling"/>
        Protected Overloads Overrides Function Convert(ByVal value As Double, ByVal parameter As Integer) As Double
            Return Math.Ceiling(value)
        End Function
        ''' <summary>Converts a value of type <see cref="Decimal"/> using <see cref="Math.Ceiling"/>.</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="parameter">Ignored (not supported).</param>
        ''' <remarks>A converted value - lowest inegral number greater than or equal to <paramref name="value"/>.</remarks>
        ''' <seelaso cref="Math.Ceiling"/>
        Protected Overloads Overrides Function Convert(ByVal value As Single, ByVal parameter As Integer) As Single
            Return Math.Ceiling(value)
        End Function
    End Class

    ''' <summary>Converter which performs the <see cref="Math.Round"/> operation - rounding midpoint numbers to even</summary>
    ''' <remarks>This <see cref="BaseRoundingConverter"/>-derived class supports parameter (number of decimal places).</remarks>
    ''' <seelaso cref="Math.Round"/><seelaso cref="MidpointRounding.ToEven"/>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class RoundToEvenConverter
        Inherits BaseRoundingConverter
        ''' <summary>Converts a value of type <see cref="Decimal"/> using <see cref="Math.Round"/>.</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="parameter">Number of decimal places</param>
        ''' <remarks>A converted value - <paramref name="value"/> rounded to given number of decimal places. Midpoint rounds to even.</remarks>
        ''' <seelaso cref="Math.Round"/><seelaso cref="MidpointRounding.ToEven"/>
        Protected Overloads Overrides Function Convert(ByVal value As Decimal, ByVal parameter As Integer) As Decimal
            Return Math.Round(value, parameter, MidpointRounding.ToEven)
        End Function
        ''' <summary>Converts a value of type <see cref="Double"/> using <see cref="Math.Round"/>.</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="parameter">Number of decimal places</param>
        ''' <remarks>A converted value - <paramref name="value"/> rounded to given number of decimal places. Midpoint rounds to even.</remarks>
        ''' <seelaso cref="Math.Round"/><seelaso cref="MidpointRounding.ToEven"/>
        Protected Overloads Overrides Function Convert(ByVal value As Double, ByVal parameter As Integer) As Double
            Return Math.Round(value, parameter, MidpointRounding.ToEven)
        End Function
        ''' <summary>Converts a value of type <see cref="Single"/> using <see cref="Math.Round"/>.</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="parameter">Number of decimal places</param>
        ''' <remarks>A converted value - <paramref name="value"/> rounded to given number of decimal places. Midpoint rounds to even.</remarks>
        ''' <seelaso cref="Math.Round"/><seelaso cref="MidpointRounding.ToEven"/>
        Protected Overloads Overrides Function Convert(ByVal value As Single, ByVal parameter As Integer) As Single
            Return Math.Round(value, parameter, MidpointRounding.ToEven)
        End Function
    End Class

    ''' <summary>Converter which performs the <see cref="Math.Round"/> operation - rounding midpoint numbers away from zero</summary>
    ''' <remarks>This <see cref="BaseRoundingConverter"/>-derived class supports parameter (number of decimal places).</remarks>
    ''' <seelaso cref="Math.Round"/><seelaso cref="MidpointRounding.AwayFromZero"/>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class RoundAwayFromZeroConverter
        Inherits BaseRoundingConverter
        ''' <summary>Converts a value of type <see cref="Decimal"/> using <see cref="Math.Round"/>.</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="parameter">Number of decimal places</param>
        ''' <remarks>A converted value - <paramref name="value"/> rounded to given number of decimal places. Midpoint rounds away from zero.</remarks>
        ''' <seelaso cref="Math.Round"/><seelaso cref="MidpointRounding.AwayFromZero"/>
        Protected Overloads Overrides Function Convert(ByVal value As Decimal, ByVal parameter As Integer) As Decimal
            Return Math.Round(value, parameter, MidpointRounding.AwayFromZero)
        End Function
        ''' <summary>Converts a value of type <see cref="Double"/> using <see cref="Math.Round"/>.</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="parameter">Number of decimal places</param>
        ''' <remarks>A converted value - <paramref name="value"/> rounded to given number of decimal places. Midpoint rounds away from zero.</remarks>
        ''' <seelaso cref="Math.Round"/><seelaso cref="MidpointRounding.AwayFromZero"/>
        Protected Overloads Overrides Function Convert(ByVal value As Double, ByVal parameter As Integer) As Double
            Return Math.Round(value, parameter, MidpointRounding.AwayFromZero)
        End Function
        ''' <summary>Converts a value of type <see cref="Single"/> using <see cref="Math.Round"/>.</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="parameter">Number of decimal places</param>
        ''' <remarks>A converted value - <paramref name="value"/> rounded to given number of decimal places. Midpoint rounds away from zero.</remarks>
        ''' <seelaso cref="Math.Round"/><seelaso cref="MidpointRounding.AwayFromZero"/>
        Protected Overloads Overrides Function Convert(ByVal value As Single, ByVal parameter As Integer) As Single
            Return Math.Round(value, parameter, MidpointRounding.AwayFromZero)
        End Function
    End Class
End Namespace
#End If
