''' <summary>Exception thown when method is called when it is already on call-stack (recursively) and recursion is not allowed.</summary>
Public Class RecursionException : Inherits InvalidOperationException
    ''' <summary>CTor</summary>
    ''' <param name="message">Error message</param>
    ''' <param name="InnerException">Exception that caused this exception to be throw</param>
    Public Sub New(ByVal message$, Optional ByVal InnerException As Exception = Nothing)
        MyBase.New(message, InnerException)
    End Sub
End Class
