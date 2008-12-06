Imports System.Runtime.CompilerServices, System.Linq
Imports Tools.ReflectionT, Tools.ExtensionsT
Imports System.Reflection

#If Config <= Nightly Then 'Stage:Nightly
''' <author www="http://dzonny.cz">Đonny</author>
''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
Public Module TypeTools
    ''' <summary>Checks if specified value is member of an enumeration</summary>
    ''' <param name="value">Value to be chcked</param>
    ''' <returns>True if <paramref name="value"/> is member of <paramref name="T"/></returns>
    ''' <typeparam name="T">Enumeration to be tested</typeparam>
    ''' <exception cref="ArgumentException"><paramref name="T"/> is not <see cref="[Enum]"/></exception>
    ''' <seelaso cref="IsDefined"/>
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
    <Extension()> _
     Public Function GetAttribute(Of T As Attribute)(ByVal From As Reflection.ICustomAttributeProvider, Optional ByVal Inherit As Boolean = True) As T
        Dim attrs As Object() = From.GetCustomAttributes(GetType(T), Inherit)
        If attrs Is Nothing OrElse attrs.Length = 0 Then Return Nothing Else Return attrs(0)
    End Function
    ''' <summary>Gets all <see cref="Attribute"/>s of specified type from specified <see cref="Reflection.ICustomAttributeProvider"/></summary>
    ''' <param name="From"><see cref="Reflection.ICustomAttributeProvider"/> to get <see cref="Attribute"/> from</param>
    ''' <param name="Inherit">When true, look up the hierarchy chain for the inherited custom attribute.</param>
    ''' <returns>All attributes returned by <see cref="Reflection.ICustomAttributeProvider.GetCustomAttributes"/> or null if no attribute is returned</returns>
    ''' <typeparam name="T">Type of <see cref="Attribute"/> to get</typeparam>
    <Extension()> _
    Public Function GetAttributes(Of T As Attribute)(ByVal From As Reflection.ICustomAttributeProvider, Optional ByVal Inherit As Boolean = True) As T()

        Dim attrs As Object() = From.GetCustomAttributes(GetType(T), Inherit)
        If attrs Is Nothing Then Return Nothing
        Return (From Attr In attrs Select DirectCast(Attr, T)).ToArray
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
        Else : Throw New ArgumentException(String.Format(ResourcesT.Exceptions.UnknownUnderlyingType0, EType.Name))
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
    ''' <summary>Gets <see cref="Reflection.FieldInfo"/> representing constant of given name from an enumeration</summary>
    ''' <param name="name">Name of constant to get</param>
    ''' <param name="EnumType">Type of enumeration</param>
    ''' <returns><see cref="Reflection.FieldInfo"/> that represents constant enum member of type <paramref name="EnumType"/> with name <paramref name="name"/>. Null if such constant doesnot exists.</returns>
    ''' <exception cref="ArgumentException"><paramref name="EnumType"/> is not enumeration</exception>
    Public Function GetConstant(ByVal name As String, ByVal EnumType As Type) As Reflection.FieldInfo
        If Not EnumType.IsEnum Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.MustBeEnumeration, "Type"), "Type")
        Dim field = EnumType.GetField(name, Reflection.BindingFlags.Static Or Reflection.BindingFlags.Public)
        If field Is Nothing Then Return Nothing
        If Not field.IsLiteral Then Return Nothing
        Return field
    End Function
    ''' <summary>Gets <see cref="Reflection.FieldInfo"/> representing constant of given name from an enumeration</summary>
    ''' <param name="name">Name of constant to get</param>
    ''' <typeparam name="T">Type of enumeration</typeparam>
    ''' <returns><see cref="Reflection.FieldInfo"/> that represents constant enum member of type <paramref name="EnumType"/> with name <paramref name="name"/>. Null if such constant doesnot exists.</returns>
    ''' <exception cref="ArgumentException"><typeparamref name="T"/> is not enumeration</exception>
    <CLSCompliant(False)> _
    Public Function GetConstant(Of T As {Structure, IConvertible})(ByVal name$) As Reflection.FieldInfo
        Return GetConstant(name, GetType(T))
    End Function
    ''' <summary>Gets value of enum in its unedlying type</summary>
    ''' <param name="value">Enumeration value</param>
    ''' <returns>Value of enum in its underlying type (so it no longer derives from <see cref="System.[Enum]"/>)</returns>
    <Extension(), CLSCompliant(False)> Public Function GetValue(ByVal value As [Enum]) As IConvertible
        Return GetValueInEnumBaseType(value.GetType, value)
    End Function
    ''' <summary>Gets value of enum member</summary>
    ''' <param name="name">Name of enumeration memebr</param>
    ''' <param name="EnumType">Type of enumeration</param>
    ''' <exception cref="ArgumentException"><paramref name="EnumType"/> is not enumeration =or= Constant with name <paramref name="name"/> does not exist in enumeration <paramref name="EnumType"/>.</exception>
    ''' <returns>Value of constant with name <paramref name="name"/> in type <paramref name="EnumType"/></returns>
    Public Function GetValue(ByVal name$, ByVal EnumType As Type) As [Enum]
        Dim cns = GetConstant(name, EnumType)
        If cns Is Nothing Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.Constant0DoesNotExistInType1, name, EnumType.Name))
        Return cns.GetValue(Nothing)
    End Function
    ''' <summary>Gets value of enum member</summary>
    ''' <param name="name">Name of enumeration memebr</param>
    ''' <typeparam name="T">Type of enumeration</typeparam>
    ''' <exception cref="ArgumentException"><typeparamref name="T"/> is not enumeration =or= Constant with name <paramref name="name"/> does not exist in enumeration <typeparamref name="T"/>.</exception>
    ''' <returns>Value of constant with name <paramref name="name"/> in type <typeparamref name="T"/></returns>
    <CLSCompliant(False)> _
    Public Function GetValue(Of T As {IConvertible, Structure})(ByVal name As String) As T
        Return GetValue(name, GetType(T)).GetValue
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
    ''' <summary>Gets value idicating if given value is defined as constant in enumeration</summary>
    ''' <param name="value">Value to be checked. Value must be to type of enumeration to be checked in</param>
    ''' <returns>True if <paramref name="value"/> is defined as constant in enumeration of type of <paramref name="value"/></returns>
    ''' <remarks>Assembly Tools IL contains more type-safe generic extension function IsDefined. This is comanion function to <see cref="InEnum"/>.</remarks>
    <Extension()> Public Function IsDefined(ByVal value As [Enum]) As Boolean
        Return Array.IndexOf([Enum].GetValues(value.GetType), value) >= 0
    End Function

    ''' <summary>Converts set of flags separated by separator to value of given enumeration</summary>
    ''' <param name="Flags">Flags to parse. Each flag can be name or number</param>
    ''' <param name="EnumType">Type fo parse flags into</param>
    ''' <param name="Separator">Separator of flags</param>
    ''' <returns>Returns value of type <paramref name="EnumType"/></returns>
    ''' <exception cref="ArgumentException"><paramref name="EnumType"/> is not enumeration =or= any flag cannot be found as member of <paramref name="EnumType"/></exception>
    Public Function FlagsFromString(ByVal Flags As String, ByVal EnumType As Type, ByVal Separator As String) As [Enum]
        If Not EnumType.IsEnum Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.MustBeEnumeration, "EnumType"), "EnumType")
        If [Enum].GetUnderlyingType(EnumType).Equals(GetType(SByte)) OrElse [Enum].GetUnderlyingType(EnumType).Equals(GetType(Short)) OrElse [Enum].GetUnderlyingType(EnumType).Equals(GetType(Integer)) OrElse [Enum].GetUnderlyingType(EnumType).Equals(GetType(Long)) Then
            Dim ret As Long = 0
            For Each item In Flags.Split(Separator)
                If IsNumeric(item) Then ret = ret Or CLng(item) Else _
                ret = ret Or CType(GetValue(item, EnumType).GetValue, Long)
            Next
            Return [Enum].ToObject(EnumType, GetValueInEnumBaseType(EnumType, ret))
        Else
            Dim ret As ULong = 0
            For Each item In Flags.Split(Separator)
                If IsNumeric(item) AndAlso CDec(item) >= 0 Then : ret = ret Or CULng(item)
                ElseIf IsNumeric(item) Then : Throw New ArgumentException(ResourcesT.Exceptions.EnumerationDoesNotAllowNegativeValues)
                Else : ret = ret Or CType(GetValue(item, EnumType).GetValue, ULong) : End If
            Next
            Return [Enum].ToObject(EnumType, GetValueInEnumBaseType(EnumType, ret))
        End If
    End Function
    ''' <summary>Converts set of flags separated by separator to value of given enumeration</summary>
    ''' <param name="Flags">Flags to parse. Each flag can be name or number</param>
    ''' <param name="EnumType">Type fo parse flags into</param>
    ''' <param name="Culture">Culture to obtain separator from</param>
    ''' <returns>Returns value of type <paramref name="EnumType"/></returns>
    ''' <exception cref="ArgumentException"><paramref name="EnumType"/> is not enumeration =or= any flag cannot be found as member of <paramref name="EnumType"/></exception>
    Public Function FlagsFromString(ByVal Flags As String, ByVal EnumType As Type, ByVal Culture As Globalization.CultureInfo) As [Enum]
        Return FlagsFromString(Flags, EnumType, Culture.TextInfo)
    End Function
    ''' <summary>Converts set of flags separated by separator to value of given enumeration</summary>
    ''' <param name="Flags">Flags to parse. Each flag can be name or number</param>
    ''' <param name="EnumType">Type fo parse flags into</param>
    ''' <param name="TextInfo"><see cref="Globalization.TextInfo"/> to obtain separator from</param>
    ''' <returns>Returns value of type <paramref name="EnumType"/></returns>
    ''' <exception cref="ArgumentException"><paramref name="EnumType"/> is not enumeration =or= any flag cannot be found as member of <paramref name="EnumType"/></exception>
    Public Function FlagsFromString(ByVal Flags As String, ByVal EnumType As Type, ByVal TextInfo As Globalization.TextInfo) As [Enum]
        Return FlagsFromString(Flags, EnumType, TextInfo.ListSeparator)
    End Function
    ''' <summary>Converts set of flags separated by separator to value of given enumeration</summary>
    ''' <param name="Flags">Flags to parse. Each flag can be name or number</param>
    ''' <param name="EnumType">Type fo parse flags into</param>
    ''' <returns>Returns value of type <paramref name="EnumType"/></returns>
    ''' <exception cref="ArgumentException"><paramref name="EnumType"/> is not enumeration =or= any flag cannot be found as member of <paramref name="EnumType"/></exception>
    ''' <remarks>Obtains separator from current culture</remarks>
    Public Function FlagsFromString(ByVal Flags As String, ByVal EnumType As Type) As [Enum]
        Return FlagsFromString(Flags, EnumType, Globalization.CultureInfo.CurrentCulture)
    End Function

    ''' <summary>Converts set of flags separated by separator to value of given enumeration</summary>
    ''' <param name="Flags">Flags to parse. Each flag can be name or number</param>
    ''' <typeparam name="T">Type fo parse flags into</typeparam>
    ''' <param name="Separator">Separator of flags</param>
    ''' <returns>Returns value of type <paramref name="EnumType"/></returns>
    ''' <exception cref="ArgumentException"><typeparamref name="T"/> is not enumeration =or= any flag cannot be found as member of <paramref name="EnumType"/></exception>
    <CLSCompliant(False)> _
    Public Function FlagsFromString(Of T As {Structure, IConvertible})(ByVal Flags As String, ByVal Separator As String) As T
        Return CObj(FlagsFromString(Flags, GetType(T), Separator))
    End Function
    ''' <summary>Converts set of flags separated by separator to value of given enumeration</summary>
    ''' <param name="Flags">Flags to parse. Each flag can be name or number</param>
    ''' <typeparam name="T">Type fo parse flags into</typeparam>
    ''' <param name="Culture">Culture to obtain separator from</param>
    ''' <returns>Returns value of type <paramref name="EnumType"/></returns>
    ''' <exception cref="ArgumentException"><typeparamref name="T"/> is not enumeration =or= any flag cannot be found as member of <paramref name="EnumType"/></exception>
    <CLSCompliant(False)> _
    Public Function FlagsFromString(Of T As {Structure, IConvertible})(ByVal Flags As String, ByVal Culture As Globalization.CultureInfo) As T
        Return CObj(FlagsFromString(Flags, GetType(T), Culture.TextInfo))
    End Function
    ''' <summary>Converts set of flags separated by separator to value of given enumeration</summary>
    ''' <param name="Flags">Flags to parse. Each flag can be name or number</param>
    ''' <typeparam name="T">Type fo parse flags into</typeparam>
    ''' <param name="TextInfo"><see cref="Globalization.TextInfo"/> to obtain separator from</param>
    ''' <returns>Returns value of type <paramref name="EnumType"/></returns>
    ''' <exception cref="ArgumentException"><typeparamref name="T"/> is not enumeration =or= any flag cannot be found as member of <paramref name="EnumType"/></exception>
    <CLSCompliant(False)> _
    Public Function FlagsFromString(Of T As {Structure, IConvertible})(ByVal Flags As String, ByVal TextInfo As Globalization.TextInfo) As T
        Return CObj(FlagsFromString(Flags, GetType(T), TextInfo.ListSeparator))
    End Function
    ''' <summary>Converts set of flags separated by separator to value of given enumeration</summary>
    ''' <param name="Flags">Flags to parse. Each flag can be name or number</param>
    ''' <typeparam name="T">Type fo parse flags into</typeparam>
    ''' <returns>Returns value of type <paramref name="EnumType"/></returns>
    ''' <exception cref="ArgumentException"><typeparamref name="T"/> is not enumeration =or= any flag cannot be found as member of <paramref name="EnumType"/></exception>
    ''' <remarks>Obtains separator from current culture</remarks>
    <CLSCompliant(False)> _
    Public Function FlagsFromString(Of T As {Structure, IConvertible})(ByVal Flags As String) As T
        Return CObj(FlagsFromString(Flags, GetType(T), Globalization.CultureInfo.CurrentCulture))
    End Function
    ''' <summary>Gets toolbox bitmap assciated with given <see cref="Type"/></summary>
    ''' <param name="Type">Type to get toolbox bitmap for</param>
    ''' <param name="Large">True to obtain large bitmap (32×32), false to obtain small one (16×16))</param>
    ''' <param name="Inherit">True to allow inheriting of toolbox bitmap from base type of <paramref name="Type"/></param>
    ''' <returns>Bitmap assciated with <see cref="Type"/> if any</returns>
    ''' <remarks>If <paramref name="Type"/> is decorated with <see cref="Drawing.ToolboxBitmapAttribute"/> then it is used. If not static method <see cref="Drawing.ToolboxBitmapAttribute.GetImageFromResource"/> is used with <see cref="Type.Name"/> of <paramref name="Type"/>.</remarks>
    ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
    <Extension()> _
    Public Function GetToolBoxBitmap(ByVal Type As Type, Optional ByVal Large As Boolean = False, Optional ByVal Inherit As Boolean = False) As Drawing.Image
        If Type Is Nothing Then Throw New ArgumentException("Type")
        Dim attr = Type.GetAttribute(Of Drawing.ToolboxBitmapAttribute)(False)
        If attr Is Nothing AndAlso Inherit Then attr = Type.GetAttribute(Of Drawing.ToolboxBitmapAttribute)(True)
        If attr IsNot Nothing Then
            Return attr.GetImage(Type, Large)
        Else
            Dim bmp = Drawing.ToolboxBitmapAttribute.GetImageFromResource(Type, Type.Name, Large)
            If bmp IsNot Nothing Then Return bmp
        End If
        If Inherit AndAlso Not Type.Equals(GetType(Object)) AndAlso Type.BaseType IsNot Nothing Then
            Return Type.BaseType.GetToolBoxBitmap(Large, Inherit)
        Else
            Return Nothing
        End If
    End Function
    ''' <summary>Gets default CTor for given type</summary>
    ''' <param name="Type"><see cref="Type"/> to get default CTor for</param>
    ''' <param name="Attributes">Optionaly specifies aaccesibility attributes for default constructor. Default is <see cref="Reflection.BindingFlags.[Public]"/>.</param>
    ''' <returns><see cref="Reflection.ConstructorInfo"/> refresenting default CTor of type <paramref name="Type"/>. Null if there is no default (parameter-less) CTor</returns>
    ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
    ''' <seealso cref="HasDefaultCTor"/>
    ''' <version stage="Nightly" version="1.5.2">Fixed: Always returns null due to <paramref name="Attributes"/> being and-ed with <see cref="Reflection.BindingFlags.Instance"/> instead of or-ed</version>
    <Extension()> _
    Public Function GetDefaltCTor(ByVal Type As Type, Optional ByVal Attributes As Reflection.BindingFlags = Reflection.BindingFlags.Public) As Reflection.ConstructorInfo
        If Type Is Nothing Then Throw New ArgumentException("Type")
        Return Type.GetConstructor(Attributes Or Reflection.BindingFlags.Instance, Nothing, Type.EmptyTypes, Nothing)
    End Function
    ''' <summary>Gets value indicationg if given <see cref="Type"/> has default constructor</summary>
    ''' <param name="Type"><see cref="Type"/> to check</param>
    ''' <param name="Attributes">Optionaly specifies aaccesibility attributes for default constructor. Default is <see cref="Reflection.BindingFlags.[Public]"/>.</param>
    ''' <remarks>True if type has default (parameterless) CTor, fale otherwise.</remarks>
    ''' <seealso cref="GetDefaltCTor"/>
    ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
    <Extension()> _
    Public Function HasDefaultCTor(ByVal Type As Type, Optional ByVal Attributes As Reflection.BindingFlags = Reflection.BindingFlags.Public) As Boolean
        If Type Is Nothing Then Throw New ArgumentException("Type")
        Return Type.GetDefaltCTor(Attributes) IsNot Nothing
    End Function
    ''' <summary>Gets value indicating if instance of geven type can be easily created using default CTor</summary>
    ''' <param name="Type"><see cref="Type"/> to check</param>
    ''' <returns>False if type is either interface, abstract or open; true if type has default contructor or is value type</returns>
    ''' <seealso cref="HasDefaultCTor"/>
    ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
    <Extension()> _
    Public Function CanAutomaticallyCreateInstance(ByVal Type As Type) As Boolean
        If Type Is Nothing Then Throw New ArgumentException("Type")
        If Type.IsInterface Then Return False
        If Type.IsAbstract Then Return False
        If Type.IsGenericTypeDefinition Then Return False
        If Type.IsValueType Then Return True
        If Type.HasDefaultCTor Then Return True
    End Function
    ''' <summary>Creates an instance of the specified type using that type's default constructor.</summary>
    ''' <param name="type">The type of object to create.</param>
    ''' <returns>A reference to the newly created object.</returns>
    ''' <exception cref="System.ArgumentNullException"><paramref name="type"/> is null.</exception>
    ''' <exception cref="System.ArgumentException"><paramref name="type"/> is not a RuntimeType. -or- <paramref name="type"/> is an open generic type (that is, the <see cref="System.Type.ContainsGenericParameters" /> property returns true).</exception>
    ''' <exception cref="System.NotSupportedException"><paramref name="type"/> cannot be a <see cref="System.Reflection.Emit.TypeBuilder" />.  -or- Creation of <see cref="System.TypedReference" />, <see cref="System.ArgIterator" />, <see cref="System.Void" />, and <see cref="System.RuntimeArgumentHandle" /> types, or arrays of those types, is not supported.</exception>
    ''' <exception cref="System.Reflection.TargetInvocationException">The constructor being called throws an exception.</exception>
    ''' <exception cref="System.MethodAccessException">The caller does not have permission to call this constructor.</exception>
    ''' <exception cref="System.MemberAccessException">Cannot create an instance of an abstract class, or this member was invoked with a late-binding mechanism.</exception>
    ''' <exception cref="System.Runtime.InteropServices.InvalidComObjectException">The COM type was not obtained through Overload:<see cref="System.Type.GetTypeFromProgID" /> or Overload:<see cref="System.Type.GetTypeFromCLSID" />.</exception>
    ''' <exception cref="System.MissingMethodException">No matching public constructor was found.</exception>
    ''' <exception cref="System.Runtime.InteropServices.COMException"><paramref name="type"/> is a COM object but the class identifier used to obtain the type is invalid, or the identified class is not registered.</exception>
    ''' <exception cref="System.TypeLoadException"><paramref name="type"/> is not a valid type.</exception>
    ''' <seelaso cref="Activator.CreateInstance"/>
    <Extension()> Function CreateInstance(ByVal type As Type) As Object
        Return Activator.CreateInstance(type)
    End Function
    ''' <summary>Attempts to cast given object to specific type</summary>
    ''' <param name="obj">Object to cast</param>
    ''' <typeparam name="T">TYpe to cast <paramref name="obj"/> to</typeparam>
    ''' <returns>Value of <paramref name="obj"/> casted to type <typeparamref name="T"/>. Method uses several ways of casting.</returns>
    ''' <exception cref="InvalidCastException">No casting method from type of <paramref name="obj"/> to <paramref name="Type"/> was found -or- build in conversion from <see cref="String"/> to numeric type failed.</exception>
    ''' <exception cref="AmbiguousMatchException">Cast operators were found, but no one is most specific.</exception>
    ''' <exception cref="OverflowException">Build in conversion to numeric value (or <see cref="String"/> to <see cref="TimeSpan"/>) failed because <paramref name="obj"/> cannot be represented in <paramref name="Type"/> -or- Called cast operator have thrown this exception.</exception>
    ''' <exception cref="FormatException">Conversion of <see cref="String"/> to <see cref="TimeSpan"/> failed because string has bad format. -or- Operator being caled has thrown this exception.</exception>
    ''' <remarks>See <see cref="M:Tools.TypeTools.DynamicCast(System.Object,System.Type"/> non-generic method fro details on how casting is done.</remarks>
    ''' <seealso cref="M:Tools.TypeTools.DynamicCast(System.Object,System.Type)"/>
    ''' <version version="1.5.2">Function introduced</version>
    <Extension()> _
    Public Function DynamicCast(Of T)(ByVal obj As Object) As T
        Return DynamicCast(obj, GetType(T))
    End Function
    ''' <summary>Attempts to cast given tobject to given type</summary>
    ''' <param name="obj">Object to cast</param>
    ''' <param name="Type">Type to cast <paramref name="obj"/> to</param>
    ''' <returns>Value of <paramref name="obj"/> casted to type <paramref name="Type"/>. Method uses several ways of casting.</returns>
    ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null and <paramref name="obj"/> is not null</exception>
    ''' <exception cref="InvalidCastException">No casting method from type of <paramref name="obj"/> to <paramref name="Type"/> was found -or- build in conversion from <see cref="String"/> to numeric type failed.</exception>
    ''' <exception cref="AmbiguousMatchException">Cast operators were found, but no one is most specific.</exception>
    ''' <exception cref="OverflowException">Build in conversion to numeric value (or <see cref="String"/> to <see cref="TimeSpan"/>) failed because <paramref name="obj"/> cannot be represented in <paramref name="Type"/> -or- Called cast operator have thrown this exception.</exception>
    ''' <exception cref="FormatException">Conversion of <see cref="String"/> to <see cref="TimeSpan"/> failed because string has bad format. -or- Operator being caled has thrown this exception.</exception>
    ''' <remarks>Following ways of casting are attempted in given order
    ''' <list type="numbered">
    ''' <item>When <paramref name="obj"/> is null, null is returned (default value for value types)</item>
    ''' <item>When <paramref name="Type"/> <see cref="Type.IsAssignableFrom">is assignable from</see> <paramref name="obj"/>, <paramref name="obj"/> is returned.</item>
    ''' <item>Attempt to find cast operator using <see cref="FindBestFitCastOperator"/> is done. If operator is found, it is used.
    ''' <note>Operator being called can throw an eyception.</note></item>
    ''' <item>If <paramref name="obj"/> is enumeration, has value which is defined for its enumeration type and <paramref name="Type"/> <see cref="Type.IsAssignableFrom">is assignable from</see> <see cref="String"/>, result of <see cref="[Enum].ToString"/> is used.</item>
    ''' <item>If <paramref name="Type"/> is enumeration type and <paramref name="obj"/> is string and <paramref name="obj"/> has same value as is one of names of members of <paramref name="Type"/> enumeration, result of <see cref="[Enum].Parse"/> is returned.</item>
    ''' <item>If <paramref name="obj"/> is enumeration and <paramref name="Type"/> <see cref="Type.IsAssignableFrom">is assignable from</see> its underlying type, value of <paramref name="obj"/> in enumeration underlying type is returned.</item>
    ''' <item>If <paramref name="Type"/> is enumeration and its underlying type <see cref="Type.IsAssignableFrom">is assignable from</see> type of <paramref name="obj"/>, value <paramref name="obj"/> is returned as member of <paramref name="Type"/> enumeration.</item>
    ''' <item>If either <paramref name="obj"/> or <paramref name="Type"/> is enumeration, <see cref="DynamicCast"/> is called with <paramref name="obj"/> in its undelying type (if it is enumeration; otherwise <paramref name="obj"/> is used) and enum-underlying type of <paramref name="Type"/> (if it is enumeration; otherwise <paramref name="Type"/> is used). If <paramref name="Type"/> is enumeration result of call is converted from underlying type to <paramref name="Type"/> (otherwise it is left as retuned), and returned.</item>
    ''' <item>Build-in conversion is attempted. Build-in conversion is defined for between all combinations of following types: <see cref="Byte"/>, <see cref="SByte"/>, <see cref="UShort"/>, <see cref="Short"/>, <see cref="UInteger"/>, <see cref="Integer"/>, <see cref="ULong"/>, <see cref="Long"/>, <see cref="Single"/>, <see cref="Double"/>, <see cref="Char"/>, <see cref="String"/>, <see cref="Boolean"/>.
    ''' <para>For numeric types, simple interpretation of numeric value in different type, is attempted, with possible loss of precision and <see cref="OverflowException"/>. Visual Basic C* operators are used. They round x.5 to neares even integer.</para>
    ''' <para>For conversion from numeric type to <see cref="Char"/> <see cref="ChrW"/> is used, for conversion from <see cref="Char"/> to numeric type <see cref="AscW"/> is used. <see cref="OverflowException"/> may occur.</para>
    ''' <para>For conversion from numeric type to <see cref="String"/> and from <see cref="String"/> to numeric type, such conversion is culture-sensitive. When string cannot be interpreted as number, <see cref="InvalidCastException"/> is thrown.</para>
    ''' <para>When <see cref="String"/> is converted to <see cref="Char"/>, only firts character is converted, empty string is converted to <see cref="vbNullChar"/>. <see cref="Char"/> to <see cref="String"/> is converted as single-character string.</para>
    ''' <para><see cref="Boolean"/> to string is converted as either "True" or "False" (culture-independent). </para>
    ''' <para>Any non-zero number to <see cref="boolean"/> is converted as true, zero as false. <see cref="Boolean"/> to numeric values are converted to -1 for signed (including <see cref="Double"/> and <see cref="Single"/>) and as max value to unsigned.</para>
    ''' <para><see cref="Char"/> to <see cref="Boolean"/> is converted in same was as numbers (numeric code of character is used), <see cref="Boolean"/> to <see cref="Char"/> as well.</para></item>
    ''' <item>Special build-in conversions are attempted. Those conversions are difined between <see cref="String"/> and any of following types: <see cref="Decimal"/>, <see cref="Date"/>, <see cref="TimeSpan"/>.
    ''' <para>Values are converted from this type to <see cref="String"/> if <paramref name="Type"/> equals to <see cref="String"/> using default format in culture-sensitive way.</para>
    ''' <para>If <paramref name="obj"/> is <see cref="String"/> it is converted to one of these types when <paramref name="Type"/> <see cref="Type.IsAssignableFrom">is assignable from it</see> using culture-sensitive parsing in following order without error recovery: <see cref="Date"/>, <see cref="TimeSpan"/>, <see cref="Decimal"/>.</para></item>
    ''' <item>Special conversion between <see cref="Boolean"/> and <see cref="Decimal"/> is attempted using same rules for <see cref="Boolean"/> ↔ numeric conversions above.</item>
    ''' </list>
    ''' <para>Note that following conversion are not defined: <see cref="Date"/>↔<see cref="Boolean"/>, <see cref="Date"/>↔<see cref="TimeSpan"/>, <see cref="Date"/>↔<see cref="Char"/>.
    ''' <see cref="TimeSpanFormattable"/> is treated as any other types using its operators.
    ''' There is no specific support for <see cref="Nullable(Of T)"/></para></remarks>
    ''' <seealso cref="M:Tools.TypeTools.DynamicCast`1(System.Object)"/>
    ''' <version version="1.5.2">Function introduced</version>
    <Extension()> _
    Public Function DynamicCast(ByVal obj As Object, ByVal Type As Type) As Object
        If obj Is Nothing Then
            If Type Is Nothing Then Return Nothing
            If Type.IsValueType Then Return Activator.CreateInstance(Type)
        End If
        If Type Is Nothing Then Throw New ArgumentNullException("Type")
        Dim ObjType = obj.GetType
        'No cast needed
        If Type.IsAssignableFrom(ObjType) Then Return obj
        'Operators
        Dim [Operator] = FindBestFitCastOperator(ObjType, Type)
        If [Operator] IsNot Nothing Then Return [Operator].Invoke(Nothing, New Object() {obj})
        'Enum
        If ObjType.IsEnum OrElse Type.IsEnum Then
            If TypeOf obj Is [Enum] AndAlso Type.IsAssignableFrom(GetType(String)) AndAlso DirectCast(obj, [Enum]).IsDefined() Then Return obj.ToString
            If TypeOf obj Is String AndAlso Type.IsEnum AndAlso [Enum].GetNames(Type).Contains(obj) Then Return [Enum].Parse(Type, obj)
            If TypeOf obj Is [Enum] AndAlso Type.IsAssignableFrom([Enum].GetUnderlyingType(ObjType)) Then Return DirectCast(obj, [Enum]).GetValue
            If Type.IsEnum AndAlso [Enum].GetUnderlyingType(Type).IsAssignableFrom(ObjType) Then Return GetEnumValue(Type, obj)
            Dim EnumCastFrom = obj
            If ObjType.IsEnum Then EnumCastFrom = DirectCast(obj, [Enum]).GetValue
            Dim EnumCastTo = Type
            If Type.IsEnum Then EnumCastTo = [Enum].GetUnderlyingType(Type)
            Dim Ret = DynamicCast(EnumCastFrom, EnumCastTo)
            If Type.IsEnum Then Return GetEnumValue(Type, Ret) Else Return Ret
        End If
        'Built-in types
        If TypeOf obj Is Byte Then
            With DirectCast(obj, Byte)
                If Type.IsAssignableFrom(GetType(Byte)) Then : Return CByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(SByte)) Then : Return CSByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(Short)) Then : Return CShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(UShort)) Then : Return CUShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(Integer)) Then : Return CInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(UInteger)) Then : Return CUInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(Long)) Then : Return CLng(.self)
                ElseIf Type.IsAssignableFrom(GetType(ULong)) Then : Return CULng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Single)) Then : Return CSng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Double)) Then : Return CDbl(.self)
                ElseIf Type.IsAssignableFrom(GetType(Char)) Then : Return ChrW(.self)
                ElseIf Type.IsAssignableFrom(GetType(Boolean)) Then : Return CBool(.self)
                ElseIf Type.IsAssignableFrom(GetType(String)) Then : Return CStr(.self)
                End If
            End With
        ElseIf TypeOf obj Is SByte Then
            With DirectCast(obj, SByte)
                If Type.IsAssignableFrom(GetType(Byte)) Then : Return CByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(SByte)) Then : Return CSByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(Short)) Then : Return CShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(UShort)) Then : Return CUShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(Integer)) Then : Return CInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(UInteger)) Then : Return CUInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(Long)) Then : Return CLng(.self)
                ElseIf Type.IsAssignableFrom(GetType(ULong)) Then : Return CULng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Single)) Then : Return CSng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Double)) Then : Return CDbl(.self)
                ElseIf Type.IsAssignableFrom(GetType(Char)) Then : Return ChrW(.self)
                ElseIf Type.IsAssignableFrom(GetType(Boolean)) Then : Return CBool(.self)
                ElseIf Type.IsAssignableFrom(GetType(String)) Then : Return CStr(.self)
                End If
            End With
        ElseIf TypeOf obj Is Short Then
            With DirectCast(obj, Short)
                If Type.IsAssignableFrom(GetType(Byte)) Then : Return CByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(SByte)) Then : Return CSByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(Short)) Then : Return CShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(UShort)) Then : Return CUShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(Integer)) Then : Return CInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(UInteger)) Then : Return CUInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(Long)) Then : Return CLng(.self)
                ElseIf Type.IsAssignableFrom(GetType(ULong)) Then : Return CULng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Single)) Then : Return CSng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Double)) Then : Return CDbl(.self)
                ElseIf Type.IsAssignableFrom(GetType(Char)) Then : Return ChrW(.self)
                ElseIf Type.IsAssignableFrom(GetType(Boolean)) Then : Return CBool(.self)
                ElseIf Type.IsAssignableFrom(GetType(String)) Then : Return CStr(.self)
                End If
            End With
        ElseIf TypeOf obj Is UShort Then
            With DirectCast(obj, UShort)
                If Type.IsAssignableFrom(GetType(Byte)) Then : Return CByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(SByte)) Then : Return CSByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(Short)) Then : Return CShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(UShort)) Then : Return CUShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(Integer)) Then : Return CInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(UInteger)) Then : Return CUInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(Long)) Then : Return CLng(.self)
                ElseIf Type.IsAssignableFrom(GetType(ULong)) Then : Return CULng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Single)) Then : Return CSng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Double)) Then : Return CDbl(.self)
                ElseIf Type.IsAssignableFrom(GetType(Char)) Then : Return ChrW(.self)
                ElseIf Type.IsAssignableFrom(GetType(Boolean)) Then : Return CBool(.self)
                ElseIf Type.IsAssignableFrom(GetType(String)) Then : Return CStr(.self)
                End If
            End With
        ElseIf TypeOf obj Is Integer Then
            With DirectCast(obj, Integer)
                If Type.IsAssignableFrom(GetType(Byte)) Then : Return CByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(SByte)) Then : Return CSByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(Short)) Then : Return CShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(UShort)) Then : Return CUShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(Integer)) Then : Return CInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(UInteger)) Then : Return CUInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(Long)) Then : Return CLng(.self)
                ElseIf Type.IsAssignableFrom(GetType(ULong)) Then : Return CULng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Single)) Then : Return CSng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Double)) Then : Return CDbl(.self)
                ElseIf Type.IsAssignableFrom(GetType(Char)) Then : Return ChrW(.self)
                ElseIf Type.IsAssignableFrom(GetType(Boolean)) Then : Return CBool(.self)
                ElseIf Type.IsAssignableFrom(GetType(String)) Then : Return CStr(.self)
                End If
            End With
        ElseIf TypeOf obj Is UInteger Then
            With DirectCast(DirectCast(obj, UInteger), IConvertible)
                If Type.IsAssignableFrom(GetType(Byte)) Then : Return CByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(SByte)) Then : Return CSByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(Short)) Then : Return CShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(UShort)) Then : Return CUShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(Integer)) Then : Return CInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(UInteger)) Then : Return CUInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(Long)) Then : Return CLng(.self)
                ElseIf Type.IsAssignableFrom(GetType(ULong)) Then : Return CULng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Single)) Then : Return CSng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Double)) Then : Return CDbl(.self)
                ElseIf Type.IsAssignableFrom(GetType(Char)) Then : Return ChrW(.self)
                ElseIf Type.IsAssignableFrom(GetType(Boolean)) Then : Return CBool(.self)
                ElseIf Type.IsAssignableFrom(GetType(String)) Then : Return CStr(.self)
                End If
            End With
        ElseIf TypeOf obj Is Long Then
            With DirectCast(DirectCast(obj, Long), IConvertible)
                If Type.IsAssignableFrom(GetType(Byte)) Then : Return CByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(SByte)) Then : Return CSByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(Short)) Then : Return CShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(UShort)) Then : Return CUShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(Integer)) Then : Return CInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(UInteger)) Then : Return CUInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(Long)) Then : Return CLng(.self)
                ElseIf Type.IsAssignableFrom(GetType(ULong)) Then : Return CULng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Single)) Then : Return CSng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Double)) Then : Return CDbl(.self)
                ElseIf Type.IsAssignableFrom(GetType(Char)) Then : Return ChrW(.self)
                ElseIf Type.IsAssignableFrom(GetType(Boolean)) Then : Return CBool(.self)
                ElseIf Type.IsAssignableFrom(GetType(String)) Then : Return CStr(.self)
                End If
            End With
        ElseIf TypeOf obj Is ULong Then
            With DirectCast(DirectCast(obj, ULong), IConvertible)
                If Type.IsAssignableFrom(GetType(Byte)) Then : Return CByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(SByte)) Then : Return CSByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(Short)) Then : Return CShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(UShort)) Then : Return CUShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(Integer)) Then : Return CInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(UInteger)) Then : Return CUInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(Long)) Then : Return CLng(.self)
                ElseIf Type.IsAssignableFrom(GetType(ULong)) Then : Return CULng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Single)) Then : Return CSng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Double)) Then : Return CDbl(.self)
                ElseIf Type.IsAssignableFrom(GetType(Char)) Then : Return ChrW(.self)
                ElseIf Type.IsAssignableFrom(GetType(Boolean)) Then : Return CBool(.self)
                ElseIf Type.IsAssignableFrom(GetType(String)) Then : Return CStr(.self)
                End If
            End With
        ElseIf TypeOf obj Is Double Then
            With DirectCast(obj, Double)
                If Type.IsAssignableFrom(GetType(Byte)) Then : Return CByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(SByte)) Then : Return CSByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(Short)) Then : Return CShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(UShort)) Then : Return CUShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(Integer)) Then : Return CInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(UInteger)) Then : Return CUInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(Long)) Then : Return CLng(.self)
                ElseIf Type.IsAssignableFrom(GetType(ULong)) Then : Return CULng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Single)) Then : Return CSng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Double)) Then : Return CDbl(.self)
                ElseIf Type.IsAssignableFrom(GetType(Char)) Then : Return ChrW(.self)
                ElseIf Type.IsAssignableFrom(GetType(Boolean)) Then : Return CBool(.self)
                ElseIf Type.IsAssignableFrom(GetType(String)) Then : Return CStr(.self)
                End If
            End With
        ElseIf TypeOf obj Is Char Then
            With DirectCast(obj, Char)
                If Type.IsAssignableFrom(GetType(Byte)) Then : Return CByte(AscW(.self))
                ElseIf Type.IsAssignableFrom(GetType(SByte)) Then : Return CSByte(AscW(.self))
                ElseIf Type.IsAssignableFrom(GetType(Short)) Then : Return CShort(AscW(.self))
                ElseIf Type.IsAssignableFrom(GetType(UShort)) Then : Return CUShort(AscW(.self))
                ElseIf Type.IsAssignableFrom(GetType(Integer)) Then : Return CInt(AscW(.self))
                ElseIf Type.IsAssignableFrom(GetType(UInteger)) Then : Return CUInt(AscW(.self))
                ElseIf Type.IsAssignableFrom(GetType(Long)) Then : Return CLng(AscW(.self))
                ElseIf Type.IsAssignableFrom(GetType(ULong)) Then : Return CULng(AscW(.self))
                ElseIf Type.IsAssignableFrom(GetType(Single)) Then : Return CSng(AscW(.self))
                ElseIf Type.IsAssignableFrom(GetType(Double)) Then : Return CDbl(AscW(.self))
                ElseIf Type.IsAssignableFrom(GetType(Char)) Then : Return .self
                ElseIf Type.IsAssignableFrom(GetType(Boolean)) Then : Return CBool(AscW(.self))
                ElseIf Type.IsAssignableFrom(GetType(String)) Then : Return CStr(.self)
                End If
            End With
        ElseIf TypeOf obj Is Boolean Then
            With DirectCast(obj, Boolean)
                If Type.IsAssignableFrom(GetType(Byte)) Then : Return CByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(SByte)) Then : Return CSByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(Short)) Then : Return CShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(UShort)) Then : Return CUShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(Integer)) Then : Return CInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(UInteger)) Then : Return CUInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(Long)) Then : Return CLng(.self)
                ElseIf Type.IsAssignableFrom(GetType(ULong)) Then : Return CULng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Single)) Then : Return CSng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Double)) Then : Return CDbl(.self)
                ElseIf Type.IsAssignableFrom(GetType(Char)) Then : Return ChrW(If(.self, 1, 0))
                ElseIf Type.IsAssignableFrom(GetType(Boolean)) Then : Return CBool(.self)
                ElseIf Type.IsAssignableFrom(GetType(String)) Then : Return CStr(.self)
                End If
            End With
        ElseIf TypeOf obj Is String Then
            With DirectCast(obj, String)
                If Type.IsAssignableFrom(GetType(Byte)) Then : Return CByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(SByte)) Then : Return CSByte(.self)
                ElseIf Type.IsAssignableFrom(GetType(Short)) Then : Return CShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(UShort)) Then : Return CUShort(.self)
                ElseIf Type.IsAssignableFrom(GetType(Integer)) Then : Return CInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(UInteger)) Then : Return CUInt(.self)
                ElseIf Type.IsAssignableFrom(GetType(Long)) Then : Return CLng(.self)
                ElseIf Type.IsAssignableFrom(GetType(ULong)) Then : Return CULng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Single)) Then : Return CSng(.self)
                ElseIf Type.IsAssignableFrom(GetType(Double)) Then : Return CDbl(.self)
                ElseIf Type.IsAssignableFrom(GetType(Char)) Then : Return CChar(.self)
                ElseIf Type.IsAssignableFrom(GetType(Boolean)) Then : Return CBool(.self)
                ElseIf Type.IsAssignableFrom(GetType(String)) Then : Return .self
                End If
            End With
        End If
        'Special conversions
        If TypeOf obj Is Date AndAlso Type.IsAssignableFrom(GetType(String)) Then Return CStr(DirectCast(obj, Date))
        If TypeOf obj Is TimeSpan AndAlso Type.IsAssignableFrom(GetType(String)) Then Return DirectCast(obj, TimeSpan).ToString
        If TypeOf obj Is String AndAlso Type.Equals(GetType(Date)) Then Return CDate(DirectCast(obj, String))
        If TypeOf obj Is String AndAlso Type.Equals(GetType(TimeSpan)) Then Return TimeSpan.Parse(DirectCast(obj, String))
        If TypeOf obj Is Decimal AndAlso Type.IsAssignableFrom(GetType(String)) Then Return CStr(DirectCast(obj, Decimal))
        If TypeOf obj Is String AndAlso Type.Equals(GetType(Decimal)) Then Return CDec(DirectCast(obj, String))
        If TypeOf obj Is Boolean AndAlso Type.Equals(GetType(Decimal)) Then Return CDec(DirectCast(obj, Boolean))
        If TypeOf obj Is Decimal AndAlso Type.Equals(GetType(Boolean)) Then Return CBool(DirectCast(obj, Decimal))
        Throw New InvalidCastException(ResourcesT.Exceptions.UnableToCastType0ToType1.f(ObjType.FullName, Type.FullName))
    End Function
    
End Module


#End If