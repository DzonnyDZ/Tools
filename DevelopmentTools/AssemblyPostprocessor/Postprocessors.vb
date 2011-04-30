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
        ''' <remarks>Only items of type <see cref="MethodDefinition"/>, <see cref="FieldDefinition"/> and <see cref="TypeDefinition"/> are processed</remarks>
        Public Sub MakePublic(item As ICustomAttributeProvider, attr As MakePublicAttribute, context As IPostprocessorContext)
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
                context.LogInfo(item, String.Format(My.Resources.wrn_AttributeAppliedOnUnsupportedItem, GetType(MakePublicAttribute).Name, item.GetType.Name))
                Return
            End If
            context.LogInfo(item, String.Format(My.Resources.msg_Transform, originalAttributes, newAttributes))
        End Sub

    End Module
End Namespace