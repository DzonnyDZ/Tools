Imports System.CodeDom.Compiler
Imports System.Linq
Imports Tools.ExtensionsT
#If Config <= Nightly Then 'Stage:Nightly
Namespace CodeDomT.CompilerT
    ''' <summary>An exception thrown when compilation error occurs</summary>
    Public Class CompilerErrorException
        Inherits Exception
        ''' <summary>Generates message for single error</summary>
        ''' <param name="error">An error to generate message for</param>
        ''' <returns>Error message including line generated for <paramref name="error"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="error"/> is null</exception>
        Private Shared Function GenerateMessage([error] As CompilerError) As String
            If [error] Is Nothing Then Throw New ArgumentNullException("error")
            Return String.Format("{0}:{1} {2} {3} {4}", [error].Line, [error].Column, If([error].IsWarning, "Warning", "Error"), [error].ErrorNumber, [error].ErrorText)
        End Function
        ''' <summary>Generates message for multiple errors</summary>
        ''' <param name="errors">Errors to generate message for</param>
        ''' <returns>Exception message containing error messages for all errors</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="errors"/> is null</exception>
        Private Shared Function GenerateMessage(errors As IEnumerable(Of CompilerError)) As String
            If errors Is Nothing Then Throw New ArgumentNullException("errors")
            Dim b As New System.Text.StringBuilder
            For Each item In errors
                b.AppendLine(GenerateMessage(item))
            Next
            Return b.ToString
        End Function

        ''' <summary>CTor - creates a new instance of the <see cref="CompilerErrorException"/> from single error</summary>
        ''' <param name="error">Compilation error</param>
        ''' <param name="innerException">The exception that is the cause of the current exception, or a <see langword="null"/> if no inner exception is specified.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="error"/> isnull</exception>
        Public Sub New([error] As CompilerError, Optional innerException As Exception = Nothing)
            Me.New([error], GenerateMessage([error]), innerException)
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="CompilerErrorException"/> from multiple errors</summary>
        ''' <param name="errors">Compilation errors</param>
        ''' <param name="innerException">The exception that is the cause of the current exception, or a <see langword="null"/> if no inner exception is specified.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="errors"/> isnull</exception>
        Public Sub New(errors As IEnumerable(Of CompilerError), Optional innerException As Exception = Nothing)
            Me.New(errors, GenerateMessage(errors), innerException)
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="CompilerErrorException"/> from <see cref="CompilerErrorCollection"/></summary>
        ''' <param name="errors">Compilation errors</param>
        ''' <param name="innerException">The exception that is the cause of the current exception, or a <see langword="null"/> if no inner exception is specified.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="errors"/> isnull</exception>
        Public Sub New(errors As CompilerErrorCollection, Optional innerException As Exception = Nothing)
            Me.New(errors.ThrowIfNull("errors"), GenerateMessage(errors.OfType(Of CompilerError)), innerException)
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="CompilerErrorException"/> from single error with custom message</summary>
        ''' <param name="error">Compilation error</param>
        ''' <param name="message">The error message that explains the reason for the exception.</param>
        ''' <param name="innerException">The exception that is the cause of the current exception, or a <see langword="null"/> if no inner exception is specified.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="error"/> isnull</exception>
        Public Sub New([error] As CompilerError, message As String, Optional innerException As Exception = Nothing)
            Me.New({[error].ThrowIfNull("error")}, message, innerException)
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="CompilerErrorException"/> from <see cref="CompilerErrorCollection"/> with custom message</summary>
        ''' <param name="errors">Compilation errors</param>
        ''' <param name="message">The error message that explains the reason for the exception.</param>
        ''' <param name="innerException">The exception that is the cause of the current exception, or a <see langword="null"/> if no inner exception is specified.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="errors"/> isnull</exception>
        Public Sub New(errors As CompilerErrorCollection, message As String, Optional innerException As Exception = Nothing)
            Me.New(errors.ThrowIfNull("errors").OfType(Of CompilerError), message, innerException)
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="CompilerErrorException"/> from multiple errors with custom message</summary>
        ''' <param name="errors">Compilation errors</param>
        ''' <param name="message">The error message that explains the reason for the exception.</param>
        ''' <param name="innerException">The exception that is the cause of the current exception, or a <see langword="null"/> if no inner exception is specified.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="errors"/> isnull</exception>
        Public Sub New(errors As IEnumerable(Of CompilerError), message As String, Optional innerException As Exception = Nothing)
            MyBase.New(message, innerException)
            If errors Is Nothing Then Throw New ArgumentNullException("errors")
            _errors = If(TryCast(errors, ICollection(Of CompilerError)), errors.ToArray)
        End Sub
        Private ReadOnly _errors As ICollection(Of CompilerError)
        ''' <summary>Gets compilation erros that caused this exception to be thrown</summary>
        Public ReadOnly Property Errors As ICollection(Of CompilerError)
            Get
                Return _errors
            End Get
        End Property
    End Class
End Namespace
#End If