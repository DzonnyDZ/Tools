Imports System.Globalization

''' <summary>Base class for comparers of type <see cref="Char"/></summary>
''' <version version="1.5.4">This class is new in version 1.5.4</version>
Public MustInherit Class CharComparer
    Implements IComparer(Of Char), IEqualityComparer(Of Char)

    ''' <summary>When overridden in derived class compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
    ''' <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero <paramref name="x" /> is less than  <paramref name="y" />.Zero <paramref name="x" /> equals  <paramref name="y" />.Greater than zero <paramref name="x" /> is greater than  <paramref name="y" />.</returns>
    ''' <param name="x">The first object to compare.</param>
    ''' <param name="y">The second object to compare.</param>
    Public MustOverride Function Compare(x As Char, y As Char) As Integer Implements System.Collections.Generic.IComparer(Of Char).Compare
    ''' <summary>When overridden in derivced class determines whether the specified objects are equal.</summary>
    ''' <returns>true if the specified objects are equal; otherwise, false.</returns>
    ''' <param name="x">The first object of type <paramref name="T" /> to compare.</param>
    ''' <param name="y">The second object of type <paramref name="T" /> to compare.</param>
    Public MustOverride Overloads Function Equals(x As Char, y As Char) As Boolean Implements System.Collections.Generic.IEqualityComparer(Of Char).Equals
    ''' <summary>When overridden in derived class returns a hash code for the specified object.</summary>
    ''' <returns>A hash code for the specified object.</returns>
    ''' <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
    ''' <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null.</exception>
    Public MustOverride Overloads Function GetHashCode(obj As Char) As Integer Implements System.Collections.Generic.IEqualityComparer(Of Char).GetHashCode

#Region "Shared properties"
    ''' <summary>Cachees values of static properties</summary>
    ''' <threadsafety>Synchronize write access to this property using itself. Do not clear. Do not remove.</threadsafety>
    Private Shared ReadOnly cache As New Dictionary(Of StringComparison, CharComparer)
    ''' <summary>Gets chached values of static property or chaches a new values</summary>
    ''' <param name="key">Cache key</param>
    ''' <param name="create">A delegate to be called to create a new value if value with <paramref name="key"/> is not cached yet.</param>
    ''' <returns>Cahced value of result of<paramref name="create"/> call (in later case this result is cached for next call with same <paramref name="key"/>).</returns>
    Private Shared Function [Get](key As StringComparison, create As Func(Of CharComparer)) As CharComparer
        If Not cache.ContainsKey(key) Then
            SyncLock cache
                If Not cache.ContainsKey(key) Then cache.Add(key, create())
            End SyncLock
        End If
        Return cache(key)
    End Function

    ''' <summary>Gets a <see cref="CharComparer"/> object that performs a case-sensitive characterstring comparison using the word comparison rules of the current culture.</summary>
    Public Shared ReadOnly Property CurrentCulture As CharComparer
        Get
            Return [Get](StringComparison.CurrentCulture, Function() New StringComparerBasedCharComparer(StringComparer.CurrentCulture))
        End Get
    End Property
    ''' <summary>Gets a <see cref="CharComparer"/> object that performs case-insensitive character comparisons using the word comparison rules of the current culture.</summary>
    Public Shared ReadOnly Property CurrentCultureIgnoreCase As CharComparer
        Get
            Return [Get](StringComparison.CurrentCultureIgnoreCase, Function() New StringComparerBasedCharComparer(StringComparer.CurrentCultureIgnoreCase))
        End Get
    End Property
    ''' <summary>Gets a <see cref="CharComparer"/> object that performs a case-sensitive character comparison using the word comparison rules of the invariant culture.</summary>
    Public Shared ReadOnly Property InvariantCulture As CharComparer
        Get
            Return [Get](StringComparison.InvariantCulture, Function() New StringComparerBasedCharComparer(StringComparer.InvariantCulture))
        End Get
    End Property
    ''' <summary>Gets a <see cref="CharComparer"/> object that performs a case-insensitive character comparison using the word comparison rules of the invariant culture.</summary>
    Public Shared ReadOnly Property InvariantCultureIgnoreCase As CharComparer
        Get
            Return [Get](StringComparison.InvariantCultureIgnoreCase, Function() New StringComparerBasedCharComparer(StringComparer.InvariantCultureIgnoreCase))
        End Get
    End Property
    ''' <summary>Gets a <see cref="CharComparer"/> object that performs a case-sensitive ordinal character comparison.</summary>
    Public Shared ReadOnly Property Ordinal As CharComparer
        Get
            Return [Get](StringComparison.Ordinal, Function() New StringComparerBasedCharComparer(StringComparer.Ordinal))
        End Get
    End Property
    ''' <summary>Gets a S<see cref="CharComparer"/> object that performs a case-insensitive ordinal character comparison.</summary>
    Public Shared ReadOnly Property OrdinalIgnoreCase As CharComparer
        Get
            Return [Get](StringComparison.OrdinalIgnoreCase, Function() New StringComparerBasedCharComparer(StringComparer.Ordinal))
        End Get
    End Property
    ''' <summary>Gets a <see cref="CharComparer"/> object that performs character comparison based only on Unicode code of the character (which is by definition case-sensitive and kinda ordinal).</summary>
    Public Shared ReadOnly Property CodePoint As CharComparer
        Get
            Return CodePointCharComparer.Instance
        End Get
    End Property

#End Region

    Public Shared Function Create(ByVal culture As CultureInfo, ByVal ignoreCase As Boolean) As CharComparer
        Return New StringComparerBasedCharComparer(StringComparer.Create(culture, ignoreCase))
    End Function
End Class

''' <summary>Implements <see cref="CharComparer"/> thet compares character code points</summary>
''' <remarks>This is singleton class</remarks>
''' <version version="1.5.4">This class is new in version 1.5.4</version>
Friend NotInheritable Class CodePointCharComparer
    Inherits CharComparer

    Private Shared ReadOnly _instance As New CodePointCharComparer
    ''' <summary>Gets instance of this singleton class</summary>
    Public Shared ReadOnly Property Instance As CodePointCharComparer
        Get
            Return _instance
        End Get
    End Property
    ''' <summary>Private CTor for singleton</summary>
    Private Sub New()
    End Sub

    ''' <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
    ''' <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero <paramref name="x" /> is less than  <paramref name="y" />.Zero <paramref name="x" /> equals  <paramref name="y" />.Greater than zero <paramref name="x" /> is greater than  <paramref name="y" />.</returns>
    ''' <param name="x">The first object to compare.</param>
    ''' <param name="y">The second object to compare.</param>
    Public Overrides Function Compare(x As Char, y As Char) As Integer
        Return Comparer(Of Integer).Default.Compare(AscW(x), AscW(y))
    End Function

    ''' <summary>Determines whether the specified objects are equal.</summary>
    ''' <returns>true if the specified objects are equal; otherwise, false.</returns>
    ''' <param name="x">The first object of type <paramref name="T" /> to compare.</param>
    ''' <param name="y">The second object of type <paramref name="T" /> to compare.</param>
    Public Overloads Overrides Function Equals(x As Char, y As Char) As Boolean
        Return AscW(x) = AscW(y)
    End Function

    ''' <summary>Returns a hash code for the specified object.</summary>
    ''' <returns>A hash code for the specified object.</returns>
    ''' <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
    ''' <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null.</exception>
    Public Overloads Overrides Function GetHashCode(obj As Char) As Integer
        Return AscW(obj)
    End Function
End Class


''' <summary>Implements <see cref="CharComparer"/> based on <see cref="StringComparer"/></summary>
''' <version version="1.5.4">This class is new in version 1.5.4</version>
<EditorBrowsable(EditorBrowsableState.Advanced)>
Public NotInheritable Class StringComparerBasedCharComparer
    Inherits CharComparer
    Implements IComparer(Of String), IEqualityComparer(Of String)
    ''' <summary>Internal <see cref="StringComparer"/></summary>
    Private internal As StringComparer

    ''' <summary>CTor - creates a new instance of the <see cref="StringComparerBasedCharComparer"/></summary>
    ''' <param name="internal">String comparer to be used for character comparisons</param>
    ''' <exception cref="ArgumentNullException"><paramref name="internal"/> is null</exception>
    Public Sub New(internal As StringComparer)
        If internal Is Nothing Then Throw New ArgumentNullException("internal")
        Me.internal = internal
    End Sub

    ''' <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
    ''' <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero <paramref name="x" /> is less than  <paramref name="y" />.Zero <paramref name="x" /> equals  <paramref name="y" />.Greater than zero <paramref name="x" /> is greater than  <paramref name="y" />.</returns>
    ''' <param name="x">The first object to compare.</param>
    ''' <param name="y">The second object to compare.</param>
    Public Overrides Function Compare(x As Char, y As Char) As Integer
        Return internal.Compare(x, y)
    End Function

    ''' <summary>Determines whether the specified objects are equal.</summary>
    ''' <returns>true if the specified objects are equal; otherwise, false.</returns>
    ''' <param name="x">The first object of type <paramref name="T" /> to compare.</param>
    ''' <param name="y">The second object of type <paramref name="T" /> to compare.</param>
    Public Overloads Overrides Function Equals(x As Char, y As Char) As Boolean
        Return internal.Equals(x, y)
    End Function

    ''' <summary>Returns a hash code for the specified object.</summary>
    ''' <returns>A hash code for the specified object.</returns>
    ''' <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
    ''' <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null.</exception>
    Public Overloads Overrides Function GetHashCode(obj As Char) As Integer
        Return internal.GetHashCode(obj)
    End Function

    ''' <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
    ''' <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero <paramref name="x" /> is less than  <paramref name="y" />.Zero <paramref name="x" /> equals  <paramref name="y" />.Greater than zero <paramref name="x" /> is greater than  <paramref name="y" />.</returns>
    ''' <param name="x">The first object to compare.</param>
    ''' <param name="y">The second object to compare.</param>
    Private Overloads Function Compare(x As String, y As String) As Integer Implements System.Collections.Generic.IComparer(Of String).Compare
        Return internal.Compare(x, y)
    End Function

    ''' <summary>Determines whether the specified objects are equal.</summary>
    ''' <returns>true if the specified objects are equal; otherwise, false.</returns>
    ''' <param name="x">The first object of type <paramref name="T" /> to compare.</param>
    ''' <param name="y">The second object of type <paramref name="T" /> to compare.</param>
    Private Overloads Function Equals(x As String, y As String) As Boolean Implements System.Collections.Generic.IEqualityComparer(Of String).Equals
        Return internal.Equals(x, y)
    End Function

    ''' <summary>Returns a hash code for the specified object.</summary>
    ''' <returns>A hash code for the specified object.</returns>
    ''' <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
    ''' <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null.</exception>
    Private Overloads Function GetHashCode(obj As String) As Integer Implements System.Collections.Generic.IEqualityComparer(Of String).GetHashCode
        Return internal.GetHashCode(obj)
    End Function
End Class

''' <summary>Implements a <see cref="StringComparer"/> that's based on suplied <see cref="IEqualityComparer(Of T)"/> and <see cref="IComparer(Of T)"/> interfaces. Also provides capabilities of delegate-basing.</summary>
''' <version version="1.5.4">This class is new in version 1.5.4</version>
Public Class InterfacesBasedStringComparer
    Inherits StringComparer
    ''' <summary>Internal comparer used for &lt;=> comparisons</summary>
    Private ReadOnly comparer As IComparer(Of String)
    ''' <summary>Internal comparer used for equality comparisons</summary>
    Private ReadOnly equalityComparer As IEqualityComparer(Of String)
#Region "CTors"
    ''' <summary>CTor - creates a new instance of the <see cref="InterfacesBasedStringComparer"/> class from <see cref="IComparer(Of T)"/> and <see cref="IEqualityComparer(Of T)"/></summary>
    ''' <param name="comparer">Comparer used for  &lt;=> comparisons</param>
    ''' <param name="equalityComparer">Comparer used for equality comparisons</param>
    ''' <exception cref="ArgumentNullException"><paramref name="comparer"/> or <paramref name="equalityComparer"/> is null</exception>
    Public Sub New(comparer As IComparer(Of String), equalityComparer As IEqualityComparer(Of String))
        If comparer Is Nothing Then Throw New ArgumentNullException("comparer")
        If equalityComparer Is Nothing Then Throw New ArgumentNullException("equalityComparer")
        Me.comparer = comparer
        Me.equalityComparer = equalityComparer
    End Sub
    ''' <summary>CTor - creates a new instance of the <see cref="InterfacesBasedStringComparer"/> class from single object that implements both - <see cref="IComparer(Of T)"/> and <see cref="IEqualityComparer(Of T)"/> interfaces</summary>
    ''' <param name="comparer">The object that implements both - <see cref="IComparer(Of T)"/> and <see cref="IEqualityComparer(Of T)"/> interfaces</param>
    ''' <exception cref="ArgumentNullException"><paramref name="comparer"/> is null</exception>
    ''' <exception cref="TypeMismatchException"><paramref name="comparer"/> is not <see cref="IEqualityComparer(Of T)"/>[<see cref="String"/>] -or- <paramref name="comparer"/> is not <see cref="IComparer(Of T)"/>[<see cref="String"/>]</exception>
    ''' <remarks>This CTor is <see langword="protected"/>. For public assess use generic <see cref="Create"/> overload instead.</remarks>
    Protected Sub New(comparer As Object)
        If comparer Is Nothing Then Throw New ArgumentNullException("comparer")
        If Not TypeOf comparer Is IComparer(Of String) Then Throw New TypeMismatchException(comparer, "comparer", GetType(IComparer(Of String)))
        If Not TypeOf comparer Is IEqualityComparer(Of String) Then Throw New TypeMismatchException(comparer, "comparer", GetType(IEqualityComparer(Of String)))
        Me.comparer = comparer
        Me.equalityComparer = comparer
    End Sub
    ''' <summary>CTor - creates a new instance of the <see cref="InterfacesBasedStringComparer"/> class from <see cref="IComparer(Of T)"/></summary>
    ''' <param name="comparer">An <see cref="IComparer(Of T)"/> to base this comparison on</param>
    ''' <exception cref="ArgumentNullException"><paramref name="equalityComparer"/> is null</exception>
    ''' <remarks>Equality comparison is performed using <see cref="M:System.Collections.Generic.IComparer`1.Compare"/> checking for result 0</remarks>
    ''' <seelaso cref="DelegateBasedComparer(Of T)"/>
    Public Sub New(comparer As IComparer(Of String))
        Me.New(CObj(New InterfacesBasedStringComparer(New DelegateBasedComparer(Of String)(AddressOf comparer.Compare, AddressOf comparer.GetHashCode))))
    End Sub
    ''' <summary>Creates a new instance of the <see cref="InterfacesBasedStringComparer"/> class providing all three delegate</summary>
    ''' <param name="compare">Delegate for &lt;=> comparison</param>
    ''' <param name="equals">Delegate for equality comparison</param>
    ''' <param name="getHashCode">Delegate for getting hash code</param>
    ''' <exception cref="ArgumentNullException"><paramref name="compare"/>, <paramref name="equals"/> or <paramref name="getHashCode"/> is null</exception>
    Public Sub New(compare As Comparison(Of String), equals As Func(Of String, String, Boolean), getHashCode As Func(Of String, Integer))
        Me.New(New DelegateBasedComparer(Of String)(compare, equals, getHashCode))
    End Sub
    ''' <summary>CTor - creates a new instance of the <see cref="InterfacesBasedStringComparer"/> class providing <see cref="IComparer(Of T)"/> delegates</summary>
    ''' <param name="compare">Delegate for &lt;=> comparison</param>
    ''' <param name="getHashCode">Delegate for getting hash code</param>
    ''' <remarks>Equality comparison is performed using <paramref name="compare"/> delegate (checks for result 0)</remarks>
    ''' <exception cref="ArgumentNullException"><paramref name="compare"/> or <paramref name="getHashCode"/> is null</exception>
    Public Sub New(compare As Comparison(Of String), getHashCode As Func(Of String, Integer))
        Me.New(New DelegateBasedComparer(Of String)(compare, getHashCode))
    End Sub
#End Region
#Region "Create"
    ''' <summary>Creates a <see cref="StringComparer"/> from an object that implements both - <see cref="IEqualityComparer(Of T)"/> and <see cref="IComparer(Of T)"/></summary>
    ''' <typeparam name="TComparer">Type of the object</typeparam>
    ''' <param name="comparer">The comparer</param>
    ''' <returns>A new instance of the <see cref="StringComparer"/> class; if <paramref name="comparer"/> is <see cref="StringComparer"/> returns it.</returns>
    ''' <exception cref="ArgumentNullException"><paramref name="comparer"/>is null</exception>
    Public Shared Shadows Function Create(Of TComparer As {IEqualityComparer(Of String), IComparer(Of String)})(comparer As TComparer) As StringComparer
        If TypeOf comparer Is StringComparer Then Return DirectCast(CObj(comparer), StringComparer)
        Return New InterfacesBasedStringComparer(comparer, comparer)
    End Function
    ''' <summary>Creates a <see cref="StringComparer"/> from <see cref="IComparer(Of T)"/> and <see cref="IEqualityComparer(Of T)"/></summary>
    ''' <param name="comparer">An <see cref="IComparer(Of T)"/></param>
    ''' <param name="equalityComparer">An <see cref="IEqualityComparer(Of T)"/></param>
    ''' <returns>A new instance of <see cref="StringComparer"/></returns>
    ''' <exception cref="ArgumentNullException"><paramref name="comparer"/> or <paramref name="equalityComparer"/> is null</exception>
    Public Shared Shadows Function Create(comparer As IComparer(Of String), equalityComparer As IEqualityComparer(Of String)) As StringComparer
        Return New InterfacesBasedStringComparer(comparer, equalityComparer)
    End Function
    ''' <summary>Creates a <see cref="StringComparer"/> from <see cref="IComparer(Of T)"/></summary>
    ''' <param name="comparer">An <see cref="IComparer(Of T)"/></param>
    ''' <returns>A new instance of <see cref="StringComparer"/>; if <paramref name="comparer"/> is <see cref="StringComparer"/> returns it.</returns>
    ''' <exception cref="ArgumentNullException"><paramref name="comparer"/> is null</exception>
    ''' <remarks>Equality comparison is performed using <see cref="M:System.Collections.Generic.IComparer`1.Compare"/> checking for result 0</remarks>
    ''' <seelaso cref="DelegateBasedComparer(Of T)"/>
    Public Shared Shadows Function Create(comparer As IComparer(Of String)) As StringComparer
        If TypeOf comparer Is StringComparer Then Return comparer
        Return New InterfacesBasedStringComparer(comparer)
    End Function
    ''' <summary>Creates a <see cref="StringComparer"/> from 3 comparison delegates</summary>
    ''' <param name="compare">A delegate used for &lt;=> comparisons</param>
    ''' <param name="equals">A delegate used for equality comparison</param>
    ''' <param name="getHashCode">A delegate used to get hash code</param>
    ''' <returns>A new instance of <see cref="StringComparer"/></returns>
    ''' <exception cref="ArgumentNullException">Any parameter is null</exception>
    ''' <seelaso cref="DelegateBasedComparer(Of T)"/>
    Public Shared Shadows Function Create(compare As Comparison(Of String), equals As Func(Of String, String, Boolean), getHashCode As Func(Of String, Integer)) As StringComparer
        Return New InterfacesBasedStringComparer(compare, equals, getHashCode)
    End Function
    ''' <summary>Creates a <see cref="StringComparer"/> ffrom <see cref="IComparer(Of T)"/> delegates</summary>
    ''' <param name="compare">Delegate to be used for comparisons</param>
    ''' <param name="getHashCode">Delegate to be used to get a hash code</param>
    ''' <returns>A new instance of <see cref="StringComparer"/></returns>
    ''' <exception cref="ArgumentNullException">Any parameter is null</exception>
    ''' <remarks><paramref name="compare"/> is used also for equality comparision, checking result to be 0.</remarks>
    ''' <seelaso cref="DelegateBasedComparer(Of T)"/>
    Public Shared Shadows Function Create(compare As Comparison(Of String), getHashCode As Func(Of String, Integer)) As StringComparer
        Return New InterfacesBasedStringComparer(compare, getHashCode)
    End Function
#End Region

    ''' <summary>When overridden in a derived class, compares two strings and returns an indication of their relative sort order.</summary>
    ''' <param name="x">A string to compare to <paramref name="y" />.</param>
    ''' <param name="y">A string to compare to <paramref name="x" />.</param>
    ''' <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.ValueMeaningLess than zero <paramref name="x" /> is less than<paramref name="y" />.-or-<paramref name="x" /> is null.Zero <paramref name="x" /> is equal to  <paramref name="y" />.Greater than zero <paramref name="x" /> is greater than  <paramref name="y" />.-or- <paramref name="y" /> is null.</returns>
    ''' <filterpriority>1</filterpriority>
    Public Overloads Overrides Function Compare(x As String, y As String) As Integer
        Return comparer.Compare(x, y)
    End Function

    ''' <summary>When overridden in a derived class, indicates whether two strings are equal.</summary>
    ''' <param name="x">A string to compare to <paramref name="y" />.</param>
    ''' <param name="y">A string to compare to <paramref name="x" />.</param>
    ''' <returns>true if <paramref name="x" /> and <paramref name="y" /> refer to the same object, or <paramref name="x" /> and <paramref name="y" /> are equal; otherwise, false.</returns>
    ''' <filterpriority>1</filterpriority>
    Public Overloads Overrides Function Equals(x As String, y As String) As Boolean
        Return equalityComparer.Equals(x, y)
    End Function

    ''' <summary>When overridden in a derived class, gets the hash code for the specified object.</summary>
    ''' <param name="obj">An object.</param>
    ''' <returns>A 32-bit signed hash code calculated from the value of the <paramref name="obj" /> parameter.</returns>
    ''' <exception cref="T:System.ArgumentNullException"><paramref name="obj" /> is null.</exception>
    ''' <filterpriority>2</filterpriority>
    Public Overloads Overrides Function GetHashCode(obj As String) As Integer
        Return equalityComparer.GetHashCode(obj)
    End Function
End Class

''' <summary>Implements <see cref="IComparer(Of T)"/> and <see cref="IEqualityComparer(Of T)"/>. Operations are performed using user-suplied delegates.</summary>
''' <typeparam name="T">Type of value to compare</typeparam>
''' <version version="1.5.4">This class is new in version 1.5.4</version>
Public Class DelegateBasedComparer(Of T)
    Implements IComparer(Of T), IEqualityComparer(Of T)

    Private ReadOnly _compare As Comparison(Of T)
    Private ReadOnly _equals As Func(Of T, T, Boolean)
    Private ReadOnly _getHashCode As Func(Of T, Integer)

    ''' <summary>Creates a new instance of the <see cref="DelegateBasedComparer(Of T)"/> class providing all three delegate</summary>
    ''' <param name="compare">Delegate for &lt;=> comparison</param>
    ''' <param name="equals">Delegate for equality comparison</param>
    ''' <param name="getHashCode">Delegate for getting hash code</param>
    ''' <exception cref="ArgumentNullException"><paramref name="compare"/>, <paramref name="equals"/> or <paramref name="getHashCode"/> is null</exception>
    Public Sub New(compare As Comparison(Of T), equals As Func(Of T, T, Boolean), getHashCode As Func(Of T, Integer))
        If compare Is Nothing Then Throw New ArgumentNullException("compare")
        If equals Is Nothing Then Throw New ArgumentNullException("equals")
        If getHashCode Is Nothing Then Throw New ArgumentNullException("getHashCode")
        _compare = compare
        _equals = equals
        _getHashCode = getHashCode
    End Sub
    ''' <summary>CTor - creates a new instance of the <see cref="DelegateBasedComparer(Of T)"/> class providing <see cref="IComparer(Of T)"/> delegates</summary>
    ''' <param name="compare">Delegate for &lt;=> comparison</param>
    ''' <param name="getHashCode">Delegate for getting hash code</param>
    ''' <remarks>Equality comparison is performed using <paramref name="compare"/> delegate (checks for result 0)</remarks>
    ''' <exception cref="ArgumentNullException"><paramref name="compare"/> or <paramref name="getHashCode"/> is null</exception>
    Public Sub New(compare As Comparison(Of T), getHashCode As Func(Of T, Integer))
        If compare Is Nothing Then Throw New ArgumentNullException("compare")
        If getHashCode Is Nothing Then Throw New ArgumentNullException("getHashCode")
        _compare = compare
        _getHashCode = getHashCode
    End Sub

    ''' <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
    ''' <param name="x">The first object to compare.</param>
    ''' <param name="y">The second object to compare.</param>
    ''' <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero <paramref name="x" /> is less than  <paramref name="y" />.Zero <paramref name="x" /> equals  <paramref name="y" />.Greater than zero <paramref name="x" /> is greater than  <paramref name="y" />.</returns>
    Public Function Compare(x As T, y As T) As Integer Implements System.Collections.Generic.IComparer(Of T).Compare
        Return _compare(x, y)
    End Function

    ''' <summary>Determines whether the specified objects are equal.</summary>
    ''' <returns>true if the specified objects are equal; otherwise, false.</returns>
    ''' <param name="x">The first object of type <paramref name="T" /> to compare.</param>
    ''' <param name="y">The second object of type <paramref name="T" /> to compare.</param>
    Public Overloads Function Equals(x As T, y As T) As Boolean Implements System.Collections.Generic.IEqualityComparer(Of T).Equals
        If _equals Is Nothing Then Return Compare(x, y) = 0
        Return _equals(x, y)
    End Function

    ''' <summary>Returns a hash code for the specified object.</summary>
    ''' <returns>A hash code for the specified object.</returns>
    ''' <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
    ''' <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is null (depends on if underlying delegate throws it).</exception>
    Public Overloads Function GetHashCode(obj As T) As Integer Implements System.Collections.Generic.IEqualityComparer(Of T).GetHashCode
        Return _getHashCode(obj)
    End Function
End Class