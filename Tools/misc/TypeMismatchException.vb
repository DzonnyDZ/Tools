#If Config <= Nightly Then
''' <summary>Exception thrown when value of some type is passes where the type is not acceptable</summary>
<Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
<Version(1, 0, GetType(TypeMismatchException), LastChange:="05/18/2008")> _
<FirstVersion("05/18/2008")> _
Public Class TypeMismatchException : Inherits ArgumentException
    ''' <summary>CTor</summary>
    Public Sub New()
        MyBase.new(CreateMessage)
    End Sub
    ''' <summary>Initializes a new instance of the <see cref="TypeMismatchException" /> class with a specified error message.</summary>   ''' <param name="message">The error message that explains the reason for the exception.</param>
    Public Sub New(ByVal Message$)
        MyBase.new(Message)
    End Sub
    ''' <summary>Initializes a new instance of the <see cref="TypeMismatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
    ''' <param name="Message">The error message that explains the reason for the exception.</param>
    ''' <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
    Public Sub New(ByVal Message$, ByVal InnerException As Exception)
        MyBase.New(Message, InnerException)
    End Sub
    ''' <summary>Initializes a new instance of <see cref="TypeMismatchException"/> class with message, name of parameter, actual value, expected type and optionally reference to the inner exception that is cause of thios exception.</summary>
    ''' <param name="ParamName">Name of method argument that cased the exception to be thrown - that violated type constraint.</param>
    ''' <param name="ActualValue">Value of argument which caused the exception to be thrown</param>
    ''' <param name="ExpectedType">Type which is acceptable</param>
    ''' <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
    ''' <param name="Message">The error message that explains the reason for the exception.</param>
    Public Sub New(ByVal ParamName$, ByVal ActualValue As Object, ByVal ExpectedType As Type, ByVal Message As String, Optional ByVal InnerException As Exception = Nothing)
        MyBase.new(Message, ParamName, InnerException)
        _ActualValue = ActualValue
        _ExpectedType = ExpectedType
    End Sub
    ''' <summary>Initializes a new instance of <see cref="TypeMismatchException"/> class with name of parameter, actual value, expected type and optionally reference to the inner exception that is cause of thios exception.</summary>
    ''' <param name="ParamName">Name of method argument that cased the exception to be thrown - that violated type constraint.</param>
    ''' <param name="ActualValue">Value of argument which caused the exception to be thrown</param>
    ''' <param name="ExpectedType">Type which is acceptable</param>
    ''' <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
    Public Sub New(ByVal ParamName$, ByVal ActualValue As Object, ByVal ExpectedType As Type, Optional ByVal InnerException As Exception = Nothing)
        MyBase.new(CreateMessage(ActualValue, ExpectedType), ParamName, InnerException)
        _ActualValue = ActualValue
        _ExpectedType = ExpectedType
    End Sub
    ''' <summary>Initializes a new instance of <see cref="TypeMismatchException"/> class with message, name of parameter, actual value, expected type and optionally reference to the inner exception that is cause of thios exception.</summary>
    ''' <param name="ActualValue">Value of argument which caused the exception to be thrown</param>
    ''' <param name="ExpectedType">Type which is acceptable</param>
    ''' <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
    ''' <param name="Message">The error message that explains the reason for the exception.</param>
    Public Sub New(ByVal ActualValue As Object, ByVal ExpectedType As Type, ByVal Message As String, Optional ByVal InnerException As Exception = Nothing)
        MyBase.new(Message, InnerException)
        _ActualValue = ActualValue
        _ExpectedType = ExpectedType
    End Sub
    ''' <summary>Initializes a new instance of <see cref="TypeMismatchException"/> class with name of parameter, actual value, expected type and optionally reference to the inner exception that is cause of thios exception.</summary>
    ''' <param name="ActualValue">Value of argument which caused the exception to be thrown</param>
    ''' <param name="ExpectedType">Type which is acceptable</param>
    ''' <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
    Public Sub New(ByVal ActualValue As Object, ByVal ExpectedType As Type, Optional ByVal InnerException As Exception = Nothing)
        MyBase.new(CreateMessage(ActualValue, ExpectedType), InnerException)
        _ActualValue = ActualValue
        _ExpectedType = ExpectedType
    End Sub
    ''' <summary>Create exception message from actual argument value and expected type</summary>
    ''' <param name="ActualValue">Value of argument which caused the exception to be thrown</param>
    ''' <param name="ExpectedType">Type which is acceptable</param>
    ''' <returns>Exception message</returns>
    ''' <remarks>Either of both arguments can be null and appropriate message will be created</remarks>
    Protected Shared Function CreateMessage$(Optional ByVal ActualValue As Object = Nothing, Optional ByVal ExpectedType As Type = Nothing)
        If ActualValue Is Nothing AndAlso ExpectedType Is Nothing Then Return "Value of some type was passed where it is not acceptable."
        If ActualValue Is Nothing Then Return String.Format("Only values of type {0} are acceptable", ExpectedType.FullName)
        If ExpectedType Is Nothing Then Return String.Format("Value of type {0} is not acceptable.", ActualValue.GetType.FullName)
        Return String.Format("Value of unexpected type {0}. Expected {1}.", ActualValue.GetType.Name, ExpectedType.Name) 'Localize:Message
    End Function

    ''' <summary>Contains value of the <see cref="ActualValue"/> property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _ActualValue As Object
    ''' <summary>Value that was passed</summary>
    Public ReadOnly Property ActualValue() As Object
        <DebuggerStepThrough()> Get
            Return _ActualValue
        End Get
    End Property
    ''' <summary>Contains value of the <see cref="ExpectedType"/> property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _ExpectedType As Type
    ''' <summary>Type of value being expected</summary>
    Public ReadOnly Property ExpectedType() As Type
        <DebuggerStepThrough()> Get
            Return _ExpectedType
        End Get
    End Property


End Class
#End If