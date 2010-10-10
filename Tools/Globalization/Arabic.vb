Imports System.Runtime.CompilerServices

#If Config <= Nightly Then
Namespace GlobalizationT
    ''' <summary>Provides functions related to Arabic letters - especially for conversion between various Arabic presentation forms and default letters</summary>
    ''' <remarks>
    ''' This module especially provides functions for detecting and conversion between various forms of Arabic letters.
    ''' 5 forms are recognized - default (context-unaware form) which should be used in normal text and four contextual forms - isolated, final, medial and initial.
    ''' Not all Arabic letters have all these forms - especially many letters have only isolated and final (and of course default) form.
    ''' Character (usually non-letters) which don't have any contextual form are not supported for contextual form conversion (<see cref="ArabicLetters.ToDefault"/> returns null and <see cref="ArabicLetters.IsDefaultForm"/> returns false).
    ''' Form conversion and detection supports all characters from Arabic Presentation Forms-B Unicode block are supported.
    ''' From Arabic Presentation Forms-A Unicode block only single letters are supported (no ligatures).
    ''' Support for ligatures form Arabic Presentation Forms-A Unicode block may be added in next version.
    ''' </remarks>
    ''' <version version="1.5.3" stage="Nightly">This module is new in version 1.5.3</version>
    Public Module ArabicLetters
        ''' <summary>First character in Arabic Unicode block (0x0600 - ARABIC NUMBER SIGN - ؀)</summary>
        Public Const ArabicBlockStart As Char = ChrW(&H600)
        ''' <summary>Last character in Arabic Unicode block (0x6FF - ARABIC LETTER HEH WITH INVERTED V - ۿ)</summary>
        Public Const ArabicBlockEnd As Char = ChrW(&H6FF)
        ''' <summary>First character in Arabic Supplement Unicode block (0x750 - ARABIC LETTER BEH WITH THREE DOTS HORIZONTALLY BELOW - ݐ)</summary>
        Public Const ArabicSupplementBlockStart As Char = ChrW(&H750)
        ''' <summary>Last character in Arabic Supplement Unicode Block (0x77F - ARABIC LETTER KAF WITH TWO DOTS ABOVE - ݿ)</summary>
        Public Const ArabicSupplementBlockEnd As Char = ChrW(&H77F)
        ''' <summary>First letter in Arabic Presentation Forms-A Unicode block (0xFB50 - ARABIC LETTER ALEF WASLA ISOLATED FORM - ﭐ)</summary>
        Public Const ArabicPresentationFormsABlockStart As Char = ChrW(&HFB50)
        ''' <summary>Last letter in Arabic Presentation Forms-A Unicode block (0xFDFF - this is reserved code point)</summary>
        Public Const ArabicPresentationFormsABlockEnd As Char = ChrW(&HFDFF)
        ''' <summary>First letter in Arabic Presentation Forms-B Unicode block (0xFE70 - ARABIC SUKUN MEDIAL FORM - ﹿ)</summary>
        Public Const ArabicPresentationFormsBBlockStart As Char = ChrW(&HFE70)
        ''' <summary>Last letter in Arabic Presentation Forms-B Unicode block (0xFEFF - ZERO WIDTH NO-BREAK SPACE (ZWNBSP) - this is special character)</summary>
        Public Const ArabicPresentationFormsBBlockEnd As Char = ChrW(&HFEFF)
        ''' <summary>Gets value indicating wheather given character falls into range of one of 4 Unicode Arabic blocks</summary>
        ''' <param name="ch">Character to test</param>
        ''' <returns>True if <paramref name="ch"/> falls to one of Unicode Arabic blocks; false otherwise.</returns>
        <Extension()>
        Public Function IsArabic(ByVal ch As Char) As Boolean
            Select Case ch
                Case ArabicBlockStart To ArabicBlockEnd, ArabicSupplementBlockStart To ArabicSupplementBlockEnd,
                    ArabicPresentationFormsABlockStart To ArabicPresentationFormsABlockEnd, ArabicPresentationFormsBBlockStart To ArabicPresentationFormsBBlockEnd
                    Return True
                Case Else : Return False
            End Select
        End Function

        ''' <summary>Dictionary of Arabic letters keyed by default form</summary>
        Private ReadOnly defaults As New Dictionary(Of String, ArabicLetter)
        ''' <summary>Dictionary of Arabic letters keyed by initial form</summary>
        Private ReadOnly initials As New Dictionary(Of Char, ArabicLetter)
        ''' <summary>Dictionary of Arabic letters keyed by medial form</summary>
        Private ReadOnly medials As New Dictionary(Of Char, ArabicLetter)
        ''' <summary>Dictionary of Arabic letters keyed by final form</summary>
        Private ReadOnly finals As New Dictionary(Of Char, ArabicLetter)
        ''' <summary>Dictionary of Arabic letters keyed by isolated form</summary>
        Private ReadOnly isolateds As New Dictionary(Of Char, ArabicLetter)
        ''' <summary>Dictionary of all arabic letters</summary>
        Private ReadOnly all As New Dictionary(Of String, ArabicLetter)

        ''' <summary>Type initializer - initializes the <see cref="ArabicLetters"/> module</summary>
        Sub New()
            Dim letters = {
                New ArabicLetter(&H622, &HFE81, &HFE82),
                New ArabicLetter(&H623, &HFE83, &HFE84),
                New ArabicLetter(&H624, &HFE85, &HFE86),
                New ArabicLetter(&H625, &HFE87, &HFE88),
                New ArabicLetter(&H626, &HFE89, &HFE8A, &HFE8B, &HFE8C),
                New ArabicLetter(&H627, &HFE8D, &HFE8E),
                New ArabicLetter(&H628, &HFE8F, &HFE90, &HFE91, &HFE92),
                New ArabicLetter(&H629, &HFE93, &HFE94),
                New ArabicLetter(&H62A, &HFE95, &HFE96, &HFE97, &HFE98),
                New ArabicLetter(&H62B, &HFE99, &HFE9A, &HFE9B, &HFE9C),
                New ArabicLetter(&H62C, &HFE9D, &HFE9E, &HFE9F, &HFEA0),
                New ArabicLetter(&H62D, &HFEA1, &HFEA2, &HFEA3, &HFEA4),
                New ArabicLetter(&H62E, &HFEA5, &HFEA6, &HFEA7, &HFEA8),
                New ArabicLetter(&H62F, &HFEA9, &HFEAA),
                New ArabicLetter(&H630, &HFEAB, &HFEAC),
                New ArabicLetter(&H631, &HFEAD, &HFEAE),
                New ArabicLetter(&H632, &HFEAF, &HFEB0),
                New ArabicLetter(&H633, &HFEB1, &HFEB2, &HFEB3, &HFEB4),
                New ArabicLetter(&H634, &HFEB5, &HFEB6, &HFEB7, &HFEB8),
                New ArabicLetter(&H635, &HFEB9, &HFEBA, &HFEBB, &HFEBC),
                New ArabicLetter(&H636, &HFEBD, &HFEBE, &HFEBF, &HFEC0),
                New ArabicLetter(&H637, &HFEC1, &HFEC2, &HFEC3, &HFEC4),
                New ArabicLetter(&H638, &HFEC5, &HFEC6, &HFEC7, &HFEC8),
                New ArabicLetter(&H639, &HFEC9, &HFECA, &HFECB, &HFECC),
                New ArabicLetter(&H63A, &HFECD, &HFECE, &HFECF, &HFED0),
                New ArabicLetter(&H641, &HFED1, &HFED2, &HFED3, &HFED4),
                New ArabicLetter(&H642, &HFED5, &HFED6, &HFED7, &HFED8),
                New ArabicLetter(&H643, &HFED9, &HFEDA, &HFEDB, &HFEDC),
                New ArabicLetter(&H644, &HFEDD, &HFEDE, &HFEDF, &HFEE0),
                New ArabicLetter(&H645, &HFEE1, &HFEE2, &HFEE3, &HFEE4),
                New ArabicLetter(&H646, &HFEE5, &HFEE6, &HFEE7, &HFEE8),
                New ArabicLetter(&H647, &HFEE9, &HFEEA, &HFEEB, &HFEEC),
                New ArabicLetter(&H648, &HFEED, &HFEEE, &HFEEF, &HFEF0),
                New ArabicLetter(&H649, &HFEEF, &HFEF0),
                New ArabicLetter(&H64A, &HFEF1, &HFEF2, &HFEF3, &HFEF4),
                New ArabicLetter("لآ", &HFEF5, &HFEF6),
                New ArabicLetter("لأ", &HFEF7, &HFEF8),
                New ArabicLetter("لإ", &HFEF9, &HFEFA),
                New ArabicLetter("لا", &HFEFB, &HFEFC),
 _
                New ArabicLetter(&H671, &HFB50, &HFB51),
                New ArabicLetter(&H67B, &HFB52, &HFB53, &HFB54, &HFB55),
                New ArabicLetter(&H67E, &HFB56, &HFB57, &HFB58, &HFB59),
                New ArabicLetter(&H680, &HFB5A, &HFB5B, &HFB5C, &HFB5D),
                New ArabicLetter(&H67A, &HFB5F, &HFB60, &HFB61, &HFB62),
                New ArabicLetter(&H67F, &HFB62, &HFB63, &HFB64, &HFB65),
                New ArabicLetter(&H679, &HFB66, &HFB67, &HFB68, &HFB69),
                New ArabicLetter(&H6A4, &HFB6A, &HFB6B, &HFB6C, &HFB6D),
                New ArabicLetter(&H6A6, &HFB6E, &HFB6F, &HFB70, &HFB71),
                New ArabicLetter(&H684, &HFB72, &HFB73, &HFB74, &HFB75),
                New ArabicLetter(&H683, &HFB77, &HFB78, &HFB79, &HFB7A),
                New ArabicLetter(&H686, &HFB7A, &HFB7B, &HFB7C, &HFB7D),
                New ArabicLetter(&H687, &HFB7E, &HFB7F, &HFB80, &HFB81),
                New ArabicLetter(&H68D, &HFB82, &HFB83),
                New ArabicLetter(&H68C, &HFB84, &HFB85),
                New ArabicLetter(&H68E, &HFB86, &HFB87),
                New ArabicLetter(&H688, &HFB88, &HFB89),
                New ArabicLetter(&H698, &HFB8A, &HFB8B),
                New ArabicLetter(&H691, &HFB8C, &HFB8D),
                New ArabicLetter(&H6A9, &HFB8E, &HFB8F, &HFB90, &HFB91),
                New ArabicLetter(&H6AF, &HFB92, &HFB93, &HFB94, &HFB95),
                New ArabicLetter(&H6B3, &HFB96, &HFB97, &HFB98, &HFB99),
                New ArabicLetter(&H6B1, &HFB9A, &HFB9B, &HFB9C, &HFB9D),
                New ArabicLetter(&H6BA, &HFB9E, &HFB9F),
                New ArabicLetter(&H6BB, &HFBA0, &HFBA1, &HFBA2, &HFBA3),
                New ArabicLetter(&H6C0, &HFBA4, &HFBA5),
                New ArabicLetter(&H6C1, &HFBA6, &HFBA7, &HFBA8, &HFBA9),
                New ArabicLetter(&H6BE, &HFBAA, &HFBAB, &HFBAC, &HFBAD),
                New ArabicLetter(&H6D2, &HFBAE, &HFBAF),
                New ArabicLetter(&H6D3, &HFBB0, &HFBB1),
                New ArabicLetter(&H6AD, &HFBD3, &HFBD4, &HFBD5, &HFBD6),
                New ArabicLetter(&H6C7, &HFBD7, &HFBD8),
                New ArabicLetter(&H6C6, &HFBD9, &HFBDA),
                New ArabicLetter(&H6C8, &HFBDB, &HFBDC),
                New ArabicLetter(&H6CB, &HFBDE, &HFBDF),
                New ArabicLetter(&H6C5, &HFBE0, &HFBE1, &HFBE2, &HFBE3),
                New ArabicLetter(&H6C9, &HFBE2, &HFBE3),
                New ArabicLetter(&H6D0, &HFBE4, &HFBE5, &HFBE6, &HFBE7),
                New ArabicLetter(&H649, &HFBE8, &HFBE9)
            }
            'new arabicletter(&h677,&hfbdd,&hxxx),

            For Each letter In letters
                defaults.Add(letter.Default, letter)
                isolateds.Add(letter.Isolated, letter)
                finals.Add(letter.Final, letter)
                all.Add(letter.Default, letter)
                all.Add(letter.Isolated, letter)
                all.Add(letter.Final, letter)
                If letter.Medial.HasValue Then medials.Add(letter.Medial, letter) : all.Add(letter.Medial, letter)
                If letter.Initial.HasValue Then initials.Add(letter.Initial, letter) : all.Add(letter.Initial, letter)
            Next
        End Sub

        ''' <summary>Gets value indicating if given character represents isoalted form of Arabic letter</summary>
        ''' <param name="ch">A character</param>
        ''' <returns>True if <paramref name="ch"/> represents isolated form of Arabic letter</returns>
        ''' <remarks>See <see cref="ArabicLetters"/> for more informations and limitations.</remarks>
        <Extension()> Function IsIsolatedForm(ByVal ch As Char) As Boolean
            Select Case AscW(ch)
                Case &HFBDD, &HFE70, &HFE72, &HFE74, &HFE76, &HFE78, &HFE7A, &HFE7C, &HFE7E, &HFE80:Return True
            End Select

            Return isolateds.ContainsKey(ch)
        End Function
        ''' <summary>Gets value indicating if given character represents final form of Arabic letter</summary>
        ''' <param name="ch">A character</param>
        ''' <returns>True if <paramref name="ch"/> represents final form of Arabic letter</returns>
        ''' <remarks>See <see cref="ArabicLetters"/> for more informations and limitations.</remarks>
        <Extension()> Function IsFinalForm(ByVal ch As Char) As Boolean
            Return finals.ContainsKey(ch)
        End Function
        ''' <summary>Gets value indicating if given character represents medial form of Arabic letter</summary>
        ''' <param name="ch">A character</param>
        ''' <returns>True if <paramref name="ch"/> represents medial form of Arabic letter</returns>
        ''' <remarks>See <see cref="ArabicLetters"/> for more informations and limitations.</remarks>
        <Extension()> Function IsMedialForm(ByVal ch As Char) As Boolean
            Select Case AscW(ch)
                Case &HFE71, &HFE77, &HFE79, &HFE7B, &HFE7D, &HFE7F:Return True
            End Select
            Return medials.ContainsKey(ch)
        End Function
        ''' <summary>Gets value indicating if given character represents initial form of Arabic letter</summary>
        ''' <param name="ch">A character</param>
        ''' <returns>True if <paramref name="ch"/> represents initial form of Arabic letter</returns>
        ''' <remarks>See <see cref="ArabicLetters"/> for more informations and limitations.</remarks>
        <Extension()> Function IsInitialForm(ByVal ch As Char) As Boolean
            Return initials.ContainsKey(ch)
        End Function
        ''' <summary>Gets value indicating if given character represents default form of Arabic letter</summary>
        ''' <param name="ch">A character</param>
        ''' <returns>True if <paramref name="ch"/> represents default form of Arabic letter</returns>
        ''' <remarks>See <see cref="ArabicLetters"/> for more informations and limitations.</remarks>
        <Extension()> Function IsDefaultForm(ByVal ch As Char) As Boolean
            Select Case AscW(ch)
                Case &H677, &H64B, &H64C, &H64D, &H64E, &H64F, &H650, &H651, &H652, &H621 : Return True
            End Select
            Return defaults.ContainsKey(ch)
        End Function

        ''' <summary>Converts given character to default form of Arabic letter</summary>
        ''' <param name="ch">Character to get default form of</param>
        ''' <returns>Character representing default form of <paramref name="ch"/>. Null when given character does not have default form or it is not known Arabic charactrer.
        ''' <note>For ligatures return string can be longer than one character.</note></returns>
        ''' <remarks>See <see cref="ArabicLetters"/> for more informations and limitations.</remarks>
        <Extension()> Function ToDefault(ByVal ch As Char) As String
            Select Case AscW(ch)
                Case &H677, &HFBDD : Return ChrW(&H677)
                Case &HFE70, &H64B : Return ChrW(&H64B)
                Case &HFE72, &H64C : Return ChrW(&H64C)
                Case &HFE74, &H64D : Return ChrW(&H64D)
                Case &HFE76, &HFE77, &H64E : Return ChrW(&H64E)
                Case &HFE78, &HFE79, &H64F : Return ChrW(&H64F)
                Case &HFE7A, &HFE7B, &H650 : Return ChrW(&H650)
                Case &HFE7C, &HFE7D, &H651 : Return ChrW(&H651)
                Case &HFE7E, &HFE7F, &H652 : Return ChrW(&H652)
                Case &HFE80, &H621 : Return ChrW(&H621)
            End Select
            If all.ContainsKey(ch) Then Return all(ch).Default Else Return Nothing
        End Function
        ''' <summary>Converts given character (or ligature) to final form of Arabic letter</summary>
        ''' <param name="value">Character to get final form of</param>
        ''' <returns>Character representing final form of <paramref name="value"/>. Null when given character does not have final form or it is not known Arabic charactrer.</returns>
        ''' <remarks>See <see cref="ArabicLetters"/> for more informations and limitations.</remarks>
        <Extension()> Function ToFinal(ByVal value$) As Char?
            If all.ContainsKey(value) Then Return all(value).Final Else Return Nothing
        End Function
        ''' <summary>Converts given character (or ligature) to isolated form of Arabic letter</summary>
        ''' <param name="value">Character to get isolated form of</param>
        ''' <returns>Character representing isolated form of <paramref name="value"/>. Null when given character does not have isolated form or it is not known Arabic charactrer.</returns>
        ''' <remarks>See <see cref="ArabicLetters"/> for more informations and limitations.</remarks>
        <Extension()> Function ToIsolated(ByVal value$) As Char?
            If value.Length = 1 Then
                Select Case AscW(value)
                    Case &H677, &HFBDD : Return ChrW(&HFBDD)
                    Case &HFE70, &H64B : Return ChrW(&HFE70)
                    Case &HFE72, &H64C : Return ChrW(&HFE72)
                    Case &HFE74, &H64D : Return ChrW(&HFE74)
                    Case &HFE76, &HFE77, &H64E : Return ChrW(&HFE76)
                    Case &HFE78, &HFE79, &H64F : Return ChrW(&HFE78)
                    Case &HFE7A, &HFE7B, &H650 : Return ChrW(&HFE7A)
                    Case &HFE7C, &HFE7D, &H651 : Return ChrW(&HFE7C)
                    Case &HFE7E, &HFE7F, &H652 : Return ChrW(&HFE7E)
                End Select
            End If
            If all.ContainsKey(value) Then Return all(value).Isolated Else Return Nothing
        End Function
        ''' <summary>Converts given character (or ligature) to medial form of Arabic letter</summary>
        ''' <param name="value">Character to get medial form of</param>
        ''' <returns>Character representing medial form of <paramref name="value"/>. Null when given character does not have medial form or it is not known Arabic charactrer.</returns>
        ''' <remarks>See <see cref="ArabicLetters"/> for more informations and limitations.</remarks>
        <Extension()> Function ToMedial(ByVal value$) As Char?
            If value.Length = 1 Then
                Select Case AscW(value)
                    Case &HFE76, &HFE77, &H64E : Return ChrW(&HFE77)
                    Case &HFE78, &HFE79, &H64F : Return ChrW(&HFE79)
                    Case &HFE7A, &HFE7B, &H650 : Return ChrW(&HFE7B)
                    Case &HFE7C, &HFE7D, &H651 : Return ChrW(&HFE7D)
                    Case &HFE7E, &HFE7F, &H652 : Return ChrW(&HFE7F)
                End Select
            End If
            If all.ContainsKey(value) Then Return all(value).Medial Else Return Nothing
        End Function
        ''' <summary>Converts given character (or ligature) to initial form of Arabic letter</summary>
        ''' <param name="value">Character to get initial form of</param>
        ''' <returns>Character representing initial form of <paramref name="value"/>. Null when given character does not have initial form or it is not known Arabic charactrer.</returns>
        ''' <remarks>See <see cref="ArabicLetters"/> for more informations and limitations.</remarks>
        <Extension()> Function ToInitial(ByVal value$) As Char?
            If all.ContainsKey(value) Then Return all(value).Initial Else Return Nothing
        End Function
    End Module

    'TODO: Equals
    ''' <summary>Represents arabic letter and it's four different forms</summary>
    ''' <version version="1.5.3" stage="Nightly">This structure is new in version 1.5.3</version>
    <DebuggerDisplay("{Default}")>
    Public Structure ArabicLetter
        Private ReadOnly _default As String
        Private ReadOnly _isolated As Char
        Private ReadOnly _initial As Char?
        Private ReadOnly _medial As Char?
        Private ReadOnly _final As Char
        ''' <summary>CTor - for letters with all four forms</summary>
        ''' <param name="default">Universal from of the letter</param>
        ''' <param name="isolated">Isolated from of the letter</param>
        ''' <param name="final">Final form of the letter</param>
        ''' <param name="initial">Initial form of the letter</param>
        ''' <param name="medial">Medial form of the letter</param>
        ''' <exception cref="ArgumentNullException"><paramref name="default"/> is nulll</exception>
        ''' <exception cref="ArgumentException"><paramref name="default"/> is an empty string</exception>
        Public Sub New(ByVal [default] As String, ByVal isolated As Char, ByVal final As Char, ByVal initial As Char?, ByVal medial As Char?)
            If [default] Is Nothing Then Throw New ArgumentNullException("default")
            If [default] = "" Then Throw New ArgumentException(ResourcesT.Exceptions.ArabicDefaultNotSpecified)
            _default = [default]
            _isolated = isolated
            _final = final
            _initial = initial
            _medial = medial
        End Sub
        ''' <summary>CTor - for letters with only isolated and final form</summary>
        ''' <param name="default">Universal from of the letter</param>
        ''' <param name="isolated">Isolated from of the letter</param>
        ''' <param name="final">Final form of the letter</param>
        ''' <exception cref="ArgumentNullException"><paramref name="default"/> is nulll</exception>
        ''' <exception cref="ArgumentException"><paramref name="default"/> is an empty string</exception>
        Public Sub New(ByVal [default] As String, ByVal isolated As Char, ByVal final As Char)
            Me.New([default], isolated, final, Nothing, Nothing)
        End Sub

        ''' <summary>CTor - for letters with all four forms specified as character code</summary>
        ''' <param name="default">Universal from of the letter (code)</param>
        ''' <param name="isolated">Isolated from of the letter (code)</param>
        ''' <param name="final">Final form of the letter (code)</param>
        ''' <param name="initial">Initial form of the letter (code)</param>
        ''' <param name="medial">Medial form of the letter (code)</param>
        Public Sub New(ByVal default%, ByVal isolated%, ByVal final%, ByVal initial%, ByVal medial%)
            _default = ChrW([default])
            _isolated = ChrW(isolated)
            _final = ChrW(final)
            _initial = ChrW(initial)
            _medial = ChrW(medial)
        End Sub
        ''' <summary>CTor - for letters with only isolated and final form specified as character code</summary>
        ''' <param name="default">Universal from of the letter (code)</param>
        ''' <param name="isolated">Isolated from of the letter (code)</param>
        ''' <param name="final">Final form of the letter (code)</param>
        Public Sub New(ByVal default%, ByVal isolated%, ByVal final%)
            _default = ChrW([default])
            _isolated = ChrW(isolated)
            _final = ChrW(final)
        End Sub
        ''' <summary>CTor - for letters with only isolated and final form specified as string for default form and character codes for the others</summary>
        ''' <param name="default">Universal from of the letter</param>
        ''' <param name="isolated">Isolated from of the letter (code)</param>
        ''' <param name="final">Final form of the letter (code)</param>
        ''' <exception cref="ArgumentNullException"><paramref name="default"/> is nulll</exception>
        ''' <exception cref="ArgumentException"><paramref name="default"/> is an empty string</exception>
        Public Sub New(ByVal default$, ByVal isolated%, ByVal final%)
            If [default] Is Nothing Then Throw New ArgumentNullException("default")
            If [default] = "" Then Throw New ArgumentException(ResourcesT.Exceptions.ArabicDefaultNotSpecified)
            _default = default$
            _isolated = ChrW(isolated)
            _final = ChrW(final)
        End Sub
        ''' <summary>Gets character representing default (context-unaware) form of the letter</summary>
        ''' <remarks>For ligatures string longer than one character can be returned</remarks>
        Public ReadOnly Property [Default]() As String
            Get
                Return _default
            End Get
        End Property
        ''' <summary>Gets character representing isolated form of the letter (used when letter is written alone i.e. not in word)</summary>
        Public ReadOnly Property Isolated() As Char
            Get
                Return _isolated
            End Get
        End Property
        ''' <summary>Gets character representing initial form of the letter (used when letter is at the beginning of a word)</summary>
        Public ReadOnly Property Initial() As Char?
            Get
                Return _initial
            End Get
        End Property
        ''' <summary>Gets character representing medial form of the letter (used when lettter is in the middle of a word)</summary>
        Public ReadOnly Property Medial() As Char?
            Get
                Return _medial
            End Get
        End Property
        ''' <summary>Gets character representing final form of the letter (used when lettter is at the and of a word)</summary>
        Public ReadOnly Property Final() As Char
            Get
                Return _final
            End Get
        End Property

        ''' <summary>Converts a <see cref="ArabicLetter"/> to <see cref="String"/></summary>
        ''' <param name="a">A <see cref="ArabicLetter"/></param>
        ''' <returns><paramref name="a"/>.<see cref="[Default]">Default</see></returns>
        Public Shared Widening Operator CType(ByVal a As ArabicLetter) As String
            Return a.Default
        End Operator

        ''' <summary>Converts a <see cref="ArabicLetter"/> to <see cref="Char"/></summary>
        ''' <param name="a">A <see cref="ArabicLetter"/></param>
        ''' <returns><paramref name="a"/>.<see cref="[Default]">Default</see></returns>
        ''' <exception cref="InvalidCastException"><paramref name="a"/>.<see cref="[Default]">Default</see> is longer than one character</exception>
        Public Shared Narrowing Operator CType(ByVal a As ArabicLetter) As Char
            If a.Default.Length > 1 Then Throw New InvalidCastException(ResourcesT.Exceptions.LigatureToChar)
            Return a.Default
        End Operator
        ''' <summary>Gets string representation of current object</summary>
        ''' <returns><see cref="[Default]"/></returns>
        Public Overrides Function ToString() As String
            Return [Default]
        End Function
        ''' <summary>Gets hascode of current object</summary>
        ''' <returns>Has code of this object whic is effectively hash code of <see cref="[Default]"/>; 0 when <see cref="[Default]"/> is null</returns>
        Public Overrides Function GetHashCode() As Integer
            If [Default] Is Nothing Then Return 0
            Return [Default].GetHashCode
        End Function
    End Structure
End Namespace
#End If