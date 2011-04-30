Namespace RuntimeT.CompilerServicesT
    ''' <summary>Defines an interface of context of post-processor</summary>
    ''' <remarks>This interface is used when post-processing an assembly using <see cref="T:Tools.RuntimeT.CompilerServicesT.AssemblyPostporcessor"/></remarks>
    ''' <seealso cref="T:Tools.RuntimeT.CompilerServicesT.AssemblyPostporcessor"/>
    Public Interface IPostprocessorContext
        ''' <summary>Passes an informative messsage to context. This is used to inform about post-processing operation.</summary>
        ''' <param name="item">Current item being processed. May be null. Sould be <see cref="T:Mono.Cecil.ICustomAttributeProvider"/> otherwise it may be treated as null by implementation.</param>
        ''' <param name="message">The message</param>
        Sub LogInfo(item As Object, message As String)
    End Interface
End Namespace