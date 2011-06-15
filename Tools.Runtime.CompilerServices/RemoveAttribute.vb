Imports Tools.RuntimeT.CompilerServicesT
Namespace RuntimeT.CompilerServicesT

    ''' <summary>When applied on an item indicates that postprocessing tool should remove the item from it's parent</summary>
    ''' <remarks>Applying this attribute on a member causes nothing on itself. You must run supporting post-processsing tool (such as AssemblyPostprocessoer) on your assembly once it's compiled to apply changes denoted by this attributes.</remarks>
    ''' <seealso cref="T:Tools.RuntimeT.CompilerServicesT.AssemblyPostporcessor"/>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <Postprocessor("Tools.RuntimeT.CompilerServicesT.Postprocessors,AssemblyPostprocessor", "Remove")>
    <AttributeUsage(AttributeTargets.Class Or AttributeTargets.Delegate Or AttributeTargets.Enum Or AttributeTargets.Interface Or AttributeTargets.Struct Or
                    AttributeTargets.Method Or AttributeTargets.Constructor Or
                    AttributeTargets.Property Or AttributeTargets.Event Or
                    AttributeTargets.Field Or
                    AttributeTargets.Parameter Or AttributeTargets.GenericParameter Or
                    AttributeTargets.Module,
                    AllowMultiple:=False, Inherited:=False
                    )>
    Public Class RemoveAttribute
        Inherits PostprocessingAttribute

        ''' <summary>CTor - creates a new instance of the <see cref="RemoveAttribute"/> class</summary>
        ''' <remarks>Instance created using default constructor does not indicate removal of dependent items</remarks>
        Public Sub New()
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="RemoveAttribute"/> class indicating if dependent metadata should be deleted as well</summary>
        ''' <param name="removeRelatedMetadata">True to remove related metadata as well, false to keep them</param>
        Public Sub New(removeRelatedMetadata As Boolean)
            Me.New()
            _removeRelatedMetadata = removeRelatedMetadata
        End Sub

        Private ReadOnly _removeRelatedMetadata As Boolean
        ''' <summary>Gets value indicating if related metadata are removed or not</summary>
        ''' <remarks>When true current implementation is:
        ''' <list type="bullet">
        ''' <item>When method is removed from type and it is used as property/event accessor on the same type the accessoer is removed. If after this removal the property has neither setter nor getter the property is removed. If it is an event and it has neither adder nor remover its removed, too.</item>
        ''' <item>When property or event is removed accessor methods are removed as well (as long as theyy were declared in the same type and were marked as specialname).</item>
        ''' </list></remarks>
        Public ReadOnly Property RemoveRelatedMetadata As Boolean
            Get
                Return _removeRelatedMetadata
            End Get
        End Property

    End Class
End Namespace