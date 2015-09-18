Imports Tools.ExtensionsT
'LE-BE (endians) conversions
#If True
Partial Class MathT
    ''' <summary>Converts <see cref="Short"/> from Little Endian to Big Endian or vice versa</summary>
    ''' <param name="value">value to be converted</param>
    ''' <returns><paramref name="value"/> with reversed byte order</returns>
    ''' <author www="http://dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="RC"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Overloads Shared Function LEBE(ByVal value As Short) As Short
        Dim B1 As Byte = (value And &HFF00US) >> 8
        Dim B2 As Byte = value And &HFFUS
        Return CShort(B2) << 8 Or CShort(B1)
    End Function
    ''' <summary>Converts <see cref="UShort"/> from Little Endian to Big Endian or vice versa</summary>
    ''' <param name="value">value to be converted</param>
    ''' <returns><paramref name="value"/> with reversed byte order</returns>
    ''' <author www="http://dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="RC"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    <CLSCompliant(False)> _
        Public Overloads Shared Function LEBE(ByVal value As UShort) As UShort
        Dim B1 As Byte = (value And &HFF00US) >> 8
        Dim B2 As Byte = value And &HFFUS
        Return CUShort(B2) << 8 Or CUShort(B1)
    End Function
    ''' <summary>Converts <see cref="Integer"/> from Little Endian to Big Endian or vice versa</summary>
    ''' <param name="value">value to be converted</param>
    ''' <returns><paramref name="value"/> with reversed byte order</returns>
    ''' <author www="http://dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="RC"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    ''' <version version="1.5.3">Fix: Does not work correctly for negative values (<see cref="OverflowException"/> may be thrown).</version>
    Public Overloads Shared Function LEBE(ByVal value As Integer) As Integer
        Dim B1 As Byte = ((value And &HFF000000I) >> (3 * 8)) And &HFFI
        Dim B2 As Byte = ((value And &HFF0000I) >> (2 * 8)) And &HFFI
        Dim B3 As Byte = ((value And &HFF00I) >> 8) And &HFFI
        Dim B4 As Byte = value And &HFFI
        Return CInt(B4) << (3 * 8) Or CInt(B3) << (2 * 8) Or CInt(B2) << 8 Or B1
    End Function
    ''' <summary>Converts <see cref="UInteger"/> from Little Endian to Big Endian or vice versa</summary>
    ''' <param name="value">value to be converted</param>
    ''' <returns><paramref name="value"/> with reversed byte order</returns>
    ''' <author www="http://dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="RC"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    <CLSCompliant(False)> _
    Public Overloads Shared Function LEBE(ByVal value As UInteger) As UInteger
        Dim B1 As Byte = (value And &HFF000000UI) >> (3 * 8)
        Dim B2 As Byte = (value And &HFF0000UI) >> (2 * 8)
        Dim B3 As Byte = (value And &HFF00UI) >> 8
        Dim B4 As Byte = (value And &HFFUI)
        Return CUInt(B4) << (3 * 8) Or CUInt(B3) << (2 * 8) Or CUInt(B2) << 8 Or B1
    End Function
    ''' <summary>Converts <see cref="Long"/> from Little Endian to Big Endian or vice versa</summary>
    ''' <param name="value">value to be converted</param>
    ''' <returns><paramref name="value"/> with reversed byte order</returns>
    ''' <author www="http://dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="RC"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Overloads Shared Function LEBE(ByVal value As Long) As Long
        Dim B1 As Byte = (value And &HFF00000000000000L) >> (7 * 8)
        Dim B2 As Byte = (value And &HFF000000000000L) >> (6 * 8)
        Dim B3 As Byte = (value And &HFF0000000000L) >> (5 * 8)
        Dim B4 As Byte = (value And &HFF00000000L) >> (4 * 8)
        Dim B5 As Byte = (value And &HFF000000L) >> (3 * 8)
        Dim B6 As Byte = (value And &HFF0000L) >> (2 * 8)
        Dim B7 As Byte = (value And &HFF00L) >> 8
        Dim B8 As Byte = (value And &HFFL)
        Return CLng(B8) << (7 * 8) Or CLng(B7) << (6 * 8) Or CLng(B6) << (5 * 8) Or CLng(B5) << (4 * 8) Or CLng(B4) << (3 * 8) Or CLng(B3) << (2 * 8) Or CLng(B2) << 8 Or CLng(B1)
    End Function
    ''' <summary>Converts <see cref="ULong"/> from Little Endian to Big Endian or vice versa</summary>
    ''' <param name="value">value to be converted</param>
    ''' <returns><paramref name="value"/> with reversed byte order</returns>
    ''' <author www="http://dzonny.cz">Ðonny</author>
    ''' <version version="1.5.2" stage="RC"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    <CLSCompliant(False)> _
   Public Overloads Shared Function LEBE(ByVal value As ULong) As ULong
        Dim B1 As Byte = (value And &HFF00000000000000UL) >> (7 * 8)
        Dim B2 As Byte = (value And &HFF000000000000UL) >> (6 * 8)
        Dim B3 As Byte = (value And &HFF0000000000UL) >> (5 * 8)
        Dim B4 As Byte = (value And &HFF00000000UL) >> (4 * 8)
        Dim B5 As Byte = (value And &HFF000000UL) >> (3 * 8)
        Dim B6 As Byte = (value And &HFF0000UL) >> (2 * 8)
        Dim B7 As Byte = (value And &HFF00UL) >> 8
        Dim B8 As Byte = (value And &HFFUL)
        Return CULng(B8) << (7 * 8) Or CULng(B7) << (6 * 8) Or CULng(B6) << (5 * 8) Or CULng(B5) << (4 * 8) Or CULng(B4) << (3 * 8) Or CULng(B3) << (2 * 8) Or CULng(B2) << 8 Or CULng(B1)
    End Function
End Class
#End If