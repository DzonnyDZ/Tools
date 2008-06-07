Imports System.Runtime.CompilerServices, System.Linq, System.Collections.Generic
#If Config <= Nightly Then
Namespace LinqT
    ''' <summary>Tools for working with <see cref="IEnumerable(Of T)"/></summary>
    Public Module EnumerableT
        ''' <summary>Creates union of all given collections</summary>
        ''' <param name="collections">Collections to create union of</param>
        ''' <typeparam name="T">Type of items in collection(s)</typeparam>
        ''' <returns>Collection that contains members of all collections in <paramref name="collections"/>. If <paramref name="collections"/> is null returns an emlty collection.</returns>
        Public Function UnionAll(Of T)(ByVal ParamArray collections As IEnumerable(Of T)()) As IEnumerable(Of T)
            Return UnionAll(DirectCast(collections, IEnumerable(Of IEnumerable(Of T))))
        End Function
        ''' <summary>Creates union of all given collections</summary>
        ''' <param name="collections">Collections to create union of</param>
        ''' <typeparam name="T">Type of items in collection(s)</typeparam>
        ''' <returns>Collection that contains members of all collections in <paramref name="collections"/>. If <paramref name="collections"/> is null returns an emlty collection.</returns>
        <Extension()> Public Function UnionAll(Of T)(ByVal collections As IEnumerable(Of IEnumerable(Of T))) As IEnumerable(Of T)
            Dim ret As IEnumerable(Of T) = New List(Of T)
            If collections Is Nothing Then Return ret
            For Each item In collections
                ret = ret.Union(item)
            Next item
            Return ret
        End Function
        ''' <summary>Creates union of given collection with other given collections</summary>
        ''' <param name="collection">Firts collection for union</param>
        ''' <param name="OtherCollections">Other collections for union</param>
        ''' <typeparam name="T">Type of mmber in collection(s)</typeparam>
        ''' <returns>Collection that contains members of <paramref name="collection"/> as well as of all items in <paramref name="OtherCollections"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="collection"/> is null</exception>
        <Extension()> Public Function UnionAll(Of T)(ByVal collection As IEnumerable(Of T), ByVal ParamArray OtherCollections As IEnumerable(Of T)()) As IEnumerable(Of T)
            Return UnionAll(collection, DirectCast(OtherCollections, IEnumerable(Of IEnumerable(Of T))))
        End Function
        ''' <summary>Creates union of given collection with other given collections</summary>
        ''' <param name="collection">Firts collection for union</param>
        ''' <param name="OtherCollections">Other collections for union</param>
        ''' <typeparam name="T">Type of mmber in collection(s)</typeparam>
        ''' <returns>Collection that contains members of <paramref name="collection"/> as well as of all items in <paramref name="OtherCollections"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="collection"/> is null</exception>
        <Extension()> Public Function UnionAll(Of T)(ByVal collection As IEnumerable(Of T), ByVal OtherCollections As IEnumerable(Of IEnumerable(Of T))) As IEnumerable(Of T)
            If collection Is Nothing Then Throw New ArgumentNullException("collection")
            Return collection.Union(UnionAll(OtherCollections))
        End Function
        ''' <summary>Gets value indicating if given collection is empty</summary>
        ''' <param name="collection">Collection to check emptyness of</param>
        ''' <returns>True if first element of collection cannot be enumerated using <paramref name="collection"/>.<see cref="System.Collections.Generic.IEnumerable.GetEnumerator">GetEnumerator</see>.<see cref="IEnumerator.MoveNext">MoveNext</see>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="collection"/> is null</exception>
        <Extension()> Public Function IsEmpty(ByVal collection As IEnumerable) As Boolean
            If collection Is Nothing Then Throw New ArgumentNullException("collection")
            Dim enumerator = collection.GetEnumerator
            Return Not enumerator.MoveNext
        End Function
    End Module
End Namespace
#End If

