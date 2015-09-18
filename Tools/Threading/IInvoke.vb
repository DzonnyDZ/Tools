Imports System.Runtime.CompilerServices, System.Reflection
Namespace ThreadingT
#If True
    ''' <summary>Represents an object which is tied to a thread and can invoke methods in context of that thread</summary>
    ''' <remarks>Main purpose of this interface is to provide type-safe call of <see cref="IInvoke.Invoke"/> methods using <see cref="IInvokeExtensions"/>.</remarks>
    ''' <seelaso cref="IInvokeExtensions"/>
    ''' <version version="1.5.3">This interface is new in version 1.5.3</version>
    Public Interface IInvoke
        ''' <summary>Synchronously invokes a delegate in UI thread</summary>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="params">Delegate parameters</param>
        ''' <returns>Result of delegate call</returns>
        ''' <remarks><see cref="IInvokeExtensions"/> module provides type safe overloaded generic extension methods to this method</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Function Invoke(ByVal [delegate] As [Delegate], ByVal ParamArray params As Object()) As Object
    End Interface

    ''' <summary>Provides extension methods for <see cref="IInvoke"/> interface</summary>
    ''' <seealso cref="IInvoke"/><seealso cref="T:Tools.WindowsT.ThreadingT.DispatcherExtensions"/>
    ''' <version version="1.5.3">This module is new in version 1.5.3</version>
    Public Module IInvokeExtensions
#Region "Functions"
        ''' <summary>Calls parameter-less function in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <typeparam name="TResult">Return type of <paramref name="delegate"/></typeparam>
        ''' <returns>Result of call to <paramref name="delegate"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Function Invoke(Of TResult)(ByVal [object] As IInvoke, ByVal [delegate] As Func(Of TResult)) As TResult
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            Return [object].Invoke([delegate])
        End Function
        ''' <summary>Calls a function with 1 parameter in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <typeparam name="TResult">Return type of <paramref name="delegate"/></typeparam>
        ''' <typeparam name="T">Type of 1st parameter</typeparam>
        ''' <returns>Result of call to <paramref name="delegate"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Function Invoke(Of T, TResult)(ByVal [object] As IInvoke, ByVal [delegate] As Func(Of T, TResult), ByVal param1 As T) As TResult
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            Return [object].Invoke([delegate], param1)
        End Function
        ''' <summary>Calls a function with 2 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <typeparam name="TResult">Return type of <paramref name="delegate"/></typeparam>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <returns>Result of call to <paramref name="delegate"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Function Invoke(Of T1, T2, TResult)(ByVal [object] As IInvoke, ByVal [delegate] As Func(Of T1, T2, TResult), ByVal param1 As T1, ByVal param2 As T2) As TResult
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            Return [object].Invoke([delegate], param1, param2)
        End Function
        ''' <summary>Calls a function with 3 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <typeparam name="TResult">Return type of <paramref name="delegate"/></typeparam>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <returns>Result of call to <paramref name="delegate"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Function Invoke(Of T1, T2, T3, TResult)(ByVal [object] As IInvoke, ByVal [delegate] As Func(Of T1, T2, T3, TResult), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3) As TResult
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            Return [object].Invoke([delegate], param1, param2, param3)
        End Function
        ''' <summary>Calls a function with 4 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <typeparam name="TResult">Return type of <paramref name="delegate"/></typeparam>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <returns>Result of call to <paramref name="delegate"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Function Invoke(Of T1, T2, T3, T4, TResult)(ByVal [object] As IInvoke, ByVal [delegate] As Func(Of T1, T2, T3, T4, TResult), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4) As TResult
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            Return [object].Invoke([delegate], param1, param2, param3, param4)
        End Function
        ''' <summary>Calls a function with 5 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <typeparam name="TResult">Return type of <paramref name="delegate"/></typeparam>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <returns>Result of call to <paramref name="delegate"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Function Invoke(Of T1, T2, T3, T4, T5, TResult)(ByVal [object] As IInvoke, ByVal [delegate] As Func(Of T1, T2, T3, T4, T5, TResult), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5) As TResult
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            Return [object].Invoke([delegate], param1, param2, param3, param4, param5)
        End Function
        ''' <summary>Calls a function with 6 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <typeparam name="TResult">Return type of <paramref name="delegate"/></typeparam>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <returns>Result of call to <paramref name="delegate"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Function Invoke(Of T1, T2, T3, T4, T5, T6, TResult)(ByVal [object] As IInvoke, ByVal [delegate] As Func(Of T1, T2, T3, T4, T5, T6, TResult), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6) As TResult
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            Return [object].Invoke([delegate], param1, param2, param3, param4, param5, param6)
        End Function
        ''' <summary>Calls a function with 7 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <typeparam name="TResult">Return type of <paramref name="delegate"/></typeparam>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <returns>Result of call to <paramref name="delegate"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Function Invoke(Of T1, T2, T3, T4, T5, T6, T7, TResult)(ByVal [object] As IInvoke, ByVal [delegate] As Func(Of T1, T2, T3, T4, T5, T6, T7, TResult), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7) As TResult
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            Return [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7)
        End Function
        ''' <summary>Calls a function with 8 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <typeparam name="TResult">Return type of <paramref name="delegate"/></typeparam>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <returns>Result of call to <paramref name="delegate"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Function Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8, TResult)(ByVal [object] As IInvoke, ByVal [delegate] As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, TResult), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8) As TResult
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            Return [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8)
        End Function
        ''' <summary>Calls a function with 9 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <param name="param9">9th parameter</param>
        ''' <typeparam name="TResult">Return type of <paramref name="delegate"/></typeparam>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <typeparam name="T9">Type of 9th parameter</typeparam>
        ''' <returns>Result of call to <paramref name="delegate"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Function Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult)(ByVal [object] As IInvoke, ByVal [delegate] As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8, ByVal param9 As T9) As TResult
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            Return [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8, param9)
        End Function
        ''' <summary>Calls a function with 10 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <param name="param9">9th parameter</param>
        ''' <param name="param10">10th parameter</param>
        ''' <typeparam name="TResult">Return type of <paramref name="delegate"/></typeparam>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <typeparam name="T9">Type of 9th parameter</typeparam>
        ''' <typeparam name="T10">Type of 10th parameter</typeparam>
        ''' <returns>Result of call to <paramref name="delegate"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Function Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult)(ByVal [object] As IInvoke, ByVal [delegate] As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8, ByVal param9 As T9, ByVal param10 As T10) As TResult
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            Return [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8, param9, param10)
        End Function
        ''' <summary>Calls a function with 11 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <param name="param9">9th parameter</param>
        ''' <param name="param10">10th parameter</param>
        ''' <param name="param11">11th parameter</param>
        ''' <typeparam name="TResult">Return type of <paramref name="delegate"/></typeparam>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <typeparam name="T9">Type of 9th parameter</typeparam>
        ''' <typeparam name="T10">Type of 10th parameter</typeparam>
        ''' <typeparam name="T11">Type of 11th parameter</typeparam>
        ''' <returns>Result of call to <paramref name="delegate"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Function Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult)(ByVal [object] As IInvoke, ByVal [delegate] As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8, ByVal param9 As T9, ByVal param10 As T10, ByVal param11 As T11) As TResult
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            Return [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11)
        End Function
        ''' <summary>Calls a function with 12 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <param name="param9">9th parameter</param>
        ''' <param name="param10">10th parameter</param>
        ''' <param name="param11">11th parameter</param>
        ''' <param name="param12">12th parameter</param>
        ''' <typeparam name="TResult">Return type of <paramref name="delegate"/></typeparam>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <typeparam name="T9">Type of 9th parameter</typeparam>
        ''' <typeparam name="T10">Type of 10th parameter</typeparam>
        ''' <typeparam name="T11">Type of 11th parameter</typeparam>
        ''' <typeparam name="T12">Type of 12th parameter</typeparam>
        ''' <returns>Result of call to <paramref name="delegate"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Function Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult)(ByVal [object] As IInvoke, ByVal [delegate] As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8, ByVal param9 As T9, ByVal param10 As T10, ByVal param11 As T11, ByVal param12 As T12) As TResult
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            Return [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12)
        End Function
        ''' <summary>Calls a function with 13 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <param name="param9">9th parameter</param>
        ''' <param name="param10">10th parameter</param>
        ''' <param name="param11">11th parameter</param>
        ''' <param name="param12">12th parameter</param>
        ''' <param name="param13">13th parameter</param>
        ''' <typeparam name="TResult">Return type of <paramref name="delegate"/></typeparam>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <typeparam name="T9">Type of 9th parameter</typeparam>
        ''' <typeparam name="T10">Type of 10th parameter</typeparam>
        ''' <typeparam name="T11">Type of 11th parameter</typeparam>
        ''' <typeparam name="T12">Type of 12th parameter</typeparam>
        ''' <typeparam name="T13">Type of 13th parameter</typeparam>
        ''' <returns>Result of call to <paramref name="delegate"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Function Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult)(ByVal [object] As IInvoke, ByVal [delegate] As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8, ByVal param9 As T9, ByVal param10 As T10, ByVal param11 As T11, ByVal param12 As T12, ByVal param13 As T13) As TResult
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            Return [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13)
        End Function
        ''' <summary>Calls a function with 14 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <param name="param9">9th parameter</param>
        ''' <param name="param10">10th parameter</param>
        ''' <param name="param11">11th parameter</param>
        ''' <param name="param12">12th parameter</param>
        ''' <param name="param13">13th parameter</param>
        ''' <param name="param14">14th parameter</param>
        ''' <typeparam name="TResult">Return type of <paramref name="delegate"/></typeparam>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <typeparam name="T9">Type of 9th parameter</typeparam>
        ''' <typeparam name="T10">Type of 10th parameter</typeparam>
        ''' <typeparam name="T11">Type of 11th parameter</typeparam>
        ''' <typeparam name="T12">Type of 12th parameter</typeparam>
        ''' <typeparam name="T13">Type of 13th parameter</typeparam>
        ''' <typeparam name="T14">Type of 14th parameter</typeparam>
        ''' <returns>Result of call to <paramref name="delegate"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Function Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult)(ByVal [object] As IInvoke, ByVal [delegate] As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8, ByVal param9 As T9, ByVal param10 As T10, ByVal param11 As T11, ByVal param12 As T12, ByVal param13 As T13, ByVal param14 As T14) As TResult
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            Return [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14)
        End Function
        ''' <summary>Calls a function with 15 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <param name="param9">9th parameter</param>
        ''' <param name="param10">10th parameter</param>
        ''' <param name="param11">11th parameter</param>
        ''' <param name="param12">12th parameter</param>
        ''' <param name="param13">13th parameter</param>
        ''' <param name="param14">14th parameter</param>
        ''' <param name="param15">15th parameter</param>
        ''' <typeparam name="TResult">Return type of <paramref name="delegate"/></typeparam>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <typeparam name="T9">Type of 9th parameter</typeparam>
        ''' <typeparam name="T10">Type of 10th parameter</typeparam>
        ''' <typeparam name="T11">Type of 11th parameter</typeparam>
        ''' <typeparam name="T12">Type of 12th parameter</typeparam>
        ''' <typeparam name="T13">Type of 13th parameter</typeparam>
        ''' <typeparam name="T14">Type of 14th parameter</typeparam>
        ''' <typeparam name="T15">Type of 15th parameter</typeparam>
        ''' <returns>Result of call to <paramref name="delegate"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Function Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult)(ByVal [object] As IInvoke, ByVal [delegate] As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8, ByVal param9 As T9, ByVal param10 As T10, ByVal param11 As T11, ByVal param12 As T12, ByVal param13 As T13, ByVal param14 As T14, ByVal param15 As T15) As TResult
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            Return [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15)
        End Function
        ''' <summary>Calls a function with 16 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <param name="param9">9th parameter</param>
        ''' <param name="param10">10th parameter</param>
        ''' <param name="param11">11th parameter</param>
        ''' <param name="param12">12th parameter</param>
        ''' <param name="param13">13th parameter</param>
        ''' <param name="param14">14th parameter</param>
        ''' <param name="param15">15th parameter</param>
        ''' <param name="param16">16th parameter</param>
        ''' <typeparam name="TResult">Return type of <paramref name="delegate"/></typeparam>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <typeparam name="T9">Type of 9th parameter</typeparam>
        ''' <typeparam name="T10">Type of 10th parameter</typeparam>
        ''' <typeparam name="T11">Type of 11th parameter</typeparam>
        ''' <typeparam name="T12">Type of 12th parameter</typeparam>
        ''' <typeparam name="T13">Type of 13th parameter</typeparam>
        ''' <typeparam name="T14">Type of 14th parameter</typeparam>
        ''' <typeparam name="T15">Type of 15th parameter</typeparam>
        ''' <typeparam name="T16">Type of 16th parameter</typeparam>
        ''' <returns>Result of call to <paramref name="delegate"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Function Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult)(ByVal [object] As IInvoke, ByVal [delegate] As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8, ByVal param9 As T9, ByVal param10 As T10, ByVal param11 As T11, ByVal param12 As T12, ByVal param13 As T13, ByVal param14 As T14, ByVal param15 As T15, ByVal param16 As T16) As TResult
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            Return [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15, param16)
        End Function
#End Region
#Region "Procedures"
        ''' <summary>Calls parameter-less method in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <s>Result of call to <paramref name="delegate"/></s>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Sub Invoke(ByVal [object] As IInvoke, ByVal [delegate] As Action)
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            [object].Invoke([delegate])
        End Sub
        ''' <summary>Calls a method with 1 parameter in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <typeparam name="T">Type of 1st parameter</typeparam>
        ''' <s>Result of call to <paramref name="delegate"/></s>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Sub Invoke(Of T)(ByVal [object] As IInvoke, ByVal [delegate] As Action(Of T), ByVal param1 As T)
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            [object].Invoke([delegate], param1)
        End Sub
        ''' <summary>Calls a method with 2 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <s>Result of call to <paramref name="delegate"/></s>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Sub Invoke(Of T1, T2)(ByVal [object] As IInvoke, ByVal [delegate] As Action(Of T1, T2), ByVal param1 As T1, ByVal param2 As T2)
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            [object].Invoke([delegate], param1, param2)
        End Sub
        ''' <summary>Calls a method with 3 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <s>Result of call to <paramref name="delegate"/></s>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Sub Invoke(Of T1, T2, T3)(ByVal [object] As IInvoke, ByVal [delegate] As Action(Of T1, T2, T3), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3)
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            [object].Invoke([delegate], param1, param2, param3)
        End Sub
        ''' <summary>Calls a method with 4 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <s>Result of call to <paramref name="delegate"/></s>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Sub Invoke(Of T1, T2, T3, T4)(ByVal [object] As IInvoke, ByVal [delegate] As Action(Of T1, T2, T3, T4), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4)
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            [object].Invoke([delegate], param1, param2, param3, param4)
        End Sub
        ''' <summary>Calls a method with 5 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <s>Result of call to <paramref name="delegate"/></s>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Sub Invoke(Of T1, T2, T3, T4, T5)(ByVal [object] As IInvoke, ByVal [delegate] As Action(Of T1, T2, T3, T4, T5), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5)
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            [object].Invoke([delegate], param1, param2, param3, param4, param5)
        End Sub
        ''' <summary>Calls a method with 6 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <s>Result of call to <paramref name="delegate"/></s>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Sub Invoke(Of T1, T2, T3, T4, T5, T6)(ByVal [object] As IInvoke, ByVal [delegate] As Action(Of T1, T2, T3, T4, T5, T6), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6)
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            [object].Invoke([delegate], param1, param2, param3, param4, param5, param6)
        End Sub
        ''' <summary>Calls a method with 7 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <s>Result of call to <paramref name="delegate"/></s>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Sub Invoke(Of T1, T2, T3, T4, T5, T6, T7)(ByVal [object] As IInvoke, ByVal [delegate] As Action(Of T1, T2, T3, T4, T5, T6, T7), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7)
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7)
        End Sub
        ''' <summary>Calls a method with 8 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <s>Result of call to <paramref name="delegate"/></s>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Sub Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8)(ByVal [object] As IInvoke, ByVal [delegate] As Action(Of T1, T2, T3, T4, T5, T6, T7, T8), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8)
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8)
        End Sub
        ''' <summary>Calls a method with 9 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <param name="param9">9th parameter</param>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <typeparam name="T9">Type of 9th parameter</typeparam>
        ''' <s>Result of call to <paramref name="delegate"/></s>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Sub Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)(ByVal [object] As IInvoke, ByVal [delegate] As Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8, ByVal param9 As T9)
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8, param9)
        End Sub
        ''' <summary>Calls a method with 10 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <param name="param9">9th parameter</param>
        ''' <param name="param10">10th parameter</param>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <typeparam name="T9">Type of 9th parameter</typeparam>
        ''' <typeparam name="T10">Type of 10th parameter</typeparam>
        ''' <s>Result of call to <paramref name="delegate"/></s>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Sub Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)(ByVal [object] As IInvoke, ByVal [delegate] As Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8, ByVal param9 As T9, ByVal param10 As T10)
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8, param9, param10)
        End Sub
        ''' <summary>Calls a method with 11 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <param name="param9">9th parameter</param>
        ''' <param name="param10">10th parameter</param>
        ''' <param name="param11">11th parameter</param>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <typeparam name="T9">Type of 9th parameter</typeparam>
        ''' <typeparam name="T10">Type of 10th parameter</typeparam>
        ''' <typeparam name="T11">Type of 11th parameter</typeparam>
        ''' <s>Result of call to <paramref name="delegate"/></s>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Sub Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)(ByVal [object] As IInvoke, ByVal [delegate] As Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8, ByVal param9 As T9, ByVal param10 As T10, ByVal param11 As T11)
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11)
        End Sub
        ''' <summary>Calls a method with 12 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <param name="param9">9th parameter</param>
        ''' <param name="param10">10th parameter</param>
        ''' <param name="param11">11th parameter</param>
        ''' <param name="param12">12th parameter</param>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <typeparam name="T9">Type of 9th parameter</typeparam>
        ''' <typeparam name="T10">Type of 10th parameter</typeparam>
        ''' <typeparam name="T11">Type of 11th parameter</typeparam>
        ''' <typeparam name="T12">Type of 12th parameter</typeparam>
        ''' <s>Result of call to <paramref name="delegate"/></s>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Sub Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)(ByVal [object] As IInvoke, ByVal [delegate] As Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8, ByVal param9 As T9, ByVal param10 As T10, ByVal param11 As T11, ByVal param12 As T12)
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12)
        End Sub
        ''' <summary>Calls a method with 13 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <param name="param9">9th parameter</param>
        ''' <param name="param10">10th parameter</param>
        ''' <param name="param11">11th parameter</param>
        ''' <param name="param12">12th parameter</param>
        ''' <param name="param13">13th parameter</param>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <typeparam name="T9">Type of 9th parameter</typeparam>
        ''' <typeparam name="T10">Type of 10th parameter</typeparam>
        ''' <typeparam name="T11">Type of 11th parameter</typeparam>
        ''' <typeparam name="T12">Type of 12th parameter</typeparam>
        ''' <typeparam name="T13">Type of 13th parameter</typeparam>
        ''' <s>Result of call to <paramref name="delegate"/></s>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Sub Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)(ByVal [object] As IInvoke, ByVal [delegate] As Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8, ByVal param9 As T9, ByVal param10 As T10, ByVal param11 As T11, ByVal param12 As T12, ByVal param13 As T13)
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13)
        End Sub
        ''' <summary>Calls a method with 14 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <param name="param9">9th parameter</param>
        ''' <param name="param10">10th parameter</param>
        ''' <param name="param11">11th parameter</param>
        ''' <param name="param12">12th parameter</param>
        ''' <param name="param13">13th parameter</param>
        ''' <param name="param14">14th parameter</param>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <typeparam name="T9">Type of 9th parameter</typeparam>
        ''' <typeparam name="T10">Type of 10th parameter</typeparam>
        ''' <typeparam name="T11">Type of 11th parameter</typeparam>
        ''' <typeparam name="T12">Type of 12th parameter</typeparam>
        ''' <typeparam name="T13">Type of 13th parameter</typeparam>
        ''' <typeparam name="T14">Type of 14th parameter</typeparam>
        ''' <s>Result of call to <paramref name="delegate"/></s>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Sub Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)(ByVal [object] As IInvoke, ByVal [delegate] As Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8, ByVal param9 As T9, ByVal param10 As T10, ByVal param11 As T11, ByVal param12 As T12, ByVal param13 As T13, ByVal param14 As T14)
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14)
        End Sub
        ''' <summary>Calls a method with 15 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <param name="param9">9th parameter</param>
        ''' <param name="param10">10th parameter</param>
        ''' <param name="param11">11th parameter</param>
        ''' <param name="param12">12th parameter</param>
        ''' <param name="param13">13th parameter</param>
        ''' <param name="param14">14th parameter</param>
        ''' <param name="param15">15th parameter</param>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <typeparam name="T9">Type of 9th parameter</typeparam>
        ''' <typeparam name="T10">Type of 10th parameter</typeparam>
        ''' <typeparam name="T11">Type of 11th parameter</typeparam>
        ''' <typeparam name="T12">Type of 12th parameter</typeparam>
        ''' <typeparam name="T13">Type of 13th parameter</typeparam>
        ''' <typeparam name="T14">Type of 14th parameter</typeparam>
        ''' <typeparam name="T15">Type of 15th parameter</typeparam>
        ''' <s>Result of call to <paramref name="delegate"/></s>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Sub Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)(ByVal [object] As IInvoke, ByVal [delegate] As Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8, ByVal param9 As T9, ByVal param10 As T10, ByVal param11 As T11, ByVal param12 As T12, ByVal param13 As T13, ByVal param14 As T14, ByVal param15 As T15)
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15)
        End Sub
        ''' <summary>Calls a method with 16 parameters in UI thread</summary>
        ''' <param name="object">An object to invoke delegate in UI thread of</param>
        ''' <param name="delegate">A delegate to be invoked</param>
        ''' <param name="param1">1st parameter</param>
        ''' <param name="param2">2nd parameter</param>
        ''' <param name="param3">3rd parameter</param>
        ''' <param name="param4">4th parameter</param>
        ''' <param name="param5">5th parameter</param>
        ''' <param name="param6">6th parameter</param>
        ''' <param name="param7">7th parameter</param>
        ''' <param name="param8">8th parameter</param>
        ''' <param name="param9">9th parameter</param>
        ''' <param name="param10">10th parameter</param>
        ''' <param name="param11">11th parameter</param>
        ''' <param name="param12">12th parameter</param>
        ''' <param name="param13">13th parameter</param>
        ''' <param name="param14">14th parameter</param>
        ''' <param name="param15">15th parameter</param>
        ''' <param name="param16">16th parameter</param>
        ''' <typeparam name="T1">Type of 1st parameter</typeparam>
        ''' <typeparam name="T2">Type of 2nd parameter</typeparam>
        ''' <typeparam name="T3">Type of 3rd parameter</typeparam>
        ''' <typeparam name="T4">Type of 4th parameter</typeparam>
        ''' <typeparam name="T5">Type of 5th parameter</typeparam>
        ''' <typeparam name="T6">Type of 6th parameter</typeparam>
        ''' <typeparam name="T7">Type of 7th parameter</typeparam>
        ''' <typeparam name="T8">Type of 8th parameter</typeparam>
        ''' <typeparam name="T9">Type of 9th parameter</typeparam>
        ''' <typeparam name="T10">Type of 10th parameter</typeparam>
        ''' <typeparam name="T11">Type of 11th parameter</typeparam>
        ''' <typeparam name="T12">Type of 12th parameter</typeparam>
        ''' <typeparam name="T13">Type of 13th parameter</typeparam>
        ''' <typeparam name="T14">Type of 14th parameter</typeparam>
        ''' <typeparam name="T15">Type of 15th parameter</typeparam>
        ''' <typeparam name="T16">Type of 16th parameter</typeparam>
        ''' <s>Result of call to <paramref name="delegate"/></s>
        ''' <exception cref="ArgumentNullException"><paramref name="object"/> or <paramref name="delegate"/> is null</exception>
        ''' <exception cref="TargetInvocationException">Exception was thrown while invoking the <paramref name="delegate"/>. See <see cref="TargetInvocationException.InnerException"/> for details.</exception>
        <Extension()> Public Sub Invoke(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)(ByVal [object] As IInvoke, ByVal [delegate] As Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16), ByVal param1 As T1, ByVal param2 As T2, ByVal param3 As T3, ByVal param4 As T4, ByVal param5 As T5, ByVal param6 As T6, ByVal param7 As T7, ByVal param8 As T8, ByVal param9 As T9, ByVal param10 As T10, ByVal param11 As T11, ByVal param12 As T12, ByVal param13 As T13, ByVal param14 As T14, ByVal param15 As T15, ByVal param16 As T16)
            If [object] Is Nothing Then Throw New ArgumentNullException("object")
            If [delegate] Is Nothing Then Throw New ArgumentNullException("delegate")
            [object].Invoke([delegate], param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15, param16)
        End Sub
#End Region
    End Module
End Namespace
#End If