Imports Tools.ComponentModelT
Imports Tools.ExtensionsT
Imports System.Xml.Linq
Imports System.Globalization.CultureInfo
Imports Tools.NumericsT
Imports System.Xml.Serialization

'List of all properties http://www.unicode.org/reports/tr44/#Properties
'XML format specification http://www.unicode.org/reports/tr42/
'Property value aliases http://www.unicode.org/Public/6.0.0/ucd/PropertyValueAliases.txt

Namespace TextT.UnicodeT
    ''' <summary>Common base class for Unicode code points and groups. This class holds character properties</summary>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public MustInherit Class UnicodePropertiesProvider : Implements IXElementWrapper
        Private _element As XElement
        ''' <summary>CTor - creates a new instance of the <see cref="UnicodePropertiesProvider"/> class</summary>
        ''' <param name="element">A XML element which stores the properties</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null.</exception>
        Protected Sub New(element As XElement)
            If element Is Nothing Then Throw New ArgumentNullException("element")
            _element = element
        End Sub

        ''' <summary>Gets XML element this instance wraps</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property Element As XElement Implements IXElementWrapper.Element
            Get
                Return _element
            End Get
        End Property

        ''' <summary>Gets node this instance wraps</summary>
        Private ReadOnly Property IXNodeWrapper_Node As XNode Implements IXNodeWrapper.Node
            Get
                Return Element
            End Get
        End Property

        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="UnicodePropertiesProvider" />.</summary>
        ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="UnicodePropertiesProvider" /> (it's <see cref="UnicodePropertiesProvider"/> and its backed by the same (reference equivalence) <see cref="Element"/>); otherwise, false.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="UnicodePropertiesProvider" />. </param>
        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is UnicodePropertiesProvider Then Return Element Is DirectCast(obj, UnicodePropertiesProvider).Element
            Return MyBase.Equals(obj)
        End Function

        ''' <summary>Gets value of given property (attributes)</summary>
        ''' <param name="name">Name of the property (attribute) to get value of. This is name of property (XML attribute) as used in Unicode Character Database XML.</param>
        ''' <returns>Value of the property (attribute) as string. Null if the attribute is not present on <see cref="Element"/>.</returns>
        ''' <remarks>Derived class may provide fallback logic for providing property values when the property is not defined on <see cref="Element"/>.</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Overridable Function GetPropertyValue$(name$)
            Dim attr = Element.Attribute(name)
            If attr Is Nothing Then Return Nothing
            Return attr.Value
        End Function

        ''' <summary>When overriden in derived class get value of given property (attributes) resolving or not resolving placeholders in property value</summary>
        ''' <param name="name">Name of the property (attribute) to get value of. This is name of property (XML attribute) as used in Unicode Character Database XML.</param>
        ''' <param name="allowResolving">True to allow placeholder resolving, false not to allow it. Only used if overriden in derived class. This implementation never resolves placholders.</param>
        ''' <returns>Value of the property (attribute) as string. Null if the attribute is not present on <see cref="Element"/>.</returns>
        ''' <remarks>Derived class may provide fallback logic for providing property values when the property is not defined on <see cref="Element"/>.
        ''' <para>
        ''' This implementation simply calls <see cref="M:Tools.TextT.UnicodeT.UnicodePropertiesProvider.GetPropertyValue(System.String)"/> overload not resolving any placeholders (ignoring <paramref name="allowResolving"/>).
        ''' When derived class implements resolving it's not mandatory to happen. Derived class implementation resolves placeholders if it can and leaves them in property value if it cannot resolve them.
        ''' </para></remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Protected Overridable Function GetPropertyValue$(name$, allowResolving As Boolean)
            Return GetPropertyValue(name)
        End Function

#Region "Properties"
        ''' <summary>Gets version of Unicode in which a code point was assigned to an abstract character, or made surrogate or non-character</summary>
        ''' <returns>Version of Unicode standard or null. Null is retuened also when underlying XML attribute has value "unassigned".</returns>
        ''' <remarks>Unicode standard defines values this property can have (i.e. it cannot have any version number and typically only <see cref="Version.Major"/> and <see cref="Version.Minor"/> numbers are used.
        ''' <para>Underlying XML attribute is @age.</para></remarks>
        <XmlAttribute("age")>
        Public ReadOnly Property Age As Version
            Get
                Dim value = GetPropertyValue("age")
                If value.IsNullOrEmpty OrElse value = "unassigned" Then Return Nothing
                Return Version.Parse(value)
            End Get
        End Property

        ''' <summary>Gets name of the character in current version of Unicode standard</summary>
        ''' <remarks>
        ''' If specified on group or range can contain character #. When specified on individual code point, character # is replaced with value of current code point.
        ''' <para>Unicode character names are usually uppercase.</para>
        ''' <para>Underlying XML attribute is @na.</para>
        ''' </remarks>
        <XmlAttribute("na")>
        Public Overridable ReadOnly Property Name As String
            Get
                Return GetPropertyValue("na")
            End Get
        End Property

        ''' <summary>Gets name of the character the character had in version 1 of Unicode standard</summary>
        ''' <returns>Name character had in version 1 of Unicode standard (if specified; null otherwise)</returns>
        ''' <remarks>
        ''' If specified on group or range can contain character #. When specified on individual code point, character # is replaced with value of current code point.
        ''' <para>Underlying XML attribute is @na1.</para>
        ''' </remarks>
        <XmlAttribute("na1")>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Overridable ReadOnly Property Name1 As String
            Get
                Return GetPropertyValue("na1")
            End Get
        End Property

        ''' <summary>Gets general category of code point</summary>
        ''' <value>Default value when not assigned in Unicode Character Database is <see cref="Globalization.UnicodeCategory.OtherNotAssigned"/></value>
        ''' <exception cref="InvalidOperationException">Value of underlying attribute cannot be mapped to <see cref="Globalization.UnicodeCategory"/> enumeration value.</exception>
        ''' <remarks>Underlying XML attribute is @gc.</remarks>
        ''' <seelaso cref="System.Char.GetUnicodeCategory"/>
        <XmlAttribute("gc")>
        <DefaultValue(Globalization.UnicodeCategory.OtherNotAssigned)>
        Public ReadOnly Property GeneralCategory As Globalization.UnicodeCategory
            Get
                Dim value = GetPropertyValue("gc")
                If value = "" Then Return Globalization.UnicodeCategory.OtherNotAssigned
                Select Case value
                    Case "Lu" : Return Globalization.UnicodeCategory.UppercaseLetter
                    Case "Ll" : Return Globalization.UnicodeCategory.LowercaseLetter
                    Case "Lt" : Return Globalization.UnicodeCategory.TitlecaseLetter
                    Case "Lm" : Return Globalization.UnicodeCategory.ModifierLetter
                    Case "Lo" : Return Globalization.UnicodeCategory.OtherLetter

                    Case "Mn" : Return Globalization.UnicodeCategory.NonSpacingMark
                    Case "Mc" : Return Globalization.UnicodeCategory.SpacingCombiningMark
                    Case "Me" : Return Globalization.UnicodeCategory.EnclosingMark

                    Case "Nd" : Return Globalization.UnicodeCategory.DecimalDigitNumber
                    Case "Nl" : Return Globalization.UnicodeCategory.LetterNumber
                    Case "No" : Return Globalization.UnicodeCategory.OtherNumber

                    Case "Pc" : Return Globalization.UnicodeCategory.ConnectorPunctuation
                    Case "Pd" : Return Globalization.UnicodeCategory.DashPunctuation
                    Case "Ps" : Return Globalization.UnicodeCategory.OpenPunctuation
                    Case "Pe" : Return Globalization.UnicodeCategory.ClosePunctuation
                    Case "Pi" : Return Globalization.UnicodeCategory.InitialQuotePunctuation
                    Case "Pf" : Return Globalization.UnicodeCategory.FinalQuotePunctuation
                    Case "Po" : Return Globalization.UnicodeCategory.OtherPunctuation

                    Case "Sm" : Return Globalization.UnicodeCategory.MathSymbol
                    Case "Sc" : Return Globalization.UnicodeCategory.CurrencySymbol
                    Case "Sk" : Return Globalization.UnicodeCategory.ModifierSymbol
                    Case "So" : Return Globalization.UnicodeCategory.OtherSymbol

                    Case "Zs" : Return Globalization.UnicodeCategory.SpaceSeparator
                    Case "Zl" : Return Globalization.UnicodeCategory.LineSeparator
                    Case "Zp" : Return Globalization.UnicodeCategory.ParagraphSeparator

                    Case "Cc" : Return Globalization.UnicodeCategory.Control
                    Case "Cf" : Return Globalization.UnicodeCategory.Format
                    Case "Cs" : Return Globalization.UnicodeCategory.Surrogate
                    Case "Co" : Return Globalization.UnicodeCategory.PrivateUse
                    Case "Cn" : Return Globalization.UnicodeCategory.OtherNotAssigned
                    Case Else : Throw New InvalidOperationException(ResourcesT.Exceptions.UnsuppportedGeneralCategory.f(value))
                End Select
            End Get
        End Property

        ''' <summary>Gets generalized unicode category of code point</summary>
        ''' <value>Default value if <see cref="GeneralCategory"/> is not assigned in Unicode Character Database is <see cref="UnicodeGeneralCategoryClass.Other"/>.</value>
        ''' <seelaso cref="UnicodeExtensions.GetClass"/>
        <XmlIgnore()>
        <DefaultValue(UnicodeGeneralCategoryClass.Other)>
        Public ReadOnly Property GeneralCategoryClass As UnicodeGeneralCategoryClass
            Get
                Return GeneralCategory.GetClass
            End Get
        End Property

        ''' <summary>Gets combining class of the character</summary>
        ''' <remarks>Underlying XML attribute is @ccc.</remarks>
        <XmlAttribute("ccc")>
        Public ReadOnly Property CombiningClass As UnicodeCombiningClass
            Get
                Dim value = GetPropertyValue("ccc")
                If value = "" Then Return 0
                Return Byte.Parse(value, InvariantCulture)
            End Get
        End Property

#Region "Bidi"
        ''' <summary>Gets bidirectional category of the character</summary>
        ''' <returns>Unicode bidirectional category specified for current character. Null if bidi class is not specified in Unicode Character Database - in this case Unicode Bidirectional Alghoritm should be used to determine default value of bidi class of character.</returns>
        ''' <remarks>Underlying XML attributes is @bc</remarks>
        ''' <exception cref="InvalidOperationException">Underlying XML attribute value cannot be mapped to <see cref="UnicodeBidiCategory"/> value</exception>
        <XmlAttribute("bc")>
        Public ReadOnly Property BidiCategory As UnicodeBidiCategory?
            Get
                Dim value = GetPropertyValue("bc")
                If value = "" Then Return Nothing
                Try
                    Return GetUnicodeBidiCategory(value)
                Catch ex As ArgumentException
                    Throw New InvalidOperationException(ResourcesT.Exceptions.UnsupportedBidirectionalCategory.f(value), ex)
                End Try
            End Get
        End Property

        ''' <summary>Gets <see cref="UnicodeBidiCategory"/> value form its string abbreviated representation</summary>
        ''' <param name="value">Abbreviated string representation of Unicode bidi category</param>
        ''' <returns>A <see cref="UnicodeBidiCategory"/> value</returns>
        ''' <exception cref="ArgumentException"><paramref name="value"/> is not one of known abbreviations of <see cref="UnicodeBidiCategory"/> values.</exception>
        Private Shared Function GetUnicodeBidiCategory(ByVal value As String) As UnicodeBidiCategory
            Select Case value
                Case "AL" : Return UnicodeBidiCategory.RightToLeftArabic
                Case "AN" : Return UnicodeBidiCategory.ArabicNumber
                Case "B" : Return UnicodeBidiCategory.ParagraphSeparator
                Case "BN" : Return UnicodeBidiCategory.BoundaryNeutral
                Case "CS" : Return UnicodeBidiCategory.CommonNumberSeparator
                Case "EN" : Return UnicodeBidiCategory.EuropeanNumber
                Case "ES" : Return UnicodeBidiCategory.EuropeanNumberSeparator
                Case "ET" : Return UnicodeBidiCategory.EuropeanNumberTerminator
                Case "L" : Return UnicodeBidiCategory.LeftToRight
                Case "LRE" : Return UnicodeBidiCategory.LeftToRightEmbedding
                Case "LRO" : Return UnicodeBidiCategory.LeftToRightOverride
                Case "NSM" : Return UnicodeBidiCategory.NonSpacingMark
                Case "ON" : Return UnicodeBidiCategory.OtherNeutrals
                Case "PDF" : Return UnicodeBidiCategory.PopDirectionalFormat
                Case "R" : Return UnicodeBidiCategory.RightToLeft
                Case "RLE" : Return UnicodeBidiCategory.RightToLeftEmbedding
                Case "RLO" : Return UnicodeBidiCategory.RightToLeftOverride
                Case "S" : Return UnicodeBidiCategory.SegmentSeparator
                Case "WS" : Return UnicodeBidiCategory.Whitespace
                Case Else : Throw New ArgumentException(ResourcesT.Exceptions.UnsupportedBidirectionalCategory.f(value), "value")
            End Select
        End Function

        ''' <summary>Gets bidirectional strenght of the character</summary>
        <XmlIgnore()>
        Public ReadOnly Property BidiStrength As UnicodeBidiCategoryStrenght?
            Get
                Dim value = BidiCategory
                If value.HasValue Then Return value.Value.GetStrength Else Return Nothing
            End Get
        End Property

        ''' <summary>Gets value indicating if the character should be mirrored horizontally when rendering in right-to-left text</summary>
        ''' <remarks>Note that for some characters the mirroring is not exact mirroring but e.g. mirroring only of part of a glyph.
        ''' <para>Underlying XML attributes is @Bidi_M.</para></remarks>
        <XmlAttribute("Bidi_M")>
        Public ReadOnly Property Mirrored As Boolean
            Get
                Return GetPropertyValue("Bidi_M") = "Y"
            End Get
        End Property

        ''' <summary>Gets a code point of glyph that is typically mirrored version of this glyph when used in right-to-left text</summary>
        ''' <remarks>Underlying XML attribute is @bmg.</remarks>
        <XmlAttribute("bmg")>
        Public ReadOnly Property BidiMirroringGlyph As CodePointInfo
            Get
                Dim value = GetPropertyValue("bmg")
                If value = "" Then Return Nothing
                Return New CodePointInfo(Element.Document, UInteger.Parse("0x" & value, Globalization.NumberStyles.HexNumber, InvariantCulture))
            End Get
        End Property

        ''' <summary>Gets value indicating if code point is bidirectional control character</summary>
        ''' <remarks>Underlying XML attribute is @Bidi_C</remarks>
        <XmlAttribute("Bidi_C")>
        Public ReadOnly Property BidiControl As Boolean?
            Get
                Dim value = GetPropertyValue("Bidi_C")
                If value = "" Then Return Nothing
                Return value = "Y"
            End Get
        End Property
#End Region
#Region "Decomposition"

        ''' <summary>Gets decomposition type of character</summary>
        ''' <remarks>Underlying XML atttributes is @dt.</remarks>
        <XmlAttribute("dt"), DefaultValue(UnicodeDecompositionType.none)>
        Public ReadOnly Property DecompositionType As UnicodeDecompositionType?
            Get
                Dim value = GetPropertyValue("dt")
                If value = "" Then Return UnicodeDecompositionType.none
                Select Case value
                    Case "can" : Return UnicodeDecompositionType.Canonical
                    Case "com" : Return UnicodeDecompositionType.Compatibility
                    Case "enc" : Return UnicodeDecompositionType.Circle
                    Case "fin" : Return UnicodeDecompositionType.Final
                    Case "font" : Return UnicodeDecompositionType.Font
                    Case "fra" : Return UnicodeDecompositionType.Fraction
                    Case "init" : Return UnicodeDecompositionType.Initial
                    Case "iso" : Return UnicodeDecompositionType.Isolated
                    Case "med" : Return UnicodeDecompositionType.Medial
                    Case "nar" : Return UnicodeDecompositionType.Narrow
                    Case "nb" : Return UnicodeDecompositionType.NoBreak
                    Case "sml" : Return UnicodeDecompositionType.Small
                    Case "sqr" : Return UnicodeDecompositionType.Square
                    Case "sub" : Return UnicodeDecompositionType.Sub
                    Case "sup" : Return UnicodeDecompositionType.Super
                    Case "vert" : Return UnicodeDecompositionType.Vertical
                    Case "wide" : Return UnicodeDecompositionType.Wide
                    Case "none" : Return UnicodeDecompositionType.none
                    Case Else : Throw New InvalidOperationException(ResourcesT.Exceptions.CannotBeInterpretedAs1.f(value, GetType(UnicodeDecompositionType).Name))
                End Select
            End Get
        End Property

        ''' <summary>Gets collection of characters that forms canonic decomposition of this charatcer</summary>
        ''' <remarks>Underlying XML attribute is @dm.</remarks>
        <XmlAttribute("dm")>
        Public Overridable ReadOnly Property DecompositionMapping As CodePointInfoCollection
            Get
                Dim value = GetPropertyValue("dm")
                If value = "" Then Return Nothing
                Return New CodePointInfoCollection(Element.Document, value)
            End Get
        End Property

        ''' <summary>Gets value indicating if character is explicitly excluded from composition. This information comes from CompositionExclusions.txt</summary>
        ''' <remarks>
        ''' Underlying XML attributes is @CE.
        ''' <para>See <a href="http://www.unicode.org/reports/tr15/#Primary_Exclusion_List_Table">UAX #15</a> for details.</para>
        ''' <para>There are more reasons for character to be excluded from composition than just CompositionExclusions.txt. All the posibilities are recorded in <see cref="FullCompositionExclusion"/>.</para>
        ''' </remarks>
        ''' <seelaso cref="FullCompositionExclusion"/>
        <XmlAttribute("CE")>
        Public ReadOnly Property CompositionExclusion As Boolean
            Get
                Return GetPropertyValue("CE") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if character is explicitly or otherwise excluded from composition. This information comes from DerivedNormalizationProps.txt</summary>
        ''' <remarks>Underlying XML attributes is @Comp_Ex. <para>If you are looking only for explicit composition exclusions see <see cref="CompositionExclusion"/>.</para></remarks>
        ''' <seelaso cref="CompositionExclusion"/>
        <XmlAttribute("Comp_Ex")>
        Public ReadOnly Property FullCompositionExclusion As Boolean
            Get
                Return GetPropertyValue("Comp_Ex") = "Y"
            End Get
        End Property
#Region "Normalization form quick check"
        ''' <summary>Gets value indicating if character never (false), sometimes (null) or always (true) appears in normalization from C</summary>
        ''' <exception cref="InvalidOperationException">Value of underlying XML attribute is newither N nor M nor Y.</exception>
        ''' <remarks>Underlying XML attribute is @NFC_QC</remarks>
        <XmlAttribute("NFC_QC"), DefaultValue(True)>
        Public ReadOnly Property NormalizationFormCQuickCheck As Boolean?
            Get
                Select Case GetPropertyValue("NFC_QC")
                    Case "Y", "" : Return True
                    Case "N" : Return False
                    Case "M" : Return Nothing
                    Case Else : Throw New InvalidOperationException(ResourcesT.Exceptions.UnexpedtedValue0.f(GetPropertyValue("NFC_QC")))
                End Select
            End Get
        End Property

        ''' <summary>Gets value indicating if character always (true) or never(false) occurs in normalization from D</summary>
        ''' <remarks>Underlying XML attribute is @NFC_QD.</remarks>
        <XmlAttribute("NFD_QC"), DefaultValue(True)>
        Public ReadOnly Property NormalizationFormDQuickCheck As Boolean
            Get
                Return GetPropertyValue("NFD_QC") <> "N"
            End Get
        End Property
        ''' <summary>Gets value indicating if character always (true) or never(false) occurs in normalization from KC</summary>
        ''' <exception cref="InvalidOperationException">Value of underlying XML attribute is newither N nor M nor Y.</exception>
        ''' <remarks>Underlying XML attribute is @NFKC_QC.</remarks>
        <XmlAttribute("NFKC_QC"), DefaultValue(True)>
        Public ReadOnly Property NormalizationFormKCQuickCheck As Boolean?
            Get
                Select Case GetPropertyValue("NFC_QC")
                    Case "Y", "" : Return True
                    Case "N" : Return False
                    Case "M" : Return Nothing
                    Case Else : Throw New InvalidOperationException(ResourcesT.Exceptions.UnexpedtedValue0.f(GetPropertyValue("NFC_QC")))
                End Select
            End Get
        End Property
        ''' <summary>Gets value indicating if character always (true) or never(false) occurs in normalization from KD</summary>
        ''' <remarks>Underlying XML attribute is @NFKD_QC.</remarks>
        <XmlAttribute("NFKD_QC"), DefaultValue(True)>
        Public ReadOnly Property NormalizationFormKDQuickCheck As Boolean
            Get
                Return GetPropertyValue("NFD_QC") <> "N"
            End Get
        End Property
#End Region
#Region "Expand On Normalization From" 'Deprecated since Unicode 6.0
        ''' <summary>Gets value indicating if character expands to more than one character on normalization form C</summary>
        ''' <remarks><para>This property is deprecated as of Unicode 6.0</para>Underlying XML attribute is @XO_NFC.</remarks>
        <XmlAttribute("XO_NFC"), Obsolete("Property Expands_On_NFC is deprecated as of Unicode 6.0.0")>
        Public ReadOnly Property ExpandOnC As Boolean
            Get
                Return GetPropertyValue("XO_NFC") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if character expands to more than one character on normalization form D</summary>
        ''' <remarks><para>This property is deprecated as of Unicode 6.0</para>Underlying XML attribute is @XO_NFD.</remarks>
        <XmlAttribute("XO_NFD"), Obsolete("Property Expands_On_NFD is deprecated as of Unicode 6.0.0")>
        Public ReadOnly Property ExpandOnD As Boolean
            Get
                Return GetPropertyValue("XO_NFD") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if character expands to more than one character on normalization form KC</summary>
        ''' <remarks><para>This property is deprecated as of Unicode 6.0</para>Underlying XML attribute is @XO_NFKC.</remarks>
        <XmlAttribute("XO_NFKC"), Obsolete("Property Expands_On_NFKC is deprecated as of Unicode 6.0.0")>
        Public ReadOnly Property ExpandOnKC As Boolean
            Get
                Return GetPropertyValue("XO_NFKC") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if character expands to more than one character on normalization form KD</summary>
        ''' <remarks><para>This property is deprecated as of Unicode 6.0</para>Underlying XML attribute is @XO_NFKD.</remarks>
        <XmlAttribute("XO_NFKD"), Obsolete("Property Expands_On_NFKD is deprecated as of Unicode 6.0.0")>
        Public ReadOnly Property ExpandOnKD As Boolean
            Get
                Return GetPropertyValue("XO_NFKD") = "Y"
            End Get
        End Property
#End Region

        ''' <summary>In case character requires extra meppings for closure under Case Folding plus Normalization Form KC this property returns it.</summary>
        ''' <returns>If extra mapping is required, returns it. Otherwise returns null.</returns>
        ''' <remarks>This property is obsolete as of Unicode 6.0<para>Underlying XML attribute is @FC_NFKC.</para></remarks>
        <XmlAttribute("FC_NFKC"), Obsolete("The FC_NFKC_Closure is deprecated as of Unicode 6.0.0")>
        Public ReadOnly Property CaseFoldingClosureExtraMappingKC As CodePointInfoCollection
            Get
                Dim value = GetPropertyValue("FC_NFKC")
                If value Is Nothing Then Return Nothing
                Return New CodePointInfoCollection(Element.Document, value)
            End Get
        End Property
#End Region
#Region "Numeric"
        ''' <summary>Gets value indicating if character represents a number and if so, number of which kind it represents.</summary>
        ''' <exception cref="InvalidOperationException">Underlying XML attribute value is neither None, De, Di nor Nu.</exception>
        ''' <remarks>Underlying XML attribute is @nt.</remarks>
        <XmlAttribute("nt"), DefaultValue(UnicodeCharacterNumericType.None)>
        Public ReadOnly Property NumericType As UnicodeCharacterNumericType
            Get
                Dim value = GetPropertyValue("nt")
                If value = "" Then Return UnicodeCharacterNumericType.None
                Select Case value
                    Case "None" : Return UnicodeCharacterNumericType.None
                    Case "De" : Return UnicodeCharacterNumericType.Decimal
                    Case "Di" : Return UnicodeCharacterNumericType.Digit
                    Case "Nu" : Return UnicodeCharacterNumericType.Numeric
                    Case Else : Throw New InvalidOperationException(ResourcesT.Exceptions.UnexpedtedValue0.f(value))
                End Select
            End Get
        End Property

        ''' <summary>Gets numeric value assigned to a character (if any)</summary>
        ''' <remarks>Underlying XML attributes id @nv.</remarks>
        <XmlAttribute("nv")>
        Public ReadOnly Property NumericValue As SRational?
            Get
                If NumericType = UnicodeCharacterNumericType.None Then Return Nothing
                Dim value = GetPropertyValue("nv")
                If value = "" Then Return Nothing
                Return SRational.Parse(value, Globalization.NumberStyles.Integer, InvariantCulture)
            End Get
        End Property
#End Region
#Region "Joining"
        ''' <summary>Gets joining type of a character (for Arabic and other Middle-Eastern characters)</summary>
        ''' <value>Default value is either <see cref="UnicodeJoiningType.Transparent"/> (for characters of <see cref="GeneralCategory"/> <see cref="Globalization.UnicodeCategory.NonSpacingMark"/>, <see cref="Globalization.UnicodeCategory.EnclosingMark"/> or <see cref="Globalization.UnicodeCategory.Format"/>) or <see cref="UnicodeJoiningType.NonJoining"/> (all others).</value>
        ''' <exception cref="InvalidOperationException">Value of underlying XML attribue is neither of: R, L, D, C, U, T</exception>
        ''' <remarks>Underlying XML attribute is @jt.</remarks>
        <XmlAttribute("jt")>
        Public ReadOnly Property JoiningType As UnicodeJoiningType
            Get
                Dim value = GetPropertyValue("jt")
                If value = "" Then
                    Select Case GeneralCategory
                        Case Globalization.UnicodeCategory.NonSpacingMark, Globalization.UnicodeCategory.EnclosingMark, Globalization.UnicodeCategory.Format
                            Return UnicodeJoiningType.Transparent
                        Case Else : Return UnicodeJoiningType.NonJoining
                    End Select
                End If
                Select Case value
                    Case "R" : Return UnicodeJoiningType.Right
                    Case "L" : Return UnicodeJoiningType.Left
                    Case "D" : Return UnicodeJoiningType.Dual
                    Case "C" : Return UnicodeJoiningType.JoinCausing
                    Case "U" : Return UnicodeJoiningType.NonJoining
                    Case "T" : Return UnicodeJoiningType.Transparent
                    Case Else : Throw New InvalidOperationException(ResourcesT.Exceptions.UnexpedtedValue0.f(value))
                End Select
            End Get
        End Property

        ''' <summary>Gets joining group character belongs to (for Arabic and other Middle-Eastern characters)</summary>
        ''' <exception cref="InvalidOperationException">Underlying XML attribute value is not one of well-known values</exception>
        ''' <remarks>Underlying XML attribute is @jg.
        ''' <note>XML attribute value <c>Alef_Maqsurah</c> (which seems not to be used after Unicode 2.x) silently maps to <see cref="UnicodeJoiningGroup.YehWithTail"/>.</note></remarks>
        <DefaultValue(UnicodeJoiningGroup.none), XmlAttribute("jg")>
        Public ReadOnly Property JoiningGroup As UnicodeJoiningGroup
            Get
                Dim value = GetPropertyValue("jg")
                If value = "" Then Return UnicodeJoiningGroup.none
                Select Case value
                    Case "Ain" : Return UnicodeJoiningGroup.Ain
                    Case "Alaph" : Return UnicodeJoiningGroup.Alaph
                    Case "Alef" : Return UnicodeJoiningGroup.Alef
                    Case "Alef_Maqsurah" : Return UnicodeJoiningGroup.YehWithTail
                    Case "Beh" : Return UnicodeJoiningGroup.Beh
                    Case "Beth" : Return UnicodeJoiningGroup.Beth
                    Case "Burushaski_Yeh_Barree" : Return UnicodeJoiningGroup.BurushaskiYehBarree
                    Case "Dal" : Return UnicodeJoiningGroup.Dal
                    Case "Dalath_Rish" : Return UnicodeJoiningGroup.DotlessDalathRish
                    Case "E" : Return UnicodeJoiningGroup.E
                    Case "Farsi_Yeh" : Return UnicodeJoiningGroup.FarsiYeh
                    Case "Fe" : Return UnicodeJoiningGroup.SogdianFe
                    Case "Feh" : Return UnicodeJoiningGroup.Feh
                    Case "Final_Semkath" : Return UnicodeJoiningGroup.SemkathFinal
                    Case "Gaf" : Return UnicodeJoiningGroup.Gaf
                    Case "Gamal" : Return UnicodeJoiningGroup.Gamal
                    Case "Hah" : Return UnicodeJoiningGroup.Hah
                    Case "Hamza_On_Heh_Goal" : Return UnicodeJoiningGroup.HamzaOnHehGoal
                    Case "He" : Return UnicodeJoiningGroup.He
                    Case "Heh" : Return UnicodeJoiningGroup.Heh
                    Case "Heh_Goal" : Return UnicodeJoiningGroup.HehGoal
                    Case "Heth" : Return UnicodeJoiningGroup.Heth
                    Case "Kaf" : Return UnicodeJoiningGroup.Kaf
                    Case "Kaph" : Return UnicodeJoiningGroup.Kaph
                    Case "Khaph" : Return UnicodeJoiningGroup.SogdianKhaph
                    Case "Knotted_Heh" : Return UnicodeJoiningGroup.KnottedHeh
                    Case "Lam" : Return UnicodeJoiningGroup.Lam
                    Case "Lamadh" : Return UnicodeJoiningGroup.Lamadh
                    Case "Meem" : Return UnicodeJoiningGroup.Meem
                    Case "Mim" : Return UnicodeJoiningGroup.Mim
                    Case "No_Joining_Group" : Return UnicodeJoiningGroup.none
                    Case "Noon" : Return UnicodeJoiningGroup.Noon
                    Case "Nun" : Return UnicodeJoiningGroup.Nun
                    Case "Nya" : Return UnicodeJoiningGroup.Nya
                    Case "Pe" : Return UnicodeJoiningGroup.Pe
                    Case "Qaf" : Return UnicodeJoiningGroup.Qaf
                    Case "Qaph" : Return UnicodeJoiningGroup.Qaph
                    Case "Reh" : Return UnicodeJoiningGroup.Reh
                    Case "Reversed_Pe" : Return UnicodeJoiningGroup.ReversedPe
                    Case "Sad" : Return UnicodeJoiningGroup.Sad
                    Case "Sadhe" : Return UnicodeJoiningGroup.Sadhe
                    Case "Seen" : Return UnicodeJoiningGroup.Seen
                    Case "Semkath" : Return UnicodeJoiningGroup.Semkath
                    Case "Shin" : Return UnicodeJoiningGroup.Shin
                    Case "Swash_Kaf" : Return UnicodeJoiningGroup.SwashKaf
                    Case "Syriac_Waw" : Return UnicodeJoiningGroup.WawSyraic
                    Case "Tah" : Return UnicodeJoiningGroup.Tah
                    Case "Taw" : Return UnicodeJoiningGroup.Taw
                    Case "Teh_Marbuta" : Return UnicodeJoiningGroup.TehMarbuta
                    Case "Teh_Marbuta_Goal" : Return UnicodeJoiningGroup.TehMarbutaGoal
                    Case "Teth" : Return UnicodeJoiningGroup.Teth
                    Case "Waw" : Return UnicodeJoiningGroup.Waw
                    Case "Yeh" : Return UnicodeJoiningGroup.Yeh
                    Case "Yeh_Barree" : Return UnicodeJoiningGroup.YehBarree
                    Case "Yeh_With_Tail" : Return UnicodeJoiningGroup.YehWithTail
                    Case "Yudh" : Return UnicodeJoiningGroup.Yudh
                    Case "Yudh_He" : Return UnicodeJoiningGroup.YudhHe
                    Case "Zain" : Return UnicodeJoiningGroup.Zain
                    Case "Zhain" : Return UnicodeJoiningGroup.SogdianZhain
                    Case Else : Throw New InvalidOperationException(ResourcesT.Exceptions.UnexpedtedValue0.f(value))
                End Select
            End Get
        End Property

        ''' <summary>Gets value indicating if this is format control character which has specific functions for control of cursive joining and ligation</summary>
        ''' <remarks>Underlaying XML attribute is @Join_C.</remarks>
        <XmlAttribute("Join_C")>
        Public ReadOnly Property JoinControl As Boolean
            Get
                Return GetPropertyValue("Join_C") = "Y"
            End Get
        End Property
#End Region
#Region "Line-break"

        ''' <summary>Gets value indicating how the character behaves when libe breaking is concerned</summary>
        ''' <exception cref="InvalidOperationException">Value of underlyicng XML attribute is not one of expected values</exception>
        ''' <remarks>Underlying XML attribute is @lb</remarks>
        <XmlAttribute("lb"), DefaultValue(UnicodeLineBreakType.Unknown)>
        Public ReadOnly Property LineBreak As UnicodeLineBreakType
            Get
                Dim value = GetPropertyValue("lb")
                If value = "" Then Return UnicodeLineBreakType.Unknown
                Select Case value
                    Case "BK" : Return UnicodeLineBreakType.MandatoryBreak
                    Case "CR" : Return UnicodeLineBreakType.CarriageReturn
                    Case "LF" : Return UnicodeLineBreakType.LineFeed
                    Case "CM" : Return UnicodeLineBreakType.CombiningMark
                    Case "NL" : Return UnicodeLineBreakType.NextLine
                    Case "SG" : Return UnicodeLineBreakType.Surrogate
                    Case "WJ" : Return UnicodeLineBreakType.WordJoiner
                    Case "ZW" : Return UnicodeLineBreakType.ZeroWidthSpace
                    Case "GL" : Return UnicodeLineBreakType.NonBreakingGlue
                    Case "SP" : Return UnicodeLineBreakType.SPace
                    Case "B2" : Return UnicodeLineBreakType.BreakOpportunityBeforeAndAfter
                    Case "BA" : Return UnicodeLineBreakType.BreakAfter
                    Case "BB" : Return UnicodeLineBreakType.BreakBefore
                    Case "HY" : Return UnicodeLineBreakType.Hyphen
                    Case "CB" : Return UnicodeLineBreakType.ContingentBreakOpportunity
                    Case "CL" : Return UnicodeLineBreakType.ClosePunctuation
                    Case "CP" : Return UnicodeLineBreakType.CloseParenthesis
                    Case "EX" : Return UnicodeLineBreakType.ExclamationInterrogation
                    Case "IN" : Return UnicodeLineBreakType.Inseparable
                    Case "NS" : Return UnicodeLineBreakType.Nonstarter
                    Case "OP" : Return UnicodeLineBreakType.OpenPunctuation
                    Case "QU" : Return UnicodeLineBreakType.Quotation
                    Case "IS" : Return UnicodeLineBreakType.InfixNumericSeparator
                    Case "NU" : Return UnicodeLineBreakType.Numeric
                    Case "PO" : Return UnicodeLineBreakType.PostfixNumeric
                    Case "PR" : Return UnicodeLineBreakType.PrefixNumeric
                    Case "SY" : Return UnicodeLineBreakType.SymbolsAllowingBreakAfter
                    Case "AI" : Return UnicodeLineBreakType.Ambiguous
                    Case "AL" : Return UnicodeLineBreakType.Alphabetic
                    Case "H2" : Return UnicodeLineBreakType.HangulLvSyllable
                    Case "H3" : Return UnicodeLineBreakType.HangulLvtSyllable
                    Case "ID" : Return UnicodeLineBreakType.Ideographic
                    Case "JL" : Return UnicodeLineBreakType.HangulLJamo
                    Case "JV" : Return UnicodeLineBreakType.HangulVJamo
                    Case "JT" : Return UnicodeLineBreakType.HangulTJamo
                    Case "SA" : Return UnicodeLineBreakType.ComplexContextDependent
                    Case "XX" : Return UnicodeLineBreakType.Unknown
                    Case Else : Throw New InvalidOperationException(ResourcesT.Exceptions.UnexpedtedValue0.f(value))
                End Select
            End Get
        End Property
#End Region
#Region "East Asian width"
        ''' <summary>Gets width of East-Asian character</summary>
        ''' <exception cref="InvalidOperationException">Value of underlying XML attribute is neither of: A, F, H, N, Na, W</exception>
        ''' <remarks>Underlying XML attribute is @ea</remarks>
        <XmlAttribute("ea"), DefaultValue(UnicodeEastAsianWidth.Neutral)>
        Public ReadOnly Property EastAsianWidth As UnicodeEastAsianWidth
            Get
                Dim value = GetPropertyValue("ea")
                If value = "" Then Return UnicodeEastAsianWidth.Neutral
                Select Case value
                    Case "A" : Return UnicodeEastAsianWidth.Ambiguous
                    Case "F" : Return UnicodeEastAsianWidth.Full
                    Case "H" : Return UnicodeEastAsianWidth.Half
                    Case "N" : Return UnicodeEastAsianWidth.Neutral
                    Case "Na" : Return UnicodeEastAsianWidth.Narrow
                    Case "W" : Return UnicodeEastAsianWidth.Wide
                    Case Else : Throw New InvalidOperationException(ResourcesT.Exceptions.UnexpedtedValue0.f(value))
                End Select
            End Get
        End Property
#End Region
#Region "Case"
        ''' <summary>Gets value indicating if character is uppercase</summary>
        ''' <remarks>Underlying XML attribute is @Upper</remarks>
        <XmlAttribute("Upper")>
        Public ReadOnly Property IsUppercase As Boolean
            Get
                Return GetPropertyValue("Upper") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if character is lowercase</summary>
        ''' <remarks>Underlying XML attribute is @Lower</remarks>
        <XmlAttribute("Lower")>
        Public ReadOnly Property IsLowercase As Boolean
            Get
                Return GetPropertyValue("Lower") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if character is other uppercase</summary>
        ''' <remarks>Underlying XML attribute is @OUpper
        ''' <para>Used in deriving the <see cref="IsLowercase"/> property.</para></remarks>
        <XmlAttribute("OUpper"), EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property IsOtherUppercase As Boolean
            Get
                Return GetPropertyValue("OUpper") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if character is other lowercase</summary>
        ''' <remarks>Underlying XML attribute is @OLower
        ''' <para>Used in deriving the <see cref="IsUppercase"/> property.</para></remarks>
        <XmlAttribute("OLower"), EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property IsOtherLowercase As Boolean
            Get
                Return GetPropertyValue("OLower") = "Y"
            End Get
        End Property

        ''' <summary>Gets uppercase conterpart of this cahacter (if simple uppercase counterpar is defined)</summary>
        ''' <remarks>
        ''' Underlying XML attribute is @suc.
        ''' <para>Simple upppercase mapping is such mapping where the character maps to single uppercase character (as opposed to a sequence)</para>
        ''' <para>Many characters in Unicode maps to themselves.</para>
        ''' </remarks>
        <XmlAttribute("suc")>
        Public ReadOnly Property SimpleUppercaseMapping As CodePointInfo
            Get
                Return CodePointInfo.Parse(GetPropertyValue("suc", True), Element.Document)
            End Get
        End Property

        ''' <summary>Gets lowercase conterpart of this cahacter (if simple lowercase counterpar is defined)</summary>
        ''' <remarks>
        ''' Underlying XML attribute is @slc.
        ''' <para>Simple lowercase mapping is such mapping where the character maps to single lowercase character (as opposed to a sequence)</para>
        ''' <para>Many characters in Unicode maps to themselves.</para>
        ''' </remarks>
        <XmlAttribute("slc")>
        Public ReadOnly Property SimpleLowercaseMapping As CodePointInfo
            Get
                Return CodePointInfo.Parse(GetPropertyValue("slc", True), Element.Document)
            End Get
        End Property
        ''' <summary>Gets tilecase conterpart of this cahacter (if simple tilecase counterpar is defined)</summary>
        ''' <remarks>
        ''' Underlying XML attribute is @stc. If it doesnt provide value <see cref="SimpleUppercaseMapping"/> is used.
        ''' <para>Simple tilecase mapping is such mapping where the character maps to single tilecase character (as opposed to a sequence)</para>
        ''' <para>Many characters in Unicode maps to themselves.</para>
        ''' </remarks>
        <XmlAttribute("stc")>
        Public ReadOnly Property SimpleTilecaseMapping As CodePointInfo
            Get
                Dim value As String = GetPropertyValue("stc", True)
                If value = "" Then Return SimpleUppercaseMapping
                Return CodePointInfo.Parse(value, Element.Document)
            End Get
        End Property

        ''' <summary>Gets uppercase mapping for this charatcer (taht is one character or sequence of characters which form uppercase conterpart of current character)</summary>
        ''' <remarks>Underlying XML attribute is @uc, if it does not provide value <see cref="SimpleUppercaseMapping"/> is used instead</remarks>
        <XmlAttribute("uc")>
        Public ReadOnly Property UppercaseMappping As CodePointInfoCollection
            Get
                Dim value As String = GetPropertyValue("uc", True)
                If value = "" Then value = GetPropertyValue("suc", True)
                Return New CodePointInfoCollection(Element.Document, value)
            End Get
        End Property

        ''' <summary>Gets lowercase mapping for this charatcer (taht is one character or sequence of characters which form lowercase conterpart of current character)</summary>
        ''' <remarks>Underlying XML attribute is @lc, if it does not provide value <see cref="SimpleLowercaseMapping"/> is used instead</remarks>
        <XmlAttribute("lc")>
        Public ReadOnly Property LowercaseMappping As CodePointInfoCollection
            Get
                Dim value As String = GetPropertyValue("lc", True)
                If value = "" Then value = GetPropertyValue("slc", True)
                Return New CodePointInfoCollection(Element.Document, value)
            End Get
        End Property

        ''' <summary>Gets uppercase mapping for this charatcer (taht is one character or sequence of characters which form tilecase conterpart of current character)</summary>
        ''' <remarks>Underlying XML attribute is @tc, if it does not provide value <see cref="SimpleTilecaseMapping"/> is used instead. If it does not provide value <see cref="SimpleUppercaseMapping"/> is used instead.</remarks>
        <XmlAttribute("tc")>
        Public ReadOnly Property TilecaseMappping As CodePointInfoCollection
            Get
                Dim value As String = GetPropertyValue("tc", True)
                If value = "" Then value = GetPropertyValue("stc", True)
                If value = "" Then value = GetPropertyValue("suc", True)
                Return New CodePointInfoCollection(Element.Document, value)
            End Get
        End Property

        ''' <summary>Gets value idicating if the character is ignored for casing purposes</summary>
        ''' <remarks>Underlying XML attribute is @CI</remarks> 
        <XmlAttribute("CI")>
        Public ReadOnly Property CaseIgnorable As Boolean
            Get
                Return GetPropertyValue("CI") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if the character either llowercase, uppercase or tilecase character</summary>
        ''' <remarks>Underlying XML attribute is @Cased</remarks> 
        <XmlAttribute("CI")>
        Public ReadOnly Property IsCased As Boolean
            Get
                Return GetPropertyValue("Cased") = "Y"
            End Get
        End Property


        ''' <summary>Gets value indicating wheather character from is unstable under case folding</summary>
        ''' <returns>True if character's normalized forms are not stable under case folding.</returns>
        ''' <remarks>Underlying XML attribute is @CWCF</remarks> 
        <XmlAttribute("CWCF")>
        Public ReadOnly Property ChangesWhenCasefolded As Boolean
            Get
                Return GetPropertyValue("CWCF") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if the character may change when it undergoes case mapping</summary>
        ''' <remarks>Underlying XML attribute is @CWCM</remarks> 
        <XmlAttribute("CWCM")>
        Public ReadOnly Property ChangesWhenCasemapped As Boolean
            Get
                Return GetPropertyValue("CWCM") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating whether character's normalized forms are not stable under to-lowercase mapping</summary>
        ''' <returns>True if character's normalized froms are not stable under to-lowercase mapping</returns>
        ''' <remarks>Underlying XML attribute is @CWL</remarks> 
        <XmlAttribute("CWL")>
        Public ReadOnly Property ChangesWhenLowercased As Boolean
            Get
                Return GetPropertyValue("CWL") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating whether character's normalized forms are not stable under to-uppercase mapping</summary>
        ''' <returns>True if character's normalized froms are not stable under to-uppercase mapping</returns>
        ''' <remarks>Underlying XML attribute is @CWL</remarks> 
        <XmlAttribute("CWU")>
        Public ReadOnly Property ChangesWhenUppercased As Boolean
            Get
                Return GetPropertyValue("CWU") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating whether character's normalized forms are not stable under to-tilecase mapping</summary>
        ''' <returns>True if character's normalized froms are not stable under to-tilecase mapping</returns>
        ''' <remarks>Underlying XML attribute is @CWL</remarks> 
        <XmlAttribute("CWT")>
        Public ReadOnly Property ChangesWhenTilecased As Boolean
            Get
                Return GetPropertyValue("CWT") = "Y"
            End Get
        End Property



        ''' <summary>Gets value indicating if character is not identical to its Normalization Form KC casefold mapping</summary>
        ''' <returns>True if character is not identical to its Normalization Form KC casefold mapping.</returns>
        ''' <remarks>Underlying XML attribute is @CWKCF</remarks> 
        <XmlAttribute("CWKCF")>
        Public ReadOnly Property ChangesWhenNfKCCasefold As Boolean
            Get
                Return GetPropertyValue("CWKCF") = "Y"
            End Get
        End Property

        ''' <summary>Gets a mapping designed for best behavior when doing caseless matching of strings interpreted as identifiers</summary>
        ''' <remarks>Underlying XML attribute is @NFKC_CF</remarks>
        <XmlAttribute("NFKC_CF")>
        Public ReadOnly Property NfKCCasefold As CodePointInfoCollection
            Get
                Return New CodePointInfoCollection(Element.Document, GetPropertyValue("NFKC_CF", True))
            End Get
        End Property
#End Region
#Region "Script"
        ''' <summary>Gets an ISO 15924 code of script the character belongs to</summary>
        ''' <remarks>
        ''' Each character either belongs to specific script or it inherits it's script from preceding characters (Inherited  - value "Zinh" ("Qaai" before Unicode 5.2)), can be used with multiple scripts (Common - value "Zyyy") or its script assigment is not known (Unknown - value "Zzzz").
        ''' <para>Underlying XML attributes is @sc.</para>
        ''' </remarks>
        <XmlAttribute("sc")>
        Public ReadOnly Property Script$
            Get
                Return GetPropertyValue("sc")
            End Get
        End Property
#End Region
#Region "ISO Comment"
        ''' <summary>Gets ISO 10646 comment field.</summary>
        ''' <remarks>Underlying XML attribute is @isc.
        ''' <para>This property is deprecated: As of Unicode 5.2.0, this field no longer contains any non-null values.</para></remarks>
        <XmlAttribute("isc"), EditorBrowsable(EditorBrowsableState.Advanced), Obsolete("As of Unicode 5.2.0, this field no longer contains any non-null values.")>
        Public ReadOnly Property IsoComment$
            Get
                Return GetPropertyValue("isc")
            End Get
        End Property
#End Region
#Region "Hangul"
        ''' <summary>Gets Hangul syllable type as used in Chapter 3 of Unicode 6</summary>
        ''' <exception cref="InvalidOperationException">Underlying XML attribute value is neither of NA, L, V, LV, LVT, T</exception>
        ''' <remarks>Underlying XML attribute is @hst</remarks>
        <XmlAttribute("hst")>
        Public ReadOnly Property HangulSyllableType As UnicodeHangulSyllableType
            Get
                Dim value = GetPropertyValue("hst")
                If value = "" Then Return UnicodeHangulSyllableType.notApplicable
                Select Case value
                    Case "NA" : Return UnicodeHangulSyllableType.notApplicable
                    Case "L" : Return UnicodeHangulSyllableType.LeadingJamo
                    Case "V" : Return UnicodeHangulSyllableType.VowelJamo
                    Case "LV" : Return UnicodeHangulSyllableType.Lv
                    Case "LVT" : Return UnicodeHangulSyllableType.Lvt
                    Case "T" : Return UnicodeHangulSyllableType.TrailingJamo
                    Case Else : Throw New InvalidOperationException(ResourcesT.Exceptions.UnexpedtedValue0.f(value))
                End Select
            End Get
        End Property

        ''' <summary>For Hangul syllables gets Jamo Short Name</summary>
        ''' <remarks>Underlying XML attribute is @JSN</remarks>
        <XmlAttribute("JSN")>
        Public ReadOnly Property JamoShortName$
            Get
                Return GetPropertyValue("JSN")
            End Get
        End Property
#End Region
#Region "Indic"
        ''' <summary>A provisional property defining the placement categories for dependent vowels in Indic scripts.</summary>
        ''' <remarks>Underlying XML attribute is @InSC</remarks>
        <XmlAttribute("InSC")>
        Public ReadOnly Property IndicSyllabicCategory$
            Get
                Return GetPropertyValue("InSC")
            End Get
        End Property
        ''' <summary>A provisional property defining the structural categories of syllabic components in Indic scripts.</summary>
        ''' <remarks>Underlying XML attribute is @InMC</remarks>
        <XmlAttribute("InMC")>
        Public ReadOnly Property IndicMatraCategory$
            Get
                Return GetPropertyValue("InMC")
            End Get
        End Property
#End Region
#Region "Identifier and Pattern and programming language properties"
        ''' <summary>Gets value indicating if the character can be 1st character in name of an identifier in a programming language such as VB or C#</summary>
        ''' <remarks>Underlying XML attribute is @IDS</remarks>
        <XmlAttribute("IDS")>
        Public ReadOnly Property IsIdStart As Boolean
            Get
                Return GetPropertyValue("IDS") = "Y"
            End Get
        End Property
        ''' <summary>Used for backward compatibility of <see cref="IsIdStart"/></summary>
        ''' <remarks>Underlying XML attribute is @OIDS</remarks>
        <XmlAttribute("OIDS"), EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property OtherIdStart As Boolean
            Get
                Return GetPropertyValue("OIDS") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if the character can be 1st character in name of an identifier in a programming language such as VB or C# (improved version)</summary>
        ''' <remarks>Underlying XML attribute is @XIDS</remarks>
        <XmlAttribute("XIDS")>
        Public ReadOnly Property IsIdStartEx As Boolean
            Get
                Return GetPropertyValue("XIDS") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if the character can be non-1st character in name of an identifier in a programming language such as VB or C#</summary>
        ''' <remarks>Underlying XML attribute is @IDC</remarks>
        <XmlAttribute("IDC")>
        Public ReadOnly Property IsIdContinue As Boolean
            Get
                Return GetPropertyValue("IDC") = "Y"
            End Get
        End Property
        ''' <summary>Used for backward compatibility of <see cref="IsIdContinue"/></summary>
        ''' <remarks>Underlying XML attribute is @OIDC</remarks>
        <XmlAttribute("OIDC"), EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property OtherIdContinue As Boolean
            Get
                Return GetPropertyValue("OIDC") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if the character can be non-1st character in name of an identifier in a programming language such as VB or C# (improved version)</summary>
        ''' <remarks>Underlying XML attribute is @XIDC</remarks>
        <XmlAttribute("XIDC")>
        Public ReadOnly Property IsIdContinueEx As Boolean
            Get
                Return GetPropertyValue("XIDC") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if charatcer can be used in syntax of programming language</summary>
        ''' <remarks>Underlying XML attribute is @Pat_Syn</remarks>
        <XmlAttribute("Pat_Syn")>
        Public ReadOnly Property IsPatternSyntax As Boolean
            Get
                Return GetPropertyValue("Pat_Syn") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if charatcer shopuld be treated as whitespace by programming language compiler or interpreter</summary>
        ''' <remarks>Underlying XML attribute is @Pat_WS</remarks>
        <XmlAttribute("Pat_WS")>
        Public ReadOnly Property IsPatternWhiteSpace As Boolean
            Get
                Return GetPropertyValue("Pat_WS") = "Y"
            End Get
        End Property

#End Region
#Region "Properties related to function and graphic characteristics"
        ''' <summary>Gets value indicating if this character represents a dash</summary>
        ''' <remarks>Underlying XML attribute is @Dash</remarks>
        <XmlAttribute("Dash")>
        Public ReadOnly Property IsDash As Boolean
            Get
                Return GetPropertyValue("Dash") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this character represents a dash used to mark connections between pieces of words (includes Katakana middle dot)</summary>
        ''' <remarks>Underlying XML attribute is @Hyphen
        ''' <para>This property is deprecated as of Unicode 6.0</para></remarks>
        <XmlAttribute("Hyphen"), Obsolete("This property is deprecated as of Unicode 6.0")>
        Public ReadOnly Property IsHyphen As Boolean
            Get
                Return GetPropertyValue("Hyphen") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this character is a punctuation that functions as quotation mark</summary>
        ''' <remarks>Underlying XML attribute is @QMark</remarks>
        <XmlAttribute("QMark")>
        Public ReadOnly Property IsQuotationMark As Boolean
            Get
                Return GetPropertyValue("QMark") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if this character is a punctuation that generally marks the end of textual units.</summary>
        ''' <remarks>Underlying XML attribute is @Term</remarks>
        <XmlAttribute("Term")>
        Public ReadOnly Property IsTerminalPunctuation As Boolean
            Get
                Return GetPropertyValue("Term") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if this character is sentence terminal</summary>
        ''' <remarks>Underlying XML attribute is @STerm</remarks>
        <XmlAttribute("STerm")>
        Public ReadOnly Property IsSentenceTerminal As Boolean
            Get
                Return GetPropertyValue("STerm") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if this character is diacritics (it linguistically modifies the meaning of another character to which it applies).</summary>
        ''' <remarks>Underlying XML attribute is @Dia
        ''' <para>Note: Some diacritics are not combining characters and some combining characters are not diacritics.</para></remarks>
        <XmlAttribute("Dia")>
        Public ReadOnly Property IsDiacritic As Boolean
            Get
                Return GetPropertyValue("Dia") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if this character is an extender (its principal function is to extend value or shape of a preceding alphabetic character)</summary>
        ''' <remarks>Underlying XML attribute is @Ext</remarks>
        <XmlAttribute("Ext")>
        Public ReadOnly Property IsExtender As Boolean
            Get
                Return GetPropertyValue("Ext") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this has a soft dot (usch as j or i)</summary>
        ''' <remarks>Underlying XML attribute is @SD
        ''' <para>An accent placed on this character causes the dot to disappear. Explicit dot above can be added if required.</para></remarks>
        <XmlAttribute("SD")>
        Public ReadOnly Property IsSoftDotted As Boolean
            Get
                Return GetPropertyValue("SD") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this character is alphabetic</summary>
        ''' <remarks>Underlying XML attribute is @Alpha</remarks>
        <XmlAttribute("Alpha")>
        Public ReadOnly Property IsAlphabetic As Boolean
            Get
                Return GetPropertyValue("Alpha") = "Y"
            End Get
        End Property
        ''' <summary>Used in deriving <see cref="IsAlphabetic"/> property.</summary>
        ''' <remarks>Underlying XML attribute is @OAlpha</remarks>
        <XmlAttribute("OAlpha"), EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property IsOtherAlphabetic As Boolean
            Get
                Return GetPropertyValue("OAlpha") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this is math character</summary>
        ''' <remarks>Underlying XML attribute is @Math</remarks>
        <XmlAttribute("Math")>
        Public ReadOnly Property IsMath As Boolean
            Get
                Return GetPropertyValue("Math") = "Y"
            End Get
        End Property
        ''' <summary>Used in deriving <see cref="IsMath"/> property</summary>
        ''' <remarks>Underlying XML attribute is @OMath</remarks>
        <XmlAttribute("OMath")>
        Public ReadOnly Property IsOtherMath As Boolean
            Get
                Return GetPropertyValue("OMath") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if this character is commonly used to represent hexadigit</summary>
        ''' <remarks>Underlying XML attribute is @Hex</remarks>
        <XmlAttribute("Hex")>
        Public ReadOnly Property IsHexaDigit As Boolean
            Get
                Return GetPropertyValue("Hex") = "Y"
            End Get
        End Property
   
        ''' <summary>Gets value indicating if this character is an ASCII character used commonly to represent hexadigit</summary>
        ''' <remarks>Underlying XML attribute is @AHex</remarks>
        <XmlAttribute("AHex")>
        Public ReadOnly Property IsAsciiHey As Boolean
            Get
                Return GetPropertyValue("AHex") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this is default ignorable code point - this is it should be ignored in renderign unless explicitly supported.</summary>
        ''' <remarks>Underlying XML attribute is @DI</remarks>
        <XmlAttribute("DI")>
        Public ReadOnly Property IsDefaultIgnorable As Boolean
            Get
                Return GetPropertyValue("DI") = "Y"
            End Get
        End Property
        ''' <summary>Used in deriving <see cref="IsDefaultIgnorable"/> property</summary>
        ''' <remarks>Underlying XML attribute is @ODI</remarks>
        <XmlAttribute("ODI"), EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property IsOtherDefaultIgnorable As Boolean
            Get
                Return GetPropertyValue("ODI") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if this character represents a lexical order exception</summary>
        ''' <remarks>Underlying XML attribute is @LOE<para>A small number of spacing vowel letters occurring in certain Southeast Asian scripts such as Thai and Lao, which use a visual order display model. These letters are stored in text ahead of syllable-initial consonants, and require special handling for processes such as searching and sorting.</para></remarks>
        <XmlAttribute("LOE")>
        Public ReadOnly Property IsLexicalOrderException As Boolean
            Get
                Return GetPropertyValue("LOE") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if this character is whitespace.</summary>
        ''' <remarks>Underlying XML attribute is @WSpace</remarks>
        <XmlAttribute("WSpace")>
        Public ReadOnly Property IsWhitespace As Boolean
            Get
                Return GetPropertyValue("WSpace") = "Y"
            End Get
        End Property
#End Region
#Region "Properties related to boundaries"
        ''' <summary>Gets value indicating if this character is grapheme base.</summary>
        ''' <remarks>Underlying XML attribute is @Gr_Base</remarks>
        <XmlAttribute("Gr_Base")>
        Public ReadOnly Property IsGraphemeBase As Boolean
            Get
                Return GetPropertyValue("Gr_Base") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if this character is grapheme extend.</summary>
        ''' <remarks>Underlying XML attribute is @Gr_Ext</remarks>
        <XmlAttribute("Gr_Ext")>
        Public ReadOnly Property IsGraphemeExtend As Boolean
            Get
                Return GetPropertyValue("Gr_Ext") = "Y"
            End Get
        End Property
        ''' <summary>Used in deriving the <see cref="IsGraphemeExtend"/> property.</summary>
        ''' <remarks>Underlying XML attribute is @OGr_Ext</remarks>
        <XmlAttribute("OGr_Ext"), EditorBrowsable(EditorBrowsableState.Advanced)>
        Public ReadOnly Property IsOtherGraphemeExtend As Boolean
            Get
                Return GetPropertyValue("OGr_Ext") = "Y"
            End Get
        End Property
        ''' <summary>Formerly proposed for programatic determination of grapheme cluster boundaries.</summary>
        ''' <remarks>Underlying XML attribute is @Gr_Link<para>This property is deprecated as of Unicode 5.0</para></remarks>
        <XmlAttribute("Gr_Link"), Obsolete("Deprecated as of Unicode 5.0")>
        Public ReadOnly Property IsGraphemeLink As Boolean
            Get
                Return GetPropertyValue("Gr_Link") = "Y"
            End Get
        End Property

        ''' <summary>Gets type of grapheme cluster break</summary>
        ''' <exception cref="InvalidOperationException">Value of underlying XML attribute is not one of expected values</exception>
        ''' <remarks>Underlying XML attribute is @GCB</remarks>
        <XmlAttribute("GCB"), DefaultValue(UnicodeGraphemeClusterBreak.other)>
        Public ReadOnly Property GraphemeClusterBreak As UnicodeGraphemeClusterBreak
            Get
                Dim value = GetPropertyValue("GCB")
                If value = "" Then Return UnicodeGraphemeClusterBreak.other
                Select Case value
                    Case "CN" : Return UnicodeGraphemeClusterBreak.Control
                    Case "CR" : Return UnicodeGraphemeClusterBreak.Cr
                    Case "EX" : Return UnicodeGraphemeClusterBreak.Extend
                    Case "L" : Return UnicodeGraphemeClusterBreak.HangulL
                    Case "LF" : Return UnicodeGraphemeClusterBreak.Lf
                    Case "LV" : Return UnicodeGraphemeClusterBreak.HangulLv
                    Case "LVT" : Return UnicodeGraphemeClusterBreak.HangulLvt
                    Case "PP" : Return UnicodeGraphemeClusterBreak.Prepend
                    Case "SM" : Return UnicodeGraphemeClusterBreak.SpacingMark
                    Case "T" : Return UnicodeGraphemeClusterBreak.HangulT
                    Case "V" : Return UnicodeGraphemeClusterBreak.HangulV
                    Case "XX" : Return UnicodeGraphemeClusterBreak.other
                    Case Else : Throw New InvalidOperationException(ResourcesT.Exceptions.UnexpedtedValue0.f(value))
                End Select
            End Get
        End Property

        ''' <summary>Gets type of word break</summary>
        ''' <exception cref="InvalidOperationException">Value of underlying XML attribute is not one of expected values</exception>
        ''' <remarks>Underlying XML attribute is @WB</remarks>
        <XmlAttribute("WB"), DefaultValue(UnicodeWordBreakType.other)>
        Public ReadOnly Property WordBreak As UnicodeWordBreakType
            Get
                Dim value = GetPropertyValue("WB")
                If value = "" Then Return UnicodeWordBreakType.other
                Select Case value
                    Case "XX" : Return UnicodeWordBreakType.other
                    Case "CR" : Return UnicodeWordBreakType.Cr
                    Case "LF" : Return UnicodeWordBreakType.Lf
                    Case "NL" : Return UnicodeWordBreakType.NewLine
                    Case "Extend" : Return UnicodeWordBreakType.Extend
                    Case "FO" : Return UnicodeWordBreakType.Format
                    Case "KA" : Return UnicodeWordBreakType.Katakana
                    Case "LE" : Return UnicodeWordBreakType.ALetter
                    Case "MB" : Return UnicodeWordBreakType.MidNumLet
                    Case "ML" : Return UnicodeWordBreakType.MidLetter
                    Case "MN" : Return UnicodeWordBreakType.MidNum
                    Case "NM" : Return UnicodeWordBreakType.Numeric
                    Case "EX" : Return UnicodeWordBreakType.ExtendNumSet
                    Case Else : Throw New InvalidOperationException(ResourcesT.Exceptions.UnexpedtedValue0.f(value))
                End Select
            End Get
        End Property

        ''' <summary>Gets type of sentence break</summary>
        ''' <exception cref="InvalidOperationException">Value of underlying XML attribute is not one of expected values</exception>
        ''' <remarks>Underlying XML attribute is @SB</remarks>
        <XmlAttribute("SB")>
        Public ReadOnly Property SentenceBreak As UnicodeSentenceBreakType
            Get
                Dim value = GetPropertyValue("SB")
                If value = "" Then Return UnicodeSentenceBreakType.other
                Select Case value
                    Case "XX" : Return UnicodeSentenceBreakType.other
                    Case "CR" : Return UnicodeSentenceBreakType.Cr
                    Case "LF" : Return UnicodeSentenceBreakType.Lf
                    Case "EX" : Return UnicodeSentenceBreakType.Extend
                    Case "SE" : Return UnicodeSentenceBreakType.Separator
                    Case "FO" : Return UnicodeSentenceBreakType.Format
                    Case "SP" : Return UnicodeSentenceBreakType.Space
                    Case "LO" : Return UnicodeSentenceBreakType.Lower
                    Case "UP" : Return UnicodeSentenceBreakType.Upper
                    Case "LE" : Return UnicodeSentenceBreakType.OLetter
                    Case "NU" : Return UnicodeSentenceBreakType.Numeric
                    Case "AT" : Return UnicodeSentenceBreakType.ATerm
                    Case "SC" : Return UnicodeSentenceBreakType.[Continue]
                    Case "ST" : Return UnicodeSentenceBreakType.STerm
                    Case "CL" : Return UnicodeSentenceBreakType.Close
                    Case Else : Throw New InvalidOperationException(ResourcesT.Exceptions.UnexpedtedValue0.f(value))
                End Select
            End Get
        End Property
        'TODO: 4.4.18
#End Region
#Region "Properties related to ideographs"
        ''' <summary>Gets value indicating if the character is CKJV ideograph</summary>
        ''' <remarks>Underlying XML attribute is @Ideo</remarks>
        <XmlAttribute("Ideo")>
        Public ReadOnly Property IsIdeograph As Boolean
            Get
                Return GetPropertyValue("Ideo") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if the character is unified CJK ideograph</summary>
        ''' <remarks>Underlying XML attribute is @UIdeo</remarks>
        <XmlAttribute("UIdeo")>
        Public ReadOnly Property IsUnifiedIdeograph As Boolean
            Get
                Return GetPropertyValue("UIdeo") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if character is ideographic descriptionsequence binary operator</summary>
        ''' <remarks>Underlying XML attribute is @ISDB</remarks>
        <XmlAttribute("ISDB")>
        Public ReadOnly Property IsIdsBinaryOperator As Boolean
            Get
                Return GetPropertyValue("ISDB") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if character is ideographic description sequence trinary operator</summary>
        ''' <remarks>Underlying XML attribute is @ISDT</remarks>
        <XmlAttribute("ISDT")>
        Public ReadOnly Property IsIdsTrinaryOperator As Boolean
            Get
                Return GetPropertyValue("ISDT") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if character is radical (for ideographic description sequences)</summary>
        ''' <remarks>Underlying XML attribute is @Radical</remarks>
        <XmlAttribute("Radical")>
        Public ReadOnly Property IsRadical As Boolean
            Get
                Return GetPropertyValue("Radical") = "Y"
            End Get
        End Property
#End Region
#Region "Misc"
        ''' <summary>Gets value indicating if the character is deprecated</summary>
        ''' <remarks>Underlying XML attribute is @Dep</remarks>
        <XmlAttribute("Dep")>
        Public ReadOnly Property IsDeprecated As Boolean
            Get
                Return GetPropertyValue("Dep") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if the character is Variation Selector</summary>
        ''' <remarks>Underlying XML attribute is @VS</remarks>
        <XmlAttribute("VS")>
        Public ReadOnly Property IsVariationSelector As Boolean
            Get
                Return GetPropertyValue("VS") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if a code point is permanent non-character (i.e. the code point is permanently reserved ofr internal use)</summary>
        ''' <remarks>Underlying XML attribute is @NChar</remarks>
        <XmlAttribute("NChar")>
        Public ReadOnly Property IsNonCharacter As Boolean
            Get
                Return GetPropertyValue("NChar") = "Y"
            End Get
        End Property
#End Region
#Region "Unihan"
        'TODO: 4.4.21
#End Region
#End Region

        ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
        ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
        ''' <filterpriority>2</filterpriority>
        Public Overrides Function ToString() As String
            If Name <> "" Then Return Name
            Return MyBase.ToString()
        End Function
    End Class
End Namespace
