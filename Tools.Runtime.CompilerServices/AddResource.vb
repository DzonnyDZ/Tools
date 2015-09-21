Namespace RuntimeT.CompilerServicesT
    ''' <summary>When applied on an assembly or a module indicates that postprocessing tool should add resource to the assembly or module</summary>
    ''' <remarks>Applying this attribute on a member causes nothing on itself. You must run supporting post-processing tool (such as AssemblyPostprocessoer) on your assembly once it's compiled to apply changes denoted by this attributes.
    ''' <para>When applied on an assembly resources are added to main module of the assembly.</para></remarks>
    ''' <seealso cref="T:Tools.RuntimeT.CompilerServicesT.AssemblyPostporcessor"/>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <Postprocessor("Tools.RuntimeT.CompilerServicesT.Postprocessors,AssemblyPostprocessor", "AddResource")>
    <AttributeUsage(AttributeTargets.Assembly Or AttributeTargets.Module, AllowMultiple:=True, Inherited:=False)>
    Public Class AddResourceAttribute
        Inherits PostprocessingAttribute

        ''' <summary>CTor - creates a new instance of the <see cref="AddResourceAttribute"/> class</summary>
        ''' <param name="name">Name of the resource</param>
        ''' <param name="file">File that contains resource data. For embedded resource data are red from that file and embedded to assembly (so absolute path may be acceptable), for linked resources this file name is recorded to a module.</param>
        ''' <param name="embedded">True to create embedded resource (data are stored in assembly), false to create linked resource (requires additional file to be distributed with assembly).</param>
        ''' <param name="private">True to create private resource, false to create public resource.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="name"/> or <paramref name="file"/> is null.</exception>
        Public Sub New(name$, file$, Optional embedded As Boolean = False, Optional [private] As Boolean = False)
            If name Is Nothing Then Throw New ArgumentNullException(NameOf(name))
            If file Is Nothing Then Throw New ArgumentNullException(NameOf(file))
            Me.Embedded = embedded
            Me.Name = name
            Me.File = file
            Me.Private = [private]
        End Sub

        ''' <summary>Gets value indicating if the resource is embedded (true) or linked (false)</summary>
        Public ReadOnly Property Embedded As Boolean

        ''' <summary>Gets name of the resource</summary>
        Public ReadOnly Property Name$

        ''' <summary>Gets path to file that contains resource data</summary>
        Public ReadOnly Property File$

        ''' <summary>Gets value indicating if the resource is private (true) or public (false)</summary>
        Public ReadOnly Property [Private] As Boolean
    End Class
End Namespace
