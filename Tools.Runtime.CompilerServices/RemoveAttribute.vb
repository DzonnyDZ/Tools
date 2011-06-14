Imports Tools.RuntimeT.CompilerServicesT
Namespace RuntimeT.CompilerServicesT
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

        Public Sub New()
            Remove = True
        End Sub

        Public Sub New(removeRelatedMetadata As Boolean)
            Me.New()
            _removeRelatedMetadata = removeRelatedMetadata
        End Sub

        Private ReadOnly _removeRelatedMetadata As Boolean
        Public ReadOnly Property RemoveRelatedMetadata As Boolean
            Get
                Return _removeRelatedMetadata
            End Get
        End Property

    End Class
End Namespace