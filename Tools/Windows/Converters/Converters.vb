Imports System.Windows.Data
Imports System.Globalization

Namespace WindowsT.ConvertersT
    Friend Class NotBooleanValueConverter
        Implements IValueConverter

#Region "IValueConverter Members"

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If TypeOf value Is Boolean AndAlso targetType.Equals(GetType(Boolean)) Then
                Return Not CBool(value)
            End If
            Throw New ArgumentException("Unsupported conversion") 'LOcalize:Exception
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If TypeOf value Is Boolean AndAlso targetType.Equals(GetType(Boolean)) Then
                Return Not CBool(value)
            End If
            Throw New ArgumentException("Unsupported conversion") 'Localize:Exception
        End Function

#End Region
    End Class
    Friend Class HalfConverter
        Implements IValueConverter

#Region "IValueConverter Members"

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return Convert(value, targetType, CSng(0.5))

        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return Convert(value, targetType, CSng(2))
        End Function

#End Region
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