Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Tools.IOt

Namespace IOUT

    '''<summary>
    '''This is a test class for FileSystemToolsTest and is intended
    '''to contain all FileSystemToolsTest Unit Tests
    '''</summary>
    <TestClass()> _
    Public Class FileSystemToolsUT


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


        ''''<summary>
        ''''A test for GetLocalizedName
        ''''</summary>
        '<TestMethod()> _
        'Public Sub GetLocalizedNameTestU()
        '    'If this test fails, create desktop.ini with following text in your pictures
        '    '[.ShellClassInfo]
        '    'LocalizedResourceName=Something completelly different
        '    Dim Path As Path = "C:\Users\Public" 'Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
        '    Dim expected As String = Path.FileName
        '    Dim actual As String
        '    actual = FileSystemTools.GetLocalizedName(Path)
        '    Assert.AreNotEqual(expected, actual)
        'End Sub
    End Class
End Namespace