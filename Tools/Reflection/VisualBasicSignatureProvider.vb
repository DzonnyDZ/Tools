Imports System.Reflection, System.Runtime.InteropServices
Imports System.Runtime.CompilerServices, System.Linq, Tools.LinqT

#If Config <= Nightly Then 'Stage:Nightly
'ASAP
Namespace ReflectionT
    ''' <summary>Provides string representation of various reflection object using Visual Basic syntax</summary>
    <DebuggerDisplay("{Name} signature provider")> _
    Public NotInheritable Class VisualBasicSignatureProvider : Implements ISignatureProvider
        ''' <summary>Gets name of current provider</summary>
        ''' <returns>"Visual Basic 9"</returns>
        ReadOnly Property Name$() Implements ISignatureProvider.Name
            Get
                Return "Visual Basic 9"
            End Get
        End Property
        ''' <summary>Gets string representation of an assembly name</summary>
        ''' <param name="Assembly">Assembly to represent</param>
        ''' <param name="Flags">Controls how the signature will be rendered</param>
        ''' <returns>String representation of <paramref name="Assembly"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Assembly"/> is null</exception>
        Public Function GetSignature(ByVal Assembly As System.Reflection.AssemblyName, Optional ByVal Flags As SignatureFlags = SignatureFlags.ShortNameOnly) As String Implements ISignatureProvider.GetSignature
            If Assembly Is Nothing Then Throw New ArgumentNullException("Assembly")
            Dim ret As New System.Text.StringBuilder
            If Flags And SignatureFlags.ObjectType Then ret.Append(If(Flags And SignatureFlags.Strict, "'", "") & "Assembly ")
            If Flags And SignatureFlags.LongName Then
                ret.Append(Assembly.FullName)
            Else
                ret.Append(Assembly.Name)
            End If
            Return ret.ToString
        End Function
        ''' <summary>Gets string representation of an assembly</summary>
        ''' <param name="Assembly">Assembly to represent</param>
        ''' <param name="Flags">Controls how the signature will be rendered</param>
        ''' <returns>String representation of <paramref name="Assembly"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Assembly"/> is null</exception>
        Public Function GetSignature(ByVal Assembly As Assembly, Optional ByVal Flags As SignatureFlags = SignatureFlags.ShortNameOnly) As String Implements ISignatureProvider.GetSignature
            If Assembly Is Nothing Then Throw New ArgumentNullException("Assembly")
            Dim ret As New System.Text.StringBuilder
            If Flags And SignatureFlags.ObjectType Then ret.Append(If(Flags And SignatureFlags.Strict, "'", "") & "Assembly ")
            If Flags And SignatureFlags.LongName Then
                ret.Append(Assembly.FullName)
            Else
                ret.Append(Assembly.GetName.Name)
            End If
            If Flags And SignatureFlags.AllAttributes Then
                If Not CBool(Flags And SignatureFlags.NoMultiline) Then ret.Append(vbCrLf)
                AppendCustomAttributes(CustomAttributeData.GetCustomAttributes(Assembly), ret, Flags, True, True, Nothing)
            End If
            Return ret.ToString
        End Function

        ''' <summary>Append information about custom attributes to <see cref="Text.StringBuilder"/></summary>
        ''' <param name="Attributes">Attributes to append information about</param>
        ''' <param name="ret">target <see cref="System.Text.StringBuilder"/></param>
        ''' <param name="flags">Flags to control rendering</param>
        ''' <param name="Way">True to use assembly-style attributes</param>
        ''' <param name="Multiline">True to span attributtes on multiple lines. Note: <see cref="SignatureFlags.NoMultiline"/> is set, multiple lines will not be produced.</param>
        ''' <param name="filter">Attribute callback filter. Used only when <see cref="SignatureFlags.SomeAttributes"/> is set and <see cref="SignatureFlags.AllAttributes"/> is not set. Only attributes for which it is true will be returned.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="ret"/> is null ==or== <paramref name="filter"/> is null and <see cref="SignatureFlags.AllAttributes"/> is not set and <see cref="SignatureFlags.SomeAttributes"/> is set. ==or== <paramref name="Attributes"/> is null</exception>
        Private Sub AppendCustomAttributes(ByVal Attributes As IList(Of CustomAttributeData), ByVal ret As System.Text.StringBuilder, ByVal flags As SignatureFlags, ByVal Way As CustomAttributeFlags, ByVal Multiline As Boolean, ByVal filter As Func(Of Type, Boolean))
            If ret Is Nothing Then Throw New ArgumentNullException("ret")
            If filter Is Nothing AndAlso (flags And SignatureFlags.SomeAttributes) AndAlso Not CBool(flags And SignatureFlags.AllAttributes) Then Throw New ArgumentNullException("filter")
            If Attributes Is Nothing Then Throw New ArgumentNullException("Attributes")
            If Not CBool(flags And SignatureFlags.SomeAttributes) AndAlso Not CBool(flags And SignatureFlags.AllAttributes) Then Return
            Multiline = Multiline AndAlso Not CBool(flags And SignatureFlags.NoMultiline)
            Dim Outattr As New List(Of System.Text.StringBuilder)
            For Each attr In Attributes
                If (flags And SignatureFlags.AllAttributes) OrElse filter.Invoke(attr.Constructor.DeclaringType) Then
                    Dim this As New System.Text.StringBuilder
                    RepresentTypeName(attr.Constructor.DeclaringType, this, flags)
                    If attr.ConstructorArguments.Count > 0 OrElse attr.NamedArguments.Count > 0 OrElse Not CBool(flags And SignatureFlags.NoEmptyBraces) Then this.Append("(")
                    Dim i As Integer = 0
                    For Each cpar In attr.ConstructorArguments
                        If i > 0 Then this.Append(", ")
                        RepresentValue(cpar.Value, flags, this)
                        i += 1
                    Next cpar
                    For Each npar In attr.NamedArguments
                        If i > 0 Then this.Append(", ")
                        this.Append(npar.MemberInfo.Name & " := ")
                        RepresentValue(npar.TypedValue.Value, flags, this)
                        i += 1
                    Next npar
                    If attr.ConstructorArguments.Count > 0 OrElse attr.NamedArguments.Count > 0 OrElse Not CBool(flags And SignatureFlags.NoEmptyBraces) Then this.Append(")")
                    Outattr.Add(this)
                End If
            Next attr
            Dim ic = System.Globalization.CultureInfo.InvariantCulture
            If Outattr.Count > 0 Then
                Dim Prefix = If(Way = CustomAttributeFlags.Module, "Module", "Assembly")
                If (flags And SignatureFlags.NoMultiline) AndAlso Way Then
                    ret.AppendFormat(ic, "{1}<{0}: ", Prefix, If((flags And SignatureFlags.Strict) AndAlso Way = CustomAttributeFlags.Module, "'", ""))
                ElseIf flags And SignatureFlags.NoMultiline Then
                    ret.Append("<")
                End If
                Dim i As Integer = 0
                For Each attr In Outattr
                    If Not CBool(flags And SignatureFlags.NoMultiline) AndAlso Way Then
                        ret.AppendFormat(ic, "{1}<{0}: ", Prefix, If((flags And SignatureFlags.Strict) AndAlso Way = CustomAttributeFlags.Module, "'", ""))
                    ElseIf Not CBool(flags And SignatureFlags.NoMultiline) Then
                        ret.Append("<")
                    ElseIf i > 0 Then : ret.Append(", ")
                    End If
                    ret.Append(attr.ToString)
                    If Not CBool(flags And SignatureFlags.NoMultiline) AndAlso Way Then
                        ret.Append(">" & vbCrLf)
                    ElseIf Not CBool(flags And SignatureFlags.NoMultiline) Then
                        ret.Append("> _" & vbCrLf)
                    End If
                Next attr
                If flags And SignatureFlags.NoMultiline Then ret.Append("> ")
            End If
        End Sub

        ''' <summary>Serializes value of attribute-allowed type to <see cref="Text.StringBuilder"/></summary>
        ''' <param name="val">Value to serialize</param>
        ''' <param name="Flags">Serialization flags</param>
        ''' <param name="ret"><see cref="Text.StringBuilder"/> to serialize value to</param>
        ''' <exception cref="ArgumentNullException"><paramref name="ret"/> is null</exception>
        Private Sub RepresentValue(ByVal val As Object, ByVal Flags As SignatureFlags, ByVal ret As Text.StringBuilder)
            If ret Is Nothing Then Throw New ArgumentNullException("ret")
            Dim ic = System.Globalization.CultureInfo.InvariantCulture
            If val Is Nothing Then
                ret.Append("Nothing")
            ElseIf TypeOf val Is String Then
                Dim instr As Boolean = False
                Dim begin = True
                For Each ch In DirectCast(val, String)
                    Select Case AscW(ch)
                        Case Is < AscW(" "c)
                            ret.Append(If(instr, """ & ", "") & String.Format(ic, "ChrW({0})", AscW(ch)))
                            instr = False
                        Case AscW(""""c)
                            If Not instr Then ret.Append(If(begin, """", " & """))
                            ret.Append("""""")
                            instr = True
                        Case Else
                            If Not instr Then ret.Append(If(begin, """", " & """))
                            ret.Append(ch)
                            instr = True
                    End Select
                    begin = False
                Next ch
                If instr Then ret.Append("""")
            ElseIf TypeOf val Is [Enum] Then
                Dim enumname = [Enum].GetName(val.GetType, val)
                If enumname IsNot Nothing Then
                    RepresentTypeName(val.GetType, ret, Flags)
                    ret.Append("." & enumname)
                Else
                    ret.Append("CType(")
                    RepresentTypeName(val.GetType, ret, Flags)
                    ret.Append(String.Format(ic, "{0:d})", val))
                End If
            ElseIf TypeOf val Is Char Then
                Select Case AscW(DirectCast(val, Char))
                    Case Is < AscW(" ") : ret.AppendFormat(ic, "ChrW({0})", AscW(DirectCast(val, Char)))
                    Case AscW(""""c) : ret.Append("""""""""c")
                    Case Else : ret.Append(String.Format(ic, """{0}""c", val))
                End Select
            ElseIf TypeOf val Is Byte Then
                ret.Append(String.Format(ic, "CByte({0})", val))
            ElseIf TypeOf val Is SByte Then
                ret.AppendFormat(ic, "CSByte({0})", val)
            ElseIf TypeOf val Is Integer Then
                ret.AppendFormat(ic, "{0}I", val)
            ElseIf TypeOf val Is UInteger Then
                ret.AppendFormat(ic, "{0}UI", val)
            ElseIf TypeOf val Is Short Then
                ret.AppendFormat(ic, "{0}S", val)
            ElseIf TypeOf val Is UShort Then
                ret.AppendFormat(ic, "{0}US", val)
            ElseIf TypeOf val Is Long Then
                ret.AppendFormat(ic, "{0}L", val)
            ElseIf TypeOf val Is ULong Then
                ret.AppendFormat(ic, "{0}UL", val)
            ElseIf TypeOf val Is Single Then
                ret.AppendFormat(ic, "{0r}R", val)
            ElseIf TypeOf val Is Double Then
                ret.AppendFormat(ic, "{0r}F", val)
            ElseIf TypeOf val Is Boolean Then
                ret.Append(If(DirectCast(val, Boolean), "True", "False"))
            ElseIf TypeOf val Is Type Then
                ret.Append("GetType(")
                RepresentTypeName(val, ret, Flags)
                ret.Append(")")
            ElseIf TypeOf val Is Decimal Then
                ret.AppendFormat(ic, "CDec({0r})", val)
            Else
                RepresentValue("{" & val.ToString & "}", Flags, ret)
            End If
        End Sub
        ''' <summary>Operator suported for displaying signature by this provider</summary>
        Private Shared SupportedOperators As Operators() = {Operators.Addition, Operators.AditionAssignment, Operators.AddressOf, Operators.BitwiseAnd, Operators.BitwiseAndAssignment, Operators.BitwiseOr, Operators.BitwiseOrAssignment, Operators.Comma, Operators.Concatenate, Operators.Decrement, Operators.Division, Operators.DivisionAssignment, Operators.Equality, Operators.ExclusiveOr, Operators.ExclusiveOrAssignment, Operators.Explicit, Operators.Exponent, Operators.False, Operators.GreaterThan, Operators.GreaterThanOrEqual, Operators.Implicit, Operators.Increment, Operators.Inequality, Operators.IntegerDivision, Operators.LeftShift, Operators.LeftShiftAssignment, Operators.LessThan, Operators.LessThanOrEqual, Operators.LogicalAnd, Operators.LogicalOr, Operators.Modulus, Operators.ModulusAssignment, Operators.MultiplicationAssignment, Operators.Multiply, Operators.OnesComplement, Operators.RightShifAssignment, Operators.RightShift, Operators.Subtraction, Operators.SubtractionAssignment, Operators.True, Operators.UnaryNegation, Operators.UnaryPlus}
        ''' <summary>Operator suported for displaying signature by this provider in strict mode</summary>
        Private Shared StrictOperators As Operators() = {Operators.Addition, Operators.BitwiseAnd, Operators.BitwiseOr, Operators.Concatenate, Operators.Division, Operators.Equality, Operators.ExclusiveOr, Operators.Explicit, Operators.Exponent, Operators.False, Operators.GreaterThan, Operators.GreaterThanOrEqual, Operators.Implicit, Operators.Inequality, Operators.IntegerDivision, Operators.LeftShift, Operators.LessThan, Operators.LessThanOrEqual, Operators.Modulus, Operators.Multiply, Operators.OnesComplement, Operators.RightShift, Operators.Subtraction, Operators.True, Operators.UnaryNegation, Operators.UnaryPlus}

        ''' <summary>Gets string representation of a member</summary>
        ''' <param name="Member">Member to represent</param>
        ''' <param name="Flags">Controls how the signature will be rendered</param>
        ''' <returns>String representation of <paramref name="Member"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
        Public Function GetSignature(ByVal Member As System.Reflection.MemberInfo, ByVal Flags As SignatureFlags) As String Implements ISignatureProvider.GetSignature
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            Dim ret As New System.Text.StringBuilder
            Dim EmptyBraces As String = If(Flags And SignatureFlags.NoEmptyBraces, "", "()")
            If (Flags And SignatureFlags.SomeAttributes) OrElse (Flags And SignatureFlags.AllAttributes) Then
                AppendCustomAttributes(CustomAttributeData.GetCustomAttributes(Member), ret, Flags, False, True, _
                    Function(at As Type) _
                        (Member.MemberType = MemberTypes.Method AndAlso at.Equals(GetType(ExtensionAttribute))) OrElse _
                        ((Member.MemberType = MemberTypes.TypeInfo OrElse Member.MemberType = MemberTypes.NestedType) AndAlso DirectCast(Member, Type).IsEnum AndAlso at.Equals(GetType(FlagsAttribute))) OrElse _
                        ((Member.MemberType = MemberTypes.TypeInfo OrElse Member.MemberType = MemberTypes.NestedType) AndAlso (at.Equals(GetType(SerializableAttribute)) OrElse at.Equals(GetType(StructLayoutAttribute)))) OrElse _
                        (Member.MemberType = MemberTypes.Method AndAlso at.Equals(GetType(DllImportAttribute))) _
                    )
            End If
            If Flags And SignatureFlags.AccessModifiers Then
                'Access modifiers
                ret.Append(GetAccessModifiers(Member) & " ")
            End If
            If Flags And SignatureFlags.OtherModifiers Then
                'Static modifiers
                Select Case Member.MemberType
                    Case MemberTypes.Constructor, MemberTypes.Event, MemberTypes.Method, MemberTypes.Property
                        If Member.IsStatic Then ret.Append("Shared ")
                    Case MemberTypes.Field
                        With DirectCast(Member, FieldInfo)
                            If Not .IsLiteral AndAlso .IsStatic Then ret.Append("Shared ")
                        End With
                End Select
                If Member.MemberType = MemberTypes.Property AndAlso Member.IsDefined(GetType(DefaultMemberAttribute), False) Then
                    ret.Append("Default ")
                End If
                'Read-Write modifiers
                If Member.MemberType = MemberTypes.Field AndAlso DirectCast(Member, FieldInfo).IsInitOnly Then ret.Append("ReadOnly ")
                If Member.MemberType = MemberTypes.Property Then
                    With DirectCast(Member, PropertyInfo)
                        If Not .CanRead Then
                            ret.Append("WriteOnly ")
                        ElseIf Not .CanWrite Then
                            ret.Append("ReadOnly ")
                        End If
                    End With
                End If
                'Inheritance modifiers
                If Member.MemberType = MemberTypes.Method Then
                    With DirectCast(Member, MethodInfo)
                        If .IsVirtual AndAlso Not .IsAbstract AndAlso Not .IsFinal Then ret.Append("Overridable ")
                        If .IsAbstract Then ret.Append("MutOverride ")
                        If .IsVirtual AndAlso .IsFinal Then ret.Append("NotOveridable ")
                    End With
                ElseIf Member.MemberType = MemberTypes.TypeInfo OrElse Member.MemberType = MemberTypes.NestedType Then
                    With DirectCast(Member, Type)
                        If Not .IsInterface AndAlso .IsClass AndAlso Not GetType([Delegate]).IsAssignableFrom(Member) AndAlso Not (.IsClass AndAlso Not .IsGenericType AndAlso Not .IsGenericTypeDefinition AndAlso .IsDefined(GetType(Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute), False) AndAlso .GetConstructors(Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public).Length = 0 AndAlso .BaseType.Equals(GetType(Object))) Then
                            If .IsAbstract Then ret.Append("MustInherit ")
                            If .IsSealed Then ret.Append("NotInheritable ")
                        End If
                    End With
                End If
            End If
            'Name
            Dim MemberName$
            Dim MyOperator As Operators = Operators.no
            If Member.MemberType = MemberTypes.Method Then MyOperator = DirectCast(Member, MethodInfo).IsOperator
            Dim IsOperator = Member.MemberType = MemberTypes.Method AndAlso If(Flags And SignatureFlags.Strict, StrictOperators, SupportedOperators).Contains(MyOperator)
            If Flags And SignatureFlags.LongName Then
                Select Case Member.MemberType
                    Case MemberTypes.TypeInfo, MemberTypes.NestedType
                        Dim tnb As New System.Text.StringBuilder
                        RepresentTypeName(Member.DeclaringType, tnb, Flags)
                        MemberName = tnb.ToString
                    Case MemberTypes.Method
                        Dim tnb As New System.Text.StringBuilder
                        If Member.DeclaringType IsNot Nothing Then RepresentTypeName(Member.DeclaringType, tnb, Flags)
                        With DirectCast(Member, MethodInfo)
                            If IsOperator Then
                                MemberName = tnb.ToString & OperatorName(MyOperator)
                            ElseIf .IsGenericMethodDefinition Then
                                Dim muggle = String.Format(System.Globalization.CultureInfo.InvariantCulture, "`{0}", .GetGenericArguments.Length)
                                If .Name.EndsWith(muggle) Then
                                    MemberName = tnb.ToString & "." & Member.Name.Substring(0, Member.Name.Length - muggle.Length)
                                Else
                                    MemberName = tnb.ToString & "." & Member.Name
                                End If
                            Else
                                MemberName = tnb.ToString & "." & Member.Name
                            End If
                        End With
                    Case Else
                        Dim tnb As New System.Text.StringBuilder
                        If Member.DeclaringType IsNot Nothing Then RepresentTypeName(Member.DeclaringType, tnb, Flags)
                        MemberName = tnb.ToString & "." & Member.Name
                End Select
            Else
                If IsOperator Then
                    MemberName = OperatorName(MyOperator)
                ElseIf Member.MemberType = MemberTypes.Constructor Then
                    MemberName = "New"
                Else
                    MemberName = Member.Name
                End If
            End If
            If Flags And SignatureFlags.ObjectType Then
                'Type
                Select Case Member.MemberType
                    Case MemberTypes.Constructor : ret.Append("Sub New")
                    Case MemberTypes.Event : ret.Append(String.Format("Event {0}", MemberName))
                    Case MemberTypes.Field
                        If DirectCast(Member, FieldInfo).IsLiteral Then
                            ret.Append("Const ")
                        ElseIf Not CBool(Flags And SignatureFlags.AccessModifiers) Then
                            ret.Append("Dim ")
                        End If
                        ret.Append(MemberName)
                    Case MemberTypes.Property : ret.Append(String.Format("Property {0}", MemberName))
                    Case MemberTypes.Method
                        With DirectCast(Member, MethodInfo)
                            If SupportedOperators.Contains(.IsOperator) Then
                                ret.Append("Operator ")
                            Else
                                If .ReturnType.Equals(GetType(System.Void)) Then
                                    ret.Append("Sub ")
                                Else
                                    ret.Append("Function ")
                                End If
                                ret.Append(MemberName)
                            End If
                        End With
                    Case MemberTypes.NestedType, MemberTypes.TypeInfo
                        With DirectCast(Member, Type)
                            If .IsInterface Then
                                ret.Append("Interface ")
                            ElseIf .IsEnum Then
                                ret.Append("Enum ")
                            ElseIf .IsValueType Then
                                ret.Append("Structure ")
                            ElseIf GetType([Delegate]).IsAssignableFrom(Member) AndAlso Not .IsAbstract Then
                                ret.Append("Delegate ")
                                If .GetMethod("Invoke", BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public).ReturnType.Equals(GetType(System.Void)) Then
                                    ret.Append("Sub ")
                                Else
                                    ret.Append("Function ")
                                End If
                            ElseIf .IsClass AndAlso Not .IsGenericType AndAlso Not .IsGenericTypeDefinition AndAlso .IsDefined(GetType(Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute), False) AndAlso .GetConstructors(Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Public).Length = 0 AndAlso .BaseType.Equals(GetType(Object)) Then
                                ret.Append("Module ")
                            Else
                                ret.Append("Class ")
                            End If
                            ret.Append(MemberName)
                        End With
                End Select
            Else
                ret.Append(MemberName)
            End If
            If Flags And SignatureFlags.GenericParameters Then
                'Generic
                If Member.MemberType = MemberTypes.Method Then
                    With DirectCast(Member, MethodInfo)
                        If .IsGenericMethodDefinition Then
                            ret.Append("(Of ")
                            Dim i As Integer = 0
                            For Each g In .GetGenericArguments
                                If i > 0 Then ret.Append(", ")
                                If Flags And SignatureFlags.AllAttributes Then
                                    AppendCustomAttributes(CustomAttributeData.GetCustomAttributes(g), ret, Flags, False, False, Nothing)
                                End If
                                ret.Append(g.Name)
                                If Flags And SignatureFlags.GenericParametersDetails Then AppendGenericConstraints(g, ret, Flags)
                                i += 1
                            Next g
                            ret.Append(")")
                        End If
                    End With
                ElseIf Member.MemberType = MemberTypes.NestedType OrElse Member.MemberType = MemberTypes.TypeInfo Then
                    With DirectCast(Member, Type)
                        If .IsGenericTypeDefinition Then
                            ret.Append("(Of ")
                            Dim i As Integer = 0
                            For Each g In .GetGenericArguments
                                If i > 0 Then ret.Append(", ")
                                If Flags And SignatureFlags.AllAttributes Then
                                    AppendCustomAttributes(CustomAttributeData.GetCustomAttributes(g), ret, Flags, False, False, Nothing)
                                End If
                                ret.Append(g.Name)
                                If Flags And SignatureFlags.GenericParametersDetails Then AppendGenericConstraints(g, ret, Flags)
                                i += 1
                            Next g
                            ret.Append(")")
                        End If
                    End With
                End If
            End If
            If (Flags And SignatureFlags.Signature) OrElse (Flags And SignatureFlags.Type) Then
                'Signature
                'Method and delegate
                If Member.MemberType = MemberTypes.Constructor OrElse Member.MemberType = MemberTypes.Method OrElse ((Member.MemberType = MemberTypes.TypeInfo OrElse Member.MemberType = MemberTypes.NestedType) AndAlso GetType([Delegate]).IsAssignableFrom(Member) AndAlso Not DirectCast(Member, Type).IsAbstract) Then
                    Dim m As MethodBase = If(Member.MemberType = MemberTypes.Method OrElse Member.MemberType = MemberTypes.Constructor, _
                        Member, _
                        DirectCast(Member, Type).GetMethod("Invoke", BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.NonPublic))
                    If m IsNot Nothing Then
                        If Flags And SignatureFlags.Signature Then
                            If m.GetParameters.Length > 0 OrElse Not CBool(Flags And SignatureFlags.NoEmptyBraces) Then
                                ret.Append("(")
                                AppendSignature(m.GetParameters, Flags, ret)
                                ret.Append(")")
                            End If
                        End If
                        If Flags And SignatureFlags.Type Then
                            'Return type
                            If m.MemberType = MemberTypes.Method AndAlso Not DirectCast(m, MethodInfo).ReturnType.Equals(GetType(System.Void)) Then
                                ret.Append(" As ")
                                If Flags And SignatureFlags.AllAttributes Then
                                    AppendCustomAttributes(CustomAttributeData.GetCustomAttributes(DirectCast(m, MethodInfo).ReturnParameter), ret, Flags, False, False, Nothing)
                                End If
                                RepresentTypeName(DirectCast(m, MethodInfo).ReturnType, ret, Flags)
                            End If
                        End If
                    End If
                ElseIf ((Member.MemberType = MemberTypes.TypeInfo OrElse Member.MemberType = MemberTypes.NestedType) AndAlso DirectCast(Member, Type).IsEnum) AndAlso (Flags And SignatureFlags.Type) Then
                    'Enum type
                    ret.Append(" As ")
                    RepresentTypeName([Enum].GetUnderlyingType(Member), ret, Flags)
                ElseIf Member.MemberType = MemberTypes.Property Then
                    'Property signature
                    With DirectCast(Member, PropertyInfo)
                        If Flags And SignatureFlags.Signature Then
                            If .GetIndexParameters.Length > 0 OrElse Not CBool(Flags And SignatureFlags.NoEmptyBraces) Then
                                ret.Append("(")
                                AppendSignature(.GetIndexParameters, Flags, ret)
                                ret.Append(")")
                            End If
                        End If
                        If Flags And SignatureFlags.Type Then
                            ret.Append(" As ")
                            RepresentTypeName(.PropertyType, ret, Flags)
                        End If
                    End With
                ElseIf Member.MemberType = MemberTypes.Event AndAlso (Flags And SignatureFlags.Type) Then
                    'Event type
                    With DirectCast(Member, EventInfo)
                        ret.Append(" As ")
                        RepresentTypeName(.EventHandlerType, ret, Flags)
                    End With
                ElseIf Member.MemberType = MemberTypes.Field AndAlso (Flags And SignatureFlags.Type) Then
                    'Field type
                    With DirectCast(Member, FieldInfo)
                        ret.Append(" As ")
                        RepresentTypeName(.FieldType, ret, Flags)
                    End With
                End If
            End If
            'Ihneritance info
            If Flags And SignatureFlags.Inheritance Then
                Select Case Member.MemberType
                    Case MemberTypes.TypeInfo, MemberTypes.NestedType
                        With DirectCast(Member, Type)
                            If .BaseType IsNot Nothing AndAlso Not .BaseType.Equals(GetType(Object)) Then
                                ret.Append(If(Flags And SignatureFlags.NoMultiline, " : ", vbCrLf & vbTab) & "Inherits ")
                                RepresentTypeName(.BaseType, ret, Flags)
                            End If
                            Dim ImmediateInterfaces = .GetImplementedInterfaces
                            If Not ImmediateInterfaces.IsEmpty Then
                                ret.Append(If(Flags And SignatureFlags.NoMultiline, " : ", vbCrLf & vbTab) & If(.IsInterface, "Inherits ", "Implements "))
                                Dim i As Integer = 0
                                For Each intf In ImmediateInterfaces
                                    If i > 0 Then ret.Append(", ")
                                    RepresentTypeName(intf, ret, Flags)
                                    i += 1
                                Next intf
                            End If
                        End With
                End Select
            End If
            Return ret.ToString
        End Function
        ''' <summary>Converts any operator tor its string representation</summary>
        ''' <param name="op">Operator to convert</param>
        ''' <returns>Operator string representation in Visual Basic.</returns>
        ''' <remarks>Works also for operators that cannot be overloaded or even don't exist in VB.</remarks>
        Private Shared Function OperatorName(ByVal op As Operators) As String
            Select Case op
                Case Operators.Addition : Return "+"
                Case Operators.AddressOf : Return "AddressOf" 'Cannot be overloaded
                Case Operators.AditionAssignment : Return "+=" 'Cannot be overloaded
                Case Operators.Assign : Return ":=" 'Is not used as assignment
                Case Operators.BitwiseAnd : Return "And"
                Case Operators.BitwiseAndAssignment : Return "And=" 'Does not exist
                Case Operators.BitwiseOr : Return "Or"
                Case Operators.BitwiseOrAssignment : Return "Or=" 'Does not exist
                Case Operators.Comma : Return "," 'Does not exist
                Case Operators.Concatenate : Return "&"
                Case Operators.Decrement : Return "--" 'Does not exist
                Case Operators.Division : Return "/"
                Case Operators.DivisionAssignment : Return "/=" 'Cannot be overloaeed
                Case Operators.Equality : Return "="
                Case Operators.ExclusiveOr : Return "Xor"
                Case Operators.ExclusiveOrAssignment : Return "Xor=" 'Does not exist
                Case Operators.Explicit : Return "Narrowing CType"
                Case Operators.Exponent : Return "^"
                Case Operators.False : Return "IsFalse"
                Case Operators.GreaterThan : Return ">"
                Case Operators.GreaterThanOrEqual : Return ">="
                Case Operators.Implicit : Return "Widening CType"
                Case Operators.Increment : Return "++" 'Does not exist
                Case Operators.Inequality : Return "<>"
                Case Operators.IntegerDivision : Return "\"
                Case Operators.LeftShift : Return "<<"
                Case Operators.LeftShiftAssignment : Return "<<=" 'Cannot be overloaded
                Case Operators.LessThan : Return "<"
                Case Operators.LessThanOrEqual : Return "<="
                Case Operators.LogicalAnd : Return "AndAlso" 'Cannot be overloaded
                Case Operators.LogicalNot : Return "Neg" 'Nothing like this exists
                Case Operators.LogicalOr : Return "OrElse" 'Cannot be overloaded
                Case Operators.MemberSelection : Return "." 'Cannot be overloaded and in fact has not the same meaning
                Case Operators.Modulus : Return "Mod"
                Case Operators.ModulusAssignment : Return "Mod=" 'Does not exist
                Case Operators.MultiplicationAssignment : Return "*=" 'Cannot be overloaded
                Case Operators.Multiply : Return "*"
                Case Operators.OnesComplement : Return "Not"
                Case Operators.PointerDereference : Return "OfAddress" 'Nothing like this exists
                Case Operators.PointerToMemberSelection : Return "AddressOf." 'Does not exists
                Case Operators.RightShifAssignment : Return ">>=" 'Cannot be overloaded
                Case Operators.RightShift : Return ">>"
                Case Operators.SignedRightShif : Return ">>S" 'Nothing like this exists
                Case Operators.Subtraction : Return "-"
                Case Operators.SubtractionAssignment : Return "-=" 'Cannot be overloaded
                Case Operators.True : Return "IsTrue"
                Case Operators.UnaryNegation : Return "-"
                Case Operators.UnaryPlus : Return "+"
                Case Operators.UnsignedRightShift : Return ">>U" 'Nothing like this exists
                Case Operators.UnsignedRightShiftAssignment : Return ">>U=" 'Nothign like this exists
                Case Else : Return String.Format("op{0:d}", op)
            End Select
        End Function

        ''' <summary>Appends method signature to <see cref="System.Text.StringBuilder"/></summary>
        ''' <param name="Parameters">parameters that represents signature to append</param>
        ''' <param name="Flags">Flags that controls signature rendering</param>
        ''' <param name="ret"><see cref="System.Text.StringBuilder"/> to append signature to</param>
        ''' <remarks>Braces (()) around parameters are not rendered.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="ret"/> or <paramref name="Parameters"/> is null</exception>
        Private Sub AppendSignature(ByVal Parameters As IEnumerable(Of ParameterInfo), ByVal Flags As SignatureFlags, ByVal ret As System.Text.StringBuilder)
            If ret Is Nothing Then Throw New ArgumentNullException("ret")
            If Parameters Is Nothing Then Throw New ArgumentNullException("Parameters")
            Dim i As Integer = 0
            For Each param In Parameters
                If i > 0 Then ret.Append(", ")
                If Flags And SignatureFlags.AllAttributes Then
                    AppendCustomAttributes(CustomAttributeData.GetCustomAttributes(param), ret, Flags, False, False, Nothing)
                End If
                If Flags And SignatureFlags.SignatureDetails Then
                    If param.IsOptional Then ret.Append("Optional ")
                    If param.ParameterType.IsByRef Then
                        ret.Append("ByRef ")
                    Else
                        ret.Append("ByVal ")
                    End If
                    If param.IsDefined(GetType(ParamArrayAttribute), False) Then ret.Append("ParamArray ")
                    ret.Append(param.Name)
                    ret.Append(" As ")
                End If
                If param.ParameterType.IsByRef Then
                    RepresentTypeName(param.ParameterType.GetElementType, ret, Flags)
                Else
                    RepresentTypeName(param.ParameterType, ret, Flags)
                End If
                i += 1
            Next param
        End Sub

        ''' <summary>tes constraints of generic type to <see cref="System.Text.StringBuilder"/> in Visual-Basic-like way</summary>
        ''' <param name="gPar"><see cref="Type"/> that represents generic parameter</param>
        ''' <param name="ret"><see cref="System.Text.StringBuilder"/> to write constraints to</param>
        ''' <param name="Flags">Flags that controls rendering</param>
        ''' <exception cref="ArgumentNullException"><paramref name="ret"/> or <paramref name="gPar"/> is null</exception>
        ''' <remarks>If <paramref name="Flags"/> ans not set <see cref="SignatureFlags.GenericParametersDetails"/> bit the method exits immediatelly</remarks>
        Private Sub AppendGenericConstraints(ByVal gPar As Type, ByVal ret As System.Text.StringBuilder, ByVal Flags As SignatureFlags)
            If ret Is Nothing Then Throw New ArgumentNullException("ret")
            If gPar Is Nothing Then Throw New ArgumentNullException("gPar")
            If Not CBool(Flags And SignatureFlags.GenericParametersDetails) Then Return
            Dim TypeConstraints = gPar.GetGenericParameterConstraints
            If TypeConstraints.Length > 0 Then
                ret.Append(" As {")
                Dim i As Integer = 0
                For Each Constraint In TypeConstraints
                    If i > 0 Then ret.Append(", ")
                    RepresentTypeName(Constraint, ret, Flags)
                    i += 1
                Next Constraint
            End If
            Dim gpa As GenericParameterAttributes = gPar.GenericParameterAttributes
            Dim constraints As GenericParameterAttributes = gpa And GenericParameterAttributes.SpecialConstraintMask
            Dim opened As Boolean = TypeConstraints.Length > 0
            If constraints <> GenericParameterAttributes.None Then
                If (constraints And GenericParameterAttributes.ReferenceTypeConstraint) <> 0 Then
                    ret.Append(If(opened, ", ", " As {") & "Class")
                    opened = True
                End If
                If (constraints And GenericParameterAttributes.NotNullableValueTypeConstraint) <> 0 Then
                    ret.Append(If(opened, ", ", " As {") & "Structure")
                    opened = True
                End If
                If (constraints And GenericParameterAttributes.DefaultConstructorConstraint) <> 0 Then
                    ret.Append(If(opened, ", ", " As {") & "New")
                    opened = True
                End If
            End If
            If opened Then ret.Append("}")
        End Sub
        ''' <summary>Represents Visual-Baslic-like type name into <see cref="System.Text.StringBuilder"/></summary>
        ''' <param name="Type">Type to represent name of</param>
        ''' <param name="ret"><see cref="System.Text.StringBuilder"/> to write name to</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Type"/> or <paramref name="ret"/> is null</exception>
        Private Sub RepresentTypeName(ByVal Type As Type, ByVal ret As System.Text.StringBuilder, ByVal Flags As SignatureFlags)
            If ret Is Nothing Then Throw New ArgumentNullException("ret")
            If Type Is Nothing Then Throw New ArgumentNullException("Type")
            If Type.IsGenericParameter Then
                ret.Append(Type.Name)
                If Flags And SignatureFlags.GenericParametersDetails Then AppendGenericConstraints(Type, ret, Flags)
                Exit Sub
            ElseIf Type.IsArray Then
                RepresentTypeName(Type.GetElementType, ret, Flags)
                ret.Append("(")
                For i = 0 To Type.GetArrayRank - 1
                    If i > 0 Then ret.Append(", ")
                Next i
                ret.Append(")")
                Exit Sub
            ElseIf Type.IsByRef OrElse Type.IsPointer Then
                Stop
            ElseIf Type.IsNested Then
                RepresentTypeName(Type.DeclaringType, ret, Flags)
                ret.Append(".")
            ElseIf Type.Namespace <> "" AndAlso Not CBool(Flags And SignatureFlags.AllShortNames) Then
                ret.Append(Type.Namespace & ".")
            End If
            If Type.IsGenericTypeDefinition OrElse Type.IsGenericType Then
                Dim muggle = String.Format(System.Globalization.CultureInfo.InvariantCulture, "`{0}", Type.GetGenericArguments.Length)
                If Type.Name.EndsWith(muggle) Then
                    ret.Append(Type.Name.Substring(0, Type.Name.Length - muggle.Length))
                Else
                    ret.Append(Type.Name)
                End If
                ret.Append("(Of ")
                Dim i As Integer = 0
                For Each GPar In Type.GetGenericArguments
                    If i > 0 Then ret.Append(", ")
                    RepresentTypeName(GPar, ret, If(Type.IsGenericTypeDefinition, Flags, Flags And Not SignatureFlags.GenericParametersDetails))
                    i += 1
                Next
                ret.Append(")")
            Else
                ret.Append(Type.Name)
            End If
        End Sub
        ''' <summary>Gets member access modifiers</summary>
        ''' <param name="Member">Member to get modifiers of</param>
        ''' <returns>Visual-Basic-like member access modifiers</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Member"/> is null</exception>
        Private Function GetAccessModifiers(ByVal Member As MemberInfo) As String
            If Member Is Nothing Then Throw New ArgumentNullException("Member")
            With Member
                If .IsPublic Then
                    Return "Public"
                ElseIf .IsPrivate Then
                    Return "Private"
                ElseIf .IsFamily Then
                    Return "Protected"
                ElseIf .IsAssembly Then
                    Return "Friend"
                ElseIf .IsFamilyOrAssembly Then
                    Return "Protected Friend"
                ElseIf .IsFamilyAndAssembly Then
                    Return "Friend Protected"
                Else
                    Return ""
                End If
            End With
        End Function
        ''' <summary>Ways of rendering custom attributes</summary>
        Private Enum CustomAttributeFlags
            ''' <summary>Normal way</summary>
            Other = False
            ''' <summary>Assembly way</summary>
            Assembly = True
            ''' <summary>PE module way</summary>
            [Module] = 333
        End Enum

        ''' <summary>gets representation of a module</summary>
        ''' <param name="Module">Module to represent</param>
        ''' <param name="Flags">Controls how the signature will be rendered</param>
        ''' <returns>String representation of <paramref name="Module"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Module"/> is null</exception>
        Public Function GetSignature(ByVal [Module] As System.Reflection.Module, Optional ByVal Flags As SignatureFlags = SignatureFlags.ShortNameOnly) As String Implements ISignatureProvider.GetSignature
            If [Module] Is Nothing Then Throw New ArgumentNullException("Module")
            Dim ret As New System.Text.StringBuilder
            If Flags And SignatureFlags.ObjectType Then ret.Append("PE Module ")
            If Flags And SignatureFlags.LongName Then
                ret.Append([Module].FullyQualifiedName)
            Else
                ret.Append([Module].Name)
            End If
            If Flags And SignatureFlags.AllAttributes Then
                If Not CBool(Flags And SignatureFlags.NoMultiline) Then ret.Append(vbCrLf)
                AppendCustomAttributes(CustomAttributeData.GetCustomAttributes([Module]), ret, Flags, CustomAttributeFlags.Module, True, Nothing)
            End If
            Return ret.ToString
        End Function

        ''' <summary>Gets string representation of a namespace</summary>
        ''' <param name="Namespace">Namespace to represent</param>
        ''' <param name="Flags">Controls how the signature will be rendered</param>
        ''' <returns>String representation of <paramref name="Namespace"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Namespace"/> is null</exception>
        Public Function GetSignature(ByVal [Namespace] As NamespaceInfo, Optional ByVal Flags As SignatureFlags = SignatureFlags.LongName Or SignatureFlags.ObjectType) As String Implements ISignatureProvider.GetSignature
            If [Namespace] Is Nothing Then Throw New ArgumentNullException("Namespace")
            Dim ret As New System.Text.StringBuilder
            If Flags And SignatureFlags.ObjectType Then ret.Append("Namespace ")
            If Flags And SignatureFlags.LongName Then
                ret.Append([Namespace].Name)
            Else
                ret.Append([Namespace].ShortName)
            End If
            Return ret.ToString
        End Function

        ''' <summary>Gets string representation of attached custom attribute</summary>
        ''' <param name="AttributeData"><see cref="CustomAttributeData"/> to show</param>
        ''' <param name="Flags">Controls how the signature will be rendered</param>
        ''' <returns>String representation of given custom attribute</returns>
        Public Function GetAttribute(ByVal AttributeData As System.Reflection.CustomAttributeData, ByVal Flags As SignatureFlags) As String Implements ISignatureProvider.GetAttribute
            Return GetAttributes(New CustomAttributeData() {AttributeData}, Flags)
        End Function

        ''' <summary>Gets string representation of attached custom attributes</summary>
        ''' <param name="AttributeData">Collection of <see cref="CustomAttributeData"/> to show</param>
        ''' <param name="Flags">Controls how the signature will be rendered</param>
        ''' <returns>String representation of given custom attributes</returns>
        Public Function GetAttributes(ByVal AttributeData As System.Collections.Generic.IEnumerable(Of System.Reflection.CustomAttributeData), ByVal Flags As SignatureFlags) As String Implements ISignatureProvider.GetAttributes
            Dim ret As New Text.StringBuilder
            If (Flags And SignatureFlags.SomeAttributes) OrElse (Flags And SignatureFlags.AllAttributes) Then
                Dim AData As IList(Of CustomAttributeData)
                If TypeOf AttributeData Is IList(Of CustomAttributeData) Then
                    AData = AttributeData
                Else
                    AData = New List(Of CustomAttributeData)(AttributeData)
                End If
                AppendCustomAttributes(AData, ret, Flags, False, (Flags And SignatureFlags.NoMultiline) <> SignatureFlags.NoMultiline, _
                    Function(at As Type) _
                        at.Equals(GetType(ExtensionAttribute)) OrElse _
                        at.Equals(GetType(FlagsAttribute)) OrElse _
                        at.Equals(GetType(StructLayoutAttribute)) OrElse _
                        at.Equals(GetType(DllImportAttribute)))
            End If
            Return ret.ToString
        End Function
    End Class
End Namespace
#End If