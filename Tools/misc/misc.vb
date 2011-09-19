Imports System.Runtime.CompilerServices
''' <summary>Misc tools</summary>
<HideModuleName()>
Public Module misc_
    ''' <summary>Returns given object</summary>
    ''' <param name="obj">Object to return</param>
    ''' <typeparam name="T">Type of <paramref name="obj"/></typeparam>
    ''' <returns><paramref name="obj"/></returns>
    ''' <remarks>Somebody can think taht function that returns object itsekf in nonsense. But it is usefull with languages as VB which have <c>With</c> construct. Using this function, you can objein object itself via <c>.self</c> inside <c>With</c> conetc</remarks>
    <Extension()> Public Function self(Of T)(ByVal obj As T) As T
        Return obj
    End Function
    ''' <summary>Swaps values of given variables</summary>
    ''' <typeparam name="T">Type of variables</typeparam>
    ''' <param name="a">Variable 1</param>
    ''' <param name="b">Variable 2</param>
    ''' <version version="1.5.2">Method moved from <c>Tools.Experimantal.Utils</c> to <see cref="misc_"/>.</version>
    Public Sub Swap(Of T)(ByRef a As T, ByRef b As T)
        Dim tmp As T
        tmp = a
        a = b
        b = tmp
    End Sub

#Region "Switch"
#Region "General"
    ''' <summary>Returns one of results depending on boolean conditions</summary>
    ''' <param name="conditions">Conditions to test</param>
    ''' <param name="results">Results to swich among</param>
    ''' <param name="ThrowOnError">True to throw an <see cref="ArgumentException"/> when none of <paramref name="conditions"/> is true</param>
    ''' <typeparam name="T">Type of <paramref name="results"/> items and return value</typeparam>
    ''' <returns>Item from <paramref name="results"/> at position of first condition from <paramref name="conditions"/> which is true</returns>
    ''' <exception cref="ArgumentException"><paramref name="results"/> contains less items than is orinar number of firts condtition which is true (simply: enumeration of <paramref name="results"/> reaches the end of collection before first true condition is found) =or= None of <paramref name="conditions"/> evaluates to true and <paramref name="ThrowOnError"/> is true.</exception>
    Public Function Switch(Of T)(ByVal conditions As IEnumerable(Of Boolean), ByVal results As IEnumerable(Of T), Optional ByVal ThrowOnError As Boolean = True) As T
        Return Switch(Of T, Boolean)(True, conditions, results, ThrowOnError)
    End Function
    ''' <summary>Returns one of results depending on conditions compared to given value</summary>
    ''' <param name="conditions">Conditions to test agains <paramref name="Value"/></param>
    ''' <param name="value">Value to compare <paramref name="conditions"/> with</param>
    ''' <param name="results">Results to swich among</param>
    ''' <param name="ThrowOnError">True to throw an <see cref="ArgumentException"/> when none of <paramref name="conditions"/> equals to <paramref name="Value"/></param>
    ''' <typeparam name="T">Type of <paramref name="results"/> items and return value</typeparam>
    ''' <typeparam name="TC">Type of conditions</typeparam>
    ''' <returns>Item from <paramref name="results"/> at position of first condition from <paramref name="conditions"/> which equals to <paramref name="Value"/></returns>
    ''' <exception cref="ArgumentException"><paramref name="results"/> contains less items than is orinar number of firts condtition which equals to <paramref name="Value"/> (simply: enumeration of <paramref name="results"/> reaches the end of collection before first condition which eqauls to <paramref name="Value"/> is found) =or= None of <paramref name="conditions"/> equals to <paramref name="Value"/> and <paramref name="ThrowOnError"/> is true.</exception>
    Public Function Switch(Of T, TC)(ByVal value As TC, ByVal conditions As IEnumerable(Of TC), ByVal results As IEnumerable(Of T), Optional ByVal ThrowOnError As Boolean = True) As T
        Dim cE = conditions.GetEnumerator
        Dim rE = results.GetEnumerator
        Dim eqc = EqualityComparer(Of TC).Default
        While cE.MoveNext
            If Not rE.MoveNext Then Throw New ArgumentException(ResourcesT.Exceptions.ThereIsNotEnoughtResults, "results")
            If eqc.Equals(cE.Current, value) Then Return rE.Current
        End While
        If ThrowOnError Then Throw New ArgumentException(ResourcesT.Exceptions.NoConditionWasTrue, "conditions")
        Return Nothing
    End Function
    ''' <summary>Returns one of results depending on conditions compared to given value</summary>
    ''' <param name="conditions">Conditions to test agains <paramref name="Value"/></param>
    ''' <param name="value">Value to compare <paramref name="conditions"/> with</param>
    ''' <param name="results">Results to swich among</param>
    ''' <typeparam name="T">Type of <paramref name="results"/> items and return value</typeparam>
    ''' <returns>Item from <paramref name="results"/> at position of first condition from <paramref name="conditions"/> which equals to <paramref name="Value"/></returns>
    ''' <exception cref="ArgumentException"><paramref name="results"/> contains less items than is orinar number of firts condtition which is true (simply: enumeration of <paramref name="results"/> reaches the end of collection before first condition which equals to <paramref name="value"/> is found) =or= None of <paramref name="conditions"/> equals to <paramref name="value"/>.</exception>
    Public Function Switch(Of T, TC)(ByVal value As TC, ByVal results As IEnumerable(Of T), ByVal ParamArray conditions As TC()) As T
        Return Switch(Of T, TC)(value, DirectCast(conditions, IEnumerable(Of TC)), results, True)
    End Function
    ''' <summary>Returns one of results depending on boolean conditions</summary>
    ''' <param name="conditions">Conditions to test</param>
    ''' <param name="results">Results to swich among</param>
    ''' <typeparam name="T">Type of <paramref name="results"/> items and return value</typeparam>
    ''' <returns>Item from <paramref name="results"/> at position of first condition from <paramref name="conditions"/> which is true</returns>
    ''' <exception cref="ArgumentException"><paramref name="results"/> contains less items than is orinar number of firts condtition which is true (simply: enumeration of <paramref name="results"/> reaches the end of collection before first true condition is found) =or= None of <paramref name="conditions"/> evaluates to true.</exception>
    Public Function Switch(Of T)(ByVal results As IEnumerable(Of T), ByVal ParamArray conditions As Boolean()) As T
        Return Switch(Of T)(DirectCast(conditions, IEnumerable(Of Boolean)), results, True)
    End Function
    ''' <summary>Returns one of results depending on boolean conditions</summary>
    ''' <param name="conditions">Conditions to test</param>
    ''' <param name="results">Results to swich among</param>
    ''' <typeparam name="T">Type of <paramref name="results"/> items and return value</typeparam>
    ''' <returns>Item from <paramref name="results"/> at position of first condition from <paramref name="conditions"/> which is true</returns>
    ''' <exception cref="ArgumentException"><paramref name="results"/> contains less items than is orinar number of firts condtition which is true (simply: enumeration of <paramref name="results"/> reaches the end of collection before first true condition is found) =or= None of <paramref name="conditions"/> evaluates to true.</exception>
    Public Function Switch(Of T)(ByVal conditions As IEnumerable(Of Boolean), ByVal ParamArray results As T()) As T
        Return Switch(Of T)(conditions, DirectCast(results, IEnumerable(Of T)), True)
    End Function
    ''' <summary>Returns one of results depending on conditions compared to given value</summary>
    ''' <param name="conditions">Conditions to test agains <paramref name="Value"/></param>
    ''' <param name="value">Value to compare <paramref name="conditions"/> with</param>
    ''' <param name="results">Results to swich among</param>
    ''' ''' <typeparam name="TC">Type of conditions</typeparam>
    ''' <typeparam name="T">Type of <paramref name="results"/> items and return value</typeparam>
    ''' <returns>Item from <paramref name="results"/> at position of first condition from <paramref name="conditions"/> which equals to <paramref name="Value"/></returns>
    ''' <exception cref="ArgumentException"><paramref name="results"/> contains less items than is orinar number of firts condtition which is true (simply: enumeration of <paramref name="results"/> reaches the end of collection before first condition which equals to <paramref name="value"/> is found) =or= None of <paramref name="conditions"/> equals to <paramref name="value"/>.</exception>
    Public Function Switch(Of T, TC)(ByVal value As TC, ByVal conditions As IEnumerable(Of TC), ByVal ParamArray results As T()) As T
        Return Switch(Of T, TC)(value, conditions, DirectCast(results, IEnumerable(Of T)), True)
    End Function
#End Region
#Region "c1, c2, ..., r1, r2, ..."
    ''' <summary>Returns one of results depending on boolean conditions (2 conditions condition-condition-result-result order of parameters)</summary>
    ''' <param name="c1">Condition 1</param>
    ''' <param name="c2">Condition 2</param>
    ''' <param name="r1">Result 1 (returned when <paramref name="c1"/> is true)</param>
    ''' <param name="r2">Result 2 (returned when <paramref name="c2"/> is true)</param>
    ''' <typeparam name="T">Type of <paramref name="results"/> items and return value</typeparam>
    ''' <returns>The one of results with number of first condition which is true</returns>
    ''' <exception cref="ArgumentException">None of <paramref name="conditions"/> evaluates to true and <paramref name="ThrowOnError"/> is true.</exception>
    Public Function Switch(Of T)(ByVal c1 As Boolean, ByVal c2 As Boolean, ByVal r1 As T, ByVal r2 As T, Optional ByVal ThrowOnError As Boolean = True) As T
        Return Switch(Of T)(New Boolean() {c1, c2}, New T() {r1, r2}, ThrowOnError)
    End Function
    ''' <summary>Returns one of results depending on boolean conditions (3 conditions condition-condition-result-result order of parameters)</summary>
    ''' <param name="c1">Condition 1</param>
    ''' <param name="c2">Condition 2</param>
    ''' <param name="c3">Condition 3</param>
    ''' <param name="r1">Result 1 (returned when <paramref name="c1"/> is true)</param>
    ''' <param name="r2">Result 2 (returned when <paramref name="c2"/> is true)</param>
    ''' <param name="r3">Result 3 (returned when <paramref name="c3"/> is true)</param>
    ''' <typeparam name="T">Type of <paramref name="results"/> items and return value</typeparam>
    ''' <returns>The one of results with number of first condition which is true</returns>
    ''' <exception cref="ArgumentException">None of <paramref name="conditions"/> evaluates to true and <paramref name="ThrowOnError"/> is true.</exception>
    Public Function Switch(Of T)(ByVal c1 As Boolean, ByVal c2 As Boolean, ByVal c3 As Boolean, ByVal r1 As T, ByVal r2 As T, ByVal r3 As T, Optional ByVal ThrowOnError As Boolean = True) As T
        Return Switch(Of T)(New Boolean() {c1, c2, c3}, New T() {r1, r2, r3}, ThrowOnError)
    End Function
    ''' <summary>Returns one of results depending on boolean conditions (4 conditions condition-condition-result-result order of parameters)</summary>
    ''' <param name="c1">Condition 1</param>
    ''' <param name="c2">Condition 2</param>
    ''' <param name="c3">Condition 3</param>
    ''' <param name="c4">Condition 4</param>
    ''' <param name="r1">Result 1 (returned when <paramref name="c1"/> is true)</param>
    ''' <param name="r2">Result 2 (returned when <paramref name="c2"/> is true)</param>
    ''' <param name="r3">Result 3 (returned when <paramref name="c3"/> is true)</param>
    ''' <param name="r4">Result 4 (returned when <paramref name="c4"/> is true)</param>
    ''' <typeparam name="T">Type of <paramref name="results"/> items and return value</typeparam>
    ''' <returns>The one of results with number of first condition which is true</returns>
    ''' <exception cref="ArgumentException">None of <paramref name="conditions"/> evaluates to true and <paramref name="ThrowOnError"/> is true.</exception>
    Public Function Switch(Of T)(ByVal c1 As Boolean, ByVal c2 As Boolean, ByVal c3 As Boolean, ByVal c4 As Boolean, ByVal r1 As T, ByVal r2 As T, ByVal r3 As T, ByVal r4 As T, Optional ByVal ThrowOnError As Boolean = True) As T
        Return Switch(Of T)(New Boolean() {c1, c2, c3, c4}, New T() {r1, r2, r3, r4}, ThrowOnError)
    End Function
#End Region
#Region "c1, r1, c2, r2, ..."
    ''' <summary>Returns one of results depending on boolean conditions (2 conditions condition-result-condition-resul order of parameters)</summary>
    ''' <param name="c1">Condition 1</param>
    ''' <param name="r1">Result 1 (returned when <paramref name="c1"></paramref> is true)</param>
    ''' <param name="c2">Condition 2</param>
    ''' <param name="r2">Result 2 (returned when <paramref name="c2"></paramref> is true)</param>
    ''' <typeparam name="T">Type of <paramref name="results"></paramref> items and return value</typeparam>
    ''' <returns>The one of results with number of first condition which is true</returns>
    ''' <exception cref="ArgumentException">None of <paramref name="conditions"></paramref> evaluates to true and <paramref name="ThrowOnError"></paramref> is true.</exception>
    Public Function Switch(Of T)(ByVal c1 As Boolean, ByVal r1 As T, ByVal c2 As Boolean, ByVal r2 As T, Optional ByVal ThrowOnError As Boolean = True) As T
        Return Switch(Of T)(New Boolean() {c1, c2}, New T() {r1, r2}, ThrowOnError)
    End Function
    ''' <summary>Returns one of results depending on boolean conditions (3 conditions condition-result-condition-resul order of parameters)</summary>
    ''' <param name="c1">Condition 1</param>
    ''' <param name="r1">Result 1 (returned when <paramref name="c1"></paramref> is true)</param>
    ''' <param name="c2">Condition 2</param>
    ''' <param name="r2">Result 2 (returned when <paramref name="c2"></paramref> is true)</param>
    ''' <param name="c3">Condition 3</param>
    ''' <param name="r3">Result 3 (returned when <paramref name="c3"></paramref> is true)</param>
    ''' <typeparam name="T">Type of <paramref name="results"></paramref> items and return value</typeparam>
    ''' <returns>The one of results with number of first condition which is true</returns>
    ''' <exception cref="ArgumentException">None of <paramref name="conditions"></paramref> evaluates to true and <paramref name="ThrowOnError"></paramref> is true.</exception>
    Public Function Switch(Of T)(ByVal c1 As Boolean, ByVal r1 As T, ByVal c2 As Boolean, ByVal r2 As T, ByVal c3 As Boolean, ByVal r3 As T, Optional ByVal ThrowOnError As Boolean = True) As T
        Return Switch(Of T)(New Boolean() {c1, c2, c3}, New T() {r1, r2, r3}, ThrowOnError)
    End Function
    ''' <summary>Returns one of results depending on boolean conditions (4 conditions condition-result-condition-resul order of parameters)</summary>
    ''' <param name="c1">Condition 1</param>
    ''' <param name="r1">Result 1 (returned when <paramref name="c1"></paramref> is true)</param>
    ''' <param name="c2">Condition 2</param>
    ''' <param name="r2">Result 2 (returned when <paramref name="c2"></paramref> is true)</param>
    ''' <param name="c3">Condition 3</param>
    ''' <param name="r3">Result 3 (returned when <paramref name="c3"></paramref> is true)</param>
    ''' <param name="c4">Condition 4</param>
    ''' <param name="r4">Result 4 (returned when <paramref name="c4"></paramref> is true)</param>
    ''' <typeparam name="T">Type of <paramref name="results"></paramref> items and return value</typeparam>
    ''' <returns>The one of results with number of first condition which is true</returns>
    ''' <exception cref="ArgumentException">None of <paramref name="conditions"></paramref> evaluates to true and <paramref name="ThrowOnError"></paramref> is true.</exception>
    Public Function Switch(Of T)(ByVal c1 As Boolean, ByVal r1 As T, ByVal c2 As Boolean, ByVal r2 As T, ByVal c3 As Boolean, ByVal r3 As T, ByVal c4 As Boolean, ByVal r4 As T, Optional ByVal ThrowOnError As Boolean = True) As T
        Return Switch(Of T)(New Boolean() {c1, c2, c3, c4}, New T() {r1, r2, r3, r4}, ThrowOnError)
    End Function
#End Region

#Region "c1, c2, ..., r1, r2, ..."
    ''' <summary>Returns one of results depending on condition compared to given values (2 conditions condition-condition-result-result order of parameters)</summary>
    ''' <param name="c1">Condition 1</param><param name="value">Value to compare conditions with</param>
    ''' <param name="c2">Condition 2</param>
    ''' <param name="r1">Result 1 (returned when <paramref name="c1"/> is true)</param>
    ''' <param name="r2">Result 2 (returned when <paramref name="c2"/> is true)</param>
    ''' <typeparam name="T">Type of <paramref name="results"/> items and return value</typeparam><typeparam name="TC">Type of condition</typeparam>
    ''' <returns>The one of results with number of first condition which is true</returns>
    ''' <exception cref="ArgumentException">None of <paramref name="conditions"/> evaluates to true and <paramref name="ThrowOnError"/> is true.</exception>
    Public Function Switch(Of T, TC)(ByVal value As TC, ByVal c1 As TC, ByVal c2 As TC, ByVal r1 As T, ByVal r2 As T, Optional ByVal ThrowOnError As Boolean = True) As T
        Return Switch(Of T, TC)(value, New TC() {c1, c2}, New T() {r1, r2}, ThrowOnError)
    End Function
    ''' <summary>Returns one of results depending on condition compared to given values (3 conditions condition-condition-result-result order of parameters)</summary>
    ''' <param name="c1">Condition 1</param><param name="value">Value to compare conditions with</param>
    ''' <param name="c2">Condition 2</param>
    ''' <param name="c3">Condition 3</param>
    ''' <param name="r1">Result 1 (returned when <paramref name="c1"/> is true)</param>
    ''' <param name="r2">Result 2 (returned when <paramref name="c2"/> is true)</param>
    ''' <param name="r3">Result 3 (returned when <paramref name="c3"/> is true)</param>
    ''' <typeparam name="T">Type of <paramref name="results"/> items and return value</typeparam><typeparam name="TC">Type of condition</typeparam>
    ''' <returns>The one of results with number of first condition which is true</returns>
    ''' <exception cref="ArgumentException">None of <paramref name="conditions"/> evaluates to true and <paramref name="ThrowOnError"/> is true.</exception>
    Public Function Switch(Of T, TC)(ByVal value As TC, ByVal c1 As TC, ByVal c2 As TC, ByVal c3 As TC, ByVal r1 As T, ByVal r2 As T, ByVal r3 As T, Optional ByVal ThrowOnError As Boolean = True) As T
        Return Switch(Of T, TC)(value, New TC() {c1, c2, c3}, New T() {r1, r2, r3}, ThrowOnError)
    End Function
    ''' <summary>Returns one of results depending on condition compared to given values (4 conditions condition-condition-result-result order of parameters)</summary>
    ''' <param name="c1">Condition 1</param><param name="value">Value to compare conditions with</param>
    ''' <param name="c2">Condition 2</param>
    ''' <param name="c3">Condition 3</param>
    ''' <param name="c4">Condition 4</param>
    ''' <param name="r1">Result 1 (returned when <paramref name="c1"/> is true)</param>
    ''' <param name="r2">Result 2 (returned when <paramref name="c2"/> is true)</param>
    ''' <param name="r3">Result 3 (returned when <paramref name="c3"/> is true)</param>
    ''' <param name="r4">Result 4 (returned when <paramref name="c4"/> is true)</param>
    ''' <typeparam name="T">Type of <paramref name="results"/> items and return value</typeparam><typeparam name="TC">Type of condition</typeparam>
    ''' <returns>The one of results with number of first condition which is true</returns>
    ''' <exception cref="ArgumentException">None of <paramref name="conditions"/> evaluates to true and <paramref name="ThrowOnError"/> is true.</exception>
    Public Function Switch(Of T, TC)(ByVal value As TC, ByVal c1 As TC, ByVal c2 As TC, ByVal c3 As TC, ByVal c4 As TC, ByVal r1 As T, ByVal r2 As T, ByVal r3 As T, ByVal r4 As T, Optional ByVal ThrowOnError As Boolean = True) As T
        Return Switch(Of T, TC)(value, New TC() {c1, c2, c3, c4}, New T() {r1, r2, r3, r4}, ThrowOnError)
    End Function
#End Region
#Region "c1, r1, c2, r2, ..."
    ''' <summary>Returns one of results depending on condition compared to given values (2 conditions condition-result-condition-resul order of parameters)</summary>
    ''' <param name="c1">Condition 1</param><param name="value">Value to compare conditions with</param>
    ''' <param name="r1">Result 1 (returned when <paramref name="c1"></paramref> is true)</param>
    ''' <param name="c2">Condition 2</param>
    ''' <param name="r2">Result 2 (returned when <paramref name="c2"></paramref> is true)</param>
    ''' <typeparam name="T">Type of <paramref name="results"></paramref> items and return value</typeparam><typeparam name="TC">Type of condition</typeparam>
    ''' <returns>The one of results with number of first condition which is true</returns>
    ''' <exception cref="ArgumentException">None of <paramref name="conditions"></paramref> evaluates to true and <paramref name="ThrowOnError"></paramref> is true.</exception>
    Public Function Switch(Of T, TC)(ByVal value As TC, ByVal c1 As TC, ByVal r1 As T, ByVal c2 As TC, ByVal r2 As T, Optional ByVal ThrowOnError As Boolean = True) As T
        Return Switch(Of T, TC)(value, New TC() {c1, c2}, New T() {r1, r2}, ThrowOnError)
    End Function
    ''' <summary>Returns one of results depending on condition compared to given values (3 conditions condition-result-condition-resul order of parameters)</summary>
    ''' <param name="c1">Condition 1</param><param name="value">Value to compare conditions with</param>
    ''' <param name="r1">Result 1 (returned when <paramref name="c1"></paramref> is true)</param>
    ''' <param name="c2">Condition 2</param>
    ''' <param name="r2">Result 2 (returned when <paramref name="c2"></paramref> is true)</param>
    ''' <param name="c3">Condition 3</param>
    ''' <param name="r3">Result 3 (returned when <paramref name="c3"></paramref> is true)</param>
    ''' <typeparam name="T">Type of <paramref name="results"></paramref> items and return value</typeparam><typeparam name="TC">Type of condition</typeparam>
    ''' <returns>The one of results with number of first condition which is true</returns>
    ''' <exception cref="ArgumentException">None of <paramref name="conditions"></paramref> evaluates to true and <paramref name="ThrowOnError"></paramref> is true.</exception>
    Public Function Switch(Of T, TC)(ByVal value As TC, ByVal c1 As TC, ByVal r1 As T, ByVal c2 As TC, ByVal r2 As T, ByVal c3 As TC, ByVal r3 As T, Optional ByVal ThrowOnError As Boolean = True) As T
        Return Switch(Of T, TC)(value, New TC() {c1, c2, c3}, New T() {r1, r2, r3}, ThrowOnError)
    End Function
    ''' <summary>Returns one of results depending on condition compared to given values (4 conditions condition-result-condition-resul order of parameters)</summary>
    ''' <param name="c1">Condition 1</param><param name="value">Value to compare conditions with</param>
    ''' <param name="r1">Result 1 (returned when <paramref name="c1"></paramref> is true)</param>
    ''' <param name="c2">Condition 2</param>
    ''' <param name="r2">Result 2 (returned when <paramref name="c2"></paramref> is true)</param>
    ''' <param name="c3">Condition 3</param>
    ''' <param name="r3">Result 3 (returned when <paramref name="c3"></paramref> is true)</param>
    ''' <param name="c4">Condition 4</param>
    ''' <param name="r4">Result 4 (returned when <paramref name="c4"></paramref> is true)</param>
    ''' <typeparam name="T">Type of <paramref name="results"></paramref> items and return value</typeparam><typeparam name="TC">Type of condition</typeparam>
    ''' <returns>The one of results with number of first condition which is true</returns>
    ''' <exception cref="ArgumentException">None of <paramref name="conditions"></paramref> evaluates to true and <paramref name="ThrowOnError"></paramref> is true.</exception>
    Public Function Switch(Of T, TC)(ByVal value As TC, ByVal c1 As TC, ByVal r1 As T, ByVal c2 As TC, ByVal r2 As T, ByVal c3 As TC, ByVal r3 As T, ByVal c4 As TC, ByVal r4 As T, Optional ByVal ThrowOnError As Boolean = True) As T
        Return Switch(Of T, TC)(value, New TC() {c1, c2, c3, c4}, New T() {r1, r2, r3, r4}, ThrowOnError)
    End Function
#End Region
#End Region
#Region "Arrays"
    ''' <summary>Returns given 1D array</summary>
    ''' <param name="items">Array to be returned</param>
    ''' <typeparam name="T">Type of items in array</typeparam>
    ''' <returns><paramref name="items"/></returns>
    ''' <remarks>The aim of this function is to provide syntactically the shortets way of obtaining arrays of given type. For example in visual basic you can obtain array this way:
    ''' <example>Dim arr = New T() {Itme1, Item2, Item3}</example>
    ''' This function shorten this to:
    ''' <example>Dim arr = arr(Item1, Item2, Item3)</example>
    ''' </remarks>
    Public Function arr(Of T)(ByVal ParamArray items As T()) As T()
        Return items
    End Function
    ''' <summary>Returns given 1D array as <see cref="IEnumerable(Of T)"/></summary>
    ''' <param name="items">Array to be returned</param>
    ''' <typeparam name="T">Type of items in array</typeparam>
    ''' <returns><paramref name="items"/></returns>
    ''' <remarks>The aim of this function is to provide syntactically the shortets way of obtaining <see cref="IEnumerable(Of T)"/>. For example in visual basic you can obtain <see cref="IEnumerable(Of T)"/> this way:
    ''' <example>Dim arr = DIrectCast(New T() {Itme1, Item2, Item3}, IEnumerable)</example>
    ''' This function shorten this to:
    ''' <example>Dim arr = enm(Item1, Item2, Item3)</example>
    ''' </remarks>
    Public Function enm(Of T)(ByVal ParamArray items As T()) As IEnumerable(Of T)
        Return items
    End Function
#End Region
End Module
