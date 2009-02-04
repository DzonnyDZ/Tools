Imports System.Collections
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Tools.GlobalizationT.NumberingSystemsT

Namespace GlobalizationUT.NumberingSystemsUT
    <TestClass()> _
    Public Class PositionalNumberingSystemUT
        Private testContextInstance As TestContext

        Public Property TestContext() As TestContext
            Get
                Return testContextInstance
            End Get
            Set(ByVal value As TestContext)
                testContextInstance = value
            End Set
        End Property


        <TestMethod()> Public Sub GetValueU()
            Assert.AreEqual("13", PositionalNumberingSystem.Decimal.GetValue(13))
            Assert.AreEqual("0", PositionalNumberingSystem.Decimal.GetValue(0))
            Assert.AreEqual("306", PositionalNumberingSystem.Decimal.GetValue(306))
            Assert.AreEqual("1544586458", PositionalNumberingSystem.Decimal.GetValue(1544586458))
            Assert.AreEqual("-1544586458", PositionalNumberingSystem.Decimal.GetValue(-1544586458))
            Assert.AreEqual("-5", PositionalNumberingSystem.Decimal.GetValue(-5))

            Assert.AreEqual(Hex(13), PositionalNumberingSystem.HexadecimalUperCase.GetValue(13))
            Assert.AreEqual(Hex(0), PositionalNumberingSystem.HexadecimalUperCase.GetValue(0))
            Assert.AreEqual(Hex(306), PositionalNumberingSystem.HexadecimalUperCase.GetValue(306))
            Assert.AreEqual(Hex(1544586458), PositionalNumberingSystem.HexadecimalUperCase.GetValue(1544586458))
            Assert.AreEqual("-" & Hex(1544586458), PositionalNumberingSystem.HexadecimalUperCase.GetValue(-1544586458))
            Assert.AreEqual("-" & Hex(5), PositionalNumberingSystem.HexadecimalUperCase.GetValue(-5))

            Assert.AreEqual(Oct(13), PositionalNumberingSystem.Octal.GetValue(13))
            Assert.AreEqual(Oct(0), PositionalNumberingSystem.Octal.GetValue(0))
            Assert.AreEqual(Oct(306), PositionalNumberingSystem.Octal.GetValue(306))
            Assert.AreEqual(Oct(1544586458), PositionalNumberingSystem.Octal.GetValue(1544586458))
            Assert.AreEqual("-" & Oct(1544586458), PositionalNumberingSystem.Octal.GetValue(-1544586458))
            Assert.AreEqual("-" & Oct(5), PositionalNumberingSystem.Octal.GetValue(-5))

            Assert.AreEqual("1101", PositionalNumberingSystem.Binary.GetValue(13))
            Assert.AreEqual("0", PositionalNumberingSystem.Binary.GetValue(0))
            Assert.AreEqual("100110010", PositionalNumberingSystem.Binary.GetValue(306))
            Assert.AreEqual("1011100000100001000010011011010", PositionalNumberingSystem.Binary.GetValue(1544586458))
            Assert.AreEqual("-1011100000100001000010011011010", PositionalNumberingSystem.Binary.GetValue(-1544586458))
            Assert.AreEqual("-101", PositionalNumberingSystem.Binary.GetValue(-5))

            For i As Integer = 0 To 65536
                Assert.AreEqual(Hex(i), PositionalNumberingSystem.HexadecimalUperCase.GetValue(i))
                Assert.AreEqual(Oct(i), PositionalNumberingSystem.Octal.GetValue(i))
            Next

            Dim unary As New PositionalNumberingSystem("|"c)
            Assert.AreEqual("", unary.GetValue(0))
            Assert.AreEqual("||||||", unary.GetValue(6))
            Assert.AreEqual("-||||||", unary.GetValue(-6))

            Dim sys32 As New PositionalNumberingSystem("0123456789ABCDEFGHIJKLMNOPQRSTUV".ToCharArray)
            Assert.AreEqual("D", sys32.GetValue(13))
            Assert.AreEqual("0", sys32.GetValue(0))
            Assert.AreEqual("9I", sys32.GetValue(306))
            Assert.AreEqual("1E1116Q", sys32.GetValue(1544586458))
            Assert.AreEqual("-1E1116Q", sys32.GetValue(-1544586458))
            Assert.AreEqual("-5", sys32.GetValue(-5))
        End Sub

        <TestMethod()> Public Sub ParseU()
            Assert.AreEqual(13, PositionalNumberingSystem.Decimal.Parse("13"))
            Assert.AreEqual(0, PositionalNumberingSystem.Decimal.Parse("0"))
            Assert.AreEqual(306, PositionalNumberingSystem.Decimal.Parse("306"))
            Assert.AreEqual(1544586458, PositionalNumberingSystem.Decimal.Parse("1544586458"))
            Assert.AreEqual(-1544586458, PositionalNumberingSystem.Decimal.Parse("-1544586458"))
            Assert.AreEqual(-5, PositionalNumberingSystem.Decimal.Parse("-5"))

            Assert.AreEqual(13, PositionalNumberingSystem.HexadecimalUperCase.Parse(Hex(13)))
            Assert.AreEqual(0, PositionalNumberingSystem.HexadecimalUperCase.Parse(Hex(0)))
            Assert.AreEqual(306, PositionalNumberingSystem.HexadecimalUperCase.Parse(Hex(306)))
            Assert.AreEqual(1544586458, PositionalNumberingSystem.HexadecimalUperCase.Parse(Hex(1544586458)))
            Assert.AreEqual(-1544586458, PositionalNumberingSystem.HexadecimalUperCase.Parse("-" & Hex(1544586458)))
            Assert.AreEqual(-5, PositionalNumberingSystem.HexadecimalUperCase.Parse("-" & Hex(5)))

            Assert.AreEqual(13, PositionalNumberingSystem.Octal.Parse(Oct(13)))
            Assert.AreEqual(0, PositionalNumberingSystem.Octal.Parse(Oct(0)))
            Assert.AreEqual(306, PositionalNumberingSystem.Octal.Parse(Oct(306)))
            Assert.AreEqual(1544586458, PositionalNumberingSystem.Octal.Parse(Oct(1544586458)))
            Assert.AreEqual(-1544586458, PositionalNumberingSystem.Octal.Parse("-" & Oct(1544586458)))
            Assert.AreEqual(-5, PositionalNumberingSystem.Octal.Parse("-" & Oct(5)))

            Assert.AreEqual(13, PositionalNumberingSystem.Binary.Parse("1101"))
            Assert.AreEqual(0, PositionalNumberingSystem.Binary.Parse("0"))
            Assert.AreEqual(306, PositionalNumberingSystem.Binary.Parse("100110010"))
            Assert.AreEqual(1544586458, PositionalNumberingSystem.Binary.Parse("1011100000100001000010011011010"))
            Assert.AreEqual(-1544586458, PositionalNumberingSystem.Binary.Parse("-1011100000100001000010011011010"))
            Assert.AreEqual(-5, PositionalNumberingSystem.Binary.Parse("-101"))

            For i As Integer = 0 To 65536
                Assert.AreEqual(i, PositionalNumberingSystem.HexadecimalUperCase.Parse(Hex(i)))
                Assert.AreEqual(i, PositionalNumberingSystem.Octal.Parse(Oct(i)))
            Next

            Dim unary As New PositionalNumberingSystem("|"c)
            Assert.AreEqual(0, unary.Parse(""))
            Assert.AreEqual(6, unary.Parse("||||||"))
            Assert.AreEqual(-6, unary.Parse("-||||||"))

            Dim sys32 As New PositionalNumberingSystem("0123456789ABCDEFGHIJKLMNOPQRSTUV")
            Assert.AreEqual(13, sys32.Parse("D"))
            Assert.AreEqual(0, sys32.Parse("0"))
            Assert.AreEqual(306, sys32.Parse("9I"))
            Assert.AreEqual(1544586458, sys32.Parse("1E1116Q"))
            Assert.AreEqual(-1544586458, sys32.Parse("-1E1116Q"))
            Assert.AreEqual(-5, sys32.Parse("-5"))
        End Sub

        <ExpectedException(GetType(ArgumentException))> _
        <TestMethod()> Public Sub NewU1()
            Dim x As New PositionalNumberingSystem("1"c, "2"c, "1"c)
            Assert.Fail()
        End Sub
        <ExpectedException(GetType(ArgumentException))> _
        <TestMethod()> Public Sub NewU2()
            Dim x As New PositionalNumberingSystem("1"c, "2"c, "3"c, "-"c)
            Assert.Fail()
        End Sub

        <ExpectedException(GetType(ArgumentException))> _
        <TestMethod()> Public Sub NewU3()
            Dim x As New PositionalNumberingSystem("!"c, New Char() {"1"c, "2"c, "3"c, "!"c})
            Assert.Fail()
        End Sub
        <ExpectedException(GetType(ArgumentOutOfRangeException))> _
        <TestMethod()> Public Sub NewU4()
            Dim x As New PositionalNumberingSystem(61)
            Assert.Fail()
        End Sub

        <TestMethod()> _
        Public Sub NewU5()
            Dim sys60 As New PositionalNumberingSystem(60, TextT.Casing.LowerCase)
            Assert.AreEqual("ιrfd", sys60.GetValue(9602113))
            Assert.AreEqual(9602113, sys60.Parse("ιrfd"))
        End Sub

    End Class

End Namespace