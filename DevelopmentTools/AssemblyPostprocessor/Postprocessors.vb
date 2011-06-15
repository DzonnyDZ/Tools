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
        ''' Type inside which <paramref name="item"/> is defined does not implement/inherit type specified in <paramref name="attr"/>.<see cref="ImplementsAttribute.Base">Base</see>. -or-
        ''' <paramref name="item"/> is <see cref="MethodDefinition"/> which is static member. -or-
        ''' <paramref name="item"/> is <see cref="EventDefinition"/> or <see cref="PropertyDefinition"/> with some accessors static.
        ''' </exception>
        ''' <exception cref="NotSupportedException">
        ''' <paramref name="item"/> is neither <see cref="MethodDefinition"/> nor <see cref="PropertyDefinition"/> nor <see cref="EventDefinition"/>. -or-
        ''' <paramref name="item"/> is <see cref="EventDefinition"/> or <see cref="PropertyDefinition"/> and the event or property has some instance and some static accessors or is missing get and set (property) or add and remove (event) accessoers.
        ''' </exception>
        ''' <exception cref="MissingMemberException"><paramref name="item"/> is <see cref="EventDefinition"/> or <see cref="PropertyDefinition"/> and appropriate property to override cannot be found in base class/interface.</exception>
        ''' <exception cref="MissingMethodException"><paramref name="item"/> is <see cref="MethodDefinition"/> and appropriate method cannot be found in base class.</exception>
        ''' <exception cref="Reflection.AmbiguousMatchException">More than one appropriates methods, properties or events found in base class.</exception>
        Public Sub [Implements](item As ICustomAttributeProvider, attr As ImplementsAttribute, context As IPostprocessorContext)
            If item Is Nothing Then Throw New ArgumentNullException("item")
            If attr Is Nothing Then Throw New ArgumentNullException("attr")


            Dim base = attr.Base.ToTypeReference

            If TypeOf item Is IMemberDefinition Then

                Dim member As IMemberDefinition = item

                Dim baseResolved As TypeDefinition = base.Resolve
                If baseResolved.IsInterface AndAlso Not (From iface In member.DeclaringType.Interfaces Where iface.TypeEquals(base)).Any Then
                    Throw New InvalidOperationException(String.Format(My.Resources.ex_TypeDoesNotImplement, member.DeclaringType.FullName, base.FullName))
                ElseIf Not baseResolved.IsInterface AndAlso base.TypeEquals(member.DeclaringType) Then
                    Throw New InvalidOperationException(String.Format(My.Resources.ex_TypeDoesNotInherit, member.DeclaringType.FullName, base.FullName))
                End If


                If TypeOf member Is MethodDefinition Then
                    Dim method As MethodDefinition = member

                    If method.IsStatic Then Throw New InvalidOperationException(String.Format(My.Resources.ex_StaticMethodCannotImplement, method.FullName))


                    Dim candidates = From m In baseResolved.Methods
                                 Where Not m.IsStatic AndAlso m.Name = attr.Member AndAlso (baseResolved.IsInterface OrElse m.IsVirtual) AndAlso
                                       IsSameSignature(method, m, , TryCast(base, GenericInstanceType))
                    If Not candidates.Any Then Throw New MissingMethodException(base.FullName, attr.Member)
                    If candidates.Count > 1 Then Throw New Reflection.AmbiguousMatchException(String.Format(My.Resources.ex_MethodAmbiguous, base.FullName, attr.Member))

                    Dim implementedMethod As MethodReference = New MethodReference(candidates.Single.Name, method.ReturnType, candidates.Single.DeclaringType)
                    method.Overrides.Add(method.Module.Import(implementedMethod))
                    If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources._Implements, method.FullName, implementedMethod.FullName))

                ElseIf TypeOf member Is PropertyDefinition Then
                    Dim propty As PropertyDefinition = member

                    If propty.SetMethod Is Nothing AndAlso propty.GetMethod Is Nothing Then _
                        Throw New NotSupportedException(My.Resources.ex_PropertyWOGetterSetter)
                    If propty.AllMethods.Any(Function(m) m.IsStatic) Then _
                        Throw New InvalidOperationException(My.Resources.ex_PropertyMixedAccessorStaticInstance)
                    If propty.AllMethods.Any(Function(m) Not m.DeclaringType.TypeEquals(propty.DeclaringType)) Then _
                        Throw New NotSupportedException(My.Resources.ex_PropertyExternalAccessor)

                    Dim candidates = From p In baseResolved.Properties
                                     Where p.Name = attr.Member AndAlso p.AllMethods.All(Function(m) Not m.IsStatic AndAlso (m.IsVirtual OrElse baseResolved.IsInterface)) AndAlso
                                           (propty.GetMethod Is Nothing) = (p.GetMethod Is Nothing) AndAlso (propty.SetMethod Is Nothing) = (p.SetMethod Is Nothing) AndAlso
                                           (p.SetMethod IsNot Nothing OrElse p.GetMethod IsNot Nothing) AndAlso p.OtherMethods.Count = propty.OtherMethods.Count AndAlso
                                           (p.GetMethod Is Nothing OrElse IsSameSignature(propty.GetMethod, p.GetMethod, , TryCast(base, GenericInstanceType))) AndAlso
                                           (p.SetMethod Is Nothing OrElse IsSameSignature(propty.SetMethod, p.SetMethod, , TryCast(base, GenericInstanceType))) AndAlso
                                           (propty.OtherMethods.Count = 0 OrElse (
                                               (From propty_om In propty.OtherMethods Join p_om In p.OtherMethods On propty_om.Name Equals p_om.Name).All(
                                                   Function(om) IsSameSignature(om.propty_om, om.p_om, , TryCast(base, GenericInstanceType))
                                               )
                                           ))
                    If Not candidates.Any Then Throw New MissingMemberException(base.FullName, attr.Member)
                    If candidates.Count > 1 Then Throw New Reflection.AmbiguousMatchException(String.Format(My.Resources.ex_PropertyAmbiguous, base.FullName, attr.Member))

                    If propty.GetMethod IsNot Nothing Then
                        Dim implementedMethod As MethodReference = New MethodReference(candidates.Single.GetMethod.Name, propty.GetMethod.ReturnType, candidates.Single.GetMethod.DeclaringType)
                        propty.GetMethod.Overrides.Add(propty.GetMethod.Module.Import(implementedMethod))
                        If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources._Implements, propty.GetMethod.FullName, implementedMethod.FullName))
                    End If
                    If propty.SetMethod IsNot Nothing Then
                        Dim implementedMethod As MethodReference = New MethodReference(candidates.Single.SetMethod.Name, propty.SetMethod.ReturnType, candidates.Single.SetMethod.DeclaringType)
                        propty.SetMethod.Overrides.Add(propty.SetMethod.Module.Import(implementedMethod))
                        If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources._Implements, propty.SetMethod.FullName, implementedMethod.FullName))
                    End If
                    If propty.OtherMethods.Count > 0 Then
                        For Each om In From propty_om In propty.OtherMethods Join cand_om In candidates.Single.OtherMethods On propty_om.Name Equals cand_om.Name
                            Dim implementedmethod As MethodReference = New MethodReference(om.cand_om.Name, om.propty_om.ReturnType, om.cand_om.DeclaringType)
                            om.propty_om.Overrides.Add(om.propty_om.Module.Import(om.cand_om))
                            If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources._Implements, om.propty_om.FullName, implementedmethod.FullName))
                        Next
                    End If
                ElseIf TypeOf member Is EventDefinition Then
                    Dim evt As EventDefinition = member
                   
                    If evt.AddMethod Is Nothing AndAlso evt.RemoveMethod Is Nothing Then _
                                           Throw New NotSupportedException(My.Resources.ex_EventWOAddRemove)
                    If evt.AllMethods.Any(Function(m) m.IsStatic) Then _
                        Throw New InvalidOperationException(My.Resources.ex_EventMixedAccessorStaticInstance)
                    If evt.AllMethods.Any(Function(m) Not m.DeclaringType.TypeEquals(evt.DeclaringType)) Then _
                        Throw New NotSupportedException(My.Resources.ex_EventExternalAccessor)

                    Dim candidates = From e In baseResolved.Events
                                     Where e.Name = attr.Member AndAlso e.AllMethods.All(Function(m) Not m.IsStatic AndAlso (m.IsVirtual OrElse baseResolved.IsInterface)) AndAlso
                                           (evt.AddMethod Is Nothing) = (e.AddMethod Is Nothing) AndAlso (evt.RemoveMethod Is Nothing) = (e.RemoveMethod Is Nothing) AndAlso (evt.InvokeMethod Is Nothing) = (e.InvokeMethod Is Nothing) AndAlso
                                           (e.RemoveMethod IsNot Nothing OrElse e.AddMethod IsNot Nothing) AndAlso e.OtherMethods.Count = evt.OtherMethods.Count AndAlso
                                           (e.AddMethod Is Nothing OrElse IsSameSignature(evt.AddMethod, e.AddMethod, , TryCast(base, GenericInstanceType))) AndAlso
                                           (e.RemoveMethod Is Nothing OrElse IsSameSignature(evt.RemoveMethod, e.RemoveMethod, , TryCast(base, GenericInstanceType))) AndAlso
                                           (e.InvokeMethod Is Nothing OrElse IsSameSignature(evt.InvokeMethod, e.InvokeMethod, , TryCast(base, GenericInstanceType))) AndAlso
                                           (evt.OtherMethods.Count = 0 OrElse (
                                               (From evt_om In evt.OtherMethods Join e_om In e.OtherMethods On evt_om.Name Equals e_om.Name).All(
                                                   Function(om) IsSameSignature(om.evt_om, om.e_om, , TryCast(base, GenericInstanceType))
                                               )
                                           ))
                    If Not candidates.Any Then Throw New MissingMemberException(base.FullName, attr.Member)
                    If candidates.Count > 1 Then Throw New Reflection.AmbiguousMatchException(String.Format(My.Resources.ex_EventAmbiguous, base.FullName, attr.Member))

                    If evt.AddMethod IsNot Nothing Then
                        Dim implementedMethod As MethodReference = New MethodReference(candidates.Single.AddMethod.Name, evt.AddMethod.ReturnType, candidates.Single.AddMethod.DeclaringType)
                        evt.AddMethod.Overrides.Add(evt.AddMethod.Module.Import(implementedMethod))
                        If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources._Implements, evt.AddMethod.FullName, implementedMethod.FullName))
                    End If
                    If evt.RemoveMethod IsNot Nothing Then
                        Dim implementedMethod As MethodReference = New MethodReference(candidates.Single.RemoveMethod.Name, evt.RemoveMethod.ReturnType, candidates.Single.RemoveMethod.DeclaringType)
                        evt.RemoveMethod.Overrides.Add(evt.RemoveMethod.Module.Import(implementedMethod))
                        If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources._Implements, evt.RemoveMethod.FullName, implementedMethod.FullName))
                    End If
                    If evt.InvokeMethod IsNot Nothing Then
                        Dim implementedMethod As MethodReference = New MethodReference(candidates.Single.InvokeMethod.Name, evt.InvokeMethod.ReturnType, candidates.Single.InvokeMethod.DeclaringType)
                        evt.RemoveMethod.Overrides.Add(evt.InvokeMethod.Module.Import(implementedMethod))
                        If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources._Implements, evt.InvokeMethod.FullName, implementedMethod.FullName))
                    End If
                    If evt.OtherMethods.Count > 0 Then
                        For Each om In From evt_om In evt.OtherMethods Join cand_om In candidates.Single.OtherMethods On evt_om.Name Equals cand_om.Name
                            Dim implementedmethod As MethodReference = New MethodReference(om.cand_om.Name, om.evt_om.ReturnType, om.cand_om.DeclaringType)
                            om.evt_om.Overrides.Add(om.evt_om.Module.Import(om.cand_om))
                            If context IsNot Nothing Then context.LogInfo(item, String.Format(My.Resources._Implements, om.evt_om.FullName, implementedmethod.FullName))
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
    End Module
End Namespace