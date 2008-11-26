Imports System.Runtime.CompilerServices
#If Config <= Nightly Then 'Stage:Nightly
Namespace ExtensionsT
    ''' <summary>Contains <see cref="[Delegate]"/>-related extension functions</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed. Accessibility changed to public.</version>
    Public Module DelegateExtensions
#Region "Function"
        ''' <summary>Wraps given procedure delegate as delegate of function returning always null</summary>
        ''' <param name="d">Delegate to be wrapped</param>
        ''' <returns>Function that invokes delegate <paramref name="d"/> and returns null</returns>
        <Extension()> _
        Public Function [AsFunction](ByVal d As action) As Func(Of Object)
            Return Function() d.DynamicInvoke(New Object() {})
        End Function
        ''' <summary>Wraps given procedure delegate as delegate of function returning always null</summary>
        ''' <param name="d">Delegate to be wrapped</param>
        ''' <returns>Function that invokes delegate <paramref name="d"/> and returns null</returns>
        ''' <typeparam name="T1">Type of first argument of procedure</typeparam>
        <Extension()> _
        Public Function [AsFunction](Of T1)(ByVal d As Action(Of T1)) As Func(Of T1, Object)
            Return Function(a1 As T1) d.DynamicInvoke(New Object() {a1})
        End Function
        ''' <summary>Wraps given procedure delegate as delegate of function returning always null</summary>
        ''' <param name="d">Delegate to be wrapped</param>
        ''' <returns>Function that invokes delegate <paramref name="d"/> and returns null</returns>
        ''' <typeparam name="T1">Type of first argument of procedure</typeparam>
        ''' <typeparam name="T2">Type of 2nd argument of procedure</typeparam>
        <Extension()> _
        Public Function [Function](Of T1, T2)(ByVal d As Action(Of T1, T2)) As Func(Of T1, T2, Object)
            Return Function(a1 As T1, a2 As T2) d.DynamicInvoke(New Object() {a1, a2})
        End Function
        ''' <summary>Wraps given procedure delegate as delegate of function returning always null</summary>
        ''' <param name="d">Delegate to be wrapped</param>
        ''' <returns>Function that invokes delegate <paramref name="d"/> and returns null</returns>
        ''' <typeparam name="T1">Type of first argument of procedure</typeparam>
        ''' <typeparam name="T2">Type of 2nd argument of procedure</typeparam>
        ''' <typeparam name="T3">Type of third argument of procedure</typeparam>
        <Extension()> _
        Public Function [AsFunction](Of T1, T2, T3)(ByVal d As Action(Of T1, T2, T3)) As Func(Of T1, T2, T3, Object)
            Return Function(a1 As T1, a2 As T2, a3 As T3) d.DynamicInvoke(New Object() {a1, a2, a3})
        End Function
        ''' <summary>Wraps given procedure delegate as delegate of function returning always null</summary>
        ''' <param name="d">Delegate to be wrapped</param>
        ''' <returns>Function that invokes delegate <paramref name="d"/> and returns null</returns>
        ''' <typeparam name="T1">Type of first argument of procedure</typeparam>
        ''' <typeparam name="T2">Type of 2nd argument of procedure</typeparam>
        ''' <typeparam name="T3">Type of third argument of procedure</typeparam>
        ''' <typeparam name="T4">Type of 4th argument of procedure</typeparam>
        <Extension()> _
       Public Function [AsFunction](Of T1, T2, T3, T4)(ByVal d As Action(Of T1, T2, T3, T4)) As Func(Of T1, T2, T3, T4, Object)
            Return Function(a1 As T1, a2 As T2, a3 As T3, a4 As T4) d.DynamicInvoke(New Object() {a1, a2, a3, a4})
        End Function
#End Region
#Region "Ommit"
        ''' <summary>Wraps given procedure delegate as procedure with one more argument (which is ignored)</summary>
        ''' <param name="d">Delegate to wrap</param>
        ''' <typeparam name="TIgnored">Type of ignored argument</typeparam>
        ''' <returns>Delegate of procedure with one more argument then <paramref name="d"/></returns>
        <Extension()> _
        Public Function AddArgument(Of TIgnored)(ByVal d As action) As Action(Of TIgnored)
            Return Function(ignored As TIgnored) d.DynamicInvoke(New Object() {})
        End Function
        ''' <summary>Wraps given procedure delegate as procedure with one more argument (which is ignored)</summary>
        ''' <param name="d">Delegate to wrap</param>
        ''' <typeparam name="TIgnored">Type of ignored argument</typeparam>
        ''' <returns>Delegate of procedure with one more argument then <paramref name="d"/></returns>
        ''' <typeparam name="T1">Type of first argument of procedure <paramref name="d"/></typeparam>
        <Extension()> _
        Public Function AddArgument(Of T1, TIgnored)(ByVal d As Action(Of T1)) As Action(Of T1, TIgnored)
            Return Function(a1 As T1, ignored As TIgnored) d.DynamicInvoke(New Object() {a1})
        End Function
        ''' <summary>Wraps given procedure delegate as procedure with one more argument (which is ignored)</summary>
        ''' <param name="d">Delegate to wrap</param>
        ''' <typeparam name="TIgnored">Type of ignored argument</typeparam>
        ''' <returns>Delegate of procedure with one more argument then <paramref name="d"/></returns>
        ''' <typeparam name="T1">Type of first argument of procedure <paramref name="d"/></typeparam>
        ''' <typeparam name="T2">Type of second argument of procedure <paramref name="d"/></typeparam>
        <Extension()> _
        Public Function AddArgument(Of T1, T2, Tignored)(ByVal d As Action(Of T1, T2)) As Action(Of T1, T2, Tignored)
            Return Function(a1 As T1, a2 As T2, ignored As Tignored) d.DynamicInvoke(New Object() {a1, a2})
        End Function
        ''' <summary>Wraps given procedure delegate as procedure with one more argument (which is ignored)</summary>
        ''' <param name="d">Delegate to wrap</param>
        ''' <typeparam name="TIgnored">Type of ignored argument</typeparam>
        ''' <returns>Delegate of procedure with one more argument then <paramref name="d"/></returns>
        ''' <typeparam name="T1">Type of first argument of procedure <paramref name="d"/></typeparam>
        ''' <typeparam name="T2">Type of second argument of procedure <paramref name="d"/></typeparam>
        ''' <typeparam name="T3">Type of third argument of procedure <paramref name="d"/></typeparam>
        <Extension()> _
       Public Function AddArgument(Of T1, T2, T3, TIgnored)(ByVal d As Action(Of T1, T2, T3)) As Action(Of T1, T2, T3, TIgnored)
            Return Function(a1 As T1, a2 As T2, a3 As T3, ignored As TIgnored) d.DynamicInvoke(New Object() {a1, a2, a3})
        End Function


        ''' <summary>Wraps given function delegate as function with one more argument (which is ignored)</summary>
        ''' <param name="d">Delegate to wrap</param>
        ''' <typeparam name="TIgnored">Type of ignored argument</typeparam>
        ''' <typeparam name=" TRet">Return type of function</typeparam>
        ''' <returns>Delegate of function with one more argument than <paramref name="d"/></returns>
        <Extension()> _
        Public Function AddArgument(Of TIgnored, TRet)(ByVal d As Func(Of TRet)) As Func(Of TIgnored, TRet)
            Return Function(ignored As TIgnored) d.Invoke
        End Function
        ''' <summary>Wraps given function delegate as function with one more argument (which is ignored)</summary>
        ''' <param name="d">Delegate to wrap</param>
        ''' <typeparam name="TIgnored">Type of ignored argument</typeparam>
        ''' <typeparam name=" TRet">Return type of function</typeparam>
        ''' <returns>Delegate of function with one more argument than <paramref name="d"/></returns>
        ''' <typeparam name="T1">Type of first argument of procedure <paramref name="d"/></typeparam>
        <Extension()> _
        Public Function AddArgument(Of T1, TIgnored, TRet)(ByVal d As Func(Of T1, TRet)) As Func(Of T1, TIgnored, TRet)
            Return Function(a1 As T1, ignored As TIgnored) d.Invoke(a1)
        End Function
        ''' <summary>Wraps given function delegate as function with one more argument (which is ignored)</summary>
        ''' <param name="d">Delegate to wrap</param>
        ''' <typeparam name="TIgnored">Type of ignored argument</typeparam>
        ''' <typeparam name=" TRet">Return type of function</typeparam>
        ''' <returns>Delegate of function with one more argument than <paramref name="d"/></returns>
        ''' <typeparam name="T1">Type of first argument of procedure <paramref name="d"/></typeparam>
        ''' <typeparam name="T2">Type of second argument of procedure <paramref name="d"/></typeparam>
        <Extension()> _
        Public Function AddArgument(Of T1, T2, TIgnored, TRet)(ByVal d As Func(Of T1, T2, TRet)) As Func(Of T1, T2, TIgnored, TRet)
            Return Function(a1 As T1, a2 As T2, ignored As TIgnored) d.Invoke(a1, a2)
        End Function
        ''' <summary>Wraps given function delegate as function with one more argument (which is ignored)</summary>
        ''' <param name="d">Delegate to wrap</param>
        ''' <typeparam name="TIgnored">Type of ignored argument</typeparam>
        ''' <typeparam name=" TRet">Return type of function</typeparam>
        ''' <returns>Delegate of function with one more argument than <paramref name="d"/></returns>
        ''' <typeparam name="T1">Type of first argument of procedure <paramref name="d"/></typeparam>
        ''' <typeparam name="T2">Type of second argument of procedure <paramref name="d"/></typeparam>
        ''' <typeparam name="T3">Type of third argument of procedure <paramref name="d"/></typeparam>
        <Extension()> _
       Public Function AddArgument(Of T1, T2, T3, Tignored, TRet)(ByVal d As Func(Of T1, T2, T3, TRet)) As Func(Of T1, T2, T3, Tignored, TRet)
            Return Function(a1 As T1, a2 As T2, a3 As T3, ignored As Tignored) d.Invoke(a1, a2, a3)
        End Function
#End Region
    End Module
End Namespace
#End If