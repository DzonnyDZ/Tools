Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Collections.Generic
Imports System.Collections
Imports Tools.CollectionsT.GenericT

Namespace CollectionsUT.GenericUT


    ''' <summary>
    '''This is a test class for DuplicateDictionaryTest and is intended
    '''to contain all DuplicateDictionaryTest Unit Tests
    '''</summary>
    <TestClass()> _
    Public Class DuplicateDictionaryTest
        ''' <summary>
        '''Gets or sets the test context which provides
        '''information about and functionality for the current test run.
        '''</summary>
        Public Property TestContext() As TestContext
            Get
                Return m_TestContext
            End Get
            Set(ByVal value As TestContext)
                m_TestContext = value
            End Set
        End Property
        Private m_TestContext As TestContext

        Public Sub New()
            instance = New DuplicateDictionary(Of String, String)(array, AddressOf StringComparer.Ordinal.Compare)
        End Sub

        Private array As KeyValuePair(Of String, String)() = {New KeyValuePair(Of String, String)("a", "а"), New KeyValuePair(Of String, String)("d", "д"), New KeyValuePair(Of String, String)("g", "г"), New KeyValuePair(Of String, String)("b", "б"), New KeyValuePair(Of String, String)("x", "кс"), New KeyValuePair(Of String, String)("y", "ы"), _
         New KeyValuePair(Of String, String)("X", "КС"), New KeyValuePair(Of String, String)("k", "к"), New KeyValuePair(Of String, String)("k", "к"), New KeyValuePair(Of String, String)("š", "ш"), New KeyValuePair(Of String, String)("šč", "щ"), New KeyValuePair(Of String, String)("šč", "шч"), _
         New KeyValuePair(Of String, String)("", ""), New KeyValuePair(Of String, String)(Nothing, "")}
        Private instance As DuplicateDictionary(Of String, String)

        <TestMethod()> _
        Public Sub DuplicateDictionaryConstructorTest()

            Dim instance As New DuplicateDictionary(Of String, String)(array, AddressOf StringComparer.Ordinal.Compare)
            Assert.AreEqual(array.Length, instance.Count)
            Dim i As Integer = 0
            Assert.AreEqual(instance(i).Key, Nothing)
            Assert.AreEqual(instance(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)).Value, "")
            Assert.AreEqual(instance(i).Key, "")
            Assert.AreEqual(instance(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)).Value, "")
            Assert.AreEqual(instance(i).Key, "X")
            Assert.AreEqual(instance(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)).Value, "КС")
            Assert.AreEqual(instance(i).Key, "a")
            Assert.AreEqual(instance(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)).Value, "а")
            Assert.AreEqual(instance(i).Key, "b")
            Assert.AreEqual(instance(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)).Value, "б")
            Assert.AreEqual(instance(i).Key, "d")
            Assert.AreEqual(instance(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)).Value, "д")
            Assert.AreEqual(instance(i).Key, "g")
            Assert.AreEqual(instance(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)).Value, "г")
            Assert.AreEqual(instance(i).Key, "k")
            Assert.AreEqual(instance(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)).Value, "к")
            Assert.AreEqual(instance(i).Key, "k")
            Assert.AreEqual(instance(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)).Value, "к")
            Assert.AreEqual(instance(i).Key, "x")
            Assert.AreEqual(instance(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)).Value, "кс")
            Assert.AreEqual(instance(i).Key, "y")
            Assert.AreEqual(instance(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)).Value, "ы")
            Assert.AreEqual(instance(i).Key, "š")
            Assert.AreEqual(instance(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)).Value, "ш")
            Assert.AreEqual(instance(i).Key, "šč")
            Assert.AreEqual(instance(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)).Value, "щ")
            Assert.AreEqual(instance(i).Key, "šč")
            Assert.AreEqual(instance(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)).Value, "шч")
        End Sub


        <TestMethod()> _
        Public Sub ContainsKeyTest()
            For Each item In array
                Assert.IsTrue(instance.ContainsKey(item.Key))
            Next
            Assert.IsFalse(instance.ContainsKey("n"))
            Assert.IsFalse(instance.ContainsKey("A"))
        End Sub

        <TestMethod()> _
        Public Sub CopyToTest()
            Dim array = New KeyValuePair(Of String, String)(instance.Count - 1) {}
            instance.CopyTo(array, 0)
            For i As Integer = 0 To array.Length - 1
                Assert.AreEqual(instance(i).Key, array(i).Key)
                Assert.AreEqual(instance(i).Value, array(i).Value)
            Next
        End Sub

        <TestMethod()> _
        Public Sub FirstIndexOfTest()
            Dim i As Integer = 0
            Assert.AreEqual(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1), instance.FirstIndexOf(Nothing))
            Assert.AreEqual(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1), instance.FirstIndexOf(""))
            Assert.AreEqual(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1), instance.FirstIndexOf("X"))
            Assert.AreEqual(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1), instance.FirstIndexOf("a"))
            Assert.AreEqual(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1), instance.FirstIndexOf("b"))
            Assert.AreEqual(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1), instance.FirstIndexOf("d"))
            Assert.AreEqual(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1), instance.FirstIndexOf("g"))
            Assert.AreEqual(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1), instance.FirstIndexOf("k"))
            i += 1
            Assert.AreEqual(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1), instance.FirstIndexOf("x"))
            Assert.AreEqual(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1), instance.FirstIndexOf("y"))
            Assert.AreEqual(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1), instance.FirstIndexOf("š"))
            Assert.AreEqual(System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1), instance.FirstIndexOf("šč"))
            i += 1
        End Sub

        <TestMethod()> _
        Public Sub GetAllTest()
            Assert.AreEqual(0, instance.GetAll("ň").Length)
            Assert.AreEqual(1, instance.GetAll("a").Length)
            Assert.AreEqual("д", instance.GetAll("d")(0))
            Assert.AreEqual(2, instance.GetAll("šč").Length)
            Assert.IsTrue(instance.GetAll("šč")(0) = "щ" OrElse instance.GetAll("šč")(0) = "шч")
            Assert.IsTrue(instance.GetAll("šč")(1) = "щ" OrElse instance.GetAll("šč")(1) = "шч")
        End Sub

        <TestMethod()> _
        Public Sub GetEnumeratorTest()
            Using enumerator = instance.GetEnumerator()
                Dim i As Integer = 0
                While enumerator.MoveNext()
                    Assert.AreEqual(instance(i), enumerator.Current)
                    i += 1
                End While
            End Using
        End Sub


        <TestMethod()> _
        Public Sub GetFirstTest()
            Assert.AreEqual(instance.GetFirst(Nothing), "")
            Assert.AreEqual(instance.GetFirst(""), "")
            Assert.AreEqual(instance.GetFirst("a"), "а")
            Assert.AreEqual(instance.GetFirst("d"), "д")
            Assert.AreEqual(instance.GetFirst("g"), "г")
            Assert.AreEqual(instance.GetFirst("b"), "б")
            Assert.AreEqual(instance.GetFirst("x"), "кс")
            Assert.AreEqual(instance.GetFirst("y"), "ы")
            Assert.AreEqual(instance.GetFirst("X"), "КС")
            Assert.AreEqual(instance.GetFirst("k"), "к")
            Assert.AreEqual(instance.GetFirst("š"), "ш")

            Assert.IsTrue(instance.GetFirst("šč") = "щ" OrElse instance.GetFirst("šč") = "шч")
        End Sub


        <TestMethod()> _
        Public Sub LastIndexOfTest()
            Assert.AreEqual(instance.FirstIndexOf(Nothing), instance.LastIndexOf(Nothing))
            Assert.AreEqual(instance.FirstIndexOf(""), instance.LastIndexOf(""))
            Assert.AreEqual(instance.FirstIndexOf("X"), instance.LastIndexOf("X"))
            Assert.AreEqual(instance.FirstIndexOf("a"), instance.LastIndexOf("a"))
            Assert.AreEqual(instance.FirstIndexOf("b"), instance.LastIndexOf("b"))
            Assert.AreEqual(instance.FirstIndexOf("d"), instance.LastIndexOf("d"))
            Assert.AreEqual(instance.FirstIndexOf("g"), instance.LastIndexOf("g"))
            Assert.AreEqual(instance.FirstIndexOf("k") + 1, instance.LastIndexOf("k"))
            Assert.AreEqual(instance.FirstIndexOf("x"), instance.LastIndexOf("x"))
            Assert.AreEqual(instance.FirstIndexOf("y"), instance.LastIndexOf("y"))
            Assert.AreEqual(instance.FirstIndexOf("š"), instance.LastIndexOf("š"))
            Assert.AreEqual(instance.FirstIndexOf("šč") + 1, instance.LastIndexOf("šč"))
        End Sub

        <TestMethod()> _
        <DeploymentItem("RFERL.Core.dll")> _
        Public Sub PairComparisonTest()
            Dim acc As New PrivateObject(instance)
            Assert.AreEqual(instance.Comparison("a", "a"), acc.Invoke("PairComparison", New KeyValuePair(Of String, String)("a", "xxx"), New KeyValuePair(Of String, String)("a", "yyy")))
            Assert.AreEqual(instance.Comparison("a", "b"), acc.Invoke("PairComparison", New KeyValuePair(Of String, String)("a", "xxx"), New KeyValuePair(Of String, String)("b", "yyy")))
            Assert.AreEqual(instance.Comparison("b", "a"), acc.Invoke("PairComparison", New KeyValuePair(Of String, String)("b", "xxx"), New KeyValuePair(Of String, String)("a", "yyy")))
        End Sub

        <TestMethod()> _
        Public Sub Clone()
            Dim clone = instance.Clone()
            Assert.AreEqual(instance.Count, clone.Count)
            Assert.AreSame(instance.Comparison, clone.Comparison)
            For i As Integer = 0 To instance.Count - 1
                Assert.AreEqual(instance(i).Key, clone(i).Key)
                Assert.AreEqual(instance(i).Value, clone(i).Value)
            Next
        End Sub

        <TestMethod()> _
        Public Sub RemoveTest()

            Dim clone = instance.Clone()
            Assert.IsFalse(clone.Remove("ž"))
            Assert.IsTrue(clone.Remove("b"))
            Assert.AreEqual(instance.Count - 1, clone.Count)
            Assert.IsFalse(clone.ContainsKey("b"))
            Assert.IsTrue(clone.Remove("šč"))
            Assert.AreEqual(instance.Count - 2, clone.Count)
            Assert.AreEqual(clone.FirstIndexOf("šč"), clone.LastIndexOf("šč"))
            Assert.IsTrue(clone.Remove("šč"))
            Assert.AreEqual(instance.Count - 3, clone.Count)
            Assert.IsFalse(clone.ContainsKey("šč"))
            Assert.IsFalse(clone.Remove("šč"))
        End Sub

        <TestMethod()> _
        <DeploymentItem("RFERL.Core.dll")> _
        Public Sub AddTest()
            Dim clone = instance.Clone()
            Dim dicClone As IDictionary(Of String, String) = clone
            dicClone.Add("c", "ц")
            Assert.AreEqual(instance.Count + 1, clone.Count)
            Assert.AreEqual(clone.LastIndexOf("b") + 1, clone.FirstIndexOf("c"))
            dicClone.Add("c", "тс")
            Assert.AreEqual(instance.Count + 2, clone.Count)
            Assert.AreEqual(2, clone.GetAll("c").Length)
        End Sub

        <TestMethod()> _
        <DeploymentItem("RFERL.Core.dll")> _
        Public Sub ClearTest()
            Dim clone = instance.Clone()
            Dim dicClone As IDictionary(Of String, String) = clone
            dicClone.Clear()
            Assert.AreEqual(0, clone.Count)
        End Sub

        <TestMethod()> _
        <DeploymentItem("RFERL.Core.dll")> _
        Public Sub ContainsTest()
            Dim dic As IDictionary(Of String, String) = instance
            Assert.IsFalse(dic.Contains(New KeyValuePair(Of String, String)("ň", "њ")))
            Assert.IsFalse(dic.Contains(New KeyValuePair(Of String, String)("a", "А")))
            Assert.IsTrue(dic.Contains(New KeyValuePair(Of String, String)("a", "а")))
        End Sub


        <TestMethod()> _
        <DeploymentItem("RFERL.Core.dll")> _
        Public Sub RemoveTest1()
            Dim clone = instance.Clone()
            Dim dic As IDictionary(Of String, String) = clone
            Assert.IsFalse(dic.Remove(New KeyValuePair(Of String, String)("ň", "њ")))
            Assert.IsFalse(dic.Remove(New KeyValuePair(Of String, String)("a", "А")))
            Assert.IsTrue(dic.Remove(New KeyValuePair(Of String, String)("a", "а")))
            Assert.AreEqual(instance.Count - 1, clone.Count)
        End Sub

        <TestMethod()> _
        <DeploymentItem("RFERL.Core.dll")> _
        Public Sub AddTest1()
            Dim clone = instance.Clone()
            Dim dicClone As IDictionary(Of String, String) = clone
            dicClone.Add(New KeyValuePair(Of String, String)("c", "ц"))
            Assert.AreEqual(instance.Count + 1, clone.Count)
            Assert.AreEqual(clone.LastIndexOf("b") + 1, clone.FirstIndexOf("c"))
            dicClone.Add(New KeyValuePair(Of String, String)("c", "тс"))
            Assert.AreEqual(instance.Count + 2, clone.Count)
            Assert.AreEqual(2, clone.GetAll("c").Length)
        End Sub

        <TestMethod()> _
        Public Sub TryGetValueTest()
            Dim value As String = Nothing
            Assert.IsTrue(instance.TryGetValue("a", value))
            Assert.AreEqual("а", value)
            Assert.IsTrue(instance.TryGetValue("šč", value))
            Assert.IsTrue(value = "щ" OrElse value = "шч")
            Assert.IsFalse(instance.TryGetValue("ň", value))
        End Sub

        <TestMethod()> _
        Public Sub ComparisonTest()
            Assert.AreEqual(DirectCast(AddressOf StringComparer.Ordinal.Compare, Comparison(Of String)), instance.Comparison)
        End Sub

        <TestMethod()> _
        Public Sub CountTest()
            Assert.AreEqual(array.Length, instance.Count)
        End Sub

        <TestMethod()> _
        Public Sub ItemTest()
            Assert.AreEqual(instance("a"), "а")
            Assert.AreEqual(instance(""), "")
            Assert.AreEqual(instance(CStr(Nothing)), "")
            Assert.IsTrue(instance("šč") = "шч" OrElse instance("šč") = "щ")
            Dim exc As Exception = Nothing
            Try
                Dim val As String = instance("ň")
            Catch ex As Exception
                exc = ex
            End Try
            Assert.IsInstanceOfType(exc, GetType(KeyNotFoundException))

            Dim clone = instance.Clone()
            clone("a") = "А"
            Assert.AreEqual("А", clone("a"))
            clone("šč") = "сц"
            Assert.AreEqual("сц", clone("šč"))
            Dim array__1 = clone.GetAll("šč")
            Assert.IsTrue(System.Array.IndexOf(array__1, "сц") >= 0)
            Assert.IsTrue(System.Array.IndexOf(array__1, "щ") >= 0 OrElse System.Array.IndexOf(array__1, "шч") >= 0)

            exc = Nothing
            Try
                instance("ň") = "њ"
            Catch ex As Exception
                exc = ex
            End Try
            Assert.IsInstanceOfType(exc, GetType(KeyNotFoundException))
        End Sub

        <TestMethod()> _
        Public Sub ItemTest1()
            'This mainly is tested by CTor test
            Dim no As Integer = 0
            Dim i As Integer = instance.FirstIndexOf("šč")
            While i <= instance.LastIndexOf("šč")
                Assert.AreEqual("šč", instance(i).Key)
                Assert.IsTrue(instance(i).Value = "шч" OrElse instance(i).Value = "щ")
                i += 1
                no += 1
            End While
            Assert.AreEqual(2, no)

            Dim exc As Exception = Nothing
            Try
                Dim val = instance(instance.Count)
            Catch ex As Exception
                exc = ex
            End Try
            Assert.IsInstanceOfType(exc, GetType(ArgumentOutOfRangeException))

            exc = Nothing
            Try
                Dim val = instance(-1)
            Catch ex As Exception
                exc = ex
            End Try
            Assert.IsInstanceOfType(exc, GetType(ArgumentOutOfRangeException))
        End Sub

        <TestMethod()> _
        Public Sub KeysTest()
            Dim keys = instance.Keys
            Assert.AreEqual(instance.Count, keys.Count)
            Using dicE = instance.GetEnumerator()
                Using keE = keys.GetEnumerator()
                    While dicE.MoveNext() Or keE.MoveNext()
                        '| is here intentionaly
                        Assert.AreEqual(dicE.Current.Key, keE.Current)
                    End While
                End Using
            End Using
        End Sub

        <TestMethod()> _
        <DeploymentItem("RFERL.Core.dll")> _
        Public Sub IsReadOnlyTest()
            Assert.AreEqual(False, DirectCast(instance, IDictionary(Of String, String)).IsReadOnly)
        End Sub

        <TestMethod()> _
        Public Sub ValuesTest()
            Dim values = instance.Values
            Assert.AreEqual(instance.Count, values.Count)
            Using dicE = instance.GetEnumerator()
                Using valE = values.GetEnumerator()
                    While dicE.MoveNext() Or valE.MoveNext()
                        '| is here intentionaly
                        Assert.AreEqual(dicE.Current.Value, valE.Current)
                    End While
                End Using
            End Using
        End Sub
    End Class
End Namespace
