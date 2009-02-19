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
            Protected Sub MethodProtected()
            End Sub
            Private Sub MethodPrivate()
            End Sub
            Public Overridable Sub GMethod(Of T)(ByVal a As T)
            End Sub
            Public Overridable Sub GMethod(Of T)(ByVal a As Action(Of T))
            End Sub
            Overridable Sub GMethod(Of T)(ByVal a As Long, ByVal b As Long)
            End Sub
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
            Public Overrides Sub GMethod(Of T)(ByVal a As T)
            End Sub
            Public Overrides Sub GMethod(Of T)(ByVal a As Long, ByVal b As Long)
            End Sub
            Public Overrides Sub GMethod(Of T)(ByVal a As System.Action(Of T))
            End Sub
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
        Private Class MAttribute : Inherits Attribute
            Public ReadOnly n%
            Public Sub New(ByVal n%)
                Me.n = n
            End Sub
        End Class

        Private Class CGenericBase1(Of T, U)
            <M(1)> Public Overridable Sub Method(ByVal a As T, ByVal b As Long)
            End Sub
            <M(2)> Public Overridable Sub Method(ByVal a As Long)
            End Sub
            <M(3)> Public Overridable Sub Method(ByVal a As Action(Of T))
            End Sub
        End Class
        Private Class CGenericDerived1(Of T, U) : Inherits CGenericBase1(Of T, u)
            <M(1)> Public Overrides Sub Method(ByVal a As T, ByVal b As Long)
            End Sub
            <M(2)> Public Overrides Sub Method(ByVal a As Long)
            End Sub
            <M(3)> Public Overrides Sub Method(ByVal a As System.Action(Of T))
            End Sub
            <M(4)> Public Overloads Sub Method(ByVal a As U)
            End Sub
        End Class
        Private Class CGenericDerived2(Of X, Y) : Inherits CGenericBase1(Of Y, Integer)
            <M(1)> Public Overrides Sub Method(ByVal a As Y, ByVal b As Long)
            End Sub
            <M(2)> Public Overrides Sub Method(ByVal a As Long)
            End Sub
            <M(3)> Public Overrides Sub Method(ByVal a As System.Action(Of Y))
            End Sub
            <M(4)> Public Overloads Sub Method(ByVal a As Integer)
            End Sub
        End Class
        Private Class CGenericDerived3(Of X) : Inherits CGenericBase1(Of Action(Of X), CGenericDerived3(Of X))
            <M(1)> Public Overrides Sub Method(ByVal a As Action(Of X), ByVal b As Long)
            End Sub
            <M(2)> Public Overrides Sub Method(ByVal a As Long)
            End Sub
            <M(3)> Public Overrides Sub Method(ByVal a As System.Action(Of Action(Of X)))
            End Sub
            <M(4)> Public Overloads Sub Method(ByVal a As Integer)
            End Sub
        End Class
        Private Class CGenericDerived4(Of T) : Inherits CGenericBase1(Of CGenericDerived3(Of Action(Of T)), Activator)
            <M(1)> Public Overrides Sub Method(ByVal a As CGenericDerived3(Of System.Action(Of T)), ByVal b As Long)
            End Sub
            <M(2)> Public Overrides Sub Method(ByVal a As Long)
            End Sub
            <M(3)> Public Overrides Sub Method(ByVal a As System.Action(Of CGenericDerived3(Of System.Action(Of T))))
            End Sub
            <M(4)> Public Overloads Sub Method(ByVal a As Integer)
            End Sub
        End Class
        Private Class CGenericDerived5 : Inherits CGenericBase1(Of Long, Long)
            <M(1)> Public Overrides Sub Method(ByVal a As Long, ByVal b As Long)
            End Sub
            <M(2)> Public Overrides Sub Method(ByVal a As Long)
            End Sub
            <M(3)> Public Overrides Sub Method(ByVal a As System.Action(Of Long))
            End Sub
            <M(4)> Public Overloads Sub Method(ByVal a As Integer)
            End Sub
        End Class


        '<TestMethod()> Public Sub GetBaseClassMethodU()
        '    Dim BaseClass = GetType(BaseClass)
        '    Dim DerivedClass1 = GetType(DerivedClass1)
        '    Dim DerivedClass2 = GetType(DerivedClass2)
        '    Dim DerivedClass3 = GetType(DerivedClass3)

        '    Dim Integer_Long As Type() = New Type() {GetType(Integer), GetType(Long)}
        '    Dim Char_Integer As Type() = New Type() {GetType(Char), GetType(Integer)}
        '    Dim Integer_Integer As Type() = New Type() {GetType(Integer), GetType(Integer)}
        '    Dim String_SByte As Type() = New Type() {GetType(String), GetType(SByte)}

        '    Dim BaseClass_Method_Integer_Long = BaseClass.GetMethod("Method", Integer_Long)
        '    Dim BaseClass_Method_Integer_Integer = BaseClass.GetMethod("Method", Integer_Integer)
        '    Dim BaseClass_Method_Char_Integer = BaseClass.GetMethod("Method", Char_Integer)
        '    Dim BaseClass_SomeProperty_Get = BaseClass.GetProperty("SomeProperty").GetGetMethod
        '    Dim DerivedClass1_Method_Integer_Integer = DerivedClass1.GetMethod("Method", Integer_Integer)
        '    Dim DerivedClass1_Method_Integer_Long = DerivedClass1.GetMethod("Method", Integer_Long)
        '    Dim DerivedClass1_SomeProperty_Get = DerivedClass1.GetProperty("SomeProperty").GetGetMethod
        '    Dim DerivedClass1_Method_String_SByte = DerivedClass1.GetMethod("Method", String_SByte)
        '    Dim DerivedClass2_Method_Integer_Long = DerivedClass2.GetMethod("Method", Integer_Long)
        '    Dim DerivedClass2_Method_Char_Integer = DerivedClass2.GetMethod("Method", Char_Integer)
        '    Dim DerivedClass3_Method_Integer_Long = DerivedClass3.GetMethod("Method", Integer_Long)

        '    If Not DerivedClass1_Method_Integer_Integer.DeclaringType.Equals(DerivedClass1) Then Throw New InternalTestFailureException
        '    If Not DerivedClass1_Method_Integer_Long.DeclaringType.Equals(DerivedClass1) Then Throw New InternalTestFailureException
        '    If Not DerivedClass1_SomeProperty_Get.DeclaringType.Equals(DerivedClass1) Then Throw New InternalTestFailureException
        '    If Not DerivedClass1_Method_String_SByte.DeclaringType.Equals(DerivedClass1) Then Throw New InternalTestFailureException
        '    If Not DerivedClass2_Method_Char_Integer.DeclaringType.Equals(DerivedClass2) Then Throw New InternalTestFailureException
        '    If Not DerivedClass2_Method_Integer_Long.DeclaringType.Equals(DerivedClass2) Then Throw New InternalTestFailureException
        '    If Not DerivedClass3_Method_Integer_Long.DeclaringType.Equals(DerivedClass3) Then Throw New InternalTestFailureException

        '    Assert.AreEqual(BaseClass_Method_Integer_Integer, DerivedClass1_Method_Integer_Integer.GetBaseClassMethod)
        '    Assert.AreEqual(DerivedClass1_Method_Integer_Integer.GetBaseDefinition, DerivedClass1_Method_Integer_Integer.GetBaseClassMethod)
        '    Assert.AreEqual(BaseClass_Method_Integer_Long, DerivedClass1_Method_Integer_Long.GetBaseClassMethod)
        '    Assert.AreEqual(DerivedClass1_Method_Integer_Long.GetBaseDefinition, DerivedClass1_Method_Integer_Long.GetBaseClassMethod)
        '    Assert.AreEqual(BaseClass_SomeProperty_Get, DerivedClass1_SomeProperty_Get.GetBaseClassMethod)
        '    Assert.AreEqual(DerivedClass1_SomeProperty_Get.GetBaseDefinition, DerivedClass1_SomeProperty_Get.GetBaseClassMethod)
        '    Assert.IsNull(DerivedClass1_Method_String_SByte.GetBaseClassMethod)

        '    Assert.IsNull(DerivedClass2_Method_Integer_Long.GetBaseClassMethod)
        '    Assert.AreEqual(BaseClass_Method_Char_Integer, DerivedClass2_Method_Char_Integer.GetBaseClassMethod)
        '    Assert.AreEqual(DerivedClass2_Method_Char_Integer.GetBaseDefinition, DerivedClass2_Method_Char_Integer.GetBaseClassMethod)
        '    Assert.AreEqual(DerivedClass2_Method_Integer_Long, DerivedClass3_Method_Integer_Long.GetBaseClassMethod)
        '    Assert.AreEqual(DerivedClass3_Method_Integer_Long.GetBaseDefinition, DerivedClass3_Method_Integer_Long.GetBaseClassMethod)

        '    'Generic classes
        '    Dim GenericBaseClass = GetType(CGenericBase1(Of ,))
        '    Dim GBCParams = GenericBaseClass.GetGenericArguments
        '    Dim GBC_M1 = GenericBaseClass.GetMethod("Method", New Type() {GBCParams(0), GetType(Long)})
        '    Dim GBC_M2 = GenericBaseClass.GetMethod("Method", New Type() {GetType(Long)})
        '    Dim GBC_M3 = GenericBaseClass.GetMethod("Method", New Type() {GetType(Action(Of )).MakeGenericType(GBCParams(0))})
        '    Dim GDC1 = GetType(CGenericDerived1(Of ,))
        '    Dim GDC2 = GetType(CGenericDerived2(Of ,))
        '    Dim GDC3 = GetType(CGenericDerived3(Of ))
        '    Dim GDC4 = GetType(CGenericDerived4(Of ))
        '    Dim GDC5 = GetType(CGenericDerived5)
        '    Dim GDC1Params = GDC1.GetGenericArguments
        '    Dim GDC2Params = GDC2.GetGenericArguments
        '    Dim GDC3Params = GDC3.GetGenericArguments
        '    Dim GDC4Params = GDC4.GetGenericArguments
        '    Dim GDC1a = GetType(CGenericDerived1(Of Long, Short))
        '    Dim GDC2a = GetType(CGenericDerived2(Of Object, [Enum]))
        '    Dim GDC1b = GetType(CGenericDerived1(Of ,)).MakeGenericType(GetType(String), GDC1Params(1))
        '    Dim dTypes As Type() = {GDC1, GDC2, GDC3, GDC4, GDC5, GDC1a, GDC2a, GDC1b}
        '    Dim bases As Type() = {GenericBaseClass.MakeGenericType(GDC1Params(0), GDC1Params(1)), _
        '                          GenericBaseClass.MakeGenericType(GDC2Params(1), GetType(Integer)), _
        '                          GenericBaseClass.MakeGenericType(GetType(Action(Of )).MakeGenericType(GDC3Params(0)), GDC3.MakeGenericType(GDC3Params(0))), _
        '                          GenericBaseClass.MakeGenericType(GDC3.MakeGenericType(GetType(Action(Of )).MakeGenericType(GDC4Params(0))), GetType(Activator)), _
        '                          GenericBaseClass.MakeGenericType(GetType(Long), GetType(Long)), _
        '                          GenericBaseClass.MakeGenericType(GetType(Long), GetType(Short)), _
        '                          GenericBaseClass.MakeGenericType(GetType([Enum]), GetType(Integer)), _
        '                          GenericBaseClass.MakeGenericType(GetType(String), GDC1Params(1))}
        '    Dim i% = 0
        '    For Each dType In dTypes
        '        For Each m In dType.GetMethods
        '            If Not m.Name = "Method" Then Continue For
        '            Dim mattr = m.GetAttribute(Of MAttribute)()
        '            If mattr.n = 4 Then
        '                Assert.IsNull(m.GetBaseClassMethod)
        '            Else
        '                Dim bm = m.GetBaseClassMethod
        '                Assert.AreEqual(mattr.n, bm.GetAttribute(Of MAttribute).n)
        '                Assert.AreEqual(bases(i), bm.DeclaringType)
        '                Assert.AreEqual(m.GetBaseDefinition, bm)
        '            End If
        '        Next
        '        i += 1
        '    Next

        'End Sub

        Private Interface Interface1
        End Interface
        Private Class Class1 : Implements Interface1
        End Class
        Private Enum Enum1 As Byte
            [__]
        End Enum
        Private Class GenericBaseClass(Of T As BaseClass)
        End Class
        Private Class GenericDerivedClass(Of T As BaseClass) : Inherits GenericBaseClass(Of T)
        End Class

        Private Class GenericBaseClass(Of T1, T2, T3)
        End Class
        Private Class GenericDerivedClass(Of T1, T2, T3)
            Inherits GenericBaseClass(Of T3, Long, T2)
        End Class
        Private Class NonGenericDerivedClass
            Inherits GenericDerivedClass(Of Long, Long, GenericBaseClass(Of Long(,,,,), IntPtr, Char?))
        End Class

        Private Class AnotherGenericBaseClass(Of T1, T2)
        End Class
        Private Class AnotherGenericDerivedClass(Of T1, T2) : Inherits AnotherGenericBaseClass(Of T1, T2)
        End Class

        <TestMethod()> _
        Public Sub IsBaseClassOfU()
            Assert.IsTrue(GetType(BaseClass).IsBaseClassOf(GetType(DerivedClass3)))
            Assert.IsTrue(GetType(BaseClass).IsBaseClassOf(GetType(DerivedClass2)))
            Assert.IsTrue(GetType(BaseClass).IsBaseClassOf(GetType(DerivedClass1)))
            Assert.IsTrue(GetType(Object).IsBaseClassOf(GetType(DerivedClass2)))
            Assert.IsFalse(GetType(DerivedClass2).IsBaseClassOf(GetType(DerivedClass1)))
            Assert.IsFalse(GetType(ReflectionToolsUT).IsBaseClassOf(GetType(BaseClass)))
            Assert.IsFalse(GetType(Interface1).IsBaseClassOf(GetType(Class1)))
            Assert.IsFalse(GetType(Byte).IsBaseClassOf(GetType(Enum1)))
            Assert.IsTrue(GetType([Enum]).IsBaseClassOf(GetType(Enum1)))
            Assert.IsTrue(GetType(ValueType).IsBaseClassOf(GetType([Enum])))
            Assert.IsTrue(GetType(BaseClass).IsBaseClassOf(GetType(GenericBaseClass(Of )).GetGenericArguments()(0)))
            Assert.IsFalse(GetType(BaseClass).IsBaseClassOf(GetType(BaseClass)))
            Assert.IsTrue(GetType(GenericBaseClass(Of )).IsBaseClassOf(GetType(GenericDerivedClass(Of ))))
            Assert.IsTrue(GetType(GenericBaseClass(Of DerivedClass3)).IsBaseClassOf(GetType(GenericDerivedClass(Of DerivedClass3))))
            Assert.IsTrue(GetType(GenericBaseClass(Of )).IsBaseClassOf(GetType(GenericDerivedClass(Of DerivedClass3))))
            Assert.IsFalse(GetType(GenericBaseClass(Of DerivedClass3)).IsBaseClassOf(GetType(GenericDerivedClass(Of ))))

            Assert.IsTrue(GetType(GenericBaseClass(Of ,,)).IsBaseClassOf(GetType(GenericDerivedClass(Of ,,))))
            Assert.IsTrue(GetType(GenericBaseClass(Of ,,)).IsBaseClassOf(GetType(GenericDerivedClass(Of Long, String, TimeSpan))))
            Assert.IsFalse(GetType(GenericDerivedClass(Of Long, String, TimeSpan)).IsBaseClassOf(GetType(GenericBaseClass(Of ,,))))
            Assert.IsTrue(GetType(GenericBaseClass(Of ,,)).IsBaseClassOf(GetType(NonGenericDerivedClass)))
            Assert.IsTrue(GetType(GenericDerivedClass(Of ,,)).IsBaseClassOf(GetType(NonGenericDerivedClass)))
            Assert.IsTrue(GetType(GenericDerivedClass(Of Long, Long, GenericBaseClass(Of Long(,,,,), IntPtr, Char?))).IsBaseClassOf(GetType(NonGenericDerivedClass)))
            Assert.IsTrue(GetType(GenericBaseClass(Of GenericBaseClass(Of Long(,,,,), IntPtr, Char?), Long, Long)).IsBaseClassOf(GetType(NonGenericDerivedClass)))
            Assert.IsTrue(GetType(AnotherGenericBaseClass(Of ,)).MakeGenericType(GetType(AnotherGenericBaseClass(Of ,)).GetGenericArguments(0), GetType(Integer)).IsBaseClassOf(GetType(AnotherGenericDerivedClass(Of Byte, Integer))))
        End Sub

        <TestMethod()> _
        Public Sub IsDerivedFromU()
            Assert.IsTrue(GetType(DerivedClass3).IsDerivedFrom(GetType(BaseClass)))
            Assert.IsTrue(GetType(DerivedClass2).IsDerivedFrom(GetType(BaseClass)))
            Assert.IsTrue(GetType(DerivedClass1).IsDerivedFrom(GetType(BaseClass)))
            Assert.IsTrue(GetType(DerivedClass2).IsDerivedFrom(GetType(Object)))
            Assert.IsFalse(GetType(DerivedClass1).IsDerivedFrom(GetType(DerivedClass2)))
            Assert.IsFalse(GetType(BaseClass).IsDerivedFrom(GetType(ReflectionToolsUT)))
            Assert.IsFalse(GetType(Class1).IsDerivedFrom(GetType(Interface1)))
            Assert.IsFalse(GetType(Enum1).IsDerivedFrom(GetType(Byte)))
            Assert.IsTrue(GetType(Enum1).IsDerivedFrom(GetType([Enum])))
            Assert.IsTrue(GetType([Enum]).IsDerivedFrom(GetType(ValueType)))
            Assert.IsTrue(GetType(GenericBaseClass(Of )).GetGenericArguments()(0).IsDerivedFrom(GetType(BaseClass)))
            Assert.IsFalse(GetType(BaseClass).IsDerivedFrom(GetType(BaseClass)))
            Assert.IsTrue(GetType(GenericDerivedClass(Of )).IsDerivedFrom(GetType(GenericBaseClass(Of ))))
            Assert.IsTrue(GetType(GenericDerivedClass(Of DerivedClass3)).IsDerivedFrom(GetType(GenericBaseClass(Of DerivedClass3))))
            Assert.IsTrue(GetType(GenericDerivedClass(Of DerivedClass3)).IsDerivedFrom(GetType(GenericBaseClass(Of ))))
            Assert.IsFalse(GetType(GenericDerivedClass(Of )).IsDerivedFrom(GetType(GenericBaseClass(Of DerivedClass3))))
        End Sub

        Private MustInherit Class TestClass
            Private Sub PrivateSub()
            End Sub
            Protected MustOverride Sub ProtectedSub()
            Public MustOverride Sub PublicSub()
            Friend MustOverride Sub FriendSub()
            Protected Friend MustOverride Sub ProtectedFriendSub()

            Private Property PrivateProperty() As Object
                Get
                    Return Nothing
                End Get
                Set(ByVal value As Object)
                End Set
            End Property
            Protected MustOverride Property ProtectedProperty() As Object
            Public MustOverride Property PublicProperty() As Object
            Friend MustOverride Property FriendProperty() As Object
            Protected Friend MustOverride Property ProtectedFriendProperty() As Object

            Private Event PrivateEvent()
            Protected Event ProtectedEvent()
            Public Event PublicEvent()
            Friend Event FriendEvent()
            Protected Friend Event ProtectedFriendEvent()

            Private PrivateField%
            Protected ProtectedField%
            Public PublicField%
            Friend FriendField%
            Protected Friend ProtectedFriendField%

            Private Delegate Sub PrivateType()
            Protected Delegate Sub ProtectedType()
            Public Delegate Sub PublicType()
            Friend Delegate Sub FriendType()
            Protected Friend Delegate Sub ProtectedFriendType()
        End Class

    End Class
    <CLSCompliant(False), ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
    Public Delegate Sub _TestDelegate_Public()
    <ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
    Friend Delegate Sub _TestDelegate_Friend()
End Namespace
Namespace ReflectionUT
    Partial Class ReflectionToolsUT
        <TestMethod()> _
        Public Sub VisibilityU()
            Assert.AreEqual(Visibility.Private, GetType(TestClass).GetMember("PrivateSub", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.Family, GetType(TestClass).GetMember("ProtectedSub", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.Public, GetType(TestClass).GetMember("PublicSub", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.Assembly, GetType(TestClass).GetMember("FriendSub", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.FamORAssem, GetType(TestClass).GetMember("ProtectedFriendSub", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)

            Assert.AreEqual(Visibility.Private, GetType(TestClass).GetMember("PrivateProperty", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.Family, GetType(TestClass).GetMember("ProtectedProperty", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.Public, GetType(TestClass).GetMember("PublicProperty", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.Assembly, GetType(TestClass).GetMember("FriendProperty", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.FamORAssem, GetType(TestClass).GetMember("ProtectedFriendProperty", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)

            Assert.AreEqual(Visibility.Private, GetType(TestClass).GetMember("PrivateEvent", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.Family, GetType(TestClass).GetMember("ProtectedEvent", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.Public, GetType(TestClass).GetMember("PublicEvent", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.Assembly, GetType(TestClass).GetMember("FriendEvent", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.FamORAssem, GetType(TestClass).GetMember("ProtectedFriendEvent", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)

            Assert.AreEqual(Visibility.Private, GetType(TestClass).GetMember("PrivateField", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.Family, GetType(TestClass).GetMember("ProtectedField", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.Public, GetType(TestClass).GetMember("PublicField", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.Assembly, GetType(TestClass).GetMember("FriendField", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.FamORAssem, GetType(TestClass).GetMember("ProtectedFriendField", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)

            Assert.AreEqual(Visibility.Private, GetType(TestClass).GetMember("PrivateType", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.Family, GetType(TestClass).GetMember("ProtectedType", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.Public, GetType(TestClass).GetMember("PublicType", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.Assembly, GetType(TestClass).GetMember("FriendType", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.FamORAssem, GetType(TestClass).GetMember("ProtectedFriendType", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)(0).Visibility)
            Assert.AreEqual(Visibility.Public, GetType(_TestDelegate_Public).Visibility)
            Assert.AreEqual(Visibility.Assembly, GetType(_TestDelegate_Friend).Visibility)
        End Sub
        <ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)> _
        Public Class TestClassWithManyMembers
            Partial Private Class PrivateClass
                Private PrivateField%
                Protected FamilyField%
                Public PublicField%
                Friend AssemblyField%
                Protected Friend FamORAssemField%
                Private Class ClassInPrivateClass
                End Class
            End Class
            Partial Protected Class FamilyClass
                Private PrivateField%
                Protected FamilyField%
                Public PublicField%
                Friend AssemblyField%
                Protected Friend FamORAssemField%
            End Class
            Partial Public Class PublicClass
                Private PrivateField%
                Protected FamilyField%
                Public PublicField%
                Friend AssemblyField%
                Protected Friend FamORAssemField%
            End Class
            Partial Friend Class AssemblyClass
                Private PrivateField%
                Protected FamilyField%
                Public PublicField%
                Friend AssemblyField%
                Protected Friend FamORAssemField%
            End Class
            Partial Protected Friend Class FamORAssemClass
                Private PrivateField%
                Protected FamilyField%
                Public PublicField%
                Friend AssemblyField%
                Protected Friend FamORAssemField%
            End Class
        End Class

        <TestMethod()> Public Sub HowIsSeenBy()
            'Private    Private
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Private    Family
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Private    Public
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Private    Assembly
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Private    FamORAssem
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Family     Private
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Family     Family
            Assert.AreEqual(Visibility.Family, GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Family     Public
            Assert.AreEqual(Visibility.Family, GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Family     Assembly
            Assert.AreEqual(Visibility.FamANDAssem, GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Family     FamORAssem
            Assert.AreEqual(Visibility.Family, GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Public     Private
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Public     Family
            Assert.AreEqual(Visibility.Family, GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Public     Public
            Assert.AreEqual(Visibility.Public, GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Public     Assembly
            Assert.AreEqual(Visibility.Assembly, GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Public     FamORAssem
            Assert.AreEqual(Visibility.FamORAssem, GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Assembly   Private
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Assembly   Family
            Assert.AreEqual(Visibility.FamANDAssem, GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Assembly   Public
            Assert.AreEqual(Visibility.Assembly, GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Assembly   Assembly
            Assert.AreEqual(Visibility.Assembly, GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'Assembly   FamORAssem
            Assert.AreEqual(Visibility.Assembly, GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'FamORAssem Private
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'FamORAssem Family
            Assert.AreEqual(Visibility.Family, GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'FamORAssem Public
            Assert.AreEqual(Visibility.FamORAssem, GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'FamORAssem Assembly
            Assert.AreEqual(Visibility.Assembly, GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))
            'FamORAssem FamORAssem
            Assert.AreEqual(Visibility.FamORAssem, GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(Nothing))

            'Private    Private
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Private    Family
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Private    Public
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Private    Assembly
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Private    FamORAssem
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Family     Private
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Family     Family
            Assert.AreEqual(Visibility.Family, GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Family     Public
            Assert.AreEqual(Visibility.Family, GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Family     Assembly
            Assert.AreEqual(Visibility.FamANDAssem, GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Family     FamORAssem
            Assert.AreEqual(Visibility.Family, GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Public     Private
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Public     Family
            Assert.AreEqual(Visibility.Family, GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Public     Public
            Assert.AreEqual(Visibility.Public, GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Public     Assembly
            Assert.AreEqual(Visibility.Assembly, GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Public     FamORAssem
            Assert.AreEqual(Visibility.FamORAssem, GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Assembly   Private
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Assembly   Family
            Assert.AreEqual(Visibility.FamANDAssem, GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Assembly   Public
            Assert.AreEqual(Visibility.Assembly, GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Assembly   Assembly
            Assert.AreEqual(Visibility.Assembly, GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'Assembly   FamORAssem
            Assert.AreEqual(Visibility.Assembly, GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'FamORAssem Private
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'FamORAssem Family
            Assert.AreEqual(Visibility.Family, GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'FamORAssem Public
            Assert.AreEqual(Visibility.FamORAssem, GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'FamORAssem Assembly
            Assert.AreEqual(Visibility.Assembly, GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))
            'FamORAssem FamORAssem
            Assert.AreEqual(Visibility.FamORAssem, GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers)))

            'Private    Private
            Assert.AreEqual(Visibility.Private, GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetNestedType("ClassInPrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)))
            'Private    Family
            Assert.AreEqual(Visibility.Family, GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetNestedType("ClassInPrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)))
            'Private    Public
            Assert.AreEqual(Visibility.Public, GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetNestedType("ClassInPrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)))
            'Private    Assembly
            Assert.AreEqual(Visibility.Assembly, GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetNestedType("ClassInPrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)))
            'Private    FamORAssem
            Assert.AreEqual(Visibility.FamORAssem, GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).HowIsSeenBy(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetNestedType("ClassInPrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)))
        End Sub

        <TestMethod()> Public Sub CanBeSeenFrom()
            'Private    Private
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Private    Family
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Private    Public
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Private    Assembly
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Private    FamORAssem
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Family     Private
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Family     Family
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Family     Public
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Family     Assembly
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Family     FamORAssem
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Public     Private
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Public     Family
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Public     Public
            Assert.IsTrue(GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Public     Assembly
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Public     FamORAssem
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Assembly   Private
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Assembly   Family
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Assembly   Public
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Assembly   Assembly
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'Assembly   FamORAssem
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'FamORAssem Private
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'FamORAssem Family
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'FamORAssem Public
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'FamORAssem Assembly
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))
            'FamORAssem FamORAssem
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(Nothing))

            'Private    Private
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Private    Family
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Private    Public
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Private    Assembly
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Private    FamORAssem
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Family     Private
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Family     Family
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Family     Public
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Family     Assembly
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Family     FamORAssem
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("FamilyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Public     Private
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Public     Family
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Public     Public
            Assert.IsTrue(GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Public     Assembly
            Assert.IsTrue(GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Public     FamORAssem
            Assert.IsTrue(GetType(TestClassWithManyMembers).GetNestedType("PublicClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Assembly   Private
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Assembly   Family
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Assembly   Public
            Assert.IsTrue(GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Assembly   Assembly
            Assert.IsTrue(GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'Assembly   FamORAssem
            Assert.IsTrue(GetType(TestClassWithManyMembers).GetNestedType("AssemblyClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'FamORAssem Private
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'FamORAssem Family
            Assert.IsFalse(GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'FamORAssem Public
            Assert.IsTrue(GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'FamORAssem Assembly
            Assert.IsTrue(GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))
            'FamORAssem FamORAssem
            Assert.IsTrue(GetType(TestClassWithManyMembers).GetNestedType("FamORAssemClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers)))

            'Private    Private
            Assert.IsTrue(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PrivateField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetNestedType("ClassInPrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)))
            'Private    Family
            Assert.IsTrue(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamilyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetNestedType("ClassInPrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)))
            'Private    Public
            Assert.IsTrue(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("PublicField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetNestedType("ClassInPrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)))
            'Private    Assembly
            Assert.IsTrue(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("AssemblyField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetNestedType("ClassInPrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)))
            'Private    FamORAssem
            Assert.IsTrue(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetField("FamORAssemField", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Static).CanBeSeenFrom(GetType(TestClassWithManyMembers).GetNestedType("PrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic).GetNestedType("ClassInPrivateClass", Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)))

            Assert.IsTrue(GetType(BaseClass).GetMethod("MethodProtected", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Static Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public).CanBeSeenFrom(GetType(DerivedClass3)))
            Assert.IsFalse(GetType(BaseClass).GetMethod("MethodPrivate", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Static Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public).CanBeSeenFrom(GetType(DerivedClass3)))
        End Sub
    End Class
End Namespace