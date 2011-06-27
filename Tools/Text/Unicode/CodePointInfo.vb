Imports System.Xml.Linq
Imports System.Linq
Imports Tools.NumericsT
Imports Tools.ExtensionsT
Imports System.Globalization.CultureInfo

Namespace TextT.UnicodeT
    ''' <summary>Points to a code point</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class CodePointInfo
        Private xml As XDocument
        Private _codepoint As UInteger

        ''' <summary>CTor - creates a new instance of <see cref="CodePointInfo"/> class from unsigned ocdepoint value</summary>
        ''' <param name="xml">A XML document containign current Unicode Character Database XML</param>
        ''' <param name="codepoint">Code point value</param>
        ''' <remarks>This CTor is not CLS-compliant. CLS-compliant overload exists.</remarks>
        <CLSCompliant(False)>
        Public Sub New(xml As XDocument, codepoint As UInteger)
            _codepoint = codepoint
            Me.xml = xml
        End Sub
        ''' <summary>CTor - creates a new instance of <see cref="CodePointInfo"/> class from signed ocdepoint value</summary>
        ''' <param name="xml">A XML document containign current Unicode Character Database XML</param>
        ''' <param name="codepoint">Code point value</param>
        Public Sub New(xml As XDocument, codepoint As Integer)
            Me.New(xml, codepoint.BitwiseSame)
        End Sub
        ''' <summary>CTor - creates a new instance of <see cref="CodePointInfo"/> class from UTF-16 character</summary>
        ''' <param name="xml">A XML document containign current Unicode Character Database XML</param>
        ''' <param name="character">A UTF-16 character</param>
        Public Sub New(xml As XDocument, character As Char)
            Me.New(xml, Char.ConvertToUtf32(CStr(character), 0))
        End Sub

        Private _isPLaceholder As Boolean
        ''' <summary>CTor - creates a new instance of <see cref="CodePointInfo"/> class which represents current character placeholder</summary>
        Private Sub New(xml As XDocument)
            Me.xml = xml
            _isPLaceholder = True
        End Sub

        ''' <summary>Gets value indicating if this instance represents current charatcer placeholder (#)</summary>
        ''' <remarks>In Unicode Character Database XML current character placeholders are used to denote current charatcer</remarks>
        Public ReadOnly Property IsPlaceholder As Boolean
            Get
                Return _isPLaceholder
            End Get
        End Property
        ''' <summary>Creates a new instance of the <see cref="CodePointInfo"/> class which represents current character placeholder (#)</summary>
        ''' <param name="xml">XML document all characters were loaded from</param>
        ''' <returns>A new instance of <see cref="CodePointInfo"/> class representing placeholder character</returns>
        Public Shared Function CreatePlaceholder(Optional xml As XDocument = Nothing) As CodePointInfo
            Return New CodePointInfo(xml)
        End Function

        ''' <summary>Gets value of current code point</summary>
        ''' <remarks>This property is not CLS-compilant. CLS-compliant alternative is <see cref="CodepointSigned"/></remarks>
        ''' <exception cref="InvalidOperationException"><see cref="IsPlaceholder"/> is true</exception>
        <CLSCompliant(False)>
        Public ReadOnly Property CodePoint As UInteger
            Get
                If IsPlaceholder Then Throw New InvalidOperationException(ResourcesT.Exceptions.NotAllowedForPlaceholderCharacters)
                Return _codepoint
            End Get
        End Property

        ''' <summary>CLS-compliant alternative of <see cref="Codepoint"/></summary>
        ''' <exception cref="InvalidOperationException"><see cref="IsPlaceholder"/> is true</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property CodePointSigned As Integer
            Get
                Return CodePoint.BitwiseSame
            End Get
        End Property

        ''' <summary>Gets string representing current code point.</summary>
        ''' <returns>String of lenght 1 or 2 (for surrogate pairs)</returns>
        ''' <exception cref="InvalidOperationException"><see cref="IsPlaceholder"/> is true</exception>
        Public ReadOnly Property Characters As String
            Get
                Return Char.ConvertFromUtf32(CodePointSigned)
            End Get
        End Property

        ''' <summary>Gets a UTF-16 character representing current code point</summary>
        ''' <exception cref="InvalidOperationException">Current code point is surrogate pair -or- <see cref="IsPlaceholder"/> is true</exception>
        Public ReadOnly Property Character As Char
            Get
                If Characters.Length > 1 Then Throw New InvalidOperationException(ResourcesT.Exceptions.CharacterRepresentsSurrogatePair)
                Return Characters(0)
            End Get
        End Property

        ''' <summary>Gets value indicating if current code point represents surrogate pair</summary>
        ''' <exception cref="InvalidOperationException"><see cref="IsPlaceholder"/> is true</exception>
        Public ReadOnly Property IsSurrogatePair As Boolean
            Get
                Return Characters.Length > 1
            End Get
        End Property

        ''' <summary>Gets <see cref="UnicodeT.UnicodeCodePoint"/> instance which provides information about current code point</summary>
        ''' <returns>
        ''' A <see cref="UnicodeT.UnicodeCodePoint"/> instance which provides information about current code point.
        ''' Null if this instance was initialized with null XML document or if the XML document does not contain information for current code point.
        ''' </returns>
        ''' <exception cref="InvalidOperationException"><see cref="IsPlaceholder"/> is true</exception>
        Public ReadOnly Property UnicodeCodePoint As UnicodeCodePoint
            Get
                If xml Is Nothing Then Return Nothing
                Return New UnicodeCharacterDatabase(xml).FindCodePoint(CodePoint)
            End Get
        End Property

        ''' <summary>Serves as a hash function for a particular type. </summary>
        ''' <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function GetHashCode() As Integer
            Return If(IsPlaceholder, 0, CodePoint.GetHashCode)
        End Function

        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
        ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is CodePointInfo Then
                Dim oth As CodePointInfo = DirectCast(obj, CodePointInfo)
                If Me.xml IsNot oth.xml Then Return False
                If oth.IsPlaceholder AndAlso Me.IsPlaceholder Then Return True
                If oth.IsPlaceholder OrElse Me.IsPlaceholder Then Return False
                Return Me.CodePoint = oth.CodePoint
            End If
            Return MyBase.Equals(obj)
        End Function

        ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
        ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function ToString() As String
            Return If(IsPlaceholder, "#", String.Format(InvariantCulture, "U+{0:X}", CodePoint))
        End Function

        ''' <summary>Compares two instances of <see cref="CodePointInfo"/> for equality</summary>
        ''' <param name="a">A <see cref="CodePointInfo"/></param>
        ''' <param name="b">A <see cref="CodePointInfo"/></param>
        ''' <returns>True if <paramref name="a"/> and <paramref name="b"/> are backed by same <see cref="XDocument"/> (reference equivalence) and represent same code point (numeric value). Also returns true if both - <paramref name="a"/> and <paramref name="b"/> are null.</returns>
        Public Shared Operator =(a As CodePointInfo, b As CodePointInfo) As Boolean
            If a Is Nothing AndAlso b Is Nothing Then Return True
            If a Is Nothing OrElse b Is Nothing Then Return False
            Return a.Equals(b)
        End Operator

        ''' <summary>Compares two instances of <see cref="CodePointInfo"/> for inequality</summary>
        ''' <param name="a">A <see cref="CodePointInfo"/></param>
        ''' <param name="b">A <see cref="CodePointInfo"/></param>
        ''' <returns>False if <paramref name="a"/> and <paramref name="b"/> are backed by same <see cref="XDocument"/> (reference equivalence) and represent same code point (numeric value). Also returns false if both - <paramref name="a"/> and <paramref name="b"/> are null.</returns>
        Public Shared Operator <>(a As CodePointInfo, b As CodePointInfo) As Boolean
            Return Not (a = b)
        End Operator

        ''' <summary>Compares codepoints represented by two <see cref="CodePointInfo"/> instances</summary>
        ''' <param name="a">A <see cref="CodePointInfo"/></param>
        ''' <param name="b">A <see cref="CodePointInfo"/></param>
        ''' <returns>True if codepoint represented by <paramref name="a"/> is lower than codepoint represented by <paramref name="b"/>. Null if either <paramref name="a"/> or <paramref name="b"/> is null.</returns>
        Public Shared Operator <(a As CodePointInfo, b As CodePointInfo) As Boolean?
            If a Is Nothing OrElse b Is Nothing Then Return Nothing
            If a.IsPlaceholder AndAlso b.IsPlaceholder Then Return False
            If a.IsPlaceholder OrElse b.IsPlaceholder Then Return Nothing
            Return a.CodePoint < b.CodePoint
        End Operator

        ''' <summary>Compares codepoints represented by two <see cref="CodePointInfo"/> instances</summary>
        ''' <param name="a">A <see cref="CodePointInfo"/></param>
        ''' <param name="b">A <see cref="CodePointInfo"/></param>
        ''' <returns>True if codepoint represented by <paramref name="a"/> is greater than codepoint represented by <paramref name="b"/>. Null if either <paramref name="a"/> or <paramref name="b"/> is null.</returns>
        Public Shared Operator >(a As CodePointInfo, b As CodePointInfo) As Boolean?
            If a Is Nothing OrElse b Is Nothing Then Return Nothing
            If a.IsPlaceholder AndAlso b.IsPlaceholder Then Return False
            If a.IsPlaceholder OrElse b.IsPlaceholder Then Return Nothing
            Return a.CodePoint > b.CodePoint
        End Operator

        ''' <summary>Compares codepoints represented by two <see cref="CodePointInfo"/> instances</summary>
        ''' <param name="a">A <see cref="CodePointInfo"/></param>
        ''' <param name="b">A <see cref="CodePointInfo"/></param>
        ''' <returns>True if codepoint represented by <paramref name="a"/> is lower than or equal to codepoint represented by <paramref name="b"/>. Null if either <paramref name="a"/> or <paramref name="b"/> is null.</returns>
        Public Shared Operator <=(a As CodePointInfo, b As CodePointInfo) As Boolean?
            If a Is Nothing OrElse b Is Nothing Then Return Nothing
            If a.IsPlaceholder AndAlso b.IsPlaceholder Then Return True
            If a.IsPlaceholder OrElse b.IsPlaceholder Then Return Nothing
            Return a.CodePoint <= b.CodePoint
        End Operator

        ''' <summary>Compares codepoints represented by two <see cref="CodePointInfo"/> instances</summary>
        ''' <param name="a">A <see cref="CodePointInfo"/></param>
        ''' <param name="b">A <see cref="CodePointInfo"/></param>
        ''' <returns>True if codepoint represented by <paramref name="a"/> is greater than or equal to codepoint represented by <paramref name="b"/>. Null if either <paramref name="a"/> or <paramref name="b"/> is null.</returns>
        Public Shared Operator >=(a As CodePointInfo, b As CodePointInfo) As Boolean?
            If a Is Nothing OrElse b Is Nothing Then Return Nothing
            If a.IsPlaceholder AndAlso b.IsPlaceholder Then Return True
            If a.IsPlaceholder OrElse b.IsPlaceholder Then Return Nothing
            Return a.CodePoint >= b.CodePoint
        End Operator
    End Class

    ''' <summary>Read-only collection of Unicode code points</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <EditorBrowsable(EditorBrowsableState.Advanced)>
    Public Class CodePointInfoCollection
        Implements ICollection(Of UnicodeCodePoint)

        Private _codePoints As UInteger?()
        Private xml As XDocument

        ''' <summary>CTor - ctreates a new instance of the <see cref="CodePointInfoCollection"/> class</summary>
        ''' <param name="xml">A XML document all code points were loaded from</param>
        ''' <param name="codePoints">Actual code points</param>
        ''' <remarks>This CTor is not CLS-compliant. CLS-compliant overload exists.</remarks>
        <CLSCompliant(False)>
        Public Sub New(xml As XDocument, ParamArray codePoints() As UInteger)
            _codePoints = If(codePoints Is Nothing, New UInteger?() {}, (From cp In codePoints Select New UInteger?(cp)).ToArray)
            Me.xml = xml
        End Sub

        ''' <summary>CTor - ctreates a new instance of the <see cref="CodePointInfoCollection"/> class</summary>
        ''' <param name="xml">A XML document all code points were loaded from</param>
        ''' <param name="codePoints">Actual code points</param>
        Public Sub New(xml As XDocument, ParamArray codePoints() As Integer)
            Me.New(xml, If(codePoints Is Nothing, Nothing, From cp In codePoints Select cp.BitwiseSame).ToArray)
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="CodePointInfoCollection"/> from string representation of code points sequence</summary>
        ''' <param name="xml">A XML document all code points were loaded from</param>
        ''' <param name="codePoints">Whitespace-delimited list of code point values specified as hexanumbers (without any hexa specifier). Special code point # (current character placeholder) is allowed.</param>
        ''' <exception cref="FormatException">A code point does not represent valid hexadecimal number</exception>
        ''' <exception cref="OverflowException">A code point represents number which is less than <see cref="UInt32.MinValue"/> or greater than <see cref="UInt32.MaxValue"/>.</exception>
        Public Sub New(xml As XDocument, codePoints As String)
            _codePoints = If(codePoints.IsNullOrWhiteSpace, New UInteger?() {}, (
                           From cp In codePoints.WhiteSpaceSplit
                           Select If(cp = "#", New UInteger?, UInteger.Parse("0x" & cp, Globalization.NumberStyles.HexNumber, InvariantCulture))
                           ).ToArray)
            Me.xml = xml
        End Sub


        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        ''' <filterpriority>1</filterpriority>
        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of UnicodeCodePoint) Implements System.Collections.Generic.IEnumerable(Of UnicodeCodePoint).GetEnumerator
            Return From itm In _codePoints Select If(itm.HasValue, New CodePointInfo(xml, itm.Value), CodePointInfo.CreatePlaceholder(xml))
        End Function

        ''' <summary>Returns an enumerator that iterates through a collection.</summary>
        ''' <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        ''' <filterpriority>2</filterpriority>
        Private Function IEnumerable_GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <param name="item">Ignored</param>
        ''' <exception cref="NotSupportedException">Always (this collection is read-only)</exception>
        Private Sub ICollection_Add(item As UnicodeCodePoint) Implements System.Collections.Generic.ICollection(Of UnicodeCodePoint).Add
            Throw New NotSupportedException(ResourcesT.Exceptions.CollectionIsReadOnly)
        End Sub

        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <exception cref="NotSupportedException">Always (this collection is read-only)</exception>
        Private Sub Clear() Implements System.Collections.Generic.ICollection(Of UnicodeCodePoint).Clear
            Throw New NotSupportedException(ResourcesT.Exceptions.CollectionIsReadOnly)
        End Sub

        ''' <summary>Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.</summary>
        ''' <returns>true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.</returns>
        ''' <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        Public Function Contains(item As UnicodeCodePoint) As Boolean Implements System.Collections.Generic.ICollection(Of UnicodeCodePoint).Contains
            Return Enumerable.Contains(Me, item)
        End Function

        ''' <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
        ''' <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        ''' <param name="arrayIndex">The zero-based index in 
        ''' <paramref name="array" /> at which copying begins.</param>
        ''' <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is null.</exception>
        ''' <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex" /> is less than 0.</exception>
        ''' <exception cref="T:System.ArgumentException">
        ''' <paramref name="array" /> is multidimensional.-or-
        ''' The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from  <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.-or-
        ''' Type  <paramref name="T" /> cannot be cast automatically to the type of the destination <paramref name="array" />.
        ''' </exception>
        Public Sub CopyTo(array() As UnicodeCodePoint, arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of UnicodeCodePoint).CopyTo
            If array Is Nothing Then Throw New ArgumentNullException("array")
            If arrayIndex < 0 Then Throw New ArgumentOutOfRangeException("arrayIndex")
            If arrayIndex + Count > array.Length Then Throw New ArgumentException(ResourcesT.Exceptions.NotEnoughSpaceInDestinationArray)
            Dim i% = arrayIndex
            For Each itm In Me
                array(i) = itm
            Next
        End Sub

        ''' <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</summary>
        ''' <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        Public ReadOnly Property Count As Integer Implements System.Collections.Generic.ICollection(Of UnicodeCodePoint).Count
            Get
                Return _codePoints.Length
            End Get
        End Property

        ''' <summary>Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.</summary>
        ''' <returns>true - this collection is read-only</returns>
        Private ReadOnly Property ICollection_IsReadOnly As Boolean Implements System.Collections.Generic.ICollection(Of UnicodeCodePoint).IsReadOnly
            Get
                Return True
            End Get
        End Property

        ''' <summary>Throws <see cref="NotSupportedException"/></summary>
        ''' <param name="item">Ignored</param>
        ''' <returns>This method never returns and always throws <see cref="NotSupportedException"/></returns>
        ''' <exception cref="NotSupportedException">Always (this collection is read-only)</exception>
        Private Function ICollection_Remove(item As UnicodeCodePoint) As Boolean Implements System.Collections.Generic.ICollection(Of UnicodeCodePoint).Remove
            Throw New NotSupportedException(ResourcesT.Exceptions.CollectionIsReadOnly)
        End Function

        ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
        ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function ToString() As String
            Return (From itm In _codePoints Select If(itm.HasValue, itm.Value.ToString("X", InvariantCulture), "#")).Join(" ")
        End Function
        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
        ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is CodePointInfo Then
                Dim cpi As CodePointInfoCollection = obj
                If cpi.xml Is Me.xml AndAlso cpi.Count = Me.Count Then
                    For i = 0 To cpi.Count - 1
                        If Me._codePoints(i).HasValue <> cpi._codePoints(i).HasValue Then Return False
                        If Me._codePoints(i).HasValue AndAlso Me._codePoints(i) <> cpi._codePoints(i) Then Return False
                    Next
                    Return True
                End If
                Return False
            End If
            Return MyBase.Equals(obj)
        End Function

        ''' <summary>Serves as a hash function for a particular type. </summary>
        ''' <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function GetHashCode() As Integer
            Dim ret As Integer = 0
            For Each value In _codePoints
                ret = ret Or value.GetHashCode
            Next
            Return ret
        End Function

        ''' <summary>Gets item at specified index</summary>
        ''' <param name="index">Index to get item at</param>
        ''' <exception cref="IndexOutOfRangeException"><paramref name="index"/> is less than zero or <paramref name="index"/> is greater than or equal to <see cref="Count"/>.</exception>"
        Default Public ReadOnly Property Item(index As Integer) As CodePointInfo
            Get
                If index < 0 OrElse index >= Count Then Throw New IndexOutOfRangeException("index")
                Return If(_codePoints(index).HasValue, New CodePointInfo(xml, _codePoints(index).Value), CodePointInfo.CreatePlaceholder(xml))
            End Get
        End Property

        ''' <summary>Gets array of code point values contained in this collection</summary>
        ''' <remarks>This property is not CLS-compliant. CLS-compliant alternative is <see cref="CodePointsSigned"/></remarks>
        ''' <exception cref="InvalidOperationException">This collection contains placeholder charatcer (#)</exception>
        <CLSCompliant(False)>
        Public ReadOnly Property CodePoints As UInteger()
            Get
                If _codePoints.Contains(Nothing) Then Throw New InvalidOperationException(ResourcesT.Exceptions.SequenceContainsPlaceholder)
                Return _codePoints.Clone
            End Get
        End Property

        ''' <summary>CLS-compliant version of <see cref="CodePoints"/></summary>
        ''' <exception cref="InvalidOperationException">This collection contains placeholder charatcer (#)</exception>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property CodePointsSigned As Integer()
            Get
                If _codePoints.Contains(Nothing) Then Throw New InvalidOperationException(ResourcesT.Exceptions.SequenceContainsPlaceholder)
                Return (From cp In _codePoints Select cp.Value.BitwiseSame).ToArray
            End Get
        End Property
    End Class
End Namespace
