Imports System.Collections

Imports System.Collections.Generic

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Tools.CollectionsT.GenericT

Namespace CollectionsUT.GenericUT

    <TestClass()> _
    Public Class PriorityQueueUT


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
        '<ClassInitialize()> _
        'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
        'End Sub
        Private Instance As PriorityQueue(Of Integer)
        '
        'Use ClassCleanup to run code after all tests in a class have run
        '<ClassCleanup()>  _
        'Public Shared Sub MyClassCleanup()
        'End Sub
        '
        'Use TestInitialize to run code before running each test
        <TestInitialize()> _
        Public Sub MyTestInitialize()
            Instance = New PriorityQueue(Of Integer)(New GenericComparer(Of Integer)(Function(a%, b%) a.CompareTo(b)))
        End Sub
        '
        'Use TestCleanup to run code after each test has run
        '<TestCleanup()>  _
        'Public Sub MyTestCleanup()
        'End Sub
        '
#End Region
#Region "CTors"
        <ExpectedException(GetType(ArgumentNullException))> _
        <TestMethod()> Public Sub NewU()
            Dim ch = Comparer(Of GenericParameterHelper).Default
            Dim instance1 As New PriorityQueue(Of GenericParameterHelper)(ch)
            Assert.AreSame(ch, instance1.Comparer, "Comparer is same as set by CTor")
            Dim instance As New PriorityQueue(Of GenericParameterHelper)(Nothing)
            Assert.Fail("New PriorityQueue(Of T)(Nothing) should throw ArgumentNullException")
        End Sub
        <TestMethod()> _
        Public Sub NewU2()
            Dim ch = Comparer(Of GenericParameterHelper).Default
            Dim Instance As New PriorityQueue(Of GenericParameterHelper)(ch, PriorityTarget.MinimumFirst)
            Assert.AreSame(ch, Instance.Comparer, "Comparer is same as set by CTor")
            Assert.AreEqual(PriorityTarget.MinimumFirst, Instance.PriorityTarget, "PriorityTarget is MinimumFirst")
            Dim Instance2 As New PriorityQueue(Of GenericParameterHelper)(ch, PriorityTarget.MaximumFirst)
            Assert.AreEqual(PriorityTarget.MaximumFirst, Instance2.PriorityTarget, "PriorityTarget is MaximumFirst")
        End Sub
        <TestMethod()> _
        Public Sub NewU3()
            Dim ch = Comparer(Of Integer).Default
            Dim instance As New PriorityQueue(Of Integer)(New Integer() {12, 14, 22}, ch)
            Assert.IsTrue(instance.Contains(12) AndAlso instance.Contains(14) AndAlso instance.Contains(22), "Instance contains values passed in CTor")
        End Sub
        <TestMethod()> _
        Public Sub NewU3b()
            Dim ch = Comparer(Of Integer).Default
            Dim instance As New PriorityQueue(Of Integer)(New Integer() {12, 14, 22}, ch)
            Assert.IsTrue(instance(0) = 12 AndAlso instance(1) = 14 AndAlso instance(2) = 22, "Instance is correctly ordred")
            instance = New PriorityQueue(Of Integer)(New Integer() {14, 22, 12}, ch)
            Assert.IsTrue(instance(0) = 12 AndAlso instance(1) = 14 AndAlso instance(2) = 22, "Instance is correctly ordred")
        End Sub
        <TestMethod()> _
        Public Sub NewU4()
            Dim c As New GenericComparer(Of Integer)(Function(a, b) -a.CompareTo(b))
            Dim instance As New PriorityQueue(Of Integer)(New Integer() {14, 22, 12}, c)
            Assert.AreSame(c, instance.Comparer, "Comparer is that set by CTor")
            Assert.IsTrue(instance(0) = 22 AndAlso instance(1) = 14 AndAlso instance(2) = 12, "Instance is correctly ordered")
        End Sub
        <TestMethod()> _
        Public Sub NewU5()
            Dim Instance As New PriorityQueue(Of GenericParameterHelper)
            Assert.AreSame(Collections.Generic.Comparer(Of GenericParameterHelper).Default, Instance.Comparer, "Uses default comparer")
        End Sub
#End Region
        <TestMethod()> Public Sub PriorityTargetU()
            Instance.PriorityTarget = PriorityTarget.MaximumFirst
            Assert.AreEqual(PriorityTarget.MaximumFirst, Instance.PriorityTarget, "Priority target is MaximumFirst")
            Instance.PriorityTarget = PriorityTarget.MinimumFirst
            Assert.AreEqual(PriorityTarget.MinimumFirst, Instance.PriorityTarget, "Priority target is MinimumFirst")
        End Sub
        <TestMethod()> Public Sub Push_RemoveU()
            Instance.Clear()
            Instance.Push(14)
            Assert.IsTrue(Instance.Contains(14), "When pushed instance contains it")
            Instance.Remove(14)
            Assert.IsFalse(Instance.Contains(14), "When removed, instance doesn't contain it")
        End Sub
        <TestMethod()> Public Sub Pop_PeekU()
            Instance.Clear()
            Instance.PriorityTarget = PriorityTarget.MinimumFirst
            Instance.Push(22)
            Instance.Push(11)
            Instance.Push(11)
            Instance.Push(5)
            Instance.Push(10)
            Instance.Push(3)
            Instance.Push(28)
            Dim Peek%, Pop%
            Peek = Instance.Peek : Pop = Instance.Pop
            Assert.AreEqual(3, Peek, "Values are poke in corred order")
            Assert.AreEqual(3, Pop, "Values are popped in correct order")
            Peek = Instance.Peek : Pop = Instance.Pop
            Assert.AreEqual(5, Peek, "Values are poke in corred order")
            Assert.AreEqual(5, Pop, "Values are popped in correct order")
            Peek = Instance.Peek : Pop = Instance.Pop
            Assert.AreEqual(10, Peek, "Values are poke in corred order")
            Assert.AreEqual(10, Pop, "Values are popped in correct order")
            Peek = Instance.Peek : Pop = Instance.Pop
            Assert.AreEqual(11, Peek, "Values are poke in corred order")
            Assert.AreEqual(11, Pop, "Values are popped in correct order")
            Peek = Instance.Peek : Pop = Instance.Pop
            Assert.AreEqual(11, Peek, "Values are poke in corred order")
            Assert.AreEqual(11, Pop, "Values are popped in correct order")
            Peek = Instance.Peek : Pop = Instance.Pop
            Assert.AreEqual(22, Peek, "Values are poke in corred order")
            Assert.AreEqual(22, Pop, "Values are popped in correct order")
            Peek = Instance.Peek : Pop = Instance.Pop
            Assert.AreEqual(28, Peek, "Values are poke in corred order")
            Assert.AreEqual(28, Pop, "Values are popped in correct order")

            Instance.Clear()
            Instance.PriorityTarget = PriorityTarget.MaximumFirst
            Instance.Push(22)
            Instance.Push(11)
            Instance.Push(11)
            Instance.Push(5)
            Instance.Push(10)
            Instance.Push(3)
            Instance.Push(28)

            Peek = Instance.Peek : Pop = Instance.Pop
            Assert.AreEqual(28, Peek, "Values are poke in corred order")
            Assert.AreEqual(28, Pop, "Values are popped in correct order")
            Peek = Instance.Peek : Pop = Instance.Pop
            Assert.AreEqual(22, Peek, "Values are poke in corred order")
            Assert.AreEqual(22, Pop, "Values are popped in correct order")
            Peek = Instance.Peek : Pop = Instance.Pop
            Assert.AreEqual(11, Peek, "Values are poke in corred order")
            Assert.AreEqual(11, Pop, "Values are popped in correct order")
            Peek = Instance.Peek : Pop = Instance.Pop
            Assert.AreEqual(11, Peek, "Values are poke in corred order")
            Assert.AreEqual(11, Pop, "Values are popped in correct order")
            Peek = Instance.Peek : Pop = Instance.Pop
            Assert.AreEqual(10, Peek, "Values are poke in corred order")
            Assert.AreEqual(10, Pop, "Values are popped in correct order")
            Peek = Instance.Peek : Pop = Instance.Pop
            Assert.AreEqual(5, Peek, "Values are poke in corred order")
            Assert.AreEqual(5, Pop, "Values are popped in correct order")
            Peek = Instance.Peek : Pop = Instance.Pop
            Assert.AreEqual(3, Peek, "Values are poke in corred order")
            Assert.AreEqual(3, Pop, "Values are popped in correct order")
        End Sub
        <TestMethod()> Public Sub GetInsertIndexU()
            Instance.Clear()
            Instance.PriorityTarget = PriorityTarget.MinimumFirst
            Instance.Push(22) '5
            Instance.Push(11) '4
            Instance.Push(11) '3
            Instance.Push(5) '1
            Instance.Push(9) '2
            Instance.Push(3) '0
            Instance.Push(28) '6
            '3  5  9  11 11 22 28
            '0  1  2  3  4  5  6
            Assert.AreEqual(2, Instance.GetInsertIndex(9))
            Assert.AreEqual(4, Instance.GetInsertIndex(11))
            Assert.AreEqual(0, Instance.GetInsertIndex(0))
            Assert.AreEqual(5, Instance.GetInsertIndex(12))
            Assert.AreEqual(3, Instance.GetInsertIndex(10))
            Assert.AreEqual(7, Instance.GetInsertIndex(30))
        End Sub
        <TestMethod()> Public Sub PeekIndexU()
            Instance.Clear()
            Instance.PriorityTarget = PriorityTarget.MinimumFirst
            Instance.Push(22)
            Instance.Push(11)
            Instance.Push(11)
            Instance.Push(5)
            Instance.Push(10)
            Instance.Push(3)
            Instance.Push(28)
            Assert.AreEqual(0, Instance.PeekIndex, "PeekIndex respects PriorityTarget")
            Instance.PriorityTarget = PriorityTarget.MaximumFirst
            Assert.AreEqual(Instance.Count - 1, Instance.PeekIndex, "PeekIndex respects PriorityTarget")
        End Sub

        <TestMethod()> Public Sub Count_ClearU()
            Instance.Clear()
            Assert.AreEqual(0, Instance.Count, "Cleared instance has no items")
            Instance.Push(10) : Instance.Push(10) : Instance.Push(3)
            Assert.AreEqual(3, Instance.Count, "Cleared instance has 3 items")
            Instance.Clear()
            Assert.AreEqual(0, Instance.Count, "Cleared instance has no items")
        End Sub
        <TestMethod()> Public Sub ItemU()
            Instance.Clear()
            Instance.Push(3)
            Instance.Push(4)
            Instance.Push(1)
            Assert.AreEqual(1, Instance(0))
            Assert.AreEqual(3, Instance(1))
            Assert.AreEqual(4, Instance(2))
        End Sub
        <TestMethod()> Public Sub GetItemsWithSamePriorityU()
            Instance.Clear()
            Instance.Push(10)
            Instance.Push(3)
            Instance.Push(10)
            Instance.Push(11)
            Instance.Push(10)
            Instance.Push(10)
            Instance.Push(103)
            Dim sp = Instance.GetItemsWithSamePriority(10)
            Assert.IsTrue(sp.Count = (From i In sp Where i = 10).Count)
        End Sub
        <TestMethod()> Public Sub Remove_ContainsU()
            Instance.Clear()
            Assert.IsFalse(Instance.Contains(15))
            Instance.Push(15)
            Assert.IsTrue(Instance.Contains(15))
            Instance.Remove(15)
            Assert.IsFalse(Instance.Contains(15))
            Instance.Push(15)
            Instance.Push(15)
            Assert.IsTrue(Instance.Contains(15))
            Instance.Remove(15)
            Assert.IsTrue(Instance.Contains(15))
            Instance.Remove(15)
            Assert.IsFalse(Instance.Contains(15))
        End Sub
        <TestMethod()> Public Sub CopyToU()
            Instance.Clear()
            Instance.Push(10) : Instance.Push(3) : Instance.Push(11)
            Dim arr(Instance.Count - 1) As Integer
            Instance.CopyTo(arr, 0)
            Assert.IsTrue(arr(0) = Instance(0) AndAlso arr(1) = Instance(1) AndAlso arr(2) = Instance(2))
        End Sub
        <TestMethod()> Public Sub GetEnumeratorU()
            Instance.Clear()
            Instance.Push(10) : Instance.Push(3) : Instance.Push(11)
            Dim en = Instance.GetEnumerator
            For i As Integer = 0 To Instance.Count - 1
                Assert.IsTrue(en.MoveNext)
                Assert.AreEqual(Instance(i), en.Current)
            Next
            Assert.IsFalse(en.MoveNext)
        End Sub

        <TestMethod()> Public Sub IndexOfU()
            Instance.Clear()
            Instance.Push(10) : Instance.Push(3) : Instance.Push(11) : Instance.Push(11)
            Assert.AreEqual(0, Instance.IndexOf(3))
            Assert.AreEqual(1, Instance.IndexOf(10))
            Assert.AreEqual(2, Instance.IndexOf(11))
        End Sub

        <TestMethod()> Public Sub AddU()
            Instance.Clear()
            Instance.Push(10) : Instance.Push(3) : Instance.Push(11) : Instance.Push(11)
            Assert.AreEqual(10, Instance(1))
            DirectCast(Instance, IList(Of Integer))(1) = 105
            Assert.AreEqual(11, Instance(1))
            Assert.AreEqual(105, Instance(3))
        End Sub

        <TestMethod()> Public Sub SortU()
            Dim mul = 1
            Dim Comparer As New GenericComparer(Of Integer)(Function(a, b) mul * Collections.Generic.Comparer(Of Integer).Default.Compare(a, b))
            Dim instance As New PriorityQueue(Of Integer)(Comparer)
            instance.Push(10) : instance.Push(9) : instance.Push(7) : instance.Push(11) : instance.Push(8)
            Assert.IsTrue(instance(0) = 7 AndAlso instance(1) = 8 AndAlso instance(2) = 9 AndAlso instance(3) = 10 AndAlso instance(4) = 11)
            mul = -1
            instance.Sort()
            Assert.IsTrue(instance(4) = 7 AndAlso instance(3) = 8 AndAlso instance(2) = 9 AndAlso instance(1) = 10 AndAlso instance(0) = 11)
        End Sub

    End Class

    <TestClass()> _
    Public Class PriorityQueueUT2


        Private testContextInstance As TestContext

        Public Property TestContext() As TestContext
            Get
                Return testContextInstance
            End Get
            Set(ByVal value As TestContext)
                testContextInstance = value
            End Set
        End Property

        <TestMethod()> Public Sub NewU1()
            Dim Instance As New PriorityQueue(Of Integer, String)
            Assert.IsInstanceOfType(Instance.Comparer, GetType(GenericComparer(Of KeyValuePair(Of Integer, String))))
            Assert.IsTrue( _
                Instance.Comparer.Compare(New KeyValuePair(Of Integer, String)(1, "one"), New KeyValuePair(Of Integer, String)(2, "two")) < 0)
        End Sub

        <TestMethod()> Public Sub NewU2()
            Dim c As New GenericComparer(Of Integer)(Function(a, b) b.CompareTo(a))
            Dim Instance As New PriorityQueue(Of Integer, String)(c)
            Assert.IsInstanceOfType(Instance.Comparer, GetType(GenericComparer(Of KeyValuePair(Of Integer, String))))
            Assert.IsTrue( _
                Instance.Comparer.Compare(New KeyValuePair(Of Integer, String)(1, "one"), New KeyValuePair(Of Integer, String)(2, "two")) > 0)
        End Sub

        <TestMethod()> Public Sub Push_Pop_PeekU()
            Dim instance As New PriorityQueue(Of Integer, String)
            instance.PriorityTarget = PriorityTarget.MinimumFirst
            instance.Push(1, "one")
            instance.Push(3, "three")
            instance.Push(0, "zero")
            instance.Push(2, "two")
            instance.Push(4, "four")
            Assert.AreEqual(0, instance.PeekPriority)
            Assert.AreEqual("zero", instance.Peek)
            Assert.AreEqual("zero", instance.Pop)
            Assert.AreEqual(1, instance.PeekPriority)
            Assert.AreEqual("one", instance.Peek)
            Assert.AreEqual("one", instance.Pop)
            Assert.AreEqual(2, instance.PeekPriority)
            Assert.AreEqual("two", instance.Peek)
            Assert.AreEqual("two", instance.Pop)
            Assert.AreEqual(3, instance.PeekPriority)
            Assert.AreEqual("three", instance.Peek)
            Assert.AreEqual("three", instance.Pop)
            Assert.AreEqual(4, instance.PeekPriority)
            Assert.AreEqual("four", instance.Peek)
            Assert.AreEqual("four", instance.Pop)
        End Sub
        <TestMethod()> Public Sub IndexOf_Contains_RemoveU()
            Dim instance As New PriorityQueue(Of Integer, String)
            instance.PriorityTarget = PriorityTarget.MinimumFirst
            instance.Push(1, "one")
            Assert.IsTrue(instance.Contains("one"))
            Assert.IsFalse(instance.Contains("two"))
            instance.Push(3, "three")
            Assert.IsTrue(instance.Contains("three"))
            instance.Push(0, "zero")
            Assert.IsTrue(instance.Contains("zero"))
            instance.Push(2, "two")
            Assert.IsTrue(instance.Contains("two"))
            instance.Push(4, "four")
            Assert.IsTrue(instance.Contains("four"))
            Assert.AreEqual(0, instance.IndexOf("zero"))
            Assert.AreEqual(1, instance.IndexOf("one"))
            Assert.AreEqual(2, instance.IndexOf("two"))
            Assert.AreEqual(3, instance.IndexOf("three"))
            Assert.AreEqual(4, instance.IndexOf("four"))
            instance.Remove("one")
            Assert.IsFalse(instance.Contains("one"))
        End Sub
        <TestMethod()> Public Sub GetItemsWithSamePriorityU()
            Dim instance As New PriorityQueue(Of Integer, String)
            instance.Push(1, "one")
            instance.Push(2, "two")
            instance.Push(0, "zero")
            instance.Push(1, "jedna")
            instance.Push(1, "eins")
            instance.Push(1, "jedan")
            instance.Push(1, "один")
            instance.Push(1, "ένα")
            Assert.AreEqual(0, instance.GetItemsWithSamePriority(16).Count)
            Assert.AreEqual(1, instance.GetItemsWithSamePriority(2).Count)
            Assert.AreEqual("two", instance.GetItemsWithSamePriority(2).First)
            Dim wsp = instance.GetItemsWithSamePriority(1)
            Assert.IsTrue(wsp.Contains("jedna") AndAlso wsp.Contains("one") AndAlso wsp.Contains("eins") AndAlso wsp.Contains("jedan") AndAlso wsp.Contains("один") AndAlso wsp.Contains("ένα"))
            Assert.AreEqual(6, wsp.Count)
        End Sub
        <TestMethod()> Public Sub GetEnumeratorU()
            Dim instance As New PriorityQueue(Of Integer, String)
            instance.PriorityTarget = PriorityTarget.MinimumFirst
            instance.Push(1, "one")
            instance.Push(3, "three")
            instance.Push(0, "zero")
            instance.Push(2, "two")
            instance.Push(4, "four")
            Dim en = instance.GetEnumerator
            Assert.IsTrue(en.MoveNext)
            Assert.AreEqual("zero", en.Current)
            Assert.IsTrue(en.MoveNext)
            Assert.AreEqual("one", en.Current)
            Assert.IsTrue(en.MoveNext)
            Assert.AreEqual("two", en.Current)
            Assert.IsTrue(en.MoveNext)
            Assert.AreEqual("three", en.Current)
            Assert.IsTrue(en.MoveNext)
            Assert.AreEqual("four", en.Current)
            Assert.IsFalse(en.MoveNext)
        End Sub
    End Class
End Namespace