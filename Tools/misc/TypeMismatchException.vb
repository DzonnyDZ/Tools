Imports Tools.LinqT, System.Linq
#If Config <= Nightly Then
''' <summary>Exception thrown when value of some type is passes where the type is not acceptable</summary>
''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Đonny</author>
''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
''' <version version="1.5.3">Added <see cref="SerializableAttribute"/></version>
''' <version version="1.5.3">Added support for multiple expected types</version>
<Serializable()>
Public Class TypeMismatchException : Inherits ArgumentException
#Region "CTors"
    ''' <summary>CTor</summary>
    ''' <filterpriority>0</filterpriority>
    Public Sub New()
        MyBase.new(CreateMessage)
    End Sub
    ''' <summary>Initializes a new instance of the <see cref="TypeMismatchException" /> class with a specified error message.</summary>  
    ''' <param name="message">The error message that explains the reason for the exception.</param>
    ''' <version version="1.5.4">Parameter <c>Message</c> renamed to <c>message</c></version>
    ''' <filterpriority>1</filterpriority>
    Public Sub New(ByVal message$)
        MyBase.New(Message)
    End Sub
    ''' <summary>Initializes new instance of the <see cref="TypeMismatchException"/> class with message and actual unacceptable value.</summary>
    ''' <param name="message">The error message that explains the reason for the exception.</param>
    ''' <param name="actualValue">Value of argument which caused the exception to be thrown</param>
    ''' <version version="1.5.4">Parameters renamed to camelCase</version>
    ''' <filterpriority>2</filterpriority>
    Public Sub New(ByVal message$, ByVal actualValue As Object)
        MyBase.New(Message)
        Me._ActualValue = ActualValue
    End Sub
    ''' <summary>Initializes a new instance of the <see cref="TypeMismatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
    ''' <param name="message">The error message that explains the reason for the exception.</param>
    ''' <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
    ''' <version version="1.5.4">Parameters renamed to camelCase</version>
    ''' <filterpriority>3</filterpriority>
    Public Sub New(ByVal message$, ByVal innerException As Exception)
        MyBase.New(Message, InnerException)
    End Sub
    ''' <summary>Initializes a new instance of <see cref="TypeMismatchException"/> class with message, name of parameter, actual value, expected type and optionally reference to the inner exception that is cause of thios exception.</summary>
    ''' <param name="paramName">Name of method argument that cased the exception to be thrown - that violated type constraint.</param>
    ''' <param name="actualValue">Value of argument which caused the exception to be thrown</param>
    ''' <param name="expectedType">Type which is acceptable</param>
    ''' <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
    ''' <param name="message">The error message that explains the reason for the exception.</param>
    ''' <version version="1.5.4">Parameters renamed to camelCase</version>
    ''' <filterpriority>4</filterpriority>
    Public Sub New(ByVal paramName$, ByVal actualValue As Object, ByVal expectedType As Type, ByVal message As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(Message, ParamName, InnerException)
        _ActualValue = ActualValue
        _ExpectedTypes = {ExpectedType}
    End Sub
    ''' <summary>Initializes a new instance of <see cref="TypeMismatchException"/> class with name of parameter, actual value, expected type and optionally reference to the inner exception that is cause of thios exception.</summary>
    ''' <param name="ParamName">Name of method argument that cased the exception to be thrown - that violated type constraint.</param>
    ''' <param name="ActualValue">Value of argument which caused the exception to be thrown</param>
    ''' <param name="ExpectedType">Type which is acceptable</param>
    ''' <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
    ''' <version version="1.5.3">Fix: Exception is thrown from <see cref="CreateMessage"/> when neither <paramref name="ActualValue"/> nor <paramref name="ExpectedType"/> is null</version>
    ''' <version version="1.5.4">Generated message now contains full names of types and shows generic parameters</version>
    ''' <version version="1.5.4">Parameters renamed to camelCase</version>
    ''' <filterpriority>5</filterpriority>
    Public Sub New(ByVal paramName$, ByVal actualValue As Object, ByVal expectedType As Type, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(CreateMessage(ActualValue, ExpectedType), ParamName, InnerException)
        _ActualValue = ActualValue
        _ExpectedTypes = {ExpectedType}
    End Sub
    ''' <summary>Initializes a new instance of <see cref="TypeMismatchException"/> class with message, name of parameter, actual value, expected type and optionally reference to the inner exception that is cause of thios exception.</summary>
    ''' <param name="ActualValue">Value of argument which caused the exception to be thrown</param>
    ''' <param name="ExpectedType">Type which is acceptable</param>
    ''' <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
    ''' <param name="Message">The error message that explains the reason for the exception.</param>
    ''' <version version="1.5.4">Parameters renamed to camelCase</version>
    ''' <filterpriority>6</filterpriority>
    Public Sub New(ByVal actualValue As Object, ByVal expectedType As Type, ByVal message As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(Message, InnerException)
        _ActualValue = ActualValue
        _ExpectedTypes = {ExpectedType}
    End Sub
    ''' <summary>Initializes a new instance of <see cref="TypeMismatchException"/> class with name of parameter, actual value, expected type and optionally reference to the inner exception that is cause of thios exception.</summary>
    ''' <param name="ActualValue">Value of argument which caused the exception to be thrown</param>
    ''' <param name="ExpectedType">Type which is acceptable</param>
    ''' <param name="innerException">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
    ''' <version version="1.5.3">Fix: Exception is thrown from <see cref="CreateMessage"/> when neither <paramref name="ActualValue"/> nor <paramref name="ExpectedType"/> is null</version>
    ''' <version version="1.5.4">Generated message now contains full names of types and shows generic parameters</version>
    ''' <version version="1.5.4">Parameters renamed to camelCase</version>
    ''' <filterpriority>7</filterpriority>
    Public Sub New(ByVal actualValue As Object, ByVal expectedType As Type, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(CreateMessage(ActualValue, ExpectedType), InnerException)
        _ActualValue = ActualValue
        _ExpectedTypes = {ExpectedType}
    End Sub
#Region "Multiple"
    ''' <summary>Initializes a new instance of the <see cref="TypeMismatchException"/> class with actual parameter value, name of the parameter, exception message, inner exception and expected types</summary>
    ''' <param name="actualValue">Unacceptable value that caused exception to be thrown. Ignored when null.</param>
    ''' <param name="paramName">Name of parameter <paramref name="actualValue"/> was passed to. Ignored when null.</param>
    ''' <param name="message">Exception message</param>
    ''' <param name="innerException">Exception that caused this exception to be thrown</param>
    ''' <param name="expectedTypes">Types acceptable by <paramref name="paramName"/></param>
    ''' <exception cref="ArgumentNullException"><paramref name="expectedTypes"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="expectedTypes"/> contains null value -or- <paramref name="expectedTypes"/> is empty</exception>
    ''' <version version="1.5.3">This contructor overload is new in 1.5.3</version>
    ''' <filterpriority>8</filterpriority>
    Public Sub New(ByVal actualValue As Object, ByVal paramName As String, ByVal message As String, ByVal innerException As Exception, ByVal ParamArray expectedTypes As Type())
        MyBase.New(message, paramName, innerException)
        If expectedTypes Is Nothing Then Throw New ArgumentNullException("expectedTypes")
        If expectedTypes.Contains(Nothing) Then Throw New ArgumentException("Array cannot contain null value", "expectedTypes")
        If expectedTypes.Length = 0 Then Throw New ArgumentException("Array cannot be empty", "expectedTypes")
        _ExpectedTypes = expectedTypes.Clone
        _ActualValue = actualValue
    End Sub
    ''' <summary>Initializes a new instance of the <see cref="TypeMismatchException"/> class with actual parameter value, name of the parameter, inner exception and expected types</summary>
    ''' <param name="actualValue">Unacceptable value that caused exception to be thrown. Ignored when null.</param>
    ''' <param name="paramName">Name of parameter <paramref name="actualValue"/> was passed to. Ignored when null.</param>
    ''' <param name="innerException">Exception that caused this exception to be thrown</param>
    ''' <param name="expectedTypes">Types acceptable by <paramref name="paramName"/></param>
    ''' <exception cref="ArgumentNullException"><paramref name="expectedTypes"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="expectedTypes"/> contains null value -or- <paramref name="expectedTypes"/> is empty</exception>
    ''' <version version="1.5.3">This contructor overload is new in 1.5.3</version>
    ''' <version version="1.5.4">Generated message now contains full names of types and shows generic parameters</version>
    ''' <filterpriority>9</filterpriority>
    Public Sub New(ByVal actualValue As Object, ByVal paramName As String, ByVal innerException As Exception, ByVal ParamArray expectedTypes As Type())
        Me.New(actualValue, paramName, CreateMessage(actualValue, expectedTypes), innerException, expectedTypes)
    End Sub
    ''' <summary>Initializes a new instance of the <see cref="TypeMismatchException"/> class with actual parameter value, inner exception and expected types</summary>
    ''' <param name="actualValue">Unacceptable value that caused exception to be thrown. Ignored when null.</param>
    ''' <param name="innerException">Exception that caused this exception to be thrown</param>
    ''' <param name="expectedTypes">Acceptable types</param>
    ''' <exception cref="ArgumentNullException"><paramref name="expectedTypes"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="expectedTypes"/> contains null value -or- <paramref name="expectedTypes"/> is empty</exception>
    ''' <version version="1.5.3">This contructor overload is new in 1.5.3</version>
    ''' <filterpriority>10</filterpriority>
    Public Sub New(ByVal actualValue As Object, ByVal innerException As Exception, ByVal ParamArray expectedTypes As Type())
        Me.New(actualValue, Nothing, innerException, expectedTypes)
    End Sub
    ''' <summary>Initializes a new instance of the <see cref="TypeMismatchException"/> class with actual parameter value, name of the parameter and expected types</summary>
    ''' <param name="actualValue">Unacceptable value that caused exception to be thrown. Ignored when null.</param>
    ''' <param name="paramName">Name of parameter <paramref name="actualValue"/> was passed to. Ignored when null.</param>
    ''' <param name="expectedTypes">Types acceptable by <paramref name="paramName"/></param>
    ''' <exception cref="ArgumentNullException"><paramref name="expectedTypes"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="expectedTypes"/> contains null value -or- <paramref name="expectedTypes"/> is empty</exception>
    ''' <version version="1.5.3">This contructor overload is new in 1.5.3</version>
    ''' <filterpriority>11</filterpriority>
    Public Sub New(ByVal actualValue As Object, ByVal paramName As String, ByVal ParamArray expectedTypes As Type())
        Me.New(actualValue, paramName, Nothing, expectedTypes)
    End Sub
    ''' <summary>Initializes a new instance of the <see cref="TypeMismatchException"/> class with actual parameter value and expected types</summary>
    ''' <param name="actualValue">Unacceptable value that caused exception to be thrown. Ignored when null.</param>
    ''' <param name="expectedTypes">Acceptable types</param>
    ''' <exception cref="ArgumentNullException"><paramref name="expectedTypes"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="expectedTypes"/> contains null value -or- <paramref name="expectedTypes"/> is empty</exception>
    ''' <version version="1.5.3">This contructor overload is new in 1.5.3</version>
    ''' <filterpriority>12</filterpriority>
    Public Sub New(ByVal actualValue As Object, ByVal expectedTypes As Type())
        Me.New(actualValue, DirectCast(Nothing, Exception), expectedTypes)
    End Sub
    ''' <summary>Initializes a new instance of the <see cref="TypeMismatchException"/> class with expected types</summary>
    ''' <param name="expectedTypes">Acceptable types</param>
    ''' <exception cref="ArgumentNullException"><paramref name="expectedTypes"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="expectedTypes"/> contains null value -or- <paramref name="expectedTypes"/> is empty</exception>
    ''' <version version="1.5.3">This contructor overload is new in 1.5.3</version>
    ''' <filterpriority>13</filterpriority>
    Public Sub New(ByVal ParamArray expectedTypes As Type())
        Me.New(DirectCast(Nothing, Object), expectedTypes)
    End Sub
    ''' <summary>Initializes a new instance of the <see cref="TypeMismatchException"/> class with exception message, inner exception and expected types</summary>
    ''' <param name="message">Exception message</param>
    ''' <param name="innerException">Exception that caused this exception to be thrown</param>
    ''' <param name="expectedTypes">Acceptable types</param>
    ''' <exception cref="ArgumentNullException"><paramref name="expectedTypes"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="expectedTypes"/> contains null value -or- <paramref name="expectedTypes"/> is empty</exception>
    ''' <version version="1.5.3">This contructor overload is new in 1.5.3</version>
    ''' <filterpriority>14</filterpriority>
    Public Sub New(ByVal message As String, ByVal innerException As Exception, ByVal ParamArray expectedTypes As Type())
        Me.New(Nothing, Nothing, message, innerException, expectedTypes)
    End Sub
#End Region

#End Region
    ''' <summary>Create exception message from actual argument value and expected type</summary>
    ''' <param name="ActualValue">Value of argument which caused the exception to be thrown</param>
    ''' <param name="ExpectedType">Type which is acceptable</param>
    ''' <returns>Exception message</returns>
    ''' <remarks>Either of both arguments can be null and appropriate message will be created</remarks>
    ''' <version version="1.5.3">Fix: Exception is thrown when neither <paramref name="ActualValue"/> nor <paramref name="ExpectedType"/> is null</version>
    ''' <version version="1.5.4">Generated message now contains full names of types and shows generic parameters</version>
    ''' <version version="1.5.4">Parameter <c>ActualValue</c> renamed to <c>actualValue</c>, <c>ExpectedType</c> to <c>expectedType</c></version>
    Protected Shared Function CreateMessage$(Optional ByVal actualValue As Object = Nothing, Optional ByVal expectedType As Type = Nothing)
        If ActualValue Is Nothing AndAlso ExpectedType Is Nothing Then Return ResourcesT.Exceptions.ValueOfSomeTypeWasPassedWhereItIsNotAcceptable
        If ActualValue Is Nothing Then Return String.Format(ResourcesT.Exceptions.OnlyValuesOfType0AreAcceptable, GetNameOfType(ExpectedType))
        If ExpectedType Is Nothing Then Return String.Format(ResourcesT.Exceptions.ValueOfType0IsNotAcceptable, GetNameOfType(ActualValue.GetType))
        Return String.Format(ResourcesT.Exceptions.ValueOfUnexpectedType0Expected1, GetNameOfType(ActualValue.GetType), GetNameOfType(ExpectedType))
    End Function
    ''' <summary>Creates exception message from actual argument value and expected types</summary>
    ''' <param name="actualValue">Value of argument which caused the exception to be thrown</param>
    ''' <param name="expectedTypes">Types that are acceptable</param>
    ''' <remarks>When <paramref name="actualValue"/> is appropriate message is generated. When <paramref name="expectedTypes"/> has only one item appropriate message is generated.</remarks>
    ''' <exception cref="ArgumentNullException"><paramref name="expectedTypes"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="expectedTypes"/> contains null value.</exception>
    ''' <version version="1.5.3">This overload is new in version 1.5.3</version>
    ''' <version version="1.5.4">Generated message now contains full names of types and shows generic parameters</version>
    Protected Shared Function CreateMessage$(ByVal actualValue As Object, ByVal expectedTypes As IEnumerable(Of Type))
        If expectedTypes Is Nothing Then Throw New ArgumentNullException("expectedTypes")
        If expectedTypes.IsEmpty Then Return CreateMessage(actualValue, DirectCast(Nothing, Type))
        Dim b As New System.Text.StringBuilder
        If actualValue Is Nothing Then b.Append(ResourcesT.Exceptions.OnlyValuesOfFollowingTypesAreAcceptable & " ") _
        Else b.AppendFormat(ResourcesT.Exceptions.ValueOfUnexpectedType0ExpectedOneOfFollowingTypes & " ", GetNameOfType(actualValue.GetType))
        Dim i As Integer = 0
        For Each type In expectedTypes
            If type Is Nothing Then Throw New ArgumentException(ResourcesT.Exceptions.CollectionCannotContainNullValue, "expectedTypes")
            If i > 0 Then b.Append(System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ListSeparator & " ")
            b.Append(GetNameOfType(type))
            i += 1
        Next
        If i = 1 Then Return CreateMessage(actualValue, expectedTypes.First)
        Return b.ToString
    End Function

    ''' <summary>Gets human-readable name of type</summary>
    ''' <param name="type">Type to get name of</param>
    ''' <returns>Name of type consisting of full name without assembly qualification and array/pointer/reference designators and types of generic arguments</returns>
    ''' <exception cref="ArgumentNullException"><paramref name="type"/> is null</exception>
    Private Shared Function GetNameOfType(type As Type) As String
        If type Is Nothing Then Throw New ArgumentNullException("type")
        Dim name$
        If type.IsArray Then
            name = GetNameOfType(type.GetElementType) & "[" & New String(",", type.GetArrayRank - 1) & "]"
        ElseIf type.IsPointer Then
            name = GetNameOfType(type.GetElementType) & "*"
        ElseIf type.IsByRef Then
            name = GetNameOfType(type.GetElementType) & "&"
        Else
            name = type.Namespace & If(type.Namespace <> "", ".", "") & type.Name
            If type.IsGenericType Then
                name &= "["
                Dim i = 0
                For Each gpar In type.GetGenericArguments
                    If i > 0 Then name &= ","
                    name &= GetNameOfType(gpar)
                    i += 1
                Next
                name &= "]"
            End If
        End If
        Return name
    End Function

    ''' <summary>Contains value of the <see cref="ActualValue"/> property</summary>
    <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _ActualValue As Object
    ''' <summary>Value that was passed</summary>
    Public ReadOnly Property ActualValue() As Object
        <DebuggerStepThrough()> Get
            Return _ActualValue
        End Get
    End Property
    <EditorBrowsable(EditorBrowsableState.Never)> Private ReadOnly _ExpectedTypes As Type()
    ''' <summary>Gets type of value being expected</summary>
    ''' <remarks>When exception is thrown for more than one expected type contains first of expected types</remarks>
    Public ReadOnly Property ExpectedType() As Type
        <DebuggerStepThrough()> Get
            Return _ExpectedTypes(0)
        End Get
    End Property
    ''' <summary>Gets collection of types value was expected to be of</summary>
    ''' <version version="1.5.3">This property is new in version 1.5.3</version>
    Public ReadOnly Property ExpectedTypes As IReadOnlyCollection(Of Type)
        Get
            Return Array.AsReadOnly(_ExpectedTypes)
        End Get
    End Property


End Class
#End If