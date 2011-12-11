Imports Tools.ExtensionsT
Imports System.Globalization

''' <summary>This converter can converts values (expecially <see cref="UInteger"/>) to <see cref="UnicodeCodePoint"/> and back</summary>
''' <remarks>
''' Supported values for forward conversion are: null, <see cref="UInteger"/>, <see cref="Integer"/>, <see cref="Short"/>, <see cref="UShort"/>, <see cref="Long"/>, <see cref="ULong"/>, <see cref="Char"/>, <see cref="String"/> (only first character is considered, first two in case of surrogate pair).
''' Supported values for backward conversion are: null, <see cref="UnicodeCodePoint"/>.
''' </remarks>
Public Class CodePointConverter
    Implements IValueConverter

    Public Sub New()
        Ucd = UnicodeCharacterDatabase.Default
    End Sub

    ''' <summary>Gets or sets instance of Unicode Character to use for code-point lookup</summary>
    ''' <value>Default value if this property is <see cref="UnicodeCharacterDatabase.[Default]"/>.</value>
    ''' <remarks>This property must not be null unless you pass custom parameter of type <see cref="UnicodeCharacterDatabase"/> to the <see cref="Convert"/> function.</remarks>
    Public Property Ucd As UnicodeCharacterDatabase


    ''' <summary>Converts a value.</summary>
    ''' <param name="value">The value produced by the binding source.</param>
    ''' <param name="targetType">The type of the binding target property. Must be either null or type <see cref="UnicodeCodePoint"/> can be <see cref="DynamicCast">dynamically casted</see> to.</param>
    ''' <param name="parameter">The converter parameter to use. Used only when it <see cref="UnicodeCharacterDatabase"/>. In such case <paramref name="parameter"/> is used instead of <see cref="Ucd"/>.</param>
    ''' <param name="culture">The culture to use in the converter. Ignored.</param>
    ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    ''' <exception cref="InvalidOperationException"><see cref="Ucd"/> is null and <paramref name="parameter"/> is not <see cref="UnicodeCharacterDatabase"/>.</exception>
    ''' <exception cref="NotSupportedException"><paramref name="value"/> is not ońe of supported types.</exception>
    ''' <exception cref="InvalidCastException"><paramref name="targetType"/> is not null and <see cref="UnicodeCodePoint"/> canot be casted to that type.</exception>
    ''' <remarks>Supported values for forward conversion are: null, <see cref="UInteger"/>, <see cref="Integer"/>, <see cref="Short"/>, <see cref="UShort"/>, <see cref="Long"/>, <see cref="ULong"/>, <see cref="Char"/>, <see cref="String"/> (only first character is considered, first two in case of surrogate pair).</remarks>
    Public Function Convert(value As Object, targetType As System.Type, parameter As Object, culture As CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        Dim ret As UnicodeCodePoint
        Dim getUcd = Function()
                         If TypeOf parameter Is UnicodeCharacterDatabase Then Return DirectCast(parameter, UnicodeCharacterDatabase)
                         If Ucd Is Nothing Then Throw New InvalidOperationException("The Ucd property must be initialized")
                         Return Ucd
                     End Function
        If value Is Nothing Then
            ret = Nothing
        ElseIf TypeOf value Is UInteger Then
            ret = getUcd().FindCodePoint(DirectCast(value, UInteger))
        ElseIf TypeOf value Is Integer Then
            ret = getUcd().FindCodePoint(DirectCast(value, Integer))
        ElseIf TypeOf value Is Short Then
            ret = getUcd().FindCodePoint(DirectCast(value, Short))
        ElseIf TypeOf value Is UShort Then
            ret = getUcd().FindCodePoint(DirectCast(value, UShort))
        ElseIf TypeOf value Is Long Then
            ret = getUcd().FindCodePoint(CUInt(DirectCast(value, ULong)))
        ElseIf TypeOf value Is Char Then
            ret = getUcd().FindCodePoint(DirectCast(value, Char))
        ElseIf TypeOf value Is String Then
            ret = getUcd().FindCodePoint(Char.ConvertToUtf32(value, 0))
        Else
            Throw New NotSupportedException("Convertion from type {0} is not supported".f(value.GetType.FullName))
        End If

        If targetType Is Nothing Then Return ret
        Return DynamicCast(ret, targetType)
    End Function

    ''' <summary>Converts a value. </summary>
    ''' <param name="value">The value produced by the binding source. Must either be null or be <see cref="DynamicCast(Of T)">dynamicly castable</see> to <see cref="UnicodeCharacterDatabase"/>.</param>
    ''' <param name="targetType">The type of the binding target property. Must be either null or <see cref="UInteger"/> must be castable to it.</param>
    ''' <param name="parameter">The converter parameter to use. Ignored.</param>
    ''' <param name="culture">The culture to use in the converter. Ignored.</param>
    ''' <exception cref="InvalidCastException">
    ''' <paramref name="value"/> cannot be <see cref="DynamicCast(Of T)">dynamically casted</see> to <see cref="UnicodeCodePoint"/> -or- 
    ''' <paramref name="targetType"/> is not null and return value of type <see cref="UInteger"/> cannot be <see cref="DynamicCast">dynamically casted</see> to that type.</exception>
    ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    Public Function ConvertBack(value As Object, targetType As System.Type, parameter As Object, culture As CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        Dim ret As Object
        If value Is Nothing Then
            ret = Nothing
        Else
            ret = DynamicCast(Of UnicodeCodePoint)(value).CodePoint
        End If

        If targetType Is Nothing Then Return ret
        Return DynamicCast(ret, targetType)
    End Function
End Class
