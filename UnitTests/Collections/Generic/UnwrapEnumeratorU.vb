Imports System.Collections

Imports System.Collections.Generic

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Tools.CollectionsT.GenericT

Namespace CollectionsUT.GenericUT

    <TestClass()> _
    Public Class UnwrapEnumeratorUT


        Private testContextInstance As TestContext

        Public Property TestContext() As TestContext
            Get
                Return testContextInstance
            End Get
            Set(ByVal value As TestContext)
                testContextInstance = value
            End Set
        End Property

        <TestMethod()> Public Sub UnwrapEnumeratorU()
            Dim dic As New Dictionary(Of String, Integer)
            dic.Add("one", 1)
            dic.Add("two", 2)
            dic.Add("three", 3)
            Dim en As New UnwrapEnumerator(Of KeyValuePair(Of String, Integer), String)(dic.GetEnumerator, Function(a) a.Key)
            Assert.IsTrue(en.MoveNext)
            Assert.AreEqual("one", en.Current)
            Assert.IsTrue(en.MoveNext)
            Assert.AreEqual("two", en.Current)
            Assert.IsTrue(en.MoveNext)
            Assert.AreEqual("three", en.Current)
            Assert.IsFalse(en.MoveNext)
        End Sub
    End Class

 
End Namespace