Imports System.Reflection, Tools.ExtensionsT, Tools.ReflectionT, System.CodeDom
Imports System.IO.Packaging

''' <summary>Generates a plugin</summary>
Public Class Generator
    ''' <summary>Name of resource that contains embdeded template</summary>
    Public Const TemplateResourcename$ = "Tools.TotalCommanderT.PluginBuilder.Template.pseudozip"
    ''' <summary>CTor from assembly</summary>
    ''' <param name="Assembly">Assembly to generate plugin for</param>
    ''' <exception cref="ArgumentNullException"><paramref name="Assembly"/> is null</exception>
    ''' <remarks><see cref="OutputDirectory"/> is initilaized to <see cref="IO.Path.GetDirectoryName"/>(<paramref name="Assembly"/>.<see cref="Assembly.Location">Location</see>) when <paramref name="Assembly"/>.<see cref="Assembly.Location">Location</see> is not null; to <see cref="Environment.CurrentDirectory"/> otherwise.</remarks>
    Public Sub New(ByVal Assembly As Assembly)
        Me.Assembly = Assembly
        If Assembly.Location Is Nothing Then
            OutputDirectory = Environment.CurrentDirectory
        Else
            OutputDirectory = IO.Path.GetDirectoryName(Assembly.Location)
        End If
    End Sub
    ''' <summary>CTor from assembly and output directory</summary>
    ''' <param name="Assembly">Assembly to generate plugin for</param>
    ''' <param name="OutputDirectory">Output directory to generate plugin into</param>
    ''' <exception cref="ArgumentNullException"><paramref name="Assembly"/> or <paramref name="OutputDirectory"/> is null</exception>
    Public Sub New(ByVal Assembly As Assembly, ByVal OutputDirectory As String)
        Me.Assembly = Assembly
        Me.OutputDirectory = OutputDirectory
    End Sub
    ''' <summary>CTro from type and output directory</summary>
    ''' <param name="Type">Type to generate plugin for. Type must represent Total Commander plugin class - this is not checked in CTor but later.</param>
    ''' <param name="OutputDirectory">Output directory to generate plugin into</param>
    ''' <exception cref="ArgumentNullException"><paramref name="Type"/> or <paramref name="OutputDirectory"/> is null</exception>
    Public Sub New(ByVal Type As Type, ByVal OutputDirectory As String)
        If Type Is Nothing Then Throw New ArgumentNullException("Type")
        Assembly = Type.Assembly
        Types = New Type() {Type}
        OutputDirectory = OutputDirectory
    End Sub
    ''' <summary>Contains value of the <see cref="Assembly"> property</see></summary>
    Private _Assembly As Assembly
    ''' <summary>Gets or sets assembly to generate plugin for</summary>
    ''' <exception cref="ArgumentNullException">Value beign set is null</exception>
    Public Property Assembly() As Assembly
        Get
            Return _Assembly
        End Get
        Set(ByVal value As Assembly)
            If value Is Nothing Then Throw New ArgumentNullException("value")
            _Assembly = value
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="ProjectTemplateDirectory"/> property</summary>
    Private _ProjectTemplateDirectory As String
    ''' <summary>Gets or sets directory to use project template from</summary>
    ''' <remarks>When null, build-in template is used</remarks>
    Public Property ProjectTemplateDirectory() As String
        Get
            Return _ProjectTemplateDirectory
        End Get
        Set(ByVal value As String)
            _ProjectTemplateDirectory = value
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="OutputDirectory"/> property</summary>
    Private _OutputDirectory As String
    ''' <summary>Gets or sets output directory to wtire plugin into</summary>
    ''' <exception cref="ArgumentNullException">Value being set is null</exception>
    Public Property OutputDirectory() As String
        Get
            Return _OutputDirectory
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then Throw New ArgumentNullException("value")
            _OutputDirectory = value
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="IntermediateDirectory"/> property</summary>
    Private _IntermediateDirectory$
    ''' <summary>Gets or sets intermediate directory where temporary files are stored</summary>
    ''' <returns>Intermediate directory to store temporary files in</returns>
    ''' <value>Intermediate directory to store temporary files in. Null to chose directory automatically</value>
    ''' <remarks>{0} is replaced by plugin name. When such directory does not exist, it is created. Setting this property to null, sets <see cref="CleanIntermediateDirectory"/> to true.</remarks>
    Public Property IntermediateDirectory() As String
        Get
            Return _IntermediateDirectory
        End Get
        Set(ByVal value As String)
            _IntermediateDirectory = value
            If value Is Nothing Then CleanIntermediateDirectory = True
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="CleanIntermediateDirectory"/> property</summary>
    Private _CleanIntermediateDirectory As Boolean = True
    ''' <summary>Gets or sets value indicating if intermediate directory specified in <see cref="IntermediateDirectory"/> is deled after plugin generation</summary>
    ''' <returns>True when directory will be deleted after plugin generation, false when intermediate files will remain after generation.</returns>
    ''' <value>True to ensure that intermediate directory is deleted after compile, false to let files in intermediate directory. This property cannot be false when <see cref="IntermediateDirectory"/> is null.</value>
    ''' <exception cref="InvalidOperationException">Value is being set to false when <see cref="IntermediateDirectory"/> is null</exception>
    Public Property CleanIntermediateDirectory() As Boolean
        Get
            Return _CleanIntermediateDirectory
        End Get
        Set(ByVal value As Boolean)
            If Not value AndAlso IntermediateDirectory Is Nothing Then Throw New ArgumentNullException(String.Format(My.Resources.XcannotbeYwhenZisW, "CleanIntermediateDirectory", "false", "IntermediateDirectory", "null"))
            _CleanIntermediateDirectory = value
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="Types"/> property</summary>
    Private _Types As Type()
    ''' <summary>Gets or sets types to generate plugins for</summary>
    ''' <value>All the types in array must be in assembly <see cref="Assembly"/>; when null all public types that qualify to be Total Commander plugins will be used.</value>
    ''' <returns>Type plugins will be generated for. Null when all possible types will be used.</returns>
    ''' <remarks>When null, this property is set during generation process to actual types plugins are generated for.
    ''' <para>Passing constructed generic type here is only way to generate plugin of generic type. Otherwise plugin generator skips generic types.</para></remarks>
    Public Property Types() As Type()
        Get
            Return _Types
        End Get
        Set(ByVal value As Type())
            _Types = value
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="Filter"/> property</summary>
    Private _Filer As PluginType = PluginType.Content Or PluginType.FileSystem Or PluginType.Lister Or PluginType.Packer
    ''' <summary>Gets or sets plugin type filter. Only plugins of types according to given OR-mask will be generated.</summary>
    ''' <returns>OR-ed values of <see cref="PluginType"/> type indicating which plugin types are generated</returns>
    ''' <value>Set plugin types to generate plugins for. By default all the plugin types are generated.</value>
    Public Property Filer() As PluginType
        Get
            Return _Filer
        End Get
        Set(ByVal value As PluginType)
            _Filer = value
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="RenamingDictionary"/> property</summary>
    Private _RenamingDictionary As New Dictionary(Of String, String)
    ''' <summary>Dictionary containing <see cref="Type.FullName"/>s as key and plugin files names (witout extension) as values.</summary>
    ''' <remarks>Generated plugin files are named according to this dictionary. When dictionary entry for any type is missing, plugin is named according to full name of type.
    ''' <para><see cref="RenamingDictionary"/> takes precedence to <see cref="TotalCommanderPluginAttribute.Name"/>.</para></remarks>
    Public ReadOnly Property RenamingDictionary() As Dictionary(Of String, String)
        Get
            Return _RenamingDictionary
        End Get
    End Property

    ''' <summary>Generates plugins</summary>
    Public Sub Generate()
        If Types Is Nothing Then
            Dim iTypes As New List(Of Type)
            For Each Type In Assembly.GetTypes                       'TODO: All plugin types
                If Type.IsPublic OrElse Type.IsNestedPublic AndAlso GetType(FileSystemPlugin).IsAssignableFrom(Type) AndAlso Not Type.IsAbstract AndAlso Not Type.IsGenericTypeDefinition AndAlso Type.HasDefaultCTor Then
                    iTypes.Add(Type)
                End If
            Next
            Types = iTypes.ToArray
        End If
        Dim Ret As New List(Of String)
        For Each Type In Types
            Generate(Type)
        Next
    End Sub
    ''' <summary>Contains value of the <see cref="LogToConsole"/> property</summary>
    Private _LogToConsole As Boolean = False
    ''' <summary>Gets or sets value indicationg if generator will log its progress to console</summary>
    Friend Property LogToConsole() As Boolean
        Get
            Return _LogToConsole
        End Get
        Set(ByVal value As Boolean)
            _LogToConsole = value
        End Set
    End Property

    ''' <summary>Log a log information</summary>
    ''' <param name="Text">Formating string</param>
    ''' <param name="Obj">Format patameters</param>
    ''' <seelaso cref="System.String.Format"/>
    Private Sub Log(ByVal Text$, ByVal ParamArray Obj As Object())
        If LogToConsole Then Console.WriteLine(Text, Obj)
    End Sub

    ''' <summary>Generates plugin from type</summary>
    ''' <param name="Type">Type to generate plugin for</param>
    ''' <returns>Path to generated plugin file</returns>
    ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="Type"/> is open generic type -or- <paramref name="Type"/> is abstract -or- <paramref name="Type"/> has not acessible default CTor -or- <paramref name="Type"/> is not public</exception>
    ''' <exception cref="MissingMethodException">Plugin base type declares method with <see cref="PluginMethodAttribute"/> pointing to method that is not member of that type.</exception>
    ''' <exception cref="AmbiguousMatchException">Plugin base type declares method with <see cref="PluginMethodAttribute"/> pointing to method that is overloaded on that type.</exception>
    Private Function Generate(ByVal Type As Type) As String
        Dim Thrown As Exception = Nothing
        Log("Creating plugin for type {0}.", Type.Name)
        If Type Is Nothing Then Throw New ArgumentNullException("Type")
        If Type.IsGenericTypeDefinition Then Throw New ArgumentException(My.Resources.e_OpenGeneric.f(Type.FullName))
        If Type.IsAbstract Then Throw New ArgumentException(My.Resources.e_Abstract.f(Type.FullName))
        If Not Type.HasDefaultCTor Then Throw New ArgumentException(My.Resources.e_NoDefaultCTor.f(Type.FullName))
        If Not Type.IsPublic AndAlso Not Type.IsNestedPublic Then Throw New ArgumentException(My.Resources.e_NotPublic.f(Type.FullName))
        Dim Attr = Type.GetAttribute(Of TotalCommanderPluginAttribute)()
        Dim name$
        If Attr Is Nothing OrElse Attr.Name Is Nothing Then
            name = GetSafeName(Type.FullName)
        Else
            name = Attr.Name
        End If
        'Set directory
        Dim IntermediateDirectory$
        If Me.IntermediateDirectory Is Nothing Then
            Dim TempPath$
            Dim i% = 1
            Do
                TempPath = IO.Path.Combine(IO.Path.GetTempPath, String.Format("TCPluginGenerator_{0}_{1}", GetSafeName(Type.FullName), i))
                i += 1
            Loop While IO.Directory.Exists(TempPath) OrElse IO.File.Exists(TempPath)
            IntermediateDirectory = TempPath
            Log("Creating temporary intermediate directory {0}", IntermediateDirectory)
        Else
            IntermediateDirectory = String.Format(Me.IntermediateDirectory, GetSafeName(Type.FullName))
            If IO.File.Exists(IntermediateDirectory) Then IO.File.Delete(IntermediateDirectory)
            If IO.Directory.Exists(IntermediateDirectory) Then
                Log("Using predefined intermediate directory {0}.", IntermediateDirectory)
                Try
                    For Each file In IO.Directory.GetFiles(IntermediateDirectory)
                        IO.File.Delete(file)
                    Next
                    For Each SubDir In IO.Directory.GetDirectories(IntermediateDirectory)
                        IO.Directory.Delete(SubDir, True)
                    Next
                Catch ex As Exception
                    Log("Error while clening intermediate directory.")
                    Throw
                End Try
            Else
                Log("Creating user-defined intermediate directory {0}", IntermediateDirectory)
            End If
        End If
        If Not IO.Directory.Exists(IntermediateDirectory) Then IO.Directory.CreateDirectory(IntermediateDirectory)
        Try
            'Copy template
            Dim ProjectDirectory = IO.Path.Combine(IntermediateDirectory, "Project")
            Dim ProjectFile = IO.Path.Combine(ProjectDirectory, "Tools.TotalCommander.Plugin.vcproj")
            Dim binDirectory = IO.Path.Combine(ProjectDirectory, "bin")
            Dim objDirectory = IO.Path.Combine(ProjectDirectory, "obj")
            Log("Create project directory {0}", ProjectDirectory)
            IO.Directory.CreateDirectory(ProjectDirectory)
            If Me.ProjectTemplateDirectory IsNot Nothing Then 'Copy
                Log("Copy template from {0}", Me.ProjectTemplateDirectory)
                My.Computer.FileSystem.CopyDirectory(Me.ProjectTemplateDirectory, ProjectDirectory)
            Else 'Extract
                Log("Extract built-in template", Me.ProjectTemplateDirectory)
                Using zip = ZipPackage.Open(GetType(Generator).Assembly.GetManifestResourceStream(TemplateResourcename), IO.FileMode.Open, IO.FileAccess.Read)
                    For Each part In zip.GetParts
                        Dim PartPath = IO.Path.Combine(ProjectDirectory, part.Uri.OriginalString.Substring(1))
                        If Not IO.Directory.Exists(IO.Path.GetDirectoryName(PartPath)) Then IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(PartPath))
                        Using _
                            PartStream = part.GetStream(IO.FileMode.Open, IO.FileAccess.Read), _
                            UnzipFile = IO.File.Open(PartPath, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read), _
                            r = New IO.BufferedStream(PartStream), _
                            w = New IO.BufferedStream(UnzipFile)
                            For i = 0 To r.Length - 1
                                w.WriteByte(CByte(r.ReadByte))
                            Next
                        End Using
                    Next
                End Using
            End If
            MakeWriteable(IntermediateDirectory)
            Log("Copy assembly file {0}", Assembly.Location)
            IO.File.Copy(Assembly.Location, IO.Path.Combine(ProjectDirectory, IO.Path.GetFileName(Assembly.Location)), True)
            'Prepare projet
            Dim ext$
            If GetType(FileSystemPlugin).IsAssignableFrom(Type) Then
                ext = "wfx"
                Log("Plugin type: File System Plugin (wfx)")
                PreparePlugin(Type, ProjectDirectory, GetType(FileSystemPlugin), "TC_WFX")
            Else 'TODO: Support all plugin types
                Throw New ArgumentException(My.Resources.e_NotAPluginType.f(Type.FullName))
            End If
            Dim OutFile = IO.Path.Combine(binDirectory, name & "." & ext)
            Log("Generating assembly info")
            MakeAssemblyInfo(Type, ProjectDirectory)
            'Excute compiler
            Dim ec%
            Dim dic As New Dictionary(Of String, String)
            dic.Add("PluginOutputExtension", ext)
            dic.Add("PluginOutputName", name)
            Log("Invoking compiler {0}", IO.Path.GetFileName(VCBuild))
            ec = Exec(VCBuild, String.Format("/r ""{0}""", ProjectFile), dic)
            If ec <> 0 Then Throw New ExitCodeException(ec, IO.Path.GetFileName(VCBuild))
            'Copy result
            Dim TargetFile = IO.Path.Combine(Me.OutputDirectory, name & "." & ext)
            Log("Copying output from {0} to {1}", OutFile, TargetFile)
            IO.File.Copy(OutFile, TargetFile, True)
            Dim PdbFile = IO.Path.Combine(binDirectory, name & ".pdb")
            If CopyPDB AndAlso IO.File.Exists(PdbFile) Then
                Dim PdbTarget As String = IO.Path.Combine(Me.OutputDirectory, name & ".pdb")
                Log("Copying pdb file from {0} to {1}", PdbFile, PdbTarget)
                IO.File.Copy(PdbFile, PdbTarget, True)
            End If
            Return name & "." & ext
        Catch ex As Exception
            Thrown = ex
            Throw
        Finally
            If CleanIntermediateDirectory Then
                Log("Removing intermediate directory {0}", IntermediateDirectory)
                Try
                    IO.Directory.Delete(IntermediateDirectory, True)
                Catch ex As Exception
                    Log("Error while removing intermediate directory {0}", ex.Message)
                    If Thrown Is Nothing Then Throw
                End Try
            End If
        End Try
    End Function
    ''' <summary>COntains value of the <see cref="CopyPDB"/> property</summary>
    Private _CopyPDB As Boolean
    ''' <summary>Gets or sets value indicating if pdb file for plugin will be copied to outpud directory</summary>
    Public Property CopyPDB() As Boolean
        Get
            Return _CopyPDB
        End Get
        Set(ByVal value As Boolean)
            _CopyPDB = value
        End Set
    End Property
#Region "Paths"
    ''' <summary>Contains value of the <see cref="VCBuild"/></summary>
    Private _vcbuild$ = My.Settings.vcbuild
    ''' <summary>Gets to sets path to vcbuild.exe</summary>
    ''' <remarks>Default value is stored in settings</remarks>
    Public Property VCBuild$()
        Get
            Return _vcbuild
        End Get
        Set(ByVal value$)
            _vcbuild = value
        End Set
    End Property
#End Region
    ''' <summary>Executes a process and waits for it to terminate</summary>
    ''' <param name="Program">Program to execute</param>
    ''' <param name="ComandLine">Program arguments</param>
    ''' <returns>Program exit code</returns>
    Private Function Exec%(ByVal Program$, ByVal CommandLine$, Optional ByVal Env As Dictionary(Of String, String) = Nothing) ', ByVal setenv As Boolean, ByVal objDir$) As Integer
        Dim p As New Process
        p.StartInfo.UseShellExecute = False
        p.StartInfo.FileName = Program
        p.StartInfo.Arguments = CommandLine
        If Env IsNot Nothing Then
            For Each ev In Env
                p.StartInfo.EnvironmentVariables.Add(ev.Key, ev.Value)
            Next
        End If
        Log("Executing {0} {1}", Program, CommandLine)
        p.Start()
        p.WaitForExit()
        Log("{0}> Exict code {1}.", vbTab, p.ExitCode)
        Return p.ExitCode
        'End If
    End Function

    ''' <summary>Removes the readonly atribute from all the files in given directory (recursively)</summary>
    ''' <param name="dir">Directory to remove attributes from</param>
    Private Sub MakeWriteable(ByVal dir$)
        For Each file In IO.Directory.GetFiles(dir, "*", IO.SearchOption.AllDirectories)
            Dim fa As System.IO.FileAttributes = IO.File.GetAttributes(file)
            If (fa And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly Then _
                IO.File.SetAttributes(file, fa And Not IO.FileAttributes.ReadOnly)
        Next
    End Sub
    ''' <summary>Removes file-name-invalid characters from string</summary>
    ''' <param name="Name">String to remove chaRACTERS FROM</param>
    ''' <returns><paramref name="Name"/> with <see cref="IO.Path.GetInvalidFileNameChars"/> removed</returns>
    Private Function GetSafeName(ByVal Name$) As String
        For Each forbidden In IO.Path.GetInvalidFileNameChars
            Name = Name.Replace(forbidden, "")
        Next
        Return Name
    End Function
    ''' <summary>Prepares files for wfx plugin</summary>
    ''' <param name="Type">Type to prepare project for</param>
    ''' <param name="ProjectDirectory">Directory peoject is stored in</param>
    ''' <param name="PluginType">Plugin base class</param>
    ''' <param name="DefinedBy">C++ #define to define call of CTOr of plugin</param>
    ''' <exception cref="MissingMethodException"><paramref name="PluginType"/> has method decorated with <see cref="PluginMethodAttribute"/> with <see cref="PluginMethodAttribute.ImplementedBy"/> pointing to method that is not member of <paramref name="PluginType"/>.</exception>
    ''' <exception cref="AmbiguousMatchException"><paramref name="PluginType"/> has method decorated with <see cref="PluginMethodAttribute"/> with <see cref="PluginMethodAttribute.ImplementedBy"/> pointing to method that is overloaded on <paramref name="PluginType"/>.</exception>
    Private Sub PreparePlugin(ByVal Type As Type, ByVal ProjectDirectory$, ByVal PluginType As Type, ByVal DefinedBy As String)
        Using defineh = IO.File.Open(IO.Path.Combine(ProjectDirectory, "define.h"), IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read), _
                w As New IO.StreamWriter(defineh, System.Text.Encoding.Default)
            w.WriteLine("#define {0} {1}", DefinedBy, GetTypeSignature(Type))
            For Each method In PluginType.GetMethods(BindingFlags.Instance Or BindingFlags.Public)
                Dim attr = method.GetAttribute(Of PluginMethodAttribute)(False)
                If attr Is Nothing Then Continue For
                Dim Define As Boolean
                If attr.ImplementedBy Is Nothing Then
                    Define = True
                Else
                    Dim ImplementingMethod = PluginType.GetMethod(attr.ImplementedBy)
                    If ImplementingMethod Is Nothing Then Throw New MissingMethodException("Type {0} does not implement method {1} required by {2} applied onto method {3}.".f(PluginType.FullName, attr.ImplementedBy, attr.GetType.FullName, method.Name))
                    Dim DerivedMethod = ImplementingMethod.GetOverridingMethod(Type)
                    Dim NotSupported = DerivedMethod.GetAttribute(Of MethodNotSupportedAttribute)()
                    Define = NotSupported Is Nothing
                End If
                If Define Then
                    w.WriteLine("#define " & attr.DefinedBy)
                End If
            Next
            w.WriteLine("#using ""{0}""", Assembly.Location)
            w.Write("#using ""{0}""", Assembly.Load("Tools.TotalCommander, PublicKeyToken=373c02ac923768e6").Location)
        End Using
    End Sub
    ''' <summary>C++ code provider</summary>
    Private Provider As New Microsoft.VisualC.CppCodeProvider
    ''' <summary>Gets expression to ceate instance of type in C++</summary>
    ''' <param name="Type">Type to get expression for</param>
    Private Function GetTypeSignature(ByVal Type As Type) As String
        Dim b As New System.Text.StringBuilder
        Dim w As New IO.StringWriter(b)
        Provider.GenerateCodeFromExpression(New CodeObjectCreateExpression(Type, New CodeExpression() {}), w, New Compiler.CodeGeneratorOptions())
        w.Flush()
        Return b.ToString
    End Function
    ''' <summary>Writes the AssemblyInfo2.cpp file</summary>
    ''' <param name="Type">Plugin type</param>
    ''' <param name="ProjectDirectory">Project directory</param>
    Private Sub MakeAssemblyInfo(ByVal Type As Type, ByVal ProjectDirectory$)
        Using file = IO.File.Open(IO.Path.Combine(ProjectDirectory, "AssemblyInfo2.cpp"), IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read), _
                w As New IO.StreamWriter(file)
            w.WriteLine("using namespace System;")
            w.WriteLine("using namespace System::Reflection;")
            w.WriteLine("using namespace System::Runtime::CompilerServices;")
            w.WriteLine("using namespace System::Runtime::InteropServices;")
            w.WriteLine("using namespace System::Security::Permissions;")

            Dim tAsm = Type.Assembly
            Dim Version = tAsm.GetAttribute(Of AssemblyVersionAttribute)()
            If Version IsNot Nothing Then w.WriteLine("[assembly:AssemblyVersionAttribute(""{0}"")]", Version.Version.Replace("\", "\\").Replace("""", "\"""))
            Dim Culture = tAsm.GetAttribute(Of AssemblyCultureAttribute)()
            If Culture IsNot Nothing Then w.WriteLine("[assembly:AssemblyCultureAttribute(""{0}"")];", Culture.Culture.Replace("\", "\\").Replace("""", "\"""))
            Dim Trademark = tAsm.GetAttribute(Of AssemblyTrademarkAttribute)()
            If Trademark IsNot Nothing Then w.WriteLine("[assembly:AssemblyTrademarkAttribute(""{0}"")];", Trademark.Trademark.Replace("\", "\\").Replace("""", "\"""))
            Dim Copyright = tAsm.GetAttribute(Of AssemblyCopyrightAttribute)()
            If Copyright IsNot Nothing Then w.WriteLine("[assembly:AssemblyCopyrightAttribute(""{0}"")];", Copyright.Copyright.Replace("\", "\\").Replace("""", "\"""))
            Dim Product = tAsm.GetAttribute(Of AssemblyProductAttribute)()
            If Product IsNot Nothing Then w.WriteLine("[assembly:AssemblyProductAttribute(""{0}"")];", Product.Product.Replace("\", "\\").Replace("""", "\"""))
            Dim Company = tAsm.GetAttribute(Of AssemblyCompanyAttribute)()
            If Company IsNot Nothing Then w.WriteLine("[assembly:AssemblyCompanyAttribute(""{0}"")];", Company.Company.Replace("\", "\\").Replace("""", "\"""))
            Dim Configuration = tAsm.GetAttribute(Of AssemblyConfigurationAttribute)()
            If Configuration IsNot Nothing Then w.WriteLine("[assembly:AssemblyConfigurationAttribute(""{0}"")];", Configuration.Configuration.Replace("\", "\\").Replace("""", "\"""))
            Dim NeutralResourcesLanguage = tAsm.GetAttribute(Of Resources.NeutralResourcesLanguageAttribute)()
            If NeutralResourcesLanguage IsNot Nothing Then w.WriteLine("[assembly:Resources::NeutralResourcesLanguageAttribute(""{0}"",Resources::UltimateResourceFallbackLocation::{1:F}];", NeutralResourcesLanguage.CultureName, NeutralResourcesLanguage.Location)
            Dim FileVersion = tAsm.GetAttribute(Of AssemblyFileVersionAttribute)()
            If FileVersion IsNot Nothing Then w.WriteLine("[assembly:AssemblyFileVersionAttribute(""{0}"")];", FileVersion.Version.Replace("\", "\\").Replace("""", "\"""))
            Dim InformationVersion = tAsm.GetAttribute(Of AssemblyInformationalVersionAttribute)()
            If InformationVersion IsNot Nothing Then w.WriteLine("[assembly:AssemblyInformationalVersionAttribute(""{0}"")];", InformationVersion.InformationalVersion.Replace("\", "\\").Replace("""", "\"""))

            Dim PluginInfo = Type.GetAttribute(Of TotalCommanderPluginAttribute)()
            Dim Guid$ = Nothing, Title$ = Nothing, Description$ = Nothing
            If PluginInfo IsNot Nothing Then
                If PluginInfo.AssemblyGuid IsNot Nothing Then Guid = PluginInfo.AssemblyGuid
                If PluginInfo.AssemblyTitle IsNot Nothing Then Title = PluginInfo.AssemblyTitle
                If PluginInfo.AssemblyDescription IsNot Nothing Then Description = PluginInfo.AssemblyDescription
            End If
            If Title Is Nothing Then
                Dim TitleA = tAsm.GetAttribute(Of AssemblyTitleAttribute)()
                If TitleA IsNot Nothing Then Title = TitleA.Title
            End If
            If Description Is Nothing Then
                Dim DescriptionA = tAsm.GetAttribute(Of AssemblyDescriptionAttribute)()
                If DescriptionA IsNot Nothing Then Description = DescriptionA.Description
            End If
            If Guid IsNot Nothing Then w.WriteLine("[assembly:AssemblyGuidAttribute(""{0}"")];", Guid)
            If Title IsNot Nothing Then w.WriteLine("[assembly:AssemblyTitleAttribute(""{0}"")];", Title)
            If Description IsNot Nothing Then w.WriteLine("[assembly:AssemblyDescriptionAttribute(""{0}"")];", Description)
        End Using
    End Sub
End Class

''' <summary>Exception thrown when process exited with unecpedted exit code</summary>
Public Class ExitCodeException : Inherits Exception
    ''' <summary>CTor</summary>
    ''' <param name="ExitCode">Process exit code</param>
    ''' <param name="process">Process name</param>
    Public Sub New(ByVal ExitCode As Integer, ByVal process As String)
        MyBase.New("Process {0} exited with code {1}.".f(process, ExitCode))
        _Process = process
        _ExicTode = ExitCode
    End Sub
    ''' <summary>Contains value of the <see cref="Process"/> property</summary>
    Private ReadOnly _Process$
    ''' <summary>Contains value of the <see cref="ExicTode"/> property</summary>
    Private ReadOnly _ExicTode%
    ''' <summary>Gets process that returned exit code</summary>
    Public ReadOnly Property Process$()
        Get
            Return _Process
        End Get
    End Property
    ''' <summary>Gets exitcode returned by process</summary>
    Public ReadOnly Property ExicTode%()
        Get
            Return _ExicTode
        End Get
    End Property
End Class