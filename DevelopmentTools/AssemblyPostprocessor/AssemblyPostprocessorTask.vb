Imports Microsoft.Build.Framework
Imports Microsoft.Build.Utilities

Namespace RuntimeT.CompilerServicesT
    ''' <summary>Implements MS Build task based on <see cref="AssemblyPostporcessor"/></summary>
    ''' <remarks>To use this task include AssemblyPostprocessor.tasks file to your project</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class AssemblyPostporcessorTask
        Inherits Task
        ''' <summary>Gets or sets path to assembly (e.g. DLL or EXE) to post-process</summary>
        Public Property Assembly$
        ''' <summary>Gets or sets path to SNK file to re-sign the assembly with</summary>
        Public Property Snk$


        ''' <summary>Executes the task.</summary>
        ''' <returns>true if the task successfully executed; otherwise, false.</returns>
        Public Overrides Function Execute() As Boolean

            Dim postProcessor As New AssemblyPostporcessor()
            Dim errors% = 0
            postProcessor.ErrorSink = Function(ex, provider)
                                          errors += 1
                                          Me.BuildEngine3.LogErrorEvent(New BuildErrorEventArgs(Nothing, Nothing, Assembly, 0, 0, 0, 0, ex.Message, ex.HelpLink, GetType(AssemblyPostporcessorTask).Name))
                                          Return True
                                      End Function
            postProcessor.InfoSink = Function(provider, message)
                                         Me.BuildEngine3.LogWarningEvent(New BuildWarningEventArgs(Nothing, Nothing, Assembly, 0, 0, 0, 0, message, Nothing, GetType(AssemblyPostporcessorTask).Name))
                                         Return True
                                     End Function

            postProcessor.PostProcess(Assembly, Snk)

            Return errors = 0
        End Function
    End Class
End Namespace