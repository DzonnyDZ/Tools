Imports System

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Tools.ExtensionsT



'''<summary>
'''This is a test class for DateTimeExtensionsTest and is intended
'''to contain all DateTimeExtensionsTest Unit Tests
'''</summary>
<TestClass()> _
Public Class DateTimeExtensionsUT


    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(value As TestContext)
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

    '''<summary>
    '''A test for ToUnixTimestamp
    '''</summary>
    <TestMethod()> _
    Public Sub ToUnixTimestampTest()
        Assert.AreEqual(0L, unixEpoch.ToUnixTimestamp)
        Assert.AreEqual(15L, (unixEpoch + TimeSpan.FromSeconds(15)).ToUnixTimestamp)
        Assert.AreEqual(-15L, (unixEpoch + TimeSpan.FromSeconds(-15)).ToUnixTimestamp)
        Assert.AreEqual(1L * 24L * 60L * 60L, (unixEpoch + TimeSpan.FromDays(1)).ToUnixTimestamp)
        Assert.AreEqual(-1L * 24L * 60L * 60L, (unixEpoch - TimeSpan.FromDays(1)).ToUnixTimestamp)
        Assert.AreEqual(CLng(Math.Truncate((#1/1/2010 4:03:01 PM#.ToUniversalTime - unixEpoch).TotalSeconds)), #1/1/2010 4:03:01 PM#.ToUnixTimestamp)
        Assert.AreEqual(CLng(Math.Truncate((#1/1/2069 4:03:01 PM#.ToUniversalTime - unixEpoch).TotalSeconds)), #1/1/2069 4:03:01 PM#.ToUnixTimestamp)
        Assert.AreEqual(CLng(Math.Truncate((#1/1/1511 4:03:01 PM#.ToUniversalTime - unixEpoch).TotalSeconds)), #1/1/1511 4:03:01 PM#.ToUnixTimestamp)

        Assert.AreEqual(8L * 60L * 60L + Date.SpecifyKind(#6/8/1989 2:03:00 PM#, DateTimeKind.Utc).ToUnixTimestamp,
                        New DateTimeOffset(#6/8/1989 2:03:00 PM#, TimeSpan.FromHours(-8)).ToUnixTimestamp)
    End Sub


    '''<summary>
    '''A test for ToUnixTimestampShort
    '''</summary>
    <TestMethod()> _
    Public Sub ToUnixTimestampShortTest()
        Assert.AreEqual(0I, unixEpoch.ToUnixTimestampShort)
        Assert.AreEqual(15I, (unixEpoch + TimeSpan.FromSeconds(15)).ToUnixTimestampShort)
        Assert.AreEqual(-15I, (unixEpoch + TimeSpan.FromSeconds(-15)).ToUnixTimestampShort)
        Assert.AreEqual(Integer.MinValue, FromUnixTimestamp(Integer.MinValue).ToUnixTimestampShort)
        Assert.AreEqual(Integer.MaxValue, FromUnixTimestamp(Integer.MaxValue).ToUnixTimestampShort)
        Assert.AreEqual(0I, FromUnixTimestamp(CLng(Integer.MinValue) - 1L).ToUnixTimestampShort)
        Assert.AreEqual(0I, FromUnixTimestamp(CLng(Integer.MaxValue) + 1L).ToUnixTimestampShort)
        Assert.AreEqual(-1I, FromUnixTimestamp(CLng(Integer.MinValue) - 2L).ToUnixTimestampShort)
        Assert.AreEqual(1I, FromUnixTimestamp(CLng(Integer.MaxValue) + 2L).ToUnixTimestampShort)
    End Sub



    '''<summary>
    '''A test for ToUnixTimestampPrecise
    '''</summary>
    <TestMethod()> _
    Public Sub ToUnixTimestampPreciseTest()
        Assert.AreEqual(0.0R, unixEpoch.ToUnixTimestampPrecise)
        Assert.AreEqual(15.0R, (unixEpoch + TimeSpan.FromSeconds(15)).ToUnixTimestampPrecise)
        Assert.AreEqual(-15.0R, (unixEpoch + TimeSpan.FromSeconds(-15)).ToUnixTimestampPrecise)
        Assert.IsTrue(Math.Abs(15.9R - (unixEpoch + TimeSpan.FromSeconds(15.9)).ToUnixTimestampPrecise) <= 0.0000000000000020000000000000002R)
        Assert.IsTrue(Math.Abs(-15.9R - (unixEpoch + TimeSpan.FromSeconds(-15.9)).ToUnixTimestampPrecise) <= 0.0000000000000020000000000000002R)
    End Sub

    '''<summary>
    '''A test for FromUnixTimestamp
    '''</summary>
    <TestMethod()> _
    Public Sub FromUnixTimestampTest()
        Assert.AreEqual(unixEpoch, FromUnixTimestamp(0))
        Assert.AreEqual(unixEpoch + TimeSpan.FromSeconds(15), FromUnixTimestamp(15))
        Assert.AreEqual(unixEpoch - TimeSpan.FromSeconds(15), FromUnixTimestamp(-15))
        Assert.AreEqual(DateTimeKind.Utc, FromUnixTimestamp(0).Kind)
    End Sub


End Class
