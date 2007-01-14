Namespace MyGeneric
    Public Interface KeyAble(Of TKey)
        ReadOnly Property Key() As TKey
    End Interface
    Class KeyedDictionary(Of TKey, TValue As KeyAble(Of TKey))
        Inherits Dictionary(Of TKey, TValue)
        Sub New()
        End Sub
        Sub New(ByVal comparer As System.Collections.Generic.IEqualityComparer(Of TKey))
            MyBase.New(comparer)
        End Sub
        Overloads Sub Add(ByVal value As TValue)
            Add(value.Key, value)
        End Sub
    End Class
End Namespace
