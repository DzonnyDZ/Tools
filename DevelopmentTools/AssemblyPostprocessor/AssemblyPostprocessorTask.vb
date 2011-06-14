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

        Dim errors% = 0
        ''' <summary>Executes the task.</summary>
        ''' <returns>true if the task successfully executed; otherwise, false.</returns>
        Public Overrides Function Execute() As Boolean

            Dim postProcessor As New AssemblyPostporcessor()

            postProcessor.MessageReceiver = New TaskMessageReceiver(Me, Assembly)
            postProcessor.SetMessageProcessor(Of TaskMessagePorcessor)()

            postProcessor.PostProcess(Assembly, Snk)

            Return errors = 0
        End Function

        ''' <summary>Implements <see cref="MessageProcessor"/> for MSBuild tasks</summary>
        Public Class TaskMessagePorcessor
            Inherits MessageProcessor

            ''' <summary>Gets or sets instance of <see cref="MessageReceiver"/>-derived class to pass messages to</summary>
            ''' <remarks>Can be null.
            ''' <para>Messages must be passes in a for in which thay can pass application domain boundary.</para></remarks>
            ''' <exception cref="ArgumentException">Value being set is neiter null nor <see cref="TaskMessageReceiver"/></exception>
            Public Overrides Property Receiver As MessageReceiver
                Get
                    Return MyBase.Receiver
                End Get
                Set(value As MessageReceiver)
                    If value IsNot Nothing AndAlso Not TypeOf value Is TaskMessageReceiver Then _
                        Throw New ArgumentException(String.Format(My.Resources.Resources.TaskReceiverTypeError, GetType(TaskMessageReceiver).Name, "value"))
                    MyBase.Receiver = value
                End Set
            End Property

            ''' <summary>Processes an error message</summary>
            ''' <param name="ex">An exception which occured</param>
            ''' <param name="item">Item which caused the exception (may be null)</param>
            ''' <returns>True if processing should continue</returns>
            ''' <remarks>Calls <see cref="Receiver"/>.<see cref="TaskMessageReceiver.OnError"/></remarks>
            Public Overrides Function ProcessError(ex As System.Exception, item As Mono.Cecil.ICustomAttributeProvider) As Boolean
                If Receiver Is Nothing Then Return False
                Return DirectCast(Receiver, TaskMessageReceiver).OnError(ex, If(item Is Nothing, Nothing, item.ToString))
            End Function

            ''' <summary>Processes a info message</summary>
            ''' <param name="item">Item that caused the message (may be null)</param>
            ''' <param name="message">Message</param>
            ''' <remarks>Calls <see cref="Receiver"/>.<see cref="TaskMessageReceiver.OnInfo"/></remarks>
            Public Overrides Sub ProcessInfo(item As Mono.Cecil.ICustomAttributeProvider, message As String)
                If Receiver IsNot Nothing Then
                    DirectCast(Receiver, TaskMessageReceiver).OnInfo(message, If(item Is Nothing, Nothing, item.ToString))
                End If
            End Sub
        End Class

        ''' <summary>Implements <see cref="MessageReceiver"/> for MSBuild tasks</summary>
        Public Class TaskMessageReceiver
            Inherits MessageReceiver
            Private ReadOnly _task As AssemblyPostporcessorTask
            Private ReadOnly _fileName As String
            ''' <summary>Gets MSBuild task this instance was created for</summary>
            Public ReadOnly Property Task As AssemblyPostporcessorTask
                Get
                    Return _task
                End Get
            End Property

            ''' <summary>Gets name of file reported in error messages</summary>
            Public ReadOnly Property FileName() As String
                Get
                    Return _fileName
                End Get
            End Property
            ''' <summary>CTor - creates a new instance of the <see cref="TaskMessageReceiver"/> class</summary>
            ''' <param name="task">A MSBuild task</param>
            ''' <param name="fileName">Name of file to report in errors</param>
            ''' <exception cref="ArgumentNullException"><paramref name="task"/> is null</exception>
            Public Sub New(task As AssemblyPostporcessorTask, fileName$)
                If task Is Nothing Then Throw New ArgumentNullException("task")
                _task = task
                _fileName = fileName
            End Sub

            ''' <summary>Logs error message</summary>
            ''' <param name="ex">An exception which occured</param>
            ''' <param name="item">Name of item exception occured for (ma ybe null)</param>
            ''' <returns>True if processing should continue, false otherwise. This implementation always returns true.</returns>
            Public Overridable Function OnError(ex As Exception, item$) As Boolean
                Task.BuildEngine3.LogErrorEvent(New BuildErrorEventArgs(Nothing, Nothing, FileName, 0, 0, 0, 0, item & ": " & ex.Message, ex.HelpLink, GetType(AssemblyPostporcessorTask).Name))
                Task.errors += 1
                Return (True)
            End Function
            ''' <summary>Losg info message</summary>
            ''' <param name="message">The message</param>
            ''' <param name="item">Name of item the message is generated for (may be null)</param>
            Public Overridable Sub OnInfo(message$, item$)
                Task.BuildEngine3.LogMessageEvent(New BuildMessageEventArgs(item & ": " & message, Nothing, GetType(AssemblyPostporcessorTask).Name, MessageImportance.Low))
            End Sub
        End Class

    End Class
End Namespace