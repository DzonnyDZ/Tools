Imports System.Collections.Generic
Imports Globalization = System.Globalization
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Tools.ExtensionsT
Imports System.Globalization.CultureInfo
Imports System.Text.RegularExpressions

Namespace ExtensionsUT
    <TestClass()> _
    Public Class StringFormattingUT
        Private testContextInstance As TestContext

        Public Property TestContext() As TestContext
            Get
                Return testContextInstance
            End Get
            Set(ByVal value As TestContext)
                testContextInstance = value
            End Set
        End Property

#Region "CFormat/CEscape"
        <TestMethod()>
        Public Sub CEscapeU()
            Assert.AreEqual("Tes\st", "Tes\\st".CEscape)
            Assert.AreEqual("Hello" & vbCrLf & "world", "Hello\r\nworld".CEscape)
            '\a</term><description>Alert 0x7</description></item>
            Assert.AreEqual("Hello" & ChrW(&H7) & "world", "Hello\aworld".CEscape)
            '\b</term><description>Backspace 0x8</description></item>
            Assert.AreEqual("Hello" & ChrW(&H8) & "world", "Hello\bworld".CEscape)
            '\f</term><description>Form feed 0xC</description></item>
            Assert.AreEqual("Hello" & ChrW(&HC) & "world", "Hello\fworld".CEscape)
            '\n</term><description>New line 0xA</description></item>
            Assert.AreEqual("Hello" & ChrW(&HA) & "world", "Hello\nworld".CEscape)
            '\r</term><description>Carriage return 0xD</description></item>
            Assert.AreEqual("Hello" & ChrW(&HD) & "world", "Hello\rworld".CEscape)
            '\t</term><description>Horizontal tab 0x9</description></item>
            Assert.AreEqual("Hello" & ChrW(&H9) & "world", "Hello\tworld".CEscape)
            '\v</term><description>Vertical tab 0xB</description></item>
            Assert.AreEqual("Hello" & ChrW(&HB) & "world", "Hello\vworld".CEscape)
            '\.</term><description>Empty string (ignored)</description></item>
            Assert.AreEqual("Helloworld", "Hello\.world".CEscape)
            '\U[0-9A-Za-z]+, \u[0-9A-Za-z]+, \X[0-9A-Za-z]+, \x[0-9A-Za-z]+</term><description>Hexadecimal Unicode escape sequence. Given hexadecimal number of any length is passed to <see cref="[Char].ConvertFromUtf32"/>.</description></item>
            Assert.AreEqual("Hello" & ChrW(&H15) & "world", "Hello\X15world".CEscape)
            Assert.AreEqual("Hello" & ChrW(&H15) & "world", "Hello\x15world".CEscape)
            Assert.AreEqual("Hello" & ChrW(&H15) & "world", "Hello\u15world".CEscape)
            Assert.AreEqual("Hello" & ChrW(&H15) & "world", "Hello\U15world".CEscape)
            Assert.AreEqual("Hello" & ChrW(&HAFB) & "world", "Hello\xafbworld".CEscape)
            Assert.AreEqual("Hello" & Char.ConvertFromUtf32(&H10FFFF) & "world", "Hello\X10FFFFworld".CEscape)
            '\[0-9]+</term><description>Decimal Unicode escape sequence. Given decimal number of any length is passed to <see cref="[Char].ConvertFromUtf32"/>.</description></item>
            Assert.AreEqual("Hello" & ChrW(714) & "world", "Hello\714world".CEscape)
            Assert.AreEqual("Hello" & ChrW(15902) & "world", "Hello\15902world".CEscape)
            '\&lt;any other character></term>The character, not the backslash.</item>
            Assert.AreEqual("Helloňworld", "Hello\ňworld".CEscape)
            '\0</term><description>Nullchar</description></item>
            Assert.AreEqual("Hello" & ChrW(0) & "world", "Hello\0world".CEscape)
            '\\</term><description>\</description></item>
            Assert.AreEqual("Hello""world", "Hello\""world".CEscape)
            '\"</term><description>"</description></item>
            Assert.AreEqual("Hello'world", "Hello\'world".CEscape)
            '\'</term><description>'</description></item>
        End Sub

        <TestMethod()>
        Public Sub CFormatU()
            Assert.AreEqual("Number 7", "Number {0}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number 7", "Number {00}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number   7", "Number {0,3}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number 7  ", "Number {0,-3}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number 7  ", "Number {0,-03}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number 7  ", "Number {0,-003}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number 007", "Number {0:000}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number {007}", "Number {{{0:000}}}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number {007}", "Number \{{0:000}\}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number  07", "Number {0,3:00}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number 07 ", "Number {0,-3:00}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number 07 ", "Number {0,-03:00}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number 07 ", "Number {0,-003:00}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Time 14hours\15minutes {4seconds}..", "Time {0:h""hours""\\\\m""minutes"" \{s""seconds""}}"".""}.".CFormat(New TimeSpanFormattable(14, 15, 4)))
        End Sub
#End Region

#Region "Replace"
        <TestMethod()>
        Public Sub CReplaceEscapingU()
            Assert.AreEqual("Tes\st", "Tes\\st".CReplace(EmptyArray.Object))
            Assert.AreEqual("Hello" & vbCrLf & "world", "Hello\r\nworld".CReplace(EmptyArray.Object))
            '\a</term><description>Alert 0x7</description></item>
            Assert.AreEqual("Hello" & ChrW(&H7) & "world", "Hello\aworld".CReplace(EmptyArray.Object))
            '\b</term><description>Backspace 0x8</description></item>
            Assert.AreEqual("Hello" & ChrW(&H8) & "world", "Hello\bworld".CReplace(EmptyArray.Object))
            '\f</term><description>Form feed 0xC</description></item>
            Assert.AreEqual("Hello" & ChrW(&HC) & "world", "Hello\fworld".CReplace(EmptyArray.Object))
            '\n</term><description>New line 0xA</description></item>
            Assert.AreEqual("Hello" & ChrW(&HA) & "world", "Hello\nworld".CReplace(EmptyArray.Object))
            '\r</term><description>Carriage return 0xD</description></item>
            Assert.AreEqual("Hello" & ChrW(&HD) & "world", "Hello\rworld".CReplace(EmptyArray.Object))
            '\t</term><description>Horizontal tab 0x9</description></item>
            Assert.AreEqual("Hello" & ChrW(&H9) & "world", "Hello\tworld".CReplace(EmptyArray.Object))
            '\v</term><description>Vertical tab 0xB</description></item>
            Assert.AreEqual("Hello" & ChrW(&HB) & "world", "Hello\vworld".CReplace(EmptyArray.Object))
            '\.</term><description>Empty string (ignored)</description></item>
            Assert.AreEqual("Helloworld", "Hello\.world".CReplace(EmptyArray.Object))
            '\U[0-9A-Za-z]+, \u[0-9A-Za-z]+, \X[0-9A-Za-z]+, \x[0-9A-Za-z]+</term><description>Hexadecimal Unicode escape sequence. Given hexadecimal number of any length is passed to <see cref="[Char].ConvertFromUtf32"/>.</description></item>
            Assert.AreEqual("Hello" & ChrW(&H15) & "world", "Hello\X15world".CReplace(EmptyArray.Object))
            Assert.AreEqual("Hello" & ChrW(&H15) & "world", "Hello\x15world".CReplace(EmptyArray.Object))
            Assert.AreEqual("Hello" & ChrW(&H15) & "world", "Hello\u15world".CReplace(EmptyArray.Object))
            Assert.AreEqual("Hello" & ChrW(&H15) & "world", "Hello\U15world".CReplace(EmptyArray.Object))
            Assert.AreEqual("Hello" & ChrW(&HAFB) & "world", "Hello\xafbworld".CReplace(EmptyArray.Object))
            Assert.AreEqual("Hello" & Char.ConvertFromUtf32(&H10FFFF) & "world", "Hello\X10FFFFworld".CReplace(EmptyArray.Object))
            '\[0-9]+</term><description>Decimal Unicode escape sequence. Given decimal number of any length is passed to <see cref="[Char].ConvertFromUtf32"/>.</description></item>
            Assert.AreEqual("Hello" & ChrW(714) & "world", "Hello\714world".CReplace(EmptyArray.Object))
            Assert.AreEqual("Hello" & ChrW(15902) & "world", "Hello\15902world".CReplace(EmptyArray.Object))
            '\&lt;any other character></term>The character, not the backslash.</item>
            Assert.AreEqual("Helloňworld", "Hello\ňworld".CReplace(EmptyArray.Object))
            '\0</term><description>Nullchar</description></item>
            Assert.AreEqual("Hello" & ChrW(0) & "world", "Hello\0world".CReplace(EmptyArray.Object))
            '\\</term><description>\</description></item>
            Assert.AreEqual("Hello""world", "Hello\""world".CReplace(EmptyArray.Object))
            '\"</term><description>"</description></item>
            Assert.AreEqual("Hello'world", "Hello\'world".CReplace(EmptyArray.Object))
            '\'</term><description>'</description></item>
        End Sub

        <TestMethod>
        Public Sub ReplaceTransformation()
            Assert.AreEqual("Test 01", "Test {a:00|html}".Replace(InvariantCulture, "a", 1))
            Assert.AreEqual("Test 1", "Test {a|html}".Replace(InvariantCulture, "a", 1))
            Assert.AreEqual("Test &lt;b", "Test {a|html}b".Replace(InvariantCulture, "a", "<"))
            Assert.AreEqual("Test &amp;lt;b", "Test {a:|html|html}b".Replace(InvariantCulture, "a", "<"))
            Assert.AreEqual("Alert('Hello \'world\'!');", "Alert('{message|JS}');".Replace(InvariantCulture, "message", "Hello 'world'!"))
            Assert.AreEqual(
                "<a onclick=""alert('Do you wanna go to page &lt;\&apos;Shit &amp; Hit\&apos;>?')"" href=""http://shitandhit.com?a=1&amp;b=2"">Shit &amp; Hit</a>",
                "<a onclick=""alert('{message|JS|xmlAttribute}')"" href=""{url|htmlAttribute}"">{text|html}</a>".
                    Replace(InvariantCulture,
                        "message", "Do you wanna go to page <'Shit & Hit'>?",
                        "url", "http://shitandhit.com?a=1&b=2",
                        "text", "Shit & Hit"
            ))
        End Sub

        <TestMethod>
        Public Sub CFormatTransformation()
            Assert.AreEqual("Test 01", "Test {0:00|html}".CFormat(InvariantCulture, 1))
            Assert.AreEqual("Test 1", "Test {0|html}".CFormat(InvariantCulture, 1))
            Assert.AreEqual("Test &lt;b", "Test {0|html}b".CFormat(InvariantCulture, "<"))
            Assert.AreEqual("Test &amp;lt;b", "Test {0:|html|html}b".CFormat(InvariantCulture, "<"))
            Assert.AreEqual("Alert('Hello \'world\'!');", "Alert('{0|JS}');".CFormat(InvariantCulture, "Hello 'world'!"))
            Assert.AreEqual(
                "<a onclick=""alert('Do you wanna go to page &lt;\&apos;Shit &amp; Hit\&apos;>?')"" href=""http://shitandhit.com?a=1&amp;b=2"">Shit &amp; Hit</a>",
                "<a onclick=""alert('{0|JS|xmlAttribute}')"" href=""{1|htmlAttribute}"">{2|html}</a>".
                    CFormat(InvariantCulture, "Do you wanna go to page <'Shit & Hit'>?", "http://shitandhit.com?a=1&b=2", "Shit & Hit"
            ))
        End Sub

        <TestMethod>
        Public Sub ReplaceGet()
            Dim getter As Func(Of String, Object) =
                Function(name$)
                    Select Case name
                        Case "Str" : Return "vStr"
                        Case "Int" : Return 7
                        Case "{name}" : Return "value of name"
                        Case "null" : Return Nothing
                        Case "\" : Return "BackSlash"
                        Case ":" : Return "colon"
                        Case "," : Return "comma"
                        Case "{" : Return "open"
                        Case "}" : Return "close"
                        Case "a\" : Return "xBackSlash"
                        Case "a:" : Return "xcolon"
                        Case "a," : Return "xcomma"
                        Case "a{" : Return "xopen"
                        Case "a}" : Return "xclose"
                        Case "Date" : Return New Date(1970, 1, 1)
                        Case Else : Throw New ArgumentException("Unknown value", "name")
                    End Select
                End Function
            Assert.AreEqual("vStr", "{Str}".Replace(getter))
            Assert.AreEqual("avStrb", "a{Str}b".Replace(getter))
            Assert.AreEqual("{Str}", "{{Str}}".Replace(getter))
            Assert.AreEqual("{Str}", "{{Str}".Replace(getter))
            Assert.AreEqual("{vStr}", "{{{Str}{}}".Replace(getter))
            Assert.AreEqual("7", "{Int}".Replace(getter, InvariantCulture))
            Assert.AreEqual("007", "{Int:000}".Replace(getter, InvariantCulture))
            Assert.AreEqual("|{1970}|", "{Date:||{yyyy}{}}|".Replace(getter, InvariantCulture))
            Assert.AreEqual("|{1970}|", "{Date:||{{yyyy}}||}".Replace(getter, InvariantCulture))
            Assert.AreEqual("|{1970}|", "|{Date:{yyyy}}}|".Replace(getter, InvariantCulture))
            Assert.AreEqual("{|1970|}", "{{{Date:||yyyy||}{}}".Replace(getter, InvariantCulture))
            Assert.AreEqual(" 7", "{Int,2}".Replace(getter, InvariantCulture))
            Assert.AreEqual("7 ", "{Int,-2}".Replace(getter, InvariantCulture))
            Assert.AreEqual("7 ", "{Int,-02}".Replace(getter, InvariantCulture))
            Assert.AreEqual("7  ", "{Int,-003}".Replace(getter, InvariantCulture))
            Assert.AreEqual("07 ", "{Int,-3:00}".Replace(getter, InvariantCulture))
            Assert.AreEqual("value of name", "{}{{name}}}".Replace(getter, InvariantCulture))
            Assert.AreEqual("{value of name}", "{{{}{{name}}}{}}".Replace(getter, InvariantCulture))
            Assert.AreEqual("", "{does not exist}".Replace(getter, InvariantCulture))
            Assert.AreEqual("", "{null}".Replace(getter, InvariantCulture))
            Assert.AreEqual("BackSlash", "{\\}".CReplace(getter, InvariantCulture))
            Assert.AreEqual("colon", "{\:}".CReplace(getter, InvariantCulture))
            Assert.AreEqual("colon", "{::}".CReplace(getter, InvariantCulture))
            Assert.AreEqual("comma", "{\,}".CReplace(getter, InvariantCulture))
            Assert.AreEqual("comma", "{,,}".CReplace(getter, InvariantCulture))
            Assert.AreEqual("open", "{\{}".CReplace(getter, InvariantCulture))
            Assert.AreEqual("open", "{}{{}".CReplace(getter, InvariantCulture))
            Assert.AreEqual("close", "{}}}".CReplace(getter, InvariantCulture))
            Assert.AreEqual("close", "{\}}".CReplace(getter, InvariantCulture))

            Assert.AreEqual("xBackSlash", "{a\\}".CReplace(getter, InvariantCulture))
            Assert.AreEqual("xcolon", "{a\:}".CReplace(getter, InvariantCulture))
            Assert.AreEqual("xcolon", "{a::}".CReplace(getter, InvariantCulture))
            Assert.AreEqual("xcomma", "{a\,}".CReplace(getter, InvariantCulture))
            Assert.AreEqual("xcomma", "{a,,}".CReplace(getter, InvariantCulture))
            Assert.AreEqual("xopen", "{a\{}".CReplace(getter, InvariantCulture))
            Assert.AreEqual("xopen", "{a{{}".CReplace(getter, InvariantCulture))
            Assert.AreEqual("xclose", "{a}}}".CReplace(getter, InvariantCulture))
            Assert.AreEqual("xclose", "{a\}}".CReplace(getter, InvariantCulture))
        End Sub

        <TestMethod>
        Public Sub ReplaceWithRegex()
            Assert.AreEqual("before AA after BB", "before {a} after {b}".Replace(New Dictionary(Of String, Object) From {{"a", "AA"}, {"b", "BB"}}, New Regex("^[A-Za-z]+$")))
            Assert.AreEqual("before AA after {b-b}", "before {a} after {b-b}".Replace(New Dictionary(Of String, Object) From {{"a", "AA"}, {"b-b", "BB"}}, New Regex("^[A-Za-z]+$")))
            Assert.AreEqual("before 179.0 after {b-b}", "before {a:0.0} after {b-b}".Replace(New Dictionary(Of String, Object) From {{"a", 178.9997}, {"b-b", "BB"}}, New Regex("^[A-Za-z]+$"), InvariantCulture))
        End Sub
#End Region
    End Class
End Namespace