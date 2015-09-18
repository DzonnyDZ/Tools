Imports Tools.LinqT, System.Linq
#If True
Namespace ComponentModelT
    ''' <summary>Exception throw when multiple exceptions occured</summary>
    ''' <version version="1.5.2">Class introduced</version>
    Public Class MultipleException
        Inherits Exception : Implements IEnumerable(Of Exception)
        ''' <summary>CTor</summary>
        ''' <param name="Exceptions">Exceptions that have occured</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Exceptions"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Exceptions"/> is empty or contains only one item.</exception>
        Public Sub New(ByVal Exceptions As IEnumerable(Of Exception))
            MyBase.New(ResourcesT.Exceptions.MultipleExceptionsHaveOccured)
            If Exceptions Is Nothing Then Throw New ArgumentNullException("Exceptions")
            If Exceptions.IsEmpty Then Throw New ArgumentException(ResourcesT.Exceptions.ListOfExceptionsCannotBeEmpty)
            If Exceptions.Count <= 1 Then Throw New ArgumentException(ResourcesT.Exceptions.MultipleExceptionClassRequiresAtLeast2Excptions)
            _Exceptions = Exceptions
        End Sub
        ''' <summary>CTor</summary>
        ''' <param name="Exceptions">Exceptions that have occured</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Exceptions"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Exceptions"/> is empty or contains only one item.</exception>
        ''' <param name="InnerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        Public Sub New(ByVal Exceptions As IEnumerable(Of Exception), ByVal InnerException As Exception)
            MyBase.New(ResourcesT.Exceptions.MultipleExceptionsHaveOccured, InnerException)
            If Exceptions Is Nothing Then Throw New ArgumentNullException("Exceptions")
            If Exceptions.IsEmpty Then Throw New ArgumentException(ResourcesT.Exceptions.ListOfExceptionsCannotBeEmpty)
            If Exceptions.Count <= 1 Then Throw New ArgumentException(ResourcesT.Exceptions.MultipleExceptionClassRequiresAtLeast2Excptions)
            _Exceptions = Exceptions
        End Sub
        ''' <summary>CTor</summary>
        ''' <param name="Exceptions">Exceptions that have occured</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Exceptions"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Exceptions"/> is empty or contains only one item.</exception>
        ''' <param name="Message">The message that describes the error.</param>
        Public Sub New(ByVal Exceptions As IEnumerable(Of Exception), ByVal Message$)
            MyBase.New(Message)
            If Exceptions Is Nothing Then Throw New ArgumentNullException("Exceptions")
            If Exceptions.IsEmpty Then Throw New ArgumentException(ResourcesT.Exceptions.ListOfExceptionsCannotBeEmpty)
            If Exceptions.Count <= 1 Then Throw New ArgumentException(ResourcesT.Exceptions.MultipleExceptionClassRequiresAtLeast2Excptions)
            _Exceptions = Exceptions
        End Sub
        ''' <summary>CTor</summary>
        ''' <param name="Exceptions">Exceptions that have occured</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Exceptions"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="Exceptions"/> is empty or contains only one item.</exception>
        ''' <param name="Message">The message that describes the error.</param>
        ''' <param name="InnerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        Public Sub New(ByVal Exceptions As IEnumerable(Of Exception), ByVal Message$, ByVal InnerException As Exception)
            MyBase.New(Message, InnerException)
            If Exceptions Is Nothing Then Throw New ArgumentNullException("Exceptions")
            If Exceptions.IsEmpty Then Throw New ArgumentException(ResourcesT.Exceptions.ListOfExceptionsCannotBeEmpty)
            If Exceptions.Count <= 1 Then Throw New ArgumentException(ResourcesT.Exceptions.MultipleExceptionClassRequiresAtLeast2Excptions)
            _Exceptions = Exceptions
        End Sub
        ''' <summary>Contains value of the <see cref="Exceptions"/> property</summary>
        Private _Exceptions As IEnumerable(Of Exception)
        ''' <summary>Gets exceptions that occured</summary>
        Public ReadOnly Property Exceptions() As IEnumerable(Of Exception)
            Get
                Return _Exceptions
            End Get
        End Property

        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of System.Exception) Implements System.Collections.Generic.IEnumerable(Of System.Exception).GetEnumerator
            Return Exceptions.GetEnumerator
        End Function

        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        Private Function IEnumerable_GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return Exceptions.GetEnumerator
        End Function
        ''' <summary>Gets exception or null based items in given collction</summary>
        ''' <param name="Exceptions">Exception that have occured</param>
        ''' <returns>Null when <paramref name="Exceptions"/> is null or empty; first item from <paramref name="Exceptions"/> when it has only one item; <see cref="MultipleException"/> whan <paramref name="Exceptions"/> contains more than one item.</returns>
        Public Shared Function GetException(ByVal Exceptions As IEnumerable(Of Exception)) As Exception
            If Exceptions Is Nothing OrElse Exceptions.IsEmpty Then
                Return Nothing
            ElseIf Exceptions.Count = 1 Then
                Return Exceptions.First
            Else
                Return New MultipleException(Exceptions)
            End If
        End Function
    End Class
End Namespace
#End If

