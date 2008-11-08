Imports System.Runtime.CompilerServices

#If Config <= Nightly Then 'Stage:Nightly
Namespace ExtensionsT
    ''' <summary>Contains extension methods for working with numbers of basic data types</summary>
    <Author("Đonny", "dzonny.dz@gmail.com", "http://dzonny.cz")> _
    <Version(1, 0, GetType(Numbers), LastChange:="6/20/2008"), FirstVersion(2008, 6, 20)> _
 Public Module Numbers
        ''' <summary>Returns a value indicating whether the specified number evaluates to not a number (<see cref="System.Single.NaN" />).</summary>
        ''' <param name="n">A single-precision floating-point number.</param>
        ''' <returns>true if f evaluates to not a number (<see cref="System.Single.NaN" />); otherwise, false.</returns>
        <Extension()> Function IsNaN(ByVal n As Single) As Boolean
            Return Single.IsNaN(n)
        End Function
        ''' <summary>Returns a value indicating whether the specified number evaluates to a value that is not a number (<see cref="System.Double.NaN" />).</summary>
        ''' <param name="n">A double-precision floating-point number.</param>
        ''' <returns>true if d evaluates to <see cref="System.Double.NaN" />; otherwise, false.</returns>
        <Extension()> Function IsNaN(ByVal n As Double) As Boolean
            Return Double.IsNaN(n)
        End Function
        ''' <summary>Returns a value indicating whether the specified number evaluates to negative or positive infinity.</summary>
        ''' <param name="n">A single-precision floating-point number.</param>
        ''' <returns>true if f evaluates to <see cref="System.Single.PositiveInfinity" /> or <see cref="System.Single.NegativeInfinity" />; otherwise, false.</returns>
        <Extension()> Function IsInfinity(ByVal n As Single) As Boolean
            Return Single.IsInfinity(n)
        End Function
        ''' <summary>Returns a value indicating whether the specified number evaluates to negative or positive infinity</summary>
        ''' <param name="n">A double-precision floating-point number.</param>
        ''' <returns>true if d evaluates to <see cref="System.Double.PositiveInfinity" /> or <see cref="System.Double.NegativeInfinity" />; otherwise, false.</returns>
        <Extension()> Function IsInfinity(ByVal n As Double) As Boolean
            Return Double.IsInfinity(n)
        End Function
        ''' <summary>Returns a value indicating whether the specified number evaluates to negative infinity.</summary>
        ''' <param name="n">A single-precision floating-point number.</param>
        ''' <returns>true if f evaluates to <see cref="System.Single.NegativeInfinity" />; otherwise, false.</returns>
        <Extension()> Function IsNegativeInfinity(ByVal n As Single) As Boolean
            Return Single.IsNegativeInfinity(n)
        End Function
        ''' <summary>Returns a value indicating whether the specified number evaluates to positive infinity.</summary>
        ''' <param name="n">A single-precision floating-point number.</param>
        ''' <returns>true if f evaluates to <see cref="System.Single.PositiveInfinity" />; otherwise, false.</returns>
        <ExtenderProvidedProperty()> Function IsPositiveInfinity(ByVal n As Single) As Boolean
            Return Single.IsPositiveInfinity(n)
        End Function
        ''' <summary>Returns a value indicating whether the specified number evaluates to negative infinity.</summary>
        ''' <param name="n">A double-precision floating-point number.</param>
        ''' <returns>true if d evaluates to <see cref="System.Double.NegativeInfinity" />; otherwise, false.</returns>
        <Extension()> Function IsNegativeInfinity(ByVal n As Double) As Boolean
            Return Double.IsNegativeInfinity(n)
        End Function
        ''' <summary>Returns a value indicating whether the specified number evaluates to positive infinity.</summary>
        ''' <param name="n">A double-precision floating-point number.</param>
        ''' <returns>true if d evaluates to <see cref="System.Double.PositiveInfinity" />; otherwise, false.</returns>
        <Extension()> Function IsPositiveInfinity(ByVal n As Double) As Boolean
            Return Double.IsPositiveInfinity(n)
        End Function
        ''' <summary>Converts the value of a specified instance of <see cref="System.Decimal" /> to its equivalent binary representation.</summary>
        ''' <param name="n">A <see cref="System.Decimal" /> value.</param>
        ''' <returns>A 32-bit signed integer array with four elements that contain the binary representation of d.</returns>
        <Extension()> Function GetBits(ByVal n As Decimal) As Integer()
            Return Decimal.GetBits(n)
        End Function
#Region "Low and high"
        ''' <summary>Gets low (least significant) byte from <see cref="UShort"/> value</summary>
        ''' <param name="n">Value to get byte from</param>
        ''' <returns>Least significant byte of <paramref name="n"/></returns>
        <CLSCompliant(False)> _
        <Extension()> Function Low(ByVal n As UShort) As Byte
            Return n And &HFFUS
        End Function
        ''' <summary>Gets high (most significant) byte from <see cref="UShort"/> value</summary>
        ''' <param name="n">Value to get byte from</param>
        ''' <returns>Most significant of <paramref name="n"/></returns>
        <CLSCompliant(False)> _
        <Extension()> Function High(ByVal n As UShort) As Byte
            Return (n And &HFF00US) >> 8
        End Function
        ''' <summary>Gets high (most significant) word (2 bytes) from <see cref="Uinteger"/> value</summary>
        ''' <param name="n">Value to get word from</param>
        ''' <returns>Most significant word of <paramref name="n"/></returns>
        <CLSCompliant(False)> _
        <Extension()> Function High(ByVal n As UInteger) As UShort
            Return (n And &HFFFF0000UI) >> 16
        End Function
        ''' <summary>Gets low (leastLeast significant) word (2 bytes) from <see cref="UInteger"/> value</summary>
        ''' <param name="n">Value to get word from</param>
        ''' <returns>Least significant word of <paramref name="n"/></returns>
        <CLSCompliant(False)> _
        <Extension()> Function Low(ByVal n As UInteger) As UShort
            Return n And &HFFFFUI
        End Function
        ''' <summary>Gets high (most significant) dword (4 bytes) from <see cref="ULong"/> value</summary>
        ''' <param name="n">Value to get dword from</param>
        ''' <returns>Most significant dword of <paramref name="n"/></returns>
        <CLSCompliant(False)> _
        <Extension()> Function High(ByVal n As ULong) As UInteger
            Return (n And &HFFFFFFFF00000000UL) >> 32
        End Function
        ''' <summary>Gets low (least significant) dword (4 bytes) from <see cref="ULong"/> value</summary>
        ''' <param name="n">Value to get dword from</param>
        ''' <returns>Least significant dword of <paramref name="n"/></returns>
        <CLSCompliant(False)> _
        <Extension()> Function Low(ByVal n As ULong) As UInteger
            Return n And &HFFFFFFFFUL
        End Function
        ''' <summary>Gets low (least significant) byte from <see cref="Short"/> value</summary>
        ''' <param name="n">Value to get byte from</param>
        ''' <returns>Least significant byte of <paramref name="n"/></returns>
        <Extension()> Function Low(ByVal n As Short) As Byte
            Return n.BitwiseSame.Low
        End Function
        ''' <summary>Gets high (most significant) byte from <see cref="Short"/> value</summary>
        ''' <param name="n">Value to get byte from</param>
        ''' <returns>Most significant byte of <paramref name="n"/></returns>
        <Extension()> Function High(ByVal n As Short) As Byte
            Return n.BitwiseSame.High
        End Function
        ''' <summary>Gets low (least significant) word (2 bytes) from <see cref="Integer"/> value</summary>
        ''' <param name="n">Value to get word from</param>
        ''' <returns>Least significant word of <paramref name="n"/></returns>
        <Extension(), CLSCompliant(False)> Function Low(ByVal n As Integer) As UShort
            Return n.BitwiseSame.Low
        End Function
        ''' <summary>Gets high (most significant) word (2 bytes) from <see cref="Integer"/> value</summary>
        ''' <param name="n">Value to get word from</param>
        ''' <returns>Most significant word of <paramref name="n"/></returns>
        <Extension(), CLSCompliant(False)> Function High(ByVal n As Integer) As UShort
            Return n.BitwiseSame.High
        End Function
        ''' <summary>Gets low (least significant) dword (4 bytes) from <see cref="Long"/> value</summary>
        ''' <param name="n">Value to get dword from</param>
        ''' <returns>Least significant dword of <paramref name="n"/></returns>
        <Extension(), CLSCompliant(False)> Function Low(ByVal n As Long) As UInteger
            Return n.BitwiseSame.Low
        End Function
        ''' <summary>Gets high (most significant) dword (4 bytes) from <see cref="Long"/> value</summary>
        ''' <param name="n">Value to get dword from</param>
        ''' <returns>Most significant dword of <paramref name="n"/></returns>
        <Extension(), CLSCompliant(False)> Function High(ByVal n As Long) As UInteger
            Return n.BitwiseSame.High
        End Function
#End Region
#Region "GetBits"
        ''' <summary>Gets bits form given <see cref="Byte"/></summary>
        ''' <param name="n">Number to get bits of</param>
        ''' <returns>Bits from given byte. Index 0 is LSB, index 7 is MSB.</returns>
        <Extension()> Function GetBits(ByVal n As Byte) As BitArray
            Return New BitArray(New Byte() {n})
        End Function
        ''' <summary>Gets bits form given <see cref="USHort"/></summary>
        ''' <param name="n">Number to get bits of</param>
        ''' <returns>Bits from given byte. Index 0 is LSB, index 15 is MSB.</returns>
        <CLSCompliant(False)> _
        <Extension()> Function GetBits(ByVal n As UShort) As BitArray
            Return New BitArray(New Byte() {n.Low, n.High})
        End Function
        ''' <summary>Gets bits form given <see cref="UInteger"/></summary>
        ''' <param name="n">Number to get bits of</param>
        ''' <returns>Bits from given byte. Index 0 is LSB, index 31 is MSB.</returns>
        <CLSCompliant(False)> _
        <Extension()> Function GetBits(ByVal n As UInteger) As BitArray
            Return New BitArray(New Byte() {n.Low.Low, n.Low.High, n.High.Low, n.High.High})
        End Function
        ''' <summary>Gets bits form given <see cref="ULong"/></summary>
        ''' <param name="n">Number to get bits of</param>
        ''' <returns>Bits from given byte. Index 0 is LSB, index 63 is MSB.</returns>
        <CLSCompliant(False)> _
        <Extension()> Function GetBits(ByVal n As ULong) As BitArray
            Return New BitArray(New Byte() {n.Low.Low.Low, n.Low.Low.High, n.Low.High.Low, n.Low.High.High, n.High.Low.Low, n.High.Low.High, n.High.High.Low, n.High.High.High})
        End Function
        ''' <summary>Gets bits form given <see cref="SByte"/></summary>
        ''' <param name="n">Number to get bits of</param>
        ''' <returns>Bits from given byte. Index 0 is LSB, index 7 is MSB.</returns>
        <CLSCompliant(False)> _
        <Extension()> Function GetBits(ByVal n As SByte) As BitArray
            Return n.BitwiseSame.GetBits
        End Function
        ''' <summary>Gets bits form given <see cref="Short"/></summary>
        ''' <param name="n">Number to get bits of</param>
        ''' <returns>Bits from given byte. Index 0 is LSB, index 15 is MSB.</returns>
        <Extension()> Function GetBits(ByVal n As Short) As BitArray
            Return n.BitwiseSame.GetBits
        End Function
        ''' <summary>Gets bits form given <see cref="Integer"/></summary>
        ''' <param name="n">Number to get bits of</param>
        ''' <returns>Bits from given byte. Index 0 is LSB, index 31 is MSB.</returns>
        <Extension()> Function GetBits(ByVal n As Integer) As BitArray
            Return n.BitwiseSame.GetBits
        End Function
        ''' <summary>Gets bits form given <see cref="Long"/></summary>
        ''' <param name="n">Number to get bits of</param>
        ''' <returns>Bits from given byte. Index 0 is LSB, index 63 is MSB.</returns>
        <Extension()> Function GetBits(ByVal n As Long) As BitArray
            Return n.BitwiseSame.GetBits
        End Function
#End Region
#Region "Bitwise same"
        ''' <summary>Gets signed number with same hexadecimal value as given unsigned number</summary>
        ''' <param name="n">Number to convert</param>
        ''' <remarks>Number that has binary same value if <paramref name="n"/> but is signed</remarks>
        <CLSCompliant(False)> _
        <Extension()> Function BitwiseSame(ByVal n As Byte) As SByte
            Return If(n > SByte.MaxValue, CShort(n) - Byte.MaxValue - 1, n)
        End Function
        ''' <summary>Gets signed number with same hexadecimal value as given unsigned number</summary>
        ''' <param name="n">Number to convert</param>
        ''' <remarks>Number that has binary same value if <paramref name="n"/> but is signed</remarks>
        <CLSCompliant(False)> _
        <Extension()> Function BitwiseSame(ByVal n As UShort) As Short
            Return If(n > Short.MaxValue, CInt(n) - UShort.MaxValue - 1, n)
        End Function
        ''' <summary>Gets signed number with same hexadecimal value as given unsigned number</summary>
        ''' <param name="n">Number to convert</param>
        ''' <remarks>Number that has binary same value if <paramref name="n"/> but is signed</remarks>
        <Extension()> Function BitwiseSame(ByVal n As UInteger) As Integer
            Return If(n > Integer.MaxValue, CLng(n) - UInteger.MaxValue - 1, n)
        End Function
        ''' <summary>Gets signed number with same hexadecimal value as given unsigned number</summary>
        ''' <param name="n">Number to convert</param>
        ''' <remarks>Number that has binary same value if <paramref name="n"/> but is signed</remarks>
        <CLSCompliant(False)> _
        <Extension()> Function BitwiseSame(ByVal n As ULong) As Long
            Return If(n > Long.MaxValue, CDec(n) - ULong.MaxValue - 1, n)
        End Function
        ''' <summary>Gets unsigned number with same hexadecimal value as given signed number</summary>
        ''' <param name="n">Number to convert</param>
        ''' <remarks>Number that has binary same value if <paramref name="n"/> but is unsigned</remarks>
        <CLSCompliant(False)> _
        <Extension()> Function BitwiseSame(ByVal n As SByte) As Byte
            Return If(n < 0, n + Byte.MaxValue + 1, n)
        End Function
        ''' <summary>Gets unsigned number with same hexadecimal value as given signed number</summary>
        ''' <param name="n">Number to convert</param>
        ''' <remarks>Number that has binary same value if <paramref name="n"/> but is unsigned</remarks>
        <CLSCompliant(False)> _
        <Extension()> Function BitwiseSame(ByVal n As Short) As UShort
            Return If(n < 0, n + UShort.MaxValue + 1, n)
        End Function
        ''' <summary>Gets unsigned number with same hexadecimal value as given signed number</summary>
        ''' <param name="n">Number to convert</param>
        ''' <remarks>Number that has binary same value if <paramref name="n"/> but is unsigned</remarks>
        <CLSCompliant(False)> _
        <Extension()> Function BitwiseSame(ByVal n As Integer) As UInteger
            Return If(n < 0, n + UInteger.MaxValue + 1, n)
        End Function
        ''' <summary>Gets unsigned number with same hexadecimal value as given signed number</summary>
        ''' <param name="n">Number to convert</param>
        ''' <remarks>Number that has binary same value if <paramref name="n"/> but is unsigned</remarks>
        <CLSCompliant(False)> _
        <Extension()> Function BitwiseSame(ByVal n As Long) As ULong
            Return If(n < 0, n + ULong.MaxValue + 1, n)
        End Function
#End Region
#Region "Bitwise"
        ''' <summary>Gets value of given bit of <see cref="Byte"/></summary>
        ''' <param name="n">Number to get bit of</param>
        ''' <param name="bit">Number of bit to get (0 for LSB, 7 for MSB)</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="bit"/> is greater than 7</exception>
        <Extension()> Function GetBit(ByVal n As Byte, ByVal bit As Byte) As Boolean
            If bit > 7 Then Throw New ArgumentOutOfRangeException("bit", String.Format(ResourcesT.Exceptions.NumberOfBitIn0MustBeLessThat1, "Byte", 8))
            Return (n And (CByte(1) << bit)) = (CByte(1) << bit)
        End Function
        ''' <summary>Gets value of given bit of <see cref="ushort"/></summary>
        ''' <param name="n">Number to get bit of</param>
        ''' <param name="bit">Number of bit to get (0 for LSB, 15 for MSB)</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="bit"/> is greater than 15</exception>
        <CLSCompliant(False)> _
        <Extension()> Function GetBit(ByVal n As UShort, ByVal bit As Byte) As Boolean
            If bit > 15 Then Throw New ArgumentOutOfRangeException("bit", String.Format(ResourcesT.Exceptions.NumberOfBitIn0MustBeLessThat1, "UInt16", 16))
            Return (n And (1US << bit)) = (1US << bit)
        End Function
        ''' <summary>Gets value of given bit of <see cref="uinteger"/></summary>
        ''' <param name="n">Number to get bit of</param>
        ''' <param name="bit">Number of bit to get (0 for LSB, 31 for MSB)</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="bit"/> is greater than 31</exception>
        <CLSCompliant(False)> _
        <Extension()> Function GetBit(ByVal n As UInteger, ByVal bit As Byte) As Boolean
            If bit > 31 Then Throw New ArgumentOutOfRangeException("bit", String.Format(ResourcesT.Exceptions.NumberOfBitIn0MustBeLessThat1, "UInt32", 32))
            Return (n And (1UI << bit)) = (1UI << bit)
        End Function
        ''' <summary>Gets value of given bit of <see cref="ULong"/></summary>
        ''' <param name="n">Number to get bit of</param>
        ''' <param name="bit">Number of bit to get (0 for LSB, 63 for MSB)</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="bit"/> is greater than 63</exception>
        <CLSCompliant(False)> _
        <Extension()> Function GetBit(ByVal n As ULong, ByVal bit As Byte) As Boolean
            If bit > 63 Then Throw New ArgumentOutOfRangeException("bit", String.Format(ResourcesT.Exceptions.NumberOfBitIn0MustBeLessThat1, "UInt64", 64))
            Return (n And (1UL << bit)) = (1UL << bit)
        End Function

        ''' <summary>Gets value of given bit of <see cref="sByte"/></summary>
        ''' <param name="n">Number to get bit of</param>
        ''' <param name="bit">Number of bit to get (0 for LSB, 7 for MSB)</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="bit"/> is greater than 7</exception>
        <CLSCompliant(False)> _
        <Extension()> Function GetBit(ByVal n As SByte, ByVal bit As Byte) As Boolean
            Return n.BitwiseSame.GetBit(n)
        End Function
        ''' <summary>Gets value of given bit of <see cref="short"/></summary>
        ''' <param name="n">Number to get bit of</param>
        ''' <param name="bit">Number of bit to get (0 for LSB, 15 for MSB)</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="bit"/> is greater than 15</exception>
        <Extension()> Function GetBit(ByVal n As Short, ByVal bit As Byte) As Boolean
            Return n.BitwiseSame.GetBit(n)
        End Function
        ''' <summary>Gets value of given bit of <see cref="integer"/></summary>
        ''' <param name="n">Number to get bit of</param>
        ''' <param name="bit">Number of bit to get (0 for LSB, 31 for MSB)</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="bit"/> is greater than 31</exception>
        <Extension()> Function GetBit(ByVal n As Integer, ByVal bit As Byte) As Boolean
            Return n.BitwiseSame.GetBit(n)
        End Function
        ''' <summary>Gets value of given bit of <see cref="ULong"/></summary>
        ''' <param name="n">Number to get bit of</param>
        ''' <param name="bit">Number of bit to get (0 for LSB, 63 for MSB)</param>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="bit"/> is greater than 63</exception>
        <Extension()> Function GetBit(ByVal n As Long, ByVal bit As Byte) As Boolean
            Return n.BitwiseSame.GetBit(n)
        End Function

        ''' <summary>Sets value of given bit in number</summary>
        ''' <param name="n">Passed by reference. Number to change bit in</param>
        ''' <param name="bit">Number of bit to be set. 0 for LSB, 7 for MSB</param>
        ''' <param name="value">New value of bit number <paramref name="bit"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="bit"/> is greater than 7</exception>
        <Extension()> Sub SetBit(ByRef n As Byte, ByVal bit As Byte, ByVal value As Boolean)
            If bit > 7 Then Throw New ArgumentOutOfRangeException("bit", String.Format(ResourcesT.Exceptions.NumberOfBitIn0MustBeLessThat1, "Byte", 8))
            If value Then n = n Or (CByte(1) << bit) _
            Else n = n And Not (CByte(1) << bit)
        End Sub
        ''' <summary>Sets value of given bit in number</summary>
        ''' <param name="n">Passed by reference. Number to change bit in</param>
        ''' <param name="bit">Number of bit to be set. 0 for LSB, 7 for MSB</param>
        ''' <param name="value">New value of bit number <paramref name="bit"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="bit"/> is greater than 7</exception>
        <CLSCompliant(False)> _
        <Extension()> Sub SetBit(ByRef n As SByte, ByVal bit As Byte, ByVal value As Boolean)
            If bit > 7 Then Throw New ArgumentOutOfRangeException("bit", String.Format(ResourcesT.Exceptions.NumberOfBitIn0MustBeLessThat1, "SByte", 8))
            If value Then n = n Or (CSByte(1) << bit) _
            Else n = n And Not (CSByte(1) << bit)
        End Sub
        ''' <summary>Sets value of given bit in number</summary>
        ''' <param name="n">Passed by reference. Number to change bit in</param>
        ''' <param name="bit">Number of bit to be set. 0 for LSB, 15 for MSB</param>
        ''' <param name="value">New value of bit number <paramref name="bit"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="bit"/> is greater than 15</exception>
        <CLSCompliant(False)> _
        <Extension()> Sub SetBit(ByRef n As UShort, ByVal bit As Byte, ByVal value As Boolean)
            If bit > 15 Then Throw New ArgumentOutOfRangeException("bit", String.Format(ResourcesT.Exceptions.NumberOfBitIn0MustBeLessThat1, "UInt16", 16))
            If value Then n = n Or (1US << bit) _
            Else n = n And Not (1US << bit)
        End Sub
        ''' <summary>Sets value of given bit in number</summary>
        ''' <param name="n">Passed by reference. Number to change bit in</param>
        ''' <param name="bit">Number of bit to be set. 0 for LSB, 15 for MSB</param>
        ''' <param name="value">New value of bit number <paramref name="bit"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="bit"/> is greater than 15</exception>
        <Extension()> Sub SetBit(ByRef n As Short, ByVal bit As Byte, ByVal value As Boolean)
            If bit > 15 Then Throw New ArgumentOutOfRangeException("bit", String.Format(ResourcesT.Exceptions.NumberOfBitIn0MustBeLessThat1, "Int16", 16))
            If value Then n = n Or (1S << bit) _
            Else n = n And Not (1S << bit)
        End Sub
        ''' <summary>Sets value of given bit in number</summary>
        ''' <param name="n">Passed by reference. Number to change bit in</param>
        ''' <param name="bit">Number of bit to be set. 0 for LSB, 31 for MSB</param>
        ''' <param name="value">New value of bit number <paramref name="bit"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="bit"/> is greater than 31</exception>
        <Extension()> Sub SetBit(ByRef n As Integer, ByVal bit As Byte, ByVal value As Boolean)
            If bit > 31 Then Throw New ArgumentOutOfRangeException("bit", String.Format(ResourcesT.Exceptions.NumberOfBitIn0MustBeLessThat1, "Int32", 32))
            If value Then n = n Or (1I << bit) _
            Else n = n And Not (1I << bit)
        End Sub
        ''' <summary>Sets value of given bit in number</summary>
        ''' <param name="n">Passed by reference. Number to change bit in</param>
        ''' <param name="bit">Number of bit to be set. 0 for LSB, 63 for MSB</param>
        ''' <param name="value">New value of bit number <paramref name="bit"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="bit"/> is greater than 63</exception>
        <Extension()> Sub SetBit(ByRef n As Long, ByVal bit As Byte, ByVal value As Boolean)
            If bit > 63 Then Throw New ArgumentOutOfRangeException("bit", String.Format(ResourcesT.Exceptions.NumberOfBitIn0MustBeLessThat1, "Int64", 64))
            If value Then n = n Or (1L << bit) _
            Else n = n And Not (1L << bit)
        End Sub
        ''' <summary>Sets value of given bit in number</summary>
        ''' <param name="n">Passed by reference. Number to change bit in</param>
        ''' <param name="bit">Number of bit to be set. 0 for LSB, 31 for MSB</param>
        ''' <param name="value">New value of bit number <paramref name="bit"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="bit"/> is greater than 31</exception>
        <CLSCompliant(False)> _
        <Extension()> Sub SetBit(ByRef n As UInteger, ByVal bit As Byte, ByVal value As Boolean)
            If bit > 31 Then Throw New ArgumentOutOfRangeException("bit", String.Format(ResourcesT.Exceptions.NumberOfBitIn0MustBeLessThat1, "UInt32", 32))
            If value Then n = n Or (1UI << bit) _
            Else n = n And Not (1UI << bit)
        End Sub
        ''' <summary>Sets value of given bit in number</summary>
        ''' <param name="n">Passed by reference. Number to change bit in</param>
        ''' <param name="bit">Number of bit to be set. 0 for LSB, 63 for MSB</param>
        ''' <param name="value">New value of bit number <paramref name="bit"/></param>
        ''' <exception cref="ArgumentNullException"><paramref name="bit"/> is greater than 63</exception>
        <CLSCompliant(False)> _
        <Extension()> Sub SetBit(ByRef n As ULong, ByVal bit As Byte, ByVal value As Boolean)
            If bit > 63 Then Throw New ArgumentOutOfRangeException("bit", String.Format(ResourcesT.Exceptions.NumberOfBitIn0MustBeLessThat1, "UInt64", 64))
            If value Then n = n Or (1UL << bit) _
            Else n = n And Not (1UL << bit)
        End Sub
#End Region
    End Module
End Namespace
#End If
