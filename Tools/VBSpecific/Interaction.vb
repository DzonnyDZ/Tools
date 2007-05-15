#If COnfig <= Release Then
Namespace VisualBasic
    ''' <summary>The Interaction module contains procedures used to interact with objects, applications, and systems.</summary>
    <DoNotApplyAuthorAndVersionAttributes()> _
    Public Module Interaction
        ''' <summary>Returns one of two objects, depending on the evaluation of an expression.</summary>
        ''' <param name="Expression">The expression you want to evaluate.</param>
        ''' <param name="FalsePart">Returned if <paramref name="Expression">Expression</paramref> evaluates to False.</param>
        ''' <param name="TruePart">Returned if <paramref name="Expression">Expression</paramref> evaluates to True.</param>
        ''' <returns>Returns one of two objects, depending on the evaluation of an <paramref name="Expression"/>.</returns>
        ''' <typeparam name="T">The type of object to return.</typeparam>
        <Author("Đonny", "dzony.dz@gmail.com"), Version(1, 0, GetType(Interaction), LastChMMDDYYYY:="12/20/2006")> _
        Public Function iif(Of T)(ByVal Expression As Boolean, ByVal TruePart As T, ByVal FalsePart As T) As T
            If Expression Then Return TruePart Else Return FalsePart
        End Function
    End Module
End Namespace
#End If

