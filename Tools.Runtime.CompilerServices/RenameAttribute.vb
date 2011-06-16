Namespace RuntimeT.CompilerServicesT
    ''' <summary>When applied on a type or a member indicates that postprocessing tool should rename the member</summary>
    ''' <remarks>Applying this attribute on a type or a member causes nothing on itself. You must run supporting post-processsing tool (such as AssemblyPostprocessoer) on your assembly once it's compiled to apply changes denoted by this attributes.</remarks>
    ''' <seealso cref="T:Tools.RuntimeT.CompilerServicesT.AssemblyPostporcessor"/>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <AttributeUsage(AttributeTargets.Class Or AttributeTargets.Constructor Or AttributeTargets.Delegate Or AttributeTargets.Enum Or AttributeTargets.Event Or AttributeTargets.Field Or AttributeTargets.GenericParameter Or AttributeTargets.Interface Or AttributeTargets.Method Or AttributeTargets.Parameter Or AttributeTargets.Property Or AttributeTargets.Struct, allowmultiple:=False, inherited:=False)>
    <Postprocessor("Tools.RuntimeT.CompilerServicesT.Postprocessors,AssemblyPostprocessor", "Rename")>
    Public Class RenameAttribute
        Inherits PostprocessingAttribute
        Private ReadOnly _newName$
        Public ReadOnly Property NewName$
            Get
                Return _newName
            End Get
        End Property
        Public Sub New(newName$)
            If newName Is Nothing Then Throw New ArgumentNullException("newName")
            _newName = newName
        End Sub
    End Class

End Namespace