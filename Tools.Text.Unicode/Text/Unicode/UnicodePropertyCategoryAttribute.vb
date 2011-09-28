Imports System.Xml.Serialization
Imports Tools.ComponentModelT

Namespace TextT.UnicodeT
    ''' <summary>When applied to a property of <see cref="UnicodePropertiesProvider"/> indicates category that property belongs to</summary>
    ''' <remarks>You cannot define this attribute on your properties. Only properties of <see cref="UnicodePropertiesProvider"/> can be decorated with this attribute.</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <AttributeUsage(AttributeTargets.Property, allowmultiple:=False, inherited:=True)>
    Public MustInherit Class UnicodePropertyCategoryAttribute
        Inherits CategoryAttribute

        ''' <summary>CTor - creates a new instance of the <see cref="UnicodePropertyCategoryAttribute"/> from <see cref="UnicodePropertyCategory"/> value</summary>
        ''' <param name="category">Indicates the category</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="category"/> is not one of supported <see cref="UnicodePropertyCategory"/> values.</exception>
        Friend Sub New(category As UnicodePropertyCategory)
            MyBase.New(GetLocalizedString(category))
            _category = category
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="UnicodePropertyCategoryAttribute"/> class from <see cref="UnicodePropertyCategory"/> value and string category name</summary>
        ''' <param name="category">Indicates the category</param>
        ''' <param name="name">Alternative category name</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="category"/> is not one of supported <see cref="UnicodePropertyCategory"/> values.</exception>
        ''' <remarks>This property is intended to be used from derived classes which provide different set of categories than <see cref="UnicodePropertyCategory"/> (i.e. - subsets).</remarks>
        Friend Sub New(category As UnicodePropertyCategory, name As String)
            MyBase.New(name)
            If Not category.IsDefined Then Throw New InvalidEnumArgumentException("category", category, category.GetType)
            _category = category
        End Sub

        ''' <summary>Looks up the localized name of the specified category.</summary>
        ''' <returns>The localized name of the category, or null if a localized name does not exist.</returns>
        ''' <param name="value">The identifer for the category to look up. </param>
        Protected Overloads Overrides Function GetLocalizedString(value As String) As String
            Dim val As UnicodePropertyCategory
            If [Enum].TryParse(value, True, val) Then Return GetLocalizedString(val)
            Select Case value.ToLowerInvariant
                Case "shaping and rendering" : Return GetLocalizedString(UnicodePropertyCategory.ShapingAndRendering)
            End Select
            Return MyBase.GetLocalizedString(value)
        End Function

        Private _category As UnicodePropertyCategory
        ''' <summary>Gets enumerated value indicating the category the property belongs to</summary>
        Public ReadOnly Property UnicodeCategory As UnicodePropertyCategory
            Get
                Return _category
            End Get
        End Property

        ''' <summary>Converts <see cref="UnicodePropertyCategory"/> value to human-readable localized string</summary>
        ''' <param name="category">The category</param>
        ''' <returns>Human-readable localized string representing <paramref name="category"/>.</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="category"/> is not one of supported <see cref="UnicodePropertyCategory"/> values</exception>
        Public Overloads Shared Function GetLocalizedString(category As UnicodePropertyCategory) As String
            Select Case category
                Case UnicodePropertyCategory.General : Return UnicodeResources.propcat_General
                Case UnicodePropertyCategory.Case : Return UnicodeResources.propcat_Case
                Case UnicodePropertyCategory.Normalization : Return UnicodeResources.propcat_Normalization
                Case UnicodePropertyCategory.ShapingAndRendering : Return UnicodeResources.propcat_ShapingAndRendering
                Case UnicodePropertyCategory.Bidirectional : Return UnicodeResources.propcat_Bidirectional
                Case UnicodePropertyCategory.Identifiers : Return UnicodeResources.propcat_Identifiers
                Case UnicodePropertyCategory.Cjk : Return UnicodeResources.propcat_Cjk
                Case UnicodePropertyCategory.Miscellaneous : Return UnicodeResources.propcat_Miscellaneous
                Case UnicodePropertyCategory.Contributory : Return UnicodeResources.propcat_Contributory
                Case UnicodePropertyCategory.Numeric : Return UnicodeResources.propcat_Numeric
                Case Else : Throw New InvalidEnumArgumentException("category", category, category.GetType)
            End Select
        End Function
    End Class

    ''' <summary>Implements <see cref="UnicodePropertyCategoryAttribute"/> that actually can be applied on properties.</summary>
    ''' <remarks>Only purpose of this class is to provide public constructors that are defined as internal in <see cref="UnicodePropertyCategoryAttribute"/>.</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    <AttributeUsage(AttributeTargets.Property, allowmultiple:=False, inherited:=True)>
    Friend NotInheritable Class UcdCategoryAttribute
        Inherits UnicodePropertyCategoryAttribute
        ''' <summary>CTor - creates a new instance of the <see cref="UnicodePropertyCategoryAttribute"/></summary>
        ''' <param name="category">Indicate sthe category</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="category"/> is not one of supported <see cref="UnicodePropertyCategory"/> values.</exception>
        Public Sub New(category As UnicodePropertyCategory)
            MyBase.New(category)
        End Sub
    End Class

    ''' <summary>Categories of Unicode character properties as defined in Unicode Character Database chapter 5.1, Table 7</summary>
    ''' <remarks>If you want to connvert this enumeration's value to human-readable localized string you can use <see cref="UnicodePropertyCategoryAttribute.GetLocalizedString"/>.</remarks>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    Public Enum UnicodePropertyCategory
        ''' <summary>General-purpose properties</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_General")>
        General
        ''' <summary>Casing properties</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_Case")>
        [Case]
        ''' <summary>Normalization-related properties</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_Normalization")>
        Normalization
        ''' <summary>Sahping and Rendering</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_ShapingAndRendering")>
        ShapingAndRendering
        ''' <summary>Bidi-related properties</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_Bidirectional")>
        Bidirectional
        ''' <summary>Properties related to programming language identifiers</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_Identifiers")>
        Identifiers
        ''' <summary>CJK (China, Japan, Korea) properties (does not cover UniHan)</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_Cjk")>
        Cjk
        ''' <summary>Miscellaneous properties</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_Miscellaneous")>
        Miscellaneous
        ''' <summary>Contributory properties</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_Contributory")>
        Contributory
        ''' <summary>Numeric-related properties</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_Numeric")>
        Numeric
    End Enum
End Namespace
