Imports System.Collections
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Tools.GlobalizationT

Namespace GlobalizationUT
    <TestClass()> _
    Public Class NumberingSystemUT
        <TestMethod()> _
        Public Sub ConvertU_RomanUpperCase_RomanLowerCase()
            Assert.AreEqual("xvii", NumberingSystem.Convert("XVII", NumberingSystem.RomanUpperCase, NumberingSystem.RomanLowerCase))
            Assert.AreEqual("mlxii", NumberingSystem.Convert("MLXII", NumberingSystem.RomanUpperCase, NumberingSystem.RomanLowerCase))
            Assert.AreEqual("mdxlv", NumberingSystem.Convert("MDXLV", NumberingSystem.RomanUpperCase, NumberingSystem.RomanLowerCase))
        End Sub
        <TestMethod()> _
        Public Sub ConvertU_RomanLowerCase_RomanUperCase()
            Assert.AreEqual("XVII", NumberingSystem.Convert("xvii", NumberingSystem.RomanLowerCase, NumberingSystem.RomanUpperCase))
            Assert.AreEqual("MLXII", NumberingSystem.Convert("mlxii", NumberingSystem.RomanLowerCase, NumberingSystem.RomanUpperCase))
            Assert.AreEqual("MDXLV", NumberingSystem.Convert("mdxlv", NumberingSystem.RomanLowerCase, NumberingSystem.RomanUpperCase))
        End Sub
        <TestMethod()> _
        Public Sub ConvertU_RomanUpperCaseUnicode_RomanLowerCase()
            Assert.AreEqual("xvii", NumberingSystem.Convert("ⅩⅦ", NumberingSystem.RomanUnicodeUpperCase, NumberingSystem.RomanLowerCase))
            Assert.AreEqual("mlxii", NumberingSystem.Convert("ⅯⅬⅩⅡ", NumberingSystem.RomanUnicodeUpperCase, NumberingSystem.RomanLowerCase))
            Assert.AreEqual("mdxlv", NumberingSystem.Convert("ⅯⅮⅩⅬⅤ", NumberingSystem.RomanUnicodeUpperCase, NumberingSystem.RomanLowerCase))
        End Sub
    End Class
End Namespace