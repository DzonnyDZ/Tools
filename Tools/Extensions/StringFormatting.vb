Imports System.Runtime.CompilerServices
Imports CultureInfo = System.Globalization.CultureInfo

Namespace ExtensionsT
#If Config <= Nightly Then 'Stage:Nightly
    ''' <summary>Provides string formatting</summary>
    ''' <remarks>Formatting rules:
    ''' <para>Formating combines C#-like string escaping and formatting used by <see cref="[String].Format"/>.</para>
    ''' <para>Escaping rules:</para>
    ''' <list type="table"><listheader><term>Escape sequence</term><description>Meaning</description></listheader>
    ''' <item><term>\a</term><description>Allert 0x7</description></item>
    ''' <item><term>\b</term><description>Backspace 0x8</description></item>
    ''' <item><term>\f</term><description>Form feed 0xC</description></item>
    ''' <item><term>\n</term><description>New line 0xA</description></item>
    ''' <item><term>\r</term><description>Carriage return 0xD</description></item>
    ''' <item><term>\t</term><description>Horizontal tab 0x9</description></item>
    ''' <item><term>\v</term><description>Vertical tab 0xB</description></item>
    ''' <item><term>\.</term><description>Empty string (ignored)</description></item>
    ''' <item><term>\U[0-9A-Za-z]+, \u[0-9A-Za-z]+, \X[0-9A-Za-z]+, \x[0-9A-Za-z]+</term><description>Hexadecimal Unicode escape sequence. Given hexadecimal number of any length is passed to <see cref="[Char].ConvertFromUtf32"/>.</description></item>
    ''' <item><term>\[0-9]+</term><description>Decimal Unicode escape sequence. Given decimal number of any length is passed to <see cref="[Char].ConvertFromUtf32"/>.</description></item>
    ''' <item><term>\&lt;any other character></term>The character, not the backslash.</item>
    ''' <item><term>{{</term><description>{ (only when formatting is allowed; {{ otherwise)</description></item>
    ''' <item><term>}}</term><description>} (only when formatting is allowed; }} otherwise)</description></item>
    ''' </list>
    ''' <para>In particular following escape sequences are available:</para>
    ''' <list><listheader><term>Escape sequance</term><description>Meaning</description></listheader>
    ''' <item><term>\0</term><description>Nullchar</description></item>
    ''' <item><term>\\</term><description>\</description></item>
    ''' <item><term>\"</term><description>"</description></item>
    ''' <item><term>\'</term><description>'</description></item>
    ''' </list>
    ''' <para>Be carefull when typign such string in languages that processes it like C#, C++ or PHP. Get output \ from escaping \\\\ must be typed.</para>
    ''' <para>Following rules apply to fromatting (only when formating is being done):</para>
    ''' <list type="bullet">
    ''' <item>String may contain placeholers of arguments being formatted (passed in array). Each placeholder reffers to index within the array.</item>
    ''' <item>Placeholder is in format  {index[,alignment][:formatString]}</item>
    ''' <item>Index is any non-negative decimal integral number [0-9]+</item>
    ''' <item>Alignment is optional, preseded with comma and it is -?[0-9]+ defimal integral number delaring minimal width of string replacing the placeholder. Negative for left-align, positive for right aling. If padding is necessary space is used. Trimming never occurs.</item>
    ''' <item>FormatString is optional formatting string passed to formating method of argument. Ifnored when argument does implement neither <see cref="ICustomFormatter"/> nor <see cref="IFormattable"/>.</item>
    ''' </list>
    ''' <para>In format string escaping is done in same way as described above.</para>
    ''' </remarks>
    ''' <version version="1.5.2">Module introduced</version>
    ''' <version version="1.5.3">In 1.5.2 the module was not made public by mistake - so, accessibility changed from Friend (internal) to Public.</version>
    Public Module StringFormatting
        ''' <summary>Formats and escape string according to rules described for <see cref="StringFormatting"/></summary>
        ''' <param name="str">Formatting string</param>
        ''' <param name="Args">Objects to format</param>
        ''' <returns>String formatted</returns>
        ''' <param name="provider">Formatting provider</param>
        ''' <exception cref="FormatException"><paramref name="str"/> is invalid format string</exception>
        <Extension()> _
        Public Function CFormat(ByVal str As String, ByVal provider As IFormatProvider, ByVal ParamArray Args As Object()) As String
            Return CFormat(provider, str, Args, True)
        End Function
        ''' <summary>Un-escapes string according to rules described for <see cref="StringFormatting"/> without formatting it</summary>
        ''' <param name="str">String to un-escape</param>
        ''' <returns>String unescaped</returns>
        ''' <exception cref="FormatException"><paramref name="str"/> contains invalid escape sequence</exception>
        ''' <remarks>This method does not allow formatting, so "{{" gets to output as "{{" and "}}" as "}}".</remarks>
        <Extension()> _
        Public Function CEscape(ByVal str As String) As String
            Return CFormat(CultureInfo.CurrentCulture, str, New Object() {}, False)
        End Function
        ''' <summary>Formats and escape string according to rules described for <see cref="StringFormatting"/></summary>
        ''' <param name="str">Formatting string</param>
        ''' <param name="Args">Objects to format</param>
        ''' <returns>String formatted</returns>
        ''' <exception cref="FormatException"><paramref name="str"/> is invalid format string</exception>
        ''' <remarks>This method uses <see cref="CultureInfo.CurrentCulture"/></remarks>
        <Extension()> _
        Public Function CFormat(ByVal str As String, ByVal ParamArray Args As Object()) As String
            Return CFormat(str, CultureInfo.CurrentCulture, Args)
        End Function
        ''' <summary>Fine State Automaton states for string formatting and escaping</summary>
        Private Enum CFormatFSA
            ''' <summary>Normal state</summary>
            [String]
            ''' <summary>{ in normal</summary>
            Open1
            ''' <summary>} in normal</summary>
            Close1
            ''' <summary>\ in normal</summary>
            Back
            ''' <summary>\x, \X, \u, \U in normal</summary>
            X
            ''' <summary>\x, \X, \u, \U and hexanumber in normal</summary>
            Xnext
            ''' <summary>\ and number in normal</summary>
            NumEscape
            ''' <summary>{0</summary>
            ArgNum
            ''' <summary>{0,</summary>
            Comma
            ''' <summary>{0,0, {0,-0</summary>
            Width
            ''' <summary>Custom format</summary>
            CustomFormat
            ''' <summary>{ in format</summary>
            cOpen1
            ''' <summary>} in format</summary>
            cClose1
            ''' <summary>{0,-</summary>
            MinusWidth
        End Enum
        ''' <summary>Internaly pefrorms the formatting process</summary>
        ''' <param name="provider">Provider providing formatting</param>
        ''' <param name="str">Formatting string</param>
        ''' <param name="args">Arguments to format</param>
        ''' <param name="format">True to do formatting and escaping, false to do escaping only</param>
        ''' <returns>String formatted</returns>
        ''' <exception cref="FormatException"><paramref name="str"/> contains invalid format string</exception>
        Private Function CFormat(ByVal provider As IFormatProvider, ByVal str As String, ByVal args As Object(), ByVal format As Boolean) As String
            If provider Is Nothing Then provider = CultureInfo.CurrentCulture
            If str = "" Then Return ""
            If args Is Nothing Then args = New Object() {}
            Dim ret As New System.Text.StringBuilder
            Dim state As CFormatFSA = CFormatFSA.String
            Dim NumEscapeValue% = 0
            Dim ArgNum% = 0
            Dim Width% = 0
            Dim WidthSign% = 1
            Dim CustomFormat As System.Text.StringBuilder = Nothing
            Dim RetState As CFormatFSA = CFormatFSA.String
            For Each ch As Char In str
SelectCase:     Select Case state
                    Case CFormatFSA.String 'Normal situation
                        Select Case ch
                            Case "{"c : If format Then state = CFormatFSA.Open1 Else ret.Append(ch)
                            Case "}"c : If format Then state = CFormatFSA.Close1 Else ret.Append(ch)
                            Case "\"c : state = CFormatFSA.Back : RetState = CFormatFSA.String
                            Case Else : ret.Append(ch)
                        End Select
                    Case CFormatFSA.Back '\
                        Dim AppendTo As System.Text.StringBuilder = IIf(RetState = CFormatFSA.String, ret, CustomFormat)
                        Select Case ch
                            Case "a"c : AppendTo.Append(ChrW(7)) : state = RetState  'Aletr
                            Case "b"c : AppendTo.Append(ChrW(8)) : state = RetState 'Backspace
                            Case "f"c : AppendTo.Append(ChrW(&HC)) : state = RetState 'Form feed
                            Case "n"c : AppendTo.Append(ChrW(&HA)) : state = RetState 'New line
                            Case "r"c : AppendTo.Append(ChrW(&HD)) : state = RetState 'Carriage return
                            Case "t"c : AppendTo.Append(ChrW(9)) : state = RetState 'Horizontal tab
                            Case "v"c : AppendTo.Append(ChrW(&HB)) : state = RetState 'Vertical tab
                            Case "." : state = RetState
                            Case "x"c, "X"c, "u"c, "U"c : state = CFormatFSA.X
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                state = CFormatFSA.NumEscape : NumEscapeValue = AscW(ch) - AscW("0"c)
                            Case Else : AppendTo.Append(ch) : state = RetState
                        End Select
                    Case CFormatFSA.NumEscape '\0 \1 ...
                        Dim AppendTo As System.Text.StringBuilder = IIf(RetState = CFormatFSA.String, ret, CustomFormat)
                        Select Case ch
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                NumEscapeValue = NumEscapeValue * 10 + AscW(ch) - AscW("0"c)
                            Case Else
                                state = RetState
                                Try
                                    AppendTo.Append(Char.ConvertFromUtf32(NumEscapeValue))
                                Catch ex As ArgumentOutOfRangeException
                                    Throw New FormatException(String.Format(ResourcesT.Exceptions.InvalidFormatStringInvalidUnicodeCodePoint0D, NumEscapeValue), ex)
                                End Try
                                GoTo SelectCase
                        End Select
                    Case CFormatFSA.X '\x \X \U \u
                        Select Case ch
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                NumEscapeValue = AscW(ch) - AscW("0"c) : state = CFormatFSA.Xnext
                            Case "a"c, "b"c, "c"c, "d"c, "e"c, "f"c
                                NumEscapeValue = AscW(ch) - AscW("a"c) + 10 : state = CFormatFSA.Xnext
                            Case "A"c, "B"c, "C"c, "D"c, "E"c, "F"c
                                NumEscapeValue = AscW(ch) - AscW("A"c) + 10 : state = CFormatFSA.Xnext
                            Case Else : Throw New FormatException(ResourcesT.Exceptions.InvalidFormatStringInvalidHexadecimalEscapeSequence)
                        End Select
                    Case CFormatFSA.Xnext '\x1 \X1 \u1 \U1 ...
                        Dim AppendTo As System.Text.StringBuilder = IIf(RetState = CFormatFSA.String, ret, CustomFormat)
                        Select Case ch
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                NumEscapeValue = NumEscapeValue * 16 + AscW(ch) - AscW("0"c)
                            Case "a"c, "b"c, "c"c, "d"c, "e"c, "f"c
                                NumEscapeValue = NumEscapeValue * 16 + AscW(ch) - AscW("a"c) + 10
                            Case "A"c, "B"c, "C"c, "D"c, "E"c, "F"c
                                NumEscapeValue = NumEscapeValue * 16 + AscW(ch) - AscW("A"c) + 10
                            Case Else
                                state = RetState
                                Try
                                    AppendTo.Append(Char.ConvertFromUtf32(NumEscapeValue))
                                Catch ex As ArgumentOutOfRangeException
                                    Throw New FormatException(String.Format(ResourcesT.Exceptions.InvalidFormatStringInvalidUnicodeCodePoint0x0X, NumEscapeValue), ex)
                                End Try
                                GoTo SelectCase
                        End Select
                    Case CFormatFSA.Close1 '}
                        Select Case ch
                            Case "}"c : ret.Append("}"c) : state = CFormatFSA.String
                            Case Else : ret.Append("}"c) : state = CFormatFSA.String : GoTo SelectCase
                        End Select
                    Case CFormatFSA.Open1 '{
                        Select Case ch
                            Case "{"c : ret.Append("{"c) : state = CFormatFSA.String
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                ArgNum = AscW(ch) - AscW("0"c) : state = CFormatFSA.ArgNum
                            Case Else : Throw New FormatException(ResourcesT.Exceptions.InvalidFormatStringArgumentNumberExpected)
                        End Select
                    Case CFormatFSA.ArgNum '{0, {1
                        Select Case ch
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                ArgNum = ArgNum * 10 + AscW(ch) - AscW("0"c)
                            Case ","c : state = CFormatFSA.Comma
                            Case ":"c : state = CFormatFSA.CustomFormat : Width = 0 : CustomFormat = New System.Text.StringBuilder
                            Case "}"c : ret.Append(FormatInternal(ArgNum, 0, Nothing, args, provider)) : state = CFormatFSA.String
                            Case Else : Throw New FormatException(ResourcesT.Exceptions.InvalidFormatStringNumeralOrExpected2)
                        End Select
                    Case CFormatFSA.Comma '{0,
                        Select Case ch
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                Width = AscW(ch) - AscW("0"c) : state = CFormatFSA.Width
                                WidthSign = +1
                            Case "-"c : WidthSign = -1 : state = CFormatFSA.MinusWidth
                            Case Else : Throw New Exception(ResourcesT.Exceptions.InvalidFormatStringExpectedWidthNumberOr)
                        End Select
                    Case CFormatFSA.MinusWidth '{0,-
                        Select Case ch
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                Width = AscW(ch) - AscW("0"c) : state = CFormatFSA.Width
                            Case Else : Throw New Exception(ResourcesT.Exceptions.InvalidFormatStringExpectedWidthNumber)
                        End Select
                    Case CFormatFSA.Width '{0,0
                        Select Case ch
                            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c
                                Width = Width * 10 + AscW(ch) - AscW("0"c)
                            Case ":"c : state = CFormatFSA.CustomFormat : CustomFormat = New System.Text.StringBuilder
                            Case "}"c : ret.Append(FormatInternal(ArgNum, Width * WidthSign, Nothing, args, provider)) : state = CFormatFSA.String
                            Case Else : Throw New FormatException(ResourcesT.Exceptions.InvalidFormatStringNumeralOrExpected)
                        End Select
                    Case CFormatFSA.CustomFormat  '{0:, {0,0:
                        Select Case ch
                            Case "{"c : state = CFormatFSA.cOpen1
                            Case "}"c : state = CFormatFSA.cClose1
                            Case "\"c : state = CFormatFSA.Back : RetState = CFormatFSA.CustomFormat
                            Case Else : CustomFormat.Append(ch)
                        End Select
                    Case CFormatFSA.cOpen1
                        Select Case ch
                            Case "{"c : CustomFormat.Append("{"c) : state = CFormatFSA.CustomFormat
                            Case Else : state = CFormatFSA.CustomFormat : CustomFormat.Append("{"c) : GoTo SelectCase
                        End Select
                    Case CFormatFSA.cClose1
                        Select Case ch
                            Case "}"c : CustomFormat.Append("}"c) : state = CFormatFSA.CustomFormat
                            Case Else : ret.Append(FormatInternal(ArgNum, Width * WidthSign, CustomFormat.ToString, args, provider)) : state = CFormatFSA.String : GoTo SelectCase
                        End Select
                End Select
            Next
            Select Case state
                Case CFormatFSA.String 'Do nothing
                Case CFormatFSA.Back
                    If RetState = CFormatFSA.String Then ret.Append("\") Else Throw New FormatException(ResourcesT.Exceptions.IncompleteFormatString)
                Case CFormatFSA.NumEscape
                    If RetState = CFormatFSA.String Then
                        Try
                            ret.Append(Char.ConvertFromUtf32(NumEscapeValue))
                        Catch ex As ArgumentOutOfRangeException
                            Throw New FormatException(String.Format(ResourcesT.Exceptions.InvalidFormatStringInvalidUnicodeCodePoint0D, NumEscapeValue), ex)
                        End Try
                    Else : Throw New FormatException(ResourcesT.Exceptions.IncompleteFormatString)
                    End If
                Case CFormatFSA.X : Throw New FormatException(ResourcesT.Exceptions.IncompleteFormatString)
                Case CFormatFSA.Xnext
                    If RetState = CFormatFSA.String Then
                        Try
                            ret.Append(Char.ConvertFromUtf32(NumEscapeValue))
                        Catch ex As ArgumentOutOfRangeException
                            Throw New FormatException(String.Format(ResourcesT.Exceptions.InvalidFormatStringInvalidUnicodeCodePoint0x0X, NumEscapeValue), ex)
                        End Try
                    Else : Throw New FormatException(ResourcesT.Exceptions.IncompleteFormatString)
                    End If
                Case CFormatFSA.Close1 : ret.Append("}"c)
                Case CFormatFSA.Open1 : ret.Append("{"c)
                Case CFormatFSA.ArgNum, CFormatFSA.Comma, CFormatFSA.MinusWidth, CFormatFSA.Width, CFormatFSA.CustomFormat, CFormatFSA.cOpen1
                    Throw New FormatException(ResourcesT.Exceptions.IncompleteFormatString)
                Case CFormatFSA.cClose1
                    ret.Append(FormatInternal(ArgNum, Width * WidthSign, CustomFormat.ToString, args, provider))
            End Select
            Return ret.ToString
        End Function
        ''' <summary>Formats object using format string, width and format provider</summary>
        ''' <param name="ArgNum">Number of argument - index to <paramref name="Args"/></param>
        ''' <param name="Width">Specifies minimal width of returned string. Negative for left align, positive for right align.</param>
        ''' <param name="format">Format string fo value</param>
        ''' <param name="Args">Arguments. Item with index <paramref name="ArgNum"/> from this array will be formatted</param>
        ''' <param name="provider">Formatting provider</param>
        ''' <returns>Formatted <paramref name="Args"/>[<paramref name="ArgNum"/>]. If argument is null an empty string is used; if it is <see cref="ICustomFormatter"/> <see cref="ICustomFormatter.Format"/> is used; if it is <see cref="IFormattable"/> <see cref="IFormattable.ToString"/> is used; otherwise <see cref="System.[Object].ToString"/>. After formatting, value if widhtened to <paramref name="Width"/>.</returns>
        ''' <exception cref="FormatException"><paramref name="ArgNum"/> is greater than or equal to <paramref name="Args"/>.<see cref="Array.Length">Length</see> -or- <paramref name="format"/> is invalid according to object being formatted.</exception>
        Private Function FormatInternal(ByVal ArgNum%, ByVal Width%, ByVal format As String, ByVal Args As Object(), ByVal provider As IFormatProvider) As String
            If ArgNum >= Args.Length Then Throw New FormatException(String.Format("Invalid format string. Required argument number {0} missing.", ArgNum))
            Dim ret As String
            If Args(ArgNum) Is Nothing Then
                ret = ""
            ElseIf TypeOf Args(ArgNum) Is ICustomFormatter Then
                ret = DirectCast(Args(ArgNum), ICustomFormatter).Format(format, Args(ArgNum), provider)
            ElseIf TypeOf Args(ArgNum) Is IFormattable Then
                ret = DirectCast(Args(ArgNum), IFormattable).ToString(format, provider)
            Else
                ret = Args(ArgNum).ToString
            End If
            If ret.Length >= Math.Abs(Width) Then
                Return ret
            ElseIf Width > 0 Then 'Right
                Return New String(" "c, Width - ret.Length) & ret
            Else 'Left
                Return ret & New String(" "c, -Width - ret.Length)
            End If
        End Function
    End Module
#End If
End Namespace
