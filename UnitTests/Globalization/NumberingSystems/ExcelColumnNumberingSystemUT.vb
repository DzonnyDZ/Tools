Imports System.Collections
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Tools.GlobalizationT.NumberingSystemsT

Namespace GlobalizationUT.NumberingSystemsUT
    <TestClass()> _
    Public Class ExcelColumnNumberingSystemUT
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
        Public Sub GetValueU()
            Assert.AreEqual("A", ExcelColumnNumberingSystem.Default.GetValue(1))
            Assert.AreEqual("B", ExcelColumnNumberingSystem.Default.GetValue(2))
            Assert.AreEqual("Z", ExcelColumnNumberingSystem.Default.GetValue(26))
            Assert.AreEqual("AA", ExcelColumnNumberingSystem.Default.GetValue(27))
            Assert.AreEqual("AB", ExcelColumnNumberingSystem.Default.GetValue(28))
            Assert.AreEqual("AZ", ExcelColumnNumberingSystem.Default.GetValue(52))
            Assert.AreEqual("BA", ExcelColumnNumberingSystem.Default.GetValue(53))
            Assert.AreEqual("ZZ", ExcelColumnNumberingSystem.Default.GetValue(702))
            Assert.AreEqual("AAA", ExcelColumnNumberingSystem.Default.GetValue(703))
            Assert.AreEqual("AAZ", ExcelColumnNumberingSystem.Default.GetValue(728))
            Assert.AreEqual("ABA", ExcelColumnNumberingSystem.Default.GetValue(729))
            Assert.AreEqual("XFD", ExcelColumnNumberingSystem.Default.GetValue(16384))
        End Sub
        <TestMethod()> _
        Public Sub ParseU()
            Assert.AreEqual(1, ExcelColumnNumberingSystem.Default.Parse("A"))
            Assert.AreEqual(2, ExcelColumnNumberingSystem.Default.Parse("B"))
            Assert.AreEqual(26, ExcelColumnNumberingSystem.Default.Parse("Z"))
            Assert.AreEqual(27, ExcelColumnNumberingSystem.Default.Parse("AA"))
            Assert.AreEqual(28, ExcelColumnNumberingSystem.Default.Parse("AB"))
            Assert.AreEqual(52, ExcelColumnNumberingSystem.Default.Parse("AZ"))
            Assert.AreEqual(53, ExcelColumnNumberingSystem.Default.Parse("BA"))
            Assert.AreEqual(702, ExcelColumnNumberingSystem.Default.Parse("ZZ"))
            Assert.AreEqual(703, ExcelColumnNumberingSystem.Default.Parse("AAA"))
            Assert.AreEqual(728, ExcelColumnNumberingSystem.Default.Parse("AAZ"))
            Assert.AreEqual(729, ExcelColumnNumberingSystem.Default.Parse("ABA"))
            Assert.AreEqual(16384, ExcelColumnNumberingSystem.Default.Parse("XFD"))
        End Sub
    End Class

End Namespace