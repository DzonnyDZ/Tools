Imports System.Runtime.CompilerServices
Imports System.Globalization
Imports System.Xml.Serialization

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
        <XmlEnum("L")>
        LeftToRight = UnicodeBidiCategoryStrenght.Strong Or 1
        ''' <summary>Strong, left-to-right embedding (LRE; LRE character)</summary>
        <XmlEnum("LRE")>
        LeftToRightEmbedding = UnicodeBidiCategoryStrenght.Strong Or 2
        ''' <summary>Strong, left-to-right override (LRO; LRO character)</summary>
        <XmlEnum("LRO")>
        LeftToRightOverride = UnicodeBidiCategoryStrenght.Strong Or 3
        ''' <summary>Strong, right-to-left character (R; right-to-left letters)</summary>
        <XmlEnum("R")>
        RightToLeft = UnicodeBidiCategoryStrenght.Strong Or 4
        ''' <summary>Strong, right-to-left Arabic (AL; Arabic and related scripts)</summary>
        <XmlEnum("AL")>
        RightToLeftArabic = UnicodeBidiCategoryStrenght.Strong Or 5
        ''' <summary>Strong, right-to-left embedding (RLE; RLE character)</summary>
        <XmlEnum("RLE")>
        RightToLeftEmbedding = UnicodeBidiCategoryStrenght.Strong Or 6
        ''' <summary>Strong, right-to.left override (RLO; RLO character)</summary>
        <XmlEnum("RLO")>
        RightToLeftOverride = UnicodeBidiCategoryStrenght.Strong Or 7

        ''' <summary>Weak, pop directional fromat (PDF; PDF character)</summary>
        <XmlEnum("PDF")>
        PopDirectionalFormat = UnicodeBidiCategoryStrenght.Weak Or 1
        ''' <summary>Weak, European numbers (EN; European digits and Eastern Arabic-Indic digits etc.)</summary>
        <XmlEnum("EN")>
        EuropeanNumber = UnicodeBidiCategoryStrenght.Weak Or 2
        ''' <summary>Weak, European number separators (ES; +, -)</summary>
        <XmlEnum("ES")>
        EuropeanNumberSeparator = UnicodeBidiCategoryStrenght.Weak Or 3
        ''' <summary>Weak, European number terminators (ET; ° and currency symbols)</summary>
        <XmlEnum("ET")>
        EuropeanNumberTerminator = UnicodeBidiCategoryStrenght.Weak Or 4
        ''' <summary>Weak, Arabic numbers (AN; Arabic-Indic digits)</summary>
        <XmlEnum("AN")>
        ArabicNumber = UnicodeBidiCategoryStrenght.Weak Or 5
        ''' <summary>Weak, common number separators (CS; :, ,, ., NBSP etc.)</summary>
        <XmlEnum("CS")>
        CommonNumberSeparator = UnicodeBidiCategoryStrenght.Weak Or 6
        ''' <summary>Weak, non spacing mark (NSM; characters of general category <see cref="UnicodeCategory.NonSpacingMark"/> and <see cref="UnicodeCategory.EnclosingMark"/>)</summary>
        <XmlEnum("NSM")>
        NonSpacingMark = UnicodeBidiCategoryStrenght.Weak Or 7
        ''' <summary>Weak, boundary neutral (BN; default ignorables)</summary>
        <XmlEnum("BN")>
        BoundaryNeutral = UnicodeBidiCategoryStrenght.Weak Or 8

        ''' <summary>Neutral, paragraph separator (B; paragraph separators)</summary>
        <XmlEnum("B")>
        ParagraphSeparator = UnicodeBidiCategoryStrenght.Neutral Or 1
        ''' <summary>Neutral, segment separator (S; TAB)</summary>
        <XmlEnum("S")>
        SegmentSeparator = UnicodeBidiCategoryStrenght.Neutral Or 2
        ''' <summary>Neutral, whitespace (WS; various spaces)</summary>
        <XmlEnum("WS")>
        Whitespace = UnicodeBidiCategoryStrenght.Neutral Or 3
        ''' <summary>Neutral, other neutrals (ON; other characters)</summary>
        <XmlEnum("ON")>
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

    ''' <summary>This enumeration describes well-known values of Unicode combining classes</summary>
    ''' <remarks>
    ''' Any number from 0 to 255 can be used as combining class. This enumeration contains only certain predefined values.
    ''' When used for canonical ordering alghoritm the only thing that matters is numerical value of the property.
    ''' <para>Name of values that have long symbolic aliases in PropertyValueAliases.txt of Unicode Character Database start with upper case letter, other start with lower case letter.</para>
    ''' </remarks>
    ''' <version version="1.5.4">This enumeration is in version 1.5.4</version>
    Public Enum UnicodeCombiningClass As Byte
        ''' <summary>Spacing and enclosing marks; also many vowel and consonant signs, even if nonspacing</summary>
        <XmlEnum("0")> NotReordered = 0
        ''' <summary>Marks which overlay a base letter or symbol</summary>
        <XmlEnum("1")> Overlay = 1
        ''' <summary>Diacritic nukta marks in Brahmi-derived scripts</summary>
        <XmlEnum("7")> Nukuta = 7
        ''' <summary>Hiragana/Katakana voicing marks</summary>
        <XmlEnum("8")> KanaVoicing = 8
        ''' <summary>Viramas</summary>
        <XmlEnum("9")> Virama = 9
        ''' <summary>Start of fixed position classes</summary>
        <XmlEnum("10")> fixedStart = 10
        ''' <summary>End of fixed position classes</summary>
        <XmlEnum("199")> fixedEnd = 199
        ''' <summary>Marks attached at the bottom left</summary>
        <XmlEnum("200")> AttachedBelowLeft = 200
        ''' <summary>Marks attached directly below</summary>
        <XmlEnum("202")> AttachedBelow = 202
        ''' <summary>Marks attached at the bottom right</summary>
        <XmlEnum("204")> attachedBottomRight = 204
        ''' <summary>Marks attached to the left</summary>
        <XmlEnum("208")> attachedLeft = 208
        ''' <summary>Marks attached to the right</summary>
        <XmlEnum("210")> attachedRight = 210
        ''' <summary>Marks attached at the top left</summary>
        <XmlEnum("212")> attachedTopLeft = 212
        ''' <summary>Marks attached directly above</summary>
        <XmlEnum("214")> AttachedAbove = 214
        ''' <summary>Marks attached at the top right</summary>
        <XmlEnum("216")> AttachedAboveRight = 216
        ''' <summary>Distinct marks at the bottom left</summary>
        <XmlEnum("218")> BelowLeft = 218
        ''' <summary>Distinct marks directly below</summary>
        <XmlEnum("220")> Below = 220
        ''' <summary>Distinct marks at the bottom right</summary>
        <XmlEnum("222")> BelowRight = 222
        ''' <summary>Distinct marks to the left</summary>
        <XmlEnum("224")> Left = 224
        ''' <summary>Distinct marks to the right</summary>
        <XmlEnum("226")> Right = 226
        ''' <summary>Distinct marks at the top left</summary>
        <XmlEnum("228")> AboveLeft = 228
        ''' <summary>Distinct marks directly above</summary>
        <XmlEnum("230")> Above = 230
        ''' <summary>Distinct marks at the top right</summary>
        <XmlEnum("232")> AboveRight = 232
        ''' <summary>Distinct marks subtending two bases</summary>
        <XmlEnum("233")> DoubleBelow = 233
        ''' <summary>Distinct marks extending above two bases</summary>
        <XmlEnum("234")> DoubleAbove = 234
        ''' <summary>Greek iota subscript only</summary>
        <XmlEnum("240")> IotaSubscript = 240
    End Enum

    ''' <summary>Unicode decomposition types of characters</summary>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    Public Enum UnicodeDecompositionType
        ''' <summary>Canonical mapping (can)</summary>
        <XmlEnum("can")> Canonical
        ''' <summary>Otherwise unspecified compatibility character (com, &lt;compat>)</summary>
        <XmlEnum("com")> Compatibility
        ''' <summary>Encircled form (enc, &lt;circle>)</summary>
        <XmlEnum("enc")> Circle
        ''' <summary>Final presentation form (Arabic; fin; &lt;final>)</summary>
        <XmlEnum("fin")> Final
        ''' <summary>Font variant (for example, a blackletter form; font; &lt;font>)</summary>
        <XmlEnum("font")> Font
        ''' <summary>Vulgar fraction form (fra; &lt;fraction>)</summary>
        <XmlEnum("fra")> Fraction
        ''' <summary>Initial presentation form (Arabic; init; &lt;initial>)</summary>
        <XmlEnum("init")> Initial
        ''' <summary>Isolated presentation form (Arabic; iso; &lt;isolated>)</summary>
        <XmlEnum("iso")> Isolated
        ''' <summary>Medial presentation form (Arabic; med; &lt;medial>)</summary>
        <XmlEnum("med")> Medial
        ''' <summary>Narrow (or hankaku) compatibility character (nar; &lt;narrow>)</summary>
        <XmlEnum("nar")> Narrow
        ''' <summary>No-break version of a space or hyphen (nb; &lt;noBreak>)</summary>
        <XmlEnum("nb")> NoBreak
        ''' <summary>Small variant form (CNS compatibility; sml; &lt;small>)</summary>
        <XmlEnum("sml")> Small
        ''' <summary>CJK squared font variant (sqr; &lt;square>)</summary>
        <XmlEnum("sqr")> Square
        ''' <summary>Subscript form (sub; &lt;sub>)</summary>
        <XmlEnum("sub")> [Sub]
        ''' <summary>Superscript form (sup; &lt;super>)</summary>
        <XmlEnum("sup")> Super
        ''' <summary>Vertical layout presentation form (vert; &lt;vertical>)</summary>
        <XmlEnum("vert")> Vertical
        ''' <summary>Wide (or zenkaku) compatibility character (wide; &lt;wide>)</summary>
        <XmlEnum("wide")> Wide
        ''' <summary>No decomposition (none)</summary>
        <XmlEnum("none")> none
    End Enum

End Namespace