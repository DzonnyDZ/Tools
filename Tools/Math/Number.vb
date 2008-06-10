Imports Tools.DataStructuresT.GenericT, Tools.ExtensionsT
'ASAP:Comment
'TODO:Implement
#If Config <= Nightly Then
Public Structure Number
    Implements IConvertible, IFormattable
    Private ReadOnly _Value As IConvertible
    <CLSCompliant(False)> _
    Public ReadOnly Property Value() As IConvertible
        Get
            If _Value Is Nothing Then Return 0
            Return _Value
        End Get
    End Property
#Region "CTors"
    Public Sub New(ByVal v As Byte)
        _Value = v
    End Sub
    <CLSCompliant(False)> _
    Public Sub New(ByVal v As SByte)
        _Value = v
    End Sub

    Public Sub New(ByVal v As Short)
        _Value = v
    End Sub
    <CLSCompliant(False)> _
    Public Sub New(ByVal v As UShort)
        _Value = v
    End Sub
    Public Sub New(ByVal v As Integer)
        _Value = v
    End Sub
    <CLSCompliant(False)> _
    Public Sub New(ByVal v As UInteger)
        _Value = v
    End Sub
    Public Sub New(ByVal v As Long)
        _Value = v
    End Sub
    <CLSCompliant(False)> _
    Public Sub New(ByVal v As ULong)
        _Value = v
    End Sub
    Public Sub New(ByVal v As Single)
        _Value = v
    End Sub
    Public Sub New(ByVal v As Double)

    End Sub
    Public Sub New(ByVal v As Decimal)
        _Value = v
    End Sub
    Public Sub New(ByVal v As Char)
        _Value = v
    End Sub
#End Region
#Region "CTypes"
#Region "Box"
    Public Shared Widening Operator CType(ByVal a As Byte) As Number
        Return New Number(a)
    End Operator
    <CLSCompliant(False)> _
    Public Shared Widening Operator CType(ByVal a As SByte) As Number
        Return New Number(a)
    End Operator
    Public Shared Widening Operator CType(ByVal a As Short) As Number
        Return New Number(a)
    End Operator
    <CLSCompliant(False)> _
    Public Shared Widening Operator CType(ByVal a As UShort) As Number
        Return New Number(a)
    End Operator
    Public Shared Widening Operator CType(ByVal a As Integer) As Number
        Return New Number(a)
    End Operator
    <CLSCompliant(False)> _
    Public Shared Widening Operator CType(ByVal a As UInteger) As Number
        Return New Number(a)
    End Operator
    Public Shared Widening Operator CType(ByVal a As Long) As Number
        Return New Number(a)
    End Operator
    <CLSCompliant(False)> _
    Public Shared Widening Operator CType(ByVal a As ULong) As Number
        Return New Number(a)
    End Operator
    Public Shared Widening Operator CType(ByVal a As Single) As Number
        Return New Number(a)
    End Operator
    Public Shared Widening Operator CType(ByVal a As Double) As Number
        Return New Number(a)
    End Operator
    Public Shared Widening Operator CType(ByVal a As Decimal) As Number
        Return New Number(a)
    End Operator
    Public Shared Widening Operator CType(ByVal a As Char) As Number
        Return New Number(a)
    End Operator
#End Region
#Region "Unbox"
    Public Shared Narrowing Operator CType(ByVal a As Number) As Byte
        Return a.Value.ToByte(Globalization.CultureInfo.CurrentCulture)
    End Operator
    <CLSCompliant(False)> _
    Public Shared Narrowing Operator CType(ByVal a As Number) As SByte
        Return a.Value.ToSByte(Globalization.CultureInfo.CurrentCulture)
    End Operator
    Public Shared Narrowing Operator CType(ByVal a As Number) As Short
        Return a.Value.ToInt16(Globalization.CultureInfo.CurrentCulture)
    End Operator
    <CLSCompliant(False)> _
    Public Shared Narrowing Operator CType(ByVal a As Number) As UShort
        Return a.Value.ToUInt16(Globalization.CultureInfo.CurrentCulture)
    End Operator
    Public Shared Narrowing Operator CType(ByVal a As Number) As Integer
        Return a.Value.ToInt32(Globalization.CultureInfo.CurrentCulture)
    End Operator
    <CLSCompliant(False)> _
    Public Shared Narrowing Operator CType(ByVal a As Number) As UInteger
        Return a.Value.ToUInt32(Globalization.CultureInfo.CurrentCulture)
    End Operator
    Public Shared Narrowing Operator CType(ByVal a As Number) As Long
        Return a.Value.ToInt64(Globalization.CultureInfo.CurrentCulture)
    End Operator
    <CLSCompliant(False)> _
    Public Shared Narrowing Operator CType(ByVal a As Number) As ULong
        Return a.Value.ToUInt64(Globalization.CultureInfo.CurrentCulture)
    End Operator
    Public Shared Widening Operator CType(ByVal a As Number) As Single
        Return a.Value.ToSingle(Globalization.CultureInfo.CurrentCulture)
    End Operator
    Public Shared Widening Operator CType(ByVal a As Number) As Double
        Return a.Value.ToDouble(Globalization.CultureInfo.CurrentCulture)
    End Operator
    Public Shared Narrowing Operator CType(ByVal a As Number) As Char
        Return ChrW(a)
    End Operator
    Public Shared Narrowing Operator CType(ByVal a As Number) As Decimal
        Return a.Value.ToDecimal(Globalization.CultureInfo.CurrentCulture)
    End Operator
    Public Shared Widening Operator CType(ByVal a As Number) As String
        Return a.ToString
    End Operator
#End Region
#End Region
#Region "IConvertible"
    Private Function GetTypeCode() As System.TypeCode Implements System.IConvertible.GetTypeCode
        Return Value.GetTypeCode
    End Function

    Private Function ToBoolean(ByVal provider As System.IFormatProvider) As Boolean Implements System.IConvertible.ToBoolean
        Return Value.ToBoolean(provider)
    End Function

    Private Function ToByte(ByVal provider As System.IFormatProvider) As Byte Implements System.IConvertible.ToByte
        Return Value.ToByte(provider)
    End Function

    Private Function ToDateTime(ByVal provider As System.IFormatProvider) As Date Implements System.IConvertible.ToDateTime
        Return Value.ToDateTime(provider)
    End Function

    Private Function ToDecimal(ByVal provider As System.IFormatProvider) As Decimal Implements System.IConvertible.ToDecimal
        Return Value.ToDecimal(provider)
    End Function

    Private Function ToDouble(ByVal provider As System.IFormatProvider) As Double Implements System.IConvertible.ToDouble
        Return Value.ToDouble(provider)
    End Function

    Private Function ToChar(ByVal provider As System.IFormatProvider) As Char Implements System.IConvertible.ToChar
        Return Value.ToChar(provider)
    End Function

    Private Function ToInt16(ByVal provider As System.IFormatProvider) As Short Implements System.IConvertible.ToInt16
        Return Value.ToInt16(provider)
    End Function

    Private Function ToInt32(ByVal provider As System.IFormatProvider) As Integer Implements System.IConvertible.ToInt32
        Return Value.ToInt32(provider)
    End Function

    Private Function ToInt64(ByVal provider As System.IFormatProvider) As Long Implements System.IConvertible.ToInt64
        Return Value.ToInt64(provider)
    End Function

    Private Function ToSByte(ByVal provider As System.IFormatProvider) As SByte Implements System.IConvertible.ToSByte
        Return Value.ToSByte(provider)
    End Function

    Private Function ToSingle(ByVal provider As System.IFormatProvider) As Single Implements System.IConvertible.ToSingle
        Return Value.ToSingle(provider)
    End Function

    Private Overloads Function ToString(ByVal provider As System.IFormatProvider) As String Implements System.IConvertible.ToString
        Return Value.ToString(provider)
    End Function

    Private Function ToType(ByVal conversionType As System.Type, ByVal provider As System.IFormatProvider) As Object Implements System.IConvertible.ToType
        Return Value.ToType(conversionType, provider)
    End Function

    Private Function ToUInt16(ByVal provider As System.IFormatProvider) As UShort Implements System.IConvertible.ToUInt16
        Return Value.ToUInt16(provider)
    End Function

    Private Function ToUInt32(ByVal provider As System.IFormatProvider) As UInteger Implements System.IConvertible.ToUInt32
        Return Value.ToUInt32(provider)
    End Function

    Private Function ToUInt64(ByVal provider As System.IFormatProvider) As ULong Implements System.IConvertible.ToUInt64
        Return Value.ToUInt64(provider)
    End Function
#End Region
    Public Overrides Function ToString() As String
        Return DirectCast(Value, Object).ToString
    End Function

    Public Overloads Function ToString(ByVal format As String, ByVal formatProvider As System.IFormatProvider) As String Implements System.IFormattable.ToString
        Return DirectCast(Value, IFormattable).ToString(format, formatProvider)
    End Function
    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Return Value.Equals(obj)
    End Function
    Private Shared Function GetCommons(ByVal v1 As TypeCode, ByVal v2 As TypeCode) As TypeCode
        If Not v1.IsNumber AndAlso v1 <> TypeCode.Char Then Throw New ArgumentException("v1", "Type code must represent numeric type or System.Char")
        If Not v2.IsNumber AndAlso v2 <> TypeCode.Char Then Throw New ArgumentException("v2", "Type code must represent numeric type or System.Char")
        If v1 = v2 Then Return v1
        If v1 = TypeCode.Char Then v1 = TypeCode.UInt16
        If v2 = TypeCode.Char Then v2 = TypeCode.UInt16
        If v1 = v2 Then Return v1
        If v1.IsFloating AndAlso v2.IsFloating Then
            Select Case v1
                Case TypeCode.Single : Return v2
                Case TypeCode.Double : Return TypeCode.Double
                Case TypeCode.Decimal : Return If(v2 = TypeCode.Single, TypeCode.Decimal, TypeCode.Double)
            End Select
        ElseIf v1.IsFloating Then : Return v1
        ElseIf v2.IsFloating Then : Return v2
        ElseIf v1.IsUnsigned = v2.IsUnsigned Then : Return If(v1.Compare(v2) > 0, v1, v2)
        ElseIf v1 = TypeCode.Int64 OrElse v1 = TypeCode.UInt64 OrElse v2 = TypeCode.UInt64 OrElse v2 = TypeCode.Int64 Then : Return TypeCode.Single
        ElseIf v1.ByteSize = v2.ByteSize Then : Return v1.GetSigned.GetBigger
        ElseIf v1.CompareTo(v2) > 0 Then : Return v1.GetSigned
        Else : Return v2.GetSigned
        End If
    End Function
   
End Structure
#End If