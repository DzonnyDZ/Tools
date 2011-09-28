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
                    Case Else : Throw New InvalidOperationException(UnicodeResources.ex_UnsuppportedGeneralCategory.f(value))
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
                    Throw New InvalidOperationException(UnicodeResources.ex_UnsupportedBidirectionalCategory.f(value), ex)
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
                Case Else : Throw New ArgumentException(UnicodeResources.ex_UnsupportedBidirectionalCategory.f(value), "value")
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

        ''' <summary>Unihan helper - gets value of XML attribute stored as space-separated Unicode characters</summary>
        ''' <param name="attributeName">Name of the XML attribute to parse value of</param>
        ''' <returns>Array of characters obtained from attribute value. An empty array if the attribute is either not present or empty.</returns>
        ''' <remarks>Use only for attributes that are guaranteed to have single characters separated by spaces and only if the characters are quaranteed no be UTF-16 character (i.e. not surrogate pairs).</remarks>
        ''' <exception cref="InvalidOperationException">An item in the array is either 0 characters long or more than one UTF-16 character long (this includes 2 or more subsequent regular characcters or surrogate pair(s))</exception>
        Protected Function GetCharArray(attributeName$) As Char()
            Dim value As String = GetPropertyValue(attributeName)
            If value.IsNullOrEmpty Then Return EmptyArray.Char
            Return (From str In value.Split(" "c) Select str.Single).ToArray
        End Function

        ''' <summary>Unihan helper - gets value of XML attribute stored as space-separated version values</summary>
        ''' <param name="attributeName">Name of the XML attribute to parse value of</param>
        ''' <returns>Array of <see cref="Version"/>s obtained from attribute value. An empty array if the attribute is either not present or empty.</returns>
        ''' <remarks>For supported version parsings see <see cref="Version.Parse"/>.</remarks>
        ''' <exception cref="ArgumentException">Input has fewer than two or more than four version components.</exception>
        ''' <exception cref="ArgumentOutOfRangeException">At least one component in input is less than zero.</exception>
        ''' <exception cref="FormatException">At least one component in input is not an integer.</exception>
        ''' <exception cref="OverflowException">At least one component in input represents a number that is greater than <see cref="System.Int32.MaxValue"/>.</exception>
        ''' <seelaso cref="Version.Parse"/>
        Protected Function GetVersionArray(attributeName$) As Version()
            Dim value As String = GetPropertyValue(attributeName)
            If value.IsNullOrEmpty Then Return EmptyArray(Of Version).value
            Return (From str In value.Split(" "c) Select Version.Parse(str)).ToArray
        End Function

        ''' <summary>Unihan helper - gets value of XML attribute stored as space-separated <see cref="RadicalStrokeCount"/> values</summary>
        ''' <param name="attributeName">Name of the XML attribute to parse value of</param>
        ''' <returns>Array of <see cref="RadicalStrokeCount"/> values obtained from attribute value. An empty array if the attribute is either not present or empty.</returns>
        ''' <remarks>For supported version parsings see <see cref="RadicalStrokeCount.Parse"/>.</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="value"/> is null</exception>
        ''' <exception cref="FormatException"><paramref name="value"/> does not contain 2 dot(.)-separated parts -or- AdditionalStrokes part of <paramref name="value"/> cannot be parsed as <see cref="Integer"/></exception>
        ''' <exception cref="ArgumentException">Radical part of <paramref name="value"/> does not represent a value that can be converted to <see cref="CjkRadical"/> value using the <see cref="LookupRadical"/> function.</exception>
        ''' <exception cref="ArgumentOutOfRangeException">AdditionalStrokes part of <paramref name="value"/> represents negative number</exception>
        ''' <exception cref="OverflowException">AdditionalStrokes part of <paramref name="value"/> is too big or too small for datatype <see cref="Integer"/>.</exception>
        ''' <seelaso cref="RadicalStrokeCount.Parse"/>
        Protected Function GetRadicalStrokeCountArray(attributeName$) As RadicalStrokeCount()
            Dim value As String = GetPropertyValue(attributeName)
            If value.IsNullOrEmpty Then Return EmptyArray(Of RadicalStrokeCount).value
            Return (From str In value.Split(" "c) Select RadicalStrokeCount.Parse(str)).ToArray
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
                Return GetDecimalArray("kCowles")
            End Get
        End Property

        ''' <summary>The position of this character in the Dae Jaweon (Korean) dictionary used in the four-dictionary sorting algorithm</summary>
        ''' <remarks>The position is in the form “page.position” with the final digit in the position being “0” for characters actually in the dictionary and “1” for characters not found in the dictionary and assigned a “virtual” position in the dictionary.</remarks>
        <XmlAttribute("kDaeJaweon")>
        <UcdProperty("kDaeJaweon", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), DisplayName("Unihan Dae Jaweon")>
        Public ReadOnly Property HanDaeJaweon As String()
            Get
                Return GetStringArray("kDaeJaweon")
            End Get
        End Property

        ''' <summary>An English definition for this character.</summary>
        ''' <remarks>
        ''' Definitions are for modern written Chinese and are usually (but not always) the same as the definition in other Chinese dialects or non-Chinese languages. In some cases, synonyms are indicated. Fuller variant information can be found using the various variant fields.
        ''' <para>Definitions specific to non-Chinese languages or Chinese dialects other than modern Mandarin are marked, e.g., (Cant.) or (J).</para>
        ''' <para>Major definitions are separated by semicolons, and minor definitions by commas. Any valid Unicode character (except for tab, double-quote, and any line break character) may be used within the definition field.</para></remarks>
        <XmlAttribute("kDefinition")>
        <UcdProperty("kDefinition", UnihanPropertyCategory.Readings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Readings), DisplayName("Unihan Definition")>
        Public ReadOnly Property HanDefinition As String
            Get
                Return GetPropertyValue("kDefinition")
            End Get
        End Property

        ''' <summary>The EACC mapping for this character.</summary>
        <XmlAttribute("kEACC")>
        <UcdProperty("kEACC", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan EACC")>
        Public ReadOnly Property HanEacc As Integer()
            Get
                Return GetHexArray("kEACC")
            End Get
        End Property

        ''' <summary>Data on the character from The Five Thousand Dictionary (aka Fenn’s Chinese-English Pocket Dictionary) by Courtenay H. Fenn, Cambridge, Mass.: Harvard University Press, 1979.</summary>
        ''' <remarks>
        ''' The data here consists of a decimal number followed by a letter A through K, the letter P, or an asterisk. The decimal number gives the Soothill number for the character’s phonetic, and the letter is a rough frequency indication, with A indicating the 500 most common ideographs, B the next five hundred, and so on.
        ''' <para>P is used by Fenn to indicate a rare character included in the dictionary only because it is the phonetic element in other characters.</para>
        ''' <para>An asterisk is used instead of a letter in the final position to indicate a character which belongs to one of Soothill’s phonetic groups but is not found in Fenn’s dictionary.</para>
        ''' <para>Characters which have a frequency letter but no Soothill phonetic group are assigned group 0.</para>
        ''' </remarks>
        <XmlAttribute("kFenn")>
        <UcdProperty("kFenn", UnihanPropertyCategory.DictionaryLikeData, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryLikeData), DisplayName("Unihan Fenn")>
        Public ReadOnly Property HanFenn As String()
            Get
                Return GetStringArray("kFenn")
            End Get
        End Property

        ''' <summary>The position of this character in <em>Fenn’s Chinese-English Pocket Dictionary</em> by Courtenay H. Fenn, Cambridge, Mass.: Harvard University Press, 1942.</summary>
        ''' <remarks>The position is indicated by a three-digit page number followed by a period and a two-digit position on the page.</remarks>
        <XmlAttribute("kFennIndex")>
        <UcdProperty("kFennIndex", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), DisplayName("Unihan Fenn Index")>
        Public ReadOnly Property HanFennIndex As Decimal()
            Get
                Return GetDecimalArray("kFennIndex")
            End Get
        End Property

        ''' <summary>The four-corner code(s) for the character. This data is derived from data provided in the public domain by Hartmut Bohn, Urs App, and Christian Wittern.</summary>
        ''' <remarks>
        ''' The four-corner system assigns each character a four-digit code from 0 through 9. The digit is derived from the “shape” of the four corners of the character (upper-left, upper-right, lower-left, lower-right). An optional fifth digit can be used to further distinguish characters; the fifth digit is derived from the shape in the character’s center or region immediately to the left of the fourth corner.
        ''' <para>The four-corner system is now used only rarely. Full descriptions are available online, e.g., at <a href="http://en.wikipedia.org/wiki/Four_corner_input">Wikipedia</a>.</para>
        ''' <para>Values in this field consist of four decimal digits, optionally followed by a period and fifth digit for a five-digit form.</para>
        ''' </remarks>
        <XmlAttribute("kFourCornerCode")>
        <UcdProperty("kFourCornerCode", UnihanPropertyCategory.DictionaryLikeData, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryLikeData), DisplayName("Unihan Four-corner Code")>
        Public ReadOnly Property HanFourCornerCode As Decimal()
            Get
                Return GetDecimalArray("kFourCornerCode")
            End Get
        End Property

        ''' <summary>A rough frequency measurement for the character based on analysis of traditional Chinese USENET postings</summary>
        ''' <remarks>Characters with a <see cref="HanFrequency"/> of 1 are the most common, those with a <see cref="HanFrequency"/> of 2 are less common, and so on, through a <see cref="HanFrequency"/> of 5.</remarks>
        <XmlAttribute("kFrequency")>
        <UcdProperty("kFrequency", UnihanPropertyCategory.DictionaryLikeData, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryLikeData), Description("Unihan Frequency")>
        Public ReadOnly Property HanFrequency As Integer()
            Get
                Return GetIntArray("kFrequency")
            End Get
        End Property

        ''' <summary>The GB 2312-80 mapping for this character in ku/ten form.</summary>
        <XmlAttribute("kGB0")>
        <UcdProperty("kGB0", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan GB 2312-80")>
        Public ReadOnly Property HanGB0 As Integer()
            Get
                Return GetIntArray("kGB0")
            End Get
        End Property

#Region "kGBx"
        ''' <summary>The GB 12345-90 mapping for this character in ku/ten form.</summary>
        <XmlAttribute("kGB1")>
        <UcdProperty("kGB1", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan GB 12345-90")>
        Public ReadOnly Property HanGB1 As Integer()
            Get
                Return GetIntArray("kGB1")
            End Get
        End Property

        ''' <summary>The GB 7589-87 mapping for this character in ku/ten form.</summary>
        <XmlAttribute("kGB3")>
        <UcdProperty("kGB3", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan GB 7589-87")>
        Public ReadOnly Property HanGB3 As Integer()
            Get
                Return GetIntArray("kGB3")
            End Get
        End Property

        ''' <summary>The GB 7590-87 mapping for this character in ku/ten form.</summary>
        <XmlAttribute("kGB5")>
        <UcdProperty("kGB5", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan GB 7590-87")>
        Public ReadOnly Property HanGB5 As Integer()
            Get
                Return GetIntArray("kGB5")
            End Get
        End Property

        ''' <summary>The GB 8565-89 mapping for this character in ku/ten form (GB7).</summary>
        <XmlAttribute("kGB7")>
        <UcdProperty("kGB7", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan GB 8565-89 (GB7)")>
        Public ReadOnly Property HanGB7 As Integer()
            Get
                Return GetIntArray("kGB7")
            End Get
        End Property

        ''' <summary>The GB 8565-89 mapping for this character in ku/ten form (GB8).</summary>
        <XmlAttribute("kGB8")>
        <UcdProperty("kGB8", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan GB 8565-89 (GB8)")>
        Public ReadOnly Property HanGB8 As Integer()
            Get
                Return GetIntArray("kGB8")
            End Get
        End Property
#End Region

        ''' <summary>The primary grade in the Hong Kong school system by which a student is expected to know the character</summary>
        ''' <remarks>This data is derived from 朗文初級中文詞典, Hong Kong: Longman, 2001.</remarks>
        <XmlAttribute("kGradeLevel")>
        <UcdProperty("kGradeLevel", UnihanPropertyCategory.DictionaryLikeData, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryLikeData), DisplayName("Unihan Grade Level")>
        Public ReadOnly Property HanGradeLevel As Integer()
            Get
                Return GetIntArray("kGradeLevel")
            End Get
        End Property

        ''' <summary>The position of this character in Bernhard Karlgren’s Grammata Serica Recensa (1957).</summary>
        ''' <remarks>This dataset contains a total of 7,405 records. References are given in the form DDDDa('), where “DDDD” is a set number in the range [0001..1260] zero-padded to 4-digits, “a” is a letter in the range [a..z] (excluding “w”), optionally followed by apostrophe ('). The data from which this mapping table is extracted contains a total of 10,023 references. References to inscriptional forms have been omitted.</remarks>
        <XmlAttribute("kGSR")>
        <UcdProperty("kGSR", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), DisplayName("Unihan Grammata Serica Recensa")>
        Public ReadOnly Property HanGsr As String()
            Get
                Return GetStringArray("kGSR")
            End Get
        End Property

        ''' <summary>The modern Korean pronunciation(s) for this character in Hangul.</summary>
        ''' <remarks>Strings returned are composed only from characters from range 0x1100 ÷ 0x11FF (ᄀ÷ᇿ)</remarks>
        <XmlAttribute("kHangul")>
        <UcdProperty("kHangul", UnihanPropertyCategory.Readings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Readings), DisplayName("Unihan Readings")>
        Public ReadOnly Property HanHangul As String()
            Get
                Return GetStringArray("kHangul")
            End Get
        End Property

        ''' <summary>The position of this character in the Hanyu Da Zidian (HDZ) Chinese character dictionary</summary>
        ''' <remarks>
        ''' The character references are given in the form “ABCDE.XYZ”, in which: “A” is the volume number [1..8]; “BCDE” is the zero-padded page number [0001..4809]; “XY” is the zero-padded number of the character on the page [01..32]; “Z” is “0” for a character actually in the dictionary, and greater than 0 for a character assigned a “virtual” position in the dictionary. For example, 53024.060 indicates an actual HDZ character, the 6th character on Page 3,024 of Volume 5 (i.e. 籉 [U+7C49]). Note that the Volume 8 “BCDE” references are in the range [0008..0044] inclusive, referring to the pagination of the “Appendix of Addendum” at the end of that volume (beginning after p. 5746).
        ''' <para>The first character assigned a given virtual position has an index ending in 1; the second assigned the same virtual position has an index ending in 2; and so on.</para>
        ''' </remarks>
        <XmlAttribute("kHanYu")>
        <UcdProperty("kHanYu", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), DisplayName("Unihan Hanyu Da Zidian")>
        Public ReadOnly Property HanHanYu As String()
            Get
                Return GetStringArray("kHanYu")
            End Get
        End Property

        ''' <summary>The Pronunciations and Frequencies of this character, based in part on those appearing in 《現代漢語頻率詞典》 &lt;Xiandai Hanyu Pinlu Cidian> (XDHYPLCD) [Modern Standard Beijing Chinese Frequency Dictionary]</summary>
        ''' <remarks>
        ''' <para>This dataset contains a total of 3799 records. (The original data provided to Unihan 2003/02/04 contained a total of 3800 records, including 〇 [U+3007] líng ‘IDEOGRAPHIC NUMBER ZERO’, not included in Unihan since it is not a CJK UNIFIED IDEOGRAPH.)</para>
        ''' <para>Each entry is comprised of two pieces of data.</para>
        ''' <para>The Hanyu Pinyin (HYPY) pronunciation(s) of the character, with numeric tone marks (1-5, where 5 indicates the “neutral tone”) immediately following each alphabetic string.</para>
        ''' <para>Immediately following the numeric tone mark, a numeric string appears in parentheses: e.g. in “a1(392)” the numeric string “392” indicates the sum total of the frequencies of the pronunciations of the character as given in HYPLCD.</para>
        ''' <para>Where more than one pronunciation exists, these are sorted by descending frequency, and the list elements are “space” delimited.</para>
        ''' </remarks>
        <XmlAttribute("kHanyuPinlu")>
        <UcdProperty("kHanyuPinlu", UnihanPropertyCategory.Readings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Readings), DisplayName("Inihan Xindai Hanyu Pinlu Cidian")>
        Public ReadOnly Property HanHanyuPinlu As String()
            Get
                Return GetStringArray("kHanyuPinlu")
            End Get
        End Property

        ''' <summary>The 漢語拼音 Hànyǔ Pīnyīn reading(s) appearing in the edition of 《漢語大字典》 Hànyǔ Dà Zìdiǎn (HDZ) specified in the “kHanYu” property description (q.v.).</summary>
        ''' <remarks>
        ''' Each location has the form “ABCDE.XYZ” (as in “kHanYu”); multiple locations for a given pīnyīn reading are separated by “,” (comma). The list of locations is followed by “:” (colon), followed by a comma-separated list of one or more pīnyīn readings. Where multiple pīnyīn readings are associated with a given mapping, these are ordered as in HDZ (for the most part reflecting relative commonality). The following are representative records.
        ''' <list type="list">
        ''' <item>| U+34CE | 㓎 | 10297.260: qīn,qìn,qǐn |</item>
        ''' <item>| U+34D8 | 㓘 | 10278.080,10278.090: sù |</item>
        ''' <item>| U+5364 | 卤 | 10093.130: xī,lǔ 74609.020: lǔ,xī |</item>
        ''' <item>| U+5EFE | 廾 | 10513.110,10514.010,10514.020: gǒng |</item>
        ''' </list>
        ''' For example, the “kHanyuPinyin” value for 卤 U+5364 is “10093.130: xī,lǔ 74609.020: lǔ,xī”. This means that 卤 U+5364 is found in “kHanYu” at entries 10093.130 and 74609.020. The former entry has the two pīnyīn readings xī and lǔ (in that order), whereas the latter entry has the readings lǔ and xī (reversing the order).
        ''' <para>This data was originally input by 井作恆 Jǐng Zuòhéng, proofed by 聃媽歌 Dān Māgē (Magda Danish, using software donated by 文林 Wénlín Institute, Inc. and tables prepared by 曲理查 Qū Lǐchá), and proofed again and prepared for the Unicode Consortium by 曲理查 Qū Lǐchá (2008-01-14).</para>
        ''' </remarks>
        <XmlAttribute("kHanyuPinyin")>
        <UcdProperty("kHanyuPinyin", UnihanPropertyCategory.Readings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Readings), DisplayName("Unihan Hànyǔ Dà Zìdiǎn")>
        Public ReadOnly Property HanHanyuPinyin As String()
            Get
                Return GetStringArray("kHanyuPinyin")
            End Get
        End Property

        ''' <summary>Indicates that 《漢語大字典》 Hanyu Da Zidian has a radical break beginning at this character’s position.</summary>
        ''' <remarks>The field consists of the radical (with its Unicode code point), a colon, and then the Hanyu Da Zidian position as in the kHanyu field.</remarks>
        <XmlAttribute("kHDZRadBreak")>
        <UcdProperty("kHDZRadBreak", UnihanPropertyCategory.DictionaryLikeData, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryLikeData), DisplayName("Unihan Hanyu Da Zidian Break")>
        Public ReadOnly Property HanHDZRadBreak As String
            Get
                Return GetPropertyValue("kHDZRadBreak")
            End Get
        End Property

        ''' <summary>The index of the character in 常用字字形表 (二零零零年修訂本),香港: 香港教育學院, 2000, ISBN 962-949-040-4.</summary>
        ''' <remarks>This publication gives the “proper” shapes for 4759 characters as used in the Hong Kong school system. The index is an integer, zero-padded to four digits.</remarks>
        <XmlAttribute("kHKGlyph")>
        <UcdProperty("kHKGlyph", UnihanPropertyCategory.DictionaryLikeData, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryLikeData), DisplayName("Unihan Hong Kong Glyph")>
        Public ReadOnly Property HanHKGlyph As Integer()
            Get
                Return GetIntArray("kHKGlyph")
            End Get
        End Property

        ''' <summary>Mappings to the Big Five extended code points used for the Hong Kong Supplementary Character Set.</summary>
        <XmlAttribute("kHKSCS")>
        <UcdProperty("kHKSCS", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan Hong Kong Supplementary Character Set")>
        Public ReadOnly Property HanHkscs As Integer()
            Get
                Return GetHexArray("kHKSCS")
            End Get
        End Property

        ''' <summary>The IBM Japanese mapping for this character in hexadecimal.</summary>
        ''' <remarks>Format of each string is <c>^F[ABC][0-9A-F]{2}$</c></remarks>
        <XmlAttribute("kIBMJapan")>
        <UcdProperty("kIBMJapan", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan IBM Japan")>
        Public ReadOnly Property HanIbmJapan As String()
            Get
                Return GetStringArray("kIBMJapan")
            End Get
        End Property

        ''' <summary>A boolean indicating that a character is in IICore, the IRG-produced minimal set of required ideographs for East Asian use. A character is in IICore if and only if it has a value for the kIICore field.</summary>
        ''' <remarks>The only value currently in this field is “2.1”, which is the identifier of the version of IICore used to populate this field.</remarks>
        <XmlAttribute("kIICore")>
        <UcdProperty("kIICore", UnihanPropertyCategory.IrgSources, UnicodePropertyStatus.Normative)>
        <UcdCategoryUnihan(UnihanPropertyCategory.IrgSources), DisplayName("Unihan IICore")>
        Public ReadOnly Property HanIICore As Version()
            Get
                Return GetVersionArray("kIICore")
            End Get
        End Property

        ''' <summary>The position of this character in the Dae Jaweon (Korean) dictionary used in the four-dictionary sorting algorithm.</summary>
        ''' <remarks>
        ''' The position is in the form “page.position” with the final digit in the position being “0” for characters actually in the dictionary and “1” for characters not found in the dictionary and assigned a “virtual” position in the dictionary.
        ''' <para>Thus, “1187.060” indicates the sixth character on page 1187. A character not in this dictionary but assigned a position between the 6th and 7th characters on page 1187 for sorting purposes would have the code “1187.061”</para>
        ''' <para>This field represents the official position of the character within the Dae Jaweon dictionary as used by the IRG in the four-dictionary sorting algorithm.</para>
        ''' </remarks>
        <XmlAttribute("kIRGDaeJaweon")>
        <UcdProperty("kIRGDaeJaweon", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), DisplayName("Unihan Dae Jaweon (IRG)")>
        Public ReadOnly Property HanIrgDaeJaweon As String()
            Get
                Return GetStringArray("kIRGDaeJaweon")
            End Get
        End Property

        ''' <summary>The index of this character in the Dai Kanwa Ziten, aka Morohashi dictionary (Japanese) used in the four-dictionary sorting algorithm.</summary>
        ''' <remarks>This field represents the official position of the character within the DaiKanwa dictionary as used by the IRG in the four-dictionary sorting algorithm.</remarks>
        <XmlAttribute("kIRGDaiKanwaZiten")>
        <UcdProperty("kIRGDaiKanwaZiten", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), Description("Unihan Dai Kanwa Ziten (IRG)")>
        Public ReadOnly Property HanIrgDaiKanwaZiten As String()
            Get
                Return GetStringArray("kIRGDaiKanwaZiten")
            End Get
        End Property

        ''' <summary>The position of this character in the Hanyu Da Zidian (PRC) dictionary used in the four-dictionary sorting algorithm.</summary>
        ''' <remarks>
        ''' The position is in the form “volume page.position” with the final digit in the position being “0” for characters actually in the dictionary and “1” for characters not found in the dictionary and assigned a “virtual” position in the dictionary.
        ''' <para>Thus, “32264.080” indicates the eighth character on page 2264 in volume 3. A character not in this dictionary but assigned a position between the 8th and 9th characters on this page for sorting purposes would have the code “32264.081”</para>
        ''' <para>This field represents the official position of the character within the Hanyu Da Zidian dictionary as used by the IRG in the four-dictionary sorting algorithm.</para>
        ''' </remarks>
        <XmlAttribute("kIRGHanyuDaZidian")>
        <UcdProperty("kIRGHanyuDaZidian", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), Description("Unihan Hanyu Da Zidian (IRG)")>
        Public ReadOnly Property HanIrgHanyuDaZidian As String()
            Get
                Return GetStringArray("kIRGHanyuDaZidian")
            End Get
        End Property

        ''' <summary>The official IRG position of this character in the 《康熙字典》 Kang Xi Dictionary used in the four-dictionary sorting algorithm.</summary>
        ''' <remarks>
        ''' The position is in the form “page.position” with the final digit in the position being “0” for characters actually in the dictionary and “1” for characters not found in the dictionary but assigned a “virtual” position in the dictionary.
        ''' <para>Thus, “1187.060” indicates the sixth character on page 1187. A character not in this dictionary but assigned a position between the 6th and 7th characters on page 1187 for sorting purposes would have the code “1187.061”.</para>
        ''' </remarks>
        <XmlAttribute("kIRGKangXi")>
        <UcdProperty("kIRGKangXi", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), Description("Unihan Kang Xi (IRG)")>
        Public ReadOnly Property HanIrgKangXi As String()
            Get
                Return GetStringArray("kIRGKangXi")
            End Get
        End Property

#Region "IRG Sources"
        ''' <summary>The IRG “G” (China + Singapore) source mapping for this character in hex.</summary>
        ''' <remarks>
        ''' The IRG G source consists of data from the following national standards, publications, and lists from the People’s Republic of China and Singapore. The versions of the standards used are those provided by the PRC to the IRG and may not always reflect published versions of the standards generally available.
        ''' <list>
        ''' <item>G0 GB2312-80</item>
        ''' <item>G1 GB12345-90 with 58 Hong Kong and 92 Korean “Idu” characters</item>
        ''' <item>G3 GB7589-87 unsimplified forms</item>
        ''' <item>G5 GB7590-87 unsimplified forms</item>
        ''' <item>G7 General Purpose Hanzi List for Modern Chinese Language, and General List of Simplified Hanzi</item>
        ''' <item>GS Singapore Characters</item>
        ''' <item>G8 GB8565-88</item>
        ''' <item>G9 GB18030-2000</item>
        ''' <item>GE GB16500-95</item>
        ''' <item>G4K Siku Quanshu (四庫全書)</item>
        ''' <item>GBK Chinese Encyclopedia (中國大百科全書)</item>
        ''' <item>GCH Ci Hai (辞海)</item>
        ''' <item>GCY Ci Yuan (辭源)</item>
        ''' <item>GCYY Chinese Academy of Surveying and Mapping Ideographs (中国测绘科学院用字) GFZ Founder Press System (方正排版系统)</item>
        ''' <item>GGH Gudai Hanyu Cidian (古代汉语词典)</item>
        ''' <item>GHC Hanyu Dacidian (漢語大詞典)</item>
        ''' <item>GHZ Hanyu Dazidian ideographs (漢語大字典)</item>
        ''' <item>GIDC ID system of the Ministry of Public Security of China, 2009</item>
        ''' <item>GJZ Commercial Press Ideographs (商务印书馆用字)</item>
        ''' <item>GKX Kangxi Dictionary ideographs(康熙字典)9th edition (1958) including the addendum (康熙字典)補遺</item>
        ''' <item>GXC Xiandai Hanyu Cidian (现代汉语词典)</item>
        ''' <item>GZFY Hanyu Fangyan Dacidian (汉语方言大辞典)</item>
        ''' <item>GZH ZhongHua ZiHai (中华字海)</item>
        ''' <item>GZJW Yinzhou Jinwen Jicheng Yinde (殷周金文集成引得)</item>
        ''' </list>
        ''' </remarks>
        <XmlAttribute("kIRG_GSource")>
        <UcdProperty("kIRG_GSource", UnihanPropertyCategory.IrgSources, UnicodePropertyStatus.Normative)>
        <UcdCategoryUnihan(UnihanPropertyCategory.IrgSources), DisplayName("Unihan IRG ""G"" Source")>
        Public ReadOnly Property HanIrgGSource As String '““G””
            Get
                Return GetPropertyValue("kIRG_GSource")
            End Get
        End Property

        ''' <summary>The IRG “H” (Hong Kong) source mapping for this character in hex.</summary>
        ''' <remarks>The IRG “H” source consists of data from the Hong Kong Supplementary Character Set – 2008.</remarks>
        <XmlAttribute("kIRG_HSource")>
        <UcdProperty("kIRG_HSource", UnihanPropertyCategory.IrgSources, UnicodePropertyStatus.Normative)>
        <UcdCategoryUnihan(UnihanPropertyCategory.IrgSources), DisplayName("Unihan IRG ""H"" Source")>
        Public ReadOnly Property HanIrgHSource As String '““H””
            Get
                Return GetPropertyValue("kIRG_HSource")
            End Get
        End Property

        ''' <summary>The IRG “J” (Japan) source mapping for this character in hex.</summary>
        ''' <remarks>
        ''' The IRG “J” source consists of data from the following national standards and lists from Japan.
        ''' <list type="list">
        ''' <item>J0 JIS X 0208-1990</item>
        ''' <item>J1 JIS X 0212-1990</item>
        ''' <item>J3 JIS X 0213:2000 level-3</item>
        ''' <item>J3A JIS X 0213:2004 level-3</item>
        ''' <item>J4 JIS X 0213:2000 level-4</item>
        ''' <item>JA Unified Japanese IT Vendors Contemporary Ideographs, 1993</item>
        ''' <item>JH Hanyo-Denshi Program (汎用電子情報交換環境整備プログラム), 2002-2009</item>
        ''' <item>JK Japanese KOKUJI Collection</item>
        ''' <item>JARIB Association of Radio Industries and Businesses (ARIB) ARIB STD-B24 Version 5.1, March 14 2007</item>
        ''' </list>
        ''' </remarks>
        <XmlAttribute("kIRG_JSource")>
        <UcdProperty("kIRG_JSource", UnihanPropertyCategory.IrgSources, UnicodePropertyStatus.Normative)>
        <UcdCategoryUnihan(UnihanPropertyCategory.IrgSources), DisplayName("Unihan IRG ""J"" Source")>
        Public ReadOnly Property HanIrgJSource As String '““J””
            Get
                Return GetPropertyValue("kIRG_JSource")
            End Get
        End Property

        ''' <summary>The IRG “KP” (North Korea) source mapping for this character in hex.</summary>
        ''' <remarks>
        ''' The IRG “KP” source consists of data from the following national standards and lists from the Democratic People’s Republic of Korea (North Korea).
        ''' <list type="list">
        ''' <item>KP0 KPS 9566-97</item>
        ''' <item>KP1 KPS 10721-2000</item>
        ''' </list>
        ''' </remarks>
        <XmlAttribute("kIRG_KPSource")>
        <UcdProperty("kIRG_KPSource", UnihanPropertyCategory.IrgSources, UnicodePropertyStatus.Normative)>
        <UcdCategoryUnihan(UnihanPropertyCategory.IrgSources), DisplayName("Unihan IRG ""KP"" Source")>
        Public ReadOnly Property HanIrgKPSource As String '““KP””
            Get
                Return GetPropertyValue("kIRG_KPSource")
            End Get
        End Property

        ''' <summary>The IRG “K” (Korea) source mapping for this character in hex.</summary>
        ''' <remarks>
        ''' The IRG “K” source consists of data from the following national standards and lists from the Republic of Korea (South Korea).
        ''' <list type="list">
        ''' <item>K0 KS X 1001:2004 (formerly KS C 5601-1987)</item>
        ''' <item>K1 KS X 1002:2001 (formerly KS C 5657-1991)</item>
        ''' <item>K2 PKS C 5700-1 1994</item>
        ''' <item>K3 PKS C 5700-2 1994</item>
        ''' <item>K4 PKS 5700-3:1998</item>
        ''' <item>K5 Korean IRG Hanja Character Set 5th Edition: 2001</item>
        ''' </list>
        ''' </remarks>
        <XmlAttribute("kIRG_KSource")>
        <UcdProperty("kIRG_KSource", UnihanPropertyCategory.IrgSources, UnicodePropertyStatus.Normative)>
        <UcdCategoryUnihan(UnihanPropertyCategory.IrgSources), DisplayName("Unihan IRG ""K"" Source")>
        Public ReadOnly Property HanIrgKSource As String '““K””
            Get
                Return GetPropertyValue("kIRG_KSource")
            End Get
        End Property

        ''' <summary>The IRG “M” (Macao) source mapping for this character.</summary>
        ''' <remarks>The IRG “M” source consists of data from the Macao Information System Character Set (澳門資訊系統字集).</remarks>
        <XmlAttribute("kIRG_MSource")>
        <UcdProperty("kIRG_MSource", UnihanPropertyCategory.IrgSources, UnicodePropertyStatus.Normative)>
        <UcdCategoryUnihan(UnihanPropertyCategory.IrgSources), DisplayName("Unihan IRG ""M"" Source")>
        Public ReadOnly Property HanIrgMSource As String '““M””
            Get
                Return GetPropertyValue("kIRG_MSource")
            End Get
        End Property

        ''' <summary>The IRG “T” (Taiwan) source mapping for this character in hex.</summary>
        ''' <remarks>
        ''' The IRG “T” source consists of data from the following national standards and lists from the Republic of China (Taiwan).
        ''' <list type="list">
        ''' <item>T1 TCA-CNS 11643-1992 1st plane</item>
        ''' <item>T2 TCA-CNS 11643-1992 2nd plane</item>
        ''' <item>T3 TCA-CNS 11643-1992 3rd plane with some additional characters</item>
        ''' <item>T4 TCA-CNS 11643-1992 4th plane</item>
        ''' <item>T5 TCA-CNS 11643-1992 5th plane</item>
        ''' <item>T6 TCA-CNS 11643-1992 6th plane</item>
        ''' <item>T7 TCA-CNS 11643-1992 7th plane</item>
        ''' <item>TB TCA-CNS Ministry of Education, Hakka dialect, May 2007</item>
        ''' <item>TC TCA-CNS 11643-1992 12th plane</item>
        ''' <item>TD TCA-CNS 11643-1992 13th plane</item>
        ''' <item>TE TCA-CNS 11643-1992 14th plane</item>
        ''' <item>TF TCA-CNS 11643-1992 15th plane</item>
        ''' </list>
        ''' </remarks>
        <XmlAttribute("kIRG_TSource")>
        <UcdProperty("kIRG_TSource", UnihanPropertyCategory.IrgSources, UnicodePropertyStatus.Normative)>
        <UcdCategoryUnihan(UnihanPropertyCategory.IrgSources), DisplayName("Unihan IRG ""T"" Source")>
        Public ReadOnly Property HanIrgTSource As String '““T””
            Get
                Return GetPropertyValue("kIRG_TSource")
            End Get
        End Property

        ''' <summary>The IRG “U” (Unicode) source mapping for this character.</summary>
        ''' <remarks>U-source references are a reference into the U-source ideograph database; see UTR #45. These consist of “UTC” followed by a five-digit, zero-padded index into the database.</remarks>
        <XmlAttribute("kIRG_USource")>
        <UcdProperty("kIRG_USource", UnihanPropertyCategory.IrgSources, UnicodePropertyStatus.Normative)>
        <UcdCategoryUnihan(UnihanPropertyCategory.IrgSources), DisplayName("Unihan IRG ""U"" Source")>
        Public ReadOnly Property HanIrgUSource As String '““U””
            Get
                Return GetPropertyValue("kIRG_USource")
            End Get
        End Property

        ''' <summary>The IRG “V” (Vietnam) source mapping for this character in hex.</summary>
        ''' <remarks>
        '''  The IRG “V” source consists of data from the following national standards and lists from Vietnam.
        ''' <list type="list">
        ''' <item>V0 TCVN 5773:1993</item>
        ''' <item>V1 TCVN 6056:1995</item>
        ''' <item>V2 VHN 01:1998</item>
        ''' <item>V3 VHN 02: 1998</item>
        ''' <item>V4 Dictionary on Nom 2006, Dictionary on Nom of Tay ethnic 2006, Lookup Table for Nom in the South 1994</item>
        ''' </list>
        ''' </remarks>
        <XmlAttribute("kIRG_VSource")>
        <UcdProperty("kIRG_VSource", UnihanPropertyCategory.IrgSources, UnicodePropertyStatus.Normative)>
        <UcdCategoryUnihan(UnihanPropertyCategory.IrgSources), DisplayName("Unihan IRG ""V"" Source")>
        Public ReadOnly Property HanIrgVSource As String '““V””
            Get
                Return GetPropertyValue("kIRG_VSource")
            End Get
        End Property
#End Region

        ''' <summary>Meaning of this property is not documented in Unicode Standard. It was dropped in Unicdoe 3.2</summary>
        ''' <remarks>This property is superseded by <see cref="HanHanYu"/>.</remarks>
        <XmlAttribute("kAlternateHanYu")>
        <UcdProperty("kAlternateHanYu", UnihanPropertyCategory.unknown, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.unknown), DisplayName("Unihan Alternate Han Yu")>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False), Obsolete("This property was dropped in Unicode 3.2. Its superseded by HanHanYu.")>
        Public ReadOnly Property HanAlternateHanYu As String
            Get
                Return GetPropertyValue("kAlternateHanYu")
            End Get
        End Property

        ''' <summary>Meaning of this property is not documented in Unicode Standard. It was dropped in Unicdoe 3.1</summary>
        <XmlAttribute("kAlternateJEF")>
        <UcdProperty("kAlternateJEF", UnihanPropertyCategory.unknown, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.unknown), DisplayName("Unihan Alternate JEF")>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False), Obsolete("This property was dropped in Unicode 3.1.")>
        Public ReadOnly Property HanAlternateJef As String
            Get
                Return GetPropertyValue("kAlternateJEF")
            End Get
        End Property

        ''' <summary>Meaning of this property is not documented in Unicode Standard. It was dropped in Unicdoe 4.1</summary>
        <XmlAttribute("kAlternateKangXi")>
        <UcdProperty("kAlternateKangXi", UnihanPropertyCategory.unknown, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.unknown), DisplayName("Unihan Alternate Kang Xi")>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False), Obsolete("This property was dropped in Unicode 4.1.")>
        Public ReadOnly Property HanAlternateKangXi As String
            Get
                Return GetPropertyValue("kAlternateKangXi")
            End Get
        End Property

        ''' <summary>Meaning of this property is not documented in Unicode Standard. It was dropped in Unicdoe 4.1</summary>
        <XmlAttribute("kAlternateMorohashi")>
        <UcdProperty("kAlternateMorohashi", UnihanPropertyCategory.unknown, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.unknown), DisplayName("Unihan Alternate Morohashi")>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False), Obsolete("This property was dropped in Unicode 4.1.")>
        Public ReadOnly Property HanAlternateMorohashi As String
            Get
                Return GetPropertyValue("kAlternateMorohashi")
            End Get
        End Property

        'Note: Unihan propertyies below were semi-automatically generated from Unihan generator.xlsx

        ''' <summary>The Japanese pronunciation(s) of this character.</summary>
        <XmlAttribute("kJapaneseKun")>
        <UcdProperty("kJapaneseKun", UnihanPropertyCategory.Readings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Readings), DisplayName("Unihan Japanese Kun")>
        Public ReadOnly Property HanJapaneseKun As String()
            Get
                Return GetStringArray("kJapaneseKun")
            End Get
        End Property

        ''' <summary>The Sino-Japanese pronunciation(s) of this character.</summary>
        <XmlAttribute("kJapaneseOn")>
        <UcdProperty("kJapaneseOn", UnihanPropertyCategory.Readings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Readings), DisplayName("Unihan Japanese On")>
        Public ReadOnly Property HanJapaneseOn As String()
            Get
                Return GetStringArray("kJapaneseOn")
            End Get
        End Property

        ''' <summary>The JIS X 0208-1990 mapping for this character in ku/ten form.</summary>
        <XmlAttribute("kJis0")>
        <UcdProperty("kJis0", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan JIS X 0208-1990")>
        Public ReadOnly Property HanJis0208 As Integer()
            Get
                Return GetIntArray("kJis0")
            End Get
        End Property

        ''' <summary>The JIS X 0213-2000 mapping for this character in min,ku,ten form.</summary>
        <XmlAttribute("kJIS0213")>
        <UcdProperty("kJIS0213", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan JIS X 0213-2000")>
        Public ReadOnly Property HanJis0213 As String()
            Get
                Return GetStringArray("kJIS0213")
            End Get
        End Property

        ''' <summary>The JIS X 0212-1990 mapping for this character in ku/ten form.</summary>
        <XmlAttribute("kJis1")>
        <UcdProperty("kJis1", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan JIS X 0212-1190")>
        Public ReadOnly Property HanJisX0212 As Integer()
            Get
                Return GetIntArray("kJis1")
            End Get
        End Property

        ''' <summary>The position of this character in the 《康熙字典》 Kang Xi Dictionary used in the four-dictionary sorting algorithm.</summary>
        ''' <remarks> The position is in the form “page.position” with the final digit in the position being “0” for characters actually in the dictionary and “1” for characters not found in the dictionary but assigned a “virtual” position in the dictionary.
        ''' <para>Thus, “1187.060” indicates the sixth character on page 1187. A character not in this dictionary but assigned a position between the 6th and 7th characters on page 1187 for sorting purposes would have the code “1187.061”.</para></remarks>
        <XmlAttribute("kKangXi")>
        <UcdProperty("kKangXi", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), DisplayName("Unihan Kang Xi")>
        Public ReadOnly Property HanKangXi As String()
            Get
                Return GetStringArray("kKangXi")
            End Get
        End Property

        ''' <summary>The index of this character in <em>Analytic Dictionary of Chinese and Sino-Japanese</em> by Bernhard Karlgren, New York: Dover Publications, Inc., 1974.</summary>
        ''' <remarks>
        ''' <para>If the index is followed by an asterisk (*), then the index is an interpolated one, indicating where the character would be found if it were to have been included in the dictionary. Note that while the index itself is usually an integer, there are some cases where it is an integer followed by an “A”.</para></remarks>
        <XmlAttribute("kKarlgren")>
        <UcdProperty("kKarlgren", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), DisplayName("Unihan Karlgren")>
        Public ReadOnly Property HanKarlgren As String()
            Get
                Return GetStringArray("kKarlgren")
            End Get
        End Property

        ''' <summary>The Korean pronunciation(s) of this character, using the Yale romanization system.</summary>
        ''' <remarks> (See <a href="http://www.coffeesigns.com/Resources/romanization/korean.asp">http://www.coffeesigns.com/Resources/romanization/korean.asp</a> for a comparison of the various Korean romanization systems.)</remarks>
        <XmlAttribute("kKorean")>
        <UcdProperty("kKorean", UnihanPropertyCategory.Readings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Readings), DisplayName("Unihan Korean")>
        Public ReadOnly Property HanKorean As String()
            Get
                Return GetStringArray("kKorean")
            End Get
        End Property

        ''' <summary>The KPS 9566-97 mapping for this character in hexadecimal form.</summary>
        <XmlAttribute("kKPS0")>
        <UcdProperty("kKPS0", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan KPS 9566-97")>
        Public ReadOnly Property HanKps9566 As Integer()
            Get
                Return GetHexArray("kKPS0")
            End Get
        End Property

        ''' <summary>The KPS 10721-2000 mapping for this character in hexadecimal form.</summary>
        <XmlAttribute("kKPS1")>
        <UcdProperty("kKPS1", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan KPS 10721-2000")>
        Public ReadOnly Property HanKps10721 As Integer()
            Get
                Return GetHexArray("kKPS1")
            End Get
        End Property

        ''' <summary>The KS X 1001:1992 (KS C 5601-1989) mapping for this character in ku/ten form.</summary>
        <XmlAttribute("kKSC0")>
        <UcdProperty("kKSC0", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan KS X 1001:1992")>
        Public ReadOnly Property HanKSX1001 As Integer()
            Get
                Return GetIntArray("kKSC0")
            End Get
        End Property

        ''' <summary>The KS X 1002:1991 (KS C 5657-1991) mapping for this character in ku/ten form.</summary>
        <XmlAttribute("kKSC1")>
        <UcdProperty("kKSC1", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan KS X 1002:1991")>
        Public ReadOnly Property HanKSX1002 As Integer()
            Get
                Return GetIntArray("kKSC1")
            End Get
        End Property

        ''' <summary>The index of this character in A Practical Cantonese-English Dictionary by Sidney Lau, Hong Kong: The Government Printer, 1977.</summary>
        ''' <remarks>
        ''' <para>The index consists of an integer. Missing indices indicate unencoded characters which are being submitted to the IRG for inclusion in future versions of the standard.</para></remarks>
        <XmlAttribute("kLau")>
        <UcdProperty("kLau", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), DisplayName("Unihan Lau")>
        Public ReadOnly Property HanLau As Integer()
            Get
                Return GetIntArray("kLau")
            End Get
        End Property

        ''' <summary>The PRC telegraph code for this character, </summary>
        ''' <remarks>derived from “Kanzi denpou koudo henkan-hyou” (“Chinese character telegraph code conversion table”), Lin Jinyi, KDD Engineering and Consulting, Tokyo, 1984.</remarks>
        <XmlAttribute("kMainlandTelegraph")>
        <UcdProperty("kMainlandTelegraph", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan Chinese Telegraph")>
        Public ReadOnly Property HanMainlandTelegraph As Integer()
            Get
                Return GetIntArray("kMainlandTelegraph")
            End Get
        End Property

        ''' <summary>The Mandarin pronunciation(s) for this character in pinyin; </summary>
        ''' <remarks>Mandarin pronunciations are sorted in order of frequency, not alphabetically.</remarks>
        <XmlAttribute("kMandarin")>
        <UcdProperty("kMandarin", UnihanPropertyCategory.Readings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Readings), DisplayName("Unihan Mandarin")>
        Public ReadOnly Property HanMandarin As String()
            Get
                Return GetStringArray("kMandarin")
            End Get
        End Property

        ''' <summary>The index of this character in Mathews’ Chinese-English Dictionary by Robert H. Mathews, Cambrige: Harvard University Press, 1975.</summary>
        ''' <remarks>
        ''' <para>Note that the field name is kMatthews instead of kMathews to maintain compatibility with earlier versions of this file, where it was inadvertently misspelled.</para></remarks>
        <XmlAttribute("kMatthews")>
        <UcdProperty("kMatthews", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), DisplayName("Unihan Matthews")>
        Public ReadOnly Property HanMatthews As String()
            Get
                Return GetStringArray("kMatthews")
            End Get
        End Property

        ''' <summary>The index of this character in the Student’s Cantonese-English Dictionary by Bernard F. Meyer and Theodore F. Wempe (3rd edition, 1947).</summary>
        ''' <remarks> The index is an integer, optionally followed by a lower-case Latin letter if the listing is in a subsidiary entry and not a main one. In some cases where the character is found in the radical-stroke index, but not in the main body of the dictionary, the integer is followed by an asterisk (e.g., U+50E5, which is listed as 736* as well as 1185a).</remarks>
        <XmlAttribute("kMeyerWempe")>
        <UcdProperty("kMeyerWempe", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), DisplayName("Unihan Meyer & Wempe")>
        Public ReadOnly Property HanMeyerWempe As String()
            Get
                Return GetStringArray("kMeyerWempe")
            End Get
        End Property

        ''' <summary>The index of this character in the Dae Kanwa Ziten, aka Morohashi dictionary (Japanese) used in the four-dictionary sorting algorithm.</summary>
        ''' <remarks>
        ''' <para>The edition used is the revised edition, published in Tokyo by Taishuukan Shoten, 1986.</para></remarks>
        <XmlAttribute("kMorohashi")>
        <UcdProperty("kMorohashi", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), DisplayName("Unihan Morohashi")>
        Public ReadOnly Property HanMorohashi As String()
            Get
                Return GetStringArray("kMorohashi")
            End Get
        End Property

        ''' <summary>The index of this character in The Modern Reader’s Japanese-English Character Dictionary by Andrew Nathaniel Nelson, Rutland, Vermont: Charles E. Tuttle Company, 1974.</summary>
        <XmlAttribute("kNelson")>
        <UcdProperty("kNelson", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), DisplayName("Unihan Nelson")>
        Public ReadOnly Property HanNelson As Integer()
            Get
                Return GetIntArray("kNelson")
            End Get
        End Property

        ''' <summary>The numeric value for the character in certain unusual, specialized contexts.</summary>
        ''' <remarks>
        ''' <para>The three numeric-value fields should have no overlap; that is, characters with a kOtherNumeric value should not have a kAccountingNumeric or kPrimaryNumeric value as well.</para></remarks>
        <XmlAttribute("kOtherNumeric")>
        <UcdProperty("kOtherNumeric", UnihanPropertyCategory.NumericValues, UnicodePropertyStatus.Informative)>
        <UcdCategoryUnihan(UnihanPropertyCategory.NumericValues), DisplayName("Unihan Other Numeric")>
        Public ReadOnly Property HanOtherNumeric As Integer()
            Get
                Return GetIntArray("kOtherNumeric")
            End Get
        End Property

        ''' <summary>The phonetic index for the character from <em>Ten Thousand Characters: An Analytic Dictionary</em>, by G. Hugh Casey, S.J. Hong Kong: Kelley and Walsh, 1980.</summary>
        <XmlAttribute("kPhonetic")>
        <UcdProperty("kPhonetic", UnihanPropertyCategory.DictionaryLikeData, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryLikeData), DisplayName("Unihan Phonetic")>
        Public ReadOnly Property HanPhonetic As String()
            Get
                Return GetStringArray("kPhonetic")
            End Get
        End Property

        ''' <summary>The value of the character when used in the writing of numbers in the standard fashion.</summary>
        ''' <remarks>
        ''' <para>The three numeric-value fields should have no overlap; that is, characters with a kPrimaryNumeric value should not have a kAccountingNumeric or kOtherNumeric value as well.</para></remarks>
        <XmlAttribute("kPrimaryNumeric")>
        <UcdProperty("kPrimaryNumeric", UnihanPropertyCategory.NumericValues, UnicodePropertyStatus.Informative)>
        <UcdCategoryUnihan(UnihanPropertyCategory.NumericValues), DisplayName("Unihan Primary Numeric")>
        Public ReadOnly Property HanPrimaryNumeric As Integer()
            Get
                Return GetIntArray("kPrimaryNumeric")
            End Get
        End Property

        ''' <summary>A “GB 12345-90” code point assigned to this character for the purposes of including it within Unihan.</summary>
        ''' <remarks> Pseudo-GB1 codes were used to provide official code points for characters not already in national standards, such as characters used to write Cantonese, and so on.</remarks>
        <XmlAttribute("kPseudoGB1")>
        <UcdProperty("kPseudoGB1", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan Pseudo GB 12345-90")>
        Public ReadOnly Property HanPseudoGB1 As Integer()
            Get
                Return GetIntArray("kPseudoGB1")
            End Get
        End Property

        ''' <summary>Information on the glyphs in Adobe-Japan1-6 as contributed by Adobe.</summary>
        ''' <remarks> The value consists of a number of space-separated entries. Each entry consists of three pieces of information separated by a plus sign:
        ''' <list type="number">
        ''' <item>C or V. “C” indicates that the Unicode code point maps directly to the Adobe-Japan1-6 CID that appears after it, and “V” indicates that it is considered a variant form, and thus not directly encoded.</item></remarks>
        <XmlAttribute("kRSAdobe_Japan1_6")>
        <UcdProperty("kRSAdobe_Japan1_6", UnihanPropertyCategory.RadicalStrokeCounts, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.RadicalStrokeCounts), DisplayName("Unihan Adobe-Japan1-6")>
        Public ReadOnly Property HanRSAdobeJapan As String()
            Get
                Return GetStringArray("kRSAdobe_Japan1_6")
            End Get
        End Property

        ''' <summary>A Japanese radical/stroke count for this character in the form “radical.additional strokes”.</summary>
        <XmlAttribute("kRSJapanese")>
        <UcdProperty("kRSJapanese", UnihanPropertyCategory.RadicalStrokeCounts, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.RadicalStrokeCounts), DisplayName("Unihan Radical/stroke Count (Japanese)")>
        Public ReadOnly Property HanRSJapanese As RadicalStrokeCount()
            Get
                Return GetRadicalStrokeCountArray("kRSJapanese")
            End Get
        End Property

        ''' <summary>The KangXi radical/stroke count for this character consistent with the value of the kKangXi field in the form “radical.additional strokes”.</summary>
        <XmlAttribute("kRSKangXi")>
        <UcdProperty("kRSKangXi", UnihanPropertyCategory.RadicalStrokeCounts, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.RadicalStrokeCounts), DisplayName("Unihan Radical/stroke Count (Kang Xi)")>
        Public ReadOnly Property HanRSKangXi As RadicalStrokeCount()
            Get
                Return GetRadicalStrokeCountArray("kRSKangXi")
            End Get
        End Property

        ''' <summary>A Morohashi radical/stroke count for this character in the form “radical.additional strokes”.</summary>
        <XmlAttribute("kRSKanWa")>
        <UcdProperty("kRSKanWa", UnihanPropertyCategory.RadicalStrokeCounts, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.RadicalStrokeCounts), DisplayName("Unihan Radical/stroke Count (Morohashi)")>
        Public ReadOnly Property HanRSKanWa As RadicalStrokeCount()
            Get
                Return GetRadicalStrokeCountArray("kRSKanWa")
            End Get
        End Property

        ''' <summary>A Korean radical/stroke count for this character in the form “radical.additional strokes”.</summary>
        <XmlAttribute("kRSKorean")>
        <UcdProperty("kRSKorean", UnihanPropertyCategory.RadicalStrokeCounts, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.RadicalStrokeCounts), DisplayName("Unihan Radical/stroke Count (Korean)")>
        Public ReadOnly Property HanRSKorean As RadicalStrokeCount()
            Get
                Return GetRadicalStrokeCountArray("kRSKorean")
            End Get
        End Property

        ''' <summary>A standard radical/stroke count for this character in the form “radical.additional strokes”.</summary>
        ''' <remarks> The radical is indicated by a number in the range (1..214) inclusive. An apostrophe (') after the radical indicates a simplified version of the given radical. The “additional strokes” value is the residual stroke-count, the count of all strokes remaining after eliminating all strokes associated with the radical.
        ''' <para>This field is also used for additional radical-stroke indices where either a character may be reasonably classified under more than one radical, or alternate stroke count algorithms may provide different stroke counts.</para>
        ''' <para>The first value is intended to reflect the same radical as the kRSKangXi field and the stroke count of the glyph used to print the character within the Unicode Standard.</para></remarks>
        <XmlAttribute("kRSUnicode")>
        <UcdProperty("kRSUnicode", UnihanPropertyCategory.RadicalStrokeCounts, UnicodePropertyStatus.Informative)>
        <UcdCategoryUnihan(UnihanPropertyCategory.RadicalStrokeCounts), DisplayName("Unihan Radical/stroke Count (Unicode)")>
        Public ReadOnly Property HanRSUnicode As RadicalStrokeCount()
            Get
                Return GetRadicalStrokeCountArray("kRSUnicode")
            End Get
        End Property

        ''' <summary>The position of this character in the Song Ben Guang Yun (SBGY) Medieval Chinese character dictionary</summary>
        ''' <remarks>
        ''' <para>The 25334 character references are given in the form “ABC.XY”, in which: “ABC” is the zero-padded page number [004..546]; “XY” is the zero-padded number of the character on the page [01..73]. For example, 364.38 indicates the 38th character on Page 364 (i.e. 澍). Where a given Unicode Scalar Value (USV) has more than one reference, these are space-delimited.</para></remarks>
        <XmlAttribute("kSBGY")>
        <UcdProperty("kSBGY", UnihanPropertyCategory.DictionaryIndices, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryIndices), DisplayName("Unihan Song Ben Guang Yun")>
        Public ReadOnly Property HanSongBenGuangYun As String()
            Get
                Return GetStringArray("kSBGY")
            End Get
        End Property

        ''' <summary>The Unicode value for a semantic variant for this character.</summary>
        ''' <remarks> A semantic variant is an x- or y-variant with similar or identical meaning which can generally be used in place of the indicated character.
        ''' <para>The basic syntax is a Unicode scalar value. It may optionally be followed by additional data. The additional data is separated from the Unicode scalar value by a less-than sign (&lt;), and may be subdivided itself into substrings by commas, each of which may be divided into two pieces by a colon. The additional data consists of a series of field tags for another field in the Unihan database indicating the source of the information. If subdivided, the final piece is a string consisting of the letters T (for tòng, U+540C 同) B (for bù, U+4E0D 不), or Z (for zhèng, U+6B63 正).</para>
        ''' <para>T is used if the indicated source explicitly indicates the two are the same (e.g., by saying that the one character is “the same as” the other).</para>
        ''' <para>B is used if the source explicitly indicates that the two are used improperly one for the other.</para>
        ''' <para>Z is used if the source explicitly indicates that the given character is the preferred form. Thus, kHanYu indicates that U+5231 刱 and U+5275 創 are semantic variants and that U+5275 創 is the preferred form.</para></remarks>
        <XmlAttribute("kSemanticVariant")>
        <UcdProperty("kSemanticVariant", UnihanPropertyCategory.Variants, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Variants), DisplayName("Unihan Semantic Variant")>
        Public ReadOnly Property HanSemanticVariant As String()
            Get
                Return GetStringArray("kSemanticVariant")
            End Get
        End Property

        ''' <summary>The Unicode value for the simplified Chinese variant for this character (if any).</summary>
        ''' <remarks>
        ''' <para>Note that a character can be both a traditional Chinese character in its own right and the simplified variant for other characters (e.g., U+53F0).</para>
        ''' <para>In such case, the character is listed as its own simplified variant and one of its own traditional variants. This distinguishes this from the case where the character is not the simplified form for any character (e.g., U+4E95).</para>
        ''' <para>Much of the of the data on simplified and traditional variants was supplied by Wenlin <a href="http://www.wenlin.com">http://www.wenlin.com</a></para></remarks>
        <XmlAttribute("kSimplifiedVariant")>
        <UcdProperty("kSimplifiedVariant", UnihanPropertyCategory.Variants, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Variants), DisplayName("Unihan Simplified Variant")>
        Public ReadOnly Property HanSimplifiedVariant As String()
            Get
                Return GetUnicodeArray("kSimplifiedVariant")
            End Get
        End Property

        ''' <summary>The Unicode value for a specialized semantic variant for this character.</summary>
        ''' <remarks> The syntax is the same as for the kSemanticVariant field.
        ''' <para>A specialized semantic variant is an x- or y-variant with similar or identical meaning only in certain contexts (such as accountants’ numerals).</para></remarks>
        <XmlAttribute("kSpecializedSemanticVariant")>
        <UcdProperty("kSpecializedSemanticVariant", UnihanPropertyCategory.Variants, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Variants), DisplayName("Unihan Specialized Semantic Variant")>
        Public ReadOnly Property HanSpecializedSemanticVariant As String()
            Get
                Return GetStringArray("kSpecializedSemanticVariant")
            End Get
        End Property

        ''' <summary>The Taiwanese telegraph code for this character, derived from “Kanzi denpou koudo henkan-hyou” (“Chinese character telegraph code conversion table”), Lin Jinyi, KDD Engineering and Consulting, Tokyo, 1984.</summary>
        <XmlAttribute("kTaiwanTelegraph")>
        <UcdProperty("kTaiwanTelegraph", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan Taiwan Telegraph")>
        Public ReadOnly Property HanTaiwanTelegraph As Integer()
            Get
                Return GetIntArray("kTaiwanTelegraph")
            End Get
        End Property

        ''' <summary>The Tang dynasty pronunciation(s) of this character, derived from or consistent with <em>T’ang Poetic Vocabulary</em> by Hugh M. Stimson, Far Eastern Publications, Yale Univ. 1976.</summary>
        ''' <remarks> An asterisk indicates that the word or morpheme represented in toto or in part by the given character with the given reading occurs more than four times in the seven hundred poems covered.</remarks>
        <XmlAttribute("kTang")>
        <UcdProperty("kTang", UnihanPropertyCategory.Readings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Readings), DisplayName("Unihan Tang")>
        Public ReadOnly Property HanTang As String()
            Get
                Return GetStringArray("kTang")
            End Get
        End Property

        ''' <summary>The total number of strokes in the character (including the radical).</summary>
        ''' <remarks> This value is for the character as drawn in the Unicode charts.</remarks>
        <XmlAttribute("kTotalStrokes")>
        <UcdProperty("kTotalStrokes", UnihanPropertyCategory.DictionaryLikeData, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.DictionaryLikeData), DisplayName("Unihan Total Strokes")>
        Public ReadOnly Property HanTotalStrokes As Integer()
            Get
                Return GetIntArray("kTotalStrokes")
            End Get
        End Property

        ''' <summary>The Unicode value(s) for the traditional Chinese variant(s) for this character.</summary>
        ''' <remarks>
        ''' <para>Note that a character can be both a traditional Chinese character in its own right and the simplified variant for other characters (e.g., 台 U+53F0).</para>
        ''' <para>In such case, the character is listed as its own simplified variant and one of its own traditional variants. This distinguishes this from the case where the character is not the simplified form for any character (e.g., 井 U+4E95).</para>
        ''' <para>Much of the of the data on simplified and traditional variants was graciously supplied by Wenlin Institute, Inc. <a href="http://www.wenlin.com">http://www.wenlin.com</a>.</para></remarks>
        <XmlAttribute("kTraditionalVariant")>
        <UcdProperty("kTraditionalVariant", UnihanPropertyCategory.Variants, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Variants), DisplayName("Unihan Traditional Variant")>
        Public ReadOnly Property HanTraditionalVariant As String()
            Get
                Return GetUnicodeArray("kTraditionalVariant")
            End Get
        End Property

        ''' <summary>The character’s pronunciation(s) in Quốc ngữ.</summary>
        <XmlAttribute("kVietnamese")>
        <UcdProperty("kVietnamese", UnihanPropertyCategory.Readings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Readings), DisplayName("Unihan Vietnamese")>
        Public ReadOnly Property HanVietnamese As String()
            Get
                Return GetStringArray("kVietnamese")
            End Get
        End Property

        ''' <summary>The Xerox code for this character.</summary>
        <XmlAttribute("kXerox")>
        <UcdProperty("kXerox", UnihanPropertyCategory.OtherMappings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.OtherMappings), DisplayName("Unihan Xerox")>
        Public ReadOnly Property HanXerox As String()
            Get
                Return GetStringArray("kXerox")
            End Get
        End Property

        ''' <summary>One or more Hànyǔ Pīnyīn readings as given in the Xiàndài Hànyǔ Cídiǎn (full bibliographic information below).</summary>
        ''' <remarks>
        ''' <para>Each pīnyīn reading is preceded by the character’s location(s) in the dictionary, separated from the reading by “:” (colon); multiple locations for a given reading are separated by “,” (comma); multiple “location: reading” values are separated by “ ” (space). Each location reference is of the form /[0-9]{4}\.[0-9]{3}\*?/ . The number preceding the period is the page number, zero-padded to four digits. The first two digits of the number following the period are the entry’s position on the page, zero-padded. The third digit is 0 for a main entry and greater than 0 for a parenthesized variant of the main entry. A trailing “*” (asterisk) on the location indicates an encoded variant substituted for an unencoded character (see below).</para></remarks>
        <XmlAttribute("kXHC1983")>
        <UcdProperty("kXHC1983", UnihanPropertyCategory.Readings, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Readings), DisplayName("Unihan Xiàndài Hànyǔ Cídiǎn")>
        Public ReadOnly Property HanXiàndàiHànyǔCídiǎn As String()
            Get
                Return GetStringArray("kXHC1983")
            End Get
        End Property

        ''' <summary>The Unicode value(s) for known z-variants of this character.</summary>
        ''' <remarks>
        ''' <para>The basic syntax is a Unicode scalar value. It may optionally be followed by additional data. The additional data is separated from the Unicode scalar value by a less-than sign (&lt;), and may be subdivided itself into substrings by commas. The additional data consists of a series of field tags for another field in the Unihan database indicating the source of the information.</para></remarks>
        <XmlAttribute("kZVariant")>
        <UcdProperty("kZVariant", UnihanPropertyCategory.Variants, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.Variants), DisplayName("Unihan Z-variant")>
        Public ReadOnly Property HanZVariant As String()
            Get
                Return GetStringArray("kZVariant")
            End Get
        End Property

        'End of generated properties

        ''' <summary>This property is undocumented in Unicode standard</summary>
        <XmlAttribute("kJHJ")>
        <UcdProperty("kJHJ", UnihanPropertyCategory.unknown, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.unknown), DisplayName("Unihan JHJ")>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)>
        Public ReadOnly Property HanJhj As String
            Get
                Return GetPropertyValue("kJHJ")
            End Get
        End Property


        ''' <summary>Meaning of this property is not documented in Unicode standard but it seems to be merge of some or all Radical/stroke Count properties. This property was dropped in Unicode 3.1</summary>
        <XmlAttribute("kRSMerged")>
        <UcdProperty("kRSMerged", UnihanPropertyCategory.unknown, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.RadicalStrokeCounts), DisplayName("Unihan Radical/stroke Count (Merged)")>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False), Obsolete("This property was dropped from Unicode 3.1")>
        Public ReadOnly Property HanRSMerged As String
            Get
                Return GetPropertyValue("kRSMerged")
            End Get
        End Property

        ''' <summary>This property is undocumented in Unicode standard</summary>
        <XmlAttribute("kWubi")>
        <UcdProperty("kWubi", UnihanPropertyCategory.unknown, UnicodePropertyStatus.Provisional)>
        <UcdCategoryUnihan(UnihanPropertyCategory.unknown), DisplayName("Unihan Wubi")>
        <EditorBrowsable(EditorBrowsableState.Advanced), Browsable(False)>
        Public ReadOnly Property HanWubi As String
            Get
                Return GetPropertyValue("kWubi")
            End Get
        End Property

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
