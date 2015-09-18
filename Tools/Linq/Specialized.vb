Imports System.Linq
Imports System.Runtime.CompilerServices

#If True
Namespace LinqT
    ''' <summary>Type specific LINQ extensions</summary>
    Module Specialized
        ''' <summary>Invokes all given delegates and returns results of them</summary>
        ''' <param name="Delegates">Delegates to be invoked</param>
        ''' <typeparam name="TResult">Type of return value of delegate</typeparam>
        ''' <returns>Collection of results of all delegates</returns>
        ''' <remarks>Results are obtained using LINQ, sou delegates are invoked when needed</remarks>
        <Extension()> _
        Function Results(Of TResult)(ByVal Delegates As IEnumerable(Of Func(Of TResult))) As IEnumerable(Of TResult)
            Return From del In Delegates Select del.Invoke
        End Function
        ''' <summary>Invokes all given delegates and returns results of them</summary>
        ''' <param name="Delegates">Delegates to be invoked</param>
        ''' <param name="Arg1">1st argument</param>
        ''' <typeparam name="T1">Type of 1st argument</typeparam>
        ''' <typeparam name="TResult">Type of return value of delegate</typeparam>
        ''' <returns>Collection of results of all delegates</returns>
        ''' <remarks>Results are obtained using LINQ, sou delegates are invoked when needed</remarks>
        <Extension()> _
        Function Results(Of T1, TResult)(ByVal Delegates As IEnumerable(Of Func(Of T1, TResult)), ByVal Arg1 As T1) As IEnumerable(Of TResult)
            Return From del In Delegates Select del.Invoke(Arg1)
        End Function
        ''' <summary>Invokes all given delegates and returns results of them</summary>
        ''' <param name="Delegates">Delegates to be invoked</param>
        ''' <param name="Arg1">1st argument</param>
        ''' <param name="Arg2">2nd argument</param>
        ''' <typeparam name="T1">Type of 1st argument</typeparam>
        ''' <typeparam name="T2">Type of 2nd argument</typeparam>
        ''' <typeparam name="TResult">Type of return value of delegate</typeparam>
        ''' <returns>Collection of results of all delegates</returns>
        ''' <remarks>Results are obtained using LINQ, sou delegates are invoked when needed</remarks>
        <Extension()> _
        Function Results(Of T1, T2, TResult)(ByVal Delegates As IEnumerable(Of Func(Of T1, T2, TResult)), ByVal Arg1 As T1, ByVal Arg2 As T2) As IEnumerable(Of TResult)
            Return From del In Delegates Select del.Invoke(Arg1, Arg2)
        End Function
        ''' <summary>Invokes all given delegates and returns results of them</summary>
        ''' <param name="Delegates">Delegates to be invoked</param>
        ''' <param name="Arg1">1st argument</param>
        ''' <param name="Arg2">2nd argument</param>
        ''' <param name="Arg3">3rd argument</param>
        ''' <typeparam name="T1">Type of 1st argument</typeparam>
        ''' <typeparam name="T2">Type of 2nd argument</typeparam>
        ''' <typeparam name="T3">Type of 3rd argument</typeparam>
        ''' <typeparam name="TResult">Type of return value of delegate</typeparam>
        ''' <returns>Collection of results of all delegates</returns>
        ''' <remarks>Results are obtained using LINQ, sou delegates are invoked when needed</remarks>
        <Extension()> _
        Function Results(Of T1, T2, T3, TResult)(ByVal Delegates As IEnumerable(Of Func(Of T1, T2, T3, TResult)), ByVal Arg1 As T1, ByVal Arg2 As T2, ByVal Arg3 As T3) As IEnumerable(Of TResult)
            Return From del In Delegates Select del.Invoke(Arg1, Arg2, Arg3)
        End Function
        ''' <summary>Invokes all given delegates and returns results of them</summary>
        ''' <param name="Delegates">Delegates to be invoked</param>
        ''' <param name="Arg1">1st argument</param>
        ''' <param name="Arg2">2nd argument</param>
        ''' <param name="Arg3">3rd argument</param>
        ''' <param name="Arg4">4th argument</param>
        ''' <typeparam name="T1">Type of 1st argument</typeparam>
        ''' <typeparam name="T2">Type of 2nd argument</typeparam>
        ''' <typeparam name="T3">Type of 3rd argument</typeparam>
        ''' <typeparam name="T4">Type of 4th argument</typeparam>
        ''' <typeparam name="TResult">Type of return value of delegate</typeparam>
        ''' <returns>Collection of results of all delegates</returns>
        ''' <remarks>Results are obtained using LINQ, sou delegates are invoked when needed</remarks>
        <Extension()> _
        Function Results(Of T1, T2, T3, T4, TResult)(ByVal Delegates As IEnumerable(Of Func(Of T1, T2, T3, T4, TResult)), ByVal Arg1 As T1, ByVal Arg2 As T2, ByVal Arg3 As T3, ByVal Arg4 As T4) As IEnumerable(Of TResult)
            Return From del In Delegates Select del.Invoke(Arg1, Arg2, Arg3, Arg4)
        End Function
        ''' <summary>Performs AND on results of delegates</summary>
        ''' <param name="Delegates">Delegates to perform AND on results of</param>
        ''' <param name="Condition">Condition to be passed to all delegates</param>
        ''' <typeparam name="TCondition">Type of condition</typeparam>
        ''' <returns>Result of AND operation on delegate results</returns>
        ''' <remarks>If <paramref name="Delegates"/> is empty returns <c>false</c></remarks>
        <Extension()> _
        Public Function [And](Of TCondition)(ByVal Delegates As IEnumerable(Of Func(Of TCondition, Boolean)), ByVal Condition As TCondition) As Boolean
            Return Delegates.Results(Condition).And
        End Function
        ''' <summary>Performs OR on results of delegates</summary>
        ''' <param name="Delegates">Delegates to perform OR on results of</param>
        ''' <param name="Condition">Condition to be passed to all delegates</param>
        ''' <typeparam name="TCondition">Type of condition</typeparam>
        ''' <returns>Result of OR operation on delegate results</returns>
        ''' <remarks>If <paramref name="Delegates"/> is empty returns <c>false</c></remarks>
        <Extension()> _
        Public Function [Or](Of TCondition)(ByVal Delegates As IEnumerable(Of Func(Of TCondition, Boolean)), ByVal Condition As TCondition) As Boolean
            Return Delegates.Results(Condition).Or
        End Function
        ''' <summary>Performs AND between given <see cref="Boolean"/> values</summary>
        ''' <param name="bools">Values to perform AND on</param>
        ''' <returns>Results of AND operation on <paramref name="bools"/></returns>
        ''' <remarks>If <paramref name="bools"/> is empty resutns <c>true</c></remarks>
        <Extension()> _
        Public Function [And](ByVal bools As IEnumerable(Of Boolean)) As Boolean
            For Each Imt In bools
                If Not Imt Then Return False
            Next
            Return True
        End Function
        ''' <summary>Performs OR between given <see cref="Boolean"/> values</summary>
        ''' <param name="bools">Values to perform OR on</param>
        ''' <returns>Results of OR operation on <paramref name="bools"/></returns>
        ''' <remarks>If <paramref name="bools"/> is empty resutns <c>false</c></remarks>
        <Extension()> _
        Public Function [Or](ByVal bools As IEnumerable(Of Boolean)) As Boolean
            For Each Itm In bools
                If Itm Then Return True
            Next
            Return False
        End Function
    End Module
End Namespace
#End If