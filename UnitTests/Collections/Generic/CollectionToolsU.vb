Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Tools.CollectionsT.GenericT
Imports System.Collections.Generic

Namespace CollectionsUT.GenericUT

    '''<summary>
    '''This is a test class for CollectionToolsTest and is intended
    '''to contain all CollectionToolsTest Unit Tests
    '''</summary>
    <TestClass()> _
    Public Class CollectionToolsTestUT


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

#Region "GetTypedEnumerator"
        '''<summary>'A test for GetTypedEnumerator</summary>
        Public Sub GetTypedEnumeratorH(Of T As {New, Class})()
            Dim Array() As T = {New T, New T, New T, New T, New T, New T, New T, New T}
            Dim actual As TypedArrayEnumerator(Of T)
            actual = Array.GetTypedEnumerator
            Dim count% = 0
            While actual.MoveNext
                count += 1
            End While
            Assert.IsTrue(count = Array.Length, "GetTypedEnumerator returns an enumerator enumerating over same number of items as is in array")
            actual.Reset()
            actual = Array.GetTypedEnumerator
            Dim i As Integer = 0
            While actual.MoveNext
                Assert.IsTrue(actual.Current Is Array(i), "GetTypedEnumerator return an enumerator enumerating over items in array in array order")
                i += 1
            End While
        End Sub

        <TestMethod()> _
        Public Sub GetTypedEnumeratorU()
            GetTypedEnumeratorH(Of GenericParameterHelper)()
        End Sub
        Public Sub GetTypedEnumeratorH2(Of T As {New, Class})()
            Dim Array() As T = {New T, New T, New T, New T, New T, New T, New T, New T}
            Dim actual As TypedArrayEnumerator(Of T)
            actual = Array.GetTypedEnumerator(False)
            Dim count% = 0
            While actual.MoveNext
                count += 1
            End While
            Assert.IsTrue(count = Array.Length, "GetTypedEnumerator(False) returns an enumerator enumerating over same number of items as is in array")
            actual.Reset()
            actual = Array.GetTypedEnumerator(False)
            Dim i As Integer = 0
            While actual.MoveNext
                Assert.IsTrue(actual.Current Is Array(i), "GetTypedEnumerator(False) returns an enumerator enumerating over items in array in array order")
                i += 1
            End While

            actual = Array.GetTypedEnumerator(True)
            count% = 0
            While actual.MoveNext
                count += 1
            End While
            Assert.IsTrue(count = Array.Length, "GetTypedEnumerator(True) returns an enumerator enumerating over same number of items as is in array")
            actual.Reset()
            actual = Array.GetTypedEnumerator(True)
            i = 0
            While actual.MoveNext
                Assert.IsTrue(actual.Current Is Array(Array.Length - i - 1), "GetTypedEnumerator(True) returns an enumerator enumerating over items in array in array reverse order")
                i += 1
            End While
        End Sub
        <TestMethod()> _
        Public Sub GetTypedEnumerator2U()
            GetTypedEnumeratorH2(Of GenericParameterHelper)()
        End Sub
#End Region
#Region "Last"
        <TestMethod()> _
        Public Sub LastU()
            LastH(Of GenericParameterHelper)()
        End Sub
        Private Sub LastH(Of T As {New, Class})()
            Dim List As New List(Of T)(New T() {New T, New T, New T, New T, New T, New T, New T, New T})
            Dim Last = DirectCast(List, IEnumerable(Of T)).Last
            Assert.IsTrue(Last Is List(List.Count - 1), "Last returns last item of enumerable")
        End Sub
        <TestMethod()> _
        Public Sub Last2U()
            LastH(Of GenericParameterHelper)()
        End Sub
        Private Sub Last2H(Of T As {New, Class})()
            Dim List As New List(Of T)(New T() {New T, New T, New T, New T, New T, New T, New T, New T})
            Dim Last = DirectCast(List, IList(Of T)).Last
            Assert.IsTrue(Last Is List(List.Count - 1), "Last returns last item of list")
        End Sub
        <TestMethod()> _
        Public Sub Last3U()
            LastH(Of GenericParameterHelper)()
        End Sub
        Private Sub Last3H(Of T As {New, Class})()
            Dim a As T() = {New T, New T, New T, New T, New T, New T, New T, New T}
            Dim Last = a.Last
            Assert.IsTrue(Last Is a(a.Count - 1), "Last returns last item of array")
        End Sub

#End Region
#Region "AddRange"
        <TestMethod()> _
        Public Sub AddRangeU()
            AddRangeH(Of GenericParameterHelper)()
        End Sub
        Private Sub AddRangeH(Of T As {New, Class})()
            Dim col As ICollection(Of T) = New List(Of T)(New T() {New T, New T, New T})
            Dim addArray As T() = {New T, New T, New T}
            col.AddRange(addArray)
            Assert.IsTrue(addArray(0) Is col(3) AndAlso addArray(1) Is col(4) AndAlso addArray(2) Is col(5), "AddRange correctly adds item to colletion")
        End Sub
        <TestMethod()> _
        Public Sub AddRange2U()
            AddRangeH(Of GenericParameterHelper)()
        End Sub
        Private Sub AddRange2H(Of T As {New, Class})()
            Dim colorig As New ListWithEvents(Of T)(New T() {New T, New T, New T})
            Dim col As IAddable(Of T) = colorig
            Dim addArray As T() = {New T, New T, New T}
            col.AddRange(addArray)
            Assert.IsTrue(addArray(0) Is colorig(3) AndAlso addArray(1) Is colorig(4) AndAlso addArray(2) Is colorig(5), "AddRange correctly adds item to IAddable")
        End Sub

#End Region
    End Class
End Namespace