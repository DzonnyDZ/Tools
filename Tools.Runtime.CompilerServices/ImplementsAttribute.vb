Imports Tools.RuntimeT.CompilerServicesT

Namespace RuntimeT.CompilerServicesT
    <Postprocessor("Tools.RuntimeT.CompilerServicesT.Postprocessors,AssemblyPostprocessor", "Implements")>
    <AttributeUsage(AttributeTargets.Method Or AttributeTargets.Event Or AttributeTargets.Property, AllowMultiple:=True, Inherited:=False)>
    Public Class ImplementsAttribute
        Inherits PostprocessingAttribute

        Private ReadOnly _base As Type
        Private ReadOnly _member As String

        Public Sub New(base As Type, member As String)
            _base = base
            _member = member
            Remove = True
        End Sub


        Public ReadOnly Property Base() As Type
            Get
                Return _base
            End Get
        End Property

        Public ReadOnly Property Member() As String
            Get
                Return _member
            End Get
        End Property
    End Class
End Namespace