Imports System.Text

Namespace TextT
    'TODO: Parity with System.String
    ''' <summary>Represents UTF32 string</summary>
    ''' <remarks>Advantage of this class is that it does not split non-BMP (i.e. non-UTF16) characters top low and high surrogates</remarks>
    ''' <version version="1.5.5">This class is new in version 1.5.5</version>
    Public NotInheritable Class Utf32String
        Implements ICloneable(Of Utf32String), IComparable, IComparable(Of Utf32String), IConvertible, IEnumerable(Of Integer), IEquatable(Of Utf32String)
        Private ReadOnly _characters As Integer()

#Region "Shared"
        ''' <summary>Represents an empty instance of this string</summary>
        Public Shared ReadOnly Empty As Utf32String
        ''' <summary>Type initializer - initializes the <see cref="Utf32String"/> class</summary>
        Shared Sub New()
            Empty = New Utf32String
        End Sub
#End Region

#Region "CTors"
        ''' <summary>CTor - creates a new instance empty of the <see cref="Utf32String"/></summary>
        Private Sub New()
            ReDim _characters(0 To 0)
        End Sub

        ''' <summary>Creates a new instance of the <see cref="Utf32String"/> class from <see cref="String"/></summary>
        ''' <param name="str$">String to initializes new UTF 32 string from</param>
        ''' <exception cref="ArgumentNullException"><paramref name="str"/> is null</exception>
        Public Sub New(str$)
            If str Is Nothing Then Throw New ArgumentNullException(NameOf(str))
            Dim lst As New List(Of Integer)
            For i = 0 To str.Length - 1
                lst.Add(Char.ConvertToUtf32(str, i))
                If Char.IsSurrogatePair(str, i) Then i += 1
            Next
            _characters = lst.ToArray
        End Sub

        ''' <summary>Creates a new instance of the <see cref="Utf32String"/> class from array of UTF 32 characters</summary>
        ''' <param name="characters">Array of UTF32 characters</param>
        ''' <exception cref="ArgumentNullException"><paramref name="characters"/> is null</exception>
        Public Sub New(characters As Integer())
            If characters Is Nothing Then Throw New ArgumentNullException(NameOf(characters))
            ReDim Me._characters(0 To characters.Length - 1)
            Array.Copy(characters, Me._characters, characters.Length)
        End Sub
#End Region

#Region "Override"
        ''' <summary>Gets <see cref="String"/> representation of this <see cref="Utf32String"/></summary>
        ''' <returns><see cref="String"/> that represents current instance of <see cref="Utf32String"/></returns>
        Public Overrides Function ToString() As String
            Dim b As New StringBuilder
            For Each ch In _characters
                b.Append(Char.ConvertFromUtf32(ch))
            Next
            Return b.ToString
        End Function

        ''' <summary>Determines whether the specified object is equal to the current instance.</summary>
        ''' <param name="obj">The object to compare with the current instance.</param>
        ''' <returns>True if the specified object is equal to the current instance; otherwise, false.</returns>
        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is Utf32String Then Return Me = DirectCast(obj, Utf32String)
            Return MyBase.Equals(obj)
        End Function
#End Region

#Region "Properties"
        ''' <summary>Gets length of this <see cref="Utf32String"/> in number of characters (Unicode code-points)</summary>
        ''' <returns>Number of Unicode (UTF-32) characters in this instance</returns>
        Public ReadOnly Property Length As Integer
            Get
                Return _characters.Length
            End Get
        End Property

        ''' <summary>Gets character at given index</summary>
        ''' <param name="index">Index of character to get</param>
        ''' <returns>Character at given index</returns>
        ''' <exception cref="IndexOutOfRangeException"><paramref name="index"/> is less than zero or greater than or equal to <see cref="Length"/></exception>
        Default Public ReadOnly Property Characters(index%) As Integer
            Get
                Return _characters(index)
            End Get
        End Property

#End Region

#Region "Functions"
        ''' <summary>Gets Unicode code points this string is composed from</summary>
        ''' <returns>UTF32 code points</returns>
        Public Function ToUtf32CharArray() As Integer()
            Return _characters.Clone()
        End Function
#End Region

#Region "Operators"
        ''' <summary>Converts <see cref="String"/> to <see cref="Utf32String"/></summary>
        ''' <param name="str">A <see cref="String"/></param>
        ''' <returns>A new instance of <see cref="Utf32String"/> initialized from <paramref name="str"/>; null if <paramref name="str"/> is null.</returns>
        Public Shared Narrowing Operator CType(str$) As Utf32String
            Return If(str Is Nothing, Nothing, New Utf32String(str))
        End Operator

        ''' <summary>Converts <see cref="Utf32String"/> to <see cref="String"/></summary>
        ''' <param name="str32">A <see cref="Utf32String"/></param>
        ''' <returns><paramref name="str32"/> converted to <see cref="String"/>; null if <paramref name="str32"/> is null.</returns>
        Public Shared Narrowing Operator CType(str32 As Utf32String) As String
            Return str32?.ToString
        End Operator

        ''' <summary>Compares two <see cref="Utf32String"/> instances</summary>
        ''' <param name="a">A <see cref="Utf32String"/> to compare</param>
        ''' <param name="b">A <see cref="Utf32String"/> to compare</param>
        ''' <returns>True if both instances are either null or equal; false if one instance is null an the other is not null or they are not equal</returns>
        Public Shared Operator =(a As Utf32String, b As Utf32String) As Boolean
            If a Is Nothing OrElse b Is Nothing Then Return (a Is Nothing) = (b Is Nothing)
            If a.Length <> b.Length Then Return False
            For i = 0 To a.Length - 1
                If a(i) <> b(i) Then Return False
            Next
            Return True
        End Operator

        ''' <summary>Compares two <see cref="Utf32String"/> instances for inequality</summary>
        ''' <param name="a">A <see cref="Utf32String"/> to compare</param>
        ''' <param name="b">A <see cref="Utf32String"/> to compare</param>
        ''' <returns>False if both instances are either null or equal; true if one instance is null an the other is not null or they are not equal</returns>
        Public Shared Operator <>(a As Utf32String, b As Utf32String) As Boolean
            Return Not a = b
        End Operator
#End Region

#Region "Interfaces implementation"
        ''' <summary>Returns current instance</summary>
        ''' <returns>Current instance</returns>
        ''' <remarks>No need to clone immutable object</remarks>
        Public Function Clone() As Utf32String Implements ICloneable(Of Utf32String).Clone
            Return Me
        End Function

        Private Function ICloneable_Clone() As Object Implements ICloneable.Clone
            Return Clone()
        End Function

        ''' <summary>Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.</summary>
        ''' <param name="obj">An object to compare with this instance.</param>
        ''' <returns>
        ''' A value that indicates the relative order of the objects being compared.
        ''' The return value has these meanings: Value Meaning Less than zero This instance precedes obj in the sort order.
        ''' Zero This instance occurs in the same position in the sort order as obj.
        ''' Greater than zero This instance follows obj in the sort order.
        ''' </returns>
        ''' <exception cref="ArgumentException">obj is not the same type as this instance.</exception>
        Private Function CompareTo(obj As Object) As Integer Implements IComparable.CompareTo
            If TypeOf obj Isnot Utf32String Then Throw New ArgumentException ($"Value must be of type {GetType(Utf32String).Name}", NameOf(obj))
            Return Me.ToString().CompareTo(obj)
        End Function

        ''' <summary>Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.</summary>
        ''' <param name="other">An object to compare with this instance.</param>
        ''' <returns>
        ''' A value that indicates the relative order of the objects being compared.
        ''' The return value has these meanings: Value Meaning Less than zero This instance precedes other in the sort order.
        ''' Zero This instance occurs in the same position in the sort order as other.
        ''' Greater than zero This instance follows other in the sort order.
        ''' </returns>
        Public Function CompareTo(other As Utf32String) As Integer Implements IComparable(Of Utf32String).CompareTo
            If other Is Nothing Then Return 1
            Return Me.ToString.CompareTo(other.ToString)
        End Function

#Region "IConvertible"
        Private Function GetTypeCode() As TypeCode Implements IConvertible.GetTypeCode
            Return TypeCode.Object
        End Function

        Private Function ToBoolean(provider As IFormatProvider) As Boolean Implements IConvertible.ToBoolean
            Return DirectCast(ToString(), IConvertible).ToBoolean(provider)
        End Function

        Private Function ToChar(provider As IFormatProvider) As Char Implements IConvertible.ToChar
            Return DirectCast(ToString(), IConvertible).ToChar(provider)
        End Function

        Private Function ToSByte(provider As IFormatProvider) As SByte Implements IConvertible.ToSByte
            Return DirectCast(ToString(), IConvertible).ToSByte(provider)
        End Function

        Private Function ToByte(provider As IFormatProvider) As Byte Implements IConvertible.ToByte
            Return DirectCast(ToString(), IConvertible).ToByte(provider)
        End Function

        Private Function ToInt16(provider As IFormatProvider) As Short Implements IConvertible.ToInt16
            Return DirectCast(ToString(), IConvertible).ToInt16(provider)
        End Function

        Private Function ToUInt16(provider As IFormatProvider) As UShort Implements IConvertible.ToUInt16
            Return DirectCast(ToString(), IConvertible).ToUInt16(provider)
        End Function

        Private Function ToInt32(provider As IFormatProvider) As Integer Implements IConvertible.ToInt32
            Return DirectCast(ToString(), IConvertible).ToInt32(provider)
        End Function

        Private Function ToUInt32(provider As IFormatProvider) As UInteger Implements IConvertible.ToUInt32
            Return DirectCast(ToString(), IConvertible).ToUInt32(provider)
        End Function

        Private Function ToInt64(provider As IFormatProvider) As Long Implements IConvertible.ToInt64
            Return DirectCast(ToString(), IConvertible).ToInt64(provider)
        End Function

        Private Function ToUInt64(provider As IFormatProvider) As ULong Implements IConvertible.ToUInt64
            Return DirectCast(ToString(), IConvertible).ToUInt64(provider)
        End Function

        Private Function ToSingle(provider As IFormatProvider) As Single Implements IConvertible.ToSingle
            Return DirectCast(ToString(), IConvertible).ToSingle(provider)
        End Function

        Private Function ToDouble(provider As IFormatProvider) As Double Implements IConvertible.ToDouble
            Return DirectCast(ToString(), IConvertible).ToDouble(provider)
        End Function

        Private Function ToDecimal(provider As IFormatProvider) As Decimal Implements IConvertible.ToDecimal
            Return DirectCast(ToString(), IConvertible).ToDecimal(provider)
        End Function

        Private Function ToDateTime(provider As IFormatProvider) As Date Implements IConvertible.ToDateTime
            Return DirectCast(ToString(), IConvertible).ToDateTime(provider)
        End Function

        Private Overloads Function ToString(provider As IFormatProvider) As String Implements IConvertible.ToString
            Return ToString()
        End Function

        Private Function ToType(conversionType As Type, provider As IFormatProvider) As Object Implements IConvertible.ToType
            If (conversionType = GetType(String)) Then Return ToString()
            If (conversionType = GetType(Utf32String)) Then Return Me
            If (conversionType = GetType(Integer())) Then Return ToUtf32CharArray()
            Return DirectCast(ToString(), IConvertible).ToType(conversionType, provider)
        End Function
#End Region

        Public Function GetEnumerator() As IEnumerator(Of Integer) Implements IEnumerable(Of Integer).GetEnumerator
            Return _characters.GetEnumerator
        End Function

        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

        Public Overloads Function Equals(other As Utf32String) As Boolean Implements IEquatable(Of Utf32String).Equals
            Return Me = other
        End Function
#End Region
    End Class
End Namespace