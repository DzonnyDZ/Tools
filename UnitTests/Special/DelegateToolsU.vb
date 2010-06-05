Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Tools.SpecialT.DelegateTools
Imports System.Collections.Generic

Namespace SpecialUT

    '''<summary>This is a test class for <see cref="Tools.SpecialT.DelegateTools"/> and is intended to contain all <see cref="Tools.SpecialT.DelegateTools"/> Unit Tests</summary>
    <TestClass()> _
    Public Class DelegateToolsUT


        Private testContextInstance As TestContext

        '''<summary>
        '''Gets or sets the test context which provides
        '''information about and functionality for the current test run.
        '''</summary>
        Public Property TestContext() As TestContext
            Get
                Return testContextInstance
            End Get
            Set(ByVal value As TestContext)
                testContextInstance = value
            End Set
        End Property

        <TestMethod()>
        Public Sub CombineWith_Delegate_Delegate()
            Dim a As Action = AddressOf CombineWith_Delegate_Delegate
            Dim b As Action = AddressOf Helper

            Dim c = a.CombineWith(b)

            Assert.AreEqual(2, c.GetInvocationList.Length)
            Assert.AreSame(a, c.GetInvocationList(0))
            Assert.AreSame(b, c.GetInvocationList(1))
        End Sub

        <TestMethod()>
        Public Sub CombineWith_Delegate_DelegateArray()
            Dim a As Action = AddressOf CombineWith_Delegate_DelegateArray
            Dim b As Action = AddressOf Helper
            Dim c As Action = Sub() Console.WriteLine()
            Dim d = Sub() Console.Write("a")

            Dim comb = a.CombineWith(b, c, d)

            Assert.AreEqual(4, comb.GetInvocationList.Length)
            Assert.AreEqual(a, comb.GetInvocationList(0))
            Assert.AreEqual(b, comb.GetInvocationList(1))
            Assert.AreEqual(c, comb.GetInvocationList(2))
            Assert.AreEqual(CType(d, Action), comb.GetInvocationList(3))
        End Sub

        Private Sub Helper()
        End Sub

        <TestMethod()>
        Public Sub RemoveOf()
            Dim a As Action = AddressOf CombineWith_Delegate_DelegateArray
            Dim b As Action = AddressOf Helper
            Dim c As Action = Sub() Console.WriteLine()
            Dim d = Sub() Console.Write("a")
            Dim comb = a.CombineWith(b, c, d)
            Assert.AreEqual(4, comb.GetInvocationList.Length)
            Assert.AreEqual(a, comb.GetInvocationList(0))
            Assert.AreEqual(b, comb.GetInvocationList(1))
            Assert.AreEqual(c, comb.GetInvocationList(2))
            Assert.AreEqual(CType(d, Action), comb.GetInvocationList(3))

            Assert.IsTrue(comb.GetInvocationList.Contains(c))
            comb = comb.Remove(c)
            Assert.AreEqual(3, comb.GetInvocationList.Length)
            Assert.IsFalse(comb.GetInvocationList.Contains(c))

            comb = a.CombineWith(b, c, d, c, b)
            Assert.AreEqual(6, comb.GetInvocationList.Length)
            Assert.AreEqual(c, comb.GetInvocationList(2))
            Assert.AreEqual(c, comb.GetInvocationList(4))
            comb = comb.Remove(c)
            Assert.AreEqual(c, comb.GetInvocationList(2))
            Assert.AreNotEqual(c, comb.GetInvocationList(4))
            Assert.AreEqual(1, (From del In comb.GetInvocationList Where del.Equals(c)).Count)
        End Sub

        <TestMethod()>
        Public Sub RemoveOfAll()
            Dim a As Action = AddressOf CombineWith_Delegate_DelegateArray
            Dim b As Action = AddressOf Helper
            Dim c As Action = Sub() Console.WriteLine()
            Dim d = Sub() Console.Write("a")
            Dim comb = a.CombineWith(b, c, d, c, b)
            Assert.AreEqual(6, comb.GetInvocationList.Length)
            Assert.AreEqual(2, (From del In comb.GetInvocationList Where del.Equals(c)).Count)
            comb = comb.RemoveAll(c)
            Assert.IsFalse(comb.GetInvocationList.Contains(c))
            Assert.AreEqual(4, comb.GetInvocationList.Length)
        End Sub



    End Class
End Namespace