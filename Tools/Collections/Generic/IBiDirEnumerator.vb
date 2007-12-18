#If Config <= RC Then 'Stage: RC
Namespace CollectionsT.GenericT
    'ASAP: Mark, WiKi, Forum
    ''' <summary>Provides interface of bidirectional type-safe enumerator</summary>
    ''' <typeparam name="T">Type of items to enumerate</typeparam>
    Public Interface IBiDirEnumerator(Of T)
        Inherits IEnumerator(Of T)
        ''' <summary>Moves internal pointer of enumerator to previos member of collection</summary>
        ''' <returns>True when pointer was succesfully mowed to item inside the collection. False if it was moved before start of collection or if it already was before start of collection. If pointer was after end of collection, it is moved to last item of collection and return value is true.</returns>
        Function MovePrevious() As Boolean
    End Interface

    ''' <summary>Provides interface for collections that has bidirectional enumerator</summary>
    ''' <typeparam name="T">Type of element in collection</typeparam>
    Public Interface IBiDirEnumerable(Of T)
        Inherits IEnumerable(Of T)
        ''' <summary>Gets <see cref="IBiDirEnumerator(Of T)"/> for the collection</summary>
        Shadows Function GetEnumerator() As IBiDirEnumerator(Of T)
    End Interface
End Namespace
#End If