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


        Public Sub [Implements](item As ICustomAttributeProvider, attr As ImplementsAttribute, context As IPostprocessorContext)
            If item Is Nothing Then Throw New ArgumentNullException("item")
            If attr Is Nothing Then Throw New ArgumentNullException("attr")


            Dim base = attr.Base.ToTypeReference

            If TypeOf item Is IMemberDefinition Then

                Dim member As IMemberDefinition = item

                Dim baseResolved As TypeDefinition = base.Resolve
                If baseResolved.IsInterface AndAlso Not (From iface In member.DeclaringType.Interfaces Where iface.TypeEquals(base)).Any Then
                    Throw New InvalidOperationException(String.Format("{0} does not implement {1}", member.DeclaringType.FullName, base.FullName))
                ElseIf Not baseResolved.IsInterface AndAlso base.TypeEquals(member.DeclaringType) Then
                    Throw New InvalidOperationException(String.Format("{0} does not derive from {1}", member.DeclaringType.FullName, base.FullName))
                End If


                If TypeOf member Is MethodDefinition Then
                    Dim method As MethodDefinition = member

                    If method.IsStatic Then Throw New InvalidOperationException(String.Format("Static method {0} cannot implement base member", method.FullName))


                    Dim candidates = From m In baseResolved.Methods
                                 Where Not m.IsStatic AndAlso m.Name = attr.Member AndAlso (baseResolved.IsInterface OrElse m.IsVirtual) AndAlso m.Parameters.Count = method.Parameters.Count AndAlso
                                       m.ReturnType.SupplyGenericParameters(TryCast(base, GenericInstanceType)).TypeEquals(method.ReturnType) AndAlso
                                       Function(c As MethodDefinition)
                                           For i = 0 To c.Parameters.Count - 1
                                               If Not c.Parameters(i).ParameterType.SupplyGenericParameters(TryCast(base, GenericInstanceType)).TypeEquals(method.Parameters(i).ParameterType) Then
                                                   Return False
                                               End If
                                           Next
                                           Return True
                                       End Function(m)
                    If Not candidates.Any Then Throw New MissingMethodException(base.FullName, attr.Member)
                    If candidates.Count > 1 Then Throw New Reflection.AmbiguousMatchException(String.Format("More than one {0}.{1} method matches criteria", base.FullName, attr.Member))

                    Dim implementedMethod As MethodReference = New MethodReference(candidates.Single.Name, method.ReturnType, candidates.Single.DeclaringType)
                    method.Overrides.Add(method.Module.Import(implementedMethod))

                ElseIf TypeOf member Is PropertyDefinition Then
                    Dim propty As PropertyDefinition = member
                    If propty.GetMethod IsNot Nothing Then
                        [Implements](propty.GetMethod, New ImplementsAttribute(attr.Base, "get_" + attr.Member), context) 'TODO: This assumption may not be always true
                    End If
                    If propty.SetMethod IsNot Nothing Then
                        [Implements](propty.SetMethod, New ImplementsAttribute(attr.Base, "set_" + attr.Member), context) 'TODO: This assumption may not be always true
                    End If
                ElseIf TypeOf member Is PropertyDefinition Then
                    Dim evet As EventDefinition = member
                    If evet.AddMethod IsNot Nothing Then
                        [Implements](evet.AddMethod, New ImplementsAttribute(attr.Base, "add_" + attr.Member), context) 'TODO: This assumption may not be always true
                    End If
                    If evet.RemoveMethod IsNot Nothing Then
                        [Implements](evet.RemoveMethod, New ImplementsAttribute(attr.Base, "remove_" + attr.Member), context) 'TODO: This assumption may not be always true
                    End If
                    If evet.InvokeMethod IsNot Nothing Then
                        [Implements](evet.InvokeMethod, New ImplementsAttribute(attr.Base, "raise_" + attr.Member), context) 'TODO: This assumption may not be always true
                    End If
                ElseIf TypeOf member Is PropertyDefinition Then
                    Throw New NotSupportedException(String.Format("{0} cannot be implemented", item.GetType().Name))
                End If
            Else
                Throw New NotSupportedException(String.Format("{0} cannot be implemented", item.GetType().Name))
            End If

        End Sub

        Public Sub Remove(item As ICustomAttributeProvider, attr As RemoveAttribute, context As IPostprocessorContext)
            If item Is Nothing Then Throw New ArgumentNullException("item")
            If attr Is Nothing Then Throw New ArgumentNullException("attr")

            If TypeOf item Is ModuleDefinition Then
                DirectCast(item, ModuleDefinition).Assembly.Modules.Remove(item)
            ElseIf TypeOf item Is TypeDefinition Then
                If DirectCast(item, TypeDefinition).IsNested Then
                    DirectCast(item, TypeDefinition).DeclaringType.NestedTypes.Remove(item)
                Else
                    DirectCast(item, TypeDefinition).Module.Types.Remove(item)
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

                mtd.DeclaringType.Methods.Remove(mtd)
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
            ElseIf TypeOf item Is ParameterDefinition Then
                DirectCast(item, ParameterDefinition).Method.Parameters.Remove(item)
            ElseIf TypeOf item Is GenericParameter Then
                Dim gpOwner = DirectCast(item, GenericParameter).Owner
                If TypeOf gpOwner Is TypeDefinition Then
                    DirectCast(gpOwner, TypeDefinition).GenericParameters.Remove(item)
                ElseIf TypeOf gpOwner Is MethodDefinition Then
                    DirectCast(gpOwner, MethodDefinition).GenericParameters.Remove(item)
                Else
                    Throw New NotSupportedException(String.Format("Removal of generic parameters of {0} is not supported", gpOwner.GetType().Name))
                End If
            ElseIf TypeOf item Is FieldDefinition Then
                DirectCast(item, FieldDefinition).DeclaringType.Fields.Remove(item)
            Else
                Throw New NotSupportedException(String.Format("Removal of {0} is not supported", item.GetType().Name))
            End If

        End Sub

        Public Sub RemoveReference(item As ICustomAttributeProvider, attr As RemoveReferenceAttribute, context As IPostprocessorContext)

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
                Throw New NotSupportedException(String.Format("{0} supports only assemblies and modules", GetType(RemoveReferenceAttribute)))
            End If

        End Sub
    End Module
End Namespace