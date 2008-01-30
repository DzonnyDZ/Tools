Imports System.Runtime.CompilerServices
''' <summary>Misc tools</summary>
<HideModuleName()> Public Module _misc
    ''' <summary>Returns given object</summary>
    ''' <param name="obj">Object to return</param>
    ''' <typeparam name="T">Type of <paramref name="obj"/></typeparam>
    ''' <returns><paramref name="obj"/></returns>
    ''' <remarks>Somebody can think taht function that returns object itsekf in nonsense. But it is usefull with languages as VB which have <c>With</c> construct. Using this function, you can objein object itself via <c>.self</c> inside <c>With</c> conetc</remarks>
    <Extension()> Public Function self(Of T)(ByVal obj As T) As T
        Return obj
    End Function
End Module