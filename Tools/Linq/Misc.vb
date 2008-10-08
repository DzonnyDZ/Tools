Imports System.Linq, System.Runtime.CompilerServices
Imports Tools.DataStructuresT.GenericT

Namespace LinqT
#If Config <= Nightly Then 'Stage:Nightly
    ''' <summary>Contains miscaleneous LINQ extensions</summary>
    Module LinqExtensions
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
    End Module
#End If
End Namespace