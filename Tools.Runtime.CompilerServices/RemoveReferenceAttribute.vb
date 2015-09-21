Imports Tools.RuntimeT.CompilerServicesT

Namespace RuntimeT.CompilerServicesT

    ''' <summary>When applied module or assembly indicates that postprocessing tool should remove assembly reference from it</summary>
    ''' <remarks>
    '''     Applying this attribute on a member causes nothing on itself. You must run supporting post-processing tool (such as AssemblyPostprocessoer)
    '''     on your assembly once it's compiled to apply changes denoted by this attributes.
    ''' </remarks>
    ''' <seealso cref="T:Tools.RuntimeT.CompilerServicesT.AssemblyPostporcessor"/>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <Postprocessor("Tools.RuntimeT.CompilerServicesT.Postprocessors,AssemblyPostprocessor", "RemoveReference")>
    <AttributeUsage(AttributeTargets.Module Or AttributeTargets.Assembly, AllowMultiple:=True, Inherited:=False)>
    Public Class RemoveReferenceAttribute
        Inherits PostprocessingAttribute

        ''' <summary>Gets name of assembly to remove reference to</summary>
        Public ReadOnly Property AssemblyName$

        ''' <summary>CTor - creates a new instance of the <see cref="RemoveReferenceAttribute"/> class from assembly full name</summary>
        ''' <param name="assemblyFullName">Full name of assembly</param>
        ''' <exception cref="ArgumentNullException"><paramref name="assemblyFullName"/> is null</exception>
        Public Sub New(assemblyFullName$)
            If assemblyFullName Is Nothing Then Throw New ArgumentNullException(assemblyFullName)
            AssemblyName = assemblyFullName
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="RemoveReferenceAttribute"/> class from type from assembly</summary>
        ''' <param name="typeFromAssembly">Any type form assembly to remove reference to</param>
        ''' <exception cref="ArgumentNullException"><paramref name="typeFromAssembly"/> is null</exception>
        Public Sub New(typeFromAssembly As Type)
            Me.New(ThrowIfNull(typeFromAssembly, NameOf(typeFromAssembly)).Assembly)
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="RemoveReferenceAttribute"/> class from assembly</summary>
        ''' <param name="assembly">An assembly to remove reference to</param>
        ''' <exception cref="ArgumentNullException"><paramref name="assembly"/> is null</exception>
        Public Sub New(assembly As Reflection.Assembly)
            Me.New(ThrowIfNull(assembly, NameOf(assembly)).FullName)
        End Sub

        ''' <summary>Checks if object is null and throws <see cref="ArgumentException"/></summary>
        ''' <param name="value">Value to check</param>
        ''' <param name="paramName">Name of parameter to be reported to <see cref="ArgumentNullException.ParamName"/></param>
        ''' <typeparam name="T">Type of value</typeparam>
        ''' <returns><paramref name="value"/> (if it's not null)</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
        Private Shared Function ThrowIfNull(Of T As Class)(value As T, paramName As String) As T
            If value Is Nothing Then Throw New ArgumentNullException(paramName)
            Return value
        End Function
    End Class
End Namespace