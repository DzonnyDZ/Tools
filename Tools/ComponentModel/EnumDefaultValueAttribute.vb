#If Config <= Nightly Then 'Stage: Nightly
Namespace ComponentModelT
    ''' <summary>Stores default value of property of enumeration type</summary>
    ''' <version stage="Nightly" version="1.5.2">Class introduced</version>
    Public Class EnumDefaultValueAttribute
        Inherits DefaultValueAttribute
        ''' <summary>CTor for <see cref="SByte"/> enumeration</summary>
        ''' <param name="EnumValue">Default value in enumeration underlying type</param>
        ''' <param name="EnumType">Type of enumeration</param>
        ''' <exception cref="ArgumentNullException"><paramref name="EnumType"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="EnumType"/> is not enumeration type -or- <paramref name="EnumValue"/> cannot be represented in enumeration underlying type</exception>
        ''' <remarks>You shoudl use appropriate constructor depending on underlying type of enumeration</remarks>
        <CLSCompliant(False)> Public Sub New(ByVal EnumValue As SByte, ByVal EnumType As Type)
            MyBase.New([Enum].ToObject(EnumType, EnumValue))
        End Sub
        ''' <summary>CTor for <see cref="Byte"/> enumeration</summary>
        ''' <param name="EnumValue">Default value in enumeration underlying type</param>
        ''' <param name="EnumType">Type of enumeration</param>
        ''' <exception cref="ArgumentNullException"><paramref name="EnumType"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="EnumType"/> is not enumeration type -or- <paramref name="EnumValue"/> cannot be represented in enumeration underlying type</exception>
        ''' <remarks>You shoudl use appropriate constructor depending on underlying type of enumeration</remarks>
        Public Sub New(ByVal EnumValue As Byte, ByVal EnumType As Type)
            MyBase.New([Enum].ToObject(EnumType, EnumValue))
        End Sub
        ''' <summary>CTor for <see cref="Short"/> enumeration</summary>
        ''' <param name="EnumValue">Default value in enumeration underlying type</param>
        ''' <param name="EnumType">Type of enumeration</param>
        ''' <exception cref="ArgumentNullException"><paramref name="EnumType"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="EnumType"/> is not enumeration type -or- <paramref name="EnumValue"/> cannot be represented in enumeration underlying type</exception>
        ''' <remarks>You shoudl use appropriate constructor depending on underlying type of enumeration</remarks>
        Public Sub New(ByVal EnumValue As Short, ByVal EnumType As Type)
            MyBase.New([Enum].ToObject(EnumType, EnumValue))
        End Sub
        ''' <summary>CTor for <see cref="UShort"/> enumeration</summary>
        ''' <param name="EnumValue">Default value in enumeration underlying type</param>
        ''' <param name="EnumType">Type of enumeration</param>
        ''' <exception cref="ArgumentNullException"><paramref name="EnumType"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="EnumType"/> is not enumeration type -or- <paramref name="EnumValue"/> cannot be represented in enumeration underlying type</exception>
        ''' <remarks>You shoudl use appropriate constructor depending on underlying type of enumeration</remarks>
        <CLSCompliant(False)> Public Sub New(ByVal EnumValue As UShort, ByVal EnumType As Type)
            MyBase.New([Enum].ToObject(EnumType, EnumValue))
        End Sub
        ''' <summary>CTor for <see cref="Int32"/> enumeration</summary>
        ''' <param name="EnumValue">Default value in enumeration underlying type</param>
        ''' <param name="EnumType">Type of enumeration</param>
        ''' <exception cref="ArgumentNullException"><paramref name="EnumType"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="EnumType"/> is not enumeration type -or- <paramref name="EnumValue"/> cannot be represented in enumeration underlying type</exception>
        ''' <remarks>You shoudl use appropriate constructor depending on underlying type of enumeration</remarks>
        Public Sub New(ByVal EnumValue As Integer, ByVal EnumType As Type)
            MyBase.New([Enum].ToObject(EnumType, EnumValue))
        End Sub
        ''' <summary>CTor for <see cref="UInt32"/> enumeration</summary>
        ''' <param name="EnumValue">Default value in enumeration underlying type</param>
        ''' <param name="EnumType">Type of enumeration</param>
        ''' <exception cref="ArgumentNullException"><paramref name="EnumType"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="EnumType"/> is not enumeration type -or- <paramref name="EnumValue"/> cannot be represented in enumeration underlying type</exception>
        ''' <remarks>You shoudl use appropriate constructor depending on underlying type of enumeration</remarks>
        <CLSCompliant(False)> Public Sub New(ByVal EnumValue As UInteger, ByVal EnumType As Type)
            MyBase.New([Enum].ToObject(EnumType, EnumValue))
        End Sub
        ''' <summary>CTor for <see cref="Long"/> enumeration</summary>
        ''' <param name="EnumValue">Default value in enumeration underlying type</param>
        ''' <param name="EnumType">Type of enumeration</param>
        ''' <exception cref="ArgumentNullException"><paramref name="EnumType"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="EnumType"/> is not enumeration type -or- <paramref name="EnumValue"/> cannot be represented in enumeration underlying type</exception>
        ''' <remarks>You shoudl use appropriate constructor depending on underlying type of enumeration</remarks>
        Public Sub New(ByVal EnumValue As Long, ByVal EnumType As Type)
            MyBase.New([Enum].ToObject(EnumType, EnumValue))
        End Sub
        ''' <summary>CTor for <see cref="ULong"/> enumeration</summary>
        ''' <param name="EnumValue">Default value in enumeration underlying type</param>
        ''' <param name="EnumType">Type of enumeration</param>
        ''' <exception cref="ArgumentNullException"><paramref name="EnumType"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="EnumType"/> is not enumeration type -or- <paramref name="EnumValue"/> cannot be represented in enumeration underlying type</exception>
        ''' <remarks>You shoudl use appropriate constructor depending on underlying type of enumeration</remarks>
        <CLSCompliant(False)> Public Sub New(ByVal EnumValue As ULong, ByVal EnumType As Type)
            MyBase.New([Enum].ToObject(EnumType, EnumValue))
        End Sub
        ''' <summary>CTro from enumerated value</summary>
        ''' <param name="EnumValue">Enumerated value</param>
        Public Sub New(ByVal EnumValue As [Enum])
            MyBase.New(EnumValue)
        End Sub
    End Class
End Namespace
#End If