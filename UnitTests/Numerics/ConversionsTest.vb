Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Tools.NumericsT
Imports System.Numerics


Namespace NumericsUT
    '''<summary>
    '''This is a test class for ConversionsTTest and is intended
    '''to contain all ConversionsTTest Unit Tests
    '''</summary>
    <TestClass()> _
    Public Class ConversionsTTest



        '''<summary>
        '''Gets or sets the test context which provides
        '''information about and functionality for the current test run.
        '''</summary>
        Public Property TestContext() As TestContext


#Region "Additional test attributes"
        '
        'You can use the following additional attributes as you write your tests:
        '
        'Use ClassInitialize to run code before running the first test in the class
        '<ClassInitialize()>  _
        'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
        'End Sub
        '
        'Use ClassCleanup to run code after all tests in a class have run
        '<ClassCleanup()>  _
        'Public Shared Sub MyClassCleanup()
        'End Sub
        '
        'Use TestInitialize to run code before running each test
        '<TestInitialize()>  _
        'Public Sub MyTestInitialize()
        'End Sub
        '
        'Use TestCleanup to run code after each test has run
        '<TestCleanup()>  _
        'Public Sub MyTestCleanup()
        'End Sub
        '
#End Region


        '''<summary>
        '''A test for ChangeBase
        '''</summary>
        <TestMethod()> _
        Public Sub Dec2Xxx()
            '10 - 10
            Dim item = New With {.Source = 1, .To = 10, .Expected = "1"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 987445, .To = 10, .Expected = "987445"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 0, .To = 10, .Expected = "0"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            '10 - 16
            item = New With {.Source = 0, .To = 16, .Expected = "0"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 1, .To = 16, .Expected = "1"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 9, .To = 16, .Expected = "9"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 10, .To = 16, .Expected = "A"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To), True)

            item = New With {.Source = 15, .To = 16, .Expected = "F"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To), True)

            item = New With {.Source = 6977, .To = 16, .Expected = "1B41"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To), True)

            '10 - 8
            item = New With {.Source = 0, .To = 8, .Expected = "0"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 1, .To = 8, .Expected = "1"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 7, .To = 8, .Expected = "7"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 8, .To = 8, .Expected = "10"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 72008, .To = 8, .Expected = "214510"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            '10 - 2
            item = New With {.Source = 0, .To = 2, .Expected = "0"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 1, .To = 2, .Expected = "1"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 2, .To = 2, .Expected = "10"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 72008, .To = 2, .Expected = "10001100101001000"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            '10-3
            item = New With {.Source = 0, .To = 3, .Expected = "0"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 1, .To = 3, .Expected = "1"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 2, .To = 3, .Expected = "2"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 3, .To = 3, .Expected = "10"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 4, .To = 3, .Expected = "11"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 5, .To = 3, .Expected = "12"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 7775, .To = 3, .Expected = "101122222"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            '10-20
            item = New With {.Source = 0, .To = 20, .Expected = "0"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 1, .To = 20, .Expected = "1"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 19, .To = 20, .Expected = "J"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To), True)

            item = New With {.Source = 20, .To = 20, .Expected = "10"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 557923, .To = 20, .Expected = "39eg3"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To), True)


            '10-36
            item = New With {.Source = 0, .To = 36, .Expected = "0"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 1, .To = 36, .Expected = "1"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 35, .To = 36, .Expected = "Z"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To), True)

            item = New With {.Source = 36, .To = 36, .Expected = "10"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 557923, .To = 36, .Expected = "BYHV"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To), True)

            '10-10 - special
            Dim alphabet = "٠١٢٣٤٥٦٧٨٩".ToCharArray

            item = New With {.Source = 0, .To = 10, .Expected = "٠"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To, alphabet))

            item = New With {.Source = 1, .To = 10, .Expected = "١"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To, alphabet))

            item = New With {.Source = 2, .To = 10, .Expected = "٢"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To, alphabet))

            item = New With {.Source = 9, .To = 10, .Expected = "٩"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To, alphabet))

            item = New With {.Source = 10, .To = 10, .Expected = "١٠"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To, alphabet))

            item = New With {.Source = 6208, .To = 10, .Expected = "٦٢٠٨"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To, alphabet))

            '10-1
            item = New With {.Source = 0, .To = 1, .Expected = ""}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 1, .To = 1, .Expected = "1"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 5, .To = 1, .Expected = "11111"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            item = New With {.Source = 55, .To = 1, .Expected = New String("1"c, 55)}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To))

            '10-59
            alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZαβγδεζθικλμνξοπρστυφχψω".ToCharArray

            item = New With {.Source = 0, .To = 59, .Expected = "0"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To, alphabet))

            item = New With {.Source = 1, .To = 59, .Expected = "1"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To, alphabet))

            item = New With {.Source = 10, .To = 59, .Expected = "A"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To, alphabet))

            item = New With {.Source = 35, .To = 59, .Expected = "Z"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To, alphabet))

            item = New With {.Source = 36, .To = 59, .Expected = "α"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To, alphabet))

            item = New With {.Source = 58, .To = 59, .Expected = "ω"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To, alphabet))

            item = New With {.Source = 59, .To = 59, .Expected = "10"}
            Assert.AreEqual(item.Expected, ConversionsT.Dec2Xxx(item.Source, item.To, alphabet))
        End Sub

        <TestMethod()> _
        Public Sub Xxx2Dec()
            '10 - 10
            Dim item = New With {.Expected = 1, .From = 10, .Value = "1"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 987445, .From = 10, .Value = "987445"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 0, .From = 10, .Value = "0"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            '10 - 16
            item = New With {.Expected = 0, .From = 16, .Value = "0"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 1, .From = 16, .Value = "1"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 9, .From = 16, .Value = "9"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 10, .From = 16, .Value = "A"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 15, .From = 16, .Value = "F"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 6977, .From = 16, .Value = "1B41"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            '10 - 8
            item = New With {.Expected = 0, .From = 8, .Value = "0"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 1, .From = 8, .Value = "1"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 7, .From = 8, .Value = "7"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 8, .From = 8, .Value = "10"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 72008, .From = 8, .Value = "214510"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            '10 - 2
            item = New With {.Expected = 0, .From = 2, .Value = "0"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 1, .From = 2, .Value = "1"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 2, .From = 2, .Value = "10"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 72008, .From = 2, .Value = "10001100101001000"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            '10-3
            item = New With {.Expected = 0, .From = 3, .Value = "0"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 1, .From = 3, .Value = "1"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 2, .From = 3, .Value = "2"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 3, .From = 3, .Value = "10"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 4, .From = 3, .Value = "11"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 5, .From = 3, .Value = "12"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 7775, .From = 3, .Value = "101122222"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            '10-20
            item = New With {.Expected = 0, .From = 20, .Value = "0"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 1, .From = 20, .Value = "1"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 19, .From = 20, .Value = "J"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 20, .From = 20, .Value = "10"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 557923, .From = 20, .Value = "39eg3"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))


            '10-36
            item = New With {.Expected = 0, .From = 36, .Value = "0"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 1, .From = 36, .Value = "1"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 35, .From = 36, .Value = "Z"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 36, .From = 36, .Value = "10"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 557923, .From = 36, .Value = "BYHV"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            '10-10 - special
            Dim alphabet = "٠١٢٣٤٥٦٧٨٩".ToCharArray

            item = New With {.Expected = 0, .From = 10, .Value = "٠"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From, alphabet))

            item = New With {.Expected = 1, .From = 10, .Value = "١"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From, alphabet))

            item = New With {.Expected = 2, .From = 10, .Value = "٢"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From, alphabet))

            item = New With {.Expected = 9, .From = 10, .Value = "٩"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From, alphabet))

            item = New With {.Expected = 10, .From = 10, .Value = "١٠"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From, alphabet))

            item = New With {.Expected = 6208, .From = 10, .Value = "٦٢٠٨"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From, alphabet))

            '10-1
            item = New With {.Expected = 0, .From = 1, .Value = ""}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 1, .From = 1, .Value = "1"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 5, .From = 1, .Value = "11111"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            item = New With {.Expected = 55, .From = 1, .Value = New String("1"c, 55)}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From))

            '10-59
            alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZαβγδεζθικλμνξοπρστυφχψω".ToCharArray

            item = New With {.Expected = 0, .From = 59, .Value = "0"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From, alphabet))

            item = New With {.Expected = 1, .From = 59, .Value = "1"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From, alphabet))

            item = New With {.Expected = 10, .From = 59, .Value = "A"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From, alphabet))

            item = New With {.Expected = 35, .From = 59, .Value = "Z"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From, alphabet))

            item = New With {.Expected = 36, .From = 59, .Value = "α"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From, alphabet))

            item = New With {.Expected = 58, .From = 59, .Value = "ω"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From, alphabet))

            item = New With {.Expected = 59, .From = 59, .Value = "10"}
            Assert.AreEqual(CType(item.Expected, BigInteger), ConversionsT.Xxx2Dec(item.Value, item.From, alphabet))
        End Sub
    End Class
End Namespace