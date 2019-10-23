Imports System.Xml.Serialization
Imports Tools.ComponentModelT

Namespace TextT.UnicodeT
    ''' <summary>An attribute applied ot a property of <see cref="UnicodePropertiesProvider"/> to indicate that the property is unicode property and provide UCD details about the property</summary>
    ''' <remarks>
    ''' You cannot define this attribute on your properties. Only properties of <see cref="UnicodePropertiesProvider"/> can be decorated with this attribute.
    ''' <para>This attribute is usually accompanied by few more attributtes that also provide information about the property.</para>
    ''' </remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <AttributeUsage(AttributeTargets.Property, allowmultiple:=False, inherited:=True)>
    Public MustInherit Class UnicodePropertyAttribute
        Inherits Attribute
        ''' <summary>CTor - creates a new instance of the <see cref="UnicodePropertyAttribute"/> class</summary>
        ''' <param name="name">Unlocalized name of the property as used in Unicode standard (UCD)</param>
        ''' <param name="source">Name of file from Unicode Character Database values of this property are defined in</param>
        ''' <param name="type">Type of the property</param>
        ''' <param name="status">Status of the property</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="type"/> is not one of <see cref="UnicodePropertyType"/> values -or- <paramref name="status"/> is not one of <see cref="UnicodePropertyStatus"/> values.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="name"/> is null</exception>
        Friend Sub New(name$, source$, type As UnicodePropertyType, status As UnicodePropertyStatus)
            If name Is Nothing Then Throw New ArgumentNullException("name")
            If Not type.IsDefined Then Throw New InvalidEnumArgumentException("type", type, type.GetType)
            If Not status.IsDefined Then Throw New InvalidEnumArgumentException("status", status, status.GetType)
            _source = source
            _type = type
            _status = status
            _name = name
        End Sub
        Private ReadOnly _source$
        Private ReadOnly _type As UnicodePropertyType
        Private ReadOnly _status As UnicodePropertyStatus
        Private ReadOnly _name$
        ''' <summary>Gets name of file from Unicode Character Database where values of this property are difined in</summary>
        ''' <remarks>For Unihan properties the file is packed in Unihan.zip</remarks>
        Public ReadOnly Property Source$
            Get
                Return _source
            End Get
        End Property
        ''' <summary>Gets type of the property</summary>
        Public ReadOnly Property Type() As UnicodePropertyType
            Get
                Return _type
            End Get
        End Property
        ''' <summary>Gets status of the property</summary>
        Public ReadOnly Property Status() As UnicodePropertyStatus
            Get
                Return _status
            End Get
        End Property
        ''' <summary>Gets unlocalized name of the property (as provided by Unicode Standard)</summary>
        ''' <remarks>For localized names look for <see cref="DisplayNameAttribute"/>.</remarks>
        Public ReadOnly Property Name$
            Get
                Return _name
            End Get
        End Property
    End Class

    ''' <summary>Implements <see cref="UnicodePropertyAttribute"/> that actually can be applied on properties.</summary>
    ''' <remarks>Only purpose of this class is to provide public constructors that are defined as internal in <see cref="UnicodePropertyAttribute"/>.</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)>
    Friend NotInheritable Class UcdPropertyAttribute
        Inherits UnicodePropertyAttribute
        ''' <summary>CTor - creates a new instance of the <see cref="UcdPropertyAttribute"/> class</summary>
        ''' <param name="name">Unlocalized name of the property as used in Unicode standard (UCD)</param>
        ''' <param name="source">Name of file from Unicode Character Database values of this property are defined in</param>
        ''' <param name="type">Type of the property</param>
        ''' <param name="status">Status of the property</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="type"/> is not one of <see cref="UnicodePropertyType"/> values -or- <paramref name="status"/> is not one of <see cref="UnicodePropertyStatus"/> values.</exception>
        ''' <exception cref="ArgumentNullException"><paramref name="name"/> is null</exception>
        Public Sub New(name$, source$, type As UnicodePropertyType, status As UnicodePropertyStatus)
            MyBase.New(name, source, type, status)
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="UcdPropertyAttribute"/> for Unihan property</summary>
        ''' <param name="name">Name of the property</param>
        ''' <param name="unihanCategory">Unihan property category the property belongs to (determines <see cref="UnicodePropertyAttribute.Source"/>)</param>
        ''' <param name="status">Status of the property</param>
        Public Sub New(name$, unihanCategory As UnihanPropertyCategory, status As UnicodePropertyStatus)
            MyBase.New(name, UnihanPropertyCategoryAttribute.GetFileName(unihanCategory), UnicodePropertyType.String, status)
        End Sub
    End Class

    ''' <summary>Unicode property types (indicates data type of the property)</summary>
    ''' <remarks>Values of constants from this enumeration are codes of character used to represent values in Unicode standard (it often reffers to property type only by it's initial letter).</remarks>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    Public Enum UnicodePropertyType
        ''' <summary>Catalog properties (C) have enumerated values which are expected to be regularly extended in successive versions of the Unicode Standard. This distinguishes them from Enumeration properties.</summary>
        <XmlEnum("C"), LDisplayName(GetType(UnicodeResources), "proptype_Catalog")>
        Catalog = AscW("C"c)
        ''' <summary>Enumeration properties (E) have enumerated values which constitute a logical partition space; new values will generally not be added to them in successive versions of the standard.</summary>
        <XmlEnum("E"), LDisplayName(GetType(UnicodeResources), "proptype_Enumeration")>
        Enumeration = AscW("E"c)
        ''' <summary>Binary properties (B) are a special case of Enumeration properties, which have exactly two values: Yes and No (or True and False).</summary>
        <XmlEnum("B"), LDisplayName(GetType(UnicodeResources), "proptype_Binary")>
        Binary = AscW("B"c)
        ''' <summary>String properties (S) are typically mappings from a Unicode code point to another Unicode code point or sequence of Unicode code points; examples include case mappings and decomposition mappings.</summary>
        <XmlEnum("S"), LDisplayName(GetType(UnicodeResources), "proptype_String")>
        [String] = AscW("S"c)
        ''' <summary>Numeric properties (N) specify the actual numeric values for digits and other characters associated with numbers in some way.</summary>
        <XmlEnum("N"), LDisplayName(GetType(UnicodeResources), "proptype_Numeric")>
        Numeric = AscW("N"c)
        ''' <summary>Miscellaneous properties (M) are those properties that do not fit neatly into the other property categories; they currently include character names, comments about characters, and the Unicode_Radical_Stroke property (a combination of numeric values) documented in Unicode Standard Annex #38, "Unicode Han Database (Unihan)"</summary>
        <XmlEnum("M"), LDisplayName(GetType(UnicodeResources), "proptype_Miscellaneous")>
        Miscellaneous = AscW("M"c)
    End Enum

    ''' <summary>Property statuses of Unicode Character Database (UCD) properties</summary>
    ''' <remarks>
    ''' <para>These property statuses apply on entire property for all codepoints not for single usege of property on single codepoint.</para>
    ''' <para>Values of constants from this enumeration are codes of character used to represent values in Unicode standard (it often reffers to property type only by it's initial letter).</para>
    ''' </remarks>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    Public Enum UnicodePropertyStatus
        ''' <summary>Normative property (N)</summary>
        <XmlEnum("N"), LDisplayName(GetType(UnicodeResources), "propstat_Normative")>
        Normative = AscW("N"c)
        ''' <summary>Informative property (I)</summary>
        <XmlEnum("I"), LDisplayName(GetType(UnicodeResources), "propstat_Informative")>
        Informative = AscW("I"c)
        ''' <summary>Contributory property (C)</summary>
        ''' <remarks>
        ''' Contributory properties contain sets of exceptions used in the generation of other properties derived from them. The contributory properties specifically concerned with identifiers and casing contribute to the maintenance of stability guarantees for properties and/or to invariance relationships between related properties. Other contributory properties are simply defined as a convenience for property derivation.
        ''' <para>Most contributory properties have names using the pattern "Other_XXX" and are used to derive the corresponding "XXX" property. For example, the Other_Alphabetic property is used in the derivation of the Alphabetic property.</para>
        ''' </remarks>
        <XmlEnum("C"), LDisplayName(GetType(UnicodeResources), "propstat_Contributory")>
        Contributory = AscW("C"c)
        ''' <summary>Provisional property (P)</summary>
        <XmlEnum("P"), LDisplayName(GetType(UnicodeResources), "propstat_Provisional")>
        Provisional = AscW("P"c)
    End Enum
End Namespace