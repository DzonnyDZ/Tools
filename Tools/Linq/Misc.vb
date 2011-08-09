Imports System.Linq, System.Runtime.CompilerServices
Imports Tools.DataStructuresT.GenericT

Namespace LinqT
#If Config <= Nightly Then 'Stage:Nightly
    ''' <summary>Contains miscaleneous LINQ extensions</summary>
    ''' <version version="1.5.4">Fix: Module made public (it was internal before)</version>
    Public Module LinqExtensions
        ''' <summary>Adds all items from given collection to given dictionary</summary>
        ''' <param name="Target">Collection which is target of adding</param>
        ''' <param name="Collection">Items to be added</param>
        ''' <typeparam name="TKey">Type of keys in <paramref name="Target"/></typeparam>
        ''' <typeparam name="TValue">Type of values in <paramref name="Target"/></typeparam>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> or <paramref name="Target"/> is null</exception>
        <Extension()> _
        Public Sub AddRange(Of TKey, TValue)(ByVal Target As IDictionary(Of TKey, TValue), ByVal Collection As IEnumerable(Of KeyValuePair(Of TKey, TValue)))
            If Target Is Nothing Then Throw New ArgumentNullException("Target")
            If Collection Is Nothing Then Throw New ArgumentException("Collection")
            For Each item In Collection
                Target.Add(item.Key, item.Value)
            Next item
        End Sub
        ''' <summary>Adds all items from given collection to given dictionary</summary>
        ''' <param name="Target">Collection which is target of adding</param>
        ''' <param name="Collection">Items to be added</param>
        ''' <typeparam name="TKey">Type of keys in <paramref name="Target"/></typeparam>
        ''' <typeparam name="TValue">Type of values in <paramref name="Target"/></typeparam>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> or <paramref name="Target"/> is null</exception>
        <Extension()> _
        Public Sub AddRange(Of TKey, TValue)(ByVal Target As IDictionary(Of TKey, TValue), ByVal Collection As IEnumerable(Of IPair(Of TKey, TValue)))
            If Target Is Nothing Then Throw New ArgumentNullException("Target")
            If Collection Is Nothing Then Throw New ArgumentException("Collection")
            For Each item In Collection
                Target.Add(item.Value1, item.Value2)
            Next item
        End Sub
        ''' <summary>Gets value indicating if given object is contained among given objects</summary>
        ''' <param name="Obj">Object to search in <paramref name="List"/> for</param>
        ''' <param name="List">Objects to search for <paramref name="Obj"/> among</param>
        ''' <typeparam name="T">Type of object</typeparam>
        ''' <returns>True if <paramref name="Obj"/> is contained in <paramref name="List"/>; false otherwise</returns>
        ''' <version version="1.5.2">Function introduced</version>
        <Extension()> Function [In](Of T)(ByVal Obj As T, ByVal ParamArray List As T()) As Boolean
            Return Obj.In(DirectCast(List, IEnumerable(Of T)))
        End Function
        ''' <summary>Gets value indicating if given object is contained among given objects</summary>
        ''' <param name="Obj">Object to search in <paramref name="List"/> for</param>
        ''' <param name="List">Objects to search for <paramref name="Obj"/> among</param>
        ''' <typeparam name="T">Type of object</typeparam>
        ''' <returns>True if <paramref name="Obj"/> is contained in <paramref name="List"/>; false otherwise</returns>
        ''' <version version="1.5.2">Function introduced</version>
        <Extension()> Function [In](Of T)(ByVal Obj As T, ByVal List As IEnumerable(Of T)) As Boolean
            Return List.Contains(Obj)
        End Function

        ''' <summary>Gets value indicating if given object is not contained among given objects</summary>
        ''' <param name="Obj">Object to search in <paramref name="List"/> for</param>
        ''' <param name="List">Objects to search for <paramref name="Obj"/> among</param>
        ''' <typeparam name="T">Type of object</typeparam>
        ''' <returns>False if <paramref name="Obj"/> is contained in <paramref name="List"/>; true otherwise</returns>
        ''' <seelaso cref="[In]"/>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension()> Function [NotIn](Of T)(ByVal Obj As T, ByVal ParamArray List As T()) As Boolean
            Return Not Obj.In(List)
        End Function
        ''' <summary>Gets value indicating if given object is not contained among given objects</summary>
        ''' <param name="Obj">Object to search in <paramref name="List"/> for</param>
        ''' <param name="List">Objects to search for <paramref name="Obj"/> among</param>
        ''' <typeparam name="T">Type of object</typeparam>
        ''' <returns>False if <paramref name="Obj"/> is contained in <paramref name="List"/>; true otherwise</returns>
        ''' <seelaso cref="[In]"/>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        <Extension()> Function [NotIn](Of T)(ByVal Obj As T, ByVal List As IEnumerable(Of T)) As Boolean
            Return Not Obj.In(List)
        End Function
    End Module
#End If
End Namespace