Imports System.Linq
Imports DMKSoftware.CodeGenerators
Imports System.Runtime.InteropServices
Imports Microsoft.VisualStudio.Designer.Interfaces
Imports System.CodeDom.Compiler
Imports System.ComponentModel.Design
Imports System.Reflection
Imports Microsoft.VisualStudio

''' <summary>Command line wrapper of <see cref="BaseResXFileCodeGeneratorEx"/></summary>
Module ResXFileGenerator
    ''' <summary>Performs generation</summary>
    Sub Main()
        If My.Application.CommandLineArgs.Count = 0 Then
            Logo()
            Console.WriteLine(My.Resources.Legend, My.Application.Info.AssemblyName)
            Exit Sub
        End If
        'Check
        If Not My.Application.CommandLineArgs.Contains("/nologo") Then Logo()
        If My.Application.CommandLineArgs.IndexOf("/in") < 0 Then
            Console.WriteLine(My.Resources.MissingParameterIn)
            Environment.Exit(1)
            If My.Application.CommandLineArgs.IndexOf("/in") + 1 >= My.Application.CommandLineArgs.Count OrElse My.Application.CommandLineArgs(My.Application.CommandLineArgs.IndexOf("/in") + 1).StartsWith("/") Then
                Console.WriteLine(My.Resources.MissingInfileSpecification)
                Environment.Exit(2)
            End If
        End If
        If My.Application.CommandLineArgs.IndexOf("/out") < 0 Then
            Console.WriteLine(My.Resources.MissingParameterOut)
            Environment.Exit(3)
            If My.Application.CommandLineArgs.IndexOf("/out") + 1 >= My.Application.CommandLineArgs.Count OrElse My.Application.CommandLineArgs(My.Application.CommandLineArgs.IndexOf("/out") + 1).StartsWith("/") Then
                Console.WriteLine(My.Resources.MissingOutfileSpecification)
                Environment.Exit(4)
            End If
        End If
        'Init
        Dim Lang$
        Dim Internal = My.Application.CommandLineArgs.Contains("/internal")
        Dim Infile = My.Application.CommandLineArgs(My.Application.CommandLineArgs.IndexOf("/in") + 1)
        Dim Outfile = My.Application.CommandLineArgs(My.Application.CommandLineArgs.IndexOf("/out") + 1)
        If My.Application.CommandLineArgs.Contains("/lang") AndAlso My.Application.CommandLineArgs.IndexOf("/lang") + 1 < My.Application.CommandLineArgs.Count Then _
            Lang = My.Application.CommandLineArgs(My.Application.CommandLineArgs.IndexOf("/lang") + 1) _
        Else Lang = IO.Path.GetExtension(Outfile).Substring(1)
        Dim ns = ""
        If My.Application.CommandLineArgs.Contains("/ns") AndAlso My.Application.CommandLineArgs.IndexOf("/ns") + 1 < My.Application.CommandLineArgs.Count Then _
            ns = My.Application.CommandLineArgs(My.Application.CommandLineArgs.IndexOf("/ns") + 1)
        Dim rname As String
        If My.Application.CommandLineArgs.Contains("/name") AndAlso My.Application.CommandLineArgs.IndexOf("/name") + 1 < My.Application.CommandLineArgs.Count Then _
            rname = My.Application.CommandLineArgs(My.Application.CommandLineArgs.IndexOf("/name") + 1) _
        Else rname = IO.Path.GetFileNameWithoutExtension(Infile)
        Dim FirstLine As String = Nothing
        If My.Application.CommandLineArgs.Contains("/firstline") AndAlso My.Application.CommandLineArgs.IndexOf("/firstline") + 1 < My.Application.CommandLineArgs.Count Then _
            FirstLine = My.Application.CommandLineArgs(My.Application.CommandLineArgs.IndexOf("/firstline") + 1)
        Dim SplitOn As String = Nothing
        If My.Application.CommandLineArgs.Contains("/spliton") AndAlso My.Application.CommandLineArgs.IndexOf("/spliton") + 1 < My.Application.CommandLineArgs.Count Then _
            SplitOn = My.Application.CommandLineArgs(My.Application.CommandLineArgs.IndexOf("/spliton") + 1).Replace("\n", vbLf).Replace("\r", vbCr).Replace("\\", "\")
        Dim Outfile2 As String = Nothing
        If My.Application.CommandLineArgs.Contains("/out2") AndAlso My.Application.CommandLineArgs.IndexOf("/out2") + 1 < My.Application.CommandLineArgs.Count Then _
            Outfile2 = My.Application.CommandLineArgs(My.Application.CommandLineArgs.IndexOf("/out2") + 1)
        If SplitOn Is Nothing Xor SplitOn Is Nothing Then
            Console.WriteLine(My.Resources.SplitonAndOut2MustBeSpecifiedBothOrNone)
            Environment.Exit(5)
        End If
        Dim Provider As CodeDom.Compiler.CodeDomProvider
        Select Case Lang.ToLower
            Case "vb" : Provider = New Microsoft.VisualBasic.VBCodeProvider
            Case "cs" : Provider = New Microsoft.CSharp.CSharpCodeProvider
            Case "h", "c", "cpp" : Provider = New Microsoft.VisualC.CppCodeProvider
            Case "cpp.7" : Provider = New Microsoft.VisualC.CppCodeProvider7
            Case "cpp.vs" : Provider = New Microsoft.VisualC.VSCodeProvider
            Case "js" : Provider = New Microsoft.JScript.JScriptCodeProvider
            Case "jsl", "java"
                Try
                    Dim asm = Reflection.Assembly.Load("VJSharpCodeProvider,Version=2.0.0.0, Culture=neutral,PublicKeyToken=b03f5f7f11d50a3a", Nothing)
                    Provider = Activator.CreateInstance(asm.GetType("Microsoft.VJSharp.VJSharpCodeProvider"))
                Catch ex As Exception
                    Console.WriteLine("Failed to initialize J# code provider. Install J# runtime or copy VJSharpCodeProvider.dll to application directory")
                    Environment.Exit(6)
                    Return
                End Try
            Case Else
                Console.WriteLine(My.Resources.UnknownLanguage0)
                Environment.Exit(4)
                Return
        End Select

        'Generate
        Dim generator As BaseResXFileCodeGeneratorEx
        If Internal Then generator = New InternalResXFileCodeGeneratorEx Else generator = New ResXFileCodeGeneratorEx
        AddHandler generator.BeforeGenerateText, AddressOf generator_BeforeGenerateText
        Dim ip As New InternalProvider(Provider)
        generator.SetSite(ip)
        generator.ResourceNamespace = rname
        Dim pOutput(0) As IntPtr
        Dim OutputSize As UInteger
        Dim Progress As New ProgressMonitor
        Try
            generator.Generate(Infile, My.Computer.FileSystem.ReadAllText(Infile), ns, pOutput, OutputSize, Progress)
        Catch ex As IO.FileNotFoundException
            Console.WriteLine(My.Resources.ErrorWhileGeneratingCode)
            Console.WriteLine(ex.Message)
            Environment.Exit(52)
        Catch ex As IO.IOException
            Console.WriteLine(My.Resources.ErrorWhileGeneratingCode)
            Console.WriteLine(ex.Message)
            Environment.Exit(52)
        Catch ex As Exception
            Console.WriteLine(My.Resources.ErrorWhileGeneratingCode)
            Console.WriteLine(ex.Message)
            Environment.Exit(51)
        End Try
        'Alter result
        If pOutput(0) = IntPtr.Zero Then
            Console.WriteLine(My.Resources.NoOutputWasGenerated)
            Environment.Exit(101)
        End If
        Dim bytes(OutputSize) As Byte
        Marshal.Copy(pOutput(0), bytes, 0, OutputSize)
        Marshal.FreeCoTaskMem(pOutput(0))
        Dim str = System.Text.Encoding.UTF8.GetString(bytes)
        str = str.TrimStart(ChrW(65279))
        Dim NL As String
        If str.Contains(vbCrLf) Then
            NL = vbCrLf
        ElseIf str.Contains(vbLf) Then
            NL = vbLf
        ElseIf str.Contains(vbCr) Then
            NL = vbCr
        Else
            NL = Environment.NewLine
        End If
        Dim str1$, str2$
        If SplitOn IsNot Nothing Then
            Dim splitonpos = str.IndexOf(SplitOn)
            If splitonpos < 0 Then
                Console.WriteLine(My.Resources.UnableToSplitGeneratedText)
                Environment.Exit(11)
            End If
            str1 = str.Substring(0, splitonpos)
            str2 = str.Substring(splitonpos)
        Else
            str1 = str
            str2 = Nothing
        End If
        Dim lines1 As List(Of String)
        Dim lines2 As List(Of String) = Nothing
        lines1 = New List(Of String)(str1.Split(New String() {NL}, StringSplitOptions.None))
        If SplitOn IsNot Nothing Then
            lines2 = New List(Of String)(str2.Split(New String() {NL}, StringSplitOptions.None))
        End If
        If FirstLine IsNot Nothing Then lines1.Insert(0, FirstLine)
        For i As Integer = 0 To My.Application.CommandLineArgs.Count - 1
            If My.Application.CommandLineArgs(i) = "/l" AndAlso i + 3 < My.Application.CommandLineArgs.Count Then
                Dim ab = My.Application.CommandLineArgs(i + 1)
                Try
                    Select Case ab
                        Case "a"
                            lines1.Insert(Integer.Parse(My.Application.CommandLineArgs(i + 2), System.Globalization.CultureInfo.InvariantCulture), My.Application.CommandLineArgs(i + 3))
                        Case "b"
                            lines2.Insert(Integer.Parse(My.Application.CommandLineArgs(i + 2), System.Globalization.CultureInfo.InvariantCulture), My.Application.CommandLineArgs(i + 3))
                        Case Else
                            Console.WriteLine(My.Resources.InvalidPartSpecifierForL0, ab)
                    End Select
                Catch ex As FormatException
                    Console.WriteLine(ex.Message)
                    Environment.Exit(53)
                Catch ex As ArgumentOutOfRangeException
                    Console.WriteLine(My.Resources.InvalidInsertIndex0, My.Application.CommandLineArgs(i + 2))
                    Console.WriteLine(ex.Message)
                    Environment.Exit(54)
                End Try
                i += 3
            ElseIf My.Application.CommandLineArgs(i) = "/l" Then
                Console.WriteLine(My.Resources.WarningIncompleteLArgumentIgnored)
            End If
        Next
        str1 = String.Join(NL, lines1.ToArray)
        'Compare
        Dim Write1 As Boolean = False
        Dim Write2 As Boolean = False
        If IO.File.Exists(Outfile) Then
            Try : Write1 = My.Computer.FileSystem.ReadAllText(Outfile) <> str1
            Catch : Write1 = True
            End Try
        Else : Write1 = True
        End If
        If lines2 IsNot Nothing Then str2 = String.Join(NL, lines2.ToArray)
        If str2 IsNot Nothing AndAlso IO.File.Exists(Outfile2) Then
            Try : Write2 = My.Computer.FileSystem.ReadAllText(Outfile2) <> str2
            Catch : Write2 = True
            End Try
        Else : Write2 = str2 IsNot Nothing
        End If
        'Write
        Try
            If Write1 Then My.Computer.FileSystem.WriteAllText(Outfile, str1, False)
            If Write2 Then My.Computer.FileSystem.WriteAllText(Outfile2, str2, False)
        Catch ex As Exception When TypeOf ex Is IO.FileNotFoundException OrElse TypeOf ex Is IO.DirectoryNotFoundException OrElse TypeOf ex Is IO.IOException OrElse TypeOf ex Is IO.PathTooLongException OrElse TypeOf ex Is NotSupportedException OrElse TypeOf ex Is Security.SecurityException OrElse TypeOf ex Is UnauthorizedAccessException
            Console.WriteLine("{0}: {1}", ex.GetType.Name, ex.Message)
            Environment.Exit(105)
        End Try
    End Sub

    ''' <summary>Handles the <see cref="BaseResXFileCodeGeneratorEx.BeforeGenerateText"/> event of generator</summary>
    ''' <param name="Generator">Generator to be used</param>
    ''' <remarks>When <paramref name="Generator"/> is <see cref="Microsoft.VisualC.CppCodeGeneratorBase"/> some special initialization is needed</remarks>
    Private Sub generator_BeforeGenerateText(ByVal Generator As ICodeGenerator)
        If TypeOf Generator Is Microsoft.VisualC.CppCodeGeneratorBase Then
            With DirectCast(Generator, Microsoft.VisualC.CppCodeGeneratorBase)
                .TypeResolutionService = New MyTypeResolutionService
            End With
        End If
    End Sub
    ''' <summary>Very easy implementation of <see cref="ITypeResolutionService"/></summary>
    Private Class MyTypeResolutionService
        Implements ITypeResolutionService

        ''' <summary>Gets the requested assembly.</summary>
        ''' <returns>An instance of the requested assembly, or null if no assembly can be located.</returns>
        ''' <param name="name">The name of the assembly to retrieve.</param>
        Public Function GetAssembly(ByVal name As System.Reflection.AssemblyName) As System.Reflection.Assembly Implements System.ComponentModel.Design.ITypeResolutionService.GetAssembly
            Return GetAssembly(name, True)
        End Function

        ''' <summary>Gets the requested assembly.</summary>
        ''' <returns>An instance of the requested assembly, or null if no assembly can be located.</returns>
        ''' <param name="name">The name of the assembly to retrieve.</param>
        ''' <param name="throwOnError">true if this method should throw an exception if the assembly cannot be located; otherwise, false, and this method returns null if the assembly cannot be located.</param>
        Public Function GetAssembly(ByVal name As System.Reflection.AssemblyName, ByVal throwOnError As Boolean) As System.Reflection.Assembly Implements System.ComponentModel.Design.ITypeResolutionService.GetAssembly
            Try
                Return Assembly.Load(name)
            Catch
                If throwOnError Then Throw
                Return Nothing
            End Try
        End Function

        ''' <summary>Gets the path to the file from which the assembly was loaded.</summary>
        ''' <returns>The path to the file from which the assembly was loaded.</returns>
        ''' <param name="name">The name of the assembly.</param>
        Public Function GetPathOfAssembly(ByVal name As System.Reflection.AssemblyName) As String Implements System.ComponentModel.Design.ITypeResolutionService.GetPathOfAssembly
            Return Assembly.Load(name).ManifestModule.FullyQualifiedName
        End Function

        ''' <summary>Loads a type with the specified name.</summary>
        ''' <returns>An instance of <see cref="T:System.Type" /> that corresponds to the specified name, or null if no type can be found.</returns>
        ''' <param name="name">The name of the type. If the type name is not a fully qualified name that indicates an assembly, this service will search its internal set of referenced assemblies.</param>
        Public Overloads Function [GetType](ByVal name As String) As System.Type Implements System.ComponentModel.Design.ITypeResolutionService.GetType
            Return Type.GetType(name)
        End Function

        ''' <summary>Loads a type with the specified name.</summary>
        ''' <returns>An instance of <see cref="T:System.Type" /> that corresponds to the specified name, or null if no type can be found.</returns>
        ''' <param name="name">The name of the type. If the type name is not a fully qualified name that indicates an assembly, this service will search its internal set of referenced assemblies.</param>
        ''' <param name="throwOnError">true if this method should throw an exception if the assembly cannot be located; otherwise, false, and this method returns null if the assembly cannot be located.</param>
        Public Overloads Function [GetType](ByVal name As String, ByVal throwOnError As Boolean) As System.Type Implements System.ComponentModel.Design.ITypeResolutionService.GetType
            Return Type.GetType(name, throwOnError)
        End Function

        ''' <summary>Loads a type with the specified name.</summary>
        ''' <returns>An instance of <see cref="T:System.Type" /> that corresponds to the specified name, or null if no type can be found.</returns>
        ''' <param name="name">The name of the type. If the type name is not a fully qualified name that indicates an assembly, this service will search its internal set of referenced assemblies.</param>
        ''' <param name="throwOnError">true if this method should throw an exception if the assembly cannot be located; otherwise, false, and this method returns null if the assembly cannot be located.</param>
        ''' <param name="ignoreCase">true to ignore case when searching for types; otherwise, false.</param>
        Public Overloads Function [GetType](ByVal name As String, ByVal throwOnError As Boolean, ByVal ignoreCase As Boolean) As System.Type Implements System.ComponentModel.Design.ITypeResolutionService.GetType
            Return Type.GetType(name, throwOnError, ignoreCase)
        End Function

        ''' <summary>Adds a reference to the specified assembly.</summary>
        ''' <param name="name">An <see cref="T:System.Reflection.AssemblyName" /> that indicates the assembly to reference.</param>
        ''' <remarks>Does  nothing</remarks>
        Public Sub ReferenceAssembly(ByVal name As System.Reflection.AssemblyName) Implements System.ComponentModel.Design.ITypeResolutionService.ReferenceAssembly
            'Do nothing
        End Sub
    End Class
    ''' <summary>Echoes application version and copyright information</summary>
    Private Sub Logo()
        Console.WriteLine("{0} {1} {2}", My.Application.Info.ProductName, My.Application.Info.Title, My.Application.Info.Version)
        Console.WriteLine("{0}, {1}", My.Application.Info.CompanyName, My.Application.Info.Copyright)
    End Sub

    ''' <summary>Very basic implementation of <see cref="Microsoft.VisualStudio.Shell.Interop.IVsGeneratorProgress"/></summary>
    Private Class ProgressMonitor : Implements Microsoft.VisualStudio.Shell.Interop.IVsGeneratorProgress
        ''' <summary>Called when generator encounters an error</summary>
        ''' <param name="bstrError">Error description</param>
        ''' <param name="dwColumn">Column in source code</param>
        ''' <param name="dwLine">Line in source code</param>
        ''' <param name="dwLevel">Error level</param>
        ''' <param name="fWarning">If not zero then this is not error, but warning only</param>
        ''' <returns>0</returns>
        Public Function GeneratorError(ByVal fWarning As Integer, ByVal dwLevel As UInteger, ByVal bstrError As String, ByVal dwLine As UInteger, ByVal dwColumn As UInteger) As Integer Implements Microsoft.VisualStudio.Shell.Interop.IVsGeneratorProgress.GeneratorError
            If fWarning <> 0 Then Console.WriteLine(My.Resources.Warning0, bstrError) _
            Else Console.WriteLine(My.Resources.Error0, bstrError)
            Return 0
        End Function
        ''' <summary>Reports progress</summary>
        ''' <param name="nComplete">True if operation is completed</param>
        ''' <param name="nTotal">Progress percentage</param>
        ''' <returns>0</returns>
        Public Function Progress(ByVal nComplete As UInteger, ByVal nTotal As UInteger) As Integer Implements Microsoft.VisualStudio.Shell.Interop.IVsGeneratorProgress.Progress
            Return 0
        End Function
    End Class
    ''' <summary>Provides code generator for custom tool implementation</summary>
    Private Class InternalProvider
        Implements Microsoft.VisualStudio.OLE.Interop.IServiceProvider
        Implements IVSMDCodeDomProvider
        ''' <summary>Provider to be provided</summary>
        Private provider As CodeDom.Compiler.CodeDomProvider
        ''' <summary>Guid of CodeDOM service</summary>
        Public Shared ReadOnly CodeDomServiceGuid As New Guid("{73E59688-C7C4-4a85-AF64-A538754784C5}")
        ''' <summary>CTor</summary>
        ''' <param name="Provider">Provider to provide</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Provider"/> is null</exception>
        Public Sub New(ByVal Provider As CodeDom.Compiler.CodeDomProvider)
            If Provider Is Nothing Then Throw New ArgumentNullException("Provider")
            Me.provider = Provider
        End Sub
        ''' <summary>Gets CodeDom provider</summary>
        ''' <returns><see cref="CodeDom.Compiler.CodeDomProvider"/></returns>
        Public ReadOnly Property CodeDomProvider() As Object Implements Microsoft.VisualStudio.Designer.Interfaces.IVSMDCodeDomProvider.CodeDomProvider
            Get
                Return provider
            End Get
        End Property

        ''' <summary>Queries a service from this <see cref="Microsoft.VisualStudio.OLE.Interop.IServiceProvider"/></summary>
        ''' <param name="guidService">Identifies type of service</param>
        ''' <param name="riid">Ignored</param>
        ''' <param name="ppvObject">Returns pointer to service object; <see cref="IntPtr.Zero"/> if no object is returned</param>
        ''' <returns>When <paramref name="guidService"/> is <see cref="CodeDomServiceGuid"/> <see cref="VSConstants.S_OK"/>; <see cref="VSConstants.S_FALSE"/> otherwise</returns>
        ''' <remarks>This <see cref="Microsoft.VisualStudio.OLE.Interop.IServiceProvider"/> provides only CodeDOM service <see cref="IVSMDCodeDomProvider"/>. It is returned as pointer to current instance itself.</remarks>
        Private Function QueryService(ByRef guidService As System.Guid, ByRef riid As System.Guid, ByRef ppvObject As System.IntPtr) As Integer Implements Microsoft.VisualStudio.OLE.Interop.IServiceProvider.QueryService
            If guidService = CodeDomServiceGuid Then
                ppvObject = Marshal.GetComInterfaceForObject(Me, GetType(IVSMDCodeDomProvider))
                Return VSConstants.S_OK
            End If
            ppvObject = IntPtr.Zero
            Return VSConstants.S_FALSE
        End Function
    End Class



End Module
