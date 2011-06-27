Imports System.Runtime.CompilerServices
Imports System.Globalization

Namespace TextT.UnicodeT
    ''' <summary>Contains Unicode-related extension methods</summary>
    ''' <version version="1.5.4">This module is new in version 1.5.4</version>
    Public Module UnicodeExtensions
        ''' <summary>Gets generalized Unicode category given general category belongs to</summary>
        ''' <param name="category">A Unicode general category to gete generalized category for</param>
        ''' <returns>A <see cref="UnicodeGeneralCategoryClass"/> indicating type of <paramref name="category"/>.</returns>
        ''' <exception cref="InvalidEnumArgumentException"><paramref name="category"/> is not one of <see cref="UnicodeCategory"/> values</exception>
        <Extension()>
        Public Function GetClass(category As UnicodeCategory) As UnicodeGeneralCategoryClass
            Select Case category
                Case UnicodeCategory.LowercaseLetter, UnicodeCategory.UppercaseLetter, UnicodeCategory.TitlecaseLetter, UnicodeCategory.ModifierLetter, UnicodeCategory.OtherLetter
                    Return UnicodeGeneralCategoryClass.Letter
                Case UnicodeCategory.NonSpacingMark, UnicodeCategory.SpacingCombiningMark, UnicodeCategory.EnclosingMark
                    Return UnicodeGeneralCategoryClass.Mark
                Case UnicodeCategory.DecimalDigitNumber, UnicodeCategory.LetterNumber, UnicodeCategory.OtherNumber
                    Return UnicodeGeneralCategoryClass.Number
                Case UnicodeCategory.ConnectorPunctuation, UnicodeCategory.DashPunctuation, UnicodeCategory.OpenPunctuation, UnicodeCategory.ClosePunctuation, UnicodeCategory.InitialQuotePunctuation, UnicodeCategory.FinalQuotePunctuation, UnicodeCategory.OtherPunctuation
                    Return UnicodeGeneralCategoryClass.Punctuation
                Case UnicodeCategory.MathSymbol, UnicodeCategory.CurrencySymbol, UnicodeCategory.ModifierSymbol, UnicodeCategory.OtherSymbol
                    Return UnicodeGeneralCategoryClass.Symbol
                Case UnicodeCategory.SpaceSeparator, UnicodeCategory.LineSeparator, UnicodeCategory.ParagraphSeparator
                    Return UnicodeGeneralCategoryClass.Separator
                Case UnicodeCategory.Control, UnicodeCategory.Format, UnicodeCategory.Surrogate, UnicodeCategory.PrivateUse, UnicodeCategory.OtherNotAssigned
                    Return UnicodeGeneralCategoryClass.Other
                Case Else
                    Throw New InvalidEnumArgumentException("category", category, category.GetType)
            End Select
        End Function

        ''' <summary>Gets strength of bidirectional class</summary>
        ''' <param name="bidClass">A bidirectional class to get strength of</param>
        ''' <returns>Strength of bidirectional class <paramref name="bidClass"/> as indicated by 2nd-least significant byte (2nd LSB) of that number.</returns>
        <Extension()>
        Public Function GetStrength(bidClass As UnicodeBidiCategory) As UnicodeBidiCategoryStrenght
            Return bidClass And (UnicodeBidiCategoryStrenght.Neutral Or UnicodeBidiCategoryStrenght.Strong Or UnicodeBidiCategoryStrenght.Weak)
        End Function
    End Module

    ''' <summary>Gets classes Unicode general categories can be divided to</summary>
    ''' <version version="1.5.4">This enum is new in version 1.5.4</version>
    Public Enum UnicodeGeneralCategoryClass
        ''' <summary>Letters category</summary>
        Letter
        ''' <summary>Marks category</summary>
        Mark
        ''' <summary>Numbers category</summary>
        Number
        ''' <summary>Punctuation category</summary>
        Punctuation
        ''' <summary>Symbols category</summary>
        Symbol
        ''' <summary>Separators category</summary>
        Separator
        ''' <summary>Other category</summary>
        Other
    End Enum

    ''' <summary>Defines Unicode bidirectional categories of cahacters</summary>
    ''' <remarks>To determine strength programatically match 2nd-least significant byte against <see cref="UnicodeBidiCategoryStrenght"/>.</remarks>
    ''' <version version="1.5.4">This class is new in version 1.5.4</version>
    Public Enum UnicodeBidiCategory
        ''' <summary>Strong, left-to-right character (L; left-to-right letters and numerals)</summary>
        LeftToRight = UnicodeBidiCategoryStrenght.Strong Or 1
        ''' <summary>Strong, left-to-right embedding (LRE; LRE character)</summary>
        LeftToRightEmbedding = UnicodeBidiCategoryStrenght.Strong Or 2
        ''' <summary>Strong, left-to-right override (LRO; LRO character)</summary>
        LeftToRightOverride = UnicodeBidiCategoryStrenght.Strong Or 3
        ''' <summary>Strong, right-to-left character (R; right-to-left letters)</summary>
        RightToLeft = UnicodeBidiCategoryStrenght.Strong Or 4
        ''' <summary>Strong, right-to-left Arabic (AL; Arabic and related scripts)</summary>
        RightToLeftArabic = UnicodeBidiCategoryStrenght.Strong Or 5
        ''' <summary>Strong, right-to-left embedding (RLE; RLE character)</summary>
        RightToLeftEmbedding = UnicodeBidiCategoryStrenght.Strong Or 6
        ''' <summary>Strong, right-to.left override (RLO; RLO character)</summary>
        RightToLeftOverride = UnicodeBidiCategoryStrenght.Strong Or 7

        ''' <summary>Weak, pop directional fromat (PDF; PDF character)</summary>
        PopDirectionalFormat = UnicodeBidiCategoryStrenght.Weak Or 1
        ''' <summary>Weak, European numbers (EN; European digits and Eastern Arabic-Indic digits etc.)</summary>
        EuropeanNumber = UnicodeBidiCategoryStrenght.Weak Or 2
        ''' <summary>Weak, European number separators (ES; +, -)</summary>
        EuropeanNumberSeparator = UnicodeBidiCategoryStrenght.Weak Or 3
        ''' <summary>Weak, European number terminators (ET; ° and currency symbols)</summary>
        EuropeanNumberTerminator = UnicodeBidiCategoryStrenght.Weak Or 4
        ''' <summary>Weak, Arabic numbers (AN; Arabic-Indic digits)</summary>
        ArabicNumber = UnicodeBidiCategoryStrenght.Weak Or 5
        ''' <summary>Weak, common number separators (CS; :, ,, ., NBSP etc.)</summary>
        CommonNumberSeparator = UnicodeBidiCategoryStrenght.Weak Or 6
        ''' <summary>Weak, non spacing mark (NSM; characters of general category <see cref="UnicodeCategory.NonSpacingMark"/> and <see cref="UnicodeCategory.EnclosingMark"/>)</summary>
        NonSpacingMark = UnicodeBidiCategoryStrenght.Weak Or 7
        ''' <summary>Weak, boundary neutral (BN; default ignorables)</summary>
        BoundaryNeutral = UnicodeBidiCategoryStrenght.Weak Or 8

        ''' <summary>Neutral, paragraph separator (B; paragraph separators)</summary>
        ParagraphSeparator = UnicodeBidiCategoryStrenght.Neutral Or 1
        ''' <summary>Neutral, segment separator (S; TAB)</summary>
        SegmentSeparator = UnicodeBidiCategoryStrenght.Neutral Or 2
        ''' <summary>Neutral, whitespace (WS; various spaces)</summary>
        Whitespace = UnicodeBidiCategoryStrenght.Neutral Or 3
        ''' <summary>Neutral, other neutrals (ON; other characters)</summary>
        OtherNeutrals = UnicodeBidiCategoryStrenght.Neutral Or 4
    End Enum

    ''' <summary>Indicates bidirectional strenght</summary>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    Public Enum UnicodeBidiCategoryStrenght
        ''' <summary>Strong bidirectional characters (has direction and affect neutrals)</summary>
        Strong = &H10
        ''' <summary>Weak bidirectional characters (has direction but doesn't affect neutrals)</summary>
        Weak = &H20
        ''' <summary>Neutral characters (directionality inherited from context - from strong characters)</summary>
        Neutral = &H0
    End Enum

End Namespace