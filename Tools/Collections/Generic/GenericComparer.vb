#If True
Namespace CollectionsT.GenericT
    ''' <summary>Delegate-backed comparer</summary>
    ''' <typeparam name="T">Tpe to campare</typeparam>
    ''' <version version="1.5.2" stage="Nightly">Class introduced</version>
    Public NotInheritable Class GenericComparer(Of T)
        Implements IComparer(Of T)
        ''' <summary>CTor</summary>
        ''' <param name="Comparer">Comparing delegte</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Comparer"/> is null</exception>
        Public Sub New(ByVal Comparer As Func(Of T, T, Integer))
            If Comparer Is Nothing Then Throw New ArgumentNullException("Comparer")
            Me.Comparer = Comparer
        End Sub
        ''' <summary>Comparing delegate</summary>
        Private Comparer As Func(Of T, T, Integer)
        ''' <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
        ''' <param name="x">The first object to compare.</param>
        ''' <param name="y">The second object to compare.</param>
        ''' <returns>Value Condition Less than zero x is less than y.  Zero x equals y.  Greater than zero x is greater than y.</returns>
        Public Function Compare(ByVal x As T, ByVal y As T) As Integer Implements System.Collections.Generic.IComparer(Of T).Compare
            Return Comparer.Invoke(x, y)
        End Function
    End Class
End Namespace
#End If