Imports System.Xml.Serialization
Imports Tools.ComponentModelT

Namespace TextT.UnicodeT
    ''' <summary>When applied to a property of <see cref="UnicodePropertiesProvider"/> indicates Unihan database category that property belongs to</summary>
    ''' <remarks>You cannot define this attribute on your properties. Only properties of <see cref="UnicodePropertiesProvider"/> can be decorated with this attribute.</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public MustInherit Class UnihanPropertyCategoryAttribute
        Inherits UnicodePropertyCategoryAttribute

        ''' <summary>CTor - creates a new instance of the <see cref="UnicodePropertyCategoryAttribute"/> from <see cref="UnihanPropertyCategory"/> value</summary>
        ''' <param name="category">Indicates the category</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="category"/> is not one of supported <see cref="UnihanPropertyCategory"/> values.</exception>
        Friend Sub New(category As UnihanPropertyCategory)
            MyBase.New(UnicodePropertyCategory.Cjk, TextT.UnicodeT.UnicodeResources.propcat_unihan_UnihanPrefix & GetLocalizedString(category))
            _category = category
        End Sub

        Private _category As UnihanPropertyCategory
        ''' <summary>Gets enumerated value indicating the Unihan category the property belongs to</summary>
        Public ReadOnly Property UnihanCategory As UnihanPropertyCategory
            Get
                Return _category
            End Get
        End Property

        ''' <summary>Looks up the localized name of the specified category.</summary>
        ''' <returns>The localized name of the category, or null if a localized name does not exist.</returns>
        ''' <param name="value">The identifer for the category to look up. </param>
        Protected Overloads Overrides Function GetLocalizedString(value As String) As String
            Const unihanPrefix As String = "unihan - "

            Dim cat As UnihanPropertyCategory
            If [Enum].TryParse(If(value.ToLowerInvariant.StartsWith(unihanPrefix), value.Substring(unihanPrefix.Length), value), True, cat) Then Return GetLocalizedString(cat)

            Select Case value.ToLowerInvariant
                Case "irg sources", "unihan - irg sources" : cat = UnihanPropertyCategory.IrgSources
                Case "other mappings", "unihan - other mappings" : cat = UnihanPropertyCategory.OtherMappings
                Case "dictionary indices", "unihan - dictionary indices" : cat = UnihanPropertyCategory.DictionaryIndices
                Case "readings", "unihan - readings" : cat = UnihanPropertyCategory.Readings
                Case "dictionary-like data", "unihan - dictionary-like data" : cat = UnihanPropertyCategory.DictionaryLikeData
                Case "radical stroke count", "unihan - radical stroke count" : cat = UnihanPropertyCategory.RadicalStrokeCountS
                Case "variants", "unihan - variants" : cat = UnihanPropertyCategory.Variants
                Case "numeric values", "unihan - numeric values" : cat = UnihanPropertyCategory.NumericValues
                Case "unknown" : cat = UnihanPropertyCategory.unknown
                Case Else : Return MyBase.GetLocalizedString(value)
            End Select
        End Function

        ''' <summary>Converts <see cref="UnihanPropertyCategory"/> value to human-readable localized string</summary>
        ''' <param name="category">The category</param>
        ''' <returns>Human-readable localized string representing <paramref name="category"/>.</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="category"/> is not one of supported <see cref="UnihanPropertyCategory"/> values</exception>
        Public Overloads Shared Function GetLocalizedString(category As UnihanPropertyCategory) As String
            Select Case category
                Case UnihanPropertyCategory.IrgSources : Return UnicodeResources.propcat_unihan_IrgSources
                Case UnihanPropertyCategory.OtherMappings : Return UnicodeResources.propcat_unihan_OtherMappings
                Case UnihanPropertyCategory.DictionaryIndices : Return UnicodeResources.propcat_unihan_DictionaryIndices
                Case UnihanPropertyCategory.Readings : Return UnicodeResources.propcat_unihan_Readings
                Case UnihanPropertyCategory.DictionaryLikeData : Return UnicodeResources.propcat_unihan_DictionaryLikeData
                Case UnihanPropertyCategory.RadicalStrokeCounts : Return UnicodeResources.propcat_unihan_RadicalStrokeCount
                Case UnihanPropertyCategory.Variants : Return UnicodeResources.propcat_unihan_Variants
                Case UnihanPropertyCategory.NumericValues : Return UnicodeResources.propcat_unihan_NumericValues
                Case UnihanPropertyCategory.unknown : Return UnicodeResources.propcat_unihan_Unknown
                Case Else : Throw New InvalidEnumArgumentException("category", category, category.GetType)
            End Select
        End Function

        ''' <summary>Gets name of text file properties of given <see cref="UnihanPropertyCategory"/> are defined in</summary>
        ''' <param name="category">The Unihan property category</param>
        ''' <returns>Name of file (from Unihan.zip) that contains values for properties of given category</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="category"/> is not one of supported <see cref="UnihanPropertyCategory"/> values</exception>
        Public Shared Function GetFileName(category As UnihanPropertyCategory) As String
            Select Case category
                Case UnihanPropertyCategory.IrgSources : Return "Unihan_IRGSources.txt"
                Case UnihanPropertyCategory.OtherMappings : Return "Unihan_OtherMappings.txt"
                Case UnihanPropertyCategory.DictionaryIndices : Return "Unihan_DictionaryIndices.txt"
                Case UnihanPropertyCategory.Readings : Return "Unihan_Readings.txt"
                Case UnihanPropertyCategory.DictionaryLikeData : Return "Unihan_DictionaryLikeData.txt"
                Case UnihanPropertyCategory.RadicalStrokeCountS : Return "Unihan_RadicalStrokeCounts.txt"
                Case UnihanPropertyCategory.Variants : Return "Unihan_Variants.txt"
                Case UnihanPropertyCategory.NumericValues : Return "Unihan_NumericValues.txt"
                Case UnihanPropertyCategory.unknown : Return Nothing
                Case Else : Throw New InvalidEnumArgumentException("category", category, category.GetType)
            End Select
        End Function

    End Class

    ''' <summary>Implements <see cref="UnihanPropertyCategoryAttribute"/> that actually can be applied on properties.</summary>
    ''' <remarks>Only purpose of this class is to provide public constructors that are defined as internal in <see cref="UnihanPropertyCategoryAttribute"/>.</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Friend NotInheritable Class UcdCategoryUnihanAttribute
        Inherits UnihanPropertyCategoryAttribute
        ''' <summary>CTor - creates a new instance of the <see cref="UcdCategoryUnihanAttribute"/> from <see cref="UnihanPropertyCategory"/> value</summary>
        ''' <param name="category">Indicates the category</param>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="category"/> is not one of supported <see cref="UnihanPropertyCategory"/> values.</exception>
        Public Sub New(category As UnihanPropertyCategory)
            MyBase.New(category)
        End Sub
    End Class

    ''' <summary>Categories of Unihan database as defined in Unicode Han Database chapter 3</summary>
    ''' <remarks>If you want to connvert this enumeration's value to human-readable localized string you can use <see cref="UnihanPropertyCategoryAttribute.GetLocalizedString"/>.</remarks>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    Public Enum UnihanPropertyCategory
        ''' <summary>These represent the official mappings between Unihan and the various encoded character sets or collections which have been submitted by IRG members.</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_unihan_IrgSources")>
        IrgSources
        ''' <summary>Mapping tables between the ideographic portions of Unicode and those of encoded character sets or character collections not used by the IRG in its work.</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_unihan_OtherMappings")>
        OtherMappings
        ''' <summary>Indices to standard dictionaries</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_unihan_DictionaryIndices")>
        DictionaryIndices
        ''' <summary>Reading and pronounciation</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_unihan_Readings")>
        Readings
        ''' <summary>Various fields including information one might find in a dictionary</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_unihan_DictionaryLikeData")>
        DictionaryLikeData
        ''' <summary>Redical stroke counts</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_unihan_RadicalStrokeCount")>
        RadicalStrokeCounts
        ''' <summary>Glyph variants</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_unihan_Variants")>
        Variants
        ''' <summary>Numerical values an ideograph may have</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_unihan_NumericValues")>
        NumericValues
        ''' <summary>Unknown/undocumented</summary>
        <LDisplayName(GetType(UnicodeResources), "propcat_unihan_Unknown")>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        unknown
    End Enum
End Namespace