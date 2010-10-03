#If Config <= nightly Then
''' <summary>An exception thrown when there is syntax error in text</summary>
''' <version version="1.5.3" stage="nightly">This class is new in version 1.5.3</version>
<Serializable()>
Public Class SyntaxErrorException
    Inherits Exception
    ''' <summary>CTor - creates a new instance of the <see cref="SyntaxErrorException"/> class</summary>
    Public Sub New()
    End Sub
    ''' <summary>CTor - initializes a new instance of the <see cref="SyntaxErrorException"/> class with error message</summary>
    ''' <param name="message">The message that describes the error.</param>
    Public Sub New(ByVal message$)
        MyBase.New(message)
    End Sub
    ''' <summary>CTor - initializes a new instance of the <see cref="SyntaxErrorException"/> class with error message and inner exception</summary>
    ''' <param name="message">The message that describes the error.</param>
    ''' <param name="innerException">The exception that is the cause of the current exception, or a <see langword="null"/> if no inner exception is specified.</param>
    Public Sub New(ByVal message$, ByVal innerException As Exception)
        MyBase.New(message, innerException)
    End Sub
    ''' <summary>CTor - initializes a new instance of the <see cref="SyntaxErrorException"/> class with error message and location</summary>
    ''' <param name="message">The message that describes the error.</param>
    ''' <param name="row">A 1-based row index of row the error occured at. 0 if not available.</param>
    ''' <param name="column">A 1-based column (char) index the error started at. 0 if not available.</param>
    Public Sub New(ByVal message$, ByVal row%, ByVal column%)
        MyBase.New(String.Format("{0} ({1}, {2})", message, row, column))
    End Sub
    ''' <summary>CTor - initializes a new instance of the <see cref="SyntaxErrorException"/> class with error message, location and inner exception</summary>
    ''' <param name="message">The message that describes the error.</param>
    ''' <param name="innerException">The exception that is the cause of the current exception, or a <see langword="null"/> if no inner exception is specified.</param>
    ''' <param name="row">A 1-based row index of row the error occured at. 0 if not available.</param>
    ''' <param name="column">A 1-based column (char) index the error started at. 0 if not available.</param>
    Public Sub New(ByVal message$, ByVal row%, ByVal column%, ByVal innerException As Exception)
        MyBase.New(String.Format("{0} ({1}, {2})", message, row, column), innerException)
    End Sub

    Private _row%
    Private _column%
    ''' <summary>Gets 1-based row index of row the error occured at</summary>
    ''' <returns>1-based row index of row the error occured at. 0 when this information is not available.</returns>
    Public ReadOnly Property Row%
        Get
            Return _row
        End Get
    End Property
    ''' <summary>Gets 1-based column (char) index of row the error occured at</summary>
    ''' <returns>1-based column (char) index of row the error occured at. 0 when this information is not available.</returns>
    Public ReadOnly Property Column%
        Get
            Return _column
        End Get
    End Property
End Class
#End If