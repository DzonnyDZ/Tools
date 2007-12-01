#If Config <= Nightly Then 'Stage: Nightly
Imports System.Runtime.CompilerServices
Imports Tools.Collections.Generic

#If VBC_VER >= 9.0 Then
Namespace CollectionsT.GenericT
    'ASAP: Mark,  Wiki, Forum
    ''' <summary>Extension methods for working with collection</summary>
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
    End Module
End Namespace
#End If
#End If