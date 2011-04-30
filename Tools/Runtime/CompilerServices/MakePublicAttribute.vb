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

End Namespace