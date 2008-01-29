Imports System.Reflection

#If Config <= Nightly Then 'Stage:Nightly
'ASAP
Namespace ReflectionT
    ''' <summary>Provides interface of object that provides string representation of various reflection object</summary>
    Public Interface ISignatureProvider
        ''' <summary>Gets string representation of an assembly</summary>
        ''' <param name="Assembly">Assembly to represent</param>
        ''' <returns>String representation of <paramref name="Assembly"/></returns>
        Function GetSignature(ByVal Assembly As Assembly) As String
        ''' <summary>gets representation of a module</summary>
        ''' <param name="Module">Module to represent</param>
        ''' <returns>String representation of <paramref name="Module"/></returns>
        Function GetSignature(ByVal [Module] As [Module]) As String
        ''' <summary>Gets string representation of a namespace</summary>
        ''' <param name="Namespace">Namespace to represent</param>
        ''' <returns>String representation of <paramref name="Namespace"/></returns>
        Function GetSignature(ByVal [Namespace] As NamespaceInfo) As String
        ''' <summary>Gets string representation of a member</summary>
        ''' <param name="Member">Member to represent</param>
        ''' <param name="WithModifiers">If true modifiers (such as Public, Static, etc.) should be included in signature. If falůse they should not.</param>
        ''' <param name="Long">If true, long name should be provided (fully qualified)</param>
        ''' <returns>String representation of <paramref name="Member"/></returns>
        Function GetSignature(ByVal Member As MemberInfo, ByVal WithModifiers As Boolean, ByVal [Long] As Boolean) As String
    End Interface
    ''' <summary>Provides string representation of various reflection object using Visual Basic syntax</summary>
    Public Class VisualBasicSignatureProvider : Implements ISignatureProvider
        ''' <summary>Gets string representation of an assembly</summary>
        ''' <param name="Assembly">Assembly to represent</param>
        ''' <returns>String representation of <paramref name="Assembly"/></returns>
        Public Function GetSignature(ByVal Assembly As System.Reflection.Assembly) As String Implements ISignatureProvider.GetSignature
            Return String.Format("'Assembly {0}", Assembly.FullName)
        End Function

        ''' <summary>Gets string representation of a member</summary>
        ''' <param name="Member">Member to represent</param>
        ''' <param name="WithModifiers">If true modifiers (such as Public, Static, etc.) should be included in signature. If falůse they should not.</param>
        ''' <param name="Long">If true, long name should be provided (fully qualified)</param>
        ''' <returns>String representation of <paramref name="Member"/></returns>
        Public Function GetSignature(ByVal Member As System.Reflection.MemberInfo, ByVal WithModifiers As Boolean, ByVal [Long] As Boolean) As String Implements ISignatureProvider.GetSignature
            Dim ret As New System.Text.StringBuilder
            If WithModifiers Then
                'Access modifiers
                ret.Append(GetAccessModifiers(Member) & " ")
                'Static modifiers
                Select Case Member.MemberType
                    Case MemberTypes.Constructor, MemberTypes.Event, MemberTypes.Field, MemberTypes.Method, MemberTypes.Property
                        If Member.IsStatic Then ret.Append("Static ")
                End Select
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
            If [Long] Then
                Select Case Member.MemberType
                    Case MemberTypes.TypeInfo : MemberName = DirectCast(Member, Type).FullName
                    Case Else
                        MemberName = Member.DeclaringType.FullName & "." & Member.Name
                End Select
            Else
                MemberName = Member.Name
            End If
            'Type
            Select Case Member.MemberType
                Case MemberTypes.Constructor : ret.Append("Sub New")
                Case MemberTypes.Event : ret.Append(String.Format("Event {0}", MemberName))
                Case MemberTypes.Field
                    If DirectCast(Member, FieldInfo).IsLiteral Then
                        ret.Append("Const ")
                    ElseIf Not WithModifiers Then
                        ret.Append("Dim ")
                    End If
                    ret.Append(MemberName)
                Case MemberTypes.Property : ret.Append(String.Format("Property {0}", MemberName))
                Case MemberTypes.Method
                    With DirectCast(Member, MethodInfo)
                        If .ReturnType.Equals(GetType(System.Void)) Then
                            ret.Append("Sub ")
                        Else
                            ret.Append("Function ")
                        End If
                        ret.Append(MemberName)
                    End With
                Case MemberTypes.NestedType, MemberTypes.TypeInfo
                    With DirectCast(Member, Type)
                        If .IsInterface Then
                            ret.Append("Interface ")
                        ElseIf .IsEnum Then
                            ret.Append("Enum ")
                        ElseIf .IsValueType Then
                            ret.Append("Structure ")
                        ElseIf GetType([Delegate]).IsAssignableFrom(Member) Then
                            ret.Append("Delegate ")
                            If .GetMethod("Invoke", BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public).ReflectedType.Equals(GetType(System.Void)) Then
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
            'Generic
            If Member.MemberType = MemberTypes.Method Then
                With DirectCast(Member, MethodInfo)
                    If .IsGenericMethodDefinition Then
                        ret.Append("(Of ")
                        Dim i As Integer = 0
                        For Each g In .GetGenericArguments
                            If i > 0 Then ret.Append(", ")
                            ret.Append(g.Name)
                            AppendGenericConstraints(g, ret)
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
                            AppendGenericConstraints(g, ret)
                            ret.Append(g.Name)
                            i += 1
                        Next g
                        ret.Append(")")
                    End If
                End With
            End If
            'Signature
            If Member.MemberType = MemberTypes.Constructor OrElse Member.MemberType = MemberTypes.Method OrElse ((Member.MemberType = MemberTypes.TypeInfo OrElse Member.MemberType = MemberTypes.NestedType) AndAlso GetType([Delegate]).IsAssignableFrom(Member)) Then
                Dim m As MethodBase = If(Member.MemberType = MemberTypes.Method OrElse Member.MemberType = MemberTypes.Constructor, _
                    Member, _
                    DirectCast(Member, Type).GetMethod("Invoke", BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.NonPublic))
                ret.Append("(")
                Dim i As Integer = 0
                For Each param In m.GetParameters
                    If i > 0 Then ret.Append(", ")
                    If param.IsOptional Then ret.Append("Optional ")
                    ret.Append(param.Name)
                    ret.Append(" As ")
                    'TODO: ByVal, ByRef, ParamArray
                    RepresentTypeName(param.ParameterType, ret)
                    i += 1
                Next param
                ret.Append(")")
                If m.MemberType = MemberTypes.Method AndAlso Not DirectCast(m, MethodInfo).ReturnType.Equals(GetType(System.Void)) Then
                    ret.Append(" As ")
                    RepresentTypeName(DirectCast(m, MethodInfo).ReturnType, ret)
                End If
            End If
            Return ret.ToString
        End Function
        Private Sub AppendGenericConstraints(ByVal gPar As Type, ByVal ret As System.Text.StringBuilder)
            Dim TypeConstraints = gPar.GetGenericParameterConstraints
            If TypeConstraints.Length > 0 Then
                ret.Append(" As {")
                Dim i As Integer = 0
                For Each Constraint In TypeConstraints
                    If i > 0 Then ret.Append(", ")
                    RepresentTypeName(Constraint, ret)
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
        Private Sub RepresentTypeName(ByVal Type As Type, ByVal ret As System.Text.StringBuilder)
            'TODO: Do something smart here and represent generic types and array in good way
            ret.Append(Type.FullName)
        End Sub
        ''' <summary>Gets member access modifiers</summary>
        ''' <param name="Member">Member to get modifiers of</param>
        ''' <returns>Visual-Basic-like member access modifiers</returns>
        Private Function GetAccessModifiers(ByVal Member As MemberInfo) As String
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

        ''' <summary>gets representation of a module</summary>
        ''' <param name="Module">Module to represent</param>
        ''' <returns>String representation of <paramref name="Module"/></returns>
        Public Function GetSignature(ByVal [Module] As System.Reflection.Module) As String Implements ISignatureProvider.GetSignature
            Return String.Format("'PE Module {0}", [Module].FullyQualifiedName)
        End Function

        ''' <summary>Gets string representation of a namespace</summary>
        ''' <param name="Namespace">Namespace to represent</param>
        ''' <returns>String representation of <paramref name="Namespace"/></returns>
        Public Function GetSignature(ByVal [Namespace] As NamespaceInfo) As String Implements ISignatureProvider.GetSignature
            Return String.Format("Namespace {0}", [Namespace].Name)
        End Function
    End Class
End Namespace
#End If