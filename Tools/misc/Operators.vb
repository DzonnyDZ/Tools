Imports System.Runtime.InteropServices, Tools.ExtensionsT.Numbers

#If Config <= Nightly Then 'Stage: Nightly

''' <summary>Defines operator methods for basic CLR types</summary>
''' <version version="1.5.3" stage="Nightly">This module is new in version 1.5.3</version>
Public Module Operators 'TODO: Implement cross-type operators

    ''' <summary>Helper structure - represents union of <see cref="Char"/> and <see cref="UShort"/></summary>
    <StructLayout(LayoutKind.Explicit, CharSet:=CharSet.Unicode, Size:=2)>
    Private Structure CharHelper
        ''' <summary><see cref="Char"/> value</summary>
        <FieldOffset(0)> Public [char] As Char
        ''' <summary><see cref="UShort"/> value</summary>
        <FieldOffset(0)> Public [uShort] As UShort
        ''' <summary>CTor - creates a new instance of the <see cref="CharHelper"/> union from <see cref="Char"/> value</summary>
        Public Sub New(ByVal [char] As Char)
            Me.[char] = [char]
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="CharHelper"/> union from <see cref="UShort"/> value</summary>
        Public Sub New(ByVal [ushort] As UShort)
            Me.[uShort] = [ushort]
        End Sub
    End Structure
#Region "Unary"
#Region "Decrement"
    ''' <summary>Unary decrement operator (--)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is less than <see cref="System.Byte.MinValue"/></exception>
    Public Function op_Decrement(ByVal a As Byte) As Byte
        Return a - 1
    End Function
    ''' <summary>Unary decrement operator (--)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is less than <see cref="System.SByte.MinValue"/></exception>
    <CLSCompliant(False)>
    Public Function op_Decrement(ByVal a As SByte) As SByte
        Return a - 1
    End Function
    ''' <summary>Unary decrement operator (--)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is less than <see cref="System.Int16.MinValue"/></exception>
    Public Function op_Decrement(ByVal a As Short) As Short
        Return a - 1
    End Function
    ''' <summary>Unary decrement operator (--)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is less than <see cref="System.UInt16.MinValue"/></exception>
    <CLSCompliant(False)>
    Public Function op_Decrement(ByVal a As UShort) As UShort
        Return a - 1
    End Function
    ''' <summary>Unary decrement operator (--)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is less than <see cref="System.Int32.MinValue"/></exception>
    Public Function op_Decrement(ByVal a As Integer) As Integer
        Return a - 1
    End Function
    ''' <summary>Unary decrement operator (--)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is less than <see cref="System.UInt32.MinValue"/></exception>
    <CLSCompliant(False)>
    Public Function op_Decrement(ByVal a As UInteger) As UInteger
        Return a - 1
    End Function
    ''' <summary>Unary decrement operator (--)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is less than <see cref="System.Int64.MinValue"/></exception>
    Public Function op_Decrement(ByVal a As Long) As Long
        Return a - 1
    End Function
    ''' <summary>Unary decrement operator (--)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is less than <see cref="System.UInt64.MinValue"/></exception>
    <CLSCompliant(False)>
    Public Function op_Decrement(ByVal a As ULong) As ULong
        Return a - 1
    End Function
    ''' <summary>Unary decrement operator (--)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is less than <see cref="System.Single.MinValue"/></exception>
    Public Function op_Decrement(ByVal a As Single) As Single
        Return a - 1
    End Function
    ''' <summary>Unary decrement operator (--)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is less than <see cref="System.Double.MinValue"/></exception>
    Public Function op_Decrement(ByVal a As Double) As Double
        Return a - 1
    End Function
    ''' <summary>Unary decrement operator (--)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is less than <see cref="System.Char.MinValue"/></exception>
    Public Function op_Decrement(ByVal a As Char) As Char
        Dim helper As New CharHelper(a)
        helper.uShort -= 1
        Return helper.char
    End Function
#End Region

#Region "Increment"
    ''' <summary>Unary increment operator (++)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is greater than <see cref="System.Byte.MaxValue"/></exception>
    Public Function op_Increment(ByVal a As Byte) As Byte
        Return a + 1
    End Function
    ''' <summary>Unary increment operator (++)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is greater than <see cref="System.SByte.MaxValue"/></exception>
    <CLSCompliant(False)>
    Public Function op_Increment(ByVal a As SByte) As SByte
        Return a + 1
    End Function
    ''' <summary>Unary increment operator (++)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is greater than <see cref="System.Int16.MaxValue"/></exception>
    Public Function op_Increment(ByVal a As Short) As Short
        Return a + 1
    End Function
    ''' <summary>Unary increment operator (++)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is greater than <see cref="System.UInt16.MaxValue"/></exception>
    <CLSCompliant(False)>
    Public Function op_Increment(ByVal a As UShort) As UShort
        Return a + 1
    End Function
    ''' <summary>Unary increment operator (++)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is greater than <see cref="System.Int32.MaxValue"/></exception>
    Public Function op_Increment(ByVal a As Integer) As Integer
        Return a + 1
    End Function
    ''' <summary>Unary increment operator (++)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is greater than <see cref="System.UInt32.MaxValue"/></exception>
    <CLSCompliant(False)>
    Public Function op_Increment(ByVal a As UInteger) As UInteger
        Return a + 1
    End Function
    ''' <summary>Unary increment operator (++)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is greater than <see cref="System.Int64.MaxValue"/></exception>
    Public Function op_Increment(ByVal a As Long) As Long
        Return a + 1
    End Function
    ''' <summary>Unary increment operator (++)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is greater than <see cref="System.UInt64.MaxValue"/></exception>
    <CLSCompliant(False)>
    Public Function op_Increment(ByVal a As ULong) As ULong
        Return a + 1
    End Function
    ''' <summary>Unary increment operator (++)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    Public Function op_Increment(ByVal a As Single) As Single
        Return a + 1
    End Function
    ''' <summary>Unary increment operator (++)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    Public Function op_Increment(ByVal a As Double) As Double
        Return a + 1
    End Function
    ''' <summary>Unary increment operator (++)</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/> - 1</returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> - 1 is greater than <see cref="System.Char.MaxValue"/></exception>
    Public Function op_Increment(ByVal a As Char) As Char
        Dim helper As New CharHelper(a)
        helper.uShort += 1
        Return helper.char
    End Function
#End Region

#Region "UnaryNegation"
    ''' <summary>Unary negation operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>-<paramref name="a"/></returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> is not 0</exception>
    Public Function op_UnaryNegation(ByVal a As Byte) As Byte
        Return -a
    End Function
    ''' <summary>Unary negation operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>-<paramref name="a"/></returns>
    ''' <exception cref="OverflowException"> <paramref name="a"/> is <see cref="System.SByte.MinValue"/> (-128)</exception>
    <CLSCompliant(False)>
    Public Function op_UnaryNegation(ByVal a As SByte) As SByte
        Return -a
    End Function
    ''' <summary>Unary negation operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>-<paramref name="a"/></returns>
    ''' <exception cref="OverflowException"> <paramref name="a"/> is <see cref="System.Int16.MinValue"/> (-32768)</exception>
    Public Function op_UnaryNegation(ByVal a As Short) As Short
        Return -a
    End Function
    ''' <summary>Unary negation operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>-<paramref name="a"/></returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> is not 0</exception>    
    <CLSCompliant(False)>
    Public Function op_UnaryNegation(ByVal a As UShort) As UShort
        Return -a
    End Function
    ''' <summary>Unary negation operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>-<paramref name="a"/></returns>
    ''' <exception cref="OverflowException"> <paramref name="a"/> is <see cref="System.Int32.MinValue"/> (-2147483648)</exception>
    Public Function op_UnaryNegation(ByVal a As Integer) As Integer
        Return -a
    End Function
    ''' <summary>Unary negation operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>-<paramref name="a"/></returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> is not 0</exception>
    <CLSCompliant(False)>
    Public Function op_UnaryNegation(ByVal a As UInteger) As UInteger
        Return -a
    End Function
    ''' <summary>Unary negation operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>-<paramref name="a"/></returns>
    ''' <exception cref="OverflowException"> <paramref name="a"/> is <see cref="System.Int64.MinValue"/> (-9223372036854775808)</exception>
    Public Function op_UnaryNegation(ByVal a As Long) As Long
        Return -a
    End Function
    ''' <summary>Unary negation operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>-<paramref name="a"/></returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> is not 0</exception>
    <CLSCompliant(False)>
    Public Function op_UnaryNegation(ByVal a As ULong) As ULong
        Return -a
    End Function
    ''' <summary>Unary negation operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>-<paramref name="a"/></returns>
    Public Function op_UnaryNegation(ByVal a As Single) As Single
        Return -a
    End Function
    ''' <summary>Unary negation operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>-<paramref name="a"/></returns>
    Public Function op_UnaryNegation(ByVal a As Double) As Double
        Return -a
    End Function
    ''' <summary>Unary negation operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>-<paramref name="a"/></returns>
    ''' <exception cref="OverflowException"><paramref name="a"/> is not <see cref="Chars.NullChar"/></exception>
    Public Function op_UnaryNegation(ByVal a As Char) As Char
        Dim helper As New CharHelper(a)
        helper.uShort = -helper.uShort
        Return helper.char
    End Function
#End Region

#Region "UnaryPlus"
    ''' <summary>Unary plus operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>+<paramref name="a"/></returns>
    Public Function op_UnaryPlus(ByVal a As Byte) As Byte
        Return +a
    End Function
    ''' <summary>Unary plus operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>+<paramref name="a"/></returns>
    <CLSCompliant(False)>
    Public Function op_UnaryPlus(ByVal a As SByte) As SByte
        Return +a
    End Function
    ''' <summary>Unary plus operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>+<paramref name="a"/></returns>
    Public Function op_UnaryPlus(ByVal a As Short) As Short
        Return +a
    End Function
    ''' <summary>Unary plus operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>+<paramref name="a"/></returns>
    <CLSCompliant(False)>
    Public Function op_UnaryPlus(ByVal a As UShort) As UShort
        Return +a
    End Function
    ''' <summary>Unary plus operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>+<paramref name="a"/></returns>
    Public Function op_UnaryPlus(ByVal a As Integer) As Integer
        Return +a
    End Function
    ''' <summary>Unary plus operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>+<paramref name="a"/></returns>
    <CLSCompliant(False)>
    Public Function op_UnaryPlus(ByVal a As UInteger) As UInteger
        Return +a
    End Function
    ''' <summary>Unary plus operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>+<paramref name="a"/></returns>
    Public Function op_UnaryPlus(ByVal a As Long) As Long
        Return +a
    End Function
    ''' <summary>Unary plus operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>+<paramref name="a"/></returns>
    <CLSCompliant(False)>
    Public Function op_UnaryPlus(ByVal a As ULong) As ULong
        Return +a
    End Function
    ''' <summary>Unary plus operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>+<paramref name="a"/></returns>
    Public Function op_UnaryPlus(ByVal a As Single) As Single
        Return +a
    End Function
    ''' <summary>Unary plus operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>+<paramref name="a"/></returns>
    Public Function op_UnaryPlus(ByVal a As Double) As Double
        Return +a
    End Function
    ''' <summary>Unary plus operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>+<paramref name="a"/></returns>
    Public Function op_UnaryPlus(ByVal a As Char) As Char
        Dim helper As New CharHelper(a)
        helper.uShort = +helper.uShort
        Return helper.char
    End Function
#End Region

#Region "LogicalNot"
    ''' <summary>Logical not operator (!, Not)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>Not <paramref name="a"/></returns>
    Public Function op_LogicalNot(ByVal a As Boolean) As Boolean
        Return Not a
    End Function
#End Region

#Region "True"
    ''' <summary>IsTrue test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>True if <paramref name="a"/> represents logical value true, false otherwise</returns>
    Public Function op_True(ByVal a As Byte) As Boolean
        Return a
    End Function
    ''' <summary>IsTrue test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>True if <paramref name="a"/> represents logical value true, false otherwise</returns>
    <CLSCompliant(False)>
    Public Function op_True(ByVal a As SByte) As Boolean
        Return a
    End Function
    ''' <summary>IsTrue test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>True if <paramref name="a"/> represents logical value true, false otherwise</returns>
    Public Function op_True(ByVal a As Short) As Boolean
        Return a
    End Function
    ''' <summary>IsTrue test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>True if <paramref name="a"/> represents logical value true, false otherwise</returns>
    <CLSCompliant(False)>
    Public Function op_True(ByVal a As UShort) As Boolean
        Return a
    End Function
    ''' <summary>IsTrue test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>True if <paramref name="a"/> represents logical value true, false otherwise</returns>
    Public Function op_True(ByVal a As Integer) As Boolean
        Return a
    End Function
    ''' <summary>IsTrue test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>True if <paramref name="a"/> represents logical value true, false otherwise</returns>
    <CLSCompliant(False)>
    Public Function op_True(ByVal a As UInteger) As Boolean
        Return a
    End Function
    ''' <summary>IsTrue test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>True if <paramref name="a"/> represents logical value true, false otherwise</returns>
    Public Function op_True(ByVal a As Long) As Boolean
        Return a
    End Function
    ''' <summary>IsTrue test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>True if <paramref name="a"/> represents logical value true, false otherwise</returns>
    <CLSCompliant(False)>
    Public Function op_True(ByVal a As ULong) As Boolean
        Return a
    End Function
    ''' <summary>IsTrue test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>True if <paramref name="a"/> represents logical value true, false otherwise</returns>
    Public Function op_True(ByVal a As Single) As Boolean
        Return a
    End Function
    ''' <summary>IsTrue test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>True if <paramref name="a"/> represents logical value true, false otherwise</returns>
    Public Function op_True(ByVal a As Double) As Boolean
        Return a
    End Function
    ''' <summary>IsTrue test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>True if <paramref name="a"/> represents logical value true, false otherwise</returns>
    Public Function op_True(ByVal a As Char) As Boolean
        Return a <> Chars.NullChar
    End Function
    ''' <summary>IsTrue test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/></returns>
    Public Function op_True(ByVal a As Boolean) As Boolean
        Return a
    End Function
    ''' <summary>IsTrue test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/></returns>
    Public Function op_True(ByVal a As String) As Boolean
        Return a <> ""
    End Function
#End Region

#Region "False"
    ''' <summary>IsFalse test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>False if <paramref name="a"/> represents logical value false, true otherwise</returns>
    Public Function op_False(ByVal a As Byte) As Boolean
        Return Not op_True(a)
    End Function
    ''' <summary>IsFalse test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>False if <paramref name="a"/> represents logical value false, true otherwise</returns>
    <CLSCompliant(False)>
    Public Function op_False(ByVal a As SByte) As Boolean
        Return Not op_True(a)
    End Function
    ''' <summary>IsFalse test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>False if <paramref name="a"/> represents logical value false, true otherwise</returns>
    Public Function op_False(ByVal a As Short) As Boolean
        Return Not op_True(a)
    End Function
    ''' <summary>IsFalse test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>False if <paramref name="a"/> represents logical value false, true otherwise</returns>
    <CLSCompliant(False)>
    Public Function op_False(ByVal a As UShort) As Boolean
        Return Not op_True(a)
    End Function
    ''' <summary>IsFalse test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>False if <paramref name="a"/> represents logical value false, true otherwise</returns>
    Public Function op_False(ByVal a As Integer) As Boolean
        Return Not op_True(a)
    End Function
    ''' <summary>IsFalse test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>False if <paramref name="a"/> represents logical value false, true otherwise</returns>
    <CLSCompliant(False)>
    Public Function op_False(ByVal a As UInteger) As Boolean
        Return Not op_True(a)
    End Function
    ''' <summary>IsFalse test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>False if <paramref name="a"/> represents logical value false, true otherwise</returns>
    Public Function op_False(ByVal a As Long) As Boolean
        Return Not op_True(a)
    End Function
    ''' <summary>IsFalse test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>False if <paramref name="a"/> represents logical value false, true otherwise</returns>
    <CLSCompliant(False)>
    Public Function op_False(ByVal a As ULong) As Boolean
        Return Not op_True(a)
    End Function
    ''' <summary>IsFalse test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>False if <paramref name="a"/> represents logical value false, true otherwise</returns>
    Public Function op_False(ByVal a As Single) As Boolean
        Return Not op_True(a)
    End Function
    ''' <summary>IsFalse test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>False if <paramref name="a"/> represents logical value false, true otherwise</returns>
    Public Function op_False(ByVal a As Double) As Boolean
        Return Not op_True(a)
    End Function
    ''' <summary>IsFalse test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns>False if <paramref name="a"/> represents logical value false, true otherwise</returns>
    Public Function op_False(ByVal a As Char) As Boolean
        Return Not op_True(a)
    End Function
    ''' <summary>IsFalse test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/></returns>
    Public Function op_False(ByVal a As Boolean) As Boolean
        Return Not op_True(a)
    End Function

    ''' <summary>IsFalse test operator</summary>
    ''' <param name="a">A value</param>
    ''' <returns><paramref name="a"/></returns>
    Public Function op_False(ByVal a As String) As Boolean
        Return Not op_True(a)
    End Function
#End Region

#Region "OnesComplement"
    ''' <summary>One's complement operator (binary not; Not, ~)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>One's complement of <paramref name="a"/></returns>
    Public Function op_OnesComplement(ByVal a As Byte) As Byte
        Return Not a
    End Function
    ''' <summary>One's complement operator (binary not; Not, ~)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>One's complement of <paramref name="a"/></returns>
    <CLSCompliant(False)>
    Public Function op_OnesComplement(ByVal a As SByte) As SByte
        Return Not a
    End Function
    ''' <summary>One's complement operator (binary not; Not, ~)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>One's complement of <paramref name="a"/></returns>
    Public Function op_OnesComplement(ByVal a As Short) As Short
        Return Not a
    End Function
    ''' <summary>One's complement operator (binary not; Not, ~)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>One's complement of <paramref name="a"/></returns>
    <CLSCompliant(False)>
    Public Function op_OnesComplement(ByVal a As UShort) As UShort
        Return Not a
    End Function
    ''' <summary>One's complement operator (binary not; Not, ~)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>One's complement of <paramref name="a"/></returns>
    Public Function op_OnesComplement(ByVal a As Integer) As Integer
        Return Not a
    End Function
    ''' <summary>One's complement operator (binary not; Not, ~)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>One's complement of <paramref name="a"/></returns>
    <CLSCompliant(False)>
    Public Function op_OnesComplement(ByVal a As UInteger) As UInteger
        Return Not a
    End Function
    ''' <summary>One's complement operator (binary not; Not, ~)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>One's complement of <paramref name="a"/></returns>
    Public Function op_OnesComplement(ByVal a As Long) As Long
        Return Not a
    End Function
    ''' <summary>One's complement operator (binary not; Not, ~)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>One's complement of <paramref name="a"/></returns>
    <CLSCompliant(False)>
    Public Function op_OnesComplement(ByVal a As ULong) As ULong
        Return Not a
    End Function
    ''' <summary>One's complement operator (binary not; Not, ~)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>One's complement of <paramref name="a"/></returns>
    Public Function op_OnesComplement(ByVal a As Char) As Char
        Dim helper As New CharHelper(a)
        helper.uShort = Not helper.uShort
        Return helper.char
    End Function
    ''' <summary>One's complement operator (binary not; Not, ~)</summary>
    ''' <param name="a">A value</param>
    ''' <returns>One's complement of <paramref name="a"/></returns>
    Public Function op_OnesComplement(ByVal a As Boolean) As Boolean
        Return Not a
    End Function
#End Region
#End Region

#Region "Binary - single type"
#Region "Addition"
    ''' <summary>Addition (plus) operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> + <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.Byte.MaxValue"/>.</exception>
    Public Function op_Addition(ByVal a As Byte, ByVal b As Byte) As Byte
        Return a + b
    End Function
    ''' <summary>Addition (plus) operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> + <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.SByte.MaxValue"/> or less than <see cref="System.SByte.MinValue"/>.</exception>
    <CLSCompliant(False)>
    Public Function op_Addition(ByVal a As SByte, ByVal b As SByte) As SByte
        Return a + b
    End Function
    ''' <summary>Addition (plus) operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> + <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.Int16.MaxValue"/> or less than <see cref="System.Int16.MinValue"/>.</exception>
    Public Function op_Addition(ByVal a As Short, ByVal b As Short) As Short
        Return a + b
    End Function
    ''' <summary>Addition (plus) operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> + <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.UInt16.MaxValue"/>.</exception>
    <CLSCompliant(False)>
    Public Function op_Addition(ByVal a As UShort, ByVal b As UShort) As UShort
        Return a + b
    End Function
    ''' <summary>Addition (plus) operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> + <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.Int32.MaxValue"/> or less than <see cref="System.Int32.MinValue"/>.</exception>
    Public Function op_Addition(ByVal a As Integer, ByVal b As Integer) As Integer
        Return a + b
    End Function
    ''' <summary>Addition (plus) operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> + <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.UInt32.MaxValue"/>.</exception>
    <CLSCompliant(False)>
    Public Function op_Addition(ByVal a As UInteger, ByVal b As UInteger) As UInteger
        Return a + b
    End Function
    ''' <summary>Addition (plus) operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> + <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.Int64.MaxValue"/> or less than <see cref="System.Int64.MinValue"/>.</exception>
    Public Function op_Addition(ByVal a As Long, ByVal b As Long) As Long
        Return a + b
    End Function
    ''' <summary>Addition (plus) operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> + <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.UInt64.MaxValue"/>.</exception>
    <CLSCompliant(False)>
    Public Function op_Addition(ByVal a As ULong, ByVal b As ULong) As ULong
        Return a + b
    End Function
    ''' <summary>Addition (plus) operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> + <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.Single.MaxValue"/> or less than <see cref="System.Single.MinValue"/>.</exception>
    Public Function op_Addition(ByVal a As Single, ByVal b As Single) As Single
        Return a + b
    End Function
    ''' <summary>Addition (plus) operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> + <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.Double.MaxValue"/> or less than <see cref="System.Double.MinValue"/>.</exception>
    Public Function op_Addition(ByVal a As Double, ByVal b As Double) As Double
        Return a + b
    End Function
    ''' <summary>Addition (plus) operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> + <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.Char.MaxValue"/>.</exception>
    Public Function op_Addition(ByVal a As Char, ByVal b As Char) As Char
        Dim ha As New CharHelper(a), hb As New CharHelper(b)
        ha.uShort += hb.uShort
        Return ha.char
    End Function
    ''' <summary>Addition (plus) operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> + <paramref name="b"/></returns>
    Public Function op_Addition(ByVal a As Boolean, ByVal b As Boolean) As Boolean
        Return a + b
    End Function

    ''' <summary>Addition (plus) operator (+)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> + <paramref name="b"/></returns>
    Public Function op_Addition(ByVal a As String, ByVal b As String) As String
        Return a + b
    End Function
#End Region

#Region "Subtraction"
    ''' <summary>Subtraction (minus) operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> - <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is less than <see cref="System.Byte.MinValue"/>.</exception>
    Public Function op_Subtraction(ByVal a As Byte, ByVal b As Byte) As Byte
        Return a - b
    End Function
    ''' <summary>Subtraction (minus) operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> - <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is less than <see cref="System.SByte.MinValue"/> or greater than <see cref="System.SByte.MaxValue"/>.</exception>
    <CLSCompliant(False)>
    Public Function op_Subtraction(ByVal a As SByte, ByVal b As SByte) As SByte
        Return a - b
    End Function
    ''' <summary>Subtraction (minus) operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> - <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is less than <see cref="System.Int16.MinValue"/> or greater than <see cref="System.Int16.MaxValue"/>..</exception>
    Public Function op_Subtraction(ByVal a As Short, ByVal b As Short) As Short
        Return a - b
    End Function
    ''' <summary>Subtraction (minus) operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> - <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is less than <see cref="System.UInt16.MinValue"/>.</exception>
    <CLSCompliant(False)>
    Public Function op_Subtraction(ByVal a As UShort, ByVal b As UShort) As UShort
        Return a - b
    End Function
    ''' <summary>Subtraction (minus) operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> - <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is less than <see cref="System.Int32.MinValue"/> or greater than <see cref="System.Int32.MaxValue"/>..</exception>
    Public Function op_Subtraction(ByVal a As Integer, ByVal b As Integer) As Integer
        Return a - b
    End Function
    ''' <summary>Subtraction (minus) operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> - <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is less than <see cref="System.UInt32.MinValue"/>.</exception>
    <CLSCompliant(False)>
    Public Function op_Subtraction(ByVal a As UInteger, ByVal b As UInteger) As UInteger
        Return a - b
    End Function
    ''' <summary>Subtraction (minus) operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> - <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is less than <see cref="System.Int64.MinValue"/> or greater than <see cref="System.Int64.MaxValue"/>..</exception>
    Public Function op_Subtraction(ByVal a As Long, ByVal b As Long) As Long
        Return a - b
    End Function
    ''' <summary>Subtraction (minus) operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> - <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is less than <see cref="System.UInt64.MinValue"/>.</exception>
    <CLSCompliant(False)>
    Public Function op_Subtraction(ByVal a As ULong, ByVal b As ULong) As ULong
        Return a - b
    End Function
    ''' <summary>Subtraction (minus) operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> - <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is less than <see cref="System.Single.MinValue"/> or greater than <see cref="System.Single.MaxValue"/>..</exception>
    Public Function op_Subtraction(ByVal a As Single, ByVal b As Single) As Single
        Return a - b
    End Function
    ''' <summary>Subtraction (minus) operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> - <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is less than <see cref="System.Double.MinValue"/>  or greater than <see cref="System.Double.MaxValue"/>..</exception>
    Public Function op_Subtraction(ByVal a As Double, ByVal b As Double) As Double
        Return a - b
    End Function
    ''' <summary>Subtraction (minus) operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> - <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is less than <see cref="System.Char.MinValue"/>.</exception>
    Public Function op_Subtraction(ByVal a As Char, ByVal b As Char) As Char
        Dim ha As New CharHelper(a), hb As New CharHelper(b)
        ha.uShort -= hb.uShort
        Return ha.char
    End Function
    ''' <summary>Subtraction (minus) operator (-)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> - <paramref name="b"/></returns>
    Public Function op_Subtraction(ByVal a As Boolean, ByVal b As Boolean) As Boolean
        Return a - b
    End Function
#End Region

#Region "Multiply"
    ''' <summary>Multiply operator (*)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> * <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.Byte.MaxValue"/>.</exception>
    Public Function op_Multiply(ByVal a As Byte, ByVal b As Byte) As Byte
        Return a * b
    End Function
    ''' <summary>Multiply operator (*)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> * <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.SByte.MaxValue"/> or less than <see cref="System.SByte.MinValue"/>.</exception>
    <CLSCompliant(False)>
    Public Function op_Multiply(ByVal a As SByte, ByVal b As SByte) As SByte
        Return a * b
    End Function
    ''' <summary>Multiply operator (*)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> * <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.Int16.MaxValue"/> or less than <see cref="System.Int16.MinValue"/>.</exception>
    Public Function op_Multiply(ByVal a As Short, ByVal b As Short) As Short
        Return a * b
    End Function
    ''' <summary>Multiply operator (*)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> * <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.UInt16.MaxValue"/>.</exception>
    <CLSCompliant(False)>
    Public Function op_Multiply(ByVal a As UShort, ByVal b As UShort) As UShort
        Return a * b
    End Function
    ''' <summary>Multiply operator (*)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> * <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.Int32.MaxValue"/> or less than <see cref="System.Int32.MinValue"/>.</exception>
    Public Function op_Multiply(ByVal a As Integer, ByVal b As Integer) As Integer
        Return a * b
    End Function
    ''' <summary>Multiply operator (*)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> * <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.UInt32.MaxValue"/>.</exception>
    <CLSCompliant(False)>
    Public Function op_Multiply(ByVal a As UInteger, ByVal b As UInteger) As UInteger
        Return a * b
    End Function
    ''' <summary>Multiply operator (*)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> * <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.Int64.MaxValue"/> or less than <see cref="System.Int64.MinValue"/>.</exception>
    Public Function op_Multiply(ByVal a As Long, ByVal b As Long) As Long
        Return a * b
    End Function
    ''' <summary>Multiply operator (*)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> * <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.UInt64.MaxValue"/>.</exception>
    <CLSCompliant(False)>
    Public Function op_Multiply(ByVal a As ULong, ByVal b As ULong) As ULong
        Return a * b
    End Function
    ''' <summary>Multiply operator (*)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> * <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.Single.MaxValue"/> or less than <see cref="System.Single.MinValue"/>.</exception>
    Public Function op_Multiply(ByVal a As Single, ByVal b As Single) As Single
        Return a * b
    End Function
    ''' <summary>Multiply operator (*)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> * <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.Double.MaxValue"/> or less than <see cref="System.Double.MinValue"/>.</exception>
    Public Function op_Multiply(ByVal a As Double, ByVal b As Double) As Double
        Return a * b
    End Function
    ''' <summary>Multiply operator (*)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> * <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.Char.MaxValue"/>.</exception>
    Public Function op_Multiply(ByVal a As Char, ByVal b As Char) As Char
        Dim ha As New CharHelper(a), hb As New CharHelper(b)
        ha.uShort *= hb.uShort
        Return ha.char
    End Function
    ''' <summary>Multiply operator (*)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> * <paramref name="b"/></returns>
    Public Function op_Multiply(ByVal a As Boolean, ByVal b As Boolean) As Boolean
        Return a * b
    End Function
#End Region

#Region "Division"
    ''' <summary>Division operator (/; integral)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> / <paramref name="b"/></returns>
    Public Function op_Division(ByVal a As Byte, ByVal b As Byte) As Byte
        Return a \ b
    End Function
    ''' <summary>Division operator (/; integral)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> / <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_Division(ByVal a As SByte, ByVal b As SByte) As SByte
        Return a \ b
    End Function
    ''' <summary>Division operator (/; integral)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> / <paramref name="b"/></returns>
    Public Function op_Division(ByVal a As Short, ByVal b As Short) As Short
        Return a \ b
    End Function
    ''' <summary>Division operator (/; integral)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> / <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.UInt16.MaxValue"/>.</exception>
    <CLSCompliant(False)>
    Public Function op_Division(ByVal a As UShort, ByVal b As UShort) As UShort
        Return a \ b
    End Function
    ''' <summary>Division operator (/; integral)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> / <paramref name="b"/></returns>
    Public Function op_Division(ByVal a As Integer, ByVal b As Integer) As Integer
        Return a \ b
    End Function
    ''' <summary>Division operator (/; integral)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> / <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_Division(ByVal a As UInteger, ByVal b As UInteger) As UInteger
        Return a \ b
    End Function
    ''' <summary>Division operator (/; integral)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> / <paramref name="b"/></returns>
    Public Function op_Division(ByVal a As Long, ByVal b As Long) As Long
        Return a \ b
    End Function
    ''' <summary>Division operator (/; integral)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> / <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_Division(ByVal a As ULong, ByVal b As ULong) As ULong
        Return a \ b
    End Function
    ''' <summary>Division operator (/)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> / <paramref name="b"/></returns>
    Public Function op_Division(ByVal a As Single, ByVal b As Single) As Single
        Return a / b
    End Function
    ''' <summary>Division operator (/)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> / <paramref name="b"/></returns>
    Public Function op_Division(ByVal a As Double, ByVal b As Double) As Double
        Return a / b
    End Function
    ''' <summary>Division operator (/; integral)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> / <paramref name="b"/></returns>
    Public Function op_Division(ByVal a As Char, ByVal b As Char) As Char
        Dim ha As New CharHelper(a), hb As New CharHelper(b)
        ha.uShort \= hb.uShort
        Return ha.char
    End Function
    ''' <summary>Division operator (/; integral)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> / <paramref name="b"/></returns>
    Public Function op_Division(ByVal a As Boolean, ByVal b As Boolean) As Boolean
        Return a \ b
    End Function
#End Region

#Region "Modulus"
    ''' <summary>Modulus (plus) operator (%, Mod)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> mod <paramref name="b"/></returns>
    Public Function op_Modulus(ByVal a As Byte, ByVal b As Byte) As Byte
        Return a Mod b
    End Function
    ''' <summary>Modulus (plus) operator (%, Mod)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> mod <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_Modulus(ByVal a As SByte, ByVal b As SByte) As SByte
        Return a Mod b
    End Function
    ''' <summary>Modulus (plus) operator (%, Mod)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> mod <paramref name="b"/></returns>
    Public Function op_Modulus(ByVal a As Short, ByVal b As Short) As Short
        Return a Mod b
    End Function
    ''' <summary>Modulus (plus) operator (%, Mod)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> mod <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_Modulus(ByVal a As UShort, ByVal b As UShort) As UShort
        Return a Mod b
    End Function
    ''' <summary>Modulus (plus) operator (%, Mod)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> mod <paramref name="b"/></returns>
    Public Function op_Modulus(ByVal a As Integer, ByVal b As Integer) As Integer
        Return a Mod b
    End Function
    ''' <summary>Modulus (plus) operator (%, Mod)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> mod <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_Modulus(ByVal a As UInteger, ByVal b As UInteger) As UInteger
        Return a Mod b
    End Function
    ''' <summary>Modulus (plus) operator (%, Mod)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> mod <paramref name="b"/></returns>
    Public Function op_Modulus(ByVal a As Long, ByVal b As Long) As Long
        Return a Mod b
    End Function
    ''' <summary>Modulus (plus) operator (%, Mod)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> mod <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_Modulus(ByVal a As ULong, ByVal b As ULong) As ULong
        Return a Mod b
    End Function
    ''' <summary>Modulus (plus) operator (%, Mod)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> mod <paramref name="b"/></returns>
    ''' <exception cref="OverflowException">Resulting value is greater than <see cref="System.Single.MaxValue"/> or less than <see cref="System.Single.MinValue"/>.</exception>
    Public Function op_Modulus(ByVal a As Single, ByVal b As Single) As Single
        Return a Mod b
    End Function
    ''' <summary>Modulus (plus) operator (%, Mod)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> mod <paramref name="b"/></returns>
    Public Function op_Modulus(ByVal a As Double, ByVal b As Double) As Double
        Return a Mod b
    End Function
    ''' <summary>Modulus (plus) operator (%, Mod)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> mod <paramref name="b"/></returns>
    Public Function op_Modulus(ByVal a As Char, ByVal b As Char) As Char
        Dim ha As New CharHelper(a), hb As New CharHelper(b)
        ha.uShort = hb.uShort Mod hb.uShort
        Return ha.char
    End Function
    ''' <summary>Modulus (plus) operator (%, Mod)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> mod <paramref name="b"/></returns>
    Public Function op_Modulus(ByVal a As Boolean, ByVal b As Boolean) As Boolean
        Return a Mod b
    End Function
#End Region

#Region "ExclusiveOr"
    ''' <summary>Binary exclusive or operator (Xor, ^)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> xor <paramref name="b"/></returns>
    Public Function op_ExclusiveOr(ByVal a As Byte, ByVal b As Byte) As Byte
        Return a Xor b
    End Function
    ''' <summary>Binary exclusive or operator (Xor, ^)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> xor <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_ExclusiveOr(ByVal a As SByte, ByVal b As SByte) As SByte
        Return a Xor b
    End Function
    ''' <summary>Binary exclusive or operator (Xor, ^)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> xor <paramref name="b"/></returns>
    Public Function op_ExclusiveOr(ByVal a As Short, ByVal b As Short) As Short
        Return a Xor b
    End Function
    ''' <summary>Binary exclusive or operator (Xor, ^)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> xor <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_ExclusiveOr(ByVal a As UShort, ByVal b As UShort) As UShort
        Return a Xor b
    End Function
    ''' <summary>Binary exclusive or operator (Xor, ^)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> xor <paramref name="b"/></returns>
    Public Function op_ExclusiveOr(ByVal a As Integer, ByVal b As Integer) As Integer
        Return a Xor b
    End Function
    ''' <summary>Binary exclusive or operator (Xor, ^)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> xor <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_ExclusiveOr(ByVal a As UInteger, ByVal b As UInteger) As UInteger
        Return a Xor b
    End Function
    ''' <summary>Binary exclusive or operator (Xor, ^)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> xor <paramref name="b"/></returns>
    Public Function op_ExclusiveOr(ByVal a As Long, ByVal b As Long) As Long
        Return a Xor b
    End Function
    ''' <summary>Binary exclusive or operator (Xor, ^)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> xor <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_ExclusiveOr(ByVal a As ULong, ByVal b As ULong) As ULong
        Return a Xor b
    End Function

    ''' <summary>Binary exclusive or operator (Xor, ^)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> xor <paramref name="b"/></returns>
    Public Function op_ExclusiveOr(ByVal a As Char, ByVal b As Char) As Char
        Dim ha As New CharHelper(a), hb As New CharHelper(b)
        ha.uShort = ha.uShort Xor hb.uShort
        Return ha.char
    End Function
    ''' <summary>Boolean exclusive or operator (Xor, ^)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> xor <paramref name="b"/></returns>
    Public Function op_ExclusiveOr(ByVal a As Boolean, ByVal b As Boolean) As Boolean
        Return a Xor b
    End Function
#End Region

#Region "BitwiseAnd"
    ''' <summary>Bitwise And operator (And, &amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    Public Function op_BitwiseAnd(ByVal a As Byte, ByVal b As Byte) As Byte
        Return a And b
    End Function
    ''' <summary>Bitwise And operator (And, &amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_BitwiseAnd(ByVal a As SByte, ByVal b As SByte) As SByte
        Return a And b
    End Function
    ''' <summary>Bitwise And operator (And, &amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    Public Function op_BitwiseAnd(ByVal a As Short, ByVal b As Short) As Short
        Return a And b
    End Function
    ''' <summary>Bitwise And operator (And, &amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_BitwiseAnd(ByVal a As UShort, ByVal b As UShort) As UShort
        Return a And b
    End Function
    ''' <summary>Bitwise And operator (And, &amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    Public Function op_BitwiseAnd(ByVal a As Integer, ByVal b As Integer) As Integer
        Return a And b
    End Function
    ''' <summary>Bitwise And operator (And, &amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_BitwiseAnd(ByVal a As UInteger, ByVal b As UInteger) As UInteger
        Return a And b
    End Function
    ''' <summary>Bitwise And operator (And, &amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    Public Function op_BitwiseAnd(ByVal a As Long, ByVal b As Long) As Long
        Return a And b
    End Function
    ''' <summary>Bitwise And operator (And, &amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_BitwiseAnd(ByVal a As ULong, ByVal b As ULong) As ULong
        Return a And b
    End Function

    ''' <summary>Bitwise And operator (And, &amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    Public Function op_BitwiseAnd(ByVal a As Char, ByVal b As Char) As Char
        Dim ha As New CharHelper(a), hb As New CharHelper(b)
        ha.uShort = ha.uShort And hb.uShort
        Return ha.char
    End Function
    ''' <summary>Boolean And operator (And, ^)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    Public Function op_BitwiseAnd(ByVal a As Boolean, ByVal b As Boolean) As Boolean
        Return a And b
    End Function
#End Region

#Region "BitwiseOr"
    ''' <summary>Bitwise Or operator (Or, |)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    Public Function op_BitwiseOr(ByVal a As Byte, ByVal b As Byte) As Byte
        Return a Or b
    End Function
    ''' <summary>Bitwise Or operator (Or, |)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_BitwiseOr(ByVal a As SByte, ByVal b As SByte) As SByte
        Return a Or b
    End Function
    ''' <summary>Bitwise Or operator (Or, |)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    Public Function op_BitwiseOr(ByVal a As Short, ByVal b As Short) As Short
        Return a Or b
    End Function
    ''' <summary>Bitwise Or operator (Or, |)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_BitwiseOr(ByVal a As UShort, ByVal b As UShort) As UShort
        Return a Or b
    End Function
    ''' <summary>Bitwise Or operator (Or, |)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    Public Function op_BitwiseOr(ByVal a As Integer, ByVal b As Integer) As Integer
        Return a Or b
    End Function
    ''' <summary>Bitwise Or operator (Or, |)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_BitwiseOr(ByVal a As UInteger, ByVal b As UInteger) As UInteger
        Return a Or b
    End Function
    ''' <summary>Bitwise Or operator (Or, |)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    Public Function op_BitwiseOr(ByVal a As Long, ByVal b As Long) As Long
        Return a Or b
    End Function
    ''' <summary>Bitwise Or operator (Or, |)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_BitwiseOr(ByVal a As ULong, ByVal b As ULong) As ULong
        Return a Or b
    End Function

    ''' <summary>Bitwise Or operator (Or, |)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    Public Function op_BitwiseOr(ByVal a As Char, ByVal b As Char) As Char
        Dim ha As New CharHelper(a), hb As New CharHelper(b)
        ha.uShort = ha.uShort Or hb.uShort
        Return ha.char
    End Function
    ''' <summary>Boolean Or operator (Or, |)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    Public Function op_BitwiseOr(ByVal a As Boolean, ByVal b As Boolean) As Boolean
        Return a Or b
    End Function
#End Region

#Region "LogicalAnd"
    ''' <summary>Logical And operator (And, &amp;&amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    Public Function op_LogicalAnd(ByVal a As Byte, ByVal b As Byte) As Boolean
        Return a AndAlso b
    End Function
    ''' <summary>Logical And operator (And, &amp;&amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LogicalAnd(ByVal a As SByte, ByVal b As SByte) As Boolean
        Return a AndAlso b
    End Function
    ''' <summary>Logical And operator (And, &amp;&amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    Public Function op_LogicalAnd(ByVal a As Short, ByVal b As Short) As Boolean
        Return a AndAlso b
    End Function
    ''' <summary>Logical And operator (And, &amp;&amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LogicalAnd(ByVal a As UShort, ByVal b As UShort) As Boolean
        Return a AndAlso b
    End Function
    ''' <summary>Logical And operator (And, &amp;&amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    Public Function op_LogicalAnd(ByVal a As Integer, ByVal b As Integer) As Boolean
        Return a AndAlso b
    End Function
    ''' <summary>Logical And operator (And, &amp;&amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LogicalAnd(ByVal a As UInteger, ByVal b As UInteger) As Boolean
        Return a AndAlso b
    End Function
    ''' <summary>Logical And operator (And, &amp;&amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    Public Function op_LogicalAnd(ByVal a As Long, ByVal b As Long) As Boolean
        Return a AndAlso b
    End Function
    ''' <summary>Logical And operator (And, &amp;&amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LogicalAnd(ByVal a As ULong, ByVal b As ULong) As Boolean
        Return a AndAlso b
    End Function
    ''' <summary>Logical And operatAnd (And, &amp;&amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LogicalAnd(ByVal a As Single, ByVal b As Single) As Boolean
        Return a OrElse b
    End Function

    ''' <summary>Logical And operatAnd (And, &amp;&amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LogicalAnd(ByVal a As Double, ByVal b As Double) As Boolean
        Return a OrElse b
    End Function
    ''' <summary>Logical And operator (And, &amp;&amp;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    Public Function op_LogicalAnd(ByVal a As Char, ByVal b As Char) As Boolean
        Dim ha As New CharHelper(a), hb As New CharHelper(b)
        Return ha.uShort AndAlso hb.uShort
    End Function
    ''' <summary>Boolean And operator (And, ^)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> And <paramref name="b"/></returns>
    Public Function op_LogicalAnd(ByVal a As Boolean, ByVal b As Boolean) As Boolean
        Return a AndAlso b
    End Function
#End Region

#Region "LogicalOr"
    ''' <summary>Logical Or operator (Or, ||)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    Public Function op_LogicalOr(ByVal a As Byte, ByVal b As Byte) As Boolean
        Return a OrElse b
    End Function
    ''' <summary>Logical Or operator (Or, ||)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LogicalOr(ByVal a As SByte, ByVal b As SByte) As Boolean
        Return a OrElse b
    End Function
    ''' <summary>Logical Or operator (Or, ||)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    Public Function op_LogicalOr(ByVal a As Short, ByVal b As Short) As Boolean
        Return a OrElse b
    End Function
    ''' <summary>Logical Or operator (Or, ||)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LogicalOr(ByVal a As UShort, ByVal b As UShort) As Boolean
        Return a OrElse b
    End Function
    ''' <summary>Logical Or operator (Or, ||)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    Public Function op_LogicalOr(ByVal a As Integer, ByVal b As Integer) As Boolean
        Return a OrElse b
    End Function
    ''' <summary>Logical Or operator (Or, ||)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LogicalOr(ByVal a As UInteger, ByVal b As UInteger) As Boolean
        Return a OrElse b
    End Function
    ''' <summary>Logical Or operator (Or, ||)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    Public Function op_LogicalOr(ByVal a As Long, ByVal b As Long) As Boolean
        Return a OrElse b
    End Function
    ''' <summary>Logical Or operator (Or, ||)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LogicalOr(ByVal a As Single, ByVal b As Single) As Boolean
        Return a OrElse b
    End Function

    ''' <summary>Logical Or operator (Or, ||)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LogicalOr(ByVal a As Double, ByVal b As Double) As Boolean
        Return a OrElse b
    End Function

    ''' <summary>Logical Or operator (Or, ||)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LogicalOr(ByVal a As ULong, ByVal b As ULong) As Boolean
        Return a OrElse b
    End Function

    ''' <summary>Logical Or operator (Or, ||)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    Public Function op_LogicalOr(ByVal a As Char, ByVal b As Char) As Boolean
        Dim ha As New CharHelper(a), hb As New CharHelper(b)
        Return ha.uShort OrElse hb.uShort
    End Function
    ''' <summary>Boolean Or operator (Or, ^)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> Or <paramref name="b"/></returns>
    Public Function op_LogicalOr(ByVal a As Boolean, ByVal b As Boolean) As Boolean
        Return a OrElse b
    End Function
#End Region

#Region "Assign"
    ''' <summary>Assignment operator (=, &lt;-)</summary>
    ''' <param name="a">Target of assignment. When function returns this parameter contains value of <paramref name="b"/>.</param>
    ''' <param name="b">Source of assignment</param>
    ''' <typeparam name="T">Type of values</typeparam>
    ''' <returns>Assigned value (<paramref name="b"/>)</returns>
    Public Function op_Assign(Of T)(<Out()> ByRef a As T, ByVal b As T) As T
        a = b
        Return a
    End Function
#End Region

#Region "LeftShift"
    ''' <summary>Left shift operator (&lt;&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> &lt;&lt; <paramref name="b"/></returns>
    Public Function op_LeftShift(ByVal a As Byte, ByVal b As Byte) As Byte
        If b > 7 Then Return 0
        Return a << b
    End Function
    ''' <summary>Left shift operator (&lt;&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> &lt;&lt; <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LeftShift(ByVal a As SByte, ByVal b As SByte) As SByte
        If b < 0 Then Return op_RightShift(a, -b)
        If b > 7 Then Return 0
        Return a << b
    End Function
    ''' <summary>Left shift operator (&lt;&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> &lt;&lt; <paramref name="b"/></returns>
    Public Function op_LeftShift(ByVal a As Short, ByVal b As Short) As Short
        If b < 0 Then Return op_RightShift(a, -b)
        If b > 15 Then Return 0
        Return a << b
    End Function
    ''' <summary>Left shift operator (&lt;&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> &lt;&lt; <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LeftShift(ByVal a As UShort, ByVal b As UShort) As UShort
        If b > 15 Then Return 0
        Return a << b
    End Function
    ''' <summary>Left shift operator (&lt;&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> &lt;&lt; <paramref name="b"/></returns>
    Public Function op_LeftShift(ByVal a As Integer, ByVal b As Integer) As Integer
        If b < 0 Then Return op_RightShift(a, -b)
        If b > 31 Then Return 0
        Return a << b
    End Function
    ''' <summary>Left shift operator (&lt;&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> &lt;&lt; <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LeftShift(ByVal a As UInteger, ByVal b As UInteger) As UInteger
        If b > 31 Then Return 0
        Return a << b
    End Function
    ''' <summary>Left shift operator (&lt;&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> &lt;&lt; <paramref name="b"/></returns>
    Public Function op_LeftShift(ByVal a As Long, ByVal b As Long) As Long
        If b < 0 Then Return op_RightShift(a, -b)
        If b > 63 Then Return 0
        Return a << b
    End Function
    ''' <summary>Left shift operator (&lt;&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> &lt;&lt; <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LeftShift(ByVal a As ULong, ByVal b As ULong) As ULong
        If b > 63 Then Return 0
        Return a << b
    End Function

    ''' <summary>Left shift operator (&lt;&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> &lt;&lt; <paramref name="b"/></returns>
    Public Function op_LeftShift(ByVal a As Char, ByVal b As Char) As Char
        Dim ha As New CharHelper(a), hb As New CharHelper(b)
        If hb.uShort > 15 Then Return Chars.NullChar
        ha.uShort <<= hb.uShort
        Return ha.char
    End Function

#End Region

#Region "RightShift"
    ''' <summary>Left shift operator (>>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> >> <paramref name="b"/></returns>
    Public Function op_RightShift(ByVal a As Byte, ByVal b As Byte) As Byte
        If b > 7 Then Return 0
        Return a >> b
    End Function
    ''' <summary>Left shift operator (>>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> >> <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_RightShift(ByVal a As SByte, ByVal b As SByte) As SByte
        Const hFF As Byte = &HFF
        If b < 0 Then Return op_LeftShift(a, -b)
        If b > 7 Then Return If(b.MSB, hFF.BitwiseSame, CSByte(0))
        Return a >> b
    End Function
    ''' <summary>Left shift operator (>>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> >> <paramref name="b"/></returns>
    Public Function op_RightShift(ByVal a As Short, ByVal b As Short) As Short
        If b < 0 Then Return op_LeftShift(a, -b)
        If b > 15 Then Return If(b.MSB, &HFFFFS, 0S)
        Return a >> b
    End Function
    ''' <summary>Left shift operator (>>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> >> <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_RightShift(ByVal a As UShort, ByVal b As UShort) As UShort
        If b > 15 Then Return 0
        Return a >> b
    End Function
    ''' <summary>Left shift operator (>>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> >> <paramref name="b"/></returns>
    Public Function op_RightShift(ByVal a As Integer, ByVal b As Integer) As Integer
        If b < 0 Then Return op_LeftShift(a, -b)
        If b > 31 Then Return If(b.MSB, &HFFFFFFFFI, 0I)
        Return a >> b
    End Function
    ''' <summary>Left shift operator (>>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> >> <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_RightShift(ByVal a As UInteger, ByVal b As UInteger) As UInteger
        If b > 31 Then Return 0
        Return a >> b
    End Function
    ''' <summary>Left shift operator (>>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> >> <paramref name="b"/></returns>
    Public Function op_RightShift(ByVal a As Long, ByVal b As Long) As Long
        If b < 0 Then Return op_LeftShift(a, -b)
        If b > 63 Then Return If(b.MSB, &HFFFFFFFFFFFFFFFFL, 0I)
        Return a >> b
    End Function
    ''' <summary>Left shift operator (>>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> >> <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_RightShift(ByVal a As ULong, ByVal b As ULong) As ULong
        If b > 63 Then Return 0
        Return a >> b
    End Function

    ''' <summary>Left shift operator (>>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> >> <paramref name="b"/></returns>
    Public Function op_RightShift(ByVal a As Char, ByVal b As Char) As Char
        Dim ha As New CharHelper(a), hb As New CharHelper(b)
        If hb.uShort > 15 Then Return Chars.NullChar
        ha.uShort >>= hb.uShort
        Return ha.char
    End Function

#End Region

#Region "SignedRightShift"
    ''' <summary>Signed right shift operator (arithmetic right shift, SAR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SAR <paramref name="b"/></returns>
    Public Function op_SignedRightShift(ByVal a As Byte, ByVal b As Byte) As Byte
        If b > 7 Then Return If(b.MSB, CByte(&HFF), CByte(0))
        Return (a.BitwiseSame >> b).BitwiseSame
    End Function
    ''' <summary>Signed right shift operator (arithmetic right shift, SAR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SAR <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_SignedRightShift(ByVal a As SByte, ByVal b As SByte) As SByte
        Return op_RightShift(a, b)
    End Function
    ''' <summary>Signed right shift operator (arithmetic right shift, SAR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SAR <paramref name="b"/></returns>
    Public Function op_SignedRightShift(ByVal a As Short, ByVal b As Short) As Short
        Return op_RightShift(a, b)
    End Function
    ''' <summary>Signed right shift operator (arithmetic right shift, SAR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SAR <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_SignedRightShift(ByVal a As UShort, ByVal b As UShort) As UShort
        If b > 15 Then Return If(b.MSB, &HFFFFUS, 0US)
        Return (a.BitwiseSame >> b).BitwiseSame
    End Function
    ''' <summary>Signed right shift operator (arithmetic right shift, SAR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SAR <paramref name="b"/></returns>
    Public Function op_SignedRightShift(ByVal a As Integer, ByVal b As Integer) As Integer
        Return op_RightShift(a, b)
    End Function
    ''' <summary>Signed right shift operator (arithmetic right shift, SAR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SAR <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_SignedRightShift(ByVal a As UInteger, ByVal b As UInteger) As UInteger
        If b > 61 Then Return If(b.MSB, &HFFFFFFFFUI, 0UI)
        Return (a.BitwiseSame >> b).BitwiseSame
    End Function
    ''' <summary>Signed right shift operator (arithmetic right shift, SAR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SAR <paramref name="b"/></returns>
    Public Function op_SignedRightShift(ByVal a As Long, ByVal b As Long) As Long
        Return a >> b
    End Function
    ''' <summary>Signed right shift operator (arithmetic right shift, SAR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SAR <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_SignedRightShift(ByVal a As ULong, ByVal b As ULong) As ULong
        If b > 63 Then Return If(b.MSB, &HFFFFFFFFFFFFFFFFUL, 0UL)
        Return (a.BitwiseSame >> b).BitwiseSame
    End Function

    ''' <summary>Signed right shift operator (arithmetic right shift, SAR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SAR <paramref name="b"/></returns>
    Public Function op_SignedRightShift(ByVal a As Char, ByVal b As Char) As Char
        Dim ha As New CharHelper(a), hb As New CharHelper(b)
        ha.uShort = op_SignedRightShift(ha.uShort, hb.uShort)
        Return ha.char
    End Function

#End Region

#Region "UnsignedRightShift"
    ''' <summary>Unsigned right shift (SHR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SHR <paramref name="b"/></returns>
    Public Function op_UnsignedRightShift(ByVal a As Byte, ByVal b As Byte) As Byte
        Return op_RightShift(a, b)
    End Function
    ''' <summary>Unsigned right shift (SHR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SHR <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_UnsignedRightShift(ByVal a As SByte, ByVal b As SByte) As SByte
        If b > 7 Then Return 0
        Return (a.BitwiseSame >> b).BitwiseSame
    End Function
    ''' <summary>Unsigned right shift (SHR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SHR <paramref name="b"/></returns>
    Public Function op_UnsignedRightShift(ByVal a As Short, ByVal b As Short) As Short
        If b > 15 Then Return 0
        Return (a.BitwiseSame >> b).BitwiseSame
    End Function
    ''' <summary>Unsigned right shift (SHR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SHR <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_UnsignedRightShift(ByVal a As UShort, ByVal b As UShort) As UShort
        Return op_RightShift(a, b)
    End Function
    ''' <summary>Unsigned right shift (SHR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SHR <paramref name="b"/></returns>
    Public Function op_UnsignedRightShift(ByVal a As Integer, ByVal b As Integer) As Integer
        If b > 31 Then Return 0
        Return (a.BitwiseSame >> b).BitwiseSame
    End Function
    ''' <summary>Unsigned right shift (SHR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SHR <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_UnsignedRightShift(ByVal a As UInteger, ByVal b As UInteger) As UInteger
        Return op_RightShift(a, b)
    End Function
    ''' <summary>Unsigned right shift (SHR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SHR <paramref name="b"/></returns>
    Public Function op_UnsignedRightShift(ByVal a As Long, ByVal b As Long) As Long
        If b > 63 Then Return 0
        Return (a.BitwiseSame >> b).BitwiseSame
    End Function
    ''' <summary>Unsigned right shift (SHR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SHR <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_UnsignedRightShift(ByVal a As ULong, ByVal b As ULong) As ULong
        Return op_RightShift(a, b)
    End Function

    ''' <summary>Unsigned right shift (SHR)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns><paramref name="a"/> SHR <paramref name="b"/></returns>
    Public Function op_UnsignedRightShift(ByVal a As Char, ByVal b As Char) As Char
        Dim ha As New CharHelper(a), hb As New CharHelper(b)
        ha.uShort = op_UnsignedRightShift(ha.uShort, hb.uShort)
        Return ha.char
    End Function
#End Region

#Region "Equality"
    ''' <summary>Compares thwo objects for equality (a == operator)</summary>
    ''' <param name="a">A object</param>
    ''' <param name="b">A object</param>
    ''' <returns>True if both objects are null or are equal using <paramref name="a"/>.<see cref="System.Object.Equals">Equals</see>(<paramref name="b"/>)</returns>
    Public Function op_Equality(ByVal a As Object, ByVal b As Object) As Boolean
        If a Is Nothing OrElse b Is Nothing Then Return a Is b
        Return a.Equals(b)
    End Function
#End Region

#Region "Inequality"
    ''' <summary>Compares thwo objects for inequality</summary>
    ''' <param name="a">A object</param>
    ''' <param name="b">A object</param>
    ''' <returns>True if exactly one of objects is null or objectse not equal using <paramref name="a"/>.<see cref="System.Object.Equals">Equals</see>(<paramref name="b"/>)</returns>
    Public Function op_Inequality(ByVal a As Object, ByVal b As Object) As Boolean
        If a Is Nothing OrElse b Is Nothing Then Return a IsNot b
        Return Not a.Equals(b)
    End Function
#End Region

#Region "GreaterThan"
    ''' <summary>Greater than comparison operator (>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> > <paramref name="b"/></returns>
    Public Function op_GreaterThan(ByVal a As Byte, ByVal b As Byte) As Boolean
        Return a > b
    End Function
    ''' <summary>Greater than comparison operator (>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> > <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_GreaterThan(ByVal a As SByte, ByVal b As SByte) As Boolean
        Return a > b
    End Function
    ''' <summary>Greater than comparison operator (>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> > <paramref name="b"/></returns>
    Public Function op_GreaterThan(ByVal a As Short, ByVal b As Short) As Boolean
        Return a > b
    End Function
    ''' <summary>Greater than comparison operator (>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> > <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_GreaterThan(ByVal a As UShort, ByVal b As UShort) As Boolean
        Return a > b
    End Function
    ''' <summary>Greater than comparison operator (>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> > <paramref name="b"/></returns>
    Public Function op_GreaterThan(ByVal a As Integer, ByVal b As Integer) As Boolean
        Return a > b
    End Function
    ''' <summary>Greater than comparison operator (>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> > <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_GreaterThan(ByVal a As UInteger, ByVal b As UInteger) As Boolean
        Return a > b
    End Function
    ''' <summary>Greater than comparison operator (>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> > <paramref name="b"/></returns>
    Public Function op_GreaterThan(ByVal a As Long, ByVal b As Long) As Boolean
        Return a > b
    End Function
    ''' <summary>Greater than comparison operator (>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> > <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_GreaterThan(ByVal a As ULong, ByVal b As ULong) As Boolean
        Return a > b
    End Function
    ''' <summary>Greater than comparison operator (>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> > <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_GreaterThan(ByVal a As Single, ByVal b As Single) As Boolean
        Return a > b
    End Function

    ''' <summary>Greater than comparison operator (>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> > <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_GreaterThan(ByVal a As Double, ByVal b As Double) As Boolean
        Return a > b
    End Function
    ''' <summary>Greater than comparison operator (>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> > <paramref name="b"/></returns>
    Public Function op_GreaterThan(ByVal a As Char, ByVal b As Char) As Boolean
        Return a > b
    End Function
    ''' <summary>Greater than comparison operator (>)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> > <paramref name="b"/></returns>
    Public Function op_GreaterThan(ByVal a As Boolean, ByVal b As Boolean) As Boolean
        Return a > b
    End Function

    ''' <summary>Greater than comparison operator (>))</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> > <paramref name="b"/></returns>
    Public Function op_GreaterThan(ByVal a As String, ByVal b As String) As Boolean
        Return a > b
    End Function
#End Region

#Region "LessThan"
    ''' <summary>Less than comparison operator (&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt; <paramref name="b"/></returns>
    Public Function op_LessThan(ByVal a As Byte, ByVal b As Byte) As Boolean
        Return a < b
    End Function
    ''' <summary>Less than comparison operator (&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt; <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LessThan(ByVal a As SByte, ByVal b As SByte) As Boolean
        Return a < b
    End Function
    ''' <summary>Less than comparison operator (&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt; <paramref name="b"/></returns>
    Public Function op_LessThan(ByVal a As Short, ByVal b As Short) As Boolean
        Return a < b
    End Function
    ''' <summary>Less than comparison operator (&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt; <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LessThan(ByVal a As UShort, ByVal b As UShort) As Boolean
        Return a < b
    End Function
    ''' <summary>Less than comparison operator (&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt; <paramref name="b"/></returns>
    Public Function op_LessThan(ByVal a As Integer, ByVal b As Integer) As Boolean
        Return a < b
    End Function
    ''' <summary>Less than comparison operator (&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt; <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LessThan(ByVal a As UInteger, ByVal b As UInteger) As Boolean
        Return a < b
    End Function
    ''' <summary>Less than comparison operator (&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt; <paramref name="b"/></returns>
    Public Function op_LessThan(ByVal a As Long, ByVal b As Long) As Boolean
        Return a < b
    End Function
    ''' <summary>Less than comparison operator (&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt; <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LessThan(ByVal a As ULong, ByVal b As ULong) As Boolean
        Return a < b
    End Function
    ''' <summary>Less than comparison operator (&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt; <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LessThan(ByVal a As Single, ByVal b As Single) As Boolean
        Return a < b
    End Function

    ''' <summary>Less than comparison operator (&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt; <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LessThan(ByVal a As Double, ByVal b As Double) As Boolean
        Return a < b
    End Function
    ''' <summary>Less than comparison operator (&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt; <paramref name="b"/></returns>
    Public Function op_LessThan(ByVal a As Char, ByVal b As Char) As Boolean
        Return a < b
    End Function
    ''' <summary>Less than comparison operator (&lt;)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt; <paramref name="b"/></returns>
    Public Function op_LessThan(ByVal a As Boolean, ByVal b As Boolean) As Boolean
        Return a < b
    End Function

    ''' <summary>Less than comparison operator (&lt;))</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt; <paramref name="b"/></returns>
    Public Function op_LessThan(ByVal a As String, ByVal b As String) As Boolean
        Return a < b
    End Function
#End Region


#Region "GreaterThanOrEqual"
    ''' <summary>Greater than or equal comparison operator (>=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> >= <paramref name="b"/></returns>
    Public Function op_GreaterThanOrEqual(ByVal a As Byte, ByVal b As Byte) As Boolean
        Return a >= b
    End Function
    ''' <summary>Greater than or equal comparison operator (>=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> >= <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_GreaterThanOrEqual(ByVal a As SByte, ByVal b As SByte) As Boolean
        Return a >= b
    End Function
    ''' <summary>Greater than or equal comparison operator (>=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> >= <paramref name="b"/></returns>
    Public Function op_GreaterThanOrEqual(ByVal a As Short, ByVal b As Short) As Boolean
        Return a >= b
    End Function
    ''' <summary>Greater than or equal comparison operator (>=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> >= <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_GreaterThanOrEqual(ByVal a As UShort, ByVal b As UShort) As Boolean
        Return a >= b
    End Function
    ''' <summary>Greater than or equal comparison operator (>=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> >= <paramref name="b"/></returns>
    Public Function op_GreaterThanOrEqual(ByVal a As Integer, ByVal b As Integer) As Boolean
        Return a >= b
    End Function
    ''' <summary>Greater than or equal comparison operator (>=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> >= <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_GreaterThanOrEqual(ByVal a As UInteger, ByVal b As UInteger) As Boolean
        Return a >= b
    End Function
    ''' <summary>Greater than or equal comparison operator (>=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> >= <paramref name="b"/></returns>
    Public Function op_GreaterThanOrEqual(ByVal a As Long, ByVal b As Long) As Boolean
        Return a >= b
    End Function
    ''' <summary>Greater than or equal comparison operator (>=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> >= <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_GreaterThanOrEqual(ByVal a As ULong, ByVal b As ULong) As Boolean
        Return a >= b
    End Function
    ''' <summary>Greater than or equal comparison operator (>=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> >= <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_GreaterThanOrEqual(ByVal a As Single, ByVal b As Single) As Boolean
        Return a >= b
    End Function

    ''' <summary>Greater than or equal comparison operator (>=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> >= <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_GreaterThanOrEqual(ByVal a As Double, ByVal b As Double) As Boolean
        Return a >= b
    End Function
    ''' <summary>Greater than or equal comparison operator (>=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> >= <paramref name="b"/></returns>
    Public Function op_GreaterThanOrEqual(ByVal a As Char, ByVal b As Char) As Boolean
        Return a >= b
    End Function
    ''' <summary>Greater than or equal comparison operator (>=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> >= <paramref name="b"/></returns>
    Public Function op_GreaterThanOrEqual(ByVal a As Boolean, ByVal b As Boolean) As Boolean
        Return a >= b
    End Function

    ''' <summary>Greater than or equal comparison operator (>=))</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> >= <paramref name="b"/></returns>
    Public Function op_GreaterThanOrEqual(ByVal a As String, ByVal b As String) As Boolean
        Return a >= b
    End Function
#End Region

#Region "GreaterThanOrEqual"
    ''' <summary>Less than or equal comparison operator (&lt;=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt;= <paramref name="b"/></returns>
    Public Function op_LessThanOrEqual(ByVal a As Byte, ByVal b As Byte) As Boolean
        Return a <= b
    End Function
    ''' <summary>Less than or equal comparison operator (&lt;=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt;= <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LessThanOrEqual(ByVal a As SByte, ByVal b As SByte) As Boolean
        Return a <= b
    End Function
    ''' <summary>Less than or equal comparison operator (&lt;=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt;= <paramref name="b"/></returns>
    Public Function op_LessThanOrEqual(ByVal a As Short, ByVal b As Short) As Boolean
        Return a <= b
    End Function
    ''' <summary>Less than or equal comparison operator (&lt;=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt;= <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LessThanOrEqual(ByVal a As UShort, ByVal b As UShort) As Boolean
        Return a <= b
    End Function
    ''' <summary>Less than or equal comparison operator (&lt;=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt;= <paramref name="b"/></returns>
    Public Function op_LessThanOrEqual(ByVal a As Integer, ByVal b As Integer) As Boolean
        Return a <= b
    End Function
    ''' <summary>Less than or equal comparison operator (&lt;=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt;= <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LessThanOrEqual(ByVal a As UInteger, ByVal b As UInteger) As Boolean
        Return a <= b
    End Function
    ''' <summary>Less than or equal comparison operator (&lt;=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt;= <paramref name="b"/></returns>
    Public Function op_LessThanOrEqual(ByVal a As Long, ByVal b As Long) As Boolean
        Return a <= b
    End Function
    ''' <summary>Less than or equal comparison operator (&lt;=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt;= <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LessThanOrEqual(ByVal a As ULong, ByVal b As ULong) As Boolean
        Return a <= b
    End Function
    ''' <summary>Less than or equal comparison operator (&lt;=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt;= <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LessThanOrEqual(ByVal a As Single, ByVal b As Single) As Boolean
        Return a <= b
    End Function

    ''' <summary>Less than or equal comparison operator (&lt;=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt;= <paramref name="b"/></returns>
    <CLSCompliant(False)>
    Public Function op_LessThanOrEqual(ByVal a As Double, ByVal b As Double) As Boolean
        Return a <= b
    End Function
    ''' <summary>Less than or equal comparison operator (&lt;=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt;= <paramref name="b"/></returns>
    Public Function op_LessThanOrEqual(ByVal a As Char, ByVal b As Char) As Boolean
        Return a <= b
    End Function
    ''' <summary>Less than or equal comparison operator (&lt;=)</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt;= <paramref name="b"/></returns>
    Public Function op_LessThanOrEqual(ByVal a As Boolean, ByVal b As Boolean) As Boolean
        Return a <= b
    End Function

    ''' <summary>Less than or equal comparison operator (&lt;=))</summary>
    ''' <param name="a">A value</param>
    ''' <param name="b">A value</param>
    ''' <returns>True if <paramref name="a"/> &lt;= <paramref name="b"/></returns>
    Public Function op_LessThanOrEqual(ByVal a As String, ByVal b As String) As Boolean
        Return a <= b
    End Function
#End Region
#Region "Comma"
    ''' <summary>Implements the comma (,) operator</summary>
    ''' <param name="a">Ignored</param>
    ''' <param name="b">Returned</param>
    ''' <typeparam name="T">Type of parameter 2 and return value</typeparam>
    ''' <returns><paramref name="b"/></returns>
    Public Function op_Comma(Of T)(ByVal a As Object, ByVal b As T) As T
        Return b
    End Function
#End Region
#End Region
End Module

#End If