Imports Globalization = System.Globalization
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Tools.ExtensionsT

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
        <TestMethod()> Public Sub CEscapeU()
            Assert.AreEqual("Tes\st", "Tes\\st".CEscape)
            Assert.AreEqual("Hello" & vbCrLf & "world", "Hello\r\nworld".CEscape)
            '\a</term><description>Allert 0x7</description></item>
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
        <TestMethod()> Public Sub CFormatU()
            Assert.AreEqual("Number 7", "Number {0}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number   7", "Number {0,3}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number 7  ", "Number {0,-3}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number 007", "Number {0:000}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number {007}", "Number {{{0:000}}}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number {007}", "Number \{{0:000}\}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Number  07", "Number {0,3:00}".CFormat(Globalization.CultureInfo.InvariantCulture, 7))
            Assert.AreEqual("Time 14hours\15minutes {4seconds}..", "Time {0:h""hours""\\\\m""minutes"" \{s""seconds""}}"".""}.".CFormat(New TimeSpanFormattable(14, 15, 4)))
        End Sub
    End Class
End Namespace