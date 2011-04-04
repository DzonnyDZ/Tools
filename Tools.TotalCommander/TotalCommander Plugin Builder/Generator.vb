Imports System.Reflection, Tools.ExtensionsT, Tools.ReflectionT, System.CodeDom
Imports System.IO.Packaging, System.Xml.Linq
Imports <xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
Imports Microsoft.Build.Evaluation
Imports Microsoft.Build.Utilities

''' <summary>Generates a plugin</summary>
''' <remarks>
''' This class generates one or more Total Commander pluggin wrappers from given assembly/types.
''' Wrapper generation process can be invoked from command line or programatically using <see cref="Generator"/> API.
''' Separate wrapper is generated for each plugin class.
''' Wrapper generation consists of following steps:
''' <list type="number">
''' <item>Prepare wrapper template - Template is copied to intermediate directory (either from specified template directory or from built-in template)</item>
''' <item>Configure tamplate - Type wraper is being generated for is inspected to detect plugin type and which methods the plugin implements. Appropriate settings are generated to template files. Namely: Conditional compilation is set up to generate only those functions supported by plugin; Module export definition is written; Assembly info is generated.</item>
''' <item>Invoke compiler - C++ compiler invoked via MSBuild to compile the wrapper. MSBuild and C++ compiler must be installled on your machine.</item>
''' </list>
''' Following rules apply to plugin being generated:
''' <list type="bullet">
''' <item>Plugin class must be non-abstract, must have defualt (parameter-less) constructor and must notbe open generic type. When class that looks like plugin class is found, but does not meet these requirements it is ignored.</item>
''' <item>Plugin class must derive from one of plugin types like <see cref="FileSystemPlugin"/>.</item>
''' <item>Plugin class overrides its's base class methods to define plugin functionality. A few methods must be overriden. Majority of methods is optional.</item>
''' <item>When non-compulsory method is overriden in derived class, wrapped generator examines it. When it is decorated with <see cref="MethodNotSupportedAttribute"/> it is NOT generated for the plugin; otherwise it is.</item>
''' <item>Plugin class can, of course, define as many as you want additional methoda, but those methods are not part of plugin contract. When a new version of Total Commander is issued that defines more plugin API functions, those functions are not automatically supported by the generator. A new version of generator must be issued as well.</item>
''' <item>When plugin class is decorated with <see cref="NotAPluginAttribute"/>, it is skipped from plugin generation.</item>
''' <item>Optionaly plugin class can be decorated with <see cref="TotalCommanderPluginAttribute"/> to precise plugin generation.</item>
''' <item>Plugin assembly is given correct plugin extension by plugin type.</item>
''' <item>Plugin assembly inherits certain attributes from assembly plugin type is defined in or from plugin type istelf. Namely:
''' <list type="table">
''' <item><term><see cref="AssemblyVersionAttribute"/></term><description>Got from assembly's <see cref="AssemblyVersionAttribute"/> (when defined); otherwise from <see cref="AssemblyName.Version"/>.</description></item>
''' <item><term><see cref="AssemblyTrademarkAttribute"/>, <see cref="AssemblyCopyrightAttribute"/>, <see cref="AssemblyProductAttribute"/>, <see cref="AssemblyCompanyAttribute"/>, <see cref="AssemblyConfigurationAttribute"/>, <see cref="Resources.NeutralResourcesLanguageAttribute"/>, <see cref="AssemblyFileVersionAttribute"/>, <see cref="AssemblyInformationalVersionAttribute"/></term><description>Derived from assembly attributes (when set; ignored when not set)</description></item>
''' <item><term><see cref="Runtime.InteropServices.GuidAttribute"/></term><description>Got from <see cref="TotalCommanderPluginAttribute.AssemblyGuid"/> of plugin type (when set; ignored when not set)</description></item>
''' <item><term><see cref="AssemblyTitleAttribute"/>, <see cref="AssemblyDescriptionAttribute"/></term><description>Got from <see cref="TotalCommanderPluginAttribute.AssemblyTitle"/> and <see cref="TotalCommanderPluginAttribute.AssemblyDescription"/> of plugin type when set; otherwise got from appropriate assembly attributes when set; otherwise ignored.</description></item>
''' </list>
''' </item>
''' <item>When giving wrapper asembly a string name (using <see cref="Generator.SnkPath"/>) the assembly plugin is defined in must have string name as well.</item>
''' <item>Wrapper name is specified by (in order of presedence): <see cref="Generator.RenamingDictionary"/>, <see cref="TotalCommanderPluginAttribute.Name"/>, <see cref="Type.FullName"/>.</item>
''' </list>
''' Wrapped debugging info is copied to target only when <see cref="Generator.CopyPDB"/> is set. Do not set this to true when name of your plugin DLL and plugin wrapper are same but the extension. (Your debugging info will be overwritten with almost useless debugging info of wrapper.)
''' <para>To genereta plugin wraper for your plugin from Visual Stuidio environment as part of build process, simply add plugin generator tool TCPluginBuilder.exe as post-build event. Example post-build event:</para>
''' <example>This command line invokes TCPluginBuilder.exe and generates plugin(s) from output of project, places the w?x files into project output directory and uses project obj directory as its intermediate directory. Intermediate files are not deleted. Built-in template is used. w?x assembly is not signed.
''' <code>TCPluginBuilder.exe "$(TargetPath)" /out "$(TargetDir)\" /int "$(ProjectDir)obj\$(ConfigurationName)" /keepint</code>
''' </example>
''' <para><b>Deploying the plugin:</b></para>
''' <para>When Total Commander refuses to load your plugin try following troubleshoting tips:</para>
''' <list type="bullet">
''' <item>Place plugin files inside the same directory as TOTALCMD.EXE or one of subdirectories.</item>
''' <item>Windows Vista: If you are ceating subdirectory inside Total Commander directory you will copy plugin for testing during development and you are changing rights on the directory in order to get rid of UAC, do not make yourself owner of the directory.</item>
''' <item>Restart Total Commander in order to test a new version of your plugin.</item>
''' </list>
''' </remarks>
''' <version version="1.5.3">Property <c>VCBuild</c> removed. Class now use MSBuild which is referenced as assembly. Reference to VCBuild.exe removed from settings as well.</version>
''' <version version="1.5.3">Property <c>SN</c> removed. Class now use MSBuild task to sign resulting assembly. Reference to SN.exe removed from settings as well.</version>
Public Class Generator
    ''' <summary>Name of resource that contains embdeded template</summary>
    Public Const TemplateResourcename$ = "Tools.TotalCommanderT.PluginBuilder.Template.pseudozip"
#Region "CTor"
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
#End Region
#Region "Properties"
    ''' <summary>Contains value of the <see cref="Assembly"> property</see></summary>
    Private _Assembly As Assembly
    ''' <summary>Gets or sets assembly to generate plugin for</summary>
    ''' <exception cref="ArgumentNullException">Value beign set is null</exception>
    Public Property Assembly() As Assembly
        <DebuggerStepThrough()> Get
            Return _Assembly
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Assembly)
            If value Is Nothing Then Throw New ArgumentNullException("value")
            _Assembly = value
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="ProjectTemplateDirectory"/> property</summary>
    Private _ProjectTemplateDirectory As String
    ''' <summary>Gets or sets directory to use project template from</summary>
    ''' <remarks>When null, build-in template is used</remarks>
    Public Property ProjectTemplateDirectory() As String
        <DebuggerStepThrough()> Get
            Return _ProjectTemplateDirectory
        End Get
        <DebuggerStepThrough()> Set(ByVal value As String)
            _ProjectTemplateDirectory = value
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="OutputDirectory"/> property</summary>
    Private _OutputDirectory As String
    ''' <summary>Gets or sets output directory to wtire plugin into</summary>
    ''' <exception cref="ArgumentNullException">Value being set is null</exception>
    Public Property OutputDirectory() As String
        <DebuggerStepThrough()> Get
            Return _OutputDirectory
        End Get
        <DebuggerStepThrough()> Set(ByVal value As String)
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
        <DebuggerStepThrough()> Get
            Return _IntermediateDirectory
        End Get
        <DebuggerStepThrough()> Set(ByVal value As String)
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
        <DebuggerStepThrough()> Get
            Return _CleanIntermediateDirectory
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Boolean)
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
        <DebuggerStepThrough()> Get
            Return _Types
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Type())
            _Types = value
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="Filter"/> property</summary>
    Private _Filer As PluginType = PluginType.Content Or PluginType.FileSystem Or PluginType.Lister Or PluginType.Packer
    ''' <summary>Gets or sets plugin type filter. Only plugins of types according to given OR-mask will be generated.</summary>
    ''' <returns>OR-ed values of <see cref="PluginType"/> type indicating which plugin types are generated</returns>
    ''' <value>Set plugin types to generate plugins for. By default all the plugin types are generated.</value>
    Public Property Filer() As PluginType
        <DebuggerStepThrough()> Get
            Return _Filer
        End Get
        <DebuggerStepThrough()> Set(ByVal value As PluginType)
            _Filer = value
        End Set
    End Property
    ''' <summary>Contains value of the <see cref="RenamingDictionary"/> property</summary>
    Private _RenamingDictionary As New Dictionary(Of String, String)
    ''' <summary>Dictionary containing <see cref="Type.FullName"/>s as key and plugin files names (witout extension) as values.</summary>
    ''' <remarks>Generated plugin files are named according to this dictionary. When dictionary entry for any type is missing, plugin is named according to full name of type.
    ''' <para><see cref="RenamingDictionary"/> takes precedence to <see cref="TotalCommanderPluginAttribute.Name"/>.</para></remarks>
    Public ReadOnly Property RenamingDictionary() As Dictionary(Of String, String)
        <DebuggerStepThrough()> Get
            Return _RenamingDictionary
        End Get
    End Property
    ''' <summary>Contains value of the <see cref="LogToConsole"/> property</summary>
    Private _LogToConsole As Boolean = False
    ''' <summary>Gets or sets value indicationg if generator will log its progress to console</summary>
    Friend Property LogToConsole() As Boolean
        <DebuggerStepThrough()> Get
            Return _LogToConsole
        End Get
        <DebuggerStepThrough()> Set(ByVal value As Boolean)
            _LogToConsole = value
        End Set
    End Property
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
    ''' <summary>Contains value of the <see cref="SnkPath"/> property</summary>
    Private _SnkPath$
    ''' <summary>Gets or sets path to snk (strong name key) file to sign wrapper assembly with</summary>
    ''' <remarks>When this property is non-null plugin wrapper assembly is given <see cref="AssemblyKeyFileAttribute"/> and it is signed using the sn.exe utility.</remarks>
    Public Property SnkPath$()
        <DebuggerStepThrough()> Get
            Return _SnkPath
        End Get
        <DebuggerStepThrough()> Set(ByVal value$)
            _SnkPath = value
        End Set
    End Property
#Region "Paths"
    ' ''' <summary>Contains value of the <see cref="MSBuild"/></summary>
    'Private _msbuild$ = My.Settings.MSBuild
    ' ''' <summary>Gets to sets path to msbuild.exe</summary>
    ' ''' <remarks>Default value is stored in settings</remarks>
    'Public Property MSBuild$()
    '    <DebuggerStepThrough()> Get
    '        Return _msbuild
    '    End Get
    '    <DebuggerStepThrough()> Set(ByVal value$)
    '        _msbuild = value
    '    End Set
    'End Property
    ' ''' <summary>Contains value of the <see cref="SN"/> property</summary>
    'Private _SN$ = My.Settings.sn
    ' ''' <summary>Gets or sets path to the sn.exe utility used fro signing assemblies</summary>
    ' ''' <remarks>Used only when <see cref="SnkPath"/> is non-null</remarks>
    'Public Property SN$()
    '    <DebuggerStepThrough()> Get
    '        Return _SN
    '    End Get
    '    <DebuggerStepThrough()> Set(ByVal value$)
    '        _SN = value
    '    End Set
    'End Property
#End Region
#End Region
#Region "Generation"
    ''' <summary>Generates plugins</summary>
    ''' <exception cref="MissingMethodException">Plugin base type declares method with <see cref="PluginMethodAttribute"/> pointing to method that is not member of that type.</exception>
    ''' <exception cref="AmbiguousMatchException">Plugin base type declares method with <see cref="PluginMethodAttribute"/> pointing to method that is overloaded on that type.</exception>
    ''' <exception cref="BuildException">Plugin template project failed to build.</exception>
    ''' <version version="1.5.3"><see cref="BuildException"/> can be thrown</version>
    ''' <version version="1.5.3"><c>ExitCodeException</c> is no longer thrown</version>
    Public Sub Generate()
        If Types Is Nothing Then
            Dim iTypes As New List(Of Type)
            'TODO: All plugin types
            For Each Type In Assembly.GetTypes
                If (Type.IsPublic OrElse Type.IsNestedPublic) AndAlso Not Type.IsAbstract AndAlso Not Type.IsGenericTypeDefinition AndAlso Type.HasDefaultCTor Then
                    Dim Ignore = Type.GetAttribute(Of NotAPluginAttribute)()
                    If Ignore IsNot Nothing Then Continue For
                    If GetType(FileSystemPlugin).IsAssignableFrom(Type) Then
                        iTypes.Add(Type)
                    End If
                End If
            Next
            Types = iTypes.ToArray
        End If
        Dim Ret As New List(Of String)
        For Each Type In Types
            Generate(Type)
        Next
    End Sub
    ''' <summary>Generates plugin from type</summary>
    ''' <param name="Type">Type to generate plugin for</param>
    ''' <returns>Path to generated plugin file</returns>
    ''' <exception cref="ArgumentNullException"><paramref name="Type"/> is null</exception>
    ''' <exception cref="ArgumentException"><paramref name="Type"/> is open generic type -or- <paramref name="Type"/> is abstract -or- <paramref name="Type"/> has not acessible default CTor -or- <paramref name="Type"/> is not public</exception>
    ''' <exception cref="MissingMethodException">Plugin base type declares method with <see cref="PluginMethodAttribute"/> pointing to method that is not member of that type.</exception>
    ''' <exception cref="AmbiguousMatchException">Plugin base type declares method with <see cref="PluginMethodAttribute"/> pointing to method that is overloaded on that type.</exception>
    ''' <exception cref="BuildException">Plugin template project failed to build.</exception>
    ''' <version version="1.5.3"><see cref="BuildException"/> can be thrown</version>
    ''' <version version="1.5.3"><c>ExitCodeException</c> is no longer thrown</version>
    Private Function Generate(ByVal Type As Type) As String
        Dim Thrown As Exception = Nothing
        Log(My.Resources.i_CreatingPluginForType, Type.Name)
        If Type Is Nothing Then Throw New ArgumentNullException("Type")
        If Type.IsGenericTypeDefinition Then Throw New ArgumentException(My.Resources.e_OpenGeneric.f(Type.FullName))
        If Type.IsAbstract Then Throw New ArgumentException(My.Resources.e_Abstract.f(Type.FullName))
        If Not Type.HasDefaultCTor Then Throw New ArgumentException(My.Resources.e_NoDefaultCTor.f(Type.FullName))
        If Not Type.IsPublic AndAlso Not Type.IsNestedPublic Then Throw New ArgumentException(My.Resources.e_NotPublic.f(Type.FullName))
        Dim Attr = Type.GetAttribute(Of TotalCommanderPluginAttribute)()
        Dim name$
        If RenamingDictionary.ContainsKey(Type.FullName) Then
            name = RenamingDictionary(Type.FullName)
        ElseIf Attr Is Nothing OrElse Attr.Name Is Nothing Then
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
            Log(My.Resources.i_TempIntermediateDir, IntermediateDirectory)
        Else
            IntermediateDirectory = String.Format(Me.IntermediateDirectory, GetSafeName(Type.FullName))
            If IO.File.Exists(IntermediateDirectory) Then IO.File.Delete(IntermediateDirectory)
            If IO.Directory.Exists(IntermediateDirectory) Then
                Log(My.Resources.i_PredefinedIntermediateDir, IntermediateDirectory)
                Try
                    For Each file In IO.Directory.GetFiles(IntermediateDirectory)
                        IO.File.Delete(file)
                    Next
                    For Each SubDir In IO.Directory.GetDirectories(IntermediateDirectory)
                        DeleteDir(SubDir, Me)
                    Next
                Catch ex As Exception
                    Log(My.Resources.e_CleanIntermediate)
                    Throw
                End Try
            Else
                Log(My.Resources.i_CeateUserDefinedIntermediateDir, IntermediateDirectory)
            End If
        End If
        If Not IO.Directory.Exists(IntermediateDirectory) Then IO.Directory.CreateDirectory(IntermediateDirectory)
        Try
            'Copy template
            Dim ProjectDirectory = IO.Path.Combine(IntermediateDirectory, "Project")
            Dim ProjectFile = IO.Path.Combine(ProjectDirectory, "Tools.TotalCommander.Plugin.vcxproj")
            Dim binDirectory = IO.Path.Combine(ProjectDirectory, "bin")
            Dim objDirectory = IO.Path.Combine(ProjectDirectory, "obj")
            Log(My.Resources.I_CreateProjectDir, ProjectDirectory)
            IO.Directory.CreateDirectory(ProjectDirectory)
            If Me.ProjectTemplateDirectory IsNot Nothing Then 'Copy
                Log(My.Resources.i_CopyTemplate, Me.ProjectTemplateDirectory)
                My.Computer.FileSystem.CopyDirectory(Me.ProjectTemplateDirectory, ProjectDirectory)
            Else 'Extract
                Log(My.Resources.i_ExtractTemplate, Me.ProjectTemplateDirectory)
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
            Log(My.Resources.i_CopyAssembly, Assembly.Location)
            IO.File.Copy(Assembly.Location, IO.Path.Combine(ProjectDirectory, IO.Path.GetFileName(Assembly.Location)), True)
            'Prepare projet
            Dim ext$
            If GetType(FileSystemPlugin).IsAssignableFrom(Type) Then
                ext = "wfx"
                Log(My.Resources.i_PluginType_wfx)
                PreparePlugin(Type, ProjectDirectory, GetType(FileSystemPlugin), "TC_WFX", PluginType.FileSystem)
            Else 'TODO: Support all plugin types
                Throw New ArgumentException(My.Resources.e_NotAPluginType.f(Type.FullName))
            End If
            Dim OutFile = IO.Path.Combine(binDirectory, name & "." & ext)
            Log(My.Resources.i_GeneratingAssemblyInfo)
            MakeAssemblyInfo(Type, ProjectDirectory)
            'Excute compiler

            Dim dic As New Dictionary(Of String, String)
            Log(My.Resources.i_InvokingCompiler)
            Log(My.Resources.EnvironmentalVariables)
            Log(vbTab & "PluginOutputExtension = {0}", ext)
            Log(vbTab & "PluginOutputName = {0}", name)

            'ec = Exec(MSBuild, String.Format("/r ""{0}""", ProjectFile), dic)
            'If ec <> 0 Then Throw New ExitCodeException(ec, IO.Path.GetFileName(MSBuild))

            Dim projectDoc = XDocument.Load(ProjectFile)
            projectDoc.AddAnnotation(SaveOptions.OmitDuplicateNamespaces)
            If SnkPath <> "" Then
                'projectDoc.<Project>.<ItemDefinitionGroup>.<Link>.First.SetElementValue(GetXmlNamespace().GetName("KeyFile"), SnkPath)
                projectDoc.<Project>.<ItemDefinitionGroup>.<Link>.First.Add(<KeyFile><%= SnkPath %></KeyFile>)
            End If
            Dim Project As New Project(projectDoc.CreateReader) '(ReaderOptions.OmitDuplicateNamespaces))
            Project.FullPath = ProjectFile
            Project.SetGlobalProperty("PluginOutputExtension", ext)
            Project.SetGlobalProperty("PluginOutputName", name)
            Project.SetGlobalProperty("Configuration", "general")
            Project.SetGlobalProperty("Platform", "Win32")
            If Not Project.Build(New CommandLineLogger) Then
                Throw New BuildException(My.Resources.e_FailedToBuildProject.f(ProjectFile))
            End If

            'Copy result
            Dim TargetFile = IO.Path.Combine(Me.OutputDirectory, name & "." & ext)
            Log(My.Resources.i_CopyOutput, OutFile, TargetFile)
            IO.File.Copy(OutFile, TargetFile, True)
            Dim PdbFile = IO.Path.Combine(binDirectory, name & ".pdb")
            If CopyPDB AndAlso IO.File.Exists(PdbFile) Then
                Dim PdbTarget As String = IO.Path.Combine(Me.OutputDirectory, name & ".pdb")
                Log(My.Resources.i_CopyPDB, PdbFile, PdbTarget)
                IO.File.Copy(PdbFile, PdbTarget, True)
            End If
            Dim configFile = IO.Path.Combine(IO.Path.GetDirectoryName(OutFile), name & ".config")
            If IO.File.Exists(configFile) Then
                Dim configTarget = IO.Path.Combine(Me.OutputDirectory, name & ".config")
                Log(My.Resources.i_CopyAppConfig, configFile, configTarget)
                IO.File.Copy(configFile, configTarget, True)
            End If
            Return name & "." & ext
        Catch ex As Exception
            Thrown = ex
            Throw
        Finally
            If CleanIntermediateDirectory Then
                Log(My.Resources.i_RemoveIntermediate, IntermediateDirectory)
                Try
                    IO.Directory.Delete(IntermediateDirectory, True)
                Catch ex As Exception
                    Log(My.Resources.e_RemoveIntermediate, ex.Message)
                    If Thrown Is Nothing Then Throw
                End Try
            End If
        End Try
    End Function
    ''' <summary>Prepares files for plugin</summary>
    ''' <param name="Type">Type to prepare project for</param>
    ''' <param name="ProjectDirectory">Directory peoject is stored in</param>
    ''' <param name="PluginType">Plugin base class</param>
    ''' <param name="DefinedBy">C++ #define to define call of CTOr of plugin</param>
    ''' <param name="EnumeratedPluginType">Identifies plugin type being created</param>
    ''' <exception cref="MissingMethodException"><paramref name="PluginType"/> has method decorated with <see cref="PluginMethodAttribute"/> with <see cref="PluginMethodAttribute.ImplementedBy"/> pointing to method that is not member of <paramref name="PluginType"/>.</exception>
    ''' <exception cref="AmbiguousMatchException"><paramref name="PluginType"/> has method decorated with <see cref="PluginMethodAttribute"/> with <see cref="PluginMethodAttribute.ImplementedBy"/> pointing to method that is overloaded on <paramref name="PluginType"/>.</exception>
    Private Sub PreparePlugin(ByVal Type As Type, ByVal ProjectDirectory$, ByVal PluginType As Type, ByVal DefinedBy As String, ByVal EnumeratedPluginType As PluginType)
        Using defineh = IO.File.Open(IO.Path.Combine(ProjectDirectory, "define.h"), IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read), _
                w As New IO.StreamWriter(defineh, System.Text.Encoding.Default)
            'defineh2 = IO.File.Open(IO.Path.Combine(ProjectDirectory, "define2.h"), IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read), _
            'w2 As New IO.StreamWriter(defineh2, System.Text.Encoding.Default), _
            'exports = IO.File.Open(IO.Path.Combine(ProjectDirectory, "Exports.def"), IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read), _
            'ew As New IO.StreamWriter(exports, System.Text.Encoding.Default)
            w.WriteLine("#define {0} {1}", DefinedBy, GetTypeSignature(Type))
            'w2.WriteLine("#define {0} {1}", DefinedBy, GetTypeSignature(Type))
            'ew.WriteLine("#include ""define2.h""")
            'ew.WriteLine("EXPORTS")
            For Each method In PluginType.GetMethods(BindingFlags.Instance Or BindingFlags.Public)
                Dim attr = method.GetAttribute(Of PluginMethodAttribute)(False)
                If attr Is Nothing Then Continue For
                Dim Define As Boolean
                If attr.ImplementedBy Is Nothing Then
                    Define = True
                Else
                    Dim ImplementingMethod = PluginType.GetMethod(attr.ImplementedBy, BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.NonPublic)
                    If ImplementingMethod IsNot Nothing AndAlso (Not ImplementingMethod.IsPublic AndAlso Not ImplementingMethod.IsFamily AndAlso Not ImplementingMethod.IsFamilyOrAssembly) Then ImplementingMethod = Nothing
                    If ImplementingMethod Is Nothing Then Throw New MissingMethodException(My.Resources.e_MissingPluginMethod.f(PluginType.FullName, attr.ImplementedBy, attr.GetType.FullName, method.Name))
                    Dim DerivedMethod = ImplementingMethod.GetOverridingMethod(Type)
                    Dim NotSupported = DerivedMethod.GetAttribute(Of MethodNotSupportedAttribute)()
                    Define = NotSupported Is Nothing
                End If
                If Define Then
                    w.WriteLine("#define " & attr.DefinedBy)
                    'w2.WriteLine("#define " & attr.DefinedBy)
                    'If attr.AdditionalCondition IsNot Nothing Then ew.WriteLine("#if {0}", attr.AdditionalCondition)
                    'ew.WriteLine("#ifdef " & attr.DefinedBy)
                    'ew.WriteLine(vbTab & attr.GetExportedAs(EnumeratedPluginType, method.Name))
                    'ew.WriteLine("#endif")
                    'If attr.AdditionalCondition IsNot Nothing Then ew.WriteLine("#endif")
                End If
            Next
            w.WriteLine("#ifdef PLUGIN_COMPILATION", Assembly.Location)
            w.WriteLine("#using ""{0}""", Assembly.Location)
            w.WriteLine("#using ""{0}""", Assembly.Load("Tools.TotalCommander, PublicKeyToken=373c02ac923768e6").Location)
            w.WriteLine("#define PLUGIN_NAME ""{0}""", Assembly.GetName.Name.Replace("\", "\\").Replace("""", "\"""))
            w.WriteLine("#endif", Assembly.Location)
        End Using
        Dim ia = Type.GetAttribute(Of PluginIconBaseAttribute)()
        If ia IsNot Nothing Then
            Log("Extracting icon")
            Try
                Using pIcon = ia.getIcon(Type), IconFile = IO.File.Open(IO.Path.Combine(ProjectDirectory, "Icon.ico"), IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read)
                    pIcon.Save(IconFile)
                End Using
            Catch ex As Exception
                Log("Error while extracting icon using {0}: {1}: {2}", ia.GetType.Name, ex.GetType.Name, ex.Message)
                Throw
            End Try
        End If
    End Sub
    ''' <summary>Writes the AssemblyInfo2.cpp file</summary>
    ''' <param name="Type">Plugin type</param>
    ''' <param name="ProjectDirectory">Project directory</param>
    Private Sub MakeAssemblyInfo(ByVal Type As Type, ByVal ProjectDirectory$)
        Using file = IO.File.Open(IO.Path.Combine(ProjectDirectory, "AssemblyInfo2.cpp"), IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.Read), _
                w As New IO.StreamWriter(file, System.Text.Encoding.Default)
            w.WriteLine("using namespace System;")
            w.WriteLine("using namespace System::Reflection;")
            w.WriteLine("using namespace System::Runtime::CompilerServices;")
            w.WriteLine("using namespace System::Runtime::InteropServices;")
            w.WriteLine("using namespace System::Security::Permissions;")

            Dim tAsm = Type.Assembly
            Dim Version = tAsm.GetAttribute(Of AssemblyVersionAttribute)()
            If Version IsNot Nothing Then
                w.WriteLine("[assembly:AssemblyVersionAttribute(""{0}"")];", Version.Version.Replace("\", "\\").Replace("""", "\"""))
            Else
                w.WriteLine("[assembly:AssemblyVersionAttribute(""{0}"")];", tAsm.GetName.Version.ToString)
            End If
            'Dim Culture = tAsm.GetAttribute(Of AssemblyCultureAttribute)()
            'If Culture IsNot Nothing Then w.WriteLine("[assembly:AssemblyCultureAttribute(""{0}"")];", Culture.Culture.Replace("\", "\\").Replace("""", "\"""))
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
            If NeutralResourcesLanguage IsNot Nothing Then w.WriteLine("[assembly:Resources::NeutralResourcesLanguageAttribute(""{0}"",Resources::UltimateResourceFallbackLocation::{1:F})];", NeutralResourcesLanguage.CultureName, NeutralResourcesLanguage.Location)
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
            If Guid IsNot Nothing Then w.WriteLine("[assembly:Runtime::InteropServices::GuidAttribute(""{0}"")];", Guid.Replace("\", "\\").Replace("""", "\"""))
            If Title IsNot Nothing Then w.WriteLine("[assembly:AssemblyTitleAttribute(""{0}"")];", Title.Replace("\", "\\").Replace("""", "\"""))
            If Description IsNot Nothing Then w.WriteLine("[assembly:AssemblyDescriptionAttribute(""{0}"")];", Description.Replace("\", "\\").Replace("""", "\"""))

            If SnkPath IsNot Nothing Then w.WriteLine("[assembly:AssemblyKeyFileAttribute (""{0}"")];", SnkPath.Replace("\", "\\").Replace("""", "\"""))
        End Using
    End Sub
#End Region
#Region "Utility"
    ''' <summary>Log a log information</summary>
    ''' <param name="Text">Formating string</param>
    ''' <param name="Obj">Format patameters</param>
    ''' <seelaso cref="System.String.Format"/>
    <DebuggerStepThrough()> _
    Private Sub Log(ByVal Text$, ByVal ParamArray Obj As Object())
        If LogToConsole Then Console.WriteLine(Text, Obj)
    End Sub

    ''' <summary>Executes a process and waits for it to terminate</summary>
    ''' <param name="Program">Program to execute</param>
    ''' <param name="CommandLine">Program arguments</param>
    ''' <param name="Env">Environmentl variables for the process</param>
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
        Log(My.Resources.i_Executing, Program, CommandLine)
        p.Start()
        p.WaitForExit()
        Log(My.Resources.i_ExitCode, vbTab, p.ExitCode)
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
    ''' <summary>Attempts to recursivelly delete directory</summary>
    ''' <param name="Dir">Directory to be deleted</param>
    ''' <remarks>Skips subdirectories than cannot be deleted, throws an exception when file cannot be deleted</remarks>
    ''' <param name="Instance">Instance of the <see cref="Generator"/> class. Used only for warning logging. Can be null.</param>
    ''' <param name="ContentOnly">Does not delete the <paramref name="Dir"/> directory, only deletes all its content.</param>
    Friend Shared Sub DeleteDir(ByVal Dir$, ByVal Instance As Generator, Optional ByVal ContentOnly As Boolean = False)
        For Each file In IO.Directory.GetFiles(Dir)
            IO.File.Delete(file)
        Next
        For Each SubDir In IO.Directory.GetDirectories(Dir)
            DeleteDir(SubDir, Instance)
        Next
        If ContentOnly Then Exit Sub
        Try
            IO.Directory.Delete(Dir, True)
        Catch ex As Exception
            If Instance IsNot Nothing Then Instance.Log(My.Resources.w_DelDir, Dir, ex.Message)
        End Try
    End Sub
#End Region
End Class

''' <summary>Exception thrown when build failed</summary>
''' <version version="1.5.3">This class is new in version 1.5.3</version>
Public Class BuildException
    Inherits Exception
    ''' <summary>Initializes a new instance of the <see cref="BuildException"/> class with a specified error message.</summary>
    ''' <param name="message">The message that describes the error.</param>
    Public Sub New(ByVal message$)
        MyBase.New(message)
    End Sub
End Class

''' <summary>Logs events to command line</summary>
Friend Class CommandLineLogger
    Inherits Logger
    ''' <summary>Subscribes the logger to specific events.</summary>
    ''' <param name="eventSource">The available events that a logger can subscribe to.</param>
    Public Overrides Sub Initialize(ByVal eventSource As Microsoft.Build.Framework.IEventSource)
        'Register for the ProjectStarted, TargetStarted, and ProjectFinished events
        AddHandler eventSource.ProjectStarted, Sub(sender As Object, e As Microsoft.Build.Framework.ProjectStartedEventArgs)
                                                   Console.WriteLine(My.Resources.i_ProjectStarted + e.ProjectFile)
                                               End Sub
        AddHandler eventSource.TargetStarted, Sub(sender As Object, e As Microsoft.Build.Framework.TargetStartedEventArgs)
                                                  If Verbosity = Microsoft.Build.Framework.LoggerVerbosity.Detailed Then _
                                                      Console.WriteLine(My.Resources.i_TargetStarted + e.TargetName)
                                              End Sub
        AddHandler eventSource.ProjectFinished, Sub(sender As Object, e As Microsoft.Build.Framework.ProjectFinishedEventArgs)
                                                    Console.WriteLine(My.Resources.i_ProjectFinished + e.ProjectFile)
                                                End Sub
        AddHandler eventSource.ErrorRaised, Sub(sender As Object, e As Microsoft.Build.Framework.BuildErrorEventArgs)
                                                Console.WriteLine(FormatErrorEvent(e))
                                            End Sub
        AddHandler eventSource.WarningRaised, Sub(sender As Object, e As Microsoft.Build.Framework.BuildWarningEventArgs)
                                                  Console.WriteLine(FormatWarningEvent(e))
                                              End Sub
    End Sub
End Class