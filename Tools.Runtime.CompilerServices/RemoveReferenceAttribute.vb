Imports Tools.RuntimeT.CompilerServicesT

Namespace RuntimeT.CompilerServicesT
    <Postprocessor("Tools.RuntimeT.CompilerServicesT.Postprocessors,AssemblyPostprocessor", "RemoveReference")>
    <AttributeUsage(AttributeTargets.Module Or AttributeTargets.Assembly, AllowMultiple:=True, Inherited:=False)>
    Public Class RemoveReferenceAttribute
        Inherits PostprocessingAttribute

        Private ReadOnly _assemblyName$
        Public ReadOnly Property AssemblyName$
            Get
                Return _assemblyName
            End Get
        End Property

        Public Sub New(assemblyFullName$)
            _assemblyName = assemblyFullName
            Remove = True
        End Sub
        Public Sub New(typeFromAssembly As Type)
            Me.new(typeFromAssembly.Assembly)
        End Sub
        Public Sub New(assembly As Reflection.Assembly)
            Me.new(assembly.FullName)
        End Sub
    End Class
End Namespace