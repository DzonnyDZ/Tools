Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Tools.TestsT
Imports System.Collections.Generic
Imports System.Reflection

<TestClass()> _
Public Class GeneralTestsUT


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

    <TestMethod()> Public Sub AttributeTest()
        Dim at As New AttributeTest
        at.CreateStatistic = False
        at.PreventReTesting = False
        Dim objects As New List(Of ICustomAttributeProvider)
        objects.Add(Assembly.Load("Tools"))
        objects.Add(Assembly.Load("Tools.IL"))
        objects.Add(Assembly.Load("Tools.Metadata"))
        objects.Add(Assembly.Load("Tools.TotalCommander"))
        objects.Add(Assembly.Load("Tools.VisualStudio"))
        objects.Add(Assembly.Load("Tools.VisualStudio.CS"))
        objects.Add(Assembly.Load("Tools.Win"))
        objects.Add(Assembly.Load("Tools.Windows"))
        objects.Add(Assembly.Load("TransformCodeGenerator"))
        objects.Add(Assembly.Load("Tools.VisualStudio.Macros"))
        AddHandler at.Warning, AddressOf at_Warning
        AddHandler at.Error, AddressOf at_Error
        AddHandler at.BeforeTest, AddressOf at_BeforeTest
        at.Test(objects, True)
        If at.Errors.Count > 0 Then Assert.Fail("{0} errors in during attribute test", at.Errors.Count)
    End Sub
    Private Sub at_Warning(ByVal sender As AttributeTest, ByVal e As AttributeTest.WarningEventArgs)
        TestContext.WriteLine("Warning: {0} at {1}", e.Kind, ObjectString(e.Object))
    End Sub
    Private Sub at_Error(ByVal sender As AttributeTest, ByVal e As AttributeTest.AttributeTestErrorEventArgs)
        TestContext.WriteLine("Error: {0} at {1}", e.Exception.Message, ObjectString(e.Object))
    End Sub
    Private Sub at_BeforeTest(ByVal sender As AttributeTest, ByVal e As AttributeTest.CancelObjectEventArgs)
        If TypeOf e.Object Is Assembly Then
            TestContext.WriteLine("Testing assembly {0}", DirectCast(e.Object, Assembly).GetName)
        End If
    End Sub
    Private Function ObjectString(ByVal obj As ICustomAttributeProvider) As String
        If TypeOf obj Is Assembly Then
            Return "Assembly " & DirectCast(obj, Assembly).GetName.Name
        ElseIf TypeOf obj Is [Module] Then
            Return "Module " & DirectCast(obj, [Module]).Name
        ElseIf TypeOf obj Is MemberInfo Then
            Return DirectCast(obj, MemberInfo).MemberType.ToString & " " & If(DirectCast(obj, MemberInfo).DeclaringType IsNot Nothing, DirectCast(obj, MemberInfo).DeclaringType.FullName & ".", "") & DirectCast(obj, MemberInfo).Name
        ElseIf TypeOf obj Is ParameterInfo Then
            If DirectCast(obj, ParameterInfo).IsRetval Then Return "<retval>"
            Return DirectCast(obj, ParameterInfo).Name
        Else
            Return obj.ToString
        End If
    End Function
End Class
