Imports System.Runtime.CompilerServices

#If Config <= Nightly Then 'Stage:Nightly
<Author("Ðonny", "dzonny.dz@.gmail.com", "http://dzonny.cz")> _
<Version(1, 0, GetType(TypeTools))> _
Public Module TypeTools
    ''' <summary>Checks if specified value is member of an enumeration</summary>
    ''' <param name="value">Value to be chcked</param>
    ''' <returns>True if <paramref name="value"/> is member of <paramref name="T"/></returns>
    ''' <typeparam name="T">Enumeration to be tested</typeparam>
    ''' <exception cref="ArgumentException"><paramref name="T"/> is not <see cref="[Enum]"/></exception>
    <CLSCompliant(False)> _
    Public Function InEnum(Of T As {IConvertible, Structure})(ByVal value As T) As Boolean
        Return Array.IndexOf([Enum].GetValues(GetType(T)), value) >= 0
    End Function
    ''' <summary>Gets <see cref="Reflection.FieldInfo"/> that represent given constant within an enum</summary>
    ''' <param name="value">Constant to be found</param>
    ''' <returns><see cref="Reflection.FieldInfo"/> of given <paramref name="value"/> if <paramref name="value"/> is member of <paramref name="T"/></returns>
    ''' <typeparam name="T"><see cref="[Enum]"/> to found constant within</typeparam>
    ''' <exception cref="ArgumentException"><paramref name="T"/> is not <see cref="[Enum]"/></exception>
    ''' <exception cref="ArgumentNullException"><paramref name="value"/> is not member of <paramref name="T"/></exception>
    <CLSCompliant(False)> _
    Public Function GetConstant(Of T As {IConvertible, Structure})(ByVal value As T) As Reflection.FieldInfo
        Return GetType(T).GetField([Enum].GetName(GetType(T), value))
    End Function
    ''' <summary>Gets first <see cref="Attribute"/> of specified type from specified <see cref="Reflection.ICustomAttributeProvider"/></summary>
    ''' <param name="From"><see cref="Reflection.ICustomAttributeProvider"/> to get <see cref="Attribute"/> from</param>
    ''' <param name="Inherit">When true, look up the hierarchy chain for the inherited custom attribute.</param>
    ''' <returns>First attribute returned by <see cref="Reflection.ICustomAttributeProvider.GetCustomAttributes"/> or null if no attribute is returned</returns>
    ''' <typeparam name="T">Type of <see cref="Attribute"/> to get</typeparam>
#If VBC_VER >= 9 Then
    <Extension()> _
    Public Function GetAttribute(Of T As Attribute)(ByVal From As Reflection.ICustomAttributeProvider, Optional ByVal Inherit As Boolean = True) As T
#Else
    Public Function GetAttribute(Of T As Attribute)(ByVal From As Reflection.ICustomAttributeProvider, Optional ByVal Inherit As Boolean = True) As T
#End If
        Dim attrs As Object() = From.GetCustomAttributes(GetType(T), Inherit)
        If attrs Is Nothing OrElse attrs.Length = 0 Then Return Nothing Else Return attrs(0)
    End Function
    ''' <summary>Converts specified value to underlying type of specified enumeration (type-safe)</summary>
    ''' <param name="value"><see cref="IConvertible"/> to be converted using invariant culture</param>
    ''' <typeparam name="T">Type of enumeration (must derive from <see cref="System.Enum"/>)</typeparam>
    ''' <exception cref="System.ArgumentException"><paramref name="T"/> is not an <see cref="System.Enum"/> -or- Underlying type of <paramref name="Type"/> is neither <see cref="Byte"/>, <see cref="SByte"/>, <see cref="Short"/>, <see cref="UShort"/>, <see cref="Integer"/>, <see cref="UInteger"/>, <see cref="Long"/> nor <see cref="ULong"/></exception>
    <CLSCompliant(False)> _
    Public Function GetValueInEnumBaseType(Of T As {IConvertible, Structure})(ByVal value As IConvertible) As IConvertible
        Return GetValueInEnumBaseType(GetType(T), value)
    End Function
    ''' <summary>Converts specified value to underlying type of specified enumeration (type-unsafe)</summary>
    ''' <param name="value"><see cref="IConvertible"/> to be converted using invariant culture</param>
    ''' <param name="Type">Type of enumeration (must derive from <see cref="System.Enum"/>)</param>
    ''' <exception cref="System.ArgumentNullException"><paramref name="Type"/> is null.</exception>
    ''' <exception cref="System.ArgumentException"><paramref name="Type"/> is not an <see cref="System.Enum"/> -or- Underlying type of <paramref name="Type"/> is neither <see cref="Byte"/>, <see cref="SByte"/>, <see cref="Short"/>, <see cref="UShort"/>, <see cref="Integer"/>, <see cref="UInteger"/>, <see cref="Long"/> nor <see cref="ULong"/></exception>
    <CLSCompliant(False)> _
    Public Function GetValueInEnumBaseType(ByVal Type As Type, ByVal Value As IConvertible) As IConvertible
        Dim EType As Type = [Enum].GetUnderlyingType(Type)
        If GetType(Byte).Equals(EType) Then : Return Value.ToByte(System.Globalization.CultureInfo.InvariantCulture)
        ElseIf GetType(SByte).Equals(EType) Then : Return Value.ToSByte(System.Globalization.CultureInfo.InvariantCulture)
        ElseIf GetType(Short).Equals(EType) Then : Return Value.ToInt16(System.Globalization.CultureInfo.InvariantCulture)
        ElseIf GetType(UShort).Equals(EType) Then : Return Value.ToUInt16(System.Globalization.CultureInfo.InvariantCulture)
        ElseIf GetType(Integer).Equals(EType) Then : Return Value.ToInt32(System.Globalization.CultureInfo.InvariantCulture)
        ElseIf GetType(UInteger).Equals(EType) Then : Return Value.ToUInt32(System.Globalization.CultureInfo.InvariantCulture)
        ElseIf GetType(Long).Equals(EType) Then : Return Value.ToInt64(System.Globalization.CultureInfo.InvariantCulture)
        ElseIf GetType(ULong).Equals(EType) Then : Return Value.ToUInt64(System.Globalization.CultureInfo.InvariantCulture)
        Else : Throw New ArgumentException(String.Format("Unknown underlying type {0}", EType.Name))
        End If
    End Function
    ''' <summary>Gets name of given enumeration value</summary>
    ''' <param name="value">Value to get name of</param>
    ''' <returns>Name of value in enumeration or null if there is no constant with given value</returns>
    <Extension()> Public Function GetName(ByVal value As [Enum]) As String
        Return [Enum].GetName(value.GetType, value)
    End Function
    ''' <summary>Gets constant field that represents given enum value</summary>
    ''' <param name="value">Value to get constant of</param>
    ''' <returns><see cref="Reflection.FieldInfo"/> with constant value equal to <paramref name="value"/> or null if such field does not exist</returns>
    <Extension()> Public Function GetConstant(ByVal value As [Enum]) As System.Reflection.FieldInfo
        Dim name = [Enum].GetName(value.GetType, value)
        If name Is Nothing Then Return Nothing
        Return value.GetType.GetField(name)
    End Function
    ''' <summary>Gets value of enum in its unedlying type</summary>
    ''' <param name="value">Enumeration value</param>
    ''' <returns>Value of enum in its underlying type (so it no longer derives from <see cref="System.[Enum]"/>)</returns>
    <Extension(), CLSCompliant(False)> Public Function GetValue(ByVal value As [Enum]) As IConvertible
        Return GetValueInEnumBaseType(value.GetType, value)
    End Function
    ''' <summary>Converts specified <see cref="IConvertible"/> to specified <see cref="[Enum]"/> (type-safe)</summary>
    ''' <param name="value"><see cref="IConvertible"/> to be converted using invariant culture</param>
    ''' <typeparam name="T">Type of enumeration (must derive from <see cref="System.Enum"/>)</typeparam>
    ''' <exception cref="System.ArgumentException"><paramref name="T"/> is not an <see cref="System.Enum"/> -or- Underlying type of <paramref name="Type"/> is neither <see cref="Byte"/>, <see cref="SByte"/>, <see cref="Short"/>, <see cref="UShort"/>, <see cref="Integer"/>, <see cref="UInteger"/>, <see cref="Long"/> nor <see cref="ULong"/></exception>
    <CLSCompliant(False)> _
    Public Function GetEnumValue(Of T As {IConvertible, Structure})(ByVal Value As IConvertible) As T
        Return CObj(GetEnumValue(GetType(T), Value))
    End Function
    ''' <summary>Converts specified <see cref="IConvertible"/> to specified <see cref="[Enum]"/> (type-unsafe)</summary>
    ''' <param name="value"><see cref="IConvertible"/> to be converted using invariant culture</param>
    ''' <param name="Type">Type of enumeration (must derive from <see cref="System.Enum"/>)</param>
    ''' <exception cref="System.ArgumentNullException"><paramref name="Type"/> is null.</exception>
    ''' <exception cref="System.ArgumentException"><paramref name="Type"/> is not an <see cref="System.Enum"/> -or- Underlying type of <paramref name="Type"/> is neither <see cref="Byte"/>, <see cref="SByte"/>, <see cref="Short"/>, <see cref="UShort"/>, <see cref="Integer"/>, <see cref="UInteger"/>, <see cref="Long"/> nor <see cref="ULong"/></exception>
    <CLSCompliant(False)> _
    Public Function GetEnumValue(ByVal Type As Type, ByVal Value As IConvertible) As [Enum]
        Return [Enum].ToObject(Type, GetValueInEnumBaseType(Type, Value))
    End Function
End Module
<CLSCompliant(False)> _
Public Module __ASAP_Delete
    <Extension()> Public Function IsDefined(Of T As System.Exception )(ByVal value As [Enum]) As Boolean
        Return Array.IndexOf([Enum].GetValues(GetType(T)), value) >= 0
    End Function
End Module
#End If