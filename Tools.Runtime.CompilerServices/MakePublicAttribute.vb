Namespace RuntimeT.CompilerServicesT

    ''' <summary>When applied on a type or a member indicates that postprocessing tool should change visibility of the member to <c>public</c></summary>
    ''' <remarks>Applying this attribute on a type or a member causes nothing on itself. You must run supporting post-processsing tool (such as AssemblyPostprocessoer) on your assembly once it's compiled to apply changes denoted by this attributes.</remarks>
    ''' <seealso cref="T:Tools.RuntimeT.CompilerServicesT.AssemblyPostporcessor"/>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <AttributeUsage(AttributeTargets.Class Or AttributeTargets.Constructor Or AttributeTargets.Delegate Or AttributeTargets.Enum Or AttributeTargets.Field Or AttributeTargets.Interface Or AttributeTargets.Method Or AttributeTargets.Struct, allowmultiple:=False, inherited:=False)>
    <Postprocessor("Tools.RuntimeT.CompilerServicesT.Postprocessors,AssemblyPostprocessor", "MakePublic")>
    Public NotInheritable Class MakePublicAttribute
        Inherits PostprocessingAttribute
    End Class

    ''' <summary>When applied a module indicates that a type declared in that module will be made <c>public</c>.</summary>
    ''' <remarks>Applying this attribute on a type or a member causes nothing on itself. You must run supporting post-processsing tool (such as AssemblyPostprocessoer) on your assembly once it's compiled to apply changes denoted by this attributes.
    ''' <para>Use this sttribute instead of <see cref="MakePublicAttribute"/> if you don't have access to the type (such as <c>&ltModule></c>). Does not work with nested types.</para></remarks>
    ''' <seealso cref="T:Tools.RuntimeT.CompilerServicesT.AssemblyPostporcessor"/>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <AttributeUsage(AttributeTargets.Module, allowmultiple:=True, inherited:=False)>
    <Postprocessor("Tools.RuntimeT.CompilerServicesT.Postprocessors,AssemblyPostprocessor", "MakeTypePublic")>
    Public NotInheritable Class MakeTypePublicAttribute
        Inherits PostprocessingAttribute
        Private _typeName$
        ''' <summary>CTor - creates a new instance of the <see cref="MakeTypePublicAttribute"/> class</summary>
        ''' <param name="typeName">Full name of type to be made public. Do not include any specifiers that are not part of type</param>
        ''' <exception cref="ArgumentNullException"><paramref name="typeName"/> is null.</exception>
        Public Sub New(typeName$)
            If typeName Is Nothing Then Throw New ArgumentNullException("typeName")
            _typeName = typeName
        End Sub
        ''' <summary>Gets name of type (in current module) to be made public</summary>
        Public ReadOnly Property TypeName$
            Get
                Return _typeName
            End Get
        End Property
    End Class

End Namespace
