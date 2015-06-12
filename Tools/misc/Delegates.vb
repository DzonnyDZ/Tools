#If Config <= RC Then 'Stage: RC
''' <summary>Universal delegate of procedure with no argument</summary>
<Obsolete("Use System.Action instead"), EditorBrowsable(EditorBrowsableState.Never)> _
Public Delegate Sub dSub()
''' <summary>Universal delegate of procedure with 1 argument</summary>
''' <param name="arg1">First argument</param>
''' <typeparam name="T1">Type of first argument</typeparam>
<Obsolete("Use System.Action instead"), EditorBrowsable(EditorBrowsableState.Never)> _
Public Delegate Sub dSub(Of T1)(ByVal arg1 As T1)
''' <summary>Universal delegate of procedure with 2 arguments</summary>
''' <param name="arg1">First argument</param>
''' <param name="arg2">Second argument</param>
''' <typeparam name="T1">Type of first argument</typeparam>
''' <typeparam name="T2">Type of second argument</typeparam>
<Obsolete("Use System.Action instead"), EditorBrowsable(EditorBrowsableState.Never)> _
Public Delegate Sub dSub(Of T1, T2)(ByVal arg1 As T1, ByVal arg2 As T2)
''' <summary>Universal delegate of procedure with 3 arguments</summary>
''' <param name="arg1">First argument</param>
''' <param name="arg2">Second argument</param>
''' <param name="arg3">Third argument</param>
''' <typeparam name="T1">Type of first argument</typeparam>
''' <typeparam name="T2">Type of second argument</typeparam>
''' <typeparam name="T3">Type of third argument</typeparam>
<Obsolete("Use System.Action instead"), EditorBrowsable(EditorBrowsableState.Never)> _
Public Delegate Sub dSub(Of T1, T2, T3)(ByVal arg1 As T1, ByVal arg2 As T2, ByVal arg3 As T3)
''' <summary>Universal delegate of procedure with 4 arguments</summary>
''' <param name="arg1">First argument</param>
''' <param name="arg2">Second argument</param>
''' <param name="arg3">Third argument</param>
''' <param name="arg4">Fourth argument</param>
''' <typeparam name="T1">Type of first argument</typeparam>
''' <typeparam name="T2">Type of second argument</typeparam>
''' <typeparam name="T3">Type of third argument</typeparam>
''' <typeparam name="T4">Type of third argument</typeparam>
<Obsolete("Use System.Action instead"), EditorBrowsable(EditorBrowsableState.Never)> _
Public Delegate Sub dSub(Of T1, T2, T3, T4)(ByVal arg1 As T1, ByVal arg2 As T2, ByVal arg3 As T3, ByVal arg4 As T4)
''' <summary>Universal delegate of function with no argument</summary>
''' <typeparam name="TRet">Type of return value</typeparam>
<Obsolete("Use System.Func instead"), EditorBrowsable(EditorBrowsableState.Never)> _
Public Delegate Function dFunction(Of TRet)() As TRet
''' <summary>Universal delegate of function with 1 argument</summary>
''' <param name="arg1">First argument</param>
''' <typeparam name="TRet">Type of return value</typeparam>
''' <typeparam name="T1">Type of first argument</typeparam>
<Obsolete("Use System.Func instead"), EditorBrowsable(EditorBrowsableState.Never)> _
Public Delegate Function dFunction(Of TRet, T1)(ByVal arg1 As T1) As TRet
''' <summary>Universal delegate of function with 2 arguments</summary>
''' <param name="arg1">First argument</param>
''' <param name="arg2">Second argument</param>
''' <typeparam name="TRet">Type of return value</typeparam>
''' <typeparam name="T1">Type of first argument</typeparam>
''' <typeparam name="T2">Type of second argument</typeparam>
<Obsolete("Use System.Func instead"), EditorBrowsable(EditorBrowsableState.Never)> _
Public Delegate Function dFunction(Of TRet, T1, T2)(ByVal arg1 As T1, ByVal arg2 As T2) As TRet
''' <summary>Universal delegate of function with 3 arguments</summary>
''' <param name="arg1">First argument</param>
''' <param name="arg2">Second argument</param>
''' <param name="arg3">Third argument</param>
''' <typeparam name="TRet">Type of return value</typeparam>
''' <typeparam name="T1">Type of first argument</typeparam>
''' <typeparam name="T2">Type of second argument</typeparam>
''' <typeparam name="T3">Type of third argument</typeparam>
<Obsolete("Use System.Func instead"), EditorBrowsable(EditorBrowsableState.Never)> _
Public Delegate Function dFunction(Of TRet, T1, T2, T3)(ByVal arg1 As T1, ByVal arg2 As T2, ByVal arg3 As T3) As TRet
''' <summary>Universal delegate of function with 4 arguments</summary>
''' <param name="arg1">First argument</param>
''' <param name="arg2">Second argument</param>
''' <param name="arg3">Third argument</param>
''' <param name="arg4">Fourth argument</param>
''' <typeparam name="TRet">Type of return value</typeparam>
''' <typeparam name="T1">Type of first argument</typeparam>
''' <typeparam name="T2">Type of second argument</typeparam>
''' <typeparam name="T3">Type of third argument</typeparam>
''' <typeparam name="T4">Type of third argument</typeparam>
<Obsolete("Use System.Func instead"), EditorBrowsable(EditorBrowsableState.Never)> _
Public Delegate Function dFunction(Of TRet, T1, T2, T3, T4)(ByVal arg1 As T1, ByVal arg2 As T2, ByVal arg3 As T3, ByVal arg4 As T4) As TRet
''' <summary>Represents type-safe event handler</summary>
''' <typeparam name="TControl">Type of <paramref name="source"/></typeparam>
''' <typeparam name=" TEventArgs">Type of <paramref name="e"/></typeparam>
''' <param name="source"><see cref="System.Windows.Forms.Control"/> that caused the event</param>
''' <param name="e">Event arguments</param>
Public Delegate Sub ControlEventHandler(Of TControl As System.Windows.Forms.Control, TEventArgs As EventArgs)(ByVal source As TControl, ByVal e As TEventArgs)
''' <summary>Generic event handler delegate</summary>
''' <typeparam name="TSender">Type of the <paramref name="sender"/> argument</typeparam>
''' <typeparam name="TEventArgs">Type of the <paramref name="e"/> argument</typeparam>
''' <param name="sender">Source of the event - instance of object which fired the event</param>
''' <param name="e">Event arguments</param>
Public Delegate Sub EventHandler(Of TSender As Class, TEventArgs As EventArgs)(ByVal sender As Tsender, ByVal e As TEventArgs)
#End If