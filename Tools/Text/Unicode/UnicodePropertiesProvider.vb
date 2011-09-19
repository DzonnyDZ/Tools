Imports Tools.ComponentModelT
Imports System.Linq
Imports Tools.ExtensionsT
Imports System.Xml.Linq
Imports System.Globalization.CultureInfo
Imports Tools.NumericsT
Imports System.Xml.Serialization

'List of all properties http://www.unicode.org/reports/tr44/#Properties
'XML format specification http://www.unicode.org/reports/tr42/
'Property value aliases http://www.unicode.org/Public/6.0.0/ucd/PropertyValueAliases.txt

'TODO: Localize DisplayName

Namespace TextT.UnicodeT
    ''' <summary>Common base class for Unicode code points and groups. This class holds character properties</summary>
    ''' <remarks>
    ''' This class defines Unicode Character Database (UCD) properties.
    ''' Properties that belong to UCD are decorated with <see cref="UnicodePropertyAttribute"/>.
    ''' Properties that do not belong to UCD are decorated with <see cref="XmlIncludeAttribute"/>.
    ''' Other attributes are used to describe UCD properties and how they are used in UCD.
    ''' <list type="table"><listheader><term>Attribute</term><description>Used for</description></listheader>
    ''' <item><term><see cref="UnicodePropertyAttribute"/></term><description>Indicates that this property is UCD property and provides basic information about it.</description></item>
    ''' <item><term><see cref="UnicodePropertyCategory"/></term><description>
    '''     Indicates to which category (according to Table 7, chapter 5.1 of Unicode Standard Annex #44) the property belong.
    '''     This attribute derives from <see cref="CategoryAttribute"/>.
    '''     Unihan properties are decorated with <see cref="UnihanPropertyCategoryAttribute"/> attribute instead (which inherits from <see cref="UnicodePropertyAttribute"/>).
    ''' </description></item>
    ''' <item><term><see cref="DisplayNameAttribute"/></term><description>Provides (localized) human-friendly property name</description></item>
    ''' <item><term><see cref="XmlAttributeAttribute"/></term><description>Indicates name of XML attribute the property is stored in in Unicode Character Database XML.</description></item>
    ''' <item><term><see cref="ObsoleteAttribute"/></term><description>Indicates that the property is deprecated in latest Unicode Standard (supported by this class)</description></item>
    ''' <item><term><see cref="XmlIgnoreAttribute"/></term><description>
    '''     Indicates that this property is not UCD property.
    '''     It's either property that supports <see cref="TextT.UnicodeT"/> infrastructure or it's property that provides information derived from one or more UCD properties.
    '''     Infrastructure properties are also decorated with <see cref="BrowsableAttribute"/> with <see cref="BrowsableAttribute.Browsable"/> set to false.
    ''' </description></item>
    ''' <item><term><see cref="BrowsableAttribute"/></term><description>
    '''     Only used with <see cref="BrowsableAttribute.Browsable"/> set to false.
    '''     If the property is also decorated with <see cref="XmlIgnoreAttribute"/> it's an infrastructure property used to support <see cref="TextT.UnicodeT"/> infrastructure.
    '''     Otherwise it's hidden UCD property. This is property used for deriving of other UCD properties
    '''     (i.e. Contributory property - <see cref="UnicodePropertyCategoryAttribute.Category"/> is <see cref="UnicodePropertyCategory.Contributory"/> and <see cref="UnicodePropertyAttribute.Status"/> is <see cref="UnicodePropertyStatus.Contributory"/>).
    '''     Name of such property usually starts with <c>Other_</c>.
    ''' </description></item>
    ''' <item><term><see cref="DefaultValueAttribute"/></term><description>
    '''     Some properties indicates default value used by UCD when property value is not explicitly specified.
    ''' </description></item>
    ''' <item><term><see cref="EditorBrowsableAttribute"/></term><description>
    '''     Contributory properties
    '''     (i.e. properties used when deriving values of other UCD derived properties, <see cref="UnicodePropertyCategoryAttribute.Category"/> is <see cref="UnicodePropertyCategory.Contributory"/> and <see cref="UnicodePropertyAttribute.Status"/> is <see cref="UnicodePropertyStatus.Contributory"/>)
    '''     are usually decorated with <see cref="EditorBrowsableAttribute"/> with <see cref="EditorBrowsableAttribute.State"/> set to <see cref="EditorBrowsableState.Advanced"/>.
    '''     These properties are typically also decorated with <see cref="BrowsableAttribute"/> with <see cref="BrowsableAttribute.Browsable"/> set to false.
    '''     Also some obsolete/deprecated properties are decorated with <see cref="EditorBrowsableAttribute"/> with <see cref="EditorBrowsableAttribute.State"/> set to <see cref="EditorBrowsableState.Advanced"/>.
    ''' </description></item>
    ''' </list>
    ''' <note>May boolean properties' names are prefixed with the "Is" prefix which is not used in UCD.</note>
    ''' <note>Unihan database properties has prefix "Han" (Uninah property can be also determined by use of <see cref="UnihanPropertyCategoryAttribute"/> instead of <see cref="UnicodePropertyCategoryAttribute"/>).</note>
    ''' </remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public MustInherit Class UnicodePropertiesProvider : Implements IXElementWrapper
#Region "Infrastructure"
        Private _element As XElement
        ''' <summary>CTor - creates a new instance of the <see cref="UnicodePropertiesProvider"/> class</summary>
        ''' <param name="element">A XML element which stores the properties</param>
        ''' <exception cref="ArgumentNullException"><paramref name="element"/> is null.</exception>
        Protected Sub New(element As XElement)
            If element Is Nothing Then Throw New ArgumentNullException("element")
            _element = element
        End Sub

        ''' <summary>Gets XML element this instance wraps</summary>
        <EditorBrowsable(EditorBrowsableState.Advanced), XmlIgnore(), Browsable(False)>
        Public ReadOnly Property Element As XElement Implements IXElementWrapper.Element
            Get
                Return _element
            End Get
        End Property

        ''' <summary>Gets node this instance wraps</summary>
        <XmlIgnore(), Browsable(False)>
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
#End Region

#Region "Properties"
#Region "General"
        ''' <summary>Gets version of Unicode in which a code point was assigned to an abstract character, or made surrogate or non-character</summary>
        ''' <returns>Version of Unicode standard or null. Null is retuened also when underlying XML attribute has value "unassigned".</returns>
        ''' <remarks>Unicode standard defines values this property can have (i.e. it cannot have any version number and typically only <see cref="Version.Major"/> and <see cref="Version.Minor"/> numbers are used.
        ''' <para>Underlying XML attribute is @age.</para></remarks>
        <XmlAttribute("age")>
        <UcdProperty("Age", "DerivedAge.txt", UnicodePropertyType.Catalog, UnicodePropertyStatus.Normative), UcdCategory(UnicodePropertyCategory.General)>
        <DisplayName("Age")>
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
        ''' <para>These names match exactly the names published in the code charts of the Unicode Standard.</para>
        ''' </remarks>
        <XmlAttribute("na")>
        <UcdProperty("Name", "UnicodeData.txt", UnicodePropertyType.Miscellaneous, UnicodePropertyStatus.Normative), UcdCategory(UnicodePropertyCategory.General)>
        <DisplayName("Name")>
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
        <UcdProperty("Unicode_1_Name", "UnicodeData.txt", UnicodePropertyType.Miscellaneous, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Miscellaneous), DisplayName("Unicode 1 Name")>
        Public Overridable ReadOnly Property Name1 As String
            Get
                Return GetPropertyValue("na1")
            End Get
        End Property

        ''' <summary>Gets general category of code point</summary>
        ''' <value>Default value when not assigned in Unicode Character Database is <see cref="Globalization.UnicodeCategory.OtherNotAssigned"/></value>
        ''' <exception cref="InvalidOperationException">Value of underlying attribute cannot be mapped to <see cref="Globalization.UnicodeCategory"/> enumeration value.</exception>
        ''' <remarks>Underlying XML attribute is @gc.
        ''' <para>This is a useful breakdown into various character types which can be used as a default categorization in implementations.</para></remarks>
        ''' <seelaso cref="System.Char.GetUnicodeCategory"/>
        <XmlAttribute("gc")>
        <DefaultValue(Globalization.UnicodeCategory.OtherNotAssigned)>
        <UcdProperty("General_Category", "UnicodeData.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Normative), UcdCategory(UnicodePropertyCategory.General)>
        <DisplayName("General Category")>
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
        ''' <remarks>Underlying XML attribute is @ccc.
        ''' <para>The classes used for the Canonical Ordering Algorithm in the Unicode Standard. This property could be considered either an enumerated property or a numeric property: the principal use of the property is in terms of the numeric values.</para></remarks>
        <XmlAttribute("ccc")>
        <UcdProperty("Canonical_Combining_Class", "UnicodeData.txt", UnicodePropertyType.Numeric, UnicodePropertyStatus.Normative), UcdCategory(UnicodePropertyCategory.Normalization)>
        <DisplayName("Canonical Combining Class")>
        Public ReadOnly Property CanonicalCombiningClass As UnicodeCombiningClass
            Get
                Dim value = GetPropertyValue("ccc")
                If value = "" Then Return 0
                Return Byte.Parse(value, InvariantCulture)
            End Get
        End Property
#End Region

#Region "Bidi"
        ''' <summary>Gets bidirectional category of the character</summary>
        ''' <returns>Unicode bidirectional category specified for current character. Null if bidi class is not specified in Unicode Character Database - in this case Unicode Bidirectional Alghoritm should be used to determine default value of bidi class of character.</returns>
        ''' <remarks>Underlying XML attributes is @bc</remarks>
        ''' <exception cref="InvalidOperationException">Underlying XML attribute value cannot be mapped to <see cref="UnicodeBidiCategory"/> value</exception>
        <XmlAttribute("bc")>
        <UcdProperty("Bidi_Class", "UnicodeData.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Bidirectional)>
        <DisplayName("Bidi Class")>
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
        <UcdProperty("Bidi_Mirrored", "UnicodeData.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Bidirectional)>
        <DisplayName("Bidi Mirrored")>
        Public ReadOnly Property IsMirrored As Boolean
            Get
                Return GetPropertyValue("Bidi_M") = "Y"
            End Get
        End Property

        ''' <summary>Gets a code point of glyph that is typically mirrored version of this glyph when used in right-to-left text</summary>
        ''' <remarks>Underlying XML attribute is @bmg.</remarks>
        <XmlAttribute("bmg")>
        <UcdProperty("Bidi_Mirroring_Glyph", "BidiMirroring.txt", UnicodePropertyType.String, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Bidirectional)>
        <DisplayName("Bidi Mirroring Glyph")>
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
        <UcdProperty("Bidi_Control", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Bidirectional)>
        <DisplayName("Bidi Control")>
        Public ReadOnly Property IsBidiControl As Boolean?
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
        <UcdProperty("Decomposition_Type", "UnicodeData.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Normalization)>
        <DisplayName("Decomposition type")>
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
        <UcdProperty("Decomposition_Mapping", "UnicodeData.txt", UnicodePropertyType.String, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Normalization), DisplayName("Decomposition Mapping")>
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
        <UcdProperty("Composition_Exclusion", "CompositionExclusions.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Normalization), DisplayName("Composition Exclusion")>
        Public ReadOnly Property CompositionExclusion As Boolean
            Get
                Return GetPropertyValue("CE") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if character is explicitly or otherwise excluded from composition. This information comes from DerivedNormalizationProps.txt</summary>
        ''' <remarks>Underlying XML attributes is @Comp_Ex. <para>If you are looking only for explicit composition exclusions see <see cref="CompositionExclusion"/>.</para></remarks>
        ''' <seelaso cref="CompositionExclusion"/>
        <XmlAttribute("Comp_Ex")>
        <UcdProperty("Full_Composition_Exclusion", "DerivedNormalizationProps.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Normalization), DisplayName("Full Composition Exclusion")>
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
        <UcdProperty("NFC_Quick_Check", "DerivedNormalizationProps.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Normalization), DisplayName("Normalization Form C Quick Check")>
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
        <UcdProperty("NFD_Quick_Check", "DerivedNormalizationProps.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Normalization), DisplayName("Normalization Form D Quick Check")>
        Public ReadOnly Property NormalizationFormDQuickCheck As Boolean
            Get
                Return GetPropertyValue("NFD_QC") <> "N"
            End Get
        End Property
        ''' <summary>Gets value indicating if character always (true) or never(false) occurs in normalization from KC</summary>
        ''' <exception cref="InvalidOperationException">Value of underlying XML attribute is newither N nor M nor Y.</exception>
        ''' <remarks>Underlying XML attribute is @NFKC_QC.</remarks>
        <XmlAttribute("NFKC_QC"), DefaultValue(True)>
        <UcdProperty("NFKC_Quick_Check", "DerivedNormalizationProps.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Normalization), DisplayName("Normalization Form KC Quick Check")>
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
        <UcdProperty("NFKD_Quick_Check", "DerivedNormalizationProps.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Normalization), DisplayName("Normalization Form KD Quick Check")>
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
        <UcdProperty("Expands_On_NFC", "DerivedNormalizationProps.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Normalization), DisplayName("Expands on Normalization Form C")>
        Public ReadOnly Property ExpandOnC As Boolean
            Get
                Return GetPropertyValue("XO_NFC") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if character expands to more than one character on normalization form D</summary>
        ''' <remarks><para>This property is deprecated as of Unicode 6.0</para>Underlying XML attribute is @XO_NFD.</remarks>
        <XmlAttribute("XO_NFD"), Obsolete("Property Expands_On_NFD is deprecated as of Unicode 6.0.0")>
        <UcdProperty("Expands_On_NFD", "DerivedNormalizationProps.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Normalization), DisplayName("Expands on Normalization Form D")>
        Public ReadOnly Property ExpandOnD As Boolean
            Get
                Return GetPropertyValue("XO_NFD") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if character expands to more than one character on normalization form KC</summary>
        ''' <remarks><para>This property is deprecated as of Unicode 6.0</para>Underlying XML attribute is @XO_NFKC.</remarks>
        <XmlAttribute("XO_NFKC"), Obsolete("Property Expands_On_NFKC is deprecated as of Unicode 6.0.0")>
        <UcdProperty("Expands_On_NFKC", "DerivedNormalizationProps.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Normalization), DisplayName("Expands on Normalization Form KC")>
        Public ReadOnly Property ExpandOnKC As Boolean
            Get
                Return GetPropertyValue("XO_NFKC") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if character expands to more than one character on normalization form KD</summary>
        ''' <remarks><para>This property is deprecated as of Unicode 6.0</para>Underlying XML attribute is @XO_NFKD.</remarks>
        <XmlAttribute("XO_NFKD"), Obsolete("Property Expands_On_NFKD is deprecated as of Unicode 6.0.0")>
        <UcdProperty("Expands_On_NFKD", "DerivedNormalizationProps.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Normalization), DisplayName("Expands on Normalization Form KD")>
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
        <UcdProperty("FC_NFKC_Closure", "DerivedNormalizationProps.txt", UnicodePropertyType.String, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Normalization), DisplayName("Case Folding Closure Mapping KC")>
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
        <UcdProperty("Numeric_Type", "UnicodeData.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Numeric), DisplayName("Numeric Type")>
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
        <UcdProperty("Numeric_Value", "UnicodeData.txt", UnicodePropertyType.Numeric, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Numeric), DisplayName("Numeric Value")>
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
        <UcdProperty("Joining_Type", "ArabicShaping.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.ShapingAndRendering), DisplayName("Joining Type")>
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
        <UcdProperty("Joining_Group", "ArabicShaping.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.ShapingAndRendering), DisplayName("Joining Group")>
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
        <UcdProperty("Join_Control", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.ShapingAndRendering), DisplayName("Join Control")>
        Public ReadOnly Property IsJoinControl As Boolean
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
        <UcdProperty("Line_Break", "LineBreak.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.ShapingAndRendering), DisplayName("Line Break")>
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
        <UcdProperty("East_Asian_Width", "EastAsianWidth.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.ShapingAndRendering), DisplayName("East Asian Width")>
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
        <UcdProperty("Uppercase", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Uppercase")>
        Public ReadOnly Property IsUppercase As Boolean
            Get
                Return GetPropertyValue("Upper") = "Y"
            End Get
        End Property
        ''' <summary>Gets value indicating if character is lowercase</summary>
        ''' <remarks>Underlying XML attribute is @Lower</remarks>
        <XmlAttribute("Lower")>
        <UcdProperty("Lowercase", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Lowercase")>
        Public ReadOnly Property IsLowercase As Boolean
            Get
                Return GetPropertyValue("Lower") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if character is other uppercase</summary>
        ''' <remarks>Underlying XML attribute is @OUpper
        ''' <para>Used in deriving the <see cref="IsLowercase"/> property.</para></remarks>
        <XmlAttribute("OUpper"), EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)>
        <UcdProperty("Other_Uppercase", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Contributory)>
        <UcdCategory(UnicodePropertyCategory.Contributory), Description("Other Uppercase")>
        Public ReadOnly Property IsOtherUppercase As Boolean
            Get
                Return GetPropertyValue("OUpper") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if character is other lowercase</summary>
        ''' <remarks>Underlying XML attribute is @OLower
        ''' <para>Used in deriving the <see cref="IsUppercase"/> property.</para></remarks>
        <XmlAttribute("OLower"), EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)>
        <UcdProperty("Other_Lowercase", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Contributory)>
        <UcdCategory(UnicodePropertyCategory.Contributory), DisplayName("Other Lowercase")>
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
        <UcdProperty("Simple_Uppercase_Mapping", "UnicodeData.txt", UnicodePropertyType.String, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Simple Uppercase Mapping")>
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
        <UcdProperty("Simple_Lowercase_Mapping", "UnicodeData.txt", UnicodePropertyType.String, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Simple Lowercase Mapping")>
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
        <UcdProperty("Simple_Titlecase_Mapping", "UnicodeData.txt", UnicodePropertyType.String, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Simple Tilecase Mapping")>
        Public ReadOnly Property SimpleTilecaseMapping As CodePointInfo
            Get
                Dim value As String = GetPropertyValue("stc", True)
                If value = "" Then Return SimpleUppercaseMapping
                Return CodePointInfo.Parse(value, Element.Document)
            End Get
        End Property

        ''' <summary>Gets uppercase mapping for this charatcer (that is one character or sequence of characters which form uppercase conterpart of current character)</summary>
        ''' <remarks>Underlying XML attribute is @uc, if it does not provide value <see cref="SimpleUppercaseMapping"/> is used instead</remarks>
        <XmlAttribute("uc")>
        <UcdProperty("Uppercase_Mapping", "SpecialCasing.txt", UnicodePropertyType.String, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Uppercase Mapping")>
        Public ReadOnly Property UppercaseMappping As CodePointInfoCollection
            Get
                Dim value As String = GetPropertyValue("uc", True)
                If value = "" Then value = GetPropertyValue("suc", True)
                Return New CodePointInfoCollection(Element.Document, value)
            End Get
        End Property

        ''' <summary>Gets lowercase mapping for this charatcer (that is one character or sequence of characters which form lowercase conterpart of current character)</summary>
        ''' <remarks>Underlying XML attribute is @lc, if it does not provide value <see cref="SimpleLowercaseMapping"/> is used instead</remarks>
        <XmlAttribute("lc")>
        <UcdProperty("Lowercase_Mapping", "SpecialCasing.txt", UnicodePropertyType.String, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Lowercase Mapping")>
        Public ReadOnly Property LowercaseMappping As CodePointInfoCollection
            Get
                Dim value As String = GetPropertyValue("lc", True)
                If value = "" Then value = GetPropertyValue("slc", True)
                Return New CodePointInfoCollection(Element.Document, value)
            End Get
        End Property

        ''' <summary>Gets uppercase mapping for this charatcer (that is one character or sequence of characters which form tilecase conterpart of current character)</summary>
        ''' <remarks>Underlying XML attribute is @tc, if it does not provide value <see cref="SimpleTilecaseMapping"/> is used instead. If it does not provide value <see cref="SimpleUppercaseMapping"/> is used instead.</remarks>
        <XmlAttribute("tc")>
        <UcdProperty("Titlecase_Mapping", "SpecialCasing.txt", UnicodePropertyType.String, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Tilecase Mapping")>
        Public ReadOnly Property TilecaseMappping As CodePointInfoCollection
            Get
                Dim value As String = GetPropertyValue("tc", True)
                If value = "" Then value = GetPropertyValue("stc", True)
                If value = "" Then value = GetPropertyValue("suc", True)
                Return New CodePointInfoCollection(Element.Document, value)
            End Get
        End Property

        ''' <summary>Gets mapping from character to its case-folded forms (if the character maps to only one character).</summary>
        ''' <remarks>Underlying XML attribute is @scf.<para>If this property does not providde value check <see cref="CaseFolding"/>.</para></remarks>
        <XmlAttribute("scf")>
        <UcdProperty("Simple_Case_Folding", "CaseFolding.txt", UnicodePropertyType.String, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Simple Case Folding")>
        Public ReadOnly Property SimpleCaseFolding As CodePointInfo
            Get
                Dim value As String = GetPropertyValue("scf", True)
                If value = "" Then Return Nothing
                Return New CodePointInfo(Element.Document, UInteger.Parse("0x" & value, Globalization.NumberStyles.HexNumber, InvariantCulture))
            End Get
        End Property

        ''' <summary>Gets mapping from character to its case-folded forms.</summary>
        ''' <remarks>Underlying XML attribute is @scf. If this attribute is not specified this propertxy returns value of <see cref="SimpleCaseFolding"/> as one-item collection.</remarks>
        <XmlAttribute("cf")>
        <UcdProperty("Case_Folding", "CaseFolding.txt", UnicodePropertyType.String, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Case Folding")>
        Public ReadOnly Property CaseFolding As CodePointInfoCollection
            Get
                Dim value As String = GetPropertyValue("cf", True)
                If value = "" Then value = GetPropertyValue("scf", True)
                If value = "" Then Return Nothing
                Return New CodePointInfoCollection(Element.Document, value)
            End Get
        End Property

        ''' <summary>Gets value idicating if the character is ignored for casing purposes</summary>
        ''' <remarks>Underlying XML attribute is @CI</remarks> 
        <XmlAttribute("CI")>
        <UcdProperty("Case_Ignorable", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Case Ignorable")>
        Public ReadOnly Property IsCaseIgnorable As Boolean
            Get
                Return GetPropertyValue("CI") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if the character either llowercase, uppercase or tilecase character</summary>
        ''' <remarks>Underlying XML attribute is @Cased</remarks> 
        <XmlAttribute("CI")>
        <UcdProperty("Cased", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Cased")>
        Public ReadOnly Property IsCased As Boolean
            Get
                Return GetPropertyValue("Cased") = "Y"
            End Get
        End Property


        ''' <summary>Gets value indicating wheather character from is unstable under case folding</summary>
        ''' <returns>True if character's normalized forms are not stable under case folding.</returns>
        ''' <remarks>Underlying XML attribute is @CWCF</remarks> 
        <XmlAttribute("CWCF")>
        <UcdProperty("Changes_When_Casefolded", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Changes when Casefolded")>
        Public ReadOnly Property ChangesWhenCasefolded As Boolean
            Get
                Return GetPropertyValue("CWCF") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if the character may change when it undergoes case mapping</summary>
        ''' <remarks>Underlying XML attribute is @CWCM</remarks> 
        <XmlAttribute("CWCM")>
        <UcdProperty("Changes_When_Casemapped", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Changes when Casemapped")>
        Public ReadOnly Property ChangesWhenCasemapped As Boolean
            Get
                Return GetPropertyValue("CWCM") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating whether character's normalized forms are not stable under to-lowercase mapping</summary>
        ''' <returns>True if character's normalized froms are not stable under to-lowercase mapping</returns>
        ''' <remarks>Underlying XML attribute is @CWL</remarks> 
        <XmlAttribute("CWL")>
        <UcdProperty("Changes_When_Lowercased", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Changes when Loweracsed")>
        Public ReadOnly Property ChangesWhenLowercased As Boolean
            Get
                Return GetPropertyValue("CWL") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating whether character's normalized forms are not stable under to-uppercase mapping</summary>
        ''' <returns>True if character's normalized froms are not stable under to-uppercase mapping</returns>
        ''' <remarks>Underlying XML attribute is @CWL</remarks> 
        <XmlAttribute("CWU")>
        <UcdProperty("Changes_When_Uppercased", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Changes when Uppercased")>
        Public ReadOnly Property ChangesWhenUppercased As Boolean
            Get
                Return GetPropertyValue("CWU") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating whether character's normalized forms are not stable under to-tilecase mapping</summary>
        ''' <returns>True if character's normalized froms are not stable under to-tilecase mapping</returns>
        ''' <remarks>Underlying XML attribute is @CWL</remarks> 
        <XmlAttribute("CWT")>
        <UcdProperty("Changes_When_Titlecased", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Changes when Tilecased")>
        Public ReadOnly Property ChangesWhenTilecased As Boolean
            Get
                Return GetPropertyValue("CWT") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if character is not identical to its Normalization Form KC casefold mapping</summary>
        ''' <returns>True if character is not identical to its Normalization Form KC casefold mapping.</returns>
        ''' <remarks>Underlying XML attribute is @CWKCF</remarks> 
        <XmlAttribute("CWKCF")>
        <UcdProperty("Changes_When_NFKC_Casefolded", "DerivedNormalizationProps.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Normalization), DisplayName("Changes when Normalization Form KC Casefold")>
        Public ReadOnly Property ChangesWhenNfKCCasefold As Boolean
            Get
                Return GetPropertyValue("CWKCF") = "Y"
            End Get
        End Property

        ''' <summary>Gets a mapping designed for best behavior when doing caseless matching of strings interpreted as identifiers</summary>
        ''' <remarks>Underlying XML attribute is @NFKC_CF</remarks>
        <XmlAttribute("NFKC_CF")>
        <UcdProperty("NFKC_Casefold", "DerivedNormalizationProps.txt", UnicodePropertyType.String, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Normalization), DisplayName("Normalization form KC Casefold")>
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
        <UcdProperty("Script", "Scripts.txt", UnicodePropertyType.Catalog, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.General), DisplayName("Script")>
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
        <UcdProperty("ISO_Comment", "UnicodeData.txt", UnicodePropertyType.Miscellaneous, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Miscellaneous), DisplayName("ISO Comment")>
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
        <UcdProperty("Hangul_Syllable_Type", "HangulSyllableType.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.General), DisplayName("Jangul Syllable Type")>
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
        <UcdProperty("Jamo_Short_Name", "Jamo.txt", UnicodePropertyType.Miscellaneous, UnicodePropertyStatus.Contributory)>
        <UcdCategory(UnicodePropertyCategory.Contributory), DisplayName("Jamo Short Name")>
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
        <UcdProperty("Indic_Syllabic_Category", "IndicSyllabicCategory.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Provisional)>
        <UcdCategory(UnicodePropertyCategory.Miscellaneous), DisplayName("Indic Syllabic Category")>
        Public ReadOnly Property IndicSyllabicCategory$
            Get
                Return GetPropertyValue("InSC")
            End Get
        End Property

        ''' <summary>A provisional property defining the structural categories of syllabic components in Indic scripts.</summary>
        ''' <remarks>Underlying XML attribute is @InMC</remarks>
        <XmlAttribute("InMC")>
        <UcdProperty("Indic_Matra_Category", "IndicMatraCategory.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Provisional)>
        <UcdCategory(UnicodePropertyCategory.Miscellaneous), DisplayName("Indic Mantra Category")>
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
        <UcdProperty("ID_Start", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Identifiers), DisplayName("Identifier Start")>
        Public ReadOnly Property IsIdStart As Boolean
            Get
                Return GetPropertyValue("IDS") = "Y"
            End Get
        End Property

        ''' <summary>Used for backward compatibility of <see cref="IsIdStart"/></summary>
        ''' <remarks>Underlying XML attribute is @OIDS</remarks>
        <XmlAttribute("OIDS"), EditorBrowsable(EditorBrowsableState.Advanced)>
        <UcdProperty("Other_ID_Start", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Contributory)>
        <UcdCategory(UnicodePropertyCategory.Contributory), DisplayName("Other Identifier Start")>
        Public ReadOnly Property IsOtherIdStart As Boolean
            Get
                Return GetPropertyValue("OIDS") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if the character can be 1st character in name of an identifier in a programming language such as VB or C# (improved version)</summary>
        ''' <remarks>Underlying XML attribute is @XIDS</remarks>
        <XmlAttribute("XIDS")>
        <UcdProperty("XID_Start", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Identifiers), DisplayName("Identifier Start alternative")>
        Public ReadOnly Property IsIdStartEx As Boolean
            Get
                Return GetPropertyValue("XIDS") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if the character can be non-1st character in name of an identifier in a programming language such as VB or C#</summary>
        ''' <remarks>Underlying XML attribute is @IDC</remarks>
        <XmlAttribute("IDC")>
        <UcdProperty("ID_Continue", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Identifiers), DisplayName("Identifier Continuation")>
        Public ReadOnly Property IsIdContinue As Boolean
            Get
                Return GetPropertyValue("IDC") = "Y"
            End Get
        End Property

        ''' <summary>Used for backward compatibility of <see cref="IsIdContinue"/></summary>
        ''' <remarks>Underlying XML attribute is @OIDC</remarks>
        <XmlAttribute("OIDC"), EditorBrowsable(EditorBrowsableState.Advanced)>
        <UcdProperty("Other_ID_Continue", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Contributory)>
        <UcdCategory(UnicodePropertyCategory.Contributory), DisplayName("Other Identifier Continuation")>
        Public ReadOnly Property IsOtherIdContinue As Boolean
            Get
                Return GetPropertyValue("OIDC") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if the character can be non-1st character in name of an identifier in a programming language such as VB or C# (improved version)</summary>
        ''' <remarks>Underlying XML attribute is @XIDC</remarks>
        <XmlAttribute("XIDC")>
        <UcdProperty("XID_Continue", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Identifiers), DisplayName("Identifier Continuation alternative")>
        Public ReadOnly Property IsIdContinueEx As Boolean
            Get
                Return GetPropertyValue("XIDC") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if charatcer can be used in syntax of programming language</summary>
        ''' <remarks>Underlying XML attribute is @Pat_Syn</remarks>
        <XmlAttribute("Pat_Syn")>
        <UcdProperty("Pattern_Syntax", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Identifiers), DisplayName("Pattern Syntax")>
        Public ReadOnly Property IsPatternSyntax As Boolean
            Get
                Return GetPropertyValue("Pat_Syn") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if charatcer shopuld be treated as whitespace by programming language compiler or interpreter</summary>
        ''' <remarks>Underlying XML attribute is @Pat_WS</remarks>
        <XmlAttribute("Pat_WS")>
        <UcdProperty("Pattern_White_Space", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Identifiers), DisplayName("Pattern Whitespace")>
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
        <UcdProperty("Dash", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Miscellaneous), DisplayName("Dash")>
        Public ReadOnly Property IsDash As Boolean
            Get
                Return GetPropertyValue("Dash") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this character represents a dash used to mark connections between pieces of words (includes Katakana middle dot)</summary>
        ''' <remarks>Underlying XML attribute is @Hyphen
        ''' <para>This property is deprecated as of Unicode 6.0</para></remarks>
        <XmlAttribute("Hyphen"), Obsolete("This property is deprecated as of Unicode 6.0")>
        <UcdProperty("Hyphen", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Miscellaneous), DisplayName("Hyphen")>
        Public ReadOnly Property IsHyphen As Boolean
            Get
                Return GetPropertyValue("Hyphen") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this character is a punctuation that functions as quotation mark</summary>
        ''' <remarks>Underlying XML attribute is @QMark</remarks>
        <XmlAttribute("QMark")>
        <UcdProperty("Quotation_Mark", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Miscellaneous), DisplayName("Quotation Mark")>
        Public ReadOnly Property IsQuotationMark As Boolean
            Get
                Return GetPropertyValue("QMark") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this character is a punctuation that generally marks the end of textual units.</summary>
        ''' <remarks>Underlying XML attribute is @Term</remarks>
        <XmlAttribute("Term")>
        <UcdProperty("Terminal_Punctuation", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Miscellaneous), DisplayName("Terminal Punctuation")>
        Public ReadOnly Property IsTerminalPunctuation As Boolean
            Get
                Return GetPropertyValue("Term") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this character is sentence terminal</summary>
        ''' <remarks>Underlying XML attribute is @STerm</remarks>
        <XmlAttribute("STerm")>
        <UcdProperty("STerm", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Miscellaneous), DisplayName("Sentence Terminal")>
        Public ReadOnly Property IsSentenceTerminal As Boolean
            Get
                Return GetPropertyValue("STerm") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this character is diacritics (it linguistically modifies the meaning of another character to which it applies).</summary>
        ''' <remarks>Underlying XML attribute is @Dia
        ''' <para>Note: Some diacritics are not combining characters and some combining characters are not diacritics.</para></remarks>
        <XmlAttribute("Dia")>
        <UcdProperty("Diacritic", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Miscellaneous), DisplayName("Diacritic")>
        Public ReadOnly Property IsDiacritic As Boolean
            Get
                Return GetPropertyValue("Dia") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this character is an extender (its principal function is to extend value or shape of a preceding alphabetic character)</summary>
        ''' <remarks>Underlying XML attribute is @Ext</remarks>
        <XmlAttribute("Ext")>
        <UcdProperty("Extender", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Miscellaneous), DisplayName("Extender")>
        Public ReadOnly Property IsExtender As Boolean
            Get
                Return GetPropertyValue("Ext") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this has a soft dot (usch as j or i)</summary>
        ''' <remarks>Underlying XML attribute is @SD
        ''' <para>An accent placed on this character causes the dot to disappear. Explicit dot above can be added if required.</para></remarks>
        <XmlAttribute("SD")>
        <UcdProperty("Soft_Dotted", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Case), DisplayName("Soft-dotted")>
        Public ReadOnly Property IsSoftDotted As Boolean
            Get
                Return GetPropertyValue("SD") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this character is alphabetic</summary>
        ''' <remarks>Underlying XML attribute is @Alpha</remarks>
        <XmlAttribute("Alpha")>
        <UcdProperty("Alphabetic", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.General), DisplayName("Alphabetic")>
        Public ReadOnly Property IsAlphabetic As Boolean
            Get
                Return GetPropertyValue("Alpha") = "Y"
            End Get
        End Property

        ''' <summary>Used in deriving <see cref="IsAlphabetic"/> property.</summary>
        ''' <remarks>Underlying XML attribute is @OAlpha</remarks>
        <XmlAttribute("OAlpha"), EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)>
        <UcdProperty("Other_Alphabetic", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Contributory)>
        <UcdCategory(UnicodePropertyCategory.Contributory), DisplayName("Other Alphabetic")>
        Public ReadOnly Property IsOtherAlphabetic As Boolean
            Get
                Return GetPropertyValue("OAlpha") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this is math character</summary>
        ''' <remarks>Underlying XML attribute is @Math</remarks>
        <XmlAttribute("Math")>
        <UcdProperty("Math", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Miscellaneous), DisplayName("Math")>
        Public ReadOnly Property IsMath As Boolean
            Get
                Return GetPropertyValue("Math") = "Y"
            End Get
        End Property

        ''' <summary>Used in deriving <see cref="IsMath"/> property</summary>
        ''' <remarks>Underlying XML attribute is @OMath</remarks>
        <XmlAttribute("OMath"), EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)>
        <UcdProperty("Other_Math", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Contributory)>
        <UcdCategory(UnicodePropertyCategory.Contributory), DisplayName("Other Math")>
        Public ReadOnly Property IsOtherMath As Boolean
            Get
                Return GetPropertyValue("OMath") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this character is commonly used to represent hexadigit</summary>
        ''' <remarks>Underlying XML attribute is @Hex</remarks>
        <XmlAttribute("Hex")>
        <UcdProperty("Hex_Digit", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Numeric), DisplayName("Hexa-digit")>
        Public ReadOnly Property IsHexaDigit As Boolean
            Get
                Return GetPropertyValue("Hex") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this character is an ASCII character used commonly to represent hexadigit</summary>
        ''' <remarks>Underlying XML attribute is @AHex</remarks>
        <XmlAttribute("AHex")>
        <UcdProperty("ASCII_Hex_Digit", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Numeric), DisplayName("ASCII Hexa-digit")>
        Public ReadOnly Property IsAsciiHex As Boolean
            Get
                Return GetPropertyValue("AHex") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this is default ignorable code point - this is it should be ignored in renderign unless explicitly supported.</summary>
        ''' <remarks>Underlying XML attribute is @DI</remarks>
        <XmlAttribute("DI")>
        <UcdProperty("Default_Ignorable_Code_Point", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.General), DisplayName("Default Ignorable Code Point")>
        Public ReadOnly Property IsDefaultIgnorable As Boolean
            Get
                Return GetPropertyValue("DI") = "Y"
            End Get
        End Property

        ''' <summary>Used in deriving <see cref="IsDefaultIgnorable"/> property</summary>
        ''' <remarks>Underlying XML attribute is @ODI</remarks>
        <XmlAttribute("ODI"), EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)>
        <UcdProperty("Other_Default_Ignorable_Code_Point", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Contributory)>
        <UcdCategory(UnicodePropertyCategory.Contributory), DisplayName("Other Default Ignorable Code Point")>
        Public ReadOnly Property IsOtherDefaultIgnorable As Boolean
            Get
                Return GetPropertyValue("ODI") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this character represents a lexical order exception</summary>
        ''' <remarks>Underlying XML attribute is @LOE<para>A small number of spacing vowel letters occurring in certain Southeast Asian scripts such as Thai and Lao, which use a visual order display model. These letters are stored in text ahead of syllable-initial consonants, and require special handling for processes such as searching and sorting.</para></remarks>
        <XmlAttribute("LOE")>
        <UcdProperty("Logical_Order_Exception", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.General), DisplayName("Logical Order Exception")>
        Public ReadOnly Property IsLogicalOrderException As Boolean
            Get
                Return GetPropertyValue("LOE") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this character is whitespace.</summary>
        ''' <remarks>Underlying XML attribute is @WSpace</remarks>
        <XmlAttribute("WSpace")>
        <UcdProperty("White_Space", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.General), DisplayName("Whitespace")>
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
        <UcdProperty("Grapheme_Base", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Miscellaneous), DisplayName("Grapheme Base")>
        Public ReadOnly Property IsGraphemeBase As Boolean
            Get
                Return GetPropertyValue("Gr_Base") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if this character is grapheme extend.</summary>
        ''' <remarks>Underlying XML attribute is @Gr_Ext</remarks>
        <XmlAttribute("Gr_Ext")>
        <UcdProperty("Grapheme_Extend", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Miscellaneous), DisplayName("Grapheme Extend")>
        Public ReadOnly Property IsGraphemeExtend As Boolean
            Get
                Return GetPropertyValue("Gr_Ext") = "Y"
            End Get
        End Property

        ''' <summary>Used in deriving the <see cref="IsGraphemeExtend"/> property.</summary>
        ''' <remarks>Underlying XML attribute is @OGr_Ext</remarks>
        <XmlAttribute("OGr_Ext"), EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)>
        <UcdProperty("Other_Grapheme_Extend", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Contributory)>
        <UcdCategory(UnicodePropertyCategory.Contributory), DisplayName("Other Grapheme Extend")>
        Public ReadOnly Property IsOtherGraphemeExtend As Boolean
            Get
                Return GetPropertyValue("OGr_Ext") = "Y"
            End Get
        End Property

        ''' <summary>Formerly proposed for programatic determination of grapheme cluster boundaries.</summary>
        ''' <remarks>Underlying XML attribute is @Gr_Link<para>This property is deprecated as of Unicode 5.0</para></remarks>
        <XmlAttribute("Gr_Link"), Obsolete("Deprecated as of Unicode 5.0")>
         <UcdProperty("Grapheme_Link ", "DerivedCoreProperties.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Miscellaneous), DisplayName("Grapheme Link")>
        Public ReadOnly Property IsGraphemeLink As Boolean
            Get
                Return GetPropertyValue("Gr_Link") = "Y"
            End Get
        End Property

        ''' <summary>Gets type of grapheme cluster break</summary>
        ''' <exception cref="InvalidOperationException">Value of underlying XML attribute is not one of expected values</exception>
        ''' <remarks>Underlying XML attribute is @GCB</remarks>
        <XmlAttribute("GCB"), DefaultValue(UnicodeGraphemeClusterBreak.other)>
        <UcdProperty("Grapheme_Cluster_Break", "GraphemeBreakProperty.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.ShapingAndRendering), DisplayName("Grapheme Cluster Break")>
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
        <UcdProperty("Word_Break", "WordBreakProperty.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.ShapingAndRendering), DisplayName("Word Break")>
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
        <UcdProperty("Sentence_Break", "SentenceBreakProperty.txt", UnicodePropertyType.Enumeration, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.ShapingAndRendering), DisplayName("Sentence Break")>
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
#End Region

#Region "Properties related to ideographs"
        ''' <summary>Gets value indicating if the character is CKJV ideograph</summary>
        ''' <remarks>Underlying XML attribute is @Ideo</remarks>
        <XmlAttribute("Ideo")>
        <UcdProperty("Ideographic", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Informative)>
        <UcdCategory(UnicodePropertyCategory.Cjk), DisplayName("Ideographic")>
        Public ReadOnly Property IsIdeograph As Boolean
            Get
                Return GetPropertyValue("Ideo") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if the character is unified CJK ideograph</summary>
        ''' <remarks>Underlying XML attribute is @UIdeo</remarks>
        <XmlAttribute("UIdeo")>
        <UcdProperty("Unified_Ideograph", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Cjk), DisplayName("Unified Ideograph")>
        Public ReadOnly Property IsUnifiedIdeograph As Boolean
            Get
                Return GetPropertyValue("UIdeo") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if character is ideographic descriptionsequence binary operator</summary>
        ''' <remarks>Underlying XML attribute is @ISDB</remarks>
        <XmlAttribute("ISDB")>
        <UcdProperty("IDS_Binary_Operator", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Cjk), DisplayName("Ideographic Description Sequence Binary Operator")>
        Public ReadOnly Property IsIdsBinaryOperator As Boolean
            Get
                Return GetPropertyValue("ISDB") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if character is ideographic description sequence trinary operator</summary>
        ''' <remarks>Underlying XML attribute is @ISDT</remarks>
        <XmlAttribute("ISDT")>
        <UcdProperty("IDS_Trinary_Operator", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Cjk), DisplayName("Ideographic Description Sequence Trinary Operator")>
        Public ReadOnly Property IsIdsTrinaryOperator As Boolean
            Get
                Return GetPropertyValue("ISDT") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if character is radical (for ideographic description sequences)</summary>
        ''' <remarks>Underlying XML attribute is @Radical</remarks>
        <XmlAttribute("Radical")>
        <UcdProperty("Radical", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.Cjk), DisplayName("CJK Radical")>
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
        <UcdProperty("Deprecated", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.General), DisplayName("Deprecated")>
        Public ReadOnly Property IsDeprecated As Boolean
            Get
                Return GetPropertyValue("Dep") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if the character is Variation Selector</summary>
        ''' <remarks>Underlying XML attribute is @VS</remarks>
        <XmlAttribute("VS")>
        <UcdProperty("Variation_Selector", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.General), DisplayName("Variation Selector")>
        Public ReadOnly Property IsVariationSelector As Boolean
            Get
                Return GetPropertyValue("VS") = "Y"
            End Get
        End Property

        ''' <summary>Gets value indicating if a code point is permanent non-character (i.e. the code point is permanently reserved ofr internal use)</summary>
        ''' <remarks>Underlying XML attribute is @NChar</remarks>
        <XmlAttribute("NChar")>
        <UcdProperty("Noncharacter_Code_Point", "PropList.txt", UnicodePropertyType.Binary, UnicodePropertyStatus.Normative)>
        <UcdCategory(UnicodePropertyCategory.General), DisplayName("Non-character Code Point")>
        Public ReadOnly Property IsNonCharacter As Boolean
            Get
                Return GetPropertyValue("NChar") = "Y"
            End Get
        End Property
#End Region

#Region "Unihan"
        'Unihan properties in XML: http://www.unicode.org/reports/tr42/#w1aac13c13c49b1
        'Unihan Database - property descriptions: http://www.unicode.org/reports/tr38/#AlphabeticalListing
        'TODO: 4.4.21

#Region "Helpers"
        ''' <summary>Unihan helper - gets value of XML attribute and parses it as space-separated array of decimal integers as used in Unihan database</summary>
        ''' <param name="attributeName">Name of the XML attribute to parse value of</param>
        ''' <returns>Parsed values from attribute value. An empty array if the attribute is not present or it is empty.</returns>
        ''' <exception cref="FormatException">Value cannot be parsed as integer</exception>
        ''' <exception cref="OverflowException">Value can be parsed as number but it's too big (small) for <see cref="Int32"/>.</exception>
        Protected Function GetIntArray(attributeName$) As Integer()
            Dim value As String = GetPropertyValue(attributeName$)
            If value.IsNullOrEmpty Then Return EmptyArray.Int32
            Return (From str In value.Split(" "c) Select Integer.Parse(str, InvariantCulture)).ToArray
        End Function
        ''' <summary>Unihan helper - gets value of XML attribute and parses it as space-separated array of hexadecimal integers as used in Unihan database</summary>
        ''' <param name="attributeName">Name of the XML attribute to parse value of</param>
        ''' <returns>Parsed values from attribute value. An empty array if the attribute is not present or it is empty.</returns>
        ''' <exception cref="FormatException">Value cannot be parsed as hex integer</exception>
        ''' <exception cref="OverflowException">Value can be parsed as number but it's too big (small) for <see cref="Int32"/>.</exception>
        Protected Function GetHexArray(attributeName$) As Integer()
            Dim value As String = GetPropertyValue(attributeName$)
            If value.IsNullOrEmpty Then Return EmptyArray.Int32
            Return (From str In value.Split(" "c) Select Integer.Parse("0x" & str, Globalization.NumberStyles.HexNumber, InvariantCulture)).ToArray
        End Function
        ''' <summary>Unihan helper - gets value of XML attribute stored as space-separated array of strings (this format is used in Unihan database)</summary>
        ''' <param name="attributeName">Name of the XML attribute to parse value of</param>
        ''' <returns>Array obtained by splitting space-delimited string. An empty array if attribute is not present or it's empty.</returns>
        Protected Function GetStringArray(attributeName$) As String()
            Dim value As String = GetPropertyValue(attributeName)
            If value.IsNullOrEmpty Then Return EmptyArray.String
            Return value.Split(" "c)
        End Function
        ''' <summary>Unihan helper - gets value of XML attribute stored as space-separated array of Unicode code points in format <c>U\+2?[0-9A-F]{4}</c></summary>
        ''' <param name="attributeName">Name of the XML attribute to parse value of</param>
        ''' <returns>
        ''' Array of strings (not chars because code point can represent non-UTF-16 character (i.e. surrogate pair in UTF-16)) - each item in array represents one code-point (so it's either single chaaracter or surrogate pair).
        ''' An empty array if attribute is not present or it's empty.
        ''' </returns>
        ''' <exception cref="FormatException">Value following the U+ prefix cannot be parsed as hexadecimal integer. -or- Any string in space-separated array does not start with U+.</exception>
        ''' <exception cref="OverflowException">Value following the U+ prefix can be parsed as number but it's either to low or to big for <see cref="Int32"/> type.</exception>
        ''' <exception cref="ArgumentOutOfRangeException">Value following the U+ prefix does not represent a valid 21-bit Unicode code point (see <see cref="System.Char.ConvertFromUtf32"/>).</exception>
        Protected Function GetUnicodeArray(attributeName$) As String()
            Dim value As String = GetPropertyValue(attributeName)
            If value.IsNullOrEmpty Then Return EmptyArray.String
            Return (
                From str In value.Split(" "c)
                Select Char.ConvertFromUtf32(Integer.Parse("0x" & (
                    Function()
                        If Not str.StartsWith("U+") Then Throw New FormatException(UnicodeResources.ex_CodepointMustStartWithUPlus)
                        Return str.Substring(2)
                    End Function
                )(), Globalization.NumberStyles.HexNumber, InvariantCulture))).ToArray
        End Function

        ''' <summary>Unihan helper - gets value of XML attribute stored as space-separated decimal numbers</summary>
        ''' <param name="attributeName">Name of the XML attribute to parse value of</param>
        ''' <returns>Array of <see cref="Decimal"/> values parsed from attribute <paramref name="attributeName"/> value. An empty array if the attribute is either not present or empty.</returns>
        ''' <exception cref="FormatException">A value cannot be parsed as <see cref="Decimal"/></exception>
        ''' <exception cref="OverflowException">A value can be parsed as number but is too high or too low for datatype <see cref="Decimal"/>.</exception>
        Protected Function GetDecimalArray(attributeName$) As Decimal()
            Dim value As String = GetPropertyValue(attributeName)
            If value.IsNullOrEmpty Then Return EmptyArray.Decimal
            Return (From str In value.Split(" "c) Select Decimal.Parse(str, InvariantCulture)).ToArray
        End Function
#End Region

        ''' <summary>The value of the character when used in the writing of accounting numerals.</summary>
        ''' <remarks>Accounting numerals are used in East Asia to prevent fraud. Because a number like ten (十) is easily turned into one thousand (千) with a stroke of a brush, monetary documents will often use an accounting form of the numeral ten (such as 拾) in their place.</remarks>
        <XmlAttribute("kAccountingNumeric")>
        <UcdProperty("kAccountingNumeric", UnihanPropertyCategory.NumericValues, UnicodePropertyStatus.Informative)>
        <UcdCategoryUnihan(UnihanPropertyCategory.NumericValues), DisplayName("Unihan Accounting Numeric")>
        Public ReadOnly Property HanAccountingNumeric As Integer()
            Get
                Return GetIntArray("kAccountingNumeric")
            End Get
        End Property

        ''' <summary>The Big Five mapping for this character</summary>
        ''' <remarks><note>This does not cover any of the Big Five extensions in common use, including the ETEN extensions.</note></remarks>
        <XmlAttribute("kBigFive")>
        <UcdProperty("kBigFive", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan Big 5 Mapping")>
        Public ReadOnly Property HanBig5 As Integer()
            Get
                Return GetHexArray("kBigFive")
            End Get
        End Property

        ''' <summary>The cangjie input code for the character.</summary>
        ''' <remarks>This incorporates data from the file cangjie-table.b5 by Christian Wittern.</remarks>
        <XmlAttribute("kCangjie")>
        <UcdProperty("kCangjie", UnihanPropertyCategory.DictionaryLikeData, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryLikeData), DisplayName("Unihan Cangjie code")>
        Public ReadOnly Property HanCangjie As String()
            Get
                Return GetStringArray("kCangjie")
            End Get
        End Property

        ''' <summary>The Cantonese pronunciation(s) for this character using the jyutping romanization.</summary>
        <XmlAttribute("kCantonese")>
        <UcdProperty("kCantonese", UnihanPropertyCategory.Readings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Readings), DisplayName("Unihan Cantonese Pronounciation")>
        Public ReadOnly Property HanCantonese As String()
            Get
                Return GetStringArray("kCantonese")
            End Get
        End Property

        ''' <summary>The CCCII mapping for this character</summary>
        <XmlAttribute("kCCCII")>
        <UcdProperty("kCCCII", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan CCCII Mapping")>
        Public ReadOnly Property HanCccii As Integer()
            Get
                Return GetHexArray("kCCCII")
            End Get
        End Property

        'code-point-properties &= attribute kCheungBauer
        '   { text }?
        ''' <summary>Data regarding the character in Cheung Kwan-hin and Robert S. Bauer, <em>The Representation of Cantonese with Chinese Characters</em>, Journal of Chinese Linguistics, Monograph Series Number 18, 2002</summary>
        ''' <remarks>
        ''' The string(s) returned have special format:
        ''' The data consist of three pieces, separated by semicolons:
        ''' <list type="list">
        ''' <item>The character’s radical-stroke index as a three-digit radical, slash, two-digit stroke count</item>
        ''' <item>The character’s cangjie input code (if any)</item>
        ''' <item>A comma-separated list of Cantonese readings using the jyutping romanization in alphabetical order.</item>
        ''' </list>
        ''' </remarks>
        <XmlAttribute("kCheungBauer")>
        <UcdProperty("kCheungBauer", UnihanPropertyCategory.DictionaryLikeData, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryLikeData), DisplayName("Unihan Cheung-Bauer Reading")>
        Public ReadOnly Property HanCheungBauer As String()
            Get
                Return GetStringArray("kCheungBauer")
            End Get
        End Property

        ''' <summary>The position of the character in Cheung Kwan-hin and Robert S. Bauer, <em>The Representation of Cantonese with Chinese Characters</em>, Journal of Chinese Linguistics, Monograph Series Number 18, 2002</summary>
        ''' <remarks>The format is a three-digit page number followed by a two-digit position number, separated by a period.</remarks>
        <XmlAttribute("kCheungBauerIndex")>
        <UcdProperty("kCheungBauerIndex", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), DisplayName("Unihan Cheung-Bauer Index")>
        Public ReadOnly Property HanCheungBauerIndex As String()
            Get
                Return GetStringArray("kCheungBauerIndex")
            End Get
        End Property

        ''' <summary>The position of this character in the Cihai (辭海) dictionary, single volume edition, published in Hong Kong by the Zhonghua Bookstore, 1983 (reprint of the 1947 edition), ISBN 962-231-005-2.</summary>
        ''' <remarks>The position is indicated by a decimal number. The digits to the left of the decimal are the page number. The first digit after the decimal is the row on the page, and the remaining two digits after the decimal are the position on the row.</remarks>
        <XmlAttribute("kCihaiT")>
        <UcdProperty("kCihaiT", UnihanPropertyCategory.DictionaryLikeData, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryLikeData), DisplayName("Unihan Cihai")>
        Public ReadOnly Property HanCihai As String()
            Get
                Return GetStringArray("kCihaiT")
            End Get
        End Property

        ''' <summary>The CNS 11643-1986 mapping for this character in hex.</summary>
        ''' <remarks>The strings returned by this property match to following regular expression <c>^[12E]-[0-9A-F]{4}$</c></remarks>
        <XmlAttribute("kCNS1986")>
        <UcdProperty("kCNS1986", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan CNS 11643-1986 Mapping")>
        Public ReadOnly Property HanCns1986 As String()
            Get
                Return GetStringArray("kCNS1986")
            End Get
        End Property

        ''' <summary>The CNS 11643-1992 mapping for this character in hex.</summary>
        ''' <remarks>The strings returned by this property match to following regular expression <c>^[1-9]-[0-9A-F]{4}$</c></remarks>
        <XmlAttribute("kCNS1992")>
        <UcdProperty("kCNS1992", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan CNS 11643-1992 Mapping")>
        Public ReadOnly Property HanCns1992 As String()
            Get
                Return GetStringArray("kCNS1992")
            End Get
        End Property

        ''' <summary>The compatibility decomposition for this ideograph</summary>
        ''' <remarks>Derived from the UnicodeData.txt file.</remarks>
        <XmlAttribute("kCompatibilityVariant")>
        <UcdProperty("kCompatibilityVariant", UnihanPropertyCategory.Variants, UnicodePropertyStatus.Normative)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Variants), DisplayName("Unihan Compatibility Variant")>
        Public ReadOnly Property HanCompatibilityVariant As String()
            Get
                Return GetUnicodeArray("kCompatibilityVariant")
            End Get
        End Property

        ''' <summary>The index or indices of this character in Roy T. Cowles, A Pocket Dictionary of Cantonese, Hong Kong: University Press, 1999.</summary>
        ''' <remarks>The Cowles indices are numerical, usually integers but occasionally fractional where a character was added after the original indices were determined. Cowles is missing indices 1222 and 4949, and four characters in Cowles are part of Unicode’s “Hangzhou” numeral set: 2964 (U+3025), 3197 (U+3028), 3574 (U+3023), and 4720 (U+3027).
        ''' <para>Approximately 100 characters from Cowles which are not currently encoded are being submitted to the IRG by Unicode for inclusion in future versions of the standard.</para></remarks>
        <XmlAttribute("kCowles")>
        <UcdProperty("kCowles", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), DisplayName("Unihan Cowles")>
        Public ReadOnly Property HanCowles As Decimal()
            Get
                Return getDecimalArray("kCowles")
            End Get
        End Property

        'code-point-properties &= attribute kAlternateHanYu
        '   { text }?  #old

        'code-point-properties &= attribute kAlternateJEF
        '   { text }?  #old

        'code-point-properties &= attribute kAlternateKangXi
        '   { text }?

        'code-point-properties &= attribute kAlternateMorohashi
        '   { text }?   

        'code-point-properties &= attribute kDaeJaweon
        '   { xsd:string {pattern="[0-9]{4}\.[0-9]{2}[0158]"} }?

        'code-point-properties &= attribute kDefinition
        '   { text }?

        'code-point-properties &= attribute kEACC
        '   { xsd:string {pattern="[0-9A-F]{6}"} }?

        'code-point-properties &= attribute kFenn
        '   { list { xsd:string {pattern="[0-9]+a?[A-KP*]"} +}}?

        'code-point-properties &= attribute kFennIndex
        '   { list { xsd:string {pattern="[1-9][0-9]{0,2}\.[01][0-9]"} +}}?

        'code-point-properties &= attribute kFourCornerCode
        '   { list { xsd:string {pattern="[0-9]{4}(\.[0-9])?"} +}}?

        'code-point-properties &= attribute kFrequency
        '   { xsd:string {pattern="[1-5]"} }?

        'code-point-properties &= attribute kGB0
        '   { xsd:string {pattern="[0-9A-F]{4}"} }?

        'code-point-properties &= attribute kGB1
        '   { xsd:string {pattern="[0-9A-F]{4}"} }?

        'code-point-properties &= attribute kGB3
        '   { xsd:string {pattern="[0-9A-F]{4}"} }?

        'code-point-properties &= attribute kGB5
        '   { xsd:string {pattern="[0-9A-F]{4}"} }?

        'code-point-properties &= attribute kGB7
        '   { xsd:string {pattern="[0-9A-F]{4}"} }?

        'code-point-properties &= attribute kGB8
        '   { xsd:string {pattern="[0-9]{4}"} }?

        'code-point-properties &= attribute kGradeLevel
        '   { xsd:string {pattern="[1-6]"} }?

        'code-point-properties &= attribute kGSR
        '   { list { xsd:string {pattern="[0-9]{4}[a-vx-z]'*"} +}}?

        'code-point-properties &= attribute kHangul
        '   { text }?

        'code-point-properties &= attribute kHanYu
        '   { list { xsd:string {pattern="[1-8][0-9]{4}\.[0-9]{2}[0-3]"} +}}?

        'code-point-properties &= attribute kHanyuPinlu
        '   { list { xsd:string {pattern="[a-zü̈]+[1-5]\([0-9]+\)"} +}}?

        'code-point-properties &= attribute kHanyuPinyin
        '   { list { xsd:string {pattern="([0-9]{5}\.[0-9]{2}0,)*[0-9]{5}\.[0-9]{2}0:([a-z̀-̂̄̈̌]+,)*[a-z̀-̂̄̈̌]+"} +}}?

        'code-point-properties &= attribute kHDZRadBreak
        '   { xsd:string {pattern="[⼀-⿕]\[U\+2?[0-9A-F]{4}\]:[1-8][0-9]{4}\.[0-9]{2}[012]"} }?

        'code-point-properties &= attribute kHKGlyph
        '   { list { xsd:string {pattern="[0-9]{4}"} +}}?

        'code-point-properties &= attribute kHKSCS
        '   { xsd:string {pattern="[0-9A-F]{4}"} }?

        'code-point-properties &= attribute kIBMJapan
        '   { xsd:string {pattern="F[ABC][0-9A-F]{2}"} }?

        'code-point-properties &= attribute kIICore
        '   { xsd:string {pattern="[1-9]\.[1-9]"} }?

        'code-point-properties &= attribute kIRGDaeJaweon
        '   { xsd:string {pattern="([0-9]{4}\.[0-9]{2}[01])|(0000\.555)"} }?

        'code-point-properties &= attribute kIRGDaiKanwaZiten
        '   { xsd:string {pattern="[0-9]{5}'?"} }?

        'code-point-properties &= attribute kIRGHanyuDaZidian
        '   { xsd:string {pattern="[1-8][0-9]{4}\.[0-3][0-9][01]"} }?

        'code-point-properties &= attribute kIRGKangXi
        '   { xsd:string {pattern="[01][0-9]{3}\.[0-7][0-9][01]"} }?

        'code-point-properties &= attribute kIRG_GSource
        '   { "" | xsd:string {pattern="(0|1|2|3|5|7|8|9|E|S|(4K)|(BK)|(CH)|(CY)|(FZ)|(FZ_BK)|(HC)|(HZ)|(KX)|(ZJW)|(ZFY)|(CYY)|(GJZ)|(XC)|(GH))(-)?([0-9A-F]{4,6})?"}
        '        | xsd:string {pattern="G0-[0-9A-F]{4}"}
        '        | xsd:string {pattern="G1-[0-9A-F]{4}"}
        '        | xsd:string {pattern="G3-[0-9A-F]{4}"}
        '        | xsd:string {pattern="G5-[0-9A-F]{4}"}
        '        | xsd:string {pattern="G7-[0-9A-F]{4}"}
        '        | xsd:string {pattern="GS-[0-9A-F]{4}"}
        '        | xsd:string {pattern="G8-[0-9A-F]{4}"}
        '        | xsd:string {pattern="G9-[0-9A-F]{4}"}
        '        | xsd:string {pattern="GE-[0-9A-F]{4}"}
        '        | xsd:string {pattern="G4K"}
        '        | xsd:string {pattern="GBK"}
        '        | xsd:string {pattern="GBK-[0-9]{4}\.[0-9]{2}"}
        '        | xsd:string {pattern="GCH"}
        '        | xsd:string {pattern="GCH-[0-9]{4}\.[0-9]{2}"}
        '        | xsd:string {pattern="GCY"}
        '        | xsd:string {pattern="GCYY-[0-9]{5}"}
        '        | xsd:string {pattern="GFZ"}
        '        | xsd:string {pattern="GFZ-[0-9]{5}"}
        '        | xsd:string {pattern="GGH-[0-9]{4}\.[0-9]{2}"}
        '        | xsd:string {pattern="GHC"}
        '        | xsd:string {pattern="GHC-[0-9]{4}\.[0-9]{2}"}
        '        | xsd:string {pattern="GHZ"}
        '        | xsd:string {pattern="GHZ-[0-9]{5}\.[0-9]{2}"}
        '        | xsd:string {pattern="GIDC-[0-9]{3}"}
        '        | xsd:string {pattern="GJZ-[0-9]{5}"}
        '        | xsd:string {pattern="GKX-[0-9]{4}\.[0-9]{2}"}
        '        | xsd:string {pattern="GXC-[0-9]{4}\.[0-9]{2}"}
        '        | xsd:string {pattern="GZFY-[0-9]{5}"}
        '        | xsd:string {pattern="GZH-[0-9]{4}\.[0-9]{2}"}
        '        | xsd:string {pattern="GZJW-[0-9]{5}"} }?

        'code-point-properties &= attribute kIRG_HSource
        '   { "" | xsd:string {pattern="[0-9A-F]{4}"}
        '        | xsd:string {pattern="H-[0-9A-F]{4}"} }?

        'code-point-properties &= attribute kIRG_JSource
        '   { "" | xsd:string {pattern="(0|1|3|(3A)|4|A|(ARIB)|K)-[0-9A-F]{4,5}"} 
        '        | xsd:string {pattern="J0-[0-9A-F]{4}"}
        '        | xsd:string {pattern="J1-[0-9A-F]{4}"}
        '        | xsd:string {pattern="J3-[0-9A-F]{4}"}
        '        | xsd:string {pattern="J3A-[0-9A-F]{4}"}
        '        | xsd:string {pattern="J4-[0-9A-F]{4}"}
        '        | xsd:string {pattern="JA-[0-9A-F]{4}"}
        '        | xsd:string {pattern="JH-[0-9A-Z]{6,7}"}
        '        | xsd:string {pattern="JK-[0-9]{5}"}
        '        | xsd:string {pattern="JARIB-[0-9A-F]{4}"} }?


        'code-point-properties &= attribute kIRG_KPSource
        '   { "" | xsd:string {pattern="((KP0)|(KP1))-[0-9A-F]{4}"} }?

        'code-point-properties &= attribute kIRG_KSource
        '   { "" | xsd:string {pattern="((0|1|2|3|4|5)-[0-9A-F]{4})|(KZ[0-9]{6})"}
        '        | xsd:string {pattern="K0-[0-9A-F]{4}"}
        '        | xsd:string {pattern="K1-[0-9A-F]{4}"}
        '        | xsd:string {pattern="K2-[0-9A-F]{4}"}
        '        | xsd:string {pattern="K3-[0-9A-F]{4}"}
        '        | xsd:string {pattern="K4-[0-9A-F]{4}"}
        '        | xsd:string {pattern="K5-[0-9A-F]{4}"} }?

        'code-point-properties &= attribute kIRG_MSource
        '   { "" | xsd:string {pattern="MAC[0-9]{5}"}
        '        | xsd:string {pattern="MAC-[0-9]{5}"} }?

        'code-point-properties &= attribute kIRG_TSource
        '   { "" | xsd:string {pattern="(1-[0-9A-F]{4})|(2-[0-9A-F]{4})|(3-[0-9A-F]{4})|(4-[0-9A-F]{4})|(5-[0-9A-F]{4})|(6-[0-9A-F]{4})|(7-[0-9A-F]{4})|(F-[0-9A-F]{4})|(C-[0-9A-F]{4})|(D-[0-9A-F]{4})|(E-[0-9A-F]{4})"}
        '        | xsd:string {pattern="T1-[0-9A-F]{4}"}
        '        | xsd:string {pattern="T2-[0-9A-F]{4}"}
        '        | xsd:string {pattern="T3-[0-9A-F]{4}"}
        '        | xsd:string {pattern="T4-[0-9A-F]{4}"}
        '        | xsd:string {pattern="T5-[0-9A-F]{4}"}
        '        | xsd:string {pattern="T6-[0-9A-F]{4}"}
        '        | xsd:string {pattern="T7-[0-9A-F]{4}"}
        '        | xsd:string {pattern="TB-[0-9A-F]{4}"}
        '        | xsd:string {pattern="TC-[0-9A-F]{4}"}
        '        | xsd:string {pattern="TD-[0-9A-F]{4}"}
        '        | xsd:string {pattern="TE-[0-9A-F]{4}"}
        '        | xsd:string {pattern="TF-[0-9A-F]{4}"} }?


        'code-point-properties &= attribute kIRG_USource
        '   { "" | xsd:string {pattern="(U\+2?[0-9A-F]{4})|(UTC[0-9]{5})"} }?

        'code-point-properties &= attribute kIRG_VSource
        '   { "" | xsd:string {pattern="(0|1|2|3|4)-[0-9A-F]{4}"}
        '        | xsd:string {pattern="V0-[0-9A-F]{4}"}
        '        | xsd:string {pattern="V1-[0-9A-F]{4}"}
        '        | xsd:string {pattern="V2-[0-9A-F]{4}"}
        '        | xsd:string {pattern="V3-[0-9A-F]{4}"}
        '        | xsd:string {pattern="V4-[0-9A-F]{4}"} }?

        'code-point-properties &= attribute kJHJ
        '   { text }?

        'code-point-properties &= attribute kJIS0213
        '   { xsd:string {pattern="[12],[0-9]{2},[0-9]{1,2}"} }?

        'code-point-properties &= attribute kJapaneseKun
        '   { list { xsd:string {pattern="[A-Z]+"}+ } }?

        'code-point-properties &= attribute kJapaneseOn
        '   { list { xsd:string {pattern="[A-Z]+"}+ } }?

        'code-point-properties &= attribute kJis0
        '   { xsd:string {pattern="[0-9]{4}"} }?

        'code-point-properties &= attribute kJis1
        '   { xsd:string {pattern="[0-9]{4}"} }?

        'code-point-properties &= attribute kKPS0
        '   { xsd:string {pattern="[0-9A-F]{4}"} }?

        'code-point-properties &= attribute kKPS1
        '   { xsd:string {pattern="[0-9A-F]{4}"} }?

        'code-point-properties &= attribute kKSC0
        '   { xsd:string {pattern="[0-9]{4}"} }?

        'code-point-properties &= attribute kKSC1
        '   { xsd:string {pattern="[0-9]{4}"} }?

        'code-point-properties &= attribute kKangXi
        '   { xsd:string {pattern="[0-9]{4}\.[0-9]{2}[01]"} }?

        'code-point-properties &= attribute kKarlgren
        '   { xsd:string {pattern="[1-9][0-9]{0,3}[A*]?"} }?

        'code-point-properties &= attribute kKorean
        '   { list { xsd:string {pattern="[A-Z]+"} +}}?

        'code-point-properties &= attribute kLau
        '   { list { xsd:string {pattern="[1-9][0-9]{0,3}"} +}}?

        'code-point-properties &= attribute kMainlandTelegraph
        '   { xsd:string {pattern="[0-9]{4}"} }?

        'code-point-properties &= attribute kMandarin
        '   { list { xsd:string {pattern="[A-ZÜ̈]+[1-5]"} +}}?

        'code-point-properties &= attribute kMatthews
        '   { xsd:string {pattern="[0-9]{1,4}(a|\.5)?"} }?

        'code-point-properties &= attribute kMeyerWempe
        '   { list { xsd:string {pattern="[1-9][0-9]{0,3}[a-t*]?"} +}}?

        'code-point-properties &= attribute kMorohashi
        '   { xsd:string {pattern="[0-9]{5}'?"} }?

        'code-point-properties &= attribute kNelson
        '   { list { xsd:string {pattern="[0-9]{4}"} +}}?

        'code-point-properties &= attribute kOtherNumeric
        '   { list { xsd:string {pattern="[0-9]+"} +}}?

        'code-point-properties &= attribute kPhonetic
        '   { list { xsd:string {pattern="[1-9][0-9]{0,3}[A-D]?\*?"} +}}?

        'code-point-properties &= attribute kPrimaryNumeric
        '   { xsd:string {pattern="[0-9]+"} }?

        'code-point-properties &= attribute kPseudoGB1
        '   { xsd:string {pattern="[0-9]{4}"} }?

        'code-point-properties &= attribute kRSAdobe_Japan1_6
        '   { list { xsd:string {pattern="[CV]\+[0-9]{1,5}\+[1-9][0-9]{0,2}\.[1-9][0-9]?\.[0-9]{1,2}"} +}}?

        'code-point-properties &= attribute kRSJapanese
        '   { xsd:string {pattern="[0-9]{1,3}\.[0-9]{1,2}"} }?

        'code-point-properties &= attribute kRSKanWa
        '   { xsd:string {pattern="[0-9]{1,3}\.[0-9]{1,2}"} }?

        'code-point-properties &= attribute kRSKangXi
        '   { xsd:string {pattern="[0-9]{1,3}\.[0-9]{1,2}"} }?

        'code-point-properties &= attribute kRSKorean
        '   { xsd:string {pattern="[0-9]{1,3}\.[0-9]{1,2}"} }?

        'code-point-properties &= attribute kRSMerged
        '   { text }?

        'code-point-properties &= attribute kRSUnicode
        '   { list { xsd:string {pattern="[0-9]{1,3}'?\.[0-9]{1,2}"} +}}?

        'code-point-properties &= attribute kSBGY
        '   { list { xsd:string {pattern="[0-9]{3}\.[0-9]{2}"} +}}?

        'code-point-properties &= attribute kSemanticVariant
        '   { list { xsd:string {pattern="U\+2?[0-9A-F]{4}(<k[A-Za-z:0-9]+(,k[A-Za-z0-9]+)*)?"} +}}?

        'code-point-properties &= attribute kSimplifiedVariant
        '   { list { xsd:string {pattern="U\+2?[0-9A-F]{4}"} +}}?

        'code-point-properties &= attribute kSpecializedSemanticVariant
        '   { list { xsd:string {pattern="U\+2?[0-9A-F]{4}(<k[A-Za-z0-9]+(,k[A-Za-z0-9]+)*)?"} +}}?

        'code-point-properties &= attribute kTaiwanTelegraph
        '   { xsd:string {pattern="[0-9]{4}"} }?

        'code-point-properties &= attribute kTang
        '   { list { xsd:string {pattern="\*?[A-Za-z\(\)æɑəɛ̀̌]+"} +}}?

        'code-point-properties &= attribute kTotalStrokes
        '   { xsd:string {pattern="[1-9][0-9]{0,2}"} }?

        'code-point-properties &= attribute kTraditionalVariant
        '   { list { xsd:string {pattern="U\+2?[0-9A-F]{4}"} +}}?

        'code-point-properties &= attribute kVietnamese
        '   { list { xsd:string {pattern="[A-Za-zà-ừ-̛̣̆̉ạ-ỹ]+"} +}}?

        'code-point-properties &= attribute kXHC1983
        '   { list { xsd:string {pattern="[0-9,.*]+:[a-zǜ́̄̈̌]+"} +}} ?

        'code-point-properties &= attribute kWubi
        '   { text }?

        'code-point-properties &= attribute kXerox
        '   { xsd:string {pattern="[0-9]{3}:[0-9]{3}"} }?

        'code-point-properties &= attribute kZVariant
        '   { xsd:string {pattern="U\+2?[0-9A-F]{4}((<k[A-Za-z0-9]+(:[TBZ]+)?(,k[A-Za-z0-9]+(:[TBZ]+)?)*)|(:k[A-Za-z]+))?"} }?

#End Region

        'TODO: Properties:
        'Name_Alias
        'Block
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
