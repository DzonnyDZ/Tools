Imports System
Imports Tools.NumericsT
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Tools
Imports Tools.ExtensionsT  
Imports Tools.enumcore  

<TestClass()> _
Public Class EnumCoreTest

    Public Property TestContext() As TestContext

    <TestMethod()> _
    Public Sub GetFlagsTest_Enum()
        Dim flags = GetFlags(MyEnum.Flag1 Or MyEnum.Flag2 Or MyEnum.Flag3)
        Assert.AreEqual(3, flags.Length)
        Assert.IsTrue(flags.Contains(MyEnum.Flag1))
        Assert.IsTrue(flags.Contains(MyEnum.Flag2))
        Assert.IsTrue(flags.Contains(MyEnum.Flag3))
    End Sub

    <Flags()>
    Private Enum MyEnum
        Flag1 = 1
        Flag2 = 2
        Flag3 = 1024
        Flag4 = 2048
    End Enum

    <TestMethod()> _
    Public Sub GetFlagsTest_Int64()
        Dim flags = GetFlags(2L Or 4L Or 1024L)
        Assert.AreEqual(3, flags.Length)
        Assert.IsTrue(flags.Contains(2L))
        Assert.IsTrue(flags.Contains(4L))
        Assert.IsTrue(flags.Contains(1024L))
    End Sub

    <TestMethod()> _
    Public Sub GetFlagsTest_UInt32()
        Dim flags =GetFlags (2UI Or 4UI Or 1024UI)
        Assert.AreEqual(3, flags.Length)
        Assert.IsTrue(flags.Contains(2UI))
        Assert.IsTrue(flags.Contains(4UI))
        Assert.IsTrue(flags.Contains(1024UI))
    End Sub

    <TestMethod()> _
    Public Sub GetFlagsTest_UInt16()
        Dim flags = GetFlags(2US Or 4US Or 1024US)
        Assert.AreEqual(3, flags.Length)
        Assert.IsTrue(flags.Contains(2US))
        Assert.IsTrue(flags.Contains(4US))
        Assert.IsTrue(flags.Contains(1024US))
    End Sub

    <TestMethod()> _
    Public Sub GetFlagsTest_UInt64()
        Dim flags = GetFlags(2UL Or 4UL Or 1024UL)
        Assert.AreEqual(3, flags.Length)
        Assert.IsTrue(flags.Contains(2UL))
        Assert.IsTrue(flags.Contains(4UL))
        Assert.IsTrue(flags.Contains(1024UL))
    End Sub

    <TestMethod()> _
    Public Sub GetFlagsTest_Int16()
        Dim flags = GetFlags(2S Or 4S Or 1024S)
        Assert.AreEqual(3, flags.Length)
        Assert.IsTrue(flags.Contains(2S))
        Assert.IsTrue(flags.Contains(4S))
        Assert.IsTrue(flags.Contains(1024S))
    End Sub

    <TestMethod()> _
    Public Sub GetFlagsTest_Byte()
        Dim flags = GetFlags(CByte(2) Or CByte(4) Or CByte(128))
        Assert.AreEqual(3, flags.Length)
        Assert.IsTrue(flags.Contains(CByte(2)))
        Assert.IsTrue(flags.Contains(CByte(4)))
        Assert.IsTrue(flags.Contains(CByte(128)))
    End Sub

    <TestMethod()> _
    Public Sub GetFlagsTest_Int32()
        Dim flags = GetFlags(2I Or 4I Or 1024I)
        Assert.AreEqual(3, flags.Length)
        Assert.IsTrue(flags.Contains(2I))
        Assert.IsTrue(flags.Contains(4I))
        Assert.IsTrue(flags.Contains(1024I))
    End Sub

    <TestMethod()> _
    Public Sub GetFlagsTest_SByte()
        Dim flags = GetFlags(CSByte(2) Or CSByte(4) Or CByte(128).BitwiseSame)
        Assert.AreEqual(3, flags.Length)
        Assert.IsTrue(flags.Contains(CSByte(2)))
        Assert.IsTrue(flags.Contains(CSByte(4)))
        Assert.IsTrue(flags.Contains(CByte(128).BitwiseSame))
    End Sub
End Class
