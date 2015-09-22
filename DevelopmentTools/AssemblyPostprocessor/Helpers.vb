Imports Mono.Cecil
Imports System.Runtime.CompilerServices

''' <summary>Contains extension and other helper methods for working with <see cref="Mono.Cecil"/> and for <see cref="Mono.Cecil"/>-<see cref="System.Reflection"/> interop.</summary>
''' <version version="1.5.4">This module is new in version 1.5.4</version>
Public Module CecilHelpers

    ''' <summary>Gets <see cref="Type"/> from <see cref="TypeReference"/></summary>
    ''' <param name="type">A <see cref="TypeReference"/> to resolve</param>
    ''' <returns>A <see cref="Type"/> representing the <paramref name="type"/> <see cref="TypeReference"/></returns>
    ''' <exception cref="ArgumentNullException"><paramref name="type"/> is null</exception>
    ''' <exception cref="IO.FileNotFoundException">Cannot find assembly for type -or- type requires a dependent assembly that could not be found</exception>
    ''' <exception cref="IO.FileLoadException">An assembly file (or dependent assembly file) that was found could not be loaded.</exception>
    ''' <exception cref="BadImageFormatException">Attempt to load an invalid assembly -or- Version 2.0 or later of the common language runtime is currently loaded and the assembly was compiled with a later version.</exception>
    ''' <exception cref="NotSupportedException"><paramref name="type"/> is generic parameter defined on something else than type (e.g. method) - this is not supported yet.</exception>
    Public Function [GetType](type As TypeReference) As Type
        If type Is Nothing Then Throw New ArgumentNullException("type")
        Dim resolvedType = type.Resolve
        If type.IsGenericParameter Then
            Dim type_gen As GenericParameter = type
            If TypeOf type_gen.Owner Is TypeReference Then
                Dim owner = [GetType](DirectCast(type_gen.Owner, TypeReference))
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
            Return ret.MakeGenericType((From garg In DirectCast(type, GenericInstanceType).GenericArguments Select [GetType](garg)).ToArray)
        Else
            Return ret
        End If
    End Function

    ''' <summary>Constructs <see cref="TypeReference"/> from <see cref="System.Type"/></summary>
    ''' <param name="type">A <see cref="Type"/> to mage <see cref="TypeReference"/> from</param>
    ''' <param name="module">A module to import <see cref="TypeReference"/> for</param>
    ''' <returns>A <see cref="TypeReference"/> pointing to a type represented by <paramref name="type"/></returns>
    ''' <exception cref="ArgumentNullException"><paramref name="type"/> is null</exception>
    ''' <seealso cref="M:Mono.Cecil.ModuleDefinition.Import(System.Type)"/>
    <Extension()>
    Public Function ToTypeReference(type As Type, [module] As ModuleDefinition) As TypeReference
        If type Is Nothing Then Throw New ArgumentNullException("type")
        Return [module].Import(type)
    End Function

    ''' <summary>Gets value indicating if two <see cref="TypeReference">TypeReferences</see> point to the same type</summary>
    ''' <param name="a">A <see cref="TypeReference"/></param>
    ''' <param name="b">A <see cref="TypeReference"/></param>
    ''' <returns>Returns true if <paramref name="a"/> and <paramref name="b"/> represent the same type or are both null.</returns>
    ''' <exception cref="NotSupportedException"><paramref name="a"/> <see cref="TypeReference.IsGenericParameter">is generic parameter</see> and <paramref name="a"/>.<see cref="GenericParameter.Owner">Owner</see> is neither <see cref="TypeReference"/> nor <see cref="MethodDefinition"/> (should not happen).</exception>
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
                Throw New NotSupportedException(My.Resources.ex_UnsupportedGParOwner)
            End If
        ElseIf a.IsNested Then
            Return b.IsNested AndAlso a.DeclaringType.TypeEquals(b.DeclaringType) AndAlso a.Name = b.Name
        Else
            Return a.FullName = b.FullName AndAlso a.Resolve.Module.Assembly.FullName = b.Resolve.Module.Assembly.FullName
        End If
    End Function

    ''' <summary>Supplies types for generic parameters used in a <see cref="TypeReference"/></summary>
    ''' <param name="type">A type reference containing generic parameters (type placeholders)</param>
    ''' <param name="source">Source of actual types for type parameters</param>
    ''' <returns><paramref name="type"/> with generic parameters resolved from <paramref name="source"/>; <paramref name="source"/> if <paramref name="type"/> is null</returns>
    ''' <exception cref="ArgumentNullException"><paramref name="type"/> is null</exception>
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
            Dim fp As FunctionPointerType = type
            Dim ret As New FunctionPointerType()
            ret.CallingConvention = fp.CallingConvention
            ret.DeclaringType = fp.DeclaringType.SupplyGenericParameters(source)
            ret.ExplicitThis = fp.ExplicitThis
            For Each gpar In fp.GenericParameters
                ret.GenericParameters.Add(New GenericParameter(gpar.Name, ret))
            Next
            ret.HasThis = fp.HasThis
            ret.Name = fp.Name
            ret.Namespace = fp.Namespace
            For Each par In fp.Parameters
                ret.Parameters.Add(New ParameterDefinition(par.Name, par.Attributes, par.ParameterType.SupplyGenericParameters(source)))
            Next
            ret.ReturnType = fp.ReturnType.SupplyGenericParameters(source)
            Return ret
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


    ''' <summary>Supplies generic arguments for a type</summary>
    ''' <param name="self">A type to supply generic arguments for</param>
    ''' <param name="arguments">Types to be supplied for generic arguments</param>
    ''' <returns>Type <paramref name="self"/> with generic arguments supplied; <paramref name="self"/> if <paramref name="arguments"/> is null or empty.</returns>
    ''' <exception cref="ArgumentException">Number of generic arguments of type <paramref name="self"/> and number arguments passed in <paramref name="arguments"/> differ</exception>
    ''' <exception cref="ArgumentNullException"><paramref name="self"/></exception>
    ''' <remarks>This method comes from Mono.Cecli test suite</remarks>
    <Extension()> _
    Public Function MakeGenericType(self As TypeReference, ParamArray arguments As TypeReference()) As GenericInstanceType
        If self Is Nothing Then Throw New ArgumentNullException("self")
        If arguments Is Nothing OrElse arguments.Length = 0 Then Return self

        If self.GenericParameters.Count <> arguments.Length Then
            Throw New ArgumentException()
        End If

        Dim instance = New GenericInstanceType(self)
        For Each argument In arguments
            instance.GenericArguments.Add(argument)
        Next

        Return instance
    End Function

    ''' <summary>Supplies generic arguments for a method and method declaring type</summary>
    ''' <param name="self">A method to supply generic arguments for</param>
    ''' <param name="arguments">Types to be supplied for generic arguments</param>
    ''' <returns>Method reference to method that was made generic instance and which declaring type was made generic instance; <paramref name="self"/> if <paramref name="arguments"/> is null or empty</returns>
    ''' <exception cref="ArgumentException">Number of arguments in <paramref name="arguments"/> is different than number of generic arguments in <paramref name="self"/>.<see cref="MethodReference.DeclaringType">DeclaringType</see>.</exception>
    ''' <remarks>This method comes from Mono.Cecil test suite</remarks>
    <Extension()>
    Public Function MakeGeneric(self As MethodReference, ParamArray arguments As TypeReference()) As MethodReference
        If self Is Nothing Then Throw New ArgumentNullException("self")
        If arguments Is Nothing OrElse arguments.Length = 0 Then Return self
        Dim reference = New MethodReference(self.Name, self.ReturnType) With { _
          .DeclaringType = self.DeclaringType.MakeGenericType(arguments), _
          .HasThis = self.HasThis, _
          .ExplicitThis = self.ExplicitThis, _
          .CallingConvention = self.CallingConvention _
        }

        For Each parameter In self.Parameters
            reference.Parameters.Add(New ParameterDefinition(parameter.ParameterType))
        Next

        For Each generic_parameter In self.GenericParameters
            reference.GenericParameters.Add(New GenericParameter(generic_parameter.Name, reference))
        Next

        Return reference
    End Function

    ''' <summary>Gets all methods of a property</summary>
    ''' <param name="property">A property to get methods of</param>
    ''' <returns>All methods associated with property <paramref name="property"/></returns>
    ''' <exception cref="ArgumentNullException"><paramref name="property"/> is null</exception>
    <Extension()>
    Function AllMethods([property] As PropertyDefinition) As IEnumerable(Of MethodDefinition)
        If [property] Is Nothing Then Throw New ArgumentNullException("property")
        Return If([property].GetMethod Is Nothing, New MethodDefinition() {}, New MethodDefinition() {[property].GetMethod}).Union(
               If([property].SetMethod Is Nothing, New MethodDefinition() {}, New MethodDefinition() {[property].SetMethod})).Union(
               [property].OtherMethods)
    End Function

    ''' <summary>Gets all methods of an event</summary>
    ''' <param name="event">An event to get methods of</param>
    ''' <returns>All methods associated with event <paramref name="event"/></returns>
    ''' <exception cref="ArgumentNullException"><paramref name="event"/> is null</exception>
    <Extension()>
    Public Function AllMethods([event] As EventDefinition) As IEnumerable(Of MethodDefinition)
        If [event] Is Nothing Then Throw New ArgumentNullException("event")
        Return If([event].AddMethod Is Nothing, New MethodDefinition() {}, New MethodDefinition() {[event].AddMethod}).Union(
               If([event].RemoveMethod Is Nothing, New MethodDefinition() {}, New MethodDefinition() {[event].RemoveMethod})).Union(
               If([event].InvokeMethod Is Nothing, New MethodDefinition() {}, New MethodDefinition() {[event].InvokeMethod})).Union(
               [event].OtherMethods)
    End Function


    ''' <summary>Compares signatures of two methods and gets value indicating if they are same</summary>
    ''' <param name="a">A method to compare signature of</param>
    ''' <param name="b">A method to compare signature of</param>
    ''' <param name="aGenParSupplier">Supplies generic parameters for <paramref name="a"/></param>
    ''' <param name="bGenParSupplier">Supplies generic parameters for <paramref name="b"/></param>
    ''' <returns>True if method signatures of <paramref name="a"/> and <paramref name="b"/> are same</returns>
    ''' <exception cref="ArgumentNullException"><paramref name="a"/> or <paramref name="b"/> is null</exception>
    Public Function IsSameSignature(a As MethodDefinition, b As MethodDefinition, Optional ByVal aGenParSupplier As GenericInstanceType = Nothing, Optional ByVal bGenParSupplier As GenericInstanceType = Nothing) As Boolean
        If a Is Nothing Then Throw New ArgumentNullException("a")
        If b Is Nothing Then Throw New ArgumentNullException("b")
        If a.Parameters.Count <> b.Parameters.Count Then Return False
        If Not a.ReturnType.SupplyGenericParameters(aGenParSupplier).TypeEquals(b.ReturnType.SupplyGenericParameters(bGenParSupplier)) Then Return False
        For i = 0 To a.Parameters.Count - 1
            If Not a.Parameters(i).ParameterType.SupplyGenericParameters(aGenParSupplier).TypeEquals(b.Parameters(i).ParameterType.SupplyGenericParameters(bGenParSupplier)) Then
                Return False
            End If
        Next
        Return True
    End Function


    ''' <summary>Adds explicit override to method</summary>
    ''' <param name="derivedClassMethod">A method to add override to</param>
    ''' <param name="baseClassMethod">A method which is overriden by <paramref name="derivedClassMethod"/></param>
    ''' <param name="baseClassGenericArguments">
    ''' If <paramref name="baseClassMethod"/>.<see cref="MethodDefinition.DeclaringType">DeclaringType</see> <see cref="TypeDefinition.HasGenericParameters">has generic parameters</see> you can pass actual types for the parameters here.
    ''' Ignored if null, empty or if <paramref name="baseClassMethod"/>.<see cref="MethodDefinition.DeclaringType">DeclaringType</see> does not have generic arguments.
    ''' </param>
    ''' <exception cref="ArgumentNullException"><paramref name="derivedClassMethod"/> is null or <paramref name="baseClassMethod"/> is null.</exception>
    ''' <exception cref="ArgumentException"><paramref name="baseClassMethod"/>.<see cref="MethodDefinition.DeclaringType">DeclaringType</see> <see cref="TypeDefinition.HasGenericParameters">has generic parameters</see>, <paramref name="baseClassGenericArguments"/> is neither null nor empty and number of generic arguments in <paramref name="baseClassGenericArguments"/> and of <paramref name="baseClassMethod"/>.<see cref="MethodDefinition.DeclaringType">DeclaringType</see> differ.</exception>
    <Extension()>
    Public Sub AddOverride(derivedClassMethod As MethodDefinition, baseClassMethod As MethodReference, Optional baseClassGenericArguments As TypeReference() = Nothing)
        If derivedClassMethod Is Nothing Then Throw New ArgumentNullException("derivedClassMethod")
        If baseClassMethod Is Nothing Then Throw New ArgumentNullException("baseClassMethod")

        If baseClassMethod.DeclaringType.HasGenericParameters AndAlso baseClassGenericArguments IsNot Nothing AndAlso baseClassGenericArguments.Length > 0 Then _
                       baseClassMethod = baseClassMethod.MakeGeneric(baseClassGenericArguments)
        derivedClassMethod.Overrides.Add(baseClassMethod)

    End Sub

    ''' <summary>Adds explicit override to method</summary>
    ''' <param name="derivedClassMethod">A method to add override to</param>
    ''' <param name="baseClassMethod">A method which is overriden by <paramref name="derivedClassMethod"/></param>
    ''' <param name="baseClassGenericArgumentsSource">
    ''' If <paramref name="baseClassMethod"/>.<see cref="MethodDefinition.DeclaringType">DeclaringType</see> <see cref="TypeDefinition.HasGenericParameters">has generic parameters</see> this type serves as source of generic parameter types.
    ''' Ignored if null or if <paramref name="baseClassMethod"/>.<see cref="MethodDefinition.DeclaringType">DeclaringType</see> does not have generic arguments.
    ''' </param>
    ''' <exception cref="ArgumentNullException"><paramref name="derivedClassMethod"/> is null or <paramref name="baseClassMethod"/> is null.</exception>
    ''' <exception cref="ArgumentException"><paramref name="baseClassMethod"/>.<see cref="MethodDefinition.DeclaringType">DeclaringType</see> <see cref="TypeDefinition.HasGenericParameters">has generic parameters</see>, <paramref name="baseClassGenericArgumentsSource"/> is not null <paramref name="baseClassGenericArgumentsSource"/>.<see cref="GenericInstanceType.GenericArguments">GenericArguments</see> and of <paramref name="baseClassMethod"/>.<see cref="MethodDefinition.DeclaringType">DeclaringType</see> differ.</exception>
    <Extension()>
    Public Sub AddOverride(derivedClassMethod As MethodDefinition, baseClassMethod As MethodReference, baseClassGenericArgumentsSource As GenericInstanceType)
        derivedClassMethod.AddOverride(baseClassMethod, If(baseClassGenericArgumentsSource Is Nothing, Nothing, baseClassGenericArgumentsSource.GenericArguments.ToArray))
    End Sub
End Module
