Namespace NumericsT
    ''' <summary>Provides operators for basic math operations</summary>
    ''' <version version="1.5.4">This module is new in version 1.5.4</version>
    Public Module Operators

#Region "+"
        ''' <summary>Implements operator + for type <see cref="SByte"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> + <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="SByte"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Addition(a As SByte, b As SByte) As SByte
            Return a + b
        End Function
        ''' <summary>Implements operator + for type <see cref="Byte"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> + <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Byte"/>.</exception>
        Public Function op_Addition(a As Byte, b As Byte) As Byte
            Return a + b
        End Function
        ''' <summary>Implements operator + for type <see cref="Short"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> + <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Short"/>.</exception>
        Public Function op_Addition(a As Short, b As Short) As Short
            Return a + b
        End Function
        ''' <summary>Implements operator + for type <see cref="UShort"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> + <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="UShort"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Addition(a As UShort, b As UShort) As UShort
            Return a + b
        End Function
        ''' <summary>Implements operator + for type <see cref="Integer"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> + <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Integer"/>.</exception>
        Public Function op_Addition(a As Integer, b As Integer) As Integer
            Return a + b
        End Function
        ''' <summary>Implements operator + for type <see cref="UInteger"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> + <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="UInteger"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Addition(a As UInteger, b As UInteger) As UInteger
            Return a + b
        End Function
        ''' <summary>Implements operator + for type <see cref="Long"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> + <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Long"/>.</exception>
        Public Function op_Addition(a As Long, b As Long) As Long
            Return a + b
        End Function
        ''' <summary>Implements operator + for type <see cref="ULong"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> + <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="ULong"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Addition(a As ULong, b As ULong) As ULong
            Return a + b
        End Function
        ''' <summary>Implements operator + for type <see cref="Single"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> + <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Single"/>.</exception>
        Public Function op_Addition(a As Single, b As Single) As Single
            Return a + b
        End Function
        ''' <summary>Implements operator + for type <see cref="Double"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> + <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Double"/>.</exception>
        Public Function op_Addition(a As Double, b As Double) As Double
            Return a + b
        End Function
        ''' <summary>Implements operator + for type <see cref="String"/> (string concatenation)</summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> + <paramref name="b"/> (strings <paramref name="a"/> and <paramref name="b"/> concatenated)</remarks>
        Public Function op_Addition(a As String, b As String) As String
            Return a & b
        End Function
#End Region
#Region "-"
        ''' <summary>Implements operator - for type <see cref="SByte"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> - <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="SByte"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Subtraction(a As SByte, b As SByte) As SByte
            Return a - b
        End Function
        ''' <summary>Implements operator - for type <see cref="Byte"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> - <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Byte"/>.</exception>
        Public Function op_Subtraction(a As Byte, b As Byte) As Byte
            Return a - b
        End Function
        ''' <summary>Implements operator - for type <see cref="Short"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> - <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Short"/>.</exception>
        Public Function op_Subtraction(a As Short, b As Short) As Short
            Return a - b
        End Function
        ''' <summary>Implements operator - for type <see cref="UShort"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> - <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="UShort"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Subtraction(a As UShort, b As UShort) As UShort
            Return a - b
        End Function
        ''' <summary>Implements operator - for type <see cref="Integer"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> - <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Integer"/>.</exception>
        Public Function op_Subtraction(a As Integer, b As Integer) As Integer
            Return a - b
        End Function
        ''' <summary>Implements operator - for type <see cref="UInteger"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> - <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="UInteger"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Subtraction(a As UInteger, b As UInteger) As UInteger
            Return a - b
        End Function
        ''' <summary>Implements operator - for type <see cref="Long"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> - <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Long"/>.</exception>
        Public Function op_Subtraction(a As Long, b As Long) As Long
            Return a - b
        End Function
        ''' <summary>Implements operator - for type <see cref="ULong"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> - <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="ULong"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Subtraction(a As ULong, b As ULong) As ULong
            Return a - b
        End Function
        ''' <summary>Implements operator - for type <see cref="Single"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> - <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Single"/>.</exception>
        Public Function op_Subtraction(a As Single, b As Single) As Single
            Return a - b
        End Function
        ''' <summary>Implements operator - for type <see cref="Double"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> - <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Double"/>.</exception>
        Public Function op_Subtraction(a As Double, b As Double) As Double
            Return a - b
        End Function
#End Region
#Region "*"
        ''' <summary>Implements operator * for type <see cref="SByte"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> * <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="SByte"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Multiply(a As SByte, b As SByte) As SByte
            Return a * b
        End Function
        ''' <summary>Implements operator * for type <see cref="Byte"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> * <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Byte"/>.</exception>
        Public Function op_Multiply(a As Byte, b As Byte) As Byte
            Return a * b
        End Function
        ''' <summary>Implements operator * for type <see cref="Short"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> * <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Short"/>.</exception>
        Public Function op_Multiply(a As Short, b As Short) As Short
            Return a * b
        End Function
        ''' <summary>Implements operator * for type <see cref="UShort"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> * <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="UShort"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Multiply(a As UShort, b As UShort) As UShort
            Return a * b
        End Function
        ''' <summary>Implements operator * for type <see cref="Integer"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> * <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Integer"/>.</exception>
        Public Function op_Multiply(a As Integer, b As Integer) As Integer
            Return a * b
        End Function
        ''' <summary>Implements operator * for type <see cref="UInteger"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> * <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="UInteger"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Multiply(a As UInteger, b As UInteger) As UInteger
            Return a * b
        End Function
        ''' <summary>Implements operator * for type <see cref="Long"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> * <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Long"/>.</exception>
        Public Function op_Multiply(a As Long, b As Long) As Long
            Return a * b
        End Function
        ''' <summary>Implements operator * for type <see cref="ULong"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> * <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="ULong"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Multiply(a As ULong, b As ULong) As ULong
            Return a * b
        End Function
        ''' <summary>Implements operator * for type <see cref="Single"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> * <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Single"/>.</exception>
        Public Function op_Multiply(a As Single, b As Single) As Single
            Return a * b
        End Function
        ''' <summary>Implements operator * for type <see cref="Double"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> * <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Double"/>.</exception>
        Public Function op_Multiply(a As Double, b As Double) As Double
            Return a * b
        End Function
#End Region
#Region "/"
        ''' <summary>Implements operator / (integral) for type <see cref="SByte"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> / <paramref name="b"/> (integral division)</remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="SByte"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Division(a As SByte, b As SByte) As SByte
            Return a \ b
        End Function
        ''' <summary>Implements operator / (integral) for type <see cref="Byte"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> / <paramref name="b"/> (integral division)</remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Byte"/>.</exception>
        Public Function op_Division(a As Byte, b As Byte) As Byte
            Return a \ b
        End Function
        ''' <summary>Implements operator / (integral) for type <see cref="Short"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> / <paramref name="b"/> (integral division)</remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Short"/>.</exception>
        Public Function op_Division(a As Short, b As Short) As Short
            Return a \ b
        End Function
        ''' <summary>Implements operator / (integral) for type <see cref="UShort"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> / <paramref name="b"/> (integral division)</remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="UShort"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Division(a As UShort, b As UShort) As UShort
            Return a \ b
        End Function
        ''' <summary>Implements operator / (integral) for type <see cref="Integer"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> / <paramref name="b"/> (integral division)</remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Integer"/>.</exception>
        Public Function op_Division(a As Integer, b As Integer) As Integer
            Return a \ b
        End Function
        ''' <summary>Implements operator / (integral) for type <see cref="UInteger"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> / <paramref name="b"/> (integral division)</remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="UInteger"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Division(a As UInteger, b As UInteger) As UInteger
            Return a \ b
        End Function
        ''' <summary>Implements operator / (integral) for type <see cref="Long"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> / <paramref name="b"/> (integral division)</remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Long"/>.</exception>
        Public Function op_Division(a As Long, b As Long) As Long
            Return a \ b
        End Function
        ''' <summary>Implements operator / for type <see cref="ULong"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> / <paramref name="b"/> (integral division)</remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="ULong"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Division(a As ULong, b As ULong) As ULong
            Return a \ b
        End Function
        ''' <summary>Implements operator / for type <see cref="Single"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> / <paramref name="b"/> (floating point division)</remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Single"/>.</exception>
        Public Function op_Division(a As Single, b As Single) As Single
            Return a / b
        End Function
        ''' <summary>Implements operator / for type <see cref="Double"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> / <paramref name="b"/> (floating point division)</remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Double"/>.</exception>
        Public Function op_Division(a As Double, b As Double) As Double
            Return a / b
        End Function
#End Region
#Region "^"
        ''' <summary>Implements operator power (VB ^) for type <see cref="SByte"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> powered on <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="SByte"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Exponent(a As SByte, b As SByte) As SByte
            Return a ^ b
        End Function
        ''' <summary>Implements operator power (VB ^) for type <see cref="Byte"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> powered on <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Byte"/>.</exception>
        Public Function op_Exponent(a As Byte, b As Byte) As Byte
            Return a ^ b
        End Function
        ''' <summary>Implements operator power (VB ^) for type <see cref="Short"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> powered on <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Short"/>.</exception>
        Public Function op_Exponent(a As Short, b As Short) As Short
            Return a ^ b
        End Function
        ''' <summary>Implements operator power (VB ^) for type <see cref="UShort"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> powered on <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="UShort"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Exponent(a As UShort, b As UShort) As UShort
            Return a ^ b
        End Function
        ''' <summary>Implements operator power (VB ^) for type <see cref="Integer"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> powered on <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Integer"/>.</exception>
        Public Function op_Exponent(a As Integer, b As Integer) As Integer
            Return a ^ b
        End Function
        ''' <summary>Implements operator power (VB ^) for type <see cref="UInteger"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> powered on <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="UInteger"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Exponent(a As UInteger, b As UInteger) As UInteger
            Return a ^ b
        End Function
        ''' <summary>Implements operator power (VB ^) for type <see cref="Long"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> powered on <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Long"/>.</exception>
        Public Function op_Exponent(a As Long, b As Long) As Long
            Return a ^ b
        End Function
        ''' <summary>Implements operator power (VB ^) for type <see cref="ULong"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> powered on <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="ULong"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Exponent(a As ULong, b As ULong) As ULong
            Return a ^ b
        End Function
        ''' <summary>Implements operator power (VB ^) for type <see cref="Single"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> powered on <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Single"/>.</exception>
        Public Function op_Exponent(a As Single, b As Single) As Single
            Return a ^ b
        End Function
        ''' <summary>Implements operator power (VB ^) for type <see cref="Double"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> powered on <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Double"/>.</exception>
        Public Function op_Exponent(a As Double, b As Double) As Double
            Return a ^ b
        End Function
#End Region
#Region "%"
        ''' <summary>Implements operator modulo (%) for type <see cref="SByte"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> modulo <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="SByte"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Modulus(a As SByte, b As SByte) As SByte
            Return a Mod b
        End Function
        ''' <summary>Implements operator modulo (%) for type <see cref="Byte"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> modulo <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Byte"/>.</exception>
        Public Function op_Modulus(a As Byte, b As Byte) As Byte
            Return a Mod b
        End Function
        ''' <summary>Implements operator modulo (%) for type <see cref="Short"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> modulo <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Short"/>.</exception>
        Public Function op_Modulus(a As Short, b As Short) As Short
            Return a Mod b
        End Function
        ''' <summary>Implements operator modulo (%) for type <see cref="UShort"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> modulo <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="UShort"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Modulus(a As UShort, b As UShort) As UShort
            Return a Mod b
        End Function
        ''' <summary>Implements operator modulo (%) for type <see cref="Integer"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> modulo <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Integer"/>.</exception>
        Public Function op_Modulus(a As Integer, b As Integer) As Integer
            Return a Mod b
        End Function
        ''' <summary>Implements operator modulo (%) for type <see cref="UInteger"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> modulo <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="UInteger"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Modulus(a As UInteger, b As UInteger) As UInteger
            Return a Mod b
        End Function
        ''' <summary>Implements operator modulo (%) for type <see cref="Long"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> modulo <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Long"/>.</exception>
        Public Function op_Modulus(a As Long, b As Long) As Long
            Return a Mod b
        End Function
        ''' <summary>Implements operator modulo (%) for type <see cref="ULong"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> modulo <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="ULong"/>.</exception>
        <CLSCompliant(False)>
        Public Function op_Modulus(a As ULong, b As ULong) As ULong
            Return a Mod b
        End Function
        ''' <summary>Implements operator modulo (%) for type <see cref="Single"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> modulo <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Single"/>.</exception>
        Public Function op_Modulus(a As Single, b As Single) As Single
            Return a Mod b
        End Function
        ''' <summary>Implements operator modulo (%) for type <see cref="Double"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks><paramref name="a"/> modulo <paramref name="b"/></remarks>
        ''' <exception cref="OverflowException">Result of operation is out of range of type <see cref="Double"/>.</exception>
        Public Function op_Modulus(a As Double, b As Double) As Double
            Return a Mod b
        End Function
#End Region
#Region "&"
        ''' <summary>Implements string concatenation operator (VB &amp;, PHP .) for type <see cref="String"/></summary>
        ''' <param name="a">1st operand</param>
        ''' <param name="b">2nd operand</param>
        ''' <remarks>Strings <paramref name="a"/> and <paramref name="b"/> concatenated</remarks>
        Public Function op_Concatenate(a As String, b As String) As String
            Return a & b
        End Function
#End Region

    End Module
End Namespace