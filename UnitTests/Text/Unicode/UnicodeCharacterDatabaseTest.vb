Imports System.Xml.Linq

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Tools.TextT.UnicodeT



'''<summary>
'''This is a test class for UnicodeCharacterDatabaseTest and is intended
'''to contain all UnicodeCharacterDatabaseTest Unit Tests
'''</summary>
<TestClass()> _
Public Class UnicodeCharacterDatabaseTest


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
            testContextInstance = Value
        End Set
    End Property

    '#Region "Additional test attributes"



    '    ''' <summary>Use ClassInitialize to run code before running the first test in the class</summary>
    '    <ClassInitialize()> _
    '    Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    '        AddHandler AppDomain.CurrentDomain.ResourceResolve, AddressOf CurrentDomain_ResourceResolve
    '    End Sub

    '    ''' <summary>Use ClassCleanup to run code after all tests in a class have run</summary>
    '    <ClassCleanup()> _
    '    Public Shared Sub MyClassCleanup()
    '        RemoveHandler AppDomain.CurrentDomain.ResourceResolve, AddressOf CurrentDomain_ResourceResolve
    '    End Sub

    '#End Region
    '    Private Shared Function CurrentDomain_ResourceResolve(sender As Object, e As ResolveEventArgs) As Reflection.Assembly
    '        If e.RequestingAssembly.Equals(GetType(UnicodeCharacterDatabase).Assembly) AndAlso e.Name = UnicodeCharacterDatabase.UnicodeXmlDatabaseResourceName Then _
    '            Return GetType(UnicodeCharacterDatabase).Assembly
    '        Return Nothing
    '    End Function

    '''<summary>
    '''A test for GetXml
    '''</summary>
    <TestMethod()>
    <DeploymentItem("..\Tools\Text\Unicode\ucd.all.grouped.xml.gz")>
    Public Sub GetXmlTest()
        Assert.IsNotNull(UnicodeCharacterDatabase.GetXml)
    End Sub




End Class
