Imports Tools.ExtensionsT
#If Config <= Nightly Then 'Stage:Nightly
Namespace GlobalizationT.NumberingSystemsT
    ''' <summary>Roman numbering system defines uppercase and lowercase roman numerals like I, X, L, C, D, M or i, x, l, c, d, m</summary>
    ''' <remarks>This roman numeral system follows rules stated at <a href="http://en.wikipedia.org/wiki/Roman_numerals#XCIX_vs._IC">Wikipedia</a> stating that shortcut numbers like IC (instead of XCIX) etc. are not allowed. This implementation neither produce such numbers nor can parse them.
    ''' <para>Speial numerals for 11 (Ⅺ or ⅺ) and 12 (Ⅻ or ⅻ) are used only in numbers like 11, 12, 111, 12, 211, 212, 511, 512, 1011, 1012 etc. not in numbers like 21, 22, 61, 62, 121, 122 etc.  - only when the "spoken" meaning is eleven or twelve.</para></remarks>
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
    End Class
    ''' <summary>Roman numbering system based on Unicode character instead of latin letters</summary>
    ''' <remarks>Unicode characters differs in such way that they have single code-points for II, III, IV, VI, VII, VII IX, XI, and XII.</remarks>
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
        ''' <returns>False</returns>
        Public Overrides ReadOnly Property SupportsParse() As Boolean
            Get
                Return False
            End Get
        End Property
    End Class
End Namespace
#End If