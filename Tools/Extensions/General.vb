Imports System.Runtime.CompilerServices

#If Config <= Nightly Then 'Stage:Nightly
Namespace ExtensionsT
    ''' <summary>General extension funcions</summary>
    Public Module General
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
    End Module
End Namespace
#End If