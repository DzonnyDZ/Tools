Imports Mono.Cecil

Namespace RuntimeT.CompilerServicesT

    ''' <summary>Delegate of context-aware post-processing function</summary>
    ''' <typeparam name="T">
    ''' Type of attribute this delegeta is specific for. It can be any type, it can be <see cref="Object"/> if you don't care about specific type.
    ''' Typically it is <see cref="Attribute"/>-derived type or more specifically <see cref="PostprocessingAttribute"/>-derived type.
    ''' </typeparam>
    ''' <param name="item">An item being post-processed</param>
    ''' <param name="attr">The attribute applied on <paramref name="item"/> that triggeŕed post-processing</param>
    ''' <param name="context">Post-processing context (usually <see cref="AssemblyPostporcessor"/>.</param>
    ''' <remarks>This delegate is here only for reference. It's actually not used anywhere. But it provides you with referece how post-processing method for <see cref="PostprocessorAttribute"/> should look like.</remarks>
    Public Delegate Sub PostprocessorWithContext(Of T)(item As ICustomAttributeProvider, attr As T, context As IPostprocessorContext)
    ''' <summary>Delegate of context-unaware post-processing function</summary>
    ''' <typeparam name="T">
    ''' Type of attribute this delegeta is specific for. It can be any type, it can be <see cref="Object"/> if you don't care about specific type.
    ''' Typically it is <see cref="Attribute"/>-derived type or more specifically <see cref="PostprocessingAttribute"/>-derived type.
    ''' </typeparam>
    ''' <param name="item">An item being post-processed</param>
    ''' <param name="attr">The attribute applied on <paramref name="item"/> that triggeŕed post-processing</param>
    ''' <remarks>This delegate is here only for reference. It's actually not used anywhere. But it provides you with referece how post-processing method for <see cref="PostprocessorAttribute"/> should look like.</remarks>
    Public Delegate Sub PostprocessorWithoutContext(Of T)(item As ICustomAttributeProvider, attr As T)

    ''' <summary>Defines built-in post-processors for <see cref="PostprocessingAttribute"/>.derived attributes declared in <see cref="RuntimeT.CompilerServicesT"/></summary>
    ''' <version version="1.5.4">This module is new in version 1.5.4</version>
    Public Module Postprocessors

        ''' <summary>Implements postprocessing defined by <see cref="MakePublicAttribute"/></summary>
        ''' <param name="item">An item to post-process</param>
        ''' <param name="attr">Instance of <see cref="MakePublicAttribute"/> (ignored)</param>
        ''' <param name="context">Postprocessing context</param>
        ''' <exception cref="ArgumentNullException"><paramref name="item"/> is null</exception>
        ''' <remarks>Only items of type <see cref="MethodDefinition"/>, <see cref="FieldDefinition"/> and <see cref="TypeDefinition"/> are processed</remarks>
        Public Sub MakePublic(item As ICustomAttributeProvider, attr As MakePublicAttribute, context As IPostprocessorContext)
            If item Is Nothing Then Throw New ArgumentNullException("item")
            Dim originalAttributes As [Enum], newAttributes As [Enum]
            If TypeOf item Is MethodDefinition Then
                Dim mtd As MethodDefinition = DirectCast(item, MethodDefinition)
                originalAttributes = mtd.Attributes
                mtd.Attributes = (mtd.Attributes And Not MethodAttributes.MemberAccessMask) Or MethodAttributes.Public
                newAttributes = mtd.Attributes
            ElseIf TypeOf item Is FieldDefinition Then
                Dim fld As FieldDefinition = DirectCast(item, FieldDefinition)
                originalAttributes = fld.Attributes
                fld.Attributes = (fld.Attributes And Not FieldAttributes.FieldAccessMask) Or FieldAttributes.Public
                newAttributes = fld.Attributes
            ElseIf TypeOf item Is TypeDefinition Then
                Dim td As TypeDefinition = item
                originalAttributes = td.Attributes
                td.Attributes = (td.Attributes And Not TypeAttributes.VisibilityMask) Or If(td.IsNested, TypeAttributes.NestedPublic, TypeAttributes.Public)
                newAttributes = td.Attributes
            Else
                If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources.wrn_AttributeAppliedOnUnsupportedItem, GetType(MakePublicAttribute).Name, item.GetType.Name))
                Return
            End If
            If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources.msg_Transform, originalAttributes, newAttributes))
        End Sub

        ''' <summary>Implements postprocessing defined by <see cref="MakeTypePublicAttribute"/></summary>
        ''' <param name="item">An item to post-process</param>
        ''' <param name="attr">Instance of <see cref="MakeTypePublicAttribute"/></param>
        ''' <param name="context">Postprocessing context</param>
        ''' <exception cref="ArgumentNullException"><paramref name="item"/> or <paramref name="attr"/> is null</exception>
        Public Sub MakeTypePublic(item As ICustomAttributeProvider, attr As MakeTypePublicAttribute, context As IPostprocessorContext)
            If attr Is Nothing Then Throw New ArgumentNullException("attr")
            If item Is Nothing Then Throw New ArgumentNullException("item")
            If TypeOf item Is ModuleDefinition Then
                Dim mdl As ModuleDefinition = item
                Dim type As TypeDefinition = Nothing
                If mdl.HasTypes Then
                    For Each t In mdl.Types
                        If t.FullName = attr.TypeName Then
                            type = t
                            Exit For
                        End If
                    Next
                End If
                If type Is Nothing Then
                    If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources.wrn_TypeNotFound, attr.TypeName))
                    Return
                End If
                Dim originalAttributes = type.Attributes
                type.Attributes = (type.Attributes And Not TypeAttributes.VisibilityMask) Or If(type.IsNested, TypeAttributes.NestedPublic, TypeAttributes.Public)
                Dim newAttributes = type.Attributes
                If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources.msg_Transform, originalAttributes, newAttributes))
            Else
                If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources.wrn_AttributeAppliedOnUnsupportedItem, GetType(MakeTypePublicAttribute).Name, item.GetType.Name))
            End If
        End Sub

        ''' <summary>Implements postprocessing defined by <see cref="ImplementsAttribute"/></summary>
        ''' <param name="item">An item to post-process</param>
        ''' <param name="attr">Instance of <see cref="ImplementsAttribute"/></param>
        ''' <param name="context">Postprocessing context</param>
        ''' <exception cref="ArgumentNullException"><paramref name="item"/> or <paramref name="attr"/> is null</exception>
        ''' <exception cref="InvalidOperationException">
        '''     Type inside which <paramref name="item"/> is defined does not implement/inherit type specified in <paramref name="attr"/>.<see cref="ImplementsAttribute.Base">Base</see>. -or-
        '''     <paramref name="item"/> is <see cref="MethodDefinition"/> which is static member. -or-
        '''     <paramref name="item"/> is <see cref="EventDefinition"/> or <see cref="PropertyDefinition"/> with some accessors static.
        ''' </exception>
        ''' <exception cref="NotSupportedException">
        '''     <paramref name="item"/> is neither <see cref="MethodDefinition"/> nor <see cref="PropertyDefinition"/> nor <see cref="EventDefinition"/>. -or-
        '''     <paramref name="item"/> is <see cref="EventDefinition"/> or <see cref="PropertyDefinition"/> and the event or property has some instance and some static accessors or is missing get and set (property) or add and remove (event) accessoers. -or-
        '''     <paramref name="item"/> is <see cref="EventDefinition"/> or <see cref="PropertyDefinition"/> and <paramref name="attr"/>.<see cref="ImplementsAttribute.Acceessor">Accessor</see> is not <see cref="Accessor.All"/>.
        ''' </exception>
        ''' <exception cref="MissingMemberException"><paramref name="item"/> is <see cref="EventDefinition"/> or <see cref="PropertyDefinition"/> and appropriate property to override cannot be found in base class/interface.</exception>
        ''' <exception cref="MissingMethodException">
        '''     <paramref name="item"/> is <see cref="MethodDefinition"/> and appropriate method cannot be found in base class -or-
        '''     <paramref name="item"/> is <see cref="PropertyDefinition"/> or <see cref="EventDefinition"/> and a overriding method (accessor) cannot be found.
        ''' </exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">More than one appropriates methods, properties or events found in base class.</exception>
        ''' <version version="1.5.4"><see cref="MissingMethodException"/> can be thrown if <paramref name="item"/> is <see cref="PropertyDefinition"/> or <see cref="EventDefinition"/>.</version>
        ''' <version version="1.5.4">Property or event can implement base property/event with fewer accessors than implementing property/event (as long as matching accessors exist).</version>
        ''' <version version="1.5.4">Accessor matching for other accessors of events and properties is now done by name and signature (previously it was doen only by name).</version>
        ''' <version version="1.5.4">Added support for <see cref="ImplementsAttribute.Acceessor"/>.</version>
        Public Sub [Implements](item As ICustomAttributeProvider, attr As ImplementsAttribute, context As IPostprocessorContext)
            If item Is Nothing Then Throw New ArgumentNullException("item")
            If attr Is Nothing Then Throw New ArgumentNullException("attr")


            If TypeOf item Is IMemberDefinition Then
                Dim member As IMemberDefinition = item
                Dim base = attr.Base.ToTypeReference(member.DeclaringType.Module)

                Dim baseResolved As TypeDefinition = base.Resolve
                If baseResolved.IsInterface AndAlso Not (From iface In member.DeclaringType.Interfaces Where iface.TypeEquals(base)).Any Then
                    Throw New InvalidOperationException(String.Format(My.Resources.ex_TypeDoesNotImplement, member.DeclaringType.FullName, base.FullName))
                ElseIf Not baseResolved.IsInterface AndAlso base.TypeEquals(member.DeclaringType) Then
                    Throw New InvalidOperationException(String.Format(My.Resources.ex_TypeDoesNotInherit, member.DeclaringType.FullName, base.FullName))
                End If


                If TypeOf member Is MethodDefinition Then
                    Dim method As MethodDefinition = member

                    If method.IsStatic Then Throw New InvalidOperationException(String.Format(My.Resources.ex_StaticMethodCannotImplement, method.FullName))


                    Dim candidates As IEnumerable(Of MethodDefinition)
                    Select Case attr.Acceessor
                        Case Accessor.Get
                            candidates = From p In baseResolved.Properties
                                         Where p.GetMethod IsNot Nothing AndAlso Not p.GetMethod.IsStatic AndAlso p.Name = If(attr.Member, member.Name) AndAlso
                                               (baseResolved.IsInterface OrElse p.GetMethod.IsVirtual) AndAlso
                                               IsSameSignature(method, p.GetMethod, , TryCast(base, GenericInstanceType))
                                         Select p.GetMethod
                        Case Accessor.Set
                            candidates = From p In baseResolved.Properties
                                         Where p.SetMethod IsNot Nothing AndAlso Not p.SetMethod.IsStatic AndAlso p.Name = If(attr.Member, member.Name) AndAlso
                                               (baseResolved.IsInterface OrElse p.SetMethod.IsVirtual) AndAlso
                                               IsSameSignature(method, p.SetMethod, , TryCast(base, GenericInstanceType))
                                         Select p.SetMethod
                        Case Accessor.Add
                            candidates = From p In baseResolved.Events
                                         Where p.AddMethod IsNot Nothing AndAlso Not p.AddMethod.IsStatic AndAlso p.Name = If(attr.Member, member.Name) AndAlso
                                               (baseResolved.IsInterface OrElse p.AddMethod.IsVirtual) AndAlso
                                               IsSameSignature(method, p.AddMethod, , TryCast(base, GenericInstanceType))
                                         Select p.AddMethod
                        Case Accessor.Remove
                            candidates = From p In baseResolved.Events
                                         Where p.RemoveMethod IsNot Nothing AndAlso Not p.RemoveMethod.IsStatic AndAlso p.Name = If(attr.Member, member.Name) AndAlso
                                               (baseResolved.IsInterface OrElse p.RemoveMethod.IsVirtual) AndAlso
                                               IsSameSignature(method, p.RemoveMethod, , TryCast(base, GenericInstanceType))
                                         Select p.RemoveMethod
                        Case Accessor.Raise
                            candidates = From p In baseResolved.Events
                                         Where p.InvokeMethod IsNot Nothing AndAlso Not p.InvokeMethod.IsStatic AndAlso p.Name = If(attr.Member, member.Name) AndAlso
                                               (baseResolved.IsInterface OrElse p.InvokeMethod.IsVirtual) AndAlso
                                               IsSameSignature(method, p.InvokeMethod, , TryCast(base, GenericInstanceType))
                                         Select p.InvokeMethod
                        Case Else
                            candidates = From m In baseResolved.Methods
                                         Where Not m.IsStatic AndAlso m.Name = If(attr.Member, member.Name) AndAlso (baseResolved.IsInterface OrElse m.IsVirtual) AndAlso
                                               IsSameSignature(method, m, , TryCast(base, GenericInstanceType))
                    End Select
                    
                    If Not candidates.Any Then Throw New MissingMethodException(base.FullName, If(attr.Member, member.Name))
                    If candidates.Count > 1 Then Throw New Reflection.AmbiguousMatchException(String.Format(My.Resources.ex_MethodAmbiguous, base.FullName, If(attr.Member, member.Name)))

                    method.AddOverride(method.Module.Import(candidates.First), TryCast(base, GenericInstanceType))

                    If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources._Implements, method.FullName, candidates.First.FullName))
                ElseIf TypeOf member Is PropertyDefinition Then
                    If attr.Acceessor <> Accessor.All AndAlso attr.Acceessor <> Accessor.Standard Then Throw New NotSupportedException("For properties, accessor must be All or Standard")
                    Dim propty As PropertyDefinition = member

                    If propty.SetMethod Is Nothing AndAlso propty.GetMethod Is Nothing Then _
                        Throw New NotSupportedException(My.Resources.ex_PropertyWOGetterSetter)
                    If propty.AllMethods.Any(Function(m) m.IsStatic) Then _
                        Throw New InvalidOperationException(My.Resources.ex_PropertyMixedAccessorStaticInstance)
                    If propty.AllMethods.Any(Function(m) Not m.DeclaringType.TypeEquals(propty.DeclaringType)) Then _
                        Throw New NotSupportedException(My.Resources.ex_PropertyExternalAccessor)

                    Dim candidates = From p In baseResolved.Properties
                                     Where p.Name = If(attr.Member, member.Name) AndAlso
                                           p.AllMethods.All(Function(m) Not m.IsStatic AndAlso (m.IsVirtual OrElse baseResolved.IsInterface)) AndAlso
                                           (p.SetMethod IsNot Nothing OrElse p.GetMethod IsNot Nothing) AndAlso
                                           (p.GetMethod Is Nothing OrElse IsSameSignature(propty.GetMethod, p.GetMethod, , TryCast(base, GenericInstanceType))) AndAlso
                                           (p.SetMethod Is Nothing OrElse IsSameSignature(propty.SetMethod, p.SetMethod, , TryCast(base, GenericInstanceType)))
                    If Not candidates.Any Then Throw New MissingMemberException(base.FullName, If(attr.Member, member.Name))
                    If candidates.Count > 1 Then Throw New Reflection.AmbiguousMatchException(String.Format(My.Resources.ex_PropertyAmbiguous, base.FullName, If(attr.Member, member.Name)))

                    Dim candidate As PropertyDefinition = candidates.Single
                    If candidate.GetMethod IsNot Nothing AndAlso Not candidate.GetMethod.IsStatic AndAlso (baseResolved.IsInterface OrElse candidate.GetMethod.IsVirtual) Then
                        If propty.GetMethod Is Nothing Then Throw New MissingMethodException(base.FullName, "get_" + If(attr.Member, member.Name))
                        Dim implementedMethod As MethodReference = New MethodReference(candidate.GetMethod.Name, propty.GetMethod.ReturnType, candidate.GetMethod.DeclaringType)
                        propty.GetMethod.AddOverride(propty.GetMethod.Module.Import(candidates.First.GetMethod), TryCast(base, GenericInstanceType))
                        If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources._Implements, propty.GetMethod.FullName, implementedMethod.FullName))
                    End If
                    If candidate.SetMethod IsNot Nothing AndAlso Not candidate.SetMethod.IsStatic AndAlso (baseResolved.IsInterface OrElse candidate.SetMethod.IsVirtual) Then
                        If propty.SetMethod Is Nothing Then Throw New MissingMethodException(base.FullName, "set_" + If(attr.Member, member.Name))
                        Dim implementedMethod As MethodReference = New MethodReference(candidate.SetMethod.Name, propty.SetMethod.ReturnType, candidate.SetMethod.DeclaringType)
                        propty.SetMethod.AddOverride(propty.SetMethod.Module.Import(candidates.First.SetMethod), TryCast(base, GenericInstanceType))
                        If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources._Implements, propty.SetMethod.FullName, implementedMethod.FullName))
                    End If
                    If propty.OtherMethods.Count > 0 AndAlso attr.Acceessor = Accessor.All Then
                        For Each cand_om In candidate.OtherMethods
                            If cand_om.IsStatic OrElse (Not cand_om.IsVirtual AndAlso Not baseResolved.IsInterface) Then Continue For
                            Dim overridingMethodFound As MethodDefinition = Nothing
                            For Each propty_om In propty.OtherMethods
                                If cand_om.Name = propty_om.Name AndAlso IsSameSignature(cand_om, propty_om) Then
                                    overridingMethodFound = cand_om : Exit For
                                End If
                            Next
                            If overridingMethodFound Is Nothing Then Throw New MissingMethodException(member.DeclaringType.FullName, cand_om.Name)
                        Next
                    End If
                ElseIf TypeOf member Is EventDefinition Then
                    If attr.Acceessor <> Accessor.All AndAlso attr.Acceessor <> Accessor.Standard Then Throw New NotSupportedException("For events, accessor must be All or Standard")
                    Dim evt As EventDefinition = member

                    If evt.AddMethod Is Nothing AndAlso evt.RemoveMethod Is Nothing Then _
                                           Throw New NotSupportedException(My.Resources.ex_EventWOAddRemove)
                    If evt.AllMethods.Any(Function(m) m.IsStatic) Then _
                        Throw New InvalidOperationException(My.Resources.ex_EventMixedAccessorStaticInstance)
                    If evt.AllMethods.Any(Function(m) Not m.DeclaringType.TypeEquals(evt.DeclaringType)) Then _
                        Throw New NotSupportedException(My.Resources.ex_EventExternalAccessor)

                    Dim candidates = From e In baseResolved.Events
                                     Where e.Name = If(attr.Member, member.Name) AndAlso
                                           e.AllMethods.All(Function(m) Not m.IsStatic AndAlso (m.IsVirtual OrElse baseResolved.IsInterface)) AndAlso
                                           (e.RemoveMethod IsNot Nothing OrElse e.AddMethod IsNot Nothing) AndAlso
                                           (e.AddMethod Is Nothing OrElse IsSameSignature(evt.AddMethod, e.AddMethod, , TryCast(base, GenericInstanceType))) AndAlso
                                           (e.RemoveMethod Is Nothing OrElse IsSameSignature(evt.RemoveMethod, e.RemoveMethod, , TryCast(base, GenericInstanceType))) AndAlso
                                           (e.InvokeMethod Is Nothing OrElse IsSameSignature(evt.InvokeMethod, e.InvokeMethod, , TryCast(base, GenericInstanceType)))

                    If Not candidates.Any Then Throw New MissingMemberException(base.FullName, If(attr.Member, member.Name))
                    If candidates.Count > 1 Then Throw New Reflection.AmbiguousMatchException(String.Format(My.Resources.ex_EventAmbiguous, base.FullName, If(attr.Member, member.Name)))

                    Dim candidate As EventDefinition = candidates.Single

                    If candidate.AddMethod IsNot Nothing AndAlso Not candidate.AddMethod.IsStatic AndAlso (baseResolved.IsInterface OrElse candidate.AddMethod.IsVirtual) Then
                        If evt.AddMethod Is Nothing Then Throw New MissingMethodException(base.FullName, "add_" + If(attr.Member, member.Name))
                        Dim implementedMethod As MethodReference = New MethodReference(candidate.AddMethod.Name, evt.AddMethod.ReturnType, candidate.AddMethod.DeclaringType)
                        evt.AddMethod.AddOverride(evt.AddMethod.Module.Import(candidates.First.AddMethod), TryCast(base, GenericInstanceType))
                        If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources._Implements, evt.AddMethod.FullName, implementedMethod.FullName))
                    End If
                    If candidate.RemoveMethod IsNot Nothing AndAlso Not candidate.RemoveMethod.IsStatic AndAlso (baseResolved.IsInterface OrElse candidate.RemoveMethod.IsVirtual) Then
                        If evt.RemoveMethod Is Nothing Then Throw New MissingMethodException(base.FullName, "remove_" + If(attr.Member, member.Name))
                        Dim implementedMethod As MethodReference = New MethodReference(candidate.RemoveMethod.Name, evt.RemoveMethod.ReturnType, candidate.RemoveMethod.DeclaringType)
                        evt.RemoveMethod.AddOverride(evt.RemoveMethod.Module.Import(candidates.First.RemoveMethod), TryCast(base, GenericInstanceType))
                        If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources._Implements, evt.RemoveMethod.FullName, implementedMethod.FullName))
                    End If
                    If candidate.InvokeMethod IsNot Nothing AndAlso Not candidate.InvokeMethod.IsStatic AndAlso (baseResolved.IsInterface OrElse candidate.InvokeMethod.IsVirtual) Then
                        If evt.InvokeMethod Is Nothing Then Throw New MissingMethodException(base.FullName, "invoke_" + If(attr.Member, member.Name))
                        Dim implementedMethod As MethodReference = New MethodReference(candidate.InvokeMethod.Name, evt.InvokeMethod.ReturnType, candidate.InvokeMethod.DeclaringType)
                        evt.InvokeMethod.AddOverride(evt.InvokeMethod.Module.Import(candidates.First.InvokeMethod), TryCast(base, GenericInstanceType))
                        If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources._Implements, evt.InvokeMethod.FullName, implementedMethod.FullName))
                    End If
                    If evt.OtherMethods.Count > 0 AndAlso attr.Acceessor = Accessor.All Then
                        For Each cand_om In candidate.OtherMethods
                            If cand_om.IsStatic OrElse (Not cand_om.IsVirtual AndAlso Not baseResolved.IsInterface) Then Continue For
                            Dim overridingMethodFound As MethodDefinition = Nothing
                            For Each evt_om In evt.OtherMethods
                                If cand_om.Name = evt_om.Name AndAlso IsSameSignature(cand_om, evt_om) Then
                                    overridingMethodFound = cand_om : Exit For
                                End If
                            Next
                            If overridingMethodFound Is Nothing Then Throw New MissingMethodException(member.DeclaringType.FullName, cand_om.Name)
                        Next
                    End If
                Else
                    Throw New NotSupportedException(String.Format(My.Resources.ex_CannotImplement, item.GetType().Name))
                End If
            Else
                Throw New NotSupportedException(String.Format(My.Resources.ex_CannotImplement, item.GetType().Name))
            End If

        End Sub

        ''' <summary>Implements postprocessing defined by <see cref=" RemoveAttribute"/></summary>
        ''' <param name="item">An item to post-process</param>
        ''' <param name="attr">Instance of <see cref="RemoveAttribute"/></param>
        ''' <param name="context">Postprocessing context</param>
        ''' <exception cref="ArgumentNullException"><paramref name="item"/> or <paramref name="attr"/> is null</exception>
        ''' <exception cref="NotSupportedException">
        ''' <paramref name="item"/> is of unsupported type -or-
        ''' <paramref name="item"/> is <see cref="GenericParameter"/> which is defined neither on type nor on method. -or-
        ''' <paramref name="item"/> is <see cref="ParameterDefinition"/> which <see cref="ParameterDefinition.IsReturnValue">is return value</see>.
        ''' </exception>
        Public Sub Remove(item As ICustomAttributeProvider, attr As RemoveAttribute, context As IPostprocessorContext)
            If item Is Nothing Then Throw New ArgumentNullException("item")
            If attr Is Nothing Then Throw New ArgumentNullException("attr")

            If TypeOf item Is ModuleDefinition Then
                DirectCast(item, ModuleDefinition).Assembly.Modules.Remove(item)
                If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources.Removed, item, DirectCast(item, ModuleDefinition).Assembly))
            ElseIf TypeOf item Is TypeDefinition Then
                If DirectCast(item, TypeDefinition).IsNested Then
                    DirectCast(item, TypeDefinition).DeclaringType.NestedTypes.Remove(item)
                    If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources.Removed, item, DirectCast(item, TypeDefinition).DeclaringType))
                Else
                    DirectCast(item, TypeDefinition).Module.Types.Remove(item)
                    If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources.Removed, item, DirectCast(item, TypeDefinition).Module))
                End If
            ElseIf TypeOf item Is MethodDefinition Then
                Dim mtd As MethodDefinition = DirectCast(item, MethodDefinition)
                For Each prp In (From p In mtd.DeclaringType.Properties Where p.GetMethod Is mtd).ToArray
                    prp.GetMethod = Nothing
                    If prp.GetMethod Is Nothing AndAlso prp.SetMethod Is Nothing Then Remove(prp, attr, context)
                Next
                For Each prp In (From p In mtd.DeclaringType.Properties Where p.SetMethod Is mtd).ToArray
                    prp.SetMethod = Nothing
                    If prp.GetMethod Is Nothing AndAlso prp.SetMethod Is Nothing Then Remove(prp, attr, context)
                Next
                For Each prp In (From p In mtd.DeclaringType.Properties Where p.OtherMethods.Contains(mtd)).ToArray
                    prp.OtherMethods.Remove(mtd)
                Next

                For Each evt In (From p In mtd.DeclaringType.Events Where p.AddMethod Is mtd).ToArray
                    evt.AddMethod = Nothing
                    If evt.AddMethod Is Nothing AndAlso evt.RemoveMethod Is Nothing Then Remove(evt, attr, context)
                Next
                For Each evt In (From p In mtd.DeclaringType.Events Where p.RemoveMethod Is mtd).ToArray
                    evt.RemoveMethod = Nothing
                    If evt.AddMethod Is Nothing AndAlso evt.RemoveMethod Is Nothing Then Remove(evt, attr, context)
                Next
                For Each evt In (From p In mtd.DeclaringType.Events Where p.InvokeMethod Is mtd).ToArray
                    evt.InvokeMethod = Nothing
                Next
                For Each evt In (From p In mtd.DeclaringType.Events Where p.OtherMethods.Contains(mtd)).ToArray
                    evt.OtherMethods.Remove(mtd)
                Next

                mtd.DeclaringType.Methods.Remove(mtd)
                If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources.Removed, item, mtd.DeclaringType))
            ElseIf TypeOf item Is PropertyDefinition Then
                Dim prp As PropertyDefinition = DirectCast(item, PropertyDefinition)
                If attr.RemoveRelatedMetadata Then
                    If prp.GetMethod IsNot Nothing AndAlso prp.GetMethod.DeclaringType Is prp.DeclaringType AndAlso prp.GetMethod.IsSpecialName Then
                        prp.GetMethod.DeclaringType.Methods.Remove(prp.GetMethod)
                    End If
                    If prp.SetMethod IsNot Nothing AndAlso prp.SetMethod.DeclaringType Is prp.DeclaringType AndAlso prp.SetMethod.IsSpecialName Then
                        prp.SetMethod.DeclaringType.Methods.Remove(prp.SetMethod)
                    End If
                    If prp.HasOtherMethods Then
                        For Each om In (From m In prp.OtherMethods Where m.DeclaringType Is prp.DeclaringType AndAlso m.IsSpecialName).ToArray
                            om.DeclaringType.Methods.Remove(om)
                        Next
                    End If
                End If
                prp.DeclaringType.Properties.Remove(item)
                If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources.Removed, item, prp.DeclaringType))
            ElseIf TypeOf item Is EventDefinition Then
                Dim evt As EventDefinition = DirectCast(item, EventDefinition)

                If attr.RemoveRelatedMetadata Then
                    If evt.AddMethod IsNot Nothing AndAlso evt.AddMethod.DeclaringType Is evt.DeclaringType AndAlso evt.AddMethod.IsSpecialName Then
                        evt.AddMethod.DeclaringType.Methods.Remove(evt.AddMethod)
                    End If
                    If evt.RemoveMethod IsNot Nothing AndAlso evt.RemoveMethod.DeclaringType Is evt.DeclaringType AndAlso evt.RemoveMethod.IsSpecialName Then
                        evt.RemoveMethod.DeclaringType.Methods.Remove(evt.RemoveMethod)
                    End If
                    If evt.InvokeMethod IsNot Nothing AndAlso evt.InvokeMethod.DeclaringType Is evt.DeclaringType AndAlso evt.InvokeMethod.IsSpecialName Then
                        evt.InvokeMethod.DeclaringType.Methods.Remove(evt.InvokeMethod)
                    End If
                    If evt.HasOtherMethods Then
                        For Each om In (From m In evt.OtherMethods Where m.DeclaringType Is evt.DeclaringType AndAlso m.IsSpecialName).ToArray
                            om.DeclaringType.Methods.Remove(om)
                        Next
                    End If
                End If

                evt.DeclaringType.Events.Remove(item)
                If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources.Removed, item, evt.DeclaringType))
            ElseIf TypeOf item Is ParameterDefinition Then
                If DirectCast(item, ParameterDefinition).IsReturnValue Then Throw New NotSupportedException(My.Resources.ex_RemoveReturnValue)
                DirectCast(item, ParameterDefinition).Method.Parameters.Remove(item)
                If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources.Removed, item, DirectCast(item, ParameterDefinition).Method))
            ElseIf TypeOf item Is GenericParameter Then
                Dim gpOwner = DirectCast(item, GenericParameter).Owner
                If TypeOf gpOwner Is TypeDefinition Then
                    DirectCast(gpOwner, TypeDefinition).GenericParameters.Remove(item)
                ElseIf TypeOf gpOwner Is MethodDefinition Then
                    DirectCast(gpOwner, MethodDefinition).GenericParameters.Remove(item)
                Else
                    Throw New NotSupportedException(String.Format(My.Resources.ex_UnsupportedGParRemoval, gpOwner.GetType().Name))
                End If
                If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources.Removed, item, DirectCast(item, GenericParameter).Owner))
            ElseIf TypeOf item Is FieldDefinition Then
                DirectCast(item, FieldDefinition).DeclaringType.Fields.Remove(item)
            Else
                Throw New NotSupportedException(String.Format(My.Resources.ex_RemovalNotSupported, item.GetType().Name))
            End If

        End Sub

        ''' <summary>Implements postprocessing defined by <see cref="RemoveReferenceAttribute"/></summary>
        ''' <param name="item">An item (asssembly or module) to post-process</param>
        ''' <param name="attr">Instance of <see cref="RemoveReferenceAttribute"/> (ignored)</param>
        ''' <param name="context">Postprocessing context</param>
        ''' <exception cref="ArgumentNullException"><paramref name="item"/> is null</exception>
        ''' <exception cref="NotSupportedException"><paramref name="item"/> is neither <see cref="ModuleDefinition"/> nor <see cref="AssemblyDefinition"/>.</exception>
        Public Sub RemoveReference(item As ICustomAttributeProvider, attr As RemoveReferenceAttribute, context As IPostprocessorContext)
            If item Is Nothing Then Throw New ArgumentNullException("item")

            If TypeOf item Is ModuleDefinition Then
                Dim m As ModuleDefinition = item
                For Each ar In m.AssemblyReferences
                    If ar.FullName = attr.AssemblyName Then
                        If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources.msg_RemoveReference, ar, m))
                        m.AssemblyReferences.Remove(ar)
                        Return
                    End If
                Next
            ElseIf TypeOf item Is AssemblyDefinition Then
                For Each [module] In DirectCast(item, AssemblyDefinition).Modules
                    RemoveReference([module], attr, context)
                Next
            Else
                Throw New NotSupportedException(String.Format(My.Resources.ex_OnylAssemblyAndModule, GetType(RemoveReferenceAttribute)))
            End If

        End Sub

        ''' <summary>Implements postprocessing defined by <see cref="RenameAttribute"/></summary>
        ''' <param name="item">An item to be renamed</param>
        ''' <param name="attr">Instance of <see cref="RenameAttribute"/></param>
        ''' <param name="context">Postprocessing context</param>
        ''' <exception cref="ArgumentNullException"><paramref name="item"/> or <paramref name="attr"/> is null</exception>
        ''' <exception cref="NotSupportedException"><paramref name="item"/> is not of supported type</exception>
        Public Sub Rename(item As ICustomAttributeProvider, attr As RenameAttribute, context As IPostprocessorContext)
            If item Is Nothing Then Throw New ArgumentNullException("item")
            If attr Is Nothing Then Throw New ArgumentNullException("attr")

            If TypeOf item Is TypeDefinition Then
                DirectCast(item, TypeDefinition).Name = attr.NewName
            ElseIf TypeOf item Is MethodDefinition Then
                DirectCast(item, MethodDefinition).Name = attr.NewName
            ElseIf TypeOf item Is PropertyDefinition Then
                DirectCast(item, PropertyDefinition).Name = attr.NewName
            ElseIf TypeOf item Is EventDefinition Then
                DirectCast(item, EventDefinition).Name = attr.NewName
            ElseIf TypeOf item Is FieldDefinition Then
                DirectCast(item, FieldDefinition).Name = attr.NewName
            ElseIf TypeOf item Is ParameterDefinition Then
                DirectCast(item, ParameterDefinition).Name = attr.NewName
            ElseIf TypeOf item Is GenericParameter Then
                DirectCast(item, GenericParameter).Name = attr.NewName
            Else
                Throw New NotSupportedException(String.Format(My.Resources.ex_AttrIsNotSupportedOnItem, GetType(RenameAttribute).Name, item.GetType.Name))
            End If

            If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources.msg_Rename, item, attr.NewName))
        End Sub

        ''' <summary>Implements postprocessing defined by <see cref="AddResourceAttribute"/></summary>
        ''' <param name="item">An item to postprocess</param>
        ''' <param name="attr">A <see cref="AddResourceAttribute"/></param>
        ''' <param name="context">Postprocessing context</param>
        ''' <exception cref="ArgumentNullException"><paramref name="item"/> or <paramref name="attr"/> is null</exception>
        Public Sub AddResource(item As ICustomAttributeProvider, attr As AddResourceAttribute, context As IPostprocessorContext)
            If item Is Nothing Then Throw New ArgumentNullException("item")
            If attr Is Nothing Then Throw New ArgumentNullException("attr")
            If TypeOf item Is AssemblyDefinition Then
                AddResource(DirectCast(item, AssemblyDefinition).MainModule, attr, context)
            ElseIf TypeOf item Is ModuleDefinition Then
                Dim [module] As ModuleDefinition = DirectCast(item, ModuleDefinition)
                [module].Resources.Add(If(attr.Embedded,
                                          DirectCast(
                                              New EmbeddedResource(attr.Name, If(attr.Private, ManifestResourceAttributes.Private, ManifestResourceAttributes.Public),
                                                                   IO.File.ReadAllBytes(If(IO.Path.IsPathRooted(attr.File), attr.File,
                                                                                           IO.Path.Combine(IO.Path.GetDirectoryName([module].FullyQualifiedName), attr.File)
                                                                  ))), 
                                              Resource),
                                          DirectCast(
                                              New LinkedResource(attr.Name, If(attr.Private, ManifestResourceAttributes.Private, ManifestResourceAttributes.Public), attr.File), 
                                              Resource)
                                      ))
                If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources.msg_AddedResource, If(attr.Embedded, My.Resources.resource_embeded, My.Resources.resource_linked), attr.Name, attr.File))
            Else
                Throw New NotSupportedException(String.Format(My.Resources.ex_OnylAssemblyAndModule, GetType(AddResourceAttribute).Name))
            End If
        End Sub
    End Module
End Namespace