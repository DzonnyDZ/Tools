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
            Dim hssHA As System.Reflection.MethodInfo = New Action(AddressOf Me.hssHA).Method
            Dim hssHB As System.Reflection.MethodInfo = New Action(AddressOf Me.hssHB).Method
            Dim hssHC As System.Reflection.MethodInfo = New Func(Of Integer)(AddressOf Me.hssHC).Method
            Dim hssHD As System.Reflection.MethodInfo = New Action(Of Long)(AddressOf Me.hssHD).Method
            Dim hssHE As System.Reflection.MethodInfo = New Action(Of Integer)(AddressOf Me.hssHE).Method
            Dim hssHF As System.Reflection.MethodInfo = New Action(Of Integer)(AddressOf Me.hssHF).Method
            Dim hssHG As System.Reflection.MethodInfo = New RefAct(Of Integer)(AddressOf Me.hssHG).Method
            Dim hssHI As System.Reflection.MethodInfo = New RefAct(Of Integer)(AddressOf Me.hssHI).Method
            Dim hssHJ As System.Reflection.MethodInfo = New RefAct(Of Integer)(AddressOf Me.hssHJ).Method
            Dim hssHK As System.Reflection.MethodInfo = New RefFunc(Of SByte, Integer)(AddressOf Me.hssHK).Method
            Dim FsDisconect = Reflection.Assembly.Load("Tools.TotalCommander").GetType("Tools.TotalCommanderT.FileSystemPlugin").GetMethod("FsDisconnect")

            Assert.IsTrue(hssHA.HasSameSignature(hssHA, SignatureComparisonStrictness.Strict))
            Assert.IsTrue(hssHA.HasSameSignature(hssHB, SignatureComparisonStrictness.Strict))
            Assert.IsFalse(hssHA.HasSameSignature(hssHC, SignatureComparisonStrictness.Strict))
            Assert.IsTrue(hssHA.HasSameSignature(hssHC, SignatureComparisonStrictness.IgnoreReturn))
            Assert.IsFalse(hssHA.HasSameSignature(hssHD, SignatureComparisonStrictness.Strict))
            Assert.IsFalse(hssHE.HasSameSignature(hssHD, SignatureComparisonStrictness.Strict))
            Assert.IsTrue(hssHE.HasSameSignature(hssHF, SignatureComparisonStrictness.Strict))
            Assert.IsFalse(hssHE.HasSameSignature(hssHG, SignatureComparisonStrictness.Strict))
            Assert.IsTrue(hssHE.HasSameSignature(hssHG, SignatureComparisonStrictness.IgnoreByRef))
            Assert.IsFalse(hssHI.HasSameSignature(hssHJ, SignatureComparisonStrictness.Strict))
            Assert.IsTrue(hssHI.HasSameSignature(hssHJ, SignatureComparisonStrictness.IgnoreDirection))
            Assert.IsTrue(hssHI.HasSameSignature(hssHE, SignatureComparisonStrictness.IgnoreDirection Or SignatureComparisonStrictness.IgnoreByRef))
            Assert.IsFalse(FsDisconect.HasSameSignature(hssHK, SignatureComparisonStrictness.Strict))
            'Assert.IsFalse(FsDisconect.HasSameSignature(hssHK, SignatureComparisonStrictness.TreatPointerAsReference))
            Assert.IsTrue(FsDisconect.HasSameSignature(hssHK, SignatureComparisonStrictness.IgnoreModOpt Or SignatureComparisonStrictness.TreatPointerAsReference))
            'Assert.IsFalse(FsDisconect.HasSameSignature(hssHK, SignatureComparisonStrictness.IgnoreModReq Or SignatureComparisonStrictness.TreatPointerAsReference))
            Assert.IsTrue(FsDisconect.HasSameSignature(hssHK, SignatureComparisonStrictness.CLS Or SignatureComparisonStrictness.TreatPointerAsReference))
        End Sub


        Private Class BaseClass
            Public Overridable Sub Method(ByVal a As Integer, ByVal b As Long)
            End Sub
            Public Overridable Sub Method(ByVal a As Integer, ByVal b As Integer)
            End Sub
            Public Overridable Sub Method(ByVal a As Char, ByVal b As Integer)
            End Sub
            Public Overridable ReadOnly Property SomeProperty%()
                Get
                End Get
            End Property
        End Class
        Private Class DerivedClass1
            Inherits BaseClass
            Public Overrides Sub Method(ByVal a As Integer, ByVal b As Integer)
                MyBase.Method(a, b)
            End Sub
            Public NotOverridable Overrides Sub Method(ByVal a As Integer, ByVal b As Long)
                MyBase.Method(a, b)
            End Sub
            Public Overloads Sub Method(ByVal a As String, ByVal b As SByte)
            End Sub
            Public Overrides ReadOnly Property SomeProperty() As Integer
                Get
                    Return MyBase.SomeProperty
                End Get
            End Property
        End Class
        Private Class DerivedClass2
            Inherits DerivedClass1
            Overridable Overloads Sub Method(ByVal a As Integer, ByVal b As Long)
            End Sub
            Public Overloads Overrides Sub Method(ByVal a As Char, ByVal b As Integer)
                MyBase.Method(a, b)
            End Sub
        End Class
        Private Class DerivedClass3
            Inherits DerivedClass2
            Public Overrides Sub Method(ByVal a As Integer, ByVal b As Long)
                MyBase.Method(a, b)
            End Sub
        End Class


        <TestMethod()> Public Sub GetBaseClassMethodU() 'TODO: IMplements test
            Dim BaseClass = GetType(BaseClass)
            Dim DerivedClass1 = GetType(DerivedClass1)
            Dim DerivedClass2 = GetType(DerivedClass2)
            Dim DerivedClass3 = GetType(DerivedClass3)

            Dim Integer_Long As Type() = New Type() {GetType(Integer), GetType(Long)}
            Dim Char_Integer As Type() = New Type() {GetType(Char), GetType(Integer)}
            Dim Integer_Integer As Type() = New Type() {GetType(Integer), GetType(Integer)}
            Dim String_SByte As Type() = New Type() {GetType(String), GetType(SByte)}

            Dim BaseClass_Method_Integer_Long = BaseClass.GetMethod("Method", Integer_Long)
            Dim BaseClass_Method_Integer_Integer = BaseClass.GetMethod("Method", Integer_Integer)
            Dim BaseClass_Method_Char_Integer = BaseClass.GetMethod("Method", Char_Integer)
            Dim BaseClass_SomeProperty_Get = BaseClass.GetProperty("SomeProperty").GetGetMethod
            Dim DerivedClass1_Method_Integer_Integer = DerivedClass1.GetMethod("Method", Integer_Integer)
            Dim DerivedClass1_Method_Integer_Long = DerivedClass1.GetMethod("Method", Integer_Long)
            Dim DerivedClass1_SomeProperty_Get = DerivedClass1.GetProperty("SomeProperty").GetGetMethod
            Dim DerivedClass1_Method_String_SByte = DerivedClass1.GetMethod("Method", String_SByte)
            Dim DerivedClass2_Method_Integer_Long = DerivedClass2.GetMethod("Method", Integer_Long)
            Dim DerivedClass2_Method_Char_Integer = DerivedClass2.GetMethod("Method", Char_Integer)
            Dim DerivedClass3_Method_Integer_Long = DerivedClass3.GetMethod("Method", Integer_Long)

            If Not DerivedClass1_Method_Integer_Integer.DeclaringType.Equals(DerivedClass1) Then Throw New InternalTestFailureException
            If Not DerivedClass1_Method_Integer_Long.DeclaringType.Equals(DerivedClass1) Then Throw New InternalTestFailureException
            If Not DerivedClass1_SomeProperty_Get.DeclaringType.Equals(DerivedClass1) Then Throw New InternalTestFailureException
            If Not DerivedClass1_Method_String_SByte.DeclaringType.Equals(DerivedClass1) Then Throw New InternalTestFailureException
            If Not DerivedClass2_Method_Char_Integer.DeclaringType.Equals(DerivedClass2) Then Throw New InternalTestFailureException
            If Not DerivedClass2_Method_Integer_Long.DeclaringType.Equals(DerivedClass2) Then Throw New InternalTestFailureException
            If Not DerivedClass3_Method_Integer_Long.DeclaringType.Equals(DerivedClass3) Then Throw New InternalTestFailureException

            Assert.AreEqual(BaseClass_Method_Integer_Integer, DerivedClass1_Method_Integer_Integer.GetBaseClassMethod)
            Assert.AreEqual(BaseClass_Method_Integer_Long, DerivedClass1_Method_Integer_Long.GetBaseClassMethod)
            Assert.AreEqual(BaseClass_SomeProperty_Get, DerivedClass1_SomeProperty_Get.GetBaseClassMethod)
            Assert.IsNull(DerivedClass1_Method_String_SByte.GetBaseClassMethod)

            Assert.IsNull(DerivedClass2_Method_Integer_Long.GetBaseClassMethod)
            Assert.AreEqual(BaseClass_Method_Char_Integer, DerivedClass2_Method_Char_Integer.GetBaseClassMethod)
            Assert.AreEqual(DerivedClass2_Method_Integer_Long, DerivedClass3_Method_Integer_Long.GetBaseClassMethod)

        End Sub

    End Class
End Namespace