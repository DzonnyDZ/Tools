Imports System.Collections
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Tools.ExtensionsT


Namespace ExtensionsUT
    <TestClass()> _
    Public Class StringExtensionsUT
        Private testContextInstance As TestContext

        Public Property TestContext() As TestContext
            Get
                Return testContextInstance
            End Get
            Set(ByVal value As TestContext)
                testContextInstance = value
            End Set
        End Property

        <TestMethod()> _
        Public Sub IsNullOrEMptyU()
            Assert.IsFalse("aaa".IsNullOrEmpty)
            Assert.IsTrue("".IsNullOrEmpty)
            Assert.IsTrue(CStr(Nothing).IsNullOrEmpty)
        End Sub

        <TestMethod()> _
        Public Sub FormatU()
            Assert.AreEqual("Hello", "Hello".Format)
            Assert.AreEqual("Hello my name is Jan.", Format("Hello my name is {0}.", "Jan"))
            Assert.AreEqual("This is 15.", "This is {0}.".Format(15))
            Assert.AreEqual("This is 0015.", "This is {0:D4}.".Format(15))
            Assert.AreEqual("This is 15 and this is 16 and this is 15 again.", "This is {0} and this is {1} and this is {0} again.".Format(15, 16))
        End Sub
        <TestMethod()> _
       Public Sub fU()
            Assert.AreEqual("Hello", "Hello".f)
            Assert.AreEqual("Hello my name is Jan.", "Hello my name is {0}.".f("Jan"))
            Assert.AreEqual("This is 15.", "This is {0}.".f(15))
            Assert.AreEqual("This is 0015.", "This is {0:D4}.".f(15))
            Assert.AreEqual("This is 15 and this is 16 and this is 15 again.", "This is {0} and this is {1} and this is {0} again.".f(15, 16))
        End Sub
        <TestMethod()> _
        Public Sub JoinU()
            Assert.AreEqual("a,b,c,d", New String() {"a", "b", "c", "d"}.Join(","))
        End Sub
        <TestMethod()> _
        Public Sub Join2U()
            Assert.AreEqual("a,b,c,d", DirectCast(New String() {"a", "b", "c", "d"}, IEnumerable(Of String)).Join(","))
        End Sub
        <TestMethod()> _
        Public Sub SplitU()
            Dim result = "a:::b:::c:::d".Split(":::", 3)
            Assert.AreEqual("a", result(0))
            Assert.AreEqual("b", result(1))
            Assert.AreEqual("c:::d", result(2))
        End Sub
        <TestMethod()> _
        Public Sub WhitespaceSplit()
            Dim result As String()
            result = "a b c  d".WhiteSpaceSplit
            Assert.AreEqual("a", result(0)) : Assert.AreEqual("b", result(1)) : Assert.AreEqual("c", result(2)) : Assert.AreEqual("d", result(3))
            result = "   a b c  d   ".WhiteSpaceSplit
            Assert.AreEqual("a", result(0)) : Assert.AreEqual("b", result(1)) : Assert.AreEqual("c", result(2)) : Assert.AreEqual("d", result(3))
            result = "   a bug- -c-  d   ".WhiteSpaceSplit
            Assert.AreEqual("a", result(0)) : Assert.AreEqual("bug-", result(1)) : Assert.AreEqual("-c-", result(2)) : Assert.AreEqual("d", result(3))
            result = (vbCrLf & "a" & vbTab & vbCrLf & vbTab & "b" & vbCrLf & vbLf & "c" & vbCr & vbCr & "d").WhiteSpaceSplit
            Assert.AreEqual("a", result(0)) : Assert.AreEqual("b", result(1)) : Assert.AreEqual("c", result(2)) : Assert.AreEqual("d", result(3))
            '0020 1680 180E 2000 200 2002 2003 2004 2005 2006 2007 2008 2009 200A 202F 205F 3000
            'U+2028
            'U+2029
            '0009 000A 000B 000C 000D 0085 0000A0
            result = (vbCrLf & "a" & ChrW(&H1680) & ChrW(&H2004) & "b" & ChrW(&H2028) & ChrW(&H2029) & "c" & ChrW(&HA0) & ChrW(&H85) & "d" & ChrW(&H3000) & ChrW(&H202F)).WhiteSpaceSplit
            Assert.AreEqual("a", result(0)) : Assert.AreEqual("b", result(1)) : Assert.AreEqual("c", result(2)) : Assert.AreEqual("d", result(3))
        End Sub
        <TestMethod()> Public Sub RevereseU()
            Assert.AreEqual("", "".Reverse)
            Assert.AreEqual("ň", "ň".Reverse)
            Assert.AreEqual("abc", "cba".Reverse)
            Assert.AreEqual("12345hntv", "vtnh54321".Reverse)
            Assert.AreEqual(Nothing, CStr(Nothing).Reverse)
        End Sub
    End Class
End Namespace