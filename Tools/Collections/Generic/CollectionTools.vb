#If Config <= Nightly Then 'Stage: Nightly
Imports System.Runtime.CompilerServices
Imports Tools.CollectionsT.GenericT

#If Framework >= 3.5 Then
Namespace CollectionsT.GenericT
    'ASAP: Mark,  Wiki, Forum
    ''' <summary>Extension methods for working with generic collections</summary>
    Public Module CollectionTools
        ''' <summary>Gets type-safe bidirectional enumerator of an array</summary>
        ''' <param name="Array">Array to get enumerator for</param>
        ''' <typeparam name="T">Type of elements in array</typeparam>
        ''' <returns>New <see cref="TypedArrayEnumerator(Of T)"/></returns>
        <Extension()> _
        Public Function GetTypedEnumerator(Of T)(ByVal Array As T()) As TypedArrayEnumerator(Of T)
            Return New TypedArrayEnumerator(Of T)(Array)
        End Function
        ''' <summary>Gets type-safe bidirectional enumerator of an array</summary>
        ''' <param name="Array">Array to get enumerator for</param>
        ''' <typeparam name="T">Type of elements in array</typeparam>
        ''' <param name="Inverse">Makes enumerator to nemumerated from last to first item using <see cref="System.Collections.IEnumerator.MoveNext"/>.</param>
        ''' <returns>New <see cref="TypedArrayEnumerator(Of T)"/></returns>
        <Extension()> _
        Public Function GetTypedEnumerator(Of T)(ByVal Array As T(), ByVal Inverse As Boolean) As TypedArrayEnumerator(Of T)
            Return New TypedArrayEnumerator(Of T)(Array, Inverse)
        End Function
        ''' <summary>Gets last item in collection</summary>
        ''' <param name="Collection">Collection to obtain item from</param>
        ''' <typeparam name="T">Type of items in collection</typeparam>
        ''' <returns>Last item in <paramref name="Collection"/>, or null if <paramref name="Collection"/> is empty</returns>
        ''' <remarks>This function have to iterate through whole <paramref name="Collection"/></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>                                 
        <Extension()> _
        Public Function Last(Of T)(ByVal Collection As IEnumerable(Of T)) As T
            If Collection Is Nothing Then Throw New ArgumentNullException("Collection")
            Dim current As T = Nothing
            For Each Item In Collection
                current = Item
            Next
            Return current
        End Function
        ''' <summary>Gets last item in collection</summary>
        ''' <param name="Collection">Collection to obtain item from</param>
        ''' <typeparam name="T">Type of items in collection</typeparam>
        ''' <returns>Last item in <paramref name="Collection"/> (item at highest index), or null if <paramref name="Collection"/> is empty</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> _
        Public Function Last(Of T)(ByVal Collection As IList(Of T)) As T
            If Collection Is Nothing Then Throw New ArgumentNullException("Collection")
            If Collection.Count = 0 Then Return Nothing
            Return Collection(Collection.Count - 1)
        End Function
        ''' <summary>Gets last item in collection</summary>
        ''' <param name="Collection">Collection to obtain item from</param>
        ''' <typeparam name="T">Type of items in collection</typeparam>
        ''' <returns>Last item in <paramref name="Collection"/> (item at highest index), or null if <paramref name="Collection"/> is empty</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> _
        Public Function Last(Of T)(ByVal Collection As T()) As T
            If Collection Is Nothing Then Throw New ArgumentNullException("Collection")
            If Collection.Length = 0 Then Return Nothing
            Return Collection(Collection.GetUpperBound(0))
        End Function
        ''' <summary>Inserts all items from one collection to another</summary>
        ''' <param name="Collection">Collection to insert items to</param>
        ''' <param name="Items">Items to be inserted</param>
        ''' <typeparam name="T">Type of item</typeparam>
        <Extension()> _
        Public Sub AddRange(Of T)(ByVal Collection As ICollection(Of T), ByVal Items As IEnumerable(Of T))
            For Each item In Items
                Collection.Add(item)
            Next
        End Sub
        ''' <summary>Inserts all items from one collection to another</summary>
        ''' <param name="Collection">Collection to insert items to</param>
        ''' <param name="Items">Items to be inserted</param>
        ''' <typeparam name="T">Type of item</typeparam>
        <Extension()> _
        Public Sub AddRange(Of T)(ByVal Collection As IAddable(Of T), ByVal Items As IEnumerable(Of T))
            For Each item In Items
                Collection.Add(item)
            Next
        End Sub
    End Module
End Namespace
#End If
#End If