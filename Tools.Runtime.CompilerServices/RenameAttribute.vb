Namespace RuntimeT.CompilerServicesT
    ''' <summary>When applied on a type or a member indicates that postprocessing tool should rename the member</summary>
    ''' <remarks>
    '''     Applying this attribute on a type or a member causes nothing on itself. You must run supporting post-processing tool (such as AssemblyPostprocessoer)
    '''     on your assembly once it's compiled to apply changes denoted by this attributes.
    ''' </remarks>
    ''' <seealso cref="T:Tools.RuntimeT.CompilerServicesT.AssemblyPostporcessor"/>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <AttributeUsage(AttributeTargets.Class Or AttributeTargets.Constructor Or AttributeTargets.Delegate Or AttributeTargets.Enum Or AttributeTargets.Event Or AttributeTargets.Field Or AttributeTargets.GenericParameter Or AttributeTargets.Interface Or AttributeTargets.Method Or AttributeTargets.Parameter Or AttributeTargets.Property Or AttributeTargets.Struct, allowmultiple:=False, inherited:=False)>
    <Postprocessor("Tools.RuntimeT.CompilerServicesT.Postprocessors,AssemblyPostprocessor", "Rename")>
    Public Class RenameAttribute
        Inherits PostprocessingAttribute
        ''' <summary>Gets or sets new name of the member (after renaming)</summary>
        ''' <returns></returns>
        Public ReadOnly Property NewName$

        ''' <summary>CTor - creates a new instance of the <see cref="RenameAttribute"/> class</summary>
        ''' <param name="newName$">New name of the member (after renaming)</param>
        Public Sub New(newName$)
            If newName Is Nothing Then Throw New ArgumentNullException(NameOf(newName))
            Me.NewName = newName
        End Sub
    End Class   
End Namespace