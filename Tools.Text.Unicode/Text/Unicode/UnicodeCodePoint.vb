Imports System.Globalization.CultureInfo
Imports Tools.ComponentModelT
Imports Tools.ExtensionsT
Imports <xmlns='http://www.unicode.org/ns/2003/ucd/1.0'>
Imports System.Xml.Serialization

Namespace TextT.UnicodeT
    ''' <summary>Represents single Unicode code point and provides information about it</summary>
    ''' <remarks><note>XML serialization attributes used to decorate properties of this class are not intended for XML serialization, they are rather intended as machine-readable documentation where the property originates from in UCD XML.</note></remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public MustInherit Class UnicodeCodePoint : Inherits UnicodePropertiesProvider
        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeCodePoint"/> class</summary>
        ''' <param name="element">A XML element which stores the properties</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null.</exception>
        Protected Sub New(element As XElement)
            MyBase.New(element)
        End Sub

        ''' <summary>When overriden in derived class gets type of this code point</summary>
        <LCategory(GetType(UnicodeResources), "propcat_other_Infrastructure", "Infrastructure"), LDisplayName(GetType(UnicodeResources), "d_other_Type")>
        Public MustOverride ReadOnly Property Type As UnicodeCodePointType

        ''' <summary>Creates an instance of <see cref="UnicodeCodePoint"/>-derived class from XML element</summary>
        ''' <param name="element">An XML element representing Unicode code point</param>
        ''' <returns>An instance of <see cref="UnicodeCodePoint"/>-derived class. Which? It depends on name of <paramref name="element"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is nulll</exception>
        ''' <exception cref="ArgumentException"><paramref name="element"/> is neither &lt;reserved>, &lt;noncharacter>, &lt;surrogate> nor &lt;char> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Shared Function Create(element As XElement) As UnicodeCodePoint
            If element Is Nothing Then Throw New ArgumentNullException("element")
            Select Case element.Name
                Case ReservedUnicodeCodePoint.elementName : Return New ReservedUnicodeCodePoint(element)
                Case UnicodeNoncharacter.elementName : Return New UnicodeNoncharacter(element)
                Case UnicodeSurrogate.elementName : Return New UnicodeSurrogate(element)
                Case UnicodeCharacter.elementName : Return New UnicodeCharacter(element)
                Case Else : Throw New ArgumentException(ResourcesT.Exceptions.UnsupportedElement.f(element.Name))
            End Select
        End Function

        ''' <summary>Creates an instance of <see cref="UnicodeCodePoint"/>-derived class from XML element and sets its group</summary>
        ''' <param name="element">An XML element representing Unicode code point</param>
        ''' <param name="group">A group code point is defined in</param>
        ''' <returns>An instance of <see cref="UnicodeCodePoint"/>-derived class. Which? It depends on name of <paramref name="element"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is nulll</exception>
        ''' <exception cref="ArgumentException"><paramref name="element"/> is neither &lt;reserved>, &lt;noncharacter>, &lt;surrogate> nor &lt;char> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Shared Function Create(element As XElement, group As UnicodeCharacterGroup) As Object
            If element Is Nothing Then Throw New ArgumentNullException("element")
            Select Case element.Name
                Case ReservedUnicodeCodePoint.elementName : Return New ReservedUnicodeCodePoint(element) With {.Group = group}
                Case UnicodeNoncharacter.elementName : Return New UnicodeNoncharacter(element) With {.Group = group}
                Case UnicodeSurrogate.elementName : Return New UnicodeSurrogate(element) With {.Group = group}
                Case UnicodeCharacter.elementName : Return New UnicodeCharacter(element) With {.Group = group}
                Case Else : Throw New ArgumentException(ResourcesT.Exceptions.UnsupportedElement.f(element.Name))
            End Select
        End Function

        Private _group As UnicodeCharacterGroup
        ''' <summary>Gets group code point represented by this instance belongs to</summary>
        ''' <returns>A group this code point belongs to. Null if current code point does not belong to any group.</returns>
        ''' <exception cref="InvalidOperationException">In setter: <note>Setter of this property is private.</note> Property is being set and it was already set.</exception>
        ''' <remarks>If Unicode Character Database was loaded from so-called flat file no character is in a group.</remarks>
        Public Property Group As UnicodeCharacterGroup
            Get
                If _group IsNot Nothing Then Return _group
                If Element.Parent.Name = UnicodeCharacterGroup.elementName Then
                    _group = New UnicodeCharacterGroup(Element.Parent)
                    Return _group
                Else
                    Return Nothing
                End If
            End Get
            Private Set(value As UnicodeCharacterGroup)
                If _group IsNot Nothing Then Throw New InvalidOperationException(ResourcesT.Exceptions.PropertyWasAlreadySet.f("Group"))
                _group = value
            End Set
        End Property

        ''' <summary>Gets value of given property (attributes)</summary>
        ''' <param name="namespace">Name of attribute XML namespace</param>
        ''' <param name="name">Name of the property (attribute) to get value of. This is name of property (XML attribute) as used in Unicode Character Database XML.</param>
        ''' <returns>Value of the property (attribute) as string. If the attribute is not present on <see cref="Element"/> and <see cref="Group"/> is not null the attribute si searched on <see cref="Group"/>. If it's present neither on <see cref="Element"/> nor on <see cref="Group"/> null is returned.</returns>
        ''' <remarks>
        ''' If <paramref name="namespace"/> is neither null nor an empty string and extension for that namespace is registered
        ''' (i.e. <see cref="Element"/>.<see cref="XElement.Document">Document</see>.<see cref="XDocument.Annotation">Annotation</see>[<see cref="T:System.Collections.Generic.IDictionary`2"/>[<see cref="String"/>, <see cref="UnicodeCharacterDatabase"/>]] is not null and the dictionary it resturns contains key <paramref name="namespace"/>)
        ''' attempt to obtain property value from that extension <see cref="UnicodeCharacterDatabase"/> is made first.
        ''' This only works for single-code-point characters (i.e. <see cref="CodePoint"/> is not null.)
        ''' </remarks>
        ''' <seelaso cref="GetExtensions"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Overrides Function GetPropertyValue$(namespace$, name$)
            If namespace$ <> "" AndAlso CodePoint.HasValue Then
                Dim extUcd = GetExtension([namespace])
                If extUcd IsNot Nothing Then
                    Dim extCodePoint = extUcd.FindCodePoint(Me.CodePoint.Value)
                    If extCodePoint IsNot Nothing Then Return extCodePoint.GetPropertyValue([namespace], name)
                End If
            End If
            Dim attr = Element.Attribute(If([namespace] Is Nothing, CType(name, XName), XName.Get(name, namespace$)))
            If attr Is Nothing AndAlso Group IsNot Nothing Then Return Group.GetPropertyValue(namespace$, name)
            Return attr.Value
        End Function

        ''' <summary>Get value of given property (attributes) resolving or not resolving placeholders in property value</summary>
        ''' <param name="namespace">Name of attribute XML namespace</param>
        ''' <param name="name">Name of the property (attribute) to get value of. This is name of property (XML attribute) as used in Unicode Character Database XML.</param>
        ''' <param name="allowResolving">True to allow placeholder resolving, false not to allow it.</param>
        ''' <returns>Value of the property (attribute) as string. Null if the attribute is not present on <see cref="Element"/>.</returns>
        ''' <remarks>Placeholder # is replace only if <see cref="CodePoint"/> is not null.</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Overrides Function GetPropertyValue(namespace$, name$, allowResolving As Boolean) As String
            Dim ret = GetPropertyValue(name, [namespace])
            If allowResolving AndAlso ret <> "" AndAlso ret.Contains("#") AndAlso CodePoint.HasValue Then _
                Return ret.Replace("#", CodePoint.Value.ToString("X", InvariantCulture))
            Return ret
        End Function

#Region "Properties"
        ''' <summary>Gets name of the character in current version of Unicode standard</summary>
        ''' <remarks>
        ''' If specified on group or range can contain character #. When specified on individual code point, character # is replaced with value of current code point.
        ''' <para>Unicode character names are usually uppercase.</para>
        ''' <para>Underlying XML attribute is @na.</para>
        ''' </remarks>
        Public Overrides ReadOnly Property Name As String
            Get
                Dim value = MyBase.Name
                If value IsNot Nothing AndAlso value.Contains("#") AndAlso CodePoint.HasValue Then
                    Return value.Replace("#", CodePoint.Value.ToString("X", InvariantCulture))
                End If
                Return value
            End Get
        End Property

        ''' <summary>Gets the of the code point thats most useful for user</summary>
        ''' <returns>
        ''' Usually returns <see cref="Name"/>.
        ''' Exceptions (conditions are evaluated in given order):
        ''' <list type="table">
        ''' <listheader><term>Condition</term><description>Returns</description></listheader>
        ''' <item><term>Name aliases are loaded and name alias exist for current code point</term><description>First name alias; see <see cref="NameAliases"/>. This value is returned without prefix.</description></item>
        ''' <item><term><see cref="GeneralCategory"/> is <see cref="Globalization.UnicodeCategory.PrivateUse"/> (i.e. this is private use character) and <see cref="Name"/> is null or an empty string and CSUR extension is loaded and provides <see cref="CsurPropertiesProvider"/> for this code-point which provides non-null non-empty name for this code-point.</term><description><see cref="CsurPropertiesProvider.Name"/> (prefix CSUR:, localizable)</description></item>
        ''' <item><term><see cref="Name"/> is null or empty and <see cref="Name1"/> is not.</term><description><see cref="Name1"/> (prefix 1:, localizable)</description></item>
        ''' <item><term><see cref="Name"/> is null or empty and  <see cref="NamesListExtensions"/> are loaded and provide aliases</term><description>First alias form <see cref="NamesListExtensions.Aliases"/> (prefix Alias:, localizable)</description></item>
        ''' </list>
        ''' <note type="inheritinfo">Derived class may provide different for <see cref="UniversalName"/> lookup.</note>
        ''' </returns>
        Public Overridable ReadOnly Property UniversalName As String
            Get
                If NameAliases IsNot Nothing AndAlso NameAliases.Length > 0 Then Return NameAliases(0)
                If Me.GeneralCategory = Globalization.UnicodeCategory.PrivateUse AndAlso Name = "" Then _
                    If Me.Csur IsNot Nothing AndAlso Me.Csur.Name <> "" Then Return TextT.UnicodeT.UnicodeResources.prefix_CSUR & Me.Csur.Name
                If Me.Name = "" AndAlso Me.Name1 <> "" Then Return TextT.UnicodeT.UnicodeResources.prefix_Unicode1 & Name1
                If Me.Name = "" Then
                    Dim a = NamesListExtensions.Aliases(Me)
                    If a IsNot Nothing AndAlso a.Length > 0 Then Return TextT.UnicodeT.UnicodeResources.prefix_Alias & a(0)
                End If
                Return Me.Name
            End Get
        End Property

        ''' <summary>Gets name of the character the character had in version 1 of Unicode standard</summary>
        ''' <returns>Name character had in version 1 of Unicode standard (if specified; null otherwise)</returns>
        ''' <remarks>
        ''' If specified on group or range can contain character #. When specified on individual code point, character # is replaced with value of current code point.
        ''' <para>Underlying XML attribute is @na1.</para>
        ''' </remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Overrides ReadOnly Property Name1 As String
            Get
                Dim value = MyBase.Name1
                If value IsNot Nothing AndAlso value.Contains("#") AndAlso CodePoint.HasValue Then
                    Return value.Replace("#", CodePoint.Value.ToString("X", InvariantCulture))
                End If
                Return value
            End Get
        End Property

        ''' <summary>When not null specifies value of the <see cref="CodePoint"/> property used for single-character instances created from ranges</summary>
        Private _codePoint As UInteger?
        ''' <summary>Gets value of current code point</summary>
        ''' <remarks>
        ''' This property is null for character ranges. They have <see cref="FirstCodePoint"/> and <see cref="LastCodePoint"/> specified instead.
        ''' <para>This property is not CLS-compliant. Corresponding CLS-compilant property is <see cref="CodePointSigned"/>.</para>
        ''' <para>Underlying XML attributes is @cp.</para>
        ''' </remarks>
        <CLSCompliant(False), LCategory(GetType(UnicodeResources), "propcat_other_Infrastructure", "Infrastructure"), LDisplayName(GetType(UnicodeResources), "d_other_CodePoint")>
        <XmlAttribute("cp")>
        Public ReadOnly Property CodePoint As UInteger?
            Get
                If _codePoint.HasValue Then Return _codePoint
                Dim value = GetPropertyValue("cp")
                If value.IsNullOrEmpty Then Return Nothing
                Return UInteger.Parse(value, Globalization.NumberStyles.HexNumber, InvariantCulture)
            End Get
        End Property

        ''' <summary>CLS-comliant version of <see cref="CodePoint"/> ptoperty</summary>
        ''' <seelaso cref="CodePoint"/>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False), XmlIgnore()>
        Public ReadOnly Property CodePointSigned As Integer?
            Get
                Dim value = CodePoint
                If value Is Nothing Then Return Nothing
                Return value.Value.BitwiseSame
            End Get
        End Property

        ''' <summary>For range characters gets value of first code point this range starts with</summary>
        ''' <returns>Value of code point character range starts with. Null if this instance does not represent character range.</returns>
        ''' <remarks>
        ''' This property is not CLS-compliant. CLS-compliant alternative is <see cref="FirstCodePointSigned"/>.
        ''' <para>Underlying XML attribute is @first-cp.</para>
        ''' </remarks>
        <CLSCompliant(False), LCategory(GetType(UnicodeResources), "propcat_other_Infrastructure", "Infrastructure"), LDisplayName(GetType(UnicodeResources), "d_other_FirstCodePoint")>
        <XmlAttribute("first-cp")>
        Public ReadOnly Property FirstCodePoint As UInteger?
            Get
                Dim value = GetPropertyValue("first-cp")
                If value.IsNullOrEmpty Then Return Nothing
                Return UInteger.Parse(value, Globalization.NumberStyles.HexNumber, InvariantCulture)
            End Get
        End Property

        ''' <summary>For range characters gets value of last code point this range ends with</summary>
        ''' <returns>Value of code point character range starts with. Null if this instance does not represent character range.</returns>
        ''' <remarks>
        ''' This property is not CLS-compliant. CLS-compliant alternative is <see cref="LastCodePointSigned"/>.
        ''' <para>Underlying XML attribute is @last-cp.</para>
        ''' </remarks>
        <CLSCompliant(False), LCategory(GetType(UnicodeResources), "propcat_other_Infrastructure", "Infrastructure"), LDisplayName(GetType(UnicodeResources), "d_other_LastCodePoint")>
        <XmlAttribute("last-cp")>
        Public ReadOnly Property LastCodePoint As UInteger?
            Get
                Dim value = GetPropertyValue("last-cp")
                If value.IsNullOrEmpty Then Return Nothing
                Return UInteger.Parse(value, Globalization.NumberStyles.HexNumber, InvariantCulture)
            End Get
        End Property

        ''' <summary>CLS-comliant version of <see cref="FirstCodePoint"/> ptoperty</summary>
        ''' <seelaso cref="CodePoint"/>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False), XmlIgnore()>
        Public ReadOnly Property FirstCodePointSigned As Integer?
            Get
                Dim value = FirstCodePoint
                If value Is Nothing Then Return Nothing
                Return value.Value.BitwiseSame
            End Get
        End Property
        ''' <summary>CLS-comliant version of <see cref="LastCodePoint"/> ptoperty</summary>
        ''' <seelaso cref="CodePoint"/>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False), XmlIgnore()>
        Public ReadOnly Property LastCodePointSigned As Integer?
            Get
                Dim value = LastCodePoint
                If value Is Nothing Then Return Nothing
                Return value.Value.BitwiseSame
            End Get
        End Property

        ''' <summary>Gets collection of characters that forms canonic decomposition of this charatcer</summary>
        ''' <remarks>Underlying XML attribute is @dm.
        ''' <para>If this character is not range character this impementation replaces #s with <see cref="CodePoint"/>.</para></remarks>
        Public Overrides ReadOnly Property DecompositionMapping As CodePointInfoCollection
            Get
                Dim value = GetPropertyValue("dm")
                If value = "" Then Return Nothing
                If value.Contains("#") AndAlso CodePoint.HasValue Then value = value.Replace("#", CodePoint.Value.ToString("X", InvariantCulture))
                Return New CodePointInfoCollection(Element.Document, value)
            End Get
        End Property
#End Region
        ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
        ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
        Public Overrides Function ToString() As String
            Dim name As String = ""
            If Me.Name IsNot Nothing Then name = Me.Name
            Dim value As String = ""
            If Me.CodePoint.HasValue Then
                value = String.Format(InvariantCulture, "U+{0:X}", Me.CodePoint)
            ElseIf Me.FirstCodePoint.HasValue AndAlso Me.LastCodePoint.HasValue Then
                value = String.Format(InvariantCulture, "U+{0:X}-U+{1:X}", Me.FirstCodePoint, Me.LastCodePoint)
            ElseIf Me.FirstCodePoint.HasValue Then
                value = String.Format(InvariantCulture, "U+{0:X}-", Me.FirstCodePoint)
            ElseIf Me.LastCodePoint.HasValue Then
                value = String.Format(InvariantCulture, "-U+{0:X}", Me.LastCodePoint)
            End If
            If name <> "" AndAlso value <> "" Then
                Return name & " " & value
            ElseIf name <> "" OrElse value <> "" Then
                Return name & value
            End If
            Return MyBase.ToString()
        End Function

        ''' <summary>Makes single-character instance form range instance</summary>
        ''' <param name="codePoint">Code point from current range to make single-character instance pointing to</param>
        ''' <returns>
        ''' A new instance of <see cref="UnicodeCodePoint"/>-derived class pointing to single character within range represented by current instance.
        ''' In case <see cref="CodePoint"/> is not null and <paramref name="codePoint"/> equals <see cref="CodePoint"/> returns current instance.
        ''' </returns>
        ''' <exception cref="InvalidOperationException">
        ''' Current instance is not character range and <paramref name="codePoint"/> differs from <see cref="CodePoint"/> -or-
        ''' The range is specified incorrectly (i.e. either <see cref="FirstCodePoint"/> or <see cref="LastCodePoint"/> is null.
        ''' </exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="codePoint"/> is less than <see cref="FirstCodePoint"/> or greater than <see cref="LastCodePoint"/>.</exception>
        ''' <remarks>This function is not CLS-compliant. CLS-compliant overload exists.</remarks>
        <CLSCompliant(False)>
        Public Function MakeSingle(codePoint As UInteger) As UnicodeCodePoint
            If Me.CodePoint.HasValue AndAlso codePoint = Me.CodePoint.Value Then Return Me
            If Me.CodePoint.HasValue Then Throw New InvalidOperationException(UnicodeResources.ex_MakeSingleFromSingle)
            If Me.FirstCodePoint Is Nothing OrElse Me.LastCodePoint Is Nothing Then Throw New InvalidOperationException(UnicodeResources.ex_RangeNotFullySpecified)
            If codePoint < Me.FirstCodePoint.Value OrElse codePoint > Me.LastCodePoint.Value Then Throw New ArgumentOutOfRangeException("codePoint")
            Dim clone = Create(Element)
            If _group IsNot Nothing Then clone.Group = _group
            clone._codePoint = codePoint
            Return clone
        End Function
        ''' <summary>Makes single-character instance form range instance</summary>
        ''' <param name="codePoint">Code point from current range to make single-character instance pointing to</param>
        ''' <returns>
        ''' A new instance of <see cref="UnicodeCodePoint"/>-derived class pointing to single character within range represented by current instance.
        ''' In case <see cref="CodePoint"/> is not null and <paramref name="codePoint"/> equals <see cref="CodePointSigned"/> returns current instance.
        ''' </returns>
        ''' <exception cref="InvalidOperationException">
        ''' Current instance is not character range and <paramref name="codePoint"/> differs from <see cref="CodePointSigned"/> -or-
        ''' The range is specified incorrectly (i.e. either <see cref="FirstCodePoint"/> or <see cref="LastCodePoint"/> is null.
        ''' </exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="codePoint"/> is less than <see cref="FirstCodePointSigned"/> or greater than <see cref="LastCodePointSigned"/>.</exception>
        <CLSCompliant(False)>
        Public Function MakeSingle(codePoint As Integer) As UnicodeCodePoint
            Return MakeSingle(codePoint.BitwiseSame)
        End Function

        ''' <summary>Gets <see cref="CodePointInfo"/> instance pointing to current character</summary>
        ''' <returns>A new <see cref="CodePointInfo"/> instance pointing to charatcer indicated by <see cref="CodePoint"/> property.</returns>
        ''' <exception cref="InvalidOperationException">This instance represents character range.</exception>
        Public Function AsCodePointInfo() As CodePointInfo
            If Not CodePoint.HasValue Then Throw New InvalidOperationException(UnicodeResources.ex_OperationIsNotValidForCharacterRanges)
            Return New CodePointInfo(Element.Document, CodePoint.Value)
        End Function

        ''' <summary>Converts <see cref="UnicodeCodePoint"/> to <see cref="CodePointInfo"/></summary>
        ''' <param name="a">A <see cref="UnicodeCodePoint"/></param>
        ''' <returns>A new instance of <see cref="CodePointInfo"/> class pointing to same character as <paramref name="a"/>; null if <paramref name="a"/> is null.</returns>
        ''' <exception cref="InvalidOperationException"><paramref name="a"/> is character range.</exception>
        ''' <seelaso cref="AsCodePointInfo"/>
        Public Shared Narrowing Operator CType(a As UnicodeCodePoint) As CodePointInfo
            If a Is Nothing Then Return Nothing
            Return a.AsCodePointInfo
        End Operator

        ''' <summary>Converts <see cref="CodePointInfo"/> to <see cref="UnicodeCodePoint"/></summary>
        ''' <param name="a">A <see cref="CodePointInfo"/></param>
        ''' <returns>A new instance of <see cref="UnicodeCodePoint"/> providing information about character pointed by <paramref name="a"/>; null if <paramref name="a"/> is null.</returns>
        ''' <exception cref="InvalidOperationException"><paramref name="a"/> was initialized without instance of <see cref="XDocument"/> -or- Information about code point pointed by <paramref name="a"/> cannot be found in the XML document.</exception>
        Public Shared Widening Operator CType(a As CodePointInfo) As UnicodeCodePoint
            If a Is Nothing Then Return Nothing
            Dim ret = a.UnicodeCodePoint
            If ret Is Nothing Then Throw New InvalidOperationException(UnicodeResources.ex_CannotFindCodePoint)
            Return ret
        End Operator

        ''' <summary>Gets a Unicode block this code-point belongs to</summary>
        ''' <returns>
        ''' A Unicode block this code-point belongs to, null if corresponding block cannot be found.
        ''' In case <see cref="CodePoint"/> is null and <see cref="FirstCodePoint"/> and <see cref="LastCodePoint"/> are specified the block is returned only in case both - <see cref="FirstCodePoint"/> and <see cref="LastCodePoint"/> belong to the same block.
        ''' If <see cref="CodePoint"/> is null and also either <see cref="FirstCodePoint"/> or <see cref="LastCodePoint"/> is null, this function always returns null.
        ''' </returns>
        ''' <exception cref="InvalidOperationException">The data in UCD XML are invalid - see <see cref="Exception.InnerException"/> for details. See <see cref="M:Tools.TextT.UnicodeT.UnicodeCharacterDatabase.FindBlock(System.Collections.Generic.IEnumerable`1[System.Xml.Linq.XElement],System.UInt32,System.Boolean)"/> for detailed explanation of <see cref="Exception.InnerException"/>.</exception>
        ''' <seelaso cref="UnicodeCharacterDatabase.FindBlock"/>
        <Browsable(False), XmlIgnore()>
        Public ReadOnly Property Block As UnicodeBlock
            Get
                If CodePoint.HasValue Then
                    Dim bel As XElement
                    Try
                        bel = UnicodeCharacterDatabase.FindBlock(Element.Document.<ucd>.<blocks>.<block>, CodePoint.Value, False)
                    Catch ex As Exception When TypeOf ex Is ArgumentNullException OrElse TypeOf ex Is FormatException OrElse TypeOf ex Is OverflowException
                        Throw New InvalidOperationException(ex.Message, ex)
                    End Try
                    If bel IsNot Nothing Then Return New UnicodeBlock(bel)
                ElseIf FirstCodePoint.HasValue AndAlso LastCodePoint.HasValue Then
                    Dim bel1 As XElement
                    Dim bel2 As XElement
                    Try
                        bel1 = UnicodeCharacterDatabase.FindBlock(Element.Document.<ucd>.<blocks>.<block>, FirstCodePoint.Value, False)
                        bel2 = UnicodeCharacterDatabase.FindBlock(Element.Document.<ucd>.<blocks>.<block>, LastCodePoint.Value, False)
                    Catch ex As Exception When TypeOf ex Is ArgumentNullException OrElse TypeOf ex Is FormatException OrElse TypeOf ex Is OverflowException
                        Throw New InvalidOperationException(ex.Message, ex)
                    End Try
                    If bel1 Is bel2 Then Return New UnicodeBlock(bel1)
                End If
                Return Nothing
            End Get
        End Property

        ''' <summary>Gets normative name aliases for name of this code point</summary>
        ''' <returns>
        ''' Name aliases (alternative normative names) for this code-point.
        ''' Retturns null if <see cref="CodePoint"/> is null or if name aliases are not registered.
        ''' Returns an empty array if there ane no formal aliases for name of current code point.
        ''' </returns>
        ''' <remarks>This property returns non-null values only if NameAliases.txt textual extension was registered for parent UCD</remarks>
        ''' <seelaso cref="UnicodeCharacterDatabase.NameAliases"/>
        Public ReadOnly Property NameAliases As String()
            Get
                If Not CodePoint.HasValue Then Return Nothing
                Dim aliases = TryCast(GetTextualExtension("NameAliases.txt"), Dictionary(Of UInteger, String()))
                If aliases Is Nothing Then Return Nothing
                Dim aarr As String() = Nothing
                If aliases.TryGetValue(CodePoint.Value, aarr) Then Return aarr Else Return New String() {}
            End Get
        End Property
    End Class

    ''' <summary>Enumerates Unicode code point types</summary>
    ''' <version version="1.5.4">This enum is new in version 1.5.4</version>
    Public Enum UnicodeCodePointType
        ''' <summary>Reserved code point (i.e. it's not assigned to a character in current version of Unicode standard)</summary>
        Reserved
        ''' <summary>Non-character code point</summary>
        Noncharacter
        ''' <summary>Surrogate code point</summary>
        Surrogate
        ''' <summary>Character code point</summary>
        Character
    End Enum

    ''' <summary>Represents reserved Unicode code point (i.e. code point that's currently not assigned to a character)</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class ReservedUnicodeCodePoint : Inherits UnicodeCodePoint
        ''' <summary>Name of element representing this class in XML</summary>
        Friend Shared ReadOnly elementName As XName = <reserved/>.Name
        ''' <summary>CTor - creates a new instance of the <see cref="ReservedUnicodeCodePoint"/> class</summary>
        ''' <param name="element">An XML element containing data for this unicode code point</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="element"/> is not &lt;reserved> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Sub New(element As XElement)
            MyBase.New(element)
            If element.Name <> elementName Then Throw New ArgumentException(ResourcesT.Exceptions.ElementMustBe.f(elementName))
        End Sub
        ''' <summary>Gets type of this code point</summary>
        ''' <returns>This implementation always returns <see cref="UnicodeCodePointType.Reserved"/></returns>
        Public NotOverridable Overrides ReadOnly Property Type As UnicodeCodePointType
            Get
                Return UnicodeCodePointType.Reserved
            End Get
        End Property
    End Class

    ''' <summary>Represents non-character code point in Unicode</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class UnicodeNoncharacter : Inherits UnicodeCodePoint
        ''' <summary>Name of element representing this class in XML</summary>
        Friend Shared ReadOnly elementName As XName = <noncharacter/>.Name
        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeNoncharacter"/> class</summary>
        ''' <param name="element">An XML element containing data for this unicode code point</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="element"/> is not &lt;noncharacter> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Sub New(element As XElement)
            MyBase.New(element)
            If element.Name <> elementName Then Throw New ArgumentException(ResourcesT.Exceptions.ElementMustBe.f(elementName))
        End Sub
        ''' <summary>Gets type of this code point</summary>
        ''' <returns>This implementation always returns <see cref="UnicodeCodePointType.Noncharacter"/></returns>
        Public NotOverridable Overrides ReadOnly Property Type As UnicodeCodePointType
            Get
                Return UnicodeCodePointType.Noncharacter
            End Get
        End Property
    End Class

    ''' <summary>Represents surrogate code point in Unicode</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class UnicodeSurrogate : Inherits UnicodeCodePoint
        ''' <summary>Name of element representing this class in XML</summary>
        Friend Shared ReadOnly elementName As XName = <surrogate/>.Name
        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeSurrogate"/> class</summary>
        ''' <param name="element">An XML element containing data for this unicode code point</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="element"/> is not &lt;surrogate> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Sub New(element As XElement)
            MyBase.New(element)
            If element.Name <> elementName Then Throw New ArgumentException(ResourcesT.Exceptions.ElementMustBe.f(elementName))
        End Sub
        ''' <summary>Gets type of this code point</summary>
        ''' <returns>This implementation always returns <see cref="UnicodeCodePointType.Surrogate"/></returns>
        Public NotOverridable Overrides ReadOnly Property Type As UnicodeCodePointType
            Get
                Return UnicodeCodePointType.Surrogate
            End Get
        End Property
    End Class

    ''' <summary>Represents character code point in Unicode</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Class UnicodeCharacter : Inherits UnicodeCodePoint
        ''' <summary>Name of element representing this class in XML</summary>
        Friend Shared ReadOnly elementName As XName = <char/>.Name
        ''' <summary>CTor - creates a new instance of the <see cref="UnicodeCharacter"/> class</summary>
        ''' <param name="element">An XML element containing data for this unicode code point</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null</exception>
        ''' <exception cref="ArgumentException"><paramref name="element"/> is not &lt;char> in namespace http://www.unicode.org/ns/2003/ucd/1.0.</exception>
        Public Sub New(element As XElement)
            MyBase.New(element)
            If element.Name <> elementName Then Throw New ArgumentException(ResourcesT.Exceptions.ElementMustBe.f(elementName))
        End Sub
        ''' <summary>Gets type of this code point</summary>
        ''' <returns>This implementation always returns <see cref="UnicodeCodePointType.Character"/></returns>
        Public NotOverridable Overrides ReadOnly Property Type As UnicodeCodePointType
            Get
                Return UnicodeCodePointType.Character
            End Get
        End Property
    End Class
End Namespace
