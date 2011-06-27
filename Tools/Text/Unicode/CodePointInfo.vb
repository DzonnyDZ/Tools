Imports System.Xml.Linq
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

        ''' <summary>Gets value of current code point</summary>
        ''' <remarks>This property is not CLS-compilant. CLS-compliant alternative is <see cref="CodepointSigned"/></remarks>
        <CLSCompliant(False)>
        Public ReadOnly Property CodePoint As UInteger
            Get
                Return _codepoint
            End Get
        End Property

        ''' <summary>CLS-compliant alternative of <see cref="Codepoint"/></summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property CodePointSigned As Integer
            Get
                Return CodePoint.BitwiseSame
            End Get
        End Property

        ''' <summary>Gets string representing current code point.</summary>
        ''' <returns>String of lenght 1 or 2 (for surrogate pairs)</returns>
        Public ReadOnly Property Characters As String
            Get
                Return Char.ConvertFromUtf32(CodepointSigned)
            End Get
        End Property

        ''' <summary>Gets a UTF-16 character representing current code point</summary>
        ''' <exception cref="InvalidOperationException">Current code point is surrogate pair</exception>
        Public ReadOnly Property Character As Char
            Get
                If Characters.Length > 1 Then Throw New InvalidOperationException(ResourcesT.Exceptions.CharacterRepresentsSurrogatePair)
                Return Characters(0)
            End Get
        End Property

        ''' <summary>Gets value indicating if current code point represents surrogate pair</summary>
        Public ReadOnly Property IsSurrogatePair As Boolean
            Get
                Return Characters.Length > 1
            End Get
        End Property

        ''' <summary>Gets <see cref="UnicodeT.UnicodeCodePoint"/> instance which provides information about current code point</summary>
        ''' <returns>
        ''' A <see cref="UnicodeT..UnicodeCodePoint"/> instance which provides information about current code point.
        ''' Null if this instance was initialized with null XML document or if the XML document does not contain information for current code point.
        ''' </returns>
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
            Return CodePoint.GetHashCode
        End Function

        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
        ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is CodePointInfo Then Return Me.xml Is DirectCast(obj, CodePointInfo).xml AndAlso Me.CodePoint = DirectCast(obj, CodePointInfo).CodePoint
            Return MyBase.Equals(obj)
        End Function

        ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
        ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function ToString() As String
            Return String.Format(InvariantCulture, "U+{0:X}", CodePoint)
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
            Return a.CodePoint < b.CodePoint
        End Operator

        ''' <summary>Compares codepoints represented by two <see cref="CodePointInfo"/> instances</summary>
        ''' <param name="a">A <see cref="CodePointInfo"/></param>
        ''' <param name="b">A <see cref="CodePointInfo"/></param>
        ''' <returns>True if codepoint represented by <paramref name="a"/> is greater than codepoint represented by <paramref name="b"/>. Null if either <paramref name="a"/> or <paramref name="b"/> is null.</returns>
        Public Shared Operator >(a As CodePointInfo, b As CodePointInfo) As Boolean?
            If a Is Nothing OrElse b Is Nothing Then Return Nothing
            Return a.CodePoint > b.CodePoint
        End Operator

        ''' <summary>Compares codepoints represented by two <see cref="CodePointInfo"/> instances</summary>
        ''' <param name="a">A <see cref="CodePointInfo"/></param>
        ''' <param name="b">A <see cref="CodePointInfo"/></param>
        ''' <returns>True if codepoint represented by <paramref name="a"/> is lower than or equal to codepoint represented by <paramref name="b"/>. Null if either <paramref name="a"/> or <paramref name="b"/> is null.</returns>
        Public Shared Operator <=(a As CodePointInfo, b As CodePointInfo) As Boolean?
            If a Is Nothing OrElse b Is Nothing Then Return Nothing
            Return a.CodePoint <= b.CodePoint
        End Operator

        ''' <summary>Compares codepoints represented by two <see cref="CodePointInfo"/> instances</summary>
        ''' <param name="a">A <see cref="CodePointInfo"/></param>
        ''' <param name="b">A <see cref="CodePointInfo"/></param>
        ''' <returns>True if codepoint represented by <paramref name="a"/> is greater than or equal to codepoint represented by <paramref name="b"/>. Null if either <paramref name="a"/> or <paramref name="b"/> is null.</returns>
        Public Shared Operator >=(a As CodePointInfo, b As CodePointInfo) As Boolean?
            If a Is Nothing OrElse b Is Nothing Then Return Nothing
            Return a.CodePoint >= b.CodePoint
        End Operator
    End Class
End Namespace
