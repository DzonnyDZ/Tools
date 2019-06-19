Namespace TextT.UnicodeT
    ''' <summary>Lists individual Unicode Character Database (UCD) files</summary>
    ''' <remarks>This list is not supposed to be comprehensive. Non-Unihan files used by <see cref="UnicodePropertiesProvider"/> are listed primarilly.</remarks>
    ''' <version version="1.6.0">This module is new in version 1.6.0</version>
    Public Module UcdFiles
        ''' <summary>DerivedAge.txt</summary>
        Public Const DerivedAgeTxt As String = "DerivedAge.txt"
        ''' <summary>UnicodeData.txt</summary>
        Public Const UnicodeDataTxt As String = "UnicodeData.txt"
        ''' <summary>Block.txt</summary>
        Public Const BlockTxt As String = "Block.txt"
        ''' <summary>BidiMirroring.txt</summary>
        Public Const BidiMirroringTxt As String = "BidiMirroring.txt"
        ''' <summary>PropList.txt</summary>
        Public Const PropListTxt As String = "PropList.txt"
        ''' <summary>BidiBrackets.txt</summary>
        Public Const BidiBracketsTxt As String = "BidiBrackets.txt"
        ''' <summary>CompositionExclusions.txt</summary>
        Public Const CompositionExclusionsTxt As String = "CompositionExclusions.txt"
        ''' <summary>DerivedNormalizationProps.txt</summary>
        Public Const DerivedNormalizationPropsTxt As String = "DerivedNormalizationProps.txt"
        ''' <summary>ArabicShaping.txt</summary>
        Public Const ArabicShapingTxt As String = "ArabicShaping.txt"
        ''' <summary>LineBreak.txt</summary>
        Public Const LineBreakTxt As String = "LineBreak.txt"
        ''' <summary>EastAsianWidth.txt</summary>
        Public Const EastAsianWidtTxt As String = "EastAsianWidth.txt"
        ''' <summary>DerivedCoreProperties.txt</summary>
        Public Const DerivedCorePropertiesTxt As String = "DerivedCoreProperties.txt"
        ''' <summary>SpecialCasing.txt</summary>
        Public Const SpacialCasingTxt As String = "SpecialCasing.txt"
        ''' <summary>CaseFolding.txt</summary>
        Public Const CaseFoldingTxt As String = "CaseFolding.txt"
        ''' <summary>Scripts.txt</summary>
        Public Const ScriptsTxt As String = "Scripts.txt"
        ''' <summary>ScriptExtensions.txt</summary>
        Public Const ScriptExtensionsTxt As String = "ScriptExtensions.txt"
        ''' <summary>HangulSyllableType.txt</summary>
        Public Const HangulSyllableTypeTxt As String = "HangulSyllableType.txt"
        ''' <summary>Jamo.txt</summary>
        Public Const JamoTxt As String = "Jamo.txt"
        ''' <summary>IndicSyllabicCategory.txt</summary>
        Public Const IndicSyllabicCategoryTxt As String = "IndicSyllabicCategory.txt"
        ''' <summary>IndicMatraCategory.txt</summary>
        Public Const IndicMantraCategoryTxt As String = "IndicMatraCategory.txt"
        ''' <summary>IndicPositionalCategory.txt</summary>
        Public Const IndicPositionalCategoryTxt As String = "IndicPositionalCategory.txt"
        ''' <summary>GraphemeBreakProperty.txt</summary>
        Public Const GraphemeBreakPropertyTxt As String = "GraphemeBreakProperty.txt"
        ''' <summary>WordBreakProperty.txt</summary>
        Public Const WordBreakPropertyTxt As String = "WordBreakProperty.txt"
        ''' <summary>SentenceBreakProperty.txt</summary>
        Public Const SentenceBreakPropertyTxt As String = "SentenceBreakProperty.txt"
        ''' <summary>TangutSources.txt</summary>
        Public Const TangutSourcesTxt As String = "TangutSources.txt"
        ''' <summary>VerticalOrientation.txt</summary>
        Public Const VerticalOrientationTxt As String = "VerticalOrientation.txt"
        ''' <summary>EquivalentUnifiedIdeograph.txt</summary>
        Public Const EquivalentUnifiedIdeographTxt As String = "EquivalentUnifiedIdeograph.txt"

        ''' <summary>Lists all non-Unihan UCD files used by <see cref="UnicodePropertiesProvider"/></summary>
        Public ReadOnly All As String() = {
            DerivedAgeTxt,
            UnicodeDataTxt,
            BlockTxt,
            BidiMirroringTxt,
            PropListTxt,
            BidiBracketsTxt,
            CompositionExclusionsTxt,
            DerivedNormalizationPropsTxt,
            ArabicShapingTxt,
            LineBreakTxt,
            EastAsianWidtTxt,
            DerivedCorePropertiesTxt,
            SpacialCasingTxt,
            CaseFoldingTxt,
            ScriptsTxt,
            ScriptExtensionsTxt,
            HangulSyllableTypeTxt,
            JamoTxt,
            IndicSyllabicCategoryTxt,
            IndicMantraCategoryTxt,
            IndicPositionalCategoryTxt,
            GraphemeBreakPropertyTxt,
            WordBreakPropertyTxt,
            SentenceBreakPropertyTxt,
            TangutSourcesTxt,
            VerticalOrientationTxt,
            EquivalentUnifiedIdeographTxt
        }

    End Module
End Namespace