Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports System.Collections.Generic

Namespace SpecialUT

    '''<summary>
    '''This is a test class for CollectionToolsTest and is intended
    '''to contain all CollectionToolsTest Unit Tests
    '''</summary>
    <TestClass()> _
    Public Class TypeToolsUT


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
        <TestMethod()> _
        Public Sub IsDefined()
            Assert.IsTrue(Tools.SpecialT.TypeTools.IsDefined(AppDomainManagerInitializationOptions.None), "IsDefined identifies that member is edfined")
            Assert.IsTrue(Not Tools.SpecialT.TypeTools.IsDefined(CType(14, AppDomainManagerInitializationOptions)), "IsDefined identifies that member is not edfined")
        End Sub

        <TestMethod()> _
        Public Sub GetConstant()
            Dim constant = Tools.SpecialT.TypeTools.GetConstant(AppWinStyle.MaximizedFocus)
            Assert.IsTrue(constant.Name = "MaximizedFocus" AndAlso constant.DeclaringType.Equals(GetType(AppWinStyle)), "GetConstant correctly gets constant")
            constant = Tools.SpecialT.TypeTools.GetConstant(CType(2104, AppWinStyle))
            Assert.IsNull(constant, "GetConstant returns null when constant is not defined")
        End Sub

    End Class
End Namespace