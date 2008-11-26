'Minimums and maximums
#If Config <= Release Then
''' <summary>Provides constants and static methods for trigonometric, logarithmic, and other common mathematical functions.</summary>
Partial Public Class MathT
    ''' <summary>Finds the smallest of parameters</summary>
    ''' <typeparam name="T">The type of parameters that implements <see cref="IComparable(Of T)"/></typeparam>
    ''' <param name="Numbers">Objects one of which should be the smallest found</param>
    ''' <returns>The smallest object in <paramref name="Numbers"/> array</returns>
    ''' <exception cref="ArgumentNullException">If <paramref name="Numbers"/> is Null (Nothing)</exception>
    ''' <exception cref="ArgumentException">If <paramref name="Numbers"/> contains no item</exception>
    ''' <remarks><seealso cref="Max"/></remarks>
    ''' <author www="http://dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Shared Function Min(Of T As IComparable(Of T))(ByVal ParamArray Numbers As T()) As T
        Return Min(CType(Numbers, IEnumerable(Of T)))
    End Function
    ''' <summary>Finds the smalles value in givel <see cref="IEnumerable(Of T)"/></summary>
    ''' <typeparam name="T">Type of object in <paramref name="Numbers"/>. Must implement <see cref="IComparable(Of T)"/>.</typeparam>
    ''' <param name="Numbers"><see cref="IEnumerable(Of T)"/> of objects where to search the smallest</param>
    ''' <returns>The smallest value from <paramref name="Numbers"/></returns>
    ''' <exception cref="ArgumentNullException">If <paramref name="Numbers"/> is Null (Nothing)</exception>
    ''' <exception cref="ArgumentException">If <paramref name="Numbers"/> contains no item</exception>
    ''' <remarks><seealso cref="Max"/></remarks>
    ''' <author www="http://dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Shared Function Min(Of T As IComparable(Of T))(ByVal Numbers As IEnumerable(Of T)) As T
        If Numbers Is Nothing Then Throw New ArgumentNullException("Numbers")
        Dim Current As Box(Of T) = Nothing
        For Each itm As T In Numbers
            If Current Is Nothing OrElse itm.CompareTo(Current) < 0 Then
                Current = itm
            End If
        Next itm
        If Current Is Nothing Then Throw New ArgumentException(String.Format(ResourcesT.Exceptions.MustContainAtLeastOneItem, "Numbers", 1), "Numbers")
        Return Current
    End Function
    ''' <summary>Finds the biggest of parameters</summary>
    ''' <typeparam name="T">The type of parameters that implements <see cref="IComparable(Of T)"/></typeparam>
    ''' <param name="Numbers">Objects one of which should be the biggest found</param>
    ''' <returns>The biggest object in <paramref name="Numbers"/> array</returns>
    ''' <exception cref="ArgumentNullException">If <paramref name="Numbers"/> is Null (Nothing)</exception>
    ''' <exception cref="ArgumentException">If <paramref name="Numbers"/> contains no item</exception>
    ''' <remarks><seealso cref="Min"/></remarks>
    ''' <author www="http://dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Shared Function Max(Of T As IComparable(Of T))(ByVal ParamArray Numbers As T()) As T
        Return Max(CType(Numbers, IEnumerable(Of T)))
    End Function
    ''' <summary>Finds the biggest value in givel <see cref="IEnumerable(Of T)"/></summary>
    ''' <typeparam name="T">Type of object in <paramref name="Numbers"/>. Must implement <see cref="IComparable(Of T)"/>.</typeparam>
    ''' <param name="Numbers"><see cref="IEnumerable(Of T)"/> of objects where to search the biggest</param>
    ''' <returns>The biggest value from <paramref name="Numbers"/></returns>
    ''' <exception cref="ArgumentNullException">If <paramref name="Numbers"/> is Null (Nothing)</exception>
    ''' <exception cref="ArgumentException">If <paramref name="Numbers"/> contains no item</exception>
    ''' <remarks><seealso cref="Min"/></remarks>
    ''' <author www="http://dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Release"><see cref="VersionAttribute"/> and <see cref="AuthorAttribute"/> removed</version>
    Public Shared Function Max(Of T As IComparable(Of T))(ByVal Numbers As IEnumerable(Of T)) As T
        If Numbers Is Nothing Then Throw New ArgumentNullException("Numbers")
        Dim Current As Box(Of T) = Nothing
        For Each itm As T In Numbers
            If Current Is Nothing OrElse itm.CompareTo(Current) > 0 Then
                Current = itm
            End If
        Next itm
        If Current Is Nothing Then Throw New ArgumentException("Numbers", String.Format(ResourcesT.Exceptions.MustContainAtLeastOneItem, "Numbers"))
        Return Current
    End Function
End Class
#End If
