Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Globalization.CultureInfo
Imports System.Collections.Generic
Imports System.Reflection
Imports Tools.EnumCore

''' <summary>This is a test class for CollectionToolsTest and is intended to contain all CollectionToolsTest Unit Tests</summary>
<TestClass()> 
Public Class EnumCoreUT
    Private testContextInstance As TestContext

    ''' <summary>
    ''' Gets or sets the test context which provides
    ''' information about and functionality for the current test run.
    ''' </summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property
    
    <TestMethod()> 
    Public Sub IsDefinedU()
        Assert.IsTrue(EnumCore.IsDefined(AppDomainManagerInitializationOptions.None), "IsDefined identifies returned false for member that is defined")
        Assert.IsFalse(EnumCore.IsDefined(CType(14, AppDomainManagerInitializationOptions)), "IsDefined identifies that member is not defined")
    End Sub

    <TestMethod()>
    Public Sub GetConstantU()
        Dim constant = EnumCore.GetConstant(AppWinStyle.MaximizedFocus)
        Assert.IsTrue(constant.Name = "MaximizedFocus" AndAlso constant.DeclaringType.Equals(GetType(AppWinStyle)), "GetConstant correctly gets constant")
        constant = EnumCore.GetConstant(CType(2104, AppWinStyle))
        Assert.IsNull(constant, "GetConstant returns null when constant is not defined")
    End Sub

    <TestMethod()>
    Public Sub HasFlagSetU()
        Dim flags As BindingFlags = BindingFlags.GetField Or BindingFlags.Static Or BindingFlags.Public
        Assert.IsTrue(HasFlagSet(flags,BindingFlags.GetField))
        Assert.IsTrue(HasFlagSet(flags,BindingFlags.Static))
        Assert.IsTrue(HasFlagSet(flags,BindingFlags.Static Or BindingFlags.Public))
        Assert.IsFalse(HasFlagSet(flags,BindingFlags.Instance))
    End Sub

    <TestMethod()>
    Public Sub GetValuesU()
        Dim values = EnumCore.GetValues(Of AssemblyNameFlags)()
        Dim values2 = [Enum].GetValues(GetType(AssemblyNameFlags))
        Assert.AreEqual(values2.Length, values.Length)
        For Each value2 In values2
            Assert.IsTrue(values.Contains(value2))
        Next
    End Sub

    <TestMethod()>
    Public Sub HasFlagSet_DictionaryU()
        Dim dictionary As New Dictionary(Of String, BindingFlags)
        dictionary.Add("A", BindingFlags.DeclaredOnly Or BindingFlags.SetField)
        dictionary.Add("B", BindingFlags.CreateInstance Or BindingFlags.IgnoreCase Or BindingFlags.ExactBinding)
        dictionary.Add("C", BindingFlags.Public Or BindingFlags.SetField)

        Assert.IsTrue(HasFlagSet (dictionary,BindingFlags.DeclaredOnly))
        Assert.IsTrue(HasFlagSet (dictionary,BindingFlags.DeclaredOnly Or BindingFlags.SetField))
        Assert.IsTrue(HasFlagSet (dictionary,BindingFlags.CreateInstance Or BindingFlags.ExactBinding))
        Assert.IsFalse(HasFlagSet(dictionary,BindingFlags.Public Or BindingFlags.ExactBinding))
        Assert.IsFalse(HasFlagSet(dictionary,BindingFlags.NonPublic))

    End Sub

    <TestMethod()>
    Public Sub ParseU()
        Assert.AreEqual(BindingFlags.NonPublic, EnumCore.Parse(Of BindingFlags)("NonPublic"))
        Assert.AreEqual(BindingFlags.NonPublic, EnumCore.Parse(Of BindingFlags)("nonPuBlic", True))
        Assert.AreEqual(BindingFlags.NonPublic, EnumCore.Parse(Of BindingFlags)(BindingFlags.NonPublic.ToString("d")))
        Assert.AreEqual(BindingFlags.PutDispProperty Or BindingFlags.Static, EnumCore.Parse(Of BindingFlags)("Static, PutDispProperty"))

        Dim ex As Exception = Nothing
        Try
            EnumCore.Parse(Of BindingFlags)("nonPublic")
        Catch ex : End Try
        Assert.IsInstanceOfType(ex, GetType(ArgumentException))

        ex = Nothing
        Try
            EnumCore.Parse(Of BindingFlags)("nonPublic", False)
        Catch ex : End Try
        Assert.IsInstanceOfType(ex, GetType(ArgumentException))

        ex = Nothing
        Try
            EnumCore.Parse(Of BindingFlags)("abrakadabraka")
        Catch ex : End Try
        Assert.IsInstanceOfType(ex, GetType(ArgumentException))

        ex = Nothing
        Try
            EnumCore.Parse(Of BindingFlags)((CLng(Integer.MaxValue) + 105L).ToString(InvariantCulture))
        Catch ex : End Try
        Assert.IsInstanceOfType(ex, GetType(OverflowException))
    End Sub

    <TestMethod()>
    Public Sub TryParseU()
        Dim val As BindingFlags
        Assert.IsTrue(EnumCore.TryParse(Of BindingFlags)("NonPublic", val))
        Assert.AreEqual(BindingFlags.NonPublic, val)
        Assert.IsTrue(EnumCore.TryParse(Of BindingFlags)("nonPuBlic", True, val))
        Assert.AreEqual(BindingFlags.NonPublic, val)
        Assert.IsTrue(EnumCore.TryParse(Of BindingFlags)(BindingFlags.NonPublic.ToString("d"), val))
        Assert.AreEqual(BindingFlags.NonPublic, val)
        Assert.IsTrue(EnumCore.TryParse(Of BindingFlags)("Static, PutDispProperty", val))
        Assert.AreEqual(BindingFlags.Static Or BindingFlags.PutDispProperty, val)

        Assert.IsFalse(EnumCore.TryParse(Of BindingFlags)("nonPublic", val))
        Assert.IsFalse(EnumCore.TryParse(Of BindingFlags)("nonPublic", False, val))
        Assert.IsFalse(EnumCore.TryParse(Of BindingFlags)("abrakadabraka", val))
        Assert.IsFalse(EnumCore.TryParse(Of BindingFlags)((CLng(Integer.MaxValue) + 105L).ToString(InvariantCulture), val))
    End Sub

End Class