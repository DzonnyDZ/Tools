Imports System.Collections.Generic
Imports System.Text
Imports System.Text.RegularExpressions

Namespace CollectionsT.GenericT
    ''' <summary>String-string dictionary for transliteration</summary>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class TransliterationDictionary
        Inherits DuplicateDictionary(Of String, String)
#Region "CTors"
        ''' <summary>CTor - creates a new instance of <see cref="TransliterationDictionary"/> class</summary>
        ''' <param name="dictionary">Contains data to populate dictionary with</param>
        ''' <param name="comparison">Comparison used for comparing keys</param>
        ''' <exception cref="ArgumentNullException"><paramref name="dictionary"/> or <paramref name="comparison"/> is null</exception>
        Public Sub New(ByVal dictionary As IEnumerable(Of KeyValuePair(Of String, String)))
            MyBase.New(dictionary, StringComparer.Ordinal)
        End Sub
        ''' <summary>Copy CTor - creates a copy of given instance of <see cref="DuplicateDictionary{TKey, TValue}"/></summary>
        ''' <param name="dictionary">A dictionary to create clone of</param>
        ''' <exception cref="ArgumentNullException"><paramref name="dictionary"/> is null</exception>
        Public Sub New(ByVal dictionary As TransliterationDictionary)
            MyBase.New(dictionary)
        End Sub
#End Region

#Region "FSA"
        ''' <summary>States of Finite State Automaton</summary>
        Private Enum State
            ''' <summary>Normal text</summary>
            Normal
            ''' <summary>&lt;</summary>
            Lt
            ''' <summary>&lt;/</summary>
            LtEnd
            ''' <summary>&lt;/text</summary>
            TagEnd
            ''' <summary>&lt;/text□</summary>
            TagEndWhite
            ''' <summary>&lt;text</summary>            
            XmlName
            ''' <summary>&lt;text/</summary>
            SelfEnd
            ''' <summary>&lt;text□</summary>
            TagInside
            ''' <summary>&lt;text□text</summary>
            AttrStart
            ''' <summary>&lt;text□text□</summary>
            AfterAttrName
            ''' <summary>&lt;text□text=</summary>
            AfterEq
            ''' <summary>&lt;text□text='</summary>
            Attr1
            ''' <summary>&lt;text□text="</summary>
            Attr2
            ''' <summary>&lt;text□text="" or &lt;text□text=''</summary>
            AfterAttr
            ''' <summary>&lt;?</summary>
            PIStart
            ''' <summary>&lt;?text</summary>
            PIName
            ''' <summary>&lt;?text?</summary>
            PIForceEnd
            ''' <summary>&lt;?text□</summary>
            PIValue
            ''' <summary>&lt;?text□?</summary>
            PIEnd
            ''' <summary>&lt;!</summary>
            LtExcl
            ''' <summary>&lt;?!-</summary>
            LtExcl1
            ''' <summary>&lt;?!--</summary>
            Comment
            ''' <summary>&lt;?!--text-</summary>
            C1
            ''' <summary>&lt;?!--text--</summary>
            C2
            ''' <summary>&lt;![</summary>
            LtExclOpen
            ''' <summary>&lt;![C</summary>
            LtExclOpenC
            ''' <summary>&lt;![CD</summary>
            LtExclOpenCD
            ''' <summary>&lt;![CDA</summary>
            LtExclOpenCDA
            ''' <summary>&lt;![CDAT</summary>
            LtExclOpenCDAT
            ''' <summary>&lt;![CDATA</summary>
            LtExclOpenCDATA
            ''' <summary>&lt;![CDATA[</summary>
            CDATA
            ''' <summary>&lt;![CDATA[text]</summary>
            CClose
            ''' <summary>&lt;![CDATA[text]]</summary>
            CClose2
            ''' <summary>&amp;</summary>
            Amp
            ''' <summary>&amp;text</summary>
            EntityName
            ''' <summary>&amp;#</summary>
            AmpHash
            ''' <summary>&amp;#x or &amp;#X</summary>
            AmpHashX
            ''' <summary>&amp;#x0</summary>
            HexEntity
            ''' <summary>&amp;#0</summary>
            DecimalEntity

            ''' <summary>Special value used to indicate error in XML parsing which requires rolback to last saved point</summary>
            [Error] = Int32.MinValue
            ''' <summary>Special state used to indicate recovery from unparsed (XML) text</summary>
            NoChange = Int32.MinValue + 1
        End Enum

        Public Function Transliterate(ByVal text As String, ByVal xmlSafe As Boolean) As String
            If text Is Nothing OrElse text = "" Then
                Return text
            End If
            Dim b As New StringBuilder(text.Length)
            'Output text
            Dim state__1 As State = State.Normal
            'FSA state
            Dim beginPoints As New Stack(Of Integer)()
            'Points for rollbacks and string-begining detection purposes
            beginPoints.Push(0)

            For i As Integer = 0 To text.Length
                If i < text.Length Then
                    Dim ch As Char = text(i)
                    Select Case state__1
                        Case State.Normal
                            ' normal text
                            If True Then
                                If xmlSafe Then
                                    Select Case ch
                                        Case "<"c
                                            state__1 = State.Lt
                                            beginPoints.Push(i)
                                            Continue For
                                        Case "&"c
                                            state__1 = State.Amp
                                            beginPoints.Push(i)
                                            Continue For
                                    End Select
                                End If
                                Dim bp As Integer = beginPoints.Pop()
                                beginPoints.Push(bp + ReplaceAndAdd(b, text.Substring(bp, i - bp + 1), False))
                                Continue For
                            End If
                        Case State.Lt
                            ' <
                            Select Case ch
                                Case "/"c
                                    state__1 = State.LtEnd
                                    Continue For
                                Case "?"c
                                    state__1 = State.PIStart
                                    Continue For
                                Case "!"c
                                    state__1 = State.LtExcl
                                    Continue For
                            End Select
                            If IsNameStartCharacter(ch) Then
                                state__1 = State.XmlName
                            Else
                                GoTo case_State_Error
                            End If
                            Continue For
                        Case State.LtEnd
                            ' </
                            If IsNameStartCharacter(ch) Then
                                state__1 = State.TagEnd
                            Else
                                GoTo case_State_Error
                            End If
                            Continue For
                        Case State.TagEnd
                            ' </text
                            If ch = ">"c Then
                                GoTo case_State_NoChange
                            End If
                            Select Case ch
                                Case " "c, ControlChars.Tab, ControlChars.Cr, ControlChars.Lf
                                    state__1 = State.TagEndWhite
                                    Continue For
                            End Select
                            If IsNameCharacter(ch) Then
                                Continue For
                            Else
                                GoTo case_State_Error
                            End If
                        Case State.TagEndWhite
                            ' </text□
                            If ch = ">"c Then
                                GoTo case_State_NoChange
                            End If
                            Select Case ch
                                Case " "c, ControlChars.Tab, ControlChars.Cr, ControlChars.Lf
                                    Continue For
                            End Select
                            GoTo case_State_Error
                        Case State.XmlName
                            ' <text
                            If ch = ">"c Then
                                GoTo case_State_NoChange
                            End If
                            Select Case ch
                                Case "/"c
                                    state__1 = State.SelfEnd
                                    Continue For
                                Case " "c, ControlChars.Tab, ControlChars.Cr, ControlChars.Lf
                                    state__1 = State.TagInside
                                    Continue For
                            End Select
                            If IsNameCharacter(ch) Then
                                Continue For
                            Else
                                GoTo case_State_Error
                            End If
                        Case State.SelfEnd
                            ' <text/ -or- <text / -or- <text someattr="xxx"/
                            If ch = ">"c Then
                                GoTo case_State_NoChange
                            Else
                                GoTo case_State_Error
                            End If
                        Case State.TagInside
                            ' <text□
                            If ch = ">"c Then
                                GoTo case_State_NoChange
                            End If
                            Select Case ch
                                Case " "c, ControlChars.Tab, ControlChars.Cr, ControlChars.Lf
                                    Continue For
                                Case "/"c
                                    state__1 = State.SelfEnd
                                    Continue For
                            End Select
                            If IsNameStartCharacter(ch) Then
                                state__1 = State.AttrStart
                            Else
                                GoTo case_State_Error
                            End If
                            Continue For
                        Case State.AttrStart
                            ' <text□text
                            Select Case ch
                                Case " "c, ControlChars.Tab, ControlChars.Cr, ControlChars.Lf
                                    state__1 = State.AfterAttrName
                                    Continue For
                                Case "="c
                                    state__1 = State.AfterEq
                                    Continue For
                            End Select
                            If IsNameCharacter(ch) Then
                                Continue For
                            Else
                                GoTo case_State_Error
                            End If
                        Case State.AfterAttrName
                            ' <text□text□
                            Select Case ch
                                Case " "c, ControlChars.Tab, ControlChars.Cr, ControlChars.Lf
                                    Continue For
                                Case "="c
                                    state__1 = State.AfterEq
                                    Continue For
                            End Select
                            GoTo case_State_Error
                        Case State.AfterEq
                            ' <text□text=
                            Select Case ch
                                Case " "c, ControlChars.Tab, ControlChars.Cr, ControlChars.Lf
                                    Continue For
                                Case "'"c
                                    state__1 = State.Attr1
                                    Continue For
                                Case """"c
                                    state__1 = State.Attr2
                                    Continue For
                            End Select
                            GoTo case_State_Error
                        Case State.Attr1
                            ' <text□text='
                            Select Case ch
                                Case "'"c
                                    state__1 = State.AfterAttr
                                    Continue For
                                Case Else
                                    Continue For
                            End Select
                        Case State.Attr2
                            ' <text□text="
                            Select Case ch
                                Case """"c
                                    state__1 = State.AfterAttr
                                    Continue For
                                Case Else
                                    Continue For
                            End Select
                        Case State.AfterAttr
                            ' <text□text="something" -or- // <text□text='something'
                            If ch = ">"c Then
                                GoTo case_State_NoChange
                            End If
                            Select Case ch
                                Case " "c, ControlChars.Tab, ControlChars.Cr, ControlChars.Lf
                                    state__1 = State.TagInside
                                    Continue For
                                Case "/"c
                                    state__1 = State.SelfEnd
                                    Continue For
                            End Select
                            GoTo case_State_Error
                            ' <?
                        Case State.PIStart
                            If IsNameStartCharacter(ch) Then
                                state__1 = State.PIName
                            Else
                                GoTo case_State_Error
                            End If
                            Continue For
                        Case State.PIName
                            ' <?text
                            Select Case ch
                                Case "?"c
                                    state__1 = State.PIForceEnd
                                    Continue For
                                Case " "c, ControlChars.Tab, ControlChars.Cr, ControlChars.Lf
                                    state__1 = State.PIValue
                                    Continue For
                            End Select
                            If IsNameCharacter(ch) Then
                                Continue For
                            Else
                                GoTo case_State_Error
                            End If
                        Case State.PIForceEnd
                            ' <?text?
                            If ch = ">"c Then
                                GoTo case_State_NoChange
                            Else
                                GoTo case_State_Error
                            End If
                        Case State.PIValue
                            ' <?text□ -or- <?text□text
                            Select Case ch
                                Case "?"c
                                    state__1 = State.PIEnd
                                    Continue For
                                Case Else
                                    Continue For
                            End Select
                        Case State.PIEnd
                            ' <?text□text?
                            If ch = ">"c Then
                                GoTo case_State_NoChange
                            End If
                            Select Case ch
                                Case "?"c
                                    Continue For
                            End Select
                            state__1 = State.PIValue
                            Continue For
                        Case State.LtExcl
                            ' <!
                            Select Case ch
                                Case "-"c
                                    state__1 = State.LtExcl1
                                    Continue For
                                Case "["c
                                    state__1 = State.LtExclOpen
                                    Continue For
                            End Select
                            GoTo case_State_Error
                        Case State.LtExcl1
                            ' <!-
                            Select Case ch
                                Case "-"c
                                    state__1 = State.Comment
                                    Continue For
                            End Select
                            GoTo case_State_Error
                        Case State.Comment
                            ' <!-- -or- <!--text
                            Select Case ch
                                Case "-"c
                                    state__1 = State.C1
                                    Continue For
                            End Select
                            Continue For
                        Case State.C1
                            ' <!--text-
                            Select Case ch
                                Case "-"c
                                    state__1 = State.C2
                                    Continue For
                                Case Else
                                    state__1 = State.Comment
                                    Continue For
                            End Select
                        Case State.C2
                            ' <!--text--
                            If ch = ">"c Then
                                GoTo case_State_NoChange
                            End If
                            Select Case ch
                                Case "-"c
                                    Continue For
                            End Select
                            state__1 = State.Comment
                            Continue For
                        Case State.LtExclOpen
                            ' <![
                            Select Case ch
                                Case "C"c
                                    state__1 = State.LtExclOpenC
                                    Continue For
                            End Select
                            GoTo case_State_Error
                        Case State.LtExclOpenC
                            ' <![C
                            Select Case ch
                                Case "D"c
                                    state__1 = State.LtExclOpenCD
                                    Continue For
                            End Select
                            GoTo case_State_Error
                        Case State.LtExclOpenCD
                            ' <![CD
                            Select Case ch
                                Case "A"c
                                    state__1 = State.LtExclOpenCDA
                                    Continue For
                            End Select
                            GoTo case_State_Error
                        Case State.LtExclOpenCDA
                            ' <![CDA
                            Select Case ch
                                Case "T"c
                                    state__1 = State.LtExclOpenCDAT
                                    Continue For
                            End Select
                            GoTo case_State_Error
                        Case State.LtExclOpenCDAT
                            ' <![CDAT
                            Select Case ch
                                Case "A"c
                                    state__1 = State.LtExclOpenCDATA
                                    Continue For
                            End Select
                            GoTo case_State_Error
                        Case State.LtExclOpenCDATA
                            ' <![CDATA
                            Select Case ch
                                Case "["c
                                    state__1 = State.CDATA
                                    b.Append("<![CDATA[")
                                    beginPoints.Pop()
                                    'XML point
                                    beginPoints.Pop()
                                    'Previous normal text point
                                    beginPoints.Push(i + 1)
                                    'New normal text point
                                    Continue For
                            End Select
                            GoTo case_State_Error
                        Case State.CDATA
                            ' <![CDATA[ -or- <![CDATA[text
                            If True Then
                                Select Case ch
                                    Case "]"c
                                        state__1 = State.CClose
                                        Continue For
                                End Select
                                Dim bp As Integer = beginPoints.Pop()
                                beginPoints.Push(bp + ReplaceAndAdd(b, text.Substring(bp, i - bp + 1), False))
                                Continue For
                            End If
                        Case State.CClose
                            ' <![CDATA[text]
                            If True Then
                                Select Case ch
                                    Case "]"c
                                        state__1 = State.CClose2
                                        Continue For
                                End Select
                                'int bp = beginPoints.Pop();
                                'beginPoints.Push(bp + ReplaceAndAdd(b, text.Substring(bp, i - bp + 1), false));
                                state__1 = State.CDATA
                                i -= 1
                                Continue For
                            End If
                        Case State.CClose2
                            ' <!CDATA[text]]
                            If True Then
                                If ch = ">"c Then
                                    beginPoints.Push(i - 2)
                                    'XML start point
                                    GoTo case_State_NoChange
                                End If
                                'int bp = beginPoints.Pop();
                                'beginPoints.Push(bp + ReplaceAndAdd(b, text.Substring(bp, i - bp + 1), false));
                                state__1 = State.CDATA
                                i -= 2
                                Continue For
                            End If
                        Case State.Amp
                            ' &
                            Select Case ch
                                Case "#"c
                                    state__1 = State.AmpHash
                                    Continue For
                            End Select
                            If IsNameStartCharacter(ch) Then
                                state__1 = State.EntityName
                            Else
                                GoTo case_State_Error
                            End If
                            Continue For
                        Case State.EntityName
                            ' &text
                            If IsNameCharacter(ch) Then
                                Continue For
                            End If
                            If ch = ";"c Then
                                GoTo case_State_NoChange
                            Else
                                GoTo case_State_Error
                            End If
                        Case State.AmpHash
                            ' &#
                            Select Case ch
                                Case "x"c, "X"c
                                    state__1 = State.AmpHashX
                                    Continue For
                                Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, _
                                 "6"c, "7"c, "8"c, "9"c
                                    state__1 = State.DecimalEntity
                                    Continue For
                            End Select
                            GoTo case_State_Error
                        Case State.AmpHashX
                            ' &#x
                            Select Case ch
                                Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, _
                                 "6"c, "7"c, "8"c, "9"c, "A"c, "B"c, _
                                 "C"c, "D"c, "E"c, "F"c, "a"c, "b"c, _
                                 "c"c, "d"c, "e"c, "f"c
                                    state__1 = State.HexEntity
                                    Continue For
                            End Select
                            GoTo case_State_Error
                        Case State.HexEntity
                            ' &#xA
                            Select Case ch
                                Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, _
                                 "6"c, "7"c, "8"c, "9"c, "A"c, "B"c, _
                                 "C"c, "D"c, "E"c, "F"c, "a"c, "b"c, _
                                 "c"c, "d"c, "e"c, "f"c
                                    Continue For
                            End Select
                            If ch = ";"c Then
                                GoTo case_State_NoChange
                            End If
                            GoTo case_State_Error
                        Case State.DecimalEntity
                            ' &#0
                            Select Case ch
                                Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, _
                                 "6"c, "7"c, "8"c, "9"c
                                    Continue For
                            End Select
                            If ch = ";"c Then
                                GoTo case_State_NoChange
                            End If
                            GoTo case_State_Error

                            '|\\|//|\\|//|\\|//|\\|//|\\|//|\\|//|\\|//|\\|//|\\|//|\\|//|\\|//|\\|//|\\|//|\\|//|\\|//|\\|//|\\|//|\\|//\\|//|\\|//|\\|//|\\|//
                            'SPECIAL
                        Case State.[Error]
Case_State_Error:           'Error occured in XML parsing - rollback
                            If True Then
                                i = beginPoints.Pop()
                                'Beginning of would-be-XML
                                Dim bp As Integer = beginPoints.Pop()
                                beginPoints.Push(bp + ReplaceAndAdd(b, text.Substring(bp, i - bp + 1), False))
                                state__1 = State.Normal
                                Continue For
                            End If
                        Case State.NoChange
Case_State_NoChange:        'End of XML parsing
                            state__1 = State.Normal
                            Dim xmlStart As Integer = beginPoints.Pop()
                            Dim normalStart As Integer = beginPoints.Pop()
                            ReplaceAndAdd(b, text.Substring(normalStart, xmlStart - normalStart), True)
                            b.Append(text.Substring(xmlStart, i - xmlStart + 1))
                            beginPoints.Push(i + 1)
                            'New normal block starts with next character
                            Continue For
                    End Select
                Else
                    'End fixup
                    Select Case state__1
                        Case State.CDATA, State.Normal
                            If True Then
                                Dim bp As Integer = beginPoints.Pop()
                                beginPoints.Push(bp + ReplaceAndAdd(b, text.Substring(bp), True))
                            End If
                            Exit Select
                        Case State.CClose, State.CClose2
                            If True Then
                                Dim bp As Integer = beginPoints.Pop()
                                beginPoints.Push(bp + ReplaceAndAdd(b, text.Substring(bp), True))
                            End If
                            Exit Select
                        Case Else
                            If True Then
                                i = beginPoints.Pop()
                                Dim bp As Integer = beginPoints.Pop()
                                beginPoints.Push(bp + ReplaceAndAdd(b, text.Substring(bp, i - bp + 1), False))
                                state__1 = State.Normal
                            End If
                            Exit Select
                    End Select
                End If
            Next

            Return b.ToString()
        End Function

        ''' <summary>Transliterates given text and adds it to output or continues lookup</summary>
        ''' <param name="target">A <see cref="StringBuilder"/> to append output to</param>
        ''' <param name="textInBlock">Current text block</param>
        ''' <param name="force">True if we are at end of text block and text must be appended - prefix lookup cannot continue</param>
        ''' <returns>
        ''' Number of characters to advance start of text block by. Always between 0 and <paramref name="textInBlock"/>.<see cref="String.Length">Length</see>.
        ''' When <paramref name="force"/> is true always returns <paramref name="textInBlock"/>.<see cref="String.Length">Length</see>.
        ''' </returns>
        Private Function ReplaceAndAdd(ByVal target As StringBuilder, ByVal textInBlock As String, ByVal force As Boolean) As Integer
            If String.IsNullOrEmpty(textInBlock) Then
                Return 0
            End If

            Dim prefixReplacements As KeyValuePair(Of String, String)() = Me.GetAllStartingWith(textInBlock)
            Dim exactReplacements As String() = Me.GetAll(textInBlock)
            If prefixReplacements.Length > 0 AndAlso prefixReplacements.Length > exactReplacements.Length AndAlso Not force Then
                'Continue prefix lookup
                Return 0
            ElseIf prefixReplacements.Length > 0 AndAlso prefixReplacements.Length = exactReplacements.Length OrElse (force AndAlso exactReplacements.Length > 0) Then
                'We found exact match
                target.Append(exactReplacements(0))
                Return textInBlock.Length
            Else
                'No prefix match found
                '1st - try shorter exact matches
                For len As Integer = textInBlock.Length - 1 To 1 Step -1
                    exactReplacements = Me.GetAll(textInBlock.Substring(0, len))
                    If exactReplacements.Length > 0 Then
                        target.Append(exactReplacements(0))
                        Return ReplaceAndAdd(target, textInBlock.Substring(len), force) + len
                    End If
                Next
                '2nd - skip 1st character
                target.Append(textInBlock(0))
                Return ReplaceAndAdd(target, textInBlock.Substring(1), force) + 1
            End If
        End Function
#Region "Regular expressions"
        ''' <summary>Regular expression fragment to detect characters that can appear at start of XML name</summary>
        Private Const NameStartCharacters As String = "\: | [A-Z] | _ | [a-z] | [\u00C0-\u00D6] | [\u00D8-\u00F6] | [\u00F8-\u02FF] | [\u0370-\u037D] | [\u037F-\u1FFF] | [\u200C-\u200D] | [\u2070-\u218F] | [\u2C00-\u2FEF] | [\u3001-\uD7FF] | [\uF900-\uFDCF] | [\uFDF0-\uFFFD] | [\uD800-\uDB7F] | [\uDC00-\uDFFFF]"
        ''' <summary>Regular expression fragment to detect characters that can appear inside XML name</summary>
        Private Const NameCharacters As String = NameStartCharacters & " | - | \. | [0-9] | \u00B7 | [\u0300-\u036F] | [\u203F-\u2040]"
        ''' <summary>Regular expression used to verify if character can appear at start of XML name</summary>
        Private Shared ReadOnly NameStartRegEx As New Regex("^" & NameStartCharacters & "$", RegexOptions.Compiled Or RegexOptions.CultureInvariant Or RegexOptions.IgnorePatternWhitespace)
        ''' <summary>Reguler expression used to verify if character ca appear inside XML name</summary>
        Private Shared ReadOnly NameRegEx As New Regex("^" & NameCharacters & "$", RegexOptions.Compiled Or RegexOptions.CultureInvariant Or RegexOptions.IgnorePatternWhitespace)
        ''' <summary>Verifies if character can appear at start of XML name</summary>
        ''' <param name="character">A character to verify</param>
        ''' <returns>True when character can appear as first character of XML name; false otherwise</returns>
        Private Shared Function IsNameStartCharacter(ByVal character As Char) As Boolean
            Return NameStartRegEx.IsMatch(character.ToString())
        End Function
        ''' <summary>Verifies if character can appear inside XML name</summary>
        ''' <param name="character">A character to verify</param>
        ''' <returns>True when character can appear as part of XML name; false otherwise</returns>
        Private Shared Function IsNameCharacter(ByVal character As Char) As Boolean
            Return NameRegEx.IsMatch(character.ToString())
        End Function
#End Region
#End Region

#Region "ICloneable members"
        ''' <summary>Internally implements the <see cref="Clone"/> method.</summary>
        ''' <returns>Cloned instance of current instance. Derived class must always override this method and return instance of derived class.</returns>
        Protected Overrides Function CloneInternal() As DuplicateDictionary(Of String, String)
            Return New TransliterationDictionary(Me)
        End Function
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance.</returns>
        Public Shadows Function Clone() As TransliterationDictionary
            Return DirectCast(CloneInternal(), TransliterationDictionary)
        End Function
#End Region

#Region "Indexing"
        ''' <summary>Gets all items from dictionary which's key starts with given prefix</summary>
        ''' <param name="prefix">Prefix to get jkeys starting with</param>
        ''' <returns>Array of keys and values whose keys start with <paramref name="prefix"/>.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="prefix"/> is null;</exception>
        Public Function GetAllStartingWith(ByVal prefix As String) As KeyValuePair(Of String, String)()
            Dim firstIndex As Integer = FirstIndexOfPrefix(prefix)
            Dim lastIndex As Integer = LastIndexOfPrefix(prefix, firstIndex)
            If firstIndex < 0 Then
                Return New KeyValuePair(Of String, String)() {}
            End If
            Dim ret As KeyValuePair(Of String, String)() = New KeyValuePair(Of String, String)(lastIndex - firstIndex) {}
            For i As Integer = 0 To ret.Length - 1
                ret(i) = Me(firstIndex + i)
            Next
            Return ret
        End Function

        ''' <summary>Gets first index of item whose key starts with given prefix</summary>
        ''' <param name="prefix">Prefix of item key</param>
        ''' <returns>First (the lowest) index of item with key starting with <paramref name="prefix"/>. -1 where ther is no such item.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="prefix"/> is null;</exception>
        Public Function FirstIndexOfPrefix(ByVal prefix As String) As Integer
            If prefix Is Nothing Then
                Throw New ArgumentNullException("prefix")
            End If
            If Count = 0 Then
                Return -1
            End If
            Dim minIndex As Integer = 0
            Dim maxIndex As Integer = Count - 1
            Do
                Dim index As Integer = (maxIndex + minIndex) \ 2
                Dim result As Integer = Comparison(Me(index).Key.Substring(0, Math.Min(Me(index).Key.Length, prefix.Length)), prefix)
                If result > 0 Then
                    'list[index] > key - need to search in lower part
                    maxIndex = index - 1
                ElseIf result < 0 Then
                    'list[index] < key - need to search in upper part
                    minIndex = index + 1
                Else
                    While index - 1 > 0 AndAlso Comparison(Me(index - 1).Key.Substring(0, Math.Min(Me(index - 1).Key.Length, prefix.Length)), prefix) = 0
                        index -= 1
                    End While
                    Return index
                End If
            Loop While maxIndex >= minIndex
            Return -1
        End Function

        ''' <summary>Gets last index of item whose key starts with given prefix</summary>
        ''' <param name="prefix">Prefix of item key</param>
        ''' <param name="firstIndex">Index of first occurence of item with key starting with <paramref name="prefix"/></param>
        ''' <returns>Index of last occurence of key starting with <paramref name="prefix"/> after <paramref name="firstIndex"/>. <paramref name="firstIndex"/> where there are no more occurences of keys with prefix <paramref name="prefix"/> after index <paramref name="firstIndex"/> or <paramref name="firstImdex"/> is less than zero.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="prefix"/> is null;</exception>
        Private Function LastIndexOfPrefix(ByVal prefix As String, ByVal firstIndex As Integer) As Integer
            If prefix Is Nothing Then
                Throw New ArgumentNullException("prefix")
            End If
            If firstIndex < 0 Then
                Return firstIndex
            End If
            Dim index As Integer = firstIndex
            While index + 1 < Count AndAlso Comparison(Me(index + 1).Key.Substring(0, Math.Min(Me(index + 1).Key.Length, prefix.Length)), prefix) = 0
                index += 1
            End While
            Return index
        End Function

        ''' <summary>Gets last index of item whose key starts with given prefix</summary>
        ''' <param name="prefix">Prefix of item key</param>
        ''' <returns>Last (the highets) index of item with key starting with <paramref name="prefix"/>. -1 where there is no such item.</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="prefix"/> is null;</exception>
        Public Function LastIndexOfPrefix(ByVal prefix As String) As Integer
            Return LastIndexOfPrefix(prefix, FirstIndexOfPrefix(prefix))
        End Function
#End Region
    End Class
End Namespace
