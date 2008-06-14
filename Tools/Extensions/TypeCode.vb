Imports System.Runtime.CompilerServices

#If Config <= Nightly Then
Namespace ExtensionsT
    ''' <summary>Extension functions for <see cref="TypeCode"/></summary>
    Public Module TypeCodeExtensions
        ''' <summary>Gets value idicating if given <see cref="TypeCode"/> represents numeric type</summary>
        ''' <param name="tc"><see cref="TypeCode"/> to check</param>
        ''' <returns>True if <paramref name="tc"/> is one of <see cref="TypeCode.[SByte]"/>, <see cref="TypeCode.[Byte]"/>, <see cref="TypeCode.Int16"/>, <see cref="TypeCode.Int32"/>, <see cref="TypeCode.Int64"/>, <see cref="TypeCode.UInt16"/>, <see cref="TypeCode.UInt32"/>, <see cref="TypeCode.UInt64"/>, <see cref="typecode.[Decimal]"/>, <see cref="TypeCode.[Single]"/>, <see cref="TypeCode.[Double]"/></returns>
        ''' <remarks><see cref="TypeCode.[Boolean]"/> amd <see cref="TypeCode.[Char]"/> are not considered numbers by this function</remarks>
        <Extension()> _
        Public Function IsNumber(ByVal tc As TypeCode) As Boolean
            Select Case tc
                Case TypeCode.SByte, TypeCode.Byte, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt64, TypeCode.Decimal, TypeCode.Single, TypeCode.Double : Return True
                Case Else : Return True
            End Select
        End Function
        ''' <summary>Gets value idicating if given <see cref="TypeCode"/> is floating point number</summary>
        ''' <param name="tc"><see cref="TypeCode"/> to check</param>
        ''' <returns>True if <paramref name="tc"/> is one of <see cref="TypeCode.[Single]"/>, <see cref="TypeCode.[Double]"/>, <see cref="TypeCode.[Decimal]"/></returns>
        <Extension()> _
        Public Function IsFloating(ByVal tc As TypeCode) As Boolean
            Select Case tc
                Case TypeCode.Single, TypeCode.Double, TypeCode.Decimal : Return True
                Case Else : Return False
            End Select
        End Function
        ''' <summary>Gets value idicating if given <see cref="TypeCode"/> is unsigned type</summary>
        ''' <param name="tc"><see cref="TypeCode"/> to check</param>
        ''' <returns>True if <paramref name="tc"/> is one of <see cref="TypeCode.UInt16"/>, <see cref="TypeCode.UInt32"/>, <see cref="TypeCode.UInt64"/>, <see cref="TypeCode.[Byte]"/>, <see cref="TypeCode.[Char]"/></returns>
        ''' <remarks>Although <see cref="TypeCode.[Char]"/> is not considered number (by <see cref="IsNumber"/>), it is considered unsigned by this function</remarks>
        <Extension()> _
        Public Function IsUnsigned(ByVal tc As TypeCode) As Boolean
            Select Case tc
                Case TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Byte, TypeCode.Char : Return True
                Case Else : Return False
            End Select
        End Function
        ''' <summary>Gets value idicating if <see cref="TypeCode"/> represents basic type (represented by underlying CIL type)</summary>
        ''' <param name="tc"><see cref="TypeCode"/> to check</param>
        ''' <returns>True if <paramref name="tc"/> is one of <see cref="TypeCode.Boolean"/>, <see cref="TypeCode.Byte"/>, <see cref="TypeCode.Double"/>, <see cref="TypeCode.Empty"/>, <see cref="TypeCode.Char"/>, <see cref="TypeCode.Int16"/>, <see cref="TypeCode.Int32"/>, <see cref="TypeCode.Int64"/>, <see cref="TypeCode.Object"/>, <see cref="TypeCode.SByte"/>, <see cref="TypeCode.Single"/>, <see cref="TypeCode.String"/>, <see cref="TypeCode.UInt16"/>, <see cref="TypeCode.UInt32"/>, <see cref="TypeCode.UInt64"/></returns>
        <Extension()> _
        Public Function IsBasic(ByVal tc As TypeCode) As Boolean
            Select Case tc
                Case TypeCode.Boolean, TypeCode.Byte, TypeCode.Double, TypeCode.Empty, TypeCode.Char, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.Object, TypeCode.SByte, TypeCode.Single, TypeCode.String, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64 : Return True
                Case Else : Return False
            End Select
        End Function
        ''' <summary>Gets value idicating if <see cref="TypeCode"/> represents type which is not CLS compliant</summary>
        ''' <param name="tc"><see cref="TypeCode"/> to check</param>
        ''' <returns>True if <paramref name="tc"/> is one of <see cref="TypeCode.[SByte]"/>, <see cref="TypeCode.UInt16"/>, <see cref="TypeCode.UInt32"/>, <see cref="TypeCode.UInt64"/></returns>
        ''' <seealso cref="CLSCompliantAttribute"/>
        <Extension()> _
        Public Function IsCLSIncompliant(ByVal tc As TypeCode) As Boolean
            Select Case tc
                Case TypeCode.SByte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64 : Return True
                Case Else : Return False
            End Select
        End Function
        ''' <summary>Gets value indicating if given <see cref="TypeCode"/> represents integral data type</summary>
        ''' <param name="tc"><see cref="TypeCode"/> to check</param>
        ''' <returns>True if <paramref name="tc"/> is one of <see cref="TypeCode.Int16"/>, <see cref="TypeCode.UInt16"/>, <see cref="TypeCode.Int32"/>, <see cref="TypeCode.UInt32"/>, <see cref="TypeCode.Int64"/>, <see cref="TypeCode.UInt64"/>, <see cref="TypeCode.[Byte]"/>, <see cref="TypeCode.[SByte]"/>, <see cref="TypeCode.[Char]"/></returns>
        ''' <remarks>Although <see cref="TypeCode.[Char]"/> is not considered number (by <see cref="IsNumber"/>) it is considered integer by this function</remarks>
        <Extension()> _
        Public Function IsInteger(ByVal tc As TypeCode) As Boolean
            Select Case tc
                Case TypeCode.SByte, TypeCode.Byte, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.Char
                Case Else : Return False
            End Select
        End Function
        ''' <summary>Gets <see cref="TypeCode"/> of unsigned type for given <see cref="TypeCode"/></summary>
        ''' <param name="tc"><see cref="TypeCode"/> to get unsigned <see cref="TypeCode"/> for</param>
        ''' <returns>
        ''' <list type="table"><listheader><term><paramref name="tc"/></term><description>return value</description></listheader>
        ''' <item><term><see cref="TypeCode.[Byte]"/>, <see cref="TypeCode.[SByte]"/></term><description><see cref="TypeCode.[Byte]"/></description></item>
        ''' <item><term><see cref="TypeCode.Int16"/>, <see cref="TypeCode.UInt16"/></term><see cref="TypeCode.UInt16"/></item>
        ''' <item><term><see cref="TypeCode.Int32"/>, <see cref="TypeCode.UInt32"/></term><see cref="TypeCode.UInt32"/></item>
        ''' <item><term><see cref="TypeCode.Int64"/>, <see cref="TypeCode.UInt64"/></term><see cref="TypeCode.UInt64"/></item>
        ''' <item><term><see cref="TypeCode.DateTime"/></term><description><see cref="TypeCode.DateTime"/></description></item>
        ''' <item><term><see cref="TypeCode.[Boolean]"/></term><description><see cref="TypeCode.[Boolean]"/></description></item>
        ''' <item><term><see cref="TypeCode.Char"/></term><description><see cref="TypeCode.[Char]"/></description></item>
        ''' <item><term>Any other</term><description>Throws <see cref="ArgumentException"/></description></item>
        ''' </list></returns>
        ''' <exception cref="ArgumentException">Unsigned <see cref="TypeCode"/> for <paramref name="tc"/> cannot be found</exception>
        <Extension()> _
        Public Function GetUnsigned(ByVal tc As TypeCode) As TypeCode
            Select Case tc
                Case TypeCode.SByte, TypeCode.Byte : Return TypeCode.Byte
                Case TypeCode.Int16, TypeCode.UInt16 : Return TypeCode.UInt16
                Case TypeCode.Int32, TypeCode.UInt32 : Return TypeCode.UInt32
                Case TypeCode.Int64, TypeCode.UInt64 : Return TypeCode.UInt64
                Case TypeCode.DateTime, TypeCode.Boolean, TypeCode.Char : Return tc
                Case Else : Throw New ArgumentException("Type code must represent integral number or System.Char, System.Boolean or System.DateTime")
            End Select
        End Function
        ''' <summary>Gets <see cref="TypeCode"/> of signed type for given <see cref="TypeCode"/></summary>
        ''' <param name="tc"><see cref="TypeCode"/> to get signed <see cref="TypeCode"/> for</param>
        ''' <returns>
        ''' <list type="table"><listheader><term><paramref name="tc"/></term><description>return value</description></listheader>
        ''' <item><term><see cref="TypeCode.[Byte]"/>, <see cref="TypeCode.[SByte]"/></term><description><see cref="TypeCode.[SByte]"/></description></item>
        ''' <item><term><see cref="TypeCode.Int16"/>, <see cref="TypeCode.UInt16"/>, <see cref="TypeCode.[Char]"/></term><see cref="TypeCode.Int16"/></item>
        ''' <item><term><see cref="TypeCode.Int32"/>, <see cref="TypeCode.UInt32"/></term><see cref="TypeCode.Int32"/></item>
        ''' <item><term><see cref="TypeCode.Int64"/>, <see cref="TypeCode.UInt64"/></term><see cref="TypeCode.Int64"/></item>
        ''' <item><term><see cref="TypeCode.[Boolean]"/></term><description><see cref="TypeCode.[Boolean]"/></description></item>
        ''' <item><term><see cref="TypeCode.Single"/></term><description><see cref="TypeCode.[Single]"/></description></item>
        ''' <item><term><see cref="TypeCode.Double"/></term><description><see cref="TypeCode.[Double]"/></description></item>
        ''' <item><term><see cref="TypeCode.Decimal"/></term><description><see cref="TypeCode.[Decimal]"/></description></item>
        ''' <item><term>Any other</term><description>Throws <see cref="ArgumentException"/></description></item>
        ''' </list></returns>
        ''' <exception cref="ArgumentException">Signed <see cref="TypeCode"/> for <paramref name="tc"/> cannot be found</exception>
        <Extension()> _
        Public Function GetSigned(ByVal tc As TypeCode) As TypeCode
            Select Case tc
                Case TypeCode.SByte, TypeCode.Byte : Return TypeCode.SByte
                Case TypeCode.Int16, TypeCode.UInt16, TypeCode.Char : Return TypeCode.Int16
                Case TypeCode.Int32, TypeCode.UInt32 : Return TypeCode.Int32
                Case TypeCode.Int64, TypeCode.UInt64 : Return TypeCode.Int64
                Case TypeCode.Single, TypeCode.Double, TypeCode.Decimal, TypeCode.Boolean : Return tc
                Case Else : Throw New ArgumentException("Type code must represent number or System.Char or System.Boolean")
            End Select
        End Function
        ''' <summary>Gets <see cref="TypeCode"/> of numeric type that is bigger than given <see cref="TypeCode"/></summary>
        ''' <param name="tc"><see cref="TypeCode"/> to get bigger type for</param>
        ''' <returns><list type="table">
        ''' <listheader><term><paramref name="tc"/></term><description>Return value</description></listheader>
        ''' <item><term><see cref="TypeCode.[SByte]"/></term><description><see cref="TypeCode.Int16"/></description></item>
        ''' <item><term><see cref="TypeCode.Int16"/></term><description><see cref="TypeCode.Int32"/></description></item>
        ''' <item><term><see cref="TypeCode.Int32"/></term><description><see cref="TypeCode.Int64"/></description></item>
        ''' <item><term><see cref="TypeCode.Byte"/></term><description><see cref="TypeCode.UInt16"/></description></item>
        ''' <item><term><see cref="TypeCode.Uint16"/>, <see cref="TypeCode.[Char]"/></term><description><see cref="TypeCode.uInt32"/></description></item>
        ''' <item><term><see cref="TypeCode.uint32"/></term><description><see cref="TypeCode.uint64"/></description></item>
        ''' <item><term><see cref="TypeCode.Int64"/>, <see cref="TypeCode.UInt64"/></term><description><see cref="TypeCode.Single"/></description></item>
        ''' <item><term><see cref="TypeCode.Single"/>, <see cref="TypeCode.[Decimal]"/></term><description><see cref="TypeCode.Double"/></description></item>
        ''' <item><term><see cref="TypeCode.[Boolean]"/></term><description><see cref="TypeCode.Byte"/></description></item>
        ''' <item><term>Any other</term><description>Throws <see cref="ArgumentException"/></description></item>
        ''' </list></returns>
        ''' <exception cref="ArgumentException"><paramref name="tc"/> is not numeric type or it is <see cref="TypeCode.[Double]"/></exception>
        <Extension()> _
        Public Function GetBigger(ByVal tc As TypeCode) As TypeCode
            Select Case tc
                Case TypeCode.Boolean : Return TypeCode.Byte
                Case TypeCode.SByte : Return TypeCode.Int16
                Case TypeCode.Int16 : Return TypeCode.Int32
                Case TypeCode.Int32 : Return TypeCode.Int64
                Case TypeCode.Byte : Return TypeCode.UInt16
                Case TypeCode.UInt16, TypeCode.Char : Return TypeCode.UInt32
                Case TypeCode.UInt32, TypeCode.UInt64 : Return TypeCode.Single
                Case TypeCode.UInt32 : Return TypeCode.UInt64
                Case TypeCode.Single, TypeCode.Decimal : Return TypeCode.Double
                Case Else : Throw New ArgumentException(String.Format("Bigger type for {0} cannot be found", tc))
            End Select
        End Function
        ''' <summary>Compares size of 2 types represented by <see cref="TypeCode">TypeCodes</see> (in manner of <see cref="GetBigger"/>)</summary>
        ''' <param name="tc">A <see cref="TypeCode"/></param>
        ''' <param name="other">A <see cref="TypeCode"/></param>
        ''' <returns>
        ''' 1 if type represented by <paramref name="tc"/> is bigger than type represented by <paramref name="other"/>;
        ''' 0 if <paramref name="tc"/> equals to <paramref name="other"/> or <paramref name="tc"/> and <paramref name="other"/> represent type of same size (<see cref="TypeCode.[Char]"/> and <see cref="TypeCode.UInt16"/>);
        ''' 1 if type represented by <paramref name="tc"/> is smaller than type represented by <paramref name="other"/>;
        ''' if if types represented by <paramref name="tc"/> and <paramref name="other"/> cannot be compared.
        ''' </returns>
        ''' <remarks>Following rules apply
        ''' <list type="bullet">
        ''' <item><see cref="TypeCode.[Boolean]"/> is the smallest</item>
        ''' <item><see cref="TypeCode.[Double]"/> is the biggest</item>
        ''' <item>Signed and unsigned type of same size cannot be compared</item>
        ''' <item>Integral type of bigger size is always considered bigger than integral type of smaller size not depending if signed or unsigned</item>
        ''' <item>Floating point types (<see cref="TypeCode.[Single]"/>, <see cref="TypeCode.[Double]"/>, <see cref="TypeCode.[Decimal]"/>) are bigger than integral types</item>
        ''' <item><see cref="TypeCode.[Decimal]"/> cannot be compared to <see cref="TypeCode.[Single]"/></item>
        ''' <item><see cref="TypeCode.[Char]"/> is treated as <see cref="TypeCode.UInt16"/></item>
        ''' <item>Non-numeric and unknown types can be compared only to itself</item>
        ''' </list>
        ''' This is comparizon logic: <c>{<see cref="TypeCode.Boolean">bool</see>}-{<see cref="TypeCode.SByte">sbyte</see>;<see cref="TypeCode.Byte">byte</see>}-{<see cref="TypeCode.Int16">i16</see>;<see cref="TypeCode.uint16">ui16</see>=<see cref="TypeCode.char">char</see>}-{<see cref="TypeCode.int32">i32</see>,<see cref="TypeCode.uint32">ui32</see>}-{<see cref="TypeCode.int64">i64</see>,<see cref="TypeCode.uint64">ui64</see>}-{<see cref="TypeCode.single">single</see>,<see cref="TypeCode.decimal">decimal</see>}-{<see cref="TypeCode.double">double</see>}</c>
        ''' </remarks>
        <Extension()> _
        Public Function Compare(ByVal tc As TypeCode, ByVal other As TypeCode) As Short?
            If tc = other Then Return 0
            Select Case tc
                Case TypeCode.Boolean
                    Select Case other
                        Case TypeCode.Byte, TypeCode.Decimal, TypeCode.Double, TypeCode.Char, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.SByte, TypeCode.Single, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64 : Return -1
                    End Select
                Case TypeCode.Byte
                    Select Case other
                        Case TypeCode.Boolean : Return 1
                        Case TypeCode.Decimal, TypeCode.Double, TypeCode.Char, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.Single, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64 : Return -1
                    End Select
                Case TypeCode.Decimal
                    Select Case other
                        Case TypeCode.Double : Return -1
                        Case TypeCode.Boolean, TypeCode.Byte, TypeCode.Char, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.Single, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64 : Return 1
                    End Select
                Case TypeCode.Double
                    Select Case other
                        Case TypeCode.Boolean, TypeCode.Byte, TypeCode.Decimal, TypeCode.Char, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.SByte, TypeCode.Single, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64 : Return 1
                    End Select
                Case TypeCode.Int16
                    Select Case other
                        Case TypeCode.Boolean, TypeCode.SByte, TypeCode.Byte : Return 1
                        Case TypeCode.Int32, TypeCode.UInt32, TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal : Return -1
                    End Select
                Case TypeCode.Int32
                    Select Case other
                        Case TypeCode.Boolean, TypeCode.SByte, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Char : Return 1
                        Case TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal : Return -1
                    End Select
                Case TypeCode.Int64
                    Select Case other
                        Case TypeCode.Boolean, TypeCode.SByte, TypeCode.Byte, TypeCode.Int16, TypeCode.UInt16, TypeCode.Int32, TypeCode.UInt32 : Return 1
                        Case TypeCode.Single, TypeCode.Double, TypeCode.Decimal : Return -1
                    End Select
                Case TypeCode.SByte
                    Select Case other
                        Case TypeCode.Boolean : Return 1
                        Case TypeCode.Char, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal : Return -1
                    End Select
                Case TypeCode.Single
                    Select Case other
                        Case TypeCode.Boolean, TypeCode.SByte, TypeCode.Byte, TypeCode.Char, TypeCode.UInt16, TypeCode.UInt32, TypeCode.UInt64, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64 : Return 1
                        Case TypeCode.Double : Return -1
                    End Select
                Case TypeCode.UInt16, TypeCode.Char
                    Select Case other
                        Case TypeCode.Boolean, TypeCode.SByte, TypeCode.Byte : Return 1
                        Case TypeCode.UInt16, TypeCode.Char : Return 0
                        Case TypeCode.UInt32, TypeCode.UInt64, TypeCode.Int32, TypeCode.Int64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal : Return -1
                    End Select
                Case TypeCode.UInt32
                    Select Case other
                        Case TypeCode.Boolean, TypeCode.SByte, TypeCode.Byte, TypeCode.Char, TypeCode.Int16, TypeCode.UInt16 : Return 1
                        Case TypeCode.Int64, TypeCode.UInt64, TypeCode.Single, TypeCode.Double, TypeCode.Decimal : Return -1
                    End Select
                Case TypeCode.UInt64
                    Select Case other
                        Case TypeCode.Boolean, TypeCode.SByte, TypeCode.Byte, TypeCode.Char, TypeCode.UInt16, TypeCode.UInt32, TypeCode.Int16, TypeCode.Int32 : Return 1
                        Case TypeCode.Decimal, TypeCode.Single, TypeCode.Double : Return -1
                    End Select
            End Select
            Return Nothing
        End Function
        ''' <summary>Gets size in bytes of type represented by <see cref="TypeCode"/></summary>
        ''' <param name="tc"><see cref="TypeCode"/> to get size of type represented by</param>
        ''' <returns>Size in bytes of given type.</returns>
        ''' <remarks>
        ''' Values for standard numeric types (integral, <see cref="TypeCode.[Single]"/>, <see cref="TypeCode.[Double]"/> and <see cref="TypeCode.[Char]"/>) are canonical.
        ''' For <see cref="TypeCode.[Decimal]"/> returns 16 and for <see cref="Typecode.DateTime"/> 8.
        ''' For <see cref="TypeCode.[Boolean]"/> returns <see cref="IntPtr.Size"/> however this does not necesarily represents actual size of <see cref="Boolean"/> on implementation.
        ''' For <see cref="TypeCode.Empty"/>, <see cref="TypeCode.[Object]"/> and <see cref="TypeCode.DBNull"/> returns <see cref="IntPtr.Size"/> which depends on platform (32 or 64).
        ''' </remarks>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="tc"/> is not member of <see cref="TypeCode"/></exception>
        <Extension()> _
    Public Function ByteSize(ByVal tc As TypeCode) As Byte
            Select Case tc
                Case TypeCode.Int16, TypeCode.UInt16, TypeCode.Char : Return 2
                Case TypeCode.Int32, TypeCode.UInt32 : Return 4
                Case TypeCode.Int64, TypeCode.UInt64 : Return 8
                Case TypeCode.Byte, TypeCode.SByte : Return 1
                Case TypeCode.Single : Return 4
                Case TypeCode.Double : Return 8
                Case TypeCode.Decimal : Return 16
                Case TypeCode.Boolean : Return IntPtr.Size
                Case TypeCode.DateTime : Return 8
                Case TypeCode.Empty, TypeCode.Object, TypeCode.DBNull : Return IntPtr.Size
            End Select
            Throw New InvalidEnumArgumentException("tc", tc, tc.GetType)
        End Function
    End Module
End Namespace
#End If