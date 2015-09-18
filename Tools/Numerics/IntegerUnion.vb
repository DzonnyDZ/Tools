Imports System.Runtime.InteropServices

#If True
Namespace NumericsT
    ''' <summary>8-byte union of integral data types</summary>
    ''' <remarks>You can use this union for bitwise operations</remarks>
    ''' <version version="1.5.3">This structure is new in version 1.5.3</version>
    <StructLayout(LayoutKind.Explicit, size:=8)>
    <DebuggerDisplay("LongUnion {long} {ulong}")>
    Public Structure LongUnion
#Region "Fields"
        ''' <summary>Most significant (high order) unsigned byte</summary>
        <FieldOffset(0)> Public byte7 As Byte
        ''' <summary>2nd-most significant unsigned byte</summary>
        <FieldOffset(1)> Public byte6 As Byte
        ''' <summary>3rd-most significant unsigned byte</summary>
        <FieldOffset(2)> Public byte5 As Byte
        ''' <summary>4rd-most significant unsigned byte</summary>
        <FieldOffset(3)> Public byte4 As Byte
        ''' <summary>4th-least significant unsigned byte</summary>
        <FieldOffset(4)> Public byte3 As Byte
        ''' <summary>3rd-least significant unsigned byte</summary>
        <FieldOffset(5)> Public byte2 As Byte
        ''' <summary>2nd-least significant unsigned byte</summary>
        <FieldOffset(6)> Public byte1 As Byte
        ''' <summary>Least significant (low order) byte</summary>
        <FieldOffset(7)> Public byte0 As Byte

        ''' <summary>Most significant (high order) signed byte</summary>
        <FieldOffset(0), CLSCompliant(False)> Public sbyte7 As SByte
        ''' <summary>2nd-most significant signed byte</summary>
        <FieldOffset(1), CLSCompliant(False)> Public sbyte6 As SByte
        ''' <summary>3rd-most significant signed byte</summary>
        <FieldOffset(2), CLSCompliant(False)> Public sbyte5 As SByte
        ''' <summary>4rd-most significant signed byte</summary>
        <FieldOffset(3), CLSCompliant(False)> Public sbyte4 As SByte
        ''' <summary>4th-least significant signed byte</summary>
        <FieldOffset(4), CLSCompliant(False)> Public sbyte3 As SByte
        ''' <summary>3rd-least significant signed byte</summary>
        <FieldOffset(5), CLSCompliant(False)> Public sbyte2 As SByte
        ''' <summary>2nd-least significant signed byte</summary>
        <FieldOffset(6), CLSCompliant(False)> Public sbyte1 As SByte
        ''' <summary>Least significant (low order) byte</summary>
        <FieldOffset(7), CLSCompliant(False)> Public sbyte0 As SByte

        ''' <summary>Most significant (high order) signed word</summary>
        <FieldOffset(0)> Public short3 As Short
        ''' <summary>2nd-most significant signed word</summary>
        <FieldOffset(2)> Public short2 As Short
        ''' <summary>2nd-least significant signed word</summary>
        <FieldOffset(4)> Public short1 As Short
        ''' <summary>Least significatnt (low order) signed word</summary>
        <FieldOffset(6)> Public short0 As Short

        ''' <summary>Most significant (high order) unsigned word</summary>
        <FieldOffset(0), CLSCompliant(False)> Public ushort3 As UShort
        ''' <summary>2nd-most significant unsigned word</summary>
        <FieldOffset(2), CLSCompliant(False)> Public ushort2 As UShort
        ''' <summary>2nd-least significant unsigned word</summary>
        <FieldOffset(4), CLSCompliant(False)> Public ushort1 As UShort
        ''' <summary>Least significatnt (low order) unsigned word</summary>
        <FieldOffset(6), CLSCompliant(False)> Public ushort0 As UShort

        ''' <summary>High order signed dword</summary>
        <FieldOffset(0)> Public integer1 As Integer
        ''' <summary>Low order signed dword</summary>
        <FieldOffset(4)> Public integer0 As Integer

        ''' <summary>Hight order unsigned dword</summary>
        <FieldOffset(0), CLSCompliant(False)> Public uinteger0 As UInteger
        ''' <summary>Low order unsigned dword</summary>
        <FieldOffset(4), CLSCompliant(False)> Public uinteger1 As UInteger

        ''' <summary>Qword signed value</summary>
        <FieldOffset(0)> Public [long] As Long
        ''' <summary>Qword unsigned value</summary>
        <FieldOffset(0), CLSCompliant(False)> Public [ulong] As ULong
#End Region

#Region "CTors"
        ''' <summary>Initializes <see cref="LongUnion"/> from 64-bit signed integer</summary>
        ''' <param name="long">A 64-bit signed integer</param>
        Public Sub New(ByVal [long] As Long)
            Me.long = [long]
        End Sub
        ''' <summary>Initializes <see cref="LongUnion"/> from 64-bit unsigned integer</summary>
        ''' <param name="ulong">A 64-bit unsigned integer</param>
        <CLSCompliant(False)> Public Sub New(ByVal [ulong] As ULong)
            Me.ulong = [ulong]
        End Sub
        ''' <summary>Initializes <see cref="LongUnion"/> from 32-bit signed integers</summary>
        ''' <param name="integer0">Low order value</param>
        ''' <param name="integer1">High order value</param>
        Public Sub New(ByVal integer0%, ByVal integer1%)
            Me.integer0 = integer0
            Me.integer1 = integer1
        End Sub
        ''' <summary>Initializes <see cref="LongUnion"/> from 32-bit unsigned integers</summary>
        ''' <param name="uinteger0">Low order value</param>
        ''' <param name="uinteger1">High order value</param>
        <CLSCompliant(False)> Public Sub New(ByVal uinteger0 As UInteger, Optional ByVal uinteger1 As UInteger = 0)
            Me.uinteger0 = uinteger0
            Me.uinteger1 = uinteger1
        End Sub
        ''' <summary>Initializes <see cref="LongUnion"/> from 16-bit signed integers</summary>
        ''' <param name="short0">Low order value</param>
        ''' <param name="short1">2nd low value</param>
        ''' <param name="short2">2nd high value</param>
        ''' <param name="short3">High order value</param>
        Public Sub New(ByVal short0 As Short, Optional ByVal short1 As Short = 0, Optional ByVal short2 As Short = 0, Optional ByVal short3 As Short = 0)
            Me.short0 = short0
            Me.short1 = short1
            Me.short2 = short2
            Me.short3 = short3
        End Sub
        ''' <summary>Initializes <see cref="LongUnion"/> from 16-bit unsigned integers</summary>
        ''' <param name="ushort0">Low order value</param>
        ''' <param name="ushort1">2nd low value</param>
        ''' <param name="ushort2">2nd high value</param>
        ''' <param name="ushort3">Low high value</param>
        <CLSCompliant(False)> Public Sub New(ByVal ushort0 As UShort, Optional ByVal ushort1 As UShort = 0, Optional ByVal ushort2 As UShort = 0, Optional ByVal ushort3 As UShort = 0)
            Me.ushort0 = ushort0
            Me.ushort1 = ushort1
            Me.ushort2 = ushort2
            Me.ushort3 = ushort3
        End Sub
        ''' <summary>Initializes <see cref="LongUnion"/> from 8-bit unsigned integers</summary>
        ''' <param name="byte0">Low order byte</param>
        ''' <param name="byte1">2nd byte</param>
        ''' <param name="byte2">3rd byte</param>
        ''' <param name="byte3">4th byte</param>
        ''' <param name="byte4">5th byte</param>
        ''' <param name="byte5">6th byte</param>
        ''' <param name="byte6">7th byte</param>
        ''' <param name="byte7">High order byte</param>
        Public Sub New(ByVal byte0 As Byte, Optional ByVal byte1 As Byte = 0, Optional ByVal byte2 As Byte = 0, Optional ByVal byte3 As Byte = 0,
                       Optional ByVal byte4 As Byte = 0, Optional ByVal byte5 As Byte = 0, Optional ByVal byte6 As Byte = 0, Optional ByVal byte7 As Byte = 0)
            Me.byte0 = byte0
            Me.byte1 = byte1
            Me.byte2 = byte2
            Me.byte3 = byte3
            Me.byte4 = byte4
            Me.byte5 = byte5
            Me.byte6 = byte6
            Me.byte7 = byte7
        End Sub
        ''' <summary>Initializes <see cref="LongUnion"/> from 8-bit signed integers</summary>
        ''' <param name="sbyte0">Low order byte</param>
        ''' <param name="sbyte1">2nd byte</param>
        ''' <param name="sbyte2">3rd byte</param>
        ''' <param name="sbyte3">4th byte</param>
        ''' <param name="sbyte4">5th byte</param>
        ''' <param name="sbyte5">6th byte</param>
        ''' <param name="sbyte6">7th byte</param>
        ''' <param name="sbyte7">High order byte</param>
        <CLSCompliant(False)> Public Sub New(ByVal sbyte0 As SByte, Optional ByVal sbyte1 As SByte = 0, Optional ByVal sbyte2 As SByte = 0, Optional ByVal sbyte3 As SByte = 0,
                                             Optional ByVal sbyte4 As SByte = 0, Optional ByVal sbyte5 As SByte = 0, Optional ByVal sbyte6 As SByte = 0, Optional ByVal sbyte7 As SByte = 0)
            Me.sbyte0 = sbyte0
            Me.sbyte1 = sbyte1
            Me.sbyte2 = sbyte2
            Me.sbyte3 = sbyte3
            Me.sbyte4 = sbyte4
            Me.sbyte5 = sbyte5
            Me.sbyte6 = sbyte6
            Me.sbyte7 = sbyte7
        End Sub
#End Region

#Region "Operators"
        ''' <summary>Converts <see cref="Long"/> to <see cref="LongUnion"/></summary>
        ''' <param name="a">A <see cref="Long"/></param>
        ''' <returns><see cref="LongUnion"/> initialized by <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As Long) As LongUnion
            Return New LongUnion(a)
        End Operator
        ''' <summary>Converts <see cref="ULong"/> to <see cref="LongUnion"/></summary>
        ''' <param name="a">A <see cref="ULong"/></param>
        ''' <returns><see cref="LongUnion"/> initialized by <paramref name="a"/></returns>
        <CLSCompliant(False)> Public Shared Widening Operator CType(ByVal a As ULong) As LongUnion
            Return New LongUnion(a)
        End Operator
        ''' <summary>Converts <see cref="LongUnion"/> to <see cref="ULong"/></summary>
        ''' <param name="a">A <see cref="LongUnion"/></param>
        ''' <returns><paramref name="a"/>.<see cref="[ulong]">ulong</see></returns>
        <CLSCompliant(False)> Public Shared Widening Operator CType(ByVal a As LongUnion) As ULong
            Return a.ulong
        End Operator
        ''' <summary>Converts <see cref="LongUnion"/> to <see cref="Long"/></summary>
        ''' <param name="a">A <see cref="LongUnion"/></param>
        ''' <returns><paramref name="a"/>.<see cref="[long]">ulong</see></returns>
        Public Shared Widening Operator CType(ByVal a As LongUnion) As Long
            Return a.long
        End Operator

        ''' <summary>Compares two instances of <see cref="LongUnion"/> for equality</summary>
        ''' <param name="a">A <see cref="LongUnion"/></param>
        ''' <param name="b">A <see cref="LongUnion"/></param>
        ''' <returns>True if values represented by <paramref name="a"/> and <paramref name="b"/> are same</returns>
        Public Shared Operator =(ByVal a As LongUnion, ByVal b As LongUnion) As Boolean
            Return a.long = b.long
        End Operator
        ''' <summary>Compares two instances of <see cref="LongUnion"/> for inequality</summary>
        ''' <param name="a">A <see cref="LongUnion"/></param>
        ''' <param name="b">A <see cref="LongUnion"/></param>
        ''' <returns>True if values represented by <paramref name="a"/> and <paramref name="b"/> differ</returns>
        Public Shared Operator <>(ByVal a As LongUnion, ByVal b As LongUnion) As Boolean
            Return a.long <> b.long
        End Operator
#End Region

#Region "Overrides"
        ''' <summary>Gets string representation of this instance</summary>
        ''' <returns>String representation of this instance showing signed and unsigned value in decimal and hexadecimal representation</returns>
        Public Overrides Function ToString() As String
            Return String.Format("{0}/{1}; {0:X}/{1:X}", Me.long, Me.ulong)
        End Function
        ''' <summary>Indicates whether this instance and a specified object are equal.</summary>
        ''' <returns>True if <paramref name="obj"/> is either <see cref="LongUnion"/>, <see cref="Long"/> or <see cref="ULong"/> and represents same value as current instance.</returns>
        ''' <param name="obj">Another object to compare to. </param>
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If TypeOf obj Is LongUnion Then Return Me = DirectCast(obj, LongUnion)
            If TypeOf obj Is Long Then Return Me = DirectCast(obj, Long)
            If TypeOf obj Is ULong Then Return Me = DirectCast(obj, ULong)
            Return False
        End Function
        ''' <summary>Returns the hash code for this instance.</summary>
        ''' <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        Public Overrides Function GetHashCode() As Integer
            Return Me.long.GetHashCode
        End Function
#End Region
        ''' <summary>Bitwise converts value of type <see cref="ULong"/> to <see cref="Long"/></summary>
        ''' <param name="unsigned">An <see cref="Long"/></param>
        ''' <returns>Signed value with same bitwise representation as <paramref name="a"/></returns>
        <CLSCompliant(False)> Public Shared Function BitwiseSame(ByVal unsigned As ULong) As Long
            Return New LongUnion(unsigned).long
        End Function
        ''' <summary>Bitwise converts value of type <see cref="Long"/> to <see cref="ULong"/></summary>
        ''' <param name="signed">A <see cref="ULong"/></param>
        ''' <returns>Unsigned value with same bitwise representation as <paramref name="a"/></returns>
        <CLSCompliant(False)> Public Shared Function BitwiseSame(ByVal signed As Long) As ULong
            Return New LongUnion(signed).ulong
        End Function
    End Structure

    ''' <summary>4-byte union of integral data types</summary>
    ''' <remarks>You can use this union for bitwise operations</remarks>
    ''' <version version="1.5.3">This structure is new in version 1.5.3</version>
    <StructLayout(LayoutKind.Explicit, size:=4)>
    <DebuggerDisplay("IntegerUnion {integer} {uinteger}")>
    Public Structure IntegerUnion
#Region "Fields"
        ''' <summary>Most significant (high order) unsigned byte</summary>
        <FieldOffset(0)> Public byte3 As Byte
        ''' <summary>2nd-most significant unsigned byte</summary>
        <FieldOffset(1)> Public byte2 As Byte
        ''' <summary>2nd-least significant unsigned byte</summary>
        <FieldOffset(2)> Public byte1 As Byte
        ''' <summary>Least significant (low order) unsigned byte</summary>
        <FieldOffset(3)> Public byte0 As Byte


        ''' <summary>Most significant (high order) signed byte</summary>
        <FieldOffset(0), CLSCompliant(False)> Public sbyte3 As SByte
        ''' <summary>2nd-most significant signed byte</summary>
        <FieldOffset(1), CLSCompliant(False)> Public sbyte2 As SByte
        ''' <summary>2nd-least significant signed byte</summary>
        <FieldOffset(2), CLSCompliant(False)> Public sbyte1 As SByte
        ''' <summary>Least significant (low order) signed byte</summary>
        <FieldOffset(3), CLSCompliant(False)> Public sbyte0 As SByte

        ''' <summary>Most significatnt (high order) signed word</summary>
        <FieldOffset(0)> Public short1 As Short
        ''' <summary>Least significatnt (low order) signed word</summary>
        <FieldOffset(2)> Public short0 As Short

        ''' <summary>Most significatnt (high order) unsigned word</summary>
        <FieldOffset(0), CLSCompliant(False)> Public ushort1 As UShort
        ''' <summary>Least significatnt (low order) unsigned word</summary>
        <FieldOffset(2), CLSCompliant(False)> Public ushort0 As UShort

        ''' <summary>Signed integer value</summary>
        <FieldOffset(0)> Public [integer] As Integer
        ''' <summary>Unsigned integer value</summary>
        <FieldOffset(0), CLSCompliant(False)> Public [uinteger] As UInteger
#End Region

#Region "CTors"
        ''' <summary>Initializes <see cref="IntegerUnion"/> from 32-bit signed integer</summary>
        ''' <param name="integer">A 32-bit signed integer</param>
        Public Sub New(ByVal integer%)
            Me.integer = integer%
        End Sub
        ''' <summary>Initializes <see cref="IntegerUnion"/> from 32-bit unsigned integer</summary>
        ''' <param name="uinteger">A 32-bit unsigned integer</param>
        <CLSCompliant(False)> Public Sub New(ByVal [uinteger] As ULong)
            Me.[uinteger] = [uinteger]
        End Sub

        ''' <summary>Initializes <see cref="IntegerUnion"/> from 16-bit signed integers</summary>
        ''' <param name="short0">Low order value</param>
        ''' <param name="short1">high order value</param>
        Public Sub New(ByVal short0 As Short, Optional ByVal short1 As Short = 0)
            Me.short0 = short0
            Me.short1 = short1
        End Sub
        ''' <summary>Initializes <see cref="IntegerUnion"/> from 16-bit unsigned integers</summary>
        ''' <param name="ushort0">Low order value</param>
        ''' <param name="ushort1">high value</param>
        <CLSCompliant(False)> Public Sub New(ByVal ushort0 As UShort, Optional ByVal ushort1 As UShort = 0)
            Me.ushort0 = ushort0
            Me.ushort1 = ushort1
        End Sub
        ''' <summary>Initializes <see cref="IntegerUnion"/> from 8-bit unsigned integers</summary>
        ''' <param name="byte0">Low order byte</param>
        ''' <param name="byte1">2nd byte</param>
        ''' <param name="byte2">3rd byte</param>
        ''' <param name="byte3">High order byte</param>
        Public Sub New(ByVal byte0 As Byte, Optional ByVal byte1 As Byte = 0, Optional ByVal byte2 As Byte = 0, Optional ByVal byte3 As Byte = 0)
            Me.byte0 = byte0
            Me.byte1 = byte1
            Me.byte2 = byte2
            Me.byte3 = byte3
        End Sub
        ''' <summary>Initializes <see cref="IntegerUnion"/> from 8-bit signed integers</summary>
        ''' <param name="sbyte0">Low order byte</param>
        ''' <param name="sbyte1">2nd byte</param>
        ''' <param name="sbyte2">3rd byte</param>
        ''' <param name="sbyte3">High order byte</param>
        <CLSCompliant(False)> Public Sub New(ByVal sbyte0 As SByte, Optional ByVal sbyte1 As SByte = 0, Optional ByVal sbyte2 As SByte = 0, Optional ByVal sbyte3 As SByte = 0)
            Me.sbyte0 = sbyte0
            Me.sbyte1 = sbyte1
            Me.sbyte2 = sbyte2
            Me.sbyte3 = sbyte3
        End Sub
#End Region

#Region "Operators"
        ''' <summary>Converts <see cref="Integer"/> to <see cref="LongUnion"/></summary>
        ''' <param name="a">A <see cref="Integer"/></param>
        ''' <returns><see cref="LongUnion"/> initialized by <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As Integer) As IntegerUnion
            Return New IntegerUnion(a)
        End Operator
        ''' <summary>Converts <see cref="UInteger"/> to <see cref="IntegerUnion"/></summary>
        ''' <param name="a">A <see cref="UInteger"/></param>
        ''' <returns><see cref="LongUnion"/> initialized by <paramref name="a"/></returns>
        <CLSCompliant(False)> Public Shared Widening Operator CType(ByVal a As UInteger) As IntegerUnion
            Return New IntegerUnion(a)
        End Operator
        ''' <summary>Converts <see cref="IntegerUnion"/> to <see cref="UInteger"/></summary>
        ''' <param name="a">A <see cref="IntegerUnion"/></param>
        ''' <returns><paramref name="a"/>.<see cref="[uinteger]">ulong</see></returns>
        <CLSCompliant(False)> Public Shared Widening Operator CType(ByVal a As IntegerUnion) As UInteger
            Return a.uinteger
        End Operator
        ''' <summary>Converts <see cref="IntegerUnion"/> to <see cref="Integer"/></summary>
        ''' <param name="a">A <see cref="IntegerUnion"/></param>
        ''' <returns><paramref name="a"/>.<see cref="[integer]">ulong</see></returns>
        Public Shared Widening Operator CType(ByVal a As IntegerUnion) As Integer
            Return a.integer
        End Operator

        ''' <summary>Compares two instances of <see cref="IntegerUnion"/> for equality</summary>
        ''' <param name="a">A <see cref="IntegerUnion"/></param>
        ''' <param name="b">A <see cref="IntegerUnion"/></param>
        ''' <returns>True if values represented by <paramref name="a"/> and <paramref name="b"/> are same</returns>
        Public Shared Operator =(ByVal a As IntegerUnion, ByVal b As IntegerUnion) As Boolean
            Return a.integer = b.integer
        End Operator
        ''' <summary>Compares two instances of <see cref="IntegerUnion"/> for inequality</summary>
        ''' <param name="a">A <see cref="IntegerUnion"/></param>
        ''' <param name="b">A <see cref="IntegerUnion"/></param>
        ''' <returns>True if values represented by <paramref name="a"/> and <paramref name="b"/> differ</returns>
        Public Shared Operator <>(ByVal a As IntegerUnion, ByVal b As IntegerUnion) As Boolean
            Return a.integer <> b.integer
        End Operator
#End Region

#Region "Overrides"
        ''' <summary>Gets string representation of this instance</summary>
        ''' <returns>String representation of this instance showing signed and unsigned value in decimal and hexadecimal representation</returns>
        Public Overrides Function ToString() As String
            Return String.Format("{0}/{1}; {0:X}/{1:X}", Me.integer, Me.uinteger)
        End Function
        ''' <summary>Indicates whether this instance and a specified object are equal.</summary>
        ''' <returns>True if <paramref name="obj"/> is either <see cref="IntegerUnion"/>, <see cref="Integer"/> or <see cref="UInteger"/> and represents same value as current instance.</returns>
        ''' <param name="obj">Another object to compare to. </param>
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If TypeOf obj Is LongUnion Then Return Me = DirectCast(obj, IntegerUnion)
            If TypeOf obj Is Integer Then Return Me = DirectCast(obj, Integer)
            If TypeOf obj Is UInteger Then Return Me = DirectCast(obj, UInteger)
            Return False
        End Function
        ''' <summary>Returns the hash code for this instance.</summary>
        ''' <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        Public Overrides Function GetHashCode() As Integer
            Return Me.integer.GetHashCode
        End Function
#End Region
        ''' <summary>Bitwise converts value of type <see cref="UInteger"/> to <see cref="Integer"/></summary>
        ''' <param name="unsigned">An <see cref="Integer"/></param>
        ''' <returns>Signed value with same bitwise representation as <paramref name="a"/></returns>
        <CLSCompliant(False)> Public Shared Function BitwiseSame(ByVal unsigned As UInteger) As Integer
            Return New IntegerUnion(unsigned).integer
        End Function
        ''' <summary>Bitwise converts value of type <see cref="Integer"/> to <see cref="UInteger"/></summary>
        ''' <param name="signed">A <see cref="UInteger"/></param>
        ''' <returns>Unsigned value with same bitwise representation as <paramref name="a"/></returns>
        <CLSCompliant(False)> Public Shared Function BitwiseSame(ByVal signed As Integer) As UInteger
            Return New IntegerUnion(signed).uinteger
        End Function
    End Structure

    ''' <summary>2-byte union of integral data types</summary>
    ''' <remarks>You can use this union for bitwise operations</remarks>
    ''' <version version="1.5.3">This structure is new in version 1.5.3</version>
    <StructLayout(LayoutKind.Explicit, size:=2)>
    <DebuggerDisplay("ShortUnion {short} {ushort}")>
    Public Structure ShortUnion
#Region "Fields"
        ''' <summary>Most significant (high order) unsigned byte</summary>
        <FieldOffset(0)> Public byte1 As Byte
        ''' <summary>Least significant (low order) unsigned byte</summary>
        <FieldOffset(1)> Public byte0 As Byte


        ''' <summary>Most significant (high order) signed byte</summary>
        <FieldOffset(0), CLSCompliant(False)> Public sbyte1 As SByte
        ''' <summary>Least significant (low order) signed byte</summary>
        <FieldOffset(1), CLSCompliant(False)> Public sbyte0 As SByte

        ''' <summary>Signed 16-bit value</summary>
        <FieldOffset(0)> Public [short] As Short
        ''' <summary>Unsigned 16-bit value</summary>
        <FieldOffset(0), CLSCompliant(False)> Public [ushort] As UShort
#End Region

#Region "CTors"
        ''' <summary>Initializes <see cref="ShortUnion"/> from 16-bit signed integer</summary>
        ''' <param name="short">A 16-bit signed integer</param>
        Public Sub New(ByVal [short] As Short)
            Me.short = [short]
        End Sub
        ''' <summary>Initializes <see cref="ShortUnion"/> from 16-bit unsigned integer</summary>
        ''' <param name="ushort">A 16-bit unsigned integer</param>
        <CLSCompliant(False)> Public Sub New(ByVal [ushort] As UShort)
            Me.ushort = [ushort]
        End Sub

        ''' <summary>Initializes <see cref="ShortUnion"/> from 8-bit unsigned integers</summary>
        ''' <param name="byte0">Low order byte</param>
        ''' <param name="byte1">High order byte</param>
        Public Sub New(ByVal byte0 As Byte, Optional ByVal byte1 As Byte = 0)
            Me.byte0 = byte0
            Me.byte1 = byte1
        End Sub
        ''' <summary>Initializes <see cref="ShortUnion"/> from 8-bit signed integers</summary>
        ''' <param name="sbyte0">Low order byte</param>
        ''' <param name="sbyte1">High order byte</param>
        <CLSCompliant(False)> Public Sub New(ByVal sbyte0 As SByte, Optional ByVal sbyte1 As SByte = 0)
            Me.sbyte0 = sbyte0
            Me.sbyte1 = sbyte1
        End Sub
#End Region

#Region "Operators"
        ''' <summary>Converts <see cref="Short"/> to <see cref="LongUnion"/></summary>
        ''' <param name="a">A <see cref="Short"/></param>
        ''' <returns><see cref="LongUnion"/> initialized by <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As Short) As ShortUnion
            Return New ShortUnion(a)
        End Operator
        ''' <summary>Converts <see cref="UShort"/> to <see cref="ShortUnion"/></summary>
        ''' <param name="a">A <see cref="UShort"/></param>
        ''' <returns><see cref="LongUnion"/> initialized by <paramref name="a"/></returns>
        <CLSCompliant(False)> Public Shared Widening Operator CType(ByVal a As UShort) As ShortUnion
            Return New ShortUnion(a)
        End Operator
        ''' <summary>Converts <see cref="ShortUnion"/> to <see cref="UShort"/></summary>
        ''' <param name="a">A <see cref="ShortUnion"/></param>
        ''' <returns><paramref name="a"/>.<see cref="[uShort]">ulong</see></returns>
        <CLSCompliant(False)> Public Shared Widening Operator CType(ByVal a As ShortUnion) As UShort
            Return a.ushort
        End Operator
        ''' <summary>Converts <see cref="ShortUnion"/> to <see cref="Short"/></summary>
        ''' <param name="a">A <see cref="ShortUnion"/></param>
        ''' <returns><paramref name="a"/>.<see cref="[Short]">ulong</see></returns>
        Public Shared Widening Operator CType(ByVal a As ShortUnion) As Short
            Return a.short
        End Operator

        ''' <summary>Compares two instances of <see cref="ShortUnion"/> for equality</summary>
        ''' <param name="a">A <see cref="ShortUnion"/></param>
        ''' <param name="b">A <see cref="ShortUnion"/></param>
        ''' <returns>True if values represented by <paramref name="a"/> and <paramref name="b"/> are same</returns>
        Public Shared Operator =(ByVal a As ShortUnion, ByVal b As ShortUnion) As Boolean
            Return a.short = b.short
        End Operator
        ''' <summary>Compares two instances of <see cref="ShortUnion"/> for inequality</summary>
        ''' <param name="a">A <see cref="ShortUnion"/></param>
        ''' <param name="b">A <see cref="ShortUnion"/></param>
        ''' <returns>True if values represented by <paramref name="a"/> and <paramref name="b"/> differ</returns>
        Public Shared Operator <>(ByVal a As ShortUnion, ByVal b As ShortUnion) As Boolean
            Return a.short <> b.short
        End Operator
#End Region

#Region "Overrides"
        ''' <summary>Gets string representation of this instance</summary>
        ''' <returns>String representation of this instance showing signed and unsigned value in decimal and hexadecimal representation</returns>
        Public Overrides Function ToString() As String
            Return String.Format("{0}/{1}; {0:X}/{1:X}", Me.short, Me.ushort)
        End Function
        ''' <summary>Indicates whether this instance and a specified object are equal.</summary>
        ''' <returns>True if <paramref name="obj"/> is either <see cref="ShortUnion"/>, <see cref="Short"/> or <see cref="UShort"/> and represents same value as current instance.</returns>
        ''' <param name="obj">Another object to compare to. </param>
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If TypeOf obj Is LongUnion Then Return Me = DirectCast(obj, ShortUnion)
            If TypeOf obj Is Short Then Return Me = DirectCast(obj, Short)
            If TypeOf obj Is UShort Then Return Me = DirectCast(obj, UShort)
            Return False
        End Function
        ''' <summary>Returns the hash code for this instance.</summary>
        ''' <returns>A 32-bit signed Short that is the hash code for this instance.</returns>
        Public Overrides Function GetHashCode() As Integer
            Return Me.short.GetHashCode
        End Function
#End Region
        ''' <summary>Bitwise converts value of type <see cref="UShort"/> to <see cref="Short"/></summary>
        ''' <param name="unsigned">An <see cref="Short"/></param>
        ''' <returns>Signed value with same bitwise representation as <paramref name="a"/></returns>
        <CLSCompliant(False)> Public Shared Function BitwiseSame(ByVal unsigned As UShort) As Short
            Return New ShortUnion(unsigned).short
        End Function
        ''' <summary>Bitwise converts value of type <see cref="Short"/> to <see cref="UShort"/></summary>
        ''' <param name="signed">A <see cref="UShort"/></param>
        ''' <returns>Unsigned value with same bitwise representation as <paramref name="a"/></returns>
        <CLSCompliant(False)> Public Shared Function BitwiseSame(ByVal signed As Short) As UShort
            Return New ShortUnion(signed).ushort
        End Function
    End Structure

    ''' <summary>1-byte union of integral data types</summary>
    ''' <remarks>You can use this union for bitwise operations</remarks>
    ''' <version version="1.5.3">This structure is new in version 1.5.3</version>
    <StructLayout(LayoutKind.Explicit, size:=1)>
    <DebuggerDisplay("ShortUnion {sbyte} {byte}")>
    Public Structure ByteUnion
#Region "Fields"
        ''' <summary>Unsigned value</summary>
        <FieldOffset(0)> Public [byte] As Byte
        ''' <summary>Signed value</summary>
        <FieldOffset(0), CLSCompliant(False)> Public [sbyte] As SByte
#End Region

#Region "CTors"
        ''' <summary>Initializes <see cref="ByteUnion"/> from 8-bit unsigned integer</summary>
        ''' <param name="byte">A 8-bit unsigned integer</param>
        Public Sub New(ByVal [byte] As Byte)
            Me.byte = [byte]
        End Sub
        ''' <summary>Initializes <see cref="ByteUnion"/> from 8-bitsigned integer</summary>
        ''' <param name="sbyte">A 8-bit signed integer</param>
        <CLSCompliant(False)> Public Sub New(ByVal [sbyte] As SByte)
            Me.sbyte = [sbyte]
        End Sub
#End Region

#Region "Operators"
        ''' <summary>Converts <see cref="Byte"/> to <see cref="LongUnion"/></summary>
        ''' <param name="a">A <see cref="Byte"/></param>
        ''' <returns><see cref="LongUnion"/> initialized by <paramref name="a"/></returns>
        Public Shared Widening Operator CType(ByVal a As Byte) As ByteUnion
            Return New ByteUnion(a)
        End Operator
        ''' <summary>Converts <see cref="sbyte"/> to <see cref="ByteUnion"/></summary>
        ''' <param name="a">A <see cref="sbyte"/></param>
        ''' <returns><see cref="LongUnion"/> initialized by <paramref name="a"/></returns>
        <CLSCompliant(False)> Public Shared Widening Operator CType(ByVal a As SByte) As ByteUnion
            Return New ByteUnion(a)
        End Operator
        ''' <summary>Converts <see cref="ByteUnion"/> to <see cref="sbyte"/></summary>
        ''' <param name="a">A <see cref="ByteUnion"/></param>
        ''' <returns><paramref name="a"/>.<see cref="[sbyte]">ulong</see></returns>
        <CLSCompliant(False)> Public Shared Widening Operator CType(ByVal a As ByteUnion) As SByte
            Return a.sbyte
        End Operator
        ''' <summary>Converts <see cref="ByteUnion"/> to <see cref="Byte"/></summary>
        ''' <param name="a">A <see cref="ByteUnion"/></param>
        ''' <returns><paramref name="a"/>.<see cref="[Byte]">ulong</see></returns>
        Public Shared Widening Operator CType(ByVal a As ByteUnion) As Byte
            Return a.byte
        End Operator

        ''' <summary>Compares two instances of <see cref="ByteUnion"/> for equality</summary>
        ''' <param name="a">A <see cref="ByteUnion"/></param>
        ''' <param name="b">A <see cref="ByteUnion"/></param>
        ''' <returns>True if values represented by <paramref name="a"/> and <paramref name="b"/> are same</returns>
        Public Shared Operator =(ByVal a As ByteUnion, ByVal b As ByteUnion) As Boolean
            Return a.byte = b.byte
        End Operator
        ''' <summary>Compares two instances of <see cref="ByteUnion"/> for inequality</summary>
        ''' <param name="a">A <see cref="ByteUnion"/></param>
        ''' <param name="b">A <see cref="ByteUnion"/></param>
        ''' <returns>True if values represented by <paramref name="a"/> and <paramref name="b"/> differ</returns>
        Public Shared Operator <>(ByVal a As ByteUnion, ByVal b As ByteUnion) As Boolean
            Return a.byte <> b.byte
        End Operator
#End Region

#Region "Overrides"
        ''' <summary>Gets string representation of this instance</summary>
        ''' <returns>String representation of this instance showing signed and unsigned value in decimal and hexadecimal representation</returns>
        Public Overrides Function ToString() As String
            Return String.Format("{0}/{1}; {0:X}/{1:X}", Me.sbyte, Me.byte)
        End Function
        ''' <summary>Indicates whether this instance and a specified object are equal.</summary>
        ''' <returns>True if <paramref name="obj"/> is either <see cref="ByteUnion"/>, <see cref="Byte"/> or <see cref="SByte"/> and represents same value as current instance.</returns>
        ''' <param name="obj">Another object to compare to. </param>
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            If TypeOf obj Is LongUnion Then Return Me = DirectCast(obj, ByteUnion)
            If TypeOf obj Is Byte Then Return Me = DirectCast(obj, Byte)
            If TypeOf obj Is SByte Then Return Me = DirectCast(obj, SByte)
            Return False
        End Function
        ''' <summary>Returns the hash code for this instance.</summary>
        ''' <returns>A 32-bit signed Byte that is the hash code for this instance.</returns>
        Public Overrides Function GetHashCode() As Integer
            Return Me.byte.GetHashCode
        End Function
#End Region
        ''' <summary>Bitwise converts value of type <see cref="Byte"/> to <see cref="SByte"/></summary>
        ''' <param name="unsigned">An <see cref="SByte"/></param>
        ''' <returns>Signed value with same bitwise representation as <paramref name="a"/></returns>
        <CLSCompliant(False)> Public Shared Function BitwiseSame(ByVal unsigned As Byte) As SByte
            Return New ByteUnion(unsigned).sbyte
        End Function
        ''' <summary>Bitwise converts value of type <see cref="SByte"/> to <see cref="Byte"/></summary>
        ''' <param name="signed">A <see cref="Byte"/></param>
        ''' <returns>Unsigned value with same bitwise representation as <paramref name="a"/></returns>
        <CLSCompliant(False)> Public Shared Function BitwiseSame(ByVal signed As SByte) As Byte
            Return New ByteUnion(signed).byte
        End Function
    End Structure

    ''' <summary>4- or 8-byte uinion of unsigned and signed native integer</summary>
    ''' <remarks>You can use this union for bitwise operations</remarks>
    ''' <version version="1.5.4">This structure is new in version 1.5.4</version>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure NativeUnion
#Region "Fields"
        ''' <summary>Signed native integer value</summary>
        Public signed As IntPtr
        ''' <summary>Unsigned native integer value</summary>
        <CLSCompliant(False)>
        <FieldOffset(0)> Public unsigned As UIntPtr
#End Region
#Region "CTors"
        ''' <summary>CTor - creates a new instance of the <see cref="NativeUnion"/> structure from <see cref="IntPtr"/> value</summary>
        ''' <param name="value">A value to populate this structure with</param>
        Public Sub New(value As IntPtr)
            signed = value
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="NativeUnion"/> structure from <see cref="IntPtr"/> value</summary>
        ''' <param name="value">A value to populate this structure with</param>
        <CLSCompliant(False)>
        Public Sub New(value As UIntPtr)
            unsigned = value
        End Sub
#End Region
#Region "Operators"
        ''' <summary>Converts <see cref="IntPtr"/> to <see  cref="NativeUnion"/></summary>
        ''' <param name="a">A <see cref="IntPtr"/></param>
        ''' <returns>A <see cref="NativeUnion"/> with <see cref="Signed"/> initialized to <paramref name="a"/></returns>
        Public Shared Widening Operator CType(a As IntPtr) As NativeUnion
            Return New NativeUnion(a)
        End Operator
        ''' <summary>Converts <see cref="UIntPtr"/> to <see  cref="NativeUnion"/></summary>
        ''' <param name="a">A <see cref="UIntPtr"/></param>
        ''' <returns>A <see cref="NativeUnion"/> with <see cref="Unsigned"/> initialized to <paramref name="a"/></returns>
        <CLSCompliant(False)>
        Public Shared Widening Operator CType(a As UIntPtr) As NativeUnion
            Return New NativeUnion(a)
        End Operator
        ''' <summary>Converts <see cref="NativeUnion"/> to <see cref="IntPtr"/></summary>
        ''' <param name="a">A <see cref="NativeUnion"/></param>
        ''' <returns><paramref name="a"/>.<see cref="signed">signed</see></returns>
        Public Shared Widening Operator CType(a As NativeUnion) As IntPtr
            Return a.signed
        End Operator
        ''' <summary>Converts <see cref="NativeUnion"/> to <see cref="UIntPtr"/></summary>
        ''' <param name="a">A <see cref="NativeUnion"/></param>
        ''' <returns><paramref name="a"/>.<see cref="unsigned">unsigned</see></returns>
        <CLSCompliant(False)>
        Public Shared Widening Operator CType(a As NativeUnion) As UIntPtr
            Return a.unsigned
        End Operator
        ''' <summary>Compares two <see cref="NativeUnion"/> objects for equality</summary>
        ''' <param name="a">A <see cref="NativeUnion"/></param>
        ''' <param name="b">A <see cref="NativeUnion"/></param>
        ''' <returns>True if <paramref name="a"/> and <paramref name="b"/> are equal, false if they are not</returns>
        Public Shared Operator =(a As NativeUnion, b As NativeUnion) As Boolean
            Return a.signed = b.signed
        End Operator
        ''' <summary>Compares two <see cref="NativeUnion"/> objects for inequality</summary>
        ''' <param name="a">A <see cref="NativeUnion"/></param>
        ''' <param name="b">A <see cref="NativeUnion"/></param>
        ''' <returns>False if <paramref name="a"/> and <paramref name="b"/> are equal, true if they are not</returns>
        Public Shared Operator <>(a As NativeUnion, b As NativeUnion) As Boolean
            Return a.signed <> b.signed
        End Operator
#End Region
#Region "Overrides"
        ''' <summary>Returns the hash code for this instance.</summary>
        ''' <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function GetHashCode() As Integer
            Return signed.GetHashCode
        End Function
        ''' <summary>Indicates whether this instance and a specified object are equal.</summary>
        ''' <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.</returns>
        ''' <param name="obj">Another object to compare to. </param>
        ''' <filterpriority>2</filterpriority>
        Public Overloads Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is NativeUnion Then Return Me = DirectCast(obj, NativeUnion)
            If TypeOf obj Is IntPtr Then Return Me.signed = DirectCast(obj, IntPtr)
            If TypeOf obj Is UIntPtr Then Return Me.unsigned = DirectCast(obj, UIntPtr)
            Return False
        End Function
        ''' <summary>Returns the fully qualified type name of this instance.</summary>
        ''' <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function ToString() As String
            Return String.Format("{0}/{1}; {0:X}/{1:X}", Me.signed, Me.unsigned)
        End Function
#End Region
    End Structure
End Namespace
#End If