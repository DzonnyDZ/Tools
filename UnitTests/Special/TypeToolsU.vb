'UpgradeIssue: Uncomment TypeTools.vb (needs Tools.IL to be buildable)
'Imports Microsoft.VisualStudio.TestTools.UnitTesting

'Imports System.Collections.Generic

'Namespace SpecialUT

'    '''<summary>
'    '''This is a test class for CollectionToolsTest and is intended
'    '''to contain all CollectionToolsTest Unit Tests
'    '''</summary>
'    <TestClass()> _
'    Public Class TypeToolsUT


'        Private testContextInstance As TestContext

'        '''<summary>
'        '''Gets or sets the test context which provides
'        '''information about and functionality for the current test run.
'        '''</summary>
'        Public Property TestContext() As TestContext
'            Get
'                Return testContextInstance
'            End Get
'            Set(ByVal value As TestContext)
'                testContextInstance = value
'            End Set
'        End Property

'        <TestMethod()> _
'        Public Sub LoadAssembly()
'            Dim path = IO.Path.Combine(My.Application.Info.DirectoryPath, "Tools.IL.dll")
'            Dim asm = Reflection.Assembly.LoadFile(path)
'            Dim TypeTools = asm.GetType("Tools.SpecialT.TypeTools")
'            Dim IsDefined = TypeTools.GetMethod("IsDefined")
'            Assert.IsNotNull(IsDefined)
'            Dim GetConstant = TypeTools.GetMethod("GetConstant")
'            Assert.IsNotNull(GetConstant)
'            IsDefined = IsDefined.MakeGenericMethod(New Type() {GetType(AppWinStyle)})
'            GetConstant = GetConstant.MakeGenericMethod(New Type() {GetType(AppWinStyle)})
'            Dim IsDefined_MaximizedFocus As Boolean = IsDefined.Invoke(Nothing, New Object() {AppWinStyle.MaximizedFocus})
'            Dim IsDefined_502 As Boolean = IsDefined.Invoke(Nothing, New Object() {CType(502, AppWinStyle)})
'            Assert.IsTrue(IsDefined_MaximizedFocus)
'            Assert.IsFalse(IsDefined_502)
'            Dim Const_MaximizedFocus As Reflection.FieldInfo = GetConstant.Invoke(Nothing, New Object() {AppWinStyle.MaximizedFocus})
'            Dim Const_502 As Reflection.FieldInfo = GetConstant.Invoke(Nothing, New Object() {CType(502, AppWinStyle)})
'            Assert.IsNotNull(Const_MaximizedFocus)
'            Assert.AreEqual(Const_MaximizedFocus.Name, "MaximizedFocus")
'            Assert.AreEqual(Const_MaximizedFocus.GetValue(Nothing), AppWinStyle.MaximizedFocus)
'            Assert.IsNull(Const_502)
'        End Sub

'        <TestMethod()> _
'        Public Sub IsDefinedU()
'            Assert.IsTrue(Tools.SpecialT.TypeTools.IsDefined(AppDomainManagerInitializationOptions.None), "IsDefined identifies that member is edfined")
'            Assert.IsTrue(Not Tools.SpecialT.TypeTools.IsDefined(CType(14, AppDomainManagerInitializationOptions)), "IsDefined identifies that member is not edfined")
'        End Sub

'        <TestMethod()> _
'        Public Sub GetConstantU()
'            Dim constant = Tools.SpecialT.TypeTools.GetConstant(AppWinStyle.MaximizedFocus)
'            Assert.IsTrue(constant.Name = "MaximizedFocus" AndAlso constant.DeclaringType.Equals(GetType(AppWinStyle)), "GetConstant correctly gets constant")
'            constant = Tools.SpecialT.TypeTools.GetConstant(CType(2104, AppWinStyle))
'            Assert.IsNull(constant, "GetConstant returns null when constant is not defined")
'        End Sub




'    End Class
'End Namespace