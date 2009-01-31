Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports Tools.API
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Reflection

Namespace APIUT
    <TestClass()> _
  Public Class HelpersUT
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

#Region "IsFunctionExported"
        <TestMethod()> _
        Public Sub IsFunctionExportedU1()
            Assert.IsTrue(Helpers.IsFunctionExported("Kernel32.dll", "LoadLibrary", Runtime.InteropServices.CharSet.Auto), "LoadLibrary is defined in Kernel32.dll (auto)")
            Assert.IsTrue(Helpers.IsFunctionExported("Kernel32.dll", "LoadLibrary", Runtime.InteropServices.CharSet.Ansi), "LoadLibrary is defined in Kernel32.dll (ansi)")
            Assert.IsTrue(Helpers.IsFunctionExported("Kernel32.dll", "LoadLibrary", Runtime.InteropServices.CharSet.Unicode), "LoadLibrary is defined in Kernel32.dll (unicode)")
            Assert.IsFalse(Helpers.IsFunctionExported("Kernel32.dll", "LoadLibrary", Runtime.InteropServices.CharSet.None), "LoadLibrary is not defined in Kernel32.dll (none)")

            Assert.IsTrue(Helpers.IsFunctionExported("Kernel32.dll", "FreeLibrary", Runtime.InteropServices.CharSet.Auto), "FreeLibrary is defined in Kernel32.dll (auto)")
            Assert.IsTrue(Helpers.IsFunctionExported("Kernel32.dll", "FreeLibrary", Runtime.InteropServices.CharSet.Ansi), "FreeLibrary is not defined in Kernel32.dll (ansi)")
            Assert.IsTrue(Helpers.IsFunctionExported("Kernel32.dll", "FreeLibrary", Runtime.InteropServices.CharSet.Unicode), "FreeLibrary is not defined in Kernel32.dll (unicode)")
            Assert.IsTrue(Helpers.IsFunctionExported("Kernel32.dll", "FreeLibrary", Runtime.InteropServices.CharSet.None), "FreeLibrary is defined in Kernel32.dll (none)")
        End Sub
        <TestMethod()> _
        Public Sub IsFunctionExportedU2()
            For Each fcn In New String() {"LoadLibrary", "FreeLibrary"}
                Dim Attr As New DllImportAttribute("Kernel32.dll")
                Attr.EntryPoint = fcn
                For Each value As CharSet In [Enum].GetValues(GetType(CharSet))
                    Attr.CharSet = value
                    Assert.AreEqual( _
                        Helpers.IsFunctionExported("Kernel32.dll", fcn, value), _
                        Helpers.IsFunctionExported(Attr), _
                        "IsFunctionExported(String,String,CharSet) has same results as IsFunctionExported(DllImportAttribute)")
                Next
            Next
        End Sub
        <TestMethod()> Public Sub IsFunctionExportedU3()
            For Each Name As String In New String() { _
                   "LoadLibrary_None", "LoadLibrary_Ansi", "LoadLibrary_Unicode", "LoadLibrary_Auto", _
                   "GetProcAddress_None", "GetProcAddress_Ansi", "GetProcAddress_Unicode", "GetProcAddress_Auto"}
                Dim Method = Me.GetType.GetMethod(Name, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Static)
                Dim IsExported = IsFunctionExported(Me.GetType, Name, Reflection.BindingFlags.NonPublic)
                Dim invoked As Boolean = False
                Try
                    If Name.StartsWith("LoadLibrary") Then
                        Dim ret As Integer = Method.Invoke(Nothing, New Object() {"this library hopefully does not exist.dll"})
                        invoked = True
                        If ret <> 0 Then API.FreeLibrary(ret)
                    Else
                        Method.Invoke(Nothing, New Object() {0, "anything"})
                        invoked = True
                    End If
                Catch ex As TargetInvocationException When TypeOf ex.InnerException Is EntryPointNotFoundException
                End Try
                Assert.AreEqual(invoked, IsExported, "IsExported returns true when function can be invoked, false if it cannot.")
            Next
        End Sub

        <TestMethod()> Public Sub IsFunctionExportedU4()
            For Each Name As String In New String() { _
                   "LoadLibrary_None", "LoadLibrary_Ansi", "LoadLibrary_Unicode", "LoadLibrary_Auto", _
                   "GetProcAddress_None", "GetProcAddress_Ansi", "GetProcAddress_Unicode", "GetProcAddress_Auto"}
                Dim Method = Me.GetType.GetMethod(Name, Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Static)
                Dim IsExported = IsFunctionExported(Method)
                Dim invoked As Boolean = False
                Try
                    If Name.StartsWith("LoadLibrary") Then
                        Dim ret As Integer = Method.Invoke(Nothing, New Object() {"this library hopefully does not exist.dll"})
                        invoked = True
                        If ret <> 0 Then API.FreeLibrary(ret)
                    Else
                        Method.Invoke(Nothing, New Object() {0, "anything"})
                        invoked = True
                    End If
                Catch ex As TargetInvocationException When TypeOf ex.InnerException Is EntryPointNotFoundException
                End Try
                Assert.AreEqual(invoked, IsExported, "IsExported returns true when function can be invoked, false if it cannot.")
            Next
        End Sub
        <TestMethod()> Public Sub IsFunctionExportedU5()
            For Each MethodDelegate In New [Delegate]() { _
                    New dLoadLibrary(AddressOf LoadLibrary_None), _
                    New dLoadLibrary(AddressOf LoadLibrary_Ansi), _
                    New dLoadLibrary(AddressOf LoadLibrary_Unicode), _
                    New dLoadLibrary(AddressOf LoadLibrary_Auto), _
                    New dGetProcAddress(AddressOf GetProcAddress_None), _
                    New dGetProcAddress(AddressOf GetProcAddress_Ansi), _
                    New dGetProcAddress(AddressOf GetProcAddress_Unicode), _
                    New dGetProcAddress(AddressOf GetProcAddress_Auto)}
                Dim IsExported = IsFunctionExported(MethodDelegate)
                Dim invoked As Boolean = False
                Dim Name = MethodDelegate.Method.Name
                Try
                    If Name.StartsWith("LoadLibrary") Then
                        Dim ret As Integer = MethodDelegate.DynamicInvoke("this library hopefully does not exist.dll")
                        invoked = True
                        If ret <> 0 Then API.FreeLibrary(ret)
                    Else
                        MethodDelegate.DynamicInvoke(0, "anything")
                        invoked = True
                    End If
                Catch ex As TargetInvocationException When TypeOf ex.InnerException Is EntryPointNotFoundException
                End Try
                Assert.AreEqual(invoked, IsExported, "IsExported returns true when function can be invoked, false if it cannot.")
            Next
        End Sub

        Private Delegate Function dLoadLibrary(ByRef lpFileName As String) As Integer
        Private Declare Function LoadLibrary_None Lib "kernel32" Alias "LoadLibrary" (ByVal lpLibFileName As String) As Integer
        Private Declare Ansi Function LoadLibrary_Ansi Lib "kernel32" Alias "LoadLibrary" (ByVal lpLibFileName As String) As Integer
        Private Declare Unicode Function LoadLibrary_Unicode Lib "kernel32" Alias "LoadLibrary" (ByVal lpLibFileName As String) As Integer
        Private Declare Auto Function LoadLibrary_Auto Lib "kernel32" Alias "LoadLibrary" (ByVal lpLibFileName As String) As Integer

        Private Delegate Function dGetProcAddress(ByVal hModule As Integer, ByRef lpProcName As String) As Integer
        Private Declare Function GetProcAddress_None Lib "kernel32" Alias "GetProcAddress" (ByVal hModule As Integer, ByVal lpProcName As String) As Integer
        Private Declare Auto Function GetProcAddress_Auto Lib "kernel32" Alias "GetProcAddress" (ByVal hModule As Integer, ByVal lpProcName As String) As Integer
        Private Declare Ansi Function GetProcAddress_Ansi Lib "kernel32" Alias "GetProcAddress" (ByVal hModule As Integer, ByVal lpProcName As String) As Integer
        Private Declare Unicode Function GetProcAddress_Unicode Lib "kernel32" Alias "GetProcAddress" (ByVal hModule As Integer, ByVal lpProcName As String) As Integer
#End Region
    End Class
End Namespace