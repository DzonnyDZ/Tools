Imports System.Runtime.CompilerServices
Imports System.Globalization
Imports System.Xml.Serialization

Namespace TextT.UnicodeT

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

    ''' <summary>Defines Unicode character numeric type</summary>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    Public Enum UnicodeCharacterNumericType
        ''' <summary>Character has no numeric value assigned</summary>
        None
        ''' <summary>Character is a decimal digit (0-9) which can be used in  a decimal radix positional numeral system. The character is incoded in Unicode in a contiguous ascending range 0..9.</summary>
        [Decimal]
        ''' <summary>Character has an interal value 0-9. This cover digits that need special handling.</summary>
        Digit
        ''' <summary>Numeric value of character is positive or negative integer or rational number expressed as fraction.</summary>
        Numeric
    End Enum

    ''' <summary>Defines Unicode character joining types used in Arabic and other Middle-Eastern scripts</summary>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    Public Enum UnicodeJoiningType
        ''' <summary>Right-joining (R)</summary>
        Right
        ''' <summary>Left-joining (L)</summary>
        Left
        ''' <summary>Dual-joining (D)</summary>
        Dual
        ''' <summary>Join-causing (C)</summary>
        JoinCausing
        ''' <summary>Non-joining (U). This includes non-Arabic letters</summary>
        NonJoining
        ''' <summary>Transparent (T)</summary>
        Transparent
    End Enum

    ''' <summary>Defines Unicode joining groups for Arabic and other Middle-Eastern scripts</summary>
    ''' <remarks>Value Alef Maqsurah once exited in Unicode starndard (v2.x) but it does not appear in newer versions. It seems to be replaced with <see cref="UnicodeJoiningGroup.YehWithTail"/>. So Alef Maqsurah is not included in this implementation.</remarks>
    ''' <seelaso cref="UnicodeExtensions.Origin"/>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    Public Enum UnicodeJoiningGroup
        ''' <summary>No joining group</summary>
        none
#Region "Dual Arabic"
        ''' <summary>Dual joining Arabic group Beh (ب)</summary>
        Beh
        ''' <summary>Dual joining Arabic group Noon (ن)</summary>
        Noon
        ''' <summary>Dual joining Arabic group Nya (ڽ)</summary>
        Nya
        ''' <summary>Dual joining Arabic group Yeh (ي)</summary>
        Yeh
        ''' <summary>Dual joining Arabic group Farsi Yeh (ی)</summary>
        FarsiYeh
        ''' <summary>Dual joining Arabic group Burushaski Yeh Barree</summary>
        BurushaskiYehBarree
        ''' <summary>Dual joining Arabic group Hah (ح)</summary>
        Hah
        ''' <summary>Dual joining Arabic group Seen (س)</summary>
        Seen
        ''' <summary>Dual joining Arabic group Sad (ص)</summary>
        Sad
        ''' <summary>Dual joining Arabic group Tah (ط)</summary>
        Tah
        ''' <summary>Dual joining Arabic group Ain (ع)</summary>
        Ain
        ''' <summary>Dual joining Arabic group Feh (ف)</summary>
        Feh
        ''' <summary>Dual joining Arabic group Qaf (ق)</summary>
        Qaf
        ''' <summary>Dual joining Arabic group Meem (م)</summary>
        Meem
        ''' <summary>Dual joining Arabic group Heh (ه)</summary>
        Heh
        ''' <summary>Dual joining Arabic group knotted Heh</summary>
        KnottedHeh
        ''' <summary>Dual joining Arabic group Heh goal</summary>
        HehGoal
        ''' <summary>Dual joining Arabic group Kaf (ك)</summary>
        Kaf
        ''' <summary>Dual joining Arabic group Swash Kaf (ڪ)</summary>
        SwashKaf
        ''' <summary>Dual joining Arabic group Gaf (گ)</summary>
        Gaf
        ''' <summary>Dual joining Arabic group Lam (ل)</summary>
        Lam
#End Region
#Region "Right Arabic"
        ''' <summary>Right joining Arabic group Alef (ا)</summary>
        Alef
        ''' <summary>Right joining Arabic group Waw (و)</summary>
        Waw
        ''' <summary>Right joining Arabic group Dal (د)</summary>
        Dal
        ''' <summary>Right joining Arabic group Reh (ر)</summary>
        Reh
        ''' <summary>Right joining Arabic group Teh Marbuta (ة)</summary>
        TehMarbuta
        ''' <summary>Right joining Arabic group Teh Marbuta goal (ۃ)</summary>
        TehMarbutaGoal
        ''' <summary>This is legacy is alias of <see cref="TehMarbutaGoal"/>.</summary>
        ''' <remarks>This value property no longer apply on U+06C2 (Heh goal with Hamza above, &#x06C2;), it applies only to U+06C3 (Teh Marbuta goal, &#x06C3;), so new property alias <see cref="TehMarbutaGoal"/> was added to Unicode which makes more sense.</remarks>
        HamzaOnHehGoal = TehMarbutaGoal
        ''' <summary>Right joining Arabic group Yeh with tail (ۍ)</summary>
        YehWithTail
        ''' <summary>Right joining Arabic group Yeh Barree (ے)</summary>
        YehBarree
#End Region
#Region "Other Arabic"
#End Region
#Region "Dual Syriac"
        ''' <summary>Dual Joining Syriac group Beth (ܒ)</summary>
        Beth
        ''' <summary>Dual Joining Syriac group Persian Bheth (ܭ)</summary>
        PersianBheth
        ''' <summary>Dual Joining Syriac group Gamal (ܓ)</summary>
        Gamal
        ''' <summary>Dual Joining Syriac group Gamal Garshuni (ܔ)</summary>
        GamalGarshuni
        ''' <summary>Dual Joining Syriac group Persian Ghamal (ܮ)</summary>
        PersianGhamal
        ''' <summary>Dual Joining Syriac group Heth (ܚ)</summary>
        Heth
        ''' <summary>Dual Joining Syriac group Teth (ܛ)</summary>
        Teth
        ''' <summary>Dual Joining Syriac group Teth Garshuni (ܜ)</summary>
        TethGarshuni
        ''' <summary>Dual Joining Syriac group Yudh (ܝ)</summary>
        Yudh
        ''' <summary>Dual Joining Syriac group Kaph (ܟ)</summary>
        Kaph
        ''' <summary>Dual Joining Syriac group Sogdian Khaph (ݎ)</summary>
        SogdianKhaph
        ''' <summary>Dual Joining Syriac group Lamadh (ܠ)</summary>
        Lamadh
        ''' <summary>Dual Joining Syriac group Mim (ܡ)</summary>
        Mim
        ''' <summary>Dual Joining Syriac group Nun (ܢ)</summary>
        Nun
        ''' <summary>Dual Joining Syriac group Semkath (ܣ)</summary>
        Semkath
        ''' <summary>Dual Joining Syriac group Semkath final (ܤ)</summary>
        SemkathFinal
        ''' <summary>Dual Joining Syriac group E (ܥ)</summary>
        E
        ''' <summary>Dual Joining Syriac group Pe (ܦ)</summary>
        Pe
        ''' <summary>Dual Joining Syriac group reversed Pe (ܧ)</summary>
        ReversedPe
        ''' <summary>Dual Joining Syriac group Sogdian Fe (ݏ)</summary>
        SogdianFe
        ''' <summary>Dual Joining Syriac group Qaph (ܩ)</summary>
        Qaph
        ''' <summary>Dual Joining Syriac group Shin (ܫ)</summary>
        Shin
#End Region
#Region "Right Syraic"
        ''' <summary>Right Joining Syriac group Dalath (ܕ)</summary>
        Dalath
        ''' <summary>Right Joining Syriac group dotless Dalath Rish (ܖ)</summary>
        DotlessDalathRish
        ''' <summary>Right Joining Syriac group Persian Dhalath (ܯ)</summary>
        PersianDhalath
        ''' <summary>Right Joining Syriac group He (ܗ)</summary>
        He
        ''' <summary>Right Joining Syriac group Waw (ܘ)</summary>
        WawSyraic
        ''' <summary>Right Joining Syriac group Zain (ܙ)</summary>
        Zain
        ''' <summary>Right Joining Syriac group Sogdian Zhain (ݍ)</summary>
        SogdianZhain
        ''' <summary>Right Joining Syriac group Yudh He (ܞ)</summary>
        YudhHe
        ''' <summary>Right Joining Syriac group Sadhe (ܨ)</summary>
        Sadhe
        ''' <summary>Right Joining Syriac group Rish (ܪ)</summary>
        Rish
        ''' <summary>Right Joining Syriac group Taw (ܬ)</summary>
        Taw
#End Region
#Region "Other Syraic"
        ''' <summary>Syraic letter Alaph (U+0710, ܐ) is right-joining character but it's glyph is subject to additional contextual shaping</summary>
        Alaph
#End Region
    End Enum

    ''' <summary>Enumeration denotes origin of <see cref="UnicodeJoiningGroup"/> enumeration member</summary>
    ''' <remarks>In description of individual enum items in braces is number of table from Unicode chapter 8 where the groups are defined.</remarks>
    ''' <seelaso cref="UnicodeExtensions.Origin"/>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    Public Enum UnicodeJoiningGroupOrigin
        ''' <summary>This is not joining group</summary>
        none
        ''' <summary>Dual-Joining Arabic Characters (8-8)</summary>
        ArabicDual
        ''' <summary>Right-Joining Arabic Characters (8-9)</summary>
        ArabicRight
        ''' <summary>Dual-Joining Syriac Characters (8-13)</summary>
        SyriacDual
        ''' <summary>Right-Joining Syriac Characters (8-14)</summary>
        SyriacRight
        ''' <summary>Other Syriac charactres</summary>
        SyriacOther
    End Enum

    ''' <summary>Indicates how character behaves in relation with line breaking</summary>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    Public Enum UnicodeLineBreakType
#Region "Non-tailorable Line Breaking Classes"
        ''' <summary>Mandatory Break (BK) - Cause a line break (after)</summary>
        <XmlEnum("BK")> MandatoryBreak = 1
        ''' <summary>Carriage Return (CR) - Cause a line break (after), except between CR and LF</summary>
        <XmlEnum("CR")> CarriageReturn = 2
        ''' <summary>Line Feed (LF) - Cause a line break (after)</summary>
        <XmlEnum("LF")> LineFeed = 3
        ''' <summary>Combining Mark (CM) - Prohibit a line break between the character and the preceding character</summary>
        <XmlEnum("CM")> CombiningMark = 4
        ''' <summary>Next Line (NL) - Cause a line break (after)</summary>
        <XmlEnum("NL")> NextLine = 5
        ''' <summary>Surrogate (SG) - Do not occur in well-formed text</summary>
        <XmlElement("SG")> Surrogate = 6
        ''' <summary>Word Joiner (WJ) - Prohibit line breaks before and after</summary>
        <XmlEnum("WJ")> WordJoiner = 7
        ''' <summary>Zero Width Space (ZW) - Provide a break opportunity</summary>
        <XmlEnum("ZW")> ZeroWidthSpace = 8
        ''' <summary>Non-breaking ("Glue") - Prohibit line breaks before and after</summary>
        <XmlEnum("GL")> NonBreakingGlue = 9
        ''' <summary>Space (SP) - Enable indirect line breaks</summary>
        <XmlEnum("SP")> SPace = 10
#End Region
#Region "Break Opportunities"
        ''' <summary>Break Opportunity Before and After (B2) - Provide a line break opportunity before and after the character</summary>
        <XmlEnum("B2")> BreakOpportunityBeforeAndAfter = 11
        ''' <summary>Break After (BA) - Generally provide a line break opportunity after the character</summary>
        <XmlEnum("BA")> BreakAfter = 12
        ''' <summary>Break Before (BB) - Generally provide a line break opportunity before the character</summary>
        <XmlEnum("BB")> BreakBefore = 13
        ''' <summary>Hyphen (HY) - Provide a line break opportunity after the character, except in numeric context</summary>
        <XmlEnum("HY")> Hyphen = 14
        ''' <summary>Contingent Break Opportunity (CB) - Provide a line break opportunity contingent on additional information</summary>
        <XmlEnum("CB")> ContingentBreakOpportunity = 15
#End Region
#Region "Characters Prohibiting Certain Breaks"
        ''' <summary>Close Punctuation (CL) - Prohibits break before</summary>
        <XmlEnum("CL")> ClosePunctuation = 16
        ''' <summary>Close Parenthesis (CP) - Prohibits break before</summary>
        <XmlEnum("CP")> CloseParenthesis = 17
        ''' <summary>Exclamation/Interrogation (EX) - Prohibits break before</summary>
        <XmlElement("EX")> ExclamationInterrogation = 18
        ''' <summary>Inseparable (IN) - Allow only indirect line breaks netween pairs</summary>
        <XmlEnum("IN")> Inseparable = 19
        ''' <summary>Nonstarter (NS) - Allow only indirect line breaks before</summary>
        <XmlEnum("NS")> Nonstarter = 20
        ''' <summary>Open Punctuation (OP) - Prohibit line breaks after</summary>
        <XmlEnum("OP")> OpenPunctuation = 21
        ''' <summary>Quotation (QU) - Act like they are both opening and closing</summary>
        <XmlEnum("QU")> Quotation = 22
#End Region
#Region "Numeric Context"
        ''' <summary>Infix Numeric Separator (IS) - Prevent breaks after any and before numeric</summary>
        <XmlEnum("IS")> InfixNumericSeparator = 23
        ''' <summary>Numeric (NU) - Form numeric expressions for line breaking purposes</summary>
        <XmlEnum("NU")> Numeric = 24
        ''' <summary>Postfix Numeric (PO) - Do not break following a numeric expression</summary>
        <XmlEnum("PO")> PostfixNumeric = 25
        ''' <summary>Prefix Numeric (PR) - Do not break in front of a numeric expression</summary>
        <XmlEnum("PR")> PrefixNumeric = 26
        ''' <summary>Symbols Allowing Break After (SY) - Prevent a break before, and allow a break after</summary>
        <XmlEnum("SY")> SymbolsAllowingBreakAfter = 27
#End Region
#Region "Other Characters"
        ''' <summary>Ambiguous (Alphabetic or Ideographic) (AI) - Act like AL when the resolved EAW is N; otherwise, act as ID</summary>
        <XmlEnum("AI")> Ambiguous = 28
        ''' <summary>Alphabetic (AL) - Are alphabetic characters or symbols that are used with alphabetic characters</summary>
        <XmlEnum("AL")> Alphabetic = 29
        ''' <summary>Hangul LV Syllable (H2) - Form Korean syllable blocks</summary>
        <XmlEnum("H2")> HangulLvSyllable = 30
        ''' <summary>Hangul LVT Syllable (H3) - Form Korean syllable blocks</summary>
        <XmlEnum("H3")> HangulLvtSyllable = 31
        ''' <summary>Ideographic (ID) - Break before or after, except in some numeric context</summary>
        <XmlEnum("ID")> Ideographic = 32
        ''' <summary>Hangul L Jamo (JL) - Form Korean syllable blocks</summary>
        <XmlEnum("JL")> HangulLJamo = 33
        ''' <summary>Hangul V Jamo (JV) - Form Korean syllable blocks</summary>
        <XmlEnum("JV")> HangulVJamo = 34
        ''' <summary>Hangul T Jamo (JT) - Form Korean syllable blocks</summary>
        <XmlEnum("JT")> HangulTJamo = 35
        ''' <summary>Complex Context Dependent (South East Asian) (SA) - Provide a line break opportunity contingent on additional, language-specific context analysis</summary>
        <XmlEnum("SA")> ComplexContextDependent = 36
        ''' <summary>Unknown (XX) - Have as yet unknown line breaking behavior or unassigned code positions</summary>
        <XmlEnum("XX")> Unknown = 0
#End Region
    End Enum

    ''' <summary>Defines width of Eas Asian character</summary>
    ''' <version version="1.5.4">This enum is new in version 1.5.4</version>
    Public Enum UnicodeEastAsianWidth
        ''' <summary>Neutral (Not East Asian) (N) - All other characters</summary>
        <XmlEnum("N")> Neutral
        ''' <summary>East Asian Fullwidth (F) - Have compatibility decomposition type &lt;wide></summary>
        <XmlEnum("F")> Full
        ''' <summary>East Asian Halfwidth (H) - Explicitly defined as Halfwidth, have compatibility decomposition type &lt;narrow></summary>
        <XmlEnum("H")> Half
        ''' <summary>East Asian Wide (W) - all other characters that are always wide</summary>
        <XmlEnum("W")> Wide
        ''' <summary>East Asian Narrow (Na) - all other characters that are always narrow</summary>
        <XmlEnum("Na")> Narrow
        ''' <summary>East Asian Ambiguous (A) - can be sometimes wide and sometimes narrow (require additional information not contained in the character code to further resolve their width)</summary>
        <XmlEnum("A")> Ambiguous
    End Enum

    ''' <summary>Indicates Hangul syllable types</summary>
    ''' <version version="1.5.4">This enum is new in version 1.5.4</version>
    Public Enum UnicodeHangulSyllableType
        ''' <summary>This is not a Hangul syllable</summary>
        <XmlEnum("NA")> NA
        ''' <summary>Hangul syllable type L</summary>
        <XmlEnum("L")> L
        ''' <summary>Hangul syllable type V</summary>
        <XmlEnum("V")> V
        ''' <summary>Hangul syllable type LV</summary>
        <XmlEnum("LV")> Lv
        ''' <summary>Hangul syllable type LVT</summary>
        <XmlEnum("LVT")> Lvt
        ''' <summary>Hangul syllable type T</summary>
        <XmlEnum("T")> T
    End Enum

    ''' <summary>Values of <see cref="UnicodePropertiesProvider.GraphemeClusterBreak"/> property</summary>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    Public Enum UnicodeGraphemeClusterBreak
        ''' <summary>No indication</summary>
        <XmlEnum("XX")> none
        ''' <summary>Carriage Return</summary>
        <XmlEnum("CR")> Cr
        ''' <summary>Line Feed</summary>
        <XmlEnum("LF")> Lf
        ''' <summary>General categoties Zl, Zp, Cc, Cf excluding characters CR, LF, ZWNJ, ZWJ</summary>
        <XmlEnum("CN")> Control
        ''' <summary>General categories <see cref="System.Globalization.UnicodeCategory.NonSpacingMark"/> or <see cref="Globalization.UnicodeCategory.EnclosingMark"/>, characters <see cref="Chars.ZeroWidthNonJoiner"/> and <see cref="Chars.ZeroWidthJoiner"/> and a few spacing marks needed for canonical equivalence.</summary>
        <XmlEnum("EX")> Extend
        ''' <summary>Some characters which represents logical order exceptions</summary>
        <XmlEnum("PP")> Prepend
        ''' <summary>General category is specing mark plus few other characters</summary>
        <XmlEnum("SM")> SpacingMark
        ''' <summary><see cref="UnicodeHangulSyllableType.L"/></summary>
        <XmlEnum("L")> HangulL
        ''' <summary><see cref="UnicodeHangulSyllableType.V"/></summary>
        <XmlEnum("V")> HangulV
        ''' <summary><see cref="UnicodeHangulSyllableType.T"/></summary>
        <XmlEnum("T")> HangulT
        ''' <summary><see cref="UnicodeHangulSyllableType.Lv"/></summary>
        <XmlEnum("LV")> HangulLv
        ''' <summary><see cref="UnicodeHangulSyllableType.Lvt"/></summary>
        <XmlEnum("LVT")> HangulLvt
    End Enum

    Public Enum UnicodeWordBreakType
        Cr
        Lf
        NewLine
        Extend
        Format
        Katakana
        ALetter
        MidNumLet
        MidLetter
        MidNum
        Numeric
        ExtendNumSet

    End Enum

End Namespace