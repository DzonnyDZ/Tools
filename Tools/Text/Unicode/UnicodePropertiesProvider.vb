Imports Tools.ComponentModelT
Imports Tools.ExtensionsT
Imports System.Xml.Linq
Imports System.Globalization.CultureInfo
Imports Tools.NumericsT
Imports System.Xml.Serialization

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
        <XmlEnum("na")>
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
        <XmlEnum("na1")>
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
        <XmlEnum("gc")>
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

#End Region
        'TODO:
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
