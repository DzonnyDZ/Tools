Imports Tools.TypeTools, Tools.ReflectionT, Tools.ExtensionsT
Imports System.ComponentModel, System.Linq
Imports System.Security
Imports System.Numerics
Imports System.Net, Tools.WindowsT.InteropT
Imports System.Windows.Data


Namespace WindowsT.WPF.ConvertersT
    ''' <summary>Converter that adds value to a numeric value</summary>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    ''' <version version="1.5.4">New property <see cref="PlusConverter.UseDynamicCast"/> now allows to chose wheather <see cref="DynamicCast"/> is used or not.</version>
    Public Class PlusConverter
        Implements IValueConverter

        ''' <summary>Gets or sets value indicating if conversion function uses <see cref="DynamicCast"/> to convert actual type to target type</summary>
        ''' <remarks>If this property is fale, value of target type is returned without attempt to cast it (only special buil-in conversions are considered).</remarks>7
        ''' <version version="1.5.4">This property is new in version 1.5.4</version>
        <DefaultValue(True)>
        Public Property UseDynamicCast As Boolean = True

        ''' <summary>Converts a value.</summary>
        ''' <returns>A converted value. <paramref name="value"/> + <paramref name="parameter"/>. If <paramref name="parameter"/> <paramref name="value"/> <see cref="TypeTools.DynamicCast">dynamicly casted</see> to <paramref name="targetType"/> is returned.</returns>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property. Result of addition <paramref name="value"/> + <paramref name="parameter"/> (where <paramref name="parameter"/> is firts <see cref="TypeTools.DynamicCast">dynamicly casted</see> to type of <paramref name="value"/>) must be <see cref="TypeTools.DynamicCast"/> dynamicly castable to this type.</param>
        ''' <param name="parameter">Value to add to <paramref name="value"/>. This value must be <see cref="TypeTools.DynamicCast">dynamicly castable</see> to type of <paramref name="value"/>.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null and <paramref name="parameter"/> is not null -or- <paramref name="targetType"/> is null.</exception>
        ''' <exception cref="NotSupportedException"><paramref name="value"/> is not of supported type (see remarks for list of supporetd types)</exception>
        ''' <exception cref="InvalidCastException">Unable to cast <paramref name="parameter"/> to type of <paramref name="value"/> -or- unable to cast result of arithmetic operation to <paramref name="targetType"/>. See <see cref="TypeTools.DynamicCast"/> for more information.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">Cast operators from <paramref name="parameter"/> to type of <paramref name="value"/> or from result of addition to <paramref name="targetType"/> were found, but none is more specific. See <see cref="TypeTools.DynamicCast"/> for more info.</exception>
        ''' <exception cref="OverflowException">Arithmetic operation or type conversion resulted in overflow.</exception>
        ''' <exception cref="Exception">Format exception when attempting to convert string to numeric value</exception>
        ''' <remarks>Supported types are:
        ''' <list type="bullet">
        ''' <item><see cref="Byte"/></item>
        ''' <item><see cref="SByte"/></item>
        ''' <item><see cref="UShort"/></item>
        ''' <item><see cref="Short"/></item>
        ''' <item><see cref="UInteger"/></item>
        ''' <item><see cref="Integer"/></item>
        ''' <item><see cref="ULong"/></item>
        ''' <item><see cref="Long"/></item>
        ''' <item><see cref="Decimal"/></item>
        ''' <item><see cref="Single"/></item>
        ''' <item><see cref="Double"/></item>
        ''' </list>Cross-type conversions are performed via <see cref="TypeTools.DynamicCast"/>. <paramref name="parameter"/> can be of any (even not specifically supported) type that can be <see cref="TypeTools.DynamicCast">dynamicaly casted</see> to type of <paramref name="value"/>. <paramref name="targetType"/> may be of any type result of arithmetic operation can be <see cref="TypeTools.DynamicCast"/> dynamically casted to. Arithmetic operation is performed using Visual Basic + and - operators.</remarks>
        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            Dim oldC As Globalization.CultureInfo = Nothing
            If culture IsNot Nothing Then
                oldC = System.Threading.Thread.CurrentThread.CurrentCulture
                System.Threading.Thread.CurrentThread.CurrentCulture = culture
            End If
            Try
                Return ConvertInternal(value, targetType, parameter, False)
            Finally
                If oldC IsNot Nothing Then System.Threading.Thread.CurrentThread.CurrentCulture = oldC
            End Try
        End Function
        ''' <summary>Performs the convert or convert back operation</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="targetType">Target type of conversion. Result of <paramref name="value"/> + <paramref name="parameter"/> must be <see cref="TypeTools.DynamicCast">dynamicly castable</see> to this type.</param>
        ''' <param name="parameter">Value to add to or subtract from <paramref name="value"/>. This value must be <see cref="TypeTools.DynamicCast">dynamicly castable</see> to type of <paramref name="value"/></param>
        ''' <param name="minus">True to perform subtraction, false to perform addition</param>
        ''' <returns><paramref name="value"/> + or - <paramref name="parameter"/>. If <paramref name="parameter"/> is null <paramref name="value"/> <see cref="TypeTools.DynamicCast">dynamicly casted</see> to <paramref name="targetType"/> is returned.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null and <paramref name="parameter"/> is not null -or- <paramref name="targetType"/> is null.</exception>
        ''' <exception cref="NotSupportedException"><paramref name="value"/> is not of supported type (see remarks for list of supporetd types)</exception>
        ''' <exception cref="InvalidCastException">Unable to cast <paramref name="parameter"/> to type of <paramref name="value"/> -or- unable to cast result of arithmetic operation to <paramref name="targetType"/>. See <see cref="TypeTools.DynamicCast"/> for more information.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">Cast operators from <paramref name="parameter"/> to type of <paramref name="value"/> or from result of addition to <paramref name="targetType"/> were found, but none is more specific. See <see cref="TypeTools.DynamicCast"/> for more info.</exception>
        ''' <exception cref="OverflowException">Arithmetic operation or type conversion resulted in overflow.</exception>
        ''' <exception cref="Exception">Format exception when attempting to convert string to numeric value</exception>
        ''' <remarks>Supported types are:
        ''' <list type="bullet">
        ''' <item><see cref="Byte"/></item>
        ''' <item><see cref="SByte"/></item>
        ''' <item><see cref="UShort"/></item>
        ''' <item><see cref="Short"/></item>
        ''' <item><see cref="UInteger"/></item>
        ''' <item><see cref="Integer"/></item>
        ''' <item><see cref="ULong"/></item>
        ''' <item><see cref="Long"/></item>
        ''' <item><see cref="Decimal"/></item>
        ''' <item><see cref="Single"/></item>
        ''' <item><see cref="Double"/></item>
        ''' </list>Cross-type conversions are performed via <see cref="TypeTools.DynamicCast"/>. <paramref name="parameter"/> can be of any (even not specifically supported) type that can be <see cref="TypeTools.DynamicCast">dynamicaly casted</see> to type of <paramref name="value"/>. <paramref name="targetType"/> may be of any type result of arithmetic operation can be <see cref="TypeTools.DynamicCast"/> dynamically casted to. Arithmetic operation is performed using Visual Basic + and - operators.</remarks>
        Private Function ConvertInternal(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal minus As Boolean) As Object
            If targetType Is Nothing Then Throw New ArgumentNullException("targetType")
            If parameter Is Nothing Then Return Tools.DynamicCast(value, targetType)
            Dim ret As Object
            If value Is Nothing Then Throw New ArgumentNullException("value")
            If TypeOf value Is Byte Then
                If minus Then ret = DirectCast(value, Byte) - TypeTools.DynamicCast(Of Byte)(parameter) Else ret = DirectCast(value, Byte) + TypeTools.DynamicCast(Of Byte)(parameter)
            ElseIf TypeOf value Is SByte Then
                If minus Then ret = DirectCast(value, SByte) - TypeTools.DynamicCast(Of SByte)(parameter) Else ret = DirectCast(value, SByte) + TypeTools.DynamicCast(Of SByte)(parameter)
            ElseIf TypeOf value Is UShort Then
                If minus Then ret = DirectCast(value, UShort) - TypeTools.DynamicCast(Of UShort)(parameter) Else ret = DirectCast(value, UShort) + TypeTools.DynamicCast(Of UShort)(parameter)
            ElseIf TypeOf value Is Short Then
                If minus Then ret = DirectCast(value, Short) - TypeTools.DynamicCast(Of Short)(parameter) Else ret = DirectCast(value, Short) + TypeTools.DynamicCast(Of Short)(parameter)
            ElseIf TypeOf value Is UInteger Then
                If minus Then ret = DirectCast(value, UInteger) - TypeTools.DynamicCast(Of UInteger)(parameter) Else ret = DirectCast(value, UInteger) + TypeTools.DynamicCast(Of UInteger)(parameter)
            ElseIf TypeOf value Is Integer Then
                If minus Then ret = DirectCast(value, Integer) - TypeTools.DynamicCast(Of Integer)(parameter) Else ret = DirectCast(value, Integer) + TypeTools.DynamicCast(Of Integer)(parameter)
            ElseIf TypeOf value Is ULong Then
                If minus Then ret = DirectCast(value, ULong) - TypeTools.DynamicCast(Of ULong)(parameter) Else ret = DirectCast(value, ULong) + TypeTools.DynamicCast(Of ULong)(parameter)
            ElseIf TypeOf value Is Long Then
                If minus Then ret = DirectCast(value, Long) - TypeTools.DynamicCast(Of Long)(parameter) Else ret = DirectCast(value, Long) + TypeTools.DynamicCast(Of Long)(parameter)
            ElseIf TypeOf value Is Decimal Then
                If minus Then ret = DirectCast(value, Decimal) - TypeTools.DynamicCast(Of Decimal)(parameter) Else ret = DirectCast(value, Decimal) + TypeTools.DynamicCast(Of Decimal)(parameter)
            ElseIf TypeOf value Is Single Then
                If minus Then ret = DirectCast(value, Single) - TypeTools.DynamicCast(Of Single)(parameter) Else ret = DirectCast(value, Single) + TypeTools.DynamicCast(Of Single)(parameter)
            ElseIf TypeOf value Is Double Then
                If minus Then ret = DirectCast(value, Double) - TypeTools.DynamicCast(Of Double)(parameter) Else ret = DirectCast(value, Double) + TypeTools.DynamicCast(Of Double)(parameter)
            Else
                Throw New NotSupportedException(ConverterResources.ex_UnsupportedDataType.f(value.GetType.Name))
            End If
            If UseDynamicCast Then
                Return TypeTools.DynamicCast(ret, targetType)
            Else
                Return ret
            End If
        End Function
        ''' <summary>Converts a value.</summary>
        ''' <returns>A converted value. <paramref name="value"/> - <paramref name="parameter"/>. If <paramref name="parameter"/> <paramref name="value"/> <see cref="TypeTools.DynamicCast">dynamicly casted</see> to <paramref name="targetType"/> is returned.</returns>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property. Result of addition <paramref name="value"/> - <paramref name="parameter"/> (where <paramref name="parameter"/> is firts <see cref="TypeTools.DynamicCast">dynamicly casted</see> to type of <paramref name="value"/>) must be <see cref="TypeTools.DynamicCast"/> dynamicly castable to this type.</param>
        ''' <param name="parameter">Value to add to <paramref name="value"/>. This value must be <see cref="TypeTools.DynamicCast">dynamicly castable</see> to type of <paramref name="value"/>.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null and <paramref name="parameter"/> is not null -or- <paramref name="targetType"/> is null.</exception>
        ''' <exception cref="NotSupportedException"><paramref name="value"/> is not of supported type (see remarks for list of supporetd types)</exception>
        ''' <exception cref="InvalidCastException">Unable to cast <paramref name="parameter"/> to type of <paramref name="value"/> -or- unable to cast result of arithmetic operation to <paramref name="targetType"/>. See <see cref="TypeTools.DynamicCast"/> for more information.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">Cast operators from <paramref name="parameter"/> to type of <paramref name="value"/> or from result of addition to <paramref name="targetType"/> were found, but none is more specific. See <see cref="TypeTools.DynamicCast"/> for more info.</exception>
        ''' <exception cref="OverflowException">Arithmetic operation or type conversion resulted in overflow.</exception>
        ''' <exception cref="Exception">Format exception when attempting to convert string to numeric value</exception>
        ''' <remarks>Supported types are:
        ''' <list type="bullet">
        ''' <item><see cref="Byte"/></item>
        ''' <item><see cref="SByte"/></item>
        ''' <item><see cref="UShort"/></item>
        ''' <item><see cref="Short"/></item>
        ''' <item><see cref="UInteger"/></item>
        ''' <item><see cref="Integer"/></item>
        ''' <item><see cref="ULong"/></item>
        ''' <item><see cref="Long"/></item>
        ''' <item><see cref="Decimal"/></item>
        ''' <item><see cref="Single"/></item>
        ''' <item><see cref="Double"/></item>
        ''' </list>Cross-type conversions are performed via <see cref="TypeTools.DynamicCast"/>. <paramref name="parameter"/> can be of any (even not specifically supported) type that can be <see cref="TypeTools.DynamicCast">dynamicaly casted</see> to type of <paramref name="value"/>. <paramref name="targetType"/> may be of any type result of arithmetic operation can be <see cref="TypeTools.DynamicCast"/> dynamically casted to. Arithmetic operation is performed using Visual Basic + and - operators.</remarks>
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            Dim oldC As Globalization.CultureInfo = Nothing
            If culture IsNot Nothing Then
                oldC = System.Threading.Thread.CurrentThread.CurrentCulture
                System.Threading.Thread.CurrentThread.CurrentCulture = culture
            End If
            Try
                Return ConvertInternal(value, targetType, parameter, True)
            Finally
                If oldC IsNot Nothing Then System.Threading.Thread.CurrentThread.CurrentCulture = oldC
            End Try
        End Function
    End Class
End Namespace
