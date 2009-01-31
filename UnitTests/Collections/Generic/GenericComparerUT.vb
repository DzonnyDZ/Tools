Imports System.Collections.Generic, System.Linq

Imports System

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Tools.CollectionsT.GenericT

Namespace CollectionsUT.GenericUT

    <TestClass()> _
    Public Class GenericComparerUT


        Private testContextInstance As TestContext

        Public Property TestContext() As TestContext
            Get
                Return testContextInstance
            End Get
            Set(ByVal value As TestContext)
                testContextInstance = value
            End Set
        End Property

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


        <TestMethod()> _
        Public Sub NewU()
            Dim Instance As New GenericComparer(Of String)(AddressOf HelperComparer)
            Dim l As New List(Of String)(New String() {"two", "one", "ten", "zero", "eleven", "nine"})
            l.Sort(Instance)
            Assert.IsTrue(l(0) = "zero")
            Assert.IsTrue(l(1) = "one")
            Assert.IsTrue(l(2) = "two")
            Assert.IsTrue(l(3) = "nine")
            Assert.IsTrue(l(4) = "ten")
            Assert.IsTrue(l(5) = "eleven")
        End Sub

        Private values As String() = {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve"}
        Private Function HelperComparer(ByVal a As String, ByVal b As String) As Integer
            Dim ai = Array.IndexOf(values, a)
            Dim bi = Array.IndexOf(values, b)
            Return ai.compareto(bi)
        End Function

        <TestMethod()> _
        Public Sub CompareU()
            Dim Instance As New GenericComparer(Of String)(AddressOf HelperComparer)
            Assert.IsTrue(Instance.Compare("one", "two") < 0)
            Assert.IsTrue(Instance.Compare("one", "five") < 0)
            Assert.IsTrue(Instance.Compare("one", "seven") < 0)
            Assert.IsTrue(Instance.Compare("ten", "twelve") < 0)
            Assert.IsTrue(Instance.Compare("one", "zero") > 0)
            Assert.IsTrue(Instance.Compare("ten", "eight") > 0)
            Assert.IsTrue(Instance.Compare("seven", "seven") = 0)
            Assert.IsTrue(Instance.Compare("ňñǧ", "ááá") = 0)
        End Sub

        
    End Class
End Namespace