Imports Tools.ExtensionsT
#If Config <= Nightly Then 'Stage:Nightly
Namespace GlobalizationT.NumberingSystemsT
    ''' <summary>Roman numbering system defines uppercase and lowercase roman numerals like I, X, L, C, D, M or i, x, l, c, d, m</summary>
    ''' <remarks>This roman numeral system follows rules stated at <a href="http://en.wikipedia.org/wiki/Roman_numerals#XCIX_vs._IC">Wikipedia</a> stating that shortcut numbers like IC (instead of XCIX) etc. are not allowed. This implementation neither produce such numbers nor can parse them.
    ''' <para>Speial numerals for 11 (Ⅺ or ⅺ) and 12 (Ⅻ or ⅻ) are used only in numbers like 11, 12, 111, 12, 211, 212, 511, 512, 1011, 1012 etc. not in numbers like 21, 22, 61, 62, 121, 122 etc.  - only when the "spoken" meaning is eleven or twelve.</para></remarks>
    ''' <version version="1.5.2">Class introduced</version>
    Public Class RomanNumberingSystem
        Inherits NumberingSystem
        ''' <summary>Creates instance of roman numeral numbering system with given letter for each basic numeral</summary>
        ''' <param name="I">Letter for 1</param>
        ''' <param name="V">Letter for 5</param>
        ''' <param name="X">Letter for 10</param>
        ''' <param name="L">Letter for 50</param>
        ''' <param name="C">Letter for 100</param>
        ''' <param name="D">Letter for 500</param>
        ''' <param name="M">Letter for 1000</param>
        ''' <exception cref="ArgumentException">Any two of numerals are same</exception>
        ''' <exception cref="ArgumentNullException">Any numeral is null-char</exception>
        Protected Sub New(ByVal I As Char, ByVal V As Char, ByVal X As Char, ByVal L As Char, ByVal C As Char, ByVal D As Char, ByVal M As Char)
            If I = V OrElse I = X OrElse I = L OrElse I = C OrElse I = D OrElse I = M OrElse V = X OrElse V = L OrElse V = C OrElse V = D OrElse V = M OrElse X = L OrElse X = C OrElse X = D OrElse X = M OrElse L = C OrElse L = D OrElse L = M OrElse C = D OrElse C = M OrElse D = M Then Throw New ArgumentException(ResourcesT.Exceptions.NumeralCharactersMustBeDistinct)
            If I = vbNullChar Then Throw New ArgumentException("I")
            If V = vbNullChar Then Throw New ArgumentException("V")
            If X = vbNullChar Then Throw New ArgumentException("X")
            If L = vbNullChar Then Throw New ArgumentException("L")
            If C = vbNullChar Then Throw New ArgumentException("C")
            If D = vbNullChar Then Throw New ArgumentException("D")
            If M = vbNullChar Then Throw New ArgumentException("M")
            Me.I = I : Me.V = V : Me.X = X : Me.L = L : Me.C = C : Me.D = D : Me.M = M
        End Sub
        ''' <summary>Letter for 1</summary>
        Protected ReadOnly I As Char
        ''' <summary>Letter for 5</summary>
        Protected ReadOnly V As Char
        ''' <summary>Letter for 10</summary>
        Protected ReadOnly X As Char
        ''' <summary>Letter for 50</summary>
        Protected ReadOnly L As Char
        ''' <summary>Letter for 100</summary>
        Protected ReadOnly C As Char
        ''' <summary>Letter for 500</summary>
        Protected ReadOnly D As Char
        ''' <summary>Letter for 1000</summary>
        Protected ReadOnly M As Char
        ''' <summary>Delault instance for uppercase Roman numeral numbering system (based on I, V, X, L, C, D, M)</summary>
        Public Shared ReadOnly UpperCase As New RomanNumberingSystem("I"c, "V"c, "X"c, "L"c, "C"c, "D"c, "M"c)
        ''' <summary>Default instance for lowercase Roman numeral numbering system (based on i, v, x, l, c, d, m)</summary>
        Public Shared ReadOnly LowerCase As New RomanNumberingSystem("i"c, "v"c, "x"c, "l"c, "c"c, "d"c, "m"c)

        ''' <summary>Gets representation of given number in Roman numerals numbering system</summary>
        ''' <param name="value">Number to get representation of</param>
        ''' <returns>String representation of given integral number in Roman numerals  numbering system</returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than <see cref="Minimum"/> or greater than <see cref="Maximum"/></exception>
        Public Overrides Function GetValue(ByVal value As Integer) As String
            If value < Minimum Or value > Maximum Then Throw New ArgumentOutOfRangeException("value")
            If value > 3999 Then
                Dim remaining% = value - (value \ 1000) * 1000
                Return New String(GetValue(1000), value \ 1000) & If(remaining = 0, "", GetValue(remaining))
            End If
            Select Case value
                Case 1 : Return I
                Case 5 : Return V
                Case 10 : Return X
                Case 50 : Return L
                Case 100 : Return C
                Case 500 : Return D
                Case 1000 : Return M
            End Select
            Dim Group% = Math.Floor(Math.Log10(value))
            Dim ValueSize% = 10 ^ Group 'Size of one in this decade I, X, C, M
            Dim NextGroupSize% = 10 ^ (Group + 1) 'Next decated size X, C, M
            Dim Half% = NextGroupSize / 2 'Half of this decade V, L, D
            Dim DecadeCount% = value \ ValueSize 'Number of Is, Xs, Cs or Ms
            If NextGroupSize - value <= ValueSize Then 'IX, XC, CM
                Dim remains% = value - 9 * ValueSize
                Return GetValue(ValueSize) & GetValue(NextGroupSize) & If(remains = 0, "", GetValue(remains))
            ElseIf value >= Half Then 'VI, LX, DC
                Dim remains% = value - ValueSize * DecadeCount
                Return GetValue(NextGroupSize / 2) & New String(GetValue(ValueSize), DecadeCount - 5) & If(remains = 0, "", GetValue(remains))
            ElseIf Half - value <= ValueSize Then 'IV, XL, CD
                Dim remains% = value - 4 * ValueSize
                Return GetValue(ValueSize) & GetValue(Half) & If(remains = 0, "", GetValue(remains))
            Else 'II, XX, CC, MM
                Dim remains% = value - ValueSize * DecadeCount
                Return New String(GetValue(ValueSize), DecadeCount) & If(remains = 0, "", GetValue(remains))
            End If
        End Function
        ''' <summary>Gets minimal supported number in Roman numerals numbering system</summary>
        ''' <returns>1</returns>
        Public Overrides ReadOnly Property Minimum() As Integer
            <DebuggerStepThrough()> Get
                Return 1
            End Get
        End Property
        ''' <summary>Gets maximal supported number in Roman numerals numbering system</summary>
        ''' <returns>4999</returns>
        Public Overrides ReadOnly Property Maximum() As Integer
            <DebuggerStepThrough()> Get
                Return 4999
            End Get
        End Property

        ''' <summary>Gets value indicating if parsing of string representation to integer is suported by current numbering system</summary>
        ''' <returns>True</returns>
        Public Overrides ReadOnly Property SupportsParse() As Boolean
            <DebuggerStepThrough()> Get
                Return True
            End Get
        End Property
        ''' <summary>Attempts to parse string representation of number in Roman numeral numbering system to integer</summary>
        ''' <param name="value">String representation of number to parse</param>
        ''' <param name="result">When function exists with success contains parsed value representing <paramref name="value"/> as number</param>
        ''' <returns>When parsing is successfull returns null; otherwise returns exception describing the error.
        ''' This implementation uses <see cref="OverflowException"/> when number higher than <see cref="Maximum"/> is parsed -and-
        ''' <see cref="FormatException"/> when unexpected character is reached or unexpected sequence is reached. Note that only alloved subtractings are IV, IX, XL, XC, CD and CM. -and-
        ''' <see cref="ArgumentNullException"/> whan <paramref name="value"/> is null or an empty string</returns>
        Protected Overrides Function TryParseInternal(ByVal value As String, ByRef result As Integer) As System.Exception
            If value = "" Then Return New ArgumentNullException("value")
            Dim RetVal% = 0
            Dim ipos% = 0
            Dim prev As Char = vbNullChar
            For Each ch In value
                Dim val%
                Select Case ch
                    Case I : val = 1
                    Case V : val = 5
                    Case X : val = 10
                    Case L : val = 50
                    Case C : val = 100
                    Case D : val = 500
                    Case M : val = 1000
                    Case Else : Return New FormatException(ResourcesT.Exceptions.UnexpectedCharacter0InRomanNumeral.f(ch))
                End Select
                prev = ch
                If RetVal > val Then
                    Dim iresult%
                    Dim ex = TryParseInternal(value.Substring(ipos), iresult)
                    If ex IsNot Nothing Then Return ex
                    RetVal += iresult
                    If RetVal > Me.Maximum Then Return New OverflowException(ResourcesT.Exceptions.String0RepresentsHigherNumberThanMaximum1.f(value, Maximum))
                    result = RetVal
                    Return Nothing
                ElseIf RetVal > 0 AndAlso RetVal < val Then
                    If ((val = 5 OrElse val = 10) AndAlso RetVal <> 1) OrElse _
                       ((val = 50 OrElse val = 100) AndAlso RetVal <> 10) OrElse _
                       ((val = 500 OrElse val = 1000) AndAlso RetVal <> 100) Then _
                       Throw New FormatException(ResourcesT.Exceptions.InvalidRomanNumeralSequenceInvalidSubtraction)
                    RetVal = val - RetVal
                Else
                    RetVal += val
                End If
                ipos += 1
                If RetVal > Me.Maximum Then Return New OverflowException(ResourcesT.Exceptions.String0RepresentsHigherNumberThanMaximum1.f(value, Maximum))
            Next
            result = RetVal
            Return Nothing
        End Function

        ''' <summary>Converts string representing Roman number in current Roman Numbering system to Unicode Roman number</summary>
        ''' <param name="Value">Value to be converted</param>
        ''' <param name="LowerCase">Target system. True for <see cref="RomanNumberingSystemUnicode.UpperCase"/>, false for <see cref="RomanNumberingSystemUnicode.UpperCase"/>.</param>
        ''' <returns><paramref name="Value"/> converted from current Roman numerals numbering system to target one identified by <paramref name="LowerCase"/>.</returns>
        ''' <remarks>All unknown characters are returned unchanged. Resulting number is collapsed.
        ''' <para>This function is string-based and works even on numbers that are otherwise invalid.</para></remarks>
        ''' <seelaso cref="Convert"/><seelaso cref="RomanNumberingSystemUnicode.Collapse"/>
        Public Overridable Function ToUnicode(ByVal Value As String, ByVal LowerCase As Boolean) As String
            If Value Is Nothing Then Return Nothing
            Dim target = If(LowerCase, RomanNumberingSystemUnicode.LowerCase, RomanNumberingSystemUnicode.UpperCase)
            Return target.Collapse(CharacterwiseConvert(Value, target))
        End Function
        ''' <summary>Converts value represented in current Roman numbering system to another Roman numbering system</summary>
        ''' <param name="value">Value to be converted. Represents number in curent Roman numbering system.</param>
        ''' <param name="Target">Target Roman numbering system to convert <paramref name="value"/> to. When target is Roman numbering system that defines additional numerals (like <see cref="RomanNumberingSystemUnicode"/>), those additional numerals are ignored during conversion. Use <see cref="ToUnicode"/> fro converting to standard Unicode Roman number.</param>
        ''' <returns><paramref name="value"/> with Roman numerals characters for current Roman numbering system replaced by Roman numerals from <paramref name="Target"/>; null when <paramref name="value"/> is null.</returns>
        ''' <remarks>Roman numerals are defined by <see cref="I"/>, <see cref="V"/>, <see cref="X"/>, <see cref="L"/>, <see cref="C"/>, <see cref="D"/> and <see cref="M"/>. Unknown characters are returned unchanged.
        ''' <note type="inheritinfo">When class derived from <see cref="RomanNumberingSystem"/> defines additional numerals (i.e. as <see cref="RomanNumberingSystemUnicode"/> it should override this method to provide correct replacing.</note>
        ''' <para>This function is string-based and works even on numbers that are otherwise invalid.</para></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Target"/> is null</exception>
        ''' <seelaso cref="ToUnicode"/>
        Public Overridable Function CharacterwiseConvert(ByVal value As String, ByVal Target As RomanNumberingSystem) As String
            If Target Is Nothing Then Throw New ArgumentNullException("Target")
            If value Is Nothing Then Return Nothing
            Dim ret As New System.Text.StringBuilder(value.Length)
            For Each ch In value
                Select Case ch
                    Case I : ret.Append(Target.I)
                    Case V : ret.Append(Target.V)
                    Case X : ret.Append(Target.X)
                    Case L : ret.Append(Target.L)
                    Case C : ret.Append(Target.C)
                    Case D : ret.Append(Target.D)
                    Case M : ret.Append(Target.M)
                    Case Else : ret.Append(ch)
                End Select
            Next
            Return ret.ToString
        End Function
    End Class
    ''' <summary>Roman numbering system based on Unicode character instead of latin letters</summary>
    ''' <remarks>Unicode characters differs in such way that they have single code-points for II, III, IV, VI, VII, VII IX, XI, and XII.</remarks>
    ''' <version version="1.5.2">Class introduced</version>
    Public Class RomanNumberingSystemUnicode
        Inherits RomanNumberingSystem
        ''' <summary>Creates instance of roman numeral numbering system with given letter for each basic numeral</summary>
        ''' <param name="I">Letter for 1</param>
        ''' <param name="II">Letter for 2</param>
        ''' <param name="III">Letter for 3</param>
        ''' <param name="IV">Letter for 4</param>
        ''' <param name="V">Letter for 5</param>
        ''' <param name="VI">Letter for 6</param>
        ''' <param name="VII">Letter for 7</param>
        ''' <param name="VIII">Letter for 8</param>
        ''' <param name="IX">Letter for 9</param>
        ''' <param name="X">Letter for 10</param>
        ''' <param name="XI">Leter for 11</param>
        ''' <param name="XII">Letter for 12</param>
        ''' <param name="L">Letter for 50</param>
        ''' <param name="C">Letter for 100</param>
        ''' <param name="D">Letter for 500</param>
        ''' <param name="M">Letter for 1000</param>
        ''' <exception cref="ArgumentException">Any two of numerals are same</exception>
        ''' <exception cref="ArgumentNullException">Any numeral is null-char</exception>
        Protected Sub New(ByVal I As Char, ByVal II As Char, ByVal III As Char, ByVal IV As Char, ByVal V As Char, ByVal VI As Char, ByVal VII As Char, ByVal VIII As Char, ByVal IX As Char, ByVal X As Char, ByVal XI As Char, ByVal XII As Char, ByVal L As Char, ByVal C As Char, ByVal D As Char, ByVal M As Char)
            MyBase.New(I, V, X, L, C, D, M)
            If II = I OrElse II = V OrElse II = X OrElse II = L OrElse II = C OrElse II = D OrElse II = M OrElse _
                    III = I OrElse III = V OrElse III = X OrElse III = L OrElse III = C OrElse III = D OrElse III = M OrElse _
                    IV = I OrElse IV = V OrElse IV = X OrElse IV = L OrElse IV = C OrElse IV = D OrElse IV = M OrElse _
                    VI = I OrElse VI = V OrElse VI = X OrElse VI = L OrElse VI = C OrElse VI = D OrElse VI = M OrElse _
                    VII = I OrElse VII = V OrElse VII = X OrElse VII = L OrElse VII = C OrElse VII = D OrElse VII = M OrElse _
                    VIII = I OrElse VIII = V OrElse VIII = X OrElse VIII = L OrElse VIII = C OrElse VIII = D OrElse VIII = M OrElse _
                    IX = I OrElse IX = V OrElse IX = X OrElse IX = L OrElse IX = C OrElse IX = D OrElse IX = M OrElse _
                    XI = I OrElse XI = V OrElse XI = X OrElse XI = L OrElse XI = C OrElse XI = D OrElse XI = M OrElse _
                    XII = I OrElse XII = V OrElse XII = X OrElse XII = L OrElse XII = C OrElse XII = D OrElse XII = M OrElse _
                    II = III OrElse II = IV OrElse II = VI OrElse II = VII OrElse II = VIII OrElse II = IX OrElse II = XI OrElse II = XII OrElse _
                    III = IV OrElse III = VI OrElse III = VII OrElse III = VIII OrElse III = IX OrElse III = XI OrElse III = XII OrElse _
                    IV = VI OrElse IV = VII OrElse IV = VIII OrElse IV = IX OrElse IV = XI OrElse IV = XII OrElse _
                    VI = VII OrElse VI = VIII OrElse VI = XI OrElse VI = XII OrElse _
                    VII = VIII OrElse VII = IX OrElse VII = XI OrElse VII = XII OrElse _
                    VIII = XI OrElse VIII = XII OrElse _
                    IX = XI OrElse IX = XII OrElse _
                    XI = XII Then _
                Throw New ArgumentException(ResourcesT.Exceptions.NumeralCharactersMustBeDistinct)
            If II = vbNullChar Then Throw New ArgumentNullException("II")
            If III = vbNullChar Then Throw New ArgumentNullException("III")
            If IV = vbNullChar Then Throw New ArgumentNullException("IV")
            If VI = vbNullChar Then Throw New ArgumentNullException("VI")
            If VII = vbNullChar Then Throw New ArgumentNullException("VII")
            If VIII = vbNullChar Then Throw New ArgumentNullException("VIII")
            If IX = vbNullChar Then Throw New ArgumentNullException("IX")
            If XI = vbNullChar Then Throw New ArgumentNullException("XI")
            If XII = vbNullChar Then Throw New ArgumentNullException("XII")
            Me.II = II : Me.III = III : Me.IV = IV : Me.VI = VI : Me.VII = VII : Me.VIII = VIII : Me.IX = IX : Me.XI = XI : Me.XII = XII
        End Sub
        ''' <summary>Leter for 2</summary>
        Protected ReadOnly II As Char
        ''' <summary>Leter for 3</summary>
        Protected ReadOnly III As Char
        ''' <summary>Letter for 4</summary>
        Protected ReadOnly IV As Char
        ''' <summary>Letter for 6</summary>      
        Protected ReadOnly VI As Char
        ''' <summary>Leter for 7</summary>
        Protected ReadOnly VII As Char
        ''' <summary>Leter for 8</summary>
        Protected ReadOnly VIII As Char
        ''' <summary>Leter for 9</summary>
        Protected ReadOnly IX As Char
        ''' <summary>Leter for 11</summary>
        Protected ReadOnly XI As Char
        ''' <summary>Leter for 12</summary>
        Protected ReadOnly XII As Char
        ''' <summary>Delault instance for uppercase Roman numeral numbering system (based on Ⅰ, Ⅱ, Ⅲ, Ⅳ, Ⅴ, Ⅵ, Ⅶ, Ⅷ, Ⅸ, Ⅹ, Ⅺ, Ⅻ, Ⅼ, Ⅽ, Ⅾ, Ⅿ)</summary>
        Public Shared Shadows ReadOnly UpperCase As New RomanNumberingSystemUnicode("Ⅰ"c, "Ⅱ"c, "Ⅲ"c, "Ⅳ"c, "Ⅴ"c, "Ⅵ"c, "Ⅶ"c, "Ⅷ"c, "Ⅸ"c, "Ⅹ"c, "Ⅺ"c, "Ⅻ"c, "Ⅼ"c, "Ⅽ"c, "Ⅾ"c, "Ⅿ"c)
        ''' <summary>Default instance for lowercase Roman numeral numbering system (based on ⅰ, ⅱ, ⅲ, ⅳ, ⅴ, ⅵ, ⅶ, ⅷ, ⅸ, ⅹ, ⅺ, ⅻ, ⅼ, ⅽ,ⅾ, ⅿ)</summary>
        Public Shared Shadows ReadOnly LowerCase As New RomanNumberingSystemUnicode("ⅰ"c, "ⅱ"c, "ⅲ"c, "ⅳ"c, "ⅴ"c, "ⅵ"c, "ⅶ"c, "ⅷ"c, "ⅸ"c, "ⅹ"c, "ⅺ"c, "ⅻ"c, "ⅼ"c, "ⅽ"c, "ⅾ"c, "ⅿ"c)

        ''' <summary>Gets representation of given number in Unicode Roman numerals numbering system</summary>
        ''' <param name="value">Number to get representation of</param>
        ''' <returns>String representation of given integral number in Roman numerals  numbering system</returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than <see cref="Minimum"/> or greater than <see cref="Maximum"/></exception>
        Public Overrides Function GetValue(ByVal value As Integer) As String
            Select Case value
                Case 2 : Return II
                Case 3 : Return III
                Case 4 : Return IV
                Case 6 : Return VI
                Case 7 : Return VII
                Case 8 : Return VIII
                Case 9 : Return IX
                Case 11 : Return XI
                Case 12 : Return XII
                Case Else : Return MyBase.GetValue(value)
            End Select
        End Function

        ''' <summary>Gets value indicating if parsing of string representation to integer is suported by current numbering system</summary>
        ''' <returns>True</returns>
        Public Overrides ReadOnly Property SupportsParse() As Boolean
            Get
                Return True
            End Get
        End Property

        ''' <summary>Attempts to parse string representation of number in Roman numeral numbering system to integer</summary>
        ''' <param name="value">String representation of number to parse</param>
        ''' <param name="result">When function exists with success contains parsed value representing <paramref name="value"/> as number</param>
        ''' <returns>When parsing is successfull returns null; otherwise returns exception describing the error.
        ''' This implementation uses <see cref="OverflowException"/> when number higher than <see cref="Maximum"/> is parsed -and-
        ''' <see cref="FormatException"/> when unexpected character is reached or unexpected sequence is reached. Note that only alloved subtractings are IV, IX, XL, XC, CD and CM. -and-
        ''' <see cref="ArgumentNullException"/> whan <paramref name="value"/> is null or an empty string</returns>
        Protected Overrides Function TryParseInternal(ByVal value As String, ByRef result As Integer) As System.Exception
            Return MyBase.TryParseInternal(Expand(value), result)
        End Function
        ''' <summary>Expands all roman numerals ligatures to singe-glyph characters</summary>
        ''' <param name="Ligatured">String representing roman number containing ligature</param>
        ''' <returns>String with al roman numerals ligatures from <paramref name="Ligatured"/> expanded; null wne <paramref name="Ligatured"/> is null.</returns>
        ''' <remarks>Recognized ligatures are values of <see cref="II"/>, <see cref="III"/>, <see cref="III"/>, <see cref="IV"/>, <see cref="VI"/>, <see cref="VII"/>, <see cref="VIII"/> <see cref="IX"/>, <see cref="XI"/> and <see cref="XII"/>. All other characters remain unchanged.
        ''' <para>Note for inheritors: This function is caled by <see cref="TryParseInternal"/> before it calls <see cref="RomanNumberingSystem.TryParseInternal"/>.</para></remarks>
        Public Overridable Function Expand(ByVal Ligatured As String) As String
            If Ligatured Is Nothing Then Return Nothing
            Dim ret As New System.Text.StringBuilder(Ligatured.Length)
            For Each ch As Char In Ligatured
                Select Case ch
                    Case II : ret.Append(I & I)
                    Case III : ret.Append(I & I & I)
                    Case IV : ret.Append(I & V)
                    Case VI : ret.Append(V & I)
                    Case VII : ret.Append(V & I & I)
                    Case VIII : ret.Append(V & I & I & I)
                    Case IX : ret.Append(I & X)
                    Case XI : ret.Append(X & I)
                    Case XII : ret.Append(X & I & I)
                    Case Else : ret.Append(ch)
                End Select
            Next
            Return ret.ToString
        End Function
        ''' <summary>States of FSA implementing the <see cref="CollapseState"/> function</summary>
        Private Enum CollapseState
            ''' <summary>Default state</summary>
            S
            ''' <summary>I found</summary>
            I
            ''' <summary>II found</summary>
            II
            ''' <summary>III found</summary>
            III
            ''' <summary>more Is found</summary>
            IIII
            ''' <summary>X found</summary>
            X
            ''' <summary>XI found</summary>
            XI
            ''' <summary>XII found</summary>
            XII
            ''' <summary>L found</summary>
            L
            ''' <summary>V found</summary>
            V
            ''' <summary>VI found</summary>
            VI
            ''' <summary>VII found</summary>
            VII
            ''' <summary>VIII found</summary>
            VIII
            ''' <summary>Do not convert VI, VII, VIII</summary>
            VV
            ''' <summary>Do not convert XI, XII</summary>
            XX
        End Enum
        ''' <summary>Collapses expanded roman sring - replaces group of characters with appropriate ligatures when suitable</summary>
        ''' <param name="Expanded">String containing some expanded numbers (II, III, IV, VI, VII, VIII, IX, XI, XII)</param>
        ''' <returns><paramref name="Expanded"/> with expanded numbers represented by multiple characters collapsed to approriate ligatures; null when <paramref name="Expanded"/> is null.</returns>
        ''' <remarks>Ligatures are difined by <see cref="II"/>, <see cref="III"/>, <see cref="III"/>, <see cref="IV"/>, <see cref="VI"/>, <see cref="VII"/>, <see cref="VIII"/> <see cref="IX"/>, <see cref="XI"/> and <see cref="XII"/>
        ''' <para>Numbers in certain positions are not collapsed - like XXII, LXII etc. Numbers like XIX and XIV are collapsed to ⅩⅨ and ⅩⅣ respectivelly.</para>
        ''' <para>All unrecognized characters are left unchanged.</para>
        ''' <para>In rare cases <see cref="Collapse"/> may result to expansion. E.g. ⅩⅪ is "collapsed" as ⅩⅩⅠ.</para>
        ''' <para>Note for inheritors: This function is not called by internal number-to-string or string-to-number logic of <see cref="RomanNumberingSystemUnicode"/> class.</para></remarks>
        Public Overridable Function Collapse(ByVal Expanded As String) As String
            If Expanded Is Nothing Then Return Nothing
            Expanded = Expand(Expanded)
            Dim state As CollapseState = CollapseState.S
            Dim ret As New Text.StringBuilder(Expanded.Length)
            For idx As Integer = 0 To Expanded.Length
                Dim curch = If(idx = Expanded.Length, ChrW(0), Expanded(idx))
                Dim islast = idx = Expanded.Length
                Select Case state
                    Case CollapseState.S '<default>
                        Select Case curch
                            Case I : state = CollapseState.I
                            Case V : state = CollapseState.V
                            Case L : state = CollapseState.L : ret.Append(L)
                            Case X : state = CollapseState.X
                            Case Else : If Not islast Then ret.Append(curch)
                        End Select
                    Case CollapseState.I 'I
                        Select Case curch
                            Case V : ret.Append(IV) : state = CollapseState.VV
                            Case X : ret.Append(IX) : state = CollapseState.XX
                            Case I : state = CollapseState.II
                            Case Else : ret.Append(I) : idx -= 1 : state = CollapseState.S
                        End Select
                    Case CollapseState.II 'II
                        Select Case curch
                            Case I : state = CollapseState.III
                            Case Else : ret.Append(II) : idx -= 1 : state = CollapseState.S
                        End Select
                    Case CollapseState.III 'III
                        Select Case curch
                            Case I : ret.Append(I & I & I) : state = CollapseState.IIII
                            Case Else : ret.Append(III) : state = CollapseState.S : idx -= 1
                        End Select
                    Case CollapseState.IIII 'IIII, VIIII, XIII
                        Select Case curch
                            Case I : ret.Append(I)
                            Case Else : idx -= 1 : state = CollapseState.S
                        End Select
                    Case CollapseState.X 'X
                        Select Case curch
                            Case X : state = CollapseState.XX : ret.Append(X & X)
                            Case I : state = CollapseState.XI
                            Case Else : state = CollapseState.S : ret.Append(X) : idx -= 1
                        End Select
                    Case CollapseState.XI 'XI
                        Select Case curch
                            Case V : ret.Append(X & IV) : state = CollapseState.S
                            Case X : ret.Append(X & IX) : state = CollapseState.XX
                            Case I : state = CollapseState.XII
                            Case Else : ret.Append(XI) : idx -= 1 : state = CollapseState.S
                        End Select
                    Case CollapseState.XII 'XII
                        Select Case curch
                            Case I : ret.Append(X) : state = CollapseState.III
                            Case Else : ret.Append(XII) : state = CollapseState.S : idx -= 1
                        End Select
                    Case CollapseState.L  'L
                        Select Case curch
                            Case X : ret.Append(X) : state = CollapseState.XX
                            Case V : state = CollapseState.V
                            Case I : state = CollapseState.I
                            Case Else : state = CollapseState.S : idx -= 1
                        End Select
                    Case CollapseState.V 'V
                        Select Case curch
                            Case I : state = CollapseState.VI
                            Case Else : idx -= 1 : ret.Append(V) : state = CollapseState.VV
                        End Select
                    Case CollapseState.VI 'VI
                        Select Case curch
                            Case I : state = CollapseState.VII
                            Case Else : idx -= 1 : ret.Append(VI) : state = CollapseState.S
                        End Select
                    Case CollapseState.VII 'VII
                        Select Case curch
                            Case I : state = CollapseState.VIII
                            Case Else : idx -= 1 : ret.Append(VII) : state = CollapseState.S
                        End Select
                    Case CollapseState.VIII 'VIII
                        Select Case curch
                            Case I : ret.Append(V & I & I & I & I) : state = CollapseState.IIII
                            Case Else : idx -= 1 : ret.Append(VIII) : state = CollapseState.S
                        End Select
                    Case CollapseState.VV 'IV, VV
                        Select Case curch
                            Case V : ret.Append(V)
                            Case Else : state = CollapseState.S : idx -= 1
                        End Select
                    Case CollapseState.XX 'IX, XX, LX
                        Select Case curch
                            Case X : ret.Append(X)
                            Case Else : state = CollapseState.S : idx -= 1
                        End Select
                    Case Else : Throw New ApplicationException(ResourcesT.Exceptions.UnknownState0.f(state))
                End Select
            Next
            Return ret.ToString
        End Function

        ''' <summary>Converts value represented in current Roman numbering system to another Roman numbering system</summary>
        ''' <param name="value">Value to be converted. Represents number in curent Roman numbering system.</param>
        ''' <param name="Target">Target Roman numbering system to convert <paramref name="value"/> to. When target is Roman numbering system that defines additional numerals (those not present in <see cref="RomanNumberingSystemUnicode"/> if <paramref name="Target"/> derives from <see cref="RomanNumberingSystemUnicode"/> or those not present in <see cref="RomanNumberingSystem"/> if <paramref name="Target"/> does  not derive from <see cref="RomanNumberingSystemUnicode"/>), those additional numerals are ignored during conversion. When tagret is not or does not derive from <see cref="RomanNumberingSystemUnicode"/> <paramref name="value"/> is <see cref="Expand">expanded</see> before conversion.</param>
        ''' <returns><paramref name="value"/> with Roman numerals characters for current Roman numbering system replaced by Roman numerals from <paramref name="Target"/>; null when <paramref name="value"/> is null.</returns>
        ''' <remarks>Roman numerals are defined by <see cref="I"/>, <see cref="V"/>, <see cref="X"/>, <see cref="L"/>, <see cref="C"/>, <see cref="D"/> and <see cref="M"/> and <see cref="II"/>, <see cref="III"/>, <see cref="IV"/>, <see cref="VI"/>, <see cref="VII"/>, <see cref="VIII"/>, <see cref="IX"/>, <see cref="XI"/> and <see cref="XII"/>. Unknown characters are returned unchanged.
        ''' <note type="inheritinfo">When class derived from <see cref="RomanNumberingSystemUnicode"/> defines additional numerals (not present in <see cref="RomanNumberingSystemUnicode"/>) it should override this method to provide correct replacing.</note>
        ''' <para>This function is string-based and works even on numbers that are otherwise invalid.</para></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Target"/> is null</exception>
        Public Overrides Function CharacterwiseConvert(ByVal value As String, ByVal Target As RomanNumberingSystem) As String
            If Target Is Nothing Then Throw New ArgumentNullException("Target")
            If TypeOf Target Is RomanNumberingSystemUnicode Then
                If value Is Nothing Then Return Nothing
                Dim ret As New System.Text.StringBuilder(value.Length)
                Dim t2 As RomanNumberingSystemUnicode = Target
                For Each ch In value
                    Select Case ch
                        Case I : ret.Append(t2.I)
                        Case II : ret.Append(t2.II)
                        Case III : ret.Append(t2.III)
                        Case IV : ret.Append(t2.IV)
                        Case V : ret.Append(t2.V)
                        Case VI : ret.Append(t2.VI)
                        Case VII : ret.Append(t2.VII)
                        Case VIII : ret.Append(t2.VIII)
                        Case IX : ret.Append(t2.IX)
                        Case X : ret.Append(t2.X)
                        Case XI : ret.Append(t2.XI)
                        Case XII : ret.Append(t2.XII)
                        Case L : ret.Append(t2.L)
                        Case C : ret.Append(t2.C)
                        Case D : ret.Append(t2.D)
                        Case M : ret.Append(t2.M)
                        Case Else : ret.Append(ch)
                    End Select
                Next
                Return ret.ToString
            Else
                Return MyBase.CharacterwiseConvert(Expand(value), Target)
            End If
        End Function
        ''' <summary>Converts string representing Roman number in current Roman Numbering system to Unicode Roman number</summary>
        ''' <param name="Value">Value to be converted</param>
        ''' <param name="LowerCase">Target system. True for <see cref="RomanNumberingSystemUnicode.UpperCase"/>, false for <see cref="RomanNumberingSystemUnicode.UpperCase"/>.</param>
        ''' <returns><paramref name="Value"/> converted from current Roman numerals numbering system to target one identified by <paramref name="LowerCase"/>.</returns>
        ''' <remarks>All unknown characters are returned unchanged. Resulting number is collapsed.
        ''' <para>This function is string-based and works even on numbers that are otherwise invalid.</para></remarks>
        ''' <seelaso cref="Convert"/><seelaso cref="RomanNumberingSystemUnicode.Collapse"/>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public Overrides Function ToUnicode(ByVal Value As String, ByVal LowerCase As Boolean) As String
            Return CharacterwiseConvert(Value, If(LowerCase, RomanNumberingSystemUnicode.LowerCase, RomanNumberingSystemUnicode.UpperCase))
        End Function
        ''' <summary>Converts value represented in curent Unicode Roman numbering system to number in Latin-aplphabet-based Roman numbering system</summary>
        ''' <param name="value">Value in current Roman numbering system to be converted</param>
        ''' <param name="LowerCase">Target system. True for <see cref="RomanNumberingSystem.LowerCase"/>, false for <see cref="RomanNumberingSystem.UpperCase"/>.</param>
        ''' <returns><paramref name="value"/> represented in target Latin-alphabet-based Roman numbering system</returns>
        ''' <remarks>Characters that have not meaning in current Roman nnumbering system are pased to reaturn value without any change.
        ''' <para>This function is string-based and works even on numbers that are otherwise invalid.</para></remarks>
        Public Overridable Function ToLatin(ByVal value As String, ByVal LowerCase As Boolean) As String
            Return CharacterwiseConvert(Expand(value), If(LowerCase, RomanNumberingSystem.LowerCase, RomanNumberingSystem.UpperCase))
        End Function
    End Class

    ''' <summary>Roman umbering system based on Unicode characters with support for numbers up to 4999999</summary>
    Public NotInheritable Class RomanNumberingSystemBig
        Inherits RomanNumberingSystemUnicode
        ''' <summary>Creates instance of roman numeral numbering system with given letter for each basic numeral</summary>
        ''' <param name="I">Letter for 1</param>
        ''' <param name="II">Letter for 2</param>
        ''' <param name="III">Letter for 3</param>
        ''' <param name="IV">Letter for 4</param>
        ''' <param name="V">Letter for 5</param>
        ''' <param name="VI">Letter for 6</param>
        ''' <param name="VII">Letter for 7</param>
        ''' <param name="VIII">Letter for 8</param>
        ''' <param name="IX">Letter for 9</param>
        ''' <param name="X">Letter for 10</param>
        ''' <param name="XI">Leter for 11</param>
        ''' <param name="XII">Letter for 12</param>
        ''' <param name="L">Letter for 50</param>
        ''' <param name="C">Letter for 100</param>
        ''' <param name="D">Letter for 500</param>
        ''' <param name="M">Letter for 1000</param>
        ''' <exception cref="ArgumentException">Any two of numerals are same -or- <paramref name="ThousandMultiplySuffix"/> is same as any of neumerals.</exception>
        ''' <exception cref="ArgumentNullException">Any numeral is null-char -or- <paramref name="ThousandMultiplySuffix"/> is nullchar</exception>
        Protected Sub New(ByVal I As Char, ByVal II As Char, ByVal III As Char, ByVal IV As Char, ByVal V As Char, ByVal VI As Char, ByVal VII As Char, ByVal VIII As Char, ByVal IX As Char, ByVal X As Char, ByVal XI As Char, ByVal XII As Char, ByVal L As Char, ByVal C As Char, ByVal D As Char, ByVal M As Char, ByVal ThousandMultiplySuffix As Char)
            MyBase.New(I, II, III, IV, V, VI, VII, VIII, IX, X, XI, XII, L, C, D, M)
            If ThousandMultiplySuffix = vbNullChar Then Throw New ArgumentNullException("ThousandMultiplySuffix")
            If ThousandMultiplySuffix = I OrElse ThousandMultiplySuffix = II OrElse ThousandMultiplySuffix = III OrElse ThousandMultiplySuffix = IV OrElse ThousandMultiplySuffix = V OrElse ThousandMultiplySuffix = VI OrElse ThousandMultiplySuffix = VII OrElse ThousandMultiplySuffix = VIII OrElse ThousandMultiplySuffix = IX OrElse ThousandMultiplySuffix = X OrElse ThousandMultiplySuffix = XI OrElse ThousandMultiplySuffix = XII OrElse ThousandMultiplySuffix = L OrElse ThousandMultiplySuffix = C OrElse ThousandMultiplySuffix = D OrElse ThousandMultiplySuffix = M Then _
                Throw New ArgumentException(ResourcesT.Exceptions.ThousandMultiplicationSuffixMustDifferFromNumerals, "ThousandMultiplySuffix")
            Me.ThousandMultiplySuffix = ThousandMultiplySuffix
        End Sub
        ''' <summary>Thousand multiplication suffix of character</summary>
        Protected ReadOnly ThousandMultiplySuffix As Char

        ''' <summary>Delault instance for uppercase Roman numeral numbering system (based on Ⅰ, Ⅱ, Ⅲ, Ⅳ, Ⅴ, Ⅵ, Ⅶ, Ⅷ, Ⅸ, Ⅹ, Ⅺ, Ⅻ, Ⅼ, Ⅽ, Ⅾ, Ⅿ)</summary>
        Public Shared Shadows ReadOnly UpperCase As New RomanNumberingSystemBig("Ⅰ"c, "Ⅱ"c, "Ⅲ"c, "Ⅳ"c, "Ⅴ"c, "Ⅵ"c, "Ⅶ"c, "Ⅷ"c, "Ⅸ"c, "Ⅹ"c, "Ⅺ"c, "Ⅻ"c, "Ⅼ"c, "Ⅽ"c, "Ⅾ"c, "Ⅿ"c, ChrW(&H305))
        ''' <summary>Default instance for lowercase Roman numeral numbering system (based on ⅰ, ⅱ, ⅲ, ⅳ, ⅴ, ⅵ, ⅶ, ⅷ, ⅸ, ⅹ, ⅺ, ⅻ, ⅼ, ⅽ,ⅾ, ⅿ)</summary>
        Public Shared Shadows ReadOnly LowerCase As New RomanNumberingSystemBig("ⅰ"c, "ⅱ"c, "ⅲ"c, "ⅳ"c, "ⅴ"c, "ⅵ"c, "ⅶ"c, "ⅷ"c, "ⅸ"c, "ⅹ"c, "ⅺ"c, "ⅻ"c, "ⅼ"c, "ⅽ"c, "ⅾ"c, "ⅿ"c, ChrW(&H305))

        ''' <summary>Gets representation of given number in Unicode Roman numerals numbering system</summary>
        ''' <param name="value">Number to get representation of</param>
        ''' <returns>String representation of given integral number in Roman numerals  numbering system</returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than <see cref="Minimum"/> or greater than <see cref="Maximum"/></exception>
        Public Overrides Function GetValue(ByVal value As Integer) As String
            If value < Minimum Or value > Maximum Then Throw New ArgumentOutOfRangeException("value")
            Select Case value
                Case 1 : Return I
                Case 2 : Return II
                Case 3 : Return III
                Case 4 : Return IV
                Case 5 : Return V
                Case 6 : Return VI
                Case 7 : Return VII
                Case 8 : Return VIII
                Case 9 : Return IX
                Case 10 : Return X
                Case 11 : Return XI
                Case 12 : Return XII
                Case 50 : Return L
                Case 100 : Return C
                Case 500 : Return D
                Case 1000 : Return M
                Case 5000 : Return V & ThousandMultiplySuffix
                Case 10000 : Return X & ThousandMultiplySuffix
                Case 50000 : Return L & ThousandMultiplySuffix
                Case 100000 : Return C & ThousandMultiplySuffix
                Case 500000 : Return D & ThousandMultiplySuffix
                Case 1000000 : Return M & ThousandMultiplySuffix
            End Select
            Dim remain = value
            Dim ret As New System.Text.StringBuilder
            Do While remain > 0
                If remain <= 12 Then ret.Append(GetValue(remain)) : remain = 0 : Continue Do
                Dim Decade = 10 ^ Math.Floor(Math.Log10(remain))
                Dim DecadeUnit = remain \ Decade
                Dim current = DecadeUnit * Decade
                If remain >= 4000000 Then
                    Dim val = GetValue(1000000)
                    For i As Integer = 0 To DecadeUnit - 1
                        ret.Append(val)
                    Next
                    remain -= remain \ 1000000
                    Continue Do
                End If
                remain = remain - current
                If DecadeUnit = 1 OrElse DecadeUnit = 5 Then
                    ret.Append(GetValue(current))
                ElseIf DecadeUnit < 4 Then
                    Dim val = GetValue(Decade)
                    For i As Integer = 0 To DecadeUnit - 1
                        ret.Append(val)
                    Next
                ElseIf DecadeUnit = 4 Then
                    ret.Append(GetValue(Decade) & GetValue(5 * Decade))
                ElseIf DecadeUnit < 9 Then
                    ret.Append(GetValue(5 * Decade))
                    Dim val = GetValue(Decade)
                    For i As Integer = 0 To DecadeUnit - 5 - 1
                        ret.Append(val)
                    Next
                Else 'DecadeUnit=9
                    ret.Append(GetValue(Decade) & GetValue(10 * Decade))
                End If
            Loop
            Return ret.ToString
        End Function
        ''' <summary>Replaces numerals bigger than 1000 with appropriate number of <see cref="M">Ms</see></summary>
        ''' <param name="value">Value to perform replacemet in</param>
        ''' <returns>Value with suffixed numbers replaced by many <see cref="M">Ms</see>; null when <paramref name="value"/> is null.</returns>
        Private Function ExpandThousands(ByVal value As String) As String
            If value Is Nothing Then Return Nothing
            Return value.Replace(V & ThousandMultiplySuffix, New String(M, 5)) _
                .Replace(X & ThousandMultiplySuffix, New String(M, 10)) _
                .Replace(L & ThousandMultiplySuffix, New String(M, 50)) _
                .Replace(C & ThousandMultiplySuffix, New String(M, 100)) _
                .Replace(D & ThousandMultiplySuffix, New String(M, 500)) _
                .Replace(M & ThousandMultiplySuffix, New String(M, 1000))
        End Function

        ''' <summary>Converts value represented in current Roman numbering system to another Roman numbering system</summary>
        ''' <param name="value">Value to be converted. Represents number in curent Roman numbering system.</param>
        ''' <param name="Target">Target Roman numbering system to convert <paramref name="value"/> to. When target is Roman numbering system that defines additional numerals (those not present in <see cref="RomanNumberingSystemUnicode"/> if <paramref name="Target"/> derives from <see cref="RomanNumberingSystemUnicode"/> or those not present in <see cref="RomanNumberingSystem"/> if <paramref name="Target"/> does  not derive from <see cref="RomanNumberingSystemUnicode"/>), those additional numerals are ignored during conversion. When tagret is not or does not derive from <see cref="RomanNumberingSystemUnicode"/> <paramref name="value"/> is <see cref="Expand">expanded</see> before conversion.</param>
        ''' <returns><paramref name="value"/> with Roman numerals characters for current Roman numbering system replaced by Roman numerals from <paramref name="Target"/>; null when <paramref name="value"/> is null.</returns>
        ''' <remarks>Roman numerals are defined by <see cref="I"/>, <see cref="V"/>, <see cref="X"/>, <see cref="L"/>, <see cref="C"/>, <see cref="D"/> and <see cref="M"/> and <see cref="II"/>, <see cref="III"/>, <see cref="IV"/>, <see cref="VI"/>, <see cref="VII"/>, <see cref="VIII"/>, <see cref="IX"/>, <see cref="XI"/> and <see cref="XII"/>. Unknown characters are returned unchanged.
        ''' <note type="inheritinfo">When class derived from <see cref="RomanNumberingSystemUnicode"/> defines additional numerals (not present in <see cref="RomanNumberingSystemUnicode"/>) it should override this method to provide correct replacing.</note>
        ''' <para>This function is string-based and works even on numbers that are otherwise invalid.</para></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="Target"/> is null</exception>
        Public Overrides Function CharacterwiseConvert(ByVal value As String, ByVal Target As RomanNumberingSystem) As String
            If TypeOf Target Is RomanNumberingSystemBig Then
                Return MyBase.CharacterwiseConvert(value.Replace(ThousandMultiplySuffix, DirectCast(Target, RomanNumberingSystemBig).ThousandMultiplySuffix), Target)
            Else
                Return MyBase.CharacterwiseConvert(ExpandThousands(value), Target)
            End If
        End Function
        ''' <summary>Converts value represented in curent Unicode Roman numbering system to number in Latin-aplphabet-based Roman numbering system</summary>
        ''' <param name="value">Value in current Roman numbering system to be converted</param>
        ''' <param name="LowerCase">Target system. True for <see cref="RomanNumberingSystem.LowerCase"/>, false for <see cref="RomanNumberingSystem.UpperCase"/>.</param>
        ''' <returns><paramref name="value"/> represented in target Latin-alphabet-based Roman numbering system</returns>
        ''' <remarks>Characters that have not meaning in current Roman nnumbering system are pased to reaturn value without any change.
        ''' <para>This function is string-based and works even on numbers that are otherwise invalid.</para></remarks>
        Public Overrides Function ToLatin(ByVal value As String, ByVal LowerCase As Boolean) As String
            Return MyBase.ToLatin(ExpandThousands(value), LowerCase)
        End Function

        ''' <summary>Gets maximal supported number in Roman numerals numbering system</summary>
        ''' <returns>4999999</returns>
        Public Overrides ReadOnly Property Maximum() As Integer
            Get
                Return 4999999
            End Get
        End Property

        ''' <summary>Gets value indicating if parsing of string representation to integer is suported by current numbering system</summary>
        ''' <returns>True</returns>
        Public Overrides ReadOnly Property SupportsParse() As Boolean
            Get
                Return True
            End Get
        End Property
        ''' <summary>Expands all roman numerals ligatures to singe-glyph characters</summary>
        ''' <param name="Ligatured">String representing roman number containing ligature</param>
        ''' <returns>String with al roman numerals ligatures from <paramref name="Ligatured"/> expanded; null wne <paramref name="Ligatured"/> is null.</returns>
        ''' <remarks>Recognized ligatures are values of <see cref="II"/>, <see cref="III"/>, <see cref="III"/>, <see cref="IV"/>, <see cref="VI"/>, <see cref="VII"/>, <see cref="VIII"/> <see cref="IX"/>, <see cref="XI"/> and <see cref="XII"/>. All other characters remain unchanged.
        ''' <para>Note for inheritors: This function is caled by <see cref="TryParseInternal"/> before it calls <see cref="RomanNumberingSystem.TryParseInternal"/>.</para></remarks>
        Public Overrides Function Expand(ByVal Ligatured As String) As String
            Return MyBase.Expand(Ligatured.Replace(II & ThousandMultiplySuffix, M & M) _
                     .Replace(III & ThousandMultiplySuffix, M & M & M) _
                     .Replace(IV & ThousandMultiplySuffix, M & V & ThousandMultiplySuffix) _
                     .Replace(VI & ThousandMultiplySuffix, V & ThousandMultiplySuffix & M) _
                     .Replace(VII & ThousandMultiplySuffix, V & ThousandMultiplySuffix & M & M) _
                     .Replace(VIII & ThousandMultiplySuffix, V & ThousandMultiplySuffix & M & M & M) _
                     .Replace(IX & ThousandMultiplySuffix, M & X & ThousandMultiplySuffix) _
                     .Replace(XI & ThousandMultiplySuffix, X & ThousandMultiplySuffix & M) _
                     .Replace(XII & ThousandMultiplySuffix, X & ThousandMultiplySuffix & M & M))
        End Function
        ''' <summary>Attempts to parse string representation of number in Roman numeral numbering system to integer</summary>
        ''' <param name="value">String representation of number to parse</param>
        ''' <param name="result">When function exists with success contains parsed value representing <paramref name="value"/> as number</param>
        ''' <returns>When parsing is successfull returns null; otherwise returns exception describing the error.
        ''' This implementation uses <see cref="OverflowException"/> when number higher than <see cref="Maximum"/> is parsed -and-
        ''' <see cref="FormatException"/> when unexpected character is reached or unexpected sequence is reached. Note that only alloved subtractings are IV, IX, XL, XC, CD and CM. -and-
        ''' <see cref="ArgumentNullException"/> whan <paramref name="value"/> is null or an empty string</returns>
        Protected Overrides Function TryParseInternal(ByVal value As String, ByRef result As Integer) As System.Exception
            'TODO:
        End Function
    End Class
End Namespace
#End If