Imports System.Runtime.CompilerServices

#If Config <= Nightly Then 'Stage:Nightly
Namespace ExtensionsT
    ''' <summary>General extension funcions</summary>
    Public Module General
#Region "New if null"
        ''' <summary>If given object is null creates new instance of it</summary>
        ''' <param name="obj">Object to test</param>
        ''' <typeparam name="T">Type of object</typeparam>
        ''' <returns>If <paramref name="obj"/> is not null returns <paramref name="obj"/> otherwise creates new instance of <typeparamref name="T"/> using default constructor.</returns>
        <Extension()> _
        Public Function NewIfNull(Of T As {New, Class})(ByVal obj As T) As T
            Return If(obj, New T)
        End Function
        ''' <summary>If given object is null creates new instance of it</summary>
        ''' <param name="obj">Object to test</param>
        ''' <typeparam name="T">Type of object</typeparam>
        ''' <returns>If <paramref name="obj"/> is not null returns <paramref name="obj"/> otherwise creates new instance of array of <typeparamref name="T"/>.</returns>
        <Extension()> _
        Public Function NewIfNull(Of T)(ByVal obj As T()) As T()
            Return If(obj, New T() {})
        End Function
        ''' <summary>If given object is null creates new instance of it</summary>
        ''' <param name="obj">Object to test</param>
        ''' <typeparam name="T">Type of object</typeparam>
        ''' <returns>If <paramref name="obj"/> is not null returns <paramref name="obj"/> otherwise default instance of type <typeparamref name="T"/>.</returns>
        <Extension()> _
        Public Function NewIfNull(Of T As {Structure})(ByVal obj As T?) As T
            Return If(obj, New T)
        End Function
#End Region

#Region "Throw"
        ''' <summary>Throws given exception</summary>
        ''' <param name="ex">Exception to be thrown</param>
        ''' <typeparam name="T">Type of exception to be thrown</typeparam>
        ''' <returns>This function never returns.</returns>
        ''' <exception cref="Exception">Exception of type <typeparamref name="T"/> <paramref name="ex"/> is always thrown</exception>
        ''' <remarks>You can use this function to thown an exception in place where <c>Throw</c> cannot be used because an expression is expected.</remarks>
        <Extension()> _
        Public Function [DoThrow](Of T As Exception)(ByVal ex As T) As T
            Throw ex
        End Function
        ''' <summary>Throws an <see cref="ArgumentNullException"/> if given object is null</summary>
        ''' <param name="arg">Object to be tested</param>
        ''' <param name="argumentName">Optional. Name of argument for exception. If null "arg" is supplied.</param>
        ''' <typeparam name="T">Type of object. Must be reference type.</typeparam>
        ''' <exception cref="ArgumentNullException"><paramref name="arg"/> is null</exception>
        ''' <remarks>You should throw your exception yourself whenever possible because place where it was thrown from appears on call stack, however there ara some situations where it is not possible due to programming language limitations (chanied/base CTor call, field initializers, LINQ.</remarks>
        ''' <version version="1.5.3">Parameter <c>ArgumentName</c> renamed to <c>argumentName</c></version>
        ''' <version version="1.5.3">Documentation fix: Throws <see cref="ArgumentNullException"/> when <paramref name="arg"/> is null. Originally documentation stated tha <see cref="ArgumentException"/> is thrown, which was not accurate.</version>
        <Extension()> _
        Public Function ThrowIfNull(Of T As Class)(ByVal arg As T, Optional ByVal argumentName$ = Nothing) As T
            If arg Is Nothing Then Throw New ArgumentNullException(If(argumentName, "arg"))
            Return arg
        End Function
        ''' <summary>Throws an <see cref="ArgumentNullException"/> if given objects is null, if it is not null returns another given object</summary>
        ''' <typeparam name="TTest">Type of value to be tested</typeparam>
        ''' <typeparam name="TReturn">Type of value to be returned when test succeds</typeparam>
        ''' <param name="valueToTest">Object to be tested</param>
        ''' <param name="valueToReturn">Value to be returned in case <paramref name="valueToTest"/> is not null</param>
        ''' <param name="argumentName">Optional. Name of argument for exception. If null "valueToTest" is supplied.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="valueToTest"/> is nulll</exception>
        ''' <remarks>You should throw your exception yourself whenever possible because place where it was thrown from appears on call stack, however there ara some situations where it is not possible due to programming language limitations (chanied/base CTor call, field initializers, LINQ.</remarks>
        ''' <version version="1.5.3">This overload is new in version 1.5.3</version>
        <Extension()>
        Public Function ThrowIfNull(Of TTest As Class, TReturn)(ByVal valueToTest As TTest, ByVal valueToReturn As TReturn, Optional ByVal argumentName$ = Nothing) As TReturn
            If valueToTest Is Nothing Then Throw New ArgumentNullException(If(argumentName, "valueToTest"))
            Return valueToReturn
        End Function
#End Region


    End Module
End Namespace
#End If