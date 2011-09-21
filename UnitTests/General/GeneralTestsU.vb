Imports System.Collections.Generic
Imports System.Reflection
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Tools.ReflectionT
Imports Tools.TestsT

''' <summary>Those tests are not unit tests at all, those tests are some additional tests verifying assembly correctness and structure etc.</summary>
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
    'Assembly.Load("Tools.IL"), _
    Private ToolsAssemblies As Assembly() = { _
            Assembly.Load("Tools"), _
            Assembly.Load("Tools.Hardcore"), _
            Assembly.Load("Tools.Metadata"), _
            If(Environment.Is64BitProcess, Nothing, Assembly.Load("Tools.TotalCommander")), _
            If(Environment.Is64BitProcess, Nothing, Assembly.Load("Tools.TotalCommander.Extensions")), _
            Assembly.Load("Tools.SqlServer"), _
            If(Environment.Is64BitProcess, Nothing, Assembly.Load("Tools.VisualStudio")), _
            If(Environment.Is64BitProcess, Nothing, Assembly.Load("Tools.VisualStudio.CS")), _
            Assembly.Load("Tools.VisualStudio.Debugger"), _
            Assembly.Load("Tools.Data.Schema.Deploy"), _
            Assembly.Load("Tools.Win"), _
            Assembly.Load("Tools.Windows"), _
            If(Environment.Is64BitProcess, Nothing, Assembly.Load("TransformCodeGenerator")), _
            Assembly.Load("Tools.VisualStudio.Macros"), _
            Assembly.Load("Tools.Runtime.CompilerServices") _
        }
#Region "Attributes"
    <TestMethod()> Public Sub AttributeTest()
        Dim at As New AttributeTest
        at.CreateStatistic = False
        at.PreventReTesting = False
        Dim objects As New List(Of ICustomAttributeProvider)
        objects.AddRange(ToolsAssemblies)
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
        TestContext.WriteLine("Error: {0} at {1}{2}", e.Exception.Message, ObjectString(e.Object), GetAttributeInfo(e.CustomAttributeData))
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
    Private Function GetAttributeInfo(customAttributeData As CustomAttributeData) As Object
        If customAttributeData Is Nothing Then Return Nothing
        Try
            Return " attribute " + ObjectString(customAttributeData.Constructor)
        Catch
            Return Nothing
        End Try
    End Function
#End Region
#Region "CLS"
    <TestMethod()> Public Sub CLSTest()
        Dim Checker As New CLSComplianceChecker
        Dim violi% = 0
        AddHandler Checker.Violation, Sub(sender, e)
                                          violi += 1
                                          TestContext.WriteLine("{0:d} {0:f} {1} @ {2} {3} CLSCompliant: {4}", e.Rule, e.Message,
                                                                GetMemberString(e.Member, True),
                                                                If(e.Exception IsNot Nothing, e.Exception.Message, ""),
                                                                e.member.IsClsCompliant
                                                               )
                                      End Sub
        For Each Item In ToolsAssemblies
            Checker.Check(Item)
        Next
        Assert.AreEqual(0, violi, String.Format("{0} CLS-compliance violations detected.", violi))
    End Sub
    ''' <summary>Gets string representing member</summary>
    ''' <param name="member">Member to represent</param>
    ''' <param name="IncludeType">Include kind of param such as "Asssembly", "Type" etc.</param>
    ''' <param name="AsParam">Only when <paramref name="member"/> is <see cref="Type"/> (otherwise ignored): True when parameter is used as parameter of method/generic type, so if it is generic parameter, declaring method/type will be ommitted.</param>
    Private Function GetMemberString(ByVal member As ICustomAttributeProvider, ByVal IncludeType As Boolean, Optional ByVal AsParam As Boolean = False) As String
        Dim Type$, Name$
        If TypeOf member Is Assembly Then
            Type = "Assembly"
            Name = DirectCast(member, Assembly).FullName
        ElseIf TypeOf member Is [Module] Then
            Type = "Module"
            Name = DirectCast(member, [Module]).Name
        ElseIf TypeOf member Is Type Then
            With DirectCast(member, Type)
                If .IsGenericParameter Then
                    Type = "Generic parameter"
                    If .DeclaringMethod IsNot Nothing Then
                        Name = If(.FullName, .Name) & If(AsParam, "", " of " & GetMemberString(.DeclaringMethod, False))
                    Else
                        Name = If(.FullName, .Name) & If(AsParam, "", " of " & GetMemberString(.DeclaringType, False))
                    End If
                ElseIf .IsGenericType Then
                    Type = "Type"
                    Name = If(.FullName, .Name) & "["
                    Dim i% = 0
                    For Each gpar In .GetGenericArguments
                        If i > 0 Then Name &= ","
                        Name &= GetMemberString(gpar, False, True)
                        i += 1
                    Next
                    Name &= "]"
                ElseIf .IsArray Then
                    Type = "Type"
                    Name = GetMemberString(.GetElementType, False, False) & "[" & New String(","c, .GetArrayRank - 1) & "]"
                ElseIf .IsByRef Then
                    Type = "Type"
                    Name = GetMemberString(.GetElementType, False, False) & "&"
                ElseIf .IsPointer Then
                    Type = "Type"
                    Name = GetMemberString(.GetElementType, False, False) & "*"
                Else
                    Type = "Type"
                    Name = If(.FullName, .Name)
                End If
            End With
        ElseIf TypeOf member Is PropertyInfo Then
            Type = "Property"
            Name = GetMemberString(DirectCast(member, MemberInfo).DeclaringType, False) & "." & DirectCast(member, MemberInfo).Name
        ElseIf TypeOf member Is FieldInfo Then
            Type = "Field"
            Name = If(DirectCast(member, MemberInfo).DeclaringType IsNot Nothing, GetMemberString(DirectCast(member, MemberInfo).DeclaringType, False) & ".", "") & DirectCast(member, MemberInfo).Name
        ElseIf TypeOf member Is EventInfo Then
            Type = "Event"
            Name = GetMemberString(DirectCast(member, MemberInfo).DeclaringType, False) & "." & DirectCast(member, MemberInfo).Name
        ElseIf TypeOf member Is MethodBase Then
            With DirectCast(member, MethodBase)
                If .IsConstructor AndAlso .IsStatic Then
                    Type = "Initializer"
                ElseIf .IsConstructor Then
                    Type = "Constructor"
                Else
                    Type = "Method"
                End If
                Name = If(DirectCast(member, MemberInfo).DeclaringType IsNot Nothing, GetMemberString(DirectCast(member, MemberInfo).DeclaringType, False) & ".", "") & DirectCast(member, MemberInfo).Name & "("
                Dim i% = 0
                For Each param In .GetParameters
                    If i > 0 Then Name &= ","
                    Name &= GetMemberString(param.ParameterType, False, True)
                    i += 1
                Next
                Name &= ")"
            End With
        ElseIf TypeOf member Is ParameterInfo Then
            With DirectCast(member, ParameterInfo)
                If .IsRetval Then
                    Type = "Return value"
                    Name = If(.Name = "", "", .Name & " ") & GetMemberString(.Member, True)
                Else
                    Type = "Parameter"
                    Name = .Name & " of " & GetMemberString(.Member, True)
                End If
            End With
        Else
            Type = member.GetType.Name
            Name = member.ToString
        End If
        If IncludeType Then Return Type & " " & Name
        Return Name
    End Function
#End Region

 

End Class
