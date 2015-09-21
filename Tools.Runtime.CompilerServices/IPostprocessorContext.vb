Namespace RuntimeT.CompilerServicesT
    ''' <remarks>This interface is used when post-processing an assembly using <see cref="T:Tools.RuntimeT.CompilerServicesT.AssemblyPostporcessor"/></remarks>
    ''' <seealso cref="T:Tools.RuntimeT.CompilerServicesT.AssemblyPostporcessor"/>
    ''' <version version="1.5.4">This interface is new in version 1.5.4</version>
    Public Interface IPostprocessorContext
        ''' <summary>Passes an informative message to context. This is used to inform about post-processing operation.</summary>
        ''' <param name="item">Current item being processed. May be null. Should be <see cref="T:Mono.Cecil.ICustomAttributeProvider"/> otherwise it may be treated as null by implementation.</param>
        ''' <param name="message">The message</param>
        Sub LogInfo(item As Object, message As String)
    End Interface
End Namespace