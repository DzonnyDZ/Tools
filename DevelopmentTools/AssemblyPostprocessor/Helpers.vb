Imports Mono.Cecil
Imports System.Runtime.CompilerServices


Public Module CecilHelpers

    ''' <summary>Gets <see cref="Type"/> from <see cref="TypeReference"/></summary>
    ''' <param name="type">A <see cref="TypeReference"/> to resolve</param>
    ''' <returns>A <see cref="Type"/> representing the <paramref name="type"/> <see cref="TypeReference"/></returns>
    ''' <exception cref="ArgumentNullException"><paramref name="type"/> is null</exception>
    ''' <exception cref="IO.FileNotFoundException">Cannot find assembly for type -or- type requires a dependent assembly that could not be found</exception>
    ''' <exception cref="IO.FileLoadException">An assembly file (or dependent assembly file) that was found could not be loaded.</exception>
    ''' <exception cref="BadImageFormatException">Attempt to load an invalid assembly -or- Version 2.0 or later of the common language runtime is currently loaded and the assembly was compiled with a later version.</exception>
    ''' <exception cref="NotSupportedException"><paramref name="type"/> is generic parameter defined on somethiong else than type (e.g. method) - this is not supported yet.</exception>
    Public Function [GetType](type As TypeReference) As Type ', currentModule$
        If type Is Nothing Then Throw New ArgumentNullException("type")
        Dim resolvedType = type.Resolve
        If type.IsGenericParameter Then
            Dim type_gen As GenericParameter = type
            If TypeOf type_gen.Owner Is TypeReference Then
                Dim owner = [GetType](DirectCast(type_gen.Owner, TypeReference)) ', currentModule)
                Return owner.GetGenericArguments()(type_gen.Position)
            Else
                Throw New NotSupportedException(String.Format(My.Resources.Resources.GenParsNotSupported, type_gen.Owner.GetType.Name))
            End If
        End If
        Dim asm As System.Reflection.Assembly = Nothing

        For Each lasm In My.Application.Info.LoadedAssemblies
            If lasm.FullName = resolvedType.Module.Assembly.FullName Then
                asm = lasm
                Exit For
            End If
        Next
        If asm Is Nothing Then _
            asm = System.Reflection.Assembly.LoadFrom(resolvedType.Module.Assembly.MainModule.FullyQualifiedName)
        Dim ret = asm.GetType(resolvedType.FullName)
        If type.IsArray Then
            Return ret.MakeArrayType(DirectCast(type, ArrayType).Rank)
        ElseIf type.IsByReference Then
            Return ret.MakeByRefType
        ElseIf type.IsPointer Then
            Return ret.MakePointerType
        ElseIf type.IsGenericInstance Then
            Return ret.MakeGenericType((From garg In DirectCast(type, GenericInstanceType).GenericArguments Select [GetType](garg)).ToArray) 'currentModule
        Else
            Return ret
        End If
    End Function

    <Extension()>
    Public Function ToTypeReference(type As Type) As TypeReference

        If type.IsArray Then
            Return New ArrayType(type.GetElementType.ToTypeReference, type.GetArrayRank)
        ElseIf type.IsByRef Then
            Return New ByReferenceType(type.GetElementType.ToTypeReference)
        ElseIf type.IsPointer Then
            Return New PointerType(type.GetElementType.ToTypeReference)
        ElseIf type.IsGenericType AndAlso Not type.IsGenericTypeDefinition Then
            Dim gtype = type.GetGenericTypeDefinition.ToTypeReference
            Dim ret = New GenericInstanceType(gtype)
            For Each gpar In type.GetGenericArguments
                ret.GenericArguments.Add(gpar.ToTypeReference)
            Next
            Return ret
        ElseIf type.IsGenericParameter Then
            If type.DeclaringMethod Is Nothing Then
                Return New GenericParameter(type.Name, type.DeclaringType.ToTypeReference)
            Else
                Throw New NotSupportedException
            End If
        Else
            Dim [module] = ModuleDefinition.ReadModule(type.Module.FullyQualifiedName)
            Return New TypeReference(type.Namespace, type.Name, [module], [module])
        End If
    End Function

    <Extension()>
    Public Function TypeEquals(a As TypeReference, b As TypeReference) As Boolean
        If a Is Nothing OrElse b Is Nothing Then Return a Is Nothing AndAlso b Is Nothing
        If a.IsArray Then
            Return b.IsArray AndAlso DirectCast(a, ArrayType).Rank = DirectCast(b, ArrayType).Rank AndAlso DirectCast(a, ArrayType).ElementType.TypeEquals(DirectCast(b, ArrayType).ElementType)
        ElseIf a.IsByReference Then
            Return b.IsByReference AndAlso DirectCast(a, ByReferenceType).ElementType.TypeEquals(DirectCast(b, ByReferenceType).ElementType)
        ElseIf a.IsPointer Then
            Return b.IsPointer AndAlso DirectCast(a, PointerType).ElementType.TypeEquals(DirectCast(b, PointerType).ElementType)
        ElseIf a.IsGenericInstance Then
            If Not b.IsGenericInstance Then Return False
            Dim ag As GenericInstanceType = a
            Dim bg As GenericInstanceType = b
            If Not ag.ElementType.TypeEquals(bg.ElementType) Then Return False
            If ag.GenericArguments.Count <> bg.GenericArguments.Count Then Return False
            For i = 0 To ag.GenericArguments.Count - 1
                If Not ag.GenericArguments(i).TypeEquals(bg.GenericArguments(i)) Then Return False
            Next
            Return True
        ElseIf a.IsPinned Then
            Return b.IsPinned AndAlso DirectCast(a, PinnedType).ElementType.TypeEquals(DirectCast(b, PinnedType).ElementType)
        ElseIf a.IsRequiredModifier Then
            Return b.IsRequiredModifier AndAlso DirectCast(a, RequiredModifierType).ElementType.TypeEquals(DirectCast(b, RequiredModifierType).ElementType) AndAlso DirectCast(a, RequiredModifierType).ModifierType.TypeEquals(DirectCast(b, RequiredModifierType).ModifierType)
        ElseIf a.IsOptionalModifier Then
            Return b.IsOptionalModifier AndAlso DirectCast(a, OptionalModifierType).ElementType.TypeEquals(DirectCast(b, OptionalModifierType).ElementType) AndAlso DirectCast(a, OptionalModifierType).ModifierType.TypeEquals(DirectCast(b, OptionalModifierType).ModifierType)
        ElseIf a.IsSentinel Then
            Return b.IsSentinel AndAlso DirectCast(a, SentinelType).ElementType.TypeEquals(DirectCast(b, SentinelType).ElementType)
        ElseIf a.IsFunctionPointer Then
            If Not b.IsFunctionPointer Then Return False
            Dim af As FunctionPointerType = a
            Dim bf As FunctionPointerType = b
            If af.CallingConvention <> bf.CallingConvention OrElse af.ExplicitThis <> bf.ExplicitThis OrElse af.HasThis <> bf.HasThis OrElse Not af.ReturnType.TypeEquals(bf.ReturnType) OrElse af.Parameters.Count <> bf.Parameters.Count Then Return False
            For i = 0 To af.Parameters.Count - 1
                If Not af.Parameters(i).ParameterType.TypeEquals(bf.Parameters(i).ParameterType) Then Return False
            Next
            Return True
        ElseIf a.IsGenericParameter Then
            If Not b.IsGenericParameter Then Return False
            Dim ag As GenericParameter = a
            Dim bg As GenericParameter = b
            If ag.Position <> bg.Position Then Return False
            If TypeOf ag.Owner Is TypeReference Then
                If Not TypeOf bg.Owner Is TypeReference Then Return False
                Return DirectCast(ag.Owner, TypeReference).TypeEquals(DirectCast(bg.Owner, TypeReference))
            ElseIf TypeOf ag.Owner Is MethodDefinition Then
                If Not TypeOf bg.Owner Is MethodDefinition Then Return False
                Dim aMethod As MethodDefinition = ag.Owner
                Dim bMethod As MethodDefinition = bg.Owner
                Return aMethod.MetadataToken = bMethod.MetadataToken
            Else
                Throw New NotSupportedException("Equality comparison of types representing generic arguments of something else then type and method is not supported")
            End If
        ElseIf a.IsNested Then
            Return b.IsNested AndAlso a.DeclaringType.TypeEquals(b.DeclaringType) AndAlso a.Name = b.Name
        Else
            Return a.FullName = b.FullName
        End If
    End Function

    <Extension()>
    Public Function SupplyGenericParameters(type As TypeReference, source As GenericInstanceType) As TypeReference
        If type Is Nothing Then Throw New ArgumentNullException("type")
        If source Is Nothing Then Return type
        If type.IsGenericParameter Then
            Dim resolvedGenericSource = source.ElementType.Resolve
            Dim gp As GenericParameter = type
            For i = 0 To resolvedGenericSource.GenericParameters.Count - 1
                If resolvedGenericSource.GenericParameters(i).TypeEquals(gp) Then Return source.GenericArguments(i)
            Next
            Return type
        ElseIf type.TypeEquals(source.ElementType) Then
            Return source
        ElseIf type.IsArray Then
            Return New ArrayType(DirectCast(type, ArrayType).ElementType.SupplyGenericParameters(source), DirectCast(type, ArrayType).Rank)
        ElseIf type.IsByReference Then
            Return New ByReferenceType(DirectCast(type, ByReferenceType).ElementType.SupplyGenericParameters(source))
        ElseIf type.IsPointer Then
            Return New PointerType(DirectCast(type, PointerType).ElementType.SupplyGenericParameters(source))
        ElseIf type.IsPinned Then
            Return New PinnedType(DirectCast(type, PinnedType).ElementType.SupplyGenericParameters(source))
        ElseIf type.IsSentinel Then
            Return New SentinelType(DirectCast(type, SentinelType).ElementType.SupplyGenericParameters(source))
        ElseIf type.IsOptionalModifier Then
            Return New OptionalModifierType(DirectCast(type, OptionalModifierType).ModifierType.SupplyGenericParameters(source), DirectCast(type, OptionalModifierType).ElementType.SupplyGenericParameters(source))
        ElseIf type.IsRequiredModifier Then
            Return New RequiredModifierType(DirectCast(type, RequiredModifierType).ModifierType.SupplyGenericParameters(source), DirectCast(type, RequiredModifierType).ElementType.SupplyGenericParameters(source))
        ElseIf type.IsFunctionPointer Then
            Throw New NotSupportedException("Function pointers are not supported yet")
        ElseIf type.IsGenericInstance Then
            Dim ret = New GenericInstanceType(DirectCast(type, GenericInstanceType).ElementType)
            For Each gpar In DirectCast(type, GenericInstanceType).GenericArguments
                ret.GenericArguments.Add(gpar.SupplyGenericParameters(source))
            Next
            Return ret
        Else
            Return type
        End If
    End Function

End Module


