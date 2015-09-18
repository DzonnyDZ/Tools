Imports Tools.ExtensionsT
#If True
Namespace ComponentModelT
    ''' <summary>Spacial kind of exception intended to be thown by application when exception occured during some process and it was reported to user by the process itself, so the place exception is caught should not report it to user again.</summary>
    ''' <remarks>Intended use of this exception is as follows: Some processs is going on. Something bad happens in the process (an exception occurs). The exception is reported to user (mesage box, console, log etc.) and it needs to be rethrown but upper catch should be notified that it should not report the error to user again.</remarks>
    ''' <version version="1.5.3" stage="Beta">This class is new in version 1.5.3</version>
    <Serializable()>
    Public Class HandledException
        Inherits ApplicationException
        ''' <summary>CTor - Creates a new instance of the <see cref="HandledException"/> class</summary>
        ''' <param name="innerException"> The exception that is the cause of the current exception.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="innerException"/> is null</exception>
        Public Sub New(ByVal innerException As Exception)
            MyBase.New(innerException.ThrowIfNull("innerException").Message, innerException)
        End Sub
    End Class
End Namespace
#End If