Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Tools.ReflectionT
Imports System.Runtime.InteropServices

Namespace ReflectionUT

    '''<summary>
    '''This is a test class for FileSystemToolsTest and is intended
    '''to contain all FileSystemToolsTest Unit Tests
    '''</summary>
    <TestClass()> _
    Public Class ReflectionToolsUT


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

        Private Sub hssHA()
        End Sub
        Private Sub hssHB()
        End Sub
        Private Function hssHC() As Integer
        End Function
        Private Sub hssHD(ByVal x As Long)
        End Sub
        Private Sub hssHE(ByVal x As Integer)
        End Sub
        Private Sub hssHF(ByVal x As Integer)
        End Sub
        Private Sub hssHG(ByRef x As Integer)
        End Sub
        Private Delegate Sub RefAct(Of T)(ByRef x As T)
        Private Sub hssHI(<Out()> ByRef x As Integer)
        End Sub
        Private Sub hssHJ(<[In](), Out()> ByRef x As Integer)
        End Sub
        Private Function hssHK(ByRef DisconnectRoot As SByte) As Integer
        End Function
        Private Delegate Function RefFunc(Of T1, TRet)(ByRef x As t1) As tret
        <TestMethod()> Public Sub HasSameSignatureU()
            Assert.IsTrue(New Action(AddressOf hssHA).Method.HasSameSignature(New Action(AddressOf hssHA).Method, SignatureComparisonStrictness.Strict))
            Assert.IsTrue(New Action(AddressOf hssHA).Method.HasSameSignature(New Action(AddressOf hssHB).Method, SignatureComparisonStrictness.Strict))
            Assert.IsFalse(New Action(AddressOf hssHA).Method.HasSameSignature(New Func(Of Integer)(AddressOf hssHC).Method, SignatureComparisonStrictness.Strict))
            Assert.IsTrue(New Action(AddressOf hssHA).Method.HasSameSignature(New Func(Of Integer)(AddressOf hssHC).Method, SignatureComparisonStrictness.IgnoreReturn))
            Assert.IsFalse(New Action(AddressOf hssHA).Method.HasSameSignature(New Action(Of Long)(AddressOf hssHD).Method, SignatureComparisonStrictness.Strict))
            Assert.IsFalse(New Action(Of Integer)(AddressOf hssHE).Method.HasSameSignature(New Action(Of Long)(AddressOf hssHD).Method, SignatureComparisonStrictness.Strict))
            Assert.IsTrue(New Action(Of Integer)(AddressOf hssHE).Method.HasSameSignature(New Action(Of Integer)(AddressOf hssHF).Method, SignatureComparisonStrictness.Strict))
            Assert.IsFalse(New Action(Of Integer)(AddressOf hssHE).Method.HasSameSignature(New RefAct(Of Integer)(AddressOf hssHG).Method, SignatureComparisonStrictness.Strict))
            Assert.IsTrue(New Action(Of Integer)(AddressOf hssHE).Method.HasSameSignature(New RefAct(Of Integer)(AddressOf hssHG).Method, SignatureComparisonStrictness.IgnoreByRef))
            Assert.IsFalse(New RefAct(Of Integer)(AddressOf hssHI).Method.HasSameSignature(New RefAct(Of Integer)(AddressOf hssHJ).Method, SignatureComparisonStrictness.Strict))
            Assert.IsTrue(New RefAct(Of Integer)(AddressOf hssHI).Method.HasSameSignature(New RefAct(Of Integer)(AddressOf hssHJ).Method, SignatureComparisonStrictness.IgnoreDirection))
            Assert.IsTrue(New RefAct(Of Integer)(AddressOf hssHI).Method.HasSameSignature(New Action(Of Integer)(AddressOf hssHE).Method, SignatureComparisonStrictness.IgnoreDirection Or SignatureComparisonStrictness.IgnoreByRef))
            Dim FsDisconect = GetType(Tools.TotalCommanderT.FileSystemPlugin).GetMethod("FsDisconnect")
            Assert.IsFalse(FsDisconect.HasSameSignature(New RefFunc(Of SByte, Integer)(AddressOf Me.hssHK).Method, SignatureComparisonStrictness.Strict))
            Assert.IsTrue(FsDisconect.HasSameSignature(New RefFunc(Of SByte, Integer)(AddressOf Me.hssHK).Method, SignatureComparisonStrictness.IgnoreModOpt))
            Assert.IsFalse(FsDisconect.HasSameSignature(New RefFunc(Of SByte, Integer)(AddressOf Me.hssHK).Method, SignatureComparisonStrictness.IgnoreModReq))
            Assert.IsTrue(FsDisconect.HasSameSignature(New RefFunc(Of SByte, Integer)(AddressOf Me.hssHK).Method, SignatureComparisonStrictness.CLS))
        End Sub


        <TestMethod()> Public Sub GetBaseClassMethodU() 'TODO: IMplements test
            Assert.Inconclusive("Test not implemented")
        End Sub

    End Class
End Namespace