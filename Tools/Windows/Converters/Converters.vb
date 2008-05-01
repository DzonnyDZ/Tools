﻿Imports System.Windows.Data
Imports System.Globalization

Namespace WindowsT.ConvertersT
    ''' <summary>Implements <see cref="IValueConverter"/> which negates <see cref="Boolean"/> value</summary>
    <Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(NotBooleanValueConverter)), FirstVersion(2008, 5, 1)> _
    Public Class NotBooleanValueConverter
        Inherits StronglyTypedConverter(Of Boolean, Boolean)
        ''' <summary>Converts a value - makes boolean negation of it.</summary>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="parameter">The converter parameter to use. Ignored.</param>
        ''' <param name="culture">The culture to use in the converter. Ignored.</param>
        ''' <returns>Boolean negation of <paramref name="value"/></returns>
        Public Overrides Function Convert(ByVal value As Boolean, ByVal parameter As Object, ByVal culture As CultureInfo) As Boolean
            Return Not value
        End Function
        ''' <summary>Converts a value - makes boolean negation of it.</summary>
        ''' <param name="value">The value that is produced by the binding target.</param>
        ''' <param name="parameter">The converter parameter to use. Ignored.</param>
        ''' <param name="culture">The culture to use in the converter. Ignored.</param>
        ''' <returns>Boolean negation of <paramref name="value"/>.</returns>
        Public Overrides Function ConvertBack(ByVal value As Boolean, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Boolean
            Return Not value
        End Function
    End Class

    ''' <summary>Implements <see cref="IValueConverter"/> for converting numeric values to halfs of them</summary>
    <Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
    <Version(1, 0, GetType(HalfConverter)), FirstVersion(2008, 5, 1)> _
    Friend Class HalfConverter
        Implements IValueConverter
        ''' <summary>Converts a value. </summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <exception cref="ArgumentException"><paramref name="value"/> or <paramref name="targetType"/> is not supported.
        ''' Supported types are <see cref="Byte"/>, <see cref="SByte"/>, <see cref="Short"/>, <see cref="UShort"/>, <see cref="Integer"/>, <see cref="UInteger"/>, <see cref="Long"/>, <see cref="ULong"/>, <see cref="Single"/>, <see cref="Double"/> and <see cref="Decimal"/>
        ''' </exception>
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return Convert(value, targetType, CSng(0.5))

        End Function

        ''' <summary>Converts a value. </summary>
        ''' <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        ''' <param name="value">The value produced by the binding source.</param>
        ''' <param name="targetType">The type of the binding target property.</param>
        ''' <param name="parameter">The converter parameter to use.</param>
        ''' <param name="culture">The culture to use in the converter.</param>
        ''' <exception cref="ArgumentException"><paramref name="value"/> or <paramref name="targetType"/> is not supported.
        ''' Supported types are <see cref="Byte"/>, <see cref="SByte"/>, <see cref="Short"/>, <see cref="UShort"/>, <see cref="Integer"/>, <see cref="UInteger"/>, <see cref="Long"/>, <see cref="ULong"/>, <see cref="Single"/>, <see cref="Double"/> and <see cref="Decimal"/>
        ''' </exception>
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Convert(value, targetType, CSng(2))
        End Function


        ''' <summary>Performs a conversion</summary>
        ''' <param name="value">Value to be converted</param>
        ''' <param name="targetType">Type of return value</param>
        ''' <param name="param">Multiplication constant</param>
        ''' <returns><paramref name="value"/> * <paramref name="param"/> in type <paramref name="targetType"/></returns>
        ''' <exception cref="ArgumentException"><paramref name="value"/>is of unsupported type or <paramref name="targetType"/> is unsupported</exception>
        Private Shared Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal param As Single) As Object
            Dim multiplied As Double
            If TypeOf value Is SByte Then
                multiplied = CDbl((CDbl(CSByte(value)) * param))
            ElseIf TypeOf value Is Byte Then
                multiplied = CDbl((CDbl(CByte(value)) * param))
            ElseIf TypeOf value Is Short Then
                multiplied = CDbl((CDbl(CShort(value)) * param))
            ElseIf TypeOf value Is UShort Then
                multiplied = CDbl((CDbl(CUShort(value)) * param))
            ElseIf TypeOf value Is Integer Then
                multiplied = CDbl((CDbl(CInt(value)) * param))
            ElseIf TypeOf value Is UInteger Then
                multiplied = CDbl((CDbl(CInt(value)) * param))
            ElseIf TypeOf value Is Long Then
                multiplied = CDbl((CDbl(CLng(value)) * param))
            ElseIf TypeOf value Is ULong Then
                multiplied = CDbl((CDbl(CLng(value)) * param))
            ElseIf TypeOf value Is Single Then
                multiplied = CDbl((CDbl(CSng(value)) * param))
            ElseIf TypeOf value Is Double Then
                multiplied = CDbl((CDbl(CDbl(value)) * param))
            ElseIf TypeOf value Is Decimal Then
                multiplied = CDbl((CDbl(CDec(value)) * param))
            Else
                Throw New ArgumentException("Unsupported source type", "value") 'Localize:Exception
            End If
            If targetType.Equals(GetType(SByte)) Then
                Return CSByte(multiplied)
            ElseIf targetType.Equals(GetType(Byte)) Then
                Return CByte(multiplied)
            ElseIf targetType.Equals(GetType(Short)) Then
                Return CShort(multiplied)
            ElseIf targetType.Equals(GetType(UShort)) Then
                Return CUShort(multiplied)
            ElseIf targetType.Equals(GetType(Integer)) Then
                Return CInt(multiplied)
            ElseIf targetType.Equals(GetType(UInteger)) Then
                Return CInt(multiplied)
            ElseIf targetType.Equals(GetType(Long)) Then
                Return CLng(multiplied)
            ElseIf targetType.Equals(GetType(ULong)) Then
                Return CLng(multiplied)
            ElseIf targetType.Equals(GetType(Single)) Then
                Return CSng(multiplied)
            ElseIf targetType.Equals(GetType(Double)) Then
                Return CDbl(multiplied)
            ElseIf targetType.Equals(GetType(Decimal)) Then
                Return CDec(multiplied)
            Else
                Throw New ArgumentException("Unsupported target type", "targetType") 'Localize:Exception
            End If
        End Function
    End Class
End Namespace