Imports System.Runtime.CompilerServices, System.Linq, System.Collections.Generic
#If Config <= Nightly Then
Namespace LinqT
    ''' <summary>Tools for working with <see cref="IEnumerable(Of T)"/></summary>
    Public Module EnumerableT
        ''' <summary>Creates union of all given collections</summary>
        ''' <param name="collections">Collections to create union of</param>
        ''' <typeparam name="T">Type of items in collection(s)</typeparam>
        ''' <returns>Collection that contains members of all collections in <paramref name="collections"/>. If <paramref name="collections"/> is null returns an emlty collection.</returns>
        ''' <remarks>Unification is done immediatelly.</remarks>
        ''' <seelaso cref="FlatAllDeffered"/>
        Public Function UnionAll(Of T)(ByVal ParamArray collections As IEnumerable(Of T)()) As IEnumerable(Of T)
            Return UnionAll(DirectCast(collections, IEnumerable(Of IEnumerable(Of T))))
        End Function
        ''' <summary>Creates union of all given collections</summary>
        ''' <param name="collections">Collections to create union of</param>
        ''' <typeparam name="T">Type of items in collection(s)</typeparam>
        ''' <returns>Collection that contains all members of all collections in <paramref name="collections"/>. If <paramref name="collections"/> is null returns an emlty collection.</returns>
        ''' <remarks>Unification is done immediatelly.</remarks>
        ''' <seelaso cref="FlatAllDeffered"/>
        <Extension()> Public Function UnionAll(Of T)(ByVal collections As IEnumerable(Of IEnumerable(Of T))) As IEnumerable(Of T)
            Dim ret As IEnumerable(Of T) = New List(Of T)
            If collections Is Nothing Then Return ret
            For Each item In collections
                ret = ret.Union(item)
            Next item
            Return ret
        End Function
        ''' <summary>Creates union of all given collections</summary>
        ''' <param name="collections">Collections to create union of</param>
        ''' <typeparam name="T">Type of items in collection(s)</typeparam>
        ''' <returns>Collection that contains all members of all collections in <paramref name="collections"/>. If <paramref name="collections"/> is null returns an emlty collection.</returns>
        ''' <remarks>This is alias of <see cref="UnionAll"/> which takes one parameter.
        ''' <para>Unification is done immediatelly.</para></remarks>
        ''' <seelaso cref="FlatAllDeffered"/>
        ''' <seelaso cref="UnionAll"/>
        <Extension()> Public Function FlatAll(Of T)(ByVal collections As IEnumerable(Of IEnumerable(Of T))) As IEnumerable(Of T)
            Return UnionAll(collections)
        End Function
        ''' <summary>Creates union of all geiven colections</summary>
        ''' <param name="collections">Collections to create union of</param>
        ''' <typeparam name="T">Type of items in collections</typeparam>
        ''' <returns><see cref="UnionEnumerable(Of T)"/> over <paramref name="collections"/></returns>
        ''' <remarks>Unioning is deffered to time when collections are iterated</remarks>
        ''' <version version="1.5.2">Function introduced</version>
        <Extension()> Public Function FlatAllDeffered(Of T)(ByVal collections As IEnumerable(Of IEnumerable(Of T))) As IEnumerable(Of T)
            Return New UnionEnumerable(Of T)(collections)
        End Function
        ''' <summary>Unions all unique items in given collections to one collection</summary>
        ''' <param name="collections">Collections to create union of</param>
        ''' <typeparam name="T">Type of items in collection(s)</typeparam>
        ''' <returns>Collection that contains all unique members of collections in <paramref name="collections"/></returns>
        <Extension()> Public Function FlatDistinct(Of T)(ByVal collections As IEnumerable(Of IEnumerable(Of T))) As IEnumerable(Of T)
            Dim ret As New List(Of T)
            If collections Is Nothing Then Return ret
            For Each collection In collections
                For Each item In collection
                    If Not ret.Contains(item) Then ret.Add(item)
                Next
            Next
            Return ret
        End Function
        ''' <summary>Unions all unique items in given collections to one collection using given <see cref="IComparer(Of T)"/></summary>
        ''' <param name="collections">Collections to create union of</param>
        ''' <typeparam name="T">Type of items in collection(s)</typeparam>
        ''' <returns>Collection that contains all unique members of collections in <paramref name="collections"/></returns>
        ''' <param name="comparer">Comparer used for distinguishing unique items</param>
        <Extension()> Public Function FlatDistinct(Of T)(ByVal collections As IEnumerable(Of IEnumerable(Of T)), ByVal comparer As IEqualityComparer(Of T)) As IEnumerable(Of T)
            If comparer Is Nothing Then Throw New ArgumentNullException("comparer")
            Dim ret As New List(Of T)
            If collections Is Nothing Then Return ret
            For Each collection In collections
                For Each item In collection
                    If Not ret.Contains(item, comparer) Then ret.Add(item)
                Next
            Next
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
        ''' <summary>Gets value indicating if given collection is non-empty</summary>
        ''' <param name="collection">Collection to check non-emptyness of</param>
        ''' <returns>True if first element of collection can be enumerated using <paramref name="collection"/>.<see cref="System.Collections.Generic.IEnumerable.GetEnumerator">GetEnumerator</see>.<see cref="IEnumerator.MoveNext">MoveNext</see>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="collection"/> is null</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        <Extension()> Public Function Exists(ByVal collection As IEnumerable) As Boolean
            Return Not collection.IsEmpty
        End Function
    End Module
End Namespace
#End If

