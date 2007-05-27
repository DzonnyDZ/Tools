#If COnfig <= Release Then
Namespace VisualBasicT
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
#If Config <= Alpha Then 'Stage:Alpha
        ''' <summary>Returns item or ist alternative item depending on if item has meaningful value</summary>
        ''' <param name="value">Item to be returned if has meaningful value</param>
        ''' <param name="alternative">Alternative (fallback) item to be returned if <paramref name="value"/> has no meaningful value</param>
        ''' <returns><paramref name="value"/> if it is not null, <paramref name="alternative"/> otherwise</returns>
        ''' <typeparam name="T">Type of item</typeparam>
        <Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
        <Version(1, 0, GetType(Interaction), LastChMMDDYYYY:="05/21/2007")> _
        Public Function IfNull(Of T As Class)(ByVal value As T, ByVal alternative As T) As T
            If value Is Nothing Then Return alternative Else Return value
        End Function
        ''' <summary>Returns item or ist alternative item depending on if item has meaningful value</summary>
        ''' <param name="value">Item to be returned if has meaningful value</param>
        ''' <param name="alternative">Alternative (fallback) item to be returned if <paramref name="value"/> has no meaningful value</param>
        ''' <returns><paramref name="value"/> if it is not null and is not an empty <see cref="String"/>, <paramref name="alternative"/> otherwise</returns>
        <Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
        <Version(1, 0, GetType(Interaction), LastChMMDDYYYY:="05/21/2007")> _
        Public Function IfNull(ByVal value As String, ByVal alternative As String) As String
            If value Is Nothing OrElse value = "" Then Return alternative Else Return value
        End Function
        ''' <summary>Returns item or ist alternative item depending on if item has meaningful value</summary>
        ''' <typeparam name="T">Type of structure contained in <paramref name="value"/> and to be returned</typeparam>
        ''' <param name="value">Item to be returned if has meaningful value</param>
        ''' <param name="alternative">Alternative (fallback) item to be returned if <paramref name="value"/> has no meaningful value</param>
        ''' <returns><paramref name="value"/> if it's <see cref="Nullable(Of String).HasValue"/> is true, <paramref name="alternative"/> otherwise</returns>
        <Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
        <Version(1, 0, GetType(Interaction), LastChMMDDYYYY:="05/21/2007")> _
        Public Function IfNull(Of T As Structure)(ByVal value As Nullable(Of T), ByVal alternative As T) As T
            If value.HasValue Then Return value Else Return alternative
        End Function
        ''' <summary>Returns item or ist alternative item depending on if item has meaningful value</summary>
        ''' <typeparam name="T">Type of value to be returned</typeparam>
        ''' <param name="value">Item to be returned if has meaningful value</param>
        ''' <param name="alternative">Alternative (fallback) item to be returned if <paramref name="value"/> has no meaningful value</param>
        ''' <returns><paramref name="value"/> if it is not nothing and is not <see cref="DBNull"/>, <paramref name="alternative"/> otherwise</returns>
        ''' <exception cref="InvalidCastException">Casting from <paramref name="value"/> to <paramref name="T"/> failed</exception>
        <Author("Đonny", "dzonny@dzonny.cz", "http://dzonny.cz")> _
        <Version(1, 0, GetType(Interaction), LastChMMDDYYYY:="05/21/2007")> _
        Public Function IfNull(Of T)(ByVal value As Object, ByVal alternative As T) As T
            If value Is Nothing OrElse TypeOf value Is DBNull Then Return alternative Else Return value
        End Function
#End If
    End Module
End Namespace
#End If

