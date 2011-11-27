Imports System.Text

Namespace TextT.UnicodeT
    ''' <summary>An alternative implementation of <see cref="String"/> that internally uses UTF-32 encoding, instead of .NET-standard UTF-16</summary>
    ''' <remarks>
    ''' Advantage of this type of string is that it can nativelly handle all Unicode code points form all planes without use of surrogate pairs.
    ''' Disadvantages are that it is not a native type and that it consumes twice as much memory for most of code-points that are usually used.
    ''' </remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public NotInheritable Class Utf32String
        Implements ICloneable, ICloneable(Of Utf32String), IComparable, IComparable(Of Utf32String), IComparable(Of String), IConvertible, IEnumerable(Of Integer), IEquatable(Of Utf32String), IEquatable(Of String)

        Private storage As Integer()
        Public Sub New(str As String)
            If str Is Nothing Then Throw New ArgumentNullException("str")
            Dim storage As New List(Of Integer)
            For i = 0 To str.Length - 1
                Dim cp = Char.ConvertToUtf32(str, i)
                If Char.IsHighSurrogate(str(i)) AndAlso str.Length >= i AndAlso Char.IsLowSurrogate(str(i + 1)) Then i += 1
            Next
            Me.storage = storage.ToArray
        End Sub
        Public Sub New(wchars As Int32())
            If wchars Is Nothing Then Throw New ArgumentNullException
            Me.storage = wchars.Clone
        End Sub
        Private Sub New()
        End Sub

        Public Overrides Function ToString() As String
            Dim b As New StringBuilder
            For Each cp In storage
                b.Append(Char.ConvertFromUtf32(cp))
            Next
            Return b.ToString
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return ToString.GetHashCode
        End Function


        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of Integer) Implements System.Collections.Generic.IEnumerable(Of Integer).GetEnumerator
            Return storage.GetEnumerator
        End Function

        Private Function IEnumerable_GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

        Private Function ICloneable_Clone() As Object Implements System.ICloneable.Clone
            Return Clone()
        End Function

        Private Function CompareTo(obj As Object) As Integer Implements System.IComparable.CompareTo
            If TypeOf obj Is Utf32String Then Return CompareTo(DirectCast(obj, Utf32String))
            If TypeOf obj Is String Then Return CompareTo(DirectCast(obj, String))
            If obj Is Nothing Then Throw New ArgumentNullException("obj")
            Throw New ArgumentException("Unsupported type for comparison: " & obj.GetType.FullName, "obj")
        End Function

        Public Function CompareTo(other As String) As Integer Implements System.IComparable(Of String).CompareTo
            Return ToString.CompareTo(other)
        End Function

        Public Function CompareTo(other As Utf32String) As Integer Implements System.IComparable(Of Utf32String).CompareTo
            Return ToString.CompareTo(other.ToString)
        End Function

        Private Function GetTypeCode() As System.TypeCode Implements System.IConvertible.GetTypeCode
            Return TypeCode.String
        End Function

        Private Function ToBoolean(provider As System.IFormatProvider) As Boolean Implements System.IConvertible.ToBoolean
            Return DirectCast(ToString(), IConvertible).ToBoolean(provider)
        End Function

        Private Function ToByte(provider As System.IFormatProvider) As Byte Implements System.IConvertible.ToByte
            Return DirectCast(ToString(), IConvertible).ToByte(provider)
        End Function

        Private Function ToDateTime(provider As System.IFormatProvider) As Date Implements System.IConvertible.ToDateTime
            Return DirectCast(ToString(), IConvertible).ToDateTime(provider)
        End Function

        Private Function ToDecimal(provider As System.IFormatProvider) As Decimal Implements System.IConvertible.ToDecimal
            Return DirectCast(ToString(), IConvertible).ToDecimal(provider)
        End Function

        Private Function ToDouble(provider As System.IFormatProvider) As Double Implements System.IConvertible.ToDouble
            Return DirectCast(ToString(), IConvertible).ToDouble(provider)
        End Function

        Private Function ToChar(provider As System.IFormatProvider) As Char Implements System.IConvertible.ToChar
            Return DirectCast(ToString(), IConvertible).ToChar(provider)
        End Function

        Private Function ToInt16(provider As System.IFormatProvider) As Short Implements System.IConvertible.ToInt16
            Return DirectCast(ToString(), IConvertible).ToInt16(provider)
        End Function

        Private Function ToInt32(provider As System.IFormatProvider) As Integer Implements System.IConvertible.ToInt32
            Return DirectCast(ToString(), IConvertible).ToInt32(provider)
        End Function

        Private Function ToInt64(provider As System.IFormatProvider) As Long Implements System.IConvertible.ToInt64
            Return DirectCast(ToString(), IConvertible).ToInt64(provider)
        End Function

        Private Function ToSByte(provider As System.IFormatProvider) As SByte Implements System.IConvertible.ToSByte
            Return DirectCast(ToString(), IConvertible).ToSByte(provider)
        End Function

        Private Function ToSingle(provider As System.IFormatProvider) As Single Implements System.IConvertible.ToSingle
            Return DirectCast(ToString(), IConvertible).ToSingle(provider)
        End Function

        Private Overloads Function ToString(provider As System.IFormatProvider) As String Implements System.IConvertible.ToString
            Return DirectCast(ToString(), IConvertible).ToString(provider)
        End Function

        Private Function ToType(conversionType As System.Type, provider As System.IFormatProvider) As Object Implements System.IConvertible.ToType
            Return DirectCast(ToString(), IConvertible).ToType(conversionType, provider)
        End Function

        Private Function ToUInt16(provider As System.IFormatProvider) As UShort Implements System.IConvertible.ToUInt16
            Return DirectCast(ToString(), IConvertible).ToUInt16(provider)
        End Function

        Private Function ToUInt32(provider As System.IFormatProvider) As UInteger Implements System.IConvertible.ToUInt32
            Return DirectCast(ToString(), IConvertible).ToUInt32(provider)
        End Function

        Private Function ToUInt64(provider As System.IFormatProvider) As ULong Implements System.IConvertible.ToUInt64
            Return DirectCast(ToString(), IConvertible).ToUInt64(provider)
        End Function

        Public Overloads Function Equals(other As String) As Boolean Implements System.IEquatable(Of String).Equals
            Return ToString.Equals(other)
        End Function

        Public Overloads Function Equals(other As Utf32String) As Boolean Implements System.IEquatable(Of Utf32String).Equals
            If other Is Nothing Then Return False
            If Not other.LongLength = Me.LongLength Then Return False
            For i = 0L To Me.LongLength - 1
                If DirectCast(storage.GetValue(i), Int32) <> DirectCast(other.storage.GetValue(i), Int32) Then Return False
            Next
            Return True
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is Utf32String Then Return Equals(DirectCast(obj, Utf32String))
            If TypeOf obj Is String Then Return Equals(DirectCast(obj, String))
            Return MyBase.Equals(obj)
        End Function

        Public Function Clone() As Utf32String Implements ICloneable(Of Utf32String).Clone
            Return New Utf32String(DirectCast(storage.Clone, Int32()))
        End Function

        Public ReadOnly Property Lenght As Integer
            Get
                Return storage.Length
            End Get
        End Property

        Public ReadOnly Property LongLength As Long
            Get
                Return storage.LongLength
            End Get
        End Property

        Public Shared Operator =(a As Utf32String, b As Utf32String) As Boolean
            If (a Is Nothing) <> (b Is Nothing) Then Return False
            If a Is Nothing Then Return True
            Return a.Equals(b)
        End Operator
        Public Shared Operator =(a As Utf32String, b As String) As Boolean
            If (a Is Nothing) <> (b Is Nothing) Then Return False
            If a Is Nothing Then Return True
            Return a.ToString.Equals(b)
        End Operator
        Public Shared Operator =(a As String, b As Utf32String) As Boolean
            If (a Is Nothing) <> (b Is Nothing) Then Return False
            If a Is Nothing Then Return True
            Return a.Equals(b.ToString)
        End Operator

        Public Shared Operator <>(a As Utf32String, b As Utf32String) As Boolean
            Return Not (a = b)
        End Operator
        Public Shared Operator <>(a As Utf32String, b As String) As Boolean
            Return Not (a=b)
        End Operator
        Public Shared Operator <>(a As String, b As Utf32String) As Boolean
            Return Not (a=b)
        End Operator

        Public Shared Operator &(a As Utf32String, b As Utf32String) As Utf32String
            If a Is Nothing AndAlso b Is Nothing Then Return Nothing
            Dim ret(0L To If(a Is Nothing, 0L, a.LongLength) + If(b Is Nothing, 0L, b.LongLength) - 1) As Integer
            If a IsNot Nothing Then Array.Copy(a.storage, ret, a.LongLength)
            If b IsNot Nothing Then Array.Copy(b.storage, 0L, ret, If(a Is Nothing, 0L, a.LongLength), b.LongLength)
            Return New Utf32String() With {.storage = ret}
        End Operator
        Public Shared Operator +(a As Utf32String, b As Utf32String) As Utf32String
            Return a & b
        End Operator
        Public Shared Operator &(a As String, b As Utf32String) As Utf32String
            Return New Utf32String(a) & b
        End Operator
        Public Shared Operator +(a As String, b As Utf32String) As Utf32String
            Return a & b
        End Operator
        Public Shared Operator &(a As Utf32String, b As String) As Utf32String
            Return a & New Utf32String(b)
        End Operator
        Public Shared Operator +(a As Utf32String, b As String) As Utf32String
            Return a & b
        End Operator

        Public Shared Widening Operator CType(a As Utf32String) As String
            Return a.ToString
        End Operator

        Public Shared Widening Operator CType(a As String) As Utf32String
            Return New Utf32String(a)
        End Operator

        Default Public ReadOnly Property Chars(index As Integer) As Integer
            Get
                Return storage(index)
            End Get
        End Property
        Default Public ReadOnly Property Chars(index As Long) As Integer
            Get
                Return storage(index)
            End Get
        End Property


    End Class
End Namespace
