#If True
Imports System.Runtime.CompilerServices
Imports Tools.CollectionsT.GenericT

'#If Framework >= 3.5 Then
Namespace CollectionsT
    'ASAP: Mark,  Wiki, Forum
    ''' <summary>Extension methods for working with type-unsafe collections</summary>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Đonny</author>
   ''' <version version="1.5.2" stage="Nightly"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Module CollectionTools
        ''' <summary>Gets last item in collection</summary>
        ''' <param name="Collection">Collection to obtain item from</param>
        ''' <returns>Last item in <paramref name="Collection"/>, or null if <paramref name="Collection"/> is empty</returns>
        ''' <remarks>This function have to iterate through whole <paramref name="Collection"/></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>                                 
        <Extension()> _
        Public Function Last(ByVal Collection As IEnumerable) As Object
            If Collection Is Nothing Then Throw New ArgumentNullException("Collection")
            Dim current As Object = Nothing
            For Each Item In Collection
                current = Item
            Next
            Return current
        End Function
        ''' <summary>Gets last item in collection</summary>
        ''' <param name="Collection">Collection to obtain item from</param>
        ''' <returns>Last item in <paramref name="Collection"/> (item at highest index), or null if <paramref name="Collection"/> is empty</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="Collection"/> is null</exception>
        <Extension()> _
        Public Function Last(ByVal Collection As IList) As Object
            If Collection Is Nothing Then Throw New ArgumentNullException("Collection")
            If Collection.Count = 0 Then Return Nothing
            Return Collection(Collection.Count - 1)
        End Function
        ''' <summary>Gets value indicationg if given colection is empty</summary>
        ''' <param name="collection">Collection to examine</param>
        ''' <returns>True if <paramref name="collection"/>.<see cref="Collections.IEnumerable.GetEnumerator">GetEnumerator</see>.<see cref="Collections.IEnumerator.MoveNext">MoveNext</see> returns false.</returns>
        <Extension()> Public Function IsEmpty(ByVal collection As IEnumerable) As Boolean
            Return Not collection.GetEnumerator.MoveNext
        End Function
    End Module
End Namespace
'#End If
#End If